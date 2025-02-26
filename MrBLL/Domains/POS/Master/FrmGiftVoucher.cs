using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.FinanceSetup;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmGiftVoucher : Form
{
    // GIFT VOUCHER

    #region --------------- GIFT VOUCHER LIST ---------------

    public FrmGiftVoucher(bool zoom = false)
    {
        _actionTag = string.Empty;
        _isZoom = zoom;
        _giftVoucherRepository = new GiftVoucherRepository();
        _setup = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        InitializeComponent();
        _setup.BindGiftVoucherType(CmbGiftVoucherType);
        EnableControl();
        ClearControl();
    }

    private void FrmGiftVoucher_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedGiftVoucherLists();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmGiftVoucher_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("GIFT VOUCHER") is DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    EnableControl();
                    ClearControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ClearControl();
        EnableControl(true);
        TxtGiftVoucherNo.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtGiftVoucherNo.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtGiftVoucherNo.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        _ = GetMasterList.GetGiftVoucherList(_actionTag);
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveGiftVoucher() > 0)
            {
                if (_isZoom)
                {
                    _giftVoucherNo = TxtGiftVoucherNo.GetInt();
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "GIFT VOUCHERS", _actionTag);
                ClearControl();
                TxtGiftVoucherNo.Focus();
            }
            else
            {
                CustomMessageBox.ErrorMessage($"ERROR OCCURS WHILE {_actionTag}");
                TxtGiftVoucherNo.Focus();
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        BtnExit.PerformClick();
    }

    private void TxtGiftVoucherNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnGiftVoucher_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtGiftVoucherNo.IsBlankOrEmpty())
            {
                TxtGiftVoucherNo.WarningMessage("GIFT VOUCHER NUMBER IS REQUIRED..!!");
                return;
            }
        }
        else if (TxtGiftVoucherNo.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGiftVoucherNo, BtnGiftVoucher);
        }
    }

    private void TxtGiftVoucherNo_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtGiftVoucherNo.IsValueExits() && _actionTag != "DELETE")
        {
            var dtRow = TxtGiftVoucherNo.IsDuplicate("CardNo", _giftVoucherNo.ToString(), _actionTag, "GIFTVOUCHER");
            if (!dtRow)
            {
                return;
            }
            TxtGiftVoucherNo.WarningMessage($"GIFT VOUCHER NUMBER IS REQUIRED FOR [{_actionTag}]");
            GetLastVoucherNo();
            return;
        }

        if (_actionTag.IsValueExits() && TxtGiftVoucherNo.IsBlankOrEmpty())
        {
            if (TxtGiftVoucherNo.ValidControl(ActiveControl))
            {
                TxtGiftVoucherNo.WarningMessage($"GIFT VOUCHER NUMBER IS REQUIRED FOR [{_actionTag}]");
                GetLastVoucherNo();
                return;
            }
        }
    }

    private void BtnGiftVoucher_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGiftVoucherList(_actionTag);
        if (description.IsValueExits())
        {
            TxtGiftVoucherNo.Text = description;
            _giftVoucherNo = id;
            if (_actionTag != "SAVE")
            {
                FillGiftVoucherInformation();
            }
        }
        TxtGiftVoucherNo.Focus();
    }

    private void MskExpireDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (!MskExpireDate.MaskCompleted)
            {
                CustomMessageBox.Warning("EXPIRY DATE IS INVALID..!!");
                MskExpireDate.Focus();
                return;
            }
        }
    }

    private void MskExpireDate_Validating(object sender, CancelEventArgs e)
    {
        if (!MskExpireDate.MaskCompleted && _actionTag.IsValueExits())
        {
            if (MskExpireDate.ValidControl(ActiveControl))
            {
            }
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("GIFT VOUCHER DESCRIPTION IS REQUIRED");
                return;
            }
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsBlankOrEmpty() && _actionTag.ActionValid())
        {
            if (TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage("GIFT VOUCHER DESCRIPTION IS REQUIRED ..!!");
                return;
            }
        }
    }

    private void TxtIIssueAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtIIssueAmount.GetDouble() is 0)
            {
                TxtIIssueAmount.WarningMessage("GIFT VOUCHER AMOUNT IS REQUIRED..!!");
                return;
            }
        }
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedGiftVoucherLists);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- GIFT VOUCHER LIST ---------------

    // METHOD FOR THIS

    #region --------------- METHOD ---------------

    private bool IsValidForm()
    {
        if (TxtGiftVoucherNo.IsBlankOrEmpty())
        {
            TxtGiftVoucherNo.WarningMessage("GIFT VOUCHER NUMBER IS REQUIRED..!!");
            return false;
        }
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage("GIFT VOUCHER DESCRIPTION IS REQUIRED");
            return false;
        }
        if (!MskExpireDate.MaskCompleted)
        {
            CustomMessageBox.Warning("EXPIRY DATE IS INVALID..!!");
            MskExpireDate.Focus();
            return false;
        }
        if (TxtIIssueAmount.GetDouble() is 0)
        {
            TxtIIssueAmount.WarningMessage("GIFT VOUCHER AMOUNT IS REQUIRED");
            return false;
        }
        return true;
    }

    private int SaveGiftVoucher()
    {
        try
        {
            const int SyncId = 0;
            _giftVoucherNo = _actionTag is "SAVE" ? _giftVoucherNo.ReturnMaxIntId("GIFTVOUCHER") : _giftVoucherNo;

            _giftVoucherRepository.ObjGiftVoucher.VoucherId = _giftVoucherNo;
            _giftVoucherRepository.ObjGiftVoucher.CardNo = TxtGiftVoucherNo.Text.GetLong();
            _giftVoucherRepository.ObjGiftVoucher.ExpireDate = MskExpireDate.Text.GetDateTime();
            _giftVoucherRepository.ObjGiftVoucher.Description = TxtDescription.Text;
            _giftVoucherRepository.ObjGiftVoucher.VoucherType = CmbGiftVoucherType.SelectedValue.ToString();
            _giftVoucherRepository.ObjGiftVoucher.IssueAmount = TxtIIssueAmount.GetDecimal();
            _giftVoucherRepository.ObjGiftVoucher.Status = ChkActive.Checked;
            _giftVoucherRepository.ObjGiftVoucher.BranchId = ObjGlobal.SysBranchId;
            _giftVoucherRepository.ObjGiftVoucher.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
            _giftVoucherRepository.ObjGiftVoucher.SyncRowVersion =
                (byte)SyncId.ReturnSyncRowNo("GIFTVOUCHER", _giftVoucherNo.ToString());

            return _giftVoucherRepository.SaveGiftVoucherList(_actionTag);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return 0;
        }

    }

    private void GetLastVoucherNo()
    {
        if (!_actionTag.IsValueExits() || _actionTag == "DELETE")
        {
            return;
        }
        var dt = _setup.GetLastGiftVoucherNumber();
        if (dt.Rows.Count <= 0)
        {
            return;
        }
        var cardNo = dt.Rows[0]["CardNo"].GetLong();
        if (cardNo is 0)
        {
            var date = DateTime.Now.ToString("yyyMMdd");
            TxtGiftVoucherNo.Text = date;
            return;
        }
        cardNo += 1;
        TxtGiftVoucherNo.Text = cardNo.ToString();
    }

    private void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $"GIFT VOUCHER GENERATE [{_actionTag}]" : "GIFT VOUCHER GENERATE";
        MskExpireDate.Text = ObjGlobal.CfEndAdDate.GetDateString();
        TxtDescription.Clear();
        TxtIIssueAmount.Clear();
        TxtGiftVoucherNo.Clear();
        //GetLastVoucherNo();
        ChkActive.Checked = true;
        CmbGiftVoucherType.SelectedIndex = 1;
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = !isEnable;
        BtnEdit.Enabled = BtnDelete.Enabled = BtnNew.Enabled;
        TxtGiftVoucherNo.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtDescription.Enabled = isEnable;
        MskExpireDate.Enabled = isEnable;
        TxtIIssueAmount.Enabled = isEnable;
        CmbGiftVoucherType.Enabled = isEnable;
        ChkActive.Enabled = _actionTag.Equals("UPDATE");
        BtnSave.Enabled = isEnable || _actionTag.Equals("DELETE");
        BtnCancel.Enabled = BtnSave.Enabled;
    }

    private void FillGiftVoucherInformation()
    {
        var dt = _giftVoucherRepository.GetGiftVoucherNumberInformation(_giftVoucherNo);
        if (dt.Rows.Count <= 0) return;
        TxtGiftVoucherNo.Text = dt.Rows[0]["CardNo"].ToString();
        TxtDescription.Text = dt.Rows[0]["Description"].ToString();
        MskExpireDate.Text = dt.Rows[0]["ExpireDate"].ToString();
        TxtIIssueAmount.Text = dt.Rows[0]["IssueAmount"].GetDecimalString();
        var type = dt.Rows[0]["VoucherType"].ToString();
        CmbGiftVoucherType.SelectedIndex = CmbGiftVoucherType.FindString(type);
    }

    private async void GetAndSaveUnsynchronizedGiftVoucherLists()
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
            GetUrl = @$"{_configParams.Model.Item2}GiftVoucherList/GetGiftVoucherListsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}GiftVoucherList/InsertGiftVoucherList",
            UpdateUrl = @$"{_configParams.Model.Item2}GiftVoucherList/UpdateGiftVoucher"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var giftRepo = DataSyncProviderFactory.GetRepository<GiftVoucherList>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));

        // pull all new gift voucher data
        var pullResponse = await _giftVoucherRepository.PullCurrencyServerToClientByRowCounts(giftRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new gift voucher data
        var sqlCrQuery = _giftVoucherRepository.GetGiftVoucherScript();
        var queryResponse = await QueryUtils.GetListAsync<GiftVoucherList>(sqlCrQuery);
        var giftList = queryResponse.List.ToList();
        if (giftList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await giftRepo.PushNewListAsync(giftList);
            SplashScreenManager.CloseForm();

        }
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS

    #region --------------- OBJECT ---------------

    private int _giftVoucherNo;
    private readonly bool _isZoom;
    private string _actionTag = string.Empty;
    private IMasterSetup _setup;
    private IGiftVoucherRepository _giftVoucherRepository;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
    #endregion --------------- OBJECT ---------------
}