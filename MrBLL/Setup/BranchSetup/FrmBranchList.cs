using MrBLL.Setup.CompanySetup;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Setup.BranchSetup;

public partial class FrmBranchList : MrForm
{
    //OBJECT FOR THIS FORM

    #region --------------- Global  ---------------

    private int _rowIndex;

    #endregion --------------- Global  ---------------

    // BRANCH LIST FORM

    #region --------------- BRANCH LIST FORM ---------------

    public FrmBranchList()
    {
        InitializeComponent();
        ObjGlobal.DGridColorCombo(DGrid);
        ObjGlobal.GetCompanyInfoDetails();
        ObjGlobal.BindFiscalYear(CmbFiscalYear);
    }

    private void FrmBranchList_Shown(object sender, EventArgs e)
    {
        CmbFiscalYear.SelectedValue = ObjGlobal.SysFiscalYearId;
        DGrid.Focus();
    }

    private void FrmBranchList_Load(object sender, EventArgs e)
    {
        BindBranchDetails();
    }

    private void FrmBranchList_FormClosing(object sender, FormClosingEventArgs e)
    {
        Hide();
        e.Cancel = true;
    }

    private void FrmBranchList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Escape) return;
        if (MessageBox.Show(@"ARE YOU SURE WANT TO RETURN TO COMPANY LIST..!!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        var frm1 = new FrmCompanyList(false) { Owner = this };
        frm1.Show();
        Hide();
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        BranchLogin();
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        var frm1 = new FrmCompanyList(false)
        {
            Owner = this
        };
        frm1.Show();
        Hide();
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        var currentColumn = e.ColumnIndex;
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        _rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        if (DGrid.Rows.Count != 0)
        {
            DGrid.Rows[_rowIndex].Selected = true;
            e.SuppressKeyPress = true;
            ObjGlobal.SysBranchId = Convert.ToInt16(DGrid.SelectedRows[0].Cells[0].Value);
            ObjGlobal.SysBranchName = DGrid.SelectedRows[0].Cells[1].Value.ToString();
            BranchLogin();
        }
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        BranchLogin();
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        BranchLogin();
    }

    private void RGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        BranchLogin();
    }

    #endregion --------------- BRANCH LIST FORM ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method ---------------

    internal void BranchLogin()
    {
        if (DGrid.CurrentRow != null)
        {
            ObjGlobal.SysBranchId = DGrid.CurrentRow.Cells[0].Value.GetInt();
            ObjGlobal.SysBranchName = DGrid.CurrentRow.Cells[1].Value.ToString();
        }
        ObjGlobal.SysFiscalYearId = CmbFiscalYear.SelectedValue.GetInt();
        DialogResult = DialogResult.OK;
        Close();
    }

    internal void BindBranchDetails()
    {
        DGrid.Rows.Clear();
        var query = ObjGlobal.LogInUser.ToUpper() is "ADMIN" || ObjGlobal.LogInUser.ToUpper() is "AMSADMIN"
            ? "Select * from AMS.Branch b"
            : $"Select * from AMS.Branch b LEFT OUTER JOIN AMS.BranchRights br ON br.BranchId = b.Branch_Id WHERE br.UserId='{ObjGlobal.LogInUserId}'";
        var dt = GetConnection.SelectDataTableQuery(query);
        if (dt is not { Rows: { Count: > 0 } }) return;
        foreach (DataRow ro in dt.Rows)
        {
            DGrid.Rows.Add();
            DGrid.Rows[DGrid.RowCount - 1].Cells["txt_BranchId"].Value = ro["Branch_Id"].ToString();
            DGrid.Rows[DGrid.RowCount - 1].Cells["txt_BranchName"].Value = ro["Branch_Name"].ToString();
        }
    }

    #endregion --------------- Method ---------------
}