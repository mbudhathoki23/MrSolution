using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmSearchCProduct : MrForm
{
    //SEARCH COUNTER PRODUCT

    #region --------------- SearchCounterProduct ---------------

    public FrmSearchCProduct(string listType)
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
        WindowState = FormWindowState.Maximized;
        DGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        _listType = listType;
        GetDataList(_listType);
    }

    public FrmSearchCProduct(string listType, string listName, string searchKey, string searchValue, bool isActive = false)
    {
        InitializeComponent();
        _listName = listName;
        _searchKey = searchKey;
        _isActive = isActive;
        _listType = listType;
        GetDataList(_listType);
    }

    private void FrmSearchCounterProduct_Load(object sender, EventArgs e)
    {
        //ObjGlobal.DGridColorCombo(DGrid);
        Text = _listName;
        SearchData(_searchKey);
        TxtSearchText.Focus();
    }

    private void FrmSearchCounterProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            SelectList = [];
            Close();
        }

        if (e.KeyChar == 27)
        {
            Close();
            return;
        }
        else if (e.KeyChar == 13)
        {
            if (DGrid.CurrentRow != null)
            {
                var dtGet = GetDataTable();
                SelectList.Add(dtGet.Rows[DGrid.CurrentRow.Index]);
            }

            Close();
        }
        else
        {
            if (ActiveControl.Name == "TxtSearchText")
            {
                return;
            }
            TxtSearchText.Text += e.KeyChar.ToString();
            TxtSearchText.Focus();
        }
    }

    private void FrmSearchCounterProduct_MinimumSizeChanged(object sender, EventArgs e)
    {
    }

    private void FrmSearchCounterProduct_Resize(object sender, EventArgs e)
    {
        Refresh();
    }

    private void TxtProductShortName_TextChanged(object sender, EventArgs e)
    {
        TxtSearchText.Focus();
        SearchData(TxtSearchText.Text);
    }

    private void TxtProductShortName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            SendKeys.Send("{TAB}");
        }
        if (e.KeyCode is Keys.Down or Keys.Right or Keys.Up or Keys.Left)
        {
            DGrid.Focus();
        }
    }

    private void TxtProductShortName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtSearchText, 'E');
    }

    private void TxtProductShortName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtSearchText, 'L');
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        if (_listType.Equals("StockReports"))
        {
            return;
        }
        else
        {
            if (DGrid.CurrentRow != null)
            {
                var DTget = GetDataTable();
                SelectList.Add(DTget.Rows[DGrid.CurrentRow.Index]);
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        SelectList = [];
        Close();
    }

    // GRID CONTROL EVENTS
    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = false;
            btn_Ok.PerformClick();
        }
        else if (e.KeyCode == Keys.Enter)
        {
            btn_Cancel.PerformClick();
        }
    }

    private void Grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        BtnOk_Click(sender, e);
    }

    private void DGrid_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
    {
        if (!DGrid.VirtualMode)
        {
            return;
        }
        if (e.RowIndex == -1 || e.ColumnIndex == -1)
        {
            return;
        }
        var currentColumn = DGrid.Columns[e.ColumnIndex];
        e.Value = currentColumn.Name switch
        {
            "PID" => _dataTable.Rows[e.RowIndex].Field<string>("PID"),
            "PShortName" => _dataTable.Rows[e.RowIndex].Field<string>("PShortName"),
            "PName" => _dataTable.Rows[e.RowIndex].Field<string>("PName"),
            "Barcode" => _dataTable.Rows[e.RowIndex].Field<string>("Barcode"),
            "Barcode1" => _dataTable.Rows[e.RowIndex].Field<string>("Barcode1"),
            "SalesRate" => _dataTable.Rows[e.RowIndex].Field<string>("SalesRate"),
            "BuyRate" => _dataTable.Rows[e.RowIndex].Field<string>("BuyRate"),
            "UnitName" => _dataTable.Rows[e.RowIndex].Field<string>("UnitName"),
            "Taxable" => _dataTable.Rows[e.RowIndex].Field<string>("Taxable"),
            "GrpName" => _dataTable.Rows[e.RowIndex].Field<string>("GrpName"),
            _ => e.Value
        };
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        btn_Ok.PerformClick();
    }

    private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    #endregion --------------- SearchCounterProduct ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    public DataTable GetDataTable()
    {
        var dtLocal = _dataTable.Clone();
        foreach (DataGridViewRow dr in DGrid.Rows)
        {
            var drLocal = dtLocal.NewRow();
            for (var i = 0; i < DGrid.Columns.Count; i++)
            {
                var item = DGrid.Columns[i].DataPropertyName;
                drLocal[item] = dr.Cells[i].Value;
            }

            dtLocal.Rows.Add(drLocal);
        }

        return dtLocal;
    }

    private void SearchData(string searchText)
    {
        const int colIndex = 0;
        var bs = new BindingSource();
        try
        {
            if (DGrid.DataSource == null)
            {
                return;
            }

            ((DataTable)DGrid.DataSource).DefaultView.RowFilter = _listType.Equals("StockReports") ? $"PName LIKE '%{searchText}%'" : $"PName LIKE '%{searchText}%' or PShortName LIKE '%{searchText}%' or Barcode LIKE '%{searchText}%' or Barcode1 LIKE '%{searchText}%' or Barcode2 LIKE '%{searchText}%' or Barcode3 LIKE '%{searchText}%'";
        }
        catch (Exception ex) { ex.ToNonQueryErrorResult(ex.StackTrace); }
    }

    public void GetDataList(string listType)
    {
        PageWidth = 0;
        if (listType == "CounterProduct")
        {
            CounterProductList();
        }
        else if (listType is "StockReports")
        {
            GetStockReports();
        }
    }

    private void CounterProductList()
    {
        DGrid.AutoGenerateColumns = false;
        DGrid.AddColumn("PID", "PRODUCT_ID", "PID", 0, 2, false);
        DGrid.AddColumn("HsCode", "HS CODE", "HsCode", 150, 100, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("PShortName", "BARCODE", "PShortName", 150, 100, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("Barcode", "BARCODE", "Barcode", 150, 100, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("Barcode1", "BARCODE", "Barcode1", 150, 100, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("Barcode2", "BARCODE", "Barcode2", 150, 0, false, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("Barcode3", "BARCODE", "Barcode3", 150, 0, false, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("PName", "DESCRIPTION", "PName", 400, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        PageWidth += 400;
        DGrid.AddColumn("StockQty", "STOCK", "StockQty", 150, 100, true, DataGridViewContentAlignment.MiddleRight);
        PageWidth += 150;
        DGrid.AddColumn("UnitName", "UOM", "UnitName", 80, 70, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 80;
        DGrid.AddColumn("SalesRate", "SALES_RATE", "SalesRate", 150, 100, true, DataGridViewContentAlignment.MiddleRight);
        PageWidth += 150;
        DGrid.AddColumn("Taxable", "IS_TAXABLE", "Taxable", 100, 80, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 100;
        DGrid.AddColumn("GrpName", "CATEGORY", "GrpName", 200, 150, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 200;
        _dataTable = _objMaster.GetMasterCounterProductList(0, _isActive);
        DGrid.DataSource = _dataTable;
    }

    private void GetStockReports()
    {
        DGrid.AutoGenerateColumns = false;
        DGrid.AddColumn("PID", "PRODUCT_ID", "PID", 0, 2, false);
        DGrid.AddColumn("PShortName", "BARCODE", "PShortName", 0, 2, false, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 150;
        DGrid.AddColumn("PName", "DESCRIPTION", "PName", 400, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        PageWidth += 400;
        DGrid.AddColumn("StockQty", "STOCK", "StockQty", 150, 100, true, DataGridViewContentAlignment.MiddleRight);
        PageWidth += 150;
        DGrid.AddColumn("UnitName", "UOM", "UnitName", 80, 70, true, DataGridViewContentAlignment.MiddleCenter);
        PageWidth += 80;
        DGrid.AddColumn("CostRate", "COST_RATE", "CostRate", 150, 100, true, DataGridViewContentAlignment.MiddleRight);
        PageWidth += 150;
        DGrid.AddColumn("SalesRate", "SALES_RATE", "SalesRate", 150, 100, true, DataGridViewContentAlignment.MiddleRight);
        PageWidth += 150;
        DGrid.AddColumn("GrpName", "CATEGORY", "GrpName", 200, 150, true, DataGridViewContentAlignment.MiddleCenter, DataGridViewColumnSortMode.NotSortable, DataGridViewAutoSizeColumnMode.Fill);
        PageWidth += 200;
        DGrid.AddColumn("SubGrpName", "SUB_CATEGORY", "SubGrpName", 200, 150, true, DataGridViewContentAlignment.MiddleCenter, DataGridViewColumnSortMode.NotSortable, DataGridViewAutoSizeColumnMode.Fill);
        PageWidth += 200;
        _dataTable = _objMaster.GetProductListWithQty();
        DGrid.DataSource = _dataTable;
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    public List<DataRow> SelectList = [];
    public static DataTable Dt;
    private DataTable _dataTable = new();
    private readonly bool _isActive;
    private string _searchCol;
    private readonly string _searchKey = string.Empty;
    private readonly string _listType;
    private readonly string _listName = string.Empty;
    public string ModuleName;
    public static string ModuleType = string.Empty;
    public static int PageWidth;
    private readonly IMasterSetup _objMaster = new ClsMasterSetup();

    #endregion --------------- OBJECT ---------------
}