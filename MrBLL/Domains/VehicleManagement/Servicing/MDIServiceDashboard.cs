using DevExpress.Utils;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.VehicleManagement.Servicing;

public partial class MDIServiceDashboard : XtraForm
{
    private XtraUserControl _ucPosInvoice1;

    public MDIServiceDashboard()
    {
        InitializeComponent();
        _ucPosInvoice1 = CreateUserControl("POINT OF SALES INVOICE FIRST");
        accordionControl.SelectedElement = tbSalesInvoice1;
    }

    private static XtraUserControl CreateUserControl(string text)
    {
        var result = new xUCService
        {
            Name = $"{text.ToLower()} UserControl",
            Text = text
        };
        var label = new LabelControl { Parent = result };
        label.Appearance.Font = new Font("Tahoma", 25.25F);
        label.Appearance.ForeColor = Color.Gray;
        label.Dock = DockStyle.Fill;
        label.AutoSizeMode = LabelAutoSizeMode.None;
        label.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
        label.Appearance.TextOptions.VAlignment = VertAlignment.Center;
        label.Text = text;
        return result;
    }

    private void accordionControl_SelectedElementChanged(object sender, SelectedElementChangedEventArgs e)
    {
        try
        {
            var userControl = new XtraUserControl();
            if (e.Element == null) return;
            if (e.Element.Name == "tbSalesInvoice1") userControl = _ucPosInvoice1;
            tabbedView.AddDocument(userControl);
            tabbedView.ActivateDocument(userControl);
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }

    private void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
    {
        RecreateUserControls(e);
        SetAccordionSelectedElement(e);
    }

    private void SetAccordionSelectedElement(DocumentEventArgs e)
    {
        if (tabbedView.Documents.Count != 0)
            accordionControl.SelectedElement = e.Document.Caption switch
            {
                @"POINT OF SALES INVOICE FIRST" => tbSalesInvoice1,
                @"POINT OF SALES INVOICE SECOND" => tbSalesInvoice2,
                @"POINT OF SALES INVOICE THIRD" => tbSalesInvoice3,
                @"POINT OF SALES INVOICE FORTH" => tbSalesInvoice4,
                _ => accordionControl.SelectedElement
            };
        else
            accordionControl.SelectedElement = null;
    }

    private void RecreateUserControls(DocumentEventArgs e)
    {
        try
        {
            _ucPosInvoice1 = e.Document.Caption switch
            {
                @"POINT OF SALES INVOICE FIRST" => CreateUserControl("POINT OF SALES INVOICE FIRST"),
                @"POINT OF SALES INVOICE SECOND" => CreateUserControl("POINT OF SALES INVOICE FIRST"),
                _ => _ucPosInvoice1
            };
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
        }
    }
}