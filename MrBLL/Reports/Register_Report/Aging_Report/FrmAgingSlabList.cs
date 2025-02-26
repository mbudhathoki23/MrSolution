using MrDAL.Control.WinControl;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Aging_Report;

public partial class FrmAgingSlabList : MrForm
{
    public FrmAgingSlabList()
    {
        InitializeComponent();
    }

    private void FrmAgingSlabList_Load(object sender, EventArgs e)
    {
        GSlabList.Focus();
    }

    private void FrmAgingSlabList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape) Close();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void Control_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (GSlabList.CurrentCell.ColumnIndex != 1)
        {
            return;
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !short.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    #region --------------- Grid Event-------------------

    private void GSlabList_DataError(object sender, DataGridViewDataErrorEventArgs e)
    {
        e.Cancel = true;
    }

    private void GSlabList_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void GSlabList_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void GSlabList_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void GSlabList_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void GSlabList_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void GSlabList_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
    }

    private void GSlabList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        e.Control.KeyPress += Control_KeyPress;
    }

    #endregion --------------- Grid Event-------------------
}