using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.ProductSetup;

public class WareHouseRepository : IWareHouseRepository
{
    public WareHouseRepository()
    {
        ObjGodown = new Godown();
    }

    // INSERT UPDATE DELETE
    public int SaveGodown(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag == "SAVE")
        {
            cmdString.Append(@"
            INSERT INTO AMS.Godown (GID, GName, GCode, GLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($"VALUES({ObjGodown.GID}, ");
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
            var deleteUri = @$"{configParams.Model.Item2}Godown/DeleteGodownAsync?id=" + ObjGodown.GID;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}Godown/GetGodownById",
                InsertUrl = @$"{configParams.Model.Item2}Godown/InsertGodown",
                UpdateUrl = @$"{configParams.Model.Item2}Godown/UpdateGodown",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var godownRepo = DataSyncProviderFactory.GetRepository<Godown>(DataSyncManager.GetGlobalInjectData());

            var gd = new Godown
            {
                GID = ObjGodown.GID,
                GName = string.IsNullOrEmpty(ObjGodown.GName) ? null : ObjGodown.GName,
                GCode = string.IsNullOrEmpty(ObjGodown.GCode) ? null : ObjGodown.GCode,
                GLocation = string.IsNullOrEmpty(ObjGodown.GLocation) ? null : ObjGodown.GLocation,
                Status = ObjGodown.Status,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                CompUnit = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                BranchUnit = ObjGlobal.SysBranchId,
                SyncRowVersion = ObjGodown.SyncRowVersion == null ? (short)1 : ObjGodown.SyncRowVersion
            };

            var result = actionTag switch
            {
                "SAVE" => await godownRepo?.PushNewAsync(gd),
                "UPDATE" => await godownRepo?.PutNewAsync(gd),
                "DELETE" => await godownRepo?.DeleteNewAsync(),
                _ => await godownRepo?.PushNewAsync(gd)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    public int SaveGodownAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_GODOWN(GID, GName, GCode, GLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, ModifyAction, ModifyBy, ModifyDate)
            SELECT GID, GName, GCode, GLocation, Status, EnterBy, EnterDate, CompUnit, BranchUnit, '' ModifyAction, '' ModifyBy, GETDATE() ModifyDate 
            FROM AMS.Godown
            WHERE GID=''";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterGoDown(string actionTag, int status = 0, int selectedId = 0)
    {
        if (selectedId == 0)
        {
            var cmdString = @"SELECT GID,GName,GCode,GLocation FROM AMS.Godown";
            return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }
        else
        {
            var cmdString = $@"SELECT GID,GName,GCode,GLocation FROM AMS.Godown WHERE GID = '{selectedId}'";
            return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }
    }

    // OBJECT FOR THIS FORM
    public Godown ObjGodown { get; set; }
}