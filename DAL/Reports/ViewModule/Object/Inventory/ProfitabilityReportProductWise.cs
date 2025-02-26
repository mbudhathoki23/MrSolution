namespace MrDAL.Reports.ViewModule.Object.Inventory;

public class ProfitabilityReportProductWise
{
    public string Product_Id { get; set; }
    public string PName { get; set; }
    public string PShortName { get; set; }
    public string PGrpId { get; set; }
    public string GrpName { get; set; }
    public string PSubGrpId { get; set; }
    public string SubGrpName { get; set; }
    public string Qty { get; set; }
    public string UOM { get; set; }
    public string Cogs { get; set; }
    public string SalesAmount { get; set; }
    public string Profit { get; set; }
    public string Ratio { get; set; }
    public int IsGroup { get; set; }
}