using MrDAL.Control.WinControl;
using System;
using System.Drawing;

namespace MrBLL.Reports.Register_Report.Register_Report;

public partial class FrmCreditNoteRegister : MrForm
{
    public FrmCreditNoteRegister()
    {
        InitializeComponent();
    }

    private void CreditNoteRegister_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
    }
}