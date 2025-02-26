using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmProductInfo : MrForm
{
    // PRODUCT INFORMATION
    public FrmProductInfo(long productId, decimal altQty, decimal qty, decimal rate, decimal pDiscountPercent, decimal pDiscount)
    {
        InitializeComponent();
        _objMaster = new ClsMasterSetup();
        SetProductInfo(productId);
        TxtAltQty.Text = altQty.ToString(ObjGlobal.SysQtyFormat);
        TxtAltQty.Enabled = altQty > 0;
        TxtQty.Text = qty.ToString(ObjGlobal.SysQtyFormat);
        TxtRate.Text = rate > 0 ? rate.ToString(ObjGlobal.SysAmountFormat) : TxtRate.Text;
        ChangeRate = rate;
        Discount = pDiscount;
        DiscountPercent = pDiscountPercent;
        TxtDiscountPercent.Text = DiscountPercent.ToString(ObjGlobal.SysAmountFormat);
        TxtDiscountAmount.Text = Discount.ToString(ObjGlobal.SysAmountFormat);
    }

    private void FrmProductInfo_Load(object sender, EventArgs e)
    {
        if (TxtAltQty.Enabled)
        {
            TxtAltQty.Focus();
        }
        else TxtQty.Focus();
    }

    private void FrmProductInfo_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        else if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.Question("DOY YOU WANT TO CLOSED..??") is DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void SetProductInfo(long productId)
    {
        var dtProduct = _objMaster.GetMasterProductList("SAVE", productId);
        if (dtProduct.Rows.Count <= 0)
        {
            return;
        }

        LblBuyRate.Text = dtProduct.Rows[0]["PBuyRate"].GetDecimalString();
        TxtRate.Text = dtProduct.Rows[0]["PSalesRate"].GetDecimalString();
        LblSalesRate.Text = TxtRate.Text;

        LblMrpRate.Text = dtProduct.Rows[0]["PMRP"].GetDecimalString();
        LblMaxStockQty.Text = dtProduct.Rows[0]["PMax"].GetDecimalString();
        LblMinStockQty.Text = dtProduct.Rows[0]["PMin"].GetDecimalString();

        LblStockAltQty.Text = 0.ToString(ObjGlobal.SysQtyFormat);
        lblStockQty.Text = dtProduct.Rows[0]["StockQty"].GetDecimalQtyString();
        LblBarcode.Text = dtProduct.Rows[0]["PShortName"].ToString();
        LblBarcode1.Text = dtProduct.Rows[0]["Barcode1"].ToString();
        LblAltUOM.Text = dtProduct.Rows[0]["AltUnitCode"].ToString();
        LblAltUOM.Tag = dtProduct.Rows[0]["PAltUnit"].GetInt();
        LblUOM.Text = dtProduct.Rows[0]["UnitCode"].ToString();
        LblUOM.Tag = dtProduct.Rows[0]["PUnit"].GetInt();

        LblConvAltQty.Text = dtProduct.Rows[0]["PAltConv"].GetDecimalString();
        LblConvQty.Text = dtProduct.Rows[0]["PQtyConv"].GetDecimalString();
        LblAltRate.Text = dtProduct.Rows[0]["AltSalesRate"].GetDecimalString();
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (BtnOk.DialogResult != DialogResult.OK)
        {
            DialogResult = DialogResult.Cancel;
            return;
        }
        ChangeAltQty = TxtAltQty.GetDecimal();
        ChangeQty = TxtQty.GetDecimal();
        ChangeRate = TxtRate.GetDecimal();
        DiscountPercent = TxtDiscountPercent.GetDecimal();
        Discount = TxtDiscountAmount.GetDecimal();
        DialogResult = DialogResult.OK;
        Close();
    }

    private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        {
            e.Handled = true;
        }
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
        {
            e.Handled = true;
        }
        if (char.IsControl(e.KeyChar))
        {
            return;
        }
        var textBox = (TextBox)sender;
        if (textBox.Text.IndexOf('.') <= -1 || textBox.Text.Substring(textBox.Text.IndexOf('.')).Length < 5)
        {
            return;
        }
        e.Handled = true;
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
        var qty = TxtQty.GetDecimal();
        if (qty is 0)
        {
            TxtAmount.Text = 0.GetDecimalString();
            TxtNetAmount.Text = 0.GetDecimalString();
        }
        else
        {
            var amount = qty * TxtRate.GetDecimal();
            TxtAmount.Text = amount.GetDecimalString();
            var discount = TxtDiscountPercent.GetDecimal();
            if (discount > 0)
            {
                TxtDiscountAmount.Text = (amount * discount / 100).GetDecimalString();
            }
            var discountAmt = TxtDiscountAmount.GetDecimal();
            TxtNetAmount.Text = discountAmt > 0 ? (amount - discountAmt).GetDecimalString() : TxtAmount.Text;
        }
    }

    private void TxtRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
        {
            e.Handled = true;
        }
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1)
        {
            e.Handled = true;
        }

        if (char.IsControl(e.KeyChar))
        {
            return;
        }
        var textBox = (TextBox)sender;
        if (textBox.Text.IndexOf('.') <= -1 || textBox.Text.Substring(textBox.Text.IndexOf('.')).Length < 5)
        {
            return;
        }
        e.Handled = true;
    }

    private void TxtRate_TextChanged(object sender, EventArgs e)
    {
        var qty = TxtQty.GetDecimal();
        if (qty is 0)
        {
            TxtAmount.Text = 0.GetDecimalString();
            TxtNetAmount.Text = 0.GetDecimalString();
        }
        else
        {
            var amount = qty * TxtRate.GetDecimal();
            TxtAmount.Text = amount.GetDecimalString();
            var discount = TxtDiscountPercent.GetDecimal();
            if (discount > 0)
            {
                TxtDiscountAmount.Text = (amount * discount / 100).GetDecimalString();
            }
            var discountAmt = TxtDiscountAmount.GetDecimal();
            TxtNetAmount.Text = discountAmt > 0 ? (amount - discountAmt).GetDecimalString() : TxtAmount.Text;
        }
    }

    private void TxtDiscountPercent_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true;
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1) e.Handled = true;
        if (char.IsControl(e.KeyChar)) return;
        var textBox = (TextBox)sender;
        if (textBox.Text.IndexOf('.') <= -1 || textBox.Text.Substring(textBox.Text.IndexOf('.')).Length < 5) return;
        e.Handled = true;
    }

    private void TxtDiscountPercent_TextChanged(object sender, EventArgs e)
    {
        var discount = TxtDiscountPercent.GetDecimal();
        TxtDiscountAmount.Text = discount > 0 ? (TxtAmount.GetDecimal() * discount / 100).GetDecimalString() : 0.GetDecimalString();
        var discountAmt = TxtDiscountAmount.GetDecimal();
        TxtNetAmount.Text = discountAmt > 0 ? (TxtAmount.GetDecimal() - discountAmt).GetDecimalString() : TxtAmount.Text;
    }

    private void TxtDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true;
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1) e.Handled = true;
        if (char.IsControl(e.KeyChar)) return;
        var textBox = (TextBox)sender;
        if (textBox.Text.IndexOf('.') <= -1 || textBox.Text.Substring(textBox.Text.IndexOf('.')).Length < 5) return;
        e.Handled = true;
    }

    private void TxtDiscountAmount_TextChanged(object sender, EventArgs e)
    {
        var discountAmt = TxtDiscountAmount.GetDecimal();
        TxtNetAmount.Text = discountAmt > 0 ? (TxtAmount.GetDecimal() - discountAmt).GetDecimalString() : TxtAmount.Text;
    }

    private void TxtAltQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true;
        if (e.KeyChar == '.' && ((TextBox)sender).Text.IndexOf('.') > -1) e.Handled = true;
        if (char.IsControl(e.KeyChar)) return;
        var textBox = (TextBox)sender;
        if (textBox.Text.IndexOf('.') <= -1 || textBox.Text.Substring(textBox.Text.IndexOf('.')).Length < 5) return;
        e.Handled = true;
    }

    private void TxtAltQty_TextChanged(object sender, EventArgs e)
    {
        if (!TxtAltQty.Focused)
        {
            return;
        }

        var conQty = LblConvQty.GetDecimal();
        if (conQty > 0)
        {
            TxtQty.Text = (conQty * TxtAltQty.GetDecimal()).GetDecimalString();
        }
        TxtQty_TextChanged(sender, e);
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
        TxtQty.Text = TxtQty.Text.GetDecimalQtyString();
    }

    private void TxtAltQty_Validating(object sender, CancelEventArgs e)
    {
        TxtAltQty.Text = TxtQty.Text.GetDecimalQtyString();
    }

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public decimal ChangeAltQty;
    public decimal ChangeQty;
    public decimal ChangeRate;
    public decimal DiscountPercent;
    public decimal Discount;
    private readonly IMasterSetup _objMaster;

    #endregion --------------- OBJECT ---------------
}