using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Threading.Tasks;

namespace MrDAL.Domains.Interface;

public interface ITableMasterRepository
{
    int SaveTable(string actionTag);
    Task<int> SyncTableMasterAsync(string actionTag);
    string GetMasterTable(int selectedId = 0);

    Task<bool> PullTableMasterServerToClientByRowCounts(IDataSyncRepository<TableMaster> currencyRepository,
        int callCount);

    // OBJECT
    TableMaster Table { get; set; }
}