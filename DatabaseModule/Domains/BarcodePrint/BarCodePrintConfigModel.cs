using DevExpress.XtraPrinting.BarCode.Native;
using System;
using System.Drawing;
using System.Drawing.Printing;

namespace DatabaseModule.Domains.BarcodePrint;

[Serializable]
public class BarCodePrintConfigModel
{
    public PaperKind PaperKind { get; set; }
    public SizeF PaperSize { get; set; }
    public string PrinterName { get; set; }
    public Margins Margins { get; set; }
    public BarCodeSymbology Symbology { get; set; }
    public int ColumnCount { get; set; }
    public decimal RowHeight { get; set; }
    public uint ColumnWidth { get; set; }
    public decimal ColumnSpacing { get; set; }
    public decimal BarModule { get; set; }
    public bool AutoAdjustBarModule { get; set; }
    public int? AutoFitPagesWidth { get; set; }
    public bool DrawBorder { get; set; }
    public bool FillBarSpace { get; set; }
    public bool IsPrintBarcode { get; set; }
    public string PrintedBarCode { get; set; }
    public string ProductBarCode { get; set; }
    public string CompanyName { get; set; }
    public bool IsPrintCompany { get; set; }
    public bool IsPrintProductName { get; set; }
    public bool IsPrintSalesRate { get; set; }
    public string CompanyAlignment { get; set; }
    public string ProductAlignment { get; set; }
    public string RateAlignment { get; set; }
    public string BarcodeAlignment { get; set; }
    public string RateOption { get; set; }
    public BarCodeLabelProperty LblCompanyProperty { get; set; } = new();
    public BarCodeLabelProperty LblRateProperty { get; set; } = new();
    public BarCodeLabelProperty LblProductNameProperty { get; set; } = new();
}