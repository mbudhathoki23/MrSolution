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

public class MainAgentRepository : IMainAgentRepository

{
    public MainAgentRepository()
    {
        ObjSeniorAgent = new MainAgent();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }


    // INSERT UPDATE DELETE

    public int SaveMainAgentAuditLog(string actionTag)
    {
        var cmdString = @$"
            INSERT INTO AUD.AUDIT_SENIORAGENT(SAgentId, SAgent, SAgentCode, PhoneNo, Address, Comm, GLID, Branch_id, Company_ID, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT SAgentId, SAgent, SAgentCode, PhoneNo, Address, Comm, GLID, Branch_id, Company_ID, Status, EnterBy, EnterDate,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.SeniorAgent
            WHERE SAgentId='{ObjSeniorAgent.SAgentId}'
             ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    public int SaveSeniorAgent(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "DELETE")
        {
            cmdString.Append($"Delete from AMS.SeniorAgent where SAgentId = {ObjSeniorAgent.SAgentId} ");
        }

        if (actionTag == "SAVE")
        {
            cmdString.Append(
                " INSERT INTO AMS.SeniorAgent(SAgentId, NepaliDesc, SAgent, SAgentCode, PhoneNo, Address, Comm, TagetLimit, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append($"Values({ObjSeniorAgent.SAgentId},N'{ObjSeniorAgent.NepaliDesc.GetTrimReplace()}',N'{ObjSeniorAgent.SAgent.GetTrimReplace()}',N'{ObjSeniorAgent.SAgentCode.GetTrimReplace()}', ");
            cmdString.Append(ObjSeniorAgent.PhoneNo.IsValueExits() ? $"N'{ObjSeniorAgent.PhoneNo}'," : "NULL,");
            cmdString.Append(ObjSeniorAgent.Address.IsValueExits()
                ? $"N'{ObjSeniorAgent.Address.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append(ObjSeniorAgent.Comm.GetDecimal() > 0 ? $"{ObjSeniorAgent.Comm}," : "NULL,");
            cmdString.Append(ObjSeniorAgent.TagetLimit.IsValueExits() ? $"{ObjSeniorAgent.TagetLimit}," : "NULL,");
            cmdString.Append(ObjSeniorAgent.GLID > 0 ? $"N'{ObjSeniorAgent.GLID}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjSeniorAgent.Status is true ? "1," : "0,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append($"GETDATE(),GETDATE(),{ObjSeniorAgent.SyncRowVersion}); ");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.SeniorAgent SET ");
            cmdString.Append($"SAgent= N'{ObjSeniorAgent.SAgent.GetTrimReplace()}',");
            cmdString.Append($"NepaliDesc= N'{ObjSeniorAgent.NepaliDesc.GetTrimReplace()}',");
            cmdString.Append($"SAgentCode= N'{ObjSeniorAgent.SAgentCode.GetTrimReplace()}',");
            cmdString.Append(ObjSeniorAgent.PhoneNo.IsValueExits()
                ? $"PhoneNo= N'{ObjSeniorAgent.PhoneNo}',"
                : "PhoneNo= NULL,");
            cmdString.Append(ObjSeniorAgent.Address.IsValueExits()
                ? $"Address= N'{ObjSeniorAgent.Address.GetTrimReplace()}',"
                : "Address = NULL,");
            cmdString.Append(ObjSeniorAgent.Comm.GetDecimal() > 0 ? $"Comm = {ObjSeniorAgent.Comm}," : "Comm = NULL,");
            cmdString.Append(ObjSeniorAgent.TagetLimit.GetDecimal() > 0
                ? $"TagetLimit = {ObjSeniorAgent.TagetLimit},"
                : "TagetLimit =NULL,");
            cmdString.Append(ObjSeniorAgent.GLID > 0 ? $"GLID= N'{ObjSeniorAgent.GLID}'," : "GLID= NULL,");
            cmdString.Append(ObjSeniorAgent.Status is true ? "Status= 1," : "Status= 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append(ObjSeniorAgent.SyncRowVersion.GetInt() > 0
                ? $"SyncRowVersion = {ObjSeniorAgent.SyncRowVersion}"
                : "SyncRowVersion =NULL,");
            cmdString.Append($"where SAgentId = {ObjSeniorAgent.SAgentId} ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncSeniorAgentAsync(actionTag));
            }
        }

        return exe;
    }

    public async Task<int> SyncSeniorAgentAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}SeniorAgent/GetSeniorAgentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SeniorAgent/InsertSeniorAgentList",
            UpdateUrl = @$"{_configParams.Model.Item2}SeniorAgent/UpdateSeniorAgent",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var seniorAgentRepo = DataSyncProviderFactory.GetRepository<MainAgent>(_injectData);
        var mainAgents = new List<MainAgent>
        {
            ObjSeniorAgent
        };
        // push realtime senioragent details to server
        await seniorAgentRepo.PushNewListAsync(mainAgents);

        // update senioragent SyncGlobalId to local
        if (seniorAgentRepo.GetHashCode() > 0)
        {
            await SyncUpdateSeniorAgent(ObjSeniorAgent.SAgentId);
        }
        return seniorAgentRepo.GetHashCode();
    }

    public async Task<bool> SyncSeniorAgentDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}SeniorAgent/GetSeniorAgentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SeniorAgent/InsertSeniorAgentList",
            UpdateUrl = @$"{_configParams.Model.Item2}SeniorAgent/UpdateSeniorAgent",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var seniorAgentRepo = DataSyncProviderFactory.GetRepository<MainAgent>(_injectData);

        // pull all new main agent data
        var pullResponse = await PullSeniorAgentsFromServerToClientDBByCallCount(seniorAgentRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new main agent data
        var sqlSaQuery = GetSeniorAgentScript();
        var queryResponse = await QueryUtils.GetListAsync<MainAgent>(sqlSaQuery);
        var saList = queryResponse.List.ToList();
        if (saList.Count > 0)
        {
            var pushResponse = await seniorAgentRepo.PushNewListAsync(saList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateSeniorAgent(int sAgentId = 0)
    {
        var commandText = $@"
            UPDATE AMS.SeniorAgent SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (sAgentId > 0)
        {
            commandText += $" WHERE SAgentId = '{sAgentId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    #region ---------- PULL SENIOR AGENTS ----------

    public async Task<bool> PullSeniorAgentsFromServerToClientDBByCallCount(IDataSyncRepository<MainAgent> seniorAgentRepo, int callCount)
    {
        try
        {
            var pullResponse = await seniorAgentRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetSeniorAgentScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);
            foreach (var seniorAgentData in pullResponse.List)
            {
                ObjSeniorAgent = seniorAgentData;

                var alreadyExistData = alldata.Select("SAgentId= " + seniorAgentData.SAgentId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (seniorAgentData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveSeniorAgent("UPDATE");
                    }
                }
                else
                {
                    var result = SaveSeniorAgent("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullSeniorAgentsFromServerToClientDBByCallCount(seniorAgentRepo, callCount);
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

    public string GetSeniorAgentScript(int sAgentId = 0)
    {
        var cmdString = "SELECT * FROM AMS.SeniorAgent sa ";
        cmdString += sAgentId > 0 ? $" WHERE sa.SyncGlobalId IS NULL AND sa.Branch_Id= {sAgentId} " : "";
        return cmdString;
    }

    public DataTable GetMasterSrAgent(string actionTag, long selectedId = 0)
    {
        var cmdString = string.Empty;
        if (selectedId == 0)
        {
            cmdString =
                "SELECT SAgentId, SAgent, SAgentCode , Address, SA.PhoneNo ,Comm , gl.GLName FROM AMS.SENIORAGENT SA left outer join ams.GeneralLedger gl on GL.GLID = SA.GLID \n";
            cmdString += " ";

            if (!string.IsNullOrEmpty(actionTag) && actionTag is "DELETE")
            {
                cmdString +=
                    "WHERE not exists (select ad.Agent_ID  from  AMS.AccountDetails ad where sa.SAgentId = ad.Agent_ID)";
            }
        }
        else
        {
            cmdString =
                " SELECT SAgentId, SAgent, SAgentCode, SA.PhoneNo,Address,SA.GLID,GL.GLName,Comm,SA.Status from AMS.SeniorAgent as SA LEFT OUTER JOIN AMS.GeneralLedger GL on SA.SAgentId = GL.GLID\n ";
            cmdString = $"{cmdString}WHERE SA.SAgentId = '{selectedId}'";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt)
    {
        var cmdString = $"SELECT  {tableId} SelectedId From {tableName} where {tableColumn}='{filterTxt}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["SelectedId"].GetInt() : 0;
    }

    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId,
        string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }




    // OBJECT FOR THIS FROM 

    public MainAgent ObjSeniorAgent { get; set; }
    private IMasterSetup _master;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}