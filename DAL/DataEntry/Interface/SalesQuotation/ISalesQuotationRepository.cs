using DatabaseModule.DataEntry.SalesMaster.SalesQuotation;
using System.Collections.Generic;
using System.Data;

namespace MrDAL.DataEntry.Interface.SalesQuotation;

public interface ISalesQuotationRepository
{
    // INSERT UPDATE DELETE
    int SaveSalesQuotation(string actionTag);
    int SalesQuotationTermPosting();

    // RETURN VALUE IN SHORT 
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);

    // DATA TABLE FUNCTION
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);

    // OBJECT FOR THIS FORM
    SQ_Master SqMaster { get; set; }
    List<SQ_Details> DetailsList { get; set; }
    List<SQ_Term> Terms { get; set; }
}