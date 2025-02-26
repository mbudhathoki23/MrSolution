using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.ProductSetup;

public interface IProductSubGroupRepository
{
    // INSERT UPDATE DELETE
    int SaveProductSubGroup(string actionTag);

    Task<int> SyncProductSubGroupAsync(string actionTag);
    Task<bool> SyncProductSubGroupDetailsAsync();

    int SaveProductSubGroupAuditLog(string actionTag);
    string GetProductSubGroupScript(int productSubGroupId = 0);

    Task<bool> PullProductSubGroupsServerToClientByRowCount(IDataSyncRepository<ProductSubGroup> productSubGroupRepo, int callCount);

    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterProductSubGroup(string actionTag, string category, bool status = false, int selectedId = 0, int groupId = 0);

    // OBJECT FOR THIS FORM
    ProductSubGroup ObjProductSubGroup { get; set; }

}