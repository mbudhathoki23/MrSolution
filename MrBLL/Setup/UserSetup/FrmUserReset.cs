using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Setup.Interface;
using MrDAL.Setup.UserSetup;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmUserReset : MrForm
{
    // RESET USER FORM
    #region ---------------FORM---------------
    public FrmUserReset()
    {
        InitializeComponent();
        _userSetup = new UserSetupRepository();
    }
    private void FrmUserReset_Load(object sender, EventArgs e)
    {
        if (!Debugger.IsAttached)
        {
            TxtUserName.Focus();
        }
    }

    private void BtnReset_Click(object sender, EventArgs e)
    {
        if (TxtUserName.IsBlankOrEmpty())
        {
            TxtUserName.WarningMessage("USER NAME IS REQUIRED FOR RESET..!!");
            return;

        }

        if (TxtUserPassword.IsBlankOrEmpty())
        {
            TxtUserPassword.WarningMessage("USER PASSWORD IS REQUIRED FOR RESET");
            return;
        }

        if (ValidUser().Equals(true))
        {
            var result = ResetLoginUser();
            CustomMessageBox.Information("USER RESET SUCCESSFULLY..!!");
            Close();
            return;
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CHECK YOUR USER NAME & PASSWORD...!!");
            return;
        }

    }

    private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtUserName.IsBlankOrEmpty())
            {
                TxtUserName.WarningMessage("USER NAME REQUIRED...!!");
                return;
            }
        }

    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }


    #endregion ---------------EVENT---------------

    // METHOD FOR THIS FORM
    #region ---------------METHOD---------------

    private bool ValidUser()
    {
        var password = ObjGlobal.Encrypt(TxtUserPassword.Text);
        return _userSetup.CheckUsernamePassword(TxtUserName.Text, password); ;
    }

    private int ResetLoginUser()
    {
        var result = _userSetup.ResetLoginLog(TxtUserName.Text);
        return result;
    }
    #endregion ---------------METHOD---------------


    // OBJECT FOR THIS FORM
    #region ---------------GLOBAL---------------
    public string DeviceName = string.Empty;
    public string DeviceIp = string.Empty;
    public string SystemId = string.Empty;
    private readonly IUserSetupRepository _userSetup;

    #endregion ---------------GLOBAL---------------
}