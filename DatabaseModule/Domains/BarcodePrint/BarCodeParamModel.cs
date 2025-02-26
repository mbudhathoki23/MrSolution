using DevExpress.XtraPrinting.BarCode.Native;

namespace DatabaseModule.Domains.BarcodePrint;

public class BarCodeParamModel
{
    public BarCodeSymbology Symbology { get; set; }
    public int ColumnCount { get; set; }
    public int RowHeight { get; set; }
    public int ColumnWidth { get; set; }
    public int ColumnSpacing { get; set; }
    public decimal BarModule { get; set; }
    public bool AutoModule { get; set; }
    public bool DrawBorder { get; set; }
    public int? AutoFitPageWidth { get; set; }
    public bool FillBarSpace { get; set; }
    public string CompanyTextAlignment { get; set; }
    public string ProductTextAlignment { get; set; }
    public string RateTextAlignment { get; set; }
    public string BarcodeTextAlignment { get; set; }
    public BarCodeLabelProperty CompanyProperty { get; set; }
    public BarCodeLabelProperty RateProperty { get; set; }
    public BarCodeLabelProperty ProductNameProperty { get; set; }
    public BarCodeLabelProperty PrintedBarcodeProperty { get; set; }
}