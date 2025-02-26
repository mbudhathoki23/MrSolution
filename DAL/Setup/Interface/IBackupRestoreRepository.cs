using DatabaseModule.Setup.LogSetting;
using System.Threading.Tasks;

namespace MrDAL.Setup.Interface;

public interface IBackupRestore
{
    Task<int> SaveLoginLog(string actionTag);
    Task<int> SaveBackupAndRestoreDatabaseLog(string actionTag);

    BR_LOG BackupLog { get; set; }
    LOGIN_LOG LoginLog { get; set; }
}