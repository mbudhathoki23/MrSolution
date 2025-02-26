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
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmPaymentSetting : MrForm
{
    // PAYMENT SETTING
    public FrmPaymentSetting()
    {
        InitializeComponent();
        _paymentSettingRepository = new PaymentSettingRepository();
    }

    private void FrmPaymentSetting_Load(object sender, EventArgs e)
    {
        FillPaymentSetting();
        TxtCardLedger.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedPaymentSetting();
            });
        }
    }

    private void FrmPaymentSetting_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{Tab}");
        }
    }

    private void TxtCashLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCashLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtCashLedger.Text, _cashLedgerId) = GetMasterList.CreateGeneralLedger("CASH", true);
            TxtCashLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCashLedger, BtnCashLedger);
        }
    }

    private void BtnCashLedger_Click(object sender, EventArgs e)
    {
        (TxtCashLedger.Text, _cashLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "CASH");
        TxtCashLedger.Focus();
    }

    private void TxtCardLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCardLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtCardLedger.Text, _cardLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtCardLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCardLedger, BtnCardLedger);
        }
    }

    private void BtnCardLedger_Click(object sender, EventArgs e)
    {
        (TxtCardLedger.Text, _cardLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
        TxtCardLedger.Focus();
    }

    private void TxtBankLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnBankLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtBankLedger.Text, _bankLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtBankLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBankLedger, BtnBankLedger);
        }
    }

    private void BtnBankLedger_Click(object sender, EventArgs e)
    {
        (TxtBankLedger.Text, _bankLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
    }

    private void TxtPhonePayLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnPhonePayLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtPhonePayLedger.Text, _phonePayLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtPhonePayLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPhonePayLedger, BtnPhonePayLedger);
        }
    }

    private void BtnPhonePayLedger_Click(object sender, EventArgs e)
    {
        (TxtPhonePayLedger.Text, _phonePayLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
        TxtPhonePayLedger.Focus();
    }

    private void TxtEsewaLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnEsewaLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtEsewaLedger.Text, _eSewaLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtEsewaLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtEsewaLedger, BtnEsewaLedger);
        }
    }

    private void BtnEsewaLedger_Click(object sender, EventArgs e)
    {
        (TxtEsewaLedger.Text, _eSewaLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
        TxtEsewaLedger.Focus();
    }

    private void TxtKhaltiLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnKhaltiLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtKhaltiLedger.Text, _khaltiLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtKhaltiLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtKhaltiLedger, BtnKhaltiLedger);
        }
    }

    private void BtnKhaltiLedger_Click(object sender, EventArgs e)
    {
        (TxtKhaltiLedger.Text, _khaltiLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
    }

    private void TxtRemitLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnRemitLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtRemitLedger.Text, _remitLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtRemitLedger, BtnRemitLedger);
        }
    }

    private void BtnRemitLedger_Click(object sender, EventArgs e)
    {
        (TxtRemitLedger.Text, _remitLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
        TxtRemitLedger.Focus();
    }

    private void TxtConnectIPSLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnConnectIpsLedger.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtConnectIPSLedger.Text, _bankLedgerId) = GetMasterList.CreateGeneralLedger("BANK", true);
            TxtConnectIPSLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtConnectIPSLedger, BtnConnectIpsLedger);
        }
    }

    private void BtnConnectIpsLedger_Click(object sender, EventArgs e)
    {
        (TxtConnectIPSLedger.Text, _connectIpsLedgerId) = GetMasterList.GetGeneralLedger("SAVE", "BANK");
        TxtConnectIPSLedger.Focus();
    }

    private void BtnSaveClosed_Click(object sender, EventArgs e)
    {
        var result = ReturnSystemSettingStatus();
        if (result)
        {
            Close();
        }
        return;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        ReturnSystemSettingStatus();
    }

    private void TxtPartialCustomer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnPartialCustomer.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtPartialCustomer.Text, _partialCustomerId) = GetMasterList.CreateGeneralLedger("CUSTOMER", true);
            TxtPartialCustomer.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPartialCustomer, BtnPartialCustomer);
        }
    }

    private void BtnPartialCustomer_Click(object sender, EventArgs e)
    {
        (TxtPartialCustomer.Text, _partialCustomerId) = GetMasterList.GetGeneralLedger("SAVE", "CUSTOMER");
        TxtPartialCustomer.Focus();
    }

    private void TxtGiftVoucher_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnGiftVoucher.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (description.IsValueExits())
            {
                TxtGiftVoucher.Text = description;
                _giftVoucherLedgerId = id;
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGiftVoucher, BtnGiftVoucher);
        }
    }

    private void BtnGiftVoucher_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("SAVE", "OTHER");
        if (description.IsValueExits())
        {
            TxtGiftVoucher.Text = description;
            _giftVoucherLedgerId = id;
        }
        TxtGiftVoucher.Focus();
    }

    // METHOD FOR THIS FORM
    private void FillPaymentSetting()
    {
        var dtPayment = _setup.GetPaymentSetting();
        if (dtPayment.Rows.Count <= 0) return;
        foreach (DataRow dr in dtPayment.Rows)
        {
            _cashLedgerId = dr["CashLedgerId"].GetLong();
            TxtCashLedger.Text = dr["CashLedger"].GetString();
            _cardLedgerId = dr["CardLedgerId"].GetLong();
            TxtCardLedger.Text = dr["CardLedger"].GetString();
            _bankLedgerId = dr["BankLedgerId"].GetLong();
            TxtBankLedger.Text = dr["BankLedger"].GetString();
            _phonePayLedgerId = dr["PhonePayLedgerId"].GetLong();
            TxtPhonePayLedger.Text = dr["PhonePayLedger"].GetString();
            _eSewaLedgerId = dr["EsewaLedgerId"].GetLong();
            TxtEsewaLedger.Text = dr["EsewaLedger"].GetString();
            _khaltiLedgerId = dr["KhaltiLedgerId"].GetLong();
            TxtKhaltiLedger.Text = dr["KhaltiLedger"].GetString();
            _remitLedgerId = dr["RemitLedgerId"].GetLong();
            TxtRemitLedger.Text = dr["RemitLedger"].GetString();
            _connectIpsLedgerId = dr["ConnectIpsLedgerId"].GetLong();
            TxtConnectIPSLedger.Text = dr["IpsLedger"].GetString();
            _partialCustomerId = dr["PartialLedgerId"].GetLong();
            TxtPartialCustomer.Text = dr["PartialLedger"].GetString();
        }
    }

    private bool ReturnSystemSettingStatus()
    {
        if (SavePaymentSetting() == 0)
        {
            CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE PAYMENT SETTING IS SAVE ");
            return false;
        }
        ObjGlobal.FillSystemConFiguration();
        CustomMessageBox.ActionSuccess("PAYMENT", "SETTING", "SAVE");
        return true;
    }

    private int SavePaymentSetting()
    {
        _paymentSettingRepository.VmPayment.PaymentId = 1;
        _paymentSettingRepository.VmPayment.CashLedgerId = _cashLedgerId > 0 ? _cashLedgerId : ObjGlobal.FinanceCashLedgerId;
        _paymentSettingRepository.VmPayment.CardLedgerId = _cardLedgerId > 0 ? _cardLedgerId : ObjGlobal.FinanceBankLedgerId;
        _paymentSettingRepository.VmPayment.BankLedgerId = _bankLedgerId > 0 ? _bankLedgerId : ObjGlobal.FinanceBankLedgerId;
        _paymentSettingRepository.VmPayment.PhonePayLedgerId = _phonePayLedgerId;
        _paymentSettingRepository.VmPayment.EsewaLedgerId = _eSewaLedgerId;
        _paymentSettingRepository.VmPayment.KhaltiLedgerId = _khaltiLedgerId;
        _paymentSettingRepository.VmPayment.RemitLedgerId = _remitLedgerId;
        _paymentSettingRepository.VmPayment.ConnectIpsLedgerId = _connectIpsLedgerId;
        _paymentSettingRepository.VmPayment.PartialLedgerId = _partialCustomerId;
        var result = _paymentSettingRepository.SavePaymentSetting("");
        return result;
    }
    private async void GetAndSaveUnsynchronizedPaymentSetting()
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
                GetUrl = @$"{_configParams.Model.Item2}PaymentSetting/GetPaymentSettingByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}PaymentSetting/InsertPaymentSettingList",
                UpdateUrl = @$"{_configParams.Model.Item2}PaymentSetting/UpdatePaymentSetting"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var settingRepo = DataSyncProviderFactory.GetRepository<DatabaseModule.Setup.SystemSetting.PaymentSetting>(_injectData);

            SplashScreenManager.ShowForm(typeof(PleaseWait));
            //pull all new table master data
            var pullResponse = await _paymentSettingRepository.PullPaymentSettingServerToClientByRowCounts(settingRepo, 1);
            SplashScreenManager.CloseForm();
            // push all new table master data
            var sqlCrQuery = _paymentSettingRepository.GetPaymentScript();
            var queryResponse = await QueryUtils.GetListAsync<DatabaseModule.Setup.SystemSetting.PaymentSetting>(sqlCrQuery);
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
    // OBJECT FOR THIS FORM
    private string actionTag = "UPDATE";

    private long _cashLedgerId;
    private long _cardLedgerId;
    private long _bankLedgerId;
    private long _phonePayLedgerId;
    private long _eSewaLedgerId;
    private long _khaltiLedgerId;
    private long _remitLedgerId;
    private long _connectIpsLedgerId;
    private long _partialCustomerId;
    private long _giftVoucherLedgerId;
    private readonly ISetup _setup = new ClsSetup();
    private readonly IPaymentSettingRepository _paymentSettingRepository;
    private DbSyncRepoInjectData _injectData = new();
    private InfoResult<ValueModel<string, string, Guid>> _configParams = new();
}