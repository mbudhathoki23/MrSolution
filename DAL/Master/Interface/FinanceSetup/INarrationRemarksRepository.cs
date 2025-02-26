using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.FinanceSetup;

public interface INarrationRemarksRepository
{
    // INSERT UPDATE DELETE
    int SaveNarration(string actionTag);
    Task<int> SyncNarrationAsync(string actionTag);

    string GetNarrationMasterScript(int nrid = 0);

    Task<bool> PullNarrationMasterServerToClientByRowCount(IDataSyncRepository<NR_Master> narrationRepo,
        int callCount);
    // RETURN DATA IN DATA TABLE
    DataTable GetMasterNarration(string actionTag, int selectedId = 0);
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);

    // OBJECT FOR THIS FORM
    NR_Master Narration { get; set; }
}