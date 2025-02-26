using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting.Interface;

public interface IPurchaseSettingRepository
{
    int SavePurchaseSetting(string actionTag);
    string GetPurchaseScript(int settingId = 0);
    Task<bool> PullPurchaseSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.PurchaseSetting> purchaseRepo, int callCount);
    // OBJECT 
    DatabaseModule.Setup.SystemSetting.PurchaseSetting VmPurchase { get; set; }
}