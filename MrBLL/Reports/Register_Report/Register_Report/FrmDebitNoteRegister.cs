using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Register_Report.Register_Report;

public partial class FrmDebitNoteRegister : MrForm
{
    public FrmDebitNoteRegister()
    {
        InitializeComponent();
    }

    #region Class

    private string RptType;
    private string RptName;
    private string RptDate;
    private string FromADDate;
    private string FromBSDate;
    private string ToADDate;
    private string ToBSDate;
    private readonly string VoucherNo = string.Empty;
    private string Ledger_Code = string.Empty;
    private string Query = string.Empty;
    private string AgentId = string.Empty;
    private readonly string Product_Code = string.Empty;
    private string SubledgerId = string.Empty;
    private string ClassCode = string.Empty;
    private readonly ArrayList HeaderCap = new();
    private readonly ArrayList ColumnWidths = new();

    #endregion Class

    #region Frm

    private void DebitNoteRegister_Load(object sender, EventArgs e)
    {
        Location = new Point(240, 40);
        BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.BindPeriodicDate(msk_FromDate, msk_ToDate);
        BindGroupItem();
        cmb_GroupBy.SelectedIndex = 0;
        cmb_Currency.SelectedIndex = 0;
        cmbSysDateType.SelectedIndex = 8;
        chk_Summary_CheckedChanged(sender, e);
        ObjGlobal.BindBranch(cmb_Branch);
        chk_Date.Text = ObjGlobal.SysDateType == "M" ? "Date" : "Miti";

        msk_FromDate.Focus();
    }

    private void DebitNoteRegister_Activated(object sender, EventArgs e)
    {

    }

    private void DebitNoteRegister_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    #endregion Frm

    #region Methods

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
        list.Add(new GroupBy(i, "Voucher No", "2"));
        i = i + 1;
        list.Add(new GroupBy(i, "PartyWise", "3"));
        i = i + 1;
        list.Add(new GroupBy(i, "SubledgerWise", "4"));
        i = i + 1;
        list.Add(new GroupBy(i, "AgentWise", "5"));
        i = i + 1;
        list.Add(new GroupBy(i, "AreaWise", "6"));
        i = i + 1;
        cmb_GroupBy.DataSource = list;
        cmb_GroupBy.DisplayMember = "Name";
        cmb_GroupBy.ValueMember = "Value";
    }

    #endregion Methods

    #region Botton

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }

    private void btn_Cancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    #endregion Botton

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
        //ClassCode = ObjGlobal.GetClassById("Notes_Master", string.Empty);
        //if (string.IsNullOrEmpty(ClassCode))
        //{
        //    if (ObjGlobal._ProjectCode == "07" || ObjGlobal._ProjectCode == "08")
        //    {
        //        MessageBox.Show("Table Code List Not Selected!", ObjGlobal.Caption);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Class Code List Not Selected!", ObjGlobal.Caption);
        //    }
        //    return;
        //}
        //else
        //{ txt_Class.Text = ClassCode; }
    }

    private void chk_Class_Click(object sender, EventArgs e)
    {
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
}