using DatabaseModule.CloudSync;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
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

public partial class FrmSalesSetting : MrForm
{
    // SALES SETTING

    #region --------------- Form ---------------

    public FrmSalesSetting()
    {
        InitializeComponent();
        BindPCreditBalanceWarning();
        BindPCreditDaysWarning();
        FillSalesSetting();
        EnableControl();
        _salesSettingRepository = new SalesSettingRepository();
    }

    private void FrmSalesSetting_Load(object sender, EventArgs e)
    {
        TxtSalesLedger.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedSalesSetting();
            });
        }
    }

    private void BtnSalesInvoice_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
        if (id > 0)
        {
            TxtSalesLedger.Text = description;
            _salesLedgerId = id;
        }
        TxtSalesLedger.Focus();
    }

    private void TxtSalesAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesInvoice_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var result = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (result.id > 0)
            {
                _salesLedgerId = result.id;
                TxtSalesLedger.Text = result.description;
            }

            TxtSalesLedger.Focus();
        }
        else if (e.KeyCode is Keys.Delete or Keys.Back)
        {
            _salesLedgerId = 0;
            TxtSalesLedger.Clear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtSalesLedger.IsBlankOrEmpty())
            {
                TxtSalesLedger.WarningMessage("SALES LEDGER IS REQUIRED FOR LEDGER POSTING..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesLedger, BtnSalesInvoice);
        }
    }

    private void TxtSalesReturnAc_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesReturn_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                _returnLedgerId = id;
                TxtReturnLedger.Text = description;
            }

            TxtSalesLedger.Focus();
        }
        else if (e.KeyData is Keys.Delete or Keys.Back)
        {
            _returnLedgerId = 0;
            TxtReturnLedger.Clear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtReturnLedger.IsBlankOrEmpty())
            {
                TxtReturnLedger.WarningMessage(@"SALES RETURN IS BLANK..!!");
                return;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesLedger, BtnSalesReturn);
        }
    }

    private void TxtSalesVat_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesVAT_Click(sender, e);
        }
        else if (e.KeyData is Keys.Delete || e.KeyCode is Keys.Back)
        {
            _vatTermId = 0;
            TxtSalesVat.Clear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (_vatTermId == 0)
            {
                CustomMessageBox.Warning("PLEASE TAG VAT TERM FOR VAT CALCULATION..!!");
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesVat, BtnSalesVAT);
        }
    }

    private void BtnSaveClosed_Click(object sender, EventArgs e)
    {
        if (!IsValidForm())
        {
            return;
        }
        if (SaveSalesSetting() != 0)
        {
            ObjGlobal.FillSystemConFiguration();
        }
        Close();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsValidForm()) return;
        if (SaveSalesSetting() != 0) ObjGlobal.FillSystemConFiguration();
    }

    private void ChkSEnableOrder_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkOrderEnable.Checked)
            ChkOrderMandatory.Enabled = true;
        else
            ChkOrderMandatory.Enabled = ChkOrderMandatory.Checked = false;
    }

    private void ChkDepartmentEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkDepartmentEnable.Checked)
            ChkDeparmentMandatory.Enabled = true;
        else
            ChkDeparmentMandatory.Enabled = ChkDeparmentMandatory.Checked = false;
    }

    private void ChkGodownEnable_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkGodownEnable.Checked)
            ChkGodownMandatory.Enabled = false;
        else
            ChkGodownMandatory.Checked = ChkGodownMandatory.Enabled = false;
    }

    private void ChkSEnableChallan_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkChallanEnable.Checked)
            ChkChallanMandatory.Enabled = true;
        else
            ChkChallanMandatory.Enabled = ChkChallanMandatory.Checked = false;
    }

    private void ChkSEnableCurrency_CheckStateChanged(object sender, EventArgs e)
    {
        if (ChkCurrencyEnable.Checked)
            ChkCurrencyMandatory.Enabled = true;
        else
            ChkCurrencyMandatory.Enabled = ChkCurrencyMandatory.Checked = false;
    }

    private void ChkSEnableAgent_CheckStateChanged(object sender, EventArgs e)
    {
        ChkAgentMandatory.Enabled = ChkAgentEnable.Checked switch
        {
            true => true,
            _ => ChkAgentMandatory.Checked = false
        };
    }

    private void ChkSEnableRemarks_CheckStateChanged(object sender, EventArgs e)
    {
        ChkRemarksMandatory.Enabled = ChkRemarksEnable.Checked switch
        {
            true => true,
            _ => ChkRemarksMandatory.Checked = false
        };
    }

    private void ChkSEnableSubLedger_CheckStateChanged(object sender, EventArgs e)
    {
        ChkSubLedgerMandatory.Enabled = ChkSubLedgerEnable.Checked switch
        {
            true => true,
            _ => ChkSubLedgerMandatory.Checked = false
        };
    }

    private void ChkSDispatchOrder_CheckStateChanged(object sender, EventArgs e)
    {
        ChkDispatchOrderMandatory.Enabled = ChkDispatchOrder.Checked switch
        {
            true => true,
            _ => ChkDispatchOrderMandatory.Checked = false
        };
    }

    private void ChkSEnableQuotation_CheckedChanged(object sender, EventArgs e)
    {
        ChkQuotationMandatory.Enabled = ChkQuotationEnable.Checked switch
        {
            true => true,
            _ => ChkQuotationMandatory.Checked = false
        };
    }

    private void BtnSalesReturn_Click(object sender, EventArgs e)
    {
        (TxtReturnLedger.Text, _returnLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
    }

    private void BtnSalesVAT_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetSalesTermList("SAVE", "PB");
        if (result.id > 0)
        {
            TxtSalesVat.Text = result.description;
            _vatTermId = result.id;
        }

        TxtSalesVat.Focus();
    }

    private void TextBoxCtrl_Leave(object sender, EventArgs e)
    {
    }

    private void TextBoxCtrl_Enter(object sender, EventArgs e)
    {
    }

    public void Global_EnterKey(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnProductDiscountTerm_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetSalesTermList("SAVE", "P");
        if (result.id > 0)
        {
            TxtProductDiscountTerm.Text = result.description;
            _productDiscountTermId = result.id;
        }

        TxtProductDiscountTerm.Focus();
    }

    private void BtnSalesDiscountTerm_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetSalesTermList("SAVE", "");
        if (result.id > 0)
        {
            TxtSalesDiscountTerm.Text = result.description;
            _salesDiscountTermId = result.id;
        }
        TxtSalesDiscountTerm.Focus();
    }

    private void TxtProductDiscountTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnProductDiscountTerm_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Delete or Keys.Back)
        {
            _productDiscountTermId = 0;
            TxtProductDiscountTerm.Clear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.SoftwareModule.Equals("POS") || ObjGlobal.SoftwareModule.Equals("RESTRO"))
            {
                if (_productDiscountTermId == 0)
                {
                    CustomMessageBox.Warning("PLEASE TAG SALES PRODUCT TERM FOR PRODUCT WISE DISCOUNT..!!");
                }
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtProductDiscountTerm, BtnProcuctDiscountTerm);
        }
    }

    private void TxtSalesDiscountTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesDiscountTerm_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Delete or Keys.Back)
        {
            _salesDiscountTermId = 0;
            TxtSalesDiscountTerm.Clear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.SoftwareModule.Equals("POS") || ObjGlobal.SoftwareModule.Equals("RESTO"))
            {
                if (_salesDiscountTermId > 0)
                {
                    CustomMessageBox.Warning("PLEASE TAG SALES DISCOUNT TO DISCOUNT IN INBOX..!!");
                }
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesDiscountTerm, BtnSalesDiscountTerm);
        }
    }

    private void BtnSalesServiceTerm_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetSalesTermList("SAVE", "");
        if (result.id > 0)
        {
            TxtSalesServiceTerm.Text = result.description;
            _salesServiceChargeTermId = result.id;
        }
        TxtSalesServiceTerm.Focus();
    }

    private void TxtSalesServiceTerm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesServiceTerm_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Delete or Keys.Back)
        {
            _salesServiceChargeTermId = 0;
            TxtSalesServiceTerm.Clear();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.SoftwareModule.Equals("RESTRO"))
            {
                if (_salesServiceChargeTermId == 0)
                {
                    CustomMessageBox.Warning("PLEASE TAG SALES SERVICE CHARGE FOR ADDITIONAL..!!");
                }
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesServiceTerm, BtnSalesServiceTerm);
        }
    }

    #endregion --------------- Form ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    internal bool IsValidForm()
    {
        if (_salesLedgerId is 0 || TxtSalesLedger.IsBlankOrEmpty())
        {
            TxtSalesLedger.WarningMessage("SALES LEDGER NOT TAG IN SALES SETTING");
            return false;
        }

        if (_returnLedgerId is 0 || TxtReturnLedger.IsBlankOrEmpty())
        {
            TxtReturnLedger.WarningMessage("SALES RETURN LEDGER NOT TAG IN SALES SETTING");
            return false;
        }

        if (ObjGlobal.IsIrdRegister)
        {
            if (_vatTermId is 0 || TxtSalesVat.IsBlankOrEmpty())
            {
                var question = CustomMessageBox.Question("VAT TERM IS NOT TAG.. DO YOU WANT TO CONTINUE..??");
                return question == DialogResult.Yes;
            }
        }

        return true;
    }

    internal void EnableControl()
    {
        ChkQuotationMandatory.Enabled = false;
        ChkDispatchOrderMandatory.Enabled = false;
        ChkOrderMandatory.Enabled = false;
        ChkChallanMandatory.Enabled = false;
        ChkSubLedgerMandatory.Enabled = false;
        ChkAgentMandatory.Enabled = false;
        ChkDeparmentMandatory.Enabled = false;
        ChkCurrencyMandatory.Enabled = false;
        ChkGodownMandatory.Enabled = false;
    }

    internal int SaveSalesSetting()
    {
        _salesSettingRepository.VmSales.SalesId = 1;
        _salesSettingRepository.VmSales.SBLedgerId = _salesLedgerId;
        _salesSettingRepository.VmSales.SRLedgerId = _returnLedgerId;
        _salesSettingRepository.VmSales.SBVatTerm = _vatTermId;
        _salesSettingRepository.VmSales.SBDiscountTerm = _salesDiscountTermId;
        _salesSettingRepository.VmSales.SBProductDiscountTerm = _productDiscountTermId;
        _salesSettingRepository.VmSales.SBAdditionalTerm = 0;
        _salesSettingRepository.VmSales.SBServiceCharge = _salesServiceChargeTermId;
        _salesSettingRepository.VmSales.SBDateChange = ChkDateChange.Checked;
        _salesSettingRepository.VmSales.SBCreditDays = CmbCreditDaysWarning.Text.Substring(0, 1);
        _salesSettingRepository.VmSales.SBCreditLimit = CmbCreditBalanceWarning.Text.Substring(0, 1);
        _salesSettingRepository.VmSales.SBCarryRate = ChkCarryRate.Checked;
        _salesSettingRepository.VmSales.SBChangeRate = ChkChangeRate.Checked;
        _salesSettingRepository.VmSales.SBLastRate = ChkLastRate.Checked;
        _salesSettingRepository.VmSales.SBQuotationEnable = ChkQuotationEnable.Checked;
        _salesSettingRepository.VmSales.SBQuotationMandetory = ChkQuotationMandatory.Checked;
        _salesSettingRepository.VmSales.SBDispatchOrderEnable = ChkDispatchOrder.Checked;
        _salesSettingRepository.VmSales.SBDispatchMandetory = ChkDispatchOrderMandatory.Checked;
        _salesSettingRepository.VmSales.SOEnable = ChkOrderEnable.Checked;
        _salesSettingRepository.VmSales.SOMandetory = ChkOrderMandatory.Checked;
        _salesSettingRepository.VmSales.SCEnable = ChkChallanEnable.Checked;
        _salesSettingRepository.VmSales.SCMandetory = ChkChallanMandatory.Checked;
        _salesSettingRepository.VmSales.SBSublegerEnable = ChkSubLedgerEnable.Checked;
        _salesSettingRepository.VmSales.SBSubledgerMandetory = ChkSubLedgerMandatory.Checked;
        _salesSettingRepository.VmSales.SBAgentEnable = ChkAgentEnable.Checked;
        _salesSettingRepository.VmSales.SBAgentMandetory = ChkAgentMandatory.Checked;
        _salesSettingRepository.VmSales.SBDepartmentEnable = ChkDepartmentEnable.Checked;
        _salesSettingRepository.VmSales.SBDepartmentMandetory = ChkDeparmentMandatory.Checked;
        _salesSettingRepository.VmSales.SBCurrencyEnable = ChkCurrencyEnable.Checked;
        _salesSettingRepository.VmSales.SBCurrencyMandetory = ChkCurrencyMandatory.Checked;
        _salesSettingRepository.VmSales.SBCurrencyRateChange = false;
        _salesSettingRepository.VmSales.SBGodownEnable = ChkGodownEnable.Checked;
        _salesSettingRepository.VmSales.SBGodownMandetory = ChkGodownMandatory.Checked;
        _salesSettingRepository.VmSales.SBAlternetUnitEnable = ChkAltUnitEnable.Checked;
        _salesSettingRepository.VmSales.SBIndent = ChkIndentEnable.Checked;
        _salesSettingRepository.VmSales.SBNarration = ChkDescriptionEnable.Checked;
        _salesSettingRepository.VmSales.SBBasicAmount = ChkBasicAmtEnable.Checked;
        _salesSettingRepository.VmSales.SBAviableStock = ChkAvailabeStock.Checked;
        _salesSettingRepository.VmSales.SBReturnValue = ChkStockValueinSalesReturn.Checked;
        _salesSettingRepository.VmSales.PartyInfo = ChkPartyInfo.Checked;
        _salesSettingRepository.VmSales.SBRemarksEnable = ChkRemarksEnable.Checked;
        _salesSettingRepository.VmSales.SBRemarksMandatory = ChkRemarksMandatory.Checked;
        return _salesSettingRepository.SaveSalesSetting("");
    }

    internal void FillSalesSetting()
    {
        var dtSales = _setup.GetSalesSetting();
        foreach (DataRow dr in dtSales.Rows)
        {
            TxtSalesLedger.Text = dr["SBLedger"].ToString();
            _salesLedgerId = dr["SBLedgerId"].GetLong();
            TxtReturnLedger.Text = dr["SRLedger"].ToString();
            _returnLedgerId = dr["SRLedgerId"].GetLong();
            TxtSalesVat.Text = dr["VatTerm"].ToString();
            _vatTermId = dr["SBVatTerm"].GetInt();
            TxtProductDiscountTerm.Text = dr["ProductDiscountTerm"].ToString();
            _productDiscountTermId = dr["SBProductDiscountTerm"].GetInt();
            TxtSalesDiscountTerm.Text = dr["DiscountTerm"].ToString();
            _salesDiscountTermId = dr["SBDiscountTerm"].GetInt();
            TxtSalesServiceTerm.Text = dr["ServiceChargeTerm"].ToString();
            _salesServiceChargeTermId = dr["SBServiceCharge"].GetInt();

            var value = dr["SBCreditDays"].ToString();
            CmbCreditDaysWarning.SelectedIndex = value.Equals("B") ? 0 : value.Equals("W") ? 2 : 1;

            value = dr["SBCreditLimit"].ToString();
            CmbCreditBalanceWarning.SelectedIndex = value.Equals("B") ? 0 : value.Equals("W") ? 2 : 1;

            ChkCarryRate.Checked = dr["SBCarryRate"].GetBool();
            ChkLastRate.Checked = dr["SBLastRate"].GetBool();
            ChkChangeRate.Checked = dr["SBChangeRate"].GetBool();
            ChkDateChange.Checked = dr["SBDateChange"].GetBool();
            ChkAltUnitEnable.Checked = dr["SBAlternetUnitEnable"].GetBool();
            ChkIndentEnable.Checked = dr["SBIndent"].GetBool();
            ChkRemarksEnable.Checked = dr["SBNarration"].GetBool();
            ChkBasicAmtEnable.Checked = dr["SBBasicAmount"].GetBool();
            ChkAvailabeStock.Checked = dr["SBAviableStock"].GetBool();
            ChkStockValueinSalesReturn.Checked = dr["SBReturnValue"].GetBool();
            ChkPartyInfo.Checked = dr["PartyInfo"].GetBool();

            ChkQuotationEnable.Checked = dr["SBQuotationEnable"].GetBool();
            ChkQuotationMandatory.Checked = dr["SBQuotationMandetory"].GetBool();
            ChkDispatchOrder.Checked = dr["SBDispatchOrderEnable"].GetBool();
            ChkDispatchOrderMandatory.Checked = dr["SBDispatchMandetory"].GetBool();
            ChkOrderEnable.Checked = dr["SOEnable"].GetBool();
            ChkOrderMandatory.Checked = dr["SOMandetory"].GetBool();
            ChkChallanEnable.Checked = dr["SCEnable"].GetBool();
            ChkChallanMandatory.Checked = dr["SCMandetory"].GetBool();
            ChkSubLedgerEnable.Checked = dr["SBSublegerEnable"].GetBool();
            ChkSubLedgerMandatory.Checked = dr["SBSubledgerMandetory"].GetBool();
            ChkAgentEnable.Checked = dr["SBAgentEnable"].GetBool();
            ChkAgentMandatory.Checked = dr["SBAgentMandetory"].GetBool();
            ChkDepartmentEnable.Checked = dr["SBDepartmentEnable"].GetBool();
            ChkDeparmentMandatory.Checked = dr["SBDepartmentMandetory"].GetBool();
            ChkCurrencyEnable.Checked = dr["SBCurrencyEnable"].GetBool();
            ChkCurrencyMandatory.Checked = dr["SBCurrencyMandetory"].GetBool();
            ChkGodownEnable.Checked = dr["SBGodownEnable"].GetBool();
            ChkGodownMandatory.Checked = dr["SBGodownMandetory"].GetBool();
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
        CmbCreditDaysWarning.DataSource = list;
        CmbCreditDaysWarning.DisplayMember = "Item1";
        CmbCreditDaysWarning.ValueMember = "Item2";
        CmbCreditDaysWarning.SelectedIndex = 2;
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
        CmbCreditBalanceWarning.DataSource = list;
        CmbCreditBalanceWarning.DisplayMember = "Item1";
        CmbCreditBalanceWarning.ValueMember = "Item2";
        CmbCreditBalanceWarning.SelectedIndex = 2;
    }
    private async void GetAndSaveUnsynchronizedSalesSetting()
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
                GetUrl = @$"{_configParams.Model.Item2}SalesSetting/GetSalesSettingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}SalesSetting/InsertSalesSettingList",
                UpdateUrl = @$"{_configParams.Model.Item2}SalesSetting/UpdateSalesSetting"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var settingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.SystemSetting.SalesSetting>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new table master data
            var pullResponse = await _salesSettingRepository.PullSalesSettingServerToClientByRowCounts(settingRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new table master data
            var sqlCrQuery = _salesSettingRepository.GetSalesSettingScript();
            var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.SystemSetting.SalesSetting>(sqlCrQuery);
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

    //OBJECT FOR THIS FORM

    #region --------------- Global ---------------

    private int _vatTermId;
    private int _salesDiscountTermId;
    private int _productDiscountTermId;
    private int _salesServiceChargeTermId;

    private long _salesLedgerId;
    private long _returnLedgerId;

    private readonly ISetup _setup = new ClsSetup();
    private readonly ISalesSettingRepository _salesSettingRepository;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
    #endregion --------------- Global ---------------
}