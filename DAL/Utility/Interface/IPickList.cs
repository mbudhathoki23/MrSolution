using System.Data;

namespace MrDAL.Utility.Interface;

public interface IPickList
{
    /// <summary>
    ///     SETUP MASTER LIST
    /// </summary>
    /// <returns></returns>

    #region --------------- SETUP MASTER LIST ---------------

    DataTable GetCompanyList();

    #endregion --------------- SETUP MASTER LIST ---------------

    /// <summary>
    ///     LIST OF MASTER
    /// </summary>
    /// <param name="actionTag"></param>
    /// <param name="category"></param>
    /// <param name="status"></param>
    /// <param name="isPrimary"></param>
    /// <returns></returns>

    #region --------------- FINANCE LIST OF MASTER ---------------

    DataTable GetAccountGroupList(string actionTag, string category, int status = 0, bool isPrimary = false);

    DataTable GetCounterList(string actionTag, bool isActive = false);

    DataTable GetGeneralLedgerList(string actionTag, string category, string loginDate, bool status);

    #endregion --------------- FINANCE LIST OF MASTER ---------------

    /// <summary>
    ///     PURCHASE  REGISTER VOUCHER LIST
    /// </summary>
    /// <param name="voucherType"></param>
    /// <returns></returns>

    #region --------------- PURCHASE  REGISTER ---------------

    DataTable ReturnPurchaseIndentVoucherNoList(string voucherType);

    DataTable ReturnPurchaseOrderVoucherNoList(string voucherType);

    DataTable ReturnPurchaseChallanVoucherNoList(string voucherType);

    DataTable ReturnPurchaseInvoiceVoucherNoList(string voucherType);

    DataTable ReturnPurchaseReturnVoucherNoList(string voucherType);

    DataTable ReturnPurchaseAdditionalVoucherNoList(string voucherType);

    #endregion --------------- PURCHASE  REGISTER ---------------

    /// <summary>
    ///     SALES REGISTER VOUCHER LIST
    /// </summary>
    /// <param name="voucherType"></param>
    /// <returns></returns>

    #region --------------- SALES REGISTER ---------------

    DataTable ReturnSalesQuotationVoucherNoList(string voucherType);

    DataTable ReturnSalesHoldVoucherNoList(string voucherType);

    #endregion --------------- SALES REGISTER ---------------
}