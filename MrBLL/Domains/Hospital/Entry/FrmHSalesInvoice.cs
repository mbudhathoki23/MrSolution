using MrBLL.DataEntry.Common;
using MrBLL.Domains.Hospital.Master;
using MrBLL.Domains.POS.Master;
using MrBLL.Master.LedgerSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Billing;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Custom;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Interface;
using MrDAL.Utility.PickList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.Hospital.Entry;

public partial class FrmHSalesInvoice : MrForm
{
    // OPB BILLING FUNCTION

    #region --------------- SALES INVOICE ---------------

    public FrmHSalesInvoice(bool isIpd = false, string invoiceType = "NORMAL")
    {
        InitializeComponent();
        _design.GetPointOfSalesDesign(RGrid, "HSB");
        KeyDown += FrmHSalesInvoice_KeyDown;
        Shown += FrmHSalesInvoice_Shown;
        _invoiceService = new SalesInvoiceService();
        _productModels = new List<SalesInvoiceProductModel>();
        _setup.BindPaymentType(CmbPaymentType);
        InitialiseDataTable();
        ObjGlobal.DGridColorCombo(RGrid);
        _invoiceType = invoiceType;
    }

    private void FrmHSalesInvoice_Shown(object sender, EventArgs e)
    {
        var frmCounter = new FrmCounterTagList();
        frmCounter.ShowDialog();
        TxtCounter.Text = frmCounter.SelectedCounter;
        CounterId = frmCounter.SelectedCounterId;
        if (!TxtCounter.Text.IsValueExits())
        {
            Close();
            return;
        }

        if (_invoiceType.Equals("RETURN"))
        {
            TxtRefVno.Focus();
        }
        else
        {
            TxtPatientId.Focus();
        }
    }

    private void FrmHSalesInvoice_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F6)
        {
            BtnPatient_Click(sender, e);
        }
    }

    private void FrmHSalesInvoice_Load(object sender, EventArgs e)
    {
        _actionTag = string.Empty;
        BtnNew_Click(sender, e);
    }

    private void FrmHSalesInvoice_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)21)
        {
            if (!ObjGlobal.IsIrdRegister)
            {
                _actionTag = "UPDATE";
                Text = @"POS INVOICE DETAILS [UPDATE]";
                ClearControl();
                EnableControl(true);
                TxtVNo.Enabled = true;
                TxtVNo.Focus();
            }
        }
        else if (e.KeyChar == (char)27)
        {
            if (!btnNew.Enabled)
            {
                if (PDetails.Visible)
                {
                    PDetails.Visible = false;
                    PDetails.Enabled = false;
                    TxtProduct.Focus();
                }
                else
                {
                    _actionTag = "";
                    _invoiceType = "NORMAL";
                    ClearControl();
                    EnableControl(false);
                    btnNew.Focus();
                }
            }
            else
            {
                btnExit.PerformClick();
            }
        }
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        ClearControl();
        _actionTag = "SAVE";
        EnableControl(true);
        ReturnSbVoucherNumber();
        RGrid.ReadOnly = true;
        MskDate.Text = DateTime.Now.GetDateString();
        MskMiti.GetNepaliDate(MskDate.Text);
        MskMiti.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (RGrid.RowCount is 0)
        {
            if (MessageBox.Show(@"DO YOU WANT TO CLOSE THIS FORM..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes) Close();
        }
        else
        {
            ClearControl();
        }
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ClearControl();
        Text = @$"POS INVOICE DETAILS [{_actionTag}]";
        EnableControl(false);
        BtnVno.PerformClick();
        TxtVNo.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintVoucher();
    }

    private void BtnLock_Click(object sender, EventArgs e)
    {
        new FrmLockScreen().ShowDialog();
    }

    private void TxtVno_Enter(object sender, EventArgs e)
    {
        GlobalControl_Enter(sender, e);
    }

    private void TxtVno_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor((TextBox)sender, 'L');
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.Equals("UPDATE"))
        {
            FillInvoiceData(TxtVNo.Text);
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var listType = _invoiceType.Equals("RETURN") && _actionTag.Equals("REVERSE") ? "SR" : "SB";
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, listType, DateTime.Now.GetDateString());
        if (voucherNo.IsValueExits())
        {
            if (_invoiceType.Equals("RETURN") || _actionTag.Equals("REVERSE"))
            {
                if (_invoiceType.Equals("RETURN") && _actionTag.Equals("SAVE"))
                {
                    TxtRefVno.Text = voucherNo;
                }
                else
                {
                    TxtVNo.Text = voucherNo;
                }
                if (_invoiceType.Equals("RETURN") && _actionTag.Equals("REVERSE"))
                {
                    FillReturnInvoiceData(voucherNo);
                }
                else
                {
                    FillInvoiceData(_invoiceType.Equals("RETURN") ? TxtRefVno.Text : TxtVNo.Text);
                }
            }
            else if (_invoiceType.Equals("NORMAL") && _actionTag.Equals("UPDATE"))
            {
                TxtVNo.Text = voucherNo;
                FillInvoiceData(TxtVNo.Text);
            }
        }
        TxtVNo.Focus();
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskMiti.MaskCompleted && !MskMiti.IsDateExits("M") || !MskMiti.MaskCompleted && MskMiti.Enabled && TxtVNo.IsValueExits())
        {
            MskMiti.WarningMessage($"ENTER MITI IS INVALID..!!");
            return;
        }

        if (MskMiti.MaskCompleted && !MskMiti.Text.IsValidDateRange("M"))
        {
            MskMiti.WarningMessage($"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
            return;
        }

        if (MskMiti.MaskCompleted)
        {
            MskDate.Text = MskDate.GetEnglishDate(MskMiti.Text);
        }
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskDate.MaskCompleted && !MskDate.IsDateExits("D") || !MskDate.MaskCompleted && MskDate.Enabled && TxtVNo.IsValueExits())
        {
            MskDate.WarningMessage("VOUCHER DATE IS INVALID..!!");
            return;
        }

        if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            MskDate.WarningMessage($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
            return;
        }

        if (MskDate.MaskCompleted)
        {
            MskMiti.GetNepaliDate(MskDate.Text);
        }
    }

    private void TxtVNo_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVno.PerformClick();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (_actionTag.Equals("REVERSE"))
            {
                PDetails.Visible = true;
                PDetails.Enabled = true;
                TxtRemarks.Enabled = true;
                TxtRemarks.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }

    private void TxtCounter_Leave(object sender, EventArgs e)
    {
        GlobalControl_Leave(sender, e);
        if (ActiveControl != null && _actionTag.IsValueExits() && TxtCounter.Focused &&
            TxtCounter.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCounter, "TERMINAL IS REQUIRED FOR BILLING..!!");
            TxtCounter.Focus();
        }
    }

    private void BtnCounter_Click(object sender, EventArgs e)
    {
        var frmCounter = new FrmCounterTagList();
        frmCounter.ShowDialog();
        TxtCounter.Text = frmCounter.SelectedCounter;
        CounterId = frmCounter.SelectedCounterId;
        _defaultPrinter = frmCounter.CounterPrinter;
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1) BtnVno_Click(sender, e);
    }

    private void TxtRefVno_Leave(object sender, EventArgs e)
    {
        if (_tagStrings.Equals(_invoiceType))
        {
            if (TxtRefVno.Text.IsBlankOrEmpty() && TxtRefVno.Focused && ActiveControl != null &&
                _actionTag.IsValueExits())
            {
                if (MessageBox.Show(
                        @"PLEASE SELECT SALES INVOICE NUMBER FOR RETURN OR CLICK ON YES TO CONTINUE..!!",
                        ObjGlobal.Caption, MessageBoxButtons.YesNo) is DialogResult.No)
                    TxtRefVno.Focus();
            }
            else if (RGrid.RowCount is 0 && _invoiceType.Equals("RETURN") && TxtVNo.Text.IsValueExits())
            {
                FillInvoiceData(TxtRefVno.Text);
            }
        }
    }

    private void TxtRefVno_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalControl_KeyPress(sender, e);
    }

    private void TxtProduct_Enter(object sender, EventArgs e)
    {
        GlobalControl_Enter(sender, e);
        if (TxtProduct.Text.IsBlankOrEmpty()) RGrid.ClearSelection();
        PDetails.Visible = false;
        PDetails.Enabled = false;
        TxtProduct.SelectAll();
    }

    private void TxtProduct_Leave(object sender, EventArgs e)
    {
    }

    private void TxtProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Back && TxtProduct.Text.IsValueExits() && ProductId > 0)
            ClearDetails();
        else if (e.KeyChar is (char)Keys.Escape && TxtProduct.Text.IsValueExits() && ProductId > 0)
            if (MessageBox.Show(@"DO YOU WANT TO CLEAR THE DETAILS..??", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo) is DialogResult.Yes)
            {
                ClearDetails();
                TxtProduct.Focus();
            }
    }

    private void TxtProduct_Validating(object sender, CancelEventArgs e)
    {
        if (TxtProduct.IsBlankOrEmpty() && RGrid.RowCount > 0)
        {
        }
        else if (!TxtProduct.Text.IsValueExits() && ProductId is 0 && _actionTag.IsValueExits() &&
                 RGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtProduct, "PLEASE ENTER THE BARCODE OF THE ITEMS..!!");
        }
    }

    private void TxtProduct_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtProduct.Text, ProductId) = GetMasterList.CreateProduct(true);
            SetProductInfo();
        }
        else if (e.KeyCode == Keys.F1)
        {
            BtnProduct_Click(sender, e);
        }
        else if (e.KeyCode == Keys.F2)
        {
            (TxtProduct.Text, ProductId) = GetMasterList.GetCounterProduct(TxtProduct.Text);
            SetProductInfo();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            if (TxtProduct.Text.Trim() == string.Empty && TxtProduct.Enabled)
            {
                if (RGrid.Rows.Count == 0)
                {
                    MessageBox.Show(@"BARCODE CAN'T BE LEFT BLANK..!!", ObjGlobal.Caption);
                    TxtProduct.Focus();
                }
                else
                {
                    TxtNetAmount.Text = TxtNetAmount.GetDecimal() is 0 ? TxtBasicAmount.Text : TxtNetAmount.Text;
                    PDetails.Visible = PDetails.Enabled = true;
                    PDetails.Focus();
                    if (_tagStrings.Contains(_invoiceType))
                    {
                        TxtRemarks.Enabled = true;
                        TxtRemarks.Focus();
                    }
                    else
                    {
                        CmbPaymentType.Focus();
                    }
                }
            }
            else if (TxtProduct.Text.Trim() != string.Empty)
            {
                TxtQty.Enabled = true;
                TxtQty.Focus();
            }
        }
        else
        {
            var searchKeys = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", searchKeys,
                TxtProduct, BtnService);
        }
    }

    private void BtnProduct_Click(object sender, EventArgs e)
    {
        (TxtProduct.Text, ProductId) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        SetProductInfo();
        TxtProduct.Focus();
    }

    private void TxtQty_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtQty, 'E');
        TxtQty.SelectAll();
    }

    private void TxtQty_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtQty, 'L');
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
    }

    private void TxtQty_KeyDown(object sender, KeyEventArgs e)
    {
        // if user tried to change the qty, rate or discount
        if (e.KeyCode != Keys.F2)
        {
            return;
        }
        var confirm = new FrmProductInfo(ProductId, GetAltQty, TxtQty.GetDecimal(), GetSalesRate, PDiscountPercentage, PDiscount);
        confirm.ShowDialog();
        if (confirm.DialogResult != DialogResult.OK)
        {
            return;
        }
        GetAltQty = confirm.ChangeAltQty.GetDecimal();
        TxtQty.Text = confirm.ChangeQty.GetDecimalQtyString();
        PDiscount = confirm.Discount.GetDecimal();
        PDiscountPercentage = confirm.DiscountPercent.GetDecimal();
        GetSalesRate = confirm.ChangeRate.GetDecimal();
    }

    private void TxtQty_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) AddDataToGridDetails(IsRowUpdate);

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var _);
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        RIndex = e.RowIndex;
        SGridId = RIndex;
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        CIndex = e.ColumnIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter || RGrid.CurrentRow == null) return;
        e.SuppressKeyPress = true;
        SGridId = RIndex;
        SetDataFromGridToTextBox();
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        SetDataFromGridToTextBox();
    }

    private void RGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
    }

    private void RGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        TotalCalculationOfInvoice();
        for (var i = 0; i < RGrid.RowCount; i++) RGrid.Rows[i].Cells["GTxtSNo"].Value = i + 1;
    }

    private void CmbPaymentType_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbPaymentType, 'E');
    }

    private void CmbPaymentType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) TxtCustomer.Focus();
    }

    private void CmbPaymentType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(CmbPaymentType, 'L');
    }

    private void CmbPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        TxtCustomer.ReadOnly = CmbPaymentType.SelectedIndex != 0;
    }

    private void CmbPaymentType_SelectionChangeCommitted(object sender, EventArgs e)
    {
    }

    private void CmbPaymentType_DisplayMemberChanged(object sender, EventArgs e)
    {
    }

    private void TxtCustomer_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCustomer, 'E');
    }

    private void TxtCustomer_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtCustomer, 'L');
    }

    private void TxtCustomer_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            btnCustomer.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            using var frm = new FrmGeneralLedger("Customer", true);
            frm.ShowDialog();
            TxtCustomer.Text = frm.LedgerDesc;
            LedgerId = frm.LedgerId;
            frm.Dispose();
        }
        else if (e.KeyCode is Keys.Enter or Keys.Tab)
        {
            if (TxtCustomer.Text.Trim() == string.Empty) BtnCustomer_Click(sender, e);
            if (TxtCustomer.Text.Trim() != string.Empty)
            {
                if (TxtBillDiscountPercentage.Enabled)
                    TxtBillDiscountPercentage.Focus();
                else
                    TxtTenderAmount.Focus();
            }
            else
            {
                MessageBox.Show(@"CUSTOMER CANNOT LEFT BLANK..! PLEASE SELECT CUSTOMER..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button3,
                    MessageBoxOptions.ServiceNotification);
            }
        }
        else if (TxtCustomer.ReadOnly)
        {
            var searchKeys = e.KeyCode.ToString();
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCustomer, btnCustomer);
        }
    }

    private void BtnCustomer_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger("", "Customer");
        if (description.IsValueExits())
        {
            TxtCustomer.Text = description;
            LedgerId = id;
            LedgerCurrentBalance(LedgerId);
        }
        TxtCustomer.Focus();
    }

    private void TxtCustomer_TextChanged(object sender, EventArgs e)
    {
        LedgerCurrentBalance(LedgerId);
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (TxtRemarks.Text.IsBlankOrEmpty())
            {
                if (_tagStrings.Contains(_invoiceType) || _tagStrings.Contains(_actionTag))
                {
                    var msg = _tagStrings.Contains(_invoiceType)
                        ? "PLEASE ENTER THE REMARKS FOR RETURN"
                        : $"PLEASE ENTER THE REMARKS FOR {_actionTag}";

                    this.NotifyValidationError(TxtRemarks, msg);
                    return;
                }

                if (TxtRemarks.Enabled && ObjGlobal.SalesRemarksMandatory)
                {
                    this.NotifyValidationError(TxtRemarks, $"PLEASE ENTER THE REMARKS FOR {_actionTag}");
                    return;
                }
            }

            if (_actionTag == "REVERSE")
            {
                PDetails.Visible = PDetails.Enabled = true;
                PDetails.Focus();
                btnSave.Focus();
            }
            else
            {
                btnSave.Focus();
            }
        }
    }

    private void TxtRemarks_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtRemarks, 'E');
    }

    private void TxtRemarks_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtRemarks, 'L');
    }

    private void TxtRemarks_Validated(object sender, EventArgs e)
    {
        if (ObjGlobal.SalesRemarksMandatory && TxtRemarks.Text.Trim() == string.Empty)
        {
            MessageBox.Show(@"REMARKS IS MANDATORY, SO IT CAN'T BE LEFT BLANK..!!", ObjGlobal.Caption);
            TxtRemarks.Focus();
        }
    }

    private void TxtBillDiscountPercentage_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountPercentage, 'E');
        TxtBillDiscountPercentage.SelectAll();
    }

    private void TxtBillDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
    {
        GlobalControl_KeyPress(sender, e);
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtBillDiscountPercentage_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountPercentage, 'L');
    }

    private void TxtBillDiscountPercentage_Validated(object sender, EventArgs e)
    {
        if (TxtBillDiscountPercentage.Text.GetDecimal() > 0)
        {
            if (ObjGlobal.ReturnDecimal(TxtBillDiscountPercentage.Text) > 100)
            {
                MessageBox.Show(@"DISCOUNT PERCENTAGE CAN'T MORE THEN 100%..!!", ObjGlobal.Caption);
                TxtBillDiscountPercentage.Clear();
                TxtBillDiscountPercentage.Focus();
            }
            else
            {
                TxtBillDiscountPercentage.Text = ((object)TxtBillDiscountPercentage.Text).GetDecimalString();
            }
        }
        else
        {
            TxtBillDiscountPercentage.Text = ObjGlobal.ReturnDouble("0").ToString(ObjGlobal.SysAmountFormat);
        }
    }

    private void TxtBillDiscountAmount_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountAmount, 'E');
        TxtBillDiscountAmount.SelectAll();
    }

    private void TxtBillDiscountAmount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) TxtTenderAmount.Focus();

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtBillDiscountAmount_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtBillDiscountAmount, 'L');
        if (TxtBillDiscountAmount.Text.GetDecimal() > 0)
            TxtBillDiscountAmount.Text = TxtBillDiscountAmount.Text.GetDecimalString();
    }

    private void TxtBillDiscountAmount_Validating(object sender, CancelEventArgs e)
    {
        try
        {
            if (ObjGlobal.ReturnDecimal(TxtBillDiscountAmount.Text) > ObjGlobal.ReturnDecimal(TxtBasicAmount.Text))
            {
                MessageBox.Show(@"DISCOUNT AMOUNT CAN'T BE GREATER THEN BASIC AMOUNT..!!", ObjGlobal.Caption);
                TxtBillDiscountAmount.Focus();
            }
            else
            {
                TxtNetAmount.Text =
                    (ObjGlobal.ReturnDouble(TxtBasicAmount.Text) -
                     ObjGlobal.ReturnDouble(TxtBillDiscountAmount.Text)).ToString(ObjGlobal.SysAmountFormat);
            }
        }
        catch
        {
            // ~~ignored~~
        }
    }

    private void TxtTenderAmt_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtTenderAmount, 'E');
        TxtTenderAmount.SelectAll();
    }

    private void TxtTenderAmt_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (CmbPaymentType.SelectedIndex == 0)
            {
                if (TxtTenderAmount.GetDecimal() is 0 && !_tagStrings.Contains(_invoiceType))
                {
                    this.NotifyValidationError(TxtTenderAmount, @"TENDER AMOUNT CAN NOT LEFT BLANK OR ZERO AMOUNT");
                    return;
                }

                if (TxtTenderAmount.GetDecimal() != 0 &&
                    TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal() < 0)
                {
                    this.NotifyValidationError(TxtTenderAmount, @"TENDER AMOUNT CAN NOT LESS THEN INVOICE INVOICE");
                    return;
                }
            }

            if (ObjGlobal.SalesRemarksEnable && TxtRemarks.Enabled)
                TxtRemarks.Focus();
            else
                btnSave.Focus();
        }
    }

    private void TxtTenderAmt_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtTenderAmt_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtTenderAmount, 'L');
    }

    private void PDetails_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            PDetails.Visible = false;
            TxtProduct.Focus();
        }
    }

    private void TxtTenderAmt_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl.Name == "TxtChangeAmount" && TxtChangeAmount.GetDecimal() is 0 &&
            !_tagStrings.Contains(_invoiceType))
        {
            e.Cancel = true;
            MessageBox.Show(@"TENDER AMOUNT CAN'T BE BLANK..!!", ObjGlobal.Caption);
            TxtTenderAmount.Focus();
        }

        if (TxtTenderAmount.GetDecimal() > 0 && TxtTenderAmount.Text.GetDouble() < TxtNetAmount.Text.GetDouble() &&
            TxtTenderAmount.Focused && PDetails.Visible)
        {
            e.Cancel = true;
            MessageBox.Show(@"TENDER AMOUNT CAN'T BE LESS THAN BILL AMOUNT..!!", ObjGlobal.Caption);
            TxtTenderAmount.Focus();
        }

        TxtTenderAmount.Text = ObjGlobal.ReturnDouble(TxtTenderAmount.Text).ToString(ObjGlobal.SysAmountFormat);
    }

    private void BtnDayClosing_Click(object sender, EventArgs e)
    {
        new FrmCashClosing().ShowDialog();
    }

    private void BtnTodaySales_Click(object sender, EventArgs e)
    {
        _entry.SbMaster.Invoice_Date = DateTime.Now;
        var frmPickList = new FrmAutoPopList("MAX", "TODAYSALES", ObjGlobal.SearchText, _actionTag, "ALL", "TRANSACTION");
        if (FrmAutoPopList.GetListTable == null || FrmAutoPopList.GetListTable.Rows.Count <= 0) return;
        frmPickList.ShowDialog();
        if (frmPickList.SelectedList.Count > 0)
        {
        }
        frmPickList.Dispose();
    }

    private void BtnPatient_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetPatient(_actionTag);
        if (description.IsValueExits())
        {
            TxtPatientId.Text = description;
            PatientId = id;
            FillPatientInfo(PatientId);
        }

        TxtPatientId.Focus();
    }

    private void TxtPatientId_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnPatient_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            TxtProduct.Focus();
        }
    }

    private void TxtPatientId_Validated(object sender, EventArgs e)
    {
        if (ActiveControl.Name != "TxtProduct") return;
        if (TxtPatientId.IsValueExits())
        {
            var dtPatient = _master.CheckPatientIdExitsOrNot(TxtPatientId.Text);
            if (dtPatient.Rows.Count > 0)
            {
                FillPatientInfo(dtPatient.Rows[0]["PaitentId"].GetLong());
            }
            else
            {
                var frm = new FrmPatient(true);
                frm.ShowDialog();
                FillPatientInfo(frm.PatientId.GetLong());
            }
        }
        else if (RGrid.RowCount is 0)
        {
            var frm = new FrmPatient(true);
            frm.ShowDialog();
            TxtPatientId.Text = frm.PatientDesc;
            FillPatientInfo(frm.PatientId.GetLong());
            TxtPatientId.Focus();
        }
    }

    private void TxtPatientId_KeyPress(object sender, KeyPressEventArgs e)
    {
        e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
    }

    private void TxtTenderAmount_TextChanged(object sender, EventArgs e)
    {
        if (TxtTenderAmount.Focused)
            TxtChangeAmount.Text = (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString();
    }

    private void TxtBillDiscountPercentage_TextChanged(object sender, EventArgs e)
    {
        TxtBillDiscountAmount.Text =
            (TxtBillDiscountPercentage.Text.GetDecimal() * TxtBasicAmount.Text.GetDecimal() / 100)
            .GetDecimalString();
        TxtNetAmount.Text =
            (TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal()).ToString(ObjGlobal.SysAmountFormat);
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(((object)TxtNetAmount.Text).GetDecimalString());
    }

    private void TxtBillDiscountAmount_TextChanged(object sender, EventArgs e)
    {
        if (!TxtBillDiscountAmount.Focused) return;
        if (TxtBasicAmount.GetDecimal() <= TxtBillDiscountAmount.GetDecimal())
            this.NotifyValidationError(TxtBillDiscountAmount,
                "DISCOUNT AMOUNT CANNOT BE GREATER THAN INVOICE AMOUNT");

        TotalCalculationOfInvoice();
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
            btnSave.Enabled = false;
            if (IsValidInvoice())
            {
                var result = 0;
                CreateDatabaseTable.DropTrigger();
                result = _actionTag switch
                {
                    "REVERSE" => ReverseSelectedInvoiceNumber(),
                    _ => _invoiceType.Equals("RETURN") ? SavePosReturnInvoice() : SaveOpbBillingInvoice()
                };
                if (result > 0)
                {
                    btnSave.Enabled = true;
                    if (_actionTag.Equals("SAVE"))
                    {
                        _entry.UpdateDocumentNumbering(_invoiceType.Equals("RETURN") ? "SR" : "SB",
                            _invoiceType.Equals("RETURN") ? _returnNumberSchema : _docDesc);
                        if (ChkPrint.Checked && _actionTag == "SAVE")
                        {
                            PrintVoucher();
                        }
                    }
                    var resultDesc = _invoiceType.Equals("RETURN") ? "SALES RETURN INVOICE" : "SALES INVOICE";
                    this.NotifySuccess($@"{TxtVNo.Text} {resultDesc} {_actionTag} SUCCESSFULLY..!!");
                    if (_actionTag == "REVERSE")
                    {
                        EnableControl(true);
                    }
                    if (_invoiceType.Equals("RETURN"))
                    {
                        Close();
                        return;
                    }
                    _invoiceType = "NORMAL";
                    _actionTag = _actionTag.Equals("UPDATE") ? _actionTag :
                        _actionTag.IsValueExits() ? "SAVE" : _actionTag;
                    LblDisplayReceivedAmount.Text = TxtTenderAmount.Text;
                    LblDisplayReturnAmount.Text = TxtChangeAmount.Text;
                    ClearControl();
                    PnlInvoiceDetails.Visible = true;
                    if (_actionTag.Equals("UPDATE"))
                        TxtVNo.Focus();
                    else
                        MskMiti.Focus();
                }
                else
                {
                    btnSave.Enabled = true;
                    this.NotifyError($@" ERROR OCCURS WHILE {TxtVNo.Text} {_actionTag} ..!!");
                    MskMiti.Focus();
                }

                CreateDatabaseTable.CreateTrigger();
            }
            else
            {
                btnSave.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            btnSave.Enabled = true;
            CreateDatabaseTable.CreateTrigger();
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
        }
    }

    private void BtnHold_Click(object sender, EventArgs e)
    {
        try
        {
            BtnHold.Enabled = false;
            _invoiceType = "HOLD";
            if (IsValidInvoice())
            {
                if (SavePosInvoiceHold() != 0)
                {
                    BtnHold.Enabled = true;
                    this.NotifySuccess($"{TxtVNo.Text} NUMBER HOLD SUCCESSFULLY..!!");
                    ClearControl();
                    TxtProduct.Focus();
                }
                else
                {
                    BtnHold.Enabled = true;
                    MessageBox.Show(@"ERROR OCCURS WHILE SAVE DATA", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(@"ERROR OCCURS WHILE SAVE DATA", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                BtnHold.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            BtnHold.Enabled = true;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
        }
    }

    private void GlobalControl_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void GlobalControl_Leave(object sender, EventArgs e)
    {
        if (sender is TextBox box) ObjGlobal.TxtBackColor(box, 'L');
    }

    private void GlobalControl_Enter(object sender, EventArgs e)
    {
        if (sender is TextBox box) ObjGlobal.TxtBackColor(box, 'E');
    }

    private void BtnDoctor_Click(object sender, EventArgs e)
    {
    }

    private void TxtDoctor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateHospitalDepartment(true);
            if (id > 0)
            {
                TxtDepartment.Text = description;
                DepartmentId = id;
            }
            TxtDepartment.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsBlankOrEmpty())
            {
                TxtDepartment.WarningMessage(" DEPARTMENT IS REQUIRED..!!");
                return;
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetHospitalDepartmentList(_actionTag);
        if (description.IsValueExits())
        {
            TxtDepartment.Text = description;
            DepartmentId = id;
        }
        TxtDepartment.Focus();
    }

    #endregion --------------- SALES INVOICE ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private int ReverseSelectedInvoiceNumber()
    {
        if (_invoiceType.Equals("RETURN"))
        {
            _entry.SrMaster.SR_Invoice = TxtVNo.Text;
        }
        else
        {
            _entry.SbMaster.SB_Invoice = TxtVNo.Text;
        }

        return _invoiceType.Equals("RETURN") ? _entry.SavePosReturnInvoice(_actionTag) : _entry.SavePosInvoice(_actionTag);
    }

    private int SaveOpbBillingInvoice()
    {
        TxtVNo.Text =
            _actionTag.Equals("SAVE")
                ? TxtVNo.GetCurrentVoucherNo("POS", _docDesc)
                : TxtVNo.Text; //    ObjGlobal.ReturnDocumentNumbering("AMS.SB_Master", "SB_Invoice", "POS", _docDesc)
        _entry.SbMaster.SB_Invoice = TxtVNo.Text;
        _entry.SbMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _entry.SbMaster.Invoice_Miti = MskMiti.Text;
        _entry.SbMaster.Invoice_Time = DateTime.Now;
        _entry.SbMaster.PB_Vno = _tempSalesInvoice;
        _entry.SbMaster.Vno_Date = _tempSalesInvoice.IsValueExits() ? _tempInvoiceDate.GetDateTime() : DateTime.Now;
        _entry.SbMaster.Vno_Miti = _tempInvoiceMiti;
        _entry.SbMaster.Customer_Id = LedgerId;

        _entry.SbMaster.PartyLedgerId = PatientId;
        _entry.SbMaster.Party_Name = LblPatientDesc.Text;
        _entry.SbMaster.Vat_No = LblPatientAge.Text;
        _entry.SbMaster.Contact_Person = string.Empty;
        _entry.SbMaster.Mobile_No = string.Empty;
        _entry.SbMaster.Address = LblPatientNumber.Text;
        _entry.SbMaster.ChqNo =
            _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _entry.SbMaster.ChqDate =
            _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString())
                ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString())
                : DateTime.Now;

        _entry.SbMaster.Invoice_Type = "LOCAL";
        _entry.SbMaster.Invoice_Mode = "POS";
        _entry.SbMaster.Payment_Mode = CmbPaymentType.Text;
        _entry.SbMaster.DueDays = 0;
        _entry.SbMaster.DueDate = DateTime.Now;
        _entry.SbMaster.Agent_Id = AgentId;
        _entry.SbMaster.Subledger_Id = SubLedgerId;
        _entry.SbMaster.SO_Invoice = string.Empty;
        _entry.SbMaster.SO_Date = DateTime.Now;
        _entry.SbMaster.SC_Invoice = string.Empty;
        _entry.SbMaster.SC_Date = DateTime.Now;
        _entry.SbMaster.HDepartmentId = DepartmentId;
        _entry.SbMaster.Cls1 = 0;
        _entry.SbMaster.Cls2 = 0;
        _entry.SbMaster.Cls3 = 0;
        _entry.SbMaster.Cls4 = 0;
        _entry.SbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _entry.SbMaster.Cur_Rate = 1;
        _entry.SbMaster.CounterId = CounterId;
        _entry.SbMaster.PatientId = PatientId;
        _entry.SbMaster.DoctorId = DoctorId;
        _entry.SbMaster.HDepartmentId = DepartmentId;
        _entry.SbMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _entry.SbMaster.SpecialDiscountRate = TxtBillDiscountPercentage.GetDecimal();
        _entry.SbMaster.SpecialDiscount = TxtBillDiscountAmount.GetDecimal();
        _entry.SbMaster.VatRate = 0;
        _entry.SbMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _entry.SbMaster.VatAmount = 0;
        _entry.SbMaster.N_Amount = TxtNetAmount.GetDecimal();
        _entry.SbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _entry.SbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _entry.SbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _entry.SbMaster.V_Amount = 0;
        _entry.SbMaster.Tbl_Amount = 0;
        _entry.SbMaster.Action_Type = _actionTag;
        _entry.SbMaster.R_Invoice = false;
        _entry.SbMaster.No_Print = 0;
        _entry.SbMaster.In_Words = LblNumberInWords.Text;
        _entry.SbMaster.Remarks = TxtRemarks.Text;
        _entry.SbMaster.Audit_Lock = false;
        //_entry.SbMaster.GetView = RGrid;
        return _entry.SaveOpdBillingInvoice(_actionTag);
    }

    private int SavePosReturnInvoice()
    {
        TxtVNo.Text = _actionTag.Equals("SAVE") ? TxtVNo.GetCurrentVoucherNo("SR", _returnNumberSchema) : TxtVNo.Text;
        _entry.SrMaster.SR_Invoice = TxtVNo.Text;
        _entry.SrMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _entry.SrMaster.Invoice_Miti = MskMiti.Text;
        _entry.SrMaster.Invoice_Time = DateTime.Now;
        _entry.SrMaster.SB_Invoice = TxtRefVno.Text;
        _entry.SrMaster.SB_Date = TxtRefVno.Text.IsValueExits() ? _refInvoiceDate.GetDateTime() : DateTime.Now;
        _entry.SrMaster.SB_Miti = _refInvoiceMiti;
        _entry.SrMaster.Customer_ID = LedgerId;

        _entry.SrMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
        _entry.SrMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _entry.SrMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
        _entry.SrMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _entry.SrMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _entry.SrMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _entry.SrMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;

        _entry.SrMaster.Invoice_Type = "LOCAL";
        _entry.SrMaster.Invoice_Mode = "POS";
        _entry.SrMaster.Payment_Mode = CmbPaymentType.Text;
        _entry.SrMaster.DueDays = 0;
        _entry.SrMaster.DueDate = DateTime.Now;
        _entry.SrMaster.Agent_Id = AgentId;
        _entry.SrMaster.Subledger_Id = SubLedgerId;
        _entry.SrMaster.Cls1 = 0;
        _entry.SrMaster.Cls2 = 0;
        _entry.SrMaster.Cls3 = 0;
        _entry.SrMaster.Cls4 = 0;
        _entry.SrMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _entry.SrMaster.Cur_Rate = 1;
        _entry.SrMaster.CounterId = CounterId;
        _entry.SrMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _entry.SrMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _entry.SrMaster.N_Amount = TxtNetAmount.GetDecimal();
        _entry.SrMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _entry.SrMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _entry.SrMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _entry.SrMaster.V_Amount = 0;
        _entry.SrMaster.Tbl_Amount = 0;
        _entry.SrMaster.Action_Type = _actionTag;
        _entry.SrMaster.R_Invoice = false;
        _entry.SrMaster.No_Print = 0;
        _entry.SrMaster.In_Words = LblNumberInWords.Text;
        _entry.SrMaster.Remarks = TxtRemarks.Text;
        _entry.SrMaster.Audit_Lock = false;

        //_entry.SrMaster.GetView = RGrid;

        return _entry.SavePosReturnInvoice(_actionTag);
    }

    private int SavePosInvoiceHold()
    {
        ReturnTsbVoucherNumber();
        _entry.TsbMaster.SB_Invoice = TxtVNo.Text;
        _entry.TsbMaster.Invoice_Date = MskDate.Text.GetDateTime();
        _entry.TsbMaster.Invoice_Miti = MskMiti.Text;
        _entry.TsbMaster.Invoice_Time = DateTime.Now;
        _entry.TsbMaster.PB_Vno = string.Empty;
        _entry.TsbMaster.Vno_Date = DateTime.Now;
        _entry.TsbMaster.Vno_Miti = string.Empty;
        _entry.TsbMaster.Customer_Id = LedgerId;

        _entry.TsbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0
            ? _dtPartyInfo.Rows[0]["PartyName"].ToString()
            : string.Empty;
        _entry.TsbMaster.Vat_No =
            _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
        _entry.TsbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0
            ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString()
            : string.Empty;
        _entry.TsbMaster.Mobile_No =
            _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
        _entry.TsbMaster.Address =
            _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
        _entry.TsbMaster.ChqNo =
            _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
        _entry.TsbMaster.ChqDate =
            _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString())
                ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString())
                : DateTime.Now;
        _entry.TsbMaster.Invoice_Type = "LOCAL";
        _entry.TsbMaster.Invoice_Mode = "NORMAL";
        _entry.TsbMaster.Payment_Mode = CmbPaymentType.Text;
        _entry.TsbMaster.DueDays = 0;
        _entry.TsbMaster.DueDate = DateTime.Now;
        _entry.TsbMaster.Agent_Id = AgentId;
        _entry.TsbMaster.Subledger_Id = SubLedgerId;
        _entry.TsbMaster.SO_Invoice = string.Empty;
        _entry.TsbMaster.SO_Date = DateTime.Now;
        _entry.TsbMaster.SC_Invoice = string.Empty;
        _entry.TsbMaster.SC_Date = DateTime.Now;
        _entry.TsbMaster.Cls1 = DepartmentId;
        _entry.TsbMaster.Cls2 = 0;
        _entry.TsbMaster.Cls3 = 0;
        _entry.TsbMaster.Cls4 = 0;
        _entry.TsbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _entry.TsbMaster.Cur_Rate = 1;
        _entry.TsbMaster.CounterId = CounterId;
        _entry.TsbMaster.B_Amount = TxtBasicAmount.GetDecimal();
        _entry.TsbMaster.TermRate = TxtBillDiscountPercentage.GetDecimal();
        _entry.TsbMaster.T_Amount = TxtBillDiscountAmount.GetDecimal();
        _entry.TsbMaster.N_Amount = TxtNetAmount.GetDecimal();
        _entry.TsbMaster.LN_Amount = TxtNetAmount.GetDecimal();
        _entry.TsbMaster.Tender_Amount = TxtTenderAmount.GetDecimal();
        _entry.TsbMaster.Return_Amount = TxtChangeAmount.GetDecimal();
        _entry.TsbMaster.V_Amount = 0;
        _entry.TsbMaster.Tbl_Amount = 0;
        _entry.TsbMaster.Action_Type = _actionTag;
        _entry.TsbMaster.R_Invoice = false;
        _entry.TsbMaster.No_Print = 0;
        _entry.TsbMaster.In_Words = LblNumberInWords.Text;
        _entry.TsbMaster.Remarks = TxtRemarks.Text;
        _entry.TsbMaster.Audit_Lock = false;
        _entry.TsbMaster.GetView = RGrid;

        return _entry.SaveTempSalesInvoice(_actionTag);
    }

    private bool IsValidInvoice()
    {
        if (TxtVNo.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVNo, "VOUCHER NUMBER CANNOT BE BLANK..!!");
            return false;
        }

        if (TxtCounter.Text.IsBlankOrEmpty() || CounterId is 0)
        {
            this.NotifyValidationError(TxtCounter, "TERMINAL IS REQUIRED FOR BILLING..!!");
            return false;
        }

        if (TxtProduct.Text.IsBlankOrEmpty() && RGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtProduct, "INVOICE PRODUCT DETAILS IS MISSING CANNOT SAVE BLANK..!!");
            return false;
        }

        if (TxtCustomer.Text.IsBlankOrEmpty() || LedgerId is 0)
        {
            this.NotifyValidationError(TxtCustomer, "INVOICE CUSTOMER DETAILS IS MISSING CANNOT SAVE BLANK..!!");
            return false;
        }

        if (TxtBillDiscountPercentage.GetDecimal() >= 100)
        {
            TxtBillDiscountPercentage.Clear();
            this.NotifyValidationError(TxtBillDiscountPercentage, "DISCOUNT PERCENTAGE CAN'T MORE THEN 100%..!!");
            return false;
        }

        if (TxtBillDiscountAmount.GetDecimal() >= TxtBasicAmount.GetDecimal())
        {
            TxtBillDiscountPercentage.Clear();
            this.NotifyValidationError(TxtBillDiscountPercentage,
                "DISCOUNT AMOUNT CAN'T MORE THEN INVOICE AMOUNT ..!!");
            return false;
        }

        if (TxtTenderAmount.GetDecimal() is 0 && _invoiceType.Equals("NORMAL"))
        {
            this.NotifyValidationError(TxtTenderAmount, "TENDER AMOUNT CAN'T BE ZERO..!!");
            return false;
        }

        if (TxtPatientId.IsValueExits() && PatientId is 0)
        {
            this.NotifyValidationError(TxtPatientId, "SELECTED MEMBER IS INVALID..!!");
            return false;
        }

        return true;
    }

    private void PrintVoucher()
    {
        var module = _invoiceType.Equals("RETURN") ? "SR" : "SB";
        var dtDesign = _setup.GetPrintVoucherList(module);
        if (dtDesign.Rows.Count <= 0)
        {
            return;
        }
        var frmDp = new FrmDocumentPrint("Crystal", module, TxtVNo.Text, TxtVNo.Text, true)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }

    private void FillPatientInfo(long patientId)
    {
        if (patientId is 0) return;
        var dtPatient = _master.ReturnPatientInformation(patientId);
        if (dtPatient.Rows.Count <= 0) return;
        PatientId = patientId;
        DoctorId = dtPatient.Rows[0]["DrId"].GetInt();
        DepartmentId = dtPatient.Rows[0]["DepartmentId"].GetInt();
        LblPatientDesc.Text = dtPatient.Rows[0]["AccountLedger"].ToString();
        LblPatientAge.Text = dtPatient.Rows[0]["Age"].ToString();
        LblPatientNumber.Text = dtPatient.Rows[0]["ContactNo"].ToString();
        LblPatientAddress.Text = dtPatient.Rows[0]["TAddress"].ToString();
    }

    private void SetProductInfo()
    {
        if (ProductId is 0) return;
        var dtProduct = _setup.GetPosProductInfo(ProductId.ToString());
        if (dtProduct.Rows.Count <= 0) return;
        PnlProductDetails.Visible = true;
        PnlProductDetails.Enabled = true;
        TxtProduct.Text = dtProduct.Rows[0]["PName"].GetString();
        LblProduct.Text = TxtProduct.Text;
        LblBarcode.Text = dtProduct.Rows[0]["PShortName"].GetString();
        SalesRate = dtProduct.Rows[0]["PSalesRate"].GetDecimal();
        UnitId = dtProduct.Rows[0]["PUnit"].GetInt();
        AltUnitId = dtProduct.Rows[0]["PAltUnit"].GetInt();
        AltQtyConv = dtProduct.Rows[0]["PAltConv"].GetDecimal();
        QtyConv = dtProduct.Rows[0]["PQtyConv"].GetDecimal();
        TxtUnit.Text = dtProduct.Rows[0]["UOM"].GetString();
        GetAltUnit = dtProduct.Rows[0]["AltUOM"].GetString();
        TaxRate = dtProduct.Rows[0]["PTax"].GetDecimal();
        IsTaxable = TaxRate > 0;
        GetMrp = dtProduct.Rows[0]["PMRP"].GetDecimal();
        TxtQty.Text = TxtQty.GetDecimal() is 0 ? 1.00.GetDecimalQtyString() : TxtQty.Text.GetDecimalQtyString();
        LblBarcode.Text = TxtProduct.Text;
        LblUnit.Text = TxtUnit.Text;
        LblSalesRate.Text = SalesRate.GetDecimalString();
    }

    private void TotalCalculationOfInvoice()
    {
        var viewRows = RGrid.Rows.OfType<DataGridViewRow>();
        var gridViewRows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

        LblItemsTotalQty.Text = gridViewRows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
        LblItemsTotal.Text = gridViewRows.Sum(row => row.Cells["GTxtDisplayAmount"].Value.GetDecimal()).GetDecimalString();
        LblItemsDiscountSum.Text = gridViewRows.Sum(row => row.Cells["GTxtPDiscount"].Value.GetDecimal()).GetDecimalString();
        LblItemsNetAmount.Text = gridViewRows.Sum(row => row.Cells["GTxtDisplayNetAmount"].Value.GetDecimal()).GetDecimalString();

        TxtBasicAmount.Text = LblItemsNetAmount.Text;
        TxtBillDiscountPercentage_TextChanged(null, EventArgs.Empty);
        var netAmount = TxtBasicAmount.GetDecimal() - TxtBillDiscountAmount.GetDecimal();
        TxtNetAmount.Text = netAmount.GetDecimalString();

        TxtChangeAmount.Text = TxtTenderAmount.GetDecimal() > 0
            ? (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetDecimalString()
            : 0.GetDecimalString();

        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(TxtNetAmount.Text.GetDecimalString());
    }

    private void EnableControl(bool isEnable)
    {
        btnNew.Enabled = !isEnable;
        TxtVNo.Enabled = _tagStrings.Contains(_actionTag);
        BtnVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtRefVno.Visible = BtnRefVno.Visible = _invoiceType.Equals("RETURN");
        TxtRefVno.Enabled = BtnRefVno.Enabled = _invoiceType.Equals("RETURN");
        TxtProduct.Enabled = isEnable;
        TxtDepartment.Enabled = isEnable;
        TxtCounter.Enabled = isEnable;
        BtnCounter.Enabled = isEnable;
        TxtBasicAmount.ReadOnly = true;
        TxtBasicAmount.Enabled = isEnable;
        TxtChangeAmount.ReadOnly = true;
        TxtChangeAmount.Enabled = isEnable;
        TxtTenderAmount.Enabled = isEnable;
        TxtNetAmount.ReadOnly = true;
        TxtNetAmount.Enabled = isEnable;
        CmbPaymentType.Enabled = isEnable;
        TxtCustomer.Enabled = isEnable;
        btnCustomer.Enabled = isEnable;
        TxtQty.Enabled = isEnable;
        TxtUnit.Enabled = false;
        TxtBillDiscountAmount.Enabled = isEnable;
        TxtBillDiscountPercentage.Enabled = isEnable;
        TxtRemarks.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        btnSave.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        BtnHold.Visible = !_tagStrings.Contains(_invoiceType) ||
                          !_invoiceType.Equals("RETURN") && _actionTag.Equals("SAVE");
        BtnHold.Enabled = BtnHold.Visible;
        RGrid.ReadOnly = true;
    }

    private void ClearControl()
    {
        var module = IsIPDBilling ? "IPD" : "OPD";
        Text = _invoiceType switch
        {
            "RETURN" when _actionTag.IsValueExits() => $"REFUND PATIENT {module} BILLING [{_actionTag}]",
            "NORMAL" when _actionTag.IsValueExits() => $"PATIENT {module} BILLING [{_actionTag}]",
            _ => $"PATIENT {module} BILLING"
        };
        TxtVNo.Clear();
        TxtVNo.Text = _actionTag.Equals("SAVE")
            ? TxtVNo.GetCurrentVoucherNo("SB", _docDesc)
            : TxtVNo.Text;
        if (RGrid.Rows.Count > 0)
        {
            RGrid.Rows.Clear();
        }
        ClearDetails();
        LblInvoiceNo.Visible = false;
        TxtRefVno.Enabled = TxtRefVno.Visible = false;
        BtnRefVno.Enabled = BtnRefVno.Visible = false;
        TxtRefVno.Clear();
        LblItemsTotal.Text = string.Empty;
        LblItemsTotalQty.Text = string.Empty;
        LblItemsDiscountSum.Text = string.Empty;
        LblItemsNetAmount.Text = string.Empty;
        TxtPatientId.Clear();
        PatientId = 0;
        CmbPaymentType.SelectedIndex = 0;
        TxtCustomer.Clear();
        LedgerId = ObjGlobal.FinanceCashLedgerId.GetLong();
        TxtCustomer.Text = _setup.GetLedgerDescription(LedgerId);
        TxtBillDiscountAmount.Clear();
        TxtBillDiscountPercentage.Clear();
        TxtBasicAmount.Clear();
        TxtNetAmount.Clear();
        TxtTenderAmount.Clear();
        TxtChangeAmount.Clear();
        TxtRemarks.Clear();
        _dtPartyInfo.Clear();
        PatientId = 0;
        LblNumberInWords.Text = string.Empty;
        LblPatientNumber.IsClear();
        LblPatientDesc.IsClear();
        LblPatientAge.IsClear();
        PDetails.Visible = false;
    }

    private void ClearDetails()
    {
        SGridId = -1;
        IsRowUpdate = false;
        ProductId = 0;
        UnitId = 0;
        GetUnitId = 0;
        GetAltUnit = string.Empty;
        GetAltQty = 0;
        GetSalesRate = 0;
        GetMrp = 0;
        GetQty = 0;
        PDiscount = 0;
        PDiscountPercentage = 0;
        SalesRate = 0;
        TxtProduct.Clear();
        TxtQty.Text = 1.GetDecimalQtyString();
        TxtUnit.Clear();
        PnlProductDetails.Visible = false;
        PnlProductDetails.Enabled = PnlProductDetails.Visible;
        LblProduct.Text = string.Empty;
        LblBarcode.Text = string.Empty;
        LblUnit.Text = string.Empty;
        LblSalesRate.Text = string.Empty;
        PnlInvoiceDetails.Visible = false;
    }

    private void SetDataFromGridToTextBox()
    {
        if (RGrid.CurrentRow != null)
        {
            IsRowUpdate = true;
            ProductId = RGrid.Rows[SGridId].Cells["GTxtProductId"].Value.GetLong();
            TxtProduct.Text = RGrid.Rows[SGridId].Cells["GTxtShortName"].Value.GetString();
            SetProductInfo();
            GetAltQty = RGrid.Rows[SGridId].Cells["GTxtAltQty"].Value.GetDecimal();
            TxtQty.Text = RGrid.Rows[SGridId].Cells["GTxtQty"].Value.GetDecimalQtyString();
            GetSalesRate = RGrid.Rows[SGridId].Cells["GTxtDisplayRate"].Value.GetDecimal();
            PDiscountPercentage = RGrid.Rows[SGridId].Cells["GTxtDiscountRate"].Value.GetDecimal();
            PDiscount = RGrid.Rows[SGridId].Cells["GTxtPDiscount"].Value.GetDecimal();
        }

        IsRowUpdate = true;
        TxtProduct.Focus();
    }

    private void AddDataToGridDetails(bool isUpdate)
    {
        LblDisplayReceivedAmount.Text = string.Empty;
        LblDisplayReturnAmount.Text = string.Empty;
        if (ProductId is 0 || TxtProduct.Text.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtProduct, "INVALID PRODUCT OR PLEASE CHECK THE BARCODE..!!");
            return;
        }

        if (TxtQty.Text.GetDecimal() is 0)
        {
            this.NotifyValidationError(TxtProduct, "QUANTITY CANNOT BE ZERO..!!");
            return;
        }

        if (SalesRate is 0)
            if (MessageBox.Show(@"SALES RATE IS ZERO. DO YOU WANT TO CONTINUE..!!", ObjGlobal.Caption,
                    MessageBoxButtons.YesNo) is DialogResult.No)
            {
                TxtProduct.Focus();
                return;
            }

        var newRowQty = false;
        if (!isUpdate)
        {
            var row = RGrid.Rows.Cast<DataGridViewRow>().FirstOrDefault(r =>
                string.Equals(r.Cells["GTxtProductId"].Value.GetString(), $"{ProductId}"));
            if (row != null)
                if (MessageBox.Show(
                        $@"{TxtProduct.Text.ToUpperInvariant()} PRODUCT IS ALREADY ADDED ON THIS INVOICE. DO YOU WANT TO UPDATE..??",
                        ObjGlobal.Caption, MessageBoxButtons.YesNo) is DialogResult.Yes)
                {
                    newRowQty = true;
                    SGridId = row.Index;
                }
        }

        if (newRowQty)
        {
            isUpdate = true;
        }
        else if (!isUpdate)
        {
            RGrid.Rows.Add();
            SGridId = RGrid.RowCount - 1;
        }

        RGrid.Rows[SGridId].Cells["GTxtSNo"].Value =
            !isUpdate ? RGrid.RowCount : RGrid.Rows[SGridId].Cells["GTxtSNo"].Value;
        RGrid.Rows[SGridId].Cells["GTxtProductId"].Value = ProductId;
        RGrid.Rows[SGridId].Cells["GTxtShortName"].Value = LblBarcode.Text;
        RGrid.Rows[SGridId].Cells["GTxtProduct"].Value = TxtProduct.Text;
        RGrid.Rows[SGridId].Cells["GTxtGodownId"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtGodown"].Value = string.Empty;
        RGrid.Rows[SGridId].Cells["GTxtAltQty"].Value = GetAltQty > 0 ? GetAltQty : string.Empty;
        RGrid.Rows[SGridId].Cells["GTxtAltUOMId"].Value = AltUnitId;
        RGrid.Rows[SGridId].Cells["GTxtAltUOM"].Value = GetAltUnit;
        var qty = TxtQty.Text.GetDecimal();
        qty = newRowQty ? RGrid.Rows[SGridId].Cells["GTxtQty"].Value.GetDecimal() + qty : qty;
        RGrid.Rows[SGridId].Cells["GTxtQty"].Value = qty.GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtUOMId"].Value = UnitId;
        RGrid.Rows[SGridId].Cells["GTxtMRP"].Value = GetMrp;
        RGrid.Rows[SGridId].Cells["GTxtUOM"].Value = TxtUnit.Text;
        SalesRate = GetSalesRate > 0 ? GetSalesRate : SalesRate;
        RGrid.Rows[SGridId].Cells["GTxtDisplayRate"].Value = SalesRate.GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtDisplayAmount"].Value = (qty * SalesRate).GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtDiscountRate"].Value = PDiscountPercentage.GetDecimal();
        RGrid.Rows[SGridId].Cells["GTxtPDiscount"].Value = PDiscount.GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtValueBDiscount"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtDisplayNetAmount"].Value = (qty * SalesRate - PDiscount).GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtValueRate"].Value = SalesRate;
        RGrid.Rows[SGridId].Cells["GTxtValueNetAmount"].Value = (qty * SalesRate - PDiscount).GetDecimalString();
        RGrid.Rows[SGridId].Cells["GTxtIsTaxable"].Value = IsTaxable;
        RGrid.Rows[SGridId].Cells["GTxtTaxPriceRate"].Value = TaxRate;
        RGrid.Rows[SGridId].Cells["GTxtValueVatAmount"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtValueTaxableAmount"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtValueExemptedAmount"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtNarration"].Value = string.Empty;
        RGrid.Rows[SGridId].Cells["GTxtFreeQty"].Value = 0;
        RGrid.Rows[SGridId].Cells["GTxtFreeUnitId"].Value = 0;
        RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[0];
        TotalCalculationOfInvoice();
        ClearDetails();

        TxtProduct.Focus();
    }

    private void FillInvoiceForUpdate(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        var dtMaster = dsSales.Tables[0];
        var dtDetails = dsSales.Tables[1];
        if (dtMaster.Rows.Count < 0) return;
        foreach (DataRow dr in dtMaster.Rows)
        {
            TxtVNo.Text = dr["SB_Invoice"].ToString();
            MskDate.Text = dr["Invoice_Date"].GetDateString();
            MskMiti.Text = dr["Invoice_Miti"].ToString();
            TxtCustomer.Text = dr["GLName"].ToString();
            LedgerId = dr["Customer_ID"].GetLong();

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
            TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
            TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
            var discount = dr["T_Amount"].GetDecimal() > 0
                ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100
                : 0;
            TxtBillDiscountPercentage.Text = discount.GetDecimalString();
        }

        if (dtDetails.RowsCount() > 0)
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dtDetails.RowsCount());
            foreach (DataRow dr in dtDetails.Rows)
            {
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["AltUnitCode"].ToString();
                var qty = dr["Qty"].GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                var salesRate = dr["Rate"].GetDecimal();
                var pDiscount = dr["PDiscount"].GetDecimal();

                RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
                    (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
                RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = dr["PDiscountRate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = dr["BDiscount"].GetDecimalString();
                var taxRate = dr["PTax"].GetDecimal();
                var isTaxable = taxRate > 0;
                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
                    (qty * salesRate - pDiscount).GetDecimalString();
                var taxableSalesRate = isTaxable ? salesRate / (decimal)1.13 : salesRate;
                RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = taxableSalesRate;
                RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;
                RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;
                var taxableAmount = isTaxable
                    ? RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / (decimal)1.13
                    : RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();
                var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;

                RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = dr["Invoice_SNo"].ToString();
                iRow++;
            }

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void FillInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        var dtMaster = dsSales.Tables[0];
        if (dtMaster.Rows.Count < 0) return;
        foreach (DataRow dr in dtMaster.Rows)
        {
            _selectedInvoice = _invoiceType.Equals("NORMAL") ? TxtVNo.Text : TxtRefVno.Text;
            MskDate.Text = _invoiceType.Equals("RETURN")
                ? DateTime.Now.GetDateString()
                : dr["Invoice_Date"].GetDateString();
            MskMiti.Text = _invoiceType.Equals("RETURN")
                ? MskMiti.GetNepaliDate(MskDate.Text)
                : dr["Invoice_Miti"].ToString();

            if (dr["PB_Vno"].ToString() != string.Empty && _invoiceType.Equals("NORMAL"))
            {
                TxtRefVno.Text = Convert.ToString(dr["PB_Vno"].ToString());
                _refInvoiceMiti = dr["Vno_Miti"].GetDateString();
                _refInvoiceDate = ObjGlobal.ReturnEnglishDate(_refInvoiceMiti);
            }
            else if (_invoiceType.Equals("RETURN"))
            {
                TxtRefVno.Text = dr["SB_Invoice"].ToString();
                _refInvoiceMiti = dr["Invoice_Miti"].ToString();
                _refInvoiceDate = dr["Invoice_Date"].ToString();
            }

            TxtPatientId.Text = dr["RegNumber"].GetString();
            PatientId = dr["PatientId"].GetLong();
            FillPatientInfo(PatientId);

            LedgerId = _setup.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
            LedgerId = LedgerId is 0 ? dr["Customer_ID"].GetLong() : LedgerId;
            TxtCustomer.Text = _setup.GetLedgerDescription(LedgerId);

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
            TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
            TxtChangeAmount.Text = dr["Return_Amount"].GetDecimalString();
            TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();

            if (dsSales.Tables[1].Rows.Count <= 0) return;
            {
                var iRow = 0;
                RGrid.Rows.Clear();
                RGrid.Rows.Add(dsSales.Tables[1].Rows.Count);
                foreach (DataRow row in dsSales.Tables[1].Rows)
                {
                    RGrid.Rows[iRow].Cells["GTxtSNo"].Value = row["Invoice_SNo"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtProductId"].Value = row["P_Id"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtShortName"].Value = row["PShortName"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtProduct"].Value = row["PName"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                    RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                    RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = row["Alt_Qty"].GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = row["Alt_UnitId"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
                    RGrid.Rows[iRow].Cells["GTxtQty"].Value = row["AltUnitCode"].ToString();
                    var qty = row["Qty"].GetDecimal();
                    RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = row["Unit_Id"].ToString();
                    RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                    RGrid.Rows[iRow].Cells["GTxtUOM"].Value = row["UnitCode"].ToString();
                    var salesRate = row["Rate"].GetDecimal();
                    var pDiscount = row["PDiscount"].GetDecimal();

                    RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
                        (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
                    RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = row["PDiscountRate"].GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
                    RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = row["BDiscount"].GetDecimalString();
                    var taxRate = row["PTax"].GetDecimal();
                    var isTaxable = taxRate > 0;
                    RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
                        (qty * salesRate - pDiscount).GetDecimalString();
                    var taxableSalesRate = isTaxable ? salesRate / (decimal)1.05 : salesRate;
                    RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = taxableSalesRate;
                    RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;
                    RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                    RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = taxRate;
                    var taxableAmount = isTaxable
                        ? RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / (decimal)1.05
                        : RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();
                    var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                    RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
                    RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
                    RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;

                    RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                    RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                    RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
                    RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                    RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = row["Invoice_SNo"].ToString();
                    iRow++;
                }

                RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
                ObjGlobal.DGridColorCombo(RGrid);
                TotalCalculationOfInvoice();
                TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                TxtTenderAmount.Text = dr["Tender_Amount"].GetDecimalString();
                TxtChangeAmount.Text = (TxtTenderAmount.GetDecimal() - TxtNetAmount.GetDecimal()).GetRateDecimalString();
                TxtBillDiscountAmount.Text = dr["T_Amount"].GetDecimalString();
                var discount = dr["T_Amount"].GetDecimal() > 0
                    ? dr["T_Amount"].GetDecimal() / dr["B_Amount"].GetDecimal() * 100
                    : 0;
                TxtBillDiscountPercentage.Text = discount.GetDecimalString();
                RGrid.ClearSelection();
            }
        }
    }

    private void FillReturnInvoiceData(string voucherNo)
    {
        var dsSales = _entry.ReturnSalesReturnDetailsInDataSet(voucherNo);
        if (dsSales.Tables.Count <= 0) return;
        foreach (DataRow dr in dsSales.Tables[0].Rows)
        {
            _selectedInvoice = TxtVNo.Text;
            MskDate.Text = DateTime.Now.GetDateString();
            MskMiti.Text = MskDate.GetNepaliDate(MskDate.Text);

            if (dr["SB_Invoice"].ToString() != string.Empty)
            {
                TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());
                _refInvoiceMiti = dr["SB_Date"].GetDateString();
                _refInvoiceDate = dr["SB_Miti"].ToString();
            }

            TxtCustomer.Text = dr["GLName"].ToString();
            LedgerId = dr["Customer_ID"].GetLong();

            _dtPartyInfo.Rows.Clear();
            var newRow = _dtPartyInfo.NewRow();
            newRow["PartyLedgerId"] = null;
            newRow["PartyName"] = dr["Party_Name"];
            newRow["ChequeNo"] = dr["ChqNo"];
            newRow["ChequeDate"] = dr["ChqDate"].GetDateString();
            newRow["VatNo"] = dr["Vat_No"];
            newRow["ContactPerson"] = dr["Contact_Person"];
            newRow["Address"] = dr["Address"];
            newRow["Mob"] = dr["Mobile_No"];
            _dtPartyInfo.Rows.Add(newRow);

            _currencyId = dr["Cur_Id"].GetInt();
            TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
        }

        if (dsSales.Tables[1].Rows.Count <= 0) return;
        {
            var iRow = 0;
            RGrid.Rows.Clear();
            RGrid.Rows.Add(dsSales.Tables[1].Rows.Count);
            foreach (DataRow dr in dsSales.Tables[1].Rows)
            {
                RGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                RGrid.Rows[iRow].Cells["GTxtGodownId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtGodown"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                RGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = GetAltUnit;
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["AltUnitCode"].ToString();
                var qty = dr["Qty"].GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtQty"].Value = qty.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                RGrid.Rows[iRow].Cells["GTxtMRP"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                var salesRate = dr["Rate"].GetDecimal();
                var pDiscount = dr["PDiscount"].GetDecimal();

                RGrid.Rows[iRow].Cells["GTxtDisplayRate"].Value = salesRate.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtDisplayAmount"].Value =
                    (salesRate * qty).ToString(ObjGlobal.SysAmountFormat);
                RGrid.Rows[iRow].Cells["GTxtDiscountRate"].Value = dr["PDiscountRate"].GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtPDiscount"].Value = pDiscount.GetDecimalString();
                RGrid.Rows[iRow].Cells["GTxtValueBDiscount"].Value = dr["BDiscount"].GetDecimalString();
                var TaxRate = dr["PTax"].GetDecimal();
                var isTaxable = TaxRate > 0;
                RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value =
                    (qty * salesRate - pDiscount).GetDecimalString();
                var taxableSalesRate = isTaxable
                    ? salesRate / (decimal)1.13
                    : salesRate;
                RGrid.Rows[iRow].Cells["GTxtValueRate"].Value = taxableSalesRate;
                RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value = taxableSalesRate * qty;
                RGrid.Rows[iRow].Cells["GTxtIsTaxable"].Value = isTaxable;
                RGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = TaxRate;
                var taxableAmount = isTaxable
                    ? RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal() / (decimal)1.13
                    : RGrid.Rows[iRow].Cells["GTxtValueNetAmount"].Value.GetDecimal();
                var netAmount = RGrid.Rows[iRow].Cells["GTxtDisplayNetAmount"].Value.GetDecimal();
                RGrid.Rows[iRow].Cells["GTxtValueVatAmount"].Value = isTaxable ? netAmount - taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueTaxableAmount"].Value = isTaxable ? taxableAmount : 0;
                RGrid.Rows[iRow].Cells["GTxtValueExemptedAmount"].Value = isTaxable ? 0 : taxableAmount;

                RGrid.Rows[iRow].Cells["GTxtNarration"].Value = string.Empty;
                RGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtFreeUnitId"].Value = 0;
                RGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
                RGrid.Rows[iRow].Cells["GTxtInvoiceSNo"].Value = dr["Invoice_SNo"].ToString();
                iRow++;
            }

            RGrid.CurrentCell = RGrid.Rows[RGrid.RowCount - 1].Cells[CIndex];
            ObjGlobal.DGridColorCombo(RGrid);
            TotalCalculationOfInvoice();
            RGrid.ClearSelection();
        }
    }

    private void LedgerCurrentBalance(long selectLedgerId)
    {
        if (selectLedgerId is 0) return;
        var date = MskDate.MaskCompleted ? MskDate.Text.GetSystemDate() : DateTime.Now.GetSystemDate();
        var dtCustomer = ClsMasterSetup.LedgerInformation(selectLedgerId, date);
        if (dtCustomer is { Rows: { Count: > 0 } })
        {
            lblPan.Text = dtCustomer.Rows[0]["PanNo"].ToString();
            lblCreditDays.Text = dtCustomer.Rows[0]["CrDays"].GetDecimalString();
            lblCrLimit.Text = dtCustomer.Rows[0]["CrLimit"].GetDecimalString();
            double.TryParse(dtCustomer.Rows[0]["Amount"].ToString(), out var result);
            lbl_CurrentBalance.Text = result > 0 ? $"{Math.Abs(result).GetDecimalString()} Dr" :
                result < 0 ? $"{Math.Abs(result).GetDecimalString()} Cr" : "0";
            if (_actionTag is not "SAVE") return;
            AgentId = dtCustomer.Rows[0]["AgentId"].GetInt();
            _currencyId = dtCustomer.Rows[0]["CurrId"].GetInt();
        }
        else
        {
            lblPan.Text = 0.GetDecimalString();
            lblCreditDays.Text = 0.GetDecimalString();
            lblCrLimit.Text = 0.GetDecimalString();
            lbl_CurrentBalance.Text = 0.GetDecimalString();
        }
    }

    private void ReturnSbVoucherNumber()
    {
        var module = _invoiceType.Equals("RETURN") ? "SR" : "SB";
        var dt = _setup.IsExitsCheckDocumentNumbering(module);
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVNo.GetCurrentVoucherNo(module, _docDesc);
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVNo.Focus();
        }
    }

    private void ReturnTsbVoucherNumber()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("TSB");
        if (dt?.Rows.Count is 1)
        {
            _holdSalesSchemaNumber = dt.Rows[0]["DocDesc"].ToString();
            TxtVNo.Text = ObjGlobal.ReturnDocumentNumbering("AMS.temp_SB_Master", "SB_Invoice", "TSB",
                _holdSalesSchemaNumber);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("TSB", "AMS.temp_SB_Master", "SB_Invoice");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVNo.Text = wnd.VNo;
        }
        else if (dt?.Rows.Count is 0)
        {
            MessageBox.Show(@"VOUCHER NUMBER SCHEME NOT FOUND..!!", ObjGlobal.Caption);
            TxtVNo.Focus();
        }
    }

    private void InitialiseDataTable()
    {
        _dtPTerm = _setup.GetBillingTerm();
        _dtPartyInfo = _setup.GetPartyInfo();
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private int RIndex { get; set; }
    private int CIndex { get; set; }

    private int DepartmentId { get; set; }
    private int DoctorId { get; set; }
    private int SubLedgerId { get; set; }
    private int AgentId { get; set; }
    private int UnitId { get; set; }
    private int AltUnitId { get; set; }
    private int CounterId { get; set; }
    private int SGridId { get; set; }

    private int _currencyId;
    private int GetUnitId { get; set; }
    private int GetAltUnitId { get; set; }

    private bool IsRowUpdate { get; set; }
    private bool IsTaxable { get; set; }
    private bool IsIPDBilling { get; set; }

    private long ProductId { get; set; }
    private long LedgerId { get; set; }
    private long PatientId { get; set; }

    private decimal SalesRate { get; set; }
    private decimal AltSalesRate { get; set; }
    private decimal AltQtyConv { get; set; }
    private decimal QtyConv { get; set; }

    private string GetAltUnit { get; set; }
    private decimal GetAltQty { get; set; }
    private decimal TaxRate { get; set; }
    private decimal GetMrp { get; set; }
    private decimal GetQty { get; set; }
    private decimal GetSalesRate { get; set; }
    private decimal PDiscountPercentage { get; set; }
    private decimal PDiscount { get; set; }
    private decimal PNetAmount { get; set; }
    private string _actionTag = "SAVE";
    private string _docDesc;
    private string _holdSalesSchemaNumber;
    private string _returnNumberSchema;
    private string _defaultPrinter = string.Empty;
    private string _invoiceType = "NORMAL";
    private string _selectedInvoice = "";
    private string _tempSalesInvoice;
    private string _tempInvoiceDate;
    private string _tempInvoiceMiti;
    private string _refInvoiceDate;
    private string _refInvoiceMiti;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE", "RETURN"];
    private readonly IHMaster _master = new ClsHMaster();
    private readonly IMasterSetup _setup = new ClsMasterSetup();
    private readonly ISalesEntry _entry = new ClsSalesEntry();
    private readonly ISalesDesign _design = new SalesEntryDesign();
    private IPickList _pickList = new ClsPickList();
    private readonly SalesInvoiceService _invoiceService;
    private readonly IList<SalesInvoiceProductModel> _productModels;
    private DataTable _dtPTerm;
    private DataTable _dtPartyInfo;
    private DataTable _productInfo;

    #endregion --------------- OBJECT ---------------
}