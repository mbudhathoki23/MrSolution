using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IProductGroupRepository
{
    // INSERT UPDATE DELETE
    int SaveProductGroup(string actionTag);
    Task<int> SyncProductGroupAsync(string actionTag);
    Task<bool> SyncProductGroupDetailsAsync();
    int SaveProductGroupAuditLog(string actionTag);
    string GetProductGroupScript(int productGroupId = 0);

    Task<bool> PullProductGroupsServerToClientByRowCount(IDataSyncRepository<ProductGroup> productGroupRepo, int callCount);

    // RETURN VALUE IN DATA TABLE
    DataTable GetProductGroupLedgerDetails(int groupId);

    // OBJECT FOR THIS FORM
    ProductGroup ObjProductGroup { get; set; }
}

