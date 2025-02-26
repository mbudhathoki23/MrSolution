using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.OpeningMaster;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.OpeningMaster;

[SuppressMessage("ReSharper", "UseConfigureAwaitFalse")]
public class LedgerOpeningRepository : ILedgerOpeningRepository
{
    // INSERT UPDATE DELETE
    private int SaveDataInDatabase(string getQuery)
    {
        var exe = SqlExtensions.ExecuteNonTrans(getQuery.ToString());
        return exe;
    }
    private int SaveDataInDatabase(StringBuilder getQuery)
    {
        return SaveDataInDatabase(getQuery.ToString());
    }
    public int SaveLedgerOpening(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "UPDATE" or "DELETE")
        {
            cmdString.Append(
                $"Delete from AMS.AccountDetails where Module= 'LOB' and Voucher_No = '{GetOpening.Voucher_No}' \n");
            cmdString.Append(
                $"Delete from [AMS].[LedgerOpening]  Where [Voucher_No] = '{GetOpening.Voucher_No}'  and [Module] IN( 'N','OB','LOB') \n");
        }

        if (actionTag is "SAVE" or "UPDATE")
        {
            if (GetOpening.GetView != null)
            {

                if (Details.Count > 0)
                {
                    foreach (var item in Details)
                    {
                        cmdString.Append(@" 
                            INSERT INTO AMS.LedgerOpening(Opening_Id, Module, Serial_No, Voucher_No, OP_Date, OP_Miti, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, IsReverse, CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                        cmdString.Append("\n VALUES \n");
                        cmdString.Append($"({item.Opening_Id}, "); //OpeningId
                        cmdString.Append($" N'{item.Module}',");
                        cmdString.Append($"{item.Serial_No} , "); //SNo
                        cmdString.Append($" N'{item.Voucher_No}',"); //Voucher No
                        cmdString.Append($" '{item.OP_Date.GetSystemDate()}', N'{item.OP_Miti}', "); // Miti
                        cmdString.Append(item.Ledger_Id > 0 ? $" {item.Ledger_Id}," : " 0,"); //Ledger_Id
                        cmdString.Append(item.Subledger_Id > 0 ? $" '{item.Subledger_Id}'," : " Null ,"); //Subledger_Id
                        cmdString.Append(item.Agent_Id > 0 ? $" '{item.Agent_Id}'," : " Null ,"); //Subledger_Id
                        cmdString.Append(item.Cls1 > 0 ? $" {item.Cls1}, " : " Null ,"); //Cls1
                        cmdString.Append(item.Cls2 > 0 ? $" {item.Cls2} ," : "Null, "); //Cls2
                        cmdString.Append(item.Cls3 > 0 ? $" {item.Cls3} ," : "Null, "); //Cls3
                        cmdString.Append(item.Cls4 > 0 ? $" {item.Cls4} ," : "Null, "); //Cls4
                        cmdString.Append(item.Currency_Id > 0 ? $" {item.Currency_Id}, " : $" {ObjGlobal.SysCurrencyId} ,");
                        cmdString.Append(item.Currency_Rate > 0 ? $" {item.Currency_Rate.GetDecimal()}," : "1,");
                        cmdString.Append($" {item.Debit.GetDecimal()}, {item.LocalDebit.GetDecimal()},");
                        cmdString.Append($" {item.Credit.GetDecimal()}, {item.LocalCredit.GetDecimal()},");
                        cmdString.Append(item.Narration.IsValueExits() ? $" N'{item.Narration}'," : "NULL,"); //Narration
                        cmdString.Append(item.Remarks.IsValueExits() ? $" N'{item.Remarks}'," : "NULL,"); //Remarks
                        cmdString.Append($" N'{item.Enter_By}','{item.Enter_Date.GetSystemDate()}' , N'{item.Reconcile_By}' ,'{item.Reconcile_Date.GetSystemDate()}' ,"); //Enter_By
                        cmdString.Append(item.Branch_Id > 0 ? $"{item.Branch_Id}," : $"{ObjGlobal.SysBranchId},");
                        cmdString.Append(ObjGlobal.SysCompanyUnitId.GetInt() > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : " Null,");
                        cmdString.Append(item.FiscalYearId > 0 ? $"{item.FiscalYearId} ," : $"{ObjGlobal.SysFiscalYearId},");
                        cmdString.Append($" 0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL, {item.SyncRowVersion.GetDecimal(true)}); \n");
                    }
                }
                else
                {
                    cmdString.Append(@"
                        INSERT INTO AMS.LedgerOpening(Opening_Id, Module, Serial_No, Voucher_No, OP_Date, OP_Miti, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, IsReverse, CancelRemarks, CancelBy, CancelDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                    cmdString.Append("\n VALUES \n");
                    cmdString.Append($"( {GetOpening.Opening_Id}, "); //OpeningId
                    cmdString.Append($" '{GetOpening.Module}',");
                    cmdString.Append($" {GetOpening.Serial_No}, "); //SNo
                    cmdString.Append($" '{GetOpening.Voucher_No}',"); //Voucher No
                    cmdString.Append($" '{GetOpening.OP_Date.GetSystemDate()}','{GetOpening.OP_Miti}', "); // Miti
                    cmdString.Append($"{GetOpening.Ledger_Id},"); //Ledger_Id
                    cmdString.Append(GetOpening.Subledger_Id > 0 ? $" '{GetOpening.Subledger_Id}'," : " Null ,"); //Subledger_Id
                    cmdString.Append(GetOpening.Agent_Id > 0 ? $" '{GetOpening.Agent_Id}'," : " Null ,"); //Subledger_Id
                    cmdString.Append(GetOpening.Cls1 > 0 ? $" '{GetOpening.Cls1}', " : " Null ,"); //Cls1
                    cmdString.Append(" Null ,Null ,Null ,"); //Cls2
                    cmdString.Append(GetOpening.Currency_Id > 0 ? $" {GetOpening.Currency_Id}, " : $" {ObjGlobal.SysCurrencyId} ,");
                    cmdString.Append(GetOpening.Currency_Rate > 0 ? $" {GetOpening.Currency_Rate}," : "1,");
                    cmdString.Append($" {GetOpening.Debit.GetDecimal()}, {GetOpening.LocalDebit.GetDecimal()},");
                    cmdString.Append($" {GetOpening.Credit.GetDecimal()}, {GetOpening.LocalCredit.GetDecimal()},");
                    cmdString.Append(!string.IsNullOrEmpty(GetOpening.Narration) ? $" '{GetOpening.Narration}'," : "Null,"); //Narration
                    cmdString.Append($" N'{GetOpening.Remarks}','{ObjGlobal.LogInUser}',GETDATE(), Null ,Null ,{ObjGlobal.SysBranchId},"); //Enter_By
                    cmdString.Append(ObjGlobal.SysCompanyUnitId.GetInt() > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : " Null,");
                    cmdString.Append($" {ObjGlobal.SysFiscalYearId},0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL, {GetOpening.SyncRowVersion.GetDecimal(true)});");
                }
            }
        }

        if (actionTag is "UPDATE" && GetOpening.GetView is null)
        {
            cmdString.Append("  UPDATE AMS.LedgerOpening SET \n"); //OpeningId
            cmdString.Append($" OP_Miti= '{GetOpening.OP_Miti}',"); // Miti
            cmdString.Append($" OP_Date= '{GetOpening.OP_Date}',"); // Miti
            cmdString.Append(GetOpening.Ledger_Id > 0 ? $" Ledger_Id= '{GetOpening.Ledger_Id}'," : " Ledger_Id= Null ,"); //Ledger_Id
            cmdString.Append(GetOpening.Subledger_Id > 0 ? $" Subledger_Id= '{GetOpening.Subledger_Id}'," : " Subledger_Id= Null ,"); //Subledger_Id
            cmdString.Append(GetOpening.Agent_Id > 0 ? $" Agent_Id= '{GetOpening.Agent_Id}'," : " Agent_Id= Null ,"); //Subledger_Id
            cmdString.Append(GetOpening.Cls1 > 0 ? $" Cls1= '{GetOpening.Cls1}', " : " Cls1= Null ,"); //Cls1
            cmdString.Append(GetOpening.Currency_Id > 0 ? $" Currency_Id= '{GetOpening.Currency_Id}', " : $"Currency_Id= {ObjGlobal.SysCurrencyId},");
            cmdString.Append(GetOpening.Currency_Rate > 0 ? $" Currency_Rate= '{GetOpening.Currency_Rate}'," : "Currency_Rate= 1,");
            cmdString.Append($" Debit=  {GetOpening.Debit},LocalDebit=  {GetOpening.LocalDebit},");
            cmdString.Append($" Credit=  {GetOpening.Credit},LocalCredit=  {GetOpening.LocalCredit},");
            cmdString.Append(!string.IsNullOrEmpty(GetOpening.Narration) ? $" Narration = '{GetOpening.Narration.Trim().Replace("'", "''")}'," : "Narration=Null,"); //Narration
            cmdString.Append($" GETDATE(), SyncRowVersion =  {GetOpening.SyncRowVersion}");
            cmdString.Append($" where Voucher_No= '{GetOpening.Voucher_No}' and Module='{GetOpening.Module}'; "); //Narration
        }

        var exe = SaveDataInDatabase(cmdString);
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncLedgerOpeningAsync(actionTag));
        }

        if (actionTag != "DELETE")
        {
            AuditLogLedgerOpening(actionTag);
            LedgerOpeningAccountPosting();
        }

        return exe;
    }
    public async Task<int> SyncLedgerOpeningAsync(string actionTag)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}LedgerOpening/GetLedgerOpeningsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}LedgerOpening/InsertLedgerOpeningList",
            UpdateUrl = @$"{_configParams.Model.Item2}LedgerOpening/UpdateLedgerOpening"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var ledgerOpeningRepo = DataSyncProviderFactory.GetRepository<LedgerOpening>(_injectData);
        var ledgerOpenings = new List<LedgerOpening>
        {
            GetOpening
        };
        // push realtime ledger opening details to server
        await ledgerOpeningRepo.PushNewListAsync(ledgerOpenings);

        // update ledger opening SyncGlobalId to local
        if (ledgerOpeningRepo.GetHashCode() > 0)
        {
            await SyncUpdateLedgerOpening(GetOpening.Opening_Id);
        }

        return ledgerOpeningRepo.GetHashCode();
    }

    public async Task<bool> SyncLedgerOpeningDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);

        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}LedgerOpening/GetLedgerOpeningsCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}LedgerOpening/InsertLedgerOpeningList",
            UpdateUrl = @$"{_configParams.Model.Item2}LedgerOpening/UpdateLedgerOpening"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var ledgerOpeningRepo = DataSyncProviderFactory.GetRepository<LedgerOpening>(_injectData);

        // pull all new ledger opening data
        var pullResponse = await PullAccountGroupsServerToClientByRowCounts(ledgerOpeningRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        //push all new ledger opening data
        var sqlQuery = GetLedgerOpeningScript();
        var queryResponse = await QueryUtils.GetListAsync<LedgerOpening>(sqlQuery);
        var loList = queryResponse.List.ToList();
        if (loList.Count > 0)
        {
            var pushResponse = await ledgerOpeningRepo.PushNewListAsync(loList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm(true);
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateLedgerOpening(int openingId)
    {
        var commandText = $@"
            UPDATE AMS.LedgerOpening SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (openingId > 0)
        {
            commandText += $" WHERE Opening_Id = '{openingId}'";
        }

        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    private void AuditLogLedgerOpening(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_LEDGER_OPENING (Opening_Id, Module, Voucher_No, OP_Date, OP_Miti, Serial_No, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, ModifyAction, ModifyBy, ModifyDate)
            SELECT Opening_Id, Module, Voucher_No, OP_Date, OP_Miti, Serial_No, Ledger_Id, Subledger_Id, Agent_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, Debit, LocalDebit, Credit, LocalCredit, Narration, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, Branch_Id, Company_Id, FiscalYearId, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE ( ) ModifyDate
            FROM AMS.LedgerOpening
            WHERE Voucher_No = '{GetOpening.Voucher_No}';";
        SqlExtensions.ExecuteNonQuery(cmdString);
    }
    public int LedgerOpeningAccountPosting()
    {
        var query = @"
			Delete from [AMS].[AccountDetails]  Where [Module] IN ('N','OB','LOB') ";
        query += GetOpening.Voucher_No.IsValueExits() ? $" and [Voucher_No] = '{GetOpening.Voucher_No}' " : " ";
        query += @"
			INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Ledger_ID, Voucher_Date, Voucher_Miti, Voucher_Time, CbLedger_ID, Subleder_ID, Agent_ID, Department_ID1, Department_ID2, Department_ID3, Department_ID4, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, DueDate, DueDays, Narration, Remarks, RefNo, RefDate, Reconcile_By, Reconcile_Date, Authorize_By, Authorize_Date, Clearing_By, Clearing_Date, Posted_By, Posted_Date, Cheque_No, Cheque_Date, Cheque_Miti, PartyName, PartyLedger_Id, Party_PanNo, CmpUnit_ID, FiscalYearId, DoctorId, PatientId, HDepartmentId, Branch_ID, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            Select 'LOB' Module, Serial_No, Voucher_No, Ledger_ID,LOB.OP_Date Voucher_Date,LOB.OP_Miti Voucher_Miti,LOB.Enter_Date Voucher_Time,Ledger_ID CbLedger_ID,LOB.Subledger_Id Subleder_ID,LOB.Agent_Id Agent_ID,LOB.Cls1 Department_ID1,LOB.Cls2 Department_ID2,LOB.Cls3 Department_ID3,LOB.Cls4 Department_ID4,LOB.Currency_Id Currency_ID,LOB.Currency_Rate Currency_Rate,LOB.Debit Debit_Amt,LOB.Credit Credit_Amt,LOB.LocalDebit LocalDebit_Amt,LOB.LocalCredit LocalCredit_Amt,NULL DueDate,0 DueDays, Narration, Remarks,NULL RefNo,NULL RefDate, Reconcile_By, Reconcile_Date,NULL Authorize_By,NULL Authorize_Date,NULL Clearing_By,NULL Clearing_Date,NULL Posted_By,NULL Posted_Date,NULL Cheque_No,NULL Cheque_Date,NULL Cheque_Miti,NULL PartyName,NULL PartyLedger_Id,NULL Party_PanNo,LOB.Company_Id CmpUnit_ID, FiscalYearId,NULL DoctorId,NULL PatientId,NULL HDepartmentId, Branch_ID,LOB.Enter_By EnterBy,LOB.Enter_Date EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM AMS.LedgerOpening as LOB ";
        query += GetOpening.Voucher_No.IsValueExits() ? $@"where LOB.Voucher_No = '{GetOpening.Voucher_No}'" : "";
        var exe = SqlExtensions.ExecuteNonQuery(query);
        return exe;
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

    //PULL LEDGER OPENING
    public async Task<bool> PullAccountGroupsServerToClientByRowCounts(IDataSyncRepository<LedgerOpening> ledgerOpeningRepo, int callCount)
    {
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item2 == null)
            {
                return false;
            }

            _injectData.ApiConfig = new SyncApiConfig
            {
                GetUrl = @$"{_configParams.Model.Item2}LedgerOpening/GetLedgerOpeningsByCallCount?callCount={callCount}"
            };
            var pullResponse = await ledgerOpeningRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }
            else
            {
                var query = GetLedgerOpeningScript();
                var dataSetSql = SqlExtensions.ExecuteDataSetSql(query);

                foreach (var ledgerOpeningData in pullResponse.List)
                {
                    GetOpening = ledgerOpeningData;

                    var alreadyExistData = dataSetSql.Select(("Opening_Id = '" + ledgerOpeningData.Opening_Id + "'"));
                    if (alreadyExistData.Length > 0)
                    {
                        //get SyncRowVersion from client database table
                        var versionId = alreadyExistData[0]["SyncRowVersion"].GetInt();

                        //update only server SyncRowVersion is greater than client database while data pulling from server
                        if (ledgerOpeningData.SyncRowVersion > versionId)
                        {
                            var result = SaveLedgerOpening("UPDATE");
                        }
                    }
                    else
                    {
                        var result = SaveLedgerOpening("SAVE");
                    }
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullAccountGroupsServerToClientByRowCounts(ledgerOpeningRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    // RETURN VALUE IN DATA TABLE
    public DataSet ReturnOpeningLedgerVoucherInDataSet(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
		    SELECT Distinct VM.Opening_Id ,VM.Voucher_No,CONVERT(varchar(10),OP_Date,103)Voucher_Date,OP_Miti Voucher_Miti, Currency_Id,Cur.Ccode Ccode,CName,Currency_Rate,VM.Cls1,D1.DName Department1,Remarks FROM AMS.LedgerOpening as VM  Left Outer Join AMS.Currency as CUR ON Cur.CId=VM.Currency_Id Left Outer Join AMS.Department as D1 On D1.DId = VM.Cls1 Where VM.Voucher_No='{voucherNo}';
		    SELECT VM.Voucher_No,GL.GLID,GL.GLCode,GLName,VM.Subledger_Id,SL.SLName,SL.SLCode,VM.Agent_ID,AG.AgentName,AG.AgentCode,VM.Cls1,D1.DName Department1,VM.Cls2,D2.DName Department2, VM.Cls3,D3.DName Department3 ,VM.Cls4,D4.DName Department4,VM.Currency_Id,c.Ccode,VM.Currency_Rate,Debit,Credit,LocalDebit,LocalCredit,Narration FROM AMS.LedgerOpening as VM Inner Join AMS.GeneralLedger as GL On GL.GLID=VM.Ledger_Id Left Outer Join AMS.SubLedger as SL On SL.SLId=VM.Subledger_Id Left Outer Join AMS.JuniorAgent as AG ON AG.AgentId=VM.Agent_ID Left Outer Join AMS.Department as D1 On D1.DId=VM.Cls1 Left Outer Join AMS.Department as D2 On D2.DId=VM.Cls2 Left Outer Join AMS.Department as D3 On D3.DId=VM.Cls3 Left Outer Join AMS.Department as D4 On D4.DId=VM.Cls4 LEFT OUTER JOIN AMS.Currency c ON VM.Currency_Id = C.CId Where VM.Voucher_No='{voucherNo}' Order by Serial_No ; ");
        return SqlExtensions.ExecuteDataSet(
            cmdString.ToString());
    }
    public string GetLedgerOpeningScript(int openingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.LedgerOpening ";
        cmdString += openingId > 0 ? $" WHERE SyncGlobalId IS NULL AND Opening_Id= {openingId} " : "";
        return cmdString;
    }


    // OBJECT FOR THIS FORM
    public LedgerOpening GetOpening { get; set; } = new();
    public List<LedgerOpening> Details { get; set; } = new();
    private readonly DbSyncRepoInjectData _injectData = new();
    private IMasterSetup _master = new ClsMasterSetup();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
}