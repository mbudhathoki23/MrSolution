using MrBLL.Utility.Common;
using MrBLL.Utility.CrystalReports;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Sales_Register;

public partial class FrmTicketSalesRegister : MrForm
{
    public FrmTicketSalesRegister()
    {
        InitializeComponent();
    }

    private void FrmTicketSalesRegister_Activated(object sender, EventArgs e)
    {
    }

    private void FrmTicketSalesRegister_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        BindGroupItem();
        cmb_GroupBy.SelectedIndex = 0;
        cmb_Currency.SelectedIndex = 0;
        cmb_InvoiceType.SelectedIndex = 0;
        cmbSysDateType.SelectedIndex = 8;
        chk_Summary_CheckedChanged(sender, e);
        ObjGlobal.BindBranch(cmb_Branch);
        ObjGlobal.BindAdvanceReport(cmb_AdvanceReport, "SB");

        if (ObjGlobal.SysDateType == "M")
            chk_Date.Text = "Date";
        else
            chk_Date.Text = "Miti";

        msk_FromDate.Focus();
    }

    private void FrmTicketSalesRegister_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    #region Class

    private string RptType;
    private string RptName;
    private string RptDate;
    private string FromADDate;
    private string FromBSDate;
    private string ToADDate;
    private string ToBSDate;
    private readonly string InvoiceNo = string.Empty;
    private string Ledger_Code = string.Empty;
    private string Product_Code = string.Empty;
    private readonly string Product_GroupId = string.Empty;
    private readonly string Product_SubGroupId = string.Empty;
    private string Query = string.Empty;
    private string AgentId = string.Empty;
    private readonly string SubledgerId = string.Empty;
    private readonly string ClassCode = string.Empty;
    private readonly ArrayList HeaderCap = new();
    private readonly ArrayList ColumnWidths = new();

    #endregion Class

    #region Event

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", cmbSysDateType.SelectedIndex, msk_FromDate, msk_ToDate);
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
        if (msk_FromDate.Text.Trim() != "/  /")
        {
            if (ObjGlobal.SysDateType == "D")
            {
                if (ObjGlobal.ValidDate(msk_FromDate.Text, ObjGlobal.SysDateType))
                {
                    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(msk_FromDate.Text)) == false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " + ObjGlobal.CfStartAdDate + " and " +
                            ObjGlobal.CfEndAdDate + " ", ObjGlobal.Caption, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        msk_FromDate.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_FromDate.Focus();
                }
            }
            else
            {
                if (ObjGlobal.ValidDate(msk_FromDate.Text, ObjGlobal.SysDateType))
                {
                    if (ObjGlobal.ValidDateRange(
                            Convert.ToDateTime(ObjGlobal.ReturnEnglishDate(msk_FromDate.Text))) == false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString()) + " and " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfEndAdDate.ToShortDateString()) + " ",
                            ObjGlobal.Caption,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msk_FromDate.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_FromDate.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("From Date Cann't be Left Blank!");
            msk_FromDate.Focus();
        }
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
        if (msk_ToDate.Text.Trim() != "/  /")
        {
            if (ObjGlobal.SysDateType == "D")
            {
                if (ObjGlobal.ValidDate(msk_ToDate.Text, ObjGlobal.SysDateType))
                {
                    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(msk_ToDate.Text)) == false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " + ObjGlobal.CfStartAdDate + " and " +
                            ObjGlobal.CfEndAdDate + " ", ObjGlobal.Caption, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        msk_ToDate.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_ToDate.Focus();
                }
            }
            else
            {
                if (ObjGlobal.ValidDate(msk_ToDate.Text, ObjGlobal.SysDateType))
                {
                    if (ObjGlobal.ValidDateRange(
                            Convert.ToDateTime(ObjGlobal.ReturnEnglishDate(msk_ToDate.Text))) == false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString()) + " and " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfEndAdDate.ToShortDateString()) + " ",
                            ObjGlobal.Caption,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msk_ToDate.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_ToDate.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("To Date Cann't be Left Blank!");
            msk_ToDate.Focus();
        }
    }

    private void txt_Find_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Find, 'E');
    }

    private void txt_Find_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Find, 'L');
    }

    private void cmb_GroupBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmb_GroupBy.SelectedIndex == 0)
        {
            chk_SelectAll.Enabled = false;
            chk_Summary.Enabled = true;
        }
        else if (cmb_GroupBy.SelectedIndex == 6 || cmb_GroupBy.SelectedIndex == 7)
        {
            chk_Summary.Checked = false;
            chk_Summary.Enabled = false;
        }
        else
        {
            chk_SelectAll.Enabled = true;
            chk_Summary.Enabled = true;
        }
    }

    private void chk_Summary_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_Summary.Checked)
            chk_Horizontal.Enabled = false;
        else
            chk_Horizontal.Enabled = true;
    }

    private void btn_Class_Click(object sender, EventArgs e)
    {
        //ClassCode = ObjGlobal.GetClassById("SB_Master", string.Empty);
        //if (string.IsNullOrEmpty(ClassCode))
        //{
        //    MessageBox.Show("Department Code List Not Selected!");
        //    return;
        //}
        //else
        //{
        //    txt_Class.Text = ClassCode;
        //}
    }

    private void chk_Class_Click(object sender, EventArgs e)
    {
        if (chk_Class.Checked)
        {
            txt_Class.Text = string.Empty;
            txt_Class.Enabled = false;
            btn_Class.Enabled = false;
        }
        else
        {
            txt_Class.Enabled = true;
            btn_Class.Enabled = true;
        }
    }

    private void txt_Class_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Class, 'E');
    }

    private void txt_Class_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_Class, 'L');
    }

    private void txt_Class_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) btn_Class_Click(sender, e);
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    #endregion Event

    #region Botton

    private void btn_Show_Click(object sender, EventArgs e)
    {
        try
        {
            //if (chk_Class.Checked == true)
            //{ ClassCode = "true"; }
            //else
            //{
            //    if (!string.IsNullOrEmpty(txt_Class.Text))
            //    { }
            //    else
            //    {
            //        ClassCode = string.Empty;
            //    }
            //}
            //if (cmb_GroupBy.Text == "SubLedger" && chk_SelectAll.Checked == false)//subledger as Customer  in Ticket Sales
            //{
            //    SubledgerId = ObjGlobal.GetAgentId("SBT_Details", "Slb_Id");
            //    if (string.IsNullOrEmpty(AgentId))
            //    {
            //        MessageBox.Show("Customer List Not Selected!");
            //        return;
            //    }
            //}
            //else if (cmb_GroupBy.Text == "Product" && chk_SelectAll.Checked == false)//Product as Ticket No  in Ticket Sales
            //{
            //    Product_Code = ObjGlobal.GetProductId("SBT_Details", string.Empty);
            //    if (string.IsNullOrEmpty(Product_Code))
            //    {
            //        MessageBox.Show("Product List Not Selected!");
            //        return;
            //    }
            //}
            //else if (cmb_GroupBy.Text == "Product Group" && !chk_SelectAll.Checked)//Product Group as Air lines  in Ticket Sales
            //{
            //    Product_GroupId = ObjGlobal.GetProductGroupId(string.Empty);

            //    if (!string.IsNullOrEmpty(Product_GroupId))
            //    {
            //        Product_Code = ObjGlobal.GetProductIdByGroupIdwise("SBT_Details", string.Empty, Product_GroupId);
            //        if (string.IsNullOrEmpty(Product_Code))
            //        {
            //            MessageBox.Show("Product list not available in this group!");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Product Group List Not Selected!");
            //        return;
            //    }
            //}
            //else if (cmb_GroupBy.Text == "Product Sub Group" && !chk_SelectAll.Checked)
            //{
            //    Product_GroupId = ObjGlobal.GetProductGroupId(string.Empty);

            //    if (!string.IsNullOrEmpty(Product_GroupId))
            //    {
            //        Product_SubGroupId = ObjGlobal.GetProductSubGroupIdByGrouId("SBT_Details", Product_GroupId);

            //        if (string.IsNullOrEmpty(Product_SubGroupId))
            //        {
            //            MessageBox.Show("Product Sub Group list not available in this group!");
            //            return;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Product Group List Not Selected!");
            //        return;
            //    }
            //}
            Ledger_Code = string.Empty;
            ClsTagList.PlValue1 = string.Empty;
            ClsTagList.PlValue2 = string.Empty;
            ClsTagList.PlValue3 = string.Empty;
            if (cmb_GroupBy.SelectedIndex == 1 && chk_SelectAll.Checked == false)
            {
                HeaderCap.Clear();
                ColumnWidths.Clear();
                HeaderCap.Add("Check");
                HeaderCap.Add("Invoice No");
                HeaderCap.Add("Date");
                ColumnWidths.Add(50);
                ColumnWidths.Add(150);
                ColumnWidths.Add(150);
                Query = "SELECT CONVERT(bit, 0) Chk,SBT_Invoice,Invoice_Miti FROM AMS.SBT_Master";
                var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Invoice No List");
                if (PkLst.ShowDialog() == DialogResult.OK)
                {
                    if (ClsTagList.PlValue1 != null && ClsTagList.PlValue1 != string.Empty)
                    {
                        Ledger_Code = ClsTagList.PlValue1;
                    }
                    else
                    {
                        MessageBox.Show("Invoice No Not Selected..!!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invoice No Not Selected..!!");
                    return;
                }
            }

            if (cmb_GroupBy.SelectedIndex == 2 && chk_SelectAll.Checked == false)
            {
                HeaderCap.Clear();
                ColumnWidths.Clear();
                HeaderCap.Add("Check");
                HeaderCap.Add("Id");
                HeaderCap.Add("Code");
                HeaderCap.Add("Name");
                ColumnWidths.Add(50);
                ColumnWidths.Add(0);
                ColumnWidths.Add(100);
                ColumnWidths.Add(250);
                Query =
                    "SELECT  CONVERT(bit, 0) Chk,GlID Id,GlCode as Code,GlName as Name FROM AMS.GeneralLedger as GL Inner Join AMS.SBT_Master as SM On SM.Customer_ID=GL.GlID Where GlType in ('Customer','Both','Cash','Bank') Order by GL.GlName";
                var PkLst = FrmTagList.CreateInstance(Query, HeaderCap, ColumnWidths, "Ledger List");
                if (PkLst.ShowDialog() == DialogResult.OK)
                {
                    if (ClsTagList.PlValue1 != null && ClsTagList.PlValue1 != string.Empty)
                    {
                        Ledger_Code = ClsTagList.PlValue1;
                    }
                    else
                    {
                        MessageBox.Show("Ledger Not Selected..!!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ledger Not Selected..!!");
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
                RptName = "Ticket Sales Register Summary";
            else
                RptName = "Ticket Sales Register Details";

            if (cmb_GroupBy.Text == "Product Group")
                RptName = "Ticket Sales Register Details Product Group Wise";
            else if (cmb_GroupBy.Text == "Product Sub Group")
                RptName = "Ticket Sales Register Details Product Sub Group Wise";

            if (cmb_AdvanceReport.SelectedValue.ToString() == "0")
            {
                if (ObjGlobal.SysDateType == "M" && chk_Date.Checked == false ||
                    ObjGlobal.SysDateType == "D" && chk_Date.Checked)
                    RptDate = "From Date " + FromBSDate + " To " + ToBSDate;
                else
                    RptDate = "From Date " + Convert.ToDateTime(FromADDate).ToShortDateString() + " To " +
                              Convert.ToDateTime(ToADDate).ToShortDateString();
            }
            else
            {
                var dt = new DataTable();
                Query =
                    " Select DesignerPaper_Name,Paths,Paper_Size,Orientation,Height,Width,Margin_Left,Margin_Bottom,Margin_Right,Margin_Top from Master.AMS.PrintDocument_Designer as PDD Where Station = 'SBT' and Type='Report'";
                Query = Query + " and DesignerPaper_Name = '" + cmb_AdvanceReport.Text + "' ";
                dt = GetConnection.SelectDataTableQuery(Query);
                if (dt.Rows.Count > 0)
                    try
                    {
                        new FrmPrintPreViewer(string.Empty, string.Empty, string.Empty, 0,
                            dt.Rows[0]["DesignerPaper_Name"].ToString(), string.Empty, string.Empty, string.Empty,
                            false, dt.Rows[0]["Paths"].ToString()).ShowDialog();
                        return;

                        //Utility.FrmCrystalReportViewer frm = new Utility.FrmCrystalReportViewer();
                        //frm.rtype = "Voucher Print";
                        //frm.design_Name = ReportTable.Rows[0]["DesignerPaper_Name"].ToString();
                        //frm.Path = ReportTable.Rows[0]["Paths"].ToString();
                        //frm.Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ObjGlobal.Caption, MessageBoxButtons.RetryCancel,
                            MessageBoxIcon.Error);
                    }
            }

            AgentId = string.Empty;
            Product_Code = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion Botton

    #region method

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

    private void BindGroupItem()
    {
        cmb_GroupBy.Enabled = true;
        var i = 1;
        var list = new List<GroupBy>
        {
            new(i, "Date", "1")
        };
        i = i + 1;
        list.Add(new GroupBy(i, "Invoice No", "2"));
        i = i + 1;
        list.Add(new GroupBy(i, "Party", "3"));
        i = i + 1;
        list.Add(new GroupBy(i, "SubLedger", "4"));
        i = i + 1;
        list.Add(new GroupBy(i, "Product", "6"));
        i = i + 1;
        list.Add(new GroupBy(i, "Product Group", "7"));
        i = i + 1;
        list.Add(new GroupBy(i, "Product Sub Group", "8"));
        i = i + 1;
        cmb_GroupBy.DataSource = list;
        cmb_GroupBy.DisplayMember = "Name";
        cmb_GroupBy.ValueMember = "Value";
    }

    #endregion method
}