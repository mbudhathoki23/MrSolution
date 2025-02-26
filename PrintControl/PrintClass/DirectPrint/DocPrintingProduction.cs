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

public class DocPrintingProduction
{
    public string PrintDoc(bool print)
    {
        //if (print == true)
        //{
        _printDocument.BeginPrint += printDocument1_BeginPrint;
        _printDocument.PrintPage += printDocument1_PrintPage;
        //}
        //else
        _printPreviewDialog.Click += printPreviewDialog1_Click;
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
                else
                {
                    _printDocument.DefaultPageSettings.PaperSize = DocDesign_Name switch
                    {
                        "FNB Invoice 3inch" => new PaperSize("FNB", 320, 1000),
                        "FNB Order 3inch" => new PaperSize("FNB", 320, 1000),
                        "FNB Order KOT/BOT 3inch" => new PaperSize("FNB", 320, 1000),
                        _ => _printDocument.DefaultPageSettings.PaperSize
                    };
                }
                //printDocument1.DefaultPageSettings.Landscape = PRT.Landscape;

                Query = string.Empty;
                VouNo = string.Empty;
                if (Station == "MBOM")
                {
                    Query =
                        $"SELECT Quotation_No Voucher_No FROM AMS.SalesQuotation_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and Quotation_No>='{FromDocNo}' and Quotation_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType == "AD"
                            ? $"{Query} and Quotation_Date between '{FromDate}' and '{ToDate}'"
                            : $"{Query} and Quotation_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                }
                else if (Station == "RMI")
                {
                    Query =
                        $"SELECT Order_No Voucher_No FROM AMS.SalesOrder_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and Order_No>='{FromDocNo}' and Order_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType == "AD"
                            ? $"{Query} and Order_Date between '{FromDate}' and '{ToDate}'"
                            : $"{Query} and Order_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'";
                }
                else if (Station == "RMIR")
                {
                    Query =
                        $"SELECT OrderCancellation_No Voucher_No FROM AMS.SalesOrderCancellation_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query =
                            $"{Query} and OrderCancellation_No>='{FromDocNo}' and OrderCancellation_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "AD" => $"{Query} and OrderCancellation_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and OrderCancellation_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Station == "RMR")
                {
                    Query =
                        $"SELECT Challan_No Voucher_No FROM AMS.SalesChallan_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = $"{Query} and Challan_No>='{FromDocNo}' and Challan_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "AD" => $"{Query} and Challan_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and Challan_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
                        };
                }
                else if (Station == "RMIR")
                {
                    Query =
                        $"SELECT SalesReturn_Invoice_No Voucher_No FROM AMS.SalesReturn_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query =
                            $"{Query} and PurReturn_Invoice_No>='{FromDocNo}' and PurReturn_Invoice_No<='{ToDocNo}'";
                    else
                        Query = ObjGlobal.SysDateType switch
                        {
                            "AD" => $"{Query} and ReturnInvoice_Date between '{FromDate}' and '{ToDate}'",
                            _ =>
                                $"{Query} and ReturnInvoice_Date between '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString())}' and '{ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate).ToShortDateString())}'"
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
                            _printDocument.PrintController =
                                _printController; //User Printing page dialog press enter enter(2 times) button then cancel print voucher
                            _printDocument.Print();
                        }
                        else
                        {
                            _printPreviewDialog.Document = _printDocument;
                            _printPreviewDialog.PrintPreviewControl.Zoom = 1;
                            ((Form)_printPreviewDialog).WindowState = FormWindowState.Maximized;
                            _printPreviewDialog.ShowDialog();
                        }
                    }
                }
            }

            if (print)
                foreach (DataRow VNro in DTNoOfVou.Rows)
                {
                    VouNo = VNro["Voucher_No"].ToString();
                    dt.Reset();
                    Query =
                        $"Insert Into AMS.Print_Voucher Values ('{VouNo}', '{Station}', {NoOf_Copy}, {ObjGlobal.LogInUserId},getdate(), {ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId})";
                    dt = GetConnection.SelectDataTableQuery(Query);
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
        //if (Station == "SQ")
        //    PrintSalesQuotationDetails(e);
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        _printPreviewDialog.Document = _printDocument;
        _printPreviewDialog.ShowDialog();
    }

    #region Global

    private DataTable _dataTable = new();
    private readonly System.Drawing.Printing.PrintDocument _printDocument = new();
    private readonly PrintController _printController = new StandardPrintController();
    private readonly PrintPreviewDialog _printPreviewDialog = new();

    private DataTable dt = new();
    private DataTable DTTemp = new();
    private DataTable DTNoOfVou = new();
    private DataTable DTVouMain = new();
    private DataTable DTVouDetails = new();
    private DataTable DTTermDetails = new();
    private DataTable DTProdTerm = new();
    private DataTable DTBillTerm = new();
    private DataSet ds = new();
#pragma warning disable CS0169 // The field 'DocPrinting_Production.str' is never used
    private string str;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.str' is never used
    private string Query;
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
#pragma warning disable CS0169 // The field 'DocPrinting_Production.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.FromAdDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.ToADDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotDrAmt' is never used
    private double TotDrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotDrAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotCrAmt' is never used
    private double TotCrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotCrAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotAltQty' is never used
    private double TotAltQty;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotAltQty' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotQty' is never used
    private double TotQty;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotQty' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotBasicAmt' is never used
    private double TotBasicAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotBasicAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotNetAmt' is never used
    private double TotNetAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotNetAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.TotGrandAmt' is never used
    private double TotGrandAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.TotGrandAmt' is never used
    private Font myFont = new("Arial", 10);
#pragma warning disable CS0414 // The field 'DocPrinting_Production.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.rowPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.N_Line' is assigned but its value is never used
    private int N_Line;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.N_Line' is assigned but its value is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.N_FootLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.N_TermDet' is never used
    private int N_TermDet;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.N_TermDet' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Production.RemarksTotLen' is never used
    private int RemarksTotLen;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.RemarksTotLen' is never used
    private StringFormat strFormat; //Used to format the grid rows.
    private ArrayList ColHeaders = new();
    private ArrayList ColWidths = new();
    private ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList arrColumnWidths = new(); //Used to save column widths
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iCellHeight' is assigned but its value is never used
    private int iCellHeight; //Used to get/set the datagridview cell height
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iCellHeight' is assigned but its value is never used
    public int PageWidth;
    public int PageHeight;
    public long PageNo;
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iRow' is assigned but its value is never used
    private long iRow = 0; //Used as counter
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iRow' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iCount' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.bFirstPage' is assigned but its value is never used
    private bool bFirstPage; //Used to check whether we are printing first page
#pragma warning restore CS0414 // The field 'DocPrinting_Production.bFirstPage' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.bNewPage' is assigned but its value is never used
    private bool bNewPage; // Used to check whether we are printing a new page
#pragma warning restore CS0414 // The field 'DocPrinting_Production.bNewPage' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iHeaderHeight' is assigned but its value is never used
    private int iHeaderHeight = 0; //Used for the header height
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iHeaderHeight' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.bMorePagesToPrint' is assigned but its value is never used
    private bool bMorePagesToPrint = false;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.bMorePagesToPrint' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iLeftMargin' is assigned but its value is never used
    private int iLeftMargin = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iLeftMargin' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iRightMargin' is assigned but its value is never used
    private int iRightMargin = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iRightMargin' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iTopMargin' is assigned but its value is never used
    private int iTopMargin = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iTopMargin' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.iTmpWidth' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.i' is assigned but its value is never used
    private int i;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.i' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Production.DRo' is assigned but its value is never used
    private int DRo;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.DRo' is assigned but its value is never used
    private string VouNo = string.Empty;
#pragma warning disable CS0169 // The field 'DocPrinting_Production.CashVoucher' is never used
    private bool CashVoucher;
#pragma warning restore CS0169 // The field 'DocPrinting_Production.CashVoucher' is never used
    private string Msg = string.Empty;
#pragma warning disable CS0414 // The field 'DocPrinting_Production.Printed' is assigned but its value is never used
    private bool Printed;
#pragma warning restore CS0414 // The field 'DocPrinting_Production.Printed' is assigned but its value is never used

    public string FrmName { get; set; }

    public string Station { get; set; }

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
}