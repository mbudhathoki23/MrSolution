using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmGeneralLedger : MrForm
{
    // ACCOUNT GENERAL LEDGER EVENTS

    #region -------------- FROM --------------

    public FrmGeneralLedger(string category = "Other", bool zoom = false)
    {
        InitializeComponent();
        _category = category;
        _isZoom = zoom;
        _ledgerRepository = new GeneralLedgerRepository();
        _setup = new ClsMasterSetup();
        _ledgerRepository.BindBalanceType(CmbCreditType);
        _ledgerRepository.BindLedgerType(CmbCategory);
        CmbCategory.SelectedIndex = 3;
        CmbCreditType.SelectedIndex = 0;

        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmGeneralLedger_Load(object sender, EventArgs e)
    {
        if (_isZoom)
        {
            CmbCategory.SelectedIndex = CmbCategory.FindString(_category.GetUpper());
        }
        ClearControl();
        EnableControl();
        groupBox2.Focus();
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedGeneralLedgers);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmGeneralLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("LEDGER") == DialogResult.Yes)
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

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedGeneralLedgers);
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
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsFormValid())
        {
            if (SaveGeneralLedger() != 0)
            {
                if (_isZoom)
                {
                    LedgerDesc = TxtDescription.Text.Trim();
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess($@"{TxtDescription.Text.GetUpper()}", "GENERAL LEDGER", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                CustomMessageBox.ErrorMessage($@"DATA {_actionTag} UN-SUCCESSFULLY...!!");
                TxtDescription.Focus();
            }
        }
        else
        {
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.Trim() == string.Empty)
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
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

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            if (TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage($"LEDGER DESCRIPTION IS REQUIRED FOR {_actionTag}");
                return;
            }
        }
        if (TxtDescription.IsValueExits() && _actionTag.ToUpper() is "SAVE" && TxtDescription.Enabled)
        {
            TxtShortName.Text = TxtShortName.IsBlankOrEmpty() ? TxtShortName.GenerateShortName("GENERALLEDGER", TxtDescription.Text, "GLCode") : TxtShortName.Text;
            CheckShortName(TxtShortName.Text, "GLCode");
            TxtAccountingCode.Text = TxtAccountingCode.GenerateAccountingCode(_groupId);
        }
        if (TxtDescription.IsDuplicate("GlName", LedgerId.ToString(), _actionTag, "GENERALLEDGER"))
        {
            CustomMessageBox.Warning("LEDGER DESCRIPTION IS ALREADY EXITS");
            TxtDescription.Focus();
        }
    }

    private void TxtDescription_TextChanged(object sender, EventArgs e)
    {
        if (_actionTag == "DELETE")
        {
            return;
        }
        TxtGroup.Enabled = !string.IsNullOrEmpty(TxtDescription.Text);
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (description.IsValueExits())
        {
            (TxtDescription.Text, LedgerId) = (description, id);
            if (_actionTag != "SAVE")
            {
                TxtDescription.ReadOnly = false;
                SetGridDataToText(LedgerId);
            }
        }
        TxtDescription.Focus();
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
                TxtShortName.WarningMessage("LEDGER SHORT NAME IS REQUIRED..!!");
            }
        }
    }

    private void TxtAccountingCode_KeyDown(object sender, KeyEventArgs e)
    {

    }

    private void TxtAcGroup_TextChanged(object sender, EventArgs e)
    {
        _subGrpId = 0;
        TxtSubGroup.Clear();
    }

    private void TxtAcGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateAccountGroup(true);
            if (description.IsValueExits())
            {
                TxtGroup.Text = description;
                _groupId = id;
            }
            TxtGroup.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnAcGroup_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtGroup.Text.IsBlankOrEmpty())
            {
                BtnAcGroup_Click(sender, e);
            }
            else
            {
                Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGroup, BtnAcGroup);
        }
    }

    private void TxtAcGroup_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty() && TxtGroup.Enabled && _actionTag.ActionValid())
        {
            if (TxtDescription.ValidControl(ActiveControl))
            {
                MessageBox.Show(@"GROUP NAME CAN'T BE LEFT BLANK..!!", ObjGlobal.Caption);
                TxtGroup.Focus();
                return;
            }
        }
        if (TxtGroup.IsValueExits() && TxtGroup.Enabled)
        {
            var dt = GetConnection.SelectDataTableQuery($"SELECT *  FROM AMS.AccountGroup Where GrpName='{TxtGroup.Text}'");
            if (dt.Rows.Count > 0)
            {
                _groupId = dt.Rows[0]["GrpId"].GetInt();
                if (dt.Rows[0]["PrimaryGrp"].ToString() is "Balance Sheet" or "BS")
                {
                    TxtOPDebit.Enabled = TxtOPDebit.Visible = true;
                    TxtOPCredit.Enabled = TxtOPCredit.Visible = true;
                }
                else
                {
                    TxtOPDebit.Enabled = TxtOPDebit.Visible = false;
                    TxtOPCredit.Enabled = TxtOPCredit.Visible = false;
                }
            }
            else
            {
                MessageBox.Show(@"DOES NOT EXITS ACCOUNT GROUP.!!", ObjGlobal.Caption);
                TxtGroup.Focus();
            }
        }
    }

    private void BtnAcGroup_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAccountGroupList(_actionTag);
        if (description.IsValueExits())
        {
            TxtGroup.Text = description;
            _groupId = id;
            TxtAccountingCode.Text = TxtAccountingCode.GenerateAccountingCode(_groupId);
        }
        TxtGroup.Focus();
    }

    private void BtnAcSubGroup_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAccountSubGroupLists(_actionTag, _groupId);
        if (description.IsValueExits())
        {
            TxtSubGroup.Text = description;
            _subGrpId = id;
        }
        TxtSubGroup.Focus();
    }

    private void BtnArea_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAreaList(_actionTag);
        if (description.IsValueExits())
        {
            TxtArea.Text = description;
            _areaId = id;
        }
        TxtArea.Focus();
    }

    private void CmbCategory_Enter(object sender, EventArgs e)
    {
        if (_actionTag == "DELETE" || _actionTag.IsBlankOrEmpty())
        {
            return;
        }

        var isEnable = _categoryStrings.Contains(CmbCategory.GetUpper());
        TxtArea.Enabled = isEnable;
        TxtPanNo.Enabled = isEnable || CmbCategory.Text.GetUpper().Equals("BANK");
        TxtAgent.Enabled = isEnable;
        TxtCurrency.Enabled = isEnable;
        TxtArea.Enabled = isEnable;
        TxtAgent.Enabled = isEnable;
        TxtAddress.Enabled = isEnable;
        TxtOwner.Enabled = TxtPhone1.Enabled = TxtPhone2.Enabled = isEnable;
        TxtPhoneNo.Enabled = TxtEmail.Enabled = TxtScheme.Enabled = isEnable;
    }

    private void CbType_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    private void CbType_Validating(object sender, CancelEventArgs e)
    {
        if (!TxtGroup.Text.IsValueExits()) return;
        var dt = GetConnection.SelectDataTableQuery($"Select GrpType  FROM AMS.AccountGroup Where GrpName ='{TxtGroup.Text}'");
        if (dt.Rows.Count <= 0)
        {
            return;
        }
        _grpType = Convert.ToString(dt.Rows[0]["GrpType"].ToString());
        switch (_grpType)
        {
            case "Assets" when CmbCategory.Text is "Vendor":
            case "A" when CmbCategory.Text is "Vendor":
                {
                    this.NotifyValidationError(TxtGroup, "YOUR GROUP IS LIABILITIES SO YOU CANNOT USE VENDOR CATEGORY BCZ IT IS ASSETS");
                    break;
                }
            case "Liabilities" when CmbCategory.Text is "Customer":
            case "L" when CmbCategory.Text is "Customer":
                {
                    this.NotifyValidationError(TxtGroup, "YOUR GROUP IS LIABILITIES SO YOU CANNOT USE VENDOR CATEGORY BCZ IT IS ASSETS");
                    TxtGroup.Focus();
                    break;
                }
        }
    }

    private void CmbCategory_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void CmbCategory_SelectionChangeCommitted(object sender, EventArgs e)
    {
        TxtPanNo.MaxLength = CmbCategory.Text is "Bank" ? 250 : 9;
        lblPan.Text = CmbCategory.Text is "Bank" ? "A/c No" : "TPAN No.";
        CmbCategory_Enter(sender, e);
    }

    private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        CmbCategory_SelectionChangeCommitted(sender, e);
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        CheckShortName(TxtShortName.Text, "GLCode");
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }

    private void TxtPanNo_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtPanNo.Text.Trim()))
        {
            if (ClsMasterSetup.CheckValidPan(_actionTag, TxtPanNo.Text.Trim(), LedgerId))
            {
                if (CustomMessageBox.Question(@"PAN IS ALREADY EXITS ON GENERAL LEDGER -- DO YOU WANT TO CONTINUE..??") == DialogResult.No)
                {
                    TxtPanNo.Focus();
                }
            }
        }
    }

    private void TxtAcSubGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateAccountSubGroup(true);
            if (description.IsValueExits())
            {
                TxtSubGroup.Text = description;
                _subGrpId = id;
            }
            TxtSubGroup.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnAcSubGroup_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)(Keys.Enter)));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubGroup, BtnAcSubGroup);
        }
    }

    private void TxtAgent_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAgent_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateAgent(true);
            if (description.IsValueExits())
            {
                TxtAgent.Text = description;
                _agentId = id;
            }
            TxtAgent.Focus();
        }
        else if (e.KeyData is Keys.Delete or Keys.Back)
        {
            _agentId = 0;
            TxtAgent.Clear();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAgent, BtnAgent);
        }
    }

    private void TxtArea_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnArea_Click(sender, e);
        }

        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateArea(true);
            if (description.IsValueExits())
            {
                TxtArea.Text = description;
                _areaId = id;
            }
            TxtArea.Focus();
        }
        else if (e.KeyData is Keys.Delete or Keys.Back)
        {
            _areaId = 0;
            TxtArea.Clear();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }

        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtArea, BtnArea);
        }
    }

    private void CmbCreditType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Space)
        {
            SendKeys.Send("{F4}");
            return;
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void BtnAgent_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetAgentList(_actionTag);
        if (description.IsValueExits())
        {
            TxtAgent.Text = description;
            _agentId = id;
        }
        else
        {
            TxtAgent.Enabled = true;
        }
        TxtAgent.Focus();
    }

    private void TxtOPDebit_Leave(object sender, EventArgs e)
    {
        if (TxtOPDebit.GetDecimal() > 0)
        {
            TxtOPCredit.Clear();
        }
        TxtOPDebit.Text = TxtOPDebit.GetDecimalString();
    }

    private void TxtOPDebit_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtOPDebit.GetDecimal() > 0)
            {
                TxtOPCredit.Enabled = false;
                TxtOPCredit.Clear();
            }
            else
            {
                TxtOPCredit.Enabled = true;
            }
        }
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar is not '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtOPCredit_Leave(object sender, EventArgs e)
    {
        if (TxtOPCredit.GetDecimal() > 0)
        {
            TxtOPDebit.Clear();
        }
        TxtOPCredit.Text = TxtOPCredit.GetDecimalString();
    }

    private void TxtOPCredit_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtOPCredit.GetDecimal() > 0)
            {
                TxtOPDebit.Enabled = false;
                TxtOPDebit.Clear();
            }
            else
            {
                TxtOPDebit.Enabled = true;
            }
        }
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar is not '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetGeneralLedger(_actionTag, string.Empty, "LIST");
        if (TxtDescription.Enabled)
        {
            TxtDescription.Focus();
        }
    }

    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCurrency_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id, exchangeRate) = GetMasterList.CreateCurrency(true);
            if (!description.IsValueExits())
            {
                return;
            }
            TxtCurrency.Text = description;
            _currencyId = id;
            TxtCurrency.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCurrency, BtnCurrency);
        }
    }

    private void BtnCurrency_Click(object sender, EventArgs e)
    {
        var (description, id, exchangeRate) = GetMasterList.GetCurrencyList(_actionTag);
        if (description.IsValueExits())
        {
            TxtCurrency.Text = description;
            _currencyId = id;
        }
        TxtCurrency.Focus();
    }

    private void BtnPdfAttachment_Click(object sender, EventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            Filter = @"PDF Files (*.pdf)|*.pdf"
        };
    }

    private void TxtScheme_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnScheme_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtScheme, BtnScheme);
        }
    }

    private void BtnScheme_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetSchemeList(_actionTag);
        if (description.IsValueExits())
        {
            TxtScheme.Text = description;
            _schemaId = id;
        }
        TxtScheme.Focus();
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
    {
        AttachPdfFile();
    }

    #endregion -------------- From --------------

    // METHOD FOR THIS FORM

    #region -------------- METHOD --------------
    private bool IsFormValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (_actionTag is "DELETE")
        {
            if (TxtDescription.IsBlankOrEmpty() || LedgerId is 0)
            {
                TxtDescription.WarningMessage(@"GENERAL LEDGER NAME IS REQUIRED..!!");
                return false;
            }

            if (ObjGlobal.SysConfirmDelete)
            {
                return CustomMessageBox.Question("DO YOU WANT TO DELETE SELECTED LEDGER..??") is DialogResult.Yes;
            }
        }
        else
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage(@"GENERAL LEDGER NAME IS REQUIRED..!!");
                return false;
            }

            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage(@"GENERAL LEDGER SHORT NAME IS REQUIRED..!!");
                return false;
            }

            if (TxtAccountingCode.IsBlankOrEmpty())
            {
                TxtAccountingCode.WarningMessage(@$"ACCOUNTING CODE IS REQUIRED..!!");
                return false;
            }

            if (TxtGroup.IsBlankOrEmpty())
            {
                TxtGroup.WarningMessage(@"ACCOUNT GROUP IS REQUIRED...!!");
                return false;
            }

            if (_actionTag != "DELETE")
            {
                CheckShortName(TxtShortName.Text, "GLCode");
                CheckShortName(TxtAccountingCode.Text, "ACCode");
            }
            if (LedgerId is 0 && _actionTag is "UPDATE")
            {
                TxtDescription.WarningMessage("GENERAL LEDGER IS INVALID....!!");
                return false;
            }
            if (_groupId is 0)
            {
                TxtDescription.WarningMessage("GENERAL LEDGER ACCOUNT GROUP TAG IS INVALID....!!");
                return false;
            }
            if (ObjGlobal.SysConfirmUpdate)
            {
                return CustomMessageBox.Question("DO YOU WANT TO UPDATE SELECTED LEDGER..??") is DialogResult.Yes;
            }
            if (ObjGlobal.SysConfirmSave)
            {
                return CustomMessageBox.Question("DO YOU WANT TO SAVE SELECTED LEDGER..??") is DialogResult.Yes;
            }
        }


        return true;
    }
    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TabLedger.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtDescription.Enabled = TabLedger.Enabled;
        BtnDescription.Enabled = TabLedger.Enabled;
        TxtGroup.Enabled = BtnAcGroup.Enabled = isEnable;
        TxtSubGroup.Enabled = BtnAcSubGroup.Enabled = isEnable;
        CmbCategory.Enabled = isEnable;
        TxtShortName.Enabled = TxtAccountingCode.Enabled = isEnable;

        TxtPhone2.Enabled = _categoryStrings.Contains(CmbCategory.Text.GetUpper()) && isEnable;
        TxtPhoneNo.Enabled = TxtPhone1.Enabled = TxtPhone2.Enabled;
        TxtPanNo.Enabled = TxtCurrency.Enabled = TxtPhone2.Enabled;

        TxtAgent.Enabled = BtnAgent.Enabled = TxtPhone2.Enabled;
        TxtArea.Enabled = BtnArea.Enabled = TxtPhone2.Enabled;
        TxtCreditLimit.Enabled = TxtCreditDays.Enabled = TxtInterestRate.Enabled = TxtPhone2.Enabled;
        TxtAddress.Enabled = TxtPhone2.Enabled;
        CmbCreditType.Enabled = TxtPhone2.Enabled;
        TxtOPDebit.Enabled = TxtOPCredit.Enabled = isEnable;
        TxtScheme.Enabled = isEnable;
        BtnCancel.Enabled = BtnSave.Enabled = TabLedger.Enabled;
        ChkActive.Enabled = !string.IsNullOrEmpty(_actionTag) && _actionTag == "UPDATE";
    }
    private void SetGridDataToText(long ledgerId)
    {
        using var dt = _ledgerRepository.GetMasterGeneralLedger(_actionTag, ledgerId);
        if (dt.Rows.Count <= 0) return;
        TxtDescription.Text = dt.Rows[0]["GLName"].GetTrimApostrophe();
        TxtShortName.Text = dt.Rows[0]["GLCode"].ToString();
        TxtAccountingCode.Text = dt.Rows[0]["ACCode"].ToString();
        CmbCategory.SelectedIndex = 0;
        CmbCategory.SelectedIndex = CmbCategory.FindString(dt.Rows[0]["GLType"].ToString());
        _groupId = dt.Rows[0]["GrpId"].GetInt();
        TxtGroup.Text = dt.Rows[0]["GrpName"].ToString();
        _subGrpId = dt.Rows[0]["SubGrpId"].GetInt();
        TxtSubGroup.Text = dt.Rows[0]["SubGrpName"].ToString();
        TxtPanNo.Text = dt.Rows[0]["PanNo"].ToString();
        _areaId = dt.Rows[0]["AreaId"].GetInt();
        TxtArea.Text = dt.Rows[0]["AreaName"].ToString();
        _agentId = dt.Rows[0]["AgentId"].GetInt();
        TxtAgent.Text = dt.Rows[0]["AgentName"].ToString();
        _currencyId = dt.Rows[0]["CurrId"].GetInt();
        TxtCurrency.Text = dt.Rows[0]["Ccode"].ToString();
        TxtCreditDays.Text = dt.Rows[0]["CrDays"].ToString();
        TxtCreditLimit.Text = dt.Rows[0]["CrLimit"].GetDecimalString();
        CmbCreditType.Text = dt.Rows[0]["CrType"].ToString();
        TxtInterestRate.Text = dt.Rows[0]["IntRate"].GetDecimalString();
        TxtAddress.Text = dt.Rows[0]["GLAddress"].GetTrimApostrophe();
        TxtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
        TxtPhone1.Text = dt.Rows[0]["LandLineNo"].ToString();
        TxtOwner.Text = dt.Rows[0]["OwnerName"].ToString();
        TxtPhone2.Text = dt.Rows[0]["OwnerNumber"].ToString();
        TxtScheme.Text = dt.Rows[0]["Scheme"].ToString();
        TxtEmail.Text = dt.Rows[0]["Email"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
        if (_actionTag == "UPDATE")
        {
            TxtGroup.Focus();
        }
        else
        {
            BtnSave.Focus();
        }
    }
    private void ClearControl()
    {
        Text = _actionTag.IsValueExits()
            ? $"GENERAL LEDGER SETUP DETAILS [{_actionTag}]"
            : "GENERAL LEDGER SETUP DETAILS";
        lblPan.Text = CmbCategory.Text is "Bank" ? "A/c No" : "TPAN No.";
        TabLedger.SelectedIndex = 0;
        LedgerId = 0;
        LedgerDesc = string.Empty;
        TxtDescription.Clear();
        _subGrpId = 0;
        TxtSubGroup.Clear();

        TxtShortName.Clear();
        TxtAccountingCode.Clear();
        TxtPhoneNo.Clear();
        TxtPhone1.Clear();
        TxtPhone2.Clear();

        TxtPanNo.Clear();
        TxtAgent.Clear();
        TxtArea.Clear();
        TxtCurrency.Clear();

        TxtCreditLimit.Text = TxtCreditDays.Text = TxtInterestRate.Text = string.Empty;
        CmbCreditType.SelectedIndex = 0;
        TxtAddress.Clear();
        TxtOwner.Clear();
        TxtPhone1.Clear();
        TxtPhone2.Clear();
        TxtPhoneNo.Clear();
        TxtEmail.Clear();
        TxtScheme.Clear();
        TxtOPDebit.Text = TxtOPCredit.Text = TxtCreditLimit.Text = string.Empty;
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
    }
    private int SaveGeneralLedger()
    {
        LedgerId = _actionTag is "SAVE" ? LedgerId.ReturnMaxLongId("GENERALLEDGER", LedgerId.ToString()) : LedgerId;
        _ledgerRepository.ObjGeneralLedger.GLID = LedgerId;
        _ledgerRepository.ObjGeneralLedger.GLName = TxtDescription.Text;
        _ledgerRepository.ObjGeneralLedger.GLCode = TxtShortName.Text;
        if (TxtAccountingCode.Text.IsBlankOrEmpty())
        {
            TxtAccountingCode.Text = _setup.GenerateLedgerAccountNumber(TxtGroup.Text).ToString();
        }

        _ledgerRepository.ObjGeneralLedger.ACCode = TxtAccountingCode.Text;
        _ledgerRepository.ObjGeneralLedger.GLType = CmbCategory.SelectedValue.ToString();
        _ledgerRepository.ObjGeneralLedger.GrpId = _groupId;
        _ledgerRepository.ObjGeneralLedger.SubGrpId = _subGrpId == 0 ? null : _subGrpId;
        _ledgerRepository.ObjGeneralLedger.PanNo = TxtPanNo.Text;
        _ledgerRepository.ObjGeneralLedger.AreaId = _areaId == 0 ? null : _areaId;
        _ledgerRepository.ObjGeneralLedger.AgentId = _agentId == 0 ? null : _agentId;
        _ledgerRepository.ObjGeneralLedger.CurrId = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _ledgerRepository.ObjGeneralLedger.CrDays = TxtCreditDays.Text.GetDecimal();
        _ledgerRepository.ObjGeneralLedger.CrLimit = TxtCreditLimit.Text.GetDecimal();
        _ledgerRepository.ObjGeneralLedger.CrTYpe = CmbCreditType.SelectedValue.ToString();
        _ledgerRepository.ObjGeneralLedger.IntRate = TxtInterestRate.Text.GetDecimal();
        _ledgerRepository.ObjGeneralLedger.Branch_ID = ObjGlobal.SysBranchId;
        _ledgerRepository.ObjGeneralLedger.EnterBy = ObjGlobal.LogInUser;
        _ledgerRepository.ObjGeneralLedger.EnterDate = DateTime.Now;
        _ledgerRepository.ObjGeneralLedger.GLAddress = TxtAddress.Text;
        _ledgerRepository.ObjGeneralLedger.PhoneNo = TxtPhoneNo.Text;
        _ledgerRepository.ObjGeneralLedger.LandLineNo = TxtPhone1.Text;
        _ledgerRepository.ObjGeneralLedger.OwnerName = TxtOwner.Text;
        _ledgerRepository.ObjGeneralLedger.OwnerNumber = TxtPhone2.Text;
        _ledgerRepository.ObjGeneralLedger.Scheme = _schemaId == 0 ? null : _schemaId;
        _ledgerRepository.ObjGeneralLedger.Email = TxtEmail.Text;
        _ledgerRepository.ObjGeneralLedger.Status = ChkActive.Checked;
        _ledgerRepository.ObjGeneralLedger.PrimaryGroupId = 0;
        _ledgerRepository.ObjGeneralLedger.PrimarySubGroupId = 0;
        _ledgerRepository.ObjGeneralLedger.NepaliDesc = TxtDescription.Text;
        _ledgerRepository.ObjGeneralLedger.SyncRowVersion =
            _ledgerRepository.ObjGeneralLedger.SyncRowVersion.ReturnSyncRowNo("GENERALLEDGER", LedgerId.ToString());
        return _ledgerRepository.SaveGeneralLedger(_actionTag);
    }
    private void AttachPdfFile()
    {
        try
        {
            var fs = new FileStream(TxtScheme.Text, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            br.ReadBytes((int)fs.Length);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }
    private void CheckShortName(string shortName, string filterColumn)
    {
        while (true)
        {
            const int blankCharLength = 5;
            var table = shortName.IsDuplicate(filterColumn, LedgerId.ToString(), _actionTag, @"GENERALLEDGER");
            if (!table)
            {
                return;
            }
            var getNumber = string.Empty;
            getNumber = TxtShortName.Text.Where(char.IsDigit).Aggregate(getNumber, (current, t) => current + t);
            var result = TxtDescription.GetTrimReplace().Substring(0, 2).GetUpper();
            var lastNo = getNumber.GetInt();
            lastNo += 1;
            var maxLength = blankCharLength - lastNo.ToString().Length;
            var blankEnumerable = Enumerable.Repeat(0, maxLength);
            var generate = string.Join(string.Empty, blankEnumerable) + lastNo;
            TxtShortName.Text = result + generate;
            shortName = TxtShortName.Text.Trim();
        }
    }
    private async void GetAndSaveUnSynchronizedGeneralLedgers()
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
                GetUrl = @$"{_configParams.Model.Item2}GeneralLedger/GetGeneralLedgersByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}GeneralLedger/InsertGeneralLedgerList",
                UpdateUrl = @$"{_configParams.Model.Item2}GeneralLedger/UpdateGeneralLedger"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var generalLedgerRepo = DataSyncProviderFactory.GetRepository<GeneralLedger>(_injectData);
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            // pull all new account data
            var pullResponse = await _ledgerRepository.PullGeneralLedgersServerToClientByRowCount(generalLedgerRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new account data
            var sqlglQuery = _ledgerRepository.GetGeneralLedgerScript();
            var queryResponse = await QueryUtils.GetListAsync<GeneralLedger>(sqlglQuery);
            var glList = queryResponse.List.ToList();
            if (glList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await generalLedgerRepo.PushNewListAsync(glList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    #endregion -------------- Method --------------

    // OBJECT FOR THIS FORM

    #region --------------  GLOBAL CLASS --------------

    public string LedgerDesc = string.Empty;
    public long LedgerId;
    private int _groupId;
    private int _subGrpId;
    private int _areaId;
    private int _agentId;
    private int _currencyId;
    private int _schemaId;
    private readonly bool _isZoom;
    private readonly string _category;
    private string _actionTag = string.Empty;
    private string _grpType;
    private readonly IGeneralLedgerRepository _ledgerRepository;
    private IMasterSetup _setup;
    private readonly string[] _categoryStrings = ["CUSTOMER", "VENDOR", "BOTH"];

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;



    #endregion --------------  Global Class --------------
}