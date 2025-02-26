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

public class MembershipSetupRepository : IMembershipSetupRepository
{
    public MembershipSetupRepository()
    {
        MemberShipSetup = new MemberShipSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    public int SaveMembershipSetup(string actionTag)
    {
        actionTag = actionTag.GetUpper();

        var cmdString = new StringBuilder();
        if (actionTag is "SAVE")
        {
            SaveMembershipSetupAuditLog(actionTag);
            cmdString.Append(@" 
                INSERT INTO AMS.MemberShipSetup(MShipId, MemberId, NepaliDesc, MShipDesc, MShipShortName, PhoneNo, PriceTag, LedgerId, EmailAdd, MemberTypeId, BranchId, CompanyUnitId, MValidDate, MExpireDate, EnterBy, EnterDate, ActiveStatus, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append("\n Values \n ");
            cmdString.Append($"({MemberShipSetup.MShipId}, ");
            cmdString.Append($"{MemberShipSetup.MemberId}, ");
            cmdString.Append(MemberShipSetup.NepaliDesc.IsValueExits() ? $"N'{MemberShipSetup.NepaliDesc.GetTrimReplace()}'," : "NULL,");
            cmdString.Append(MemberShipSetup.MShipDesc.IsValueExits() ? $"N'{MemberShipSetup.MShipDesc}'," : "NULL,");
            cmdString.Append(MemberShipSetup.MShipShortName.IsValueExits() ? $"N'{MemberShipSetup.MShipShortName}'," : "NULL,");
            cmdString.Append($"N'{MemberShipSetup.PhoneNo}',");
            cmdString.Append($"'{MemberShipSetup.PriceTag}',");
            cmdString.Append(MemberShipSetup.LedgerId > 0 ? $"{MemberShipSetup.LedgerId}," : "NULL,");
            cmdString.Append($"'{MemberShipSetup.EmailAdd}',");
            cmdString.Append(MemberShipSetup.MemberTypeId > 0 ? $"{MemberShipSetup.MemberTypeId}," : ",");
            cmdString.Append(MemberShipSetup.BranchId > 0 ? $"{MemberShipSetup.BranchId}," : $"{ObjGlobal.SysBranchId},");
            cmdString.Append(MemberShipSetup.CompanyUnitId > 0 ? $"{MemberShipSetup.CompanyUnitId}," : "NULL,");
            cmdString.Append($"'{MemberShipSetup.MValidDate.GetSystemDate()}',");
            cmdString.Append($"'{MemberShipSetup.MExpireDate.GetSystemDate()}',");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(MemberShipSetup.ActiveStatus ? "1," : "0,");
            cmdString.Append(MemberShipSetup.SyncBaseId.IsValueExits() ? $"N'{MemberShipSetup.SyncBaseId}'," : "NULL,");
            cmdString.Append(MemberShipSetup.SyncGlobalId.IsValueExits() ? $"N'{MemberShipSetup.SyncGlobalId}'," : "NULL,");
            cmdString.Append(MemberShipSetup.SyncOriginId.IsValueExits() ? $"N'{MemberShipSetup.SyncOriginId}'," : "NULL,");
            cmdString.Append(MemberShipSetup.SyncCreatedOn.IsValueExits()
                ? $"N'{MemberShipSetup.SyncCreatedOn.GetSystemDate()}',"
                : "NULL,");
            cmdString.Append(MemberShipSetup.SyncLastPatchedOn.IsValueExits()
                ? $"N'{MemberShipSetup.SyncLastPatchedOn.GetSystemDate()}',"
                : "NULL,");
            cmdString.Append($"{MemberShipSetup.SyncRowVersion} ); \n");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.MembershipSetup SET ");
            cmdString.Append($"MemberId = {MemberShipSetup.MemberId},");
            cmdString.Append(MemberShipSetup.NepaliDesc.IsValueExits()
                ? $"NepaliDesc =N'{MemberShipSetup.NepaliDesc}',"
                : "NepaliDesc =NULL,");
            cmdString.Append(MemberShipSetup.MShipDesc.IsValueExits()
                ? $"MShipDesc =N'{MemberShipSetup.MShipDesc}',"
                : "MShipDesc =NULL,");
            cmdString.Append(MemberShipSetup.MShipShortName.IsValueExits()
                ? $"MShipShortName =N'{MemberShipSetup.MShipShortName}',"
                : "MShipShortName =NULL,");
            cmdString.Append($"PrimaryGrp =N'{MemberShipSetup.PrimaryGrp}',");
            cmdString.Append($" PhoneNo= N'{MemberShipSetup.PhoneNo}',");
            cmdString.Append($"PriceTag='{MemberShipSetup.PriceTag}',");
            cmdString.Append(MemberShipSetup.LedgerId > 0 ? $"LedgerId= N'{MemberShipSetup.LedgerId}," : " LedgerId =NULL,");
            cmdString.Append($"EmailAdd'{MemberShipSetup.EmailAdd}'");
            cmdString.Append(MemberShipSetup.MemberTypeId > 0 ? $"MemberTypeId{MemberShipSetup.MemberTypeId}," : ",");
            cmdString.Append(MemberShipSetup.BranchId > 0 ? $"BranchId{MemberShipSetup.BranchId}," : $"{ObjGlobal.SysBranchId},");
            cmdString.Append(MemberShipSetup.CompanyUnitId > 0 ? $"CompanyUnitId=N'{MemberShipSetup.CompanyUnitId}," : "CompanyUnitId= NULL,");
            cmdString.Append($"MValidDate{MemberShipSetup.MValidDate},");
            cmdString.Append($"MExpireDate'{MemberShipSetup.MExpireDate.GetSystemDate()}',");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(MemberShipSetup.ActiveStatus ? "1," : "0,");
            cmdString.Append(MemberShipSetup.SyncBaseId.IsValueExits()
                ? $"SyncBaseId=N'{MemberShipSetup.SyncBaseId}',"
                : "SyncBaseId=NULL,");
            cmdString.Append(MemberShipSetup.SyncGlobalId.IsValueExits()
                ? $"SyncGlobalId = N'{MemberShipSetup.SyncGlobalId}',"
                : "SyncGlobalId =NULL,");
            cmdString.Append(MemberShipSetup.SyncOriginId.IsValueExits()
                ? $"SyncOriginId =N'{MemberShipSetup.SyncOriginId}',"
                : "SyncOriginId = NULL,");
            cmdString.Append(MemberShipSetup.SyncCreatedOn.IsValueExits()
                ? $" SyncCreatedOn = N'{MemberShipSetup.SyncCreatedOn.GetSystemDate()}',"
                : " SyncCreatedOn = NULL,");
            cmdString.Append(MemberShipSetup.SyncLastPatchedOn.IsValueExits()
                ? $" SyncLastPatchedOn = N'{MemberShipSetup.SyncLastPatchedOn.GetSystemDate()}',"
                : " SyncLastPatchedOn = NULL,");
            cmdString.Append($"SyncRowVersion= N'{MemberShipSetup.SyncRowVersion} ); \n");
        }
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncMembershipSetupAsync(actionTag));
        }

        return exe;
    }
    public async Task<int> SyncMembershipSetupAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}MemberShipSetup/GetMemberShipSetupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}MemberShipSetup/InsertMemberShipSetupList",
            UpdateUrl = @$"{_configParams.Model.Item2}MemberShipSetup/UpdateMemberShipSetup",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var memberShipTypeRepo = DataSyncProviderFactory.GetRepository<MemberShipSetup>(_injectData);
        var memberShipSetups = new List<MemberShipSetup>
        {
            MemberShipSetup
        };
        // push realtime membership setup details to server
        await memberShipTypeRepo.PushNewListAsync(memberShipSetups);

        // update membership setup SyncGlobalId to local
        if (memberShipTypeRepo.GetHashCode() > 0)
        {
            await SyncUpdateMemberShipSetup(MemberShipSetup.MShipId);
        }
        return memberShipTypeRepo.GetHashCode();
    }

    public Task<int> SyncUpdateMemberShipSetup(int mShipId = 0)
    {
        var commandText = $@"
            UPDATE AMS.MemberShipSetup SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (mShipId > 0)
        {
            commandText += $" WHERE MShipId = {mShipId}";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    public int SaveMembershipSetupAuditLog(string actionTag)
    {
        var cmdAudit = @$"
            INSERT INTO AUD.AUDIT_MEMBERSHIPSETUP(MShipId,MShipDesc, MShipShortName,PhoneNo,LedgerId,EmailAdd,MemberTypeId,MemberId, BranchId,CompanyUnitId,MValidDate,EnterBy,EnterDate,ActiveStatus,ModifyAction,ModifyBy,ModifyDate)
            SELECT MShipId,MShipDesc, MShipShortName,PhoneNo,LedgerId,EmailAdd,MemberTypeId,MemberId, BranchId,CompanyUnitId,MValidDate,EnterBy,EnterDate,ActiveStatus,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.MembershipSetup
            WHERE MShipId = {MemberShipSetup.MShipId}";
        var exe = SqlExtensions.ExecuteNonQuery(cmdAudit);
        return exe;
    }
    public async Task<bool> PullMembershipSetupServerToClientByRowCount(IDataSyncRepository<MemberShipSetup> membershipSetupRepo, int callCount)
    {
        try
        {
            var pullResponse = await membershipSetupRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetMembershipSetupScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var membershipData in pullResponse.List)
            {
                MemberShipSetup = membershipData;

                var alreadyExistData = alldata.Select("MShipId= " + membershipData.MShipId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (membershipData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveMembershipSetup("UPDATE");
                    }
                }
                else
                {
                    var result = SaveMembershipSetup("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMembershipSetupServerToClientByRowCount(membershipSetupRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public string GetMembershipSetupScript(int membershipId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.MemberShipSetup";
        cmdString += membershipId > 0 ? $" WHERE SyncGlobalId IS NULL AND MShipId= {membershipId} " : "";
        return cmdString;
    }
    // OBJECT FOR THIS FORM
    public MemberShipSetup MemberShipSetup { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}