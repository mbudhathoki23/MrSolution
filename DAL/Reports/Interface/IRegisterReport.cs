using MrDAL.Reports.ViewModule;
using System.Data;

namespace MrDAL.Reports.Interface;

public interface IRegisterReport
{
    #region --------------- OBJECT ---------------

    VmRegisterReports GetReports { get; set; }

    #endregion --------------- OBJECT ---------------

    // ANALYSIS REPORTS

    #region --------------- ANALYSIS REPORT ---------------

    DataTable GetTopNRegisterReport();

    DataTable GetSalesAnalysisReport();

    DataTable GetPartyAgingReport(bool isCustomer);

    #endregion --------------- ANALYSIS REPORT ---------------

    // VAT REGISTER REPORT

    #region --------------- VAT REGISTER REPORT ---------------

    DataTable GetVatRegisterTransactionValue();

    DataTable GetVatRegisterNormal();

    DataTable GetPurchaseVatRegisterDetailsReport();

    // SALES VAT REGISTER
    DataTable GetSalesVatRegisterDateWiseReport();

    DataTable GetSalesVatRegisterReportMonthly();

    DataTable GetSalesVatRegisterCustomer();

    DataTable GetSalesVatRegisterProductWise();

    DataTable GetSalesVatRegisterSummary();

    DataTable GetSalesVatRegisterVoucherWise();

    // MATERIALIZE VIEW REGISTER
    DataTable GetMaterializeViewRegister();

    DataTable GetEntryLogRegister();

    DataTable GetPurchaseSalesTermName(bool isPurchase);

    #endregion --------------- VAT REGISTER REPORT ---------------

    // SALES REGISTER REPORTS

    #region --------------- SALES REGISTER REPORTS ---------------

    //SALES REGISTER SUMMARY REPORT

    #region ---------- SUMMARY ----------

    DataTable GetSalesQuotationRegisterSummary();

    DataTable GetSalesOrderRegisterSummary();

    DataTable GetSalesChallanRegisterSummary();

    DataTable GenerateSalesInvoiceRegisterSummaryReports();

    DataTable GetSalesInvoiceRegisterInvoiceTypeSummary();

    DataTable GetSalesReturnRegisterSummary();

    #endregion ---------- SUMMARY ----------

    //SALES REGISTER DETAILS REPORT

    #region ---------- DETAILS ----------

    DataTable GetSalesQuotationRegisterDetails();

    DataTable GetSalesOrderRegisterDetails();

    DataTable GetSalesChallanRegisterDetails();

    DataTable GetSalesInvoiceRegisterDetails();

    DataTable GetSalesReturnRegisterDetails();

    #endregion ---------- DETAILS ----------

    #endregion --------------- SALES REGISTER REPORTS ---------------

    //PURCHASE REGISTER REPORT

    #region --------------- PURCHASE REGISTER REPORT ---------------

    // PURCHASE REGISTER DETAILS REPORT

    #region **----- DETAILS -----**

    DataTable GetPurchaseIndentRegisterDetails();

    DataTable GetPurchaseOrderRegisterDetails();

    DataTable GetPurchaseChallanRegisterDetails();

    DataTable GetPurchaseInvoiceRegisterDetails();

    DataTable GetPurchaseReturnRegisterDetails();

    #endregion **----- DETAILS -----**

    // PURCHASE REGISTER SUMMARY REPORT

    #region **----- SUMMARY -----**

    DataTable GetPurchaseIndentRegisterSummary();

    DataTable GetPurchaseOrderRegisterSummary();

    DataTable GetPurchaseChallanRegisterSummary();

    DataTable GetPurchaseInvoiceRegisterSummary();

    DataTable GetPurchaseReturnRegisterSummary();

    #endregion **----- SUMMARY -----**

    #endregion --------------- PURCHASE REGISTER REPORT ---------------

    // OUTSTANDING REPORTS

    #region --------------- OUTSTANDING REPORT ---------------

    DataTable GetPartyOutstandingDetailsReport(bool isCustomer);

    DataTable GetPartyOutstandingSummaryReport(bool isCustomer);

    DataTable GetOutstandingSummaryReport();

    DataTable GetOutstandingDetailsReport();

    #endregion --------------- OUTSTANDING REPORT ---------------
}