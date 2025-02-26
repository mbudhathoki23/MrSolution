using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.GodownWise;

public partial class FrmGodownProduct : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string FromADDate;
    private string FromBSDate;
    private string GodownId = string.Empty;
    private string InvoiceNo = string.Empty;
    private string ProductGroupId = string.Empty;
    private string ProductId = string.Empty;
    private string ProductSubGroupId = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmGodownProduct()
    {
        InitializeComponent();
    }

    private void FrmGodownProduct_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        ObjGlobal.BindBranch(cmb_Branch);
        BindGroupItem();
        BindUnit();
        cmb_GroupBy.SelectedIndex = 0;
        cmb_Unit.SelectedIndex = 0;
        chk_Date.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        rb_GodownProductWise.Focus();
    }

    private void FrmGodownProduct_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
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

        var list1 = new List<GroupBy>();
        list1.Add(new GroupBy(1, "Include Free", "1"));
        list1.Add(new GroupBy(2, "Exclude Free", "2"));
        list1.Add(new GroupBy(3, "Free Only", "3"));
        cmb_FreeGoods.DataSource = list1;
        cmb_FreeGoods.DisplayMember = "Name";
        cmb_FreeGoods.ValueMember = "Value";
        cmb_FreeGoods.SelectedIndex = 0;
    }

    public void BindUnit()
    {
        //var obj = new ObjGlobal();
        //var ReportTable = obj.BindUnitForInventoryRpt();
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

    private void txt_Find_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Find, 'E');
    }

    private void txt_Find_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Find, 'L');
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
        if (Chk_SelectAllGodown.Checked == false)
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Name");
            HeaderCap.Add("Code");
            HeaderCap.Add("Id");
            ColumnWidths.Add(250);
            ColumnWidths.Add(100);
            ColumnWidths.Add(0);

            Query =
                "SELECT GName as Name,GCode as Code,GId FROM AMS.Godown  Where Status=1 Union All Select 'Non Godown','Non Godown',0 Order By Name";
            var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Godown List");
            if (PkLst.ShowDialog() == DialogResult.OK)
            {
                if (ClsTagList.PlValue1 != null && ClsTagList.PlValue1 != string.Empty)
                {
                    GodownId = ClsTagList.PlValue3;
                }
                else
                {
                    MessageBox.Show("Godown Name Not Selected!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Godown Not Selected!");
                return;
            }
        }

        if (cmb_GroupBy.SelectedIndex == 1 && chk_SelectAllProduct.Checked == false)
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Name");
            HeaderCap.Add("Code");
            HeaderCap.Add("Id");
            ColumnWidths.Add(150);
            ColumnWidths.Add(100);
            ColumnWidths.Add(0);

            Query =
                "SELECT GrpName as Name,GrpCode as Code,PGrpID FROM AMS.ProductGroup Union All Select 'Non Group','Non Group',0 Order By Name";
            var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Product Group List");
            if (PkLst.ShowDialog() == DialogResult.OK)
            {
                if (ClsTagList.PlValue3 != null && ClsTagList.PlValue3 != string.Empty)
                {
                    ProductGroupId = ClsTagList.PlValue3;
                }
                else
                {
                    MessageBox.Show("Product Group Not Selected!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Product Group Not Selected!");
                return;
            }
        }

        if ((cmb_GroupBy.SelectedIndex == 1 || cmb_GroupBy.SelectedIndex == 2) &&
            chk_SelectAllProduct.Checked == false)
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            ClsTagList.PlValue4 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Name");
            HeaderCap.Add("Code");
            HeaderCap.Add("PG Name");
            HeaderCap.Add("Id");
            ColumnWidths.Add(150);
            ColumnWidths.Add(100);
            ColumnWidths.Add(150);
            ColumnWidths.Add(0);

            Query =
                "SELECT SubGrpName as Name,ShortName as Code,GrpName [PG Name],PSubGrpId FROM AMS.ProductSubGroup as PSG Inner Join AMS.ProductGroup as PG On PG.PGrpID=PSG.GrpID Union All Select 'Non SubGroup','Non SubGroup','Non Group', 0 Order By Name";
            var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Product Group List");
            if (PkLst.ShowDialog() == DialogResult.OK)
            {
                if (ClsTagList.PlValue4 != null && ClsTagList.PlValue4 != string.Empty)
                {
                    ProductSubGroupId = ClsTagList.PlValue4;
                }
                else
                {
                    MessageBox.Show("Product Group Not Selected!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Product Group Not Selected!");
                return;
            }
        }

        if (chk_SelectAllProduct.Checked == false) //cmb_GroupBy.SelectedIndex == 0 &&
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            ClsTagList.PlValue4 = string.Empty;
            ClsTagList.PlValue5 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Check");
            HeaderCap.Add("Description");
            HeaderCap.Add("ShortName");
            HeaderCap.Add("Group");
            HeaderCap.Add("Id");
            HeaderCap.Add("SubGroup");

            ColumnWidths.Add(50);
            ColumnWidths.Add(325);
            ColumnWidths.Add(100);
            ColumnWidths.Add(150);
            ColumnWidths.Add(0);
            ColumnWidths.Add(150);
            Query =
                "SELECT CONVERT(bit, 0) Chk, PName as Name,PShortName as Code,GrpName,SubGrpName,PId FROM AMS.Product as P Left Outer Join AMS.ProductGroup as PG On PG.PGrpID=P.PGrpId Left Outer Join AMS.ProductSubGroup as PSG On PSG.PSubGrpId=P.PSubGrpId Where P.Status=1 Order By  PName";
            var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Product List");
            if (PkLst.ShowDialog() == DialogResult.OK)
            {
                if (ClsTagList.PlValue5 != null && ClsTagList.PlValue5 != string.Empty)
                {
                    ProductId = ClsTagList.PlValue5;
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
        if (chk_Summary.Checked)
        {
            if (rb_GodownProductWise.Checked)
                RptName = "Stock Ledger Summary - Godown/Product Wise";
            else
                RptName = "Stock Ledger Summary - Product/Godown Wise";
        }
        else
        {
            if (rb_GodownProductWise.Checked)
                RptName = "Stock Ledger Details - Godown/Product Wise";
            else
                RptName = "Stock Ledger Details - Product/Godown Wise";
        }

        if (ObjGlobal.SysDateType == "M" && chk_Date.Checked == false ||
            ObjGlobal.SysDateType == "D" && chk_Date.Checked)
            RptDate = "From Date " + FromBSDate + " To " + ToBSDate;
        else
            RptDate = "From Date " + Convert.ToDateTime(FromADDate).ToShortDateString() + " To " +
                      Convert.ToDateTime(ToADDate).ToShortDateString();

        ProductGroupId = string.Empty;
        ProductSubGroupId = string.Empty;
        ProductId = string.Empty;
        GodownId = string.Empty;
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