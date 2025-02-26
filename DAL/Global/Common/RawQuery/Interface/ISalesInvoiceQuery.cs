namespace MrDAL.Global.Common.RawQuery.Interface;

internal interface ISalesInvoiceQuery
{
    string GetSalesInvoiceMaster(string voucherNo);

    string GetSalesInvoiceDetails(string voucherNo);

    string GetSalesInvoiceTerm(string voucherNo);

    string GetSalesInvoiceProductTerm(string voucherNo);
}