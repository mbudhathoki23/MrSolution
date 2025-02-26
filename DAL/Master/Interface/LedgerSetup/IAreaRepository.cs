using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IAreaRepository
{
    // INSERT UPDATE DELETE
    string GetAreaScript(int areaId = 0);
    int SaveArea(string actionTag);
    Task<int> SyncAreaAsync(string actionTag);
    Task<bool> SyncAreaDetailsAsync();
    int SaveAreaAuditLog(string actionTag);

    Task<bool> PullAreaFromServerToClientDBByCallCount(IDataSyncRepository<Area> areaRepo, int callCount);


    // RETURN DATA IN DATA TABLE
    DataTable GetMasterArea(string actionTag, int selectedId = 0);

    // OBJECT FOR THIS FROM 

    Area ObjArea { get; set; }
}