using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;

namespace PrintControl.Print.DirectPrint;

public class DocPrinting_SalesInvoice
{
    public string PrintDoc(bool print)
    {
        try
        {
            PD.BeginPrint += printDocument1_BeginPrint;
            PD.PrintPage += printDocument1_PrintPage;
            PPD.Click += printPreviewDialog1_Click;
            Printed = false;
            if (Convert.ToInt16(NoOf_Copy) > 0)
            {
                DTCD = GetConnection.SelectDataTableQuery(
                    "SELECT Company_Name,Address,Country,City,State ,PhoneNo,Pan_No,Email,Website  FROM AMS.CompanyInfo");
                for (var Noc = 1; Noc <= Convert.ToInt16(NoOf_Copy); Noc++)
                {
                    //PageNo = 0;
                    if (Noc == 1)
                        Fristprintcopy = true;
                    else
                        Fristprintcopy = false;

                    PD.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                    PD.PrinterSettings.PrinterName = Printer_Name;
                    if (DocDesign_Name == "A4 Full" || DocDesign_Name == "Receipt Voucher" ||
                        DocDesign_Name == "Payment Voucher")
                    {
                        N_Line = 30;
                        PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                    }
                    else if (DocDesign_Name == "Sales A5" || DocDesign_Name == "Order A5" ||
                             DocDesign_Name == "Sales A5 PAN")
                    {
                        N_Line = 30;
                        PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA5", 550, 850);
                    }
                    else if (DocDesign_Name == "TaxInvoice5inch" || DocDesign_Name == "SalesReturn5inch")
                    {
                        N_Line = 20;
                        PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA5", 500, 850);
                    }
                    else if (DocDesign_Name == "A4 Half" || DocDesign_Name == "Sales A4 Half" ||
                             DocDesign_Name == "Order A4 Half" || DocDesign_Name == "A4 Half PAN")
                    {
                        N_Line = 5;
                        PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                    }
                    else if (DocDesign_Name == "LETTER FULL")
                    {
                        PD.DefaultPageSettings.PaperSize.PaperName = PaperKind.Letter.ToString();
                    }
                    else if (DocDesign_Name == "FNB Invoice 3inch" || DocDesign_Name == "FNB Order 3inch" ||
                             DocDesign_Name == "FNB Order KOT/BOT 3inch" ||
                             DocDesign_Name == "FNB Order KOT/BOT 3inch Preview" || DocDesign_Name == "KOT/BOT")
                    {
                        PD.DefaultPageSettings.PaperSize = new PaperSize("FNB", 320, 1000);
                    }
                    else if (DocDesign_Name == "AbbreviatedTaxInvoice3inch" ||
                             DocDesign_Name == "AbbreviatedTaxInvoice3inchWithPreview" ||
                             DocDesign_Name == "TaxInvoice3inch" || DocDesign_Name == "Invoice3inch")
                    {
                        PD.DefaultPageSettings.PaperSize = new PaperSize("FNB", 280, 1000);
                    }
                    else if (DocDesign_Name == "TaxInvoice5inch")
                    {
                        PD.DefaultPageSettings.PaperSize = new PaperSize("Invoice", 480, 1000);
                    }
                    else if (DocDesign_Name == "Order 3inch")
                    {
                        PD.DefaultPageSettings.PaperSize = new PaperSize("FNB", 320, 1000);
                    }

                    //printDocument1.DefaultPageSettings.Landscape = PRT.Landscape;
                    Query = string.Empty;
                    VouNo = string.Empty;
                    if (Module == "SQ")
                    {
                        Query = "SELECT SQ_Invoice Voucher_No FROM AMS.SQ_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SQ_Invoice>='{FromDocNo}' and SQ_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }
                    }
                    else if (Module == "SO" || Module == "SOF")
                    {
                        Query = "SELECT SO_Invoice Voucher_No FROM AMS.SO_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SO_Invoice>='{FromDocNo}' and SO_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }
                    }
                    else if (Module == "SOC")
                    {
                        Query = "SELECT SOC_Invoice Voucher_No FROM AMS.SOC_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SOC_Invoice>='{FromDocNo}' and SOC_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }
                    }
                    else if (Module == "SC")
                    {
                        Query = "SELECT SC_Invoice Voucher_No FROM AMS.SC_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SC_Invoice>='{FromDocNo}' and SC_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }
                    }
                    else if (Module == "SB" || Module == "SBF" || Module == "POS" || Module == "ATI")
                    {
                        if (!string.IsNullOrEmpty(InvoiceType))
                        {
                            Query =
                                $"SELECT SB_Invoice Voucher_No FROM AMS.SB_Master Where Invoice_Mode='{InvoiceType}' ";
                            if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                            {
                                Query = $"{Query} and SB_Invoice>='{FromDocNo}' and SB_Invoice<='{ToDocNo}' ";
                            }
                            else if (!string.IsNullOrEmpty(FromDate))
                            {
                                if (ObjGlobal.SysDateType == "D")
                                    Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}' ";
                                else
                                    Query =
                                        $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}' ";
                            }
                        }
                        else
                        {
                            Query = "SELECT SB_Invoice Voucher_No FROM AMS.SB_Master Where 1=1 ";
                            if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                            {
                                Query = $"{Query} and SB_Invoice>='{FromDocNo}' and SB_Invoice<='{ToDocNo}'";
                            }
                            else if (!string.IsNullOrEmpty(FromDate))
                            {
                                if (ObjGlobal.SysDateType == "D")
                                    Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                                else
                                    Query =
                                        $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                            }
                        }
                    }
                    else if (Module == "SAB")
                    {
                        Query = "SELECT SAB_Invoice Voucher_No FROM AMS.SAB_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SAB_Invoice>='{FromDocNo}' and SAB_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }
                    }
                    else if (Module == "SR")
                    {
                        Query = "SELECT SR_Invoice Voucher_No FROM AMS.SR_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SR_Invoice>='{FromDocNo}' and SR_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }
                    }
                    else if (Module == "SEB")
                    {
                        Query = "SELECT SEB_Invoice Voucher_No FROM AMS.SEB_Master Where 1=1 ";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SEB_Invoice>='{FromDocNo}' and SEB_Invoice<='{ToDocNo}'";
                        }
                        else if (!string.IsNullOrEmpty(FromDate))
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
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
                            i = 0;
                            if (print)
                            {
                                PD.PrintController =
                                    printController; //User Printing page dialog press enter enter(2 times) button then cancel print voucher
                                PD.Print();
                            }
                            else
                            {
                                PPD.Document = PD;
                                PPD.PrintPreviewControl.Zoom = 1;
                                ((Form)PPD).WindowState = FormWindowState.Maximized;
                                PPD.ShowDialog();
                            }
                        }
                    }
                }

                if (print)
                    if (Module == "SB" || Module == "SBF" || Module == "POS" || Module == "ATI")
                    {
                        Query =
                            $"Update AMS.SB_Master Set Is_Printed=1,No_Print= isnull(No_Print,0) + 1,Printed_By='{ObjGlobal.LogInUser}' , Printed_Date='{DateTime.Now:MM/dd/yyyy HH:mm:ss}'  Where ('0'='{ObjGlobal.SysBranchId}' or CBranch_Id={ObjGlobal.SysBranchId})";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and SB_Invoice>='{FromDocNo}' and SB_Invoice<='{ToDocNo}'";
                        }
                        else
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                        }

                        var a = ExecuteCommand.ExecuteNonQuery(Query) != 0;
                    }
            }

            if (print) Msg = "Print Completed!";

            return Msg;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
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
            DRo = 0;
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
        if (Module == "SQ")
        {
            PrintSalesQuotationDetails(e);
        }
        else if (Module == "SO" || Module == "SOF")
        {
            if (DocDesign_Name == "FNB Order 3inch")
                PrintFNBSalesOrderDetails3InchRollPaper(e);
            if (DocDesign_Name == "FNB Order 3inch")
                PrintFNBSalesOrderDetails3InchRollPaper(e);
            if (DocDesign_Name == "KOT/BOT")
                PrintKOTBOT3Inch(e);
            else if (DocDesign_Name == "Order KOT/BOT 3inch Preview")
                PrintFNBSalesOrderKOTBOTHeader3InchRollPaper(e);
            else if (DocDesign_Name == "Order 3inch")
                PrintOrderHeader3InchRollPaper(e);
            else if (DocDesign_Name == "PreInvoice")
                //PrintPreInvoiceDetails3InchRollPaper(e);
                PrintPreInvoiceWithoutUnitDetails3InchRollPaper(e);
            else if (DocDesign_Name == "Order A4 Half")
                PrintSalesOrderHalfDetails(e);
            else if (DocDesign_Name == "Order A4 Half PAN")
                PrintSalesOrderHalfDetails(e);
            else if (DocDesign_Name == "Order A5")
                PrintSalesOrderA5Details(e);
            else
                PrintSalesOrderDetails(e);
        }
        else if (Module == "SC")
        {
            PrintSalesChallanDetails(e);
        }
        else if (Module == "SB" || Module == "SBF" || Module == "POS" || Module == "ATI")
        {
            if (DocDesign_Name == "Counter 3 Inch" && !string.IsNullOrEmpty(InvoiceType) && InvoiceType == "POS")
                PrintCounterSalesBillDetails(e);
            else if (DocDesign_Name == "FNB Invoice 3inch")
                PrintFNBSalesBillDetails3InchRollPaper(e);
            else if (DocDesign_Name == "TaxInvoice3inch")
                PrintFNBSalesBillDetails3InchRollPaper(e);
            else if (DocDesign_Name == "Invoice3inch" || DocDesign_Name == "EstimateInvoice3inch")
                PrintSalesBillDetails3InchRollPaper(e);
            else if (DocDesign_Name == "AbbreviatedTaxInvoice3inch" ||
                     DocDesign_Name == "AbbreviatedTaxInvoice3inchWithPreview")
                PrintAbbreviatedSalesBillDetails3InchRollPaper(e);
            else if (DocDesign_Name == "Sales Invoice FNB")
                PrintFNBSalesBillDetails(e);
            else if (DocDesign_Name == "Sales Invoice")
                PrintSalesInvoiceDetails(e);
            else if (DocDesign_Name == "Sales A4 Half")
                PrintSalesBillHalfDetails(e);
            else if (DocDesign_Name == "Sales A5")
                PrintSalesBillA5Details(e);
            else if (DocDesign_Name == "Sales A5 PAN")
                PrintSalesBillA5Details(e);
            else if (DocDesign_Name == "TaxInvoice5inch")
                PrintSalesTaxInvoiceA5Details(e);
            else if (DocDesign_Name == "A4 Half PAN")
                PrintSalesBillHalfDetails(e);
            else
                PrintSalesBillDetails(e);
        }
        else if (Module == "SR")
        {
            if (DocDesign_Name == "Invoice3inch" || DocDesign_Name == "EstimateInvoice3inch")
                PrintSalesReturnDetails3InchRollPaper(e);
            else if (DocDesign_Name == "SalesReturn5inch")
                PrintSalesReturnInvoiceA5Details(e);
            else
                PrintSalesBillReturnDetails(e);
        }
        else if (Module == "SEB")
        {
            PrintSalesBillExBrReturnDetails(e);
        }
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        PPD.Document = PD;
        PPD.PrintPreviewControl.Zoom = 1;
        ((Form)PPD).WindowState = FormWindowState.Maximized;
        PPD.ShowDialog();
    }

    #region Global

    private DataTable DTCD = new();
    private readonly System.Drawing.Printing.PrintDocument PD = new();
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
    private DataTable DTKOTBOT = new();
    private DataSet ds = new();
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.str' is never used
    private string str;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.str' is never used
    private string Query;
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.FromAdDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.ToADDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.TotDrAmt' is never used
    private double TotDrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.TotDrAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.TotCrAmt' is never used
    private double TotCrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.TotCrAmt' is never used
    private double TotAltQty;
    private double TotQty;
    private double TotPBasicAmt;
    private double TotPTermAmt;
    private double TotBasicAmt;
#pragma warning disable CS0414 // The field 'DocPrinting_SalesInvoice.TotNetAmt' is assigned but its value is never used
    private double TotNetAmt;
#pragma warning restore CS0414 // The field 'DocPrinting_SalesInvoice.TotNetAmt' is assigned but its value is never used
    private double TotGrandAmt;
    private Font myFont = new("Arial", 10);
#pragma warning disable CS0414 // The field 'DocPrinting_SalesInvoice.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_SalesInvoice.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_SalesInvoice.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_SalesInvoice.rowPosition' is assigned but its value is never used
    private int N_Line;
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.N_FootLine' is never used
    private int N_TermDet;
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.RemarksTotLen' is never used
    private int RemarksTotLen;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.RemarksTotLen' is never used
    private StringFormat strFormat;
    private readonly ArrayList ColHeaders = new();
    private readonly ArrayList ColWidths = new();
    private readonly ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new();
    private readonly ArrayList arrColumnWidths = new();
    private int iCellHeight;
    public int PageWidth;
    public int PageHeight;
    public long PageNo;
    private long iRow;
#pragma warning disable CS0414 // The field 'DocPrinting_SalesInvoice.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_SalesInvoice.iCount' is assigned but its value is never used
    private bool bFirstPage;
    private bool bNewPage;
    private int iHeaderHeight;
    private bool bMorePagesToPrint;
    private int iLeftMargin;
    private int iRightMargin;
    private int iTopMargin;
#pragma warning disable CS0414 // The field 'DocPrinting_SalesInvoice.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_SalesInvoice.iTmpWidth' is assigned but its value is never used
    private int i;
    private int DRo;
    private string VouNo = string.Empty;
    private readonly string GrpCode = string.Empty;
#pragma warning disable CS0169 // The field 'DocPrinting_SalesInvoice.CashVoucher' is never used
    private bool CashVoucher;
#pragma warning restore CS0169 // The field 'DocPrinting_SalesInvoice.CashVoucher' is never used
    private string Msg = string.Empty;
    private bool Printed;
    private bool Fristprintcopy;

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

    #endregion Global

    #region ---------------------------------------- Sales Quotation Default Design ----------------------------------------

    private void PrintSalesQuotationHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

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
            e.Graphics.DrawString("SALES QUOTATION", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES QUOTATION", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
            e.Graphics.DrawString($"Quotation No : {DTVouMain.Rows[0]["SQ_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Quotation No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["GlAddress"] != null && DTVouMain.Rows[0]["GlAddress"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Quotation Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Quotation Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["PanNo"] != null && DTVouMain.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"PAN/VAT No     : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }

            if (DTVouMain.Rows[0]["PhoneNo"] != null && DTVouMain.Rows[0]["PhoneNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Phone No       : {DTVouMain.Rows[0]["PhoneNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesQuotationDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SQM.SQ_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, Case When (SQM.Party_Name IS NULL or SQM.Party_Name='') Then GL.GLName Else SQM.Party_Name End GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,Case When (SQM.Vat_No IS NULL or SQM.Vat_No='') Then GL.PanNo Else SQM.Vat_No End PanNo, Remarks,Payment_Mode FROM AMS.SQ_Master as SQM Inner Join AMS.SQ_Details as SQD On SQM.SQ_Invoice=SQD.SQ_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SQM.Customer_Id  Left Outer Join AMS.JuniorAgent as W On W.AgentId=SQM.Agent_Id ";
                Query =
                    $"{Query} Where SQM.SQ_Invoice='{VouNo}' and SQM.CBranch_Id={ObjGlobal.SysBranchId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SQM.SQ_Invoice,SQD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode AltUnitCode,SQD.Alt_Qty,PU.UnitCode , SQD.Qty,SQD.Rate, SQD.B_Amount, SQD.T_Amount,SQD.N_Amount ";
                Query =
                    $"{Query} From AMS.SQ_Details as SQD Inner Join AMS.SQ_Master as SQM On SQM.SQ_Invoice = SQD.SQ_Invoice Inner Join AMS.Product as P On P.PId = SQD.P_Id Left Outer Join AMS.Godown as G On G.GID = SQD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SQD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SQD.Unit_Id ";
                Query = $"{Query} Where SQM.SQ_Invoice='{VouNo}' and SQM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SQ_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SQM.SQ_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SQ_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SQ_Master as SQM On SQM.SQ_Invoice=STD.SQ_VNo  ";
                Query =
                    $"{Query} Where SQM.SQ_Invoice='{VouNo}' and SQM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SQM.SQ_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SQ_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(345);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesQuotationHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'int' is never equal to 'null' of type 'int?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["AltUnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["AltUnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion ---------------------------------------- Sales Quotation Default Design ----------------------------------------

    #region ---------------------------------------- Sales Order Design ----------------------------------------

    #region ---------------------------------------- Sales Order Default Design ----------------------------------------

    private void PrintSalesOrderHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
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
            e.Graphics.DrawString("SALES ORDER", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES ORDER", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Order No : {DTVouMain.Rows[0]["SO_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Order No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["GlAddress"] != null && DTVouMain.Rows[0]["GlAddress"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Order Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Order Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesOrderDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SO_Invoice,Invoice_Date,Invoice_Miti,GL.GlCode,GL.GlName,GlAddress,PhoneNo,LandLineNo,Email,PanNo,Party_Name,Vat_No, Remarks,Is_Printed,No_Print,TM.TableCode FROM AMS.SO_Master as SOM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Left Outer Join AMS.TableMaster as TM  On SOM.TableId=TM.TableId ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,AltU.UnitCode AltUnit,U.UnitCode Unit,Alt_Qty,Qty,Rate,SOD.B_Amount, ";
                Query =
                    $"{Query} SOD.T_Amount,SOD.N_Amount From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId=SOD.P_Id Left Outer Join AMS.Godown as G On G.GId=SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as AltU On AltU.UID=SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as U On U.UID=SOD.Unit_Id";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(35);
                ColWidths.Add(345);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesOrderHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["AltUnit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["AltUnit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Remarks", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintFNBSalesOrderHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;
            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;

            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;

            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("SALES ORDER", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("SALES ORDER", myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["GLName"].ToString().Length > 19)
                e.Graphics.DrawString(
                    $"Name : {DTVouMain.Rows[0]["GLName"].ToString().Substring(0, 20)}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 175, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Name : {DTVouMain.Rows[0]["GLName"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 175, iCellHeight), strFormat);
            if (DTVouMain.Rows[0]["TableCode"] != null && DTVouMain.Rows[0]["TableCode"].ToString() != string.Empty)
            {
                if (InvoiceType == "5")
                    e.Graphics.DrawString($"Room No: {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(170, iTopMargin, PageWidth, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString($"Table No: {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(170, iTopMargin, PageWidth, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 5;

            e.Graphics.DrawString($"Order No : {DTVouMain.Rows[0]["SO_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 175, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD): {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS): {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(170, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
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

    private void PrintFNBSalesOrderDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SO_Invoice,Invoice_Date,Invoice_Miti,GL.GlCode,GL.GlName,GlAddress,PhoneNo,LandLineNo,Email,PanNo,Party_Name,Vat_No, Remarks,Is_Printed,No_Print,TM.TableCode FROM AMS.SO_Master as SOM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Left Outer Join AMS.TableMaster as TM  On SOM.TableId=TM.TableId ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,AltU.UnitCode AltUnit,U.UnitCode Unit,Alt_Qty,Qty,Rate,SOD.B_Amount, ";
                Query =
                    $"{Query} SOD.T_Amount,SOD.N_Amount From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId=SOD.P_Id Left Outer Join AMS.Godown as G On G.GId=SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as AltU On AltU.UID=SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as U On U.UID=SOD.Unit_Id";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("Sn");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(100);
                ColWidths.Add(45);
                ColWidths.Add(50);
                ColWidths.Add(70);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 20;
                var iCount = 0;
                PrintFNBSalesOrderHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, 300, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                    Brushes.Black, new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
                myFont = new Font("Arial", 8, FontStyle.Bold);
                e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 40)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 39)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(39, AmountWords.Length - 39)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 7;
            if (DTVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Remarks", myFont).Height + 20;
            }

            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + ObjGlobal._Company_Name + " ", myFont, Brushes.Black, new RectangleF(10, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 5;
            e.Graphics.DrawString($"User :  {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintKOTBOT3Inch(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;
            var dtGroup = new DataTable();

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SO_Invoice,Invoice_Date,Invoice_Miti,GL.GlCode,GL.GlName,GlAddress,PhoneNo,LandLineNo,Email,PanNo,Party_Name,Vat_No, Remarks,Is_Printed,No_Print,TM.TableCode FROM AMS.SO_Master as SOM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Left Outer Join AMS.TableMaster as TM  On SOM.TableId=TM.TableId ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,AltU.UnitCode AltUnit,U.UnitCode Unit,Alt_Qty,Qty,Rate,SOD.B_Amount, ";
                Query =
                    $"{Query} SOD.T_Amount,SOD.N_Amount From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId=SOD.P_Id Left Outer Join AMS.Godown as G On G.GId=SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as AltU On AltU.UID=SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as U On U.UID=SOD.Unit_Id";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("Sn");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                //ColHeaders.Add("Rate");
                //ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                //ColFormat.Add("Right");
                //ColFormat.Add("Right");

                ColWidths.Add(30);
                ColWidths.Add(200);
                ColWidths.Add(50);
                //ColWidths.Add(50);
                //ColWidths.Add(70);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height

                iCellHeight = 20;
                var iCount = 0;
                PrintFNBSalesOrderHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    //if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    //{
                    //    strFormat.Alignment = StringAlignment.Far;
                    //    e.Graphics.DrawString(Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black,
                    //    new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin, (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                    //}
                    iCount++;

                    //if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    //{
                    //    TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                    //    strFormat.Alignment = StringAlignment.Far;
                    //    e.Graphics.DrawString(Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black,
                    //    new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin, (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                    //}
                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //myFont = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
            //e.Graphics.DrawString("Total : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)300, (float)iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Total", myFont).Height) + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            //if (DTTermDetails.Rows.Count > 0)
            //{
            //    foreach (DataRow DTRo in DTTermDetails.Rows)
            //    {
            //        strFormat.Alignment = StringAlignment.Near;
            //        e.Graphics.DrawString(DTRo["ST_Name"].ToString() + " : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //        strFormat.Alignment = StringAlignment.Far;
            //        e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[3], (float)iTopMargin, (int)arrColumnWidths[3], (float)iCellHeight), strFormat);
            //        e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);

            //        iTopMargin += (int)(e.Graphics.MeasureString("" + DTRo["ST_Name"].ToString() + "", myFont).Height) + 5;
            //        TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
            //    }

            //    strFormat.Alignment = StringAlignment.Near;
            //    e.Graphics.DrawString("-----------------------------------------------------------", myFont, Brushes.Black, new RectangleF(70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 5;
            //    myFont = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
            //    e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    strFormat.Alignment = StringAlignment.Far;
            //    e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("Total", myFont).Height) + 5;
            //}
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            //string AmountWords;
            //AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            //if (AmountWords.Length <= 40)
            //{
            //    e.Graphics.DrawString("In Words : " + AmountWords + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("In Words", myFont).Height) + 1;
            //}
            //else
            //{
            //    e.Graphics.DrawString("In Words : " + AmountWords.Substring(0, 39) + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("In Words", myFont).Height) + 1;
            //    e.Graphics.DrawString(AmountWords.Substring(39, AmountWords.Length - 39) + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("In Words", myFont).Height) + 1;
            //}
            e.Graphics.DrawString("-----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 7;
            //if (DTVouMain.Rows[0]["Remarks"].ToString() != "")
            //{
            //    e.Graphics.DrawString("Remarks : " + DTVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("Remarks", myFont).Height) + 20;
            //}
            e.Graphics.DrawString($"User :  {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintFNBSalesOrderKOTBOTHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;
            Query =
                "SELECT SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,GL.GlCode,GL.GlName,GL.GlAddress,PhoneNo,LandLineNo,Email,PanNo,SOM.TableCode,Remarks,Is_Printed,SOM.Enter_By, SOM.Enter_Date FROM AMS.SO_Master as SOM ";
            Query = $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id  ";
            Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.Branch_Id={ObjGlobal.SysBranchId}  ";

            Query =
                "SELECT SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks,Is_Printed,SOM.Enter_By, SOM.Enter_Date FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
            Query =
                $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
            Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
            DTVouMain.Reset();
            DTVouMain = GetConnection.SelectDataTableQuery(Query);
            if (DTVouMain.Rows.Count <= 0) return;

            Query =
                "Select Distinct Case When P.P_Type=1 then 'KOT' When P.P_Type=2 Then 'BOT' End P_TypeName,P.P_Type From AMS.SO_Details as SOD ";
            Query =
                $"{Query} Inner Join AMS.SalesOrder_Main as SOM On SOM.SalesOrder_Id=SOD.Reference_Id and SOM.SO_Invoice=SOD.SO_Invoice Inner Join AMS.Product as P On P.PId=SOD.P_Id ";
            Query =
                $"{Query} Where P.P_Type is Not Null and SOM.Order_No='{VouNo}' and SOM.Branch_Id={ObjGlobal.SysBranchId}  ";
            DTKOTBOT.Reset();
            DTKOTBOT = GetConnection.SelectDataTableQuery(Query);

            for (var j = 0; j < DTKOTBOT.Rows.Count; j++)
            {
                myFont = new Font("Arial", 9, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(DTKOTBOT.Rows[j]["P_TypeName"].ToString(), myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString(DTKOTBOT.Rows[j]["P_TypeName"].ToString(), myFont)
                    .Height + 5;

                if (DTVouMain.Rows[0]["Class_Code"] != null &&
                    DTVouMain.Rows[0]["Class_Code"].ToString() != string.Empty)
                {
                    if (InvoiceType == "5")
                        e.Graphics.DrawString($"Room No: {DTVouMain.Rows[0]["Class_Code"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString($"Table No: {DTVouMain.Rows[0]["Class_Code"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                }

                iTopMargin += (int)e.Graphics.MeasureString("Table No : ", myFont).Height + 5;

                myFont = new Font("Arial", 8, FontStyle.Regular);
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"Order No : {DTVouMain.Rows[0]["Order_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, 175, iCellHeight), strFormat);
                if (ObjGlobal.SysDateType == "AD")
                    e.Graphics.DrawString(
                        $"{Convert.ToDateTime(DTVouMain.Rows[0]["Order_Date"].ToString()).ToShortDateString()}  {Convert.ToDateTime(DTVouMain.Rows[0]["Created_Date"].ToString()).ToShortTimeString()}",
                        myFont, Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString(
                        $"{DTVouMain.Rows[0]["Order_BSDate"]}  {Convert.ToDateTime(DTVouMain.Rows[0]["Created_Date"].ToString()).ToShortTimeString()}",
                        myFont, Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;

                if (DTVouMain.Rows[0]["Ledger_Name"].ToString().Length > 19)
                    e.Graphics.DrawString(
                        $"Name : {DTVouMain.Rows[0]["Ledger_Name"].ToString().Substring(0, 20)}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(0, iTopMargin, 175, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString($"Name : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(0, iTopMargin, 175, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 1;

                ColHeaders.Clear();
                ColHeaders.Add("Sn");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Center");

                ColWidths.Add(20);
                ColWidths.Add(150);
                ColWidths.Add(55);
                ColWidths.Add(60);

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(
                    "-------------------------------------------------------------------------------", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
                myFont = new Font("Arial", 8, FontStyle.Bold);
                for (var i = 0; i < ColHeaders.Count; i++)
                {
                    if (ColFormat[i].ToString() == "Left")
                        strFormat.Alignment = StringAlignment.Near;
                    else if (ColFormat[i].ToString() == "Center")
                        strFormat.Alignment = StringAlignment.Center;
                    else if (ColFormat[i].ToString() == "Right")
                        strFormat.Alignment = StringAlignment.Far;

                    iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                        Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                    e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i],
                            iHeaderHeight), strFormat);
                }

                iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString(
                    "-------------------------------------------------------------------------------", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

                iTopMargin += 1;
                //---------End Report Header ----------------

                Query =
                    "Select Distinct SOM.Order_No,P.Product_Code,Product_Name,Gdn_Code,Gdn_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,SOD.Basic_Amount, ";
                Query =
                    $"{Query} SOD.Term_Amount,SOD.Net_Amount From AMS.SalesOrder_Details as SOD Inner Join AMS.SalesOrder_Main as SOM On SOM.Order_No=SOD.Order_No ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.Product_Id=SOD.Product_Id Left Outer Join AMS.Godown as G On G.Gdn_Id=SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=SOD.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=SOD.Unit_Id";
                Query =
                    $"{Query} Where SOM.Order_No='{VouNo}' and P.P_Type={DTKOTBOT.Rows[j]["P_Type"]} and SOM.Branch_Id={ObjGlobal.SysBranchId}  and SOM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                PrintFNBSalesOrderKOTBOTDetails3InchRollPaper(e);
            }
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintFNBSalesOrderKOTBOTDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 20;
                var iCount = 0;
                for (i = 0; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;
                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            //iTopMargin += 10;
            //iLeftMargin = ((e.MarginBounds.Width * 3) / 100);
            //strFormat.Alignment = StringAlignment.Near;
            //myFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            //e.Graphics.DrawString("-----------------------------------------------------------", myFont, Brushes.Black, new RectangleF(70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //myFont = new System.Drawing.Font("Arial", 8, FontStyle.Bold);
            //e.Graphics.DrawString("Total : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)300, (float)iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Total", myFont).Height) + 5;

            //if (DTVouMain.Rows[0]["Remarks"].ToString() != "")
            //{
            //    e.Graphics.DrawString("Remarks : " + DTVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;
            //}
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintOrderHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = 300; // e.MarginBounds.Width;
            iLeftMargin = 5; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;
            iCellHeight = 20;
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;
            Query =
                "SELECT Top 1 SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
            Query =
                $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
            Query =
                $"{Query} Where SOM.SO_Invoice='{VouNo}' and (PrintedItem =0 or PrintedItem is null) Order By OrderTime desc ";
            DTVouMain.Reset();
            DTVouMain = GetConnection.SelectDataTableQuery(Query);
            if (DTVouMain.Rows.Count <= 0) return;
            Query =
                "Select Distinct PG.GrpName,PG.PGrpID,PG.GrpCode,PG.Gprinter From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=SOD.SO_Invoice  Inner Join AMS.Product as P On P.PID=SOD.P_Id Inner Join AMS.ProductGroup as PG On PG.PGrpID=P.PGrpId ";
            Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' and (PrintedItem =0 or PrintedItem is null) ";
            if (GrpCode != string.Empty)
                Query = $"{Query} and PG.GrpCode ='{GrpCode}' ";
            DTKOTBOT.Reset();
            DTKOTBOT = GetConnection.SelectDataTableQuery(Query);

            for (var j = 0; j < DTKOTBOT.Rows.Count; j++)
            {
                iLeftMargin = 5;

                myFont = new Font("Arial", 9, FontStyle.Bold);
                strFormat.Alignment = StringAlignment.Center;
                e.Graphics.DrawString(DTKOTBOT.Rows[j]["GrpName"].ToString(), myFont, Brushes.Black,
                    new RectangleF(5, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin +=
                    (int)e.Graphics.MeasureString(DTKOTBOT.Rows[j]["GrpName"].ToString(), myFont).Height + 5;

                //myFont = new System.Drawing.Font("Arial", 9, FontStyle.Bold);
                //strFormat.Alignment = StringAlignment.Center;
                //e.Graphics.DrawString("KOT/BOT", myFont, Brushes.Black, new RectangleF(35, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
                //iTopMargin += (int)(e.Graphics.MeasureString("KOT/BOT", myFont).Height) + 5;

                e.Graphics.DrawString($"Table No : {DTVouMain.Rows[0]["TableName"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(5, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Table No : ", myFont).Height + 5;

                myFont = new Font("Arial", 8, FontStyle.Regular);
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"Order No : {DTVouMain.Rows[0]["SO_Invoice"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(5, iTopMargin, 170, iCellHeight), strFormat);
                if (ObjGlobal.SysDateType == "D")
                    e.Graphics.DrawString(
                        $"{Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}  {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}",
                        myFont, Brushes.Black, new RectangleF(172, iTopMargin, PageWidth, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString(
                        $"{DTVouMain.Rows[0]["Invoice_Miti"]}  {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}",
                        myFont, Brushes.Black, new RectangleF(172, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;

                //if (DTVouMain.Rows[0]["Catagory"].ToString() == "Cash Book")
                //{
                //    e.Graphics.DrawString("Customer : Cash", myFont, Brushes.Black, new RectangleF(5, (float)iTopMargin, (int)175, (float)iCellHeight), strFormat);
                //}
                //else
                //{
                //    if (DTVouMain.Rows[0]["GlDesc"].ToString().Length > 19)
                //        e.Graphics.DrawString("Customer : " + DTVouMain.Rows[0]["GlDesc"].ToString().Substring(0, 20) + "", myFont, Brushes.Black, new RectangleF(5, (float)iTopMargin, (int)175, (float)iCellHeight), strFormat);
                //    else
                //        e.Graphics.DrawString("Customer : " + DTVouMain.Rows[0]["GlDesc"].ToString() + "", myFont, Brushes.Black, new RectangleF(5, (float)iTopMargin, (int)175, (float)iCellHeight), strFormat);
                //}
                e.Graphics.DrawString($"Waiter : {DTVouMain.Rows[0]["WaiterName"]}", myFont, Brushes.Black,
                    new RectangleF(5, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height;

                ColHeaders.Clear();
                ColHeaders.Add("Sn");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(200);
                ColWidths.Add(55);

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("-------------------------------------------------------------------------",
                    myFont, Brushes.Black, new RectangleF(5, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height;
                myFont = new Font("Arial", 8, FontStyle.Bold);
                for (var i = 0; i < ColHeaders.Count; i++)
                {
                    if (ColFormat[i].ToString() == "Left")
                        strFormat.Alignment = StringAlignment.Near;
                    else if (ColFormat[i].ToString() == "Center")
                        strFormat.Alignment = StringAlignment.Center;
                    else if (ColFormat[i].ToString() == "Right")
                        strFormat.Alignment = StringAlignment.Far;

                    iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                        Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                    e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i],
                            iHeaderHeight), strFormat);
                }

                iTopMargin += 8; // (int)(e.Graphics.MeasureString("Particulars", myFont).Height) ;
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("------------------------------------------------------------------------",
                    myFont, Brushes.Black, new RectangleF(5, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height;

                //---------End Report Header ----------------

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PID, P.PShortName,PName,APU.UnitCode AltUnitCode,PU.UnitCode ,SOD.Qty,SOD.Rate,SOD.B_Amount,SOD.T_Amount,SOD.N_Amount,Notes From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PID = SOD.P_Id Left Outer Join AMS.ProductUnit as APU On APU.UID = SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SOD.Unit_Id ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and P.PGrpId='{DTKOTBOT.Rows[j]["PGrpID"]}' and (PrintedItem =0 or PrintedItem is null)  ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                PrintOrderDetails3InchRollPaper(e);
            }
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintOrderDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 20;
                var iCount = 0;
                for (i = 0; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    if (DTVouDetails.Rows[i]["PName"].ToString().Length > 180)
                        iCellHeight = 60;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 120)
                        iCellHeight = 50;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 80)
                        iCellHeight = 40;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 40)
                        iCellHeight = 30;
                    else
                        iCellHeight = 15;

                    myFont = new Font("Arial", 7, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        var PDesc = string.Empty;
                        PDesc = DTVouDetails.Rows[i]["PName"].ToString();
                        strFormat.Alignment = StringAlignment.Near;
                        if (PDesc.Length <= 40)
                        {
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                        }
                        else
                        {
                            var Pheight = iTopMargin;
                            if (PDesc.Length > 80 && PDesc.Length < 120)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(40, 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15 + 15;
                                e.Graphics.DrawString(PDesc.Substring(80, PDesc.Length - 80), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else if (PDesc.Length > 40 && PDesc.Length < 80)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(40, PDesc.Length - 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else
                            {
                                strFormat.Alignment = StringAlignment.Near;
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont,
                                    Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                        }
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;

                    if (DTVouDetails.Rows[i]["Notes"].ToString().Trim() != string.Empty)
                    {
                        iCount = 1;
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString($"-> {DTVouDetails.Rows[i]["Notes"]}", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                        iRow++;
                        iTopMargin += iCellHeight;
                    }

                    if (Printed)
                    {
                        var Smt = string.Empty;
                        Smt = " Update AMS.SO_Details Set PrintedItem=1 ";
                        Smt = $"{Smt} Where SO_Invoice = '{DTVouDetails.Rows[i]["SO_Invoice"]}' and ";
                        Smt = $"{Smt} Invoice_SNo = '{DTVouDetails.Rows[i]["Invoice_SNo"]}' and ";
                        Smt = $"{Smt} PId = '{DTVouDetails.Rows[i]["PId"]}' ";
                        var con = GetConnection.ReturnConnection();
                        var cmd = new SqlCommand(Smt, con);
                        var Result = cmd.ExecuteNonQuery();
                    }
                }
            }

            iTopMargin += (int)e.Graphics.MeasureString("Remarks", myFont).Height + 20;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintPreInvoiceHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 5;
            e.Graphics.DrawString(DTCD.Rows[0]["PhoneNo"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["PhoneNo"].ToString(), myFont).Height + 5;
            //e.Graphics.DrawString("PAN No : " + DTCD.Rows[0]["PanNo"].ToString() + "", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)250, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("PAN No : ", myFont).Height) + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("PRE INVOICE", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PRE INVOICE", myFont).Height + 1;

            //e.Graphics.DrawString("Table No : " + DTVouMain.Rows[0]["TableShortName"] + "", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)250, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("PRE INVOICE", myFont).Height) + 1;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("---------------------------------------------------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)250, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("-----------", myFont).Height) + 1;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Purchaser Name : " + DTVouMain.Rows[0]["GlDesc"].ToString() + "", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)175, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Name : ", myFont).Height) + 5;
            ////e.Graphics.DrawString("Purchaser PAN : " + DTVouMain.Rows[0]["PanNo"].ToString() + "", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)175, (float)iCellHeight), strFormat);
            ////iTopMargin += (int)(e.Graphics.MeasureString("Name : ", myFont).Height) + 5;
            //e.Graphics.DrawString("Address : " + DTVouMain.Rows[0]["AddressI"].ToString() + "", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)175, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("AddressI : ", myFont).Height) + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["SO_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 155, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(157, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 5;
            e.Graphics.DrawString($"Table No  : {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 155, iCellHeight), strFormat);
            e.Graphics.DrawString($"Miti  : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(157, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
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

    private void PrintPreInvoiceDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode,PU.UnitCode AltUnitCode, SOD.Qty,SOD.Rate, SOD.B_Amount, SOD.T_Amount,SOD.N_Amount ";
                Query =
                    $"{Query} From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice = SOD.SO_Invoice Inner Join AMS.Product as P On P.PId = SOD.P_Id Left Outer Join AMS.Godown as G On G.GID = SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SOD.Unit_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("SN.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add(string.Empty); //Unit
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(75);
                ColWidths.Add(35);
                ColWidths.Add(30);
                ColWidths.Add(40);
                ColWidths.Add(50);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 15;
                var iCount = 0;
                PrintPreInvoiceHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()).ToString("0.00"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Basic Amount : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, 300, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(Global._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString("0.0"), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString("0.0"), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString("0.0"), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString("0.0"), myFont,
                    Brushes.Black,
                    new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 33)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 33)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(33, AmountWords.Length - 33)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 15;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Cashier :  " + Global.UserCode + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintPreInvoiceWithoutUnitDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT Top 1 SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode,PU.UnitCode AltUnitCode, SOD.Qty,SOD.Rate, SOD.B_Amount, SOD.T_Amount,SOD.N_Amount ";
                Query =
                    $"{Query} From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice = SOD.SO_Invoice Inner Join AMS.Product as P On P.PId = SOD.P_Id Left Outer Join AMS.Godown as G On G.GID = SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SOD.Unit_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("SN.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(100);
                ColWidths.Add(35);
                ColWidths.Add(40);
                ColWidths.Add(50);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 15;
                var iCount = 0;
                PrintPreInvoiceHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    if (DTVouDetails.Rows[i]["PName"].ToString().Length > 80)
                        iCellHeight = 60;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 60)
                        iCellHeight = 50;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 40)
                        iCellHeight = 40;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 20)
                        iCellHeight = 30;
                    else
                        iCellHeight = 15;

                    myFont = new Font("Arial", 7, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        var PDesc = string.Empty;
                        PDesc = DTVouDetails.Rows[i]["PName"].ToString();
                        strFormat.Alignment = StringAlignment.Near;
                        if (PDesc.Length <= 20)
                        {
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                        }
                        else
                        {
                            var Pheight = iTopMargin;
                            if (PDesc.Length > 40 && PDesc.Length < 60)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(20, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15 + 15;
                                e.Graphics.DrawString(PDesc.Substring(40, PDesc.Length - 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else if (PDesc.Length > 20 && PDesc.Length < 40)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(20, PDesc.Length - 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else
                            {
                                strFormat.Alignment = StringAlignment.Near;
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont,
                                    Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                            }
                        }
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Basic Amount : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, 300, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(Global._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString("0.0"), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    //e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[3], (float)iTopMargin, (int)arrColumnWidths[3], (float)iCellHeight), strFormat);
                    e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString("0.0"), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString("0.0"), myFont,
                    Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 33)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 33)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(33, AmountWords.Length - 33)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 15;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Cashier :  " + Global.UserCode + " ", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesOrderHalfHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 15;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            if (DTCD.Rows[0]["Pan_No"] != null && DTCD.Rows[0]["Pan_No"].ToString() != string.Empty)
                e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Date   : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()} {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["SO_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString($"Miti   : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            e.Graphics.DrawString($"Table No/Room : {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 400, iCellHeight), strFormat);
            e.Graphics.DrawString($"Guest Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(400, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["PanNo"] != null && DTVouMain.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Customer's PAN/VAT No     : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(20, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }
            //if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != "")
            //    e.Graphics.DrawString("Address  : " + DTVouMain.Rows[0]["Address"].ToString() + "", myFont, Brushes.Black, new RectangleF(20, (float)iTopMargin, (int)650, (float)iCellHeight), strFormat);

            //if (DTVouMain.Rows[0]["TelNoI"] != null && DTVouMain.Rows[0]["TelNoI"].ToString() != "")
            //{//" + DTVouMain.Rows[0]["TelNoI"] + "
            e.Graphics.DrawString($"Mode Of Payment : {DTVouMain.Rows[0]["Payment_Mode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 650, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            //}

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesOrderHalfDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, Case When (SOM.Party_Name IS NULL or SOM.Party_Name='') Then GL.GLName Else SOM.Party_Name End GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,Case When (SOM.Vat_No IS NULL or SOM.Vat_No='') Then GL.PanNo Else SOM.Vat_No End PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks,Payment_Mode FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode AltUnitCode,PU.UnitCode , SOD.Qty,SOD.Rate, SOD.B_Amount, SOD.T_Amount,SOD.N_Amount ";
                Query =
                    $"{Query} From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice = SOD.SO_Invoice Inner Join AMS.Product as P On P.PId = SOD.P_Id Left Outer Join AMS.Godown as G On G.GID = SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SOD.Unit_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(425);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesOrderHalfHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(Global._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ",
                myFont, Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            ////e.Graphics.DrawString("           " + CGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {DTCD.Rows[0]["Company_Name"]} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesOrderHalfPANDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, Case When (SOM.Party_Name IS NULL or SOM.Party_Name='') Then GL.GLName Else SOM.Party_Name End GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,Case When (SOM.Vat_No IS NULL or SOM.Vat_No='') Then GL.PanNo Else SOM.Vat_No End PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks,Payment_Mode FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode AltUnitCode,PU.UnitCode , SOD.Qty,SOD.Rate, SOD.B_Amount, SOD.T_Amount,SOD.N_Amount ";
                Query =
                    $"{Query} From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice = SOD.SO_Invoice Inner Join AMS.Product as P On P.PId = SOD.P_Id Left Outer Join AMS.Godown as G On G.GID = SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SOD.Unit_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(425);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesOrderHalfHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(Global._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ",
                myFont, Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            ////e.Graphics.DrawString("           " + CGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {DTCD.Rows[0]["Company_Name"]} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesOrderA5Header(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 15;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            if (DTCD.Rows[0]["Pan_No"] != null && DTCD.Rows[0]["Pan_No"].ToString() != string.Empty)
                e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Date   : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()} {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["SO_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString($"Miti   : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            e.Graphics.DrawString($"Table No/Room : {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 200, iCellHeight), strFormat);
            e.Graphics.DrawString($"Guest Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(200, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["PanNo"] != null && DTVouMain.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Customer's PAN/VAT No     : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }
            //if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != "")
            //    e.Graphics.DrawString("Address  : " + DTVouMain.Rows[0]["Address"].ToString() + "", myFont, Brushes.Black, new RectangleF(20, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);

            //if (DTVouMain.Rows[0]["TelNoI"] != null && DTVouMain.Rows[0]["TelNoI"].ToString() != "")
            //{//" + DTVouMain.Rows[0]["TelNoI"] + "
            e.Graphics.DrawString($"Mode Of Payment : {DTVouMain.Rows[0]["Payment_Mode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            //}

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
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

    private void PrintSalesOrderA5Details(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SOM.SO_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, Case When (SOM.Party_Name IS NULL or SOM.Party_Name='') Then GL.GLName Else SOM.Party_Name End GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,Case When (SOM.Vat_No IS NULL or SOM.Vat_No='') Then GL.PanNo Else SOM.Vat_No End PanNo,CM.CCode ,CM.CName,RTM.TableName,RTM.TableCode,W.AgentName WaiterName, Remarks,Payment_Mode FROM AMS.SO_Master as SOM Inner Join AMS.SO_Details as SOD On SOM.SO_Invoice=SOD.SO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SOM.Customer_Id Inner Join AMS.TableMaster as RTM On RTM.TableId=SOM.TableId Left Outer Join AMS.Counter as CM On CM.CId=SOM.CounterId Left Outer Join AMS.JuniorAgent as W On W.AgentId=SOM.Agent_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SOM.SO_Invoice,SOD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode AltUnitCode ,PU.UnitCode, SOD.Qty,SOD.Rate, SOD.B_Amount, SOD.T_Amount,SOD.N_Amount ";
                Query =
                    $"{Query} From AMS.SO_Details as SOD Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice = SOD.SO_Invoice Inner Join AMS.Product as P On P.PId = SOD.P_Id Left Outer Join AMS.Godown as G On G.GID = SOD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SOD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SOD.Unit_Id ";
                Query = $"{Query} Where SOM.SO_Invoice='{VouNo}' ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SO_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SO_Master as SOM On SOM.SO_Invoice=STD.SO_VNo  ";
                Query =
                    $"{Query} Where SOM.SO_Invoice='{VouNo}' and SOM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SOM.SO_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SO_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(200);
                ColWidths.Add(55);
                ColWidths.Add(40);
                ColWidths.Add(70);
                ColWidths.Add(100);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesOrderA5Header(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 9, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(Global._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ",
                myFont, Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Remarks", myFont).Height + 15;

            ////e.Graphics.DrawString("           " + CGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {DTCD.Rows[0]["Company_Name"]} ", myFont, Brushes.Black,
                new RectangleF(50, iTopMargin, 450, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion ---------------------------------------- Sales Order Default Design ----------------------------------------

    #endregion ---------------------------------------- Sales Order Design ----------------------------------------

    #region ---------------------------------------- Sales Challan Design ----------------------------------------

    #region ---------------------------------------- Sales Challan Default Design ----------------------------------------

    private void PrintSalesChallanHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

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
            e.Graphics.DrawString("SALES CHALLAN", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES CHALLAN", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Challan No : {DTVouMain.Rows[0]["SC_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Challan No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["GlAddress"] != null && DTVouMain.Rows[0]["GlAddress"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Challan Date  : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Challan Miti  : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Challan Date   : ", myFont).Height + 1;

            if (DTVouMain.Rows[0]["PanNo"] != null && DTVouMain.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"PAN/VAT No     : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }

            if (DTVouMain.Rows[0]["PhoneNo"] != null && DTVouMain.Rows[0]["PhoneNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Phone No       : {DTVouMain.Rows[0]["PhoneNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesChallanDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SCM.SC_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Invoice_Time,GL.GlCode,GL.GlType, Case When (SCM.Party_Name IS NULL or SCM.Party_Name='') Then GL.GLName Else SCM.Party_Name End GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,Case When (SCM.Vat_No IS NULL or SCM.Vat_No='') Then GL.PanNo Else SCM.Vat_No End PanNo, Remarks,Payment_Mode FROM AMS.SC_Master as SCM Inner Join AMS.SC_Details as SCD On SCM.SC_Invoice=SCD.SC_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SCM.Customer_Id  Left Outer Join AMS.JuniorAgent as W On W.AgentId=SCM.Agent_Id ";
                Query =
                    $"{Query} Where SCM.SC_Invoice='{VouNo}' and SCM.CBranch_Id={ObjGlobal.SysBranchId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SCM.SC_Invoice,SCD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode AltUnitCode,SCD.Alt_Qty, PU.UnitCode , SCD.Qty,SCD.Rate, SCD.B_Amount, SCD.T_Amount,SCD.N_Amount ";
                Query =
                    $"{Query} From AMS.SC_Details as SCD Inner Join AMS.SC_Master as SCM On SCM.SC_Invoice = SCD.SC_Invoice Inner Join AMS.Product as P On P.PId = SCD.P_Id Left Outer Join AMS.Godown as G On G.GID = SCD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as APU On APU.UID = SCD.Alt_UnitId Left Outer Join AMS.ProductUnit as PU On PU.UID = SCD.Unit_Id ";
                Query = $"{Query} Where SCM.SC_Invoice='{VouNo}' and SCM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SC_Invoice,ST_Id,Order_No,ST_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SCM.SC_Invoice,BT.ST_Id,Order_No,ST_Name,Case When Term_Type='B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign='-' Then -Sum(STD.Amount) Else  Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SC_Term as STD On BT.ST_ID=STD.ST_Id Inner Join AMS.SC_Master as SCM On SCM.SC_Invoice=STD.SC_VNo  ";
                Query =
                    $"{Query} Where SCM.SC_Invoice='{VouNo}' and SCM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND STD.Amount<>0 ";
                Query = $"{Query} Group By SCM.SC_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By SC_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(35);
                ColWidths.Add(345);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesChallanHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["AltUnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["AltUnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Remarks", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion ---------------------------------------- Sales Challan Default Design ----------------------------------------

    #endregion ---------------------------------------- Sales Challan Design ----------------------------------------

    #region ---------------------------------------- Sales Bill Design ----------------------------------------

    #region Sales Bill Default Design

    private void PrintSalesBillHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(30, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(30, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(30, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["Is_Printed"].ToString()))
            {
                if (Convert.ToBoolean(DTVouMain.Rows[0]["Is_Printed"].ToString())) //DUBLICATE COPY
                    e.Graphics.DrawString("COPY OF ORIGINAL", myFont, Brushes.Black,
                        new RectangleF(30, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                        new RectangleF(30, iTopMargin, 800, iCellHeight), strFormat);
                //e.Graphics.DrawString("TAX INVOICE", myFont, Brushes.Black, new RectangleF(30, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                    new RectangleF(30, iTopMargin, 800, iCellHeight), strFormat);
            }
            //e.Graphics.DrawString("TAX INVOICE", myFont, Brushes.Black, new RectangleF(30, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);

            if (DTVouMain.Rows[0]["No_Print"].ToString() != "0")
            {
                myFont = new Font("Arial", 10, FontStyle.Regular);
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"Copy Of Print :{DTVouMain.Rows[0]["No_Print"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("TAX INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(30, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString($"Invoice No  : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["GlAddress"].ToString()))
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(30, iTopMargin, 600, iCellHeight), strFormat);

            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Invoice Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Invoice Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["PanNo"].ToString()))
            {
                e.Graphics.DrawString($"PAN/VAT No  : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(30, iTopMargin, 600, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;
            }

            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["PhoneNo"].ToString()))
            {
                e.Graphics.DrawString($"Phone No    : {DTVouMain.Rows[0]["PhoneNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(30, iTopMargin, 600, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
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

    private void PrintSalesBillDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 10; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(325);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesBillHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Alt_UnitCode"].ToString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            ////e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesInvoiceHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["Is_Printed"].ToString()))
            {
                if (Convert.ToBoolean(DTVouMain.Rows[0]["Is_Printed"].ToString())) //DUBLICATE COPY
                    e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                        new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("TAX INVOICE", myFont, Brushes.Black,
                        new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
            }
            else
            {
                if (Fristprintcopy)
                    e.Graphics.DrawString("TAX INVOICE", myFont, Brushes.Black,
                        new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                        new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
            }

            //dt.Reset();
            //Query = "Select count(Voucher_No) NoOf_Print from AMS.Print_Voucher Where Voucher_No='" + DTVouMain.Rows[0]["Invoice_No"] + "' and Module= '" + Module + "' and Branch_Id=" + ObjGlobal._Branch_Id + " ";
            //dt = GetConnection.SelectDataTableQuery(Query);
            //if (dt.Rows.Count > 0)
            //{
            if (DTVouMain.Rows[0]["No_Print"].ToString() != "0")
            {
                myFont = new Font("Arial", 10, FontStyle.Regular);
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"COPY OF ORIGINAL  - {DTVouMain.Rows[0]["No_Print"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            }

            //}
            iTopMargin += (int)e.Graphics.MeasureString("TAX INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Seller PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Seller PAN/VAT No : ", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Seller Address : {DTCD.Rows[0]["Address"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(25, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Seller Address : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Customer Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(25, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString($"Invoice No   : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            //if (DTVouMain.Rows[0]["GlAddress"] != null && DTVouMain.Rows[0]["GlAddress"].ToString() != "")
            e.Graphics.DrawString($"Customer Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(25, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Invoice Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Miti"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date(BS[AD]) : ", myFont).Height + 5;

            e.Graphics.DrawString($"Customer PAN/VAT No. :{DTVouMain.Rows[0]["PanNo"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(25, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"[{Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}]", myFont,
                Brushes.Black, new RectangleF(680, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Customer PAN/VAT No. ", myFont).Height + 5;

            e.Graphics.DrawString("Payment Terms : Cash/Cheque/Credit/Others" + string.Empty, myFont, Brushes.Black,
                new RectangleF(25, iTopMargin, 660, iCellHeight), strFormat);
            e.Graphics.DrawString($"Print Date   : {DateTime.Now}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Print Date : ", myFont).Height + 5;

            e.Graphics.DrawString($"Phone No. : {DTVouMain.Rows[0]["PhoneNo"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(25, iTopMargin, 600, iCellHeight), strFormat);
            //e.Graphics.DrawString("" + System.DateTime.Now.ToShortTimeString() + "", myFont, Brushes.Black, new RectangleF(650, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Phone No.  : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
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

    private void PrintSalesInvoiceDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 10; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 60;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                //ColHeaders.Add("Alt Qty");
                //ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                //ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                //ColFormat.Add("Right");
                //ColFormat.Add("Middle");
                ColFormat.Add("Right");
                //ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(325);
                //ColWidths.Add(60);
                //ColWidths.Add(40);
                ColWidths.Add(160);
                //ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesInvoiceHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 7)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 7; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                var tai = 1;
                double Term_Amount = 0;
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        $"{Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat)} %",
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                    Term_Amount = Term_Amount + Convert.ToDouble(DTRo["Term_Amount"]);
                    tai = tai + 1;

                    if (tai == DTTermDetails.Rows.Count)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Taxable Amt. : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(
                            Convert.ToDecimal(Convert.ToDecimal(TotBasicAmt.ToString()) +
                                              Convert.ToDecimal(Term_Amount)).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics.MeasureString("Taxable Amt. : ", myFont).Height + 5;
                    }
                }
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            //if (DTVouMain.Rows[0]["Remarks"].ToString() != "")
            //{
            //    e.Graphics.DrawString("Remarks : " + DTVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("Remarks ", myFont).Height) + 50;
            //}
            e.Graphics.DrawString(
                "Note : " + "Payments received by cheques are subject to the collection by the concerned bank.",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Note :", myFont).Height + 50;

            e.Graphics.DrawString(
                "     -----------------------------------                                                                                                  -------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString("     Signature Of Receiver          ", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 200, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Signed By", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintCounterSalesBillHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_Number"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["Printed"].ToString()))
            {
                if (Convert.ToBoolean(DTVouMain.Rows[0]["Printed"].ToString())) //DUBLICATE COPY
                    e.Graphics.DrawString("COPY OF ORIGINAL", myFont, Brushes.Black,
                        new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black,
                        new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            }

            dt.Reset();
            Query =
                $"Select count(Voucher_No) NoOf_Print from AMS.Print_Voucher Where Voucher_No='{DTVouMain.Rows[0]["Invoice_No"]}' and Module= '{Module}' and Branch_Id={ObjGlobal.SysBranchId} and FiscalYear_Id={ObjGlobal.SysFiscalYearId} ";
            dt = GetConnection.SelectDataTableQuery(Query);
            if (dt.Rows.Count > 0)
                if (dt.Rows[0]["NoOf_Print"].ToString() != "0")
                {
                    myFont = new Font("Arial", 10, FontStyle.Regular);
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"Copy Of Print :{dt.Rows[0]["NoOf_Print"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
                }

            iTopMargin += (int)e.Graphics.MeasureString("SALES INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Customer Name  : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["Invoice_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Class_Code"] != null &&
                DTVouMain.Rows[0]["Class_Code"].ToString() != string.Empty)
                e.Graphics.DrawString($"Counter  : {DTVouMain.Rows[0]["Class_Code"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintCounterSalesBillDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query =
                    $"{Query} Where SIM.Invoice_Mode= '{InvoiceType}' and SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query =
                    $"{Query} Where SIM.Invoice_Mode= '{InvoiceType}' and SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.Invoice_Mode= '{InvoiceType}' and SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(35);
                ColWidths.Add(345);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintCounterSalesBillHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["AltUnit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["AltUnit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Basic_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Basic_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Basic_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintFNBSalesBillHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;

            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_Number"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["Printed"].ToString()))
            {
                if (Convert.ToBoolean(DTVouMain.Rows[0]["Printed"].ToString())) //DUBLICATE COPY
                    e.Graphics.DrawString("COPY OF ORIGINAL", myFont, Brushes.Black,
                        new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black,
                        new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            }

            //dt.Reset();
            //Query = "Select count(Voucher_No) NoOf_Print from AMS.Print_Voucher Where Voucher_No='" + DTVouMain.Rows[0]["Invoice_No"] + "' and Module= '" + Module + "' and Branch_Id=" + ObjGlobal._Branch_Id + " and FiscalYear_Id=" + ObjGlobal._FiscalYear_Id + " ";
            //dt = GetConnection.SelectDataTableQuery(Query);
            //if (dt.Rows.Count > 0)
            //{
            if (DTVouMain.Rows[0]["No_Print"].ToString() != "0")
            {
                myFont = new Font("Arial", 10, FontStyle.Regular);
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString($"Copy Of Print :{DTVouMain.Rows[0]["No_Print"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            }

            //}
            iTopMargin += (int)e.Graphics.MeasureString("SALES INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Customer Name  : {DTVouMain.Rows[0]["Gl_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Class_Code"] != null &&
                DTVouMain.Rows[0]["Class_Code"].ToString() != string.Empty)
            {
                if (InvoiceType == "H")
                    e.Graphics.DrawString($"Room No  : {DTVouMain.Rows[0]["Class_Code"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString($"Table No  : {DTVouMain.Rows[0]["Class_Code"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            }

            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Invoice Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Invoice Date : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintFNBSalesBillDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query =
                    $"{Query} Where SIM.Invoice_Mode= '{InvoiceType}' and SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query =
                    $"{Query} Where SIM.Invoice_Mode= '{InvoiceType}' and SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.Invoice_Mode= '{InvoiceType}' and SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(35);
                ColWidths.Add(445);
                ColWidths.Add(105);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintFNBSalesBillHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Basic_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Basic_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Basic_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            if (DTVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;
            }
            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintFNBSalesBillHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;

            myFont = new Font("Arial", 9, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            //if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["Printed"].ToString()))
            //{
            //    if (Convert.ToBoolean(DTVouMain.Rows[0]["Printed"].ToString()) == true)//DUBLICATE COPY
            //        e.Graphics.DrawString("COPY OF ORIGINAL", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    else
            //        e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //}
            //else
            e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);

            //dt.Reset();
            //Query = "Select count(Voucher_No) NoOf_Print from AMS.Print_Voucher Where Voucher_No='" + DTVouMain.Rows[0]["Invoice_No"] + "' and Module= '" + Module + "' and Branch_Id=" + ObjGlobal._Branch_Id + " and FiscalYear_Id=" + ObjGlobal._FiscalYear_Id + " ";
            //dt = GetConnection.SelectDataTableQuery(Query);
            //if (dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["NoOf_Print"].ToString() != "0")
            //    {
            //        myFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            //        strFormat.Alignment = StringAlignment.Near;
            //        e.Graphics.DrawString("Copy Of Print :" + dt.Rows[0]["NoOf_Print"] + "", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    }
            //}
            iTopMargin += (int)e.Graphics.MeasureString("SALES INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Name : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 175, iCellHeight), strFormat);
            if (DTVouMain.Rows[0]["TableCode"] != null && DTVouMain.Rows[0]["TableCode"].ToString() != string.Empty)
            {
                if (InvoiceType == "H")
                    e.Graphics.DrawString($"Room No: {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString($"Table No: {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 5;

            e.Graphics.DrawString($"Bill No : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 175, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Invoice Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Invoice Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(177, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
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

    private void PrintFNBSalesBillDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName,TM.TableCode FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id Left Outer Join AMS.TableMaster as TM On TM.TableId=SIM.TableId ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                if (!string.IsNullOrEmpty(InvoiceType)) Query = $"{Query} and SIM.Invoice_Mode = '{InvoiceType}' ";

                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                if (!string.IsNullOrEmpty(InvoiceType)) Query = $"{Query} and SIM.Invoice_Mode = '{InvoiceType}' ";

                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 ";
                if (!string.IsNullOrEmpty(InvoiceType)) Query = $"{Query} and SIM.Invoice_Mode = '{InvoiceType}' ";

                Query = $"{Query} Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("Sn");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(100);
                ColWidths.Add(45);
                ColWidths.Add(50);
                ColWidths.Add(70);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 20;
                var iCount = 0;
                PrintFNBSalesBillHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents
#pragma warning disable CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                    if (ObjGlobal.SysFontName != string.Empty && ObjGlobal.SysFontName != null &&
                        ObjGlobal.SysFontSize != 0 && ObjGlobal.SysFontSize != null)
#pragma warning restore CS0472 // The result of the expression is always 'true' since a value of type 'decimal' is never equal to 'null' of type 'decimal?'
                        myFont = new Font(ObjGlobal.SysFontName, Convert.ToInt16(ObjGlobal.SysFontSize),
                            FontStyle.Regular);
                    else
                        myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, 300, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                    Brushes.Black, new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
                myFont = new Font("Arial", 8, FontStyle.Bold);
                e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("---------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 40)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 39)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(39, AmountWords.Length - 39)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("---------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 7;
            if (DTVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;
            }

            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + ObjGlobal._Company_Name + " ", myFont, Brushes.Black, new RectangleF(10, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 5;
            e.Graphics.DrawString($"User :  {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintAbbreviatedSalesBillHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;

            myFont = new Font("Arial", 9, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Red,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            if (DTCD.Rows[0]["Address"].ToString() != string.Empty)
            {
                e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Red,
                    new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 5;
            }

            if (DTCD.Rows[0]["PhoneNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString(DTCD.Rows[0]["PhoneNo"].ToString(), myFont, Brushes.Red,
                    new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["PhoneNo"].ToString(), myFont).Height + 5;
            }

            if (DTCD.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["PanNo"]}{string.Empty}", myFont, Brushes.Red,
                    new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }

            myFont = new Font("Arial", 8, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            //e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Red, new RectangleF(iLeftMargin, (float)iTopMargin, (int)250, (float)iCellHeight), strFormat);
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["No_Print"].ToString()))
            {
                if (Convert.ToInt16(DTVouMain.Rows[0]["No_Print"].ToString()) > 1) //DUBLICATE COPY
                    e.Graphics.DrawString($"COPY OF ORIGINAL  {DTVouMain.Rows[0]["No_Print"]}", myFont, Brushes.Red,
                        new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Abbreviated Tax Invoice", myFont, Brushes.Red,
                        new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString("Abbreviated Tax Invoice", myFont, Brushes.Red,
                    new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("SALES INVOICE", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;

            e.Graphics.DrawString($"Bill No : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 150, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(153, iTopMargin, PageWidth, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(153, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["GlType"].ToString() == "Cash")
            {
                if (DTVouMain.Rows[0]["Party_Name"].ToString() != string.Empty)
                    e.Graphics.DrawString($"Name : Cash ( {DTVouMain.Rows[0]["Party_Name"]} )", myFont,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 150, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Name : Cash", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 150, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString($"Name : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
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

    private void PrintAbbreviatedSalesBillDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,GL.GLCode,GL.ACCode, GL.GLName,GL.GlType, GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,SIM.Party_Name,SIM.Vat_No,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("SN.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add(string.Empty); //Unit
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(95);
                ColWidths.Add(25);
                ColWidths.Add(25);
                ColWidths.Add(35);
                ColWidths.Add(50);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 15;
                var iCount = 0;
                PrintAbbreviatedSalesBillHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents
                    if (DTVouDetails.Rows[i]["PName"].ToString().Length > 60)
                        iCellHeight = 60;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 45)
                        iCellHeight = 50;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 30)
                        iCellHeight = 40;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 15)
                        iCellHeight = 30;
                    else
                        iCellHeight = 15;

                    myFont = new Font("Arial", 7, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        var PDesc = string.Empty;
                        PDesc = DTVouDetails.Rows[i]["PName"].ToString();
                        strFormat.Alignment = StringAlignment.Near;
                        if (PDesc.Length <= 15)
                        {
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                        }
                        else
                        {
                            var Pheight = iTopMargin;
                            if (PDesc.Length > 30 && PDesc.Length < 45)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 15), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(15, 15), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                                Pheight = iTopMargin + 15 + 15;
                                e.Graphics.DrawString(PDesc.Substring(30, PDesc.Length - 30), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                            }
                            else if (PDesc.Length > 15 && PDesc.Length < 30)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 15), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(15, PDesc.Length - 15), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], iCellHeight), strFormat);
                            }
                        }
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;
                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;
                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, PageWidth, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, 105, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1], iCellHeight),
                        strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("-----------------------------------------------------------", myFont,
                    Brushes.Black, new RectangleF((int)arrColumnLefts[1], iTopMargin, PageWidth, iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
                myFont = new Font("Arial", 8, FontStyle.Bold);
                e.Graphics.DrawString("Net Total : ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("Tender Amount : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, PageWidth, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(
                Convert.ToDecimal(DTVouMain.Rows[0]["Tender_Amount"].GetDecimal()).ToString("0.0"), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Tender Amount", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("Change Amount : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, PageWidth, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(
                Convert.ToDecimal(DTVouMain.Rows[0]["Return_Amount"].GetDecimal()).ToString("0.0"), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Change Amount", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 40)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 35)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 5;
                e.Graphics.DrawString($"{AmountWords.Substring(35, AmountWords.Length - 35)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 7;
            if (DTVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Remarks", myFont).Height + 20;
            }

            myFont = new Font("Arial", 8, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (DTVouMain.Rows[0]["TableCode"] != null && DTVouMain.Rows[0]["TableCode"].ToString() != string.Empty)
            {
                if (InvoiceType == "Hotel")
                    e.Graphics.DrawString(
                        $"Room No : {DTVouMain.Rows[0]["TableCode"]}     Print Time   {DateTime.Now.ToShortTimeString()}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight),
                        strFormat);
                else if (InvoiceType == "Restro")
                    e.Graphics.DrawString(
                        $"Table No : {DTVouMain.Rows[0]["TableCode"]}     Print Time   {DateTime.Now.ToShortTimeString()}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight),
                        strFormat);
                else
                    e.Graphics.DrawString(
                        $"Counter : {DTVouMain.Rows[0]["TableCode"]}     Print Time   {DateTime.Now.ToShortTimeString()}{string.Empty}",
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight),
                        strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Counter : ", myFont).Height + 5;

            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + CObjGlobal._Company_Name + " ", myFont, Brushes.Black, new RectangleF(10, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 5;
            e.Graphics.DrawString($"User :  {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(string.Empty, myFont, Brushes.Red,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("****Goods one sold will not be return****", myFont, Brushes.Red,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Exchange Within 7 Days with Receipt Only.", myFont, Brushes.Red,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(string.Empty, myFont, Brushes.Red,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("****Thank You Visit Again****", myFont, Brushes.Red,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Thank You Visit Again", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesBillHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont).Height + 5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 5;
            e.Graphics.DrawString(DTCD.Rows[0]["Phone"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Phone"].ToString(), myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;

            e.Graphics.DrawString("SALES INVOICE", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES INVOICE", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["VNo"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 155, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Date : {Convert.ToDateTime(DTVouMain.Rows[0]["VDate"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(157, iTopMargin, PageWidth, iCellHeight), strFormat);
            //e.Graphics.DrawString("Miti  : " + DTVouMain.Rows[0]["VMiti"].ToString() + "", myFont, Brushes.Black, new RectangleF(157, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 5;

            e.Graphics.DrawString($"Table No : {DTVouMain.Rows[0]["TableName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 155, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Time    : {Convert.ToDateTime(DTVouMain.Rows[0]["VTime"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(157, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Customer : {DTVouMain.Rows[0]["GlDesc"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;
            e.Graphics.DrawString($"Waiter : {DTVouMain.Rows[0]["WaiterName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Waiter : ", myFont).Height;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
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

    private void PrintSalesBillDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT SIM.SB_Invoice,SIM.Invoice_Date,SIM.Invoice_Miti,SIM.Invoice_Time,GL.GlCode,GL.ACCode,Case When (SIM.Party_Name IS NULL or SIM.Party_Name='') Then GL.GLName Else SIM.Party_Name End GLName,GL.GlType,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,SIM.Vat_No,SIM.Cls1,SIM.Remarks,SIM.Is_Printed,IsNull(SIM.No_Print,0) No_Print,SIM.Payment_Mode,SIM.Tender_Amount,SIM.Return_Amount,TableCode,TableName ,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM  ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id Left Outer Join AMS.SO_Master as SOM on SIM.SO_Invoice=SOM.SO_Invoice Left Outer Join AMS.TableMaster as RTM on SOM.TableId=RTM.TableId LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId}  ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;
                Query =
                    "Select Distinct SIM.SB_Invoice,SID.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName,SID.Qty,SID.Rate,SID.B_Amount,SID.T_Amount,SID.N_Amount From AMS.SB_Details as SID Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId=SID.P_Id Left Outer Join AMS.Godown as G On G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.VNo='{VouNo}'  and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;
                ColHeaders.Clear();
                ColHeaders.Add("SN.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                //ColHeaders.Add("");//Unit
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(105);
                ColWidths.Add(35);
                ColWidths.Add(40);
                ColWidths.Add(50);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 15;
                var iCount = 0;
                PrintSalesBillHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    iCellHeight = 15;

                    //Draw Columns Contents
                    if (DTVouDetails.Rows[i]["PName"].ToString().Length > 80)
                        iCellHeight = 60;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 60)
                        iCellHeight = 50;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 40)
                        iCellHeight = 40;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 20)
                        iCellHeight = 30;
                    else
                        iCellHeight = 15;

                    myFont = new Font("Arial", 7, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        var PDesc = string.Empty;
                        PDesc = DTVouDetails.Rows[i]["PName"].ToString();
                        strFormat.Alignment = StringAlignment.Near;
                        if (PDesc.Length <= 20)
                        {
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], 15), strFormat);
                        }
                        else
                        {
                            var Pheight = iTopMargin;
                            if (PDesc.Length > 40 && PDesc.Length < 60)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(20, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15 + 15;
                                e.Graphics.DrawString(PDesc.Substring(40, PDesc.Length - 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else if (PDesc.Length > 20 && PDesc.Length < 40)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(20, PDesc.Length - 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else
                            {
                                strFormat.Alignment = StringAlignment.Near;
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont,
                                    Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                        }
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()).ToString("0.00"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Basic Amount : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, 300, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString("0.0"), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    //e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[3], (float)iTopMargin, (int)arrColumnWidths[3], (float)iCellHeight), strFormat);
                    e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString("0.0"), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                iTopMargin -= 5;
                e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString("0.0"), myFont,
                    Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Tender Amount : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString("", myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(DTVouMain.Rows[0]["Tender_Amount"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[5], (float)iTopMargin, (int)arrColumnWidths[5], (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Tender Amount", myFont).Height) + 5;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Change Amount : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString("", myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(DTVouMain.Rows[0]["Return_Amount"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[5], (float)iTopMargin, (int)arrColumnWidths[5], (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Change Amount", myFont).Height) + 5;

            //strFormat.Alignment = StringAlignment.Near;
            //myFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            //e.Graphics.DrawString("-----------------------------------------------------------------", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 33)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 33)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(33, AmountWords.Length - 33)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Cashier :  {ObjGlobal.LogInUser}                    Sign if any Credit", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 15;
            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Thank you for being our valuable customer.", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesBillHalfHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 10;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            if (DTCD.Rows[0]["Pan_No"] != null && DTCD.Rows[0]["Pan_No"].ToString() != string.Empty)
                e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Invoice Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()} {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
            e.Graphics.DrawString($"Invoice Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            e.Graphics.DrawString($"Table No/Room : {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 400, iCellHeight), strFormat);
            e.Graphics.DrawString($"Guest Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(400, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["PanNo"] != null && DTVouMain.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Customer's PAN/VAT No     : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(20, iTopMargin, 650, iCellHeight), strFormat);
                //if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != "")
                //    e.Graphics.DrawString("Address  : " + DTVouMain.Rows[0]["Address"].ToString() + "", myFont, Brushes.Black, new RectangleF(20, (float)iTopMargin, (int)650, (float)iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }

            //if (DTVouMain.Rows[0]["TelNoI"] != null && DTVouMain.Rows[0]["TelNoI"].ToString() != "")
            //{//" + DTVouMain.Rows[0]["TelNoI"] + "
            e.Graphics.DrawString($"Mode Of Payment : {DTVouMain.Rows[0]["Payment_Mode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 650, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            //}

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesBillHalfDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Payment_Mode,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName,'' TableCode FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(425);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesBillHalfHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 800, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 800, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ",
                myFont, Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 8;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            ////e.Graphics.DrawString("           " + CObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {DTCD.Rows[0]["Company_Name"]} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesBillA5Header(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 10;

            myFont = new Font("Arial", 9, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["No_Print"].ToString()))
            {
                if (Convert.ToInt16(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["No_Print"].ToString())) >
                    0) //DUBLICATE COPY
                {
                    e.Graphics.DrawString("INVOICE ", myFont, Brushes.Black,
                        new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString("- COPY OF ORIGINAL", myFont, Brushes.Black,
                        new RectangleF(20, iTopMargin, 450, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                        new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
                }
            }
            else
            {
                e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            if (DTCD.Rows[0]["Pan_No"] != null && DTCD.Rows[0]["Pan_No"].ToString() != string.Empty)
                e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Invoice Date   : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()} {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;

            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString($"Invoice Miti : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            e.Graphics.DrawString($"Table No/Room : {DTVouMain.Rows[0]["TableCode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 200, iCellHeight), strFormat);
            e.Graphics.DrawString($"Guest Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(200, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["PanNo"] != null && DTVouMain.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Customer's PAN/VAT No     : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
                //if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != "")
                //    e.Graphics.DrawString("Address  : " + DTVouMain.Rows[0]["Address"].ToString() + "", myFont, Brushes.Black, new RectangleF(20, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            }

            //if (DTVouMain.Rows[0]["TelNoI"] != null && DTVouMain.Rows[0]["TelNoI"].ToString() != "")
            //{//" + DTVouMain.Rows[0]["TelNoI"] + "
            e.Graphics.DrawString($"Mode Of Payment : {DTVouMain.Rows[0]["Payment_Mode"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 350, iCellHeight), strFormat);
            if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["No_Print"].ToString()))
                if (Convert.ToInt16(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["No_Print"].ToString())) >
                    0) //DUBLICATE COPY
                    e.Graphics.DrawString($"Copy Of Print :{DTVouMain.Rows[0]["No_Print"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(350, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            //}

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
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

    private void PrintSalesBillA5Details(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Payment_Mode,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName,'' TableCode FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(200);
                ColWidths.Add(55);
                ColWidths.Add(40);
                ColWidths.Add(70);
                ColWidths.Add(100);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesBillA5Header(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ",
                myFont, Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 8;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            ////e.Graphics.DrawString("           " + CObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {DTCD.Rows[0]["Company_Name"]} ", myFont, Brushes.Black,
                new RectangleF(50, iTopMargin, 450, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesBillA5PANDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Payment_Mode,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName,'' TableCode FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(200);
                ColWidths.Add(55);
                ColWidths.Add(40);
                ColWidths.Add(70);
                ColWidths.Add(100);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesBillA5Header(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("----------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ",
                myFont, Brushes.Black, new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 8;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            ////e.Graphics.DrawString("           " + CObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)550, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {DTCD.Rows[0]["Company_Name"]} ", myFont, Brushes.Black,
                new RectangleF(50, iTopMargin, 450, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesTaxInvoiceA5Header(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 9, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Fristprintcopy)
            {
                if (!string.IsNullOrEmpty(DTVouMain.Rows[0]["No_Print"].ToString()))
                {
                    if (Convert.ToInt16(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["No_Print"].ToString())) >
                        0) //DUBLICATE COPY
                        e.Graphics.DrawString(
                            $"TAX INVOICE - COPY OF ORIGINAL  {Convert.ToInt16(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["No_Print"].ToString()))}",
                            myFont, Brushes.Black, new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString("TAX INVOICE", myFont, Brushes.Black,
                            new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                        new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
                }
            }
            else
            {
                e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("TAX INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN / VAT No", myFont).Height + 15;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Retailer Name : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString($"Invoice No: {DTVouMain.Rows[0]["SB_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Retailer Name : ", myFont).Height + 5;

            e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Invoice Date: {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()} {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            e.Graphics.DrawString($"Retailer's PAN/VAT No : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString("Vehicle Name: ", myFont, Brushes.Black,
                new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);

            if (DTVouMain.Rows[0]["AgentName"].ToString() != string.Empty)
            {
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
                e.Graphics.DrawString($"Sales Man : {DTVouMain.Rows[0]["AgentName"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Sales Man : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
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

    private void PrintSalesTaxInvoiceA5Details(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotPBasicAmt = 0;
                TotPTermAmt = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SB_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Payment_Mode,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SB_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SB_Details as SID  Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=SID.SB_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SB_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SB_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SB_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SB_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo";
                Query =
                    $"{Query} Where SIM.SB_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ( 'B') AND STD.Amount <> 0 Group By SIM.SB_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type"; //Term_Type in ('P',
                Query = $"{Query} ) as aa Group By SB_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Basic Amount");
                ColHeaders.Add("Scheme Disc");
                ColHeaders.Add("Net Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(30);
                ColWidths.Add(120);
                ColWidths.Add(50);
                ColWidths.Add(30);
                ColWidths.Add(40);
                ColWidths.Add(60);
                ColWidths.Add(60);
                ColWidths.Add(60);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesTaxInvoiceA5Header(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotPBasicAmt = TotPBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["T_Amount"].ToString() != null)
                    {
                        TotPTermAmt = TotPTermAmt + +Convert.ToDouble(DTVouDetails.Rows[i]["T_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["T_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["N_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["N_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["N_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "----------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysQtyFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotPBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotPTermAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "----------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        $"{Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat)} %",
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 8;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            e.Graphics.DrawString($"           {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ----------------------   ",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                "           Prepared By          " +
                "                                                 Issuing Signatory      ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + DTCD.Rows[0]["Company_Name"].ToString() + " ", myFont, Brushes.Black, new RectangleF(50, (float)iTopMargin, (int)450, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 10;

            e.Graphics.DrawString("Terms and Conditions :", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
            e.Graphics.DrawString("1. Goods once sold are not returnable.", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
            e.Graphics.DrawString("2. Without officeal receipts the payment of this bill will not be valid.",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 15;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion Sales Bill Default Design

    #endregion ---------------------------------------- Sales Bill Design ----------------------------------------

    #region ----------------------------------------Sales Bill Return Design ----------------------------------------

    #region ---------------------------------------- Sales Bill Return Default Design ----------------------------------------

    private void PrintSalesBillReturnHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

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
            e.Graphics.DrawString("SALES RETURN INVOICE", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES RETURN INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            e.Graphics.DrawString("Invoice No : ", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 650, iCellHeight), strFormat);
            e.Graphics.DrawString(DTVouMain.Rows[0]["SR_Invoice"] + string.Empty, myFont, Brushes.Black,
                new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            e.Graphics.DrawString("Invoice Date   : ", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 650, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString() +
                    string.Empty, myFont, Brushes.Black, new RectangleF(700, iTopMargin, 800, iCellHeight),
                    strFormat);
            else
                e.Graphics.DrawString(DTVouMain.Rows[0]["Invoice_Miti"] + string.Empty, myFont, Brushes.Black,
                    new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 1;

            if (DTVouMain.Rows[0]["SB_Invoice"] != null &&
                DTVouMain.Rows[0]["SB_Invoice"].ToString() != string.Empty)
            {
                e.Graphics.DrawString("Ref. No : ", myFont, Brushes.Black,
                    new RectangleF(600, iTopMargin, 650, iCellHeight), strFormat);
                e.Graphics.DrawString(DTVouMain.Rows[0]["SB_Invoice"] + string.Empty, myFont, Brushes.Black,
                    new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Ref. Inv No : ", myFont).Height + 5;

                if (DTVouMain.Rows[0]["SB_Date"] != null && DTVouMain.Rows[0]["SB_Date"].ToString() != string.Empty)
                {
                    e.Graphics.DrawString("Ref. Date : ", myFont, Brushes.Black,
                        new RectangleF(600, iTopMargin, 650, iCellHeight), strFormat);
                    if (ObjGlobal.SysDateType == "D")
                        e.Graphics.DrawString(DTVouMain.Rows[0]["SB_Date"] + string.Empty, myFont, Brushes.Black,
                            new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString(
                            ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["SB_Date"])
                                .ToShortDateString()) + string.Empty, myFont, Brushes.Black,
                            new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("Sales Inv No : ", myFont).Height + 1;
                }
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesBillReturnDetails(PrintPageEventArgs e)
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
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SR_Invoice,Invoice_Date,Invoice_Miti,SB_Invoice,SB_Date,GL.GlCode,GL.GLName,Address,PhoneNo,LandLineNo,Email,PanNo,Remarks,Is_Printed FROM AMS.SR_Master as SRM Inner Join AMS.GeneralLedger as GL on GL.GlId=SRM.Customer_Id ";
                Query = $"{Query} Where SRM.SR_Invoice>='{VouNo}' and SRM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SR_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SRM.SR_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SR_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SR_Master as SRM On SRM.SR_Invoice = STD.SR_VNo";
                Query =
                    $"{Query} Where SRM.SR_Invoice ='{VouNo}' and SRM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type = 'B' and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SRM.SR_Invoice,BT.ST_ID,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SR_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct SRM.SR_Invoice,P.PShortName,PName,GCode,GName,AltU.UnitCode AltUnit,U.UnitCode Unit,Alt_Qty,Qty,Rate,SRD.B_Amount,SRD.T_Amount,SRD.N_Amount From AMS.SR_Details as SRD Inner Join AMS.SR_Master as SRM On SRM.SR_Invoice=SRD.SR_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId = SRD.P_Id Left Outer Join AMS.Godown as G On G.GId = SRD.Gdn_Id Left Outer Join AMS.ProductUnit as AltU On AltU.UId = SRD.Alt_UnitId Left Outer Join AMS.ProductUnit as U On U.UId = SRD.Unit_Id ";
                Query = $"{Query} Where SRM.SR_Invoice='{VouNo}' and SRM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(35);
                ColWidths.Add(345);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesBillReturnHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["AltUnit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["AltUnit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesReturnHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 5;
            e.Graphics.DrawString(DTCD.Rows[0]["PhoneNo"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["PhoneNo"].ToString(), myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;

            e.Graphics.DrawString("SALES RETURN", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 250, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES RETURN", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Return No : {DTVouMain.Rows[0]["SR_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 155, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Date : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(157, iTopMargin, PageWidth, iCellHeight), strFormat);
            //e.Graphics.DrawString("Miti  : " + DTVouMain.Rows[0]["VMiti"].ToString() + "", myFont, Brushes.Black, new RectangleF(157, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 5;

            e.Graphics.DrawString(
                $"Time    : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(157, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Customer : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
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

    private void PrintSalesReturnDetails3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width;
            iLeftMargin = 0; // ((e.MarginBounds.Width * 3) / 100) ; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 10;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SRM.SR_Invoice,SRM.Invoice_Date,SRM.Invoice_Miti,SRM.Invoice_Time,L.GLCode,Case When (SRM.Party_Name IS NULL or SRM.Party_Name='') Then L.GlName Else SRM.Party_Name End GlName,L.GLType, Address,PhoneNo,Email,PanNo,SRM.Cls1,SRM.Remarks,SRM.No_Print,SRM.Payment_Mode,T_Amount,Return_Amount FROM AMS.SR_Master as SRM ";
                Query = $"{Query} Inner Join AMS.GeneralLedger as L on L.GlId=SRM.Customer_Id ";
                Query = $"{Query} Where SRM.SR_Invoice='{VouNo}' ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct SRM.SR_Invoice,SRD.Invoice_SNo,P.PShortName,PName,GCode,GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName,SRD.Qty,SRD.Rate,SRD.B_Amount,SRD.T_Amount,SRD.N_Amount From AMS.SR_Details as SRD Inner Join AMS.SR_Master as SRM On SRM.SR_Invoice=SRD.SR_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId=SRD.P_Id Left Outer Join AMS.Godown as G On G.GId= SRD.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SRD.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SRD.Unit_Id ";
                Query = $"{Query} Where SRM.SR_Invoice='{VouNo}' ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SR_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SRM.SR_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SR_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SR_Master as SRM On SRM.SR_Invoice = STD.SR_VNo";
                Query =
                    $"{Query} Where SRM.SR_Invoice = '{VouNo}' and SRM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ('P', 'B') AND STD.Amount <> 0 Group By SRM.SR_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type";
                Query = $"{Query} ) as aa Group By SR_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("SN.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                //ColHeaders.Add("");//Unit
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(105);
                ColWidths.Add(35);
                ColWidths.Add(40);
                ColWidths.Add(50);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                //Set the cell height
                iCellHeight = 15;
                var iCount = 0;
                PrintSalesReturnHeader3InchRollPaper(e);
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    iCellHeight = 15;

                    //Draw Columns Contents
                    if (DTVouDetails.Rows[i]["PName"].ToString().Length > 80)
                        iCellHeight = 60;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 60)
                        iCellHeight = 50;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 40)
                        iCellHeight = 40;
                    else if (DTVouDetails.Rows[i]["PName"].ToString().Length > 20)
                        iCellHeight = 30;
                    else
                        iCellHeight = 15;

                    myFont = new Font("Arial", 7, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        var PDesc = string.Empty;
                        PDesc = DTVouDetails.Rows[i]["PName"].ToString();
                        strFormat.Alignment = StringAlignment.Near;
                        if (PDesc.Length <= 20)
                        {
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], 15), strFormat);
                        }
                        else
                        {
                            var Pheight = iTopMargin;
                            if (PDesc.Length > 40 && PDesc.Length < 60)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(20, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15 + 15;
                                e.Graphics.DrawString(PDesc.Substring(40, PDesc.Length - 40), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else if (PDesc.Length > 20 && PDesc.Length < 40)
                            {
                                e.Graphics.DrawString(PDesc.Substring(0, 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                                Pheight = iTopMargin + 15;
                                e.Graphics.DrawString(PDesc.Substring(20, PDesc.Length - 20), myFont, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], Pheight,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                            else
                            {
                                strFormat.Alignment = StringAlignment.Near;
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont,
                                    Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                        (int)arrColumnWidths[iCount], 15), strFormat);
                            }
                        }
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()).ToString("0.00"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString()).ToString("0.0"), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    iRow++;
                    iTopMargin += iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Basic Amount : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, 300, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[2], (float)iTopMargin, (int)arrColumnWidths[2], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString("0.0"), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
            {
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    //e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[3], (float)iTopMargin, (int)arrColumnWidths[3], (float)iCellHeight), strFormat);
                    e.Graphics.DrawString(Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString("0.0"), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

                strFormat.Alignment = StringAlignment.Near;
                iTopMargin -= 5;
                e.Graphics.DrawString("-------------------------------------------------", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                    new RectangleF(70, iTopMargin, PageWidth, iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString("0.0"), myFont,
                    Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height;
            }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Tender Amount : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString("", myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(DTVouMain.Rows[0]["TenderAmt"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[5], (float)iTopMargin, (int)arrColumnWidths[5], (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Tender Amount", myFont).Height) + 5;

            //strFormat.Alignment = StringAlignment.Near;
            //e.Graphics.DrawString("Change Amount : ", myFont, Brushes.Black, new RectangleF((int)70, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString("", myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(DTVouMain.Rows[0]["ChangeAmt"].ToString()).ToString("0.0"), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[5], (float)iTopMargin, (int)arrColumnWidths[5], (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("Change Amount", myFont).Height) + 5;

            //strFormat.Alignment = StringAlignment.Near;
            //myFont = new System.Drawing.Font("Arial", 8, FontStyle.Regular);
            //e.Graphics.DrawString("-----------------------------------------------------------------", myFont, Brushes.Black, new RectangleF(0, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString());
            if (AmountWords.Length <= 33)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 33)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(33, AmountWords.Length - 33)} ", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-----------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Cashier :  {ObjGlobal.LogInUser}                    Sign if any Credit", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 15;
            strFormat.Alignment = StringAlignment.Center;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("Thank you for being our valuable customer.", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintSalesReturnInvoiceA5Header(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Company_Name"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Company_Name"].ToString(), myFont).Height +
                          5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont).Height + 5;

            myFont = new Font("Arial", 9, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString($"PAN/VAT No : {DTCD.Rows[0]["Pan_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 500, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN / VAT No", myFont).Height + 15;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Retailer Name : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString($"Return No: {DTVouMain.Rows[0]["SR_Invoice"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Retailer Name : ", myFont).Height + 5;

            e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Return Date: {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()} {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Time"].ToString()).ToShortTimeString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            e.Graphics.DrawString($"Retailer's PAN/VAT No : {DTVouMain.Rows[0]["PanNo"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            e.Graphics.DrawString("Vehicle Name: ", myFont, Brushes.Black,
                new RectangleF(300, iTopMargin, 550, iCellHeight), strFormat);

            if (DTVouMain.Rows[0]["AgentName"].ToString() != string.Empty)
            {
                iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
                e.Graphics.DrawString($"Sales Man : {DTVouMain.Rows[0]["AgentName"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(20, iTopMargin, 300, iCellHeight), strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Sales Man : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
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

    private void PrintSalesReturnInvoiceA5Details(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
            iRightMargin = PageWidth - iLeftMargin - 20; //Set the Right Margin
            iTopMargin = e.MarginBounds.Top; //Set the top margin
            iTopMargin = 30;

            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotPBasicAmt = 0;
                TotPTermAmt = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SR_Invoice,Invoice_Date,Invoice_Miti,Invoice_Time,Payment_Mode,GL.GLCode,GL.ACCode, GL.GLName,GL.GlAddress,GL.PhoneNo,LandLineNo,GL.Email,GL.PanNo,Remarks,Is_Printed,IsNull(No_Print,0) No_Print,SL.SLName, JA.AgentName FROM AMS.SR_Master as SIM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=SIM.Customer_Id LEFT OUTER JOIN AMS.SubLedger Sl ON Sl.SLId = SIM.Subledger_Id  LEFT OUTER JOIN AMS.JuniorAgent JA ON JA.AgentId = SIM.Agent_Id";
                Query = $"{Query} Where SIM.SR_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select SID.*,PName,PShortName ,G.GName,APU.UnitCode Alt_UnitCode,APU.UnitName as Alt_UnitName ,PU.UnitCode,PU.UnitName from AMS.SR_Details as SID  Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice=SID.SR_Invoice ";
                Query =
                    $"{Query} left outer join AMS.Product as P on P.PId= SID.P_Id left outer join AMS.Godown as G on G.GId= SID.Gdn_Id left outer join AMS.ProductUnit as APU on APU.UID= SID.Alt_UnitId left outer join AMS.ProductUnit as PU on PU.UID= SID.Unit_Id ";
                Query = $"{Query} Where SIM.SR_Invoice='{VouNo}' and SIM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select SR_Invoice, ST_Id, Order_No, ST_Name, Term_Rate, Sum(Term_Amount)Term_Amount From(Select Distinct SIM.SR_Invoice, BT.ST_Id, Order_No, ST_Name, Case When Term_Type= 'B' Then STD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When ST_Sign = '-' Then - Sum(STD.Amount) Else Sum(STD.Amount) End Term_Amount from AMS.ST_Term as BT Inner Join AMS.SR_Term as STD On BT.ST_Id = STD.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice = STD.SR_VNo";
                Query =
                    $"{Query} Where SIM.SR_Invoice = '{VouNo}' and SIM.CBranch_Id = {ObjGlobal.SysBranchId} and Term_Type in ( 'B') AND STD.Amount <> 0 Group By SIM.SR_Invoice,BT.ST_Id,Order_No,ST_Name,ST_Sign,Rate,Term_Type"; //Term_Type in ('P',
                Query = $"{Query} ) as aa Group By SR_Invoice,ST_Id,Order_No,ST_Name,Term_Rate Order By Order_No";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Basic Amount");
                ColHeaders.Add("Scheme Disc");
                ColHeaders.Add("Net Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(30);
                ColWidths.Add(120);
                ColWidths.Add(50);
                ColWidths.Add(30);
                ColWidths.Add(40);
                ColWidths.Add(60);
                ColWidths.Add(60);
                ColWidths.Add(60);
            }

            long LCnt;
            LCnt = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    iCellHeight = 15;
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
                        PrintSalesReturnInvoiceA5Header(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
                    iCount = 0;
                    //Draw Columns Contents

                    myFont = new Font("Arial", 8, FontStyle.Regular);

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["PName"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PName"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["UnitCode"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["UnitCode"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["B_Amount"].ToString() != null)
                    {
                        TotPBasicAmt = TotPBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["B_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["B_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["T_Amount"].ToString() != null)
                    {
                        TotPTermAmt = TotPTermAmt + +Convert.ToDouble(DTVouDetails.Rows[i]["T_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["T_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["N_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["N_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["N_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "----------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);

            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, (int)arrColumnWidths[1], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysQtyFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotPBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotPTermAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "----------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    if (ObjGlobal.SalesVatTermId == Convert.ToInt16(DTRo["ST_Id"].ToString()))
                    {
                        e.Graphics.DrawString("Taxable Amount : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4],
                                iCellHeight), strFormat);
                        e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7],
                                iCellHeight), strFormat);

                        iTopMargin += (int)e.Graphics
                            .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["ST_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        $"{Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat)} %",
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["ST_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 3;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Math.Round(TotGrandAmt).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 3;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"In Words : {ClsMoneyConversion.MoneyConversion(Math.Round(TotGrandAmt).ToString())} ", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 8;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;

            e.Graphics.DrawString($"           {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ----------------------   ",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                "           Prepared By          " +
                "                                                 Issuing Signatory      ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 550, iCellHeight), strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + DTCD.Rows[0]["Company_Name"].ToString() + " ", myFont, Brushes.Black, new RectangleF(50, (float)iTopMargin, (int)450, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 15;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion ---------------------------------------- Sales Bill Return Default Design ----------------------------------------

    #endregion ----------------------------------------Sales Bill Return Design ----------------------------------------

    #region Sales Bill ExBr Return Design

    #region Sales Bill ExBr Return Default Design

    private void PrintSalesBillExBrReturnHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape)
                LeftMargin = LeftMargin + 200;

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
            e.Graphics.DrawString("SALES EX/BR RETURN INVOICE", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("SALES EX/BR RETURN INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"ExBr Invoice No : {DTVouMain.Rows[0]["ExpiryBreakage_No"]}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["ExpiryBreakage_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["ExpiryBreakage_BSDate"]}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Far;
            if (DTVouMain.Rows[0]["RefBill_No"] != null &&
                DTVouMain.Rows[0]["RefBill_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Ref. Bill No : {DTVouMain.Rows[0]["RefBill_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("RefBill No : ", myFont).Height + 5;

                if (DTVouMain.Rows[0]["RefBill_Date"] != null &&
                    DTVouMain.Rows[0]["RefBill_Date"].ToString() != string.Empty)
                {
                    if (ObjGlobal.SysDateType == "AD")
                        e.Graphics.DrawString(
                            $"RefBill Date(AD) : {DTVouMain.Rows[0]["RefBill_Date"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString(
                            $"RefBill Date(BS) : {ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["RefBill_Date"]).ToShortDateString())}{string.Empty}",
                            myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("RefBill No : ", myFont).Height + 1;
                }
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right")
                    strFormat.Alignment = StringAlignment.Far;

                iHeaderHeight = (int)e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString())).Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
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

    private void PrintSalesBillExBrReturnDetails(PrintPageEventArgs e)
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
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT ExpiryBreakage_No,ExpiryBreakage_Date,ExpiryBreakage_BsDate,RefBill_No,RefBill_Date,L.Ledger_Code,L.Ledger_Name,Country,State,City, ";
                Query =
                    $"{Query} Address,Phone_No,AltPhone_No,Fax_No,Email_Id,Pan_No,Remarks,Printed FROM AMS.SalesExpiryBreakage_Main as SRM ";
                Query = $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=SRM.Ledger_Code ";
                Query =
                    $"{Query} Where SRM.ExpiryBreakage_No='{VouNo}' and SRM.Branch_Id={ObjGlobal.SysBranchId} and SRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct SRM.SalesReturn_Invoice_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then STD.Term_Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(STD.Term_Amount) Else  Sum(STD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT Inner Join AMS.SalesTerm_Details as STD On BT.BT_Id=STD.Term_Id Inner Join AMS.SalesExpiryBreakage_Main as SRM On SRM.ExpiryBreakage_No=STD.Invoice_No ";
                Query =
                    $"{Query} Where SRM.ExpiryBreakage_No='{VouNo}' and SRM.Branch_Id={ObjGlobal.SysBranchId} and SRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=2 and Module='SEB' and BillTerm_Type in (1,2) AND STD.Term_Amount<>0 Group By SRM.ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type ";
                Query =
                    $"{Query} ) as aa Group By ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct SRM.ExpiryBreakage_No,P.Product_Code,Product_Name,Gdn_Code,Gdn_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,SRD.Basic_Amount, ";
                Query =
                    $"{Query} SRD.Term_Amount,SRD.Net_Amount From AMS.SalesExpiryBreakage_Details as SRD Inner Join AMS.SalesExpiryBreakage_Main as SRM On SRM.ExpiryBreakage_No=SRD.ExpiryBreakage_No ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.Product_Id=SRD.Product_Id Left Outer Join AMS.Godown as G On G.Gdn_Id=SRD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=SRD.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=SRD.Unit_Id";
                Query =
                    $"{Query} Where SRM.ExpiryBreakage_No='{VouNo}' and SRM.Branch_Id={ObjGlobal.SysBranchId} and SRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Alt Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Qty");
                ColHeaders.Add("Unit");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(35);
                ColWidths.Add(345);
                ColWidths.Add(60);
                ColWidths.Add(40);
                ColWidths.Add(65);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            //Loop till all the grid rows not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < DTVouDetails.Rows.Count; i++)
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
                        PrintSalesBillExBrReturnHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            iCellHeight), strFormat);
                    iCount++;

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Alt_Qty"].ToString() != null)
                    {
                        TotAltQty = TotAltQty + Convert.ToDouble(DTVouDetails.Rows[i]["Alt_Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Alt_Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["AltUnit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["AltUnit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Center;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                iCellHeight), strFormat);
                    }

                    iCount++;

                    if (DTVouDetails.Rows[i]["Basic_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(DTVouDetails.Rows[i]["Basic_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(DTVouDetails.Rows[i]["Basic_Amount"].ToString())
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

            if (LCnt < N_Line - N_TermDet - 2)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 2; ln++)
                {
                    e.Graphics.DrawString(" ", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                        myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics
                        .MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont).Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat),
                myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString())} ", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion Sales Bill ExBr Return Default Design

    #endregion Sales Bill ExBr Return Design
}