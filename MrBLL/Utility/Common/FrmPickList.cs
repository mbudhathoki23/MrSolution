using Microsoft.VisualBasic;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Utility.Common;

public partial class FrmPickList : MrForm
{
    public static DataTable dts;
    public static string ModuleType = string.Empty;
    private readonly ClsPickList obj = new();
    public string _SearchText = string.Empty;
    public bool Batch_Ent;
    public string C_Search = string.Empty;
    private int col;

    private int currentColumn;
    public bool Cust_Ent;
    private DataTable dt = new();
    private DataTable dtPickUp = new();
    private ObjGlobal Gobj = new();
    public string My_String = string.Empty;

    public bool Pro_Ent;
    private string Query = string.Empty;

    private int rowIndex;

    private string SearchCol,
        _SearchKey = string.Empty,
        _ListType = string.Empty,
        _Listname = string.Empty,
        _SearchValue = string.Empty;

    public List<DataRow> SelectList = [];
    private string Str = string.Empty;
    private bool Test;
    private int TotGridWidth;

    public FrmPickList(string Size)
    {
        InitializeComponent();
    }

    public FrmPickList(string selectQuery, string Size = "MINI")
    {
        InitializeComponent();
        obj.SelectQuery = selectQuery;
    }

    public FrmPickList(DataTable data, ArrayList headerCap, ArrayList columnWidths, string frmName, string searchText, string size = "MINI")
    {
        InitializeComponent();
        obj.Data = data;
        obj.HeaderCap = headerCap;
        obj.ColumnWidths = columnWidths;
        obj.FrmName = frmName;
        obj.SearchText = searchText;
        _Listname = frmName;
        _SearchValue = searchText;
        obj.Size = size;
        BindData();
    }

    public FrmPickList(string selectQuery, ArrayList headerCap, ArrayList columnWidths, string frmName, string searchText, string size = "MINI")
    {
        InitializeComponent();
        obj.SelectQuery = selectQuery;
        obj.HeaderCap = headerCap;
        obj.ColumnWidths = columnWidths;
        obj.FrmName = frmName;
        obj.SearchText = searchText;
        obj.Size = size;
        _Listname = frmName;
        _SearchValue = searchText;
        BindData();
    }

    public FrmPickList(string selectQuery, ArrayList headerCap, ArrayList columnWidths, string frmName, string frmType, string fromDate, string toDate, string size = "MINI")
    {
        InitializeComponent();
        obj.SelectQuery = selectQuery;
        obj.HeaderCap = headerCap;
        obj.ColumnWidths = columnWidths;
        obj.FrmName = frmName;
        obj.FrmType = frmType;
        obj.FromDate = fromDate;
        obj.ToDate = toDate;
        obj.Size = size;
        BindData();
    }

    public string ModuleName { get; set; }

    private void FrmPickList_Load(object sender, EventArgs e)
    {
        RGrid.RowHeadersWidth = 20;
        Size = obj.Size.ToUpper() == "MIN" ? new Size(792, 521) :
            obj.Size.ToUpper() == "MAX" ? new Size(1272, 628) : new Size(521, 521);
        StartPosition = FormStartPosition.CenterParent;
        BackColor = ObjGlobal.FrmBackColor();
        Text = obj.FrmName;
        BindData();
    }

    private void BindData()
    {
        if (!string.IsNullOrEmpty(obj.SelectQuery))
        {
            if (obj.ConType == "MASTER")
            {
                dtPickUp.Reset();
                dtPickUp = GetConnection.SelectQueryFromMaster(obj.SelectQuery);
            }
            else
            {
                dtPickUp.Reset();
                dtPickUp = GetConnection.SelectDataTableQuery(obj.SelectQuery);
            }
        }
        else if (obj.Data.Rows.Count != 0)
        {
            dtPickUp = obj.Data;
        }

        dts = dtPickUp;
        RGrid.DataSource = dtPickUp;
        if (RGrid.Rows.Count == 0) return;
        RGrid.ReadOnly = true;

        for (var i = 0; i < obj.HeaderCap.Count; i++)
        {
            if (Convert.ToInt16(obj.ColumnWidths[i]) > 0)
            {
                if (string.IsNullOrEmpty(obj.FrmType)) RGrid.Columns[i].HeaderText = obj.HeaderCap[i].ToString();
                RGrid.Columns[i].Width = Convert.ToInt16(obj.ColumnWidths[i].ToString());
                TotGridWidth += Convert.ToInt16(obj.ColumnWidths[i]);
                RGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                RGrid.Columns[i].HeaderCell.Style.Font = new Font("Bookman Old Style", 10, FontStyle.Regular);
            }
            else
            {
                RGrid.Columns[i].Visible = false;
            }

            if (i >= 6)
                if (Pro_Ent)
                {
                    RGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    RGrid.Columns[i].DefaultCellStyle.Format =
                        i >= 9 ? ObjGlobal.SysQtyFormat : ObjGlobal.SysAmountFormat;
                }

            if (i < RGrid.Columns.Count - 8) continue;
            if (i == RGrid.Columns.Count - 3 || i == RGrid.Columns.Count - 4) continue;
            if (!Batch_Ent) continue;
            RGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            RGrid.Columns[i].DefaultCellStyle.Format = i >= 9 ? ObjGlobal.SysQtyFormat : ObjGlobal.SysAmountFormat;
        }

        ObjGlobal.DGridColorCombo(RGrid);
        RGrid.Width = 45 + TotGridWidth;
        Width = RGrid.Width + 40;
        lbl_SearchValue.Width = TotGridWidth - 35;
        TxtFindValue.Width = TotGridWidth - 142;
        btn_Find.Left = TotGridWidth;

        if (RGrid.Rows.Count > 10)
        {
            lbl_SearchValue.Width = TotGridWidth;
            TxtFindValue.Width = TotGridWidth - 142; // -50;
            btn_Find.Left = TxtFindValue.Width + 156;
            RGrid.Width = RGrid.Width + 15;
            Width = Width + 20;
        }

        lbl_SearchValue.Text = obj.SearchText;
        _SearchText = obj.SearchText;
        //SearchData(_SearchText);
    }

    private void FrmPickList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            SelectList = [];
            Close();
        }
        else if (e.KeyChar == 13)
        {
            if (RGrid.CurrentRow != null)
            {
                var dtget = GetDataTable();
                SelectList.Add(dtget.Rows[RGrid.CurrentRow.Index]);
                ShowList(rowIndex);
            }

            Close();
        }
        else
        {
            if (e.KeyChar == 6)
                if (e.KeyChar == 19)
                    RGrid.Focus();

            if (ActiveControl.Name == "TxtFindValue") return;

            if (Strings.InStr(
                    @"ABCDEFGHIJKLMNOPQRSTUVWYXZabcdefghijklmnopqrstuvwxy z`~!@#$%^&*()_+1234567890-=/ <>.",
                    e.KeyChar.ToString().ToUpper()) > 0)
            {
                if (_SearchText != string.Empty)
                {
                    _SearchKey = _SearchText;
                    _SearchKey += e.KeyChar.ToString().ToUpper();
                    _SearchText = _SearchKey;
                }
                else
                {
                    //SearchKey=SearchKey;
                    _SearchKey += e.KeyChar.ToString().ToUpper();
                }

                if (_SearchKey.Length > 0) lbl_SearchValue.Text = _SearchKey;
                SearchValue();
            }

            if (e.KeyChar == 8)
            {
                if (_SearchKey.Length > 0)
                {
                    _SearchKey = _SearchKey.Substring(0, _SearchKey.Length - 1);
                    if (_SearchKey.Length == 0) _SearchText = string.Empty;
                    SearchValue();
                    lbl_SearchValue.Text = _SearchKey;
                }
                else
                {
                    lbl_SearchValue.Text = _SearchKey;
                }
            }

            if (e.KeyChar == 39) e.KeyChar = '0';
        }
    }

    private void CmdCancel_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        ClsPickList.PlValue4 = string.Empty;
        ClsPickList.PlValue5 = string.Empty;
        SelectList = [];
        Close();
    }

    private void CmdOk_Click(object sender, EventArgs e)
    {
        if (RGrid.Rows.Count > 0)
        {
            if (RGrid.CurrentRow != null)
            {
                var DTget = GetDataTable();
                SelectList.Add(DTget.Rows[RGrid.CurrentRow.Index]);
            }

            ShowList(rowIndex);
        }
    }

    private void TxtFindValue_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtFindValue, 'E');
    }

    private void TxtFindValue_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue == 13 || e.KeyValue == 9) btn_Find.PerformClick();
    }

    private void TxtFindValue_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtFindValue, 'L');

        if (TxtFindValue.Text.Trim() != string.Empty) btn_Find.PerformClick();
    }

    private void btn_Find_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow ro in RGrid.Rows)
        {
            var cell = ro.Cells[currentColumn];
            cell.Style.BackColor = Color.FloralWhite;
            cell.Style.SelectionBackColor = SystemColors.Highlight;
        }

        if (TxtFindValue.Text == string.Empty)
        {
            var cell = RGrid.Rows[0].Cells[currentColumn];
            cell.Style.BackColor = SystemColors.Highlight;
        }

        if (TxtFindValue.Text != string.Empty)
        {
            int i;
            for (i = 0; i < RGrid.Rows.Count; i++)
            {
                var cell = RGrid.Rows[rowIndex].Cells[currentColumn];
                cell.Style.BackColor = Color.FloralWhite;
                if (RGrid.Rows[i].Cells[currentColumn].Value != null &&
                    RGrid.Rows[i].Cells[currentColumn].Value.ToString() != string.Empty)
                    if (RGrid.Rows[i].Cells[currentColumn].Value.ToString().Trim().Substring(0).Length >=
                        TxtFindValue.Text.Trim().Length)
                        if (RGrid.Rows[i].Cells[currentColumn].Value.ToString().Trim()
                                .Substring(0, TxtFindValue.Text.Trim().Length).ToUpper() ==
                            TxtFindValue.Text.ToUpper().Trim())
                        {
                            RGrid.ClearSelection();
                            cell = RGrid.Rows[i].Cells[currentColumn];
                            cell.Style.BackColor = SystemColors.Highlight;
                            RGrid.Focus();
                            rowIndex = i;
                            return;
                        }
            }
        }
    }

    private void dgv_PickList_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            CmdOk_Click(sender, e);
            DialogResult = DialogResult.OK;
        }

        if (e.KeyValue == 37 || e.KeyValue == 38 || e.KeyValue == 39 || e.KeyValue == 40)
            if (RGrid.Rows.Count > 0)
            {
                var cell = RGrid.Rows[0].Cells[currentColumn];
                foreach (DataGridViewRow ro in RGrid.Rows)
                {
                    cell = ro.Cells[currentColumn];
                    cell.Style.BackColor = Color.FloralWhite;
                    cell.Style.SelectionBackColor = SystemColors.Highlight;
                }
            }
    }

    private void dgv_PickList_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void dgv_PickList_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void dgv_PickList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        CmdOk_Click(sender, e);
        DialogResult = DialogResult.OK;
    }

    private void dgv_PickList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
    {
        ObjGlobal.DGridColorCombo(RGrid);
    }

    private void lbl_SearchValue_TextChanged(object sender, EventArgs e)
    {
        #region Grid Search

        SearchData(lbl_SearchValue.Text);
        //return;
        if (RGrid.Rows.Count == 0)
        {
            RGrid.DataSource = dtPickUp;
        }
        else
        {
            foreach (DataGridViewRow ro in RGrid.Rows)
            {
                var cell = ro.Cells[currentColumn];
                cell.Style.BackColor = Color.FloralWhite;
                cell.Style.SelectionBackColor = SystemColors.Highlight;
            }

            if (lbl_SearchValue.Text == string.Empty)
            {
                if (RGrid.Rows.Count <= 0) return;
                var cell = RGrid.Rows[0].Cells[currentColumn];
                cell.Style.BackColor = SystemColors.Highlight;
            }
            else if (lbl_SearchValue.Text != string.Empty)
            {
                int i;
                for (i = 0; i < RGrid.Rows.Count; i++)
                {
                    var cell = RGrid.Rows[rowIndex].Cells[currentColumn];
                    cell.Style.BackColor = Color.FloralWhite;

                    if (RGrid.Rows[i].Cells[currentColumn].Value == null ||
                        RGrid.Rows[i].Cells[currentColumn].Value.ToString() == string.Empty) continue;
                    if (RGrid.Rows[i].Cells[currentColumn].Value.ToString().Trim().Substring(0).Length <
                        lbl_SearchValue.Text.Trim().Length) continue;
                    if (RGrid.Rows[i].Cells[currentColumn].Value.ToString().Trim()
                            .Substring(0, lbl_SearchValue.Text.Trim().Length).ToUpper() !=
                        lbl_SearchValue.Text.ToUpper().Trim()) continue;
                    RGrid.ClearSelection();
                    rowIndex = i;
                    cell = RGrid.Rows[rowIndex].Cells[currentColumn];
                    cell.Style.BackColor = SystemColors.Highlight;
                    if (RGrid.Rows.Count > 0 && cell.ColumnIndex != 0)
                        RGrid.CurrentCell = RGrid.Rows[i].Cells[currentColumn];

                    RGrid.Focus();
                    return;
                }
            }
        }

        #endregion Grid Search
    }

    private void Search()
    {
        if (RGrid.CurrentCell == null) return;
        var searchColumn = RGrid.CurrentCell.OwningColumn.Name;
        ((DataTable)RGrid.DataSource).DefaultView.RowFilter =
            string.Format($" {searchColumn} LIKE '{0}%'", lbl_SearchValue.Text.Trim());
        RGrid.DataSource = dtPickUp;
    }

    private void ShowList(int ro)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        ClsPickList.PlValue4 = string.Empty;
        ClsPickList.PlValue5 = string.Empty;

        for (var Co = 0; Co < RGrid.Columns.Count; Co++)
            switch (Co)
            {
                case 0:
                    ClsPickList.PlValue1 = RGrid.Rows[ro].Cells[Co].Value.ToString();
                    break;

                case 1:
                    ClsPickList.PlValue2 = RGrid.Rows[ro].Cells[Co].Value.ToString();
                    break;

                case 2:
                    ClsPickList.PlValue3 = RGrid.Rows[ro].Cells[Co].Value.ToString();
                    break;

                case 3:
                    ClsPickList.PlValue4 = RGrid.Rows[ro].Cells[Co].Value.ToString();
                    break;

                case 5:
                    ClsPickList.PlValue5 = RGrid.Rows[ro].Cells[Co].Value.ToString();
                    break;
            }
    }

    private void SearchValue()
    {
        My_String = _SearchKey;

        if (My_String == string.Empty) return;
        if (RGrid.Rows.Count > 0)
        {
            Str = "[" + RGrid.Rows[rowIndex].Cells[currentColumn].Value + "]";
            Str = Str + " Like '" + My_String + "*'";
        }
        else if (RGrid.Rows.Count == 0)
        {
            RGrid.DataSource = dtPickUp;
        }
    }

    public DataTable GetDataTable()
    {
        var dtLocal = dts.Clone();
        var cName = RGrid.Columns[RGrid.CurrentCell.ColumnIndex].Name;
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

    private void SearchData(string searchText)
    {
        int colIndex = 0, rowIndex = 0;
        if (lbl_SearchValue.Text.Trim() != string.Empty)
            if (RGrid.Rows.Count > 0)
            {
                if (RGrid.CurrentCell != null)
                {
                    SearchCol = RGrid.Columns[RGrid.CurrentCell.ColumnIndex].Name;
                    colIndex = RGrid.CurrentCell.ColumnIndex;
                }
                else
                {
                    return;
                }
            }

        var bs = new BindingSource();
        if (!string.IsNullOrEmpty(searchText) && searchText != "F1" && RGrid.Rows.Count != 0)
        {
            bs.DataSource = RGrid.DataSource;
            bs.Filter = string.Format("Convert(" + SearchCol + ", 'System.String')") + " like '%" + searchText +
                        "%'";
            if (bs.Count <= 0) return;
            RGrid.DataSource = bs;
            var row = new DataGridViewRow();
            if (row.Index != 0 && row.Index > 0) RGrid.CurrentCell = RGrid.Rows[rowIndex].Cells[colIndex];

            if (RGrid.Rows[0].Cells[colIndex].Value.ToString() == searchText)
                RGrid.Rows[0].Cells[colIndex].Selected = true;
        }
        else
        {
            if (dtPickUp.Rows.Count <= 0) return;
            bs.DataSource = dtPickUp;
            if (searchText == string.Empty) return;
            bs.Filter = $"{string.Format("Convert(" + SearchCol + ", 'System.String')")} like '%{searchText}%'";
            if (bs.Count <= 0) return;
            RGrid.DataSource = dtPickUp;
            rowIndex = RGrid.CurrentCell.RowIndex;
            if (RGrid.CurrentCell.ColumnIndex != 0 && rowIndex > 0)
                RGrid.CurrentCell = RGrid.Rows[rowIndex].Cells[colIndex];
            if (RGrid.Rows[0].Cells[colIndex].Value.ToString() == searchText)
                RGrid.Rows[0].Cells[colIndex].Selected = true;
        }
    }
}