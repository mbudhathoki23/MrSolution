using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModule.DataEntry.SalesMaster.SalesInvoice;

public class SalesInvoicePaymentModel
{
    public long LedgerId { get; set; }
    public string InvoiceToName { get; set; }
    public string InvoiceToAddress { get; set; }
    public string InvoiceToPhone { get; set; }
    public string InvoiceToPan { get; set; }
    public string ContactPerson { get; set; }
    public decimal TenderAmount { get; set; }
    public decimal ReturnAmount { get; set; }
    public long? CardLedgerId { get; set; }
    public long? CreditLedgerId { get; set; }
    public bool PrintInvoice { get; set; }
    public string Remarks { get; set; }

    [NotMapped]
    public SalesInvoicePaymentMode PaymentMode { get; set; }
}