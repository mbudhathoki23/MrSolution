using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.ProductSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.DataEntry.PurchaseMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
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

public partial class FrmPurchaseReturnChallanEntry : MrForm
{
    // PURCHASE CHALLAN ENTRY

    #region --------------- PURCHASE CHALLAN ENTRY ---------------

    public FrmPurchaseReturnChallanEntry(bool zoom, string txtZoomVno, bool provisionVoucher = false, string invoiceType = "NORMAL")
    {
        InitializeComponent();
        _dtPartyInfo = _master.GetPartyInfo();
        _dtBillTerm = _master.GetBillingTerm();
        _dtProductTerm = _master.GetBillingTerm();
        _challanReturn = new PurchaseChallanReturnRepository();
        _challanReturn.DetailsList = [];
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _isPTermExits = _master.IsBillingTermExitsOrNot("PCR", "P");
        _isBTermExits = _master.IsBillingTermExitsOrNot("PCR", "B");
        BindDataToComboBox();
        DesignGridColumnsAsync();
        EnableControl();
        ClearControl();
    }

    private void FrmPurchaseReturnChallanEntry_Load(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillChallanReturnData(_txtZoomVno);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmPurchaseReturnChallanEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        _getKeys = e;
        if (e.KeyChar is not (char)Keys.Escape) return;
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
                SendToBack();
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

    private void FrmPurchaseReturnChallanEntry_Shown(object sender, EventArgs e)
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
        ClearControl();
        EnableControl(true);
        TxtVno.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }

    private void BtnSalesInvoice_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        FillSalesInvoiceData(voucherNo);
    }

    private void BtnPrintInvoice_Click(object sender, EventArgs e)
    {
        var frmDp = new FrmDocumentPrint("Crystal", "PCR", TxtVno.Text, TxtVno.Text, TxtVno.Text)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
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
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PCR");
        if (voucherNo.IsValueExits())
        {
            TxtVno.Text = voucherNo;
            FillChallanReturnData(TxtVno.Text);
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

    private void BtnReturn_Click(object sender, EventArgs e)
    {
        new FrmPurchaseReturnEntry(true, string.Empty, false, "PCR").ShowDialog();
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PCR", "OPCR");
        if (_actionTag == "SAVE")
        {
            return;
        }
        ClearControl();
        TxtVno.Text = voucherNo;
        FillChallanReturnData(TxtVno.Text);
    }

    private void BtnOrder_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PO", "OPO");
        ClearControl();
        TxtOrderNo.Text = voucherNo;
        FillOrderData(TxtOrderNo.Text);
    }

    private void BtnIndent_Click(object sender, EventArgs e)
    {
        TxtIndentNo.Text =
            GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PIN", "OPIN");
        FillIndentData(TxtIndentNo.Text);
    }

    private void BtnVendor_Click(object sender, EventArgs e)
    {
        (TxtVendor.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag, "VENDOR");
        SetLedgerInfo(_ledgerId);
    }

    private void BtnCurrency_Click(object sender, EventArgs e)
    {
        (TxtCurrencyRate.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
    }

    private void BtnSubledger_Click(object sender, EventArgs e)
    {
        (TxtSubledger.Text, _subLedgerId) = GetMasterList.GetSubLedgerList(_actionTag);
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
        if (!TxtBillTermAmount.Enabled || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0) return;
        var result = new FrmTermCalculation(_dtBillTerm.RowsCount() > 0 ? "UPDATE" : "SAVE",
            LblTotalBasicAmount.Text, false, "PC", 0, 0, _dtBillTerm, LblTotalQty.Text);
        result.ShowDialog();
        TxtBillTermAmount.Text = result.TotalTermAmount;
        AddToBillingTerm(result.CalcTermTable);
        TxtNetAmount.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValidInformation())
        {
            if (SavePurchaseChallanReturn() != 0)
            {
                PrintVoucher();
                if (_isZoom)
                {
                    Close();
                }

                this.NotifySuccess($@"{TxtVno.Text} PURCHASE CHALLAN RETURN NUMBER {_actionTag} SUCCESSFULLY..!!");
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
            else
            {
                this.NotifyError($@"ERROR OCCURS ON PURCHASE CHALLAN RETURN NUMBER {_actionTag}..!!");
                MskMiti.Focus();
            }
        }
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        ClearControl();
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVno_Click(sender, e);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
        else if (TxtVno.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
        }
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (_actionTag == "SAVE")
        {
            if (TxtVno.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtVno, "CHALLAN VOUCHER NUMBER IS BLANK..!!");
            }
            else
            {
                var dtVoucher = _challanReturn.CheckVoucherExitsOrNot("PC_Master", "PC_Invoice", TxtVno.Text);
                if (dtVoucher.RowsCount() > 0 && _actionTag.Equals("SAVE"))
                {
                    this.NotifyValidationError(TxtVno, "CHALLAN VOUCHER NUMBER ALREADY EXITS..!!");
                }
                else if (dtVoucher.Rows.Count <= 0 && !_actionTag.Equals("SAVE"))
                {
                    this.NotifyValidationError(TxtVno, "CHALLAN VOUCHER NUMBER NOT EXITS..!!");
                }
            }
        }
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (!MskMiti.MaskCompleted && MskMiti.Enabled && TxtVno.IsValueExits())
        {
            this.NotifyValidationError(MskMiti, "VOUCHER MITI IS INVALID..!!");
        }
        else if (MskMiti.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskMiti, $"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
        }

        if (MskMiti.MaskCompleted)
        {
            MskDate.GetEnglishDate(MskMiti.Text);
        }
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (!MskDate.MaskCompleted && MskDate.Enabled && TxtVno.IsValueExits())
        {
            this.NotifyValidationError(MskDate, "VOUCHER DATE IS INVALID..!!");
        }
        else if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskDate, $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
        }

        if (MskDate.MaskCompleted)
        {
            MskMiti.GetNepaliDate(MskDate.Text);
        }
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtRefVno.IsValueExits())
            {
                GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
            else if (TxtOrderNo.Enabled)
            {
                TxtOrderNo.Focus();
            }
            else if (TxtIndentNo.Enabled)
            {
                TxtIndentNo.Focus();
            }
            else
            {
                GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
        }
        else if (e.KeyCode is Keys.F1)
        {
            var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PC", "OPCR");
            if (voucherNo.IsValueExits())
            {
                TxtRefVno.Text = voucherNo;
                FillChallanData(voucherNo);
            }

            TxtRefVno.Focus();
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
            GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
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
            GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
        }
    }

    private void TxtVendor_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVendor_Click(sender, e);
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
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtVendor, BtnVendor);
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
            {
                this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY PLEASE SELECT CURRENCY");
            }
            else
            {
                GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCurrency, BtnCurrency);
        }
    }

    private void TxtCurrencyRate_TextChanged(object sender, EventArgs e)
    {
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

    private void TxtSubledger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSubledger_Click(sender, e);
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
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSubledger, BtnSubledger);
        }
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnSubledger_Click(sender, e);
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
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtDepartment, BtnDepartment);
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
            {
                this.NotifyValidationError(TxtDepartment, "AGENT IS MANDATORY PLEASE SELECT AGENT");
            }
            else
            {
                DGrid.Focus();
            }
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtAgent, BtnAgent);
        }
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No) return;
            _isRowDelete = true;
            var sno = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
            var productId = DGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
            DeletedRowExitsOrNot(sno, productId);
            DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
            if (DGrid.RowCount is 0)
            {
                DGrid.Rows.Add();
            }

            GetSerialNo();
        }

        if (e.KeyCode is Keys.Enter && !TxtProduct.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableGridControl(true);
            DGrid.CurrentCell = DGrid.CurrentRow.Cells[0];
            AdjustControlsInDataGrid();
            if (DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.IsValueExits())
            {
                TextFromGrid();
                TxtProduct.Focus();
                return;
            }

            GetSerialNo();
            TxtProduct.Focus();
        }
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void TxtBillTermAmount_Enter(object sender, EventArgs e)
    {
        if (TxtBillTermAmount.Enabled && DGrid.RowCount > 0)
        {
            BtnBillingTerm_Click(sender, e);
        }
    }

    private void TxtBillTermAmount_TextChanged(object sender, EventArgs e)
    {
        LblTotalNetAmount.Text = (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal(true)).GetDecimalString();
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalNetAmount.Text);
    }

    private void TxtBillTermAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
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
                GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
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
            BtnOrderNo.PerformClick();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtOrderNo, BtnOrderNo);
        }
    }

    private void TxtOrder_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnIndentNo.PerformClick();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtIndentNo, BtnIndentNo);
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
            var isFileExists = dlg.FileName;
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
            var isFileExists = dlg.FileName;
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
            var isFileExists = dlg.FileName;
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

    #endregion --------------- PURCHASE CHALLAN ENTRY ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private int SavePurchaseChallanReturn()
    {
        try
        {
            if (_actionTag is "SAVE")
            {
                TxtVno.GetCurrentVoucherNo("PCR", _numberScheme);
            }

            _challanReturn.PcrMaster.PCR_Invoice = TxtVno.Text;
            _challanReturn.PcrMaster.Invoice_Date = DateTime.Parse(MskDate.Text);
            _challanReturn.PcrMaster.Invoice_Miti = MskMiti.Text;
            _challanReturn.PcrMaster.PB_Vno = TxtRefVno.Text;
            _challanReturn.PcrMaster.Vno_Date = DateTime.Parse(ObjGlobal.ReturnEnglishDate(MskRefDate.Text));
            _challanReturn.PcrMaster.Vno_Miti = MskRefDate.Text;
            _challanReturn.PcrMaster.Invoice_Time = DateTime.Now;
            _challanReturn.PcrMaster.Vendor_ID = _ledgerId;

            _challanReturn.PcrMaster.Party_Name = _dtPartyInfo.Rows.Count > 0
                ? _dtPartyInfo.Rows[0]["PartyName"].ToString()
                : string.Empty;
            _challanReturn.PcrMaster.Vat_No =
                _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
            _challanReturn.PcrMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0
                ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString()
                : string.Empty;
            _challanReturn.PcrMaster.Mobile_No =
                _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
            _challanReturn.PcrMaster.Address = _dtPartyInfo.Rows.Count > 0
                ? _dtPartyInfo.Rows[0]["Address"].ToString()
                : string.Empty;
            _challanReturn.PcrMaster.ChqNo = _dtPartyInfo.Rows.Count > 0
                ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString()
                : string.Empty;
            _challanReturn.PcrMaster.ChqDate =
                _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString())
                    ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString())
                    : DateTime.Now;

            _challanReturn.PcrMaster.Invoice_Type = CmbInvoiceType.Text;
            _challanReturn.PcrMaster.Invoice_In = CmbPaymentMode.Text;
            _challanReturn.PcrMaster.DueDays = TxtDueDays.Text.GetInt();
            _challanReturn.PcrMaster.DueDate = MskDueDays.MaskCompleted
                ? ObjGlobal.ReturnEnglishDate(MskDueDays.Text).GetDateTime()
                : DateTime.Now;
            _challanReturn.PcrMaster.Agent_Id = _agentId;
            _challanReturn.PcrMaster.Subledger_Id = _subLedgerId;
            _challanReturn.PcrMaster.QOT_Invoice = TxtIndentNo.Text;
            _challanReturn.PcrMaster.QOT_Date = _mskIndentDate.GetDateTime();
            _challanReturn.PcrMaster.PO_Invoice = TxtOrderNo.Text;
            _challanReturn.PcrMaster.PO_Date = _mskOrderDate.GetDateTime();
            _challanReturn.PcrMaster.Cls1 = _departmentId;
            _challanReturn.PcrMaster.Cls2 = 0;
            _challanReturn.PcrMaster.Cls3 = 0;
            _challanReturn.PcrMaster.Cls4 = 0;
            _challanReturn.PcrMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _challanReturn.PcrMaster.Cur_Rate =
                TxtCurrencyRate.Text.GetDecimal() > 0 ? TxtCurrencyRate.Text.GetDecimal() : 1;
            _challanReturn.PcrMaster.Counter_ID = 0;
            _challanReturn.PcrMaster.B_Amount = LblTotalBasicAmount.Text.GetDecimal();
            _challanReturn.PcrMaster.T_Amount = TxtBillTermAmount.Text.GetDecimal();
            _challanReturn.PcrMaster.N_Amount = LblTotalNetAmount.Text.GetDecimal();
            _challanReturn.PcrMaster.LN_Amount = LblTotalLocalNetAmount.Text.GetDecimal();
            _challanReturn.PcrMaster.Tender_Amount = 0;
            _challanReturn.PcrMaster.Change_Amount = 0;
            _challanReturn.PcrMaster.V_Amount = 0;
            _challanReturn.PcrMaster.Tbl_Amount = 0;
            _challanReturn.PcrMaster.Action_type = _actionTag;
            _challanReturn.PcrMaster.R_Invoice = false;
            _challanReturn.PcrMaster.No_Print = 0;
            _challanReturn.PcrMaster.In_Words = LblNumberInWords.Text;
            _challanReturn.PcrMaster.Remarks = TxtRemarks.Text;
            _challanReturn.PcrMaster.Audit_Lock = false;
            _challanReturn.PcrMaster.SyncRowVersion = _challanReturn.ReturnMaxSyncRowVersion("PC", TxtVno.Text);
            _challanReturn.PcrMaster.GetView = DGrid;
            _challanReturn.PcrMaster.BillTerm = _dtBillTerm;
            _challanReturn.PcrMaster.ProductTerm = _dtProductTerm;

            //_entry.GetImage.VoucherNo = TxtVno.Text;
            _challanReturn.PcrMaster.PAttachment1 = PAttachment1.Image.ConvertImage();
            _challanReturn.PcrMaster.PAttachment2 = PAttachment2.Image.ConvertImage();
            _challanReturn.PcrMaster.PAttachment3 = PAttachment3.Image.ConvertImage();
            _challanReturn.PcrMaster.PAttachment4 = PAttachment4.Image.ConvertImage();
            _challanReturn.PcrMaster.PAttachment5 = PAttachment5.Image.ConvertImage();

            var sync = _challanReturn.ReturnSyncRowVersionVoucher("PCR", TxtVno.Text);
            _challanReturn.PcrMaster.SyncRowVersion = sync;

            _challanReturn.PcrMaster.GetView = DGrid;

            // PURCHASE RETURN CHALLAN DETAILS
            _challanReturn.DetailsList?.Clear();
            foreach (DataGridViewRow viewRow in DGrid.Rows)
            {
                var list = new PCR_Details();
                var detailsProduct = viewRow.Cells["GTxtProductId"].Value.GetLong();
                if (detailsProduct is 0)
                {
                    continue;
                }
                list.PCR_Invoice = TxtVno.Text;
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
                list.Tax_Amount = list.V_Amount = list.V_Rate = 0;
                list.Free_Unit_Id = 0;
                list.Free_Qty = list.StockFree_Qty = 0;
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

                _challanReturn.DetailsList?.Add(list);
            }

            // PURCHASE TERMS
            _challanReturn.Terms.Clear();
            foreach (DataRow row in _dtProductTerm.Rows)
            {
                var list = new PCR_Term()
                {
                    PCR_VNo = TxtVno.Text,
                    PT_Id = row["TermId"].GetInt(),
                    SNo = row["SNo"].GetInt(),
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
                _challanReturn.Terms.Add(list);
            }

            return _challanReturn.SavePurchaseChallanReturn(_actionTag);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            MessageBox.Show(ex.Message, ObjGlobal.Caption);
            return 0;
        }
    }

    private void ReturnVoucherNo()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("PCR");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("PCR", _numberScheme);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("PCR");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _numberScheme = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"PURCHASE CHALLAN RETURN DETAILS [{_actionTag}]"
            : "PURCHASE CHALLAN RETURN DETAILS";
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVno.Clear();
        if (!_isZoom)
        {
            if (_actionTag.Equals("SAVE"))
            {
                TxtVno.GetCurrentVoucherNo("PCR", _numberScheme);
            }

            if (BtnNew.Enabled)
            {
                MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                MskMiti.Text = DateTime.Now.GetNepaliDate();
                MskRefDate.Text = ObjGlobal.SysDateType == "M"
                    ? MskRefDate.GetNepaliDate(MskDate.Text)
                    : MskDate.Text;
            }

            _ledgerId = _agentId = _subLedgerId = _departmentId = _departmentId = _currencyId = 0;
            TxtOrderNo.Clear();
            TxtIndentNo.Clear();
            TxtRefVno.Clear();
            TxtDueDays.Clear();
            TxtVendor.Clear();
            TxtDepartment.Clear();
            TxtAgent.Clear();
            TxtSubledger.Clear();
            TxtCurrencyRate.Clear();
            TxtVendor.Clear();
            CmbInvoiceType.SelectedIndex = 4;
            CmbPaymentMode.SelectedIndex = 0;
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
            TxtBillTermAmount.Clear();
            LblPanNo.IsClear();
            LblBalance.IsClear();
            LblCreditDays.IsClear();
            LblCreditLimit.IsClear();
            LblTotalAltQty.IsClear();
            LblTotalQty.IsClear();
            LblTotalBasicAmount.IsClear();
            LblTotalNetAmount.IsClear();
            LblTotalLocalNetAmount.IsClear();
            TxtTermAmount.Clear();
            if (_dtProductTerm.RowsCount() > 0)
            {
                _dtProductTerm.Rows.Clear();
            }

            if (_dtBillTerm.RowsCount() > 0)
            {
                _dtBillTerm.Rows.Clear();
            }

            DGrid.Rows.Clear();
            ClearProductDetails();
        }

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
        VoucherTotalCalculation();
        AdjustControlsInDataGrid();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = BtnReverse.Enabled = !isEnable;
        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtOrderNo.Enabled = BtnOrderNo.Enabled = isEnable && ObjGlobal.PurchaseOrderEnable;
        TxtIndentNo.Enabled = BtnIndentNo.Enabled = isEnable && ObjGlobal.PurchaseIndentEnable;
        CmbPaymentMode.Enabled = CmbInvoiceType.Enabled = isEnable;
        TxtVendor.Enabled = BtnVendor.Enabled = isEnable;
        MskDueDays.Enabled = false;
        TxtDueDays.Enabled = isEnable;
        TxtCurrency.Enabled = BtnCurrency.Enabled =
            TxtCurrencyRate.Enabled = isEnable && ObjGlobal.PurchaseCurrencyEnable;
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

        TxtGodown.Enabled = TxtGodown.Visible = isEnable && ObjGlobal.StockGodownEnable;

        TxtAltQty.Enabled = false;
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
        //DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }

    private void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _master.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0) return;
        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["Balance"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["CrLimit"].GetDecimalString();
        LblCreditDays.Text = dtLedger.Rows[0]["CrDays"].GetDecimalString();
        TxtDueDays.Text = LblCreditDays.GetDecimalString();
        MskDueDays.Text = MskDate.GetDateTime().AddDays(TxtDueDays.GetDouble()).GetDateString();
        var ledgerType = dtLedger.Rows[0]["GLType"].GetUpper();
        if (ledgerType is "CASH" or "BANK")
        {
            CashAndBankValidation(ledgerType.Substring(0, 1));
        }
    }

    private void OpenProductList()
    {
        (TxtProduct.Text, _productId) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        SetProductInfo(_productId);
    }

    private void OpenGodownList()
    {
    }

    private void PrintVoucher()
    {
        var dtDesign = _master.GetPrintVoucherList("PCR");
        if (dtDesign.Rows.Count <= 0)
        {
            return;
        }
    }

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null) return;
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSno"].Value.GetString();
        TxtProduct.Text = DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.GetString();
        _productId = DGrid.Rows[_rowIndex].Cells["GTxtProductId"].Value.GetLong();
        SetProductInfo(_productId);
        TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.GetString();
        _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
        _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUOMId"].Value.GetInt();
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
        _isRowUpdate = true;
    }

    private void SetProductInfo(long productId)
    {
        if (productId is 0) return;
        var dtLedger = _master.GetMasterProductList(_actionTag, productId);
        if (dtLedger.Rows.Count <= 0) return;
        TxtShortName.Text = dtLedger.Rows[0]["PShortName"].ToString();
        //LblAltQty.Text = dtLedger.Rows[0]["AltStockQty"].ToString();
        //LblQty.Text = dtLedger.Rows[0]["StockQty"].ToString();
        _altUnitId = dtLedger.Rows[0]["PAltUnit"].GetInt();
        _unitId = dtLedger.Rows[0]["PUnit"].GetInt();
        _conQty = dtLedger.Rows[0]["PQtyConv"].GetDecimal();
        TxtAltUnit.Text = dtLedger.Rows[0]["AltUnitCode"].GetString();
        TxtUnit.Text = dtLedger.Rows[0]["UnitCode"].GetString();
        TxtAltQty.Enabled = _altUnitId > 0;
        TxtRate.Text = dtLedger.Rows[0]["PBuyRate"].GetDecimalString();
        //LblBuyRate.Text = dtLedger.Rows[0]["PBuyRate"].GetDecimalString();
        //LblSalesRate.Text = dtLedger.Rows[0]["PSalesRate"].GetDecimalString();
    }

    private void GetSerialNo()
    {
        if (DGrid.RowCount <= 0) return;
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProduct"].Value.IsValueExits()) return;
        LblTotalAltQty.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal()).GetDecimalString();
        LblTotalQty.Text = DGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtQty"].Value.GetDecimal())
            .GetDecimalString();
        LblTotalBasicAmount.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtNetAmount"].Value.GetDecimal()).GetDecimalString();
        TxtBillTermAmount.Text = CalculateBillingTerm();
        LblTotalNetAmount.Text =
            (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = TxtCurrencyRate.GetDecimal() > 1
            ? (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal()).GetDecimalString()
            : LblTotalNetAmount.Text;
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
        _design.GetPurchaseEntryDesign(DGrid, "PC");
        DGrid.Columns["GTxtGodown"].Visible = ObjGlobal.StockGodownEnable;
        DGrid.Columns["GTxtTermAmount"].Visible = _isPTermExits;
        DGrid.Columns["GTxtNetAmount"].Visible = _isPTermExits;
        if (DGrid.Columns["GTxtGodown"].Visible)
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtGodown"].Width;
        DGrid.Columns["GTxtShortName"].Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
            DGrid.Columns["GTxtProduct"].Width -= DGrid.Columns["GTxtShortName"].Width;
        DGrid.GotFocus += (sender, e) => { DGrid.Rows[_rowIndex].Cells[0].Selected = true; };
        DGrid.RowEnter += (_, e) => _rowIndex = e.RowIndex;
        DGrid.CellEnter += (_, e) => _columnIndex = e.ColumnIndex;
        DGrid.KeyDown += DGrid_KeyDown;
        DGrid.EnterKeyPressed += DGrid_EnterKeyPressed;
        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtShortName = new MrGridTextBox(DGrid)
        {
            ReadOnly = !ObjGlobal.StockShortNameWise
        };
        TxtShortName.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                (TxtProduct.Text, _productId) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtProduct.Text, _productId) = GetMasterList.CreateProduct(true);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtProduct, OpenProductList);
            }
        };
        TxtProduct = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtProduct.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
            {
                OpenProductList();
            }
            else if (e.KeyCode is Keys.F2)
            {
                (TxtProduct.Text, _productId) = GetMasterList.GetCounterProduct(TxtShortName.Text);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtProduct.Text, _productId) = GetMasterList.CreateProduct(true);
                SetProductInfo(_productId);
                TxtProduct.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtProduct, OpenProductList);
            }
        };
        TxtProduct.Validating += (_, e) =>
        {
            if (DGrid.RowCount is 0)
            {
                this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
            }

            if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
                if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
                {
                    if (_getKeys.KeyChar is (char)Keys.Enter) _getKeys.Handled = false;
                    this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                    return;
                }

            if (DGrid.RowCount >= 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
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
        };
        TxtGodown = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtGodown.KeyDown += (_, e) =>
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
                var frm = new FrmGodownName(true);
                frm.ShowDialog();
                TxtGodown.Text = frm.GodownMaster;
                _godownId = frm.GodownId;
                TxtGodown.Focus();
            }
            else
            {
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtGodown, OpenGodownList);
            }
        };
        TxtAltQty = new MrGridNumericTextBox(DGrid);
        TxtAltQty.Validating += (sender, e) =>
        {
            if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0) TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
        };
        TxtAltQty.TextChanged += (sender, e) =>
        {
            if (!TxtAltQty.Focused) return;
            if (TxtAltQty.GetDecimal() > 0 && _conQty > 0)
                TxtQty.Text = (TxtAltQty.GetDecimal() * _conQty).GetDecimalQtyString();
            else if (TxtAltQty.GetDecimal() is 0) TxtQty.Text = 1.GetDecimalQtyString();
        };
        TxtAltUnit = new MrGridTextBox(DGrid);
        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += (sender, e) =>
        {
            TxtQty.Text = TxtQty.GetDecimalQtyString();
            if (!TxtQty.Enabled || !TxtProduct.Enabled) return;
            if (TxtQty.IsBlankOrEmpty())
            {
                this.NotifyValidationError(TxtQty, "PRODUCT OPENING QTY CANNOT BE ZERO");
            }
        };
        TxtQty.TextChanged += (sender, e) =>
        {
            if (!TxtQty.Focused) return;
            TxtBasicAmount.Text = TxtQty.Enabled && TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
            TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
        };
        TxtUnit = new MrGridTextBox(DGrid);
        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
        };
        TxtRate.TextChanged += (sender, e) =>
        {
            if (!TxtRate.Focused) return;
            TxtBasicAmount.Text = TxtRate.GetDecimal() > 0
                ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
            TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
        };
        TxtRate.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtQty) return;
            if (TxtTermAmount.Visible) return;
            if (!TxtProduct.Enabled || !TxtProduct.IsValueExits()) return;
            AddTextToGrid(_isRowUpdate);
            TxtProduct.Focus();
        };
        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += (sender, e) =>
        {
            if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
        };
        TxtBasicAmount.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtRate || TxtTermAmount.Enabled) return;
            if (TxtProduct.Enabled && TxtProduct.IsValueExits())
            {
                AddTextToGrid(_isRowUpdate);
                TxtProduct.Focus();
            }
        };
        TxtTermAmount = new MrGridNumericTextBox(DGrid);
        TxtTermAmount.Enter += (_, e) =>
        {
            if (!TxtProduct.IsValueExits() || !TxtProduct.Enabled) return;
            var existingTerm = new DataTable();
            var serialNo = 0;
            if (DGrid.CurrentRow != null)
            {
                serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
                var exDetails = _dtProductTerm.AsEnumerable().Any(c =>
                    c.Field<string>("ProductSno").Equals(serialNo.ToString()) &&
                    c.Field<string>("ProductId").Equals(_productId.ToString()));
                if (exDetails)
                    existingTerm = _dtProductTerm.Select($"ProductSno= '{serialNo}' and ProductId='{_productId}'")
                        .CopyToDataTable();
            }

            var result = new FrmTermCalculation(_actionTag == "SAVE" && !_isRowUpdate ? "SAVE" : "UPDATE",
                TxtBasicAmount.Text, true, "PC", _productId, serialNo, existingTerm, TxtQty.Text);
            result.ShowDialog();
            TxtTermAmount.Text = result.TotalTermAmount;
            AddToProductTerm(result.CalcTermTable);
            TxtNetAmount.Focus();
        };
        TxtTermAmount.TextChanged += (sender, e) =>
        {
            var basicAmount = TxtBasicAmount.GetDecimal();
            var termAmount = TxtTermAmount.GetDecimal();
            TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
        };
        TxtTermAmount.Validating += (sender, e) =>
        {
            if (ActiveControl == TxtBasicAmount) return;
            if (!TxtProduct.Enabled || !TxtProduct.IsValueExits()) return;
            AddTextToGrid(_isRowUpdate);
            TxtProduct.Focus();
        };
        TxtNetAmount = new MrGridNumericTextBox(DGrid);
        ObjGlobal.DGridColorCombo(DGrid);
        AdjustControlsInDataGrid();
    }

    private void AddToProductTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0) return;
        var serialNo = 0;
        if (DGrid.CurrentRow == null) return;
        serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
        var exDetails = _dtProductTerm.AsEnumerable().Any(c =>
            c.Field<string>("ProductSno").Equals(serialNo.ToString()) &&
            c.Field<string>("ProductId").Equals(_productId.ToString()));
        if (exDetails)
        {
            foreach (DataRow ro in dtTerm.Rows)
            foreach (DataRow row in _dtProductTerm.Rows)
                if (row["ProductSno"] == ro["GTxtProductSno"] && row["ProductId"] == ro["GTxtProductId"])
                {
                    var index = _dtProductTerm.Rows.IndexOf(row);
                    _dtProductTerm.Rows[index].SetField("TermRate", ro["GTxtRate"]);
                    _dtProductTerm.Rows[index].SetField("TermAmt", ro["GTxtValueAmount"]);
                    _dtProductTerm.AcceptChanges();
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
                dataRow["TermType"] = ro["GTxtTermCondition"];
                dataRow["TermRate"] = ro["GTxtRate"];
                dataRow["TermAmt"] = ro["GTxtValueAmount"];
                dataRow["Source"] = "PCR";
                dataRow["Formula"] = string.Empty;
                dataRow["ProductSno"] = ro["GTxtProductSno"];
                _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
            }
        }
    }

    private void AddToBillingTerm(DataTable dtTerm)
    {
        if (dtTerm.RowsCount() <= 0) return;
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
            dataRow["TermAmt"] = ro["GTxtValueAmount"];
            dataRow["Source"] = "PCR";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtBillTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }

    private void CashAndBankValidation(string ledgerType)
    {
        var partyInfo = new FrmPartyInfo(ledgerType, _dtPartyInfo, "PCR");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0)
        {
            _dtPartyInfo.Rows.Clear();
        }

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

    private void FillChallanReturnData(string voucherNo)
    {
        try
        {
            var dsChallan = _challanReturn.ReturnPurchaseChallanReturnDetailsInDataSet(voucherNo);
            if (dsChallan.Tables.Count <= 0) return;
            if (dsChallan.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow dr in dsChallan.Tables[0].Rows)
            {
                TxtVno.Text = dr["PCR_Invoice"].ToString();
                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtRefVno.Text = Convert.ToString(dr["PB_VNo"].ToString());

                if (dr["VNo_Date"].IsValueExits())
                {
                    MskRefDate.Text = dr["Vno_Miti"].ToString();
                }
                if (_actionTag != "SAVE")
                {
                    TxtIndentNo.Text = Convert.ToString(dr["QOT_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtIndentNo.Text.Trim()))
                        _mskIndentDate = dr["QOT_Date"].GetDateString();

                    TxtOrderNo.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrderNo.Text.Trim()))
                        _mskOrderDate = dr["PO_Date"].GetDateString();
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

            if (dsChallan.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsChallan.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dsChallan.Tables[1].Rows)
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
                    DGrid.Rows[iRows].Cells["GTxtIndentNo"].Value = dr["QOT_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtIndentSno"].Value = dr["QOT_Sno"].ToString();
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
            }

            if (dsChallan.Tables[2].Rows.Count > 0)
            {
                _dtProductTerm.Rows.Clear();
                var dtPTerm = dsChallan.Tables[2];
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
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                }
            }

            if (dsChallan.Tables[3].Rows.Count > 0)
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

    private void FillChallanData(string voucherNo)
    {
        try
        {
            var dsChallan = _challanReturn.ReturnPurchaseChallanDetailsInDataSet(voucherNo);
            if (dsChallan.Tables.Count <= 0)
            {
                return;
            }
            if (dsChallan.Tables[0].Rows.Count <= 0)
            {
                return;
            }

            foreach (DataRow dr in dsChallan.Tables[0].Rows)
            {
                TxtRefVno.Text = voucherNo;
                MskRefDate.Text = dr["Invoice_Miti"].ToString();
                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtRefVno.Text = Convert.ToString(dr["PB_VNo"].ToString());

                if (dr["VNo_Date"].IsValueExits()) MskRefDate.Text = dr["Vno_Miti"].ToString();
                if (_actionTag != "SAVE")
                {
                    TxtIndentNo.Text = Convert.ToString(dr["QOT_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtIndentNo.Text.Trim()))
                        _mskIndentDate = dr["QOT_Date"].GetDateString();

                    TxtOrderNo.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrderNo.Text.Trim()))
                        _mskOrderDate = dr["PO_Date"].GetDateString();
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

            if (dsChallan.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsChallan.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dsChallan.Tables[1].Rows)
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
                    DGrid.Rows[iRows].Cells["GTxtIndentNo"].Value = dr["PC_Invoice"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtIndentSno"].Value = dr["Invoice_SNo"].ToString();
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
            }

            if (dsChallan.Tables[2].Rows.Count > 0)
            {
                _dtProductTerm.Rows.Clear();
                var dtPTerm = dsChallan.Tables[2];
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
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                }
            }

            if (dsChallan.Tables[3].Rows.Count > 0)
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

    private void FillIndentData(string voucherNo)
    {
        try
        {
            var dsChallan = _challanReturn.ReturnPurchaseChallanDetailsInDataSet(voucherNo);
            if (dsChallan.Tables.Count <= 0) return;
            if (dsChallan.Tables[0].Rows.Count <= 0) return;
            foreach (DataRow dr in dsChallan.Tables[0].Rows)
            {
                MskMiti.Text = dr["Invoice_Miti"].ToString();
                MskDate.Text = dr["Invoice_Date"].GetDateString();
                TxtRefVno.Text = Convert.ToString(dr["PB_VNo"].ToString());

                if (dr["VNo_Date"].IsValueExits()) MskRefDate.Text = dr["Vno_Miti"].ToString();
                if (_actionTag != "SAVE")
                {
                    TxtIndentNo.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtIndentNo.Text.Trim()))
                        _mskIndentDate = dr["PO_Date"].GetDateString();

                    TxtOrderNo.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrderNo.Text.Trim()))
                        _mskOrderDate = dr["PC_Date"].GetDateString();
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

            if (dsChallan.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsChallan.Tables[1].Rows.Count + 1);
                foreach (DataRow dr in dsChallan.Tables[1].Rows)
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
                    DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
                    iRows++;
                }

                DGrid.ClearSelection();
            }

            if (dsChallan.Tables[2].Rows.Count > 0)
            {
                _dtProductTerm.Rows.Clear();
                var dtPTerm = dsChallan.Tables[2];
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
                    dataRow["TermRate"] = ro["TermRate"];
                    dataRow["TermAmt"] = ro["TermAmt"];
                    dataRow["Source"] = ro["Source"];
                    dataRow["Formula"] = ro["Formula"];
                    dataRow["ProductSno"] = ro["SNo"];
                    _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                }
            }

            if (dsChallan.Tables[3].Rows.Count > 0)
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
            var dsOrder = _challanReturn.ReturnPurchaseInvoiceDetailsInDataSet(voucherNo);
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
                    TxtIndentNo.Text = Convert.ToString(dr["PO_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtIndentNo.Text.Trim()))
                        _mskIndentDate = dr["PO_Date"].GetDateString();

                    TxtOrderNo.Text = Convert.ToString(dr["PC_Invoice"].ToString());
                    if (!string.IsNullOrEmpty(TxtOrderNo.Text.Trim()))
                        _mskOrderDate = dr["PC_Date"].GetDateString();
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
            if (dsSales.Tables.Count > 0)
                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsSales.Tables[0].Rows)
                    {
                        TxtVno.GetCurrentVoucherNo("PCR", _numberScheme);
                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());

                        if (dr["VNo_Date"].IsValueExits()) MskRefDate.Text = dr["Vno_Miti"].ToString();

                        if (_actionTag != "SAVE")
                        {
                            TxtIndentNo.Text = Convert.ToString(dr["SO_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtIndentNo.Text.Trim()))
                                _mskIndentDate = dr["SO_Date"].GetDateString();

                            TxtOrderNo.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                            if (!string.IsNullOrEmpty(TxtOrderNo.Text.Trim()))
                                _mskOrderDate = dr["SC_Date"].GetDateString();
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

                    if (dsSales.Tables[1].Rows.Count > 0)
                    {
                        var iRows = 0;
                        DGrid.Rows.Clear();
                        DGrid.Rows.Add(dsSales.Tables[1].Rows.Count + 1);
                        foreach (DataRow dr in dsSales.Tables[1].Rows)
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
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value =
                                dr["AltStock_Qty"].GetDecimalString();
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

                    if (dsSales.Tables[2].Rows.Count > 0)
                    {
                        _dtProductTerm.Rows.Clear();
                        var dtPTerm = dsSales.Tables[2];
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
                            dataRow["TermRate"] = ro["TermRate"];
                            dataRow["TermAmt"] = ro["TermAmt"];
                            dataRow["Source"] = ro["Source"];
                            dataRow["Formula"] = ro["Formula"];
                            dataRow["ProductSno"] = ro["SNo"];
                            _dtProductTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
                        }
                    }

                    if (dsSales.Tables[3].Rows.Count > 0)
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
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void DeletedRowExitsOrNot(int sno, long productId)
    {
        if (_dtProductTerm.RowsCount() <= 0) return;
        if (DGrid.CurrentRow == null) return;
        var exDetails = _dtProductTerm.AsEnumerable().Any(c =>
            c.Field<string>("ProductSno").Equals(sno.ToString()) &&
            c.Field<string>("ProductId").Equals(productId.ToString()));
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
        var list = new List<ValueModel<string, string>>
        {
            new("Local", "NORMAL"),
            new("Import", "IMPORT"),
            new("Assets", "ASSETS"),
            new("Abbreviation", "AVT"),
            new("Include VAT", "POS")
        };
        if (list.Count <= 0) return;
        CmbInvoiceType.DataSource = list;
        CmbInvoiceType.DisplayMember = "Item1";
        CmbInvoiceType.ValueMember = "Item2";
        CmbInvoiceType.SelectedIndex = 4;

        list.Clear();
        list =
        [
            new("Credit", "CREDIT"), new("Cash", "CASH"), new("Bank", "BANK"), new("Phone Pay", "PHONE PAY"),
            new("E-Sewa", "POS"), new("Khalti", "KHALTI"), new("Card", "CARD")
        ];
        if (list.Count <= 0) return;
        CmbPaymentMode.DataSource = list;
        CmbPaymentMode.DisplayMember = "Item1";
        CmbPaymentMode.ValueMember = "Item2";
        CmbPaymentMode.SelectedIndex = 0;
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
        if (ObjGlobal.PurchaseDescriptionsEnable)
            _description = GetMasterList.GetNarrationOfProduct(_actionTag, _description);
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
        DGrid.Rows[iRows].Cells["GTxtIndentNo"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtIndentSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = 0;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = 0;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxGroupId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxableAmount"].Value = string.Empty;
        var currentRow = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[currentRow].Cells[_columnIndex];
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
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) == DialogResult.No)
                return false;

        return true;
    }

    private string CalculateBillingTerm()
    {
        decimal termAmount = 0;
        var term = _master.GetTermCalculationForVoucher("PB");
        if (term.RowsCount() <= 0) return string.Empty;

        var exitsTerm = _dtBillTerm.Copy();
        _dtBillTerm.Rows.Clear();
        var iRows = 1;
        if (_dtBillTerm.Rows.Count is 0)
            foreach (DataRow ro in term.Rows)
            {
                decimal exitRate = 0;
                decimal exitAmount = 0;
                var termId = ro["TermId"].GetInt();
                var exDetails = exitsTerm.AsEnumerable()
                    .Any(c => c.Field<string>("TermId").Equals(termId.ToString()));
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
                row["TermRate"] = exitRate > 0 ? exitRate :
                    _actionTag.Equals("SAVE") ? ro["TermRate"].GetDecimalString() : 0.GetDecimalString();
                var termType = ro["TermType"].GetTrimReplace();
                row["TermType"] = ro["TermType"];
                row["TermCondition"] = ro["TermCondition"];
                decimal round = 0;
                if (termType.Equals("R"))
                {
                    var netAmount = LblTotalBasicAmount.GetDecimal() + termAmount;
                    var roundAmount = netAmount - Math.Truncate(netAmount);
                    if (roundAmount >= 0.5.GetDecimal() && ro["TermSign"].GetTrimReplace().Equals("+"))
                        round = 1 - roundAmount;

                    if (roundAmount < 0.5.GetDecimal() && ro["TermSign"].GetTrimReplace().Equals("-"))
                        round = roundAmount;
                }

                if (termType.Equals("R")) row["TermAmt"] = round.GetDecimal();
                else
                    row["TermAmt"] = exitAmount > 0
                        ? exitAmount
                        : termBasic.Equals("VALUE") && _actionTag.Equals("SAVE")
                            ? ro["TermRate"].GetDecimal() * LblTotalBasicAmount.GetDecimal() / 100
                            : _actionTag.Equals("SAVE")
                                ? ro["TermRate"].GetDecimal() * LblTotalQty.GetDecimal()
                                : 0.GetDecimalString();
                row["ProductId"] = 0;
                row["ProductSno"] = 0;
                _dtBillTerm.Rows.InsertAt(row, iRows);
                var amount = _dtBillTerm.Rows[iRows - 1]["TermAmt"].GetDecimal();
                if (_dtBillTerm.Rows[iRows - 1]["Sign"].Equals("-"))
                    termAmount -= amount;
                else
                    termAmount += amount;
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

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private readonly bool _isZoom;
    private readonly bool _isPTermExits;
    private readonly bool _isBTermExits;

    private string _actionTag = string.Empty;
    private string _mskIndentDate = string.Empty;
    private string _mskOrderDate = string.Empty;
    private string _numberScheme = string.Empty;
    private readonly string _txtZoomVno;
    private string _description = string.Empty;
    private readonly string _batchNo = string.Empty;
    private readonly string _mfgDate = string.Empty;
    private readonly string _expDate = string.Empty;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private decimal _conQty;

    private KeyPressEventArgs _getKeys;
    private readonly DataTable _dtPartyInfo;
    private readonly DataTable _dtProductTerm;
    private readonly DataTable _dtBillTerm;
    // private readonly IPurchaseEntry _entry = new ClsPurchaseEntry();
    private readonly IPurchaseChallanReturn _challanReturn;
    private readonly ISalesEntry _salesEntry = new ClsSalesEntry();
    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly IPurchaseDesign _design = new PurchaseEntryDesign();
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

    #endregion -------------- OBJECT --------------
}