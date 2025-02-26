using DatabaseModule.DataEntry.SalesMaster.SalesQuotation;
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
using MrDAL.DataEntry.Interface.SalesQuotation;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.dbMaster;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.SalesMaster;

public partial class FrmSalesQuotationEntry : MrForm
{
    //SALES ORDER ENTRY

    #region --------------- SALES ORDER ENTRY ---------------

    public FrmSalesQuotationEntry(bool zoom, string txtZoomVno, bool provisionVoucher = false, string invoiceType = "NORMAL")
    {
        InitializeComponent();
        _dtPartyInfo = _master.GetPartyInfo();
        _dtBillTerm = _master.GetBillingTerm();
        _dtProductTerm = _master.GetBillingTerm();
        _isProvision = provisionVoucher;
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _invoiceType = invoiceType;
        _isPTermExits = _master.IsBillingTermExitsOrNot("SQ", "P");
        _isBTermExits = _master.IsBillingTermExitsOrNot("SQ", "B");
        DesignGridColumnsAsync();
        EnableControl();
        ClearControl();
    }

    private void FrmSalesQuotationEntry_Load(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillOrderData(_txtZoomVno);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmSalesQuotationEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        _getKeys = e;
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }

        if (BtnNew.Enabled)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
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

    private void FrmSalesQuotationEntry_Shown(object sender, EventArgs e)
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
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        if (TxtVno.IsValueExits())
        {
            if (MskMiti.Enabled)
                MskMiti.Focus();
            else
                TxtRefVno.Focus();
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
        if (!_actionTag.Equals("SAVE") && _actionTag.IsValueExits() && _isZoom)
        {
            if (_txtZoomVno.IsValueExits())
            {
                FillOrderData(_txtZoomVno);
            }
        }

        TxtVno.Focus();
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        if (!_actionTag.Equals("SAVE") && _actionTag.IsValueExits() && _isZoom)
        {
            if (_txtZoomVno.IsValueExits())
            {
                FillOrderData(_txtZoomVno);
            }
        }

        TxtVno.Focus();
    }

    private void BtnPurchaseInvoice_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PB");
        FillPurchaseInvoiceDetails(voucherNo);
    }

    private void BtnPrintInvoice_Click(object sender, EventArgs e)
    {
        PrintVoucher();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ClearControl();
        EnableControl();
        if (!_actionTag.Equals("SAVE") && _actionTag.IsValueExits() && _isZoom)
        {
            if (_txtZoomVno.IsValueExits())
            {
                FillOrderData(_txtZoomVno);
            }
        }

        TxtVno.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SQ");
        FillOrderData(TxtVno.Text);
        MskMiti.Focus();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SQ");
        if (_actionTag == "SAVE") return;
        ClearControl();
        TxtVno.Text = voucherNo;
        FillOrderData(TxtVno.Text);
        TxtVno.Focus();
    }

    private void BtnVendor_Click(object sender, EventArgs e)
    {
        (TxtCustomer.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag, "CUSTOMER");
        SetLedgerInfo(_ledgerId);
        TxtCustomer.Focus();
    }

    private void BtnCurrency_Click(object sender, EventArgs e)
    {
        (TxtCurrencyRate.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
        TxtCurrencyRate.Focus();
    }

    private void BtnSubLedger_Click(object sender, EventArgs e)
    {
        (TxtSubledger.Text, _subLedgerId) = GetMasterList.GetSubLedgerList(_actionTag);
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
        if (!TxtBillTermAmount.Enabled || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0) return;
        var result = new FrmTermCalculation(_dtBillTerm.RowsCount() > 0 ? "UPDATE" : "SAVE", LblTotalBasicAmount.Text, false, "SQ", 0, 0, _dtBillTerm);
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

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.SalesRemarksMandatory && TxtRemarks.IsValueExits())
            {
                TxtRemarks.WarningMessage("REMARKS IS REQUIRED FOR INVOICE..!!");
            }
            else
            {
                BtnSave.Focus();
            }
        }
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
            CreateDatabaseTable.DropTrigger();
            if (SaveSalesQuotation() != 0)
            {
                if (!_actionTag.Equals("DELETE"))
                {
                    PrintVoucher();
                }
                if (_isZoom)
                {
                    Close();
                    return;
                }
                this.NotifySuccess($@"{TxtVno.Text} SALES INVOICE NUMBER {_actionTag} SUCCESSFULLY..!!");
                ClearControl();
                CreateDatabaseTable.CreateTrigger();
                if (_actionTag != "SAVE")
                {
                    TxtVno.Enabled = true;
                    TxtVno.Focus();
                }
                else if (TxtVno.IsValueExits())
                {
                    if (MskMiti.Enabled)
                        MskMiti.Focus();
                    else
                        TxtRefVno.Focus();
                }
                else
                {
                    TxtVno.Focus();
                }
            }
            else
            {
                this.NotifyError($@"{TxtVno.Text} ERROR OCCURS WHILE SALES INVOICE {_actionTag}..!!");
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
            SendKeys.Send("{TAB}");
        }
        else if (TxtVno.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
        }
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        var dtVoucher = _entry.CheckVoucherNoExitsOrNot("AMS.SB_Master", "SB_Invoice", TxtVno.Text);
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
        if (MskMiti.MaskCompleted && !MskMiti.IsDateExits("M") ||
            !MskMiti.MaskCompleted && MskMiti.Enabled && TxtVno.IsValueExits())
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

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskDate.MaskCompleted && !MskDate.IsDateExits("D") ||
            !MskDate.MaskCompleted && MskDate.Enabled && TxtVno.IsValueExits())
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

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtRefVno.IsValueExits())
            {
                SendKeys.Send("{TAB}");
            }
            else
            {
                TxtCustomer.Focus();
            }
        }
    }

    private void MskRefDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            TxtCustomer.Focus();
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
            if (TxtCustomer.IsBlankOrEmpty())
            {
                BtnVendor.PerformClick();
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("CUSTOMER", true);
            if (description.IsValueExits())
            {
                TxtCustomer.Text = description;
                _ledgerId = id;
                SetLedgerInfo(_ledgerId);
            }
            TxtCustomer.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCustomer, BtnVendor);
        }
    }

    private void TxtDueDays_TextChanged(object sender, EventArgs e)
    {
        var datetime = MskDate.GetDateTime();
        datetime = datetime.AddDays(TxtDueDays.GetInt());
        MskDueDays.Text = datetime.GetDateString();
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
            if (TxtCurrency.IsBlankOrEmpty() && ObjGlobal.SalesCurrencyMandatory)
            {
                this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY PLEASE SELECT CURRENCY");
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
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void TxtSubledger_KeyDown(object sender, KeyEventArgs e)
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
            if (TxtSubledger.IsBlankOrEmpty() && ObjGlobal.SalesSubLedgerMandatory)
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
            BtnSubLedger_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtDepartment.Text, _departmentId) = GetMasterList.CreateDepartment(true);
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.SalesDepartmentMandatory)
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
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.SalesAgentMandatory)
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
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAgent, BtnAgent);
        }
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No)
            {
                return;
            }

            _rowDelete = true;
            if (DGrid.CurrentRow != null)
            {
                var sno = DGrid.CurrentRow.Cells["GTxtSNo"].Value.GetInt();
                var productId = DGrid.CurrentRow.Cells["GTxtProductId"].Value.GetLong();
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

    private void TxtBillTermAmount_Enter(object sender, EventArgs e)
    {
        if (_actionTag.IsValueExits() && TxtBillTermAmount.Enabled)
        {
            BtnBillingTerm_Click(sender, e);
        }
    }

    private void TxtBillTermAmount_TextChanged(object sender, EventArgs e)
    {
        LblTotalNetAmount.Text =
            (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = TxtCurrencyRate.GetDecimal() > 1
            ? (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal()).GetDecimalString()
            : LblTotalNetAmount.Text;
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalNetAmount.Text);
    }

    private void TxtBillTermAmount_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtRemarks.Enabled)
                TxtRemarks.Focus();
            else
                BtnSave.Focus();
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

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
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
            PAttachment1.ImageLocation = fileName;
            var bitmap = new Bitmap(fileName);
            PAttachment1.Image = bitmap;
            PAttachment1.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment1.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty) MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
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
            var fileName = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment2.ImageLocation = fileName;
            var bitmap = new Bitmap(fileName);
            PAttachment2.Image = bitmap;
            PAttachment2.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment2.Text = Path.GetFileName(fileName);
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
            PAttachment3.ImageLocation = fileName;
            var bitmap = new Bitmap(fileName);
            PAttachment3.Image = bitmap;
            PAttachment3.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment3.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty) MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment3);
    }

    private void BtnAttachment4_Click(object sender, EventArgs e)
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
            PAttachment4.ImageLocation = fileName;
            var bitmap = new Bitmap(fileName);
            PAttachment4.Image = bitmap;
            PAttachment4.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment4.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty) MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment4);
    }

    private void BtnAttachment5_Click(object sender, EventArgs e)
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
            PAttachment5.ImageLocation = fileName;
            var bitmap = new Bitmap(fileName);
            PAttachment5.Image = bitmap;
            PAttachment5.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment5.Text = Path.GetFileName(fileName);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty) MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
        }
    }

    private void LinkAttachment5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment5);
    }

    // GRID CONTROL EVENTS
    private void OnDGridOnEnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
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

    private void OnTxtTermAmountOnValidating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtBasicAmount) return;
        if (!TxtProduct.Enabled || !TxtProduct.IsValueExits()) return;
        AddTextToGrid(_isRowUpdate);
        TxtProduct.Focus();
    }

    private void OnTxtTermAmountOnTextChanged(object sender, EventArgs e)
    {
        var basicAmount = TxtBasicAmount.GetDecimal();
        var termAmount = TxtTermAmount.GetDecimal();
        TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
    }

    private void OnTxtTermAmountOnEnter(object _, EventArgs e)
    {
        if (!TxtProduct.IsValueExits() || !TxtProduct.Enabled) return;
        var existingTerm = new DataTable();
        var serialNo = 0;

        if (DGrid.CurrentRow != null)
        {
            serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
            var exDetails = _dtProductTerm.AsEnumerable()
                .Any(c => c.Field<string>("ProductId").Equals(_productId.ToString().Trim()));
            if (exDetails)
            {
                var exitAny = _dtProductTerm.AsEnumerable().Any(c =>
                    c.Field<string>("ProductId").Equals(_productId.ToString()) &&
                    c.Field<string>("ProductSno").Equals(serialNo.GetString()));
                if (exitAny)
                {
                    existingTerm = _dtProductTerm.Select($"ProductId = '{_productId}'").CopyToDataTable();
                    if (existingTerm.Rows.Count is 0)
                    {
                        existingTerm = _dtProductTerm.Select($" ProductId = '{_productId}'").CopyToDataTable();
                    }
                    else if (existingTerm.Rows.Count > 1)
                    {
                        var any = _dtProductTerm.AsEnumerable().Any(c =>
                            c.Field<string>("ProductId").Equals(_productId.ToString()) &&
                            c.Field<string>("ProductSno").Equals(serialNo.GetString()));
                        if (any)
                        {
                            existingTerm = _dtProductTerm
                                .Select($"ProductSno = '{serialNo}' and ProductId = '{_productId}'")
                                .CopyToDataTable();
                        }
                    }
                }
            }
        }

        var tag = _actionTag == "SAVE" && !_isRowUpdate ? "SAVE" : "UPDATE";
        var result = new FrmTermCalculation(tag, TxtBasicAmount.Text, true, "SQ", _productId, serialNo,
            existingTerm, TxtQty.Text);
        result.ShowDialog();
        TxtTermAmount.Text = result.TotalTermAmount;
        var basicAmount = TxtBasicAmount.GetDecimal();
        var termAmount = TxtTermAmount.GetDecimal();
        TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
        AddToProductTerm(result.CalcTermTable);
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() + TxtTermAmount.GetDecimal()).GetDecimalString();
        TxtNetAmount.Focus();
    }

    private void OnTxtBasicAmountOnValidating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtRate || TxtTermAmount.Enabled) return;
        OnTxtBasicAmountOnTextChanged(sender, e);
        if (TxtProduct.Enabled && TxtProduct.IsValueExits())
        {
            AddTextToGrid(_isRowUpdate);
            TxtProduct.Focus();
        }
    }

    private void OnTxtBasicAmountOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtBasicAmount.Enabled)
        {
            return;
        }

        TxtRate.Text = TxtBasicAmount.GetDecimal() is 0
            ? 0.GetDecimalString()
            : (TxtBasicAmount.GetDecimal() / TxtQty.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void OnTxtBasicAmountOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
    }

    private void OnTxtRateOnValidating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtQty) return;
        if (TxtTermAmount.Enabled) return;
        if (!TxtProduct.Enabled || !TxtProduct.IsValueExits()) return;
        if (TxtBasicAmount.Enabled) return;
        AddTextToGrid(_isRowUpdate);
        TxtProduct.Focus();
    }

    private void OnTxtRateOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtRate.Focused) return;
        TxtBasicAmount.Text = TxtRate.GetDecimal() > 0
            ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
            : 0.GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void OnTxtRateOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
    }

    private void TxtQty_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F2 && DGrid.CurrentRow != null)
        {
            _freeQty = DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtFreeQty"].Value.GetDecimal();
            var result = new FrmFreeQty(_freeQty);
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {
                _freeQty = result.FreeQty;
            }
        }
    }

    private void OnTxtQtyOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtQty.Focused) return;
        TxtBasicAmount.Text = TxtQty.Enabled && TxtRate.GetDecimal() > 0
            ? (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString()
            : 0.GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void OnTxtQtyOnValidated(object sender, EventArgs e)
    {
        TxtQty.Text = TxtQty.GetDecimalQtyString();
        OnTxtQtyOnTextChanged(sender, e);
        if (!TxtQty.Enabled || !TxtProduct.Enabled)
        {
            return;
        }

        if (TxtQty.IsBlankOrEmpty())
        {
            TxtQty.WarningMessage("PRODUCT OPENING QTY CANNOT BE ZERO");
            return;
        }
    }

    private void OnTxtAltQtyOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtAltQty.Focused) return;
        if (TxtAltQty.GetDecimal() > 0 && _conQty > 0)
            TxtQty.Text = (TxtAltQty.GetDecimal() * _conQty).GetDecimalQtyString();
        else if (TxtAltQty.GetDecimal() is 0) TxtQty.Text = 1.GetDecimalQtyString();
    }

    private void OnTxtAltQtyOnValidating(object sender, CancelEventArgs e)
    {
        if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0) TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
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
    }

    private void OnTxtProductOnValidating(object _, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0) this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
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
                TxtBillTermAmount.Focus();
            else if (TxtRemarks.Enabled)
                TxtRemarks.Focus();
            else
                BtnSave.Focus();
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
    }

    private void OnTxtShortNameOnKeyDown(object _, KeyEventArgs e)
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
    }

    #endregion --------------- SALES ORDER ENTRY ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private int SaveSalesQuotation()
    {
        try
        {
            if (_actionTag is "SAVE" && TxtVno.IsDuplicate("SQ", _actionTag))
            {
                TxtVno.Text = TxtVno.GetCurrentVoucherNo("SQ", _numberScheme);
            }
            _salesQuotation.SqMaster.SQ_Invoice = TxtVno.Text;
            _salesQuotation.SqMaster.Invoice_Date = DateTime.Parse(MskDate.Text);
            _salesQuotation.SqMaster.Invoice_Miti = MskMiti.Text;
            _salesQuotation.SqMaster.Ref_Vno = TxtRefVno.Text;
            _salesQuotation.SqMaster.Ref_VMiti = MskRefDate.Text;
            _salesQuotation.SqMaster.Ref_VDate = MskRefDate.GetEnglishDate(MskRefDate.Text).GetDateTime();
            _salesQuotation.SqMaster.Invoice_Time = DateTime.Now;
            _salesQuotation.SqMaster.Expiry_Date = DateTime.Now.AddMonths(1);
            _salesQuotation.SqMaster.Customer_Id = _ledgerId;

            _salesQuotation.SqMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
            _salesQuotation.SqMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
            _salesQuotation.SqMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
            _salesQuotation.SqMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
            _salesQuotation.SqMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
            _salesQuotation.SqMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
            _salesQuotation.SqMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? _dtPartyInfo.Rows[0]["ChequeDate"].GetDateTime() : DateTime.Now;

            _salesQuotation.SqMaster.DueDays = TxtDueDays.Text.GetInt();
            _salesQuotation.SqMaster.DueDate = MskDueDays.MaskCompleted ? MskDueDays.GetEnglishDate(MskDueDays.Text).GetDateTime() : DateTime.Now;
            _salesQuotation.SqMaster.Agent_Id = _agentId;
            _salesQuotation.SqMaster.Subledger_Id = _subLedgerId;
            _salesQuotation.SqMaster.Cls1 = _departmentId;
            _salesQuotation.SqMaster.Cls2 = 0;
            _salesQuotation.SqMaster.Cls3 = 0;
            _salesQuotation.SqMaster.Cls4 = 0;
            _salesQuotation.SqMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _salesQuotation.SqMaster.Cur_Rate = TxtCurrencyRate.Text.GetDecimal() > 0 ? TxtCurrencyRate.Text.GetDecimal() : 1;
            _salesQuotation.SqMaster.B_Amount = LblTotalBasicAmount.Text.GetDecimal();
            _salesQuotation.SqMaster.T_Amount = TxtBillTermAmount.Text.GetDecimal();
            _salesQuotation.SqMaster.N_Amount = LblTotalNetAmount.Text.GetDecimal();
            _salesQuotation.SqMaster.LN_Amount = LblTotalLocalNetAmount.Text.GetDecimal();
            _salesQuotation.SqMaster.Tender_Amount = 0;
            _salesQuotation.SqMaster.Return_Amount = 0;
            _salesQuotation.SqMaster.V_Amount = 0;
            _salesQuotation.SqMaster.Tbl_Amount = 0;
            _salesQuotation.SqMaster.Action_Type = _actionTag;
            _salesQuotation.SqMaster.R_Invoice = false;
            _salesQuotation.SqMaster.No_Print = 0;
            _salesQuotation.SqMaster.In_Words = LblNumberInWords.Text;
            _salesQuotation.SqMaster.Remarks = TxtRemarks.Text;
            _salesQuotation.SqMaster.Audit_Lock = false;
            _salesQuotation.SqMaster.CBranch_Id = ObjGlobal.SysBranchId;

            _salesQuotation.SqMaster.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            _salesQuotation.SqMaster.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            if (ObjGlobal.LocalOriginId != null)
            {
                _salesQuotation.SqMaster.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                    ? ObjGlobal.LocalOriginId.Value
                    : Guid.Empty;
            }

            _salesQuotation.SqMaster.SyncCreatedOn = DateTime.Now;
            _salesQuotation.SqMaster.SyncLastPatchedOn = DateTime.Now;

            var sync = _salesQuotation.ReturnSyncRowVersionVoucher("SQ", TxtVno.Text);
            _salesQuotation.SqMaster.SyncRowVersion = sync;

            // SALES QUOTATION DETAILS
            _salesQuotation.DetailsList.Clear();

            if (DGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in DGrid.Rows)
                {
                    var list = new SQ_Details();
                    if (row.Cells["GTxtProductId"].Value.GetLong() is 0)
                    {
                        continue;
                    }
                    list.SQ_Invoice = TxtVno.Text;
                    list.Invoice_SNo = row.Cells["GTxtSno"].Value.GetInt();
                    list.P_Id = row.Cells["GTxtProductId"].Value.GetLong();
                    list.Gdn_Id = row.Cells["GTxtGodownId"].Value.GetInt();
                    list.Alt_Qty = row.Cells["GTxtAltQty"].Value.GetDecimal();
                    list.Alt_UnitId = row.Cells["GTxtAltUOMId"].Value.GetInt();
                    list.Qty = row.Cells["GTxtQty"].Value.GetDecimal();
                    list.Unit_Id = row.Cells["GTxtUOMId"].Value.GetInt();
                    list.Rate = row.Cells["GTxtRate"].Value.GetDecimal();
                    list.B_Amount = row.Cells["GTxtAmount"].Value.GetDecimal();
                    list.T_Amount = row.Cells["GTxtTermAmount"].Value.GetDecimal();
                    list.N_Amount = row.Cells["GTxtNetAmount"].Value.GetDecimal();
                    list.AltStock_Qty = row.Cells["GTxtAltStockQty"].Value.GetDecimal();
                    list.Stock_Qty = row.Cells["GTxtStockQty"].Value.GetDecimal();
                    list.Narration = row.Cells["GTxtNarration"].Value.GetString();
                    list.Tax_Amount = list.V_Amount = list.V_Rate = 0;
                    list.Free_Unit_Id = 0;
                    list.Free_Qty = list.StockFree_Qty = 0;
                    list.ExtraFree_Unit_Id = 0;
                    list.ExtraFree_Qty = 0;
                    list.ExtraStockFree_Qty = 0;
                    list.T_Product = row.Cells["IsTaxable"].Value.GetBool();
                    list.S_Ledger = row.Cells["GTxtSBLedgerId"].Value.GetLong();
                    list.SR_Ledger = row.Cells["GTxtSRLedgerId"].Value.GetLong();
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

                    _salesQuotation.DetailsList.Add(list);
                }
            }

            // SALES PRODUCT TERM
            _salesQuotation.Terms.Clear();
            foreach (DataRow row in _dtProductTerm.Rows)
            {
                var list = new SQ_Term()
                {
                    SQ_Vno = TxtVno.Text,
                    ST_Id = row["TermId"].GetInt(),
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
                _salesQuotation.Terms.Add(list);
            }

            // SALES BILL TERM
            foreach (DataRow row in _dtBillTerm.Rows)
            {
                var list = new SQ_Term
                {
                    SQ_Vno = TxtVno.Text,
                    ST_Id = row["TermId"].GetInt(),
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
                _salesQuotation.Terms.Add(list);
            }

            return _salesQuotation.SaveSalesQuotation(_actionTag);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return 0;
        }
    }
    private void ReturnVoucherNo()
    {
        var dt = _master.IsExitsCheckDocumentNumbering("SQ");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("SQ", _numberScheme);
            if (ObjGlobal.IsIrdApproved.Equals("YES")) TxtVno.Enabled = false;
        }
        else if (dt.Rows.Count > 1)
        {
            var wnd = new FrmNumberingScheme("SQ");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _numberScheme = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"SALES QUOTATION ENTRY [{_actionTag}]"
            : "SALES QUOTATION DETAILS";
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVno.Clear();
        if (_actionTag.Equals("SAVE")) TxtVno.GetCurrentVoucherNo("SQ", _numberScheme);
        if (BtnNew.Enabled)
        {
            MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            MskMiti.Text = DateTime.Now.GetNepaliDate();
            MskRefDate.Text = ObjGlobal.SysDateType == "M" ? MskRefDate.GetNepaliDate(MskDate.Text) : MskDate.Text;
        }

        _ledgerId = _agentId = _subLedgerId = _departmentId = _departmentId = _currencyId = 0;
        ledgerType = string.Empty;
        TxtRefVno.Clear();
        TxtDueDays.Clear();
        TxtCustomer.Clear();
        TxtDepartment.Clear();
        TxtAgent.Clear();
        TxtSubledger.Clear();
        TxtCurrencyRate.Clear();
        TxtCustomer.Clear();
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
        _freeQty = 0;
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
        BtnNew.Enabled = !_tagStrings.Contains(_actionTag) && !isEnable;
        BtnEdit.Enabled = BtnDelete.Enabled = BtnReverse.Enabled = BtnNew.Enabled;

        BtnEdit.Visible = BtnDelete.Visible = !ObjGlobal.IsIrdApproved.Equals("YES") || isEnable;
        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskDate.Enabled = MskMiti.Enabled = isEnable && !ObjGlobal.IsIrdApproved.Equals("YES");
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtCustomer.Enabled = BtnVendor.Enabled = isEnable;
        MskDueDays.Enabled = false;
        TxtDueDays.Enabled = isEnable;
        TxtCurrency.Enabled = BtnCurrency.Enabled = isEnable && ObjGlobal.SalesCurrencyEnable;
        TxtCurrencyRate.Enabled = TxtCurrency.Enabled;
        TxtSubledger.Enabled = BtnSubledger.Enabled = isEnable && ObjGlobal.SalesSubLedgerEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.SalesDepartmentEnable;
        TxtAgent.Enabled = BtnAgent.Enabled = isEnable && ObjGlobal.SalesAgentEnable;
        TxtBillTermAmount.Enabled = BtnBillingTerm.Enabled = isEnable && _isBTermExits;
        TabLedgerOpening.Enabled = isEnable;
        TxtRemarks.Enabled = isEnable && ObjGlobal.SalesRemarksEnable || _tagStrings.Contains(_actionTag);
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

        TxtBasicAmount.Enabled = isEnable && ObjGlobal.SalesBasicAmountEnable;
        TxtBasicAmount.Visible = isEnable;

        TxtTermAmount.Enabled = TxtTermAmount.Visible = isEnable && _isPTermExits;
        TxtNetAmount.Enabled = false;
        TxtNetAmount.Visible = isEnable && _isPTermExits;
        //DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }

    private void SetLedgerInfo(long ledgerId)
    {
        if (ledgerId is 0) return;
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
    }

    private void OpenProductList()
    {
        (TxtProduct.Text, _productId) = GetMasterList.GetProduct(_actionTag, MskDate.Text);
        SetProductInfo(_productId);
        TxtProduct.Focus();
    }

    private void OpenGodownList()
    {
        (TxtGodown.Text, _godownId) = GetMasterList.GetGodown(_actionTag);
        TxtGodown.Focus();
    }

    private void PrintVoucher()
    {
        var dtDesign = _master.GetPrintVoucherList("SQ");
        if (dtDesign.Rows.Count > 0)
        {
            var frmName = dtDesign.Rows[0]["DesignerPaper_Name"].ToString();
            var frmDp = new FrmDocumentPrint(frmName.IsValueExits() ? frmName : "Crystal", "SQ", TxtVno.Text, TxtVno.Text, true)
            {
                Owner = ActiveForm
            };
            frmDp.ShowDialog();
        }
    }

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
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
        if (productId is 0)
        {
            return;
        }
        var dtLedger = _master.GetMasterProductList(_actionTag, productId);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }

        TxtShortName.Text = dtLedger.Rows[0]["PShortName"].ToString();
        _altUnitId = dtLedger.Rows[0]["PAltUnit"].GetInt();
        _unitId = dtLedger.Rows[0]["PUnit"].GetInt();
        _conQty = dtLedger.Rows[0]["PQtyConv"].GetDecimal();
        TxtAltUnit.Text = dtLedger.Rows[0]["AltUnitCode"].GetString();
        TxtUnit.Text = dtLedger.Rows[0]["UnitCode"].GetString();
        TxtAltQty.Enabled = _altUnitId > 0;
        TxtRate.Text = dtLedger.Rows[0]["PSalesRate"].GetDecimalString();
    }

    private void AddTextToGrid(bool isUpdate)
    {
        _description = ObjGlobal.SalesDescriptionsEnable switch
        {
            true => GetMasterList.GetNarrationOfProduct(_actionTag, _description),
            _ => _description
        };
        if (TxtProduct.IsBlankOrEmpty() || _productId is 0)
        {
            this.NotifyValidationError(TxtProduct, "SELECTED PRODUCT IS INVALID..!!");
            return;
        }

        if (ObjGlobal.SalesGodownEnable && TxtGodown.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtGodown, "GODOWN IS MANDATORY..!!");
            return;
        }

        if (TxtQty.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtQty, "PRODUCT QTY CANNOT BE ZERO..!!");
            return;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtShortName"].Value = TxtShortName.Text;
        DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _productId.ToString();
        DGrid.Rows[iRows].Cells["GTxtProduct"].Value = TxtProduct.Text;
        DGrid.Rows[iRows].Cells["GTxtGodownId"].Value = _godownId.ToString();
        DGrid.Rows[iRows].Cells["GTxtGodown"].Value = TxtGodown.Text;
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = _altUnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId;
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.Text.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBasicAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = TxtTermAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = TxtNetAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = TxtAltQty.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = TxtQty.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = _description;
        //DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = string.Empty;
        //DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = string.Empty;
        //DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = string.Empty;
        //DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = _freeQty;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = _freeQty;
        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraStockQty"].Value = string.Empty;
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
            VoucherTotalCalculation();
            DGrid.Focus();
            return;
        }

        ClearProductDetails();
        VoucherTotalCalculation();
        TxtProduct.AcceptsTab = false;
        GetSerialNo();
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
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProductId"].Value.IsValueExits())
        {
            return;
        }
        var execute = DGrid.Rows.OfType<DataGridViewRow>();
        var rows = execute as DataGridViewRow[] ?? execute.ToArray();

        var totalAltQty = rows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal());
        var totalQty = rows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal());
        var totalBasicAmount = rows.Sum(row => row.Cells["GTxtNetAmount"].Value.GetDecimal());

        LblTotalAltQty.Text = totalAltQty.GetDecimalString();
        LblTotalQty.Text = totalQty.GetDecimalString();
        LblTotalBasicAmount.Text = totalBasicAmount.GetDecimalString();

        TxtBillTermAmount.Text = CalculateBillingTerm();
        LblTotalNetAmount.Text = (LblTotalBasicAmount.GetDecimal() + TxtBillTermAmount.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = TxtCurrencyRate.GetDecimal() > 1
            ? (LblTotalNetAmount.GetDecimal() * TxtCurrencyRate.GetDecimal()).GetDecimalString()
            : LblTotalNetAmount.Text;
        LblNumberInWords.Text = ClsMoneyConversion.MoneyConversion(LblTotalLocalNetAmount.Text);
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;
        var currentRow = _rowIndex;

        var columnIndex = DGrid.Columns["GTxtSNo"]!.Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtShortName"]!.Index;
        TxtShortName.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtShortName.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtShortName.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtProduct"]!.Index;
        TxtProduct.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtProduct.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtProduct.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtGodown"]!.Index;
        TxtGodown.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtGodown.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtGodown.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltQty"]!.Index;
        TxtAltQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAltUOM"]!.Index;
        TxtAltUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAltUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAltUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtQty"]!.Index;
        TxtQty.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtUOM"]!.Index;
        TxtUnit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtUnit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtUnit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRate"]!.Index;
        TxtRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"]!.Index;
        TxtBasicAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBasicAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBasicAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtTermAmount"]!.Index;
        TxtTermAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtTermAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtTermAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtNetAmount"]!.Index;
        TxtNetAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNetAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNetAmount.TabIndex = columnIndex;
    }

    private void DesignGridColumnsAsync()
    {
        _design.GetSalesEntryGridDesign(DGrid, "SQ");
        DGrid.Columns["GTxtGodown"]!.Visible = ObjGlobal.StockGodownEnable;
        DGrid.Columns["GTxtTermAmount"]!.Visible = _isPTermExits;
        DGrid.Columns["GTxtNetAmount"]!.Visible = _isPTermExits;
        if (DGrid.Columns["GTxtGodown"].Visible)
        {
            DGrid.Columns["GTxtProduct"]!.Width -= DGrid.Columns["GTxtGodown"].Width;
        }

        DGrid.Columns["GTxtShortName"]!.Visible = ObjGlobal.StockShortNameWise;
        if (DGrid.Columns["GTxtShortName"].Visible)
        {
            DGrid.Columns["GTxtProduct"]!.Width -= DGrid.Columns["GTxtShortName"].Width;
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
        TxtAltQty.Validating += OnTxtAltQtyOnValidating;
        TxtAltQty.TextChanged += OnTxtAltQtyOnTextChanged;

        TxtAltUnit = new MrGridTextBox(DGrid);

        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += OnTxtQtyOnValidated;
        TxtQty.KeyDown += TxtQty_KeyDown;
        TxtQty.TextChanged += OnTxtQtyOnTextChanged;

        TxtUnit = new MrGridTextBox(DGrid);

        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += OnTxtRateOnKeyDown;
        TxtRate.TextChanged += OnTxtRateOnTextChanged;
        TxtRate.Validating += OnTxtRateOnValidating;

        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += OnTxtBasicAmountOnKeyDown;
        TxtBasicAmount.TextChanged += OnTxtBasicAmountOnTextChanged;
        TxtBasicAmount.Validating += OnTxtBasicAmountOnValidating;

        TxtTermAmount = new MrGridNumericTextBox(DGrid);
        TxtTermAmount.Enter += OnTxtTermAmountOnEnter;
        TxtTermAmount.TextChanged += OnTxtTermAmountOnTextChanged;
        TxtTermAmount.Validating += OnTxtTermAmountOnValidating;
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
        var exDetails = _dtProductTerm.AsEnumerable()
            .Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
        if (exDetails)
        {
            var exitAny = _dtProductTerm.AsEnumerable().Any(c =>
                c.Field<string>("ProductId").Equals(_productId.ToString()) &&
                c.Field<string>("ProductSno").Equals(serialNo.ToString().Trim()));
            if (exitAny)
            {
                foreach (DataRow ro in dtTerm.Rows)
                {
                    foreach (DataRow row in _dtProductTerm.Rows)
                    {
                        if (Equals(row["ProductSno"], ro["GTxtProductSno"]) &&
                            Equals(row["ProductId"], ro["GTxtProductId"]))
                        {
                            var index = _dtProductTerm.Rows.IndexOf(row);
                            _dtProductTerm.Rows[index].SetField("TermRate", ro["GTxtRate"]);
                            _dtProductTerm.Rows[index].SetField("TermAmt", ro["GTxtAmount"]);
                            _dtProductTerm.AcceptChanges();
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
                    dataRow["Source"] = "SQ";
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
                dataRow["Source"] = "SQ";
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
            dataRow["TermAmt"] = ro["GTxtAmount"];
            dataRow["Source"] = "SQ";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtBillTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }

    private void CashAndBankValidation(string type)
    {
        var partyInfo = new FrmPartyInfo(type, _dtPartyInfo, "SQ");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0) _dtPartyInfo.Rows.Clear();
        _dtPartyInfo = partyInfo.PartyInfo;
    }

    private void FillOrderData(string voucherNo)
    {
        try
        {
            var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
            var dtMaster = dsSales.Tables[0];
            var dtDetails = dsSales.Tables[1];
            var dtProductTerm = dsSales.Tables[2];
            var dtBillTerm = dsSales.Tables[3];
            var dtTransport = dsSales.Tables[4];

            if (dsSales.Tables.Count > 0)
            {
                foreach (DataRow dr in dtMaster.Rows)
                {
                    if (!_actionTag.Equals("SAVE") && _isZoom) TxtVno.Text = _txtZoomVno;

                    MskMiti.Text = dr["Invoice_Miti"].ToString();
                    MskDate.Text = dr["Invoice_Date"].GetDateString();
                    if (dr["PB_Vno"].ToString() != string.Empty)
                    {
                        TxtRefVno.Text = Convert.ToString(dr["PB_Vno"].ToString());
                        MskRefDate.Text = dr["Vno_Miti"].ToString();
                    }
                    TxtCustomer.Text = dr["GLName"].GetString();
                    _ledgerId = dr["Customer_ID"].GetLong();
                    TxtDueDays.Text = dr["DueDays"].ToString();
                    MskDueDays.Text = dr["DueDate"].GetDateString();

                    _dtPartyInfo.Rows.Clear();
                    var dataRow = _dtPartyInfo.NewRow();
                    dataRow["PartyLedgerId"] = null;
                    dataRow["PartyName"] = dr["Party_Name"];
                    dataRow["ChequeNo"] = dr["ChqNo"];
                    dataRow["ChequeDate"] = dr["ChqDate"].GetDateString();
                    dataRow["VatNo"] = dr["Vat_No"];
                    dataRow["ContactPerson"] = dr["Contact_Person"];
                    dataRow["Address"] = dr["Address"];
                    dataRow["Mob"] = dr["Mobile_No"];
                    _dtPartyInfo.Rows.Add(dataRow);

                    TxtSubledger.Text = dr["SlName"].ToString();
                    TxtAgent.Text = dr["AgentName"].ToString();
                    if (!string.IsNullOrEmpty(dr["DName"].ToString()))
                    {
                        TxtDepartment.Text = dr["DName"].ToString();
                        _departmentId = ObjGlobal.ReturnInt(dr["Cls1"].ToString());
                    }

                    if (dr["Cur_Id"].ToString() != string.Empty)
                    {
                        _currencyId = ObjGlobal.ReturnInt(dr["Cur_Id"].ToString());
                        TxtCurrency.Text = dr["Ccode"].ToString();
                    }

                    TxtCurrencyRate.Text = dr["Cur_Rate"].GetDecimalString(true);
                    LblTotalBasicAmount.Text = dr["B_Amount"].GetDecimalString();
                    TxtBillTermAmount.Text = dr["T_Amount"].GetDecimalString();
                    LblTotalNetAmount.Text = dr["N_Amount"].GetDecimalString();
                    LblTotalLocalNetAmount.Text = dr["LN_Amount"].GetDecimalString();
                    TxtRemarks.Text = Convert.ToString(dr["Remarks"].ToString());
                }

                if (dtDetails.Rows.Count > 0)
                {
                    var iRow = 0;
                    DGrid.Rows.Clear();
                    DGrid.Rows.Add(dtDetails.Rows.Count + 1);
                    foreach (DataRow dr in dtDetails.Rows)
                    {
                        DGrid.Rows[iRow].Cells["GTxtSNo"].Value = dr["Invoice_SNo"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtProductId"].Value = dr["P_Id"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtProduct"].Value = dr["PName"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtGodownId"].Value = dr["Gdn_Id"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtGodown"].Value = dr["GName"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtAltQty"].Value = dr["Alt_Qty"].GetDecimalQtyString();
                        DGrid.Rows[iRow].Cells["GTxtAltUOMId"].Value = dr["Alt_UnitId"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtAltUOM"].Value = dr["AltUnitCode"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtQty"].Value = dr["Qty"].GetDecimalQtyString();
                        DGrid.Rows[iRow].Cells["GTxtUOMId"].Value = dr["Unit_Id"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtUOM"].Value = dr["UnitCode"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtRate"].Value = dr["Rate"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtNarration"].Value = dr["Narration"].ToString();

                        DGrid.Rows[iRow].Cells["GTxtChallanNo"].Value = dr["SC_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtChallanSno"].Value = dr["Invoice_SNo"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["SO_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtOrderSno"].Value = dr["SO_SNo"].ToString();

                        DGrid.Rows[iRow].Cells["GTxtTaxableAmount"].Value = dr["Tax_Amount"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtVatAmount"].Value = dr["V_Amount"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = dr["V_Rate"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtFreeQty"].Value = dr["Free_Qty"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtExtraFreeQty"].Value = dr["StockFree_Qty"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtStockFreeQty"].Value = dr["StockFree_Qty"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtExtraFreeUnitId"].Value = dr["ExtraFree_Unit_Id"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtExtraStockQty"].Value = dr["ExtraStockFree_Qty"].ToString();
                        DGrid.Rows[iRow].Cells["IsTaxable"].Value = dr["T_Product"].GetBool();
                        DGrid.Rows[iRow].Cells["GTxtSBLedgerId"].Value = dr["S_Ledger"].GetLong();
                        DGrid.Rows[iRow].Cells["GTxtSRLedgerId"].Value = dr["SR_Ledger"].GetLong();
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

                        iRow++;
                    }

                    DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
                    DGrid.ClearSelection();
                }

                if (dtBillTerm.Rows.Count > 0)
                {
                    var dtBTerm = dsSales.Tables[3];
                    _dtBillTerm.Rows.Clear();
                    foreach (DataRow ro in dtBTerm.Rows)
                    {
                        var dataRow = _dtBillTerm.NewRow();
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
                        _dtBillTerm.Rows.InsertAt(dataRow, _dtBillTerm.RowsCount() + 1);
                    }
                }

                if (dtTransport.Rows.Count > 0)
                {
                    foreach (DataRow dr in dsSales.Tables[4].Rows)
                    {
                        TxtTransport.Text = dr["Transport"].ToString();
                        TxtVechileNo.Text = dr["VechileNo"].ToString();
                        TxtBiltyNo.Text = dr["BiltyNo"].ToString();
                        TxtPackage.Text = dr["Package"].ToString();
                        MskBiltyDate.Text = !string.IsNullOrEmpty(dr["BiltyDate"].ToString())
                            ? Convert.ToDateTime(dr["BiltyDate"].ToString()).ToString("dd/MM/yyyy")
                            : string.Empty;
                        CmbBiltyType.Text = dr["BiltyType"].ToString();
                        TxtDriver.Text = dr["Driver"].ToString();
                        TxtPhoneNo.Text = dr["PhoneNo"].ToString();
                        TxtLicenseNo.Text = dr["LicenseNo"].ToString();
                        TxtMailingAddress.Text = dr["MailingAddress"].ToString();
                        TxtMCity.Text = dr["MCity"].ToString();
                        TxtMState.Text = dr["MState"].ToString();
                        TxtMCountry.Text = dr["MCountry"].ToString();
                        TxtMEmail.Text = dr["MEmail"].ToString();
                        TxtShippingAddress.Text = dr["ShippingAddress"].ToString();
                        TxtSCity.Text = dr["SCity"].ToString();
                        TxtSState.Text = dr["SState"].ToString();
                        TxtSCountry.Text = dr["SCountry"].ToString();
                        TxtSEmail.Text = dr["SEmail"].ToString();
                        TxtContractNo.Text = dr["ContractNo"].ToString();
                        MskContractNoDate.Text = !string.IsNullOrEmpty(dr["ContractDate"].ToString())
                            ? Convert.ToDateTime(dr["ContractDate"].ToString()).ToString("dd/MM/yyyy")
                            : string.Empty;
                        TxtExportInvoiceNo.Text = dr["ExportInvoice"].ToString();
                        MskExportInvoiceDate.Text = !string.IsNullOrEmpty(dr["ExportInvoiceDate"].ToString())
                            ? Convert.ToDateTime(dr["ExportInvoiceDate"].ToString()).ToString("dd/MM/yyyy")
                            : string.Empty;
                        TxtVendorOrderNo.Text = dr["VendorOrderNo"].ToString();
                        TxtBankDetails.Text = dr["BankDetails"].ToString();
                        TxtLCNumber.Text = dr["LcNumber"].ToString();
                        TxtCustomName.Text = dr["CustomDetails"].ToString();
                    }
                }
            }
            ObjGlobal.DGridColorCombo(DGrid);
            VoucherTotalCalculation();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void FillPurchaseInvoiceDetails(string voucherNo)
    {
        try
        {
            var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
            if (dsSales.Tables.Count > 0)
            {
                var dtProductTerm = dsSales.Tables.Count > 2 ? dsSales.Tables[2] : new DataTable();
                if (dsSales.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsSales.Tables[0].Rows)
                    {
                        if (_actionTag != "SAVE") TxtVno.Text = dr["PB_Invoice"].ToString();
                        MskMiti.Text = dr["Invoice_Miti"].ToString();
                        MskDate.Text = dr["Invoice_Date"].GetDateString();
                        TxtRefVno.Text = dr["PB_VNo"].ToString();

                        MskRefDate.Text = string.IsNullOrEmpty(dr["VNo_Date"].ToString()) switch
                        {
                            false => dr["VNo_Date"].GetDateString(),
                            _ => MskRefDate.Text
                        };
                        TxtCustomer.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = dr["Vendor_ID"].GetLong();
                        SetLedgerInfo(_ledgerId);
                        TxtDueDays.Text = dr["DueDays"].ToString();
                        MskDueDays.Text = dr["DueDate"].GetDateString();
                        _dtPartyInfo.Rows.Clear();
                        var drp = _dtPartyInfo.NewRow();
                        drp["PartyLedger_Id"] = null;
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
                        if (dr["Cur_Id"].GetInt() > 0)
                        {
                            _currencyId = dr["Cur_Id"].GetInt();
                            TxtCurrency.Text = dr["Ccode"].ToString();
                        }
                        else
                        {
                            _currencyId = ObjGlobal.SysCurrencyId;
                            TxtCurrency.Text = ObjGlobal.SysCurrency;
                        }

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

                    if (dsSales.Tables[1].Rows.Count <= 0)
                    {
                    }
                    else
                    {
                        DGrid.Rows.Clear();
                        var iRows = 0;
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
                            DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimalString();
                            DGrid.Rows[iRows].Cells["GTxtNarration"].Value = dr["Narration"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = dr["SO_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = dr["SO_Sno"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = dr["SC_Invoice"].ToString();
                            DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = dr["SC_SNo"].ToString();
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
                            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                            iRows++;
                        }

                        DGrid.ClearSelection();
                    }

                    if (dsSales.Tables[2].Rows.Count > 0)
                    {
                        _dtProductTerm = dsSales.Tables[2];
                    }

                    if (dsSales.Tables[3].Rows.Count > 0)
                    {
                        _dtBillTerm = dsSales.Tables[3];
                    }
                }
            }

            ObjGlobal.DGridColorCombo(DGrid);
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

    private static void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.BackgroundImage == null) return;
        var fileExt = Path.GetExtension(pictureBox.ImageLocation);
        var location = pictureBox.ImageLocation;
        if (fileExt is ".JPEG" or ".jpg" or ".Bitmap" or ".png")
            ObjGlobal.PreviewPicture(pictureBox, string.Empty);
        else
            Process.Start(location ?? string.Empty);
    }

    private bool IsValidInformation()
    {
        if (_actionTag != "SAVE")
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) == DialogResult.No)
                return false;

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

        if (TxtCustomer.IsBlankOrEmpty() || _ledgerId is 0)
        {
            this.NotifyValidationError(TxtCustomer, "ENTER VENDOR LEDGER OR SELECTED IT..!!");
            return false;
        }

        if (TxtRemarks.IsBlankOrEmpty() && _tagStrings.Contains(_actionTag))
        {
            this.NotifyValidationError(TxtRemarks, $"ENTER REMARKS OF THIS VOUCHER FOR {_actionTag}..!!");
            return false;
        }

        if ((TxtSubledger.IsBlankOrEmpty() || _subLedgerId is 0) && ObjGlobal.SalesSubLedgerMandatory)
        {
            this.NotifyValidationError(TxtSubledger, "ENTER SUB LEDGER OF THIS VOUCHER..!!");
            return false;
        }

        if ((TxtAgent.IsBlankOrEmpty() || _agentId is 0) && ObjGlobal.SalesAgentMandatory)
        {
            this.NotifyValidationError(TxtSubledger, "ENTER SUB LEDGER OF THIS VOUCHER..!!");
            return false;
        }

        if ((TxtDepartment.IsBlankOrEmpty() || _departmentId is 0) && ObjGlobal.SalesDepartmentMandatory)
        {
            this.NotifyValidationError(TxtDepartment, "ENTER DEPARTMENT OF THIS VOUCHER..!!");
            return false;
        }

        return true;
    }

    private string CalculateBillingTerm()
    {
        decimal termAmount = 0;
        var term = _master.GetTermCalculationForVoucher("SQ");
        if (term.RowsCount() <= 0)
        {
            return string.Empty;
        }
        var exitsTerm = _dtBillTerm.Copy();
        _dtBillTerm.Rows.Clear();
        var iRows = 1;
        if (term.Rows.Count <= 0) return termAmount.GetDecimalString();
        foreach (DataRow ro in term.Rows)
        {
            decimal exitRate = 0;
            var termId = ro["TermId"].GetInt();
            var exDetails = exitsTerm.AsEnumerable().Any(c => c.Field<string>("TermId").Equals(termId.ToString()));
            if (exDetails)
            {
                var dtAmount = exitsTerm.Select($" TermId='{termId}'").CopyToDataTable();
                exitRate = dtAmount.Rows[0]["TermRate"].GetDecimal();
                dtAmount.Rows[0]["TermAmt"].GetDecimal();
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
            row["TermAmt"] = exitRate > 0 ? (exitRate * LblTotalBasicAmount.GetDecimal() / 100).GetDecimalString() : 0.GetDecimalString();
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
            if (_dtBillTerm.Rows[iRows - 1]["Sign"].Equals("-"))
            {
                termAmount -= amount;
            }
            else
            {
                termAmount += amount;
            }

            iRows++;
        }

        return termAmount.GetDecimalString();
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    // OBJECT FOR THIS FORM

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
    private readonly bool _isProvision;
    private readonly bool _isZoom;
    private readonly bool _isPTermExits;
    private readonly bool _isBTermExits;

    private string _actionTag = string.Empty;
    private string _mskOrderDate = string.Empty;
    private string _mskChallanDate = string.Empty;
    private string _numberScheme = string.Empty;
    private readonly string _txtZoomVno = string.Empty;
    private string _invoiceType = string.Empty;
    private string _description = string.Empty;
    private readonly string _batchNo = string.Empty;
    private readonly string _mfgDate = string.Empty;
    private readonly string _expDate = string.Empty;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string[] _ledgerType = ["CASH", "BANK"];
    private string ledgerType;
    private decimal _conQty;
    private decimal _freeQty;

    private KeyPressEventArgs _getKeys;
    private DataTable _dtPartyInfo;
    private DataTable _dtProductTerm;
    private DataTable _dtBillTerm;
    private readonly ISalesEntry _entry = new ClsSalesEntry();
    private readonly ISalesQuotationRepository _salesQuotation = new SalesQuotationRepository();
    private readonly IFinanceEntry _financeEntry = new ClsFinanceEntry();
    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly ISalesDesign _design = new SalesEntryDesign();
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