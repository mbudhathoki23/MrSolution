using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.OpeningMaster;
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
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Master.ProductSetup;

public partial class FrmProduct : MrForm
{
    #region -------------- Form --------------

    public FrmProduct(bool zoom)
    {
        InitializeComponent();

        _isZoom = zoom;
        _actionTag = string.Empty;
        _master = new ClsMasterSetup();
        _productRepository = new ProductRepository();
            
        _master.BindStockValueType(CmbValuation);
        _master.BindProductCategory(CmbItemCategory);
        _master.BindProductType(CmbItemType);

        ControlEnable();
        ClearControl();

        CmbItemType.SelectedIndex = 0;
        CmbValuation.SelectedIndex = 0;
        CmbItemCategory.SelectedIndex = 0;
        
        _productOpeningRepository = new ProductOpeningRepository();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    private void FrmProduct_Load(object sender, EventArgs e)
    {
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProducts);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private async void GetAndSaveUnSynchronizedProducts()
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
            GetUrl = @$"{_configParams.Model.Item2}Product/GetProductsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Product/InsertProductList",
            UpdateUrl = @$"{_configParams.Model.Item2}Product/UpdateProduct"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productsRepo = DataSyncProviderFactory.GetRepository<Product>(_injectData);
        SplashScreenManager.ShowForm(typeof(PleaseWait));
        // pull all new account data
        var pullResponse = await _productRepository.PullProductsServerToClientByRowCount(productsRepo, 1);
        SplashScreenManager.CloseForm();

        // push all new account data
        var sqlQuery = _productRepository.GetProductScript();
        var queryResponse = await QueryUtils.GetListAsync<Product>(sqlQuery);
        var prList = queryResponse.List.ToList();
        if (prList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await productsRepo.PushNewListAsync(prList);
            SplashScreenManager.CloseForm();
        }

    }

    private void FrmProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)27)
        {
            if (BtnNew.Enabled is false || BtnEdit.Enabled is false || BtnDelete.Enabled is false)
            {
                if (CustomMessageBox.ClearVoucherDetails("PRODUCT") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    ClearControl();
                    ControlEnable();
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
            Task.Run(GetAndSaveUnSynchronizedProducts);
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
        ControlEnable(true);
        ReturnVoucherNumber();
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        ControlEnable(true);
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        ControlEnable();
        TxtDescription.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
            {
                this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
                return;
            }
            if (IsFormValid())
            {
                if (SaveProduct() > 0)
                {
                    if (TxtOpeningStock.GetDecimal() > 0)
                    {
                        SaveProductOpening();
                    }
                    if (_isZoom)
                    {
                        ProductDesc = TxtDescription.Text;
                        _shortName = TxtShortName.Text;
                        Close();
                        return;
                    }
                    CustomMessageBox.ActionSuccess(TxtDescription.Text.ToUpper(), "ITEM PRODUCT", _actionTag);
                    ClearControl();
                    BtnSave.Enabled = true;
                    TxtDescription.Focus();
                }
                else
                {
                    TxtDescription.ErrorMessage($@"ERROR OCCURS WHILE {TxtDescription.Text.ToUpper()} ITEM PRODUCT {_actionTag} SUCCESSFULLY..!!");
                    return;
                }
            }
            else
            {
                BtnSave.Enabled = true;
                TxtDescription.ErrorMessage($@"ERROR OCCURS WHILE {TxtDescription.Text.ToUpper()} ITEM PRODUCT {_actionTag} SUCCESSFULLY..!!");
                return;
            }
        }
        catch (Exception exception)
        {
            exception.ToNonQueryErrorResult(exception);
            TxtDescription.ErrorMessage($@"ERROR OCCURS WHILE {TxtDescription.Text.ToUpper()} ITEM PRODUCT {_actionTag} SUCCESSFULLY..!!");
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetProduct("VIEW", DateTime.Now.GetDateString());
        BtnView.Focus();
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
                TxtDescription.WarningMessage("PRODUCT DESCRIPTION IS BLANK..!!");
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

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag.IsValueExits())
        {
            if (TxtAlias.IsBlankOrEmpty())
            {
                TxtAlias.Text = TxtDescription.Text;
            }
            TxtShortName.Text = TxtDescription.GenerateShortName("Product", "PShortName");
            var result = TxtDescription.IsDuplicate("PName", ProductId, _actionTag, "P");
            if (result)
            {
                TxtDescription.WarningMessage("PRODUCT DESCRIPTION IS ALREADY EXITS..!!");
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
                TxtDescription.WarningMessage("PRODUCT DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMasterProduct(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            ProductId = _actionTag.Equals("SAVE") ? 0 : id;
            if (_actionTag != "SAVE")
            {
                TxtDescription.ReadOnly = false;
                GetProductDetails(ProductId);
            }
        }
        TxtDescription.Focus();
    }

    private void TxtAlias_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtUnit_Leave(object sender, EventArgs e)
    {
        LblUnit.Text = TxtUnit.IsValueExits() ? TxtUnit.Text : string.Empty;
    }

    private void TxtUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnUOM_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var result = GetMasterList.CreateProductUnit(true);
            if (result.id > 0)
            {
                TxtUnit.Text = result.description;
                _pUnitId = result.id;
            }
            TxtUnit.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back or Keys.Delete)
        {
            TxtUnit.Clear();
            LblUnit.IsClear();
            _pUnitId = 0;
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtUnit.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtUnit, "ITEM UNIT IS REQUIRED");
                return;
            }

            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtUnit, BtnUOM);
        }
    }

    private void BtnUOM_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProductUnit(_actionTag);
        if (id > 0)
        {
            TxtUnit.Text = description;
            _pUnitId = id;
        }
        LblUnit.Text = TxtUnit.Text;
        TxtUnit.Focus();
    }

    private void TxtAltUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAltUOM_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtAltUnit.Text, _pAltUnitId) = GetMasterList.CreateProductUnit();
            TxtAltUnit.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyData is Keys.Enter)
        {
            TxtConvQty.Enabled = TxtConvAltQty.Enabled = _pAltUnitId > 0;
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtAltUnit.Clear();
            LblAltUnit.IsClear();
            _pAltUnitId = 0;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAltUnit, BtnAltUOM);
        }
    }

    private void TxtAltUnit_TextChanged(object sender, EventArgs e)
    {
        TxtConvQty.Enabled = TxtConvAltQty.Enabled = _pAltUnitId > 0;
    }

    private void BtnAltUOM_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProductUnit(_actionTag);
        if (id > 0)
        {
            TxtAltUnit.Text = description;
            _pAltUnitId = id;
        }
        LblAltUnit.Text = TxtAltUnit.Text;
        TxtAltUnit.Focus();
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        CheckShortName(TxtShortName.Text);
        if (TxtShortName.IsBlankOrEmpty() && _actionTag.IsValueExits() && TxtShortName.Enabled)
        {
            TxtShortName.WarningMessage($"PRODUCT SHORT NAME IS REQUIRED FOR [{_actionTag}]");
            return;
        }
    }
    private void TxtHsCode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }
    private void CmbCategory_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == 32)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbCategory1_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == 32)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void CmbValuationMethod_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Return)
        {
            e.Handled = true;
        }

        if (e.KeyChar == 32)
        {
            SendKeys.Send("{F4}");
        }
    }

    private void TxtAltQtyCov_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar is not '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtAltQtyCov_Leave(object sender, EventArgs e)
    {
        if (TxtConvAltQty.Enabled && _pAltUnitId > 0 && TxtAltUnit.Enabled && TxtConvAltQty.GetDecimal() is 0)
        {
            this.NotifyValidationError(TxtConvAltQty, @"UNIT CONVERSION CAN NOT LEFT BLANK..!!");
        }
    }

    private void TxtConvQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void BtnCompany_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDepartmentList(_actionTag, "PRODUCT COMPANY");
        if (id > 0)
        {
            TxtCompany.Text = description;
            _companyId = id;
        }
        TxtCompany.Focus();
    }

    private void TxtGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtGroup.Text, _pGroupId) = GetMasterList.CreateProductGroup();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnGroup_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtGroup.Clear();
            _pGroupId = 0;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGroup, BtnGroup);
        }
    }

    private void TxtGroup_Leave(object sender, EventArgs e)
    {
        if (ObjGlobal.StockGroupWiseCategory && _pGroupId is 0 && _actionTag.IsValueExits())
        {
            this.NotifyValidationError(TxtGroup, "GROUP IS REQUIRED..!! PLEASE SELECT THE GROUP..!!");
        }
    }

    private void TxtGroup_Validating(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.StockGroupWiseCategory && TxtGroup.Enabled && TxtGroup.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning($"GROUP IS REQUIRED FOR THE INFORMATION {_actionTag}");
            TxtGroup.Focus();
            return;
        }
    }

    private void TxtSubGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateProductSubGroup(true, _pGroupId);
            if (id > 0)
            {
                TxtSubGroup.Text = description;
                _pSubGroupId = id;
            }
            TxtSubGroup.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnSubGroup_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtSubGroup.Clear();
            _pSubGroupId = 0;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubGroup, BtnSubGroup);
        }
    }

    private void BtnGroup_Click(object sender, EventArgs e)
    {
        var (description, id, margin) = GetMasterList.GetProductGroup(_actionTag);
        if (description.IsValueExits())
        {
            TxtGroup.Text = description;
            _pGroupId = id;
            TxtPurchaseMargin.Text = margin.GetDecimalString();
            GetProductGroupInformation();
        }
        TxtGroup.Focus();
    }

    private void BtnSubGroup_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProductSubGroup("SAVE", _pGroupId);
        if (description.IsValueExits())
        {
            TxtSubGroup.Text = description;
            _pSubGroupId = id;
        }
        TxtSubGroup.Focus();
    }

    private void TxtPurRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar is not '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtPurRate_TextChanged(object sender, EventArgs e)
    {
        if (TxtPurRate.Enabled)
        {
            var taxRatio = (100 + TxtVat.GetDecimal()) / 100;
            TxtBuyRateBeforeVat.Text = TxtVat.GetDecimal() > 0 && TxtPurRate.GetDecimal() > 0 ? (TxtPurRate.GetDecimal() / taxRatio).GetRateDecimalString() : TxtPurRate.GetRateDecimalString();
            TxtOpeningRate.Text = TxtPurRate.GetRateDecimalString();
        }
    }

    private void TxtMRPMargin_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtTradeRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtMrp_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtPurchaseMargin_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtMRPMargin_TextChanged(object sender, EventArgs e)
    {
        if (TxtMRPMargin.IsValueExits())
        {
            var mrp = TxtMrp.GetDecimal();
            var margin = TxtMRPMargin.GetDecimal();
            var marginPercent = (100 + margin) / 100;
            var salesRate = mrp / marginPercent;
            TxtSalesRate.Text = salesRate.GetDecimalString();
        }
        else
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtSalesRate.Text = TxtMrp.Text;
            }
        }
    }

    private void TxtPurchaseMargin_TextChanged(object sender, EventArgs e)
    {
        if (TxtPurRate.IsValueExits())
        {
            var mrp = TxtPurRate.GetDecimal();
            var margin = TxtPurchaseMargin.GetDecimal();
            var salesRate = mrp + mrp * margin / 100;
            TxtSalesRate.Text = salesRate.GetDecimalString();
        }
        else
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtSalesRate.Text = TxtPurRate.Text;
            }
        }
    }

    private void TxtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtSalesRate_TextChanged(object sender, EventArgs e)
    {
        if (TxtSalesRate.Enabled)
        {
            var taxRatio = (100 + TxtVat.GetDecimal()) / 100;
            if (TxtSalesRate.GetDecimal() > 0)
            {
                TxtSalesRateBeforeVat.Text = TxtVat.GetDecimal() > 0
                    ? (TxtSalesRate.GetDecimal() / taxRatio).GetRateDecimalString()
                    : TxtSalesRate.GetRateDecimalString();
            }
            else
            {
                TxtSalesRateBeforeVat.Text = TxtSalesRate.GetDecimalString();
            }
        }
    }

    private void TxtMRPMargin_Validating(object sender, CancelEventArgs e)
    {
        TxtMRPMargin.Text = TxtMRPMargin.GetDecimalString();
        TxtPurchaseMargin.Enabled = TxtMRPMargin.GetDecimal() == 0;
        TxtMRPMargin_TextChanged(sender, e);
    }

    private void TxtPurchaseMargin_Validating(object sender, CancelEventArgs e)
    {
        TxtPurchaseMargin.Text = TxtPurchaseMargin.GetDecimalString();
        TxtMRPMargin.Enabled = TxtPurchaseMargin.GetDecimal() == 0;
        TxtPurchaseMargin_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtAltSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            TabProduct.SelectedTab = TpDetails;
            CmbValuation.Focus();
        }

        if (e.KeyChar is not (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtVat_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtCompany_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtCompany.Text, _companyId) = GetMasterList.CreateDepartment(true, "COMPANY DETAILS");
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnCompany_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtCompany.Clear();
            _companyId = 0;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCompany, BtnCompany);
        }
    }

    private void TxtPurchaseLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtPurchaseLedger.Text, _purchaseLedgerId) = GetMasterList.GetGeneralLedger(_actionTag);
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseLedger_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtPurchaseLedger.Clear();
            _purchaseLedgerId = 0;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPurchaseLedger, BtnPurchaseLedger);
        }
    }

    private void BtnPurchaseLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtPurchaseLedger.Text = TxtPurchaseReturn.Text = description;
            _purchaseLedgerId = _purchaseReturnLedgerId = id;
        }
        TxtPurchaseLedger.Focus();
    }

    private void BtnPurchaseReturn_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtPurchaseReturn.Text = description;
            _purchaseReturnLedgerId = id;
        }
        TxtPurchaseReturn.Focus();
    }

    private void TxtPurchaseReturn_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                TxtPurchaseReturn.Text = description;
                _purchaseReturnLedgerId = id;
            }
            TxtPurchaseReturn.Focus();
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnPurchaseReturn_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtPurchaseReturn.Clear();
            _purchaseReturnLedgerId = 0;
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPurchaseReturn, BtnPurchaseReturn);
        }
    }

    private void TxtSalesLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter && TxtSalesLedger.IsBlankOrEmpty())
        {
            BtnSalesLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                TxtSalesLedger.Text = description;
                _salesLedgerId = id;
            }
            TxtSalesLedger.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtSalesLedger.Clear();
            _salesLedgerId = 0;
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnSalesLedger_Click(sender, e);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesLedger, BtnSalesLedger);
        }
    }

    private void BtnSalesLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtSalesLedger.Text = TxtSalesReturn.Text = description;
            _salesLedgerId = _salesReturnLedgerId = id;
        }
        TxtSalesLedger.Focus();
    }

    private void TxtProductSalesReturn_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                TxtSalesReturn.Text = description;
                _salesReturnLedgerId = id;
            }
            TxtSalesReturn.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtSalesReturn.Clear();
            _salesReturnLedgerId = 0;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnSalesReturn_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSalesReturn, BtnSalesReturn);
        }
    }

    private void BtnSalesReturn_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtSalesReturn.Text = description;
            _salesReturnLedgerId = id;
        }

        TxtSalesReturn.Focus();
    }

    private void ImageBox_DoubleClick(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        try
        {
            using var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var fileName = dlg.FileName;
            isFileExists = dlg.FileName;
            PbPicbox.ImageLocation = fileName;
            PbPicbox.Load(fileName);
            lblProductPic.Visible = false;
            lnk_PreviewImage.Visible = true;
            lbl_ImageAttachment.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrEmpty(isFileExists))
            {
                MessageBox.Show(@"Picture File Format & " + ex.Message);
            }
            else
            {
                lblProductPic.Visible = true;
                lnk_PreviewImage.Visible = false;
            }
        }
    }

    private void LblProductPic_DoubleClick(object sender, EventArgs e)
    {
        ImageBox_DoubleClick(sender, e);
    }

    private void Lnk_PreviewImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        if (PbPicbox.Image == null) return;
        _fileExt = Path.GetExtension(PbPicbox.ImageLocation);
        if (_fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png") //&& this.Tag == "SAVE")
        {
            ObjGlobal.FetchPic(PbPicbox, string.Empty);
        }
        else
        {
            _saveFilePath = PbPicbox.ImageLocation;
            Process.Start(_saveFilePath ?? string.Empty);
        }
    }

    private void TxtOpeningStock_Validating(object sender, CancelEventArgs e)
    {
        TxtOpeningStock.Text = TxtOpeningStock.GetDecimalQtyString();
    }

    private void TxtOpeningStock_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtOpeningRate_Validating(object sender, CancelEventArgs e)
    {
        if (TxtOpeningStock.GetDecimal() > 0)
        {
            TxtOpeningAmount.Text = (TxtOpeningStock.GetDecimal() * TxtOpeningRate.GetDecimal()).GetDecimalString();
        }
    }

    private void TxtOpeningRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtOpeningAmount_Validating(object sender, CancelEventArgs e)
    {
        if (TxtOpeningStock.GetDecimal() > 0)
        {
            TxtOpeningRate.Text = (TxtOpeningAmount.GetDecimal() / TxtOpeningStock.GetDecimal()).GetDecimalString();
        }
    }

    private void TxtVat_Validating(object sender, CancelEventArgs e)
    {
        TxtBuyRateBeforeVat.Text = TxtVat.GetDecimal() > 0 && TxtPurRate.GetDecimal() > 0
            ? (TxtPurRate.GetDecimal() + (TxtPurRate.GetDecimal() / TxtVat.GetDecimal() * 100)).GetDecimalString()
            : TxtPurRate.Text;
        TxtSalesRateBeforeVat.Text = TxtVat.GetDecimal() > 0 && TxtSalesRate.GetDecimal() > 0
            ? (TxtSalesRate.GetDecimal() + (TxtSalesRate.GetDecimal() / TxtVat.GetDecimal() * 100))
            .GetDecimalString()
            : TxtSalesRate.Text;
    }

    private void TxtPurRate_Validated(object sender, EventArgs e)
    {
        var taxRatio = (100 + TxtVat.GetDecimal()) / 100;
        if (TxtPurRate.Enabled)
        {
            TxtPurRate.Text = TxtPurRate.GetDecimalString();
            TxtBuyRateBeforeVat.Text = TxtVat.GetDecimal() > 0 && TxtPurRate.GetDecimal() > 0
                ? (TxtPurRate.GetDecimal() / taxRatio).GetDecimalString()
                : TxtPurRate.Text;
            TxtOpeningRate.Text = TxtPurRate.Text;
        }
        if (_actionTag.Equals("SAVE") && TxtSalesRate.GetDecimal() is 0)
        {
            TxtSalesRate.Text = TxtPurRate.GetDecimal() > 0 && TxtPurchaseMargin.GetDecimal() > 0
                ? (TxtSalesRate.GetDecimal() / taxRatio.GetDecimal()).GetDecimalString()
                : TxtPurRate.Text;
            TxtSalesRateBeforeVat.Text = TxtVat.GetDecimal() > 0 && TxtSalesRate.GetDecimal() > 0
                ? (TxtSalesRate.GetDecimal() / taxRatio).GetDecimalString()
                : TxtSalesRate.Text;
        }
    }

    private void TxtSales_Validated(object sender, EventArgs e)
    {
        var taxRatio = (100 + TxtVat.GetDecimal()) / 100;
        TxtSalesRate.Text = TxtSalesRate.GetRateDecimalString();
        TxtSalesRateBeforeVat.Text = TxtVat.GetDecimal() > 0 && TxtSalesRate.GetDecimal() > 0
            ? (TxtSalesRate.GetDecimal() / taxRatio).GetRateDecimalString()
            : TxtSalesRate.GetRateDecimalString();
    }

    private void TxtBuyRateBeforeVat_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar is (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtSalesRateBeforeVat_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        e.Graphics.DrawImage(pictureBox1.Image, 0, 0);
    }

    private void TxtOpeningLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                TxtOpeningLedger.Text = description;
                _openingLedgerId = id;
            }

            TxtOpeningLedger.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtOpeningLedger.Clear();
            _openingLedgerId = 0;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnOpeningLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOpeningLedger, BtnOpeningLedger);
        }
    }

    private void BtnOpeningLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtOpeningLedger.Text = description;
            _openingLedgerId = id;
        }
        TxtOpeningLedger.Focus();
    }

    private void TxtClosingLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("OTHER", true);
            if (id > 0)
            {
                TxtClosingLedger.Text = description;
                _closingLedgerId = id;
            }
            TxtClosingLedger.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtClosingLedger.Clear();
            _closingLedgerId = 0;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnClosingLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtClosingLedger, BtnClosingLedger);
        }
    }

    private void BtnClosingLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtClosingLedger.Text = description;
            _closingLedgerId = id;
        }
        TxtClosingLedger.Focus();
    }

    private void BtnBSClosingLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtBSClosingLedger.Text = description;
            _stockInHandLedgerId = id;
        }
        TxtBSClosingLedger.Focus();
    }

    private void TpDetails_Click(object sender, EventArgs e)
    {
    }

    private void TxtReorderLevel_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtReorderStock_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtMinQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtMaxQy_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtOpeningStock_TextChanged(object sender, EventArgs e)
    {
        TxtOpeningAmount.Text = TxtOpeningStock.GetDecimal() > 0 ? (TxtOpeningStock.GetDecimal() * TxtOpeningRate.GetDecimal()).GetDecimalString() : 0.GetDecimalString();
    }

    private void TxtOpeningRate_TextChanged(object sender, EventArgs e)
    {
        TxtOpeningAmount.Text = TxtOpeningStock.GetDecimal() > 0 ? (TxtOpeningStock.GetDecimal() * TxtOpeningRate.GetDecimal()).GetDecimalString() : 0.GetDecimalString();
    }

    private void TxtOpeningAmount_TextChanged(object sender, EventArgs e)
    {
        TxtOpeningAmount.Text = TxtOpeningRate.GetDecimal() > 0 ? (TxtOpeningAmount.GetDecimal() / TxtOpeningStock.GetDecimal()).GetDecimalString() : 0.GetDecimalString();
    }

    private void TxtOpeningAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            BtnSave.Focus();
        }
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !((TextBox)sender).Text.Contains(".")) return;
        e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtBSClosingLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtBSClosingLedger.Text, _stockInHandLedgerId) = GetMasterList.CreateGeneralLedger("OTHER", true);
            TxtBSClosingLedger.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtBSClosingLedger.Clear();
            _stockInHandLedgerId = 0;
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnBSClosingLedger_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            BtnSave.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBSClosingLedger, BtnClosingLedger);
        }
    }

    #endregion -------------- Form --------------

    // METHOD FOR THIS FORM

    #region -------------- Method --------------

    private bool IsFormValid()
    {
        if (string.IsNullOrEmpty(TxtDescription.Text))
        {
            CustomMessageBox.Warning(@"Product Name is Required.");
            TxtDescription.Focus();
            return false;
        }

        if (_actionTag.Equals("DELETE"))
        {
            return CustomMessageBox.FormAction(_actionTag, "PRODUCT") == DialogResult.Yes;
        }

        if (string.IsNullOrEmpty(TxtShortName.Text))
        {
            CustomMessageBox.Warning(@"PRODUCT SHORT NAME IS REQUIRED..!!");
            TxtShortName.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(CmbItemType.Text))
        {
            CustomMessageBox.Warning(@"PRODUCT TYPE IS REQUIRED..!!");
            CmbItemType.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(CmbValuation.Text))
        {
            CustomMessageBox.Warning(@"PRODUCT VALUATION METHOD IS REQUIRED..!!");
            CmbValuation.Focus();
            return false;
        }
        if (ObjGlobal.StockGroupWiseCategory && TxtGroup.Enabled && TxtGroup.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning($"GROUP IS REQUIRED FOR THE INFORMATION {_actionTag}");
            TxtGroup.Focus();
            return false;
        }
        return true;
    }

    private void ControlEnable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable && !_actionTag.Equals("DELETE");

        TabProduct.Enabled = TxtDescription.Enabled = BtnDescription.Enabled = !string.IsNullOrEmpty(_actionTag) || _actionTag.Equals("DELETE");
        CmbItemCategory.Enabled = isEnable;

        TxtMrp.Enabled = isEnable;
        TxtTradeRate.Enabled = isEnable;
        TxtOpeningStock.Enabled = TxtOpeningRate.Enabled = TxtOpeningAmount.Enabled = isEnable;

        ChkBatchWise.Enabled = isEnable;

        TxtCompany.Enabled = BtnCompany.Enabled = isEnable && ObjGlobal.StockDepartmentEnable;

        TxtAlias.Enabled = isEnable;
        TxtShortName.Enabled = isEnable;
        TxtHsCode.Enabled = isEnable;
        CmbItemType.Enabled = isEnable;

        TxtUnit.Enabled = isEnable;

        TxtAltUnit.Enabled = isEnable;
        TxtConvAltQty.Enabled = TxtConvQty.Enabled = false;
        CmbValuation.Enabled = isEnable;
        ChkSerialNoWise.Enabled = isEnable;
        ChkVehicleInfo.Enabled = isEnable;
        TxtPurRate.Enabled = isEnable;
        TxtMRPMargin.Enabled = isEnable;
        TxtPurchaseMargin.Enabled = isEnable;
        TxtSalesRate.Enabled = isEnable;
        TxtGroup.Enabled = isEnable;
        TxtSubGroup.Enabled = isEnable;
        TxtVat.Enabled = isEnable;
        txtMinQty.Enabled = isEnable;
        txtMaxQy.Enabled = isEnable;
        ChkActive.Enabled = isEnable && _actionTag.Equals("UPDATE");
        BtnGroup.Enabled = isEnable;
        BtnSubGroup.Enabled = isEnable;
        lblProductPic.Enabled = isEnable;
        PbPicbox.Enabled = isEnable;
        BtnSave.Enabled = btnClear.Enabled = isEnable || _actionTag.Equals("DELETE");
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"PRODUCT SETUP DETAILS [{_actionTag}]" : "PRODUCT SETUP DETAILS";
        ProductId = 0;
        TabProduct.SelectedTab = TPProductInfo;
        TxtDescription.ReadOnly = !_actionTag.Equals("SAVE");
        TxtDescription.Clear();
        TxtAlias.Clear();
        TxtShortName.Clear();
        TxtHsCode.Clear();
        CmbItemCategory.SelectedIndex = CmbValuation.SelectedIndex = CmbItemType.SelectedIndex = 0;

        _pUnitId = 0;
        TxtUnit.Clear();
            
        _pAltUnitId = 0;
        TxtAltUnit.Clear();
            
        TxtConvAltQty.Text = 0.GetDecimalQtyString();
        TxtConvQty.Text = 0.GetDecimalQtyString();
        lnk_PreviewImage.Visible = false;

        TxtPurRate.Text = 0.GetDecimalString();
        TxtMRPMargin.Text = 0.GetDecimalString();
        TxtPurchaseMargin.Text = 0.GetDecimalString();
        TxtSalesRate.Text = 0.GetDecimalString();
        TxtAltSalesRate.Text = 0.GetDecimalString();
            
        _pGroupId = 0;
        TxtGroup.Clear();
            
        _pSubGroupId = 0;
        TxtSubGroup.Clear();
            
        _companyId = 0;
        TxtCompany.Clear();

        TxtVat.Text = 13.GetDecimalString();
        txtMinQty.Text = 0.GetDecimalQtyString();
        txtMaxQy.Text = 0.GetDecimalQtyString();
        TxtMrp.Text = 0.GetDecimalString();
        TxtTradeRate.Text = 0.GetDecimalString();

        ChkActive.Enabled = _actionTag.Equals("UPDATE");
        ChkActive.Checked = true;

        ChkBatchWise.Checked = false;
        ChkSerialNoWise.Checked = false;
        ChkVehicleInfo.Checked = false;
        ChkSizeWise.Checked = false;
        ChkPublicationInfo.Checked = false;

        lblProductPic.Visible = true;
        TxtPurchaseLedger.Clear();
        TxtPurchaseReturn.Clear();
        TxtSalesLedger.Clear();
        TxtSalesReturn.Clear();
        lbl_ImageAttachment.IsClear();
        PbPicbox.Image = null;
        TxtOpeningStock.Clear();
        TxtOpeningRate.Clear();
        TxtOpeningAmount.Clear();
        TxtBuyRateBeforeVat.Clear();
        TxtSalesRateBeforeVat.Clear();
        LblUnit.IsClear();
        LblAltUnit.IsClear();

        _purchaseLedgerId = ObjGlobal.PurchaseLedgerId;
        _purchaseReturnLedgerId = ObjGlobal.PurchaseReturnLedgerId;

        _salesLedgerId = ObjGlobal.SalesLedgerId;
        _salesReturnLedgerId = ObjGlobal.SalesReturnLedgerId;

        _openingLedgerId = ObjGlobal.StockOpeningStockLedgerId;
        _closingLedgerId = ObjGlobal.StockClosingStockLedgerId;
        _stockInHandLedgerId = ObjGlobal.StockStockInHandLedgerId;

        TxtPurchaseLedger.Text = _master.GetLedgerDescription(_purchaseLedgerId);
        TxtPurchaseReturn.Text = _master.GetLedgerDescription(_purchaseReturnLedgerId);

        TxtSalesLedger.Text = _master.GetLedgerDescription(_salesLedgerId);
        TxtSalesReturn.Text = _master.GetLedgerDescription(_salesReturnLedgerId);

        TxtOpeningLedger.Text = _master.GetLedgerDescription(_openingLedgerId);
        TxtClosingLedger.Text = _master.GetLedgerDescription(_closingLedgerId);
        TxtBSClosingLedger.Text = _master.GetLedgerDescription(_stockInHandLedgerId);
    }

    private void GetProductDetails(long productId)
    {
        _dtData.Reset();
        _dtData = _master.GetMasterProductList(_actionTag, productId);
        if (_dtData == null || _dtData.Rows.Count <= 0) return;
        TxtAlias.Text = _dtData.Rows[0]["PAlias"].GetString();
        TxtShortName.Text = _dtData.Rows[0]["PShortName"].GetString();
        TxtHsCode.Text = _dtData.Rows[0]["HsCode"].GetString();
        var pType = _dtData.Rows[0]["PType"].GetString();
        var valType = _dtData.Rows[0]["PValTech"].GetString();
        CmbItemType.SelectedIndex = CmbItemType.FindString(pType);
        CmbValuation.SelectedIndex = CmbValuation.FindString(valType);
        _pGroupId = _dtData.Rows[0]["PGrpId"].GetInt();
        TxtGroup.Text = _dtData.Rows[0]["GrpName"].GetString();
        _pSubGroupId = _dtData.Rows[0]["PSubGrpId"].GetInt();
        TxtSubGroup.Text = _dtData.Rows[0]["SubGrpName"].GetString();
        _pUnitId = _dtData.Rows[0]["PUnit"].GetInt();
        TxtUnit.Text = _dtData.Rows[0]["UnitCode"].GetString();
        LblUnit.Text = TxtUnit.Text;
        _pAltUnitId = _dtData.Rows[0]["PAltUnit"].GetInt();
        TxtAltUnit.Text = _dtData.Rows[0]["AltUnitCode"].GetString();
        LblAltUnit.Text = TxtAltUnit.Text;
        _companyId = _dtData.Rows[0]["CmpId"].GetInt();
        TxtCompany.Text = _dtData.Rows[0]["DName"].ToString();
        TxtConvAltQty.Text = _dtData.Rows[0]["PAltConv"].GetDecimalQtyString();
        TxtConvQty.Text = _dtData.Rows[0]["PQtyConv"].GetDecimalQtyString();

        ChkBatchWise.Checked = _dtData.Rows[0]["PBatchwise"].GetBool();
        ChkSizeWise.Checked = _dtData.Rows[0]["PSizewise"].GetBool();
        ChkSerialNoWise.Checked = _dtData.Rows[0]["PSerialNo"].GetBool();
        ChkSerialNoWise.Checked = _dtData.Rows[0]["PSerialNo"].GetBool();
        ChkSerialNoWise.Checked = _dtData.Rows[0]["PSerialNo"].GetBool();

        TxtPurRate.Text = _dtData.Rows[0]["PBuyRate"].GetRateDecimalString();
        TxtSalesRate.Text = _dtData.Rows[0]["PSalesRate"].GetRateDecimalString();
        TxtMrp.Text = _dtData.Rows[0]["PMRP"].GetRateDecimalString();
        TxtMRPMargin.Text = _dtData.Rows[0]["PMargin1"].GetRateDecimalString();
        TxtPurchaseMargin.Text = _dtData.Rows[0]["PMargin2"].GetRateDecimalString();
        txtMinQty.Text = _dtData.Rows[0]["PMin"].GetDecimalString();
        txtMaxQy.Text = _dtData.Rows[0]["PMax"].GetDecimalString();
        TxtVat.Text = _dtData.Rows[0]["PTax"].GetRateDecimalString();
        TxtTradeRate.Text = _dtData.Rows[0]["TradeRate"].GetRateDecimalString();

        ChkActive.Checked = _dtData.Rows[0]["Status"].GetBool();

        _purchaseLedgerId = _dtData.Rows[0]["PPL"].GetLong();
        if (_purchaseLedgerId is 0)
        {
            _purchaseLedgerId = ObjGlobal.PurchaseLedgerId;
            TxtPurchaseLedger.Text = _master.GetLedgerDescription(_purchaseLedgerId);
        }
        else
        {
            TxtPurchaseLedger.Text = _dtData.Rows[0]["PurchaseLedger"].ToString();
        }

        _purchaseReturnLedgerId = _dtData.Rows[0]["PPR"].GetLong();
        if (_purchaseReturnLedgerId is 0)
        {
            _purchaseReturnLedgerId = ObjGlobal.PurchaseReturnLedgerId;
            TxtPurchaseReturn.Text = _master.GetLedgerDescription(_purchaseReturnLedgerId);
        }
        else
        {
            TxtPurchaseReturn.Text = _dtData.Rows[0]["PurchaseReturn"].ToString();
        }

        _salesLedgerId = _dtData.Rows[0]["PSL"].GetLong();
        if (_salesLedgerId is 0)
        {
            _salesLedgerId = ObjGlobal.SalesLedgerId;
            TxtSalesLedger.Text = _master.GetLedgerDescription(_salesLedgerId);
        }
        else
        {
            TxtSalesLedger.Text = _dtData.Rows[0]["SalesLedger"].ToString();
        }

        _salesReturnLedgerId = _dtData.Rows[0]["PSR"].GetLong();
        if (_salesReturnLedgerId is 0)
        {
            _salesReturnLedgerId = ObjGlobal.SalesReturnLedgerId;
            TxtSalesReturn.Text = _master.GetLedgerDescription(_salesReturnLedgerId);
        }
        else
        {
            TxtSalesReturn.Text = _dtData.Rows[0]["SalesReturn"].ToString();
        }

        TxtSalesRateBeforeVat.Text = _dtData.Rows[0]["BeforeSalesRate"].GetRateDecimalString();
        TxtBuyRateBeforeVat.Text = _dtData.Rows[0]["BeforeBuyRate"].GetRateDecimalString();

        _openingLedgerId = _dtData.Rows[0]["PL_Opening"].GetLong();
        if (_openingLedgerId is 0)
        {
            _openingLedgerId = ObjGlobal.StockOpeningStockLedgerId;
            TxtOpeningLedger.Text = _master.GetLedgerDescription(_openingLedgerId);
        }
        else
        {
            TxtOpeningLedger.Text = _dtData.Rows[0]["OpeningLedger"].ToString();
        }
        _closingLedgerId = _dtData.Rows[0]["PL_Closing"].GetLong();
        if (_closingLedgerId is 0)
        {
            _closingLedgerId = ObjGlobal.StockClosingStockLedgerId;
            TxtClosingLedger.Text = _master.GetLedgerDescription(_closingLedgerId);
        }
        else
        {
            TxtClosingLedger.Text = _dtData.Rows[0]["ClosingLedger"].ToString();
        }

        _stockInHandLedgerId = _dtData.Rows[0]["BS_Closing"].GetLong();
        if (_stockInHandLedgerId is 0)
        {
            _stockInHandLedgerId = ObjGlobal.StockStockInHandLedgerId;
            TxtBSClosingLedger.Text = _master.GetLedgerDescription(_stockInHandLedgerId);
        }
        else
        {
            TxtBSClosingLedger.Text = _dtData.Rows[0]["BSClosingLedger"].ToString();
        }

        _productRepository.ObjProduct.Barcode = _dtData.Rows[0]["Barcode"].ToString();
        _productRepository.ObjProduct.Barcode1 = _dtData.Rows[0]["Barcode1"].ToString();
        _productRepository.ObjProduct.Barcode2 = _dtData.Rows[0]["Barcode2"].ToString();
        _productRepository.ObjProduct.Barcode3 = _dtData.Rows[0]["Barcode3"].ToString();

        var pImage = _dtData.Rows[0]["PImage"].GetInt();
        if (pImage.IsValueExits() && pImage > 0)
        {
            PbPicbox.Image = Image.FromStream(new MemoryStream(pImage));
        }
    }

    private int SaveProduct()
    {
        short sync = 0;

        ProductId = _actionTag is "SAVE" ? ProductId.ReturnMaxLongId("PRODUCT", ProductId.ToString()) : ProductId;
        _productRepository.ObjProduct.PID = ProductId;
        _productRepository.ObjProduct.PName = TxtDescription.Text.GetTrimReplace();
        _productRepository.ObjProduct.PAlias = TxtDescription.Text.GetTrimReplace();
        _productRepository.ObjProduct.PShortName = TxtShortName.Text.GetTrimReplace();
        _productRepository.ObjProduct.HsCode = TxtHsCode.Text.GetTrimReplace();
        _productRepository.ObjProduct.PType = CmbItemType.SelectedValue.ToString();
        _productRepository.ObjProduct.PCategory = CmbItemCategory.SelectedValue.ToString();
        _productRepository.ObjProduct.PUnit = _pUnitId == 0 ? 1 : _pUnitId;
        _productRepository.ObjProduct.PAltUnit = _pAltUnitId > 0 ? _pAltUnitId : null;
        _productRepository.ObjProduct.PQtyConv = TxtConvQty.GetDecimal();
        _productRepository.ObjProduct.PAltConv = TxtConvAltQty.GetDecimal();
        _productRepository.ObjProduct.PValTech = CmbValuation.SelectedValue.ToString();
        _productRepository.ObjProduct.PSerialno = ChkSerialNoWise.Checked;
        _productRepository.ObjProduct.PSizewise = ChkSizeWise.Checked;
        _productRepository.ObjProduct.PBatchwise = ChkBatchWise.Checked;
        _productRepository.ObjProduct.PublicationWise = ChkPublicationInfo.Checked;
        _productRepository.ObjProduct.PVehicleWise = ChkVehicleInfo.Checked;
        _productRepository.ObjProduct.PBuyRate = TxtPurRate.GetDecimal();
        _productRepository.ObjProduct.PMRP = TxtMrp.GetDecimal();
        _productRepository.ObjProduct.PSalesRate = TxtSalesRate.GetDecimal();
        _productRepository.ObjProduct.AltSalesRate = TxtSalesRate.GetDecimal() * TxtConvQty.GetDecimal();
        _productRepository.ObjProduct.PMargin1 = TxtMRPMargin.GetDecimal();
        _productRepository.ObjProduct.TradeRate = TxtTradeRate.GetDecimal();
        _productRepository.ObjProduct.PMargin2 = TxtPurchaseMargin.GetDecimal();
        _productRepository.ObjProduct.PGrpId = _pGroupId > 0 ? _pGroupId : null;
        _productRepository.ObjProduct.PSubGrpId = _pSubGroupId > 0 ? _pSubGroupId : null;
        _productRepository.ObjProduct.PTax = TxtVat.GetDecimal();
        _productRepository.ObjProduct.PMin = txtMinQty.GetDecimal();
        _productRepository.ObjProduct.PMax = txtMaxQy.GetDecimal();
        _productRepository.ObjProduct.CmpId = _companyId > 0 ? _companyId : null;
        _productRepository.ObjProduct.Branch_Id = ObjGlobal.SysBranchId;
        _productRepository.ObjProduct.EnterBy = ObjGlobal.LogInUser;
        _productRepository.ObjProduct.EnterDate = DateTime.Now;
        _productRepository.ObjProduct.PPL = _purchaseLedgerId > 0 ? _purchaseLedgerId : null;
        _productRepository.ObjProduct.PPR = _purchaseReturnLedgerId > 0 ? _purchaseReturnLedgerId : null;
        _productRepository.ObjProduct.PSL = _salesLedgerId > 0 ? _salesLedgerId : null;
        _productRepository.ObjProduct.PSR = _salesReturnLedgerId > 0 ? _salesReturnLedgerId : null;
        _productRepository.ObjProduct.PL_Opening = _openingLedgerId > 0 ? _openingLedgerId : null;
        _productRepository.ObjProduct.PL_Closing = _closingLedgerId > 0 ? _closingLedgerId : null;
        _productRepository.ObjProduct.BS_Closing = _stockInHandLedgerId > 0 ? _stockInHandLedgerId : null;
        _productRepository.ObjProduct.PImage = null;
        _productRepository.ObjProduct.Status = ChkActive.Checked;
        _productRepository.ObjProduct.Barcode = LblHsCode.Text;

        sync = sync.ReturnSyncRowNo("PRODUCT", ProductId.ToString());
        _productRepository.ObjProduct.SyncRowVersion = sync;
        _productRepository.ObjProduct.BeforeBuyRate = TxtBuyRateBeforeVat.Text.Trim().GetDecimal();
        _productRepository.ObjProduct.BeforeSalesRate = TxtSalesRateBeforeVat.Text.Trim().GetDecimal();
        _productRepository.ObjProduct.ChasisNo = _productRepository.ObjProduct.EngineNo = string.Empty;
        _productRepository.ObjProduct.VHModel = _productRepository.ObjProduct.VHColor = string.Empty;

        return _productRepository.SaveProductInfo(_actionTag);
    }

    private void ReturnVoucherNumber()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("POP");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            _txtVno.GetCurrentVoucherNo("POP", _docDesc);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            _txtVno.GetCurrentVoucherNo("POP", _docDesc);
        }
    }

    private int SaveProductOpening()
    {
        if (_actionTag is "SAVE")
        {
            _txtVno.GetCurrentVoucherNo("POP", _docDesc);
            _openingId = _openingId.ReturnMaxIntId("POP", "");
        }
        var opDate = ObjGlobal.CfStartAdDate.AddDays(-1);

        _productOpeningRepository.VmProductOpening.Voucher_No = _txtVno.Text;
        _productOpeningRepository.VmProductOpening.Serial_No = 1;
        _productOpeningRepository.VmProductOpening.OP_Date = opDate.GetDateTime();
        _productOpeningRepository.VmProductOpening.OP_Miti = opDate.GetNepaliDate();
        _productOpeningRepository.VmProductOpening.Product_Id = ProductId;
        _productOpeningRepository.VmProductOpening.Godown_Id = 0;
        _productOpeningRepository.VmProductOpening.Cls1 = 0;
        _productOpeningRepository.VmProductOpening.Cls2 = 0;
        _productOpeningRepository.VmProductOpening.Cls3 = 0;
        _productOpeningRepository.VmProductOpening.Cls4 = 0;
        _productOpeningRepository.VmProductOpening.Currency_Id = ObjGlobal.SysCurrencyId;
        _productOpeningRepository.VmProductOpening.Currency_Rate = 1;
        _productOpeningRepository.VmProductOpening.AltQty = 0;
        _productOpeningRepository.VmProductOpening.AltUnit = 0;
        _productOpeningRepository.VmProductOpening.Qty = TxtOpeningStock.GetDecimal();
        _productOpeningRepository.VmProductOpening.QtyUnit = _pUnitId;
        _productOpeningRepository.VmProductOpening.Rate = TxtOpeningRate.GetDecimal();
        _productOpeningRepository.VmProductOpening.LocalRate = TxtOpeningRate.GetDecimal();
        _productOpeningRepository.VmProductOpening.Amount = TxtOpeningAmount.GetDecimal();
        _productOpeningRepository.VmProductOpening.LocalAmount = TxtOpeningAmount.GetDecimal();
        _productOpeningRepository.VmProductOpening.Enter_By = ObjGlobal.LogInUser;
        _productOpeningRepository.VmProductOpening.Enter_Date = DateTime.Now;
        _productOpeningRepository.VmProductOpening.Reconcile_By = string.Empty;
        _productOpeningRepository.VmProductOpening.Reconcile_Date = DateTime.Now;
        _productOpeningRepository.VmProductOpening.CBranch_Id = ObjGlobal.SysBranchId.GetInt();
        _productOpeningRepository.VmProductOpening.CUnit_Id = ObjGlobal.SysCompanyUnitId.GetInt();
        _productOpeningRepository.VmProductOpening.FiscalYearId = ObjGlobal.SysFiscalYearId.GetInt();

        var syncRow = _productOpeningRepository.VmProductOpening.SyncRowVersion.ReturnSyncRowNo("POP", _txtVno.Text);
        _productOpeningRepository.VmProductOpening.SyncRowVersion = syncRow;

        return _productOpeningRepository.SaveProductOpeningSetup(_actionTag);
    }

    private void CheckShortName(string shortName)
    {
        while (true)
        {
            const int blankCharLength = 5;
            var table = shortName.IsDuplicate("PShortName", ProductId.ToString(), _actionTag, "PRODUCT");
            if (!table)
            {
                return;
            }
            var getNumber = string.Empty;
            getNumber = TxtShortName.Text.Where(char.IsDigit).Aggregate(getNumber, (current, t) => current + t);
            var desc = TxtDescription.Text.GetTrimReplace();
            var result = desc.Substring(0, 2);
            result = result.ToUpper();
            var lastNo = getNumber.GetInt();
            lastNo += 1;
            var maxLength = blankCharLength - lastNo.ToString().Length;
            var blankEnumerable = Enumerable.Repeat(0, maxLength);
            var generate = string.Join(string.Empty, blankEnumerable) + lastNo;
            shortName = result + generate;
            TxtShortName.Text = shortName;
        }
    }

    private void GetProductGroupInformation()
    {
        var dt = _productRepository.GetProductGroupLedgerDetails(_pGroupId);
        if (dt is not { Rows: { Count: > 0 } })
        {
            return;
        }
        foreach (DataRow row in dt.Rows)
        {
            _purchaseLedgerId = row["PurchaseLedgerId"].GetLong();
            if (_purchaseLedgerId == 0)
            {
                _purchaseLedgerId = ObjGlobal.PurchaseLedgerId;
                TxtPurchaseLedger.Text = _master.GetLedgerDescription(_purchaseLedgerId);
            }
            else
            {
                TxtPurchaseLedger.Text = row["PurchaseLedger"].GetString();
            }

            _purchaseReturnLedgerId = row["PurchaseReturnLedgerId"].GetLong();
            if (_purchaseReturnLedgerId == 0)
            {
                _purchaseReturnLedgerId = ObjGlobal.PurchaseReturnLedgerId;
                TxtPurchaseReturn.Text = _master.GetLedgerDescription(+_purchaseReturnLedgerId);
            }
            else
            {
                TxtPurchaseReturn.Text = row["PurchaseReturnLedger"].GetString();
            }

            _salesLedgerId = row["SalesLedgerId"].GetLong();
            if (_salesLedgerId == 0)
            {
                _salesLedgerId = ObjGlobal.SalesLedgerId;
                TxtSalesLedger.Text = _master.GetLedgerDescription(_salesLedgerId);
            }
            else
            {
                TxtSalesLedger.Text = row["SalesLedger"].GetString();
            }

            _salesReturnLedgerId = row["SalesReturnLedgerId"].GetLong();
            if (_salesReturnLedgerId is 0)
            {
                _salesReturnLedgerId = ObjGlobal.SalesReturnLedgerId;
                TxtSalesReturn.Text = _master.GetLedgerDescription(_salesReturnLedgerId);
            }
            else
            {
                TxtSalesReturn.Text = row["SalesReturnLedger"].GetString();
            }

            _openingLedgerId = row["OpeningStockLedgerId"].GetLong();
            if (_openingLedgerId is 0)
            {
                _openingLedgerId = ObjGlobal.StockOpeningStockLedgerId;
                TxtOpeningLedger.Text = _master.GetLedgerDescription(_openingLedgerId);
            }
            else
            {
                TxtOpeningLedger.Text = row["OpeningStockLedger"].GetString();
            }

            _closingLedgerId = row["ClosingStockLedgerId"].GetLong();
            if (_closingLedgerId is 0)
            {
                _closingLedgerId = ObjGlobal.StockClosingStockLedgerId;
                TxtClosingLedger.Text = _master.GetLedgerDescription(_closingLedgerId);
            }
            else
            {
                TxtClosingLedger.Text = row["ClosingStockLedger"].GetString();
            }

            _stockInHandLedgerId = row["StockInHandLedgerId"].GetLong();
            if (_stockInHandLedgerId is 0)
            {
                _stockInHandLedgerId = ObjGlobal.StockStockInHandLedgerId;
                TxtBSClosingLedger.Text = _master.GetLedgerDescription(_stockInHandLedgerId);
            }
            else
            {
                TxtBSClosingLedger.Text = row["StockInHandLedger"].GetString();
            }
        }
    }

    #endregion -------------- Method --------------

    // OBJECT FOR THIS FORM

    #region -------------- GLOBAL CLASS --------------

    public string ProductDesc = string.Empty;
    private string _shortName = string.Empty;
    public long ProductId;

    private int _pUnitId;
    private int _openingId;
    private int _pAltUnitId;
    private int _pGroupId;
    private int _pSubGroupId;
    private int _companyId;

    private long _purchaseLedgerId;
    private long _purchaseReturnLedgerId;
    private long _salesLedgerId;
    private long _salesReturnLedgerId;
    private long _openingLedgerId;
    private long _closingLedgerId;
    private long _stockInHandLedgerId;

    private string _fileExt = string.Empty;
    private string _saveFilePath = string.Empty;
    private string _docDesc = string.Empty;
    private string _actionTag;

    private DataTable _dtData = new();

    private readonly TextBox _txtVno = new();
    private readonly bool _isZoom;
    private readonly IMasterSetup _master;
    private readonly IProductRepository _productRepository;
    private readonly IProductOpeningRepository _productOpeningRepository;

    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;


    #endregion -------------- GLOBAL CLASS --------------

        
}