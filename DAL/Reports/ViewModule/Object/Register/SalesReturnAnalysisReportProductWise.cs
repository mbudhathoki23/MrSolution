namespace MrDAL.Reports.ViewModule.Object.Register;

public class SalesReturnAnalysisReportProductWise
{
    public long ProductId { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string GroupCode { get; set; }
    public string Group { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGroup { get; set; }
    public decimal? ReturnAltQty { get; set; }
    public string ReturnAltUnit { get; set; }
    public decimal? ReturnQty { get; set; }
    public string ReturnUnit { get; set; }
    public decimal? ReturnAmount { get; set; }
}