using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Domains.DynamicReport.UserControl;
using MrBLL.Utility.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.DynamicReport;

public partial class FrmDynamicInventoryReports : RibbonForm
{
    private XtraUserControl _createControl;
    private uInventoryReports _uReports;
    private string _reportDesc;

    [Obsolete]
    public FrmDynamicInventoryReports()
    {
        InitializeComponent();
        _createControl = CreateUserControl("USER CONTROL : FINANCE REPORTS");
        accordionControl.SelectedElement = uMnuStockLedger;
    }

    [Obsolete]
    private XtraUserControl CreateUserControl(string text)
    {
        _uReports = new uInventoryReports(text, text.GetUpper(), false)
        {
            Name = $"{text.GetUpper()} USER CONTROL",
            Text = text,
            Dock = DockStyle.Fill
        };
        var label = new LabelControl
        {
            Parent = _uReports
        };
        label.Appearance.Font = new Font("Bookman Old Style", 12F);
        label.Appearance.ForeColor = Color.Gray;
        label.Dock = DockStyle.Fill;
        label.AutoSizeMode = LabelAutoSizeMode.None;
        label.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        label.Appearance.TextOptions.VAlignment = VertAlignment.Center;
        label.Text = text;
        return _uReports;
    }

    [Obsolete]
    private void AccordionControl_SelectedElementChanged(object sender, AccordionControlElementEventArgs e)
    {
        try
        {
            if (e.Element == null)
            {
                return;
            }
            _reportDesc = e.Element.Text;
            if (_reportDesc.Equals("Stock Ledger Reports"))
            {
                return;
            }
            for (var i = 0; i < tabbedView.Documents.Count; i++)
            {
                var result = tabbedView.AppearancePage.Header;
            }
            _createControl = CreateUserControl(e.Element.Text);
            tabbedView.AddDocument(_createControl);
            tabbedView.ActivateDocument(_createControl);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
        }
    }

    [Obsolete]
    private void AccordionControl_ElementClick(object sender, ElementClickEventArgs e)
    {
        AccordionControl_SelectedElementChanged(sender, new SelectedElementChangedEventArgs(e.Element));
    }

    [Obsolete]
    private void TabbedView_DocumentClosed(object sender, DocumentEventArgs e)
    {
        RecreateUserControls(e);
    }

    [Obsolete]
    private void RecreateUserControls(DocumentEventArgs e)
    {
        try
        {
            if (e.Document == null)
            {
                return;
            }
            _createControl = CreateUserControl(e.Document.Caption);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void BtnSaveTemplate_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_uReports.ReportTabControl.SelectedTabPageIndex.GetInt() > 0)
        {
            _uReports.PGrid.SaveLayoutToXml($"{_reportDesc.GetUpper()}.xml");
        }
        else
        {
            _uReports.SaveTemplate("UPDATE");
        }
    }

    private void BtnSaveAsTemplate_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_uReports.ReportTabControl.SelectedTabPage.GetInt() > 0)
        {
            _uReports.PGrid.SaveLayoutToXml($"{_reportDesc.GetUpper()}.xml");
        }
        else
        {
            _uReports.SaveAsTemplate();
        }
    }

    private void BtnRemoveTemplate_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_uReports.ReportTabControl.SelectedTabPage.GetInt() > 0)
        {
            _uReports.PGrid.SaveLayoutToXml($"{_reportDesc.GetUpper()}.xml");
        }
        else
        {
            _uReports.SaveTemplate("DELETE");
        }
    }

    [Obsolete]
    private void MnuCheckFooter_CheckedChanged(object sender, ItemClickEventArgs e)
    {
        _uReports.EnableFooter(MnuCheckFooter.Checked);
    }

    private void MnuExportToExcel_ItemClick(object sender, ItemClickEventArgs e)
    {
        _uReports.ExportToExcel();
    }

    private void BtnPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
    {
        _uReports.PrintPreview();
    }

    private void BtnFilterReports_ItemClick(object sender, ItemClickEventArgs e)
    {
        var result = new FrmFilter(_reportDesc, _uReports.ReportTabControl.SelectedTabPageIndex is 0 ? "R" : "P", _reportDesc);
        result.ShowDialog();
        if (result.DialogResult is not DialogResult.Yes)
        {
            return;
        }
        try
        {
            SplashScreenManager.ShowForm(typeof(FrmWait));
            _uReports.TemplateId = result.GetTemplateId;
            _uReports.TempDesc = result._templateName;
            _uReports.TxtGridReportTemplete.Text = _uReports.TempDesc;
            _uReports.FromDate = result.MskFrom.Text;
            _uReports.ToDate = result.MskToDate.Text;
            _uReports.BindSelectedReportAsync(_reportDesc.GetUpper(), result.MskFrom.Text, result.MskToDate.Text);
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
}