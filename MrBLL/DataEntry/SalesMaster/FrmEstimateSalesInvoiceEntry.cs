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
using MrDAL.DataEntry.Interface.SalesMaster;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.dbMaster;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.SalesMaster;

public partial class FrmEstimateSalesInvoiceEntry : MrForm
{
    //SALES INVOICE ENTRY

    #region --------------- SALES INVOICE ENTRY ---------------

    public FrmEstimateSalesInvoiceEntry(bool zoom, string txtZoomVno)
    {
        InitializeComponent();
        _dtPartyInfo = _master.GetPartyInfo();
        _dtBillTerm = _master.GetBillingTerm();
        _dtProductTerm = _master.GetBillingTerm();
        _txtZoomVno = txtZoomVno;
        _isZoom = zoom;
        _isPTermExits = _master.IsBillingTermExitsOrNot("SB", "P");
        _isBTermExits = _master.IsBillingTermExitsOrNot("SB", "B");
        _dtBatchInfo = _master.GetProductBatchFormat();
        BindDataInComboBox();
        DesignGridColumnsAsync();
        EnableControl();
        ClearControl();
        _invoiceRepository = new SalesInvoiceRepository();
    }

    private void FrmEstimateSalesInvoiceEntry_Load(object sender, EventArgs e)
    {
        if (_isZoom && _txtZoomVno.IsValueExits())
        {
            FillInvoiceData(_txtZoomVno);
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmEstimateSalesInvoiceEntry_KeyPress(object sender, KeyPressEventArgs e)
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

    private void FrmEstimateSalesInvoiceEntry_Shown(object sender, EventArgs e)
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
                FillInvoiceData(_txtZoomVno);
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
                FillInvoiceData(_txtZoomVno);
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
        PrintVoucher(false);
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
                FillInvoiceData(_txtZoomVno);
            }
        }
        TxtVno.Focus();
    }

    private void BtnReturnInvoice_Click(object sender, EventArgs e)
    {
        var result = new FrmSalesReturnEntry(true, "");
        result.ShowDialog();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        BtnNew.PerformClick();
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        FillInvoiceData(TxtVno.Text);
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
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SB");
        if (_actionTag == "SAVE")
        {
            return;
        }
        ClearControl();
        TxtVno.Text = voucherNo;
        FillInvoiceData(TxtVno.Text);
        TxtVno.Focus();
    }

    private void BtnChallan_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SC", "OSC");
        if (voucherNo.IsValueExits())
        {
            TxtChallan.Text = voucherNo;
            FillChallanData(TxtChallan.Text);
        }
        TxtChallan.Focus();
    }

    private void BtnOrder_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "SO", "O");
        if (result.IsValueExits())
        {
            TxtOrder.Text = result;
            FillOrderData(TxtOrder.Text);
        }
        TxtOrder.Focus();
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
        if (!TxtBillTermAmount.Enabled || DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
        {
            return;
        }
        var status = _dtBillTerm.RowsCount() > 0 ? "UPDATE" : "SAVE";
        var result = new FrmTermCalculation(status, LblTotalBasicAmount.GetDecimal(), "SB", _dtBillTerm, _tableAmount, LblTotalQty.GetDecimal(), CmbInvoiceType.Text);
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
            if (SaveSalesInvoice() != 0)
            {
                if (!_actionTag.Equals("DELETE") && !_actionTag.Equals("REVERSE"))
                {
                    PrintVoucher();
                }

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
        e.Handled = true;
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
        if (_actionTag.IsValueExits() && TxtVno.IsValueExits())
        {
            var dtVoucher = _entry.CheckVoucherNoExitsOrNot("AMS.SB_Master", "SB_Invoice", TxtVno.Text);
            if (dtVoucher.RowsCount() > 0 && _actionTag.Equals("SAVE"))
            {
                TxtVno.WarningMessage("INVOICE NUMBER ALREADY EXITS..!!");
            }
            else if (dtVoucher.Rows.Count <= 0 && !_actionTag.Equals("SAVE"))
            {
                TxtVno.WarningMessage("INVOICE NUMBER NOT EXITS..!!");
            }
        }
        else
        {
            if (_actionTag.IsValueExits() && TxtVno.IsBlankOrEmpty())
            {
                if (TxtVno.ValidControl(ActiveControl))
                {
                    TxtVno.WarningMessage("VOUCHER NUMBER IS REQUIRED FOR THE INVOICE..!!");
                    return;
                }
            }
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
                MskMiti.Text = MskMiti.GetNepaliDate(MskDate.Text);
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
            else
            {
                if (TxtChallan.Enabled)
                {
                    TxtChallan.Focus();
                }
                else if (TxtOrder.Enabled)
                {
                    TxtOrder.Focus();
                }
                else
                {
                    CmbInvoiceType.Focus();
                }
            }
        }
    }

    private void MskRefDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (MskRefDate.MaskCompleted)
            {
                if (TxtChallan.Enabled)
                {
                    TxtChallan.Focus();
                }
                else if (TxtOrder.Enabled)
                {
                    TxtOrder.Focus();
                }
                else
                {
                    CmbInvoiceType.Focus();
                }
            }
            else
            {
                return;
            }
        }
    }

    private void TxtChallan_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnChallan_Click(sender, e);
        }
        else if (e.KeyData is Keys.Enter)
        {
            if (ObjGlobal.SalesChallanMandatory && TxtChallan.IsBlankOrEmpty())
            {
                TxtChallan.WarningMessage("SALES GRN NUMBER IS REQUIRED FOR INVOICE");
                return;
            }
            else
            {
                if (TxtOrder.Enabled)
                {
                    TxtOrder.Focus();
                }
                else
                {
                    CmbPaymentMode.Focus();
                }
            }
        }
    }

    private void TxtChallan_Validating(object sender, CancelEventArgs e)
    {
        if (TxtChallan.IsBlankOrEmpty() && TxtChallan.Enabled && ObjGlobal.SalesChallanMandatory)
        {
            if (_actionTag.IsValueExits() && TxtChallan.ValidControl(ActiveControl))
            {
                TxtChallan.WarningMessage("SALES GRN NUMBER IS REQUIRED FOR INVOICE");
            }
        }
    }

    private void TxtOrder_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnOrder_Click(sender, e);
        }
        else if (e.KeyData is Keys.Enter)
        {
            if (ObjGlobal.SalesOrderMandatory && TxtOrder.IsBlankOrEmpty())
            {
                TxtOrder.WarningMessage("SALES GRN NUMBER IS REQUIRED FOR INVOICE");
                return;
            }
            else
            {
                CmbPaymentMode.Focus();
            }
        }
    }

    private void TxtOrder_Validating(object sender, CancelEventArgs e)
    {
        if (TxtOrder.IsBlankOrEmpty() && TxtOrder.Enabled && ObjGlobal.SalesOrderMandatory)
        {
            if (_actionTag.IsValueExits() && TxtOrder.ValidControl(ActiveControl))
            {
                TxtOrder.WarningMessage("SALES ORDER NUMBER IS REQUIRED FOR INVOICE");
                return;
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

    private void TxtCustomer_Validating(object sender, CancelEventArgs e)
    {
        if (TxtCustomer.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            if (ActiveControl == BtnExit || ActiveControl == BtnVendor)
            {
                return;
            }

            if (TxtCustomer.ValidControl(ActiveControl))
            {
                TxtCustomer.WarningMessage("CUSTOMER IS REQUIRED FOR INVOICE..!!");
                return;
            }
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
            BtnDepartment_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var (description, i) = GetMasterList.CreateDepartment(true);
            if (i > 0)
            {
                TxtDepartment.Text = description;
                _departmentId = i;
            }
            TxtDepartment.Focus();
        }
        else if (e.KeyCode is Keys.Enter)
        {
            if (TxtDepartment.IsBlankOrEmpty() && ObjGlobal.SalesDepartmentMandatory)
            {
                TxtDepartment.WarningMessage("DEPARTMENT IS MANDATORY PLEASE SELECT DEPARTMENT");
                TxtDepartment.Focus();
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
    private void TxtNetAmount_TextChanged(object sender, EventArgs e)
    {
        VoucherTotalCalculation();
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
        var result = new FrmTermCalculation(tag, TxtBasicAmount.Text, true, "SB", _productId, serialNo,
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
        if (ActiveControl == TxtRate || TxtTermAmount.Enabled)
        {
            return;
        }
        OnTxtBasicAmountOnTextChanged(sender, e);
        TxtRate.Text = TxtBasicAmount.GetDecimal() is 0
            ? 0.GetDecimalString()
            : (TxtBasicAmount.GetDecimal() / TxtQty.GetDecimal()).GetRateDecimalString();
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
        TxtRate.Text = TxtRate.GetRateDecimalString();
        TxtBasicAmount.Text = (TxtQty.GetDecimal() * TxtRate.GetDecimal()).GetDecimalString();
        TxtNetAmount.Text = (TxtBasicAmount.GetDecimal() + TxtTermAmount.GetDecimal()).GetDecimalString();
        if (ActiveControl == TxtQty)
        {
            return;
        }
        if (TxtTermAmount.Enabled)
        {
            return;
        }

        if (!TxtProduct.Enabled || !TxtProduct.IsValueExits())
        {
            return;
        }

        if (TxtBasicAmount.Enabled)
        {
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
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
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
    }

    private void TxtQty_Validating(object sender, CancelEventArgs e)
    {
        if (TxtQty.GetDecimal() is 0 && TxtQty.Enabled && TxtProduct.IsValueExits())
        {
            if (TxtQty.ValidControl(ActiveControl))
            {
                TxtQty.WarningMessage("PRODUCT OPENING QTY CANNOT BE ZERO");
                return;
            }
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
        if (TxtAltQty.Enabled && TxtAltQty.GetDecimal() > 0)
        {
            TxtAltQty.Text = TxtAltQty.GetDecimalQtyString();
        }

        TxtBasicAmount.Text = (TxtRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
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
        if (DGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
        }

        if (DGrid.RowCount == 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtProductId"].Value.GetLong() is 0)
            {
                if (_getKeys.KeyChar is (char)Keys.Enter) _getKeys.Handled = false;
                this.NotifyValidationError(TxtProduct, "PLEASE SELECT PRODUCT LEDGER FOR OPENING");
                return;
            }
        }

        if (DGrid.RowCount >= 1 && TxtProduct.Enabled && TxtProduct.IsBlankOrEmpty())
        {
            EnableGridControl();
            DGrid.ClearSelection();
            if (_getKeys is
                {
                    KeyChar: (char)Keys.Escape
                })
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
            var (description, id) = GetMasterList.GetCounterProduct(TxtShortName.Text);
            if (id > 0)
            {
                TxtProduct.Text = description;
                _productId = id;
                SetProductInfo();
            }
            TxtProduct.Focus();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
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
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateProduct(true);
            if (id > 0)
            {
                _productId = id;
                TxtProduct.Text = description;
                SetProductInfo();
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
                SetProductInfo();
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
                _productId = id;
                TxtProduct.Text = description;
                SetProductInfo();
            }
            TxtProduct.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtProduct, OpenProductList);
        }
    }

    #endregion --------------- SALES INVOICE ENTRY ---------------

    // METHOD FOR THIS FORM

    private int SaveCashAndBankDetails()
    {
        try
        {
            if (_ledgerType.Contains(ledgerType))
            {
                return 0;
            }
            _financeEntry.CbMaster.VoucherMode = "SB";
            _financeEntry.CbMaster.Voucher_No = TxtVno.Text;
            _financeEntry.CbMaster.Voucher_Date = MskDate.Text.GetDateTime();
            _financeEntry.CbMaster.Voucher_Miti = MskMiti.Text;
            _financeEntry.CbMaster.Voucher_Time = DateTime.Now;
            _financeEntry.CbMaster.Ref_VNo = TxtRefVno.Text;
            _financeEntry.CbMaster.Ref_VDate = !string.IsNullOrEmpty(TxtRefVno.Text.Trim())
                ? DateTime.Parse(MskRefDate.Text) : DateTime.Now;
            _financeEntry.CbMaster.VoucherType = "ALL";
            _financeEntry.CbMaster.Ledger_Id = CmbPaymentMode.Text.GetUpper().Equals("BANK")
                ? ObjGlobal.FinanceBankLedgerId.GetLong() : ObjGlobal.FinanceCashLedgerId.GetLong();
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
            _financeEntry.CbMaster.SyncRowVersion = _entry.SbMaster.SyncRowVersion;
            _financeEntry.CbDetails.Voucher_No = TxtVno.Text;
            _financeEntry.CbDetails.Ledger_Id = _ledgerId;
            _financeEntry.CbDetails.Subledger_Id = _subLedgerId;
            _financeEntry.CbDetails.Agent_Id = _agentId;
            _financeEntry.CbDetails.Cls1 = _departmentId;
            _financeEntry.CbDetails.Debit = 0;
            _financeEntry.CbDetails.LocalDebit = 0;
            _financeEntry.CbDetails.Credit = LblTotalNetAmount.GetDecimal();
            _financeEntry.CbDetails.LocalCredit = LblTotalLocalNetAmount.GetDecimal();
            _financeEntry.CbDetails.Narration = $"BEING PAYMENT RECEIVED AGAINST SALES INVOICE NO : {TxtVno.Text}";
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

    private int SaveSalesInvoice()
    {
        try
        {
            if (_actionTag is "SAVE" && TxtVno.IsDuplicate("SB", _actionTag))
            {
                TxtVno.Text = TxtVno.GetCurrentVoucherNo("SB", _numberScheme);
            }

            _invoiceRepository.SbMaster.SB_Invoice = TxtVno.Text;
            _invoiceRepository.SbMaster.Invoice_Date = DateTime.Parse(MskDate.Text);
            _invoiceRepository.SbMaster.Invoice_Miti = MskMiti.Text.Contains("/") ? MskMiti.Text : MskMiti.Text.Replace("-", "/");
            _invoiceRepository.SbMaster.PB_Vno = TxtRefVno.Text;
            _invoiceRepository.SbMaster.Vno_Miti = MskRefDate.Text.Contains("/") ? MskRefDate.Text : MskRefDate.Text.Replace("-", "/");
            _invoiceRepository.SbMaster.Vno_Date = MskRefDate.GetEnglishDate(MskRefDate.Text).GetDateTime();
            _invoiceRepository.SbMaster.Invoice_Time = DateTime.Now;
            _invoiceRepository.SbMaster.Customer_Id = _ledgerId;

            _invoiceRepository.SbMaster.Party_Name = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["PartyName"].ToString() : string.Empty;
            _invoiceRepository.SbMaster.Vat_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["VatNo"].ToString() : string.Empty;
            _invoiceRepository.SbMaster.Contact_Person = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ContactPerson"].ToString() : string.Empty;
            _invoiceRepository.SbMaster.Mobile_No = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Mob"].ToString() : string.Empty;
            _invoiceRepository.SbMaster.Address = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["Address"].ToString() : string.Empty;
            _invoiceRepository.SbMaster.ChqNo = _dtPartyInfo.Rows.Count > 0 ? _dtPartyInfo.Rows[0]["ChequeNo"].ToString() : string.Empty;
            _invoiceRepository.SbMaster.ChqDate = _dtPartyInfo.Rows.Count > 0 && _dtPartyInfo.Rows[0]["ChequeNo"].IsValueExits() ? _dtPartyInfo.Rows[0]["ChequeDate"].GetDateTime() : DateTime.Now;

            _invoiceRepository.SbMaster.Invoice_Type = CmbInvoiceType.Text;
            _invoiceRepository.SbMaster.Invoice_Mode = "AIMS";
            _invoiceRepository.SbMaster.Payment_Mode = CmbPaymentMode.Text;
            _invoiceRepository.SbMaster.DueDays = TxtDueDays.Text.GetInt();
            _invoiceRepository.SbMaster.DueDate = MskDueDays.MaskCompleted ? MskDueDays.GetEnglishDate(MskDueDays.Text).GetDateTime() : DateTime.Now;
            _invoiceRepository.SbMaster.Agent_Id = _agentId;
            _invoiceRepository.SbMaster.Subledger_Id = _subLedgerId;
            _invoiceRepository.SbMaster.SO_Invoice = TxtOrder.Text;
            _invoiceRepository.SbMaster.SO_Date = _mskOrderDate.GetDateTime();
            _invoiceRepository.SbMaster.SC_Invoice = TxtChallan.Text;
            _invoiceRepository.SbMaster.SC_Date = _mskChallanDate.GetDateTime();
            _invoiceRepository.SbMaster.Cls1 = _departmentId;
            _invoiceRepository.SbMaster.Cls2 = 0;
            _invoiceRepository.SbMaster.Cls3 = 0;
            _invoiceRepository.SbMaster.Cls4 = 0;
            _invoiceRepository.SbMaster.Cur_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
            _invoiceRepository.SbMaster.Cur_Rate = TxtCurrencyRate.Text.GetDecimal() > 0 ? TxtCurrencyRate.Text.GetDecimal() : 1;
            _invoiceRepository.SbMaster.CounterId = 0;
            _invoiceRepository.SbMaster.B_Amount = LblTotalBasicAmount.Text.GetDecimal();
            _invoiceRepository.SbMaster.T_Amount = TxtBillTermAmount.Text.GetDecimal();
            _invoiceRepository.SbMaster.N_Amount = LblTotalNetAmount.Text.GetDecimal();
            _invoiceRepository.SbMaster.LN_Amount = LblTotalLocalNetAmount.Text.GetDecimal();
            _invoiceRepository.SbMaster.Tender_Amount = 0;
            _invoiceRepository.SbMaster.Return_Amount = 0;
            _invoiceRepository.SbMaster.V_Amount = 0;
            _invoiceRepository.SbMaster.Tbl_Amount = 0;
            _invoiceRepository.SbMaster.Action_Type = _actionTag;
            _invoiceRepository.SbMaster.R_Invoice = false;
            _invoiceRepository.SbMaster.No_Print = 0;
            _invoiceRepository.SbMaster.In_Words = LblNumberInWords.Text;
            _invoiceRepository.SbMaster.Remarks = TxtRemarks.Text;
            _invoiceRepository.SbMaster.Audit_Lock = false;

            //_invoiceRepository.SbMaster.GetView = DGrid;
            //_invoiceRepository.SbMaster.dtBTerm = _dtBillTerm;
            //_invoiceRepository.SbMaster.dtPTerm = _dtProductTerm;
            //_invoiceRepository.SbMaster.ProductBatch = _dtBatchInfo;

            _invoiceRepository.SbMaster.PAttachment1 = PAttachment1.Image.ConvertImage();
            _invoiceRepository.SbMaster.PAttachment2 = PAttachment2.Image.ConvertImage();
            _invoiceRepository.SbMaster.PAttachment3 = PAttachment3.Image.ConvertImage();
            _invoiceRepository.SbMaster.PAttachment4 = PAttachment4.Image.ConvertImage();
            _invoiceRepository.SbMaster.PAttachment5 = PAttachment5.Image.ConvertImage();

            _invoiceRepository.SbOther.SB_Invoice = TxtVno.Text;
            _invoiceRepository.SbOther.Transport = TxtTransport.Text;
            _invoiceRepository.SbOther.VechileNo = TxtVechileNo.Text;
            _invoiceRepository.SbOther.BiltyNo = TxtBiltyNo.Text;
            _invoiceRepository.SbOther.Package = TxtPackage.Text;
            _invoiceRepository.SbOther.BiltyDate = MskBiltyDate.GetDateTime();
            _invoiceRepository.SbOther.BiltyType = CmbBiltyType.Text;
            _invoiceRepository.SbOther.Driver = TxtDriver.Text;
            _invoiceRepository.SbOther.PhoneNo = TxtPhoneNo.Text;
            _invoiceRepository.SbOther.LicenseNo = TxtLicenseNo.Text;
            _invoiceRepository.SbOther.MailingAddress = TxtMailingAddress.Text;
            _invoiceRepository.SbOther.MCity = TxtMCity.Text;
            _invoiceRepository.SbOther.MState = TxtMState.Text;
            _invoiceRepository.SbOther.MCountry = TxtMCountry.Text;
            _invoiceRepository.SbOther.MEmail = TxtMEmail.Text;
            _invoiceRepository.SbOther.ShippingAddress = TxtShippingAddress.Text;
            _invoiceRepository.SbOther.SCity = TxtSCity.Text;
            _invoiceRepository.SbOther.SState = TxtSState.Text;
            _invoiceRepository.SbOther.SCountry = TxtSCountry.Text;
            _invoiceRepository.SbOther.SEmail = TxtSEmail.Text;
            _invoiceRepository.SbOther.ContractNo = TxtContractNo.Text;
            _invoiceRepository.SbOther.ContractDate = MskContractNoDate.GetDateTime();
            _invoiceRepository.SbOther.ExportInvoice = TxtExportInvoiceNo.Text;
            _invoiceRepository.SbOther.ExportInvoiceDate = MskExportInvoiceDate.GetDateTime();
            _invoiceRepository.SbOther.ExportInvoice = TxtVendorOrderNo.Text;
            _invoiceRepository.SbOther.BankDetails = TxtBankDetails.Text;
            _invoiceRepository.SbOther.LcNumber = TxtLCNumber.Text;
            _invoiceRepository.SbOther.CustomDetails = TxtCustomName.Text;

            return _invoiceRepository.SaveSalesInvoice(_actionTag);
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
        var dt = _master.IsExitsCheckDocumentNumbering("SB");
        if (dt?.Rows.Count is 1)
        {
            _numberScheme = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("SB", _numberScheme);
            TxtVno.Enabled = ObjGlobal.IsIrdRegister;
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            var wnd = new FrmNumberingScheme("SB");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _numberScheme = wnd.Description;
            TxtVno.Text = wnd.VNo;
            TxtVno.Enabled = ObjGlobal.IsIrdRegister;
        }
    }

    private void ClearControl()
    {
        Text = !string.IsNullOrEmpty(_actionTag)
            ? $"SALES INVOICE DETAILS [{_actionTag}]"
            : "SALES INVOICE DETAILS";
        TxtVno.ReadOnly = !string.IsNullOrEmpty(_actionTag) && _actionTag != "SAVE";
        TxtVno.Clear();
        if (_actionTag.Equals("SAVE")) TxtVno.GetCurrentVoucherNo("SB", _numberScheme);
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
        CmbInvoiceType.SelectedIndex = 0;
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
        if (_dtBatchInfo.Rows.Count > 0)
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
        _isTaxable = false;
        _isBatch = false;

        _productId = _altUnitId = _godownId = _unitId = 0;
        _conQty = _freeQty = 0;

        TxtProduct.Clear();
        TxtShortName.Clear();
        TxtGodown.Clear();
        TxtAltQty.Enabled = false;
        TxtAltQty.Clear();
        TxtAltUnit.Clear();
        TxtQty.Clear();

        TxtUnit.Clear();
        TxtRate.Clear();
        TxtBasicAmount.Clear();
        TxtTermAmount.Clear();
        TxtNetAmount.Clear();

        _description = string.Empty;

        LblShortName.IsClear();
        LblAltStockQty.IsClear();
        LblStockQty.IsClear();
        LblSalesRate.IsClear();
        LblAltSalesRate.IsClear();

        if (_dtProduct.Columns.Count > 0)
        {
            _dtProduct.Clear();
        }
        AdjustControlsInDataGrid();
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = !_tagStrings.Contains(_actionTag) && !isEnable;

        BtnEdit.Enabled = BtnDelete.Enabled = BtnNew.Enabled && !ObjGlobal.IsIrdRegister;

        BtnEdit.Visible = BtnDelete.Visible = !ObjGlobal.IsIrdRegister;

        TxtVno.Enabled = BtnVno.Enabled = isEnable || _tagStrings.Contains(_actionTag);
        if (TxtVno.Enabled && _actionTag != "REVERSE")
        {
            TxtVno.Enabled = !ObjGlobal.IsIrdRegister;
        }
        MskDate.Enabled = MskMiti.Enabled = isEnable && !ObjGlobal.IsIrdApproved.Equals("YES");

        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TxtChallan.Enabled = BtnChallanNo.Enabled = isEnable && ObjGlobal.SalesChallanEnable;
        MskChallanDate.Enabled = MskChallanMiti.Enabled = false;
        TxtOrder.Enabled = BtnOrderNo.Enabled = isEnable && ObjGlobal.SalesOrderEnable;
        MskOrderDate.Enabled = MskOrderMiti.Enabled = false;
        CmbPaymentMode.Enabled = CmbInvoiceType.Enabled = isEnable;
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
            _productId = id;
            TxtProduct.Text = description;
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
        var dtDesign = _master.GetPrintVoucherList("SB");
        if (dtDesign.Rows.Count > 0)
        {
            var type = dtDesign.Rows[0]["DesignerPaper_Name"].ToString();
            var isOnline = dtDesign.Rows[0]["Is_Online"].GetBool();
            if (!isOnline && isInvoice)
            {
                return;
            }
            var frmDp = new FrmDocumentPrint(type, "SB", TxtVno.Text, TxtVno.Text, true)
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
        _isRowUpdate = true;
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSno"].Value.GetString();
        TxtProduct.Text = DGrid.Rows[_rowIndex].Cells["GTxtProduct"].Value.GetString();
        _productId = DGrid.Rows[_rowIndex].Cells["GTxtProductId"].Value.GetLong();

        TxtGodown.Text = DGrid.Rows[_rowIndex].Cells["GTxtGodown"].Value.GetString();
        _godownId = DGrid.Rows[_rowIndex].Cells["GTxtGodownId"].Value.GetInt();
        _altUnitId = DGrid.Rows[_rowIndex].Cells["GTxtAltUOMId"].Value.GetInt();
        TxtAltQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltQty"].Value.GetString();
        TxtAltUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtAltUOM"].Value.GetString();
        _unitId = DGrid.Rows[_rowIndex].Cells["GTxtUOMId"].Value.GetInt();
        TxtQty.Text = DGrid.Rows[_rowIndex].Cells["GTxtQty"].Value.GetString();
        TxtUnit.Text = DGrid.Rows[_rowIndex].Cells["GTxtUOM"].Value.GetString();
        TxtRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtRate"].Value.GetRateDecimalString();
        TxtBasicAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetString();
        TxtTermAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtTermAmount"].Value.GetString();
        TxtNetAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtNetAmount"].Value.GetString();
        _description = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        SetProductInfo(true);
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
            _isTaxable = _dtProduct.Rows[0]["PTax"].GetDecimal() > 0;
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
        if (DGrid.CurrentRow != null)
        {
            var serialNo = TxtSno.Text;
            var exDetails = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()));
            if (exDetails)
            {
                var exitAny = _dtBatchInfo.AsEnumerable().Any(c => c.Field<string>("ProductId").Equals(_productId.ToString()) && c.Field<string>("ProductSno").Equals(serialNo.Trim()));
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

        if (_productType.Equals("I"))
        {
            if (LblStockQty.GetDecimal() - TxtQty.GetDecimal() < 0 && ObjGlobal.StockNegativeStockBlock)
            {
                TxtProduct.WarningMessage("STOCK IS NEGATIVE & BLOCK TO BILL IN NEGATIVE..!!");
                return;
            }
            if (LblStockQty.GetDecimal() - TxtQty.GetDecimal() < 0 && ObjGlobal.StockNegativeStockWarning)
            {
                if (CustomMessageBox.Question("STOCK IS NEGATIVE. DO YOU WANT TO CONTINUE..!!") == DialogResult.No)
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
        DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = TxtOrder.Text;
        DGrid.Rows[iRows].Cells["GTxtOrderSno"].Value = DGrid.Rows[iRows].Cells["GTxtSNo"].Value;
        DGrid.Rows[iRows].Cells["GTxtChallanNo"].Value = TxtChallan.Text;
        DGrid.Rows[iRows].Cells["GTxtChallanSno"].Value = DGrid.Rows[iRows].Cells["GTxtSNo"].Value;

        DGrid.Rows[iRows].Cells["GTxtConFraction"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtFreeQty"].Value = _freeQty;
        DGrid.Rows[iRows].Cells["GTxtStockFreeQty"].Value = _freeQty;

        DGrid.Rows[iRows].Cells["GTxtFreeUnitId"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraStockQty"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtExtraFreeUnitId"].Value = string.Empty;

        DGrid.Rows[iRows].Cells["GTxtTaxPriceRate"].Value = string.Empty;
        DGrid.Rows[iRows].Cells["GTxtTaxGroupId"].Value = string.Empty;

        var taxAmount = (TxtNetAmount.GetDecimal() / 1.13.GetDecimal()).GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtVatAmount"].Value = _isTaxable ? taxAmount : 0;
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
        _design.GetSalesEntryGridDesign(DGrid, "SB");
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
        TxtQty.Validating += TxtQty_Validating;
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

        TxtTermAmount = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true
        };
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

        if (DGrid.CurrentRow == null)
        {
            return;
        }

        var serialNo = DGrid.CurrentRow.Cells["GTxtSno"].Value.GetInt();
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
                    dataRow["Source"] = "SB";
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
                dataRow["Source"] = "SB";
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
            _dtBillTerm.Rows.Clear();
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
            dataRow["Source"] = "SB";
            dataRow["Formula"] = string.Empty;
            dataRow["ProductSno"] = ro["GTxtProductSno"];
            _dtBillTerm.Rows.InsertAt(dataRow, _dtProductTerm.RowsCount() + 1);
        }
    }

    private void CashAndBankValidation(string type)
    {
        var partyInfo = new FrmPartyInfo(type, _dtPartyInfo, "SB");
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

    private void FillChallanData(string voucherNo)
    {
        try
        {
            var dsChallan = _entry.ReturnSalesChallanDetailsInDataSet(voucherNo);
            if (dsChallan.Tables.Count <= 0)
            {
                return;
            }
            var dtMaster = dsChallan.Tables[0];
            var dtDetails = dsChallan.Tables[1];
            var dtProductTerm = dsChallan.Tables[2];
            var dtBillTerm = dsChallan.Tables[3];
            var dtTransport = dsChallan.Tables[4];

            foreach (DataRow dr in dtMaster.Rows)
            {
                if (ObjGlobal.IsIrdApproved != "YES")
                {
                    MskMiti.Text = dr["Invoice_Miti"].ToString();
                    MskDate.Text = Convert.ToDateTime(dr["Invoice_Date"].ToString()).ToString("dd/MM/yyyy");
                }
                else
                {
                    MskDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    MskMiti.Text = ObjGlobal.ReturnNepaliDate(MskDate.Text);
                }

                if (dr["Ref_Date"].ToString() != string.Empty)
                {
                    TxtRefVno.Text = Convert.ToString(dr["Ref_VNo"].ToString());
                    MskRefDate.Text = dr["Ref_Date"].GetDateString();
                }

                if (_actionTag != "DELETE")
                {
                    TxtChallan.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                    _mskChallanDate = !string.IsNullOrEmpty(TxtChallan.Text)
                        ? dr["Invoice_Date"].GetDateString()
                        : DateTime.Now.GetDateString();
                    TxtOrder.Text = Convert.ToString(dr["SO_Invoice"].ToString());
                    _mskOrderDate = !string.IsNullOrEmpty(TxtOrder.Text)
                        ? dr["SO_Date"].GetDateString()
                        : DateTime.Now.GetDateString();
                }

                TxtCustomer.Text = Convert.ToString(dr["GLName"].ToString());
                _ledgerId = ObjGlobal.ReturnLong(dr["Customer_ID"].ToString());
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
                if (!string.IsNullOrEmpty(dr["DName"].ToString()))
                {
                    TxtDepartment.Text = dr["DName"].ToString();
                    _departmentId = dr["Cls1"].GetInt();
                }

                if (dr["Cur_Id"].ToString() != string.Empty)
                {
                    _currencyId = dr["Cur_Id"].GetInt();
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
                    DGrid.Rows[iRow].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimal();
                    DGrid.Rows[iRow].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimal();
                    DGrid.Rows[iRow].Cells["GTxtNarration"].Value = dr["Narration"].ToString();

                    if (_actionTag != "SAVE")
                    {
                        DGrid.Rows[iRow].Cells["GTxtChallanNo"].Value = dr["SC_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtChallanSno"].Value = dr["Invoice_SNo"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["SO_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtOrderSno"].Value = dr["SO_SNo"].ToString();
                    }

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

                if (dtBillTerm.Rows.Count > 0)
                {
                    var dtBTerm = dsChallan.Tables[3];
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
                    foreach (DataRow dr in dsChallan.Tables[4].Rows)
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
            var dsSales = _entry.ReturnSalesOrderDetailsInDataSet(voucherNo);
            var dtMaster = dsSales.Tables[0];
            var dtDetails = dsSales.Tables[1];
            var dtProductTerm = dsSales.Tables[2];
            var dtBillTerm = dsSales.Tables[3];
            var dtTransport = dsSales.Tables[4];

            if (dsSales.Tables.Count > 0)
            {
                foreach (DataRow dr in dtMaster.Rows)
                {
                    if (dr["Ref_Vno"].ToString() != string.Empty)
                    {
                        TxtRefVno.Text = Convert.ToString(dr["Ref_Vno"].ToString());
                        MskRefDate.Text = dr["Ref_Miti"].ToString();
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
                        DGrid.Rows[iRow].Cells["GTxtRate"].Value = dr["Rate"].GetRateDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtAmount"].Value = dr["B_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtTermAmount"].Value = dr["T_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtNetAmount"].Value = dr["N_Amount"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtAltStockQty"].Value = dr["AltStock_Qty"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtStockQty"].Value = dr["Stock_Qty"].GetDecimalString();
                        DGrid.Rows[iRow].Cells["GTxtNarration"].Value = dr["Narration"].ToString();

                        DGrid.Rows[iRow].Cells["GTxtOrderNo"].Value = dr["SO_Invoice"].ToString();
                        DGrid.Rows[iRow].Cells["GTxtOrderSno"].Value = dr["Invoice_SNo"].ToString();

                        DGrid.Rows[iRow].Cells["GTxtChallanNo"].Value = string.Empty;
                        DGrid.Rows[iRow].Cells["GTxtChallanSno"].Value = 0;

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
                        MskBiltyDate.Text = !string.IsNullOrEmpty(dr["BiltyDate"].ToString()) ? Convert.ToDateTime(dr["BiltyDate"].ToString()).ToString("dd/MM/yyyy") : string.Empty;
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
                        MskContractNoDate.Text = !string.IsNullOrEmpty(dr["ContractDate"].ToString()) ? Convert.ToDateTime(dr["ContractDate"].ToString()).ToString("dd/MM/yyyy") : string.Empty;
                        TxtExportInvoiceNo.Text = dr["ExportInvoice"].ToString();
                        MskExportInvoiceDate.Text = !string.IsNullOrEmpty(dr["ExportInvoiceDate"].ToString()) ? Convert.ToDateTime(dr["ExportInvoiceDate"].ToString()).ToString("dd/MM/yyyy") : string.Empty;
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

                        if (_actionTag != "SAVE")
                        {
                            TxtOrder.Text = dr["SO_Invoice"].ToString();
                            _mskOrderDate = string.IsNullOrEmpty(TxtOrder.Text.Trim()) switch
                            {
                                false => dr["SO_Date"].GetDateString(),
                                _ => _mskOrderDate
                            };
                            TxtChallan.Text = Convert.ToString(dr["SC_Invoice"].ToString());
                            _mskChallanDate = string.IsNullOrEmpty(TxtChallan.Text.Trim()) switch
                            {
                                false => dr["SC_Date"].GetDateString(),
                                _ => _mskChallanDate
                            };
                        }

                        TxtCustomer.Text = Convert.ToString(dr["GLName"].ToString());
                        _ledgerId = ObjGlobal.ReturnLong(dr["Vendor_ID"].ToString());
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

                    if (dsSales.Tables[1].Rows.Count > 0)
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
                            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[_columnIndex];
                            iRows++;
                        }

                        DGrid.ClearSelection();
                    }

                    if (dsSales.Tables[2].Rows.Count > 0) _dtProductTerm = dsSales.Tables[2];
                    if (dsSales.Tables[3].Rows.Count > 0) _dtBillTerm = dsSales.Tables[3];
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
                    MskDate.WarningMessage($"ENTER DATE MUST BE BETWEEN {ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}");
                    return false;
                }
            }
            else
            {
                MskDate.WarningMessage("ENTER VOUCHER DATE IS INVALID..!!");
                return false;
            }
        }
        else if (!MskDate.MaskCompleted)
        {
            MskDate.WarningMessage("ENTER VOUCHER DATE IS INVALID..!!");
            return false;
        }

        if (MskMiti.MaskCompleted)
        {
            if (MskMiti.Text.IsDateExits("M"))
            {
                if (!MskMiti.Text.IsValidDateRange("M"))
                {
                    MskMiti.WarningMessage($"ENTER MITI MUST BE BETWEEN {ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}");
                    return false;
                }
            }
            else
            {
                MskMiti.WarningMessage("ENTER VOUCHER MITI IS INVALID..!!");
                return false;
            }
        }
        else if (!MskDate.MaskCompleted)
        {
            MskMiti.WarningMessage("ENTER VOUCHER MITI IS INVALID..!!");
            return false;
        }

        if (DGrid.RowCount == 0 || DGrid.RowCount > 0 && !DGrid.Rows[0].Cells["GTxtProductId"].Value.IsValueExits())
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
    private bool _isBatch;
    private bool _isTaxable;

    private readonly bool _isProvision;
    private readonly bool _isZoom;
    private readonly bool _isPTermExits;
    private readonly bool _isBTermExits;

    private string _actionTag = string.Empty;
    private string _mskOrderDate = string.Empty;
    private string _mskChallanDate = string.Empty;
    private string _numberScheme = string.Empty;
    private readonly string _txtZoomVno;
    private string _invoiceType;
    private string _description = string.Empty;
    private readonly string _batchNo = string.Empty;
    private readonly string _mfgDate = string.Empty;
    private readonly string _expDate = string.Empty;

    private readonly string[] _tagStrings =
    [
        "DELETE", "REVERSE"
    ];

    private readonly string[] _ledgerType =
    [
        "CASH",
        "BANK"
    ];

    private string ledgerType;
    private string _productType;

    private decimal _conQty;
    private decimal _freeQty;
    private decimal _tableAmount = 0;

    private KeyPressEventArgs _getKeys;
    private DataTable _dtPartyInfo;
    private DataTable _dtProductTerm;
    private DataTable _dtBillTerm;

    private DataTable _dtLedger = new();
    private DataTable _dtProduct = new();
    private DataTable _dtBatchInfo;

    private readonly ISalesEntry _entry = new ClsSalesEntry();
    private readonly IFinanceEntry _financeEntry = new ClsFinanceEntry();
    private readonly IMasterSetup _master = new ClsMasterSetup();
    private readonly ISalesDesign _design = new SalesEntryDesign();
    private readonly ISalesInvoiceRepository _invoiceRepository;

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