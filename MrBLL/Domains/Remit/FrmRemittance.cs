using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.Remit;

public partial class FrmRemittance : Form
{
    public FrmRemittance()
    {
        InitializeComponent();
        _voucher = new FinanceDesign();
        _entry = new ClsFinanceEntry();
        _setup = new ClsMasterSetup();

        _voucher.GetRemittanceDesign(RGrid, "R");
        _voucher.GetRemittanceDesign(PGrid, "P");

        PaymentControlsInDataGrid();
        ReceiptControlsInDataGrid();
        GetPGridColumns();
        GetRGridColumns();
        EnableControl();
        ClearControl();
    }

    private void FrmRemittance_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], Tag);
    }

    private void FrmRemittance_KeyPress(object sender, KeyPressEventArgs e)
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
                if (TxtPLedger.Enabled)
                {
                    ClearPLedgerDetails();
                    EnablePGridControl();
                    PGrid.Focus();
                }
                else if (TxtRLedger.Enabled)
                {
                    ClearRLedgerDetails();
                    EnableRGridControl();
                    RGrid.Focus();
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

    private void GlobalKeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    private void FrmRemittance_Shown(object sender, EventArgs e)
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

    private void BtnNew_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        MskMiti.Focus();
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

    private void BtnReverse_Click(object sender, EventArgs e)
    {
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintVoucher();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        var result = CustomMessageBox.ExitActiveForm();
        if (result == DialogResult.Yes)
        {
            Close();
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        var result = IsControlValidForm();
        if (result)
        {
            var saveRemit = SaveRemittanceVoucher();
            if (saveRemit != 0)
            {
                CustomMessageBox.ActionSuccess(TxtVno.Text, "REMIT VOUCHER", _actionTag);
                ClearControl();
                if (TxtVno.Enabled)
                {
                    TxtVno.Focus();
                }
                else
                {
                    MskMiti.Focus();
                }
            }
            else
            {
                CustomMessageBox.ErrorMessage($"ERROR OCCURS WHILE VOUCHER NUMBER [{_actionTag}]");
            }
        }
        else
        {
            return;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (TxtVno.IsBlankOrEmpty() && _actionTag.IsValueExits())
        {
            if (TxtVno.ValidControl(ActiveControl))
            {
                TxtVno.WarningMessage("VOUCHER NUMBER IS REQUIRED..!!");
                return;
            }
        }

    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var result = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "CB", "REMIT");
        if (result.IsValueExits())
        {
            TxtVno.Text = result;
            if (_actionTag != "SAVE")
            {
                FillRemittanceVoucherDetails();
            }
        }

        TxtVno.Focus();
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
                MskDate.WarningMessage($"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
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

    private void MskRefDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            TabReceiptControl.Focus();
            RGrid.Focus();
        }
    }

    private void TxtRemarks_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (ObjGlobal.FinanceRemarksMandatory)
            {
                if (TxtRemarks.IsBlankOrEmpty())
                {
                    TxtRemarks.WarningMessage("REMIT VOUCHER REMARKS IS MANDATORY..!!");
                    return;
                }
            }
            BtnSave.Focus();
        }
    }

    private void TxtRemarks_Validating(object sender, CancelEventArgs e)
    {
        if (TxtRemarks.ValidControl(ActiveControl))
        {
            if (ObjGlobal.FinanceRemarksMandatory)
            {
                if (TxtRemarks.IsBlankOrEmpty())
                {
                    TxtRemarks.WarningMessage("REMIT VOUCHER REMARKS IS MANDATORY..!!");
                    return;
                }
            }
        }
    }


    //DATA GRID CONTROL
    private void PGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && PGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No)
            {
                return;
            }
            _isRowDelete = true;
            if (PGrid.CurrentRow is { Index: >= 0 } && PGrid.Rows.Count > PGrid.CurrentRow.Index)
            {
                PGrid.Rows.RemoveAt(PGrid.CurrentRow.Index);
            }

            if (PGrid.RowCount is 0)
            {
                PGrid.Rows.Add();
            }
            GetPSerialNo();
            VoucherTotalCalculation();
        }

        if (e.KeyCode is Keys.Enter && !TxtPLedger.Enabled)
        {
            e.SuppressKeyPress = true;
            PaymentControlsInDataGrid();
            EnablePGridControl(true);
            ClearPLedgerDetails();
            if (PGrid.Rows[_rowIndex].Cells["GTxtPLedger"].Value.IsValueExits())
            {
                TextFromPGrid();
                TxtPLedger.Focus();
                return;
            }
            GetPSerialNo();
            TxtPLedger.Focus();
        }
    }

    private void PGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        PGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void PGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (CustomMessageBox.DeleteRow() is DialogResult.No)
        {
            return;
        }
        _isRowDelete = true;
    }

    private void PGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!_isRowDelete) return;
        if (PGrid.RowCount is 0)
        {
            PGrid.Rows.Add();
        }
        GetPSerialNo();
    }

    private void OnPGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void OnPGridOnGotFocus(object sender, EventArgs e)
    {
        PGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }

    private void OnPGridOnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        if (PGrid.CurrentCell != null)
        {
            _rowIndex = PGrid.CurrentCell.RowIndex.GetInt();
        }
    }

    private void OnPGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        if (TxtPLedger.Enabled)
        {
            return;
        }
        _rowIndex = e.RowIndex;
        PaymentControlsInDataGrid();
    }

    private void TxtPaymentOnValidating(object sender, CancelEventArgs e)
    {
        TxtPayment.Text = TxtPayment.GetDecimalString();
    }

    private void TxtPCurrencyRateOnTextChanged(object sender, EventArgs e)
    {
        VoucherTotalCalculation();
    }

    private void TxtPCurrencyOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenPCurrency();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id, exchangeRate) = GetMasterList.CreateCurrency(true);
            if (id > 0)
            {
                TxtPCurrency.Text = description;
                _pCurrencyId = id;
                TxtPCurrencyRate.Text = exchangeRate;
            }
            TxtPCurrency.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPCurrency, OpenPCurrency);
        }
    }

    private void TxtPLedgerOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenPLedger();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other", true);
            if (id > 0)
            {
                TxtPLedger.Text = description;
                _pLedgerId = id;
            }
            TxtPLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtPLedger, OpenPLedger);
        }
    }

    private void TxtPLedgerOnLeave(object sender, EventArgs e)
    {
        if (PGrid.RowCount > 0 && PGrid.Rows[0].Cells["GTxtPLedgerId"].Value.GetLong() > 0 && TxtPLedger.IsBlankOrEmpty())
        {
            EnablePGridControl();
        }
    }

    private void TxtPLedgerOnValidating(object sender, CancelEventArgs e)
    {
        if (PGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtPLedger, "PLEASE SELECT GENERAL LEDGER FOR OPENING");
        }

        if (PGrid.RowCount == 1 && TxtPLedger.Enabled && TxtPLedger.IsBlankOrEmpty())
        {
            if (PGrid.Rows[0].Cells["GTxtPLedgerId"].Value.GetLong() is 0)
            {
                if (RGrid.RowCount > 0 && RGrid.Rows[0].Cells["GTxtRLedgerId"].Value.GetLong() is 0)
                {
                    EnablePGridControl();
                    PGrid.ClearSelection();
                    this.NotifyValidationError(RGrid, "PLEASE SELECT GENERAL LEDGER FOR VOUCHER");
                    return;
                }
            }
        }
        if (PGrid.RowCount >= 1 && TxtPLedger.Enabled && TxtPLedger.IsBlankOrEmpty())
        {
            EnablePGridControl();
            PGrid.ClearSelection();
            BtnSave.Focus();
        }
    }

    private void TxtPNarration_Validating(object sender, CancelEventArgs e)
    {
        TxtPLedger.TabStop = true;
        if (!TxtPLedger.Enabled || ActiveControl == TxtPayment)
        {
            return;
        }
        if (!AddTextToPGrid(_isRowUpdate))
        {
            return;
        }
        TxtPLedger.Focus();
    }

    private void TxtPNarration_Validated(object sender, EventArgs e)
    {
        //if (ActiveControl.Name == "TxtPayment" || !TxtLedger.Enabled)
        //{
        //    return;
        //}
        //if (!AddTextToGrid(_isRowUpdate)) return;
        //TxtLedger.Focus();
        //return;
    }

    private void TxtPNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode == Keys.Tab)
        {
            TxtPayment.Focus();
            SendToBack();
            return;
        }

        if (PGrid.CurrentRow != null && PGrid.CurrentRow.Index - 1 >= 0 && PGrid.Rows.Count > PGrid.CurrentRow.Index - 1)
        {
            TxtPNarration.Text = e.KeyCode switch
            {
                Keys.F2 => PGrid.Rows.Count > 0 ? PGrid.Rows[PGrid.CurrentRow.Index - 1].Cells["GTxtPNarration"].Value?.ToString() : string.Empty,
                Keys.F3 => PGrid.Rows.Count > 0 ? PGrid.Rows[1].Cells["GTxtPNarration"].Value?.ToString() : string.Empty,
                Keys.F4 => PGrid.Rows.Count > 0 ? PGrid.Rows[2].Cells["GTxtPNarration"].Value?.ToString() : string.Empty,
                Keys.F5 => PGrid.Rows.Count > 0 ? PGrid.Rows[3].Cells["GTxtPNarration"].Value?.ToString() : string.Empty,
                Keys.F6 => PGrid.Rows.Count > 0 ? PGrid.Rows[PGrid.Rows.Count - 1].Cells["GTxtPNarration"].Value?.ToString() : string.Empty,
                _ => TxtPNarration.Text
            };
        }
    }

    //DATA GRID CONTROL
    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && RGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No) return;
            _isRowDelete = true;
            if (RGrid.CurrentRow is { Index: >= 0 } && RGrid.Rows.Count > RGrid.CurrentRow.Index)
            {
                RGrid.Rows.RemoveAt(RGrid.CurrentRow.Index);
            }

            if (RGrid.RowCount is 0)
            {
                RGrid.Rows.Add();
            }
            GetRSerialNo();
            VoucherTotalCalculation();
        }

        if (e.KeyCode is Keys.Enter && !TxtRLedger.Enabled)
        {
            e.SuppressKeyPress = true;
            ReceiptControlsInDataGrid();
            EnableRGridControl(true);
            ClearRLedgerDetails();
            if (RGrid.Rows[_rowIndex].Cells["GTxtRLedger"].Value.IsValueExits())
            {
                TextFromRGrid();
                TxtRLedger.Focus();
                return;
            }

            GetRSerialNo();
            TxtRLedger.Focus();
        }
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        RGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void RGrid_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
    {
        if (CustomMessageBox.DeleteRow() is DialogResult.No)
        {
            return;
        }
        _isRowDelete = true;
    }

    private void RGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!_isRowDelete) return;
        if (RGrid.RowCount is 0)
        {
            RGrid.Rows.Add();
        }
        GetRSerialNo();
    }

    private void OnRGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void OnRGridOnGotFocus(object sender, EventArgs e)
    {
        RGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }

    private void OnRGridOnRowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        if (RGrid.CurrentCell != null)
        {
            _rowIndex = RGrid.CurrentCell.RowIndex.GetInt();
        }
    }

    private void OnRGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        if (TxtRLedger.Enabled)
        {
            return;
        }
        _rowIndex = e.RowIndex;
        ReceiptControlsInDataGrid();
    }

    private void TxtReceiptOnValidating(object sender, CancelEventArgs e)
    {
        TxtReceipt.Text = TxtReceipt.GetDecimalString();
    }

    private void TxtRCurrencyRateOnTextChanged(object sender, EventArgs e)
    {
        VoucherTotalCalculation();
    }

    private void TxtRCurrencyOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenRCurrency();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id, exchangeRate) = GetMasterList.CreateCurrency(true);
            if (id > 0)
            {
                TxtRCurrency.Text = description;
                _rCurrencyId = id;
                TxtRCurrencyRate.Text = exchangeRate;
            }
            TxtRCurrency.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtRCurrency, OpenRCurrency);
        }
    }

    private void TxtRLedgerOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenRLedger();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var (description, id) = GetMasterList.CreateGeneralLedger("Other", true);
            if (id > 0)
            {
                TxtRLedger.Text = description;
                _rLedgerId = id;
            }
            TxtRLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtRLedger, OpenRLedger);
        }
    }

    private void TxtRLedgerOnLeave(object sender, EventArgs e)
    {
        if (RGrid.RowCount > 0 && RGrid.Rows[0].Cells["GTxtRLedgerId"].Value.GetLong() > 0 && TxtRLedger.IsBlankOrEmpty())
        {
            EnableRGridControl();
        }
    }

    private void TxtRLedgerOnValidating(object sender, CancelEventArgs e)
    {
        if (TxtRLedger.IsBlankOrEmpty())
        {
            EnableRGridControl();
            RGrid.ClearSelection();
            TabPaymentControl.Focus();
            PGrid.Focus();
        }
    }

    private void TxtRNarration_Validating(object sender, CancelEventArgs e)
    {
        TxtRLedger.TabStop = true;
        if (!TxtRLedger.Enabled || ActiveControl == TxtPayment)
        {
            return;
        }
        if (!AddTextToRGrid(_isRowUpdate))
        {
            return;
        }
        TxtRLedger.Focus();
    }

    private void TxtRNarration_Validated(object sender, EventArgs e)
    {
        //if (ActiveControl.Name == "TxtPayment" || !TxtLedger.Enabled)
        //{
        //    return;
        //}
        //if (!AddTextToGrid(_isRowUpdate)) return;
        //TxtLedger.Focus();
        //return;
    }

    private void TxtRNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode == Keys.Tab)
        {
            TxtPayment.Focus();
            SendToBack();
            return;
        }

        if (RGrid.CurrentRow != null && RGrid.CurrentRow.Index - 1 >= 0 && RGrid.Rows.Count > RGrid.CurrentRow.Index - 1)
        {
            TxtRNarration.Text = e.KeyCode switch
            {
                Keys.F2 => RGrid.Rows.Count > 0 ? RGrid.Rows[RGrid.CurrentRow.Index - 1].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F3 => RGrid.Rows.Count > 0 ? RGrid.Rows[1].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F4 => RGrid.Rows.Count > 0 ? RGrid.Rows[2].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F5 => RGrid.Rows.Count > 0 ? RGrid.Rows[3].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F6 => RGrid.Rows.Count > 0 ? RGrid.Rows[RGrid.Rows.Count - 1].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                _ => TxtRNarration.Text
            };
        }
    }

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
        else if (dt is { Rows: { Count: > 1 } })
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

        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;

        TxtRemarks.Enabled = isEnable && ObjGlobal.FinanceRemarksEnable;
        btnRemarks.Enabled = TxtRemarks.Enabled;

        EnableRGridControl();
        EnablePGridControl();
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? " REMITTANCE VOUCHER SETUP "
            : $" REMITTANCE VOUCHER SETUP [{_actionTag}]";
        _zoom = !_actionTag.Equals("SAVE") && _zoom;
        _zoomVno = _actionTag.Equals("SAVE") ? string.Empty : _zoomVno;
        if (_zoom)
        {
            return;
        }
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

        TxtRefVno.Clear();
        RGrid.ReadOnly = true;
        RGrid.ClearSelection();
        RGrid.Rows.Clear();

        PGrid.ReadOnly = true;
        PGrid.ClearSelection();
        PGrid.Rows.Clear();

        ClearPLedgerDetails();
        ClearRLedgerDetails();
    }

    private void ClearPLedgerDetails()
    {
        if (PGrid.RowCount is 0)
        {
            PGrid.Rows.Add();
            TxtPSno.Text = PGrid.RowCount.ToString();
        }
        _isRowUpdate = false;
        _pLedgerId = 0;
        TxtPLedger.Clear();
        _pCurrencyId = 0;
        TxtPCurrency.Clear();
        TxtPCurrencyRate.Clear();
        TxtPayment.Clear();
        TxtPNarration.Clear();
        VoucherTotalCalculation();
    }

    private void ClearRLedgerDetails()
    {
        if (RGrid.RowCount is 0)
        {
            RGrid.Rows.Add();
            TxtRSno.Text = RGrid.RowCount.ToString();
        }
        _isRowUpdate = false;
        _rLedgerId = 0;
        TxtRLedger.Clear();
        _rCurrencyId = 0;
        TxtRCurrency.Clear();
        TxtRCurrencyRate.Clear();
        TxtReceipt.Clear();
        TxtRNarration.Clear();
        VoucherTotalCalculation();
    }

    private void EnablePGridControl(bool isEnable = false)
    {
        TxtPSno.Enabled = false;
        TxtPSno.Visible = isEnable;

        TxtPLedger.Enabled = TxtPLedger.Visible = isEnable;
        TxtPCurrency.Enabled = isEnable;
        TxtPCurrency.Visible = isEnable;

        TxtPCurrencyRate.Enabled = TxtPCurrencyRate.Visible = isEnable;
        TxtPayment.Enabled = TxtPayment.Visible = isEnable;
        TxtPNarration.Enabled = TxtPNarration.Visible = isEnable;
    }

    private void EnableRGridControl(bool isEnable = false)
    {
        TxtRSno.Enabled = false;
        TxtRSno.Visible = isEnable;
        TxtRLedger.Enabled = TxtRLedger.Visible = isEnable;

        TxtRCurrency.Enabled = TxtRCurrency.Visible = isEnable;
        TxtRCurrencyRate.Enabled = TxtRCurrencyRate.Visible = isEnable;

        TxtReceipt.Enabled = TxtReceipt.Visible = isEnable;
        TxtRNarration.Enabled = TxtRNarration.Visible = isEnable;
    }

    private void OpenRLedger()
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtRLedger.Text = description;
            _rLedgerId = id;
        }
        TxtRLedger.Focus();
    }

    private void OpenPLedger()
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag);
        if (id > 0)
        {
            TxtPLedger.Text = description;
            _pLedgerId = id;
        }
        TxtPLedger.Focus();
    }

    private void OpenRCurrency()
    {
        var (description, id, currencyRate) = GetMasterList.GetCurrencyList(_actionTag);
        if (id > 0)
        {
            TxtRCurrency.Text = description;
            TxtRCurrencyRate.Text = currencyRate;
            _rCurrencyId = id;
        }

        TxtRCurrency.Focus();
    }

    private void OpenPCurrency()
    {
        var (description, id, currencyRate) = GetMasterList.GetCurrencyList(_actionTag);
        if (id > 0)
        {
            TxtPCurrency.Text = description;
            TxtPCurrencyRate.Text = currencyRate;
            _pCurrencyId = id;
        }
    }

    private void GetPGridColumns()
    {
        PGrid.RowEnter += OnPGridOnRowEnter;
        PGrid.RowsAdded += OnPGridOnRowsAdded;
        PGrid.GotFocus += OnPGridOnGotFocus;
        PGrid.CellEnter += OnPGridOnCellEnter;
        PGrid.KeyDown += PGrid_KeyDown;
        PGrid.UserDeletingRow += PGrid_UserDeletingRow;
        PGrid.EnterKeyPressed += PGrid_EnterKeyPressed;
        PGrid.UserDeletedRow += PGrid_UserDeletedRow;

        TxtPSno = new MrGridNumericTextBox(PGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtPLedger = new MrGridTextBox(PGrid)
        {
            ReadOnly = true
        };
        TxtPLedger.KeyDown += TxtPLedgerOnKeyDown;
        TxtPLedger.Leave += TxtPLedgerOnLeave;
        TxtPLedger.Validating += TxtPLedgerOnValidating;

        TxtPCurrency = new MrGridTextBox(PGrid)
        {
            ReadOnly = true
        };
        TxtPCurrency.KeyDown += TxtPCurrencyOnKeyDown;

        TxtPCurrencyRate = new MrGridNumericTextBox(PGrid);
        TxtPCurrencyRate.TextChanged += TxtPCurrencyRateOnTextChanged;

        TxtPayment = new MrGridNumericTextBox(PGrid);
        TxtPayment.Validating += TxtPaymentOnValidating;

        TxtPNarration = new MrGridNormalTextBox(PGrid);
        TxtPNarration.KeyDown += TxtPNarration_KeyDown;
        TxtPNarration.Validating += TxtPNarration_Validating;
        TxtPNarration.Validated += TxtPNarration_Validated;

        ObjGlobal.DGridColorCombo(PGrid);
    }

    private void GetRGridColumns()
    {
        RGrid.RowEnter += OnRGridOnRowEnter;
        RGrid.RowsAdded += OnRGridOnRowsAdded;
        RGrid.GotFocus += OnRGridOnGotFocus;
        RGrid.CellEnter += OnRGridOnCellEnter;
        RGrid.KeyDown += RGrid_KeyDown;
        RGrid.UserDeletingRow += RGrid_UserDeletingRow;
        RGrid.EnterKeyPressed += RGrid_EnterKeyPressed;
        RGrid.UserDeletedRow += RGrid_UserDeletedRow;
        TxtRSno = new MrGridNumericTextBox(RGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtRLedger = new MrGridTextBox(RGrid)
        {
            ReadOnly = true
        };
        TxtRLedger.KeyDown += TxtRLedgerOnKeyDown;
        TxtRLedger.Leave += TxtRLedgerOnLeave;
        TxtRLedger.Validating += TxtRLedgerOnValidating;

        TxtRCurrency = new MrGridTextBox(RGrid)
        {
            ReadOnly = true
        };
        TxtRCurrency.KeyDown += TxtRCurrencyOnKeyDown;

        TxtRCurrencyRate = new MrGridNumericTextBox(RGrid);
        TxtRCurrencyRate.TextChanged += TxtRCurrencyRateOnTextChanged;

        TxtReceipt = new MrGridNumericTextBox(RGrid);
        TxtReceipt.Validating += TxtReceiptOnValidating;

        TxtRNarration = new MrGridNormalTextBox(RGrid);
        TxtRNarration.KeyDown += TxtRNarration_KeyDown;
        TxtRNarration.KeyPress += (_, e) => e.Handled = false;
        TxtRNarration.Validating += TxtRNarration_Validating;
        TxtRNarration.Validated += TxtRNarration_Validated;

        ObjGlobal.DGridColorCombo(RGrid);
    }

    private void TextFromPGrid()
    {
        if (PGrid.CurrentRow == null)
        {
            return;
        }
        TxtPSno.Text = PGrid.Rows[_rowIndex].Cells["GTxtPSNo"].Value.GetString();
        TxtPLedger.Text = PGrid.Rows[_rowIndex].Cells["GTxtPLedger"].Value.GetString();
        _pLedgerId = PGrid.Rows[_rowIndex].Cells["GTxtPLedgerId"].Value.GetLong();
        TxtPCurrency.Text = PGrid.Rows[_rowIndex].Cells["GTxtPCurrency"].Value.GetString();
        TxtPCurrencyRate.Text = PGrid.Rows[_rowIndex].Cells["GTxtPExchangeRate"].Value.GetString();
        _pCurrencyId = PGrid.Rows[_rowIndex].Cells["GTxtPCurrencyId"].Value.GetInt();
        TxtPayment.Text = PGrid.Rows[_rowIndex].Cells["GTxtPayment"].Value.GetDecimalString();
        TxtPNarration.Text = PGrid.Rows[_rowIndex].Cells["GTxtPNarration"].Value.GetString();
        _isRowUpdate = true;
    }

    private void TextFromRGrid()
    {
        if (RGrid.CurrentRow == null)
        {
            return;
        }
        TxtRSno.Text = RGrid.Rows[_rowIndex].Cells["GTxtRSNo"].Value.GetString();
        TxtRLedger.Text = RGrid.Rows[_rowIndex].Cells["GTxtRLedger"].Value.GetString();
        _rLedgerId = RGrid.Rows[_rowIndex].Cells["GTxtRLedgerId"].Value.GetLong();
        TxtRCurrency.Text = RGrid.Rows[_rowIndex].Cells["GTxtRCurrency"].Value.GetString();
        TxtRCurrencyRate.Text = RGrid.Rows[_rowIndex].Cells["GTxtRExchangeRate"].Value.GetString();
        _rCurrencyId = RGrid.Rows[_rowIndex].Cells["GTxtRCurrencyId"].Value.GetInt();
        TxtReceipt.Text = RGrid.Rows[_rowIndex].Cells["GTxtReceipt"].Value.GetDecimalString();
        TxtRNarration.Text = RGrid.Rows[_rowIndex].Cells["GTxtRNarration"].Value.GetString();
        _isRowUpdate = true;
    }

    private bool AddTextToPGrid(bool isUpdate)
    {
        if (TxtPayment.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtReceipt, "DEBIT & CREDIT AMOUNT CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            PGrid.Rows.Add();
            PGrid.Rows[iRows].Cells["GTxtPSNo"].Value = iRows + 1;
        }

        PGrid.Rows[iRows].Cells["GTxtPLedgerId"].Value = _pLedgerId;
        PGrid.Rows[iRows].Cells["GTxtPLedger"].Value = TxtPLedger.Text;
        PGrid.Rows[iRows].Cells["GTxtPCurrencyId"].Value = _pCurrencyId;
        PGrid.Rows[iRows].Cells["GTxtPCurrency"].Value = TxtPCurrency.Text;
        PGrid.Rows[iRows].Cells["GTxtPExchangeRate"].Value = TxtPCurrencyRate.Text;

        PGrid.Rows[iRows].Cells["GTxtPayment"].Value = TxtPayment.GetDecimalString();

        var currencyRate = TxtPCurrencyRate.Text;

        var payment = currencyRate.GetDecimal(true) * TxtPayment.GetDecimal();
        PGrid.Rows[iRows].Cells["GTxtLocalPayment"].Value = payment.GetDecimalString();

        PGrid.Rows[iRows].Cells["GTxtPNarration"].Value = TxtPNarration.Text;
        _rowIndex = PGrid.RowCount - 1 > iRows ? iRows + 1 : PGrid.RowCount - 1;
        PGrid.CurrentCell = PGrid.Rows[_rowIndex].Cells[_columnIndex];
        if (_isRowUpdate)
        {
            EnablePGridControl();
            ClearPLedgerDetails();
            PGrid.Focus();
            return false;
        }

        PaymentControlsInDataGrid();
        ClearPLedgerDetails();
        TxtPLedger.AcceptsTab = false;
        GetPSerialNo();
        return true;
    }

    private bool AddTextToRGrid(bool isUpdate)
    {
        if (TxtReceipt.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtReceipt, "DEBIT & CREDIT AMOUNT CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            RGrid.Rows.Add();
            RGrid.Rows[iRows].Cells["GTxtRSNo"].Value = iRows + 1;
        }

        RGrid.Rows[iRows].Cells["GTxtRLedgerId"].Value = _rLedgerId;
        RGrid.Rows[iRows].Cells["GTxtRLedger"].Value = TxtRLedger.Text;
        RGrid.Rows[iRows].Cells["GTxtRCurrencyId"].Value = _rCurrencyId;
        RGrid.Rows[iRows].Cells["GTxtRCurrency"].Value = TxtRCurrency.Text;
        RGrid.Rows[iRows].Cells["GTxtRExchangeRate"].Value = TxtRCurrencyRate.Text;

        RGrid.Rows[iRows].Cells["GTxtReceipt"].Value = TxtReceipt.GetDecimalString();

        var currencyRate = TxtRCurrencyRate.Text;

        var receipt = currencyRate.GetDecimal(true) * TxtReceipt.GetDecimal();
        RGrid.Rows[iRows].Cells["GTxtLocalReceipt"].Value = receipt.GetDecimalString();

        RGrid.Rows[iRows].Cells["GTxtRNarration"].Value = TxtRNarration.Text;
        _rowIndex = RGrid.RowCount - 1 > iRows ? iRows + 1 : RGrid.RowCount - 1;
        RGrid.CurrentCell = RGrid.Rows[_rowIndex].Cells[_columnIndex];
        if (_isRowUpdate)
        {
            EnableRGridControl();
            ClearRLedgerDetails();
            RGrid.Focus();
            return false;
        }

        ReceiptControlsInDataGrid();
        ClearRLedgerDetails();
        TxtRLedger.AcceptsTab = false;
        GetRSerialNo();
        return true;
    }

    private void VoucherTotalCalculation()
    {
        if (PGrid.RowCount > 0 && PGrid.Rows[0].Cells["GTxtPLedger"].Value.IsValueExits())
        {
            var viewRows = PGrid.Rows.OfType<DataGridViewRow>();
            var rows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

            LblTotalPayment.Text = rows.Sum(row => row.Cells["GTxtPayment"].Value.GetDecimal()).GetDecimalString();
            LblTotalLocalPayment.Text = rows.Sum(row => row.Cells["GTxtLocalPayment"].Value.GetDecimal()).GetDecimalString();
        }
        if (RGrid.RowCount > 0 && RGrid.Rows[0].Cells["GTxtRLedger"].Value.IsValueExits())
        {
            var viewRows = RGrid.Rows.OfType<DataGridViewRow>();
            var rows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

            LblTotalReceipt.Text = rows.Sum(row => row.Cells["GTxtReceipt"].Value.GetDecimal()).GetDecimalString();
            LblTotalLocalRecepit.Text = rows.Sum(row => row.Cells["GTxtLocalReceipt"].Value.GetDecimal()).GetDecimalString();
        }
    }

    private void GetRSerialNo()
    {
        for (var i = 0; i < RGrid.RowCount; i++)
        {
            TxtRSno.Text = (i + 1).ToString();
            RGrid.Rows[i].Cells["GTxtRSNo"].Value = TxtRSno.Text;
        }
    }

    private void GetPSerialNo()
    {
        for (var i = 0; i < PGrid.RowCount; i++)
        {
            TxtPSno.Text = (i + 1).ToString();
            PGrid.Rows[i].Cells["GTxtPSNo"].Value = TxtPSno.Text;
        }
    }

    private bool IsControlValidForm()
    {
        if (_actionTag != "SAVE")
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) is DialogResult.No)
            {
                return false;
            }
        }

        if (TxtVno.IsBlankOrEmpty())
        {
            if (!TxtVno.Enabled)
            {
                TxtVno.Enabled = true;
            }
            this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS BLANK..!!");
            return false;
        }

        if (_actionTag == "DELETE")
        {
            return true;
        }
        if (!MskDate.MaskCompleted)
        {
            this.NotifyValidationError(MskDate, "VOUCHER DATE IS INVALID..!!");
            return false;
        }

        if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskDate, $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
            return false;
        }

        if (!MskMiti.MaskCompleted)
        {
            this.NotifyValidationError(MskMiti, "VOUCHER MITI IS INVALID..!!");
            return false;
        }

        if (MskMiti.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
        {
            this.NotifyValidationError(MskMiti, $"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
            return false;
        }

        if (RGrid.RowCount > 0 && RGrid.Rows[0].Cells["GTxtRLedgerId"].Value.IsBlankOrEmpty())
        {
            if (PGrid.RowCount > 0 && PGrid.Rows[0].Cells["GTxtPLedgerId"].Value.IsBlankOrEmpty())
            {
                this.NotifyValidationError(PGrid, "VOUCHER DETAILS IS BLANK..!!");
                return false;
            }
        }

        if (PGrid.RowCount > 0 && PGrid.Rows[0].Cells["GTxtPLedgerId"].Value.IsBlankOrEmpty())
        {
            if (RGrid.RowCount > 0 && RGrid.Rows[0].Cells["GTxtRLedgerId"].Value.IsBlankOrEmpty())
            {
                this.NotifyValidationError(RGrid, "VOUCHER DETAILS IS BLANK..!!");
                return false;
            }
        }
        return true;
    }

    private int SaveRemittanceVoucher()
    {
        if (ObjGlobal.FinanceCashLedgerId is 0)
        {
            return 0;
        }
        TxtVno.Text = _actionTag is "SAVE" ? TxtVno.GetCurrentVoucherNo("CV", _docDesc) : TxtVno.Text;
        _entry.CbMaster.VoucherMode = "REMIT";
        _entry.CbMaster.Voucher_No = TxtVno.Text;
        _entry.CbMaster.Voucher_Date = MskDate.Text.GetDateTime();
        _entry.CbMaster.Voucher_Miti = MskMiti.Text;
        _entry.CbMaster.Voucher_Time = DateTime.Now;
        _entry.CbMaster.Ref_VNo = TxtRefVno.Text;
        _entry.CbMaster.Ref_VDate = TxtRefVno.IsValueExits() ? MskRefDate.GetDateTime() : DateTime.Now;
        _entry.CbMaster.VoucherType = "REMIT";
        _entry.CbMaster.Ledger_Id = ObjGlobal.FinanceCashLedgerId;
        _entry.CbMaster.CheqNo = string.Empty;
        _entry.CbMaster.CheqDate = DateTime.Now;
        _entry.CbMaster.CheqMiti = string.Empty;
        _entry.CbMaster.Currency_Id = ObjGlobal.SysCurrencyId;
        _entry.CbMaster.Currency_Rate = 1;
        _entry.CbMaster.Cls1 = 0;
        _entry.CbMaster.Cls2 = 0;
        _entry.CbMaster.Cls3 = 0;
        _entry.CbMaster.Cls4 = 0;
        _entry.CbMaster.Remarks = TxtRemarks.Text;
        _entry.CbMaster.SyncRowVersion = _entry.CbMaster.SyncRowVersion.ReturnSyncRowNo("CB", TxtVno.Text);
        var result = _entry.SaveRemittanceVoucher(RGrid, PGrid, _actionTag);
        if (result != 0)
        {
            //PrintVoucher();
        }
        return result;
    }

    private void PrintVoucher()
    {
        var dtDesign = _setup.GetPrintVoucherList("CB");
        var frmName = dtDesign.Rows.Count > 0 ? "Crystal" : "DLL";
        var frmDp = new FrmDocumentPrint(frmName, "CB", TxtVno.Text, TxtVno.Text)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }

    private void PaymentControlsInDataGrid()
    {
        if (PGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = PGrid.Columns["GTxtPSNo"].Index;
        TxtPSno.Size = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPSno.Location = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPSno.TabIndex = columnIndex;

        columnIndex = PGrid.Columns["GTxtPLedger"].Index;
        TxtPLedger.Size = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPLedger.Location = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPLedger.TabIndex = columnIndex;

        columnIndex = PGrid.Columns["GTxtPCurrency"].Index;
        TxtPCurrency.Size = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPCurrency.Location = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPCurrency.TabIndex = columnIndex;

        columnIndex = PGrid.Columns["GTxtPExchangeRate"].Index;
        TxtPCurrencyRate.Size = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPCurrencyRate.Location = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPCurrencyRate.TabIndex = columnIndex;

        columnIndex = PGrid.Columns["GTxtPayment"].Index;
        TxtPayment.Size = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPayment.Location = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPayment.TabIndex = columnIndex;

        columnIndex = PGrid.Columns["GTxtPNarration"].Index;
        TxtPNarration.Size = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtPNarration.Location = PGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtPNarration.TabIndex = columnIndex;
    }

    private void ReceiptControlsInDataGrid()
    {
        if (RGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = RGrid.Columns["GTxtRSNo"].Index;
        TxtRSno.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRSno.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRSno.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtRLedger"].Index;
        TxtRLedger.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRLedger.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRLedger.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtRCurrency"].Index;
        TxtRCurrency.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRCurrency.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRCurrency.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtRExchangeRate"].Index;
        TxtRCurrencyRate.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRCurrencyRate.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRCurrencyRate.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtReceipt"].Index;
        TxtReceipt.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtReceipt.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtReceipt.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtRNarration"].Index;
        TxtRNarration.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRNarration.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRNarration.TabIndex = columnIndex;
    }

    private void FillRemittanceVoucherDetails()
    {
        var dsVoucher = _entry.ReturnCashBankVoucherInDataSet(TxtVno.Text);
        if (dsVoucher.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow ro in dsVoucher.Tables[0].Rows)
            {
                if (_actionTag is not ("SAVE" or "COPY")) TxtVno.Text = ro["Voucher_No"].ToString();
                MskDate.Text = ro["Voucher_Date"].ToString();
                MskMiti.Text = ro["Voucher_Miti"].ToString();
                TxtRefVno.Text = ro["Ref_VNo"].ToString();
                MskRefDate.GetNepaliDate(ro["Ref_VDate"].ToString());

                TxtRemarks.Text = ro["Remarks"].ToString();

            }


            if (dsVoucher.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                var receipt = dsVoucher.Tables[1].Select("Credit > 0");
                var payment = dsVoucher.Tables[1].Select("Debit > 0");
                if (receipt.Length > 0)
                {
                    RGrid.Rows.Clear();
                    RGrid.Rows.Add(receipt.Length + 1);
                    foreach (var ro in receipt)
                    {
                        RGrid.Rows[iRows].Cells["GTxtRSNo"].Value = iRows + 1;
                        RGrid.Rows[iRows].Cells["GTxtRLedgerId"].Value = ro["GlId"].ToString();
                        RGrid.Rows[iRows].Cells["GTxtRLedger"].Value = ro["GLName"].ToString();


                        RGrid.Rows[iRows].Cells["GTxtRCurrencyId"].Value = ro["CurrencyId"].ToString();
                        RGrid.Rows[iRows].Cells["GTxtRCurrency"].Value = ro["Ccode"].ToString();
                        RGrid.Rows[iRows].Cells["GTxtRExchangeRate"].Value = ro["CurrencyRate"].GetDecimalString(true);

                        RGrid.Rows[iRows].Cells["GTxtReceipt"].Value = ro["Credit"].GetDecimalString();
                        RGrid.Rows[iRows].Cells["GTxtLocalReceipt"].Value = ro["LocalCredit"].GetDecimalString();
                        RGrid.Rows[iRows].Cells["GTxtRNarration"].Value = ro["Narration"].ToString();
                        iRows++;
                    }

                }
                if (payment.Length > 0)
                {
                    iRows = 0;
                    PGrid.Rows.Clear();
                    PGrid.Rows.Add(payment.Length + 1);
                    foreach (var ro in payment)
                    {
                        PGrid.Rows[iRows].Cells["GTxtPSNo"].Value = iRows + 1;
                        PGrid.Rows[iRows].Cells["GTxtPLedgerId"].Value = ro["GlId"].ToString();
                        PGrid.Rows[iRows].Cells["GTxtPLedger"].Value = ro["GLName"].ToString();


                        PGrid.Rows[iRows].Cells["GTxtPCurrencyId"].Value = ro["CurrencyId"].ToString();
                        PGrid.Rows[iRows].Cells["GTxtPCurrency"].Value = ro["Ccode"].ToString();
                        PGrid.Rows[iRows].Cells["GTxtPExchangeRate"].Value = ro["CurrencyRate"].GetDecimalString(true);

                        PGrid.Rows[iRows].Cells["GTxtPayment"].Value = ro["Debit"].GetDecimalString();
                        PGrid.Rows[iRows].Cells["GTxtLocalPayment"].Value = ro["LocalDebit"].GetDecimalString();
                        PGrid.Rows[iRows].Cells["GTxtPNarration"].Value = ro["Narration"].ToString();
                        iRows++;
                    }

                }
            }
        }
        ObjGlobal.DGridColorCombo(RGrid);
        ObjGlobal.DGridColorCombo(PGrid);
        RGrid.ClearSelection();
        PGrid.ClearSelection();
    }
    #endregion --------------- METHOD OF THIS FORM ---------------

    private int _pCurrencyId = ObjGlobal.SysCurrencyId;
    private int _rCurrencyId = ObjGlobal.SysCurrencyId;
    private int _rowIndex;
    private int _columnIndex;

    private long _rLedgerId;
    private long _pLedgerId;

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private bool _multiCurrency;
    private bool _zoom;
    private bool _isProvision;
    private bool _isCash;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;
    private string _ledgerType = string.Empty;
    private string _balanceType = string.Empty;
    private string _voucherType;
    private string _zoomVno;

    private KeyPressEventArgs _getKeys;

    private readonly IFinanceEntry _entry;
    private readonly IMasterSetup _setup;
    private readonly IFinanceDesign _voucher;

    private MrGridNumericTextBox TxtPSno { get; set; }
    private MrGridTextBox TxtPLedger { get; set; }
    private MrGridTextBox TxtPCurrency { get; set; }
    private MrGridNumericTextBox TxtPCurrencyRate { get; set; }
    private MrGridNumericTextBox TxtPayment { get; set; }
    private MrGridNormalTextBox TxtPNarration { get; set; }

    private MrGridNumericTextBox TxtRSno { get; set; }
    private MrGridTextBox TxtRLedger { get; set; }
    private MrGridTextBox TxtRCurrency { get; set; }
    private MrGridNumericTextBox TxtRCurrencyRate { get; set; }
    private MrGridNumericTextBox TxtReceipt { get; set; }
    private MrGridNormalTextBox TxtRNarration { get; set; }

}