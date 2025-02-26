namespace DatabaseModule.DataEntry.StockTransaction.ProductScheme;

public class Scheme_Details
{
    public int SchemeId { get; set; }
    public int SNo { get; set; }
    public long ProductId { get; set; }
    public int? GroupId { get; set; }
    public int? SubGroupId { get; set; }
    public decimal Qty { get; set; }
    public decimal DiscountPercent { get; set; }
    public decimal DiscountValue { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public decimal SalesRate { get; set; }
    public decimal MrpRate { get; set; }
    public decimal OfferRate { get; set; }
}