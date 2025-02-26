using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmGodownName : MrForm
{
    // GODOWN SETUP
    #region--------------- Frm ---------------

    public FrmGodownName(bool zoom)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _zoom = zoom;
        _godownRepository = new GodownRepository();
        _injectData = new DbSyncRepoInjectData();
        _setup = new ClsMasterSetup();
    }

    private void FrmGodownName_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedGodown);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmGodownName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("GODOWN") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }
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
        TxtDescription.ReadOnly = false;
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtDescription.ReadOnly = true;
        TxtDescription.Focus();
    }


    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtDescription.ReadOnly = true;
        TxtDescription.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!InformationValid())
        {
            TxtDescription.Focus();
            return;
        }

        if (SaveGodownDetails() > 0)
        {
            if (_zoom)
            {
                GodownMaster = TxtDescription.Text;
                Close();
                return;
            }
            CustomMessageBox.ActionSuccess(TxtDescription.GetUpper(), "GODOWN", _actionTag);
            ClearControl();
            TxtDescription.Focus();
        }
        else
        {
            CustomMessageBox.ErrorMessage($"ERROR OCCURS WHILE GODOWN {_actionTag}");
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim())) BtnExit.PerformClick();
        else ClearControl();
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetUpper();
        }
        else if (e.Control && e.KeyCode is Keys.L)
        {
            TxtDescription.Text = TxtDescription.GetProperCase();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("GODOWN DESCRIPTION IS REQUIRED..!!");
                return;
            }
        }
        else if (TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetGodown(_actionTag);
        if (result.id > 0)
        {
            if (_actionTag != "SAVE")
            {
                GodownId = result.id;
                TxtDescription.Text = result.description;
                SetGridDataToText();
            }
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            TxtShortName.Text = TxtDescription.GenerateShortName("Godown", "GCode");
            var result = TxtDescription.IsDuplicate("GName", GodownId, _actionTag, "GD");
            if (result)
            {
                TxtDescription.WarningMessage("GODOWN DESCRIPTION IS ALREADY EXITS..!!");
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
                TxtDescription.WarningMessage("GODOWN DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits(_actionTag))
        {
            var result = TxtShortName.IsDuplicate("GCode", GodownId.ToString(), _actionTag, "GD");
            if (!result)
            {
                return;
            }
            TxtShortName.WarningMessage(@"ACCOUNT GROUP SHORT NAME ALREADY EXITS..!!");
            return;
        }

        if (TxtShortName.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            if (ActiveControl != TxtShortName)
            {
                TxtShortName.WarningMessage(@"ACCOUNT GROUP SHORT NAME IS REQUIRED..!!");
                return;
            }
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtLocation_Leave(object sender, EventArgs e)
    {
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("SHORT NAME IS REQUIRED ");
                return;
            }
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedGodown);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    #endregion

    #region--------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        BtnDescription.Enabled = TxtDescription.Enabled;

        TxtShortName.Enabled = TxtLocation.Enabled = isEnable;

        ChkActive.Enabled = _actionTag.Equals("UPDATE");

        BtnSave.Enabled = isEnable || _actionTag.Equals("DELETE");
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"GODOWN DETAILS SETUP {_actionTag}" : "GODOWN DETAILS SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtLocation.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly =
            !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE" ? true : false;
    }

    private void SetGridDataToText()
    {
        var dt = _godownRepository.GetMasterGoDown(_actionTag, 1, GodownId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        GodownId = dt.Rows[0]["GID"].GetInt();
        TxtDescription.Text = dt.Rows[0]["GName"].ToString();
        TxtShortName.Text = dt.Rows[0]["GCode"].ToString();
        TxtLocation.Text = dt.Rows[0]["GLocation"].ToString();
    }

    private int SaveGodownDetails()
    {
        const short syncId = 0;
        GodownId = _actionTag is "SAVE" ? GodownId.ReturnMaxIntId("GD") : GodownId;
        _godownRepository.ObjGodown.GID = GodownId;
        _godownRepository.ObjGodown.GName = TxtDescription.Text.Trim().Replace("'", "''");
        _godownRepository.ObjGodown.GCode = TxtShortName.Text.Trim().Replace("'", "''");
        _godownRepository.ObjGodown.GLocation = TxtLocation.Text.Trim().Replace("'", "''");
        _godownRepository.ObjGodown.Status = ChkActive.Checked;
        _godownRepository.ObjGodown.EnterBy = ObjGlobal.LogInUser;
        _godownRepository.ObjGodown.EnterDate = DateTime.Now;
        _godownRepository.ObjGodown.BranchUnit = ObjGlobal.SysBranchId;
        _godownRepository.ObjGodown.SyncRowVersion = (short)(_actionTag is "UPDATE" ? syncId.ReturnSyncRowNo("GD", GodownId) : 1);
        return _godownRepository.SaveGodown(_actionTag);
    }

    private bool InformationValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (_actionTag.Equals("DELETE"))
        {
            if (TxtDescription.IsBlankOrEmpty() || GodownId is 0)
            {
                TxtDescription.WarningMessage("SELECTED GODOWN IS INVALID..!!");
                return false;
            }
            return true;
        }
        else
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("SELECTED GODOWN IS INVALID..!!");
                return false;
            }


            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("GODOWN SHORT NAME IS INVALID OR BLANK..!!");
                return false;
            }
        }
        return true;
    }

    private async void GetAndSaveUnSynchronizedGodown()
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
            GetUrl = @$"{_configParams.Model.Item2}Godown/GetGoDownsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Godown/InsertGodownList",
            UpdateUrl = @$"{_configParams.Model.Item2}Godown/UpdateGodown"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var goDownRepo = DataSyncProviderFactory.GetRepository<Godown>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new godown data
        var pullResponse = await _godownRepository.PullGoDownsServerToClientByRowCounts(goDownRepo, 1);
        SplashScreenManager.CloseForm();
        // push all new account data
        var sqlQuery = _godownRepository.GetGodownScript();
        var queryResponse = await QueryUtils.GetListAsync<Godown>(sqlQuery);
        var gdList = queryResponse.List.ToList();
        if (gdList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await goDownRepo.PushNewListAsync(gdList);
            SplashScreenManager.CloseForm();
        }
    }

    #endregion


    // OBJECT FOR THIS FORM
    #region--------------- Class ---------------

    public string GodownMaster = string.Empty;

    public int GodownId;
    public int ShortId = 0;
    private string _actionTag = string.Empty;
    private readonly bool _zoom;

    private readonly IGodownRepository _godownRepository;
    private IMasterSetup _setup;

    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;

    #endregion


}