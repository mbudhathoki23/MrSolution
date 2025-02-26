using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IProductRepository
{
    // INSERT UPDATE DELETE
    int SaveProductInfo(string actionTag);
    Task<int> SyncProductAsync(string actionTag);
    Task<bool> SyncProductDetailsAsync();
    int SaveProductAuditLog(string actionTag);
    string GetProductScript(int productId = 0);
    Task<bool> GetAndSaveUnSynchronizedProducts();
    // Task<bool> PullProductsByCallCount(IDataSyncRepository<Product> productRepo, int callCount);
    Task<bool> PullProductsServerToClientByRowCount(IDataSyncRepository<Product> productRepo, int callCount);

    // RETURN VALUE IN DATA TABLE
    DataTable GetProductGroupLedgerDetails(int groupId);

    // OBJECT FOR THIS FORM
    Product ObjProduct { get; set; }
    BookDetails BookDetails { get; set; }
    List<BarcodeList> BarcodeLists { get; set; }
}