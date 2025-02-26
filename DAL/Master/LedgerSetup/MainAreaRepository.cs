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

public class MainAreaRepository : IMainAreaRepository
{
    public MainAreaRepository()
    {
        ObjMainArea = new MainArea();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    public int SaveMainArea(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() is "DELETE")
        {
            SaveMainAreaAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.Area where Main_Area = {ObjMainArea.MAreaId}");
            cmdString.Append($"Delete from AMS.MainArea where MAreaId = {ObjMainArea.MAreaId}");
        }

        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.MainArea(MAreaId, NepaliDesc, MAreaName, MAreaCode, MCountry, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append($"\n VALUES \n");
            cmdString.Append($" ({ObjMainArea.MAreaId},N'{ObjMainArea.NepaliDesc.GetTrimReplace()}',N'{ObjMainArea.MAreaName.GetTrimReplace()}',N'{ObjMainArea.MAreaCode.GetTrimReplace()}',");
            cmdString.Append(ObjMainArea.MCountry.IsValueExits()
                ? $" N'{ObjMainArea.MCountry.Replace("'", "''")}',"
                : "NULL,");
            cmdString.Append(ObjMainArea.Branch_ID > 0 ? $" {ObjMainArea.Branch_ID}," : $"{ObjGlobal.SysBranchId},");
            cmdString.Append(ObjMainArea.Company_Id > 0 ? $" {ObjMainArea.Company_Id}," : $"NULL,");
            cmdString.Append(ObjMainArea.Status is true ? " 1," : "0,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? " NEWID()," : " NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? " NEWID()," : " NULL,");
            cmdString.Append($"GETDATE(),GETDATE(), {ObjMainArea.SyncRowVersion} ); \n");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.MainArea SET ");
            cmdString.Append($" MAreaName = N'{ObjMainArea.MAreaName.GetTrimReplace()}',");
            cmdString.Append($" MAreaCode = N'{ObjMainArea.MAreaCode.GetTrimReplace()}',");
            cmdString.Append(ObjMainArea.MCountry.IsValueExits()
                ? $" MCountry = N'{ObjMainArea.MCountry.GetTrimReplace()}',"
                : " MCountry = NULL,");
            cmdString.Append(ObjMainArea.Status is true ? " Status = 1," : " Status = 0,");
            cmdString.Append(" SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($" SyncRowVersion = {ObjMainArea.SyncRowVersion}");
            cmdString.Append($" WHERE MAreaId = {ObjMainArea.MAreaId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            try
            {
                if (ObjGlobal.IsOnlineSync)
                {
                    Task.Run(() => SyncMainAreaAsync(actionTag));
                }
            }
            catch (Exception ex)
            {
                var errmg = ex.Message;
            }

            SaveMainAreaAuditLog(actionTag);
        }

        return exe;
    }
    public async Task<int> SyncMainAreaAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}MainArea/GetMainAreasByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}MainArea/InsertMainAreaList",
            UpdateUrl = @$"{_configParams.Model.Item2}MainArea/UpdateMainArea",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var mainAreaRepo = DataSyncProviderFactory.GetRepository<MainArea>(_injectData);
        var mainAreas = new List<MainArea>
        {
            ObjMainArea
        };

        // push realtime main area details to server
        await mainAreaRepo.PushNewListAsync(mainAreas);

        // update main area SyncGlobalId to local
        if (mainAreaRepo.GetHashCode() > 0)
        {
            await SyncUpdateMainArea(ObjMainArea.MAreaId);
        }
        return mainAreaRepo.GetHashCode();
    }

    public async Task<bool> SyncMainAreaDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}MainArea/GetMainAreasByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}MainArea/InsertMainAreaList",
            UpdateUrl = @$"{_configParams.Model.Item2}MainArea/UpdateMainArea",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var mainAreaRepo = DataSyncProviderFactory.GetRepository<MainArea>(_injectData);

        // pull all new account data
        var pullResponse = await PullMainAreasServerToClientByRowCount(mainAreaRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }


        // push all new account data
        var sqlSaQuery = GetMainAreaScript();
        var queryResponse = await QueryUtils.GetListAsync<MainArea>(sqlSaQuery);
        var maList = queryResponse.List.ToList();
        if (maList.Count > 0)
        {
            var pushResponse = await mainAreaRepo.PushNewListAsync(maList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public int SaveMainAreaAuditLog(string actionTag)
    {
        var cmdString = $@"
			INSERT INTO AUD.AUDIT_MAINAREA(MAreaId, MAreaName, MAreaCode, MCountry, Branch_ID, Company_ID, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
			SELECT MAreaId, MAreaName, MAreaCode, MCountry, Branch_ID, Company_ID, Status, EnterBy, EnterDate,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate FROM AMS.MainArea
			WHERE MAreaId = '{ObjMainArea.MAreaId}';";
        var result = SqlExtensions.ExecuteNonTrans(cmdString);
        return result;
    }
    public Task<int> SyncUpdateMainArea(int mainAreaId = 0)
    {
        var commandText = $@"
            UPDATE AMS.MainArea SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (mainAreaId > 0)
        {
            commandText += $" WHERE MAreaId = '{mainAreaId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    // PULL MAIN AREA
    #region ---------- PULL MAIN AREA ----------

    public async Task<bool> PullMainAreasServerToClientByRowCount(IDataSyncRepository<MainArea> mainAreaRepo, int callCount)
    {
        try
        {
            var pullResponse = await mainAreaRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetMainAreaScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var mainAreaData in pullResponse.List)
            {
                ObjMainArea = mainAreaData;

                var alreadyExistData = alldata.Select("MAreaId= " + mainAreaData.MAreaId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (mainAreaData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveMainArea("UPDATE");
                    }
                }
                else
                {
                    var result = SaveMainArea("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMainAreasServerToClientByRowCount(mainAreaRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public string GetMainAreaScript(int mainAreaId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.MainArea";
        cmdString += mainAreaId > 0 ? $" WHERE SyncGlobalId IS NULL AND MAreaId= {mainAreaId} " : "";
        return cmdString;
    }
    #endregion
    //RETURN VALUE IN DATA TABLE
    public DataTable GetMasterMainArea(string actionTag, bool status, int selectedId = 0)
    {
        var cmdString =
            $@"SELECT MAreaId,MAreaName,MAreaCode,MCountry,Status from AMS.MainArea where MAreaId = '{selectedId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    //OBJECT FOR THIS FORM
    public MainArea ObjMainArea { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}