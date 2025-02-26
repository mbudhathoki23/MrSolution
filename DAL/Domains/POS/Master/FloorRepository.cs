using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Interface;
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
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Domains.POS.Master;

public class FloorRepository : IFloorRepository
{
    public FloorRepository()
    {
        ObjFloor = new FloorSetup();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    //INSERT UPDATE DELETE 
    public int SaveSetupFloor(string actionTag)
    {
        var cmdstring = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdstring.Append(
                "INSERT INTO AMS.Floor(FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_ID, Company_Id, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdstring.Append("\n VALUES \n");
            cmdstring.Append($"({ObjFloor.FloorId}, N'{ObjFloor.Description}', N'{ObjFloor.ShortName}', '{ObjFloor.Type}',");
            cmdstring.Append($"'{ObjFloor.EnterBy}','{ObjFloor.EnterDate.GetSystemDate()}',");
            cmdstring.Append(ObjFloor.Branch_ID > 0 ? $"{ObjFloor.Branch_ID}, " : "0,");
            cmdstring.Append(ObjFloor.Company_Id > 0 ? $"{ObjFloor.Company_Id}, " : "NULL,");
            cmdstring.Append(ObjFloor.Status ? $"1," : "0,");
            cmdstring.Append(ObjGlobal.IsOnlineSync ? "NEWID(), " : "NULL, ");
            cmdstring.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdstring.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,");
            cmdstring.Append($"'{ObjFloor.SyncCreatedOn.GetSystemDate()}', '{ObjFloor.SyncLastPatchedOn.GetSystemDate()}', {ObjFloor.SyncRowVersion}); \n");

        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdstring.Append("UPDATE AMS.Floor SET ");
            cmdstring.Append(!string.IsNullOrEmpty(ObjFloor.Description)
                ? $"Description = N'{ObjFloor.Description}', " : "Description = NULL,");
            cmdstring.Append(!string.IsNullOrEmpty(ObjFloor.ShortName)
                ? $"ShortName = N'{ObjFloor.ShortName}',"
                : "ShortName  = NULL,");
            cmdstring.Append(ObjFloor.Status ? "Status= 1," : "Status= 0,");
            cmdstring.Append("SyncLastPatchedOn = GETDATE(),");
            cmdstring.Append($"SyncRowVersion = {ObjFloor.SyncRowVersion}");
            cmdstring.Append($" WHERE FloorId = {ObjFloor.FloorId} ");

        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdstring.Append($"Delete from AMS.Floor Where FloorId = {ObjFloor.FloorId}");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdstring.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncFloorAsync(actionTag));
        }

        return exe;
    }

    public async Task<int> SyncFloorAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Floor/GetFloorsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Floor/InsertFloorList",
            UpdateUrl = @$"{_configParams.Model.Item2}Floor/UpdateFloor",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var floorRepo = DataSyncProviderFactory.GetRepository<FloorSetup>(_injectData);
        var counters = new List<FloorSetup>
        {
            ObjFloor
        };
        // push realtime counter details to server
        await floorRepo.PushNewListAsync(counters);

        // update counter SyncGlobalId to local
        if (floorRepo.GetHashCode() > 0)
        {
            // ReSharper disable once UseConfigureAwaitFalse
            await SyncUpdateFloor(ObjFloor.FloorId);
        }

        return floorRepo.GetHashCode();
    }

    public Task<int> SyncUpdateFloor(int floorId)
    {
        var commandText = $@"
            UPDATE AMS.Floor SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (floorId > 0)
        {
            commandText += $" WHERE FloorId = '{floorId}'";
        }

        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public string GetFloorScript(int floorId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Floor f";
        cmdString += floorId > 0 ? $" WHERE f.SyncGlobalId IS NULL AND f.FloorId= {floorId} " : "";
        return cmdString;
    }

    public async Task<bool> PullFloorServerToClientByRowCounts(
        IDataSyncRepository<FloorSetup> floorRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}Floor/GetFloorsByCallCount?callCount={callCount}";
        var pullResponse = await floorRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetFloorScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var floorData in pullResponse.List)
            {
                ObjFloor = floorData;

                var alreadyExistData = alldata.Select("FloorId='" + floorData.FloorId + "'");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (floorData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveSetupFloor("UPDATE");
                    }
                }
                else
                {
                    var result = SaveSetupFloor("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullFloorServerToClientByRowCounts(floorRepo, callCount);
        }

        return true;
    }



    // OBJECT FOR THIS FORM
    public FloorSetup ObjFloor { get; set; }
    public DbSyncRepoInjectData _injectData;
    public IMasterSetup _master;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
    private Microsoft.Office.Interop.Excel.Floor _objFloor;
}