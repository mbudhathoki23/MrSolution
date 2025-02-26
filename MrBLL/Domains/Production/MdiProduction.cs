using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.Production;

public partial class MdiProduction : RibbonForm
{
    private XtraUserControl loomSetupControl;
    private XtraUserControl wrapControl;

    public MdiProduction()
    {
        InitializeComponent();
        wrapControl = CreateWrapControl("WRAP SETUP");
        loomSetupControl = CreateLoomControl("LOOM SETUP");
        accordionControl.SelectedElement = employeesAccordionControlElement;
    }

    private XtraUserControl CreateWrapControl(string text)
    {
        var result = new XtraUserControl
        {
            Name = text.ToLower() + "UserControl",
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

    private XtraUserControl CreateLoomControl(string text)
    {
        var result = new XtraUserControl
        {
            Name = text.ToLower() + "USER CONTROL",
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
        if (e.Element == null) return;
        var userControl = e.Element.Text.Equals("WRAP SETUP") ? wrapControl : loomSetupControl;
        tabbedView.AddDocument(userControl);
        tabbedView.ActivateDocument(userControl);
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
            accordionControl.SelectedElement = e.Document.Caption == "Employees"
                ? customersAccordionControlElement
                : employeesAccordionControlElement;
        else
            accordionControl.SelectedElement = null;
    }

    private void RecreateUserControls(DocumentEventArgs e)
    {
        switch (e.Document.Caption)
        {
            case "WRAP SETUP":
            {
                wrapControl = CreateWrapControl("WRAP SETUP");
                break;
            }
            case "LOOM SETUP":
            {
                loomSetupControl = CreateLoomControl("LOOM SETUP");
                break;
            }
        }
    }
}