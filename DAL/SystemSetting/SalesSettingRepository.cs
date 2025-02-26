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

public class SalesSettingRepository : ISalesSettingRepository
{
    public SalesSettingRepository()
    {
        VmSales = new DatabaseModule.Setup.SystemSetting.SalesSetting();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    public int SaveSalesSetting(string actionTag)
    {
        var cmdString = $@"
			TRUNCATE TABLE AMS.SalesSetting;
			INSERT INTO AMS.SalesSetting (SalesId, SBLedgerId, SRLedgerId, SBVatTerm, SBDiscountTerm, SBProductDiscountTerm, SBAdditionalTerm, SBServiceCharge, SBDateChange, SBCreditDays, SBCreditLimit, SBCarryRate, SBChangeRate, SBLastRate, SBQuotationEnable, SBQuotationMandetory, SBDispatchOrderEnable, SBDispatchMandetory, SOEnable, SOMandetory, SCEnable, SCMandetory, SBSublegerEnable, SBSubledgerMandetory, SBAgentEnable, SBAgentMandetory, SBDepartmentEnable, SBDepartmentMandetory, SBCurrencyEnable, SBCurrencyMandetory, SBCurrencyRateChange, SBGodownEnable, SBGodownMandetory, SBAlternetUnitEnable, SBIndent, SBNarration, SBBasicAmount, SBAviableStock, SBReturnValue, PartyInfo, SBRemarksEnable, SBRemarksMandatory)
			VALUES({VmSales.SalesId}, ";
        cmdString += VmSales.SBLedgerId > 0 ? $"{VmSales.SBLedgerId}, " : "NULL,";
        cmdString += VmSales.SRLedgerId > 0 ? $"{VmSales.SRLedgerId}, " : "NULL,";
        cmdString += VmSales.SBVatTerm > 0 ? $"{VmSales.SBVatTerm}, " : "NULL,";
        cmdString += VmSales.SBDiscountTerm > 0 ? $"{VmSales.SBDiscountTerm}, " : "NULL,";
        cmdString += VmSales.SBProductDiscountTerm > 0 ? $"{VmSales.SBProductDiscountTerm}, " : "NULL,";
        cmdString += VmSales.SBAdditionalTerm > 0 ? $"{VmSales.SBAdditionalTerm}, " : "NULL,";
        cmdString += VmSales.SBServiceCharge > 0 ? $"{VmSales.SBServiceCharge}, " : "NULL,";
        cmdString +=
            $"  CAST('{VmSales.SBDateChange}' AS BIT), '{VmSales.SBCreditDays}', '{VmSales.SBCreditLimit}', CAST('{VmSales.SBCarryRate}' AS BIT), CAST('{VmSales.SBChangeRate}' AS BIT), CAST('{VmSales.SBLastRate}' AS BIT), CAST('{VmSales.SBQuotationEnable}' AS BIT), CAST('{VmSales.SBQuotationMandetory}' AS BIT), CAST('{VmSales.SBDispatchOrderEnable}' AS BIT), CAST('{VmSales.SBDispatchMandetory}' AS BIT), CAST('{VmSales.SOEnable}' AS BIT), CAST('{VmSales.SOMandetory}' AS BIT), CAST('{VmSales.SCEnable}' AS BIT), CAST('{VmSales.SCMandetory}' AS BIT), CAST('{VmSales.SBSublegerEnable}' AS BIT), CAST('{VmSales.SBSubledgerMandetory}' AS BIT), CAST('{VmSales.SBAgentEnable}' AS BIT), CAST('{VmSales.SBAgentMandetory}' AS BIT), CAST('{VmSales.SBDepartmentEnable}' AS BIT), CAST('{VmSales.SBDepartmentMandetory}' AS BIT), CAST('{VmSales.SBCurrencyEnable}' AS BIT), CAST('{VmSales.SBCurrencyMandetory}' AS BIT), CAST('{VmSales.SBCurrencyRateChange}' AS BIT), CAST('{VmSales.SBGodownEnable}' AS BIT), CAST('{VmSales.SBGodownMandetory}' AS BIT), CAST('{VmSales.SBAlternetUnitEnable}' AS BIT), CAST('{VmSales.SBIndent}' AS BIT), CAST('{VmSales.SBNarration}' AS BIT), CAST('{VmSales.SBBasicAmount}' AS BIT), CAST('{VmSales.SBAviableStock}' AS BIT), CAST('{VmSales.SBReturnValue}' AS BIT), CAST('{VmSales.PartyInfo}' AS BIT), CAST('{VmSales.SBRemarksEnable}' AS BIT), CAST('{VmSales.SBRemarksMandatory}' AS BIT));";
        var result = SqlExtensions.ExecuteNonTrans(cmdString);

        return result;
    }

    public string GetSalesSettingScript(int settingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.SalesSetting s";
        cmdString += settingId > 0 ? $" WHERE s.SalesId= {settingId} " : "";
        return cmdString;
    }

    public async Task<bool> PullSalesSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.SalesSetting> salesSettingRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}SalesSetting/GetSalesSettingByCallCount?callCount={callCount}";
        var pullResponse = await salesSettingRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetSalesSettingScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var settingData in pullResponse.List)
            {
                VmSales = settingData;

                var alreadyExistData = alldata.Select("SalesId='" + settingData.SalesId + "'");
                if (alreadyExistData.Length > 0)
                {
                    var result = SaveSalesSetting("UPDATE");
                }
                else
                {
                    var result = SaveSalesSetting("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullSalesSettingServerToClientByRowCounts(salesSettingRepo, callCount);
        }

        return true;
    }

    public DatabaseModule.Setup.SystemSetting.SalesSetting VmSales { get; set; }
    public DbSyncRepoInjectData _injectData;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
}