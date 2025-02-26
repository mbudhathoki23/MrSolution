using DatabaseModule.Setup.TermSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.SystemSetup;

public interface ISalesBillingTermRepository
{
    // UPDATE INSERT DELETE
    int SaveSalesTerm(string actionTag);

    // RETURN DATA IN DATA TABLE
    DataTable GetMasterSalesTermList(string actionTag, string category, int selectedId = 0);
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);

    // RETURN VALUE IN INT
    bool IsBillingTermUsedOrNot(string module, int termId);
    string GetLedgerDescription(long ledgerId);
    int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt);

    Task<bool> PullSalesBillingTermServerToClientByRowCounts(IDataSyncRepository<ST_Term> salesTermRepo,
        int callCount);
    string GetSalesTermScript(int Id = 0);
    // OBJECT FOR THIS FORM
    ST_Term StTerm { get; set; }
}