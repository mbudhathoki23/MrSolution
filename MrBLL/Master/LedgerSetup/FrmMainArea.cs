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
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.LedgerSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmMainArea : MrForm
{
    // MAIN AREA EVENTS

    #region --------------- MAIN AREA EVENTS ---------------

    public FrmMainArea(bool zoom)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = zoom;
        _control = new ClsMasterForm(this, BtnExit);
        _mainArea = new MainAreaRepository();

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    private void FrmMainArea_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedMainAreas);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedMainAreas);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private void FrmMainArea_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }
        if (!BtnNew.Enabled)
        {
            if (CustomMessageBox.ClearVoucherDetails("MAIN AREA") != DialogResult.Yes)
            {
                return;
            }
            _actionTag = string.Empty;
            ClearControl();
            EnableControl();
            BtnNew.Focus();
        }
        else
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
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
        else if (e.Control && e.KeyCode is Keys.L)
        {
            TxtDescription.Text = TxtDescription.GetProperCase();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("DESCRIPTION IS REQUIRED..!!");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else
        {
            if (_actionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits())
        {
            var result = TxtDescription.IsDuplicate("MAreaName", MainAreaId, _actionTag, "MainArea");
            if (result)
            {
                TxtDescription.WarningMessage("DESCRIPTION IS ALREADY EXITS..!!");
            }
            else if (_actionTag.Equals("SAVE"))
            {
                TxtShortName.Text = TxtDescription.GenerateShortName("MainArea", "MAreaCode");
            }
        }
        else if (TxtDescription.IsBlankOrEmpty() && TxtDescription.Enabled)
        {
            if (TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage("DESCRIPTION IS REQUIRED..!!");
                return;
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id, _) = GetMasterList.GetMainAreaList(_actionTag);
        if (id > 0)
        {
            TxtDescription.Text = description;
            MainAreaId = id;
            if (_actionTag != "SAVE")
            {
                SetGridDataToText();
                if (_actionTag.Equals("UPDATE"))
                {
                    TxtDescription.ReadOnly = false;
                }
            }
        }
        TxtDescription.Focus();
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits())
        {
            var result = TxtShortName.CheckValueExits(_actionTag, "MainArea", "MAreaCode", MainAreaId);
            if (result.Rows.Count > 0)
            {
                TxtShortName.WarningMessage("SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtShortName.IsBlankOrEmpty() && TxtShortName.Enabled)
        {
            if (TxtShortName.ValidControl(ActiveControl))
            {
                TxtShortName.WarningMessage("MAIN AREA SHORT NAME IS REQUIRED..!!");
                return;
            }
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim()) && TxtShortName.Focused is true &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"AREA SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
        Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
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

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsControlValid())
        {
            if (SaveMainAreaDetails() > 0)
            {
                if (_isZoom is true)
                {
                    MainAreaDesc = TxtDescription.Text;
                    CountryDesc = TxtCountry.Text;
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess($"{TxtDescription.Text}", "MAIN AREA", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
        }
        else
        {
            MessageBox.Show($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    #endregion --------------- MAIN AREA EVENTS ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        BtnDescription.Enabled = TxtDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtShortName.Enabled = TxtCountry.Enabled = isEnable;

        BtnSave.Enabled = BtnCancel.Enabled = TxtDescription.Enabled;
        ChkActive.Enabled = _actionTag! == "UPDATE";
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits()
            ? $@"MAIN AREA DETAILS SETUP [{_actionTag}]"
            : "MAIN AREA DETAILS SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtCountry.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.IsValueExits() && !_actionTag.Equals("SAVE");
    }

    private int SaveMainAreaDetails()
    {
        const short sync = 0;
        MainAreaId = _actionTag is "SAVE" ? MainAreaId.ReturnMaxIntId("MainArea") : MainAreaId;
        _mainArea.ObjMainArea.MAreaId = MainAreaId;
        _mainArea.ObjMainArea.MAreaName = TxtDescription.Text.Trim();
        _mainArea.ObjMainArea.NepaliDesc = TxtDescription.Text.ConvertToNepali();
        _mainArea.ObjMainArea.MAreaCode = TxtShortName.Text.Trim();
        _mainArea.ObjMainArea.Branch_ID = ObjGlobal.SysBranchId;
        _mainArea.ObjMainArea.EnterBy = ObjGlobal.LogInUser;
        _mainArea.ObjMainArea.EnterDate = DateTime.Now;
        _mainArea.ObjMainArea.MCountry = TxtCountry.Text.Trim();
        _mainArea.ObjMainArea.Status = ChkActive.Checked;
        _mainArea.ObjMainArea.SyncRowVersion = (short)(_actionTag is "UPDATE" ? sync.ReturnSyncRowNo("MainArea", MainAreaId) : 1);
        return _mainArea.SaveMainArea(_actionTag);
    }

    private bool IsControlValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"PLEASE ENTER VALID INFORMATION OF MAIN AREA...!!");
            return false;
        }
        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"MAIN AREA SHORT NAME IS REQUIRED...!!");
            return false;
        }
        if (_actionTag.Equals("DELETE") || _actionTag.Equals("UPDATE"))
        {
            if (CustomMessageBox.FormAction(_actionTag, "AREA GROUP") == DialogResult.Yes)
            {
                return true;
            }
            if (MainAreaId is 0 && _actionTag != "SAVE")
            {
                TxtDescription.WarningMessage("SELECTED MAIN AREA IS INVALID...!");
                return true;
            }
        }
        return true;
    }

    private void SetGridDataToText()
    {
        using var dt = _mainArea.GetMasterMainArea(_actionTag, false, MainAreaId);
        if (dt.Rows.Count <= 0)
        {
            return;
        }
        TxtDescription.Text = dt.Rows[0]["MAreaName"].ToString();
        TxtShortName.Text = dt.Rows[0]["MAreaCode"].ToString();
        TxtCountry.Text = dt.Rows[0]["MCountry"].IsBlankOrEmpty() ? "NEPAL" : dt.Rows[0]["MCountry"].GetString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    private async void GetAndSaveUnsynchronizedMainAreas()
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
            GetUrl = @$"{_configParams.Model.Item2}MainArea/GetMainAreasByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}MainArea/InsertMainAreaList",
            UpdateUrl = @$"{_configParams.Model.Item2}MainArea/UpdateMainArea",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var mainAreaRepo = DataSyncProviderFactory.GetRepository<MainArea>(_injectData);

        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new account data
        var pullResponse = await _mainArea.PullMainAreasServerToClientByRowCount(mainAreaRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlSaQuery = _mainArea.GetMainAreaScript();
        var queryResponse = await QueryUtils.GetListAsync<MainArea>(sqlSaQuery);
        var maList = queryResponse.List.ToList();
        if (maList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await mainAreaRepo.PushNewListAsync(maList);
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

    public int MainAreaId;
    private readonly bool _isZoom;

    public string CountryDesc = string.Empty;
    public string MainAreaDesc = string.Empty;

    private string _actionTag;
    private IMainAreaRepository _mainArea;
    private IMasterSetup _setup;
    private ClsMasterForm _control;

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

    #endregion --------------- Class ---------------


}