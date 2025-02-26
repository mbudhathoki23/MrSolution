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

public class JuniorAgentRepository : IJuniorAgentRepository
{
    public JuniorAgentRepository()
    {
        ObjJuniorAgent = new JuniorAgent();
        ObjSeniorAgent = new MainAgent();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public int SaveJuniorAgent(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() is "DELETE")
        {
            cmdString.Append($"Delete from AMS.JuniorAgent where AgentId = {ObjJuniorAgent.AgentId}; ");
        }

        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.JuniorAgent (AgentId, AgentName, AgentCode, Address, PhoneNo, GLCode, Commission, SAgent, Email, CRLimit,TargetLimit, CrDays, CrType, Branch_id, Company_ID, EnterBy, EnterDate, Status, SyncBaseId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn,  SyncGlobalId,SyncRowVersion ) \n");
            cmdString.Append($"Values({ObjJuniorAgent.AgentId}, ");
            cmdString.Append(!ObjJuniorAgent.AgentName.IsBlankOrEmpty()
                ? $"N'{ObjJuniorAgent.AgentName.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append(ObjJuniorAgent.AgentCode.IsValueExits()
                ? $"N'{ObjJuniorAgent.AgentCode.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append(ObjJuniorAgent.Address.IsValueExits()
                ? $"N'{ObjJuniorAgent.Address.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append(!ObjJuniorAgent.PhoneNo.IsBlankOrEmpty() ? $"N'{ObjJuniorAgent.PhoneNo}'," : "NULL,");
            cmdString.Append(ObjJuniorAgent.GLCode.GetLong() > 0 ? $"N'{ObjJuniorAgent.GLCode}'," : "NULL,");
            cmdString.Append(ObjJuniorAgent.Commission.GetDecimal() > 0 ? $"{ObjJuniorAgent.Commission}," : "0,");
            cmdString.Append(ObjJuniorAgent.SAgent.GetInt() > 0 ? $"N'{ObjJuniorAgent.SAgent}'," : "NULL,");
            cmdString.Append(ObjJuniorAgent.Email.IsValueExits() ? $"N'{ObjJuniorAgent.Email}'," : "NULL,");
            cmdString.Append(ObjJuniorAgent.CrLimit.GetDecimal() > 0 ? $"N{ObjJuniorAgent.CrLimit}," : "0,");
            cmdString.Append(ObjJuniorAgent.TargetLimit.GetDecimal() > 0 ? $"{ObjJuniorAgent.TargetLimit}," : "0,");
            cmdString.Append(ObjJuniorAgent.CrDays.GetDecimal() > 0 ? $"N{ObjJuniorAgent.CrDays}," : "0,");
            cmdString.Append(ObjJuniorAgent.CrTYpe.IsValueExits() ? $"N'{ObjJuniorAgent.CrTYpe}'," : "'I',");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjJuniorAgent.Status ? "1," : "0,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync
                ? ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,"
                : "NULL,");
            cmdString.Append("GetDate(),GetDate(), ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append($"{ObjJuniorAgent.SyncRowVersion}); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.JuniorAgent SET ");
            cmdString.Append(ObjJuniorAgent.AgentName.IsValueExits()
                ? $"AgentName = N'{ObjJuniorAgent.AgentName.GetTrimReplace()}',"
                : "AgentName = NULL,");
            cmdString.Append(ObjJuniorAgent.AgentCode.IsValueExits()
                ? $"AgentCode = N'{ObjJuniorAgent.AgentCode.GetTrimReplace()}',"
                : "AgentCode = NULL,");
            cmdString.Append(ObjJuniorAgent.Address.IsValueExits()
                ? $"Address = N'{ObjJuniorAgent.Address.GetTrimReplace()}',"
                : "Address = NULL,");
            cmdString.Append(ObjJuniorAgent.PhoneNo.IsValueExits()
                ? $"PhoneNo = N'{ObjJuniorAgent.PhoneNo}',"
                : "PhoneNo = NULL,");
            cmdString.Append(ObjJuniorAgent.GLCode.GetLong() > 0
                ? $" GLCode = N'{ObjJuniorAgent.GLCode}',"
                : "GLCode = NULL,");
            cmdString.Append(ObjJuniorAgent.Commission.GetDecimal() > 0
                ? $"Commission = N'{ObjJuniorAgent.Commission}',"
                : "Commission = 0,");
            cmdString.Append(ObjJuniorAgent.SAgent.GetInt() > 0
                ? $"SAgent= N'{ObjJuniorAgent.SAgent.ToString().GetTrimReplace()}',"
                : "SAgent = NULL,");
            cmdString.Append(ObjJuniorAgent.Email.IsValueExits()
                ? $"Email = N'{ObjJuniorAgent.Email}',"
                : "Email = NULL,");
            cmdString.Append(ObjJuniorAgent.CrLimit.GetDecimal() > 0
                ? $"CrLimit = N'{ObjJuniorAgent.CrLimit}',"
                : "CrLimit = 0,");
            cmdString.Append(ObjJuniorAgent.CrDays.IsValueExits()
                ? $"CrDays = N'{ObjJuniorAgent.CrDays}',"
                : " CrDays = 'NUL',");
            cmdString.Append(ObjJuniorAgent.CrTYpe.IsValueExits()
                ? $"CrType = N'{ObjJuniorAgent.CrTYpe}',"
                : " CrType = 'I',");
            cmdString.Append(ObjJuniorAgent.Status.GetBool() ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjJuniorAgent.SyncRowVersion}");
            cmdString.Append($" WHERE AgentId = {ObjJuniorAgent.AgentId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncJuniorAgentAsync(actionTag));
        }
        return exe;
    }
    public async Task<int> SyncJuniorAgentAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}JuniorAgent/GetJuniorAgentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}JuniorAgent/InsertJuniorAgentList",
            UpdateUrl = @$"{_configParams.Model.Item2}JuniorAgent/UpdateJuniorAgent",
        };
        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var juniorAgentRepo = DataSyncProviderFactory.GetRepository<JuniorAgent>(_injectData);
        var juniorAgents = new List<JuniorAgent>
        {
            ObjJuniorAgent
        };
        // push realtime branch details to server
        await juniorAgentRepo.PushNewListAsync(juniorAgents);

        // update branch SyncGlobalId to local
        if (juniorAgentRepo.GetHashCode() > 0)
        {
            await SyncUpdateJuniorAgent(ObjJuniorAgent.AgentId);
        }
        return juniorAgentRepo.GetHashCode();
    }

    public async Task<bool> SyncJuniorAgentDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}JuniorAgent/GetJuniorAgentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}JuniorAgent/InsertJuniorAgentList",
            UpdateUrl = @$"{_configParams.Model.Item2}JuniorAgent/UpdateJuniorAgent",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var juniorAgentRepo = DataSyncProviderFactory.GetRepository<JuniorAgent>(_injectData);

        // pull all new agent data
        var pullResponse = await PullJuniorAgentsFromServerToClientDBByCallCount(juniorAgentRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }


        // push all new agent data

        var sqlJaQuery = GetJuniorAgentScript();
        var queryResponse = await QueryUtils.GetListAsync<JuniorAgent>(sqlJaQuery);
        var jaList = queryResponse.List.ToList();
        if (jaList.Count > 0)
        {
            var pushResponse = await juniorAgentRepo.PushNewListAsync(jaList);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateJuniorAgent(int juniorAgentId = 0)
    {
        var commandText = $@"
            UPDATE AMS.JuniorAgent SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (juniorAgentId > 0)
        {
            commandText += $" WHERE AgentId = '{juniorAgentId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    // public int SaveSeniorAgent(string actionTag)
    // {
    //     var cmdString = new StringBuilder();
    //     if (actionTag is "DELETE")
    //     {
    //         cmdString.Append($"Delete from AMS.SeniorAgent where SAgentId = {ObjSeniorAgent.SAgentId} ");
    //     }
    //
    //     if (actionTag == "SAVE")
    //     {
    //         cmdString.Append(
    //             " INSERT INTO AMS.SeniorAgent(SAgentId, NepaliDesc, SAgent, SAgentCode, PhoneNo, Address, Comm, TagetLimit, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
    //         cmdString.Append(
    //             $"Values({ObjSeniorAgent.SAgentId},N'{ObjSeniorAgent.SAgent.GetTrimReplace()}',N'{ObjSeniorAgent.SAgent.GetTrimReplace()}',N'{ObjSeniorAgent.SAgentCode.GetTrimReplace()}', ");
    //         cmdString.Append(ObjSeniorAgent.PhoneNo.IsValueExits() ? $"N'{ObjSeniorAgent.PhoneNo}'," : "NULL,");
    //         cmdString.Append(ObjSeniorAgent.Address.IsValueExits()
    //             ? $"N'{ObjSeniorAgent.Address.GetTrimReplace()}',"
    //             : "NULL,");
    //         cmdString.Append(ObjSeniorAgent.Comm.GetDecimal() > 0 ? $"{ObjSeniorAgent.Comm}," : "NULL,");
    //         cmdString.Append(ObjSeniorAgent.TagetLimit.IsValueExits() ? $"{ObjSeniorAgent.TagetLimit}," : "NULL,");
    //         cmdString.Append(ObjSeniorAgent.GLID > 0 ? $"N'{ObjSeniorAgent.GLID}'," : "NULL,");
    //         cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
    //         cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
    //         cmdString.Append(ObjSeniorAgent.Status is true ? "1," : "0,");
    //         cmdString.Append($"0,'{ObjGlobal.LogInUser}', GETDATE(),");
    //         cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
    //         cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
    //             ? $" '{ObjGlobal.LocalOriginId}',"
    //             : "NULL,");
    //         cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
    //         cmdString.Append($"GETDATE(),GETDATE(),{ObjSeniorAgent.SyncRowVersion}); ");
    //     }
    //     else if (actionTag == "UPDATE")
    //     {
    //         cmdString.Append("UPDATE AMS.SeniorAgent SET ");
    //         cmdString.Append($"SAgent= N'{ObjSeniorAgent.SAgent.GetTrimReplace()}',");
    //         cmdString.Append($"SAgentCode= N'{ObjSeniorAgent.SAgentCode.GetTrimReplace()}',");
    //         cmdString.Append(ObjSeniorAgent.PhoneNo.IsValueExits()
    //             ? $"PhoneNo= N'{ObjSeniorAgent.PhoneNo}',"
    //             : "PhoneNo= NULL,");
    //         cmdString.Append(ObjSeniorAgent.Address.IsValueExits()
    //             ? $"Address= N'{ObjSeniorAgent.Address.GetTrimReplace()}',"
    //             : "Address = NULL,");
    //         cmdString.Append(ObjSeniorAgent.Comm.GetDecimal() > 0 ? $"Comm = {ObjSeniorAgent.Comm}," : "Comm = NULL,");
    //         cmdString.Append(ObjSeniorAgent.TagetLimit.GetDecimal() > 0
    //             ? $"TagetLimit = {ObjSeniorAgent.TagetLimit},"
    //             : "TagetLimit =NULL,");
    //         cmdString.Append(ObjSeniorAgent.GLID > 0 ? $"GLID= N'{ObjSeniorAgent.GLID}'," : "GLID= NULL,");
    //         cmdString.Append(ObjSeniorAgent.Status is true ? "Status= 1," : "Status= 0,");
    //         cmdString.Append("SyncLastPatchedOn = GETDATE(),");
    //         cmdString.Append(ObjSeniorAgent.SyncRowVersion.GetInt() > 0
    //             ? $"SyncRowVersion = {ObjSeniorAgent.SyncRowVersion}"
    //             : "SyncRowVersion =NULL,");
    //         cmdString.Append($"where SAgentId = {ObjSeniorAgent.SAgentId} ");
    //     }
    //
    //     var exe = ExecuteCommand.ExecuteNonQuery(cmdString.ToString());
    //     if (exe > 0)
    //     {
    //         try
    //         {
    //             if (ObjGlobal.IsOnlineSync)
    //             {
    //                 Task.Run(() => SyncSeniorAgentAsync(actionTag));
    //             }
    //         }
    //         catch
    //         {
    //         }
    //     }
    //
    //     return exe;
    // }
    // public async Task<int> SyncSeniorAgentAsync(string actionTag)
    // {
    //     var apiConfig = new SyncApiConfig
    //     {
    //         BaseUrl = _configParams.Model.Item2,
    //         Apikey = _configParams.Model.Item3,
    //         Username = ObjGlobal.LogInUser,
    //         BranchId = ObjGlobal.SysBranchId,
    //         GetUrl = @$"{_configParams.Model.Item2}SeniorAgent/GetSeniorAgentsByCallCount",
    //         InsertUrl = @$"{_configParams.Model.Item2}SeniorAgent/InsertSeniorAgentList",
    //         UpdateUrl = @$"{_configParams.Model.Item2}SeniorAgent/UpdateSeniorAgent",
    //     };
    //
    //     DataSyncHelper.SetConfig(apiConfig);
    //     _injectData.ApiConfig = apiConfig;
    //     DataSyncManager.SetGlobalInjectData(_injectData);
    //     var seniorAgentRepo = DataSyncProviderFactory.GetRepository<MainAgent>(_injectData);
    //     var mainAgents = new List<MainAgent>
    //     {
    //         ObjSeniorAgent
    //     };
    //     // push realtime senioragent details to server
    //     await seniorAgentRepo.PushNewListAsync(mainAgents);
    //
    //     // update senioragent SyncGlobalId to local
    //     if (seniorAgentRepo.GetHashCode() > 0)
    //     {
    //         await SyncUpdateSeniorAgent(ObjSeniorAgent.SAgentId);
    //     }
    //     return seniorAgentRepo.GetHashCode();
    // }
    // public Task<int> SyncUpdateSeniorAgent(int sAgentId = 0)
    // {
    //     var commandText = $@"
    //     UPDATE AMS.SeniorAgent SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
    //     if (sAgentId > 0)
    //     {
    //         commandText += $" WHERE SAgentId = '{sAgentId}'";
    //     }
    //     var result = ExecuteCommand.ExecuteNonQueryAsync(commandText);
    //     return result;
    // }
    public string GetJuniorAgentScript(int juniorAgentId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.JuniorAgent ja";
        cmdString += juniorAgentId > 0 ? $" WHERE ja.SyncGlobalId IS NULL AND ja.AgentId= {juniorAgentId} " : "";
        return cmdString;
    }

    #region ---------- PULL JUNIOR AGENT ----------

    public async Task<bool> PullJuniorAgentsFromServerToClientDBByCallCount(IDataSyncRepository<JuniorAgent> juniorAgentRepo, int callCount)
    {
        try
        {
            var pullResponse = await juniorAgentRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetJuniorAgentScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var juniorAgentData in pullResponse.List)
            {
                ObjJuniorAgent = juniorAgentData;

                var alreadyExistData = alldata.Select("AgentId= " + juniorAgentData.AgentId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (juniorAgentData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveJuniorAgent("UPDATE");
                    }
                }
                else
                {
                    var result = SaveJuniorAgent("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullJuniorAgentsFromServerToClientDBByCallCount(juniorAgentRepo, callCount);
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
    public DataTable GetMasterJrAgent(string actionTag, long selectedId = 0)
    {
        var cmdString = $@"
            SELECT JA.AgentId, JA.AgentName, JA.AgentCode, JA.Address, JA.PhoneNo, JA.GLCode, GL.GLName, JA.SAgent, SA.SAgent SAgentDesc, Commission 
            FROM AMS.JuniorAgent AS JA 
            LEFT OUTER JOIN AMS.GeneralLedger GL ON JA.GLCode = GL.GLID 
            LEFT OUTER JOIN AMS.SeniorAgent SA ON JA.SAgent = SA.SAgentId 
            WHERE JA.AgentId = '{selectedId}'
            ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt)
    {
        var cmdString = $"SELECT  {tableId} SelectedId From {tableName} where {tableColumn}='{filterTxt}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["SelectedId"].GetInt() : 0;
    }
    public int SaveJuniorAgentAuditLog(string actionTag)
    {
        var cmdString = @$"
            INSERT INTO AUD.AUDIT_JUNIOR_AGENT(AgentId, AgentName, AgentCode, Address, PhoneNo, GLCode, Commission, SAgent, Email, CRLimit, CrDays, CrType, Branch_id, Company_ID, EnterBy, EnterDate, Status, ModifyAction, ModifyBy, ModifyDate)
            SELECT AgentId, AgentName, AgentCode, Address, PhoneNo, GLCode, Commission, SAgent, Email, CRLimit, CrDays, CrType, Branch_id, Company_ID, EnterBy, EnterDate, Status,'' ModifyAction,'' ModifyBy,GETDATE() ModifyDate
            FROM AMS.JuniorAgent
            WHERE AgentId='{ObjJuniorAgent.AgentId}'
            ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }



    // OBJECT FOR THIS FROM 
    public JuniorAgent ObjJuniorAgent { get; set; }
    public MainAgent ObjSeniorAgent { get; set; }

    // MainAgent ObjSeniorAgent { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}