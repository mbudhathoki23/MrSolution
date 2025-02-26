using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.DataEntry.PurchaseMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.WinForm;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.PurchaseMaster;

public partial class FrmPurchaseInvoiceEntry : MrForm
{
    // PURCHASE INVOICE ENTRY

    #region --------------- PURCHASE INVOICE ENTRY ---------------
    public FrmPurchaseInvoiceEntry(bool zoom, string txtZoomVno, bool provisionVoucher = false, string invoiceType = "NORMAL")
    {
        InitializeComponent();

        _salesEntry = new ClsSalesEntry();
        _purchaseInvoice = new PurchaseInvoiceRepository();
        _cashBank = new CashBankVoucherRepository();
        _master = new ClsMasterSetup();
        _entry = new ClsPurchaseEntry();

        _purchaseInvoice.DetailsList = [];
        _dtPartyInfo = _master.GetPartyInfo();
        _dtBillTerm = _master.GetBillingTerm();
        _dtProductTerm = _master.GetBillingTerm();
        _dtBatchInfo = _master.GetProductBatchFormat();

        _isProvision = provisionVoucher;
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _invoiceType = invoiceType;

        _isPTermExits = _master.IsBillingTermExitsOrNot("PB", "P");
        _isBTermExits = _master.IsBillingTermExitsOrNot("PB", "B");

        _design = new PurchaseEntryDesign();
        BindDataToComboBox();
        DesignGridColumnsAsync();
        EnableControl();
        ClearControl();
    }
    private void FrmPurchaseInvoiceEntry_Load(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillInvoiceData(_txtZoomVno);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], Tag);
    }
    private void FrmPurchaseInvoiceEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        _getKeys = e;
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }
        if (BtnNew.Enabled)
        {
            var result = CustomMessageBox.ExitActiveForm();
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
        else
        {
            if (TxtProduct.Enabled)
            {
                ClearProductDetails();
                EnableGridControl();
                DGrid.Focus();
            }
            else
            {
                _actionTag = string.Empty;
                EnableControl();
                ClearControl();
                BtnNew.Focus();
            }
        }
    }
    private void FrmPurchaseInvoiceEntry_Shown(object sender, EventArgs e)
    {
        if (BtnNew.Enabled)
        {
            BtnNew.Focus();
        }
        else
        {
            if (TxtVno.Enabled)
            {
                TxtVno.Focus();
            }
            else
            {
                MskDate.Focus();
            }
        }
    }
    private void GlobalEnter_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        if (TxtVno.IsValueExits())
        {
            MskMiti.Focus();
        }
        else
        {
            TxtVno.Focus();
        }
    }
    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        if (!_isZoom)
        {
            ClearControl();
        }
        EnableControl(true);
        TxtVno.Focus();
    }
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        if (!_isZoom)
        {
            ClearControl();
        }
        EnableControl();
        TxtVno.Focus();
    }
    private void BtnSalesInvoice_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        if (voucherNo.IsValueExits())
        {
            FillSalesInvoiceData(voucherNo);
        }

        if (TxtVno.Enabled)
        {
            TxtVno.Focus();
        }
        else if (MskMiti.Enabled)
        {
            MskMiti.Focus();
        }
        else
        {
            TxtRefVno.Focus();
        }
    }
    private void BtnPrintInvoice_Click(object sender, EventArgs e)
    {
        var frmDp = new FrmDocumentPrint("Crystal", "PB", TxtVno.Text, TxtVno.Text)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }
    private void BtnReturnInvoice_Click(object sender, EventArgs e)
    {
        var dialog = new FrmPurchaseReturnEntry(true, string.Empty);
        dialog.ShowDialog();
    }
    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }
    private void BtnCopy_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PB");
        if (result.IsValueExits())
        {
            FillInvoiceData(result);
        }
        TxtVno.Focus();
    }
    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
        {
            Close();
        }
    }
    private void BtnVno_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PB");
        if (_actionTag != "SAVE")
        {
            ClearControl();
            TxtVno.Text = voucherNo;
            FillInvoiceData(TxtVno.Text);
        }
    }
    private void BtnChallan_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PC", "OPC");
        ClearControl();
        TxtChallan.Text = voucherNo;
        FillChallanData(TxtChallan.Text);
    }
    private void BtnOrder_Click(object sender, EventArgs e)
    {
        TxtOrder.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PO", "OPO");
        FillOrderData(TxtOrder.Text);
    }
    private void BtnVendor_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag, "VENDOR");
        if (description.IsValueExits())
        {
            TxtVendor.Text = description;
            _ledgerId = id;
        }
        SetLedgerInfo(_ledgerId);
    }
    private void BtnCurrency_Click(object sender, EventArgs e)
    {
        var (description, id, rate) = GetMasterList.GetCurrencyList(_actionTag);
        if (description.IsValueExits())
        {
            TxtCurrencyRate.Text = description;
            _currencyId = id;
            TxtCurrencyRate.Text = rate;
        }
        TxtCurrencyRate.Focus();
    }
    private void BtnSubLedger_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetSubLedgerList(_actionTag);
        if (description.IsValueExits())
        {
            TxtSubledger.Text = description;
            _subLedgerId = id;
        }
        TxtSubledger.Focus();
    }
    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        (TxtDepartment.Text, _departmentId) = GetMasterList.GetDepartmentList(_actionTag);
    }
    private void BtnAgent_Click(object sender, EventArgs e)
    {
        (TxtAgent.Text, _agentId) = GetMasterList.GetAgentList(_actionTag);
    }
    private void BtnBillingTerm_Click(object sender, EventArgs e)
    {
        if (!TxtBillTermAmount.Enabled || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            return;
        }
        var result = new FrmTermCalculation(_dtBillTerm.RowsCount() > 0 ? "UPDATE" : "SAVE", LblTotalBasicAmount.Text, false, "PB", 0, 0, _dtBillTerm, LblTotalQty.Text, _totalTaxable.GetDecimalString(), CmbInvoiceType.Text);
        result.ShowDialog();
        TxtBillTermAmount.Text = result.TotalTermAmount;
        AddToBillingTerm(result.CalcTermTable);
        if (TxtRemarks.Enabled)
        {
            TxtRemarks.Focus();
        }
        else
        {
            BtnSave.Focus();
        }
    }
    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (!IsValidInformation())
        {
            return;
        }
        if (SavePurchaseInvoice() == 0)
        {
            this.NotifyError($@"ERROR OCCURS ON PURCHASE INVOICE {_actionTag}..!!");
            MskMiti.Focus();
        }
        if (CmbInvoiceType.Text.GetUpper().Equals("IMPORT"))
        {
            var result = new FrmPurchaseAdditional(true, TxtVno.Text, "INVOICE");
            result.ShowDialog();
        }
        PrintVoucher();
        if (CmbPaymentMode.Text.GetUpper() is "CASH" or "BANK")
        {
            if (_actionTag != "SAVE")
            {
                var dtCheck = TxtVno.IsDuplicate("Voucher_No", string.Empty, _actionTag, "CB");
                if (dtCheck)
                {
                    SaveCashAndBankDetails();
                }
            }
            else
            {
                SaveCashAndBankDetails();
            }
        }
        if (_isZoom)
        {
            Close();
            return;
        }
        this.NotifySuccess($@"{TxtVno.Text} PURCHASE INVOICE NUMBER {_actionTag} SUCCESSFULLY..!!");
        ClearControl();
        if (_actionTag != "SAVE")
        {
            TxtVno.Enabled = true;
            TxtVno.Focus();
        }
        else if (TxtVno.IsValueExits())
        {
            MskMiti.Focus();
        }
        else
        {
            TxtVno.Focus();
        }
    }
    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
    }
    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnVno_Click(sender, e);
        else if (e.KeyCode is Keys.Enter)
            GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        else if (TxtVno.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
        }
    }
    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (TxtVno.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            this.NotifyValidationError(TxtVno, "INVOICE VOUCHER NUMBER IS BLANK..!!");
        }
        if (_actionTag != "SAVE")
        {
            return;
        }
        var dtVoucher = _entry.CheckVoucherExitsOrNot("PB_Master", "PB_Invoice", TxtVno.Text);
        if (dtVoucher.RowsCount() > 0 && _actionTag.Equals("SAVE"))
        {
            this.NotifyValidationError(TxtVno, "INVOICE NUMBER ALREADY EXITS..!!");
        }
        else if (dtVoucher.Rows.Count <= 0 && !_actionTag.Equals("SAVE"))
        {
            this.NotifyValidationError(TxtVno, "INVOICE NUMBER NOT EXITS..!!");
        }
    }
    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && MskMiti.Enabled && MskMiti.MaskCompleted)
        {
            if (MskMiti.MaskCompleted && !MskMiti.IsDateExits("M") || !MskMiti.MaskCompleted && MskMiti.Enabled && TxtVno.IsValueExits())
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
                MskRefDate.Text = MskMiti.Text;
            }
        }

        if (_actionTag.IsValueExits() && !MskMiti.MaskCompleted)
        {
            if (MskMiti.ValidControl(ActiveControl))
            {
                MskMiti.WarningMessage("ENTER INVOICE MITI IS INVALID..!!");
            }
        }
    }
    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag.IsValueExits() && MskDate.Enabled && MskDate.MaskCompleted)
        {
            if (MskDate.MaskCompleted && !MskDate.IsDateExits("D"))
            {
                MskDate.WarningMessage("VOUCHER DATE IS INVALID..!!");
                return;
            }

            if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
            {
                MskDate.WarningMessage(
                    $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
                return;
            }

            if (MskDate.MaskCompleted)
            {
                MskMiti.GetNepaliDate(MskDate.Text);
            }
        }
        if (_actionTag.IsValueExits() && !MskDate.MaskCompleted)
        {
            if (MskDate.ValidControl(ActiveControl))
            {
                MskDate.WarningMessage("ENTER INVOICE DATE IS INVALID..!!");
            }
        }
    }
    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtRefVno.IsValueExits())
            {
                SendKeys.Send("{TAB}");
            }
            else if (TxtChallan.Enabled)
            {
                TxtChallan.Focus();
            }
            else if (TxtOrder.Enabled)
            {
                TxtOrder.Focus();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
    }
    private void CmbInvType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
    private void CmbBillIn_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Space)
        {
            SendKeys.Send("{F4}");
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }
    private void TxtVendor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVendor_Click(sender, e);
        }
        else if (e.KeyCode is Keys.F2)
        {
            ledgerType = _master.GetLedgerTypeDescription(_ledgerId);
            if (_ledgerType.Contains(ledgerType))
            {
                CashAndBankValidation(ledgerType.Substring(0, 1));
            }
        }
        else if (e.KeyData is Keys.Enter)
        {
            if (TxtVendor.IsBlankOrEmpty())
            {
                BtnVendor.PerformClick();
            }
            else
            {
                GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtVendor.Text, _ledgerId) = GetMasterList.CreateGeneralLedger("VENDOR", true);
            SetLedgerInfo(_ledgerId);
            TxtVendor.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVendor, BtnVendor);
        }
    }
    private void TxtVendor_Validating(object sender, CancelEventArgs e)
    {
        if (_ledgerId > 0 && _actionTag.IsValueExits() && TxtVendor.Enabled)
        {
            if (TxtRefVno.IsValueExits())
            {
                var dtVoucherNo = _purchaseInvoice.CheckRefVoucherNo(_actionTag, _ledgerId, TxtRefVno.Text, TxtVno.Text);
                if (dtVoucherNo.RowsCount() is 0)
                {
                    return;
                }
                this.NotifyValidationError(TxtRefVno, "THIS INVOICE NUMBER IS ALREADY ENTER IN THE LEDGER..!!");
            }
        }
        if (TxtVendor.IsBlankOrEmpty() || _ledgerId is 0)
        {
            if (_actionTag.IsValueExits() && TxtVendor.Enabled && TxtVendor.ValidControl(ActiveControl))
            {
                TxtVendor.WarningMessage("VENDOR IS REQUIRED FOR INVOICE..!!");
                return;
            }
        }
    }
    private void TxtDueDays_TextChanged(object sender, EventArgs e)
    {
        MskDueDays.Text = MskDate.GetDateTime().AddDays(TxtDueDays.GetInt()).GetDateString();
    }

    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCurrency_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtCurrency.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.CreateCurrency(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtCurrency.IsBlankOrEmpty() && ObjGlobal.PurchaseCurrencyMandatory)
                this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY PLEASE SELECT CURRENCY");
            else
                GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCurrency, BtnCurrency);
        }
    }

    private void TxtCurrencyRate_TextChanged(object sender, EventArgs e)
    {
        if (_ledgerId is 0)
        {
            return;
        }
        VoucherTotalCalculation();
    }

    private void TxtCurrencyRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtSubledger.Enabled)
            {
                TxtSubledger.Focus();
            }
            else if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else if (TxtAgent.Enabled)
            {
                TxtAgent.Focus();
            }
            else
            {
                DGrid.Focus();
            }
        }

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void TxtSubLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSubLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtSubledger.Text, _subLedgerId) = GetMasterList.CreateSubLedger(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtSubledger.IsBlankOrEmpty() && ObjGlobal.PurchaseSubLedgerMandatory)
            {
                this.NotifyValidationError(TxtSubledger, "SUB LEDGER IS MANDATORY PLEASE SELECT SUBLEDGER");
            }
            else
            {
                if (TxtDepartment.Enabled)
                {
                    TxtDepartment.Focus();
                }
                else if (TxtAgent.Enabled)
                {
                    TxtAgent.Focus();
                }
                else
                {
                    DGrid.Focus();
                }
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubledger, BtnSubledger);
        }
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnDepartment_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtDepartment.Text, _departmentId) = GetMasterList.CreateDepartment(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.PurchaseDepartmentMandatory)
            {
                this.NotifyValidationError(TxtDepartment, "DEPARTMENT IS MANDATORY PLEASE SELECT DEPARTMENT");
            }
            else
            {
                if (TxtAgent.Enabled)
                {
                    TxtAgent.Focus();
                }
                else
                {
                    DGrid.Focus();
                }
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDepartment, BtnDepartment);
        }
    }

    private void TxtAgent_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnAgent_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtAgent.Text, _agentId) = GetMasterList.CreateAgent(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.PurchaseAgentMandatory)
                this.NotifyValidationError(TxtDepartment, "AGENT IS MANDATORY PLEASE SELECT AGENT");
            else
            {
                DGrid.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAgent, BtnAgent);
        }
    }

    private void TxtBillTermAmount_Enter(object sender, EventArgs e)
    {
        if (TxtBillTermAmount.Focused && TxtBillTermAmount.Enabled || ActiveControl == TxtBillTermAmount && TxtBillTermAmount.Focused)
        {
            BtnBillingTerm_Click(BtnBillingTerm, EventArgs.Empty);
        }
    }

    private void TxtBillTermAmount_TextChanged(object sender, EventArgs e)
    {
        LblTotalNetAmount.Text = (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal(true)).GetDecimalString();
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalNetAmount.Text);
    }

    private void TxtBillTermAmount_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtBillTermAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            BtnBillingTerm_Click(BtnBillingTerm, EventArgs.Empty);
            if (TxtRemarks.Enabled)
            {
                TxtRemarks.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void TxtDueDays_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtCurrency.Enabled)
            {
                SendKeys.Send("{TAB}");
            }
            else if (TxtSubledger.Enabled)
            {
                TxtSubledger.Focus();
            }
            else if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else if (TxtAgent.Enabled)
            {
                TxtAgent.Focus();
            }
            else
            {
                DGrid.Focus();
            }
        }
        e.Handled = e.IsDecimal(sender);
    }

    private void TxtChallan_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnChallanNo.PerformClick();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtChallan, BtnChallanNo);
        }
    }

    private void TxtOrder_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOrderNo.PerformClick();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOrder, BtnOrderNo);
        }
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
    {
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var filename = dlg.FileName;
            var isFileExists = dlg.FileName;
            PAttachment1.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment1.Image = bitmap;
            PAttachment1.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment1.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment1);
    }

    private void BtnAttachment2_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var filename = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment2.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment2.Image = bitmap;
            PAttachment2.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment2.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
            }
        }
    }

    private void LinkAttachment2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment2);
    }

    private void BtnAttachment3_Click(object sender, EventArgs e)
    {
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var filename = dlg.FileName;
            PAttachment3.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment3.Image = bitmap;
            PAttachment3.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment3.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment3);
    }

    private void BtnAttachment4_Click(object sender, EventArgs e)
    {
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var filename = dlg.FileName;
            PAttachment4.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment4.Image = bitmap;
            PAttachment4.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment4.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment4);
    }

    private void BtnAttachment5_Click(object sender, EventArgs e)
    {
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            var filename = dlg.FileName;
            PAttachment5.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment5.Image = bitmap;
            PAttachment5.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment5.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment5);
    }

    private void TxtNetAmount_TextChanged(object sender, EventArgs e)
    {
        VoucherTotalCalculation();
    }

    private void OnTxtTermAmountOnValidating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtBasicAmount)
        {
            return;
        }

        if (!TxtProduct.Enabled || !TxtProduct.IsValueExits())
        {
            return;
        }
        AddTextToGrid(_isRowUpdate);
        if (ObjGlobal.StockShortNameWise)
        {
            TxtShortName.Focus();
        }
        else
        {
            TxtProduct.Focus();
        }
    }

    private void OnTxtTermAmountOnTextChanged(object sender, EventArgs e)
    {
        var basicAmount = TxtBasicAmount.GetDecimal();
        var termAmount = TxtTermAmount.GetDecimal();
        TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
    }

    private void OnTxtTermAmountOnEnter(object _, EventArgs e)
    {
        if (!TxtProduct.IsValueExits() || !TxtProduct.Enabled)
        {
            return;
        }
        var existingTerm = new DataTable();
        var serialNo = 0;

        if (DGrid.CurrentRow != null)
        {
            serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
            var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString().Trim()));
            if (exDetails)
            {
                var exitAny = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.GetString()));
                if (exitAny)
                {
                    existingTerm = _dtProductTerm.Select($"ProductId = '{_productId}'").CopyToDataTable();
                    if (existingTerm.Rows.Count is 0)
                    {
                        existingTerm = _dtProductTerm.Select($" ProductId = '{_productId}'").CopyToDataTable();
                    }
                    else if (existingTerm.Rows.Count > 1)
                    {
                        var any = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.GetString()));
                        if (any)
                        {
                            existingTerm = _dtProductTerm.Select($"ProductSno = '{serialNo}' and ProductId = '{_productId}'").CopyToDataTable();
                        }
                    }
                }
            }
        }

        var tag = _actionTag == "SAVE" && !_isRowUpdate ? "SAVE" : "UPDATE";
        var result = new FrmTermCalculation(tag, TxtBasicAmount.Text, true, "PB", _productId, serialNo, existingTerm, TxtQty.Text);
        result.ShowDialog();
        TxtTermAmount.Text = result.TotalTermAmount;
        var basicAmount = TxtBasicAmount.GetDecimal();
        var termAmount = TxtTermAmount.GetDecimal();
        TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
        AddToProductTerm(result.CalcTermTable);
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() + TxtTermAmount.GetDecimal()).GetDecimalString();
        TxtNetAmount.Focus();
    }

    private void TxtBasicAmount_TextChanged(object sender, EventArgs e)
    {
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void TxtBasicAmount_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtRate || TxtTermAmount.Enabled)
        {
            return;
        }
        if (TxtProduct.Enabled && TxtProduct.IsValueExits())
        {
            TxtBasicAmount.Text = TxtBasicAmount.GetDecimalString();
            AddTextToGrid(_isRowUpdate);
            if (ObjGlobal.StockShortNameWise)
            {
                TxtShortName.Focus();
            }
            else
            {
                TxtProduct.Focus();
            }
        }
    }

    private void TxtBasicAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
    }

    private void TxtRate_Validating(object sender, CancelEventArgs e)
    {
        TxtRate.Text = TxtRate.GetRateDecimalString();
        //TxtBasicAmount.Text = (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() + TxtTermAmount.GetDecimal()).GetDecimalString();
        if (e == null || ActiveControl == TxtQty || TxtTermAmount.Visible || TxtBasicAmount.Enabled)
        {
            return;
        }
        if (!TxtProduct.Enabled || !TxtProduct.IsValueExits())
        {
            return;
        }
        AddTextToGrid(_isRowUpdate);
        if (ObjGlobal.StockShortNameWise)
        {
            TxtShortName.Focus();
        }
        else
        {
            TxtProduct.Focus();
        }
    }

    private void TxtRate_TextChanged(object sender, EventArgs e)
    {
        if (!TxtRate.Focused || ActiveControl != TxtRate)
        {
            return;
        }
        TxtBasicAmount.Text = (TxtRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void TxtRate_KeyDown(object sender, KeyEventArgs e)
    {
        if (!e.Shift || e.KeyCode is not Keys.Tab)
        {
            return;
        }

        SendToBack();
        TxtRate_Validating(this, null);
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
        if (ActiveControl == TxtAltQty)
        {
            return;
        }
        var con = _conAltQty > 0 ?  _conQty / _conAltQty : 0;
        TxtAltQty.Text = con switch
        {
            > 0 when TxtQty.GetDecimal() > 0 => (TxtQty.GetDecimal() / con).GetDecimalQtyString(),
            _ => 0.GetDecimalQtyString()
        };
        TxtBasicAmount.Text = (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void TxtQty_Validated(object sender, EventArgs e)
    {
        TxtQty.Text = TxtQty.GetDecimalQtyString();
        if (!TxtQty.Enabled || !TxtProduct.Enabled)
        {
            return;
        }
        if (TxtQty.IsBlankOrEmpty())
        {
            TxtQty.WarningMessage("PURCHASE QUANTITY IS REQUIRED..!!");
            return;
        }
        TxtQty_TextChanged(sender, e);
    }

    private void TxtAltQty_TextChanged(object sender, EventArgs e)
    {
        if (ActiveControl == TxtQty)
        {
            return;
        }
        var con = _conAltQty > 0 ?  _conQty / _conAltQty : 0;
        TxtQty.Text = _conQty switch
        {
            > 0 when TxtAltQty.GetDecimal() > 0 => (TxtAltQty.GetDecimal() * con).GetDecimalQtyString(),
            _ => 0.GetDecimalQtyString()
        };

    }

    private void TxtAltQty_Validating(object sender, CancelEventArgs e)
    {
        if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0)
        {
            TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
        }
    }

    private void OnTxtGodownOnKeyDown(object _, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenGodownList();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateGodown(true);
            if (id > 0)
            {
                TxtGodown.Text = description;
                _godownId = id;
            }
            TxtGodown.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtGodown, OpenGodownList);
        }
    }

    private void OnTxtProductOnValidating(object _, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0)
        {
            CustomMessageBox.Warning("PLEASE SELECT PRODUCT LEDGER FOR OPENING");
            TxtProduct.Focus();
            return;
        }

        if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
            {
                if (_getKeys.KeyChar is (char)Keys.Enter)
                {
                    _getKeys.Handled = false;
                }
                TxtProduct.WarningMessage("PLEASE ENTER INVOICE PRODUCT DETAILS..!!");
                return;
            }
        }

        if (DGrid.RowCount >= 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
        {
            DGrid.ClearSelection();
            if (_getKeys is { KeyChar: (char)Keys.Escape })
            {
                _getKeys = null;
                return;
            }
            if (TxtBillTermAmount.Enabled)
            {
                TxtBillTermAmount.Focus();
            }
            else if (TxtRemarks.Enabled)
            {
                TxtRemarks.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
            EnableGridControl();
        }
    }

    private void OnTxtProductOnKeyDown(object _, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenProductList();
        }
        else if (e.KeyCode is Keys.F2)
        {
            var (description, id) = GetMasterList.GetCounterProduct(TxtShortName.Text);
            if (id > 0)
            {
                TxtProduct.Text = description;
                _productId = id;
                SetProductInfo(_productId);
            }
            TxtProduct.Focus();
        }
        else if (e.KeyCode is Keys.F3)
        {
            if (_productId > 0)
            {
                if (_isBatch)
                {
                    CallProductBatch();
                }
            }
        }
        else if (e.KeyCode is Keys.F6)
        {
        }
        else if (e.KeyCode is Keys.F7)
        {
            if (_ledgerId > 0)
            {
            }
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateProduct(true);
            if (id > 0)
            {
                TxtProduct.Text = description;
                _productId = id;
                SetProductInfo(_productId);
            }
            TxtProduct.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtProduct, OpenProductList);
        }
    }

    private void OnTxtShortNameOnKeyDown(object _, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenProductList();
        }
        else if (e.KeyCode is Keys.F2)
        {
            var (description, id) = GetMasterList.GetCounterProduct(TxtShortName.Text);
            if (id > 0)
            {
                TxtProduct.Text = description;
                _productId = id;
                SetProductInfo(_productId);
            }
            TxtProduct.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateProduct(true);
            if (id > 0)
            {
                TxtProduct.Text = description;
                _productId = id;
                SetProductInfo(_productId);
            }
            TxtShortName.Focus();
        }
    }

    private void TxtShortName_Validating(object sender, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0)
        {
            CustomMessageBox.Warning("PLEASE SELECT PRODUCT LEDGER FOR OPENING");
            TxtShortName.Focus();
            return;
        }

        if (TxtShortName.IsValueExits() && TxtShortName.Enabled)
        {
            var dtProduct = _master.GetProductInfoWithBarcode(TxtShortName.Text);
            dtProduct = dtProduct.Rows.Count is 0 ? _master.GetProductWithBarcode(TxtShortName.Text) : dtProduct;
            if (dtProduct.Rows.Count == 1)
            {
                _productId = dtProduct.Rows[0]["SelectedId"].GetLong();
                SetProductInfo(_productId);
            }
            else if (dtProduct.Rows.Count > 1)
            {
                var frmPickList = new FrmAutoPopList(dtProduct);
                frmPickList.ShowDialog();
                if (frmPickList.SelectedList.Count > 0)
                {
                    _productId = frmPickList.SelectedList[0]["SelectedId"].GetLong();
                }

                SetProductInfo(_productId);
            }
        }
        if (DGrid.RowCount == 1 && TxtShortName.Enabled && TxtShortName.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
            {
                if (_getKeys.KeyChar is (char)Keys.Enter) _getKeys.Handled = false;
                this.NotifyValidationError(TxtShortName, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                return;
            }
        }

        if (DGrid.RowCount >= 1 && TxtShortName.Enabled && TxtShortName.IsBlankOrEmpty())
        {
            EnableGridControl();
            DGrid.ClearSelection();
            if (_getKeys is { KeyChar: (char)Keys.Escape })
            {
                _getKeys = null;
                return;
            }
            if (TxtBillTermAmount.Enabled)
            {
                TxtBillTermAmount.Focus();
            }
            else if (TxtRemarks.Enabled)
            {
                TxtRemarks.Focus();
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void OnDGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void OnDGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void OnDGridOnGotFocus(object sender, EventArgs e)
    {
        DGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (DGrid.CurrentRow != null)
            {
                var sno = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
                var productId = DGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();

                if (productId is 0)
                {
                    return;
                }
                if (CustomMessageBox.DeleteRow() is DialogResult.No)
                {
                    return;
                }
                _rowDelete = true;
                DeletedRowExitsOrNot(sno, productId);
            }

            if (DGrid.CurrentRow != null)
            {
                DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
            }
            if (DGrid.RowCount is 0)
            {
                DGrid.Rows.Add();
            }
            VoucherTotalCalculation();
            GetSerialNo();
        }

        if (e.KeyCode is Keys.Enter && !TxtProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            DGrid.CurrentCell = DGrid.CurrentRow?.Cells[0];
            AdjustControlsInDataGrid();
            if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
            {
                TextFromGrid();
                if (ObjGlobal.StockShortNameWise)
                {
                    TxtShortName.Focus();
                }
                else
                {
                    TxtProduct.Focus();
                }
                return;
            }
            GetSerialNo();
            if (ObjGlobal.StockShortNameWise)
            {
                TxtShortName.Focus();
            }
            else
            {
                TxtProduct.Focus();
            }
        }
    }

    #endregion --------------- PURCHASE INVOICE ENTRY ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private int SaveCashAndBankDetails()
    {
        try
        {
            if (_ledgerType.Contains(ledgerType))
            {
                return 0;
            }

            const int syncRow = 0;
            _cashBank.CbMaster.VoucherMode = "PB";
            _cashBank.CbMaster.Voucher_No = TxtVno.Text;
            _cashBank.CbMaster.Voucher_Date = MskDate.Text.GetDateTime();
            _cashBank.CbMaster.Voucher_Miti = MskMiti.Text;
            _cashBank.CbMaster.Voucher_Time = DateTime.Now;
            _cashBank.CbMaster.Ref_VNo = TxtRefVno.Text;
            _cashBank.CbMaster.Ref_VDate = MskRefDate.GetEnglishDate(MskRefDate.Text).GetDateTime();
            _cashBank.CbMaster.VoucherType = "ALL";
            _cashBank.CbMaster.Ledger_Id = CmbPaymentMode.Text.GetUpper() switch
            {
                "BANK" => ObjGlobal.FinanceBankLedgerId,
                _ => ObjGlobal.FinanceCashLedgerId,
            };
            _cashBank.CbMaster.CheqNo = string.Empty;
            _cashBank.CbMaster.CheqDate = DateTime.Now;
            _cashBank.CbMaster.CheqMiti = string.Empty;
            _cashBank.CbMaster.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _cashBank.CbMaster.Currency_Rate = TxtCurrencyRate.GetDecimal(true);
            _cashBank.CbMaster.Cls1 = _departmentId;
            _cashBank.CbMaster.Cls2 = 0;
            _cashBank.CbMaster.Cls3 = 0;
            _cashBank.CbMaster.Cls4 = 0;
            _cashBank.CbMaster.Remarks = TxtRemarks.Text;
            _cashBank.CbMaster.SyncRowVersion = _entry.ReturnSyncRowVersionVoucher("CB", TxtVno.Text);

            var list = new CB_Details
            {
                Ledger_Id = _ledgerId,
                Voucher_No = TxtVno.Text,
                SNo = 1,
                CBLedgerId = ObjGlobal.FinanceCashLedgerId,
                Subledger_Id = _subLedgerId,
                Agent_Id = _agentId,
                Cls1 = _departmentId,
                Cls2 = 0,
                Cls3 = 0,
                Cls4 = 0,
                CurrencyId = _currencyId,
                CurrencyRate = TxtCurrencyRate.GetDecimal(true),
                Debit = LblTotalNetAmount.GetDecimal(),
                Credit = 0,
                LocalDebit = LblTotalLocalNetAmount.GetDecimal(),
                LocalCredit = 0,
                Tbl_Amount = 0,
                V_Amount = 0,
                Narration = $"BEING VOUCHER SETTLEMENT AGAINS PURCHASE INVOICE [{TxtVno.Text}]",
                Party_No = string.Empty,
                Invoice_Date = DateTime.Now,
                Invoice_Miti = string.Empty,
                VatLedger_Id = 0,
                PanNo = 0,
                Vat_Reg = false,
                SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                SyncOriginId = ObjGlobal.IsOnlineSync ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                SyncCreatedOn = DateTime.Now,
                SyncLastPatchedOn = DateTime.Now,
                SyncRowVersion = _cashBank.CbMaster.SyncRowVersion
            };
            _cashBank.CbDetailsList.Add(list);

            return _cashBank.SaveCashBankVoucher(_actionTag);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    private int SavePurchaseInvoice()
    {
        try
        {
            if (_actionTag is "SAVE" && TxtVno.IsDuplicate("PB", _actionTag))
            {
                TxtVno.Text = TxtVno.GetCurrentVoucherNo("PB", _numberScheme);
            }

            _purchaseInvoice.PbMaster.PB_Invoice = TxtVno.Text;
            _purchaseInvoice.PbMaster.Invoice_Date = DateTime.Parse(MskDate.Text);
            _purchaseInvoice.PbMaster.Invoice_Miti = MskMiti.Text;
            _purchaseInvoice.PbMaster.PB_Vno = TxtRefVno.Text;
            _purchaseInvoice.PbMaster.Vno_Miti = MskRefDate.Text;
            _purchaseInvoice.PbMaster.Vno_Date = MskRefDate.GetEnglishDate(MskRefDate.Text).GetDateTime();
            _purchaseInvoice.PbMaster.Invoice_Time = DateTime.Now;
            _purchaseInvoice.PbMaster.Vendor_ID = _ledgerId;

            _purchaseInvoice.PbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
            _purchaseInvoice.PbMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
            _purchaseInvoice.PbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
            _purchaseInvoice.PbMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
            _purchaseInvoice.PbMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
            _purchaseInvoice.PbMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
            _purchaseInvoice.PbMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString()) ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;

            _purchaseInvoice.PbMaster.Invoice_Type = CmbInvoiceType.Text;
            _purchaseInvoice.PbMaster.Invoice_In = CmbPaymentMode.Text;
            _purchaseInvoice.PbMaster.DueDays = TxtDueDays.Text.GetInt();
            _purchaseInvoice.PbMaster.DueDate = MskDueDays.MaskCompleted ? MskDueDays.GetEnglishDate().GetDateTime() : DateTime.Now;
            _purchaseInvoice.PbMaster.Agent_Id = _agentId;
            _purchaseInvoice.PbMaster.Subledger_Id = _subLedgerId;
            _purchaseInvoice.PbMaster.PO_Invoice = TxtOrder.Text;
            _purchaseInvoice.PbMaster.PO_Date = MskOrderDate.GetDateTime();
            _purchaseInvoice.PbMaster.PC_Invoice = TxtChallan.Text;
            _purchaseInvoice.PbMaster.PC_Date = MskChallanDate.GetDateTime();
            _purchaseInvoice.PbMaster.Cls1 = _departmentId;
            _purchaseInvoice.PbMaster.Cls2 = 0;
            _purchaseInvoice.PbMaster.Cls3 = 0;
            _purchaseInvoice.PbMaster.Cls4 = 0;
            _purchaseInvoice.PbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _purchaseInvoice.PbMaster.Cur_Rate = TxtCurrencyRate.Text.GetDecimal() > 0 ? TxtCurrencyRate.Text.GetDecimal() : 1;
            _purchaseInvoice.PbMaster.Counter_ID = 0;
            _purchaseInvoice.PbMaster.B_Amount = LblTotalBasicAmount.Text.GetDecimal();
            _purchaseInvoice.PbMaster.T_Amount = TxtBillTermAmount.Text.GetDecimal();
            _purchaseInvoice.PbMaster.N_Amount = LblTotalNetAmount.Text.GetDecimal();
            _purchaseInvoice.PbMaster.LN_Amount = LblTotalLocalNetAmount.Text.GetDecimal();
            _purchaseInvoice.PbMaster.Tender_Amount = 0;
            _purchaseInvoice.PbMaster.Change_Amount = 0;
            _purchaseInvoice.PbMaster.V_Amount = 0;
            _purchaseInvoice.PbMaster.Tbl_Amount = 0;
            _purchaseInvoice.PbMaster.Action_type = _actionTag;

            _purchaseInvoice.PbMaster.R_Invoice = false;
            _purchaseInvoice.PbMaster.No_Print = 0;
            _purchaseInvoice.PbMaster.In_Words = LblNumberInWords.Text;
            _purchaseInvoice.PbMaster.Remarks = TxtRemarks.Text;

            _purchaseInvoice.PbMaster.Audit_Lock = false;

            _purchaseInvoice.PbMaster.SyncBaseId = Guid.NewGuid();
            _purchaseInvoice.PbMaster.SyncGlobalId = ObjGlobal.SyncOrginIdSync.GetGuid();
            _purchaseInvoice.PbMaster.SyncOriginId = ObjGlobal.SyncBaseSync.GetGuid();
            _purchaseInvoice.PbMaster.SyncLastPatchedOn = DateTime.Now;
            _purchaseInvoice.PbMaster.SyncCreatedOn = DateTime.Now;

            var sync = _entry.ReturnSyncRowVersionVoucher("PB", TxtVno.Text);
            _purchaseInvoice.PbMaster.SyncRowVersion = sync;

            _purchaseInvoice.PbMaster.GetView = DGrid;

            if (DGrid.RowCount <= 0)
            {
                return 0;
            }

            // PURCHASE INVOICE DETAILS
            _purchaseInvoice.DetailsList?.Clear();
            foreach (DataGridViewRow viewRow in DGrid.Rows)
            {
                var list = new PB_Details();
                var detailsProduct = viewRow.Cells["GTxtProductId"].Value.GetLong();
                if (detailsProduct is 0)
                {
                    continue;
                }
                list.PB_Invoice = TxtVno.Text;
                list.Invoice_SNo = viewRow.Cells["GTxtSno"].Value.GetInt();
                list.P_Id = viewRow.Cells["GTxtProductId"].Value.GetLong();
                list.Gdn_Id = viewRow.Cells["GTxtGodownId"].Value.GetInt();
                list.Alt_Qty = viewRow.Cells["GTxtAltQty"].Value.GetDecimal();
                list.Alt_UnitId = viewRow.Cells["GTxtAltUOMId"].Value.GetInt();
                list.Qty = viewRow.Cells["GTxtQty"].Value.GetDecimal();
                list.Unit_Id = viewRow.Cells["GTxtUOMId"].Value.GetInt();
                list.Rate = viewRow.Cells["GTxtRate"].Value.GetDecimal();
                list.B_Amount = viewRow.Cells["GTxtAmount"].Value.GetDecimal();
                list.T_Amount = viewRow.Cells["GTxtTermAmount"].Value.GetDecimal();
                list.N_Amount = viewRow.Cells["GTxtNetAmount"].Value.GetDecimal();
                list.AltStock_Qty = viewRow.Cells["GTxtAltStockQty"].Value.GetDecimal();
                list.Stock_Qty = viewRow.Cells["GTxtStockQty"].Value.GetDecimal();
                list.Narration = viewRow.Cells["GTxtNarration"].Value.GetString();
                list.PO_Invoice = viewRow.Cells["GTxtOrderNo"].Value.GetString();
                list.PO_Sno = viewRow.Cells["GTxtOrderSno"].Value.GetInt();
                list.PC_Invoice = viewRow.Cells["GTxtChallanNo"].Value.GetString();
                list.PC_SNo = viewRow.Cells["GTxtChallanSno"].Value.GetInt();

                list.Tax_Amount = viewRow.Cells["GTxtTaxableAmount"].Value.GetDecimal();
                list.V_Amount = viewRow.Cells["GTxtVatAmount"].Value.GetDecimal();
                list.V_Rate = viewRow.Cells["GTxtTaxPriceRate"].Value.GetDecimal();

                list.Free_Unit_Id = 0;
                list.Free_Qty = 0;
                list.StockFree_Qty = 0;
                list.ExtraFree_Unit_Id = 0;
                list.ExtraFree_Qty = 0;
                list.ExtraStockFree_Qty = 0;
                    
                list.T_Product = viewRow.Cells["IsTaxable"].Value.GetBool();
                list.P_Ledger = viewRow.Cells["GTxtPBLedgerId"].Value.GetLong();
                list.PR_Ledger = viewRow.Cells["GTxtPRLedgerId"].Value.GetLong();

                list.SZ1 = list.SZ2 = list.SZ3 = list.SZ4 = list.SZ5 = list.SZ6 = list.SZ7 = list.SZ8 = list.SZ9 = list.SZ10 = null;

                list.Serial_No = null;
                list.Batch_No = null;
                list.Exp_Date = null;
                list.Manu_Date = null;
                list.TaxExempted_Amount = 0;

                list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                if (ObjGlobal.LocalOriginId != null)
                {
                    list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                        ? ObjGlobal.LocalOriginId.Value
                        : Guid.Empty;
                }

                list.SyncCreatedOn = DateTime.Now;
                list.SyncLastPatchedOn = DateTime.Now;
                list.SyncRowVersion = sync;

                _purchaseInvoice.DetailsList?.Add(list);
            }

            // PURCHASE DETAILS
            _purchaseInvoice.Terms.Clear();
            foreach (DataRow row in _dtProductTerm.Rows)
            {
                var list = new PB_Term()
                {
                    PB_VNo = TxtVno.Text,
                    PT_Id = row["TermId"].GetInt(),
                    SNo = row["ProductSno"].GetInt(),
                    Term_Type = "P",
                    Product_Id = row["ProductId"].GetLong(),
                    Rate = row["TermRate"].GetDecimal(),
                    Amount = row["TermAmt"].GetDecimal(),
                    Taxable = row["TermId"].GetInt() == ObjGlobal.PurchaseVatTermId ? "Y" : "N",
                    SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                    SyncCreatedOn = DateTime.Now,
                    SyncLastPatchedOn = DateTime.Now,
                    SyncRowVersion = sync
                };
                _purchaseInvoice.Terms.Add(list);
            }

            // PURCHASE TERM 
            foreach (DataRow row in _dtBillTerm.Rows)
            {
                var list = new PB_Term
                {
                    PB_VNo = TxtVno.Text,
                    PT_Id = row["TermId"].GetInt(),
                    SNo = row["SNo"].GetInt(),
                    Term_Type = "B",
                    Product_Id = null,
                    Rate = row["TermRate"].GetDecimal(),
                    Amount = row["TermAmt"].GetDecimal(),
                    Taxable = row["TermId"].GetInt() == ObjGlobal.PurchaseVatTermId ? "Y" : "N",
                    SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                    SyncCreatedOn = DateTime.Now,
                    SyncLastPatchedOn = DateTime.Now,
                    SyncRowVersion = sync
                };
                _purchaseInvoice.Terms.Add(list);
            }

            // ADDITIONAL INFORMATION
            _purchaseInvoice.AddInfos.Clear();
            foreach (DataRow row in _dtBatchInfo.Rows)
            {
                var list = new ProductAddInfo
                {
                    Module = "PB",
                    VoucherNo = TxtVno.Text,

                    VoucherType = "I",
                    ProductId = row["ProductId"].GetLong(),
                    Sno = row["SNo"].GetInt(),

                    SizeNo = row["SizeNo"].GetString(),
                    SerialNo = row["SerialNo"].GetString(),
                    BatchNo = row["BatchNo"].GetString(),

                    ChasisNo = row["ChasisNo"].GetString(),
                    EngineNo = row["EngineNo"].GetString(),
                    VHModel = row["VHModel"].GetString(),
                    VHColor = row["VHColor"].GetString(),

                    MFDate = row["MfDate"].GetDateTime(),
                    ExpDate = row["ExpDate"].GetDateTime(),

                    Mrp = row["Mrp"].GetDecimal(),
                    Rate = row["Rate"].GetDecimal(),

                    AltQty = row["AltQty"].GetDecimal(),
                    Qty = row["Qty"].GetDecimal(),

                    BranchId = ObjGlobal.SysBranchId,
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId,
                    EnterBy = ObjGlobal.LogInUser,
                    EnterDate = DateTime.Now,

                    SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty,
                    SyncCreatedOn = DateTime.Now,
                    SyncLastPatchedOn = DateTime.Now,
                    SyncRowVersion = sync
                };
                _purchaseInvoice.AddInfos.Add(list);
            }

            // IMAGE
            _purchaseInvoice.PbMaster.PAttachment1 = PAttachment1.Image.ConvertImage();
            _purchaseInvoice.PbMaster.PAttachment2 = PAttachment2.Image.ConvertImage();
            _purchaseInvoice.PbMaster.PAttachment3 = PAttachment3.Image.ConvertImage();
            _purchaseInvoice.PbMaster.PAttachment4 = PAttachment4.Image.ConvertImage();
            _purchaseInvoice.PbMaster.PAttachment5 = PAttachment5.Image.ConvertImage();

            return _purchaseInvoice.SavePurchaseInvoice(_actionTag);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    private void ReturnVoucherNo()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("PB");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("PB", _numberScheme);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("PB");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _numberScheme = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"PURCHASE INVOICE DETAILS [{_actionTag}]"
            : "PURCHASE INVOICE DETAILS";
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVno.Clear();
        if (_actionTag.Equals("SAVE"))
        {
            TxtVno.GetCurrentVoucherNo("PB", _numberScheme);
        }

        if (BtnNew.Enabled)
        {
            MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.Text = DateTime.Now.GetNepaliDate();
            MskRefDate.Text = ObjGlobal.SysDateType == "M" ? MskRefDate.GetNepaliDate(MskDate.Text) : MskDate.Text;
        }

        _ledgerId = _agentId = _subLedgerId = _departmentId = _departmentId = _currencyId = 0;
        ledgerType = string.Empty;
        TxtChallan.Clear();
        MskChallanDate.Clear();
        MskChallanMiti.Clear();
        TxtOrder.Clear();
        MskOrderDate.Clear();
        MskOrderMiti.Clear();
        TxtRefVno.Clear();
        TxtDueDays.Clear();
        TxtVendor.Clear();
        TxtDepartment.Clear();
        TxtAgent.Clear();
        TxtSubledger.Clear();
        TxtCurrencyRate.Clear();
        TxtVendor.Clear();
        CmbInvoiceType.SelectedIndex = 3;
        CmbPaymentMode.SelectedIndex = 1;
        TxtCurrency.Text = ObjGlobal.SysCurrency;
        TxtCurrencyRate.Text = 1.GetDecimalString(true);
        PAttachment1.Image = null;
        PAttachment2.Image = null;
        PAttachment3.Image = null;
        PAttachment4.Image = null;
        PAttachment5.Image = null;
        _dtProductTerm.Clear();
        _dtPartyInfo.Clear();
        _dtBillTerm.Clear();
        TxtRemarks.Clear();
        LblTotalAltQty.IsClear();
        LblTotalQty.IsClear();
        LblTotalBasicAmount.IsClear();
        LblTotalNetAmount.IsClear();
        LblTotalLocalNetAmount.IsClear();
        TxtBillTermAmount.Clear();
        LblPanNo.IsClear();
        LblBalance.IsClear();
        LblCreditDays.IsClear();
        LblCreditLimit.IsClear();
        TxtTermAmount.Clear();
        if (_dtProductTerm.RowsCount() > 0)
        {
            _dtProductTerm.Rows.Clear();
        }
        if (_dtBillTerm.RowsCount() > 0)
        {
            _dtBillTerm.Rows.Clear();
        }
        if (_dtBatchInfo.RowsCount() > 0)
        {
            _dtBatchInfo.Rows.Clear();
        }
        DGrid.Rows.Clear();
        ClearProductDetails();
        DGrid.ClearSelection();
    }

    private void ClearProductDetails()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            GetSerialNo();
        }

        _isRowUpdate = false;
        _productId = _altUnitId = _godownId = _unitId = 0;
        _conQty = 0;
        _conAltQty = 0;
        TxtProduct.Clear();
        TxtShortName.Clear();
        TxtGodown.Clear();
        TxtAltQty.Enabled = false;
        TxtAltQty.Clear();
        _altUnitId = 0;
        TxtAltUnit.Clear();
        TxtQty.Clear();
        _unitId = 0;
        TxtUnit.Clear();
        TxtRate.Clear();
        TxtBasicAmount.Clear();
        TxtTermAmount.Clear();
        TxtNetAmount.Clear();
        AdjustControlsInDataGrid();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = BtnReverse.Enabled = !isEnable && !_tagStrings.Contains(_actionTag);
        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtChallan.Enabled = BtnChallanNo.Enabled = isEnable && ObjGlobal.PurchaseChallanEnable;
        MskChallanDate.Enabled = MskChallanMiti.Enabled = false;
        TxtOrder.Enabled = BtnOrderNo.Enabled = isEnable && ObjGlobal.PurchaseOrderEnable;
        MskOrderDate.Enabled = MskOrderMiti.Enabled = false;
        CmbPaymentMode.Enabled = CmbInvoiceType.Enabled = isEnable;
        TxtVendor.Enabled = BtnVendor.Enabled = isEnable;
        MskDueDays.Enabled = false;
        TxtDueDays.Enabled = isEnable;
        TxtCurrency.Enabled = BtnCurrency.Enabled = TxtCurrencyRate.Enabled = isEnable && ObjGlobal.PurchaseCurrencyEnable;
        TxtSubledger.Enabled = BtnSubledger.Enabled = isEnable && ObjGlobal.PurchaseSubLedgerEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.PurchaseDepartmentEnable;
        TxtAgent.Enabled = BtnAgent.Enabled = isEnable && ObjGlobal.PurchaseAgentEnable;
        TxtBillTermAmount.Enabled = BtnBillingTerm.Enabled = isEnable && _isBTermExits;
        TabLedgerOpening.Enabled = isEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.PurchaseRemarksEnable || _tagStrings.Contains(_actionTag);
        BtnSave.Enabled = BtnCancel.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        EnableGridControl();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;

        TxtShortName.Enabled = TxtShortName.Visible = isEnable && ObjGlobal.StockShortNameWise;

        TxtProduct.Enabled = TxtProduct.Visible = isEnable;

        TxtGodown.Enabled = TxtGodown.Visible = isEnable && ObjGlobal.PurchaseGodownEnable;

        TxtAltQty.Enabled = isEnable && _conQty > 0;
        TxtAltQty.Visible = isEnable;

        TxtAltUnit.Enabled = false;
        TxtAltUnit.Visible = isEnable;

        TxtQty.Enabled = TxtQty.Visible = isEnable;
        TxtUnit.Enabled = false;
        TxtUnit.Visible = isEnable;

        TxtRate.Enabled = TxtRate.Visible = isEnable;

        TxtBasicAmount.Enabled = isEnable && ObjGlobal.PurchaseBasicAmountEnable;
        TxtBasicAmount.Visible = isEnable;

        TxtTermAmount.Enabled = TxtTermAmount.Visible = isEnable && _isPTermExits;
        TxtNetAmount.Enabled = false;
        TxtNetAmount.Visible = isEnable && _dtProductTerm.Columns.Count > 0;
    }

    private void SetLedgerInfo(long ledgerId)
    {
        if (ledgerId is 0)
        {
            return;
        }
        var dtLedger = _master.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0) return;
        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        var balance = dtLedger.Rows[0]["Balance"].GetDecimal();
        LblBalance.Text = balance.GetDecimalString();
        LblBalanceType.Text = balance < 0 ? "Cr" : balance > 0 ? "Dr" : "";
        LblCreditLimit.Text = dtLedger.Rows[0]["CrLimit"].GetDecimalString();
        LblCreditDays.Text = dtLedger.Rows[0]["CrDays"].GetDecimalString();
        TxtDueDays.Text = LblCreditDays.GetDecimalString();
        MskDueDays.Text = MskDate.GetDateTime().AddDays(TxtDueDays.GetDouble()).GetDateString();
        ledgerType = dtLedger.Rows[0]["GLType"].GetUpper();
        if (_ledgerType.Contains(ledgerType))
        {
            if (_actionTag.IsValueExits())
            {
                CashAndBankValidation(ledgerType.Substring(0, 1));
            }
        }
        else
        {
            _dtPartyInfo.Rows.Clear();
        }
    }

    private void OpenProductList()
    {
        var (description, id) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        if (description.IsValueExits())
        {
            TxtProduct.Text = description;
            _productId = id;
            SetProductInfo(_productId);
        }
        TxtProduct.Focus();
    }

    private void OpenGodownList()
    {
        (TxtGodown.Text, _godownId) = GetMasterList.GetGodown(_actionTag);
        TxtGodown.Focus();
    }

    private void PrintVoucher()
    {
        var dtDesign = _master.GetPrintVoucherList("PB");
        if (dtDesign.Rows.Count <= 0) return;
    }

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        _isRowUpdate = true;
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSno"].Value.GetString();
        TxtProduct.Text = DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.GetString();
        _productId = DGrid.Rows[_rowIndex].Cells["GTxtProductId"].Value.GetLong();
        SetProductInfo(_productId);
        TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.GetString();
        _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
        _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUOMId"].Value.GetInt();
        TxtAltQty.Enabled = _altUnitId > 0;
        TxtAltQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltQty"].Value.GetString();
        TxtAltUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltUOM"].Value.GetString();
        _unitId = DGrid.Rows[_rowIndex].Cells["GTxtUOMId"].Value.GetInt();
        TxtQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtQty"].Value.GetString();
        TxtUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtUOM"].Value.GetString();
        TxtRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtRate"].Value.GetString();
        TxtBasicAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetString();
        TxtTermAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtTermAmount"].Value.GetString();
        TxtNetAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtNetAmount"].Value.GetString();
        _description = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        
    }

    private void SetProductInfo(long productId)
    {
        if (productId is 0)
        {
            return;
        }
        var productList = _master.GetMasterProductList(_actionTag, productId);
        if (productList.Rows.Count <= 0)
        {
            return;
        }
        foreach (DataRow row in productList.Rows)
        {
            if (!_isRowUpdate)
            {
                TxtShortName.Text = row["PShortName"].ToString();

                _altUnitId = row["PAltUnit"].GetInt();
                TxtAltUnit.Text = row["AltUnitCode"].GetString();

                _unitId = row["PUnit"].GetInt();
                TxtUnit.Text = row["UnitCode"].GetString();

                TxtRate.Text = row["PBuyRate"].GetDecimalString();

                _productTaxRate = row["PTax"].GetDecimal();
                _isTaxable = _productTaxRate > 0;
                _isBatch = row["PBatchwise"].GetBool();
            }
            _conQty = productList.Rows[0]["PQtyConv"].GetDecimal();
            _conAltQty = productList.Rows[0]["PAltConv"].GetDecimal();
            TxtAltQty.Enabled = _altUnitId > 0;

            if (!_isRowUpdate)
            {
                var altRate = row["AltSalesRate"].GetDecimal();
                if (altRate > 0)
                {
                    TxtAltQty.Tag = altRate;
                    TxtRate.Text = (altRate / _conQty).GetDecimalString();
                }
            }

            if (_isBatch)
            {
                CallProductBatch();
            }
        }
    }

    private void CallProductBatch()
    {
        var exitRows = _dtBatchInfo.Select($"ProductId = '{_productId}' and ProductSno='{TxtSno.Text}'");
        var result = new FrmProductBatchList(exitRows is { Length: > 0 } ? exitRows.CopyToDataTable() : new DataTable())
        {
            ProductId = _productId,
            ProductSno = TxtSno.GetInt()
        };
        result.ShowDialog();

        if (result.DialogResult == DialogResult.OK)
        {
            TxtQty.Text = result.LblTotalQty.Text;
            TxtAltQty.Text = _conQty > 0 ? (TxtQty.GetDecimal() / _conQty).GetDecimalString() : "";
            ProductBatchInfo(result.ProductInfo);
            TxtQty.Enabled = TxtAltQty.Enabled = false;
            return;
        }
        else
        {
            TxtQty.Enabled = TxtAltQty.Enabled = true;
            if (exitRows is not { Length: > 0 })
            {
                return;
            }
            foreach (var row in exitRows)
            {
                _dtBatchInfo.Rows.Remove(row);
            }
        }
    }

    private void ProductBatchInfo(DataTable dt)
    {
        if (DGrid.CurrentRow != null)
        {
            var serialNo = TxtSno.Text;
            var exDetails = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
            if (exDetails)
            {
                var exitAny = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.ToString().Trim()));
                if (exitAny)
                {
                    foreach (DataRow ro in dt.Rows)
                    {
                        foreach (DataRow row in _dtBatchInfo.Rows)
                        {
                            if (row["ProductSno"].GetInt() == ro["ProductSno"].GetInt() && row["ProductId"].GetLong() == ro["ProductId"].GetLong())
                            {
                                var index = _dtBatchInfo.Rows.IndexOf(row);
                                _dtBatchInfo.Rows[index].SetField("BatchNo", ro["BatchNo"]);
                                _dtBatchInfo.Rows[index].SetField("MfDate", ro["MfDate"]);
                                _dtBatchInfo.Rows[index].SetField("ExpDate", ro["ExpDate"]);
                                _dtBatchInfo.Rows[index].SetField("Qty", ro["Qty"]);
                                _dtBatchInfo.Rows[index].SetField("MRP", ro["MRP"]);
                                _dtBatchInfo.Rows[index].SetField("Rate", ro["Rate"]);
                                _dtBatchInfo.AcceptChanges();
                            }
                        }
                    }
                }
                else
                {
                    foreach (DataRow ro in dt.Rows)
                    {
                        var dataRow = _dtBatchInfo.NewRow();
                        dataRow["SNo"] = ro["SNo"];
                        dataRow["ProductId"] = ro["ProductId"];
                        dataRow["BatchNo"] = ro["BatchNo"];
                        dataRow["MfDate"] = ro["MfDate"];
                        dataRow["ExpDate"] = ro["ExpDate"];
                        dataRow["Qty"] = ro["Qty"];
                        dataRow["MRP"] = ro["MRP"];
                        dataRow["Rate"] = ro["Rate"];
                        dataRow["ProductSno"] = serialNo;
                        _dtBatchInfo.Rows.InsertAt(dataRow, _dtBatchInfo.RowsCount() + 1);
                    }
                }
            }
            else
            {
                foreach (DataRow ro in dt.Rows)
                {
                    var dataRow = _dtBatchInfo.NewRow();
                    dataRow["SNo"] = ro["SNo"];
                    dataRow["ProductId"] = ro["ProductId"];
                    dataRow["BatchNo"] = ro["BatchNo"];
                    dataRow["MfDate"] = ro["MfDate"];
                    dataRow["ExpDate"] = ro["ExpDate"];
                    dataRow["MRP"] = ro["MRP"];
                    dataRow["Qty"] = ro["Qty"];
                    dataRow["ProductSno"] = ro["ProductSno"];
                    _dtBatchInfo.Rows.InsertAt(dataRow, _dtBatchInfo.RowsCount() + 1);
                }
            }
        }
    }

    private void GetSerialNo()
    {
        if (DGrid.RowCount <= 0)
        {
            return;
        }
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProduct"].Value.IsValueExits()) return;

        var viewRows = DGrid.Rows.OfType<DataGridViewRow>();
        var gridViewRows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

        LblTotalAltQty.Text = gridViewRows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal()).GetDecimalString();
        LblTotalQty.Text = gridViewRows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal()).GetDecimalString();
        LblTotalBasicAmount.Text = gridViewRows.Sum(row => row.Cells["GTxtNetAmount"].Value.GetDecimal()).GetDecimalString();

        _totalTaxable = gridViewRows.Sum(row => row.Cells["GTxtTaxableAmount"].Value.GetDecimal());
        _totalVat = gridViewRows.Sum(row => row.Cells["GTxtVatAmount"].Value.GetDecimal());

        TxtBillTermAmount.Text = CalculateBillingTerm();

        LblTotalNetAmount.Text = (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = TxtCurrencyRate.GetDecimal() > 1 ? (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal()).GetDecimalString() : LblTotalNetAmount.Text;
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalNetAmount.Text);
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;
        var currentRow = _rowIndex;

        var columnIndex = DGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtShortName"].Index;
        TxtShortName.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtShortName.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtShortName.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtProduct"].Index;
        TxtProduct.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtGodown"].Index;
        TxtGodown.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtGodown.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtGodown.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltQty"].Index;
        TxtAltQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltUOM"].Index;
        TxtAltUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtQty"].Index;
        TxtQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtUOM"].Index;
        TxtUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRate"].Index;
        TxtRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"].Index;
        TxtBasicAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBasicAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBasicAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtTermAmount"].Index;
        TxtTermAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtTermAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtTermAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtNetAmount"].Index;
        TxtNetAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNetAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNetAmount.TabIndex = columnIndex;
    }

    private void DesignGridColumnsAsync()
    {
        _design.GetPurchaseEntryDesign(DGrid, "PB");
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.PurchaseGodownEnable;
        DGrid.Columns["GTxtTermAmount"].Visible = _isPTermExits;
        DGrid.Columns["GTxtNetAmount"].Visible = _isPTermExits;
        if (DGrid.Columns["GTxtGodown"].Visible)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;
        }

        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
        {
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtShortName"].Width;
        }

        DGrid.GotFocus += OnDGridOnGotFocus;
        DGrid.RowEnter += OnDGridOnRowEnter;
        DGrid.CellEnter += OnDGridOnCellEnter;
        DGrid.KeyDown += DGrid_KeyDown;

        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtShortName = new MrGridTextBox(DGrid)
        {
            ReadOnly = !ObjGlobal.StockShortNameWise
        };
        TxtShortName.KeyDown += OnTxtShortNameOnKeyDown;
        TxtShortName.Validating += TxtShortName_Validating;
        TxtProduct = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtProduct.KeyDown += OnTxtProductOnKeyDown;
        TxtProduct.Validating += OnTxtProductOnValidating;

        TxtGodown = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtGodown.KeyDown += OnTxtGodownOnKeyDown;

        TxtAltQty = new MrGridNumericTextBox(DGrid);
        TxtAltQty.Validating += TxtAltQty_Validating;
        TxtAltQty.TextChanged += TxtAltQty_TextChanged;

        TxtAltUnit = new MrGridTextBox(DGrid);

        TxtQty = new MrGridNumericTextBox(DGrid);

        TxtQty.Validated += TxtQty_Validated;
        TxtQty.TextChanged += TxtQty_TextChanged;
        TxtUnit = new MrGridTextBox(DGrid);

        TxtRate = new MrGridNumericTextBox(DGrid);

        TxtRate.KeyDown += TxtRate_KeyDown;
        TxtRate.TextChanged += TxtRate_TextChanged;
        TxtRate.Validating += TxtRate_Validating;

        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += TxtBasicAmount_KeyDown;
        TxtBasicAmount.Validating += TxtBasicAmount_Validating;
        TxtBasicAmount.TextChanged += TxtBasicAmount_TextChanged;

        TxtTermAmount = new MrGridNumericTextBox(DGrid);
        TxtTermAmount.Enter += OnTxtTermAmountOnEnter;
        TxtTermAmount.TextChanged += OnTxtTermAmountOnTextChanged;
        TxtTermAmount.Validating += OnTxtTermAmountOnValidating;

        TxtNetAmount = new MrGridNumericTextBox(DGrid);
        TxtNetAmount.TextChanged += TxtNetAmount_TextChanged;
        ObjGlobal.DGridColorCombo(DGrid);
        AdjustControlsInDataGrid();
    }

    private void AddToProductTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0)
        {
            return;
        }

        var serialNo = 0;
        if (DGrid.CurrentRow == null)
        {
            return;
        }

        serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
        var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
        if (exDetails)
        {
            var exitAny = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.ToString().Trim()));
            if (exitAny)
            {
                foreach (DataRow row in dtTerm.Rows)
                {
                    var termId = row["GTxtTermId"].GetInt();
                    foreach (DataRow dataRow in _dtProductTerm.Rows)
                    {
                        var resultId = dataRow["TermId"].GetInt();
                        var getProductId = dataRow["ProductId"].GetLong();
                        var productSno = dataRow["ProductSno"].GetInt();
                        if (resultId == termId && getProductId == _productId && serialNo == productSno)
                        {
                            var index = _dtProductTerm.Rows.IndexOf(dataRow);
                            _dtProductTerm.Rows[index].SetField("TermRate", row["GTxtRate"]);
                            _dtProductTerm.Rows[index].SetField("TermAmt", row["GTxtAmount"]);
                            continue;
                        }
                    }
                }
            }
            else
            {
                foreach (DataRow ro in dtTerm.Rows)
                {
                    var dataRow = _dtProductTerm.NewRow();
                    dataRow["OrderNo"] = ro["GTxtOrderNo"];
                    dataRow["SNo"] = ro["GTxtSno"];
                    dataRow["TermId"] = ro["GTxtTermId"];
                    dataRow["TermName"] = ro["GTxtDescription"];
                    dataRow["Basis"] = ro["GTxtBasic"];
                    dataRow["Sign"] = ro["GTxtSign"];
                    dataRow["ProductId"] = ro["GTxtProductId"];
                    dataRow["TermCondition"] = ro["GTxtTermCondition"];
                    dataRow["TermType"] = ro["GTxtTermType"];
                    dataRow["TermRate"] = ro["GTxtRate"];
                    dataRow["TermAmt"] = ro["GTxtAmount"];
                    dataRow["Source"] = "PB";
                    dataRow["Formula"] = string.Empty;
                    dataRow["ProductSno"] = ro["GTxtProductSno"];
                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                }
            }
        }
        else
        {
            foreach (DataRow ro in dtTerm.Rows)
            {
                var dataRow = _dtProductTerm.NewRow();
                dataRow["OrderNo"] = ro["GTxtOrderNo"];
                dataRow["SNo"] = ro["GTxtSno"];
                dataRow["TermId"] = ro["GTxtTermId"];
                dataRow["TermName"] = ro["GTxtDescription"];
                dataRow["Basis"] = ro["GTxtBasic"];
                dataRow["Sign"] = ro["GTxtSign"];
                dataRow["ProductId"] = ro["GTxtProductId"];
                dataRow["TermCondition"] = ro["GTxtTermCondition"];
                dataRow["TermType"] = ro["GTxtTermType"];
                dataRow["TermRate"] = ro["GTxtRate"];
                dataRow["TermAmt"] = ro["GTxtAmount"];
                dataRow["Source"] = "PB";
                dataRow["Formula"] = string.Empty;
                dataRow["ProductSno"] = ro["GTxtProductSno"];
                _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
            }
        }
    }

    private void AddToBillingTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0)
        {
            return;
        }
        _dtBillTerm.Rows.Clear();
        foreach (DataRow ro in dtTerm.Rows)
        {
            var dataRow = _dtBillTerm.NewRow();
            dataRow["OrderNo"] = ro["GTxtOrderNo"];
            dataRow["SNo"] = ro["GTxtSno"];
            dataRow["TermId"] = ro["GTxtTermId"];
            dataRow["TermName"] = ro["GTxtDescription"];
            dataRow["Basis"] = ro["GTxtBasic"];
            dataRow["Sign"] = ro["GTxtSign"];
            dataRow["ProductId"] = ro["GTxtProductId"];
            dataRow["TermType"] = ro["GTxtTermCondition"];
            dataRow["TermRate"] = ro["GTxtRate"];
            dataRow["TermAmt"] = ro["GTxtAmount"];
            dataRow["Source"] = "PB";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtBillTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }

    private void CashAndBankValidation(string type)
    {
        var partyInfo = new FrmPartyInfo(type, _dtPartyInfo, "PB");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0) _dtPartyInfo.Rows.Clear();
        foreach (DataRow row in partyInfo.PartyInfo.Rows)
        {
            var dr = _dtPartyInfo.NewRow();
            dr["PartyLedgerId"] = row["PartyLedgerId"];
            dr["PartyName"] = row["PartyName"];
            dr["ChequeNo"] = row["ChequeNo"];
            dr["ChequeDate"] = row["ChequeDate"];
            dr["VatNo"] = row["VatNo"];
            dr["ContactPerson"] = row["ContactPerson"];
            dr["Address"] = row["Address"];
            dr["City"] = row["City"];
            dr["Mob"] = row["Mob"];
            dr["Email"] = row["Email"];
            _dtPartyInfo.Rows.Add(dr);
        }
    }

    private void FillInvoiceData(string voucherNo)
    {
        try
        {
            var dsPurchase = _entry.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            var dtMaster = dsPurchase.Tables[0];
            var dtDetails = dsPurchase.Tables[1];
            var dtProductTerm = dsPurchase.Tables[2];
            var dtBillTerm = dsPurchase.Tables[3];
            var dtProductBatch = dsPurchase.Tables[4];
            if (dtMaster.Rows.Count <= 0)
            {
                return;
            }
            foreach (DataRow dr in dtMaster.Rows)
            {
                if (_actionTag != "SAVE") TxtVno.Text = dr["PB_Invoice"].ToString();

                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtRefVno.Text = dr["PB_VNo"].ToString();

                if (dr["VNo_Date"].IsValueExits())
                {
                    MskRefDate.Text = dr["Vno_Miti"].ToString();
                }

                if (_actionTag != "SAVE")
                {
                    TxtOrder.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                    {
                        MskOrderDate.Text = dr["PO_Date"].GetDateString();
                        MskOrderMiti.Text = MskOrderMiti.GetNepaliDate(MskOrderDate.Text);
                    }

                    TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                    {
                        MskChallanDate.Text = dr["PC_Date"].GetDateString();
                        MskChallanMiti.Text = MskChallanMiti.GetNepaliDate(MskChallanDate.Text);
                    }
                }

                TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                _ledgerId = dr["Vendor_ID"].GetLong();
                SetLedgerInfo(_ledgerId);

                var invoiceType = dr["Invoice_Type"].GetTrimReplace();
                var invoiceMode = dr["Invoice_In"].GetTrimReplace();

                var index = CmbInvoiceType.FindString(invoiceType);
                CmbInvoiceType.SelectedIndex = index;

                index = CmbPaymentMode.FindStringExact(invoiceMode);
                CmbPaymentMode.SelectedIndex = index;

                TxtDueDays.Text = dr["DueDays"].ToString();
                MskDueDays.Text = dr["DueDate"].GetDateString();

                _dtPartyInfo.Rows.Clear();
                if (_ledgerType.Contains(ledgerType))
                {
                    var drp = _dtPartyInfo.NewRow();
                    drp["PartyLedgerId"] = null;
                    drp["PartyName"] = dr["Party_Name"];
                    drp["ChequeNo"] = dr["ChqNo"];
                    drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                    drp["VatNo"] = dr["Vat_No"];
                    drp["ContactPerson"] = dr["Contact_Person"];
                    drp["Address"] = dr["Address"];
                    drp["Mob"] = dr["Mobile_No"];
                    _dtPartyInfo.Rows.Add(drp);
                }

                _subLedgerId = dr["Subledger_Id"].GetInt();
                TxtSubledger.Text = dr["SlName"].ToString();
                CmbInvoiceType.SelectedItem = CmbInvoiceType.FindString(dr["Invoice_Type"].GetUpper());
                CmbPaymentMode.SelectedIndex = CmbPaymentMode.FindString(dr["Invoice_In"].GetUpper());
                _agentId = dr["Agent_Id"].GetInt();
                TxtAgent.Text = dr["AgentName"].ToString();

                TxtDepartment.Text = dr["DName"].ToString();
                _departmentId = dr["Cls1"].GetInt();
                _currencyId = dr["Cur_Id"].GetInt();
                TxtCurrency.Text = dr["Ccode"].ToString();
                TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());

                PAttachment1.Image = dr["PAttachment1"].GetImage();
                PAttachment2.Image = dr["PAttachment2"].GetImage();
                PAttachment3.Image = dr["PAttachment3"].GetImage();
                PAttachment4.Image = dr["PAttachment4"].GetImage();
                PAttachment5.Image = dr["PAttachment5"].GetImage();
            }

            if (dtDetails.Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dtDetails.Rows.Count + 1);
                foreach (DataRow dr in dtDetails.Rows)
                {
                    DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                        
                    DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                    DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                    DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                        
                    DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                        
                    DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                    DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;

                    var taxableAmount = dr["Tax_Amount"].GetDecimal();
                    var netAmount = dr["N_Amount"].GetDecimal();
                    var taxRate = dr["PTax"].GetDecimal();
                    if (taxableAmount == 0 && taxRate > 0)
                    {
                        taxableAmount = netAmount;
                    }

                    DGrid.Rows[iRows].Cells["GTxtTaxableAmount"].Value = taxableAmount;
                    DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = dr["V_Amount"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = dr["V_Rate"].GetDecimal() > 0 ? dr["V_Rate"].GetDecimal() : taxRate;


                    if (dtProductTerm.Rows.Count > 0)
                    {
                        var serialNo = dr["Invoice_SNo"].GetInt();
                        var productId = dr["P_Id"].GetLong();
                        var exDetails = dtProductTerm.AsEnumerable().Any(c => c.Field<long>("ProductId").Equals(productId));
                        if (exDetails)
                        {
                            var exitAny = dtProductTerm.AsEnumerable().Any(c => c.Field<long>("ProductId").Equals(productId) && c.Field<int>("SNo").Equals(serialNo));
                            if (exitAny)
                            {
                                foreach (var ro in dtProductTerm.Select($"ProductId='{productId}' AND SNo='{serialNo}'"))
                                {
                                    var dataRow = _dtProductTerm.NewRow();
                                    dataRow["OrderNo"] = ro["OrderNo"];
                                    dataRow["SNo"] = dr["Invoice_SNo"];
                                    dataRow["TermId"] = ro["TermId"];
                                    dataRow["TermName"] = ro["TermName"];
                                    dataRow["Basis"] = ro["Basis"];
                                    dataRow["Sign"] = ro["Sign"];
                                    dataRow["ProductId"] = ro["ProductId"];
                                    dataRow["TermType"] = ro["TermType"];
                                    dataRow["TermCondition"] = ro["TermCondition"];
                                    dataRow["TermRate"] = ro["TermRate"];
                                    dataRow["TermAmt"] = ro["TermAmt"];
                                    dataRow["Source"] = ro["Source"];
                                    dataRow["Formula"] = ro["Formula"];
                                    dataRow["ProductSno"] = dr["Invoice_SNo"];
                                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                                }
                            }
                        }
                    }
                    iRows++;
                }
                DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
                DGrid.ClearSelection();
            }
            if (dtBillTerm.Rows.Count > 0)
            {
                var sNo = 1;
                _dtBillTerm.Rows.Clear();
                foreach (DataRow ro in dtBillTerm.Rows)
                {
                    var dataRow = _dtBillTerm.NewRow();
                    dataRow["OrderNo"] = ro["OrderNo"];
                    dataRow["SNo"] = sNo;
                    dataRow["TermId"] = ro["TermId"];
                    dataRow["TermName"] = ro["TermName"];
                    dataRow["Basis"] = ro["Basis"];
                    dataRow["Sign"] = ro["Sign"];
                    dataRow["ProductId"] = ro["ProductId"];
                    dataRow["TermType"] = ro["TermType"];
                    dataRow["TermCondition"] = ro["TermCondition"];
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtBillTerm.Rows.InsertAt(dataRow, _dtBillTerm.RowsCount() + 1);
                    sNo++;
                }

                if (dtProductBatch.Rows.Count > 0)
                {
                    _dtBatchInfo.Rows.Clear();
                    foreach (DataRow row in dtProductBatch.Rows)
                    {
                        var dataRow = _dtBatchInfo.NewRow();
                        dataRow["SNo"] = dtProductBatch.Rows.IndexOf(row) + 1;
                        dataRow["ProductId"] = row["ProductId"];
                        dataRow["BatchNo"] = row["BatchNo"];
                        dataRow["MfDate"] = row["MfDate"].GetDateString();
                        dataRow["ExpDate"] = row["ExpDate"].GetDateString();
                        dataRow["Qty"] = row["Qty"].GetDecimalQtyString();
                        dataRow["MRP"] = row["MRP"].GetDecimalString();
                        dataRow["Rate"] = row["Rate"].GetDecimalString();
                        dataRow["ProductSno"] = row["Sno"];
                        _dtBatchInfo.Rows.InsertAt(dataRow, _dtBatchInfo.Rows.Count + 1);
                    }
                }
            }

            VoucherTotalCalculation();
            DGrid.ClearSelection();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void FillChallanData(string voucherNo)
    {
        try
        {
            var dsChallan = _entry.ReturnPurchaseChallanDetailsInDataSet(voucherNo);
            if (dsChallan.Tables.Count <= 0)
            {
                return;
            }
            var dtMaster = dsChallan.Tables[0];
            var dtDetails = dsChallan.Tables[1];
            var dtProductTerm = dsChallan.Tables[2];
            var dtBillTerm = dsChallan.Tables[3];
            if (dsChallan.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            foreach (DataRow dr in dtMaster.Rows)
            {
                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtRefVno.Text = Convert.ToString(dr["PB_VNo"].ToString());

                if (dr["VNo_Date"].IsValueExits()) MskRefDate.Text = dr["Vno_Miti"].ToString();
                if (_actionTag != "SAVE")
                {
                    TxtOrder.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                    {
                        MskOrderDate.Text = dr["PO_Date"].GetDateString();
                        MskOrderMiti.Text = MskOrderMiti.GetNepaliDate(MskOrderDate.Text);
                    }

                    TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                    {
                        MskChallanDate.Text = dr["PC_Date"].GetDateString();
                        MskChallanMiti.Text = MskChallanMiti.GetNepaliDate(MskChallanDate.Text);
                    }
                }

                TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                _ledgerId = dr["Vendor_ID"].GetLong();
                SetLedgerInfo(_ledgerId);
                TxtDueDays.Text = dr["DueDays"].ToString();
                MskDueDays.Text = dr["DueDate"].GetDateString();

                _dtPartyInfo.Rows.Clear();
                var drp = _dtPartyInfo.NewRow();
                drp["PartyLedgerId"] = null;
                drp["PartyName"] = dr["Party_Name"];
                drp["ChequeNo"] = dr["ChqNo"];
                drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                drp["VatNo"] = dr["Vat_No"];
                drp["ContactPerson"] = dr["Contact_Person"];
                drp["Address"] = dr["Address"];
                drp["Mob"] = dr["Mobile_No"];
                _dtPartyInfo.Rows.Add(drp);

                TxtSubledger.Text = dr["SlName"].ToString();
                TxtAgent.Text = dr["AgentName"].ToString();
                TxtDepartment.Text = dr["DName"].ToString();
                _departmentId = dr["Cls1"].GetInt();
                _currencyId = dr["Cur_Id"].GetInt();
                TxtCurrency.Text = dr["Ccode"].ToString();
                TxtCurrencyRate.Text = _currencyId.Equals(ObjGlobal.SysCurrencyId) ? ObjGlobal.SysCurrencyRate.GetDecimalString(true) : dr["Cur_Rate"].GetDecimalString(true);
                LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());

                PAttachment1.Image = dr["PAttachment1"].GetImage();
                PAttachment2.Image = dr["PAttachment2"].GetImage();
                PAttachment3.Image = dr["PAttachment3"].GetImage();
                PAttachment4.Image = dr["PAttachment4"].GetImage();
                PAttachment5.Image = dr["PAttachment5"].GetImage();
            }

            if (dtDetails.Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsChallan.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dtDetails.Rows)
                {
                    DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtShortName"].Value = dr["PShortName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                    DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                    DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                    DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["Invoice_SNo"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                    if (dtProductTerm.Rows.Count > 0)
                    {
                        var serialNo = dr["Invoice_SNo"].GetInt();
                        var productId = dr["P_Id"].GetLong();
                        var exDetails = dtProductTerm.AsEnumerable().Any(c => c.Field<long>("ProductId").Equals(productId));
                        if (exDetails)
                        {
                            var exitAny = dtProductTerm.AsEnumerable().Any(c => c.Field<long>("ProductId").Equals(productId) && c.Field<int>("SNo").Equals(serialNo));
                            if (exitAny)
                            {
                                foreach (var ro in dtProductTerm.Select($"ProductId='{productId}' AND SNo='{serialNo}'"))
                                {
                                    var dataRow = _dtProductTerm.NewRow();
                                    dataRow["OrderNo"] = ro["OrderNo"];
                                    dataRow["SNo"] = dr["Invoice_SNo"];
                                    dataRow["TermId"] = ro["TermId"];
                                    dataRow["TermName"] = ro["TermName"];
                                    dataRow["Basis"] = ro["Basis"];
                                    dataRow["Sign"] = ro["Sign"];
                                    dataRow["ProductId"] = ro["ProductId"];
                                    dataRow["TermType"] = ro["TermType"];
                                    dataRow["TermCondition"] = ro["TermCondition"];
                                    dataRow["TermRate"] = ro["TermRate"];
                                    dataRow["TermAmt"] = ro["TermAmt"];
                                    dataRow["Source"] = ro["Source"];
                                    dataRow["Formula"] = ro["Formula"];
                                    dataRow["ProductSno"] = dr["Invoice_SNo"];
                                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                                }
                            }
                        }
                    }

                    iRows++;
                }

                DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
                DGrid.ClearSelection();
            }

            if (dtBillTerm.Rows.Count > 0)
            {
                var sNo = 1;
                _dtBillTerm.Rows.Clear();
                var dtBTerm = dsChallan.Tables[3];
                foreach (DataRow ro in dtBTerm.Rows)
                {
                    var dataRow = _dtBillTerm.NewRow();
                    dataRow["OrderNo"] = ro["OrderNo"];
                    dataRow["SNo"] = sNo;
                    dataRow["TermId"] = ro["TermId"];
                    dataRow["TermName"] = ro["TermName"];
                    dataRow["Basis"] = ro["Basis"];
                    dataRow["Sign"] = ro["Sign"];
                    dataRow["ProductId"] = ro["ProductId"];
                    dataRow["TermType"] = ro["TermType"];
                    dataRow["TermCondition"] = ro["TermCondition"];
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtBillTerm.Rows.InsertAt(dataRow, _dtBillTerm.RowsCount() + 1);
                    sNo++;
                }
            }

            VoucherTotalCalculation();
            DGrid.ClearSelection();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void FillOrderData(string voucherNo)
    {
        try
        {
            var dsOrder = _entry.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
            if (dsOrder.Tables.Count <= 0) return;
            if (dsOrder.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow dr in dsOrder.Tables[0].Rows)
            {
                if (_actionTag != "SAVE") TxtVno.Text = dr["PB_Invoice"].ToString();

                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtRefVno.Text = Convert.ToString(dr["PB_VNo"].ToString());

                if (dr["VNo_Date"].IsValueExits()) MskRefDate.Text = dr["Vno_Miti"].ToString();

                if (_actionTag != "SAVE")
                {
                    TxtOrder.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                    {
                        MskOrderDate.Text = dr["PO_Date"].GetDateString();
                        MskOrderMiti.Text = MskOrderMiti.GetNepaliDate(MskOrderDate.Text);
                    }

                    TxtChallan.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                    {
                        MskChallanDate.Text = dr["PC_Date"].GetDateString();
                        MskChallanMiti.Text = MskChallanMiti.GetNepaliDate(MskChallanDate.Text);
                    }
                }

                TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                _ledgerId = dr["Vendor_ID"].GetLong();
                SetLedgerInfo(_ledgerId);
                TxtDueDays.Text = dr["DueDays"].ToString();
                MskDueDays.Text = dr["DueDate"].GetDateString();

                _dtPartyInfo.Rows.Clear();
                var drp = _dtPartyInfo.NewRow();
                drp["PartyLedgerId"] = null;
                drp["PartyName"] = dr["Party_Name"];
                drp["ChequeNo"] = dr["ChqNo"];
                drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                drp["VatNo"] = dr["Vat_No"];
                drp["ContactPerson"] = dr["Contact_Person"];
                drp["Address"] = dr["Address"];
                drp["Mob"] = dr["Mobile_No"];
                _dtPartyInfo.Rows.Add(drp);

                TxtSubledger.Text = dr["SlName"].ToString();
                TxtAgent.Text = dr["AgentName"].ToString();
                TxtDepartment.Text = dr["DName"].ToString();
                _departmentId = dr["Cls1"].GetInt();
                _currencyId = dr["Cur_Id"].GetInt();
                TxtCurrency.Text = dr["Ccode"].ToString();
                TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());

                PAttachment1.Image = dr["PAttachment1"].GetImage();
                PAttachment2.Image = dr["PAttachment2"].GetImage();
                PAttachment3.Image = dr["PAttachment3"].GetImage();
                PAttachment4.Image = dr["PAttachment4"].GetImage();
                PAttachment5.Image = dr["PAttachment5"].GetImage();
            }

            if (dsOrder.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsOrder.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dsOrder.Tables[1].Rows)
                {
                    DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                    DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                    DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                    DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                    DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                    DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
                    iRows++;
                }

                DGrid.ClearSelection();
            }

            if (dsOrder.Tables[2].Rows.Count > 0)
            {
                _dtProductTerm.Rows.Clear();
                var dtPTerm = dsOrder.Tables[2];
                foreach (DataRow ro in dtPTerm.Rows)
                {
                    var amount = ro["TermAmt"].GetDecimal();
                    if (amount is 0) continue;
                    var dataRow = _dtProductTerm.NewRow();
                    dataRow["OrderNo"] = ro["OrderNo"];
                    dataRow["SNo"] = ro["SNo"];
                    dataRow["TermId"] = ro["TermId"];
                    dataRow["TermName"] = ro["TermName"];
                    dataRow["Basis"] = ro["Basis"];
                    dataRow["Sign"] = ro["Sign"];
                    dataRow["ProductId"] = ro["ProductId"];
                    dataRow["TermType"] = ro["TermType"];
                    dataRow["TermCondition"] = ro["TermCondition"];
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                }
            }

            if (dsOrder.Tables[3].Rows.Count > 0)
            {
                var sNo = 1;
                _dtBillTerm.Rows.Clear();
                var dtBTerm = dsOrder.Tables[3];
                foreach (DataRow ro in dtBTerm.Rows)
                {
                    var dataRow = _dtBillTerm.NewRow();
                    dataRow["OrderNo"] = ro["OrderNo"];
                    dataRow["SNo"] = sNo;
                    dataRow["TermId"] = ro["TermId"];
                    dataRow["TermName"] = ro["TermName"];
                    dataRow["Basis"] = ro["Basis"];
                    dataRow["Sign"] = ro["Sign"];
                    dataRow["ProductId"] = ro["ProductId"];
                    dataRow["TermType"] = ro["TermType"];
                    dataRow["TermCondition"] = ro["TermCondition"];
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtBillTerm.Rows.InsertAt(dataRow, _dtBillTerm.RowsCount() + 1);
                    sNo++;
                }
            }

            VoucherTotalCalculation();
            DGrid.ClearSelection();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void FillSalesInvoiceData(string voucherNo)
    {
        try
        {
            var dsSales = _salesEntry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
            var dtMaster = dsSales.Tables[0];
            var dtDetails = dsSales.Tables[1];
            var dtProductTerm = dsSales.Tables[2];
            var dtBillTerm = dsSales.Tables[3];
            if (dsSales.Tables.Count > 0)
            {
                if (dtMaster.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtMaster.Rows)
                    {
                        TxtVno.GetCurrentVoucherNo("PB", _numberScheme);
                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());

                        if (dr["VNo_Date"].IsValueExits())
                        {
                            MskRefDate.Text = dr["Vno_Miti"].ToString();
                        }

                        if (_actionTag != "SAVE")
                        {
                            TxtOrder.Text = Convert.ToString(dr["SO_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtOrder.Text.Trim()))
                            {
                                MskOrderDate.Text = dr["SO_Date"].GetDateString();
                                MskOrderMiti.Text = MskOrderMiti.GetNepaliDate(MskOrderDate.Text);
                            }
                            TxtChallan.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtChallan.Text.Trim()))
                            {
                                MskChallanDate.Text = dr["SC_Date"].GetDateString();
                                MskChallanMiti.Text = MskChallanMiti.GetNepaliDate(MskChallanDate.Text);
                            }
                        }

                        TxtVendor.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = dr["Customer_ID"].GetLong();
                        SetLedgerInfo(_ledgerId);
                        TxtDueDays.Text = dr["DueDays"].ToString();
                        MskDueDays.Text = dr["DueDate"].GetDateString();

                        _dtPartyInfo.Rows.Clear();
                        var drp = _dtPartyInfo.NewRow();
                        drp["PartyLedgerId"] = null;
                        drp["PartyName"] = dr["Party_Name"];
                        drp["ChequeNo"] = dr["ChqNo"];
                        drp["ChequeDate"] = dr["ChqDate"].GetDateString();
                        drp["VatNo"] = dr["Vat_No"];
                        drp["ContactPerson"] = dr["Contact_Person"];
                        drp["Address"] = dr["Address"];
                        drp["Mob"] = dr["Mobile_No"];
                        _dtPartyInfo.Rows.Add(drp);

                        TxtSubledger.Text = dr["SlName"].ToString();
                        TxtAgent.Text = dr["AgentName"].ToString();
                        TxtDepartment.Text = dr["DName"].ToString();
                        _departmentId = dr["Cls1"].GetInt();
                        _currencyId = dr["Cur_Id"].GetInt();
                        TxtCurrency.Text = dr["Ccode"].ToString();
                        TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                        LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                        TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                        LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                        LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                        TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());

                        PAttachment1.Image = dr["PAttachment1"].GetImage();
                        PAttachment2.Image = dr["PAttachment2"].GetImage();
                        PAttachment3.Image = dr["PAttachment3"].GetImage();
                        PAttachment4.Image = dr["PAttachment4"].GetImage();
                        PAttachment5.Image = dr["PAttachment5"].GetImage();
                    }

                    if (dtDetails.Rows.Count > 0)
                    {
                        var iRows = 0;
                        DGrid.Rows.Clear();
                        DGrid.Rows.Add(dtDetails.Rows.Count + 1);
                        foreach (DataRow dr in dtDetails.Rows)
                        {
                            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = dr["AltUnitId"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                            DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                            DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["PO_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["PO_Sno"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["PC_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["PC_SNo"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
                            DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
                            if (dtProductTerm.Rows.Count > 0)
                            {
                                var serialNo = dr["Invoice_SNo"].GetInt();
                                var productId = dr["P_Id"].GetLong();
                                var exDetails = dtProductTerm.AsEnumerable().Any(c => c.Field<long>("ProductId").Equals(productId));
                                if (exDetails)
                                {
                                    var exitAny = dtProductTerm.AsEnumerable().Any(c => c.Field<long>("ProductId").Equals(productId) && c.Field<int>("SNo").Equals(serialNo));
                                    if (exitAny)
                                    {
                                        foreach (var ro in dtProductTerm.Select($"ProductId='{productId}' AND SNo='{serialNo}'"))
                                        {
                                            var dataRow = _dtProductTerm.NewRow();
                                            dataRow["OrderNo"] = ro["OrderNo"];
                                            dataRow["SNo"] = dr["Invoice_SNo"];
                                            dataRow["TermId"] = ro["TermId"];
                                            dataRow["TermName"] = ro["TermName"];
                                            dataRow["Basis"] = ro["Basis"];
                                            dataRow["Sign"] = ro["Sign"];
                                            dataRow["ProductId"] = ro["ProductId"];
                                            dataRow["TermType"] = ro["TermType"];
                                            dataRow["TermCondition"] = ro["TermCondition"];
                                            dataRow["TermRate"] = ro["TermRate"];
                                            dataRow["TermAmt"] = ro["TermAmt"];
                                            dataRow["Source"] = ro["Source"];
                                            dataRow["Formula"] = ro["Formula"];
                                            dataRow["ProductSno"] = dr["Invoice_SNo"];
                                            _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                                        }
                                    }
                                }
                            }
                            iRows++;
                        }
                        DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
                        DGrid.ClearSelection();
                    }

                    if (dtBillTerm.Rows.Count > 0)
                    {
                        var sNo = 1;
                        _dtBillTerm.Rows.Clear();
                        var dtBTerm = dsSales.Tables[3];
                        foreach (DataRow ro in dtBTerm.Rows)
                        {
                            var dataRow = _dtBillTerm.NewRow();
                            dataRow["OrderNo"] = ro["OrderNo"];
                            dataRow["SNo"] = sNo;
                            dataRow["TermId"] = ro["TermId"];
                            dataRow["TermName"] = ro["TermName"];
                            dataRow["Basis"] = ro["Basis"];
                            dataRow["Sign"] = ro["Sign"];
                            dataRow["ProductId"] = ro["ProductId"];
                            dataRow["TermType"] = ro["TermType"];
                            dataRow["TermCondition"] = ro["TermCondition"];
                            dataRow["TermRate"] = ro["TermRate"];
                            dataRow["TermAmt"] = ro["TermAmt"];
                            dataRow["Source"] = ro["Source"];
                            dataRow["Formula"] = ro["Formula"];
                            dataRow["ProductSno"] = ro["SNo"];
                            _dtBillTerm.Rows.InsertAt(dataRow, _dtBillTerm.RowsCount() + 1);
                            sNo++;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void DeletedRowExitsOrNot(int sno, long productId)
    {
        if (_dtProductTerm.RowsCount() <= 0 || DGrid.CurrentRow == null)
        {
            return;
        }
        var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductSno").Equals(sno.ToString()) && c.Field<string>("ProductId").Equals(productId.ToString()));
        if (!exDetails) return;
        foreach (DataRow ro in _dtProductTerm.Rows)
        {
            var index = _dtProductTerm.Rows.IndexOf(ro);
            if (sno == ro["ProductSno"].GetLong() && productId == ro["ProductId"].GetInt())
            {
                _dtProductTerm.Rows.Remove(ro);
                break;
            }
        }

        for (var i = 0; i < _dtProductTerm.RowsCount(); i++)
        {
            var exitSno = _dtProductTerm.Rows[i]["ProductSno"].GetInt();
            exitSno -= 1;
            _dtProductTerm.Rows[i].SetField("ProductSno", exitSno);
        }
    }

    private void BindDataToComboBox()
    {
        _master.BindVoucherType(CmbInvoiceType, "PB");
        CmbInvoiceType.SelectedIndex = 0;

        _master.BindPaymentType(CmbPaymentMode);
        CmbPaymentMode.SelectedIndex = 1;
    }

    private void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.BackgroundImage == null) return;
        var fileExt = Path.GetExtension(pictureBox.ImageLocation);
        var location = pictureBox.ImageLocation;
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png")
        {
            ObjGlobal.PreviewPicture(pictureBox, string.Empty);
        }
        else
        {
            Process.Start(location ?? string.Empty);
        }
    }

    private bool AddTextToGrid(bool isUpdate)
    {
        _description = ObjGlobal.PurchaseDescriptionsEnable switch
        {
            true => GetMasterList.GetNarrationOfProduct(_actionTag, _description),
            _ => _description
        };
        if (ObjGlobal.PurchaseGodownEnable && TxtGodown.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtGodown, "GODOWN IS MANDATORY..!!");
            return false;
        }

        if (TxtQty.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtQty, "PRODUCT QTY IS CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _productId.ToString();
        DGrid.Rows[iRows].Cells["GTxtShortName"].Value = TxtShortName.Text;
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = _godownId.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;

        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = _altUnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text.Trim();

        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId;
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;

        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBasicAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = TxtTermAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = TxtNetAmount.GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = TxtAltQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = TxtQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = _description;
        DGrid.Rows[iRows].Cells["GTxtBatchNo"].Value = _batchNo.Length > 0 ? _batchNo : string.Empty;
        DGrid.Rows[iRows].Cells["GTxtMFGDate"].Value = _batchNo.Length > 0 ? _mfgDate : string.Empty;
        DGrid.Rows[iRows].Cells["GMskEXPDate"].Value = _batchNo.Length > 0 ? _expDate : string.Empty;
        DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = 0;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = 0;

        DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = _productTaxRate;

        DGrid.Rows[iRows].Cells["GTxtTaxGroupId"].Value = string.Empty;

        var vatPercentAmount = _productTaxRate / 100;
        var taxRate = 1 + vatPercentAmount;
        var vatAmount = vatPercentAmount > 0 ? TxtNetAmount.GetDecimal() / taxRate : 0;

        DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = vatAmount.GetDecimal();
        DGrid.Rows[iRows].Cells["GTxtTaxableAmount"].Value = _isTaxable ? TxtNetAmount.GetDecimal() : 0;

        var currentRow = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[currentRow].Cells[_columnIndex];
        _master.UpdateProductPurchaseRate(TxtRate.GetDouble(), TxtProduct.Text);
        VoucherTotalCalculation();
        CalculateBillingTerm();
        if (_isRowUpdate)
        {
            EnableGridControl();
            ClearProductDetails();
            DGrid.Focus();
            return false;
        }
        ClearProductDetails();
        TxtProduct.AcceptsTab = false;
        GetSerialNo();
        return true;
    }

    private bool IsValidInformation()
    {
        if (_actionTag != "SAVE")
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) == DialogResult.No)
            {
                return false;
            }
        }

        if (TxtVno.IsBlankOrEmpty())
        {
            TxtVno.Enabled = true;
            this.NotifyValidationError(TxtVno, $"VOUCHER NUMBER MUST HAVE A VALUE FOR {_actionTag}..!!");
            return false;
        }

        if (_tagStrings.Contains(_actionTag)) return true;

        if (MskDate.MaskCompleted)
        {
            if (MskDate.Text.IsDateExits("D"))
            {
                if (!MskDate.Text.IsValidDateRange("D"))
                {
                    this.NotifyValidationError(MskDate,
                        $"ENTER DATE MUST BE BETWEEN {ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}");
                    return false;
                }
            }
            else
            {
                this.NotifyValidationError(MskDate, "ENTER VOUCHER DATE IS INVALID..!!");
                return false;
            }
        }
        else if (!MskDate.MaskCompleted)
        {
            this.NotifyValidationError(MskDate, "ENTER VOUCHER MITI IS INVALID..!!");
            return false;
        }

        if (MskMiti.MaskCompleted)
        {
            if (MskMiti.Text.IsDateExits("M"))
            {
                if (!MskMiti.Text.IsValidDateRange("M"))
                {
                    this.NotifyValidationError(MskMiti,
                        $"ENTER MITI MUST BE BETWEEN {ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}");
                    return false;
                }
            }
            else
            {
                this.NotifyValidationError(MskMiti, "ENTER VOUCHER MITI IS INVALID..!!");
                return false;
            }
        }
        else if (!MskDate.MaskCompleted)
        {
            this.NotifyValidationError(MskMiti, "ENTER VOUCHER MITI IS INVALID..!!");
            return false;
        }
        if (TxtRefVno.IsValueExits())
        {
            var dtVoucherNo = _purchaseInvoice.CheckRefVoucherNo(_actionTag, _ledgerId, TxtRefVno.Text, TxtVno.Text);
            if (dtVoucherNo.RowsCount() > 0)
            {
                this.NotifyValidationError(TxtRefVno, "THIS INVOICE NUMBER IS ALREADY ENTER IN THE LEDGER..!!");
                return false;
            }
        }
        if (DGrid.RowCount > 0 && !DGrid.Rows[0].Cells["GTxtProductId"].Value.IsValueExits())
        {
            this.NotifyValidationError(DGrid, "ENTER VOUCHER PRODUCT DETAILS..!!");
            return false;
        }
        if (DGrid.RowCount <= 0)
        {
            this.NotifyValidationError(DGrid, "ENTER VOUCHER PRODUCT DETAILS..!!");
            return false;
        }
        if (TxtVno.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVno, "ENTER VOUCHER NUMBER OR SELECTED IT..!!");
            return false;
        }
        if (TxtVendor.IsBlankOrEmpty() || _ledgerId is 0)
        {
            this.NotifyValidationError(TxtVendor, "ENTER VENDOR LEDGER OR SELECTED IT..!!");
            return false;
        }
        if (TxtRemarks.IsBlankOrEmpty() && _tagStrings.Contains(_actionTag))
        {
            this.NotifyValidationError(TxtRemarks, $"ENTER REMARKS OF THIS VOUCHER FOR {_actionTag}..!!");
            return false;
        }
        if ((TxtSubledger.IsBlankOrEmpty() || _subLedgerId is 0) && ObjGlobal.PurchaseSubLedgerMandatory)
        {
            this.NotifyValidationError(TxtSubledger, "ENTER SUB LEDGER OF THIS VOUCHER..!!");
            return false;
        }
        if ((TxtAgent.IsBlankOrEmpty() || _agentId is 0) && ObjGlobal.PurchaseAgentMandatory)
        {
            this.NotifyValidationError(TxtSubledger, "ENTER SUB LEDGER OF THIS VOUCHER..!!");
            return false;
        }
        if ((TxtDepartment.IsBlankOrEmpty() || _departmentId is 0) && ObjGlobal.PurchaseDepartmentMandatory)
        {
            this.NotifyValidationError(TxtDepartment, "ENTER DEPARTMENT OF THIS VOUCHER..!!");
            return false;
        }

        return true;
    }

    private string CalculateBillingTerm()
    {
        decimal termAmount = 0;
        decimal basicAmount = LblTotalBasicAmount.GetDecimal();
        decimal termTaxableAmount = _totalTaxable;
        var term = _master.GetTermCalculationForVoucher("PB");
        if (term.RowsCount() <= 0)
        {
            return string.Empty;
        }
        var exitsTerm = _dtBillTerm.Copy();
        _dtBillTerm.Rows.Clear();
        var iRows = 1;
        if (term.Rows.Count <= 0)
        {
            return termAmount.GetDecimalString();
        }
        foreach (DataRow ro in term.Rows)
        {
            decimal exitRate = 0;
            decimal exitAmount = 0;
            var termId = ro["TermId"].GetInt();
            var exDetails = exitsTerm.AsEnumerable().Any(c => c.Field<string>("TermId").Equals(termId.ToString()));
            if (exDetails)
            {
                var dtAmount = exitsTerm.Select($" TermId='{termId}'").CopyToDataTable();
                exitRate = dtAmount.Rows[0]["TermRate"].GetDecimal();
                exitAmount = dtAmount.Rows[0]["TermAmt"].GetDecimal();
            }
            var row = _dtBillTerm.NewRow();
            row["SNo"] = iRows;
            row["TermId"] = ro["TermId"];
            row["OrderNo"] = ro["OrderNo"];
            row["TermName"] = ro["TermDesc"];
            var termBasic = ro["TermBasic"].GetTrimReplace();
            row["Basis"] = termBasic;
            row["Sign"] = ro["TermSign"];
            exitRate = exitRate > 0 ? exitRate : _actionTag.Equals("SAVE") ? ro["TermRate"].GetDecimal() : 0.GetDecimal();
            row["TermRate"] = exitRate > 0 ? exitRate : _actionTag.Equals("SAVE") ? ro["TermRate"].GetDecimalString() : 0.GetDecimalString();
            if (CmbInvoiceType.Text.Equals("P VAT") && ObjGlobal.PurchaseVatTermId.Equals(termId))
            {
                var termTotalAmount = exitRate > 0 ? (exitRate * termTaxableAmount / 100) : 0;
                row["TermAmt"] = termTotalAmount.GetDecimalString();
            }
            else
            {
                var termTotalAmount = exitRate > 0 ? (exitRate * basicAmount / 100) : 0;
                row["TermAmt"] = termTotalAmount.GetDecimalString();
            }
            var termType = ro["TermType"].GetTrimReplace();
            row["TermType"] = ro["TermType"];
            row["TermCondition"] = ro["TermCondition"];
            decimal round = 0;
            if (termType.Equals("R"))
            {
                var netAmount = LblTotalBasicAmount.GetDecimal() + termAmount;
                var roundAmount = netAmount - Math.Truncate(netAmount);
                if (roundAmount >= 0.5.GetDecimal() && ro["TermSign"].GetTrimReplace().Equals("+"))
                {
                    round = 1 - roundAmount;
                }
                if (roundAmount < 0.5.GetDecimal() && ro["TermSign"].GetTrimReplace().Equals("-"))
                {
                    round = roundAmount;
                }
            }
            if (termType.Equals("R"))
            {
                row["TermAmt"] = round.GetDecimal();
            }
            row["ProductId"] = 0;
            row["ProductSno"] = 0;
            _dtBillTerm.Rows.InsertAt(row, iRows);
            var amount = _dtBillTerm.Rows[iRows - 1]["TermAmt"].GetDecimal();
            var termRatio =basicAmount > 0 ?  amount / basicAmount * termTaxableAmount : 0;
            if (_dtBillTerm.Rows[iRows - 1]["Sign"].Equals("-"))
            {
                termAmount -= amount;
                termTaxableAmount -= termRatio;
            }
            else
            {
                termAmount += amount;
                termTaxableAmount += termRatio;
            }
            iRows++;
        }
        return termAmount.GetDecimalString();
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    //  OBJECT FOR THIS FORM

    #region -------------- OBJECT --------------

    private int _subLedgerId;
    private int _currencyId = ObjGlobal.SysCurrencyId;
    private int _departmentId;
    private int _agentId;
    private int _godownId;
    private int _unitId;
    private int _altUnitId;
    private int _rowIndex;
    private int _columnIndex;

    private long _ledgerId;
    private long _productId;

    private bool _rowDelete;
    private bool _isRowUpdate;
    private bool _isProvision;
    private bool _isZoom;
    private bool _isPTermExits;
    private bool _isBTermExits;
    private bool _isTaxable;
    private bool _isBatch;


    private string _actionTag = string.Empty;
    private string _numberScheme = string.Empty;
    private string _txtZoomVno = string.Empty;
    private string _invoiceType;
    private string _description = string.Empty;
    private string _batchNo = string.Empty;
    private string _mfgDate = string.Empty;
    private string _expDate = string.Empty;
    private string[] _tagStrings = ["DELETE", "REVERSE"];
    private string[] _ledgerType = ["CASH", "BANK"];
    private string ledgerType;

    private decimal _conQty;
    private decimal _conAltQty;
    private decimal _totalTaxable;
    private decimal _totalVat;
    private decimal _productTaxRate = 0;
    private KeyPressEventArgs _getKeys;

    private DataTable _dtPartyInfo;
    private DataTable _dtProductTerm;
    private DataTable _dtBillTerm;
    private DataTable _dtBatchInfo;

    private ISalesEntry _salesEntry;
    private IPurchaseInvoice _purchaseInvoice;
    private IPurchaseEntry _entry;
    private ICashBankVoucherRepository _cashBank;
    private IMasterSetup _master;
    private IPurchaseDesign _design;


    #region ** ----- GRID CONTROL ----- **
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtProduct { get; set; }
    private MrGridTextBox TxtGodown { get; set; }
    private MrGridTextBox TxtAltUnit { get; set; }
    private MrGridTextBox TxtUnit { get; set; }
    private MrGridNumericTextBox TxtAltQty { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtBasicAmount { get; set; }
    private MrGridNumericTextBox TxtTermAmount { get; set; }
    private MrGridNumericTextBox TxtNetAmount { get; set; }
    #endregion

    #endregion -------------- OBJECT --------------

        
}