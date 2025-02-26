namespace DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;

public class StockIssue_Details
{
    public string VoucherNo { get; set; }
    public int SerialNo { get; set; }
    public long? ProductId { get; set; }
    public int? CostCenterId { get; set; }
    public int? GodownId { get; set; }
    public decimal AltQty { get; set; }
    public int? AltUnitId { get; set; }
    public decimal Qty { get; set; }
    public int? UnitId { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public decimal? ConvFactor { get; set; }
    public decimal? BomQty { get; set; }
    public string Source { get; set; }
}