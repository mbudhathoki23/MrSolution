using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.PurchaseMaster;

public interface IPurchaseChallanReturn
{
    // INSERT UPDATE DELETE
    short ReturnMaxSyncRowVersion(string module, string vno);
    int SavePurchaseChallanReturn(string actionTag);
    Task<int> SyncPurchaseChallanReturnAsync(string actionTag);

    // RETURN SYNC ROW VALUE IN SHORT
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);

    // RETURN VALUE IN DATA TABLE
    DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo);
    DataSet ReturnPurchaseChallanReturnDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseChallanDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo);

    // OBJECT FOR THIS FORM
    PCR_Master PcrMaster { get; set; }
    PC_Master PcMaster { get; set; }
    public List<PCR_Details> DetailsList { get; set; }
    public List<PCR_Term> Terms { get; set; }
}