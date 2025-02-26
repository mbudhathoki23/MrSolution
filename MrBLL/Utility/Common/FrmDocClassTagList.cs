using Microsoft.VisualBasic;
using MrDAL.Control.WinControl;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MrDAL.Global.Control;

namespace MrBLL.Utility.Common;

public partial class FrmDocClassTagList : MrForm
{
    private readonly ClsTagList obj = new();
    public string C_Search = string.Empty;
    private int currentColumn;
    private DataTable dt = new();
    private DataTable dtTagUp = new();
    private ObjGlobal Gobj = new();
    private string Query = string.Empty;
    private int rowIndex;
    private string SearchString = string.Empty;
    private string Str = string.Empty;

    private int TotGridWidth;

    public FrmDocClassTagList()
    {
        InitializeComponent();
    }

    public FrmDocClassTagList(string SelectQuery, ArrayList HeaderCap, ArrayList ColumnWidths, string FrmName)
    {
        InitializeComponent();
        obj.TagList.SelectQuery = SelectQuery;
        obj.TagList.HeaderCap = HeaderCap;
        obj.TagList.ColumnWidths = ColumnWidths;
        obj.TagList.FrmName = FrmName;
    }

    public string SelectQuery { get; set; }

    private void FrmDocClassTagList_Load(object sender, EventArgs e)
    {
        Text = obj.TagList.FrmName;
        ObjGlobal.DgvBackColor(dgv_ClassList);
        ///------****--------------Bind Header & Data----------*****------------
        if (obj.TagList.SelectQuery != string.Empty && obj.TagList.SelectQuery != null)
        {
            if (obj.TagList.ConType == "MASTER")
            {
                dtTagUp.Reset();
                dtTagUp = GetConnection.SelectQueryFromMaster(obj.TagList.SelectQuery);
            }
            else
            {
                dtTagUp.Reset();
                dtTagUp = GetConnection.SelectDataTableQuery(obj.TagList.SelectQuery);
            }

            if (dtTagUp.Rows.Count <= 0)
            {
                ClsPickList.PlValue1 = string.Empty;
                ClsPickList.PlValue2 = string.Empty;
                ClsPickList.PlValue3 = string.Empty;
                MessageBox.Show(@"No Data Found To Display", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                Close();
                return;
            }

            dgv_ClassList.Rows.Clear();
            dgv_ClassList.Columns.Clear();
            dgv_ClassList.DataSource = dtTagUp;

            for (var i = 0; i < obj.TagList.HeaderCap.Count; i++)
                if (Convert.ToInt16(obj.TagList.ColumnWidths[i]) > 0)
                {
                    dgv_ClassList.Columns[i].HeaderText = obj.TagList.HeaderCap[i].ToString();
                    if (dgv_ClassList.Columns[i].HeaderText == "Check")
                    {
                        dgv_ClassList.Columns[i].ReadOnly = false;
                        dgv_ClassList.Columns[i].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleCenter;
                        dgv_ClassList.Columns[i].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
                    }
                    else
                    {
                        dgv_ClassList.Columns[i].ReadOnly = true;
                        dgv_ClassList.Columns[i].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleLeft;
                        dgv_ClassList.Columns[i].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
                    }

                    dgv_ClassList.Columns[i].Width = Convert.ToInt16(obj.TagList.ColumnWidths[i].ToString());
                    TotGridWidth = TotGridWidth + Convert.ToInt16(obj.TagList.ColumnWidths[i]);
                }

            dgv_ClassList.Width = 45 + TotGridWidth;
            Width = dgv_ClassList.Width + 10;
            lbl_SearchValue.Width = TotGridWidth - 15;

            if (dgv_ClassList.Rows.Count > 10)
            {
                lbl_SearchValue.Width = TotGridWidth;
                dgv_ClassList.Width = dgv_ClassList.Width + 15;
                Width = Width + 15;
            }
        }
        else
        {
            LoadData();
        }

        dgv_ClassList.Focus();
    }

    private void FrmDocClassTagList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            Close();
        }
        else
        {
            if (e.KeyChar == 6)
                //txt_Find.focus();
                if (e.KeyChar == 19)
                    dgv_ClassList.Focus();

            if (ActiveControl.Name == "TxtFindValue")
                return;

            if (Strings.InStr(
                    @"ABCDEFGHIJKLMNOPQRSTUVWYXZabcdefghijklmnopqrstuvwxy z`~!@#$%^&*()_+1234567890-=/ <>.",
                    e.KeyChar.ToString().ToUpper()) > 0) C_Search = C_Search + e.KeyChar.ToString().ToUpper();
            if (e.KeyChar == 8)
                if (C_Search.Length > 0)
                    C_Search = C_Search.Substring(0, C_Search.Length - 1);
            if (C_Search.Trim() != string.Empty)
                lbl_SearchValue.Text = C_Search;
            if (e.KeyChar == 39)
                e.KeyChar = '0';
        }
    }

    private void CmdCancel_Click(object sender, EventArgs e)
    {
        ClsTagList.PlValue1 = string.Empty;
        ClsTagList.PlValue2 = string.Empty;
        ClsTagList.PlValue3 = string.Empty;
        Close();
    }

    private void CmdOk_Click(object sender, EventArgs e)
    {
        ShowList();
    }

    private void dgv_TagList_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void dgv_TagList_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void dgv_TagList_KeyDown(object sender, KeyEventArgs e)
    {
        dgv_ClassList.Rows[rowIndex].Selected = true;
        if (e.KeyCode == Keys.Enter)
        {
            dgv_ClassList.Rows[rowIndex].Selected = true;
            e.SuppressKeyPress = true;

            if (dgv_ClassList.CurrentRow.Cells[0].Value == null)
                dgv_ClassList.CurrentRow.Cells[0].Value = string.Empty;
        }
    }

    private void dgv_TagList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 6)
            return;
        if (e.KeyChar == 16)
            return;
    }

    private void dgv_TagList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
    }

    private void dgv_TagList_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {
            if (currentColumn != 0)
            {
                return;
            }

            if (dgv_ClassList.Rows.Count <= 0)
            {
                return;
            }

            for (var i = 0; i < dgv_ClassList.Rows.Count; i++)
            {
                if (dgv_ClassList.Rows[i].Cells[4].Value.ToString() !=
                    dgv_ClassList.CurrentRow?.Cells[4].Value.ToString())
                {
                    continue;
                }

                if (dgv_ClassList.CurrentRow != dgv_ClassList.Rows[i])
                {
                    dgv_ClassList.Rows[i].Cells[0].Value = false;
                }
            }
        }
        catch (Exception exc)
        {
            exc.ToNonQueryErrorResult(exc.StackTrace);
            exc.DialogResult();
        }
    }

    private void LoadData()
    {
        Query = SelectQuery;
        dt.Reset();
        dgv_ClassList.Rows.Clear();

        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow ro in dt.Rows)
            {
                var rows = dgv_ClassList.Rows.Count;
                dgv_ClassList.Rows.Add();
                dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[0].Value = ro[0].ToString();
                dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[1].Value = ro[1].ToString();
                dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[2].Value = ro[2].ToString();
            }
        }
        else
        {
            MessageBox.Show(@"NO DATA FOUND TO DISPLAY", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            Close();
            return;
        }

        dgv_ClassList.ClearSelection();
    }

    private void ShowList()
    {
        if (dgv_ClassList.Rows.Count > 0)
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            ClsTagList.PlValue4 = string.Empty;
            ClsTagList.PlValue5 = string.Empty;
            foreach (DataGridViewRow dr in dgv_ClassList.Rows)
                if (Convert.ToBoolean(dr.Cells[0].Value))
                    for (var Co = 0; Co < dgv_ClassList.Columns.Count; Co++)
                        if (Co == 0)
                            ClsTagList.PlValue1 = ClsTagList.PlValue1 + dr.Cells[0].Value.ToString().Trim() + ",";
                        else if (Co == 1)
                            ClsTagList.PlValue2 = ClsTagList.PlValue2 + dr.Cells[1].Value.ToString().Trim() + ",";
                        else if (Co == 2)
                            ClsTagList.PlValue3 = ClsTagList.PlValue3 + dr.Cells[2].Value.ToString().Trim() + ",";
                        else if (Co == 3)
                            ClsTagList.PlValue4 = ClsTagList.PlValue4 + dr.Cells[3].Value.ToString().Trim() + ",";
                        else if (Co == 4)
                            ClsTagList.PlValue5 = ClsTagList.PlValue5 + dr.Cells[4].Value.ToString().Trim() + ",";

            if (ClsTagList.PlValue1.Length > 0)
                ClsTagList.PlValue1 = ClsTagList.PlValue1.Remove(ClsTagList.PlValue1.Length - 1);
            if (ClsTagList.PlValue2.Length > 0)
                ClsTagList.PlValue2 = ClsTagList.PlValue2.Remove(ClsTagList.PlValue2.Length - 1);
            if (ClsTagList.PlValue3.Length > 0)
                ClsTagList.PlValue3 = ClsTagList.PlValue3.Remove(ClsTagList.PlValue3.Length - 1);
            if (ClsTagList.PlValue4.Length > 0)
                ClsTagList.PlValue4 = ClsTagList.PlValue4.Remove(ClsTagList.PlValue4.Length - 1);
            if (ClsTagList.PlValue5.Length > 0)
                ClsTagList.PlValue5 = ClsTagList.PlValue5.Remove(ClsTagList.PlValue5.Length - 1);
        }
        else
        {
            MessageBox.Show(@"Not Selected!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dgv_ClassList.Focus();
        }
    }

    private void lbl_SearchValue_TextChanged(object sender, EventArgs e)
    {
        foreach (DataGridViewRow ro in dgv_ClassList.Rows)
        {
            var cell = ro.Cells[currentColumn];
            cell.Style.BackColor = Color.FloralWhite;
            cell.Style.SelectionBackColor = SystemColors.Highlight;
        }

        if (lbl_SearchValue.Text == string.Empty)
        {
            var cell = dgv_ClassList.Rows[0].Cells[currentColumn];
            cell.Style.BackColor = SystemColors.Highlight;
        }

        if (lbl_SearchValue.Text != string.Empty)
        {
            int i;
            for (i = 0; i < dgv_ClassList.Rows.Count; i++)
            {
                var cell = dgv_ClassList.Rows[rowIndex].Cells[currentColumn];
                cell.Style.BackColor = Color.FloralWhite;
                if (dgv_ClassList.Rows[i].Cells[currentColumn].Value != null)
                    if (dgv_ClassList.Rows[i].Cells[currentColumn].Value.ToString() != string.Empty)
                        if (dgv_ClassList.Rows[i].Cells[currentColumn].Value.ToString().Trim().Substring(0)
                                .Length >= lbl_SearchValue.Text.Trim().Length)
                            if (dgv_ClassList.Rows[i].Cells[currentColumn].Value.ToString().Trim()
                                    .Substring(0, lbl_SearchValue.Text.Trim().Length).ToUpper() ==
                                lbl_SearchValue.Text.ToUpper().Trim())
                            {
                                dgv_ClassList.ClearSelection();
                                rowIndex = i;
                                //dgv_PickList.Rows[i].Selected = true;
                                //dgv_PickList.Rows[i].DefaultCellStyle.BackColor= System.Drawing.Color.FloralWhite;
                                cell = dgv_ClassList.Rows[rowIndex].Cells[currentColumn];
                                cell.Style.BackColor = SystemColors.Highlight;
                                dgv_ClassList.CurrentCell = dgv_ClassList.Rows[i].Cells[currentColumn];
                                dgv_ClassList.Focus();
                                return;
                            }
            }
        }
    }

    private void GridDesignSetting()
    {
        DataGridViewColumn column = new DataGridViewCheckBoxColumn();
        column.HeaderText = "Check";
        column.Name = "gv_Check";
        column.Width = 50;
        column.ReadOnly = false;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_ClassList.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Class_Code";
        column.Name = "gv_ClassCode";
        column.Width = 100;
        column.ReadOnly = true;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_ClassList.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Class";
        column.Name = "gv_Class";
        column.Width = 250;
        column.ReadOnly = true;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_ClassList.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Class_Id";
        column.Name = "gv_ClassId";
        column.Width = 0;
        column.Visible = false;
        column.ReadOnly = true;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_ClassList.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Segment";
        column.Name = "gv_Segment";
        column.Width = 0;
        column.Visible = false;
        column.ReadOnly = true;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_ClassList.Columns.Add(column);

        dgv_ClassList.Columns[1].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_ClassList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        foreach (DataRow ro in dtTagUp.Rows)
        {
            dgv_ClassList.Rows.Add();
            dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[0].Value = ro["Chk"].ToString();
            dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[1].Value = ro["Class_Code"].ToString();
            dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[2].Value = ro["Class"].ToString();
            dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[3].Value = ro["Class_Id"].ToString();
            dgv_ClassList.Rows[dgv_ClassList.RowCount - 1].Cells[4].Value = ro["Segment"].ToString();
        }

        ObjGlobal.DgvBackColor(dgv_ClassList);
    }
}