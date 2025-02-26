using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.DataEntry.PurchaseMaster;

public partial class FrmAdditionalTerms : MrForm
{
    #region ------------- PURCHASE ADDITIONAL TERM -------------

    public FrmAdditionalTerms(DataTable dtTerm, decimal basicAmount)
    {
        InitializeComponent();
        _master = new ClsMasterSetup();
        LblBasicAmount.Text = basicAmount.GetDecimalString();
        DesignGridColumnsAsync();
        BindExitingTerm(dtTerm);
        EnableBillGridControl();
        AdjustControlsInDataBillGrid();
    }

    private void FrmAdditionalTerms_Load(object sender, EventArgs e)
    {
    }

    private void FrmAdditionalTerms_Shown(object sender, EventArgs e)
    {
        BillGrid.Focus();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (BillGrid.RowCount > 0)
        {
            TermCalculation = new DataTable();
            foreach (DataGridViewColumn col in BillGrid.Columns)
            {
                TermCalculation.AddColumn(col.Name, col.CellType);
            }
            foreach (DataGridViewRow row in BillGrid.Rows)
            {
                TermCalculation.Rows.Add(row);
            }
        }
        DialogResult = DialogResult.Yes;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void TxtBTermAmount_Validated(object sender, EventArgs e)
    {
        if (TxtBTermAmount.Focused)
        {
            TxtBTermAmount.Text = TxtBTermAmount.GetDecimalString();
            TxtBTermAmount_TextChanged(sender, EventArgs.Empty);
            if (TxtBTermAmount.GetDecimal() > 0)
            {
                AddTextToBillGrid(_isRowUpdate);
                AdjustControlsInDataBillGrid();
                ClearBillDetails();
                TxtBTermDesc.Focus();
                return;
            }
        }
    }

    private void TxtBTermAmount_TextChanged(object sender, EventArgs e)
    {
        var amount = LblBasicAmount.GetDecimal();
        if (BillGrid.CurrentRow != null)
        {
            for (var i = 0; i < BillGrid.CurrentRow.Index; i++)
            {
                var sign = BillGrid.Rows[i].Cells["GTxtSign"].Value.GetString();
                if (sign.Equals("-"))
                {
                    amount -= BillGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
                }
                else if (sign.Equals("+"))
                {
                    amount += BillGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
                }
            }
            amount = TxtBTermSign.Text switch
            {
                "-" => amount - TxtBTermAmount.GetDecimal(),
                _ => amount - TxtBTermAmount.GetDecimal()
            };
        }
        TxtBNetAmount.Text = amount.GetDecimalString();
    }

    private void TxtBTermRate_Validated(object sender, EventArgs e)
    {
        TxtBTermRate.Text = TxtBTermRate.GetDecimalString();
        TxtBTermRate_TextChanged(sender, EventArgs.Empty);
    }

    private void TxtBTermRate_TextChanged(object sender, EventArgs e)
    {
        decimal basicAmount = 0;
        if (BillGrid.CurrentRow != null)
        {
            basicAmount = BillGrid.CurrentRow.Index <= 0
                ? LblBasicAmount.GetDecimal()
                : BillGrid.Rows[BillGrid.CurrentRow.Index - 1].Cells["GTxtAmount"].Value.GetDecimal();
        }
        var billTermRate = TxtBTermRate.GetDecimal();
        var amount = billTermRate switch
        {
            > 0 => billTermRate * basicAmount / 100,
            _ => 0
        };
        TxtBTermAmount.Text = amount.GetDecimalString();
        TxtBTermAmount_TextChanged(sender, e);
    }

    private void TxtBSubLedger_Validated(object sender, EventArgs e)
    {
    }

    private void TxtBSubLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenSubLedgerList();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBSubLedger, OpenSubLedgerList);
        }
    }

    private void TxtBLedger_Validated(object sender, EventArgs e)
    {
    }

    private void TxtBLedger_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.F1)
        {
            OpenGeneralLedger();
        }
        else if (e.KeyCode is Keys.Enter)
        {
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtBLedger, OpenGeneralLedger);
        }
    }

    private void TxtBTerm_Validated(object sender, EventArgs e)
    {
        if (BillGrid.CurrentRow != null && BillGrid.RowCount > 0 && BillGrid.Rows[BillGrid.CurrentRow.Index].Cells["GTxtTermId"].Value.GetInt() is 0)
        {
            BtnSave.Focus();
        }
    }

    private void TxtBTerm_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void BillGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        BillGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void BillGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Enter && !TxtBTermDesc.Enabled)
        {
            e.SuppressKeyPress = true;
            EnableBillGridControl(true);
            BillGrid.CurrentCell = BillGrid.CurrentRow?.Cells[0];
            AdjustControlsInDataBillGrid();
            if (BillGrid.CurrentRow != null && BillGrid.Rows[BillGrid.CurrentRow.Index].Cells["GTxtTerm"].Value.IsValueExits())
            {
                TextFromBillGrid();
                TxtBTermDesc.Focus();
                return;
            }
            TxtBTermDesc.Focus();
        }
    }

    private void FrmAdditionalTerms_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (TxtBTermDesc.Enabled)
            {
                EnableBillGridControl();
                BillGrid.Focus();
            }
            else
            {
                if (CustomMessageBox.ExitActiveForm() is DialogResult.Yes)
                {
                    Close();
                }
            }
        }
    }

    #endregion ------------- PURCHASE ADDITIONAL TERM -------------

    // METHOD FOR THIS FORM

    #region ------------- METHOD -------------

    private void ClearBillDetails()
    {
        _termId = 0;
        TxtBTermDesc.Clear();
        _ledgerId = 0;
        TxtBLedger.Clear();
        _subledgerId = 0;
        TxtBSubLedger.Clear();
        TxtBTermRate.Clear();
        TxtBTermAmount.Clear();
        TxtBTermType.Clear();
        TxtBTermSign.Clear();
        TxtBNetAmount.Clear();
    }

    private void TextFromBillGrid()
    {
        if (BillGrid.CurrentRow is null)
        {
            return;
        }
        var index = BillGrid.CurrentRow.Index;
        TxtBTermSno.Text = BillGrid.Rows[index].Cells["GTxtTermSno"].Value.GetIntString();
        _termId = BillGrid.Rows[index].Cells["GTxtTermId"].Value.GetInt();
        TxtBTermDesc.Text = BillGrid.Rows[index].Cells["GTxtTerm"].Value.GetString();
        _ledgerId = BillGrid.Rows[index].Cells["GTxtLedgerId"].Value.GetLong();
        TxtBLedger.Text = BillGrid.Rows[index].Cells["GTxtLedger"].Value.GetString();
        _subledgerId = BillGrid.Rows[index].Cells["GTxtSubledgerId"].Value.GetInt();
        TxtBSubLedger.Text = BillGrid.Rows[index].Cells["GTxtSubLedger"].Value.GetString();
        TxtBTermType.Text = BillGrid.Rows[index].Cells["GTxtTermType"].Value.GetString();
        TxtBTermSign.Text = BillGrid.Rows[index].Cells["GTxtSign"].Value.GetString();
        TxtBTermRate.Text = BillGrid.Rows[index].Cells["GTxtRate"].Value.GetDecimalString();
        TxtBTermAmount.Text = BillGrid.Rows[index].Cells["GTxtAmount"].Value.GetDecimalString();
        TxtBNetAmount.Text = BillGrid.Rows[index].Cells["GTxtInvoiceNetAmount"].Value.GetDecimalString();
        _isRowUpdate = true;
    }

    private void BindExitingTerm(DataTable table)
    {
        var dtTerm = _master.GetTermCalculationForVoucher("PAB", "P");
        if (dtTerm.Rows.Count > 0)
        {
            BillGrid.Rows.Add(dtTerm.Rows.Count);
            foreach (DataRow row in dtTerm.Rows)
            {
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtTermSno"].Value = dtTerm.Rows.IndexOf(row) + 1;
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtTermId"].Value = row["TermId"].ToString();
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtTerm"].Value = row["TermDesc"].ToString();
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtCbLedgerId"].Value = row["Ledger"].ToString();
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtTermType"].Value = row["TermBasic"].ToString();
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtSign"].Value = row["TermSign"].ToString();
                var rateAmount = row["TermRate"].GetDecimal();
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtRate"].Value = rateAmount.GetDecimalString();

                var amount = LblBasicAmount.GetDecimal() * rateAmount / 100;
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtRate"].Value = amount.GetDecimalString();
            }
        }
        if (table.Rows.Count > 0)
        {
            foreach (DataRow row in table.Rows)
            {
                BillGrid.Rows[table.Rows.IndexOf(row)].Cells["GTxtTermSno"].Value = row[""].ToString();
                BillGrid.Rows[table.Rows.IndexOf(row)].Cells["GTxtTermId"].Value = row[""].ToString();
                BillGrid.Rows[table.Rows.IndexOf(row)].Cells["GTxtCbLedgerId"].Value = row[""].ToString();
                BillGrid.Rows[table.Rows.IndexOf(row)].Cells["GTxtTermType"].Value = row[""].ToString();
                BillGrid.Rows[table.Rows.IndexOf(row)].Cells["GTxtSign"].Value = row[""].ToString();
                var rateAmount = row["TermRate"].GetDecimal();
                BillGrid.Rows[dtTerm.Rows.IndexOf(row)].Cells["GTxtRate"].Value = rateAmount.GetDecimalString();
            }
        }
    }

    private void DesignGridColumnsAsync()
    {
        TxtBTermSno = new MrGridNumericTextBox(BillGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtBTermDesc = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBTermDesc.KeyDown += TxtBTerm_KeyDown;
        TxtBTermDesc.Validated += TxtBTerm_Validated;

        TxtBLedger = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBLedger.KeyDown += TxtBLedger_KeyDown;
        TxtBLedger.Validated += TxtBLedger_Validated;

        TxtBSubLedger = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBSubLedger.KeyDown += TxtBSubLedger_KeyDown;
        TxtBSubLedger.Validated += TxtBSubLedger_Validated;

        TxtBTermType = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBTermSign = new MrGridTextBox(BillGrid)
        {
            ReadOnly = true
        };
        TxtBTermRate = new MrGridNumericTextBox(BillGrid);
        TxtBTermRate.TextChanged += TxtBTermRate_TextChanged;
        TxtBTermRate.Validated += TxtBTermRate_Validated;

        TxtBTermAmount = new MrGridNumericTextBox(BillGrid);
        TxtBTermAmount.TextChanged += TxtBTermAmount_TextChanged;
        TxtBTermAmount.Validated += TxtBTermAmount_Validated;

        TxtBNetAmount = new MrGridNumericTextBox(BillGrid)
        {
            ReadOnly = true
        };
        ObjGlobal.DGridColorCombo(BillGrid);
        AdjustControlsInDataBillGrid();
    }

    private void AdjustControlsInDataBillGrid()
    {
        if (BillGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = BillGrid.CurrentRow.Index;
        var columnIndex = BillGrid.Columns["GTxtTermSno"]!.Index;
        TxtBTermSno.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermSno.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermSno.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtTerm"]!.Index;
        TxtBTermDesc.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermDesc.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermDesc.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtLedger"]!.Index;
        TxtBLedger.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBLedger.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBLedger.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtSubLedger"]!.Index;
        TxtBSubLedger.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBSubLedger.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBSubLedger.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtTermType"]!.Index;
        TxtBTermType.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermType.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermType.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtSign"]!.Index;
        TxtBTermSign.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermSign.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermSign.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtRate"]!.Index;
        TxtBTermRate.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermRate.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermRate.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtAmount"]!.Index;
        TxtBTermAmount.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBTermAmount.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBTermAmount.TabIndex = columnIndex;

        columnIndex = BillGrid.Columns["GTxtInvoiceNetAmount"]!.Index;
        TxtBNetAmount.Size = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBNetAmount.Location = BillGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBNetAmount.TabIndex = columnIndex;
    }

    private void OpenSubLedgerList()
    {
        var (description, id) = GetMasterList.GetSubLedgerList(_actionTag);
        if (description.IsValueExits())
        {
            TxtBSubLedger.Text = description;
            _subledgerId = id;
        }
        TxtBSubLedger.Focus();
    }

    private void OpenGeneralLedger()
    {
        var (description, id) = GetMasterList.GetGeneralLedger(_actionTag, "VENDOR");
        if (description.IsValueExits())
        {
            TxtBLedger.Text = description;
            _ledgerId = id;
        }
        TxtBLedger.Focus();
    }

    private bool AddTextToBillGrid(bool isUpdate)
    {
        if (BillGrid.CurrentRow != null)
        {
            var iRows = BillGrid.CurrentRow.Index;
            BillGrid.Rows[iRows].Cells["GTxtTermId"].Value = _termId.ToString();
            BillGrid.Rows[iRows].Cells["GTxtTerm"].Value = TxtBTermDesc.Text;
            BillGrid.Rows[iRows].Cells["GTxtLedgerId"].Value = _ledgerId.ToString();
            BillGrid.Rows[iRows].Cells["GTxtLedger"].Value = TxtBLedger.Text;
            BillGrid.Rows[iRows].Cells["GTxtSubledgerId"].Value = _subledgerId;
            BillGrid.Rows[iRows].Cells["GTxtSubLedger"].Value = TxtBSubLedger.Text;
            BillGrid.Rows[iRows].Cells["GTxtTermType"].Value = TxtBTermType.Text;
            BillGrid.Rows[iRows].Cells["GTxtSign"].Value = TxtBTermSign.Text;
            BillGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtBTermRate.Text;
            BillGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtBTermAmount.Text;
            BillGrid.Rows[iRows].Cells["GTxtInvoiceNetAmount"].Value = TxtBNetAmount.Text;
            var currentRow = BillGrid.RowCount - 1 > iRows ? iRows + 1 : BillGrid.RowCount - 1;
            BillGrid.CurrentCell = BillGrid.Rows[currentRow].Cells[0];
        }
        return true;
    }

    private void EnableBillGridControl(bool isEnable = false)
    {
        TxtBTermSno.Enabled = false;
        TxtBTermSno.Visible = isEnable;

        TxtBTermDesc.Enabled = TxtBTermDesc.Visible = isEnable;
        TxtBLedger.Enabled = TxtBLedger.Visible = isEnable;

        TxtBSubLedger.Enabled = TxtBSubLedger.Enabled = isEnable && ObjGlobal.PurchaseSubLedgerEnable;

        TxtBTermType.Enabled = false;
        TxtBTermType.Visible = isEnable;

        TxtBTermSign.Enabled = false;
        TxtBTermSign.Visible = isEnable;

        TxtBTermRate.Enabled = TxtBTermRate.Visible = isEnable;
        TxtBTermAmount.Enabled = TxtBTermAmount.Visible = isEnable;

        TxtBNetAmount.Enabled = false;
        TxtBNetAmount.Visible = isEnable;
    }

    #endregion ------------- METHOD -------------

    // OBJECT FOR THIS FORM

    #region ------------- OBJECT -------------

    private int _termId = 0;
    private int _subledgerId = 0;
    private long _ledgerId = 0;
    private long _productId = 0;
    private bool _isRowUpdate = false;
    private string _actionTag = "SAVE";
    public DataTable TermCalculation;
    private IMasterSetup _master;

    // GRID CONTROL

    #region ------------- Grid Control -------------

    private MrGridNumericTextBox TxtBTermSno { get; set; }
    private MrGridTextBox TxtBTermDesc { get; set; }
    private MrGridTextBox TxtBLedger { get; set; }
    private MrGridTextBox TxtBSubLedger { get; set; }
    private MrGridTextBox TxtBTermType { get; set; }
    private MrGridTextBox TxtBTermSign { get; set; }
    private MrGridNumericTextBox TxtBTermRate { get; set; }
    private MrGridNumericTextBox TxtBTermAmount { get; set; }
    private MrGridNumericTextBox TxtBNetAmount { get; set; }

    #endregion ------------- Grid Control -------------

    #endregion ------------- OBJECT -------------
}