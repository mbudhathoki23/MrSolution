using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using System;

namespace MrBLL.DataEntry.Common;

public partial class FrmVoucherAdjustment : MrForm
{
    private readonly MrGridTextBox TxtAdjustmentAmount;

    public FrmVoucherAdjustment()
    {
        InitializeComponent();
        TxtAdjustmentAmount = new MrGridTextBox(DGrid);
        GridControlMode(false);
    }

    private void FrmVoucherAdjustment_Load(object sender, EventArgs e)
    {
    }

    private void GridControlMode(bool isFocus)
    {
        if (DGrid.CurrentRow != null)
        {
            var rowIndex = DGrid.CurrentRow.Index;
            if (isFocus)
            {
                var colIndex = DGrid.Columns[@"GTxtAdjustment"].Index;
                TxtAdjustmentAmount.Size = DGrid.GetCellDisplayRectangle(colIndex, rowIndex, true).Size;
            }
        }
    }
}