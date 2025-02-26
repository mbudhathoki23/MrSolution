using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.Analysis;

public partial class FrmStockAgeing : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string FromADDate;
    private string FromBSDate;
    private string ProductGroupId = string.Empty;
    private string ProductId = string.Empty;
    private string ProductSubGroupId = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmStockAgeing()
    {
        InitializeComponent();
    }

    private void FrmStockAgeing_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.PageLoadDateType(msk_AsOnDate);
        ObjGlobal.BindBranch(cmb_Branch);
        BindGroupItem();
        cmb_GroupBy.SelectedIndex = 0;
        chk_Date.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";
        msk_AsOnDate.Focus();
    }

    private void FrmStockAgeing_KeyPress(object sender, KeyPressEventArgs e)
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

    private void msk_AsOnDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_AsOnDate, 'E');
    }

    private void msk_AsOnDate_KeyDown(object sender, KeyEventArgs e)
    {
    }

    private void msk_AsOnDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_AsOnDate, 'L');
    }

    private void msk_AsOnDate_Validated(object sender, EventArgs e)
    {
    }

    private void chk_Details_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Details.Checked)
        {
            if (cmb_GroupBy.Text == "Product Group")
            {
                chk_IncludeProduct.Enabled = false;
                chk_IncludeProduct.Checked = false;
            }
            else
            {
                chk_IncludeProduct.Enabled = true;
            }
        }
        else
        {
            chk_IncludeProduct.Enabled = true;
        }
    }

    private void chk_IncludeProduct_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_IncludeProduct.Checked)
        {
            if (cmb_GroupBy.Text == "Product Group")
            {
                chk_Details.Enabled = false;
                chk_Details.Checked = false;
            }
            else
            {
                chk_Details.Enabled = true;
            }
        }
        else
        {
            chk_Details.Enabled = true;
        }
    }

    private void cmb_GroupBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_GroupBy.Text == "Product")
        {
            chk_IncludeProduct.Checked = false;
            chk_IncludeProduct.Enabled = false;
            //Chk_IncludeSubGroup.Enabled = false;
        }
        else
        {
            chk_IncludeProduct.Enabled = true;
            //Chk_IncludeSubGroup.Enabled = true;
        }
    }

    private void txt_Days_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Days, 'E');
    }

    private void txt_Days_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        var isNumber = 0;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
    }

    private void txt_Days_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Days, 'L');
    }

    private void txt_Days_Validating(object sender, CancelEventArgs e)
    {
        if (Convert.ToInt32(ObjGlobal.ReturnDecimal(txt_Days.Text)) < 1)
        {
            MessageBox.Show("Days Can't Be Zero Or Null!");
            e.Cancel = true;
            return;
        }

        txt_Days.BackColor = SystemColors.Window;
    }

    private void txt_Interval_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Interval, 'E');
    }

    private void txt_Interval_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Back || e.KeyChar == '.' && !(sender as TextBox).Text.Contains(".")) return;
        var isNumber = 0;
        e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
    }

    private void txt_Interval_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Interval, 'L');
    }

    private void txt_Interval_Validating(object sender, CancelEventArgs e)
    {
        if (Convert.ToInt32(ObjGlobal.ReturnDecimal(txt_Interval.Text)) < 1)
        {
            MessageBox.Show("Interval Can't Be Zero Or Null!");
            e.Cancel = true;
            return;
        }

        txt_Interval.BackColor = SystemColors.Window;
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ObjGlobal.ReturnDecimal(txt_Days.Text)) < 1)
        {
            MessageBox.Show("Days Should Not Be Zero Or Null!");
            return;
        }

        if (Convert.ToInt32(ObjGlobal.ReturnDecimal(txt_Interval.Text)) < 1)
        {
            MessageBox.Show("Interval Should Not Be Zero Or Null!");
            return;
        }

        if (cmb_GroupBy.SelectedIndex != 0 && chk_SelectAll.Checked == false)
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

        if (cmb_GroupBy.SelectedIndex == 2 && chk_SelectAll.Checked == false)
        {
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            ClsTagList.PlValue4 = string.Empty;
            HeaderCap.Clear();
            ColumnWidths.Clear();
            HeaderCap.Add("Name");
            HeaderCap.Add("Code");
            HeaderCap.Add("Id");
            ColumnWidths.Add(150);
            ColumnWidths.Add(100);
            ColumnWidths.Add(0);

            Query =
                "SELECT SubGrpName as Name,ShortName as Code,GrpName [PG Name],PSubGrpId FROM AMS.ProductSubGroup as PSG Inner Join AMS.ProductGroup as PG On PG.PGrpID=PSG.GrpID Union All Select 'Non SubGroup','Non SubGroup','Non Group', 0 Order By Name";
            var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Product Sub Group List");
            if (PkLst.ShowDialog() == DialogResult.OK)
            {
                if (ClsTagList.PlValue3 != null && ClsTagList.PlValue3 != string.Empty)
                {
                    ProductSubGroupId = ClsTagList.PlValue3;
                }
                else
                {
                    MessageBox.Show("Product Sub Group Not Selected!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Product Sub Group Not Selected!");
                return;
            }
        }

        if (chk_SelectAll.Checked == false && cmb_GroupBy.SelectedIndex == 0 || chk_SelectAll.Checked == false &&
            cmb_GroupBy.SelectedIndex == 1 && chk_IncludeProduct.Checked)
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
            FromADDate = ObjGlobal.ReturnEnglishDate(ObjGlobal.CfStartBsDate);
            FromBSDate = ObjGlobal.CfStartBsDate;
            ToADDate = ObjGlobal.ReturnEnglishDate(msk_AsOnDate.Text);
            ToBSDate = msk_AsOnDate.Text;
        }
        else
        {
            FromADDate = ObjGlobal.CfStartAdDate.ToShortDateString();
            FromBSDate = ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString());
            ToADDate = msk_AsOnDate.Text;
            ToBSDate = ObjGlobal.ReturnNepaliDate(msk_AsOnDate.Text);
        }

        RptType = "Normal";
        if (cmb_GroupBy.Text == "Product")
        {
            if (chk_Details.Checked)
                RptName = "Stock Ageing Details - Product Wise";
            else
                RptName = "Stock Ageing Summary - Product Wise";
        }
        else if (cmb_GroupBy.Text == "Product Group")
        {
            if (chk_Details.Checked)
                RptName = "Stock Ageing Details - Product Group Wise";
            else if (chk_IncludeProduct.Checked)
                RptName = "Stock Ageing Summary - Product Group Wise";
            else
                RptName = "Stock Ageing Summary - Product Group Wise";
        }
        else if (cmb_GroupBy.Text == "Product Sub Group")
        {
            if (chk_Details.Checked)
                RptName = "Stock Ageing Details - Product Sub Group Wise";
            else
                RptName = "Stock Ageing Summary - Product Sub Group Wise";
        }

        if (ObjGlobal.SysDateType == "M" && chk_Date.Checked == false ||
            ObjGlobal.SysDateType == "D" && chk_Date.Checked)
            RptDate = "As On Date " + ToBSDate;
        else
            RptDate = "As On Date " + Convert.ToDateTime(ToADDate).ToShortDateString();

        ProductId = string.Empty;
        ProductGroupId = string.Empty;
        ProductSubGroupId = string.Empty;
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