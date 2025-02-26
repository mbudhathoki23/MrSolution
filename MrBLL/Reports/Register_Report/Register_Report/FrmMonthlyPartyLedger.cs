using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Register_Report;

public partial class FrmMonthlyPartyLedger : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string AccountType;
    private string AgentId = string.Empty;
    private string FromADDate;
    private string FromBSDate;
    private string Ledger_Code = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string SubLedger_Code = string.Empty;
    private string ToADDate;
    private string ToBSDate;

    public FrmMonthlyPartyLedger()
    {
        InitializeComponent();
    }

    private void MonthlyPartyLedger_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        if (ObjGlobal.SysDateType == "BS")
        {
            msk_FromDate.Text = ObjGlobal.CfStartBsDate.Substring(3, 7);
            msk_ToDate.Text = ObjGlobal.CfEndBsDate.Substring(3, 7);
        }
        else
        {
            msk_FromDate.Text = ObjGlobal.CfStartAdDate.ToString("MM/yyyy");
            msk_ToDate.Text = ObjGlobal.CfEndAdDate.ToString("MM/yyyy");
        }

        cmb_GroupBy.SelectedIndex = 0;
        ObjGlobal.BindBranch(cmb_Branch);
        if (ObjGlobal.SysDateType == "M")
            chk_Date.Text = "Date";
        else
            chk_Date.Text = "Date";

        msk_FromDate.Focus();
        //if (ObjGlobal._MenuName == "Monthly Customer")
        //{
        //    Text = "Monthly Customer";
        //    lbl_PartyType.Visible = false;
        //    rb_Customer.Checked = true;
        //    rb_Vendor.Checked = false;
        //    rb_Customer.Visible = false;
        //    rb_Vendor.Visible = false;
        //}
        //else //--if (ObjGlobal.Form_Name == "Monthly Vendor")
        //{
        //    Text = "Monthly Vendor";
        //    lbl_PartyType.Visible = false;
        //    rb_Customer.Checked = false;
        //    rb_Vendor.Checked = true;
        //    rb_Customer.Visible = false;
        //    rb_Vendor.Visible = false;
        //}

        cmbSysDateType.SelectedIndex = 8;
        cmbSysDateType.Focus();
    }

    private void MonthlyPartyLedger_KeyPress(object sender, KeyPressEventArgs e)
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
    }

    private void cmbSysDateType_Enter(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(cmbSysDateType, 'E');
    }

    private void cmbSysDateType_Leave(object sender, EventArgs e)
    {
        ObjGlobal.ComboBoxBackColor(cmbSysDateType, 'L');
    }

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", cmbSysDateType.SelectedIndex, msk_FromDate, msk_ToDate);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
            MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes) Close();
    }

    private void cmbSysDateType_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void cmbSysDateType_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 32) SendKeys.Send("{F4}");
    }
}