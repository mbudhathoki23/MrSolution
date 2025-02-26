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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.LedgerSetup;

public class SubLedgerRepository : ISubLedgerRepository
{
    public SubLedgerRepository()
    {
        ObjSubLedger = new SubLedger();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public int SaveSubLedger(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() is "DELETE")
        {
            SaveSubLedgerAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.SubLedger where SLId ='{ObjSubLedger.SLId}'");
        }

        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(@"
				INSERT INTO AMS.SubLedger (SLId, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_ID, Status,IsDefault, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)");
            cmdString.Append($" \n VALUES ({ObjSubLedger.SLId},N'{ObjSubLedger.SLName}', N'{ObjSubLedger.SLCode}',");
            cmdString.Append(ObjSubLedger.SLAddress.IsValueExits() ? $"N'{ObjSubLedger.SLAddress}'," : "NULL,");
            cmdString.Append(ObjSubLedger.SLPhoneNo.IsValueExits() ? $"'{ObjSubLedger.SLPhoneNo}'," : "NULL,");
            cmdString.Append(ObjSubLedger.GLID.IsValueExits() ? $"'{ObjSubLedger.GLID}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjSubLedger.Status is true ? "1," : "0,");
            cmdString.Append($"0,'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append($" GETDATE(),GETDATE(),{ObjSubLedger.SyncRowVersion} , ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID());" : "NULL);");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.SubLedger SET ");
            cmdString.Append($"SLName = N'{ObjSubLedger.SLName}',SLCode= N'{ObjSubLedger.SLCode}',");
            cmdString.Append(ObjSubLedger.SLAddress.IsValueExits()
                ? $"SLAddress= N'{ObjSubLedger.SLAddress}',"
                : "SLAddress= NULL,");
            cmdString.Append(ObjSubLedger.SLPhoneNo.IsValueExits()
                ? $"SLPhoneNo= N'{ObjSubLedger.SLPhoneNo}',"
                : "SLPhoneNo= NULL,");
            cmdString.Append(ObjSubLedger.GLID.IsValueExits() ? $"GLID= N'{ObjSubLedger.GLID}'," : "GLID = NULL,");
            cmdString.Append(ObjSubLedger.Status is true ? "Status= 1," : "Status= 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjSubLedger.SyncRowVersion}");
            cmdString.Append($"where SLId = '{ObjSubLedger.SLId}'; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe != 0)
        {
            if (actionTag != "DELETE")
            {
                SaveSubLedgerAuditLog(actionTag);
            }

            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncSubLedgerAsync(actionTag));
            }
        }

        return exe;
    }
    public async Task<int> SyncSubLedgerAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}SubLedger/GetSubLedgersByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SubLedger/InsertSubLedgerList",
            UpdateUrl = @$"{_configParams.Model.Item2}SubLedger/UpdateSubLedger",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var subLedgerRepo = DataSyncProviderFactory.GetRepository<SubLedger>(_injectData);
        var subLedgers = new List<SubLedger>
        {
            ObjSubLedger
        };

        // push realtime to server
        await subLedgerRepo.PushNewListAsync(subLedgers);

        // update SyncGlobalId to local
        if (subLedgerRepo.GetHashCode() > 0)
        {
            await SyncUpdateSubLedger(ObjSubLedger.SLId);
        }
        return subLedgerRepo.GetHashCode();

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
        //     var deleteUri = @$"{configParams.Model.Item2}SubLedger/DeleteSubLedgerAsync?id=" + ObjSubLedger.SLId;
        //
        //     var apiConfig = new SyncApiConfig
        //     {
        //         BaseUrl = configParams.Model.Item2,
        //         Apikey = configParams.Model.Item3,
        //         Username = ObjGlobal.LogInUser,
        //         BranchId = ObjGlobal.SysBranchId,
        //         GetUrl = @$"{configParams.Model.Item2}SubLedger/GetSubLedgerById",
        //         InsertUrl = @$"{configParams.Model.Item2}SubLedger/InsertSubLedger",
        //         UpdateUrl = @$"{configParams.Model.Item2}SubLedger/UpdateSubLedger",
        //         DeleteUrl = deleteUri
        //     };
        //
        //     DataSyncHelper.SetConfig(apiConfig);
        //     injectData.ApiConfig = apiConfig;
        //     DataSyncManager.SetGlobalInjectData(injectData);
        //     var subLedgerRepo =
        //         DataSyncProviderFactory.GetRepository<SubLedger>(DataSyncManager.GetGlobalInjectData());
        //
        //     var sl = new SubLedger
        //     {
        //         SLId = ObjSubLedger.SLId,
        //         SLName = ObjSubLedger.SLName,
        //         SLCode = ObjSubLedger.SLCode,
        //         SLAddress = ObjSubLedger.SLAddress.IsValueExits() ? ObjSubLedger.SLAddress : null,
        //         SLPhoneNo = ObjSubLedger.SLPhoneNo.IsValueExits() ? ObjSubLedger.SLPhoneNo : null,
        //         GLID = ObjSubLedger.GLID.IsValueExits() ? ObjSubLedger.GLID : null,
        //         Branch_ID = ObjGlobal.SysBranchId > 0 ? ObjGlobal.SysBranchId : 0,
        //         Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
        //         Status = ObjSubLedger.Status,
        //         IsDefault = 0,
        //         EnterBy = ObjGlobal.LogInUser,
        //         EnterDate = DateTime.Now,
        //         SyncRowVersion = ObjSubLedger.SyncRowVersion
        //     };
        //     var result = actionTag.ToUpper() switch
        //     {
        //         "SAVE" => await subLedgerRepo?.PushNewAsync(sl),
        //         "UPDATE" => await subLedgerRepo?.PutNewAsync(sl),
        //         "DELETE" => await subLedgerRepo?.DeleteNewAsync(),
        //         _ => await subLedgerRepo?.PushNewAsync(sl)
        //     };
        //     return 1;
        // }
        // catch (Exception ex)
        // {
        //     return 1;
        // }
    }

    public async Task<bool> SyncSubLedgerDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}SubLedger/GetSubLedgersByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SubLedger/InsertSubLedgerList",
            UpdateUrl = @$"{_configParams.Model.Item2}SubLedger/UpdateSubLedger",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var subLedgerRepo = DataSyncProviderFactory.GetRepository<SubLedger>(_injectData);

        // pull all new sub ledger data
        var pullResponse = await PullSubLedgersServerToClientByRowCount(subLedgerRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new sub ledger data
        var sqlSlQuery = GetSubLedgerScript();
        var queryResponse = await QueryUtils.GetListAsync<SubLedger>(sqlSlQuery);
        var slList = queryResponse.List.ToList();
        if (slList.Count > 0)
        {
            var pushResponse = await subLedgerRepo.PushNewListAsync(slList);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public int SaveSubLedgerAuditLog(string actionTag)
    {
        var cmdString = $@"
			INSERT INTO AUD.AUDIT_SUBLEDGER(SLId, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_ID, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
			SELECT SLId, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_Id, Status, EnterBy, EnterDate,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
			FROM AMS.SubLedger sl
			WHERE sl.SLId = '{ObjSubLedger.SLId}';";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        return result;
    }
    public Task<int> SyncUpdateSubLedger(int subLedgerId = 0)
    {
        var commandText = $@"
            UPDATE AMS.Subledger SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (subLedgerId > 0)
        {
            commandText += $" WHERE SLId = '{subLedgerId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    // PULL SUBLEDGER
    #region ---------- PULL SUB LEDGER ----------
    public async Task<bool> GetAndSaveUnSynchronizedSubLedgers()
    {
        try
        {
            var subLedgerList = await _master.GetUnSynchronizedData("AMS.SubLedger");
            foreach (var data in subLedgerList.List)
            {
                var subLedgerData = JsonConvert.DeserializeObject<SubLedger>(data.JsonData);
                var actionTag = data.Action;
                if (subLedgerData != null)
                {
                    ObjSubLedger.SLId = subLedgerData.SLId;
                    ObjSubLedger.SLName = subLedgerData.SLName;
                    ObjSubLedger.SLCode = subLedgerData.SLCode;
                    ObjSubLedger.SLAddress = subLedgerData.SLAddress;
                    ObjSubLedger.SLPhoneNo = subLedgerData.SLPhoneNo;
                    ObjSubLedger.GLID = subLedgerData.GLID;
                    ObjSubLedger.Branch_ID = subLedgerData.Branch_ID;
                    ObjSubLedger.Company_Id = subLedgerData.Company_Id;
                    ObjSubLedger.Status = subLedgerData.Status;
                    ObjSubLedger.IsDefault = subLedgerData.IsDefault;
                    ObjSubLedger.EnterBy = subLedgerData.EnterBy;
                    ObjSubLedger.EnterDate = subLedgerData.EnterDate;
                    ObjSubLedger.SyncRowVersion = subLedgerData.SyncRowVersion;
                }

                var result = SaveSubLedger(actionTag);
                if (result <= 0)
                {
                    continue;
                }
                //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                //actionTag = "SAVE";
                //var response = await _master.SaveSyncLogDetails(actionTag);
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    // public async Task<bool> PullSubLedgersByCallCount(IDataSyncRepository<SubLedger> subLedgerRepo, int callCount)
    // {
    //     try
    //     {
    //         _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}SubLedger/GetSubLedgersByCallCount?callCount=" + callCount;
    //         var pullResponse = await subLedgerRepo.GetUnSynchronizedDataAsync();
    //         if (!pullResponse.Success)
    //         {
    //             return false;
    //         }
    //         else
    //         {
    //             foreach (var subLedgerData in pullResponse.List)
    //             {
    //                 var actionTag = "UPDATE";
    //                 var query = $@"
    //                 select SLCode from AMS.SubLedger where Lower(SLCode)='{subLedgerData.SLCode.ToLower()}'";
    //                 var alreadyExistData = ExecuteCommand.ExecuteDataSetSql(query);
    //                 if (alreadyExistData.Rows.Count == 0)
    //                 {
    //                     query = $@"
    //                     select SLId from AMS.SubLedger where SLId='{subLedgerData.SLId}'";
    //                     var alreadyExistData1 = ExecuteCommand.ExecuteDataSetSql(query);
    //                     if (alreadyExistData1.Rows.Count == 0)
    //                     {
    //                         actionTag = "SAVE";
    //                     }
    //                 }
    //                 ObjSubLedger.SLId = subLedgerData.SLId;
    //                 ObjSubLedger.NepalDesc = subLedgerData.NepalDesc;
    //                 ObjSubLedger.SLName = subLedgerData.SLName;
    //                 ObjSubLedger.SLCode = subLedgerData.SLCode;
    //                 ObjSubLedger.SLAddress = subLedgerData.SLAddress;
    //                 ObjSubLedger.SLPhoneNo = subLedgerData.SLPhoneNo;
    //                 ObjSubLedger.GLID = subLedgerData.GLID;
    //                 ObjSubLedger.Branch_ID = subLedgerData.Branch_ID;
    //                 ObjSubLedger.Company_Id = subLedgerData.Company_Id;
    //                 ObjSubLedger.Status = subLedgerData.Status;
    //                 ObjSubLedger.IsDefault = subLedgerData.IsDefault;
    //                 ObjSubLedger.EnterBy = subLedgerData.EnterBy;
    //                 ObjSubLedger.EnterDate = subLedgerData.EnterDate;
    //                 ObjSubLedger.SyncBaseId = subLedgerData.SyncBaseId;
    //                 ObjSubLedger.SyncGlobalId = subLedgerData.SyncGlobalId;
    //                 ObjSubLedger.SyncOriginId = subLedgerData.SyncOriginId;
    //                 ObjSubLedger.SyncCreatedOn = subLedgerData.SyncCreatedOn;
    //                 ObjSubLedger.SyncLastPatchedOn = subLedgerData.SyncLastPatchedOn;
    //                 ObjSubLedger.SyncRowVersion = subLedgerData.SyncRowVersion;
    //                 var result = SaveSubLedger(actionTag);
    //             }
    //         }
    //
    //         if (pullResponse.IsReCall)
    //         {
    //             callCount++;
    //             await PullSubLedgersByCallCount(subLedgerRepo, callCount);
    //         }
    //
    //         return true;
    //     }
    //     catch (Exception e)
    //     {
    //         return false;
    //     }
    // }
    public async Task<bool> PullSubLedgersServerToClientByRowCount(IDataSyncRepository<SubLedger> subLedgerRepo, int callCount)
    {
        try
        {
            var pullResponse = await subLedgerRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetSubLedgerScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);
            foreach (var subLedgerData in pullResponse.List)
            {
                ObjSubLedger = subLedgerData;

                var alreadyExistData = alldata.Select("SLId= " + subLedgerData.SLId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (subLedgerData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveSubLedger("UPDATE");
                    }
                }
                else
                {
                    var result = SaveSubLedger("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullSubLedgersServerToClientByRowCount(subLedgerRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public string GetSubLedgerScript(int subLedgerId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Subledger";
        cmdString += subLedgerId > 0 ? $" WHERE SyncGlobalId IS NULL AND SLId= {subLedgerId} " : "";
        return cmdString;
    }
    #endregion

    // RETURN VALUE IN DATA TABLE
    public DataTable GetMasterSubLedger(string actionTag, int selectedId = 0)
    {
        var cmdString = $@"
			SELECT sl.SLId, sl.NepalDesc, sl.SLName, sl.SLCode, sl.SLAddress, sl.SLPhoneNo, sl.GLID,GL.GLName, sl.Branch_ID, sl.Company_Id, sl.Status, sl.IsDefault, sl.EnterBy, sl.EnterDate, sl.SyncBaseId, sl.SyncGlobalId, sl.SyncOriginId, sl.SyncCreatedOn, sl.SyncLastPatchedOn, sl.SyncRowVersion
			FROM AMS.SubLedger sl
				 LEFT OUTER JOIN AMS.GeneralLedger GL ON TRY_CAST(ISNULL(sl.GLID,0) AS BIGINT) =GL.GLID
				 WHERE sl.SLId = '{selectedId}';";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public SubLedger ObjSubLedger { get; set; }
    private IMasterSetup _master;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}