using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using MrDAL.Utility.PickList;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Reports.Finance_Report.Analysis;

public partial class FrmCashFlowStatement : MrForm
{
    private readonly ArrayList ColumnWidths = new();
    private readonly ArrayList HeaderCap = new();
    private DataTable dtTemp = new();
    private string FromADDate;
    private string FromBSDate;
    private string Ledger_Code = string.Empty;
    private string Query = string.Empty;
    private string RptDate;
    private string RptName;
    private string RptType;
    private string ToADDate;
    private string ToBSDate;

    public FrmCashFlowStatement()
    {
        InitializeComponent();
    }

    private void FrmCashFlowStatement_Load(object sender, EventArgs e)
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
    }

    private void FrmCashFlowStatement_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }
    }

    private void FrmCashFlowStatement_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
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

    private void btn_BookName_Click(object sender, EventArgs e)
    {
        ClsPickList.PlValue1 = string.Empty;
        ClsPickList.PlValue2 = string.Empty;
        ClsPickList.PlValue3 = string.Empty;
        HeaderCap.Clear();
        ColumnWidths.Clear();

        HeaderCap.Add("Name");
        HeaderCap.Add("Code");
        ColumnWidths.Add(250);
        ColumnWidths.Add(100);
        Query =
            "Select distinct GlName,GlCode from AMS.GeneralLedger as L  where GlType in ('Cash','Bank') order by L.GlName ";
        var PkLst = new FrmPickList(Query, HeaderCap, ColumnWidths, "Book List", string.Empty);
        if (PkLst.ShowDialog() == DialogResult.OK)
            if (ClsPickList.PlValue2 != null && ClsPickList.PlValue1 != null &&
                ClsPickList.PlValue2 != string.Empty && ClsPickList.PlValue1 != string.Empty)
            {
                txt_BookName.Text = ClsPickList.PlValue1;
                txt_BookName.Focus();
            }
    }

    private void txt_BookName_Enter(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_BookName, 'E');
    }

    private void txt_BookName_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F1) btn_BookName_Click(sender, e);
    }

    private void txt_BookName_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void txt_BookName_Leave(object sender, EventArgs e)
    {
        ObjGlobal.TxtBackColor(txt_BookName, 'L');
    }

    private void txt_BookName_Validated(object sender, EventArgs e)
    {
        if (txt_BookName.Text.Trim() != string.Empty)
        {
            Query =
                "Select distinct GlId,GlName,GlCode from AMS.GeneralLedger as L  where GlType in ('Cash','Bank') and GlName='" +
                txt_BookName.Text + "' order by L.GlName ";
            dtTemp.Clear();
            dtTemp.Reset();
            dtTemp = GetConnection.SelectDataTableQuery(Query);
            if (dtTemp.Rows.Count > 0)
            {
                Ledger_Code = dtTemp.Rows[0]["GlId"].ToString();
                txt_BookName.Text = dtTemp.Rows[0]["GlName"].ToString();
            }
            else
            {
                MessageBox.Show("Book Name Doesn't Not Exists!!");
                txt_BookName.Focus();
            }
        }
        else
        {
            MessageBox.Show("Book Name can't be blank!!");
            txt_BookName.Focus();
        }
    }

    private void btn_Show_Click(object sender, EventArgs e)
    {
    }
}