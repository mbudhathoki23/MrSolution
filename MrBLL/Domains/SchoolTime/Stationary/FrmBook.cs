using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.ProductSetup;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MrBLL.Domains.SchoolTime.Stationary;

public partial class FrmBook : XtraForm
{
    // BOOK SETUP

    #region --------------- FRM BOOK ---------------

    public FrmBook(string actionTag, bool isZoom = false)
    {
        InitializeComponent();
        Shown += FrmBook_Shown;
        _actionTag = actionTag;
        _isZoom = isZoom;
        _objMaster = new ClsMasterSetup();
        _product = new ProductRepository();
    }

    private void FrmBook_Shown(object sender, EventArgs e)
    {
        TxtISBN.Focus();
    }

    private void Book_Load(object sender, EventArgs e)
    {
        if (_actionTag is "DELETE")
        {
            ClearControl();
            EnableControl();
        }
        else if (_actionTag is "UPDATE" && TxtISBN.ReadOnly is false)
        {
            TxtISBN.ReadOnly = true;
        }
        else
        {
            ClearControl();
        }
    }

    private void Book_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (!string.IsNullOrEmpty(TxtISBN.Text) ||
                !string.IsNullOrEmpty(TxtAlias.Text.Trim().Replace("'", "''")))
            {
                if (MessageBox.Show(@"DO YOU WANT TO CLEAR THE DATA INFORMATION..??", ObjGlobal.Caption,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) is DialogResult.Yes)
                {
                    ClearControl();
                    TxtISBN.Focus();
                }
            }
            else
            {
                Close();
            }
        }
    }

    private void Global_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void Ctrl_Leave(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.AliceBlue;

        if (ActiveControl != null && !string.IsNullOrEmpty(_actionTag) && TxtGroup.Focused &&
            string.IsNullOrEmpty(TxtGroup.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"GROUP CAN'T LEFT BLANK..!!", ObjGlobal.Caption);
            TxtGroup.Focus();
        }
    }

    private void TxtUnit_Leave(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.AliceBlue;

        if (ActiveControl != null && !string.IsNullOrEmpty(_actionTag) && TxtUnit.Focused &&
            string.IsNullOrEmpty(TxtUnit.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"UNIT CAN'T LEFT BLANK..!!", ObjGlobal.Caption);
            TxtUnit.Focus();
        }
    }

    private void Ctrl_Enter(object sender, EventArgs e)
    {
        ((TextBox)sender).BackColor = Color.LightPink;
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
                MessageBox.Show(@"PICTURE FILE FORMAT & " + ex.Message, ObjGlobal.Caption);
        }
    }

    private void TxtISBN_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtISBN.IsValueExits())
        {
            var result = TxtISBN.CheckValueExits(_actionTag, "PRODUCT", "PShortName", ProductId);
            if (result.Rows.Count > 0)
            {
                TxtISBN.WarningMessage($"{TxtISBN.Text} ISBN NUMBER ALREADY EXITS..!!");
                return;
            }
        }
        if (_actionTag.IsValueExits() && TxtISBN.IsBlankOrEmpty() && ActiveControl.Name != "TxtISBN")
        {
            TxtISBN.WarningMessage($"ISBN NUMBER IS REQUIRED..!!");
            return;
        }
    }

    private void TxtISBN_Leave(object sender, EventArgs e)
    {
    }

    private void TxtISBN_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnISBN_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtISBN.IsBlankOrEmpty())
            {
                TxtISBN.WarningMessage($"ISBN NUMBER IS REQUIRED..!!");
                return;
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtISBN.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtISBN, BtnISBN);
        }

        if (_actionTag is "DELETE" && e.KeyCode is Keys.Enter)
        {
            xtbProduct.SelectedTabPage = tbImage;
        }
    }

    private void BtnISBN_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetMasterProduct(_actionTag);
        if (description.IsValueExits())
        {
            if (_actionTag != "SAVE")
            {
                ProductId = id;
                BindSelectedProduct(ProductId);
                TxtISBN.ReadOnly = false;
            }
        }
        TxtISBN.Focus();
    }

    private void TxtUnit_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtUnit_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (ActiveControl != null && string.IsNullOrEmpty(TxtUnit.Text.Trim().Replace("'", "''")) &&
                TxtUnit.Focused && !string.IsNullOrEmpty(_actionTag))
            {
                MessageBox.Show(@"UNIT CANNOT BE BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtUnit.Focus();
            }
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnUnit_Click(sender, e);
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            using var Frm = new FrmProductUnit(true);
            Frm.ShowDialog();
            TxtUnit.Text = Frm.ProductUnitName;
            _unitId = Frm.ProductUnitId;
            TxtUnit.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtUnit, BtnUnit);
        }
    }

    private void BtnUnit_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProductUnit(_actionTag);
        if (description.IsValueExits())
        {
            TxtUnit.Text = description;
            _unitId = id;
        }
        TxtUnit.Focus();
    }

    private void TxtAlias_Leave(object sender, EventArgs e)
    {
        if (ActiveControl != null && !string.IsNullOrEmpty(_actionTag) && TxtAlias.Focused &&
            string.IsNullOrEmpty(TxtAlias.Text.Trim().Replace("'", "''")))
        {
            MessageBox.Show(@"BOOK TITLE  CAN'T LEFT BLANK..!!", ObjGlobal.Caption);
            TxtAlias.Focus();
            return;
        }

        Ctrl_Leave(sender, e);
    }

    private void TxtAlias_Validating(object sender, CancelEventArgs e)
    {
        //if (!string.IsNullOrEmpty(ActionTag) && ActionTag != "DELETE")
        //{
        //    using var dtDesc = VmMaster.CheckIsValidData(ActionTag, "Product", "PName", "PID", TxtAlias.Text.Trim().Replace("'", "''"), ProductId.ToString());
        //    if (dtDesc != null && dtDesc.Rows.Count > 0)
        //    {
        //        MessageBox.Show("BOOK ALREADY EXITS..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
        //        TxtAlias.Focus();
        //        return;
        //    }
        //}
    }

    private void TxtAlias_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)

        {
            BtnDescription_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtAlias.Text.Trim().Replace("'", "''")) && TxtAlias.Enabled is true &&
                TxtAlias.Focused is true)
            {
                MessageBox.Show(@"BOOK NAME CAN'T LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtAlias.Focus();
            }
        }
        else if (_actionTag.ToUpper() != "SAVE" && TxtAlias.ReadOnly is true)
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtAlias, BtnDescription);
        }
    }

    private void BtnDescription_Click(object sender, EventArgs e)
    {
        var frmPickList = new FrmAutoPopList("MAX", "PRODUCT", _actionTag, string.Empty, "NORMAL", "LIST");
        //var frmPickList = new FrmAutoPopList("MAX", "PRODUCT", "LIST", ObjGlobal.SearchKey, ActionTag, "NORMAL", string.Empty);
        if (FrmAutoPopList.GetListTable.Rows.Count > 0)
        {
            frmPickList.ShowDialog();
            if (frmPickList.SelectedList.Count > 0)
                if (_actionTag != "SAVE")
                {
                    TxtAlias.Text = frmPickList.SelectedList[0]["PName"].ToString().Trim();
                    ProductId = Convert.ToInt64(frmPickList.SelectedList[0]["Pid"].ToString().Trim());
                }

            frmPickList.Dispose();
        }
        else
        {
            MessageBox.Show(@"BOOK LIST ARE NOT AVAILABLE..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            TxtAlias.Focus();
            return;
        }

        ObjGlobal.SearchKey = string.Empty;
        TxtAlias.Focus();
    }

    private void TxtGroup_Validating(object sender, CancelEventArgs e)
    {
        if (_groupId > 0) TxtSubGroup.Enabled = true;
    }

    private void TxtGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnGroup_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (string.IsNullOrEmpty(TxtGroup.Text.Trim().Replace("'", "''")) && TxtGroup.Enabled is true &&
                TxtGroup.Focused is true)
            {
                MessageBox.Show(@"BOOK GROUP LEFT BLANK..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                TxtGroup.Focus();
            }
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateProductGroup();
            if (description.IsValueExits())
            {
                TxtGroup.Text = description;
                _groupId = id;
            }
            TxtGroup.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtGroup, BtnGroup);
        }
    }

    private void BtnGroup_Click(object sender, EventArgs e)
    {
        var (description, id, margin) = GetMasterList.GetProductGroup(_actionTag);
        if (description.IsValueExits())
        {
            TxtGroup.Text = description;
            _groupId = id;
        }
        TxtGroup.Focus();
    }

    private void TxtSubGroup_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtSubGroup_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSubGroup_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateProductSubGroup(true);
            if (description.IsValueExits())
            {
                TxtSubGroup.Text = description;
                _subGroupId = id;
            }
            TxtSubGroup.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubGroup, BtnSubGroup);
        }
    }

    private void BtnSubGroup_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetProductSubGroup(_actionTag, _groupId);
        if (description.IsValueExits())
        {
            TxtSubGroup.Text = description;
            _subGroupId = id;
        }
        TxtSubGroup.Focus();
    }

    private void TxtPublication_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnPublication_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            if (string.IsNullOrEmpty(TxtPublication.Text.Trim().Replace("'", "''")) &&
                TxtPublication.Enabled is true && TxtPublication.Focused is true)
            {
                var dialogResult = MessageBox.Show(@"ARE YOU SURE TO LEAVE PUBLICATION BLANK..!!",
                    ObjGlobal.Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    TxtPublication.Focus();
                }
            }
    }

    private void TxtPublication_Validating(object sender, CancelEventArgs e)
    {
    }

    private void BtnPublication_Click(object sender, EventArgs e)
    {
        var description = GetMasterList.GetPublicationList(_actionTag);
        if (description.IsValueExits())
        {
            TxtPublication.Text = description;
        }
        TxtPublication.Focus();
    }

    private void TxtTradeRate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            xtbProduct.SelectedTabPage = tbImage;
            pictureEdit1.Focus();
        }
    }

    private void TxtWriter_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtWriter_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAuthor_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtAuthor.IsBlankOrEmpty())
            {
                if (CustomMessageBox.Question(@"ARE YOU SURE TO LEAVE AUTHOR BLANK..!!") == DialogResult.Yes)
                {
                    SendKeys.Send("{TAB}");
                }
                else
                {
                    TxtAuthor.Focus();
                }
            }
        }
    }

    private void PictureEdit1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void BtnAuthor_Click(object sender, EventArgs e)
    {
        var description = GetMasterList.GetBookAuthorList(_actionTag);
        if (description.IsValueExits())
        {
            TxtAuthor.Text = description;
        }
        TxtAuthor.Focus();
    }

    private void TxtMrp_Validating(object sender, CancelEventArgs e)
    {
        TxtMrp.Text = TxtMrp.GetDecimalString();
    }

    private void TxtMrp_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtPurRate_Validating(object sender, CancelEventArgs e)
    {
        TxtPurRate.Text = TxtPurRate.GetDecimalString();
    }

    private void TxtPurRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtSalesRate_Validating(object sender, CancelEventArgs e)
    {
        TxtSalesRate.Text = TxtSalesRate.GetDecimalString();
    }

    private void TxtSalesRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtTradeRate_Validating(object sender, CancelEventArgs e)
    {
        TxtTradeRate.Text = TxtTradeRate.GetDecimalString();
    }

    private void TxtTradeRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        Global_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (IsValidForm())
        {
            if (SaveBookedInformation() > 0)
            {
                if (_isZoom)
                {
                    ProductDesc = TxtAlias.Text.Trim();
                    Close();
                    return;
                }
                CustomMessageBox.ActionSuccess(TxtISBN.Text, "BOOK", _actionTag);
                ClearControl();
                xtbProduct.SelectedTabPage = tbInformation;
                TxtISBN.Focus();
            }
            else
            {
                TxtISBN.ErrorMessage(@"ERROR OCCURS WHILE DATA SAVING..!!");
                TxtISBN.Focus();
                return;
            }
        }
        else
        {
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (TxtAlias.IsValueExits() || TxtISBN.IsValueExits())
        {
            Close();
        }
        else
        {
            ClearControl();
        }
    }

    #endregion --------------- FRM BOOK ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private bool IsValidForm()
    {
        if (TxtAlias.IsBlankOrEmpty())
        {
            TxtAlias.WarningMessage(@"BOOK NAME CAN NOT BE LEFT BLANK..!!");
            return false;
        }
        if (TxtISBN.IsBlankOrEmpty())
        {
            TxtISBN.WarningMessage("BOOK ISBN NUMBER CAN NOT BE LEFT BLANK..!!");
            return false;
        }
        if (TxtUnit.IsBlankOrEmpty() || _unitId is 0)
        {
            TxtUnit.WarningMessage("BOOK UNIT IS REQUIRED FOR SAVE..!!");
            return false;
        }
        if (_actionTag.IsValueExits() && _actionTag != "SAVE")
        {
            if (ProductId is 0)
            {
                TxtISBN.WarningMessage("SELECTED BOOK IS INVALID..!!");
                return false;
            }
        }

        return true;
    }

    private int SaveBookedInformation()
    {
        ProductId = _actionTag switch
        {
            "SAVE" => ClsMasterSetup.ReturnMaxLongValue("AMS.Product", "PID"),
            _ => ProductId
        };
        _product.ObjProduct.PID = ProductId;

        //_objMaster.BookDetails.BookId = ProductId;
        //_objMaster.BookDetails.ISBNNo = TxtISBN.Text.Trim().Replace("'", "''");
        //_objMaster.BookDetails.Author = TxtAuthor.Text.Trim().Replace("'", "''");
        //_objMaster.BookDetails.Publisher = TxtPublication.Text.Trim().Replace("'", "''");
        //_objMaster.BookDetails.PrintDesc = TxtAlias.Text.Trim().Replace("'", "''");

        _product.ObjProduct.PName = $"{TxtAlias.Text.Trim().Replace("'", "''")} | ({ProductId}) ";
        _product.ObjProduct.PAlias = TxtAlias.Text.Trim().Replace("'", "''");
        _product.ObjProduct.PShortName = TxtISBN.Text.Trim();
        _product.ObjProduct.PType = "I";
        _product.ObjProduct.PCategory = "FG";
        _product.ObjProduct.PUnit = _unitId;
        _product.ObjProduct.PAltUnit = 0;
        _product.ObjProduct.PQtyConv = 0;
        _product.ObjProduct.PAltConv = 0;
        _product.ObjProduct.PValTech = "FIFO";
        _product.ObjProduct.PSerialno = _product.ObjProduct.PSizewise = _product.ObjProduct.PBatchwise = false;

        _product.ObjProduct.PBuyRate = TxtPurRate.Text.Trim().GetDecimal();
        _product.ObjProduct.PMRP = TxtMrp.Text.Trim().GetDecimal();
        _product.ObjProduct.PMargin2 = 0;
        _product.ObjProduct.TradeRate = TxtTradeRate.Text.Trim().GetDecimal();
        _product.ObjProduct.PSalesRate = TxtSalesRate.Text.Trim().GetDecimal();
        _product.ObjProduct.PMargin1 = 0;

        var converter = new ImageConverter();
        var arr = (byte[])converter.ConvertTo(pictureEdit1?.Image!, typeof(byte[]));
        _product.ObjProduct.PImage = arr;

        _product.ObjProduct.PGrpId = _groupId;
        _product.ObjProduct.PSubGrpId = _subGroupId;
        _product.ObjProduct.PTax = ChkIsTaxable.Checked ? 13 : 0;
        _product.ObjProduct.PMin = txtMinQty.Text.Trim().GetDecimal();
        _product.ObjProduct.PMax = txtMaxQy.Text.Trim().GetDecimal();
        _product.ObjProduct.CmpId = 0;
        _product.ObjProduct.PPL = _product.ObjProduct.PPR = _product.ObjProduct.PSL = 0;
        _product.ObjProduct.PSR = _product.ObjProduct.PL_Opening = 0;
        _product.ObjProduct.PL_Closing = _product.ObjProduct.BS_Closing = 0;
        _product.ObjProduct.Status = true;
        _product.ObjProduct.BeforeBuyRate = TxtPurRate.Text.Trim().GetDecimal();
        _product.ObjProduct.BeforeSalesRate = ChkIsTaxable.Checked ? TxtSalesRate.Text.Trim().GetDecimal() / "1.13".GetDecimal() : TxtSalesRate.Text.Trim().GetDecimal();
        _product.ObjProduct.Barcode = TxtISBN.Text.Trim();
        _product.ObjProduct.Barcode1 = string.Empty;
        _product.ObjProduct.ChasisNo = string.Empty;
        _product.ObjProduct.VHModel = _product.ObjProduct.VHColor = string.Empty;
        _product.ObjProduct.Barcode2 = _product.ObjProduct.Barcode3 = string.Empty;
        _product.ObjProduct.SyncRowVersion = _product.ObjProduct.SyncRowVersion.ReturnSyncRowNo("PRODUCT", ProductId.ToString());
        return _product.SaveProductInfo(_actionTag);
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? @$"BOOK ITEM DETAILS [{_actionTag}]" : @"BOOK ITEM DETAILS";
        _groupId = 0;
        _subGroupId = 0;
        _unitId = 0;
        ProductId = 0;
        TxtISBN.Clear();
        TxtAlias.Clear();
        TxtAuthor.Clear();
        TxtPublication.Clear();
        TxtGroup.Clear();
        TxtSubGroup.Clear();
        TxtSubGroup.Enabled = false;
        TxtUnit.Clear();
        TxtSalesRate.Clear();
        ChkActive.Checked = true;
        ChkIsTaxable.Checked = true;
        TxtPurRate.Text = @"0.00";
        TxtSalesRate.Text = @"0.00";
        TxtMrp.Text = @"0.00";
        TxtTradeRate.Text = @"0.00";
        pictureEdit1.Image = null;
        TxtReorderLevel.Text = @"0.00";
        TxtReorderStock.Text = @"0.00";
        txtMinQty.Text = @"0.00";
        txtMaxQy.Text = @"0.00";
        LinkAttachment1.Enabled = true;
    }

    private void EnableControl()
    {
        TxtISBN.Enabled = true;
        TxtISBN.ReadOnly = true;
        TxtAlias.Enabled = false;
        TxtAuthor.Enabled = false;
        TxtPublication.Enabled = false;
        TxtGroup.Enabled = false;
        TxtSubGroup.Enabled = false;
        TxtSubGroup.Enabled = false;
        TxtUnit.Enabled = false;
        TxtSalesRate.Enabled = false;
        ChkActive.Enabled = false;
        TxtPurRate.Enabled = false;
        TxtSalesRate.Enabled = false;
        TxtMrp.Enabled = false;
        TxtTradeRate.Enabled = false;
        pictureEdit1.Enabled = false;
        TxtReorderLevel.Enabled = false;
        TxtReorderStock.Enabled = false;
        txtMinQty.Enabled = false;
        txtMaxQy.Enabled = false;
        LinkAttachment1.Enabled = false;
    }

    private void BindSelectedProduct(long selectedProductId)
    {
        BtnSave.Text = _actionTag switch
        {
            "UPDATE" => @"&UPDATE",
            "DELETE" => @"&DELETE",
            _ => BtnSave.Text
        };
        //ActionTag = "UPDATE";
        //Text = @"COUNTER PRODUCT [UPDATE]";
        var dtTemp = _objMaster.GetMasterBookList(_actionTag, selectedProductId);
        if (dtTemp.Rows.Count <= 0) return;
        TxtAlias.Text = dtTemp.Rows[0]["PAlias"].ToString();
        TxtISBN.Text = dtTemp.Rows[0]["Barcode"].ToString();

        ProductId = dtTemp.Rows[0]["PID"].GetLong();
        _groupId = dtTemp.Rows[0]["PGrpID"].GetInt();
        _subGroupId = dtTemp.Rows[0]["PSubGrpId"].GetInt();

        TxtGroup.Text = dtTemp.Rows[0]["GrpName"].ToString();
        TxtSubGroup.Text = dtTemp.Rows[0]["SubGrpName"].ToString();

        TxtPublication.Text = dtTemp.Rows[0]["Publisher"].ToString();
        TxtAuthor.Text = dtTemp.Rows[0]["Author"].ToString();

        _unitId = dtTemp.Rows[0]["UID"].GetInt();
        TxtUnit.Text = dtTemp.Rows[0]["UnitCode"].ToString();
        TxtMrp.Text = dtTemp.Rows[0]["PMRP"].GetDecimalString();
        TxtSalesRate.Text = dtTemp.Rows[0]["PSalesRate"].GetDecimalString();
        TxtPurRate.Text = dtTemp.Rows[0]["PBuyRate"].GetDecimalString();
        TxtTradeRate.Text = dtTemp.Rows[0]["TradeRate"].GetDecimalString();

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

    private void LinkAttachment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        var pcvBox = new PictureBox { Image = pictureEdit1.Image };
        PreviewImage(pcvBox);
    }

    private static void PreviewImage(PictureBox pbPictureBox)
    {
        if (pbPictureBox.Image == null) return;
        var location = pbPictureBox.ImageLocation ?? pbPictureBox.Image.ToString();
        var fileExt = Path.GetExtension(location);
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png")
        {
            ObjGlobal.PreviewPicture(pbPictureBox, string.Empty);
        }
        else

        {
            var path = pbPictureBox.Location.ToString();
            Process.Start(path);
        }
    }

    #endregion --------------- METHOD ---------------

    // GLOBAL OBJECT FOR THIS FORM

    #region --------------- GLOBAL VALUE ---------------

    private int _groupId;
    private int _subGroupId;
    private int _unitId;
    public long ProductId;
    public string ProductDesc = string.Empty;
    private readonly string _actionTag;
    private readonly bool _isZoom;
    private readonly IMasterSetup _objMaster;
    private readonly IProductRepository _product;

    #endregion --------------- GLOBAL VALUE ---------------
}