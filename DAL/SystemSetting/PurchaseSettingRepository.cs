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

public class PurchaseSettingRepository : IPurchaseSettingRepository
{
    public PurchaseSettingRepository()
    {
        VmPurchase = new DatabaseModule.Setup.SystemSetting.PurchaseSetting();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    public int SavePurchaseSetting(string actionTag)
    {
        var cmdString = $@"
			TRUNCATE TABLE AMS.PurchaseSetting;
			INSERT INTO AMS.PurchaseSetting (PurId, PBLedgerId, PRLedgerId, PBVatTerm, PBDiscountTerm, PBProductDiscountTerm, PBAdditionalTerm, PBDateChange, PBCreditDays, PBCreditLimit, PBCarryRate, PBChangeRate, PBLastRate, POEnable, POMandetory, PCEnable, PCMandetory, PBSublegerEnable, PBSubledgerMandetory, PBAgentEnable, PBAgentMandetory, PBDepartmentEnable, PBDepartmentMandetory, PBCurrencyEnable, PBCurrencyMandetory, PBCurrencyRateChange, PBGodownEnable, PBGodownMandetory, PBAlternetUnitEnable, PBIndent, PBNarration, PBBasicAmount, PBRemarksEnable, PBRemarksMandatory)
			VALUES({VmPurchase.PurId}, ";
        cmdString += VmPurchase.PBLedgerId > 0 ? $"{VmPurchase.PBLedgerId}, " : "NULL,";
        cmdString += VmPurchase.PRLedgerId > 0 ? $"{VmPurchase.PRLedgerId}, " : "NULL,";
        cmdString += VmPurchase.PBVatTerm > 0 ? $"{VmPurchase.PBVatTerm}, " : "NULL,";
        cmdString += VmPurchase.PBDiscountTerm > 0 ? $"{VmPurchase.PBDiscountTerm}, " : "NULL,";
        cmdString += VmPurchase.PBProductDiscountTerm > 0 ? $"{VmPurchase.PBProductDiscountTerm}, " : "NULL,";
        cmdString += VmPurchase.PBAdditionalTerm > 0 ? $"{VmPurchase.PBAdditionalTerm}, " : "NULL,";
        cmdString +=
            @$" CAST('{VmPurchase.PBDateChange}' AS BIT), '{VmPurchase.PBCreditDays}', '{VmPurchase.PBCreditLimit}', CAST('{VmPurchase.PBCarryRate}' AS BIT), CAST('{VmPurchase.PBChangeRate}' AS BIT), CAST('{VmPurchase.PBLastRate}' AS BIT), CAST('{VmPurchase.POEnable}' AS BIT), CAST('{VmPurchase.POMandetory}' AS BIT), CAST('{VmPurchase.PCEnable}' AS BIT), CAST('{VmPurchase.PCMandetory}' AS BIT), CAST('{VmPurchase.PBSublegerEnable}' AS BIT), CAST('{VmPurchase.PBSubledgerMandetory}' AS BIT), CAST('{VmPurchase.PBAgentEnable}' AS BIT), CAST('{VmPurchase.PBAgentMandetory}' AS BIT), CAST('{VmPurchase.PBDepartmentEnable}' AS BIT), CAST('{VmPurchase.PBDepartmentMandetory}' AS BIT), CAST('{VmPurchase.PBCurrencyEnable}' AS BIT), CAST('{VmPurchase.PBCurrencyMandetory}' AS BIT), CAST('{VmPurchase.PBCurrencyRateChange}' AS BIT), CAST('{VmPurchase.PBGodownEnable}' AS BIT), CAST('{VmPurchase.PBGodownMandetory}' AS BIT), CAST('{VmPurchase.PBAlternetUnitEnable}' AS BIT), CAST('{VmPurchase.PBIndent}' AS BIT), CAST('{VmPurchase.PBNarration}' AS BIT), CAST('{VmPurchase.PBBasicAmount}' AS BIT), CAST('{VmPurchase.PBRemarksEnable}' AS BIT), CAST('{VmPurchase.PBRemarksMandatory}' AS BIT));";
        return SqlExtensions.ExecuteNonTrans(cmdString);
    }

    public string GetPurchaseScript(int settingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.PurchaseSetting s";
        cmdString += settingId > 0 ? $" WHERE s.PurId= {settingId} " : "";
        return cmdString;
    }

    public async Task<bool> PullPurchaseSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.PurchaseSetting> purchaseSettingRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}SavePurchaseSetting/GetSavePurchaseSettingByCallCount?callCount={callCount}";
        var pullResponse = await purchaseSettingRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetPurchaseScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var settingData in pullResponse.List)
            {
                VmPurchase = settingData;

                var alreadyExistData = alldata.Select("PurId='" + settingData.PurId + "'");
                if (alreadyExistData.Length > 0)
                {
                    var result = SavePurchaseSetting("UPDATE");
                }
                else
                {
                    var result = SavePurchaseSetting("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullPurchaseSettingServerToClientByRowCounts(purchaseSettingRepo, callCount);
        }

        return true;
    }

    public DatabaseModule.Setup.SystemSetting.PurchaseSetting VmPurchase { get; set; }
    public DbSyncRepoInjectData _injectData;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
}