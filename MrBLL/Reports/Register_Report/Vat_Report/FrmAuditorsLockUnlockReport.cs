using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Word;
using MrBLL.Utility.Common;
using MrDAL.Control.WinControl;
using MrDAL.Global.Common;
using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Word.Application;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;

namespace MrBLL.Reports.Register_Report.Vat_Report;

public partial class FrmAuditorsLockUnlockReport : MrForm
{
    private int currentColumn;

    private int rowIndex;

    public FrmAuditorsLockUnlockReport()
    {
        InitializeComponent();
    }

    public FrmAuditorsLockUnlockReport(string RptType, string RptName, string RptDate, string FromADDate,
        string ToADDate, bool GroupWise, bool Details, string Currency_Id, bool Remarks, bool TFormat,
        bool SubLedger, bool Date, string Voucher_No, string Source, string BranchId, string CurrencyType,
        string OpenFor)
    {
        InitializeComponent();
    }

    private void FrmAuditorsLockUnlockReport_Activated(object sender, EventArgs e)
    {
    }

    private void FrmAuditorsLockUnlockReport_Load(object sender, EventArgs e)
    {
        BackColor = ObjGlobal.FrmBackColor();
        toolStrip1.BackColor = ObjGlobal.FrmBackColor();
        ObjGlobal.DgvBackColor(dgv_DisplayReport);

        lbl_ComanyName.Text = ObjGlobal.LogInCompany;
        lbl_CompanyAddress.Text = ObjGlobal.CompanyAddress;
    }

    private void FrmAuditorsLockUnlockReport_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            var dialogResult = MessageBox.Show("Are you sure want to Close Form!", ObjGlobal.Caption,
                MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Close();
        }

        if (e.KeyChar == (char)Keys.Enter) SendKeys.Send("{TAB}");
    }

    private void FrmAuditorsLockUnlockReport_Resize(object sender, EventArgs e)
    {
    }

    private void dgv_DisplayReport_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        currentColumn = e.ColumnIndex;
    }

    private void dgv_DisplayReport_RowEnter(object sender, DataGridViewCellEventArgs e)
    {
        rowIndex = e.RowIndex;
    }

    private void dgv_DisplayReport_KeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            if (e.KeyCode == Keys.Enter) //|| e.KeyCode == Keys.Tab
            {
                dgv_DisplayReport.Rows[rowIndex].Selected = true;
                e.SuppressKeyPress = true;

                if (dgv_DisplayReport.CurrentRow.Cells[0].Value == null)
                    dgv_DisplayReport.CurrentRow.Cells[0].Value = string.Empty;

                if (dgv_DisplayReport.Rows.Count > 0)
                    if (dgv_DisplayReport.CurrentRow.Cells[6].Value != null &&
                        dgv_DisplayReport.CurrentRow.Cells[6].Value.ToString() != string.Empty &&
                        dgv_DisplayReport.CurrentRow.Cells[2].Value != null &&
                        dgv_DisplayReport.CurrentRow.Cells[2].Value.ToString() != string.Empty)
                        ZoomingRpt(Convert.ToString(dgv_DisplayReport.CurrentRow.Cells[6].Value.ToString()),
                            Convert.ToString(dgv_DisplayReport.CurrentRow.Cells[2].Value.ToString()));
            }
        }
        catch
        {
        }
    }

    private void dgv_DisplayReport_KeyPress(object sender, KeyPressEventArgs e)
    {
    }

    private void dgv_DisplayReport_CellClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    public void GridDesignCashDayJvBookWithCurrency()
    {
    }

    public void GridDesignEntryLogRegister()
    {
        dgv_DisplayReport.ReadOnly = true;
        dgv_DisplayReport.Rows.Clear();
        dgv_DisplayReport.Columns.Clear();

        DataGridViewColumn column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Document Number";
        column.Name = "gv_DCN";
        column.Width = 150;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Date";
        column.Name = "gv_Date";
        column.Width = 90;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action";
        column.Name = "gv_Action";
        column.Width = 80;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action Date/Time";
        column.Name = "gv_ADT";
        column.Width = 150;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Description";
        column.Name = "gv_description";
        column.Width = 300;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Amount";
        column.Name = "gv_Amount";
        column.Width = 90;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Is Printed";
        column.Name = "gv_IsPrinted";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "No of Print";
        column.Name = "gv_NoofPrint";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Printed Time";
        column.Name = "gv_PrintedTime";
        column.Width = 120;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Printed By";
        column.Name = "gv_PrintedBy";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Remarks";
        column.Name = "gv_remarks";
        column.Width = 200;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        dgv_DisplayReport.Columns[1].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }

    public void GridDesignNewAuditLogRegister()
    {
        dgv_DisplayReport.ReadOnly = true;
        dgv_DisplayReport.Rows.Clear();
        dgv_DisplayReport.Columns.Clear();

        DataGridViewColumn column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Document Number";
        column.Name = "gv_DCN";
        column.Width = 140;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Date";
        column.Name = "gv_Date";
        column.Width = 90;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action Date/Time";
        column.Name = "gv_ADT";
        column.Width = 130;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Description";
        column.Name = "gv_description";
        column.Width = 220;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Amount";
        column.Name = "gv_Amount";
        column.Width = 90;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action User";
        column.Name = "gv_User";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Sys User";
        column.Name = "gv_sysUser";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Operation";
        column.Name = "gv_operation";
        column.Width = 90;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Is Printed";
        column.Name = "gv_IsPrinted";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Noof Print";
        column.Name = "gv_NoofPrint";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Printed Time";
        column.Name = "gv_PrintedTime";
        column.Width = 120;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Printed By";
        column.Name = "gv_PrintedBy";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Remarks";
        column.Name = "gv_remarks";
        column.Width = 200;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        dgv_DisplayReport.Columns[1].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }

    private void DisplayDayBook()
    {
    }

    private void DisplayEntryLogRegister()
    {
    }

    private void DisplayNewAuditLogRegister()
    {
    }

    private void btn_TagAll_Click(object sender, EventArgs e)
    {
    }

    private void btn_UnTagAll_Click(object sender, EventArgs e)
    {
    }

    private void cmb_OpenFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplayDayBook();
    }

    private void msk_Date_Enter(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_Date, 'E');
    }

    private void msk_Date_Leave(object sender, EventArgs e)
    {
        ObjGlobal.MskTxtBackColor(msk_Date, 'L');
    }

    private void msk_Date_Validated(object sender, EventArgs e)
    {
        if (msk_Date.Text.Trim() != "/  /")
        {
            if (ObjGlobal.SysDateType == "D")
            {
                if (ObjGlobal.ValidDate(msk_Date.Text, "D"))
                {
                    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(msk_Date.Text)) == false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " + ObjGlobal.CfStartAdDate + " and " +
                            ObjGlobal.CfEndAdDate + " ", "AMIS Info.", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        msk_Date.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_Date.Focus();
                }
            }
            else
            {
                if (ObjGlobal.ValidDate(msk_Date.Text, "M"))
                {
                    if (ObjGlobal.ValidDateRange(Convert.ToDateTime(ObjGlobal.ReturnEnglishDate(msk_Date.Text))) ==
                        false)
                    {
                        MessageBox.Show(
                            "Date Must be Between " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfStartAdDate.ToShortDateString()) + " and " +
                            ObjGlobal.ReturnNepaliDate(ObjGlobal.CfEndAdDate.ToShortDateString()) + " ",
                            "AMIS Info.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msk_Date.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(@"Enter Valid Date !", ObjGlobal.Caption, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    msk_Date.Focus();
                }
            }
        }
        else
        {
            MessageBox.Show("Voucher Date Cann't be Left Blank!");
            msk_Date.Focus();
        }
    }

    private void tsbtn_Search_Click(object sender, EventArgs e)
    {
        dgv_DisplayReport.ClearSelection();
        var SH = new FrmRptSearch(dgv_DisplayReport);
        if (SH.ShowDialog() == DialogResult.OK)
        {
        }
    }

    private void tsbtn_Refresh_Click(object sender, EventArgs e)
    {
        FrmAuditorsLockUnlockReport_Load(sender, e);
    }

    private void FrmAuditorsLockUnlockReport_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.P)
            tsbtn_Print_Click(sender, e);
        else if (e.Control && e.KeyCode == Keys.E)
            tsbtn_Export_Click(sender, e);
        else if (e.Control && e.KeyCode == Keys.R)
            tsbtn_Refresh_Click(sender, e);
        else if (e.Control && e.KeyCode == Keys.F) tsbtn_Search_Click(sender, e);
    }

    public void GridDesignAuditTrial(string FilterBy)
    {
        dgv_DisplayReport.ReadOnly = true;
        dgv_DisplayReport.Rows.Clear();
        dgv_DisplayReport.Columns.Clear();

        DataGridViewColumn column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Sn";
        column.Name = "gv_Sn";
        column.Width = 40;
        column.Visible = false;
        column.ReadOnly = true;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Bill No";
        column.Name = "gv_BillNo";
        column.Width = 250;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Bill Date";
        column.Name = "gv_BillDate";
        column.Width = 100;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action";
        column.Name = "gv_Action";
        column.Width = 350;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action Date";
        column.Name = "gv_ActionDate";
        column.Width = 120;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn(); // this column fixed for 6th column for zooming
        column.HeaderText = "User";
        column.Name = "gv_User";
        column.Width = 120;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);

        column = new DataGridViewTextBoxColumn();
        column.HeaderText = "Action User";
        column.Name = "gv_ActionUser";
        column.Width = 120;
        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        column.HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.Columns.Add(column);
        dgv_DisplayReport.Columns[2].Visible = true;
        dgv_DisplayReport.Columns[3].Visible = true;
        dgv_DisplayReport.Columns[4].Visible = true;
        dgv_DisplayReport.Columns[5].Visible = true;
        if (FilterBy == "User")
        {
            dgv_DisplayReport.Columns[1].Width = 250 + 120;
            dgv_DisplayReport.Columns[5].Width = 0;
            dgv_DisplayReport.Columns[5].Visible = false;
        }
        else if (FilterBy == "Action")
        {
            dgv_DisplayReport.Columns[1].Width = 250 + 120;
            dgv_DisplayReport.Columns[3].Width = 0;
            dgv_DisplayReport.Columns[3].Visible = false;
        }
        else if (FilterBy == "Bill Date")
        {
            dgv_DisplayReport.Columns[1].Width = 250 + 120;
            dgv_DisplayReport.Columns[2].Width = 0;
            dgv_DisplayReport.Columns[2].Visible = false;
        }
        else if (FilterBy == "Bill No")
        {
            dgv_DisplayReport.Columns[1].Width = 250 + 120;
            dgv_DisplayReport.Columns[4].Width = 0;
            dgv_DisplayReport.Columns[4].Visible = false;
        }

        dgv_DisplayReport.Columns[1].HeaderCell.Style.Font = new Font("Arial", 8, FontStyle.Bold);
        dgv_DisplayReport.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
    }

    private void DisplayAuditTrialSummaryReportForAction()
    {
    }

    private void UpdateLockUnlock(string Voucher_No, string Station, bool Locked)
    {
    }

    public void ZoomingRpt(string Source, string Voucher_No)
    {
    }

    #region Global

    private ObjGlobal Gobj = new();
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
    private DataTable dt = new();
    private DataTable dtTemp = new();
    private DataTable dtdata = new();
    private DataTable DtUser = new();
    private readonly FrmPrintDialog PRT = new();

    private string Query = string.Empty;
    private string Ledger_Code = string.Empty;
    private string Ledger_Name = string.Empty;
    private double DebitAmt;
    private double CreditAmt;

    private Font myFont = new("Arial", 10);
    private StringFormat strFormat; //Used to format the grid rows.
    private readonly ArrayList arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList arrColumnWidths = new(); //Used to save column widths
    private int iCellHeight; //Used to get/set the datagridview cell height
    public int PageWidth;
    public int PageHeight;
    public int GridWidth;
    public long PageNo;
    private long iRow; //Used as counter
    private int iCount;
    private bool bFirstPage; //Used to check whether we are printing first page
    private bool bNewPage; // Used to check whether we are printing a new page
    private int iHeaderHeight; //Used for the header height
    private bool bMorePagesToPrint;
    private int iLeftMargin;
    private int iRightMargin;
    private int iTopMargin;
    private int iTmpWidth;

    [DllImport("kernel32.dll")]
    public static extern bool Beep(int BeepFreq, int BeepDuration);

    #endregion Global

    #region Export Event/Method

    private void tsbtn_Export_Click(object sender, EventArgs e)
    {
        ExportReport();
        MessageBox.Show("Export Completed!");
    }

    private void ExportReport()
    {
        var FileName = string.Empty;
        var ConvFileName = string.Empty;
        int Co;
        int Ro;

        try
        {
            saveFileDialog1.Title = "Save";
            saveFileDialog1.Filter =
                "Excel File 1997-07 |*.Xls|Excel File 2010-13 |*.xlsx|PDF File |*.pdf|Word File|*.Doc|Html Page |*.Html";
            saveFileDialog1.ShowDialog();
            FileName = saveFileDialog1.FileName;

            if (saveFileDialog1.FilterIndex == 2)
            {
                ConvFileName = FileName;
                FileName = FileName.Substring(0, FileName.Length - 4) + ".Doc";
            }

            var Wrt = new StreamWriter(FileName);
            //-----------Start Report Exporting------------
            Wrt.Write("<html>");
            //-----------Report Heading------------
            Wrt.Write("<head>");
            Wrt.Write("<title>" + ObjGlobal.LogInCompany + "</title>");
            Wrt.Write("</head>");
            Wrt.Write("<body>");
            Wrt.Write("<P align=Right>");
            Wrt.Write("<System.Drawing.Font color=#FF0000>");
            Wrt.Write("<align=right><System.Drawing.Font face=Times New Roman size=2><i>" + "Accounting Period" +
                      "</i> </System.Drawing.Font> <BR>");
            Wrt.Write("<align=right><System.Drawing.Font face=Times New Roman size=2><i>" + lbl_AccPeriodDate.Text +
                      " </i></System.Drawing.Font></System.Drawing.Font><BR>");
            Wrt.Write("</P>");
            Wrt.Write("<P align=center>");
            Wrt.Write("<align=center><System.Drawing.Font face=Times New Roman size=5 color=#800000><b>" +
                      lbl_ComanyName.Text + "</b></System.Drawing.Font><BR>");
            Wrt.Write("<align=center><System.Drawing.Font face=Times New Roman size=3 color=#0000FF><b>" +
                      lbl_CompanyAddress.Text + "</b></System.Drawing.Font><BR></P>");
            //if (lbl_Branch.Text != "" )
            //{
            //    Wrt.Write "<align=Left><System.Drawing.Font face=Times New Roman size=3 ><b>" + lbl_Branch.Text.ToString() + "</b> </System.Drawing.Font><BR>");
            //    Wrt.Write "<align=Left><System.Drawing.Font face=Times New Roman size=3 ><b>" + lbl_Branch.Text.ToString() + "</b> </System.Drawing.Font><BR>");
            //}
            Wrt.Write("<p align=center>");
            Wrt.Write("<align=center><u><System.Drawing.Font face=Times New Roman size=3 color=#0000FF><b>" +
                      lbl_ReportName.Text + "</b></System.Drawing.Font></u><BR>");
            Wrt.Write("<align=center><u><System.Drawing.Font face=Times New Roman size=3 color=#0000FF><b>" +
                      lbl_ReportDate.Text + "</b></System.Drawing.Font></u>");
            Wrt.Write("<table border=1 width=100% cellspacing=0 bgcolor=#FFFFFF cellpadding=.5>");
            //-----------Report Column Heading------------
            Wrt.Write("<tr>");
            for (var i = 0; i < dgv_DisplayReport.Columns.Count; i++)
                if (dgv_DisplayReport.Columns[i].Visible)
                    Wrt.Write("<td align=center><b><System.Drawing.Font size=2>" +
                              dgv_DisplayReport.Columns[i].HeaderText.ToUpper() +
                              "</System.Drawing.Font></b></td>");

            Wrt.Write("</tr>");
            //-----------Report Details--------------------
            string FontBol;
            string FontAlign;

            for (Ro = 0; Ro < dgv_DisplayReport.Rows.Count; Ro++)
            {
                Wrt.Write("<tr>");
                for (Co = 0; Co < dgv_DisplayReport.Columns.Count; Co++)
                {
                    FontBol = string.Empty;
                    if (dgv_DisplayReport.Rows[Ro].Cells[Co].InheritedStyle.Font.Bold)
                        FontBol = "<B>";

                    if (dgv_DisplayReport.Rows[Ro].Cells[Co].InheritedStyle.Font.Italic)
                        FontBol = "<I>";

                    FontAlign = string.Empty;
                    if (dgv_DisplayReport.Rows[Ro].Cells[Co].InheritedStyle.Alignment ==
                        DataGridViewContentAlignment.MiddleCenter)
                        FontAlign = "Align = Center";
                    else if (dgv_DisplayReport.Columns[Co].InheritedStyle.Alignment ==
                             DataGridViewContentAlignment.MiddleLeft)
                        FontAlign = "Align = Left";
                    else if (dgv_DisplayReport.Columns[Co].InheritedStyle.Alignment ==
                             DataGridViewContentAlignment.MiddleRight)
                        FontAlign = "Align = Right";
                    if (dgv_DisplayReport.Rows[Ro].Cells[Co].Visible)
                        Wrt.Write("<td " + FontAlign + ">" + FontBol +
                                  "<System.Drawing.Font face=Courier New size=2>" +
                                  dgv_DisplayReport.Rows[Ro].Cells[Co].Value + "</System.Drawing.Font></td></B>");

                    //if (dgv_DisplayReport.Rows[Ro].Cells[Co].Value != null && dgv_DisplayReport.Rows[Ro].Cells[Co].Value != "")
                    //{
                    //    //if (saveFileDialog1.FilterIndex == 3)
                    //    //Wrt.Write("<td " + FontAlign + ">" + FontBol + "<System.Drawing.Font face=Courier New size=2>" + ((dgv_DisplayReport.Rows[Ro].Cells[Co].Value != "" && dgv_DisplayReport.Rows[Ro].Cells[Co].Value != null) ? dgv_DisplayReport.Rows[Ro].Cells[Co].Value.ToString() : "&nbsp;") + "</System.Drawing.Font></td></B>");
                    //    //else
                    //        Wrt.Write("<td " + FontAlign + ">" + FontBol + "<System.Drawing.Font face=Courier New size=2>" + dgv_DisplayReport.Rows[Ro].Cells[Co].Value.ToString() + "</System.Drawing.Font></td></B>");
                    //}
                    //else
                    //    Wrt.Write("<td " + FontAlign + ">" + FontBol + "<System.Drawing.Font face=Courier New size=2>" + "&nbsp;" + "</System.Drawing.Font></td></B>");
                    //    }
                    //}
                }

                Wrt.Write("</tr>");
            }

            Wrt.Write("</table>");
            Wrt.Write("</body>");
            Wrt.Write("</html>");
            Wrt.Close();
            //-----------End Report Exporting------------
            if (saveFileDialog1.FilterIndex == 2)
                if (FileName != string.Empty && ConvFileName != string.Empty)
                {
                    ConvertWordToPdf(FileName, ConvFileName);
                    //ConvertExcelToPdf(FileName, ConvFileName);
                    File.Delete(FileName);
                }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error Assigning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    public string ConvertWordToPdf(string inputFile, string outputFileName)
    {
        ////string outputFileName = "Desired Output File Path";
        var wordApp = new Application();
        Document wordDoc = null;
        object inputFileTemp = inputFile;
        try
        {
            wordDoc = wordApp.Documents.Open(inputFile);
            wordDoc.ExportAsFixedFormat(outputFileName, WdExportFormat.wdExportFormatPDF);
        }
        finally
        {
            if (wordDoc != null) wordDoc.Close(WdSaveOptions.wdDoNotSaveChanges);
            if (wordApp != null)
            {
                wordApp.Quit(WdSaveOptions.wdDoNotSaveChanges);
                wordApp = null;
            }
        }

        return outputFileName;
    }

    public string ConvertExcelToPdf(string inputFile, string outputFileName)
    {
        //string outputFileName = "DesireOutput File Path";
        var excelApp = new Microsoft.Office.Interop.Excel.Application();
        excelApp.Visible = false;
        Workbook workbook = null;
        Workbooks workbooks = null;
        try
        {
            workbooks = excelApp.Workbooks;
            workbook = workbooks.Open(inputFile);
            workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, outputFileName,
                XlFixedFormatQuality.xlQualityStandard, true, true, Type.Missing, Type.Missing, false,
                Type.Missing);
        }
        finally
        {
            if (workbook != null)
            {
                workbook.Close(XlSaveAction.xlDoNotSaveChanges);
                while (Marshal.FinalReleaseComObject(workbook) != 0)
                {
                }

                ;
                workbook = null;
            }

            if (workbooks != null)
            {
                workbooks.Close();
                while (Marshal.FinalReleaseComObject(workbooks) != 0)
                {
                }

                ;
                workbooks = null;
            }

            if (excelApp != null)
            {
                excelApp.Quit();
                excelApp.Application.Quit();
                while (Marshal.FinalReleaseComObject(excelApp) != 0)
                {
                }

                ;
                excelApp = null;
            }
        }

        return outputFileName;
    }

    #endregion Export Event/Method

    #region Print Event/Method

    private void tsbtn_Print_Click(object sender, EventArgs e)
    {
        if (PRT.ShowDialog() == DialogResult.OK)
            if (PRT.No_Copy > 0)
            {
                for (var i = 1; i <= PRT.No_Copy; i++)
                {
                    PageNo = 0;
                    printDocument1.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                    if (PRT.Paper_Size == "A4 Full")
                    {
                        //printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 387, 750);
                        //printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.A4.ToString();
                    }
                    else if (PRT.Paper_Size == "Letter Full")
                    {
                        //printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.Letter.ToString();
                    }

                    printDocument1.DefaultPageSettings.Landscape = PRT.Landscape;
                    printDocument1.Print();
                }

                if (PRT.N_Line <= 0)
                    return;
            }

        MessageBox.Show("Print Completed!");
    }

    private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
    {
        try
        {
            strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Trimming = StringTrimming.EllipsisCharacter;

            arrColumnLefts.Clear();
            arrColumnWidths.Clear();
            iCellHeight = 0;
            iCount = 0;
            bFirstPage = true;
            bNewPage = true;

            // Calculating Total Widths
            GridWidth = 0;
            foreach (DataGridViewColumn dgvGridCol in dgv_DisplayReport.Columns)
                if (dgvGridCol.Visible)
                    GridWidth += dgvGridCol.Width;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        PrintFunction(e);
    }

    private void PrintHeaderFunction(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 615;
            if (PRT.Landscape)
                LeftMargin = LeftMargin + 150;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(lbl_ComanyName.Text, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("Accounting Period", myFont, Brushes.Black,
                new RectangleF(LeftMargin, iTopMargin, 150, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(lbl_ComanyName.Text, myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(lbl_CompanyAddress.Text, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(lbl_AccPeriodDate.Text, myFont, Brushes.Black,
                new RectangleF(LeftMargin, iTopMargin, 150, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(lbl_CompanyAddress.Text, myFont).Height + 5;
            //    if (lbl_Branch.Text.Trim() != "")
            //    {
            //        e.Graphics.DrawString(lbl_Branch.Text, myFont, Brushes.Black, lbl_Branch.Location.X, lbl_AccPeriodDate.Location.Y);
            //    }

            //    if (lbl_Branch.Text.Trim() != "")
            //    {
            //        e.Graphics.DrawString(lbl_Branch.Text, myFont, Brushes.Black, lbl_Branch.Location.X, lbl_AccPeriodDate.Location.Y);
            //    }

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(" " + lbl_ReportName.Text + " ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            //if (ObjGlobal._Printing_DateTime == true)
            //{
            //    strFormat.Alignment = StringAlignment.Center;
            //    myFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            //    string PrintDate = "Date/Time : " + System.DateTime.Now;
            //    e.Graphics.DrawString(PrintDate, myFont, Brushes.Black, new RectangleF(LeftMargin - 25, (float)iTopMargin, (int)(lbl_DateTime.Width + 50), (float)iCellHeight), strFormat);
            //}
            iTopMargin += (int)e.Graphics.MeasureString(lbl_ReportName.Text, myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Report Date  " + lbl_ReportDate.Text, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            if (PageNo < 1)
            {
                if (PRT.Start_Page > 0)
                    PageNo = PRT.Start_Page - 1;
                else
                    PageNo = 0;
            }

            PageNo = PageNo + 1;
            string PNo;
            PNo = "Page No. " + PageNo;
            //strFormat.Alignment = StringAlignment.Center;
            //e.Graphics.DrawString(PNo, myFont, Brushes.Black, new RectangleF(LeftMargin, (float)iTopMargin, (int)lbl_PageNo.Width, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString(PNo, myFont).Height);

            //Draw Grid Header
            e.Graphics.DrawString("Items Ordered", new Font(dgv_DisplayReport.Font, FontStyle.Bold), Brushes.Black,
                e.MarginBounds.Left,
                e.MarginBounds.Top - e.Graphics.MeasureString("Items Ordered",
                    new Font(dgv_DisplayReport.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13);
            var strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            //Draw Date
            e.Graphics.DrawString(strDate, new Font(dgv_DisplayReport.Font, FontStyle.Bold), Brushes.Black,
                e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                    new Font(dgv_DisplayReport.Font, FontStyle.Bold), e.MarginBounds.Width).Width),
                e.MarginBounds.Top - e.Graphics.MeasureString("Items Ordered",
                    new Font(new Font(dgv_DisplayReport.Font, FontStyle.Bold), FontStyle.Bold),
                    e.MarginBounds.Width).Height - 13);
            //Draw Columns
            iTopMargin += 20;
            myFont = new Font("Arial", 9, FontStyle.Bold);
            if (bFirstPage)
                foreach (DataGridViewColumn GridCol in dgv_DisplayReport.Columns)
                    if (GridCol.Visible)
                    {
                        iTmpWidth = (int)Math.Floor(GridCol.Width / (double)GridWidth * PageWidth);
                        //iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width / (double)GridWidth * (double)GridWidth * ((double)e.MarginBounds.Width / (double)GridWidth))));
                        iHeaderHeight = (int)e.Graphics
                            .MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height + 11;
                        // Save width and height of headres
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }

            iCount = 0;
            foreach (DataGridViewColumn GridCol in dgv_DisplayReport.Columns)
                if (GridCol.Visible)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iHeaderHeight));
                    e.Graphics.DrawRectangle(Pens.Black,
                        new Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iHeaderHeight));
                    e.Graphics.DrawString(GridCol.HeaderText, myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iHeaderHeight), strFormat);
                    iCount++;
                }

            iTopMargin += iHeaderHeight;

            //---------End Report Header ----------------
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void PrintFunction(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100 - 35;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 25; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;
            iLeftMargin = iLeftMargin + 5;
#pragma warning disable CS0168 // The variable 'stto' is declared but never used
            long stto;
#pragma warning restore CS0168 // The variable 'stto' is declared but never used
            long LCnt;
            LCnt = 0;

            //iRow = stto;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed

            while (iRow <= dgv_DisplayReport.Rows.Count - 1)
            {
                LCnt = LCnt + 1;
                var GridRow = dgv_DisplayReport.Rows[Convert.ToInt16(iRow)];
                //Set the cell height
                iCellHeight = GridRow.Height; //+ 5
                var iCount = 0;
                //Check whether the current page settings allo more rows to print
                if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    bNewPage = true;
                    bFirstPage = false;
                    bMorePagesToPrint = true;
                    break;
                }

                if (bNewPage)
                {
                    PrintHeaderFunction(e);
                    bNewPage = false;
                }

                iCount = 0;
                myFont = !string.IsNullOrEmpty(ObjGlobal.SysFontName) && ObjGlobal.SysFontSize != 0
                    ? new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize), FontStyle.Regular)
                    : new Font("Arial", 9, FontStyle.Regular);
                foreach (DataGridViewCell cel in GridRow.Cells)
                    if (cel.Visible)
                    {
                        strFormat.Alignment = cel.InheritedStyle.Alignment switch
                        {
                            DataGridViewContentAlignment.MiddleLeft => StringAlignment.Near,
                            DataGridViewContentAlignment.MiddleCenter => StringAlignment.Center,
                            DataGridViewContentAlignment.MiddleRight => StringAlignment.Far,
                            _ => strFormat.Alignment
                        };
                        e.Graphics.DrawString(cel.Value != null
                                ? cel.Value.ToString()
                                : string.Empty, cel.InheritedStyle.Font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                        iCount++;
                    }

                iRow++;
                iTopMargin += iCellHeight;
            }

            e.HasMorePages = bMorePagesToPrint;
        }
        catch (Exception exc)
        {
            MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    #endregion Print Event/Method
}