using System.Data;

namespace MrDAL.DataEntry.Interface;

public interface IPurchaseEntry
{
    // RETURN SYNC ROW VALUE IN SHORT
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);


    // DATA TABLE FUNCTION
    #region --------------- DATATABLE --------------
    DataTable CheckVoucherExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo);
    DataTable CheckVoucherList(string module);
    #endregion --------------- DATATABLE --------------

    // DATA SET FUNCTION

    #region --------------- DATATABLE --------------
    DataSet ReturnPurchaseInvoiceDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseIndentDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseOrderDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseChallanDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseReturnDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseAdditionalDetailsInDataSet(string voucherNo);
    DataSet ReturnPurchaseExpiryBreakageDetailsInDataSet(string voucherNo);

    #endregion
}

