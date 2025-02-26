using DatabaseModule.CloudSync;

namespace MrDAL.Reports.Interface;

public interface IApiErrorLog
{
    int SaveErrorLog();

    SyncApiErrorLog ObjLog { get; set; }
}