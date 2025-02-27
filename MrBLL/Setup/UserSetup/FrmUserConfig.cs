using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using MrDAL.Setup.Interface;
using MrDAL.Setup.UserSetup;

namespace MrBLL.Setup.UserSetup;

public partial class FrmUserConfig : MrForm
{
    // USER CONFIG
    #region --------------- USER CONFIG ---------------
    public FrmUserConfig()
    {
        InitializeComponent();
        _user = new UserSetupRepository();
        _getForm = new ClsMasterForm(this, BtnCancel);
        _objMaster = new ClsMasterSetup();
    }

    private void FrmUserConfig_Load(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        CmbCategory.SelectedIndex = 0;
        ClearControl();
        TxtUser.Focus();
    }

    private void FrmUserConfig_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (TxtUser.IsValueExits())
            {
                if (CustomMessageBox.Question("DO YOU WANT TO EXIT THE FORM..!!") is DialogResult.Yes)
                {
                    ClearControl();
                    TxtUser.Focus();
                }
            }
            else
            {
                BtnCancel.PerformClick();
            }
        }
    }

    private void TxtUser_Leave(object sender, EventArgs e)
    {

    }

    private void TxtUser_Validating(object sender, CancelEventArgs e)
    {
        if (_userId is 0 && TxtUser.IsBlankOrEmpty())
        {
            _userId = _objMaster.ReturnIntValueFromTable("MASTER.AMS.UserInfo", "User_Id", "User_Name", TxtUser.Text.Trim());
            if (_userId is 0)
            {
                TxtUser.WarningMessage("PLEASE ENTER USER NAME..!!");
                return;
            }
        }
    }

    private void BtnUser_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetUserInfoList("SAVE");
        if (result.id > 0)
        {
            _userId = result.id;
            FillUserDetails();
        }
        TxtUser.Focus();
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {

    }

    private void TxtLedger_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag, "CASH");
        if (description.IsValueExits())
        {
            _ledgerId = id;
            TxtLedger.Text = description;
        }
        TxtLedger.Focus();
    }

    private void TxtValidDays_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || !(e.KeyChar is not '.' || ((TextBox)sender).Text.Contains("."))) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out _);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveUserConfig() > 0)
            {
                CustomMessageBox.ActionSuccess(TxtUser.Text.GetUpper(), "USER CONFIGURATION", _actionTag);
                ClearControl();
                TxtUser.Focus();
                return;
            }
            else
            {
                TxtUser.WarningMessage($"ERROR OCCURS WHILE USER CONFIGURATION {_actionTag}");
                return;
            }
        }
        else
        {
            TxtUser.ErrorMessage($"ERROR OCCURS WHILE USER CONFIGURATION {_actionTag}");
            return;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
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

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else if (TxtLedger.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtLedger, BtnLedger);
        }
    }
    #endregion --------------- FrmUserConfig ---------------


    // METHOD FOR THIS FORM
    #region --------------- Methods ---------------

    private bool IsValidForm()
    {
        if (TxtUser.IsBlankOrEmpty())
        {
            MessageBox.Show(@"USER NAME CAN'T LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtUser.Focus();
            return false;
        }
        return true;
    }

    private int SaveUserConfig()
    {
        _user.UserSetup.User_Id = _userId;
        _user.UserSetup.Ledger_Id = _ledgerId.GetInt();
        _user.UserSetup.Posting_StartDate = MskPostStartDate.GetEnglishDate().GetDateTime();
        _user.UserSetup.Posting_EndDate = MskPostEndDate.GetEnglishDate().GetDateTime();
        _user.UserSetup.Modify_StartDate = MskModifyStartDate.GetEnglishDate().GetDateTime();
        _user.UserSetup.Modify_EndDate = MskModifyEndDate.GetEnglishDate().GetDateTime();
        _user.UserSetup.Valid_Days = TxtValidDays.Text.GetInt();
        _user.UserSetup.Category = CmbCategory.Text.ToUpper();
        _user.UserSetup.Authorized = ChkAuthorized.Checked;
        _user.UserSetup.IsRateChange = ChkChangeRate.Checked;
        _user.UserSetup.IsModify = ChkModify.Checked;
        _user.UserSetup.IsDeleted = ChkDelete.Checked;
        _user.UserSetup.IsPDCDashBoard = ChkPDCDashBoard.Checked;
        _user.UserSetup.Allow_Posting = ChkPost.Checked;
        _user.UserSetup.Auditors_Lock = ChkAuditLog.Checked;
        _user.UserSetup.IsQtyChange = ChkQtyChange.Checked;
        return _user.SaveUserInfo(_actionTag);
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"USER CONFIG SETUP [{_actionTag}]"
            : "USER CONFIG SETUP";
        _userId = 0;
        TxtUser.Clear();
        _ledgerId = 0;
        TxtLedger.Clear();
        MskPostStartDate.Text = MskModifyStartDate.Text = ObjGlobal.CfStartBsDate;
        MskModifyEndDate.Text = MskPostEndDate.Text = ObjGlobal.CfEndBsDate;
    }

    private void FillUserDetails()
    {
        var dtUser = _user.GetUserInformationUsingId(_userId);
        foreach (DataRow dr in dtUser.Rows)
        {
            _userId = dr["User_Id"].GetInt();
            TxtUser.Text = Convert.ToString(dr["User_Name"].ToString());
            _ledgerId = dr["Ledger_Id"].GetLong();
            TxtLedger.Text = dr["GLName"].ToString();

            MskPostStartDate.Text = dr["Posting_StartDate"].IsValueExits() ? dr["Posting_StartDate"].GetNepaliDate() : ObjGlobal.CfStartBsDate;
            MskPostEndDate.Text = dr["Posting_EndDate"].IsValueExits() ? dr["Posting_EndDate"].GetNepaliDate() : ObjGlobal.CfEndBsDate;
            MskModifyStartDate.Text = dr["Modify_StartDate"].IsValueExits() ? dr["Modify_StartDate"].GetNepaliDate() : ObjGlobal.CfStartBsDate;
            MskModifyEndDate.Text = dr["Modify_EndDate"].IsValueExits() ? dr["Modify_EndDate"].GetNepaliDate() : ObjGlobal.CfEndBsDate;

            TxtValidDays.Text = dr["Valid_Days"].GetIntString();
            CmbCategory.Text = dr["Category"].ToString();
            ChkAuthorized.Checked = dr["Authorized"].GetBool();
            ChkPost.Checked = dr["Allow_Posting"].GetBool();
            ChkModify.Checked = dr["IsModify"].GetBool();
            ChkDelete.Checked = dr["IsDeleted"].GetBool();
            ChkPDCDashBoard.Checked = dr["IsPDCDashBoard"].GetBool();
            ChkChangeRate.Checked = dr["IsRateChange"].GetBool();
            ChkPost.Checked = dr["Allow_Posting"].GetBool();
            ChkAuditLog.Checked = dr["Auditors_Lock"].GetBool();
            ChkQtyChange.Checked = dr["IsQtyChange"].GetBool();
        }
    }

    #endregion --------------- Methods ---------------


    // GLOBAL OBJECT FOR THIS FORM
    #region --------------- Global Variable ---------------
    private int _userId;
    private long _ledgerId;
    private string _actionTag = string.Empty;
    private readonly IMasterSetup _objMaster;
    private readonly IUserSetupRepository _user;
    private ClsMasterForm _getForm;

    #endregion --------------- Global Variable ---------------




}