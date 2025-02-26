using MrDAL.Core.Extensions;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Master;

public partial class FrmConfirmationDiscount : Form
{
    public FrmConfirmationDiscount(string basicAmount)
    {
        InitializeComponent();
        LblBasicAmount.Text = basicAmount.GetDecimalString();
    }

    private void FrmConfirmationDiscount_Load(object sender, EventArgs e)
    {
        TxtPercent.Focus();
    }

    private void FrmConfirmationDiscount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtPercent_TextChanged(object sender, EventArgs e)
    {
        TxtAmount.Text = (TxtPercent.GetDecimal() * LblBasicAmount.GetDecimal() / 100).GetDecimalString();
    }

    private void TxtAmount_Validating(object sender, CancelEventArgs e)
    {
        TxtAmount.Text = TxtAmount.GetDecimalString();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
    }
}