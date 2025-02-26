using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.Analysis;

public partial class FrmFundNetPosition : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private string AsOnADDate;
    private string AsOnBSDate;
    private string Ledger_Code = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;

    public FrmFundNetPosition()
    {
        InitializeComponent();
    }

    private void FrmFundNetPosition_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.PageLoadDateType(msk_AsOnDate);
        ObjGlobal.BindBranch(cmb_Branch);
        if (ObjGlobal.SysDateType == "M")
            chk_Date.Text = "Date";
        else
            chk_Date.Text = "Miti";
    }

    private void FrmFundNetPosition_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }

        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void msk_AsOnDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_AsOnDate, 'E');
    }

    private void msk_AsOnDate_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_AsOnDate, 'L');
    }

    private void msk_AsOnDate_Validated(object sender, EventArgs e)
    {
        if (msk_AsOnDate.Text.Trim() != "/  /")
        {
            if (ObjGlobal.SysDateType == "D")
            {
                if (ObjGlobal.ValidDate(msk_AsOnDate.Text, ObjGlobal.SysDateType))
                {
                    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(msk_AsOnDate.Text)) == false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " + ObjGlobal.CfStartAdDate + " and " +
                            ObjGlobal.CfEndAdDate + " ", ObjGlobal.Caption, MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        msk_AsOnDate.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_AsOnDate.Focus();
                }
            }
            else
            {
                if (ObjGlobal.ValidDate(msk_AsOnDate.Text, ObjGlobal.SysDateType))
                {
                    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(ObjGlobal.EntryDateType(msk_AsOnDate.Text))) ==
                        false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString()) + " and " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfEndAdDate.ToShortDateString()) + " ",
                            ObjGlobal.Caption,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msk_AsOnDate.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Plz. Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_AsOnDate.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("As On Date Cann't be Left Blank!");
            msk_AsOnDate.Focus();
        }
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }
}