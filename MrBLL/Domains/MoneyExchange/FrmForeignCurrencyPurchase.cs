using MrBLL.DataEntry.Common;
using MrBLL.Utility.Common.Class;
using MrDAL.Control.ControlsEx.GridControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Design;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.LedgerSetup;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.Domains.MoneyExchange;

public partial class FrmForeignCurrencyPurchase : Form
{
    // FOREIGN CURRENCY PURCHASE
    public FrmForeignCurrencyPurchase()
    {
        InitializeComponent();
        _voucher = new FinanceDesign();
        _entry = new ClsFinanceEntry();
        _setup = new ClsMasterSetup();
        _currency = new CurrencyRepository();

        _voucher.GetCurrencyPurchaseEntryDesign(RGrid);

        GetRGridColumns();
        AddControlsInDataGrid();
        EnableControl();
        ClearControl();
    }
    private void FrmForeignCurrencyPurchase_Load(object sender, EventArgs e)
    {

    }
    private void FrmForeignCurrencyPurchase_Shown(object sender, EventArgs e)
    {
        if (BtnNew.Enabled)
        {
            MskMiti.Focus();
        }
        else
        {
            BtnNew.Focus();
        }
    }
    private void FrmForeignCurrencyPurchase_KeyPress(object sender, KeyPressEventArgs e)
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
                if (TxtShortName.Enabled)
                {
                    ClearCurrencyDetails();
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


    // FORM CONTROL EVENTS
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
        MskMiti.Focus();
    }
    private void BtnPrint_Click(object sender, EventArgs e)
    {
        PrintVoucher(false);
    }
    private void BtnExit_Click(object sender, EventArgs e)
    {

    }
    private void BtnSave_Click(object sender, EventArgs e)
    {

    }
    private void BtnCancel_Click(object sender, EventArgs e)
    {
            
    }

    // GRID CONTROL
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
            AddControlsInDataGrid();
            EnableRGridControl(true);
            ClearCurrencyDetails();
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
    private void TextFromRGrid()
    {
        if (RGrid.CurrentRow == null)
        {
            return;
        }
        //TxtSno.Text = RGrid.Rows[_rowIndex].Cells["GTxtSNo"].Value.GetString();
        //TxtCurrency.Text = RGrid.Rows[_rowIndex].Cells["GTxtCurrency"].Value.GetString();
        //_currencyId = RGrid.Rows[_rowIndex].Cells["GTxtCurrencyId"].Value.GetInt();
        //TxtAmount.Text = RGrid.Rows[_rowIndex].Cells["GTxtAmount"].Value.GetDecimalString();
        //TxtBuyRate.Text = RGrid.Rows[_rowIndex].Cells["GTxtBuyRate"].Value.GetDecimalString();
        //TxtSalesRate.Text = RGrid.Rows[_rowIndex].Cells["GTxtSalesRate"].Value.GetDecimalString();
        //TxtNarration.Text = RGrid.Rows[_rowIndex].Cells["GTxtNarration"].Value.GetString();
        _isRowUpdate = true;
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
        AddControlsInDataGrid();
    }
    private void TxtShortName_KeyDown(object sender, KeyEventArgs e)
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
            var (description, id, currencyRate) = GetMasterList.GetCurrencyList("NEW");
            if (id > 0)
            {
                SetCurrencyInfo(id);
            }
            TxtShortName.Focus();
        }
        else
        {
            ClsKeyPreview.KeyEvent(e, "DELETE", TxtShortName, OpenRCurrency);
        }
    }
    private void TxtBuyRate_TextChanged(object sender, EventArgs e)
    {
        TxtNetAmount.Text = (TxtBuyRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
    }
    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
        TxtNetAmount.Text = (TxtBuyRate.GetDecimal() * TxtQty.GetDecimal()).GetDecimalString();
    }

    // METHOD FOR THIS FORM
    private void GetRSerialNo()
    {
        for (var i = 0; i < RGrid.RowCount; i++)
        {
            TxtSno.Text = (i + 1).ToString();
            RGrid.Rows[i].Cells["GTxtSNo"].Value = TxtSno.Text;
        }
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
        TxtShortName = new MrGridTextBox(RGrid);
        TxtShortName.KeyDown += TxtShortName_KeyDown;

        TxtCurrency = new MrGridTextBox(RGrid)
        {
            ReadOnly = true
        };
        TxtCurrency.KeyDown += TxtShortName_KeyDown;

        TxtQty = new MrGridNumericTextBox(RGrid)
        {
            AcceptsReturn = true
        };
        TxtQty.TextChanged += TxtQty_TextChanged;


        TxtBuyRate = new MrGridNumericTextBox(RGrid)
        {
            AcceptsReturn = true
        };
        TxtBuyRate.TextChanged += TxtBuyRate_TextChanged;
        TxtBuyRate.AcceptsReturn = true;

        TxtLocalRate = new MrGridNumericTextBox(RGrid)
        {
            ReadOnly = true,
            Enabled = false
        };

        TxtNetAmount = new MrGridNumericTextBox(RGrid);
        TxtLocalNetAmount = new MrGridNumericTextBox(RGrid)
        {
            ReadOnly = true,
            Enabled = false
        };

        TxtNarration = new MrGridNormalTextBox(RGrid);


        ObjGlobal.DGridColorCombo(RGrid);
    }
    private void AddControlsInDataGrid()
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

        columnIndex = RGrid.Columns["GTxtShortName"].Index;
        TxtShortName.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtShortName.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtShortName.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtCurrency"].Index;
        TxtCurrency.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtCurrency.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtCurrency.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtQty"].Index;
        TxtQty.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtQty.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtQty.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxtBuyRate"].Index;
        TxtBuyRate.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtBuyRate.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtBuyRate.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxLocalRate"].Index;
        TxtLocalRate.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtLocalRate.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtLocalRate.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxNetAmount"].Index;
        TxtNetAmount.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNetAmount.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNetAmount.TabIndex = columnIndex;

        columnIndex = RGrid.Columns["GTxLocalNetAmount"].Index;
        TxtLocalNetAmount.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtLocalNetAmount.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtLocalNetAmount.TabIndex = columnIndex;


        columnIndex = RGrid.Columns["GTxtNarration"].Index;
        TxtNarration.Size = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Size;
        TxtNarration.Location = RGrid.GetCellDisplayRectangle(columnIndex, currentRow, true).Location;
        TxtNarration.TabIndex = columnIndex;
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
    private void SetLedgerInfo(long ledgerId)
    {
        var dtLedger = _setup.GetLedgerBalance(ledgerId, MskDate);
        if (dtLedger.Rows.Count <= 0)
        {
            return;
        }
        LblBalance.Text = dtLedger.Rows[0]["Balance"].GetDecimalString();
    }
    private void SetCurrencyInfo(int currencyId)
    {
        var masterCurrency = _currency.GetMasterCurrency(_actionTag, 1, currencyId); ;
        if (masterCurrency.Rows.Count <= 0)
        {
            return;
        }

        TxtShortName.Text = masterCurrency.Rows[0]["CCode"].GetString();
        TxtCurrency.Text = masterCurrency.Rows[0]["CCode"].GetString();
        TxtBuyRate.Text = masterCurrency.Rows[0]["CCode"].GetString();
    }
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

        ClearCurrencyDetails();
    }
    private void ClearCurrencyDetails()
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
        TxtNarration.Clear();
        VoucherTotalCalculation();
    }
    private void EnableRGridControl(bool isEnable = false)
    {
        TxtSno.Enabled = false;
        TxtSno.Visible = isEnable;

        TxtShortName.Enabled = TxtShortName.Visible = isEnable;
        TxtCurrency.Enabled = TxtCurrency.Visible = isEnable;

        TxtQty.Enabled = TxtQty.Visible = isEnable;

        TxtBuyRate.Enabled = TxtBuyRate.Visible = isEnable;

        TxtNarration.Enabled = TxtNarration.Visible = isEnable;
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


    // OBJECT FOR THE FORM
    private readonly IFinanceEntry _entry;
    private readonly IMasterSetup _setup;
    private readonly ICurrencyRepository _currency;
    private readonly IFinanceDesign _voucher;

    private int _rowIndex;
    private int _columnIndex;
    private int _currencyId = ObjGlobal.SysCurrencyId;

    private long _ledgerId;

    private bool _isRowDelete;
    private bool _isRowUpdate;
    private bool _zoom;

    private readonly string[] _tagStrings = ["DELETE", "REVERSE"];
    private string _actionTag = string.Empty;
    private string _docDesc = string.Empty;
    private string _voucherType;
    private string _zoomVno;

    // GRID CONTROL
    private MrGridNumericTextBox TxtSno { get; set; }
    private MrGridTextBox TxtShortName { get; set; }
    private MrGridTextBox TxtCurrency { get; set; }
    private MrGridNumericTextBox TxtQty { get; set; }
    private MrGridNumericTextBox TxtBuyRate { get; set; }
    private MrGridNumericTextBox TxtLocalRate { get; set; }
    private MrGridNumericTextBox TxtNetAmount { get; set; }
    private MrGridNumericTextBox TxtLocalNetAmount { get; set; }
    private MrGridNormalTextBox TxtNarration { get; set; }

    private void BtnVendor_Click(object sender, EventArgs e)
    {

    }

        
}