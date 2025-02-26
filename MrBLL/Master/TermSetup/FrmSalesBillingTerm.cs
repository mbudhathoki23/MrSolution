using DatabaseModule.CloudSync;
using DatabaseModule.Setup.TermSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Master.SystemSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.TermSetup;

public partial class FrmSalesBillingTerm : MrForm
{
    #region --------------- FrmSalesTerm   ---------------

    public FrmSalesBillingTerm(string term, bool zoom = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = zoom;
        _salesBillingTermRepository = new SalesBillingTermRepository();
        BindTermData(term);
        ClearControl();
        EnableControl();
        var form = new ClsMasterForm(this, BtnExit);
    }

    private void FrmSalesTerm_Load(object sender, EventArgs e)
    {
        CmbSign.SelectedIndex = 0;
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedSalesBillingTerm);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmSalesTerm_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape) return;
        if (!BtnNew.Enabled)
        {
            if (CustomMessageBox.ExitActiveForm() != DialogResult.Yes) return;
            _actionTag = string.Empty;
            ClearControl();
            EnableControl();
            BtnNew.Focus();
        }
        else
        {
            BtnExit.PerformClick();
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        TxtCode.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtCode.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtCode.Focus();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtCode.Text))
        {
            BtnExit.PerformClick();
        }
        else ClearControl();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidForm())
        {
            TxtCode.Focus();
            return;
        }

        if (SaveSalesTerm() > 0)
        {
            if (_isZoom)
            {
                Close();
                return;
            }

            CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "SALES TERM", _actionTag);
            TxtCode.Enabled = true;
            ClearControl();
            TxtCode.Focus();
        }
        else
        {
            TxtCode.ErrorMessage($"ERROR OCCURS WHILE SALES BILLING TERM {_actionTag}");
            TxtCode.Focus();
        }
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Dispose(true);
        }
    }

    private void TxtCode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCode_Click(sender, e);
        }
        else if (TxtCode.ReadOnly is true)
        {
            if (_actionTag.ToUpper() != "SAVE" && TxtCode.ReadOnly is true)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtCode, BtnCode);
            }
        }
    }

    private void BtnCode_Click(object sender, EventArgs e)
    {
        var (description, termId) = GetMasterList.GetSalesTermList(_actionTag, string.Empty);
        if (_actionTag == "SAVE")
        {
            TxtCode.Focus();
            return;
        }

        _termId = termId;
        TxtDescription.Text = description;
        TxtDescription.ReadOnly = false;
        SetGridDataToText(_termId);
        TxtCode.Enabled = false;
        CmbModule.Enabled = false;
        TxtDescription.Focus();
    }

    private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtCode.IsBlankOrEmpty())
            {
                TxtCode.WarningMessage(@"CODE NUMBER CAN NOT BE ZERO..!!");
                TxtCode.Focus();
            }
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var _);
        }
    }

    private void TxtCode_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is not null && string.IsNullOrEmpty(TxtCode.Text.Trim()) && TxtCode.Focused &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"CODE IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtCode.Focus();
        }
    }

    private void TxtCode_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && _actionTag != "DELETE")
        {
            if (TxtCode.GetInt() is 0 && !_actionTag.Equals("SAVE"))
            {
                TxtCode.WarningMessage(@"CODE NUMBER CAN NOT BE ZERO..!!");
                TxtCode.Focus();
            }
            using var dt = _salesBillingTermRepository.CheckIsValidData(_actionTag, "ST_Term", "Order_No", "ST_ID", TxtCode.Text, _termId.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            TxtCode.WarningMessage(@"CODE ALREADY EXITS..!!");
            TxtCode.Focus();
        }
    }

    private void CmbModule_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(CmbModule.Text.Trim()))
            {
                MessageBox.Show(@"Billing Module Cannot Left  blank, Plz. Enter The Name ..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbModule.Focus();
            }
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && TxtDescription.IsBlankOrEmpty())
        {
            MessageBox.Show(@"PLEASE ENTER THE DESCRIPTION OF PURCHASE BILLING TERM..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && TxtDescription.IsBlankOrEmpty() && TxtDescription.Focused &&
            _actionTag.IsValueExits())
        {
            MessageBox.Show(@"PLEASE ENTER THE DESCRIPTION OF SALE BILLING TERM..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void CmbType_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        (TxtLedger.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag, "OTHER", DateTime.Now.GetDateString(), "LIST");
        TxtLedger.Focus();
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && TxtLedger.IsBlankOrEmpty() && TxtLedger.Focused && _actionTag.IsValueExits())
        {
            this.NotifyValidationError(TxtLedger, "LEDGER SHOULD BE TAG IN BILLING TERM");
            TxtLedger.Focus();
            return;
        }
    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtLedger.Text, _ledgerId) = GetMasterList.CreateGeneralLedger("OTHER", true);
            TxtLedger.Focus();
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
        if (_ledgerId is 0 && !string.IsNullOrEmpty(TxtLedger.Text.Trim()))
        {
            _ledgerId = _salesBillingTermRepository.ReturnIntValueFromTable("AMS.GeneralLedger", "GLID", "GLName",
                TxtLedger.Text.Trim());
        }
    }

    private void TxtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void Cmb_TypeKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbBasis_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbSign_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbCondition_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        BtnCode_Click(sender, e);
        BtnView.Focus();
    }

    #endregion --------------- FrmSalesTerm   ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private int SaveSalesTerm()
    {
        _salesBillingTermRepository.StTerm.ST_ID = _actionTag.Equals("SAVE") ? _termId.ReturnMaxIntId("ST_TERM") : _termId;
        _salesBillingTermRepository.StTerm.Order_No = TxtCode.GetInt();
        _salesBillingTermRepository.StTerm.Module = CmbModule.SelectedValue.ToString();
        _salesBillingTermRepository.StTerm.ST_Name = TxtDescription.Text.Trim();
        _salesBillingTermRepository.StTerm.ST_Type = CmbType.SelectedValue.ToString();
        _salesBillingTermRepository.StTerm.Ledger = _ledgerId;
        _salesBillingTermRepository.StTerm.ST_Basis = CmbBasis.SelectedValue.ToString();
        _salesBillingTermRepository.StTerm.ST_Sign = CmbSign.Text.Trim();
        _salesBillingTermRepository.StTerm.ST_Condition = CmbCondition.SelectedValue.ToString();
        _salesBillingTermRepository.StTerm.ST_Rate = TxtRate.GetDecimal();
        _salesBillingTermRepository.StTerm.ST_Branch = ObjGlobal.SysBranchId;
        _salesBillingTermRepository.StTerm.ST_CompanyUnit = ObjGlobal.SysCompanyUnitId;
        _salesBillingTermRepository.StTerm.ST_Profitability = ChkProfitability.Checked;
        _salesBillingTermRepository.StTerm.ST_Supess = ChkSupress.Checked;
        _salesBillingTermRepository.StTerm.ST_Status = ChkActive.Checked;
        _salesBillingTermRepository.StTerm.EnterBy = ObjGlobal.LogInUser;
        _salesBillingTermRepository.StTerm.EnterDate = DateTime.Now;
        _salesBillingTermRepository.StTerm.SyncRowVersion = _salesBillingTermRepository.StTerm.SyncRowVersion.ReturnSyncRowNo("ST_TERM", _termId.ToString());
        return _salesBillingTermRepository.SaveSalesTerm(_actionTag);
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_actionTag.Equals("DELETE");
        TxtCode.Enabled = BtnCode.Enabled = isEnable || _actionTag.Equals("DELETE");

        CmbModule.Enabled = TxtDescription.Enabled == isEnable;
        CmbType.Enabled = TxtLedger.Enabled = CmbBasis.Enabled = isEnable;
        CmbCondition.Enabled = isEnable;
        CmbSign.Enabled = isEnable;
        TxtRate.Enabled = isEnable;
        ChkProfitability.Enabled = isEnable;
        ChkSupress.Enabled = isEnable;

        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _actionTag.Equals("DELETE");
        ChkActive.Enabled = _actionTag is "UPDATE";
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty() ? "SALES BILLING TERM" : $"SALES BILLING TERM [{_actionTag}]";
        TxtCode.ReadOnly = _actionTag.ToUpper() != "SAVE";
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        _termId = 0;
        TxtCode.Clear();
        TxtDescription.Clear();
        _ledgerId = 0;
        TxtLedger.Clear();
        _ledgerId = _ledgerId is 0 ? ObjGlobal.SalesLedgerId : _ledgerId;
        TxtLedger.Text = _salesBillingTermRepository.GetLedgerDescription(_ledgerId);
        CmbModule.SelectedIndex = 0;
        CmbSign.SelectedIndex = 0;
        CmbType.SelectedIndex = 0;
        CmbCondition.SelectedIndex = 0;
        CmbBasis.SelectedIndex = 0;
        TxtRate.Clear();
    }

    private void SetGridDataToText(int selectedSalesTermId)
    {
        var dt = _salesBillingTermRepository.GetMasterSalesTermList(_actionTag, "", selectedSalesTermId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }

        _termId = dt.Rows[0]["ST_ID"].GetInt();
        TxtCode.Text = dt.Rows[0]["Order_No"].ToString();
        CmbModule.Text = dt.Rows[0]["Module"].ToString();
        TxtDescription.Text = dt.Rows[0]["ST_Name"].ToString();
        CmbType.Text = dt.Rows[0]["ST_Type"].ToString();
        _ledgerId = dt.Rows[0]["GLID"].GetLong();
        TxtLedger.Text = dt.Rows[0]["GlName"].ToString();
        var basic = dt.Rows[0]["ST_Basis"].ToString();
        CmbBasis.SelectedIndex = basic.Equals("P") ? 1 : 0;
        var sign = dt.Rows[0]["ST_Sign"].ToString();
        CmbSign.SelectedIndex = sign.Equals("+") ? 0 : 1;

        var condition = dt.Rows[0]["ST_Condition"].ToString();
        CmbCondition.SelectedIndex = condition.Equals("P") ? 1 : 0;

        TxtRate.Text = dt.Rows[0]["ST_Rate"].GetDecimalString();

        ChkProfitability.Checked = dt.Rows[0]["ST_Profitability"].GetBool();
        ChkSupress.Checked = dt.Rows[0]["ST_Supess"].GetBool();
        ChkActive.Checked = dt.Rows[0]["ST_Status"].GetBool();

        var isUsed = _salesBillingTermRepository.IsBillingTermUsedOrNot("SB", _termId);
        CmbModule.Enabled = !isUsed;
        //TxtCode.Enabled = !isUsed;
        CmbType.Enabled = !isUsed;
        CmbBasis.Enabled = !isUsed;
        CmbCondition.Enabled = !isUsed;
        CmbSign.Enabled = !isUsed;
    }

    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(_actionTag) || string.IsNullOrEmpty(TxtDescription.Text) ||
            string.IsNullOrEmpty(TxtCode.Text))
        {
            return false;
        }

        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            MessageBox.Show(@"SALES TERM DESCRIPTION IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtLedger.Text.Trim()))
        {
            MessageBox.Show(@"GENERAL LEDGER NAME IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtDescription.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtCode.Text.Trim()))
        {
            MessageBox.Show(@"Billing Code is Required.", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtCode.Focus();
            return false;
        }

        return true;
    }

    private void BindTermData(string term)
    {
        CmbModule.BindTermModule(term);
        CmbType.BindTermType();
        CmbBasis.BindTermBasis();
        CmbCondition.BindTermCondition();
    }
    private async void GetAndSaveUnSynchronizedSalesBillingTerm()
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
            GetUrl = @$"{_configParams.Model.Item2}SalesBillingTerm/GetSalesBillingTermByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}SalesBillingTerm/InsertSalesBillingTermList",
            UpdateUrl = @$"{_configParams.Model.Item2}SalesBillingTerm/UpdateSalesBillingTerm"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var salesTermRepo = DataSyncProviderFactory.GetRepository<ST_Term>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new sales billing term data
        var pullResponse = await _salesBillingTermRepository.PullSalesBillingTermServerToClientByRowCounts(salesTermRepo, 1);
        SplashScreenManager.CloseForm();
        // push all new sales billing term data
        var sqlglQuery = _salesBillingTermRepository.GetSalesTermScript();
        var queryResponse = await QueryUtils.GetListAsync<ST_Term>(sqlglQuery);
        var glList = queryResponse.List.ToList();
        if (glList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await salesTermRepo.PushNewListAsync(glList);
            SplashScreenManager.CloseForm();
        }
    }
    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Class  ---------------

    private int _termId;
    private long _ledgerId;
    private readonly bool _isZoom;
    private string _actionTag;
    private readonly ISalesBillingTermRepository _salesBillingTermRepository;
    private IMasterSetup _setup;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();

    #endregion --------------- Class  ---------------


}