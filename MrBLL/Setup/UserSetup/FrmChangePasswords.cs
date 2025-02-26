using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmChangePasswords : MrForm
{
    public FrmChangePasswords()
    {
        InitializeComponent();
        //_userInfo = new ClsUserInfo();
    }

    private void FrmChangePassword_Load(object sender, EventArgs e)
    {
        TxtOldPassword.Focus();
    }

    private void FrmChangePassword_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
                return;
            }
        }
    }

    private void TxtOldPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtOldPassword.IsBlankOrEmpty())
            {
                TxtOldPassword.WarningMessage(@"OLD PASSWORD CANNOT LEFT BLANK..!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtNewPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtNewPassword.IsBlankOrEmpty())
            {
                TxtNewPassword.WarningMessage("NEW PASSWORD IS REQUIRED FOR PASSWORD CHANGE..!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtConfirmPassword.IsBlankOrEmpty())
            {
                TxtConfirmPassword.WarningMessage("CONFORMATION PASSWORD IS REQUIRED TO CHANGE PASSWORD..!!");
                return;
            }
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtOldPassword_Validating(object sender, CancelEventArgs e)
    {
        if (TxtOldPassword.IsBlankOrEmpty() && TxtOldPassword.ValidControl(ActiveControl))
        {
            TxtOldPassword.WarningMessage("PLEASE ENTER OLD PASSWORD TO CHANGE THE PASSWORD..!!");
            return;
        }
    }

    private void TxtNewPassword_Validating(object sender, CancelEventArgs e)
    {
        if (TxtNewPassword.IsBlankOrEmpty() && TxtNewPassword.ValidControl(ActiveControl))
        {
            TxtNewPassword.WarningMessage("PLEASE ENTER NEW PASSWORD TO CHANGE THE PASSWORD");
            return;
        }
    }

    private void TxtConfirmPassword_Validating(object sender, CancelEventArgs e)
    {
        if ((TxtNewPassword.IsValueExits() || TxtConfirmPassword.IsValueExits()) && TxtOldPassword.IsValueExits())
        {
            if (TxtNewPassword.Text != TxtConfirmPassword.Text)
            {
                TxtNewPassword.WarningMessage("ENTER NEW PASSWORD AND CONFORMATION PASSWORD IS NOT MATCH");
                return;
            }
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (TxtNewPassword.IsBlankOrEmpty() || TxtConfirmPassword.IsBlankOrEmpty())
        {
            TxtNewPassword.WarningMessage("ENTER PASSWORD IS INVALID..!!");
            return;
        }
        if (TxtNewPassword.Text == TxtConfirmPassword.Text)
        {

            //var password = _userInfo.CheckUsernamePassword(ObjGlobal.LogInUser, TxtOldPassword.Text);
            //if (!password)
            //{
            //    CustomMessageBox.Warning("CONTACT TO ADMIN FOR CHANGE PASSWORD..!!");
            //    return;
            //}
            //var changePassword = _userInfo.ChangePassword(ObjGlobal.LogInUser, TxtNewPassword.Text);
            //if (changePassword == 0)
            //{
            //    return;
            //}
            CustomMessageBox.Information($"{ObjGlobal.LogInUser} USER PASSWORD CHANGE SUCCESSFULLY.!!");
            ClearControl();
        }
        else
        {
            TxtNewPassword.WarningMessage("ERROR OCCURS WHILE CHANGE PLEASE CONTACT WITH YOU ADMIN..!!");
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
        Close();
    }

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    public void ClearControl()
    {
        TxtNewPassword.Text = string.Empty;
        TxtOldPassword.Text = string.Empty;
        TxtConfirmPassword.Text = string.Empty;
        TxtOldPassword.Focus();
        Close();
    }

    #endregion --------------- Method ---------------

    // OBJECT OF THIS FORM
    //private readonly ClsUserInfo _userInfo;
}