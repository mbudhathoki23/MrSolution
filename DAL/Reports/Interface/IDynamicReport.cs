using MrDAL.Reports.ViewModule;
using System.Data;

namespace MrDAL.Reports.Interface;

public interface IDynamicReport
{
    VmDynamicReportTemplate Model { get; set; }
    VmRegisterReports GetReports { get; set; }

    int SaveTemplate();

    string IsExistsTemplateName(string reportType, string v);

    DataTable ListTemplateType(string reportCategory, int templateId, string reportsType);

    DataTable ListAllTemplate();

    // SALES INVOICE REGISTER REPORTS
    DataTable GetSalesInvoiceRegisterSummaryReports();

    DataTable GetSalesInvoiceRegisterDetailsReports();

    DataTable GetSalesInvoiceRegisterPartialPaymentReport();

    DataTable GetSalesInvoiceRegisterYearWiseReport();

    DataSet GetSalesInvoiceMasterDetailsReports();

    DataTable GetSalesInvoiceRegisterProductLedgerReports();

    DataTable GetSalesInvoiceRegisterTableWiseReports();

    DataTable GetSalesInvoiceVatRegisterReports();

    DataTable GetSalesInvoiceVatRegisterIncludeReturnReports();

    DataTable GetSalesInvoiceVatRegisterCustomerWise();

    DataTable GetSalesReturnVatRegisterReports();

    // PURCHASE INVOICE REGISTER REPORTS
    DataTable GetPurchaseInvoiceRegisterSummaryReports();

    DataTable GetPurchaseInvoiceRegisterDetailsReports();

    DataSet GetPurchaseInvoiceMasterDetailsReports();

    DataTable GetPurchaseInvoiceVatRegisterReports();

    DataTable GetPurchaseInvoiceVatRegisterIncludeReturnReports();

    DataTable GetPurchaseReturnVatRegisterReports();

    // FINANCE REPORTS
    DataTable GetPostDatedCheque(string status = "Due");

    DataTable GetCashBankVoucherDetails();
}