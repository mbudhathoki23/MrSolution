using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.Master.Interface.LedgerSetup;

public interface ICurrencyRepository
{
    // INSERT UPDATE DELETE
    int SaveCurrency(string actionTag);
    Task<int> SyncCurrencyAsync(string actionTag);
    Task<bool> SyncCurrencyDetailsAsync();
    Task<int> SyncUpdateCurrency(int currencyId = 0);




    Task<bool> GetAndSaveUnSynchronizedCurrencies();
    int SaveCurrencyAuditLog(string actionTag);

    //PULL CURRENCY
    Task<bool> PullCurrencyServerToClientByRowCounts(IDataSyncRepository<Currency> currencyRepository, int callCount);



    public string GetCurrencyScript(int currencyId = 0);
    // RETURN VALUE IN DATA TABLE
    DataTable GetMasterCurrency(string actionTag, int status = 0, int selectedId = 0);


    // OBJECT FOR THIS FORM
    Currency ObjCurrency { get; set; }
}