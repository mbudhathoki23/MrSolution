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
using MrDAL.Domains.Interface;
using MrDAL.Domains.POS.Master;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Master;

public partial class FrmFloor : MrForm
{
    // FLOOR SETUP

    #region --------------- FLOOR  ---------------

    public FrmFloor()
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _clsFormControl = new ClsMasterForm(this, BtnExit);
        _floorRepository = new FloorRepository();
    }

    private void FrmFloor_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BindStations();
        _actionTag = "SAVE";
        BtnView.Enabled = true;
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedFloors();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmFloor_KeyPress(object sender, KeyPressEventArgs e)
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
                    Text = "FLOOR DETAILS SETUP";
                }
            }
            else
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO CLEAR FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
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

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            if (SaveFloorDetails() > 0)
            {
                if (_zoom)
                {
                    FloorDesc = TxtDescription.Text;
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "FLOOR", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                TxtDescription.ErrorMessage($@"ERROR OCCURS WHILE [{TxtDescription.Text}] [{_actionTag}]");
                return;
            }
        }
        else
        {
            TxtDescription.ErrorMessage($@"ERROR OCCURS WHILE [{TxtDescription.Text}] [{_actionTag}]");
            TxtDescription.Focus();
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
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage($"FLOOR DESCRIPTION IS REQUIRED FOR [{_actionTag}]..!!");
                return;
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetFloorList(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            FloorId = id;
            if (_actionTag != "SAVE")
            {
                SetGridDataToText();
                TxtDescription.ReadOnly = false;
            }
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits())
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtShortName.Text = TxtDescription.Text;
            }
            var result = TxtDescription.CheckValueExits(_actionTag, "FLOOR", "Description", FloorId);
            if (result.Rows.Count > 0)
            {
                TxtDescription.WarningMessage("FLOOR DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
        if (TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            TxtDescription.WarningMessage($"FLOOR DESCRIPTION IS REQUIRED FOR [{_actionTag}]..!!");
            return;
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage($"FLOOR SHORTNAME IS REQUIRED FOR [{_actionTag}]");
                return;
            }
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits())
        {
            var result = TxtShortName.CheckValueExits(_actionTag, "FLOOR", "ShortName", FloorId);
            if (result.Rows.Count > 0)
            {
                TxtShortName.WarningMessage("FLOOR SHORTNAME IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void CmbType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) //Action New
        {
            SendKeys.Send("{F4}");
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetFloorList("VIEW");
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedFloors);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- FLOOR  ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------
    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnDelete.Enabled = BtnEdit.Enabled = !isEnable;

        BtnDescription.Enabled = TxtDescription.Enabled;
        TxtShortName.Enabled = CmbType.Enabled = isEnable;
        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "UPDATE";
        BtnSave.Enabled = BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || isEnable;
        BtnView.Enabled = true;
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $@"FLOOR DETAILS SETUP [{_actionTag}]" : "FLOOR DETAILS SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        ChkActive.Checked = true;
    }

    private bool IsValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            //TxtDescription.WarningMessage($"FLOOR DESCRIPTION IS REQUIRED FOR [{_actionTag}]");
            TxtDescription.WarningMessage(@"FLOOR DESCRIPTION IS REQUPIRED  ...!");
            return false;
        }
        if (TxtShortName.IsBlankOrEmpty())
        {
            //TxtShortName.WarningMessage($"FLOOR SHORTNAME IS REQUIRED FOR [{_actionTag}]");
            TxtShortName.WarningMessage(@"FLOOR SHORTNAME IS REQUIRED...!");
            return false;
        }
        return true;
    }

    private void SetGridDataToText()
    {
        var dt = _objSetup.GetMasterFloor(_actionTag, FloorId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        FloorId = dt.Rows[0]["LedgerId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["Description"].ToString();
        TxtShortName.Text = dt.Rows[0]["ShortName"].ToString();
        CmbType.SelectedValue = dt.Rows[0]["Type"].ToString().Trim();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }
    private int SaveFloorDetails()
    {
        try
        {
            FloorId = _actionTag is "SAVE" ? FloorId.ReturnMaxIntId("FLOOR") : FloorId;
            _floorRepository.ObjFloor.FloorId = FloorId;
            _floorRepository.ObjFloor.Description = TxtDescription.Text.Trim().Replace("'", "''");
            _floorRepository.ObjFloor.ShortName = TxtShortName.Text.Trim().Replace("'", "''");
            _floorRepository.ObjFloor.Type = CmbType.Text.Trim().Replace("'", "''");
            _floorRepository.ObjFloor.EnterBy = ObjGlobal.LogInUser;
            _floorRepository.ObjFloor.EnterDate = DateTime.Now;
            _floorRepository.ObjFloor.Status = ChkActive.Checked;
            _floorRepository.ObjFloor.Branch_ID = ObjGlobal.SysBranchId;
            _floorRepository.ObjFloor.Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            _floorRepository.ObjFloor.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            _floorRepository.ObjFloor.SyncRowVersion = (short)(_actionTag is "UPDATE" ? _floorRepository.ObjFloor.SyncRowVersion.ReturnSyncRowNo("FLOOR", FloorId.ToString()) : 1);

            return _floorRepository.SaveSetupFloor(_actionTag);
        }
        catch (Exception e)
        {
            e.DialogResult();
            return 0;
        }


    }

    private void BindStations()
    {
        ValueModel<int> model1 = new ValueModel<int>(1, "First Value");
        ValueModel<string> model2 = new ValueModel<string>("ID_2", "Second Value");

        //var list = new List<ValueModel<string, string>>
        //{
        //    new("1St", "1St"),
        //    new("2nd", "2nd"),
        //    new("3rd", "3rd"),
        //    new("4th", "4th"),
        //    new("5th", "5th"),
        //    new("6th", "6th"),
        //    new("7th", "6th"),
        //    new("8th", "8th"),
        //    new("9th", "9th"),
        //    new("10th", "10th"),
        //    new("11th", "11th"),
        //    new("12th", "12th"),
        //    new("13th", "13th"),
        //    new("14th", "14th"),
        //    new("15th", "15th"),
        //    new("16th", "15th"),
        //    new("17th", "17th"),
        //    new("18th", "18th"),
        //    new("19th", "19th"),
        //    new("20th", "20th"),
        //    new("21st", "21st"),
        //    new("22nd", "21st"),
        //    new("23rd", "23rd"),
        //    new("24th", "24th"),
        //    new("25th", "25th")
        //};
        //CmbType.DataSource = list;
        CmbType.DisplayMember = "Item1";
        CmbType.ValueMember = "Item2";
        CmbType.SelectedIndex = 0;
    }

    private async void GetAndSaveUnsynchronizedFloors()
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
                GetUrl = @$"{_configParams.Model.Item2}Floor/GetFloorsByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}Floor/InsertFloorList",
                UpdateUrl = @$"{_configParams.Model.Item2}Floor/UpdateFloor"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var floorRepo = DataSyncProviderFactory.GetRepository<FloorSetup>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new table master data
            var pullResponse = await _floorRepository.PullFloorServerToClientByRowCounts(floorRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new table master data
            var sqlCrQuery = _floorRepository.GetFloorScript();
            var queryResponse = await QueryUtils.GetListAsync<FloorSetup>(sqlCrQuery);
            var curList = queryResponse.List.ToList();
            if (curList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await floorRepo.PushNewListAsync(curList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Class ---------------

    public string FloorDesc = string.Empty;

    public int FloorId;
    private bool _zoom;
    private string _query = string.Empty;
    private string _actionTag = string.Empty;
    private string _searchKey = string.Empty;
    private readonly IMasterSetup _objSetup = new ClsMasterSetup();
    private ClsMasterForm _clsFormControl;
    private readonly IFloorRepository _floorRepository;
    private FloorSetup ObjFloor { get; set; }
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion --------------- Class ---------------
}