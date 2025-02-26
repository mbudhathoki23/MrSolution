using DatabaseModule.DataEntry.PurchaseMaster.PurchaseAdditional;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseAdditionalRepository
{
    // INSERT UPDATE DELETE
    int SavePurchaseInvoice(string actionTag);
    int AuditLogPurchaseInvoice(string actionTag);
    int SaveDataInDatabase(StringBuilder stringBuilder);
    Task<int> SyncPurchaseInvoiceAsync(string actionTag);
    int PurchaseInvoiceTermPosting();
    int PurchaseInvoiceAccountDetailsPosting();
    int PurchaseInvoiceStockDetailsPosting();

    // RETURN VALUE IN DATA TABLE
    DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo);
    DataTable GetPurchaseTermInformation(int termId);

    // OBJECT FOR THIS FORM
    PAB_Master PabMaster { get; set; }
    PB_Master PbMaster { get; set; }
    List<PAB_Details> DetailsList { get; set; }
}