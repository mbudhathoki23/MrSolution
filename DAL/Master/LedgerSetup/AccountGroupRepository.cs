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
using System.Windows.Forms;
// ReSharper disable All

namespace MrDAL.Master.LedgerSetup;

public class AccountGroupRepository : IAccountGroupRepository
{
    // ACCOUNT GROUP REPOSITORY
    #region ---------- ACCOUNT GROUP REPOSITORY ----------
    public AccountGroupRepository()
    {
        ObjAccountGroup = new AccountGroup();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    public int SaveAccountGroup(string actionTag)
    {
        actionTag = actionTag.GetUpper();

        var cmdString = new StringBuilder();
        if (actionTag is "DELETE")
        {
            SaveAccountGroupAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.AccountGroup where GrpId =  {ObjAccountGroup.GrpId}");
        }

        if (actionTag == "SAVE")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.AccountGroup(GrpId, NepaliDesc, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append("\n Values \n ");
            cmdString.Append($"({ObjAccountGroup.GrpId}, ");
            cmdString.Append(ObjAccountGroup.NepaliDesc.IsValueExits() ? $"N'{ObjAccountGroup.NepaliDesc.GetTrimReplace()}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.GrpName.IsValueExits() ? $"N'{ObjAccountGroup.GrpName.GetTrimReplace()}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.GrpCode.IsValueExits() ? $"N'{ObjAccountGroup.GrpCode.GetTrimReplace()}'," : "NULL,");
            cmdString.Append($"{ObjAccountGroup.Schedule.GetDecimal(true)},");
            cmdString.Append(ObjAccountGroup.PrimaryGrp.IsValueExits() ? $"N'{ObjAccountGroup.PrimaryGrp.GetTrimReplace()}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.GrpType.IsValueExits() ? $"N'{ObjAccountGroup.GrpType.GetTrimReplace()}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.Branch_ID > 0 ? $" {ObjAccountGroup.Branch_ID}," : "0,");
            cmdString.Append(ObjAccountGroup.Company_Id > 0 ? $" {ObjAccountGroup.Company_Id}," : "NULL,");
            cmdString.Append(ObjAccountGroup.Status ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjAccountGroup.PrimaryGroupId > 0 ? $"{ObjAccountGroup.PrimaryGroupId}," : "NULL,");
            cmdString.Append(ObjAccountGroup.IsDefault.IsValueExits() ? $"{ObjAccountGroup.IsDefault}," : "NULL,");

            cmdString.Append(ObjAccountGroup.SyncBaseId.IsValueExits() ? $"N'{ObjAccountGroup.SyncBaseId}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.SyncGlobalId.IsValueExits() ? $"N'{ObjAccountGroup.SyncGlobalId}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.SyncOriginId.IsValueExits() ? $"N'{ObjAccountGroup.SyncOriginId}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.SyncCreatedOn.IsValueExits() ? $"N'{ObjAccountGroup.SyncCreatedOn.GetSystemDate()}'," : "NULL,");
            cmdString.Append(ObjAccountGroup.SyncLastPatchedOn.IsValueExits() ? $"N'{ObjAccountGroup.SyncLastPatchedOn.GetSystemDate()}'," : "NULL,");
            cmdString.Append($"{ObjAccountGroup.SyncRowVersion} ); \n");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.AccountGroup SET ");
            cmdString.Append(ObjAccountGroup.GrpName.IsValueExits() ? $"GrpName = N'{ObjAccountGroup.GrpName.GetTrimReplace()}'," : "GrpName = NULL");
            cmdString.Append(ObjAccountGroup.GrpCode.IsValueExits() ? $"GrpCode = N'{ObjAccountGroup.GrpCode.GetTrimReplace()}'," : "GrpCode = NULL,");
            cmdString.Append(ObjAccountGroup.NepaliDesc.IsValueExits() ? $"NepaliDesc = N'{ObjAccountGroup.NepaliDesc.GetTrimReplace()}'," : $"NepaliDesc = N'{ObjAccountGroup.GrpName.GetTrimReplace()}',");
            cmdString.Append($"Schedule = N'{ObjAccountGroup.Schedule.ToString().GetTrimReplace()}',");
            cmdString.Append(ObjAccountGroup.PrimaryGrp.IsValueExits() ? $"PrimaryGrp = N'{ObjAccountGroup.PrimaryGrp.GetTrimReplace()}'," : "PrimaryGrp = NULL,");
            cmdString.Append(ObjAccountGroup.GrpType.IsValueExits() ? $"GrpType = N'{ObjAccountGroup.GrpType.GetTrimReplace()}'," : "GrpType = NULL");
            cmdString.Append(ObjAccountGroup.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "SyncLastPatchedOn = GETDATE()," : "SyncLastPatchedOn = NULL,");
            cmdString.Append($"SyncRowVersion = {ObjAccountGroup.SyncRowVersion}");
            cmdString.Append($" WHERE GrpId = {ObjAccountGroup.GrpId} ");
        }
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncAccountGroupAsync(actionTag));
        }

        return exe;
    }

    public async Task<int> SyncAccountGroupAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}AccountGroup/GetAccountGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}AccountGroup/InsertAccountGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}AccountGroup/UpdateAccountGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var accountGroupRepo = DataSyncProviderFactory.GetRepository<AccountGroup>(_injectData);
        var accountGroups = new List<AccountGroup>
        {
            ObjAccountGroup
        };

        // push realtime accountgroup details to server
        await accountGroupRepo.PushNewListAsync(accountGroups);

        // update accountGroup SyncGlobalId to local
        if (accountGroupRepo.GetHashCode() > 0)
        {
            await SyncUpdateAccountGroup(ObjAccountGroup.GrpId);
        }

        return accountGroupRepo.GetHashCode();

    }

    public Task<int> SyncUpdateAccountGroup(int grpId)
    {
        var commandText = $@"
            UPDATE AMS.AccountGroup SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (grpId > 0)
        {
            commandText += $" WHERE GrpId = '{grpId}'";
        }

        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public async Task<bool> SyncAccountGroupDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}AccountGroup/GetAccountGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}AccountGroup/InsertAccountGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}AccountGroup/UpdateAccountGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var accountGroupRepo = DataSyncProviderFactory.GetRepository<AccountGroup>(_injectData);

        // pull all new account data
        var pullResponse = await PullAccountGroupsServerToClientByRowCounts(accountGroupRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new account data
        var sqlAgQuery = GetAccountGroupScript();
        var queryResponse = await QueryUtils.GetListAsync<AccountGroup>(sqlAgQuery);
        var agList = queryResponse.List.ToList();
        if (agList.Count > 0)
        {
            var pushResponse = await accountGroupRepo.PushNewListAsync(agList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }

        }

        return true;
    }

    public int SaveAccountGroupAuditLog(string actionTag)
    {
        var cmdAudit = @$"
            INSERT INTO AUD.AUDIT_ACCOUNT_GROUP(GrpId, PrimaryGroupId, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, IsDefault, NepaliDesc, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, ModifyAction, ModifyBy, ModifyDate)
            SELECT GrpId, PrimaryGroupId, GrpName, GrpCode, Schedule, PrimaryGrp, GrpType, IsDefault, NepaliDesc, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate 
            FROM AMS.AccountGroup
            WHERE GrpId = '{ObjAccountGroup.GrpId}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdAudit);
        return exe;
    }
    #endregion


    // PULL ACCOUNT GROUP
    #region ---------- PULL ACCOUNT GROUP ----------
    public async Task<bool> PullAccountGroupsServerToClientByRowCounts(IDataSyncRepository<AccountGroup> accountGroupRepo, int callCount)
    {
        try
        {

            var pullResponse = await accountGroupRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetAccountGroupScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var accountGroupData in pullResponse.List)
            {
                ObjAccountGroup = accountGroupData;

                var alreadyExistData = alldata.Select("GrpId= " + accountGroupData.GrpId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = alreadyExistData[0]["SyncRowVersion"].GetShort();

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (accountGroupData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveAccountGroup("UPDATE");
                    }
                }
                else
                {
                    var result = SaveAccountGroup("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullAccountGroupsServerToClientByRowCounts(accountGroupRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    #endregion


    // BIND DATA IN COMBO BOX
    #region ---------- BIND DATA IN COMBO BOX ----------
    public void BindPrimaryGroup(ComboBox cmbType)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Balance Sheet", "BS"),
            new("Profit & Loss", "PL"),
            new("Trading Account", "TA")
        };
        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
    }
    public void BindAccountGrpType(ComboBox cmbType, string station)
    {
        var list = station switch
        {
            "Balance Sheet" or "BS" => new List<ValueModel<string, string>>
            {
                new("Assets", "A"),
                new("Liabilities", "L")
            },
            "Profit & Loss" or "Trading Account" or "PL" or "TA" => new List<ValueModel<string, string>>
            {
                new("Expenses", "E"),
                new("Income", "I")
            },
            _ => new List<ValueModel<string, string>>()
        };
        if (list.Count == 0)
        {
            return;
        }

        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
        cmbType.SelectedIndex = 0;
    }
    #endregion


    // RETURN VALUE IN DATA TABLE
    #region ---------- RETURN VALUE IN DATA TABLE ----------
    public string GetAccountGroupScript(int grpId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.AccountGroup ag";
        cmdString += grpId > 0 ? $" WHERE ag.SyncGlobalId IS NULL AND ag.GrpId= {grpId} " : "";
        return cmdString;
    }
    public DataTable GetMasterAccountGroup(string tag, string category, int status = 0, int selectedId = 0, bool isPrimary = false)
    {
        var cmdString = @$"
			SELECT ag.GrpId, ag.GrpName, ag.GrpCode, ag.Schedule, CASE WHEN ag.PrimaryGrp='BS' THEN 'Balance Sheet' WHEN ag.PrimaryGrp='PL' THEN 'Profit & Loss' WHEN ag.PrimaryGrp='TA' THEN 'Trading Account' ELSE ag.PrimaryGrp END PrimaryGrp, CASE WHEN ag.GrpType='A' THEN 'Assets' WHEN ag.GrpType='L' THEN 'Liabilities' WHEN ag.GrpType='E' THEN 'Expenses' WHEN ag.GrpType='I' THEN 'Income' ELSE ag.GrpType END GrpType, ag.Branch_ID, ag.Company_Id, ag.Status, ag.EnterBy, ag.EnterDate, ag.PrimaryGroupId, ag1.GrpName SecGroup, ag.IsDefault, ag.NepaliDesc
			FROM AMS.AccountGroup ag
				 LEFT OUTER JOIN AMS.AccountGroup ag1 ON ag.PrimaryGroupId=ag1.GrpId
			Where ag.GrpId='{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion


    // OBJECT FOR THIS FORM
    #region ---------- OBJECT FOR THIS FORM ----------
    public AccountGroup ObjAccountGroup { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    #endregion
}