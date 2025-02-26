using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Shared.DataSync.Abstractions;
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
// ReSharper disable All

namespace MrDAL.DataEntry.SalesMaster;

public class SalesInvoiceRepository : ISalesInvoiceRepository
{
    // SALES INVOICE INSERT UPDATE DELETE
    #region ---------- PULL SALES INVOICE ----------
    public int SaveSalesInvoice(string actionTag)
    {
        var invoiceNo = SbMaster.SB_Invoice;
        try
        {
            var cmdString = new StringBuilder();
            if (actionTag is "DELETE" or "REVERSE" or "UPDATE")
            {
                if (!actionTag.Equals("UPDATE"))
                {
                    AuditLogSalesInvoice(actionTag);
                }

                if (!actionTag.Equals("REVERSE"))
                {
                    cmdString.Append($@" 
                    DELETE FROM AMS.SB_Term WHERE SB_VNo ='{SbMaster.SB_Invoice}'; ");
                    cmdString.Append($@" 
                    DELETE FROM AMS.SB_Details WHERE SB_Invoice ='{SbMaster.SB_Invoice}';");
                }

                cmdString.Append($@" 
                    DELETE FROM AMS.AccountDetails WHERE module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; ");
                cmdString.Append($@" 
                    DELETE FROM AMS.StockDetails WHERE module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; ");
                if (actionTag.Equals("DELETE"))
                {
                    cmdString.Append($@" 
                        DELETE FROM AMS.SB_ExchangeDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; ");
                    cmdString.Append($@" 
                        DELETE FROM AMS.SB_Master_OtherDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; ");
                    cmdString.Append($@" 
                        DELETE FROM AMS.SB_Master WHERE SB_Invoice ='{SbMaster.SB_Invoice}'; ");
                }

                if (actionTag == "REVERSE")
                {
                    cmdString.Append($@" 
                        UPDATE AMS.SB_Master SET R_Invoice =1, Cancel_By='{ObjGlobal.LogInUser}', Cancel_Date= GETDATE(),Cancel_Remarks='{SbMaster.Remarks}' WHERE SB_Invoice='{SbMaster.SB_Invoice}' ");
                }
            }

            if (actionTag.Equals("SAVE"))
            {
                cmdString.Append(@" 
                INSERT INTO AMS.SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
                cmdString.Append($"\n VALUES (N'{SbMaster.SB_Invoice}', '{SbMaster.Invoice_Date.GetSystemDate()}', N'{SbMaster.Invoice_Miti}', GETDATE(),");
                cmdString.Append(SbMaster.PB_Vno.IsValueExits()
                    ? $" N'{SbMaster.PB_Vno}', '{SbMaster.Vno_Date.GetSystemDate()}','{SbMaster.Vno_Miti}',"
                    : "NULL,NULL,NULL,");
                cmdString.Append($" {SbMaster.Customer_Id},");
                cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" N'{SbMaster.Party_Name}'," : "NULL,");
                cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $" N'{SbMaster.Vat_No}'," : "NULL,");
                cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" N'{SbMaster.Contact_Person}'," : "NULL,");
                cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" N'{SbMaster.Mobile_No}'," : "NULL,");
                cmdString.Append(SbMaster.Address.IsValueExits() ? $" N'{SbMaster.Address}'," : "NULL,");
                cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqNo}'," : "NULL,");
                cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqDate.GetSystemDate()}'," : "NULL,");
                cmdString.Append($"N'{SbMaster.Invoice_Type}', N'{SbMaster.Invoice_Mode}','{SbMaster.Payment_Mode}',{SbMaster.DueDays},");
                cmdString.Append(SbMaster.DueDays > 0 ? $" '{SbMaster.DueDate.GetSystemDate()}'," : "NULL,");
                cmdString.Append(SbMaster.Agent_Id > 0 ? $" {SbMaster.Agent_Id}," : "NULL,");
                cmdString.Append(SbMaster.Subledger_Id > 0 ? $" {SbMaster.Subledger_Id}," : "NULL,");
                cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Invoice}'," : "NULL,");
                cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Date.GetSystemDate()}'," : "NULL,");
                cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Invoice}'," : "NULL,");
                cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Date.GetSystemDate()}'," : "NULL,");
                cmdString.Append(SbMaster.Cls1 > 0 ? $" {SbMaster.Cls1}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,");
                cmdString.Append(SbMaster.CounterId > 0 ? $" {SbMaster.CounterId}," : "NULL,");
                cmdString.Append(SbMaster.Cur_Id > 0 ? $" {SbMaster.Cur_Id}," : "1,");
                cmdString.Append($" {SbMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($" {SbMaster.B_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.T_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.N_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.LN_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.V_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.Tbl_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.Tender_Amount.GetDecimal()},");
                cmdString.Append($" {SbMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($"'{SbMaster.Action_Type}',");
                cmdString.Append(SbMaster.In_Words.IsValueExits() ? $" N'{SbMaster.In_Words}'," : "NULL,");
                cmdString.Append(SbMaster.Remarks.IsValueExits() ? $" N'{SbMaster.Remarks.Trim().Replace("'", "''")}'," : "NULL,");
                cmdString.Append(SbMaster.R_Invoice ? $"CAST('{SbMaster.R_Invoice}' AS BIT) ," : "0,");
                cmdString.Append(SbMaster.Is_Printed ? $"CAST('{SbMaster.Is_Printed}' AS BIT) ," : "0,");
                cmdString.Append(SbMaster.No_Print > 0 ? $" {SbMaster.No_Print}," : "0,");
                cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_By}'," : "0,");
                cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_Date.GetSystemDate()}'," : "NULL,");
                cmdString.Append(SbMaster.Audit_Lock is true ? $"CAST('{SbMaster.Audit_Lock}' AS BIT) ," : "0,");
                cmdString.Append($" '{ObjGlobal.LogInUser}',GETDATE(),");
                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
                cmdString.Append("NULL,NULL,");
                cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
                cmdString.Append(SbMaster.SyncGlobalId.IsGuidExits() ? $"'{SbMaster.SyncGlobalId}'," : "NULL,");
                cmdString.Append(SbMaster.SyncOriginId.IsGuidExits() ? $"'{SbMaster.SyncOriginId}'," : "NULL,");
                cmdString.Append($"'{SbMaster.SyncCreatedOn.GetSystemDate()}','{SbMaster.SyncLastPatchedOn.GetSystemDate()}',");
                cmdString.Append($"{SbMaster.SyncRowVersion},");
                cmdString.Append(SbMaster.SyncBaseId.IsGuidExits() ? $"'{SbMaster.SyncBaseId}'); \n" : "NULL);");
            }
            else if (actionTag.Equals("UPDATE"))
            {
                cmdString.Append(" UPDATE AMS.SB_Master SET \n");
                cmdString.Append($"Invoice_Date = '{SbMaster.Invoice_Date.GetSystemDate()}', Invoice_Miti = N'{SbMaster.Invoice_Miti}',");
                cmdString.Append(SbMaster.PB_Vno.IsValueExits()
                    ? $" PB_Vno = N'{SbMaster.PB_Vno}',Vno_Date = '{SbMaster.Vno_Date.GetSystemDate()}',Vno_Miti = '{SbMaster.Vno_Miti}',"
                    : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
                cmdString.Append($" Customer_Id = {SbMaster.Customer_Id},");
                cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" Party_Name = N'{SbMaster.Party_Name}'," : "Party_Name = NULL,");
                cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $"Vat_No =  N'{SbMaster.Vat_No}'," : "Vat_No = NULL,");
                cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" Contact_Person = N'{SbMaster.Contact_Person}'," : "Contact_Person = NULL,");
                cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" Mobile_No = N'{SbMaster.Mobile_No}'," : "Mobile_No = NULL,");
                cmdString.Append(SbMaster.Address.IsValueExits() ? $" Address = N'{SbMaster.Address}'," : "Address = NULL,");
                cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SbMaster.ChqNo}'," : "ChqNo = NULL,");
                cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqDate = N'{SbMaster.ChqDate.GetSystemDate()}'," : "ChqDate = NULL,");
                cmdString.Append($"Invoice_Type = N'{SbMaster.Invoice_Type}', Invoice_Mode = N'{SbMaster.Invoice_Mode}',Payment_Mode =  '{SbMaster.Payment_Mode}',DueDays =  {SbMaster.DueDays},");
                cmdString.Append(SbMaster.DueDays > 0 ? $" DueDate = '{SbMaster.DueDate.GetSystemDate()}'," : "DueDate = NULL,");
                cmdString.Append(SbMaster.Agent_Id > 0 ? $" Agent_Id = {SbMaster.Agent_Id}," : "Agent_Id = NULL,");
                cmdString.Append(SbMaster.Subledger_Id > 0 ? $" Subledger_Id = {SbMaster.Subledger_Id}," : "Subledger_Id = NULL,");
                cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" SO_Invoice = N'{SbMaster.SO_Invoice}'," : "SO_Invoice = NULL,");
                cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" SO_Date = N'{SbMaster.SO_Date.GetSystemDate()}'," : "SO_Date = NULL,");
                cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" SC_Invoice = N'{SbMaster.SC_Invoice}'," : "SC_Invoice = NULL,");
                cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" SC_Date = N'{SbMaster.SC_Date.GetSystemDate()}'," : "SC_Date = NULL,");
                cmdString.Append(SbMaster.Cls1 > 0 ? $" Cls1 = {SbMaster.Cls1}," : "Cls1 = NULL,");
                cmdString.Append(SbMaster.CounterId > 0 ? $"CounterId = {SbMaster.CounterId}," : " CounterId = NULL,");
                cmdString.Append(SbMaster.Cur_Id > 0 ? $" Cur_Id = {SbMaster.Cur_Id}," : "Cur_Id = 1,");
                cmdString.Append($"Cur_Rate =  {SbMaster.Cur_Rate.GetDecimal(true)},");
                cmdString.Append($"B_Amount =  {SbMaster.B_Amount.GetDecimal()},");
                cmdString.Append($"T_Amount = {SbMaster.T_Amount.GetDecimal()},");
                cmdString.Append($"N_Amount = {SbMaster.N_Amount.GetDecimal()},");
                cmdString.Append($"LN_Amount = {SbMaster.LN_Amount.GetDecimal()},");
                cmdString.Append($"V_Amount =  {SbMaster.V_Amount.GetDecimal()},");
                cmdString.Append($"Tbl_Amount =  {SbMaster.Tbl_Amount.GetDecimal()},");
                cmdString.Append($"Tender_Amount = {SbMaster.Tender_Amount.GetDecimal()},");
                cmdString.Append($"Return_Amount =  {SbMaster.Return_Amount.GetDecimal()},");
                cmdString.Append($"Action_Type = '{SbMaster.Action_Type}',");
                cmdString.Append(SbMaster.In_Words.IsValueExits() ? $"In_Words =  N'{SbMaster.In_Words}'," : "In_Words = NULL,");
                cmdString.Append(SbMaster.Remarks.IsValueExits() ? $" Remarks = N'{SbMaster.Remarks.Trim().Replace("'", "''")}'," : "Remarks = NULL,");
                cmdString.Append("IsSynced=0 ");
                cmdString.Append($" WHERE SB_Invoice = N'{SbMaster.SB_Invoice}'; \n");
            }

            if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
            {
                if (DetailsList.Count > 0)
                {
                    cmdString.Append(@" INSERT INTO AMS.SB_Details(SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                    cmdString.Append("\n VALUES \n");

                    foreach (var dr in DetailsList)
                    {
                        var index = DetailsList.IndexOf(dr);
                        cmdString.Append($@"('{dr.SB_Invoice}', {dr.Invoice_SNo}, {dr.P_Id},");
                        cmdString.Append(dr.Gdn_Id > 0 ? $@"{dr.Gdn_Id}," : "NULL,");
                        cmdString.Append($@"{dr.Alt_Qty},");
                        cmdString.Append(dr.Alt_UnitId > 0 ? $@"{dr.Alt_UnitId}," : "NULL,");
                        cmdString.Append($@"{dr.Qty}, {dr.Unit_Id}, {dr.Rate}, {dr.B_Amount}, {dr.T_Amount}, {dr.N_Amount},");
                        cmdString.Append($@"{dr.AltStock_Qty}, {dr.Stock_Qty},");
                        cmdString.Append(dr.Narration.IsValueExits() ? $@"'{dr.Narration}'," : "NULL,");
                        cmdString.Append(dr.SO_Invoice.IsValueExits() ? $@"'{dr.SO_Invoice}', {dr.SO_Sno}, " : "NULL,0,");
                        cmdString.Append(dr.SC_Invoice.IsValueExits() ? $@"'{dr.SC_Invoice}', {dr.SC_SNo}," : "NULL,0,");
                        cmdString.Append($@"{dr.Tax_Amount},{dr.V_Amount} , {dr.V_Rate}, ");
                        cmdString.Append(dr.Free_Unit_Id > 0 ? $@"{dr.Free_Unit_Id}," : "NULL,");
                        cmdString.Append($@"{dr.Free_Qty}, {dr.StockFree_Qty},");
                        cmdString.Append(dr.ExtraFree_Unit_Id > 0 ? $@"{dr.ExtraFree_Unit_Id}," : "NULL,");
                        cmdString.Append($@"{dr.ExtraFree_Qty}, {dr.ExtraStockFree_Qty}, CAST('{dr.T_Product}' AS BIT), ");
                        cmdString.Append(dr.S_Ledger > 0 ? $@"{dr.S_Ledger}," : "NULL,");
                        cmdString.Append(dr.SR_Ledger > 0 ? $@"{dr.SR_Ledger}," : "NULL,");
                        cmdString.Append($@"NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                        cmdString.Append($@"NULL, NULL, NULL, NULL, '{dr.MaterialPost}',");
                        cmdString.Append($@"{dr.PDiscountRate}, {dr.PDiscount}, {dr.BDiscountRate}, {dr.BDiscount}, {dr.ServiceChargeRate}, {dr.ServiceCharge},");
                        cmdString.Append(SbMaster.SyncBaseId.IsGuidExits() ? $"'{SbMaster.SyncBaseId}'," : "NULL,");
                        cmdString.Append(SbMaster.SyncGlobalId.IsGuidExits() ? $"'{SbMaster.SyncGlobalId}'," : "NULL,");
                        cmdString.Append(SbMaster.SyncOriginId.IsGuidExits() ? $"'{SbMaster.SyncOriginId}'," : "NULL,");
                        cmdString.Append($"'{SbMaster.SyncCreatedOn.GetSystemDate()}','{SbMaster.SyncLastPatchedOn.GetSystemDate()}',");
                        cmdString.Append($"{SbMaster.SyncRowVersion}");
                        cmdString.Append(index == DetailsList.Count - 1 ? ");\n" : "),\n");

                    }
                }

                if (Terms != null && Terms.Count > 0)
                {
                    if (actionTag.Equals("UPDATE"))
                    {
                        cmdString.Append($@"
                        DELETE AMS.SB_Term WHERE SB_VNo='{SbMaster.SB_Invoice}' AND Term_Type='B' ");
                    }
                    cmdString.Append(@" 
                    INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) 
                    VALUES ");
                    foreach (var dr in Terms)
                    {
                        var index = Terms.IndexOf(dr);
                        cmdString.Append($"\n ('{dr.SB_VNo}',");
                        cmdString.Append($"{dr.ST_Id},");
                        cmdString.Append(dr.SNo.GetInt() > 0 ? $"{dr.SNo}," : "1,");
                        cmdString.Append($"{dr.Rate},{dr.Amount},'{dr.Term_Type}',");
                        cmdString.Append(dr.Product_Id > 0 ? $"{dr.Product_Id}," : "NULL,");
                        cmdString.Append(dr.ST_Id == ObjGlobal.SalesVatTermId ? "'Y'," : "'N',");
                        cmdString.Append(SbMaster.SyncGlobalId.IsGuidExits() ? $"'{SbMaster.SyncGlobalId}'," : "NULL,");
                        cmdString.Append(SbMaster.SyncOriginId.IsGuidExits() ? $"'{SbMaster.SyncOriginId}'," : "NULL,");
                        cmdString.Append($"'{SbMaster.SyncCreatedOn.GetSystemDate()}','{SbMaster.SyncLastPatchedOn.GetSystemDate()}',");
                        cmdString.Append($"{SbMaster.SyncRowVersion},");
                        cmdString.Append(SbMaster.SyncBaseId.IsGuidExits() ? $"'{SbMaster.SyncBaseId}'" : "NULL");
                        cmdString.Append(index == Terms.Count - 1 ? "); \n" : "),\n");
                    }
                }

                if (AddInfos != null && AddInfos.Count > 0)
                {
                    cmdString.Append($@" 
                    DELETE AMS.ProductAddInfo WHERE VoucherNo='{SbMaster.SB_Invoice}' AND Module='SB';");
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

                
            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }
            if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null)
            {
                //Task.Run(() => SyncSalesInvoiceAsync(actionTag));
            }
            _ = SbMaster.TableId > 0 ? UpdateSalesTableStatus() : 0;
            _ = SbMaster.SO_Invoice.IsValueExits() ? UpdateSalesOrderStatus() : 0;
            _ = SalesTermPostingAsync();
            _ = SalesInvoiceAccountPosting();
            _ = SalesInvoiceStockPosting();
            if (!ObjGlobal.IsIrdRegister)
            {
                return iResult;
            }

            PostingBillToApi(invoiceNo);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }
    public int SavePointOfSalesInvoiceWithQuery(string queryString, string actionTag)
    {
        try
        {
            var cmdString = new StringBuilder();
            cmdString.Append(queryString);
            var iResult = SaveDataInDatabase(cmdString);
            if (iResult <= 0)
            {
                return iResult;
            }
            SaveSalesOtherDetails("SB", actionTag);
            _ = SalesTermPostingAsync();
            _ = SalesInvoiceAccountPosting();
            _ = SalesInvoiceStockPosting();
            if (!ObjGlobal.IsIrdRegister)
            {
                return iResult;
            }

            PostingBillToApi(SbMaster.SB_Invoice);
            return iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }

    }
    private int SaveSalesOtherDetails(string module, string actionTag)
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
    public int SaveRestroInvoice(string actionTag)
    {
        //try
        //{
        //    var invoiceNo = SbMaster.SB_Invoice;
        //    var cmdString = new StringBuilder();
        //    string[] strNamesArray = { "UPDATE", "DELETE", "REVERSE" };
        //    if (strNamesArray.Any(x => x == actionTag))
        //    {
        //        if (!actionTag.Equals("UPDATE"))
        //        {
        //            AuditLogSalesInvoice(actionTag);
        //        }

        //        if (actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($@"
        //        UPDATE AMS.SB_Master SET Action_Type='CANCEL' ,R_Invoice=1 WHERE SB_Invoice='{SbMaster.SB_Invoice}';");
        //        }

        //        if (!actionTag.Equals("REVERSE"))
        //        {
        //            cmdString.Append($"Delete from AMS.SB_Term where SB_VNo ='{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Details where SB_Invoice ='{SbMaster.SB_Invoice}'; \n");
        //        }

        //        cmdString.Append(
        //            $"Delete from AMS.AccountDetails Where module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; \n");
        //        cmdString.Append(
        //            $"Delete from AMS.StockDetails Where module='SB' and Voucher_No ='{SbMaster.SB_Invoice}'; \n");

        //        if (actionTag.Equals("DELETE"))
        //        {
        //            cmdString.Append(
        //                $"DELETE FROM AMS.SB_ExchangeDetails WHERE SB_Invoice = '{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append(
        //                $"Delete from AMS.SB_Master_OtherDetails where SB_Invoice = '{SbMaster.SB_Invoice}'; \n");
        //            cmdString.Append($"Delete from AMS.SB_Master where SB_Invoice ='{SbMaster.SB_Invoice}'; \n");
        //        }
        //    }

        //    if (actionTag.Equals("SAVE"))
        //    {
        //        if (SbMaster.PB_Vno.IsValueExits())
        //        {
        //            cmdString.Append(
        //                $" UPDATE AMS.temp_SB_Master SET Action_Type='POST' WHERE SB_Invoice='{SbMaster.PB_Vno}' \n");
        //        }

        //        cmdString.Append(
        //            " INSERT INTO AMS.SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, Agent_Id, Subledger_Id, SO_Invoice, SO_Date, SC_Invoice, SC_Date, Cls1, Cls2, Cls3, Cls4, CounterId, Cur_Id, Cur_Rate, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, CUnit_Id, CBranch_Id, IsAPIPosted, IsRealtime, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, DoctorId, PatientId, HDepartmentId, MShipId, TableId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //        cmdString.Append(
        //            $" VALUES (N'{SbMaster.SB_Invoice}', '{SbMaster.Invoice_Date:yyyy-MM-dd}', N'{SbMaster.Invoice_Miti}', GETDATE(),");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" N'{SbMaster.PB_Vno}', '{SbMaster.Vno_Date:yyyy-MM-dd}','{SbMaster.Vno_Miti}',"
        //            : "NULL,NULL,NULL,");
        //        cmdString.Append($" {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits() ? $" N'{SbMaster.Party_Name}'," : "NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits() ? $" N'{SbMaster.Vat_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits() ? $" N'{SbMaster.Contact_Person}'," : "NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits() ? $" N'{SbMaster.Mobile_No}'," : "NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits() ? $" N'{SbMaster.Address}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqNo}'," : "NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" N'{SbMaster.ChqDate:yyyy-MM-dd}'," : "NULL,");

        //        cmdString.Append(
        //            $"N'{SbMaster.Invoice_Type}', N'{SbMaster.Invoice_Mode}', '{SbMaster.Payment_Mode}', {SbMaster.DueDays},");

        //        cmdString.Append(SbMaster.DueDays > 0 ? $" '{SbMaster.DueDate:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" {SbMaster.Agent_Id}," : "NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0 ? $" {SbMaster.Subledger_Id}," : "NULL,");

        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits() ? $" N'{SbMaster.SO_Date:yyyy-MM-dd}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Invoice}'," : "NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits() ? $" N'{SbMaster.SC_Date:yyyy-MM-dd}'," : "NULL,");

        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" {SbMaster.Cls1}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $" {SbMaster.CounterId}," : "NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" {SbMaster.Cur_Id}," : "1,");
        //        cmdString.Append(SbMaster.Cur_Rate > 0 ? $" {SbMaster.Cur_Rate}," : "1,");
        //        cmdString.Append(SbMaster.B_Amount > 0 ? $" {SbMaster.B_Amount}," : "0,");
        //        cmdString.Append(SbMaster.T_Amount > 0 ? $" {SbMaster.T_Amount}," : "0,");
        //        cmdString.Append(SbMaster.N_Amount > 0 ? $" {SbMaster.N_Amount}," : "0,");
        //        cmdString.Append(SbMaster.LN_Amount > 0 ? $" {SbMaster.LN_Amount}," : "0,");
        //        cmdString.Append(SbMaster.V_Amount > 0 ? $" {SbMaster.V_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Tbl_Amount > 0 ? $" {SbMaster.Tbl_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Tender_Amount > 0 ? $" {SbMaster.Tender_Amount}," : "0,");
        //        cmdString.Append(SbMaster.Return_Amount > 0 ? $" {SbMaster.Return_Amount}," : "0,");
        //        cmdString.Append($"'{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits() ? $" N'{SbMaster.In_Words}'," : "NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits()
        //            ? $" N'{SbMaster.Remarks.Trim().Replace("'", "''")}',"
        //            : "NULL,");
        //        cmdString.Append(SbMaster.R_Invoice ? $"CAST('{SbMaster.R_Invoice}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.Is_Printed ? $"CAST('{SbMaster.Is_Printed}' AS BIT) ," : "0,");
        //        cmdString.Append(SbMaster.No_Print > 0 ? $" {SbMaster.No_Print}," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits() ? $" '{SbMaster.Printed_By}'," : "0,");
        //        cmdString.Append(SbMaster.Printed_By.IsValueExits()
        //            ? $" '{SbMaster.Printed_Date: yyyy-MM-dd}',"
        //            : "NULL,");
        //        cmdString.Append(SbMaster.Audit_Lock is true ? $"CAST('{SbMaster.Audit_Lock}' AS BIT) ," : "0,");
        //        cmdString.Append($" '{ObjGlobal.LogInUser.ToUpper()}',GETDATE(),");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
        //        cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,");
        //        cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
        //        cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
        //        cmdString.Append(SbMaster.MShipId > 0 ? $"{SbMaster.MShipId}," : "NULL,");
        //        cmdString.Append(SbMaster.TableId > 0 ? $"{SbMaster.TableId}," : "NULL,");
        //        cmdString.Append("NULL,NUll,NULL,NULL,1,NULL); \n");
        //    }
        //    else if (actionTag.Equals("UPDATE"))
        //    {
        //        cmdString.Append("UPDATE AMS.SB_Master SET ");
        //        cmdString.Append(
        //            $"Invoice_Date = '{SbMaster.Invoice_Date:yyyy-MM-dd}', Invoice_Miti = N'{SbMaster.Invoice_Miti}',");
        //        cmdString.Append(SbMaster.PB_Vno.IsValueExits()
        //            ? $" PB_Vno = N'{SbMaster.PB_Vno}',Vno_Date = '{SbMaster.Vno_Date:yyyy-MM-dd}',Vno_Miti = '{SbMaster.Vno_Miti}',"
        //            : "PB_Vno = NULL,Vno_Date = NULL,Vno_Miti = NULL,");
        //        cmdString.Append($" Customer_Id = {SbMaster.Customer_Id},");
        //        cmdString.Append(SbMaster.Party_Name.IsValueExits()
        //            ? $" Party_Name = N'{SbMaster.Party_Name}',"
        //            : "Party_Name = NULL,");
        //        cmdString.Append(SbMaster.Vat_No.IsValueExits()
        //            ? $"Vat_No =  N'{SbMaster.Vat_No}',"
        //            : "Vat_No = NULL,");
        //        cmdString.Append(SbMaster.Contact_Person.IsValueExits()
        //            ? $" Contact_Person = N'{SbMaster.Contact_Person}',"
        //            : "Contact_Person = NULL,");
        //        cmdString.Append(SbMaster.Mobile_No.IsValueExits()
        //            ? $" Mobile_No = N'{SbMaster.Mobile_No}',"
        //            : "Mobile_No = NULL,");
        //        cmdString.Append(SbMaster.Address.IsValueExits()
        //            ? $" Address = N'{SbMaster.Address}',"
        //            : "Address = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits() ? $" ChqNo = N'{SbMaster.ChqNo}'," : "ChqNo = NULL,");
        //        cmdString.Append(SbMaster.ChqNo.IsValueExits()
        //            ? $" ChqDate = N'{SbMaster.ChqDate:yyyy-MM-dd}',"
        //            : "ChqDate = NULL,");
        //        cmdString.Append(
        //            $"Invoice_Type = N'{SbMaster.Invoice_Type}', Invoice_Mode = N'{SbMaster.Invoice_Mode}',Payment_Mode =  '{SbMaster.Payment_Mode}',DueDays =  {SbMaster.DueDays},");
        //        cmdString.Append(SbMaster.DueDays > 0
        //            ? $" DueDate = '{SbMaster.DueDate:yyyy-MM-dd}',"
        //            : "DueDate = NULL,");
        //        cmdString.Append(SbMaster.Agent_Id > 0 ? $" Agent_Id = {SbMaster.Agent_Id}," : "Agent_Id = NULL,");
        //        cmdString.Append(SbMaster.Subledger_Id > 0
        //            ? $" Subledger_Id = {SbMaster.Subledger_Id},"
        //            : "Subledger_Id = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits()
        //            ? $" SO_Invoice = N'{SbMaster.SO_Invoice}',"
        //            : "SO_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SO_Invoice.IsValueExits()
        //            ? $" SO_Date = N'{SbMaster.SO_Date:yyyy-MM-dd}',"
        //            : "SO_Date = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
        //            ? $" SC_Invoice = N'{SbMaster.SC_Invoice}',"
        //            : "SC_Invoice = NULL,");
        //        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
        //            ? $" SC_Date = N'{SbMaster.SC_Date:yyyy-MM-dd}',"
        //            : "SC_Date = NULL,");
        //        cmdString.Append(SbMaster.Cls1 > 0 ? $" Cls1 = {SbMaster.Cls1}," : "Cls1 = NULL,");
        //        cmdString.Append(SbMaster.CounterId > 0 ? $"CounterId = {SbMaster.CounterId}," : " CounterId = NULL,");
        //        cmdString.Append(SbMaster.Cur_Id > 0 ? $" Cur_Id = {SbMaster.Cur_Id}," : "Cur_Id = 1,");
        //        cmdString.Append(SbMaster.Cur_Rate > 0 ? $"Cur_Rate =  {SbMaster.Cur_Rate}," : "Cur_Rate = 1,");
        //        cmdString.Append(SbMaster.B_Amount > 0 ? $"B_Amount =  {SbMaster.B_Amount}," : "B_Amount = 0,");
        //        cmdString.Append(SbMaster.T_Amount > 0 ? $"T_Amount = {SbMaster.T_Amount}," : "T_Amount = 0,");
        //        cmdString.Append(SbMaster.N_Amount > 0 ? $"N_Amount = {SbMaster.N_Amount}," : "N_Amount = 0,");
        //        cmdString.Append(SbMaster.LN_Amount > 0 ? $" LN_Amount = {SbMaster.LN_Amount}," : "LN_Amount = 0,");
        //        cmdString.Append(SbMaster.V_Amount > 0 ? $"V_Amount =  {SbMaster.V_Amount}," : "V_Amount = 0,");
        //        cmdString.Append(SbMaster.Tbl_Amount > 0 ? $"Tbl_Amount =  {SbMaster.Tbl_Amount}," : "Tbl_Amount = 0,");
        //        cmdString.Append(SbMaster.Tender_Amount > 0
        //            ? $" Tender_Amount = {SbMaster.Tender_Amount},"
        //            : " Tender_Amount = 0,");
        //        cmdString.Append(SbMaster.Return_Amount > 0
        //            ? $"Return_Amount =  {SbMaster.Return_Amount},"
        //            : "Return_Amount = 0,");
        //        cmdString.Append($"Action_Type = '{SbMaster.Action_Type}',");
        //        cmdString.Append(SbMaster.In_Words.IsValueExits()
        //            ? $"In_Words =  N'{SbMaster.In_Words}',"
        //            : "In_Words = NULL,");
        //        cmdString.Append(SbMaster.Remarks.IsValueExits()
        //            ? $" Remarks = N'{SbMaster.Remarks.Trim().Replace("'", "''")}'"
        //            : "Remarks = NULL");
        //        cmdString.Append($" WHERE SB_Invoice = N'{SbMaster.SB_Invoice}'; \n");
        //    }

        //    if (!actionTag.Equals("DELETE") && !actionTag.Equals("REVERSE"))
        //    {
        //        if (SbMaster.GetView is { RowCount: > 0 })
        //        {
        //            var iRows = 0;
        //            cmdString.Append(
        //                " INSERT INTO AMS.SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
        //            cmdString.Append(" VALUES \n");
        //            foreach (DataGridViewRow dr in SbMaster.GetView.Rows)
        //            {
        //                iRows++;
        //                cmdString.Append($"('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{dr.Cells["GTxtProductId"].Value},");
        //                cmdString.Append(dr.Cells["GTxtGodownId"]?.Value.GetInt() > 0 ? $"{dr.Cells["GTxtGodownId"].Value}," : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtAltQty"].Value.GetDecimal() > 0 ? $"{dr.Cells["GTxtAltQty"].Value}," : "0,");
        //                cmdString.Append(dr.Cells["GTxtAltUOMId"]?.Value.GetInt() > 0 ? $"{dr.Cells["GTxtAltUOMId"].Value}," : "NULL,");
        //                cmdString.Append(dr.Cells["GTxtQty"].Value.GetDecimal() > 0 ? $"{dr.Cells["GTxtQty"].Value}," : "1,");
        //                cmdString.Append(dr.Cells["GTxtUOMId"].Value.GetInt() > 0 ? $"{dr.Cells["GTxtUOMId"].Value}," : "NUll,");
        //                cmdString.Append($"{dr.Cells["GTxtDisplayRate"].Value.GetDecimal()},");

        //                var termAmount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var basicAmount = dr.Cells["GTxtDisplayAmount"].Value.GetDecimal();
        //                cmdString.Append($"{basicAmount},");
        //                cmdString.Append($"{termAmount},");

        //                cmdString.Append($"{dr.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtAltQty"].Value.GetDecimal()},");
        //                cmdString.Append($"{dr.Cells["GTxtQty"].Value.GetDecimal()},");
        //                cmdString.Append(dr.Cells["GTxtNarration"].Value.IsValueExits() is true
        //                    ? $"'{dr.Cells["GTxtNarration"].Value}',"
        //                    : "NULL,");
        //                cmdString.Append("NULL,0,NULL,0,");

        //                var taxAmount = dr.Cells["GTxtValueTaxableAmount"].Value.GetDecimal();
        //                var vAmount = dr.Cells["GTxtValueVatAmount"].Value.GetDecimal();
        //                var vRate = dr.Cells["GTxtTaxPriceRate"].Value.GetDecimal();

        //                cmdString.Append($"{taxAmount},{vAmount},{vRate}, 0,  0, 0, 0, 0, 0, 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N',");

        //                var pDiscountRate = dr.Cells["GTxtDiscountRate"].Value.GetDecimal();
        //                var pDiscount = dr.Cells["GTxtPDiscount"].Value.GetDecimal();
        //                var bDiscount = dr.Cells["GTxtValueBDiscount"].Value.GetDecimal();

        //                cmdString.Append($"{pDiscountRate},{pDiscount},{SbMaster.TermRate},{bDiscount},0,0,NULL,NULL,NULL,NULL,1,NULL");
        //                cmdString.Append(iRows == SbMaster.GetView.RowCount ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }

        //            for (var i = 0; i < SbMaster.GetView.Rows.Count; i++)
        //            {
        //                var val = SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal();
        //                if (val <= 0)
        //                {
        //                    continue;
        //                }

        //                cmdString.Append(@"
        //                INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //                cmdString.Append($"\n VALUES('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{ObjGlobal.SalesDiscountTermId},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtSNo"].Value},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtDiscountRate"].Value.GetDecimal()},");
        //                cmdString.Append($"{SbMaster.GetView.Rows[i].Cells["GTxtPDiscount"].Value.GetDecimal()},");
        //                cmdString.Append($"'P',{SbMaster.GetView.Rows[i].Cells["GTxtProductId"].Value},'N',NULL,NULL,NULL,NULL,1,NULL); \n");
        //            }
        //        }

        //        if (SbMaster.SpecialDiscount > 0)
        //        {
        //            cmdString.Append(@"
        //            INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //            cmdString.Append($"\n VALUES( '{SbMaster.SB_Invoice}',{ObjGlobal.SalesSpecialDiscountTermId},1,");
        //            cmdString.Append($"{SbMaster.SpecialDiscountRate.GetDecimal()},{SbMaster.SpecialDiscount.GetDecimal()},");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL); \n");
        //        }

        //        if (SbMaster.ServiceCharge > 0)
        //        {
        //            cmdString.Append(@"
        //            INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //            cmdString.Append($" \n VALUES( '{SbMaster.SB_Invoice}',{ObjGlobal.SalesServiceChargeTermId},1,");
        //            cmdString.Append(
        //                $"{SbMaster.ServiceChargeRate.GetDecimal()},{SbMaster.ServiceCharge.GetDecimal()},");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL); \n");
        //        }

        //        if (SbMaster.VatAmount > 0)
        //        {
        //            cmdString.Append(@"
        //            INSERT INTO AMS.SB_Term (SB_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
        //            cmdString.Append($"\n VALUES('{SbMaster.SB_Invoice}',{ObjGlobal.SalesVatTermId},1,");
        //            cmdString.Append($"{SbMaster.VatRate.GetDecimal()},{SbMaster.VatAmount.GetDecimal()},");
        //            cmdString.Append("'B',NULL,'N',NULL,NULL,NULL,NULL,1,NULL); \n");
        //        }

        //        if (SbMaster.GetPaymentInformation is { RowCount: > 0 })
        //        {
        //            cmdString.Append("INSERT INTO AMS.InvoiceSettlement(SB_Invoice, LedgerId, PaymentMode, Amount) \n");
        //            cmdString.Append("VALUES");
        //            foreach (DataGridViewRow dr in SbMaster.GetPaymentInformation.Rows)
        //            {
        //                cmdString.Append($"('{SbMaster.SB_Invoice}',");
        //                cmdString.Append($"{dr.Cells["GTxtLedgerId"].Value.GetInt()},");
        //                cmdString.Append($"'{dr.Cells["GTxtPaymentMode"].Value.GetString()}',");
        //                cmdString.Append($"{dr.Cells["GTxtAmount"].Value.GetDecimal()}");
        //                cmdString.Append(dr.Index == SbMaster.GetPaymentInformation.RowCount - 1 ? " );" : "),");
        //                cmdString.Append(" \n");
        //            }
        //        }
        //    }

        //    var iResult = SaveDataInDatabase(cmdString);
        //    if (iResult <= 0)
        //    {
        //        return iResult;
        //    }

        //    if (SbMaster.TableId.IsValueExits())
        //    {
        //        var cmdText = $@"
        //        UPDATE AMS.TableMaster SET TableStatus ='A' WHERE TableId = {SbMaster.TableId}";
        //        var result = ExecuteCommand.ExecuteNonQuery(cmdText);
        //    }

        //    AuditLogSalesInvoice(actionTag);
        //    if (actionTag != "UPDATE" && strNamesArray.Contains(actionTag))
        //    {
        //        return iResult;
        //    }

        //    _ = SalesTermPostingAsync();
        //    _ = SalesInvoiceAccountPosting();
        //    _ = SalesInvoiceStockPosting();
        //    if (SbMaster.SC_Invoice.IsValueExits() || SbMaster.SO_Invoice.IsValueExits())
        //    {
        //        UpdateOrderChallanOnInvoice();
        //    }

        //    if (!ObjGlobal.IsIrdRegister)
        //    {
        //        return iResult;
        //    }

        //    PostingBillToApi(invoiceNo);
        //    return iResult;
        //}
        //catch (Exception e)
        //{
        //    e.ToNonQueryErrorResult(e.StackTrace);
        //    e.DialogResult();
        //    return 0;
        //}

        return 0;
    }
    public async Task<int> SyncSalesInvoiceAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Sales/GetUnSynchronizedSales",
            InsertUrl = @$"{_configParams.Model.Item2}Sales/InsertSalesList",
            UpdateUrl = @$"{_configParams.Model.Item2}Sales/UpdateSales"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var salesNewRepo = DataSyncProviderFactory.GetRepository<SB_Master>(_injectData);
        // pull all new sales data
        if (ObjGlobal.IsSalesInvoiceSync)
        {
            var result = await PullUnSyncSales(salesNewRepo);
            if (!result)
                return 0;
        }
        // push all new sales data
        var sqlQuery = @"SELECT *FROM AMS.SB_Master WHERE ISNULL(IsSynced,0)=0";
        var queryResponse = await QueryUtils.GetListAsync<SB_Master>(sqlQuery);
        var saList = queryResponse.List.ToList();
        if (saList.Count > 0)
        {
            var loopCount = 1;
            if (saList.Count > 100)
            {
                loopCount = saList.Count / 100 + 1;
            }
            var newSaList = new List<SB_Master>();
            var cmdString = new StringBuilder();
            var salesList = new List<SB_Master>();
            for (var i = 0; i < loopCount; i++)
            {
                newSaList.Clear();
                newSaList.AddRange(i == 0 ? saList.Take(100) : saList.Skip(100 * i).Take(100));
                salesList.Clear();
                foreach (var sa in newSaList)
                {
                    sqlQuery = $@"SELECT *FROM AMS.SB_Details WHERE SB_Invoice='{sa.SB_Invoice}'";
                    var dtlQueryResponse = await QueryUtils.GetListAsync<SB_Details>(sqlQuery);
                    var sadList = dtlQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.SB_Term WHERE SB_Vno='{sa.SB_Invoice}'";
                    var pcTermQueryResponse = await QueryUtils.GetListAsync<SB_Term>(sqlQuery);
                    var saTermList = pcTermQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.ProductAddInfo WHERE VoucherNo='{sa.SB_Invoice}' AND Module='SB'";
                    var pAddInfoQueryResponse = await QueryUtils.GetListAsync<ProductAddInfo>(sqlQuery);
                    var prdAddInfoList = pAddInfoQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.SB_Master_OtherDetails WHERE SB_Invoice='{sa.SB_Invoice}'";
                    var saOtherDtlQuery = await QueryUtils.GetListAsync<SB_Master_OtherDetails>(sqlQuery);
                    var otherDetailsList = saOtherDtlQuery.List.ToList();

                    var sbMaster = new SB_Master();
                    sbMaster = sa;
                    sbMaster.DetailsList = sadList;
                    sbMaster.Terms = saTermList;
                    sbMaster.ProductAddInfoModels = prdAddInfoList;
                    sbMaster.OtherDetailsList = otherDetailsList;
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
                            cmdString.Append($"UPDATE AMS.SB_Master SET IsSynced = 1 WHERE SB_Invoice = '{sa.SB_Invoice}'; \n");
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
    private int UpdateOrderChallanOnInvoice()
    {
        var cmdString = new StringBuilder();
        if (SbMaster.SO_Invoice.IsValueExits())
        {
            cmdString.Append($@" 
                UPDATE AMS.SO_Master SET Invoice_Type ='POSTED' WHERE SO_Invoice= '{SbMaster.SO_Invoice}';");
        }

        if (SbMaster.SC_Invoice.IsValueExits())
        {
            cmdString.Append($@" 
                UPDATE AMS.SC_Master SET Invoice_Type ='POSTED' WHERE SC_Invoice = '{SbMaster.SC_Invoice}';");
        }

        var result = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return result;
    }
    #endregion


    // PULL SALES INVOICE
    #region ---------- PULL SALES INVOICE ----------
    private void PostingBillToApi(string voucherNo)
    {
        _ = Task.Run(() => _apiSync.SyncSalesBillApiAsync(voucherNo));
    }
    private async Task<bool> PullUnSyncSales(IDataSyncRepository<SB_Master> salesNewRepo)
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
                    _dataEntry.SaveUnSyncSalesFromServerAsync(data, "SAVE");
                }
                SplashScreenManager.CloseForm();
            }

            if (pullResponse.IsReCall)
                await PullUnSyncSales(salesNewRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    #endregion


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


    // AUDIT LOG
    #region -------------------- AUDIT LOG --------------------
    private int AuditLogSalesInvoice(string actionTag)
    {
        var cmdString = @$"
		    INSERT INTO AUD.AUDIT_SB_Master (SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Subledger_Id, Agent_Id, CounterId, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, SO_Invoice, SO_Date, SC_Invoice, SC_Date, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, DoctorId, PatientId, HDepartmentId, MShipId, TableId, CBranch_Id, CUnit_Id, FiscalYearId, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, Cancel_Remarks, IsAPIPosted, IsRealtime, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SB_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Customer_Id, Subledger_Id, Agent_Id, CounterId, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate, Invoice_Type, Invoice_Mode, Payment_Mode, DueDays, DueDate, SO_Invoice, SO_Date, SC_Invoice, SC_Date, B_Amount, T_Amount, N_Amount, LN_Amount, V_Amount, Tbl_Amount, Tender_Amount, Return_Amount, Action_Type, In_Words, Remarks, R_Invoice, Is_Printed, No_Print, Printed_By, Printed_Date, DoctorId, PatientId, HDepartmentId, MShipId, TableId, CBranch_Id, CUnit_Id, FiscalYearId, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, Cleared_By, Cleared_Date, Cancel_By, Cancel_Date, '{SbMaster.Remarks ?? string.Empty}', IsAPIPosted, IsRealtime, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, '{actionTag}', '{ObjGlobal.LogInUser.ToUpper()}', GETDATE() FROM AMS.SB_Master sm WHERE sm.SB_Invoice='{SbMaster.SB_Invoice}'
		
            INSERT INTO AUD.AUDIT_SB_Details (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, SO_Invoice, SO_Sno, SC_Invoice, SC_SNo, Tax_Amount, V_Amount, V_Rate, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, S_Ledger, SR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, MaterialPost, PDiscountRate, PDiscount, BDiscountRate, BDiscount, ServiceChargeRate, ServiceCharge, '{actionTag}', '{ObjGlobal.LogInUser.ToUpper()}', GETDATE()	FROM AMS.SB_Details sd WHERE sd.SB_Invoice='{SbMaster.SB_Invoice}'
		
            INSERT INTO AUD.AUDIT_SB_Term (SB_VNo, ST_Id, Product_Id, SNo, Term_Type, Rate, Amount, Taxable, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SB_VNo, ST_Id, Product_Id, SNo, Term_Type, Rate, Amount, Taxable, '{actionTag}', '{ObjGlobal.LogInUser.ToUpper()}', GETDATE() FROM AMS.SB_Term st WHERE st.SB_VNo='{SbMaster.SB_Invoice}'
		
            INSERT INTO AUD.AUDIT_SB_ExchangeDetails (SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, ExchangeGLD, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, N_Amount, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SB_Invoice, Invoice_SNo, P_Id, Gdn_Id, ExchangeGLD, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, N_Amount, '{actionTag}', '{ObjGlobal.LogInUser.ToUpper()}', GETDATE() FROM AMS.SB_ExchangeDetails sed WHERE sed.SB_Invoice ='{SbMaster.SB_Invoice}'
		
            INSERT INTO AUD.AUDIT_SB_Master_OtherDetails (SB_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails, ModifyAction, ModifyBy, ModifyDate)
		    SELECT SB_Invoice, Transport, VechileNo, BiltyNo, Package, BiltyDate, BiltyType, Driver, PhoneNo, LicenseNo, MailingAddress, MCity, MState, MCountry, MEmail, ShippingAddress, SCity, SState, SCountry, SEmail, ContractNo, ContractDate, ExportInvoice, ExportInvoiceDate, VendorOrderNo, BankDetails, LcNumber, CustomDetails, '{actionTag}', '{ObjGlobal.LogInUser.ToUpper()}', GETDATE() FROM AMS.SB_Master_OtherDetails smod WHERE smod.SB_Invoice ='{SbMaster.SB_Invoice}'             ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    #endregion


    // ACCOUNTING LEDGER POSTING
    #region ---------- ACCOUNTING LEDGER POSTING ----------
    public int SalesInvoiceAccountPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			DELETE AMS.AccountDetails WHERE Module='SB' ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits()
            ? $@" AND Voucher_No ='{SbMaster.SB_Invoice}' "
            : string.Empty);
        cmdString.Append($@"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM ( SELECT 'SB' AS Module, ROW_NUMBER() OVER (PARTITION BY sm.SB_Invoice ORDER BY sm.SB_Invoice) AS Serial_No, sm.SB_Invoice AS Voucher_No, sm.Invoice_Date AS Voucher_Date, sm.Invoice_Miti AS Voucher_Miti, sm.Invoice_Time AS Voucher_Time,ISNULL(ss.LedgerId, sm.Customer_Id) AS Ledger_ID, {ObjGlobal.SalesLedgerId} AS CbLedger_ID, sm.Subledger_Id AS Subleder_ID, sm.Agent_Id AS Agent_ID, sm.Cls1 AS Department_ID1, sm.Cls2 AS Department_ID2, sm.Cls3 AS Department_ID3, sm.Cls4 AS Department_ID4, sm.Cur_Id AS Currency_ID, sm.Cur_Rate AS Currency_Rate,ISNULL(ss.Amount,sm.N_Amount) AS Debit_Amt, 0 AS Credit_Amt,ISNULL(ss.Amount, sm.LN_Amount) AS LocalDebit_Amt, 0 AS LocalCredit_Amt, sm.DueDate AS DueDate, sm.DueDays AS DueDays, NULL AS Narration, sm.Remarks AS Remarks, sm.Enter_By AS EnterBy, sm.Enter_Date AS EnterDate, sm.PB_Vno AS RefNo, sm.Vno_Date AS RefDate, sm.Reconcile_By AS Reconcile_By, sm.Reconcile_Date AS Reconcile_Date, sm.Auth_By AS Authorize_By, sm.Auth_Date AS Authorize_Date, sm.Cleared_By AS Clearing_By, sm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, sm.ChqNo AS Cheque_No, sm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, sm.Party_Name AS PartyName, NULL AS PartyLedger_Id, sm.Vat_No AS Party_PanNo, sm.CBranch_Id AS Branch_ID, sm.CUnit_Id AS CmpUnit_ID, sm.FiscalYearId AS FiscalYearId, sm.DoctorId AS DoctorId, sm.PatientId AS PatientId, sm.HDepartmentId AS HDepartmentId, sm.SyncGlobalId AS SyncGlobalId, sm.SyncOriginId AS SyncOriginId, sm.SyncCreatedOn AS SyncCreatedOn, SyncLastPatchedOn, sm.SyncRowVersion AS SyncRowVersion, sm.SyncBaseId AS SyncBaseId
				 FROM AMS.SB_Master sm
					LEFT OUTER JOIN AMS.InvoiceSettlement ss ON ss.SB_Invoice = sm.SB_Invoice
				 WHERE (sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL')");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits()
            ? $@" AND sm.SB_Invoice ='{SbMaster.SB_Invoice}' "
            : string.Empty);
        cmdString.Append(@"
			UNION ALL
			SELECT 'SB' AS Module,ROW_NUMBER() OVER (PARTITION BY sd.SB_Invoice ORDER BY sd.SB_Invoice) AS Serial_No, sd.SB_Invoice AS Voucher_No, sm.Invoice_Date AS Voucher_Date, sm.Invoice_Miti AS Voucher_Miti, sm.Invoice_Time AS Voucher_Time, ISNULL(p.PSL, (SELECT SBLedgerId FROM AMS.SalesSetting)) Ledger_ID, sm.Customer_Id AS CbLedger_ID, sm.Subledger_Id AS Subleder_ID, sm.Agent_Id AS Agent_ID, sm.Cls1 AS Department_ID1, sm.Cls2 AS Department_ID2, sm.Cls3 AS Department_ID3, sm.Cls4 AS Department_ID4, sm.Cur_Id AS Currency_ID, sm.Cur_Rate AS Currency_Rate, 0 AS Debit_Amt, SUM(sd.B_Amount) AS Credit_Amt, 0 AS LocalDebit_Amt, SUM(sd.B_Amount)* sm.Cur_Rate AS LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, sm.Enter_By AS EnterBy, sm.Enter_Date AS EnterDate, sm.PB_Vno AS RefNo, sm.Vno_Date AS RefDate, sm.Reconcile_By AS Reconcile_By, sm.Reconcile_Date AS Reconcile_Date, sm.Auth_By AS Authorize_By, sm.Auth_Date AS Authorize_Date, sm.Cleared_By AS Clearing_By, sm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, sm.ChqNo AS Cheque_No, sm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, sm.Party_Name AS PartyName, NULL AS PartyLedger_Id, sm.Vat_No AS Party_PanNo, sm.CBranch_Id AS Branch_ID, sm.CUnit_Id AS CmpUnit_ID, sm.FiscalYearId AS FiscalYearId, sm.DoctorId AS DoctorId, sm.PatientId AS PatientId, sm.HDepartmentId AS HDepartmentId, sd.SyncGlobalId AS SyncGlobalId, sd.SyncOriginId AS SyncOriginId, sd.SyncCreatedOn AS SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion AS SyncRowVersion, sd.SyncBaseId AS SyncBaseId
			 FROM AMS.SB_Details sd
				  INNER JOIN AMS.SB_Master sm ON sd.SB_Invoice=sm.SB_Invoice
				  INNER JOIN AMS.Product p ON sd.P_Id=p.PID
			 WHERE (sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL') ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $@" AND sd.SB_Invoice ='{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
			GROUP BY sd.SB_Invoice,sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time, sm.Customer_Id, sm.Subledger_Id, sm.Agent_Id, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, Narration, Remarks, sm.Enter_By, sm.Enter_Date, sm.PB_Vno, sm.Vno_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId, sm.DoctorId, sm.PatientId, sm.HDepartmentId, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, p.PSL
			UNION ALL
			SELECT 'SB' AS Module,ROW_NUMBER() OVER (PARTITION BY st.SB_VNo ORDER BY st.SB_VNo) AS Serial_No, st.SB_VNo AS Voucher_No, sm.Invoice_Date AS Voucher_Date, sm.Invoice_Miti AS Voucher_Miti, sm.Invoice_Time AS Voucher_Time, st1.Ledger AS Ledger_ID, sm.Customer_Id AS CbLedger_ID, sm.Subledger_Id AS Subleder_ID, sm.Agent_ID AS Agent_ID, sm.Cls1 AS Department_ID1, sm.Cls2 AS Department_ID2, sm.Cls3 AS Department_ID3, sm.Cls4 AS Department_ID4, sm.Cur_Id AS Currency_ID, sm.Cur_Rate AS Currency_Rate, SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END) AS Debit_Amt, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END) AS Credit_Amt, SUM(CASE WHEN st1.ST_Sign='-' THEN st.Amount ELSE 0 END)* sm.Cur_Rate AS LocalDebit_Amt, SUM(CASE WHEN st1.ST_Sign='+' THEN st.Amount ELSE 0 END)* sm.Cur_Rate AS LocalCredit_Amt, DueDate, DueDays, NULL AS Narration, sm.Remarks, sm.Enter_By AS EnterBy, sm.Enter_Date AS EnterDate, sm.PB_Vno AS RefNo, sm.Vno_Date AS RefDate, sm.Reconcile_By AS Reconcile_By, sm.Reconcile_Date AS Reconcile_Date, sm.Auth_By AS Authorize_By, sm.Auth_Date AS Authorize_Date, sm.Cleared_By AS Clearing_By, sm.Cleared_Date AS Clearing_Date, NULL AS Posted_By, NULL AS Posted_Date, sm.ChqNo AS Cheque_No, sm.ChqDate AS Cheque_Date, NULL AS Cheque_Miti, sm.Party_Name AS PartyName, NULL AS PartyLedger_Id, sm.Vat_No AS Party_PanNo, sm.CBranch_Id AS Branch_ID, sm.CUnit_Id AS CmpUnit_ID, sm.FiscalYearId AS FiscalYearId, sm.DoctorId AS DoctorId, sm.PatientId AS PatientId, sm.HDepartmentId AS HDepartmentId, st.SyncGlobalId AS SyncGlobalId, st.SyncOriginId AS SyncOriginId, st.SyncCreatedOn AS SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion AS SyncRowVersion, st.SyncBaseId AS SyncBaseId
			FROM AMS.SB_Term st
				LEFT OUTER JOIN AMS.SB_Master sm ON st.SB_VNo=sm.SB_Invoice
				LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id=st1.ST_Id
			 WHERE st.Term_Type IN ('B','P') AND (sm.R_Invoice=0 AND sm.Action_Type <> 'CANCEL') ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $@" AND st.SB_VNo ='{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
			GROUP BY st.SB_VNo,sm.Invoice_Date, sm.Invoice_Miti, sm.Invoice_Time, sm.Customer_Id, sm.Subledger_Id, sm.Agent_Id, sm.Cls1, sm.Cls2, sm.Cls3, sm.Cls4, sm.Cur_Id, sm.Cur_Rate, DueDate, DueDays, st1.ST_Sign, Remarks, sm.Enter_By, sm.Enter_Date, sm.PB_Vno, sm.Vno_Date, sm.Reconcile_By, sm.Reconcile_Date, sm.Auth_By, sm.Auth_Date, sm.Cleared_By, sm.Cleared_Date, sm.ChqNo, sm.ChqDate, sm.Party_Name, sm.Vat_No, sm.CBranch_Id, sm.CUnit_Id, sm.FiscalYearId, sm.DoctorId, sm.PatientId, sm.HDepartmentId, st.SyncGlobalId, st.SyncOriginId, st.SyncCreatedOn, st.SyncLastPatchedOn, st.SyncRowVersion, st.SyncBaseId, st1.Ledger ) Account ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return isResult;
    }
    #endregion


    // SALES TERM
    #region ---------- SALES TERM ----------
    public int SalesTermPostingAsync()
    {
        var cmdString = @"
		    DELETE AMS.SB_Term WHERE Term_Type='BT' ";
        if (SbMaster.SB_Invoice.IsValueExits())
        {
            cmdString += $" AND SB_VNo='{SbMaster.SB_Invoice}' ";
        }
        cmdString += $@"
            INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT sbt.SB_VNo SB_VNo, sbt.ST_Id ST_Id, sd.Invoice_SNo AS SNo, 'BT' Term_Type, sd.P_Id Product_Id, 
		             CASE WHEN sm.Invoice_Type='P VAT' AND ISNULL(P.PTax, 0)=0 AND sbt.ST_Id={ObjGlobal.SalesVatTermId} THEN 0 ELSE sbt.Rate END Rate, 
		             CASE WHEN sm.Invoice_Type='P VAT' AND ISNULL(P.PTax, 0)=0 AND sbt.ST_Id={ObjGlobal.SalesVatTermId} THEN 0 
			              WHEN sm.Invoice_Type='P VAT' AND ISNULL(P.PTax, 0)>0 AND sbt.ST_Id={ObjGlobal.SalesVatTermId} AND ISNULL(sm.B_Amount,0) > 0 THEN (sbt.Amount / ISNULL(taxable.NetAmount, 0))* sd.N_Amount WHEN ISNULL(sm.B_Amount,0) > 0 THEN (sbt.Amount / sm.B_Amount)* sd.N_Amount ELSE 0 END  Amount, 
			              CASE WHEN ISNULL(P.PTax, 0)>0 AND sbt.ST_Id={ObjGlobal.SalesVatTermId} THEN 'Y' ELSE 'N' END Taxable, sbt.SyncBaseId, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion
            FROM AMS.SB_Details sd
                 LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
                 LEFT OUTER JOIN AMS.SB_Term sbt ON sd.SB_Invoice=sbt.SB_VNo
                 LEFT OUTER JOIN AMS.Product P ON P.PID=sd.P_Id
                 LEFT OUTER JOIN(SELECT sd1.SB_Invoice, SUM(CASE WHEN ISNULL(p1.PTax, 0)>0 THEN sd1.N_Amount ELSE 0 END) NetAmount
                                 FROM AMS.SB_Details sd1
                                      LEFT OUTER JOIN AMS.Product p1 ON p1.PID=sd1.P_Id
                                 GROUP BY sd1.SB_Invoice) AS taxable ON taxable.SB_Invoice=sd.SB_Invoice
            WHERE sbt.Term_Type='B' AND Product_Id IS NULL ";
        if (SbMaster.SB_Invoice.IsValueExits())
        {
            cmdString += $" AND sbt.SB_VNo='{SbMaster.SB_Invoice}' ";
        }

        return SqlExtensions.ExecuteNonTrans(cmdString);
    }
    #endregion

    // RESTAURANT MANAGEMENT SYSTEM
    #region ---------- RESTAURANT MANAGEMENT SYSTEM ----------
    private int UpdateSalesTableStatus()
    {
        var cmdString = @$"
            UPDATE AMS.TableMaster SET TableStatus='A' WHERE TableStatus <> 'A' AND  TableId = {SbMaster.TableId}  ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    private int UpdateSalesOrderStatus()
    {
        var cmdString = @$"
            UPDATE AMS.SO_Master SET Invoice_Type='POSTED' WHERE SO_Invoice='{SbMaster.SO_Invoice}' AND Invoice_Type <> 'POSTED'  AND Invoice_Mode='RSO'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    #endregion

    // STOCK LEDGER POSTING
    #region ---------- STOCK LEDGER POSTING ----------
    public int SalesInvoiceStockPosting()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(" DELETE AMS.StockDetails WHERE module='SB' \n");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $" AND Voucher_No='{SbMaster.SB_Invoice}'; \n" : " ");
        cmdString.Append(SbMaster.SC_Invoice.IsValueExits()
            ? $@" DELETE AMS.StockDetails WHERE module ='SC' AND Voucher_No='{SbMaster.SC_Invoice}';"
            : " ");
        cmdString.Append(@"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Branch_Id, CmpUnit_Id, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, FiscalYearId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId
			FROM (SELECT 'SB' Module, sd.SB_Invoice Voucher_No, sd.Invoice_SNo Serial_No, sm.PB_Vno PurRefVno, sm.Invoice_Date Voucher_Date, sm.Invoice_Miti Voucher_Miti, sm.Invoice_Time Voucher_Time, sm.Customer_Id Ledger_Id, sm.Subledger_Id Subledger_Id, sm.Agent_Id Agent_Id, sm.Cls1 Department_Id1, sm.Cls2 Department_Id2, sm.Cls3 Department_Id3, sm.Cls4 Department_Id4, sm.Cur_Id Currency_Id, sm.Cur_Rate Currency_Rate, sd.P_Id Product_Id, sd.Gdn_Id Godown_Id, NULL CostCenter_Id, sd.Alt_Qty AltQty, sd.Alt_UnitId AltUnit_Id, sd.Qty Qty, sd.Unit_Id Unit_Id, sd.AltStock_Qty AltStockQty, sd.Stock_Qty StockQty, sd.Free_Qty FreeQty, sd.Free_Unit_Id FreeUnit_Id, sd.StockFree_Qty StockFreeQty, 0 ConvRatio, 0 ExtraFreeQty, NULL ExtraFreeUnit_Id, 0 ExtraStockFreeQty, sd.Rate Rate, sd.B_Amount BasicAmt, sd.T_Amount TermAmt, sd.N_Amount NetAmt, ISNULL(stockval.StockValue, 0) BillTermAmt, sd.V_Rate TaxRate, sd.Tax_Amount TaxableAmt, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) DocVal, 0 ReturnVal, (sd.N_Amount + ISNULL(stockval.StockValue, 0)) stockval, 0 AddStockVal, sm.PB_Vno PartyInv, 'O' EntryType, sm.Auth_By AuthBy, sm.Auth_Date AuthDate, sm.Reconcile_By RecoBy, sm.Reconcile_Date RecoDate, sm.CounterId Counter_Id, NULL RoomNo, sm.Enter_By EnterBy, sm.Enter_Date EnterDate, sm.CBranch_Id Branch_Id, sm.CUnit_Id CmpUnit_Id, 0 Adj_Qty, NULL Adj_VoucherNo, NULL Adj_Module, sd.Rate SalesRate, FiscalYearId, sm.SyncGlobalId, sm.SyncOriginId, sm.SyncCreatedOn, sm.SyncLastPatchedOn, sm.SyncRowVersion, sm.SyncBaseId
				FROM AMS.SB_Details sd
				INNER JOIN AMS.Product p ON p.PID = sd.P_Id
				INNER JOIN AMS.SB_Master sm ON sd.SB_Invoice = sm.SB_Invoice
				LEFT OUTER JOIN (SELECT st.Product_Id, st.SB_VNo, st.SNo, SUM(CASE WHEN st1.ST_Sign = '-' THEN -st.Amount ELSE st.Amount END) StockValue
					FROM AMS.SB_Term st
					LEFT OUTER JOIN AMS.ST_Term st1 ON st.ST_Id = st1.ST_Id
					WHERE st1.ST_Profitability > 0
					AND st.Term_Type <> 'B' ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? $@" AND st.SB_VNo = '{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
					GROUP BY st.Product_Id, st.SB_VNo, st.SNo
				) stockval ON stockval.Product_Id = sd.P_Id AND sd.SB_Invoice = stockval.SB_VNo AND sd.Invoice_SNo = stockval.SNo
				WHERE p.PType IN ('I','Inventory')  AND sm.R_Invoice = 0 AND sm.Action_Type <> 'CANCEL' ");
        cmdString.Append(SbMaster.SB_Invoice.IsValueExits() ? @$" AND sd.SB_Invoice = '{SbMaster.SB_Invoice}' " : " ");
        cmdString.Append(@"
				) AS Stock ");
        var isResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (isResult == 0)
        {
            return isResult;
        }

        if (SbMaster.SC_Invoice.IsValueExits())
        {
            var update =
                "UPDATE AMS.StockDetails SET Adj_VoucherNo = SD.SC_Invoice, Adj_Qty = sd.Qty, Adj_Module = 'SC' FROM AMS.SB_Details sd WHERE Voucher_No = sd.SB_Invoice AND Product_Id = sd.P_Id ";
            update += SbMaster.SB_Invoice.IsValueExits() ? $" AND Voucher_No='{SbMaster.SB_Invoice}';" : " ";
            var ok = SqlExtensions.ExecuteNonQuery(update);
        }

        return isResult;
    }
    #endregion


    //METHOD FOR THIS CLASS
    #region ------- FUNCTION --------------
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

    #endregion


    // DATA TABLE FUNCTION
    #region ---------- CHECK REFRENCE VOUCHER NO ----------
    public DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo)
    {
        var cmdString = $@" 
            SELECT PB_Vno FROM AMS.PB_Master where Vendor_ID = '{ledgerId}' and PB_Vno = '{txtRefVno}'  AND FiscalYearId = {ObjGlobal.SysFiscalYearId} ";
        if (action != "SAVE") cmdString += $" AND PB_Invoice <> '{voucherNo}' and Vendor_ID <> '{ledgerId}'  ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataSet ReturnTempSalesInvoiceDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}';
			SELECT GL.GLCode, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
			 FROM AMS.temp_SB_Master AS PIM
				  INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID = PIM.Customer_Id
				  LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId = PIM.Subledger_Id
				  LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId = PIM.Agent_Id
				  LEFT OUTER JOIN AMS.Currency AS C ON C.CId = PIM.Cur_Id
				  LEFT OUTER JOIN AMS.Department AS D ON D.DId = PIM.Cls1
			 WHERE PIM.SB_Invoice = @voucherNo;
			SELECT sd.SB_Invoice, sd.Invoice_SNo, sd.P_Id, P.PName, P.PShortName, sd.Gdn_Id, G.GName, G.GCode, sd.Alt_Qty, sd.Alt_UnitId, ALTU.UnitCode AltUnitCode, sd.Qty, sd.Unit_Id, sd.Rate, U.UnitCode, sd.B_Amount, sd.T_Amount, sd.N_Amount, sd.AltStock_Qty, sd.Stock_Qty, sd.Narration, sd.SO_Invoice, sd.SO_Sno, sd.SC_Invoice, sd.SC_SNo, sd.Tax_Amount, sd.V_Amount, sd.V_Rate, sd.Free_Unit_Id, sd.Free_Qty, sd.StockFree_Qty, sd.ExtraFree_Unit_Id, sd.ExtraFree_Qty, sd.ExtraStockFree_Qty, sd.T_Product, sd.S_Ledger, sd.SR_Ledger, sd.SZ1, sd.SZ2, sd.SZ3, sd.SZ4, sd.SZ5, sd.SZ6, sd.SZ7, sd.SZ8, sd.SZ9, sd.SZ10, sd.Serial_No, sd.Batch_No, sd.Exp_Date, sd.Manu_Date, sd.MaterialPost, sd.PDiscountRate, sd.PDiscount, sd.BDiscountRate, sd.BDiscount, sd.ServiceChargeRate, sd.ServiceCharge, sd.SyncGlobalId, sd.SyncOriginId, sd.SyncCreatedOn, sd.SyncLastPatchedOn, sd.SyncRowVersion, sd.SyncBaseId, P.PTax
			 FROM AMS.temp_SB_Details sd
				  INNER JOIN AMS.Product AS P ON P.PID = sd.P_Id
				  LEFT OUTER JOIN AMS.Godown AS G ON G.GID = sd.Gdn_Id
				  LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				  LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			 WHERE sd.SB_Invoice = @voucherNo; ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    #endregion


    // VALUE RETURN IN STRING
    #region ---------- VALUE RETURN IN STRING  ----------
    public (string invoice, string avtInvoice) GetPointOfSalesDesign()
    {
        var cmdString = @" 
            SELECT TOP (1) Paper_Name FROM AMS.DocumentDesignPrint WHERE Module = 'SB'";
        var invoiceDesign = cmdString.GetQueryData();
        cmdString = @" 
            SELECT TOP (1) Paper_Name FROM AMS.DocumentDesignPrint WHERE Module = 'AVT'";
        var avtDesign = cmdString.GetQueryData();
        return (invoiceDesign, avtDesign);
    }
    public string GetInvoicePaymentMode(long ledgerId)
    {
        var cmdString = $@"
            SELECT TOP 1 COUNT(sm.Payment_Mode) CountNo, sm.Payment_Mode
            FROM AMS.SB_Master sm
            WHERE sm.Customer_Id = '{ledgerId}'
            GROUP BY sm.Payment_Mode
            ORDER BY COUNT(sm.Payment_Mode) DESC ";
        var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (result.Rows.Count > 0)
        {
            return result.Rows[0]["Payment_Mode"].GetString();
        }

        return string.Empty;
    }
    #endregion


    // OBJECT FOR THIS CLASS
    #region ---------- OBJECT FOR THIS CLASS ----------
    public SB_Master SbMaster { get; set; } = new();
    public List<SB_Details> DetailsList { get; set; } = new();
    public List<SB_Term> Terms { get; set; } = new();
    public List<SB_ExchangeDetails> SB_ExchangeDetails { get; set; }
    public SB_Master_OtherDetails SbOther { get; set; } = new();
    public List<ProductAddInfo> AddInfos { get; set; } = new();
    #endregion

    #region ---------- OBJECT CLASS ----------
    private readonly ClsIrdApiSync _apiSync = new();
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    private readonly ISalesEntry _dataEntry = new ClsSalesEntry();
    #endregion
}