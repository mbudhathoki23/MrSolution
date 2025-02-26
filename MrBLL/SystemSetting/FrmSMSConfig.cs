using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Config;
using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmSMSConfig : MrForm
{
    public FrmSMSConfig()
    {
        InitializeComponent();
        GetForm = new ClsMasterForm(this, Btn_Cancel);
    }

    private void FrmSMSConfig_Load(object sender, EventArgs e)
    {
        _ActionTag = "SAVE";
        CheckData();
        TxtToken.Focus();
    }

    private void TxtToken_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && TxtToken.Focused && string.IsNullOrEmpty(TxtToken.Text.Trim()))
        {
            MessageBox.Show(@"SMS TOKEN CAN'T LEFT BLANK PLEASE CONTACT WITH VENDOR", ObjGlobal.Caption);
            TxtToken.Focus();
        }
    }

    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(TxtToken.Text.Trim()))
        {
            MessageBox.Show(@"SMS TOKEN CAN'T LEFT BLANK PLEASE CONTACT WITH VENDOR", ObjGlobal.Caption);
            TxtToken.Focus();
            return false;
        }

        return true;
    }

    private async void BtnSave_ClickAsync(object sender, EventArgs e)
    {
        Contract.Ensures(Contract.Result<Task>() != null);
        if (IsValidForm())
            if (I_U_D_SMS_CONFIG() > 0)
            {
                MessageBox.Show(@"CONFIG SAVE SUCCESSFULLY..!!", ObjGlobal.Caption);
                if (MessageBox.Show(@"DO YOU WANT TO TEXT ON YOUR PHONE..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) is DialogResult.Yes)
                    await SMSConfig.SendSmsAsync(TxtToken.Text.Trim(), TxtAlterNumber.Text,
                        $"{ObjGlobal.LogInCompany} :- SMS CONFIG SUCCESSFULLY..!!");
                Close();
            }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private int I_U_D_SMS_CONFIG()
    {
        //ObjMaster.ObjSms.SMSCONFIG_ID = SMSConfigId;
        //ObjMaster.ObjSms.TOKEN = ObjGlobal.Encrypt(TxtToken.Text);
        //ObjMaster.ObjSms.IsCashBank = ChkCashBank.Checked;
        //ObjMaster.ObjSms.IsJournalVoucher = ChkJournalVoucher.Checked;
        //ObjMaster.ObjSms.IsSalesReturn = ChkSalesReturn.Checked;
        //ObjMaster.ObjSms.IsSalesInvoice = ChkSalesInvoice.Checked;
        //ObjMaster.ObjSms.IsPurchaseInvoice = ChkPurchaseInvoice.Checked;
        //ObjMaster.ObjSms.IsPurchaseReturn = ChkPurchaseReturn.Checked;
        //ObjMaster.ObjSms.AlternetNumber = ObjGlobal.Encrypt(TxtAlterNumber.Text);
        //return ObjMaster.SaveSmsConfig(_ActionTag);
        return 0;
    }

    private void CheckData()
    {
        var dtCheck = ObjMaster.CheckSmsConfig();
        if (dtCheck != null && dtCheck.Rows.Count > 0)
        {
            _ActionTag = "UPDATE";
            SMSConfigId = ObjGlobal.ReturnInt(dtCheck.Rows[0]["SMSCONFIG_ID"].ToString());
            TxtToken.Text = ObjGlobal.Decrypt(dtCheck.Rows[0]["TOKEN"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtCheck.Rows[0]["IsCashBank"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtCheck.Rows[0]["IsJournalVoucher"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtCheck.Rows[0]["IsSalesReturn"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtCheck.Rows[0]["IsSalesInvoice"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtCheck.Rows[0]["IsPurchaseInvoice"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtCheck.Rows[0]["IsPurchaseReturn"].ToString());
            TxtAlterNumber.Text = ObjGlobal.Decrypt(dtCheck.Rows[0]["AlternetNumber"].ToString());
        }
    }

    #region -------------- Global Variable --------------

    private int SMSConfigId;
    private string _ActionTag = string.Empty;
    private readonly IMasterSetup ObjMaster = new ClsMasterSetup();
    private ClsMasterForm GetForm;

    #endregion -------------- Global Variable --------------
}