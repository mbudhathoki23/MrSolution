using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx;
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
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmAccountSubGroup : MrForm
{
    // ACCOUNT SUB GROUP EVENTS

    #region --------------- Form ---------------
    public FrmAccountSubGroup(bool zoom = false, int groupId = 0)
    {
        InitializeComponent();
        _zoom = zoom;
        _setup = new ClsMasterSetup();
        _subGroupRepository = new AccountSubGroupRepository();
        _injectData = new DbSyncRepoInjectData();
        _groupId = groupId;
        TxtGroup.Text = _setup.GetAccountGroupDescription(_groupId);
    }

    private void FrmAccountSubGroup_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        TxtNepaliName.Font = new Font("Kantipur", 12F);
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedAccountSubGroups);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmAccountSubGroup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {

            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (CustomMessageBox.ClearVoucherDetails("ACCOUNT SUB GROUP") is DialogResult.Yes)
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
        else if (e.KeyChar is (char)Keys.Enter)
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
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtDescription.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetAccountSubGroupList("VIEW", 0);
        BtnView.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!FormValidate())
        {
            return;
        }
        if (SaveAccountSubgroup() > 0)
        {
            if (_zoom)
            {
                AccountSubGrpDesc = TxtDescription.Text.Trim();
                Close();
                return;
            }
            CustomMessageBox.ActionSuccess(TxtDescription.Text, "ACCOUNT SUB GROUP", _actionTag);
            ClearControl();
            TxtDescription.Focus();
        }
        else
        {
            CustomMessageBox.ErrorMessage($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!");
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty())
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
        }
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
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
                TxtDescription.WarningMessage("ACCOUNT SUB GROUP DESCRIPTION IS BLANK..!!");
                return;
            }
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
        var (description, id) = GetMasterList.GetAccountSubGroupList(_actionTag, 0);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            SubGroupId = id;
            if (!_actionTag.Equals("SAVE"))
            {
                FillTextFromGrid(SubGroupId);
                TxtDescription.ReadOnly = false;
            }
        }
        if (_actionTag.Equals("DELETE"))
        {
            BtnSave.Focus();
        }
        else
        {
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            TxtShortName.Text = TxtDescription.GenerateShortName("AccountSubGroup", "SubGrpCode");
            var result = TxtDescription.IsDuplicate("SubGrpName", SubGroupId, _actionTag, "ASG");
            if (result)
            {
                TxtDescription.WarningMessage("ACCOUNT SUB GROUP DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtDescription.IsBlankOrEmpty() && TxtDescription.Enabled)
        {
            if (ActiveControl == BtnDescription)
            {
                return;
            }
            if (TxtDescription.ValidControl(ActiveControl) && _actionTag.IsValueExits())
            {
                TxtDescription.WarningMessage("ACCOUNT SUB GROUP DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits())
        {
            var result = TxtShortName.IsDuplicate("SubGrpCode", SubGroupId, _actionTag, "ASG");
            if (result)
            {
                TxtShortName.WarningMessage("ACCOUNT SUB GROUP SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtShortName.IsBlankOrEmpty() && TxtShortName.Enabled)
        {
            if (TxtShortName.ValidControl(ActiveControl) && _actionTag.IsValueExits())
            {
                TxtShortName.WarningMessage("ACCOUNT SUB GROUP SHORT NAME IS REQUIRED..!!");
            }
        }
    }

    private void TxtGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateAccountGroup(true);
            if (description.IsValueExits())
            {
                TxtGroup.Text = description;
                _groupId = id;
            }
            TxtGroup.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnAccountGroup_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtGroup.IsBlankOrEmpty())
            {
                TxtGroup.WarningMessage("ACCOUNT GROUP IS REQUIRED ..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGroup, BtnGroup);
        }
    }

    private void BtnAccountGroup_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAccountGroupList("UPDATE");
        if (description.IsValueExits())
        {
            TxtGroup.Text = description;
            _groupId = id;
        }
        TxtGroup.Focus();
    }

    private void TxtGroup_Validating(object sender, CancelEventArgs e)
    {
        if (_groupId == 0 && TxtGroup.Enabled)
        {
            if (TxtGroup.ValidControl(ActiveControl) && _actionTag.IsValueExits())
            {
                TxtGroup.WarningMessage("ACCOUNT GROUP IS REQUIRED ..!!");
            }
        }
    }

    private void BtnSecondary_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAccountSubGroupList(_actionTag, 0);
        if (description.IsValueExits())
        {
            TxtSecondary.Text = description;
            _secondaryGroupId = id;
        }
        TxtSecondary.Focus();
    }

    private void ChkSecondary_CheckedChanged(object sender, EventArgs e)
    {
        BtnSecondary.Enabled = TxtSecondary.Enabled = ChkSecondary.Checked;
    }

    private void TxtSecondary_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSecondary_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N && _zoom)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSecondary, BtnSecondary);
        }
    }

    private void ChkSecondary_CheckStateChanged(object sender, EventArgs e)
    {
        BtnSecondary.Enabled = TxtSecondary.Enabled = ChkSecondary.Checked;
    }


    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void Global_keypress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }

    }


    private void TxtGroup_keypress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }

    }



    private void TxtSecondary_keypress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
    private void TxtNepaliName_keypress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }

    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedAccountSubGroups);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- Form ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_actionTag.Equals("DELETE");
        TxtDescription.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
        BtnDescription.Enabled = TxtDescription.Enabled;
        //TxtDescription.Enabled = BtnDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtShortName.Enabled = isEnable;
        TxtGroup.Enabled = BtnGroup.Enabled = isEnable;
        ChkSecondary.Enabled = isEnable;
        TxtSecondary.Enabled = BtnSecondary.Enabled = ChkSecondary.Checked;
        TxtNepaliName.Enabled = isEnable;
        BtnCancel.Enabled = BtnSave.Enabled = isEnable || _actionTag.Equals("DELETE");
        BtnSync.Visible = ObjGlobal.IsOnlineSync;

        ChkActive.Enabled = _actionTag is "UPDATE";
        BtnSave.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $"ACCOUNT SUBGROUP SETUP [{_actionTag}]" : "ACCOUNT SUBGROUP SETUP";
        SubGroupId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtSecondary.Clear();
        ChkSecondary.Checked = false;
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
    }

    private int SaveAccountSubgroup()
    {
        var sync = 0;
        _subGroupRepository.ObjAccountSubGroup.SubGrpId = _actionTag is "SAVE" ? SubGroupId.ReturnMaxIntId("ASG") : SubGroupId;
        _subGroupRepository.ObjAccountSubGroup.SubGrpName = TxtDescription.Text.Replace("'", "''");
        _subGroupRepository.ObjAccountSubGroup.GrpId = _groupId;
        _subGroupRepository.ObjAccountSubGroup.SubGrpCode = TxtShortName.Text.Replace("'", "''");
        _subGroupRepository.ObjAccountSubGroup.Status = ChkActive.Checked;
        _subGroupRepository.ObjAccountSubGroup.PrimaryGroupId = _secondaryGroupId;
        _subGroupRepository.ObjAccountSubGroup.NepaliDesc = TxtNepaliName.Text.Replace("'", "''");
        _subGroupRepository.ObjAccountSubGroup.Branch_ID = ObjGlobal.SysBranchId;
        _subGroupRepository.ObjAccountSubGroup.IsDefault = 0;
        _subGroupRepository.ObjAccountSubGroup.EnterBy = ObjGlobal.LogInUser;
        _subGroupRepository.ObjAccountSubGroup.EnterDate = DateTime.Now;
        _subGroupRepository.ObjAccountSubGroup.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : null;
        sync = _subGroupRepository.ObjAccountSubGroup.SyncRowVersion.ReturnSyncRowNo("AG", SubGroupId);
        _subGroupRepository.ObjAccountSubGroup.SyncRowVersion = (short)sync;
        return _subGroupRepository.SaveAccountSubGroup(_actionTag);
    }

    private bool FormValidate()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"ACCOUNT SUBGROUP DESCRIPTION IS BLANK..!!");
            return false;
        }
        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"ACCOUNT SUB GROUP SHORT NAME IS BLANK..!!");
            return false;
        }

        if (_actionTag.Equals("DELETE") || _actionTag.Equals("UPDATE"))
        {
            if (CustomMessageBox.FormAction(_actionTag, "ACCOUNT GROUP") == DialogResult.Yes)
            {
                return true;
            }
        }

        if (SubGroupId is 0 && _actionTag != "SAVE")
        {
            TxtDescription.WarningMessage("SELECTED ACCOUNT GROUP IS INVALID");
            return true;
        }
        if (TxtGroup.IsBlankOrEmpty())
        {
            TxtGroup.WarningMessage(@"ACCOUNT GROUP IS BLANK..!!");
            return false;
        }
        return true;
    }

    private void FillTextFromGrid(int subGrpId)
    {
        var dt = _subGroupRepository.GetMasterAccountSubGroup(_actionTag, string.Empty, 1, SubGroupId);
        if (dt == null || dt.Rows.Count <= 0) return;
        SubGroupId = ObjGlobal.ReturnInt(dt.Rows[0]["SubGrpId"].ToString());
        TxtDescription.Text = dt.Rows[0]["SubGrpName"].ToString();
        _groupId = ObjGlobal.ReturnInt(dt.Rows[0]["GrpID"].ToString());
        TxtGroup.Text = dt.Rows[0]["GrpName"].ToString();
        TxtShortName.Text = dt.Rows[0]["SubGrpCode"].ToString();
        _secondaryGroupId = ObjGlobal.ReturnInt(dt.Rows[0]["PrimaryGroupId"].ToString());
        ChkSecondary.Checked = _secondaryGroupId > 0 ? true : false;

        if (_secondaryGroupId > 0) TxtSecondary.Text = dt.Rows[0]["Sub_GrpName"].ToString();

        ChkActive.Checked = ObjGlobal.ReturnBool(dt.Rows[0]["Status"].ToString());
        TxtNepaliName.Text = dt.Rows[0]["NepaliDesc"].ToString();
    }

    private async void GetAndSaveUnSynchronizedAccountSubGroups()
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
                GetUrl = @$"{_configParams.Model.Item2}AccountSubGroup/GetAccountSubGroupsByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}AccountSubGroup/InsertAccountSubGroupList",
                UpdateUrl = @$"{_configParams.Model.Item2}AccountSubGroup/UpdateAccountSubGroup"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var accountSubGroupRepo = DataSyncProviderFactory.GetRepository<AccountSubGroup>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            // pull all new account data from server database
            var pullResponse = await _subGroupRepository.PullAccountSubGroupsServerToClientByRowCount(accountSubGroupRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new account data of client database to server database
            var sqlAsgQuery = _subGroupRepository.GetAccountSubGroupScript();
            var queryResponse = await QueryUtils.GetListAsync<AccountSubGroup>(sqlAsgQuery);
            var asgList = queryResponse.List.ToList();
            if (asgList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await accountSubGroupRepo.PushNewListAsync(asgList);
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

    #region ---------------  Global Class ---------------

    public int SubGroupId;
    private int _groupId;
    private int _secondaryGroupId;
    private readonly bool _zoom;

    public string AccountSubGrpDesc = string.Empty;
    private string _actionTag = string.Empty;

    private readonly IAccountSubGroupRepository _subGroupRepository;
    private IMasterSetup _setup;
    private ClsMasterForm _getForm;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;



    #endregion ---------------  Global Class ---------------

    private void ChkActive_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
}