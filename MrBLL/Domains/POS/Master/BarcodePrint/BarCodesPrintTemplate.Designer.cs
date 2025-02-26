
using DatabaseModule.Domains.BarcodePrint;

namespace MrBLL.Domains.POS.Master.BarcodePrint
{
    partial class BarCodesPrintTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.panel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.lblBarCode = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode = new DevExpress.XtraReports.UI.XRBarCode();
            this.lblRate = new DevExpress.XtraReports.UI.XRLabel();
            this.lblProduct = new DevExpress.XtraReports.UI.XRLabel();
            this.lblCompany = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panel1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 333.7031F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.MultiColumn.ColumnCount = 3;
            this.Detail.MultiColumn.ColumnSpacing = 25F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            this.Detail.BeforePrint += new DevExpress.XtraReports.UI.BeforePrintEventHandler(this.Detail_BeforePrint);
            // 
            // panel1
            // 
            this.panel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.panel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblBarCode,
            this.xrBarCode,
            this.lblRate,
            this.lblProduct,
            this.lblCompany});
            this.panel1.Dpi = 254F;
            this.panel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 4.453152F);
            this.panel1.Name = "panel1";
            this.panel1.SizeF = new System.Drawing.SizeF(591.1042F, 329.2499F);
            // 
            // lblBarCode
            // 
            this.lblBarCode.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.lblBarCode.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.lblBarCode.AutoWidth = true;
            this.lblBarCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblBarCode.CanGrow = false;
            this.lblBarCode.Dpi = 254F;
            this.lblBarCode.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([PrintText] == true, [PrintedBarCode], null)")});
            this.lblBarCode.Font = new DevExpress.Drawing.DXFont("Tahoma", 8.25F);
            this.lblBarCode.LocationFloat = new DevExpress.Utils.PointFloat(13.19784F, 226.8299F);
            this.lblBarCode.Name = "lblBarCode";
            this.lblBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 5, 0, 0, 254F);
            this.lblBarCode.ProcessNullValues = DevExpress.XtraReports.UI.ValueSuppressType.SuppressAndShrink;
            this.lblBarCode.SizeF = new System.Drawing.SizeF(568.5001F, 40F);
            this.lblBarCode.StylePriority.UseBorders = false;
            this.lblBarCode.StylePriority.UseFont = false;
            this.lblBarCode.StylePriority.UsePadding = false;
            this.lblBarCode.StylePriority.UseTextAlignment = false;
            // 
            // xrBarCode
            // 
            this.xrBarCode.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.xrBarCode.AnchorVertical = ((DevExpress.XtraReports.UI.VerticalAnchorStyles)((DevExpress.XtraReports.UI.VerticalAnchorStyles.Top | DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom)));
            this.xrBarCode.AutoModule = true;
            this.xrBarCode.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrBarCode.Dpi = 254F;
            this.xrBarCode.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[ProductBarCode]")});
            this.xrBarCode.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F);
            this.xrBarCode.LocationFloat = new DevExpress.Utils.PointFloat(11.30199F, 101.9654F);
            this.xrBarCode.Module = 5.08F;
            this.xrBarCode.Name = "xrBarCode";
            this.xrBarCode.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 254F);
            this.xrBarCode.ShowText = false;
            this.xrBarCode.SizeF = new System.Drawing.SizeF(568.5002F, 124.8645F);
            this.xrBarCode.StylePriority.UseBorders = false;
            this.xrBarCode.StylePriority.UseFont = false;
            this.xrBarCode.StylePriority.UsePadding = false;
            this.xrBarCode.Symbology = code128Generator1;
            // 
            // lblRate
            // 
            this.lblRate.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.lblRate.AnchorVertical = DevExpress.XtraReports.UI.VerticalAnchorStyles.Bottom;
            this.lblRate.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblRate.CanGrow = false;
            this.lblRate.Dpi = 254F;
            this.lblRate.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([PrintSalesRate] == true, [SalesRate], null)")});
            this.lblRate.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F);
            this.lblRate.LocationFloat = new DevExpress.Utils.PointFloat(13.19784F, 271.8299F);
            this.lblRate.Multiline = true;
            this.lblRate.Name = "lblRate";
            this.lblRate.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 5, 0, 0, 254F);
            this.lblRate.SizeF = new System.Drawing.SizeF(568.5001F, 50.00003F);
            this.lblRate.StylePriority.UseBorders = false;
            this.lblRate.StylePriority.UseFont = false;
            this.lblRate.StylePriority.UsePadding = false;
            this.lblRate.StylePriority.UseTextAlignment = false;
            this.lblRate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.lblRate.TextFormatString = "{0:N}";
            // 
            // lblProduct
            // 
            this.lblProduct.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.lblProduct.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblProduct.CanShrink = true;
            this.lblProduct.Dpi = 254F;
            this.lblProduct.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([PrintProductName] == true, [ProductName], null)")});
            this.lblProduct.Font = new DevExpress.Drawing.DXFont("Tahoma", 8.25F);
            this.lblProduct.LocationFloat = new DevExpress.Utils.PointFloat(11.30206F, 55.96542F);
            this.lblProduct.Multiline = true;
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 5, 0, 0, 254F);
            this.lblProduct.SizeF = new System.Drawing.SizeF(568.5001F, 45F);
            this.lblProduct.StylePriority.UseBorders = false;
            this.lblProduct.StylePriority.UseFont = false;
            this.lblProduct.StylePriority.UsePadding = false;
            this.lblProduct.StylePriority.UseTextAlignment = false;
            this.lblProduct.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // lblCompany
            // 
            this.lblCompany.AnchorHorizontal = ((DevExpress.XtraReports.UI.HorizontalAnchorStyles)((DevExpress.XtraReports.UI.HorizontalAnchorStyles.Left | DevExpress.XtraReports.UI.HorizontalAnchorStyles.Right)));
            this.lblCompany.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.lblCompany.CanShrink = true;
            this.lblCompany.Dpi = 254F;
            this.lblCompany.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "Iif([PrintCompanyName] == true, [CompanyName], null)")});
            this.lblCompany.Font = new DevExpress.Drawing.DXFont("Arial", 8.25F, DevExpress.Drawing.DXFontStyle.Bold);
            this.lblCompany.LocationFloat = new DevExpress.Utils.PointFloat(11.30206F, 8.96542F);
            this.lblCompany.Multiline = true;
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 5, 0, 0, 254F);
            this.lblCompany.SizeF = new System.Drawing.SizeF(568.5001F, 45F);
            this.lblCompany.StylePriority.UseBorders = false;
            this.lblCompany.StylePriority.UseFont = false;
            this.lblCompany.StylePriority.UsePadding = false;
            this.lblCompany.StylePriority.UseTextAlignment = false;
            this.lblCompany.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 11.91691F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // objectDataSource1
            // 
            this.objectDataSource1.DataSource = typeof(ProductBarCodePrintModel);
            this.objectDataSource1.Name = "objectDataSource1";
            // 
            // BarCodesPrintTemplate
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
            this.DataSource = this.objectDataSource1;
            this.Dpi = 254F;
            this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
            this.Margins = new DevExpress.Drawing.DXMargins(0, 0, 0, 12);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A4;
            this.ReportPrintOptions.DetailCountOnEmptyDataSource = 12;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "22.1";
            ((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion
        private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel panel1;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        public DevExpress.XtraReports.UI.XRBarCode xrBarCode;
        private DevExpress.XtraReports.UI.XRLabel lblRate;
        private DevExpress.XtraReports.UI.XRLabel lblProduct;
        private DevExpress.XtraReports.UI.XRLabel lblCompany;
        private DevExpress.XtraReports.UI.XRLabel lblBarCode;
    }
}
