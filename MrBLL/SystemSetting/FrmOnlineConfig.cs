using MrDAL.Control.ControlsEx;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.SystemSetting;
using MrDAL.SystemSetting.SystemInterface;
using System;
using System.Windows.Forms;

namespace MrBLL.SystemSetting;

public partial class FrmOnlineConfig : MrForm
{
    // ONLINE CONFIG
    public FrmOnlineConfig()
    {
        InitializeComponent();
        _getForm = new ClsMasterForm(this, BtnCancel);
        _objMaster = new ClsMasterSetup();
        _onlineConfigurationRepository = new OnlineConfigurationRepository();
    }
    private void FrmOnlineConfig_Load(object sender, EventArgs e)
    {
        SaveApiSyncDetails();
        ChkBranch.Focus();
    }
    private void FrmOnlineConfig_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape) BtnCancel.PerformClick();
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        //if (!IsValidForm())
        //{
        //    return;
        //}
        if (SaveOnlineSync() != 0)
        {
            CustomMessageBox.Information(@"ONLINE DATA SYNC SUCCESSFULLY CONFIG..!!");
            Close();
        }
        else
        {
            CustomMessageBox.Information(@"SOMETHING IS MISSING FOR CONFIG..!!");
            TxtApiUrl.Focus();
        }
    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    // OBJECT FOR THIS FORM
    #region --------------- Method ---------------
    private void SaveApiSyncDetails()
    {
        var dtSync = _objMaster.GetOnlineSync();
        if (dtSync.Rows.Count > 0)
        {
            ChkBranch.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsBranch"].ToString());
            ChkGeneralLedger.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsGeneralLedger"].ToString());
            ChkTableId.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsTableId"].ToString());
            ChkArea.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsArea"].ToString());
            ChkBillingTerm.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsBillingTerm"].ToString());
            ChkAgent.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsAgent"].ToString());
            ChkProduct.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsProduct"].ToString());
            ChkCostCenter.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsCostCenter"].ToString());
            ChkMember.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsMember"].ToString());
            ChkCashBank.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsCashBank"].ToString());
            ChkJournalVoucher.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsJournalVoucher"].ToString());
            ChkNotesRegister.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsNotesRegister"].ToString());
            ChkPDCVoucher.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPDCVoucher"].ToString());
            ChkLedgerOpening.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsLedgerOpening"].ToString());
            ChkProductOpening.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsProductOpening"].ToString());
            ChkSalesQuotation.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsSalesQuotation"].ToString());
            ChkSalesOrder.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsSalesOrder"].ToString());
            ChkSalesChallan.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsSalesChallan"].ToString());
            ChkSalesInvoice.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsSalesInvoice"].ToString());
            ChkSalesReturn.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsSalesReturn"].ToString());
            ChkSalesAdditional.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsSalesAdditional"].ToString());
            ChkPurchaseIndent.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPurchaseIndent"].ToString());
            ChkPurchaseOrder.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPurchaseOrder"].ToString());
            ChkPurchaseChallan.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPurchaseChallan"].ToString());
            ChkPurchaseInvoice.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPurchaseInvoice"].ToString());
            ChkPurchaseReturn.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPurchaseReturn"].ToString());
            ChkPurchaseAdditional.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsPurchaseAdditional"].ToString());
            ChkStockAdjustment.Checked = ObjGlobal.ReturnBool(dtSync.Rows[0]["IsStockAdjustment"].ToString());
            TxtApiUrl.Text = ObjGlobal.Decrypt(dtSync.Rows[0]["SyncAPI"].ToString());
            TxtOrginId.Text = ObjGlobal.Decrypt(dtSync.Rows[0]["SyncOriginId"].ToString());
        }
    }
    private bool IsValidForm()
    {
        if (string.IsNullOrEmpty(TxtApiUrl.Text))
        {
            MessageBox.Show("ONLINE SYNC API IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtApiUrl.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(TxtOrginId.Text))
        {
            MessageBox.Show("ONLINE ORIGIN ID IS REQUIRED..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            TxtOrginId.Focus();
            return false;
        }

        return true;
    }
    private int SaveOnlineSync()
    {
        _onlineConfigurationRepository.ObjSync.IsBranch = ChkBranch.Checked;
        _onlineConfigurationRepository.ObjSync.IsGeneralLedger = ChkGeneralLedger.Checked;
        _onlineConfigurationRepository.ObjSync.IsTableId = ChkTableId.Checked;
        _onlineConfigurationRepository.ObjSync.IsArea = ChkArea.Checked;
        _onlineConfigurationRepository.ObjSync.IsBillingTerm = ChkBillingTerm.Checked;
        _onlineConfigurationRepository.ObjSync.IsAgent = ChkAgent.Checked;
        _onlineConfigurationRepository.ObjSync.IsProduct = ChkProduct.Checked;
        _onlineConfigurationRepository.ObjSync.IsCostCenter = ChkCostCenter.Checked;
        _onlineConfigurationRepository.ObjSync.IsMember = ChkMember.Checked;
        _onlineConfigurationRepository.ObjSync.IsCashBank = ChkCashBank.Checked;
        _onlineConfigurationRepository.ObjSync.IsJournalVoucher = ChkJournalVoucher.Checked;
        _onlineConfigurationRepository.ObjSync.IsNotesRegister = ChkNotesRegister.Checked;
        _onlineConfigurationRepository.ObjSync.IsPDCVoucher = ChkPDCVoucher.Checked;
        _onlineConfigurationRepository.ObjSync.IsLedgerOpening = ChkLedgerOpening.Checked;
        _onlineConfigurationRepository.ObjSync.IsProductOpening = ChkProductOpening.Checked;
        _onlineConfigurationRepository.ObjSync.IsSalesQuotation = ChkSalesQuotation.Checked;
        _onlineConfigurationRepository.ObjSync.IsSalesOrder = ChkSalesOrder.Checked;
        _onlineConfigurationRepository.ObjSync.IsSalesChallan = ChkSalesChallan.Checked;
        _onlineConfigurationRepository.ObjSync.IsSalesInvoice = ChkSalesInvoice.Checked;
        _onlineConfigurationRepository.ObjSync.IsSalesReturn = ChkSalesReturn.Checked;
        _onlineConfigurationRepository.ObjSync.IsSalesAdditional = ChkSalesAdditional.Checked;
        _onlineConfigurationRepository.ObjSync.IsPurchaseIndent = ChkPurchaseIndent.Checked;
        _onlineConfigurationRepository.ObjSync.IsPurchaseOrder = ChkPurchaseOrder.Checked;
        _onlineConfigurationRepository.ObjSync.IsPurchaseChallan = ChkPurchaseChallan.Checked;
        _onlineConfigurationRepository.ObjSync.IsPurchaseInvoice = ChkPurchaseInvoice.Checked;
        _onlineConfigurationRepository.ObjSync.IsPurchaseReturn = ChkPurchaseReturn.Checked;
        _onlineConfigurationRepository.ObjSync.IsPurchaseAdditional = ChkPurchaseAdditional.Checked;
        _onlineConfigurationRepository.ObjSync.IsStockAdjustment = ChkStockAdjustment.Checked;
        _onlineConfigurationRepository.ObjSync.SyncAPI = !string.IsNullOrEmpty(TxtApiUrl.Text) ? ObjGlobal.Encrypt(TxtApiUrl.Text) : TxtApiUrl.Text;
        _onlineConfigurationRepository.ObjSync.SyncOriginId = !string.IsNullOrEmpty(TxtOrginId.Text) ? ObjGlobal.Encrypt(TxtOrginId.Text) : TxtOrginId.Text;
        _onlineConfigurationRepository.ObjSync.ApiKey = !string.IsNullOrEmpty(TxtOrginId.Text) ? TxtOrginId.Text.GetGuid() : null;
        return _onlineConfigurationRepository.SaveOnlineSyncConfig("SAVE");
    }

    #endregion --------------- Method ---------------

    // OBJECT FOR THIS FORM
    private readonly IMasterSetup _objMaster;
    private readonly IOnlineConfigurationRepository _onlineConfigurationRepository;
    private ClsMasterForm _getForm;
}