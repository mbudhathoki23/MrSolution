using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.Master.ProductSetup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.SalesMaster;

public interface ISalesInvoiceRepository
{
    // INSERT UPDATE DELETE
    int SaveSalesInvoice(string actionTag);
    int SaveRestroInvoice(string actionTag);
    int SavePointOfSalesInvoiceWithQuery(string queryString, string actionTag);
    int SalesTermPostingAsync();
    int SalesInvoiceAccountPosting();
    int SalesInvoiceStockPosting();

    // TASK VALUE RETURN INT 
    Task<int> SyncSalesInvoiceAsync(string actionTag);



    // RETURN VALUE IN SHORT 
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);



    // DATA TABLE FUNCTION
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);
    DataSet ReturnTempSalesInvoiceDetailsInDataSet(string voucherNo);



    // STRING VALUE RETURN
    string GetInvoicePaymentMode(long ledgerId);
    (string invoice, string avtInvoice) GetPointOfSalesDesign();



    // OBJECT FOR THIS FORM
    SB_Master SbMaster { get; set; }
    List<SB_Details> DetailsList { get; set; }
    List<SB_Term> Terms { get; set; }
    List<SB_ExchangeDetails> SB_ExchangeDetails { get; set; }
    SB_Master_OtherDetails SbOther { get; set; }
    List<ProductAddInfo> AddInfos { get; set; }
}