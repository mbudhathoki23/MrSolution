using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PrintControl.Print.DirectPrint;

public class DocPrintingHotel
{
    public string PrintDoc(bool print)
    {
        try
        {
            _printDocument.BeginPrint += printDocument1_BeginPrint;
            _printDocument.PrintPage += printDocument1_PrintPage;

            _printPreviewDialog.Click += printPreviewDialog1_Click;
            Printed = false;
            if (Convert.ToInt16(NoOfCopy) > 0)
            {
                _dataTable = GetConnection.SelectDataTableQuery(
                    "SELECT Company_Name,Address,Country,City,State ,PhoneNo,Pan_No,Email,Website  FROM AMS.CompanyInfo");
                for (var Noc = 1; Noc <= Convert.ToInt16(NoOfCopy); Noc++)
                {
                    //PageNo = 0;
                    _printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                    _printDocument.PrinterSettings.PrinterName = PrinterName;
                    if (DocDesignName is "A4 Full" or "Receipt Voucher" or "Payment Voucher")
                    {
                        N_Line = 30;
                        //printDocument1.DefaultPageSettings.PaperSize.PaperName = System.Drawing.Printing.PaperKind.A4.ToString();
                        _printDocument.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                    }
                    else if (DocDesignName == "A4 Half")
                    {
                        N_Line = 5;
                        _printDocument.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                    }
                    else if (DocDesignName == "LETTER FULL")
                    {
                        _printDocument.DefaultPageSettings.PaperSize.PaperName = PaperKind.Letter.ToString();
                    }
                    else if (DocDesignName is "FNB Invoice 3inch" or "FNB Order 3inch" or "FNB Order KOT/BOT 3inch")
                    {
                        _printDocument.DefaultPageSettings.PaperSize = new PaperSize("FNB", 320, 1000);
                    }
                    //printDocument1.DefaultPageSettings.Landscape = PRT.Landscape;

                    Query = string.Empty;
                    VouNo = string.Empty;
                    if (Module == "HBN")
                    {
                        Query =
                            $"SELECT Booking_No Voucher_No FROM AMS.HTBooking_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and Booking_No>='{FromDocNo}' and Booking_No<='{ToDocNo}'";
                        }
                        else
                        {
                            if (ObjGlobal.SysDateType == "D")
                                Query = $"{Query} and Booking_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Booking_Date between '{ObjGlobal.ReturnNepaliDate(FromDate)}' and '{ObjGlobal.ReturnNepaliDate(ToDate)}'";
                        }
                    }
                    else if (Module == "HBC")
                    {
                        Query =
                            $"SELECT Cancle_No Voucher_No FROM AMS.HTBooking_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and Cancle_No>='{FromDocNo}' and Cancle_No<='{ToDocNo}'";
                        }
                        else
                        {
                            if (ObjGlobal.SysDateType == "AD")
                                Query = $"{Query} and Cancle_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and Cancle_Date between '{ObjGlobal.ReturnNepaliDate(FromDate)}' and '{ObjGlobal.ReturnNepaliDate(ToDate)}'";
                        }
                    }
                    else if (Module == "HCI")
                    {
                        Query =
                            $"SELECT CheckIn_No Voucher_No FROM AMS.HTCheckIn_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and CheckIn_No>='{FromDocNo}' and CheckIn_No<='{ToDocNo}'";
                        }
                        else
                        {
                            if (ObjGlobal.SysDateType == "AD")
                                Query = $"{Query} and CheckIn_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and CheckIn_Date between '{ObjGlobal.ReturnNepaliDate(FromDate)}' and '{ObjGlobal.ReturnNepaliDate(ToDate)}'";
                        }
                    }
                    else if (Module == "HCO")
                    {
                        Query =
                            $"SELECT CheckOut_No Voucher_No FROM AMS.HTCheckOut_Main Where FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                        if (FromDocNo.Trim() != string.Empty && ToDocNo.Trim() != string.Empty)
                        {
                            Query = $"{Query} and CheckOut_No>='{FromDocNo}' and CheckOut_No<='{ToDocNo}'";
                        }
                        else
                        {
                            if (ObjGlobal.SysDateType == "AD")
                                Query = $"{Query} and CheckOut_Date between '{FromDate}' and '{ToDate}'";
                            else
                                Query =
                                    $"{Query} and CheckOut_Date between '{ObjGlobal.ReturnNepaliDate(FromDate)}' and '{ObjGlobal.ReturnNepaliDate(ToDate)}'";
                        }
                    }

                    if (Query != string.Empty)
                    {
                        _dtNoOfVou.Reset();
                        _dtNoOfVou = GetConnection.SelectDataTableQuery(Query);
                        if (_dtNoOfVou.Rows.Count <= 0)
                        {
                            Msg = "Does not exit Voucher No!";
                            return Msg;
                        }

                        foreach (DataRow VNro in _dtNoOfVou.Rows)
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
                    foreach (DataRow VNro in _dtNoOfVou.Rows)
                    {
                        VouNo = VNro["Voucher_No"].ToString();
                        _table.Reset();
                        Query =
                            $"Insert Into AMS.Print_Voucher Values ('{VouNo}', '{Module}', {NoOfCopy}, {ObjGlobal.LogInUserId},getdate(), {ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId})";
                        _table = GetConnection.SelectDataTableQuery(Query);
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

            _iCellHeight = 0;
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
        try
        {
            if (Module is "HBN" or "HBCN" or "HCI")
            {
                if (DocDesignName == "Booking List")
                {
                    N_Line = 30;
                    PrintHotelBookingDetails(e);
                }
                else if (DocDesignName == "CheckIn List")
                {
                    N_Line = 30;
                    PrintHotelCheckInDetails(e);
                }
                else
                {
                    PrintHotelAdvanceChargeVoucherDetails(e);
                }
            }
            else if (Module == "HCO")
            {
                if (DocDesignName == "Chk Out Invoice")
                    PrintHotelCheckOutInvoiceDetails(e);
                else if (DocDesignName == "Hotel Check Out 3inch")
                    PrintHotelCheckOutDetails3InchRollPaper(e);
                else
                    PrintHotelCheckOutBillDetails(e);
            }
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        _printPreviewDialog.Document = _printDocument;
        _printPreviewDialog.ShowDialog();
    }

    private void PrintHotelCheckOutHeader3InchRollPaper(PrintPageEventArgs e)
    {
        try
        {
            // ---------Start Report Header ----------------
            var LeftMargin = 0;
            LeftMargin = iLeftMargin + 550;

            myFont = new Font("Arial", 9, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(ObjGlobal.LogInCompany, myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString($"PAN/VAT No : {_dataTable.Rows[0]["Pan_Number"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("PAN/VAT No : ", myFont).Height + 5;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("INVOICE", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 8, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Bill No : {_dtVouMain.Rows[0]["CheckOut_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, 145, _iCellHeight), strFormat);
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date: {Convert.ToDateTime(_dtVouMain.Rows[0]["CheckOut_Date"].ToString()).ToShortDateString()}  {Convert.ToDateTime(_dtVouMain.Rows[0]["Created_Date"].ToString()).ToShortTimeString()}",
                    myFont, Brushes.Black, new RectangleF(150, iTopMargin, PageWidth, _iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    $"Date: {_dtVouMain.Rows[0]["CheckOut_BSDate"]}  {Convert.ToDateTime(_dtVouMain.Rows[0]["Created_Date"].ToString()).ToShortTimeString()}",
                    myFont, Brushes.Black, new RectangleF(150, iTopMargin, PageWidth, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Name : ", myFont).Height + 5;

            if (_dtVouMain.Rows[0]["Guest_Name"].ToString().Length >= 30)
                e.Graphics.DrawString(
                    $"Name : {_dtVouMain.Rows[0]["Guest_Name"].ToString().Substring(0, 30)}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(0, iTopMargin, 175, _iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Name : {_dtVouMain.Rows[0]["Guest_Name"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, 175, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date    : ", myFont).Height + 1;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;

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
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
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

    private void PrintHotelCheckOutDetails3InchRollPaper(PrintPageEventArgs e)
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
                    "SELECT CheckOut_No,CheckOut_Date,AMS.Return_NepaliDate(CONVERT(varchar, CheckOut_Date,101)) CheckOut_BsDate,L.Ledger_Code,(Case When L.Title=1 Then 'Mr.' When L.Title=2 Then 'Mrs.' When L.Title not in(1,2) Then 'Others' end + ' ' + L.First_Name + ' ' + L.Last_Name) as Guest_Name,Country,City, ";
                Query =
                    $"{Query} Address,Telphone_No,Mobile_No,Email_Id,HCOM.Remarks,IsNull(Bar_Charge,0)Bar_Charge,IsNull(Food_Charge,0)Food_Charge,IsNull(Laundary_Charge,0)Laundary_Charge,IsNull(Phone_Charge,0)Phone_Charge,IsNull(Internet_Charge,0)Internet_Charge,IsNull(Transport_Charge,0)Transport_Charge,IsNull(Misc_Charge,0)Misc_Charge,IsNull(Advance_Amount,0)Advance_Amount,IsNull(GrandTotal_Amount,0)GrandTotal_Amount,HCOM.Created_Date FROM AMS.HTCheckOut_Main as HCOM ";
                Query = $"{Query} Inner Join AMS.HTGuestInfo as L on L.Guest_Id=HCOM.Guest_Id";
                Query =
                    $"{Query} Where HCOM.CheckOut_No='{VouNo}' and HCOM.Branch_Id={ObjGlobal.SysBranchId}  and HCOM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                _dtVouMain.Reset();
                _dtVouMain = GetConnection.SelectDataTableQuery(Query);
                if (_dtVouMain.Rows.Count <= 0) return;
                Query = "Select CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From ( ";
                Query =
                    $"{Query} Select Distinct SIM.CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then STD.Term_Rate else 0 end Term_Rate, ";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(STD.Term_Amount) Else  Sum(STD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT  ";
                Query =
                    $"{Query} Inner Join AMS.SalesTerm_Details as STD On BT.BT_Id=STD.Term_Id Inner Join AMS.HTCheckOut_Main as SIM On SIM.CheckOut_No=STD.Invoice_No  ";
                Query =
                    $"{Query} Where SIM.CheckOut_No='{VouNo}' and SIM.Branch_Id={ObjGlobal.SysBranchId}  and SIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=2 and Module='HCO' and BillTerm_Type in (1,2) AND STD.Term_Amount<>0  ";
                Query = $"{Query} Group By SIM.CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type  ";
                Query =
                    $"{Query} ) as aa Group By CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo ";
                _dtTermDetails.Reset();
                _dtTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = _dtTermDetails.Rows.Count;

                Query =
                    "Select Distinct SIM.CheckOut_No,Room_No + ' - Room Charges' as Room_No,Bill_Days,Rate,SID.Basic_Amount,SID.Term_Amount,SID.Net_Amount From AMS.HTCheckOut_Details as SID ";
                Query = $"{Query} Inner Join AMS.HTCheckOut_Main as SIM On SIM.CheckOut_No=SID.CheckOut_No";
                Query = $"{Query} Inner Join AMS.HTRoom as P On P.Room_Id=SID.Room_Id ";
                Query =
                    $"{Query} Where SIM.CheckOut_No='{VouNo}' and SIM.Branch_Id={ObjGlobal.SysBranchId}  and SIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                _vouDetails.Reset();
                _vouDetails = GetConnection.SelectDataTableQuery(Query);

                ColHeaders.Clear();
                ColHeaders.Add("Sn");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Days");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Add(20);
                ColWidths.Add(110);
                ColWidths.Add(35);
                ColWidths.Add(50);
                ColWidths.Add(70);
            }

            long LCnt;
            LCnt = 0;
            _iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 8, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (_vouDetails.Rows.Count > 0)
            {
                //Set the cell height
                _iCellHeight = 20;
                var iCount = 0;
                PrintHotelCheckOutHeader3InchRollPaper(e);
                for (i = DRo; i < _vouDetails.Rows.Count; i++)
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
                    e.Graphics.DrawString($"{_iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            _iCellHeight), strFormat);
                    iCount++;

                    if (_vouDetails.Rows[i]["Room_No"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(_vouDetails.Rows[i]["Room_No"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Bill_Days"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(_vouDetails.Rows[i]["Bill_Days"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(Convert.ToInt16(_vouDetails.Rows[i]["Bill_Days"].ToString()).ToString(),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Basic_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(_vouDetails.Rows[i]["Basic_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Basic_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    _iRow++;
                    iTopMargin += _iCellHeight;
                }
            }

            iTopMargin += 10;
            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black, new RectangleF(70, iTopMargin, 300, _iCellHeight),
                strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToInt16(TotQty.ToString()).ToString(), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], _iCellHeight), strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 8, FontStyle.Regular);

            if (_dtTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in _dtTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF(70, iTopMargin, PageWidth, _iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], _iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;
                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Food_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Food Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Food_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Bar_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Bar Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Bar_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Laundary_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Laundary Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Laundary_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Phone_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Phone Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Phone_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Transport_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Transport Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Transport_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Internet_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Internet Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Internet_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Misc_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Misc. Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Misc_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Advance_Amount"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 8, FontStyle.Regular);
                e.Graphics.DrawString("Advance Amount ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    $" - {Convert.ToDecimal(_dtVouMain.Rows[0]["Advance_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat)}",
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("---------------------------------------------------------", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 8, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF(70, iTopMargin, PageWidth, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(
                Convert.ToDecimal(_dtVouMain.Rows[0]["GrandTotal_Amount"].ToString())
                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8, FontStyle.Regular);
            e.Graphics.DrawString("-------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            string AmountWords;
            AmountWords = ClsMoneyConversion.MoneyConversion(_dtVouMain.Rows[0]["GrandTotal_Amount"].ToString());
            if (AmountWords.Length <= 40)
            {
                e.Graphics.DrawString($"In Words : {AmountWords} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }
            else
            {
                e.Graphics.DrawString($"In Words : {AmountWords.Substring(0, 39)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
                e.Graphics.DrawString($"{AmountWords.Substring(39, AmountWords.Length - 39)} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            }

            e.Graphics.DrawString("-------------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 7;
            if (_dtVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
            {
                e.Graphics.DrawString($"Remarks : {_dtVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                    new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 20;
            }

            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + ObjGlobal._Company_Name + " ", myFont, Brushes.Black, new RectangleF(10, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 5;
            e.Graphics.DrawString($"User :  {ObjGlobal.LogInUser} ", myFont, Brushes.Black,
                new RectangleF(0, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #region Global

    private DataTable _dataTable = new();
    private readonly System.Drawing.Printing.PrintDocument _printDocument = new();
    private readonly PrintController _printController = new StandardPrintController();
    private readonly PrintPreviewDialog _printPreviewDialog = new();

    private DataTable _table = new();
    private DataTable _dtTemp = new();
    private DataTable _dtNoOfVou = new();
    private DataTable _dtVouMain = new();
    private DataTable _vouDetails = new();
    private DataTable _dtTermDetails = new();
    private DataTable _dtProdTerm = new();
    private DataTable _dtBillTerm = new();
    private DataTable _dtKotBot = new();
    private readonly DataSet _dataSet = new();
    private string _str;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.str' is never used
    private string Query;
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.FromAdDate' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.ToADDate' is never used
    private double TotDrAmt;
    private double TotCrAmt;
#pragma warning disable CS0414 // The field 'DocPrinting_Hotel.TotAltQty' is assigned but its value is never used
    private double TotAltQty;
#pragma warning restore CS0414 // The field 'DocPrinting_Hotel.TotAltQty' is assigned but its value is never used
    private double TotQty;
    private double TotBasicAmt;
    private double TotNetAmt;
    private double TotGrandAmt;
    private Font myFont = new("Arial", 10);
#pragma warning disable CS0414 // The field 'DocPrinting_Hotel.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_Hotel.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Hotel.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_Hotel.rowPosition' is assigned but its value is never used
    private int N_Line;
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.N_FootLine' is never used
    private int N_TermDet;
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.RemarksTotLen' is never used
    private int RemarksTotLen;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.RemarksTotLen' is never used
    private StringFormat strFormat; //Used to format the grid rows.
    private readonly ArrayList ColHeaders = new();
    private readonly ArrayList ColWidths = new();
    private readonly ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new(); //Used to save left coordinates of columns
    private readonly ArrayList arrColumnWidths = new(); //Used to save column widths
    private int _iCellHeight; //Used to get/set the datagridview cell height
    public int PageWidth;
    public int PageHeight;
    public long PageNo;
    private long _iRow; //Used as counter
#pragma warning disable CS0414 // The field 'DocPrinting_Hotel.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_Hotel.iCount' is assigned but its value is never used
    private bool bFirstPage; //Used to check whether we are printing first page
    private bool bNewPage; // Used to check whether we are printing a new page
    private int iHeaderHeight; //Used for the header height
    private bool bMorePagesToPrint;
    private int iLeftMargin;
    private int iRightMargin;
    private int iTopMargin;
#pragma warning disable CS0414 // The field 'DocPrinting_Hotel.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Hotel.iTmpWidth' is assigned but its value is never used
    private int i;
    private int DRo;
    private string VouNo = string.Empty;
#pragma warning disable CS0169 // The field 'DocPrinting_Hotel.CashVoucher' is never used
    private bool CashVoucher;
#pragma warning restore CS0169 // The field 'DocPrinting_Hotel.CashVoucher' is never used
    private string Msg = string.Empty;
#pragma warning disable CS0414 // The field 'DocPrinting_Hotel.Printed' is assigned but its value is never used
    private bool Printed;
#pragma warning restore CS0414 // The field 'DocPrinting_Hotel.Printed' is assigned but its value is never used

    public string FrmName { get; set; }

    public string Module { get; set; }

    public string SelectQuery { get; set; }

    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public string FromDocNo { get; set; }

    public string ToDocNo { get; set; }

    public string InvoiceType { get; set; }

    public string PrinterName { get; set; }

    public string DocDesignName { get; set; }

    public int NoOfCopy { get; set; }

    public string DocDesignNotes { get; set; }

    #endregion Global

    #region Hotel  Booking/Cancel/CheckIn

    private void PrintHotelAdvanceChargeVoucherHeader(PrintPageEventArgs e)
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
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "HBN")
                e.Graphics.DrawString("Booking Advance Receipt Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else if (Module == "HBCN")
                e.Graphics.DrawString("Cancel Charge Receipt Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else if (Module == "HCI")
                e.Graphics.DrawString("Check In Advance Receipt Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Far;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Voucher No : {_dtVouMain.Rows[0]["Voucher_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Voucher No : ", myFont).Height + 5;
            strFormat.Alignment = StringAlignment.Far;
            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(_dtVouMain.Rows[0]["Voucher_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {_dtVouMain.Rows[0]["Voucher_BsDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);

            e.Graphics.DrawString(
                "S.N.     Particulars                                                                                                                    Debit Amt              Credit Amt",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Particulars", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
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

    private void PrintHotelAdvanceChargeVoucherDetails(PrintPageEventArgs e)
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

            _iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;

                Query =
                    "SELECT Voucher_No,Voucher_Date,Voucher_BsDate,L.Ledger_Code,L.Ledger_Name,Remarks FROM AMS.AcTran as VM Inner Join AMS.Ledger as L on L.Ledger_Code=VM.Ledger_Code ";
                Query =
                    $"{Query} Where VM.Module='{Module}' and VM.Voucher_No ='{VouNo}' and L.Ledger_Code not in ('000023')";
                _dtVouMain.Reset();
                _dtVouMain = GetConnection.SelectDataTableQuery(Query);

                Query =
                    "SELECT VD.Voucher_No,L.Ledger_Code,L.Ledger_Name,Dr_Amt,Cr_Amt,Narration FROM AMS.AcTran as VD";
                Query = $"{Query} Inner Join AMS.Ledger as L on L.Ledger_Code=VD.Ledger_Code";
                Query = $"{Query} Where VD.Module='{Module}' and VD.Voucher_No='{VouNo}' ";
                Query = $"{Query} Order by Dr_Amt desc,Cr_Amt";
                _vouDetails.Reset();
                _vouDetails = GetConnection.SelectDataTableQuery(Query);

                ColWidths.Add(50);

                ColWidths.Add(460);
                ColWidths.Add(0);

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
            if (_vouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < _vouDetails.Rows.Count; i++) //foreach(DataRow ro in _vouDetails.Rows)
                {
                    //Set the cell height
                    _iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintHotelAdvanceChargeVoucherHeader(e);
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
                            _iCellHeight), strFormat);
                    iCount++;

                    if (_vouDetails.Rows[i]["Ledger_Name"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        if (_vouDetails.Rows[i]["Cr_Amt"].ToString() != null &&
                            Convert.ToDouble(_vouDetails.Rows[i]["Cr_Amt"].ToString()) != 0)
                            e.Graphics.DrawString($"     {_vouDetails.Rows[i]["Ledger_Name"]}", myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    _iCellHeight), strFormat);
                        else
                            e.Graphics.DrawString(_vouDetails.Rows[i]["Ledger_Name"].ToString(), myFont, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                    _iCellHeight), strFormat);
                    }

                    iCount++;

                    iCount++; //for sub 0 width

                    if (_vouDetails.Rows[i]["Dr_Amt"].ToString() != null)
                    {
                        TotDrAmt = TotDrAmt + Convert.ToDouble(_vouDetails.Rows[i]["Dr_Amt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Dr_Amt"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Cr_Amt"].ToString() != null)
                    {
                        TotCrAmt = TotCrAmt + Convert.ToDouble(_vouDetails.Rows[i]["Cr_Amt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Cr_Amt"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    _iRow++;
                    iTopMargin += _iCellHeight;
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
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + _iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 500, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotDrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], _iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotCrAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"On Account Of : {_dtVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ---------------------------                                    -------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString(
                "           Prepared By          " +
                "                                                   Checked By 	                                                " +
                " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight),
                strFormat);
            //strFormat.Alignment = StringAlignment.Far;
            //e.Graphics.DrawString(" For : " + ObjGlobal._Company_Name + " ", myFont, Brushes.Black, new RectangleF(200, (float)iTopMargin, (int)620, (float)iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintHotelBookingDetails(PrintPageEventArgs e)
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

            _iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotNetAmt = 0;
                TotQty = 0;

                //_dataSet = ObjGlobal.SelectDataForMultipleTable(1, VouNo, "HBN", string.Empty, string.Empty, string.Empty);

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Room No");
                ColHeaders.Add("From Date");
                ColHeaders.Add("To Date");
                ColHeaders.Add("No Of Days");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(245);
                ColWidths.Add(100);
                ColWidths.Add(100);
                ColWidths.Add(100);
                ColWidths.Add(90);
                ColWidths.Add(90);

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
            if (_dataSet.Tables[1].Rows.Count > 0)
            {
                for (i = DRo; i < _dataSet.Tables[1].Rows.Count; i++) //foreach(DataRow ro in _vouDetails.Rows)
                {
                    //Set the cell height
                    _iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintHotelBookingHeader(e);
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
                            _iCellHeight), strFormat);
                    iCount++;

                    if (_dataSet.Tables[1].Rows[i]["Room_No"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(_dataSet.Tables[1].Rows[i]["Room_No"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["From_Date"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            Convert.ToDateTime(_dataSet.Tables[1].Rows[i]["From_Date"].ToString()).ToShortDateString(),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["To_Date"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            Convert.ToDateTime(_dataSet.Tables[1].Rows[i]["To_Date"].ToString()).ToShortDateString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["No_Of_Days"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(_dataSet.Tables[1].Rows[i]["No_Of_Days"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_dataSet.Tables[1].Rows[i]["No_Of_Days"].ToString())
                                .ToString(ObjGlobal.SysQtyFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_dataSet.Tables[1].Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["Net_Amount"].ToString() != null)
                    {
                        TotNetAmt = TotNetAmt + Convert.ToDouble(_dataSet.Tables[1].Rows[i]["Net_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_dataSet.Tables[1].Rows[i]["Net_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    _iRow++;
                    iTopMargin += _iCellHeight;
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
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + _iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 500, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysQtyFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotNetAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotNetAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {_dataSet.Tables[0].Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Remarks : ", myFont).Height + 50;
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics
                              .MeasureString("---------------------------------------------------------------------",
                                  myFont).Height +
                          1;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintHotelBookingHeader(PrintPageEventArgs e)
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
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "HBN")
                e.Graphics.DrawString("Booking Information", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else if (Module == "HBCN")
                e.Graphics.DrawString("Cancel Charge Receipt Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else if (Module == "HCI")
                e.Graphics.DrawString("Check In Advance Receipt Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Booking No", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Booking_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Date(AD)", myFont, Brushes.Black, new RectangleF(600, iTopMargin, 700, _iCellHeight),
                strFormat);
            e.Graphics.DrawString(
                $" : {Convert.ToDateTime(_dataSet.Tables[0].Rows[0]["Booking_Date"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(700, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Booking No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Guest Name", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["GuestName"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Advance", myFont, Brushes.Black, new RectangleF(600, iTopMargin, 700, _iCellHeight),
                strFormat);
            e.Graphics.DrawString(
                $" : {Convert.ToDecimal(_dataSet.Tables[0].Rows[0]["Advance_Amount"]).ToString(ObjGlobal.SysAmountFormat)}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(700, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Guest Name : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Agent Name", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Name"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Currency Name", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 700, _iCellHeight), strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Cur_Name"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(700, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Agent Name : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Booked By", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Booked_By"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Payment Mode", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 700, _iCellHeight), strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Payment_Type"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(700, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Currency Name : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("No of Adults", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 120, _iCellHeight), strFormat);
            if (Convert.ToInt32(_dataSet.Tables[0].Rows[0]["No_OfAdult"]) > 0)
                e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["No_OfAdult"]}", myFont, Brushes.Black,
                    new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            else
                e.Graphics.DrawString(" : ", myFont, Brushes.Black, new RectangleF(120, iTopMargin, 600, _iCellHeight),
                    strFormat);

            e.Graphics.DrawString("No of Childs", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 700, _iCellHeight), strFormat);
            if (Convert.ToInt32(_dataSet.Tables[0].Rows[0]["No_OfChild"]) > 0)
                e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["No_OfChild"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(700, iTopMargin, 800, _iCellHeight), strFormat);
            else
                e.Graphics.DrawString(" : ", myFont, Brushes.Black, new RectangleF(700, iTopMargin, 800, _iCellHeight),
                    strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("No of Adults : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;

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

            iTopMargin += (int)e.Graphics.MeasureString("Room No", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
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

    private void PrintHotelCheckInDetails(PrintPageEventArgs e)
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

            _iRow = 1;
            //Whether more pages have to print or not
            bMorePagesToPrint = false;

            var myFont = new Font("Arial", 9, FontStyle.Regular);
            if (bFirstPage)
            {
                TotNetAmt = 0;
                TotQty = 0;

                //_dataSet = ObjGlobal.SelectDataForMultipleTable(1, VouNo, "HCI", string.Empty, string.Empty, string.Empty);

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Room No");
                ColHeaders.Add("From Date");
                ColHeaders.Add("To Date");
                ColHeaders.Add("No Of Days");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Right");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(245);
                ColWidths.Add(100);
                ColWidths.Add(100);
                ColWidths.Add(100);
                ColWidths.Add(90);
                ColWidths.Add(90);

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
            if (_dataSet.Tables[1].Rows.Count > 0)
            {
                for (i = DRo; i < _dataSet.Tables[1].Rows.Count; i++) //foreach(DataRow ro in _vouDetails.Rows)
                {
                    //Set the cell height
                    _iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintHotelCheckInHeader(e);
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
                            _iCellHeight), strFormat);
                    iCount++;

                    if (_dataSet.Tables[1].Rows[i]["Room_No"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(_dataSet.Tables[1].Rows[i]["Room_No"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["From_Date"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            Convert.ToDateTime(_dataSet.Tables[1].Rows[i]["From_Date"].ToString()).ToShortDateString(),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["To_Date"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            Convert.ToDateTime(_dataSet.Tables[1].Rows[i]["To_Date"].ToString()).ToShortDateString(), myFont,
                            Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["Days"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(_dataSet.Tables[1].Rows[i]["Days"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_dataSet.Tables[1].Rows[i]["Days"].ToString()).ToString(ObjGlobal.SysQtyFormat),
                            myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_dataSet.Tables[1].Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    if (_dataSet.Tables[1].Rows[i]["Net_Amount"].ToString() != null)
                    {
                        TotNetAmt = TotNetAmt + Convert.ToDouble(_dataSet.Tables[1].Rows[i]["Net_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_dataSet.Tables[1].Rows[i]["Net_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    _iRow++;
                    iTopMargin += _iCellHeight;
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
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + _iCellHeight;
                }

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 500, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysQtyFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotNetAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            e.Graphics.DrawString($"Amount In Words : {ClsMoneyConversion.MoneyConversion(TotNetAmt.ToString())} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {_dataSet.Tables[0].Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Remarks : ", myFont).Height + 50;
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 1;
            e.Graphics.DrawString("---------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics
                              .MeasureString("---------------------------------------------------------------------",
                                  myFont).Height +
                          1;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintHotelCheckInHeader(PrintPageEventArgs e)
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
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Module == "HBN")
                e.Graphics.DrawString("Booking Information", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else if (Module == "HBCN")
                e.Graphics.DrawString("Cancel Charge Receipt Voucher", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            else if (Module == "HCI")
                e.Graphics.DrawString("Check In Information", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("CheckIn No", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["CheckIn_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("CheckIn Date(AD)", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 720, _iCellHeight), strFormat);
            e.Graphics.DrawString(
                $" : {Convert.ToDateTime(_dataSet.Tables[0].Rows[0]["CheckIn_Date"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(720, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("CheckIn No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Booking No", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Booking_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Booking Date(AD)", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 720, _iCellHeight), strFormat);
            e.Graphics.DrawString(
                $" : {Convert.ToDateTime(_dataSet.Tables[0].Rows[0]["Booking_Date"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(720, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Booking No : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Guest Name", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["GuestName"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Payment Mode", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 720, _iCellHeight), strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Payment_Type"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(720, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Guest Name : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Agent Name", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 120, _iCellHeight),
                strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["AgentName"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("Currency Name", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 720, _iCellHeight), strFormat);
            e.Graphics.DrawString($" : {_dataSet.Tables[0].Rows[0]["Cur_Name"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(720, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Agent Name : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("Booking Advance", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 120, _iCellHeight), strFormat);
            e.Graphics.DrawString(
                $" : {Convert.ToDecimal(_dataSet.Tables[0].Rows[0]["BAdvance_Amount"]).ToString(ObjGlobal.SysAmountFormat)}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(120, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString("CheckIn Advance", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 720, _iCellHeight), strFormat);
            e.Graphics.DrawString(
                $" : {Convert.ToDecimal(_dataSet.Tables[0].Rows[0]["CAdvance_Amount"]).ToString(ObjGlobal.SysAmountFormat)}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(720, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Booking Advance : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;

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

            iTopMargin += (int)e.Graphics.MeasureString("Room No", myFont).Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
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

    #endregion Hotel  Booking/Cancel/CheckIn

    #region Hotel Check Out Bill Default Design

    private void PrintHotelCheckOutBillHeader(PrintPageEventArgs e)
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
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(ObjGlobal.CompanyAddress, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.CompanyAddress, myFont).Height + 15;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("INVOICE", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("INVOICE", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Guest Name  : {_dtVouMain.Rows[0]["Guest_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 650, _iCellHeight), strFormat);
            e.Graphics.DrawString($"Bill No : {_dtVouMain.Rows[0]["CheckOut_No"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(650, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Bill No : ", myFont).Height + 5;

            if (_dtVouMain.Rows[0]["Address"] != null && _dtVouMain.Rows[0]["Address"].ToString() != string.Empty)
                e.Graphics.DrawString($"Address  : {_dtVouMain.Rows[0]["Address"]}{string.Empty}", myFont, Brushes.Black,
                    new RectangleF(15, iTopMargin, 650, _iCellHeight), strFormat);

            if (ObjGlobal.SysDateType == "AD")
                e.Graphics.DrawString(
                    $"Date(AD)   : {Convert.ToDateTime(_dtVouMain.Rows[0]["CheckOut_Date"].ToString()).ToShortDateString()}{string.Empty}",
                    myFont, Brushes.Black, new RectangleF(650, iTopMargin, 800, _iCellHeight), strFormat);
            else
                e.Graphics.DrawString($"Date(BS)   : {_dtVouMain.Rows[0]["CheckOut_BSDate"]}{string.Empty}", myFont,
                    Brushes.Black, new RectangleF(650, iTopMargin, 800, _iCellHeight), strFormat);

            iTopMargin += (int)e.Graphics.MeasureString("Date       : ", myFont).Height + 5;

            if (_dtVouMain.Rows[0]["Telphone_No"] != null &&
                _dtVouMain.Rows[0]["Telphone_No"].ToString() != string.Empty && _dtVouMain.Rows[0]["Mobile_No"] != null &&
                _dtVouMain.Rows[0]["Mobile_No"].ToString() != string.Empty)
            {
                if (_dtVouMain.Rows[0]["Mobile_No"] != null && _dtVouMain.Rows[0]["Mobile_No"].ToString() != string.Empty)
                    e.Graphics.DrawString(
                        $"Phone No       : {_dtVouMain.Rows[0]["Telphone_No"]} / {_dtVouMain.Rows[0]["Mobile_No"]} ",
                        myFont, Brushes.Black, new RectangleF(15, iTopMargin, 650, _iCellHeight), strFormat);
                else
                    e.Graphics.DrawString($"Phone No       : {_dtVouMain.Rows[0]["Telphone_No"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(15, iTopMargin, 650, _iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;

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
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
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

    private void PrintHotelCheckOutBillDetails(PrintPageEventArgs e)
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
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT CheckOut_No,CheckOut_Date,AMS.Return_NepaliDate(CONVERT(varchar, CheckOut_Date,101)) CheckOut_BsDate,L.Ledger_Code,(Case When L.Title=1 Then 'Mr.' When L.Title=2 Then 'Mrs.' When L.Title not in(1,2) Then 'Others' end + ' ' + L.First_Name + ' ' + L.Last_Name) as Guest_Name,Country,City, ";
                Query =
                    $"{Query} Address,Telphone_No,Mobile_No,Email_Id,HCOM.Remarks,IsNull(Bar_Charge,0)Bar_Charge,IsNull(Food_Charge,0)Food_Charge,IsNull(Laundary_Charge,0)Laundary_Charge,IsNull(Phone_Charge,0)Phone_Charge,IsNull(Internet_Charge,0)Internet_Charge,IsNull(Transport_Charge,0)Transport_Charge,IsNull(Misc_Charge,0)Misc_Charge,IsNull(Advance_Amount,0)Advance_Amount,IsNull(GrandTotal_Amount,0)GrandTotal_Amount FROM AMS.HTCheckOut_Main as HCOM ";
                Query = $"{Query} Inner Join AMS.HTGuestInfo as L on L.Guest_Id=HCOM.Guest_Id";
                Query =
                    $"{Query} Where HCOM.CheckOut_No='{VouNo}' and HCOM.Branch_Id={ObjGlobal.SysBranchId}  and HCOM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                _dtVouMain.Reset();
                _dtVouMain = GetConnection.SelectDataTableQuery(Query);
                if (_dtVouMain.Rows.Count <= 0) return;
                Query = "Select CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From ( ";
                Query =
                    $"{Query} Select Distinct SIM.CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then STD.Term_Rate else 0 end Term_Rate, ";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(STD.Term_Amount) Else  Sum(STD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT  ";
                Query =
                    $"{Query} Inner Join AMS.SalesTerm_Details as STD On BT.BT_Id=STD.Term_Id Inner Join AMS.HTCheckOut_Main as SIM On SIM.CheckOut_No=STD.Invoice_No  ";
                Query =
                    $"{Query} Where SIM.CheckOut_No='{VouNo}' and SIM.Branch_Id={ObjGlobal.SysBranchId}  and SIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=2 and Module='HCO' and BillTerm_Type in (1,2) AND STD.Term_Amount<>0  ";
                Query = $"{Query} Group By SIM.CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type  ";
                Query =
                    $"{Query} ) as aa Group By CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo ";
                _dtTermDetails.Reset();
                _dtTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = _dtTermDetails.Rows.Count;

                Query =
                    "Select Distinct SIM.CheckOut_No,Room_No + ' - Room Charges' as Room_No,Bill_Days,Rate,SID.Basic_Amount,SID.Term_Amount,SID.Net_Amount From AMS.HTCheckOut_Details as SID ";
                Query = $"{Query} Inner Join AMS.HTCheckOut_Main as SIM On SIM.CheckOut_No=SID.CheckOut_No";
                Query = $"{Query} Inner Join AMS.HTRoom as P On P.Room_Id=SID.Room_Id ";
                Query =
                    $"{Query} Where SIM.CheckOut_No='{VouNo}' and SIM.Branch_Id={ObjGlobal.SysBranchId}  and SIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                _vouDetails.Reset();
                _vouDetails = GetConnection.SelectDataTableQuery(Query);

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Days");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(510);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            _iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (_vouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < _vouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    _iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintHotelCheckOutBillHeader(e);
                        bNewPage = false;
                    }

                    LCnt = LCnt + 1;
                    DRo = DRo + 1;
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
                    e.Graphics.DrawString($"{_iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            _iCellHeight), strFormat);
                    iCount++;

                    if (_vouDetails.Rows[i]["Room_No"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(_vouDetails.Rows[i]["Room_No"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Bill_Days"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(_vouDetails.Rows[i]["Bill_Days"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Bill_Days"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Basic_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(_vouDetails.Rows[i]["Basic_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Basic_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    _iRow++;
                    iTopMargin += _iCellHeight;
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
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + _iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], _iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Food_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Food Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Food_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Bar_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Bar Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Bar_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Laundary_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Laundary Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Laundary_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Phone_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Phone Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Phone_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Transport_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Transport Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Transport_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Internet_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Internet Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Internet_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Misc_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Misc. Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Misc_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Advance_Amount"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Advance Amount ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    $" - {Convert.ToDecimal(_dtVouMain.Rows[0]["Advance_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat)}",
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (_dtTermDetails.Rows.Count > 0)
                foreach (DataRow DTRo in _dtTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], _iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(
                Convert.ToDecimal(_dtVouMain.Rows[0]["GrandTotal_Amount"].ToString())
                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            //e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Convert.ToDecimal(_dtVouMain.Rows[0]["GrandTotal_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat))} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            e.Graphics.DrawString($"Remarks : {_dtVouMain.Rows[0]["Remarks"]} ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("On Account Of", myFont).Height + 50;

            //e.Graphics.DrawString("           " + ObjGlobal._UserName + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("user name", myFont).Height) + 1;
            //e.Graphics.DrawString("     --------------------------                                                      ----------------------                                         -------------------------", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            //iTopMargin += (int)(e.Graphics.MeasureString("------------------------------", myFont).Height) + 1;
            //e.Graphics.DrawString("           Prepared By          " + "                                                   Checked By 	                                                " + " Approved By ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)850, (float)iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("user name", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void PrintHotelCheckOutInvoiceHeader(PrintPageEventArgs e)
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
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString(ObjGlobal.LogInCompany, myFont).Height + 5;

            myFont = new Font("Arial", 12, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("Invoice", myFont, Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Invoice", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Company PAN/VAT No : {_dataTable.Rows[0]["Pan_Number"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Company PAN/VAT No : ", myFont).Height + 5;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString($"Company Address : {_dataTable.Rows[0]["Address"]}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Company Address : ", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;

            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString($"Guest Name  : {_dtVouMain.Rows[0]["Guest_Name"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString($"Bill No   : {_dtVouMain.Rows[0]["CheckOut_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(600, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Bill No : ", myFont).Height + 5;

            //if (_dtVouMain.Rows[0]["Address"] != null && _dtVouMain.Rows[0]["Address"].ToString() != "")
            e.Graphics.DrawString($"Guest Address  : {_dtVouMain.Rows[0]["Address"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Date(BS[AD]): {Convert.ToDateTime(_dtVouMain.Rows[0]["CheckOut_BsDate"].ToString()).ToShortDateString()}{string.Empty}",
                myFont, Brushes.Black, new RectangleF(600, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Date(BS[AD]) : ", myFont).Height + 5;

            e.Graphics.DrawString($"Guest PAN/VAT No. :{_dtVouMain.Rows[0]["Pan_No"]}{string.Empty}", myFont,
                Brushes.Black, new RectangleF(15, iTopMargin, 600, _iCellHeight), strFormat);
            e.Graphics.DrawString($"[{_dtVouMain.Rows[0]["CheckOut_Date"]}]", myFont, Brushes.Black,
                new RectangleF(670, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Guest PAN/VAT No. ", myFont).Height + 5;

            e.Graphics.DrawString("Payment Terms : Cash/Cheque/Credit/Others" + string.Empty, myFont, Brushes.Black,
                new RectangleF(15, iTopMargin, 660, _iCellHeight), strFormat);
            e.Graphics.DrawString($"Print Date   : {DateTime.Now}{string.Empty}", myFont, Brushes.Black,
                new RectangleF(600, iTopMargin, 800, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Print Date : ", myFont).Height + 5;

            if (_dtVouMain.Rows[0]["Telphone_No"] != null &&
                _dtVouMain.Rows[0]["Telphone_No"].ToString() != string.Empty && _dtVouMain.Rows[0]["Mobile_No"] != null &&
                _dtVouMain.Rows[0]["Mobile_No"].ToString() != string.Empty)
            {
                if (_dtVouMain.Rows[0]["Mobile_No"] != null && _dtVouMain.Rows[0]["Mobile_No"].ToString() != string.Empty)
                    e.Graphics.DrawString(
                        $"Phone No       : {_dtVouMain.Rows[0]["Telphone_No"]} / {_dtVouMain.Rows[0]["Mobile_No"]} ",
                        myFont, Brushes.Black, new RectangleF(15, iTopMargin, 650, _iCellHeight), strFormat);
                else
                    e.Graphics.DrawString($"Phone No       : {_dtVouMain.Rows[0]["Telphone_No"]}{string.Empty}", myFont,
                        Brushes.Black, new RectangleF(15, iTopMargin, 650, _iCellHeight), strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Phone No : ", myFont).Height + 1;
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("-----------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;

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
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
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

    private void PrintHotelCheckOutInvoiceDetails(PrintPageEventArgs e)
    {
        try
        {
            PageWidth = e.MarginBounds.Width - e.MarginBounds.Width * 4 / 100;
            iLeftMargin = e.MarginBounds.Width * 3 / 100; //Set the left margin
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
                    "SELECT CheckOut_No,CheckOut_Date,AMS.Return_NepaliDate(CONVERT(varchar, CheckOut_Date,101)) CheckOut_BsDate,L.Ledger_Code,(Case When GI.Title=1 Then 'Mr.' When GI.Title=2 Then 'Mrs.' When GI.Title not in(1,2) Then 'Others' end + ' ' + GI.First_Name + ' ' + GI.Last_Name) as Guest_Name,GI.Country,GI.City, ";
                Query =
                    $"{Query} GI.Address,GI.Pan_No,Telphone_No,Mobile_No,GI.Email_Id,HCOM.Remarks,Food_Charge,Bar_Charge,Laundary_Charge,Phone_Charge,Transport_Charge,Internet_Charge,Misc_Charge,Advance_Amount,GrandTotal_Amount FROM AMS.HTCheckOut_Main as HCOM ";
                Query =
                    $"{Query} Inner Join AMS.HTGuestInfo as GI on GI.Guest_Id=HCOM.Guest_Id Inner Join AMS.Ledger as L On L.Ledger_Code=GI.Ledger_Code";
                Query =
                    $"{Query} Where HCOM.CheckOut_No='{VouNo}' and HCOM.Branch_Id={ObjGlobal.SysBranchId}  and HCOM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                _dtVouMain.Reset();
                _dtVouMain = GetConnection.SelectDataTableQuery(Query);
                if (_dtVouMain.Rows.Count <= 0) return;
                Query = "Select CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate,Sum(Term_Amount)Term_Amount From ( ";
                Query =
                    $"{Query} Select Distinct SIM.CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Case When BillTerm_Type=2 Then STD.Term_Rate else 0 end Term_Rate, ";
                Query =
                    $"{Query} Case When Sign='-' Then -Sum(STD.Term_Amount) Else  Sum(STD.Term_Amount) End Term_Amount from AMS.BillingTerm as BT  ";
                Query =
                    $"{Query} Inner Join AMS.SalesTerm_Details as STD On BT.BT_Id=STD.Term_Id Inner Join AMS.HTCheckOut_Main as SIM On SIM.CheckOut_No=STD.Invoice_No  ";
                Query =
                    $"{Query} Where SIM.CheckOut_No='{VouNo}' and SIM.Branch_Id={ObjGlobal.SysBranchId}  and SIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId} and Term_Type=2 and Module='HCO' and BillTerm_Type in (1,2) AND STD.Term_Amount<>0  ";
                Query = $"{Query} Group By SIM.CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Sign,Term_Rate,BillTerm_Type  ";
                Query =
                    $"{Query} ) as aa Group By CheckOut_No,BT_Id,BT_OrderNo,BT_Name,Term_Rate Order By BT_OrderNo ";
                _dtTermDetails.Reset();
                _dtTermDetails = GetConnection.SelectDataTableQuery(Query);
                N_TermDet = _dtTermDetails.Rows.Count;

                Query =
                    "Select Distinct SIM.CheckOut_No,Room_No + ' - Room Charges' as Room_No,Bill_Days,Rate,SID.Basic_Amount,SID.Term_Amount,SID.Net_Amount From AMS.HTCheckOut_Details as SID ";
                Query = $"{Query} Inner Join AMS.HTCheckOut_Main as SIM On SIM.CheckOut_No=SID.CheckOut_No";
                Query = $"{Query} Inner Join AMS.HTRoom as P On P.Room_Id=SID.Room_Id ";
                Query =
                    $"{Query} Where SIM.CheckOut_No='{VouNo}' and SIM.Branch_Id={ObjGlobal.SysBranchId}  and SIM.FiscalYear_Id={ObjGlobal.SysFiscalYearId}{string.Empty}";
                _vouDetails.Reset();
                _vouDetails = GetConnection.SelectDataTableQuery(Query);

                ColHeaders.Clear();
                ColHeaders.Add("S.N.");
                ColHeaders.Add("Particulars");
                ColHeaders.Add("Days");
                ColHeaders.Add("Rate");
                ColHeaders.Add("Amount");

                ColFormat.Clear();
                ColFormat.Add("Left");
                ColFormat.Add("Left");
                ColFormat.Add("Middle");
                ColFormat.Add("Right");
                ColFormat.Add("Right");

                ColWidths.Clear();
                ColWidths.Add(35);
                ColWidths.Add(510);
                ColWidths.Add(40);
                ColWidths.Add(100);
                ColWidths.Add(115);
            }

            long LCnt;
            LCnt = 0;
            _iRow = 1;
            bMorePagesToPrint = false;
            var myFont = new Font("Arial", 9, FontStyle.Regular);

            //Loop till all the grid rows not get printed
            if (_vouDetails.Rows.Count > 0)
            {
                for (i = DRo; i < _vouDetails.Rows.Count; i++)
                {
                    //Set the cell height
                    _iCellHeight = 20;
                    var iCount = 0;
                    //Check whether the current page settings allo more rows to print
                    if (iTopMargin + _iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }

                    if (bNewPage)
                    {
                        PrintHotelCheckOutInvoiceHeader(e);
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
                    e.Graphics.DrawString($"{_iRow}.", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                            _iCellHeight), strFormat);
                    iCount++;

                    if (_vouDetails.Rows[i]["Room_No"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(_vouDetails.Rows[i]["Room_No"].ToString(), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Bill_Days"].ToString() != null)
                    {
                        TotQty = TotQty + Convert.ToDouble(_vouDetails.Rows[i]["Bill_Days"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Bill_Days"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Rate"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;

                    if (_vouDetails.Rows[i]["Basic_Amount"].ToString() != null)
                    {
                        TotBasicAmt = TotBasicAmt + Convert.ToDouble(_vouDetails.Rows[i]["Basic_Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(
                            Convert.ToDecimal(_vouDetails.Rows[i]["Basic_Amount"].ToString())
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount],
                                _iCellHeight), strFormat);
                    }

                    iCount++;
                    _iRow++;
                    iTopMargin += _iCellHeight;
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
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty, myFont).Height + _iCellHeight;
                }
            else
                iTopMargin += 20;

            iLeftMargin = e.MarginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(Convert.ToDecimal(TotQty.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], _iCellHeight),
                strFormat);
            e.Graphics.DrawString(Convert.ToDecimal(TotBasicAmt.ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                Brushes.Black, new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Food_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Food Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Food_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Bar_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Bar Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Bar_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Laundary_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Laundary Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Laundary_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Phone_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Phone Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Phone_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Transport_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Transport Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Transport_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Internet_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Internet Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Internet_Charge"].ToString())
                        .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Misc_Charge"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Misc. Charges  ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    Convert.ToDecimal(_dtVouMain.Rows[0]["Misc_Charge"].ToString()).ToString(ObjGlobal.SysAmountFormat),
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            if (Convert.ToDecimal(_dtVouMain.Rows[0]["Advance_Amount"].ToString()) != 0)
            {
                strFormat.Alignment = StringAlignment.Near;
                myFont = new Font("Arial", 10, FontStyle.Regular);
                e.Graphics.DrawString("Advance Amount ", myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
                strFormat.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(
                    $" - {Convert.ToDecimal(_dtVouMain.Rows[0]["Advance_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat)}",
                    myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                    strFormat);
                iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;
            }

            TotGrandAmt = TotGrandAmt + TotBasicAmt;
            myFont = new Font("Arial", 10, FontStyle.Regular);

            if (_dtTermDetails.Rows.Count > 0)
            {
                var tai = 1;
                double Term_Amount = 0;
                foreach (DataRow DTRo in _dtTermDetails.Rows)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString($"{DTRo["BT_Name"]} : ", myFont, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, _iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Rate"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], _iCellHeight),
                        strFormat);
                    e.Graphics.DrawString(
                        Convert.ToDecimal(DTRo["Term_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat), myFont,
                        Brushes.Black,
                        new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                        strFormat);

                    iTopMargin += (int)e.Graphics.MeasureString(string.Empty + DTRo["BT_Name"] + string.Empty, myFont)
                        .Height + 5;

                    TotGrandAmt = TotGrandAmt + Convert.ToDouble(DTRo["Term_Amount"]);
                    Term_Amount = Term_Amount + Convert.ToDouble(DTRo["Term_Amount"]);
                    tai = tai + 1;

                    if (tai == _dtTermDetails.Rows.Count)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Taxable Amt. : ", myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[2], iTopMargin, 480, _iCellHeight), strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(string.Empty, myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], _iCellHeight),
                            strFormat);
                        e.Graphics.DrawString(
                            Convert.ToDecimal(
                                    Convert.ToDecimal(TotBasicAmt.ToString()) + Convert.ToDecimal(Term_Amount))
                                .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight),
                            strFormat);

                        iTopMargin += (int)e.Graphics.MeasureString("Taxable Amt. : ", myFont).Height + 5;
                    }
                }
            }

            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString("-----------------------------------------------------------------------", myFont,
                Brushes.Black, new RectangleF(500, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 5;
            myFont = new Font("Arial", 10, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString("Grand Total : ", myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[1], iTopMargin, 480, _iCellHeight), strFormat);
            //e.Graphics.DrawString(Convert.ToDecimal(TotGrandAmt.ToString()).ToString(ObjGlobal._Amount_Format), myFont, Brushes.Black, new RectangleF((int)arrColumnLefts[4], (float)iTopMargin, (int)arrColumnWidths[4], (float)iCellHeight), strFormat);
            e.Graphics.DrawString(
                Convert.ToDecimal(_dtVouMain.Rows[0]["GrandTotal_Amount"].ToString())
                    .ToString(ObjGlobal.SysAmountFormat), myFont, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Total", myFont).Height + 5;

            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10, FontStyle.Regular);
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;
            //e.Graphics.DrawString("Amount In Words : " + ClsMoneyConversion.MoneyConversion(TotGrandAmt.ToString()) + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            e.Graphics.DrawString(
                $"Amount In Words : {ClsMoneyConversion.MoneyConversion(Convert.ToDecimal(_dtVouMain.Rows[0]["GrandTotal_Amount"].ToString()).ToString(ObjGlobal.SysAmountFormat))} ",
                myFont, Brushes.Black, new RectangleF(15, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Amount In Words", myFont).Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 10;
            //if (_dtVouMain.Rows[0]["Remarks"].ToString() != "")
            //{
            //    e.Graphics.DrawString("Remarks : " + _dtVouMain.Rows[0]["Remarks"] + " ", myFont, Brushes.Black, new RectangleF(iLeftMargin, (float)iTopMargin, (int)PageWidth, (float)iCellHeight), strFormat);
            //    iTopMargin += (int)(e.Graphics.MeasureString("Remarks ", myFont).Height) + 50;
            //}
            e.Graphics.DrawString(
                "Note : " + "Payments received by cheques are subject to the collection by the concerned bank.", myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, PageWidth, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Note :", myFont).Height + 50;

            e.Graphics.DrawString(
                "     -----------------------------------                                                                                                  -------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("------------------------------", myFont).Height + 1;

            e.Graphics.DrawString("     Signature Of Receiver          ", myFont, Brushes.Black,
                new RectangleF(20, iTopMargin, 200, _iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            e.Graphics.DrawString($" For : {ObjGlobal.LogInCompany} ", myFont, Brushes.Black,
                new RectangleF(200, iTopMargin, 620, _iCellHeight), strFormat);
            iTopMargin += (int)e.Graphics.MeasureString("Signed By", myFont).Height + 5;
        }
        catch (Exception ex)
        {
            Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    #endregion Hotel Check Out Bill Default Design
}