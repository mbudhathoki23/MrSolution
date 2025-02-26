using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Master.LedgerSetup;

public partial class FrmCustomerList : MrForm
{
    public static DataTable dt;
    public static string ModuleType = string.Empty;

    public static int PageWidth;
    private string Query = string.Empty;
    private string SearchCol;
    private readonly string _SearchKey = string.Empty;
    private string _ListType = string.Empty;
    private readonly string _Listname = string.Empty;
    private string _SearchValue = string.Empty;
    public List<DataRow> SelectList = [];

    public FrmCustomerList()
    {
        InitializeComponent();
    }

    public FrmCustomerList(string ListType, string ListName, string SearchKey, string SearchValue)
    {
        InitializeComponent();
        _Listname = ListName;
        _SearchValue = SearchValue;

        GetDataList(ListType);
        _SearchKey = SearchKey;
    }

    public string Module_Name { get; set; }

    private void FrmCustomerList_Load(object sender, EventArgs e)
    {
        Top = 100;
        Left = 30;
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.DGridColorCombo(Grid);
        Text = _Listname;
        SearchData(_SearchKey);
        if (PageWidth == 0)
            PageWidth = Grid.Width;
        Width = PageWidth + 40;
        Grid.Width = PageWidth + 15;
        txtCustomer.Focus();
    }

    private void FrmCustomerList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            SelectList = [];
            Close();
        }

        if (e.KeyChar == 27)
        {
            Close();
        }
        else if (e.KeyChar == 13)
        {
            if (Grid.CurrentRow != null)
            {
                var dtget = GetDataTable();
                SelectList.Add(dtget.Rows[Grid.CurrentRow.Index]);
            }

            Close();
        }
    }

    #region Method

    public DataTable GetDataTable()
    {
        var dtLocal = new DataTable();
        dtLocal = dt.Clone();

        var colname = Grid.Columns[Grid.CurrentCell.ColumnIndex].Name;

        DataRow drLocal = null;
        foreach (DataGridViewRow dr in Grid.Rows)
        {
            drLocal = dtLocal.NewRow();
            for (var i = 0; i < Grid.Columns.Count; i++)
            {
                var item = Grid.Columns[i].DataPropertyName;
                drLocal[item] = dr.Cells[i].Value;
            }

            dtLocal.Rows.Add(drLocal);
        }

        return dtLocal;
    }

    private void SearchData(string SearchText)
    {
        if (txtPan.Text.Trim() != string.Empty)
            SearchCol = "PanNo";
        else if (txtCustomer.Text.Trim() != string.Empty)
            SearchCol = "GLName";
        else if (txtPhoneNo.Text.Trim() != string.Empty)
            SearchCol = "PhoneNo";
        else if (txtAddress.Text.Trim() != string.Empty)
            SearchCol = "GLAddress";

        int colIndex = 0, rowIndex = 0;
        var bs = new BindingSource();
        if (!string.IsNullOrEmpty(SearchText) && SearchText != "F1")
            try
            {
                bs.DataSource = Grid.DataSource;
                bs.Filter = string.Format("Convert(" + SearchCol + ", 'System.String')") + " like '%" + SearchText +
                            "%'";
                Grid.DataSource = bs;
                var row = new DataGridViewRow();
                if (Grid.Rows[0].Cells[colIndex].Value.ToString() == SearchText)
                    Grid.Rows[0].Cells[colIndex].Selected = true;
            }
            catch
            {
            }
        else
            try
            {
                bs.DataSource = Grid.DataSource;
                bs.Filter = string.Format("Convert(" + SearchCol + ", 'System.String')") + " like '%" + SearchText +
                            "%'";
                Grid.DataSource = bs;
                rowIndex = Grid.CurrentCell.RowIndex;
                if (Grid.Rows[0].Cells[colIndex].Value.ToString() == SearchText)
                    Grid.Rows[0].Cells[colIndex].Selected = true;
            }
            catch
            {
            }
    }

    public void GetDataList(string ListType)
    {
        PageWidth = 0;
        switch (ListType)
        {
            case "CustomerList":
                CounterProductList();
                SearchCol = "GLName";
                break;
        }
    }

    private void CounterProductList()
    {
        try
        {
            Grid.AutoGenerateColumns = false;
            Query = string.Empty;
            Query =
                "Select GLID, PanNo,GLName,GLAddress,PhoneNo,Convert(Decimal(18,2),isNull(Sum(Debit_Amt),0) -isNull(SUm(Credit_Amt),0)) as Balance from AMS.GeneralLedger as Gl left Outer Join  AMS.AccountDetails as AD on AD.ledger_Id=GL.GLID Where GLType In ('Customer','Both') Group By GLID,PanNo,GLName,GLAddress,PhoneNo";
            // if (_SearchValue != null && _SearchValue != "")
            Query = Query + " ";
            Query = Query + " Order by PanNo";
            dt = GetConnection.SelectDataTableQuery(Query);
            Grid.DataSource = dt;

            Grid.Columns.Add("GLID", "GLID");
            Grid.Columns["GLID"].DataPropertyName = "GLID";
            Grid.Columns["GLID"].Width = 0;
            Grid.Columns["GLID"].Visible = false;

            Grid.Columns.Add("PanNo", "Pan No");
            Grid.Columns["PanNo"].DataPropertyName = "PanNo";
            Grid.Columns["PanNo"].Width = 120;
            PageWidth = PageWidth + 120;

            Grid.Columns.Add("Description", "Description");
            Grid.Columns["Description"].DataPropertyName = "GLName";
            Grid.Columns["Description"].Width = 250;
            PageWidth = PageWidth + 250;

            Grid.Columns.Add("PhoneNo", "Phone No");
            Grid.Columns["PhoneNo"].DataPropertyName = "PhoneNo";
            Grid.Columns["PhoneNo"].Width = 140;
            PageWidth = PageWidth + 140;

            Grid.Columns.Add("GLAddress", "Address");
            Grid.Columns["GLAddress"].DataPropertyName = "GLAddress";
            Grid.Columns["GLAddress"].Width = 220;
            PageWidth = PageWidth + 220;

            Grid.Columns.Add("Balance", "Balance");
            Grid.Columns["Balance"].DataPropertyName = "Balance";
            Grid.Columns["Balance"].Width = 120;
            Grid.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            PageWidth = PageWidth + 120;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    #endregion Method
}