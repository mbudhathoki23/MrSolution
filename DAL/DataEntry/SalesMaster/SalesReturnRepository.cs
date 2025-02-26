using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.SalesReturn;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Dialogs;
using MrDAL.Master;
using MrDAL.Models.Common;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.SalesMaster;

public class SalesReturnRepository : ISalesReturn
{
    // SALES RETURN INSERT UPDATE DELETE
    public int SaveSalesReturn(string actionTag)
    {
        try
        {
            var invoiceNo = SrMaster.SR_Invoice;
            var cmdString = new StringBuilder();
            string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
            if (strNamesArray.Any(x => x == actionTag))
            {
                if (!actionTag.Equals("UPDATE"))
                {
                    AuditLogSalesReturn(actionTag);
                }

                if (!actionTag.Equals("REVERSE"))
                {
                    cmdString.Append($"Delete from AMS.SR_Term where SR_VNo ='{SrMaster.SR_Invoice}'; \n");
                    cmdString.Append($"Delete from AMS.SR_Details where SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
                }

                if (actionTag.Equals("REVERSE"))
                {
                    cmdString.Append(
                        $"UPDATE AMS.SR_Master SET Action_Type='CANCEL',R_Invoice =1 WHERE SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
                }

                cmdString.Append(
                    $"Delete from AMS.AccountDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}'; \n");
                cmdString.Append(
                    $"Delete from AMS.StockDetails Where module='SR' and Voucher_No ='{SrMaster.SR_Invoice}'; \n");

                if (actionTag.Equals("DELETE"))
                {
                    cmdString.Append(
                        $"Delete from AMS.SR_Master_OtherDetails where SR_Invoice = '{SrMaster.SR_Invoice}'; \n");
                    cmdString.Append($"Delete from AMS.SR_Master where SR_Invoice ='{SrMaster.SR_Invoice}'; \n");
                }
            }

            if (actionTag.Equals("SAVE"))
            {
                if (SrMaster.SB_Invoice.IsValueExits())
                {
                    cmdString.Append(
                        $" UPDATE AMS.SB_Master SET Action_Type ='RETURN' WHERE SB_Invoice='{SrMaster.SB_Invoice}' \n");
                }

                cmdString.Append(@" 
                    INSERT INTO AMS.SR_Master(SR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, SB_Invoice, SB_Date, SB_Miti, Customer_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, Cancel_By, Cancel_Date, Cancel_Remarks, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, IsAPIPosted, IsRealtime, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append(" \n VALUES \n");
                cmdString.Append($" (N'{SrMaster.SR_Invoice}', '{SrMaster.Invoice_Date.GetSystemDate()}', N'{SrMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(SrMaster.SB_Invoice.IsValueExits() ? $" N'{SrMaster.SB_Invoice}', '{SrMaster.SB_Date.GetSystemDate()}','{SrMaster.SB_Miti}'," : "NULL,NULL,NULL,");
                cmdString.Append($" {SrMaster.Customer_ID},");
                cmdString.Append(SrMaster.PartyLedgerId > 0 ? $" {SrMaster.PartyLedgerId}," : "NULL,");
                cmdString.Append(SrMaster.Party_Name.IsValueExits() ? $" N'{SrMaster.Party_Name}'," : "NULL,");
                cmdString.Append(SrMaster.Vat_No.IsValueExits() ? $" N'{SrMaster.Vat_No}'," : "NULL,");
                cmdString.Append(SrMaster.Contact_Person.IsValueExits() ? $" N'{SrMaster.Contact_Person}'," : "NULL,");
                cmdString.Append(SrMaster.Mobile_No.IsValueExits() ? $" N'{SrMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(SrMaster.Address.IsValueExits() ? $" N'{SrMaster.Address}'," : "NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqNo}'," : "NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqDate.GetSystemDate()}'," : "NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" N'{SrMaster.ChqMiti}'," : "NULL,");
                cmdString.Append($" N'{SrMaster.Invoice_Type}', N'{SrMaster.Invoice_Mode}', '{SrMaster.Payment_Mode}', {SrMaster.DueDays},");
                cmdString.Append(SrMaster.DueDays > 0 ? $" '{SrMaster.DueDate.GetSystemDate()}'," : "NULL,");
                cmdString.Append(SrMaster.Agent_Id > 0 ? $" {SrMaster.Agent_Id}," : "NULL,");
                cmdString.Append(SrMaster.Subledger_Id > 0 ? $" {SrMaster.Subledger_Id}," : "NULL,");
                cmdString.Append(SrMaster.Cls1 > 0 ? $" {SrMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(SrMaster.CounterId > 0 ? $" {SrMaster.CounterId}," : "NULL,");
                cmdString.Append(SrMaster.Cur_Id > 0 ? $" {SrMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
                cmdString.Append($"{SrMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($"{SrMaster.B_Amount.GetDecimal()},{SrMaster.T_Amount.GetDecimal()}, {SrMaster.N_Amount.GetDecimal()},{SrMaster.LN_Amount.GetDecimal()},{SrMaster.V_Amount.GetDecimal()},{SrMaster.Tbl_Amount.GetDecimal()},{SrMaster.Tender_Amount.GetDecimal()},{SrMaster.Return_Amount.GetDecimal()},'{SrMaster.Action_Type}',");
                cmdString.Append(SrMaster.In_Words.IsValueExits() ? $" N'{SrMaster.In_Words}'," : "NULL,");
                cmdString.Append(SrMaster.Remarks.IsValueExits() ? $" N'{SrMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
                cmdString.Append($"0,0,NULL,NULL,NULL,0,NULL,NULL,0,'{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,0,0,{ObjGlobal.SysBranchId},");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
                cmdString.Append($"NUll,NULL,NULL,NULL,NULL,{SrMaster.SyncRowVersion.GetDecimal(true)}); \n");
            }

            if (actionTag.Equals("UPDATE"))
            {
                cmdString.Append(" UPDATE AMS.SR_Master SET	 \n");
                cmdString.Append(
                    $" Invoice_Date='{SrMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti= N'{SrMaster.Invoice_Miti}',");
                cmdString.Append(SrMaster.SB_Invoice.IsValueExits()
                    ? $"SB_Invoice= N'{SrMaster.SB_Invoice}',SB_Date= '{SrMaster.SB_Date:yyyy-MM-dd}',SB_Miti='{SrMaster.SB_Miti}',"
                    : "SB_Invoice=NULL,SB_Date=NULL,SB_Miti=NULL,");
                cmdString.Append($" Customer_ID= {SrMaster.Customer_ID},");
                cmdString.Append(SrMaster.PartyLedgerId > 0
                    ? $" PartyLedgerId= {SrMaster.PartyLedgerId},"
                    : "PartyLedgerId= NULL,");
                cmdString.Append(SrMaster.Party_Name.IsValueExits()
                    ? $" Party_Name= N'{SrMaster.Party_Name}',"
                    : "Party_Name = NULL,");
                cmdString.Append(SrMaster.Vat_No.IsValueExits()
                    ? $" Vat_No = N'{SrMaster.Vat_No}',"
                    : "Vat_No = NULL,");
                cmdString.Append(SrMaster.Contact_Person.IsValueExits()
                    ? $" Contact_Person = N'{SrMaster.Contact_Person}',"
                    : "Contact_Person = NULL,");
                cmdString.Append(SrMaster.Mobile_No.IsValueExits()
                    ? $" Mobile_No = N'{SrMaster.Mobile_No}',"
                    : "Mobile_No = NULL,");
                cmdString.Append(SrMaster.Address.IsValueExits()
                    ? $" Address = N'{SrMaster.Address}',"
                    : "Address = NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SrMaster.ChqNo}'," : "ChqNo = NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits()
                    ? $" ChqDate = N'{SrMaster.ChqDate:yyyy-MM-dd}',"
                    : "ChqDate = NULL,");
                cmdString.Append(SrMaster.ChqNo.IsValueExits()
                    ? $" ChqMiti = N'{SrMaster.ChqMiti}',"
                    : "ChqMiti = NULL,");
                cmdString.Append(
                    $" Invoice_Type = N'{SrMaster.Invoice_Type}', Invoice_Mode = N'{SrMaster.Invoice_Mode}',Payment_Mode =  '{SrMaster.Payment_Mode}',DueDays =  {SrMaster.DueDays},");
                cmdString.Append(SrMaster.DueDays > 0
                    ? $" DueDate= '{SrMaster.DueDate:yyyy-MM-dd}',"
                    : "DueDate = NULL,");
                cmdString.Append(SrMaster.Agent_Id > 0 ? $"Agent_Id =  {SrMaster.Agent_Id}," : "Agent_Id = NULL,");
                cmdString.Append(SrMaster.Subledger_Id > 0
                    ? $" Subledger_Id = {SrMaster.Subledger_Id},"
                    : "Subledger_Id = NULL,");
                cmdString.Append(SrMaster.Cls1 > 0 ? $" Cls1 = {SrMaster.Cls1}," : "Cls1 = NULL,");
                cmdString.Append(SrMaster.CounterId > 0 ? $"CounterId =  {SrMaster.CounterId}," : "CounterId = NULL,");
                cmdString.Append(SrMaster.Cur_Id > 0
                    ? $" Cur_Id = {SrMaster.Cur_Id},"
                    : $"Cur_Id = {ObjGlobal.SysCurrencyId},");
                cmdString.Append($"Cur_Rate = {SrMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append(
                    $"B_Amount = {SrMaster.B_Amount.GetDecimal()},T_Amount = {SrMaster.T_Amount.GetDecimal()},N_Amount =  {SrMaster.N_Amount.GetDecimal()},LN_Amount = {SrMaster.LN_Amount.GetDecimal()},V_Amount = {SrMaster.V_Amount.GetDecimal()},Tbl_Amount = {SrMaster.Tbl_Amount.GetDecimal()},Tender_Amount = {SrMaster.Tender_Amount.GetDecimal()},Return_Amount = {SrMaster.Return_Amount.GetDecimal()},Action_Type = '{SrMaster.Action_Type}',");
                cmdString.Append(SrMaster.In_Words.IsValueExits()
                    ? $" In_Words = N'{SrMaster.In_Words}',"
                    : "In_Words = NULL,");
                cmdString.Append(SrMaster.Remarks.IsValueExits()
                    ? $" Remarks = N'{SrMaster.Remarks.Trim().Replace("'", "''")}',"
                    : "Remarks = NULL,");
                cmdString.Append("IsSynced=0,");
                cmdString.Append($"SyncRowVersion = {SrMaster.SyncRowVersion.GetDecimal(true)} WHERE SR_Invoice = '{SrMaster.SR_Invoice}'; \n");
            }

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (DetailsList.Count > 0)
                {
                    cmdString.Append(@" 
                        INSERT INTO AMS.SR_Details(SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                    cmdString.Append(" \n VALUES \n");

                    foreach (var item in DetailsList)
                    {
                        var index = DetailsList.IndexOf(item);
                        cmdString.Append($"('{item.SR_Invoice}', {item.Invoice_SNo}, {item.P_Id},");
                        cmdString.Append(item.Gdn_Id > 0 ? $"{item.Gdn_Id}," : "NULL,");
                        cmdString.Append(item.Alt_Qty > 0 ? $"{item.Alt_Qty}," : "0,");
                        cmdString.Append(item.Alt_UnitId > 0 ? $"{item.Alt_UnitId}," : "NULL,");
                        cmdString.Append($" {item.Qty},");
                        cmdString.Append(item.Unit_Id > 0 ? $"{item.Unit_Id}," : "NULL,");
                        cmdString.Append($"{item.Rate}, {item.B_Amount}, {item.T_Amount}, {item.N_Amount}, {item.AltStock_Qty}, {item.Stock_Qty}, ");
                        cmdString.Append($" N'{item.Narration}', '{item.SB_Invoice}', {item.SB_Sno}, {item.Tax_Amount},");
                        cmdString.Append($" {item.V_Amount}, {item.V_Rate}, {item.Free_Unit_Id}, {item.Free_Qty}, {item.StockFree_Qty}, {item.ExtraFree_Unit_Id}, {item.ExtraFree_Qty},");
                        cmdString.Append($" {item.ExtraStockFree_Qty}, CAST('{item.T_Product}' AS BIT),");
                        cmdString.Append(item.S_Ledger > 0 ? $"{item.S_Ledger}," : "NULL,");
                        cmdString.Append(item.SR_Ledger > 0 ? $"{item.SR_Ledger}," : "NULL,");
                        cmdString.Append($" NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                        cmdString.Append($" '{item.Serial_No}', N'{item.Batch_No}', '{item.Exp_Date.GetSystemDate()}', '{item.Manu_Date.GetSystemDate()}', {item.PDiscountRate}, {item.PDiscount}, {item.BDiscountRate}, {item.BDiscount}, {item.ServiceChargeRate}, {item.ServiceCharge},");
                        cmdString.Append(item.SyncBaseId.IsGuidExits() ? $"'{item.SyncBaseId}'," : "NULL,");
                        cmdString.Append(item.SyncGlobalId.IsGuidExits() ? $"'{item.SyncGlobalId}'," : "NULL,");
                        cmdString.Append(item.SyncOriginId.IsGuidExits() ? $"'{item.SyncOriginId}'," : "NULL,");
                        cmdString.Append($" '{item.SyncCreatedOn.GetSystemDate()}','{item.SyncLastPatchedOn.GetSystemDate()}',");
                        cmdString.Append($" {item.SyncRowVersion}");
                        cmdString.Append(index == DetailsList.Count - 1 ? ");\n" : "),\n");
                    }

                    if (Terms.Count > 0)
                    {
                        if (actionTag == "UPDATE")
                        {
                            cmdString.Append($" Delete from AMS.SR_Term where SR_VNo ='{SrMaster.SR_Invoice}'; \n");
                        }

                        cmdString.Append(@" 
                            INSERT INTO AMS.SR_Term(SR_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                        cmdString.Append($" \n VALUES \n");
                        foreach (var item in Terms)
                        {
                            var index = Terms.IndexOf(item);
                            cmdString.Append($"('{item.SR_VNo}', {item.ST_Id}, {item.SNo}, '{item.Term_Type}',");
                            cmdString.Append(item.Product_Id > 0 ? $"{item.Product_Id}," : "Null,");
                            cmdString.Append($"{item.Rate}, {item.Amount}, '{item.Taxable}',");
                            cmdString.Append(item.SyncBaseId.IsGuidExits() ? $"'{item.SyncBaseId}'," : "NULL,");
                            cmdString.Append(item.SyncGlobalId.IsGuidExits() ? $"'{item.SyncGlobalId}'," : "NULL,");
                            cmdString.Append(item.SyncOriginId.IsGuidExits() ? $"'{item.SyncOriginId}'," : "NULL,");
                            cmdString.Append($"'{item.SyncCreatedOn.GetSystemDate()}','{item.SyncLastPatchedOn.GetSystemDate()}',");
                            cmdString.Append($"{item.SyncRowVersion}");
                            cmdString.Append(index == Terms.Count - 1 ? ");\n" : "),\n");
                        }
                    }
                }
            }

            var iResult = SqlExtensions.ExecuteNonTrans(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }

            if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null)
            {
                Task.Run(() => SyncSalesReturnAsync(false, actionTag));
            }

            if (_tagStrings.Contains(actionTag))
            {
                return iResult;
            }

            _ = SalesReturnTermPosting();
            _ = SalesReturnAccountPosting();
            _ = SalesReturnStockPosting();

            if (SrMaster.SB_Invoice.IsValueExits())
            {
                UpdateInvoiceTagOnSalesReturn();
            }

            if (!ObjGlobal.IsIrdRegister)
            {
                return iResult;
            }

            PostingSalesReturnToApi(invoiceNo);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return 0;
        }
    }

    // STOCK POSTING
    public int SalesReturnStockPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.StockDetails WHERE Module = 'SR' ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" AND Voucher_No = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM (SELECT 'SR' Module, sd.SR_Invoice Voucher_No, sd.Invoice_SNo Serial_No, sm.SB_Invoice PurRefVno, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_Id, sm.Subledger_Id Subledger_Id, sm.Agent_Id Agent_Id, sm.Cls1 Department_Id1, sm.Cls2 Department_Id2, sm.Cls3 Department_Id3, sm.Cls4 Department_Id4, sm.Cur_Id Currency_Id, sm.Cur_Rate Currency_Rate, sd.P_Id Product_Id, sd.Gdn_Id Godown_Id, NULL CostCenter_Id, sd.Alt_Qty AltQty, sd.Alt_UnitId AltUnit_Id, sd.Qty Qty, sd.Unit_Id Unit_Id, sd.AltStock_Qty AltStockQty, sd.Stock_Qty StockQty, sd.Free_Qty FreeQty, sd.Free_Unit_Id FreeUnit_Id, sd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, sd.Rate Rate, sd.B_Amount BasicAmt, sd.T_Amount TermAmt, sd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, sd.V_Rate TaxRate, sd.Tax_Amount TaxableAmt, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, sm.SB_Invoice PartyInv, 'I' EntryType, sm.Auth_By AuthBy, sm.Auth_Date AuthDate, sm.Reconcile_By RecoBy, sm.Reconcile_Date RecoDate, sm.CounterId Counter_Id, NULL RoomNo, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.CBranch_Id Branch_Id, sm.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, sd.Rate SalesRate, FiscalYearId, sm.SyncGlobalId, sm.SyncOriginId, sm.SyncCreatedOn, sm.SyncLastPatchedOn, sm.SyncRowVersion, sm.SyncBaseId
				FROM AMS.SR_Details sd
				INNER JOIN AMS.Product p ON p.PID = sd.P_Id
				INNER JOIN AMS.SR_Master sm ON sd.SR_Invoice = sm.SR_Invoice
				LEFT OUTER JOIN (SELECT st.Product_Id, st.SR_VNo, st.SNo, SUM(CASE WHEN st1.ST_Sign = '-' THEN -st.Amount ELSE st.Amount END) StockValue
					FROM AMS.SR_Term st
					LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
					WHERE st1.ST_Profitability > 0 AND st.Term_Type <> 'B'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" AND st.SR_VNo = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
					GROUP BY st.Product_Id, st.SR_VNo, st.SNo
				) stockval ON stockval.Product_Id = sd.P_Id AND sd.SR_Invoice = stockval.SR_VNo AND sd.Invoice_SNo = stockval.SNo
				WHERE p.PType IN ('I','Inventory') AND sm.R_Invoice=0  AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" AND sd.SR_Invoice = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
				) AS Stock ");
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString());
    }

    public async Task<int> UpdateSalesStockValue()
    {
        var result = 0;
        var cmdString = $@"
        SELECT sm.Invoice_Date,sd.SR_Invoice ,sd.P_Id,sm.FiscalYearId FROM AMS.SR_Details sd 
            LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice = sd.SR_Invoice 
        WHERE sm.FiscalYearId = '{ObjGlobal.SysFiscalYearId}'
        ORDER BY sd.P_Id ";
        var product = await SqlExtensions.GetDataTableAsync(cmdString);
        if (product == null || product.RowsCount() <= 0)
        {
            return result;
        }

        foreach (DataRow row in product.Rows)
        {
            var productId = row["P_Id"].GetLong();
            var voucherNo = row["SR_Invoice"].GetString();
            var voucherDate = row["Invoice_Date"].GetSystemDate();
            cmdString = $@"
            SELECT TOP(1)(StockVal / StockQty) Rate 
                FROM AMS.StockDetails 
            WHERE Product_Id={productId} AND EntryType = 'O' AND Voucher_Date <='{voucherDate.GetSystemDate()}' AND FiscalYearId= {ObjGlobal.SysFiscalYearId} 
            ORDER BY Voucher_Date DESC ";
            var value = cmdString.GetQueryData().GetDecimal();
            if (value <= 0)
            {
                continue;
            }
            cmdString = $@"
            UPDATE AMS.StockDetails
            SET StockVal = StockQty * IsNULL({value},0) WHERE Product_Id={productId} AND Module='SR' AND Voucher_No = '{voucherNo}';";
            await SqlExtensions.ExecuteNonQueryAsync(cmdString);
        }

        return result;
    }


    // TERM POSTING
    public int SalesReturnTermPosting()
    {
        var cmdString = @"
		DELETE AMS.SR_Term WHERE Term_Type='BT' ";
        cmdString += SrMaster.SR_Invoice.IsValueExits() ? $@" AND SR_VNo ='{SrMaster.SR_Invoice}'; " : " ";
        cmdString += $@"
		    INSERT INTO AMS.SR_Term(SR_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
		    SELECT sbt.SR_VNo, sbt.ST_Id, sd.Invoice_SNo AS SERIAL_NO,CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = {ObjGlobal.SalesVatTermId.GetInt()} THEN 0 ELSE sbt.Rate END Rate,
            CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0  AND sbt.ST_Id = {ObjGlobal.SalesVatTermId.GetInt()} THEN 0 WHEN ISNULL(sm.B_Amount,0) >  0 then (sbt.Amount/sm.B_Amount) * sd.N_Amount ELSE 0 END Amount, 'BT' Term_Type, sd.P_Id Product_Id, sbt.Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion, sbt.SyncBaseId
		    FROM AMS.SR_Details sd
			     LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=sd.SR_Invoice
			     LEFT OUTER JOIN AMS.SR_Term sbt ON sd.SR_Invoice=sbt.SR_VNo
                 LEFT OUTER JOIN AMS.Product P ON P.PID = sd.P_Id
		    WHERE sbt.Term_Type='B' AND Product_Id IS NULL ";
        cmdString += SrMaster.SR_Invoice.IsValueExits() ? $@" AND sbt.SR_VNo='{SrMaster.SR_Invoice}' " : "";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    // ACCOUNTING POSTING
    public int SalesReturnAccountPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@" DELETE AMS.AccountDetails WHERE module='SR'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? $@" and Voucher_No='{SrMaster.SR_Invoice}' " : string.Empty);
        cmdString.Append($@"
		    INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_Id, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_Id, CmpUnit_Id, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
		    SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_Id, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_Id, CmpUnit_Id, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
		    FROM (SELECT 'SR' Module, 1 Serial_No, sm.SR_Invoice Voucher_No, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_ID, {ObjGlobal.SalesReturnLedgerId} CbLedger_ID, sm.Subledger_Id Subleder_ID, sm.Agent_ID Agent_ID, sm.Cls1 Department_ID1, sm.Cls2 Department_ID2, sm.Cls3 Department_ID3, sm.Cls4 Department_ID4, sm.Cur_Id Currency_ID, sm.Cur_Rate Currency_Rate, 0 Debit_Amt, sm.N_Amount Credit_Amt, 0 LocalDebit_Amt, sm.LN_Amount LocalCredit_Amt, sm.DueDate DueDate, sm.DueDays DueDays, NULL Narration, sm.Remarks Remarks, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.SB_Invoice RefNo, sm.SB_Date RefDate, sm.Reconcile_By Reconcile_By, sm.Reconcile_Date Reconcile_Date, sm.Auth_By Authorize_By, sm.Auth_Date Authorize_Date, sm.Cleared_By Clearing_By, sm.Cleared_Date Clearing_Date, NULL Posted_By, NULL Posted_Date, sm.ChqNo Cheque_No, sm.ChqDate Cheque_Date, NULL Cheque_Miti, sm.Party_Name PartyName, NULL PartyLedger_Id, sm.Vat_No Party_PanNo, sm.CBranch_Id Branch_ID, sm.CUnit_Id CmpUnit_ID, sm.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, sm.SyncGlobalId SyncGlobalId, sm.SyncOriginId SyncOriginId, sm.SyncCreatedOn SyncCreatedOn, SyncLastPatchedOn, sm.SyncRowVersion SyncRowVersion, sm.SyncBaseId SyncBaseId
				    FROM AMS.SR_Master sm
				    WHERE sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? @$" AND SM.SR_Invoice ='{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
		    UNION ALL
		    SELECT 'SR' Module, sd.Invoice_SNo Serial_No, sd.SR_Invoice Voucher_No, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, CASE WHEN CAST(ISNULL(p.PSR, 0) AS BIGINT)>0 THEN p.PSL ELSE (SELECT SRLedgerId FROM AMS.SalesSetting) END Ledger_ID, sm.Customer_ID CbLedger_ID, sm.Subledger_Id Subleder_ID, sm.Agent_ID Agent_ID, sm.Cls1 Department_ID1, sm.Cls2 Department_ID2, sm.Cls3 Department_ID3, sm.Cls4 Department_ID4, sm.Cur_Id Currency_ID, sm.Cur_Rate Currency_Rate, SUM(sd.B_Amount) Debit_Amt, 0 Credit_Amt, SUM(sd.B_Amount)* sm.Cur_Rate LocalDebit_Amt, 0 LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.SB_Invoice RefNo, sm.SB_Date RefDate, sm.Reconcile_By Reconcile_By, sm.Reconcile_Date Reconcile_Date, sm.Auth_By Authorize_By, sm.Auth_Date Authorize_Date, sm.Cleared_By Clearing_By, sm.Cleared_Date Clearing_Date, NULL Posted_By, NULL Posted_Date, sm.ChqNo Cheque_No, sm.ChqDate Cheque_Date, NULL Cheque_Miti, sm.Party_Name PartyName, NULL PartyLedger_Id, sm.Vat_No Party_PanNo, sm.CBranch_Id Branch_ID, sm.CUnit_Id CmpUnit_ID, sm.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, sd.SyncGlobalId SyncGlobalId, sd.SyncOriginId SyncOriginId, sd.SyncCreatedOn SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion SyncRowVersion, sd.SyncBaseId SyncBaseId
			    FROM AMS.SR_Details sd
			    INNER JOIN AMS.SR_Master sm ON sd.SR_Invoice = sm.SR_Invoice
			    INNER JOIN AMS.Product p ON sd.P_Id = p.PID
			    WHERE sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL'  ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? @$" AND SD.SR_Invoice ='{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
			    GROUP BY sd.SR_Invoice, sd.Invoice_SNo, sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time,p.PSR,sm.Customer_Id, sm.Subledger_Id, sm.Agent_ID, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, Narration, Remarks, sm.Enter_By, sm.Enter_Date, sm.SB_Invoice, sm.SB_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId,sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, p.PSL
		    UNION ALL
		    SELECT 'SR' Module, st.SNo Serial_No, st.SR_VNo Voucher_No, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, st1.Ledger Ledger_ID, sm.Customer_Id CbLedger_ID, sm.Subledger_Id Subleder_ID, sm.Agent_ID Agent_ID, sm.Cls1 Department_ID1, sm.Cls2 Department_ID2, sm.Cls3 Department_ID3, sm.Cls4 Department_ID4, sm.Cur_Id Currency_ID, sm.Cur_Rate Currency_Rate, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END) Debit_Amt, SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END)  Credit_Amt, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END) *  sm.Cur_Rate LocalDebit_Amt,SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END) * sm.Cur_Rate  LocalCredit_Amt, DueDate, DueDays, NULL Narration, sm.Remarks, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.SB_Invoice RefNo, sm.SB_Date RefDate, sm.Reconcile_By Reconcile_By, sm.Reconcile_Date Reconcile_Date, sm.Auth_By Authorize_By, sm.Auth_Date Authorize_Date, sm.Cleared_By Clearing_By, sm.Cleared_Date Clearing_Date, NULL Posted_By, NULL Posted_Date, sm.ChqNo Cheque_No, sm.ChqDate Cheque_Date, NULL Cheque_Miti, sm.Party_Name PartyName, NULL PartyLedger_Id, sm.Vat_No Party_PanNo, sm.CBranch_Id Branch_ID, sm.CUnit_Id CmpUnit_ID, sm.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, st.SyncGlobalId SyncGlobalId, st.SyncOriginId SyncOriginId, st.SyncCreatedOn SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion SyncRowVersion, st.SyncBaseId SyncBaseId
			    FROM AMS.SR_Term st
			    LEFT OUTER JOIN AMS.SR_Master sm ON st.SR_VNo = sm.SR_Invoice
			    LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
		    WHERE st.Term_Type <> 'BT' AND sm.R_Invoice=0  AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(SrMaster.SR_Invoice.IsValueExits() ? @$" AND st.SR_VNo ='{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
		    GROUP BY st.SR_VNo, st.SNo, sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time, sm.Customer_Id, sm.Subledger_Id, sm.Agent_ID, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, st1.ST_Sign, Remarks, sm.Enter_By, sm.Enter_Date, sm.SB_Invoice, sm.SB_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId, st.SyncGlobalId, st.SyncOriginId, st.SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion, st.SyncBaseId, st1.Ledger) Account ");
        return SqlExtensions.ExecuteNonQuery(cmdString.ToString());
    }

    // IRD API POSTING
    private void PostingSalesReturnToApi(string voucherNo)
    {
        _ = Task.Run(() => _apiSync.SyncSalesReturnApi(voucherNo));
    }


    // RETURN VALUE IN INT 
    private int UpdateInvoiceTagOnSalesReturn()
    {
        var cmdString = $@" 
            UPDATE AMS.SB_Master SET Action_Type ='RETURN' WHERE SB_Invoice='{SrMaster.SB_Invoice}';";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }

    public async Task<int> SyncSalesReturnAsync(bool isPosReturn, string actionTag)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item1 == null)
        {
            return 1;
        }

        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}SalesReturn/GetSalesReturnById",
            InsertUrl = @$"{_configParams.Model.Item2}SalesReturn/InsertSalesReturn",
            UpdateUrl = @$"{_configParams.Model.Item2}SalesReturn/UpdateSalesReturn",
            DeleteUrl = @$"{_configParams.Model.Item2}SalesReturn/DeleteSalesReturnAsync?id=" + SrMaster.SR_Invoice + ""
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var salesNewRepo = DataSyncProviderFactory.GetRepository<SR_Master>(_injectData);
        // pull all new sales data
        if (ObjGlobal.IsSalesInvoiceSync)
        {
            var result = await PullUnSyncSalesReturn(salesNewRepo);
            if (!result)
            {
                return 0;
            }
        }
        // push all new sales data
        var sqlQuery = @"SELECT *FROM AMS.SR_Master WHERE ISNULL(IsSynced,0)=0";
        var queryResponse = await QueryUtils.GetListAsync<SR_Master>(sqlQuery);
        var saList = queryResponse.List.ToList();
        if (saList.Count > 0)
        {
            var loopCount = 1;
            if (saList.Count > 100)
            {
                loopCount = saList.Count / 100 + 1;
            }
            var newSaList = new List<SR_Master>();
            var cmdString = new StringBuilder();
            var salesList = new List<SR_Master>();
            for (var i = 0; i < loopCount; i++)
            {
                newSaList.Clear();
                newSaList.AddRange((IEnumerable<SR_Master>)(i == 0 ? saList.Take(100) : saList.Skip(100 * i).Take(100)));
                salesList.Clear();
                foreach (var sa in newSaList)
                {
                    sqlQuery = $@"SELECT *FROM AMS.SR_Details WHERE SR_Invoice='{sa.SB_Invoice}'";
                    var dtlQueryResponse = await QueryUtils.GetListAsync<SB_Details>(sqlQuery);
                    var sadList = dtlQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.SR_Term WHERE SR_Vno='{sa.SB_Invoice}'";
                    var pcTermQueryResponse = await QueryUtils.GetListAsync<SB_Term>(sqlQuery);
                    var saTermList = pcTermQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.ProductAddInfo WHERE VoucherNo='{sa.SB_Invoice}' AND Module='SR'";
                    var pAddInfoQueryResponse = await QueryUtils.GetListAsync<ProductAddInfo>(sqlQuery);
                    var prdAddInfoList = pAddInfoQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.SR_Master_OtherDetails WHERE SR_Invoice='{sa.SB_Invoice}'";
                    var saOtherDtlQuery = await QueryUtils.GetListAsync<SB_Master_OtherDetails>(sqlQuery);
                    var otherDetailsList = saOtherDtlQuery.List.ToList();

                    var sbMaster = new SR_Master();
                    sbMaster = sa;
                    sbMaster.DetailsList = DetailsList;
                    sbMaster.Terms = Terms;
                    salesList.Add(sbMaster);
                }
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await salesNewRepo.PushNewListAsync(salesList);
                if (!pushResponse.Value)
                {
                    SplashScreenManager.CloseForm();
                    pushResponse.ShowErrorDialog();
                    return 0;
                }
                else
                {
                    try
                    {
                        cmdString.Clear();
                        foreach (var sa in newSaList)
                        {
                            cmdString.Append($"UPDATE AMS.SR_Master SET IsSynced = 1 WHERE SR_Invoice = '{sa.SR_Invoice}'; \n");
                        }
                        var query = cmdString.ToString();
                        CreateDatabaseTable.DropTrigger();
                        await QueryUtils.ExecNonQueryAsync(query);
                        CreateDatabaseTable.CreateTrigger();
                    }
                    catch
                    {
                        CreateDatabaseTable.CreateTrigger();
                    }
                    SplashScreenManager.CloseForm();
                }
            }
        }

        return 1;


    }
    private async Task<bool> PullUnSyncSalesReturn(Domains.Shared.DataSync.Abstractions.IDataSyncRepository<SR_Master> salesNewRepo)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pullResponse = await salesNewRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    _dataEntry.SaveUnSyncSalesReturnFromServerAsync(data, "SAVE");
                }
                SplashScreenManager.CloseForm();
            }

            if (pullResponse.IsReCall)
            {
                await PullUnSyncSalesReturn(salesNewRepo);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    //AUDIT LOG OF SALES
    private int AuditLogSalesReturn(string actionTag)
    {
        var cmdString = @$"
		    INSERT INTO [AUD].[AUDIT_SR_Master] ([SR_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[SB_Invoice] ,[SB_Date] ,[SB_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Subledger_Id] ,[Agent_Id] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[In_Words] ,[Remarks] ,[R_Invoice] ,[Is_Printed] ,[No_Print] ,[Printed_By] ,[Printed_Date] ,[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[Cleared_By] ,[Cleared_Date] ,[Cancel_By] ,[Cancel_Date] ,[Cancel_Remarks] ,[IsAPIPosted] ,[IsRealtime] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		    SELECT  [SR_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[SB_Invoice] ,[SB_Date] ,[SB_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] , [Subledger_Id],[Agent_ID]  ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[In_Words] ,[Remarks] ,[R_Invoice] ,[Is_Printed] ,[No_Print] ,[Printed_By] ,[Printed_Date],[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[Cleared_By] ,[Cleared_Date] ,[Cancel_By] ,[Cancel_Date] ,[Cancel_Remarks] ,[IsAPIPosted] ,[IsRealtime] ,[CBranch_Id], [CUnit_Id] ,[FiscalYearId] ,'{actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SR_Master] where SR_Invoice = '{SrMaster.SR_Invoice}';
		    
            INSERT INTO AUD.AUDIT_SR_Details (SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SB_Invoice, SB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate FROM AMS.SR_Details sd where SR_Invoice = '{SrMaster.SR_Invoice}'
		    
            INSERT INTO AUD.AUDIT_SR_Term (SR_VNo, ST_Id, SNo, Product_Id, Term_Type, Rate, Amount, Taxable, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SR_VNo, ST_Id, SNo, Product_Id, Term_Type, Rate, Amount, Taxable,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate FROM AMS.SR_Term st WHERE st.SR_VNo='{SrMaster.SR_Invoice}'";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }


    // RETURN VALUE IN SHORT 
    #region  --------------- SHORT ---------------
    public short ReturnSyncRowVersionVoucher(string module, string voucherNo)
    {
        var cmdString = module switch
        {
            "PIN" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PIN_Master pm WHERE pm.PIN_Invoice = '{voucherNo}'",
            "PO" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PO_Master pm WHERE pm.PO_Invoice = '{voucherNo}'",
            "PC" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PC_Master pm WHERE pm.PC_Invoice = '{voucherNo}'",
            "GIT" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.GIT_Master pm WHERE pm.GIT_Invoice = '{voucherNo}'",
            "PB" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PB_Master pm WHERE pm.PB_Invoice = '{voucherNo}'",
            "PR" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PR_Master pm WHERE pm.PR_Invoice = '{voucherNo}'",
            "PAB" => $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.VmPabMaster pm WHERE pm.PAB_Invoice = '{voucherNo}'",
            _ => string.Empty
        };
        var result = cmdString.IsBlankOrEmpty() ? (short)1 : cmdString.GetQueryData().GetShort();

        return result.GetHashCode() > 0 ? result : (short)1;
    }
    #endregion



    // DATA TABLE FUNCTION
    public DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo)
    {
        var cmdString = $@" 
            SELECT PB_Vno FROM AMS.PB_Master where Vendor_ID = '{ledgerId}' and PB_Vno = '{txtRefVno}'  AND FiscalYearId = {ObjGlobal.SysFiscalYearId} ";
        if (action != "SAVE") cmdString += $" AND PB_Invoice <> '{voucherNo}' and Vendor_ID <> '{ledgerId}'  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // OBJECT FOR THIS FORM
    public SR_Master SrMaster { get; set; } = new();
    public List<SR_Details> DetailsList { get; set; } = new();
    public List<SR_Term> Terms { get; set; } = new();
    public List<SR_Master_OtherDetails> SrOther { get; set; } = new();

    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };

    private readonly ClsIrdApiSync _apiSync = new();
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    private readonly ISalesEntry _dataEntry = new ClsSalesEntry();
}