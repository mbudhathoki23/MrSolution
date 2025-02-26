using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.FinanceTransaction.PostDateCheque;

public interface IPostDateChequeRepository
{
    // INSERT UPDATE DELETE
    int SaveProvisionCheque(string actionTag, string ledgerDesc);
    int PdcAccountPosting();

    Task<int> SyncProvisionChequeAsync(string actionTag, string ledgerDesc);


    // RETURN VALUE OF DATA TABLE
    DataTable GetPostDatedChequeVoucher(string voucherNo);


    // OBJECT FOR THIS FORM
    DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque.PostDateCheque PdcMaster { get; set; }

}