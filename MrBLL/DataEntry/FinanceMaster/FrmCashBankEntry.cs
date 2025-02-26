using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.FinanceMaster;

public partial class FrmCashBankEntry : MrForm
{
    // CASH BANK VOUCHER
    #region --------------- LEDGER OPENING FORM ---------------
    public FrmCashBankEntry(bool zoom = false, string txtZoomVno = "", string voucherType = "CB", bool isProvision = false, bool isCash = true, bool multiCurrency = false)
    {
        InitializeComponent();
        _zoom = zoom;
        _zoomVno = txtZoomVno;
        _voucherType = voucherType;
        _isProvision = isProvision;
        _isCash = isCash;
        _multiCurrency = multiCurrency;

        _cashBank = new CashBankVoucherRepository();
        _setup = new ClsMasterSetup();
        _entry = new ClsFinanceEntry();
        _voucher = new FinanceDesign();

        GetGridColumns();
        AdjustControlsInDataGrid();
        EnableControl();
        ClearControl();
    }
    private void FrmCashBankEntry_Shown(object sender, EventArgs e)
    {
        if (BtnNew.Enabled)
        {
            BtnNew.Focus();
        }
        else
        {
            MskMiti.Focus();
        }
    }
    private void FrmCashBankEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
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
                if (TxtLedger.Enabled)
                {
                    ClearLedgerDetails();
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
    }
    private void FrmCashBankEntry_Load(object sender, EventArgs e)
    {
        if (_zoom && _zoomVno.IsValueExits() && _voucherType == "CB")
        {
            TxtVno.Text = _zoomVno;
            FillCashBankVoucher(TxtVno.Text);
        }
        else if (_zoom && _zoomVno.IsValueExits() && _voucherType == "PDC")
        {
            var isZoom = _zoom;
            var zoomVoucher = _zoomVno;
            BtnNew_Click(sender, e);
            TxtRefVno.Text = zoomVoucher;
            FillPdcVoucherDetails(TxtRefVno.Text);
            _zoom = isZoom;
        }
        else
        {
            if (!_zoom || _zoomVno.IsValueExits() || _voucherType != "PROV")
            {
                return;
            }
            _actionTag = "POST";
            ClearControl();
            FillCashBankVoucher(_zoomVno);
            BtnSave.Text = @"&POST";
            BtnEdit.Text = @"&POST";
            TxtVno.Focus();
            BtnSave.Focus();
        }

        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }
    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVno_Click(sender, e);
        }
        else if (TxtVno.ReadOnly)
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtVno, BtnVno);
        }
    }
    private void BtnVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "CB", "CONTRA");
        if (result.IsValueExits())
        {
            TxtVno.Text = result;
            if (_actionTag != "SAVE")
            {
                FillCashBankVoucher(TxtVno.Text);
            }
        }

        TxtVno.Focus();
    }
    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl.Name == "BtnVno") return;
        if (TxtVno.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS  BLANK OR INVALID..!!");
        }
        else if (_actionTag.Equals("SAVE"))
        {
            var dtVoucher = _cashBank.IsCheckVoucherNoExits("AMS.CB_Master", "Voucher_no", TxtVno.Text);
            if (dtVoucher.RowsCount() > 0) this.NotifyValidationError(TxtVno, "VOUCHER NUMBER ALREADY EXITS..!!");
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
                MskMiti.WarningMessage("ENTER VOUCHER MITI IS INVALID..!!");
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
                MskDate.WarningMessage("ENTER VOUCHER DATE IS INVALID..!!");
            }
        }
    }
    private void TxtRefNo_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtRefVno.IsBlankOrEmpty())
                TxtCashLedger.Focus();
            else
                GlobalEnter_KeyPress(sender, e);
        }
    }
    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.F1) return;
        if (_voucherType.Equals("PROV"))
        {
            TxtRefVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "CB", "PROV");
            if (TxtRefVno.IsValueExits())
            {
                FillCashBankVoucher(TxtRefVno.Text);
            }

            TxtRefVno.Focus();
        }
        else
        {
            TxtRefVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PDC", "DUE");
            if (TxtRefVno.IsValueExits())
            {
                FillPdcVoucherDetails(TxtRefVno.Text);
            }
            TxtRefVno.Focus();
        }
    }
    private void TxtCashLedger_Enter(object sender, EventArgs e)
    {
        if (TxtCashLedger.IsValueExits() && _cbLedgerId > 0)
        {
            SetCashLedgerInfo(_cbLedgerId);
        }
    }
    private void TxtCashLedger_Validating(object sender, CancelEventArgs e)
    {
        if (_balanceType.Equals("WARNING") && !ObjGlobal.FinanceNegativeWarning && LblCashBalance.GetDecimal() < 0)
        {
            TxtCashLedger.WarningMessage("LEDGER BALANCE IS NEGATIVE..!!");
        }
        if (_balanceType.Equals("BLOCK") && !ObjGlobal.FinanceNegativeWarning && LblCashBalance.GetDecimal() < 0)
        {
            TxtCashLedger.WarningMessage("LEDGER BALANCE IS NEGATIVE..!!");
            return;
        }
        if (TxtCashLedger.Enabled && DGrid.Enabled && TxtCashLedger.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCashLedger, "CASH & BANK LEDGER IS BLANK..!!");
        }
        if (TxtCashLedger.Enabled && _cbLedgerId > 0)
        {
            SetCashLedgerInfo(_cbLedgerId);
        }
    }
    private void TxtCashLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCashBank_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtCashLedger.Text, _cbLedgerId) = GetMasterList.CreateGeneralLedger(_isCash ? "CASH" : "BANK", true);
            SetCashLedgerInfo(_cbLedgerId);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCashLedger, BtnCashBank);
        }
    }
    private void BtnCashBank_Click(object sender, EventArgs e)
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag, "CASH", MskDate.Text, "MASTER");
        if (id > 0)
        {
            TxtLedger.Text = description;
            _cbLedgerId = id;
            SetCashLedgerInfo(_cbLedgerId);
        }
    }
    private void TxtChequeNo_Validating(object sender, CancelEventArgs e)
    {
        if (!TxtChequeNo.IsValueExits()) return;
        var dtCheque = _cashBank.IsCheckChequeNoExits(_actionTag, TxtVno.Text, TxtChequeNo.Text, _cbLedgerId);
        if (dtCheque.RowsCount() > 0) this.NotifyValidationError(TxtChequeNo, "CHEQUE NUMBER IS ALREADY EXITS..!!");
        if (MskChequeDate.MaskCompleted) return;
        MskChequeDate.Text = MskDate.Text;
        MskChequeMiti.Text = MskMiti.Text;
    }
    private void MskChequeDate_Validating(object sender, CancelEventArgs e)
    {
        if (MskChequeDate.MaskCompleted) MskChequeMiti.GetNepaliDate(MskDate.Text);
    }
    private void MskChequeMiti_Validating(object sender, CancelEventArgs e)
    {
        if (MskChequeMiti.MaskCompleted) MskChequeDate.GetEnglishDate(MskChequeMiti.Text);
    }
    private void TxtCashLedger_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtChequeNo.Enabled)
            {
                TxtChequeNo.Focus();
            }
            else if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else if (TxtCurrency.Enabled)
            {
                TxtCurrency.Focus();
            }
            else
            {
                TabLedgerOpening.Focus();
                DGrid.Focus();
            }
        }
    }
    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            OpenCurrency();
        else if (e.Control && e.KeyCode is Keys.N)
            (TxtCurrency.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.CreateCurrency(true);
        else
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCurrency, OpenCurrency);
    }
    private void TxtCurrency_KeyPress(object sender, KeyPressEventArgs e)
    {
    }
    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
    }
    private void TxtDepartment_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtCurrency.Enabled)
            {
                TxtCurrency.Focus();
            }
            else
            {
                TabLedgerOpening.Focus();
                DGrid.Focus();
            }
        }
    }
    private void BtnDepartment_Click(object sender, EventArgs e)
    {
    }

    private void TxtCurrencyRate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            TabLedgerOpening.Focus();
            DGrid.Focus();
        }
    }

    private void TxtNarration_Validating(object sender, CancelEventArgs e)
    {
        TxtLedger.TabStop = true;
        if (!TxtLedger.Enabled || ActiveControl == TxtPayment)
        {
            return;
        }


        if (!AddTextToGrid(_isRowUpdate)) return;
        TxtLedger.Focus();
    }

    private void TxtNarration_Validated(object sender, EventArgs e)
    {
        //if (ActiveControl.Name == "TxtPayment" || !TxtLedger.Enabled)
        //{
        //    return;
        //}
        //if (!AddTextToGrid(_isRowUpdate)) return;
        //TxtLedger.Focus();
        //return;
    }

    private void TxtNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode == Keys.Tab)
        {
            TxtPayment.Focus();
            SendToBack();
            return;
        }

        if (DGrid.CurrentRow != null && DGrid.CurrentRow.Index - 1 >= 0 && DGrid.Rows.Count > DGrid.CurrentRow.Index - 1)
        {
            TxtNarration.Text = e.KeyCode switch
            {
                Keys.F2 => DGrid.Rows.Count > 0 ? DGrid.Rows[DGrid.CurrentRow.Index - 1].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
                Keys.F3 => DGrid.Rows.Count > 0 ? DGrid.Rows[1].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
                Keys.F4 => DGrid.Rows.Count > 0 ? DGrid.Rows[2].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
                Keys.F5 => DGrid.Rows.Count > 0 ? DGrid.Rows[3].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
                Keys.F6 => DGrid.Rows.Count > 0 ? DGrid.Rows[DGrid.Rows.Count - 1].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
                _ => TxtNarration.Text
            };
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        if (DGrid.RowCount > 0)
        {
            var valueExits = DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong();
            if (valueExits > 0)
            {
                ClearControl();
            }
            else
            {
                BtnExit.PerformClick();
            }
        }
        else
        {
            BtnExit.PerformClick();
        }
    }

    private void TxtRemarks_Validating(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.FinanceRemarksEnable && TxtRemarks.IsBlankOrEmpty())
            this.NotifyValidationError(TxtRemarks, "REMARKS IS MANDATORY PLEASE ENTER REMARKS OF VOUCHER");
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.FinanceRemarksEnable && TxtRemarks.IsBlankOrEmpty())
            {
                e.SuppressKeyPress = true;
                return;
            }

            SendKeys.Send("{TAB}");
        }
    }

    private void GlobalEnter_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{Tab }");
    }

    private void MskChequeDate_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else if (TxtCurrency.Enabled)
            {
                TxtCurrency.Focus();
            }
            else
            {
                TabLedgerOpening.Focus();
                DGrid.Focus();
            }
        }
    }

    private void TxtLedgerOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenLedger();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtLedger.Text, _ledgerId) = GetMasterList.CreateGeneralLedger("Other", true);
            SetLedgerInfo(_ledgerId);
            TxtLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtLedger, OpenLedger);
        }
    }

    private void TxtLedgerOnLeave(object sender, EventArgs e)
    {
        if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() > 0 && TxtLedger.IsBlankOrEmpty())
        {
            EnableGridControl();
        }
    }

    private void TxtLedgerOnValidating(object sender, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtLedger, "PLEASE SELECT GENERAL LEDGER FOR OPENING");
        }

        if (DGrid.RowCount == 1 && TxtLedger.Enabled && TxtLedger.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() is 0)
            {
                if (!TxtSno.Visible)
                {
                    return;
                }
                if (_getKeys is { KeyChar: (char)Keys.Enter })
                {
                    _getKeys.Handled = false;
                }
                this.NotifyValidationError(TxtLedger, "PLEASE SELECT GENERAL LEDGER FOR OPENING");
                return;
            }
        }
        if (DGrid.RowCount >= 1 && TxtLedger.Enabled && TxtLedger.IsBlankOrEmpty())
        {
            EnableGridControl();
            DGrid.ClearSelection();
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

    private void TxtSubLedgerOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenSubLedger();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtSubLedger.Text, _subLedgerId) = GetMasterList.CreateSubLedger(true);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubLedger, OpenSubLedger);
        }
    }

    private void TxtAgentOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenAgent();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtAgent.Text, _agentId) = GetMasterList.CreateAgent(true);
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAgent, OpenAgent);
        }
    }

    private void TxtItemDepartmentOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenDepartment();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtDepartment.Text, _departmentId) = GetMasterList.CreateDepartment(true);
            TxtDepartment.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtDepartment, OpenDepartment);
        }
    }

    private void TxtPaymentOnValidating(object sender, CancelEventArgs e)
    {
        TxtPayment.Text = TxtPayment.GetDecimalString();
        TxtReceipt.Text = TxtPayment.GetDecimal() > 0 ? 0.GetDecimalString() : TxtReceipt.GetDecimalString();
    }

    private void TxtReceiptOnValidating(object sender, CancelEventArgs e)
    {
        TxtReceipt.Text = TxtReceipt.GetDecimalString();
        TxtPayment.Text = TxtReceipt.GetDecimal() > 0 ? 0.GetDecimalString() : TxtPayment.GetDecimalString();
    }

    private void TxtItemCurrencyRateOnTextChanged(object sender, EventArgs e)
    {
        VoucherTotalCalculation();
    }

    private void TxtItemCurrencyOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenItemCurrency();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtItemCurrency.Text, _itemCurrencyId, TxtItemCurrencyRate.Text) = GetMasterList.CreateCurrency(true);
            TxtItemCurrency.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtItemCurrency, OpenItemCurrency);
        }
    }


    // BUTTON CLICK EVENT
    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (IsValidForm())
        {
            if (SaveCashBankVoucher() > 0)
            {
                if (_zoom)
                {
                    Close();
                    DialogResult = DialogResult.OK;
                    return;
                }

                MessageBox.Show($@"{TxtVno.Text} VOUCHER NUMBER {_actionTag} SUCCESSFULLY..!!", ObjGlobal.Caption);
                ClearControl();
                BtnSave.Enabled = true;
            }

            if (_actionTag != "SAVE")
            {
                BtnVno.Enabled = TxtVno.Enabled = true;
                TxtVno.Clear();
                TxtVno.Focus();
            }
            else
            {
                if (string.IsNullOrEmpty(TxtVno.Text))
                    TxtVno.Focus();
                else
                    MskMiti.Focus();
            }
        }
        else
        {
            CustomMessageBox.ErrorMessage("ERROR OCCURS WHILE LEDGER OPENING..!!");
            BtnSave.Enabled = true;
        }
    }

    private void BtnDelete_Click(object sender, EventArgs e)
    {
        _actionTag = "DELETE";
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }

    private void BtnEdit_Click(object sender, EventArgs e)
    {
        _actionTag = "UPDATE";
        ClearControl();
        EnableControl(true);
        TxtVno.Focus();
    }

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        MskMiti.Focus();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
        _actionTag = "REVERSE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl();
        MskMiti.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "CB");
        FillCashBankVoucher(voucherNo);
        MskMiti.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintVoucher(false);
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }


    //DATA GRID CONTROL
    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No) return;
            _isRowDelete = true;
            if (DGrid.CurrentRow is { Index: >= 0 } && DGrid.Rows.Count > DGrid.CurrentRow.Index)
                DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
            if (DGrid.RowCount is 0) DGrid.Rows.Add();
            GetSerialNo();
            VoucherTotalCalculation();
        }

        if (e.KeyCode is Keys.Enter && !TxtLedger.Enabled)
        {
            e.SuppressKeyPress = true;
            AdjustControlsInDataGrid();
            EnableGridControl(true);
            ClearLedgerDetails();
            if (DGrid.Rows[_rowIndex].Cells["GTxtLedger"].Value.IsValueExits())
            {
                TextFromGrid();
                TxtLedger.Focus();
                return;
            }

            GetSerialNo();
            TxtLedger.Focus();
        }
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void DGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (CustomMessageBox.DeleteRow() is DialogResult.No)
            return;
        _isRowDelete = true;
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!_isRowDelete) return;
        if (DGrid.RowCount is 0) DGrid.Rows.Add();

        GetSerialNo();
    }

    private void OnDGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void OnDGridOnGotFocus(object sender, EventArgs e)
    {
        DGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }

    private void OnDGridOnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        if (DGrid.CurrentCell != null) _rowIndex = DGrid.CurrentCell.RowIndex.GetInt();
    }

    private void OnDGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        if (TxtLedger.Enabled) return;
        _rowIndex = e.RowIndex;
        AdjustControlsInDataGrid();
    }

    private void TxtPaymentOnTextChanged(object sender, EventArgs e)
    {
        if (TxtPayment.Focused)
        {
            TxtReceipt.Text = TxtPayment.GetDecimal() > 0 ? string.Empty : TxtReceipt.Text;
        }
    }

    private void TxtReceiptOnTextChanged(object sender, EventArgs e)
    {
        if (TxtReceipt.Focused)
        {
            TxtPayment.Text = TxtReceipt.GetDecimal() > 0 ? string.Empty : TxtPayment.Text;
        }
    }

    #endregion --------------- LEDGER OPENING FORM ---------------


    //METHOD FOR THIS FORM
    #region --------------- METHOD OF THIS FORM ---------------

    private void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("CV");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("CV", _docDesc);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("CV");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !_tagStrings.Contains(_actionTag) && !isEnable;

        TxtVno.Enabled = BtnVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtCashLedger.Enabled = BtnCashBank.Enabled = isEnable;
        TxtChequeNo.Enabled = MskChequeDate.Enabled = MskChequeMiti.Enabled = false;
        TxtCurrency.Enabled = BtnCurrency.Enabled = isEnable && ObjGlobal.FinanceCurrencyEnable;
        TxtCurrencyRate.Enabled = TxtCurrency.Enabled;
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TabLedgerOpening.Enabled = !_tagStrings.Contains(_actionTag) && isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.FinanceDepartmentEnable;
        TxtRemarks.Enabled = btnRemarks.Enabled = isEnable && ObjGlobal.FinanceRemarksEnable;
        EnableGridControl();
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? "CASH & BANK VOUCHER ENTRY"
            : $"CASH & BANK VOUCHER ENTRY [{_actionTag}]";
        _zoom = !_actionTag.Equals("SAVE") && _zoom;
        _zoomVno = _actionTag.Equals("SAVE") ? string.Empty : _zoomVno;
        if (!_zoom)
        {
            TxtVno.Clear();
            TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("CV", _docDesc) : TxtVno.Text;
            TxtVno.ReadOnly = !_actionTag.Equals("SAVE");
            if (BtnNew.Enabled)
            {
                MskDate.Text = ObjGlobal.SysCurrentDate
                    ? DateTime.Now.GetDateString()
                    : MskDate.GetLastVoucherDate("CB");
                MskMiti.Text = ObjGlobal.SysCurrentDate
                    ? DateTime.Now.GetNepaliDate()
                    : MskMiti.GetLastVoucherDate("CB").GetNepaliDate();
                MskRefDate.Text = MskMiti.Text;
            }

            _cbLedgerId = _isCash
                ? ObjGlobal.FinanceCashLedgerId.GetLong()
                : ObjGlobal.FinanceBankLedgerId.GetLong();
            TxtCashLedger.Text = _setup.GetLedgerDescription(_cbLedgerId);
            TxtRefVno.Clear();
            TxtRemarks.Clear();
            DGrid.ReadOnly = true;
            _currencyId = ObjGlobal.SysCurrencyId;
            TxtCurrency.Text = ObjGlobal.SysCurrency;
            TxtCurrencyRate.Text = 1.GetDecimalString();
            DGrid.ClearSelection();
            DGrid.Rows.Clear();
            ClearLedgerDetails();
        }

        DGrid.ClearSelection();
    }

    private void ClearLedgerDetails()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            TxtSno.Text = DGrid.RowCount.ToString();
        }

        _isRowUpdate = false;
        _ledgerId = 0;
        TxtLedger.Clear();
        _subLedgerId = 0;
        TxtSubLedger.Clear();
        _agentId = 0;
        TxtAgent.Clear();
        _departmentId = 0;
        TxtDepartment.Clear();
        _itemCurrencyId = 0;
        TxtItemCurrency.Clear();
        TxtItemCurrencyRate.Clear();
        LblPanNo.IsClear();
        LblBalance.IsClear();
        TxtReceipt.Clear();
        TxtPayment.Clear();
        TxtNarration.Clear();
        VoucherTotalCalculation();
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtLedger.Enabled = TxtLedger.Visible = isEnable;
        TxtSubLedger.Enabled = TxtSubLedger.Visible = isEnable && ObjGlobal.FinanceSubLedgerEnable;
        TxtItemDepartment.Enabled = TxtItemDepartment.Visible = isEnable && ObjGlobal.FinanceDepartmentItemEnable;
        TxtAgent.Enabled = TxtAgent.Visible = isEnable && ObjGlobal.FinanceAgentEnable;
        TxtItemCurrency.Enabled = TxtItemCurrency.Visible = isEnable && ObjGlobal.FinanceCurrencyEnable || isEnable && _multiCurrency;
        TxtItemCurrencyRate.Enabled = TxtItemCurrencyRate.Visible = isEnable && ObjGlobal.FinanceCurrencyEnable || isEnable && _multiCurrency;
        TxtReceipt.Enabled = TxtReceipt.Visible = isEnable;
        TxtPayment.Enabled = TxtPayment.Visible = isEnable;
        TxtNarration.Enabled = TxtNarration.Visible = isEnable;
    }

    private void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }
        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["Balance"].GetDecimalString();
    }

    private void SetCashLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }
        _cbLedgerId = dtLedger.Rows[0]["GLID"].GetInt();
        TxtCashLedger.Text = dtLedger.Rows[0]["GLName"].ToString();
        LblCashBalance.Text = dtLedger.Rows[0]["Balance"].GetDecimalString();
        _ledgerType = dtLedger.Rows[0]["GLType"].GetUpper();
        _balanceType = dtLedger.Rows[0]["CrTYpe"].GetUpper();
        TxtChequeNo.Enabled = MskChequeDate.Enabled = MskChequeMiti.Enabled = !_tagStrings.Contains(_actionTag) && _ledgerType.Equals("BANK");
    }

    private void OpenLedger()
    {
        (TxtLedger.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag);
        SetLedgerInfo(_ledgerId);
    }

    private void OpenSubLedger()
    {
        (TxtSubLedger.Text, _subLedgerId) = GetMasterList.GetSubLedgerList(_actionTag);
    }

    private void OpenAgent()
    {
        (TxtAgent.Text, _agentId) = GetMasterList.GetAgentList(_actionTag);
    }

    private void OpenDepartment()
    {
        (TxtDepartment.Text, _departmentId) = GetMasterList.GetDepartmentList(_actionTag);
    }

    private void OpenCurrency()
    {
        (TxtCurrencyRate.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
        TxtCurrencyRate.Focus();
    }

    private void OpenItemCurrency()
    {
        (TxtItemCurrency.Text, _itemCurrencyId, TxtItemCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
        TxtItemCurrency.Focus();
    }

    private void GetGridColumns()
    {
        _voucher.GetCashBankDesign(DGrid, "S");
        if (DGrid.ColumnCount > 0)
        {
            DGrid.Columns[@"GTxtCurrency"].Visible = _multiCurrency;
            DGrid.Columns[@"GTxtExchangeRate"].Visible = _multiCurrency;

            DGrid.Columns["GTxtLocalReceipt"].Visible = false;
            DGrid.Columns["GTxtLocalPayment"].Visible = false;

            DGrid.Columns["GTxtSubledger"].Visible = ObjGlobal.FinanceSubLedgerEnable;
            if (DGrid.Columns["GTxtSubledger"].Visible)
            {
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtSubledger"].Width;
            }
            else
            {
                DGrid.Columns["GTxtNarration"].Width += DGrid.Columns["GTxtSubledger"].Width;
            }

            DGrid.Columns["GTxtAgent"].Visible = ObjGlobal.FinanceAgentEnable;
            if (DGrid.Columns["GTxtAgent"].Visible)
            {
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtAgent"].Width;
            }
            else
            {
                DGrid.Columns["GTxtNarration"].Width += DGrid.Columns["GTxtAgent"].Width;
            }

            DGrid.Columns["GTxtDepartment"].Visible = ObjGlobal.FinanceDepartmentItemEnable;
            if (DGrid.Columns["GTxtDepartment"].Visible)
            {
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtDepartment"].Width;
            }
        }

        DGrid.RowEnter += OnDGridOnRowEnter;
        DGrid.RowsAdded += OnDGridOnRowsAdded;
        DGrid.GotFocus += OnDGridOnGotFocus;
        DGrid.CellEnter += OnDGridOnCellEnter;
        DGrid.KeyDown += DGrid_KeyDown;
        DGrid.UserDeletingRow += DGrid_UserDeletingRow;
        DGrid.EnterKeyPressed += DGrid_EnterKeyPressed;
        DGrid.UserDeletedRow += DGrid_UserDeletedRow;
        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtLedger = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtLedger.KeyDown += TxtLedgerOnKeyDown;
        TxtLedger.Leave += TxtLedgerOnLeave;
        TxtLedger.Validating += TxtLedgerOnValidating;
        TxtSubLedger = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtSubLedger.KeyDown += TxtSubLedgerOnKeyDown;
        TxtAgent = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtAgent.KeyDown += TxtAgentOnKeyDown;
        TxtItemDepartment = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtItemDepartment.KeyDown += TxtItemDepartmentOnKeyDown;

        TxtItemCurrency = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtItemCurrency.KeyDown += TxtItemCurrencyOnKeyDown;

        TxtItemCurrencyRate = new MrGridNumericTextBox(DGrid);
        TxtItemCurrencyRate.TextChanged += TxtItemCurrencyRateOnTextChanged;

        TxtReceipt = new MrGridNumericTextBox(DGrid);
        TxtReceipt.TextChanged += TxtReceiptOnTextChanged;
        TxtReceipt.Validating += TxtReceiptOnValidating;

        TxtPayment = new MrGridNumericTextBox(DGrid);
        TxtPayment.TextChanged += TxtPaymentOnTextChanged;
        TxtPayment.Validating += TxtPaymentOnValidating;

        TxtNarration = new MrGridNormalTextBox(DGrid);
        TxtNarration.KeyDown += TxtNarration_KeyDown;
        TxtNarration.KeyPress += (_, e) => e.Handled = false;
        TxtNarration.Validating += TxtNarration_Validating;
        TxtNarration.Validated += TxtNarration_Validated;

        ObjGlobal.DGridColorCombo(DGrid);
    }

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null) return;
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSNo"].Value.GetString();
        TxtLedger.Text = DGrid.Rows[_rowIndex].Cells["GTxtLedger"].Value.GetString();
        _ledgerId = DGrid.Rows[_rowIndex].Cells["GTxtLedgerId"].Value.GetLong();
        SetLedgerInfo(_ledgerId);
        TxtSubLedger.Text = DGrid.Rows[_rowIndex].Cells["GTxtSubLedger"].Value.GetString();
        _subLedgerId = DGrid.Rows[_rowIndex].Cells["GTxtSubledgerId"].Value.GetInt();
        TxtAgent.Text = DGrid.Rows[_rowIndex].Cells["GTxtAgent"].Value.GetString();
        _subLedgerId = DGrid.Rows[_rowIndex].Cells["GTxtAgentId"].Value.GetInt();
        TxtDepartment.Text = DGrid.Rows[_rowIndex].Cells["GTxtDepartment"].Value.GetString();
        _subLedgerId = DGrid.Rows[_rowIndex].Cells["GTxtDepartmentId"].Value.GetInt();
        TxtItemCurrency.Text = DGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.GetString();
        TxtItemCurrencyRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtExchangeRate"].Value.GetString();
        _itemCurrencyId = DGrid.Rows[_rowIndex].Cells["GTxtCurrencyId"].Value.GetInt();
        TxtReceipt.Text = DGrid.Rows[_rowIndex].Cells["GTxtReceipt"].Value.GetDecimalString();
        TxtPayment.Text = DGrid.Rows[_rowIndex].Cells["GTxtPayment"].Value.GetDecimalString();
        TxtNarration.Text = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        _isRowUpdate = true;
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = DGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtLedger"].Index;
        TxtLedger.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtLedger.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtLedger.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtSubLedger"].Index;
        TxtSubLedger.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSubLedger.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSubLedger.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAgent"].Index;
        TxtAgent.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAgent.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAgent.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtDepartment"].Index;
        TxtItemDepartment.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtItemDepartment.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtItemDepartment.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtCurrency"].Index;
        TxtItemCurrency.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtItemCurrency.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtItemCurrency.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtExchangeRate"].Index;
        TxtItemCurrencyRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtItemCurrencyRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtItemCurrencyRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtReceipt"].Index;
        TxtReceipt.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtReceipt.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtReceipt.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtPayment"].Index;
        TxtPayment.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPayment.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPayment.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtNarration"].Index;
        TxtNarration.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNarration.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNarration.TabIndex = columnIndex;
    }

    private bool AddTextToGrid(bool isUpdate)
    {
        if (ObjGlobal.FinanceAgentMandatory && TxtAgent.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtAgent, "AGENT IS MANDATORY..!!");
            return false;
        }

        if (ObjGlobal.FinanceSubLedgerMandatory && TxtSubLedger.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtSubLedger, "SUBLEDGER IS MANDATORY..!!");
            return false;
        }

        if (ObjGlobal.FinanceCurrencyMandatory && TxtCurrency.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtCurrency, "CURRENCY IS MANDATORY..!!");
            return false;
        }

        if (ObjGlobal.FinanceDepartmentMandatory && TxtDepartment.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtDepartment, "DEPARTMENT IS MANDATORY..!!");
            return false;
        }

        if (Math.Abs(TxtPayment.GetDecimal() - TxtReceipt.GetDecimal()) is 0)
        {
            this.NotifyValidationError(TxtReceipt, "DEBIT & CREDIT AMOUNT CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = _ledgerId;
        DGrid.Rows[iRows].Cells["GTxtLedger"].Value = TxtLedger.Text;
        DGrid.Rows[iRows].Cells["GTxtSubLedgerId"].Value = _subLedgerId.ToString();
        DGrid.Rows[iRows].Cells["GTxtSubLedger"].Value = TxtSubLedger.Text;
        DGrid.Rows[iRows].Cells["GTxtDepartmentId"].Value = _departmentId.ToString();
        DGrid.Rows[iRows].Cells["GTxtDepartment"].Value = TxtDepartment.Text;
        DGrid.Rows[iRows].Cells["GTxtAgentId"].Value = _agentId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAgent"].Value = TxtAgent.Text;
        DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = _itemCurrencyId;
        DGrid.Rows[iRows].Cells["GTxtCurrency"].Value = TxtItemCurrency.Text;
        DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = TxtItemCurrencyRate.Text;

        DGrid.Rows[iRows].Cells["GTxtReceipt"].Value = TxtReceipt.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtPayment"].Value = TxtPayment.GetDecimalString();

        var currencyRate = _multiCurrency ? TxtItemCurrencyRate.Text : TxtCurrencyRate.Text;
        var localReceipt = currencyRate.GetDecimal(true) * TxtReceipt.GetDecimal();
        var localPayment = currencyRate.GetDecimal(true) * TxtPayment.GetDecimal();

        DGrid.Rows[iRows].Cells["GTxtLocalReceipt"].Value = localReceipt.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtLocalPayment"].Value = localPayment.GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = TxtNarration.Text;
        _rowIndex = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[_rowIndex].Cells[_columnIndex];
        if (_isRowUpdate)
        {
            EnableGridControl();
            ClearLedgerDetails();
            DGrid.Focus();
            return false;
        }

        AdjustControlsInDataGrid();
        ClearLedgerDetails();
        TxtLedger.AcceptsTab = false;
        GetSerialNo();
        return true;
    }

    private void FillCashBankVoucher(string voucherNo)
    {
        var dsVoucher = _cashBank.ReturnCashBankVoucherInDataSet(voucherNo);
        if (dsVoucher.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow ro in dsVoucher.Tables[0].Rows)
            {
                if (_actionTag is not ("SAVE" or "COPY")) TxtVno.Text = ro["Voucher_No"].ToString();
                MskDate.Text = ro["Voucher_Date"].ToString();
                MskMiti.Text = ro["Voucher_Miti"].ToString();
                TxtRefVno.Text = ro["Ref_VNo"].ToString();
                MskRefDate.GetNepaliDate(ro["Ref_VDate"].ToString());

                TxtChequeNo.Text = ro["Voucher_Miti"].ToString();
                MskChequeDate.Text = ro["Voucher_Date"].ToString();
                MskChequeMiti.Text = ro["Voucher_Miti"].ToString();
                TxtCashLedger.Text = ro["GLName"].ToString();
                _cbLedgerId = ro["GLId"].GetLong();
                SetCashLedgerInfo(_cbLedgerId);
                _currencyId = ro["Currency_Id"].GetInt();
                TxtCurrency.Text = ro["Ccode"].ToString();
                TxtCurrencyRate.Text = ro["Currency_Rate"].GetDecimalString(true);
                TxtRemarks.Text = ro["Remarks"].ToString();
                TxtChequeNo.Text = ro["CheqNo"].ToString();
                MskChequeDate.Text = ro["CheqDate"].ToString();
                MskChequeMiti.Text = ro["CheqMiti"].ToString();

                if (!string.IsNullOrEmpty(ro["Cls1"].ToString()))
                {
                    TxtDepartment.Text = ro["Department1"].ToString();
                    _departmentId = ro["Cls1"].GetInt();
                }

                PAttachment1.Image = ro["PAttachment1"].GetImage();
                PAttachment2.Image = ro["PAttachment2"].GetImage();
                PAttachment3.Image = ro["PAttachment3"].GetImage();
                PAttachment4.Image = ro["PAttachment4"].GetImage();
                PAttachment5.Image = ro["PAttachment5"].GetImage();
            }

            var iRows = 0;
            if (dsVoucher.Tables[1].Rows.Count > 0)
            {
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsVoucher.Tables[1].Rows.Count + 1);
                foreach (DataRow ro in dsVoucher.Tables[1].Rows)
                {
                    DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
                    DGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = ro["GlId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtLedger"].Value = ro["GLName"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtDepartmentId"].Value = ro["Cls1"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtDepartment"].Value = ro["Department1"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtAgentId"].Value = ro["Agent_ID"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAgent"].Value = ro["AgentName"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtSubledgerId"].Value = ro["Subledger_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtSubledger"].Value = ro["SLName"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = ro["CurrencyId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtCurrency"].Value = ro["Ccode"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = ro["CurrencyRate"].GetDecimalString(true);

                    DGrid.Rows[iRows].Cells["GTxtReceipt"].Value = ro["Credit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtLocalReceipt"].Value = ro["LocalCredit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtPayment"].Value = ro["Debit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtLocalPayment"].Value = ro["LocalDebit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtNarration"].Value = ro["Narration"].ToString();
                    iRows++;
                }
            }
        }

        ClearLedgerDetails();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
    }

    private void FillPdcVoucherDetails(string pdcVno)
    {
        if (pdcVno.IsBlankOrEmpty()) return;

        var dt = _cashBank.ReturnPdcVoucherInDataTable(pdcVno);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow ro in dt.Rows)
            {
                MskDate.Text = ro["CheqDate"].IsValueExits() ? ro["CheqDate"].ToString() : MskDate.Text;
                MskMiti.Text = ro["ChqMiti"].IsValueExits() ? ro["ChqMiti"].ToString() : MskMiti.Text;
                TxtCashLedger.Text = ro["BankName"].IsValueExits() ? ro["BankName"].ToString() : string.Empty;
                _cbLedgerId = ro["BankLedgerId"].GetLong();
                SetCashLedgerInfo(_cbLedgerId);
                TxtRemarks.Text = ro["Remarks"].ToString();
                TxtRefVno.Text = pdcVno;
                MskRefDate.Text = ro["RefDate"].ToString();
                TxtChequeNo.Text = ro["ChequeNo"].ToString();
                MskChequeDate.Text = ro["CheqDate"].ToString();
                MskChequeMiti.Text = ro["ChqMiti"].ToString();
                if (ro["Cls1"].IsValueExits())
                {
                    TxtDepartment.Text = dt.Rows[0]["Department1"].ToString();
                    _departmentId = dt.Rows[0]["Cls1"].GetInt();
                }

                PAttachment1.Image = ro["PAttachment1"].GetImage();
            }

            DGrid.Rows.Clear();
            DGrid.Rows.Add(dt.Rows.Count + 1);
            var iRows = 0;
            foreach (DataRow ro in dt.Rows)
            {
                DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
                DGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = ro["GlId"].ToString();
                DGrid.Rows[iRows].Cells["GTxtSubledgerId"].Value = "0";
                DGrid.Rows[iRows].Cells["GTxtDepartmentId"].Value = "0";
                DGrid.Rows[iRows].Cells["GTxtAgentId"].Value = "0";
                DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = "0";
                DGrid.Rows[iRows].Cells["GTxtLedger"].Value = ro["GLName"].ToString();
                DGrid.Rows[iRows].Cells["GTxtReceipt"].Value = ro["VoucherType"].Equals("Receipt")
                    ? ro["Amount"].GetDecimalString()
                    : 0.GetDecimalString();
                DGrid.Rows[iRows].Cells["GTxtLocalReceipt"].Value = ro["VoucherType"].Equals("Receipt")
                    ? ro["Amount"].GetDecimalString()
                    : 0.GetDecimalString();
                DGrid.Rows[iRows].Cells["GTxtPayment"].Value = ro["VoucherType"].Equals("Receipt")
                    ? 0.GetDecimalString()
                    : ro["Amount"].GetDecimalString();
                DGrid.Rows[iRows].Cells["GTxtLocalPayment"].Value = ro["VoucherType"].Equals("Receipt")
                    ? 0.GetDecimalString()
                    : ro["Amount"].GetDecimalString();
                DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = ObjGlobal.SysCurrencyId;
                DGrid.Rows[iRows].Cells["GTxtCurrency"].Value = ObjGlobal.SysCurrency;
                DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = 1.GetDecimalString(true);
                DGrid.Rows[iRows].Cells["GTxtNarration"].Value = ro["Remarks"].ToString();
                iRows++;
            }

            ClearLedgerDetails();
            ObjGlobal.DGridColorCombo(DGrid);
            DGrid.ClearSelection();
        }

        MskMiti.Focus();
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 && !DGrid.Rows[0].Cells["GTxtLedger"].Value.IsValueExits()) return;
        LblTotalReceipt.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtReceipt"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalRecepit.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtLocalReceipt"].Value.GetDecimal()).GetDecimalString();
        LblTotalPayment.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtPayment"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalPayment.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtLocalPayment"].Value.GetDecimal()).GetDecimalString();
    }

    private void GetSerialNo()
    {
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    private bool IsValidForm()
    {
        if (_actionTag != "SAVE")
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) is DialogResult.No)
                return false;
        if (TxtVno.IsBlankOrEmpty())
        {
            if (!TxtVno.Enabled) TxtVno.Enabled = true;
            this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS BLANK..!!");
            return false;
        }

        if (_actionTag == "DELETE") return true;
        if (!MskDate.MaskCompleted)
        {
            this.NotifyValidationError(MskDate, "VOUCHER DATE IS INVALID..!!");
            return false;
        }

        if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskDate,
                $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
            return false;
        }

        if (!MskMiti.MaskCompleted)
        {
            this.NotifyValidationError(MskMiti, "VOUCHER MITI IS INVALID..!!");
            return false;
        }

        if (MskMiti.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskMiti,
                $"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
            return false;
        }

        if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.IsBlankOrEmpty())
        {
            this.NotifyValidationError(DGrid, "VOUCHER DETAILS IS BLANK..!!");
            return false;
        }

        if (ObjGlobal.FinanceRemarksEnable && TxtRemarks.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtRemarks, "VOUCHER REMARKS IS BLANK..!!");
            return false;
        }

        return true;
    }

    private int SaveCashBankVoucher()
    {
        const int syncRow = 0;
        if (_actionTag is "SAVE")
        {
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("CV", _docDesc);
        }
        _cashBank.CbMaster.VoucherMode = !_isProvision ? "Contra" : "PROV";
        _cashBank.CbMaster.Voucher_No = TxtVno.Text;
        _cashBank.CbMaster.Voucher_Date = MskDate.Text.GetDateTime();
        _cashBank.CbMaster.Voucher_Miti = MskMiti.Text;
        _cashBank.CbMaster.Voucher_Time = DateTime.Now;
        _cashBank.CbMaster.Ref_VNo = TxtRefVno.Text;
        _cashBank.CbMaster.Ref_VDate = TxtRefVno.IsValueExits() ? MskRefDate.GetDateTime() : DateTime.Now;
        _cashBank.CbMaster.VoucherType = "Contra";
        _cashBank.CbMaster.Ledger_Id = _cbLedgerId;
        _cashBank.CbMaster.CheqNo = TxtChequeNo.Text;
        _cashBank.CbMaster.CheqDate = TxtChequeNo.IsValueExits() ? MskChequeDate.Text.GetDateTime() : DateTime.Now;
        _cashBank.CbMaster.CheqMiti = MskChequeMiti.Text;
        _cashBank.CbMaster.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _cashBank.CbMaster.Currency_Rate = TxtCurrencyRate.GetDecimal() > 0 ? TxtCurrency.Text.GetDecimal() : 1;
        _cashBank.CbMaster.Cls1 = _departmentId;
        _cashBank.CbMaster.Cls2 = 0;
        _cashBank.CbMaster.Cls3 = 0;
        _cashBank.CbMaster.Cls4 = 0;
        _cashBank.CbMaster.Remarks = TxtRemarks.Text;
        _cashBank.CbMaster.Action_Type = _actionTag;
        _cashBank.CbMaster.SyncRowVersion = syncRow.ReturnSyncRowNo("CB", TxtVno.Text);
        _cashBank.CbMaster.GetView = DGrid;
        _cashBank.CbDetailsList.Clear();
        if (DGrid.RowCount <= 0)
        {
            return _cashBank.SaveCashBankVoucher(_actionTag);
        }
        foreach (DataGridViewRow viewRow in DGrid.Rows)
        {
            var list = new CB_Details();
            var detailsLedger = viewRow.Cells["GTxtLedgerId"].Value.GetLong();
            if (detailsLedger is 0)
            {
                continue;
            }
            list.Voucher_No = TxtVno.Text;
            list.SNo = viewRow.Cells["GTxtSNo"].Value.GetInt();
            list.CBLedgerId = _ledgerId;
            list.Ledger_Id = viewRow.Cells["GTxtLedgerId"].Value.GetLong();
            list.Subledger_Id = viewRow.Cells["GTxtSubledgerId"].Value.GetInt();
            list.Agent_Id = viewRow.Cells["GTxtAgentId"].Value.GetInt();
            list.Cls1 = viewRow.Cells["GTxtDepartmentId"].Value.GetInt();
            list.Cls2 = 0;
            list.Cls3 = 0;
            list.Cls4 = 0;
            list.CurrencyId = viewRow.Cells["GTxtCurrencyId"].Value.GetInt();
            list.CurrencyRate = viewRow.Cells["GTxtExchangeRate"].Value.GetDecimal(true);
            list.Debit = viewRow.Cells["GTxtPayment"].Value.GetDecimal();
            list.Credit = viewRow.Cells["GTxtReceipt"].Value.GetDecimal();
            list.LocalDebit = viewRow.Cells["GTxtLocalPayment"].Value.GetDecimal();
            list.LocalCredit = viewRow.Cells["GTxtLocalReceipt"].Value.GetDecimal();
            list.Tbl_Amount = 0;
            list.V_Amount = 0;
            list.Narration = viewRow.Cells["GTxtNarration"].Value.GetString();
            list.Party_No = string.Empty;
            list.Invoice_Date = DateTime.Now;
            list.Invoice_Miti = string.Empty;
            list.VatLedger_Id = 0;
            list.PanNo = 0;
            list.Vat_Reg = false;
            list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
            list.SyncOriginId = ObjGlobal.IsOnlineSync ? ObjGlobal.LocalOriginId.GetValueOrDefault() : Guid.Empty;
            list.SyncCreatedOn = DateTime.Now;
            list.SyncLastPatchedOn = DateTime.Now;
            list.SyncRowVersion = _cashBank.CbMaster.SyncRowVersion;
            _cashBank.CbDetailsList.Add(list);
        }
        return _cashBank.SaveCashBankVoucher(_actionTag);
    }

    private void PrintVoucher(bool isInvoice = true)
    {
        var dtDesign = _setup.GetPrintVoucherList("CB");
        if (dtDesign.Rows.Count > 0)
        {
            var type = dtDesign.Rows[0]["DesignerPaper_Name"].ToString();
            var isOnline = dtDesign.Rows[0]["Is_Online"].GetBool();
            if (!isOnline && isInvoice)
            {
                return;
            }
            var frmDp = new FrmDocumentPrint(type, "CB", TxtVno.Text, TxtVno.Text, true)
            {
                Owner = ActiveForm
            };
            frmDp.ShowDialog();
        }
        else if (!isInvoice)
        {
            CustomMessageBox.Information("PRINT DESIGN SETTING IS REQUIRED FOR PRINT DOCUMENT..!!");
        }
        // var frmName = dtDesign.Rows.Count > 0 ? "Crystal" : "DLL";
        // var frmDp = new FrmDocumentPrint(frmName, "CB", TxtVno.Text, TxtVno.Text)
        // {
        //     Owner = ActiveForm
        // };
        // frmDp.ShowDialog();
    }

    #endregion --------------- METHOD OF THIS FORM ---------------


    // OBJECT FOR THIS FORM
    #region -------------- OBJECT FOR THIS FORM --------------

    private int _subLedgerId;
    private int _departmentId;
    private int _currencyId = ObjGlobal.SysCurrencyId;
    private int _itemCurrencyId = ObjGlobal.SysCurrencyId;
    private int _agentId;
    private int _rowIndex;
    private int _columnIndex;

    private long _ledgerId;
    private long _cbLedgerId = ObjGlobal.FinanceCashLedgerId.GetLong();

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private bool _multiCurrency;
    private bool _zoom;
    private bool _isProvision;
    private bool _isCash;

    private string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;
    private string _ledgerType = string.Empty;
    private string _balanceType = string.Empty;
    private string _voucherType;
    private string _zoomVno;

    private KeyPressEventArgs _getKeys;
    private readonly ICashBankVoucherRepository _cashBank;
    private readonly IFinanceEntry _entry;
    private readonly IMasterSetup _setup;
    private readonly IFinanceDesign _voucher;

    #region ** ----- GRID CONTROL ----- **
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtLedger { get; set; }
    private MrGridTextBox TxtSubLedger { get; set; }
    private MrGridTextBox TxtAgent { get; set; }
    private MrGridTextBox TxtItemDepartment { get; set; }
    private MrGridTextBox TxtItemCurrency { get; set; }
    private MrGridNumericTextBox TxtItemCurrencyRate { get; set; }
    private MrGridNumericTextBox TxtReceipt { get; set; }
    private MrGridNumericTextBox TxtPayment { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }
    #endregion

    #endregion -------------- OBJECT FOR THIS FORM --------------


}