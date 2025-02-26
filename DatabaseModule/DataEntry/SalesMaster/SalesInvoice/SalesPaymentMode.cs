namespace DatabaseModule.DataEntry.SalesMaster.SalesInvoice;

public class SalesPaymentMode
{
    public string SB_Invoice { get; set; }
    public long LedgerId { get; set; }
    public int SerialNo { get; set; }
    public string Payment_Mode { get; set; }
    public decimal Amount { get; set; }
}