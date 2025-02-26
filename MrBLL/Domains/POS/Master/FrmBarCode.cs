using DatabaseModule.Domains.BarcodePrint;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.BarCode.Native;
using DevExpress.XtraReports.UI;
using MrBLL.Domains.POS.Master.BarcodePrint;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Dialogs;
using MrDAL.Master;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmBarCode : MrForm
{
    private IList<ProductBarCodeInfoModel> _allProducts;
    private bool _isPrintBarcode = true, _isPrintCompany = true, _isPrintProductName = true, _isPrintSalesRate = true;

    private string _companyName, _fixedCompanyName
        , _productBarCode = BarcodeSource.BarCode3.ToString(), _printedBarCode = BarcodeSource.BarCode.ToString()
        , _companyAlign = "Left", _productNameAlign = "Left", _rateAlign = "Left", _barcodeAlign = "Center", _rateOption = "Rate";

    private BarCodeSymbology _barcodeSymbology = BarCodeSymbology.Code128;

    public FrmBarCode()
    {
        InitializeComponent();

        _allProducts = new List<ProductBarCodeInfoModel>();

        // Add a search button to the Text Box
        var button = new Button
        {
            Size = new Size(25, ClientSize.Height + 2),
            Dock = DockStyle.Right,
            Cursor = Cursors.Default,
            Image = Properties.Resources.search16,
            ImageAlign = ContentAlignment.MiddleCenter,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            FlatAppearance = { BorderSize = 0, MouseDownBackColor = Color.DarkGray }
        };
        txtSearchProduct.Controls.Add(button);
        SendMessage(txtSearchProduct.Handle, 0x1501, 1, "Search products or barcodes");

        gridBarcodes.ReadOnly = gridProducts.ReadOnly = false;
        // make the columns readonly
        Column2.ReadOnly = Column3.ReadOnly = Column4.ReadOnly =
            Column1.ReadOnly = Column5.ReadOnly = Column8.ReadOnly = true;

        cbxProductSource.SelectedIndexChanged -= cbxProductSource_SelectedIndexChanged;
        cbxProductSource.DisplayMember = "Item2";
        cbxProductSource.ValueMember = "Item1";

        cmbSelectedBarCode.SelectedIndexChanged -= cmbSelectedBarCode_SelectedIndexChanged;
        cmbSelectedBarCode.DisplayMember = "Item2";
        cmbSelectedBarCode.ValueMember = "Item1";

        cmbCompAlign.SelectedIndexChanged -= cmbCompAlign_SelectedIndexChanged;
        cmbCompAlign.SelectedItem = _companyAlign;
        cmbProductAlign.SelectedIndexChanged -= cmbProductAlign_SelectedIndexChanged;
        cmbProductAlign.SelectedItem = _productNameAlign;
        cmbRateAlign.SelectedIndexChanged -= cmbRateAlign_SelectedIndexChanged;
        cmbRateAlign.SelectedItem = _rateAlign;
        cmbBarcodeAlign.SelectedIndexChanged -= cmbBarcodeAlign_SelectedIndexChanged;
        cmbBarcodeAlign.SelectedItem = _barcodeAlign;

        cmbRateOptions.SelectedIndexChanged -= cmbRateOptions_SelectedIndexChanged;
        cmbRateOptions.SelectedItem = _rateOption;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

    private async void FrmBarCode_Load(object sender, EventArgs e)
    {
        try
        {

            cbxPaperKind.SelectedIndexChanged += (_, _) =>
            {
                nudPaperHeight.Visible = nudPaperWidth.Visible = cbxPaperKind.SelectedItem is PaperKind.Custom;
            };
            cbxPaperKind.DataSource = Enum.GetValues(typeof(PaperKind));
            cbxSymbology.DataSource = Enum.GetValues(typeof(BarCodeSymbology));
            foreach (var printer in PrinterSettings.InstalledPrinters) cbxPrinters.Items.Add(printer);
            colTextAlignment.DataSource = Enum.GetValues(typeof(TextAlignment));
            colTextType.DataSource = Enum.GetValues(typeof(PrintTextType));
            cbxSymbology.SelectedItem = BarCodeSymbology.Code128;
            cbxPaperKind.SelectedItem = PaperKind.A4;
            cbxPrinters.SelectedIndex = 0;

            var companyResponse = await QueryUtils.GetFirstOrDefaultAsync<string>("SELECT PrintDesc FROM AMS.CompanyInfo ");
            if (companyResponse.Model == null)
            {
                companyResponse.ShowErrorDialog();
                Close();
                return;
            }
            _companyName = companyResponse.Model;
            _fixedCompanyName = companyResponse.Model;
            await LoadConfigurationAsync();

            var sources = Enum.GetValues(typeof(BarcodeSource));
            foreach (BarcodeSource source in sources)
            {
                cbxProductSource.Items.Add(new ValueModel<BarcodeSource, string>(source, source.GetDescription()));
                cmbSelectedBarCode.Items.Add(new ValueModel<BarcodeSource, string>(source, source.GetDescription()));
            }

            cbxProductSource.SelectedIndexChanged += cbxProductSource_SelectedIndexChanged;
            cbxProductSource.SelectedIndex = 0;

            cmbSelectedBarCode.SelectedIndexChanged += cmbSelectedBarCode_SelectedIndexChanged;
            cmbSelectedBarCode.SelectedIndex = 0;
        }
        catch (Exception exception)
        {
            var msg = exception.Message;
        }
    }

    private void BtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnPreview_Click(object sender, EventArgs e)
    {
        if (bsBarcodes.Count == 0)
        {
            this.NotifyError(@"No product exported to print");
            return;
        }

        var listItems = bsBarcodes.List.OfType<ProductBarCodePrintModel>().ToList();
        var invalidItems = listItems.Where(x => x.PrintCount <= 0).ToList();
        if (invalidItems.Any())
        {
            var builder = new StringBuilder();
            foreach (var i in invalidItems)
            {
                builder.AppendLine($"{i.ProductName} - {i.ProductBarCode}");
            }
            MessageBox.Show(@"Invalid print count for following items: " + Environment.NewLine + builder);
            return;
        }

        // build the list as per print count
        var finalList = new List<ProductBarCodePrintModel>();
        listItems.ForEach(model =>
        {
            for (var i = 0; i < model.PrintCount; i++) finalList.Add(model);
        });

        if (string.IsNullOrWhiteSpace(cbxPrinters.Text))
        {
            this.NotifyError(@"No printer selected");
            return;
        }

        var matchingElement = new List<ProductBarCodeInfoModel>().FirstOrDefault(element => false/* your condition here */);

        if (matchingElement != null)
        {
            // Code to handle the presence of a matching element
        }
        else
        {
            // Code to handle the absence of a matching element
        }

        var paramModel = new BarCodeParamModel
        {
            BarModule = nudBarModule.Value,
            AutoModule = chkAutoBarModule.Checked,
            ColumnCount = (int)nudColumnCount.Value,
            ColumnSpacing = (int)nudColumnSpacing.Value,
            ColumnWidth = (int)nudColumnWidth.Value,
            RowHeight = (int)nudRowHeight.Value,
            Symbology = (BarCodeSymbology)cbxSymbology.SelectedItem,
            DrawBorder = chkDrawBorder.Checked,
            AutoFitPageWidth = chkAutoFitPageWidth.Checked ? (int?)nudAutoFitPageWidth.Value : null,
            CompanyProperty = new BarCodeLabelProperty(),
            ProductNameProperty = new BarCodeLabelProperty(),
            RateProperty = new BarCodeLabelProperty(),
            FillBarSpace = chkFillBarSpace.Checked,
            CompanyTextAlignment = _companyAlign,
            ProductTextAlignment = _productNameAlign,
            RateTextAlignment = _rateAlign,
            BarcodeTextAlignment = _barcodeAlign
        };
        if (llCompanyFont.Tag is Font cFont)
        {
            paramModel.CompanyProperty.FontName = cFont.Name;
            paramModel.CompanyProperty.FontSize = cFont.Size;
            paramModel.CompanyProperty.FontStyle = cFont.Style;
        }

        if (llProductNameFont.Tag is Font pFont)
        {
            paramModel.ProductNameProperty.FontName = pFont.Name;
            paramModel.ProductNameProperty.FontSize = pFont.Size;
            paramModel.ProductNameProperty.FontStyle = pFont.Style;
        }

        if (llSalesRate.Tag is Font rFont)
        {
            paramModel.RateProperty.FontName = rFont.Name;
            paramModel.RateProperty.FontSize = rFont.Size;
            paramModel.RateProperty.FontStyle = rFont.Style;
        }

        var report = new BarCodesPrintTemplate(paramModel)
        {
            DataSource = finalList,
            PrinterName = cbxPrinters.Text.Trim(),
            PaperKind = (DevExpress.Drawing.Printing.DXPaperKind)(PaperKind)cbxPaperKind.SelectedItem,
            Margins = new Margins((int)nudMarginLeft.Value, (int)nudMarginRight.Value, (int)nudMarginTop.Value, (int)nudMarginBottom.Value)
        };

        if (cbxPaperKind.SelectedItem is PaperKind.Custom)
        {
            report.PageSize = new Size((int)nudPaperWidth.Value * 10, (int)nudPaperHeight.Value * 10);
            report.PageWidth = (int)nudPaperWidth.Value * 10;
            report.PageHeight = (int)nudPaperHeight.Value * 10;
        }

        if (paramModel.AutoFitPageWidth.HasValue)
            report.PrintingSystem.Document.AutoFitToPagesWidth = 1;

        if (!paramModel.AutoModule)
        {
            var barCodeError = report.xrBarCode.Validate();
            if (barCodeError != DevExpress.XtraPrinting.BarCode.BarCodeError.None)
            {
                this.NotifyError("Bar spacing error: " + barCodeError.GetDescription());
                return;
            }
        }

        //ReportPrintTool printTool = new(report);
        //printTool.PreviewForm.PrintingSystem.PageSettings.PaperKind = (PaperKind)cbxPaperKind.SelectedItem;
        //printTool.PreviewForm.PrintingSystem.PageSettings.PaperName;

        report.ShowPreviewDialog();
    }

    private void BtnSelectAll_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in gridProducts.Rows)
            row.Cells[colCheckBox.Index].Value = true;
    }

    private void BtnSelectNone_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow row in gridProducts.Rows)
            row.Cells[colCheckBox.Index].Value = false;
    }

    private void GridProducts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex != -1 && e.ColumnIndex == colAdd.Index) e.Value = "Add";
    }

    private async void BtnAddSelected_Click(object sender, EventArgs e)
    {
        var existingItems = bsBarcodes.Count == 0 ? [] : bsBarcodes.List.OfType<ProductBarCodePrintModel>().ToList();

        var selectedItems = new List<ProductBarCodeInfoModel>();
        foreach (DataGridViewRow row in gridProducts.Rows)
            if (row.Cells[colCheckBox.Index].Value is true || row.Cells[colCheckBox.Index].Value?.ToString() == "true")
                selectedItems.Add((ProductBarCodeInfoModel)row.DataBoundItem);

        if (selectedItems.Count == 0)
        {
            this.NotifyError(@"No product selected");
            return;
        }
        var s = (ValueModel<BarcodeSource, string>)cmbSelectedBarCode.SelectedItem;
        foreach (var item in selectedItems)
        {
            if (existingItems.FirstOrDefault(x => x.ProductBarCode == item.ProductBarCode) == null)
            {
                var printedBarCode = await GetPrintedBarcode(s.Item1, item.ProductId);
                bsBarcodes.Add(new ProductBarCodePrintModel
                {
                    ProductName = item.ProductName,
                    ProductBarCode = item.ProductBarCode,
                    PrintedBarCode = printedBarCode == null ? "" : printedBarCode.PrintedBarCode,
                    PrintCount = 1,
                    ProductId = item.ProductId,
                    ProductCategory = item.ProductCategory,
                    CompanyName = _companyName,
                    SalesRate = SetSalesRate(printedBarCode, item.SalesRate),
                    SalesRateDecimal = item.SalesRate,
                    PrintText = chkBoxPrintBarcode.Checked,
                    PrintCompanyName = chkBoxPrintCompany.Checked,
                    PrintProductName = chkBoxPrintProductName.Checked,
                    PrintSalesRate = chkBoxPrintSalesRate.Checked
                });
            }
        }
    }

    private async void ChangePrintOptions()
    {
        var s = (ValueModel<BarcodeSource, string>)cmbSelectedBarCode.SelectedItem;
        foreach (ProductBarCodePrintModel data in bsBarcodes)
        {
            var printedBarcode = await GetPrintedBarcode(s.Item1, data.ProductId);
            data.PrintText = _isPrintBarcode;
            data.PrintedBarCode = printedBarcode == null ? "" : printedBarcode.PrintedBarCode;
            data.PrintCompanyName = _isPrintCompany;
            data.PrintProductName = _isPrintProductName;
            data.PrintSalesRate = _isPrintSalesRate;
            data.SalesRate = SetSalesRate(printedBarcode, data.SalesRateDecimal);
            data.CompanyName = _companyName;
        }
        gridBarcodes.Refresh();
    }

    private string SetSalesRate(ProductBarCodeInfoModel model, decimal salesRate)
    {
        var salesRateOutput = "0";
        if (_rateOption == "RateWithCode")
        {
            var rate = model != null ? Math.Truncate(model.BuyRate).ToString().ReverseString() : "000";
            if (rate.Length > 3)
            {
                rate = rate.Substring(rate.Length - 3, 3);
            }

            salesRateOutput = rate + "#" + Math.Truncate(salesRate).ToString();
        }
        else
        {
            salesRateOutput = "Price:" + Math.Truncate(salesRate).ToString();
        }
        return salesRateOutput;
    }

    private void GridBarcode_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex != -1 && e.ColumnIndex == colDelete.Index)
        {
            e.Value = "Delete";
        }
    }

    private async void GridProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex == -1 || e.ColumnIndex != colAdd.Index) return;
        var model = (ProductBarCodeInfoModel)gridProducts.Rows[e.RowIndex].DataBoundItem;
        var existingItems = bsBarcodes.Count == 0 ? [] : bsBarcodes.List.OfType<ProductBarCodePrintModel>().ToList();
        if (existingItems.FirstOrDefault(x => x.ProductBarCode == model.ProductBarCode) != null)
        {
            this.NotifyError(@"Already added");
            return;
        }

        //var selectedItem = cmbSelectedBarCode.SelectedItem as ValueModel<BarcodeSource, string>;

        //if (selectedItem != null)
        //{
        //    //var printedBarCode = await GetPrintedBarcode(s.Item1, model.ProductId);

        //}
        if (cmbSelectedBarCode.SelectedItem == null)
        {
            return;
        }
        var s = (ValueModel<BarcodeSource, string>)cmbSelectedBarCode.SelectedItem;
        var printedBarCode = await GetPrintedBarcode(s.Item1, model.ProductId);
        bsBarcodes.Add(new ProductBarCodePrintModel
        {
            ProductName = model.ProductName,
            ProductBarCode = model.ProductBarCode,
            PrintedBarCode = printedBarCode == null ? "" : printedBarCode.PrintedBarCode,
            PrintCount = 1,
            ProductId = model.ProductId,
            ProductCategory = model.ProductCategory,
            CompanyName = _companyName,
            SalesRate = SetSalesRate(printedBarCode, model.SalesRate),
            SalesRateDecimal = model.SalesRate,
            PrintText = chkBoxPrintBarcode.Checked,
            PrintCompanyName = chkBoxPrintCompany.Checked,
            PrintProductName = chkBoxPrintProductName.Checked,
            PrintSalesRate = chkBoxPrintSalesRate.Checked
        });
    }

    private void GridBarcode_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex != -1 && e.ColumnIndex == colDelete.Index)
            gridBarcodes.Rows.Remove(gridBarcodes.Rows[e.RowIndex]);
    }

    private void BtnClearList_Click(object sender, EventArgs e)
    {
        gridBarcodes.Rows.Clear();
    }

    private void gridBarcodes_SizeChanged(object sender, EventArgs e)
    {
        //chkAllPrintCode.Location = colPrintCode.HeaderCell.GetContentBounds(-1).Location;
        //chkAllPrintName.Location = colPrintProductName.HeaderCell.GetContentBounds(-1).Location;
        //chkAllPrintCompany.Location = colPrintCompany.HeaderCell.GetContentBounds(-1).Location;
        //chkAllPrintRate.Location = colPrintSalesRate.HeaderCell.GetContentBounds(-1).Location;
    }

    private async void btnSaveConfig_Click(object sender, EventArgs e)
    {
        var config = new BarCodePrintConfigModel
        {
            PaperKind = (PaperKind)cbxPaperKind.SelectedValue,
            Margins = new Margins((int)nudMarginLeft.Value, (int)nudMarginLeft.Value, (int)nudMarginTop.Value, (int)nudMarginBottom.Value),
            PrinterName = cbxPrinters.Text.Trim(),
            PaperSize = new SizeF((float)nudPaperWidth.Value, (float)nudPaperHeight.Value),
            Symbology = (BarCodeSymbology)cbxSymbology.SelectedValue,
            ColumnCount = (int)nudColumnCount.Value,
            RowHeight = nudRowHeight.Value,
            ColumnWidth = (uint)nudColumnWidth.Value,
            ColumnSpacing = nudColumnSpacing.Value,
            BarModule = nudBarModule.Value,
            AutoAdjustBarModule = chkAutoBarModule.Checked,
            FillBarSpace = chkFillBarSpace.Checked,
            DrawBorder = chkDrawBorder.Checked,
            AutoFitPagesWidth = chkAutoFitPageWidth.Checked ? (int?)nudAutoFitPageWidth.Value : null,
            IsPrintBarcode = chkBoxPrintBarcode.Checked,
            PrintedBarCode = ((ValueModel<BarcodeSource, string>)cmbSelectedBarCode.SelectedItem).Item2,
            ProductBarCode = ((ValueModel<BarcodeSource, string>)cbxProductSource.SelectedItem).Item2,
            CompanyName = txtBoxCompany.Text,
            IsPrintCompany = chkBoxPrintCompany.Checked,
            IsPrintProductName = chkBoxPrintProductName.Checked,
            IsPrintSalesRate = chkBoxPrintSalesRate.Checked,
            LblCompanyProperty = new BarCodeLabelProperty(),
            LblProductNameProperty = new BarCodeLabelProperty(),
            LblRateProperty = new BarCodeLabelProperty(),
            CompanyAlignment = _companyAlign,
            ProductAlignment = _productNameAlign,
            RateAlignment = _rateAlign,
            BarcodeAlignment = _barcodeAlign,
            RateOption = _rateOption
        };

        if (llProductNameFont.Tag is Font pFont)
        {
            config.LblProductNameProperty.FontName = pFont.Name;
            config.LblProductNameProperty.FontStyle = pFont.Style;
            config.LblProductNameProperty.FontSize = pFont.Size;
        }
        if (llCompanyFont.Tag is Font cFont)
        {
            config.LblCompanyProperty.FontName = cFont.Name;
            config.LblCompanyProperty.FontStyle = cFont.Style;
            config.LblCompanyProperty.FontSize = cFont.Size;
        }
        if (llSalesRate.Tag is Font rFont)
        {
            config.LblRateProperty.FontName = rFont.Name;
            config.LblRateProperty.FontStyle = rFont.Style;
            config.LblRateProperty.FontSize = rFont.Size;
        }

        var configXml = XmlUtils.SerializeToXml(config);

        var response = await QueryUtils.ExecNonQueryAsync("UPDATE ams.InventorySetting SET barcode_print_config = @prConfig ", new { prConfig = configXml });
        if (!response.Value)
        {
            response.ShowErrorDialog();
            return;
        }

        this.NotifySuccess("Configuration Saved Successfully");
    }

    private async Task LoadConfigurationAsync()
    {
        var response = await QueryUtils.GetFirstOrDefaultAsync<string>(
            @"SELECT barcode_print_config FROM ams.InventorySetting");
        if (!response.Success || string.IsNullOrWhiteSpace(response.Model)) return;

        var model = XmlUtils.XmlDeserialize<BarCodePrintConfigModel>(response.Model);
        if (model == null)
        {
            this.NotifyWarning("Unable to load barcode print configuration.");
            return;
        }

        cbxPaperKind.SelectedItem = model.PaperKind;
        if (model.PaperKind == PaperKind.Custom)
        {
            nudPaperHeight.Value = (decimal)model.PaperSize.Height;
            nudPaperWidth.Value = (decimal)model.PaperSize.Width;
        }

        cbxPrinters.Text = model.PrinterName;
        nudMarginLeft.Value = model.Margins.Left;
        nudMarginRight.Value = model.Margins.Right;
        nudMarginTop.Value = model.Margins.Top;
        nudMarginBottom.Value = model.Margins.Bottom;

        cbxSymbology.SelectedItem = model.Symbology;
        nudColumnCount.Value = model.ColumnCount;
        nudRowHeight.Value = model.RowHeight;
        nudColumnSpacing.Value = model.ColumnSpacing;
        nudColumnWidth.Value = model.ColumnWidth;
        nudBarModule.Value = model.BarModule;

        var companyNameFont = new Font(new FontFamily(model.LblCompanyProperty.FontName), model.LblCompanyProperty.FontSize, model.LblCompanyProperty.FontStyle);
        llCompanyFont.Tag = lblCompanyFont.Font = companyNameFont;

        var productNameFont = new Font(new FontFamily(model.LblProductNameProperty.FontName), model.LblProductNameProperty.FontSize, model.LblProductNameProperty.FontStyle);
        llProductNameFont.Tag = lblProductFont.Font = productNameFont;

        var rateFont = new Font(new FontFamily(model.LblRateProperty.FontName), model.LblRateProperty.FontSize, model.LblRateProperty.FontStyle);
        llSalesRate.Tag = lblSalesRateFont.Font = rateFont;

        lblCompanyFont.Text = $@"{model.LblCompanyProperty.FontName}, {model.LblCompanyProperty.FontStyle}, {model.LblCompanyProperty.FontSize}";
        lblProductFont.Text = $@"{model.LblProductNameProperty.FontName}, {model.LblProductNameProperty.FontStyle}, {model.LblProductNameProperty.FontSize}";
        lblSalesRateFont.Text = $@"{model.LblRateProperty.FontName}, {model.LblRateProperty.FontStyle}, {model.LblRateProperty.FontSize}";

        chkAutoBarModule.Checked = model.AutoAdjustBarModule;
        chkDrawBorder.Checked = model.DrawBorder;
        chkFillBarSpace.Checked = model.FillBarSpace;
        chkBoxPrintBarcode.Checked = _isPrintBarcode = model.IsPrintBarcode;
        txtBoxCompany.Text = string.IsNullOrEmpty(model.CompanyName) ? "" : model.CompanyName;
        chkBoxPrintCompany.Checked = _isPrintCompany = model.IsPrintCompany;
        _printedBarCode = string.IsNullOrEmpty(model.PrintedBarCode) ? BarcodeSource.BarCode.ToString() : model.PrintedBarCode;
        _productBarCode = string.IsNullOrEmpty(model.ProductBarCode) ? BarcodeSource.BarCode3.ToString() : model.ProductBarCode;
        chkBoxPrintProductName.Checked = _isPrintProductName = model.IsPrintProductName;
        chkBoxPrintSalesRate.Checked = _isPrintSalesRate = model.IsPrintSalesRate;
        if (model.AutoFitPagesWidth.HasValue)
        {
            chkAutoFitPageWidth.Checked = true;
            nudAutoFitPageWidth.Value = model.AutoFitPagesWidth.Value;
        }
        else
        {
            chkAutoFitPageWidth.Checked = false;
            nudAutoFitPageWidth.Value = 0;
        }

        _companyAlign = string.IsNullOrEmpty(model.CompanyAlignment) ? _companyAlign : model.CompanyAlignment;
        cmbCompAlign.Text = _companyAlign;
        cmbCompAlign.SelectedIndexChanged += cmbCompAlign_SelectedIndexChanged;
        _productNameAlign = string.IsNullOrEmpty(model.ProductAlignment) ? _productNameAlign : model.ProductAlignment;
        cmbProductAlign.Text = _productNameAlign;
        cmbProductAlign.SelectedIndexChanged += cmbProductAlign_SelectedIndexChanged;
        _rateAlign = string.IsNullOrEmpty(model.RateAlignment) ? _rateAlign : model.RateAlignment;
        cmbRateAlign.Text = _rateAlign;
        cmbRateAlign.SelectedIndexChanged += cmbRateAlign_SelectedIndexChanged;
        _barcodeAlign = string.IsNullOrEmpty(model.BarcodeAlignment) ? _barcodeAlign : model.BarcodeAlignment;
        cmbBarcodeAlign.Text = _barcodeAlign;
        cmbBarcodeAlign.SelectedIndexChanged += cmbBarcodeAlign_SelectedIndexChanged;

        _rateOption = string.IsNullOrEmpty(model.RateOption) ? _rateOption : model.RateOption;
        cmbRateOptions.Text = _rateOption;
        cmbRateOptions.SelectedIndexChanged += cmbRateOptions_SelectedIndexChanged;
    }

    private void btnPageDialog_Click(object sender, EventArgs e)
    {
        var dialog = new PageSetupDialog
        {
            PageSettings = new PageSettings(new PrinterSettings
            {
                PrinterName = cbxPrinters.Text.Trim()
            })
        };
        if (dialog.ShowDialog() != DialogResult.OK) return;
    }

    private void llProductNameFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var fd = new FontDialog();
        if (fd.ShowDialog() != DialogResult.OK) return;

        llProductNameFont.Tag = fd.Font;
        lblProductFont.Text = $@"{fd.Font.Name}, {fd.Font.Style}, {fd.Font.Size}";
        lblProductFont.Font = fd.Font;
    }

    private void llCompanyFont_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var fd = new FontDialog();
        if (fd.ShowDialog() != DialogResult.OK) return;

        llCompanyFont.Tag = fd.Font;
        lblCompanyFont.Text = $@"{fd.Font.Name}, {fd.Font.Style}, {fd.Font.Size}";
        lblCompanyFont.Font = fd.Font;
    }

    private void llSalesRate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        using var fd = new FontDialog();
        if (fd.ShowDialog() != DialogResult.OK) return;

        llSalesRate.Tag = fd.Font;
        lblSalesRateFont.Text = $@"{fd.Font.Name}, {fd.Font.Style}, {fd.Font.Size}";
        lblSalesRateFont.Font = fd.Font;
    }

    private void txtSearchProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Enter) return;
        var searchQuery = txtSearchProduct.Text.Trim();

        e.Handled = true;
        bsProducts.DataSource = string.IsNullOrWhiteSpace(searchQuery) ? _allProducts :
            _allProducts.Where(x => x.ProductName.IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1 ||
                                    (x.PrintedBarCode ?? "").IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1 ||
                                    (x.ProductBarCode ?? "").IndexOf(searchQuery, StringComparison.OrdinalIgnoreCase) != -1).ToList();
    }

    private void cmbCompAlign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _companyAlign = cmbCompAlign.SelectedItem.ToString();
        }
        catch
        {
            _companyAlign = "Left";
        }
    }

    private void cmbProductAlign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _productNameAlign = cmbProductAlign.SelectedItem.ToString();
        }
        catch
        {
            _productNameAlign = "Left";
        }
    }

    private void cmbRateAlign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _rateAlign = cmbRateAlign.SelectedItem.ToString();
        }
        catch
        {
            _rateAlign = "Left";
        }
    }

    private void cmbRateOptions_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _rateOption = cmbRateOptions.SelectedItem.ToString();
            ChangePrintOptions();
        }
        catch
        {
            _rateOption = "Rate";
        }
    }

    private void cbxSymbology_SelectedIndexChanged(object sender, EventArgs e)
    {
        _barcodeSymbology = (BarCodeSymbology)cbxSymbology.SelectedItem;
        ChangeBarcode3();
    }

    private void cmbBarcodeAlign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _barcodeAlign = cmbBarcodeAlign.SelectedItem.ToString();
        }
        catch
        {
            _barcodeAlign = "Center";
        }
    }

    private async void cbxProductSource_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cbxProductSource.SelectedIndex == -1 || cbxProductSource.SelectedItem is not ValueModel<BarcodeSource, string> source) return;
        await LoadProductsAsync(source.Item1);
    }

    private void chkBoxPrintCompany_CheckedChanged(object sender, EventArgs e)
    {
        _isPrintCompany = chkBoxPrintCompany.Checked;
        ChangePrintOptions();
    }

    private void chkBoxPrintProductName_CheckedChanged(object sender, EventArgs e)
    {
        _isPrintProductName = chkBoxPrintProductName.Checked;
        ChangePrintOptions();
    }

    private void txtBoxCompany_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtBoxCompany.Text))
        {
            _companyName = _fixedCompanyName;
        }
        else
        {
            _companyName = txtBoxCompany.Text;
        }
        ChangePrintOptions();
    }

    private void cmbSelectedBarCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangePrintOptions();
    }

    private void chkBoxPrintBarcode_CheckedChanged(object sender, EventArgs e)
    {
        _isPrintBarcode = chkBoxPrintBarcode.Checked;
        ChangePrintOptions();
    }

    private void chkBoxPrintSalesRate_CheckedChanged(object sender, EventArgs e)
    {
        _isPrintSalesRate = chkBoxPrintSalesRate.Checked;
        ChangePrintOptions();
    }

    private async Task LoadProductsAsync(BarcodeSource source)
    {
        _allProducts?.Clear();
        bsProducts.Clear();

        var sql = source switch
        {
            BarcodeSource.PShortName => @"
                    SELECT p.PID  AS ProductId, p.PName AS ProductName, pg.GrpName AS ProductCategory, p.PShortName AS ProductBarCode, p.Barcode AS PrintedBarCode, p.PSalesRate AS SalesRate
                    FROM AMS.Product AS p
                    LEFT JOIN AMS.ProductGroup AS pg ON pg.PGrpId = p.PGrpId",
            BarcodeSource.BarCode => @"
                    SELECT p.PID  AS ProductId, p.PName AS ProductName, pg.GrpName AS ProductCategory, p.Barcode AS ProductBarCode, p.Barcode AS PrintedBarCode, p.PSalesRate AS SalesRate
                    FROM AMS.Product AS p
                    LEFT JOIN AMS.ProductGroup AS pg ON pg.PGrpId = p.PGrpId
                    WHERE p.Barcode IS NOT NULL ",
            BarcodeSource.BarCode1 => @"
                    SELECT p.PID  AS ProductId, p.PName AS ProductName, pg.GrpName AS ProductCategory, p.Barcode1 AS ProductBarCode, p.Barcode AS PrintedBarCode, p.PSalesRate AS SalesRate
                    FROM AMS.Product AS p
                    LEFT JOIN AMS.ProductGroup AS pg ON pg.PGrpId = p.PGrpId
                    WHERE p.Barcode1 IS NOT NULL ",
            BarcodeSource.BarCodeList => @"
                    SELECT bc.ProductId, p.PName AS ProductName, pg.GrpName AS ProductCategory, bc.Barcode AS ProductBarCode, p.Barcode AS PrintedBarCode, p.PSalesRate AS SalesRate
                    FROM AMS.BarcodeList AS bc
                    JOIN AMS.Product AS p ON p.PID = bc.ProductId
                    LEFT JOIN AMS.ProductGroup AS pg ON pg.PGrpId = p.PGrpId
                    WHERE bc.Barcode IS NOT NULL ",
            BarcodeSource.BarCode2 => @"
                    SELECT p.PID  AS ProductId, p.PName AS ProductName, pg.GrpName AS ProductCategory, p.Barcode2 AS ProductBarCode, p.Barcode AS PrintedBarCode, p.PSalesRate AS SalesRate
                    FROM AMS.Product AS p
                    LEFT JOIN AMS.ProductGroup AS pg ON pg.PGrpId = p.PGrpId
                    WHERE p.Barcode2 IS NOT NULL ",
            BarcodeSource.BarCode3 => @"
                    SELECT p.PID  AS ProductId, p.PName AS ProductName, pg.GrpName AS ProductCategory, p.Barcode3 AS ProductBarCode, p.Barcode AS PrintedBarCode, p.PSalesRate AS SalesRate
                    FROM AMS.Product AS p
                    LEFT JOIN AMS.ProductGroup AS pg ON pg.PGrpId = p.PGrpId
                    WHERE p.Barcode3 IS NOT NULL ",
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };

        var queryResponse = await QueryUtils.GetListAsync<ProductBarCodeInfoModel>(sql);

        if (!queryResponse.Success)
        {
            queryResponse.ShowErrorDialog("Unable to fetch products list.");
            return;
        }

        bsProducts.DataSource = _allProducts = queryResponse.List.ToList();
        ChangeBarcode3();
    }

    private async void ChangeBarcode3()
    {
        if (cbxProductSource.SelectedIndex == -1 || cbxProductSource.SelectedItem is not ValueModel<BarcodeSource, string> source) return;
        if (source.Item1 == BarcodeSource.BarCode3)
        {
            ClsMasterSetup masterSetup = new ClsMasterSetup();
            foreach (ProductBarCodeInfoModel data in bsProducts)
            {
                var barcode3 = masterSetup.GenerateBarcode3(data.ProductId, _barcodeSymbology);
                if (barcode3 == data.ProductBarCode)
                    break;
                else
                {
                    var query = $@"UPDATE AMS.Product SET Barcode3='{barcode3}' WHERE PID={data.ProductId}";
                    await QueryUtils.ExecNonQueryAsync(query);
                    data.ProductBarCode = barcode3;
                }
            }
            gridProducts.Refresh();
        }
    }

    private async Task<ProductBarCodeInfoModel> GetPrintedBarcode(BarcodeSource source, long productId)
    {
        var sql = source switch
        {
            BarcodeSource.PShortName => $@"
                SELECT p.PShortName AS PrintedBarCode, p.PBuyRate AS BuyRate
                FROM AMS.Product AS p where p.PID={productId}",
            BarcodeSource.BarCode => $@"
                SELECT p.Barcode AS PrintedBarCode,  p.PBuyRate AS BuyRate
                FROM AMS.Product AS p where p.PID={productId}",
            BarcodeSource.BarCode1 => $@"
                SELECT p.Barcode1 AS PrintedBarCode,  p.PBuyRate AS BuyRate
                FROM AMS.Product AS p where p.PID={productId}",
            BarcodeSource.BarCodeList => $@"
                SELECT bc.Barcode AS PrintedBarCode, p.PBuyRate AS BuyRate
                FROM AMS.BarcodeList AS bc
                JOIN AMS.Product AS p ON p.PID = bc.ProductId
                where p.PID={productId}",
            BarcodeSource.BarCode2 => $@"SELECT
	                    p.Barcode2 AS PrintedBarCode,  p.PBuyRate AS BuyRate
                    FROM AMS.Product AS p where p.PID={productId}",
            BarcodeSource.BarCode3 => $@"SELECT
	                   p.Barcode3 AS PrintedBarCode, p.PBuyRate AS BuyRate
                    FROM AMS.Product AS p where p.PID={productId}",
            _ => throw new ArgumentOutOfRangeException(nameof(source), source, null)
        };

        var queryResponse = await QueryUtils.GetFirstOrDefaultAsync<ProductBarCodeInfoModel>(sql);
        if (!queryResponse.Success)
        {
            queryResponse.ShowErrorDialog("Unable to fetch products list.");
            return null;
        }

        return queryResponse.Model;
    }

    private enum BarcodeSource
    {
        [Description("Short Name")]
        PShortName,

        [Description("BarCode")]
        BarCode,

        [Description("BarCode1")]
        BarCode1,

        [Description("BarCode2")]
        BarCode2,

        [Description("BarCode3")]
        BarCode3,

        [Description("BarCode List")]
        BarCodeList
    }
}