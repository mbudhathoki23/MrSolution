using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseReturn;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.PurchaseMaster;

public class PurchaseReturnInvoiceRepository : IPurchaseReturn
{
    // PURCHASE RETURN INVOICE REPOSITORY
    #region ---------- PURCHASE RETURN INVOICE ----------
    // PURCHASE RETURN INVOICE REPOSITORY
    public PurchaseReturnInvoiceRepository()
    {
        DetailsList = new List<PR_Details>();
        AddInfos = new List<ProductAddInfo>();
        Terms = new List<PR_Term>();
    }


    // INSERT UPDATE DELETE
    public int SavePurchaseReturnInvoice(string actionTag)
    {
        var cmdString = new StringBuilder();

        if (!actionTag.Equals("SAVE"))
        {
            AuditLogPurchaseReturnInvoice(actionTag);
        }

        if (actionTag is "DELETE" or "UPDATE" or "REVERSE")
        {
            cmdString.Append($@" 
                DELETE AMS.StockDetails WHERE Module='PR' AND Voucher_No= '{PrMaster.PR_Invoice}';");
            cmdString.Append($@" 
                DELETE AMS.AccountDetails WHERE Module ='PR' AND Voucher_No = '{PrMaster.PR_Invoice}';");
            if (actionTag != "REVERSE")
            {
                cmdString.Append($" DELETE AMS.PR_Term WHERE PR_VNo = '{PrMaster.PR_Invoice}'; \n");
                cmdString.Append($" DELETE AMS.PR_Details WHERE PR_Invoice='{PrMaster.PR_Invoice}'; \n");
            }

            if (actionTag is "DELETE")
            {
                cmdString.Append($" DELETE AMS.PR_Master WHERE PR_Invoice ='{PrMaster.PR_Invoice}'; \n");
            }
        }

        if (actionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(@"
                INSERT INTO AMS.PR_Master(PR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, R_Invoice, CancelBy, CancelDate, CancelRemarks, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, ReturnChallanNo, ReturnChallanDate, IsSynced) ");
            cmdString.Append($@"
                VALUES('{PrMaster.PR_Invoice}', '{PrMaster.Invoice_Date.GetSystemDate()}', '{PrMaster.Invoice_Miti}','{PrMaster.Invoice_Time.GetSystemDate()}',");
            cmdString.Append(PrMaster.PB_Invoice.IsValueExits() ? $" '{PrMaster.PB_Invoice}','{PrMaster.PB_Date.GetSystemDate()}','{PrMaster.PB_Miti}'," : "NULL,NULL,NULL,");
            cmdString.Append($" {PrMaster.Vendor_ID},");
            cmdString.Append(PrMaster.PartyLedgerId.IsValueExits() ? $" {PrMaster.PartyLedgerId}," : "NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" '{PrMaster.Party_Name}'," : "NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" '{PrMaster.Vat_No}'," : "NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" '{PrMaster.Contact_Person}'," : "NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" '{PrMaster.Mobile_No}'," : "NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" '{PrMaster.Address}'," : "NULL,");
            cmdString.Append(PrMaster.ChqNo.IsValueExits() ? $" '{PrMaster.ChqNo}', '{PrMaster.ChqDate.GetSystemDate()}', '{PrMaster.ChqMiti}'," : "NULL,NULL,NULL,");
            cmdString.Append($" '{PrMaster.Invoice_Type}', '{PrMaster.Invoice_In}', {PrMaster.DueDays},");
            cmdString.Append(PrMaster.DueDays > 0 ? $" '{PrMaster.DueDate.GetSystemDate()}'," : "NULL,");
            cmdString.Append(PrMaster.Agent_Id > 0 ? $"{PrMaster.Agent_Id} ," : "NULL,");
            cmdString.Append(PrMaster.Subledger_Id > 0 ? $"{PrMaster.Subledger_Id} ," : "NULL,");
            cmdString.Append(PrMaster.Cls1 > 0 ? $"{PrMaster.Cls1} ," : "NULL,");
            cmdString.Append($" NULL, NULL, NULL,");
            cmdString.Append(PrMaster.Cur_Id > 0 ? $" {PrMaster.Cur_Id}, " : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append($" {PrMaster.Cur_Rate},{PrMaster.B_Amount}, {PrMaster.T_Amount}, {PrMaster.N_Amount}, {PrMaster.LN_Amount},");
            cmdString.Append($" {PrMaster.Tender_Amount}, {PrMaster.Change_Amount}, {PrMaster.V_Amount}, {PrMaster.Tbl_Amount},");
            cmdString.Append($" '{PrMaster.Action_type}', 0, '{PrMaster.In_Words}',");
            cmdString.Append(PrMaster.Remarks.IsValueExits() ? $"'{PrMaster.Remarks}' ," : "NULL,");
            cmdString.Append($" 0, '{PrMaster.Enter_By}', '{PrMaster.Enter_Date.GetSystemDate()}', ");
            cmdString.Append($"NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL,");
            cmdString.Append(PrMaster.CBranch_Id > 0 ? $" {PrMaster.CBranch_Id}," : $"{ObjGlobal.SysBranchId},");
            cmdString.Append(PrMaster.CUnit_Id > 0 ? $" {PrMaster.CUnit_Id}," : "NULL,");
            cmdString.Append(PrMaster.FiscalYearId > 0 ? $"{PrMaster.FiscalYearId} ," : $"{ObjGlobal.SysFiscalYearId},");
            cmdString.Append(" NULL, NULL, NULL, NULL, NULL,");
            cmdString.Append(PrMaster.SyncBaseId.IsGuidExits() ? $"'{PrMaster.SyncBaseId}'," : "NULL,");
            cmdString.Append(PrMaster.SyncBaseId.IsGuidExits() ? $" '{PrMaster.SyncGlobalId}'," : "NULL,");
            cmdString.Append(PrMaster.SyncBaseId.IsGuidExits() ? $" '{PrMaster.SyncOriginId}'," : "NULL,");
            cmdString.Append($" '{PrMaster.SyncCreatedOn.GetSystemDate()}',");
            cmdString.Append($" '{PrMaster.SyncLastPatchedOn.GetSystemDate()}',");
            cmdString.Append($" {PrMaster.SyncRowVersion.GetDecimal(true)},");
            cmdString.Append(PrMaster.ReturnChallanNo.IsValueExits() ? $" '{PrMaster.ReturnChallanNo}'," : "NULL,");
            cmdString.Append(PrMaster.ReturnChallanNo.IsValueExits() ? $"'{PrMaster.ReturnChallanDate.GetSystemDate()}'," : "NULL,");
            cmdString.Append(" 0); \n");
        }

        else if (actionTag.ToUpper() is "UPDATE")
        {
            cmdString.Append("\n UPDATE AMS.PR_Master SET ");
            cmdString.Append($" Invoice_Date = '{PrMaster.Invoice_Date.GetSystemDate()}', Invoice_Miti ='{PrMaster.Invoice_Miti}',");
            cmdString.Append(PrMaster.PB_Invoice.IsValueExits() ? $" PB_Invoice ='{PrMaster.PB_Invoice}',PB_Date = '{PrMaster.PB_Date.GetSystemDate()}',PB_Miti = '{PrMaster.PB_Miti}'," : "PB_Invoice = NULL,PB_Date = NULL,PB_Miti = NULL,");
            cmdString.Append($" Vendor_ID = {PrMaster.Vendor_ID},");
            cmdString.Append(PrMaster.PartyLedgerId.IsValueExits() ? $" PartyLedgerId = {PrMaster.PartyLedgerId}," : "PartyLedgerId = NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" Party_Name = '{PrMaster.Party_Name}'," : "Party_Name = NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" Vat_No = '{PrMaster.Vat_No}'," : "Vat_No = NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" Contact_Person = '{PrMaster.Contact_Person}'," : "Contact_Person = NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" Mobile_No = '{PrMaster.Mobile_No}'," : "Mobile_No = NULL,");
            cmdString.Append(PrMaster.Party_Name.IsValueExits() ? $" Address = '{PrMaster.Address}'," : "Address = NULL,");
            cmdString.Append(PrMaster.ChqNo.IsValueExits() ? $" ChqNo = '{PrMaster.ChqNo}', ChqDate = '{PrMaster.ChqDate.GetSystemDate()}', ChqMiti = '{PrMaster.ChqMiti}'," : "ChqNo = NULL,ChqDate = NULL,ChqMiti = NULL,");
            cmdString.Append($" Invoice_Type ='{PrMaster.Invoice_Type}', Invoice_In ='{PrMaster.Invoice_In}', DueDays ={PrMaster.DueDays},");
            cmdString.Append(PrMaster.DueDays > 0 ? $" DueDate = '{PrMaster.DueDate.GetSystemDate()}'," : "DueDate =NULL,");
            cmdString.Append(PrMaster.Agent_Id > 0 ? $"Agent_Id = {PrMaster.Agent_Id} ," : "Agent_Id = NULL,");
            cmdString.Append(PrMaster.Subledger_Id > 0 ? $"Subledger_Id = {PrMaster.Subledger_Id} ," : "Subledger_Id = NULL,");
            cmdString.Append(PrMaster.Cls1 > 0 ? $"Cls1 = {PrMaster.Cls1} ," : "Cls1 = NULL,");
            cmdString.Append(PrMaster.Cur_Id > 0 ? $" Cur_Id = {PrMaster.Cur_Id}, " : $"Cur_Id = {ObjGlobal.SysCurrencyId},");
            cmdString.Append($" Cur_Rate = {PrMaster.Cur_Rate},B_Amount = {PrMaster.B_Amount}, T_Amount = {PrMaster.T_Amount}, N_Amount = {PrMaster.N_Amount}, LN_Amount = {PrMaster.LN_Amount},");
            cmdString.Append($" Tender_Amount = {PrMaster.Tender_Amount}, Change_Amount = {PrMaster.Change_Amount}, V_Amount = {PrMaster.V_Amount}, Tbl_Amount = {PrMaster.Tbl_Amount},");
            cmdString.Append($" Action_type = '{PrMaster.Action_type}', In_Words = '{PrMaster.In_Words}',");
            cmdString.Append(PrMaster.Remarks.IsValueExits() ? $"Remarks = '{PrMaster.Remarks}' ," : "Remarks = NULL,");
            cmdString.Append($" SyncLastPatchedOn = '{PrMaster.SyncLastPatchedOn.GetSystemDate()}',");
            cmdString.Append($" SyncRowVersion = {PrMaster.SyncRowVersion.GetDecimal(true)},");
            cmdString.Append(PrMaster.ReturnChallanNo.IsValueExits() ? $" ReturnChallanNo = '{PrMaster.ReturnChallanNo}'," : "ReturnChallanNo =NULL,");
            cmdString.Append(PrMaster.ReturnChallanNo.IsValueExits() ? $" ReturnChallanDate = '{PrMaster.ReturnChallanDate.GetSystemDate()}'," : "ReturnChallanDate =NULL");
            cmdString.Append($" WHERE PR_Invoice='{PrMaster.PR_Invoice}'; \n");
        }

        if (actionTag.ToUpper() != "DELETE" && !actionTag.Equals("REVERSE"))
        {
            cmdString.Append(@"
                INSERT INTO AMS.PR_Details(PR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PB_Invoice, PB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, ReturnChallanNo,ReturnChallanSno)
                VALUES ");
            foreach (var details in DetailsList)
            {
                var index = DetailsList.IndexOf(details);

                cmdString.Append($" ('{details.PR_Invoice}',{details.Invoice_SNo} , {details.P_Id},");
                cmdString.Append(details.Gdn_Id > 0 ? $" {details.Gdn_Id}," : "NULL,");

                cmdString.Append($" {details.Alt_Qty},");
                cmdString.Append(details.Alt_UnitId > 0 ? $" {details.Alt_UnitId}," : "NULL,");

                cmdString.Append($" {details.Qty}, {details.Unit_Id},");
                cmdString.Append($" {details.Rate}, {details.B_Amount}, {details.T_Amount}, {details.N_Amount},");
                cmdString.Append($" {details.AltStock_Qty}, {details.Stock_Qty},");

                cmdString.Append(details.Narration.IsValueExits() ? $" '{details.Narration}', " : "NULL,");

                cmdString.Append(details.PB_Invoice.IsValueExits() ? $" '{details.PB_Invoice}', {details.PB_Sno}," : "NULL,0,");
                cmdString.Append($" {details.Tax_Amount}, {details.V_Amount}, {details.V_Rate},");
                cmdString.Append(details.Free_Unit_Id > 0 ? $" {details.Free_Unit_Id}, " : "NULL,");

                cmdString.Append($" {details.Free_Qty},");
                cmdString.Append($" {details.StockFree_Qty},");
                cmdString.Append(details.ExtraFree_Unit_Id > 0 ? $" {details.ExtraFree_Unit_Id}," : "NULL,");
                cmdString.Append($" {details.ExtraFree_Qty},{details.ExtraStockFree_Qty},CAST('{details.T_Product}' AS BIT),");

                cmdString.Append(details.P_Ledger > 0 ? $" {details.P_Ledger}," : "NULL,");
                cmdString.Append(details.PR_Ledger > 0 ? $" {details.PR_Ledger}," : "NULL,");

                cmdString.Append($" NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");

                cmdString.Append(details.Serial_No.IsValueExits() ? $" {details.Serial_No}," : "NULL,");
                cmdString.Append(details.Batch_No.IsValueExits() ? $" '{details.Batch_No}', '{details.Exp_Date.GetSystemDate()}', '{details.Manu_Date.GetSystemDate()}'," : "NULL,NULL,NULL,");

                cmdString.Append(details.SyncBaseId.IsGuidExits() ? $" '{details.SyncBaseId}'," : "NULL,");
                cmdString.Append(details.SyncGlobalId.IsGuidExits() ? $" '{details.SyncGlobalId}'," : "NULL,");
                cmdString.Append(details.SyncOriginId.IsGuidExits() ? $" '{details.SyncOriginId}'," : "NULL,");

                cmdString.Append($"'{details.SyncCreatedOn.GetSystemDate()}',");
                cmdString.Append($"'{details.SyncLastPatchedOn.GetSystemDate()}',");
                cmdString.Append($" {details.SyncRowVersion},");

                cmdString.Append(details.ReturnChallanNo.IsValueExits() ? $" '{details.ReturnChallanNo}'," : "NULL,");
                cmdString.Append(details.ReturnChallanNo.IsValueExits() ? $" {details.ReturnChallanSno}" : "NULL");
                cmdString.Append(index == DetailsList.Count - 1 ? " ); \n" : "),\n");
            }

            if (Terms != null && Terms.Count > 0)
            {
                if (actionTag.Equals("UPDATE"))
                {
                    cmdString.Append($"DELETE AMS.PR_Term WHERE PR_VNo='{PrMaster.PR_Invoice}' AND Term_Type='B' \n");
                }
                cmdString.Append(@" 
                    INSERT INTO AMS.PR_Term(PR_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES ");
                foreach (var term in Terms)
                {
                    var index = Terms.IndexOf(term);

                    cmdString.Append($" ('{term.PR_VNo}',{term.PT_Id},");
                    cmdString.Append(term.SNo > 0 ? $"{term.SNo}," : "1,");
                    cmdString.Append($" '{term.Term_Type}',");
                    cmdString.Append(term.Product_Id > 0 ? $"{term.Product_Id}," : "NULL,");
                    cmdString.Append($" {term.Rate},{term.Amount},");
                    cmdString.Append($"'{term.Taxable}',");
                    cmdString.Append($" NULL,NULL,NULL,'{term.SyncCreatedOn.GetSystemDate()}','{term.SyncLastPatchedOn.GetSystemDate()}',{term.SyncRowVersion}");
                    cmdString.Append(index == Terms.Count - 1 ? "); \n" : "), \n");
                }
            }

            if (AddInfos != null && AddInfos.Count > 0)
            {
                cmdString.Append($@" 
                    DELETE AMS.ProductAddInfo WHERE VoucherNo='{PrMaster.PR_Invoice}' AND Module='PR';");
                cmdString.Append(@"  
                    INSERT INTO AMS.ProductAddInfo(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES ");

                foreach (var info in AddInfos)
                {
                    var iRows = AddInfos.IndexOf(info);

                    cmdString.Append($"('{info.Module}','{info.VoucherNo}','{info.VoucherType}',{info.ProductId},{info.Sno},");
                    cmdString.Append(info.SizeNo.IsValueExits() ? $" '{info.SizeNo}'," : "NULL,");
                    cmdString.Append(info.SerialNo.IsValueExits() ? $" '{info.SerialNo}'," : "NULL,");
                    cmdString.Append(info.BatchNo.IsValueExits() ? $" '{info.BatchNo}'," : "NULL,");
                    cmdString.Append(info.ChasisNo.IsValueExits() ? $" '{info.ChasisNo}','{info.EngineNo}','{info.VHModel}','{info.VHColor}'," : "NULL,NULL,NULL,NULL,");
                    cmdString.Append($"'{info.MFDate.GetSystemDate()}','{info.ExpDate.GetSystemDate()}',");
                    cmdString.Append($" {info.Mrp}, {info.Rate}, {info.AltQty}, {info.Qty},{info.BranchId},");
                    cmdString.Append(info.CompanyUnitId > 0 ? $" {info.CompanyUnitId}," : $"{ObjGlobal.SysCompanyUnitId},");
                    cmdString.Append($" '{info.EnterBy}', '{info.EnterDate.GetSystemDate()}',");
                    cmdString.Append($" NULL,NULL,NULL,");
                    cmdString.Append($" '{info.SyncCreatedOn.GetSystemDate()}', '{info.SyncLastPatchedOn.GetSystemDate()}',{info.SyncRowVersion}");
                    cmdString.Append(iRows == AddInfos.Count - 1 ? " ); \n" : "),\n");
                }
            }
        }

        var isOk = SqlExtensions.ExecuteNonTrans(cmdString);
        if (isOk == 0)
        {
            return isOk;
        }

        if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null)
        {
            Task.Run(() => SyncPurchaseReturnAsync(actionTag));
        }

        if (!_tagStrings.Contains(actionTag))
        {
            if (actionTag is "SAVE")
            {
                AuditLogPurchaseReturnInvoice(actionTag);
            }
            PurchaseReturnInvoiceTermPosting();
            PurchaseReturnInvoiceAccountDetailsPosting();
            PurchaseReturnInvoiceStockDetailsPosting();
        }

        return isOk;
    }
    private int AuditLogPurchaseReturnInvoice(string actionTag)
    {
        var cmdString = @$"
			INSERT INTO AUD.AUDIT_PR_MASTER(PR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, Vendor_ID, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_ID, Subledger_Id, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_Type, No_Print, In_Words, Remarks, Audit_Lock, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CancelBy, CancelDate, CancelRemarks, CUnit_Id, CBranch_Id, FiscalYearId, Enter_By, Enter_Date, ModifyAction, ModifyBy, ModifyDate)
            SELECT PR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Invoice, PB_Date, PB_Miti, Vendor_ID, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, No_Print, In_Words, Remarks, Audit_Lock, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CancelBy, CancelDate, CancelRemarks, CUnit_Id, CBranch_Id, FiscalYearId, Enter_By, Enter_Date, '{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.PR_Master
            WHERE PR_Invoice='{PrMaster.PR_Invoice}';

            INSERT INTO AUD.AUDIT_PR_DETAILS(PR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PB_Invoice, PB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, ModifyAction, ModifyBy, ModifyDate)
            SELECT PR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PB_Invoice, PB_Sno, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.PR_Details
            WHERE PR_Invoice='{PrMaster.PR_Invoice}';

            INSERT INTO AUD.AUDIT_PR_TERM(PR_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, ModifyAction, ModifyBy, ModifyDate)
            SELECT PR_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.PR_Term
            WHERE PR_VNo='{PrMaster.PR_Invoice}';

            INSERT INTO AUD.AUDIT_PRODUCT_ADD_INFO(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, ModifyAction, ModifyBy, ModifyDate)
            SELECT Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.ProductAddInfo
            WHERE Module='PR' AND VoucherNo='{PrMaster.PR_Invoice}';";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public int PurchaseReturnInvoiceTermPosting()
    {
        var cmdString = @"
			DELETE AMS.PR_Term WHERE Term_Type='BT' ";
        cmdString += PrMaster.PR_Invoice.IsValueExits() ? @$" AND PR_VNo='{PrMaster.PR_Invoice}';" : "";
        cmdString += @"
			INSERT INTO AMS.PR_Term(PR_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT sbt.PR_VNo, sbt.PT_Id,  sd.Invoice_SNo AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion,1) SyncRowVersion, sbt.SyncBaseId
			FROM AMS.PR_Details sd
					LEFT OUTER JOIN AMS.PR_Master sm ON sm.PR_Invoice=sd.PR_Invoice
					LEFT OUTER JOIN AMS.PR_Term sbt ON sd.PR_Invoice=sbt.PR_VNo
			WHERE  sbt.Term_Type='B' AND Product_Id IS NULL AND sbt.Amount > 0 ";
        cmdString += PrMaster.PR_Invoice.IsValueExits() ? $" AND sbt.PR_VNo = '{PrMaster.PR_Invoice}';" : "";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }
    public int PurchaseReturnInvoiceAccountDetailsPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.AccountDetails WHERE Module='PR' ");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? $@" AND Voucher_No ='{PrMaster.PR_Invoice}' " : string.Empty);
        cmdString.Append($@"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
            FROM(SELECT 'PR' AS Module, ROW_NUMBER() OVER (PARTITION BY pm.PR_Invoice ORDER BY pm.PR_Invoice) AS Serial_No, pm.PR_Invoice AS Voucher_No, pm.Invoice_Date AS Voucher_Date, pm.Invoice_Miti AS Voucher_Miti, pm.Invoice_Time AS Voucher_Time, pm.Vendor_ID AS Ledger_ID, {ObjGlobal.PurchaseReturnLedgerId} AS CbLedger_ID, pm.Subledger_Id AS Subleder_ID, pm.Agent_Id AS Agent_ID, pm.Cls1 AS Department_ID1, pm.Cls2 AS Department_ID2, pm.Cls3 AS Department_ID3, pm.Cls4 AS Department_ID4, pm.Cur_Id AS Currency_ID, pm.Cur_Rate AS Currency_Rate, pm.N_Amount AS Debit_Amt, 0 AS Credit_Amt, pm.LN_Amount AS LocalDebit_Amt, 0 AS LocalCredit_Amt, pm.DueDate AS DueDate, pm.DueDays AS DueDays, NULL AS Narration, pm.Remarks AS Remarks, pm.Enter_By AS EnterBy, pm.Enter_Date AS EnterDate, pm.PB_Invoice AS RefNo, pm.PB_Date AS RefDate, pm.Reconcile_By AS Reconcile_By, pm.Reconcile_Date AS Reconcile_Date, pm.Auth_By AS Authorize_By, pm.Auth_Date AS Authorize_Date, pm.Cleared_By AS Clearing_By, pm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, pm.ChqNo AS Cheque_No, pm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, pm.Party_Name AS PartyName, NULL AS PartyLedger_Id, pm.Vat_No AS Party_PanNo, pm.CBranch_Id AS Branch_ID, pm.CUnit_Id AS CmpUnit_ID, pm.FiscalYearId AS FiscalYearId, NULL AS DoctorId, NULL AS PatientId, NULL AS HDepartmentId, pm.SyncGlobalId AS SyncGlobalId, pm.SyncOriginId AS SyncOriginId, pm.SyncCreatedOn AS SyncCreatedOn, SyncLastPatchedOn, pm.SyncRowVersion AS SyncRowVersion, pm.SyncBaseId AS SyncBaseId
                 FROM AMS.PR_Master pm
                 WHERE(pm.R_Invoice=0 AND pm.Action_Type<>'CANCEL')");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? $@" AND pm.PR_Invoice ='{PrMaster.PR_Invoice}' " : string.Empty);
        cmdString.Append(@$"
			UNION ALL
			SELECT 'PR' AS Module, ROW_NUMBER() OVER (PARTITION BY sd.PR_Invoice ORDER BY sd.PR_Invoice) AS Serial_No, sd.PR_Invoice AS Voucher_No, pm.Invoice_Date AS Voucher_Date, pm.Invoice_Miti AS Voucher_Miti, pm.Invoice_Time AS Voucher_Time, ISNULL(p.PSL, {ObjGlobal.PurchaseReturnLedgerId}) Ledger_ID, pm.Vendor_ID AS CbLedger_ID, pm.Subledger_Id AS Subleder_ID, pm.Agent_Id AS Agent_ID, pm.Cls1 AS Department_ID1, pm.Cls2 AS Department_ID2, pm.Cls3 AS Department_ID3, pm.Cls4 AS Department_ID4, pm.Cur_Id AS Currency_ID, pm.Cur_Rate AS Currency_Rate, 0 AS Debit_Amt, SUM(sd.B_Amount) AS Credit_Amt, 0 AS LocalDebit_Amt, SUM(sd.B_Amount)* pm.Cur_Rate AS LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, pm.Enter_By AS EnterBy, pm.Enter_Date AS EnterDate, pm.PB_Invoice AS RefNo, pm.PB_Date AS RefDate, pm.Reconcile_By AS Reconcile_By, pm.Reconcile_Date AS Reconcile_Date, pm.Auth_By AS Authorize_By, pm.Auth_Date AS Authorize_Date, pm.Cleared_By AS Clearing_By, pm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, pm.ChqNo AS Cheque_No, pm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, pm.Party_Name AS PartyName, NULL AS PartyLedger_Id, pm.Vat_No AS Party_PanNo, pm.CBranch_Id AS Branch_ID, pm.CUnit_Id AS CmpUnit_ID, pm.FiscalYearId AS FiscalYearId, NULL AS DoctorId, NULL AS PatientId, NULL AS HDepartmentId, sd.SyncGlobalId AS SyncGlobalId, sd.SyncOriginId AS SyncOriginId, sd.SyncCreatedOn AS SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion AS SyncRowVersion, sd.SyncBaseId AS SyncBaseId
            FROM AMS.PR_Details sd
                 INNER JOIN AMS.PR_Master pm ON sd.PR_Invoice=pm.PR_Invoice
                 INNER JOIN AMS.Product p ON sd.P_Id=p.PID
            WHERE(pm.R_Invoice=0 AND pm.Action_Type<>'CANCEL') ");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? $@" AND sd.PR_Invoice ='{PrMaster.PR_Invoice}' " : " ");
        cmdString.Append(@$"
			GROUP BY ISNULL(p.PSL, {ObjGlobal.PurchaseReturnLedgerId}), sd.PR_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Invoice_Time, pm.Vendor_ID, pm.Subledger_Id, pm.Agent_Id, pm.Cls1, pm.Cls2, pm.Cls3, pm.Cls4, pm.Cur_Id, pm.Cur_Rate, pm.Cur_Rate, pm.DueDate, pm.DueDays, sd.Narration, pm.Remarks, pm.Enter_By, pm.Enter_Date, pm.PB_Invoice, pm.PB_Date, pm.Reconcile_By, pm.Reconcile_Date, pm.Auth_By, pm.Auth_Date, pm.Cleared_By, pm.Cleared_Date, pm.ChqNo, pm.ChqDate, pm.Party_Name, pm.Vat_No, pm.CBranch_Id, pm.CUnit_Id, pm.FiscalYearId, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId
			UNION ALL
			SELECT 'PR' AS Module, ROW_NUMBER() OVER (PARTITION BY st.PR_VNo ORDER BY st.PR_VNo) AS Serial_No, st.PR_VNo AS Voucher_No, pm.Invoice_Date AS Voucher_Date, pm.Invoice_Miti AS Voucher_Miti, pm.Invoice_Time AS Voucher_Time, st1.Ledger AS Ledger_ID, pm.Vendor_ID AS CbLedger_ID, pm.Subledger_Id AS Subleder_ID, pm.Agent_Id AS Agent_ID, pm.Cls1 AS Department_ID1, pm.Cls2 AS Department_ID2, pm.Cls3 AS Department_ID3, pm.Cls4 AS Department_ID4, pm.Cur_Id AS Currency_ID, pm.Cur_Rate AS Currency_Rate, SUM(CASE WHEN st1.PT_Sign='-' THEN st.Amount ELSE 0 END) AS Debit_Amt, SUM(CASE WHEN st1.PT_Sign='+' THEN st.Amount ELSE 0 END) AS Credit_Amt, SUM(CASE WHEN st1.PT_Sign='-' THEN st.Amount ELSE 0 END)* pm.Cur_Rate AS LocalDebit_Amt, SUM(CASE WHEN st1.PT_Sign='+' THEN st.Amount ELSE 0 END)* pm.Cur_Rate AS LocalCredit_Amt, DueDate, DueDays, NULL AS Narration, pm.Remarks, pm.Enter_By AS EnterBy, pm.Enter_Date AS EnterDate, pm.PB_Invoice AS RefNo, pm.PB_Date AS RefDate, pm.Reconcile_By AS Reconcile_By, pm.Reconcile_Date AS Reconcile_Date, pm.Auth_By AS Authorize_By, pm.Auth_Date AS Authorize_Date, pm.Cleared_By AS Clearing_By, pm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, pm.ChqNo AS Cheque_No, pm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, pm.Party_Name AS PartyName, NULL AS PartyLedger_Id, pm.Vat_No AS Party_PanNo, pm.CBranch_Id AS Branch_ID, pm.CUnit_Id AS CmpUnit_ID, pm.FiscalYearId AS FiscalYearId, NULL AS DoctorId, NULL AS PatientId, NULL AS HDepartmentId, st.SyncGlobalId AS SyncGlobalId, st.SyncOriginId AS SyncOriginId, st.SyncCreatedOn AS SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion AS SyncRowVersion, st.SyncBaseId AS SyncBaseId
            FROM AMS.PR_Term st
                 LEFT OUTER JOIN AMS.PR_Master pm ON st.PR_VNo=pm.PR_Invoice
                 LEFT OUTER JOIN AMS.PT_Term st1 ON st.PT_Id=st1.PT_ID
            WHERE st.Term_Type IN ('B','P')AND(pm.R_Invoice=0 AND pm.Action_Type<>'CANCEL') ");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? $@" AND st.PR_VNo ='{PrMaster.PR_Invoice}' " : " ");
        cmdString.Append(@"
			GROUP BY st.PR_VNo, pm.Invoice_Date, pm.Invoice_Miti, pm.Invoice_Time, pm.Vendor_ID, pm.Subledger_Id, pm.Agent_Id, pm.Cls1, pm.Cls2, pm.Cls3, pm.Cls4, pm.Cur_Id, pm.Cur_Rate, DueDate, DueDays, st1.PT_Sign, Remarks, pm.Enter_By, pm.Enter_Date, pm.PB_Invoice, pm.PB_Date, pm.Reconcile_By, pm.Reconcile_Date, pm.Auth_By, pm.Auth_Date, pm.Cleared_By, pm.Cleared_Date, pm.ChqNo, pm.ChqDate, pm.Party_Name, pm.Vat_No, pm.CBranch_Id, pm.CUnit_Id, pm.FiscalYearId, st.SyncGlobalId, st.SyncOriginId, st.SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion, st.SyncBaseId, st1.Ledger) Account;");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return isResult;
    }
    public int PurchaseReturnInvoiceStockDetailsPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(" DELETE AMS.StockDetails WHERE module='PR' \n");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? $" AND Voucher_No='{PrMaster.PR_Invoice}'; \n" : " ");
        cmdString.Append(PrMaster.ReturnChallanNo.IsValueExits()
            ? $@" DELETE AMS.StockDetails WHERE module ='PCR' AND Voucher_No='{PrMaster.ReturnChallanNo}';"
            : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
            SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, stockval, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
            FROM(SELECT 'PR' Module, pd.PR_Invoice Voucher_No, pd.Invoice_SNo Serial_No, pr.PB_Invoice PurRefVno, pr.Invoice_Date Voucher_Date, pr.Invoice_Miti Voucher_Miti, pr.Invoice_Time Voucher_Time, pr.Vendor_ID Ledger_Id, pr.Subledger_Id Subledger_Id, pr.Agent_Id Agent_Id, pr.Cls1 Department_Id1, pr.Cls2 Department_Id2, pr.Cls3 Department_Id3, pr.Cls4 Department_Id4, pr.Cur_Id Currency_Id, pr.Cur_Rate Currency_Rate, pd.P_Id Product_Id, pd.Gdn_Id Godown_Id, NULL CostCenter_Id, pd.Alt_Qty AltQty, pd.Alt_UnitId AltUnit_Id, pd.Qty Qty, pd.Unit_Id Unit_Id, pd.AltStock_Qty AltStockQty, pd.Stock_Qty StockQty, pd.Free_Qty FreeQty, pd.Free_Unit_Id FreeUnit_Id, pd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, pd.Rate Rate, pd.B_Amount BasicAmt, pd.T_Amount TermAmt, pd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, pd.V_Rate TaxRate, pd.Tax_Amount TaxableAmt, (pd.N_Amount+ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (pd.N_Amount+ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, pr.PB_Invoice PartyInv, 'O' EntryType, pr.Auth_By AuthBy, pr.Auth_Date AuthDate, pr.Reconcile_By RecoBy, pr.Reconcile_Date RecoDate, NULL Counter_Id, NULL RoomNo, pr.Enter_By EnterBy, pr.Enter_Date EnterDate, pr.CBranch_Id Branch_Id, pr.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, pd.Rate SalesRate, pr.FiscalYearId, pr.SyncGlobalId, pr.SyncOriginId, pr.SyncCreatedOn, pr.SyncLastPatchedOn, pr.SyncRowVersion, pr.SyncBaseId
                 FROM AMS.PR_Details pd
                      INNER JOIN AMS.Product p ON p.PID=pd.P_Id
                      INNER JOIN AMS.PR_Master pr ON pd.PR_Invoice=pr.PR_Invoice
                      LEFT OUTER JOIN(SELECT st.Product_Id, st.PR_VNo, st.SNo, SUM(CASE WHEN st1.PT_Sign='-' THEN -st.Amount ELSE st.Amount END) StockValue
                                      FROM AMS.PR_Term st
                                           LEFT OUTER JOIN AMS.PT_Term st1 ON st.PT_Id=st1.PT_ID
                                      WHERE st1.PT_Profitability>0 AND st.Term_Type<>'B' ");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? $@" AND st.PR_VNo = '{PrMaster.PR_Invoice}' " : " ");
        cmdString.Append(@"
					 GROUP BY st.Product_Id, st.PR_VNo, st.SNo) stockVal ON stockVal.Product_Id=pd.P_Id AND pd.PR_Invoice=stockVal.PR_VNo AND pd.Invoice_SNo=stockVal.SNo
				WHERE p.PType IN ('I', 'Inventory')AND pr.R_Invoice=0 AND pr.Action_type<>'CANCEL' ");
        cmdString.Append(PrMaster.PR_Invoice.IsValueExits() ? @$" AND pd.PR_Invoice = '{PrMaster.PR_Invoice}' " : " ");
        cmdString.Append(@" ) AS Stock ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (isResult == 0)
        {
            return isResult;
        }

        if (PrMaster.ReturnChallanNo.IsValueExits())
        {
            var update = @"
                UPDATE AMS.StockDetails SET Adj_VoucherNo = SD.SC_Invoice, Adj_Qty = sd.Qty, Adj_Module = 'PCR' FROM AMS.SB_Details sd WHERE Voucher_No = sd.SB_Invoice AND Product_Id = sd.P_Id ";
            update += PrMaster.PR_Invoice.IsValueExits() ? $" AND Voucher_No='{PrMaster.PR_Invoice}';" : " ";
            var ok = SqlExtensions.ExecuteNonQuery(update);
        }

        return isResult;
    }
    public async Task<int> SyncPurchaseReturnAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}PurchaseReturn/GetUnSynchronizedPurchaseReturns",
            InsertUrl = @$"{_configParams.Model.Item2}PurchaseReturn/InsertPurchaseReturnList",
            UpdateUrl = @$"{_configParams.Model.Item2}PurchaseReturn/UpdatePurchaseReturn"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var purchaseReturnRepo = DataSyncProviderFactory.GetRepository<PR_Master>(_injectData);
        // pull all new purchase return
        if (ObjGlobal.IsPurchaseReturnSync)
        {
            var result = await PullUnSyncPurchaseReturn(purchaseReturnRepo);
            if (!result)
                return 0;
        }
        // push all new purchase return
        var sqlQuery = @"SELECT *FROM AMS.PR_Master WHERE ISNULL(IsSynced,0)=0";
        var queryResponse = await QueryUtils.GetListAsync<PR_Master>(sqlQuery);
        var prList = queryResponse.List.ToList();
        if (prList.Count > 0)
        {
            var loopCount = 1;
            if (prList.Count > 100)
            {
                loopCount = prList.Count / 100 + 1;
            }
            var newPrList = new List<PR_Master>();
            var cmdString = new StringBuilder();
            var purchaseReturnList = new List<PR_Master>();
            for (var i = 0; i < loopCount; i++)
            {
                newPrList.Clear();
                newPrList.AddRange(i == 0 ? prList.Take(100) : prList.Skip(100 * i).Take(100));
                purchaseReturnList.Clear();
                foreach (var pr in newPrList)
                {
                    sqlQuery = $@"SELECT *FROM AMS.PR_Details WHERE PR_Invoice='{pr.PR_Invoice}'";
                    var dtlQueryResponse = await QueryUtils.GetListAsync<PR_Details>(sqlQuery);
                    var prdList = dtlQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.PR_Term WHERE PR_VNo='{pr.PR_Invoice}'";
                    var pcTermQueryResponse = await QueryUtils.GetListAsync<PR_Term>(sqlQuery);
                    var prTermList = pcTermQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.ProductAddInfo WHERE VoucherNo='{pr.PR_Invoice}' AND Module='PR'";
                    var pAddInfoQueryResponse = await QueryUtils.GetListAsync<ProductAddInfo>(sqlQuery);
                    var prdAddInfoList = pAddInfoQueryResponse.List.ToList();

                    var prMaster = new PR_Master();
                    prMaster = pr;
                    prMaster.DetailsList = prdList;
                    prMaster.Terms = prTermList;
                    prMaster.ProductAddInfos = prdAddInfoList;
                    purchaseReturnList.Add(prMaster);
                }
                var pushResponse = await purchaseReturnRepo.PushNewListAsync(purchaseReturnList);
                if (!pushResponse.Value)
                {
                    SplashScreenManager.CloseForm();
                    pushResponse.ShowErrorDialog();
                    return 0;
                }
                else
                {
                    cmdString.Clear();
                    foreach (var pr in newPrList)
                    {
                        cmdString.Append($"UPDATE AMS.PR_Master SET IsSynced = 1 WHERE PR_Invoice = '{pr.PR_Invoice}';\n");
                    }

                    var result = await QueryUtils.ExecNonQueryAsync(cmdString.ToString());
                }
            }
        }

        return 1;
    }
    #endregion


    // PULL PURCHASE RETURN INVOICE
    #region ---------- PULL PURCHASE RETURN INVOICE ----------
    private async Task<bool> PullUnSyncPurchaseReturn(IDataSyncRepository<PR_Master> purchaseReturnRepo)
    {
        try
        {
            var pullResponse = await purchaseReturnRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    // _purchaseDataEntry.SaveUnSyncPurchaseReturnFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncPurchaseReturn(purchaseReturnRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // RETURN VALUE IN SHORT 
    #region  --------------- RETURN VALUE IN SHORT ---------------
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


    // RETURN VALUE IN DATA TABLE
    #region --------------- RETURN VALUE IN DATA TABLE ---------------
    public DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        throw new System.NotImplementedException();
    }
    public DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo)
    {
        throw new System.NotImplementedException();
    }
    #endregion


    // OBJECT FOR THIS CLASS
    #region --------------- OBJECT FOR THIS CLASS ---------------
    public PR_Master PrMaster { get; set; } = new();
    public List<PR_Details> DetailsList { get; set; }
    public List<PR_Term> Terms { get; set; }
    public List<ProductAddInfo> AddInfos { get; set; }
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

    // STRING VALUE
    private readonly string[] _tagStrings = { "DELETE", "UPDATE", "REVERSE" };
    #endregion

}