using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface ISubLedgerRepository
{
    // INSERT UPDATE DELETE

    int SaveSubLedger(string actionTag);

    Task<int> SyncSubLedgerAsync(string actionTag);
    Task<bool> SyncSubLedgerDetailsAsync();

    int SaveSubLedgerAuditLog(string actionTag);

    Task<bool> GetAndSaveUnSynchronizedSubLedgers();
    // Task<bool> PullSubLedgersByCallCount(IDataSyncRepository<SubLedger> subLedgerRepo, int callCount);

    Task<bool> PullSubLedgersServerToClientByRowCount(IDataSyncRepository<SubLedger> subLedgerRepo, int callCount);

    string GetSubLedgerScript(int subLedgerId = 0);
    // RETURN VALUE IN DATA TABLE

    DataTable GetMasterSubLedger(string actionTag, int selectedId = 0);

    // OBJECT FOR THIS FORM

    SubLedger ObjSubLedger { get; set; }
}