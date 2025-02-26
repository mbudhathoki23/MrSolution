using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.InventorySetup;

public interface ICostCenterRepository
{

    // INSERT UPDATE DELETE
    int SaveCostCenter(string actionTag);
    Task<int> SyncCostCenterAsync(string actionTag);

    string GetCostCentreScript(int costCentreId = 0);

    Task<bool> PullCostCentreServerToClientByRowCounts(IDataSyncRepository<CostCenter> costCRepository, int callCount);
    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterCostCenter(string actionTag, int status = 0, int selectedId = 0);

    // OBJECT FOR THIS FORM
    CostCenter ObjCostCenter { get; set; }

}