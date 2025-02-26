using DatabaseModule.CloudSync;
using DatabaseModule.Setup.SoftwareRegistration;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Utility.Licensing;

public class SoftwareLicenseRepository : ISoftwareLicenseRepository
{
    public int SaveSoftwareLicense(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "SAVE" or "NEW" or "UPDATE")
        {
            cmdString = @"
                TRUNCATE TABLE AMS.SoftwareRegistration;
                INSERT INTO AMS.SoftwareRegistration (RegistrationId,CustomerId,Server_MacAdd,Server_Desc,ClientDescription,ClientAddress,ClientSerialNo,RequestBy,RegisterBy,RegistrationDate,RegistrationDays,ExpiredDate,ProductDescription,NoOfNodes,Module,System_Id,ActivationCode,IsOnline) ";
            cmdString += " \n VALUES ";
            cmdString += $" ('{Registration.RegistrationId}', N'{Registration.CustomerId.GetLong().GuidFromLong()}', N'{Registration.Server_MacAdd}',";
            cmdString += $" N'{Registration.Server_Desc}', N'{Registration.ClientDescription}', N'{Registration.ClientAddress}', N'{Registration.ClientSerialNo}', N'{Registration.RequestBy}',";
            cmdString += $" N'{Registration.RegisterBy}', N'{Registration.RegistrationDate}', N'{Registration.RegistrationDays}', N'{Registration.ExpiredDate}', N'{Registration.ProductDescription}',";
            cmdString += $" N'{Registration.NoOfNodes}', N'{Registration.Module}', N'{Registration.System_Id}', N'{Registration.ActivationCode}', CAST('{Registration.IsOnline}' AS BIT)); ";

        }
        else if (tag is "UPDATE")
        {
            cmdString = @$"
                UPDATE AMS.SoftwareRegistration SET ExpiredDate=N'{Registration.ExpiredDate}',IsOnline = CAST('{Registration.IsOnline}' AS BIT) 
                WHERE RegistrationId = '{Registration.RegistrationId}';";
        }
        var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        if (result != 0)
        {
            //InitialLicenseInfo(tag);
            if (Registration.IrdRegistration)
            {
                cmdString = "UPDATE AMS.CompanyInfo SET IsTaxRegister=1;";
                SqlExtensions.ExecuteNonQuery(cmdString);
            }
            
        }
        return result;
    }

    private void InitialLicenseInfo(string tag)
    {
        License.LicenseId = Guid.NewGuid();

        License.InstalDate = Registration.RegistrationDate;
        License.RegistrationId = Registration.RegistrationId;
        License.RegistrationDate = Registration.RegistrationDate;

        License.SerialNo = DateTime.Now.GuidFromDateTime();

        License.ActivationId = Registration.CustomerId.GetLong().GuidFromLong();
        License.LicenseExpireDate = Registration.ExpiredDate;

        var expiry = Registration.ExpiredDate;
        var expiryDate = ObjGlobal.Decrypt(expiry).GetDateTime();
        License.ExpireGuid = expiryDate.GuidFromDateTime();

        License.LicenseTo = Registration.ClientDescription;
        License.RegAddress = Registration.ClientAddress;
        License.ParentCompany = ObjGlobal.CompanyAddress;
        License.ParentAddress = ObjGlobal.CompanyAddress;
        License.CustomerId = Registration.CustomerId;
        License.ServerMacAddress = Registration.Server_MacAdd;
        License.ServerName = Registration.Server_Desc;
        License.RequestBy = Registration.RequestBy;
        License.RegisterBy = Registration.RegisterBy;
        License.LicenseDays = Registration.RegistrationDays;
        License.LicenseModule = Registration.Module;
        License.NodesNo = Registration.NoOfNodes;
        License.SystemId = Registration.System_Id;
        SaveLicenseInfo(tag);
    }
    public int SaveLicenseInfo(string tag)  
    {
        var cmdString = @$"
            TRUNCATE TABLE AMS.LicenseInfo;
            INSERT INTO AMS.LicenseInfo (LicenseId,InstalDate,RegistrationId,RegistrationDate,SerialNo,ActivationId,LicenseExpireDate,ExpireGuid,LicenseTo,RegAddress,ParentCompany,ParentAddress,CustomerId,ServerMacAddress,ServerName,RequestBy,RegisterBy,LicenseDays,LicenseModule,NodesNo,SystemId)
            VALUES('{License.LicenseId}',N'{License.InstalDate}','{License.RegistrationId}',N'{License.RegistrationDate}',";
        cmdString += @$" '{License.SerialNo}',N'{License.ActivationId}','{License.LicenseExpireDate}',N'{License.ExpireGuid}',";
        cmdString += @$" N'{License.LicenseTo}',N'{License.RegAddress}',N'{License.ParentCompany}',N'{License.ParentAddress}',";
        cmdString += @$" N'{License.CustomerId}',N'{License.ServerMacAddress}',N'{License.ServerName}',N'{License.RequestBy}',N'{License.RegisterBy}',";
        cmdString += @$" N'{License.LicenseDays}',N'{License.LicenseModule}',N'{License.NodesNo}','{License.SystemId}'); ";
        var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        return result;
    }

    //sync
    public async Task<bool> SyncLicenseRegistration()
    {
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            {
                configParams.Model = new ValueModel<string, string, Guid>
                {
                    Item1 = string.Empty,
                    Item2 = ObjGlobal.CloudSyncBaseUrl,
                    Item3 = Guid.NewGuid(),
                };
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

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}License/GetLicenseInfoByCallCount",
                InsertUrl = @$"{configParams.Model.Item2}License/InsertLicenseInfo",
                UpdateUrl = @$"{configParams.Model.Item2}License/UpdateLicenseInfo",
            };


            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var syncRepository = DataSyncProviderFactory.GetRepository<LicenseInfo>(injectData);

            var sr = License;
            var result = _actionTag.ToUpper() switch
            {
                "NEW" => await syncRepository?.PushNewAsync(sr)!,
                "UPDATE" => await syncRepository?.PutNewAsync(sr)!,
                _ => await syncRepository?.PushNewAsync(sr)!
            };

            return result.GetInt() > 0;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable ReturnSoftwareRegistrationHistory()
    {
        var cmdString = $"SELECT TOP (1) * FROM AMS.SoftwareRegistration WHERE Server_MacAdd ='{ObjGlobal.Encrypt(ObjGlobal.SystemMacAddress)}' OR ClientSerialNo='{ObjGlobal.Encrypt(ObjGlobal.SystemSerialNo)}' ORDER BY RegistrationId";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable ReturnLicenseInfoHistory()
    {
        var cmdString = $"SELECT TOP (1) * FROM AMS.LicenseInfo WHERE ServerMacAddress='{ObjGlobal.Encrypt(ObjGlobal.SystemMacAddress)}' ORDER BY LicenseId";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    // OBJECT FOR SOFTWARE
    public SoftwareRegistration Registration { get; set; } = new();
    public LicenseInfo License { get; set; } = new();

    private readonly string _actionTag = string.Empty;
}