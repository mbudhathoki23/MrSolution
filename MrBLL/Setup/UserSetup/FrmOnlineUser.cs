using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using System;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmOnlineUser : MrForm
{
    private readonly SimpleButton BtnExit = new();
    private ClsMasterForm GetForm;
    public bool IsVerify = false;
    public string OnlineLoginUser;
    public int OnlineLoginUserId;

    public FrmOnlineUser()
    {
        InitializeComponent();
        GetForm = new ClsMasterForm(this, BtnExit);
    }

    private void FrmOnlineUser_Load(object sender, EventArgs e)
    {
    }

    private void FrmOnlineUser_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape) BtnExit.PerformClick();
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
    }
}