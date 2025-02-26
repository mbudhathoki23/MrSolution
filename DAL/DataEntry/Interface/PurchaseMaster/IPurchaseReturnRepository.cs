using DatabaseModule.DataEntry.PurchaseMaster.PurchaseReturn;
using DatabaseModule.Master.ProductSetup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseReturn
{
    // int SaveCashBankVoucher(string actionTag);
    int SavePurchaseReturnInvoice(string actionTag);
    int PurchaseReturnInvoiceTermPosting();
    int PurchaseReturnInvoiceAccountDetailsPosting();
    int PurchaseReturnInvoiceStockDetailsPosting();
    Task<int> SyncPurchaseReturnAsync(string actionTag);

    // RETURN SYNC ROW VALUE IN SHORT
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);

    // DATA TABLE FUNCTION
    DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo);
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);



    // OBJECT FOR THIS FORM
    PR_Master PrMaster { get; set; }
    List<PR_Details> DetailsList { get; set; }
    List<PR_Term> Terms { get; set; }
    List<ProductAddInfo> AddInfos { get; set; }
}