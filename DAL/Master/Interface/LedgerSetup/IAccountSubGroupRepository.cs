using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IAccountSubGroupRepository
{
    // INSERT UPDATE DELETE
    int SaveAccountSubGroup(string actionTag);
    Task<int> SyncAccountSubGroupAsync(string actionTag);
    Task<int> SyncUpdateAccountSubGroup(int subGrpId = 0);
    Task<bool> SyncAccountSubGroupDetailsAsync();
    int SaveAccountSubGroupAuditLog(string actionTag);

    Task<bool> PullAccountSubGroupsServerToClientByRowCount(IDataSyncRepository<AccountSubGroup> accountSubGroupRepo, int callCount);



    // RETURN VALUE IN DATA TABLE

    public string GetAccountSubGroupScript(int subGrpId = 0);
    DataTable GetMasterAccountSubGroup(string actionTag, string category, int status = 0, int selectedId = 0, bool isPrimary = false);



    // OBJECT FOR THIS FORM
    AccountSubGroup ObjAccountSubGroup { get; set; }
}