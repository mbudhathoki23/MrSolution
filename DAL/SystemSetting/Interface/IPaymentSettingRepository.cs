using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting.Interface;

public interface IPaymentSettingRepository
{
    int SavePaymentSetting(string actionTag);
    string GetPaymentScript(int settingId = 0);
    Task<bool> PullPaymentSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.PaymentSetting> paymentSettingRepo, int callCount);

    // OBJECT 
    DatabaseModule.Setup.SystemSetting.PaymentSetting VmPayment { get; set; }
}