namespace MrDAL.Models.Custom;

public class SalesInvoiceProductModel
{
    public long ProductId { get; set; }
    public string BarCode { get; set; }
    public string ProductName { get; set; }
    public string ProductShortName { get; set; }
    public decimal BalanceQty { get; set; }
    public string UnitCode { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SalesRate { get; set; }
    public string GroupName { get; set; }
    public string SubGroupName { get; set; }
    public int? UnitId { get; set; }
    public int? AltUnitId { get; set; }
    public string AltUnitCode { get; set; }
    public bool HasAltUnit => AltUnitId > 0;
    public decimal? QtyConversionRate { get; set; }
    public decimal? AltQtyConversionRate { get; set; }
    public decimal MrpRate { get; set; }
    public decimal TradeRate { get; set; }
    public decimal WholesaleRate { get; set; }
    public decimal RetailRate { get; set; }
    public decimal DealerRate { get; set; }
    public decimal ResellerRate { get; set; }
    public decimal TaxPercent { get; set; }
    public bool Taxable => TaxPercent > 0;
}