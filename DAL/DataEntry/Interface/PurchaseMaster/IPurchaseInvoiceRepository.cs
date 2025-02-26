using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using DatabaseModule.Master.ProductSetup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseInvoice
{
    // INSERT UPDATE DELETE
    int SavePurchaseInvoice(string actionTag);
    int PurchaseInvoiceTermPosting();
    int PurchaseInvoiceAccountDetailsPosting();
    int PurchaseInvoiceStockDetailsPosting();
    Task<int> SyncPurchaseInvoiceAsync(string actionTag);

    // DATA TABLE FUNCTION
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);


    // OBJECT FOR THIS FORM
    PB_Master PbMaster { get; set; }
    List<PB_Details> DetailsList { get; set; }
    public List<PB_Term> Terms { get; set; }
    public List<ProductAddInfo> AddInfos { get; set; }
}