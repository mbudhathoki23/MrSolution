using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.OpeningMaster;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using MrBLL.DataEntry.Common;
using MrBLL.Master.LedgerSetup;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.DataEntry.OpeningMaster;

public partial class FrmLedgerOpeningEntry : MrForm
{
    #region --------------- LEDGER OPENING FORM ---------------

    public FrmLedgerOpeningEntry(bool isZoom = false, string voucherNo = "")
    {
        InitializeComponent();
        _ledgerOpeningRepository = new LedgerOpeningRepository();

        _setup = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        GetGridColumns();
        AdjustControlsInDataGrid();
        EnableControl();
        ClearControl();
    }

    private void FrmLedgerOpeningEntry_Shown(object sender, EventArgs e)
    {
        BtnNew.Focus();
        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    private void FrmLedgerOpeningEntry_KeyPress(object sender, KeyPressEventArgs e)
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

    private void FrmLedgerOpeningEntry_Load(object sender, EventArgs e)
    {

        ObjGlobal.GetFormAccessControl([BtnNew, BtnEdit, BtnDelete], this.Tag);
    }

    #endregion --------------- LEDGER OPENING FORM ---------------

    //METHOD FOR THIS FORM

    #region --------------- METHOD OF THIS FORM ---------------

    private void ReturnVoucherNo()
    {
        var dt = _setup.IsExitsCheckDocumentNumbering("COA");
        if (dt?.Rows.Count is 1)
        {
            _docDesc = dt.Rows[0]["DocDesc"].ToString();
            TxtVno.Text = TxtVno.GetCurrentVoucherNo("COA", _docDesc);
        }
        else if (dt?.Rows.Count > 1)
        {
            using var wnd = new FrmNumberingScheme("COA");
            if (wnd.ShowDialog() != DialogResult.OK) return;
            if (string.IsNullOrEmpty(wnd.VNo)) return;
            _docDesc = wnd.Description;
            TxtVno.Text = wnd.VNo;
        }
    }

    private void EnableControl(bool isEnable = false)
    {
        BtnNew.Enabled = BtnEdit.Enabled = BtnDelete.Enabled = !isEnable;
        TxtVno.Enabled = BtnVno.Enabled = _tagStrings.Contains(_actionTag) || isEnable;
        MskDate.Enabled = MskMiti.Enabled = false;
        TxtCurrency.Enabled = BtnCurrency.Enabled = isEnable && ObjGlobal.FinanceCurrencyEnable;
        TxtCurrencyRate.Enabled = TxtCurrency.Enabled;
        TxtRefNo.Enabled = MskRefDate.Enabled = isEnable;
        TabLedgerOpening.Enabled = !_tagStrings.Contains(_actionTag) && isEnable;
        TxtDepartment.Enabled = BtnDepartment.Enabled = isEnable && ObjGlobal.FinanceDepartmentEnable;
        TxtRemarks.Enabled = btnRemarks.Enabled = isEnable && ObjGlobal.FinanceRemarksEnable;
        EnableGridControl();
    }

    private void ClearControl()
    {
        Text = _actionTag.IsBlankOrEmpty()
            ? "LEDGER OPENING BALANCE ENTRY"
            : $"LEDGER OPENING BALANCE ENTRY [{_actionTag}]";
        TxtVno.Clear();
        TxtVno.Text = TxtVno.GetCurrentVoucherNo("COA", _docDesc);
        TxtVno.ReadOnly = !_actionTag.Equals("SAVE");
        MskDate.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetDateString();
        MskMiti.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetNepaliDate();
        MskRefDate.Clear();
        TxtRefNo.Clear();
        TxtRemarks.Clear();
        DGrid.ReadOnly = true;
        DGrid.ClearSelection();
        DGrid.Rows.Clear();
        ClearLedgerDetails();
        DGrid.ClearSelection();
    }

    private void ClearLedgerDetails()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            TxtSno.Text = DGrid.RowCount.ToString();
        }

        isRowUpdate = false;
        LedgerId = 0;
        TxtLedger.Clear();
        SubledgerId = 0;
        TxtSubledger.Clear();
        AgentId = 0;
        TxtAgent.Clear();
        DepartmentId = 0;
        TxtDepartment.Clear();
        CurrencyId = ObjGlobal.SysCurrencyId;
        TxtCurrency.Text = ObjGlobal.SysCurrency;
        TxtCurrencyRate.Text = 1.GetDecimalString();
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
        TxtSubledger.Enabled = TxtSubledger.Visible = isEnable && ObjGlobal.FinanceSubLedgerEnable;
        TxtItemDepartment.Enabled = TxtItemDepartment.Visible = isEnable && ObjGlobal.FinanceDepartmentItemEnable;
        TxtAgent.Enabled = TxtAgent.Visible = isEnable && ObjGlobal.FinanceAgentEnable;
        TxtDebit.Enabled = TxtDebit.Visible = isEnable;
        TxtCredit.Enabled = TxtCredit.Visible = isEnable;
        TxtNarration.Enabled = TxtNarration.Visible = isEnable;
        //DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }

    private void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0) return;
        LblPanNo.Text = dtLedger.Rows[0]["PanNo"].ToString();
        LblBalance.Text = dtLedger.Rows[0]["Balance"].ToString();
    }

    private void OpenLedger()
    {
        var (description, id) = GetMasterList.GetOpeningGeneralLedger(_actionTag, MskDate.Text);
        if (id > 0)
        {
            TxtLedger.Text = description;
            LedgerId = id;
        }
        TxtLedger.Focus();
    }

    private void OpenSubLedger()
    {
        (TxtSubledger.Text, SubledgerId) = GetMasterList.GetSubLedgerList(_actionTag);
    }

    private void OpenAgent()
    {
        (TxtAgent.Text, AgentId) = GetMasterList.GetSubLedgerList(_actionTag);
        TxtAgent.Focus();
    }

    private void OpenDepartment()
    {
        (TxtDepartment.Text, DepartmentId) = GetMasterList.GetDepartmentList(_actionTag);
        TxtDepartment.Focus();
    }

    private void OpenCurrency()
    {
        (TxtCurrency.Text, AgentId, TxtCurrencyRate.Text) = GetMasterList.GetCurrencyList(_actionTag);
        TxtCurrency.Focus();
    }

    private void GetGridColumns()
    {
        _voucher.GetJournalVoucherDesign(DGrid, "S");
        if (DGrid.ColumnCount > 0)
        {
            DGrid.Columns["GTxtSubledger"].Visible = ObjGlobal.FinanceSubLedgerEnable;
            if (DGrid.Columns["GTxtSubledger"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtSubledger"].Width;
            DGrid.Columns["GTxtAgent"].Visible = ObjGlobal.FinanceAgentEnable;
            if (DGrid.Columns["GTxtAgent"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtAgent"].Width;
            DGrid.Columns["GTxtDepartment"].Visible = ObjGlobal.FinanceDepartmentItemEnable;
            if (DGrid.Columns["GTxtDepartment"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtDepartment"].Width;
            DGrid.Columns["GTxtCurrency"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtCurrency"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtCurrency"].Width;
            DGrid.Columns["GTxtExchangeRate"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtExchangeRate"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtExchangeRate"].Width;
            DGrid.Columns["GTxtLocalDebit"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtLocalDebit"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtLocalDebit"].Width;

            DGrid.Columns["GTxtLocalCredit"].Visible = ObjGlobal.FinanceCurrencyEnable;
            if (DGrid.Columns["GTxtLocalCredit"].Visible)
                DGrid.Columns["GTxtLedger"].Width -= DGrid.Columns["GTxtLocalCredit"].Width;
        }

        DGrid.RowEnter += (_, e) => _rowIndex = e.RowIndex;
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
            else if (e.KeyCode is Keys.F2)
            {
                var (description, id) = GetMasterList.GetOpeningGeneralLedger(_actionTag, MskDate.Text);
                if (id > 0)
                {
                    TxtLedger.Text = description;
                    LedgerId = id;
                    SetLedgerInfo(LedgerId);
                }
                TxtLedger.Focus();
            }
            else if (e.Shift && e.KeyCode is Keys.Tab)
            {
                SendToBack();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                (TxtLedger.Text, LedgerId) = GetMasterList.CreateGeneralLedger("Other", true);
                SetLedgerInfo(LedgerId);
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
                (TxtSubledger.Text, SubledgerId) = GetMasterList.CreateSubLedger(true);
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
                (TxtAgent.Text, AgentId) = GetMasterList.CreateAgent(true);
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
                (TxtDepartment.Text, DepartmentId) = GetMasterList.CreateDepartment(true);
            else
                ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                    e.KeyCode.ToString(), TxtDepartment, OpenDepartment);
        };
        TxtDebit = new MrGridNumericTextBox(DGrid);
        TxtDebit.Validating += (_, _) =>
        {
            TxtDebit.Text = TxtDebit.GetDecimalString();
            TxtCredit.Text = TxtDebit.GetDecimal() > 0 ? 0.GetDecimalString() : TxtCredit.GetDecimalString();
        };
        TxtCredit = new MrGridNumericTextBox(DGrid);
        TxtCredit.Validating += (_, _) =>
        {
            TxtCredit.Text = TxtCredit.GetDecimalString();
            TxtDebit.Text = TxtCredit.GetDecimal() > 0 ? 0.GetDecimalString() : TxtDebit.GetDecimalString();
        };
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
        LedgerId = DGrid.Rows[_rowIndex].Cells["GTxtLedgerId"].Value.GetLong();
        SetLedgerInfo(LedgerId);
        TxtSubledger.Text = DGrid.Rows[_rowIndex].Cells["GTxtSubLedger"].Value.GetString();
        SubledgerId = DGrid.Rows[_rowIndex].Cells["GTxtSubledgerId"].Value.GetInt();
        TxtAgent.Text = DGrid.Rows[_rowIndex].Cells["GTxtAgent"].Value.GetString();
        SubledgerId = DGrid.Rows[_rowIndex].Cells["GTxtAgentId"].Value.GetInt();
        TxtDepartment.Text = DGrid.Rows[_rowIndex].Cells["GTxtDepartment"].Value.GetString();
        SubledgerId = DGrid.Rows[_rowIndex].Cells["GTxtDepartmentId"].Value.GetInt();
        TxtCurrency.Text = DGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.GetString();
        TxtCurrencyRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtExchangeRate"].Value.GetString();
        CurrencyId = DGrid.Rows[_rowIndex].Cells["GTxtCurrencyId"].Value.GetInt();
        TxtDebit.Text = DGrid.Rows[_rowIndex].Cells["GTxtDebit"].Value.GetDecimalString();
        TxtCredit.Text = DGrid.Rows[_rowIndex].Cells["GTxtCredit"].Value.GetDecimalString();
        TxtNarration.Text = DGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        isRowUpdate = true;
    }

    private void AdjustControlsInDataGrid()
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

        if (Math.Abs(TxtCredit.GetDecimal() - TxtDebit.GetDecimal()) is 0)
        {
            this.NotifyValidationError(TxtDebit, "DEBIT & CREDIT AMOUNT CANNOT BE ZERO..!!");
            return false;
        }

        var iRows = _rowIndex;
        if (!isUpdate)
        {
            DGrid.Rows.Add();
            DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
        }

        DGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = LedgerId.ToString();
        DGrid.Rows[iRows].Cells["GTxtLedger"].Value = TxtLedger.Text;
        DGrid.Rows[iRows].Cells["GTxtSubLedgerId"].Value = SubledgerId.ToString();
        DGrid.Rows[iRows].Cells["GTxtSubLedger"].Value = TxtSubledger.Text;
        DGrid.Rows[iRows].Cells["GTxtDepartmentId"].Value = DepartmentId.ToString();
        DGrid.Rows[iRows].Cells["GTxtDepartment"].Value = TxtDepartment.Text;
        DGrid.Rows[iRows].Cells["GTxtAgentId"].Value = AgentId.ToString();
        DGrid.Rows[iRows].Cells["GTxtAgent"].Value = TxtAgent.Text;
        DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = CurrencyId;
        DGrid.Rows[iRows].Cells["GTxtCurrency"].Value = TxtCurrency.Text;
        DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = TxtCurrencyRate.Text;
        DGrid.Rows[iRows].Cells["GTxtDebit"].Value = TxtDebit.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtLocalDebit"].Value = TxtCurrencyRate.GetDecimal() > 1
            ? (TxtCurrencyRate.GetDecimal() * TxtDebit.GetDecimal()).GetDecimalString()
            : TxtDebit.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtCredit"].Value = TxtCredit.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtLocalCredit"].Value = TxtCurrencyRate.GetDecimal() > 1
            ? (TxtCurrencyRate.GetDecimal() * TxtCredit.GetDecimal()).GetDecimalString()
            : TxtCredit.GetDecimalString();
        DGrid.Rows[iRows].Cells["GTxtNarration"].Value = TxtNarration.Text;
        DGrid.CurrentCell = DGrid.Rows[isRowUpdate ? iRows : DGrid.RowCount - 1].Cells[_columnIndex];
        if (isRowUpdate)
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

    private void FillLedgerOpeningVoucher(string voucherNo)
    {
        var dsVoucher = _ledgerOpeningRepository.ReturnOpeningLedgerVoucherInDataSet(voucherNo);
        if (dsVoucher.Tables[0].Rows.Count > 0)
        {
            if (_actionTag != "SAVE")
            {
                TxtVno.Text = dsVoucher.Tables[0].Rows[0]["Voucher_No"].ToString();
            }
            _openingId = dsVoucher.Tables[0].Rows[0]["Opening_Id"].GetInt();
            MskDate.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetDateString();
            MskMiti.Text = ObjGlobal.CfStartAdDate.AddDays(-1).GetNepaliDate();
            TxtCurrency.Text = dsVoucher.Tables[0].Rows[0]["CCode"].ToString();
            TxtCurrencyRate.Text = dsVoucher.Tables[0].Rows[0]["Currency_Rate"].GetDecimalString();
            TxtRemarks.Text = dsVoucher.Tables[0].Rows[0]["Remarks"].ToString();
            if (!string.IsNullOrEmpty(dsVoucher.Tables[0].Rows[0]["Cls1"].ToString()))
            {
                TxtDepartment.Text = dsVoucher.Tables[0].Rows[0]["Department1"].ToString();
                DepartmentId = ObjGlobal.ReturnInt(dsVoucher.Tables[0].Rows[0]["Cls1"].ToString());
            }

            if (dsVoucher.Tables[1].Rows.Count > 0)
            {
                var iRows = 0;
                DGrid.Rows.Clear();
                DGrid.Rows.Add(dsVoucher.Tables[1].Rows.Count + 1);
                foreach (DataRow ro in dsVoucher.Tables[1].Rows)
                {
                    DGrid.Rows[iRows].Cells["GTxtSNo"].Value = iRows + 1;
                    DGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = ro["GlId"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtLedger"].Value = ro["GLName"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtSubledgerId"].Value = ro["Subledger_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtSubLedger"].Value = ro["SLName"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtDepartmentId"].Value = ro["Cls1"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtDepartment"].Value = ro["Department1"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtAgentId"].Value = ro["Agent_ID"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtAgent"].Value = ro["AgentName"].ToString();

                    DGrid.Rows[iRows].Cells["GTxtCurrencyId"].Value = ro["Currency_Id"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtCurrency"].Value = ro["Ccode"].ToString();
                    DGrid.Rows[iRows].Cells["GTxtExchangeRate"].Value = ro["Currency_Rate"].GetDecimalString();

                    DGrid.Rows[iRows].Cells["GTxtDebit"].Value = ro["Debit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtLocalDebit"].Value = ro["LocalDebit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtCredit"].Value = ro["Credit"].GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtLocalCredit"].Value = ro["LocalCredit"].GetDecimalString();

                    DGrid.Rows[iRows].Cells["GTxtNarration"].Value = ro["Narration"].ToString();
                    iRows++;
                }
            }
        }

        ClearLedgerDetails();
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.ClearSelection();
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() <= 0) return;
        var gridViewRows = DGrid.Rows.OfType<DataGridViewRow>();
        var rows = gridViewRows as DataGridViewRow[] ?? gridViewRows.ToArray();

        LblTotalDebit.Text = rows.Sum(row => row.Cells["GTxtDebit"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalDebit.Text = rows.Sum(row => row.Cells["GTxtLocalDebit"].Value.GetDecimal()).GetDecimalString();
        LblTotalCredit.Text = rows.Sum(row => row.Cells["GTxtCredit"].Value.GetDecimal()).GetDecimalString();
        LblTotalLocalCredit.Text = rows.Sum(row => row.Cells["GTxtLocalCredit"].Value.GetDecimal()).GetDecimalString();

        lbl_NoInWordsDetl.Text = ClsMoneyConversion.MoneyConversion(LblTotalCredit.Text);
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
        if (_actionTag.IsBlankOrEmpty())
        {
            return false;
        }
        if (_actionTag != "SAVE")
        {
            if (CustomMessageBox.VoucherNoAction(_actionTag, TxtVno.Text) is DialogResult.No)
            {
                return false;
            }
        }
        if (_actionTag == "DELETE")
        {
            return true;
        }
        if (TxtVno.IsBlankOrEmpty())
        {
            if (!TxtVno.Enabled) TxtVno.Enabled = true;
            this.NotifyValidationError(TxtVno, "VOUCHER NUMBER IS BLANK..!!");
        }

        if (DGrid.RowCount == 0 || DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.IsBlankOrEmpty())
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

    private int SaveLedgerOpening()
    {
        try
        {
            const int syncRow = 0;
            _ledgerOpeningRepository.GetOpening.Details = [];

            if (_actionTag == "SAVE")
            {
                TxtVno.Text = TxtVno.GetCurrentVoucherNo("COA", _docDesc);
                _openingId = _ledgerOpeningRepository.GetOpening.Opening_Id.ReturnMaxIntId("COA", "Opening_Id");
            }
            _ledgerOpeningRepository.GetOpening.Opening_Id = _openingId;
            _ledgerOpeningRepository.GetOpening.Voucher_No = TxtVno.Text;
            _ledgerOpeningRepository.GetOpening.Module = "LOB";
            _ledgerOpeningRepository.GetOpening.OP_Date = DateTime.Parse(MskDate.Text);
            _ledgerOpeningRepository.GetOpening.OP_Miti = MskMiti.Text.Contains("/") ? MskMiti.Text : MskMiti.Text.Replace("-", "/");
            _ledgerOpeningRepository.GetOpening.Remarks = TxtRemarks.Text;
            _ledgerOpeningRepository.GetOpening.Ledger_Id = LedgerId;
            _ledgerOpeningRepository.GetOpening.Agent_Id = AgentId > 0 ? AgentId : null;
            _ledgerOpeningRepository.GetOpening.Cls1 = DepartmentId > 0 ? DepartmentId : null;
            _ledgerOpeningRepository.GetOpening.Branch_Id = ObjGlobal.SysBranchId;
            _ledgerOpeningRepository.GetOpening.Company_Id = ObjGlobal.SysCompanyUnitId;
            _ledgerOpeningRepository.GetOpening.Currency_Id = ObjGlobal.SysCurrencyId;
            _ledgerOpeningRepository.GetOpening.Enter_By = ObjGlobal.LogInUser;
            _ledgerOpeningRepository.GetOpening.Enter_Date = DateTime.Now;
            _ledgerOpeningRepository.GetOpening.FiscalYearId = ObjGlobal.SysFiscalYearId;

            _ledgerOpeningRepository.GetOpening.GetView = DGrid;

            var sync = syncRow.ReturnSyncRowNo("COA", _openingId.ToString());

            foreach (DataGridViewRow item in DGrid.Rows)
            {
                var result = item.Cells["GTxtLedgerId"].Value.GetLong();
                if (result == 0)
                {
                    continue;
                }

                var details = new LedgerOpening()
                {
                    Opening_Id = _openingId,
                    Module = "LOB",
                    Serial_No = item.Cells["GTxtSNo"].Value.GetInt(),
                    Voucher_No = TxtVno.Text.Trim(),
                    OP_Date = MskDate.Text.GetDateTime(),
                    OP_Miti = MskMiti.Text.Trim(),
                    Ledger_Id = item.Cells["GTxtLedgerId"].Value.GetLong(),
                    Subledger_Id = item.Cells["GTxtSubledgerId"].Value.GetInt(),
                    Agent_Id = item.Cells["GTxtAgentId"].Value.GetInt(),
                    Cls1 = DepartmentId > 0 ? DepartmentId : null,
                    Cls2 = 0,
                    Cls3 = 0,
                    Cls4 = 0,
                    Currency_Id = item.Cells["GTxtCurrencyId"].Value.GetInt(),
                    Currency_Rate = item.Cells["GTxtExchangeRate"].Value.GetDecimal(true),
                    Debit = item.Cells["GTxtDebit"].Value.GetDecimal(),
                    LocalDebit = item.Cells["GTxtLocalDebit"].Value.GetDecimal(),
                    Credit = item.Cells["GTxtCredit"].Value.GetDecimal(),
                    LocalCredit = item.Cells["GTxtLocalCredit"].Value.GetDecimal(),
                    Narration = item.Cells["GTxtNarration"].Value.GetTrimReplace(),
                    Remarks = lbl_Remarks.Text.Trim(),
                    Enter_By = ObjGlobal.LogInUser,
                    Enter_Date = DateTime.Now,
                    Reconcile_By = ObjGlobal.LogInUser,
                    Reconcile_Date = DateTime.Now,
                    Branch_Id = ObjGlobal.SysBranchId,
                    Company_Id = ObjGlobal.SysCompanyUnitId,
                    FiscalYearId = ObjGlobal.SysFiscalYearId,
                    IsReverse = false,
                    CancelRemarks = "",
                    CancelDate = DateTime.Now,
                    SyncBaseId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncGlobalId = ObjGlobal.IsOnlineSync ? Guid.NewGuid() : Guid.Empty,
                    SyncOriginId = ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits()
                        ? ObjGlobal.LocalOriginId.GetValueOrDefault()
                        : Guid.Empty,
                    SyncCreatedOn = DateTime.Now,
                    SyncLastPatchedOn = DateTime.Now,
                    SyncRowVersion = sync
                };
                _ledgerOpeningRepository.Details.Add(details);
            }

            _ledgerOpeningRepository.GetOpening.SyncRowVersion = sync;

            return _ledgerOpeningRepository.SaveLedgerOpening(_actionTag);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return 0;
        }
    }

    private async void GetAndSaveUnSynchronizedLedgerOpening()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return;
        }
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}LedgerOpening/GetLedgerOpeningsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}LedgerOpening/InsertLedgerOpeningList",
            UpdateUrl = @$"{_configParams.Model.Item2}LedgerOpening/UpdateLedgerOpening"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var ledgerOpeningRepo = DataSyncProviderFactory.GetRepository<LedgerOpening>(_injectData);

        SplashScreenManager.ShowForm(typeof(PleaseWait));
        //pull all new ledger opening
        var pullResponse =
            await _ledgerOpeningRepository.PullAccountGroupsServerToClientByRowCounts(ledgerOpeningRepo, 1);

        SplashScreenManager.CloseForm();
        // push all new ledger opening data
        var sqlQuery = _ledgerOpeningRepository.GetLedgerOpeningScript();
        var queryResponse = await QueryUtils.GetListAsync<LedgerOpening>(sqlQuery);
        var loList = queryResponse.List.ToList();
        if (loList.Count > 0)
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var pushResponse = await ledgerOpeningRepo.PushNewListAsync(loList);
            SplashScreenManager.CloseForm();

        }
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
        TxtRefNo.Focus();
    }

    private void BtnReverse_Click(object sender, EventArgs e)
    {
    }

    private void BtnCopy_Click(object sender, EventArgs e)
    {
    }

    private void BtnPrint_Click(object sender, EventArgs e)
    {
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
            BtnVno_Click(sender, e);
        else if (TxtVno.ReadOnly)
            ClsKeyPreview.KeyEvent(e, "DELETE",
                TxtVno, BtnVno);
    }

    private void BtnVno_Click(object sender, EventArgs e)
    {
        var voucherNo = GetTransactionList.GetTransactionVoucherNo(_actionTag, MskDate.Text, "MED", "LOB");
        if (_actionTag != "SAVE")
        {
            ClearControl();
            TxtVno.Text = voucherNo;
            FillLedgerOpeningVoucher(TxtVno.Text);
        }

        //TxtVno.Focus();
    }

    private void MskMiti_Validating(object sender, CancelEventArgs e)
    {
    }

    private void MskDate_Validating(object sender, CancelEventArgs e)
    {
    }

    private void TxtRefNo_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (TxtRefNo.IsValueExits())
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
            else
            {
                if (MskRefDate.Enabled)
                {
                    MskRefDate.Focus();
                }
                else if (TxtCurrency.Enabled)
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
            CurrencyId = frm.CurrencyId;
        }
        else
        {
            ClsKeyPreview.KeyEvent((char)e.KeyCode, e.KeyValue, e.KeyData.ToString(), "DELETE",
                e.KeyCode.ToString(), TxtCurrency, OpenCurrency);
        }
        GlobalEnter_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
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
        if (!AddTextToGrid(isRowUpdate)) return;
        TxtLedger.Focus();
    }

    private void TxtNarration_Validated(object sender, EventArgs e)
    {
        //if (ActiveControl.Name == "TxtCredit" || !TxtLedger.Enabled)
        //{
        //    return;
        //}
        //if (!AddTextToGrid(isRowUpdate)) return;
        //TxtLedger.Focus();
        //return;
    }

    private void TxtNarration_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode == Keys.Tab)
        {
            TxtCredit.Focus();
            SendToBack();
        }
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
            if (SaveLedgerOpening() > 0)
            {
                this.NotifySuccess($@"{TxtVno.Text} VOUCHER NUMBER {_actionTag} SUCCESSFULLY..!!");
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
                    DGrid.Focus();
            }
        }
        else
        {
            this.NotifyError($@"ERROR OCCURRED WHILE LEDGER OPENING..!!");
            BtnSave.Enabled = true;
        }
    }

    private void BtnSync_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(GetAndSaveUnSynchronizedLedgerOpening);
        }
        else
        {
            CustomMessageBox.Warning("PLEASE CONFIG YOUR CLOUD BACK UP SYNC..!!");
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        ClearControl();
        Close();
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

    private void MskRefDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter)
        {
            if (MskRefDate.MaskCompleted)
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
                DGrid.Focus();
            }
        }
    }
    //DATA GRID CONTROL
    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Delete && !TxtLedger.Enabled)
        {
            DGrid.Rows.RemoveAt(_rowIndex);
            if (DGrid.RowCount is 0) DGrid.Rows.Add();

            GetSerialNo();
        }

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
        IsRowDelete = true;
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        if (!IsRowDelete) return;
        if (DGrid.RowCount is 0) DGrid.Rows.Add();
        GetSerialNo();
    }

    #endregion --------------- EVENTS OF THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region -------------- OBJECT FOR THIS FORM --------------

    private int SubledgerId;
    private int DepartmentId;
    private int CurrencyId = ObjGlobal.SysCurrencyId;
    private int AgentId;
    private int _openingId;
    private int _rowIndex;
    private int _columnIndex;
    private bool IsRowDelete;
    private bool isRowUpdate;
    private long LedgerId;
    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;
    private KeyPressEventArgs _getKeys;

    private readonly ILedgerOpeningRepository _ledgerOpeningRepository;
    private readonly IMasterSetup _setup = new ClsMasterSetup();
    private readonly IFinanceDesign _voucher = new FinanceDesign();

    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtLedger { get; set; }
    private MrGridTextBox TxtSubledger { get; set; }
    private MrGridTextBox TxtAgent { get; set; }
    private MrGridTextBox TxtItemDepartment { get; set; }
    private MrGridNumericTextBox TxtDebit { get; set; }
    private MrGridNumericTextBox TxtCredit { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;


    #endregion -------------- OBJECT FOR THIS FORM --------------


}