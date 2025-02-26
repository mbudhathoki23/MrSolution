using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IProductUnitRepository
{
    // INSERT UPDATE DELETE
    int SaveProductUnit(string actionTag);
    Task<int> SyncProductUnitAsync(string actionTag);
    Task<bool> SyncProductUnitDetailsAsync();
    public int SaveProductUnitAuditLog(string actionTag);
    string GetProductUnitScript(int productUnitId = 0);

    Task<bool> PullProductUnitsServerToClientByRowCount(IDataSyncRepository<ProductUnit> productUnitRepo, int callCount);

    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterProductUnit(string actionTag, string category, int status = 0, int selectedId = 0);


    // OBJECT FOR THIS FORM
    ProductUnit ObjProductUnit { get; set; }
}