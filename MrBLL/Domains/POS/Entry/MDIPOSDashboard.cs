using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using MrBLL.Domains.POS.Entry.uControl;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry;

public partial class MDIPOSDashboard : RibbonForm
{
    private XtraUserControl _ucPosInvoice1;
    private XtraUserControl _ucPosInvoice2;
    private XtraUserControl _ucPosInvoice3;
    private XtraUserControl _ucPosInvoice4;

    public MDIPOSDashboard()
    {
        InitializeComponent();
        _ucPosInvoice1 = CreateUserControl("POINT OF SALES INVOICE FIRST");
        _ucPosInvoice2 = CreateUserControl("POINT OF SALES INVOICE SECOND");
        _ucPosInvoice3 = CreateUserControl("POINT OF SALES INVOICE THIRD");
        _ucPosInvoice4 = CreateUserControl("POINT OF SALES INVOICE FORTH");
        accordionControl.SelectedElement = tbSalesInvoice1;
        ribbonControl.Minimized = true;
    }

    private static XtraUserControl CreateUserControl(string text)
    {
        var result = new PointOfSalesInvoice()
        {
            Name = $"{text.ToLower()}UserControl",
            Text = text
        };
        var label = new LabelControl
        {
            Parent = result
        };
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
            userControl = e.Element.Name switch
            {
                "tbSalesInvoice1" => _ucPosInvoice1,
                "tbSalesInvoice2" => _ucPosInvoice2,
                "tbSalesInvoice3" => _ucPosInvoice3,
                "tbSalesInvoice4" => _ucPosInvoice4,
                _ => userControl
            };
            tabbedView.AddDocument(userControl);
            tabbedView.ActivateDocument(userControl);
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
        }
    }

    private void barButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
    {
        var barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
        accordionControl.SelectedElement = mainAccordionGroup.Elements[barItemIndex];
    }

    private void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
    {
        RecreateUserControls(e);
        SetAccordionSelectedElement(e);
    }

    private void SetAccordionSelectedElement(DocumentEventArgs e)
    {
        if (tabbedView.Documents.Count != 0)
        {
            accordionControl.SelectedElement = e.Document.Caption switch
            {
                @"POINT OF SALES INVOICE FIRST" => tbSalesInvoice1,
                @"POINT OF SALES INVOICE SECOND" => tbSalesInvoice2,
                @"POINT OF SALES INVOICE THIRD" => tbSalesInvoice3,
                @"POINT OF SALES INVOICE FORTH" => tbSalesInvoice4,
                _ => accordionControl.SelectedElement
            };
        }
        else
        {
            accordionControl.SelectedElement = null;
        }
    }

    private void RecreateUserControls(DocumentEventArgs e)
    {
        try
        {
            switch (e.Document.Caption)
            {
                case @"POINT OF SALES INVOICE FIRST":
                {
                    _ucPosInvoice1 = CreateUserControl("POINT OF SALES INVOICE FIRST");
                    break;
                }
                case @"POINT OF SALES INVOICE SECOND":
                {
                    _ucPosInvoice2 = CreateUserControl("POINT OF SALES INVOICE SECOND");
                    break;
                }
                case @"POINT OF SALES INVOICE THIRD":
                {
                    _ucPosInvoice3 = CreateUserControl("POINT OF SALES INVOICE THIRD");
                    break;
                }
                case @"POINT OF SALES INVOICE FORTH":
                {
                    _ucPosInvoice4 = CreateUserControl("POINT OF SALES INVOICE FORTH");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
        }
    }
}