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

public class DocPrintingPurchaseInvoice
{
    public string PrintDoc(bool print)
    {
        _document.BeginPrint += printDocument1_BeginPrint;
        _document.PrintPage += printDocument1_PrintPage;

        _previewDialog.Click += printPreviewDialog1_Click;
        Printed = false;

        if (Convert.ToInt16(NoOf_Copy) > 0)
        {
            GetConnection.SelectDataTableQuery("SELECT Company_Name,Address,Country,City,State ,PhoneNo,Pan_No,Email,Website  FROM AMS.CompanyInfo");

            for (var Noc = 1; Noc <= Convert.ToInt16(NoOf_Copy); Noc++)
            {
                //PageNo = 0;
                _document.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                _document.PrinterSettings.PrinterName = Printer_Name;
                if (DocDesign_Name is "A4 Full" or "Receipt Voucher" or "Payment Voucher")
                {
                    N_Line = 30;
                    //printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.A4.ToString();
                    _document.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                }
                else if (DocDesign_Name == "A4 Half")
                {
                    N_Line = 5;
                    _document.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                }
                else
                {
                    _document.DefaultPageSettings.PaperSize.PaperName = DocDesign_Name switch
                    {
                        "LETTER FULL" => PaperKind.Letter.ToString(),
                        "DefaultBarcode" => PaperKind.Letter.ToString(),
                        _ => _document.DefaultPageSettings.PaperSize.PaperName
                    };
                }

                Query = string.Empty;
                VouNo = string.Empty;
                if (Module == "PQ")
                {
                    Query = "SELECT PQ_Invoice Voucher_No FROM AMS.PQ_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and PQ_Invoice>='{FromDocNo}' and PQ_Invoice<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "PO")
                {
                    Query = "SELECT PO_Invoice Voucher_No FROM AMS.PO_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and PO_Invoice>='{FromDocNo}' and PO_Invoice<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "POC")
                {
                    Query = "SELECT POC_Invoice Voucher_No FROM AMS.POC_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and OrderCancel_No>='{FromDocNo}' and OrderCancel_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "PC")
                {
                    Query = "SELECT PC_Invoice Voucher_No FROM AMS.PC_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} Where Challan_No>='{FromDocNo}' and Challan_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "PB")
                {
                    Query = "SELECT PB_Invoice as Voucher_No FROM AMS.PB_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and PB_Invoice>='{FromDocNo}' and PB_Invoice<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "PAB")
                {
                    Query = "SELECT PAB_Invoice Voucher_No FROM AMS.VmPabMaster Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and PAB_Invoice>='{FromDocNo}' and PAB_Invoice<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "PR")
                {
                    Query = "SELECT PR_Invoice Voucher_No FROM AMS.PR_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and PR_Invoice>='{FromDocNo}' and PR_Invoice<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Module == "PEB")
                {
                    Query = "SELECT PEB_Invoice Voucher_No FROM AMS.PEB_Master Where 1=1 ";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and PEB_Invoice>='{FromDocNo}' and PEB_Invoice<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "D" => $"{Query} and Invoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Invoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
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
                            _document.PrintController =
                                _controller; //User Printing page dialog press enter enter(2 times) button then cancel print voucher
                            _document.Print();
                        }
                        else
                        {
                            _previewDialog.Document = _document;
                            _previewDialog.PrintPreviewControl.Zoom = 1;
                            ((Form)_previewDialog).WindowState = FormWindowState.Maximized;
                            _previewDialog.ShowDialog();
                        }
                    }
                }
            }

            if (print)
            {
                //foreach (DataRow VNro in DTNoOfVou.Rows)
                //{
                //    VouNo = VNro["Voucher_No"].ToString();
                //    dt.Reset();
                //    Query = "Insert Into AMS.Print_Voucher Values ('" + VouNo + "', '" + Module + "', " + NoOf_Copy + ", " + ObjGlobal._User_Id + ",getdate(), " + ObjGlobal._Branch_Id + "," + ObjGlobal._FiscalYear_Id + ")";
                //    dt = GetConnection.SelectDataTableQuery(Query);
                //}
                //dt.Reset();
                //if (Module == "PB")
                //{
                //    Query = "Update AMS.PurchaseInvoice_Main Set Printed=1 Where FiscalYear_Id=" + ObjGlobal._FiscalYear_Id + " and ('0'='" + ObjGlobal._Branch_Id + "' or Branch_Id=" + ObjGlobal._Branch_Id + ")";
                //    if (FromDocNo.Trim() != "" && ToDocNo.Trim() != "")
                //        Query = Query + " and Invoice_No>='" + FromDocNo + "' and Invoice_No<='" + ToDocNo + "'";
                //    else
                //    {
                //        if (ObjGlobal._Date_Type == "D")
                //            Query = Query + " and Invoice_Date between '" + FromDate + "' and '" + ToDate + "'";
                //        else
                //            Query = Query + " and Invoice_Date between '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) + "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)) + "'";
                //    }
                //    dt = ObjGlobal.ClsMoneyConversion(Query);
                //}
            }
        }

        Msg = print switch
        {
            true => "Print Completed!",
            _ => Msg
        };
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
        if (Module == "PQ")
            PrintPurchaseQuotationDetails(e);
        else if (Module == "PO")
            PrintPurchaseOrderDetails(e);
        else if (Module == "PC")
            PrintPurchaseChallanDetails(e);
        else if (Module == "PB")
            PrintPurchaseBillDetails(e);
        else if (Module == "PR")
            PrintPurchaseReturnBillDetails(e);
        else if (Module == "PEB") PrintPurchaseExBrReturnBillDetails(e);
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        _previewDialog.Document = _document;
        _previewDialog.ShowDialog();
    }

    #region Global

    private readonly System.Drawing.Printing.PrintDocument _document = new();
    private readonly PrintPreviewDialog _previewDialog = new();
    private readonly PrintController _controller = new StandardPrintController();
    private DataTable dt = new();
    private DataTable DTTemp = new();
    private DataTable DTNoOfVou = new();
    private DataTable DTVouMain = new();
    private DataTable DTVouDetails = new();
    private DataTable DTTermDetails = new();
    private DataTable DTProdTerm = new();
    private DataTable DTBillTerm = new();
    private DataSet ds = new();
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.str' is never used
    private string str;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.str' is never used
    private string Query;
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.FromAdDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.ToADDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.TotDrAmt' is never used
    private double TotDrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.TotDrAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.TotCrAmt' is never used
    private double TotCrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.TotCrAmt' is never used
    private double TotAltQty;
    private double TotQty;
    private double TotBasicAmt;
#pragma warning disable CS0414 // The field 'DocPrinting_PurchaseInvoice.TotNetAmt' is assigned but its value is never used
    private double TotNetAmt;
#pragma warning restore CS0414 // The field 'DocPrinting_PurchaseInvoice.TotNetAmt' is assigned but its value is never used
    private double TotGrandAmt;
    private Font myFont = new("Arial", 10);
#pragma warning disable CS0414 // The field 'DocPrinting_PurchaseInvoice.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_PurchaseInvoice.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_PurchaseInvoice.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_PurchaseInvoice.rowPosition' is assigned but its value is never used
    private int N_Line;
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.N_FootLine' is never used
    private int N_TermDet;
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.RemarksTotLen' is never used
    private int RemarksTotLen;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.RemarksTotLen' is never used
    private StringFormat strFormat; //Used to format the grid rows.
    private readonly ArrayList ColHeaders = new();
    private readonly ArrayList ColWidths = new();
    private readonly ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList arrColumnWidths = new(); //Used to save column widths
    private int iCellHeight; //Used to get/set the datagridview cell height
    public int PageWidth;
    public int PageHeight;
    public long PageNo;
    private long iRow; //Used as counter
#pragma warning disable CS0414 // The field 'DocPrinting_PurchaseInvoice.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_PurchaseInvoice.iCount' is assigned but its value is never used
    private bool bFirstPage; //Used to check whether we are printing first page
    private bool bNewPage; // Used to check whether we are printing a new page
    private int iHeaderHeight; //Used for the header height
    private bool bMorePagesToPrint;
    private int iLeftMargin;
    private int iRightMargin;
    private int iTopMargin;
#pragma warning disable CS0414 // The field 'DocPrinting_PurchaseInvoice.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_PurchaseInvoice.iTmpWidth' is assigned but its value is never used
    private int i;
    private int DRo;
    private string VouNo = string.Empty;
#pragma warning disable CS0169 // The field 'DocPrinting_PurchaseInvoice.CashVoucher' is never used
    private bool CashVoucher;
#pragma warning restore CS0169 // The field 'DocPrinting_PurchaseInvoice.CashVoucher' is never used
    private string Msg = string.Empty;
#pragma warning disable CS0414 // The field 'DocPrinting_PurchaseInvoice.Printed' is assigned but its value is never used
    private bool Printed;
#pragma warning restore CS0414 // The field 'DocPrinting_PurchaseInvoice.Printed' is assigned but its value is never used

    public string FrmName { get; set; }

    public string Module { get; set; }

    public string ClsMoneyConversion { get; set; }

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

    #region Purchase Quotation Design

    #region Purchase Quotation Default Design

    private void PrintPurchaseQuotationHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = _document.DefaultPageSettings.Landscape switch
            {
                true => LeftMargin + 200,
                _ => iLeftMargin + 650
            };

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
            e.Graphics.DrawString("PURCHASE QUOTATION", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PURCHASE QUOTATION", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name     : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);

            e.Graphics.DrawString($"Quotation No     : {DTVouMain.Rows[0]["Quotation_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Quotation No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address        : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);

            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)       : {Convert.ToDateTime(DTVouMain.Rows[0]["Quotation_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)       : {DTVouMain.Rows[0]["Quotation_BSDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(650, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["Pan_No"] != null && DTVouMain.Rows[0]["Pan_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"PAN/VAT No     : {DTVouMain.Rows[0]["Pan_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Quotation No : ", myFont).Height + 5;
            }

            if (DTVouMain.Rows[0]["Phone_No"] != null && DTVouMain.Rows[0]["Phone_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Phone No       : {DTVouMain.Rows[0]["Phone_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 650, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 1;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                strFormat.Alignment = ColFormat[i].ToString() switch
                {
                    "Left" => StringAlignment.Near,
                    _ => ColFormat[i].ToString() switch
                    {
                        "Left" => StringAlignment.Center,
                        "Right" => StringAlignment.Far,
                        _ => strFormat.Alignment
                    }
                };

                iHeaderHeight = (int)e.Graphics
                                    .MeasureString(ColHeaders[i].ToString(), myFont,
                                        Convert.ToInt16(ColWidths[i].ToString())).Height +
                                1;
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

    private void PrintPurchaseQuotationDetails(PrintPageEventArgs e)
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
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT Quotation_No,Quotation_Date,Quotation_BsDate,L.Ledger_Code,L.Ledger_Name,Country,State,City, ";
                Query =
                    $"{Query} Address,Phone_No,AltPhone_No,Fax_No,Email_Id,Pan_No,Remarks FROM AMS.PurchaseQuotation_Main as PIM ";
                Query = $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=PIM.Ledger_Code ";
                Query =
                    $"{Query} Where PIM.Quotation_No='{VouNo}' and PIM.Branch_Id={ObjGlobal.SysBranchId} and PIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Invoice_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct PIM.Quotation_No Invoice_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then PTD.Term_Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(PTD.Term_Amount) Else  Sum(PTD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT Inner Join AMS.PurchaseTerm_Details as PTD On BT.BT_Id=PTD.Term_Id Inner Join AMS.PurchaseQuotation_Main as PIM On PIM.Quotation_No=PTD.Invoice_No ";
                Query =
                    $"{Query} Where PIM.Quotation_No='{VouNo}' and PIM.Branch_Id={ObjGlobal.SysBranchId} and PIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=1 and Module='PQ' and BillTerm_Type in (1,2) AND PTD.Term_Amount<>0 Group By PIM.Quotation_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type ";
                Query =
                    $"{Query} ) as aa Group By Invoice_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct PIM.Quotation_No,P.Product_Code,Product_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,PID.Basic_Amount Basic_Amount, ";
                Query =
                    $"{Query} PID.Term_Amount,PID.Net_Amount From AMS.PurchaseQuotation_Details as PID Inner Join AMS.PurchaseQuotation_Main as PIM On PIM.Quotation_No=PID.Quotation_No ";
                Query = $"{Query} Inner Join AMS.Product as P On P.Product_Id=PID.Product_Id";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=PID.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=PID.Unit_Id";
                Query =
                    $"{Query} Where PIM.Quotation_No='{VouNo}' and PIM.Branch_Id={ObjGlobal.SysBranchId} and PIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
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
                        PrintPurchaseQuotationHeader(e);
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

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont, Brushes.Black,
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
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
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
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

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
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            // e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
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

    #endregion Purchase Quotation Default Design

    #endregion Purchase Quotation Design

    #region Purchase Order Design

    #region Purchase Order Default Design

    private void PrintPurchaseOrderHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = _document.DefaultPageSettings.Landscape switch
            {
                true => LeftMargin + 200,
                _ => iLeftMargin + 650
            };

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
            e.Graphics.DrawString("PURCHASE ORDER", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PURCHASE ORDER", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["GlName"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Order No : {DTVouMain.Rows[0]["PO_Invoice"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Order No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["GlAddress"] != null && DTVouMain.Rows[0]["GlAddress"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["GlAddress"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "D")
                e.Graphics.DrawString(
                    $"Order Date  : {Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Order Miti   : {DTVouMain.Rows[0]["Invoice_Miti"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Far;
            if (DTVouMain.Rows[0]["PB_VNo"] != null && DTVouMain.Rows[0]["PB_VNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Order A/C No : {DTVouMain.Rows[0]["PB_VNo"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

                if (DTVouMain.Rows[0]["VNo_Date"] != null && DTVouMain.Rows[0]["VNo_Date"].ToString() != string.Empty)
                {
                    if (ObjGlobal.SysDateType == "A")
                        e.Graphics.DrawString($"Order A/c Date : {DTVouMain.Rows[0]["VNo_Date"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString($"Order A/c Miti : {DTVouMain.Rows[0]["VNo_Miti"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString("Order No : ", myFont).Height + 1;
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
                strFormat.Alignment = ColFormat[i].ToString() switch
                {
                    "Left" => StringAlignment.Near,
                    _ => ColFormat[i].ToString() switch
                    {
                        "Left" => StringAlignment.Center,
                        "Right" => StringAlignment.Far,
                        _ => strFormat.Alignment
                    }
                };

                iHeaderHeight = (int)e.Graphics
                                    .MeasureString(ColHeaders[i].ToString(), myFont,
                                        Convert.ToInt16(ColWidths[i].ToString())).Height +
                                1;
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

    private void PrintPurchaseOrderDetails(PrintPageEventArgs e)
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
                    "SELECT PO_Invoice,Invoice_Date,Invoice_Miti,PB_VNo,Vno_Date,Vno_Miti,GL.GlCode,GL.GlName,GlAddress,GL.PhoneNo,LandLineNo,GL.Email,PanNo,Party_Name,Vat_No,AgentName,AgentCode,Remarks FROM AMS.PO_Master as POM ";
                Query =
                    $"{Query} Inner Join AMS.GeneralLedger as GL on GL.GlId=POM.Vendor_Id Left Outer Join AMS.JuniorAgent as JA On JA.AgentId=POM.Agent_Id ";
                Query = $"{Query} Where POM.PO_Invoice='{VouNo}' and POM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Distinct POM.PO_Invoice,POD.Invoice_SNo,P.PShortName,PName,GCode,GName,AltU.UnitCode AltUnit,U.UnitCode Unit,Alt_Qty,Qty,Rate,POD.B_Amount, ";
                Query =
                    $"{Query} POD.T_Amount,POD.N_Amount From AMS.PO_Details as POD Inner Join AMS.PO_Master as POM On POM.PO_Invoice=POD.PO_Invoice ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.PId=POD.P_Id Left Outer Join AMS.Godown as G On G.GId=POD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.ProductUnit as AltU On AltU.UID=POD.Alt_UnitId Left Outer Join AMS.ProductUnit as U On U.UID=POD.Unit_Id";
                Query = $"{Query} Where POM.PO_Invoice='{VouNo}' and POM.CBranch_Id={ObjGlobal.SysBranchId} ";
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "Select PO_Invoice,PT_Id,Order_No,PT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct POM.PO_Invoice,BT.PT_Id,Order_No,PT_Name,Case When Term_Type='B' Then PTD.Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When PT_Sign='-' Then -Sum(PTD.Amount) Else  Sum(PTD.Amount) End Term_Amount from AMS.PT_Term as BT Inner Join AMS.PO_Term as PTD On BT.PT_ID=PTD.PT_Id Inner Join AMS.PO_Master as POM On POM.PO_Invoice=PTD.PO_VNo  ";
                Query =
                    $"{Query} Where POM.PO_Invoice='{VouNo}' and POM.CBranch_Id={ObjGlobal.SysBranchId}  and Term_Type in ('P','B') AND PTD.Amount<>0 ";
                Query = $"{Query} Group By POM.PO_Invoice,BT.PT_Id,Order_No,PT_Name,PT_Sign,Rate,Term_Type ";
                Query = $"{Query} ) as aa Group By PO_Invoice,PT_Id,Order_No,PT_Name,Term_Rate Order By Order_No";
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

            //Loop till all the Details Items not get printed
            if (DTVouDetails.Rows.Count > 0)
            {
                var i = 0;
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
                        PrintPurchaseOrderHeader(e);
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
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (DTTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in DTTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["PT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["PT_Name"] + string.Empty, myFont)
                        .Height + 5;

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
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            //e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
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

    #endregion Purchase Order Default Design

    #endregion Purchase Order Design

    #region Purchase Challan

    private void PrintPurchaseChallanHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = _document.DefaultPageSettings.Landscape switch
            {
                true => LeftMargin + 200,
                _ => iLeftMargin + 650
            };

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
            e.Graphics.DrawString("PURCHASE CHALLAN", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PURCHASE CHALLAN", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Challan No : {DTVouMain.Rows[0]["Challan_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Challan No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["Challan_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["Challan_BSDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                strFormat.Alignment = ColFormat[i].ToString() switch
                {
                    "Left" => StringAlignment.Near,
                    _ => ColFormat[i].ToString() switch
                    {
                        "Left" => StringAlignment.Center,
                        "Right" => StringAlignment.Far,
                        _ => strFormat.Alignment
                    }
                };

                iHeaderHeight = (int)e.Graphics
                                    .MeasureString(ColHeaders[i].ToString(), myFont,
                                        Convert.ToInt16(ColWidths[i].ToString())).Height +
                                1;
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

    private void PrintPurchaseChallanDetails(PrintPageEventArgs e)
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
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT Challan_No,Challan_Date,Challan_BsDate,L.Ledger_Code,L.Ledger_Name,Country,State,City,Address,Phone_No,AltPhone_No,Fax_No,Email_Id,Pan_No,Remarks ";
                Query =
                    $"{Query} FROM AMS.PurchaseChallan_Main as PCM Inner Join AMS.Ledger as L on L.Ledger_Code=PCM.Ledger_Code  Where PCM.Challan_No='{VouNo}' and PCM.Branch_Id={ObjGlobal.SysBranchId} and PCM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Challan_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct PCM.Challan_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then PTD.Term_Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(PTD.Term_Amount) Else  Sum(PTD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT Inner Join AMS.PurchaseTerm_Details as PTD On BT.BT_Id=PTD.Term_Id Inner Join AMS.PurchaseChallan_Main as PCM On PCM.Challan_No=PTD.Invoice_No ";
                Query =
                    $"{Query} Where PCM.Challan_No='{VouNo}' and PCM.Branch_Id={ObjGlobal.SysBranchId} and PCM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=1 and Module='PC' and BillTerm_Type in (1,2) AND PTD.Term_Amount<>0 Group By PCM.Challan_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type ";
                Query =
                    $"{Query} ) as aa Group By Challan_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct PCM.Challan_No,P.Product_Code,Product_Name,Gdn_Code,Gdn_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,PCD.Basic_Amount, ";
                Query =
                    $"{Query} PCD.Term_Amount,PCD.Net_Amount From AMS.PurchaseChallan_Details as PCD Inner Join AMS.PurchaseChallan_Main as PCM On PCM.Challan_No=PCD.Challan_No ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.Product_Id=PCD.Product_Id Left Outer Join AMS.Godown as G On G.Gdn_Id=PCD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=PCD.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=PCD.Unit_Id";
                Query =
                    $"{Query} Where PCM.Challan_No='{VouNo}' and PCM.Branch_Id={ObjGlobal.SysBranchId} and PCM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
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
                var i = 0;
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
                        PrintPurchaseChallanHeader(e);
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
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont, Brushes.Black,
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
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + 15;
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
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
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
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

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
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            //e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
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

    #endregion Purchase Challan

    #region Purchase Bill Design

    #region Purchase Bill Default Design

    private void PrintPurchaseBillHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = _document.DefaultPageSettings.Landscape switch
            {
                true => LeftMargin + 200,
                _ => iLeftMargin + 650
            };

            myFont = new Font("Arial", 12, FontStyle.Bold);
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("PURCHASE INVOICE", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PURCHASE INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name    : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);

            e.Graphics.DrawString("Invoice No       : ", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 690, iCellHeight), strFormat);
            e.Graphics.DrawString(DTVouMain.Rows[0]["Invoice_No"] + string.Empty, myFont, Brushes.Black,
                new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address          : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                    new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);

            e.Graphics.DrawString("Date                : ", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 690, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    Convert.ToDateTime(DTVouMain.Rows[0]["Invoice_Date"].ToString()).ToShortDateString() + string.Empty,
                    myFont, Brushes.Black, new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(DTVouMain.Rows[0]["Invoice_BSDate"] + string.Empty, myFont, Brushes.Black,
                    new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            if (DTVouMain.Rows[0]["Pan_No"] != null && DTVouMain.Rows[0]["Pan_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"PAN/VAT No  : {DTVouMain.Rows[0]["Pan_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
                if (DTVouMain.Rows[0]["Party_Invoice_No"] != null &&
                    DTVouMain.Rows[0]["Party_Invoice_No"].ToString() != string.Empty)
                {
                    e.Graphics.DrawString($"Party Inv No    : {string.Empty}", myFont, Brushes.Black,
                        new RectangleF(600, iTopMargin, 690, iCellHeight), strFormat);
                    e.Graphics.DrawString(DTVouMain.Rows[0]["Party_Invoice_No"] + string.Empty, myFont, Brushes.Black,
                        new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                }

                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;
            }
            else
            {
                if (DTVouMain.Rows[0]["Party_Invoice_No"] != null &&
                    DTVouMain.Rows[0]["Party_Invoice_No"].ToString() != string.Empty)
                {
                    e.Graphics.DrawString($"Party Inv No    : {string.Empty}", myFont, Brushes.Black,
                        new RectangleF(600, iTopMargin, 690, iCellHeight), strFormat);
                    e.Graphics.DrawString(DTVouMain.Rows[0]["Party_Invoice_No"] + string.Empty, myFont, Brushes.Black,
                        new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;
                }
            }

            if (DTVouMain.Rows[0]["Phone_No"] != null && DTVouMain.Rows[0]["Phone_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Phone No       : {DTVouMain.Rows[0]["Phone_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(20, iTopMargin, 600, iCellHeight), strFormat);
                if (DTVouMain.Rows[0]["Party_Invoice_Date"] != null &&
                    DTVouMain.Rows[0]["Party_Invoice_Date"].ToString() != string.Empty)
                {
                    e.Graphics.DrawString($"Party Inv Date : {string.Empty}", myFont, Brushes.Black,
                        new RectangleF(600, iTopMargin, 690, iCellHeight), strFormat);
                    if (ObjGlobal.SysDateType == "AD")
                        e.Graphics.DrawString(DTVouMain.Rows[0]["Party_Invoice_Date"] + string.Empty, myFont,
                            Brushes.Black, new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString(
                            ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["Party_Invoice_Date"])
                                .ToShortDateString()) + string.Empty, myFont, Brushes.Black,
                            new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                }

                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 1;
            }
            else
            {
                if (DTVouMain.Rows[0]["Party_Invoice_Date"] != null &&
                    DTVouMain.Rows[0]["Party_Invoice_Date"].ToString() != string.Empty)
                {
                    e.Graphics.DrawString($"Party Inv Date : {string.Empty}", myFont, Brushes.Black,
                        new RectangleF(600, iTopMargin, 690, iCellHeight), strFormat);
                    if (ObjGlobal.SysDateType == "AD")
                        e.Graphics.DrawString(DTVouMain.Rows[0]["Party_Invoice_Date"] + string.Empty, myFont,
                            Brushes.Black, new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString(
                            ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["Party_Invoice_Date"])
                                .ToShortDateString()) + string.Empty, myFont, Brushes.Black,
                            new RectangleF(700, iTopMargin, 800, iCellHeight), strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 1;
                }
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                strFormat.Alignment = ColFormat[i].ToString() switch
                {
                    "Left" => StringAlignment.Near,
                    _ => ColFormat[i].ToString() switch
                    {
                        "Left" => StringAlignment.Center,
                        "Right" => StringAlignment.Far,
                        _ => strFormat.Alignment
                    }
                };

                iHeaderHeight = (int)e.Graphics
                                    .MeasureString(ColHeaders[i].ToString(), myFont,
                                        Convert.ToInt16(ColWidths[i].ToString())).Height +
                                1;
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

    private void PrintPurchaseBillDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100 + 10; //Set the left margin
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
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT Invoice_No,Invoice_Date,Invoice_BsDate,Party_Invoice_No,Party_Invoice_Date,L.Ledger_Code,L.Ledger_Name,Country,State,City, ";
                Query =
                    $"{Query} Address,Phone_No,AltPhone_No,Fax_No,Email_Id,Pan_No,Remarks FROM AMS.PurchaseInvoice_Main as PIM  Inner Join AMS.Ledger as L on L.Ledger_Code=PIM.Ledger_Code ";
                Query =
                    $"{Query} Where PIM.Invoice_No='{VouNo}' and PIM.Branch_Id={ObjGlobal.SysBranchId} and PIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select Invoice_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct PIM.Invoice_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then PTD.Term_Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(PTD.Term_Amount) Else  Sum(PTD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT Inner Join AMS.PurchaseTerm_Details as PTD On BT.BT_Id=PTD.Term_Id Inner Join AMS.PurchaseInvoice_Main as PIM On PIM.Invoice_No=PTD.Invoice_No ";
                Query =
                    $"{Query} Where PIM.Invoice_No='{VouNo}' and PIM.Branch_Id={ObjGlobal.SysBranchId} and PIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=1 and Module='PB' and BillTerm_Type in (1,2) AND PTD.Term_Amount<>0 Group By PIM.Invoice_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type ";
                Query =
                    $"{Query} ) as aa Group By Invoice_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct PIM.Invoice_No,P.Product_Code,Product_Name,Gdn_Code,Gdn_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,PID.Basic_Amount, ";
                Query =
                    $"{Query} PID.Term_Amount,PID.Net_Amount From AMS.PurchaseInvoice_Details as PID Inner Join AMS.PurchaseInvoice_Main as PIM On PIM.Invoice_No=PID.Invoice_No ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.Product_Id=PID.Product_Id Left Outer Join AMS.Godown as G On G.Gdn_Id=PID.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=PID.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=PID.Unit_Id";
                Query =
                    $"{Query} Where PIM.Invoice_No='{VouNo}' and PIM.Branch_Id={ObjGlobal.SysBranchId} and PIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} ";
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
                ColWidths.Add(325);
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
                        PrintPurchaseBillHeader(e);
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

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont, Brushes.Black,
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
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 820, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
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
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 820, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 820, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            // e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 820, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {DTVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            ////e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)800, (float)iCellHeight), strFormat);
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

    #endregion Purchase Bill Default Design

    #endregion Purchase Bill Design

    #region Purchase Return

    private void PrintPurchaseReturnBillHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = _document.DefaultPageSettings.Landscape switch
            {
                true => LeftMargin + 200,
                _ => iLeftMargin + 650
            };

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
            e.Graphics.DrawString("PURCHASE RETURN INVOICE", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PURCHASE RETURN INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"Invoice No : {DTVouMain.Rows[0]["PurReturn_Invoice_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["ReturnInvoice_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["ReturnInvoice_BSDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Far;
            if (DTVouMain.Rows[0]["Purchase_Invoice_No"] != null &&
                DTVouMain.Rows[0]["Purchase_Invoice_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Pur Inv No : {DTVouMain.Rows[0]["Purchase_Invoice_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

                if (DTVouMain.Rows[0]["Purchase_Invoice_Date"] != null &&
                    DTVouMain.Rows[0]["Purchase_Invoice_Date"].ToString() != string.Empty)
                {
                    if (ObjGlobal.SysDateType == "AD")
                        e.Graphics.DrawString(
                            $"Pur Inv Date(AD) : {DTVouMain.Rows[0]["Purchase_Invoice_Date"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString(
                            $"Pur Inv Date(BS) : {ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["Purchase_Invoice_Date"]).ToShortDateString())}{string.Empty}",
                            myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 1;
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
                strFormat.Alignment = ColFormat[i].ToString() switch
                {
                    "Left" => StringAlignment.Near,
                    _ => ColFormat[i].ToString() switch
                    {
                        "Left" => StringAlignment.Center,
                        "Right" => StringAlignment.Far,
                        _ => strFormat.Alignment
                    }
                };

                iHeaderHeight = (int)e.Graphics
                                    .MeasureString(ColHeaders[i].ToString(), myFont,
                                        Convert.ToInt16(ColWidths[i].ToString())).Height +
                                1;
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

    private void PrintPurchaseReturnBillDetails(PrintPageEventArgs e)
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
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT PurReturn_Invoice_No,ReturnInvoice_Date,ReturnInvoice_BsDate,Purchase_Invoice_No,Purchase_Invoice_Date,L.Ledger_Code,L.Ledger_Name,Country,State,City, ";
                Query =
                    $"{Query} Address,Phone_No,AltPhone_No,Fax_No,Email_Id,Pan_No,Remarks FROM AMS.PurchaseReturn_Main as PRM Inner Join AMS.Ledger as L on L.Ledger_Code=PRM.Ledger_Code";
                Query =
                    $"{Query} Where PRM.PurReturn_Invoice_No ='{VouNo}' and PRM.Branch_Id={ObjGlobal.SysBranchId} and PRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select PurReturn_Invoice_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct PRM.PurReturn_Invoice_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then PTD.Term_Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(PTD.Term_Amount) Else  Sum(PTD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT Inner Join AMS.PurchaseTerm_Details as PTD On BT.BT_Id=PTD.Term_Id Inner Join AMS.PurchaseReturn_Main as PRM On PRM.PurReturn_Invoice_No=PTD.Invoice_No ";
                Query =
                    $"{Query} Where PRM.PurReturn_Invoice_No='{VouNo}' and PRM.Branch_Id={ObjGlobal.SysBranchId} and PRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=1 and Module='PR' and BillTerm_Type in (1,2) AND PTD.Term_Amount<>0 Group By PRM.PurReturn_Invoice_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type ";
                Query =
                    $"{Query} ) as aa Group By PurReturn_Invoice_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct PRM.PurReturn_Invoice_No,P.Product_Code,Product_Name,Gdn_Code,Gdn_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,PRD.Basic_Amount, ";
                Query =
                    $"{Query} PRD.Term_Amount,PRD.Net_Amount From AMS.PurchaseReturn_Details as PRD Inner Join AMS.PurchaseReturn_Main as PRM On PRM.PurReturn_Invoice_No=PRD.PurReturn_Invoice_No ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.Product_Id=PRD.Product_Id Left Outer Join AMS.Godown as G On G.Gdn_Id=PRD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=PRD.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=PRD.Unit_Id";
                Query =
                    $"{Query} Where PRM.PurReturn_Invoice_No='{VouNo}' and PRM.Branch_Id={ObjGlobal.SysBranchId} and PRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
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
                        PrintPurchaseReturnBillHeader(e);
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

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont, Brushes.Black,
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

            if (LCnt < N_Line - N_TermDet - 5)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 5; ln++)
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
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
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
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

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
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            // e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
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

    #endregion Purchase Return

    #region Purchase ExBrReturn

    private void PrintPurchaseExBrReturnBillHeader(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = _document.DefaultPageSettings.Landscape switch
            {
                true => LeftMargin + 200,
                _ => iLeftMargin + 650
            };

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
            e.Graphics.DrawString("PURCHASE EX/BR RETURN INVOICE", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PURCHASE EX/BR RETURN INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Party Name  : {DTVouMain.Rows[0]["Ledger_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($"ExBr Return Inv No : {DTVouMain.Rows[0]["ExpiryBreakage_No"]}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            if (DTVouMain.Rows[0]["Address"] != null && DTVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {DTVouMain.Rows[0]["Address"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(DTVouMain.Rows[0]["ExpiryBreakage_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {DTVouMain.Rows[0]["ExpiryBreakage_BSDate"]}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Far;
            if (DTVouMain.Rows[0]["RefBill_No"] != null && DTVouMain.Rows[0]["RefBill_No"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Ref. Bill No : {DTVouMain.Rows[0]["RefBill_No"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 5;

                if (DTVouMain.Rows[0]["RefBill_Date"] != null &&
                    DTVouMain.Rows[0]["RefBill_Date"].ToString() != string.Empty)
                {
                    if (ObjGlobal.SysDateType == "AD")
                        e.Graphics.DrawString(
                            $"Ref. Bill Date(AD) : {DTVouMain.Rows[0]["RefBill_Date"]}{string.Empty}", myFont,
                            Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);
                    else
                        e.Graphics.DrawString(
                            $"Ref. Bill Date(BS) : {ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(DTVouMain.Rows[0]["RefBill_Date"]).ToShortDateString())}{string.Empty}",
                            myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, iCellHeight), strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString("Invoice No : ", myFont).Height + 1;
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
                strFormat.Alignment = ColFormat[i].ToString() switch
                {
                    "Left" => StringAlignment.Near,
                    _ => ColFormat[i].ToString() switch
                    {
                        "Left" => StringAlignment.Center,
                        "Right" => StringAlignment.Far,
                        _ => strFormat.Alignment
                    }
                };

                iHeaderHeight = (int)e.Graphics
                                    .MeasureString(ColHeaders[i].ToString(), myFont,
                                        Convert.ToInt16(ColWidths[i].ToString())).Height +
                                1;
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

    private void PrintPurchaseExBrReturnBillDetails(PrintPageEventArgs e)
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
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;

                Query =
                    "SELECT ExpiryBreakage_No,ExpiryBreakagee_Date,ExpiryBreakage_BsDate,RefBill_No,RefBill_Date,L.Ledger_Code,L.Ledger_Name,Country,State,City, ";
                Query =
                    $"{Query} Address,Phone_No,AltPhone_No,Fax_No,Email_Id,Pan_No,Remarks FROM AMS.PurchaseExpiryBreakage_Main as PRM Inner Join AMS.Ledger as L on L.Ledger_Code=PRM.Ledger_Code";
                Query =
                    $"{Query} Where PRM.ExpiryBreakage_No ='{VouNo}' and PRM.Branch_Id={ObjGlobal.SysBranchId} and PRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} ";
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count <= 0) return;

                Query =
                    "Select ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From (Select Distinct PRM.ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then PTD.Term_Rate else 0 end Term_Rate,";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(PTD.Term_Amount) Else  Sum(PTD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT Inner Join AMS.PurchaseTerm_Details as PTD On BT.BT_Id=PTD.Term_Id Inner Join AMS.PurchaseExpiryBreakage_Main as PRM On PRM.ExpiryBreakage_No=PTD.Invoice_No ";
                Query =
                    $"{Query} Where PRM.ExpiryBreakage_No='{VouNo}' and PRM.Branch_Id={ObjGlobal.SysBranchId} and PRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=1 and Module='PEB' and BillTerm_Type in (1,2) AND PTD.Term_Amount<>0 Group By PRM.ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type ";
                Query =
                    $"{Query} ) as aa Group By ExpiryBreakage_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo";
                DTTermDetails.Reset();
                DTTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = DTTermDetails.Rows.Count;

                Query =
                    "Select Distinct PRM.ExpiryBreakage_No,P.Product_Code,Product_Name,Gdn_Code,Gdn_Name,AltU.Unit_Code AltUnit,U.Unit_Code Unit,Alt_Qty,Qty,Rate,PRD.Basic_Amount, ";
                Query =
                    $"{Query} PRD.Term_Amount,PRD.Net_Amount From AMS.PurchaseExpiryBreakage_Details as PRD Inner Join AMS.PurchaseExpiryBreakage_Main as PRM On PRM.ExpiryBreakage_No=PRD.ExpiryBreakage_No ";
                Query =
                    $"{Query} Inner Join AMS.Product as P On P.Product_Id=PRD.Product_Id Left Outer Join AMS.Godown as G On G.Gdn_Id=PRD.Gdn_Id ";
                Query =
                    $"{Query} Left Outer Join AMS.Unit as AltU On AltU.Unit_Id=PRD.Alt_Unit_Id Left Outer Join AMS.Unit as U On U.Unit_Id=PRD.Unit_Id";
                Query =
                    $"{Query} Where PRM.ExpiryBreakage_No='{VouNo}' and PRM.Branch_Id={ObjGlobal.SysBranchId} and PRM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
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
                        PrintPurchaseExBrReturnBillHeader(e);
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

                    if (DTVouDetails.Rows[i]["Product_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Product_Name"].ToString(), myFont, Brushes.Black,
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

            if (LCnt < N_Line - N_TermDet - 5)
                for (var ln = LCnt; ln <= N_Line - N_TermDet - 5; ln++)
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
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
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
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

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
            e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[7], iTopMargin, (int)arrColumnWidths[7], iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            // e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(15, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
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

    #endregion Purchase ExBrReturn
}