using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmBarcodePrint : MrForm
{
    public FrmBarcodePrint()
    {
        InitializeComponent();
    }

    private void FrmBarcodePrint_Load(object sender, EventArgs e)
    {
    }

    private void BtnProduct_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProduct("SAVE", DateTime.Now.GetDateString());
        if (description.IsValueExits())
        {
        }
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtProduct_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
    }

    private void TxtProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtProduct.Text.Trim() != string.Empty)
            {
                SendKeys.Send("{TAB}");
            }
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnProduct.PerformClick();
        }
    }

    private void TxtSalesRate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && TxtSalesRate.Text.Trim() != string.Empty)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtQty_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && txtQty.Text.Trim() != string.Empty)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        Close();
    }
}