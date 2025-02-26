using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.InventorySetup;

public interface IRackRepository
{
    // INSERT UPDATE DELETE
    int SaveRack(string actionTag);
    Task<int> SyncRackAsync(string actionTag);
    Task<bool> SyncRackDetailsAsync();

    // RETURN VALUE IN DATA TABLE
    DataTable GetRackList(string actionTag, int status = 0, int selectedId = 0);

    Task<bool> PullRacksServerToClientByRowCount(IDataSyncRepository<RACK> rackRepo, int callCount);
    string GetRackScript(int rackId = 0);

    // OBJECT FOR THIS FORM
    RACK ObjRack { get; set; }
}