using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.FinanceTransaction.JournalVoucher;
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

namespace MrDAL.DataEntry.FinanceTransaction.JournalVoucher;

public class JournalVoucherRepository : IJournalVoucherRepository
{
    public JournalVoucherRepository()
    {
        JvMaster = new JV_Master();
        JvDetailsList = new List<JV_Details>();
        //JvDetails = new JV_Details();
    }

    // INSERT UPDATE DELETE
    private int SaveDataInDatabase(string getQuery)
    {
        var exe = SqlExtensions.ExecuteNonTrans(getQuery);
        return exe;
    }
    private int SaveDataInDatabase(StringBuilder getQuery)
    {
        return SaveDataInDatabase(getQuery.ToString());
    }
    public async Task<int> SyncJournalVoucherAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
                //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //configParams.ShowErrorDialog();
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
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}JournalVoucher/GetJournalVoucherById";
            insertUrl = @$"{configParams.Model.Item2}JournalVoucher/InsertJournalVoucher";
            updateUrl = @$"{configParams.Model.Item2}JournalVoucher/UpdateJournalVoucher";
            deleteUrl = @$"{configParams.Model.Item2}JournalVoucher/DeleteJournalVoucherAsync?id=" +
                        JvMaster.Voucher_No;

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

            var jvRepo = DataSyncProviderFactory.GetRepository<JV_Master>(DataSyncManager.GetGlobalInjectData());
            var jv = new JV_Master
            {
                VoucherMode = JvMaster.VoucherMode.IsValueExits() ? JvMaster.VoucherMode : "MDMC",
                Voucher_No = JvMaster.Voucher_No,
                Voucher_Date = Convert.ToDateTime(JvMaster.Voucher_Date.GetSystemDate()),
                Voucher_Miti = JvMaster.Voucher_Miti,
                Voucher_Time = DateTime.Now,
                Ref_VNo = JvMaster.Ref_VNo.IsValueExits() ? JvMaster.Ref_VNo : null,
                Ref_VDate = JvMaster.Ref_VNo.IsValueExits()
                    ? Convert.ToDateTime(JvMaster.Ref_VDate.GetSystemDate())
                    : null,
                Currency_Id = JvMaster.Currency_Id > 0 ? JvMaster.Currency_Id : ObjGlobal.SysCurrencyId,
                Currency_Rate = JvMaster.Currency_Rate > 0 ? JvMaster.Currency_Rate : 1,
                Cls1 = JvMaster.Cls1 > 0 ? JvMaster.Cls1 : null,
                Cls2 = null,
                Cls3 = null,
                Cls4 = null,
                N_Amount = JvMaster.N_Amount.GetDecimal(),
                Remarks = JvMaster.Remarks.IsValueExits() ? JvMaster.Remarks.Trim().GetTrimReplace() : null,
                Action_Type = actionTag,
                EnterBy = ObjGlobal.LogInUser.ToUpper(),
                EnterDate = DateTime.Now,
                Audit_Lock = false,
                IsReverse = false,
                CancelBy = null,
                CancelDate = null,
                CancelReason = null,
                ReconcileBy = null,
                ReconcileDate = null,
                ClearingBy = null,
                ClearingDate = null,
                PrintValue = null,
                CBranch_Id = ObjGlobal.SysBranchId,
                CUnit_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                FiscalYearId = ObjGlobal.SysFiscalYearId,
                PAttachment1 = null,
                PAttachment2 = null,
                PAttachment3 = null,
                PAttachment4 = null,
                PAttachment5 = null,
                SyncRowVersion = JvMaster.SyncRowVersion
            };

            var jvDetails = new List<JV_Details>();
            if (actionTag != "DELETE" && actionTag != "REVERSE")
            {
                jvDetails.AddRange(JvMaster.GetView.Rows.Cast<DataGridViewRow>().Select(drCel => new JV_Details
                {
                    Voucher_No = JvMaster.Voucher_No,
                    SNo = drCel.Cells["GTxtSNo"].Value.GetInt(),
                    CBLedger_Id = null,
                    Ledger_Id = drCel.Cells["GTxtLedgerId"].Value.GetLong(),
                    Subledger_Id =
                        drCel.Cells["GTxtSubledgerId"].Value.GetInt() > 0
                            ? drCel.Cells["GTxtSubledgerId"].Value.GetInt()
                            : null,
                    Agent_Id =
                        drCel.Cells["GTxtAgentId"].Value.GetInt() > 0
                            ? drCel.Cells["GTxtAgentId"].Value.GetInt()
                            : null,
                    Cls1 = null,
                    Cls2 = null,
                    Cls3 = null,
                    Cls4 = null,
                    Chq_No = null,
                    Chq_Date = null,
                    CurrencyId =
                        drCel.Cells["GTxtCurrencyId"].Value.GetInt() > 0
                            ? drCel.Cells["GTxtCurrencyId"].Value.GetInt()
                            : ObjGlobal.SysCurrencyId,
                    CurrencyRate = drCel.Cells["GTxtExchangeRate"].Value.GetDecimal(true),
                    Debit = drCel.Cells["GTxtDebit"].Value.GetDecimal(),
                    Credit = drCel.Cells["GTxtCredit"].Value.GetDecimal(),
                    LocalDebit = drCel.Cells["GTxtLocalDebit"].Value.GetDecimal(),
                    LocalCredit = drCel.Cells["GTxtCredit"].Value.GetDecimal(),
                    Narration = drCel.Cells["GTxtNarration"].Value.ToString().Replace("'", "''"),
                    Tbl_Amount = 0,
                    V_Amount = 0,
                    Vat_Reg = false,
                    Party_No = null,
                    Invoice_Date = null,
                    Invoice_Miti = null,
                    VatLedger_Id = null,
                    PanNo = null,
                    SyncRowVersion = JvMaster.SyncRowVersion
                }));
            }

            jv.DetailsList = jvDetails;
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await (jvRepo?.PushNewAsync(jv)!),
                "NEW" => await (jvRepo?.PushNewAsync(jv)!),
                "UPDATE" => await (jvRepo?.PutNewAsync(jv)),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await (jvRepo?.PushNewAsync(jv)!)
            };
            if (result.Value)
            {
                var cmdString = new StringBuilder();
                cmdString.Append($@"
                    UPDATE AMS.JV_Master SET IsSynced = 1 WHERE Voucher_No = '{JvMaster.Voucher_No}';");
                SaveDataInDatabase(cmdString);
            }

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    private void AuditLogJournalVoucher()
    {
        var cmdString = $@"
        INSERT INTO AUD.AUDIT_JV_MASTER(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks, Action_Type, Audit_Lock, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
        SELECT VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks,N'{JvMaster.Action_Type}' Action_Type, Audit_Lock, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, EnterBy, EnterDate, N'{JvMaster.Action_Type}' ModifyAction, N'{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
        FROM AMS.JV_Master jm
        WHERE jm.Voucher_No = '{JvMaster.Voucher_No}';

        INSERT INTO AUD.AUDIT_JV_DETAILS(Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CBLedger_Id, Chq_No, Chq_Date, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Vat_Reg, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, CurrencyId, CurrencyRate, ModifyAction, ModifyBy, ModifyDate)
        SELECT Voucher_No, SNo, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, CBLedger_Id, Chq_No, Chq_Date, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Vat_Reg, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, CurrencyId, CurrencyRate, N'{JvMaster.Action_Type}' ModifyAction,N'{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
        FROM AMS.JV_Details jd
        WHERE jd.Voucher_No ='{JvMaster.Voucher_No}'; ";
        SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public int JournalVoucherAccountPosting()
    {
        var query = @"Delete from  AMS.AccountDetails Where Module in ('JV') ";
        if (((object)JvMaster.Voucher_No).IsValueExits())
        {
            query += $" and Voucher_No='{JvMaster.Voucher_No}' ";
        }

        query += @"
			INSERT INTO AMS.AccountDetails (Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId)
             SELECT 'JV' AS Module, SNo Serial_No, JVM.Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, JVD.Ledger_Id, CBLedger_Id, Subledger_Id, Agent_Id, ISNULL(JVM.Cls1, JVD.Cls1), ISNULL(JVM.Cls2, JVD.Cls2), ISNULL(JVM.Cls3, JVD.Cls3), ISNULL(JVM.Cls4, JVD.Cls4), ISNULL(JVD.CurrencyId,JVM.Currency_Id) Currency_Id,ISNULL(JVD.CurrencyRate,JVM.Currency_Rate) Currency_Rate, Debit, Credit, CASE WHEN LocalDebit=0 THEN Debit ELSE LocalDebit END, CASE WHEN LocalCredit=0 THEN Credit ELSE LocalCredit END, NULL DueDate, NULL DueDays, Narration, Remarks, EnterBy, EnterDate, Ref_VNo, Ref_VDate, ReconcileBy, ReconcileDate, NULL Authorize_By, NULL Authorize_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL PostedDate, Chq_No, Chq_Date, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo, CBranch_Id, CUnit_Id, JVM.FiscalYearId
              FROM AMS.JV_Master JVM, AMS.JV_Details JVD
              WHERE JVM.Voucher_No = JVD.Voucher_No AND JVM.Action_Type <> 'Cancel' ";
        if (((object)JvMaster.Voucher_No).IsValueExits())
        {
            query += $" and JVM.Voucher_No = '{JvMaster.Voucher_No}' ";
        }

        var exe = SqlExtensions.ExecuteNonQuery(query);
        return exe;
    }
    public int SaveJournalVoucher(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (_tagStrings.Contains(actionTag))
        {
            cmdString.Append(
                $"DELETE AMS.AccountDetails WHERE Voucher_No = '{JvMaster.Voucher_No}' AND Module = 'JV' \n");
            if (actionTag != "REVERSE")
            {
                cmdString.Append($"DELETE AMS.JV_Details WHERE Voucher_No = '{JvMaster.Voucher_No}' \n");
            }

            if (actionTag is "DELETE")
            {
                cmdString.Append($"DELETE AMS.JV_Master WHERE Voucher_No = '{JvMaster.Voucher_No}' \n");
            }

            if (actionTag != "UPDATE")
            {
                AuditLogJournalVoucher();
            }
        }

        if (actionTag is "SAVE")
        {
            cmdString.Append(@"
                INSERT INTO AMS.JV_Master(VoucherMode, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ref_VNo, Ref_VDate, Currency_Id, Currency_Rate, Cls1, Cls2, Cls3, Cls4, N_Amount, Remarks, Action_Type, EnterBy, EnterDate, Audit_Lock, IsReverse, CancelBy, CancelDate, CancelReason, ReconcileBy, ReconcileDate, ClearingBy, ClearingDate, PrintValue, CBranch_Id, CUnit_Id, FiscalYearId, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append("\n VALUES ( ");
            cmdString.Append(JvMaster.VoucherMode.IsValueExits() ? $"'{JvMaster.VoucherMode}', " : "'MDMC'");
            cmdString.Append($"N'{JvMaster.Voucher_No}','{JvMaster.Voucher_Date.GetSystemDate()}',N'{JvMaster.Voucher_Miti}',GETDATE(),");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits() ? $"'{JvMaster.Ref_VNo}', " : "NULL,");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits() ? $"'{JvMaster.Ref_VDate.GetSystemDate()}', " : "NULL,");
            cmdString.Append(JvMaster.Currency_Id > 0 ? $"'{JvMaster.Currency_Id}', " : $"{ObjGlobal.SysCurrencyId},");
            cmdString.Append(JvMaster.Currency_Rate > 0 ? $"'{JvMaster.Currency_Rate}', " : "1,");
            cmdString.Append(JvMaster.Cls1 > 0 ? $"'{JvMaster.Cls1}', " : "NULL,");
            cmdString.Append($" NULL, NULL, NULL, {JvMaster.N_Amount.GetDecimal()},");
            cmdString.Append(JvMaster.Remarks.IsValueExits() ? $"'{JvMaster.Remarks.Trim().GetTrimReplace()}'," : " NULL,");
            cmdString.Append($"'SAVE', N'{ObjGlobal.LogInUser.ToUpper()}', GETDATE(), 0,0, NULL,NULL, NULL, NULL,NULL, NULL, NULL, 0, {ObjGlobal.SysBranchId},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($" {ObjGlobal.SysFiscalYearId}, NULL, NULL, NULL, NULL, NULL,");
            cmdString.Append($" NULL, NULL, NULL, NULL, NULL,{JvMaster.SyncRowVersion.GetDecimal(true)} ); \n");
        }
        else if (actionTag is "UPDATE")
        {
            cmdString.Append("UPDATE AMS.JV_Master SET ");
            cmdString.Append(
                $"Voucher_Date = '{JvMaster.Voucher_Date.GetSystemDate()}', Voucher_Miti = N'{JvMaster.Voucher_Miti}',");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits() ? $"Ref_VNo = '{JvMaster.Ref_VNo}', " : "Ref_VNo = NULL,");
            cmdString.Append(JvMaster.Ref_VNo.IsValueExits()
                ? $"Ref_VDate = '{JvMaster.Ref_VDate.GetSystemDate()}', "
                : "Ref_VDate = NULL,");
            cmdString.Append(JvMaster.Currency_Id > 0
                ? $"Currency_Id = '{JvMaster.Currency_Id}', "
                : $"Currency_Id = {ObjGlobal.SysCurrencyId},");
            cmdString.Append(JvMaster.Currency_Rate > 0
                ? $"Currency_Rate = '{JvMaster.Currency_Rate}', "
                : "Currency_Rate= 1,");
            cmdString.Append(JvMaster.Cls1 > 0 ? $"Cls1 = '{JvMaster.Cls1}', " : "Cls1 = NULL,");
            cmdString.Append(JvMaster.Remarks.IsValueExits() ? $"Remarks = '{JvMaster.Remarks}', " : "Remarks = NULL,");
            cmdString.Append($"SyncRowVersion =  {JvMaster.SyncRowVersion},");
            cmdString.Append($"Action_Type = 'UPDATE', IsSynced=0 WHERE Voucher_No = N'{JvMaster.Voucher_No}' ; \n");
        }

        if (actionTag != "DELETE" && actionTag != "REVERSE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.JV_Details(Voucher_No, SNo, CBLedger_Id, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Chq_No, Chq_Date, CurrencyId, CurrencyRate, Debit, Credit, LocalDebit, LocalCredit, Narration, Tbl_Amount, V_Amount, Vat_Reg, Party_No, Invoice_Date, Invoice_Miti, VatLedger_Id, PanNo, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append("\n VALUES  \n");
            var index = 0;
            foreach (var details in JvDetailsList)
            {
                index++;
                cmdString.Append($"('{details.Voucher_No}', {details.SNo},");
                cmdString.Append(details.CBLedger_Id > 0 ? $" {details.CBLedger_Id}," : "NULL,");
                cmdString.Append($"{details.Ledger_Id},");
                cmdString.Append(details.Subledger_Id > 0 ? $"{details.Subledger_Id}," : "NULL,");
                cmdString.Append(details.Agent_Id > 0 ? $"{details.Agent_Id}," : "NULL,");
                cmdString.Append(details.Cls1 > 0 ? $" {details.Cls1}," : "NULL,");
                cmdString.Append(details.Cls2 > 0 ? $" {details.Cls2}," : "NULL,");
                cmdString.Append(details.Cls3 > 0 ? $" {details.Cls3}," : "NULL,");
                cmdString.Append(details.Cls4 > 0 ? $" {details.Cls4}," : "NULL,");
                cmdString.Append(details.Chq_No.IsValueExits() ? $"'{details.Chq_No}'," : "NULL,");
                cmdString.Append(details.Chq_No.IsValueExits() ? $"'{details.Chq_Date}'," : "NULL,");
                cmdString.Append(details.CurrencyId > 0 ? $" {details.CurrencyId}," : $"{ObjGlobal.SysCurrencyId},");
                cmdString.Append(details.CurrencyRate > 0 ? $" {details.CurrencyRate}," : "1,");
                cmdString.Append(details.Debit > 0 ? $" {details.Debit}," : "0,");
                cmdString.Append(details.Credit > 0 ? $" {details.Credit}," : "0,");
                cmdString.Append(details.LocalDebit > 0 ? $"{details.LocalDebit}," : "0,");
                cmdString.Append(details.LocalCredit > 0 ? $"{details.LocalCredit}," : "0,");
                cmdString.Append(details.Narration.IsValueExits() ? $"'{details.Narration}'," : "NULL,");
                cmdString.Append(details.Tbl_Amount > 0 ? $" {details.Tbl_Amount}," : "0,");
                cmdString.Append(details.V_Amount > 0 ? $" {details.V_Amount}," : "0,");
                cmdString.Append(details.Vat_Reg is true ? $"1," : "0,");
                cmdString.Append(details.Party_No.IsValueExits() ? $"'{details.Party_No}'," : "NULL,");
                cmdString.Append(details.Party_No.IsValueExits() ? $"'{details.Invoice_Date.GetSystemDate()}'," : "NULL,");
                cmdString.Append(details.Party_No.IsValueExits() ? $"'{details.Invoice_Miti}'," : "NULL,");
                cmdString.Append(details.VatLedger_Id > 0 ? $" {details.VatLedger_Id}," : "NULL,");
                cmdString.Append(details.Party_No.IsValueExits() ? $"'{details.PanNo}'," : "NULL,");
                cmdString.Append(details.SyncBaseId.IsGuidExits() ? $"'{details.SyncBaseId}'," : "NULL,");
                cmdString.Append(details.SyncGlobalId.IsGuidExits() ? $"'{details.SyncGlobalId}'," : "NULL,");
                cmdString.Append(details.SyncOriginId.IsGuidExits() ? $"'{details.SyncOriginId}'," : "NULL,");
                cmdString.Append($"'{details.SyncCreatedOn.GetSystemDate()}','{details.SyncLastPatchedOn.GetSystemDate()}',");
                cmdString.Append(details.SyncRowVersion > 0 ? $"{details.SyncRowVersion}" : $"{JvMaster.SyncRowVersion}");
                cmdString.Append(index == JvDetailsList.Count ? "); \n" : "), \n");
            }
        }

        var saveData = SaveDataInDatabase(cmdString);
        if (saveData <= 0)
        {
            return saveData;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncJournalVoucherAsync(actionTag));
        }

        if (actionTag.Equals("DELETE"))
        {
            return saveData;
        }

        AuditLogJournalVoucher();
        if (JvMaster.VoucherMode != "PROV")
        {
            JournalVoucherAccountPosting();
        }

        return saveData;
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable IsCheckVoucherNoExits(string tableName, string tableVoucherNo, string inputVoucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($" SELECT * FROM {tableName} WHERE {tableVoucherNo}= '{inputVoucherNo}'");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }
    public DataSet ReturnJournalVoucherInDataSet(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			SELECT CBM.Voucher_No,CONVERT(varchar(10),Voucher_Date,103)Voucher_Date ,Voucher_Miti, CBM.Ref_VNo, CBM.Ref_VDate,CBM.Currency_Id,CUR.Ccode,CName,CBM.Currency_Rate,CBM.Cls1,D1.DName Department1,CBM.Cls2,D2.DName Department2, CBM.Cls3,D3.DName Department3 ,CBM.Cls4,D4.DName Department4,Remarks,CBM.PAttachment1, CBM.PAttachment2, CBM.PAttachment3, CBM.PAttachment4, CBM.PAttachment5 FROM AMS.JV_Master as CBM Left Outer Join AMS.Currency as CUR ON Cur.CId=CBM.Currency_Id Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4  Where CBM.Voucher_No='{voucherNo}'
			SELECT CBD.SNo,CBM.Voucher_No,GL.GLID,GL.GLCode,GLName,CBD.Subledger_Id,SL.SLName,SL.SLCode,CBD.Agent_ID,AG.AgentName,AG.AgentCode,CBD.Cls1,D1.DName Department1,CBD.Cls2,D2.DName Department2, CBD.Cls3,D3.DName Department3 ,CBD.Cls4,D4.DName Department4,CBD.CurrencyId,c.Ccode,CBD.CurrencyRate,Debit,Credit,LocalDebit,LocalCredit,Narration,Tbl_Amount,V_Amount,Party_No,Invoice_Date,Invoice_Miti,VatLedger_Id,CBD.PanNo FROM AMS.JV_Master as CBM  Inner Join AMS.JV_Details as CBD On CBD.Voucher_No=CBM.Voucher_No Inner Join AMS.GeneralLedger as GL On GL.GLID=CBD.Ledger_Id Left Outer Join AMS.SubLedger as SL On SL.SLId=CBD.Subledger_Id Left Outer Join AMS.JuniorAgent as AG ON AG.AgentId=CBD.Agent_ID Left Outer Join AMS.Department as D1 On D1.DId=CBM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=CBM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=CBM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=CBM.Cls4 LEFT HASH JOIN AMS.Currency c ON CBD.CurrencyId=c.CId Where CBM.Voucher_No='{voucherNo}' ORDER BY CBD.SNo");
        return SqlExtensions.ExecuteDataSet(
            cmdString.ToString());
    }

    // OBJECT FOR THIS FORM
    public JV_Details JvDetails { get; set; }
    public List<JV_Details> JvDetailsList { get; set; }
    public JV_Master JvMaster { get; set; }
    private readonly string[] _tagStrings = { "DELETE", "UPDATE", "REVERSE" };

}