using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting.Interface;

public interface ISystemSettingRepository
{
    int SaveSystemSetting(string actionTag);
    string GetSystemSettingScript(int settingId = 0);
    Task<bool> PullSystemSettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.SystemSetting> systemSettingRepo, int callCount);

    // OBJECT 
    DatabaseModule.Setup.SystemSetting.SystemSetting VmSystem { get; set; }
}