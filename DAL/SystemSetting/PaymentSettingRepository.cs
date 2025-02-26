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

public class PaymentSettingRepository : IPaymentSettingRepository
{
    public PaymentSettingRepository()
    {
        VmPayment = new DatabaseModule.Setup.SystemSetting.PaymentSetting();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    public int SavePaymentSetting(string actionTag)
    {
        var cmdString = $@"
			TRUNCATE TABLE AMS.PaymentSetting;
			INSERT INTO AMS.PaymentSetting(PaymentId, CashLedgerId, CardLedgerId, BankLedgerId, PhonePayLedgerId, EsewaLedgerId, KhaltiLedgerId, RemitLedgerId, ConnectIpsLedgerId,PartialLedgerId)
			VALUES( {VmPayment.PaymentId}, ";
        cmdString += VmPayment.CashLedgerId > 0 ? $" {VmPayment.CashLedgerId}," : "NULL, ";
        cmdString += VmPayment.CardLedgerId > 0 ? $"{VmPayment.CardLedgerId}," : "NULL, ";
        cmdString += VmPayment.BankLedgerId > 0 ? $"{VmPayment.BankLedgerId}," : "NULL,";
        cmdString += VmPayment.PhonePayLedgerId > 0 ? $" {VmPayment.PhonePayLedgerId}," : "NULL,";
        cmdString += VmPayment.EsewaLedgerId > 0 ? $" {VmPayment.EsewaLedgerId}," : "NULL,";
        cmdString += VmPayment.KhaltiLedgerId > 0 ? $" {VmPayment.KhaltiLedgerId}," : "NULL,";
        cmdString += VmPayment.RemitLedgerId > 0 ? $" {VmPayment.RemitLedgerId}," : "NULL,";
        cmdString += VmPayment.ConnectIpsLedgerId > 0 ? $" {VmPayment.ConnectIpsLedgerId}," : "NULL,";
        cmdString += VmPayment.PartialLedgerId > 0 ? $" {VmPayment.PartialLedgerId}" : "NULL";
        cmdString += " );";
        return SqlExtensions.ExecuteNonTrans(cmdString);
    }

    public string GetPaymentScript(int settingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.PaymentSetting p";
        cmdString += settingId > 0 ? $" WHERE p.PaymentId= {settingId} " : "";
        return cmdString;
    }

    public async Task<bool> PullPaymentSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.PaymentSetting> paymentSettingRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}PaymentSetting/GetPaymentSettingByCallCount?callCount={callCount}";
        var pullResponse = await paymentSettingRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetPaymentScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var settingData in pullResponse.List)
            {
                VmPayment = settingData;

                var alreadyExistData = alldata.Select("PaymentId='" + settingData.PaymentId + "'");
                if (alreadyExistData.Length > 0)
                {
                    var result = SavePaymentSetting("UPDATE");
                }
                else
                {
                    var result = SavePaymentSetting("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullPaymentSettingServerToClientByRowCounts(paymentSettingRepo, callCount);
        }

        return true;
    }

    public DatabaseModule.Setup.SystemSetting.PaymentSetting VmPayment { get; set; }
    public DbSyncRepoInjectData _injectData;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
}