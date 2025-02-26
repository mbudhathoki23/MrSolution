using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.Domains.Interface;

public interface ICounterRepository
{
    // INSERT UPDATE DELETE
    int SaveCounter(string actionTag);
    Task<int> SyncCounterAsync(string actionTag);
    Task<int> SyncUpdateCounter(int CId);


    //PULL COUNTER
    string GetCounterScript(int counterId = 0);
    Task<bool> PullCounterServerToClientByRowCounts(IDataSyncRepository<Counter> counterRepo, int callCount);

    // OBJECT FOR THIS FORM
    Counter ObjCounter { get; set; }
}