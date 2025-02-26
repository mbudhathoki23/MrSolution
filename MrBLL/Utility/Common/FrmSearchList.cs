using MrDAL.Control.WinControl;
using MrDAL.Core.Utils;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Utility.Common;

public partial class FrmSearchList : MrForm
{
    public static DataTable dt;
    public static string Floor;
    public static string AcGroup = string.Empty, GlDesc = string.Empty;
    public static DateTime FDate, TDate;
    public static string ModuleType = string.Empty;
    public static string ProductGroup = string.Empty;
    private readonly string _searchKey = string.Empty;
    private string _ListType = string.Empty, _ListName = string.Empty;
    public bool checkPickList = false;
    public bool IsDataAvailable = false;
    private string SearchCol;
    public DataTable SearchDT = new();
    public List<DataRow> SelectedList = [];

    public FrmSearchList(string ListType, string ListName, string searchKey)
    {
        InitializeComponent();
        GetList(ListType);
        _searchKey = searchKey;
    }

    public string Module_Name { get; set; }

    private void FrmSearchList_Load(object sender, EventArgs e)
    {
        Search(_searchKey);
    }

    public void GetList(string ListType)
    {
        switch (ListType)
        {
            case "Table":
                Table();
                SearchCol = "Table_Name";
                break;

            case "ProductList":
                ProductList();
                SearchCol = "P_ShortName";
                break;
        }
    }

    private void Grid_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 8)
        {
            if (txtSearch.Text.Length > 0)
                txtSearch.Text = txtSearch.Text.Substring(0, txtSearch.Text.Length - 1);
        }
        else if (e.KeyChar == 27)
        {
            Close();
        }
        else if (e.KeyChar == 13)
        {
            if (Grid.CurrentRow != null)
            {
                var dt1 = GetDataTable();
                SelectedList.Add(dt1.Rows[Grid.CurrentRow.Index]);
            }

            Close();
        }
        else
        {
            txtSearch.Text += e.KeyChar.ToString();
        }
    }

    private void FrmSearchList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = false;
            btnOk.PerformClick();
        }
        else if (e.KeyCode == Keys.Escape)
        {
            btnCancel.PerformClick();
        }
    }

    private void Grid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.Handled = false;
            btnOk.PerformClick();
        }
        else if (e.KeyCode == Keys.Escape)
        {
            btnCancel.PerformClick();
        }
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
        if (Grid.CurrentRow != null)
        {
            using var dt1 = GetDataTable();
            SelectedList.Add(dt1.Rows[Grid.CurrentRow.Index]);
        }

        Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        SelectedList = [];
        SelectedList.Clear();
        Close();
    }

    private void Table()
    {
        // AMS.Master.frmKotNo table = new AMS.Master.frmKotNo();
        Grid.AutoGenerateColumns = false;
        dt = GetConnection.SelectDataTableQuery(
            "Select Rest.*,Ara.Description as FloorName from AMS.TableMaster as ResT Left Outer Join AMS.Floor as Ara on Ara.FloorId = ResT.FloorId");
        Grid.DataSource = dt;

        Grid.Columns.Add("TableId", " Code");
        Grid.Columns["TableId"].DataPropertyName = "TableId";
        Grid.Columns["TableId"].Width = 50;
        Grid.Columns["TableId"].Visible = false;

        Grid.Columns.Add("TableName", "Table Name");
        Grid.Columns["TableName"].DataPropertyName = "TableName";
        Grid.Columns["TableName"].Width = 150;

        Grid.Columns.Add("TableType", "Table Type");
        Grid.Columns["TableType"].DataPropertyName = "TableType";
        Grid.Columns["TableType"].Width = 100;

        Grid.Columns.Add("FloorName", "Floor");
        Grid.Columns["FloorName"].DataPropertyName = "FloorName";
        Grid.Columns["FloorName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
    }

    private void ProductList()
    {
        ClientSize = new Size(700, 411);
        Grid.Size = new Size(690, 311);

        txtSearch.Size = new Size(450, 21);
        btnCancel.Location = new Point(360, 6);
        btnOk.Location = new Point(288, 6);

        dt = GetConnection.SelectDataTableQuery(
            "select PID,PName,PShortName,PSalesRate,PCategory from  AMS.Product");

        Grid.AutoGenerateColumns = false;
        Grid.DataSource = dt;

        Grid.Columns.Add("PID", "Code");
        Grid.Columns["PID"].DataPropertyName = "PID";
        Grid.Columns["PID"].Visible = false;

        Grid.Columns.Add("PName", "Description");
        Grid.Columns["PName"].DataPropertyName = "PName";
        Grid.Columns["PName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        Grid.Columns.Add("PShortName", "Short Name");
        Grid.Columns["PShortName"].DataPropertyName = "PShortName";
        Grid.Columns["PShortName"].Width = 100;

        Grid.Columns.Add("PSalesRate", "Sales Rate");
        Grid.Columns["PSalesRate"].DataPropertyName = "PSalesRate";
        Grid.Columns["PSalesRate"].Width = 100;

        Grid.Columns.Add("PCategory", "Product Category");
        Grid.Columns["PCategory"].DataPropertyName = "PCategory";
        Grid.Columns["PCategory"].Width = 100;
    }

    private void Search(string SearchText)
    {
        int colIndex = 0, rowIndex = 0;
        var bs = new BindingSource();
        if (!string.IsNullOrEmpty(SearchText) && SearchText != "F1")
            try
            {
                txtSearch.Text = SearchText;
                bs.DataSource = Grid.DataSource;
                var i = 0;
                SearchCol = string.Empty;
                for (i = 0; i <= Grid.Columns.Count - 1; i++)
                    if (i != Grid.Columns.Count - 1)
                        SearchCol +=
                            string.Format("Convert([" + Grid.Columns[i].DataPropertyName + "], 'System.String')") +
                            " like '%" + txtSearch.Text + "%' or ";
                    else
                        SearchCol +=
                            string.Format("Convert([" + Grid.Columns[i].DataPropertyName + "], 'System.String')") +
                            " like '%" + txtSearch.Text + "%'";

                bs.Filter = SearchCol;
                Grid.DataSource = bs;
                var row = new DataGridViewRow();
                Grid.Rows[0].Cells[colIndex].Selected = true;
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult(e.StackTrace);
            }
        else
            try
            {
                bs.DataSource = Grid.DataSource;
                var i = 0;
                SearchCol = string.Empty;
                if (Grid.RowCount <= 0) return;
                for (i = 0; i <= Grid.Columns.Count - 1; i++)
                    SearchCol += i != Grid.Columns.Count - 1
                        ? $"Convert({{Grid.Columns[i].DataPropertyName}}, 'System.String') like '%{txtSearch.Text}%' or "
                        : $"Convert({Grid.Columns[i].DataPropertyName}, 'System.String')" + " like '%" +
                          txtSearch.Text + "%'";
                bs.Filter = SearchCol;
                Grid.DataSource = bs;
                rowIndex = Grid.CurrentCell.RowIndex;
                Grid.Rows[0].Cells[colIndex].Selected = true;
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult(e.StackTrace);
            }
    }

    private void txtSearch_TextChanged(object sender, EventArgs e)
    {
        Grid.Focus();
        Search(txtSearch.Text);
    }

    public DataTable GetDataTable()
    {
        var dtLocal = new DataTable();
        dtLocal = dt.Clone();

        var unused = Grid.Columns[Grid.CurrentCell.ColumnIndex].Name;
        foreach (DataGridViewRow dr in Grid.Rows)
        {
            var drLocal = dtLocal.NewRow();
            for (var i = 0; i < Grid.Columns.Count; i++)
            {
                var item = Grid.Columns[i].DataPropertyName;
                drLocal[item] = dr.Cells[i].Value;
            }

            dtLocal.Rows.Add(drLocal);
        }

        return dtLocal;
    }
}