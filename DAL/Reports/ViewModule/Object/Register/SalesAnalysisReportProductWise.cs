namespace MrDAL.Reports.ViewModule.Object.Register;

public class SalesAnalysisReportProductWise
{
    public long ProductId { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string GroupCode { get; set; }
    public string Group { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGroup { get; set; }
    public string SalesAltQty { get; set; }
    public string SalesAltUnit { get; set; }
    public string SalesQty { get; set; }
    public string SalesUnit { get; set; }
    public string SalesAmount { get; set; }
    public int IsGroup { get; set; } = 0;
}