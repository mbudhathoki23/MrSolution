using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;

namespace PrintControl.Print.DirectPrint;

public class DocPrintingVoucher
{
    public string PrintDoc(bool print)
    {
        _printDocument.BeginPrint += printDocument1_BeginPrint;
        _printDocument.PrintPage += printDocument1_PrintPage;
        PPD.Click += printPreviewDialog1_Click;
        Printed = false;

        if (Convert.ToInt16(NoOf_Copy) > 0)
        {
            _dataTable = GetConnection.SelectDataTableQuery(
                "SELECT Company_Name,Address,Country,City,State ,PhoneNo,Pan_No,Email,Website  FROM AMS.CompanyInfo");

            for (var Noc = 1; Noc <= Convert.ToInt16(NoOf_Copy); Noc++)
            {
                //PageNo = 0;
                _printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                _printDocument.PrinterSettings.PrinterName = Printer_Name;
                if (DocDesign_Name is "A4 Full" or "Receipt Voucher" or "Payment Voucher")
                {
                    N_Line = 30;
                    //printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.A4.ToString();
                    _printDocument.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                }
                else if (DocDesign_Name == "A4 Half")
                {
                    N_Line = 5;
                    _printDocument.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                }
                else if (DocDesign_Name == "LETTER FULL")
                {
                    _printDocument.DefaultPageSettings.PaperSize.PaperName = PaperKind.Letter.ToString();
                }
                //printDocument1.DefaultPageSettings.Landscape = PRT.Landscape;

                Query = string.Empty;
                VouNo = string.Empty;
                if (Module == "JV") //|| Module == "RV" || Module == "PV" || Module == "CB" || Module == "RPV" || Module == "DN" || Module == "CN"
                {
                    if (Module == "RV" && DocDesign_Name == "Receipt")
                    {
                        N_Line = 5;
                        _printDocument.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                    }

                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}' ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }
                else if (Module == "RV")
                {
                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}'";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }
                else if (Module == "PV")
                {
                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}'";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }
                else if (Module == "CB")
                {
                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}'";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }
                else if (Module == "RPV")
                {
                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}'";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }
                else if (Module == "DN")
                {
                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}'";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }
                else if (Module == "DN")
                {
                    Query = $"SELECT Voucher_No FROM AMS.AccountDetails Where Module='{Module}'";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = $"{Query} and Voucher_No>='{FromDocNo}' and Voucher_No<='{ToDocNo}'";
                    }
                    else
                    {
                        Query = ObjGlobal.SysDateType == "AD" ? $"{Query} and Voucher_Date between '{FromDate}' and '{ToDate}'" : $"{Query} and Voucher_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                    }
                }

                if (Query != string.Empty)
                {
                    DTNoOfVou.Reset();
                    DTNoOfVou = GetConnection.SelectDataTableQuery(Query);
                    if (DTNoOfVou.Rows.Count <= 0)
                    {
                        Msg = "Does not exit Voucher No!";
                        return Msg;
                    }

                    foreach (DataRow VNro in DTNoOfVou.Rows)
                    {
                        VouNo = VNro["Voucher_No"].ToString();
                        DRo = 0;
                        i = 0;
                        if (print)
                        {
                            _printDocument.PrintController =
                                printController; //User Printing page dialog press enter enter(2 times) button then cancel print voucher
                            _printDocument.Print();
                        }
                        else
                        {
                            PPD.Document = _printDocument;
                            PPD.PrintPreviewControl.Zoom = 1;
                            ((Form)PPD).WindowState = FormWindowState.Maximized;
                            PPD.ShowDialog();
                        }
                    }
                }
            }

            foreach (DataRow VNro in DTNoOfVou.Rows)
            {
                //string PrintDate;
                //PrintDate = ObjGlobal._Current_Date.ToShortDateString();
                //string[] split;
                //split = PrintDate.Split(new char[] { '/', ' ' });
                //PrintDate = Convert.ToString(split[2].ToString() + "/" + split[1].ToString() + "/" + split[0].ToString());
                VouNo = VNro["Voucher_No"].ToString();
                dt.Reset();
                Query =
                    $"Insert Into AMS.Print_Voucher Values ('{VouNo}', '{Module}', {NoOf_Copy}, {ObjGlobal.LogInUserId},getdate(), {ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId})";
                dt = GetConnection.SelectDataTableQuery(Query);
            }

            //dt.Reset();
            //if (Module == "JV" || Module == "RV" || Module == "PV" || Module == "RPV" || Module == "CV" || Module == "DN" || Module == "CN")
            //{
            //    Query = "Update AMS.Voucher_Main Set Printed=1 Where FiscalYear_Id=" + ObjGlobal._FiscalYear_Id + " and ('0'='" + ObjGlobal._Branch_Id + "' or Branch_Id=" + ObjGlobal._Branch_Id + ")";
            //    if (FromDocNo.Trim() != "" && ToDocNo.Trim() != "")
            //        Query = Query + " and Voucher_No>='" + FromDocNo + "' and Voucher_No<='" + ToDocNo + "'";
            //    else
            //    {
            //        if (ObjGlobal._Date_Type == "AD")
            //            Query = Query + " and Voucher_Date between '" + FromDate + "' and '" + ToDate + "'";
            //        else
            //            Query = Query + " and Voucher_Date between '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) + "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)) + "'";
            //    }
            //    dt = GetConnection.SelectDataTableQuery(Query);
            //}
        }

        if (print) Msg = "Print Completed!";

        return Msg;
    }

    private void printDocument1_BeginPrint(object sender, CancelEventArgs e)
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
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        if (Module is "JV" or "RV" or "PV")
        {
            switch (Module)
            {
                case "RV" when DocDesign_Name == "Receipt":
                    {
                        PrintReceiptDetails(e);
                        break;
                    }
                case "RV" or "PV" when
                    DocDesign_Name is "Receipt Voucher" or "Payment Voucher":
                    {
                        PrintReceiptPaymentVoucherDetails(e);
                        break;
                    }
                default:
                    {
                        PrintJVDetails(e);
                        break;
                    }
            }
        }
        else if (Module is "RV" or "PV" or "CB" or "RPV")
        {
            if (Module == "RV" && DocDesign_Name == "Receipt")
                PrintReceiptDetails(e);
            else
                PrintReceiptPaymentVoucherDetails(e);
        }

        if (Module is "DN" or "CN") PrintDNCNDetails(e);
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        PPD.Document = _printDocument;
        PPD.ShowDialog();
    }

    #region --------------------------------------------- Global ---------------------------------------------

    private DataTable _dataTable = new();
    private readonly System.Drawing.Printing.PrintDocument _printDocument = new();
    private readonly PrintController printController = new StandardPrintController();
    private readonly PrintPreviewDialog PPD = new();
    private DataTable dt = new();
    private DataTable DTTemp = new();
    private DataTable DTNoOfVou = new();
    private DataTable DTVouMain = new();
    private DataTable DTVouDetails = new();
    private DataTable DTTermDetails = new();
    private DataTable DTProdTerm = new();
    private DataTable DTBillTerm = new();
    private DataSet ds = new();
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.str' is never used
    private string str;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.str' is never used
    private string Query;
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.FromAdDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.ToADDate' is never used
    private double TotDrAmt;
    private double TotCrAmt;
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.TotAltQty' is never used
    private double TotAltQty;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.TotAltQty' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.TotQty' is never used
    private double TotQty;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.TotQty' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.TotBasicAmt' is never used
    private double TotBasicAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.TotBasicAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.TotNetAmt' is never used
    private double TotNetAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.TotNetAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.TotGrandAmt' is never used
    private double TotGrandAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.TotGrandAmt' is never used
    private Font myFont = new("Arial", 10);
#pragma warning disable CS0414 // The field 'DocPrinting_Voucher.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_Voucher.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Voucher.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_Voucher.rowPosition' is assigned but its value is never used
    private int N_Line;
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.N_FootLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Voucher.N_TermDet' is never used
    private int N_TermDet;
#pragma warning restore CS0169 // The field 'DocPrinting_Voucher.N_TermDet' is never used
    private int RemarksTotLen;
    private StringFormat strFormat; //Used to format the grid rows.
    private ArrayList ColHeaders = new();
    private readonly ArrayList ColWidths = new();
    private ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList arrColumnWidths = new(); //Used to save column widths
    private int iCellHeight; //Used to get/set the datagridview cell height
    public int PageWidth;
    public int PageHeight;
    public long PageNo;
    private long iRow; //Used as counter
#pragma warning disable CS0414 // The field 'DocPrinting_Voucher.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_Voucher.iCount' is assigned but its value is never used
    private bool bFirstPage; //Used to check whether we are printing first page
    private bool bNewPage; // Used to check whether we are printing a new page
    private int iHeaderHeight; //Used for the header height
    private bool bMorePagesToPrint;
    private int iLeftMargin;
    private int iRightMargin;
    private int iTopMargin;
#pragma warning disable CS0414 // The field 'DocPrinting_Voucher.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Voucher.iTmpWidth' is assigned but its value is never used
    private int i;
    private int DRo;
    private string VouNo = string.Empty;
    private bool CashVoucher;
    private string Msg = string.Empty;
#pragma warning disable CS0414 // The field 'DocPrinting_Voucher.Printed' is assigned but its value is never used
    private bool Printed;
#pragma warning restore CS0414 // The field 'DocPrinting_Voucher.Printed' is assigned but its value is never used

    public string FrmName { get; set; }

    public string Module { get; set; }

    public string SelectDataTableQuery { get; set; }

    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public string FromDocNo { get; set; }

    public string ToDocNo { get; set; }

    public string InvoiceType { get; set; }

    public string Printer_Name { get; set; }

    public string DocDesign_Name { get; set; }

    public int NoOf_Copy { get; set; }

    public string DocDesign_Notes { get; set; }

    #endregion --------------------------------------------- Global ---------------------------------------------

    #region --------------------------------------------- Journal Voucher Design ---------------------------------------------

    #region --------------------------------------------- Journal Voucher Default Design ---------------------------------------------

    private void PrintJVHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20;
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (_printDocument.DefaultPageSettings.Landscape) LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "JV")
            {
                e.Graphics.DrawString("Journal Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "RV")
            {
                if (CashVoucher == false)
                    e.Graphics.DrawString("Bank Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Cash Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "PV")
            {
                if (CashVoucher == false)
                    e.Graphics.DrawString("Bank Payment Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Cash Payment Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Voucher No : {DTVouMain.Rows[0]["Voucher_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Voucher No : ", myFont).Height + 5;
            if (Module != "JV")
                if (DTVouMain.Rows[0]["Chq_No"] != null && DTVouMain.Rows[0]["Chq_No"].ToString() != string.Empty)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"Cheque No  : {DTVouMain.Rows[0]["Chq_No"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                    if (DTVouMain.Rows[0]["Chq_Date"] != null &&
                        DTVouMain.Rows[0]["Chq_Date"].ToString() != string.Empty)
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            e.Graphics.DrawString(
                                $"Cheque Date(AD)  : {Convert.ToDateTime(DTVouMain.Rows[0]["Chq_Date"].ToString()).ToShortDateString()}{string.Empty}",
                                myFont, Brushes.Black, new RectangleF(300, iTopMargin, 800, iCellHeight), strFormat);
                        else
                            e.Graphics.DrawString(
                                $"Cheque Date(BS)    : {ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["Chq_Date"]).ToShortDateString())}{string.Empty}",
                                myFont, Brushes.Black, new RectangleF(300, iTopMargin, 800, iCellHeight), strFormat);
                    }
                }

            strFormat.Alignment = StringAlignment.Near;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)    : {Convert.ToDateTime(DTVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)    : {DTVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            Query =
                $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{DTVouMain.Rows[0]["Voucher_No"]}' and SubLedger_Id is not null";
            DTTemp.Reset();
            DTTemp = GetConnection.SelectDataTableQuery(Query);
            if (DTTemp.Rows.Count > 0)
                e.Graphics.DrawString(
                    "S.N.     Ledger                                              Sub Ledger                                                         Debit Amt              Credit Amt",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    "S.N.     Particulars                                                                                                                    Debit Amt              Credit Amt",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            iTopMargin += 1;

            //---------End Report Header ----------------
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintJVDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            long LCnt;
            LCnt = 0;

            iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;

                if (Module == "JV")
                    Query =
                        $"SELECT Voucher_No,Voucher_Date,Voucher_BsDate,VM.Ledger_Code,Remarks FROM AMS.Voucher_Main as VM Where Module='{Module}'";
                else
                    Query =
                        $"SELECT Voucher_No,Voucher_Date,Voucher_BsDate,L.Ledger_Code,L.Ledger_Name,Chq_No,Chq_Date,Remarks FROM AMS.Voucher_Main as VM Inner Join AMS.Ledger as L on L.Ledger_Code=VM.Ledger_Code Where VM.Module='{Module}'";

                Query =
                    $"{Query} and VM.Voucher_No ='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);

                CashVoucher = false;
                Query =
                    $"Select distinct Ledger_Id as Id,Ledger_Code as Code,Ledger_Name as Name from AMS.Ledger as L  where (L.Ledger_Code in ('000023')) and L.Ledger_Code = '{DTVouMain.Rows[0]["Ledger_Code"]}' and Is_Ledger=1 and Status=1 order by L.Ledger_Code ";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);
                if (DTTemp.Rows.Count > 0) CashVoucher = true;

                Query =
                    $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{VouNo}' and SubLedger_Id is not null";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "SELECT VM.Voucher_No,L.Ledger_Code,L.Ledger_Name,SL.SubLed_Id,SL.Name as SL_Name,Dr_Amt,Cr_Amt,Narration FROM ";
                if (Module == "JV")
                    Query = $"{Query}  AMS.Voucher_Details as VD ";
                else
                    Query = $"{Query}  AMS.AcTran as VD ";

                Query = $"{Query} Inner Join AMS.Voucher_Main as VM On VM.Voucher_No=VD.Voucher_No ";
                Query =
                    $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=VD.Ledger_Code Left Outer Join AMS.Sub_Ledger as SL On SL.SubLed_Id=VD.SubLedger_Id ";
                Query =
                    $"{Query} Where VM.Module='{Module}' and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                if (Module == "JV")
                    Query = $"{Query} Order by VD_Id,Dr_Amt desc,Cr_Amt";
                else
                    Query = $"{Query} Order by Dr_Amt desc,Cr_Amt";

                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                ColWidths.Add(50);
                if (DTTemp.Rows.Count > 0)
                {
                    ColWidths.Add(230);
                    ColWidths.Add(230);
                }
                else
                {
                    ColWidths.Add(460);
                    ColWidths.Add(0);
                }

                ColWidths.Add(125);
                ColWidths.Add(125);

                for (var i = 0; i < ColWidths.Count; i++)
                {
                    iHeaderHeight = (int)e.Graphics
                        .MeasureString("SNo.", myFont, Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++) //foreach(DataRow ro in DTVouDetails.Rows)
                {
                    //Set the cell height
                    iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintJVHeader(e);
                        bNewPage = false;
                    }

                    DRo = DRo + 1;
                    LCnt = LCnt + 1;
                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (!string.IsNullOrEmpty(ObjGlobal.SysFontName) && ObjGlobal.SysFontSize != 0 &&
                        ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DRo}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Ledger_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null &&
                            Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"].ToString()) != 0)
                            e.Graphics.DrawString($"     {DTVouDetails.Rows[i]["Ledger_Name"]}", myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        else
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["SL_Name"].ToString() != null &&
                        DTVouDetails.Rows[i]["SL_Name"].ToString() != string.Empty)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["SL_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Dr_Amt"].ToString() != null)
                    {
                        TotDrAmt = TotDrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Dr_Amt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Dr_Amt"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null)
                    {
                        TotCrAmt = TotCrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Cr_Amt"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;
                    if (DTVouDetails.Rows[i]["Narration"].ToString() != string.Empty &&
                        DTVouDetails.Rows[i]["Narration"].ToString() != null)
                    {
                        iRow++;
                        iTopMargin += iCellHeight;
                        strFormat.Alignment = StringAlignment.Near;
                        RemarksTotLen = DTVouDetails.Rows[i]["Narration"].ToString().Length;
                        myFont = new Font("Arial", 8, FontStyle.Regular);
                        if (DTVouDetails.Rows[0]["Narration"].ToString().Length <= 70)
                        {
                            e.Graphics.DrawString($"  Narr : {DTVouDetails.Rows[i]["Narration"]}", myFont,
                                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight),
                                strFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(
                                $"   Narr : {DTVouDetails.Rows[i]["Narration"].ToString().Substring(0, 70)}", myFont,
                                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight),
                                strFormat);
                            iTopMargin += (int)e.Graphics.MeasureString("Narr", myFont).Height + 5;
                            if (RemarksTotLen > 140)
                            {
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                                iTopMargin += (int)e.Graphics.MeasureString("Narr", myFont).Height + 5;
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(141, RemarksTotLen - 141)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(
                                    $"  {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                            }
                        }
                    }

                    iRow++;
                    iTopMargin += iCellHeight;
                }

                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.HasMorePages = false;
            }

            if (LCnt < N_Line)
                for (var ln = LCnt; ln <= N_Line; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            //e.Graphics.DrawString("On Account Of : " + DTVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);

            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            myFont = new Font("Arial", 9, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length <= 120)
            {
                e.Graphics.DrawString($"On Account Of : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString(
                    $"On Account Of : {DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120)} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                if (RemarksTotLen > 220)
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
            }

            if (Module is not ("RV" and "PV"))
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 30;
            else
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ---------------------------                                    -------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                "           Prepared By          " +
                "                                                   Checked By 	                                                " +
                " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;

            if (Module == "RV")
            {
                iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 10;
                e.Graphics.DrawString("     --------------------------                             ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString("           Paid By                  ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else if (Module == "PV")
            {
                iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 10;
                e.Graphics.DrawString("     --------------------------                             ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString("           Received By                     ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintJVDetailsOld(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            TotDrAmt = 0;
            TotCrAmt = 0;
            long LCnt;
            LCnt = 0;

            iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                ColWidths.Add(50);
                if (DTTemp.Rows.Count > 0)
                {
                    ColWidths.Add(250);
                    ColWidths.Add(250);
                }
                else
                {
                    ColWidths.Add(500);
                    ColWidths.Add(0);
                }

                ColWidths.Add(125);
                ColWidths.Add(125);

                for (var i = 0; i < ColWidths.Count; i++)
                {
                    iHeaderHeight = (int)e.Graphics
                        .MeasureString("SNo.", myFont, Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++) //foreach(DataRow ro in DTVouDetails.Rows)
                {
                    DRo = DRo + 1;
                    LCnt = LCnt + 1;
                    //Set the cell height
                    iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top) //LCnt >= N_Line)//
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        // ---------Start Report Header ----------------
                        var LeftMargin = 0;
                        LeftMargin = iLeftMargin + 650;
                        if (_printDocument.DefaultPageSettings.Landscape) LeftMargin = LeftMargin + 200;

                        myFont = new Font("Arial", 12, FontStyle.Bold);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                            new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                            new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

                        myFont = new Font("Arial", 12, FontStyle.Bold);
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString("Journal Voucher", myFont, Brushes.Black,
                            new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Journal Voucher", myFont).Height + 5;

                        strFormat.Alignment = StringAlignment.Far;
                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        e.Graphics.DrawString($"Voucher No : {DTVouMain.Rows[0]["Voucher_No"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Voucher No : ", myFont).Height + 5;

                        strFormat.Alignment = StringAlignment.Far;
                        if (ObjGlobal.SysDateType == "AD")
                            e.Graphics.DrawString(
                                $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString()}{string.Empty}",
                                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                        else
                            e.Graphics.DrawString(
                                $"Date(BS)   : {DTVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                            myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height +
                                      1;
                        myFont = new Font("Arial", 10, FontStyle.Bold);

                        if (DTTemp.Rows.Count > 0)
                            e.Graphics.DrawString(
                                "S.N.     Ledger                                                   Sub Ledger                                                            Debit Amt              Credit Amt",
                                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
                        else
                            e.Graphics.DrawString(
                                "S.N.     Particulars                                                                                                                            Debit Amt              Credit Amt",
                                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
                        e.Graphics.DrawString(
                            "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                            myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height +
                                      1;

                        iTopMargin += 1;

                        bNewPage = false;
                        //---------End Report Header ----------------
                    }

                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DRo}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Ledger_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null &&
                            Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"].ToString()) != 0)
                            e.Graphics.DrawString($"     {DTVouDetails.Rows[i]["Ledger_Name"]}", myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        else
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["SL_Name"].ToString() != null &&
                        DTVouDetails.Rows[i]["SL_Name"].ToString() != string.Empty)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["SL_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Dr_Amt"].ToString() != null)
                    {
                        TotDrAmt = TotDrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Dr_Amt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Dr_Amt"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null)
                    {
                        TotCrAmt = TotCrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Cr_Amt"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.HasMorePages = false;
            }

            if (LCnt < N_Line)
                for (var ln = LCnt; ln <= N_Line; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 500, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"On Account Of : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            e.Graphics.DrawString($"           {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ---------------------------                                    -------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                "           Prepared By          " +
                "                                                   Checked By 	                                                " +
                " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion --------------------------------------------- Journal Voucher Default Design ---------------------------------------------

    #endregion --------------------------------------------- Journal Voucher Design ---------------------------------------------

    #region --------------------------------------------- Receipt/Payment/Contra Voucher Design ---------------------------------------------

    #region --------------------------------------------- Receipt/Payment/Contra Voucher Default Design ---------------------------------------------

    private void PrintReceiptPaymentVoucherHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20;
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (_printDocument.DefaultPageSettings.Landscape) LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "RV")
            {
                if (CashVoucher == false)
                    e.Graphics.DrawString("Bank Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Cash Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "PV")
            {
                if (CashVoucher == false)
                    e.Graphics.DrawString("Bank Payment Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Cash Payment Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "CV")
            {
                e.Graphics.DrawString("Contra Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "RPV")
            {
                e.Graphics.DrawString("Receipt/Payment Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Receipt/Payment Voucher", myFont).Height + 10;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Cash/Bank : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Voucher No : {DTVouMain.Rows[0]["Voucher_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Voucher No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["Chq_No"] != null && DTVouMain.Rows[0]["Chq_No"].ToString() != string.Empty)
            {
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"Cheque No  : {DTVouMain.Rows[0]["Chq_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                if (ObjGlobal.SysDateType == "AD")
                    e.Graphics.DrawString(
                        $"Cheque Date(AD) : {Convert.ToDateTime(DTVouMain.Rows[0]["Chq_Date"].ToString()).ToShortDateString()}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(300, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString(
                        $"Cheque Date(BS) : {ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["Chq_Date"]).ToShortDateString())}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(300, iTopMargin, 800, iCellHeight), strFormat);
            }

            strFormat.Alignment = StringAlignment.Near;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            Query =
                $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{DTVouMain.Rows[0]["Voucher_No"]}' and SubLedger_Id is not null";
            DTTemp.Reset();
            DTTemp = GetConnection.SelectDataTableQuery(Query);
            if (DTTemp.Rows.Count > 0)
            {
                //e.Graphics.DrawString("S.N.     Ledger                                              Sub Ledger                                                                                         Amount", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
                if (Module == "RV")
                    e.Graphics.DrawString(
                        "S.N.     Ledger                                              Sub Ledger                                                                              Receipt Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module == "PV")
                    e.Graphics.DrawString(
                        "S.N.     Ledger                                              Sub Ledger                                                                              Payment Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module is "CV" or "RPV")
                    e.Graphics.DrawString(
                        "S.N.     Ledger                                              Sub Ledger                                                Payment Amt            Receipt Amt",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else
            {
                if (Module == "RV")
                    e.Graphics.DrawString(
                        "S.N.     Particulars                                                                                                                                        Receipt Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module == "PV")
                    e.Graphics.DrawString(
                        "S.N.     Particulars                                                                                                                                        Payment Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module is "CV" or "RPV")
                    e.Graphics.DrawString(
                        "S.N.     Particulars                                                                                                               Payment Amt            Receipt Amt",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            iTopMargin += 1;
            myFont = new Font("Arial", 9, FontStyle.Bold);

            //---------End Report Header ----------------
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintReceiptPaymentVoucherDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            long LCnt;
            LCnt = 0;

            iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);

            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    $"SELECT Voucher_No,Voucher_Date,Voucher_BsDate,L.Ledger_Code,L.Ledger_Name,Chq_No,Chq_Date,Remarks FROM AMS.Voucher_Main as VM Inner Join AMS.Ledger as L on L.Ledger_Code=VM.Ledger_Code Where VM.Module='{Module}'";
                Query =
                    $"{Query} and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                CashVoucher = false;
                Query =
                    $"Select distinct Ledger_Id as Id,Ledger_Code as Code,Ledger_Name as Name from AMS.Ledger as L  where (L.Ledger_Code in ('000023')) and L.Ledger_Code = '{DTVouMain.Rows[0]["Ledger_Code"]}' and Is_Ledger=1 and Status=1 order by L.Ledger_Code ";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);
                if (DTTemp.Rows.Count > 0) CashVoucher = true;

                Query =
                    $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{VouNo}' and SubLedger_Id is not null";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "SELECT VM.Voucher_No,L.Ledger_Code,L.Ledger_Name,SL.SubLed_Id,SL.Name as SL_Name,Dr_Amt,Cr_Amt,Narration ";
                Query =
                    $"{Query} FROM AMS.Voucher_Details as VD Inner Join AMS.Voucher_Main as VM On VM.Voucher_No=VD.Voucher_No ";
                Query =
                    $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=VD.Ledger_Code Left Outer Join AMS.Sub_Ledger as SL On SL.SubLed_Id=VD.SubLedger_Id ";
                Query =
                    $"{Query} Where VM.Module= '{Module}' and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                ColWidths.Add(50);
                if (DTTemp.Rows.Count > 0)
                {
                    ColWidths.Add(230);
                    ColWidths.Add(230);
                }
                else
                {
                    ColWidths.Add(460);
                    ColWidths.Add(0);
                }

                if (Module is "CV" or "RPV")
                {
                    ColWidths.Add(125);
                    ColWidths.Add(125);
                }
                else if (Module == "PV")
                {
                    ColWidths.Add(250);
                    ColWidths.Add(0);
                }
                else if (Module == "RV")
                {
                    ColWidths.Add(0);
                    ColWidths.Add(250);
                }

                foreach (var t in ColWidths)
                {
                    iHeaderHeight =
                        (int)e.Graphics.MeasureString("SNo.", myFont, Convert.ToInt16(t.ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(t);
                    iLeftMargin += Convert.ToInt16(t.ToString());
                }
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++) //foreach(DataRow ro in DTVouDetails.Rows)
                {
                    //Set the cell height
                    iCellHeight = 20;
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
                        PrintReceiptPaymentVoucherHeader(e);
                        bNewPage = false;
                    }

                    iCount = 0;
                    DRo = DRo + 1;
                    LCnt = LCnt + 1;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DRo}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Ledger_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["SL_Name"].ToString() != null &&
                        DTVouDetails.Rows[i]["SL_Name"].ToString() != string.Empty)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["SL_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (Module is "PV" or "CV" or "RPV")
                        if (DTVouDetails.Rows[i]["Dr_Amt"].ToString() != null)
                        {
                            TotDrAmt = TotDrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Dr_Amt"]);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(
                                Convert.ToDecimal(DTVouDetails.Rows[i]["Dr_Amt"].ToString())
                                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        }

                    iCount++;
                    if (Module is "RV" or "CV" or "RPV")
                        if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null)
                        {
                            TotCrAmt = TotCrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"]);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(
                                Convert.ToDecimal(DTVouDetails.Rows[i]["Cr_Amt"].ToString())
                                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        }

                    iCount++;
                    if (DTVouDetails.Rows[i]["Narration"].ToString() != string.Empty &&
                        DTVouDetails.Rows[i]["Narration"].ToString() != null)
                    {
                        iRow++;
                        iTopMargin += iCellHeight;
                        strFormat.Alignment = StringAlignment.Near;
                        RemarksTotLen = DTVouDetails.Rows[i]["Narration"].ToString().Length;
                        myFont = new Font("Arial", 8, FontStyle.Regular);
                        if (DTVouDetails.Rows[0]["Narration"].ToString().Length <= 70)
                        {
                            e.Graphics.DrawString($"  Narr : {DTVouDetails.Rows[i]["Narration"]}", myFont,
                                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight),
                                strFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(
                                $"   Narr : {DTVouDetails.Rows[i]["Narration"].ToString().Substring(0, 70)}", myFont,
                                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight),
                                strFormat);
                            iRow++;
                            iTopMargin += iCellHeight;
                            if (RemarksTotLen > 140)
                            {
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                                iRow++;
                                iTopMargin += iCellHeight;
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(141, RemarksTotLen - 141)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(
                                    $"  {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                            }
                        }
                    }

                    iRow++;
                    iTopMargin += iCellHeight;
                }

                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.HasMorePages = false;
            }

            if (LCnt < N_Line)
                for (var ln = LCnt; ln <= N_Line; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (Module == "PV")
            {
                e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                    strFormat);
            }
            else if (Module == "RV")
            {
                e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
            }
            else //if (Obj.Module == "CV" || Obj.Module == "RPV")
            {
                e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                    strFormat);
                e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;

            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            if (Module == "RV")
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotCrAmt.ToString())} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            else if (Module == "PV")
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString())} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion((TotDrAmt - TotCrAmt).ToString())} ",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            //e.Graphics.DrawString("On Account Of : " + DTVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            myFont = new Font("Arial", 9, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length <= 120)
            {
                e.Graphics.DrawString($"On Account Of : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString(
                    $"On Account Of : {DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120)} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                if (RemarksTotLen > 220)
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
            }

            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;

            if (Module == "RV")
            {
                e.Graphics.DrawString(
                    "          ---------------------                        ------------------------                        ------------------------                              ------------------------",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString(
                    "           Prepared By             " + "                     Paid By                  " +
                    "                    Checked By 	                               " + "      Approved By ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else if (Module == "PV")
            {
                e.Graphics.DrawString(
                    "          ---------------------                        ------------------------                        ------------------------                              ------------------------",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString(
                    "           Prepared By             " + "                  Received By               " +
                    "                    Checked By 	                               " + "      Approved By ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString(
                    "          ---------------------                                                           -----------------                                                   ---------------",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString(
                    "           Prepared By          " +
                    "                                                   Checked By 	                                                " +
                    " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintReceiptDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 30; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;
            long LCnt;
            LCnt = 0;
            iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
            if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null && ObjGlobal.SysFontSize != 0 &&
                ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize), FontStyle.Regular);
            else
                myFont = new Font("Arial", 9, FontStyle.Regular);

            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    $"SELECT Voucher_No,Voucher_Date,Voucher_BsDate,L.Ledger_Code,L.Ledger_Name,Chq_No,Chq_Date,Remarks,Ref_Voucher_No,Ref_Voucher_Date FROM AMS.Voucher_Main as VM Inner Join AMS.Ledger as L on L.Ledger_Code=VM.Ledger_Code Where VM.Module='{Module}'";
                Query =
                    $"{Query} and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "SELECT VM.Voucher_No,L.Ledger_Code,L.Ledger_Name,SL.SubLed_Id,SL.Name as SL_Name,Dr_Amt,Cr_Amt,Narration ";
                Query =
                    $"{Query} FROM AMS.Voucher_Details as VD Inner Join AMS.Voucher_Main as VM On VM.Voucher_No=VD.Voucher_No ";
                Query =
                    $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=VD.Ledger_Code Left Outer Join AMS.Sub_Ledger as SL On SL.SubLed_Id=VD.SubLedger_Id ";
                Query =
                    $"{Query} Where VM.Module= '{Module}' and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                TotCrAmt = 0;
                //Set the cell height
                iCellHeight = 20;
                //Check whether the current page settings allo more rows to print
                if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                {
                    bNewPage = true;
                    bFirstPage = false;
                    bMorePagesToPrint = true;
                }
                else
                {
                    if (bNewPage)
                    {
                        // ---------Start Report Header ----------------
                        var DTCD = new DataTable();
                        DTCD = GetConnection.SelectDataTableQuery("SELECT Address,Phone,Pan_Number  FROM AMS.Company");

                        var LeftMargin = 0;
                        LeftMargin = iLeftMargin + 650;
                        if (_printDocument.DefaultPageSettings.Landscape) LeftMargin = LeftMargin + 200;

                        myFont = new Font("Arial", 12, FontStyle.Bold);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 5;

                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString($"Phone No. : {DTCD.Rows[0]["Phone"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Phone No. : ", myFont).Height + 15;

                        myFont = new Font("Arial", 12, FontStyle.Bold);
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString("RECEIPT", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Phone No. : ", myFont).Height + 10;

                        strFormat.Alignment = StringAlignment.Near;
                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        e.Graphics.DrawString("Receipt No          : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        e.Graphics.DrawString(DTVouMain.Rows[0]["Voucher_No"].ToString(), myFont, Brushes.Black,
                            new RectangleF(150, iTopMargin, 800, iCellHeight), strFormat);

                        strFormat.Alignment = StringAlignment.Near;
                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        //if (ObjGlobal._Date_Type == "AD")
                        //    e.Graphics.DrawString("Date(AD)   : " + Convert.ToDateTime(DTVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString() + "", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
                        //else
                        e.Graphics.DrawString(
                            $"Date(BS[AD])   : {DTVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Bill Of                  : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        e.Graphics.DrawString(DTVouDetails.Rows[0]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF(150, iTopMargin, 800, iCellHeight), strFormat);

                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString($"[{DTVouMain.Rows[0]["Voucher_Date"]}]", myFont, Brushes.Black,
                            new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics
                            .MeasureString(DTVouDetails.Rows[0]["Ledger_Name"].ToString(), myFont).Height + 5;

                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Invoice No           : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        //if (DTVouMain.Rows[0]["Ref_Voucher_No"] != null && DTVouMain.Rows[0]["Ref_Voucher_No"].ToString() != "")
                        //{
                        //    e.Graphics.DrawString(DTVouMain.Rows[0]["Ref_Voucher_No"].ToString(), myFont, Brushes.Black, new RectangleF(120, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
                        //}
                        iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

                        if (DTVouDetails.Rows[0]["Cr_Amt"].ToString() != null)
                        {
                            TotCrAmt = TotCrAmt + Convert.ToDouble(DTVouDetails.Rows[0]["Cr_Amt"]);
                            strFormat.Alignment = StringAlignment.Near;
                            //e.Graphics.DrawString("Invoice Amount : " + Convert.ToDecimal(DTVouDetails.Rows[0]["Cr_Amt"].ToString()).ToString(ObjGlobal._Amount_Format) + "", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
                            e.Graphics.DrawString("Invoice Amount   : ", myFont, Brushes.Black,
                                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                            iTopMargin += (int)e.Graphics
                                .MeasureString(DTVouDetails.Rows[0]["Cr_Amt"].ToString(), myFont).Height + 5;
                        }

                        e.Graphics.DrawString("Payment Mode    : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        if (DTVouMain.Rows[0]["Chq_No"] == null &&
                            DTVouMain.Rows[0]["Chq_No"].ToString() == string.Empty)
                            e.Graphics.DrawString("Cash", myFont, Brushes.Black,
                                new RectangleF(150, iTopMargin, 800, iCellHeight), strFormat);
                        if (DTVouMain.Rows[0]["Chq_No"] != null &&
                            DTVouMain.Rows[0]["Chq_No"].ToString() != string.Empty)
                        {
                            e.Graphics.DrawString("Bank", myFont, Brushes.Black,
                                new RectangleF(150, iTopMargin, 800, iCellHeight), strFormat);
                            iTopMargin += (int)e.Graphics.MeasureString("Payment Mode : ", myFont).Height + 5;

                            e.Graphics.DrawString("Cheque No          : ", myFont, Brushes.Black,
                                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                            e.Graphics.DrawString(DTVouMain.Rows[0]["Chq_No"].ToString(), myFont, Brushes.Black,
                                new RectangleF(150, iTopMargin, 800, iCellHeight), strFormat);
                        }

                        iTopMargin += (int)e.Graphics.MeasureString("bank : ", myFont).Height + 5;

                        myFont = new Font("Arial", 10, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Name Of Bank    : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                        e.Graphics.DrawString(DTVouMain.Rows[0]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF(150, iTopMargin, 800, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("bank : ", myFont).Height + 5;

                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Received Amt     : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                        e.Graphics.DrawString(
                            Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                            Brushes.Black, new RectangleF(150, iTopMargin, 850, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Received Amount : ", myFont).Height + 5;

                        e.Graphics.DrawString("In Words             : ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                        e.Graphics.DrawString(ClsMoneyConversion.MoneyConversion(TotCrAmt.ToString()), myFont,
                            Brushes.Black, new RectangleF(150, iTopMargin, 850, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("Received Amount (In Words) :  ", myFont).Height +
                                      5;

                        //strFormat.Alignment = StringAlignment.Near;
                        //e.Graphics.DrawString("On Account Of    : ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
                        //e.Graphics.DrawString(DTVouMain.Rows[0]["Remarks"].ToString() , myFont, Brushes.Black, new RectangleF(150, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 30;

                        //e.Graphics.DrawString("      ------------------------                                                                  ------------------------                              ----------------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
                        e.Graphics.DrawString(
                            "                                                                                                                                                      ----------------------------------",
                            myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight),
                            strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height +
                                      1;
                        //e.Graphics.DrawString("            Paid By              " + "                            " + "                                         Received By 	                               " + "    Authorised Signature ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
                        e.Graphics.DrawString(
                            "                                 " + "                            " +
                            "                                                      	                                     " +
                            "    Authorised Signature ", myFont, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                        iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;

                        //---------End Report Header ----------------
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    //Draw Columns Contents
                }

                iRow++;
                iTopMargin += iCellHeight;

                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.HasMorePages = false;
            }
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintDNCNHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (_printDocument.DefaultPageSettings.Landscape) LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "DN")
                e.Graphics.DrawString("Debit Note Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else if (Module == "CN")
                e.Graphics.DrawString("Credit Note Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Credit Note Voucher", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Party Name : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Voucher No : {DTVouMain.Rows[0]["Voucher_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Voucher No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["SL_Name"].ToString() != string.Empty && DTVouMain.Rows[0]["SL_Name"] != null)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString($"SL Name : {DTVouMain.Rows[0]["SL_Name"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            }

            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            Query =
                $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{DTVouMain.Rows[0]["Voucher_No"]}' and SubLedger_Id is not null";
            DTTemp.Reset();
            DTTemp = GetConnection.SelectDataTableQuery(Query);
            if (DTTemp.Rows.Count > 0)
                e.Graphics.DrawString(
                    "S.N.     Ledger                                              Sub Ledger                                                                                         Amount",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    "S.N.     Particulars                                                                                                                                                    Amount",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            iTopMargin += 1;

            //---------End Report Header ----------------
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintDNCNDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            long LCnt;
            LCnt = 0;

            iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;

                Query =
                    "SELECT Voucher_No,Voucher_Date,Voucher_BsDate,L.Ledger_Code,L.Ledger_Name,SL.SubLed_Id,SL.Name as SL_Name,A.Agent_Id,A.Name as Ag_Name,Chq_No,Chq_Date,Remarks FROM AMS.Voucher_Main as VM Inner Join AMS.Ledger as L on L.Ledger_Code=VM.Ledger_Code ";
                Query =
                    $"{Query} Left Outer Join AMS.Sub_Ledger as SL On SL.SubLed_Id=VM.SubLed_Id Left Outer Join AMS.Agent as A On A.Agent_Id=VM.Agent_Id";
                Query =
                    $"{Query} Where VM.Module='{Module}' and VM.Voucher_No ='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);

                Query =
                    $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{VouNo}' and SubLedger_Id is not null";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "SELECT VM.Voucher_No,L.Ledger_Code,L.Ledger_Name,SL.SubLed_Id,SL.Name as SL_Name,Dr_Amt,Cr_Amt,Narration FROM AMS.Voucher_Details as VD";
                Query = $"{Query} Inner Join AMS.Voucher_Main as VM On VM.Voucher_No=VD.Voucher_No ";
                Query =
                    $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=VD.Ledger_Code Left Outer Join AMS.Sub_Ledger as SL On SL.SubLed_Id=VD.SubLedger_Id ";
                Query =
                    $"{Query} Where VM.Module='{Module}' and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                Query = $"{Query} Order by Dr_Amt desc,Cr_Amt";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                ColWidths.Add(50);
                if (DTTemp.Rows.Count > 0)
                {
                    ColWidths.Add(280);
                    ColWidths.Add(280);
                }
                else
                {
                    ColWidths.Add(560);
                    ColWidths.Add(0);
                }

                ColWidths.Add(150);

                for (var i = 0; i < ColWidths.Count; i++)
                {
                    iHeaderHeight = (int)e.Graphics
                        .MeasureString("SNo.", myFont, Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++) //foreach(DataRow ro in DTVouDetails.Rows)
                {
                    //Set the cell height
                    iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintDNCNHeader(e);
                        bNewPage = false;
                    }

                    DRo = DRo + 1;
                    LCnt = LCnt + 1;
                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DRo}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Ledger_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["SL_Name"].ToString() != null &&
                        DTVouDetails.Rows[i]["SL_Name"].ToString() != string.Empty)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["SL_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;
                    if (Module == "CN")
                    {
                        if (DTVouDetails.Rows[i]["Dr_Amt"].ToString() != null)
                        {
                            TotDrAmt = TotDrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Dr_Amt"]);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(
                                Convert.ToDecimal(DTVouDetails.Rows[i]["Dr_Amt"].ToString())
                                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        }
                    }
                    else
                    {
                        if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null)
                        {
                            TotCrAmt = TotCrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"]);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(
                                Convert.ToDecimal(DTVouDetails.Rows[i]["Cr_Amt"].ToString())
                                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        }
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Narration"].ToString() != string.Empty &&
                        DTVouDetails.Rows[i]["Narration"].ToString() != null)
                    {
                        iRow++;
                        iTopMargin += iCellHeight;
                        strFormat.Alignment = StringAlignment.Near;
                        RemarksTotLen = DTVouDetails.Rows[i]["Narration"].ToString().Length;
                        myFont = new Font("Arial", 8, FontStyle.Regular);
                        if (DTVouDetails.Rows[0]["Narration"].ToString().Length <= 70)
                        {
                            e.Graphics.DrawString($"  Narr : {DTVouDetails.Rows[i]["Narration"]}", myFont,
                                Brushes.Black,
                                new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1],
                                    iCellHeight), strFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(
                                $"   Narr : {DTVouDetails.Rows[i]["Narration"].ToString().Substring(0, 70)}", myFont,
                                Brushes.Black,
                                new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1],
                                    iCellHeight), strFormat);
                            iTopMargin += (int)e.Graphics.MeasureString("Narr", myFont).Height + 5;
                            if (RemarksTotLen > 140)
                            {
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1],
                                        iCellHeight), strFormat);
                                iTopMargin += (int)e.Graphics.MeasureString("Narr", myFont).Height + 5;
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(141, RemarksTotLen - 141)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1],
                                        iCellHeight), strFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(
                                    $"  {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1],
                                        iCellHeight), strFormat);
                            }
                        }
                    }

                    iRow++;
                    iTopMargin += iCellHeight;
                }

                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.HasMorePages = false;
            }

            if (LCnt < N_Line)
                for (var ln = LCnt; ln <= N_Line; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 500, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (Module == "CN")
                e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                    strFormat);
            else
                e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                    strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            if (Module == "DN")
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotCrAmt.ToString())} ", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString())} ", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            myFont = new Font("Arial", 9, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length <= 120)
            {
                e.Graphics.DrawString($"On Account Of : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString(
                    $"On Account Of : {DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120)} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                if (RemarksTotLen > 220)
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
            }

            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;
            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ---------------------------                                    -------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                "           Prepared By          " +
                "                                                   Checked By 	                                                " +
                " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintCBHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20;
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (_printDocument.DefaultPageSettings.Landscape) LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "RV")
            {
                if (CashVoucher == false)
                    e.Graphics.DrawString("Bank Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Cash Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "PV")
            {
                if (CashVoucher == false)
                    e.Graphics.DrawString("Bank Payment Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Cash Payment Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "CB")
            {
                e.Graphics.DrawString("Cash/Bank Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else if (Module == "RPV")
            {
                e.Graphics.DrawString("Receipt/Payment Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Receipt/Payment Voucher", myFont).Height + 10;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Cash/Bank : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Voucher No : {DTVouMain.Rows[0]["Voucher_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Voucher No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["Chq_No"] != null && DTVouMain.Rows[0]["Chq_No"].ToString() != string.Empty)
            {
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"Cheque No  : {DTVouMain.Rows[0]["Chq_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                if (ObjGlobal.SysDateType == "AD")
                    e.Graphics.DrawString(
                        $"Cheque Date(AD) : {Convert.ToDateTime(DTVouMain.Rows[0]["Chq_Date"].ToString()).ToShortDateString()}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(300, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString(
                        $"Cheque Date(BS) : {ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["Chq_Date"]).ToShortDateString())}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(300, iTopMargin, 800, iCellHeight), strFormat);
            }

            strFormat.Alignment = StringAlignment.Near;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            Query =
                $"SELECT Distinct [SUBLEDGER DESC] FROM AMS.[VIEW_JOURNALVOUCHER] Where [VOUCHER NO]='{DTVouMain.Rows[0]["Voucher_No"]}'";
            DTTemp.Reset();
            DTTemp = GetConnection.SelectDataTableQuery(Query);
            if (DTTemp.Rows.Count > 0)
            {
                //e.Graphics.DrawString("S.N.     Ledger                                              Sub Ledger                                                                                         Amount", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
                if (Module == "RV")
                    e.Graphics.DrawString(
                        "S.N.     Ledger                                              Sub Ledger                                                                              Receipt Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module == "PV")
                    e.Graphics.DrawString(
                        "S.N.     Ledger                                              Sub Ledger                                                                              Payment Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module is "CB" or "RPV")
                    e.Graphics.DrawString(
                        "S.N.     Ledger                                              Sub Ledger                                                Payment Amt            Receipt Amt",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }
            else
            {
                if (Module == "RV")
                    e.Graphics.DrawString(
                        "S.N.     Particulars                                                                                                                                        Receipt Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module == "PV")
                    e.Graphics.DrawString(
                        "S.N.     Particulars                                                                                                                                        Payment Amount",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
                else if (Module is "CB" or "RPV")
                    e.Graphics.DrawString(
                        "S.N.     Particulars                                                                                                               Payment Amt            Receipt Amt",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            iTopMargin += 1;
            myFont = new Font("Arial", 9, FontStyle.Bold);

            //---------End Report Header ----------------
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintCBDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            long LCnt;
            LCnt = 0;

            iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);

            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    $"SELECT Voucher_No,Voucher_Date,Voucher_BsDate,L.Ledger_Code,L.Ledger_Name,Chq_No,Chq_Date,Remarks FROM AMS.Voucher_Main as VM Inner Join AMS.Ledger as L on L.Ledger_Code=VM.Ledger_Code Where VM.Module='{Module}'";
                Query =
                    $"{Query} and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                CashVoucher = false;
                Query =
                    $"Select distinct Ledger_Id as Id,Ledger_Code as Code,Ledger_Name as Name from AMS.Ledger as L  where (L.Ledger_Code in ('000023')) and L.Ledger_Code = '{DTVouMain.Rows[0]["Ledger_Code"]}' and Is_Ledger=1 and Status=1 order by L.Ledger_Code ";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);
                if (DTTemp.Rows.Count > 0) CashVoucher = true;

                Query =
                    $"SELECT Distinct SubLedger_Id FROM AMS.Voucher_Details Where Voucher_No='{VouNo}' and SubLedger_Id is not null";
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "SELECT VM.Voucher_No,L.Ledger_Code,L.Ledger_Name,SL.SubLed_Id,SL.Name as SL_Name,Dr_Amt,Cr_Amt,Narration ";
                Query =
                    $"{Query} FROM AMS.Voucher_Details as VD Inner Join AMS.Voucher_Main as VM On VM.Voucher_No=VD.Voucher_No ";
                Query =
                    $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=VD.Ledger_Code Left Outer Join AMS.Sub_Ledger as SL On SL.SubLed_Id=VD.SubLedger_Id ";
                Query =
                    $"{Query} Where VM.Module= '{Module}' and VM.Voucher_No='{VouNo}' and VM.Branch_Id={ObjGlobal.SysBranchId} and VM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                ColWidths.Add(50);
                if (DTTemp.Rows.Count > 0)
                {
                    ColWidths.Add(230);
                    ColWidths.Add(230);
                }
                else
                {
                    ColWidths.Add(460);
                    ColWidths.Add(0);
                }

                if (Module is "CV" or "RPV")
                {
                    ColWidths.Add(125);
                    ColWidths.Add(125);
                }
                else if (Module == "PV")
                {
                    ColWidths.Add(250);
                    ColWidths.Add(0);
                }
                else if (Module == "RV")
                {
                    ColWidths.Add(0);
                    ColWidths.Add(250);
                }

                for (var i = 0; i < ColWidths.Count; i++)
                {
                    iHeaderHeight = (int)e.Graphics
                        .MeasureString("SNo.", myFont, Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++) //foreach(DataRow ro in DTVouDetails.Rows)
                {
                    //Set the cell height
                    iCellHeight = 20;
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
                        PrintCBHeader(e);
                        bNewPage = false;
                    }

                    iCount = 0;
                    DRo = DRo + 1;
                    LCnt = LCnt + 1;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DRo}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Ledger_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["SL_Name"].ToString() != null &&
                        DTVouDetails.Rows[i]["SL_Name"].ToString() != string.Empty)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["SL_Name"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (Module is "PV" or "CV" or "RPV")
                        if (DTVouDetails.Rows[i]["Dr_Amt"].ToString() != null)
                        {
                            TotDrAmt = TotDrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Dr_Amt"]);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(
                                Convert.ToDecimal(DTVouDetails.Rows[i]["Dr_Amt"].ToString())
                                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        }

                    iCount++;
                    if (Module is "RV" or "CV" or "RPV")
                        if (DTVouDetails.Rows[i]["Cr_Amt"].ToString() != null)
                        {
                            TotCrAmt = TotCrAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Cr_Amt"]);
                            strFormat.Alignment = StringAlignment.Far;
                            e.Graphics.DrawString(
                                Convert.ToDecimal(DTVouDetails.Rows[i]["Cr_Amt"].ToString())
                                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    iCellHeight), strFormat);
                        }

                    iCount++;
                    if (DTVouDetails.Rows[i]["Narration"].ToString() != string.Empty &&
                        DTVouDetails.Rows[i]["Narration"].ToString() != null)
                    {
                        iRow++;
                        iTopMargin += iCellHeight;
                        strFormat.Alignment = StringAlignment.Near;
                        RemarksTotLen = DTVouDetails.Rows[i]["Narration"].ToString().Length;
                        myFont = new Font("Arial", 8, FontStyle.Regular);
                        if (DTVouDetails.Rows[0]["Narration"].ToString().Length <= 70)
                        {
                            e.Graphics.DrawString($"  Narr : {DTVouDetails.Rows[i]["Narration"]}", myFont,
                                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight),
                                strFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(
                                $"   Narr : {DTVouDetails.Rows[i]["Narration"].ToString().Substring(0, 70)}", myFont,
                                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight),
                                strFormat);
                            iRow++;
                            iTopMargin += iCellHeight;
                            if (RemarksTotLen > 140)
                            {
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                                iRow++;
                                iTopMargin += iCellHeight;
                                e.Graphics.DrawString(
                                    $" {DTVouDetails.Rows[i]["Narration"].ToString().Substring(141, RemarksTotLen - 141)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                            }
                            else
                            {
                                e.Graphics.DrawString(
                                    $"  {DTVouDetails.Rows[i]["Narration"].ToString().Substring(71, RemarksTotLen - 71)}",
                                    myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460, iCellHeight), strFormat);
                            }
                        }
                    }

                    iRow++;
                    iTopMargin += iCellHeight;
                }

                if (bMorePagesToPrint)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.HasMorePages = false;
            }

            if (LCnt < N_Line)
                for (var ln = LCnt; ln <= N_Line; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 20;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (Module == "PV")
            {
                e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                    strFormat);
            }
            else if (Module == "RV")
            {
                e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
            }
            else //if (Obj.Module == "CV" || Obj.Module == "RPV")
            {
                e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                    strFormat);
                e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;

            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            if (Module == "RV")
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotCrAmt.ToString())} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            else if (Module == "PV")
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString())} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    $"Amount In Words : {ClsMoneyConversion.MoneyConversion((TotDrAmt - TotCrAmt).ToString())} ",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            //e.Graphics.DrawString("On Account Of : " + DTVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            myFont = new Font("Arial", 9, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length <= 120)
            {
                e.Graphics.DrawString($"On Account Of : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString(
                    $"On Account Of : {DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120)} ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                if (RemarksTotLen > 220)
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 5;
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        $"                     {DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121)} ",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                }
            }

            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;

            if (Module == "RV")
            {
                e.Graphics.DrawString(
                    "          ---------------------                        ------------------------                        ------------------------                              ------------------------",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString(
                    "           Prepared By             " + "                     Paid By                  " +
                    "                    Checked By 	                               " + "      Approved By ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else if (Module == "PV")
            {
                e.Graphics.DrawString(
                    "          ---------------------                        ------------------------                        ------------------------                              ------------------------",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString(
                    "           Prepared By             " + "                  Received By               " +
                    "                    Checked By 	                               " + "      Approved By ", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString(
                    "          ---------------------                                                           -----------------                                                   ---------------",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
                e.Graphics.DrawString(
                    "           Prepared By          " +
                    "                                                   Checked By 	                                                " +
                    " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion --------------------------------------------- Receipt/Payment/Contra Voucher Default Design ---------------------------------------------

    #endregion --------------------------------------------- Receipt/Payment/Contra Voucher Design ---------------------------------------------
}