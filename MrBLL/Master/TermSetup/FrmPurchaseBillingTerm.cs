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

public partial class FrmPurchaseBillingTerm : MrForm
{
    // PURCHASE BILLING TERM

    #region--------------- Frm ---------------

    public FrmPurchaseBillingTerm(string term, bool zoom = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = zoom;
        _purchaseBillingTermRepository = new PurchaseBillingTermRepository();
        BindTermData(term);
        var form = new ClsMasterForm(this, BtnExit);
    }

    private void FrmPurchaseTerm_Load(object sender, EventArgs e)
    {
        ClearControl();
        EnableControl();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedPurchaseBillingTerm);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmPurchaseTerm_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape) return;
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (CustomMessageBox.ClearVoucherDetails("PURCHASE TERM") == DialogResult.Yes)
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
        if (TxtCode.IsValueExits())
        {
            BtnExit.PerformClick();
        }
        else ClearControl();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        var (description, termId) = GetMasterList.GetPurchaseTermList("SAVE", "");
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsFormValid())
        {
            TxtCode.Focus();
            return;
        }

        if (SavePurchaseTerm() > 0)
        {
            if (_isZoom)
            {
                PurchaseTermDesc = TxtDescription.Text.Trim();
                Close();
                return;
            }
            CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "PURCHASE TERM", _actionTag);
            TxtCode.Enabled = true;
            ClearControl();
            TxtCode.Focus();
        }
        else
        {
            MessageBox.Show($@"DATA {_actionTag.ToUpper()}  UN SUCCESSFULLY..!!", ObjGlobal.Caption,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            TxtCode.Focus();
        }
    }

    private void CmbModule_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void TxtCode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCode_Click(sender, e);
        }
        else if (TxtCode.ReadOnly)
        {
            if (_actionTag.ToUpper() != "SAVE")
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnCode);
            }
        }
    }

    private void BtnCode_Click(object sender, EventArgs e)
    {
        var (description, termId) = GetMasterList.GetPurchaseTermList("SAVE", "");
        if (_actionTag == "SAVE")
        {
            TxtCode.Focus();
            return;
        }
        _termId = termId;
        TxtDescription.Text = description;
        TxtDescription.ReadOnly = false;
        TxtCode.Enabled = false;
        CmbModule.Enabled = false;
        SetGridDataToText();
        TxtDescription.Focus();
    }

    private void TxtCode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtCode.GetInt() is 0)
            {
                TxtCode.WarningMessage(@"CODE NUMBER CAN NOT BE ZERO..!!");
                TxtCode.Focus();
            }
        }
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtCode_Leave(object sender, EventArgs e)
    {
    }

    private void TxtCode_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && _actionTag != "DELETE")
        {
            if (_termId is 0 && !_actionTag.Equals("SAVE"))
            {
                TxtCode.WarningMessage(@"CODE NUMBER CAN NOT BE ZERO..!!");
                TxtCode.Focus();
            }
            using var dt = _purchaseBillingTermRepository.CheckIsValidData(_actionTag, "PT_Term", "Order_No", "PT_ID", TxtCode.Text, _termId.ToString());
            if (dt == null || dt.Rows.Count <= 0)
            {
                return;
            }
            TxtCode.WarningMessage(@"CODE ALREADY EXITS..!!");
            TxtCode.Focus();
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                CustomMessageBox.Warning(@"PLEASE ENTER THE DESCRIPTION OF PURCHASE BILLING TERM..!!");
                TxtDescription.Focus();
            }
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnCode_Click(sender, e);
        }
        else if (TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnCode);
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.Focused && TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            this.NotifyValidationError(TxtDescription, $"TERM DESCRIPTION IS REQUIRED FOR {_actionTag}");
        }
    }

    private void CmbType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
            if (string.IsNullOrEmpty(CmbType.Text.Trim()))
            {
                MessageBox.Show(@"PURCHASE TYPE CANNOT LEFT  BLANK, PLEASE. ENTER THE NAME ..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbType.Focus();
            }
    }

    private void TxtLedger_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtLedger.Text.Trim().Replace("'", "''")) && TxtLedger.Focused &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"LEDGER IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtLedger.Focus();
        }
    }

    private void TxtLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N && _isZoom is false)
        {
            (TxtLedger.Text, _ledgerId) = GetMasterList.CreateGeneralLedger("OTHER", true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtLedger.IsBlankOrEmpty())
            {
                TxtLedger.WarningMessage(@"LEDGER IS BLANK PLEASE SELECT THE VALUE..!!");
                TxtLedger.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtLedger, BtnLedger);
        }
    }

    private void TxtLedger_Validating(object sender, CancelEventArgs e)
    {
        _ledgerId = _ledgerId switch
        {
            0 when TxtLedger.IsValueExits() => _purchaseBillingTermRepository.ReturnIntValueFromTable("AMS.GeneralLedger", "GLID", "GLName", TxtLedger.Text.Trim().Replace("'", "''")),
            _ => _ledgerId
        };
    }

    private void BtnLedger_Click(object sender, EventArgs e)
    {
        (TxtLedger.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag, "OTHER");
        TxtLedger.Focus();
    }

    private void CmbBasis_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && CmbCondition.Text.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"PURCHASE BASIS CANNOT LEFT  BLANK, PLEASE. ENTER THE NAME ..!!");
            CmbCondition.Focus();
        }
    }

    private void CmbSign_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && CmbSign.Text.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"PURCHASE SIGN CANNOT LEFT  BLANK, PLEASE. ENTER THE NAME ..!!");
            CmbSign.Focus();
        }
    }

    private void CmbCondition_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && CmbCondition.Text.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"PURCHASE CONDITION CANNOT LEFT  BLANK, PLEASE. ENTER THE NAME ..!!");
            CmbCondition.Focus();
        }
    }

    private void TxtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtRate_Validating(object sender, CancelEventArgs e)
    {
        TxtRate.Text = TxtRate.GetDecimalString();
    }

    #endregion

    // METHOD FOR THIS FORM

    #region--------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TxtCode.Enabled = BtnCode.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtDescription.Enabled = CmbModule.Enabled = CmbType.Enabled = TxtLedger.Enabled = isEnable;
        BtnLedger.Enabled = CmbBasis.Enabled = CmbCondition.Enabled = CmbSign.Enabled = isEnable;
        TxtRate.Enabled = ChkStockValuation.Enabled = ChkSupress.Enabled = isEnable;
        ChkActive.Enabled = _actionTag is "UPDATE";
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _actionTag.Equals("DELETE");
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty() ? "PURCHASE BILLING TERM" : $"PURCHASE BILLING TERM [{_actionTag}]";
        TxtCode.ReadOnly = _actionTag.ToUpper() != "SAVE";
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        _termId = 0;
        TxtCode.Clear();
        TxtDescription.Clear();
        _ledgerId = 0;
        CmbBasis.SelectedIndex = 0;
        CmbCondition.SelectedIndex = 0;
        CmbModule.SelectedIndex = 0;
        CmbSign.SelectedIndex = 0;
        CmbType.SelectedIndex = 0;
        TxtLedger.Clear();
        _ledgerId = _ledgerId is 0 ? ObjGlobal.PurchaseLedgerId : _ledgerId;
        TxtLedger.Text = _purchaseBillingTermRepository.GetLedgerDescription(_ledgerId);
        TxtRate.Clear();
    }

    private int SavePurchaseTerm()
    {
        _purchaseBillingTermRepository.PtTerm.PT_Id = _actionTag.Equals("SAVE") ? _termId.ReturnMaxIntId("PT_TERM") : _termId;
        _purchaseBillingTermRepository.PtTerm.Order_No = TxtCode.GetInt();
        _purchaseBillingTermRepository.PtTerm.Module = CmbModule.SelectedValue.ToString();
        _purchaseBillingTermRepository.PtTerm.PT_Name = TxtDescription.Text.Trim();
        _purchaseBillingTermRepository.PtTerm.PT_Type = CmbType.SelectedValue.ToString();
        _purchaseBillingTermRepository.PtTerm.Ledger = _ledgerId;
        _purchaseBillingTermRepository.PtTerm.PT_Basis = CmbBasis.SelectedValue.ToString();
        _purchaseBillingTermRepository.PtTerm.PT_Sign = CmbSign.Text.Trim();
        _purchaseBillingTermRepository.PtTerm.PT_Condition = CmbCondition.SelectedValue.ToString();
        _purchaseBillingTermRepository.PtTerm.PT_Rate = TxtRate.GetDecimal();
        _purchaseBillingTermRepository.PtTerm.PT_Branch = ObjGlobal.SysBranchId;
        _purchaseBillingTermRepository.PtTerm.PT_CompanyUnit = ObjGlobal.SysCompanyUnitId;
        _purchaseBillingTermRepository.PtTerm.PT_Profitability = ChkStockValuation.Checked;
        _purchaseBillingTermRepository.PtTerm.PT_Supess = ChkSupress.Checked;
        _purchaseBillingTermRepository.PtTerm.PT_Status = ChkActive.Checked;
        _purchaseBillingTermRepository.PtTerm.EnterBy = ObjGlobal.LogInUser;
        _purchaseBillingTermRepository.PtTerm.EnterDate = DateTime.Now;
        _purchaseBillingTermRepository.PtTerm.SyncRowVersion = _purchaseBillingTermRepository.PtTerm.SyncRowVersion.ReturnSyncRowNo("PT_TERM", _termId.ToString());
        return _purchaseBillingTermRepository.SavePurchaseTerm(_actionTag);
    }

    private void SetGridDataToText()
    {
        using var dt = _purchaseBillingTermRepository.GetMasterPurchaseTermList(_actionTag, "", true, _termId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        _termId = dt.Rows[0]["PT_ID"].GetInt();
        TxtDescription.Text = dt.Rows[0]["PT_Name"].ToString();
        TxtCode.Text = dt.Rows[0]["Order_No"].ToString();
        CmbModule.Text = dt.Rows[0]["Module"].ToString();
        _ledgerId = dt.Rows[0]["GLID"].GetLong();
        TxtLedger.Text = dt.Rows[0]["GLName"].ToString();

        var value = dt.Rows[0]["PT_TypeDesc"].ToString();
        CmbType.SelectedIndex = CmbType.FindStringExact(value); //value.Equals("A") ? 1 : value.Equals("R") ? 2 : 0;

        value = dt.Rows[0]["PT_BasisValue"].ToString();
        CmbBasis.SelectedIndex = CmbBasis.FindStringExact(value);  //value.Equals("Q") ? 1 : 0;

        value = dt.Rows[0]["PT_Sign"].ToString();
        CmbSign.SelectedIndex = value.Equals("-") ? 1 : 0;

        value = dt.Rows[0]["PT_ConditionDesc"].ToString();
        CmbCondition.SelectedIndex = CmbCondition.FindStringExact(value);

        TxtRate.Text = dt.Rows[0]["PT_Rate"].GetDecimalString();
        ChkStockValuation.Checked = dt.Rows[0]["PT_Profitability"].GetBool();
        ChkSupress.Checked = dt.Rows[0]["PT_Supess"].GetBool();
        ChkActive.Checked = dt.Rows[0]["PT_Status"].GetBool();

        var isUsed = _purchaseBillingTermRepository.IsBillingTermUsedOrNot("PB", _termId);
        CmbModule.Enabled = !isUsed;
        CmbType.Enabled = !isUsed;
        CmbBasis.Enabled = !isUsed;
        CmbCondition.Enabled = !isUsed;
        CmbSign.Enabled = !isUsed;
    }

    private void BindTermData(string term)
    {
        CmbModule.BindTermModule(term);
        CmbType.BindTermType();
        CmbBasis.BindTermBasis();
        CmbCondition.BindTermCondition();
    }

    private bool IsFormValid()
    {
        if (_actionTag.IsBlankOrEmpty() || TxtDescription.IsBlankOrEmpty() || TxtCode.IsBlankOrEmpty())
        {
            return false;
        }
        if (CmbModule.Text.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"BILLING MODULE NAME IS REQUIRED..!!");
            CmbModule.Focus();
            return false;
        }

        if (CmbType.Text.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"BILLING TYPE IS REQUIRED..!!");
            CmbType.Focus();
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning(@"BILLING DESCRIPTION IS REQUIRED..!!");
            TxtDescription.Focus();
            return false;
        }

        if (TxtLedger.IsBlankOrEmpty() || _ledgerId is 0)
        {
            CustomMessageBox.Warning(@"BILLING LEDGER NAME IS REQUIRED..!!");
            TxtDescription.Focus();
            return false;
        }

        if (!_actionTag.Equals("SAVE") && _termId is 0)
        {
            CustomMessageBox.Warning(@"BILLING TERM IS INVALID NAME IS REQUIRED..!!");
            TxtDescription.Focus();
            return false;
        }

        return true;
    }
    private async void GetAndSaveUnSynchronizedPurchaseBillingTerm()
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
            GetUrl = @$"{_configParams.Model.Item2}PurchaseBillingTerm/GetPurchaseBillingTermByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}PurchaseBillingTerm/InsertPurchaseBillingTermList",
            UpdateUrl = @$"{_configParams.Model.Item2}PurchaseBillingTerm/UpdatePurchaseBillingTerm"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var purchaseTermRepo = DataSyncProviderFactory.GetRepository<PT_Term>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new purchase billing term data
        var pullResponse = await _purchaseBillingTermRepository.PullSalesBillingTermServerToClientByRowCounts(purchaseTermRepo, 1);
        SplashScreenManager.CloseForm();
        // push all new purchase billing term data
        var sqlglQuery = _purchaseBillingTermRepository.GetPurchaseTermScript();
        var queryResponse = await QueryUtils.GetListAsync<PT_Term>(sqlglQuery);
        var glList = queryResponse.List.ToList();
        if (glList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await purchaseTermRepo.PushNewListAsync(glList);
            SplashScreenManager.CloseForm();
        }
    }
    #endregion

    // OBJECT FOR THIS FORM

    #region --------------- Class ---------------

    public string PurchaseTermDesc = string.Empty;
    private int _termId;
    private long _ledgerId;
    private readonly bool _isZoom;
    private string _actionTag;
    private readonly IPurchaseBillingTermRepository _purchaseBillingTermRepository;
    private IMasterSetup _setup;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion


}