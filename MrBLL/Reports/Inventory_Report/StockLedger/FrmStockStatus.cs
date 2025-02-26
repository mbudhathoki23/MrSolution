using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.StockLedger;

public partial class FrmStockStatus : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string FromADDate;
    private string FromBSDate;
    private string ProductId = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;

    public FrmStockStatus()
    {
        InitializeComponent();
    }

    private void FrmStockStatus_Load(object sender, EventArgs e)
    {
        Location = new Point(10, 10);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.PageLoadDateType(msk_AsOnDate);
        ObjGlobal.BindBranch(cmb_Branch);
        BindUnit();
        cmb_Unit.SelectedIndex = 0;
        if (ObjGlobal.SysDateType == "M")
            chk_Date.Text = "Date";
        else
            chk_Date.Text = "Miti";
        msk_AsOnDate.Focus();
    }

    private void FrmStockStatus_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
        if (chk_SelectAll.Checked == false)
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
                "SELECT CONVERT(bit, 0) Chk,PName as Name,PShortName as Code,PId FROM AMS.Product  Where Status=1 Order By  PName";
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
            FromADDate = ObjGlobal.ReturnEnglishDate(msk_AsOnDate.Text);
            FromBSDate = msk_AsOnDate.Text;
        }
        else
        {
            FromADDate = msk_AsOnDate.Text;
            FromBSDate = ObjGlobal.ReturnNepaliDate(msk_AsOnDate.Text);
        }

        RptType = "Normal";
        RptName = "Stock Status";

        if (ObjGlobal.SysDateType == "M" && chk_Date.Checked == false ||
            ObjGlobal.SysDateType == "D" && chk_Date.Checked)
            RptDate = "As On Date " + FromBSDate;
        else
            RptDate = "As On Date " + Convert.ToDateTime(FromADDate).ToShortDateString();
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
}