using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.PurchaseMaster;

public class PurchaseInvoiceRepository : IPurchaseInvoice
{
    // METHODS FOR THIS FORM
    public int SavePurchaseInvoice(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (!actionTag.Equals("SAVE"))
        {
            AuditLogPurchaseInvoice(actionTag);
        }
        if (actionTag is "DELETE" or "UPDATE" or "REVERSE")
        {
            cmdString.Append($@" 
                DELETE AMS.StockDetails WHERE Module='PB' AND Voucher_No= '{PbMaster.PB_Invoice}';");
            cmdString.Append($@" 
                DELETE AMS.AccountDetails WHERE Module ='PB' AND Voucher_No = '{PbMaster.PB_Invoice}';");
            if (actionTag != "REVERSE")
            {
                cmdString.Append($"\n DELETE AMS.PB_Term WHERE PB_VNo = '{PbMaster.PB_Invoice}'; \n");
                cmdString.Append($" DELETE AMS.PB_Details WHERE PB_Invoice='{PbMaster.PB_Invoice}'; \n");
            }

            if (actionTag is "DELETE")
            {
                cmdString.Append($"\n DELETE AMS.PB_Master WHERE PB_Invoice ='{PbMaster.PB_Invoice}'; \n");
                cmdString.Append($" DELETE AMS.PB_OtherMaster WHERE PAB_Invoice='{PbMaster.PB_Invoice}'; \n");
                if (!string.IsNullOrWhiteSpace(PbMaster.PC_Invoice))
                {
                    cmdString.Append($@" 
                        UPDATE AMS.PC_Master SET Invoice_Type = 'PENDING' WHERE PC_Invoice='{PbMaster.PC_Invoice}';");
                }
            }
        }

        if (actionTag.ToUpper() is "SAVE")
        {
            cmdString.Append("INSERT INTO AMS.PB_Master(PB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, PO_Invoice, PO_Date, PC_Invoice, PC_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, R_Invoice, CancelBy, CancelDate, CancelRemarks, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append($" VALUES (N'{PbMaster.PB_Invoice}','{PbMaster.Invoice_Date.GetSystemDate()}',N'{PbMaster.Invoice_Miti}',GETDATE(), ");
            cmdString.Append(PbMaster.PB_Vno.Trim().Length > 0 ? $"'{PbMaster.PB_Vno}','{PbMaster.Vno_Date.GetSystemDate()}','{PbMaster.Vno_Miti}'," : "NULL,NULL,NULL,");
            cmdString.Append($"{PbMaster.Vendor_ID},");
            cmdString.Append(PbMaster.PartyLedgerId > 0 ? $"{PbMaster.PartyLedgerId}," : "NULL,");
            cmdString.Append(PbMaster.Party_Name.Trim().Length > 0 ? $"'{PbMaster.Party_Name}'," : "NULL,");
            cmdString.Append(PbMaster.Vat_No.Trim().Length > 0 ? $"'{PbMaster.Vat_No}'," : "NULL,");
            cmdString.Append(PbMaster.Contact_Person.Trim().Length > 0 ? $"'{PbMaster.Contact_Person}'," : "NULL,");
            cmdString.Append(PbMaster.Mobile_No.Trim().Length > 0 ? $"'{PbMaster.Mobile_No}'," : "NULL,");
            cmdString.Append(PbMaster.Address.Trim().Length > 0 ? $"'{PbMaster.Address}'," : "NULL,");
            cmdString.Append(PbMaster.ChqNo.Trim().Length > 0 ? $"'{PbMaster.ChqNo}'," : "NULL,");
            cmdString.Append(PbMaster.ChqNo.Trim().Length > 0 ? $"'{PbMaster.ChqDate}'," : "NULL,");
            cmdString.Append(PbMaster.ChqNo.Trim().Length > 0 ? $"'{PbMaster.ChqMiti}'," : "NULL,");
            cmdString.Append(PbMaster.Invoice_Type.Trim().Length > 0 ? $"'{PbMaster.Invoice_Type}'," : "'NORMAL',");
            cmdString.Append(PbMaster.Invoice_In.Trim().Length > 0 ? $"'{PbMaster.Invoice_In}'," : "'CREDIT',");
            cmdString.Append($"'{PbMaster.DueDays.GetInt()}',");
            cmdString.Append(PbMaster.DueDays > 0 ? $"'{PbMaster.DueDate.GetSystemDate()}'," : "NULL,");
            cmdString.Append(PbMaster.Agent_Id > 0 ? $"{PbMaster.Agent_Id}," : "NULL,");
            cmdString.Append(PbMaster.Subledger_Id > 0 ? $"{PbMaster.Subledger_Id}," : "NULL,");
            cmdString.Append(PbMaster.PO_Invoice.Trim().Length > 0 ? $"'{PbMaster.PO_Invoice}'," : "NULL,");
            cmdString.Append(PbMaster.PO_Invoice.Trim().Length > 0 ? $"'{PbMaster.PO_Date.GetSystemDate()}'," : "NULL,");
            cmdString.Append(PbMaster.PC_Invoice.Trim().Length > 0 ? $"'{PbMaster.PC_Invoice}'," : "NULL,");
            cmdString.Append(PbMaster.PC_Invoice.Trim().Length > 0 ? $"'{PbMaster.PC_Date.GetSystemDate()}'," : "NULL,");
            cmdString.Append(PbMaster.Cls1 > 0 ? $"{PbMaster.Cls1}," : "NULL,");
            cmdString.Append("NULL,NULL,NULL,");
            cmdString.Append(PbMaster.Cur_Id > 0 ? $"{PbMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append($"{PbMaster.Cur_Rate.GetDecimal(true)},");
            cmdString.Append(PbMaster.Counter_ID > 0 ? $"{PbMaster.Counter_ID}," : "NULL,");
            cmdString.Append($"{PbMaster.B_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.T_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.N_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.LN_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.Tender_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.Change_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.V_Amount.GetDecimal()},");
            cmdString.Append($"{PbMaster.Tbl_Amount.GetDecimal()},");
            cmdString.Append(PbMaster.Action_type.Trim().Length > 0 ? $"'{PbMaster.Action_type}'," : "'SAVE',");
            cmdString.Append("0,NULL,NULL,NULL,0,");
            cmdString.Append(PbMaster.In_Words.Trim().Length > 0 ? $"'{PbMaster.In_Words.Trim().Replace("'", "''")}'," : "NULL,");
            cmdString.Append(PbMaster.Remarks.Trim().Length > 0 ? $"'{PbMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}',GETDATE(),NULL,NULL,NULL,NULL,NULL,NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
            cmdString.Append($"NULL,NULL,NULL,NULL,NULL,NUll,NULL,NULL,NULL,{PbMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (actionTag.ToUpper() is "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.PB_Master  SET \n");
            cmdString.Append($" Invoice_Date = '{PbMaster.Invoice_Date:yyyy-MM-dd}',Invoice_Miti = N'{PbMaster.Invoice_Miti}', ");
            cmdString.Append(PbMaster.PB_Vno.Trim().Length > 0 ? $"PB_Vno = '{PbMaster.PB_Vno}',Vno_Date = '{PbMaster.Vno_Date:yyyy-MM-dd}',Vno_Miti = '{PbMaster.Vno_Miti}'," : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
            cmdString.Append($"Vendor_ID = {PbMaster.Vendor_ID},");
            cmdString.Append(PbMaster.PartyLedgerId.GetInt() > 0 ? $"PartyLedgerId = '{PbMaster.PartyLedgerId}'," : "PartyLedgerId = NULL,");
            cmdString.Append(PbMaster.Party_Name.Trim().Length > 0 ? $"Party_Name = '{PbMaster.Party_Name}'," : "Party_Name = NULL,");
            cmdString.Append(PbMaster.Vat_No.Trim().Length > 0 ? $"Vat_No = '{PbMaster.Vat_No}'," : "Vat_No = NULL,");
            cmdString.Append(PbMaster.Contact_Person.Trim().Length > 0 ? $"Contact_Person = '{PbMaster.Contact_Person}'," : "Contact_Person = NULL,");
            cmdString.Append(PbMaster.Mobile_No.Trim().Length > 0 ? $"Mobile_No = '{PbMaster.Mobile_No}'," : "Mobile_No = NULL,");
            cmdString.Append(PbMaster.Address.Trim().Length > 0 ? $"Address = '{PbMaster.Address}'," : "Address = NULL,");
            cmdString.Append(PbMaster.ChqNo.Trim().Length > 0 ? $"ChqNo = '{PbMaster.ChqNo}'," : "ChqNo = NULL,");
            cmdString.Append(PbMaster.ChqNo.Trim().Length > 0 ? $"ChqDate = '{PbMaster.ChqDate}'," : "ChqDate = NULL,");
            cmdString.Append(PbMaster.Invoice_Type.Trim().Length > 0 ? $"Invoice_Type = '{PbMaster.Invoice_Type}'," : "Invoice_Type = 'Normal',");
            cmdString.Append(PbMaster.Invoice_In.Trim().Length > 0 ? $"Invoice_In = '{PbMaster.Invoice_In}'," : "Invoice_In = 'Credit',");
            cmdString.Append(PbMaster.DueDays > 0 ? $"DueDays = '{PbMaster.DueDays}'," : "DueDays = 0,");
            cmdString.Append(PbMaster.DueDays > 0 ? $"DueDate = '{PbMaster.DueDate:yyyy-MM-dd}'," : "DueDate = NULL,");
            cmdString.Append(PbMaster.Agent_Id > 0 ? $"Agent_Id = {PbMaster.Agent_Id}," : "Agent_Id = NULL,");
            cmdString.Append(PbMaster.Subledger_Id > 0 ? $"Subledger_Id = {PbMaster.Subledger_Id}," : "Subledger_Id = NULL,");
            cmdString.Append(PbMaster.PO_Invoice.Trim().Length > 0 ? $"PO_Invoice = '{PbMaster.PO_Invoice}'," : "PO_Invoice = NULL,");
            cmdString.Append(PbMaster.PO_Invoice.Trim().Length > 0 ? $"PO_Date = '{PbMaster.PO_Date:yyyy-MM-dd}'," : "PO_Date = NULL,");
            cmdString.Append(PbMaster.PC_Invoice.Trim().Length > 0 ? $"PC_Invoice = '{PbMaster.PC_Invoice}'," : "PC_Invoice = NULL,");
            cmdString.Append(PbMaster.PC_Invoice.Trim().Length > 0 ? $"PC_Date = '{PbMaster.PC_Date:yyyy-MM-dd}'," : "PC_Date = NULL,");
            cmdString.Append(PbMaster.Cls1 > 0 ? $"Cls1 = {PbMaster.Cls1}," : "Cls1 = NULL,");
            cmdString.Append(PbMaster.Cur_Id > 0 ? $"Cur_Id = {PbMaster.Cur_Id}," : "Cur_Id = 1,");
            cmdString.Append(PbMaster.Cur_Rate > 0 ? $"Cur_Rate = {PbMaster.Cur_Rate}," : "Cur_Rate = 1,");
            cmdString.Append(PbMaster.Counter_ID > 0 ? $"Counter_ID = {PbMaster.Counter_ID}," : "Counter_ID = NULL,");
            cmdString.Append(PbMaster.B_Amount > 0 ? $"B_Amount = {PbMaster.B_Amount}," : "B_Amount = 0,");
            cmdString.Append(PbMaster.T_Amount > 0 ? $"T_Amount = {PbMaster.T_Amount}," : "T_Amount = 0,");
            cmdString.Append(PbMaster.N_Amount > 0 ? $"N_Amount = {PbMaster.N_Amount}," : "N_Amount = 0,");
            cmdString.Append(PbMaster.LN_Amount > 0 ? $"LN_Amount = {PbMaster.LN_Amount}," : "LN_Amount = 0,");
            cmdString.Append(PbMaster.Tender_Amount > 0 ? $"Tender_Amount = {PbMaster.Tender_Amount}," : "Tender_Amount= 0,");
            cmdString.Append(PbMaster.Change_Amount > 0 ? $"Change_Amount = {PbMaster.Change_Amount}," : "Change_Amount = 0,");
            cmdString.Append(PbMaster.V_Amount > 0 ? $"V_Amount = {PbMaster.V_Amount}," : "V_Amount = 0,");
            cmdString.Append(PbMaster.Tbl_Amount > 0 ? $"Tbl_Amount = {PbMaster.Tbl_Amount}," : "Tbl_Amount = 0,");
            cmdString.Append(PbMaster.Action_type.Trim().Length > 0 ? $"Action_type = '{PbMaster.Action_type}'," : "Action_type = 'UPDATE',");
            cmdString.Append(PbMaster.In_Words.Trim().Length > 0 ? $"In_Words = '{PbMaster.In_Words}'," : "In_Words = NULL,");
            cmdString.Append(PbMaster.Remarks.Trim().Length > 0 ? $"Remarks = '{PbMaster.Remarks.Trim().Replace("'", "''")}' ," : "Remarks = NULL,");
            cmdString.Append("IsSynced=0");
            cmdString.Append($" WHERE PB_Invoice = N'{PbMaster.PB_Invoice}'; \n");
        }

        if (actionTag.ToUpper() != "DELETE" && !actionTag.Equals("REVERSE"))
        {
            //if (PbMaster.GetView is { RowCount: > 0 })
            if (DetailsList.Count > 0)
            {
                var iRows = 0;
                cmdString.Append(@" 
                    INSERT INTO AMS.PB_Details(PB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, PC_Invoice, PC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, TaxExempted_Amount, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                cmdString.Append("\n VALUES \n");
                foreach (var dr in DetailsList)
                {
                    cmdString.Append($"('{dr.PB_Invoice}',");
                    cmdString.Append($"{dr.Invoice_SNo},");
                    cmdString.Append($"{dr.P_Id},");
                    cmdString.Append(dr.Gdn_Id > 0 ? $"{dr.Gdn_Id}," : "NULL,");
                    cmdString.Append(dr.Alt_Qty > 0 ? $"{dr.Alt_Qty}," : "0,");
                    cmdString.Append(dr.Alt_UnitId > 0 ? $"{dr.Alt_UnitId}," : "NULL,");
                    cmdString.Append(dr.Qty > 0 ? $"{dr.Qty}," : "1,");
                    cmdString.Append(dr.Unit_Id > 0 ? $"{dr.Unit_Id}," : "NULL,");
                    cmdString.Append($"{dr.Rate},");
                    cmdString.Append($"{dr.B_Amount},");
                    cmdString.Append($"{dr.T_Amount},");
                    cmdString.Append($"{dr.N_Amount},");
                    cmdString.Append($"{dr.AltStock_Qty},");
                    cmdString.Append($"{dr.Stock_Qty},");
                    cmdString.Append(dr.Narration.IsValueExits() ? $"'{dr.Narration}'," : "NULL,");
                    cmdString.Append(dr.PO_Invoice.IsValueExits() ? $"'{dr.PO_Invoice}'," : "NULL,");
                    cmdString.Append($"'{dr.PO_Sno}',");
                    cmdString.Append(dr.PC_Invoice.IsValueExits() ? $"'{dr.PC_Invoice}'," : "NULL,");
                    cmdString.Append($"'{dr.PC_SNo}',");
                    cmdString.Append($"{dr.Tax_Amount}, {dr.V_Amount}, {dr.V_Rate}, 0, 0, 0, 0, 0, 0,");
                    cmdString.Append(dr.T_Product ? "1," : "0,");
                    cmdString.Append(dr.P_Ledger > 0 ? $"{dr.P_Ledger}," : "NULL,");
                    cmdString.Append(dr.PR_Ledger > 0 ? $"{dr.PR_Ledger}," : "NULL,");
                    cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
                    cmdString.Append($"NULL,NULL,0,NULL,NULL,NULL,'{PbMaster.SyncCreatedOn.GetSystemDate()}','{PbMaster.SyncLastPatchedOn.GetSystemDate()}',{PbMaster.SyncRowVersion.GetDecimal(true)} ");
                    cmdString.Append(iRows == DetailsList.Count - 1 ? " ); \n" : "),\n");
                    iRows++;
                }
            }

            if (Terms is { Count: > 0 })
            {
                if (actionTag.Equals("UPDATE"))
                {
                    cmdString.Append($"DELETE AMS.PB_Term WHERE PB_VNo='{PbMaster.PB_Invoice}' AND Term_Type='B' \n");
                }
                cmdString.Append(@" 
                    INSERT INTO AMS.PB_Term(PB_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES ");
                foreach (var term in Terms)
                {
                    var iRows = Terms.IndexOf(term);
                    cmdString.Append($" ('{term.PB_VNo}',{term.PT_Id},");
                    cmdString.Append(term.SNo > 0 ? $"{term.SNo}," : "1,");
                    cmdString.Append($" '{term.Term_Type}',");
                    cmdString.Append(term.Product_Id > 0 ? $"{term.Product_Id}," : "NULL,");
                    cmdString.Append($" {term.Rate},{term.Amount},");
                    cmdString.Append($"'{term.Taxable}',");
                    cmdString.Append($" NULL,NULL,NULL,'{term.SyncCreatedOn.GetSystemDate()}','{term.SyncLastPatchedOn.GetSystemDate()}',{term.SyncRowVersion} ");
                    cmdString.Append(iRows == Terms.Count - 1 ? " ); \n" : "),\n");
                }
            }

            if (AddInfos is { Count: > 0 })
            {
                cmdString.Append($@" 
                    DELETE AMS.ProductAddInfo WHERE VoucherNo='{PbMaster.PB_Invoice}' AND Module='PB';");
                cmdString.Append(@"  
                    INSERT INTO AMS.ProductAddInfo(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                cmdString.Append("\n VALUES \n");

                foreach (var info in AddInfos)
                {
                    var iRows = AddInfos.IndexOf(info);

                    cmdString.Append($"('{info.Module}','{info.VoucherNo}','{info.VoucherType}',{info.ProductId},{info.Sno},");
                    cmdString.Append(info.SizeNo.IsValueExits() ? $" '{info.SizeNo}'," : "NULL,");
                    cmdString.Append(info.SerialNo.IsValueExits() ? $" '{info.SerialNo}'," : "NULL,");
                    cmdString.Append(info.BatchNo.IsValueExits() ? $" '{info.BatchNo}'," : "NULL,");
                    cmdString.Append(info.ChasisNo.IsValueExits() ? $" '{info.ChasisNo}','{info.EngineNo}',{info.VHModel},'{info.VHColor}'," : "NULL,NULL,NULL,NULL,");
                    cmdString.Append($"'{info.MFDate.GetSystemDate()}','{info.ExpDate.GetSystemDate()}',");
                    cmdString.Append($" {info.Mrp}, {info.Rate}, {info.AltQty}, {info.Qty},{info.BranchId},");
                    cmdString.Append(info.CompanyUnitId > 0 ? $" {info.CompanyUnitId}," : "NULL,");
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
            Task.Run(() => SyncPurchaseInvoiceAsync(actionTag));
        }
        if (!_tagStrings.Contains(actionTag))
        {
            if (actionTag is "SAVE")
            {
                AuditLogPurchaseInvoice(actionTag);
            }
            PurchaseInvoiceTermPosting();
            if (PbMaster.PC_Invoice.IsValueExits())
            {
                var cmdTxt = $@"
					UPDATE AMS.PC_Master SET Invoice_Type = 'POSTED' WHERE PC_Invoice='{PbMaster.PC_Invoice}'; ";
                SqlExtensions.ExecuteNonQuery(cmdTxt);
            }

            PurchaseInvoiceAccountDetailsPosting();
            PurchaseInvoiceStockDetailsPosting();
        }

        return isOk;
    }
    private int AuditLogPurchaseInvoice(string actionTag)
    {
        var cmdString = @$"
            INSERT INTO [AUD].[AUDIT_PB_MASTER]([PB_Invoice], [Invoice_Date], [Invoice_Miti], [Invoice_Time], [PB_Vno], [Vno_Date], [Vno_Miti], [Vendor_ID], [Party_Name], [Vat_No], [Contact_Person], [Mobile_No], [Address], [ChqNo], [ChqDate], [Invoice_Type], [Invoice_In], [DueDays], [DueDate], [Agent_ID], [Subledger_Id], [PO_Invoice], [PO_Date], [PC_Invoice], [PC_Date], [Cls1], [Cls2], [Cls3], [Cls4], [Cur_Id], [Cur_Rate], [Counter_ID], [B_Amount], [T_Amount], [N_Amount], [LN_Amount], [Tender_Amount], [Change_Amount], [V_Amount], [Tbl_Amount], [Action_Type], [R_Invoice], [No_Print], [In_Words], [Remarks], [Audit_Lock], [Reconcile_By], [Reconcile_Date], [Auth_By], [Auth_Date], [Cleared_By], [Cleared_Date], [CancelBy], [CancelDate], [CancelRemarks], [CUnit_Id], [CBranch_Id], [FiscalYearId], [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5], [Enter_By], [Enter_Date], [ModifyAction], [ModifyBy], [ModifyDate])
            SELECT [PB_Invoice], [Invoice_Date], [Invoice_Miti], [Invoice_Time], [PB_Vno], [Vno_Date], [Vno_Miti], [Vendor_ID], [Party_Name], [Vat_No], [Contact_Person], [Mobile_No], [Address], [ChqNo], [ChqDate], [Invoice_Type], [Invoice_In], [DueDays], [DueDate], [Agent_Id], [Subledger_Id], [PO_Invoice], [PO_Date], [PC_Invoice], [PC_Date], [Cls1], [Cls2], [Cls3], [Cls4], [Cur_Id], [Cur_Rate], [Counter_ID], [B_Amount], [T_Amount], [N_Amount], [LN_Amount], [Tender_Amount], [Change_Amount], [V_Amount], [Tbl_Amount], [Action_type], [R_Invoice], [No_Print], [In_Words], [Remarks], [Audit_Lock], [Reconcile_By], [Reconcile_Date], [Auth_By], [Auth_Date], [Cleared_By], [Cleared_Date], [CancelBy], [CancelDate], [CancelRemarks], [CUnit_Id], [CBranch_Id], [FiscalYearId], [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5], [Enter_By], [Enter_Date], '   {actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.[PB_Master]
            WHERE PB_Invoice='{PbMaster.PB_Invoice}';

            INSERT INTO [AUD].[AUDIT_PB_DETAILS]([PB_Invoice], [Invoice_SNo], [P_Id], [Gdn_Id], [Alt_Qty], [Alt_UnitId], [Qty], [Unit_Id], [Rate], [B_Amount], [T_Amount], [N_Amount], [AltStock_Qty], [Stock_Qty], [Narration], [PO_Invoice], [PO_Sno], [PC_Invoice], [PC_SNo], [Tax_Amount], [V_Amount], [V_Rate], [Free_Unit_Id], [Free_Qty], [StockFree_Qty], [ExtraFree_Unit_Id], [ExtraFree_Qty], [ExtraStockFree_Qty], [T_Product], [P_Ledger], [PR_Ledger], [SZ1], [SZ2], [SZ3], [SZ4], [SZ5], [SZ6], [SZ7], [SZ8], [SZ9], [SZ10], [Serial_No], [Batch_No], [Exp_Date], [Manu_Date], [TaxExempted_Amount], [ModifyAction], [ModifyBy], [ModifyDate])
            SELECT [PB_Invoice], [Invoice_SNo], [P_Id], [Gdn_Id], [Alt_Qty], [Alt_UnitId], [Qty], [Unit_Id], [Rate], [B_Amount], [T_Amount], [N_Amount], [AltStock_Qty], [Stock_Qty], [Narration], [PO_Invoice], [PO_Sno], [PC_Invoice], [PC_SNo], [Tax_Amount], [V_Amount], [V_Rate], [Free_Unit_Id], [Free_Qty], [StockFree_Qty], [ExtraFree_Unit_Id], [ExtraFree_Qty], [ExtraStockFree_Qty], [T_Product], [P_Ledger], [PR_Ledger], [SZ1], [SZ2], [SZ3], [SZ4], [SZ5], [SZ6], [SZ7], [SZ8], [SZ9], [SZ10], [Serial_No], [Batch_No], [Exp_Date], [Manu_Date], [TaxExempted_Amount], '   {actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.[PB_Details]
            WHERE PB_Invoice='{PbMaster.PB_Invoice}';

            INSERT INTO [AUD].[AUDIT_PB_TERM]([PB_Vno], [PT_Id], [SNo], [Rate], [Amount], [Term_Type], [Product_Id], [Taxable], [ModifyAction], [ModifyBy], [ModifyDate])
            SELECT [PB_VNo], [PT_Id], [SNo], [Rate], [Amount], [Term_Type], [Product_Id], [Taxable], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.[PB_Term]
            WHERE [PB_Vno]='{PbMaster.PB_Invoice}';  

            INSERT INTO AUD.AUDIT_PRODUCT_ADD_INFO(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, ModifyAction, ModifyBy, ModifyDate)
            SELECT Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.ProductAddInfo
            WHERE Module = 'PB' AND VoucherNo = '{PbMaster.PB_Invoice}'";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }

    // CLOUD SYNC
    public async Task<int> SyncPurchaseInvoiceAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            {
                return 1;
            }

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };

            var getUrl = @$"{configParams.Model.Item2}PurchaseInvoice/GetPurchaseInvoiceById";
            var insertUrl = @$"{configParams.Model.Item2}PurchaseInvoice/InsertPurchaseInvoice";
            var updateUrl = @$"{configParams.Model.Item2}PurchaseInvoice/UpdatePurchaseInvoice";
            var deleteUrl = @$"{configParams.Model.Item2}PurchaseInvoice/DeletePurchaseInvoiceAsync?id=" + PbMaster.PB_Invoice;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
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

            var purchaseInvoiceRepo = DataSyncProviderFactory.GetRepository<PB_Master>(DataSyncManager.GetGlobalInjectData());
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await purchaseInvoiceRepo?.PushNewAsync(PbMaster)!,
                "NEW" => await purchaseInvoiceRepo?.PushNewAsync(PbMaster)!,
                "UPDATE" => await purchaseInvoiceRepo?.PutNewAsync(PbMaster)!,
                //"REVERSE" => await purchaseChallanReturnRepo?.PutNewAsync(PbMaster),
                //"DELETE" => await purchaseChallanReturnRepo?.DeleteNewAsync(PbMaster),
                _ => await purchaseInvoiceRepo?.PushNewAsync(PbMaster)!
            };
            if (result.Value)
            {
                var queryBuilder = new StringBuilder();
                queryBuilder.Append($"UPDATE AMS.PB_Master SET IsSynced =1 WHERE PB_Invoice='{PbMaster.PB_Invoice}'");
                await SqlExtensions.ExecuteNonQueryAsync(queryBuilder.ToString());
            }

            return 1;
        }
        catch (Exception)
        {
            return 1;
        }
    }

    // TERM POSTING
    public int PurchaseInvoiceTermPosting()
    {
        var cmdString = @"
			DELETE AMS.PB_Term WHERE Term_Type='BT' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? @$" AND PB_VNo='{PbMaster.PB_Invoice}';" : "";
        cmdString += @"
			INSERT INTO AMS.PB_Term(PB_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT sbt.PB_VNo, PT_Id,  sd.Invoice_SNo AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion,1) SyncRowVersion, sbt.SyncBaseId
			FROM AMS.PB_Details sd
					LEFT OUTER JOIN AMS.PB_Master sm ON sm.PB_Invoice=sd.PB_Invoice
					LEFT OUTER JOIN AMS.PB_Term sbt ON sd.PB_Invoice=sbt.PB_VNo
			WHERE  sbt.Term_Type='B' AND Product_Id IS NULL AND sbt.Amount > 0 ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND sbt.PB_VNo = '{PbMaster.PB_Invoice}';" : "";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }

    // FINANCE POSTING
    public int PurchaseInvoiceAccountDetailsPosting()
    {
        var cmdString = @" DELETE FROM AMS.AccountDetails WHERE Module = 'PB' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND Voucher_No = '{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId,SyncRowVersion)
			SELECT 'PB' Module,Sno, VNo, VDate, VMiti, VTime, Ledger_Id, CbLedger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, DrAmt, CrAmt, Local_DrAmt, Local_CrAmt, DueDate, DueDays, Naration, Remarks, UserName, Enter_Date, RefNo, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, ChqNo, ChqDate, Party_Name, NULL PartyLedger_Id, Vat_No, CBranch_Id, CUnit_Id, FiscalYearId,IsNUll(ACCTran.SyncRowVersion,1) SyncRowVersion
			FROM(SELECT 'PB' Module, ROW_NUMBER() OVER (PARTITION BY PBM.PB_Invoice ORDER BY PBM.PB_Invoice) AS Sno, PB_Invoice AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, Vendor_ID AS Ledger_Id, SC.PBLedgerId CbLedger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, PBM.Cur_Id, Cur_Rate, 0 AS DrAmt, N_Amount AS CrAmt, 0 AS Local_DrAmt, N_Amount * ISNULL(Cur_Rate, 1) AS Local_CrAmt, PBM.DueDate, PBM.DueDays, '' AS Naration, Remarks, Enter_By AS UserName, Enter_Date, PB_Vno RefNo, NULL Reconcile_By, NULL Reconcile_Date, PBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, FiscalYearId,PBM.SyncRowVersion
				 FROM AMS.PB_Master AS PBM, AMS.PurchaseSetting AS SC WHERE (PBM.R_Invoice = 0 OR PBM.R_Invoice IS NULL)";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? @$" AND PBM.PB_Invoice='{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
				 UNION ALL
				 SELECT 'PB' Module,ROW_NUMBER() OVER (PARTITION BY PBM.PB_Invoice ORDER BY PBM.PB_Invoice) Sno, PBM.PB_Invoice AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, ISNULL(PPL, PBLedgerId) AS Ledger_Id, PBM.Vendor_ID CbLedger_Id, Subledger_Id, PBM.Agent_Id, Cls1, Cls2, Cls3, Cls4, PBM.Cur_Id, PBM.Cur_Rate, ROUND(SUM(ISNULL(PBD.B_Amount, 0)), 2) AS DrAmt, 0 AS CrAmt, SUM(ISNULL(PBD.B_Amount, 0)* ISNULL(PBM.Cur_Rate, 1)) AS Local_DrAmt, 0 AS Local_CrAmt, PBM.DueDate, PBM.DueDays, '' AS Naration, PBM.Remarks, PBM.Enter_By AS UserName, Enter_Date, PB_Vno RefNo, NULL Reconcile_By, NULL Reconcile_Date, PBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, PBM.FiscalYearId, PBM.SyncRowVersion
				 FROM AMS.PB_Master AS PBM, AMS.PB_Details AS PBD, AMS.PurchaseSetting AS SC, AMS.Product AS P
				 WHERE PBD.P_Id=P.PID AND PBD.PB_Invoice=PBM.PB_Invoice AND (PBM.R_Invoice = 0 OR PBM.R_Invoice IS NULL) ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $@"AND PBM.PB_Invoice='{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
				 GROUP BY PBM.PB_Invoice, PPL, Invoice_Date, Invoice_Time, PBM.Cur_Id, PBM.Cur_Rate, Cls1, Cls2, Cls3, Cls4, CUnit_Id, CBranch_Id, PBM.DueDate, PBM.DueDays, PBM.Remarks, PBLedgerId, PBM.Vendor_ID, Subledger_Id, Enter_By, Enter_Date, PB_Vno, PBM.Agent_Id,Invoice_Miti, PBM.Auth_By, Auth_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, PBM.FiscalYearId, PBM.SyncRowVersion
				 UNION ALL
				 SELECT 'PB' Module, ROW_NUMBER() OVER (PARTITION BY PBM.PB_Vno ORDER BY PBM.PB_Vno) AS Sno, PBT.PB_VNo AS VNo, Invoice_Date AS VDate, Invoice_Miti AS VMiti, Invoice_Time AS VTime, PTM.Ledger Ledger_Id, PBM.Vendor_ID CbLedger_Id, Subledger_Id, PBM.Agent_Id, Cls1, Cls2, Cls3, Cls4, PBM.Cur_Id, PBM.Cur_Rate, CASE WHEN PT_Sign='+' THEN (PBT.Amount)ELSE 0 END AS DrAmt, CASE WHEN PT_Sign='-' THEN SUM(PBT.Amount)ELSE 0 END AS CrAmt, CASE WHEN PT_Sign='+' THEN SUM(ISNULL(PBT.Amount, 0) * ISNULL(PBM.Cur_Rate, 1))ELSE 0 END AS Local_DrAmt, CASE WHEN PT_Sign='-' THEN SUM(ISNULL(PBT.Amount, 0)* ISNULL(PBM.Cur_Rate, 1))ELSE 0 END AS Local_CrAmt, PBM.DueDate, PBM.DueDays, '' AS Narration, Remarks, Enter_By, Enter_Date, PBM.PB_Vno RefNo, NULL Reconcile_By, NULL Reconcile_Date, PBM.Auth_By, Auth_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, PBM.ChqNo, PBM.ChqDate, Party_Name, Vat_No, CUnit_Id, CBranch_Id, PBM.FiscalYearId,PBM.SyncRowVersion
				 FROM AMS.PT_Term AS PTM, AMS.PB_Term AS PBT, AMS.PB_Master AS PBM
				 WHERE PTM.PT_ID=PBT.PT_Id AND PBM.PB_Invoice=PBT.PB_VNo AND (PBM.R_Invoice = 0 OR PBM.R_Invoice IS NULL) AND Term_Type<>'BT' AND Term_Type<>'' AND CASE WHEN PT_Sign='+' THEN Amount ELSE -Amount END<>0  ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? @$" AND PBM.PB_Invoice='{PbMaster.PB_Invoice}'" : " ";
        cmdString += @"
				GROUP BY CASE WHEN PT_Sign='+' THEN (PBT.Amount)ELSE 0 END, PBT.PB_VNo, PBM.Invoice_Date, PBM.Invoice_Miti, PBM.Invoice_Time, PTM.Ledger, PBM.Vendor_ID, PBM.Subledger_Id, PBM.Agent_Id, PBM.Cls1, PBM.Cls2, PBM.Cls3, PBM.Cls4, PBM.Cur_Id, PBM.Cur_Rate, PTM.PT_Sign, PBM.DueDate, PBM.DueDays, PBM.Enter_By, PBM.Enter_Date, PBM.PB_Vno, PBM.Auth_By, PBM.Auth_Date, PBM.ChqNo, PBM.ChqDate, PBM.Party_Name, PBM.Vat_No, PBM.CUnit_Id, PBM.CBranch_Id, PBM.FiscalYearId, PBM.SyncRowVersion,PBM.Remarks
				) AS ACCTran;   ";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }

    // STOCK POSTING
    public int PurchaseInvoiceStockDetailsPosting()
    {
        var cmdString = @"
			DELETE FROM AMS.StockDetails WHERE Module='PB' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND Voucher_No='{PbMaster.PB_Invoice}';" : " ";
        cmdString += @"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, Subledger_Id, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_ID, RoomNo, Branch_ID, CmpUnit_ID, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, Subledger_Id, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_ID, RoomNo, Branch_ID, CmpUnit_ID, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM(SELECT 'PB' Module, pd.PB_Invoice Voucher_No, Invoice_SNo Serial_No, pm.PB_Vno PurRefVno, pm.Invoice_Date Voucher_Date, pm.Invoice_Miti Voucher_Miti, pm.Invoice_Time Voucher_Time, pm.Vendor_ID Ledger_ID, pm.Subledger_Id Subledger_Id, pm.Agent_ID, pm.Cls1 Department_ID1, pm.Cls2 Department_ID2, pm.Cls3 Department_ID3, pm.Cls4 Department_ID4, pm.Cur_Id Currency_ID, pm.Cur_Rate Currency_Rate, P_Id Product_Id, Gdn_Id Godown_Id, NULL CostCenter_Id, Alt_Qty AltQty, Alt_UnitId AltUnit_Id, Qty, Unit_Id, AltStock_Qty AltStockQty, Stock_Qty StockQty, pd.Free_Qty FreeQty, pd.Free_Unit_Id FreeUnit_Id, pd.StockFree_Qty StockFreeQty, 0 ConvRatio, pd.ExtraFree_Qty ExtraFreeQty, pd.ExtraFree_Unit_Id ExtraFreeUnit_Id, pd.ExtraStockFree_Qty ExtraStockFreeQty, pd.Rate, pd.B_Amount BasicAmt, pd.T_Amount TermAmt, pd.N_Amount NetAmt, 0 BillTermAmt, pd.V_Rate TaxRate, pm.Tbl_Amount TaxableAmt, (pd.N_Amount+ISNULL(StockValue.StockValue, 0)) DocVal, 0 ReturnVal, (pd.N_Amount+ISNULL(StockValue.StockValue, 0)) StockVal, 0 AddStockVal, pm.PB_Vno PartyInv, 'I' EntryType, pm.Auth_By AuthBy, pm.Auth_Date AuthDate, pm.Reconcile_By RecoBy, pm.Reconcile_Date RecoDate, NULL Counter_ID, NULL RoomNo, pm.CBranch_Id Branch_ID, pm.CUnit_Id CmpUnit_ID, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, 0 SalesRate, pm.FiscalYearId, pm.Enter_By EnterBy, pm.Enter_Date EnterDate, pm.SyncGlobalId, pm.SyncOriginId, pm.SyncCreatedOn, pm.SyncLastPatchedOn, pm.SyncRowVersion, pm.SyncBaseId
				 FROM AMS.PB_Details pd
					  INNER JOIN AMS.Product p ON p.PID = pd.P_Id
					  INNER JOIN AMS.PB_Master pm ON pd.PB_Invoice=pm.PB_Invoice
					  LEFT OUTER JOIN(SELECT pt.Product_Id, pt.PB_VNo, pt.SNo, SUM(CASE WHEN pt1.PT_Sign='-' THEN -pt.Amount ELSE pt.Amount END) StockValue
									  FROM AMS.PB_Term pt
										   LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
									  WHERE pt1.PT_Profitability>0 AND pt.Term_Type<>'B' ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND pt.PB_VNo='{PbMaster.PB_Invoice}' " : " ";
        cmdString += @"
									  GROUP BY pt.Product_Id, pt.PB_VNo, pt.SNo) AS StockValue ON StockValue.Product_Id = pd.P_Id AND StockValue.PB_VNo = pd.PB_Invoice AND pd.Invoice_SNo = StockValue.SNo
				 WHERE p.PType IN ('I','Inventory') ";
        cmdString += PbMaster.PB_Invoice.IsValueExits() ? $" AND pm.PB_Invoice='{PbMaster.PB_Invoice}' " : " ";
        cmdString += " ) AS Stock; ";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }


    // DATA TABLE FUNCTION
    public DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo)
    {
        var cmdString = $@" 
            SELECT PB_Vno FROM AMS.PB_Master where Vendor_ID = '{ledgerId}' and PB_Vno = '{txtRefVno}'  AND FiscalYearId = {ObjGlobal.SysFiscalYearId} ";
        if (action != "SAVE") cmdString += $" AND PB_Invoice <> '{voucherNo}' and Vendor_ID <> '{ledgerId}'  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }



    // OBJECT FOR THIS FORM

    public PB_Master PbMaster { get; set; } = new();
    public List<PB_Details> DetailsList { get; set; } = new();
    public List<PB_Term> Terms { get; set; } = new();
    public List<ProductAddInfo> AddInfos { get; set; } = new();
    public PB_OtherMaster OtherMaster { get; set; } = new();

    private string[] _tagStrings = { "DELETE", "REVERSE" };
}