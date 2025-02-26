using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Interface;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Domains.POS.Master;

public class CounterRepository : ICounterRepository
{
    public CounterRepository()
    {
        ObjCounter = new Counter();
        _master = new ClsMasterSetup();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
    }
    //INSERT UPDATE DELETE
    public int SaveCounter(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.Counter(CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append($"\n Values \n");
            cmdString.Append($"({ObjCounter.CId}, ");
            cmdString.Append(!string.IsNullOrEmpty(ObjCounter.CName)
                ? $"N'{ObjCounter.CName}',"
                : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjCounter.CCode)
                ? $"N'{ObjCounter.CCode}',"
                : "NULL,");
            cmdString.Append(ObjCounter.Branch_ID > 0 ? $" {ObjCounter.Branch_ID}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjCounter.Status ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append($"'{ObjCounter.Printer}',");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{ObjCounter.SyncRowVersion});\n ");

        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.Counter SET ");
            cmdString.Append(!string.IsNullOrEmpty(ObjCounter.CName)
                ? $"CName = N'{ObjCounter.CName}',"
                : "CName = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(ObjCounter.CCode)
                ? $"CCode = N'{ObjCounter.CCode}',"
                : "CCode = NULL,");
            cmdString.Append(ObjCounter.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjCounter.SyncRowVersion}");
            cmdString.Append($" WHERE CId = {ObjCounter.CId}; ");

        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.Counter where CId = {ObjCounter.CId}");
        }
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncCounterAsync(actionTag));
        }
        return exe;

    }

    public async Task<int> SyncCounterAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Counter/GetCountersByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Counter/InsertCounterList",
            UpdateUrl = @$"{_configParams.Model.Item2}Counter/UpdateCounter",
            DeleteUrl = @$"{_configParams.Model.Item2}Counter?id={ObjCounter.CId}",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var counterRepo = DataSyncProviderFactory.GetRepository<Counter>(_injectData);
        var counters = new List<Counter>
        {
            ObjCounter
        };
        var result = actionTag switch
        {
            "SAVE" => counterRepo.PushNewListAsync(counters),
            "UPDATE" => counterRepo.PutNewAsync(ObjCounter),
            "DELETE" => counterRepo.DeleteNewAsync()
        };


        return result.GetInt();
    }

    public Task<int> SyncUpdateCounter(int CId)
    {
        var commandText = $@"
            UPDATE AMS.Counter SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (CId > 0)
        {
            commandText += $" WHERE CId = '{CId}'";
        }

        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public string GetCounterScript(int counterId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Counter d";
        cmdString += counterId > 0 ? $" WHERE d.SyncGlobalId IS NULL AND d.CId= {counterId} " : "";
        return cmdString;
    }

    public async Task<bool> PullCounterServerToClientByRowCounts(Shared.DataSync.Abstractions.IDataSyncRepository<Counter> counterRepo, int callCount)
    {
        var pullResponse = await counterRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }

        var query = GetCounterScript();
        var alldata = SqlExtensions.ExecuteDataSetSql(query);

        foreach (var counterData in pullResponse.List)
        {
            ObjCounter = counterData;

            var alreadyExistData = alldata.Select("CId=" + counterData.CId + "");
            if (alreadyExistData.Length > 0)
            {
                //get SyncRowVersion from client database table
                int ClientSyncRowVersionId = 1;
                ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                //update only server SyncRowVersion is greater than client database while data pulling from server
                if (counterData.SyncRowVersion > ClientSyncRowVersionId)
                {
                    var result = SaveCounter("UPDATE");
                }
            }
            else
            {
                var result = SaveCounter("SAVE");
            }

        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullCounterServerToClientByRowCounts(counterRepo, callCount);
        }

        return true;
    }


    //OBJECT FOR THIS FORM 
    public Counter ObjCounter { get; set; }
    public DbSyncRepoInjectData _injectData;
    public IMasterSetup _master;
    public IMasterSetup _setup;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;

}