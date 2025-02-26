using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.SalesChallan;

public interface ISalesChallanRepository
{
    // INSERT UPDATE DELETE
    int SaveSalesChallan(string actionTag);
    int SalesChallanTermPosting();
    int SalesChallanStockPosting();
    Task<int> SyncSalesChallanAsync(string actionTag);

    // RETURN VALUE IN SHORT 
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);
    // DATA TABLE FUNCTION
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);


    // OBJECT FOR THIS FORM
    SC_Master ScMaster { get; set; }
    List<SC_Details> DetailsList { get; set; }
    List<SC_Term> Terms { get; set; }
    List<SC_Master_OtherDetails> ScOther { get; set; }
    SB_Master SbMaster { get; set; }

    SB_Master_OtherDetails SbOther { get; set; }
    SR_Master SrMaster { get; set; }
}