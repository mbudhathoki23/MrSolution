using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting.Interface;

public interface IInventorySettingRepository
{
    int SaveInventorySetting(string actionTag);
    string GetInventoryScript(int settingId = 0);
    Task<bool> PullInventorySettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.InventorySetting> inventoryRepo, int callCount);
    // OBJECT 
    DatabaseModule.Setup.SystemSetting.InventorySetting VmStock { get; set; }
}