namespace DatabaseModule.DataEntry.SalesMaster.SalesInvoice;

public class InvoiceSettlement
{
    public string SB_Invoice { get; set; }
    public long LedgerId { get; set; }
    public string PaymentMode { get; set; }
    public decimal Amount { get; set; }
    public long? GiftVoucherNo { get; set; }
    public decimal GiftVoucherAmount { get; set; }
}