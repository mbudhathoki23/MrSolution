using DatabaseModule.CloudSync;
using DatabaseModule.Setup.CompanyMaster;
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

namespace MrDAL.Setup.CompanySetup;

public class CompanyUnitSetupRepository : ICompanyUnitSetupRepository
{
    // INSERT UPDATE DELETE
    public int SaveCompanyUnit(string actionTag, bool IsSync = true)
    {
        var cmdString = new StringBuilder();
        switch (actionTag.ToUpper())
        {
            case "DELETE":
            {
                cmdString.Append($"Delete from AMS.CompanyUnit where CmpUnit_Id = {CompanyUnitSetup.CmpUnit_ID}");
                break;
            }
            case "SAVE":
            {
                cmdString.Append(
                    "INSERT INTO AMS.CompanyUnit (CmpUnit_ID, CmpUnit_Name, CmpUnit_Code, Reg_Date, Address, Country, State, City, PhoneNo, Fax, Email, ContactPerson, ContactPersonAdd, ContactPersonPhone, Branch_ID, Created_By, Created_Date, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
                cmdString.Append($"Values ({CompanyUnitSetup.CmpUnit_ID},");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.CmpUnit_Name.Trim())
                    ? $"'{CompanyUnitSetup.CmpUnit_Name.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.CmpUnit_Code.Trim())
                    ? $"'{CompanyUnitSetup.CmpUnit_Code.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Reg_Date.ToString().Trim())
                    ? $" '{Convert.ToDateTime(CompanyUnitSetup.Reg_Date):yyyy-MM-dd}',"
                    : "NUll,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Address.Trim())
                    ? $"'{CompanyUnitSetup.Address.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Country.Trim())
                    ? $"'{CompanyUnitSetup.Country.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.State.Trim())
                    ? $"'{CompanyUnitSetup.State.GetTrimReplace()}',"
                    : " NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.City.Trim())
                    ? $"'{CompanyUnitSetup.City.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.PhoneNo.Trim())
                    ? $"'{CompanyUnitSetup.PhoneNo.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Fax.Trim())
                    ? $"'{CompanyUnitSetup.Fax.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Email.Trim())
                    ? $"'{CompanyUnitSetup.Email.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.ContactPerson.Trim())
                    ? $"'{CompanyUnitSetup.ContactPerson.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.ContactPersonAdd.Trim())
                    ? $"'{CompanyUnitSetup.ContactPersonAdd.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.ContactPersonPhone.Trim())
                    ? $"'{CompanyUnitSetup.ContactPersonPhone.GetTrimReplace()}',"
                    : "NULL,");
                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
                cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync
                    ? ObjGlobal.LocalOriginId.HasValue
                        ? $" '{ObjGlobal.LocalOriginId}',"
                        : "NULL,"
                    : "NULL,");
                cmdString.Append($"GetDate(),GetDate(),{CompanyUnitSetup.SyncRowVersion} )");
                break;
            }
            case "UPDATE":
            {
                cmdString.Append(" UPDATE AMS.CompanyUnit SET ");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.CmpUnit_Name.Trim()) ? $"CmpUnit_Name = '{CompanyUnitSetup.CmpUnit_Name.GetTrimReplace()}'," : "CmpUnit_Name = NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.CmpUnit_Code.Trim()) ? $"CmpUnit_Code = '{CompanyUnitSetup.CmpUnit_Code.GetTrimReplace()}'," : "CmpUnit_Code = NULL,");
                cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Reg_Date.ToString().Trim()) ? $"Reg_Date = '{Convert.ToDateTime(CompanyUnitSetup.Reg_Date):yyyy-MM-dd}'," : "Reg_Date = NULL,");
                cmdString.Append(CompanyUnitSetup.Address.IsValueExits() ? $"[Address]=  '{CompanyUnitSetup.Address.GetTrimReplace()}'," : "[Address]= NUll,");
                cmdString.Append(CompanyUnitSetup.Country.IsValueExits() ? $" Country= '{CompanyUnitSetup.Country.GetTrimReplace()}'," : "Country= NUll,");
                cmdString.Append(CompanyUnitSetup.State.IsValueExits() ? $"[State]=  '{CompanyUnitSetup.State.GetTrimReplace()}'," : "[State]= NUll,");
                cmdString.Append(CompanyUnitSetup.City.IsValueExits() ? $"City=  '{CompanyUnitSetup.City.GetTrimReplace()}'," : "City= NUll,");
                cmdString.Append(CompanyUnitSetup.PhoneNo.IsValueExits() ? $"PhoneNo=  '{CompanyUnitSetup.PhoneNo.GetTrimReplace()}'," : "PhoneNo= NUll,");
                cmdString.Append(CompanyUnitSetup.Fax.IsValueExits() ? $"Fax=  '{CompanyUnitSetup.Fax.GetTrimReplace()}'," : "Fax= NUll,");
                cmdString.Append(CompanyUnitSetup.Email.IsValueExits() ? $"Email=  '{CompanyUnitSetup.Email.GetTrimReplace()}'," : "Email= NUll,");
                cmdString.Append(CompanyUnitSetup.ContactPerson.IsValueExits() ? $"ContactPerson=  '{CompanyUnitSetup.ContactPerson.GetTrimReplace()}'," : "ContactPerson= NUll,");
                cmdString.Append(CompanyUnitSetup.ContactPersonAdd.IsValueExits() ? $"ContactPersonAdd=  '{CompanyUnitSetup.ContactPersonAdd}'," : "ContactPersonAdd= NUll,");
                cmdString.Append(CompanyUnitSetup.ContactPersonPhone.IsValueExits() ? $"ContactPersonPhone=  '{CompanyUnitSetup.ContactPersonPhone}', " : "ContactPersonPhone= NUll ,");
                cmdString.Append($"Branch_ID = {CompanyUnitSetup.Branch_ID},");
                cmdString.Append("SyncGlobalId = NULL,");
                cmdString.Append("SyncLastPatchedOn = GETDATE(),");
                cmdString.Append($" SyncRowVersion =  {CompanyUnitSetup.SyncRowVersion} \n");
                cmdString.Append($" WHERE CmpUnit_ID = {CompanyUnitSetup.CmpUnit_ID};");

                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Address.Trim()) ? $"Address = '{CompanyUnitSetup.Address.GetTrimReplace()}'," : "Address = NULL,");
                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Country.Trim()) ? $"Country = '{CompanyUnitSetup.Country.GetTrimReplace()}'," : "Country = NULL,");
                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.PhoneNo.Trim()) ? $"PhoneNo ='{CompanyUnitSetup.PhoneNo.GetTrimReplace()}'," : "PhoneNo = NULL,");
                //cmdString.Append($"Branch_ID = {ObjUnit.Branch_ID} ? $"Branch_ID ='{ObjUnit.Branch_ID}'," : "Branch_ID = NULL,");

                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Fax.Trim()) ? $"Fax ='{CompanyUnitSetup.Fax.GetTrimReplace()}'," : "Fax = NULL,");
                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.Email.Trim()) ? $"Email ='{CompanyUnitSetup.Email.GetTrimReplace()}'," : "Email = NULL,");


                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.ContactPerson.Trim()) ? $"ContactPerson ='{CompanyUnitSetup.ContactPerson.GetTrimReplace()}'," : "ContactPerson = NULL,");

                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.ContactPersonAdd.Trim())
                //     ? $"ContactPersonAdd ='{CompanyUnitSetup.ContactPersonAdd.GetTrimReplace()}',"
                //     : "ContactPersonAdd = NULL,");

                // cmdString.Append(!string.IsNullOrEmpty(CompanyUnitSetup.ContactPersonPhone.Trim())
                //     ? $"ContactPersonPhone ='{CompanyUnitSetup.ContactPersonPhone.GetTrimReplace()}',"
                //     : "ContactPersonPhone = NULL,");

                break;
            }
        }

        var result = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (result <= 0)
        {
            return result;
        }

        if (ObjGlobal.IsOnlineSync && IsSync)
        {
            Task.Run(() => SyncCompanyUnitAsync(actionTag));
        }
        return result;
    }

    public async Task<int> SyncCompanyUnitAsync(string actionTag)
    {
        //sync
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}CompanyUnit/GetCompanyUnitByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}CompanyUnit/InsertCompanyUnitList",
            UpdateUrl = @$"{_configParams.Model.Item2}CompanyUnit/UpdateCompanyUnit"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var companyUnitRepo = DataSyncProviderFactory.GetRepository<CompanyUnit>(_injectData);
        var companyUnits = new List<CompanyUnit>
        {
            CompanyUnitSetup
        };
        await companyUnitRepo.PushNewListAsync(companyUnits);
        if (companyUnitRepo.GetHashCode() > 0)
        {
            await SyncUpdateCompanyUnit(CompanyUnitSetup.CmpUnit_ID);
        }
        return companyUnitRepo.GetHashCode();
    }

    public async Task<bool> SyncCompanyUnitDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}CompanyUnit/GetCompanyUnitByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}CompanyUnit/InsertCompanyUnitList",
            UpdateUrl = @$"{_configParams.Model.Item2}CompanyUnit/UpdateCompanyUnit"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var companyUnitRepo = DataSyncProviderFactory.GetRepository<CompanyUnit>(_injectData);
        // pull all new company unit data
        var pullResponse = await PullCompanyUnitServerToClientByRowCounts(companyUnitRepo, 1);
        if (!pullResponse)
        {
            //SplashScreenManager.CloseForm();
            return false;
        }

        // push all new company unit data
        var sqlcuQuery = GetCompanyUnitScript();
        var queryResponse = await QueryUtils.GetListAsync<CompanyUnit>(sqlcuQuery);
        var companyUnits = queryResponse.List.ToList();
        if (companyUnits.Count > 0)
        {
            var pushResponse = await companyUnitRepo.PushNewListAsync(companyUnits);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateCompanyUnit(int companyUnitID)
    {
        var commandText = $@"
            UPDATE AMS.CompanyUnit SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (companyUnitID > 0)
        {
            commandText += $" WHERE CmpUnit_ID = '{companyUnitID}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    // PULL BRANCH
    public async Task<bool> PullCompanyUnitServerToClientByRowCounts(IDataSyncRepository<CompanyUnit> companyUnitRepository, int callCount)
    {
        try
        {
            var pullResponse = await companyUnitRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetCompanyUnitScript();
            var dataSetSql = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var company in pullResponse.List)
            {
                CompanyUnitSetup = company;

                var existData = dataSetSql.Select($"CmpUnit_ID= {company.CmpUnit_ID}");
                if (existData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    short rowVersionId = existData[0]["SyncRowVersion"].GetShort();

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (company.SyncRowVersion > rowVersionId)
                    {
                        CompanyUnitSetup = company;
                        var result = SaveCompanyUnit("UPDATE");
                    }
                }
                else
                {
                    var result = SaveCompanyUnit("SAVE", false);
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullCompanyUnitServerToClientByRowCounts(companyUnitRepository, callCount);
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string GetCompanyUnitScript(int cmpunit_ID = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.CompanyUnit WHERE SyncGlobalId IS NULL";
        cmdString += cmpunit_ID > 0 ? $" AND CmpUnit_ID= {cmpunit_ID} " : "";
        return cmdString;
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable CheckMasterValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $"Select * From {tableName} where {whereValue}='{inputTxt}'";
        if (selectedId.GetInt() > 0 && actionTag != "SAVE")
        {
            cmdString += $" and {validId} <> {selectedId} ";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public CompanyUnit CompanyUnitSetup { get; set; } = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    private DbSyncRepoInjectData _injectData = new();

}