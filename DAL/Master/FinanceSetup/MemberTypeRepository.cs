using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MrDAL.Master.FinanceSetup;

public class MemberTypeRepository : IMemberTypeRepository
{

    public MemberTypeRepository()
    {
        ObjMemberType = new MemberType();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    public int SaveMemberType(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() is "DELETE")
        {
            cmdString.Append($"Delete from AMS.MemberType where MemberTypeId = {ObjMemberType.MemberTypeId}");
        }

        switch (actionTag.ToUpper())
        {
            case "SAVE":
            {
                cmdString.Append(@"
                        INSERT INTO AMS.MemberType (MemberTypeId, MemberDesc, MemberShortName, Discount, BranchId, CompanyUnitId, EnterBy, EnterDate, ActiveStatus, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
                cmdString.Append("\n VALUES \n");
                cmdString.Append($" ({ObjMemberType.MemberTypeId}, ");
                cmdString.Append($"N'{ObjMemberType.MemberDesc.GetTrimReplace()}',");
                cmdString.Append($"N'{ObjMemberType.MemberShortName}',");
                cmdString.Append(ObjMemberType.Discount > 0 ? $"{ObjMemberType.Discount}," : "0,");
                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
                cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(), 1,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                    ? $" '{ObjGlobal.LocalOriginId}',"
                    : "NULL,");
                cmdString.Append($"GetDate(),GetDate(),{ObjMemberType.SyncRowVersion} , ");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID())" : "NULL)");
                break;
            }
            case "UPDATE":
            {
                cmdString.Append("UPDATE AMS.MemberType SET ");
                cmdString.Append(!string.IsNullOrEmpty(ObjMemberType.MemberDesc.Trim())
                    ? $"MemberDesc = N'{ObjMemberType.MemberDesc.GetTrimReplace()}',"
                    : "MemberDesc = NULL,");
                cmdString.Append(!string.IsNullOrEmpty(ObjMemberType.MemberShortName.Trim())
                    ? $"MemberShortName = N'{ObjMemberType.MemberShortName.GetTrimReplace()}',"
                    : "MemberShortName = NULL,");
                cmdString.Append(ObjGlobal.ReturnDecimal(ObjMemberType.Discount.ToString()) > 0
                    ? $"Discount = N'{ObjMemberType.Discount}',"
                    : "Discount = 0,");
                cmdString.Append(ObjMemberType.ActiveStatus ? "ActiveStatus = 1," : "ActiveStatus = 0,");
                cmdString.Append("SyncLastPatchedOn = GETDATE(),");
                cmdString.Append($"SyncRowVersion = {ObjMemberType.SyncRowVersion}");
                cmdString.Append($"where MemberTypeId = {ObjMemberType.MemberTypeId}");
                break;
            }
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0 && ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncMemberTypeAsync(actionTag));
        }

        return exe;
    }

    public async Task<int> SyncMemberTypeAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}MemberType/GetMemberTypesByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}MemberType/InsertMemberTypeList",
            UpdateUrl = @$"{_configParams.Model.Item2}MemberType/UpdateMemberType",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var memberTypeRepo = DataSyncProviderFactory.GetRepository<MemberType>(_injectData);
        var memberTypes = new List<MemberType>
        {
            ObjMemberType
        };
        // push realtime member type details to server
        await memberTypeRepo.PushNewListAsync(memberTypes);

        // update member type SyncGlobalId to local
        if (memberTypeRepo.GetHashCode() > 0)
        {
            await SyncUpdateMemberType(ObjMemberType.MemberTypeId);
        }
        return memberTypeRepo.GetHashCode();
    }

    public Task<int> SyncUpdateMemberType(int memberTypeId = 0)
    {
        var commandText = $@"
            UPDATE AMS.MemberType SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (memberTypeId > 0)
        {
            commandText += $" WHERE MemberTypeId = '{memberTypeId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    public async Task<bool> PullMainAreasServerToClientByRowCount(IDataSyncRepository<MemberType> memberTypeRepo, int callCount)
    {
        try
        {
            var pullResponse = await memberTypeRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetMemberTypeScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var memberTypeData in pullResponse.List)
            {
                ObjMemberType = memberTypeData;

                var alreadyExistData = alldata.Select("MemberTypeId= " + memberTypeData.MemberTypeId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (memberTypeData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveMemberType("UPDATE");
                    }
                }
                else
                {
                    var result = SaveMemberType("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMainAreasServerToClientByRowCount(memberTypeRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public string GetMemberTypeScript(int memberTypeId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.MemberType";
        cmdString += memberTypeId > 0 ? $" WHERE SyncGlobalId IS NULL AND MemberTypeId= {memberTypeId} " : "";
        return cmdString;
    }

    //OBJECT FOR THIS FORM 
    public MemberType ObjMemberType { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

}