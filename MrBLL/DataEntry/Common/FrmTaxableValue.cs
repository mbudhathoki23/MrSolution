using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmTaxableValue : MrForm
{
    public FrmTaxableValue()
    {
        InitializeComponent();
    }

    public double Taxable_Value { get; set; }

    private void FrmTaxableValue_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        txt_TaxableValue.Text = Taxable_Value.ToString(ObjGlobal.SysAmountFormat);
        txt_TaxableValue.Focus();
    }

    private void TaxableValue_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape) Close();
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
        Taxable_Value = Convert.ToDouble(ObjGlobal.ReturnDecimal(txt_TaxableValue.Text));

        DialogResult = DialogResult.OK;
        Close();
    }

    private void txt_TaxableValue_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_TaxableValue, 'E');
    }

    private void txt_TaxableValue_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter || e.KeyData == Keys.Tab) btn_Save.Focus();
    }

    private void txt_TaxableValue_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void txt_TaxableValue_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_TaxableValue, 'L');
        txt_TaxableValue.Text = Convert.ToDouble(ObjGlobal.ReturnDecimal(txt_TaxableValue.Text))
            .ToString(ObjGlobal.SysAmountFormat);
    }
}