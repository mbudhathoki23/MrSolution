using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.SalesReturn;

public interface ISalesReturn
{
    // INSERT UPDATE DELETE
    int SaveSalesReturn(string actionTag);

    int SalesReturnTermPosting();
    int SalesReturnAccountPosting();
    int SalesReturnStockPosting();
    Task<int> UpdateSalesStockValue();

    Task<int> SyncSalesReturnAsync(bool isPosReturn, string actionTag);
    // RETURN VALUE IN SHORT 
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);

    // DATA TABLE FUNCTION
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);

    // OBJECT FOR THIS FORM
    SR_Master SrMaster { get; set; }
    List<SR_Details> DetailsList { get; set; }
    List<SR_Term> Terms { get; set; }
    List<SR_Master_OtherDetails> SrOther { get; set; }
}