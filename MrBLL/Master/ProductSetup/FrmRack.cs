using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.InventorySetup;
using MrDAL.Master.InventorySetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmRack : MrForm
{
    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnDescription_Click(sender, e);
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"RACK SHORT-NAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits())
        {
            var result = TxtShortName.IsDuplicate("RCode", RackId, _actionTag, "RK");
            if (result)
            {
                TxtShortName.WarningMessage("RACK SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtShortName.IsBlankOrEmpty() && TxtShortName.Enabled)
        {
            if (TxtShortName.ValidControl(ActiveControl) && _actionTag.IsValueExits())
            {
                TxtShortName.WarningMessage("RACK SHORT NAME IS REQUIRED..!!");
            }
        }
    }

    #region --------------- GLOBAL VARIABLE ---------------

    public string Rack = string.Empty;
    public int RackId;
    private string _SearchKey;
    private string Query = string.Empty;
    private string _actionTag = string.Empty;
    private string Searchtext = string.Empty;
    private readonly bool _IsZoom;
    private readonly IRackRepository _rackRepository = new RackRepository();
    private IMasterSetup _setup;
    private ClsMasterForm clsFormControl;

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

    #endregion --------------- GLOBAL VARIABLE ---------------

    #region --------------- FrmRack ---------------

    public FrmRack(bool IsZoom)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _IsZoom = IsZoom;
        clsFormControl = new ClsMasterForm(this, BtnExit);

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmRack_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedRacks);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmRack_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    Text = "RACK SETUP";
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
    }

    #endregion --------------- FrmRack ---------------

    #region --------------- BUTTON  ---------------

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedRacks);
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
        EnableControl(true, false);
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true, false);
        BtnView.Enabled = true;
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl(false, false);
        BtnView.Enabled = true;
        TxtDescription.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (FormValid())
        {
            if (I_U_D_RACK() > 0)
            {
                if (_IsZoom)
                {
                    Rack = TxtDescription.Text.Trim().Replace("'", "''");
                    Close();
                    return;
                }

                MessageBox.Show($@"DATA {_actionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
                TxtDescription.Focus();
            }
        }
        else
        {
            MessageBox.Show($@"DATA {_actionTag.ToUpper()}  UNSUCCESSFULLY..!!", ObjGlobal.Caption,
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

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    #endregion --------------- BUTTON  ---------------

    #region --------------- METHOD ---------------

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"RACK DETAILS SETUP {_actionTag}" : "RACK DETAILS SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtLocation.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly =
            !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE" ? true : false;
    }

    protected void SetGridDataToText(int RackId)
    {
        using var dt = _rackRepository.GetRackList(_actionTag, 1, RackId);
        if (dt != null && dt.Rows.Count > 0)
        {
            int.TryParse(dt.Rows[0]["RID"].ToString(), out var _RackId);
            RackId = _RackId;
            TxtDescription.Text = dt.Rows[0]["RName"].ToString();
            TxtShortName.Text = dt.Rows[0]["RCode"].ToString();
            TxtLocation.Text = dt.Rows[0]["Location"].ToString();

            if (_actionTag is "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = BtnView.Enabled = Btn;
        TxtDescription.Enabled = BtnDescription.Enabled =
            !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" ? true : Txt;
        TxtShortName.Enabled = TxtLocation.Enabled = Txt;
        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "UPDATE" ? true : false;
        BtnSave.Enabled =
            BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" ? true : Txt;
    }

    private bool FormValid()
    {
        if (string.IsNullOrEmpty(_actionTag))
            return false;

        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            TxtDescription.WarningMessage(@"RACK NAME  IS REQUIRED...!");
            return false;

        }
        else if (string.IsNullOrEmpty(TxtShortName.Text))
        {
            TxtShortName.WarningMessage(@"RANK IS REQUIRED...!");
            return false;
        }

        if (RackId is 0 && _actionTag != "SAVE")
        {
            TxtDescription.WarningMessage(@"RANK IS REQUIRDE...!");
            return false;
        }

        return true;
    }

    private int I_U_D_RACK()
    {
        RackId = _actionTag is "SAVE"
            ? ObjGlobal.ReturnInt(ClsMasterSetup.ReturnMaxIntValue("AMS.RACK", "RID").ToString())
            : RackId;
        _rackRepository.ObjRack.RID = RackId;
        _rackRepository.ObjRack.RName = TxtDescription.Text.Trim().Replace("'", "''");
        _rackRepository.ObjRack.RCode = TxtShortName.Text.Trim().Replace("'", "''");
        _rackRepository.ObjRack.Location = TxtLocation.Text.Trim().Replace("'", "''");
        _rackRepository.ObjRack.Status = ChkActive.Checked;
        _rackRepository.ObjRack.EnterBy = ObjGlobal.LogInUser;
        _rackRepository.ObjRack.EnterDate = DateTime.Now;
        _rackRepository.ObjRack.BranchId = ObjGlobal.SysBranchId;
        _rackRepository.ObjRack.SyncRowVersion = (short)(_actionTag is "UPDATE"
            ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.RACK", "SyncRowVersion", "RID", RackId.ToString())
            : 1);
        return _rackRepository.SaveRack(_actionTag);
    }

    private async void GetAndSaveUnsynchronizedRacks()
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
            GetUrl = @$"{_configParams.Model.Item2}Rack/GetRacksByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Rack/InsertRackList",
            UpdateUrl = @$"{_configParams.Model.Item2}Rack/UpdateRack"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var rackRepo = DataSyncProviderFactory.GetRepository<RACK>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new account data
        var pullResponse = await _rackRepository.PullRacksServerToClientByRowCount(rackRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlQuery = _rackRepository.GetRackScript();
        var queryResponse = await QueryUtils.GetListAsync<RACK>(sqlQuery);
        var raList = queryResponse.List.ToList();
        if (raList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await rackRepo.PushNewListAsync(raList);
            SplashScreenManager.CloseForm();
        }
    }

    #endregion --------------- METHOD ---------------

    #region --------------- EVENTS ---------------

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
                TxtDescription.WarningMessage("RACK DESCRIPTION IS BLANK..!!");
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
        using var frmPickList =
            new FrmAutoPopList("MIN", "RACK", ObjGlobal.SearchText, _actionTag, string.Empty, "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtDescription.Text = frmPickList.SelectedList[0]["RName"].ToString().Trim();
                RackId = ObjGlobal.ReturnInt(frmPickList.SelectedList[0]["RID"].ToString().Trim());
                if (_actionTag != "SAVE")
                {
                    TxtDescription.ReadOnly = false;
                    SetGridDataToText(RackId);
                }
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"RACK NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDescription.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            TxtShortName.Text = TxtDescription.GenerateShortName("Rack", "RCode");
            var result = TxtDescription.IsDuplicate("RName", RackId, _actionTag, "RK");
            if (result)
            {
                TxtDescription.WarningMessage("RACK DESCRIPTION IS ALREADY EXITS..!!");
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
                TxtDescription.WarningMessage("RACK DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused is true && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    #endregion --------------- EVENTS ---------------


}