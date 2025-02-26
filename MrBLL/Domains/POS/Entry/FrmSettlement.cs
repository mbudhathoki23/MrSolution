using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry;

public partial class FrmSettlement : MrForm
{
    public FrmSettlement(string basicAmount)
    {
        InitializeComponent();
        InitializeGridControl();
        ClearControl();
        EnableGridControl();
        BasicAmount = basicAmount.GetDecimal();
        LblBasicAmount.Text = basicAmount.GetDecimalString();
        DGrid.ReadOnly = true;
        DGrid.KeyDown += DGridOnKeyDown;
        DGrid.RowEnter += DGrid_RowEnter;
        DGrid.CellEnter += DGrid_CellEnter;
    }

    private void FrmSettlement_Load(object sender, EventArgs e)
    {
    }

    private void FrmSettlement_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CmbPaymentMode.Enabled)
            {
                EnableGridControl();
                DGrid.Focus();
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
                {
                    Close();
                    return;
                }
            }
        }
    }

    private void OnTxtAmountOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.Enter)
        {
            if (_ledgerId > 0 && TxtAmount.GetDecimal() > 0)
            {
            }
        }
    }

    private void TxtAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (TxtLedger.Enabled)
        {
            AddTextToGridControl(IsRowUpdate);
        }
    }

    private void OnTxtLedgerOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.F1)
        {
            OpenLedgerList();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtLedger, OpenLedgerList);
        }
    }

    private void TxtLedger_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_ledgerId is 0 && DGrid.RowCount > 0)
        {
            if (DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() is 0)
            {
                TxtLedger.Focus();
                return;
            }
            else
            {
                BtnSave.Focus();
            }
        }
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        _colIndex = e.ColumnIndex;
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void DGridOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyData is Keys.Enter && !CmbPaymentMode.Enabled)
        {
            if (DGrid.CurrentRow != null && DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtLedgerId"].Value.GetLong() is 0)
            {
            }
            else
            {
                GetTextFromGridToControl();
            }
            EnableGridControl(true);
            IndexingGridControl();
            CmbPaymentMode.Focus();
        }
        else if (e.KeyCode is Keys.Delete)
        {
            if (DGrid.CurrentRow != null)
            {
                if (DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtLedgerId"].Value.GetLong() is 0)
                {
                    return;
                }
                DGrid.Rows.RemoveAt(DGrid.CurrentRow.Index.GetInt());
                CalculateTotalAmount();
            }
            if (DGrid.RowCount is 0)
            {
                DGrid.Rows.Add();
            }
        }
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (DGrid.RowCount > 0 && DGrid.Rows[0].Cells["GTxtLedgerId"].Value.GetLong() > 0)
        {
            if (DGrid.RowCount > 0 && DGrid.Rows[DGrid.RowCount - 1].Cells["GTxtLedgerId"].Value.GetLong() is 0)
            {
                DGrid.Rows.RemoveAt(DGrid.RowCount - 1);
            }
            TotalReceived = LblTotalAmount.Text.GetDecimal();
            SettleView = DGrid;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
        return;
    }

    // METHOD FOR PARTIAL PAYMENT
    private void InitializeGridControl()
    {
        CmbPaymentMode = new MrGridComboBox(DGrid);
        _master.BindPaymentType(CmbPaymentMode);
        CmbPaymentMode.SelectedIndexChanged += CmbPaymentMode_SelectedIndexChanged;
        CmbPaymentMode.Validating += CmbPaymentMode_Validating;
        TxtLedger = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtLedger.KeyDown += OnTxtLedgerOnKeyDown;
        TxtLedger.Validating += TxtLedger_Validating;
        TxtAmount = new MrGridNumericTextBox(DGrid);
        TxtAmount.Validating += TxtAmount_Validating;
        TxtAmount.KeyDown += OnTxtAmountOnKeyDown;
    }

    private void CmbPaymentMode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (CmbPaymentMode.SelectedValue.GetString() is "GIFT")
        {
            var result = GetMasterList.GetAccountGroupList("SAVE");
        }
    }

    private void CmbPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        var result = CmbPaymentMode.Text.ToUpper();
        var userLedgerId = _master.GetUserLedgerIdFromUser(ObjGlobal.LogInUser);
        TxtLedger.ReadOnly = result.Equals("CASH");
        _ledgerId = result switch
        {
            "CASH" => userLedgerId > 0 ? userLedgerId : ObjGlobal.FinanceCashLedgerId,
            "CARD" => ObjGlobal.FinanceCardLedgerId.GetLong(),
            "BANK" => ObjGlobal.FinanceBankLedgerId.GetLong(),
            "PHONE PAY" => ObjGlobal.FinanceFonePayLedgerId.GetLong(),
            "E-SEWA" => ObjGlobal.FinanceKhaltiLedgerId.GetLong(),
            "KHALTI" => ObjGlobal.FinanceKhaltiLedgerId.GetLong(),
            "REMIT" => ObjGlobal.FinanceRemitLedgerId.GetLong(),
            "CONNECTIPS" => ObjGlobal.FinanceConnectIpsLedgerId.GetLong(),
            "PARTIAL" => ObjGlobal.FinancePartialLedgerId.GetLong(),
            "GIFT VOUCHER" => ObjGlobal.FinanaceGiftVoucherLedgerId.GetLong(),
            _ => 0
        };
        TxtLedger.Text = _master.GetLedgerDescription(_ledgerId);
        TxtLedger.Enabled = CmbPaymentMode.SelectedValue is not ("PARTIAL" or "GIFT");
    }

    private void EnableGridControl(bool isEnable = false)
    {
        CmbPaymentMode.Enabled = CmbPaymentMode.Visible = isEnable;
        TxtLedger.Enabled = TxtLedger.Visible = isEnable;
        TxtAmount.Enabled = TxtAmount.Visible = isEnable;
    }

    private void AddTextToGridControl(bool isUpdate)
    {
        if (TxtLedger.IsBlankOrEmpty() && _ledgerId is 0)
        {
            CustomMessageBox.Warning("PLEASE SELECT THE AMOUNT RECEIPT LEDGER..!!");
            TxtLedger.Focus();
            return;
        }
        if (CmbPaymentMode.Text.IsBlankOrEmpty())
        {
            CustomMessageBox.Warning("PLEASE SELECT THE PAYMENT MODE..!!");
            CmbPaymentMode.Focus();
            return;
        }
        if (TxtAmount.GetDecimal() is 0)
        {
            CustomMessageBox.Warning("PLEASE ENTER THE AMOUNT..!!");
            TxtAmount.Focus();
            return;
        }

        var amount = LblTotalAmount.Text.GetDecimal() + TxtAmount.GetDecimal();
        var invoiceAmount = LblBasicAmount.Text.GetDecimal();
        if (amount > invoiceAmount)
        {
            CustomMessageBox.Warning("ENTER THE AMOUNT SUM IS GREATER THAN INVOICE AMOUNT..!!");
            TxtAmount.Focus();
            return;
        }
        if (isUpdate)
        {
            if (DGrid.CurrentRow != null)
            {
                _rowIndex = DGrid.CurrentRow.Index;
            }
        }
        else
        {
            DGrid.Rows.Add();
        }
        DGrid.Rows[_rowIndex].Cells["GTxtPaymentMode"].Value = CmbPaymentMode.SelectedValue.GetString();
        DGrid.Rows[_rowIndex].Cells["GTxtLedgerId"].Value = _ledgerId;
        DGrid.Rows[_rowIndex].Cells["GTxtLedger"].Value = TxtLedger.Text;
        DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value = TxtAmount.GetDecimalString();
        var index = isUpdate ? _rowIndex + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[index].Cells[0];
        ClearControl();
        CalculateTotalAmount();
        CmbPaymentMode.Focus();
    }

    private void CalculateTotalAmount()
    {
        LblTotalAmount.Text = DGrid.Rows.OfType<DataGridViewRow>().Sum(row => row.Cells["GTxtAmount"].Value.GetDecimal()).GetDecimalString();
        TotalReceived = LblTotalAmount.GetDecimal();
        if (TotalReceived > BasicAmount)
        {
            CustomMessageBox.Warning("RECEIVED AMOUNT CAN'T BE GREATER THAN INVOICE AMOUNT");
        }
    }

    private void GetTextFromGridToControl()
    {
        if (DGrid.CurrentRow != null)
        {
            CmbPaymentMode.SelectedIndex = CmbPaymentMode.FindString(DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtPaymentMode"].Value.GetString());
            _ledgerId = DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtLedgerId"].Value.GetLong();
            TxtLedger.Text = DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtLedger"].Value.GetString();
            TxtAmount.Text = DGrid.Rows[DGrid.CurrentRow.Index].Cells["GTxtAmount"].Value.GetDecimalString();
        }
        IsRowUpdate = true;
        CmbPaymentMode.Focus();
    }

    private void ClearControl()
    {
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
        }
        CmbPaymentMode.SelectedIndex = 0;
        _ledgerId = 0;
        TxtLedger.Clear();
        TxtAmount.Clear();
        IsRowUpdate = false;
        IndexingGridControl();
    }

    private void IndexingGridControl()
    {
        if (DGrid.CurrentRow is null || !CmbPaymentMode.Visible) return;
        var currentRow = _rowIndex;
        var columnIndex = DGrid.Columns["GTxtPaymentMode"].Index;
        CmbPaymentMode.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        CmbPaymentMode.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        CmbPaymentMode.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtLedger"].Index;
        TxtLedger.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtLedger.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtLedger.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"].Index;
        TxtAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAmount.TabIndex = columnIndex;
    }

    private void OpenLedgerList()
    {
        var category = CmbPaymentMode.Text switch
        {
            "BANK" => "BANK",
            "CREDIT" => "CUSTOMER",
            _ => "CASH"
        };
        (TxtLedger.Text, _ledgerId) = GetMasterList.GetGeneralLedger("SAVE", category);
        TxtLedger.Focus();
    }

    // OBJECT FOR THIS FORM
    private bool IsRowUpdate { get; set; }

    private long _ledgerId = 0;
    private int _colIndex = 0;
    private int _rowIndex = 0;
    public decimal TotalReceived = 0;
    public decimal BasicAmount = 0;
    public DataGridView SettleView = new();
    private MrGridNumericTextBox TxtAmount { get; set; }
    private MrGridTextBox TxtLedger { get; set; }
    private MrGridComboBox CmbPaymentMode { get; set; }
    private readonly IMasterSetup _master = new ClsMasterSetup();
}