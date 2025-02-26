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

namespace MrDAL.Master.LedgerSetup;

public class GeneralLedgerRepository : IGeneralLedgerRepository
{
    public GeneralLedgerRepository()
    {
        ObjGeneralLedger = new GeneralLedger();

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public int SaveGeneralLedger(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag == "DELETE")
        {
            SaveGeneralLedgerAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.GeneralLedger  where GLID ='{ObjGeneralLedger.GLID}'");
        }

        switch (actionTag)
        {
            case "SAVE":
            {
                cmdString.Append(@"  INSERT INTO AMS.GeneralLedger(GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
                cmdString.Append($"\n VALUES( {ObjGeneralLedger.GLID},");
                cmdString.Append(ObjGeneralLedger.NepaliDesc.IsValueExits() ? $" N'{ObjGeneralLedger.NepaliDesc.GetTrimReplace()}'," : "NULL,");
                cmdString.Append($"N'{ObjGeneralLedger.GLName.GetTrimReplace()}',N'{ObjGeneralLedger.GLCode.GetTrimReplace()}',");
                cmdString.Append(ObjGeneralLedger.ACCode.Trim().Length > 0
                    ? $" N'{ObjGeneralLedger.ACCode.Trim()}',"
                    : $"N'{ObjGeneralLedger.GLCode.Trim()}',");
                cmdString.Append(ObjGeneralLedger.GLType.Trim().Length > 0
                    ? $" N'{ObjGeneralLedger.GLType}',"
                    : "'Other',");
                cmdString.Append(ObjGeneralLedger.GrpId > 0 ? $" {ObjGeneralLedger.GrpId}," : " NULL,");
                cmdString.Append(
                    ObjGeneralLedger.PrimaryGroupId > 0 ? $" {ObjGeneralLedger.PrimaryGroupId}," : " NULL,");
                cmdString.Append(ObjGeneralLedger.SubGrpId > 0 ? $"{ObjGeneralLedger.SubGrpId}," : " NULL,");
                cmdString.Append(ObjGeneralLedger.PrimarySubGroupId > 0
                    ? $"{ObjGeneralLedger.PrimarySubGroupId},"
                    : " NULL,");
                cmdString.Append(ObjGeneralLedger.PanNo.IsValueExits()
                    ? $"  '{ObjGeneralLedger.PanNo.GetTrimReplace()}',"
                    : "Null,");
                cmdString.Append(ObjGeneralLedger.AreaId > 0 ? $"{ObjGeneralLedger.AreaId}," : " NULL,");
                cmdString.Append(ObjGeneralLedger.AgentId > 0 ? $"{ObjGeneralLedger.AgentId}," : " NULL,");
                cmdString.Append(ObjGeneralLedger.CurrId > 0
                    ? $"{ObjGeneralLedger.CurrId},"
                    : $" {ObjGlobal.SysCurrencyId} ,");
                cmdString.Append(ObjGeneralLedger.CrDays > 0 ? $" '{ObjGeneralLedger.CrDays}'," : "0,");
                cmdString.Append($"{ObjGeneralLedger.CrLimit.GetDecimal()},");
                cmdString.Append(ObjGeneralLedger.CrTYpe.IsValueExits()
                    ? $"  '{ObjGeneralLedger.CrTYpe.Trim()}',"
                    : "'I',");
                cmdString.Append($"{ObjGeneralLedger.IntRate.GetDecimal()},");
                cmdString.Append(ObjGeneralLedger.GLAddress.IsValueExits()
                    ? $"  '{ObjGeneralLedger.GLAddress.GetTrimReplace()}',"
                    : "Null,");
                cmdString.Append(ObjGeneralLedger.PhoneNo.IsValueExits()
                    ? $"  '{ObjGeneralLedger.PhoneNo.Trim()}',"
                    : "NULL,");
                cmdString.Append(ObjGeneralLedger.LandLineNo.IsValueExits()
                    ? $"  '{ObjGeneralLedger.LandLineNo}',"
                    : "NULL,");
                cmdString.Append(ObjGeneralLedger.OwnerName.IsValueExits()
                    ? $"  '{ObjGeneralLedger.OwnerName.GetTrimReplace()}',"
                    : "Null,");
                cmdString.Append(ObjGeneralLedger.OwnerNumber.IsValueExits()
                    ? $"  '{ObjGeneralLedger.OwnerNumber.Trim()}',"
                    : "NULL,");
                cmdString.Append(ObjGeneralLedger.Scheme > 0 ? $"  '{ObjGeneralLedger.Scheme}'," : "NULL,");
                cmdString.Append(ObjGeneralLedger.Email.IsValueExits()
                    ? $"  '{ObjGeneralLedger.Email.Trim()}',"
                    : "NULL,");
                cmdString.Append($" {ObjGlobal.SysBranchId},");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" {ObjGlobal.SysCompanyUnitId}, " : "Null,");
                cmdString.Append($" '{ObjGlobal.LogInUser.ToUpper()}', GETDATE(),");
                cmdString.Append(ObjGeneralLedger.Status ? "1," : "0,");
                cmdString.Append("0,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                    ? $"'{ObjGlobal.LocalOriginId}',"
                    : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                cmdString.Append($"{ObjGeneralLedger.SyncRowVersion} ); ");
                break;
            }
            case "UPDATE":
            {
                cmdString.Append("UPDATE AMS.GeneralLedger SET ");
                cmdString.Append($"GLName= '{ObjGeneralLedger.GLName.GetTrimReplace()}',");
                cmdString.Append($"GLCode= '{ObjGeneralLedger.GLCode.GetTrimReplace()}',");
                cmdString.Append($"ACCode='{ObjGeneralLedger.ACCode.GetTrimReplace()}',");
                cmdString.Append($"GLType= '{ObjGeneralLedger.GLType.Trim()}',");
                cmdString.Append(ObjGeneralLedger.GrpId > 0 ? $"GrpId= '{ObjGeneralLedger.GrpId}'," : " GrpId= Null,");
                cmdString.Append(ObjGeneralLedger.SubGrpId > 0
                    ? $"SubGrpId ='{ObjGeneralLedger.SubGrpId}',"
                    : " SubGrpId = Null,");
                cmdString.Append(ObjGeneralLedger.PanNo.IsValueExits()
                    ? $"PanNo='{ObjGeneralLedger.PanNo.Trim()}',"
                    : " PanNo = Null,");
                cmdString.Append(
                    ObjGeneralLedger.AreaId > 0 ? $"AreaId= '{ObjGeneralLedger.AreaId}'," : "AreaId= Null,");
                cmdString.Append(ObjGeneralLedger.AgentId > 0
                    ? $"AgentId= '{ObjGeneralLedger.AgentId}',"
                    : " AgentId=Null,");
                cmdString.Append(ObjGeneralLedger.CurrId > 0
                    ? $"CurrId = '{ObjGeneralLedger.CurrId}',"
                    : "CurrId= Null,");
                cmdString.Append(ObjGeneralLedger.CrDays > 0
                    ? $" CrDays = '{ObjGeneralLedger.CrDays}',"
                    : " CrDays = 0,");
                cmdString.Append(ObjGeneralLedger.CrLimit > 0
                    ? $" CrLimit= '{ObjGeneralLedger.CrLimit}',"
                    : "CrLimit = 0,");
                cmdString.Append(ObjGeneralLedger.CrTYpe.IsValueExits()
                    ? $"CrTYpe= '{ObjGeneralLedger.CrTYpe.Trim()}',"
                    : "CrTYpe='Ignore',");
                cmdString.Append(ObjGeneralLedger.IntRate > 0
                    ? $" IntRate= '{ObjGeneralLedger.IntRate}',"
                    : "IntRate= 0,");
                cmdString.Append(ObjGeneralLedger.GLAddress.IsValueExits()
                    ? $"GLAddress= '{ObjGeneralLedger.GLAddress.GetTrimReplace()}',"
                    : "GLAddress=NULL,");
                cmdString.Append(ObjGeneralLedger.PhoneNo.IsValueExits()
                    ? $"PhoneNo= '{ObjGeneralLedger.PhoneNo.Trim()}',"
                    : "PhoneNo=NULL, ");
                cmdString.Append(ObjGeneralLedger.LandLineNo.IsValueExits()
                    ? $"LandLineNo= '{ObjGeneralLedger.LandLineNo.Trim()}',"
                    : "LandLineNo=NULL, ");
                cmdString.Append(ObjGeneralLedger.OwnerName.IsValueExits()
                    ? $"OwnerName= '{ObjGeneralLedger.OwnerName.Trim()}',"
                    : "OwnerName=NULL, ");
                cmdString.Append(ObjGeneralLedger.OwnerNumber.IsValueExits()
                    ? $"OwnerNumber= '{ObjGeneralLedger.OwnerNumber.Trim()}',"
                    : "OwnerNumber=NULL, ");
                cmdString.Append(
                    ObjGeneralLedger.Scheme > 0 ? $"Scheme= '{ObjGeneralLedger.Scheme}'," : "Scheme=NULL, ");
                cmdString.Append(ObjGeneralLedger.Email.IsValueExits()
                    ? $"Email=  '{ObjGeneralLedger.Email.GetTrimReplace()}',"
                    : "Email=NULL, ");
                cmdString.Append($"SyncRowVersion =  {ObjGeneralLedger.SyncRowVersion},");
                cmdString.Append(ObjGlobal.IsOnlineSync
                    ? " SyncLastPatchedOn =  GETDATE(),"
                    : " SyncLastPatchedOn =  NULL,");
                cmdString.Append(ObjGeneralLedger.Status ? " Status = 1" : " Status = 0 ");
                cmdString.Append($" WHERE GLID ='{ObjGeneralLedger.GLID}' ");
                break;
            }
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncGeneralLedgerAsync(actionTag));
        }
        if (actionTag != "DELETE")
        {
            SaveGeneralLedgerAuditLog(actionTag);
        }
        return exe;
    }
    public async Task<int> SyncGeneralLedgerAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}GeneralLedger/GetGeneralLedgersByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}GeneralLedger/InsertGeneralLedgerList",
            UpdateUrl = @$"{_configParams.Model.Item2}GeneralLedger/UpdateGeneralLedger"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var generalLedgerRepo = DataSyncProviderFactory.GetRepository<GeneralLedger>(_injectData);
        var generalLedgers = new List<GeneralLedger>
        {
            ObjGeneralLedger
        };

        // push realtime details to server
        await generalLedgerRepo.PushNewListAsync(generalLedgers);

        // update General Ledger SyncGlobalId to local
        if (generalLedgerRepo.GetHashCode() > 0)
        {
            await SyncUpdateGeneralLedger(ObjGeneralLedger.GLID);
        }
        return generalLedgerRepo.GetHashCode();

        // //sync
        // try
        // {
        //     var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        //     if (!configParams.Success || configParams.Model.Item1 == null)
        //     //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
        //     //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //     //configParams.ShowErrorDialog();
        //     {
        //         return 1;
        //     }
        //
        //     var injectData = new DbSyncRepoInjectData
        //     {
        //         ExternalConnectionString = null,
        //         DateTime = DateTime.Now,
        //         LocalOriginId = configParams.Model.Item1,
        //         LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
        //         Username = ObjGlobal.LogInUser,
        //         LocalConnectionString = GetConnection.ConnectionString,
        //         LocalBranchId = ObjGlobal.SysBranchId,
        //         ApiConfig = DataSyncHelper.GetConfig()
        //     };
        //     var deleteUri = @$"{configParams.Model.Item2}GeneralLedger/DeleteGeneralLedgerAsync?id=" +
        //                     ObjGeneralLedger.GLID;
        //
        //     var apiConfig = new SyncApiConfig
        //     {
        //         BaseUrl = configParams.Model.Item2,
        //         Apikey = configParams.Model.Item3,
        //         Username = ObjGlobal.LogInUser,
        //         BranchId = ObjGlobal.SysBranchId,
        //         GetUrl = @$"{configParams.Model.Item2}GeneralLedger/GetGeneralLedgerById",
        //         InsertUrl = @$"{configParams.Model.Item2}GeneralLedger/InsertGeneralLedger",
        //         UpdateUrl = @$"{configParams.Model.Item2}GeneralLedger/UpdateGeneralLedger",
        //         DeleteUrl = deleteUri
        //     };
        //
        //     DataSyncHelper.SetConfig(apiConfig);
        //     injectData.ApiConfig = apiConfig;
        //     DataSyncManager.SetGlobalInjectData(injectData);
        //
        //     var generalLedgerRepo = DataSyncProviderFactory.GetRepository<GeneralLedger>(DataSyncManager.GetGlobalInjectData());
        //
        //     var gl = new GeneralLedger
        //     {
        //         GLID = ObjGeneralLedger.GLID,
        //         NepaliDesc = ObjGeneralLedger.NepaliDesc.IsValueExits()
        //             ? ObjGeneralLedger.NepaliDesc.GetTrimReplace()
        //             : null,
        //         GLName = ObjGeneralLedger.GLName.GetTrimReplace(),
        //         GLCode = ObjGeneralLedger.GLCode.GetTrimReplace(),
        //         ACCode = ObjGeneralLedger.ACCode.Trim().Length > 0
        //             ? ObjGeneralLedger.ACCode.Trim()
        //             : ObjGeneralLedger.GLCode.Trim(),
        //         GLType = ObjGeneralLedger.GLType.Trim().Length > 0 ? ObjGeneralLedger.GLType : "Other",
        //         GrpId = ObjGeneralLedger.GrpId > 0 ? ObjGeneralLedger.GrpId : 0,
        //         PrimaryGroupId = ObjGeneralLedger.PrimaryGroupId > 0 ? ObjGeneralLedger.PrimaryGroupId : null,
        //         SubGrpId = ObjGeneralLedger.SubGrpId > 0 ? ObjGeneralLedger.SubGrpId : null,
        //         PrimarySubGroupId = ObjGeneralLedger.PrimarySubGroupId > 0 ? ObjGeneralLedger.PrimarySubGroupId : null,
        //         PanNo = ObjGeneralLedger.PanNo.IsValueExits() ? ObjGeneralLedger.PanNo.GetTrimReplace() : null,
        //         AreaId = ObjGeneralLedger.AreaId > 0 ? ObjGeneralLedger.AreaId : null,
        //         AgentId = ObjGeneralLedger.AgentId > 0 ? ObjGeneralLedger.AgentId : null,
        //         CurrId = ObjGeneralLedger.CurrId > 0 ? ObjGeneralLedger.CurrId : ObjGlobal.SysCurrencyId,
        //         CrDays = ObjGeneralLedger.CrDays > 0 ? ObjGeneralLedger.CrDays : 0,
        //         CrLimit = ObjGeneralLedger.CrLimit.GetDecimal(),
        //         CrTYpe = ObjGeneralLedger.CrTYpe.IsValueExits() ? ObjGeneralLedger.CrTYpe.Trim() : "I",
        //         IntRate = ObjGeneralLedger.IntRate.GetDecimal(),
        //         GLAddress = ObjGeneralLedger.GLAddress.IsValueExits()
        //             ? ObjGeneralLedger.GLAddress.GetTrimReplace()
        //             : null,
        //         PhoneNo = ObjGeneralLedger.PhoneNo.IsValueExits() ? ObjGeneralLedger.PhoneNo.Trim() : null,
        //         LandLineNo = ObjGeneralLedger.LandLineNo.IsValueExits() ? ObjGeneralLedger.LandLineNo : null,
        //         OwnerName = ObjGeneralLedger.OwnerName.IsValueExits()
        //             ? ObjGeneralLedger.OwnerName.GetTrimReplace()
        //             : null,
        //         OwnerNumber = ObjGeneralLedger.OwnerNumber.IsValueExits() ? ObjGeneralLedger.OwnerNumber.Trim() : null,
        //         Scheme = ObjGeneralLedger.Scheme > 0 ? ObjGeneralLedger.Scheme : null,
        //         Email = ObjGeneralLedger.Email.IsValueExits() ? ObjGeneralLedger.Email.Trim() : null,
        //         Branch_ID = ObjGlobal.SysBranchId,
        //         Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
        //         EnterBy = ObjGlobal.LogInUser.ToUpper(),
        //         EnterDate = DateTime.Now,
        //         Status = ObjGeneralLedger.Status,
        //         IsDefault = 0,
        //         SyncRowVersion = ObjGeneralLedger.SyncRowVersion
        //     };
        //     var result = actionTag.ToUpper() switch
        //     {
        //         "SAVE" => await generalLedgerRepo?.PushNewAsync(gl),
        //         "UPDATE" => await generalLedgerRepo?.PutNewAsync(gl),
        //         "DELETE" => await generalLedgerRepo?.DeleteNewAsync(),
        //         _ => await generalLedgerRepo?.PushNewAsync(gl)
        //     };
        //     return 1;
        // }
        // catch (Exception ex)
        // {
        //     return 1;
        // }
    }

    public async Task<bool> SyncGeneralLedgerDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}GeneralLedger/GetGeneralLedgersByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}GeneralLedger/InsertGeneralLedgerList",
            UpdateUrl = @$"{_configParams.Model.Item2}GeneralLedger/UpdateGeneralLedger"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var generalLedgerRepo = DataSyncProviderFactory.GetRepository<GeneralLedger>(_injectData);

        // pull all new general ledger data
        var pullResponse = await PullGeneralLedgersServerToClientByRowCount(generalLedgerRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new general ledger data
        var sqlglQuery = GetGeneralLedgerScript();
        var queryResponse = await QueryUtils.GetListAsync<GeneralLedger>(sqlglQuery);
        var glList = queryResponse.List.ToList();
        if (glList.Count > 0)
        {
            var pushResponse = await generalLedgerRepo.PushNewListAsync(glList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateGeneralLedger(long generalLedgerId = 0)
    {
        var commandText = $@"
            UPDATE AMS.GeneralLedger SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (generalLedgerId > 0)
        {
            commandText += $" WHERE GLID = {generalLedgerId}";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    public int SaveGeneralLedgerAuditLog(string actionTag)
    {
        var cmdString = $@"
		INSERT INTO AUD.AUDIT_GENERAL_LEDGER(GLID, GLName, GLCode, ACCode, GLType, GrpId, SubGrpId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_id, Company_Id, EnterBy, EnterDate, Status, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, ModifyAction, ModifyBy, ModifyDate)
		SELECT GLID, GLName, GLCode, ACCode, GLType, GrpId, SubGrpId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_id, Company_Id, EnterBy, EnterDate, Status, PrimaryGroupId, PrimarySubGroupId, IsDefault, NepaliDesc, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
		  FROM AMS.GeneralLedger gl
		 WHERE gl.GLID='{ObjGeneralLedger.GLID}';";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }
    public string GetGeneralLedgerScript(int generalLedgerId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.GeneralLedger";
        cmdString += generalLedgerId > 0 ? $" WHERE SyncGlobalId IS NULL AND GLID= {generalLedgerId} " : "";
        return cmdString;
    }


    // PULL GENERAL LEDGER 
    #region ---------- GENERAL LEDGER ----------

    public async Task<bool> PullGeneralLedgersServerToClientByRowCount(IDataSyncRepository<GeneralLedger> generalLedgerRepo, int callCount)
    {
        try
        {
            var pullResponse = await generalLedgerRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetGeneralLedgerScript();
            var allData = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var generalLedgerData in pullResponse.List)
            {
                ObjGeneralLedger = generalLedgerData;

                var alreadyExistData = allData.Select("GLID= " + generalLedgerData.GLID + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (generalLedgerData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveGeneralLedger("UPDATE");
                    }
                }
                else
                {
                    var result = SaveGeneralLedger("SAVE");
                }

            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullGeneralLedgersServerToClientByRowCount(generalLedgerRepo, callCount);
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
    public DataTable GetMasterGeneralLedger(string _ActionTag, long selectedId = 0)
    {
        var cmdString = $@"
			SELECT Gl.GLID, Gl.GLName, Gl.GLCode, Gl.ACCode, Gl.GLType, Gl.GrpId, AG.GrpName, Gl.SubGrpId, ASG.SubGrpName, Gl.PanNo, Gl.AreaId, A.AreaName, Gl.AgentId, JA.AgentName, Gl.CurrId, C.CCode, Gl.CrDays, Gl.CrLimit, Gl.CrTYpe, Gl.IntRate, Gl.GLAddress, Gl.PhoneNo, Gl.LandLineNo, Gl.OwnerName, Gl.OwnerNumber, Gl.Scheme, Gl.Email, Gl.Branch_ID, Gl.Company_Id, Gl.EnterBy, Gl.EnterDate, Gl.PrimaryGroupId, Gl.PrimarySubGroupId, Gl.IsDefault, Gl.NepaliDesc, Gl.Status, Gl.SyncBaseId, Gl.SyncGlobalId, Gl.SyncOriginId, Gl.SyncCreatedOn, Gl.SyncLastPatchedOn, Gl.SyncRowVersion
			FROM AMS.GeneralLedger AS Gl
				 LEFT OUTER JOIN AMS.AccountGroup AS AG ON Gl.GrpId=AG.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON Gl.SubGrpId=ASG.SubGrpId
				 LEFT OUTER JOIN AMS.Currency AS C ON Gl.CurrId=C.CId
				 LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId=Gl.AgentId
				 LEFT OUTER JOIN AMS.Area AS A ON Gl.AreaId=A.AreaId
			WHERE GLID = '{selectedId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // RETURN VALUE IN COMBO BOX
    public void BindLedgerType(ComboBox cmbType)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Customer", "CUSTOMER"),
            new("Vendor", "VENDOR"),
            new("Both", "BOTH"),
            new("Other", "OTHER"),
            new("Cash", "CASH"),
            new("Bank", "BANK")
        };
        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
    }
    public void BindBalanceType(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Ignore", "I"),
            new("Warning", "W"),
            new("Block", "B")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = 0;
    }

    // OBJECT FOR THIS FROM 
    public GeneralLedger ObjGeneralLedger { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}