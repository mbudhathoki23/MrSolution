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
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmSubArea : MrForm
{
    #region --------------- AREA SETUP EVENTS ---------------

    public FrmSubArea(bool zoom = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = zoom;
        _control = new ClsMasterForm(this, BtnExit);
        _areaRepository = new AreaRepository();

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmArea_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedAreas);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmArea_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }
        if (!BtnNew.Enabled)
        {
            if (CustomMessageBox.ClearVoucherDetails("AREA SETUP") != DialogResult.Yes)
            {
                return;
            }
            _actionTag = string.Empty;
            EnableControl();
            ClearControl();
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

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableControl(true);
        ClearControl();
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
        EnableControl();
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetAreaList("VIEW");
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsControlValid())
        {
            if (SaveAreaDetails() > 0)
            {
                if (_isZoom)
                {
                    AreaName = TxtDescription.Text;
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text, "AREA SETUP", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                TxtDescription.WarningMessage($"ERROR OCCURS WHILE INFORMATION ARE [{_actionTag}]");
                return;
            }
        }
        else
        {
            TxtDescription.ErrorMessage($"ERROR OCCURS WHILE INFORMATION ARE [{_actionTag}]");
            return;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            BtnExit.PerformClick();
        }
        else ClearControl();
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
                TxtDescription.WarningMessage("AREA DESCRIPTION IS BLANK..!!");
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

    private void TxtCountry_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtCountry.IsBlankOrEmpty())
            {
                TxtCountry.WarningMessage("COUNTRY IS BLANK..!!");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAreaList(_actionTag);
        if (id > 0)
        {
            TxtDescription.Text = description;
            AreaId = id;
            SetGridDataToText();
            if (_actionTag.Equals("UPDATE"))
            {
                TxtDescription.ReadOnly = false;
            }
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits())
        {
            var result = TxtDescription.IsDuplicate("AreaName", AreaId, _actionTag, "Area");
            if (result)
            {
                TxtDescription.WarningMessage("DESCRIPTION IS ALREADY EXITS..!!");
            }
            else if (_actionTag.Equals("SAVE"))
            {
                TxtShortName.Text = TxtDescription.GenerateShortName("Area", "AreaCode");
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

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits())
        {
            var result = TxtShortName.CheckValueExits(_actionTag, "Area", "AreaCode", AreaId);
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
                TxtShortName.WarningMessage("AREA SHORT NAME IS REQUIRED..!!");
                return;
            }
        }
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void TxtMainArea_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id, country) = GetMasterList.CreateMainArea(true);
            if (id > 0)
            {
                _mAreaId = id;
                TxtMainArea.Text = description;
                TxtCountry.Text = country;
            }
            TxtMainArea.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnMainArea_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtMainArea, BtnMainArea);
        }
    }

    private void TxtMainArea_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnMainArea_Click(object sender, EventArgs e)
    {
        var (description, id, country) = GetMasterList.GetMainAreaList("SAVE");
        if (id > 0)
        {
            _mAreaId = id;
            TxtMainArea.Text = description;
            TxtCountry.Text = country;
        }
        TxtMainArea.Focus();
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("AREA SHORT NAME IS REQUIRED ");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    #endregion --------------- AREA SETUP EVENTS ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedAreas);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private void SetGridDataToText()
    {
        using var dt = _areaRepository.GetMasterArea(_actionTag.ToUpper(), AreaId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }

        foreach (DataRow row in dt.Rows)
        {
            TxtDescription.Text = row["AreaName"].GetString();
            TxtShortName.Text = row["AreaCode"].GetString();
            _mAreaId = row["Main_Area"].GetInt();
            TxtMainArea.Text = row["MAreaName"].GetString();
            TxtCountry.Text = row["Country"].GetString();
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtDescription.Enabled = BtnDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtShortName.Enabled = isEnable;
        TxtCountry.Enabled = isEnable;
        TxtMainArea.Enabled = isEnable;
        ChkActive.Enabled = _actionTag is "UPDATE";
        BtnSave.Enabled = BtnCancel.Enabled = TxtDescription.Enabled;
    }

    private bool IsControlValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage("AREA DESCRIPTION IS REQUIRED..!");
            return false;
        }

        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage("AREA DESCRIPTION IS REQUIRED..!!");
            return false;
        }

        if (_actionTag.Equals("DELETE") || _actionTag.Equals("UPDATE"))
        {
            if (CustomMessageBox.FormAction(_actionTag, "AREA GROUP") == DialogResult.Yes)
            {
                return true;
            }
        }

        if (AreaId is 0 && _actionTag != "SAVE")
        {
            TxtDescription.WarningMessage("SELECTED AREA IS INVALID");
            return true;
        }
        else
        {
            //    //if (_actionTag.Equals("UPDATE"))
            //{
            //    if (AreaId is 0 || TxtDescription.IsBlankOrEmpty())
            //    {
            //        TxtDescription.WarningMessage("SELECTED AREA IS INVALID..!!");
            //        return false;
            //    }
            //}
            //if (TxtDescription.IsBlankOrEmpty())
            //{
            //    TxtDescription.WarningMessage("AREA DESCRIPTION IS REQUIRED..!!");
            //    return false;
            //}
            //if (TxtShortName.IsBlankOrEmpty())
            //{
            //    TxtDescription.WarningMessage("AREA DESCRIPTION IS REQUIRED..!!");
            //    return false;
            //}
        }
        return true;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits()
            ? $"AREA SETUP DETAILS [{_actionTag}]"
            : "AREA SETUP DETAILS";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtMainArea.Clear();
        TxtCountry.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = !_actionTag.Equals("SAVE") && _actionTag.IsValueExits();
    }

    private int SaveAreaDetails()
    {
        const short sync = 1;
        AreaId = _actionTag is "SAVE" ? AreaId.ReturnMaxIntId("AR") : AreaId;
        _areaRepository.ObjArea.AreaId = AreaId;
        _areaRepository.ObjArea.NepaliDesc = TxtDescription.Text.Trim();
        _areaRepository.ObjArea.AreaName = TxtDescription.Text.Trim();
        _areaRepository.ObjArea.AreaCode = TxtShortName.Text.Trim();
        _areaRepository.ObjArea.Branch_Id = ObjGlobal.SysBranchId;
        _areaRepository.ObjArea.EnterBy = ObjGlobal.LogInUser;
        _areaRepository.ObjArea.EnterDate = DateTime.Now;
        _areaRepository.ObjArea.Country = TxtCountry.Text.Trim();
        _areaRepository.ObjArea.MainArea = _mAreaId;
        _areaRepository.ObjArea.Status = ChkActive.Checked;
        _areaRepository.ObjArea.SyncRowVersion = sync.ReturnSyncRowNo("AR", AreaId);
        return _areaRepository.SaveArea(_actionTag);
    }

    private async void GetAndSaveUnsynchronizedAreas()
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
            GetUrl = @$"{_configParams.Model.Item2}Area/GetAreasByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Area/InsertAreaList",
            UpdateUrl = @$"{_configParams.Model.Item2}Area/UpdateArea",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var areaRepo = DataSyncProviderFactory.GetRepository<Area>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));

        // pull all new account data
        var pullResponse = await _areaRepository.PullAreaFromServerToClientDBByCallCount(areaRepo, 1);
        SplashScreenManager.CloseForm();
        // push all new account data
        var sqlArQuery = _areaRepository.GetAreaScript();
        var queryResponse = await QueryUtils.GetListAsync<Area>(sqlArQuery);
        var arList = queryResponse.List.ToList();
        if (arList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await areaRepo.PushNewListAsync(arList);
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

    #region --------------- OBJECT ---------------

    public int AreaId;
    private int _mAreaId;
    public string AreaName = string.Empty;
    private string _actionTag;
    private readonly bool _isZoom;
    private readonly IAreaRepository _areaRepository;
    private readonly IMasterSetup _setup;
    private ClsMasterForm _control;

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

    #endregion --------------- OBJECT ---------------


}