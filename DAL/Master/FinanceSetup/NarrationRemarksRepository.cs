using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.FinanceSetup;

public class NarrationRemarksRepository : INarrationRemarksRepository
{
    public NarrationRemarksRepository()
    {
        Narration = new NR_Master();
    }

    // INSERT UPDATE DELETE
    public int SaveNarration(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.NR_Master where NRID = {Narration.NRID}; ");
        }

        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(@" 
            INSERT INTO AMS.NR_Master (NRDESC, NRTYPE, IsActive, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId, CompanyUnitId, BranchId) ");
            cmdString.Append($" \n VALUES ('{Narration.NRDESC.GetTrimReplace()}',N'{Narration.NRTYPE.GetTrimReplace()}',");
            cmdString.Append(Narration.IsActive is true ? "1," : "0,");
            cmdString.Append($"N'{ObjGlobal.LogInUser}', GETDATE(),NEWID(),");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,");
            cmdString.Append($"GETDATE(), GETDATE(), {Narration.SyncRowVersion} , NEWID() ,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}')" : "NULL");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.NR_Master SET ");
            cmdString.Append(!string.IsNullOrEmpty(Narration.NRDESC.Trim())
                ? $"NRDESC = N'{Narration.NRDESC.GetTrimReplace()}'," : "NRDESC = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(Narration.NRTYPE.GetTrimReplace())
                ? $"NRTYPE = N'{Narration.NRTYPE.GetTrimReplace()}'," : "NRTYPE = NULL,");
            cmdString.Append(Narration.IsActive is true ? " IsActive = 1," : " IsActive = 0,");
            cmdString.Append($"SyncRowVersion = {Narration.SyncRowVersion}");
            cmdString.Append($" WHERE NRID = {Narration.NRID}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncNarrationAsync(actionTag));
            }
        }

        return exe;
    }
    public async Task<int> SyncNarrationAsync(string actionTag)
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
            var deleteUri = @$"{configParams.Model.Item2}NrMaster/DeleteNrMasterAsync?id=" + Narration.NRID;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}NrMaster/GetNrMasterById",
                InsertUrl = @$"{configParams.Model.Item2}NrMaster/InsertNrMaster",
                UpdateUrl = @$"{configParams.Model.Item2}NrMaster/UpdateNrMaster",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);
            var nrMasterRepo = DataSyncProviderFactory.GetRepository<NR_Master>(DataSyncManager.GetGlobalInjectData());

            var nm = new NR_Master
            {
                NRDESC = string.IsNullOrEmpty(Narration.NRDESC.Trim()) ? null : Narration.NRDESC.Trim(),
                NRTYPE = string.IsNullOrEmpty(Narration.NRTYPE.Trim()) ? null : Narration.NRTYPE.Trim(),
                IsActive = Narration.IsActive,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                BranchId = ObjGlobal.SysBranchId,
                CompanyUnitId = ObjGlobal.SysCompanyUnitId,
                SyncRowVersion = Narration.SyncRowVersion == null ? (short)1 : Narration.SyncRowVersion
            };

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await nrMasterRepo?.PushNewAsync(nm),
                "UPDATE" => await nrMasterRepo?.PutNewAsync(nm),
                "DELETE" => await nrMasterRepo?.DeleteNewAsync(),
                _ => await nrMasterRepo?.PushNewAsync(nm)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterNarration(string actionTag, int selectedId = 0)
    {
        var cmdString = selectedId == 0
            ? "SELECT NRID,NRDESC, NRTYPE, IsActive FROM AMS.NR_Master n where  1=1  AND (IsActive = 1 or IsActive is Null) "
            : $"SELECT * FROM AMS.NR_Master n WHERE n.NRID= '{selectedId}' \n";

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public int SaveNarrationMasterAuditLog(string actionTag)
    {
        var cmdString = @$"
            INSERT INTO AUD.AUDIT_NR_MASTER(NRID, NRDESC, NRTYPE, IsActive, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId, CompanyUnitId, BranchId, ModifyAction, ModifyBy, ModifyDate)
            SELECT NRID, NRDESC, NRTYPE, IsActive, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId, CompanyUnitId, BranchId,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.NR_Master
            WHERE NRID='{Narration.NRID}'
             ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    public string GetNarrationMasterScript(int nrid = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.NR_Master";
        cmdString += nrid > 0 ? $" WHERE SyncGlobalId IS NULL AND NRID= {nrid} " : "";
        return cmdString;
    }

    // PULL NARRATION MASTER 
    #region ---------- NARRATION MASTER ----------

    public async Task<bool> PullNarrationMasterServerToClientByRowCount(IDataSyncRepository<NR_Master> narrationRepo, int callCount)
    {
        try
        {
            var pullResponse = await narrationRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetNarrationMasterScript();
            var allData = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var narration in pullResponse.List)
            {
                Narration = narration;

                var alreadyExistData = allData.Select("NRID= " + narration.NRID + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (narration.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveNarration("UPDATE");
                    }
                }
                else
                {
                    var result = SaveNarration("SAVE");
                }

            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullNarrationMasterServerToClientByRowCount(narrationRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion
    // OBJECT FOR THIS FORM
    public NR_Master Narration { get; set; }
}