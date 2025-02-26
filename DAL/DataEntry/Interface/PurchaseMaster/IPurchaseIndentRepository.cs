using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseIndent;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseIndentRepository
{
    // INSERT UPDATE DELETE
    int SaveCashBankVoucher(string actionTag);
    Task<int> SyncCashBankVoucherAsync(string actionTag);
    int CashBankAccountPosting();
    int SavePurchaseInvoice(string actionTag);
    int PurchaseInvoiceAccountDetailsPosting();
    int PurchaseInvoiceStockDetailsPosting();
    int PurchaseInvoiceTermPosting();


    // RETURN VALUE IN DATA TABLE
    DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo);
    DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo);


    // OBJECT FOR THIS FORM
    PIN_Master PinMaster { get; set; }
    List<PIN_Details> DetailsList { get; set; }
    CB_Master CbMaster { get; set; }
    CB_Details CbDetails { get; set; }
    PB_Master PbMaster { get; set; }

}