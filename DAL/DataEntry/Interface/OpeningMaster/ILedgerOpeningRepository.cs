using DatabaseModule.DataEntry.OpeningMaster;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.OpeningMaster;

public interface ILedgerOpeningRepository
{
    // SAVE UPDATE DELETE
    int SaveLedgerOpening(string actionTag);
    int LedgerOpeningAccountPosting();


    Task<int> SyncLedgerOpeningAsync(string actionTag);
    Task<bool> SyncLedgerOpeningDetailsAsync();
    Task<int> SyncUpdateLedgerOpening(int openingId = 0);


    // RETURN VALUE IN SHORT 
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);


    //PULL LEDGER OPENING
    Task<bool> PullAccountGroupsServerToClientByRowCounts(IDataSyncRepository<LedgerOpening> ledgerOpeningRepo, int callCount);



    // RETURN VALUE IN DATA TABLE
    public string GetLedgerOpeningScript(int openingId = 0);
    DataSet ReturnOpeningLedgerVoucherInDataSet(string voucherNo);



    // OBJECT FOR THIS FORM
    LedgerOpening GetOpening { get; set; }
    List<LedgerOpening> Details { get; set; }
}