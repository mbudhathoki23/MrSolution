using DatabaseModule.Domains.BarcodePrint;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode.Native;
using DevExpress.XtraReports.UI;
using MrDAL.Core.Extensions;
using System.Drawing;

namespace MrBLL.Domains.POS.Master.BarcodePrint;

public partial class BarCodesPrintTemplate : XtraReport
{
    public BarCodesPrintTemplate(BarCodeParamModel paramModel)
    {
        InitializeComponent();
        xrBarCode.Module = (float)paramModel.BarModule;
        xrBarCode.Symbology = BarCodeGeneratorFactory.Create(paramModel.Symbology);
        xrBarCode.AutoModule = paramModel.AutoModule;
        Detail.MultiColumn.Mode = MultiColumnMode.UseColumnCount;
        Detail.MultiColumn.ColumnCount = paramModel.ColumnCount;
        Detail.MultiColumn.ColumnSpacing = paramModel.ColumnSpacing;
        Detail.MultiColumn.ColumnWidth = paramModel.ColumnWidth;
        Detail.HeightF = paramModel.RowHeight + paramModel.ColumnSpacing;
        panel1.WidthF = paramModel.ColumnWidth;
        panel1.HeightF = paramModel.RowHeight;
        panel1.Borders = paramModel.DrawBorder ? BorderSide.All : BorderSide.None;

        lblCompany.SetDevExpressTextAlignment(paramModel.CompanyTextAlignment);
        lblProduct.SetDevExpressTextAlignment(paramModel.ProductTextAlignment);
        lblBarCode.SetDevExpressTextAlignment(paramModel.BarcodeTextAlignment);
        lblRate.SetDevExpressTextAlignment(paramModel.RateTextAlignment);

        if (paramModel.ProductNameProperty != null) lblProduct.Font = new Font(new FontFamily(paramModel.ProductNameProperty.FontName), paramModel.ProductNameProperty.FontSize, paramModel.ProductNameProperty.FontStyle);
        if (paramModel.CompanyProperty != null) lblCompany.Font = new Font(new FontFamily(paramModel.CompanyProperty.FontName), paramModel.CompanyProperty.FontSize, paramModel.CompanyProperty.FontStyle);
        if (paramModel.RateProperty != null) lblRate.Font = new Font(new FontFamily(paramModel.RateProperty.FontName), paramModel.RateProperty.FontSize, paramModel.RateProperty.FontStyle);

        //var availSpace = panel1.HeightF - lblCompany.HeightF - lblProduct.HeightF - lblRate.HeightF;
        //if (paramModel.FillBarSpace)
        //{
        //    while (xrBarCode.Validate() != BarCodeError.None && xrBarCode.HeightF < availSpace)
        //        xrBarCode.HeightF += 100;
        //    while (xrBarCode.Validate() == BarCodeError.None && xrBarCode.HeightF < availSpace)
        //        xrBarCode.HeightF -= 10;
        //    while (xrBarCode.Validate() != BarCodeError.None && xrBarCode.HeightF < availSpace)
        //        xrBarCode.HeightF++;
        //}
    }

    private void Detail_BeforePrint(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (GetCurrentRow() is not ProductBarCodePrintModel model) return;
        //lblBarCode.TextAlignment = model.TextAlignment;
    }
}