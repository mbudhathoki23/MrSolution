using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Analysis_Report.Purchase;

public partial class FrmPurchaseAnalysis : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private readonly string InvoiceNo = string.Empty;
    private string FromADDate;
    private string FromBSDate;
    private string Ledger_Code = string.Empty;
    private string Product_Code = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmPurchaseAnalysis()
    {
        InitializeComponent();
    }

    private void FrmPurchaseAnalysis_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 0);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        BindUnit();
        rb_Product.Checked = true;
        cmb_FilteredBy.SelectedIndex = 0;
        cmb_GroupBy.SelectedIndex = 0;
        ObjGlobal.BindBranch(cmb_Branch);
        if (ObjGlobal.SysDateType == "M")
            chk_Date.Text = "Date";
        else
            chk_Date.Text = "Miti";

        msk_FromDate.Focus();
    }

    private void FrmPurchaseAnalysis_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void rb_Product_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_Product.Checked)
        {
            chk_BillAgent.Enabled = false;
            chk_SelectAllRptType.Text = "Select All Product";
            chk_SelectAllFilterBy.Text = "Select All Product";
            cmb_FilteredBy.Enabled = true;
            //cmb_FilteredBy.Items.ClearControl();
            var list = new List<GroupBy>
            {
                new(1, "None", "1"),
                new(2, "Party", "2"),
                new(3, "Agent", "3")
            };
            cmb_FilteredBy.DataSource = list;
            cmb_FilteredBy.DisplayMember = "Name";
            cmb_FilteredBy.ValueMember = "Value";
        }
    }

    private void rb_Party_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_Party.Checked)
        {
            chk_BillAgent.Enabled = false;
            chk_SelectAllRptType.Text = "Select All Party";
            chk_SelectAllFilterBy.Text = "Select All Party";
            cmb_FilteredBy.Enabled = true;
            //cmb_FilteredBy.Items.ClearControl();
            var list = new List<GroupBy>
            {
                new(1, "None", "1"),
                new(2, "Product", "2"),
                new(3, "Agent", "3")
            };
            cmb_FilteredBy.DataSource = list;
            cmb_FilteredBy.DisplayMember = "Name";
            cmb_FilteredBy.ValueMember = "Value";
        }
    }

    private void rb_Agent_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_Agent.Checked)
        {
            chk_BillAgent.Enabled = true;
            chk_SelectAllRptType.Text = "Select All Agent";
            chk_SelectAllFilterBy.Text = "Select All Agent";
            cmb_FilteredBy.Enabled = true;
            //cmb_FilteredBy.Items.ClearControl();
            var list = new List<GroupBy>
            {
                new(1, "None", "1"),
                new(2, "Product", "2"),
                new(3, "Party", "3")
            };
            cmb_FilteredBy.DataSource = list;
            cmb_FilteredBy.DisplayMember = "Name";
            cmb_FilteredBy.ValueMember = "Value";
        }
    }

    private void rb_Area_CheckedChanged(object sender, EventArgs e)
    {
        if (rb_Area.Checked)
        {
            chk_BillAgent.Enabled = false;
            chk_SelectAllRptType.Text = "Select All Area";
            chk_SelectAllFilterBy.Text = "Select All Area";
            cmb_FilteredBy.Enabled = true;
            //cmb_FilteredBy.Items.ClearControl();
            var list = new List<GroupBy>
            {
                new(1, "None", "1"),
                new(2, "Product", "2"),
                new(3, "Party", "3")
            };
            cmb_FilteredBy.DataSource = list;
            cmb_FilteredBy.DisplayMember = "Name";
            cmb_FilteredBy.ValueMember = "Value";
        }
    }

    private void cmb_FilteredBy_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    public void BindUnit()
    {
        //var obj = new ObjGlobal();
        //var ReportTable = ObjGlobal.BindUnitForInventoryRpt();
        //if (ReportTable.Rows.Count > 0)
        //{
        //    cmb_Unit.DataSource = ReportTable;
        //    cmb_Unit.DisplayMember = "Unit_Code";
        //    cmb_Unit.ValueMember = "Unit_Id";
        //}
    }

    private void msk_FromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'E');
    }

    private void msk_FromDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_FromDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'L');
    }

    private void msk_FromDate_Validated(object sender, EventArgs e)
    {
    }

    private void msk_ToDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'E');
    }

    private void msk_ToDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_ToDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_ToDate, 'L');
    }

    private void msk_ToDate_Validated(object sender, EventArgs e)
    {
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }

    private class GroupBy
    {
        public GroupBy(int s, string n, string v)
        {
            SNo = s;
            Name = n;
            Value = v;
        }

        public int SNo { get; }
        public string Name { get; }
        public string Value { get; }
    }
}