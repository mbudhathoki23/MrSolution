using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
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
using System.Windows.Forms;

namespace MrDAL.DataEntry.PurchaseMaster;

public class PurchaseChallanReturnRepository : IPurchaseChallanReturn
{
    public PurchaseChallanReturnRepository()
    {
        PcrMaster = new PCR_Master();
        PcMaster = new PC_Master();
        DetailsList = new List<PCR_Details>();
        Terms = new List<PCR_Term>();
    }

    // INSERT UPDATE DELETE
    public short ReturnMaxSyncRowVersion(string module, string vno)
    {
        var cmdString = module switch
        {
            "PIN" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PIN_Master pm WHERE pm.PIN_Invoice = '{vno}'",
            "PO" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PO_Master pm WHERE pm.PO_Invoice = '{vno}'",
            "PC" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PC_Master pm WHERE pm.PC_Invoice = '{vno}'",
            "GIT" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.GIT_Master pm WHERE pm.GIT_Invoice = '{vno}'",
            "PB" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PB_Master pm WHERE pm.PB_Invoice = '{vno}'",
            "PR" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.PR_Master pm WHERE pm.PR_Invoice = '{vno}'",
            "PAB" =>
                $"SELECT MAX(ISNULL(pm.SyncRowVersion,0) +1) SyncRowVersion FROM AMS.VmPabMaster pm WHERE pm.PAB_Invoice = '{vno}'",
            _ => string.Empty
        };
        return cmdString.IsBlankOrEmpty() ? (short)1 : cmdString.GetQueryData().GetShort();
    }
    public int SavePurchaseChallanReturn(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "DELETE" or "UPDATE")
        {
            cmdString.Append($@" DELETE FROM AMS.PCR_Term WHERE PCR_VNo = '{PcrMaster.PCR_Invoice}';
									 DELETE FROM AMS.PCR_Details WHERE PCR_Invoice = '{PcrMaster.PCR_Invoice}'; ");
            if (actionTag is "DELETE")
                cmdString.Append($" DELETE FROM AMS.PCR_Master WHERE PCR_Invoice = '{PcrMaster.PCR_Invoice}'; \n");
        }

        if (actionTag.ToUpper() is "NEW" || actionTag is "SAVE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.PCR_Master(PCR_Invoice, Invoice_Date, Invoice_Miti, Invoice_Time, PB_Vno, Vno_Date, Vno_Miti, Vendor_ID, PartyLedgerId, Party_Name, Vat_No, Contact_Person, Mobile_No, Address, ChqNo, ChqDate,ChqMiti, Invoice_Type, Invoice_In, DueDays, DueDate, Agent_Id, Subledger_Id, PO_Invoice, PO_Date, QOT_Invoice, QOT_Date, Cls1, Cls2, Cls3, Cls4, Cur_Id, Cur_Rate, Counter_ID, B_Amount, T_Amount, N_Amount, LN_Amount, Tender_Amount, Change_Amount, V_Amount, Tbl_Amount, Action_type, R_Invoice, CancelBy, CancelDate, CancelReason, No_Print, In_Words, Remarks, Audit_Lock, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Auth_By, Auth_Date, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)  ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append($"  ( N'{PcrMaster.PCR_Invoice}','{PcrMaster.Invoice_Date.GetSystemDate()}','{PcrMaster.Invoice_Miti}',GETDATE(), "); //PC_Invoice
            cmdString.Append(PcrMaster.PB_Vno.IsValueExits() ? $"N'{PcrMaster.PB_Vno}'," : "NULL,"); //PB_Vno
            cmdString.Append(PcrMaster.PB_Vno.IsValueExits()
                ? $"'{PcrMaster.Vno_Date.GetSystemDate()}',"
                : "NULL,"); //Vno_Date
            cmdString.Append(PcrMaster.PB_Vno.IsValueExits() ? $"'{PcrMaster.Vno_Miti}'," : "NULL,"); //Vno_Miti
            cmdString.Append(PcrMaster.Vendor_ID > 0 ? $"'{PcrMaster.Vendor_ID}'," : "0,"); //Vendor_ID
            if (PcrMaster.Party_Name.IsValueExits())
            {
                cmdString.Append(PcrMaster.PartyLedgerId.IsValueExits()
                    ? $"N'{PcrMaster.PartyLedgerId}',"
                    : "NULL,"); //Party_Name
                cmdString.Append(PcrMaster.Party_Name.IsValueExits()
                    ? $"N'{PcrMaster.Party_Name}',"
                    : "NULL,"); //Party_Name
                cmdString.Append(PcrMaster.Vat_No.IsValueExits() ? $"N'{PcrMaster.Vat_No}'," : "NULL,"); //Vat_No
                cmdString.Append(PcrMaster.Contact_Person.IsValueExits()
                    ? $"N'{PcrMaster.Contact_Person}',"
                    : "NULL,"); //Contact_Person
                cmdString.Append(PcrMaster.Mobile_No.IsValueExits()
                    ? $"N'{PcrMaster.Mobile_No}',"
                    : "NULL,"); //Mobile_No
                cmdString.Append(PcrMaster.Address.IsValueExits() ? $"N'{PcrMaster.Address}'," : "NULL,"); //Address
                cmdString.Append(PcrMaster.ChqNo.IsValueExits() ? $"N'{PcrMaster.ChqNo}'," : "NULL,"); //ChqNo
                cmdString.Append(PcrMaster.ChqNo.IsValueExits()
                    ? $"'{PcrMaster.ChqDate.GetSystemDate()}',"
                    : "NULL,"); //ChqDate
                cmdString.Append(PcrMaster.ChqNo.IsValueExits() ? $"'{PcrMaster.ChqDate}'," : "NULL,"); //ChqDate
            }
            else
            {
                cmdString.Append("NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
            }

            cmdString.Append(PcrMaster.Invoice_Type.IsValueExits()
                ? $"N'{PcrMaster.Invoice_Type}',"
                : "'NORMAL',"); //Invoice_Type
            cmdString.Append(PcrMaster.Invoice_In.IsValueExits()
                ? $"N'{PcrMaster.Invoice_In}',"
                : "'CREDIT',"); //Invoice_In
            cmdString.Append(PcrMaster.DueDays.GetInt() > 0 ? $"'{PcrMaster.DueDays}'," : "0,"); //DueDays
            cmdString.Append(PcrMaster.DueDate != null ? $"'{PcrMaster.DueDate:yyyy-MM-dd}'," : "NULL,"); //DueDate
            cmdString.Append(PcrMaster.Agent_Id.GetInt() > 0 ? $"'{PcrMaster.Agent_Id}'," : "NULL,"); //Agent_Id
            cmdString.Append(PcrMaster.Subledger_Id.GetInt() > 0
                ? $"'{PcrMaster.Subledger_Id}',"
                : "Null,"); //SubLedger_Id

            if (PcrMaster.PO_Invoice.IsValueExits())
            {
                cmdString.Append(PcrMaster.PO_Invoice.IsValueExits() ? $"'{PcrMaster.PO_Invoice}'," : "NULL,");
                cmdString.Append(PcrMaster.PO_Date != null ? $"'{PcrMaster.PO_Date.GetSystemDate()}'," : "NULL,");
            }
            else
            {
                cmdString.Append("NULL,NULL,");
            }

            if (PcrMaster.QOT_Invoice.IsValueExits())
            {
                cmdString.Append(PcrMaster.QOT_Invoice.IsValueExits() ? $"N'{PcrMaster.QOT_Invoice}'," : "NULL,");
                cmdString.Append(PcrMaster.QOT_Date != null ? $"'{PcrMaster.QOT_Date.GetSystemDate()}'," : "NULL,");
            }
            else
            {
                cmdString.Append("NULL,NULL,");
            }

            cmdString.Append(PcrMaster.Cls1.GetInt() > 0 ? $"'{PcrMaster.Cls1}'," : "Null,");
            cmdString.Append("NULL,NULL,NULL, "); // Cls2, Cls3, Cls4,
            cmdString.Append(PcrMaster.Cur_Id.GetInt() > 0 ? $"{PcrMaster.Cur_Id}," : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append($"{PcrMaster.Cur_Rate.GetDecimal(true)},");
            cmdString.Append(PcrMaster.Counter_ID.GetInt() > 0 ? $"'{PcrMaster.Counter_ID}'," : "Null,");
            cmdString.Append($"{PcrMaster.B_Amount.GetDecimal()},");
            cmdString.Append($"{PcrMaster.T_Amount.GetDecimal()},");
            cmdString.Append($"{PcrMaster.N_Amount.GetDecimal()},");
            cmdString.Append($"{PcrMaster.LN_Amount.GetDecimal()},");
            cmdString.Append($"{PcrMaster.Tender_Amount.GetDecimal()},");
            cmdString.Append($"{PcrMaster.Change_Amount.GetDecimal()},");
            cmdString.Append($"'{PcrMaster.V_Amount.GetDecimal()}',");
            cmdString.Append($"'{PcrMaster.Tbl_Amount.GetDecimal()}',");
            cmdString.Append(PcrMaster.Action_type.IsValueExits() ? $"N'{PcrMaster.Action_type}'," : "'SAVE',");
            cmdString.Append("0,NULL,NULL,NULL,0,");
            cmdString.Append(PcrMaster.In_Words.IsValueExits() ? $"N'{PcrMaster.In_Words}'," : "NULL,");
            cmdString.Append(PcrMaster.Remarks.IsValueExits()
                ? $"N'{PcrMaster.Remarks.Trim().Replace("'", "''")}',"
                : "NULL,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}',GETDATE(),NULL,NULL,NULL,NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysFiscalYearId > 0 ? $" {ObjGlobal.SysFiscalYearId}," : "NULL,");
            cmdString.Append($"NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,");
            cmdString.Append($"{PcrMaster.SyncRowVersion.GetDecimal(true)}); \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE [AMS].[PCR_Master] SET  ");
            cmdString.Append($"Invoice_Date = '{PcrMaster.Invoice_Date.GetSystemDate()}',");
            cmdString.Append($"Invoice_Miti = '{PcrMaster.Invoice_Miti}',");
            cmdString.Append(PcrMaster.PB_Vno.IsValueExits() ? $"PB_Vno = N'{PcrMaster.PB_Vno}'," : "PB_Vno = NULL,");
            cmdString.Append(PcrMaster.Vno_Date != null
                ? $"Vno_Date = '{PcrMaster.Vno_Date.GetSystemDate()}',"
                : "NULL,");
            cmdString.Append(PcrMaster.Vno_Miti != null ? $"Vno_Miti = '{PcrMaster.Vno_Miti}'," : "NULL,");
            cmdString.Append(PcrMaster.Vendor_ID.GetLong() > 0
                ? $"Vendor_ID = '{PcrMaster.Vendor_ID}',"
                : "Vendor_ID = 0,");
            cmdString.Append(PcrMaster.Party_Name.IsValueExits()
                ? $"Party_Name = N'{PcrMaster.Party_Name}',"
                : "Party_Name= NULL,");
            cmdString.Append(PcrMaster.Vat_No.IsValueExits() ? $"Vat_No = N'{PcrMaster.Vat_No}'," : "Vat_No = NULL,");
            cmdString.Append(PcrMaster.Contact_Person.IsValueExits()
                ? $"Contact_Person = N'{PcrMaster.Contact_Person}',"
                : "Contact_Person= NULL,");
            cmdString.Append(PcrMaster.Mobile_No.IsValueExits()
                ? $"Mobile_No = N'{PcrMaster.Mobile_No}',"
                : "Mobile_No= NULL,");
            cmdString.Append(PcrMaster.Address.IsValueExits()
                ? $"Address = N'{PcrMaster.Address}',"
                : "Address= NULL,");
            cmdString.Append(PcrMaster.ChqNo.IsValueExits() ? $"ChqNo = N'{PcrMaster.ChqNo}'," : "ChqNo= NULL,");
            cmdString.Append(PcrMaster.ChqNo.IsValueExits()
                ? $"ChqDate= '{PcrMaster.ChqDate.GetSystemDate()}',"
                : "ChqDate= NULL,");
            cmdString.Append(PcrMaster.Invoice_Type.IsValueExits()
                ? $"Invoice_Type = N'{PcrMaster.Invoice_Type}',"
                : "Invoice_Type= NULL,");
            cmdString.Append(PcrMaster.Invoice_In.IsValueExits()
                ? $"Invoice_In = N'{PcrMaster.Invoice_In}',"
                : "Invoice_In= NULL,");
            cmdString.Append(PcrMaster.DueDays.GetInt() > 0 ? $"DueDays = '{PcrMaster.DueDays}'," : "DueDays = 0,");
            cmdString.Append(PcrMaster.DueDays.GetInt() > 0
                ? $"DueDate = '{PcrMaster.DueDate.GetSystemDate()}',"
                : "DueDate= NULL,");
            cmdString.Append(
                PcrMaster.Agent_Id.GetInt() > 0 ? $"Agent_Id = '{PcrMaster.Agent_Id}'," : "Agent_Id= Null,");
            cmdString.Append(PcrMaster.Subledger_Id.GetInt() > 0
                ? $"Subledger_Id = '{PcrMaster.Subledger_Id}',"
                : "Subledger_Id =Null,");
            cmdString.Append(PcrMaster.PO_Invoice.IsValueExits()
                ? $"PO_Invoice = '{PcrMaster.PO_Invoice}',"
                : "PO_Invoice = NULL,");
            cmdString.Append(PcrMaster.PO_Invoice.IsValueExits()
                ? $"PO_Date = '{PcrMaster.PO_Date.GetSystemDate()}',"
                : "PO_Date = NULL,");
            cmdString.Append(PcrMaster.QOT_Invoice.IsValueExits()
                ? $"QOT_Invoice = N'{PcrMaster.QOT_Invoice}',"
                : "QOT_Invoice = NULL,");
            cmdString.Append(PcrMaster.QOT_Invoice.IsValueExits()
                ? $"QOT_Date = '{PcrMaster.QOT_Date.GetSystemDate()}',"
                : "QOT_Date = NULL,");
            cmdString.Append(PcrMaster.Cls1.GetInt() > 0 ? $"Cls1 = '{PcrMaster.Cls1}'," : "Cls1 = Null,");
            cmdString.Append(PcrMaster.Cur_Id.GetInt() > 0
                ? $"Cur_Id = '{PcrMaster.Cur_Id}',"
                : $"Cur_Id = {ObjGlobal.SysCurrencyId},");
            cmdString.Append(PcrMaster.Cur_Rate.GetDecimal(true) > 0
                ? $"Cur_Rate = '{PcrMaster.Cur_Rate}',"
                : "Cur_Rate = 1,");
            cmdString.Append(PcrMaster.Counter_ID.GetInt() > 0
                ? $"Counter_ID = '{PcrMaster.Counter_ID}',"
                : "Counter_ID =Null,");
            cmdString.Append($" B_Amount = '{PcrMaster.B_Amount.GetDecimal()}',");
            cmdString.Append($" T_Amount = '{PcrMaster.T_Amount.GetDecimal()}',");
            cmdString.Append($" N_Amount = '{PcrMaster.N_Amount.GetDecimal()}',");
            cmdString.Append($" Tender_Amount = '{PcrMaster.Tender_Amount.GetDecimal()}',");
            cmdString.Append($" Change_Amount = '{PcrMaster.Change_Amount.GetDecimal()}',");
            cmdString.Append($" V_Amount = '{PcrMaster.V_Amount.GetDecimal()}',");
            cmdString.Append($" LN_Amount= '{PcrMaster.LN_Amount.GetDecimal()}',");
            cmdString.Append($" Tbl_Amount = '{PcrMaster.Tbl_Amount.GetDecimal()}',");
            cmdString.Append($" Action_type = N'{PcrMaster.Action_type}',");
            cmdString.Append(PcrMaster.In_Words.IsValueExits()
                ? $" In_Words = N'{PcrMaster.In_Words}',"
                : "In_Words = NULL,");
            cmdString.Append(PcrMaster.Remarks.IsValueExits()
                ? $" Remarks = N'{PcrMaster.Remarks.Trim().Replace("'", "''")}', "
                : "Remarks = NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? " SyncLastPatchedOn = GETDATE()," : "SyncLastPatchedOn = NULL,");
            cmdString.Append($" SyncRowVersion =  {PcrMaster.SyncRowVersion.GetInt()},");
            cmdString.Append(" IsSynced =0");
            cmdString.Append($" WHERE PCR_Invoice = '{PcrMaster.PCR_Invoice}' ; \n ");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            if (DetailsList.Count > 0)
            {
                cmdString.Append(@" 
                    INSERT INTO AMS.PCR_Details(PCR_Invoice, Invoice_SNo, P_Id, Gdn_Id, Alt_Qty, Alt_UnitId, Qty, Unit_Id, Rate, B_Amount, T_Amount, N_Amount, AltStock_Qty, Stock_Qty, Narration, PO_Invoice, PO_Sno, QOT_Invoice, QOT_SNo, Tax_Amount, V_Amount, V_Rate, Issue_Qty, Free_Unit_Id, Free_Qty, StockFree_Qty, ExtraFree_Unit_Id, ExtraFree_Qty, ExtraStockFree_Qty, T_Product, P_Ledger, PR_Ledger, SZ1, SZ2, SZ3, SZ4, SZ5, SZ6, SZ7, SZ8, SZ9, SZ10, Serial_No, Batch_No, Exp_Date, Manu_Date, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append("\n VALUES \n");
                foreach (var detail in DetailsList)
                {
                    var index = DetailsList.IndexOf(detail);
                    cmdString.Append($" (N'{detail.PCR_Invoice}', {detail.Invoice_SNo}, {detail.P_Id}, ");
                    cmdString.Append(detail.Gdn_Id > 0 ? $"{detail.Gdn_Id}," : "NULL,");
                    cmdString.Append($"{detail.Alt_Qty}, ");
                    cmdString.Append(detail.Alt_UnitId > 0 ? $"{detail.Alt_UnitId}, " : "NULL,");
                    cmdString.Append($"{detail.Qty}, ");
                    cmdString.Append(detail.Unit_Id > 0 ? $"{detail.Unit_Id}, " : "NULL,");
                    cmdString.Append($"{detail.Rate}, {detail.B_Amount}, {detail.T_Amount}, {detail.N_Amount}, {detail.AltStock_Qty}, {detail.Stock_Qty}, ");
                    cmdString.Append($" N'{detail.Narration}', '{detail.PO_Invoice}', {detail.PO_Sno}, '{detail.QOT_Invoice}',");
                    cmdString.Append(detail.QOT_SNo > 0 ? $"{detail.QOT_SNo}, " : "0,");
                    cmdString.Append($"{detail.Tax_Amount}, {detail.V_Amount}, {detail.V_Rate}, {detail.Issue_Qty}, ");
                    cmdString.Append($"{detail.Free_Unit_Id}, {detail.Free_Qty}, {detail.StockFree_Qty}, {detail.ExtraFree_Unit_Id}, {detail.ExtraFree_Qty}, {detail.ExtraStockFree_Qty}, ");
                    cmdString.Append($"CAST('{detail.T_Product}' AS BIT) , ");
                    cmdString.Append(detail.P_Ledger > 0 ? $"{detail.P_Ledger}, " : "NULL,");
                    cmdString.Append(detail.PR_Ledger > 0 ? $"{detail.PR_Ledger}," : "NULL,");
                    cmdString.Append($" NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,");
                    cmdString.Append(detail.Serial_No.IsValueExits() ? $" {detail.Serial_No}," : "NULL,");
                    cmdString.Append(detail.Batch_No.IsValueExits()
                        ? $" '{detail.Batch_No}', '{detail.Exp_Date.GetSystemDate()}', '{detail.Manu_Date.GetSystemDate()}',"
                        : "NULL,NULL,NULL,");
                    cmdString.Append(detail.SyncBaseId.IsGuidExits() ? $" '{detail.SyncBaseId}'," : "NULL,");
                    cmdString.Append(detail.SyncGlobalId.IsGuidExits() ? $" '{detail.SyncGlobalId}'," : "NULL,");
                    cmdString.Append(detail.SyncOriginId.IsGuidExits() ? $" '{detail.SyncOriginId}'," : "NULL,");
                    cmdString.Append($"'{detail.SyncCreatedOn.GetSystemDate()}',");
                    cmdString.Append($"'{detail.SyncLastPatchedOn.GetSystemDate()}',");
                    cmdString.Append($"{detail.SyncRowVersion}");
                    cmdString.Append(index == DetailsList.Count - 1 ? " );" : "),");
                }

            }
            if (Terms.Count > 0)
            {
                if (actionTag == "UPDATE")
                {
                    cmdString.Append(
                        $" DELETE AMS.PCR_Term WHERE PCR_VNo='{PcrMaster.PCR_Invoice}' AND Term_Type='B'; \n");
                }

                cmdString.Append(@" 
                        INSERT INTO AMS.PCR_Term(PCR_VNo, PT_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                cmdString.Append("\n VALUES \n");

                foreach (var item in Terms)
                {
                    var index = Terms.IndexOf(item);
                    cmdString.Append($"('{item.PCR_VNo}', {item.PT_Id}, {item.SNo}, '{item.Term_Type}', ");
                    cmdString.Append(item.Product_Id > 0 ? $"{item.Product_Id}," : "Null,");
                    cmdString.Append($"{item.Rate}, {item.Amount}, ");
                    cmdString.Append($"'{item.Taxable}', ");
                    cmdString.Append(item.SyncBaseId.IsGuidExits() ? $"'{item.SyncBaseId}'," : "NULL,");
                    cmdString.Append(item.SyncGlobalId.IsGuidExits() ? $"'{item.SyncGlobalId}'," : "NULL,");
                    cmdString.Append(item.SyncOriginId.IsGuidExits() ? $"'{item.SyncOriginId}'," : "NULL,");
                    cmdString.Append($"'{item.SyncCreatedOn.GetSystemDate()}','{item.SyncLastPatchedOn.GetSystemDate()}',");
                    cmdString.Append($"{item.SyncRowVersion}");
                    cmdString.Append(index == Terms.Count - 1 ? ");\n" : "),\n");
                }

            }
        }

        var iResult = SaveDataInDatabase(cmdString);
        if (iResult == 0)
        {
            return iResult;
        }

        if (ObjGlobal.IsOnlineSync && ObjGlobal.SyncOrginIdSync != null) Task.Run(() => SyncPurchaseChallanReturnAsync(actionTag));
        if (!_tagStrings.Contains(actionTag))
        {
            AuditLogPurchaseChallan(actionTag);
            PurchaseChallanTermPosting();
        }

        return iResult;
    }
    private static int SaveDataInDatabase(StringBuilder stringBuilder)
    {
        var isExc = SqlExtensions.ExecuteNonTrans(stringBuilder.ToString());
        return isExc;
    }
    public async Task<int> SyncPurchaseChallanReturnAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                return 1;

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
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}PurchaseChallanReturn/GetPurchaseChallanReturnById";
            insertUrl = @$"{configParams.Model.Item2}PurchaseChallanReturn/InsertPurchaseChallanReturn";
            updateUrl = @$"{configParams.Model.Item2}PurchaseChallanReturn/UpdatePurchaseChallanReturn";
            deleteUrl = @$"{configParams.Model.Item2}PurchaseChallanReturn/DeletePurchaseChallanReturnAsync?id=" +
                        PcrMaster.PCR_Invoice;

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

            var purchaseChallanReturnRepo =
                DataSyncProviderFactory.GetRepository<PCR_Master>(DataSyncManager
                    .GetGlobalInjectData());
            var pcr = new PCR_Master
            {
                PCR_Invoice = PcrMaster.PCR_Invoice,
                Invoice_Date = Convert.ToDateTime(PcrMaster.Invoice_Date.GetSystemDate()),
                Invoice_Miti = PcrMaster.Invoice_Miti,
                Invoice_Time = DateTime.Now,
                PB_Vno = PcrMaster.PB_Vno.IsValueExits() ? PcrMaster.PB_Vno : null,
                Vno_Date = PcrMaster.PB_Vno.IsValueExits()
                    ? Convert.ToDateTime(PcrMaster.Vno_Date.GetSystemDate())
                    : null,
                Vno_Miti = PcrMaster.PB_Vno.IsValueExits() ? PcrMaster.Vno_Miti : null,
                Vendor_ID = PcrMaster.Vendor_ID
            };
            if (PcrMaster.Party_Name.IsValueExits())
            {
                pcr.PartyLedgerId = PcrMaster.PartyLedgerId.IsValueExits() ? PcrMaster.PartyLedgerId : null;
                pcr.Party_Name = PcrMaster.Party_Name.IsValueExits() ? PcrMaster.Party_Name : null;
                pcr.Vat_No = PcrMaster.Vat_No.IsValueExits() ? PcrMaster.Vat_No : null;
                pcr.Contact_Person = PcrMaster.Contact_Person.IsValueExits() ? PcrMaster.Contact_Person : null;
                pcr.Mobile_No = PcrMaster.Mobile_No.IsValueExits() ? PcrMaster.Mobile_No : null;
                pcr.Address = PcrMaster.Address.IsValueExits() ? PcrMaster.Address : null;
                pcr.ChqNo = PcrMaster.ChqNo.IsValueExits() ? PcrMaster.ChqNo : null;
                pcr.ChqDate = PcrMaster.ChqNo.IsValueExits()
                    ? Convert.ToDateTime(PcrMaster.ChqDate.GetSystemDate())
                    : null;
                pcr.ChqMiti = PcrMaster.ChqNo.IsValueExits() ? PcrMaster.ChqMiti : null;
            }
            else
            {
                pcr.PartyLedgerId = null;
                pcr.Party_Name = null;
                pcr.Vat_No = null;
                pcr.Contact_Person = null;
                pcr.Mobile_No = null;
                pcr.Address = null;
                pcr.ChqNo = null;
                pcr.ChqDate = null;
                pcr.ChqMiti = null;
            }

            pcr.Invoice_Type = PcrMaster.Invoice_Type.IsValueExits() ? PcrMaster.Invoice_Type : "NORMAL";
            pcr.Invoice_In = PcrMaster.Invoice_In.IsValueExits() ? PcrMaster.Invoice_In : "CREDIT";
            pcr.DueDays = PcrMaster.DueDays.GetInt() > 0 ? PcrMaster.DueDays : 0;
            pcr.DueDate = PcrMaster.DueDate != null
                ? Convert.ToDateTime(PcrMaster.DueDate.Value.ToString("yyyy-MM-dd"))
                : null;
            pcr.Agent_Id = PcrMaster.Agent_Id.GetInt() > 0 ? PcrMaster.Agent_Id : null;
            pcr.Subledger_Id = PcrMaster.Subledger_Id.GetInt() > 0 ? PcrMaster.Subledger_Id : null;
            if (PcrMaster.PO_Invoice.IsValueExits())
            {
                pcr.PO_Invoice = PcrMaster.PO_Invoice.IsValueExits() ? PcrMaster.PO_Invoice : null;
                pcr.PO_Date = PcrMaster.PO_Date != null ? Convert.ToDateTime(PcrMaster.PO_Date.GetSystemDate()) : null;
            }
            else
            {
                pcr.PO_Invoice = null;
                pcr.PO_Date = null;
            }

            if (PcrMaster.QOT_Invoice.IsValueExits())
            {
                pcr.QOT_Invoice = PcrMaster.QOT_Invoice.IsValueExits() ? PcrMaster.QOT_Invoice : null;
                pcr.QOT_Date = PcrMaster.QOT_Date != null
                    ? Convert.ToDateTime(PcrMaster.QOT_Date.GetSystemDate())
                    : null;
            }
            else
            {
                pcr.QOT_Invoice = null;
                pcr.QOT_Date = null;
            }

            pcr.Cls1 = PcrMaster.Cls1.GetInt() > 0 ? PcrMaster.Cls1 : null;
            pcr.Cls2 = null;
            pcr.Cls3 = null;
            pcr.Cls4 = null;
            pcr.Cur_Id = PcrMaster.Cur_Id.GetInt() > 0 ? PcrMaster.Cur_Id : ObjGlobal.SysCurrencyId;
            pcr.Cur_Rate = PcrMaster.Cur_Rate.GetDecimal(true);
            pcr.Counter_ID = PcrMaster.Counter_ID.GetInt() > 0 ? PcrMaster.Counter_ID : null;
            pcr.B_Amount = PcrMaster.B_Amount.GetDecimal();
            pcr.T_Amount = PcrMaster.T_Amount.GetDecimal();
            pcr.N_Amount = PcrMaster.N_Amount.GetDecimal();
            pcr.LN_Amount = PcrMaster.LN_Amount.GetDecimal();
            pcr.Tender_Amount = PcrMaster.Tender_Amount.GetDecimal();
            pcr.Change_Amount = PcrMaster.Change_Amount.GetDecimal();
            pcr.V_Amount = PcrMaster.V_Amount.GetDecimal();
            pcr.Tbl_Amount = PcrMaster.Tbl_Amount.GetDecimal();
            pcr.Action_type = PcrMaster.Action_type.IsValueExits() ? PcrMaster.Action_type : "SAVE";
            pcr.R_Invoice = false;
            pcr.CancelBy = null;
            pcr.CancelDate = null;
            pcr.CancelReason = null;
            pcr.No_Print = 0;
            pcr.In_Words = PcrMaster.In_Words.IsValueExits() ? PcrMaster.In_Words : null;
            pcr.Remarks = PcrMaster.Remarks.IsValueExits() ? PcrMaster.Remarks.Trim().Replace("'", "''") : null;
            pcr.Audit_Lock = false;
            pcr.Enter_By = ObjGlobal.LogInUser;
            pcr.Enter_Date = DateTime.Now;
            pcr.Reconcile_By = null;
            pcr.Reconcile_Date = null;
            pcr.Auth_By = null;
            pcr.Auth_Date = null;
            pcr.CBranch_Id = ObjGlobal.SysBranchId;
            pcr.CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            pcr.FiscalYearId = ObjGlobal.SysFiscalYearId;
            pcr.PAttachment1 = null;
            pcr.PAttachment2 = null;
            pcr.PAttachment3 = null;
            pcr.PAttachment4 = null;
            pcr.PAttachment5 = null;
            pcr.SyncRowVersion = PcrMaster.SyncRowVersion;
            var pcrDetails = new List<PCR_Details>();
            var pcrTerms = new List<PCR_Term>();
            if (actionTag != "DELETE" && actionTag != "REVERSE")
            {
                if (PcrMaster.GetView != null && PcrMaster.GetView.Rows.Count > 0)
                    foreach (DataGridViewRow ro in PcrMaster.GetView.Rows)
                    {
                        var pcrd = new PCR_Details
                        {
                            PCR_Invoice = PcrMaster.PCR_Invoice,
                            Invoice_SNo = ro.Cells["GTxtSNo"].Value.GetInt(),
                            P_Id = ro.Cells["GTxtProductId"].Value.GetLong(),
                            Gdn_Id = ro.Cells["GTxtGodownId"].Value.GetInt() > 0
                                ? ro.Cells["GTxtGodownId"].Value.GetInt()
                                : null,
                            Alt_Qty = ro.Cells["GTxtAltQty"].Value.GetDecimal() > 0
                                ? ro.Cells["GTxtAltQty"].Value.GetDecimal()
                                : 0,
                            Alt_UnitId = ro.Cells["GTxtAltUOMId"].Value.GetInt() > 0
                                ? ro.Cells["GTxtAltUOMId"].Value.GetInt()
                                : null,
                            Qty = ro.Cells["GTxtQty"].Value.GetDecimal() > 0
                                ? ro.Cells["GTxtQty"].Value.GetDecimal()
                                : 1,
                            Unit_Id = ro.Cells["GTxtUOMId"].Value.GetInt() > 0
                                ? ro.Cells["GTxtUOMId"].Value.GetInt()
                                : null,
                            Rate = ro.Cells["GTxtRate"].Value.GetDecimal(),
                            B_Amount = ro.Cells["GTxtAmount"].Value.GetDecimal(),
                            T_Amount = ro.Cells["GTxtTermAmount"].Value.GetDecimal(),
                            N_Amount = ro.Cells["GTxtNetAmount"].Value.GetDecimal(),
                            AltStock_Qty = ro.Cells["GTxtAltStockQty"].Value.GetDecimal(),
                            Stock_Qty = ro.Cells["GTxtStockQty"].Value.GetDecimal(),
                            Narration = ro.Cells["GTxtNarration"].Value.IsValueExits()
                                ? ro.Cells["GTxtNarration"].Value.ToString()
                                : null,
                            PO_Invoice = ro.Cells["GTxtOrderNo"].Value.IsValueExits()
                                ? ro.Cells["GTxtOrderNo"].Value.ToString()
                                : null,
                            PO_Sno = ro.Cells["GTxtOrderNo"].Value.IsValueExits()
                                ? ro.Cells["GTxtOrderSno"].Value.GetInt()
                                : null,
                            QOT_Invoice = ro.Cells["GTxtIndentNo"].Value.IsValueExits()
                                ? ro.Cells["GTxtIndentNo"].Value.ToString()
                                : null,
                            QOT_SNo = ro.Cells["GTxtIndentNo"].Value.IsValueExits()
                                ? ro.Cells["GTxtIndentSno"].Value.GetInt()
                                : null,
                            Tax_Amount = ro.Cells["GTxtTaxableAmount"].Value.GetDecimal(),
                            V_Amount = ro.Cells["GTxtVatAmount"].Value.GetDecimal(),
                            V_Rate = ro.Cells["GTxtTaxPriceRate"].Value.GetDecimal(),
                            Issue_Qty = 0,
                            Free_Unit_Id = ro.Cells["GTxtFreeUnitId"].Value.GetInt() > 0
                                ? ro.Cells["GTxtFreeUnitId"].Value.GetInt()
                                : null,
                            Free_Qty = ro.Cells["GTxtFreeQty"].Value.GetDecimal(),
                            StockFree_Qty = ro.Cells["GTxtStockFreeQty"].Value.GetDecimal(),
                            ExtraFree_Unit_Id = ro.Cells["GTxtExtraFreeUnitId"].Value.GetInt() > 0
                                ? ro.Cells["GTxtExtraFreeUnitId"].Value.GetInt()
                                : null,
                            ExtraFree_Qty = ro.Cells["GTxtExtraFreeQty"].Value.GetDecimal()
                        };
                        pcrd.ExtraFree_Unit_Id = 0;
                        pcrd.T_Product = ro.Cells["IsTaxable"].Value.GetBool() ? true : false;
                        pcrd.P_Ledger = ro.Cells["GTxtPBLedgerId"].Value.IsValueExits()
                            ? ro.Cells["GTxtPBLedgerId"].Value.GetLong()
                            : null;
                        pcrd.PR_Ledger = ro.Cells["GTxtPRLedgerId"].Value.IsValueExits()
                            ? ro.Cells["GTxtPRLedgerId"].Value.GetLong()
                            : null;
                        pcrd.SZ1 = null;
                        pcrd.SZ2 = null;
                        pcrd.SZ3 = null;
                        pcrd.SZ4 = null;
                        pcrd.SZ5 = null;
                        pcrd.SZ6 = null;
                        pcrd.SZ7 = null;
                        pcrd.SZ8 = null;
                        pcrd.SZ9 = null;
                        pcrd.SZ10 = null;
                        pcrd.Serial_No = null;
                        pcrd.Batch_No = ro.Cells["GTxtBatchNo"].Value.IsValueExits()
                            ? ro.Cells["GTxtBatchNo"].Value.ToString()
                            : null;
                        pcrd.Exp_Date = ro.Cells["GTxtBatchNo"].Value.IsValueExits()
                            ? Convert.ToDateTime(ro.Cells["GMskEXPDate"].Value)
                            : null;
                        pcrd.Manu_Date = ro.Cells["GTxtBatchNo"].Value.IsValueExits()
                            ? Convert.ToDateTime(ro.Cells["GTxtMFGDate"].Value)
                            : null;
                        pcrd.SyncRowVersion = PcrMaster.SyncRowVersion;

                        pcrDetails.Add(pcrd);
                    }

                if (PcrMaster.ProductTerm != null && PcrMaster.ProductTerm.Rows.Count > 0)
                    foreach (DataRow dr in PcrMaster.ProductTerm.Rows)
                    {
                        if (dr["TermAmt"].GetDecimal() is 0) continue;
                        var pcrTerm = new PCR_Term
                        {
                            PCR_VNo = PcrMaster.PCR_Invoice,
                            PT_Id = dr["TermId"].GetInt(),
                            SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1,
                            Term_Type = "P",
                            Product_Id = dr["ProductId"].GetLong() > 0 ? dr["ProductId"].GetLong() : null,
                            Rate = dr["TermRate"].GetDecimal(),
                            Amount = dr["TermAmt"].GetDecimal(),
                            Taxable = dr["TermRate"].GetDecimal() > 0 &&
                                      dr["TermId"].GetInt().Equals(ObjGlobal.PurchaseVatTermId)
                                ? "Y"
                                : "N",
                            SyncRowVersion = PcrMaster.SyncRowVersion
                        };
                        pcrTerms.Add(pcrTerm);
                    }

                if (PcrMaster.BillTerm != null && PcrMaster.BillTerm.Rows.Count > 0)
                    foreach (DataRow dr in PcrMaster.BillTerm.Rows)
                    {
                        if (dr["TermAmt"].GetDecimal() is 0) continue;
                        var pcrTerm = new PCR_Term
                        {
                            PCR_VNo = PcrMaster.PCR_Invoice,
                            PT_Id = dr["TermId"].GetInt(),
                            SNo = dr["ProductSno"].GetInt() > 0 ? dr["ProductSno"].GetInt() : 1,
                            Term_Type = "B",
                            Product_Id = dr["ProductId"].GetLong() > 0 ? dr["ProductId"].GetLong() : null,
                            Rate = dr["TermRate"].GetDecimal(),
                            Amount = dr["TermAmt"].GetDecimal(),
                            Taxable = dr["TermRate"].GetDecimal() > 0 &&
                                      dr["TermId"].GetInt().Equals(ObjGlobal.PurchaseVatTermId)
                                ? "Y"
                                : "N",
                            SyncRowVersion = PcrMaster.SyncRowVersion
                        };
                        pcrTerms.Add(pcrTerm);
                    }
            }

            pcr.DetailsList = pcrDetails;
            pcr.Terms = pcrTerms;
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await purchaseChallanReturnRepo?.PushNewAsync(pcr),
                "NEW" => await purchaseChallanReturnRepo?.PushNewAsync(pcr),
                "UPDATE" => await purchaseChallanReturnRepo?.PutNewAsync(pcr),
                //"REVERSE" => await purchaseChallanReturnRepo?.PutNewAsync(pcr),
                //"DELETE" => await purchaseChallanReturnRepo?.DeleteNewAsync(),
                _ => await purchaseChallanReturnRepo?.PushNewAsync(pcr)
            };
            if (result.Value)
            {
                var queryBuilder = new StringBuilder();
                queryBuilder.Append($@"
                UPDATE AMS.PCR_Master SET IsSynced =1 WHERE PCR_Invoice='{PcrMaster.PCR_Invoice}'");
                SaveDataInDatabase(queryBuilder);
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    private int AuditLogPurchaseChallan(string actionTag)
    {
        var cmdString = @$"
			INSERT INTO [AUD].[AUDIT_PC_MASTER] ([PC_Invoice], [Invoice_Date], [Invoice_Miti], [Invoice_Time], [PB_Vno], [Vno_Date], [Vno_Miti], [Vendor_ID], [Party_Name], [Vat_No], [Contact_Person], [Mobile_No], [Address], [ChqNo], [ChqDate], [Invoice_Type], [Invoice_In], [DueDays], [DueDate], [Agent_ID], [Subledger_Id], [PO_Invoice], [PO_Date], [QOT_Invoice], [QOT_Date], [Cls1], [Cls2], [Cls3], [Cls4], [Cur_Id], [Cur_Rate], [Counter_ID], [B_Amount], [T_Amount], [N_Amount], [LN_Amount], [Tender_Amount], [Change_Amount], [V_Amount], [Tbl_Amount], [Action_Type], [R_Invoice], [No_Print], [In_Words], [Remarks], [Audit_Lock], [Enter_By], [Enter_Date], [Reconcile_By], [Reconcile_Date], [Auth_By], [Auth_Date], [CUnit_Id], [CBranch_Id], [FiscalYearId], [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5], [ModifyAction], [ModifyBy], [ModifyDate])
			SELECT [PC_Invoice], [Invoice_Date], [Invoice_Miti], [Invoice_Time], [PB_Vno], [Vno_Date], [Vno_Miti], [Vendor_ID], [Party_Name], [Vat_No], [Contact_Person], [Mobile_No], [Address], [ChqNo], [ChqDate], [Invoice_Type], [Invoice_In], [DueDays], [DueDate], [Agent_Id], [Subledger_Id], [PO_Invoice], [PO_Date], [QOT_Invoice], [QOT_Date], [Cls1], [Cls2], [Cls3], [Cls4], [Cur_Id], [Cur_Rate], [Counter_ID], [B_Amount], [T_Amount], [N_Amount], [LN_Amount], [Tender_Amount], [Change_Amount], [V_Amount], [Tbl_Amount], [Action_type], [R_Invoice], [No_Print], [In_Words], [Remarks], [Audit_Lock], [Enter_By], [Enter_Date], [Reconcile_By], [Reconcile_Date], [Auth_By], [Auth_Date], [CUnit_Id], [CBranch_Id], [FiscalYearId], [PAttachment1], [PAttachment2], [PAttachment3], [PAttachment4], [PAttachment5], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
			FROM AMS.[PC_Master]
			WHERE PC_Invoice='{PcMaster.PC_Invoice}';
			INSERT INTO [AUD].[AUDIT_PC_DETAILS] ([PC_Invoice], [Invoice_SNo], [P_Id], [Gdn_Id], [Alt_Qty], [Alt_UnitId], [Qty], [Unit_Id], [Rate], [B_Amount], [T_Amount], [N_Amount], [AltStock_Qty], [Stock_Qty], [Narration], [PO_Invoice], [PO_Sno], [QOT_Invoice], [QOT_SNo], [Tax_Amount], [V_Amount], [V_Rate], [Issue_Qty], [Free_Unit_Id], [Free_Qty], [StockFree_Qty], [ExtraFree_Unit_Id], [ExtraFree_Qty], [ExtraStockFree_Qty], [T_Product], [P_Ledger], [PR_Ledger], [SZ1], [SZ2], [SZ3], [SZ4], [SZ5], [SZ6], [SZ7], [SZ8], [SZ9], [SZ10], [Serial_No], [Batch_No], [Exp_Date], [Manu_Date], [ModifyAction], [ModifyBy], [ModifyDate])
			SELECT [PC_Invoice], [Invoice_SNo], [P_Id], [Gdn_Id], [Alt_Qty], [Alt_UnitId], [Qty], [Unit_Id], [Rate], [B_Amount], [T_Amount], [N_Amount], [AltStock_Qty], [Stock_Qty], [Narration], [PO_Invoice], [PO_Sno], [QOT_Invoice], [QOT_SNo], [Tax_Amount], [V_Amount], [V_Rate], [Issue_Qty], [Free_Unit_Id], [Free_Qty], [StockFree_Qty], [ExtraFree_Unit_Id], [ExtraFree_Qty], [ExtraStockFree_Qty], [T_Product], [P_Ledger], [PR_Ledger], [SZ1], [SZ2], [SZ3], [SZ4], [SZ5], [SZ6], [SZ7], [SZ8], [SZ9], [SZ10], [Serial_No], [Batch_No], [Exp_Date], [Manu_Date], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
			FROM AMS.PC_Details
			WHERE [PC_Invoice]='{PcMaster.PC_Invoice}';
			INSERT INTO [AUD].[AUDIT_PC_TERM] ([PC_VNo], [PT_Id], [SNo], [Rate], [Amount], [Term_Type], [Product_Id], [Taxable], [ModifyAction], [ModifyBy], [ModifyDate])
			SELECT [PC_VNo], [PT_Id], [SNo], [Rate], [Amount], [Term_Type], [Product_Id], [Taxable], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
			FROM AMS.[PC_Term]
			WHERE [PC_VNo]='{PcMaster.PC_Invoice}'; ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public int PurchaseChallanTermPosting()
    {
        var cmdString = $@"
			DELETE AMS.PC_Term WHERE Term_Type='BT' AND PC_VNo='{PcMaster.PC_Invoice}';
			INSERT INTO AMS.PC_Term(PC_VNo, PT_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
			SELECT sbt.PC_VNo, PT_Id, ROW_NUMBER() OVER (ORDER BY(SELECT sbt.PC_VNo)) AS SERIAL_NO, sbt.Rate, (sbt.Amount / sm.B_Amount)* sd.N_Amount Amount, 'BT' Term_Type, sd.P_Id Product_Id, Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion, sbt.SyncBaseId
			FROM AMS.PC_Details sd
				 LEFT OUTER JOIN AMS.PC_Master sm ON sm.PC_Invoice=sd.PC_Invoice
				 LEFT OUTER JOIN AMS.PC_Term sbt ON sd.PC_Invoice=sbt.PC_VNo
			WHERE sbt.Term_Type='B' AND Product_Id IS NULL AND sbt.Amount>0 AND sbt.PC_VNo='{PcMaster.PC_Invoice}';";
        var saveResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return saveResult;
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

    // RETURN VALUE IN DATA TABLE
    public DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = $" SELECT * FROM AMS.{tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataSet ReturnPurchaseChallanReturnDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
			SELECT GL.GlID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, SCM.*
			FROM AMS.PCR_Master AS SCM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=SCM.Vendor_ID
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=SCM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=SCM.Agent_Id
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=SCM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=SCM.Cls1
			WHERE SCM.PCR_Invoice = @voucherNo;
			SELECT SCD.PCR_Invoice, SCD.Invoice_SNo, P.PName, P.PShortName, SCD.P_Id, G.GName, G.GCode, SCD.Gdn_Id, SCD.Alt_UnitId, ALTU.UnitCode AS AltUnitId, SCD.Alt_Qty, SCD.Qty, U.UnitCode UnitId, SCD.Unit_Id, SCD.Rate, SCD.B_Amount, SCD.T_Amount, SCD.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, QOT_Sno, QOT_Invoice, PO_Sno, PO_Invoice, SCD.*
			FROM AMS.PCR_Details AS SCD
				 INNER JOIN AMS.Product AS P ON P.PID=SCD.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID=SCD.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
			WHERE SCD.PCR_Invoice=@voucherNo
			ORDER BY SCD.Serial_No;
			SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
			FROM AMS.PCR_Term AS SIT
				 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
			WHERE PCR_VNo=@voucherNo AND Term_Type='P'
			ORDER BY SIT.SNo ASC
			SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
			FROM AMS.PCR_Term AS SIT
				 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
			WHERE PCR_VNo = @voucherNo AND Term_Type='B'
			ORDER BY SIT.SNo ASC ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseChallanDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
			SELECT GL.GlID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, SCM.*
			FROM AMS.PC_Master AS SCM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=SCM.Vendor_ID
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=SCM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=SCM.Agent_Id
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=SCM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=SCM.Cls1
			WHERE SCM.PC_Invoice = @voucherNo;
			SELECT SCD.PC_Invoice, SCD.Invoice_SNo, P.PName, P.PShortName, SCD.P_Id, G.GName, G.GCode, SCD.Gdn_Id, SCD.Alt_UnitId, ALTU.UnitCode AS AltUnitId, SCD.Alt_Qty, SCD.Qty, U.UnitCode UnitId, SCD.Unit_Id, SCD.Rate, SCD.B_Amount, SCD.T_Amount, SCD.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, QOT_Sno, QOT_Invoice, PO_Sno, PO_Invoice, SCD.*
			FROM AMS.PC_Details AS SCD
				 INNER JOIN AMS.Product AS P ON P.PID=SCD.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID=SCD.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID=P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID=P.PUnit
			WHERE SCD.PC_Invoice=@voucherNo
			ORDER BY SCD.Serial_No;
			SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
			FROM AMS.PC_Term AS SIT
				 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
			WHERE PC_VNo=@voucherNo AND Term_Type='P'
			ORDER BY SIT.SNo ASC
			SELECT SIT.SNo, Order_No OrderNo, SBT.ST_Id TermId, SBT.ST_Name TermName, CASE WHEN SBT.ST_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, SBT.ST_Sign Sign, SIT.Product_Id ProductId, SIT.Term_Type TermType, SIT.Rate TermRate, SIT.Amount TermAmt, 'SC' Source, '' Formula
			FROM AMS.PC_Term AS SIT
				 INNER JOIN AMS.ST_Term AS SBT ON SBT.ST_Id=SIT.PT_Id
			WHERE PC_VNo = @voucherNo AND Term_Type='B'
			ORDER BY SIT.SNo ASC ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }
    public DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo)
    {
        var cmdString = $@"
			DECLARE @voucherNo NVARCHAR(50) = '{voucherNo}'
			SELECT GL.GLID, GL.GLName, SL.SLCode, SL.SLName, A.AgentCode, A.AgentName, C.CName, C.Ccode, C.CId, D.DName, PIM.*
			FROM AMS.PB_Master AS PIM
				 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=PIM.Vendor_ID
				 LEFT OUTER JOIN AMS.SubLedger AS SL ON SL.SLId=PIM.Subledger_Id
				 LEFT OUTER JOIN AMS.JuniorAgent AS A ON A.AgentId=PIM.Agent_ID
				 LEFT OUTER JOIN AMS.Currency AS C ON C.CId=PIM.Cur_Id
				 LEFT OUTER JOIN AMS.Department AS D ON D.DId=PIM.Cls1
			WHERE PIM.PB_Invoice = @voucherNo;
			SELECT PID.PB_Invoice, PID.Invoice_SNo, P.PName, PAlias, P.PShortName, PID.P_Id, G.GName, G.GCode, PID.Gdn_Id, P.PAltUnit, PID.Alt_UnitId, ALTU.UnitCode AS AltUnitCode, PID.Alt_Qty, PID.Qty, P.PUnit, U.UnitCode, PID.Unit_Id, PID.Rate, PID.B_Amount, PID.T_Amount, PID.N_Amount, AltStock_Qty, Stock_Qty, Free_Qty, Narration, PO_Sno, PO_Invoice, PC_SNo, PC_Invoice
			FROM AMS.PB_Details AS PID
				 INNER JOIN AMS.Product AS P ON P.PID = PID.P_Id
				 LEFT OUTER JOIN AMS.Godown AS G ON G.GID = PID.Gdn_Id
				 LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = P.PAltUnit
				 LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = P.PUnit
			WHERE PID.PB_Invoice = @voucherNo
			ORDER BY Invoice_SNo
			SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
			FROM AMS.PB_Term AS PIT
				 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
			WHERE PB_Vno=@voucherNo AND Term_Type='P' AND PIT.Product_Id IN (SELECT P_Id FROM AMS.PB_Details WHERE PB_Invoice=@voucherNo)
			ORDER BY PIT.SNo ASC
			SELECT PIT.SNo, Order_No OrderNo, PBT.PT_Id TermId, PBT.PT_Name TermName, CASE WHEN PBT.PT_Basis='V' THEN 'Value' ELSE 'Qty' END Basis, PBT.PT_Sign Sign, PIT.Product_Id ProductId, PIT.Term_Type TermCondition,PBT.PT_Type TermType,PIT.Rate TermRate, PIT.Amount TermAmt, 'PB' Source, '' Formula
			FROM AMS.PB_Term AS PIT
				 INNER JOIN AMS.PT_Term AS PBT ON PBT.PT_Id=PIT.PT_Id
			WHERE PB_Vno = @voucherNo AND Term_Type='B'
			ORDER BY PIT.SNo ASC
            SELECT * FROM AMS.ProductAddInfo
            WHERE VoucherNo = @voucherNo AND Module ='PB'";

        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    // OBJECT FOR THIS FORM
    public PCR_Master PcrMaster { get; set; }
    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    public PC_Master PcMaster { get; set; }
    public List<PCR_Details> DetailsList { get; set; }
    public List<PCR_Term> Terms { get; set; }
}