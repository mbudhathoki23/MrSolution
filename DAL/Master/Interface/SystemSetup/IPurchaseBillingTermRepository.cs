using DatabaseModule.Setup.TermSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.SystemSetup;

public interface IPurchaseBillingTermRepository
{
    // INSERT UPDATE DELETE
    int SavePurchaseTerm(string actionTag);

    // RETURN VALUE IN INT
    bool IsBillingTermUsedOrNot(string module, int termId);
    string GetLedgerDescription(long ledgerId);
    int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt);
    string GetPurchaseTermScript(int Id = 0);
    Task<bool> PullSalesBillingTermServerToClientByRowCounts(IDataSyncRepository<PT_Term> purchaseTermRepo,
        int callCount);
    // RETURN DATA IN DATA TABLE
    DataTable GetMasterPurchaseTermList(string actionTag, string category, bool status = false, int selectedId = 0);
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);

    // OBJECT FOR THIS FORM
    PT_Term PtTerm { get; set; }
}