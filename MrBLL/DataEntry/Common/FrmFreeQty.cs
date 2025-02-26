using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmFreeQty : MrForm
{
    public decimal FreeQty;

    public FrmFreeQty(decimal freeQty = 0)
    {
        InitializeComponent();
        FreeQty = freeQty;
    }

    private void FrmFreeQty_Load(object sender, EventArgs e)
    {
        TxtFreeQty.Text = FreeQty.GetDecimalQtyString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        FreeQty = TxtFreeQty.GetDecimal();
        Close();
        return;
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        Close();
        return;
    }

    private void TxtFreeQty_Validating(object sender, CancelEventArgs e)
    {
        SendKeys.Send("{TAB}");
    }
}