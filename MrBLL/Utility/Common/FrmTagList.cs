using Microsoft.VisualBasic;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.Interface;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static MrDAL.Utility.PickList.ClsTagList;

namespace MrBLL.Utility.Common;

public sealed partial class FrmTagList : MrForm
{
    // TAG LIST FORM

    #region ---------------  FrmTagList ---------------

    public FrmTagList()
    {
        InitializeComponent();
    }

    public static FrmTagList CreateInstance(string selectQuery, ArrayList headerCap, ArrayList columnWidths, string frmName)
    {
        return new FrmTagList(selectQuery, headerCap, columnWidths, frmName);
    }

    private FrmTagList(string selectQuery, ArrayList headerCap, ArrayList columnWidths, string frmName)
    {
        InitializeComponent();
        Text = _objGetTag.TagList.FrmName;
        _objGetTag.TagList.SelectQuery = selectQuery;
        _objGetTag.TagList.HeaderCap = headerCap;
        _objGetTag.TagList.ColumnWidths = columnWidths;
        _objGetTag.TagList.FrmName = frmName;
        BindData();
    }

    private void FrmTagList_Load(object sender, EventArgs e)
    {
        SGrid.Focus();
        if (!string.IsNullOrEmpty(ReportDesc))
        {
            PlValue1 = string.Empty;
            PlValue2 = string.Empty;
            PlValue3 = string.Empty;
            GetMasterList(ReportDesc);
        }

        ObjGlobal.DgvBackColor(SGrid);
    }

    private void FrmTagList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            Close();
            return;
        }

        if (e.KeyChar == 6 && e.KeyChar == 19)
        {
            SGrid.Focus();
        }

        if (ActiveControl.Name == "TxtFindValue") return;
        if (Strings.InStr(@"ABCDEFGHIJKLMNOPQRSTUVWYXZabcdefghijklmnopqrstuvwxy z`~!@#$%^&*()_+1234567890-=/ <>.",
                e.KeyChar.ToString().ToUpper()) > 0)
        {
            CSearch += e.KeyChar.ToString().ToUpper();
        }

        if (e.KeyChar == 8)
        {
            if (CSearch != null)
            {
                CSearch = CSearch.Length > 0 ? CSearch.Substring(0, CSearch.Length - 1) : CSearch;
            }
        }

        TxtSearch.Text = CSearch;
        if (e.KeyChar == 39)
        {
            e.KeyChar = '0';
        }
    }

    private void TxtSearch_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            SGrid.Focus();
        }
    }

    private void TxtFindValue_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtFindValue, 'E');
    }

    private void TxtFindValue_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyValue is 13 or 9)
        {
            BtnFind_Click(sender, e);
        }
    }

    private void TxtFindValue_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(TxtFindValue, 'L');
        if (TxtFindValue.Text.Trim() != string.Empty)
        {
            BtnFind_Click(sender, e);
        }
    }

    private void BtnFind_Click(object sender, EventArgs e)
    {
        foreach (DataGridViewRow ro in SGrid.Rows) ro.DefaultCellStyle.SelectionBackColor = Color.FloralWhite;
        if (TxtFindValue.Text == string.Empty) return;
        int i;
        for (i = 0; i < SGrid.Rows.Count; i++)
        {
            var result = SGrid.Rows[i].Cells[CurrentColumn].Value.ToString().Trim();
            SGrid.Rows[i].DefaultCellStyle.BackColor = Color.FloralWhite;
            if (!result.IsValueExits())
            {
                continue;
            }

            if (result.Substring(0).Length < TxtFindValue.Text.Trim().Length)
            {
                continue;
            }
            if (result.Substring(0, TxtFindValue.Text.Trim().Length).ToUpper() != TxtFindValue.Text.ToUpper().Trim())
            {
                continue;
            }
            SGrid.ClearSelection();
            SGrid.Rows[i].Selected = true;
            SGrid.Rows[i].DefaultCellStyle.BackColor = SystemColors.Highlight;
            SGrid.Focus();
            RowIndex = i;
            return;
        }
    }

    private void rChkTagAll_CheckedChanged(object sender, EventArgs e)
    {
        switch (rb_TagAll.Checked)
        {
            case true:
            {
                foreach (DataGridViewRow ro in SGrid.Rows)
                {
                    ro.Selected = true;
                    ro.Cells[0].Value = true;
                    ro.DefaultCellStyle.BackColor = SystemColors.Highlight;
                }

                break;
            }
            case false:
            {
                foreach (DataGridViewRow ro in SGrid.Rows)
                {
                    ro.Selected = false;
                    ro.Cells[0].Value = false;
                    ro.DefaultCellStyle.BackColor = Color.FloralWhite;
                }

                break;
            }
        }
    }

    private void rChkUnTagAll_CheckedChanged(object sender, EventArgs e)
    {
        if (!rb_UnTagAll.Checked) return;
        for (var index = 0; index < SGrid.Rows.Count; index++)
        {
            var ro = SGrid.Rows[index];
            ro.Selected = false;
            ro.Cells[0].Value = false;
            ro.DefaultCellStyle.BackColor = Color.FloralWhite;
        }
    }

    private void LblSearchValue_TextChanged(object sender, EventArgs e)
    {
        SGrid.Focus();
        SGridSearch(TxtSearch.Text);
    }

    private void CmdCancel_Click(object sender, EventArgs e)
    {
        PlValue1 = string.Empty;
        PlValue2 = string.Empty;
        PlValue3 = string.Empty;
        Close();
    }

    private void CmdOk_Click(object sender, EventArgs e)
    {
        ShowList();
        DialogResult = DialogResult.OK;
    }

    #endregion ---------------  FrmTagList ---------------

    // GRID CONTROL EVENTS

    #region ---------------  FrmTagList ---------------

    private void SGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        if (SGrid.CurrentRow != null && SGrid.CurrentRow.Cells["GTxtCheck"].Value.GetBool())
        {
            SGrid.CurrentRow.Selected = false;
            SGrid.CurrentRow.Cells["GTxtCheck"].Value = false;
        }
        else
        {
            if (SGrid.CurrentRow != null)
            {
                SGrid.CurrentRow.Selected = true;
                SGrid.CurrentRow.Cells["GTxtCheck"].Value = true;
            }
        }

        if (SGrid.Rows.Count - RowIndex >= 2)
        {
            SGrid.CurrentCell = SGrid.Rows[RowIndex + 1].Cells[CurrentColumn];
        }
    }

    private void SGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        e.SuppressKeyPress = true;
        if (SGrid.CurrentRow != null && ObjGlobal.ReturnBool(SGrid.CurrentRow.Cells["GTxtCheck"].Value?.ToString()))
        {
            SGrid.CurrentRow.Selected = false;
            SGrid.CurrentRow.Cells["GTxtCheck"].Value = false;
        }
        else
        {
            if (SGrid.CurrentRow != null)
            {
                SGrid.CurrentRow.Selected = true;
                SGrid.CurrentRow.Cells["GTxtCheck"].Value = true;
            }

            if (SGrid.Rows.Count - RowIndex >= 2) SGrid.CurrentCell = SGrid.Rows[RowIndex + 1].Cells[CurrentColumn];
        }
    }

    private void SGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        RowIndex = e.RowIndex;
    }

    private void SGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        CurrentColumn = e.ColumnIndex;
    }

    private void SGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (SGrid.CurrentRow != null && Convert.ToBoolean(SGrid.CurrentRow.Cells[0].Value))
        {
            SGrid.CurrentRow.Selected = false;
            SGrid.CurrentRow.Cells[0].Value = false;
        }
        else
        {
            if (SGrid.CurrentRow == null) return;
            SGrid.CurrentRow.Selected = true;
            SGrid.CurrentRow.Cells[0].Value = true;
        }
    }

    private void SGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    #endregion ---------------  FrmTagList ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void LoadData()
    {
        Query = SelectQuery;
        _table.Reset();
        SGrid.Rows.Clear();
        _table = GetConnection.SelectDataTableQuery(Query);
        if (_table.Rows.Count > 0)
        {
            foreach (DataRow ro in _table.Rows)
            {
                var rows = SGrid.Rows.Count;
                SGrid.Rows.Add();
                SGrid.Rows[SGrid.RowCount - 1].Cells[0].Value = ro[0].ToString();
                SGrid.Rows[SGrid.RowCount - 1].Cells[1].Value = ro[1].ToString();
                SGrid.Rows[SGrid.RowCount - 1].Cells[2].Value = ro[2].ToString();
            }
        }
        else
        {
            MessageBox.Show(@"NO DATA FOUND TO DISPLAY..!!", ObjGlobal.Caption);
            Close();
            return;
        }

        SGrid.ClearSelection();
    }

    private void ShowList()
    {
        try
        {
            //var checkValue = _table.AsEnumerable().Where(r => r["Chk"].GetBool()== true);
            var checkValue = _table.AsEnumerable().Where(r => r["IsCheck"].GetBool() == true);
            //var checkValue = _table.AsEnumerable().FirstOrDefault(r => r.Field<bool>("Chk") == true);
            if (checkValue.GetHashCode() > 0)
            {
                foreach (var row in checkValue)
                {
                    if (ReportDesc is "USER")
                    {
                        PlValue3 += row[2] + ",";
                    }
                    else
                    {
                        PlValue3 += row[1] + ",";
                    }

                }
                PlValue1 = PlValue3.TrimEnd(',');
            }
            else
            {
                MessageBox.Show(@"NOT VALUE SELECTED/CHECKED..!!", ObjGlobal.Caption);
                SGrid.Focus();
            }

        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }
    }

    private void BindData()
    {
        if (!string.IsNullOrEmpty(_objGetTag.TagList.SelectQuery))
        {
            if (_objGetTag.TagList.ConType == "MASTER")
            {
                _dtTagUp.Reset();
                _dtTagUp = GetConnection.SelectQueryFromMaster(_objGetTag.TagList.SelectQuery);
            }
            else
            {
                _dtTagUp.Reset();
                _dtTagUp = GetConnection.SelectDataTableQuery(_objGetTag.TagList.SelectQuery);
                _table = _dtTagUp;
            }

            if (_dtTagUp.Rows.Count <= 0)
            {
                PlValue1 = string.Empty;
                PlValue2 = string.Empty;
                PlValue3 = string.Empty;
                CustomMessageBox.Warning(@"NO DATA FOUND TO DISPLAY..!!");
                Close();
                return;
            }

            SGrid.Rows.Clear();
            SGrid.Columns.Clear();
            SGrid.DataSource = _dtTagUp;
            SGrid.CurrentCell = SGrid.Rows[0].Cells[1];

            if (_objGetTag.TagList.FrmName == "Room No List")
            {
                SGrid.Columns[0].Visible = false;
            }
            for (var i = 0; i < _objGetTag.TagList.HeaderCap.Count; i++)
            {
                if (Convert.ToInt16(_objGetTag.TagList.ColumnWidths[i]) > 0)
                {
                    SGrid.Columns[i].HeaderText = _objGetTag.TagList.HeaderCap[i].ToString();
                    SGrid.Columns[i].Width = Convert.ToInt16(_objGetTag.TagList.ColumnWidths[i].ToString());

                    if (SGrid.Columns[i].HeaderText == @"IsCheck")
                    {
                        SGrid.Columns[i].ReadOnly = false;
                        SGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        SGrid.Columns[i].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
                    }
                    else
                    {
                        SGrid.Columns[i].ReadOnly = true;
                        SGrid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        SGrid.Columns[i].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
                    }
                }
                else
                {
                    SGrid.Columns[i].Visible = false;
                }
            }
            SearchColumn = SGrid.Columns[1].DataPropertyName;
        }
        else
        {
            LoadData();
        }
    }

    private void SGridSearch(string searchText)
    {
        if (!string.IsNullOrEmpty(searchText) && searchText != "F1")
        {
            Search(TxtSearch.Text);
        }
    }

    private void GetMasterList(string listType)
    {
        if (listType.IsBlankOrEmpty()) return;
        ReportDesc = listType;
        SearchColumn = "PARTICULAR";
        var result = listType switch
        {
            "ALL_GENERAL_LEDGER" => AllGeneralLedgerList(Category, Module),
            "GENERALLEDGER" or "GENERAL_LEDGER" or "GL" => GetGeneralLedgerList(Category, Module),
            "ACCOUNTGROUP" or "ACCOUNT_GROUP" or "AG" => GetMasterTransactionList(Category, Module),
            "ACCOUNTSUBGROUP" or "ACCOUNT_SUB_GROUP" or "ASG" => GetMasterTransactionList(Category, Module),
            "SUBLEDGER" => GetMasterTransactionList(Category, Module),
            "BRANCH" => GetMasterTransactionList(Category, Module),
            "FISCALYEAR" => GetMasterTransactionList(Category, Module),
            "PRODUCTGROUP" or "PRODUCT_GROUP" or "PG" => GetMasterTransactionList(Category, Module),
            "PRODUCTSUBGROUP" or "PRODUCT_SUB_GROUP" or "PSG" => GetMasterTransactionList(Category, Module),
            "PRODUCT" => GetProductList(Category, Module),
            "USER" => GetMasterTransactionList(Category, Module),
            "AGENT" => GetMasterTransactionList(Category, Module),
            "DOCAGENT" => GetMasterTransactionList(Category, Module),
            _ => false
        };
        if (!result)
        {
            return;
        }
    }

    private void Search(string searchText)
    {
        ((DataTable)SGrid.DataSource).DefaultView.RowFilter = $"{SearchColumn} LIKE '%{TxtSearch.Text.Trim()}%'";
        SGrid.Sort(SGrid.Columns[0], ListSortDirection.Descending);
    }

    private void RowSelected()
    {
        //if (SelectedIndex == null)
        //{
        //    return;
        //}
        //foreach (var t in SelectedIndex)
        //{
        //    var index = -1;
        //    foreach (DataGridViewRow row in SGrid.Rows)
        //    {
        //        if (!row.Cells[0].Value.ToString().Equals(t)) continue;
        //        index = row.Index;
        //        break;
        //    }

        //    if (index == -1) continue;
        //    SGrid.Rows[index].Cells["Tag"].Value = true;
        //    SGrid.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(156, 196, 197);
        //    SGrid.Rows[index].DefaultCellStyle.SelectionBackColor = Color.FromArgb(156, 196, 197);
        //}
    }

    #endregion --------------- METHOD ---------------

    // BIND THE VALUE IN GRID

    #region --------------- DEFAULT VALUE ---------------

    private bool GetMasterTransactionList(string category, string module)
    {
        if (SGrid.ColumnCount > 0) SGrid.Columns.Clear();
        SGrid.AutoGenerateColumns = false;
        _table = ReportDesc switch
        {
            "USER" => _objGetTag.GetEntryUserInfo(module),
            "BRANCH" => _objGetTag.GetEntryBranchList(),
            "FISCALYEAR" => _objGetTag.GetEntryFiscalYearList(),
            "COMPANYUNIT" => _objGetTag.GetEntryCompanyUnitList(),
            "ACCOUNTGROUP" or "ACCOUNT_GROUP" or "AG" => _objGetTag.GetAccountGroupList(),
            "ACCOUNTSUBGROUP" or "ACCOUNT_SUB_GROUP" or "ASG" => _objGetTag.GetAccountSubGroupList(GroupId),
            "SUBLEDGER" => _objGetTag.GetSubLedgerList(LedgerId, string.Empty),
            "PRODUCTGROUP" or "PRODUCT_GROUP" or "PG" => _objGetTag.GetProductGroupList(),
            "PRODUCTSUBGROUP" or "PRODUCT_SUB_GROUP" or "PSG" => _objGetTag.GetProductSubGroupList(GroupId),
            _ => new DataTable()
        };
        SGrid.DataSource = _table;
        SGrid.AddCheckColumn("GTxtCheck", "CHECK", "IsCheck", 75, 75);
        SGrid.AddColumn("GTxtParticularId", "PARTICULAR_ID", "ParticularId", 0, 0, false);
        SGrid.AddColumn("GTxtParticular", "PARTICULAR", "Particular", 250, 250, true, DataGridViewAutoSizeColumnMode.Fill);
        SGrid.AddColumn("GTxtShortName", "SHORTNAME", "ShortName", 250, 250, true);
        return SGrid.RowCount > 0;
    }

    private bool GetGeneralLedgerList(string category, string module)
    {
        if (SGrid.ColumnCount > 0) SGrid.Columns.Clear();
        SGrid.AddCheckColumn("GTxtCheck", "CHECK", "IsCheck", 75, 75);
        SGrid.AddColumn("GTxtParticularId", "ParticularId", "ParticularId", 0, 0, false);
        SGrid.AddColumn("GTxtParticular", "PARTICULAR", "Particular", 300, 300, true);
        SGrid.AddColumn("GTxtShortName", "SHORTNAME", "ShortName", 120, 120, true);
        SGrid.AddColumn("GTxtPanNo", "PAN_NO", "PanNo", 120, 120, true);
        SGrid.AddColumn("GTxtType", "TYPE", "GLType", 120, 120, true);
        SGrid.AddColumn("GTxtGroup", "GROUP", "GROUPDESC", 300, 120, true);
        SGrid.AddColumn("GTxtSubGroup", "SUB_GROUP", "SUBGROUPDESC", 200, 120, true);
        SGrid.AddColumn("GTxtAddress", "ADDRESS", "GLADDRESS", 300, 120, true);
        SGrid.AddColumn("GTxtSalesMan", "SALESMAN", "SALESMAN", 120, 120, true);
        SGrid.AutoGenerateColumns = false;
        _table = _objGetTag.GetAllGeneralLedgerList(Category, GroupId, SubGroupId, module);
        SGrid.DataSource = _table;
        return SGrid.RowCount > 0;
    }

    private bool AllGeneralLedgerList(string category, string module)
    {
        var dgvCmb = new DataGridViewCheckBoxColumn
        {
            ValueType = typeof(bool),
            Name = "GTxtCheck",
            HeaderText = "CHECK",
            DataPropertyName = "IsCheck"
        };
        SGrid.Columns.Add(dgvCmb);
        SGrid.Columns["GTxtCheck"].ReadOnly = false;
        SGrid.Columns["GTxtCheck"].Width = 75;

        SGrid.Columns.Add("LEDGERID", "LedgerId");
        SGrid.Columns["LEDGERID"].DataPropertyName = "LEDGERID";
        SGrid.Columns["LEDGERID"].Width = 0;
        SGrid.Columns["LEDGERID"].Visible = false;

        SGrid.Columns.Add("PARTICULAR", "DESCRIPTION");
        SGrid.Columns["PARTICULAR"].DataPropertyName = "PARTICULAR";
        SGrid.Columns["PARTICULAR"].Width = 450;

        SGrid.Columns.Add("SHORTNAME", "SHORTNAME");
        SGrid.Columns["SHORTNAME"].DataPropertyName = "SHORTNAME";
        SGrid.Columns["SHORTNAME"].Width = 120;

        SGrid.Columns.Add("LEDGERCODE", "LEDGERCODE");
        SGrid.Columns["LEDGERCODE"].DataPropertyName = "LEDGERCODE";
        SGrid.Columns["LEDGERCODE"].Width = 0;
        SGrid.Columns["LEDGERCODE"].Visible = false;

        SGrid.Columns.Add("PANNO", "PANNO");
        SGrid.Columns["PANNO"].DataPropertyName = "PANNO";
        SGrid.Columns["PANNO"].Width = 120;

        SGrid.Columns.Add("GLTYPE", "GLTYPE");
        SGrid.Columns["GLTYPE"].DataPropertyName = "GLTYPE";
        SGrid.Columns["GLTYPE"].Width = 120;

        SGrid.Columns.Add("GROUPDESC", "A/C GROUP");
        SGrid.Columns["GROUPDESC"].DataPropertyName = "GROUPDESC";
        SGrid.Columns["GROUPDESC"].Width = 250;

        SGrid.Columns.Add("SUBGROUPDESC", "SUBGROUP");
        SGrid.Columns["SUBGROUPDESC"].DataPropertyName = "SUBGROUPDESC";
        SGrid.Columns["SUBGROUPDESC"].Width = 120;

        SGrid.Columns.Add("GLADDRESS", "ADDRESS");
        SGrid.Columns["GLADDRESS"].DataPropertyName = "GLADDRESS";
        SGrid.Columns["GLADDRESS"].Width = 180;

        SGrid.AutoGenerateColumns = false;
        _table = _objGetTag.GetAllGeneralLedgerList(Category, GroupId, SubGroupId, module);
        SGrid.DataSource = _table;
        return SGrid.RowCount > 0;
    }

    private bool GetProductList(string category, string module)
    {
        if (SGrid.ColumnCount > 0) SGrid.Columns.Clear();
        SGrid.AutoGenerateColumns = false;
        _table = _objGetTag.GetProductList(GroupId, SubGroupId, module);
        SGrid.DataSource = _table;
        SGrid.AddCheckColumn("GTxtCheck", "CHECK", "IsCheck", 75, 75);
        SGrid.AddColumn("GTxtLedgerId", "PRODUCT_ID", "ParticularId", 0, 2, false);
        SGrid.AddColumn("PName", "DESCRIPTION", "PARTICULAR", 450, 450, true);
        SGrid.AddColumn("UnitCode", "UOM", "UnitCode", 90, 90, true);
        SGrid.AddColumn("GrpName", "GROUP", "GrpName", 250, 200, true);
        SGrid.AddColumn("SubGrpName", "UOM", "SubGrpName", 250, 200, true);
        return SGrid.RowCount > 0;
    }

    #endregion --------------- DEFAULT VALUE ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT ---------------

    private int RowIndex { get; set; }
    private int CurrentColumn { get; set; }
    public string SelectQuery { get; set; }
    public string ReportDesc { get; set; }
    public string Desc { get; set; }
    public string Category { get; set; }
    public string CSearch { get; set; }
    private string SearchColumn { get; set; }
    private string SearchKey { get; set; }
    private string Query { get; set; }
    private string Str { get; set; }
    private string SearchString { get; set; }
    private IEnumerable<string> SelectedIndex { get; } = null;
    private List<string> SelectedValue { get; set; }
    private DataTable _table = new();
    private DataTable _dtTagUp = new();
    private readonly ITagList _objGetTag = new ClsTagList();
    public string BranchId { get; set; }
    public string CompanyUnitId { get; set; }
    public string FiscalYearId { get; set; }
    public string GroupId { get; set; }
    public string SubGroupId { get; set; }
    public string SubLedgerId { get; set; }
    public string AgentId { get; set; }
    public string DepartmentId { get; set; }
    public string LedgerId { get; set; }
    public string Module { get; set; }
    public string ProductCompanyId { get; set; }

    #endregion --------------- OBJECT ---------------
}