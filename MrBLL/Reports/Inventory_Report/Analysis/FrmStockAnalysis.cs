using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.Analysis;

public partial class FrmStockAnalysis : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string FromADDate;
    private string FromBSDate;
    private string GodownId = string.Empty;
    private string InvoiceNo = string.Empty;
    private string ProductGroupId = string.Empty;
    private string ProductId = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmStockAnalysis()
    {
        InitializeComponent();
    }

    private void FrmStockAnalysis_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        ObjGlobal.BindBranch(cmb_Branch);
        BindGroupItem();
        cmb_GroupBy.SelectedIndex = 0;
        cmbSysDateType.SelectedIndex = 8;
        cmbSysDateType.Focus();
    }

    private void FrmStockAnalysis_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
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
        if (cmb_GroupBy.SelectedIndex == 0 && chk_SelectAll.Checked == false)
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Check");
            HeaderCap.Add("Description");
            HeaderCap.Add("ShortName");
            HeaderCap.Add("Id");
            ColumnWidths.Add(50);
            ColumnWidths.Add(250);
            ColumnWidths.Add(100);
            ColumnWidths.Add(0);
            Query =
                "SELECT CONVERT(bit, 0) Chk, PName as Name,PShortName as Code,PId FROM AMS.Product  Where Status=1 Order By  PName";
            var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Product List");
            if (PkLst.ShowDialog() == DialogResult.OK)
            {
                if (ClsTagList.PlValue1 != null && ClsTagList.PlValue1 != string.Empty)
                {
                    ProductId = ClsTagList.PlValue3;
                }
                else
                {
                    MessageBox.Show("Product Not Selected!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Product Not Selected!");
                return;
            }
        }

        if (ObjGlobal.SysDateType == "M")
        {
            FromADDate = ObjGlobal.ReturnEnglishDate(msk_FromDate.Text);
            FromBSDate = msk_FromDate.Text;
            ToADDate = ObjGlobal.ReturnEnglishDate(msk_ToDate.Text);
            ToBSDate = msk_ToDate.Text;
        }
        else
        {
            FromADDate = msk_FromDate.Text;
            FromBSDate = ObjGlobal.ReturnNepaliDate(msk_FromDate.Text);
            ToADDate = msk_ToDate.Text;
            ToBSDate = ObjGlobal.ReturnNepaliDate(msk_ToDate.Text);
        }

        RptType = "Normal";
        RptName = "Stock Analysis Report - Product Wise";
        if (ObjGlobal.SysDateType == "M")
            RptDate = "From Date " + FromBSDate + " To " + ToBSDate;
        else
            RptDate = "From Date " + Convert.ToDateTime(FromADDate).ToShortDateString() + " To " +
                      Convert.ToDateTime(ToADDate).ToShortDateString();

        ProductGroupId = string.Empty;
        ProductId = string.Empty;
        GodownId = string.Empty;
    }

    private void BindGroupItem()
    {
        cmb_GroupBy.Enabled = true;
        var list = new List<GroupBy>
        {
            new(1, "Product", "1"),
            new(2, "Product Group", "2"),
            new(3, "Product Sub Group", "3")
        };
        cmb_GroupBy.DataSource = list;
        cmb_GroupBy.DisplayMember = "Name";
        cmb_GroupBy.ValueMember = "Value";
    }

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", cmbSysDateType.SelectedIndex, msk_FromDate, msk_ToDate);
    }

    private void cmbSysDateType_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(cmbSysDateType, 'E');
    }

    private void cmbSysDateType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(cmbSysDateType, 'L');
    }

    private void cmbSysDateType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
            SendKeys.Send("{TAB}");
    }

    private void cmbSysDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32)
            SendKeys.Send("{F4}");
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show(@"Are you sure want to Close Form!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes) Close();
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