using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraEditors;
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
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MrBLL.Master.ProductSetup;

public partial class FrmProductGroup : MrForm
{
    // PRODUCT GROUP
    #region --------------- FORM ---------------
    public FrmProductGroup(bool isZoom = false)
    {
        InitializeComponent();
        _actionTag = string.Empty;
        _isZoom = isZoom;
        _setup = new ClsMasterSetup();
        _groupRepository = new ProductGroupRepository();
        CmbPrinter.BindPrinterList();
        ClearControl();
        EnableControl();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }
    private void FrmProductGroup_Load(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProductGroups);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
        this.ActiveControl = BtnNew;
    }
    private void FrmProductGroup_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (CustomMessageBox.ClearVoucherDetails("PRODUCT GROUP") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    EnableControl();
                    BtnNew.Focus();
                }
            }
            else
            {
                if (MessageBox.Show(@"ARE YOU SURE WANT TO EXIT THE FORM..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Close();
            }
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
    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValid())
        {
            if (SaveProductGroup() > 0)
            {
                if (_isZoom)
                {
                    ProductGroupDesc = TxtDescription.Text.Trim();
                    Close();
                    return;
                }

                CustomMessageBox.ActionSuccess($@"", "PRODUCT GROUP", _actionTag);
                ClearControl();
                TxtDescription.Focus();
                return;
            }
            else
            {
                CustomMessageBox.ErrorMessage($@"ERROR OCCURS WHILE {TxtDescription.Text} {_actionTag.ToUpper()} ..!!");
                ClearControl();
                TxtDescription.Focus();
                return;
            }
        }
        return;
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
    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
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
                TxtDescription.WarningMessage("PRODUCT GROUP DESCRIPTION IS BLANK..!!");
                return;
            }
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            if (TxtDescription.ReadOnly)
            {
                ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
            }
        }
    }
    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id, margin) = GetMasterList.GetProductGroup(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            GroupId = id;
            if (!_actionTag.Equals("SAVE"))
            {
                SetGridDataToText();
                TxtDescription.ReadOnly = false;
            }
        }

        TxtDescription.Focus();
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetProductGroup("VIEW");
    }

    private void TxtDescription_TextChanged(object sender, EventArgs e)
    {
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtDescription.Text) && _actionTag.ToUpper() is "SAVE")
        {
            TxtShortName.Text = ObjGlobal.SoftwareModule is "POS" or "RESTRO"
                ? TxtShortName.GetMaxValue(_actionTag, "PRODUCTGROUP", "GrpCode")
                : TxtShortName.GenerateShortName("PG", TxtDescription.Text, "GrpCode");
            if (ObjGlobal.SoftwareModule is "POS" or "RESTRO")
            {
                TxtShortName.Text = (TxtShortName.GetInt() + 1).ToString();
            }
        }
        else
        {
            TxtShortName.Text = TxtShortName.Text;
        }

        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE" && TxtDescription.IsValueExits())
        {
            var result = TxtDescription.IsDuplicate("GrpName", GroupId.ToString(), _actionTag, "PRODUCTGROUP");
            if (!result) return;
            CustomMessageBox.Warning(@"DESCRIPTION ALREADY EXITS..!!");
            TxtDescription.Focus();
            return;
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
        if (ActiveControl is not null && string.IsNullOrEmpty(TxtDescription.Text.Trim().Replace("'", "''")) &&
            TxtDescription.Focused && !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"DESCRIPTION IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtDescription.Focus();
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(_actionTag) && _actionTag != "DELETE" && TxtShortName.IsValueExits())
        {
            var result = TxtShortName.IsDuplicate("GrpCode", GroupId.ToString(), _actionTag, "PRODUCTGROUP");
            if (!result)
            {
                return;
            }
            CustomMessageBox.Warning(@"DESCRIPTION SHORTNAME ALREADY EXITS..!!");
            TxtDescription.Focus();
            return;
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtShortName.Text.Trim().Replace("'", "''")) && TxtShortName.Focused is true &&
            !string.IsNullOrEmpty(_actionTag))
        {
            MessageBox.Show(@"GROUP SHORTNAME IS BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter)
        {
            return;
        }

        if (TxtShortName.IsBlankOrEmpty())
        {
            TxtShortName.WarningMessage("PRODUCT GROUP SHORT NAME IS REQUIRED ");
            return;
        }
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    private void TxtMargin_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    private void ChkActive_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) e.KeyChar = '0';
    }

    private void CmbPrinter_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) SendKeys.Send("{F4}");
        Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    private void TxtPurchaseLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtPurchaseLedger.Text = TxtPurchaseReturn.Text = description;
                _purchaseLedgerId = _purchaseReturnLedgerId = id;
            }
            TxtPurchaseLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPurchaseLedger, BtnPurchaseLedger);
        }
    }

    private void BtnPurchaseLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtPurchaseLedger.Text = TxtPurchaseReturn.Text = description;
            _purchaseLedgerId = _purchaseReturnLedgerId = id;
        }

        TxtPurchaseLedger.Focus();
    }

    private void TxtPurchaseReturn_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseReturn_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtPurchaseReturn.Text = description;
                _purchaseReturnLedgerId = id;
            }
            TxtPurchaseReturn.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPurchaseReturn, BtnPurchaseReturn);
        }
    }

    private void BtnPurchaseReturn_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtPurchaseReturn.Text = description;
            _purchaseReturnLedgerId = id;
        }
        TxtPurchaseReturn.Focus();
    }

    private void TxtSalesLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtSalesLedger.Text = TxtSalesReturn.Text = description;
                _salesLedgerId = _salesReturnLedgerId = id;
            }
            TxtSalesLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesLedger, BtnSalesLedger);
        }
    }

    private void BtnSalesLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtSalesLedger.Text = TxtSalesReturn.Text = description;
            _salesLedgerId = _salesReturnLedgerId = id;
        }

        TxtSalesLedger.Focus();
    }

    private void TxtSalesReturn_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSalesReturn_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtSalesReturn.Text = description;
                _salesReturnLedgerId = id;
            }
            TxtSalesReturn.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesReturn, BtnSalesReturn);
        }
    }

    private void BtnSalesReturn_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtSalesReturn.Text = description;
            _salesReturnLedgerId = id;
        }

        TxtSalesReturn.Focus();
    }

    private void TxtOpeningLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOpeningLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtOpeningLedger.Text = description;
                _openingLedgerId = id;
            }
            TxtOpeningLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOpeningLedger, BtnOpeningLedger);
        }
    }

    private void BtnOpeningLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtOpeningLedger.Text = description;
            _openingLedgerId = id;
        }

        TxtOpeningLedger.Focus();
    }

    private void TxtClosingLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnClosingLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtClosingLedger.Text = description;
                _closingLedgerId = id;
            }
            TxtClosingLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtClosingLedger, BtnClosingLedger);
        }
    }

    private void BtnClosingLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtClosingLedger.Text = description;
            _closingLedgerId = id;
        }
        TxtClosingLedger.Focus();
    }

    private void TxtBSClosingLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnBSClosingLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other");
            if (id > 0)
            {
                TxtBSClosingLedger.Text = description;
                _stockInHandLedgerId = id;
            }
            TxtBSClosingLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBSClosingLedger, BtnBSClosingLedger);
        }
    }

    private void BtnBSClosingLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("Other", "LIST");
        if (id > 0)
        {
            TxtBSClosingLedger.Text = description;
            _stockInHandLedgerId = id;
        }
        TxtBSClosingLedger.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }

    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProductGroups);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion --------------- FORM ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TabProductGroup.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtDescription.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;
        TxtShortName.Enabled = CmbPrinter.Enabled = TxtMargin.Enabled = isEnable;
        TxtPurchaseLedger.Enabled = isEnable;
        TxtPurchaseReturn.Enabled = isEnable;
        TxtSalesLedger.Enabled = isEnable;
        TxtSalesReturn.Enabled = isEnable;
        TxtOpeningLedger.Enabled = isEnable;
        TxtClosingLedger.Enabled = isEnable;
        TxtBSClosingLedger.Enabled = isEnable;
        ChkActive.Enabled = _actionTag.IsValueExits() && _actionTag == "UPDATE";
        BtnSave.Enabled = BtnCancel.Enabled = _actionTag.IsValueExits() && _actionTag != "SAVE" || isEnable;
    }

    private void ClearControl()
    {
        GroupId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        TxtMargin.Clear();
        TxtNepaliDesc.Clear();
        TabProductGroup.SelectedPageIndex = 0;

        _purchaseLedgerId = ObjGlobal.PurchaseLedgerId;
        _purchaseReturnLedgerId = ObjGlobal.PurchaseReturnLedgerId;

        _salesLedgerId = ObjGlobal.SalesLedgerId;
        _salesReturnLedgerId = ObjGlobal.SalesReturnLedgerId;

        _openingLedgerId = ObjGlobal.StockOpeningStockLedgerId;
        _closingLedgerId = ObjGlobal.StockClosingStockLedgerId;
        _stockInHandLedgerId = ObjGlobal.StockStockInHandLedgerId;

        TxtPurchaseLedger.Text = _setup.GetLedgerDescription(_purchaseLedgerId);
        TxtPurchaseReturn.Text = _setup.GetLedgerDescription(_purchaseReturnLedgerId);

        TxtSalesLedger.Text = _setup.GetLedgerDescription(_salesLedgerId);
        TxtSalesReturn.Text = _setup.GetLedgerDescription(_salesReturnLedgerId);

        TxtOpeningLedger.Text = _setup.GetLedgerDescription(_openingLedgerId);
        TxtClosingLedger.Text = _setup.GetLedgerDescription(_closingLedgerId);
        TxtBSClosingLedger.Text = _setup.GetLedgerDescription(_stockInHandLedgerId);

        ChkActive.Checked = true;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"PRODUCT GROUP DETAILS SETUP [{_actionTag}]"
            : "PRODUCT GROUP DETAILS SETUP ";
    }

    private void SetGridDataToText()
    {
        var dtGroup = _groupRepository.GetProductGroupLedgerDetails(GroupId);
        if (dtGroup == null || dtGroup.Rows.Count <= 0)
        {
            return;
        }
        foreach (DataRow row in dtGroup.Rows)
        {
            TxtDescription.Text = row["GrpName"].ToString().GetTrimApostrophe();
            TxtShortName.Text = row["GrpCode"].ToString();
            TxtMargin.Text = row["GMargin"].GetDecimalString();
            CmbPrinter.Text = row["Gprinter"].ToString();
            TxtNepaliDesc.Text = row["NepaliDesc"].ToString().GetTrimApostrophe();
            _purchaseLedgerId = row["PurchaseLedgerId"].GetLong();
            _purchaseReturnLedgerId = row["PurchaseReturnLedgerId"].GetLong();
            _salesLedgerId = row["SalesLedgerId"].GetLong();
            _salesReturnLedgerId = row["SalesReturnLedgerId"].GetLong();
            _openingLedgerId = row["OpeningStockLedgerId"].GetLong();
            _closingLedgerId = row["ClosingStockLedgerId"].GetLong();
            _stockInHandLedgerId = row["StockInHandLedgerId"].GetLong();
            TxtPurchaseLedger.Text = row["PurchaseLedger"].GetString();
            TxtPurchaseReturn.Text = row["PurchaseReturnLedger"].GetString();
            TxtSalesLedger.Text = row["SalesLedger"].GetString();
            TxtSalesReturn.Text = row["SalesReturnLedger"].GetString();
            TxtOpeningLedger.Text = row["OpeningStockLedger"].GetString();
            TxtClosingLedger.Text = row["ClosingStockLedger"].GetString();
            TxtBSClosingLedger.Text = row["StockInHandLedger"].GetString();
            ChkActive.Checked = dtGroup.Rows[0]["Status"].GetBool();
        }
    }

    private int SaveProductGroup()
    {
        GroupId = _actionTag is "SAVE" ? ClsMasterSetup.ReturnMaxIntValue("AMS.ProductGroup", "PGrpID") : GroupId;
        _groupRepository.ObjProductGroup.PGrpId = GroupId;
        _groupRepository.ObjProductGroup.GrpName = TxtDescription.GetTrimReplace();
        _groupRepository.ObjProductGroup.GrpCode = TxtShortName.GetTrimReplace();
        _groupRepository.ObjProductGroup.Branch_ID = ObjGlobal.SysBranchId;
        _groupRepository.ObjProductGroup.EnterBy = ObjGlobal.LogInUser;
        _groupRepository.ObjProductGroup.EnterDate = DateTime.Now;
        _groupRepository.ObjProductGroup.GMargin = TxtMargin.GetDecimal();
        _groupRepository.ObjProductGroup.GPrinter = CmbPrinter.Text;
        _groupRepository.ObjProductGroup.Status = ChkActive.Checked;
        _groupRepository.ObjProductGroup.NepaliDesc = TxtNepaliDesc.Text;
        _groupRepository.ObjProductGroup.SyncRowVersion = _groupRepository.ObjProductGroup.SyncRowVersion.ReturnSyncRowNo("ProductGroup", GroupId);
        return _groupRepository.SaveProductGroup(_actionTag);
    }

    private bool IsValid()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (_actionTag.Equals("DELETE"))
        {
            if (GroupId is 0)
            {
                TxtDescription.WarningMessage("SELECTED GROUP IS INVALID..!!");
                return false;
            }
        }
        else
        {
            if (_actionTag.Equals("UPDATE"))
            {
                if (GroupId is 0)
                {
                    TxtDescription.WarningMessage("SELECTED GROUP IS INVALID..!!");
                    return false;
                }
            }
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("GROUP DESCRIPTION IS REQUIRED FOR ");
                return false;
            }

            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage($"PRODUCT SHORTNAME IS REQUIRED FOR {_actionTag}");
                return false;
            }
        }

        return true;
    }

    private async void GetAndSaveUnSynchronizedProductGroups()
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
            GetUrl = @$"{_configParams.Model.Item2}ProductGroup/GetProductGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductGroup/InsertProductGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductGroup/UpdateProductGroup"
        };
        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productGroupRepo = DataSyncProviderFactory.GetRepository<ProductGroup>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));

        // pull all new PRODUCT GROUP data
        var pullResponse = await _groupRepository.PullProductGroupsServerToClientByRowCount(productGroupRepo, 1);
        SplashScreenManager.CloseForm();
        // push all new PRODUCT GROUP data
        var sqlQuery = _groupRepository.GetProductGroupScript();
        var queryResponse = await QueryUtils.GetListAsync<ProductGroup>(sqlQuery);
        var pgList = queryResponse.List.ToList();
        if (pgList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await productGroupRepo.PushNewListAsync(pgList);
            SplashScreenManager.CloseForm();
        }
    }
    #endregion --------------- Method ---------------
    // OBJECT FOR THIS FORM

    #region --------------- Class ---------------
    public int GroupId;
    public int GroupCode;
    public string ProductGroupDesc;
    private long _stockInHandLedgerId;
    private long _closingLedgerId;
    private long _openingLedgerId;
    private long _salesReturnLedgerId;
    private long _salesLedgerId;
    private long _purchaseReturnLedgerId;
    private long _purchaseLedgerId;
    private string _actionTag;
    private readonly bool _isZoom;
    private IProductGroupRepository _groupRepository;
    private IMasterSetup _setup;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    #endregion --------------- Class ---------------


}