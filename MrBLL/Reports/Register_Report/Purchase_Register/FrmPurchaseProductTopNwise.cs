using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Purchase_Register;

public partial class FrmPurchaseProductTopNwise : MrForm
{
    private readonly string Ledger_Code = string.Empty;
    private readonly string Product_Code = string.Empty;
    private string FromADDate;
    private string FromBSDate;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmPurchaseProductTopNwise()
    {
        InitializeComponent();
    }

    private void FrmPurchaseProductTopNwise_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 0);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        cmb_OrderBy.SelectedIndex = 0;
        cmb_OrderOn.SelectedIndex = 0;
        ObjGlobal.BindBranch(cmb_Branch);
        msk_FromDate.Focus();
    }

    private void FrmPurchaseProductTopNwise_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void msk_FromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'E');
    }

    private void msk_FromDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'L');
    }

    private void msk_FromDate_Validated(object sender, EventArgs e)
    {
    }

    private void msk_ToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'E');
    }

    private void msk_ToDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_ToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'L');
    }

    private void msk_ToDate_Validated(object sender, EventArgs e)
    {
    }

    private void txt_NoOfItems_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }
}