using MrBLL.Setup.BranchSetup;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Setup.BusinessUnit;

public partial class FrmCompanyUnitList : MrForm
{
    // COMPANY UNIT LIST

    #region --------------- COMPANY UNIT LIST ---------------

    public FrmCompanyUnitList()
    {
        InitializeComponent();
    }

    private void FrmCompanyUnitList_Load(object sender, EventArgs e)
    {
        ObjGlobal.DGridColorCombo(RGrid);
        BindCompanyUnitDetl();
        RGrid.Focus();
    }

    private void FrmCompanyUnitList_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show(@"Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Application.Exit();
        }
    }

    private void FrmCompanyUnitList_FormClosing(object sender, FormClosingEventArgs e)
    {
        Hide();
        e.Cancel = true;
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        CompanyUnitLogin();
    }

    private void BtnClear_Click(object sender, EventArgs e)
    {
        new FrmBranchList
        {
            Owner = this
        }.Show();
        Hide();
    }

    private void RGrid_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void RGrid_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void RGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) //|| e.KeyCode == Keys.Tab
        {
            RGrid.Rows[rowIndex].Selected = true;
            e.SuppressKeyPress = true;

            CompanyUnitLogin();
        }
    }

    private void RGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        CompanyUnitLogin();
    }

    private void RGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        CompanyUnitLogin();
    }

    private void RGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        CompanyUnitLogin();
    }

    #endregion --------------- COMPANY UNIT LIST ---------------

    // METHOD FOR THIS FORM

    #region --------------- METHOD ---------------

    private void CompanyUnitLogin()
    {
        if (RGrid.Rows.Count > 0)
        {
            if (RGrid.CurrentRow != null)
            {
                ObjGlobal.SysCompanyUnitId = RGrid.CurrentRow.Cells[0].Value.GetInt();
                ObjGlobal.CompanyUnitName = RGrid.CurrentRow.Cells[1].Value.ToString();
            }
            DialogResult = DialogResult.OK;
            Hide();
        }
        else
        {
            MessageBox.Show(@"NO UNIT TO SELECT..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }
    }

    private void BindCompanyUnitDetl()
    {
        Query = "SELECT * FROM AMS.CompanyUnit ";
        dt.Reset();
        RGrid.Rows.Clear();
        dt = GetConnection.SelectDataTableQuery(Query);
        if (dt.Rows.Count <= 0) return;
        foreach (DataRow ro in dt.Rows)
        {
            var rows = RGrid.Rows.Count;
            RGrid.Rows.Add();
            RGrid.Rows[RGrid.RowCount - 1].Cells["txt_CmpUnitName"].Value =
                ro["CmpUnit_Name"].ToString();
            RGrid.Rows[RGrid.RowCount - 1].Cells["txt_CmpUnitId"].Value =
                ro["CmpUnit_Id"].ToString();
        }
    }

    #endregion --------------- METHOD ---------------

    // OBJECT FOR THIS FORM
    private DataTable dt = new();

    private ObjGlobal Gobj = new();
    private string Query = string.Empty;
    private int rowIndex;
    private int currentColumn;
}