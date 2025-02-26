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

public partial class FrmJuniorAgent : MrForm
{

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnDescription_Click(sender, e);
        BtnView.Focus();
    }

    #region --------------- GLOBAL VARIABLE ---------------

    public string AgentName = string.Empty;

    private int SAgentId;
    public int AgentId;
    private long LedgerId;
    private readonly bool _Zoom;

    private string _actionTag = string.Empty;
    private string Query = string.Empty;
    private string Searchtext = string.Empty;

    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;

    private readonly IJuniorAgentRepository _groupSetup = new JuniorAgentRepository();
    private IMasterSetup _setup;
    private ClsMasterForm clsFormControl;

    #endregion --------------- GLOBAL VARIABLE ---------------

    #region --------------- FORM ---------------

    public FrmJuniorAgent(bool Zoom = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _Zoom = Zoom;
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
        clsFormControl = new ClsMasterForm(this, BtnExit);
    }

    private void FrmAgent_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedJuniorAgents);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmAgent_KeyPress(object sender, KeyPressEventArgs e)
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
                    Text = "JUNIOR AGENT SETUP";
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
            Task.Run(GetAndSaveUnSynchronizedJuniorAgents);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    #endregion --------------- FORM ---------------

    #region --------------- Botton ---------------

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
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl(false, false);
        TxtDescription.Focus();
    }

    private bool FormValidate()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"AGENT DESCRIPTION NAME IS REQUIRED..!!");
            return false;
        }

        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"SHORT NAME IS REQUIRED..!!");
            return false;
        }

        //if (string.IsNullOrEmpty(_actionTag)) return false;
        //if (string.IsNullOrEmpty(TxtDescription.Text))
        //{
        //    MessageBox.Show(@"AGENT DESCRIPTION NAME IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //        MessageBoxIcon.Warning);
        //    TxtDescription.Focus();
        //    return false;
        //}

        //if (string.IsNullOrEmpty(TxtShortName.Text))
        //{
        //    MessageBox.Show(@"SHORT NAME IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //        MessageBoxIcon.Warning);
        //    TxtShortName.Focus();
        //    return false;
        //}

        return true;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (FormValidate())
        {
            if (IUDAgentDetails() > 0)
            {
                if (_Zoom is true)
                {
                    AgentName = TxtDescription.Text.Trim();
                    Close();
                    return;
                }

                MessageBox.Show($@"AGENT / SALES MAN {_actionTag.ToUpper()}  SUCCESSFULLY..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearControl();
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
        if (TxtDescription.IsBlankOrEmpty())
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
        }
    }

    #endregion --------------- Botton ---------------

    #region --------------- Method ---------------

    private void EnableControl(bool Txt = false, bool Btn = true)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = Btn;

        TxtDescription.Enabled =
            BtnDescription.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || Txt;
        TxtShortName.Enabled = Txt;
        TxtAddress.Enabled =
            TxtPhone.Enabled = TxtLedger.Enabled = TxtCommission.Enabled = TxtSeniorAgent.Enabled = Txt;

        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "UPDATE";
        BtnSave.Enabled = BtnCancel.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE" || Txt;
    }

    private void ClearControl()
    {
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtPhone.Clear();
        TxtAddress.Clear();
        TxtLedger.Clear();
        TxtCommission.Clear();
        TxtSeniorAgent.Clear();
        ChkActive.Checked = true;
        TxtLedger.ReadOnly = true;
        TxtSeniorAgent.ReadOnly = true;
        TxtDescription.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag.ToUpper() != "SAVE";
    }

    protected void SetGridDataToText(int AgentId)
    {
        using var dt = _groupSetup.GetMasterJrAgent(_actionTag, AgentId);
        if (dt.Rows.Count > 0)
        {
            int.TryParse(dt.Rows[0]["AgentId"].ToString(), out var _JrAgentId);
            AgentId = _JrAgentId;
            TxtDescription.Text = dt.Rows[0]["AgentName"].ToString();
            TxtShortName.Text = dt.Rows[0]["AgentCode"].ToString();
            TxtAddress.Text = dt.Rows[0]["Address"].ToString();
            TxtPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
            long.TryParse(dt.Rows[0]["GLCode"].ToString(), out var _ledgerId);
            LedgerId = _ledgerId;
            TxtLedger.Text = dt.Rows[0]["GLName"].ToString();
            int.TryParse(dt.Rows[0]["SAgent"].ToString(), out var _SrAgentId);
            SAgentId = _SrAgentId;
            TxtSeniorAgent.Text = dt.Rows[0]["SAgentDesc"].ToString();
            var Commission = (decimal)dt.Rows[0]["Commission"];
            TxtCommission.Text = Commission == 0 ? "0" : Commission.ToString("##.##########");

            if (_actionTag is "UPDATE")
                TxtDescription.Focus();
            else
                BtnSave.Focus();
        }
    }

    private int IUDAgentDetails()
    {
        AgentId = _actionTag is "SAVE"
            ? ObjGlobal.ReturnInt(ClsMasterSetup.ReturnMaxIntValue("AMS.JuniorAgent", "AgentId").ToString())
            : AgentId;
        _groupSetup.ObjJuniorAgent.AgentId = AgentId;
        _groupSetup.ObjJuniorAgent.AgentName = TxtDescription.Text.Trim();
        _groupSetup.ObjJuniorAgent.AgentCode = TxtShortName.Text.Trim();
        _groupSetup.ObjJuniorAgent.Address = TxtAddress.Text.Trim();
        _groupSetup.ObjJuniorAgent.PhoneNo = TxtPhone.Text.Trim();
        _groupSetup.ObjJuniorAgent.GLCode = (LedgerId > 0 ? LedgerId : null);
        _groupSetup.ObjJuniorAgent.SAgent = (SAgentId > 0 ? SAgentId : null);
        //ObjJuniorAgent.SAgent.GetInt() > 0 ? $"N'{ObjJuniorAgent.SAgent}'," : "NULL,")
        _groupSetup.ObjJuniorAgent.CrDays = 0;
        _groupSetup.ObjJuniorAgent.CrLimit = 0;
        _groupSetup.ObjJuniorAgent.CrTYpe = "I";
        _groupSetup.ObjJuniorAgent.EnterBy = ObjGlobal.LogInUser;
        _groupSetup.ObjJuniorAgent.EnterDate = DateTime.Now;
        _groupSetup.ObjJuniorAgent.Branch_ID = ObjGlobal.SysBranchId;
        _groupSetup.ObjJuniorAgent.TargetLimit = 0;
        _groupSetup.ObjJuniorAgent.Commission = ObjGlobal.ReturnDecimal(TxtCommission.Text.Trim());
        _groupSetup.ObjJuniorAgent.Status = ChkActive.Checked;
        _groupSetup.ObjJuniorAgent.SyncRowVersion = (short)(_actionTag is "UPDATE"
            ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.JuniorAgent", "SyncRowVersion", "AgentId",
                AgentId.ToString())
            : 1);
        return _groupSetup.SaveJuniorAgent(_actionTag);
    }

    private async void GetAndSaveUnSynchronizedJuniorAgents()
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
            GetUrl = @$"{_configParams.Model.Item2}JuniorAgent/GetJuniorAgentsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}JuniorAgent/InsertJuniorAgentList",
            UpdateUrl = @$"{_configParams.Model.Item2}JuniorAgent/UpdateJuniorAgent",
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var juniorAgentRepo = DataSyncProviderFactory.GetRepository<JuniorAgent>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));

        // pull all new account data
        var pullResponse = await _groupSetup.PullJuniorAgentsFromServerToClientDBByCallCount(juniorAgentRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlJaQuery = _groupSetup.GetJuniorAgentScript();
        var queryResponse = await QueryUtils.GetListAsync<JuniorAgent>(sqlJaQuery);
        var jaList = queryResponse.List.ToList();
        if (jaList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await juniorAgentRepo.PushNewListAsync(jaList);
            SplashScreenManager.CloseForm();

        }
    }

    #endregion --------------- Method ---------------

    #region --------------- Event ---------------

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
                TxtDescription.WarningMessage("JUNIOR AGENT DESCRIPTION IS BLANK..!!");
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
        {
            (TxtDescription.Text, AgentId) = GetMasterList.GetAgentList(_actionTag);
            if (!_actionTag.Equals("SAVE"))
            {
                SetGridDataToText(AgentId);
                TxtDescription.ReadOnly = !_actionTag.Equals("UPDATE");
            }
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) && _actionTag.ToUpper() is "SAVE")
            TxtShortName.Text = string.IsNullOrEmpty(TxtShortName.Text)
                ? ObjGlobal.BindAutoIncrementCode("Agent", TxtDescription.Text.Trim().Replace("'", "''"))
                : TxtShortName.Text;
        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE")
        {
            using var dt = _groupSetup.CheckIsValidData(_actionTag, "JuniorAgent", "AgentName", "AgentId",
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
        if (ActiveControl != null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("JUNIOR AGENT SHORT NAME IS REQUIRED ");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        using var dt = _groupSetup.CheckIsValidData(_actionTag, "JuniorAgent", "AgentCode", "AgentId",
            TxtDescription.Text.Replace("'", "''"),
            AgentId.ToString()); //GetConnection.SelectDataTableQuery(_Query);
        if (dt.Rows.Count > 0)
        {
            MessageBox.Show(@"SHORTNAME ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N && _Zoom is false)
        {
            using var Frm = new FrmGeneralLedger("Other", true);
            Frm.ShowDialog();
            TxtLedger.Text = Frm.LedgerDesc.Trim();
            LedgerId = Frm.LedgerId;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtLedger, BtnLedger);
        }
    }

    private void TxtLedger_Validating(object sender, CancelEventArgs e)
    {
        if (LedgerId is 0 && !string.IsNullOrEmpty(TxtLedger.Text.Trim().Replace("'", "''")))
            LedgerId = _groupSetup.ReturnIntValueFromTable("AMS.GeneralLedger", "GLID", "GLName",
                TxtLedger.Text.Trim().Replace("'", "''"));
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
        // using var frmPickList = new FrmAutoPopList("MAX", "GENERALLEDGER", ObjGlobal.SearchText, _actionTag,
        //     string.Empty, "LIST");
        // if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        // {
        //     frmPickList.ShowDialog();
        //     if (frmPickList.SelectedList.Count > 0)
        //     {
        //         TxtLedger.Text = frmPickList.SelectedList[0]["GLName"].ToString().Trim();
        //         LedgerId = Convert.ToInt64(frmPickList.SelectedList[0]["GLID"].ToString().Trim());
        //     }
        //
        //     frmPickList.Dispose();
        // }
        // else
        // {
        //     MessageBox.Show(@"GENERAL LEDGER ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //         MessageBoxIcon.Warning);
        //     TxtLedger.Focus();
        //     return;
        // }
        //
        // ObjGlobal.SearchText = string.Empty;
        // TxtLedger.Focus();
    }

    private void TxtComm_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void TxtPhone_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtSeniorAgent_Validating(object sender, CancelEventArgs e)
    {
        if (SAgentId is 0 && !string.IsNullOrEmpty(TxtSeniorAgent.Text.Trim().Replace("'", "''")))
            SAgentId = _groupSetup.ReturnIntValueFromTable("AMS.SeniorAgent", "SAgentId", "SAgent",
                TxtLedger.Text.Trim().Replace("'", "''"));
    }

    private void TxtSeniorAgent_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N) // && _Zoom is false)
        {
            using var Frm = new FrmSeniorAgent(true);
            Frm.ShowDialog();
            TxtSeniorAgent.Text = Frm.AgentDesc.Trim();
            SAgentId = Frm.AgentId;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnSeniorAgent_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            //Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
    }

    private void BtnSeniorAgent_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetSeniorAgentList(_actionTag);
        if (description.IsValueExits())
        {
            TxtSeniorAgent.Text = description;
            _ledgerId = id.ToString();
        }
        TxtSeniorAgent.Focus();
        // using var frmPickList = new FrmAutoPopList("MIN", "MAINAGENT", _actionTag, ObjGlobal.SearchText,
        //     string.Empty, "LIST");
        // if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        // {
        //     frmPickList.ShowDialog();
        //     if (frmPickList.SelectedList.Count > 0)
        //     {
        //         TxtSeniorAgent.Text = frmPickList.SelectedList[0]["SAgent"].ToString().Trim();
        //         SAgentId = Convert.ToInt32(frmPickList.SelectedList[0]["SAgentId"].ToString().Trim());
        //     }
        //
        //     frmPickList.Dispose();
        // }
        // else
        // {
        //     MessageBox.Show(@"AGENT NOT FOUND..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //         MessageBoxIcon.Warning);
        //     TxtSeniorAgent.Focus();
        //     return;
        // }
        //
        // ObjGlobal.SearchText = string.Empty;
        // TxtSeniorAgent.Focus();
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {
    }

    private string _ledgerId;

    #endregion --------------- Event ---------------
}