using DatabaseModule.Setup.LogSetting;

namespace MrDAL.SystemSetting.SystemInterface;

public interface IOnlineConfigurationRepository
{
    int SaveOnlineSyncConfig(string actionTag);

    // OBJECT
    SyncTable ObjSync { get; set; }
}