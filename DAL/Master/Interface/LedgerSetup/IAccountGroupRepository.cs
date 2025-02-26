using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IAccountGroupRepository
{
    // INSERT UPDATE DELETE
    int SaveAccountGroup(string actionTag);
    Task<int> SyncAccountGroupAsync(string actionTag);
    Task<int> SyncUpdateAccountGroup(int grpId = 0);

    Task<bool> SyncAccountGroupDetailsAsync();

    int SaveAccountGroupAuditLog(string actionTag);

    Task<bool> PullAccountGroupsServerToClientByRowCounts(IDataSyncRepository<AccountGroup> accountGroupRepo, int callCount);

    // BIND DATA IN COMBO BOX
    void BindPrimaryGroup(ComboBox cmbType);
    void BindAccountGrpType(ComboBox cmbType, string station);


    // BIND DATA IN DATA TABLE
    public string GetAccountGroupScript(int GrpId = 0);
    DataTable GetMasterAccountGroup(string actionTag, string category, int status = 0, int selectedId = 0, bool isPrimary = false);


    // OBJECT FOR THIS FORM
    AccountGroup ObjAccountGroup { get; set; }


}