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
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.MoneyExchange;

public partial class FrmCurrencyExchange : Form
{
    public FrmCurrencyExchange(string voucherType = "")
    {
            
        InitializeComponent();
        _voucher = new FinanceDesign();
        _entry = new ClsFinanceEntry();
        _setup = new ClsMasterSetup();
        _voucherType = voucherType;
        _voucher.GetCurrencyExchangeDesign(RGrid);

        ReceiptControlsInDataGrid();

        GetRGridColumns();
        EnableControl();
        ClearControl();
    }

    private void FrmCurrencyExchange_Load(object sender, EventArgs e)
    {
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], Tag);
    }

    private void FrmCurrencyExchange_KeyPress(object sender, KeyPressEventArgs e)
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
                if (TxtCurrency.Enabled)
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

    private void FrmCurrencyExchange_Shown(object sender, EventArgs e)
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
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
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

        }
    }
    private void TxtVendorLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnVendor.PerformClick();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            var result = GetMasterList.CreateGeneralLedger("VENDOR", true);
            if (result.id > 0)
            {
                TxtVendorLedger.Text = result.description;

            }
        }

    }
    private void BtnVendor_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetGeneralLedger("SAVE", "VENDOR");
        if (result.id > 0)
        {
            _vendorLedgerId = result.id;
            TxtVendorLedger.Text = result.description;
        }

        TxtVendorLedger.Focus();
    }

    private void TxtCustomerLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCustomer.PerformClick();
        }
        else if (e.KeyCode is Keys.Delete)
        {
            _customerLedgerId = 0;
            TxtCustomerLedger.Clear();
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {

        }
    }

    private void BtnCustomer_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.GetGeneralLedger("SAVE", "CUSTOMER");
        if (result.id > 0)
        {
            _customerLedgerId = result.id;
            TxtCustomerLedger.Text = result.description;
        }

        TxtCustomerLedger.Focus();
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

        if (e.KeyCode is Keys.Enter && !TxtCurrency.Enabled)
        {
            e.SuppressKeyPress = true;
            ReceiptControlsInDataGrid();
            EnableRGridControl(true);
            ClearRLedgerDetails();
            if (RGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.IsValueExits())
            {
                TextFromRGrid();
                TxtCurrency.Focus();
                return;
            }

            GetRSerialNo();
            TxtCurrency.Focus();
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
        if (TxtCurrency.Enabled)
        {
            return;
        }
        _rowIndex = e.RowIndex;
        ReceiptControlsInDataGrid();
    }
    private void TxtBuyRate_Validating(object sender, CancelEventArgs e)
    {
        TxtBuyRate.Text = TxtBuyRate.GetDecimalString();
    }
    private void TxtSalesRateOnValidating(object sender, CancelEventArgs e)
    {
        TxtSalesRate.Text = TxtSalesRate.GetDecimalString();
    }
    private void TxtAmountOnValidating(object sender, CancelEventArgs e)
    {
        TxtAmount.Text = TxtAmount.GetDecimalString();
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
                TxtCurrency.Text = description;
                _currencyId = id;
                TxtBuyRate.Text = exchangeRate;
            }
            TxtCurrency.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCurrency, OpenRCurrency);
        }
    }
    private void TxtRNarration_Validating(object sender, CancelEventArgs e)
    {
        TxtCurrency.TabStop = true;
        if (!TxtCurrency.Enabled)
        {
            return;
        }
        if (!AddTextToRGrid(_isRowUpdate))
        {
            return;
        }
        TxtCurrency.Focus();
    }
    private void TxtRNarration_Validated(object sender, EventArgs e)
    {

    }
    private void TxtRNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode == Keys.Tab)
        {
            SendToBack();
            return;
        }

        if (RGrid.CurrentRow != null && RGrid.CurrentRow.Index - 1 >= 0 && RGrid.Rows.Count > RGrid.CurrentRow.Index - 1)
        {
            TxtNarration.Text = e.KeyCode switch
            {
                Keys.F2 => RGrid.Rows.Count > 0 ? RGrid.Rows[RGrid.CurrentRow.Index - 1].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F3 => RGrid.Rows.Count > 0 ? RGrid.Rows[1].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F4 => RGrid.Rows.Count > 0 ? RGrid.Rows[2].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F5 => RGrid.Rows.Count > 0 ? RGrid.Rows[3].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                Keys.F6 => RGrid.Rows.Count > 0 ? RGrid.Rows[RGrid.Rows.Count - 1].Cells["GTxtRNarration"].Value?.ToString() : string.Empty,
                _ => TxtNarration.Text
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
    }
    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? " CURRENCY PURCHASE VOUCHER ENTRY "
            : $" CURRENCY PURCHASE VOUCHER ENTRY [{_actionTag}]";
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

        ClearRLedgerDetails();
    }
    private void ClearRLedgerDetails()
    {
        if (RGrid.RowCount is 0)
        {
            RGrid.Rows.Add();
            TxtSno.Text = RGrid.RowCount.ToString();
        }
        _isRowUpdate = false;
        _currencyId = 0;
        TxtCurrency.Clear();
        TxtBuyRate.Clear();
        TxtSalesRate.Clear();
        TxtNarration.Clear();
        VoucherTotalCalculation();
    }
    private void EnableRGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;

        TxtCurrency.Enabled = TxtCurrency.Visible = isEnable;

        TxtAmount.Enabled = TxtAmount.Visible = isEnable;

        TxtSalesRate.Enabled = TxtSalesRate.Visible = isEnable;
        TxtBuyRate.Enabled = TxtBuyRate.Visible = isEnable;

        TxtNarration.Enabled = TxtNarration.Visible = isEnable;
    }
    private void OpenRCurrency()
    {
        var (description, id, currencyRate) = GetMasterList.GetCurrencyList(_actionTag);
        if (id > 0)
        {
            TxtCurrency.Text = description;
            TxtBuyRate.Text = currencyRate;
            _currencyId = id;
        }

        TxtCurrency.Focus();
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

        TxtSno = new MrGridNumericTextBox(RGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };

        TxtCurrency = new MrGridTextBox(RGrid)
        {
            ReadOnly = true
        };
        TxtCurrency.KeyDown += TxtRCurrencyOnKeyDown;

        TxtAmount = new MrGridNumericTextBox(RGrid);
        TxtAmount.Validating += TxtAmountOnValidating;

        TxtBuyRate = new MrGridNumericTextBox(RGrid);
        TxtBuyRate.Validating += TxtBuyRate_Validating;

        TxtSalesRate = new MrGridNumericTextBox(RGrid);
        TxtSalesRate.Validating += TxtSalesRateOnValidating;

        TxtNarration = new MrGridNormalTextBox(RGrid);
        TxtNarration.KeyDown += TxtRNarration_KeyDown;
        TxtNarration.KeyPress += (_, e) => e.Handled = false;
        TxtNarration.Validating += TxtRNarration_Validating;
        TxtNarration.Validated += TxtRNarration_Validated;

        ObjGlobal.DGridColorCombo(RGrid);
    }
    private void TextFromRGrid()
    {
        if (RGrid.CurrentRow == null)
        {
            return;
        }
        TxtSno.Text = RGrid.Rows[_rowIndex].Cells["GTxtSNo"].Value.GetString();
        TxtCurrency.Text = RGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.GetString();
        _currencyId = RGrid.Rows[_rowIndex].Cells["GTxtCurrencyId"].Value.GetInt();
        TxtAmount.Text = RGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetDecimalString();
        TxtBuyRate.Text = RGrid.Rows[_rowIndex].Cells["GTxtBuyRate"].Value.GetDecimalString();
        TxtSalesRate.Text = RGrid.Rows[_rowIndex].Cells["GTxtSalesRate"].Value.GetDecimalString();
        TxtNarration.Text = RGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        _isRowUpdate = true;
    }
    private bool AddTextToRGrid(bool isUpdate)
    {
        if (TxtSalesRate.GetDecimal() <= 0)
        {
            this.NotifyValidationError(TxtSalesRate, "DEBIT & CREDIT AMOUNT CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            RGrid.Rows.Add();
            RGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        RGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = _currencyId;
        RGrid.Rows[iRows].Cells["GTxtCurrency"].Value = TxtCurrency.Text;

        RGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtAmount.GetDecimalString();
        RGrid.Rows[iRows].Cells["GTxtSalesRate"].Value = TxtSalesRate.GetDecimalString();
        RGrid.Rows[iRows].Cells["GTxtBuyRate"].Value = TxtBuyRate.GetDecimalString();

        RGrid.Rows[iRows].Cells["GTxtNarration"].Value = TxtNarration.Text;
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
        GetRSerialNo();
        return true;
    }
    private void VoucherTotalCalculation()
    {
        if (RGrid.RowCount > 0 && RGrid.Rows[0].Cells["GTxtCurrencyId"].Value.IsValueExits())
        {
            var viewRows = RGrid.Rows.OfType<DataGridViewRow>();
            var rows = viewRows as DataGridViewRow[] ?? viewRows.ToArray();

            LblTotalAmount.Text = rows.Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal()).GetDecimalString();
        }
    }
    private void GetRSerialNo()
    {
        for (var i = 0; i < RGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            RGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }
    private bool IsControlValidForm()
    {

        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }

        if (TxtVno.IsBlankOrEmpty())
        {
            TxtVno.WarningMessage(@"VOUCHER NUMBER IS REQUIRED..!!");
            return false;
        }

        if (_actionTag.Equals("DELETE"))
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, "TxtVno.Text") == DialogResult.No)
            {
                return true;

            }

            if (!MskDate.MaskCompleted)
            {
                MskDate.WarningMessage("VOUCHER DATE IS INVALID...!");
                return false;
            }

            if (_actionTag.Equals("MskDate.MaskCompleted") && !MskDate.Text.IsValidDateRange("D"))
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

            if (_actionTag != "SAVE")
            {
                if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) is DialogResult.No)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private static int SaveRemittanceVoucher()
    {
        return 0;
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
    private void ReceiptControlsInDataGrid()
    {
        if (RGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = RGrid.Columns["GTxtSNo"].Index;
        TxtSno.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtCurrency"].Index;
        TxtCurrency.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtCurrency.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtCurrency.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtAmount"].Index;
        TxtAmount.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAmount.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAmount.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtBuyRate"].Index;
        TxtBuyRate.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBuyRate.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBuyRate.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtSalesRate"].Index;
        TxtSalesRate.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSalesRate.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSalesRate.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtNarration"].Index;
        TxtNarration.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNarration.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNarration.TabIndex = columnIndex;
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
            }
        }
        ObjGlobal.DGridColorCombo(RGrid);
        RGrid.ClearSelection();
    }
    #endregion --------------- METHOD OF THIS FORM ---------------

    private int _currencyId = ObjGlobal.SysCurrencyId;

    private int _rowIndex;
    private int _columnIndex;

    private long _vendorLedgerId;
    private long _customerLedgerId;

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private bool _zoom;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;
    private string _voucherType;
    private string _zoomVno;

    private KeyPressEventArgs _getKeys;

    private readonly IFinanceEntry _entry;
    private readonly IMasterSetup _setup;
    private readonly IFinanceDesign _voucher;

    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtCurrency { get; set; }
    private MrGridNumericTextBox TxtAmount { get; set; }
    private MrGridNumericTextBox TxtBuyRate { get; set; }
    private MrGridNumericTextBox TxtSalesRate { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }


}