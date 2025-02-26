using MrDAL.Control.WinControl;
using System;
using System.Drawing;

namespace MrBLL.Reports.Register_Report.Register_Report;

public partial class FrmLedgerReconcile : MrForm
{
    public FrmLedgerReconcile()
    {
        InitializeComponent();
    }

    private void LedgerReconcile_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
    }
}