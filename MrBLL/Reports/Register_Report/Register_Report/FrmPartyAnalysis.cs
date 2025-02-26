using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Register_Report;

public partial class FrmPartyAnalysis : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string AccountType;
    private string FromADDate;
    private string FromBSDate;
    private string Ledger_Code = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmPartyAnalysis()
    {
        InitializeComponent();
    }

    private void PartyAnalysis_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        ObjGlobal.BindBranch(cmb_Branch);
        if (ObjGlobal.SysDateType == "M")
            chk_Date.Text = "Date";
        else
            chk_Date.Text = "Miti";

        msk_FromDate.Focus();

        //if (ObjGlobal._MenuName == "Customer Analysis")
        //{
        //    Text = "Customer Analysis";
        //    lbl_PartyType.Visible = false;
        //    rb_Customer.Checked = true;
        //    rb_Vendor.Checked = false;
        //    rb_Customer.Visible = false;
        //    rb_Vendor.Visible = false;
        //}
        //else //--if (ObjGlobal.Form_Name == "Vendor Analysis")
        //{
        //    Text = "Vendor Analysis";
        //    lbl_PartyType.Visible = false;
        //    rb_Customer.Checked = false;
        //    rb_Vendor.Checked = true;
        //    rb_Customer.Visible = false;
        //    rb_Vendor.Visible = false;
        //}
    }

    private void PartyAnalysis_KeyPress(object sender, KeyPressEventArgs e)
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
                    MessageBox.Show(@"Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
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
                    MessageBox.Show(@"Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_FromDate.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("Report From Date Cann't be Left Blank!");
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
                    MessageBox.Show(@"Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
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
                    MessageBox.Show(@"Report To Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_ToDate.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("Date Cann't be Left Blank!");
            msk_ToDate.Focus();
        }
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }
}