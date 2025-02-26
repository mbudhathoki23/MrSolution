using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.ProductSetup;

public class GodownRepository : IGodownRepository
{
    public GodownRepository()
    {
        ObjGodown = new Godown();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public int SaveGodown(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag == "SAVE")
        {
            cmdString.Append(@"
                INSERT INTO AMS.Godown (GID, GName, GCode, GLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
            cmdString.Append($" \n VALUES({ObjGodown.GID}, ");
            cmdString.Append(!string.IsNullOrEmpty(ObjGodown.GName) ? $"N'{ObjGodown.GName}'," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjGodown.GCode) ? $"N'{ObjGodown.GCode}'," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjGodown.GLocation) ? $"N'{ObjGodown.GLocation}'," : "NULL,");
            cmdString.Append(ObjGodown.Status is true ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,"
                : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjGodown.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID())" : "NULL)");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.Godown SET ");
            cmdString.Append(!string.IsNullOrEmpty(ObjGodown.GName) ? $"GName = N'{ObjGodown.GName}'," : "GName = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjGodown.GCode) ? $"GCode = N'{ObjGodown.GCode}'," : "GCode = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjGodown.GLocation) ? $"GLocation = N'{ObjGodown.GLocation}'," : "GLocation =NULL,");
            cmdString.Append(ObjGodown.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjGodown.SyncRowVersion}");
            cmdString.Append($" WHERE GID = {ObjGodown.GID}; ");
        }
        else if (actionTag == "DELETE")
        {
            SaveGodownAuditLog(actionTag);
            cmdString.Append($" Delete from AMS.Godown where GID = {ObjGodown.GID}");
        }

        var execute = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (execute <= 0)
        {
            return execute;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncGodownAsync(actionTag));
        }

        return execute;
    }
    public async Task<int> SyncGodownAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Godown/GetGoDownsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Godown/InsertGodownList",
            UpdateUrl = @$"{_configParams.Model.Item2}Godown/UpdateGodown"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var goDownRepo = DataSyncProviderFactory.GetRepository<Godown>(_injectData);
        var goDowns = new List<Godown>
        {
            ObjGodown
        };
        // push realtime godown details to server
        await goDownRepo.PushNewListAsync(goDowns);

        // update godown SyncGlobalId to local
        if (goDownRepo.GetHashCode() > 0)
        {
            await SyncUpdateGodown(ObjGodown.GID);
        }

        return goDownRepo.GetHashCode();
    }
    public Task<int> SyncUpdateGodown(int gId = 0)
    {
        var commandText = $@"
            UPDATE AMS.Godown SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (gId > 0)
        {
            commandText += $" WHERE GID = '{gId}'";
        }

        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public int SaveGodownAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_GODOWN(GID, GName, GCode, GLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, ModifyAction, ModifyBy, ModifyDate)
            SELECT GID, GName, GCode, GLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate 
            FROM AMS.Godown
            WHERE GID='{ObjGodown.GID}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    //PULL GODOWN
    public async Task<bool> GetAndSaveUnSynchronizedGodown()
    {
        try
        {
            var godownList = await _master.GetUnSynchronizedData("AMS.Godown");
            if (godownList.List != null)
            {
                foreach (var data in godownList.List)
                {
                    var godownData = JsonConvert.DeserializeObject<Godown>(data.JsonData);
                    var actionTag = data.Action;
                    ObjGodown.GID = godownData.GID;
                    ObjGodown.GName = godownData.GName;
                    ObjGodown.GCode = godownData.GCode;
                    ObjGodown.GLocation = godownData.GLocation;
                    ObjGodown.Status = godownData.Status;
                    ObjGodown.EnterBy = godownData.EnterBy;
                    ObjGodown.EnterDate = godownData.EnterDate;
                    ObjGodown.BranchUnit = godownData.BranchUnit;
                    ObjGodown.CompUnit = godownData.CompUnit;
                    ObjGodown.SyncRowVersion = godownData.SyncRowVersion;
                    var result = SaveGodown(actionTag);
                    if (result > 0)
                    {
                        //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                        //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                        //actionTag = "SAVE";
                        //var response = await _master.SaveSyncLogDetails(actionTag);
                    }
                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    public async Task<bool> PullGoDownsServerToClientByRowCounts(IDataSyncRepository<Godown> goDownRepo, int callCount)
    {
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item2 == null)
            {
                return false;
            }

            _injectData.ApiConfig = new SyncApiConfig();
            _injectData.ApiConfig.GetUrl =
                @$"{_configParams.Model.Item2}Godown/GetGoDownsByCallCount?callCount=" + callCount;
            var pullResponse = await goDownRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }
            else
            {
                var actionTag = "UPDATE";
                var query = GetGodownScript();
                var alldata = SqlExtensions.ExecuteDataSetSql(query);

                foreach (var godownData in pullResponse.List)
                {
                    ObjGodown.GID = godownData.GID;
                    ObjGodown.NepaliDesc = godownData.NepaliDesc;
                    ObjGodown.GName = godownData.GName;
                    ObjGodown.GCode = godownData.GCode;
                    ObjGodown.GLocation = godownData.GLocation;
                    ObjGodown.Status = godownData.Status;
                    ObjGodown.EnterBy = godownData.EnterBy;
                    ObjGodown.EnterDate = godownData.EnterDate;
                    ObjGodown.BranchUnit = godownData.BranchUnit;
                    ObjGodown.CompUnit = godownData.CompUnit;
                    ObjGodown.SyncBaseId = godownData.SyncBaseId;
                    ObjGodown.SyncGlobalId = godownData.SyncGlobalId;
                    ObjGodown.SyncOriginId = godownData.SyncOriginId;
                    ObjGodown.SyncCreatedOn = godownData.SyncCreatedOn;
                    ObjGodown.SyncLastPatchedOn = godownData.SyncLastPatchedOn;
                    ObjGodown.SyncRowVersion = godownData.SyncRowVersion;

                    var alreadyExistData = alldata.Select("GID='" + godownData.GID + "'");
                    if (alreadyExistData.Length > 0)
                    {
                        //get SyncRowVersion from client database table
                        int ClientSyncRowVersionId = 1;
                        ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                        //update only server SyncRowVersion is greater than client database while data pulling from server
                        if (godownData.SyncRowVersion > ClientSyncRowVersionId)
                        {
                            var result = SaveGodown(actionTag);
                        }
                    }
                    else
                    {
                        actionTag = "SAVE";
                        var result = SaveGodown(actionTag);

                    }
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullGoDownsServerToClientByRowCounts(goDownRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    public string GetGodownScript(int gId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Godown g";
        cmdString += gId > 0 ? $" WHERE g.SyncGlobalId IS NULL AND g.GID = {gId} " : "";
        return cmdString;
    }
    public DataTable GetMasterGoDown(string actionTag, int status = 0, int selectedId = 0)
    {
        var cmdString = $"SELECT  * FROM AMS.Godown WHERE GID= {selectedId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }




    // OBJECT FOR THIS FORM
    public Godown ObjGodown { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}