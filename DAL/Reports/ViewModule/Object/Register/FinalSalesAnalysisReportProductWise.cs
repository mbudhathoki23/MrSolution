using MrDAL.Core.Extensions;

namespace MrDAL.Reports.ViewModule.Object.Register;

public class FinalSalesAnalysisReportProductWise
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

    public string ReturnAltQty { get; set; }
    public string ReturnAltUnit { get; set; }
    public string ReturnQty { get; set; }
    public string ReturnUnit { get; set; }
    public string ReturnAmount { get; set; }

    public string NetSalesAltQty => (SalesAltQty.GetDecimal() - ReturnAltQty.GetDecimal()).ToString();
    public string NetSalesAltUnit => SalesAltUnit;
    public string NetSalesQty => (SalesQty.GetDecimal() - ReturnQty.GetDecimal()).ToString();
    public string NetSalesUnit => SalesUnit;
    public string NetSalesAmount => (SalesAmount.GetDecimal() - ReturnAmount.GetDecimal()).ToString();
    public int IsGroup { get; set; } = 0;
}