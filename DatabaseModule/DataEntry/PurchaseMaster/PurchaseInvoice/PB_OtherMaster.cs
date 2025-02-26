namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;

public class PB_OtherMaster
{
    public string PAB_Invoice { get; set; }
    public string PPNo { get; set; }
    public System.DateTime? PPDate { get; set; }
    public decimal? TaxableAmount { get; set; }
    public decimal? VatAmount { get; set; }
    public string CustomAgent { get; set; }
    public string Transportation { get; set; }
    public string VechileNo { get; set; }
    public string Cn_No { get; set; }
    public System.DateTime? Cn_Date { get; set; }
    public string BankDoc { get; set; }
}