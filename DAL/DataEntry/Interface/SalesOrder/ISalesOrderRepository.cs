using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface.SalesOrder;

public interface ISalesOrderRepository
{
    // INSERT UPDATE DELETE
    int SaveSalesOrder(string actionTag);
    int SalesOrderTermPosting();

    Task<int> SyncSalesOrderAsync(string actionTag);
    string GetSalesOrderScript(string voucherNo = "");

    Task<bool> PullSalesOrderServerToClientByRowCounts(IDataSyncRepository<SO_Master> salesOrderRepository, int callCount);

    #region ----------- RESTRO ORDER ------------

    int UpdateRestroOrder(bool isQty = false);

    int UpdateRestaurantOrderCancel();

    int SaveRestroOrderCancel(string actionTag);

    #endregion

    // RETURN VALUE IN SHORT 
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);

    // DATA TABLE FUNCTION
    DataTable CheckRefVoucherNo(string action, long ledgerId, string txtRefVno, string voucherNo);

    // OBJECT FOR THIS FORM
    SO_Master SoMaster { get; set; }
    List<SO_Details> DetailsList { get; set; }
    List<SO_Term> Terms { get; set; }
}