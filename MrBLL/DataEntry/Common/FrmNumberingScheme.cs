using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrBLL.DataEntry.Common;

public partial class FrmNumberingScheme : MrForm
{
    //NUMBERING SCHEME

    #region --------------- NUMBERING SCHEME ---------------

    public FrmNumberingScheme()
    {
        InitializeComponent();
    }

    public FrmNumberingScheme(string module)
    {
        InitializeComponent();
        Source = module;
    }

    public FrmNumberingScheme(string source, string tblName, string fldName)
    {
        InitializeComponent();
        Source = source;
    }

    private void NumberingScheme_Load(object sender, EventArgs e)
    {
        BindDocumentNumbering();
        DGrid.Focus();
    }

    private void NumberingScheme_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 27) Close();
    }

    private void DGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void DGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        _currentColumn = e.ColumnIndex;
    }

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) //|| e.KeyCode == Keys.Tab
        {
            DGrid.Rows[_rowIndex].Selected = true;
            e.SuppressKeyPress = true;
            Desc = DGrid.SelectedRows[0].Cells[1].Value.ToString();
            VNo = _textBox.GetCurrentVoucherNo(Source, Desc);
            Description = Desc;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        DGrid.Rows[_rowIndex].Selected = true;
        Desc = DGrid.SelectedRows[0].Cells[1].Value.ToString();
        VNo = _textBox.GetCurrentVoucherNo(Source, Desc);
        Description = Desc;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    #endregion --------------- NUMBERING SCHEME ---------------

    #region --------------- METHOD ---------------

    private void BindDocumentNumbering()
    {
        _dtAutoGenerate.Reset();
        var cmdString = @$"
        SELECT DocId, DocDesc, 'Auto' DocType, DocStart, DocEnd, DocCurr
        FROM [AMS].DocumentNumbering
        WHERE DocModule='{Source}' AND(FiscalYearId='{ObjGlobal.SysFiscalYearId}' OR FiscalYearId IS NULL)AND(DocBranch='{ObjGlobal.SysBranchId}' OR DocBranch IS NULL) ";
        if (ObjGlobal.DomainLoginUser.Contains(ObjGlobal.LogInUser.GetUpper()))
        {
            cmdString += "";
        }
        else
        {
            cmdString += ObjGlobal.LogInUser.GetUpper() switch
            {
                "ADMIN" => $"AND (DocUser IN ('{ObjGlobal.LogInUser}','ALL') OR DocUser IS NULL) AND DocUser<> ''",
                _ => $@" AND DocUser = '{ObjGlobal.LogInUser}' ;"
            };
        }
        _dtAutoGenerate = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (_dtAutoGenerate.Rows.Count <= 0)
        {
            return;
        }
        DGrid.Rows.Add(_dtAutoGenerate.Rows.Count);
        foreach (DataRow dr in _dtAutoGenerate.Rows)
        {
            var index = _dtAutoGenerate.Rows.IndexOf(dr);
            DGrid.Rows[index].Cells[0].Value = dr["DocId"].ToString();
            DGrid.Rows[index].Cells[1].Value = dr["DocDesc"].ToString();
            DGrid.Rows[index].Cells[2].Value = dr["DocType"].ToString();
            DGrid.Rows[index].Cells[3].Value = dr["DocStart"].ToString();
            DGrid.Rows[index].Cells[4].Value = dr["DocEnd"].ToString();
            DGrid.Rows[index].Cells[5].Value = dr["DocCurr"].ToString();
        }
        ObjGlobal.DGridColorCombo(DGrid);
        DGrid.CurrentCell = DGrid.Rows[0].Cells[_currentColumn];
        DGrid.Rows[0].Selected = true;
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM
    private int _rowIndex;

    private readonly TextBox _textBox = new();
    private int _currentColumn;
    private DataTable _dtAutoGenerate = new();
    public string Source { get; set; }
    public string TblName { get; set; }
    public string FldNAme { get; set; }
    public string BillType { get; set; }
    public string VNo { get; set; }
    public string Description { get; set; }
    private string Desc { get; set; }
}