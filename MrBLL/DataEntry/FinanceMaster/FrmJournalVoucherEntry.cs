using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using DevExpress.XtraEditors;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.FinanceTransaction.JournalVoucher;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.JournalVoucher;
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

namespace MrBLL.DataEntry.FinanceMaster;

public partial class FrmJournalVoucherEntry : MrForm
{
    // JOURNAL VOUCHER

    #region --------------- LEDGER OPENING FORM ---------------

    public FrmJournalVoucherEntry(bool zoom, string txtZoomVno, bool isProvision = false, bool isMultiCurrency = false, string voucherMode = "")
    {
        InitializeComponent();
        _isZoom = zoom;
        _zoomVoucherNo = txtZoomVno.Length > 0 ? txtZoomVno : string.Empty;
        _isMultiCurrency = false;
        _isRowUpdate = false;
        _isRowDelete = false;
        _isProvision = isProvision;
        _isMultiCurrency = isMultiCurrency;
        _voucherMode = voucherMode.Length > 0 ? voucherMode : string.Empty;
        _voucherRepository = new JournalVoucherRepository();
        _voucher = new FinanceDesign();
        _setup = new ClsMasterSetup();
        _entry = new ClsFinanceEntry();
        GetGridColumns();
        AdjustControlsInDataGrid();
        EnableControl();
        ClearControl();
    }

    private void FrmJournalVoucherEntry_Shown(object sender, EventArgs e)
    {
        if (_voucherMode.IsBlankOrEmpty())
        {
            BtnNew.Focus();
        }
    }

    private void FrmJournalVoucherEntry_KeyPress(object sender, KeyPressEventArgs e)
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

    private void FrmJournalVoucherEntry_Load(object sender, EventArgs e)
    {
        if (!_isZoom)
        {
            return;
        }

        if (_voucherMode is "PROV")
        {
            BtnEdit.PerformClick();
            TxtVno.Text = _zoomVoucherNo.Length > 0 ? _zoomVoucherNo : null;
            FillJournalVoucher(TxtVno.Text);
            BtnSave.Text = @"&POST";
            TxtVno.Focus();
        }
        else if (!string.IsNullOrEmpty(_zoomVoucherNo) && _isProvision is false)
        {
            _actionTag = "UPDATE";
            ClearControl();
            TxtVno.Text = _zoomVoucherNo.Length > 0 ? _zoomVoucherNo : string.Empty;
            FillJournalVoucher(TxtVno.Text);
            BtnEdit.Focus();
        }
        else if (!string.IsNullOrEmpty(_zoomVoucherNo) && _isProvision is false)
        {
            TxtVno.Text = _zoomVoucherNo.Length > 0 ? _zoomVoucherNo : string.Empty;
            FillJournalVoucher(TxtVno.Text);
            BtnEdit.Focus();
        }
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
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
        ClearControl();
        EnableControl();
        TxtVno.Focus();
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
        _actionTag = "SAVE";
        ReturnVoucherNo();
        ClearControl();
        EnableControl(true);
        BtnVno.PerformClick();
        MskMiti.Focus();
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintVoucher();
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
        if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
        {
            Close();
        }
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

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (ActiveControl.Name == "BtnVno") return;

        if (TxtVno.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS  BLANK OR INVALID..!!");
        }
        else if (_actionTag.Equals("SAVE"))
        {
            var dtVoucher = _voucherRepository.IsCheckVoucherNoExits("AMS.JV_Master", "Voucher_no", TxtVno.Text);
            if (dtVoucher.RowsCount() > 0)
            {
                this.NotifyValidationError(TxtVno, "VOUCHER NUMBER ALREADY EXITS..!!");
            }
        }
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var category = _isProvision ? "PROV" : "Normal";
        var reportMode = _isProvision ? "O" : "N";
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "JV", category, reportMode);
        if (_actionTag != "SAVE")
        {
            FillJournalVoucher(TxtVno.Text);
        }

        TxtVno.Focus();
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
            {
                if (TxtCurrency.Enabled)
                {
                    TxtCurrency.Focus();
                }
                else if (TxtDepartment.Enabled)
                {
                    TxtDepartment.Focus();
                }
                else
                {
                    DGrid.Focus();
                }
            }
            else
            {
                GlobalEnter_KeyPress(sender, e);
            }
        }
    }

    private void MskRefDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (TxtCurrency.Enabled)
            {
                TxtCurrency.Focus();
            }
            else if (TxtDepartment.Enabled)
            {
                TxtDepartment.Focus();
            }
            else
            {
                DGrid.Focus();
            }
        }
    }

    private void TxtCurrency_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenCurrency();
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            var frm = new FrmCurrency(true);
            frm.ShowDialog();
            TxtCurrency.Text = frm.CurrencyDesc;
            _currencyId = frm.CurrencyId;
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtCurrency, OpenCurrency);
        }
    }

    private void TxtCurrency_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void TxtDepartment_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void BtnDepartment_Click(object sender, EventArgs e)
    {
    }

    private void TxtNarration_Validating(object sender, CancelEventArgs e)
    {
        TxtLedger.TabStop = true;
        if (!TxtLedger.Enabled || ActiveControl == TxtCredit) return;

        if (!AddTextToGrid(_isRowUpdate)) return;

        TxtLedger.Focus();
    }

    private void TxtNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode == Keys.Tab)
        {
            TxtCredit.Focus();
            SendToBack();
            return;
        }

        if (DGrid.CurrentRow != null)
        {
            TxtNarration.Text = e.KeyCode switch
            {
                Keys.F2 => DGrid.Rows.Count > 0 ? DGrid.Rows[DGrid.CurrentRow.Index - 1].Cells["GTxtNarration"].Value?.ToString()
                    : string.Empty,
                Keys.F3 => DGrid.Rows.Count > 1
                    ? DGrid.Rows[1].Cells["GTxtNarration"].Value?.ToString()
                    : string.Empty,
                Keys.F4 => DGrid.Rows.Count > 2
                    ? DGrid.Rows[2].Cells["GTxtNarration"].Value?.ToString()
                    : string.Empty,
                Keys.F5 => DGrid.Rows.Count > 3
                    ? DGrid.Rows[3].Cells["GTxtNarration"].Value?.ToString()
                    : string.Empty,
                Keys.F6 => DGrid.Rows.Count > 4
                    ? DGrid.Rows[DGrid.Rows.Count - 1].Cells["GTxtNarration"].Value?.ToString()
                    : string.Empty,
                _ => TxtNarration.Text
            };
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire && !Debugger.IsAttached)
        {
            this.NotifyWarning("SOFTWARE LICENSE HAS BEEN EXPIRED..!! CONTACT WITH VENDOR FOR FURTHER..!!");
            return;
        }
        if (!IsValidForm())
        {
            return;
        }

        if (SaveJournalVoucher() > 0)
        {
            if (_isZoom)
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
            {
                TxtVno.Focus();
            }
            else
            {
                MskMiti.Focus();
            }
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
    }

    private void TxtRemarks_Validating(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.FinanceRemarksEnable && TxtRemarks.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtRemarks, "REMARKS IS MANDATORY PLEASE ENTER REMARKS OF VOUCHER");
        }
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
        if (e.KeyChar is (char)Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
    }

    //DATA GRID CONTROL
    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && DGrid.RowCount > 0)
        {
            if (CustomMessageBox.DeleteRow() is DialogResult.No) return;

            _isRowDelete = true;
            if (DGrid.CurrentRow is { Index: >= 0 } && DGrid.Rows.Count > DGrid.CurrentRow.Index)
            {
                DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index);
            }

            if (DGrid.RowCount is 0)
            {
                DGrid.Rows.Add();
            }

            GetSerialNo();
            VoucherTotalCalculation();
        }

        if (e.KeyCode is Keys.Enter && !TxtLedger.Enabled)
        {
            e.SuppressKeyPress = true;
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
        if (CustomMessageBox.DeleteRow() is DialogResult.No) return;

        _isRowDelete = true;
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!_isRowDelete) return;

        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
        }

        GetSerialNo();
    }

    #endregion --------------- LEDGER OPENING FORM ---------------

    //METHOD FOR THIS FORM

    #region --------------- METHOD OF THIS FORM ---------------

    private void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("JV");
        if (dt?.Rows.Count is 1)
        {
            _numberSchema = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("JV", _numberSchema);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("JV");
            if (wnd.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrEmpty(wnd.VNo)) return;

            _numberSchema = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TxtVno.Enabled = BtnVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskDate.Enabled = MskMiti.Enabled = isEnable;
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
        Text = _actionTag.IsBlankOrEmpty() ? "JOURNAL VOUCHER ENTRY" : $"JOURNAL VOUCHER ENTRY [{_actionTag}]";
        if (!_isZoom)
        {
            TxtVno.Clear();
            TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("JV", _numberSchema) : TxtVno.Text;
            TxtVno.ReadOnly = !_actionTag.Equals("SAVE");
            if (BtnNew.Enabled)
            {
                MskDate.Text = DateTime.Now.GetDateString();
                MskMiti.GetNepaliDate(MskDate.Text);
                MskRefDate.Text = MskMiti.Text;
            }

            TxtRefVno.Clear();
            TxtRemarks.Clear();
            _currencyId = ObjGlobal.SysCurrencyId;
            TxtCurrency.Text = ObjGlobal.SysCurrency;
            TxtCurrencyRate.Text = 1.GetDecimalString();
            lbl_NoInWordsDetl.IsClear();
            LblTotalDebit.IsClear();
            LblTotalCredit.IsClear();
            LblTotalLocalDebit.IsClear();
            LblTotalLocalCredit.IsClear();
            DGrid.ReadOnly = true;
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
        TxtItemDepartment.Clear();
        TxtItemCurrency.Clear();
        TxtItemCurrencyRate.Clear();
        LblPanNo.IsClear();
        LblBalance.IsClear();
        TxtDebit.Clear();
        TxtCredit.Clear();
        TxtNarration.Clear();
        AdjustControlsInDataGrid();
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

        TxtItemCurrency.Enabled = TxtItemCurrency.Visible = isEnable && _isMultiCurrency;
        TxtItemCurrencyRate.Enabled = TxtItemCurrencyRate.Visible = isEnable && _isMultiCurrency;

        TxtDebit.Enabled = TxtDebit.Visible = isEnable;
        TxtCredit.Enabled = TxtCredit.Visible = isEnable;
        TxtNarration.Enabled = TxtNarration.Visible = isEnable;
    }

    private void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0) return;

        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["Balance"].GetDecimalString();
        if (DGrid.RowCount > 0)
        {
            var diff = LblTotalDebit.GetDecimal() - LblTotalCredit.GetDecimal();
            if (diff > 0)
                TxtCredit.Text = diff.GetDecimalString();
            else
                TxtDebit.Text = Math.Abs(diff).GetDecimalString();
        }
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
        TxtItemDepartment.Focus();
    }

    private void OpenItemDepartment()
    {
        (TxtItemDepartment.Text, _departmentId) = GetMasterList.GetDepartmentList(_actionTag);
        TxtItemDepartment.Focus();
    }

    private void OpenCurrency()
    {
        (TxtCurrencyRate.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
    }

    private void OpenItemCurrency()
    {
        (TxtItemCurrency.Text, _itemCurrencyId, TxtItemCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
        TxtItemCurrency.Focus();
    }

    private void GetGridColumns()
    {
        _voucher.GetJournalVoucherDesign(DGrid, "S");
        if (DGrid.ColumnCount > 0 && DGrid != null)
        {
            DGrid.Columns["GTxtSubledger"].Visible = ObjGlobal.FinanceSubLedgerEnable;
            if (DGrid.Columns["GTxtSubledger"].Visible)
            {
                DGrid.Columns["GTxtLedger"].Width = DGrid.Columns["GTxtLedger"].Width - DGrid.Columns["GTxtSubledger"].Width;
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
            DGrid.Columns["GTxtCurrency"].Visible = _isMultiCurrency;
            if (DGrid.Columns["GTxtCurrency"].Visible)
            {
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtCurrency"].Width;
            }
            DGrid.Columns["GTxtExchangeRate"].Visible = _isMultiCurrency;
            if (DGrid.Columns["GTxtExchangeRate"].Visible)
            {
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtExchangeRate"].Width;
            }
        }

        DGrid.RowEnter += OnDGridOnRowEnter;
        DGrid.CellEnter += OnDGridOnCellEnter;
        DGrid.KeyDown += DGrid_KeyDown;
        DGrid.UserDeletingRow += DGrid_UserDeletingRow;
        DGrid.UserDeletedRow += DGrid_UserDeletedRow;
        DGrid.EnterKeyPressed += DGrid_EnterKeyPressed;

        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };

        TxtLedger = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtLedger.KeyDown += OnTxtLedgerOnKeyDown;
        TxtLedger.Leave += OnTxtLedgerOnLeave;
        TxtLedger.Validating += OnTxtLedgerOnValidating;

        TxtSubLedger = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtSubLedger.KeyDown += OnTxtSubLedgerOnKeyDown;
        TxtAgent = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtAgent.KeyDown += OnTxtAgentOnKeyDown;
        TxtItemDepartment = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtItemDepartment.KeyDown += OnTxtItemDepartmentOnKeyDown;

        TxtItemCurrency = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtItemCurrency.KeyDown += TxtItemCurrencyOnKeyDown;

        TxtItemCurrencyRate = new MrGridNumericTextBox(DGrid);
        TxtItemCurrencyRate.TextChanged += TxtItemCurrencyRateOnTextChanged;

        TxtDebit = new MrGridNumericTextBox(DGrid);
        TxtDebit.Validating += OnTxtDebitOnValidating;

        TxtCredit = new MrGridNumericTextBox(DGrid);
        TxtCredit.Validating += OnTxtCreditOnValidating;

        TxtNarration = new MrGridNormalTextBox(DGrid);
        TxtNarration.KeyDown += TxtNarration_KeyDown;
        TxtNarration.KeyPress += OnTxtNarrationOnKeyPress;
        TxtNarration.Validating += TxtNarration_Validating;

        ObjGlobal.DGridColorCombo(DGrid);
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

    private void OnDGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void OnDGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        if (!DGrid.Focused || TxtLedger.Enabled) return;

        _rowIndex = e.RowIndex;
    }

    private void OnTxtLedgerOnKeyDown(object _, KeyEventArgs e)
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

    private void OnTxtLedgerOnLeave(object sender, EventArgs e)
    {
        if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() > 0 && TxtLedger.IsBlankOrEmpty())
        {
            EnableGridControl();
        }
    }

    private void OnTxtNarrationOnKeyPress(object _, KeyPressEventArgs e)
    {
        e.Handled = false;
    }

    private void OnTxtCreditOnValidating(object o, CancelEventArgs cancelEventArgs)
    {
        TxtCredit.Text = TxtCredit.GetDecimalString();
        TxtDebit.Text = TxtCredit.GetDecimal() > 0 ? 0.GetDecimalString() : TxtDebit.GetDecimalString();
    }

    private void OnTxtDebitOnValidating(object o, CancelEventArgs cancelEventArgs)
    {
        TxtDebit.Text = TxtDebit.GetDecimalString();
        TxtCredit.Text = TxtDebit.GetDecimal() > 0 ? 0.GetDecimalString() : TxtCredit.GetDecimalString();
    }

    private void OnTxtItemDepartmentOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1)
        {
            OpenItemDepartment();
        }
        else if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
        else if (e.Control && e.KeyCode == Keys.N)
        {
            (TxtItemDepartment.Text, _itemDepartmentId) = GetMasterList.CreateDepartment(true);
            TxtItemDepartment.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtItemDepartment, OpenItemDepartment);
        }
    }

    private void OnTxtAgentOnKeyDown(object _, KeyEventArgs e)
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
            (TxtAgent.Text, _agentId) = GetMasterList.CreateAgent(true);
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtAgent, OpenAgent);
        }
    }

    private void OnTxtSubLedgerOnKeyDown(object _, KeyEventArgs e)
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
            TxtSubLedger.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtSubLedger, OpenSubLedger);
        }
    }

    private void OnTxtLedgerOnValidating(object _, CancelEventArgs e)
    {
        if (DGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtLedger, "PLEASE SELECT GENERAL LEDGER FOR OPENING");
        }

        if (DGrid.RowCount == 1 && TxtLedger.Enabled && TxtLedger.IsBlankOrEmpty())
        {
            if (DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() is 0)
            {
                if (_getKeys.KeyChar is (char)Keys.Enter) _getKeys.Handled = false;

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

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null) return;

        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSNo"].Value.GetString();
        _ledgerId = DGrid.Rows[_rowIndex].Cells["GTxtLedgerId"].Value.GetLong();
        TxtLedger.Text = DGrid.Rows[_rowIndex].Cells["GTxtLedger"].Value.GetString();
        SetLedgerInfo(_ledgerId);

        _subLedgerId = DGrid.Rows[_rowIndex].Cells["GTxtSubledgerId"].Value.GetInt();
        TxtSubLedger.Text = DGrid.Rows[_rowIndex].Cells["GTxtSubLedger"].Value.GetString();

        _agentId = DGrid.Rows[_rowIndex].Cells["GTxtAgentId"].Value.GetInt();
        TxtAgent.Text = DGrid.Rows[_rowIndex].Cells["GTxtAgent"].Value.GetString();

        _departmentId = DGrid.Rows[_rowIndex].Cells["GTxtDepartmentId"].Value.GetInt();
        TxtDepartment.Text = DGrid.Rows[_rowIndex].Cells["GTxtDepartment"].Value.GetString();

        _itemCurrencyId = DGrid.Rows[_rowIndex].Cells["GTxtCurrencyId"].Value.GetInt();
        TxtItemCurrency.Text = DGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.GetString();
        TxtItemCurrencyRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtExchangeRate"].Value.GetString();

        TxtDebit.Text = DGrid.Rows[_rowIndex].Cells["GTxtDebit"].Value.GetDecimalString();
        TxtCredit.Text = DGrid.Rows[_rowIndex].Cells["GTxtCredit"].Value.GetDecimalString();

        TxtNarration.Text = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        _isRowUpdate = true;
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;

        var currentRow = DGrid.CurrentRow.Index;
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

        columnIndex = DGrid.Columns["GTxtDebit"].Index;
        TxtDebit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtDebit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtDebit.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtCredit"].Index;
        TxtCredit.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtCredit.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtCredit.TabIndex = columnIndex;

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

        if (Math.Abs(TxtCredit.GetDecimal() - TxtDebit.GetDecimal()) is 0)
        {
            this.NotifyValidationError(TxtDebit, "DEBIT & CREDIT AMOUNT CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = DGrid.CurrentRow?.Index ?? _rowIndex;
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
        var currencyRate = TxtItemCurrencyRate.GetDecimal() > 0 ? TxtItemCurrencyRate.GetDecimal() : TxtCurrencyRate.GetDecimal(true);
        DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = currencyRate.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtDebit"].Value = TxtDebit.GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtLocalDebit"].Value = (currencyRate * TxtDebit.GetDecimal()).GetDecimalString();

        DGrid.Rows[iRows].Cells["GTxtCredit"].Value = TxtCredit.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtLocalCredit"].Value = (currencyRate * TxtCredit.GetDecimal()).GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = TxtNarration.Text;
        var currentRow = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[currentRow].Cells[_columnIndex];
        if (_isRowUpdate)
        {
            EnableGridControl();
            ClearLedgerDetails();
            DGrid.Focus();
            return false;
        }

        ClearLedgerDetails();
        TxtLedger.AcceptsTab = false;
        GetSerialNo();
        return true;
    }

    private void FillJournalVoucher(string voucherNo)
    {
        var dsVoucher = _voucherRepository.ReturnJournalVoucherInDataSet(voucherNo);
        if (dsVoucher.Tables[0].Rows.Count <= 0) return;

        foreach (DataRow ro in dsVoucher.Tables[0].Rows)
        {
            if (_actionTag is not "SAVE" or "COPY")
            {
                TxtVno.Text = ro["Voucher_No"].ToString();
            }

            MskDate.Text = ro["Voucher_Date"].ToString();
            MskMiti.Text = ro["Voucher_Miti"].ToString();
            TxtRefVno.Text = ro["Ref_VNo"].ToString();
            MskRefDate.GetNepaliDate(ro["Ref_VDate"].GetDateString());

            if (ro["Cls1"].GetInt() > 0)
            {
                TxtDepartment.Text = ro["Department1"].ToString();
                _departmentId = ro["Cls1"].GetInt();
            }

            _currencyId = ro["Currency_Id"].GetInt();
            TxtCurrencyRate.Text = ro["Currency_Rate"].GetDecimalString(true);
            TxtRemarks.Text = ro["Remarks"].ToString();

            PAttachment1.Image = ro["PAttachment1"].GetImage();
            PAttachment2.Image = ro["PAttachment2"].GetImage();
            PAttachment3.Image = ro["PAttachment3"].GetImage();
            PAttachment4.Image = ro["PAttachment4"].GetImage();
            PAttachment5.Image = ro["PAttachment5"].GetImage();
        }

        if (dsVoucher.Tables[1].Rows.Count > 0)
        {
            var rows = 0;
            DGrid.Rows.Clear();
            DGrid.Rows.Add(dsVoucher.Tables[1].Rows.Count + 1);
            for (var i = 0; i < dsVoucher.Tables[1].Rows.Count; i++)
            {
                DGrid.Rows[rows].Cells["GTxtSNo"].Value = dsVoucher.Tables[1].Rows[i]["SNo"].GetInt();
                DGrid.Rows[rows].Cells["GTxtLedgerId"].Value = dsVoucher.Tables[1].Rows[i]["GlId"].GetLong();
                DGrid.Rows[rows].Cells["GTxtLedger"].Value = dsVoucher.Tables[1].Rows[i]["GLName"].ToString();

                DGrid.Rows[rows].Cells["GTxtSubledgerId"].Value = dsVoucher.Tables[1].Rows[i]["Subledger_Id"].GetInt();
                DGrid.Rows[rows].Cells["GTxtSubLedger"].Value = dsVoucher.Tables[1].Rows[i]["SLName"].ToString();

                DGrid.Rows[rows].Cells["GTxtDepartmentId"].Value = dsVoucher.Tables[1].Rows[i]["Cls1"].GetInt();
                DGrid.Rows[rows].Cells["GTxtDepartment"].Value = dsVoucher.Tables[1].Rows[i]["Department1"].ToString();

                DGrid.Rows[rows].Cells["GTxtAgentId"].Value = dsVoucher.Tables[1].Rows[i]["Agent_ID"].GetInt();
                DGrid.Rows[rows].Cells["GTxtAgent"].Value = dsVoucher.Tables[1].Rows[i]["AgentName"].ToString();

                DGrid.Rows[rows].Cells["GTxtCurrencyId"].Value = dsVoucher.Tables[1].Rows[i]["CurrencyId"].GetInt();
                DGrid.Rows[rows].Cells["GTxtCurrency"].Value = dsVoucher.Tables[1].Rows[i]["Ccode"].ToString();
                DGrid.Rows[rows].Cells["GTxtExchangeRate"].Value = dsVoucher.Tables[1].Rows[i]["CurrencyRate"].GetDecimalString();

                DGrid.Rows[rows].Cells["GTxtDebit"].Value = dsVoucher.Tables[1].Rows[i]["Debit"].GetDecimalString();
                DGrid.Rows[rows].Cells["GTxtLocalDebit"].Value = dsVoucher.Tables[1].Rows[i]["LocalDebit"].GetDecimalString();
                DGrid.Rows[rows].Cells["GTxtCredit"].Value = dsVoucher.Tables[1].Rows[i]["Credit"].GetDecimalString();
                DGrid.Rows[rows].Cells["GTxtLocalCredit"].Value = dsVoucher.Tables[1].Rows[i]["LocalCredit"].GetDecimalString();
                DGrid.Rows[rows].Cells["GTxtNarration"].Value = dsVoucher.Tables[1].Rows[i]["Narration"].ToString();
                rows++;
            }

            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
        }

        ClearLedgerDetails();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetInt() <= 0) return;

        LblTotalDebit.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtDebit"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalDebit.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtLocalDebit"].Value.GetDecimal()).GetDecimalString();
        LblTotalCredit.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtCredit"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalCredit.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtLocalCredit"].Value.GetDecimal()).GetDecimalString();
        lbl_NoInWordsDetl.Text = ClsMoneyConversion.MoneyConversion(
            LblTotalDebit.GetDecimal() > LblTotalCredit.GetDecimal() ? LblTotalDebit.Text : LblTotalCredit.Text);
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

        if (_tagStrings.Contains(_actionTag)) return true;

        if (LblTotalLocalDebit.GetDecimal() != LblTotalLocalCredit.GetDecimal())
        {
            this.NotifyValidationError(DGrid, "VOUCHER DEBIT AND CREDIT IS EQUAL..!!");
            DGrid.CurrentCell = DGrid.Rows[DGrid.RowCount - 1].Cells[0];
            return false;
        }

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

    private int SaveJournalVoucher()
    {
        if (_actionTag is "SAVE")
        {
            TxtVno.GetCurrentVoucherNo("JV", _numberSchema);
        }

        var syncRow = 0;
        _voucherRepository.JvMaster.VoucherMode = _isProvision ? "PROV" : "N";
        _voucherRepository.JvMaster.Voucher_No = TxtVno.Text;
        _voucherRepository.JvMaster.Voucher_Date = Convert.ToDateTime(MskDate.Text);
        _voucherRepository.JvMaster.Voucher_Miti = MskMiti.Text;
        _voucherRepository.JvMaster.Ref_VNo = TxtRefVno.Text;
        _voucherRepository.JvMaster.Ref_VDate = TxtRefVno.Text.IsValueExits() ? MskRefDate.GetEnglishDate().GetDateTime() : DateTime.Now;
        _voucherRepository.JvMaster.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _voucherRepository.JvMaster.Currency_Rate = TxtCurrencyRate.GetDecimal() > 0 ? TxtCurrencyRate.GetDecimal() : 1;
        _voucherRepository.JvMaster.Remarks = TxtRemarks.Text;
        _voucherRepository.JvMaster.Action_Type = _actionTag.Length > 0 ? _actionTag : null;
        _voucherRepository.JvMaster.GetView = DGrid;
        _voucherRepository.JvMaster.SyncRowVersion = syncRow.ReturnSyncRowNo("JV", TxtVno.Text);
        _voucherRepository.JvDetailsList.Clear();
        if (DGrid.RowCount > 0)
        {
            foreach (DataGridViewRow viewRow in DGrid.Rows)
            {
                var list = new JV_Details();
                var ledgerId = viewRow.Cells["GTxtLedgerId"].Value.GetLong();
                if (ledgerId is 0)
                {
                    continue;
                }
                list.Voucher_No = TxtVno.Text;
                list.SNo = viewRow.Cells["GTxtSNo"].Value.GetInt();
                list.CBLedger_Id = _ledgerId;
                list.Ledger_Id = viewRow.Cells["GTxtLedgerId"].Value.GetLong();
                list.Subledger_Id = viewRow.Cells["GTxtSubledgerId"].Value.GetInt();
                list.Agent_Id = viewRow.Cells["GTxtAgentId"].Value.GetInt();
                list.Cls1 = viewRow.Cells["GTxtDepartmentId"].Value.GetInt();
                list.Cls2 = 0;
                list.Cls3 = 0;
                list.Cls4 = 0;
                list.Chq_No = string.Empty;
                list.Chq_Date = DateTime.Now;
                list.CurrencyId = viewRow.Cells["GTxtCurrencyId"].Value.GetInt();
                list.CurrencyRate = viewRow.Cells["GTxtExchangeRate"].Value.GetDecimal();
                list.Debit = viewRow.Cells["GTxtDebit"].Value.GetDecimal();
                list.Credit = viewRow.Cells["GTxtCredit"].Value.GetDecimal();
                list.LocalDebit = viewRow.Cells["GTxtLocalDebit"].Value.GetDecimal();
                list.LocalCredit = viewRow.Cells["GTxtLocalCredit"].Value.GetDecimal();
                list.Tbl_Amount = 0;
                list.V_Amount = 0;
                list.Narration = viewRow.Cells["GTxtNarration"].Value.GetString();
                list.Party_No = string.Empty;
                list.Invoice_Date = DateTime.Now;
                list.Invoice_Miti = string.Empty;
                list.VatLedger_Id = 0;
                list.PanNo = string.Empty;
                list.Vat_Reg = false;
                list.SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty;
                list.SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? ObjGlobal.LocalOriginId : Guid.Empty;
                list.SyncCreatedOn = DateTime.Now;
                list.SyncLastPatchedOn = DateTime.Now;
                list.SyncRowVersion = _voucherRepository.JvMaster.SyncRowVersion;
                _voucherRepository.JvDetailsList.Add(list);
            }
        }
        //_entry.GetImage.PAttachment1 = PAttachment1.Image.ConvertImage();
        //_entry.GetImage.PAttachment2 = PAttachment2.Image.ConvertImage();
        //_entry.GetImage.PAttachment3 = PAttachment3.Image.ConvertImage();
        //_entry.GetImage.PAttachment4 = PAttachment4.Image.ConvertImage();
        //_entry.GetImage.PAttachment5 = PAttachment5.Image.ConvertImage();

        return _voucherRepository.SaveJournalVoucher(_actionTag);
    }

    private void PrintVoucher()
    {
        var dtDesign = _setup.GetPrintVoucherList("JV");
        var frmName = dtDesign.Rows.Count > 0 ? "Crystal" : "DLL";
        var frmDp = new FrmDocumentPrint(frmName, "JV", TxtVno.Text, TxtVno.Text, true)
        {
            Owner = ActiveForm
        };
        frmDp.ShowDialog();
    }

    #endregion --------------- METHOD OF THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region -------------- OBJECT FOR THIS FORM --------------

    private int _subLedgerId;
    private int _departmentId;
    private int _itemDepartmentId;
    private int _currencyId = ObjGlobal.SysCurrencyId;
    private int _itemCurrencyId = ObjGlobal.SysCurrencyId;
    private int _agentId;
    private int _rowIndex;
    private int _columnIndex;

    private readonly bool _isZoom;
    private bool _isRowDelete;
    private bool _isRowUpdate;
    private readonly bool _isProvision;
    private readonly bool _isMultiCurrency;
    private long _ledgerId;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _numberSchema = string.Empty;
    private readonly string _zoomVoucherNo;
    private readonly string _voucherMode;

    private KeyPressEventArgs _getKeys;

    private readonly IFinanceEntry _entry;
    private readonly IMasterSetup _setup;
    private readonly IFinanceDesign _voucher;
    private readonly IJournalVoucherRepository _voucherRepository;

    // DATA GRID CONTROL
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtLedger { get; set; }
    private MrGridTextBox TxtSubLedger { get; set; }
    private MrGridTextBox TxtAgent { get; set; }
    private MrGridTextBox TxtItemDepartment { get; set; }
    private MrGridTextBox TxtItemCurrency { get; set; }
    private MrGridNumericTextBox TxtItemCurrencyRate { get; set; }
    private MrGridNumericTextBox TxtDebit { get; set; }
    private MrGridNumericTextBox TxtCredit { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }

    #endregion -------------- OBJECT FOR THIS FORM --------------
}