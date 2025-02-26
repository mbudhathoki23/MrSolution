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

public class AccountSubGroupRepository : IAccountSubGroupRepository
{
    public AccountSubGroupRepository()
    {
        ObjAccountSubGroup = new AccountSubGroup();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();

    }
    public int SaveAccountSubGroup(string actionTag)
    {
        actionTag = actionTag.GetUpper();
        var cmdString = new StringBuilder();
        if (actionTag is "DELETE")
        {
            cmdString.Append($"Delete from AMS.GeneralLedger where SubGrpId = {ObjAccountSubGroup.SubGrpId} \n");
            cmdString.Append($"Delete from AMS.AccountSubGroup where SubGrpId = {ObjAccountSubGroup.SubGrpId}");
        }

        if (actionTag == "SAVE")
        {
            cmdString.Append(
                " INSERT INTO AMS.AccountSubGroup(SubGrpId, NepaliDesc, SubGrpName, GrpId, SubGrpCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append($" VALUES({ObjAccountSubGroup.SubGrpId},");
            cmdString.Append(ObjAccountSubGroup.NepaliDesc.IsValueExits()
                ? $"N'{ObjAccountSubGroup.NepaliDesc.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append(ObjAccountSubGroup.SubGrpName.IsValueExits()
                ? $"N'{ObjAccountSubGroup.SubGrpName.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append(ObjAccountSubGroup.GrpId > 0 ? $"{ObjAccountSubGroup.GrpId}," : "NULL,");
            cmdString.Append(ObjAccountSubGroup.SubGrpCode.IsValueExits()
                ? $"N'{ObjAccountSubGroup.SubGrpCode.GetTrimReplace()}',"
                : "NULL,");
            cmdString.Append($" N'{ObjGlobal.SysBranchId}',");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjAccountSubGroup.Status ? " 1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}',GETDATE(),");
            cmdString.Append(ObjAccountSubGroup.PrimaryGroupId > 0
                ? $"N'{ObjAccountSubGroup.PrimaryGroupId}',"
                : "Null,");
            cmdString.Append(ObjAccountSubGroup.PrimarySubGroupId > 0
                ? $"N'{ObjAccountSubGroup.PrimarySubGroupId}',"
                : "Null,");
            cmdString.Append("0,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append("GETDATE(),");
            cmdString.Append($"{ObjAccountSubGroup.SyncRowVersion.GetDecimal(true)}); ");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.AccountSubGroup SET \n");
            cmdString.Append(ObjAccountSubGroup.SubGrpName.IsValueExits()
                ? $"SubGrpName = N'{ObjAccountSubGroup.SubGrpName.GetTrimReplace()}',"
                : "SubGrpName = NULL,");
            cmdString.Append(ObjAccountSubGroup.GrpId > 0
                ? $"GrpID = N'{ObjAccountSubGroup.GrpId}',"
                : "GrpID = NULL,");
            cmdString.Append(ObjAccountSubGroup.SubGrpCode.IsValueExits()
                ? $"SubGrpCode = N'{ObjAccountSubGroup.SubGrpCode.GetTrimReplace()}',"
                : "SubGrpCode = NULL,");
            cmdString.Append(ObjAccountSubGroup.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append(ObjAccountSubGroup.PrimaryGroupId > 0
                ? $"PrimaryGroupId = N'{ObjAccountSubGroup.PrimaryGroupId}',"
                : "PrimaryGroupId = Null,");
            cmdString.Append(ObjAccountSubGroup.NepaliDesc.IsValueExits()
                ? $"NepaliDesc = N'{ObjAccountSubGroup.NepaliDesc.GetTrimReplace()}',"
                : "NepaliDesc = NULL,");
            cmdString.Append("SyncLastPatchedOn = GETDATE() ,");
            cmdString.Append($" SyncRowVersion =  {ObjAccountSubGroup.SyncRowVersion} \n");
            cmdString.Append($" WHERE SubGrpId = {ObjAccountSubGroup.SubGrpId};");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncAccountSubGroupAsync(actionTag));
        }

        return exe;
    }
    public async Task<int> SyncAccountSubGroupAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}AccountSubGroup/GetAccountSubGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}AccountSubGroup/InsertAccountSubGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}AccountSubGroup/UpdateAccountSubGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var accountSubGroupRepo = DataSyncProviderFactory.GetRepository<AccountSubGroup>(_injectData);
        var accountSubGroups = new List<AccountSubGroup>
        {
            ObjAccountSubGroup
        };
        // push realtime accountSubGroup details to server
        await accountSubGroupRepo.PushNewListAsync(accountSubGroups);

        // update accountSubGroup SyncGlobalId to local
        if (accountSubGroupRepo.GetHashCode() > 0)
        {
            await SyncUpdateAccountSubGroup(ObjAccountSubGroup.SubGrpId);
        }
        return accountSubGroupRepo.GetHashCode();

    }

    public async Task<bool> SyncAccountSubGroupDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}AccountSubGroup/GetAccountSubGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}AccountSubGroup/InsertAccountSubGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}AccountSubGroup/UpdateAccountSubGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var accountSubGroupRepo = DataSyncProviderFactory.GetRepository<AccountSubGroup>(_injectData);

        // pull all new account sub-group data from server database
        var pullResponse = await PullAccountSubGroupsServerToClientByRowCount(accountSubGroupRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new account sub-group data of client database to server database
        var sqlAsgQuery = GetAccountSubGroupScript();
        var queryResponse = await QueryUtils.GetListAsync<AccountSubGroup>(sqlAsgQuery);
        var asgList = queryResponse.List.ToList();
        if (asgList.Count > 0)
        {
            var pushResponse = await accountSubGroupRepo.PushNewListAsync(asgList);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateAccountSubGroup(int subGrpId)
    {
        var commandText = $@"
            UPDATE AMS.Branch SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (subGrpId > 0)
        {
            commandText += $" WHERE SubGrpId = '{subGrpId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public int SaveAccountSubGroupAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_ACCOUNTSUBGROUP(SubGrpId, SubGrpName, Company_Id, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, GrpId, SubGrpCode, Branch_ID, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId, ModifyAction, ModifyBy, ModifyDate)
            SELECT SubGrpId, SubGrpName, Company_Id, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, GrpId, SubGrpCode, Branch_ID, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.AccountSubGroup
            WHERE SubGrpId='{ObjAccountSubGroup.SubGrpId}'
            ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    // PULL ACCOUNT SUB GROUP
    #region ---------- PULL ACCOUNT SUB GROUP ----------

    public async Task<bool> PullAccountSubGroupsServerToClientByRowCount(IDataSyncRepository<AccountSubGroup> accountSubGroupRepo, int callCount)
    {
        try
        {
            var pullResponse = await accountSubGroupRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var actionTag = "UPDATE";
            var query = GetAccountSubGroupScript();
            var allData = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var accountSubGroupData in pullResponse.List)
            {
                ObjAccountSubGroup = accountSubGroupData;

                var alreadyExistData = allData.Select("SubGrpId= " + accountSubGroupData.SubGrpId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (accountSubGroupData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveAccountSubGroup(actionTag);
                    }
                }
                else
                {
                    actionTag = "SAVE";
                    var result = SaveAccountSubGroup(actionTag);
                }


            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullAccountSubGroupsServerToClientByRowCount(accountSubGroupRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // RETURN VALUE IN DATA TABLE
    public DataTable GetMasterAccountSubGroup(string tag, string category, int status = 0, int selectedId = 0, bool isPrimary = false)
    {
        if (selectedId == 0)
        {
            return new DataTable();
        }

        var cmdString =
            "SELECT ASG.subGrpId,ASG.SubGrpName,ASG.SubGrpCode,ASG.GrpId,AG.GrpName , ASG.PrimaryGroupId,AGG.GrpName Sub_GrpName, asg1.SubGrpName SecSubGroup,ASG.Status, ASG.NepaliDesc from AMS.AccountSubGroup ASG left outer join AMS.AccountGroup AG on ASG.GrpID = AG.GrpId  left outer join AMS.AccountSubGroup asg1 on ASG.SubGrpId = AG.GrpId  left outer join AMS.AccountGroup AGG on ASG.PrimaryGroupId = AGG.GrpId  \n";
        cmdString += $" WHERE ASG.SubGrpId = '{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public string GetAccountSubGroupScript(int subGrpId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.AccountSubGroup asg";
        cmdString += subGrpId > 0 ? $" WHERE asg.SyncGlobalId IS NULL AND asg.Branch_Id= {subGrpId} " : "";
        return cmdString;
    }



    //OBJECT FOR THIS FORM
    public AccountSubGroup ObjAccountSubGroup { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}