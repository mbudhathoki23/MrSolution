using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
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
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmProductSubGroup : MrForm
{
    #region --------------- FORM ---------------
    public FrmProductSubGroup(bool zoom = false, int groupId = 0)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _repository = new ProductSubGroupRepository();
        _setup = new ClsMasterSetup();
        _isZoom = zoom;
        _pGrpId = groupId;
        Shown += FrmProductSubGroup_Shown;
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmProductSubGroup_Shown(object sender, EventArgs e)
    {
        if (_isZoom && _pGrpId > 0)
        {
            BtnNew.PerformClick();
        }
        else
        {
            ClearControl();
            EnableControl();
            BtnNew.Focus();
        }
    }

    private void FrmProductSubGroup_Load(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProductSubGroups);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmProductSubGroup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (BtnNew.Enabled == false || BtnEdit.Enabled == false || BtnDelete.Enabled == false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO EXIT THE FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
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
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProductSubGroups);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        if (_isZoom && _pGrpId > 0)
        {
            TxtProductGroup.Text = _setup.GetProductGroupDescription(_pGrpId);
        }
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

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValidControl())
        {
            if (SaveProductSubGroupDetails() > 0)
            {
                if (_isZoom)
                {
                    ProductSubGroupName = TxtDescription.Text.Trim();
                    PSubGrpId = ObjGlobal.ReturnInt(GetConnection.GetQueryData(
                        "SELECT psg.PSubGrpId FROM AMS.ProductSubGroup psg WHERE psg.SubGrpName = '" +
                        TxtDescription.Text.Trim() + "' "));
                    Close();
                    return;
                }

                MessageBox.Show($@"DATA {_actionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                MessageBox.Show($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!", ObjGlobal.Caption,
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                TxtDescription.Focus();
            }
        }
        else
        {
            MessageBox.Show($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            ClearControl();
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim())) BtnExit.PerformClick();
        else ClearControl();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    private void TxtProductGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            var result = GetMasterList.CreateProductGroup(true);
            if (result.id > 0)
            {
                TxtProductGroup.Text = result.description;
                _pGrpId = result.id;
            }

            TxtProductGroup.Focus();
        }
        else if (e.KeyCode is Keys.Delete or Keys.Back)
        {
            _pGrpId = 0;
            TxtProductGroup.Clear();
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnProductGroup_Click(sender, e);
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtProductGroup.IsBlankOrEmpty())
            {
                TxtProductGroup.WarningMessage("PRODUCT GROUP IS REQUIRED FOR SUB GROUP");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtProductGroup, BtnProductGroup);
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
                TxtDescription.WarningMessage("PRODUCT SUB GROUP DESCRIPTION IS BLANK..!!");
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
        var (subGroup, id) = GetMasterList.GetProductSubGroup(_actionTag, 0);
        if (subGroup.IsValueExits())
        {
            TxtDescription.Text = subGroup;
            PSubGrpId = id;
            if (_actionTag != "SAVE")
            {
                TxtDescription.ReadOnly = false;
                SetGridDataToText();
            }
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.Equals("SAVE"))
        {
            TxtShortName.Text = TxtShortName.IsBlankOrEmpty()
                ? TxtShortName.GenerateShortName("PRODUCTSUBGROUP", TxtDescription.Text, "ShortName")
                : TxtShortName.Text.Replace("'", "''");
        }
        if (TxtDescription.IsValueExits() && _actionTag != "DELETE")
        {
            var result = TxtDescription.CheckValueExits(_actionTag, "PRODUCTSUBGROUP", "SubGrpName", PSubGrpId);
            if (result.Rows.Count > 0)
            {
                TxtDescription.WarningMessage("PRODUCT SUB GROUP DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
    }
    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag != "DELETE")
        {
            var result = TxtShortName.CheckValueExits(_actionTag, "PRODUCTSUBGROUP", "ShortName", PSubGrpId);
            if (result.Rows.Count > 0)
            {
                TxtShortName.WarningMessage("PRODUCT SUB GROUP SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void TxtProductGroup_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnProductGroup_Click(object sender, EventArgs e)
    {
        var (group, id, margin) = GetMasterList.GetProductGroup(_actionTag);
        if (group.IsValueExits())
        {
            TxtProductGroup.Text = group;
            _pGrpId = id;
        }
        TxtProductGroup.Focus();
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("PRODUCT SUB GROUP SHORT NAME IS REQUIRED ");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    #endregion --------------- Event --------------


    // METHOD FOR THIS FORM
    #region --------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtDescription.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
        BtnDescription.Enabled = TxtDescription.Enabled;
        TxtShortName.Enabled = isEnable;
        TxtProductGroup.Enabled = BtnProductGroup.Enabled = isEnable;
        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "UPDATE";
        BtnSave.Enabled = BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $@"PRODUCT SUBGROUP DETAILS SETUP [{_actionTag}]" : @"PRODUCT SUBGROUP DETAILS SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtProductGroup.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE";
        TxtProductGroup.ReadOnly = true;
        TxtDescription.Focus();
    }

    private void SetGridDataToText()
    {
        var dt = _repository.GetMasterProductSubGroup(_actionTag, string.Empty, true, PSubGrpId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        PSubGrpId = dt.Rows[0]["PSubGrpId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["SubGrpName"].ToString();
        TxtShortName.Text = dt.Rows[0]["ShortName"].ToString();
        _pGrpId = dt.Rows[0]["GrpId"].GetInt();
        TxtProductGroup.Text = dt.Rows[0]["GrpName"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    private int SaveProductSubGroupDetails()
    {
        PSubGrpId = _actionTag == "SAVE"
            ? PSubGrpId.ReturnMaxIntId("PRODUCTSUBGROUP", string.Empty)
            : PSubGrpId;
        _repository.ObjProductSubGroup.PSubGrpId = PSubGrpId;
        _repository.ObjProductSubGroup.SubGrpName = TxtDescription.Text.Trim().Replace("'", "''");
        _repository.ObjProductSubGroup.NepaliDesc = TxtDescription.Text.Trim().Replace("'", "''");
        _repository.ObjProductSubGroup.ShortName = TxtShortName.Text.Trim().Replace("'", "''");
        _repository.ObjProductSubGroup.GrpId = _pGrpId == 0 ? 1 : _pGrpId;
        _repository.ObjProductSubGroup.Branch_ID = ObjGlobal.SysBranchId;
        _repository.ObjProductSubGroup.EnterBy = ObjGlobal.LogInUser;
        _repository.ObjProductSubGroup.EnterDate = DateTime.Now;
        _repository.ObjProductSubGroup.Status = ChkActive.Checked;
        _repository.ObjProductSubGroup.SyncRowVersion = _repository.ObjProductSubGroup.SyncRowVersion.ReturnSyncRowNo("PRODUCTSUBGROUP", PSubGrpId.ToString());
        return _repository.SaveProductSubGroup(_actionTag);
    }

    private bool IsValidControl()
    {
        if (_actionTag.IsBlankOrEmpty())

        {
            return false;
        }
        if (string.IsNullOrEmpty(_actionTag))
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"DESCRIPTION IS REQUIRED...!");
            return false;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text.Trim()))
        {
            TxtShortName.WarningMessage(@"SHORTNAME IS REQUIRED...!");
            return false;
        }

        return true;
    }

    private async void GetAndSaveUnSynchronizedProductSubGroups()
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
            GetUrl = @$"{_configParams.Model.Item2}ProductSubGroup/GetProductSubGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductSubGroup/InsertProductSubGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductSubGroup/UpdateProductSubGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productSubGroupRepo = DataSyncProviderFactory.GetRepository<ProductSubGroup>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new account data
        var pullResponse = await _repository.PullProductSubGroupsServerToClientByRowCount(productSubGroupRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlQuery = _repository.GetProductSubGroupScript();
        var queryResponse = await QueryUtils.GetListAsync<ProductSubGroup>(sqlQuery);
        var psgList = queryResponse.List.ToList();
        if (psgList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await productSubGroupRepo.PushNewListAsync(psgList);
            SplashScreenManager.CloseForm();
        }
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    #endregion --------------- Method ---------------


    // OBJECT FOR THIS FORM
    #region --------------- Class ---------------
    public string ProductSubGroupName = string.Empty;
    private int _pGrpId;
    public int PSubGrpId;

    private string _actionTag;
    private readonly bool _isZoom;

    private readonly IProductSubGroupRepository _repository;
    private readonly IMasterSetup _setup;

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

    #endregion --------------- Class ---------------


}