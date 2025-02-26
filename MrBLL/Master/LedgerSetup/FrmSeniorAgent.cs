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

public partial class FrmSeniorAgent : MrForm
{
    #region --------------- FRMSENIORAGENT ---------------

    public FrmSeniorAgent(bool isZoom = false)
    {
        InitializeComponent();
        _isZoom = isZoom;
        _clsFormControl = new ClsMasterForm(this, BtnExit);
        _mainAgentRepository = new MainAgentRepository();
        _injectData = new DbSyncRepoInjectData();
        ClearControl();
        EnableControl();
    }

    private void FrmSeniorAgent_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedSeniorAgents);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmSeniorAgent_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (CustomMessageBox.ClearVoucherDetails("MAIN AGENTS") == DialogResult.Yes)
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
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (FormValidate())
        {
            if (SaveAgentDetails() > 0)
            {
                if (_isZoom)
                {
                    AgentDesc = TxtDescription.Text.Trim();
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text, "SENIOR AGENTS", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                CustomMessageBox.Warning($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!");
                ClearControl();
                TxtDescription.Focus();
            }
        }
        else
        {
            CustomMessageBox.Warning($@"ERROR OCCUR WHILE {_actionTag.ToUpper()} DATA..!!");
            ClearControl();
            TxtDescription.Focus();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty())
        {
            BtnExit.PerformClick();
        }
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
                TxtDescription.WarningMessage(@"DESCRIPTION IS BLANK..!!");
                TxtDescription.Focus();
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
        var (description, id) = GetMasterList.GetSeniorAgentList(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            AgentId = id;
            if (_actionTag != "Save")
            {
                TxtDescription.ReadOnly = _actionTag != "SAVE";
                SetGridDataToText();
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
        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _actionTag.ToUpper() is "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text.Replace("'", "''"))
                ? ObjGlobal.BindAutoIncrementCode("SAgent", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text.Replace("'", "''");

        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE")
        {
            using var dt = _mainAgentRepository.CheckIsValidData(_actionTag, "SeniorAgent", "SAgent", "SAgentId",
                TxtDescription.Text.Replace("'", "''"), AgentId.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                MessageBox.Show(@"DESCRIPTION ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                ClearControl();
                TxtDescription.Focus();
            }
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is not null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (TxtShortName.IsValueExits())
        {
            var result = TxtShortName.IsDuplicate("SAgentCode", AgentId.ToString(), _actionTag, "SENIORAGENT");
            if (result)
            {
                TxtShortName.WarningMessage("SHORT NAME IS ALREADY EXITS..!!");
            }
        }
    }

    private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtPhone_Leave(object sender, EventArgs e)
    {
    }

    private void TxtPhone_Validating(object sender, CancelEventArgs e)
    {
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N && _isZoom is false)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (!description.IsValueExits())
            {
                return;
            }

            TxtLedger.Text = description;
            _ledgerId = id;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtLedger, BtnLedger);
        }
    }

    private void TxtLedger_Validating(object sender, CancelEventArgs e)
    {
        if (_ledgerId is 0 && !string.IsNullOrEmpty(TxtLedger.Text.Trim().Replace("'", "''")))
            _ledgerId = _mainAgentRepository.ReturnIntValueFromTable("AMS.GeneralLedger", "GLID", "GLName",
                TxtLedger.Text.Trim().Replace("''", "''"));
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        var (description, Id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (description.IsValueExits())
        {
            TxtLedger.Text = description;
            _ledgerId = Id.GetLong();
        }
        TxtLedger.Focus();
        //using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, _actionTag,
        //    string.Empty, "LIST");
        //if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        //{
        //    frmPickList.ShowDialog();
        //    if (frmPickList.SelectedList.Count > 0)
        //    {
        //        TxtLedger.Text = frmPickList.SelectedList[0]["GLName"].ToString().Trim();
        //        _ledgerId = Convert.ToInt64(frmPickList.SelectedList[0]["GLID"].ToString().Trim());
        //    }

        //    frmPickList.Dispose();
        //}
        //else
        //{
        //    MessageBox.Show(@"GENERAL LEDGER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //        MessageBoxIcon.Warning);
        //    TxtLedger.Focus();
        //    return;
        //}

        //ObjGlobal.SearchText = string.Empty;

    }

    private void TxtComm_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out _);
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
                TxtShortName.WarningMessage("SENIOR AGENT SHORT NAME IS REQUIRED..!!");
            }
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {
    }

    private void TxtAddress_Leave(object sender, EventArgs e)
    {
    }

    #endregion --------------- FRMSENIORAGENT ---------------

    #region --------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        TxtDescription.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;
        BtnDescription.Enabled = isEnable;

        TxtShortName.Enabled = isEnable;
        TxtAddress.Enabled = TxtPhone.Enabled = isEnable;

        TxtLedger.Enabled = TxtShortName.Enabled = BtnDescription.Enabled = isEnable;
        BtnLedger.Enabled = TxtCommission.Enabled = _actionTag is not "DELETE" && isEnable;

        ChkActive.Enabled = _actionTag == "UPDATE";

        BtnSave.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void ClearControl()
    {
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtPhone.Clear();
        TxtAddress.Clear();
        TxtLedger.Clear();
        TxtCommission.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        TxtLedger.ReadOnly = true;
        TxtDescription.Focus();
    }

    protected void SetGridDataToText()
    {
        var dt = _mainAgentRepository.GetMasterSrAgent(_actionTag, AgentId);
        if (dt.Rows.Count <= 0)
        {
            return;
        }
        AgentId = dt.Rows[0]["SAgentId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["SAgent"].ToString();
        TxtShortName.Text = dt.Rows[0]["SAgentCode"].ToString();
        TxtAddress.Text = dt.Rows[0]["Address"].ToString();
        TxtPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
        _ledgerId = dt.Rows[0]["GLID"].GetInt();
        TxtLedger.Text = dt.Rows[0]["GLName"].ToString();
        TxtCommission.Text = dt.Rows[0]["Comm"].GetDecimalString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
        if (_actionTag == "UPDATE")
        {
            TxtDescription.Focus();
        }
        else
        {
            BtnSave.Focus();
        }
    }

    private int SaveAgentDetails()
    {
        var syncRow = 0;
        AgentId = _actionTag is "SAVE" ? AgentId.ReturnMaxIntId("SS") : AgentId;
        _mainAgentRepository.ObjSeniorAgent.SAgentId = AgentId;
        _mainAgentRepository.ObjSeniorAgent.SAgent = TxtDescription.Text.Trim();
        _mainAgentRepository.ObjSeniorAgent.SAgentCode = TxtShortName.Text.Trim();
        _mainAgentRepository.ObjSeniorAgent.PhoneNo = TxtPhone.Text.Trim();
        _mainAgentRepository.ObjSeniorAgent.Address = TxtAddress.Text.Trim();
        _mainAgentRepository.ObjSeniorAgent.GLID = _ledgerId == 0 ? null : _ledgerId;
        _mainAgentRepository.ObjSeniorAgent.Status = ChkActive.Checked;
        _mainAgentRepository.ObjSeniorAgent.IsDefault = 0;
        _mainAgentRepository.ObjSeniorAgent.EnterBy = ObjGlobal.LogInUser;
        _mainAgentRepository.ObjSeniorAgent.EnterDate = DateTime.Now;
        _mainAgentRepository.ObjSeniorAgent.Branch_ID = ObjGlobal.SysBranchId;
        _mainAgentRepository.ObjSeniorAgent.Comm = TxtCommission.GetDecimal();
        syncRow = syncRow.ReturnSyncRowNo("SS", AgentId);
        _mainAgentRepository.ObjSeniorAgent.SyncRowVersion = (short)(_actionTag is "UPDATE" ? syncRow : 1);
        var result = _mainAgentRepository.SaveSeniorAgent(_actionTag);

        return result;
    }

    private bool FormValidate()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage("AGENT DESCRIPTION IS REQUIRED..!!");
            return false;
        }
        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage("AGENT SHORT NAME IS REQUIRED..!!");
            return false;
        }
        if (_actionTag != "SAVE" && AgentId is 0)
        {
            TxtDescription.WarningMessage("SELECTED AGENT IS INVALID..!!");
            return false;
        }
        return true;
    }

    private async void GetAndSaveUnSynchronizedSeniorAgents()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item1 == null)
        {
            return;
        }
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}SeniorAgent/GetSeniorAgentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SeniorAgent/InsertSeniorAgentList",
            UpdateUrl = @$"{_configParams.Model.Item2}SeniorAgent/UpdateSeniorAgent",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var seniorAgentRepo = DataSyncProviderFactory.GetRepository<MainAgent>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new mainagent data
        var pullResponse = await _mainAgentRepository.PullSeniorAgentsFromServerToClientDBByCallCount(seniorAgentRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new mainagent data

        var sqlSaQuery = _mainAgentRepository.GetSeniorAgentScript();
        var queryResponse = await QueryUtils.GetListAsync<MainAgent>(sqlSaQuery);
        var saList = queryResponse.List.ToList();
        if (saList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await seniorAgentRepo.PushNewListAsync(saList);
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

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedSeniorAgents);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    #endregion --------------- Method ---------------

    // GLOBAL VARIABLE

    #region --------------- GLOBAL VARIABLE ---------------

    public string AgentDesc = string.Empty;
    public int AgentId;
    private long _ledgerId;
    private readonly bool _isZoom;
    private string _actionTag = string.Empty;
    private readonly IMainAgentRepository _mainAgentRepository;
    private readonly IMasterSetup _setup;
    private ClsMasterForm _clsFormControl;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;

    #endregion --------------- GLOBAL VARIABLE ---------------


}