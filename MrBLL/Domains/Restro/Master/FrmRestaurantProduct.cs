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
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.Restro.Master;

public partial class FrmRestaurantProduct : MrForm
{
    // RESTAURANT PRODUCT

    #region "---------- FORM ----------"

    public FrmRestaurantProduct(bool isZoom)
    {
        InitializeComponent();
        _isZoom = isZoom;
        _actionTag = string.Empty;
        _master.BindProductType(CmbItemType);
        ClearControl();
        EnableDisable();
        _product = new ProductRepository();
    }

    private void FrmRestaurantProduct_Load(object sender, EventArgs e)
    {
        CmbItemType.SelectedIndex = 0;
        BtnNew.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() =>
            {
                GetAndSaveUnsynchronizedProducts();
            });
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete, BtnView], this.Tag);
    }

    private async void GetAndSaveUnsynchronizedProducts()
    {
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            {
                return;
            }
            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var getUrl = @$"{configParams.Model.Item2}Product/GetProductsByCallCount";
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);
            var productRepo = DataSyncProviderFactory.GetRepository<Product>(DataSyncManager.GetGlobalInjectData());
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            // pull all new product data
            var pullResponse = await _product.PullProductsServerToClientByRowCount(productRepo, 1);
            SplashScreenManager.CloseForm();

            // push all new product data
            var sqlSaQuery = _product.GetProductScript();
            var queryResponse = await QueryUtils.GetListAsync<Product>(sqlSaQuery);
            var productList = queryResponse.List.ToList();
            if (productList.Count > 0)
            {
                SplashScreenManager.ShowForm(typeof(PleaseWait));
                var pushResponse = await productRepo.PushNewListAsync(productList);
                SplashScreenManager.CloseForm();
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private void FrmRestaurantProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            if (!BtnNew.Enabled)
            {
                if (CustomMessageBox.ClearVoucherDetails("PRODUCT") == DialogResult.Yes)
                {
                    _actionTag = string.Empty;
                    EnableDisable();
                    ClearControl();
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
        if (e.KeyChar == (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar is (char)Keys.Space)
        {
            if (sender is System.Windows.Forms.ComboBox)
            {
                SendKeys.Send("{F4}");
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        EnableDisable(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        EnableDisable(true);
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        EnableDisable();
        ClearControl();
        TxtDescription.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (!IsFormValid())
        {
            return;
        }

        if (SaveProduct() != 0)
        {
            if (_isZoom)
            {
                ProductDesc = TxtDescription.Text;
                Close();
                return;
            }

            CustomMessageBox.ActionSuccess(TxtDescription.Text, "PRODUCT", _actionTag);
            ClearControl();
            TxtDescription.Focus();
            return;
        }

        CustomMessageBox.ActionError(TxtDescription.Text, "PRODUCT", _actionTag);
        TxtDescription.Focus();
        return;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtDescription.Text.Trim() == string.Empty)
        {
            BtnExit.PerformClick();
        }
        else
        {
            ClearControl();
        }
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (TxtDescription.IsValueExits() && _actionTag == "SAVE")
        {
            TxtShortName.Text = TxtDescription.GenerateShortName("PRODUCT", "PShortName");
        }

        if (!_actionTag.Equals("DELETE"))
        {
            var dtCheck = _master.IsDuplicate(_actionTag, "PRODUCT", TxtDescription.Text, ProductId.ToString());
            if (dtCheck.RowsCount() > 0)
            {
                this.NotifyValidationError(TxtDescription, $"{TxtDescription.Text} IS ALREADY EXITS..!!");
                return;
            }
        }
    }

    private void TxtDescription_Leave(object sender, EventArgs e)
    {
    }

    private void TxtDescription_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                TxtDescription.WarningMessage("DESCRIPTION IS REQUIRED");
                return;
            }

            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back && TxtDescription.ReadOnly)
        {
            TxtDescription.Clear();
            ProductId = 0;
        }
        else if (TxtDescription.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDescription, BtnDescription);
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDescription_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDescription.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtDescription, "SHORTNAME IS REQUIRED");
            }

            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtShortName_Leave(object sender, EventArgs e)
    {
    }

    private void TxtUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnUOM_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtUnit.Text, _unitId) = GetMasterList.CreateProductUnit(true);
            TxtUnit.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtUnit.Clear();
            _unitId = 0;
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

    private void TxtAltUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAltUOM_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtAltUnit.Text, _altUnitId) = GetMasterList.CreateProductUnit(true);
            TxtAltUnit.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtAltUnit.Clear();
            _altUnitId = 0;
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAltUnit, BtnAltUOM);
        }
    }

    private void TxtAltQtyCov_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtConvAltQty.GetDecimal() == 0 && _altUnitId > 0)
            {
                TxtConvAltQty.Text = 1.GetDecimalQtyString();
            }

            Global_KeyPress(sender, e);
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtQtyCov_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtConvAltQty.GetDecimal() == 0 && _altUnitId > 0)
            {
                TxtConvAltQty.Text = 1.GetDecimalQtyString();
            }

            Global_KeyPress(sender, e);
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
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

    private void TxtRate_Validating(object sender, CancelEventArgs e)
    {
        TxtRate.Text = TxtRate.GetDecimalString();
        if (TxtRate.GetDecimal() > 0 && TxtSalesRate.GetDecimal() is 0)
        {
            TxtSalesRate.Text = TxtVat.GetDecimal() > 0
                ? (TxtRate.GetDecimal() / (1 + TxtVat.GetDecimal() / 100)).GetDecimalString()
                : TxtRate.Text;
        }
    }

    private void TxtCategory_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnCategory_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtCategory.Text, _groupId) = GetMasterList.CreateProductGroup(true);
            TxtCategory.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtCategory.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtCategory, "CATEGORY IS REQUIRED");
            }

            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtCategory.Clear();
            _groupId = 0;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCategory, BtnCategory);
        }
    }

    private void BtnCategory_Click(object sender, EventArgs e)
    {
        var (description, id, margin) = GetMasterList.GetProductGroup(_actionTag);
        if (description.IsValueExits())
        {
            TxtCategory.Text = description;
            _groupId = id;
            if (_actionTag is "SAVE" or "NEW")
            {
                GenerateBarcode();
            }
        }

        TxtCategory.Focus();
    }

    private void TxtSubGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnSubGroup_Click(sender, e);
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.KeyCode is Keys.Back)
        {
            TxtSubGroup.Clear();
            _subGroupId = 0;
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtSubGroup.Text, _subGroupId) = GetMasterList.CreateProductSubGroup(true);
            TxtSubGroup.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubGroup, BtnSubGroup);
        }
    }

    private void BtnSubGroup_Click(object sender, EventArgs e)
    {
        (TxtSubGroup.Text, _subGroupId) = GetMasterList.GetProductSubGroup(_actionTag, _groupId);
        TxtSubGroup.Focus();
    }

    private void TxtVatRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnView_Click(object sender, EventArgs e)
    {
        GetMasterList.GetMasterProduct("SAVE");
        if (TxtDescription.Enabled)
        {
            TxtDescription.Focus();
        }
        else
        {
            BtnView.Focus();
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMasterProduct(_actionTag);
        if (description.IsValueExits())
        {
            TxtDescription.Text = description;
            ProductId = id;
            if (_actionTag != "SAVE")
            {
                FillData();
                TxtDescription.ReadOnly = false;
            }
        }
        TxtDescription.Focus();
    }

    private void BtnUOM_Click(object sender, EventArgs e)

    {
        (TxtUnit.Text, _unitId) = GetMasterList.GetProductUnit(_actionTag);
        qtyDesc.Text = TxtUnit.Text;
        TxtUnit.Focus();
    }

    private void BtnAltUOM_Click(object sender, EventArgs e)
    {
        (TxtAltUnit.Text, _altUnitId) = GetMasterList.GetProductUnit(_actionTag);
        AltUniDesc.Text = TxtUnit.Text;
        TxtAltUnit.Focus();
    }

    private void TxtAltUnit_TextChanged(object sender, EventArgs e)
    {
        if (!TxtAltUnit.Focused) return;
        TxtConvAltQty.Enabled = TxtConvQty.Enabled = _altUnitId > 0;
    }

    private void TxtConvAltQty_Validating(object sender, CancelEventArgs e)
    {
        TxtConvAltQty.GetDecimalQtyString();
    }

    private void TxtConvQty_Validating(object sender, CancelEventArgs e)
    {
        TxtConvQty.GetDecimalQtyString();
    }

    private void TxtSalesRate_Validating(object sender, CancelEventArgs e)
    {
        TxtSalesRate.Text = TxtSalesRate.GetDecimalString();
        TxtRate.Text = TxtVat.GetDecimal() > 0 ? (TxtSalesRate.GetDecimal() + TxtSalesRate.GetDecimal() * TxtVat.GetDecimal() / 100.GetDecimal()).GetDecimalString() : TxtSalesRate.Text;
    }

    private void TxtVat_TextChanged(object sender, EventArgs e)
    {
        TxtRate.Text = TxtVat.GetDecimal() > 0 ? (TxtSalesRate.GetDecimal() + TxtSalesRate.GetDecimal() * TxtVat.GetDecimal() / 100.GetDecimal()).GetDecimalString() : TxtSalesRate.Text;
    }

    private void TxtVat_Validating(object sender, CancelEventArgs e)
    {
        TxtVat.Text = TxtVat.GetDecimalString();
    }

    private void TxtItemCode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }
    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnsynchronizedProducts);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }
    #endregion "---------- FORM ----------"

    //METHOD FOR THIS FORM

    #region "---------- Method ----------"

    internal int SaveProduct()
    {
        ProductId = _actionTag is "SAVE" ? ProductId.ReturnMaxLongId("PRODUCT", "") : ProductId;
        _product.ObjProduct.PID = ProductId;
        _product.ObjProduct.PName = TxtDescription.Text.GetTrimReplace();
        _product.ObjProduct.PAlias = TxtDescription.Text.GetTrimReplace();
        _product.ObjProduct.PShortName = TxtShortName.Text.GetTrimReplace();

        _product.ObjProduct.PType = CmbItemType.SelectedValue.ToString();
        _product.ObjProduct.PCategory = "FG";

        _product.ObjProduct.PUnit = _unitId;
        _product.ObjProduct.PAltUnit = _altUnitId;
        _product.ObjProduct.PQtyConv = TxtConvAltQty.GetDecimal();

        _product.ObjProduct.PAltConv = TxtConvAltQty.Text.GetDecimal();
        _product.ObjProduct.PValTech = "FIFO";

        _product.ObjProduct.PSerialno = false;
        _product.ObjProduct.PSizewise = false;
        _product.ObjProduct.PBatchwise = false;

        _product.ObjProduct.PBuyRate = 0;
        _product.ObjProduct.PMRP = TxtRate.Text.GetDecimal();
        _product.ObjProduct.PSalesRate = TxtSalesRate.Text.GetDecimal();
        _product.ObjProduct.PMargin1 = 0;

        _product.ObjProduct.TradeRate = 0;
        _product.ObjProduct.PMargin2 = 0;
        _product.ObjProduct.PGrpId = _groupId;
        _product.ObjProduct.PSubGrpId = _subGroupId;

        _product.ObjProduct.PTax = TxtVat.Text.GetDecimal();

        _product.ObjProduct.PMin = 0;
        _product.ObjProduct.PMax = 0;
        _product.ObjProduct.CmpId = 0;

        _product.ObjProduct.PPL = 0;
        _product.ObjProduct.PPR = 0;
        _product.ObjProduct.PSL = 0;
        _product.ObjProduct.PSR = 0;

        _product.ObjProduct.PL_Opening = 0;
        _product.ObjProduct.PL_Closing = 0;
        _product.ObjProduct.BS_Closing = 0;

        _product.ObjProduct.PImage = null;
        _product.ObjProduct.Status = ChkActive.Checked;

        _product.ObjProduct.SyncRowVersion = _product.ObjProduct.SyncRowVersion.ReturnSyncRowNo("PRODUCT", ProductId.ToString());
        _product.ObjProduct.BeforeBuyRate = 0;
        _product.ObjProduct.BeforeSalesRate = 0;
        _product.ObjProduct.ChasisNo = _product.ObjProduct.EngineNo = _product.ObjProduct.VHModel = _product.ObjProduct.VHColor = string.Empty;
        _product.ObjProduct.Barcode = TxtItemCode.Text;
        _product.ObjProduct.Barcode1 = _product.ObjProduct.Barcode2 = _product.ObjProduct.Barcode3 = string.Empty;

        return _product.SaveProductInfo(_actionTag);
    }

    internal void EnableDisable(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable || _actionTag.Equals("DELETE");
        TxtDescription.Enabled = BtnDescription.Enabled = isEnable || _actionTag.Equals("DELETE");
        TxtShortName.Enabled = isEnable;
        CmbItemType.Enabled = isEnable;
        TxtUnit.Enabled = BtnUOM.Enabled = isEnable;
        TxtAltUnit.Enabled = BtnAltUOM.Enabled = isEnable;
        TxtRate.Enabled = TxtSalesRate.Enabled = isEnable;
        TxtSubGroup.Enabled = BtnSubGroup.Enabled = isEnable;
        TxtVat.Enabled = isEnable;
        ChkActive.Enabled = _actionTag.Equals("UPDATE");
        TxtCategory.Enabled = BtnCategory.Enabled = isEnable;
        BtnSubGroup.Enabled = isEnable;
        BtnSave.Enabled = BtnCancel.Enabled = isEnable;
    }

    internal void ClearControl()
    {
        Text = _actionTag.IsValueExits() ? $@"RESTRO MENU SETUP [{_actionTag}]" : @"RESTRO MENU SETUP";
        ProductId = 0;
        TxtDescription.Clear();
        TxtShortName.Clear();
        _unitId = 0;
        TxtUnit.Clear();
        _altUnitId = 0;
        TxtAltUnit.Clear();
        ChkActive.Checked = true;
        TxtConvAltQty.Enabled = TxtConvQty.Enabled = false;
        TxtDescription.ReadOnly = _actionTag.ToUpper() != "SAVE";
        _groupId = 0;
        TxtCategory.Clear();
        _subGroupId = 0;
        TxtSubGroup.Clear();
        TxtItemCode.Clear();
        TxtRate.Clear();
        TxtSalesRate.Clear();
        TxtVat.Text = 13.GetDecimalString();
        AltUniDesc.IsClear();
        qtyDesc.IsClear();
        TxtDescription.Focus();
    }

    internal void GenerateBarcode()
    {
        if (_groupId <= 0) return;
        var bb = _groupId.ToString();
        var dtProd = SqlExtensions.ExecuteDataSet(
                $"SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(ISNULL(Barcode,0), PATINDEX('%[0-9]%', Barcode), PATINDEX('%[0-9][^0-9]%', ISNULL(Barcode,0) + 't') - PATINDEX('%[0-9]%',ISNULL(Barcode,0))) AS NUMERIC)),0) + 1 AS PID FROM AMS.Product WHERE PGrpId = '{_groupId}'")
            .Tables[0];
        var pc = string.Empty;
        if (dtProd.Rows.Count <= 0)
        {
            TxtItemCode.Text = "00001";
            return;
        }
        else
        {
            try
            {
                var productId = dtProd.Rows[0]["PID"].GetInt();
                var cc = productId.ToString();
                cc = productId > 1 ? productId.ToString().Substring(bb.Length, cc.Length - bb.Length) : "0";
                long.TryParse(cc, out var cResult);
                cc = (cResult + 1).ToString();
                var aa = cc;
                double cc1 = aa.Length;
                var cc2 = 0;
                if (3 - cc1 > 0) cc2 = (int)(3 - cc1);

                int i;
                for (i = 1; i <= cc2; i++) pc += "0";
                pc += cc;
                var grpCode = $"SELECT GrpCode FROM AMS.ProductGroup WHERE PGrpId={_groupId}".GetQueryData();
                TxtItemCode.Text = grpCode + pc;
            }
            catch (Exception ex)
            {
                TxtItemCode.Text = @"10001";
                ex.ToNonQueryErrorResult(ex.StackTrace);
                var erMsg = ex.Message;
            }
        }
    }

    internal void FillData()
    {
        var dt = _master.GetMasterProductList(_actionTag, ProductId);
        if (dt == null || dt.Rows.Count <= 0)
        {
            return;
        }
        TxtDescription.Text = dt.Rows[0]["PName"].ToString();
        TxtShortName.Text = dt.Rows[0]["PShortName"].ToString();
        TxtItemCode.Text = dt.Rows[0]["Barcode"].ToString();
        _unitId = dt.Rows[0]["PUnit"].GetInt();
        TxtUnit.Text = dt.Rows[0]["UnitCode"].ToString();
        _altUnitId = dt.Rows[0]["PAltUnit"].GetInt();
        TxtAltUnit.Text = dt.Rows[0]["AltUnitCode"].ToString();
        CmbItemType.SelectedIndex = CmbItemType.FindString(dt.Rows[0]["PType"].GetUpper());
        _groupId = dt.Rows[0]["PGrpId"].GetInt();
        TxtCategory.Text = dt.Rows[0]["GrpName"].ToString();
        _subGroupId = dt.Rows[0]["PSubGrpId"].GetInt();
        TxtSubGroup.Text = dt.Rows[0]["SubGrpName"].ToString();
        TxtSalesRate.Text = dt.Rows[0]["PSalesRate"].GetDecimalString();
        TxtVat.Text = dt.Rows[0]["PTax"].GetDecimalString();
        TxtRate.Text = dt.Rows[0]["PMRP"].GetDecimalString();
        TxtItemCode.Text = dt.Rows[0]["Barcode"].ToString();
        ChkActive.Checked = dt.Rows[0]["Status"].GetBool();
    }

    internal bool IsFormValid()
    {
        if (TxtDescription.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage($"DESCRIPTION IS MANDATORY FOR {_actionTag}");
            return false;
        }

        if (_actionTag.Equals("DELETE"))
        {
            if (CustomMessageBox.FormAction(_actionTag, "PRODUCT") == DialogResult.No)
            {
                return false;
            }
        }
        else
        {
            if (TxtShortName.IsBlankOrEmpty())
            {
                TxtShortName.WarningMessage($"SHORT NAME IS MANDATORY FOR {_actionTag}");
                return false;
            }
            if (TxtCategory.IsBlankOrEmpty())
            {
                TxtCategory.WarningMessage($"CATEGORY IS MANDATORY FOR {_actionTag}");
                return false;
            }
            if (TxtSalesRate.GetDecimal() is 0)
            {
                TxtSalesRate.WarningMessage($"SALES RATE IS MANDATORY FOR {_actionTag}");
                return false;
            }
            if (TxtUnit.IsBlankOrEmpty() || _unitId == 0)
            {
                TxtUnit.WarningMessage($"SHORT NAME IS MANDATORY FOR {_actionTag}");
                return false;
            }
        }

        return true;
    }

    #endregion "---------- Method ----------"

    // OBJECT FOR THIS FORM

    #region "---------- Class ----------"

    public string ProductDesc;
    public long ProductId;
    private int _unitId;
    private int _altUnitId;
    private int _groupId;
    private int _subGroupId;
    private bool _isZoom;
    private string _actionTag;
    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly IProductRepository _product;

    #endregion "---------- Class ----------"
}