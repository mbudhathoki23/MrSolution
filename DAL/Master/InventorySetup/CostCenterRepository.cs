using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.InventorySetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.InventorySetup;

public class CostCenterRepository : ICostCenterRepository
{
    public CostCenterRepository()
    {
        ObjCostCenter = new CostCenter();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
    }

    // INSERT UPDATE DELETE
    public int SaveCostCenter(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "DELETE")
        {
            SaveCostCenterAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.CostCenter where CCId = {ObjCostCenter.CCId}");
        }

        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.CostCenter (CCId, CCName, CCCode,GodownId,Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($"Values({ObjCostCenter.CCId},N'{ObjCostCenter.CCName}',N'{ObjCostCenter.CCcode}', ");
            cmdString.Append(ObjCostCenter.GodownId > 0 ? $" {ObjCostCenter.GodownId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append($"1,'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"GETDATE(),{ObjCostCenter.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID());" : "NULL); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.CostCenter SET \n");
            cmdString.Append($"CCName = N'{ObjCostCenter.CCName}',");
            cmdString.Append($"CCcode =N'{ObjCostCenter.CCcode}',");
            cmdString.Append(ObjCostCenter.GodownId > 0 ? $"GodownId ={ObjCostCenter.GodownId}," : "GodownId=NULL,");
            cmdString.Append(ObjCostCenter.Status is true ? "Status = 1, " : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjCostCenter.SyncRowVersion}");
            cmdString.Append($" WHERE CCId = {ObjCostCenter.CCId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncCostCenterAsync(actionTag));
            }
        }

        return exe;
    }

    public async Task<int> SyncCostCenterAsync(string actionTag)
    {
        //sync
        try
        {
            _configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item1 == null)
            {
                return 1;
            }

            _injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = _configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUri = @$"{_configParams.Model.Item2}CostCenter/DeleteCostCenterAsync?id=" + ObjCostCenter.CCId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}CostCenter/GetCostCenterById",
                InsertUrl = @$"{_configParams.Model.Item2}CostCenter/InsertCostCenter",
                UpdateUrl = @$"{_configParams.Model.Item2}CostCenter/UpdateCostCenter",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var costCenterRepo =
                DataSyncProviderFactory.GetRepository<CostCenter>(DataSyncManager.GetGlobalInjectData());

            var cm = new CostCenter();
            cm.CCId = ObjCostCenter.CCId;
            cm.CCName = ObjCostCenter.CCName;
            cm.CCcode = ObjCostCenter.CCcode;
            cm.GodownId = ObjCostCenter.GodownId > 0 ? ObjCostCenter.GodownId : null;
            cm.Branch_ID = ObjGlobal.SysBranchId;
            cm.Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            cm.Status = true;
            cm.EnterBy = ObjGlobal.LogInUser;
            cm.EnterDate = DateTime.Now;
            cm.SyncRowVersion = ObjCostCenter.SyncRowVersion == null ? (short)1 : ObjCostCenter.SyncRowVersion;

            var result = actionTag switch
            {
                "SAVE" => await costCenterRepo?.PushNewAsync(cm),
                "UPDATE" => await costCenterRepo?.PutNewAsync(cm),
                "DELETE" => await costCenterRepo?.DeleteNewAsync(),
                _ => await costCenterRepo?.PushNewAsync(cm)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int SaveCostCenterAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_COSTCENTER(CCId, CCName, CCcode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT CCId, CCName, CCcode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate 
            FROM AMS.CostCenter
            WHERE CCId='{ObjCostCenter.CCId}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    public async Task<bool> PullCostCentreServerToClientByRowCounts(IDataSyncRepository<CostCenter> costCRepository, int callCount)
    {
        try
        {
            var pullResponse = await costCRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetCostCentreScript();
            var dataSetSql = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var centre in pullResponse.List)
            {
                ObjCostCenter = centre;

                var existData = dataSetSql.Select($"CCId= {centre.CCId}");
                if (existData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    short rowVersionId = existData[0]["SyncRowVersion"].GetShort();

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (centre.SyncRowVersion > rowVersionId)
                    {
                        ObjCostCenter = centre;
                        var result = SaveCostCenter("UPDATE");
                    }
                }
                else
                {
                    var result = SaveCostCenter("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullCostCentreServerToClientByRowCounts(costCRepository, callCount);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterCostCenter(string actionTag, int status = 0, int selectedId = 0)
    {
        var cmdString = $@"
			SELECT cc.*,g.GName
			FROM AMS.CostCenter cc
			LEFT OUTER JOIN AMS.Godown g ON g.GID = cc.GodownId
			WHERE CCId={selectedId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public string GetCostCentreScript(int costCentreId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.CostCenter c ";
        cmdString += costCentreId > 0 ? $" WHERE c.SyncGlobalId IS NULL AND c.CCId= {costCentreId} " : "";
        return cmdString;
    }

    // OBJECT FOR THIS FORM
    public CostCenter ObjCostCenter { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}