namespace DatabaseModule.DataEntry.StockTransaction.StockAdjustment;

public class STA_Details
{
    public string StockAdjust_No { get; set; }
    public int Sno { get; set; }
    public long ProductId { get; set; }
    public int? GodownId { get; set; }
    public int? DepartmentId { get; set; }
    public string AdjType { get; set; }
    public decimal AltQty { get; set; }
    public int? AltUnitId { get; set; }
    public decimal Qty { get; set; }
    public int? UnitId { get; set; }
    public decimal AltStockQty { get; set; }
    public decimal StockQty { get; set; }
    public decimal Rate { get; set; }
    public decimal NetAmount { get; set; }
    public string AddDesc { get; set; }
    public decimal StConvRatio { get; set; }
    public string PhyStkNo { get; set; }
    public int? PhyStkSno { get; set; }
}