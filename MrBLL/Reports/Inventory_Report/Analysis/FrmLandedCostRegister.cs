using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Reports.Inventory_Report.Analysis;

public partial class FrmLandedCostRegister : MrForm
{
    private readonly string ClassCode = string.Empty;
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private readonly string InvoiceNo = string.Empty;
    private readonly string Product_Code = string.Empty;
    private string FromADDate;
    private string FromBSDate;
    private string Ledger_Code = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmLandedCostRegister()
    {
        InitializeComponent();
    }

    private void LandedCostRegister_Load(object sender, EventArgs e)
    {
        StartPosition = FormStartPosition.CenterParent;
        BackColor = ObjGlobal.FrmBackColor();
        cmbSysDateType.SelectedIndex = 8;
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        BindGroupItem();
        cmb_GroupBy.SelectedIndex = 0;
        cmb_Currency.SelectedIndex = 0;
        ObjGlobal.BindBranch(cmb_Branch);
        switch (ObjGlobal.SysDateType)
        {
            case "M":
                chk_Date.Text = "Date";
                break;

            default:
                chk_Date.Text = "Miti";
                break;
        }

        msk_FromDate.Focus();
    }

    private void LandedCostRegister_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
            if (MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption, MessageBoxButtons.YesNo) ==
                DialogResult.Yes)
                Close();
    }

    private void msk_FromDate_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_FromDate, 'E');
    }

    private void msk_FromDate_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
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
        if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
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
            chk_SelectAll.Enabled = false;
        else
            chk_SelectAll.Enabled = true;
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }

    private void BindGroupItem()
    {
        cmb_GroupBy.Enabled = true;
        var list = new List<GroupBy>
        {
            new(1, "Date", "1"),
            new(2, "Invoice", "2"),
            new(3, "Party", "3")
        };
        cmb_GroupBy.DataSource = list;
        cmb_GroupBy.DisplayMember = "Name";
        cmb_GroupBy.ValueMember = "Value";
    }

    private void cmbSysDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ObjGlobal.LoadDate("R", cmbSysDateType.SelectedIndex, msk_FromDate, msk_ToDate);
    }

    private void txt_Find_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter) btn_Show.Focus();
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