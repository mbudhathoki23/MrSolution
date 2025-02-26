using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface IGeneralLedgerRepository
{
    // INSERT UPDATE DELETE
    int SaveGeneralLedger(string actionTag);
    Task<int> SyncGeneralLedgerAsync(string actionTag);
    Task<bool> SyncGeneralLedgerDetailsAsync();
    int SaveGeneralLedgerAuditLog(string actionTag);
    string GetGeneralLedgerScript(int generalLedgerId = 0);

    Task<bool> PullGeneralLedgersServerToClientByRowCount(IDataSyncRepository<GeneralLedger> generalLedgerRepo, int callCount);

    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterGeneralLedger(string actionTag, long selectedId = 0);

    // OBJECT FOR THIS FORM
    GeneralLedger ObjGeneralLedger { get; set; }

    // RETURN VALUE IN INT
    void BindBalanceType(ComboBox box);
    void BindLedgerType(ComboBox box);
}