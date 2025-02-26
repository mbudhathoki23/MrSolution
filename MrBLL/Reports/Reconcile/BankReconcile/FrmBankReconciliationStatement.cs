using MrDAL.Control.WinControl;
using MrDAL.Global.Control;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.Reconcile.BankReconcile;

public partial class FrmBankReconciliationStatement : MrForm
{
    public FrmBankReconciliationStatement()
    {
        InitializeComponent();
    }

    private void FrmBankReconciliationStatement_Load(object sender, EventArgs e)
    {
    }

    private void FrmBankReconciliationStatement_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void FrmBankReconciliationStatement_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
}