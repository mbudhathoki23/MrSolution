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

public class CurrencyRepository : ICurrencyRepository
{
    public CurrencyRepository()
    {
        ObjCurrency = new Currency();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
    }


    // INSERT UPDATE DELETE
    public int SaveCurrency(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(@" INSERT INTO AMS.Currency(CId, NepaliDesc, CName, CCode, CRate,BuyRate, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)");
            cmdString.Append($"\n Values({ObjCurrency.CId},N'{ObjCurrency.NepaliDesc}',N'{ObjCurrency.CName}',N'{ObjCurrency.CCode}',");
            cmdString.Append(ObjCurrency.CRate > 0 ? $"N'{ObjCurrency.CRate}'," : "1,");
            cmdString.Append(ObjCurrency.BuyRate > 0 ? $"N'{ObjCurrency.BuyRate}'," : "0,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : $"{ObjCurrency.Branch_ID},");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append($"1,0,'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{ObjCurrency.SyncRowVersion.GetDecimal(true)}); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.Currency SET \n");
            cmdString.Append($"NepaliDesc = N'{ObjCurrency.NepaliDesc}',");
            cmdString.Append($"CName = N'{ObjCurrency.CName}',");
            cmdString.Append($"Ccode = N'{ObjCurrency.CCode}',");
            cmdString.Append(ObjCurrency.CRate > 0 ? $"CRate = N'{ObjCurrency.CRate}'," : "CRate = 1,");
            cmdString.Append(ObjCurrency.BuyRate > 0 ? $"CRate = N'{ObjCurrency.BuyRate}'," : "CRate = 1,");
            cmdString.Append(ObjCurrency.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjCurrency.SyncRowVersion}");
            cmdString.Append($" WHERE CId = {ObjCurrency.CId}; ");
        }

        if (actionTag.ToUpper() == "DELETE")
        {
            //SaveCurrencyAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.GeneralLedger where CurrId =  {ObjCurrency.CId}");
            cmdString.Append($"Delete from AMS.Currency where CId =  {ObjCurrency.CId}");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }
        //SaveCurrencyAuditLog(actionTag);
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncCurrencyAsync(actionTag));
        }

        return exe;
    }
    public async Task<int> SyncCurrencyAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Currency/GetCurrenciesByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Currency/InsertCurrencyList",
            UpdateUrl = @$"{_configParams.Model.Item2}Currency/UpdateCurrency"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var currencyRepo = DataSyncProviderFactory.GetRepository<Currency>(_injectData);
        var currencies = new List<Currency>
        {
            ObjCurrency
        };
        // push realtime currency details to server
        await currencyRepo.PushNewListAsync(currencies);

        // update currency SyncGlobalId to local
        if (currencyRepo.GetHashCode() > 0)
        {
            await SyncUpdateCurrency(ObjCurrency.CId);
        }
        return currencyRepo.GetHashCode();

    }

    public async Task<bool> SyncCurrencyDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}Currency/GetCurrenciesByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Currency/InsertCurrencyList",
            UpdateUrl = @$"{_configParams.Model.Item2}Currency/UpdateCurrency"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var currencyRepo = DataSyncProviderFactory.GetRepository<Currency>(_injectData);
        // pull all new currency data
        var pullResponse = await PullCurrencyServerToClientByRowCounts(currencyRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new currency data
        var sqlCrQuery = GetCurrencyScript();
        var queryResponse = await QueryUtils.GetListAsync<Currency>(sqlCrQuery);
        var curList = queryResponse.List.ToList();
        if (curList.Count > 0)
        {
            var pushResponse = await currencyRepo.PushNewListAsync(curList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.ShowDefaultWaitForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateCurrency(int currencyId)
    {
        var commandText = $@"
            UPDATE AMS.Currency SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (currencyId > 0)
        {
            commandText += $" WHERE CId = '{currencyId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    public int SaveCurrencyAuditLog(string actionTag)
    {
        var cmdAudit = $@"
            INSERT INTO AUD.AUDIT_CURRENCY(CId, CName, Ccode, CRate, Branch_Id, Company_Id, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT CId, CName, Ccode, CRate, Branch_Id, Company_Id, Status, EnterBy, EnterDate,'{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.Currency
            WHERE CId='{ObjCurrency.CId}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdAudit);
        return exe;
    }
    public async Task<bool> GetAndSaveUnSynchronizedCurrencies()
    {
        try
        {
            var currencyList = await _master.GetUnSynchronizedData("AMS.Currency");
            if (currencyList.List != null)
            {
                foreach (var data in currencyList.List)
                {
                    var currencyData = JsonConvert.DeserializeObject<Currency>(data.JsonData);
                    var actionTag = data.Action;
                    ObjCurrency.CId = currencyData.CId;
                    ObjCurrency.CName = currencyData.CName;
                    ObjCurrency.CCode = currencyData.CCode;
                    ObjCurrency.CRate = currencyData.CRate;
                    ObjCurrency.Branch_ID = currencyData.Branch_ID;
                    ObjCurrency.Company_Id = currencyData.Company_Id;
                    ObjCurrency.Status = currencyData.Status;
                    ObjCurrency.EnterBy = currencyData.EnterBy;
                    ObjCurrency.EnterDate = currencyData.EnterDate;
                    ObjCurrency.SyncRowVersion = currencyData.SyncRowVersion;
                    var result = SaveCurrency(actionTag);
                    if (result > 0)
                    {
                        //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                        //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                        //actionTag = "SAVE";
                        //var response = await _master.SaveSyncLogDetails(actionTag);
                    }
                }
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


    // PULL BRANCH
    public async Task<bool> PullCurrencyServerToClientByRowCounts(IDataSyncRepository<Currency> currencyRepository, int callCount)
    {
        try
        {
            var pullResponse = await currencyRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }
            else
            {
                var query = GetCurrencyScript();
                var alldata = SqlExtensions.ExecuteDataSetSql(query);

                foreach (var currency in pullResponse.List)
                {
                    ObjCurrency = currency;

                    var alreadyExistData = alldata.Select("CId= " + currency.CId + "");
                    if (alreadyExistData.Length > 0)
                    {
                        //get SyncRowVersion from client database table
                        int ClientSyncRowVersionId = 1;
                        ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                        //update only server SyncRowVersion is greater than client database while data pulling from server
                        if (currency.SyncRowVersion > ClientSyncRowVersionId)
                        {
                            var result = SaveCurrency("UPDATE");
                        }
                    }
                    else
                    {
                        var result = SaveCurrency("SAVE");
                    }
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullCurrencyServerToClientByRowCounts(currencyRepository, callCount);
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public string GetCurrencyScript(int currencyId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Currency c ";
        cmdString += currencyId > 0 ? $"WHERE c.SyncGlobalId IS NULL AND c.CId= {currencyId} " : "";
        return cmdString;
    }


    // RETURN VALUE IN DATA TABLE
    public DataTable GetMasterCurrency(string actionTag, int status = 0, int selectedId = 0)
    {
        var cmdString = $"SELECT  * FROM AMS.Currency WHERE CId= {selectedId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // OBJECT FOR THIS FORM
    public Currency ObjCurrency { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}