using DevExpress.Utils.Extensions;
using MrBLL.DataEntry.StockMaster.UserControl;
using System.Windows.Forms;

namespace MrBLL.DataEntry.StockMaster;

public partial class FrmStockManagement : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
{
    public FrmStockManagement()
    {
        InitializeComponent();
    }

    private void StockAdjustmentMenu_Click(object sender, System.EventArgs e)
    {
        var control = new StockAdjustment();
        UserControlContainer.AddControl(control);
        control.Dock = DockStyle.Fill;
        control.BringToFront();
    }

    private void PhysicalStockMenu_Click(object sender, System.EventArgs e)
    {
    }

    private void GodownTransferMenu_Click(object sender, System.EventArgs e)
    {
    }

    private void MenuItemControl_Click(object sender, System.EventArgs e)
    {
    }
}