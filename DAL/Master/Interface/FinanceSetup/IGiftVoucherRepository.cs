using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.FinanceSetup;

public interface IGiftVoucherRepository
{
    // INSERT UPDATE DELETE
    int SaveGiftVoucherList(string actionTag);
    Task<int> SyncGiftVoucherListAsync(string actionTag);
    DataTable GetGiftVoucherNumberInformation(int selectedId);
    Task<bool> PullCurrencyServerToClientByRowCounts(IDataSyncRepository<GiftVoucherList> giftRepository, int callCount);
    string GetGiftVoucherScript(int currencyId = 0);

    // OBJECT FOR THIS FORM
    GiftVoucherList ObjGiftVoucher { get; set; }
}