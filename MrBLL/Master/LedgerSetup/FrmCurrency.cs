using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using DevExpress.XtraEditors;
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
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.LedgerSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmCurrency : MrForm
{
    //  CURRENCY FORM

    #region --------------- Frm ---------------

    public FrmCurrency(bool zoom = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = zoom;
        _currencyRepository = new CurrencyRepository();
        _setup = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        ClearControl();
        EnableControl();
    }

    private void FrmCurrency_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedCurrencies);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private void FrmCurrency_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("CURRENCY") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
                {
                    Close();
                }
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
        EnableControl(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        EnableControl(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        EnableControl(false);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveCurrency() > 0)
            {
                if (_isZoom)
                {
                    CurrencyDesc = TxtDescription.Text.Trim();
                    CurrencyRate = TxtSalesRate.GetDecimal();
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtDescription.Text.GetUpper(), "CURRENCY", _actionTag);
                ClearControl();
                TxtDescription.Focus();
            }
            else
            {
                TxtDescription.ErrorMessage($"ERROR OCCURS WHILE {TxtDescription.Text} {_actionTag}");
                TxtDescription.Focus();
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()))
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
        }
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetUpper();
        }
        else if (e.Control && e.KeyCode is Keys.U)
        {
            TxtDescription.Text = TxtDescription.GetProperCase();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("CURRENCY DESCRIPTION IS BLANK..!!");
                return;
            }
        }
        else
        {
            if (TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            TxtShortName.Text = TxtDescription.GenerateShortName("Currency", "CName");
            var result = TxtDescription.IsDuplicate("CName", CurrencyId, _actionTag, "C");
            if (result)
            {
                TxtDescription.WarningMessage("CURRENCY DESCRIPTION IS ALREADY EXITS..!!");
                return;
            }
        }
        else if (TxtDescription.IsBlankOrEmpty() && TxtDescription.Enabled)
        {
            if (ActiveControl == BtnDescription)
            {
                return;
            }
            if (TxtDescription.ValidControl(ActiveControl) && _actionTag.IsValueExits())
            {
                TxtDescription.WarningMessage("CURRENCY DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id, rate) = GetMasterList.GetCurrencyList(_actionTag);
        if (description.IsValueExits())
        {
            CurrencyId = id;
            TxtDescription.Text = description;
            if (_actionTag != "SAVE")
            {
                SetGridDataToText();
                TxtDescription.ReadOnly = false;
            }
        }
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl.Name != "TxtShortName" && _actionTag.IsValueExits() && TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage("CURRENCY SHORTNAME IS REQUIRED..!!");
            return;
        }
        if (TxtShortName.IsValueExits() && _actionTag.IsValueExits())
        {
            var result = TxtShortName.CheckValueExits(_actionTag, "CURRENCY", "CCode", CurrencyId);
            if (result.Rows.Count > 0)
            {
                TxtShortName.WarningMessage("CURRENCY SHORTNAME IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage("CURRENCY SHORT NAME IS REQUIRED ");
                return;
            }
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }


    private void TxtRate_Leave(object sender, EventArgs e)
    {
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetCurrencyList("VIEW");
    }



    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedCurrencies);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    #endregion --------------- Frm ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private bool IsValidForm()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage(@"$""CURRENCY DESCRIPTION IS REQUIRED..!");
            return false;
        }

        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage(@"$""CURRENCY SHORTNAME IS REQUIRED...!");
            return false;
        }

        if (_actionTag != "SAVE" && CurrencyId is 0)
        {
            TxtDescription.WarningMessage($"CURRENCY DESCRIPTION IS REQUIRED...!");
            return false;
        }
        return true;
    }

    private void EnableControl(bool isEnable = false)
    {
        #region "Details"

        TxtDescription.Enabled = _actionTag.IsValueExits() && _actionTag == "DELETE" || isEnable;
        TxtShortName.Enabled = isEnable;
        TxtSalesRate.Enabled = isEnable;
        BtnSave.Enabled = _actionTag.IsValueExits() && _actionTag == "DELETE" || isEnable;
        BtnCancel.Enabled = _actionTag.IsValueExits() && _actionTag == "DELETE" || isEnable;
        ChkActive.Enabled = _actionTag == "UPDATE";

        #endregion "Details"

        #region "Button"

        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;

        #endregion "Button"
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"CURRENCY DETAILS SETUP [{_actionTag}]" : "CURRENCY DETAILS SETUP";
        CurrencyId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.IsValueExits() && _actionTag != "SAVE";
        TxtDescription.Focus();
    }

    private int SaveCurrency()
    {
        var syncRow = 1;
        CurrencyId = _actionTag is "SAVE" ? CurrencyId.ReturnMaxIntId("CURRENCY") : CurrencyId;
        _currencyRepository.ObjCurrency.CId = CurrencyId;
        _currencyRepository.ObjCurrency.CName = TxtDescription.Text.Trim().Replace("'", "''");
        _currencyRepository.ObjCurrency.CCode = TxtShortName.Text.Trim().Replace("'", "''");
        _currencyRepository.ObjCurrency.CRate = TxtSalesRate.GetDecimal();
        _currencyRepository.ObjCurrency.BuyRate = TxtBuyRate.GetDecimal();
        _currencyRepository.ObjCurrency.Status = ChkActive.Checked;
        _currencyRepository.ObjCurrency.IsDefault = 0;
        _currencyRepository.ObjCurrency.Branch_ID = ObjGlobal.SysBranchId;
        _currencyRepository.ObjCurrency.EnterBy = ObjGlobal.LogInUser;
        _currencyRepository.ObjCurrency.EnterDate = DateTime.Now;
        _currencyRepository.ObjCurrency.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : null;
        _currencyRepository.ObjCurrency.SyncRowVersion = (short)(_actionTag is "UPDATE" ? syncRow.ReturnSyncRowNo("CURRENCY", CurrencyId.ToString()) : syncRow);
        return _currencyRepository.SaveCurrency(_actionTag);
    }

    private void SetGridDataToText()
    {
        using var dt = _currencyRepository.GetMasterCurrency(_actionTag, 1, CurrencyId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        CurrencyId = dt.Rows[0]["CId"].GetInt();
        TxtDescription.Text = dt.Rows[0]["CName"].ToString();
        TxtShortName.Text = dt.Rows[0]["CCode"].ToString();
        TxtSalesRate.Text = dt.Rows[0]["CRate"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    private async void GetAndSaveUnSynchronizedCurrencies()
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
            GetUrl = @$"{_configParams.Model.Item2}Currency/GetCurrenciesByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Currency/InsertCurrencyList",
            UpdateUrl = @$"{_configParams.Model.Item2}Currency/UpdateCurrency"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var currencyRepo = DataSyncProviderFactory.GetRepository<Currency>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new currency data
        var pullResponse = await _currencyRepository.PullCurrencyServerToClientByRowCounts(currencyRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
        }
        else
        {
            SplashScreenManager.CloseForm();
        }
        // push all new currency data
        var sqlCrQuery = _currencyRepository.GetCurrencyScript();
        var queryResponse = await QueryUtils.GetListAsync<Currency>(sqlCrQuery);
        var curList = queryResponse.List.ToList();
        if (curList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await currencyRepo.PushNewListAsync(curList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.ShowDefaultWaitForm();
            }
            else
            {
                SplashScreenManager.CloseForm();
            }
        }
    }

    #endregion --------------- Method ---------------

    //OBJECT FOR THIS FORM

    #region --------------- Class ---------------

    public int CurrencyId;
    public string CurrencyDesc = string.Empty;
    public decimal CurrencyRate = 1;
    private readonly bool _isZoom;
    private string _actionTag;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IMasterSetup _setup;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;



    #endregion --------------- Class ---------------

    private void TxtRate_KeyDown(object sender, KeyEventArgs e)
    {

    }
}