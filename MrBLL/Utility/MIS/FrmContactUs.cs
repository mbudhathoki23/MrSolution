using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.MIS;

public partial class FrmContactUs : MrForm
{
    public FrmContactUs()
    {
        InitializeComponent();
    }

    private void FrmContactUs_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
    }

    private void FrmContactUs_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            if (e.KeyChar == 32)
                SendKeys.Send("{F4}");
            if (e.KeyChar == 39)
                e.KeyChar = '0';
            if (e.KeyChar == 14) //Action New
            {
            }

            if (e.KeyChar == 21) //Action Update
            {
            }

            if (e.KeyChar == 4) //Action Delete
            {
            }

            if (e.KeyChar == 27) //Action unload page
            {
                var dialogResult = MessageBox.Show(@"Are you sure want to Close Form!", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes) Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
        }
    }

    private void label6_Click(object sender, EventArgs e)
    {
    }

    private void label5_Click(object sender, EventArgs e)
    {
    }

    private void label4_Click(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void label1_Click(object sender, EventArgs e)
    {
    }
}