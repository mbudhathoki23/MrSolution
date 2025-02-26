using DatabaseModule.CloudSync;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface.FinanceTransaction.PostDateCheque;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.FinanceTransaction.PostDateCheque;

public class PostDateChequeRepository : IPostDateChequeRepository
{
    public PostDateChequeRepository()
    {
        PdcMaster = new DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque.PostDateCheque();
    }

    // INSERT UPDATE DELETE
    public int SaveProvisionCheque(string actionTag, string ledgerDesc)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() is "SAVE" or "NEW")
        {
            cmdString.Append(
                " INSERT INTO AMS.PostDateCheque(VoucherNo, VoucherDate, VoucherTime, VoucherMiti, BankLedgerId, VoucherType, Status, BankName, BranchName, ChequeNo, ChqDate, ChqMiti, DrawOn, Amount, LedgerId, SubLedgerId, AgentId, Remarks, DepositedBy, DepositeDate, IsReverse, CancelReason, CancelBy, CancelDate, Cls1, Cls2, Cls3, Cls4, BranchId, CompanyUnitId, FiscalYearId, EnterBy, EnterDate, PAttachment1, PAttachment2, PAttachment3, PAttachment4, PAttachment5, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append(
                $" VALUES ( N'{PdcMaster.VoucherNo}',N'{PdcMaster.VoucherDate:yyyy-MM-dd}',GETDATE(),N'{PdcMaster.VoucherMiti}', \n"); //;
            cmdString.Append(PdcMaster.BankLedgerId > 0 ? $" '{PdcMaster.BankLedgerId}'," : " Null ,"); //Ledger_Id
            cmdString.Append(PdcMaster.VoucherType.IsValueExits() ? $" N'{PdcMaster.VoucherType}'," : "Null,"); // Miti
            cmdString.Append(PdcMaster.Status.IsValueExits() ? $" N'{PdcMaster.Status}'," : "N'Due',"); // Miti
            cmdString.Append(PdcMaster.BankName.IsValueExits() ? $" N'{PdcMaster.BankName}'," : "Null,"); // Miti
            cmdString.Append(PdcMaster.BranchName.IsValueExits()
                ? $" N'{PdcMaster.BranchName}',"
                : "N'OFFICE',"); // Miti
            cmdString.Append(PdcMaster.ChequeNo.IsValueExits() ? $" N'{PdcMaster.ChequeNo}'," : "Null,"); // Miti
            cmdString.Append(PdcMaster.ChequeNo.IsValueExits()
                ? $" N'{PdcMaster.ChqDate:yyyy-MM-dd}',"
                : "Null,"); // Miti
            cmdString.Append(PdcMaster.ChequeNo.IsValueExits() ? $" N'{PdcMaster.ChqMiti}'," : "Null,"); // Miti
            cmdString.Append(PdcMaster.DrawOn.IsValueExits()
                ? $" N'{PdcMaster.DrawOn}',"
                : $"N'{ledgerDesc}',"); // Miti
            cmdString.Append($" '{PdcMaster.Amount.GetDecimal()}',"); //Subledger_Id
            cmdString.Append(PdcMaster.LedgerId > 0 ? $" '{PdcMaster.LedgerId}'," : " Null ,"); //Subledger_Id
            cmdString.Append(PdcMaster.SubLedgerId > 0 ? $" '{PdcMaster.SubLedgerId}'," : " Null ,"); //Subledger_Id
            cmdString.Append(PdcMaster.AgentId > 0 ? $" '{PdcMaster.AgentId}'," : " Null ,"); //Subledger_Id
            cmdString.Append(PdcMaster.Remarks.IsValueExits()
                ? $" N'{PdcMaster.Remarks.Trim().Replace("'", "''")}',"
                : "Null,"); // Miti
            cmdString.Append(" Null ,Null ,0 ,Null ,Null ,NULL,"); //Cls2
            cmdString.Append(PdcMaster.Cls1 > 0 ? $" '{PdcMaster.Cls1}', " : " Null ,"); //Cls1
            cmdString.Append($" Null ,Null ,Null , {ObjGlobal.SysBranchId}, "); //Cls2
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : " Null,");
            cmdString.Append(
                $"{ObjGlobal.SysFiscalYearId}, '{PdcMaster.EnterBy}', GETDATE(), NULL,NULL,NULL,NULL,NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{PdcMaster.SyncRowVersion}); \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.PostDateCheque SET  \n"); //;
            cmdString.Append(PdcMaster.VoucherDate.IsValueExits()
                ? $"VoucherDate= N'{PdcMaster.VoucherDate:yyyy-MM-dd}',"
                : "VoucherDate = GETDATE(),"); //VoucherDate
            cmdString.Append(PdcMaster.VoucherMiti.IsValueExits()
                ? $"VoucherMiti=  N'{PdcMaster.VoucherMiti}',"
                : "VoucherMiti= Null,"); // Miti
            cmdString.Append(PdcMaster.BankLedgerId > 0
                ? $" BankLedgerId= '{PdcMaster.BankLedgerId}',"
                : " BankLedgerId= Null ,"); //Ledger_Id
            cmdString.Append(PdcMaster.VoucherType.IsValueExits()
                ? $"VoucherType= N'{PdcMaster.VoucherType}',"
                : "VoucherType= Null,"); // Miti
            cmdString.Append(PdcMaster.Status.IsValueExits()
                ? $" Status= N'{PdcMaster.Status}',"
                : "Status= N'Due',"); // Miti
            cmdString.Append(PdcMaster.BankName.IsValueExits()
                ? $" BankName= N'{PdcMaster.BankName}',"
                : "BankName= Null,"); // Miti
            cmdString.Append(PdcMaster.BranchName.IsValueExits()
                ? $" BranchName= N'{PdcMaster.BranchName}',"
                : "BranchName= N'Office',"); // Miti
            cmdString.Append(PdcMaster.ChequeNo.IsValueExits()
                ? $" ChequeNo= N'{PdcMaster.ChequeNo}',"
                : "ChequeNo= Null,"); // Miti
            cmdString.Append(PdcMaster.ChequeNo.IsValueExits()
                ? $"ChqDate=  N'{PdcMaster.ChqDate:yyyy-MM-dd}',"
                : "ChqDate= Null,"); // Miti
            cmdString.Append(
                PdcMaster.ChequeNo.IsValueExits() ? $" ChqMiti= N'{PdcMaster.ChqMiti}'," : "ChqMiti= Null,"); // Miti
            cmdString.Append(PdcMaster.DrawOn.IsValueExits()
                ? $"DrawOn=  N'{PdcMaster.DrawOn}',"
                : $"DrawOn= N'{ledgerDesc}',"); // Miti
            cmdString.Append(PdcMaster.Amount > 0 ? $"Amount= '{PdcMaster.Amount}'," : "Amount= 0 ,"); //Subledger_Id
            cmdString.Append(PdcMaster.LedgerId > 0
                ? $"LedgerId= '{PdcMaster.LedgerId}',"
                : "LedgerId= Null ,"); //Subledger_Id
            cmdString.Append(PdcMaster.SubLedgerId > 0
                ? $"SubLedgerId=  '{PdcMaster.SubLedgerId}',"
                : "SubLedgerId=  Null ,"); //Subledger_Id
            cmdString.Append(PdcMaster.AgentId > 0
                ? $"AgentId=  '{PdcMaster.AgentId}',"
                : "AgentId=  Null ,"); //Subledger_Id
            cmdString.Append(PdcMaster.Remarks.IsValueExits()
                ? $"Remarks=  N'{PdcMaster.Remarks.Trim().Replace("'", "''")}',"
                : "Remarks = Null,"); // Miti
            cmdString.Append(PdcMaster.Cls1 > 0 ? $"Cls1=  '{PdcMaster.Cls1}' ," : "Cls1=Null ,"); //Cls1
            cmdString.Append(" IsSynced= 0"); //Cls1
            cmdString.Append($" WHERE VoucherNo = N'{PdcMaster.VoucherNo}';  "); //Enter_Date
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($" DELETE AMS.PostDateCheque where VoucherNo ='{PdcMaster.VoucherNo}' \n");
        }

        var exe = SqlExtensions.ExecuteNonTrans(cmdString.ToString());
        if (exe != 0)
        {
            PdcAccountPosting();
        }

        if (PdcMaster.PAttachment1 != null &&
            (actionTag == "DELETE" || PdcMaster.PAttachment1.Length <= 0))
        {
            return exe;
        }

        cmdString.Clear();
        cmdString.Append(
            $"UPDATE AMS.PostDateCheque SET PAttachment1 = @PImage  WHERE VoucherNo = '{PdcMaster.VoucherNo}' ");
        using var cmd = new SqlCommand(cmdString.ToString(), GetConnection.ReturnConnection());
        if (PdcMaster.PAttachment1 != null)
        {
            cmd.Parameters.Add("@PImage", SqlDbType.VarBinary).Value = PdcMaster.PAttachment1;
        }
        else
        {
            cmd.Parameters.Add("@PImage", SqlDbType.VarBinary).Value = DBNull.Value;
        }

        cmd.ExecuteNonQuery();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncProvisionChequeAsync(actionTag, ledgerDesc));
        }

        return exe;
    }
    public int PdcAccountPosting()
    {
        var query = @"
			DELETE FROM  AMS.AccountDetails WHERE Module='PDC' ";
        query += PdcMaster.VoucherNo.IsValueExits() ? $" AND Voucher_No= '{PdcMaster.VoucherNo}'" : " ";
        query += @"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId,SyncRowVersion)
            SELECT Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, EnterBy, EnterDate, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, Branch_ID, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId,PDC.SyncRowVersion
            FROM(SELECT 'PDC' Module, 0 Serial_No, pdc.VoucherNo Voucher_No, pdc.VoucherDate Voucher_Date, pdc.VoucherMiti Voucher_Miti, pdc.VoucherTime Voucher_Time, pdc.BankLedgerId Ledger_ID, pdc.LedgerId CbLedger_ID, pdc.SubLedgerId Subleder_ID, pdc.AgentId Agent_ID, pdc.Cls1 Department_ID1, NULL Department_ID2, NULL Department_ID3, NULL Department_ID4, 1 Currency_ID, 1 Currency_Rate, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS Debit_Amt, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS Credit_Amt, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS LocalDebit_Amt, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS LocalCredit_Amt, NULL DueDate, 0 DueDays, pdc.Remarks Narration, pdc.Remarks Remarks, pdc.EnterBy EnterBy, pdc.EnterDate EnterDate, NULL RefNo, NULL RefDate, NULL Reconcile_By, NULL Reconcile_Date, NULL Authorize_By, NULL Authorize_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, pdc.ChequeNo Cheque_No, pdc.ChqDate Cheque_Date, pdc.ChqMiti Cheque_Miti, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo, pdc.BranchId Branch_ID, pdc.CompanyUnitId CmpUnit_ID, pdc.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId, pdc.SyncRowVersion
                 FROM AMS.PostDateCheque pdc
                 UNION ALL
                 SELECT 'PDC' Module, 1 Serial_No, pdc.VoucherNo Voucher_No, pdc.VoucherDate Voucher_Date, pdc.VoucherMiti Voucher_Miti, pdc.VoucherTime Voucher_Time, pdc.LedgerId Ledger_ID, pdc.BankLedgerId CbLedger_ID, pdc.SubLedgerId Subleder_ID, pdc.AgentId Agent_ID, pdc.Cls1 Department_ID1, NULL Department_ID2, NULL Department_ID3, NULL Department_ID4, 1 Currency_ID, 1 Currency_Rate, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS Debit_Amt, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS Credit_Amt, CASE WHEN pdc.VoucherType='Payment' THEN pdc.Amount ELSE 0 END AS LocalDebit_Amt, CASE WHEN pdc.VoucherType='Receipt' THEN pdc.Amount ELSE 0 END AS LocalCredit_Amt, NULL DueDate, 0 DueDays, pdc.Remarks Narration, pdc.Remarks Remarks, pdc.EnterBy EnterBy, pdc.EnterDate EnterDate, NULL RefNo, NULL RefDate, NULL Reconcile_By, NULL Reconcile_Date, NULL Authorize_By, NULL Authorize_Date, NULL Clearing_By, NULL Clearing_Date, NULL Posted_By, NULL Posted_Date, pdc.ChequeNo Cheque_No, pdc.ChqDate Cheque_Date, pdc.ChqMiti Cheque_Miti, NULL PartyName, NULL PartyLedger_Id, NULL Party_PanNo, pdc.BranchId Branch_ID, pdc.CompanyUnitId CmpUnit_ID, pdc.FiscalYearId FiscalYearId, NULL DoctorId, NULL PatientId, NULL HDepartmentId,pdc.SyncRowVersion
                 FROM AMS.PostDateCheque pdc) AS PDC";
        query += PdcMaster.VoucherNo.IsValueExits() ? $" WHERE PDC.Voucher_No = '{PdcMaster.VoucherNo}'  \n" : " ";
        query += @" 
            DELETE FROM AMS.AccountDetails WHERE Module='PDC' and Voucher_No COLLATE DATABASE_DEFAULT IN (SELECT cm.Ref_VNo FROM AMS.CB_Master cm WHERE cm.Ref_VNo IS NOT NULL AND cm.Ref_VNo <> '') ";
        query += PdcMaster.VoucherNo.IsValueExits()
            ? ""
            : " \n UPDATE AMS.PostDateCheque SET Status ='Due' WHERE Status COLLATE DATABASE_DEFAULT ='Deposit' AND VoucherNo COLLATE DATABASE_DEFAULT NOT IN(SELECT cm.Ref_VNo COLLATE DATABASE_DEFAULT FROM AMS.CB_Master cm WHERE cm.Ref_VNo COLLATE DATABASE_DEFAULT IS NOT NULL AND cm.Ref_VNo COLLATE DATABASE_DEFAULT<>'');";

        var exe = SqlExtensions.ExecuteNonQuery(query);
        return exe;
    }
    public async Task<int> SyncProvisionChequeAsync(string actionTag, string ledgerDesc)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
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

            getUrl = @$"{configParams.Model.Item2}PostDateCheque/GetPostDateChequeById";
            insertUrl = @$"{configParams.Model.Item2}PostDateCheque/InsertPostDateCheque";
            updateUrl = @$"{configParams.Model.Item2}PostDateCheque/UpdatePostDateCheque";
            deleteUrl = @$"{configParams.Model.Item2}PostDateCheque/DeletePostDateChequeAsync?id=" + PdcMaster.PDCId;

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

            var pdcRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque.PostDateCheque>(DataSyncManager.GetGlobalInjectData());
            var pdc = new DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque.PostDateCheque
            {
                VoucherNo = PdcMaster.VoucherNo,
                VoucherDate = Convert.ToDateTime(PdcMaster.VoucherDate.ToString("yyyy-MM-dd")),
                VoucherTime = DateTime.Now,
                VoucherMiti = PdcMaster.VoucherMiti,
                BankLedgerId = PdcMaster.BankLedgerId,
                VoucherType = PdcMaster.VoucherType.IsValueExits() ? PdcMaster.VoucherType : null,
                Status = PdcMaster.Status.IsValueExits() ? PdcMaster.Status : "Due",
                BankName = PdcMaster.BankName.IsValueExits() ? PdcMaster.BankName : null,
                BranchName = PdcMaster.BranchName.IsValueExits() ? PdcMaster.BranchName : "OFFICE",
                ChequeNo = PdcMaster.ChequeNo.IsValueExits() ? PdcMaster.ChequeNo : null,
                ChqDate = PdcMaster.ChequeNo.IsValueExits()
                    ? Convert.ToDateTime(PdcMaster.ChqDate?.ToString("yyyy-MM-dd"))
                    : null,
                ChqMiti = PdcMaster.ChequeNo.IsValueExits() ? PdcMaster.ChqMiti : null,
                DrawOn = PdcMaster.DrawOn.IsValueExits() ? PdcMaster.DrawOn : ledgerDesc,
                Amount = PdcMaster.Amount.GetDecimal(),
                LedgerId = PdcMaster.LedgerId,
                SubLedgerId = PdcMaster.SubLedgerId > 0 ? PdcMaster.SubLedgerId : null,
                AgentId = PdcMaster.AgentId > 0 ? PdcMaster.AgentId : null,
                Remarks = PdcMaster.Remarks.IsValueExits() ? PdcMaster.Remarks.Trim().Replace("'", "''") : null,
                DepositedBy = null,
                DepositeDate = null,
                IsReverse = false,
                CancelReason = null,
                CancelBy = null,
                CancelDate = null,
                Cls1 = PdcMaster.Cls1 > 0 ? PdcMaster.Cls1 : null,
                Cls2 = null,
                Cls3 = null,
                Cls4 = null,
                BranchId = ObjGlobal.SysBranchId,
                CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                FiscalYearId = ObjGlobal.SysFiscalYearId,
                EnterBy = PdcMaster.EnterBy,
                EnterDate = DateTime.Now
            };
            pdc.PAttachment1 = pdc.PAttachment1;
            pdc.PAttachment2 = null;
            pdc.PAttachment3 = null;
            pdc.PAttachment4 = null;
            pdc.PAttachment5 = null;
            pdc.SyncRowVersion = PdcMaster.SyncRowVersion;

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await (pdcRepo?.PushNewAsync(pdc)),
                "NEW" => await (pdcRepo?.PushNewAsync(pdc)),
                "UPDATE" => await (pdcRepo?.PutNewAsync(pdc)),
                //"REVERSE" => await salesChallanRepo?.PutNewAsync(pc),
                //"DELETE" => await salesChallanRepo?.DeleteNewAsync(),
                _ => await (pdcRepo?.PushNewAsync(pdc))
            };
            if (result.Value)
            {
                var cmdString = new StringBuilder();
                cmdString.Append($"UPDATE AMS.PostDateCheque SET IsSynced = 1 WHERE PDCId = '{pdc.PDCId}';\n");
                SaveDataInDatabase(cmdString);
            }

            return 1;
        }
        catch (Exception


               ex)
        {
            return 1;
        }
    }
    private int SaveDataInDatabase(string getQuery)
    {
        var exe = SqlExtensions.ExecuteNonTrans(getQuery.ToString());
        return exe;
    }
    private int SaveDataInDatabase(StringBuilder getQuery)
    {
        return SaveDataInDatabase(getQuery.ToString());
    }

    // RETURN VALUE OF DATA TABLE
    public DataTable GetPostDatedChequeVoucher(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
            SELECT pdc.PDCId, pdc.voucherNo, pdc.VoucherDate, pdc.VoucherMiti, pdc.BankLedgerId, gl1.GLName BankLedger, pdc.VoucherType, pdc.Status, pdc.BankName, pdc.BranchName, pdc.ChequeNo, pdc.ChqDate, pdc.ChqMiti, pdc.DrawOn, pdc.Amount, pdc.LedgerId, gl.GLName LedgerDesc, pdc.SubLedgerId, s.SLName SubLedger, pdc.AgentId, ja.AgentName SalesMan, pdc.Remarks, pdc.DepositedBy, pdc.DepositeDate, pdc.CancelReason, pdc.CancelBy, pdc.CancelDate, pdc.Cls1, d.DName Department, pdc.Cls2, pdc.Cls3, pdc.Cls4, pdc.CompanyUnitId, pdc.BranchId, pdc.EnterBy, pdc.EnterDate, pdc.FiscalYearId, pdc.VoucherTime, [PAttachment1]
            FROM AMS.PostDateCheque pdc
	             LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = pdc.LedgerId
	             LEFT OUTER JOIN AMS.GeneralLedger gl1 ON gl1.GLID = pdc.BankLedgerId
	             LEFT OUTER JOIN AMS.SubLedger s ON s.SLId = pdc.SubLedgerId
	             LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = pdc.AgentId
	             LEFT OUTER JOIN AMS.Department d ON d.DId = pdc.Cls1
            WHERE pdc.Status = 'Due' AND (BranchId = '{ObjGlobal.SysBranchId}' OR BranchId IS NULL) AND (CompanyUnitId = '{ObjGlobal.SysCompanyUnitId}' OR CompanyUnitId IS NULL) AND pdc.VoucherNo = '{voucherNo}'");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }


    // OBJECT FOR THIS FORM
    public DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque.PostDateCheque PdcMaster { get; set; }
}