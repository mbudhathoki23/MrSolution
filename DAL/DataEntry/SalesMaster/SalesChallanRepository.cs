using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.SalesChallan;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.DataEntry.SalesMaster;

public class SalesChallanRepository : ISalesChallanRepository
{
    public SalesChallanRepository()
    {
        ScMaster = new SC_Master();
        SbMaster = new SB_Master();
        SbOther = new SB_Master_OtherDetails();
        SrMaster = new SR_Master();
        Terms = new List<SC_Term>();
        ScOther = new List<SC_Master_OtherDetails>();
        DetailsList = new List<SC_Details>();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // SALES CHALLAN INSERT UPDATE DELETE
    public int SaveSalesChallan(string actionTag)
    {
        string[] invoiceNo = { ScMaster.SC_Invoice };
        try
        {
            var cmdString = new StringBuilder();
            if (actionTag is "DELETE" or "REVERSE" or "UPDATE")
            {
                if (!actionTag.Equals("UPDATE"))
                {
                    AuditLogSalesChallan(actionTag);
                }

                if (!actionTag.Equals("REVERSE"))
                {
                    cmdString.Append($" Delete from AMS.SC_Term where SC_VNo ='{ScMaster.SC_Invoice}'; \n");
                    cmdString.Append($" Delete from AMS.SC_Details where SC_Invoice ='{ScMaster.SC_Invoice}'; \n");
                }

                cmdString.Append(
                    $" Delete from AMS.StockDetails Where module='SC' and Voucher_No ='{ScMaster.SC_Invoice}'; \n");
                if (actionTag.Equals("DELETE"))
                {
                    cmdString.Append(
                        $" Delete from AMS.SC_Master_OtherDetails where SC_Invoice = '{ScMaster.SC_Invoice}'; \n");
                    cmdString.Append($" Delete from AMS.SC_Master where SC_Invoice ='{ScMaster.SC_Invoice}'; \n");
                }
            }

            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(
                    " INSERT INTO AMS.SC_Master(SC_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, Ref_Vno, Ref_Date, Ref_Miti, Customer_Id, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, QOT_Invoice, QOT_Date, SO_Invoice, SO_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append("\n VALUES \n");
                cmdString.Append($" (N'{ScMaster.SC_Invoice}', '{ScMaster.Invoice_Date.GetSystemDate()}', N'{ScMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(ScMaster.Ref_Vno.IsValueExits() ? $" N'{ScMaster.Ref_Vno}', '{ScMaster.Ref_Date.GetSystemDate()}','{ScMaster.Ref_Miti}'," : "NULL,NULL,NULL,");
                cmdString.Append($" {ScMaster.Customer_Id},");
                cmdString.Append(ScMaster.PartyLedgerId > 0 ? $" {ScMaster.PartyLedgerId}," : "NULL,");
                cmdString.Append(ScMaster.Party_Name.IsValueExits() ? $" N'{ScMaster.Party_Name}'," : "NULL,");
                cmdString.Append(ScMaster.Vat_No.IsValueExits() ? $" N'{ScMaster.Vat_No}'," : "NULL,");
                cmdString.Append(ScMaster.Contact_Person.IsValueExits() ? $" N'{ScMaster.Contact_Person}'," : "NULL,");
                cmdString.Append(ScMaster.Mobile_No.IsValueExits() ? $" N'{ScMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(ScMaster.Address.IsValueExits() ? $" N'{ScMaster.Address}'," : "NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" N'{ScMaster.ChqNo}'," : "NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" '{ScMaster.ChqDate.GetSystemDate()}'," : "NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" '{ScMaster.ChqMiti}'," : "NULL,");
                cmdString.Append($"N'{ScMaster.Invoice_Type}', N'{ScMaster.Invoice_Mode}', '{ScMaster.Payment_Mode}', {ScMaster.DueDays.GetInt()},");
                cmdString.Append(ScMaster.DueDays > 0 ? $" '{ScMaster.DueDate.GetSystemDate()}'," : "NULL,");
                cmdString.Append(ScMaster.Agent_Id > 0 ? $" {ScMaster.Agent_Id}," : "NULL,");
                cmdString.Append(ScMaster.Subledger_Id > 0 ? $" {ScMaster.Subledger_Id}," : "NULL,");
                cmdString.Append(ScMaster.QOT_Invoice.IsValueExits() ? $" N'{ScMaster.QOT_Invoice}'," : "NULL,");
                cmdString.Append(ScMaster.QOT_Invoice.IsValueExits() ? $" N'{ScMaster.QOT_Date.GetSystemDate()}'," : "NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits() ? $" N'{ScMaster.SO_Invoice}'," : "NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits() ? $" N'{ScMaster.SO_Date.GetSystemDate()}'," : "NULL,");
                cmdString.Append(ScMaster.Cls1 > 0 ? $" {ScMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(ScMaster.CounterId > 0 ? $" {ScMaster.CounterId}," : "NULL,");
                cmdString.Append(ScMaster.Cur_Id > 0 ? $" {ScMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
                cmdString.Append($" {ScMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($" {ScMaster.B_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.T_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.N_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.LN_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.V_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.Tbl_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.Tender_Amount.GetDecimal()},");
                cmdString.Append($" {ScMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($"'{ScMaster.Action_Type}',");
                cmdString.Append("0,NULL,NULL,NULL,0,");
                cmdString.Append(ScMaster.In_Words.IsValueExits() ? $" N'{ScMaster.In_Words}'," : "NULL,");
                cmdString.Append(ScMaster.Remarks.IsValueExits() ? $" N'{ScMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
                cmdString.Append($"0,'{ObjGlobal.LogInUser}',GETDATE(),NULL,NULL,NULL,NULL,{ObjGlobal.SysBranchId},");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append($" {ObjGlobal.SysFiscalYearId},");
                cmdString.Append($" NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,{ScMaster.SyncRowVersion.GetDecimal(true)}); \n");
            }
            else if (actionTag.Equals("UPDATE"))
            {
                cmdString.Append("UPDATE AMS.SC_Master SET ");
                cmdString.Append(
                    $"Invoice_Date = '{ScMaster.Invoice_Date.GetSystemDate()}', Invoice_Miti = N'{ScMaster.Invoice_Miti}',");
                cmdString.Append(ScMaster.Ref_Vno.IsValueExits()
                    ? $" Ref_Vno = N'{ScMaster.Ref_Vno}',Ref_Date = '{ScMaster.Ref_Date.GetSystemDate()}',Ref_Miti = '{ScMaster.Ref_Miti}',"
                    : "Ref_Vno = NULL,Ref_Date = NULL,Ref_Miti = NULL,");
                cmdString.Append($" Customer_Id = {ScMaster.Customer_Id},");
                cmdString.Append(ScMaster.Party_Name.IsValueExits()
                    ? $" Party_Name = N'{ScMaster.Party_Name}',"
                    : "Party_Name = NULL,");
                cmdString.Append(ScMaster.Vat_No.IsValueExits()
                    ? $"Vat_No =  N'{ScMaster.Vat_No}',"
                    : "Vat_No = NULL,");
                cmdString.Append(ScMaster.Contact_Person.IsValueExits()
                    ? $" Contact_Person = N'{ScMaster.Contact_Person}',"
                    : "Contact_Person = NULL,");
                cmdString.Append(ScMaster.Mobile_No.IsValueExits()
                    ? $" Mobile_No = N'{ScMaster.Mobile_No}',"
                    : "Mobile_No = NULL,");
                cmdString.Append(ScMaster.Address.IsValueExits()
                    ? $" Address = N'{ScMaster.Address}',"
                    : "Address = NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{ScMaster.ChqNo}'," : "ChqNo = NULL,");
                cmdString.Append(ScMaster.ChqNo.IsValueExits()
                    ? $" ChqDate = N'{ScMaster.ChqDate.GetSystemDate()}',"
                    : "ChqDate = NULL,");
                cmdString.Append(
                    $"Invoice_Type = N'{ScMaster.Invoice_Type}', Invoice_Mode = N'{ScMaster.Invoice_Mode}',Payment_Mode =  '{ScMaster.Payment_Mode}',DueDays =  {ScMaster.DueDays},");
                cmdString.Append(ScMaster.DueDays > 0
                    ? $" DueDate = '{ScMaster.DueDate.GetSystemDate()}',"
                    : "DueDate = NULL,");
                cmdString.Append(ScMaster.Agent_Id > 0 ? $" Agent_Id = {ScMaster.Agent_Id}," : "Agent_Id = NULL,");
                cmdString.Append(ScMaster.Subledger_Id > 0
                    ? $" Subledger_Id = {ScMaster.Subledger_Id},"
                    : "Subledger_Id = NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits()
                    ? $" SO_Invoice = N'{ScMaster.SO_Invoice}',"
                    : "SO_Invoice = NULL,");
                cmdString.Append(ScMaster.SO_Invoice.IsValueExits()
                    ? $" SO_Date = N'{ScMaster.SO_Date.GetSystemDate()}',"
                    : "SO_Date = NULL,");
                cmdString.Append(ScMaster.QOT_Invoice.IsValueExits()
                    ? $" QOT_Invoice = N'{ScMaster.QOT_Invoice}',"
                    : "QOT_Invoice = NULL,");
                cmdString.Append(ScMaster.QOT_Date.IsValueExits()
                    ? $" QOT_Date = N'{ScMaster.QOT_Date.GetSystemDate()}',"
                    : "SC_Date = NULL,");
                cmdString.Append(ScMaster.Cls1 > 0 ? $" Cls1 = {ScMaster.Cls1}," : "Cls1 = NULL,");
                cmdString.Append(ScMaster.CounterId > 0 ? $"CounterId = {ScMaster.CounterId}," : " CounterId = NULL,");
                cmdString.Append(ScMaster.Cur_Id > 0 ? $" Cur_Id = {ScMaster.Cur_Id}," : "Cur_Id = 1,");
                cmdString.Append($"Cur_Rate =  {ScMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($"B_Amount =  {ScMaster.B_Amount.GetDecimal()},");
                cmdString.Append($"T_Amount = {ScMaster.T_Amount.GetDecimal()},");
                cmdString.Append($"N_Amount = {ScMaster.N_Amount.GetDecimal()},");
                cmdString.Append($"LN_Amount = {ScMaster.LN_Amount.GetDecimal()},");
                cmdString.Append($"V_Amount =  {ScMaster.V_Amount.GetDecimal()},");
                cmdString.Append($"Tbl_Amount =  {ScMaster.Tbl_Amount.GetDecimal()},");
                cmdString.Append($" Tender_Amount = {ScMaster.Tender_Amount.GetDecimal()},");
                cmdString.Append($"Return_Amount =  {ScMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($"Action_Type = '{ScMaster.Action_Type}',");
                cmdString.Append(ScMaster.In_Words.IsValueExits()
                    ? $"In_Words =  N'{ScMaster.In_Words}',"
                    : "In_Words = NULL,");
                cmdString.Append(ScMaster.Remarks.IsValueExits()
                    ? $" Remarks = N'{ScMaster.Remarks.Trim().Replace("'", "''")}',"
                    : "Remarks = NULL,");
                cmdString.Append("IsSynced=0");
                cmdString.Append($" WHERE SC_Invoice = N'{ScMaster.SC_Invoice}'; \n");
            }

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (DetailsList.Count > 0)
                {
                    cmdString.Append(@" 
                        INSERT INTO AMS.SC_Details(SC_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, QOT_Invoice, QOT_Sno, SO_Invoice, SO_SNo, Tax_Amount, V_Amount, V_Rate, AltIssue_Qty, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                    cmdString.Append(" \n VALUES \n");

                    foreach (var dr in DetailsList)
                    {
                        var index = DetailsList.IndexOf(dr);
                        cmdString.Append($"('{dr.SC_Invoice}', {dr.Invoice_SNo}, {dr.P_Id},");
                        cmdString.Append(dr.Gdn_Id > 0 ? $@"{dr.Gdn_Id}," : "NULL,");
                        cmdString.Append($"{dr.Alt_Qty},");
                        cmdString.Append(dr.Alt_UnitId > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                        cmdString.Append($"{dr.Qty}, {dr.Unit_Id}, {dr.Rate}, {dr.B_Amount}, {dr.T_Amount}, {dr.N_Amount},");
                        cmdString.Append($"{dr.AltStock_Qty}, {dr.Stock_Qty},");
                        cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'," : "NULL,");
                        cmdString.Append(dr.QOT_Invoice.IsValueExits() ? $"'{dr.QOT_Invoice}', {dr.QOT_Sno}, " : "NULL,0,");
                        cmdString.Append(dr.SO_Invoice.IsValueExits() ? $"'{dr.SO_Invoice}', {dr.SO_SNo}, " : "NULL,0,");
                        cmdString.Append($"{dr.Tax_Amount},{dr.V_Amount} , {dr.V_Rate}, ");
                        cmdString.Append($"{dr.AltIssue_Qty.GetDecimal()},");
                        cmdString.Append($"{dr.Issue_Qty.GetDecimal()},");
                        cmdString.Append(dr.Free_Unit_Id > 0 ? $"{dr.Free_Unit_Id}," : "NULL,");
                        cmdString.Append($"{dr.Free_Qty}, {dr.StockFree_Qty},");
                        cmdString.Append(dr.ExtraFree_Unit_Id > 0 ? $"{dr.ExtraFree_Unit_Id}," : "NULL,");
                        cmdString.Append($"{dr.ExtraFree_Qty}, {dr.ExtraStockFree_Qty}, CAST('{dr.T_Product}' AS BIT), ");
                        cmdString.Append(dr.S_Ledger > 0 ? $"{dr.S_Ledger}," : "NULL,");
                        cmdString.Append(dr.SR_Ledger > 0 ? $"{dr.SR_Ledger}," : "NULL,");
                        cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                        cmdString.Append($"NULL, NULL, NULL, NULL, ");
                        cmdString.Append(SbMaster.SyncBaseId.IsGuidExits() ? $"'{SbMaster.SyncBaseId}'," : "NULL,");
                        cmdString.Append(SbMaster.SyncGlobalId.IsGuidExits() ? $"'{SbMaster.SyncGlobalId}'," : "NULL,");
                        cmdString.Append(SbMaster.SyncOriginId.IsGuidExits() ? $"'{SbMaster.SyncOriginId}'," : "NULL,");
                        cmdString.Append($"'{SbMaster.SyncCreatedOn.GetSystemDate()}','{SbMaster.SyncLastPatchedOn.GetSystemDate()}',");
                        cmdString.Append($"{SbMaster.SyncRowVersion}");
                        cmdString.Append(index == DetailsList.Count - 1 ? ");\n" : "),\n");
                    }

                    if (Terms.Count > 0)
                    {
                        if (actionTag == "UPDATE")
                        {
                            cmdString.Append($" Delete from AMS.SC_Term where SC_VNo ='{ScMaster.SC_Invoice}'; \n");
                        }
                        cmdString.Append(@" 
                            INSERT INTO AMS.SC_Term(SC_Vno, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                        cmdString.Append("\n VALUES \n");

                        foreach (var item in Terms)
                        {
                            var index = Terms.IndexOf(item);
                            cmdString.Append($"('{item.SC_Vno}', {item.ST_Id}, {item.SNo}, '{item.Term_Type}', ");
                            cmdString.Append(item.Product_Id > 0 ? $"{item.Product_Id}," : "Null,");
                            cmdString.Append($"{item.Rate}, {item.Amount}, ");
                            cmdString.Append($"'{item.Taxable}', ");
                            cmdString.Append(SbMaster.SyncBaseId.IsGuidExits() ? $"'{SbMaster.SyncBaseId}'," : "NULL,");
                            cmdString.Append(SbMaster.SyncGlobalId.IsGuidExits() ? $"'{SbMaster.SyncGlobalId}'," : "NULL,");
                            cmdString.Append(SbMaster.SyncOriginId.IsGuidExits() ? $"'{SbMaster.SyncOriginId}'," : "NULL,");
                            cmdString.Append($"'{SbMaster.SyncCreatedOn.GetSystemDate()}','{SbMaster.SyncLastPatchedOn.GetSystemDate()}',");
                            cmdString.Append($"{SbMaster.SyncRowVersion}");
                            cmdString.Append(index == Terms.Count - 1 ? ");\n" : "),\n");
                        }
                    }
                }



            }

            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }

            if (_tagStrings.Contains(actionTag))
            {
                return iResult;
            }

            if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null)
            {
                Task.Run(() => SyncSalesChallanAsync(actionTag));
            }

            SaveSalesOtherDetails("SC", actionTag);
            UpdateImageOnSales("SC");
            _ = SalesChallanTermPosting();
            _ = SalesChallanStockPosting();
            if (ScMaster.SO_Invoice.IsValueExits() || ScMaster.QOT_Invoice.IsValueExits())
            {
                UpdateOrderChallanOnChallan();
            }

            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            e.DialogResult();
            return 0;
        }
    }
    public int SalesChallanTermPosting()
    {
        var cmdString = $@"
		DELETE AMS.SC_Term WHERE SC_VNo='{ScMaster.SC_Invoice}' AND Term_Type='BT';
		INSERT INTO AMS.SC_Term(SC_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
		SELECT sbt.SC_VNo, ST_Id, sd.Invoice_SNo AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion, sbt.SyncBaseId
		FROM AMS.SC_Details sd
			 LEFT OUTER JOIN AMS.SC_Master sm ON sm.SC_Invoice=sd.SC_Invoice
			 LEFT OUTER JOIN AMS.SC_Term sbt ON sd.SC_Invoice=sbt.SC_VNo
		WHERE sbt.SC_VNo='{ScMaster.SC_Invoice}' AND sbt.Term_Type='B' AND Product_Id IS NULL;";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public int SalesChallanStockPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.StockDetails WHERE Module = 'SC' ");
        cmdString.Append(ScMaster.SC_Invoice.IsValueExits() ? $@" AND Voucher_No = '{SrMaster.SR_Invoice}' " : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM (SELECT 'SC' Module, sd.SC_Invoice Voucher_No, sd.Invoice_SNo Serial_No, sm.Ref_Vno PurRefVno, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_Id, sm.Subledger_Id Subledger_Id, sm.Agent_Id Agent_Id, sm.Cls1 Department_Id1, sm.Cls2 Department_Id2, sm.Cls3 Department_Id3, sm.Cls4 Department_Id4, sm.Cur_Id Currency_Id, sm.Cur_Rate Currency_Rate, sd.P_Id Product_Id, sd.Gdn_Id Godown_Id, NULL CostCenter_Id, sd.Alt_Qty AltQty, sd.Alt_UnitId AltUnit_Id, sd.Qty Qty, sd.Unit_Id Unit_Id, sd.AltStock_Qty AltStockQty, sd.Stock_Qty StockQty, sd.Free_Qty FreeQty, sd.Free_Unit_Id FreeUnit_Id, sd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, sd.Rate Rate, sd.B_Amount BasicAmt, sd.T_Amount TermAmt, sd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, sd.V_Rate TaxRate, sd.Tax_Amount TaxableAmt, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, sm.Ref_Vno PartyInv, 'O' EntryType, sm.Auth_By AuthBy, sm.Auth_Date AuthDate, sm.Reconcile_By RecoBy, sm.Reconcile_Date RecoDate, sm.CounterId Counter_Id, NULL RoomNo, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.CBranch_Id Branch_Id, sm.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, sd.Rate SalesRate, FiscalYearId, sm.SyncGlobalId, sm.SyncOriginId, sm.SyncCreatedOn, sm.SyncLastPatchedOn, sm.SyncRowVersion, sm.SyncBaseId
				FROM AMS.SC_Details sd
				INNER JOIN AMS.Product p ON p.PID = sd.P_Id
				INNER JOIN AMS.SC_Master sm ON sd.SC_Invoice = sm.SC_Invoice
				LEFT OUTER JOIN (SELECT st.Product_Id, st.SC_VNo, st.SNo, SUM(CASE WHEN st1.ST_Sign = '-' THEN -st.Amount ELSE st.Amount END) StockValue
					FROM AMS.SC_Term st
					LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
					WHERE st1.ST_Profitability > 0
					AND st.Term_Type <> 'B' ");
        cmdString.Append(ScMaster.SC_Invoice.IsValueExits() ? @$" AND st.SC_VNo = '{ScMaster.SC_Invoice}' " : " ");
        cmdString.Append(@"
					GROUP BY st.Product_Id, st.SC_VNo, st.SNo
				) stockval ON stockval.Product_Id = sd.P_Id AND sd.SC_Invoice = stockval.SC_VNo AND sd.Invoice_SNo = stockval.SNo
				WHERE p.PType IN ('I','Inventory') AND sm.R_Invoice = 0 AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(ScMaster.SC_Invoice.IsValueExits() ? @$" AND sd.SC_Invoice = '{ScMaster.SC_Invoice}' " : " ");
        cmdString.Append(@"
				) AS Stock ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return isResult;
    }

    public int UpdateOrderChallanOnChallan()
    {
        var cmdString = new StringBuilder();
        if (ScMaster.QOT_Invoice.IsValueExits())
        {
            cmdString.Append($@" 
            UPDATE AMS.SQ_Master SET Invoice_Type ='POSTED' WHERE SQ_Invoice= '{ScMaster.QOT_Invoice}';");
        }

        if (ScMaster.SO_Invoice.IsValueExits())
        {
            cmdString.Append($@" 
            UPDATE AMS.SO_Master SET Invoice_Type ='POSTED' WHERE SO_Invoice = '{ScMaster.SO_Invoice}'; \n");
        }

        var result = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return result;
    }
    public int UpdateImageOnSales(string module)
    {
        var isUpdate = 0;
        var cmdString = new StringBuilder();


        try
        {
            using SqlConnection conn = new SqlConnection(GetConnection.ConnectionString);
            conn.Open();
            if (SbMaster.PAttachment1 != null && SbMaster.PAttachment1.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment1 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment1 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment2 != null && SbMaster.PAttachment2.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment2 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment2 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment3 != null && SbMaster.PAttachment3.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment3 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment3 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment4 != null && SbMaster.PAttachment4.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment4 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment4 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            if (SbMaster.PAttachment5 != null && SbMaster.PAttachment5.Length > 0)
            {
                cmdString.Append($"UPDATE AMS.{module}_Master SET  PAttachment5 = @PImage1 WHERE {module}_Invoice = '{SbMaster.SB_Invoice}'  \n");
                using SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = cmdString.ToString();
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@PImage1", SqlDbType.Image).Value = SbMaster.PAttachment5 ?? new byte[0];
                isUpdate = cmd.ExecuteNonQuery();
            }
            conn.Close();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
        return isUpdate;
    }

    public async Task<int> SyncSalesChallanAsync(string actionTag)
    {
        //sync
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item1 == null)
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = _configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{_configParams.Model.Item2}SalesChallan/GetSalesChallanById";
            insertUrl = @$"{_configParams.Model.Item2}SalesChallan/InsertSalesChallan";
            updateUrl = @$"{_configParams.Model.Item2}SalesChallan/UpdateSalesChallan";
            deleteUrl = @$"{_configParams.Model.Item2}SalesChallan/DeleteSalesChallanAsync?id=" + ScMaster.SC_Invoice;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var salesChallanRepo = DataSyncProviderFactory.GetRepository<SC_Master>(DataSyncManager.GetGlobalInjectData());

            var sc = new SC_Master();
            sc.SC_Invoice = ScMaster.SC_Invoice;
            sc.Invoice_Date = DateTime.Parse(ScMaster.Invoice_Date.ToString("yyyy-MM-dd"));
            sc.Invoice_Miti = ScMaster.Invoice_Miti;
            sc.Invoice_Time = DateTime.Now;
            sc.Ref_Vno = ScMaster.Ref_Vno.IsValueExits() ? ScMaster.Ref_Vno : null;
            sc.Ref_Date = ScMaster.Ref_Vno.IsValueExits()
                ? DateTime.Parse(ScMaster.Ref_Date.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.Ref_Miti = ScMaster.Ref_Vno.IsValueExits() ? ScMaster.Ref_Miti : null;
            sc.Customer_Id = ScMaster.Customer_Id;
            sc.PartyLedgerId = ScMaster.PartyLedgerId > 0 ? ScMaster.PartyLedgerId : null;
            sc.Party_Name = ScMaster.Party_Name.IsValueExits() ? ScMaster.Party_Name : null;
            sc.Vat_No = ScMaster.Vat_No.IsValueExits() ? ScMaster.Vat_No : null;
            sc.Contact_Person = ScMaster.Contact_Person.IsValueExits() ? ScMaster.Contact_Person : null;
            sc.Mobile_No = ScMaster.Mobile_No.IsValueExits() ? ScMaster.Mobile_No : null;
            sc.Address = ScMaster.Address.IsValueExits() ? ScMaster.Address : null;
            sc.ChqNo = ScMaster.ChqNo.IsValueExits() ? ScMaster.ChqNo : null;
            sc.ChqDate = ScMaster.ChqNo.IsValueExits()
                ? Convert.ToDateTime(ScMaster.ChqDate.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.ChqMiti = ScMaster.ChqNo.IsValueExits() ? ScMaster.ChqMiti : null;
            sc.Invoice_Type = ScMaster.Invoice_Type;
            sc.Invoice_Mode = ScMaster.Invoice_Mode;
            sc.Payment_Mode = ScMaster.Payment_Mode;
            sc.DueDays = ScMaster.DueDays.GetInt();
            sc.DueDate = ScMaster.DueDays.GetInt() > 0
                ? Convert.ToDateTime(ScMaster.DueDate.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.Agent_Id = ScMaster.Agent_Id > 0 ? ScMaster.Agent_Id : null;
            sc.Subledger_Id = ScMaster.Subledger_Id > 0 ? ScMaster.Subledger_Id : null;
            sc.QOT_Invoice = ScMaster.QOT_Invoice.IsValueExits() ? ScMaster.QOT_Invoice : null;
            sc.QOT_Date = ScMaster.QOT_Invoice.IsValueExits()
                ? Convert.ToDateTime(ScMaster.QOT_Date.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.SO_Invoice = ScMaster.SO_Invoice.IsValueExits() ? ScMaster.SO_Invoice : null;
            sc.SO_Date = ScMaster.SO_Invoice.IsValueExits()
                ? Convert.ToDateTime(ScMaster.SO_Date.Value.ToString("yyyy-MM-dd"))
                : null;
            sc.Cls1 = ScMaster.Cls1 > 0 ? ScMaster.Cls1 : null;
            sc.Cls2 = null;
            sc.Cls3 = null;
            sc.Cls4 = null;
            sc.CounterId = ScMaster.CounterId > 0 ? ScMaster.CounterId : null;
            sc.Cur_Id = ScMaster.Cur_Id > 0 ? ScMaster.Cur_Id : ObjGlobal.SysCurrencyId;
            sc.Cur_Rate = ScMaster.Cur_Rate.GetDecimal();
            sc.B_Amount = ScMaster.B_Amount.GetDecimal();
            sc.T_Amount = ScMaster.T_Amount.GetDecimal();
            sc.N_Amount = ScMaster.N_Amount.GetDecimal();
            sc.LN_Amount = ScMaster.LN_Amount.GetDecimal();
            sc.V_Amount = ScMaster.V_Amount.GetDecimal();
            sc.Tbl_Amount = ScMaster.Tbl_Amount.GetDecimal();
            sc.Tender_Amount = ScMaster.Tender_Amount.GetDecimal();
            sc.Return_Amount = ScMaster.Return_Amount.GetDecimal();
            sc.Action_Type = ScMaster.Action_Type;
            sc.R_Invoice = false;
            sc.CancelBy = null;
            sc.CancelDate = null;
            sc.CancelReason = null;
            sc.No_Print = 0;
            sc.In_Words = ScMaster.In_Words.IsValueExits() ? ScMaster.In_Words : null;
            sc.Remarks = ScMaster.Remarks.IsValueExits() ? ScMaster.Remarks : null;
            sc.Audit_Lock = false;
            sc.Enter_By = ObjGlobal.LogInUser;
            sc.Enter_Date = DateTime.Now;
            sc.Reconcile_By = null;
            sc.Reconcile_Date = null;
            sc.Auth_By = null;
            sc.Auth_Date = null;
            sc.CBranch_Id = ObjGlobal.SysBranchId;
            sc.CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            sc.FiscalYearId = ObjGlobal.SysFiscalYearId;
            sc.PAttachment1 = null;
            sc.PAttachment2 = null;
            sc.PAttachment3 = null;
            sc.PAttachment4 = null;
            sc.PAttachment5 = null;
            sc.SyncRowVersion = ScMaster.SyncRowVersion;
            var scDetails = new List<SC_Details>();
            var scTerms = new List<SC_Term>();
            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (ScMaster.GetView is { RowCount: > 0 })
                {
                    foreach (DataGridViewRow dr in ScMaster.GetView.Rows)
                    {
                        var scd = new SC_Details();
                        scd.SC_Invoice = ScMaster.SC_Invoice;
                        scd.Invoice_SNo = dr.Cells["GTxtSno"].Value.GetInt();
                        scd.P_Id = dr.Cells["GTxtProductId"].Value.GetLong();
                        scd.Gdn_Id = dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0
                            ? dr.Cells["GTxtGodownId"].Value.GetInt()
                            : null;
                        scd.Alt_Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
                            ? dr.Cells["GTxtAltQty"].Value.GetDecimal()
                            : 0;
                        scd.Alt_UnitId = dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0
                            ? dr.Cells["GTxtAltUOMId"].Value.GetInt()
                            : null;
                        scd.Qty = dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0
                            ? dr.Cells["GTxtAltQty"].Value.GetDecimal()
                            : 1;
                        scd.Unit_Id = dr.Cells["GTxtUOMId"].Value.GetInt() > 0
                            ? dr.Cells["GTxtUOMId"].Value.GetInt()
                            : null;
                        scd.Rate = dr.Cells["GTxtRate"].Value.GetDecimal();
                        scd.B_Amount = dr.Cells["GTxtAmount"].Value.GetDecimal();
                        scd.T_Amount = dr.Cells["GTxtTermAmount"].Value.GetDecimal();
                        scd.N_Amount = dr.Cells["GTxtNetAmount"].Value.GetDecimal();
                        scd.AltStock_Qty = dr.Cells["GTxtAltStockQty"].Value.GetDecimal();
                        scd.Stock_Qty = dr.Cells["GTxtStockQty"].Value.GetDecimal();
                        scd.Narration = dr.Cells["GTxtNarration"].Value.IsValueExits()
                            ? dr.Cells["GTxtNarration"].Value.GetTrimReplace()
                            : null;
                        scd.QOT_Invoice = dr.Cells["GTxtQuotNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtQuotNo"].Value.ToString()
                            : null;
                        scd.QOT_Sno = dr.Cells["GTxtQuotNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtQuotSno"].Value.GetInt()
                            : 0;
                        scd.SO_Invoice = dr.Cells["GTxtOrderNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtOrderNo"].Value.ToString()
                            : null;
                        scd.SO_SNo = dr.Cells["GTxtOrderNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtOrderSno"].Value.GetInt()
                            : 0;
                        scd.Tax_Amount = 0;
                        scd.V_Amount = 0;
                        scd.V_Rate = 0;
                        scd.AltIssue_Qty = 0;
                        scd.Issue_Qty = 0;
                        scd.Free_Unit_Id = null;
                        scd.Free_Qty = 0;
                        scd.StockFree_Qty = 0;
                        scd.ExtraFree_Unit_Id = null;
                        scd.ExtraFree_Qty = 0;
                        scd.ExtraStockFree_Qty = 0;
                        scd.T_Product = null;
                        scd.S_Ledger = null;
                        scd.SR_Ledger = null;
                        scd.SZ1 = null;
                        scd.SZ2 = null;
                        scd.SZ3 = null;
                        scd.SZ4 = null;
                        scd.SZ5 = null;
                        scd.SZ6 = null;
                        scd.SZ7 = null;
                        scd.SZ8 = null;
                        scd.SZ9 = null;
                        scd.SZ10 = null;
                        scd.Serial_No = dr.Cells["GTxtSerialNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtSerialNo"].Value.ToString()
                            : null;
                        scd.Batch_No = dr.Cells["GTxtBatchNo"].Value.IsValueExits()
                            ? dr.Cells["GTxtBatchNo"].Value.ToString()
                            : null;
                        scd.Exp_Date = dr.Cells["GTxtBatchNo"].Value.IsValueExits()
                            ? Convert.ToDateTime(dr.Cells["GTxtExpiryDate"].Value.GetSystemDate())
                            : null;
                        scd.Manu_Date = dr.Cells["GTxtBatchNo"].Value.IsValueExits()
                            ? Convert.ToDateTime(dr.Cells["GTxtMfgDate"].Value.GetSystemDate())
                            : null;
                        scd.SyncRowVersion = ScMaster.SyncRowVersion;
                        scDetails.Add(scd);
                    }
                }

                if (ScMaster.dtPTerm != null && ScMaster.dtPTerm.Rows.Count > 0)
                {
                    foreach (DataRow dr in ScMaster.dtPTerm.Rows)
                    {
                        if (dr["TermAmt"].GetDecimal() == 0)
                        {
                            continue;
                        }

                        var sct = new SC_Term();
                        sct.SC_Vno = ScMaster.SC_Invoice;
                        sct.ST_Id = dr["TermId"].GetInt();
                        sct.SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1;
                        sct.Term_Type = "P";
                        sct.Product_Id = dr["ProductId"].GetInt() > 0 ? dr["ProductId"].GetLong() : null;
                        sct.Rate = dr["TermRate"].GetDecimal();
                        sct.Amount = dr["TermAmt"].GetDecimal();
                        sct.Taxable = dr["TermRate"].GetDecimal() > 0 &&
                                      dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId)
                            ? "Y"
                            : "N";
                        sct.SyncRowVersion = ScMaster.SyncRowVersion;
                        scTerms.Add(sct);
                    }
                }

                if (ScMaster.dtBTerm != null && ScMaster.dtBTerm.Rows.Count > 0)
                {
                    foreach (DataRow dr in ScMaster.dtBTerm.Rows)
                    {
                        if (dr["TermAmt"].GetDecimal() == 0)
                        {
                            continue;
                        }

                        var sct = new SC_Term();
                        sct.SC_Vno = ScMaster.SC_Invoice;
                        sct.ST_Id = dr["TermId"].GetInt();
                        sct.SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1;
                        sct.Term_Type = "B";
                        sct.Product_Id = dr["ProductId"].GetInt() > 0 ? dr["ProductId"].GetLong() : null;
                        sct.Rate = dr["TermRate"].GetDecimal();
                        sct.Amount = dr["TermAmt"].GetDecimal();
                        sct.Taxable = dr["TermRate"].GetDecimal() > 0 &&
                                      dr["TermId"].GetInt().Equals(ObjGlobal.SalesVatTermId)
                            ? "Y"
                            : "N";
                        sct.SyncRowVersion = ScMaster.SyncRowVersion;
                        scTerms.Add(sct);
                    }
                }
            }

            var scOtherList = new List<SC_Master_OtherDetails>();
            var sco = new SC_Master_OtherDetails
            {
                SC_Invoice = SbOther.SB_Invoice,
                Transport = SbOther.Transport,
                VechileNo = SbOther.VechileNo,
                BiltyNo = SbOther.BiltyNo,
                Package = SbOther.Package,
                BiltyDate = !string.IsNullOrEmpty(SbOther.BiltyNo)
                    ? Convert.ToDateTime(SbOther.BiltyDate.Value.ToString("yyyy-MM-dd"))
                    : null,
                BiltyType = SbOther.BiltyType,
                Driver = SbOther.Driver,
                PhoneNo = SbOther.PhoneNo,
                LicenseNo = SbOther.LicenseNo,
                MailingAddress = SbOther.MailingAddress,
                MCity = SbOther.MCity,
                MState = SbOther.MState,
                MCountry = SbOther.MCountry,
                MEmail = SbOther.MEmail,
                ShippingAddress = SbOther.ShippingAddress,
                SCity = SbOther.SCity,
                SState = SbOther.SState,
                SCountry = SbOther.SCountry,
                SEmail = SbOther.SEmail,
                ContractNo = SbOther.ContractNo,
                ContractDate = !string.IsNullOrEmpty(SbOther.ContractNo)
                    ? Convert.ToDateTime(SbOther.ContractDate.Value.ToString("yyyy-MM-dd"))
                    : null,
                ExportInvoice = SbOther.ExportInvoice,
                ExportInvoiceDate = !string.IsNullOrEmpty(SbOther.ExportInvoice)
                    ? Convert.ToDateTime(SbOther.ExportInvoiceDate.Value.ToString("yyyy-MM-dd"))
                    : null,
                VendorOrderNo = SbOther.VendorOrderNo,
                BankDetails = SbOther.BankDetails,
                LcNumber = SbOther.LcNumber,
                CustomDetails = SbOther.CustomDetails
            };
            scOtherList.Add(sco);
            sc.DetailsList = scDetails;
            sc.Terms = scTerms;
            sc.OtherDetails = scOtherList;
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await (salesChallanRepo?.PushNewAsync(sc)),
                "NEW" => await (salesChallanRepo?.PushNewAsync(sc)),
                "UPDATE" => await (salesChallanRepo?.PutNewAsync(sc)),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await (salesChallanRepo?.PushNewAsync(sc))
            };
            if (result.Value)
            {
                var cmdString = new StringBuilder();
                cmdString.Append($"UPDATE AMS.SC_Master SET IsSynced = 1 WHERE SC_Invoice = '{sc.SC_Invoice}';");
                SaveDataInDatabase(cmdString);
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int SaveSalesOtherDetails(string module, string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "SAVE")
        {
            cmdString.Append($"INSERT INTO AMS.{module}_Master_OtherDetails({module}_Invoice,");
            cmdString.Append(" Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails) \n");
            cmdString.Append(" 	VALUES (  \n");
            cmdString.Append($" N'{SbOther.SB_Invoice}', N'{SbOther.Transport}', N'{SbOther.VechileNo}', N'{SbOther.BiltyNo}', N'{SbOther.Package}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.BiltyNo)
                ? $" '{Convert.ToDateTime(SbOther.BiltyDate):yyyy-MM-dd}', " : "Null,");
            cmdString.Append(
                $"N'{SbOther.BiltyType}', N'{SbOther.Driver}', N'{SbOther.PhoneNo}', N'{SbOther.LicenseNo}', N'{SbOther.MailingAddress}', N'{SbOther.MCity}', N'{SbOther.MState}', N'{SbOther.MCountry}', N'{SbOther.MEmail}', N'{SbOther.ShippingAddress}', N'{SbOther.SCity}', N'{SbOther.SState}', N'{SbOther.SCountry}', N'{SbOther.SEmail}', N'{SbOther.ContractNo}',");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ContractNo)
                ? $" '{Convert.ToDateTime(SbOther.ContractDate):yyyy-MM-dd}', " : "Null,");
            cmdString.Append($"N'{SbOther.ExportInvoice}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ExportInvoice)
                ? $" '{Convert.ToDateTime(SbOther.ExportInvoiceDate):yyyy-MM-dd}', " : "Null,");
            cmdString.Append($"N'{SbOther.VendorOrderNo}', N'{SbOther.BankDetails}', N'{SbOther.LcNumber}', N'{SbOther.CustomDetails}'); \n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append($" UPDATE AMS.{module}_Master_OtherDetails SET ");
            cmdString.Append($" Transport= N'{SbOther.Transport}', VechileNo= N'{SbOther.VechileNo}', BiltyNo = N'{SbOther.BiltyNo}', Package = N'{SbOther.Package}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.BiltyNo)
                ? $"BiltyDate=  '{Convert.ToDateTime(SbOther.BiltyDate):yyyy-MM-dd}', " : "BiltyDate= Null,");
            cmdString.Append($" BiltyType= N'{SbOther.BiltyType}',Driver=  N'{SbOther.Driver}', PhoneNo= N'{SbOther.PhoneNo}',LicenseNo= N'{SbOther.LicenseNo}',MailingAddress=  N'{SbOther.MailingAddress}',MCity=  N'{SbOther.MCity}',MState=  N'{SbOther.MState}',MCountry=  N'{SbOther.MCountry}',MEmail=  N'{SbOther.MEmail}',ShippingAddress=  N'{SbOther.ShippingAddress}', SCity= N'{SbOther.SCity}', SState= N'{SbOther.SState}', SCountry= N'{SbOther.SCountry}',SEmail= N'{SbOther.SEmail}', ContractNo= N'{SbOther.ContractNo}',");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ContractNo)
                ? $"ContractDate=  '{Convert.ToDateTime(SbOther.ContractDate):yyyy-MM-dd}', " : "ContractDate= Null,");
            cmdString.Append($"ExportInvoice= N'{SbOther.ExportInvoice}', ");
            cmdString.Append(!string.IsNullOrEmpty(SbOther.ExportInvoice)
                ? $"ExportInvoiceDate=  '{Convert.ToDateTime(SbOther.ExportInvoiceDate):yyyy-MM-dd}', " : "ExportInvoiceDate= Null,");
            cmdString.Append($" VendorOrderNo= N'{SbOther.VendorOrderNo}',BankDetails= N'{SbOther.BankDetails}',LcNumber= N'{SbOther.LcNumber}', CustomDetails= N'{SbOther.CustomDetails}' WHERE {module}_Invoice = N'{SbOther.SB_Invoice}' ; ");
        }
        else if (actionTag is "DELETE")
        {
            cmdString.Append($" DELETE AMS.{module}_Master_OtherDetails WHERE {module}_Invoice = '{SbOther.SB_Invoice}'; ");
        }

        if (cmdString.Length is 0)
        {
            return 0;
        }

        var saveData = SaveDataInDatabase(cmdString);
        return saveData;
    }
    private int SaveDataInDatabase(StringBuilder query)
    {
        return ExecuteValueInDatabase(query.ToString());
    }
    private int ExecuteValueInDatabase(string query)
    {
        try
        {
            var iRows = SqlExtensions.ExecuteNonTrans(query);
            return iRows;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
            return 0;
        }
    }



    //AUDIT LOG OF SALES
    private int AuditLogSalesChallan(string actionTag)
    {
        var cmdString = @$"
		INSERT INTO [AUD].[AUDIT_SC_Master] ([SC_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[Ref_Vno] ,[Ref_Date] ,[Ref_Miti] ,[Customer_Id] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Agent_Id] ,[Subledger_Id] ,[QOT_Invoice] ,[QOT_Date] ,[SO_Invoice] ,[SO_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[R_Invoice] ,[No_Print] ,[In_Words] ,[Remarks] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId] ,[Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT [SC_Invoice] ,[Invoice_Date] ,[Invoice_Miti] ,[Invoice_Time] ,[Ref_VNo] ,[Ref_Date] ,[Ref_Miti] ,[Customer_ID] ,[Party_Name] ,[Vat_No] ,[Contact_Person] ,[Mobile_No] ,[Address] ,[ChqNo] ,[ChqDate] ,[Invoice_Type] ,[Invoice_Mode] ,[Payment_Mode] ,[DueDays] ,[DueDate] ,[Agent_ID] ,[Subledger_Id] ,[QOT_Invoice] ,[QOT_Date] ,[SO_Invoice] ,[SO_Date] ,[Cls1] ,[Cls2] ,[Cls3] ,[Cls4] ,[CounterId] ,[Cur_Id] ,[Cur_Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[LN_Amount] ,[V_Amount] ,[Tbl_Amount] ,[Tender_Amount] ,[Return_Amount] ,[Action_Type] ,[R_Invoice] ,[No_Print] ,[In_Words] ,[Remarks] ,[CBranch_Id] ,[CUnit_Id] ,[FiscalYearId], [Audit_Lock] ,[Enter_By] ,[Enter_Date] ,[Reconcile_By] ,[Reconcile_Date] ,[Auth_By] ,[Auth_Date] ,[PAttachment1] ,[PAttachment2] ,[PAttachment3] ,[PAttachment4] ,[PAttachment5] ,'{actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.SC_Master where SC_Invoice = '{ScMaster.SC_Invoice}';
		
        INSERT INTO [AUD].[AUDIT_SC_Term] ([SC_Vno] ,[ST_Id] ,[Product_Id] ,[SNo] ,[Term_Type] ,[Rate] ,[Amount] ,[Taxable] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT [SC_Vno] ,[ST_Id] ,[SNo] ,[Rate] ,[Amount] ,[Term_Type] ,[Product_Id] ,[Taxable]  , ' {actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SC_Term] where SC_Vno = '{ScMaster.SC_Invoice}';
		
        INSERT INTO [AUD].[AUDIT_SC_Details] ([SC_Invoice] ,[Invoice_SNo] ,[P_Id] ,[Gdn_Id] ,[Alt_Qty] ,[Alt_UnitId] ,[Qty] ,[Unit_Id] ,[Rate] ,[B_Amount] ,[T_Amount] ,[N_Amount] ,[AltStock_Qty] ,[Stock_Qty] ,[Narration] ,[QOT_Invoice] ,[QOT_Sno] ,[SO_Invoice] ,[SO_SNo] ,[Tax_Amount] ,[V_Amount] ,[V_Rate] ,[Issue_Qty] ,[Free_Unit_Id] ,[Free_Qty] ,[StockFree_Qty] ,[ExtraFree_Unit_Id] ,[ExtraFree_Qty] ,[ExtraStockFree_Qty] ,[T_Product] ,[S_Ledger] ,[SR_Ledger] ,[SZ1] ,[SZ2] ,[SZ3] ,[SZ4] ,[SZ5] ,[SZ6] ,[SZ7] ,[SZ8] ,[SZ9] ,[SZ10] ,[Serial_No] ,[Batch_No] ,[Exp_Date] ,[Manu_Date] ,[AltIssue_Qty] ,[ModifyAction] ,[ModifyBy] ,[ModifyDate])
		SELECT [SC_Invoice]	,[Invoice_SNo]	,[P_Id]	,[Gdn_Id]	,[Alt_Qty]	,[Alt_UnitId]	,[Qty]	,[Unit_Id]	,[Rate]	,[B_Amount]	,[T_Amount]	,[N_Amount]	,[AltStock_Qty]	,[Stock_Qty]	,[Narration]	,[QOT_Invoice]	,[QOT_SNo]	,[SO_Invoice]	,[SO_Sno]	,[Tax_Amount]	,[V_Amount]	,[V_Rate]	,[Issue_Qty]	,[Free_Unit_Id]	,[Free_Qty]	,[StockFree_Qty]	,[ExtraFree_Unit_Id]	,[ExtraFree_Qty]	,[ExtraStockFree_Qty]	,[T_Product]	,[S_Ledger]	,[SR_Ledger]	,[SZ1]	,[SZ2]	,[SZ3]	,[SZ4]	,[SZ5]	,[SZ6]	,[SZ7]	,[SZ8]	,[SZ9]	,[SZ10]	,[Serial_No]	,[Batch_No]	,[Exp_Date]	,[Manu_Date]	,[AltIssue_Qty]  , ' {actionTag}','{ObjGlobal.LogInUser}',GetDate() From AMS.[SC_Details] where SC_Invoice = '{ScMaster.SC_Invoice}';  ";
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
    public SC_Master ScMaster { get; set; }
    public SB_Master SbMaster { get; set; }
    public List<SC_Details> DetailsList { get; set; }
    public List<SC_Term> Terms { get; set; }
    public List<SC_Master_OtherDetails> ScOther { get; set; }
    public SB_Master_OtherDetails SbOther { get; set; }
    public SR_Master SrMaster { get; set; }

    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}