namespace MrDAL.Models.Custom;

public class ImportProductEModel
{
    public string BarCode { get; set; }
    public string Description { get; set; }
    public string BarCode1 { get; set; }
    public string BarCode2 { get; set; }
    public string Type { get; set; }
    public string Group { get; set; }
    public int GroupId { get; set; }
    public string SubGroup { get; set; }
    public int SubGroupId { get; set; }
    public string UOM { get; set; }
    public int UOMId { get; set; }
    public decimal BuyRate { get; set; }
    public decimal SalesRate { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Margin { get; set; }
    public decimal TradePrice { get; set; }
    public decimal Mrp { get; set; }
}