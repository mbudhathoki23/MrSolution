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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmSubLedger : MrForm
{
    // ACCOUNT SUB LEDGER EVENTS

    #region --------------- Form ---------------

    public FrmSubLedger(bool zoom = false)
    {
        InitializeComponent();
        _Zoom = zoom;
        clsForm = new ClsMasterForm(this, BtnExit);
        _subLedger = new SubLedgerRepository();
        _setup = new ClsMasterSetup();

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmSubLedger_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedSubLedgers);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmSubLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
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

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedSubLedgers);
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
        if (!FormValid())
        {
            TxtDescription.Focus();
            return;
        }

        if (SaveSubLedgerDetails() > 0)
        {
            if (_Zoom && _actionTag != "DELETE")
            {
                SubLedgerName = TxtDescription.Text.Replace("'", "''");
                Close();
                return;
            }

            CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "SUB LEDGER", _actionTag);
            ClearControl();
            TxtDescription.Focus();
            return;
        }
        TxtDescription.ErrorMessage($"ERROR OCCURS WHILE SUB LEDGER {_actionTag}");
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.IsBlankOrEmpty()) BtnExit.PerformClick();
        else ClearControl();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetSubLedgerList(_actionTag);
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {

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
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else if (TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsValueExits())
            {
                Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
            }
            else
            {
                TxtShortName.WarningMessage("SUB LEDGER SHORT NAME IS REQUIRED..!!");
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetSubLedgerList(_actionTag);
        if (id > 0 && _actionTag != "SAVE")
        {
            TxtDescription.Text = description;
            SubLedgerId = id;
            SetGridDataToText();
            TxtDescription.ReadOnly = false;
        }
        TxtDescription.Focus();
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty())
        {
            if (_actionTag.IsValueExits() && TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage("SUB LEDGER DESCRIPTION IS BLANK PLEASE ENTER THE VALUE ");
                return;
            }
        }
        if (_actionTag != "DELETE" && TxtDescription.IsValueExits())
        {
            TxtShortName.Text = _actionTag.Equals("SAVE")
                ? TxtDescription.GenerateShortName("SubLedger", "SLName")
                : TxtShortName.Text;
            var isResult = TxtDescription.CheckValueExits(_actionTag, "SubLedger", "SLName", SubLedgerId);
            if (isResult.Rows.Count > 0)
            {
                TxtShortName.WarningMessage($"[{TxtShortName.Text}] IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsBlankOrEmpty())
        {
            if (_actionTag.IsValueExits() && TxtShortName.ValidControl(ActiveControl))
            {
                TxtShortName.WarningMessage("SUB LEDGER SHORT NAME IS BLANK PLEASE ENTER THE VALUE ");
                return;
            }
        }
        if (_actionTag != "DELETE" && TxtShortName.IsValueExits())
        {
            var isResult = TxtShortName.CheckValueExits(_actionTag, "SubLedger", "SLCode", SubLedgerId);
            if (isResult.Rows.Count > 0)
            {
                TxtShortName.WarningMessage($"[{TxtShortName.Text}] IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N && _Zoom is false)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("", true);
            if (id > 0)
            {
                TxtLedger.Text = description;
                _ledgerId = id.ToString();
            }
            TxtLedger.Focus();
        }
        else if (e.KeyCode is Keys.Delete or Keys.Back)
        {
            _ledgerId = string.Empty;
            TxtLedger.Clear();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtLedger, BtnLedger);
        }
        Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        var (description, Id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (description.IsValueExits())
        {
            TxtLedger.Text = description;
            _ledgerId = Id.ToString();
        }
        TxtLedger.Focus();
    }

    #endregion --------------- Form ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private bool FormValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"SUBLEDGER DESCRIPTION IS REQUIRED....!");
            return false;
        }

        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"SUBLEDGER SHORTNAME  IS REQUIRED...!");
            return false;
        }

        return true;
    }

    protected void SetGridDataToText()
    {
        var dt = _subLedger.GetMasterSubLedger(_actionTag, SubLedgerId);
        if (dt.Rows.Count <= 0)
        {
            TxtDescription.WarningMessage("INVALID SUB LEDGER SELECTION PLEASE SELECT VALID SUB LEDGER ");
            return;
        }
        TxtDescription.Text = dt.Rows[0]["SLName"].ToString();
        TxtShortName.Text = dt.Rows[0]["SLCode"].ToString();
        TxtAddress.Text = dt.Rows[0]["SLAddress"].ToString();
        TxtPhoneNumber.Text = dt.Rows[0]["SLPhoneNo"].ToString();
        _ledgerId = dt.Rows[0]["GLID"].GetString();
        TxtLedger.Text = dt.Rows[0]["GLName"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnDelete.Enabled = BtnEdit.Enabled = BtnNew.Enabled = !isEnable;
        BtnDescription.Enabled = TxtDescription.Enabled = BtnSave.Enabled = _actionTag == "DELETE" || isEnable;
        TxtShortName.Enabled = TxtAddress.Enabled = TxtPhoneNumber.Enabled = isEnable;
        TxtLedger.Enabled = BtnLedger.Enabled = TxtAddress.Enabled = isEnable;
        BtnCancel.Enabled = BtnSave.Enabled = _actionTag == "DELETE" || isEnable;
        ChkActive.Enabled = _actionTag == "UPDATE";
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrWhiteSpace(_actionTag)
            ? $"SUB LEDGER DETAILS SETUP [{_actionTag}]"
            : @"SUB LEDGER DETAILS SETUP";
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtAddress.Clear();
        TxtPhoneNumber.Clear();
        ChkActive.Enabled = _actionTag.Equals("UPDATE");
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        TxtDescription.Focus();
    }

    private int SaveSubLedgerDetails()
    {
        short sync = 1;
        SubLedgerId = _actionTag is "SAVE"
            ? SubLedgerId.ReturnMaxIntId("SUBLEDGER")
            : SubLedgerId;
        _subLedger.ObjSubLedger.SLId = SubLedgerId;
        _subLedger.ObjSubLedger.SLName = TxtDescription.Text.Trim().Replace("'", "''");
        _subLedger.ObjSubLedger.SLCode = TxtShortName.Text.Trim().Replace("'", "''");
        _subLedger.ObjSubLedger.Branch_ID = ObjGlobal.SysBranchId;
        _subLedger.ObjSubLedger.EnterBy = ObjGlobal.LogInUser;
        _subLedger.ObjSubLedger.EnterDate = DateTime.Now;
        _subLedger.ObjSubLedger.SLAddress = TxtAddress.Text.Trim().Replace("'", "''");
        _subLedger.ObjSubLedger.SLPhoneNo = TxtPhoneNumber.Text;
        _subLedger.ObjSubLedger.GLID = _ledgerId;
        _subLedger.ObjSubLedger.Status = ChkActive.Checked;
        sync = sync.ReturnSyncRowNo("SUBLEDGER", SubLedgerId.ToString());

        _subLedger.ObjSubLedger.SyncRowVersion = sync > 0 ? sync : (short)1;
        return _subLedger.SaveSubLedger(_actionTag);
    }

    private async void GetAndSaveUnSynchronizedSubLedgers()
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
            GetUrl = @$"{_configParams.Model.Item2}SubLedger/GetSubLedgersByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SubLedger/InsertSubLedgerList",
            UpdateUrl = @$"{_configParams.Model.Item2}SubLedger/UpdateSubLedger",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var subLedgerRepo = DataSyncProviderFactory.GetRepository<SubLedger>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new account data
        var pullResponse = await _subLedger.PullSubLedgersServerToClientByRowCount(subLedgerRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlSlQuery = _subLedger.GetSubLedgerScript();
        var queryResponse = await QueryUtils.GetListAsync<SubLedger>(sqlSlQuery);
        var slList = queryResponse.List.ToList();
        if (slList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await subLedgerRepo.PushNewListAsync(slList);
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

    #region --------------- GLOBAL VARIABLE	 ---------------

    public string SubLedgerName = string.Empty;
    public string Desc = string.Empty;
    private string _ledgerId;
    public int SubLedgerId;
    public int AreaId;
    private string _actionTag = string.Empty;
    private readonly bool _Zoom;
    private readonly ISubLedgerRepository _subLedger;
    private IMasterSetup _setup;
    private ClsMasterForm clsForm;

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;


    #endregion --------------- GLOBAL VARIABLE	 ---------------

    private void TxtDescription_TextChanged(object sender, EventArgs e)
    {

    }
}