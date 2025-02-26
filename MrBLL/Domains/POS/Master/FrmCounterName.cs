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
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmCounterName : MrForm
{
    #region --------------- Form ---------------

    public FrmCounterName()
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _control = new ClsMasterForm(this, BtnExit);
        _setup = new ClsMasterSetup();
        ObjGlobal.BindPrinter(CmbPrinter);
        var printerName = new PrinterSettings();
        _defaultPrinter = printerName.PrinterName;
        _counterRepository = new CounterRepository();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmCounterName_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        CmbPrinter.Text = _defaultPrinter;
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedCounters();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmCounterName_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("COUNTER") == DialogResult.Yes)
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
        if (IsFormValid())
        {
            if (SaveCounterDetails() != 0)
            {
                MessageBox.Show($@"DATA {_actionTag}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {

                MessageBox.Show($@"ERROR ON DATA {_actionTag}..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text)) BtnExit.PerformClick();
        else ClearControl();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetCounterList(_actionTag);
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
                !string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE" && TxtDescription.Focused)
            {
                MessageBox.Show(@"Counter can't Left Blank..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                TxtDescription.Focus();
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDescription, BtnDescription);
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is null) return;
        if (TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits() && _actionTag != "DELETE" && TxtDescription.Enabled)
        {
            TxtDescription.WarningMessage(@"Counter can't Left Blank..!!");
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetCounterList(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            CounterId = id;
            SetGridDataToText(CounterId);
            TxtDescription.ReadOnly = false;
            if (_actionTag == "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
        else
        {
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _actionTag.ToUpper() is "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text.Replace("'", "''"))
                ? ObjGlobal.BindAutoIncrementCode("CT", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text.Replace("'", "''");
        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE")
        {
            var dt = _setup.CheckIsValidData(_actionTag, "Counter", "CName", "CId",
                TxtDescription.Text.Replace("'", "''"), CounterId.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ClearControl();
                TxtDescription.Focus();
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsBlankOrEmpty() && _actionTag.IsValueExits() && _actionTag != "DELETE" && TxtShortName.Enabled)
        {
            TxtShortName.WarningMessage(@$"{TxtDescription.Text} SHORTNAME IS BLANK..!!");
            return;
        }
        var check = _setup.CheckIsValidData(_actionTag, "Counter", "CCode", "CId", TxtShortName.Text.Trim().Replace("'", "''"), CounterId.ToString());
        if (check == null || check.Rows.Count <= 0) return;
        TxtShortName.WarningMessage(@$"{TxtShortName.Text} SHORTNAME IS ALREADY EXITS..!!");
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedCounters);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- Form ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private bool IsFormValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage($"DESCRIPTION IS REQUIRED FOR[{_actionTag}]");
            return false;
        }

        if (TxtShortName.IsBlankOrEmpty() && !_actionTag.Equals("DELETE"))
        {
            TxtShortName.WarningMessage($"SHORTNAME IS REQUIRED FOR [{_actionTag}]");
            return false;
        }

        return true;
    }

    protected void SetGridDataToText(int counterId)
    {
        var dt = _setup.GetMasterCounter(_actionTag, counterId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        //if (dt.Rows.Count <= 0) return;
        CounterId = dt.Rows[0]["CId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["CName"].ToString();
        TxtShortName.Text = dt.Rows[0]["CCode"].ToString();
        CmbPrinter.Text = dt.Rows[0]["Printer"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnDelete.Enabled = BtnEdit.Enabled = BtnNew.Enabled = !isEnable;
        BtnDescription.Enabled = TxtDescription.Enabled = _actionTag == "DELETE" || isEnable;
        TxtShortName.Enabled = CmbPrinter.Enabled = isEnable;
        BtnCancel.Enabled = BtnSave.Enabled = _actionTag == "DELETE" || isEnable;
        ChkActive.Enabled = _actionTag == "UPDATE";
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty() ? "COUNTER DETAILS SETUP" : $"COUNTER DETAILS SETUP [{_actionTag}]";
        TxtDescription.Clear();
        TxtShortName.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        TxtDescription.Focus();
    }

    private int SaveCounterDetails()
    {
        try
        {
            CounterId = _actionTag is "SAVE" ? CounterId.ReturnMaxIntId("COUNTER") : CounterId;
            _counterRepository.ObjCounter.CId = CounterId;
            _counterRepository.ObjCounter.CName = TxtDescription.Text.Trim().Replace("'", "''");
            _counterRepository.ObjCounter.CCode = TxtShortName.Text.Trim().Replace("'", "''");
            _counterRepository.ObjCounter.Printer = CmbPrinter.Text.Trim();
            _counterRepository.ObjCounter.Status = ChkActive.Checked;
            _counterRepository.ObjCounter.Branch_ID = ObjGlobal.SysBranchId;
            _counterRepository.ObjCounter.Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null;
            _counterRepository.ObjCounter.EnterBy = ObjGlobal.LogInUser;
            _counterRepository.ObjCounter.EnterDate = DateTime.Now;
            _counterRepository.ObjCounter.SyncCreatedOn = DateTime.Now;
            _counterRepository.ObjCounter.SyncLastPatchedOn = DateTime.Now;
            _counterRepository.ObjCounter.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            _counterRepository.ObjCounter.SyncRowVersion = SyncRowId.ReturnSyncRowNo("COUNTER", CounterId.ToString());

            return _counterRepository.SaveCounter(_actionTag);

        }
        catch (Exception e)
        {
            e.DialogResult();
            return 0;
        }
    }

    public async void GetAndSaveUnsynchronizedCounters()
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
                GetUrl = @$"{_configParams.Model.Item2}Counter/GetCountersByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}Counter/InsertCounterList",
                UpdateUrl = @$"{_configParams.Model.Item2}Counter/UpdateCounter"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var counterRepo = DataSyncProviderFactory.GetRepository<Counter>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));

            // pull all new counter data
            var pullResponse = await _counterRepository.PullCounterServerToClientByRowCounts(counterRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new counter data
            var sqlCrQuery = _counterRepository.GetCounterScript();
            var queryResponse = await QueryUtils.GetListAsync<Counter>(sqlCrQuery);
            var counterList = queryResponse.List.ToList();
            if (counterList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await counterRepo.PushNewListAsync(counterList);
                SplashScreenManager.CloseForm();

            }

        }
        catch (Exception e)
        {
            var message = e.Message;
            throw;
        }
    }

    #endregion --------------- Method ---------------

    // OBJECT

    #region --------------- GLOBAL VARIABLE  ---------------


    public int CounterId { get; set; }
    public string CounterDesc { get; internal set; }

    private const int SyncRowId = 0;
    private readonly string _defaultPrinter;
    private string _actionTag = string.Empty;
    //  private string _defaultPrinter = string.Empty;
    public IMasterSetup _setup;
    private ClsMasterForm _control;
    // private bool _isZoom = false;
    private ICounterRepository _counterRepository;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;

    #endregion --------------- GLOBAL VARIABLE  ---------------
}