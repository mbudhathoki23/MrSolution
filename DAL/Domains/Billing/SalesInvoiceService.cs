using Dapper;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.Master.InventorySetup;
using DatabaseModule.Master.ProductSetup;
using DatabaseModule.Setup.DocumentNumberings;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.AppLogger;
using MrDAL.Domains.Shared.AppLogger.Models;
using MrDAL.Global.Common;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Domains.Billing;

public class SalesInvoiceService
{
    public async Task<ListResult<Counter>> GetCountersAsync(int branchId)
    {
        var result = new ListResult<Counter>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.List = (await conn.QueryAsync<Counter>("SELECT * FROM AMS.Counter  ",
                new
                {
                    brId = branchId
                })).AsList(); //WHERE Branch_ID = @brId
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<Counter>(this);
        }

        return result;
    }

    public async Task<ListResult<SalesMembershipModel>> GetMembersAsync(int? branchId = null)
    {
        var result = new ListResult<SalesMembershipModel>();
        var query = new StringBuilder(@"
            SELECT ms.*, mt.Discount AS DiscountPercent, mt.MemberDesc AS MemberType, temp.Balance
            FROM AMS.MemberShipSetup ms
            LEFT JOIN(SELECT bill.MShipId, ISNULL(SUM(bill.LN_Amount), 0) AS balance FROM ams.SB_Master bill WHERE bill.MShipId IS NOT NULL GROUP BY bill.MShipId) AS temp ON temp.MShipId = ms.MShipId
            LEFT JOIN  AMS.MemberType mt ON mt.MemberTypeId = ms.MemberTypeId
            WHERE ms.ActiveStatus = 1 AND Convert(date, ms.MExpireDate) > @prToday ");
        var parameters = new DynamicParameters();
        parameters.Add("prToday", DateTime.Today);

        if (branchId.HasValue)
        {
            query.AppendLine("AND ms.BranchId = @prBranchId ");
            parameters.Add("prBranchId", branchId.Value);
        }

        query.AppendLine(" ORDER BY ms.MShipDesc ");

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.List = (await conn.QueryAsync<SalesMembershipModel>(query.ToString(), parameters)).AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SalesMembershipModel>(this);
        }

        return result;
    }

    public async Task<InfoResult<SalesMembershipModel>> GetMembershipDetailAsync(int memberId)
    {
        var result = new InfoResult<SalesMembershipModel>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.Model = await conn.QueryFirstOrDefaultAsync<SalesMembershipModel>(
                @"SELECT ms.*, mt.Discount AS DiscountPercent, mt.MemberDesc AS MemberType, (SELECT SUM(LN_Amount) AS Balance
                        FROM AMS.SB_Master WHERE MShipId = @prMembershipId GROUP BY MShipId) Balance
                        FROM AMS.MemberShipSetup ms
                        LEFT OUTER JOIN AMS.MemberType mt ON mt.MemberTypeId = ms.MemberTypeId
                        WHERE ms.MShipId = @prMembershipId ",
                new
                {
                    prMembershipId = memberId
                });
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesMembershipModel>(this);
        }

        return result;
    }

    public async Task<InfoResult<string>> HoldSalesInvoiceAsync(SalesInvoiceEModel model)
    {
        var result = new InfoResult<string>();

        var conn = new SqlConnection(GetConnection.ConnectionString);
        try
        {
            await conn.OpenAsync();

            // check document numbering
            var docNumberings = (await conn.QueryAsync<DocumentNumbering>(
                @"SELECT * FROM AMS.DocumentNumbering WHERE DocModule = 'TSB' AND DocStartDate <= @prToday AND DocEndDate >= @prToday AND Status = 1 ",
                new { prToday = model.CurrentDateTime.Date })).AsList();
            if (!docNumberings.Any())
            {
                result.ResultType = ResultType.EntityNotExists;
                result.ErrorMessage = "No doc-number exists for Temporary Sales Invoice.";
                return result;
            }

            if (docNumberings.Count > 1)
            {
                result.ResultType = ResultType.ValidationError;
                result.ErrorMessage = "Multiple document numbering exists for Temporary Sales Invoice.";
                return result;
            }

            var docNumber = docNumberings[0];
            var newInvoiceNo =
                ObjGlobal.ReturnDocumentNumbering("AMS.temp_SB_Master", "SB_Invoice", "TSB", docNumber.DocDesc);
            if (string.IsNullOrWhiteSpace(newInvoiceNo))
            {
                result.ErrorMessage = "Error generating new sales invoice document number. ";
                result.ResultType = ResultType.ValidationError;
                return result;
            }

            // begin transaction and insert into database
            using var trans = conn.BeginTransaction();

            // insert bill
            var newInvoice = new Temp_SB_Master()
            {
                SB_Invoice = newInvoiceNo,
                Action_Type = "HOLD",
                Party_Name = model.PartyName,
                Address = model.Address,
                Mobile_No = model.MobileNo,
                MShipId = model.MembershipId,
                CBranch_Id = model.CBranchId,
                Agent_Id = model.AgentId,
                Invoice_Date = model.CurrentDateTime.Date,
                Invoice_Miti = ObjGlobal.ReturnNepaliDate(model.CurrentDateTime.ToString("dd/MM/yyyy")),
                Invoice_Time = model.CurrentDateTime,
                Customer_Id = model.LedgerId,
                Vat_No = model.VatNo,
                Contact_Person = model.ContactPerson,
                ChqNo = model.ChqNo,
                ChqDate = model.ChqDate,
                Invoice_Type = model.InvoiceType,
                Invoice_Mode = model.InvoiceMode,
                Payment_Mode = string.Empty,
                DueDate = model.DueDate,
                DueDays = model.DueDays,
                CounterId = model.CounterId,
                Cur_Id = model.CurrencyId.GetValueOrDefault(),
                Cur_Rate = model.CurrencyRate.GetValueOrDefault(),
                B_Amount = model.NAmount,
                T_Amount = model.TermAmount,
                Tbl_Amount = model.TaxableAmount,
                Tender_Amount = 0,
                Return_Amount = 0,
                FiscalYearId = model.FiscalYearId,
                SyncRowVersion = 1,
                SyncGlobalId = Guid.NewGuid(),
                SyncCreatedOn = model.CurrentDateTime,
                SyncOriginId = Guid.Empty,
                Enter_Date = model.CurrentDateTime,
                Enter_By = model.EnteredBy,
                Printed_Date = model.CurrentDateTime,
                Printed_By = model.EnteredBy,
                In_Words = ClsMoneyConversion.MoneyConversion(model.NetAmount.ToString(ObjGlobal.SysAmountFormat)),
                N_Amount = model.NetAmount,
                LN_Amount = model.LocalNetAmount,
                V_Amount = model.TaxAmount,
                Remarks = model.Remarks,
                Is_Printed = model.Printed,
                No_Print = 1,
                Vno_Date = model.CurrentDateTime.Date,
                Vno_Miti = ObjGlobal.ReturnNepaliDate(model.CurrentDateTime.ToString("dd/MM/yyyy"))
            };
            await conn.InsertAsync(newInvoice, trans);

            // insert details
            foreach (var item in model.Items)
            {
                item.SB_Invoice = newInvoiceNo;
                if (model.SyncOriginId != null)
                {
                    item.SyncOriginId = (Guid)model.SyncOriginId;
                }
                item.SyncGlobalId = Guid.NewGuid();
                item.SyncRowVersion = 1;
                item.SyncCreatedOn = model.CurrentDateTime;

                // ReSharper disable once UseConfigureAwaitFalse
                await conn.InsertAsync(new Temp_SB_Details
                {
                    SB_Invoice = newInvoice.SB_Invoice,
                    Invoice_SNo = (int)item.Invoice_SNo,
                    P_Id = item.P_Id,
                    Gdn_Id = item.Gdn_Id,
                    Alt_Qty = item.Alt_Qty,
                    Qty = item.Qty,
                    Unit_Id = item.Unit_Id,
                    Rate = item.Rate,
                    B_Amount = item.B_Amount,
                    T_Amount = item.T_Amount,
                    N_Amount = item.N_Amount,
                    AltStock_Qty = item.AltStock_Qty,
                    Stock_Qty = item.Stock_Qty,
                    Narration = item.Narration,
                    SO_Invoice = item.SO_Invoice,
                    SC_SNo = item.SC_SNo,
                    Tax_Amount = item.Tax_Amount,
                    V_Amount = item.V_Amount,
                    V_Rate = item.V_Rate,
                    Free_Unit_Id = item.Free_Unit_Id,
                    Free_Qty = item.Free_Qty,
                    StockFree_Qty = item.StockFree_Qty,
                    ExtraFree_Unit_Id = item.ExtraFree_Unit_Id,
                    ExtraFree_Qty = item.ExtraFree_Qty,
                    ExtraStockFree_Qty = item.ExtraStockFree_Qty,
                    T_Product = item.T_Product,
                    SR_Ledger = item.SR_Ledger,
                    S_Ledger = item.S_Ledger,
                    Serial_No = item.Serial_No,
                    Batch_No = item.Batch_No,
                    Exp_Date = item.Exp_Date,
                    Manu_Date = item.Manu_Date,
                    PDiscount = item.PDiscount,
                    PDiscountRate = item.PDiscountRate,
                    Alt_UnitId = item.Alt_UnitId,
                    BDiscount = item.BDiscount,
                    BDiscountRate = item.BDiscountRate,
                    MaterialPost = item.MaterialPost,
                    SC_Invoice = item.SC_Invoice,
                    SO_Sno = item.SO_Sno,
                    ServiceChargeRate = item.ServiceChargeRate,
                    ServiceCharge = item.ServiceCharge,
                    SyncRowVersion = 1,
                    SyncCreatedOn = model.CurrentDateTime,
                    SyncGlobalId = Guid.NewGuid()
                }, trans);
            }

            trans.Commit();
            result.Model = newInvoice.SB_Invoice;
            result.Success = true;

            AppLoggerService.LogAsync(AppLogContext.UserActivity,
                $"Created hold invoice {newInvoice.SB_Invoice} to process later.",
                AppLogType.TemporaryInvoice, AppLogActionType.New, AppLogRefType.TempSalesInvoice, newInvoiceNo,
                newInvoiceNo, null, newInvoice, model.CBranchId);
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<string>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<string>> SaveInvoiceAsync(SalesInvoiceEModel model)
    {
        var result = new InfoResult<string>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            await conn.OpenAsync();

            // check document numbering
            var docNumberings = (await conn.QueryAsync<DocumentNumbering>(
                @"SELECT * FROM AMS.DocumentNumbering WHERE DocModule = 'POS' AND DocStartDate <= @prToday AND DocEndDate >= @prToday AND Status = 1 AND DocBranch = @prBranchId",
                new { prToday = model.CurrentDateTime.Date, prBranchId = model.CBranchId })).AsList();
            if (!docNumberings.Any())
            {
                result.ResultType = ResultType.EntityNotExists;
                result.ErrorMessage = "No doc-number exists for Sales Invoice.";
                return result;
            }

            if (docNumberings.Count > 1)
            {
                result.ResultType = ResultType.ValidationError;
                result.ErrorMessage = "Multiple document numbering exists for Temporary Sales Invoice.";
                return result;
            }

            var docNumber = docNumberings[0];

            var newInvoiceNo =
                ObjGlobal.ReturnDocumentNumbering("AMS.SB_Master", "SB_Invoice", "POS", docNumber.DocDesc);
            if (string.IsNullOrWhiteSpace(newInvoiceNo))
            {
                result.ErrorMessage = "Error generating new sales invoice document number. ";
                result.ResultType = ResultType.ValidationError;
                return result;
            }

            Temp_SB_Master refHoldInvoice = null;
            if (!string.IsNullOrWhiteSpace(model.RefTempInvoiceId))
            {
                refHoldInvoice = await conn.QueryFirstOrDefaultAsync<Temp_SB_Master>(
                    @"SELECT * FROM AMS.temp_SB_Master WHERE SB_Invoice =@prRefNo AND Action_Type = 'HOLD' ",
                    new { prRefNo = model.RefTempInvoiceId });
                if (refHoldInvoice == null ||
                    !refHoldInvoice.Action_Type.Equals("HOLD", StringComparison.OrdinalIgnoreCase))
                {
                    result.ErrorMessage = "The selected temporary/hold invoice is not valid. ";
                    result.ResultType = ResultType.ValidationError;
                    return result;
                }
            }

            // check day-end
            var conflictedInvoice = await conn.QueryFirstOrDefaultAsync<SB_Master>(
                "SELECT * FROM AMS.SB_Master WHERE Invoice_Time >= @prDateTime ",
                new { prDateTime = model.CurrentDateTime });
            if (conflictedInvoice != null)
            {
                result.ErrorMessage =
                    $"Invalid Operation because of system datetime conflict. A sales invoice {conflictedInvoice.SB_Invoice} already exists in future date. Please contact support.";
                result.ResultType = ResultType.ValidationError;
                return result;
            }

            // check for active fiscal year
            var fiscalYear = await conn.QueryFirstOrDefaultAsync<FiscalYear>(
                @"SELECT * FROM AMS.FiscalYear WHERE FY_Id = @prFiscalYearId ",
                new { prFiscalYearId = model.FiscalYearId });
            if (!fiscalYear.Current_FY || fiscalYear.Start_ADDate.Date > model.CurrentDateTime.Date ||
                fiscalYear.End_ADDate < model.CurrentDateTime)
            {
                result.ErrorMessage =
                    "Invalid fiscal year to generate new invoice. Please restart the software, check system datetime or contact support.";
                result.ResultType = ResultType.ValidationError;
                return result;
            }

            // begin transaction and insert into database
            using var trans = conn.BeginTransaction();

            // insert bill
            var newInvoice = new SB_Master
            {
                SB_Invoice = newInvoiceNo,
                Action_Type = "NEW",
                Party_Name = model.PartyName,
                Address = model.Address,
                Mobile_No = model.MobileNo,
                MShipId = model.MembershipId,
                CBranch_Id = model.CBranchId,
                Agent_Id = model.AgentId,
                Invoice_Date = model.CurrentDateTime.Date,
                Invoice_Miti = ObjGlobal.ReturnNepaliDate(model.CurrentDateTime.ToString("dd/MM/yyyy")),
                Invoice_Time = model.CurrentDateTime,
                Customer_Id = model.LedgerId,
                Vat_No = model.VatNo,
                Contact_Person = model.ContactPerson,
                ChqNo = model.ChqNo,
                ChqDate = model.ChqDate,
                Invoice_Type = model.InvoiceType,
                Invoice_Mode = model.InvoiceMode,
                Payment_Mode = model.PaymentMode,
                DueDate = model.DueDate,
                DueDays = model.DueDays.GetInt(),
                CounterId = model.CounterId,
                Cur_Id = model.CurrencyId.GetInt(),
                Cur_Rate = model.CurrencyRate.GetDecimal(),
                B_Amount = model.NAmount,
                T_Amount = model.TermAmount,
                Tbl_Amount = model.TaxableAmount,
                Tender_Amount = model.TenderAmount,
                Return_Amount = model.ReturnAmount,
                FiscalYearId = model.FiscalYearId,
                SyncRowVersion = 1,
                SyncGlobalId = Guid.NewGuid(),
                SyncCreatedOn = model.CurrentDateTime,
                SyncOriginId = model.SyncOriginId.GetGuid(),
                Enter_Date = model.CurrentDateTime,
                Enter_By = model.EnteredBy,
                Printed_Date = model.CurrentDateTime,
                Printed_By = model.EnteredBy,
                In_Words = ClsMoneyConversion.MoneyConversion(model.NetAmount.ToString(ObjGlobal.SysAmountFormat)),
                N_Amount = model.NetAmount,
                LN_Amount = model.LocalNetAmount,
                V_Amount = model.TaxAmount,
                Remarks = model.Remarks ?? string.Empty,
                Is_Printed = model.Printed,
                No_Print = 1,
                Subledger_Id = model.SubLedgerId,
                PB_Vno = model.RefTempInvoiceId,
                R_Invoice = false,
                Audit_Lock = false
            };
            await conn.InsertAsync(newInvoice, trans);

            // insert term
            foreach (var term in model.Terms)
            {
                term.SB_VNo = newInvoiceNo;
                term.SyncOriginId = model.SyncOriginId.GetGuid();
                term.SyncGlobalId = Guid.NewGuid();
                term.SyncRowVersion = 1;
                term.SyncCreatedOn = model.CurrentDateTime;

                await conn.InsertAsync(term, trans);
            }

            // insert details
            foreach (var item in model.Items)
            {
                item.SB_Invoice = newInvoiceNo;
                item.SyncOriginId = model.SyncOriginId.GetGuid();
                item.SyncGlobalId = Guid.NewGuid();
                item.SyncRowVersion = 1;
                item.SyncCreatedOn = model.CurrentDateTime;

                await conn.InsertAsync(item, trans);
            }

            // update the status of hold bill if it exists
            if (refHoldInvoice != null)
            {
                await conn.ExecuteAsync(
                    @"UPDATE AMS.temp_SB_Master SET Action_Type = 'BILLED' WHERE SB_Invoice = @prTempInvoiceId AND Action_Type = 'HOLD' ",
                    new { prTempInvoiceId = refHoldInvoice.SB_Invoice }, trans);
            }

            trans.Commit();
            result.Model = newInvoice.SB_Invoice;
            result.Success = true;

            AppLoggerService.LogAsync(AppLogContext.UserActivity,
                $"Generated sales invoice {newInvoice.SB_Invoice}.",
                AppLogType.SalesInvoice, AppLogActionType.New, AppLogRefType.SalesMaster, newInvoiceNo,
                newInvoiceNo, null, newInvoice, model.CBranchId);
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<string>(this);
        }

        return result;
    }

    public async Task<InfoResult<SB_Master>> GetLastInvoiceAsync(int branchId, int? counterId, DateTime? date)
    {
        var result = new InfoResult<SB_Master>();

        var query = new StringBuilder("SELECT * FROM AMS.SB_Master WHERE CBranch_Id = @prBranchId ");
        var parameters = new DynamicParameters();
        parameters.Add("prBranchId", branchId);

        if (counterId.HasValue)
        {
            query.AppendLine("AND CounterId = @prCounterId ");
            parameters.Add("prCounterId", counterId.Value);
        }

        if (date.HasValue)
        {
            query.AppendLine("AND CONVERT(DATE, Invoice_Time) = @prDate ");
            parameters.Add("prDate", date.Value);
        }

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.Model = await conn.QueryFirstOrDefaultAsync<SB_Master>(query.ToString(), parameters);
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SB_Master>(this);
        }

        return result;
    }

    public async Task<InfoResult<string>> GetNextInvoiceNoAsync(int branchId)
    {
        var result = new InfoResult<string>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var docNumberings = (await conn.QueryAsync<DocumentNumbering>(
                @"SELECT * FROM AMS.DocumentNumbering WHERE DocModule = 'POS'  AND Status = 1 AND DocBranch = @prBranchId",
                new
                {
                    prToday = DateTime.Now.Date,
                    prBranchId = branchId
                })).AsList(); //AND DocStartDate <= @prToday AND DocEndDate >= @prToday
            if (!docNumberings.Any())
            {
                result.ResultType = ResultType.EntityNotExists;
                result.ErrorMessage = "No doc-number exists for Sales Invoice.";
                return result;
            }

            if (docNumberings.Count > 1)
            {
                result.ResultType = ResultType.ValidationError;
                result.ErrorMessage = "Multiple document numbering exists for Sales Invoice.";
                return result;
            }

            var docNumber = docNumberings[0];
            var newInvoiceNo =
                ObjGlobal.ReturnDocumentNumbering("AMS.SB_Master", "SB_Invoice", "POS", docNumber.DocDesc);
            result.Model = newInvoiceNo;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<string>(this);
        }

        return result;
    }

    public async Task<InfoResult<Product>> GetProductAsync(long productId)
    {
        var result = new InfoResult<Product>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.Model = await conn.GetAsync<Product>(productId);
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<Product>(this);
        }

        return result;
    }

    public async Task<ListResult<Temp_SB_Master>> GetHoldInvoicesAsync(int branchId)
    {
        var result = new ListResult<Temp_SB_Master>();
        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var cmdString = @$"
            SELECT SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_ID, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_ID, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, MShipId, TableId, DoctorId, PatientId, HDepartmentId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
            FROM AMS.temp_SB_Master
            WHERE Invoice_Date='{DateTime.Now:yyyy-MM-dd}' AND SB_Invoice NOT IN ( SELECT sm.PB_Vno FROM AMS.SB_Master sm WHERE sm.PB_Vno <> '' OR sm.PB_Vno IS NOT NULL ) AND Action_Type <> 'POST' AND CBranch_Id = @prBranchId AND FiscalYearId = @prFiscalYear;";
            var query = await conn.QueryAsync<Temp_SB_Master>(cmdString, new
            {
                prBranchId = branchId,
                prFiscalYear = ObjGlobal.SysFiscalYearId
            });
            result.List = query.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<Temp_SB_Master>(this);
        }

        return result;
    }

    public async Task<DataTable> GetHoldInvoicesDetailsAsync(string voucherNo)
    {
        try
        {
            var cmdString = @$"
                SELECT p.PShortName Barcode,p.PName ProductName, tsd.QTY Qty, pu.UnitCode UnitCode, tsd.Rate RATE, tsd.B_Amount B_Amount,tsd.T_Amount,tsd.N_Amount N_Amount
                FROM AMS.temp_SB_Details tsd
	                 LEFT OUTER JOIN AMS.Product p ON tsd.P_Id = p.PID
	                 LEFT OUTER JOIN AMS.ProductUnit pu ON p.PUnit = pu.UID
                WHERE tsd.SB_Invoice = '{voucherNo}' ";
            var query = await SqlExtensions.ExecuteDataSetAsync(cmdString);
            return query.Tables[0];
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return new DataTable();
        }
    }

    public async Task<InfoResult<TempInvoiceViewModel>> GetTempInvoice(string tempInvoiceId)
    {
        var result = new InfoResult<TempInvoiceViewModel>();
        var model = new TempInvoiceViewModel();

        try
        {
            var cmdString = @"
            SELECT * FROM AMS.Temp_SB_Details WHERE SB_Invoice = @prInvoiceNo ";
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            model.Master = await conn.QueryFirstOrDefaultAsync<Temp_SB_Master>(cmdString, new { prInvoiceNo = tempInvoiceId });

            if (model.Master != null)
            {
                cmdString = @"
                SELECT * FROM AMS.Temp_SB_Details WHERE SB_Invoice = @prInvoiceId ";
                model.Items = (await conn.QueryAsync<Temp_SB_Master>(cmdString, new { prInvoiceId = tempInvoiceId })).AsList() as IList<Temp_SB_Details>;
            }

            result.Model = model;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<TempInvoiceViewModel>(this);
        }

        return result;
    }

    public async Task<InfoResult<decimal>> GetBalanceAsync(long ledgerId)
    {
        var result = new InfoResult<decimal>();

        try
        {
            const string cmdString = @"
            SELECT ISNULL(SUM(LocalDebit_Amt -LocalCredit_Amt),0) FROM AMS.accountDetails WHERE Ledger_id = @prLedgerId AND Voucher_Date <= GETDATE() ";
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var balance = await conn.QueryFirstOrDefaultAsync<decimal>(cmdString, new { prLedgerId = ledgerId });

            result.Model = balance;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<decimal>(this);
        }

        return result;
    }

    public async Task<ListResult<SalesInvoiceProductModel>> GetProductsAsync(bool includeNonExistedStock)
    {
        var result = new ListResult<SalesInvoiceProductModel>();

        var query = new StringBuilder(@"
            SELECT p.PID AS ProductId, bc.Barcode, p.PName AS ProductName, ISNULL ( CAST(stock.Qty AS DECIMAL(18, 6)), 0 ) AS BalanceQty, PU.UnitCode, CAST(p.PBuyRate AS DECIMAL(18, 4)) AS BuyRate, bc.SalesRate, bc.MRP AS MrpRate, bc.Trade AS TradeRate, bc.Wholesale AS WholesaleRate, bc.Retail AS RetailRate, bc.Dealer AS DealerRate, bc.Resellar AS ResellerRate, ISNULL ( PG.GrpName, 'No Group' ) AS GroupName, ISNULL ( PSG.SubGrpName, 'No SubGroup' ) AS SubGroupName, p.PUnit AS UnitId, p.PAltUnit AS AltUnitId, au.UnitCode AS AltUnitCode, p.PQtyConv AS QtyConversionRate, p.PAltConv AS AltQtyConversionRate, p.PTax AS TaxPercent
             FROM AMS.BarcodeList bc
                  JOIN AMS.Product p ON p.PID = bc.ProductId
                  LEFT outer JOIN (
                              SELECT sd.Product_Id, SUM ( CASE WHEN sd.EntryType = 'I' THEN sd.Qty
                                                            WHEN sd.EntryType = 'O' THEN -sd.StockQty ELSE 0 END
                                                        ) AS Qty
                               FROM AMS.StockDetails sd
                               WHERE sd.Voucher_Date <= CONVERT ( DATE, GETDATE ( ))
                               GROUP BY sd.Product_Id
                            ) AS stock ON stock.Product_Id = p.PID
                  LEFT JOIN AMS.ProductUnit AS PU ON p.PUnit = PU.UID
                  LEFT JOIN AMS.ProductGroup AS PG ON PG.PGrpID = p.PGrpId
                  LEFT JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId = p.PSubGrpId
                  LEFT JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
             WHERE p.Status = 1 ");

        if (!includeNonExistedStock)
        {
            query.AppendLine("AND ISNULL(stock.Qty,0) > 0 ");
        }

        query.AppendLine("ORDER BY p.PName ");

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<SalesInvoiceProductModel>(query.ToString());

            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SalesInvoiceProductModel>(this);
        }

        return result;
    }

    public async Task<InfoResult<SalesInvoiceProductModel>> GetProductAsync(string barCode)
    {
        var result = new InfoResult<SalesInvoiceProductModel>();
        const string query = @"
            SELECT p.PID AS ProductId, bc.Barcode, p.PName AS ProductName, p.PShortName AS ProductShortName, ISNULL ( CAST(stock.Qty AS DECIMAL(18, 6)), 0 ) AS BalanceQty, PU.UnitCode, CAST(p.PBuyRate AS DECIMAL(18, 4)) AS BuyRate, bc.SalesRate, bc.MRP AS MrpRate, bc.Trade AS TradeRate, bc.Wholesale AS WholesaleRate, bc.Retail AS RetailRate, bc.Dealer AS DealerRate, bc.Resellar AS ResellerRate, ISNULL ( PG.GrpName, 'No Group' ) AS GroupName, ISNULL ( PSG.SubGrpName, 'No SubGroup' ) AS SubGroupName, p.PUnit AS UnitId, p.PAltUnit AS AltUnitId, au.UnitCode AS AltUnitCode, p.PQtyConv AS QtyConversionRate, p.PAltConv AS AltQtyConversionRate, p.PTax AS TaxPercent
             FROM AMS.BarcodeList bc
                  JOIN AMS.Product p ON p.PID = bc.ProductId
                  LEFT JOIN (
                              SELECT sd.Product_Id, SUM ( CASE WHEN sd.EntryType = 'I' THEN sd.Qty
                                                            WHEN sd.EntryType = 'O' THEN -sd.StockQty ELSE 0 END
                                                        ) AS Qty
                               FROM AMS.StockDetails sd
                               WHERE sd.Voucher_Date <= CONVERT ( DATE, GETDATE ( ))
                               GROUP BY sd.Product_Id
                            ) AS stock ON stock.Product_Id = p.PID
                  LEFT JOIN AMS.ProductUnit AS PU ON p.PUnit = PU.UID
                  LEFT JOIN AMS.ProductGroup AS PG ON PG.PGrpID = p.PGrpId
                  LEFT JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId = p.PSubGrpId
                  LEFT JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
             WHERE bc.Barcode = @prBarCode AND p.Status = 1; ";
        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.Model = await conn.QueryFirstOrDefaultAsync<SalesInvoiceProductModel>(query, new
            {
                prBarCode = barCode
            });
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceProductModel>(this);
        }

        return result;
    }

    public async Task<ListResult<SalesInvoiceProductModel>> GetBarcodeWiseList(long productId)
    {
        var result = new ListResult<SalesInvoiceProductModel>();
        const string query = @"SELECT
	                        p.PID AS ProductId, BC.BarCode, P.PName AS ProductName, P.PShortName AS ProductShortName, ISNULL(CAST (stock.Qty AS DECIMAL(18,6)), 0) AS BalanceQty,
	                        PU.UnitCode , CAST(P.PBuyRate AS DECIMAL(18,4)) AS BuyRate, bc.SalesRate, bc.MRP AS MrpRate, bc.Trade AS TradeRate, bc.Wholesale AS WholesaleRate,
							bc.Retail AS RetailRate, bc.Dealer AS DealerRate, bc.Resellar AS ResellerRate, ISNULL(PG.GrpName, 'No Group') AS GroupName,
	                        ISNULL(PSG.SubGrpName, 'No SubGroup') AS SubGroupName, p.PUnit AS UnitId, p.PAltUnit AS AltUnitId, au.UnitCode AS AltUnitCode,
                            p.PQtyConv AS QtyConversionRate, p.PAltConv AS AltQtyConversionRate, p.PTax AS TaxPercent
                        FROM AMS.BarcodeList bc
						JOIN AMS.Product p ON p.PID = bc.ProductId
                        LEFT JOIN
	                        (
		                        SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN  sd.Qty WHEN sd.EntryType='O' THEN -sd.StockQty ELSE 0 END ) AS Qty
								FROM AMS.StockDetails sd WHERE SD.Voucher_Date <= CONVERT(DATE, GETDATE())
		                        GROUP BY sd.Product_Id
	                        ) AS stock ON stock.Product_Id = p.PID
                        LEFT JOIN AMS.ProductUnit as PU on P.PUnit = PU.UID
                        LEFT JOIN AMS.ProductGroup as PG on PG.PGrpID = p.PGrpId
                        LEFT JOIN AMS.ProductSubGroup as PSG on PSG.PSubGrpID = p.PSubGrpId
                        LEFT JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
                        WHERE bc.ProductId = @prProductId AND p.Status = 1 ";
        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<SalesInvoiceProductModel>(query, new { prProductId = productId });
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SalesInvoiceProductModel>(this);
        }

        return result;
    }

    public async Task<ListResult<SalesPartyLedgerModel>> GetPartyLedgersAsync()
    {
        var result = new ListResult<SalesPartyLedgerModel>();
        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<SalesPartyLedgerModel>(
                @"SELECT * FROM (
	                        SELECT 0 LedgerId, bill.Party_Name AS PartyName, bill.Vat_No AS VatNo,bill.Contact_Person AS ContactPerson, bill.Mobile_No AS MobileNo, bill.Address
	                        FROM AMS.SB_Master bill WHERE bill.Party_Name <>'' AND bill.Party_Name IS NOT NULL
                        GROUP BY bill.Party_Name, bill.Vat_No, bill.Contact_Person, bill.Mobile_No, bill.Address
                        UNION ALL
                        SELECT gl.GLID LedgerId, gl.GLName,gl.PanNo Vat_No,gl.OwnerName Contact_Person,gl.PhoneNo Mobile_No,gl.GLAddress Address FROM AMS.GeneralLedger gl WHERE gl.GLType='Customer'
                        ) AS Ledger ORDER BY Ledger.PartyName ");

            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SalesPartyLedgerModel>(this);
        }

        return result;
    }

    public async Task<ListResult<SalesGeneralLedgerModel>> GetGeneralLedgersAsync(SalesInvoicePaymentMode paymentMode)
    {
        var result = new ListResult<SalesGeneralLedgerModel>();

        var query = new StringBuilder(
            @"SELECT
                gl.GLID AS LedgerId, gl.GLName AS Particular, gl.GLCode AS ShortName, gl.ACCode AS LedgerCode, gl.PhoneNo,
                gl.GLAddress AS Address, gl.PanNo, gl.GLType, AG.GrpType, ag.PrimaryGrp, ag.GrpName AS GroupDesc,
                ASG.SubGrpName AS SubGroupDesc, gl.CrDays, gl.CrLimit, gl.CrType
                FROM AMS.GeneralLedger gl
                LEFT JOIN AMS.AccountGroup AS AG ON gl.GrpId = AG.GrpId
                LEFT JOIN AMS.AccountSubGroup AS ASG ON gl.SubGrpId = ASG.SubGrpId
                WHERE ISNULL(gl.Status, 1) = 1 ");

        switch (paymentMode)
        {
            case SalesInvoicePaymentMode.Credit:
                query.AppendLine(@"AND gl.GLType IN ('Customer', 'Both', 'Vendor') ");
                break;

            case SalesInvoicePaymentMode.Cash:
                query.AppendLine(@"AND gl.GLType = 'Cash' ");
                break;

            case SalesInvoicePaymentMode.Card:
                query.AppendLine(@"AND gl.GLType = 'Bank' ");
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(paymentMode), paymentMode, null);
        }

        query.AppendLine(@"ORDER BY gl.GLName ");

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<SalesGeneralLedgerModel>(query.ToString());

            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SalesGeneralLedgerModel>(this);
        }

        return result;
    }

    public async Task<InfoResult<SalesGeneralLedgerModel>> GetCashLedgerAsync(string username)
    {
        var result = new InfoResult<SalesGeneralLedgerModel>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var model = await conn.QueryFirstOrDefaultAsync<SalesGeneralLedgerModel>(
                @"SELECT
                        gl.GLID AS LedgerId, gl.GLName AS Particular, gl.GLCode AS ShortName, gl.ACCode AS LedgerCode, gl.PhoneNo,
                        gl.GLAddress AS Address, gl.PanNo, gl.GLType, AG.GrpType, ag.PrimaryGrp, ag.GrpName AS GroupDesc,
                        ASG.SubGrpName AS SubGroupDesc, gl.CrDays, gl.CrLimit, gl.CrType
                        FROM AMS.GeneralLedger gl
                        LEFT JOIN AMS.AccountGroup AS AG ON gl.GrpId = AG.GrpId
                        LEFT JOIN AMS.AccountSubGroup AS ASG ON gl.SubGrpId = ASG.SubGrpId
                        WHERE GlId = (SELECT Ledger_Id from Master.AMS.UserInfo where User_Name= @prUsername ) AND ISNULL(gl.Status, 1) = 1 ",
                new
                {
                    prUsername = username
                });

            if (model == null)
            {
                model = await conn.QueryFirstOrDefaultAsync<SalesGeneralLedgerModel>(
                    @"SELECT
                        gl.GLID AS LedgerId, gl.GLName AS Particular, gl.GLCode AS ShortName, gl.ACCode AS LedgerCode, gl.PhoneNo,
                        gl.GLAddress AS Address, gl.PanNo, gl.GLType, AG.GrpType, ag.PrimaryGrp, ag.GrpName AS GroupDesc,
                        ASG.SubGrpName AS SubGroupDesc, gl.CrDays, gl.CrLimit, gl.CrType
                        FROM AMS.GeneralLedger gl
                        LEFT JOIN AMS.AccountGroup AS AG ON gl.GrpId = AG.GrpId
                        LEFT JOIN AMS.AccountSubGroup AS ASG ON gl.SubGrpId = ASG.SubGrpId
                        WHERE gl.GLID  = (SELECT Cash_AC FROM ams.SystemConfiguration) AND ISNULL(gl.Status, 1) = 1 ");
            }

            result.Model = model;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesGeneralLedgerModel>(this);
        }

        return result;
    }

    public async Task<ListResult<SB_Master>> GetInvoicesForReverseAsync(int? counterId, int branchId)
    {
        var result = new ListResult<SB_Master>();

        var parameters = new DynamicParameters();
        parameters.Add("prDate", DateTime.Today);
        parameters.Add("prBranchId", branchId);

        var query = new StringBuilder(
            @"SELECT * FROM AMS.SB_Master WHERE CONVERT(DATE, Enter_Date) = @prDate AND CBranch_Id = @prBranchId AND Action_Type = 'NEW' ");
        if (counterId.HasValue)
        {
            query.AppendLine("AND CounterId = @prCounterId ");
            parameters.Add("prCounterId", counterId.Value);
        }

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<SB_Master>(query.ToString(), parameters);
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SB_Master>(this);
        }

        return result;
    }

    public async Task<ListResult<SB_Master>> GetInvoicesAsync(DateTime? dateFrom, DateTime? dateTo, int? counterId, int branchId, SalesInvoiceActionTag? actionTag)
    {
        var result = new ListResult<SB_Master>();

        var parameters = new DynamicParameters();
        parameters.Add("prDate", DateTime.Today);
        parameters.Add("prBranchId", branchId);

        var query = new StringBuilder(@"SELECT * FROM AMS.SB_Master WHERE CBranch_Id = @prBranchId ");
        if (dateFrom.HasValue)
        {
            query.AppendLine("AND CONVERT(DATE, Enter_Date) >= @prDate ");
        }

        if (dateTo.HasValue)
        {
            query.AppendLine("AND CONVERT(DATE, Enter_Date) <= @prDate ");
        }

        if (counterId.HasValue)
        {
            query.AppendLine("AND CounterId = @prCounterId ");
            parameters.Add("prCounterId", counterId.Value);
        }

        if (actionTag.HasValue)
        {
            query.AppendLine($@"AND Action_Type = '{actionTag.Value.GetDescription()}' ");
        }

        query.AppendLine("ORDER BY SB_Invoice ");

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<SB_Master>(query.ToString(), parameters);
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<SB_Master>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> ReverseInvoiceAsync(string invoiceNo, string username, string reason, DateTime dateTime)
    {
        var result = new NonQueryResult();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            await conn.OpenAsync();
            using var trans = conn.BeginTransaction();

            var invoice = await conn.QuerySingleOrDefaultAsync<SB_Master>(
                @"SELECT * FROM AMS.SB_Master WHERE SB_Invoice = @prInvoiceNo AND Action_Type = 'NEW' AND R_Invoice = 0 ",
                new { prInvoiceNo = invoiceNo }, trans);

            if (invoice == null)
            {
                result.ErrorMessage = "The requested invoice is not valid for return.";
                result.ResultType = ResultType.EntityNotExists;
                return result;
            }

            await conn.ExecuteAsync(
                @"UPDATE AMS.SB_Master SET Action_Type = 'CANCEL', R_Invoice = 1, SyncRowVersion = ISNULL(SyncRowVersion, 0) + 1, SyncLastPatchedOn = @prDateTime,
                                Cancel_By = @prUser, Cancel_Date = @prDateTime, Cancel_Remarks = @prReason WHERE SB_Invoice = @prInvoiceNo ",
                new
                {
                    prInvoiceNo = invoiceNo,
                    prDateTime = dateTime,
                    prUser = username,
                    prReason = reason
                }, trans);

            var newData = await conn.QueryFirstOrDefaultAsync<SB_Master>(
                @"SELECT * FROM AMS.SB_Master WHERE SB_Invoice = @prInvoiceNo ", new { prInvoiceNo = invoiceNo },
                trans);
            if (newData == null)
            {
                result.ErrorMessage = "Error fetching selected invoice detail. ";
                return result;
            }

            // reverse account and stock posting
            await conn.ExecuteAsync(
                @"DELETE FROM AMS.AccountDetails WHERE Voucher_No = @prVoucherNo AND Module = 'SB' ",
                new { prVoucherNo = invoiceNo }, trans);
            await conn.ExecuteAsync(
                @"DELETE FROM AMS.StockDetails WHERE Voucher_No = @prVoucherNo AND Module = 'SB' ",
                new { prVoucherNo = invoiceNo }, trans);

            trans.Commit();
            result.Completed = result.Value = true;

            AppLoggerService.LogAsync(AppLogContext.UserActivityWithAudit,
                $"Invoice {invoiceNo} reversed with related account and stock postings.",
                AppLogType.SalesInvoice, AppLogActionType.Reverse, AppLogRefType.SalesMaster, invoiceNo,
                invoiceNo, invoice, newData, ObjGlobal.SysBranchId);
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public async Task<InfoResult<SalesInvoiceViewModel>> GetInvoiceAsync(string invoiceNo, string actionTag)
    {
        var result = new InfoResult<SalesInvoiceViewModel>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var master = await conn.QueryFirstOrDefaultAsync<SB_Master>(
                @"SELECT * FROM AMS.SB_Master WHERE SB_Invoice = @prInvoiceNo AND Action_Type = @prActionTag ",
                new { prInvoiceNo = invoiceNo, prActionTag = actionTag });
            if (master != null)
            {
                var items = await conn.QueryAsync<SB_Details>(
                    @"SELECT * FROM AMS.SB_Details WHERE SB_Invoice = @prInvoiceNo ",
                    new { prInvoiceNo = invoiceNo });
                var terms = await conn.QueryAsync<SB_Term>(
                    @"SELECT * FROM AMS.SB_Term WHERE SB_VNo = @prInvoiceNo ", new { prInvoiceNo = invoiceNo });

                result.Model = new SalesInvoiceViewModel
                {
                    Master = master,
                    Items = items.AsList(),
                    Terms = terms.AsList()
                };
                result.Success = true;
            }

            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceViewModel>(this);
        }

        return result;
    }

    public async Task<InfoResult<AbbrInvoiceForPrintModel>> GetInvoiceForPrintAsync(string invoiceNo)
    {
        var result = new InfoResult<AbbrInvoiceForPrintModel>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var invoice = await conn.QueryFirstOrDefaultAsync<AbbrInvoiceForPrintModel>(
                @"SELECT
	                        bill.SB_Invoice AS BillNo, bill.Invoice_Date AS BillDateTime, ISNULL(bill.Party_Name,'CASH') AS PartyName,
	                        bill.Address AS PartyAddress, bill.Vat_No AS PartyPan, bill.Mobile_No AS PartyContact, 0 AS SibTotal, 0 AS Discount, 0 AS GrandTotal, bill.Payment_Mode AS PaymentMode,
	                        bill.In_Words AS AmountInWords
                        FROM ams.SB_Master bill
                        WHERE bill.SB_Invoice = @prInvoiceNo ", new { prInvoiceNo = invoiceNo });

            if (invoice != null)
            {
                var items = await conn.QueryAsync<SalesInvoiceItemModel>(
                    @"SELECT
	                        det.Invoice_SNo AS SNo, prod.PShortName AS ProductShortName, prod.PName AS ProductName,
                            unit.UnitCode AS  UnitName, aunit.UnitCode AS AltUnitName, det.Unit_Id AS UnitId, det.Alt_UnitId AS AltUnitId,
	                        det.Rate, det.P_Id AS ProductId, det.Qty, det.Free_Qty AS FreeQty, det.Stock_Qty AS StockQty,
                            det.Alt_Qty AS AltQty, det.PDiscount AS ItemDis, det.BDiscount AS BillDis, det.V_Rate AS TaxPercent
                        FROM AMS.SB_Details det
                        JOIN AMS.Product prod ON prod.PID = det.P_Id
                        LEFT JOIN AMS.ProductUnit unit ON unit.UID = prod.PUnit
                        LEFT JOIN AMS.ProductUnit aunit ON aunit.UID = det.Alt_UnitId
                        WHERE det.SB_Invoice = @prInvoiceId ", new
                    {
                        prInvoiceId = invoiceNo
                    });

                result.Model = invoice;
                result.Model.Items = items.AsList();
                result.Success = true;
            }
            else
            {
                result.ErrorMessage = "The requested sales invoice doesn't exist.";
            }
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AbbrInvoiceForPrintModel>(this);
        }

        return result;
    }
}