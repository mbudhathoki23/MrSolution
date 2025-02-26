using DevExpress.XtraPrinting;

namespace DatabaseModule.Domains.BarcodePrint;

public class ProductBarCodePrintModel
{
    public long ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductBarCode { get; set; }
    public string PrintedBarCode { get; set; }
    public string ProductCategory { get; set; }
    public string SalesRate { get; set; }
    public decimal SalesRateDecimal { get; set; }
    public string CompanyName { get; set; }
    public uint PrintCount { get; set; }
    public bool PrintText { get; set; } = true;
    public bool PrintCompanyName { get; set; } = true;
    public bool PrintSalesRate { get; set; } = true;
    public bool PrintProductName { get; set; } = true;
    public PrintTextType PrintTextType { get; set; } = PrintTextType.BarCode;
    public TextAlignment TextAlignment { get; set; } = TextAlignment.MiddleCenter;
}