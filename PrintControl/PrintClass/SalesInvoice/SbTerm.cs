namespace PrintControl.PrintClass.SalesInvoice;

public class SbTerm
{
    public string VOUCHER_NO { get; set; }
    public int VOUCHER_SNO { get; set; }
    public decimal? DISCOUNT { get; set; }
    public decimal? SPECIAL_DISCOUNT { get; set; }
    public decimal? SERVICE_CHARGE { get; set; }
    public decimal? VAT { get; set; }
}