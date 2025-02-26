using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.CommonSetup;
using MrDAL.DataEntry.Interface.Common;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmTermCalculation : MrForm
{
    #region --------------- EVENTS FOR THIS FORM ---------------

    public FrmTermCalculation(string actionTag, decimal basicAmount, string termType, DataTable exitsTerm, decimal taxable, decimal totalQty, string taxType) : this(actionTag, basicAmount.ToString(), false, termType, 0, 0, exitsTerm, totalQty.ToString(), taxable.ToString(), taxType)
    {

    }

    public FrmTermCalculation(string actionTag, string basicAmount, bool isProductWise, string termType, long productId, int serialNo = 0, DataTable exitsTerm = null, string totalQty = "", string taxableAmount = "", string taxType = "")
    {
        InitializeComponent();
        _actionTag = actionTag;
        LblBasicAmount.Text = basicAmount;
        Shown += FrmTermCalculation_Shown;
        _temCalc = new TermCalculationRepository();
        IniInitializeGrid();
        _actionTag = actionTag;
        _basicAmount = basicAmount;
        _isProductWise = isProductWise;
        _termType = termType;
        _productId = productId;
        _serialNo = serialNo;
        _exitsTerm = exitsTerm;
        _totalQty = totalQty;
        LblTaxableAmount.Text = taxableAmount.GetDecimalString();
        _taxType = taxType;
        FillGridValue();
        EnableGridControl();
        ClearLedgerDetails();
    }

    private void FrmTermCalculation_Shown(object sender, EventArgs e)
    {
        LblTaxable.Visible = LblTaxableAmount.Visible = _taxType.Equals("P VAT");
        DGrid.Focus();
    }

    private void FrmTermCalculation_Load(object sender, EventArgs e)
    {

    }

    private void FrmTermCalculation_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is not (char)Keys.Escape)
        {
            return;
        }

        CalcTermTable = ConvertToDataTable();
        var msg = CustomMessageBox.ExitActiveForm();
        if (msg != DialogResult.Yes)
        {
            return;
        }

        if (CalcTermTable == null || CalcTermTable.RowsCount() <= 0)
        {
            return;
        }

        foreach (DataRow row in CalcTermTable.Rows)
        {
            row["GTxtRate"] = 0;
            row["GTxtAmount"] = 0;
            row["GTxtValueAmount"] = 0;
        }

        DialogResult = DialogResult.OK;
        Dispose(true);

    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {

    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        CalcTermTable = ConvertToDataTable();
        TotalTermAmount = LblTotalNetTermAmount.Text;
        Close();
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is not Keys.Enter || TxtRate.Enabled)
        {
            return;
        }
        e.SuppressKeyPress = true;
        EnableGridControl(true);
        AdjustControlsInDataGrid();
        if (!DGrid.Rows[_rowIndex].Cells["GTxtDescription"].Value.IsValueExits())
        {
            return;
        }
        TextFromGrid();
        TxtRate.Focus();
    }

    private void OnDGridOnCellEnter(object _, DataGridViewCellEventArgs e)
    {
        _columnIndex = e.ColumnIndex;
    }

    private void OnDGridOnGotFocus(object _, EventArgs e)
    {
        DGrid.Rows[_rowIndex].Cells[0].Selected = true;
    }

    private void OnDGridOnRowEnter(object _, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        DGrid_KeyDown(sender, new KeyEventArgs(Keys.Enter));
    }

    private void OnTxtAmountOnValidating(object o, CancelEventArgs cancelEventArgs)
    {
        TxtAmount.Text = TxtAmount.GetDecimalString();
        if (!TxtRate.Enabled || ActiveControl == TxtRate)
        {
            return;
        }
        var result = AddTextToGrid(_isRowUpdate);
        DGrid.Focus();
        if (result)
        {
            BtnSave.Focus();
            return;
        }
    }

    private void OnTxtAmountOnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Shift && e.KeyCode is Keys.Tab)
        {
            SendToBack();
        }
    }

    private void OnTxtRateOnValidating(object sender, CancelEventArgs cancelEventArgs)
    {
        TxtRate.Text = TxtRate.GetRateDecimalString();

        if (_actionTag.Equals("UPDATE"))
        {
            if (_vatTermId.Contains(_termId))
            {
                OnTxtRateOnTextChanged(sender, EventArgs.Empty);
            }
        }
        else
        {
            var amount = TxtAmount.GetDecimal();
            TxtAmount.Text = amount.GetDecimalString();
        }


    }

    private void OnTxtRateOnTextChanged(object sender, EventArgs e)
    {
        var amount = LblBasicAmount.GetDecimal();
        var actualAmount = LblBasicAmount.GetDecimal();
        var taxableAmount = LblTaxableAmount.GetDecimal();
        var actualTaxableAmount = LblTaxableAmount.GetDecimal();

        if (DGrid.CurrentRow == null)
        {
            return;
        }

        for (var i = 0; i < DGrid.CurrentRow.Index; i++)
        {
            var termAmount = DGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
            if (DGrid.Rows[i].Cells["GTxtSign"].Value.Equals("-"))
            {
                var taxDiscount = termAmount > 0 ? termAmount / amount * taxableAmount : 0;
                amount -= termAmount;
                taxableAmount -= taxDiscount;

            }
            else
            {
                var taxDiscount = termAmount / amount * taxableAmount;
                amount += termAmount;
                taxableAmount -= taxDiscount;
            }
        }

        var netTermAmount = (TxtRate.GetDecimal() * amount / 100);
        var totalTermAmount = TxtRate.GetDecimal() > 0 ? netTermAmount : 0;
        if (_taxType.Equals("P VAT"))
        {
            if (_vatTermId.Contains(_termId))
            {
                netTermAmount = (TxtRate.GetDecimal() * taxableAmount / 100);
                totalTermAmount = TxtRate.GetDecimal() > 0 ? netTermAmount : 0;
            }
        }

        TxtAmount.Text = totalTermAmount.GetDecimalString();
    }

    #endregion --------------- EVENTS FOR THIS FORM ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD FOR THIS FORM ---------------

    private DataTable ConvertToDataTable()
    {
        if (DGrid.ColumnCount == 0)
        {
            return null;
        }
        var dtSource = new DataTable();
        foreach (DataGridViewColumn col in DGrid.Columns)
        {
            if (col.Name == string.Empty)
            {
                continue;
            }
            dtSource.Columns.Add(col.Name);
            dtSource.Columns[col.Name].Caption = col.HeaderText;
        }

        if (dtSource.Columns.Count == 0)
        {
            return null;
        }
        foreach (DataGridViewRow row in DGrid.Rows)
        {
            var drNewRow = dtSource.NewRow();
            foreach (DataColumn col in dtSource.Columns) drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
            dtSource.Rows.Add(drNewRow);
        }

        return dtSource;
    }

    private void IniInitializeGrid()
    {
        _temCalc.GetBillingTermDesign(DGrid);
        DGrid.RowEnter += OnDGridOnRowEnter;
        DGrid.GotFocus += OnDGridOnGotFocus;
        DGrid.CellEnter += OnDGridOnCellEnter;
        DGrid.KeyDown += DGrid_KeyDown;

        TxtSno = new MrGridNumericTextBox(DGrid)
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center
        };
        TxtDescription = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtBasic = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtSign = new MrGridTextBox(DGrid)
        {
            ReadOnly = true
        };
        TxtRate = new MrGridNumericTextBox(DGrid);
        TxtRate.TextChanged += OnTxtRateOnTextChanged;
        TxtRate.Validating += OnTxtRateOnValidating;

        TxtAmount = new MrGridNumericTextBox(DGrid);
        TxtAmount.KeyDown += OnTxtAmountOnKeyDown;
        TxtAmount.Validating += OnTxtAmountOnValidating;

        AdjustControlsInDataGrid();
        ObjGlobal.DGridColorCombo(DGrid);
    }

    private void FillGridValue()
    {
        if (_actionTag.Equals("UPDATE") && _exitsTerm.Rows.Count > 0)
        {
            var iRows = 0;
            _exitsTerm.DefaultView.Sort = "SNo";
            _exitsTerm = _exitsTerm.DefaultView.ToTable();
            DGrid.Rows.Add(_exitsTerm.Rows.Count);
            foreach (DataRow ro in _exitsTerm.Rows)
            {
                DGrid.Rows[iRows].Cells["GTxtSno"].Value = ro["SNo"];
                DGrid.Rows[iRows].Cells["GTxtTermId"].Value = ro["TermId"];
                DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = ro["OrderNo"];
                DGrid.Rows[iRows].Cells["GTxtDescription"].Value = ro["TermName"];
                DGrid.Rows[iRows].Cells["GTxtBasic"].Value = ro["Basis"];
                DGrid.Rows[iRows].Cells["GTxtSign"].Value = ro["Sign"];
                DGrid.Rows[iRows].Cells["GTxtRate"].Value = ro["TermRate"].GetDecimalString();
                DGrid.Rows[iRows].Cells["GTxtTermCondition"].Value = ro["TermType"];
                DGrid.Rows[iRows].Cells["GTxtTermType"].Value = ro["TermType"];
                DGrid.Rows[iRows].Cells["GTxtAmount"].Value = ro["TermAmt"].GetDecimalString();
                DGrid.Rows[iRows].Cells["GTxtValueAmount"].Value = ro["TermAmt"];
                DGrid.Rows[iRows].Cells["GTxtProductId"].Value = ro["ProductId"];
                DGrid.Rows[iRows].Cells["GTxtProductSno"].Value = ro["ProductSno"];
                iRows++;
            }
        }
        else
        {
            var dt = _temCalc.GetTermCalculationForVoucher(_termType, _isProductWise ? "P" : "B");
            if (dt.RowsCount() <= 0)
            {
                return;
            }
            var iRows = 0;
            DGrid.Rows.Clear();
            DGrid.Rows.Add(dt.Rows.Count);
            foreach (DataRow ro in dt.Rows)
            {
                DGrid.Rows[iRows].Cells["GTxtSno"].Value = iRows + 1;
                DGrid.Rows[iRows].Cells["GTxtTermId"].Value = ro["TermId"];
                DGrid.Rows[iRows].Cells["GTxtOrderNo"].Value = ro["OrderNo"];
                DGrid.Rows[iRows].Cells["GTxtDescription"].Value = ro["TermDesc"];
                DGrid.Rows[iRows].Cells["GTxtBasic"].Value = ro["TermBasic"];

                var termBasic = ro["TermBasic"].GetTrimReplace();

                DGrid.Rows[iRows].Cells["GTxtSign"].Value = ro["TermSign"];
                DGrid.Rows[iRows].Cells["GTxtRate"].Value = _actionTag.Equals("UPDATE") ? 0.GetRateDecimalString() : ro["TermRate"].GetRateDecimalString();
                DGrid.Rows[iRows].Cells["GTxtTermCondition"].Value = ro["TermCondition"];
                DGrid.Rows[iRows].Cells["GTxtTermType"].Value = ro["TermType"];
                var termType = ro["TermType"].GetTrimReplace();
                decimal round = 0;
                if (termType.Equals("R"))
                {
                    var netAmount = LblBasicAmount.GetDecimal() + LblTotalNetTermAmount.GetDecimal();
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
                    DGrid.Rows[iRows].Cells["GTxtAmount"].Value = round.GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtValueAmount"].Value = round.GetDecimalString();
                }
                else
                {
                    var valueAmount = termBasic.Equals("VALUE")
                        ? ro["TermRate"].GetDecimal() * LblBasicAmount.GetDecimal() / 100
                        : ro["TermRate"].GetDecimal() * _totalQty.GetDecimal();
                    if (_taxType.Equals("P VAT"))
                    {
                        if (_vatTermId.Contains(_termId))
                        {
                            valueAmount = ro["TermRate"].GetDecimal() * LblTaxableAmount.GetDecimal() / 100;
                        }
                    }

                    DGrid.Rows[iRows].Cells["GTxtAmount"].Value = valueAmount.GetDecimalString();
                    DGrid.Rows[iRows].Cells["GTxtValueAmount"].Value = (ro["TermRate"].GetDecimal() * LblBasicAmount.GetDecimal() / 100).GetDecimalString();
                }
                DGrid.Rows[iRows].Cells["GTxtProductId"].Value = _productId;
                DGrid.Rows[iRows].Cells["GTxtProductSno"].Value = _serialNo;
                iRows++;
            }
        }
    }

    private void CalculateTerm(decimal basicAmount, decimal taxableAmount)
    {
        if (DGrid.RowCount <= 0 && !DGrid.Rows[0].Cells["GTxtDescription"].Value.IsValueExits())
        {
            return;
        }

        decimal termTotal = 0;
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            var amount = DGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
            if (DGrid.Rows[i].Cells["GTxtSign"].Value.Equals("-"))
            {
                basicAmount -= amount;
            }
            else
            {
                basicAmount += amount;
            }
            TermCalculation(basicAmount, i + 1, taxableAmount);
            if (DGrid.Rows[i].Cells["GTxtSign"].Value.Equals("-"))
            {
                termTotal -= amount;
            }
            else
            {
                termTotal += amount;
            }

            LblTotalNetTermAmount.Text = termTotal.GetDecimalString();
        }
    }

    private void TextFromGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        TxtSno.Text = DGrid.Rows[_rowIndex].Cells["GTxtSno"].Value.GetString();
        _termId = DGrid.Rows[_rowIndex].Cells["GTxtTermId"].Value.GetInt();
        TxtDescription.Text = DGrid.Rows[_rowIndex].Cells["GTxtDescription"].Value.GetString();
        TxtBasic.Text = DGrid.Rows[_rowIndex].Cells["GTxtBasic"].Value.GetString();
        TxtSign.Text = DGrid.Rows[_rowIndex].Cells["GTxtSign"].Value.GetString();
        TxtRate.Text = DGrid.Rows[_rowIndex].Cells["GTxtRate"].Value.GetString();
        TxtAmount.Text = DGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetString();
        _isRowUpdate = true;
    }

    private void EnableGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;
        TxtDescription.Enabled = false;
        TxtDescription.Visible = isEnable;
        TxtBasic.Enabled = false;
        TxtBasic.Visible = isEnable;
        TxtSign.Enabled = false;
        TxtSign.Visible = isEnable;
        TxtRate.Enabled = TxtRate.Visible = isEnable;
        TxtAmount.Enabled = TxtAmount.Visible = isEnable;
        DGrid.ScrollBars = isEnable ? ScrollBars.None : ScrollBars.Both;
    }

    private void VoucherTotalCalculation()
    {
        if (DGrid.RowCount <= 0 || !DGrid.Rows[0].Cells["GTxtDescription"].Value.IsValueExits())
        {
            return;
        }
        var basicAmount = LblBasicAmount.GetDecimal();
        decimal termTotal = 0;
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            var amount = DGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
            if (DGrid.Rows[i].Cells["GTxtSign"].Value.Equals("-"))
            {
                termTotal -= amount;
            }
            else
            {
                termTotal += amount;
            }
            LblTotalNetTermAmount.Text = termTotal.GetDecimalString();
        }
    }

    private void TermCalculation(decimal basicAmount, int rowIndex, decimal taxableAmount)
    {
        var termAmount = basicAmount;
        var termTaxableAmount = taxableAmount;
        rowIndex += 1;
        for (var i = 0; i < DGrid.RowCount; i++)
        {
            var rate = DGrid.Rows[i].Cells["GTxtRate"].Value.GetDecimal();
            var termType = DGrid.Rows[i].Cells["GTxtTermType"].Value.GetTrimReplace();
            var sign = DGrid.Rows[i].Cells["GTxtSign"].Value.GetTrimReplace();
            var amount = DGrid.Rows[i].Cells["GTxtAmount"].Value.GetDecimal();
            var termId = DGrid.Rows[i].Cells["GTxtTermId"].Value.GetInt();
            decimal round = 0;
            if (termType.Equals("R"))
            {
                var netAmount = LblBasicAmount.GetDecimal();
                for (var r = 0; r < i; r++)
                {
                    var termSign = DGrid.Rows[r].Cells["GTxtSign"].Value.GetTrimReplace();
                    var tAmount = DGrid.Rows[r].Cells["GTxtValueAmount"].Value.GetDecimal();
                    netAmount = termSign.Equals("+") ? netAmount + tAmount : netAmount - tAmount;
                }

                var roundAmount = netAmount - Math.Truncate(netAmount);
                if (roundAmount >= 0.5.GetDecimal() && sign.Equals("+"))
                {
                    round = 1 - roundAmount;
                }
                if (roundAmount < 0.5.GetDecimal() && sign.Equals("-"))
                {
                    round = roundAmount;
                }
            }

            if (termType.Equals("R"))
            {
                DGrid.Rows[i].Cells["GTxtAmount"].Value = round.GetDecimalString();
                DGrid.Rows[i].Cells["GTxtValueAmount"].Value = round.GetDecimal();
            }
            else
            {
                if (rowIndex > i)
                {
                    var termRatio = amount switch
                    {
                        > 0 => amount / termAmount * termTaxableAmount,
                        _ => 0
                    };

                    if (sign.Equals("+"))
                    {

                        termAmount += amount;
                        termTaxableAmount += termRatio;
                    }
                    else
                    {
                        termAmount -= amount;
                        termTaxableAmount -= termRatio;
                    }
                    continue;
                }
                amount = rate > 0 ? (rate * termAmount / 100).GetDecimal() : amount.GetDecimal();
                if (_taxType.Equals("P VAT"))
                {
                    if (_vatTermId.Contains(termId))
                    {
                        amount = rate > 0 ? (rate * termTaxableAmount / 100).GetDecimal() : amount.GetDecimal();
                    }
                }
                DGrid.Rows[i].Cells["GTxtAmount"].Value = amount.GetDecimalString();
                DGrid.Rows[i].Cells["GTxtValueAmount"].Value = amount;
                termAmount = sign.Equals("+") ? +termAmount + amount : termAmount - amount;
            }
        }
    }

    private void ClearLedgerDetails()
    {
        _isRowUpdate = false;
        if (DGrid.RowCount is 0)
        {
            DGrid.Rows.Add();
            TxtSno.Text = DGrid.RowCount.ToString();
        }

        _termId = 0;
        TxtSno.Clear();
        TxtDescription.Clear();
        TxtBasic.Clear();
        TxtSign.Clear();
        TxtRate.Clear();
        TxtAmount.Clear();
        AdjustControlsInDataGrid();
        VoucherTotalCalculation();
    }

    private bool AddTextToGrid(bool isUpdate)
    {
        var iRows = _rowIndex;
        DGrid.Rows[iRows].Cells["GTxtRate"].Value = TxtRate.Text;
        DGrid.Rows[iRows].Cells["GTxtAmount"].Value = TxtAmount.Text;
        DGrid.Rows[iRows].Cells["GTxtValueAmount"].Value = TxtAmount.Text;
        var currentRow = DGrid.RowCount - 1 > iRows ? iRows + 1 : DGrid.RowCount - 1;
        DGrid.CurrentCell = DGrid.Rows[currentRow].Cells[_columnIndex];
        VoucherTotalCalculation();
        TermCalculation(LblBasicAmount.GetDecimal(), iRows, LblTaxableAmount.GetDecimal());
        if (isUpdate)
        {
            EnableGridControl();
            ClearLedgerDetails();
            DGrid.Focus();
            if (iRows + 1 != DGrid.RowCount)
            {
                return false;
            }

            BtnSave.Focus();
            return true;
        }

        ClearLedgerDetails();
        return false;
    }

    private void AdjustControlsInDataGrid()
    {
        if (DGrid.CurrentRow == null)
        {
            return;
        }
        var currentRow = _rowIndex;
        var columnIndex = DGrid.Columns["GTxtSno"].Index;
        TxtSno.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSno.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSno.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtDescription"].Index;
        TxtDescription.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtDescription.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtDescription.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtBasic"].Index;
        TxtBasic.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBasic.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBasic.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtSign"].Index;
        TxtSign.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtSign.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtSign.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtRate"].Index;
        TxtRate.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtRate.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtRate.TabIndex = columnIndex;

        columnIndex = DGrid.Columns["GTxtAmount"].Index;
        TxtAmount.Size = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtAmount.Location = DGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtAmount.TabIndex = columnIndex;
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT FOR THIS FORM ---------------

    private int _termId;
    private int _rowIndex;
    private int _columnIndex;

    private readonly int _serialNo = 0;
    private readonly int[] _vatTermId = [ObjGlobal.SalesVatTermId, ObjGlobal.PurchaseVatTermId];
    private readonly long _productId;

    private bool _isRowUpdate;
    private readonly bool _isProductWise;

    public string TotalTermAmount;

    private readonly string _actionTag;
    private string _basicAmount;
    private readonly string _totalQty;
    private readonly string _taxType;
    private readonly string _termType;

    private readonly ITermCalculationRepository _temCalc;

    private DataTable _exitsTerm;
    public DataTable CalcTermTable = new();
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtDescription { get; set; }
    private MrGridTextBox TxtBasic { get; set; }
    private MrGridTextBox TxtSign { get; set; }
    private MrGridNumericTextBox TxtRate { get; set; }
    private MrGridNumericTextBox TxtAmount { get; set; }

    #endregion --------------- OBJECT FOR THIS FORM ---------------
}