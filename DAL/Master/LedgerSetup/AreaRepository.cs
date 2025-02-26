using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.LedgerSetup;

public class AreaRepository : IAreaRepository
{
    public AreaRepository()
    {
        ObjArea = new Area();

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public string GetAreaScript(int areaId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Area";
        cmdString += areaId > 0 ? $" WHERE SyncGlobalId IS NULL AND AreaId= {areaId} " : "";
        return cmdString;
    }
    public int SaveArea(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "DELETE")
        {
            SaveAreaAuditLog(actionTag);
            cmdString.Append($"DELETE FROM  AMS.Area WHERE AreaId = {ObjArea.AreaId}; ");
        }

        if (actionTag == "SAVE")
        {
            cmdString.Append(
                " INSERT INTO AMS.Area(AreaId, NepaliDesc, AreaName, AreaCode, Country, Branch_ID, Company_Id, Main_Area, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append(
                $"Values({ObjArea.AreaId},N'{ObjArea.NepaliDesc.GetTrimReplace()}',N'{ObjArea.AreaName.GetTrimReplace()}','{ObjArea.AreaCode.GetTrimReplace()}', ");
            cmdString.Append(ObjArea.Country.IsValueExits() ? $"N'{ObjArea.Country.GetTrimReplace()}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjArea.MainArea.GetInt() > 0 ? $"{ObjArea.MainArea}," : "NULL,");
            cmdString.Append(ObjArea.Status.GetBool() ? "1," : "0,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID(),NEWID()," : "NULL,NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjArea.SyncRowVersion}); \n");
        }
        else
        if (actionTag == "UPDATE")
        {
            cmdString.Append("  UPDATE AMS.Area SET \n");
            cmdString.Append($" NepaliDesc = N'{ObjArea.NepaliDesc.GetTrimReplace()}',");
            cmdString.Append($" AreaName = N'{ObjArea.AreaName.GetTrimReplace()}',");
            cmdString.Append($" AreaCode = N'{ObjArea.AreaCode.GetTrimReplace()}',");
            cmdString.Append(ObjArea.Country.IsValueExits()
                ? $" Country = N'{ObjArea.Country.Replace("'", "''")}',"
                : "Country = NULL,");
            cmdString.Append(ObjArea.MainArea.GetInt() > 0
                ? $" Main_Area = {ObjArea.MainArea},"
                : "Main_Area = NULL,");
            cmdString.Append(ObjArea.Status.GetBool() ? " Status = 1," : "Status = 0,");
            cmdString.Append(" SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($" SyncRowVersion = {ObjArea.SyncRowVersion}");
            cmdString.Append($" WHERE AreaId = {ObjArea.AreaId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            try
            {
                if (ObjGlobal.IsOnlineSync)
                {
                    Task.Run(() => SyncAreaAsync(actionTag));
                }
            }
            catch (Exception ex)
            {
                var errMsg = ex.Message;
            }

            SaveAreaAuditLog(actionTag);
        }

        return exe;
    }
    public async Task<int> SyncAreaAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Area/GetAreasByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Area/InsertAreaList",
            UpdateUrl = @$"{_configParams.Model.Item2}Area/UpdateArea",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var areaRepo = DataSyncProviderFactory.GetRepository<Area>(_injectData);
        var areas = new List<Area>
        {
            ObjArea
        };
        // push realtime main area details to server
        await areaRepo.PushNewListAsync(areas);

        // update main area SyncGlobalId to local
        if (areaRepo.GetHashCode() > 0)
        {
            await SyncUpdateArea(ObjArea.AreaId);
        }
        return areaRepo.GetHashCode();
    }

    public async Task<bool> SyncAreaDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item1 == null)
        {
            return true;
        }
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}Area/GetAreasByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Area/InsertAreaList",
            UpdateUrl = @$"{_configParams.Model.Item2}Area/UpdateArea",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var areaRepo = DataSyncProviderFactory.GetRepository<Area>(_injectData);

        // pull all new area data
        var pullResponse = await PullAreaFromServerToClientDBByCallCount(areaRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new area data
        var sqlArQuery = GetAreaScript();
        var queryResponse = await QueryUtils.GetListAsync<Area>(sqlArQuery);
        var arList = queryResponse.List.ToList();
        if (arList.Count > 0)
        {
            var pushResponse = await areaRepo.PushNewListAsync(arList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public int SaveAreaAuditLog(string actionTag)
    {
        var cmdString = $@"
		    INSERT INTO AUD.AUDIT_Area(AreaId, AreaName, AreaCode, Country, Main_Area, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId, ModifyAction, ModifyBy, ModifyDate)
		    SELECT AreaId, AreaName, AreaCode, Country, Main_Area, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate FROM AMS.Area
		    WHERE AreaId = '{ObjArea.AreaId}';";
        var result = SqlExtensions.ExecuteNonTrans(cmdString);
        return result;
    }
    public Task<int> SyncUpdateArea(int areaId = 0)
    {
        var commandText = $@"
            UPDATE AMS.Area SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (areaId > 0)
        {
            commandText += $" WHERE AreaId = '{areaId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }


    // PULL AREA 
    #region ---------- PULL AREA ----------

    public async Task<bool> PullAreaFromServerToClientDBByCallCount(IDataSyncRepository<Area> areaRepo, int callCount)
    {
        try
        {
            var pullResponse = await areaRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = $@"select * from AMS.Area";
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var areaData in pullResponse.List)
            {
                ObjArea = areaData;

                var alreadyExistData = alldata.Select("AreaId= " + areaData.AreaId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (areaData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveArea("UPDATE");
                    }
                }
                else
                {
                    var result = SaveArea("SAVE");
                }

            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullAreaFromServerToClientDBByCallCount(areaRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterArea(string actionTag, int selectedId = 0)
    {
        var cmdString =
            $@"SELECT AreaId, AreaName, AreaCode, Main_Area, MA.MAreaName, Country FROM AMS.Area AR LEFT OUTER JOIN AMS.MainArea AS MA ON AR.Main_Area = MA.MAreaId WHERE AreaId = {selectedId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FROM 

    public Area ObjArea { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

}