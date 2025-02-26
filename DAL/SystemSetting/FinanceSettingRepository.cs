using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.SystemSetting.Interface;
using MrDAL.Utility.Server;
using System;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting;

public class FinanceSettingRepository : IFinanceSettingRepository
{
    public FinanceSettingRepository()
    {
        VmFinance = new DatabaseModule.Setup.SystemSetting.FinanceSetting();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    public int SaveFinanceSetting(string actionTag)
    {
        var cmdString = $@"
			TRUNCATE TABLE AMS.FinanceSetting;
			INSERT INTO AMS.FinanceSetting (FinId, ProfiLossId, CashId, VATLedgerId, PDCBankLedgerId, ShortNameWisTransaction, WarngNegativeTransaction, NegativeTransaction, VoucherDate, AgentEnable, AgentMandetory, DepartmentEnable, DepartmentMandetory, RemarksEnable, RemarksMandetory, NarrationMandetory, CurrencyEnable, CurrencyMandetory, SubledgerEnable, SubledgerMandetory, DetailsClassEnable, DetailsClassMandetory)
			VALUES({VmFinance.FinId}, ";
        cmdString += VmFinance.ProfiLossId > 0 ? $" {VmFinance.ProfiLossId}," : "NULL, ";
        cmdString += VmFinance.CashId > 0 ? $"{VmFinance.CashId}," : "NULL, ";
        cmdString += VmFinance.VATLedgerId > 0 ? $"{VmFinance.VATLedgerId}," : "NULL,";
        cmdString += VmFinance.PDCBankLedgerId > 0 ? $" {VmFinance.PDCBankLedgerId}," : "NULL,";
        cmdString +=
            $" CAST('{VmFinance.ShortNameWisTransaction}' AS BIT), CAST('{VmFinance.WarngNegativeTransaction}' AS BIT), N'{VmFinance.NegativeTransaction}', CAST('{VmFinance.VoucherDate}' AS BIT), CAST('{VmFinance.AgentEnable}' AS BIT), CAST('{VmFinance.AgentMandetory}' AS BIT), CAST('{VmFinance.DepartmentEnable}' AS BIT), CAST('{VmFinance.DepartmentMandetory}' AS BIT), CAST('{VmFinance.RemarksEnable}' AS BIT), CAST('{VmFinance.RemarksMandetory}' AS BIT), CAST('{VmFinance.NarrationMandetory}' AS BIT), CAST('{VmFinance.CurrencyEnable}' AS BIT), CAST('{VmFinance.CurrencyMandetory}' AS BIT), CAST('{VmFinance.SubledgerEnable}' AS BIT), CAST('{VmFinance.SubledgerMandetory}' AS BIT), CAST('{VmFinance.DetailsClassEnable}' AS BIT), CAST('{VmFinance.DetailsClassMandetory}' AS BIT));";
        return SqlExtensions.ExecuteNonTrans(cmdString);
    }

    public string GetFinanceScript(int settingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.FinanceSetting f";
        cmdString += settingId > 0 ? $" WHERE f.FinId= {settingId} " : "";
        return cmdString;
    }

    public async Task<bool> PullFinanceSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.FinanceSetting> financeRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}FinanceSetting/GetFinanceSettingByCallCount?callCount={callCount}";
        var pullResponse = await financeRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetFinanceScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var settingData in pullResponse.List)
            {
                VmFinance = settingData;

                var alreadyExistData = alldata.Select("FinId='" + settingData.FinId + "'");
                if (alreadyExistData.Length > 0)
                {
                    var result = SaveFinanceSetting("UPDATE");
                }
                else
                {
                    var result = SaveFinanceSetting("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullFinanceSettingServerToClientByRowCounts(financeRepo, callCount);
        }

        return true;
    }

    public DatabaseModule.Setup.SystemSetting.FinanceSetting VmFinance { get; set; }
    public DbSyncRepoInjectData _injectData;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
}