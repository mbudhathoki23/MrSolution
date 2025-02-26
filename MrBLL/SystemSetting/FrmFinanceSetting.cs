using DatabaseModule.CloudSync;
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
using MrDAL.Models.Common;
using MrDAL.SystemSetting;
using MrDAL.SystemSetting.Interface;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmFinanceSetting : MrForm
{
    // FINANCE CONFIG

    #region --------------- FINANACE SETTING ---------------

    public FrmFinanceSetting()
    {
        InitializeComponent();
        EnableDisable();
        BindPCreditDaysWarning();
        _financeSettingRepository = new FinanceSettingRepository();
    }

    private void FrmFinanceSetting_Load(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        FillSystemConfiguration();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedFinanceSetting();
            });
        }
    }

    private void FrmFinanceSetting_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Escape) return;
        if (MessageBox.Show(@"ARE YOU SURE WANT TO CLOSE FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
            DialogResult.Yes) Close();
    }

    private void TextBoxCtrl_Leave(object sender, EventArgs e)
    {
        //BackColor = Color.AliceBlue;
    }

    private void TextBoxCtrl_Enter(object sender, EventArgs e)
    {
        //BackColor = Color.Honeydew;
    }

    private void Global_EnterKey(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void ChkEnableAgent_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkAgentEnable.Checked)
            ChkAgentMandatory.Enabled = true;
        else
            ChkAgentMandatory.Enabled = ChkAgentMandatory.Checked = false;
    }

    private void ChkEnableDepartment_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDepartmentEnable.Checked)
            ChkDepartmentMandatory.Enabled = true;
        else
            ChkDepartmentMandatory.Enabled = ChkDepartmentMandatory.Checked = false;
    }

    private void ChkEnableRemarks_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkRemarksEnable.Checked)
            ChkRemarksMandatory.Enabled = true;
        else
            ChkRemarksMandatory.Enabled = ChkRemarksMandatory.Checked = false;
    }

    private void ChkEnableCurrency_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkCurrencyEnable.Enabled)
            ChkCurrencyMandatory.Enabled = true;
        else
            ChkCurrencyMandatory.Enabled = ChkCurrencyMandatory.Checked = false;
    }

    private void ChkEnableSubledger_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkSubledgerEnable.Enabled)
            ChkSubledgerMandatory.Enabled = true;
        else
            ChkSubledgerMandatory.Enabled = ChkSubledgerMandatory.Checked = false;
    }

    private void ChkEnableDepartmentItem_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDepartmentItemEnable.Enabled)
            ChkDepartmentItemMandatory.Enabled = true;
        else
            ChkDepartmentItemMandatory.Enabled = ChkDepartmentItemMandatory.Checked = false;
    }

    private void BtnProfitLoss_Click(object sender, EventArgs e)
    {
        (TxtProfitLoss.Text, _profitLossLedgerId) = GetMasterList.GetGeneralLedger("SAVE");
    }

    private void BtnCashLedger_Click(object sender, EventArgs e)
    {
        (TxtCashBook.Text, _cashLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "CASH");
    }

    private void BtnVatLedger_Click(object sender, EventArgs e)
    {
        (TxtVATAccount.Text, _vatLedgerId) = GetMasterList.GetGeneralLedger("SAVE");
    }

    private void BtnProvisionBank_Click(object sender, EventArgs e)
    {
        (TxtPDCBankAccount.Text, _bankLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
    }

    private void TxtProfitLoss_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnProfitLoss_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtProfitLoss.IsBlankOrEmpty() || _profitLossLedgerId is 0)
                this.NotifyValidationError(TxtProfitLoss, "PROFIT & LOSS LEDGER SHOULD BE TAG IN SETTING..!!");
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtProfitLoss, BtnProfitLoss);
        }
    }

    private void TxtCAshBookLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCashLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtCashBook.IsBlankOrEmpty() || _cashLedgerId is 0)
                this.NotifyValidationError(TxtCashBook, "CASH LEDGER SHOULD BE TAG IN SETTING..!!");
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCashBook, BtnCashLedger);
        }
    }

    private void TxtVatLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVatLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtVATAccount.IsBlankOrEmpty() || _vatLedgerId is 0)
                this.NotifyValidationError(TxtVATAccount, "VALUE ADDED TAX LEDGER SHOULD BE TAG IN SETTING..!!");
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtVATAccount, BtnVatLedger);
        }
    }

    private void TxtPDCBankAccount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnProvisionBank_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtPDCBankAccount.IsBlankOrEmpty() || _bankLedgerId is 0)
                this.NotifyValidationError(TxtPDCBankAccount, "BANK LEDGER SHOULD BE TAG IN SETTING..!!");
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPDCBankAccount, BtnProvisionBank);
        }
    }

    private void BtnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;
        if (SaveFinanceSetting() != 0) ObjGlobal.FillSystemConFiguration();
        Close();
    }

    private void Btn_Save_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;
        if (SaveFinanceSetting() != 0) ObjGlobal.FillSystemConFiguration();
    }

    #endregion --------------- FINANACE SETTING ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private int SaveFinanceSetting()
    {
        _financeSettingRepository.VmFinance.FinId = 1;
        _financeSettingRepository.VmFinance.ProfiLossId = _profitLossLedgerId;
        _financeSettingRepository.VmFinance.CashId = _cashLedgerId;
        _financeSettingRepository.VmFinance.VATLedgerId = _vatLedgerId;
        _financeSettingRepository.VmFinance.PDCBankLedgerId = _bankLedgerId;
        _financeSettingRepository.VmFinance.ShortNameWisTransaction = ChkEnableShortNameWise.Checked;
        _financeSettingRepository.VmFinance.WarngNegativeTransaction = ChkWarnNegativeTransaction.Checked;
        _financeSettingRepository.VmFinance.NegativeTransaction = CmbNegativeTransaction.SelectedValue.GetString();
        _financeSettingRepository.VmFinance.VoucherDate = ChkEnableVoucherDate.Checked;
        _financeSettingRepository.VmFinance.AgentEnable = ChkAgentEnable.Checked;
        _financeSettingRepository.VmFinance.AgentMandetory = ChkAgentMandatory.Checked;
        _financeSettingRepository.VmFinance.DepartmentEnable = ChkDepartmentEnable.Checked;
        _financeSettingRepository.VmFinance.DepartmentMandetory = ChkDepartmentMandatory.Checked;
        _financeSettingRepository.VmFinance.RemarksEnable = ChkRemarksEnable.Checked;
        _financeSettingRepository.VmFinance.RemarksMandetory = ChkRemarksMandatory.Checked;
        _financeSettingRepository.VmFinance.NarrationMandetory = ChkNarrationMandatory.Checked;
        _financeSettingRepository.VmFinance.CurrencyEnable = ChkCurrencyEnable.Checked;
        _financeSettingRepository.VmFinance.CurrencyMandetory = ChkCurrencyMandatory.Checked;
        _financeSettingRepository.VmFinance.SubledgerEnable = ChkSubledgerEnable.Checked;
        _financeSettingRepository.VmFinance.SubledgerMandetory = ChkSubledgerMandatory.Checked;
        _financeSettingRepository.VmFinance.DetailsClassEnable = ChkDepartmentItemEnable.Checked;
        _financeSettingRepository.VmFinance.DetailsClassMandetory = ChkDepartmentItemMandatory.Checked;
        return _financeSettingRepository.SaveFinanceSetting("");
    }

    private void FillSystemConfiguration()
    {
        var dtSystem = _setup.GetFinanceSetting();
        if (dtSystem.Rows.Count <= 0) return;
        foreach (DataRow dr in dtSystem.Rows)
        {
            _profitLossLedgerId = dr["ProfiLossId"].GetLong();

            TxtProfitLoss.Text = dr["PlLedger"].ToString();
            _cashLedgerId = dr["CashId"].GetInt();
            TxtCashBook.Text = dr["CashLedger"].ToString();
            _vatLedgerId = dr["VATLedgerId"].GetInt();
            TxtVATAccount.Text = dr["VatLedger"].ToString();
            _bankLedgerId = dr["PDCBankLedgerId"].GetInt();
            TxtPDCBankAccount.Text = dr["PDCLedger"].ToString();
            CmbNegativeTransaction.SelectedItem = dr["NegativeTransaction"].GetInt();

            ChkEnableShortNameWise.Checked = dr["ShortNameWisTransaction"].GetBool();
            ChkWarnNegativeTransaction.Checked = dr["WarngNegativeTransaction"].GetBool();
            ChkEnableVoucherDate.Checked = dr["VoucherDate"].GetBool();
            ChkAgentEnable.Checked = dr["AgentEnable"].GetBool();
            ChkAgentMandatory.Checked = dr["AgentMandetory"].GetBool();
            ChkDepartmentEnable.Checked = dr["DepartmentEnable"].GetBool();
            ChkDepartmentMandatory.Checked = dr["DepartmentMandetory"].GetBool();
            ChkRemarksEnable.Checked = dr["RemarksEnable"].GetBool();
            ChkRemarksMandatory.Checked = dr["RemarksMandetory"].GetBool();
            ChkNarrationMandatory.Checked = dr["NarrationMandetory"].GetBool();
            ChkCurrencyEnable.Checked = dr["CurrencyEnable"].GetBool();
            ChkCurrencyMandatory.Checked = dr["CurrencyMandetory"].GetBool();
            ChkSubledgerEnable.Checked = dr["SubledgerEnable"].GetBool();
            ChkSubledgerMandatory.Checked = dr["SubledgerMandetory"].GetBool();
            ChkDepartmentItemEnable.Checked = dr["DetailsClassEnable"].GetBool();
            ChkDepartmentItemMandatory.Checked = dr["DetailsClassMandetory"].GetBool();
        }
    }

    private void EnableDisable()
    {
        ChkAgentMandatory.Enabled = false;
        ChkDepartmentMandatory.Enabled = false;
        ChkRemarksMandatory.Enabled = false;
        ChkCurrencyMandatory.Enabled = false;
        ChkSubledgerMandatory.Enabled = false;
        ChkDepartmentItemMandatory.Enabled = false;
        ChkNarrationMandatory.Enabled = false;
    }

    internal void BindPCreditDaysWarning()
    {
        var list = new List<ValueModel<string, int>>
        {
            new("Block", 1),
            new("Ignore", 2),
            new("Warning", 3)
        };
        if (list.Count <= 0) return;
        CmbNegativeTransaction.DataSource = list;
        CmbNegativeTransaction.DisplayMember = "Item1";
        CmbNegativeTransaction.ValueMember = "Item2";
        CmbNegativeTransaction.SelectedIndex = 2;
    }

    internal bool IsValidForm()
    {
        if (_profitLossLedgerId is 0 || TxtProfitLoss.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtProfitLoss, "PROFIT AND LOSS LEDGER IS NOT TAG IN SETTING..!!");
            return false;
        }

        if (_cashLedgerId is 0 || TxtCashBook.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCashBook, "CASH LEDGER IS NOT TAG IN SETTING..!!");
            return false;
        }

        if (_vatLedgerId is 0 || TxtVATAccount.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVATAccount, "VALUE ADDED TAX LEDGER IS NOT TAG IN SETTING..!!");
            return false;
        }

        if (_bankLedgerId is 0 || TxtPDCBankAccount.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtPDCBankAccount, "BANK LEDGER IS NOT TAG IN SETTING..!!");
            return false;
        }

        return true;
    }
    private async void GetAndSaveUnsynchronizedFinanceSetting()
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
                GetUrl = @$"{_configParams.Model.Item2}FinanceSetting/GetFinanceSettingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}FinanceSetting/InsertFinanceSettingList",
                UpdateUrl = @$"{_configParams.Model.Item2}FinanceSetting/UpdateFinanceSetting"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var settingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.SystemSetting.FinanceSetting>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new FinanceSetting data
            var pullResponse = await _financeSettingRepository.PullFinanceSettingServerToClientByRowCounts(settingRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new FinanceSetting data
            var sqlCrQuery = _financeSettingRepository.GetFinanceScript();
            var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.SystemSetting.FinanceSetting>(sqlCrQuery);
            var curList = queryResponse.List.ToList();
            if (curList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await settingRepo.PushNewListAsync(curList);
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

    #region --------------- Global ---------------

    private long _vatLedgerId;
    private long _profitLossLedgerId;
    private long _cashLedgerId;
    private long _bankLedgerId;
    private string _actionTag = string.Empty;
    private readonly ISetup _setup = new ClsSetup();
    private IMasterSetup _master = new ClsMasterSetup();
    private readonly IFinanceSettingRepository _financeSettingRepository;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();

    #endregion --------------- Global ---------------
}