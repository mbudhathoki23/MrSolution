using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseChallanRepository
{
    // RETURN VALUE IN DATA TABLE
    DataSet ReturnSalesInvoiceDetailsInDataSet(string voucherNo);
    DataTable CheckVoucherExitsOrNot(string tableName, string voucherNo, string inputVoucherNo);
    DataSet ReturnPurchaseChallanDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo);


    // INSERT UPDATE DELETE
    short ReturnMaxSyncRowVersion(string module, string vno);
    int SavePurchaseChallan(string actionTag);
    Task<int> SyncPurchaseChallanAsync(string actionTag);
    Task<bool> SyncPurchaseChallanDetailsAsync();
    int PurchaseChallanStockPosting();


    // OBJECT FOR THIS FORM
    PC_Master PcMaster { get; set; }
    List<PC_Details> DetailsList { get; set; }
    List<PC_Term> ChallanTerms { get; set; }
}