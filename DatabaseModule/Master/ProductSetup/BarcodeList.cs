namespace DatabaseModule.Master.ProductSetup;

public class BarcodeList
{
    public long ProductId { get; set; }
    public string Barcode { get; set; }
    public decimal SalesRate { get; set; }
    public decimal MRP { get; set; }
    public decimal Trade { get; set; }
    public decimal Wholesale { get; set; }
    public decimal Retail { get; set; }
    public decimal Dealer { get; set; }
    public decimal Resellar { get; set; }
    public int UnitId { get; set; }
    public int? AltUnitId { get; set; }
    public bool DailyRateChange { get; set; }
}