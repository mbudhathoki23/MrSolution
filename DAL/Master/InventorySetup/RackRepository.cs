using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.InventorySetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.InventorySetup;

public class RackRepository : IRackRepository
{
    public RackRepository()
    {
        ObjRack = new RACK();

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public int SaveRack(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append("INSERT INTO AMS.RACK (RID, RName, RCode, Location, Status, EnterBy, EnterDate, CompanyUnitId, BranchId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($"Values({ObjRack.RID}, ");
            cmdString.Append(!string.IsNullOrEmpty(ObjRack.RName) ? $"N'{ObjRack.RName}'," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjRack.RCode) ? $"N'{ObjRack.RCode}'," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjRack.Location) ? $"N'{ObjRack.Location}'," : "NULL,");
            cmdString.Append(ObjRack.Status is true ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,"
                : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjRack.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID() );" : "NULL );");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.Rack SET ");
            cmdString.Append(!string.IsNullOrEmpty(ObjRack.RName)
                ? $"RName = N'{ObjRack.RName}',"
                : "RName = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjRack.RCode)
                ? $"RCode = N'{ObjRack.RCode}',"
                : "RCode = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjRack.Location)
                ? $"Location = N'{ObjRack.Location}',"
                : "Location = NULL,");
            cmdString.Append(ObjRack.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjRack.SyncRowVersion}");
            cmdString.Append($" WHERE RID = {ObjRack.RID}; ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            SaveRackAuditLog(actionTag);
            cmdString.Append($"DELETE FROM AMS.Rack WHERE RID = {ObjRack.RID}");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncRackAsync(actionTag));
            }
        }

        return exe;
    }

    public async Task<bool> SyncRackDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}Rack/GetRacksByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Rack/InsertRackList",
            UpdateUrl = @$"{_configParams.Model.Item2}Rack/UpdateRack"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var rackRepo = DataSyncProviderFactory.GetRepository<RACK>(_injectData);

        // pull all new rack data

        var pullResponse = await PullRacksServerToClientByRowCount(rackRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new rack data
        var sqlQuery = GetRackScript();
        var queryResponse = await QueryUtils.GetListAsync<RACK>(sqlQuery);
        var raList = queryResponse.List.ToList();
        if (raList.Count > 0)
        {
            var pushResponse = await rackRepo.PushNewListAsync(raList);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public async Task<int> SyncRackAsync(string actionTag)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}Rack/GetRacksByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Rack/InsertRackList",
            UpdateUrl = @$"{_configParams.Model.Item2}Rack/UpdateRack"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var rackRepo = DataSyncProviderFactory.GetRepository<RACK>(_injectData);
        var racks = new List<RACK>
        {
            ObjRack
        };

        // push realtime details to server
        await rackRepo.PushNewListAsync(racks);

        // update main area SyncGlobalId to local
        if (rackRepo.GetHashCode() > 0)
        {
            await SyncUpdateRack(ObjRack.RID);
        }
        return rackRepo.GetHashCode();
    }
    public int SaveRackAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_RACK(RID, RName, RCode, RLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, ModifyAction, ModifyBY, ModifyDate)
            SELECT RID, RName, RCode,'' RLocation, Status, EnterBy, EnterDate,'' CompUnit,'' BranchUnit,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBY,GETDATE() ModifyDate 
            FROM AMS.RACK
            WHERE RID='{ObjRack.RID}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    public Task<int> SyncUpdateRack(long rackId = 0)
    {
        var commandText = $@"
            UPDATE AMS.RACK SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (rackId > 0)
        {
            commandText += $" WHERE RID = '{rackId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }


    // RETURN DATA IN DATA TABLE
    public DataTable GetRackList(string actionTag, int status = 0, int selectedId = 0)
    {
        var cmdString = string.Empty;
        if (selectedId == 0)
        {
            cmdString = "SELECT RID, RName, RCode, Location from AMS.RACK where 1=1 ";
            if (status > 0)
            {
                cmdString += "AND[Status] = 1 OR[Status] IS NULL";
            }
        }
        else
        {
            cmdString = $"SELECT RID, RName, RCode, Location from AMS.RACK where RID  = '{selectedId}'";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public async Task<bool> PullRacksServerToClientByRowCount(IDataSyncRepository<RACK> rackRepo, int callCount)
    {
        try
        {
            var pullResponse = await rackRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetRackScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var rackData in pullResponse.List)
            {
                ObjRack = rackData;

                var alreadyExistData = alldata.Select("RID= '" + rackData.RID + "'");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (rackData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveRack("UPDATE");
                    }
                }
                else
                {
                    var result = SaveRack("SAVE");
                }

            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullRacksServerToClientByRowCount(rackRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public string GetRackScript(int rackId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.RACK";
        cmdString += rackId > 0 ? $" WHERE SyncGlobalId IS NULL AND RID= {rackId} " : "";
        return cmdString;
    }


    // OBJECT FOR THIS FORM
    public RACK ObjRack { get; set; }
    private IMasterSetup _master;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

}