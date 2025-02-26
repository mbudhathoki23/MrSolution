using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
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
using MrDAL.Master.Interface.InventorySetup;
using MrDAL.Master.InventorySetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmCostCentre : MrForm
{
    #region --------------- Frm ---------------

    public FrmCostCentre(bool isZoom)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _control = new ClsMasterForm(this, BtnExit);
        ClearControl();
        EnableControl();
        _costCenterRepository = new CostCenterRepository();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmCostCentre_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedCostCenters);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmCostCentre_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("COST CENTER") == DialogResult.Yes)
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
        _ = GetMasterList.GetCostCenterList("VIEW");
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.Question("DO YOU WANT TO EXIT THIS FORM..??");
        if (result == DialogResult.Yes)
        {
            Close();
            return;
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidControl())
        {
            if (SaveCostCentreDetails() > 0)
            {
                if (_isZoom)
                {
                    CostCenterDesc = TxtDescription.Text;
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "COST CENTER", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                CustomMessageBox.ActionError(TxtDescription.Text.GetUpper(), "COST CENTER", _actionTag);
                TxtDescription.Focus();
                return;
            }
        }
        else
        {
            CustomMessageBox.ActionError(TxtDescription.Text.GetUpper(), "COST CENTER", _actionTag);
            TxtDescription.Focus();
            return;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text)) Close();
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
                CustomMessageBox.Warning(@"DESCRIPTION IS BLANK..!!");
                TxtDescription.Focus();
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        (TxtDescription.Text, CCId) = GetMasterList.GetCostCenterList(_actionTag);
        if (_actionTag != "SAVE")
        {
            TxtDescription.ReadOnly = false;
            SetGridDataToText();
        }

        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtShortName.Text = TxtDescription.GenerateShortName("CostCenter", "CCcode");
            }

            var result = TxtDescription.IsDuplicate("CCName", CCId, _actionTag, "CostCenter");
            if (result)
            {
                TxtDescription.WarningMessage("COST CENTER DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            if (TxtDescription.ValidControl(ActiveControl))
            {
                if (ActiveControl == BtnExit || ActiveControl == BtnDescription)
                {
                    return;
                }
                TxtDescription.WarningMessage("COST CENTER DESCRIPTION IS REQUIRED..!!");
                return;
            }
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits())
        {
            var result = TxtDescription.IsDuplicate("CCcode", CCId, _actionTag, "CostCenter");
            if (result)
            {
                TxtDescription.WarningMessage("COST CENTER SHORT NAME IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtShortName.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            if (TxtShortName.ValidControl(ActiveControl))
            {
                if (ActiveControl == BtnExit || ActiveControl == BtnDescription)
                {
                    return;
                }
                TxtShortName.WarningMessage("COST CENTER SHORTNAME IS REQUIRED..!!");
                return;
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

    private void TxtGodown_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnGodown_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGodown(true);
            if (id > 0)
            {
                TxtGodown.Text = description;
                _godownId = id;
            }
            TxtGodown.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            //Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGodown, BtnGodown);
        }
    }

    private void BtnGodown_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGodown(_actionTag);
        {
            TxtGodown.Text = description;
            _godownId = id;
        }
        TxtGodown.Focus();
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedCostCenters);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- Frm ---------------

    // METHOD FOR THIS FORM
    #region--------------- Method ---------------

    private bool IsValidControl()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (_actionTag.Equals("DELETE"))
        {
            if (TxtDescription.IsBlankOrEmpty() || CCId is 0)
            {
                TxtDescription.WarningMessage("SELECTED COST CENTER IS INVALID..!!");
                return false;
            }
        }
        else
        {
            if (_actionTag.Equals("UPDATE"))
            {
                if (TxtDescription.IsBlankOrEmpty() || CCId is 0)
                {
                    TxtDescription.WarningMessage("SELECTED COST CENTER IS INVALID..!!");
                    return false;
                }
            }
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("PLEASE ENTER DESCRIPTION OF COST CENTER..!!");
                return false;
            }
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("PLEASE ENTER SHORT NAME OF COST CENTER..!!");
                return false;
            }
            if (TxtGodown.IsBlankOrEmpty() || _godownId is 0)
            {
                TxtGodown.WarningMessage("GODOWN IS REQUIRED FOR COST CENTER..!!");
                return false;
            }
        }
        return true;
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnDelete.Enabled = BtnEdit.Enabled = BtnNew.Enabled = !isEnable;
        BtnDescription.Enabled = TxtDescription.Enabled = BtnSave.Enabled = _actionTag == "DELETE" || isEnable;

        TxtShortName.Enabled = isEnable;
        TxtGodown.Enabled = BtnGodown.Enabled = isEnable;

        BtnCancel.Enabled = BtnSave.Enabled = _actionTag == "DELETE" || isEnable;
        ChkActive.Enabled = _actionTag is "UPDATE";
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits()
            ? $@"COST CENTER DETAILS SETUP [{_actionTag}]"
            : "COST CENTER DETAILS SETUP";
        ChkActive.Checked = true;
        CCId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        _godownId = 0;
        TxtGodown.Clear();
        TxtDescription.ReadOnly = !_actionTag.Equals("SAVE") && TxtDescription.Enabled;
    }

    private void SetGridDataToText()
    {
        using var dt = _costCenterRepository.GetMasterCostCenter(_actionTag, 1, CCId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        foreach (DataRow row in dt.Rows)
        {
            CCId = row["CCId"].GetInt();
            TxtDescription.Text = row["CCName"].GetString();
            TxtShortName.Text = row["CCcode"].ToString();
            _godownId = row["GodownId"].GetInt();
            TxtGodown.Text = row["GName"].ToString();
            ChkActive.Checked = row["Status"].GetBool();
        }
    }

    private int SaveCostCentreDetails()
    {
        CCId = _actionTag is "SAVE"
            ? CCId.ReturnMaxIntId("COSTCENTER", string.Empty)
            : CCId;
        _costCenterRepository.ObjCostCenter.CCId = CCId;
        _costCenterRepository.ObjCostCenter.CCName = TxtDescription.Text.Trim().Replace("'", "''");
        _costCenterRepository.ObjCostCenter.CCcode = TxtShortName.Text.Trim().Replace("'", "''");
        _costCenterRepository.ObjCostCenter.GodownId = _godownId;
        _costCenterRepository.ObjCostCenter.Branch_ID = ObjGlobal.SysBranchId;
        _costCenterRepository.ObjCostCenter.Company_Id = ObjGlobal.SysCompanyUnitId;
        _costCenterRepository.ObjCostCenter.Status = ChkActive.Checked;
        _costCenterRepository.ObjCostCenter.SyncRowVersion =
            _costCenterRepository.ObjCostCenter.SyncRowVersion.ReturnSyncRowNo("COSTCENTER", CCId.ToString());
        return _costCenterRepository.SaveCostCenter(_actionTag);
    }

    private async void GetAndSaveUnSynchronizedCostCenters()
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
                GetUrl = @$"{_configParams.Model.Item2}CostCenter/GetCostCentersByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}CostCenter/InsertCostCenterList",
                UpdateUrl = @$"{_configParams.Model.Item2}CostCenter/UpdateCostCenter"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var costCentreRepo = DataSyncProviderFactory.GetRepository<CostCenter>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));

            // pull all new Cost Centre data
            var pullResponse = await _costCenterRepository.PullCostCentreServerToClientByRowCounts(costCentreRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new Cost Centre data

            var sqlCCQuery = _costCenterRepository.GetCostCentreScript();
            var queryResponse = await QueryUtils.GetListAsync<CostCenter>(sqlCCQuery);
            var costCenters = queryResponse.List.ToList();
            if (costCenters.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await costCentreRepo.PushNewListAsync(costCenters);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    #endregion

    // OBJECT FOR THIS FORM
    #region -------------- Class ---------------

    public int CCId;
    private int _godownId;
    private bool _isZoom;

    public string CostCenterDesc = string.Empty;
    private string _actionTag;
    private string _query = string.Empty;
    private string _searchKey = string.Empty;

    private readonly ICostCenterRepository _costCenterRepository;
    private IMasterSetup _setup;
    private ClsMasterForm _control;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
    #endregion
}