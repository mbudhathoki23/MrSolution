using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.FinanceTransaction.NotesMaster;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.NotesMaster;
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

public partial class FrmVoucherNotesEntry : MrForm
{
    #region --------------- LEDGER OPENING FORM ---------------

    public FrmVoucherNotesEntry(bool zoom = false, string txtZoomVno = "", string voucherType = "DN", bool isProvision = false, bool multiCurrency = false)
    {
        InitializeComponent();
        _zoom = zoom;
        _zoomVno = txtZoomVno;
        _voucherType = voucherType;
        _isProvision = isProvision;
        _multiCurrency = multiCurrency;
        _notesMasterRepository = new NotesMasterRepository();
        IniInitializeGrid();
        AdjustControlsInDataGrid();
        EnableControl();
        ClearControl();
    }

    private void FrmVoucherNotesEntry_Shown(object sender, EventArgs e)
    {
        if (BtnNew.Enabled)
            BtnNew.Focus();
        else
            MskMiti.Focus();
    }

    private void FrmVoucherNotesEntry_KeyPress(object sender, KeyPressEventArgs e)
    {
        _getKeys = e;
        if (e.KeyChar is not (char)Keys.Escape) return;
        if (BtnNew.Enabled)
        {
            var result = CustomMessageBox.ExitActiveForm();
            if (result == DialogResult.Yes) Close();
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

    private void FrmVoucherNotesEntry_Load(object sender, EventArgs e)
    {
        if (_zoom && !string.IsNullOrEmpty(_zoomVno) && _voucherType == "CB")
        {
            TxtVno.Text = _zoomVno;
            FillCashBankVoucher(TxtVno.Text);
        }
        else if (_zoom && !string.IsNullOrEmpty(_zoomVno) && _voucherType == "PDC")
        {
            BtnNew_Click(sender, e);
            TxtRefVno.Text = _zoomVno;
            FillPdcVoucherDetails(TxtRefVno.Text);
        }
        else
        {
            if (!_zoom || string.IsNullOrEmpty(_zoomVno) || _voucherType != "PROV")
            {
                return;
            }
            _actionTag = "POST";
            ClearControl();
            FillCashBankVoucher(_zoomVno);
            BtnEdit.Text = BtnSave.Text = @"&POST";
            BtnSave.Focus();
        }
    }

    #endregion --------------- LEDGER OPENING FORM ---------------

    //METHOD FOR THIS FORM

    #region --------------- METHOD OF THIS FORM ---------------

    internal void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("DN");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("DN", _docDesc);
        }
        else if (dt != null && dt.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("DN");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    internal void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TxtVno.Enabled = BtnVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskDate.Enabled = MskMiti.Enabled = isEnable;
        TxtPartyLedger.Enabled = BtnPartyLedger.Enabled = isEnable;
        TxtCurrency.Enabled = BtnCurrency.Enabled = isEnable && ObjGlobal.FinanceCurrencyEnable;
        TxtCurrencyRate.Enabled = TxtCurrency.Enabled;
        TxtRefVno.Enabled = MskRefDate.Enabled = isEnable;
        TabLedgerOpening.Enabled = !_tagStrings.Contains(_actionTag) && isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.FinanceDepartmentEnable;
        TxtRemarks.Enabled = btnRemarks.Enabled = isEnable && ObjGlobal.FinanceRemarksEnable;
        EnableGridControl();
    }

    internal void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? "DEBIT NOTES VOUCHER DETAILS"
            : $"DEBIT NOTES VOUCHER DETAILS [{_actionTag}]";
        if (!_zoom)
        {
            TxtVno.Clear();
            TxtVno.Text = _actionTag.Equals("SAVE") ? TxtVno.GetCurrentVoucherNo("DN", _docDesc) : TxtVno.Text;
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
            TxtPartyLedger.Text = _setup.GetLedgerDescription(_cbLedgerId);
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

    internal void ClearLedgerDetails()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            TxtSno.Text = DGrid.RowCount.ToString();
        }

        _isRowUpdate = false;
        _ledgerId = 0;
        TxtLedger.Clear();
        _subledgerId = 0;
        TxtSubledger.Clear();
        _agentId = 0;
        TxtAgent.Clear();
        _departmentId = 0;
        TxtDepartment.Clear();
        LblPanNo.IsClear();
        LblBalance.IsClear();
        TxtAmount.Clear();
        TxtNarration.Clear();
        AdjustControlsInDataGrid();
        VoucherTotalCalculation();
    }

    internal void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtLedger.Enabled = TxtLedger.Visible = isEnable;
        TxtSubledger.Enabled = TxtSubledger.Visible = isEnable && ObjGlobal.FinanceSubLedgerEnable;
        TxtItemDepartment.Enabled = TxtItemDepartment.Visible = isEnable && ObjGlobal.FinanceDepartmentItemEnable;
        TxtAgent.Enabled = TxtAgent.Visible = isEnable && ObjGlobal.FinanceAgentEnable;
        TxtAmount.Enabled = TxtAmount.Visible = isEnable;
        TxtNarration.Enabled = TxtNarration.Visible = isEnable;
        DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }

    internal void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0) return;
        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["Balance"].ToString();
    }

    internal void SetCashLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0) return;
        LblCashBalance.Text = dtLedger.Rows[0]["Balance"].GetDecimalString();
        _ledgerType = dtLedger.Rows[0]["GLType"].GetUpper();
        _balanceType = dtLedger.Rows[0]["CrTYpe"].GetUpper();
    }

    internal void OpenLedger()
    {
        (TxtLedger.Text, _ledgerId) = GetMasterList.GetGeneralLedger(_actionTag);
        SetLedgerInfo(_ledgerId);
    }

    internal void OpenSubLedger()
    {
        (TxtSubledger.Text, _subledgerId) = GetMasterList.GetSubLedgerList(_actionTag);
    }

    internal void OpenAgent()
    {
        (TxtAgent.Text, _agentId) = GetMasterList.GetAgentList(_actionTag);
    }

    internal void OpenDepartment()
    {
        (TxtDepartment.Text, _departmentId) = GetMasterList.GetDepartmentList(_actionTag);
    }

    internal void OpenCurrency()
    {
        (TxtCurrencyRate.Text, _currencyId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
    }

    internal void IniInitializeGrid()
    {
        _voucher.GetCashBankDesign(DGrid, "S");
        if (DGrid.ColumnCount > 0)
        {
            DGrid.Columns[@"GTxtCurrency"].Visible = _multiCurrency;
            DGrid.Columns[@"GTxtExchangeRate"].Visible = _multiCurrency;

            DGrid.Columns["GTxtLocalReceipt"].Visible = _multiCurrency;
            DGrid.Columns["GTxtLocalPayment"].Visible = _multiCurrency;

            DGrid.Columns["GTxtSubledger"].Visible = ObjGlobal.FinanceSubLedgerEnable;
            if (DGrid.Columns["GTxtSubledger"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtSubledger"].Width;
            else
                DGrid.Columns["GTxtNarration"].Width += DGrid.Columns["GTxtSubledger"].Width;

            DGrid.Columns["GTxtAgent"].Visible = ObjGlobal.FinanceAgentEnable;
            if (DGrid.Columns["GTxtAgent"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtAgent"].Width;
            else
                DGrid.Columns["GTxtNarration"].Width += DGrid.Columns["GTxtAgent"].Width;
            DGrid.Columns["GTxtDepartment"].Visible = ObjGlobal.FinanceDepartmentItemEnable;
            if (DGrid.Columns["GTxtDepartment"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtDepartment"].Width;

            DGrid.Columns["GTxtCurrency"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtCurrency"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtCurrency"].Width;

            DGrid.Columns["GTxtExchangeRate"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtExchangeRate"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtExchangeRate"].Width;

            DGrid.Columns["GTxtLocalReceipt"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtLocalReceipt"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtLocalReceipt"].Width;

            DGrid.Columns["GTxtLocalPayment"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtLocalPayment"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtLocalPayment"].Width;
        }

        DGrid.RowEnter += (_, e) => _rowIndex = e.RowIndex;
        DGrid.GotFocus += (sender, e) => { DGrid.Rows[_rowIndex].Cells[0].Selected = true; };
        DGrid.CellEnter += (_, e) => _columnIndex = e.ColumnIndex;
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
        TxtLedger.KeyDown += (_, e) =>
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
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtLedger, OpenLedger);
            }
        };
        TxtLedger.Validating += (_, e) =>
        {
            if (DGrid.RowCount is 0)
                this.NotifyValidationError(TxtLedger, "PLEASE SELECT GENERAL LEDGER FOR OPENING");

            if (DGrid.RowCount == 1 && TxtLedger.Enabled && TxtLedger.IsBlankOrEmpty())
                if (DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() is 0)
                {
                    if (_getKeys.KeyChar is (char)Keys.Enter) _getKeys.Handled = false;

                    this.NotifyValidationError(TxtLedger, "PLEASE SELECT GENERAL LEDGER FOR OPENING");
                    return;
                }

            if (DGrid.RowCount >= 1 && TxtLedger.Enabled && TxtLedger.IsBlankOrEmpty())
            {
                EnableGridControl();
                DGrid.ClearSelection();
                if (TxtRemarks.Enabled)
                    TxtRemarks.Focus();
                else
                    BtnSave.Focus();
            }
        };
        TxtSubledger = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtSubledger.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
                OpenSubLedger();
            else if (e.Shift && e.KeyCode is Keys.Tab)
                SendToBack();
            else if (e.Control && e.KeyCode == Keys.N)
                (TxtSubledger.Text, _subledgerId) = GetMasterList.CreateSubLedger(true);
            else
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtSubledger, OpenSubLedger);
        };
        TxtAgent = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtAgent.KeyDown += (_, e) =>
        {
            if (e.KeyCode == Keys.F1)
                OpenAgent();
            else if (e.Shift && e.KeyCode is Keys.Tab)
                SendToBack();
            else if (e.Control && e.KeyCode == Keys.N)
                (TxtAgent.Text, _agentId) = GetMasterList.CreateAgent(true);
            else
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtAgent, OpenAgent);
        };
        TxtItemDepartment = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtItemDepartment.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.F1)
                OpenDepartment();
            else if (e.Shift && e.KeyCode is Keys.Tab)
                SendToBack();
            else if (e.Control && e.KeyCode == Keys.N)
                (TxtDepartment.Text, _departmentId) = GetMasterList.CreateDepartment(true);
            else
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtDepartment, OpenDepartment);
        };
        TxtAmount = new MrGridNumericTextBox(DGrid);
        TxtAmount.Validating += (_, _) => { TxtAmount.Text = TxtAmount.GetDecimalString(); };
        TxtNarration = new MrGridNormalTextBox(DGrid);
        TxtNarration.KeyDown += TxtNarration_KeyDown;
        TxtNarration.KeyPress += (_, e) => e.Handled = false;
        TxtNarration.Validating += TxtNarration_Validating;
        TxtNarration.Validated += TxtNarration_Validated;
        ObjGlobal.DGridColorCombo(DGrid);
    }

    internal void TextFromGrid()
    {
        if (DGrid.CurrentRow == null) return;
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSNo"].Value.GetString();
        TxtLedger.Text = DGrid.Rows[_rowIndex].Cells["GTxtLedger"].Value.GetString();
        _ledgerId = DGrid.Rows[_rowIndex].Cells["GTxtLedgerId"].Value.GetLong();
        SetLedgerInfo(_ledgerId);
        TxtSubledger.Text = DGrid.Rows[_rowIndex].Cells["GTxtSubLedger"].Value.GetString();
        _subledgerId = DGrid.Rows[_rowIndex].Cells["GTxtSubledgerId"].Value.GetInt();
        TxtAgent.Text = DGrid.Rows[_rowIndex].Cells["GTxtAgent"].Value.GetString();
        _subledgerId = DGrid.Rows[_rowIndex].Cells["GTxtAgentId"].Value.GetInt();
        TxtDepartment.Text = DGrid.Rows[_rowIndex].Cells["GTxtDepartment"].Value.GetString();
        _subledgerId = DGrid.Rows[_rowIndex].Cells["GTxtDepartmentId"].Value.GetInt();
        TxtCurrency.Text = DGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.GetString();
        TxtCurrencyRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtExchangeRate"].Value.GetString();
        _currencyId = DGrid.Rows[_rowIndex].Cells["GTxtCurrencyId"].Value.GetInt();
        TxtAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetDecimalString();
        TxtNarration.Text = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        _isRowUpdate = true;
    }

    internal void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null) return;
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
        TxtSubledger.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSubledger.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSubledger.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAgent"].Index;
        TxtAgent.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAgent.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAgent.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtDepartment"].Index;
        TxtItemDepartment.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtItemDepartment.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtItemDepartment.TabIndex = columnIndex;

        // columnIndex = DGrid.Columns["GTxtAmount"].Index;
        // TxtAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        // TxtAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        // TxtAmount.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtNarration"].Index;
        TxtNarration.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNarration.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNarration.TabIndex = columnIndex;
    }

    internal bool AddTextToGrid(bool isUpdate)
    {
        if (ObjGlobal.FinanceAgentMandatory && TxtAgent.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtAgent, "AGENT IS MANDATORY..!!");
            return false;
        }

        if (ObjGlobal.FinanceSubLedgerMandatory && TxtSubledger.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtSubledger, "SUBLEDGER IS MANDATORY..!!");
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

        if (Math.Abs(TxtAmount.GetDecimal()) is 0)
        {
            this.NotifyValidationError(TxtAmount, "AMOUNT CANNOT BE ZERO..!!");
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
        DGrid.Rows[iRows].Cells["GTxtSubLedgerId"].Value = _subledgerId.ToString();
        DGrid.Rows[iRows].Cells["GTxtSubLedger"].Value = TxtSubledger.Text;
        DGrid.Rows[iRows].Cells["GTxtDepartmentId"].Value = _departmentId.ToString();
        DGrid.Rows[iRows].Cells["GTxtDepartment"].Value = TxtDepartment.Text;
        DGrid.Rows[iRows].Cells["GTxtAgentId"].Value = _agentId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAgent"].Value = TxtAgent.Text;
        DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = _currencyId;
        DGrid.Rows[iRows].Cells["GTxtCurrency"].Value = TxtCurrency.Text;
        DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = TxtCurrencyRate.Text;
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtLocalReceipt"].Value = TxtCurrencyRate.GetDecimal() > 1
            ? (TxtCurrencyRate.GetDecimal() * TxtAmount.GetDecimal()).GetDecimalString()
            : TxtAmount.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = TxtNarration.Text;
        DGrid.CurrentCell = DGrid.Rows[_isRowUpdate ? iRows : DGrid.RowCount - 1].Cells[_columnIndex];
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

    internal void FillCashBankVoucher(string voucherNo)
    {
        var dsVoucher = _notesMasterRepository.ReturnCashBankVoucherInDataSet(voucherNo);
        if (dsVoucher.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow ro in dsVoucher.Tables[0].Rows)
            {
                if (_actionTag is not ("SAVE" or "COPY")) TxtVno.Text = ro["Voucher_No"].ToString();
                MskDate.Text = ro["Voucher_Date"].ToString();
                MskMiti.Text = ro["Voucher_Miti"].ToString();
                TxtRefVno.Text = ro["Ref_VNo"].ToString();
                MskRefDate.Text = ro["Ref_VDate"].ToString();
                TxtPartyLedger.Text = ro["GLName"].ToString();
                _cbLedgerId = ro["GLId"].GetLong();
                SetCashLedgerInfo(_cbLedgerId);
                _currencyId = ro["Currency_Id"].GetInt();
                TxtCurrency.Text = ro["Ccode"].ToString();
                TxtCurrencyRate.Text = ro["Currency_Rate"].GetDecimalString(true);
                TxtRemarks.Text = ro["Remarks"].ToString();
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

    internal void FillPdcVoucherDetails(string pdcVno)
    {
        if (pdcVno.IsBlankOrEmpty()) return;

        var dt = _notesMasterRepository.ReturnPdcVoucherInDataTable(pdcVno);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow ro in dt.Rows)
            {
                MskDate.Text = ro["CheqDate"].IsValueExits() ? ro["CheqDate"].ToString() : MskDate.Text;
                MskMiti.Text = ro["ChqMiti"].IsValueExits() ? ro["ChqMiti"].ToString() : MskMiti.Text;
                TxtPartyLedger.Text = ro["BankName"].IsValueExits() ? ro["BankName"].ToString() : string.Empty;
                _cbLedgerId = ro["BankLedgerId"].GetLong();
                SetCashLedgerInfo(_cbLedgerId);
                TxtRemarks.Text = ro["Remarks"].ToString();
                TxtRefVno.Text = pdcVno;
                MskRefDate.Text = ro["RefDate"].ToString();
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

    internal void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 && !DGrid.Rows[0].Cells["GTxtLedger"].Value.IsValueExits()) return;
        LblTotalNetAmount.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtReceipt"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalNetAmount.Text = DGrid.Rows.OfType<DataGridViewRow>()
            .Sum(row => row.Cells["GTxtLocalReceipt"].Value.GetDecimal()).GetDecimalString();
    }

    internal void GetSerialNo()
    {
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            DGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
    }

    internal bool IsValidForm()
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

        if (!MskDate.MaskCompleted)
            this.NotifyValidationError(MskDate, "VOUCHER DATE IS INVALID..!!");
        else if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
            this.NotifyValidationError(MskDate,
                $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
        if (!MskMiti.MaskCompleted)
            this.NotifyValidationError(MskMiti, "VOUCHER MITI IS INVALID..!!");
        else if (MskMiti.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
            this.NotifyValidationError(MskMiti,
                $"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
        if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.IsBlankOrEmpty())
            this.NotifyValidationError(DGrid, "VOUCHER DETAILS IS BLANK..!!");

        if (ObjGlobal.FinanceRemarksEnable && TxtRemarks.IsBlankOrEmpty())
            this.NotifyValidationError(TxtRemarks, "VOUCHER REMARKS IS BLANK..!!");

        return true;
    }

    internal int SaveCashBankVoucher()
    {
        if (_actionTag is "SAVE") TxtVno.GetCurrentVoucherNo("DN", _docDesc);
        if (DGrid.RowCount > 0 && DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtLedgerId"].Value.GetLong() is 0)
            DGrid.Rows.RemoveAt(DGrid.RowCount - 1);
        _notesMasterRepository.NMaster.VoucherMode = !_isProvision ? "Contra" : "PROV";
        _notesMasterRepository.NMaster.Voucher_No = TxtVno.Text;
        _notesMasterRepository.NMaster.Voucher_Date = MskDate.Text.GetDateTime();
        _notesMasterRepository.NMaster.Voucher_Miti = MskMiti.Text;
        _notesMasterRepository.NMaster.Voucher_Time = DateTime.Now;
        _notesMasterRepository.NMaster.Ref_VNo = TxtRefVno.Text;
        _notesMasterRepository.NMaster.Ref_VDate = !string.IsNullOrEmpty(TxtRefVno.Text.Trim())
            ? DateTime.Parse(MskRefDate.Text)
            : DateTime.Now;
        _notesMasterRepository.NMaster.VoucherType = "Contra";
        _notesMasterRepository.NMaster.Ledger_Id = _cbLedgerId;
        _notesMasterRepository.NMaster.Currency_Id = _currencyId > 0 ? _currencyId : ObjGlobal.SysCurrencyId;
        _notesMasterRepository.NMaster.Currency_Rate = TxtCurrencyRate.GetDecimal() > 0 ? TxtCurrency.Text.GetDecimal() : 1;
        _notesMasterRepository.NMaster.Cls1 = _departmentId;
        _notesMasterRepository.NMaster.Cls2 = 0;
        _notesMasterRepository.NMaster.Cls3 = 0;
        _notesMasterRepository.NMaster.Cls4 = 0;
        _notesMasterRepository.NMaster.Remarks = TxtRemarks.Text;
        _notesMasterRepository.NMaster.Action_Type = _actionTag;
        _notesMasterRepository.NMaster.SyncRowVersion = (short)(_actionTag is "UPDATE"
            ? ClsMasterSetup.ReturnMaxSyncBaseIdValue("AMS.Notes_Master", "SyncRowVersion", "Voucher_No",
                TxtVno.Text.Trim())
            : 1);
        _notesMasterRepository.NMaster.GetView = DGrid;
        return _notesMasterRepository.SaveCashBankVoucher(_actionTag);
    }

    #endregion --------------- METHOD OF THIS FORM ---------------

    //EVENTS OF THIS FORM

    #region --------------- EVENTS OF THIS FORM ---------------

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
    }

    private void BtnExit_Click(object sender, EventArgs e)
    {
    }

    private void TxtVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
            BtnVno_Click(sender, e);
        else if (TxtVno.ReadOnly)
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtVno, BtnVno);
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        TxtVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "CB");
        if (_actionTag != "SAVE") FillCashBankVoucher(TxtVno.Text);
    }

    private void TxtVno_Validating(object sender, CancelEventArgs e)
    {
        if (TxtVno.IsBlankOrEmpty())
        {
            this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS  BLANK OR INVALID..!!");
        }
        else if (_actionTag.Equals("SAVE"))
        {
            var dtVoucher = _notesMasterRepository.IsCheckVoucherNoExits("AMS.CB_Master", "Voucher_no", TxtVno.Text);
            if (dtVoucher.RowsCount() > 0) this.NotifyValidationError(TxtVno, "VOUCHER NUMBER ALREADY EXITS..!!");
        }
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
        if (!MskMiti.MaskCompleted && MskMiti.Enabled && TxtVno.IsValueExits())
            this.NotifyValidationError(MskMiti, "VOUCHER MITI IS INVALID..!!");
        else if (MskMiti.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
            this.NotifyValidationError(MskMiti,
                $"MITI MUST BE BETWEEN [{ObjGlobal.CfStartBsDate} AND {ObjGlobal.CfEndBsDate}]");
        if (MskMiti.MaskCompleted) MskDate.GetEnglishDate(MskMiti.Text);
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
        if (!MskDate.MaskCompleted && MskDate.Enabled && TxtVno.IsValueExits())
            this.NotifyValidationError(MskDate, "VOUCHER DATE IS INVALID..!!");
        else if (MskDate.MaskCompleted && !MskDate.Text.IsValidDateRange("D"))
            this.NotifyValidationError(MskDate,
                $"DATE MUST BE BETWEEN [{ObjGlobal.CfStartAdDate} AND {ObjGlobal.CfEndAdDate}]");
        if (MskDate.MaskCompleted) MskMiti.GetNepaliDate(MskDate.Text);
    }

    private void TxtRefNo_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtRefVno.IsBlankOrEmpty())
                TxtPartyLedger.Focus();
            else
                GlobalEnter_KeyPress(sender, e);
        }
    }

    private void TxtRefVno_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.F1) return;
        if (_voucherType.Equals("PDC"))
        {
            TxtRefVno.Text = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "PDC");
            if (TxtRefVno.IsValueExits()) FillPdcVoucherDetails(TxtRefVno.Text);
        }
        else if (_voucherType.Equals("PROV"))
        {
            TxtRefVno.Text =
                GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "CB", "PROV");
            if (TxtRefVno.IsValueExits()) FillCashBankVoucher(TxtRefVno.Text);
        }
    }

    private void TxtCashLedger_Validating(object sender, CancelEventArgs e)
    {
        if (_balanceType.Equals("WARNING") && !ObjGlobal.FinanceNegativeWarning && LblCashBalance.GetDecimal() < 0)
            this.NotifyWarning("LEDGER BALANCE IS NEGATIVE");
        if (_balanceType.Equals("BLOCK") && !ObjGlobal.FinanceNegativeWarning && LblCashBalance.GetDecimal() < 0)
            this.NotifyValidationError(TxtPartyLedger, "LEDGER BALANCE IS NEGATIVE");

        if (TxtPartyLedger.Enabled && DGrid.Enabled && TxtPartyLedger.IsBlankOrEmpty())
            this.NotifyValidationError(TxtPartyLedger, "CASH & BANK LEDGER IS BLANK..!!");
    }

    private void TxtCashLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            BtnCashBank_Click(sender, e);
        }
        else if (e.Control && e.KeyCode is Keys.N)
        {
            (TxtPartyLedger.Text, _cbLedgerId) = GetMasterList.CreateGeneralLedger(_isCash ? "CASH" : "BANK", true);
            SetCashLedgerInfo(_cbLedgerId);
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtPartyLedger, BtnPartyLedger);
        }
    }

    private void BtnCashBank_Click(object sender, EventArgs e)
    {
        (TxtPartyLedger.Text, _cbLedgerId) = GetMasterList.GetGeneralLedger(_actionTag, "CASH", MskDate.Text);
        SetCashLedgerInfo(_cbLedgerId);
    }

    private void TxtPartyLedger_KeyPress(object sender, KeyPressEventArgs e)
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
        if (!TxtLedger.Enabled || ActiveControl == TxtAmount) return;

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
            TxtAmount.Focus();
            SendToBack();
            return;
        }

        TxtNarration.Text = e.KeyCode switch
        {
            Keys.F2 => DGrid.Rows.Count > 0
                ? DGrid.Rows[DGrid.CurrentRow.Index - 1].Cells["GTxtNarration"].Value?.ToString()
                : string.Empty,
            Keys.F3 => DGrid.Rows.Count > 0 ? DGrid.Rows[1].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
            Keys.F4 => DGrid.Rows.Count > 0 ? DGrid.Rows[2].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
            Keys.F5 => DGrid.Rows.Count > 0 ? DGrid.Rows[3].Cells["GTxtNarration"].Value?.ToString() : string.Empty,
            Keys.F6 => DGrid.Rows.Count > 0
                ? DGrid.Rows[DGrid.Rows.Count - 1].Cells["GTxtNarration"].Value?.ToString()
                : string.Empty,
            _ => TxtNarration.Text
        };
    }

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
                if (_zoom) Close();
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
                    TxtPartyLedger.Focus();
            }
        }
        else
        {
            MessageBox.Show(@"ERROR OCCURS WHILE LEDGER OPENING..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            BtnSave.Enabled = true;
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
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
        if (e.KeyChar is (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    //DATA GRID CONTROL
    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter || TxtLedger.Enabled) return;
        e.SuppressKeyPress = true;
        EnableGridControl(true);
        AdjustControlsInDataGrid();
        if (DGrid.Rows[_rowIndex].Cells["GTxtLedger"].Value.IsValueExits())
        {
            TextFromGrid();
            TxtLedger.Focus();
            return;
        }

        GetSerialNo();
        TxtLedger.Focus();
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

    #endregion --------------- EVENTS OF THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region -------------- OBJECT FOR THIS FORM --------------

    private int _subledgerId;
    private int _departmentId;
    private int _currencyId = ObjGlobal.SysCurrencyId;
    private int _agentId;
    private int _rowIndex;
    private int _columnIndex;

    private long _ledgerId;
    private long _cbLedgerId = ObjGlobal.FinanceCashLedgerId.GetLong();

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private readonly bool _multiCurrency;
    private readonly bool _zoom;
    private readonly bool _isProvision;
    private bool _isCash;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;
    private string _ledgerType = string.Empty;
    private string _balanceType = string.Empty;
    private readonly string _voucherType;
    private readonly string _zoomVno;

    private KeyPressEventArgs _getKeys;

    // private readonly IFinanceEntry _entry = new ClsFinanceEntry();
    private readonly INotesMasterRepository _notesMasterRepository;
    private readonly IMasterSetup _setup = new ClsMasterSetup();
    private readonly IFinanceDesign _voucher = new FinanceDesign();
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtLedger { get; set; }
    private MrGridTextBox TxtSubledger { get; set; }
    private MrGridTextBox TxtAgent { get; set; }
    private MrGridTextBox TxtItemDepartment { get; set; }
    private MrGridNumericTextBox TxtAmount { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }

    #endregion -------------- OBJECT FOR THIS FORM --------------
}