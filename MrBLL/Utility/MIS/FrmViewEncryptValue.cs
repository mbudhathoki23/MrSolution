using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Windows.Forms;

namespace MrBLL.Utility.MIS;

public partial class FrmViewEncryptValue : Form
{
    public FrmViewEncryptValue()
    {
        InitializeComponent();
    }
    private void FrmViewEncryptValue_Load(object sender, EventArgs e)
    {

    }
    private void FrmViewEncryptValue_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar is (char)Keys.Escape)
        {
            var msg = CustomMessageBox.ExitActiveForm();
            if (msg == DialogResult.Yes)
            {
                Dispose(true);
            }
        }
    }

    private void BtnConvert_Click(object sender, EventArgs e)
    {
        TxtResult.Text = ObjGlobal.Decrypt(TxtInputValue.Text);
    }

    private void Button1_Click(object sender, EventArgs e)
    {
        TxtInputValue.Clear();
        TxtResult.Clear();
    }
}