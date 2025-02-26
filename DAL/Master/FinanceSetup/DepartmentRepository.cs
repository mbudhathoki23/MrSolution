using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ReSharper disable All

namespace MrDAL.Master.FinanceSetup;

public class DepartmentRepository : IDepartmentRepository
{
    public DepartmentRepository()
    {
        ObjDepartment = new Department();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE

    public int SaveDepartment(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.Department (DId, DName, DCode, Dlevel, Branch_ID, Company_ID, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) \n");
            cmdString.Append($"Values({ObjDepartment.DId}, ");
            cmdString.Append(!string.IsNullOrEmpty(ObjDepartment.DName)
                ? $"N'{ObjDepartment.DName}',"
                : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDepartment.DCode)
                ? $"N'{ObjDepartment.DCode}',"
                : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDepartment.Dlevel)
                ? $"N'{ObjDepartment.Dlevel}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjDepartment.Status ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,"
                : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjDepartment.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID())" : "NULL)");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.Department SET ");
            cmdString.Append(!string.IsNullOrEmpty(ObjDepartment.DName)
                ? $"DName = N'{ObjDepartment.DName}',"
                : "DName = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDepartment.DCode)
                ? $"DCode = N'{ObjDepartment.DCode}',"
                : "DCode = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjDepartment.Dlevel)
                ? $"Dlevel = N'{ObjDepartment.Dlevel}',"
                : "Dlevel = NULL,");
            cmdString.Append(ObjDepartment.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjDepartment.SyncRowVersion}");
            cmdString.Append($" WHERE DId = {ObjDepartment.DId}; ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.Department where Did = {ObjDepartment.DId}");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncDepartmentAsync(actionTag));
        }
        return exe;
    }

    public async Task<int> SyncDepartmentAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Department/GetDepartmentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Department/InsertDepartmentList",
            UpdateUrl = @$"{_configParams.Model.Item2}Department/UpdateDepartment",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var departmentRepo = DataSyncProviderFactory.GetRepository<Department>(_injectData);
        var departments = new List<Department>
        {
            ObjDepartment
        };
        // push realtime department details to server
        await departmentRepo.PushNewListAsync(departments);

        // update department SyncGlobalId to local
        if (departmentRepo.GetHashCode() > 0)
        {
            // ReSharper disable once UseConfigureAwaitFalse
            await SyncUpdateDepartment(ObjDepartment.DId);
        }

        return departmentRepo.GetHashCode();

    }

    public async Task<bool> SyncDepartmentDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}Department/GetDepartmentById",
            InsertUrl = @$"{_configParams.Model.Item2}Department/InsertDepartmentList",
            UpdateUrl = @$"{_configParams.Model.Item2}Department/UpdateDepartment"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var departmentRepo = DataSyncProviderFactory.GetRepository<Department>(_injectData);

        // pull all new department data
        var pullResponse = await PullDepartmentsServerToClientByRowCounts(departmentRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new department data
        var sqlDpQuery = GetDepartmentScript();
        var queryResponse = await QueryUtils.GetListAsync<Department>(sqlDpQuery);
        var dpList = queryResponse.List.ToList();
        if (dpList.Count > 0)
        {
            var pushResponse = await departmentRepo.PushNewListAsync(dpList);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateDepartment(int dId)
    {

        var commandText = $@"
            UPDATE AMS.Department SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (dId > 0)
        {
            commandText += $" WHERE DId = '{dId}'";
        }

        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public int SaveDepartmentAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_DEPARTMENT(DId, DName, DCode, Dlevel, Branch_ID, Company_ID, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT DId, DName, DCode, Dlevel, Branch_ID, Company_ID, Status, EnterBy, EnterDate,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.Department
            WHERE DId='{ObjDepartment.DId}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    //PULL DEPARTMENT

    public async Task<bool> PullDepartmentsServerToClientByRowCounts(IDataSyncRepository<Department> departmentRepo, int callCount)
    {
        try
        {
            var pullResponse = await departmentRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetDepartmentScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var departmentData in pullResponse.List)
            {
                ObjDepartment = departmentData;

                var alreadyExistData = alldata.Select("DId=" + departmentData.DId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (departmentData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveDepartment("UPDATE");
                    }
                }
                else
                {
                    var result = SaveDepartment("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullDepartmentsServerToClientByRowCounts(departmentRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }


    // RETURN DATA IN DATA TABLE

    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterDepartment(string actionTag, int selectedId = 0)
    {
        var cmdString = $@"Select *  FROM AMS.Department  where DId='{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public string GetDepartmentScript(int dId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Department d";
        cmdString += dId > 0 ? $" WHERE d.SyncGlobalId IS NULL AND d.DId= {dId} " : "";
        return cmdString;
    }

    // OBJECT FOR THIS FORM
    public Department ObjDepartment { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}