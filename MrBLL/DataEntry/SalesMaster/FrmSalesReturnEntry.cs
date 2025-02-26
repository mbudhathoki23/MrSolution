using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
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
using MrDAL.DataEntry.Interface.SalesReturn;
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

public partial class FrmSalesReturnEntry : MrForm
{
    // SALES RETURN INVOICE ENTRY
    #region --------------- SALES RETURN INVOICE ENTRY ---------------

    public FrmSalesReturnEntry(bool zoom, string txtZoomVno, bool provisionVoucher = false, string invoiceType = "NORMAL")
    {
        InitializeComponent();

        _isProvision = provisionVoucher;
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _invoiceType = invoiceType;


        _dtPartyInfo = _master.GetPartyInfo();
        _dtBillTerm = _master.GetBillingTerm();
        _dtProductTerm = _master.GetBillingTerm();
        _dtBatchInfo = _master.GetProductBatchFormat();


        _isPTermExits = _master.IsBillingTermExitsOrNot("SR", "P");
        _isBTermExits = _master.IsBillingTermExitsOrNot("SR", "B");
        BindDataInComboBox();
        DesignGridColumnsAsync();
        EnableControl();
        ClearControl();
    }

    private void FrmSalesReturnEntry_Load(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillReturnInvoiceData(_txtZoomVno);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmSalesReturnEntry_KeyPress(object sender, KeyPressEventArgs e)
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

    private void FrmSalesReturnEntry_Shown(object sender, EventArgs e)
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
            else if (MskDate.Enabled)
            {
                MskDate.Focus();
            }
            else
            {
                TxtRefVno.Focus();
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
            if (MskMiti.Enabled)
            {
                MskMiti.Focus();
            }
            else
            {
                TxtRefVno.Focus();
            }
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
        if (voucherNo.IsValueExits())
        {
            FillSalesInvoiceDetails(voucherNo);
        }

    }

    private void BtnPrintInvoice_Click(object sender, EventArgs e)
    {
        PrintVoucher(false);
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
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SR");
        FillReturnInvoiceData(TxtVno.Text);
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
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SR");
        if (_actionTag == "SAVE")
        {
            return;
        }

        if (voucherNo.IsValueExits())
        {
            ClearControl();
            TxtVno.Text = voucherNo;
            FillReturnInvoiceData(TxtVno.Text);
        }

        if (TxtVno.Enabled)
        {
            TxtVno.Focus();
        }
        else
        {
            TxtRefVno.Focus();
        }
    }

    private void BtnRefVno_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        if (voucherNo.IsValueExits())
        {
            TxtRefVno.Text = voucherNo;
            FillInvoiceData(TxtRefVno.Text);
        }
        TxtRefVno.Focus();
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
    }

    private void BtnSubLedger_Click(object sender, EventArgs e)
    {
        (TxtSubledger.Text, _subLedgerId) = GetMasterList.GetSubLedgerList(_actionTag);
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetDepartmentList(_actionTag);
        if (id > 0)
        {
            TxtDepartment.Text = description;
            _departmentId = id;
        }
        TxtDepartment.Focus();
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

        if (!TxtBillTermAmount.Enabled || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            return;
        }
        var status = _dtBillTerm.RowsCount() > 0 ? "UPDATE" : "SAVE";
        var result = new FrmTermCalculation(status, LblTotalBasicAmount.GetDecimal(), "SR", _dtBillTerm, _tableAmount, LblTotalQty.GetDecimal(), CmbInvoiceType.Text);
        result.ShowDialog();
        if (result.DialogResult != DialogResult.OK)
        {
            _dtBillTerm.Rows.Clear();
            TxtBillTermAmount.Clear();
        }
        else
        {
            TxtBillTermAmount.Text = result.TotalTermAmount;
            AddToBillingTerm(result.CalcTermTable);
        }

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
        if (IsValidInformation())
        {
            CreateDatabaseTable.DropTrigger();
            if (SaveReturnInvoice() != 0)
            {
                PrintVoucher(true);
                if (CmbPaymentMode.Text is "Cash" or "Bank")
                {
                    SaveCashAndBankDetails();
                }

                if (_isZoom)
                {
                    Close();
                }
                this.NotifySuccess($@"{TxtVno.Text} RETURN INVOICE NUMBER {_actionTag} SUCCESSFULLY..!!");
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
                    {
                        MskMiti.Focus();
                    }
                    else
                    {
                        TxtRefVno.Focus();
                    }
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
        Close();
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
        var dtVoucher = _entry.CheckVoucherNoExitsOrNot("AMS.SR_Master", "SR_Invoice", TxtVno.Text);
        if (dtVoucher.RowsCount() > 0 && _actionTag.Equals("SAVE"))
        {
            this.NotifyValidationError(TxtVno, "RETURN INVOICE NUMBER ALREADY EXITS..!!");
        }
        else if (dtVoucher.Rows.Count <= 0 && !_actionTag.Equals("SAVE"))
        {
            this.NotifyValidationError(TxtVno, "RETURN INVOICE NUMBER NOT EXITS..!!");
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
                MskDate.WarningMessage($@"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
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
            else if (TxtRefVno.IsBlankOrEmpty())
            {
                if (CustomMessageBox.Question(@"PLEASE SELECT INVOICE NUMBER..!! OR CLICK YES TO CONTINUE..!!") == DialogResult.Yes)
                {
                    TxtCustomer.Focus();
                    return;
                }
            }
        }
        else if (e.KeyCode is Keys.F1)
        {
            BtnRefVno.PerformClick();
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
            var (description, id) = GetMasterList.CreateGeneralLedger("VENDOR", true);
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
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtSubledger, BtnSubledger);
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
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.SalesDepartmentMandatory)
            {
                TxtDepartment.WarningMessage("DEPARTMENT IS MANDATORY PLEASE SELECT DEPARTMENT");
                return;
            }
            if (TxtAgent.Enabled)
            {
                TxtAgent.Focus();
            }
            else
            {
                DGrid.Focus();
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
            var (description, id) = GetMasterList.CreateAgent(true);
            if (id > 0)
            {
                TxtAgent.Text = description;
                _agentId = id;
            }
            TxtAgent.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtAgent.IsBlankOrEmpty() && ObjGlobal.SalesAgentMandatory)
            {
                TxtAgent.WarningMessage("AGENT IS MANDATORY..!! PLEASE SELECT AGENT...");
                return;
            }
            DGrid.Focus();
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
        else
        {

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

        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
        {
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out var isNumber);
        }
    }

    private void BtnAttachment1_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        var filename = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            filename = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment1.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment1.Image = bitmap;
            PAttachment1.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment1.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
            }
        }
    }

    private void LinkAttachment1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment1);
    }

    private void BtnAttachment2_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        var filename = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            filename = dlg.FileName;
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
        var isFileExists = string.Empty;
        var filename = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            filename = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment3.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment3.Image = bitmap;
            PAttachment3.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment3.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
            }
        }
    }

    private void LinkAttachment3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment3);
    }

    private void BtnAttachment4_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        var filename = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            filename = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment4.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment4.Image = bitmap;
            PAttachment4.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment4.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
            }
        }
    }

    private void LinkAttachment4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment4);
    }

    private void BtnAttachment5_Click(object sender, EventArgs e)
    {
        var isFileExists = string.Empty;
        var filename = string.Empty;
        try
        {
            var dlg = new OpenFileDialog
            {
                Filter = @"Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            dlg.ShowDialog();
            filename = dlg.FileName;
            isFileExists = dlg.FileName;
            PAttachment5.ImageLocation = filename;
            var bitmap = new Bitmap(filename);
            PAttachment5.Image = bitmap;
            PAttachment5.SizeMode = PictureBoxSizeMode.StretchImage;
            LblAttachment5.Text = Path.GetFileName(filename);
        }
        catch (Exception ex)
        {
            if (isFileExists != string.Empty)
            {
                MessageBox.Show($@"PICTURE FILE FORMAT & {ex.Message}");
            }
        }
    }

    private void LinkAttachment5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        PreviewImage(PAttachment5);
    }

    // GRID CONTROL EVENTS
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
        if (!TxtProduct.IsValueExits() || !TxtProduct.Enabled)
        {
            return;
        }

        var existingTerm = new DataTable();
        var serialNo = 0;
        if (DGrid.CurrentRow != null)
        {
            serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
            var exDetails = _dtProductTerm.AsEnumerable().Any(c => c.Field<string>("ProductSno").Equals(serialNo.ToString()) && c.Field<string>("ProductId").Equals(_productId.ToString()));
            existingTerm = exDetails switch
            {
                true => _dtProductTerm.Select($"ProductSno= '{serialNo}' and ProductId='{_productId}'").CopyToDataTable(),
                _ => existingTerm
            };
        }

        var result = new FrmTermCalculation(_actionTag == "SAVE" && !_isRowUpdate ? "SAVE" : "UPDATE", TxtBasicAmount.Text, true, "SR", _productId, serialNo, existingTerm, TxtQty.Text);
        result.ShowDialog();
        TxtTermAmount.Text = result.TotalTermAmount;
        var basicAmount = TxtBasicAmount.GetDecimal();
        var termAmount = TxtTermAmount.GetDecimal();
        TxtNetAmount.Text = (basicAmount + termAmount).GetDecimalString();
        AddToProductTerm(result.CalcTermTable);
        TxtNetAmount.Focus();
    }

    private void OnTxtBasicAmountOnValidating(object sender, CancelEventArgs e)
    {
        if (ActiveControl == TxtRate || TxtTermAmount.Enabled)
        {
            return;
        }

        if (TxtProduct.Enabled && TxtProduct.IsValueExits())
        {
            AddTextToGrid(_isRowUpdate);
            TxtProduct.Focus();
        }
    }

    private void OnTxtBasicAmountOnTextChanged(object sender, EventArgs e)
    {
        if (TxtBasicAmount.Focused)
        {
            TxtRate.Text = TxtBasicAmount.GetDecimal() > 0
                ? (TxtBasicAmount.GetDecimal() / TxtQty.GetDecimal()).GetDecimalString()
                : 0.GetDecimalString();
        }
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
        TxtRate.Text = TxtRate.GetRateDecimalString();
        TxtBasicAmount.Text = (TxtRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
        if (ActiveControl == TxtQty || TxtTermAmount.Enabled)
        {
            return;
        }
        if (!TxtProduct.Enabled || !TxtProduct.IsValueExits())
        {
            return;
        }
        if (TxtBasicAmount.Enabled)
        {
            TxtBasicAmount.Focus();
            e.Cancel = true;
            return;
        }
        AddTextToGrid(_isRowUpdate);
        TxtProduct.Focus();
    }

    private void OnTxtRateOnTextChanged(object sender, EventArgs e)
    {
        if (!TxtRate.Focused)
        {
            return;
        }
        TxtBasicAmount.Text = (TxtRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void OnTxtRateOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
        if (ActiveControl == TxtAltQty)
        {
            return;
        }
        var con = _conAltQty > 0 ? _conQty / _conAltQty : 0;
        TxtAltQty.Text = con switch
        {
            > 0 when TxtQty.GetDecimal() > 0 => (TxtQty.GetDecimal() / con).GetDecimalQtyString(),
            _ => 0.GetDecimalQtyString()
        };
        TxtBasicAmount.Text = (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void OnTxtQtyOnValidated(object sender, EventArgs e)
    {
        TxtQty.Text = TxtQty.GetDecimalQtyString();
        if (!TxtQty.Enabled || !TxtProduct.Enabled)
        {
            return;
        }
        if (TxtQty.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtQty, "PRODUCT OPENING QTY CANNOT BE ZERO");
        }

        TxtBasicAmount.Text = (TxtRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
    }

    private void TxtAltQty_TextChanged(object sender, EventArgs e)
    {
        if (ActiveControl == TxtQty)
        {
            return;
        }
        var con = _conAltQty > 0 ? _conQty / _conAltQty : 0;
        TxtQty.Text = _conQty switch
        {
            > 0 when TxtAltQty.GetDecimal() > 0 => (TxtAltQty.GetDecimal() * con).GetDecimalQtyString(),
            _ => 0.GetDecimalQtyString()
        };
    }

    private void OnTxtAltQtyOnValidating(object sender, CancelEventArgs e)
    {
        if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0)
        {
            TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
        }
        TxtBasicAmount.Text = (TxtRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() - TxtTermAmount.GetDecimal()).GetDecimalString();
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
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", e.KeyCode.ToString(), TxtGodown, OpenGodownList);
        }
    }

    private void OnTxtProductOnValidating(object _, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
        }

        if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
            {
                if (_getKeys.KeyChar is (char)Keys.Enter)
                {
                    _getKeys.Handled = false;
                }

                this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                return;
            }
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
            SetProductInfo();
            TxtProduct.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtProduct.Text, _productId) = GetMasterList.CreateProduct(true);
            SetProductInfo();
            TxtProduct.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", e.KeyCode.ToString(), TxtProduct, OpenProductList);
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
            SetProductInfo();
            TxtProduct.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtProduct.Text, _productId) = GetMasterList.CreateProduct(true);
            SetProductInfo();
            TxtProduct.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE", e.KeyCode.ToString(), TxtProduct, OpenProductList);
        }
    }

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

    private void OnEventHandler(object _, EventArgs e)
    {
        DGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }

    #endregion --------------- SALES INVOICE ENTRY ---------------


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
            _financeEntry.CbMaster.VoucherMode = !_isProvision ? "Contra" : "PROV";
            _financeEntry.CbMaster.Voucher_No = TxtVno.Text;
            _financeEntry.CbMaster.Voucher_Date = MskDate.Text.GetDateTime();
            _financeEntry.CbMaster.Voucher_Miti = MskMiti.Text;
            _financeEntry.CbMaster.Voucher_Time = DateTime.Now;
            _financeEntry.CbMaster.Ref_VNo = TxtRefVno.Text;
            _financeEntry.CbMaster.Ref_VDate = !string.IsNullOrEmpty(TxtRefVno.Text.Trim()) ? DateTime.Parse(MskRefDate.Text) : DateTime.Now;
            _financeEntry.CbMaster.VoucherType = "Contra";
            _financeEntry.CbMaster.Ledger_Id = CmbPaymentMode.Text.Equals("Bank") ? ObjGlobal.FinanceBankLedgerId.GetLong() : ObjGlobal.FinanceCashLedgerId.GetLong();
            _financeEntry.CbMaster.CheqNo = string.Empty;
            _financeEntry.CbMaster.CheqDate = DateTime.Now;
            _financeEntry.CbMaster.CheqMiti = string.Empty;
            _financeEntry.CbMaster.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _financeEntry.CbMaster.Currency_Rate = TxtCurrencyRate.GetDecimal() > 0 ? TxtCurrency.Text.GetDecimal() : 1;
            _financeEntry.CbMaster.Cls1 = _departmentId;
            _financeEntry.CbMaster.Cls2 = 0;
            _financeEntry.CbMaster.Cls3 = 0;
            _financeEntry.CbMaster.Cls4 = 0;
            _financeEntry.CbMaster.Remarks = TxtRemarks.Text;
            _financeEntry.CbMaster.SyncRowVersion = (short)(_actionTag is "UPDATE" ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.CB_Master", "SyncRowVersion", "Voucher_No", TxtVno.Text.Trim()) : 1);
            _financeEntry.CbDetails.Voucher_No = TxtVno.Text;
            _financeEntry.CbDetails.Ledger_Id = _ledgerId;
            _financeEntry.CbDetails.Subledger_Id = _subLedgerId;
            _financeEntry.CbDetails.Agent_Id = _agentId;
            _financeEntry.CbDetails.Cls1 = _departmentId;
            _financeEntry.CbDetails.Debit = LblTotalLocalNetAmount.GetDecimal();
            _financeEntry.CbDetails.Credit = 0;
            _financeEntry.CbDetails.Narration = $"BEING PAYMENT MADE AGAINST SALES RETURN INVOICE NO : {TxtRefVno.Text}";
            _financeEntry.CbDetails.Tbl_Amount = 0;
            _financeEntry.CbDetails.V_Amount = 0;

            _financeEntry.CbDetails.Party_No = TxtRefVno.Text;
            _financeEntry.CbDetails.Invoice_Date = MskDate.Text.GetDateTime();
            _financeEntry.CbDetails.Invoice_Miti = MskMiti.Text;
            _financeEntry.CbDetails.VatLedger_Id = 0;
            _financeEntry.CbDetails.PanNo = 0;
            _financeEntry.CbDetails.Vat_Reg = false;
            _financeEntry.CbDetails.CBLedgerId = ObjGlobal.SalesLedgerId;
            _financeEntry.CbDetails.CurrencyId = _currencyId;
            _financeEntry.CbDetails.CurrencyRate = TxtCurrencyRate.Text.GetDecimal();
            return _financeEntry.SaveCashBankVoucher(_actionTag);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            this.NotifyError(ex.Message);
            return 0;
        }
    }
    private int SaveReturnInvoice()
    {
        try
        {
            if (_actionTag is "SAVE")
            {
                TxtVno.GetCurrentVoucherNo("SR", _numberScheme);
            }
            _salesReturn.SrMaster.SR_Invoice = TxtVno.Text;
            _salesReturn.SrMaster.Invoice_Date = DateTime.Parse(MskDate.Text);
            _salesReturn.SrMaster.Invoice_Miti = MskMiti.Text;
            _salesReturn.SrMaster.SB_Invoice = TxtRefVno.Text;
            _salesReturn.SrMaster.SB_Date = DateTime.Parse(ObjGlobal.ReturnEnglishDate(MskRefDate.Text));
            _salesReturn.SrMaster.SB_Miti = MskRefDate.Text;
            _salesReturn.SrMaster.Invoice_Time = DateTime.Now;
            _salesReturn.SrMaster.Customer_ID = _ledgerId;

            _salesReturn.SrMaster.PartyLedgerId = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyLedgerId"].GetLong() : 0;
            _salesReturn.SrMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
            _salesReturn.SrMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
            _salesReturn.SrMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
            _salesReturn.SrMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
            _salesReturn.SrMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
            _salesReturn.SrMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
            _salesReturn.SrMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString()) ? DateTime.Parse(_dtPartyInfo.Rows[0]["ChequeDate"].ToString()) : DateTime.Now;
            _salesReturn.SrMaster.ChqMiti = _dtPartyInfo.Rows.Count > 0 && !string.IsNullOrEmpty(_dtPartyInfo.Rows[0]["ChequeNo"].ToString()) ? _dtPartyInfo.Rows[0]["ChequeMiti"].ToString() : DateTime.Now.GetNepaliDate();
            _salesReturn.SrMaster.Invoice_Mode = "NORMAL";
            _salesReturn.SrMaster.Invoice_Type = CmbInvoiceType.Text;
            _salesReturn.SrMaster.Payment_Mode = CmbPaymentMode.Text;
            _salesReturn.SrMaster.DueDays = TxtDueDays.Text.GetInt();
            _salesReturn.SrMaster.DueDate = MskDueDays.MaskCompleted ? MskDueDays.GetEnglishDate().GetDateTime() : DateTime.Now;
            _salesReturn.SrMaster.Agent_Id = _agentId > 0 ? _agentId : null;
            _salesReturn.SrMaster.Subledger_Id = _subLedgerId > 0 ? _subLedgerId : null;
            _salesReturn.SrMaster.Cls1 = _departmentId > 0 ? _departmentId : null;
            _salesReturn.SrMaster.Cls2 = 0;
            _salesReturn.SrMaster.Cls3 = 0;
            _salesReturn.SrMaster.Cls4 = 0;
            _salesReturn.SrMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _salesReturn.SrMaster.Cur_Rate = TxtCurrencyRate.Text.GetDecimal() > 0 ? TxtCurrencyRate.Text.GetDecimal() : 1;
            _salesReturn.SrMaster.CounterId = 0;
            _salesReturn.SrMaster.B_Amount = LblTotalBasicAmount.Text.GetDecimal();
            _salesReturn.SrMaster.T_Amount = TxtBillTermAmount.Text.GetDecimal();
            _salesReturn.SrMaster.N_Amount = LblTotalNetAmount.Text.GetDecimal();
            _salesReturn.SrMaster.LN_Amount = LblTotalLocalNetAmount.Text.GetDecimal();
            _salesReturn.SrMaster.Tender_Amount = 0;
            _salesReturn.SrMaster.Return_Amount = 0;
            _salesReturn.SrMaster.V_Amount = 0;
            _salesReturn.SrMaster.Tbl_Amount = 0;
            _salesReturn.SrMaster.Action_Type = _actionTag;
            _salesReturn.SrMaster.R_Invoice = false;
            _salesReturn.SrMaster.No_Print = 0;
            _salesReturn.SrMaster.In_Words = LblNumberInWords.Text;
            _salesReturn.SrMaster.Remarks = TxtRemarks.Text;
            _salesReturn.SrMaster.Audit_Lock = false;

            _salesReturn.SrMaster.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            _salesReturn.SrMaster.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            if (ObjGlobal.LocalOriginId != null)
            {
                _salesReturn.SrMaster.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                    ? ObjGlobal.LocalOriginId.Value
                    : Guid.Empty;
            }

            _salesReturn.SrMaster.SyncCreatedOn = DateTime.Now;
            _salesReturn.SrMaster.SyncLastPatchedOn = DateTime.Now;

            var sync = _salesReturn.ReturnSyncRowVersionVoucher("SR", TxtVno.Text);
            _salesReturn.SrMaster.SyncRowVersion = sync;



            // SALES INVOICE DETAILS
            _salesReturn.DetailsList.Clear();

            if (DGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in DGrid.Rows)
                {
                    var list = new SR_Details();
                    if (row.Cells["GTxtProductId"].Value.GetLong() is 0)
                    {
                        continue;
                    }
                    list.SR_Invoice = TxtVno.Text;
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
                    list.SB_Invoice = row.Cells["GTxtInvoiceNo"].Value.GetString();
                    list.SB_Sno = row.Cells["GTxtInvoiceSno"].Value.GetInt();
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
                    list.PDiscountRate = 0;
                    list.PDiscount = 0;
                    list.BDiscountRate = 0;
                    list.BDiscount = 0;
                    list.ServiceChargeRate = 0;
                    list.ServiceCharge = 0;

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

                    _salesReturn.DetailsList.Add(list);
                }
            }

            //SALES PRODUCT TERM
            _salesReturn.Terms.Clear();

            foreach (DataRow row in _dtProductTerm.Rows)
            {
                var list = new SR_Term()
                {
                    SR_VNo = TxtVno.Text,
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
                _salesReturn.Terms.Add(list);
            }


            foreach (DataRow row in _dtBillTerm.Rows)
            {
                var list = new SR_Term
                {
                    SR_VNo = TxtVno.Text,
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
                _salesReturn.Terms.Add(list);
            }

            return _salesReturn.SaveSalesReturn(_actionTag);
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
        var dt = _master.IsExitsCheckDocumentNumbering("SR");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("SR", _numberScheme);
            TxtVno.Enabled = ObjGlobal.IsIrdRegister;
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("SR");
            if (wnd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            if (string.IsNullOrEmpty(wnd.VNo))
            {
                return;
            }
            _numberScheme = wnd.Description;
            TxtVno.Text = wnd.VNo;
            TxtVno.Enabled = ObjGlobal.IsIrdRegister;
        }
    }
    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag) ? $"SALES RETURN DETAILS [{_actionTag}]" : "SALES RETURN DETAILS";
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVno.Clear();
        if (_actionTag.Equals("SAVE"))
        {
            TxtVno.GetCurrentVoucherNo("SR", _numberScheme);
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
        TxtOrder.Clear();
        TxtRefVno.Clear();
        TxtDueDays.Clear();
        TxtCustomer.Clear();
        TxtDepartment.Clear();
        TxtAgent.Clear();
        TxtSubledger.Clear();
        TxtCurrencyRate.Clear();
        TxtCustomer.Clear();

        CmbInvoiceType.SelectedIndex = 3;
        CmbPaymentMode.SelectedIndex = 0;

        TxtCurrency.Text = ObjGlobal.SysCurrency;
        TxtCurrencyRate.Text = 1.GetDecimalString(true);

        PAttachment1.Image = null;
        PAttachment2.Image = null;
        PAttachment3.Image = null;
        PAttachment4.Image = null;
        PAttachment5.Image = null;

        _dtPartyInfo.Clear();
        _dtBillTerm.Clear();
        _dtProductTerm.Clear();

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
        if (_dtLedger.Columns.Count > 0)
        {
            _dtLedger.Clear();
        }
        DGrid.Rows.Clear();
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillInvoiceData(_txtZoomVno);
        }

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
        _isTaxable = false;
        _productId = _altUnitId = _godownId = _unitId = 0;
        _conQty = _conAltQty = 0;
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
        _description = string.Empty;
        VoucherTotalCalculation();
        AdjustControlsInDataGrid();
    }
    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = !_tagStrings.Contains(_actionTag) && !isEnable;
        BtnEdit.Enabled = BtnDelete.Enabled = BtnNew.Enabled && !ObjGlobal.IsIrdRegister;

        BtnEdit.Visible = BtnDelete.Visible = BtnReverse.Enabled = !ObjGlobal.IsIrdRegister;

        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        if (TxtVno.Enabled)
        {
            TxtVno.Enabled = ObjGlobal.IsIrdRegister;
        }
        MskDate.Enabled = MskMiti.Enabled = isEnable && !ObjGlobal.IsIrdApproved.Equals("YES");

        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        MskDate.Enabled = MskMiti.Enabled = isEnable && !ObjGlobal.IsIrdApproved.Equals("YES");
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtChallan.Enabled = TxtOrder.Enabled = false;
        CmbPaymentMode.Enabled = CmbInvoiceType.Enabled = isEnable;
        TxtCustomer.Enabled = BtnVendor.Enabled = isEnable;
        MskDueDays.Enabled = false;
        TxtDueDays.Enabled = isEnable;
        TxtCurrency.Enabled =
            BtnCurrency.Enabled = TxtCurrencyRate.Enabled = isEnable && ObjGlobal.SalesCurrencyEnable;
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
        DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }
    private void SetLedgerInfo(long ledgerId)
    {
        if (ledgerId is 0)
        {
            return;
        }
        _dtLedger = _master.GetLedgerBalance(ledgerId, MskDate);
        if (_dtLedger.Rows.Count <= 0) return;
        LblPanNo.Text = _dtLedger.Rows[0]["PanNo"].ToString();
        var balance = _dtLedger.Rows[0]["Balance"].GetDecimal();
        LblBalance.Text = balance.GetDecimalString();
        LblBalanceType.Text = balance < 0 ? "Cr" : balance > 0 ? "Dr" : "";
        LblCreditLimit.Text = _dtLedger.Rows[0]["CrLimit"].GetDecimalString();
        LblCreditDays.Text = _dtLedger.Rows[0]["CrDays"].GetDecimalString();
        TxtDueDays.Text = LblCreditDays.GetDecimalString();
        MskDueDays.Text = MskDate.GetDateTime().AddDays(TxtDueDays.GetDouble()).GetDateString();
        ledgerType = _dtLedger.Rows[0]["GLType"].GetUpper();
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
        if (id > 0)
        {
            TxtProduct.Text = description;
            _productId = id;
            SetProductInfo();
        }
        TxtProduct.Focus();
    }
    private void OpenGodownList()
    {
        var (description, id) = GetMasterList.GetGodown(_actionTag);
        if (id > 0)
        {
            TxtGodown.Text = description;
            _godownId = id;
        }
        TxtGodown.Focus();
    }
    private void PrintVoucher(bool isInvoice = true)
    {
        var dtDesign = _master.GetPrintVoucherList("SR");
        if (dtDesign.Rows.Count > 0)
        {
            var type = dtDesign.Rows[0]["DesignerPaper_Name"].ToString();
            var frmDp = new FrmDocumentPrint(type, "SR", TxtVno.Text, TxtVno.Text, true)
            {
                Owner = ActiveForm
            };
            frmDp.ShowDialog();
        }
        else if (!isInvoice)
        {
            CustomMessageBox.Information("PRINT DESIGN SETTING IS REQUIRED FOR PRINT DOCUMENT..!!");
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
        SetProductInfo(true);
        TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.GetString();
        _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
        _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUOMId"].Value.GetInt();
        TxtAltQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltQty"].Value.GetString();
        TxtAltQty.Enabled = _altUnitId > 0;
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
    private void SetProductInfo(bool isUpdate = false)
    {
        if (_productId is 0)
        {
            return;
        }
        _dtProduct = _master.GetMasterProductList(_actionTag, _productId);
        if (_dtProduct.Rows.Count <= 0)
        {
            return;
        }

        foreach (DataRow row in _dtProduct.Rows)
        {
            LblShortName.Text = row["PShortName"].ToString();
            LblSalesRate.Text = row["PSalesRate"].GetRateDecimalString();
            LblAltSalesRate.Text = row["AltSalesRate"].GetRateDecimalString();
            LblAltStockQty.Text = row["AltStockQty"].GetDecimalQtyString();
            LblStockQty.Text = row["StockQty"].GetDecimalQtyString();
            _productType = row["PType"].GetString();
            _productTaxRate = _dtProduct.Rows[0]["PTax"].GetDecimal();
            _isTaxable = _productTaxRate > 0;
            if (!_isRowUpdate)
            {
                LblShortName.Text = row["PShortName"].ToString();
                TxtShortName.Text = LblShortName.Text;
                _altUnitId = row["PAltUnit"].GetInt();
                _unitId = row["PUnit"].GetInt();
                _conQty = row["PQtyConv"].GetDecimal();
                TxtAltUnit.Text = row["AltUnitCode"].GetString();
                TxtUnit.Text = row["UnitCode"].GetString();
                TxtAltQty.Enabled = _altUnitId > 0;
                TxtRate.Text = row["PSalesRate"].GetRateDecimalString();
            }

            var stockQty = LblStockQty.GetDecimal();
            if (_productType.Equals("I"))
            {
                if (stockQty < 0 && ObjGlobal.StockNegativeStockBlock && !isUpdate)
                {
                    TxtProduct.WarningMessage("SELECTED PRODUCT IS OUT OF STOCK CAN NOT BILLED THIS PRODUCT..!!");
                    return;
                }
                if (stockQty < 0 && ObjGlobal.StockNegativeStockWarning && !isUpdate)
                {
                    CustomMessageBox.Warning("SELECTED PRODUCT IS OUT OF STOCK..!!");
                    return;
                }
            }
            _isBatch = row["PBatchwise"].GetBool();
            if (_isBatch)
            {
                CallProductBatch();
            }
        }
    }
    private void CallProductBatch()
    {
        var exitRows = _dtBatchInfo.Select($"ProductId = '{_productId}' and ProductSno='{TxtSno.Text}'");
        var result = new FrmProductBatchList(exitRows is { Length: > 0 } && _isRowUpdate ? exitRows.CopyToDataTable() : new DataTable(), false)
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
            TxtRate.Text = result._batchRate;
            TxtRate.Enabled = TxtRate.GetDecimal() > 0;
            return;
        }
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
    private void ProductBatchInfo(DataTable dt)
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }

        var serialNo = TxtSno.Text;
        var exDetails = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
        if (!exDetails)
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
        else
        {
            var exitAny = _dtBatchInfo.AsEnumerable().Any(c =>
                c.Field<string>("ProductId").Equals(_productId.ToString()) &&
                c.Field<string>("ProductSno").Equals(serialNo.Trim()));
            if (exitAny)
            {
                foreach (DataRow ro in dt.Rows)
                {
                    foreach (DataRow row in _dtBatchInfo.Rows)
                    {
                        if (row["ProductSno"].GetInt() == ro["ProductSno"].GetInt() &&
                            row["ProductId"].GetLong() == ro["ProductId"].GetLong())
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
            this.NotifyValidationError(TxtQty, "PRODUCT QTY IS CANNOT BE ZERO..!!");
            return;
        }

        if (_productType.Equals("I"))
        {
            if (LblStockQty.GetDecimal() - TxtQty.GetDecimal() < 0 && ObjGlobal.StockNegativeStockBlock)
            {
                TxtProduct.WarningMessage("STOCK IS NEGATIVE & BLOCK TO BILL IN NEGATIVE..!!");
                return;
            }
            if (LblStockQty.GetDecimal() - TxtQty.GetDecimal() < 0 && ObjGlobal.StockNegativeStockWarning)
            {
                if (CustomMessageBox.Question("STOCK IS NEGATIVE DO YOU WANT TO CONTINUE..!!") == DialogResult.No)
                {
                    TxtProduct.Focus();
                    return;
                }
            }
        }

        if (_isBatch)
        {
            var batch = _dtBatchInfo.Select($"ProductId = '{_productId}' and ProductSno='{TxtSno.Text}'");
            if (batch.Length == 0)
            {
                TxtProduct.WarningMessage("BATCH IS REQUIRED FOR THE PRODUCT..!!");
                return;
            }
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
        DGrid.Rows[iRows].Cells["GTxtAltQty"].Value = TxtAltQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtAltUOMId"].Value = _altUnitId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAltUOM"].Value = TxtAltUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtQty"].Value = TxtQty.GetDecimalQtyString();
        DGrid.Rows[iRows].Cells["GTxtUOMId"].Value = _unitId;
        DGrid.Rows[iRows].Cells["GTxtUOM"].Value = TxtUnit.Text;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.Text.GetRateDecimalString();
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBasicAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtTermAmount"].Value = TxtTermAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNetAmount"].Value = TxtNetAmount.GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtAltStockQty"].Value = TxtAltQty.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtStockQty"].Value = TxtQty.GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = _description;

        DGrid.Rows[iRows].Cells["GTxtInvoiceNo"].Value = TxtRefVno.Text;
        DGrid.Rows[iRows].Cells["GTxtInvoiceSno"].Value = iRows + 1;

        //DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = TxtChallan.Text;
        //DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = DGrid.Rows[iRows].Cells["GTxtSNo"].Value;

        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = _freeQty;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = _freeQty;

        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraStockQty"].Value = string.Empty;
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

        _master.UpdateProductSalesRate(TxtRate.GetDouble(), TxtProduct.Text);
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
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtProductId"].Value.IsValueExits())
        {
            return;
        }
        var execute = DGrid.Rows.OfType<DataGridViewRow>();
        var rows = execute as DataGridViewRow[] ?? execute.ToArray();

        var totalAltQty = rows.Sum(row => row.Cells["GTxtAltQty"].Value.GetDecimal());
        var totalQty = rows.Sum(row => row.Cells["GTxtQty"].Value.GetDecimal());
        var totalBasicAmount = rows.Sum(row => row.Cells["GTxtNetAmount"].Value.GetDecimal());
        _tableAmount = rows.Sum(row => row.Cells["GTxtTaxableAmount"].Value.GetDecimal());

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
        if (DGrid.CurrentRow == null)
        {
            return;
        }

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
        _design.GetSalesEntryGridDesign(DGrid, "SR");
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

        DGrid.GotFocus += OnEventHandler;
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
        TxtAltQty.TextChanged += TxtAltQty_TextChanged;

        TxtAltUnit = new MrGridTextBox(DGrid);

        TxtQty = new MrGridNumericTextBox(DGrid);
        TxtQty.Validated += OnTxtQtyOnValidated;
        TxtQty.TextChanged += TxtQty_TextChanged;

        TxtUnit = new MrGridTextBox(DGrid);

        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.KeyDown += OnTxtRateOnKeyDown;
        TxtRate.TextChanged += OnTxtRateOnTextChanged;
        TxtRate.Validating += OnTxtRateOnValidating;

        TxtBasicAmount = new MrGridNumericTextBox(DGrid);
        TxtBasicAmount.KeyDown += OnTxtBasicAmountOnKeyDown;
        TxtBasicAmount.TextChanged += OnTxtBasicAmountOnTextChanged;
        TxtBasicAmount.Validating += OnTxtBasicAmountOnValidating;

        TxtTermAmount = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtTermAmount.Enter += OnTxtTermAmountOnEnter;
        TxtTermAmount.TextChanged += OnTxtTermAmountOnTextChanged;
        TxtTermAmount.Validating += OnTxtTermAmountOnValidating;
        TxtNetAmount = new MrGridNumericTextBox(DGrid);
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
                    dataRow["Source"] = "SR";
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
                dataRow["Source"] = "SR";
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
            dataRow["TermAmt"] = ro["GTxtValueAmount"];
            dataRow["Source"] = "SR";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtBillTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }
    private void CashAndBankValidation(string ledgerType)
    {
        var partyInfo = new FrmPartyInfo(ledgerType, _dtPartyInfo, "SR");
        partyInfo.ShowDialog();
        if (_dtPartyInfo.Rows.Count > 0)
        {
            _dtPartyInfo.Rows.Clear();
        }

        _dtPartyInfo = partyInfo.PartyInfo;
    }
    private void FillInvoiceData(string voucherNo)
    {
        try
        {
            var dsSales = _entry.ReturnSalesInvoiceDetailsInDataSet(voucherNo);
            if (dsSales.Tables.Count > 0)
            {
                foreach (DataRow dr in dsSales.Tables[0].Rows)
                {
                    MskDate.Text = ObjGlobal.IsIrdRegister ? DateTime.Now.GetDateString() : dr["Invoice_Date"].GetDateString();
                    MskMiti.Text = ObjGlobal.IsIrdRegister ? DateTime.Now.GetNepaliDate() : dr["Invoice_Miti"].ToString();

                    TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());
                    MskRefDate.Text = dr["Invoice_Miti"].ToString();

                    //TxtChallan.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                    //TxtOrder.Text = Convert.ToString(dr["SO_Invoice"].ToString());

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

                if (dsSales.Tables[1].Rows.Count > 0)
                {
                    var iRow = 0;
                    DGrid.Rows.Clear();
                    DGrid.Rows.Add(dsSales.Tables[1].Rows.Count + 1);
                    foreach (DataRow dr in dsSales.Tables[1].Rows)
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

                        DGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = dr["SB_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtInvoiceSno"].Value = dr["Invoice_SNo"].ToString();

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

                        DGrid.CurrentCell = DGrid.Rows[iRow].Cells[0];
                        iRow++;
                    }
                }

                if (dsSales.Tables[2].Rows.Count > 0)
                {
                    var dtPTerm = dsSales.Tables[2];
                    _dtProductTerm.Rows.Clear();
                    foreach (DataRow ro in dtPTerm.Rows)
                    {
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

                if (dsSales.Tables[3].Rows.Count > 0)
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

                if (dsSales.Tables[4].Rows.Count > 0)
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

            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
            ObjGlobal.DGridColorCombo(DGrid);
            VoucherTotalCalculation();
            DGrid.ClearSelection();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }
    private void FillReturnInvoiceData(string voucherNo)
    {
        try
        {
            var dsSales = _entry.ReturnSalesReturnDetailsInDataSet(voucherNo);
            var dtMaster = dsSales.Tables[0];
            var dtDetails = dsSales.Tables[1];
            var dtProductTerm = dsSales.Tables[2];
            var dtBillTerm = dsSales.Tables[3];
            var dtTransport = dsSales.Tables[4];

            if (dsSales.Tables.Count > 0)
            {
                foreach (DataRow dr in dtMaster.Rows)
                {
                    if (!_actionTag.Equals("SAVE") && _isZoom)
                    {
                        TxtVno.Text = _txtZoomVno;
                    }

                    MskMiti.Text = dr["Invoice_Miti"].ToString();
                    MskDate.Text = dr["Invoice_Date"].GetDateString();
                    if (dr["SB_Invoice"].IsValueExits())
                    {
                        TxtRefVno.Text = Convert.ToString(dr["SB_Invoice"].ToString());
                        MskRefDate.Text = dr["SB_Miti"].ToString();
                    }

                    //TxtChallan.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                    //TxtOrder.Text = Convert.ToString(dr["SO_Invoice"].ToString());

                    TxtCustomer.Text = dr["GLName"].GetString();
                    _ledgerId = dr["Customer_ID"].GetLong();
                    TxtDueDays.Text = dr["DueDays"].ToString();
                    MskDueDays.Text = dr["DueDate"].GetDateString();

                    _dtPartyInfo.Rows.Clear();
                    if (_ledgerType.Contains(ledgerType))
                    {
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
                    }

                    var invoiceType = dr["Invoice_Type"].GetTrimReplace();
                    var invoiceMode = dr["Payment_Mode"].GetTrimReplace();

                    var index = CmbInvoiceType.FindString(invoiceType);
                    CmbInvoiceType.SelectedIndex = index;

                    index = CmbPaymentMode.FindStringExact(invoiceMode);
                    CmbPaymentMode.SelectedIndex = index;

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
                        DGrid.Rows[iRow].Cells["GTxtRate"].Value = dr["Rate"].GetRateDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtNarration"].Value = dr["Narration"].ToString();

                        //DGrid.Rows[iRow].Cells["GTxtChallanNo"].Value = dr["SC_Invoice"].ToString();
                        //DGrid.Rows[iRow].Cells["GTxtChallanSno"].Value = dr["Invoice_SNo"].ToString();
                        //DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["SO_Invoice"].ToString();
                        //DGrid.Rows[iRow].Cells["GTxtOrderSno"].Value = dr["SO_SNo"].ToString();

                        DGrid.Rows[iRow].Cells["GTxtInvoiceNo"].Value = dr["SB_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtInvoiceSno"].Value = dr["SB_Sno"].ToString();

                        var taxableAmount = dr["Tax_Amount"].GetDecimal();
                        var netAmount = dr["N_Amount"].GetDecimal();
                        var taxRate = dr["PTax"].GetDecimal();
                        if (taxableAmount == 0 && taxRate > 0)
                        {
                            taxableAmount = netAmount;
                        }

                        DGrid.Rows[iRow].Cells["GTxtTaxableAmount"].Value = taxableAmount;
                        DGrid.Rows[iRow].Cells["GTxtVatAmount"].Value = dr["V_Amount"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = dr["V_Rate"].GetDecimal() > 0 ? dr["V_Rate"].GetDecimal() : taxRate;

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
                                    foreach (var ro in dtProductTerm.Select($"ProductId='{productId}'"))
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
                            ? dr["BiltyDate"].GetDateString()
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
                            ? dr["ContractDate"].GetDateString()
                            : string.Empty;

                        TxtExportInvoiceNo.Text = dr["ExportInvoice"].ToString();
                        MskExportInvoiceDate.Text = !string.IsNullOrEmpty(dr["ExportInvoiceDate"].ToString())
                            ? dr["ExportInvoiceDate"].GetDateString()
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
    private void FillSalesInvoiceDetails(string voucherNo)
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
                    if (!_actionTag.Equals("SAVE") && _isZoom)
                    {
                        TxtVno.Text = _txtZoomVno;
                    }

                    MskMiti.Text = dr["Invoice_Miti"].ToString();
                    MskDate.Text = dr["Invoice_Date"].GetDateString();
                    if (dr["PB_Vno"].ToString() != string.Empty)
                    {
                        TxtRefVno.Text = Convert.ToString(dr["PB_Vno"].ToString());
                        MskRefDate.Text = dr["Vno_Miti"].ToString();
                    }

                    TxtChallan.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                    TxtOrder.Text = Convert.ToString(dr["SO_Invoice"].ToString());
                    TxtCustomer.Text = dr["GLName"].GetString();
                    _ledgerId = dr["Customer_ID"].GetLong();
                    TxtDueDays.Text = dr["DueDays"].ToString();
                    MskDueDays.Text = dr["DueDate"].GetDateString();

                    _dtPartyInfo.Rows.Clear();
                    if (_ledgerType.Contains(ledgerType))
                    {
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
                    }

                    var invoiceType = dr["Invoice_Type"].GetTrimReplace();
                    var invoiceMode = dr["Payment_Mode"].GetTrimReplace();

                    var index = CmbInvoiceType.FindString(invoiceType);
                    CmbInvoiceType.SelectedIndex = index;

                    index = CmbPaymentMode.FindStringExact(invoiceMode);
                    CmbPaymentMode.SelectedIndex = index;

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
                        DGrid.Rows[iRow].Cells["GTxtRate"].Value = dr["Rate"].GetRateDecimalString();
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

                        var taxableAmount = dr["Tax_Amount"].GetDecimal();
                        var netAmount = dr["N_Amount"].GetDecimal();
                        var taxRate = dr["PTax"].GetDecimal();
                        if (taxableAmount == 0 && taxRate > 0)
                        {
                            taxableAmount = netAmount;
                        }

                        DGrid.Rows[iRow].Cells["GTxtTaxableAmount"].Value = taxableAmount;
                        DGrid.Rows[iRow].Cells["GTxtVatAmount"].Value = dr["V_Amount"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtTaxPriceRate"].Value = dr["V_Rate"].GetDecimal() > 0 ? dr["V_Rate"].GetDecimal() : taxRate;

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
                                    foreach (var ro in dtProductTerm.Select($"ProductId='{productId}'"))
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
                            ? dr["BiltyDate"].GetDateString()
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
                            ? dr["ContractDate"].GetDateString()
                            : string.Empty;

                        TxtExportInvoiceNo.Text = dr["ExportInvoice"].ToString();
                        MskExportInvoiceDate.Text = !string.IsNullOrEmpty(dr["ExportInvoiceDate"].ToString())
                            ? dr["ExportInvoiceDate"].GetDateString()
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
    private void DeletedRowExitsOrNot(int sno, long productId)
    {
        if (_dtProductTerm.RowsCount() <= 0)
        {
            return;
        }

        if (DGrid.CurrentRow == null)
        {
            return;
        }

        var exDetails = _dtProductTerm.AsEnumerable().Any(c =>
            c.Field<string>("ProductSno").Equals(sno.ToString()) &&
            c.Field<string>("ProductId").Equals(productId.ToString()));
        if (!exDetails)
        {
            return;
        }

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
    private void PreviewImage(PictureBox pictureBox)
    {
        if (pictureBox.BackgroundImage == null)
        {
            return;
        }

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
    private void BindDataInComboBox()
    {
        _master.BindVoucherType(CmbInvoiceType);
        CmbInvoiceType.SelectedIndex = 0;

        _master.BindPaymentType(CmbPaymentMode);
        CmbPaymentMode.SelectedIndex = 0;
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
        if (TxtVno.IsBlankOrEmpty() || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            return string.Empty;
        }
        decimal termAmount = 0;
        decimal basicAmount = LblTotalBasicAmount.GetDecimal();
        decimal termTaxableAmount = _tableAmount;
        var term = _master.GetTermCalculationForVoucher("SB");
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
            var termId = ro["TermId"].GetInt();
            var exDetails = exitsTerm.AsEnumerable().Any(c => c.Field<string>("TermId").Equals(termId.ToString()));
            if (exDetails)
            {
                var dtAmount = exitsTerm.Select($" TermId='{termId}'").CopyToDataTable();
                exitRate = dtAmount.Rows[0]["TermRate"].GetDecimal();
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
            if (CmbInvoiceType.Text.Equals("P VAT") && ObjGlobal.SalesVatTermId.Equals(termId))
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
            var termRatio = amount > 0 ? amount / basicAmount * termTaxableAmount : 0;
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

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private bool _isBatch;

    private bool _isProvision;
    private bool _isZoom;
    private bool _isPTermExits;
    private bool _isBTermExits;
    private bool _isTaxable;

    private string _actionTag = string.Empty;
    private string _mskOrderDate = string.Empty;
    private string _mskChallanDate = string.Empty;
    private string _invoiceType = string.Empty;
    private string _numberScheme = string.Empty;
    private string _txtZoomVno = string.Empty;

    private string _description = string.Empty;

    private string _batchNo = string.Empty;
    private string _mfgDate = string.Empty;
    private string _expDate = string.Empty;
    private string ledgerType;
    private string _productType;

    private string[] _tagStrings = ["DELETE", "REVERSE"];
    private string[] _ledgerType = ["CASH", "BANK"];



    private decimal _conQty;
    private decimal _freeQty;
    private decimal _tableAmount = 0;
    private decimal _productTaxRate = 0;
    private decimal _conAltQty;

    private DataTable _dtPartyInfo;
    private DataTable _dtProductTerm;
    private DataTable _dtBillTerm;
    private DataTable _dtBatchInfo;
    private DataTable _dtProduct = new();
    private DataTable _dtLedger = new();
    private KeyPressEventArgs _getKeys;

    private readonly ISalesEntry _entry = new ClsSalesEntry();
    private readonly ISalesReturn _salesReturn = new SalesReturnRepository();
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