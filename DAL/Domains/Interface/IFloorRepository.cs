using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.Domains.Interface;

public interface IFloorRepository
{
    //INSERT UPDATE DELETE
    int SaveSetupFloor(string actionTag);
    Task<int> SyncFloorAsync(string actionTag);
    Task<int> SyncUpdateFloor(int floorId);
    string GetFloorScript(int floorId = 0);

    //PULL COUNTER
    Task<bool> PullFloorServerToClientByRowCounts(IDataSyncRepository<FloorSetup> FloorRepo, int callCount);


    FloorSetup ObjFloor { get; set; }
}