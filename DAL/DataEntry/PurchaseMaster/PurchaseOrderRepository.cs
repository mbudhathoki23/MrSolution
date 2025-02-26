using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseOrder;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.PurchaseMaster;

public class PurchaseOrderRepository : IPurchaseOrderRepository
{
    // PURCHASE CHALLAN REPOSITORY
    public PurchaseOrderRepository()
    {
        PoMaster = new PO_Master();
        DetailsList = new List<PO_Details>();
        Terms = new List<PO_Term>();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }


    // INSERT UPDATE DELETE
    public int SavePurchaseOrder(string actionTag)
    {
        var cmdString = new StringBuilder();

        if (actionTag is "DELETE" or "UPDATE" or "REVERSE")
        {
            if (!actionTag.Equals("UPDATE"))
            {
                AuditLogPurchaseOrder(actionTag);
            }
            if (actionTag != "REVERSE")
            {
                cmdString.Append($"DELETE AMS.PO_Term WHERE PO_VNo = '{PoMaster.PO_Invoice}'; \n");
                cmdString.Append($"DELETE AMS.PO_Details WHERE PO_Invoice='{PoMaster.PO_Invoice}'; \n");
            }
            if (actionTag is "DELETE")
            {
                cmdString.Append($"DELETE AMS.PO_Master WHERE PO_Invoice ='{PoMaster.PO_Invoice}'; \n");
            }
        }

        if (actionTag.ToUpper() is "SAVE")
        {
            cmdString.Append(@"
                INSERT INTO AMS.PO_Master (PO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,PB_Vno,Vno_Date,Vno_Miti,Vendor_ID,PartyLedgerId,Party_Name,Vat_No,Contact_Person,Mobile_No,Address,ChqNo,ChqDate,ChqMiti,Invoice_Type,Invoice_In,DueDays,DueDate,Agent_Id,Subledger_Id,PIN_Invoice,PIN_Date,PQT_Invoice,PQT_Date,Cls1,Cls2,Cls3,Cls4,Cur_Id,Cur_Rate,B_Amount,T_Amount,N_Amount,LN_Amount,V_Amount,Tbl_Amount,Action_type,R_Invoice,CancelBy,CancelDate,CancelRemarks,No_Print,In_Words,Remarks,Audit_Lock,Enter_By,Enter_Date,Reconcile_By,Reconcile_Date,Auth_By,Auth_Date,CBranch_Id,CUnit_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion,IsReverse,IsSynced) ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append($" ('{PoMaster.PO_Invoice}','{PoMaster.Invoice_Date.GetSystemDate()}', '{PoMaster.Invoice_Miti}',GETDATE(),");
            cmdString.Append($" '{PoMaster.PB_Vno}', '{PoMaster.Vno_Date.GetSystemDate()}', '{PoMaster.Vno_Miti}', {PoMaster.Vendor_ID}, ");
            cmdString.Append(PoMaster.PartyLedgerId > 0 ? $"{PoMaster.PartyLedgerId}," : "NULL,");
            cmdString.Append($" N'{PoMaster.Party_Name}', '{PoMaster.Vat_No}', '{PoMaster.Contact_Person}', '{PoMaster.Mobile_No}',");
            cmdString.Append($" N'{PoMaster.Address}', '{PoMaster.ChqNo}', '{PoMaster.ChqDate.GetSystemDate()}', '{PoMaster.ChqMiti}',");
            cmdString.Append($" '{PoMaster.Invoice_Type}', '{PoMaster.Invoice_In}', {PoMaster.DueDays}, '{PoMaster.DueDate.GetSystemDate()}', ");
            cmdString.Append(PoMaster.Agent_Id > 0 ? $"{PoMaster.Agent_Id}," : "NULL,");
            cmdString.Append(PoMaster.Subledger_Id > 0 ? $"{PoMaster.Subledger_Id}," : "NULL,");
            cmdString.Append($" '{PoMaster.PIN_Invoice}', '{PoMaster.PIN_Date.GetSystemDate()}', N'{PoMaster.PQT_Invoice}', '{PoMaster.PQT_Date.GetSystemDate()}',");
            cmdString.Append(PoMaster.Cls1 > 0 ? $"{PoMaster.Cls1}," : "NULL,");
            cmdString.Append($" {PoMaster.Cls2}, {PoMaster.Cls3}, {PoMaster.Cls4},");
            cmdString.Append(PoMaster.Cur_Id > 0 ? $"{PoMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append($" {PoMaster.Cur_Rate},{PoMaster.B_Amount},{PoMaster.T_Amount},{PoMaster.N_Amount},{PoMaster.LN_Amount},{PoMaster.V_Amount},{PoMaster.Tbl_Amount}, N'{PoMaster.Action_type}', CAST('{PoMaster.R_Invoice}' AS BIT),");
            cmdString.Append($" '{PoMaster.CancelBy}', '{PoMaster.CancelDate.GetSystemDate()}', '{PoMaster.CancelRemarks}',{PoMaster.No_Print}, N'{PoMaster.In_Words}', N'{PoMaster.Remarks}', CAST('{PoMaster.Audit_Lock}' AS BIT),");
            cmdString.Append($" N'{PoMaster.Enter_By}', '{PoMaster.Enter_Date.GetSystemDate()}', N'{PoMaster.Reconcile_By}', '{PoMaster.Reconcile_Date.GetSystemDate()}', N'{PoMaster.Auth_By}', '{PoMaster.Auth_Date.GetSystemDate()}',");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
            cmdString.Append($" NULL, NULL, NULL, NULL, NULL,");
            cmdString.Append(PoMaster.SyncBaseId.IsGuidExits() ? $"'{PoMaster.SyncBaseId}'," : "NULL,");
            cmdString.Append(PoMaster.SyncGlobalId.IsGuidExits() ? $"'{PoMaster.SyncGlobalId}'," : "NULL,");
            cmdString.Append(PoMaster.SyncOriginId.IsGuidExits() ? $"'{PoMaster.SyncOriginId}'," : "NULL,");
            cmdString.Append($" '{PoMaster.SyncCreatedOn.GetSystemDate()}','{PoMaster.SyncLastPatchedOn.GetSystemDate()}',");
            cmdString.Append($" {PoMaster.SyncRowVersion.GetDecimal(true)},");
            cmdString.Append(PoMaster.IsReverse.GetBool() ? $"CAST('{PoMaster.IsReverse}' AS BIT)," : "0,");
            cmdString.Append(PoMaster.IsSynced.GetBool() ? $"CAST('{PoMaster.IsSynced}' AS BIT)" : "0); \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.PO_Master SET");
            cmdString.Append($" PO_Invoice ='{PoMaster.PO_Invoice}',Invoice_Date ='{PoMaster.Invoice_Date.GetSystemDate()}',Invoice_Miti ='{PoMaster.Invoice_Miti}',Invoice_Time =GETDATE(),");
            cmdString.Append($" PB_Vno ='{PoMaster.PB_Vno}', Vno_Date ='{PoMaster.Vno_Date.GetSystemDate()}', Vno_Miti='{PoMaster.Vno_Miti}', Vendor_ID={PoMaster.Vendor_ID},");
            cmdString.Append(PoMaster.PartyLedgerId > 0 ? $"PartyLedgerId ={PoMaster.PartyLedgerId}," : $"PartyLedgerId =NULL,");
            cmdString.Append($" Party_Name = N'{PoMaster.Party_Name}', Vat_No ='{PoMaster.Vat_No}', Contact_Person ='{PoMaster.Contact_Person}', Mobile_No ='{PoMaster.Mobile_No}',");
            cmdString.Append($" Address =N'{PoMaster.Address}', ChqNo ='{PoMaster.ChqNo}', ChqDate ='{PoMaster.ChqDate.GetSystemDate()}', ChqMiti ='{PoMaster.ChqMiti}',");
            cmdString.Append($" Invoice_Type ='{PoMaster.Invoice_Type}', Invoice_In ='{PoMaster.Invoice_In}', DueDays = {PoMaster.DueDays},");
            cmdString.Append(PoMaster.DueDate != null ? $"DueDate ='{PoMaster.DueDate.GetSystemDate()}'," : $"DueDate =NULL,");
            cmdString.Append(PoMaster.Agent_Id > 0 ? $"Agent_Id ={PoMaster.Agent_Id}," : "Agent_Id =NULL,");
            cmdString.Append(PoMaster.Subledger_Id > 0 ? $"Subledger_Id ={PoMaster.Subledger_Id}," : "Subledger_Id =NULL,");
            cmdString.Append(PoMaster.PIN_Date != null ? $"PIN_Date ='{PoMaster.PIN_Date.GetSystemDate()}'," : $"PIN_Date =NULL,");
            cmdString.Append($"PQT_Invoice ='{PoMaster.PQT_Invoice}',");
            cmdString.Append(PoMaster.PQT_Date != null ? $"PQT_Date ='{PoMaster.PQT_Date.GetSystemDate()}'," : $"PQT_Date = NULL,");
            cmdString.Append(PoMaster.Cls1 > 0 ? $" Cls1 ={PoMaster.Cls1}," : "Cls1 =NULL,");
            cmdString.Append($"Cls2 ={PoMaster.Cls2}, Cls3 ={PoMaster.Cls3}, Cls4 ={PoMaster.Cls4},");
            cmdString.Append($" Cur_Id ={PoMaster.Cur_Id}, Cur_Rate ={PoMaster.Cur_Rate}, B_Amount ={PoMaster.B_Amount}, T_Amount ={PoMaster.T_Amount}, N_Amount ={PoMaster.N_Amount}, LN_Amount ={PoMaster.LN_Amount}, V_Amount ={PoMaster.V_Amount}, Tbl_Amount ={PoMaster.Tbl_Amount}, Action_type ='{PoMaster.Action_type}', R_Invoice =CAST('{PoMaster.R_Invoice}' AS BIT),");
            cmdString.Append($" CancelBy ='{PoMaster.CancelBy}', ");
            cmdString.Append(PoMaster.CancelDate != null ? $"CancelDate ='{PoMaster.CancelDate.GetSystemDate()}'," : "CancelDate =NULL,");
            cmdString.Append($" CancelRemarks ='{PoMaster.CancelRemarks}', No_Print ={PoMaster.No_Print}, In_Words ='{PoMaster.In_Words}', Remarks ='{PoMaster.Remarks}', Audit_Lock =CAST('{PoMaster.Audit_Lock}' AS BIT),");
            cmdString.Append($" Enter_By ='{PoMaster.Enter_By}', Enter_Date ='{PoMaster.Enter_Date.GetSystemDate()}', Reconcile_By ='{PoMaster.Reconcile_By}',");
            cmdString.Append(PoMaster.Reconcile_Date != null ? $"Reconcile_Date ='{PoMaster.Reconcile_Date.GetSystemDate()}'," : $"Reconcile_Date =NULL,");
            cmdString.Append($"Auth_By ='{PoMaster.Auth_By}', ");
            cmdString.Append(PoMaster.Auth_Date != null ? $"Auth_Date ='{PoMaster.Auth_Date.GetSystemDate()}'," : $"Auth_Date =NULL,");
            cmdString.Append(PoMaster.CBranch_Id > 0 ? $" CBranch_Id ={PoMaster.CBranch_Id}, " : $"CBranch_Id ={ObjGlobal.SysBranchId},");
            cmdString.Append(PoMaster.CUnit_Id > 0 ? $" CUnit_Id ={PoMaster.CUnit_Id}," : " CUnit_Id =NULL,");
            cmdString.Append(PoMaster.FiscalYearId > 0 ? $" FiscalYearId ={PoMaster.FiscalYearId}," : $"FiscalYearId ={ObjGlobal.SysFiscalYearId},");
            cmdString.Append(PoMaster.SyncBaseId.IsGuidExits() ? $" SyncBaseId ='{PoMaster.SyncBaseId}'," : "SyncBaseId =NULL,");
            cmdString.Append(PoMaster.SyncGlobalId.IsGuidExits() ? $" SyncGlobalId ='{PoMaster.SyncGlobalId}'," : "SyncGlobalId =NULL,");
            cmdString.Append(PoMaster.SyncOriginId.IsGuidExits() ? $" SyncOriginId ='{PoMaster.SyncOriginId}'," : "SyncOriginId =NULL,");
            cmdString.Append($" SyncCreatedOn ='{PoMaster.SyncCreatedOn.GetSystemDate()}', SyncLastPatchedOn ='{PoMaster.SyncLastPatchedOn.GetSystemDate()}',");
            cmdString.Append($" SyncRowVersion ={PoMaster.SyncRowVersion}, IsReverse =CAST('{PoMaster.IsReverse}' AS BIT), IsSynced =CAST('{PoMaster.IsSynced}' AS BIT) ");
            cmdString.Append($" WHERE PO_Invoice = N'{PoMaster.PO_Invoice}'; \n");
        }

        if (!_tagStrings.Contains(actionTag))
        {
            if (DetailsList.Count > 0)
            {
                cmdString.Append(@"
                    INSERT INTO AMS.PO_Details(PO_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PIN_Invoice, PIN_Sno, PQT_Invoice, PQT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append(" \n VALUES \n");

                foreach (var detail in DetailsList)
                {
                    var index = DetailsList.IndexOf(detail);
                    cmdString.Append($" ('{detail.PO_Invoice}', {detail.Invoice_SNo}, {detail.P_Id}, ");
                    cmdString.Append(detail.Gdn_Id > 0 ? $"{detail.Gdn_Id}," : "NULL,");
                    cmdString.Append($" {detail.Alt_Qty}, ");
                    cmdString.Append(detail.Alt_UnitId > 0 ? $"{detail.Alt_UnitId}, " : "NULL,");
                    cmdString.Append($"{detail.Qty}, ");
                    cmdString.Append(detail.Unit_Id > 0 ? $"{detail.Unit_Id}, " : "NULL,");
                    cmdString.Append($"{detail.Rate}, {detail.B_Amount}, {detail.T_Amount}, {detail.N_Amount}, {detail.AltStock_Qty}, {detail.Stock_Qty}, ");
                    cmdString.Append($" N'{detail.Narration}', '{detail.PIN_Invoice}', {detail.PIN_Sno}, N'{detail.PQT_Invoice}',");
                    cmdString.Append(detail.PQT_SNo > 0 ? $" {detail.PQT_SNo}," : "0,");
                    cmdString.Append($" {detail.Tax_Amount}, {detail.V_Amount}, {detail.V_Rate},");
                    cmdString.Append($" {detail.Issue_Qty.GetDecimal()}, {detail.Free_Unit_Id}, {detail.Free_Qty.GetDecimal()}, {detail.StockFree_Qty.GetDecimal()}, {detail.ExtraFree_Unit_Id}, {detail.ExtraFree_Qty.GetDecimal()}, {detail.ExtraStockFree_Qty.GetDecimal()}, ");
                    cmdString.Append(detail.T_Product.GetBool() ? $" CAST('{detail.T_Product}' AS BIT), " : "0,");
                    cmdString.Append(detail.P_Ledger > 0 ? $" {detail.P_Ledger}, " : "NULL,");
                    cmdString.Append(detail.PR_Ledger > 0 ? $"{detail.PR_Ledger}, " : "NULL,");
                    cmdString.Append($" '{detail.SZ1}', '{detail.SZ2}', '{detail.SZ3}', '{detail.SZ4}', '{detail.SZ5}', '{detail.SZ6}', '{detail.SZ7}', '{detail.SZ8}', '{detail.SZ9}', '{detail.SZ10}',");
                    cmdString.Append($" '{detail.Serial_No}', '{detail.Batch_No}',");
                    cmdString.Append(detail.Exp_Date != null ? $"'{detail.Exp_Date.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(detail.Manu_Date != null ? $"'{detail.Manu_Date.GetSystemDate()}'," : "NULL,");
                    cmdString.Append(detail.SyncBaseId.IsGuidExits() ? $"'{detail.SyncBaseId}'," : "NULL,");
                    cmdString.Append(detail.SyncGlobalId.IsGuidExits() ? $"'{detail.SyncGlobalId}'," : "NULL,");
                    cmdString.Append(detail.SyncOriginId.IsGuidExits() ? $"'{detail.SyncOriginId}'," : "NULL,");
                    cmdString.Append($" '{detail.SyncCreatedOn.GetSystemDate()}','{detail.SyncLastPatchedOn.GetSystemDate()}', {detail.SyncRowVersion.GetDecimal(true)}");
                    cmdString.Append(index == DetailsList.Count - 1 ? " ); \n" : "),\n");
                }
            }

            if (Terms.Count > 0)
            {
                if (actionTag.Equals("UPDATE"))
                {
                    cmdString.Append($"DELETE AMS.PO_Term WHERE PO_VNo='{PoMaster.PO_Invoice}' AND Term_Type='P' \n");
                }
                cmdString.Append(@" 
                    INSERT INTO AMS.PO_Term(PO_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append("\n VALUES \n");

                foreach (var dr in Terms)
                {
                    var index = Terms.IndexOf(dr);
                    cmdString.Append($" ('{dr.PO_VNo}', {dr.PT_Id}, {dr.SNo}, '{dr.Term_Type}', ");
                    cmdString.Append(dr.Product_Id > 0 ? $"{dr.Product_Id}," : "NULL,");
                    cmdString.Append($"{dr.Rate}, {dr.Amount}, '{dr.Taxable}',");
                    cmdString.Append(dr.SyncBaseId.IsGuidExits() ? $"'{dr.SyncBaseId}'," : "NULL,");
                    cmdString.Append(dr.SyncGlobalId.IsGuidExits() ? $"'{dr.SyncGlobalId}'," : "NULL,");
                    cmdString.Append(dr.SyncOriginId.IsGuidExits() ? $"'{dr.SyncOriginId}'," : "NULL,");
                    cmdString.Append($" '{dr.SyncCreatedOn.GetSystemDate()}','{dr.SyncLastPatchedOn.GetSystemDate()}', {dr.SyncRowVersion}");
                    cmdString.Append(index == Terms.Count - 1 ? " ); \n" : "),\n");
                }
            }
        }
        var saveData = SqlExtensions.ExecuteNonTrans(cmdString);
        if (saveData == 0)
        {
            return saveData;
        }

        if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null)
        {
            Task.Run(() => SyncPurchaseOrderAsync(actionTag));
        }

        if (_tagStrings.Contains(actionTag))
        {
            return saveData;
        }

        AuditLogPurchaseOrder(actionTag);
        PurchaseOrderTermPosting();
        if (PoMaster.PIN_Invoice.IsBlankOrEmpty())
        {
            return saveData;
        }
        var cmdTxt = $@" 
            UPDATE AMS.PIN_Master SET Invoice_Type = 'POSTED' WHERE PIN_Invoice='{PoMaster.PO_Invoice}';";
        var i = SqlExtensions.ExecuteNonQuery(cmdTxt);
        return saveData;
    }
    private int AuditLogPurchaseOrder(string actionTag)
    {
        var cmdString = @$"
			DECLARE @voucherNo NVARCHAR(50) SET @voucherNo = '{PoMaster.PO_Invoice}'
            INSERT INTO AUD.AUDIT_PO_MASTER (PO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,PB_Vno,Vno_Date,Vno_Miti,Vendor_ID,Party_Name,Vat_No,Contact_Person,Mobile_No,Address,ChqNo,ChqDate,Invoice_Type,Invoice_In,DueDays,DueDate,Agent_ID,Subledger_Id,PIN_Invoice,PIN_Date,PQT_Invoice,PQT_Date,Cls1,Cls2,Cls3,Cls4,Cur_Id,Cur_Rate,B_Amount,T_Amount,N_Amount,LN_Amount,V_Amount,Tbl_Amount,Action_Type,R_Invoice,No_Print,In_Words,Remarks,Audit_Lock,Reconcile_By,Reconcile_Date,Auth_By,Auth_Date,CUnit_Id,CBranch_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,Enter_By,Enter_Date,ModifyAction,ModifyBy,ModifyDate)
            SELECT PO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,PB_Vno,Vno_Date,Vno_Miti,Vendor_ID,Party_Name,Vat_No,Contact_Person,Mobile_No,Address,ChqNo,ChqDate,Invoice_Type,Invoice_In,DueDays,DueDate,Agent_ID,Subledger_Id,PIN_Invoice,PIN_Date,PQT_Invoice,PQT_Date,Cls1,Cls2,Cls3,Cls4,Cur_Id,Cur_Rate,B_Amount,T_Amount,N_Amount,LN_Amount,V_Amount,Tbl_Amount,Action_Type,R_Invoice,No_Print,In_Words,Remarks,Audit_Lock,Reconcile_By,Reconcile_Date,Auth_By,Auth_Date,CUnit_Id,CBranch_Id,FiscalYearId,PAttachment1,PAttachment2,PAttachment3,PAttachment4,PAttachment5,Enter_By,Enter_Date,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate 
            FROM AMS.PO_Master pm 
            WHERE pm.PO_Invoice =@voucherNo; 

            INSERT INTO AUD.AUDIT_PO_DETAILS (PO_Invoice,Invoice_SNo,P_Id,Gdn_Id,Alt_Qty,Alt_UnitId,Qty,Unit_Id,Rate,B_Amount,T_Amount,N_Amount,AltStock_Qty,Stock_Qty,Narration,PIN_Invoice,PIN_Sno,PQT_Invoice,PQT_SNo,Tax_Amount,V_Amount,V_Rate,Issue_Qty,Free_Unit_Id,Free_Qty,StockFree_Qty,ExtraFree_Unit_Id,ExtraFree_Qty,ExtraStockFree_Qty,T_Product,P_Ledger,PR_Ledger,SZ1,SZ2,SZ3,SZ4,SZ5,SZ6,SZ7,SZ8,SZ9,SZ10,Serial_No,Batch_No,Exp_Date,Manu_Date,ModifyAction,ModifyBy,ModifyDate)
            SELECT PO_Invoice,Invoice_SNo,P_Id,Gdn_Id,Alt_Qty,Alt_UnitId,Qty,Unit_Id,Rate,B_Amount,T_Amount,N_Amount,AltStock_Qty,Stock_Qty,Narration,PIN_Invoice,PIN_Sno,PQT_Invoice,PQT_SNo,Tax_Amount,V_Amount,V_Rate,Issue_Qty,Free_Unit_Id,Free_Qty,StockFree_Qty,ExtraFree_Unit_Id,ExtraFree_Qty,ExtraStockFree_Qty,T_Product,P_Ledger,PR_Ledger,SZ1,SZ2,SZ3,SZ4,SZ5,SZ6,SZ7,SZ8,SZ9,SZ10,Serial_No,Batch_No,Exp_Date,Manu_Date,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate 
            FROM AMS.PO_Details pd
            WHERE pd.PO_Invoice=@voucherNo;

            INSERT INTO AUD.AUDIT_PO_TERM (PO_VNo,PT_Id,SNo,Rate,Amount,Term_Type,Product_Id,Taxable,ModifyAction,ModifyBy,ModifyDate)
            SELECT PO_VNo,PT_Id,SNo,Rate,Amount,Term_Type,Product_Id,Taxable,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate 
            FROM AMS.PO_Term pt
            WHERE pt.PO_VNo=@voucherNo; ";
        return SqlExtensions.ExecuteNonTrans(cmdString);
    }

    public async Task<int> SyncPurchaseOrderAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}PurchaseOrder/GetPurchaseOrderById",
            InsertUrl = @$"{_configParams.Model.Item2}PurchaseOrder/InsertPurchaseOrderList",
            UpdateUrl = @$"{_configParams.Model.Item2}PurchaseOrder/UpdatePurchaseOrder"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var purchaseOrderRepo = DataSyncProviderFactory.GetRepository<PO_Master>(_injectData);
        // pull all new account data
        if (ObjGlobal.IsPurchaseOrderSync)
        {
            var result = await PullUnSyncPurchaseOrder(purchaseOrderRepo);
            if (!result)
                return 0;
        }
        // push all new account data
        var sqlQuery = @"SELECT *FROM AMS.PO_Master WHERE ISNULL(IsSynced,0)=0";
        var queryResponse = await QueryUtils.GetListAsync<PO_Master>(sqlQuery);
        var poList = queryResponse.List.ToList();
        if (poList.Count > 0)
        {
            var loopCount = 1;
            if (poList.Count > 100)
            {
                loopCount = poList.Count / 100 + 1;
            }
            var newPoList = new List<PO_Master>();
            var cmdString = new StringBuilder();
            var purchaseOrderList = new List<PO_Master>();
            for (var i = 0; i < loopCount; i++)
            {
                newPoList.Clear();
                if (i == 0)
                {
                    newPoList.AddRange(poList.Take(100));
                }
                else
                {
                    newPoList.AddRange(poList.Skip(100 * i).Take(100));
                }
                purchaseOrderList.Clear();
                foreach (var pl in newPoList)
                {
                    sqlQuery = $@"SELECT *FROM AMS.PO_Details WHERE PO_Invoice='{pl.PO_Invoice}'";
                    var dtlQueryResponse = await QueryUtils.GetListAsync<PO_Details>(sqlQuery);
                    var podList = dtlQueryResponse.List.ToList();

                    sqlQuery = $@"SELECT *FROM AMS.PO_Term WHERE PO_VNo='{pl.PO_Invoice}'";
                    var poTermQueryResponse = await QueryUtils.GetListAsync<PO_Term>(sqlQuery);
                    var poTermList = poTermQueryResponse.List.ToList();

                    var purOrder = new PO_Master();
                    purOrder = pl;
                    purOrder.DetailsList = podList;
                    purOrder.Terms = poTermList;
                    purchaseOrderList.Add(purOrder);
                }
                var pushResponse = await purchaseOrderRepo.PushNewListAsync(purchaseOrderList);
                if (!pushResponse.Value)
                {
                    SplashScreenManager.CloseForm();
                    pushResponse.ShowErrorDialog();
                    return 0;
                }
                else
                {
                    cmdString.Clear();
                    foreach (var pl in newPoList)
                    {
                        cmdString.Append($"UPDATE AMS.PO_Master SET IsSynced = 1 WHERE PO_Invoice = '{pl.PO_Invoice}'; \n");
                    }

                    var result = await QueryUtils.ExecNonQueryAsync(cmdString.ToString());
                }
            }
        }

        return 1;
    }

    // PULL PURCHASE ORDER INVOICE
    #region ---------- PULL PURCHASE ORDER INVOICE ----------
    private async Task<bool> PullUnSyncPurchaseOrder(IDataSyncRepository<PO_Master> purchaseOrderRepo)
    {
        try
        {
            var pullResponse = await purchaseOrderRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    //_orderRepository.SyncPurchaseOrderAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
            {
                await PullUnSyncPurchaseOrder(purchaseOrderRepo);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    public int PurchaseOrderTermPosting()
    {
        var cmdString = $@"
			DELETE AMS.PO_Term WHERE Term_Type='BT' AND PO_VNo='{PoMaster.PO_Invoice}';
			INSERT INTO AMS.PO_Term(PO_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT sbt.PO_VNo, PT_Id, ROW_NUMBER() OVER (ORDER BY(SELECT sbt.PO_VNo)) AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion, sbt.SyncBaseId
			FROM AMS.PO_Details sd
				 LEFT OUTER JOIN AMS.PO_Master sm ON sm.PO_Invoice=sd.PO_Invoice
				 LEFT OUTER JOIN AMS.PO_Term sbt ON sd.PO_Invoice=sbt.PO_VNo
			WHERE sbt.Term_Type='B' AND Product_Id IS NULL AND sbt.Amount>0 AND sbt.PO_VNo='{PoMaster.PO_Invoice}';";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
    }

    // OBJECT FOR THIS FORM
    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    public PO_Master PoMaster { get; set; }
    public List<PO_Details> DetailsList { get; set; }
    public List<PO_Term> Terms { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}