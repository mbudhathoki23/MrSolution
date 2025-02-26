using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Reports.Interface;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Domains.DynamicReport.UserControl;

public partial class uRegisterReports : XtraUserControl
{
    [Obsolete]
    public uRegisterReports(string reportText, string reportDesc, bool isSummary)
    {
        InitializeComponent();
        gridView1.OptionsView.ShowFooter = true;
        gridView1.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded;
        ReportText = reportText;
        ReportDesc = reportDesc;
        GrpGridReportTemplete.Visible = false;
        GrpPivotReportTemplete.Visible = false;
    }

    private void UReports_Load(object sender, EventArgs e)
    {
        Text = ReportText;
        var result = new FrmFilter(ReportDesc, TabGridReport.TabIndex is 0 ? "R" : "P", ReportDesc);
        result.ShowDialog();
        if (result.DialogResult is not DialogResult.Yes) return;
        try
        {
            SplashScreenManager.ShowForm(typeof(FrmWait));
            TemplateId = result.GetTemplateId;
            TempDesc = result._templateName;
            TxtGridReportTemplete.Text = TempDesc;
            FromDate = result.MskFrom.Text;
            ToDate = result.MskToDate.Text;
            BindSelectedReportAsync(ReportDesc, FromDate, ToDate);
            _gridControl = RGrid;
            _gridView = gridView1;
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    public void BindSelectedReportAsync(string reportDesc, string fromDate, string toDate)
    {
        if (string.IsNullOrEmpty(reportDesc))
        {
            CustomMessageBox.Warning($"{reportDesc.GetUpper()} REPORTS NOT EXITS..");
            return;
        }

        if (string.IsNullOrEmpty(reportDesc))
        {
            return;
        }

        fromDate = ObjGlobal.SysDateType is "M" ? fromDate.GetEnglishDate() : fromDate;
        toDate = ObjGlobal.SysDateType is "M" ? toDate.GetEnglishDate() : toDate;

        _dynamicReport.GetReports.FromAdDate = fromDate.GetSystemDate();
        _dynamicReport.GetReports.ToAdDate = toDate.GetSystemDate();

        _table = reportDesc switch
        {
            "SALES INVOICE SUMMARY" => _dynamicReport.GetSalesInvoiceRegisterSummaryReports(),
            "SALES INVOICE DETAILS" => _dynamicReport.GetSalesInvoiceRegisterDetailsReports(),
            "SALES INVOICE LEDGER WISE" => _dynamicReport.GetSalesInvoiceRegisterProductLedgerReports(),
            "SALES REGISTER TABLE WISE" => _dynamicReport.GetSalesInvoiceRegisterTableWiseReports(),
            "SALES VAT REGISTER" => _dynamicReport.GetSalesInvoiceVatRegisterReports(),
            "SALES VAT REGISTER INCLUDE RETURN" => _dynamicReport.GetSalesInvoiceVatRegisterIncludeReturnReports(),
            "SALES VAT REGISTER CUSTOMER WISE" => _dynamicReport.GetSalesInvoiceVatRegisterCustomerWise(),
            "SALES RETURN VAT REGISTER" => _dynamicReport.GetSalesReturnVatRegisterReports(),
            "SALES INVOICE PARTIAL PAYMENT" => _dynamicReport.GetSalesInvoiceRegisterPartialPaymentReport(),
            "SALES INVOICE YEAR WISE" => _dynamicReport.GetSalesInvoiceRegisterYearWiseReport(),

            "PURCHASE INVOICE SUMMARY" => _dynamicReport.GetPurchaseInvoiceRegisterSummaryReports(),
            "PURCHASE INVOICE DETAILS" => _dynamicReport.GetPurchaseInvoiceRegisterDetailsReports(),
            "PURCHASE VAT REGISTER" => _dynamicReport.GetPurchaseInvoiceVatRegisterReports(),
            "PURCHASE VAT REGISTER INCLUDE RETURN" => _dynamicReport.GetPurchaseInvoiceVatRegisterIncludeReturnReports(),
            "PURCHASE RETURN VAT REGISTER" => _dynamicReport.GetPurchaseReturnVatRegisterReports(),
            "VENDOR CURRENCY LEDGER" => _dynamicReport.GetPurchaseReturnVatRegisterReports(),

            _ => new DataTable()
        };
        var dsRegister = reportDesc switch
        {
            "SALES INVOICE MASTER DETAILS" => _dynamicReport.GetSalesInvoiceMasterDetailsReports(),
            "PURCHASE INVOICE MASTER DETAILS" => _dynamicReport.GetPurchaseInvoiceMasterDetailsReports(),
            _ => new DataSet()
        };
        if (_table.Rows.Count == 0)
        {
            _table = dsRegister.Tables[0];
        }
        if (_table.Rows.Count == 0)
        {
            CustomMessageBox.Warning($"{reportDesc.GetUpper()} REPORTS NOT EXITS..");
            return;
        }
        if (TemplateId > 0)
        {
            var dt = _dynamicReport.ListTemplateType(ReportDesc, TemplateId, ReportTabControl.SelectedTabPageIndex is 0 ? "R" : "P");
            if (dt.Rows.Count > 0)
            {
                var byteArray = dt.Rows[0]["FileName"] as byte[];
                if (ReportTabControl.SelectedTabPageIndex == 0)
                {
                    gridView1.Columns.Clear();
                    BindReports(reportDesc);
                    if (byteArray != null)
                    {
                        gridView1.RestoreLayoutFromStream(new MemoryStream(byteArray), OptionsLayoutBase.FullLayout);
                    }
                }
                else if (ReportTabControl.SelectedTabPageIndex == 1)
                {
                    PGrid.Fields.Clear();
                    BindReports(reportDesc);
                    if (byteArray != null)
                    {
                        PGrid.RestoreLayoutFromStream(new MemoryStream(byteArray));
                    }
                }
            }
            else
            {
                BindReports(reportDesc);
            }
        }
        else
        {
            BindReports(reportDesc);
        }
    }

    private void BindReports(string reportDesc)
    {
        var result = GetRegisterReports();
    }

    private void UReports_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Dispose();
            }
        }
    }

    private bool GetRegisterReports()
    {
        RGrid.DataSource = null;
        gridView1.Columns.Clear();
        RGrid.DataSource = _table;
        gridView1.BestFitColumns();
        if (gridView1.RowCount <= 0)
        {
            return gridView1.RowCount > 0;
        }
        PGrid.Fields.Clear();
        for (var i = 0; i < gridView1.Columns.Count; i++)
        {
            var field = new PivotGridField
            {
                FieldName = gridView1.Columns[i].FieldName,
                Caption = gridView1.Columns[i].Caption,
                ValueFormat = { FormatType = FormatType.Custom, FormatString = "N2" }
            };
            PGrid.Fields.Add(field);
        }
        PGrid.DataSource = gridView1.DataSource;
        return gridView1.RowCount > 0;
    }

    private byte[] GetReportXmlFormat()
    {
        var str = new MemoryStream();
        gridView1.SaveLayoutToStream(str, OptionsLayoutBase.FullLayout);
        str.Seek(0, SeekOrigin.Begin);
        return str.ToArray();
    }

    private byte[] GetPivotReportXmlFormat()
    {
        var str = new MemoryStream();
        PGrid.SaveLayoutToStream(str, OptionsLayoutBase.FullLayout);
        str.Seek(0, SeekOrigin.Begin);
        return str.ToArray();
    }

    public void SaveTemplate(string action)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(FrmWait));
            var reportType = ReportTabControl.SelectedTabPageIndex is 0 ? "R" : "P";
            var filename = GetReportXmlFormat();
            _dynamicReport.Model.ActionTag = action;
            _dynamicReport.Model.ID = TemplateId;
            _dynamicReport.Model.Report_Name = ReportTabControl.SelectedTabPageIndex is 0 ? TxtGridReportTemplete.Text : TxtPivotGridTemplete.Text;
            _dynamicReport.Model.Reports_Type = reportType;
            _dynamicReport.Model.FileName = filename;
            _dynamicReport.Model.FullPath = string.Empty;
            _dynamicReport.Model.FromDate = ObjGlobal.CfStartAdDate.GetDateTime();
            _dynamicReport.Model.ToDate = ObjGlobal.CfEndAdDate.GetDateTime();
            _dynamicReport.Model.ReportCategory = ReportDesc;
            _dynamicReport.Model.ReportSource = reportType;
            var result = _dynamicReport.SaveTemplate();
            if (result == 0)
            {
                CustomMessageBox.Warning($"{ReportDesc} REPORT TEMPLATE {action}");
                return;
            }
            CustomMessageBox.ActionSuccess("REPORT TEMPLATE", ReportDesc, action);
            GrpGridReportTemplete.Visible = false;
            GrpPivotReportTemplete.Visible = false;
            BindSelectedReportAsync(ReportDesc, FromDate, ToDate);
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception e)
        {
            SplashScreenManager.CloseForm(false);
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    public void SaveAsTemplate()
    {
        GrpGridReportTemplete.Visible = ReportTabControl.SelectedTabPageIndex is 0;
        GrpPivotReportTemplete.Visible = ReportTabControl.SelectedTabPageIndex is 1;
    }

    [Obsolete]
    public void EnableFooter(bool footer)
    {
        _gridView.OptionsView.GroupFooterShowMode = footer ? GroupFooterShowMode.VisibleAlways : GroupFooterShowMode.Hidden;
        if (footer)
        {
            _gridView.GroupSummary.Assign(_gridView.GroupSummary);
        }
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (TxtGridReportTemplete.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning($"{ReportDesc} Template is Blank..!!");
            TxtGridReportTemplete.Focus();
        }
        SaveTemplate("SAVE");
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        GrpGridReportTemplete.Visible = false;
        GrpPivotReportTemplete.Visible = false;
    }

    public void PrintPreview()
    {
        if (_gridView.RowCount > 0)
        {
            var printableComponentLink = new PrintableComponentLink(new PrintingSystem())
            {
                Component = _gridControl,
                PaperKind = (DevExpress.Drawing.Printing.DXPaperKind)PaperKind.A4
            };
            printableComponentLink.Margins.Left = 0;
            printableComponentLink.Margins.Right = 0;
            printableComponentLink.Margins.Top = 110;
            printableComponentLink.Margins.Bottom = 25;
            printableComponentLink.CreateMarginalHeaderArea += Link_CreateMarginalHeaderArea;
            printableComponentLink.CreateDocument();
            printableComponentLink.ShowPreview();
        }
    }

    private void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
    {
        var txt1 = e.Graph.DrawString(ObjGlobal.LogInCompany, Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50), BorderSide.None);
        txt1.Font = new Font("Arial", 10, FontStyle.Bold);
        txt1.StringFormat = new BrickStringFormat(StringAlignment.Center);

        var txt2 = e.Graph.DrawString(ObjGlobal.CompanyAddress, Color.Navy, new RectangleF(0, 15, e.Graph.ClientPageSize.Width, 50), BorderSide.None);
        txt2.Font = new Font("Arial", 8, FontStyle.Regular);
        txt2.StringFormat = new BrickStringFormat(StringAlignment.Center);

        var txt7 = e.Graph.DrawString(ObjGlobal.CompanyPhoneNo, Color.Navy, new RectangleF(0, 30, e.Graph.ClientPageSize.Width, 50), BorderSide.None);
        txt7.Font = new Font("Arial", 8, FontStyle.Regular);
        txt7.StringFormat = new BrickStringFormat(StringAlignment.Center);

        var txt6 = e.Graph.DrawString("PAN/VAT NO :" + ObjGlobal.CompanyPanNo, Color.Navy, new RectangleF(0, 45, e.Graph.ClientPageSize.Width, 50), BorderSide.None);
        txt6.Font = new Font("Arial", 8, FontStyle.Regular);
        txt6.StringFormat = new BrickStringFormat(StringAlignment.Center);

        var txt3 = e.Graph.DrawString(ReportDesc, Color.Navy, new RectangleF(0, 60, e.Graph.ClientPageSize.Width, 50), BorderSide.None);
        txt3.Font = new Font("Arial", 16, FontStyle.Bold);
        txt3.ForeColor = Color.Red;
        txt3.StringFormat = new BrickStringFormat(StringAlignment.Center);

        var txt4 = e.Graph.DrawString("Accounting Period  ", Color.Navy, new RectangleF(e.Graph.ClientPageSize.Width - 100, 0, 100, 18), BorderSide.None);
        txt4.Font = new Font("Arial", 8, FontStyle.Regular);
        txt4.StringFormat = new BrickStringFormat(StringAlignment.Far);

        var txt5 = e.Graph.DrawString($"{FromDate} TO {ToDate}", Color.Navy, new RectangleF(e.Graph.ClientPageSize.Width - 100, 0, 100, 18), BorderSide.None);
        txt4.Font = new Font("Arial", 8, FontStyle.Regular);
        txt4.StringFormat = new BrickStringFormat(StringAlignment.Far);

        var drawPageInfo = e.Graph.DrawPageInfo(PageInfo.DateTime, "DT/Time : {0:F}", Color.Navy, new RectangleF(0, 36, 172, 18), BorderSide.None);
        drawPageInfo.Font = new Font("Arial", 8, FontStyle.Regular);
        drawPageInfo.Alignment = BrickAlignment.Far;

        var pageInfo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", Color.Navy, new RectangleF(0, 54, 80, 18), BorderSide.None);
        pageInfo.Font = new Font("Arial", 8, FontStyle.Regular);
        pageInfo.Alignment = BrickAlignment.Far;
    }

    public void ExportToExcel()
    {
        if (gridView1.RowCount == 0)
        {
            MessageBox.Show(@"RECORD NOT FOUND.", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }
        saveFileDialog1.Title = @"Ok";
        saveFileDialog1.Filter = @"Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
        var showDialog = saveFileDialog1.ShowDialog();
        if (showDialog == DialogResult.Cancel) return;

        var fileName = saveFileDialog1.FileName;
        var fileExtenstion = new FileInfo(fileName).Extension;
        switch (fileExtenstion)
        {
            case ".xls":
                gridView1.ExportToXls(fileName);
                break;

            case ".pdf":
                gridView1.ExportToPdf(fileName);
                break;

            case ".xlsx":
                gridView1.ExportToXlsx(fileName);
                break;

            case ".rtf":
                gridView1.ExportToRtf(fileName);
                break;

            case ".html":
                gridView1.ExportToHtml(fileName);
                break;
        }
        var result2 = MessageBox.Show(@"DO YOU WANT TO OPEN FILE..??", ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result2 is DialogResult.Yes)
        {
            var name = fileName;
            Process.Start(name);
        }
    }

    // OBJECT FOR THIS FORM
    public string ReportDesc;

    public string TempDesc;
    public string ReportText;
    public string FromDate;
    public string ToDate;
    public int TemplateId;
    private IDynamicReport _dynamicReport = new MrDAL.Reports.DyanamicReport.DynamicReport();
    private DataTable _table = new("REPORT");
    private static GridView _gridView = new();
    private static GridControl _gridControl = new();
    public static string ActionTag { get; set; }
}