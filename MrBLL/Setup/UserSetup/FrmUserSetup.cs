using MrBLL.Utility.Common.Class;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Setup.Interface;
using MrDAL.Setup.UserSetup;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmUserSetup : MrForm
{
    // USER SETUP
    #region -------------- USER SETUP --------------
    public FrmUserSetup()
    {
        InitializeComponent();
        _setup = new ClsMasterSetup();
        _userSetup = new UserSetupRepository();
        BindUserRole();
        ClearControl();
        ControlEnable();
    }
    private void FrmUserSetup_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], Tag);
    }
    private void FrmUserSetup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 27) //Action unload page
        {
            if (BtnNew.Enabled == false)
            {
                if (CustomMessageBox.ClearVoucherDetails("USER INFO") is DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ControlEnable();
                    ClearControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        ControlEnable(true);
        TxtUser.Focus();
    }
    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        ControlEnable(true);
        TxtUser.Focus();
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        ControlEnable();
        TxtUser.Focus();
    }
    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveUserInfo() > 0)
            {
                CustomMessageBox.ActionSuccess(TxtUser.GetUpper(), "SOFTWARE LOGIN USER", _actionTag);
                ClearControl();
                TxtUser.Focus();
            }
            else
            {
                TxtUser.ErrorMessage("ERROR OCCURS WHILE USER SETUP..!!");
                TxtUser.Focus();
                return;
            }
        }
        else
        {
            TxtUser.ErrorMessage("ERROR OCCURS WHILE USER SETUP..!!");
            TxtUser.Focus();
            return;
        }
    }
    private void BtnClear_Click(object sender, EventArgs e)
    {
        if (TxtUser.IsValueExits())
        {
            ClearControl();
        }
        else
        {
            BtnExit.PerformClick();
        }
    }
    private void BtnUser_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetUserInfoList("SAVE");
        if (result.id > 0)
        {
            _userId = result.id;
            SetGridDataToText();
            TxtUser.Enabled = false;
        }

        if (_actionTag.Equals("UPDATE"))
        {
            TxtUserFullName.Focus();
            return;
        }

        TxtUser.Focus();
    }
    private void TxtUser_Leave(object sender, EventArgs e)
    {

    }
    private void TxtUser_Validating(object sender, CancelEventArgs e)
    {
        if (TxtUser.IsValueExits() && _actionTag.IsValueExits())
        {
            var result = _userSetup.CheckUsernameExits(TxtUser.Text);
            if (result)
            {
                if (TxtUser.Enabled == false)
                {
                    return;
                }
                else
                {
                    TxtUser.WarningMessage("USER NAME IS ALREADY EXITS..!!");
                    return;
                }
            }
        }
    }
    private void TxtUser_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnUser_Click(sender, e);
        }
        else if (TxtUser.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtUser, BtnUser);
        }
    }
    private void TxtPassword_Validating(object sender, CancelEventArgs e)
    {
        if (TxtUser.IsValueExits())
        {
            if (TxtPassword.IsBlankOrEmpty() && TxtPassword.ValidControl(ActiveControl))
            {
                TxtPassword.WarningMessage("PASSWORD IS REQUIRED..!!");
                return;

            }
        }
    }
    private void TxtConfirm_Validating(object sender, CancelEventArgs e)
    {
        if (TxtConfirm.IsValueExits())
        {
            if (TxtPassword.Text != TxtConfirm.Text)
            {
                TxtPassword.WarningMessage("ENTER PASSWORD NOT MATCH..!!");
                return;
            }
        }
        else if (TxtPassword.IsValueExits())
        {
            TxtConfirm.WarningMessage("PASSWORD IS REQUIRED..!!");
            return;

        }
    }
    #endregion --------------- Event Handle ---------------


    // METHOD FOR THIS FORM
    #region --------------- METHOD ---------------
    private bool IsValidForm()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            BtnNew.Focus();
            return false;
        }

        if (_actionTag.Equals("DELETE"))
        {
            if (_userId is 0 || TxtUser.IsBlankOrEmpty())
            {
                TxtUser.WarningMessage("USER NAME IS REQUIRED..!!");
                return false;
            }
        }
        else
        {
            if (_actionTag.Equals("UPDATE") && _userId is 0)
            {
                TxtUser.WarningMessage("USER NAME IS REQUIRED..!!");
                return false;
            }
            if (TxtUser.IsBlankOrEmpty())
            {
                TxtUser.WarningMessage("USER NAME IS REQUIRED..!!");
                return false;
            }

            if (TxtUserFullName.IsBlankOrEmpty())
            {
                TxtUserFullName.WarningMessage("USER FULL NAME IS REQUIRED..!!");
                return false;
            }

            if (TxtPassword.Text != TxtConfirm.Text)
            {
                TxtPassword.WarningMessage("USER PASSWORD IS NOT MATCH ..!!");
                return false;
            }

            if (TxtPassword.IsBlankOrEmpty() || TxtConfirm.IsBlankOrEmpty())
            {
                TxtPassword.WarningMessage("USER PASSWORD IS REQUIRED..!!");
                return false;
            }

        }

        return true;
    }
    private int SaveUserInfo()
    {
        _userSetup.UserSetup.User_Id = _userId;
        _userSetup.UserSetup.User_Name = TxtUser.GetTrimReplace();
        _userSetup.UserSetup.Full_Name = TxtUserFullName.GetTrimReplace();
        _userSetup.UserSetup.Password = ObjGlobal.Encrypt(TxtPassword.Text.GetTrimReplace());
        _userSetup.UserSetup.Address = TxtAddress.GetTrimReplace();
        _userSetup.UserSetup.Mobile_No = TxtMobileNo.GetTrimReplace();
        _userSetup.UserSetup.Phone_No = TxtMobileNo.GetTrimReplace();
        _userSetup.UserSetup.Email_Id = TxtEmail.GetTrimReplace();
        _userSetup.UserSetup.Role_Id = (int?)CmbUserRole.SelectedValue;
        _userSetup.UserSetup.Status = ChkActive.Checked;
        return _userSetup.SaveUserInfo(_actionTag);
    }
    private void ClearControl()
    {
        Text = string.IsNullOrEmpty(_actionTag)
            ? "LOGIN USER SETUP DETAILS"
            : $"LOGIN USER SETUP DETAILS [{_actionTag}]";

        TxtUser.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";

        TxtUserFullName.Clear();
        TxtUser.Clear();

        TxtPassword.Clear();
        TxtConfirm.Clear();
        TxtAddress.Clear();
        TxtMobileNo.Clear();
        TxtTelPhoneNo.Clear();
        TxtEmail.Clear();
        ObjGlobal.BindUserRoleAsync(CmbUserRole);
        TxtPassword.Visible = true;
        TxtConfirm.Visible = true;
        if (ObjGlobal.SysCheckProfile == false)
        {
            BindUserRole();
        }
    }
    private void SetGridDataToText()
    {
        var dtUser = _userSetup.GetUserInformationUsingId(_userId);
        if (dtUser == null || dtUser.Rows.Count == 0)
        {
            return;
        }

        foreach (DataRow dr in dtUser.Rows)
        {
            TxtUserFullName.Text = Convert.ToString(dr["Full_Name"].ToString());
            TxtUser.Text = Convert.ToString(dr["User_Name"].ToString());
            TxtPassword.Text = ObjGlobal.Decrypt(Convert.ToString(dr["Password"].ToString()));
            TxtConfirm.Text = ObjGlobal.Decrypt(Convert.ToString(dr["Password"].ToString()));
            TxtAddress.Text = Convert.ToString(dr["Address"].ToString());
            TxtMobileNo.Text = Convert.ToString(dr["Mobile_No"].ToString());
            TxtTelPhoneNo.Text = Convert.ToString(dr["Phone_No"].ToString());
            TxtEmail.Text = Convert.ToString(dr["Email_Id"].ToString());
            CmbUserRole.SelectedValue = Convert.ToInt32(dr["Role_Id"].ToString());
        }
    }
    private void ControlEnable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtUser.Enabled = BtnUser.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
        TxtUserFullName.Enabled = isEnable;

        TxtPassword.Enabled = isEnable;
        TxtConfirm.Enabled = isEnable;
        TxtAddress.Enabled = isEnable;
        TxtMobileNo.Enabled = isEnable;
        TxtTelPhoneNo.Enabled = isEnable;
        TxtEmail.Enabled = isEnable;
        CmbUserRole.Enabled = isEnable;

        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "UPDATE";

        BtnSave.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
        BtnCancel.Enabled = BtnSave.Enabled;
    }
    private void BindUserRole()
    {
        const string query = "Select Role_Id,Role from Master.AMS.User_Role";
        var dt = query.GetQueryDataTable();
        CmbUserRole.DataSource = dt;
        CmbUserRole.DisplayMember = "Role";
        CmbUserRole.ValueMember = "Role_Id";
    }

    #endregion --------------- Method ---------------


    // OBJECT FOR THIS FORM
    #region -------------- GLOBAL VARIABLE --------------
    private int _userId;
    private readonly IMasterSetup _setup;
    private string _actionTag = string.Empty;
    private IUserSetupRepository _userSetup;
    #endregion -------------- Global Variable --------------
}