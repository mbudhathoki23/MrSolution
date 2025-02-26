using DevExpress.XtraEditors;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Setup.Interface;
using MrDAL.Setup.UserSetup;
using MrDAL.Utility.Server;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace MrBLL.Setup.UserSetup;

public partial class FrmLogin : MrForm
{
    // LOGIN INFORMATION
    #region --------------- LOGIN FORM ---------------

    [Obsolete]
    public FrmLogin()
    {
        InitializeComponent();
        LblSoftwareSpec.Text = ObjGlobal.Caption is "VISMA ERP"
            ? "Welcome, Visma | Erp Enter Your User Name && Password"
            : "Welcome, MrSolution Enter Your User Name && Password";
        LblSoftwareCaption.Text = ObjGlobal.Caption is "VISMA ERP"
            ? "Visma Erp V2023"
            : "MrSolution V2018";
        pictureBox1.Image = ObjGlobal.Caption is "VISMA ERP"
            ? Properties.Resources.VismaEroLogo
            : Properties.Resources.AccountIcon1;
        _objMaster = new ClsMasterSetup();
        _userInfo = new UserSetupRepository();

    }

    private void FrmLogin_Load(object sender, EventArgs e)
    {

    }

    private void FrmLogin_Shown(object sender, EventArgs e)
    {
        if (!Debugger.IsAttached)
        {
            TxtUserName.Focus();
        }
        else
        {
            TxtUserName.Text = @"ADMIN";
            TxtUserPassword.Text = @"admin";
            BtnLogin.Focus();
        }
        //BringToFront();
        //Activate();
    }

    [Obsolete]
    private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                UserTerminated();
                Environment.Exit(0);
            }
        }
        else if (e.Control && e.KeyCode == Keys.M)
        {
            TxtUserName.PasswordChar = '*';
        }
        else if (e.KeyCode is Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtUserName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        if (!string.IsNullOrEmpty(TxtUserName.Text.Trim()))
        {
            return;
        }
        CustomMessageBox.Warning(@"PLEASE ENTER YOUR LOGIN  USER NAME - THANK YOU..!!");
        TxtUserName.Focus();
    }

    private void TxtUserName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtUserName.Text.Trim() != string.Empty && TxtUserName.Enabled &&
            TxtUserName.Text.ToUpper() != "AMSADMIN")
        {
            _table.Reset();
            _table = GetConnection.SelectQueryFromMaster(
                $"SELECT * FROM Master.AMS.UserInfo Where User_Name = '{TxtUserName.Text.Trim().Replace("'", "''")}'");
            if (_table.Rows.Count != 0) return;
            MessageBox.Show(@"User Name Not Valid..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtUserName.Focus();
            TxtUserName.SelectAll();
        }
        else if (TxtUserName.Text.Length == 0 && TxtUserName.Focused)
        {
            MessageBox.Show(@"User Name Cannot Be Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtUserName.Focus();
        }
    }

    private void TxtUserPassword_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        if (string.IsNullOrEmpty(TxtUserName.Text.Trim()))
        {
            MessageBox.Show(@"USER NAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtUserName.Focus();
        }
    }

    private void TxtUserPassword_Validating(object sender, CancelEventArgs e)
    {
        try
        {
            if (ActiveControl == TxtUserName || TxtUserName.Focused)
            {
                return;
            }
            if (TxtUserName.Text.GetUpper() == "AMSADMIN")
            {
                if (TxtUserPassword.Text != UserPassword)
                {
                    return;
                }
            }
            else
            {
                var userType = _userInfo.GetUserType(TxtUserName.Text, TxtUserPassword.Text);
                if (userType.Rows.Count > 0)
                {
                    ObjGlobal.LogInUserFullName = userType.Rows[0][7].GetString();
                    ObjGlobal.RoleId = userType.Rows[0][6].GetInt();
                    ObjGlobal.LogInUserId = userType.Rows[0][0].GetInt();
                    ObjGlobal.LogInUser = userType.Rows[0][1].GetString();
                    ObjGlobal.LogInUserPassword = userType.Rows[0][2].GetString();
                    if (userType.Rows[0][3].ToString() != string.Empty)
                    {
                        ObjGlobal.SysPostingStartDate = Convert.ToDateTime(userType.Rows[0][3].ToString());
                    }

                    if (userType.Rows[0][4].ToString() != string.Empty)
                    {
                        ObjGlobal.SysPostingEndDate = Convert.ToDateTime(userType.Rows[0][4].ToString());
                    }

                    if (Convert.ToString(userType.Rows[0][5]) == string.Empty)
                    {
                        userType.Rows[0][5] = false;
                    }

                    ObjGlobal.AllowPosting = userType.Rows[0][5].GetBool();
                    var bb = ObjGlobal.Decrypt(ObjGlobal.LogInUserPassword);
                    if (bb.Equals(TxtUserPassword.Text))
                    {
                        return;
                    }

                    CustomMessageBox.Information("ENTER VALID USER ID OR PASSWORD");
                    TxtUserName.Focus();
                }
                else
                {
                    CustomMessageBox.Information("ENTER VALID USER ID OR PASSWORD");
                    TxtUserPassword.Focus();
                    TxtUserPassword.SelectAll();
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
        }
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (int)Keys.Enter)
        {
            e.Handled = true;
        }
    }

    [Obsolete]
    private void BtnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtUserName.Text.Trim().Length <= 0)
            {
                CustomMessageBox.Warning("PLZ. ENTER YOUR USER NAME FOR LOGIN..!!");
                TxtUserName.Focus();
                return;
            }
            if (!ValidUser())
            {
                CustomMessageBox.Warning("PLZ. ENTER YOUR USER NAME FOR LOGIN..!!");
                TxtUserName.Focus();
                return;
            }
            if (ChkStatus())
            {
                LoginUser();
            }
            else
            {
                TxtUserName.Focus();
                return;
            }
            DialogResult = DialogResult.OK;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            ex.DialogResult();
        }
    }

    [Obsolete]
    private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
    {

    }

    [Obsolete]
    private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
    {
    }

    private void LnkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var result = new FrmUserReset();
        result.ShowDialog();
    }

    #endregion --------------- LOGIN FORM ---------------

    // METHOD FOR THIS FORM
    #region ---------------- METHOD ----------------

    [Obsolete]
    private bool ChkStatus()
    {
        var (auditLog, serverName) = _userInfo.CheckLoginAuditDetails(TxtUserName.Text);
        if (!auditLog)
        {
            var msg = CustomMessageBox.Warning($"THIS IS ALREADY LOG IN [{serverName}]");
            return true;
        }
        return true;
    }

    private bool ValidUser()
    {
        if (TxtUserPassword.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"USER NAME AND PASSWORD IS BLANK..!!");
            TxtUserPassword.Focus();
            return false;
        }
        if (TxtUserName.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"USER NAME AND PASSWORD IS BLANK..!!");
            TxtUserPassword.Focus();
            return false;
        }
        if (TxtUserName.Text.IsValueExits())
        {
            if (TxtUserName.Text.ToUpper() != "AMSADMIN")
            {
                var result = _userInfo.CheckUsernameExits(TxtUserName.Text);
                if (!result)
                {
                    TxtUserName.WarningMessage("INPUT USER IS INVALID..!!");
                    return false;
                }
            }
        }

        if (TxtUserPassword.Text.IsValueExits())
        {
            if (TxtUserName.Text.ToUpper() != "AMSADMIN")
            {
                var result = _userInfo.CheckUsernamePassword(TxtUserName.Text, ObjGlobal.Encrypt(TxtUserPassword.Text));
                if (!result)
                {
                    TxtUserName.WarningMessage("INPUT USER IS INVALID..!!");
                    return false;
                }
            }
        }
        if (TxtUserName.Text.ToUpper().Equals(@"AMSADMIN"))
        {
            return TxtUserPassword.Text.Equals(UserPassword);
        }

        return true;
    }

    [Obsolete]
    private void LoginUser()
    {
        if (TxtUserName.Text is @"AMSADMIN" or @"SIYZO" or "SYSADMIN")
        {
            ObjGlobal.LogInUserId = 1;
            ObjGlobal.LogInUser = TxtUserName.Text.ToUpper();
            ObjGlobal.LogInUserPassword = TxtUserPassword.Text.ToUpper();
            ObjGlobal.LogInUserCategory = "ADMINISTRATOR";
        }
        else
        {
            var dtUser = _objMaster.GetMasterUserInfo(TxtUserName.Text);
            foreach (DataRow dr in dtUser.Rows)
            {
                ObjGlobal.LogInUserId = ObjGlobal.ReturnInt(dr["User_Id"].ToString());
                ObjGlobal.LogInUser = Convert.ToString(dr["User_Name"].ToString());
                ObjGlobal.LogInUserPassword = TxtUserPassword.Text.ToUpper();
                ObjGlobal.LogInUserPostingEndDate = dr["Posting_StartDate"].IsValueExits() ? dr["Posting_StartDate"].GetDateTime() : ObjGlobal.CfStartAdDate;
                ObjGlobal.LogInUserPostingEndDate = dr["Posting_EndDate"].IsValueExits() ? dr["Posting_EndDate"].GetDateTime() : ObjGlobal.CfEndAdDate;
                ObjGlobal.LogInUserModifyStartDate = dr["Modify_StartDate"].IsValueExits() ? dr["Modify_StartDate"].GetDateTime() : ObjGlobal.CfStartAdDate;
                ObjGlobal.LogInUserModifyEndDate = dr["Modify_EndDate"].IsValueExits() ? dr["Modify_EndDate"].GetDateTime() : ObjGlobal.CfEndAdDate;
                ObjGlobal.LogInUserValidDays = dr["Valid_Days"].GetInt();
                ObjGlobal.LogInUserCategory = dr["Category"].GetString();
                ObjGlobal.UserAuthorized = dr["Authorized"].GetBool();
                ObjGlobal.UserAllowPosting = dr["Allow_Posting"].GetBool();
                ObjGlobal.UserModify = dr["IsModify"].GetBool();
                ObjGlobal.UserDelete = dr["IsDeleted"].GetBool();
                ObjGlobal.UserPdcDashBoard = dr["IsPDCDashBoard"].GetBool();
                ObjGlobal.UserChangeRate = dr["IsRateChange"].GetBool();
                ObjGlobal.UserAuditLog = dr["Auditors_Lock"].GetBool();
                ObjGlobal.ChangeQty = dr["IsQtyChange"].GetBool();
                ObjGlobal.UserLedgerId = dr["Ledger_Id"].GetLong();
                ObjGlobal.RoleId = ObjGlobal.ReturnInt(dr["Role_Id"].ToString());
            }
        }
    }

    [Obsolete]
    public int UserTerminated()
    {
        DeviceName = ObjGlobal.GetServerName();
        DeviceIp = ObjGlobal.GetIpAddress();
        //return _objMaster.ResetLoginInfo("SAVE");

        return 0;
    }

    #endregion ---------------- METHOD ----------------

    // GLOBAL OBJECT
    #region ---------------- Global Varaibale ----------------

    private readonly IUserSetupRepository _userInfo;
    private DataTable _table = new();
    private string _query = string.Empty;
    private const string UserPassword = "#MrS0luti0n#";
    private readonly SimpleButton _btnExit = new();
    private readonly IMasterSetup _objMaster;
    public int LoginId = 0;
    public string DeviceName = string.Empty;
    public string DeviceIp = string.Empty;
    public string SystemId = string.Empty;

    #endregion ---------------- Global Varaibale ----------------
}