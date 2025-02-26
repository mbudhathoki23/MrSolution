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

public class DocPrintingInventory
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
            DTCD = GetConnection.SelectDataTableQuery(
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
                else if (DocDesign_Name is "FNB Invoice 3inch" or "FNB Order 3inch" or "FNB Order KOT/BOT 3inch")
                {
                    _printDocument.DefaultPageSettings.PaperSize = new PaperSize("FNB", 320, 1000);
                }
                //printDocument1.DefaultPageSettings.Landscape = PRT.Landscape;

                Query = string.Empty;
                VouNo = string.Empty;

                if (Module == "SA")
                {
                    Query = "SELECT StockAdjust_No Voucher_No FROM AMS.StockAdjustment_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and StockAdjust_No>='" + FromDocNo + "' and StockAdjust_No<='" + ToDocNo +
                                "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and StockAdjust_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and StockAdjust_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "ST")
                {
                    Query = "SELECT StockTransfer_No Voucher_No FROM AMS.StockTransfer_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and StockTransfer_No>='" + FromDocNo + "' and StockTransfer_No<='" + ToDocNo +
                                "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and StockTransfer_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and StockTransfer_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "STEB")
                {
                    Query = "SELECT ExpiryBreakage_No Voucher_No FROM AMS.StockTransferEB_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and ExpiryBreakage_No>='" + FromDocNo + "' and ExpiryBreakage_No<='" +
                                ToDocNo + "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and ExpiryBreakage_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and ExpiryBreakage_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "BOM")
                {
                    Query = "SELECT BOM_No Voucher_No FROM AMS.BOM_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        Query = Query + " and BOM_No>='" + FromDocNo + "' and BOM_No<='" + ToDocNo + "'";
                }
                else if (Module == "IRQ")
                {
                    Query = "SELECT Requisition_No Voucher_No FROM AMS.InventoryRequisition_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and Requisition_No>='" + FromDocNo + "' and Requisition_No<='" + ToDocNo +
                                "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and Requisition_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and Requisition_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "MI")
                {
                    Query = "SELECT Issue_No Voucher_No FROM AMS.MaterialIssue_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and Issue_No>='" + FromDocNo + "' and Issue_No<='" + ToDocNo + "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and Issue_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and Issue_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "MIR")
                {
                    Query = "SELECT Return_No Voucher_No FROM AMS.MaterialIssueReturn_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and Return_No>='" + FromDocNo + "' and Return_No<='" + ToDocNo + "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and Return_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and Return_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "MR")
                {
                    Query = "SELECT Receive_No Voucher_No FROM AMS.MaterialReceive_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and Receive_No>='" + FromDocNo + "' and Receive_No<='" + ToDocNo + "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and Receive_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and Receive_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
                    }
                }
                else if (Module == "MRR")
                {
                    Query = "SELECT Return_No Voucher_No FROM AMS.MaterialRecReturn_Main Where FiscalYear_Id=" +
                            ObjGlobal.SysFiscalYearId + string.Empty;
                    if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                    {
                        Query = Query + " and Return_No>='" + FromDocNo + "' and Return_No<='" + ToDocNo + "'";
                    }
                    else
                    {
                        if (ObjGlobal.SysDateType == "AD")
                            Query = Query + " and Return_Date between '" + FromDate + "' and '" + ToDate + "'";
                        else
                            Query = Query + " and Return_Date between '" +
                                    ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(FromDate).ToShortDateString()) +
                                    "' and '" + ObjGlobal.ReturnNepaliDate(Convert.ToDateTime(ToDate)
                                        .ToShortDateString()) + "'";
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
                        $"Insert Into AMS.Print_Voucher Values ('{VouNo}', '{Module}', {NoOf_Copy}, {ObjGlobal.LogInUserId},getdate(), {ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId})";
                    dt = GetConnection.SelectDataTableQuery(Query);
                }
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
        //if (Module == "SQ")
        //    PrintSalesQuotationDetails(e);
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        _printPreviewDialog.Document = _printDocument;
        _printPreviewDialog.ShowDialog();
    }

    #region Global

    private DataTable DTCD = new();
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
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.str' is never used
    private string str;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.str' is never used
    private string Query;
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.FromAdDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.ToADDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotDrAmt' is never used
    private double TotDrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotDrAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotCrAmt' is never used
    private double TotCrAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotCrAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotAltQty' is never used
    private double TotAltQty;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotAltQty' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotQty' is never used
    private double TotQty;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotQty' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotBasicAmt' is never used
    private double TotBasicAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotBasicAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotNetAmt' is never used
    private double TotNetAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotNetAmt' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.TotGrandAmt' is never used
    private double TotGrandAmt;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.TotGrandAmt' is never used
    private Font myFont = new("Arial", 10);
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.rowPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.N_Line' is assigned but its value is never used
    private int N_Line;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.N_Line' is assigned but its value is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.N_FootLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.N_TermDet' is never used
    private int N_TermDet;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.N_TermDet' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.RemarksTotLen' is never used
    private int RemarksTotLen;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.RemarksTotLen' is never used
    private StringFormat strFormat; //Used to format the grid rows.
    private ArrayList ColHeaders = new();
    private ArrayList ColWidths = new();
    private ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList arrColumnWidths = new(); //Used to save column widths
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iCellHeight' is assigned but its value is never used
    private int iCellHeight; //Used to get/set the datagridview cell height
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iCellHeight' is assigned but its value is never used
    public int PageWidth;
    public int PageHeight;
    public long PageNo;
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iRow' is assigned but its value is never used
    private long iRow = 0; //Used as counter
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iRow' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iCount' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.bFirstPage' is assigned but its value is never used
    private bool bFirstPage; //Used to check whether we are printing first page
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.bFirstPage' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.bNewPage' is assigned but its value is never used
    private bool bNewPage; // Used to check whether we are printing a new page
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.bNewPage' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iHeaderHeight' is assigned but its value is never used
    private int iHeaderHeight = 0; //Used for the header height
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iHeaderHeight' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.bMorePagesToPrint' is assigned but its value is never used
    private bool bMorePagesToPrint = false;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.bMorePagesToPrint' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iLeftMargin' is assigned but its value is never used
    private int iLeftMargin = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iLeftMargin' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iRightMargin' is assigned but its value is never used
    private int iRightMargin = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iRightMargin' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iTopMargin' is assigned but its value is never used
    private int iTopMargin = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iTopMargin' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.iTmpWidth' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.i' is assigned but its value is never used
    private int i;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.i' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.DRo' is assigned but its value is never used
    private int DRo;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.DRo' is assigned but its value is never used
    private string VouNo = string.Empty;
#pragma warning disable CS0169 // The field 'DocPrinting_Inventory.CashVoucher' is never used
    private bool CashVoucher;
#pragma warning restore CS0169 // The field 'DocPrinting_Inventory.CashVoucher' is never used
    private string Msg = string.Empty;
#pragma warning disable CS0414 // The field 'DocPrinting_Inventory.Printed' is assigned but its value is never used
    private bool Printed;
#pragma warning restore CS0414 // The field 'DocPrinting_Inventory.Printed' is assigned but its value is never used

    public string FrmName { get; set; }

    public string Module { get; set; }

    public string SelectQuery { get; set; }

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