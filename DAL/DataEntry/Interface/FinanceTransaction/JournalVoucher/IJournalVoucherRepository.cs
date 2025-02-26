using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.FinanceTransaction.JournalVoucher;

public interface IJournalVoucherRepository
{
    // SAVE UPDATE DELETE
    int SaveJournalVoucher(string actionTag);
    int JournalVoucherAccountPosting();

    Task<int> SyncJournalVoucherAsync(string actionTag);

    // RETURN VALUE IN DATA TABLE
    DataTable IsCheckVoucherNoExits(string tableName, string tableVoucherNo, string inputVoucherNo);
    DataSet ReturnJournalVoucherInDataSet(string voucherNo);

    // OBJECT FOR THIS FORM
    JV_Master JvMaster { get; set; }
    List<JV_Details> JvDetailsList { get; set; }
}