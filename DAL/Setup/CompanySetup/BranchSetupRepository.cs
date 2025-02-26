using DatabaseModule.CloudSync;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

// ReSharper disable All
namespace MrDAL.Setup.CompanySetup;

public class BranchSetupRepository : IBranchSetupRepository
{
    public BranchSetupRepository()
    {
        BranchSetup = new Branch();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
    }
    public int SaveBranch(string actionTag, bool IsSync = true)
    {
        var cmdString = new StringBuilder();
        if (actionTag == "DELETE")
        {
            cmdString.Append($"Delete from AMS.Branch where Branch_Id = '{BranchSetup.Branch_ID}'; \n");
        }
        if (actionTag is "SAVE" or "NEW")
        {
            cmdString.Append(@" 
                INSERT INTO AMS.Branch(Branch_ID, NepaliDesc, Branch_Name, Branch_Code, Reg_Date, Address, Country, State, City, PhoneNo, Fax, 
                Email, ContactPerson, ContactPersonAdd, ContactPersonPhone, Created_By, Created_Date, Modify_By, Modify_Date, 
                SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append($"\n VALUES( {BranchSetup.Branch_ID},");
            cmdString.Append(BranchSetup.NepaliDesc.IsValueExits() ? $" '{BranchSetup.NepaliDesc.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.Branch_Name.IsValueExits() ? $" '{BranchSetup.Branch_Name.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.Branch_Code.IsValueExits() ? $" '{BranchSetup.Branch_Code.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.Reg_Date.IsValueExits() ? $" '{Convert.ToDateTime(BranchSetup.Reg_Date):yyyy-MM-dd}'," : "NUll,");
            cmdString.Append(BranchSetup.Address.IsValueExits() ? $" '{BranchSetup.Address.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.Country.IsValueExits() ? $" '{BranchSetup.Country.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.State.IsValueExits() ? $" '{BranchSetup.State.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.City.IsValueExits() ? $" '{BranchSetup.City.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.PhoneNo.IsValueExits() ? $" '{BranchSetup.PhoneNo.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.Fax.IsValueExits() ? $" '{BranchSetup.Fax.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.Email.IsValueExits() ? $" '{BranchSetup.Email.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.ContactPerson.IsValueExits() ? $" '{BranchSetup.ContactPerson.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.ContactPersonAdd.IsValueExits() ? $" '{BranchSetup.ContactPersonAdd.GetTrimReplace()}'," : "NUll,");
            cmdString.Append(BranchSetup.ContactPersonPhone.IsValueExits() ? $" '{BranchSetup.ContactPersonPhone.Trim()}'," : "NUll,");
            cmdString.Append(ObjGlobal.LogInUser.IsValueExits() ? $" '{ObjGlobal.LogInUser}'," : "'MrSolution', ");
            cmdString.Append($" GETDATE(),NUll,NUll,NULL,NULL,NULL,NULL,NULL,{BranchSetup.SyncRowVersion}); \n");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.Branch set ");
            cmdString.Append(BranchSetup.Branch_Name.IsValueExits() ? $"Branch_Name= '{BranchSetup.Branch_Name.GetTrimReplace()}'," : "Branch_Name=NUll,");
            cmdString.Append(BranchSetup.Branch_Code.IsValueExits() ? $"Branch_Code=  '{BranchSetup.Branch_Code.GetTrimReplace()}'," : "Branch_Code = NUll,");
            cmdString.Append(BranchSetup.Reg_Date.HasValue ? $" Reg_Date= '{BranchSetup.Reg_Date.GetSystemDate()}'," : "Reg_Date= NUll,");
            cmdString.Append(BranchSetup.Address.IsValueExits() ? $"[Address]=  '{BranchSetup.Address.GetTrimReplace()}'," : "[Address]= NUll,");
            cmdString.Append(BranchSetup.Country.IsValueExits() ? $" Country= '{BranchSetup.Country.GetTrimReplace()}'," : "Country= NUll,");
            cmdString.Append(BranchSetup.State.IsValueExits() ? $"[State]=  '{BranchSetup.State.GetTrimReplace()}'," : "[State]= NUll,");
            cmdString.Append(BranchSetup.City.IsValueExits() ? $"City=  '{BranchSetup.City.GetTrimReplace()}'," : "City= NUll,");
            cmdString.Append(BranchSetup.PhoneNo.IsValueExits() ? $"PhoneNo=  '{BranchSetup.PhoneNo.GetTrimReplace()}'," : "PhoneNo= NUll,");
            cmdString.Append(BranchSetup.Fax.IsValueExits() ? $"Fax=  '{BranchSetup.Fax.GetTrimReplace()}'," : "Fax= NUll,");
            cmdString.Append(BranchSetup.Email.IsValueExits() ? $"Email=  '{BranchSetup.Email.GetTrimReplace()}'," : "Email= NUll,");
            cmdString.Append(BranchSetup.ContactPerson.IsValueExits() ? $"ContactPerson=  '{BranchSetup.ContactPerson.GetTrimReplace()}'," : "ContactPerson= NUll,");
            cmdString.Append(BranchSetup.ContactPersonAdd.IsValueExits() ? $"ContactPersonAdd=  '{BranchSetup.ContactPersonAdd}'," : "ContactPersonAdd= NUll,");
            cmdString.Append(BranchSetup.ContactPersonPhone.IsValueExits() ? $"ContactPersonPhone=  '{BranchSetup.ContactPersonPhone}', " : "ContactPersonPhone= NUll ,");
            cmdString.Append("SyncGlobalId = NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "SyncLastPatchedOn = GETDATE(), " : "SyncLastPatchedOn = NULL,");
            cmdString.Append($"SyncRowVersion = {BranchSetup.SyncRowVersion} ");
            cmdString.Append($"   WHERE Branch_ID = '{BranchSetup.Branch_ID}' ");
        }
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }
        if (ObjGlobal.IsOnlineSync && IsSync)
        {
            Task.Run(() => SyncBranchAsync(actionTag));
        }
        return exe;
    }
    public async Task<int> SyncBranchAsync(string actionTag)
    {
        //sync
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
            GetUrl = @$"{_configParams.Model.Item2}Branch/GetBranchByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Branch/InsertBranchList",
            UpdateUrl = @$"{_configParams.Model.Item2}Branch/UpdateBranch"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var branchRepo = DataSyncProviderFactory.GetRepository<Branch>(_injectData);
        var branches = new List<Branch>
        {
            BranchSetup
        };

        // push realtime branch details to server
        await branchRepo.PushNewListAsync(branches);

        // update branch SyncGlobalId to local
        if (branchRepo.GetHashCode() > 0)
        {
            await SyncUpdateBranch(BranchSetup.Branch_ID);
        }
        return branchRepo.GetHashCode();
    }

    public Task<int> SyncUpdateBranch(int branchId)
    {
        var commandText = $@"
            UPDATE AMS.Branch SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (branchId > 0)
        {
            commandText += $" WHERE Branch_ID = {branchId}";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }


    #region ---------- PULL BRANCH  Bug Fixed By Suraj Kumar Ganesh----------
    public async Task<bool> PullBranchServerToClientByRowCounts(IDataSyncRepository<Branch> branchRepository, int callCount)
    {
        try
        {
            //server fetch data
            var pullResponse = await branchRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            //Local Fetch data
            var query = GetBranchScript();
            var dataSetSql = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var branch in pullResponse.List)
            {
                BranchSetup = branch;

                var existData = dataSetSql.Select($"Branch_ID= {branch.Branch_ID}");
                if (existData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    short rowVersionId = existData[0]["SyncRowVersion"].GetShort();

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (branch.SyncRowVersion > rowVersionId)
                    {
                        var result = SaveBranch("UPDATE");
                    }
                }
                else
                {
                    var result = SaveBranch("SAVE", false);
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullBranchServerToClientByRowCounts(branchRepository, callCount);
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> SyncBranchDetails()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}Branch/GetBranchByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Branch/InsertBranchList",
            UpdateUrl = @$"{_configParams.Model.Item2}Branch/UpdateBranch"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var branchRepo = DataSyncProviderFactory.GetRepository<Branch>(_injectData);

        // pull all new branch data
        var pullResponse = await PullBranchServerToClientByRowCounts(branchRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm(false);
            return false;
        }
        // push all new branch data
        var sqlBrQuery = GetBranchScript();
        var queryResponse = await QueryUtils.GetListAsync<Branch>(sqlBrQuery);
        var branches = queryResponse.List.ToList();
        if (branches.Count > 0)
        {
            var pushResponse = await branchRepo.PushNewListAsync(branches);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    #endregion

    public string GetBranchScript(int branchId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Branch b ";
        cmdString += branchId > 0 ? $" WHERE b.SyncGlobalId IS NULL AND b.Branch_Id= {branchId} " : "";
        return cmdString;
    }

    //RETURN DATA IN DATA TABLE
    public DataTable GetMasterBranch(int branchId = 0)
    {
        var cmdString = GetBranchScript(branchId);
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public Branch BranchSetup { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}