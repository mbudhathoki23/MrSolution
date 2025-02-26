using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.Control;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Utility.Server;

namespace MrBLL.Domains.POS.Master;

public partial class FrmPosProduct : MrForm
{
    #region --------------- Frm ---------------

    public FrmPosProduct(bool isZoom)
    {
        InitializeComponent();
        _isZoom = isZoom;
        _objMaster = new ClsMasterSetup();
        _btnExits = new SimpleButton();
        _product = new ProductRepository();
    }

    private void FrmCounterProduct_Load(object sender, EventArgs e)
    {
        ClearControl();
        TxtBarcode.Focus();
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedProducts);
        }
    }

    private async void GetAndSaveUnSynchronizedProducts()
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
            var errMsg = e.Message;
        }
    }

    private void FrmPOSProduct_Shown(object sender, EventArgs e)
    {
        TxtBarcode.Focus();
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void Ctrl_Leave(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.AliceBlue;
    }

    private void Ctrl_Enter(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.LightPink;
    }

    private void FrmCounterProduct_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void FrmPOSProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (TxtBarcode.IsValueExits() || TxtDescription.IsValueExits())
            {
                if (CustomMessageBox.ClearVoucherDetails("POS PRODUCT") is DialogResult.Yes)
                {
                    ClearControl();
                    TxtBarcode.Focus();
                }
            }
            else
            {
                Close();
            }
        }
    }

    private void TxtBarcode_Validating(object sender, CancelEventArgs e)
    {
        if (TabPOS.SelectedTabPageIndex == 1)
        {
            return;
        }

        if (TxtBarcode.IsValueExits())
        {
            var dtProduct = _objMaster.GetProductInfoWithBarcode(TxtBarcode.Text.GetTrimReplace());
            if (dtProduct.Rows.Count == 1)
            {
                BindSelectedProduct(dtProduct.Rows[0]["SelectedId"].GetLong());
            }
            else if (dtProduct.Rows.Count > 1)
            {
                ProductId = dtProduct.Rows[0]["SelectedId"].GetLong();
                using var frmPickList = new FrmAutoPopList(dtProduct);
                frmPickList.ShowDialog();
                ProductId = frmPickList.SelectedList[0]["SelectedId"].GetLong();
                BindSelectedProduct(ProductId.GetLong());
            }

            if (ProductId > 0)
            {
                var dt = TxtBarcode.CheckValueExits(_actionTag, "PRODUCT", "PShortName", ProductId);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }

                if (CustomMessageBox.Question(@"BARCODE IS ALREADY EXITS..!! DO YOU WANT TO CREATE NEW..??") ==
                    DialogResult.Yes)
                {
                    TxtBarcode.Text = CheckShortName(TxtBarcode.Text);
                }

                TxtBarcode.Focus();
            }
        }
    }

    private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnBarcode_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            Global_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (_actionTag.ToUpper() == "DELETE")
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBarcode, BtnBarcode);
        }
    }

    private void TxtBarcode1_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void TxtBarcode1_Validating(object sender, CancelEventArgs e)
    {
        if (TxtBarcode1.IsValueExits() && _actionTag is "SAVE")
        {
            TxtBarcode1.Text = CheckBarcode1(TxtBarcode1.Text);
        }
    }

    private void TxtDescription_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtDescription.IsValueExits())
        {
            var result = TxtDescription.IsDuplicate("PName", ProductId, _actionTag, "P");
            if (!result)
            {
                return;
            }

            TxtDescription.WarningMessage("PRODUCT NAME IS ALREADY EXITS");
            TxtDescription.Focus();
        }

        if (TxtDescription.IsBlankOrEmpty() && _actionTag.ActionValid())
        {
            if (ActiveControl.Name == "TxtBarcode")
            {
                return;
            }

            if (TxtDescription.ValidControl(ActiveControl))
            {
                TxtDescription.WarningMessage("PRODUCT DESCRIPTION IS REQUIRED ..!!");
            }
        }
    }

    private void BtnBarcode_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetCounterProduct(TxtBarcode.Text);
        if (!description.IsValueExits())
        {
            return;
        }

        TxtDescription.Text = description;
        ProductId = id;
        BindSelectedProduct(ProductId);
        _actionTag = "UPDATE";
        TxtBarcode.ReadOnly = false;
        Text = @"COUNTER PRODUCT [UPDATE]";
        TxtBarcode.Focus();
    }

    private void TxtBarcode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtBarcode1_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnBarcode1_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMasterProduct(_actionTag);
        if (!description.IsValueExits())
        {
            return;
        }

        TxtDescription.Text = description;
        if (_actionTag == "SAVE")
        {
            return;
        }

        TxtDescription.Text = description;
        ProductId = id;
        BindSelectedProduct(ProductId);
        TxtBarcode1.Focus();
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMasterProduct(_actionTag);
        if (!description.IsValueExits())
        {
            return;
        }

        TxtDescription.Text = description;
        if (_actionTag == "SAVE")
        {
            return;
        }

        TxtDescription.Text = description;
        ProductId = id;
        BindSelectedProduct(ProductId);
        TxtDescription.Focus();
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
                TxtDescription.WarningMessage("PRODUCT DESCRIPTION IS REQUIRED..!!");
            }
        }
    }

    private void TxtGroup_Validating(object sender, CancelEventArgs e)
    {
        _groupId = string.IsNullOrEmpty(TxtSubCategory.Text) switch
        {
            false when _subGroupId is 0 && _actionTag != "SAVE" => _objMaster.ReturnIntValueFromTable(
                "AMS.ProductGroup", "PGrpID", "GrpName", TxtCategory.Text.Replace("'", "''")),
            _ => _groupId
        };
    }

    private void BtnProductUnit_Click(object sender, EventArgs e)
    {
        var (unit, id) = GetMasterList.GetProductUnit(_actionTag);
        if (id > 0)
        {
            TxtUnit.Text = unit;
            _pUnitId = id;
        }

        TxtUnit.Focus();
    }

    private void BtnProductGroup_Click(object sender, EventArgs e)
    {
        var (description, id, margin) = GetMasterList.GetProductGroup(_actionTag);
        if (description.IsValueExits())
        {
            _groupId = id;
            TxtCategory.Text = description;
            TxtMargin.Text = margin.GetDecimalString();
            if (_actionTag is not ("SAVE" or "NEW"))
            {
                return;
            }

            GenerateBarcode();
            if (TxtBarcode.IsBlankOrEmpty())
            {
                TxtBarcode.Text = TxtBarcode2.Text;
            }
        }
    }

    private void TxtGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtCategory.Text))
            {
                BtnProductGroup_Click(sender, e);
            }
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnProductGroup_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var frm = new FrmProductGroup(true);
            frm.ShowDialog();
            TxtCategory.Text = frm.ProductGroupDesc;
            _groupId = frm.GroupId;
            GenerateBarcode();
            TxtCategory.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCategory, BtnGroup);
        }
    }

    private void TxtProductId_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtProductCode_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtBarcode2.Text.Trim().Replace("'", "''")))
        {
            if (TxtBarcode2.IsBlankOrEmpty() && _actionTag is "SAVE")
            {
                GenerateBarcode();
            }

            if (!string.IsNullOrEmpty(TxtBarcode2.Text) && _actionTag is "SAVE")
            {
                var dtBarcode = _objMaster.IsExitsBarcode(TxtBarcode2.Text.Replace("'", "''"));
                if (dtBarcode.Rows.Count > 0)
                {
                    TxtBarcode2.Text = (long.Parse(TxtBarcode2.Text.Replace("'", "''")) + 1).ToString();
                    TxtProductCode_Validating(sender, e);
                }
                else if (string.IsNullOrEmpty(TxtBarcode.Text))
                {
                    TxtBarcode.Text = TxtBarcode2.Text;
                }
            }
        }

        if (!string.IsNullOrEmpty(TxtBarcode1.Text.Trim().Replace("'", "''")) && _actionTag is "SAVE")
        {
            var dt = _objMaster.CheckIsValidData(_actionTag, "Product", "Barcode", "PID",
                TxtBarcode1.Text.Trim().Replace("'", "''"), ProductId.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                TxtBarcode1.Text = (TxtBarcode1.Text.GetDecimal() + 0.01.GetDecimal()).GetDecimalString();
                TxtProductCode_Validating(sender, e);
            }
        }
    }

    private void TxtUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnProductUnit_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtUnit.IsBlankOrEmpty())
            {
                TxtUnit.WarningMessage("PRODUCT UNIT IS REQUIRED..!!");
            }
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateProductUnit(true);
            if (!description.IsValueExits())
            {
                return;
            }

            TxtUnit.Text = description;
            _pUnitId = id;
            TxtUnit.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtUnit, BtnUnit);
        }
    }

    private void TxtUnit_Validating(object sender, CancelEventArgs e)
    {
        if (TxtUnit.IsBlankOrEmpty() && _actionTag.ActionValid())
        {
            if (TxtUnit.ValidControl(ActiveControl))
            {
                TxtUnit.WarningMessage("PRODUCT UNIT IS REQUIRED..!!");
            }
        }
    }

    private void TxtMRP_Leave(object sender, EventArgs e)
    {
        TxtMRP.Text = TxtMRP.GetDecimalString();
    }

    private void BtnAltUnit_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProductUnit(_actionTag);
        if (id > 0)
        {
            TxtAltUnit.Text = description;
            _pAltUnitId = id;
            TxtQty.Enabled = TxtAltQty.Enabled = true;
        }
        else
        {
            TxtQty.Enabled = TxtAltQty.Enabled = true;
        }

        TxtAltUnit.Focus();
    }

    private void TxtSubCategory_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnSubGroup_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateProductSubGroup(true);
            if (id > 0)
            {
                TxtSubCategory.Text = description;
                _subGroupId = id;
            }

            TxtSubCategory.Focus();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            // TabPOS.SelectedTabPage = tbAdditional;
            // TxtAltQty.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubCategory, BtnSubGroup);
        }
    }

    private void TxtCategory_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && string.IsNullOrEmpty(TxtCategory.Text))
        {
            if (!string.IsNullOrEmpty(_actionTag) && TxtCategory.Focused)
            {
                MessageBox.Show(@"PRODUCT GROUP IS REQUIRED..!!", ObjGlobal.Caption);
                TxtCategory.Focus();
            }
        }
    }

    private void TxtPurchaseRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtPurchaseRate_Validating(object sender, CancelEventArgs e)
    {
        TxtBuyRate.Text = TxtBuyRate.GetDecimalString();
        isPurchaseExits = TxtBuyRate.GetDecimal() > 0;
    }

    private void TxtMargin_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            TxtMargin.Text = TxtMargin.GetDecimalString();
            Global_KeyPress(sender, e);
        }
        else if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtMargin_Validated(object sender, EventArgs e)
    {
        TxtMargin.Text = TxtMargin.GetDecimalString();
    }

    private void TxtMargin_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtMargin_TextChanged(object sender, EventArgs e)
    {
        CalculateSalesRate();
    }

    private void TxtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtSalesRate_TextChanged(object sender, EventArgs e)
    {
        if (TxtSalesRate.Focused)
        {
            CalculateMargin();
        }
    }

    private void TxtSalesRate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtSalesRate.GetDouble() is 0)
            {
                TxtSalesRate.WarningMessage("SALES RATE IS REQUIRED..!!");
                return;
            }

            TabPOS.SelectedTabPage = tbImage;
            BtnSave.Focus();
        }
    }

    private void TxtSalesRate_Validating(object sender, CancelEventArgs e)
    {
        CalculateMargin();
        TxtSalesRate.Text = TxtSalesRate.GetDecimalString();
    }

    private void ChkBarcodeList_CheckedChanged(object sender, EventArgs e)
    {
        if (ProductId is 0 && ChkBarcodeList.Focused && _actionTag.Equals("UPDATE"))
        {
            MessageBox.Show(@"PLEASE SELECT THE PRODUCT TO MODIFY THE BARCODE..!!", ObjGlobal.Caption);
            BtnBarcode.PerformClick();
        }

        if (ChkBarcodeList.Checked)
        {
            var getList = new FrmBarcodeList(ProductId, _barcodeView);
            getList.ShowDialog(this);
            _barcodeView = getList.DGrid;
        }
    }

    private void ChkIsTaxable_CheckedChanged(object sender, EventArgs e)
    {
        TxtTaxRate.Text = ChkIsTaxable.Checked ? 13.GetDecimalString() : 0.GetDecimalString();
        CalculateSalesRate();
    }

    private void TxtTaxRate_Validating(object sender, CancelEventArgs e)
    {
        TxtTaxRate.Text = ChkIsTaxable.Checked ? TxtTaxRate.GetDecimalString() : 0.GetDecimalString();
    }

    private void TxtSubCategory_Validating(object sender, CancelEventArgs e)
    {
        if (!string.IsNullOrEmpty(TxtSubCategory.Text) && _subGroupId is 0)
        {
            _subGroupId = _objMaster.ReturnIntValueFromTable("AMS.ProductSubGroup", "PSubGrpId", "SubGrpName",
                TxtSubCategory.Text.Replace("'", "''"));
        }
    }

    private void TxtMRP_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
        }
    }

    private void TxtAltQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtAltQty_Validating(object sender, CancelEventArgs e)
    {
        TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
    }

    private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnSubGroup_Click(object sender, EventArgs e)
    {
        using var frmPickList = new FrmAutoPopList("MED", "PRODUCTSUBGROUP", _actionTag, ObjGlobal.SearchText,
            _groupId.ToString(), "LIST");
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
            {
                TxtSubCategory.Text = frmPickList.SelectedList[0]["Description"].ToString().Trim();
                _subGroupId = Convert.ToInt32(frmPickList.SelectedList[0]["LedgerId"].ToString().Trim());
            }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"PRODUCT SUBGROUP ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            TxtSubCategory.Focus();
            return;
        }

        ObjGlobal.SearchText = string.Empty;
        TxtSubCategory.Focus();
    }

    private void TxtAltUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            BtnAltUnit_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var frmUnit = new FrmProductUnit(true);
            frmUnit.ShowDialog(this);
            _pAltUnitId = frmUnit.ProductUnitId;
            TxtAltUnit.Text = frmUnit.ProductUnitName;
            TxtAltUnit.Focus();
            TxtAltQty.Enabled = TxtQty.Enabled = true;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAltUnit, BtnAltUnit);
        }
    }

    private void TxtBuyRate_TextChanged(object sender, EventArgs e)
    {
        if (TxtBuyRate.Focused)
        {
            CalculateSalesRate();
        }
    }

    private void PictureEdit1_DoubleClick(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var fileName = dlg.FileName;
            isFileExists = dlg.FileName;
            var imagery = new Bitmap(fileName);
            pictureEdit1.Image = imagery;
            pictureEdit1.Properties.SizeMode = PictureSizeMode.Stretch;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            if (isFileExists != string.Empty)
            {
                MessageBox.Show(@"PICTURE FILE FORMAT & " + ex.Message, ObjGlobal.Caption);
            }
        }
    }

    private void PictureEdit1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            BtnSave.Focus();
        }
    }

    private void LinkAttachment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var pcvBox = new PictureBox { Image = pictureEdit1.Image };
        PreviewImage(pcvBox);
    }

    private void ChkIsTaxable_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            TxtSalesRate.Focus();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveProduct() > 0)
            {
                if (_isZoom)
                {
                    ProductDesc = TxtDescription.Text.Trim();
                    Close();
                    return;
                }

                CustomMessageBox.ActionSuccess(TxtDescription.Text, "PRODUCT", _actionTag);
                ClearControl();
                TabPOS.SelectedTabPage = tbProduct;
                TxtBarcode2.Enabled = true;
                TxtBarcode.Focus();
            }
            else
            {
                CustomMessageBox.ActionError(TxtDescription.Text, "PRODUCT", _actionTag);
                TxtBarcode.Focus();
            }
        }
        else
        {
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(TxtDescription.Text.Trim()) || string.IsNullOrEmpty(TxtBarcode.Text.Trim()) ||
            string.IsNullOrEmpty(TxtBarcode1.Text.Trim()))
        {
            Close();
        }
        else
        {
            ClearControl();
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

    #endregion --------------- Frm ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private int SaveProduct()
    {
        if (_actionTag is "SAVE")
        {
            ProductId = ProductId.ReturnMaxLongId("PRODUCT", ProductId.ToString());
        }

        _product.ObjProduct.PID = ProductId;
        _product.ObjProduct.PName = TxtDescription.GetTrimReplace();
        _product.ObjProduct.PAlias = TxtDescription.GetTrimReplace();
        _product.ObjProduct.PShortName = TxtBarcode.GetTrimReplace();
        _product.ObjProduct.HsCode = TxtHsCode.GetTrimReplace();
        _product.ObjProduct.PType = "I";
        _product.ObjProduct.PCategory = "FG";
        _product.ObjProduct.PUnit = _pUnitId;
        _product.ObjProduct.PAltUnit = _pAltUnitId;
        _product.ObjProduct.PQtyConv = TxtQty.GetDecimal();
        _product.ObjProduct.PAltConv = TxtAltQty.GetDecimal();
        _product.ObjProduct.PValTech = "FIFO";
        _product.ObjProduct.PSerialno = _product.ObjProduct.PSizewise = false;
        _product.ObjProduct.PBuyRate = TxtBuyRate.GetDecimal();
        _product.ObjProduct.PMRP = TxtMRP.GetDecimal();
        _product.ObjProduct.TradeRate = _product.ObjProduct.PMargin2 = 0;
        _product.ObjProduct.PSalesRate = TxtSalesRate.GetDecimal();
        _product.ObjProduct.PMargin1 = TxtMargin.GetDecimal();

        if (pictureEdit1.Image != null && pictureEdit1.Image.IsValueExits())
        {
            var converter = new ImageConverter();
            var arr = (byte[])converter.ConvertTo(pictureEdit1?.Image!, typeof(byte[]));
            _product.ObjProduct.PImage = arr;
        }

        _product.ObjProduct.PGrpId = _groupId;
        _product.ObjProduct.PSubGrpId = _subGroupId;
        _product.ObjProduct.PTax = ChkIsTaxable.Checked ? 13 : 0;
        _product.ObjProduct.PMin = _product.ObjProduct.PMax = 0;
        _product.ObjProduct.CmpId = 0;
        _product.ObjProduct.PPL = _product.ObjProduct.PPR = _product.ObjProduct.PSL = _product.ObjProduct.PSR =
            _product.ObjProduct.PL_Opening = _product.ObjProduct.PL_Closing = _product.ObjProduct.BS_Closing = 0;
        _product.ObjProduct.Status = ChkActive.Checked;
        _product.ObjProduct.PBatchwise = ChkBatchWise.Checked;
        _product.ObjProduct.BeforeBuyRate = TxtBuyRate.GetDecimal();
        _product.ObjProduct.BeforeSalesRate = ChkIsTaxable.Checked
            ? TxtSalesRate.GetDecimal() / 1.13.GetDecimal()
            : TxtSalesRate.GetDecimal();
        _product.ObjProduct.Barcode = TxtBarcode1.Text;
        _product.ObjProduct.Barcode1 = TxtBarcode2.Text;
        _product.ObjProduct.ChasisNo = _product.ObjProduct.VHModel = _product.ObjProduct.VHColor = string.Empty;
        _product.ObjProduct.SyncRowVersion =
            _product.ObjProduct.SyncRowVersion.ReturnSyncRowNo("PRODUCT", ProductId.ToString());
        if (_barcodeView.RowCount > 0)
        {
            _product.BarcodeLists.Clear();
            var barcodeList = new BarcodeList();
            foreach (DataGridViewRow row in _barcodeView.Rows)
            {
                var isAltUnit = false;
                if (row.Cells["GTxtAltUnit"].Value != null)
                {
                    isAltUnit = row.Cells["GTxtAltUnit"].Value.GetBool();
                }

                barcodeList.ProductId = ProductId;
                if (row.Cells["GTxtBarcode"].Value != null)
                {
                    barcodeList.Barcode = row.Cells["GTxtBarcode"].Value.ToString();
                }

                if (row.Cells["GTxtSalesRate"].Value != null)
                {
                    barcodeList.SalesRate = row.Cells["GTxtSalesRate"].Value.GetDecimal();
                }

                if (row.Cells["GTxtMRP"].Value != null)
                {
                    barcodeList.MRP = row.Cells["GTxtMRP"].Value.GetDecimal();
                }

                if (row.Cells["GTxtTrade"].Value != null)
                {
                    barcodeList.Trade = row.Cells["GTxtTrade"].Value.GetDecimal();
                }

                if (row.Cells["GTxtWholesales"].Value != null)
                {
                    barcodeList.Wholesale = row.Cells["GTxtWholesales"].Value.GetDecimal();
                }

                if (row.Cells["GTxtRetails"].Value != null)
                {
                    barcodeList.Retail = row.Cells["GTxtRetails"].Value.GetDecimal();
                }

                if (row.Cells["GTxtDealer"].Value != null)
                {
                    barcodeList.Dealer = row.Cells["GTxtDealer"].Value.GetDecimal();
                }

                if (row.Cells["GTxtReseller"].Value != null)
                {
                    barcodeList.Resellar = row.Cells["GTxtReseller"].Value.GetDecimal();
                }

                if (row.Cells["GTxtUnitId"].Value != null)
                {
                    barcodeList.UnitId = row.Cells["GTxtUnitId"].Value.GetInt();
                }

                if (row.Cells["GTxtUnitId"].Value != null)
                {
                    barcodeList.AltUnitId = isAltUnit ? row.Cells["GTxtUnitId"].Value.GetInt() : 0;
                }

                barcodeList.DailyRateChange = false;
                _product.BarcodeLists.Add(barcodeList);
            }

            _product.ObjProduct.GetView = _barcodeView;
        }

        _product.ObjProduct.PublicationWise = false;
        return _product.SaveProductInfo(_actionTag);
    }

    private bool IsValidForm()
    {
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (_actionTag.IsValueExits() && _actionTag != "SAVE")
        {
            if (ProductId is 0)
            {
                TxtBarcode.WarningMessage("SELECTED PRODUCT IS INVALID..!!");
                return false;
            }
        }

        if (_pUnitId is 0 || TxtUnit.IsBlankOrEmpty())
        {
            TxtUnit.WarningMessage("UNIT IS REQUIRED..!!");
            return false;
        }

        if (TxtDescription.Text.IsBlankOrEmpty())
        {
            TxtDescription.WarningMessage("DESCRIPTION IS BLANK..!!");
            return false;
        }

        if (TxtBarcode.Text.IsBlankOrEmpty())
        {
            TxtBarcode.Text = TxtBarcode2.Text;
        }

        if (TxtBarcode1.Text.IsBlankOrEmpty())
        {
            TxtBarcode1.Text = TxtBarcode.Text;
        }

        if (TxtBarcode2.Text.IsBlankOrEmpty())
        {
            TxtBarcode2.Text = TxtBarcode.Text;
        }

        if (TxtCategory.Text.IsBlankOrEmpty() || _groupId is 0)
        {
            TabPOS.SelectedTabPageIndex = 0;
            TxtCategory.WarningMessage("PRODUCT CATEGORY CANNOT BE BLANK..!!");
            return false;
        }

        if (TxtSalesRate.Text.GetDouble() is 0)
        {
            TabPOS.SelectedTabPageIndex = 0;
            TxtSalesRate.WarningMessage("SALES RATE CANNOT BE ZERO..!!");
            return false;
        }

        return true;
    }

    private void CalculateSalesRate()
    {
        var taxRate = TxtTaxRate.GetDecimal();
        var margin = TxtMargin.GetDecimal();
        var buyRate = TxtBuyRate.GetDecimal();
        if (margin < 0 || buyRate < 0)
        {
            TxtSalesRate.Text = TxtBuyRate.Text;
            return;
        }

        var marginRate = buyRate + buyRate * margin / 100;
        TxtSalesRate.Text = ChkIsTaxable.Checked
            ? (marginRate * 1.13.GetDecimal()).GetDecimalString()
            : marginRate.GetDecimalString();
    }

    private void CalculateMargin()
    {
        var taxRate = TxtTaxRate.GetDecimal();
        var margin = TxtMargin.GetDecimal();
        var salesRate = TxtSalesRate.GetDecimal();
        if (salesRate <= 0)
        {
            return;
        }

        salesRate = ChkIsTaxable.Checked ? salesRate / (1 + taxRate / 100) : salesRate;
        var marginRation = ("1." + margin).GetDecimal();
        var buyRate = marginRation > 0 ? salesRate / marginRation : salesRate;
        //TxtBuyRate.Text = isPurchaseExits ? TxtBuyRate.Text : buyRate.ToString(ObjGlobal.SysAmountFormat);
    }

    private void ClearControl()
    {
        _actionTag = "SAVE";
        Text = @"COUNTER PRODUCT [NEW]";
        BtnSave.Text = @"&SAVE";
        _groupId = 0;
        _subGroupId = 0;
        _pAltUnitId = 0;
        _pUnitId = 0;
        ProductId = 0;
        TxtAltQty.Clear();
        TxtQty.Clear();
        TxtAltQty.Enabled = false;
        TxtQty.Enabled = false;
        TxtBarcode1.Clear();
        TxtDescription.Clear();
        TxtBarcode2.Clear();
        TxtCategory.Clear();
        TxtSubCategory.Clear();
        TxtAltUnit.Clear();
        TxtUnit.Clear();
        ChkIsTaxable.Checked = true;
        TxtBuyRate.Clear();
        TxtSalesRate.Clear();
        TxtMRP.Clear();
        TxtMargin.Clear();
        TxtBarcode.Clear();
        pictureEdit1.Image = null;
        ChkBarcodeList.Checked = false;
        ChkIsTaxable.Checked = true;
        _barcodeView = new EntryGridViewEx();
        TabPOS.SelectedTabPageIndex = 0;
    }

    private void BindSelectedProduct(long selectedProductId)
    {
        _product.ObjProduct.Barcode3 = string.Empty;
        BtnSave.Text = @"&UPDATE";
        _actionTag = "UPDATE";
        Text = @"COUNTER PRODUCT [UPDATE]";
        var dtTemp = _objMaster.GetMasterProductList(_actionTag, selectedProductId);

        if (TabPOS.SelectedTabPage.Text == "Product Details")
        {
            if (dtTemp.Rows.Count <= 0)
            {
                return;
            }

            TxtDescription.Text = dtTemp.Rows[0]["PName"].ToString();
            TxtBarcode.Text = dtTemp.Rows[0]["PShortName"].ToString();
            TxtBarcode1.Text = dtTemp.Rows[0]["Barcode"].ToString();
            TxtBarcode2.Text = dtTemp.Rows[0]["Barcode1"].ToString();
            _product.ObjProduct.Barcode2 = dtTemp.Rows[0]["Barcode2"].ToString();
            _product.ObjProduct.Barcode3 = dtTemp.Rows[0]["Barcode3"].ToString();
            ProductId = dtTemp.Rows[0]["PID"].GetLong();
            _groupId = dtTemp.Rows[0]["PGrpId"].GetInt();
            _subGroupId = dtTemp.Rows[0]["PSubGrpId"].GetInt();

            TxtCategory.Text = dtTemp.Rows[0]["GrpName"].ToString();
            TxtSubCategory.Text = dtTemp.Rows[0]["SubGrpName"].ToString();

            TxtUnit.Text = dtTemp.Rows[0]["UnitCode"].ToString();
            _pUnitId = dtTemp.Rows[0]["PUnit"].GetInt();

            TxtAltUnit.Text = dtTemp.Rows[0]["AltUnitCode"].ToString();
            _pAltUnitId = dtTemp.Rows[0]["PAltUnit"].GetInt();

            TxtAltQty.Text = dtTemp.GetDecimalString();
            TxtQty.Text = dtTemp.GetDecimalString();
            TxtMRP.Text = dtTemp.GetDecimalString();

            ChkActive.Checked = dtTemp.Rows[0]["Status"].GetBool();
            var vatAmount = dtTemp.Rows[0]["PTax"].GetDecimal();
            TxtBuyRate.Text = dtTemp.Rows[0]["PBuyRate"].GetDecimalString();
            TxtMargin.Text = dtTemp.Rows[0]["PMargin1"].GetDecimalString();
            ChkIsTaxable.Checked = vatAmount > 0;
            TxtSalesRate.Text = dtTemp.Rows[0]["PSalesRate"].GetDecimalString();
        }

        if (TabPOS.SelectedTabPage.Text == "Product Image")
        {
            if (dtTemp.Rows.Count <= 0)
            {
                return;
            }

            TxtDescription.Text = dtTemp.Rows[0]["PName"].ToString();
            TxtBarcode.Text = dtTemp.Rows[0]["PShortName"].ToString();
            TxtBarcode1.Text = dtTemp.Rows[0]["Barcode"].ToString();
            TxtBarcode2.Text = dtTemp.Rows[0]["Barcode1"].ToString();
            _product.ObjProduct.Barcode2 = dtTemp.Rows[0]["Barcode2"].ToString();
            _product.ObjProduct.Barcode3 = dtTemp.Rows[0]["Barcode3"].ToString();
            ProductId = dtTemp.Rows[0]["PID"].GetLong();
            _groupId = dtTemp.Rows[0]["PGrpId"].GetInt();
            _subGroupId = dtTemp.Rows[0]["PSubGrpId"].GetInt();
        }

        if (dtTemp.Rows[0]["PImage"] is byte[] { Length: > 0 } imageData)
        {
            using var ms = new MemoryStream(imageData, 0, imageData.Length);
            ms.Write(imageData, 0, imageData.Length);
            var newImage = Image.FromStream(ms, true);
            pictureEdit1.Image = newImage;
        }
        else
        {
            pictureEdit1.Image = null;
        }
    }

    private void GenerateBarcode()
    {
        var cmdString =
            $"SELECT AMS.GetNumericValue(GrpCode) GroupId FROM AMS.ProductGroup WHERE PGrpId = {_groupId}";
        var categoryId = cmdString.GetQueryData();
        if (categoryId.GetInt() is 0)
        {
            MessageBox.Show(@"PLEASE ENTER CATEGORY CODE IN CATEGORY AS NUMERIC..!!", ObjGlobal.Caption);
            return;
        }

        var initial = categoryId.GetDecimal();
        categoryId = initial.ToString();
        var current = string.Empty;
        DataTable table;
        try
        {
            cmdString = $@"
				SELECT SUBSTRING( MAX(AMS.GetNumericValue(Barcode)),0,7) PID FROM AMS.Product WHERE Barcode like '{categoryId}%' AND PGrpId = {_groupId} ";
            table = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            try
            {
                cmdString = $@"
					SELECT MAX(AMS.GetNumericValue(p.Barcode1)) PID
					FROM AMS.Product p
						 LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
						 WHERE pg.GrpCode='{categoryId}'; ";
                table = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                cmdString = $@"
					SELECT ISNULL(MAX(TRY_CAST(SUBSTRING(ISNULL( p.Barcode1, 0), PATINDEX('%[0-9]%',  p.Barcode1), PATINDEX('%[0-9][^0-9]%', ISNULL( p.Barcode1, 0)+'t')-PATINDEX('%[0-9]%', ISNULL( p.Barcode1, 0))) AS NUMERIC)), 0) + 1 AS PID
					FROM AMS.Product p
					LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
					WHERE  pg.GrpCode = '{categoryId}' AND p.Barcode1 LIKE '{categoryId}%'; ";
                table = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            }
        }

        var productId = table.Rows.Count is 0 || table.Rows[0]["PID"].GetLong() is 0
            ? 1
            : table.Rows[0]["PID"].GetLong();

        if (productId.ToString().Length >= 5)
        {
            productId += 1;
            TxtBarcode2.Text = $@"{productId}";
        }
        else
        {
            current = productId.ToString().Length > categoryId.Length
                ? productId.ToString().Substring(categoryId.Length, productId.ToString().Length - categoryId.Length)
                : productId.ToString();
            current = (current.GetInt() + 1).ToString();
            productId += 1;
            var blankCharLength = 4;
            if (productId.ToString().Length < 4)
            {
                blankCharLength -= ProductId.ToString().Length;
            }
            else
            {
                blankCharLength = 0;
            }

            var blankCharArray = Enumerable.Repeat(0, blankCharLength);
            var currentNumber = string.Join(string.Empty, blankCharArray) + productId;
            TxtBarcode2.Text = $@"{categoryId}" + $@"{currentNumber}";
        }

        if (current.IsBlankOrEmpty())
        {
            current = productId.ToString().Length > categoryId.Length
                ? productId.ToString().Substring(categoryId.Length, productId.ToString().Length - categoryId.Length)
                : productId.ToString();
            TxtBarcode1.Text = initial + @"." + current;
        }

        if (TxtCategory.Text.Trim() != string.Empty && _actionTag == "SAVE")
        {
            if (TxtBarcode1.IsBlankOrEmpty())
            {
                var cmd = @$"
				    SELECT  ISNULL(CONVERT(DECIMAL(18, 3), MAX(p.Barcode))+0.001,{categoryId}.001) Barcode FROM AMS.Product p 
                    WHERE p.Barcode LIKE '{categoryId}.%' AND p.PGrpId={_groupId};";
                TxtBarcode1.Text = cmd.GetQueryData();
            }
        }

        TxtBarcode1.Text = CheckBarcode1(TxtBarcode1.Text);
        TxtBarcode2.Text = CheckShortName(TxtBarcode2.Text);
        if (TxtBarcode.IsBlankOrEmpty())
        {
            TxtBarcode.Text = TxtBarcode2.Text;
        }
    }

    private static void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.Image == null)
        {
            return;
        }

        var location = pictureBox.ImageLocation ?? pictureBox.Image.ToString();
        var fileExt = Path.GetExtension(location);
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png") //&& this.Tag == "SAVE")
        {
            ObjGlobal.PreviewPicture(pictureBox, string.Empty);
        }
        else
        {
            var path = pictureBox.Location.ToString();
            Process.Start(path);
        }
    }

    private string CheckShortName(string shortName)
    {
        var dtBarcode = _objMaster.GetBarcodeList(_groupId);
        while (true)
        {
            var exits = dtBarcode.AsEnumerable().Any(row => row["Barcode"].ToString() == shortName);
            if (!exits)
            {
                break;
            }

            var result = shortName.GetDecimal();
            result += 1.GetDecimal();
            shortName = result.GetString();
        }

        return shortName;
    }

    private string CheckBarcode1(string shortName)
    {
        var dtBarcode = _objMaster.GetBarcodeList(_groupId);
        while (true)
        {
            var exits = dtBarcode.AsEnumerable().Any(row => row["Barcode"].ToString() == shortName);
            if (!exits)
            {
                break;
            }

            var result = shortName.GetDecimal();
            result += 0.001.GetDecimal();
            shortName = result.GetString();
        }

        return shortName;
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private int _pAltUnitId;
    private int _pUnitId;
    private int _groupId;
    private int _subGroupId;

    public long ProductId;
    private readonly bool _isZoom;
    private bool isPurchaseExits;

    public string ProductDesc = string.Empty;
    private string _searchKey = string.Empty;
    private string _actionTag = string.Empty;

    private SimpleButton _btnExits;
    private readonly IMasterSetup _objMaster;
    private DataGridView _barcodeView = new();
    private readonly IProductRepository _product;

    #endregion --------------- OBJECT ---------------
}