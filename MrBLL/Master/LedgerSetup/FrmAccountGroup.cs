using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.LedgerSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = System.Drawing.Font;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmAccountGroup : MrForm
{
    // ACCOUNT GROUP EVENTS

    #region --------------- Form ---------------

    public FrmAccountGroup(bool zoom = false)
    {
        InitializeComponent();
        _isZoom = zoom;
        _groupRepository = new AccountGroupRepository();
        _setup = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        lblNepali.Font = new Font("Kantipur", 12F);
        TxtNepaliName.Font = new Font("Kantipur", 12F);
        _groupRepository.BindPrimaryGroup(CmbPrimaryGroup);
        _groupRepository.BindAccountGrpType(CmbGroupType, CmbPrimaryGroup.SelectedValue.ToString());
    }

    private void FrmAccountGroup_Shown(object sender, EventArgs e)
    {
        BtnNew.Focus();
    }

    private void FrmAccountGroup_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        CmbPrimaryGroup.SelectedIndex = 1;
        ClearControl();
        EnableControl();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedAccountGroups);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmAccountGroup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("ACCOUNT GROUP") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtSchedule.Enabled = false;
        TxtShortName.Enabled = false;
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        _ = GetMasterList.GetAccountGroupList("VIEW");
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedAccountGroups);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (FormValidate())
        {
            if (SaveAccountGroupSetup().Result > 0)
            {
                if (_isZoom)
                {
                    GroupDesc = TxtDescription.Text;
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text, "ACCOUNT GROUP", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {

                CustomMessageBox.ActionError(TxtDescription.Text, "ACCOUNT GROUP", _actionTag);
                TxtDescription.Focus();
            }
        }
        else
        {
            CustomMessageBox.ActionError(TxtDescription.Text, "ACCOUNT GROUP", _actionTag);
            TxtDescription.Focus();
            return;
        }
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Close();
    }
    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetUpper();
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetProperCase();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("ACCOUNT GROUP DESCRIPTION IS BLANK..!!");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            if (TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }
    }


    private void BtnDescription_Click(object sender, EventArgs e)
    {
        (TxtDescription.Text, GroupId) = GetMasterList.GetAccountGroupList(_actionTag);
        if (!_actionTag.Equals("SAVE"))
        {
            SetGridDataToText();
            TxtDescription.ReadOnly = !_actionTag.Equals("UPDATE");
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            if (_actionTag.Equals("SAVE"))
            {
                var scheduleNo = TxtSchedule.ReturnMaxIntId("AG", "Schedule");
                TxtSchedule.Text = scheduleNo.ToString();
                TxtShortName.Text = TxtDescription.GenerateShortName("AccountGroup", "GrpCode");
            }
            var result = TxtDescription.IsDuplicate("GrpName", GroupId.ToString(), _actionTag, "AG");
            if (!result)
            {
                return;
            }
            TxtDescription.WarningMessage(@"ACCOUNT GROUP DESCRIPTION ALREADY EXITS..!!");
        }
        else if (TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits(_actionTag))
        {
            if (ActiveControl == BtnExit || ActiveControl == BtnDescription)
            {
                return;
            }
            if (ActiveControl != TxtDescription)
            {
                TxtDescription.WarningMessage(@"ACCOUNT GROUP DESCRIPTION IS BLANK..!!");
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits(_actionTag))
        {
            var result = TxtShortName.IsDuplicate("GrpCode", GroupId.ToString(), _actionTag, "AG");
            if (!result)
            {
                return;
            }
            TxtShortName.WarningMessage(@"ACCOUNT GROUP SHORT NAME ALREADY EXITS..!!");
            return;
        }
        if (!TxtShortName.IsBlankOrEmpty() || !_actionTag.IsValueExits())
        {
            return;
        }
        if (ActiveControl == TxtShortName)
        {
            return;
        }
        TxtShortName.WarningMessage(@"ACCOUNT GROUP SHORT NAME IS REQUIRED..!!");
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtShortName, "SHORT NAME IS BLANK..!!");
                return;
            }

            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void CmbCategory_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtSchedule_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
    }

    private void CmbPrimaryGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        _groupRepository.BindAccountGrpType(CmbGroupType, CmbPrimaryGroup.Text);
    }

    private void ChkSecondary_CheckStateChanged(object sender, EventArgs e)
    {
        BtnSecondary.Enabled = TxtSecondary.Enabled = ChkSecondary.Checked;
    }

    private void TxtSecondary_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSecondary_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N && _isZoom is false)
        {
            var (description, id) = GetMasterList.CreateAccountGroup(true);
            if (id > 0)
            {
                TxtSecondary.Text = description;
                _secondaryGrpId = id;
            }
            TxtSecondary.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSecondary, BtnSecondary);
        }
    }

    private void TxtSecondary_Leave(object sender, EventArgs e)
    {
    }

    private void BtnSecondary_Click(object sender, EventArgs e)
    {
        (TxtSecondary.Text, _secondaryGrpId) = GetMasterList.GetAccountGroupList(_actionTag);
        TxtSecondary.Focus();
    }

    private void TxtSecondary_Validated(object sender, EventArgs e)
    {
    }

    #endregion --------------- Form ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private bool FormValidate()
    {

        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"ACCOUNT GROUP DESCRIPTION NAME IS REQUIRED..!!");
            return false;
        }
        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"ACCOUNT GROUP SHORT NAME IS REQUIRED..!!");
            return false;
        }
        if (TxtSchedule.GetInt() == 0)
        {
            CustomMessageBox.Warning(@"ACCOUNT SCHEDULE IS REQUIRED..!!");
            return false;
        }
        if (CmbPrimaryGroup.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"PRIMARY GROUP IS REQUIRED..!!");
            return false;
        }
        if (CmbGroupType.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"GROUP TYPE IS REQUIRED..!!");
            return false;
        }
        if (_actionTag.Equals("DELETE") || _actionTag.Equals("UPDATE"))
        {
            if (CustomMessageBox.FormAction(_actionTag, "ACCOUNT GROUP") == DialogResult.Yes)
            {
                return true;
            }
        }
        if (GroupId is 0 && _actionTag != "SAVE")
        {
            TxtDescription.WarningMessage("SELECTED ACCOUNT GROUP IS INVALID");
            return false;
        }
        return true;
    }

    private void SetGridDataToText()
    {
        using var dt = _groupRepository.GetMasterAccountGroup(_actionTag, string.Empty, 1, GroupId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        GroupId = dt.Rows[0]["GrpId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["GrpName"].ToString();
        TxtShortName.Text = dt.Rows[0]["GrpCode"].ToString();
        TxtSchedule.Text = dt.Rows[0]["Schedule"].ToString();
        CmbPrimaryGroup.SelectedIndex = CmbPrimaryGroup.FindString(dt.Rows[0]["PrimaryGrp"].ToString());
        CmbGroupType.SelectedIndex = CmbGroupType.FindString(dt.Rows[0]["GrpType"].ToString());
        if (CmbGroupType.SelectedIndex == -1)
        {
            CmbPrimaryGroup_SelectedIndexChanged(null, EventArgs.Empty);
        }
        _secondaryGrpId = dt.Rows[0]["PrimaryGroupId"].GetInt();
        ChkSecondary.Checked = _secondaryGrpId > 0;
        if (_secondaryGrpId > 0)
        {
            TxtSecondary.Text = dt.Rows[0]["SecGroup"].ToString();
        }
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
        TxtNepaliName.Text = dt.Rows[0]["NepaliDesc"].ToString();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnDelete.Enabled = BtnEdit.Enabled = BtnNew.Enabled = !isEnable && !_actionTag.Equals("DELETE");

        TxtDescription.Enabled = BtnDescription.Enabled = isEnable || _actionTag.Equals("DELETE");

        TxtSecondary.Enabled = TxtSchedule.Enabled = TxtShortName.Enabled = isEnable;
        CmbGroupType.Enabled = CmbPrimaryGroup.Enabled = isEnable;

        ChkActive.Enabled = _actionTag.Equals("UPDATE");
        BtnSecondary.Enabled = TxtSecondary.Enabled = false;
        TxtNepaliName.Enabled = isEnable;
        ChkSecondary.Enabled = isEnable;

        BtnCancel.Enabled = BtnSave.Enabled = TxtDescription.Enabled;

        BtnSync.Enabled = isEnable && _actionTag != "DELETE";
        BtnSync.Visible = ObjGlobal.IsOnlineSync;
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"ACCOUNT GROUP SETUP [{_actionTag}] " : "ACCOUNT GROUP SETUP";
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtSecondary.Clear();
        ChkSecondary.Checked = false;
        CmbPrimaryGroup.SelectedIndex = CmbPrimaryGroup.SelectedIndex;
        CmbGroupType.SelectedIndex = CmbGroupType.SelectedIndex;
        var scheduleNo = TxtSchedule.ReturnMaxIntId("AG", "Schedule");
        TxtSchedule.Text = scheduleNo.ToString();
        TxtNepaliName.Clear();
        ChkActive.Checked = true;
    }

    private async Task<int> SaveAccountGroupSetup()
    {
        try
        {
            var sync = 0;
            GroupId = _actionTag is "SAVE" ? GroupId.ReturnMaxIntId("AG", string.Empty) : GroupId;
            _groupRepository.ObjAccountGroup.GrpId = GroupId;
            _groupRepository.ObjAccountGroup.GrpName = TxtDescription.Text.Replace("'", "''");
            _groupRepository.ObjAccountGroup.GrpCode = TxtShortName.Text.Replace("'", "''");
            _groupRepository.ObjAccountGroup.Schedule = TxtSchedule.GetInt();
            _groupRepository.ObjAccountGroup.PrimaryGrp = CmbPrimaryGroup.SelectedValue.ToString();
            _groupRepository.ObjAccountGroup.GrpType = CmbGroupType.SelectedValue.ToString();
            _groupRepository.ObjAccountGroup.Status = ChkActive.Checked;
            _groupRepository.ObjAccountGroup.Branch_ID = ObjGlobal.SysBranchId;
            _groupRepository.ObjAccountGroup.EnterDate = DateTime.Now;
            _groupRepository.ObjAccountGroup.EnterBy = ObjGlobal.LogInUser;
            _groupRepository.ObjAccountGroup.NepaliDesc = TxtNepaliName.Text.Replace("'", "''");
            _groupRepository.ObjAccountGroup.SyncRowVersion = sync.ReturnSyncRowNo("AG", GroupId);

            var result = _groupRepository.SaveAccountGroup(_actionTag);

            return result.GetInt();

        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    private async void GetAndSaveUnSynchronizedAccountGroups()
    {
        try
        {
            _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item2 == null)
            {
                return;
            }
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}AccountGroup/GetAccountGroupsByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}AccountGroup/InsertAccountGroupList",
                UpdateUrl = @$"{_configParams.Model.Item2}AccountGroup/UpdateAccountGroup"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var accountGroupRepo = DataSyncProviderFactory.GetRepository<AccountGroup>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            // pull all new account data
            var pullResponse = await _groupRepository.PullAccountGroupsServerToClientByRowCounts(accountGroupRepo, 1);
            if (!pullResponse)
            {
                SplashScreenManager.CloseForm();
            }
            else
            {
                SplashScreenManager.CloseForm();
            }
            // push all new account data
            var sqlAgQuery = _groupRepository.GetAccountGroupScript();
            var queryResponse = await QueryUtils.GetListAsync<AccountGroup>(sqlAgQuery);
            var agList = queryResponse.List.ToList();
            if (agList.Count <= 0)
            {
                return;
            }
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await accountGroupRepo.PushNewListAsync(agList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
            }
            else
            {
                SplashScreenManager.CloseForm();

            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global Class ---------------

    public int GroupId;
    public string GroupDesc = string.Empty;
    private int _secondaryGrpId;
    private readonly bool _isZoom;
    private string _actionTag = string.Empty;
    private readonly IAccountGroupRepository _groupRepository;
    private readonly IMasterSetup _setup;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
    #endregion --------------- Global Class ---------------
}