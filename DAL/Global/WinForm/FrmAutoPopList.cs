using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace MrDAL.Global.WinForm;

public partial class FrmAutoPopList : Form
{
    // AUTO POP LIST FORM
    #region --------------- FrmAutoPopList ---------------
    public FrmAutoPopList(DataTable dtProduct)
    {
        InitializeComponent();
        OpenMode = "MED";
        ReturnProductFromTable(dtProduct);
    }

    public FrmAutoPopList(string query)
    {
        InitializeComponent();
        OpenMode = "MED";
        ReturnDataFromQueryTable(query);
    }

    public FrmAutoPopList(string[] inputValue) : this(inputValue[0], inputValue[1], inputValue[2], inputValue[3], inputValue[4], inputValue[5], string.Empty, DateTime.Now.GetDateString(), true)
    {

    }

    public FrmAutoPopList(string openMode, string listType, string category, bool isActive = true)
    {
        InitializeComponent();
        RGrid.RowHeadersWidth = 20;
        OpenMode = openMode;
        ActionTag = "SAVE";
        Category = category;
        LoginDate = DateTime.Now.GetDateString();
        ListType = listType;
        IsActive = isActive;
        if (listType.IsBlankOrEmpty())
        {
            return;
        }
        RGrid.SuspendLayout();

        if (listType == "ClientCollection")
        {
            ListClientCollection(category, isActive);
            SearchCol = "Description";
            _mSearchCol = ["Description", "PanNo"];
        }
        else if (listType is "ClientSource")
        {
            ListClientSource(category, isActive);
            SearchCol = "Description";
            _mSearchCol = ["Description"];
        }
        var gridViewColumns = RGrid.Columns.Cast<DataGridViewColumn>().ToList();
        gridViewColumns.ForEach(f => f.SortMode = DataGridViewColumnSortMode.Automatic);
        RGrid.ResumeLayout();
        TxtSearch.Text = ObjGlobal.SearchText.Trim();
        TxtSearch.Focus();
    }

    public FrmAutoPopList(string sizeMode, string listType, string action, string searchKey, string category, string moduleName) : this(sizeMode, listType, searchKey, action, category, moduleName, string.Empty, DateTime.Now.GetDateString(), true)
    {
    }

    public FrmAutoPopList(string sizeMode, string listType, string searchKey, string action, string category, string moduleName, string reportMode) : this(sizeMode, listType, searchKey, action, category, moduleName, reportMode, DateTime.Now.GetDateString(), true)
    {
    }

    public FrmAutoPopList(string sizeMode, string listType, string searchKey, string action, string category, string moduleName, string reportMode, bool status = false, bool isView = false) : this(sizeMode, listType, searchKey, action, category, moduleName, reportMode, DateTime.Now.ToString(), status, isView)
    {
    }

    public FrmAutoPopList(string sizeMode, string listType, string searchKey, string action, string category, string moduleName, string reportMode = "", string loginDate = "", bool status = false, bool isView = false)
    {
        InitializeComponent();
        RGrid.RowHeadersWidth = 20;
        OpenMode = sizeMode;
        if (string.IsNullOrEmpty(loginDate))
        {
            loginDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        ActionTag = action;
        Category = category;
        LoginDate = loginDate;
        ListType = listType;
        ModuleName = moduleName;
        IsActive = status;
        RGrid.SuspendLayout();
        switch (ModuleName.ToUpper())
        {
            case "MASTER":
                {
                    GetTransactionMasterList(listType);
                    break;
                }
            case "TRANSACTION":
                {
                    GetTransactionList(listType);
                    break;
                }
            case "LIST":
                {
                    GetListOfMaster(listType);
                    break;
                }
            case "HOS_MASTER":
                {
                    GetListOfHospitalMaster(listType);
                    break;
                }
            case "DETAILS":
                {
                    GetListOfMasters(listType);
                    break;
                }
        }

        typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, RGrid, [true]);
        RGrid.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.Automatic);
        RGrid.ResumeLayout();
        TxtSearch.Text = ObjGlobal.SearchText.Trim();
        RGrid.DataBindingComplete += OnDataBindingComplete;
        RGrid.CellFormatting += RGrid_CellFormatting;
        RGrid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        TxtSearch.Focus();
    }
    #endregion --------------- FrmAutoPopList ---------------

    // FORM EVENTS FOR THIS FORM
    #region --------------- Form ---------------
    private void FrmAutoPopList_Shown(object sender, EventArgs e)
    {
        Size = OpenMode switch
        {
            "MIN" => new Size(792, 521),
            "MED" => new Size(1100, 521),
            "MAX" => new Size(1366, 521),
            _ => new Size(662, 470)
        };
        if (RGrid.RowCount > 1)
        {
            RGrid.Columns[0].Visible = false;
        }
        CenterToScreen();
        RGrid.Focus();
        TxtSearch.Focus();
        TxtSearch.Select(TxtSearch.Text.Length, 2);
        TxtSearch.SelectionStart = 1;
        if (ObjGlobal.SearchText.IsValueExits())
        {
            RGridSearch(ObjGlobal.SearchText);
        }
    }

    private void PickList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            BtnCancel.PerformClick();
        }
        else if (e.KeyChar is (char)Keys.Enter)
        {
            e.Handled = false;
        }
        else
        {
            if (ActiveControl.Name == "TxtSearch")
                return;
            TxtSearch.Text += e.KeyChar.ToString();
            TxtSearch.Focus();
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData is Keys.Up or Keys.Down)
        {
            if (keyData == Keys.Up)
            {
                RGrid.CurrentCell = RowIndex switch
                {
                    // Check not already at the first row in the grid before moving up one row
                    > 0 => RGrid.Rows[RowIndex - 1].Cells[RGrid.CurrentCell.ColumnIndex],
                    _ => RGrid.CurrentCell
                };
                return true;
            }

            if (true)
            {
                // Check not already at the last row in the grid before moving down one row
                if (RowIndex + 1 < RGrid.Rows.Count)
                    RGrid.CurrentCell = RGrid.Rows[RowIndex + 1].Cells[RGrid.CurrentCell.ColumnIndex];

                return true;
            }
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }
    #endregion --------------- Form ---------------

    // EVENT CLICK FRO THIS FORM
    #region --------------- Event ---------------
    private void TxtSearch_TextChanged(object sender, EventArgs e)
    {
        RGridSearch(TxtSearch.Text);
        TxtSearch.SelectionStart = TxtSearch.Text.Length;
        TxtSearch.Focus();
    }

    private void RGridSearch(string searchText)
    {
        if (searchText != null && searchText != "F1")
            try
            {
                TxtGridSearch(TxtSearch.Text);
            }
            catch
            {
                // ignored
            }

        //ObjGlobal.DGridColorCombo(RGrid);
    }

    private void TxtGridSearch(string searchText)
    {
        var bs = new BindingSource();
        try
        {
            TxtSearch.Text = searchText;
            bs.DataSource = RGrid.DataSource;
            if (_isFirstSearch)
            {
                bs.DataSource = GetListTable;
                _isFirstSearch = false;
            }

            if (_mSearchCol == null || _mSearchCol.Length == 0)
                _mSearchCol = [SearchCol];
            bs.Filter = ObjGlobal.SysIsAlphaSearch switch
            {
                true => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}'",
                _ => _mSearchCol.Length switch
                {
                    2 => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[1]}, 'System.String') like '%{TxtSearch.Text}%'",
                    3 => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[1]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[2]}, 'System.String') like '%{TxtSearch.Text}%'",
                    4 => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[1]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[2]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[3]}, 'System.String') like '%{TxtSearch.Text}%'",
                    5 => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[1]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[2]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[3]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[4]}, 'System.String') like '%{TxtSearch.Text}%'",
                    6 => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[1]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[2]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[3]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[4]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[5]}, 'System.String') like '%{TxtSearch.Text}%'",
                    7 => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[1]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[2]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[3]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[4]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[5]}, 'System.String') like '%{TxtSearch.Text}%' or Convert({_mSearchCol[6]}, 'System.String') like '%{TxtSearch.Text}%'",
                    _ => $"Convert({_mSearchCol[0]}, 'System.String') like '%{TxtSearch.Text}%'"
                }
            };
            RGrid.DataSource = bs.DataSource;
            if (RGrid.RowCount != 0)
            {
                return;
            }

            bs.DataSource = GetListTable;
            RGrid.DataSource = bs.DataSource;
        }
        catch
        {
            // IGNORE
        }
    }
    #endregion --------------- Event ---------------

    // BUTTON CLICK EVENTS
    #region --------------- Button ---------------
    private void BtnCancel_Click(object sender, EventArgs e) { Close(); }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (RGrid.CurrentRow != null)
                SelectRowValue();
        }
        catch
        {
            // ignored
        }

        Close();
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            SelectRowValue();
        else if (e.KeyCode == Keys.Escape)
            BtnCancel.PerformClick();
    }

    private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { BtnOk.PerformClick(); }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e) { RowIndex = e.RowIndex; }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e) { ColIndex = e.ColumnIndex; }

    private void RGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        //var row = RGrid.Rows[e.RowIndex];
        //var colName = RGrid.Columns[e.ColumnIndex].Name;
        //if (row.Cells[e.ColumnIndex].ValueType == typeof(decimal) && row.Cells[e.ColumnIndex].Value.GetDecimal() == 0)
        //{
        //    e.Value = string.Empty;
        //    e.FormattingApplied = true;
        //}
        //if (colName != "IsGroup")
        //{
        //    return;
        //}

        //row.DefaultCellStyle.ForeColor = row.Cells["IsGroup"].Value.GetInt() switch
        //{
        //    1 or 11 => Color.Blue,
        //    2 or 22 => Color.BlueViolet,
        //    3 or 33 => Color.IndianRed,
        //    12 => Color.Red,
        //    _ => Color.Black
        //};

        //row.DefaultCellStyle.Font = row.Cells["IsGroup"].Value.GetInt() switch
        //{
        //    11 or 22 or 33 or 99 => new Font("Bookman Old Style", 11, FontStyle.Bold),
        //    111 => new Font("Bookman Old Style", 14, FontStyle.Bold),
        //    88 => new Font("Bookman Old Style", 11, FontStyle.Italic),
        //    _ => new Font("Bookman Old Style", 10, FontStyle.Regular)
        //};

        //var result = row.DefaultCellStyle.Alignment;
        //row.DefaultCellStyle.Alignment = row.Cells["IsGroup"].Value.GetInt() switch
        //{
        //    11 or 13 or 22 or 33 or 88 or 99 => DataGridViewContentAlignment.MiddleRight,
        //    111 => DataGridViewContentAlignment.MiddleCenter,
        //    _ => DataGridViewContentAlignment.NotSet
        //};
    }

    private void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        //foreach (DataGridViewColumn column in RGrid.Columns)
        //{
        //    var col = column.Name;
        //    if (column.ValueType == typeof(decimal))
        //    {
        //        column.DefaultCellStyle.Format = "N2";
        //    }
        //}
    }

    private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (RGrid.CurrentCell is { Selected: true } && e.KeyChar is (char)Keys.Enter)
            BtnOk.PerformClick();
    }
    #endregion --------------- Button ---------------

    // METHOD FOR THIS FORM
    #region --------------- Method ---------------
    private DataTable GetDataTable()
    {
        var dtLocal = new DataTable();
        try
        {
            dtLocal = GetListTable.Clone();
            var colName = RGrid.Columns[RGrid.CurrentCell.ColumnIndex].Name;
            foreach (DataGridViewRow dr in RGrid.Rows)
            {
                var drLocal = dtLocal.NewRow();
                for (var i = 0; i < RGrid.Columns.Count; i++)
                {
                    var item = RGrid.Columns[i].DataPropertyName;
                    drLocal[item] = dr.Cells[i].Value;
                }

                dtLocal.Rows.Add(drLocal);
            }

            return dtLocal;
        }
        catch (Exception ex)
        {
            var exMessage = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return dtLocal;
        }
    }

    private void GetTransactionMasterList(string listType)
    {
        switch (listType.ToUpper())
        {
            case "GL":
            case "GENERALLEDGER":
            case "GENERAL LEDGER":
                {
                    GetGeneralLedgerListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "PanNo"];
                    break;
                }
            case "PGL":
            case "PARTYLEDGER":
            case "PARTY LEDGER":
                {
                    GetPartyLedgerListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "PanNo"];
                    break;
                }
            case "SL":
            case "SUBLEDGER":
            case "SUB LEDGER":
                {
                    GetSubLedgerListForTransaction();
                    SearchCol = "Description";
                    break;
                }
            case "OGL":
            case "OPENINGLEDGER":
            case "OPENING LEDGER":
                {
                    GetGeneralLedgerForOpening();
                    SearchCol = "Description";
                    break;
                }
            case "P":
            case "PRODUCT":
                {
                    GetProductListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "HsCode", "ShortName", "Barcode", "Barcode1", "Barcode2", "Barcode3"];
                    break;
                }
            case "FINISEDPRODUCT":
            case "FPRODUCT":
                {
                    GetFinishedProductListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "RAWPRODUCT":
            case "RPRODUCT":
                {
                    GetRawProductListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "RP":
            case "RESTROPRODUCT":
            case "RESTRO PRODUCT":
                {
                    GetRestaurantProductListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "VEHICLE":
                {
                    GetVehicleListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
        }
    }

    private void GetTransactionList(string listType)
    {
        #region **---------- FINANCE  ----------**
        if (listType == null)
            return;
        switch (listType.ToUpper())
        {
            case "LOB":
                {
                    GetLedgerOpeningVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "GLName"];
                    break;
                }
            case "CB":
                {
                    GetFinanceTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "LedgerDesc"];
                    break;
                }
            case "JV":
                {
                    GetFinanceTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "LedgerDesc"];
                    break;
                }
            case "CN":
                {
                    GetFinanceTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "LedgerDesc"];
                    break;
                }
            case "DN":
                {
                    GetFinanceTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "LedgerDesc"];
                    break;
                }
            case "PC":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "PCR":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "PO":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "PB":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "PR":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "SO" or "RSO":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "SC":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "SB" or "POS" or "RSB":

                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "SR":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "TSB":
                {
                    GetPurchaseSalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
            case "PDC":
                {
                    GetPdcVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "GLName"];
                    break;
                }
            case "POP":
                {
                    GetProductOpeningVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "PName", "VoucherDate"];
                    break;
                }
            case "BOM":
                {
                    GetBomVoucherList();
                    SearchCol = "PName";
                    _mSearchCol = ["VoucherNo", "PName"];
                    break;
                }
            case "IBOM":
                {
                    GetInventoryBomVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "PName"];
                    break;
                }
            case "SA":
                {
                    GetStockVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "PName"];
                    break;
                }
            case "TSALES":
            case "TODAYSALES":
                {
                    GetTodaySalesTransactionVoucherList();
                    SearchCol = "VoucherNo";
                    _mSearchCol = ["VoucherNo", "Ledger", "TPAN"];
                    break;
                }
        }
        #endregion **---------- FINANCE  ----------**
    }

    private void GetListOfMaster(string listType)
    {
        PanelHeader.Visible = true;
        if (listType == null)
        {
            return;
        }
        switch (listType.ToUpper())
        {
            case "COMPANY":
            case "GC":
                {
                    GetCompanyList();
                    SearchCol = "Description";
                    break;
                }
            case "BRANCH":
            case "B":
                {
                    GetCommonMasterList();
                    SearchCol = "ValueName";
                    break;
                }
            case "FISCALYEAR":
            case "F":
                {
                    GetCommonMasterList();
                    SearchCol = "ValueName";
                    break;
                }

            case "AG":
            case "ACCOUNTGROUP":
            case "ACCOUNT GROUP":
                {
                    GetAccountGroupList(0);
                    SearchCol = "Description";
                    break;
                }
            case "SECONDARY_ACCOUNT_GROUP":
                {
                    GetAccountGroupList(1);
                    SearchCol = "Description";
                    break;
                }
            case "ASG":
            case "ACCOUNTSUBGROUP":
            case "ACCOUNT SUB GROUP":
                {
                    GetAccountSubGroupList(0, listType.ToUpper());
                    SearchCol = "Description";
                    break;
                }
            case "ACCOUNTSUBGROUPLIST":
                {
                    GetAccountSubGroupList(0, listType.ToUpper());
                    SearchCol = "Description";
                    break;
                }
            case "GL":
            case "GENERAL LEDGER":
            case "GENERALLEDGER":
                {
                    GetGeneralLedgerList();
                    SearchCol = "Description";
                    break;
                }
            case "SL":
            case "SUBLEDGER":
            case "SUB LEDGER":
                {
                    GetSubLedgerList();
                    SearchCol = "Description";
                    break;
                }
            case "MA":
            case "MAINAREA":
            case "MAIN AREA":
                {
                    GetMainAreaList();
                    SearchCol = "Description";
                    break;
                }
            case "A":
            case "AREA":
                {
                    GetAreaList();
                    SearchCol = "Description";
                    break;
                }
            case "MS":
            case "MAINAGENT":
            case "MAIN AGENT":
                {
                    GetMainAgentList();
                    SearchCol = "Description";
                    break;
                }

            case "JA":
            case "JUNIORAGENT":
            case "JUNIOR AGENT":
            case "AGENT":
                {
                    GetAgentList();
                    SearchCol = "Description";
                    break;
                }
            case "PATIENT":
                {
                    GetPatientList();
                    _mSearchCol = ["Description", "ShortName", "ContactNo"];
                    break;
                }
            case "MP":
            case "MASTER PRODUCT":
            case "MASTERPRODUCT":
                {
                    GetProductList();
                    SearchCol = "Description";
                    break;
                }
            case "PG":
            case "PRODUCT GROUP":
            case "PRODUCTGROUP":
                {
                    GetProductGroupList();
                    SearchCol = "Description";
                    break;
                }
            case "PSG":
            case "PRODUCT SUB GROUP":
            case "PRODUCTSUBGROUP":
                {
                    GetProductSubGroupList();
                    SearchCol = "Description";
                    break;
                }

            case "P":
            case "PRODUCT":
                {
                    GetProductListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case @"COUNTERPRODUCT":
            case @"COUNTER_PRODUCT":
            case @"C_PRODUCT":
            case @"POS_PRODUCT":
                {
                    GetProductList();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "PU":
            case "PRODUCTUNIT":
            case "PRODUCT UNIT":
                {
                    GetProductUnitList();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "PRODUCTBATCH":
                {
                    GetProductBatchList();
                    SearchCol = "Description";
                    _mSearchCol = ["Description"];
                    break;
                }
            case "PRINTER":
                {
                    GetPrinterInstallList();
                    SearchCol = "Description";
                    break;
                }
            case "DESIGN":
                {
                    GetPrintDesignList();
                    SearchCol = "Description";
                    break;
                }
            case "GODOWN":
                {
                    GetGodownList();
                    SearchCol = "Description";
                    break;
                }
            case "RACK":
                {
                    GetRackList();
                    SearchCol = "Description";
                    break;
                }
            case "CC":
            case "COSTCENTER":
            case "COST CENTER":
                {
                    GetCostCenterList();
                    SearchCol = "Description";
                    break;
                }
            case "C":
            case "CURRENCY":
                {
                    GetCurrencyList();
                    SearchCol = "ShortName";
                    break;
                }
            case "D":
            case "DEPARTMENT":
                {
                    GetDepartmentList();
                    SearchCol = "Description";
                    break;
                }
            case "DR":
            case "DOCTOR":
                {
                    GetDoctorList();
                    SearchCol = "Description";
                    break;
                }
            case "HD":
            case "HDEPARTMENT":
                {
                    GetHospitalDepartmentList();
                    SearchCol = "Description";
                    break;
                }
            case "DN":
            case "DOCUMENTNUMBERING":
            case "DOCUMENT NUMBERING":
                {
                    GetDocumentNumberingList();
                    SearchCol = "Description";
                    break;
                }
            case "DS":
            case "DOCUMENTSCHEMA":
            case "DOCUMENT SCHEMA":
                {
                    GetMasterDocumentScheme();
                    SearchCol = "Description";
                    break;
                }
            case "VN":
            case "VEHICLENUMBER":
            case "VEHICLE NUMBER":
                {
                    GetVehicleColorsList();
                    SearchCol = "Description";
                    break;
                }
            case "VC":
            case "VEHICLECOLOR":
            case "VEHICLE COLOR":
                {
                    GetVehicleColorsList();
                    SearchCol = "Description";
                    break;
                }
            case "VM":
            case "VEHICLEMODEL":
            case "VEHICLE MODEL":
                {
                    GetVehicleModelList();
                    SearchCol = "Description";
                    break;
                }
            case "MV":
            case "MASTERVEHICLE":
            case "MASTER VEHICLE":
                {
                    GetVehicleListForTransaction();
                    SearchCol = "Description";
                    break;
                }
            case "COUNTER":
                {
                    GetCounterList();
                    SearchCol = "Description";
                    break;
                }
            case "PT":
            case "PURCHASETERM":
            case "PURCHASE TERM":
                {
                    GetBillingTermList();
                    SearchCol = "Description";
                    break;
                }
            case "ST":
            case "SALESTERM":
            case "SALES TERM":
                {
                    GetBillingTermList();
                    SearchCol = "Description";
                    break;
                }
            case "FLOOR":
                {
                    GetFloorList();
                    SearchCol = "Description";
                    break;
                }
            case "TABLE":
                {
                    GetTableList();
                    SearchCol = "TableName";
                    break;
                }
            case "NRMASTER":
                {
                    GetNarrationRemarksList();
                    SearchCol = "NRDESC";
                    break;
                }
            case "MEMBERSHIP":
                {
                    GetMemberShipList();
                    SearchCol = "Description";
                    break;
                }

            case "BRLOG":
                {
                    GetBackupRestoreList();
                    SearchCol = "DATABASE_NAME";
                    break;
                }
            case "MEMBERTYPE":
                {
                    GetMemberTypeList();
                    SearchCol = "Description";
                    break;
                }
            case "USER_ROLE":
                {
                    GetUserRoleList();
                    SearchCol = "Description";
                    break;
                }
            case "USER":
                {
                    GetUserInfoList();
                    SearchCol = "User_Name";
                    break;
                }
            case "PGL":
            case "PARTYLEDGER":
            case "PARTY LEDGER":
                {
                    GetPartyLedgerListForTransaction();
                    SearchCol = "Description";
                    break;
                }

            case "CUNIT":
                {
                    GetCompanyUnitList();
                    SearchCol = "CmpUnit_Name";
                    break;
                }
            case "SCHEME":
                {
                    GetProductSchemeList();
                    SearchCol = "SchemeDesc";
                    break;
                }
            case "PUBLICATION":
                {
                    GetProductPublicationList();
                    SearchCol = "Description";
                    break;
                }
            case "AUTHOR":
                {
                    GetProductAuthorList();
                    SearchCol = "Description";
                    break;
                }
            case "GIFT VOUCHER":
                {
                    GetGiftVoucherList();
                    _mSearchCol = ["Description", "ShortName"];
                    SearchCol = "Description";
                    break;
                }
        }
    }

    private void GetListOfMasters(string listType)
    {
        PanelHeader.Visible = false;
        if (listType == null)
        {
            return;
        }
        switch (listType.ToUpper())
        {
            case "BRANCH":
            case "B":
                {
                    GetCommonMasterList();
                    SearchCol = "ValueName";
                    break;
                }

            case "AG":
            case "ACCOUNTGROUP":
            case "ACCOUNT GROUP":
                {
                    GetAccountGroupList(0);
                    SearchCol = "Description";
                    break;
                }
            case "SECONDARY_ACCOUNT_GROUP":
                {
                    GetAccountGroupList(1);
                    SearchCol = "Description";
                    break;
                }
            case "ASG":
            case "ACCOUNTSUBGROUP":
            case "ACCOUNT SUB GROUP":
                {
                    GetAccountSubGroupList(0, listType.ToUpper());
                    SearchCol = "Description";
                    break;
                }
            case "ACCOUNTSUBGROUPLIST":
                {
                    GetAccountSubGroupList(0, listType.ToUpper());
                    SearchCol = "Description";
                    break;
                }
            case "GL":
            case "GENERAL LEDGER":
            case "GENERALLEDGER":
                {
                    GetGeneralLedgerList();
                    SearchCol = "Description";
                    break;
                }
            case "SL":
            case "SUBLEDGER":
            case "SUB LEDGER":
                {
                    GetSubLedgerList();
                    SearchCol = "Description";
                    break;
                }
            case "MA":
            case "MAINAREA":
            case "MAIN AREA":
                {
                    GetMainAreaList();
                    SearchCol = "Description";
                    break;
                }
            case "A":
            case "AREA":
                {
                    GetAreaList();
                    SearchCol = "Description";
                    break;
                }
            case "MS":
            case "MAINAGENT":
            case "MAIN AGENT":
                {
                    GetMainAgentList();
                    SearchCol = "Description";
                    break;
                }

            case "JA":
            case "JUNIORAGENT":
            case "JUNIOR AGENT":
            case "AGENT":
                {
                    GetAgentList();
                    SearchCol = "Description";
                    break;
                }
            case "PATIENT":
                {
                    GetPatientList();
                    _mSearchCol = ["Description", "ShortName", "ContactNo"];
                    break;
                }
            case "MP":
            case "MASTER PRODUCT":
            case "MASTERPRODUCT":
                {
                    GetProductList();
                    SearchCol = "Description";
                    break;
                }
            case "PG":
            case "PRODUCT GROUP":
            case "PRODUCTGROUP":
                {
                    GetProductGroupList();
                    SearchCol = "Description";
                    break;
                }
            case "PSG":
            case "PRODUCT SUB GROUP":
            case "PRODUCTSUBGROUP":
                {
                    GetProductSubGroupLists();
                    SearchCol = "Description";
                    break;
                }

            case "P":
            case "PRODUCT":
                {
                    GetProductListForTransaction();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case @"COUNTERPRODUCT":
            case @"COUNTER_PRODUCT":
            case @"C_PRODUCT":
            case @"POS_PRODUCT":
                {
                    GetProductList();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "PU":
            case "PRODUCTUNIT":
            case "PRODUCT UNIT":
                {
                    GetProductUnitList();
                    SearchCol = "Description";
                    _mSearchCol = ["Description", "ShortName"];
                    break;
                }
            case "PRODUCTBATCH":
                {
                    GetProductBatchList();
                    SearchCol = "Description";
                    _mSearchCol = ["Description"];
                    break;
                }
            case "PRINTER":
                {
                    GetPrinterInstallList();
                    SearchCol = "Description";
                    break;
                }
            case "DESIGN":
                {
                    GetPrintDesignList();
                    SearchCol = "Description";
                    break;
                }
            case "GODOWN":
                {
                    GetGodownList();
                    SearchCol = "Description";
                    break;
                }
            case "RACK":
                {
                    GetRackList();
                    SearchCol = "Description";
                    break;
                }
            case "CC":
            case "COSTCENTER":
            case "COST CENTER":
                {
                    GetCostCenterList();
                    SearchCol = "Description";
                    break;
                }
            case "C":
            case "CURRENCY":
                {
                    GetCurrencyList();
                    SearchCol = "ShortName";
                    break;
                }
            case "D":
            case "DEPARTMENT":
                {
                    GetDepartmentList();
                    SearchCol = "Description";
                    break;
                }
            case "DR":
            case "DOCTOR":
                {
                    GetDoctorList();
                    SearchCol = "Description";
                    break;
                }
            case "HD":
            case "HDEPARTMENT":
                {
                    GetHospitalDepartmentList();
                    SearchCol = "Description";
                    break;
                }
            case "DN":
            case "DOCUMENTNUMBERING":
            case "DOCUMENT NUMBERING":
                {
                    GetDocumentNumberingList();
                    SearchCol = "Description";
                    break;
                }
            case "DS":
            case "DOCUMENTSCHEMA":
            case "DOCUMENT SCHEMA":
                {
                    GetMasterDocumentScheme();
                    SearchCol = "Description";
                    break;
                }
            case "VN":
            case "VEHICLENUMBER":
            case "VEHICLE NUMBER":
                {
                    GetVehicleColorsList();
                    SearchCol = "Description";
                    break;
                }
            case "VC":
            case "VEHICLECOLOR":
            case "VEHICLE COLOR":
                {
                    GetVehicleColorsList();
                    SearchCol = "Description";
                    break;
                }
            case "VM":
            case "VEHICLEMODEL":
            case "VEHICLE MODEL":
                {
                    GetVehicleModelList();
                    SearchCol = "Description";
                    break;
                }
            case "MV":
            case "MASTERVEHICLE":
            case "MASTER VEHICLE":
                {
                    GetVehicleListForTransaction();
                    SearchCol = "Description";
                    break;
                }
            case "COUNTER":
                {
                    GetCounterList();
                    SearchCol = "Description";
                    break;
                }
            case "PT":
            case "PURCHASETERM":
            case "PURCHASE TERM":
                {
                    GetBillingTermList();
                    SearchCol = "Description";
                    break;
                }
            case "ST":
            case "SALESTERM":
            case "SALES TERM":
                {
                    GetBillingTermList();
                    SearchCol = "Description";
                    break;
                }
            case "FLOOR":
                {
                    GetFloorList();
                    SearchCol = "Description";
                    break;
                }
            case "TABLE":
                {
                    GetTableList();
                    SearchCol = "TableName";
                    break;
                }
            case "NRMASTER":
                {
                    GetNarrationRemarksList();
                    SearchCol = "NRDESC";
                    break;
                }
            case "MEMBERSHIP":
                {
                    GetMemberShipList();
                    SearchCol = "Description";
                    break;
                }

            case "BRLOG":
                {
                    GetBackupRestoreList();
                    SearchCol = "DATABASE_NAME";
                    break;
                }
            case "MEMBERTYPE":
                {
                    GetMemberTypeList();
                    SearchCol = "Description";
                    break;
                }
            case "USER_ROLE":
                {
                    GetUserRoleList();
                    SearchCol = "Description";
                    break;
                }
            case "USER":
                {
                    GetUserInfoList();
                    SearchCol = "User_Name";
                    break;
                }
            case "PGL":
            case "PARTYLEDGER":
            case "PARTY LEDGER":
                {
                    GetPartyLedgerListForTransaction();
                    SearchCol = "Description";
                    break;
                }

            case "CUNIT":
                {
                    GetCompanyUnitList();
                    SearchCol = "CmpUnit_Name";
                    break;
                }
            case "SCHEME":
                {
                    GetProductSchemeList();
                    SearchCol = "SchemeDesc";
                    break;
                }
            case "PUBLICATION":
                {
                    GetProductPublicationList();
                    SearchCol = "Description";
                    break;
                }
            case "AUTHOR":
                {
                    GetProductAuthorList();
                    SearchCol = "Description";
                    break;
                }
            case "GIFT VOUCHER":
                {
                    GetGiftVoucherList();
                    _mSearchCol = ["Description", "ShortName"];
                    SearchCol = "Description";
                    break;
                }
        }
    }

    private void GetListOfHospitalMaster(string listType)
    {
        if (listType == null)
            return;
        switch (listType.ToUpper())
        {
            case "DOCTOR_TYPE":
                {
                    GetDoctorTypeMasterList();
                    _mSearchCol = ["TypeDescription", "TypeShortName"];
                    SearchCol = "TypeDescription";
                    break;
                }
        }
    }

    private void SelectRowValue()
    {
        SelectedList.Clear();
        using var dt1 = GetDataTable();
        if (RGrid.CurrentRow != null)
        {
            var index = RGrid.CurrentRow.Index;
            SelectedList.Add(dt1.Rows[index]);
        }

        Close();
    }
    #endregion --------------- Method ---------------

    // LIST OF MASTER LIST
    #region -------------- List of Master --------------

    // COMPANY INFO MASTER LIST
    private void GetCommonMasterList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("ValueId", "ValueId", "ValueId", 0, 2, false);
        RGrid.AddColumn("ValueName", "DESCRIPTION", "ValueName", 325, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 120, 2, true);
        GetListTable = ListType.ToUpper() switch
        {
            "BRANCH" => _objPickList.GetBranchList(),
            "COMPANY_UNIT" => _objPickList.GetCompanyUnitList(),
            "FISCALYEAR" => _objPickList.GetCompanyUnitList(),
            _ => GetListTable
        };
        RGrid.DataSource = GetListTable;
    }

    private void GetCompanyList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "CompanyId", "CompanyId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "CompanyName", 325, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetCompanyList();
        RGrid.DataSource = GetListTable;
    }

    private void GetBackupRestoreList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("DATABASE_NAME", "DATABASE_NAME", "DATABASE_NAME", 150, 2, true);
        RGrid.AddColumn("DATABASE_NAME", "COMPANY", "DATABASE_NAME", 325, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PATH", "PATH", "PATH", 225, 2, true);
        RGrid.AddColumn("LOGIN_USER", "LOGIN_USER", "LOGIN_USER", 150, 2, true);
        RGrid.AddColumn("DATE", "DATE", "DATE", 150, 2, true);
        RGrid.AddColumn("ACTION_TIMES", "ACTION_TIMES", "ACTION_TIMES", 150, 2, true);
        RGrid.AddColumn("ACTION_TAKEN", "ACTION_TAKEN", "ACTION_TAKEN", 150, 2, true);
        GetListTable = _objPickList.GetBackupRestoreLogList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetUserRoleList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 325, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("Status", "STATUS", "status", 325, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetUserRole(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetUserInfoList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("User_Id", "User_Id", "User_Id", 0, 2, false);
        RGrid.AddColumn("User_Name", "DESCRIPTION", "User_Name", 325, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("Full_Name", "USER_FULL_NAME", "Full_Name", 250, 2, true);
        RGrid.AddColumn("Mobile_No", "NUMBER", "Mobile_No", 250, 2, true);
        GetListTable = _objPickList.GetUserList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetCompanyUnitList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("CmpUnit_ID", "CmpUnit_ID", "CmpUnit_ID", 0, 2, false);
        RGrid.AddColumn(
            "CmpUnit_Name",
            "COMPANY_DETAILS",
            "CmpUnit_Name",
            325,
            2,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("CmpUnit_Code", "COMPANY_DETAILS", "CmpUnit_Code", 120, 2, true);
        RGrid.AddColumn("Reg_Date", "REG_DATE", "Reg_Date", 120, 2, true);
        GetListTable = _objPickList.GetCompanyUnitList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductSchemeList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("SchemeId", "SchemeId", "SchemeId", 0, 2, false);
        RGrid.AddColumn("SchemeDesc", "DESCRIPTION", "SchemeDesc", 200, 150, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ValidFromMiti", "FROM", "ValidFromMiti", 100, 90, true);
        RGrid.AddColumn("ValidToMiti", "TO", "ValidToMiti", 100, 90, true);
        GetListTable = _objPickList.GetProductSchemeList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductPublicationList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            200,
            150,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetProductPublicationList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductAuthorList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            200,
            150,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetProductAuthorList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetGiftVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VoucherId", "VoucherId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("ShortName", "CARD NO", "ShortName", 150, 90, true);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            200,
            150,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ExpireDate", "EXPIRE DATE", "ExpireDate", 150, 90, true);
        RGrid.AddColumn("Amount", "LIMIT", "Amount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetGiftVoucherList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    // CHARTS OF ACCOUNT LIST
    private void GetAccountGroupList(int isPrimary)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 220, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, false);
        RGrid.AddColumn("GrpType", "AC TYPE", "GrpType", 120, 2, true);
        RGrid.AddColumn("ACGroup", "PRIMARY GROUP", "ACGroup", 220, 2, true);
        RGrid.AddColumn("Schedule", "SCHEDULE", "Schedule", 120, 2, true);
        RGrid.AutoGenerateColumns = false;
        GetListTable = isPrimary is 0
            ? _objPickList.GetAccountGroupList(ActionTag, Category, IsActive ? 1 : 0)
            : _objPickList.GetAccountGroupList(ActionTag, Category, 0, true);
        RGrid.DataSource = GetListTable;
    }

    private void GetAccountSubGroupList(int isPrimary, string listType)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 220, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ACGroup", "A/C GROUP", "ACGroup", 200, 2, true);
        GetListTable = isPrimary is 0
            ? _objPickList.GetAccountSubgroupList(ActionTag, Category, listType)
            : _objPickList.GetAccountSubgroupList(ActionTag, Category, listType, 0, true);
        RGrid.DataSource = GetListTable;
    }

    private void GetGeneralLedgerList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 450, 450, true);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("PanNo", "PAN_NO", "PanNo", 110, 110, true);
        RGrid.AddColumn("Balance", "BALANCE", "Balance", 120, 120, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("BType", string.Empty, "BType", 30, 30, true);
        RGrid.AddColumn("GLType", "CATEGORY", "GLType", 120, 120, true);
        RGrid.AddColumn("GroupDesc", "A/C GROUP", "GroupDesc", 250, 250, true);
        RGrid.AddColumn("SalesMan", "SALES MAN", "SalesMan", 250, 250, true);
        RGrid.AddColumn("Area", "Area", "AreaId", 120, 120, true);
        RGrid.AddColumn("PhoneNo", "PHONE_NO", "PhoneNo", 120, 120, true);
        RGrid.AddColumn("GLAddress", "ADDRESS", "GLAddress", 180, 180, true);
        GetListTable = _objPickList.MasterGeneralLedger(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetSubLedgerList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "SUB LEDGER ID", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 250, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 120, 2, true);
        RGrid.AddColumn("PhoneNo", "PHONE NO", "PhoneNo", 120, 2, true);
        // RGrid.AddColumn("AREA", "Area", "AreaId", 120, 2, true);
        RGrid.AddColumn("Ledger", "LEDGER", "Ledger", 300, 2, true);
        GetListTable = _objPickList.MasterSubLedger(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    // COUNTER MASTER LIST
    private void GetCounterList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 250, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("Printer", "PRINTER", "Printer", 120, 2, true);
        GetListTable = _objPickList.GetCounterList(ActionTag, IsActive);
        RGrid.DataSource = GetListTable;
    }

    private void GetFloorList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 250, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("Type", "TYPE", "Type", 120, 2, true);
        GetListTable = _objPickList.GetFloor(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetTableList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("TableId", "TableId", "TableId", 0, 2, false);
        RGrid.AddColumn("TableName", "TABLE NAME", "TableName", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("TableCode", "TABLE CODE", "TableCode", 200, 2, true);
        RGrid.AddColumn("TableStatus", "STATUS", "TableStatus", 150, 2, true);
        GetListTable = _objPickList.GetTableTm(ActionTag, Active, Category);
        RGrid.DataSource = GetListTable;
    }

    // MASTER LIST
    private void GetBillingTermList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("OrderNo", "OrderNo", "OrderNo", 100, 2, true);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("LedgerName", "LEDGER", "LedgerName", 200, 2, true);
        RGrid.AddColumn("Rate", "RATE", "Rate", 200, 2, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = ListType.Equals("SALESTERM")
            ? _objPickList.GetSalesTerm(ActionTag, Category, IsActive)
            : _objPickList.GetPurchaseTerm(ActionTag, Category, IsActive);
        RGrid.DataSource = GetListTable;
    }

    private void GetNarrationRemarksList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("NRID", "NRID", "NRID", 0, 2, false);
        RGrid.AddColumn("NRDESC", "DESCRIPTION", "NRDESC", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("NRTYPE", "TYPE", "NRTYPE", 200, 2, true);
        GetListTable = _objPickList.GetNarration(ActionTag, Active);
        RGrid.DataSource = GetListTable;
    }

    private void GetDepartmentList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 200, 2, true);
        GetListTable = _objPickList.GetDepartment(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetGodownList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("Location", "LOCATION", "Location", 200, 2, true);
        GetListTable = _objPickList.GetGodownList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetRackList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("RID", "LedgerId", "RID", 0, 2, false);
        RGrid.AddColumn("RName", "DESCRIPTION", "RName", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("RCode", "SHORTNAME", "RCode", 200, 2, true);
        RGrid.AddColumn("Location", "LOCATION", "Location", 200, 2, true);
        GetListTable = _objPickList.GetRackList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetCostCenterList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("CCId", "CCId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("CCName", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("CCcode", "SHORTNAME", "ShortName", 200, 2, true);
        GetListTable = _objPickList.GetCostCenter(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetCurrencyList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("CId", "CCId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("CName", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("CCode", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("CRate", "RATE", "CRate", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetCurrency(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    //VEHICLE MASTER LIST
    private void GetVehicleModelList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 200, 2, true);
        GetListTable = _objPickList.GetVehicleModel(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetVehicleColorsList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 200, 2, true);
        GetListTable = _objPickList.GetVehicleColor(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetVehicleNumberList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("State", "STATE", "State", 200, 2, true);
        GetListTable = _objPickList.GetVehicleNumber(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    // AGENT MASTER LIST
    private void GetMainAgentList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("SAgentId", "SAgentId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("SAgent", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("SAgentCode", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 200, 2, true);
        RGrid.AddColumn("PhoneNo", "PHONE NO", "PhoneNo", 120, 2, true);
        RGrid.AddColumn("Comm", "INCENTIVE", "Comm", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetSrAgentList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetAgentList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("AgentId", "AgentId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("AgentName", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("AgentCode", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 200, 2, true);
        RGrid.AddColumn("PhoneNo", "PHONE NO", "PhoneNo", 120, 2, true);
        RGrid.AddColumn("Commission", "INCENTIVE", "Commission", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetJrAgentList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    //AREA MASTER LIST
    private void GetMainAreaList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("MAreaId", "MAreaId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("MAreaName", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("MAreaCode", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("MCountry", "COUNTRY", "MCountry", 150, 2, true);
        GetListTable = _objPickList.GetMainAreaList(ActionTag, IsActive);
        RGrid.DataSource = GetListTable;
    }

    private void GetAreaList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 200, 2, true);
        RGrid.AddColumn("Country", "COUNTRY", "Country", 150, 2, true);
        GetListTable = _objPickList.GetAreaList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    //MEMBER SETUP LIST
    private void GetMemberShipList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LEDGER_ID", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            250,
            250,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("Ledger", "LEDGER", "Ledger", 250, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("MemberType", "MEMBER_TYPE", "MemberType", 120, 120, true);
        RGrid.AddColumn("StartDate", "START_DATE", "StartDate", 120, 120, true);
        RGrid.AddColumn("EndDate", "EXPIRE_DATE", "EndDate", 120, 120, true);
        RGrid.AddColumn("Phone", "PHONE_NO", "Phone", 120, 120, true);
        RGrid.AddColumn("Email", "EMAIL", "Email", 120, 120, true);
        GetListTable = _objPickList.GetMemberShipList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetMemberTypeList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LEDGER_ID", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            250,
            250,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("Discount", "DISCOUNT", "Discount", 120, 120, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetMemberTypeList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    //HOSPITAL MASTER LIST
    private void GetPatientList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("PaitentId", "PatientId", "LedgerId", 0, 2, false);
        if (Category.ToUpper() == "DESCRIPTION")
        {
            RGrid.AddColumn(
                "PatientDesc",
                "DESCRIPTION",
                "Description",
                450,
                250,
                true,
                DataGridViewAutoSizeColumnMode.Fill);
            RGrid.AddColumn("ShortName", "PATIENT_ID", "ShortName", 120, 120, true);
        }
        else
        {
            RGrid.AddColumn("ShortName", "PATIENT_ID", "ShortName", 120, 120, true);
            RGrid.AddColumn(
                "PatientDesc",
                "DESCRIPTION",
                "Description",
                450,
                250,
                true,
                DataGridViewAutoSizeColumnMode.Fill);
        }

        RGrid.AddColumn("TAddress", "ADDRESS", "TAddress", 120, 120, true);
        RGrid.AddColumn("AccountLedger", "LEDGER", "AccountLedger", 450, 250, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ContactNo", "PHONE_NO", "ContactNo", 120, 120, true);
        GetListTable = _objPickList.GetPatientList();
        RGrid.DataSource = GetListTable;
    }

    private void GetHospitalDepartmentList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            250,
            250,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("Doctor", "DOCTOR", "Doctor", 120, 120, true);
        RGrid.AddColumn("Rate", "RATE", "Rate", 120, 120, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetHosDepartment(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetDoctorList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            250,
            250,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("DrTypeDesc", "Dr. TYPE", "DrTypeDesc", 120, 120, true);
        RGrid.AddColumn("ContactNo", "PHONE NO", "ContactNo", 120, 120, true);
        RGrid.AddColumn("Address", "ADDRESS", "Address", 120, 120, true);
        RGrid.AddColumn("DName", "DEPARTMENT", "DName", 120, 120, true);
        GetListTable = _objPickList.GetDoctor(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetDoctorTypeMasterList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("ValueId", "ValueId", "DoctorTypeId", 0, 2, false);
        RGrid.AddColumn(
            "ValueName",
            "DESCRIPTION",
            "TypeDescription",
            325,
            2,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "TypeShortName", 120, 2, true);
        GetListTable = _objPickList.GetDoctorTypeList(ActionTag, Status);
        RGrid.DataSource = GetListTable;
    }

    //CHARTS OF PRODUCT LIST
    private void GetProductGroupList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            250,
            250,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("Margin", "MARGIN", "Margin", 120, 120, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetProductGroup(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductSubGroupList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("GrpName", "GROUP DESC", "GrpName", 200, 2, true);
        GetListTable = _objPickList.GetProductSubGroup(ActionTag, Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductSubGroupLists()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("GrpName", "GROUP DESC", "GrpName", 200, 2, true);
        GetListTable = _objPickList.GetProductSubGroups(ActionTag, Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("Pid", "Pid", "LedgerId", 0, 2, false);
        RGrid.AddColumn("PName", "DESCRIPTION", "Description", 400, 375, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PShortName", "SHORTNAME", "ShortName", 120, 2, true);
        RGrid.AddColumn("Barcode", "BARCODE", "Barcode", 120, 2, false);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 120, 2, true);
        RGrid.AddColumn("BuyRate", "BUY RATE", "BuyRate", 150, 2, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn(
            "PSalesRate",
            "SALES RATE",
            "PSalesRate",
            150,
            2,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("GrpName", "CATEGORY", "GrpName", 200, 150, true);
        RGrid.AddColumn("SubGrpName", "SUB CATEGORY", "SubGrpName", 200, 2, true);
        GetListTable = _objPickList.MasterProduct(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductUnitList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 300, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 2, true);
        GetListTable = _objPickList.GetProductUnit(ActionTag, Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductBatchList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 300, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("MFDate", "MF Date", "MFDate", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("ExpDate", "Exp Date", "ExpDate", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("Qty", "Qty", "StockQty", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("Rate", "Rate", "Rate", 120, 2, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("Days", "Days", "Days", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetProductBatch(Category.GetLong());
        RGrid.DataSource = GetListTable;
    }

    // SETUP MASTER LIST
    private void GetPrinterInstallList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("Printer", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetPrinter();
        RGrid.DataSource = GetListTable;
    }

    private void GetPrintDesignList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("Design", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "MODULE", "ShortName", 200, 2, false);
        GetListTable = _objPickList.GetPrintDesign(Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetMasterDocumentScheme()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "MODULE", "ShortName", 200, 2, false);
        GetListTable = _objPickList.GetDocumentNumberingSchema();
        RGrid.DataSource = GetListTable;
    }

    private void GetDocumentNumberingList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("Module", "MODULE", "Module", 120, 2, true);
        RGrid.AddColumn("DocStartMiti", "START DATE", "DocStartMiti", 150, 2, true);
        RGrid.AddColumn("DocEndMiti", "END DATE", "DocEndMiti", 150, 2, true);
        GetListTable = _objPickList.GetDocumentNumbering(ActionTag, Category);
        RGrid.DataSource = GetListTable;
    }
    #endregion -------------- List of Master --------------

    // TRANSACTION MASTER LIST
    #region -------------- Transaction Master --------------
    private void GetGeneralLedgerForOpening()
    {
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 0, false);
        RGrid.AddColumn("GLName", "DESCRIPTION", "Description", 450, 450, true);
        RGrid.AddColumn("GLCode", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("PanNo", "PAN_NO", "PanNo", 110, 110, true);
        RGrid.AddColumn("Balance", "BALANCE", "Balance", 120, 120, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("AmtType", string.Empty, "AmtType", 30, 30, true);
        RGrid.AddColumn("GrpName", "A/C GROUP", "GrpName", 250, 250, true);
        RGrid.AddColumn("AgentName", "SALES MAN", "AgentName", 250, 250, true);
        //RGrid.AddColumn("Area", "Area", "AreaId", 120, 2, true);
        RGrid.AddColumn("PhoneNo", "PHONE_NO", "PhoneNo", 120, 120, true);
        RGrid.AddColumn("GLAddress", "ADDRESS", "GLAddress", 180, 180, true);
        RGrid.AutoGenerateColumns = false;
        GetListTable = _objPickList.GetOpeningGeneralLedgerList(ActionTag, Category, LoginDate);
        RGrid.DataSource = GetListTable;
    }

    private void GetGeneralLedgerListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("Description", "DESCRIPTION", "Description", 450, 450, true);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("PanNo", "PAN_NO", "PanNo", 110, 110, true);
        RGrid.AddColumn("Balance", "BALANCE", "Balance", 120, 120, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("BType", string.Empty, "BType", 30, 30, true);
        RGrid.AddColumn("GLType", "CATEGORY", "GLType", 120, 120, true);
        RGrid.AddColumn("GroupDesc", "A/C GROUP", "GroupDesc", 250, 250, true);
        RGrid.AddColumn("SalesMan", "SALES MAN", "SalesMan", 250, 250, true);
        RGrid.AddColumn("PhoneNo", "PHONE_NO", "PhoneNo", 120, 120, true);
        RGrid.AddColumn("GLAddress", "ADDRESS", "GLAddress", 180, 180, true);
        GetListTable = _objPickList.GetGeneralLedgerList(ActionTag, Category, LoginDate, Status);
        RGrid.DataSource = GetListTable;
    }

    private void GetPartyLedgerListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("PanNo", "PAN_NO", "PanNo", 110, 110, true);
        RGrid.AddColumn("GLType", "CATEGORY", "GLType", 120, 120, true);
        RGrid.AddColumn("GroupDesc", "A/C GROUP", "GroupDesc", 250, 250, true);
        RGrid.AddColumn("SalesMan", "SALES MAN", "SalesMan", 250, 250, true);
        RGrid.AddColumn("PhoneNo", "PHONE_NO", "PhoneNo", 120, 120, true);
        RGrid.AddColumn("GLAddress", "ADDRESS", "GLAddress", 180, 180, true);
        GetListTable = _objPickList.GetPartyLedgerList(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetSubLedgerListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        RGrid.AddColumn("PhoneNo", "PHONE NO", "PhoneNo", 120, 120, true);
        RGrid.AddColumn("SLAddress", "ADDRESS", "SLAddress", 120, 120, true);
        RGrid.AddColumn("GLName", "LEDGER", "GLName", 220, 120, true);
        GetListTable = _objPickList.GetSubLedgerList(ActionTag, Category, LoginDate);
        RGrid.DataSource = GetListTable;
    }

    private void GetProductListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;

        RGrid.AddColumn("PID", "ProductId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("HsCode", "HS CODE", "HsCode", 120, 90, true);
        RGrid.AddColumn("PName", "DESCRIPTION", "Description", 475, 400, true, DataGridViewAutoSizeColumnMode.AllCells);
        RGrid.AddColumn("PShortName", "SHORTNAME", "ShortName", 150, 90, true);

        RGrid.AddColumn("BalanceAltQty", "ALT QTY", "BalanceAltQty", 100, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("AltUnitCode", "UOM", "AltUnitCode", 90, 50, true, DataGridViewContentAlignment.MiddleCenter);

        RGrid.AddColumn("BalanceQty", "QTY", "BalanceQty", 100, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 90, 50, true, DataGridViewContentAlignment.MiddleCenter);

        RGrid.AddColumn("BuyRate", "BUY RATE", "BuyRate", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("PSalesRate", "SALES RATE", "PSalesRate", 130, 90, true, DataGridViewContentAlignment.MiddleRight);

        RGrid.AddColumn("GrpName", "GROUP", "GrpName", 220, 150, true);
        RGrid.AddColumn("SubGrpName", "SUB GROUP", "SubGrpName", 220, 150, true);

        RGrid.AddColumn("Barcode", "Barcode", "Barcode", 0, 5, false);
        RGrid.AddColumn("Barcode1", "Barcode1", "Barcode1", 0, 5, false);
        RGrid.AddColumn("Barcode2", "Barcode2", "Barcode2", 0, 5, false);
        RGrid.AddColumn("Barcode3", "Barcode3", "Barcode3", 0, 5, false);

        GetListTable = Category.Equals("LEDGER")
            ? _objPickList.GetProductWithQtyFilterByLedger(ActionTag, LoginDate, Category, ModuleType)
            : _objPickList.GetProductWithQty(ActionTag, LoginDate);

        if (GetListTable is { Rows.Count: > 100 })
        {
            var rows = GetListTable.AsEnumerable().Take(100);
            RGrid.DataSource = rows.CopyToDataTable();
        }
        else
        {
            RGrid.DataSource = GetListTable;
        }
    }

    private void GetFinishedProductListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("PID", "Pid", "LedgerId", 0, 2, false);
        RGrid.AddColumn("PName", "DESCRIPTION", "Description", 400, 400, true, DataGridViewAutoSizeColumnMode.AllCells);
        RGrid.AddColumn("PShortName", "SHORTNAME", "ShortName", 120, 90, true);
        RGrid.AddColumn("BalanceQty", "QTY", "BalanceQty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 120, 90, true, DataGridViewContentAlignment.MiddleCenter);
        RGrid.AddColumn("BuyRate", "BUY RATE", "BuyRate", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn(
            "PSalesRate",
            "SALES RATE",
            "PSalesRate",
            150,
            90,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("GrpName", "GROUP", "GrpName", 220, 150, true);
        RGrid.AddColumn("SubGrpName", "SUB GROUP", "SubGrpName", 220, 90, true);
        GetListTable = _objPickList.GetFinishedProductWithQty(ActionTag, LoginDate);
        if (GetListTable is { Rows.Count: > 100 })
        {
            var vlist = GetListTable.AsEnumerable().Take(100);
            RGrid.DataSource = vlist.CopyToDataTable();
        }
        else
        {
            RGrid.DataSource = GetListTable;
        }
    }

    private void GetRawProductListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("PID", "Pid", "LedgerId", 0, 2, false);
        RGrid.AddColumn("PName", "DESCRIPTION", "Description", 400, 400, true, DataGridViewAutoSizeColumnMode.AllCells);
        RGrid.AddColumn("PShortName", "SHORTNAME", "ShortName", 120, 90, true);
        RGrid.AddColumn("BalanceQty", "QTY", "BalanceQty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 120, 90, true, DataGridViewContentAlignment.MiddleCenter);
        RGrid.AddColumn("BuyRate", "BUY RATE", "BuyRate", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn(
            "PSalesRate",
            "SALES RATE",
            "PSalesRate",
            150,
            90,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("GrpName", "GROUP", "GrpName", 220, 150, true);
        RGrid.AddColumn("SubGrpName", "SUB GROUP", "SubGrpName", 220, 90, true);
        GetListTable = _objPickList.GetProductWithQty(ActionTag, LoginDate);
        if (GetListTable is { Rows.Count: > 100 })
        {
            var vlist = GetListTable.AsEnumerable().Take(100);
            RGrid.DataSource = vlist.CopyToDataTable();
        }
        else
        {
            RGrid.DataSource = GetListTable;
        }
    }

    private void GetVehicleListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("ShortName", "SHORTNAME", "ShortName", 120, 120, true);
        GetListTable = _objPickList.GetVehicleNumber(ActionTag);
        RGrid.DataSource = GetListTable;
    }

    private void GetRestaurantProductListForTransaction()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("PID", "Pid", "LedgerId", 0, 2, false);
        RGrid.AddColumn("PName", "DESCRIPTION", "Description", 400, 400, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PShortName", "SHORTNAME", "ShortName", 120, 90, true);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 120, 90, true, DataGridViewContentAlignment.MiddleCenter);
        RGrid.AddColumn("BuyRate", "BUY RATE", "BuyRate", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn(
            "PSalesRate",
            "SALES RATE",
            "PSalesRate",
            130,
            90,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("GrpName", "GROUP", "GrpName", 220, 90, true);
        RGrid.AddColumn("SubGrpName", "SUB GROUP", "SubGrpName", 220, 90, true);
        GetListTable = _objPickList.GetMasterRestroProductList(ActionTag);
        RGrid.DataSource = GetListTable;
    }
    #endregion -------------- Transaction Master --------------

    // LIST TRANSACTION REPORTS
    #region --------------- TransactionReport ---------------
    private void GetProductOpeningVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VDate", "VOUCHER DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("PName", "PRODUCT", "PName", 150, 90, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("Qty", "QTY", "Qty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("Rate", "RATE", "Rate", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetProductOpeningVoucherList();
        RGrid.DataSource = GetListTable;
    }

    private void GetLedgerOpeningVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VDate", "INVOICE_DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "INVOICE_NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("GLName", "LEDGER", "GLName", 450, 250, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("AmtType", string.Empty, "AmtType", 40, 40, true, DataGridViewContentAlignment.MiddleCenter);
        GetListTable = _objPickList.ReturnLedgerOpeningVoucherList(Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetPdcVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("PDCId", "PDCId", "VoucherId", 0, 2, false);
        RGrid.AddColumn("VDate", "DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER_NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("VoucherType", "VOUCHER_TYPE", "VoucherType", 150, 90, true);
        RGrid.AddColumn("GLName", "LEDGER", "GLName", 150, 90, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.ReturnPdcVoucherList(Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetPurchaseSalesTransactionVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VoucherDate", "VOUCHER_DATE", "VoucherDate", 150, 150, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER_NO", "VoucherNo", 160, 150, true);
        RGrid.AddColumn("Ledger", "LEDGER_DESC", "Ledger", 450, 450, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("TPAN", "PAN_NO", "TPAN", 110, 110, true);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 120, 120, true, DataGridViewContentAlignment.BottomRight);
        RGrid.AddColumn("Remarks", "REMARKS", "Remarks", 120, 120, true);
        if (_objPickList != null)
            GetListTable = ListType switch
            {
                "PIN" => _objPickList.ReturnPurchaseIndentVoucherNoList(Category),
                "PO" => _objPickList.ReturnPurchaseOrderVoucherNoList(Category),
                "PC" => _objPickList.ReturnPurchaseChallanVoucherNoList(Category),
                "PCR" => _objPickList.ReturnPurchaseChallanReturnVoucherNoList(Category),
                "PB" => _objPickList.ReturnPurchaseInvoiceVoucherNoList(Category),
                "PR" => _objPickList.ReturnPurchaseReturnVoucherNoList(Category),
                "PAB" => _objPickList.ReturnPurchaseAdditionalVoucherNoList(Category),

                "SQ" => _objPickList.ReturnSalesQuotationVoucherNoList(Category),
                "SO" or "RSO" => _objPickList.ReturnSalesOrderVoucherNoList(Category),
                "SC" => _objPickList.ReturnSalesChallanVoucherNoList(Category),
                "SB" or "POS" or "RSB" => _objPickList.ReturnSalesInvoiceVoucherNoList(Category),
                "SR" => _objPickList.ReturnSalesReturnVoucherNoList(Category),
                "TSB" => _objPickList.ReturnSalesHoldVoucherNoList(Category),
                _ => GetListTable
            };

        RGrid.DataSource = GetListTable;
    }

    private void GetFinanceTransactionVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VoucherDate", "DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn("LedgerDesc", "LEDGER", "LedgerDesc", 400, 400, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn(
            "DebitAmount",
            "DEBIT (Dr)",
            "DebitAmount",
            150,
            150,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn(
            "CreditAmount",
            "CREDIT (Cr)",
            "CreditAmount",
            150,
            150,
            true,
            DataGridViewContentAlignment.MiddleRight);
        GetListTable = ListType switch
        {
            "CB" => _objPickList.ReturnCashBankVoucherList(
                Category is "PROV",
                LoginDate.GetDateTime(),
                ModuleType,
                IsActive,
                Category),
            "JV" => _objPickList.ReturnJournalVoucherList(
                Category is "PROV",
                LoginDate.GetDateTime(),
                ModuleType,
                IsActive),
            "DN" => _objPickList.ReturnDebitNotesVoucherList(
                Category is "PROV",
                LoginDate.GetDateTime(),
                ModuleType,
                IsActive),
            "CN" => _objPickList.ReturnCreditNotesVoucherList(
                Category is "PROV",
                LoginDate.GetDateTime(),
                ModuleType,
                IsActive),
            _ => new DataTable()
        };
        RGrid.DataSource = GetListTable;
    }

    private void GetTodaySalesTransactionVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VoucherDate", "VOUCHER_DATE", "VoucherDate", 150, 150, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER_NO", "VoucherNo", 150, 150, true);
        RGrid.AddColumn("Ledger", "LEDGER_DESC", "Ledger", 450, 450, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("TPAN", "PAN_NO", "TPAN", 110, 110, true);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 120, 120, true);
        RGrid.AddColumn("PaymentMode", "INVOICE_TYPE", "PaymentMode", 120, 120, true);
        if (_objPickList != null)
            GetListTable = _objPickList.ReturnTodaySalesReports();
        RGrid.DataSource = GetListTable;
    }

    #region **---------- STOCK ENTRY DETAILS ----------**
    private void GetStockVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VoucherDate", "VOUCHER_DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER_NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("PhyStockNo", "REF_VNO", "PhyStockNo", 150, 90, true);
        RGrid.AddColumn("Remarks", "REMARKS", "Remarks", 150, 90, true, DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetStockAdjustmentVoucherList();
        RGrid.DataSource = GetListTable;
    }

    private void GetBomVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VDate", "VOUCHER_DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER_NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("PName", "FINISHED_GOODS", "PName", 450, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn(
            "FinishedGoodsQty",
            "QTY",
            "FinishedGoodsQty",
            150,
            90,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 90, 90, true);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetBomVoucherList(Category);
        RGrid.DataSource = GetListTable;
    }

    private void GetInventoryBomVoucherList()
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("VDate", "VOUCHER_DATE", "VoucherDate", 150, 90, true);
        RGrid.AddColumn("VoucherNo", "VOUCHER_NO", "VoucherNo", 150, 90, true);
        RGrid.AddColumn("PName", "FINISHED_GOODS", "PName", 150, 90, true);
        RGrid.AddColumn(
            "FinishedGoodsQty",
            "QTY",
            "FinishedGoodsQty",
            150,
            90,
            true,
            DataGridViewContentAlignment.MiddleRight);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 150, 90, true);
        RGrid.AddColumn("Amount", "AMOUNT", "Amount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        GetListTable = _objPickList.GetIbomVoucherList();
        RGrid.DataSource = GetListTable;
    }
    #endregion **---------- STOCK ENTRY DETAILS ----------**

    #endregion --------------- TransactionReport ---------------

    // CRM MASTER LIST
    #region --------------- CRM MASTER LIST ---------------
    private void ListClientCollection(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PanNo", "PAN_NO", "PanNo", 110, 110, true);
        RGrid.AddColumn("ClientAddress", "ADDRESS", "ClientAddress", 225, 180, true);
        RGrid.AddColumn("EmailAddress", "EMAIL", "EmailAddress", 225, 180, true);
        GetListTable = _objPickList.GetClientCollection(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }

    private void ListClientSource(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetClientSource(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }

    private void ListRoleAssign(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetClientSource(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }

    private void ListRoleUser(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetClientSource(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }

    private void ListTaskStatus(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetClientSource(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }

    private void ListTaskType(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetClientSource(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }

    private void ListTaskManagement(string category, bool status)
    {
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            450,
            450,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetClientSource(ActionTag, status);
        RGrid.DataSource = GetListTable;
    }
    #endregion --------------- CRM MASTER LIST ---------------

    // SELECT MULTIPLE PRODUCT FROM SAME CODE
    #region --------------- Select Multi Product From Same Code  ---------------
    private void ReturnProductFromTable(DataTable dtProduct)
    {
        SearchCol = "PName";
        RGrid.AutoGenerateColumns = false;
        RGrid.RowHeadersWidth = 20;
        RGrid.AddColumn("SelectedId", "SelectedId", "SelectedId", 0, 2, false);
        RGrid.AddColumn("PName", "DESCRIPTION", "PName", 150, 150, true, DataGridViewAutoSizeColumnMode.Fill);
        RGrid.AddColumn("PShortName", "SHORTNAME", "PShortName", 150, 150, true);
        RGrid.AddColumn("UnitCode", "UOM", "UnitCode", 150, 150, true);
        RGrid.AddColumn("PSalesRate", "SALES RATE", "PSalesRate", 150, 150, true);
        RGrid.AddColumn("GrpName", "GROUP", "GrpName", 150, 150, true);
        GetListTable = dtProduct;
        RGrid.DataSource = GetListTable;
    }

    private void ReturnDataFromQueryTable(string script)
    {
        SearchCol = "Description";
        RGrid.RowHeadersWidth = 20;
        RGrid.AutoGenerateColumns = false;
        RGrid.AddColumn("LedgerId", "LedgerId", "LedgerId", 0, 2, false);
        RGrid.AddColumn(
            "Description",
            "DESCRIPTION",
            "Description",
            150,
            150,
            true,
            DataGridViewAutoSizeColumnMode.Fill);
        GetListTable = _objPickList.GetGlobalQuery(script);
        RGrid.DataSource = GetListTable;
    }
    #endregion --------------- Select Multi Product From Same Code  ---------------

    // OBJECT FOR THIS FORM
    #region --------------- Global ---------------
    private int RowIndex { get; set; }

    private int ColIndex { get; set; }

    public long ProductId { get; set; }

    private bool IsActive { get; }

    private bool Status { get; set; }

    private bool Active { get; set; }

    private bool _isFirstSearch = true;

    private string ModuleName { get; }

    private string OpenMode { get; }

    private string ListType { get; }

    private string ActionTag { get; }

    private string Category { get; }

    private string LoginDate { get; }

    private string SearchCol { get; set; }

    private string[] _mSearchCol;

    private static string ModuleType { get; set; }

    public string SearchKey { get; set; }

    public static string SubGroup { get; set; }

    public static string ProductGroup { get; set; }

    public List<DataRow> SelectedList = [];
    public DataTable table = new();
    public static DataTable GetListTable;
    private readonly ClsPickList _objPickList = new();
    #endregion --------------- Global ---------------
}