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

public partial class FrmPurchaseSetting : MrForm
{
    // PURCHASE SETTING

    #region --------------- PURCHASE SETTING  ---------------

    public FrmPurchaseSetting()
    {
        InitializeComponent();
        BindPCreditBalanceWarning();
        BindPCreditDaysWarning();
        EnableControl();
        FillPurchaseSetting();
        _purchaseSettingRepository = new PurchaseSettingRepository();
    }

    private void FrmPurchaseSetting_Load(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedPurchaseSetting();
            });
        }
    }

    private void FrmPurchaseSetting_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show(@" ARE YOU SURE WANT TO CLOSE THE PURCHASE SETTING FORM..??", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                Close();
    }

    private void BtnPurchaseInvoice_Click(object sender, EventArgs e)
    {
        (TxtPurchaseAc.Text, _purchaseLedgerId) = GetMasterList.GetGeneralLedger("SAVE");
    }

    private void TxtPurchaseAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseInvoice_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtPurchaseAc.Text.Trim()) && TxtPurchaseAc.Enabled is true &&
                TxtPurchaseAc.Focused is true)
            {
                MessageBox.Show(@"PURCHASE INVOICE IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtPurchaseAc.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseAc, BtnPurchaseInvoice);
        }
    }

    private void TxtPurchaseAc_Leave(object sender, EventArgs e)
    {
        if (!TxtPurchaseAc.Focused || !string.IsNullOrEmpty(TxtPurchaseAc.Text.Trim())) return;
        MessageBox.Show(@"PURCHASE LEDGER NOT TAG ON PURCHASE SETTING..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
        TxtPurchaseAc.Focus();
    }

    private void TxtPurchaseReturnAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseReturn_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtPurchaseReturnAc.Text.Trim()) && TxtPurchaseReturnAc.Enabled is true &&
                TxtPurchaseReturnAc.Focused is true)
            {
                MessageBox.Show(@"PURCHASE RETURN IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtPurchaseReturnAc.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseReturnAc, BtnPurchaseReturn);
        }
    }

    private void TxtPurchaseReturnAc_Leave(object sender, EventArgs e)
    {
    }

    private void TxtPurchaseVat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseVAT_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtPurchaseVat.Text.Trim()) && TxtPurchaseVat.Enabled is true &&
                TxtPurchaseVat.Focused is true)
            {
                MessageBox.Show(@"VAT IS BLANK ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtPurchaseVat.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseAc, BtnPurchaseVAT);
        }
    }

    private void TxtPurchaseAddVat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnPurchaseAdditionalVAT_Click(sender, e);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPurchaseAddVat, BtnPurchaseAdditionalVAT);
    }

    private void TxtProDiscount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnPurchaseProDiscount_Click(sender, e);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtProDiscount, BtnPurchaseProDiscount);
    }

    private void TxtDiscount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnPurchaseDiscount_Click(sender, e);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDiscount, BtnPurchaseDiscount);
    }

    private void ChkDepartmentEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkDepartmentEnable.Checked)
            ChkDepartmentEnable.Enabled = true;
        else
            ChkDepartmentMandatory.Enabled = ChkDepartmentMandatory.Checked = false;
    }

    private void ChkGodownEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkGodownEnable.Checked)
            ChkGodownMandatory.Enabled = true;
        else
            ChkGodownMandatory.Enabled = ChkGodownMandatory.Checked = false;
    }

    private void ChkChallanEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkChallanEnable.Checked)
            ChkChallanMandatory.Enabled = true;
        else
            ChkChallanMandatory.Enabled = ChkChallanMandatory.Checked = false;
    }

    private void ChkCurrencyEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkCurrencyEnable.Checked)
            ChkCurrencyMandatory.Enabled = true;
        else
            ChkCurrencyMandatory.Enabled = ChkCurrencyMandatory.Checked = false;
    }

    private void Chk_PSubLedger_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkSubLedgerEnable.Checked)
            ChkSubLedgerMandatory.Enabled = true;
        else
            ChkSubLedgerMandatory.Enabled = ChkSubLedgerMandatory.Checked = false;
    }

    private void Chk_PAgent_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkAgentEnable.Checked)
            ChkAgentMandatory.Enabled = true;
        else
            ChkAgentMandatory.Enabled = ChkAgentMandatory.Checked = false;
    }

    private void Chk_POrder_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkOrderEnable.Checked)
            ChkOrderMandatory.Enabled = true;
        else
            ChkOrderMandatory.Enabled = ChkOrderMandatory.Checked = false;
    }

    private void BtnPurchaseReturn_Click(object sender, EventArgs e)
    {
        (TxtPurchaseReturnAc.Text, _returnLedgerId) = GetMasterList.GetGeneralLedger("SAVE");
    }

    private void BtnPurchaseVAT_Click(object sender, EventArgs e)
    {
        (TxtPurchaseVat.Text, _vatTermId) = GetMasterList.GetPurchaseTermList("SAVE", "");
    }

    private void BtnPurchaseAdditionalVAT_Click(object sender, EventArgs e)
    {
        (TxtPurchaseAddVat.Text, _additionalVatTermId) = GetMasterList.GetPurchaseTermList("SAVE", "A");
    }

    private void BtnPurchaseProDiscount_Click(object sender, EventArgs e)
    {
        (TxtProDiscount.Text, _productDiscountId) = GetMasterList.GetPurchaseTermList("SAVE", "P");
    }

    private void BtnPurchaseDiscount_Click(object sender, EventArgs e)
    {
        (TxtDiscount.Text, _billWiseDiscountId) = GetMasterList.GetPurchaseTermList("SAVE", "");
    }

    private void TextBoxCtrl_Leave(object sender, EventArgs e)
    {
        // BackColor = Color.AliceBlue;
    }

    private void TextBoxCtrl_Enter(object sender, EventArgs e)
    {
        // BackColor = Color.Honeydew;
    }

    public void Global_EnterKey(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void ChkRemarksEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkRemarksEnable.Checked)
            ChkRemarksMandatory.Enabled = true;
        else
            ChkRemarksMandatory.Enabled = ChkRemarksMandatory.Checked = false;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;
        if (SavePurchaseSetting() != 0) ObjGlobal.FillSystemConFiguration();
    }

    private void BtnSaveClosed_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;
        if (SavePurchaseSetting() != 0) ObjGlobal.FillSystemConFiguration();
        Close();
    }

    #endregion --------------- PURCHASE SETTING  ---------------

    //METHOD FOR THIS CLASS

    #region --------------- METHOD ---------------

    internal int SavePurchaseSetting()
    {
        _purchaseSettingRepository.VmPurchase.PurId = 1;
        _purchaseSettingRepository.VmPurchase.PBLedgerId = _purchaseLedgerId;
        _purchaseSettingRepository.VmPurchase.PRLedgerId = _returnLedgerId;
        _purchaseSettingRepository.VmPurchase.PBVatTerm = _vatTermId;
        _purchaseSettingRepository.VmPurchase.PBDiscountTerm = _billWiseDiscountId;
        _purchaseSettingRepository.VmPurchase.PBProductDiscountTerm = _productDiscountId;
        _purchaseSettingRepository.VmPurchase.PBAdditionalTerm = _additionalVatTermId;
        _purchaseSettingRepository.VmPurchase.PBDateChange = ChkDateChangeEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBCreditDays = CmbCreditDays.Text.Substring(0, 1);
        _purchaseSettingRepository.VmPurchase.PBCreditLimit = CmbCreditBalance.Text.Substring(0, 1);
        _purchaseSettingRepository.VmPurchase.PBCarryRate = ChkCarryRate.Checked;
        _purchaseSettingRepository.VmPurchase.PBChangeRate = ChkChangeRate.Checked;
        _purchaseSettingRepository.VmPurchase.PBLastRate = ChkLastRate.Checked;
        _purchaseSettingRepository.VmPurchase.POEnable = ChkOrderEnable.Checked;
        _purchaseSettingRepository.VmPurchase.POMandetory = ChkOrderMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PCEnable = ChkChallanEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PCMandetory = ChkChallanMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PBSublegerEnable = ChkSubLedgerEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBSubledgerMandetory = ChkSubLedgerMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PBAgentEnable = ChkAgentEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBAgentMandetory = ChkAgentMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PBDepartmentEnable = ChkDepartmentEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBDepartmentMandetory = ChkDepartmentMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PBCurrencyEnable = ChkCurrencyEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBCurrencyMandetory = ChkCurrencyMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PBCurrencyRateChange = ChkChangeCurrencyRate.Checked;
        _purchaseSettingRepository.VmPurchase.PBGodownEnable = ChkGodownEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBGodownMandetory = ChkGodownMandatory.Checked;
        _purchaseSettingRepository.VmPurchase.PBAlternetUnitEnable = ChkAltUnitEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBIndent = ChkIndentEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBNarration = ChkNarrationEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBBasicAmount = ChkBasicAmountEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBRemarksEnable = ChkRemarksEnable.Checked;
        _purchaseSettingRepository.VmPurchase.PBRemarksMandatory = ChkRemarksMandatory.Checked;
        return _purchaseSettingRepository.SavePurchaseSetting("");
    }

    internal void EnableControl()
    {
        ChkOrderMandatory.Enabled = false;
        ChkChallanMandatory.Enabled = false;
        ChkSubLedgerMandatory.Enabled = false;
        ChkAgentMandatory.Enabled = false;
        ChkDepartmentMandatory.Enabled = false;
        ChkCurrencyMandatory.Enabled = false;
        ChkGodownMandatory.Enabled = false;
        ChkRemarksMandatory.Enabled = false;
    }

    internal void FillPurchaseSetting()
    {
        var dtPurchase = _setup.GetPurchaseSetting();
        if (dtPurchase.Rows.Count <= 0) return;
        foreach (DataRow dr in dtPurchase.Rows)
        {
            _purchaseLedgerId = dr["PBLedgerId"].GetInt();
            TxtPurchaseAc.Text = dr["PBLedger"].ToString();

            _returnLedgerId = dr["PRLedgerId"].GetInt();
            TxtPurchaseReturnAc.Text = dr["PRLedger"].ToString();

            _vatTermId = dr["PBVatTerm"].GetInt();
            TxtPurchaseVat.Text = dr["VatTerm"].ToString();

            _additionalVatTermId = dr["PBAdditionalTerm"].GetInt();
            TxtPurchaseAddVat.Text = dr["AdditionalVatTerm"].ToString();

            _productDiscountId = dr["PBProductDiscountTerm"].GetInt();
            TxtProDiscount.Text = dr["ProductDiscountTerm"].ToString();

            _billWiseDiscountId = dr["PBDiscountTerm"].GetInt();
            TxtDiscount.Text = dr["DiscountTerm"].ToString();

            var value = dr["PBCreditDays"].ToString();
            CmbCreditDays.SelectedIndex = value.Equals("B") ? 0 : value.Equals("W") ? 2 : 1;

            value = dr["PBCreditLimit"].ToString();
            CmbCreditDays.SelectedIndex = value.Equals("B") ? 0 : value.Equals("W") ? 2 : 1;

            ChkDateChangeEnable.Checked = dr["PBDateChange"].GetBool();
            ChkCarryRate.Checked = dr["PBCarryRate"].GetBool();
            ChkChangeRate.Checked = dr["PBChangeRate"].GetBool();
            ChkLastRate.Checked = dr["PBLastRate"].GetBool();
            ChkOrderEnable.Checked = dr["POEnable"].GetBool();
            ChkOrderMandatory.Checked = dr["POMandetory"].GetBool();
            ChkChallanEnable.Checked = dr["PCEnable"].GetBool();
            ChkChallanMandatory.Checked = dr["PCMandetory"].GetBool();
            ChkSubLedgerEnable.Checked = dr["PBSublegerEnable"].GetBool();
            ChkSubLedgerMandatory.Checked = dr["PBSubledgerMandetory"].GetBool();
            ChkAgentEnable.Checked = dr["PBAgentEnable"].GetBool();
            ChkAgentMandatory.Checked = dr["PBAgentMandetory"].GetBool();
            ChkDepartmentEnable.Checked = dr["PBDepartmentEnable"].GetBool();
            ChkDepartmentMandatory.Checked = dr["PBDepartmentMandetory"].GetBool();
            ChkCurrencyEnable.Checked = dr["PBCurrencyEnable"].GetBool();
            ChkCurrencyMandatory.Checked = dr["PBCurrencyMandetory"].GetBool();
            ChkChangeCurrencyRate.Checked = dr["PBCurrencyRateChange"].GetBool();
            ChkGodownEnable.Checked = dr["PBGodownEnable"].GetBool();
            ChkGodownMandatory.Checked = dr["PBGodownMandetory"].GetBool();
            ChkAltUnitEnable.Checked = dr["PBAlternetUnitEnable"].GetBool();
            ChkIndentEnable.Checked = dr["PBIndent"].GetBool();
            ChkNarrationEnable.Checked = dr["PBNarration"].GetBool();
            ChkBasicAmountEnable.Checked = dr["PBBasicAmount"].GetBool();
        }
    }

    internal void BindPCreditBalanceWarning()
    {
        var list = new List<ValueModel<string, int>>
        {
            new("Block", 1),
            new("Ignore", 2),
            new("Warning", 3)
        };
        if (list.Count <= 0) return;
        CmbCreditBalance.DataSource = list;
        CmbCreditBalance.DisplayMember = "Item1";
        CmbCreditBalance.ValueMember = "Item2";
        CmbCreditBalance.SelectedIndex = 2;
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
        CmbCreditDays.DataSource = list;
        CmbCreditDays.DisplayMember = "Item1";
        CmbCreditDays.ValueMember = "Item2";
        CmbCreditDays.SelectedIndex = 2;
    }

    internal bool IsValidForm()
    {
        if (_purchaseLedgerId is 0 || TxtPurchaseAc.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtPurchaseAc, "PURCHASE LEDGER NOT TAG ON PURCHASE SETTING..!!");
            return false;
        }

        if (_returnLedgerId is 0 || TxtPurchaseReturnAc.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtPurchaseReturnAc,
                "PURCHASE RETURN LEDGER NOT TAG ON PURCHASE SETTING..!!");
            return false;
        }

        return true;
    }
    private async void GetAndSaveUnsynchronizedPurchaseSetting()
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
                GetUrl = @$"{_configParams.Model.Item2}PurchaseSetting/GetPurchaseSettingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}PurchaseSetting/InsertPurchaseSettingList",
                UpdateUrl = @$"{_configParams.Model.Item2}PurchaseSetting/UpdatePurchaseSetting"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var settingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.SystemSetting.PurchaseSetting>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new table master data
            var pullResponse = await _purchaseSettingRepository.PullPurchaseSettingServerToClientByRowCounts(settingRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new table master data
            var sqlCrQuery = _purchaseSettingRepository.GetPurchaseScript();
            var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.SystemSetting.PurchaseSetting>(sqlCrQuery);
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
    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- Global ---------------

    private int _vatTermId;
    private int _productDiscountId;
    private int _billWiseDiscountId;
    private int _additionalVatTermId;

    private long _purchaseLedgerId;
    private long _returnLedgerId;

    private readonly ISetup _setup = new ClsSetup();
    private readonly IPurchaseSettingRepository _purchaseSettingRepository;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion --------------- Global ---------------
}