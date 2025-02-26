using MrDAL.Global.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace PrintControl.Print.DirectPrint;

public class DocPrintingHospital
{
    public void PrintCashBankVoucher4InchRollPaper(string VouNo, string printer)
    {
        DateTime dateTime;
        double num;
        decimal num1;
        var stringBuilder = new StringBuilder();
        Query =
            "SELECT VNo,VDate,VMiti,GL.GlCode,GL.GlDesc,ChqNo,ChqDate,Remarks FROM CashBankMaster as VM Inner Join GeneralLedger as GL on GL.GlCode=VM.GlCode ";
        Query = string.Concat(Query, " Where VM.VNo ='", VouNo, "' ");
        DTVouMain.Reset();
        DTVouMain = GetConnection.SelectDataTableQuery(Query);
        CashVoucher = false;
        Query = string.Concat(
            "Select distinct GlCode as Code,GlDesc as Name from GeneralLedger as L  Where Catagory in ('Cash Book') and L.GlCode = '",
            DTVouMain.Rows[0]["GlCode"].ToString(), "' and LockBill<>'Y' ");
        DTTemp.Reset();
        DTTemp = GetConnection.SelectDataTableQuery(Query);
        if (DTTemp.Rows.Count > 0) CashVoucher = true;
        Query =
            "Select * from (SELECT VM.VNo,Gl.GlShortName,Gl.AccountingCode, GL.GlCode,GL.GlDesc,SL.SlCode,SL.SLDesc,";
        Query = string.Concat(Query,
            " Gl.Catagory,GL.AddressI ,GL.AddressII,GL.Mobile,GL.TelNoI,GL.TelNoII ,GL.ContactPerson,");
        Query = string.Concat(Query,
            " GL.PanNo,GL.PatientType ,GL.PatientGuardian,GL.ReferDoctor,GL.ReportedDoctor,HW.WardDesc,HB.BedNo,  ");
        Query = string.Concat(Query,
            " Sum(PayLocalAmt) LocalDrAmt,Sum(RecLocalAmt) LocalCrAmt,Narration FROM CashBankDetails as VD ");
        Query = string.Concat(Query, " Inner Join CashBankMaster as VM On VM.VNo=VD.VNo ");
        Query = string.Concat(Query,
            " Inner Join GeneralLedger as GL on GL.GlCode=VD.GlCode Left Outer Join SubLedger as SL On SL.SLCode=VD.SLCode ");
        Query = string.Concat(Query,
            " Left Outer Join HospitalWard as HW On HW.WardCode=Gl.WardCode Left Outer Join HospitalBeds as HB On HB.BedCode=Gl.BedCode ");
        Query = string.Concat(Query, " Where VM.VNo='", VouNo, "' ");
        Query = string.Concat(Query,
            " Group By VM.VNo,GL.GlCode,Gl.GlShortName,Gl.AccountingCode,GL.GlDesc,SL.SlCode,SL.SLDesc,Narration,Gl.Catagory,Gl.AddressI,Gl.AddressII,Gl.TelNoI,GL.TelNoII,Gl.PanNo,Gl.Mobile,GL.ContactPerson,Gl.PatientType,GL.ReferDoctor,Gl.PatientGuardian,ReportedDoctor,HW.WardDesc,HB.BedNo ");
        Query = string.Concat(Query, " ) as aa ");
        Query = string.Concat(Query, " Order by VNo,LocalDrAmt desc,LocalCrAmt");
        DTVouDetails.Reset();
        DTVouDetails = GetConnection.SelectDataTableQuery(Query);
        TotDrAmt = 0;
        TotCrAmt = 0;
        if (DTVouMain.Rows.Count > 0)
        {
            var str = string.Empty;
            DTCD = GetConnection.SelectDataTableQuery("SELECT CompanyName,Address,Phone,PanNo,City FROM companymaster");
            str = DTCD.Rows[0]["City"].ToString() == string.Empty
                ? DTCD.Rows[0]["Phone"].ToString()
                : string.Concat(DTCD.Rows[0]["City"].ToString(), ",", DTCD.Rows[0]["Phone"].ToString());
            var str1 = string.Concat("PAN/VAT NO : ", DTCD.Rows[0]["PanNo"].ToString());
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                DTCD.Rows[0]["CompanyName"].ToString().MyPadLeft(DTCD.Rows[0]["CompanyName"].ToString().Length +
                                                                 Convert.ToInt32((60 - DTCD.Rows[0]["CompanyName"]
                                                                     .ToString().Length) / 2)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                DTCD.Rows[0]["Address"].ToString().MyPadLeft(DTCD.Rows[0]["Address"].ToString().Length +
                                                             Convert.ToInt32((60 - DTCD.Rows[0]["Address"].ToString()
                                                                 .Length) / 2)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                str1.MyPadLeft(str1.Length + Convert.ToInt32((60 - str1.Length) / 2)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            if (Station == "RV")
            {
                if (CashVoucher)
                {
                    var stringBuilder1 = stringBuilder;
                    string[] shortDateString =
                    {
                        string.Empty.MyPadRight(15), "Cash Receipt Voucher", string.Empty.MyPadLeft(5), "Date    : ",
                        null
                    };
                    dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"]);
                    shortDateString[4] = dateTime.ToShortDateString();
                    stringBuilder1.Append(RawPrinterHelper.GetPrintString(
                        string.Concat(shortDateString) ?? string.Empty, RawPrinterHelper.PrintFontType.Contract));
                }
                else
                {
                    var stringBuilder2 = stringBuilder;
                    string[] strArrays =
                    {
                        string.Empty.MyPadRight(15), "Bank Receipt Voucher", string.Empty.MyPadLeft(5), "Date    : ",
                        null
                    };
                    dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"]);
                    strArrays[4] = dateTime.ToShortDateString();
                    stringBuilder2.Append(RawPrinterHelper.GetPrintString(string.Concat(strArrays) ?? string.Empty,
                        RawPrinterHelper.PrintFontType.Contract));
                }
            }
            else if (Station != "PV")
            {
                var stringBuilder3 = stringBuilder;
                string[] shortDateString1 =
                    { string.Empty.MyPadRight(15), "Cash/Bank Voucher", string.Empty.MyPadLeft(5), "Date    : ", null };
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"]);
                shortDateString1[4] = dateTime.ToShortDateString();
                stringBuilder3.Append(RawPrinterHelper.GetPrintString(string.Concat(shortDateString1) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
            }
            else if (CashVoucher)
            {
                var stringBuilder4 = stringBuilder;
                string[] strArrays1 =
                {
                    string.Empty.MyPadRight(15), "Cash Payment Voucher", string.Empty.MyPadLeft(5), "Date    : ", null
                };
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"]);
                strArrays1[4] = dateTime.ToShortDateString();
                stringBuilder4.Append(RawPrinterHelper.GetPrintString(string.Concat(strArrays1) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
            }
            else
            {
                var stringBuilder5 = stringBuilder;
                string[] shortDateString2 =
                {
                    string.Empty.MyPadRight(15), "Bank Payment Voucher", string.Empty.MyPadLeft(5), "Date    : ", null
                };
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"]);
                shortDateString2[4] = dateTime.ToShortDateString();
                stringBuilder5.Append(RawPrinterHelper.GetPrintString(string.Concat(shortDateString2) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
            }

            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Concat("Voucher No : ", DTVouMain.Rows[0]["VNo"].ToString()).MyPadRight(37),
                    string.Concat("Miti    : ", DTVouMain.Rows[0]["VMiti"].ToString()).MyPadRight(20)) ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            if (DTVouMain.Rows[0]["ChqNo"] != null && DTVouMain.Rows[0]["ChqNo"].ToString() != string.Empty)
            {
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat("Account   : ", DTVouMain.Rows[0]["GlDesc"].ToString()).MyPadRight(60),
                    RawPrinterHelper.PrintFontType.Contract));
                if (DTVouMain.Rows[0]["ChqDate"] != null && DTVouMain.Rows[0]["ChqDate"].ToString() != string.Empty)
                    stringBuilder.Append(RawPrinterHelper.GetPrintString(
                        string.Concat(
                            string.Concat("Cheque No : ", DTVouMain.Rows[0]["ChqNo"].ToString()).MyPadRight(37),
                            string.Concat("Chq Date  :", DTVouMain.Rows[0]["ChqDate"].ToString()).MyPadRight(20)) ??
                        string.Empty, RawPrinterHelper.PrintFontType.Contract));
                else if (ObjGlobal.SysDateType != "D")
                    stringBuilder.Append(RawPrinterHelper.GetPrintString(
                        string.Concat(
                            string.Concat("Cheque No : ", DTVouMain.Rows[0]["ChqNo"].ToString()).MyPadRight(37),
                            string.Concat("Chq Miti  :", DTVouMain.Rows[0]["ChqMiti"].ToString()).MyPadRight(20)) ??
                        string.Empty, RawPrinterHelper.PrintFontType.Contract));
                else
                    stringBuilder.Append(RawPrinterHelper.GetPrintString(
                        string.Concat(
                            string.Concat("Cheque No : ", DTVouMain.Rows[0]["ChqNo"].ToString()).MyPadRight(37),
                            string.Concat("Chq Date  :", DTVouMain.Rows[0]["ChqDate"].ToString()).MyPadRight(20)) ??
                        string.Empty, RawPrinterHelper.PrintFontType.Contract));
            }

            if (DTVouDetails.Rows.Count > 0)
            {
                stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat("Patient Name : ", DTVouDetails.Rows[0]["GlDesc"].ToString()).MyPadRight(60) ??
                    string.Empty, RawPrinterHelper.PrintFontType.Contract));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat("IPD Reg No   : ", DTVouDetails.Rows[0]["AccountingCode"].ToString())
                        .MyPadRight(60) ?? string.Empty, RawPrinterHelper.PrintFontType.Contract));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(
                        string.Concat("Address      : ", DTVouDetails.Rows[0]["AddressI"].ToString()).MyPadRight(37),
                        string.Concat("Age/Sex : ", DTVouDetails.Rows[0]["Mobile"].ToString(), "/",
                            DTVouDetails.Rows[0]["AddressII"].ToString()).MyPadRight(20)) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(
                        string.Concat("Tel No       : ", DTVouDetails.Rows[0]["TelNoI"].ToString()).MyPadRight(30),
                        string.Concat("Pan No : ", DTVouDetails.Rows[0]["PanNo"].ToString()).MyPadRight(30)) ??
                    string.Empty, RawPrinterHelper.PrintFontType.Contract));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(
                        string.Concat("Ward         : ", DTVouDetails.Rows[0]["WardDesc"].ToString()).MyPadRight(37),
                        string.Concat("Bed No : ", DTVouDetails.Rows[0]["BedNo"].ToString()).MyPadRight(20)) ??
                    string.Empty, RawPrinterHelper.PrintFontType.Contract));
                if (DTVouDetails.Rows[0]["ReferDoctor"].ToString() != string.Empty)
                    stringBuilder.Append(RawPrinterHelper.GetPrintString(
                        string.Concat("Refer Doctor : ", DTVouDetails.Rows[0]["ReferDoctor"].ToString())
                            .MyPadRight(60) ?? string.Empty, RawPrinterHelper.PrintFontType.Contract));
                if (DTVouDetails.Rows[0]["ReportedDoctor"].ToString() != string.Empty)
                    stringBuilder.Append(RawPrinterHelper.GetPrintString(
                        string.Concat("Reported Doctor: ", DTVouDetails.Rows[0]["ReportedDoctor"].ToString())
                            .MyPadRight(60) ?? string.Empty, RawPrinterHelper.PrintFontType.Contract));
                stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
                TotDrAmt = 0;
                TotCrAmt = 0;
                i = 0;
                while (i < DTVouDetails.Rows.Count)
                {
                    if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalDrAmt"].ToString())) !=
                        decimal.Zero)
                        TotDrAmt += Convert.ToDouble(
                            ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalDrAmt"].ToString()));
                    else if
                        (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalCrAmt"].ToString())) !=
                         decimal.Zero)
                        TotCrAmt += Convert.ToDouble(
                            ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalCrAmt"].ToString()));
                    i++;
                }
            }

            if (TotDrAmt - TotCrAmt <= 0)
            {
                var str2 = string.Empty.MyPadRight(20);
                var str3 = "Received Amount :".MyPadRight(20);
                var str4 = string.Empty.MyPadRight(2);
                num = Math.Abs(TotDrAmt - TotCrAmt);
                num1 = Convert.ToDecimal(num.ToString());
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(str2, str3, str4, num1.ToString("0.00").MyPadLeft(10)),
                    RawPrinterHelper.PrintFontType.Contract));
            }
            else
            {
                var str5 = string.Empty.MyPadRight(20);
                var str6 = "Payment Amount :".MyPadRight(20);
                var str7 = string.Empty.MyPadRight(2);
                num = Math.Abs(TotDrAmt - TotCrAmt);
                num1 = Convert.ToDecimal(num.ToString());
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(str5, str6, str7, num1.ToString("0.00").MyPadLeft(10)),
                    RawPrinterHelper.PrintFontType.Contract));
            }

            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            num = Math.Abs(TotDrAmt - TotCrAmt);
            var str8 = ClsMoneyConversion.MoneyConversion(num.ToString());
            var str9 = string.Empty;
            str9 = str8.Length < 48 ? str8 : str8.Substring(0, 48);
            var str10 = string.Empty;
            str10 = str8.Length - 48 < 58 ? str8.Remove(0, str9.Length) : str8.Substring(48, 58);
            var str11 = string.Empty;
            str11 = str8.Length - 86 < 58 ? str8.Remove(0, str9.Length + str10.Length) : str8.Substring(76, 48);
            var str12 = string.Empty;
            str12 = str8.Length - 124 < 58
                ? str8.Remove(0, str9.Length + str10.Length + str11.Length)
                : str8.Substring(124, 58);
            stringBuilder.Append(RawPrinterHelper.GetPrintString(string.Concat("In word : ", str9),
                RawPrinterHelper.PrintFontType.Contract));
            if (!string.IsNullOrEmpty(str10))
                stringBuilder.Append(RawPrinterHelper.GetPrintString(str10, RawPrinterHelper.PrintFontType.Contract));
            if (!string.IsNullOrEmpty(str11))
                stringBuilder.Append(RawPrinterHelper.GetPrintString(str11, RawPrinterHelper.PrintFontType.Contract));
            if (!string.IsNullOrEmpty(str12))
                stringBuilder.Append(RawPrinterHelper.GetPrintString(str12, RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            if (DTVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"].ToString()).MyPadRight(50),
                    RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(string.Empty.MyPadRight(60),
                RawPrinterHelper.PrintFontType.Contract));
            var str13 = string.Empty.MyPadRight(5);
            // string str14 = string.Concat("Cashier   : ", ObjGlobal._Name).MyPadRight(25);
            dateTime = DateTime.Now;
            //stringBuilder.Append(RawPrinterHelper.GetPrintString(string.Concat(str13, str14, string.Concat("Time : ", dateTime.ToString("HH:MM:ss")).MyPadRight(25)), RawPrinterHelper.PrintFontType.Contract, true));
            stringBuilder.Append("\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }

        RawPrinterHelper.SendStringToPrinter(printer, stringBuilder.ToString());
    }

    private void PrintCashBankVoucherDetails(PrintPageEventArgs e)
    {
        SizeF sizeF;
        double num;
        decimal num1;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            long num2 = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    "SELECT VNo,VDate,VMiti,GL.GlCode,GL.GlDesc,ChqNo,ChqDate,Remarks FROM CashBankMaster as VM Inner Join GeneralLedger as GL on GL.GlCode=VM.GlCode ";
                Query = string.Concat(Query, " Where VM.VNo ='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                CashVoucher = false;
                Query = string.Concat(
                    "Select distinct GlCode as Code,GlDesc as Name from GeneralLedger as L  Where Catagory in ('Cash Book') and L.GlCode = '",
                    DTVouMain.Rows[0]["GlCode"].ToString(), "' and LockBill<>'Y' ");
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);
                if (DTTemp.Rows.Count > 0) CashVoucher = true;
                Query = "Select * from (SELECT VM.VNo,Gl.GlShortName,GL.GlCode,GL.GlDesc,SL.SlCode,SL.SLDesc,";
                Query = string.Concat(Query,
                    " Gl.Catagory,GL.AddressI ,GL.AddressII,GL.Mobile,GL.TelNoI,GL.TelNoII ,GL.ContactPerson,");
                Query = string.Concat(Query,
                    " GL.PanNo,GL.PatientType ,GL.PatientGuardian,GL.ReferDoctor,GL.ReportedDoctor,  ");
                Query = string.Concat(Query,
                    " Sum(PayLocalAmt) LocalDrAmt,Sum(RecLocalAmt) LocalCrAmt,Narration FROM CashBankDetails as VD ");
                Query = string.Concat(Query, " Inner Join CashBankMaster as VM On VM.VNo=VD.VNo ");
                Query = string.Concat(Query,
                    " Inner Join GeneralLedger as GL on GL.GlCode=VD.GlCode Left Outer Join SubLedger as SL On SL.SLCode=VD.SLCode ");
                Query = string.Concat(Query, " Where VM.VNo='", VouNo, "' ");
                Query = string.Concat(Query,
                    " Group By VM.VNo,GL.GlCode,Gl.GlShortName,GL.GlDesc,SL.SlCode,SL.SLDesc,Narration,Gl.Catagory,Gl.AddressI,Gl.AddressII,Gl.TelNoI,GL.TelNoII,Gl.PanNo,Gl.Mobile,GL.ContactPerson,Gl.PatientType,GL.ReferDoctor,Gl.PatientGuardian,ReportedDoctor ");
                Query = string.Concat(Query, " ) as aa ");
                Query = string.Concat(Query, " Order by VNo,LocalDrAmt desc,LocalCrAmt");
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                ColWidths.Add(760);
                for (var i = 0; i < ColWidths.Count; i++)
                {
                    sizeF = e.Graphics.MeasureString("SNo.", font, Convert.ToInt16(ColWidths[i].ToString()));
                    iHeaderHeight = (int)sizeF.Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            if (DTVouDetails.Rows.Count > 0)
            {
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 20;
#pragma warning disable CS0219 // The variable 'num3' is assigned but its value is never used
                    var num3 = 0;
#pragma warning restore CS0219 // The variable 'num3' is assigned but its value is never used
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        if (bNewPage)
                        {
                            PrintCashBankVoucherHeader(e);
                            bNewPage = false;
                        }

                        DRo++;
                        num2 += 1;
                        num3 = 0;
                        font = new Font("Arial", 9f, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            string.Concat("Patient Name : ", DTVouDetails.Rows[0]["GlDesc"].ToString()) ?? string.Empty,
                            font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 390f, iCellHeight), strFormat);
                        e.Graphics.DrawString(
                            string.Concat("Patient Id : ", DTVouDetails.Rows[0]["GlShortName"].ToString()) ??
                            string.Empty, font, Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight),
                            strFormat);
                        e.Graphics.DrawString(
                            string.Concat("Patient Type  : ", DTVouDetails.Rows[0]["PatientType"].ToString()) ??
                            string.Empty, font, Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight),
                            strFormat);
                        var num4 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Patient Name    : ", font);
                        iTopMargin = num4 + (int)sizeF.Height + 5;
                        e.Graphics.DrawString(
                            string.Concat("Address : ", DTVouDetails.Rows[0]["AddressI"].ToString()) ?? string.Empty,
                            font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 390f, iCellHeight), strFormat);
                        e.Graphics.DrawString(string.Concat("Tel No  : ", DTVouDetails.Rows[0]["TelNoI"].ToString()),
                            font, Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight), strFormat);
                        e.Graphics.DrawString(string.Concat("Pan No  : ", DTVouDetails.Rows[0]["PanNo"].ToString()),
                            font, Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
                        var num5 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Address : ", font);
                        iTopMargin = num5 + (int)sizeF.Height + 5;
                        e.Graphics.DrawString(
                            string.Concat("Refer Doctor : ", DTVouDetails.Rows[0]["ReferDoctor"].ToString()) ??
                            string.Empty, font, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 390f, iCellHeight), strFormat);
                        e.Graphics.DrawString(
                            string.Concat("Reported Doctor : ", DTVouDetails.Rows[0]["ReportedDoctor"].ToString()) ??
                            string.Empty, font, Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight),
                            strFormat);
                        e.Graphics.DrawString(
                            string.Concat("Patient Guardian: ", DTVouDetails.Rows[0]["PatientGuardian"].ToString()) ??
                            string.Empty, font, Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight),
                            strFormat);
                        var num6 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Refer Doctor : ", font);
                        iTopMargin = num6 + (int)sizeF.Height + 1;
                        e.Graphics.DrawString(
                            "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                            font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                        var num7 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Refer Doctor : ", font);
                        iTopMargin = num7 + (int)sizeF.Height + 1;
                        if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalDrAmt"].ToString())) !=
                            decimal.Zero)
                            TotDrAmt += Convert.ToDouble(
                                ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalDrAmt"].ToString()));
                        else if (Convert.ToDecimal(
                                     ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalCrAmt"].ToString())) !=
                                 decimal.Zero)
                            TotCrAmt += Convert.ToDouble(
                                ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LocalCrAmt"].ToString()));
                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            font = new Font("Arial", 18f, FontStyle.Bold);
            if (TotDrAmt - TotCrAmt <= 0)
            {
                var graphics = e.Graphics;
                num = Math.Abs(TotDrAmt - TotCrAmt);
                num1 = Convert.ToDecimal(num.ToString());
                graphics.DrawString(string.Concat("Received Amount : ", num1.ToString("0.00")), font, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[0], iTopMargin, (int)arrColumnWidths[0], iCellHeight + 5f),
                    strFormat);
            }
            else
            {
                var graphic = e.Graphics;
                num = Math.Abs(TotDrAmt - TotCrAmt);
                num1 = Convert.ToDecimal(num.ToString());
                graphic.DrawString(string.Concat("Payment Amount : ", num1.ToString("0.00")), font, Brushes.Black,
                    new RectangleF(400f, iTopMargin, 800f, iCellHeight + 5f), strFormat);
            }

            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Payment Amount : ", font);
            iTopMargin = num8 + (int)sizeF.Height + 1;
            font = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num9 + (int)sizeF.Height + 1;
            font = new Font("Arial", 10f, FontStyle.Regular);
            var graphics1 = e.Graphics;
            num = Math.Abs(TotDrAmt - TotCrAmt);
            graphics1.DrawString(string.Concat("In Words : ", ClsMoneyConversion.MoneyConversion(num.ToString()), " "),
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("In Words", font);
            iTopMargin = num10 + (int)sizeF.Height + 1;
            font = new Font("Arial", 10f, FontStyle.Regular);
            e.Graphics.DrawString(
                "-----------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num11 + (int)sizeF.Height + 10;
            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length > 120)
            {
                e.Graphics.DrawString(
                    string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120), " "), font,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                var num12 = iTopMargin;
                sizeF = e.Graphics.MeasureString("Remarks", font);
                iTopMargin = num12 + (int)sizeF.Height + 5;
                if (RemarksTotLen <= 220)
                {
                    e.Graphics.DrawString(
                        string.Concat("                     ",
                            DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "), font,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        string.Concat("                     ",
                            DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "), font,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                    var num13 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("Remarks", font);
                    iTopMargin = num13 + (int)sizeF.Height + 5;
                    e.Graphics.DrawString(
                        string.Concat("                     ",
                            DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221), " "), font,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                }
            }
            else
            {
                e.Graphics.DrawString(string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"], " "), font,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            }

            var num14 = iTopMargin;
            sizeF = e.Graphics.MeasureString("On Account Of", font);
            iTopMargin = num14 + (int)sizeF.Height + 50;
            e.Graphics.DrawString(string.Concat("User : ", ObjGlobal.LogInUser, " "), font, Brushes.Black,
                new RectangleF(700f, iTopMargin, 800f, iCellHeight), strFormat);
            var num15 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num15 + (int)sizeF.Height + 1;
            var num16 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num16 + (int)sizeF.Height + 5;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintCashBankVoucherHeader(PrintPageEventArgs e)
    {
        try
        {
            var marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 15;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Station == "RV")
            {
                if (CashVoucher)
                    e.Graphics.DrawString("Cash Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Bank Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else if (Station != "PV")
            {
                e.Graphics.DrawString("Cash/Bank Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else if (CashVoucher)
            {
                e.Graphics.DrawString("Cash Payment Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString("Bank Payment Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }

            var num3 = iTopMargin;
            sizeF = e.Graphics.MeasureString(string.Empty, myFont);
            iTopMargin = num3 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            e.Graphics.DrawString(string.Concat("Voucher No   : ", DTVouMain.Rows[0]["VNo"]) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            var graphics = e.Graphics;
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"].ToString());
            var shortDateString = dateTime.ToShortDateString();
            dateTime = DateTime.Now;
            graphics.DrawString(
                string.Concat("Date : ", shortDateString, " ", dateTime.ToShortTimeString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Voucher No : ", myFont);
            iTopMargin = num4 + (int)sizeF.Height + 5;
            if (DTVouMain.Rows[0]["ChqNo"] == null ? true : DTVouMain.Rows[0]["ChqNo"].ToString() == string.Empty)
            {
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(string.Concat("Miti  : ", DTVouMain.Rows[0]["VMiti"].ToString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
                var num5 = iTopMargin;
                sizeF = e.Graphics.MeasureString("Date       : ", myFont);
                iTopMargin = num5 + (int)sizeF.Height + 5;
            }
            else
            {
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(string.Concat("Account  : ", DTVouMain.Rows[0]["GlDesc"].ToString()), myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 300f, iCellHeight), strFormat);
                e.Graphics.DrawString(string.Concat("Miti  : ", DTVouMain.Rows[0]["VMiti"].ToString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
                var num6 = iTopMargin;
                sizeF = e.Graphics.MeasureString("Date       : ", myFont);
                iTopMargin = num6 + (int)sizeF.Height + 5;
                strFormat.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(
                    string.Concat("Cheque No  : ", DTVouMain.Rows[0]["ChqNo"].ToString()) ?? string.Empty, myFont,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 300f, iCellHeight), strFormat);
                if (DTVouMain.Rows[0]["ChqDate"] == null
                        ? false
                        : DTVouMain.Rows[0]["ChqDate"].ToString() != string.Empty)
                {
                    if (ObjGlobal.SysDateType != "D")
                    {
                        var graphic = e.Graphics;
                        dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["ChqDate"].ToString());
                        //graphic.DrawString(string.Concat("Cheque Miti  : ", ObjGlobal._Posting_StartDate(dateTime.ToShortDateString())) ?? "", this.myFont, Brushes.Black, new RectangleF(300f, (float)this.iTopMargin, 800f, (float)this.iCellHeight), this.strFormat);
                    }
                    else
                    {
                        var graphics1 = e.Graphics;
                        dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["ChqDate"].ToString());
                        graphics1.DrawString(
                            string.Concat("Cheque Date  : ", dateTime.ToShortDateString()) ?? string.Empty, myFont,
                            Brushes.Black, new RectangleF(300f, iTopMargin, 800f, iCellHeight), strFormat);
                    }
                }
            }

            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 1;
            iTopMargin++;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    public string PrintDoc(bool print)
    {
        string msg;
        PD.BeginPrint += printDocument1_BeginPrint;
        PD.PrintPage += printDocument1_PrintPage;
        PPD.Click += printPreviewDialog1_Click;
        Printed = false;
        if (Convert.ToInt16(NoOf_Copy) > 0)
        {
            DTCD = GetConnection.SelectDataTableQuery(
                "SELECT CompanyName,Address,Phone,PanNo,EMail FROM companymaster");
            dtsys = GetConnection.SelectDataTableQuery("Select Vat from SystemControl");
            if (dtsys.Rows.Count > 0)
                if (dtsys.Rows[0]["Vat"].ToString() != string.Empty)
                    SysSalesVatId = Convert.ToInt16(dtsys.Rows[0]["Vat"].ToString());
            for (var i = 1; i <= Convert.ToInt16(NoOf_Copy); i++)
            {
                PD.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                if (Printer_Name.Trim() != string.Empty) PD.PrinterSettings.PrinterName = Printer_Name;
                if (DocDesign_Name == "Hospital A5")
                {
                    N_Line = 30;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA5", 550, 850);
                }
                else if (DocDesign_Name == "Hospital A5 Countinious")
                {
                    PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, 1000);
                }
                else if (DocDesign_Name == "Hospital 4Inch Countinious")
                {
                    PD.DefaultPageSettings.PaperSize = new PaperSize("Roll", 400, 700);
                }
                else if (DocDesign_Name == "Hospital A4 Half")
                {
                    N_Line = 13;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                }
                else if (DocDesign_Name == "Cash/Bank Voucher A4 Full" || DocDesign_Name == "Receipt Voucher"
                             ? true
                             : DocDesign_Name == "Payment Voucher")
                {
                    N_Line = 30;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                }
                else if (DocDesign_Name == "Cash Voucher A4 Half")
                {
                    N_Line = 5;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                }
                else if (DocDesign_Name == "Patient Discharge"
                             ? true
                             : DocDesign_Name == "Patient Discharge Saptarishi")
                {
                    N_Line = 30;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                }
                else if (DocDesign_Name == "Lab Report")
                {
                    N_Line = 35;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 1100);
                }

                Query = string.Empty;
                VouNo = string.Empty;
                if (Station == "HPB")
                {
                    Query = "SELECT BillNo VNo FROM CommissionEntry ";
                    if (FromDocNo.Trim() == string.Empty ? false : ToDocNo.Trim() != string.Empty)
                    {
                        Query = string.Concat(Query, " Where BillNo>='", FromDocNo, "' and BillNo<='", ToDocNo, "'");
                    }
                    else if (ObjGlobal.SysDateType != "D")
                    {
                        //  this.Query = string.Concat(new string[] { this.Query, " Where BillDate between '", ObjGlobal.DateToMiti(this.FromDate), "' and '", ObjGlobal.DateToMiti(this.ToDate), "'" });
                    }
                    else
                    {
                        Query = string.Concat(Query, " Where BillDate between '", FromDate, "' and '", ToDate, "'");
                    }
                }
                else if (Station == "CB")
                {
                    if (Station != "CB" ? false : DocDesign_Name == "Receipt")
                    {
                        N_Line = 5;
                        PD.DefaultPageSettings.PaperSize = new PaperSize("PaperA4", 850, 550);
                    }

                    Query = "SELECT VNo FROM CashBankMaster Where ";
                    if (FromDocNo.Trim() == string.Empty ? false : ToDocNo.Trim() != string.Empty)
                    {
                        Query = string.Concat(Query, "  VNo>='", FromDocNo, "' and VNo<='", ToDocNo, "'");
                    }
                    else if (ObjGlobal.SysDateType != "D")
                    {
                        string[] query = { Query, " VDate between '", null, null, null, null };
                        var dateTime = Convert.ToDateTime(FromDate);
                        // query[2] = ObjGlobal._Date_Type(dateTime.ToShortDateString());
                        query[3] = "' and '";
                        dateTime = Convert.ToDateTime(ToDate);
                        //query[4] = ObjGlobal._Date_Type(dateTime.ToShortDateString());
                        query[5] = "'";
                        Query = string.Concat(query);
                    }
                    else
                    {
                        Query = string.Concat(Query, " VDate between '", FromDate, "' and '", ToDate, "'");
                    }
                }
                else if (Station == "HPD")
                {
                    Query = "SELECT DischargeNo VNo FROM PatientDischargeMaster ";
                    if (FromDocNo.Trim() == string.Empty ? false : ToDocNo.Trim() != string.Empty)
                    {
                        Query = string.Concat(Query, " Where DischargeNo>='", FromDocNo, "' and DischargeNo<='",
                            ToDocNo, "'");
                    }
                    else if (ObjGlobal.SysDateType != "D")
                    {
                        //this.Query = string.Concat(new string[] { this.Query, " Where DischargeDate between '", ObjGlobal.DateToMiti(this.FromDate), "' and '", ObjGlobal.DateToMiti(this.ToDate), "'" });
                    }
                    else
                    {
                        Query = string.Concat(Query, " Where DischargeDate between '", FromDate, "' and '", ToDate,
                            "'");
                    }
                }
                else if (Station == "HLR")
                {
                    Query = "SELECT SampleNo as VNo FROM DMaster ";
                    if (FromDocNo.Trim() == string.Empty ? false : ToDocNo.Trim() != string.Empty)
                    {
                        Query = string.Concat(Query, " Where SampleNo>='", FromDocNo, "' and SampleNo<='", ToDocNo,
                            "'");
                    }
                    else if (ObjGlobal.SysDateType != "D")
                    {
                        //this.Query = string.Concat(new string[] { this.Query, " Where DDate between '", ObjGlobal.DateToMiti(this.FromDate), "' and '", ObjGlobal.DateToMiti(this.ToDate), "'" });
                    }
                    else
                    {
                        Query = string.Concat(Query, " Where DDate between '", FromDate, "' and '", ToDate, "'");
                    }
                }

                if (Query != string.Empty)
                {
                    DTNoOfVou.Reset();
                    DTNoOfVou = GetConnection.SelectDataTableQuery(Query);
                    if (DTNoOfVou.Rows.Count > 0)
                    {
                        foreach (DataRow row in DTNoOfVou.Rows)
                        {
                            VouNo = row["VNo"].ToString();
                            DRo = 0;
                            this.i = 0;
                            if (!print)
                            {
                                PPD.Document = PD;
                                PPD.PrintPreviewControl.Zoom = 1;
                                PPD.WindowState = FormWindowState.Maximized;
                                PPD.ShowDialog();
                            }
                            else
                            {
                                PD.PrintController = printController;
                                PD.Print();
                            }
                        }
                    }
                    else
                    {
                        Msg = "Does not exit Bill No!";
                        msg = Msg;
                        return msg;
                    }
                }
            }
        }

        if (print) Msg = "Print Completed!";
        msg = Msg;
        return msg;
    }

    private void printDocument1_BeginPrint(object sender, CancelEventArgs e)
    {
        try
        {
            strFormat = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            arrColumnLefts.Clear();
            arrColumnWidths.Clear();
            iCellHeight = 0;
            iCount = 0;
            i = 0;
            DRo = 0;
            bFirstPage = true;
            bNewPage = true;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        if (DocDesign_Name == "Hospital A4 Half")
            PrintHospitalBillHalfDetails(e);
        else if (DocDesign_Name == "Hospital A5")
            PrintHospitalBillA5Details(e);
        else if (DocDesign_Name == "Hospital A5 Countinious")
            PrintHospitalBillA5DetailsCountiniousPaper(e);
        else if (DocDesign_Name == "Hospital 4Inch Countinious")
            PrintHospitalBill4InchDetailsCountiniousPaper(e);
        else if (DocDesign_Name == "Cash Voucher A4 Half")
            PrintCashBankVoucherDetails(e);
        else if (Station == "JV" || Station == "CB" ? true : DocDesign_Name == "Cash/Bank Voucher A4 Full")
            PrintJVDetails(e);
        else if (DocDesign_Name == "Patient Discharge")
            PrintPatientDischargeDetails(e);
        else if (DocDesign_Name == "Patient Discharge Saptarishi")
            PrintPatientDischargeSaptaRishiDetails(e);
        else if (DocDesign_Name == "Lab Report") PrintPatientLabReportDetails(e);
    }

    private void PrintHospitalBill4InchDetailsCountiniousPaper(PrintPageEventArgs e)
    {
        decimal num;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SIM.BillNo,SIM.BillDate,SIM.BillMiti,GL.GLCode,Case When Gl.Catagory = 'Cash Book' Then isnull(GLP.GlShortName, Gl.GlShortName) Else Gl.GlShortName End GlShortName ,";
                Query = string.Concat(Query,
                    " Case When (SIM.PartyName IS NULL or SIM.PartyName='') Then Case When (Gl.PatientType IS  Not NULL or Gl.PatientType<>'') Then Substring(GL.GlDesc,1,Len(GL.GlDesc)-(len(GL.GlShortName)+1)) Else GL.GlDesc End Else Case When Gl.Catagory = 'Cash Book' Then 'Cash' Else Case When (Gl.PatientType IS  Not NULL or Gl.PatientType<>'') Then Substring(GL.GlDesc,1,Len(GL.GlDesc)-(len(GL.GlShortName)+1)) Else GL.GlDesc End End + ' ( '+ Case When GLP.GlShortName is null or GLP.GlShortName ='' Then SIM.PartyName Else Substring(SIM.PartyName,1,Len(SIM.PartyName)-(len(GLP.GlShortName)+1) ) End + ' )' End GlDesc, ");
                Query = string.Concat(Query,
                    " Gl.Catagory,Case When (Gl.AddressI IS NULL or Gl.AddressI='') Then GLP.AddressI Else GL.AddressI End AddressI,Case When (Gl.AddressII IS NULL or Gl.AddressII='') Then GLP.AddressII Else GL.AddressII End AddressII,Case When (Gl.Mobile IS NULL or Gl.Mobile='') Then GLP.Mobile Else GL.Mobile End Mobile,");
                Query = string.Concat(Query,
                    " Case When (Gl.TelNoI IS NULL or Gl.TelNoI='') Then GLP.TelNoI Else GL.TelNoI End TelNoI,Case When (Gl.TelNoII IS NULL or Gl.TelNoII='') Then GLP.TelNoII Else GL.TelNoII End TelNoII,Case When (Gl.ContactPerson IS NULL or Gl.ContactPerson='') Then GLP.ContactPerson Else GL.ContactPerson End ContactPerson, ");
                Query = string.Concat(Query,
                    " Case When (Gl.PanNo IS NULL or Gl.PanNo='') Then GLP.PanNo Else GL.PanNo End PanNo,Case When (Gl.PatientType IS NULL or Gl.PatientType='') Then GLP.PatientType Else GL.PatientType End PatientType,Case When (Gl.PatientGuardian IS NULL or Gl.PatientGuardian='') Then GLP.PatientGuardian Else GL.PatientGuardian End PatientGuardian, ");
                Query = string.Concat(Query,
                    " Case When (Gl.ReferDoctor IS NULL or Gl.ReferDoctor='') Then GLP.ReferDoctor Else GL.ReferDoctor End ReferDoctor,Case When (Gl.ReportedDoctor IS NULL or Gl.ReportedDoctor='') Then GLP.ReportedDoctor Else GL.ReportedDoctor End ReportedDoctor,NetAmount,SIM.Remarks,CreatedBy FROM CommissionEntry as SIM ");
                Query = string.Concat(Query,
                    " Inner Join GeneralLedger as GL on GL.GLCode=SIM.PId Left Outer Join GeneralLedger as GLP on GLP.GlDesc=SIM.PartyName ");
                Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count > 0)
                {
                    Query =
                        "Select Distinct SIM.BillNo,SID.SNo,P.PShortName,PDesc,SID.Qty,'PCS' Unit, SID.Rate,SID.HST,SID.Amount,SID.DisPer,SID.DisAmt,SID.BasicAmt,SID.HstPer ";
                    Query = string.Concat(Query,
                        " From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
                    Query = string.Concat(Query, " Inner Join Product as P On P.PCode=SID.PCode ");
                    Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
                    DTVouDetails.Reset();
                    DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                    Query = " Select BillNo,SNo,TermDesc,Sign,Sum(TermAmt) TermAmt From ( ";
                    Query = string.Concat(Query,
                        " Select Distinct SIM.BillNo,1 SNo,'Discount' TermDesc,'-' [Sign],Sum(Isnull(SID.DisAmt,0)) TermAmt From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
                    Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' Group BY SIM.BillNo ");
                    Query = string.Concat(Query, " Union All ");
                    Query = string.Concat(Query,
                        " Select Distinct SIM.BillNo,1 SNo,'Discount' TermDesc,'-' [Sign],Sum(Isnull(BDiscount,0)) TermAmt From CommissionEntry as SIM Where SIM.BillNo='",
                        VouNo, "' Group BY SIM.BillNo ");
                    Query = string.Concat(Query, " Union All ");
                    Query = string.Concat(Query,
                        " Select Distinct SIM.BillNo,2 SNo,'Service Tax' TermDesc,'+' [Sign],Sum(Isnull(SID.HST,0)) TermAmt From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
                    Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' Group BY SIM.BillNo ");
                    Query = string.Concat(Query, " ) as aa Group By BillNo,SNo,TermDesc,Sign Order By SNo ");
                    DTBillTerm.Reset();
                    DTBillTerm = GetConnection.SelectDataTableQuery(Query);
                    N_TermDet = 0;
                    ColHeaders.Clear();
                    ColHeaders.Add("SNo.");
                    ColHeaders.Add("Particulars");
                    ColHeaders.Add("Qty");
                    ColHeaders.Add("Unit");
                    ColHeaders.Add("Rate");
                    ColHeaders.Add("Amount");
                    ColFormat.Clear();
                    ColFormat.Add("Left");
                    ColFormat.Add("Left");
                    ColFormat.Add("Right");
                    ColFormat.Add("Left");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColWidths.Clear();
                    ColWidths.Add(35);
                    ColWidths.Add(120);
                    ColWidths.Add(55);
                    ColWidths.Add(40);
                    ColWidths.Add(40);
                    ColWidths.Add(60);
                }
                else
                {
                    return;
                }
            }

            long num1 = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouDetails.Rows.Count > 0)
            {
                iCellHeight = 15;
                var num2 = 0;
                PrintHospitalBill4InchHeader(e);
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    num1 += 1;
                    DRo++;
                    num2 = 0;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString(string.Concat(iRow.ToString(), "."), font, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2], iCellHeight),
                        strFormat);
                    num2++;
                    if (DTVouDetails.Rows[i]["PDesc"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PDesc"].ToString(), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty += Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString());
                        graphics.DrawString(num.ToString("0"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["Unit"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["Unit"].ToString(), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        var graphic = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString());
                        graphic.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["BasicAmt"].ToString() != null)
                    {
                        TotBasicAmt += Convert.ToDouble(DTVouDetails.Rows[i]["BasicAmt"]);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics1 = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["BasicAmt"].ToString());
                        graphics1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    iRow += 1;
                    iTopMargin += iCellHeight;
                    e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
                    i++;
                }
            }

            var paperSize = PD.DefaultPageSettings.PaperSize;
            paperSize.Height = paperSize.Height + iTopMargin;
            iTopMargin += 10;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString("--------------------------------------------------------------", font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 400f, iCellHeight), strFormat);
            var num3 = iTopMargin;
            var sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num3 + (int)sizeF.Height + 1;
            e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
            font = new Font("Arial", 8f, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 400f, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            var graphic1 = e.Graphics;
            num = Convert.ToDecimal(TotBasicAmt.ToString());
            graphic1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Total", font);
            iTopMargin = num4 + (int)sizeF.Height + 3;
            e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
            if (DTBillTerm.Rows.Count > 0)
            {
                i = 0;
                while (i < DTBillTerm.Rows.Count)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    e.Graphics.DrawString(DTBillTerm.Rows[i]["TermDesc"].ToString(), font, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[2], iTopMargin, 400f, iCellHeight), strFormat);
                    strFormat.Alignment = StringAlignment.Far;
                    var graphics2 = e.Graphics;
                    num = Convert.ToDecimal(DTBillTerm.Rows[i]["TermAmt"].ToString());
                    graphics2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight),
                        strFormat);
                    var num5 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("Total", font);
                    iTopMargin = num5 + (int)sizeF.Height + 3;
                    e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
                    if (DTBillTerm.Rows[i]["Sign"].ToString() != "+")
                        TotBasicAmt -= Convert.ToDouble(DTBillTerm.Rows[i]["TermAmt"]);
                    else
                        TotBasicAmt += Convert.ToDouble(DTBillTerm.Rows[i]["TermAmt"]);
                    i++;
                }
            }

            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 400f, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            var graphic2 = e.Graphics;
            num = Convert.ToDecimal(TotBasicAmt.ToString());
            graphic2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[5], iTopMargin, (int)arrColumnWidths[5], iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Total", font);
            iTopMargin = num6 + (int)sizeF.Height + 3;
            e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(10f, iTopMargin, 400f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num7 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("In Words : ", ClsMoneyConversion.MoneyConversion(TotBasicAmt.ToString()), " "), font,
                Brushes.Black, new RectangleF(20f, iTopMargin, 400f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Amount In Words", font);
            iTopMargin = num8 + (int)sizeF.Height + 1;
            e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(10f, iTopMargin, 400f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num9 + (int)sizeF.Height + 8;
            e.Graphics.DrawString(string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"], " "), font, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Remarks", font);
            iTopMargin = num10 + (int)sizeF.Height + 20;
            e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
            //e.Graphics.DrawString(string.Concat("           ", DataAccess.Name, " "), font, Brushes.Black, new RectangleF((float)this.iLeftMargin, (float)this.iTopMargin, (float)this.PageWidth, (float)this.iCellHeight), this.strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num11 + (int)sizeF.Height + 1;
            e.PageSettings.PaperSize.Height = e.PageSettings.PaperSize.Height + 15;
            e.Graphics.DrawString(
                "     --------------------                     ----------------------            ----------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            var num12 = iTopMargin;
            sizeF = e.Graphics.MeasureString("--------------------------", font);
            iTopMargin = num12 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "      Prepared By                           Checked By \t                  Approved By    ", font,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            var num13 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num13 + (int)sizeF.Height + 10;
            var height = PD.DefaultPageSettings.PaperSize;
            height.Height = height.Height + iTopMargin;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintHospitalBill4InchHeader(PrintPageEventArgs e)
    {
        try
        {
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 10f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(20f, iTopMargin, 400f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20f, iTopMargin, 400f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 10;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            if (DTCD.Rows[0]["PanNo"] == null ? false : DTCD.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString(string.Concat("PAN/VAT No : ", DTCD.Rows[0]["PanNo"].ToString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 400f, iCellHeight), strFormat);
                var num3 = iTopMargin;
                sizeF = e.Graphics.MeasureString("PAN/VAT No", myFont);
                iTopMargin = num3 + (int)sizeF.Height + 5;
            }

            myFont = new Font("Arial", 9f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("INVOICE", myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight),
                strFormat);
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            var graphics = e.Graphics;
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["BillDate"].ToString());
            graphics.DrawString(string.Concat("Date      : ", dateTime.ToShortDateString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(250f, iTopMargin, 400f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("INVOICE", myFont);
            iTopMargin = num4 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(string.Concat("Bill No : ", DTVouMain.Rows[0]["BillNo"]) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 250f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Miti         : ", DTVouMain.Rows[0]["BillMiti"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(250f, iTopMargin, 400f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Date       : ", myFont);
            iTopMargin = num5 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Patient Name : ", DTVouMain.Rows[0]["GlDesc"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 400f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Name : ", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(string.Concat("Address : ", DTVouMain.Rows[0]["AddressI"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 250f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Age/Sex : ", DTVouMain.Rows[0]["Mobile"].ToString(), " /",
                    DTVouMain.Rows[0]["AddressII"].ToString()), myFont, Brushes.Black,
                new RectangleF(250f, iTopMargin, 400f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Address : ", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 400f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("-----------", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 1;
            myFont = new Font("Arial", 8f, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;
                sizeF = e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString()));
                iHeaderHeight = (int)sizeF.Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Particulars", myFont);
            iTopMargin = num9 + (int)sizeF.Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 400f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num10 + (int)sizeF.Height + 1;
            iTopMargin++;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    public void PrintHospitalBill4InchRollPaper(string VouNo, string printer)
    {
        decimal num;
        var stringBuilder = new StringBuilder();
        Query =
            "SELECT SIM.BillNo,SIM.BillDate,SIM.BillMiti,GL.GLCode,Case When Gl.Catagory = 'Cash Book' Then isnull(GLP.GlShortName, Gl.GlShortName) Else Gl.GlShortName End GlShortName ,GLP.GlShortName RegId,";
        Query = string.Concat(Query,
            " Case When (SIM.PartyName IS NULL or SIM.PartyName='') Then Case When (Gl.PatientType IS  Not NULL or Gl.PatientType<>'') Then Substring(GL.GlDesc,1,Len(GL.GlDesc)-(len(GL.GlShortName)+1)) Else GL.GlDesc End Else Case When Gl.Catagory = 'Cash Book' Then 'Cash' Else Case When (Gl.PatientType IS  Not NULL or Gl.PatientType<>'') Then Substring(GL.GlDesc,1,Len(GL.GlDesc)-(len(GL.GlShortName)+1)) Else GL.GlDesc End End + ' ( '+ Case When GLP.GlShortName is null or GLP.GlShortName ='' Then SIM.PartyName Else Substring(SIM.PartyName,1,Len(SIM.PartyName)-(len(GLP.GlShortName)+1) ) End + ' )' End GlDesc, ");
        Query = string.Concat(Query,
            " Gl.Catagory,Case When (Gl.AddressI IS NULL or Gl.AddressI='') Then GLP.AddressI Else GL.AddressI End AddressI,Case When (Gl.AddressII IS NULL or Gl.AddressII='') Then GLP.AddressII Else GL.AddressII End AddressII,Case When (Gl.Mobile IS NULL or Gl.Mobile='') Then GLP.Mobile Else GL.Mobile End Mobile,");
        Query = string.Concat(Query,
            " Case When (Gl.TelNoI IS NULL or Gl.TelNoI='') Then GLP.TelNoI Else GL.TelNoI End TelNoI,Case When (Gl.TelNoII IS NULL or Gl.TelNoII='') Then GLP.TelNoII Else GL.TelNoII End TelNoII,Case When (Gl.ContactPerson IS NULL or Gl.ContactPerson='') Then GLP.ContactPerson Else GL.ContactPerson End ContactPerson, ");
        Query = string.Concat(Query,
            " Case When (Gl.PanNo IS NULL or Gl.PanNo='') Then GLP.PanNo Else GL.PanNo End PanNo,Case When (Gl.PatientType IS NULL or Gl.PatientType='') Then GLP.PatientType Else GL.PatientType End PatientType,Case When (Gl.PatientGuardian IS NULL or Gl.PatientGuardian='') Then GLP.PatientGuardian Else GL.PatientGuardian End PatientGuardian, ");
        Query = string.Concat(Query,
            " Case When (Gl.ReferDoctor IS NULL or Gl.ReferDoctor='') Then GLP.ReferDoctor Else GL.ReferDoctor End ReferDoctor,Case When (Gl.ReportedDoctor IS NULL or Gl.ReportedDoctor='') Then GLP.ReportedDoctor Else GL.ReportedDoctor End ReportedDoctor,NetAmount,AdvanceAmt,SIM.Remarks,CreatedBy FROM CommissionEntry as SIM ");
        Query = string.Concat(Query,
            " Inner Join GeneralLedger as GL on GL.GLCode=SIM.PId Left Outer Join GeneralLedger as GLP on GLP.GlDesc=SIM.PartyName ");
        Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
        DTVouMain.Reset();
        DTVouMain = GetConnection.SelectDataTableQuery(Query);
        if (DTVouMain.Rows.Count > 0)
        {
            var str = string.Empty;
            DTCD = GetConnection.SelectDataTableQuery("SELECT CompanyName,Address,Phone,PanNo,City FROM companymaster");
            str = DTCD.Rows[0]["City"].ToString() == string.Empty
                ? DTCD.Rows[0]["Phone"].ToString()
                : string.Concat(DTCD.Rows[0]["City"].ToString(), ",", DTCD.Rows[0]["Phone"].ToString());
            var str1 = string.Concat("PAN/VAT NO : ", DTCD.Rows[0]["PanNo"].ToString());
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                DTCD.Rows[0]["CompanyName"].ToString().MyPadLeft(DTCD.Rows[0]["CompanyName"].ToString().Length +
                                                                 Convert.ToInt32((60 - DTCD.Rows[0]["CompanyName"]
                                                                     .ToString().Length) / 2)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                DTCD.Rows[0]["Address"].ToString().MyPadLeft(DTCD.Rows[0]["Address"].ToString().Length +
                                                             Convert.ToInt32((60 - DTCD.Rows[0]["Address"].ToString()
                                                                 .Length) / 2)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                str1.MyPadLeft(str1.Length + Convert.ToInt32((60 - str1.Length) / 2)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            var stringBuilder1 = stringBuilder;
            string[] shortDateString =
            {
                string.Concat("ID : ", DTVouMain.Rows[0]["RegId"].ToString()).MyPadRight(25), "INVOICE",
                string.Empty.MyPadLeft(5), "Date    : ", null
            };
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["BillDate"]);
            shortDateString[4] = dateTime.ToShortDateString();
            stringBuilder1.Append(RawPrinterHelper.GetPrintString(string.Concat(shortDateString) ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Concat("Bill No      : ", DTVouMain.Rows[0]["BillNo"].ToString()).MyPadRight(37),
                    string.Concat("Miti    : ", DTVouMain.Rows[0]["BillMiti"].ToString()).MyPadRight(20)) ??
                string.Empty, RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat("Patient Name : ", DTVouMain.Rows[0]["GlDesc"].ToString()).MyPadRight(60) ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Concat("Address      : ", DTVouMain.Rows[0]["AddressI"].ToString()).MyPadRight(37),
                    string.Concat("Age/Sex : ", DTVouMain.Rows[0]["Mobile"].ToString(), "/",
                        DTVouMain.Rows[0]["AddressII"].ToString()).MyPadRight(20)) ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            if (DTVouMain.Rows[0]["Catagory"].ToString() == "Cash Book")
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(
                        string.Concat("Consultant   : ", DTVouMain.Rows[0]["ReferDoctor"].ToString()).MyPadRight(37),
                        "Payment Mode : CASH".MyPadRight(20)) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
            else if (DTVouMain.Rows[0]["Catagory"].ToString() != "Bank Book")
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(
                        string.Concat("Consultant   : ", DTVouMain.Rows[0]["ReferDoctor"].ToString()).MyPadRight(37),
                        "Payment Mode : CREDIT".MyPadRight(20)) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
            else
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(
                        string.Concat("Consultant   : ", DTVouMain.Rows[0]["ReferDoctor"].ToString()).MyPadRight(37),
                        "Payment Mode : CHEQUE".MyPadRight(20)) ?? string.Empty,
                    RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat("SN.".MyPadRight(3), string.Empty.MyPadRight(1), "Particulars".MyPadRight(25),
                    string.Empty.MyPadLeft(1), "Qty".MyPadLeft(4), string.Empty.MyPadLeft(1), "Unit".MyPadLeft(4),
                    string.Empty.MyPadLeft(1), "Rate".MyPadLeft(8), string.Empty.MyPadLeft(1), "Amount".MyPadLeft(9)) ??
                string.Empty, RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            Query =
                "Select Distinct SIM.BillNo,SID.SNo,P.PShortName,PDesc,SID.Qty,'PCS' Unit, SID.Rate,SID.HST,SID.Amount,SID.DisPer,SID.DisAmt,SID.BasicAmt,SID.HstPer ";
            Query = string.Concat(Query,
                " From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
            Query = string.Concat(Query, " Inner Join Product as P On P.PCode=SID.PCode ");
            Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
            DTVouDetails.Reset();
            DTVouDetails = GetConnection.SelectDataTableQuery(Query);
            Query = " Select BillNo,SNo,TermDesc,Sign,Sum(TermAmt) TermAmt From ( ";
            Query = string.Concat(Query,
                " Select Distinct SIM.BillNo,1 SNo,'Discount' TermDesc,'-' [Sign],Sum(Isnull(SID.DisAmt,0)) TermAmt From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
            Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' Group BY SIM.BillNo ");
            Query = string.Concat(Query, " Union All ");
            Query = string.Concat(Query,
                " Select Distinct SIM.BillNo,1 SNo,'Discount' TermDesc,'-' [Sign],Sum(Isnull(BDiscount,0)) TermAmt From CommissionEntry as SIM Where SIM.BillNo='",
                VouNo, "' Group BY SIM.BillNo ");
            Query = string.Concat(Query, " Union All ");
            Query = string.Concat(Query,
                " Select Distinct SIM.BillNo,2 SNo,'Service Tax' TermDesc,'+' [Sign],Sum(Isnull(SID.HST,0)) TermAmt From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
            Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' Group BY SIM.BillNo ");
            Query = string.Concat(Query,
                " ) as aa Group By BillNo,SNo,TermDesc,Sign  Having sum(TermAmt)<>0 Order By SNo ");
            DTBillTerm.Reset();
            DTBillTerm = GetConnection.SelectDataTableQuery(Query);
            TotBasicAmt = 0;
            if (DTVouDetails.Rows.Count > 0)
            {
                i = 0;
                while (i < DTVouDetails.Rows.Count)
                {
                    num = Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"]);
                    var str2 = num.ToString("0.00");
                    var strArrays = str2.Split('.');
                    if (Convert.ToInt32(strArrays[1]) <= 0) str2 = strArrays[0];
                    var stringBuilder2 = stringBuilder;
                    string[] strArrays1 =
                    {
                        string.Concat(DTVouDetails.Rows[i]["Sno"].ToString(), ".").MyPadRight(3),
                        string.Empty.MyPadLeft(1), DTVouDetails.Rows[i]["PDesc"].ToString().MyPadRight(25),
                        string.Empty.MyPadLeft(1), str2.MyPadLeft(4), string.Empty.MyPadLeft(1),
                        DTVouDetails.Rows[i]["Unit"].ToString().MyPadRight(4), string.Empty.MyPadLeft(1), null, null,
                        null
                    };
                    num = Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"]);
                    strArrays1[8] = num.ToString("0.00").MyPadLeft(8);
                    strArrays1[9] = string.Empty.MyPadLeft(1);
                    num = Convert.ToDecimal(DTVouDetails.Rows[i]["BasicAmt"]);
                    strArrays1[10] = num.ToString("0.00").MyPadLeft(9);
                    stringBuilder2.Append(RawPrinterHelper.GetPrintString(string.Concat(strArrays1) ?? string.Empty,
                        RawPrinterHelper.PrintFontType.Contract));
                    TotBasicAmt += Convert.ToDouble(DTVouDetails.Rows[i]["BasicAmt"].ToString());
                    i++;
                }
            }

            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat("-".MyPadLeft(20, ' '), "----------------------------------------") ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Empty.MyPadRight(20), "Total : ".MyPadRight(20), string.Empty.MyPadRight(8),
                    TotBasicAmt.ToString("0.00").MyPadLeft(10)), RawPrinterHelper.PrintFontType.Contract));
            if (DTBillTerm.Rows.Count > 0)
            {
                i = 0;
                while (i < DTBillTerm.Rows.Count)
                {
                    if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTBillTerm.Rows[i]["TermAmt"].ToString())) !=
                        decimal.Zero)
                    {
                        var str3 = string.Empty.MyPadRight(20);
                        var str4 = DTBillTerm.Rows[i]["TermDesc"].ToString().MyPadRight(20);
                        var str5 = string.Empty.MyPadRight(8);
                        num = Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTBillTerm.Rows[i]["TermAmt"].ToString()));
                        stringBuilder.Append(RawPrinterHelper.GetPrintString(
                            string.Concat(str3, str4, str5, num.ToString("0.00").MyPadLeft(10)),
                            RawPrinterHelper.PrintFontType.Contract));
                    }

                    if (DTBillTerm.Rows[i]["Sign"].ToString() != "+")
                        TotBasicAmt -= Convert.ToDouble(DTBillTerm.Rows[i]["TermAmt"]);
                    else
                        TotBasicAmt += Convert.ToDouble(DTBillTerm.Rows[i]["TermAmt"]);
                    i++;
                }

                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(string.Empty.MyPadRight(20), "Net Total : ".MyPadRight(20),
                        string.Empty.MyPadRight(8), TotBasicAmt.ToString("0.00").MyPadLeft(10)),
                    RawPrinterHelper.PrintFontType.Contract));
            }

            if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["AdvanceAmt"].ToString())) != decimal.Zero)
            {
                var str6 = string.Empty.MyPadRight(20);
                var str7 = "Deposit : ".MyPadRight(20);
                var str8 = string.Empty.MyPadRight(8);
                num = Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["AdvanceAmt"].ToString()));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(str6, str7, str8, string.Concat("-", num.ToString("0.00")).MyPadLeft(10)),
                    RawPrinterHelper.PrintFontType.Contract));
                var str9 = string.Empty.MyPadRight(20);
                var str10 = "Grand Total : ".MyPadRight(20);
                var str11 = string.Empty.MyPadRight(8);
                var totBasicAmt = TotBasicAmt -
                                  Convert.ToDouble(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["AdvanceAmt"].ToString()));
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat(str9, str10, str11, totBasicAmt.ToString("0.00").MyPadLeft(10)),
                    RawPrinterHelper.PrintFontType.Contract));
                TotBasicAmt -= Convert.ToDouble(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["AdvanceAmt"].ToString()));
            }

            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            var str12 = ClsMoneyConversion.MoneyConversion(TotBasicAmt.ToString());
            var str13 = string.Empty;
            str13 = str12.Length < 48 ? str12 : str12.Substring(0, 48);
            var str14 = string.Empty;
            str14 = str12.Length - 48 < 58 ? str12.Remove(0, str13.Length) : str12.Substring(48, 58);
            var str15 = string.Empty;
            str15 = str12.Length - 86 < 58 ? str12.Remove(0, str13.Length + str14.Length) : str12.Substring(76, 48);
            var str16 = string.Empty;
            str16 = str12.Length - 124 < 58
                ? str12.Remove(0, str13.Length + str14.Length + str15.Length)
                : str12.Substring(124, 58);
            stringBuilder.Append(RawPrinterHelper.GetPrintString(string.Concat("In word : ", str13),
                RawPrinterHelper.PrintFontType.Contract));
            if (!string.IsNullOrEmpty(str14))
                stringBuilder.Append(RawPrinterHelper.GetPrintString(str14, RawPrinterHelper.PrintFontType.Contract));
            if (!string.IsNullOrEmpty(str15))
                stringBuilder.Append(RawPrinterHelper.GetPrintString(str15, RawPrinterHelper.PrintFontType.Contract));
            if (!string.IsNullOrEmpty(str16))
                stringBuilder.Append(RawPrinterHelper.GetPrintString(str16, RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString("-".MyPadLeft(60, '-') ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            if (DTVouMain.Rows[0]["Remarks"].ToString() != string.Empty)
                stringBuilder.Append(RawPrinterHelper.GetPrintString(
                    string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"].ToString()).MyPadRight(50),
                    RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(string.Empty.MyPadRight(60),
                RawPrinterHelper.PrintFontType.Contract));
            var str17 = string.Empty.MyPadRight(5);
            var str18 = string.Concat("Cashier   : ", ObjGlobal.LogInUser).MyPadRight(25);
            dateTime = DateTime.Now;
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(str17, str18, string.Concat("Time : ", dateTime.ToString("HH:MM:ss")).MyPadRight(25)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append("\n\n\n\n\n\n\n\n\n\n\n\n\n");
        }

        RawPrinterHelper.SendStringToPrinter(printer, stringBuilder.ToString());
    }

    private void PrintHospitalBillA5Details(PrintPageEventArgs e)
    {
        decimal num;
        SizeF sizeF;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SIM.BillNo,SIM.BillDate,SIM.BillMiti,GL.GLCode,Gl.GlShortName,Case When (SIM.PartyName IS NULL or SIM.PartyName='') Then GL.GlDesc Else GL.GlDesc + ' ( '+ SIM.PartyName + ' )' End GlDesc,Gl.Catagory,Gl.AddressI,Gl.AddressII,Gl.Mobile,Gl.TelNoI,Gl.TelNoII,Gl.ContactPerson,Gl.PanNo,Gl.PatientType,Gl.PatientGuardian,Gl.ReferDoctor,Gl.ReportedDoctor, NetAmount,SIM.Remarks,CreatedBy FROM CommissionEntry as SIM ";
                Query = string.Concat(Query, " Inner Join GeneralLedger as GL on GL.GLCode=SIM.PId ");
                Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count > 0)
                {
                    Query =
                        "Select Distinct SIM.BillNo,SID.SNo,P.PShortName,PDesc,SID.Qty,SID.Rate,SID.HST,SID.Amount ";
                    Query = string.Concat(Query,
                        " From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
                    Query = string.Concat(Query, " Inner Join Product as P On P.PCode=SID.PCode ");
                    Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "'");
                    DTVouDetails.Reset();
                    DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                    N_TermDet = 0;
                    ColHeaders.Clear();
                    ColHeaders.Add("SNo.");
                    ColHeaders.Add("Particulars");
                    ColHeaders.Add("Qty");
                    ColHeaders.Add("Rate");
                    ColHeaders.Add("HST");
                    ColHeaders.Add("HST Amt");
                    ColHeaders.Add("Amount");
                    ColFormat.Clear();
                    ColFormat.Add("Left");
                    ColFormat.Add("Left");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColWidths.Clear();
                    ColWidths.Add(35);
                    ColWidths.Add(150);
                    ColWidths.Add(55);
                    ColWidths.Add(50);
                    ColWidths.Add(50);
                    ColWidths.Add(60);
                    ColWidths.Add(100);
                }
                else
                {
                    return;
                }
            }

            long num1 = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouDetails.Rows.Count > 0)
            {
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 15;
                    var num2 = 0;
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        if (bNewPage)
                        {
                            PrintHospitalBillA5Header(e);
                            bNewPage = false;
                        }

                        num1 += 1;
                        DRo++;
                        num2 = 0;
                        font = new Font("Arial", 8f, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(string.Concat(iRow.ToString(), "."), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                        num2++;
                        if (DTVouDetails.Rows[i]["PDesc"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PDesc"].ToString(), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                        {
                            TotQty += Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString());
                            graphics.DrawString(num.ToString("0"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphic = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString());
                            graphic.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["HST"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics1 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["HST"].ToString()) /
                                (Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()) *
                                 Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())) * new decimal(100);
                            graphics1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["HST"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphic1 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["HST"].ToString());
                            graphic1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Amount"].ToString() != null)
                        {
                            TotBasicAmt += Convert.ToDouble(DTVouDetails.Rows[i]["Amount"]);
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics2 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["Amount"].ToString());
                            graphics2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            if (num1 >= N_Line - N_TermDet - 9)
                iTopMargin += 20;
            else
                for (var i = num1; i <= N_Line - N_TermDet - 9; i += 1)
                {
                    e.Graphics.DrawString(" ", font, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    var num3 = iTopMargin;
                    sizeF = e.Graphics.MeasureString(string.Empty, font);
                    iTopMargin = num3 + (int)sizeF.Height + iCellHeight;
                }

            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString("----------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num4 + (int)sizeF.Height + 1;
            font = new Font("Arial", 8f, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480f, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            var graphic2 = e.Graphics;
            num = Convert.ToDecimal(TotBasicAmt.ToString());
            graphic2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Total", font);
            iTopMargin = num5 + (int)sizeF.Height + 3;
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(10f, iTopMargin, 550f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num6 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("In Words : ", ClsMoneyConversion.MoneyConversion(TotBasicAmt.ToString()), " "), font,
                Brushes.Black, new RectangleF(20f, iTopMargin, 550f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Amount In Words", font);
            iTopMargin = num7 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(10f, iTopMargin, 550f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num8 + (int)sizeF.Height + 8;
            e.Graphics.DrawString(string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"], " "), font, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("On Account Of", font);
            iTopMargin = num9 + (int)sizeF.Height + 20;
            //  e.Graphics.DrawString(string.Concat("           ", DataAccess.Name, " "), font, Brushes.Black, new RectangleF((float)this.iLeftMargin, (float)this.iTopMargin, (float)this.PageWidth, (float)this.iCellHeight), this.strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num10 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                       ----------------------                          -------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num11 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "           Prepared By                                              Checked By \t                                  Approved By ",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550f, iCellHeight), strFormat);
            var num12 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num12 + (int)sizeF.Height + 10;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintHospitalBillA5DetailsCountiniousPaper(PrintPageEventArgs e)
    {
        decimal num;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SIM.BillNo,SIM.BillDate,SIM.BillMiti,GL.GLCode,Gl.GlShortName,Case When (SIM.PartyName IS NULL or SIM.PartyName='') Then GL.GlDesc Else GL.GlDesc + ' ( '+ SIM.PartyName + ' )' End GlDesc,Gl.Catagory,Gl.AddressI,Gl.AddressII,Gl.Mobile,Gl.TelNoII,Gl.PatientType,Gl.PatientGuardian,Gl.ReferDoctor,Gl.ReportedDoctor, NetAmount,SIM.Remarks,CreatedBy FROM CommissionEntry as SIM ";
                Query = string.Concat(Query, " Inner Join GeneralLedger as GL on GL.GLCode=SIM.PId ");
                Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count > 0)
                {
                    Query =
                        "Select Distinct SIM.BillNo,SID.SNo,P.PShortName,PDesc,SID.Qty,SID.Rate,SID.HST,SID.Amount ";
                    Query = string.Concat(Query,
                        " From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
                    Query = string.Concat(Query, " Inner Join Product as P On P.PCode=SID.PCode ");
                    Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "'");
                    DTVouDetails.Reset();
                    DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                    N_TermDet = 0;
                    ColHeaders.Clear();
                    ColHeaders.Add("SNo.");
                    ColHeaders.Add("Particulars");
                    ColHeaders.Add("Qty");
                    ColHeaders.Add("Rate");
                    ColHeaders.Add("HST");
                    ColHeaders.Add("HST Amt");
                    ColHeaders.Add("Amount");
                    ColFormat.Clear();
                    ColFormat.Add("Left");
                    ColFormat.Add("Left");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColWidths.Clear();
                    ColWidths.Add(35);
                    ColWidths.Add(150);
                    ColWidths.Add(55);
                    ColWidths.Add(50);
                    ColWidths.Add(50);
                    ColWidths.Add(60);
                    ColWidths.Add(100);
                }
                else
                {
                    return;
                }
            }

            long num1 = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouDetails.Rows.Count > 0)
            {
                iCellHeight = 15;
                var num2 = 0;
                PrintHospitalBillA5Header(e);
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    num1 += 1;
                    DRo++;
                    num2 = 0;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString(string.Concat(iRow.ToString(), "."), font, Brushes.Black,
                        new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2], iCellHeight),
                        strFormat);
                    num2++;
                    if (DTVouDetails.Rows[i]["PDesc"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(DTVouDetails.Rows[i]["PDesc"].ToString(), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                    {
                        TotQty += Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString());
                        graphics.DrawString(num.ToString("0"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        var graphic = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString());
                        graphic.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["HST"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        if (Convert.ToDecimal(DTVouDetails.Rows[i]["HST"].ToString()) != decimal.Zero)
                        {
                            var graphics1 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["HST"].ToString()) /
                                (Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString()) *
                                 Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString())) * new decimal(100);
                            graphics1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["HST"].ToString() != null)
                    {
                        strFormat.Alignment = StringAlignment.Far;
                        var graphic1 = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["HST"].ToString());
                        graphic1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    if (DTVouDetails.Rows[i]["Amount"].ToString() != null)
                    {
                        TotBasicAmt += Convert.ToDouble(DTVouDetails.Rows[i]["Amount"]);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics2 = e.Graphics;
                        num = Convert.ToDecimal(DTVouDetails.Rows[i]["Amount"].ToString());
                        graphics2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                    }

                    num2++;
                    iRow += 1;
                    iTopMargin += iCellHeight;
                    PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, iTopMargin);
                    i++;
                }
            }

            iTopMargin += 10;
            PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, iTopMargin);
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString("----------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 550f, iCellHeight), strFormat);
            var num3 = iTopMargin;
            var sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num3 + (int)sizeF.Height + 1;
            PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, iTopMargin);
            font = new Font("Arial", 8f, FontStyle.Bold);
            e.Graphics.DrawString("Grand Total : ", font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, 480f, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            var graphic2 = e.Graphics;
            num = Convert.ToDecimal(TotBasicAmt.ToString());
            graphic2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnWidths[6], iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Total", font);
            iTopMargin = num4 + (int)sizeF.Height + 3;
            PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, iTopMargin);
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(10f, iTopMargin, 550f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num5 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("In Words : ", ClsMoneyConversion.MoneyConversion(TotBasicAmt.ToString()), " "), font,
                Brushes.Black, new RectangleF(20f, iTopMargin, 550f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Amount In Words", font);
            iTopMargin = num6 + (int)sizeF.Height + 1;
            PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, iTopMargin);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(10f, iTopMargin, 550f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num7 + (int)sizeF.Height + 8;
            e.Graphics.DrawString(string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"], " "), font, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("On Account Of", font);
            iTopMargin = num8 + (int)sizeF.Height + 20;
            PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, 300 + iTopMargin);
            e.Graphics.DrawString(string.Concat("           ", ObjGlobal.LogInUser, " "), font, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num9 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                       ----------------------                          -------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num10 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "           Prepared By                                              Checked By \t                                  Approved By ",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 550f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num11 + (int)sizeF.Height + 10;
            PD.DefaultPageSettings.PaperSize = new PaperSize("Countinious", 550, 300 + iTopMargin);
            var paperSize = PD.DefaultPageSettings.PaperSize;
            paperSize.Height = paperSize.Height + iTopMargin;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintHospitalBillA5Header(PrintPageEventArgs e)
    {
        try
        {
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 10f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(20f, iTopMargin, 550f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20f, iTopMargin, 550f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 10;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            if (DTCD.Rows[0]["PanNo"] == null ? false : DTCD.Rows[0]["PanNo"].ToString() != string.Empty)
            {
                e.Graphics.DrawString(string.Concat("PAN/VAT No : ", DTCD.Rows[0]["PanNo"].ToString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 300f, iCellHeight), strFormat);
                var num3 = iTopMargin;
                sizeF = e.Graphics.MeasureString("PAN/VAT No", myFont);
                iTopMargin = num3 + (int)sizeF.Height + 5;
            }

            myFont = new Font("Arial", 9f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("INVOICE", myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 550f, iCellHeight),
                strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("INVOICE", myFont);
            iTopMargin = num4 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(string.Concat("Bill No : ", DTVouMain.Rows[0]["BillNo"]) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight), strFormat);
            if (ObjGlobal.SysDateType != "D")
            {
                e.Graphics.DrawString(
                    string.Concat("Miti  : ", DTVouMain.Rows[0]["BillMiti"].ToString()) ?? string.Empty, myFont,
                    Brushes.Black, new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            }
            else
            {
                var graphics = e.Graphics;
                var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["BillDate"].ToString());
                graphics.DrawString(string.Concat("Date  : ", dateTime.ToShortDateString()) ?? string.Empty, myFont,
                    Brushes.Black, new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            }

            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Date       : ", myFont);
            iTopMargin = num5 + (int)sizeF.Height + 5;
            if (DTVouMain.Rows[0]["Catagory"].ToString() != "Cash Book")
                e.Graphics.DrawString(
                    string.Concat("Patient Name : ",
                        DTVouMain.Rows[0]["GlDesc"].ToString().Substring(0,
                            DTVouMain.Rows[0]["GlDesc"].ToString().Length -
                            (DTVouMain.Rows[0]["GlShortName"].ToString().Length + 1))) ?? string.Empty, myFont,
                    Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    string.Concat("Patient Name : ", DTVouMain.Rows[0]["GlDesc"].ToString()) ?? string.Empty, myFont,
                    Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Patient Id : ", DTVouMain.Rows[0]["GlShortName"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Name : ", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(string.Concat("Address : ", DTVouMain.Rows[0]["AddressI"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Age/Sex : ", DTVouMain.Rows[0]["Mobile"].ToString(), " /",
                    DTVouMain.Rows[0]["AddressII"].ToString()), myFont, Brushes.Black,
                new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Address : ", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Patient Type  : ", DTVouMain.Rows[0]["PatientType"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Patient Guardian: ", DTVouMain.Rows[0]["PatientGuardian"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Guardian : ", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Refer Doctor : ", DTVouMain.Rows[0]["ReferDoctor"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 350f, iCellHeight), strFormat);
            if (DTVouMain.Rows[0]["Catagory"].ToString() == "Cash Book")
                e.Graphics.DrawString("Payment Mode : Cash", myFont, Brushes.Black,
                    new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            else if (DTVouMain.Rows[0]["Catagory"].ToString() != "Bank Book")
                e.Graphics.DrawString("Payment Mode : Credit", myFont, Brushes.Black,
                    new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            else
                e.Graphics.DrawString("Payment Mode : Cheque", myFont, Brushes.Black,
                    new RectangleF(350f, iTopMargin, 550f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num9 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Reported Doctor : ", DTVouMain.Rows[0]["ReportedDoctor"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 550f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Reported Doctor : ", myFont);
            iTopMargin = num10 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 550f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("-----------", myFont);
            iTopMargin = num11 + (int)sizeF.Height + 1;
            myFont = new Font("Arial", 8f, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;
                sizeF = e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString()));
                iHeaderHeight = (int)sizeF.Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            var num12 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Particulars", myFont);
            iTopMargin = num12 + (int)sizeF.Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(
                "------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 550f, iCellHeight), strFormat);
            var num13 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num13 + (int)sizeF.Height + 1;
            iTopMargin++;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintHospitalBillHalfDetails(PrintPageEventArgs e)
    {
        decimal num;
        SizeF sizeF;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            if (bFirstPage)
            {
                TotAltQty = 0;
                TotQty = 0;
                TotBasicAmt = 0;
                TotGrandAmt = 0;
                TotNetAmt = 0;
                Query =
                    "SELECT SIM.BillNo,SIM.BillDate,SIM.BillMiti,GL.GLCode,Case When Gl.Catagory = 'Cash Book' Then isnull(GLP.GlShortName, Gl.GlShortName) Else Gl.GlShortName End GlShortName ,";
                Query = string.Concat(Query,
                    " Case When (SIM.PartyName IS NULL or SIM.PartyName='') Then Case When (Gl.PatientType IS  Not NULL or Gl.PatientType<>'') Then Substring(GL.GlDesc,1,Len(GL.GlDesc)-(len(GL.GlShortName)+1)) Else GL.GlDesc End Else Case When Gl.Catagory = 'Cash Book' Then 'Cash' Else Case When (Gl.PatientType IS  Not NULL or Gl.PatientType<>'') Then Substring(GL.GlDesc,1,Len(GL.GlDesc)-(len(GL.GlShortName)+1)) Else GL.GlDesc End End + ' ( '+ Case When GLP.GlShortName is null or GLP.GlShortName ='' Then SIM.PartyName Else Substring(SIM.PartyName,1,Len(SIM.PartyName)-(len(GLP.GlShortName)+1) ) End + ' )' End GlDesc, ");
                Query = string.Concat(Query,
                    " Gl.Catagory,Case When (Gl.AddressI IS NULL or Gl.AddressI='') Then GLP.AddressI Else GL.AddressI End AddressI,Case When (Gl.AddressII IS NULL or Gl.AddressII='') Then GLP.AddressII Else GL.AddressII End AddressII,Case When (Gl.Mobile IS NULL or Gl.Mobile='') Then GLP.Mobile Else GL.Mobile End Mobile,");
                Query = string.Concat(Query,
                    " Case When (Gl.TelNoI IS NULL or Gl.TelNoI='') Then GLP.TelNoI Else GL.TelNoI End TelNoI,Case When (Gl.TelNoII IS NULL or Gl.TelNoII='') Then GLP.TelNoII Else GL.TelNoII End TelNoII,Case When (Gl.ContactPerson IS NULL or Gl.ContactPerson='') Then GLP.ContactPerson Else GL.ContactPerson End ContactPerson, ");
                Query = string.Concat(Query,
                    " Case When (Gl.PanNo IS NULL or Gl.PanNo='') Then GLP.PanNo Else GL.PanNo End PanNo,Case When (Gl.PatientType IS NULL or Gl.PatientType='') Then GLP.PatientType Else GL.PatientType End PatientType,Case When (Gl.PatientGuardian IS NULL or Gl.PatientGuardian='') Then GLP.PatientGuardian Else GL.PatientGuardian End PatientGuardian, ");
                Query = string.Concat(Query,
                    " Case When (Gl.ReferDoctor IS NULL or Gl.ReferDoctor='') Then GLP.ReferDoctor Else GL.ReferDoctor End ReferDoctor,Case When (Gl.ReportedDoctor IS NULL or Gl.ReportedDoctor='') Then GLP.ReportedDoctor Else GL.ReportedDoctor End ReportedDoctor,BAmount,BDiscount,NetAmount,SIM.Remarks,CreatedBy FROM CommissionEntry as SIM ");
                Query = string.Concat(Query,
                    " Inner Join GeneralLedger as GL on GL.GLCode=SIM.PId Left Outer Join GeneralLedger as GLP on GLP.GlDesc=SIM.PartyName ");
                Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                if (DTVouMain.Rows.Count > 0)
                {
                    Query =
                        "Select Distinct SIM.BillNo,SID.SNo,P.PShortName,PDesc,SID.Qty,SID.Rate,SID.HST,SID.Amount,SID.DisPer,SID.DisAmt,SID.BasicAmt,SID.HstPer ";
                    Query = string.Concat(Query,
                        " From CommissionItem as SID Inner Join CommissionEntry as SIM On SIM.BillNo=SID.BillNo ");
                    Query = string.Concat(Query, " Inner Join Product as P On P.PCode=SID.PCode ");
                    Query = string.Concat(Query, " Where SIM.BillNo='", VouNo, "' ");
                    DTVouDetails.Reset();
                    DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                    Query =
                        "Select RecLocalAmt Advance From CashBankMaster as CBM Inner Join CashBankDetails as CBD  On CBM.VNo=CBD.VNo";
                    Query = string.Concat(Query, " Where CBM.RefNo='", VouNo, "'");
                    dtadv.Reset();
                    dtadv = GetConnection.SelectDataTableQuery(Query);
                    if (dtadv.Rows.Count > 0) N_Line = 13 - dtadv.Rows.Count - 1;
                    N_TermDet = 0;
                    ColHeaders.Clear();
                    ColHeaders.Add("SNo.");
                    ColHeaders.Add("Particulars");
                    ColHeaders.Add("Qty");
                    ColHeaders.Add("Rate");
                    ColHeaders.Add("Amount");
                    ColHeaders.Add("Dis");
                    ColHeaders.Add("Dis Amt");
                    ColHeaders.Add("HST");
                    ColHeaders.Add("HST Amt");
                    ColHeaders.Add("Net Amt");
                    ColFormat.Clear();
                    ColFormat.Add("Left");
                    ColFormat.Add("Left");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColFormat.Add("Right");
                    ColWidths.Clear();
                    ColWidths.Add(35);
                    ColWidths.Add(200);
                    ColWidths.Add(55);
                    ColWidths.Add(50);
                    ColWidths.Add(90);
                    ColWidths.Add(50);
                    ColWidths.Add(70);
                    ColWidths.Add(60);
                    ColWidths.Add(70);
                    ColWidths.Add(90);
                }
                else
                {
                    return;
                }
            }

            long num1 = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouDetails.Rows.Count > 0)
            {
                this.i = DRo;
                while (this.i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 15;
                    var num2 = 0;
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        if (bNewPage)
                        {
                            PrintHospitalBillHalfHeader(e);
                            bNewPage = false;
                        }

                        num1 += 1;
                        DRo++;
                        num2 = 0;
                        font = new Font("Arial", 8f, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(string.Concat(iRow.ToString(), "."), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                        num2++;
                        if (DTVouDetails.Rows[i]["PDesc"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["PDesc"].ToString(), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Qty"].ToString() != null)
                        {
                            TotQty += Convert.ToDouble(DTVouDetails.Rows[i]["Qty"]);
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["Qty"].ToString());
                            graphics.DrawString(num.ToString("0"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Rate"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphic = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["Rate"].ToString());
                            graphic.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["BasicAmt"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics1 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["BasicAmt"].ToString());
                            graphics1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["DisPer"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphic1 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["DisPer"].ToString());
                            graphic1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["DisAmt"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics2 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["DisAmt"].ToString());
                            graphics2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["HSTPer"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphic2 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["HSTPer"].ToString());
                            graphic2.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["HST"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Far;
                            var graphics3 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["HST"].ToString());
                            graphics3.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Amount"].ToString() != null)
                        {
                            TotBasicAmt += Convert.ToDouble(DTVouDetails.Rows[i]["Amount"]);
                            strFormat.Alignment = StringAlignment.Far;
                            var graphic3 = e.Graphics;
                            num = Convert.ToDecimal(DTVouDetails.Rows[i]["Amount"].ToString());
                            graphic3.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                    if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["BAmount"].ToString())) !=
                        decimal.Zero) N_Line -= 2;
                    if (num1 >= N_Line - N_TermDet - 5)
                        iTopMargin += 20;
                    else
                        for (var i = num1; i <= N_Line - N_TermDet - 5; i += 1)
                        {
                            e.Graphics.DrawString(" ", font, Brushes.Black,
                                new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                            var num3 = iTopMargin;
                            sizeF = e.Graphics.MeasureString(string.Empty, font);
                            iTopMargin = num3 + (int)sizeF.Height + iCellHeight;
                        }

                    marginBounds = e.MarginBounds;
                    iLeftMargin = marginBounds.Width * 3 / 100;
                    strFormat.Alignment = StringAlignment.Near;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    e.Graphics.DrawString(
                        "------------------------------------------------------------------------------------------------------------------------------------------------",
                        font, Brushes.Black, new RectangleF((int)arrColumnLefts[2], iTopMargin, 850f, iCellHeight),
                        strFormat);
                    var num4 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("------------------------------", font);
                    iTopMargin = num4 + ((int)sizeF.Height - 5);
                    font = new Font("Arial", 8f, FontStyle.Bold);
                    TotGrandAmt = 0;
                    if (Convert.ToDecimal(ObjGlobal.ReturnDecimal(DTVouMain.Rows[0]["BAmount"].ToString())) !=
                        decimal.Zero)
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Gross Total : ", font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnLefts[8], iCellHeight),
                            strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics4 = e.Graphics;
                        num = Convert.ToDecimal(TotBasicAmt.ToString());
                        graphics4.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[9], iTopMargin, (int)arrColumnWidths[9], iCellHeight),
                            strFormat);
                        TotGrandAmt += TotBasicAmt;
                        var num5 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Total", font);
                        iTopMargin = num5 + (int)sizeF.Height + 5;
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Discount : ", font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnLefts[8], iCellHeight),
                            strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphic4 = e.Graphics;
                        num = Convert.ToDecimal(DTVouMain.Rows[0]["BDiscount"].ToString());
                        graphic4.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[9], iTopMargin, (int)arrColumnWidths[9], iCellHeight),
                            strFormat);
                        TotGrandAmt -= Convert.ToDouble(DTVouMain.Rows[0]["BDiscount"].ToString());
                        var num6 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Total", font);
                        iTopMargin = num6 + (int)sizeF.Height + 5;
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Grand Total : ", font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnLefts[8], iCellHeight),
                            strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics5 = e.Graphics;
                        num = Convert.ToDecimal(TotGrandAmt.ToString());
                        graphics5.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[9], iTopMargin, (int)arrColumnWidths[9], iCellHeight),
                            strFormat);
                    }
                    else
                    {
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Grand Total : ", font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnLefts[8], iCellHeight),
                            strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphic5 = e.Graphics;
                        num = Convert.ToDecimal(TotBasicAmt.ToString());
                        graphic5.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[9], iTopMargin, (int)arrColumnWidths[9], iCellHeight),
                            strFormat);
                        TotGrandAmt += TotBasicAmt;
                    }

                    if (dtadv.Rows.Count <= 0)
                    {
                        var num7 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Total", font);
                        iTopMargin = num7 + (int)sizeF.Height + 1;
                    }
                    else if (dtadv.Rows[0]["Advance"].ToString() != string.Empty)
                    {
                        var num8 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Total", font);
                        iTopMargin = num8 + (int)sizeF.Height + 5;
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Advance     : ", font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnLefts[8], iCellHeight),
                            strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphics6 = e.Graphics;
                        num = Convert.ToDecimal(dtadv.Rows[0]["Advance"].ToString());
                        graphics6.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[9], iTopMargin, (int)arrColumnWidths[9], iCellHeight),
                            strFormat);
                        TotGrandAmt -= Convert.ToDouble(dtadv.Rows[0]["Advance"].ToString());
                        var num9 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Advance", font);
                        iTopMargin = num9 + (int)sizeF.Height + 5;
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString("Balance     : ", font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[6], iTopMargin, (int)arrColumnLefts[8], iCellHeight),
                            strFormat);
                        strFormat.Alignment = StringAlignment.Far;
                        var graphic6 = e.Graphics;
                        num = Convert.ToDecimal(TotGrandAmt.ToString());
                        graphic6.DrawString(num.ToString("0.00"), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[9], iTopMargin, (int)arrColumnWidths[9], iCellHeight),
                            strFormat);
                        var num10 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Balance", font);
                        iTopMargin = num10 + (int)sizeF.Height + 1;
                    }

                    strFormat.Alignment = StringAlignment.Near;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    e.Graphics.DrawString(
                        "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                        font, Brushes.Black, new RectangleF(10f, iTopMargin, 850f, iCellHeight), strFormat);
                    var num11 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("------------------------------", font);
                    iTopMargin = num11 + ((int)sizeF.Height - 5);
                    e.Graphics.DrawString(
                        string.Concat("In Words : ",
                            ClsMoneyConversion.MoneyConversion(DTVouMain.Rows[0]["NetAmount"].ToString()), " "), font,
                        Brushes.Black, new RectangleF(20f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num12 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("Amount In Words", font);
                    iTopMargin = num12 + ((int)sizeF.Height - 5);
                    e.Graphics.DrawString(
                        "------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                        font, Brushes.Black, new RectangleF(10f, iTopMargin, 850f, iCellHeight), strFormat);
                    var num13 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("------------------------------", font);
                    iTopMargin = num13 + (int)sizeF.Height + 1;
                    e.Graphics.DrawString(string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"].ToString(), " "),
                        font, Brushes.Black, new RectangleF(20f, iTopMargin, 700f, iCellHeight), strFormat);
                    e.Graphics.DrawString(string.Concat("User : ", ObjGlobal.LogInUser, " "), font, Brushes.Black,
                        new RectangleF(700f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num14 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("On Account Of", font);
                    iTopMargin = num14 + (int)sizeF.Height + 5;
                    strFormat.Alignment = StringAlignment.Near;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    e.Graphics.DrawString(
                        ">>If there are any _Reports kindly bring all the day of the appointment or treatment.", font,
                        Brushes.Black, new RectangleF(20f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num15 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("user name", font);
                    iTopMargin = num15 + (int)sizeF.Height + 5;
                    strFormat.Alignment = StringAlignment.Near;
                    font = new Font("Arial", 8f, FontStyle.Regular);
                    e.Graphics.DrawString(">>There will be no refund without original Bill.", font, Brushes.Black,
                        new RectangleF(20f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num16 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("user name", font);
                    iTopMargin = num16 + (int)sizeF.Height + 5;
                }
                else
                {
                    e.HasMorePages = true;
                }
            }
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintHospitalBillHalfHeader(PrintPageEventArgs e)
    {
        try
        {
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 10f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(20f, iTopMargin, 800f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(20f, iTopMargin, 800f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            if (DTCD.Rows[0]["PanNo"] == null ? false : DTCD.Rows[0]["PanNo"].ToString() != string.Empty)
                e.Graphics.DrawString(string.Concat("PAN/VAT No : ", DTCD.Rows[0]["PanNo"].ToString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 800f, iCellHeight), strFormat);
            var num3 = iTopMargin;
            sizeF = e.Graphics.MeasureString("PAN/VAT No : ", myFont);
            iTopMargin = num3 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("INVOICE", myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 800f, iCellHeight),
                strFormat);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(string.Concat("Bill No : ", DTVouMain.Rows[0]["BillNo"]) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("INVOICE", myFont);
            iTopMargin = num4 + (int)sizeF.Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 8f, FontStyle.Regular);
            e.Graphics.DrawString(
                "---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 800f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num5 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                string.Concat("Patient Id : ", DTVouMain.Rows[0]["GlShortName"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 390f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Patient Type  : ", DTVouMain.Rows[0]["PatientType"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight), strFormat);
            var graphics = e.Graphics;
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["BillDate"].ToString());
            var shortDateString = dateTime.ToShortDateString();
            dateTime = DateTime.Now;
            graphics.DrawString(
                string.Concat("Date : ", shortDateString, " ", dateTime.ToShortTimeString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Date       : ", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Patient Name : ", DTVouMain.Rows[0]["GlDesc"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 390f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Age/Sex : ", DTVouMain.Rows[0]["Mobile"].ToString(), " /",
                    DTVouMain.Rows[0]["AddressII"].ToString()), myFont, Brushes.Black,
                new RectangleF(390f, iTopMargin, 630f, iCellHeight), strFormat);
            e.Graphics.DrawString(string.Concat("Miti   : ", DTVouMain.Rows[0]["BillMiti"].ToString()), myFont,
                Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Name : ", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(string.Concat("Address : ", DTVouMain.Rows[0]["AddressI"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 390f, iCellHeight), strFormat);
            e.Graphics.DrawString(string.Concat("Tel No  : ", DTVouMain.Rows[0]["TelNoI"].ToString()), myFont,
                Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight), strFormat);
            e.Graphics.DrawString("Billing Mode : General", myFont, Brushes.Black,
                new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Address : ", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Contact Person : ", DTVouMain.Rows[0]["ContactPerson"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(20f, iTopMargin, 390f, iCellHeight), strFormat);
            e.Graphics.DrawString(string.Concat("Pan No  : ", DTVouMain.Rows[0]["PanNo"].ToString()), myFont,
                Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight), strFormat);
            if (DTVouMain.Rows[0]["Catagory"].ToString() == "Cash Book")
                e.Graphics.DrawString("Payment Mode : Cash", myFont, Brushes.Black,
                    new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            else if (DTVouMain.Rows[0]["Catagory"].ToString() != "Bank Book")
                e.Graphics.DrawString("Payment Mode : Credit", myFont, Brushes.Black,
                    new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            else
                e.Graphics.DrawString("Payment Mode : Cheque", myFont, Brushes.Black,
                    new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Contact Person : ", myFont);
            iTopMargin = num9 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Refer Doctor : ", DTVouMain.Rows[0]["ReferDoctor"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(20f, iTopMargin, 390f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Reported Doctor : ", DTVouMain.Rows[0]["ReportedDoctor"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(390f, iTopMargin, 630f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Patient Guardian: ", DTVouMain.Rows[0]["PatientGuardian"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(630f, iTopMargin, 800f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num10 + (int)sizeF.Height + 1;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 850f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("-----------", myFont);
            iTopMargin = num11 + ((int)sizeF.Height - 5);
            myFont = new Font("Arial", 8f, FontStyle.Bold);
            for (var i = 0; i < ColHeaders.Count; i++)
            {
                if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Near;
                else if (ColFormat[i].ToString() == "Left")
                    strFormat.Alignment = StringAlignment.Center;
                else if (ColFormat[i].ToString() == "Right") strFormat.Alignment = StringAlignment.Far;
                sizeF = e.Graphics.MeasureString(ColHeaders[i].ToString(), myFont,
                    Convert.ToInt16(ColWidths[i].ToString()));
                iHeaderHeight = (int)sizeF.Height + 1;
                arrColumnLefts.Add(iLeftMargin);
                arrColumnWidths.Add(ColWidths[i]);
                iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                e.Graphics.DrawString(ColHeaders[i].ToString(), myFont, Brushes.Black,
                    new RectangleF((int)arrColumnLefts[i], iTopMargin, (int)arrColumnWidths[i], iHeaderHeight),
                    strFormat);
            }

            var num12 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Particulars", myFont);
            iTopMargin = num12 + ((int)sizeF.Height - 5);
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(
                "----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(10f, iTopMargin, 850f, iCellHeight), strFormat);
            var num13 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num13 + (int)sizeF.Height + 1;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    public void PrintHospitalPatientRegistration4InchRollPaper(string VouNo, string printer)
    {
        var stringBuilder = new StringBuilder();
        Query =
            "Select GLP.BillingUpToDate BillDate,GLP.GLCode,GLP.GlShortName ,GLP.GlDesc,GlP.Catagory, GLP.AddressI, GLP.AddressII ,GLP.Mobile,GLP.TelNoI,GLP.TelNoII ,GLP.ContactPerson, GLP.PanNo,GLP.PatientType, ";
        Query = string.Concat(Query,
            " GLP.PatientGuardian,GLP.ReferDoctor,GLP.ReportedDoctor, AreaDesc Department From GeneralLedger as GLP Left Outer Join Area on Area.AreaCode=GLP.AreaCode ");
        Query = string.Concat(Query, " Where PatientType is not null and GLP.GLCode='", VouNo, "' ");
        DTVouMain.Reset();
        DTVouMain = GetConnection.SelectDataTableQuery(Query);
        if (DTVouMain.Rows.Count > 0)
        {
            var stringBuilder1 = stringBuilder;
            string[] shortDateString = { string.Empty.MyPadRight(5), "Date       : ", null, null, null };
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["BillDate"]);
            shortDateString[2] = dateTime.ToShortDateString();
            shortDateString[3] = " | ";
            dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["BillDate"].ToString());
            //shortDateString[4] = ObjGlobal.ValidDate(dateTime.ToString("yyyy/MM/dd"));
            stringBuilder1.Append(RawPrinterHelper.GetPrintString(string.Concat(shortDateString) ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append("\n");
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Empty.MyPadRight(5),
                    string.Concat("Patient    : ", DTVouMain.Rows[0]["GlDesc"].ToString()).MyPadRight(60)) ??
                string.Empty, RawPrinterHelper.PrintFontType.Bold));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Empty.MyPadRight(5),
                    string.Concat("Age/Sex    : ", DTVouMain.Rows[0]["Mobile"].ToString(), "/",
                        DTVouMain.Rows[0]["AddressII"].ToString()).MyPadRight(60)) ?? string.Empty,
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Empty.MyPadRight(5),
                    string.Concat("Address    : ", DTVouMain.Rows[0]["AddressI"].ToString()).MyPadRight(60)) ??
                string.Empty, RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Empty.MyPadRight(5),
                    string.Concat("Consultant : ", DTVouMain.Rows[0]["ReferDoctor"].ToString()).MyPadRight(60)) ??
                string.Empty, RawPrinterHelper.PrintFontType.Bold));
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(string.Empty.MyPadRight(5),
                    (string.Concat("Department : ", DTVouMain.Rows[0]["Department"].ToString()) ?? string.Empty)
                    .MyPadRight(60)), RawPrinterHelper.PrintFontType.Contract));
            var str = string.Empty.MyPadRight(5);
            var str1 = string.Concat("User       : ", ObjGlobal.LogInUser).MyPadRight(25);
            dateTime = DateTime.Now;
            stringBuilder.Append(RawPrinterHelper.GetPrintString(
                string.Concat(str, str1, string.Concat("Time : ", dateTime.ToString("HH:MM:ss")).MyPadRight(25)),
                RawPrinterHelper.PrintFontType.Contract));
            stringBuilder.Append("\n\n\n\n");
        }

        RawPrinterHelper.SendStringToPrinter(printer, stringBuilder.ToString());
    }

    private void PrintJVDetails(PrintPageEventArgs e)
    {
        SizeF sizeF;
        decimal num;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100 + 20;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            long num1 = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                if (Station != "JV")
                    Query =
                        "SELECT VNo,VDate,VMiti,GL.GlCode,GL.GlDesc,ChqNo,ChqDate,Remarks FROM CashBankMaster as VM Inner Join GeneralLedger as GL on GL.GlCode=VM.GlCode ";
                else
                    Query = "SELECT VNo,VDate,VMiti,VM.GlCode,Remarks FROM CashBankMaster as VM  ";
                Query = string.Concat(Query, " Where VM.VNo ='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                CashVoucher = false;
                Query = string.Concat(
                    "Select distinct GlCode as Code,GlDesc as Name from GeneralLedger as L  Where Catagory in ('Cash Book') and L.GlCode = '",
                    DTVouMain.Rows[0]["GlCode"].ToString(), "' and LockBill<>'Y' ");
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);
                if (DTTemp.Rows.Count > 0) CashVoucher = true;
                Query = string.Concat("SELECT Distinct SlCode FROM CashBankDetails Where VNo='", VouNo,
                    "' and SlCode is not null");
                DTTemp.Reset();
                DTTemp = GetConnection.SelectDataTableQuery(Query);
                Query = "Select * from (SELECT VM.VNo,L.GlCode,L.GlDesc,SL.SlCode,SL.SLDesc,";
                if (Station != "JV")
                    Query = string.Concat(Query,
                        " Sum(LocalDrAmt) LocalDrAmt,Sum(LocalCrAmt) LocalCrAmt,Narration FROM ACTransaction as VD ");
                else
                    Query = string.Concat(Query,
                        " Sum(PayLocalAmt) LocalDrAmt,Sum(RecLocalAmt) LocalCrAmt,Narration FROM CashBankDetails as VD ");
                Query = string.Concat(Query, " Inner Join CashBankMaster as VM On VM.VNo=VD.VNo ");
                Query = string.Concat(Query,
                    " Inner Join GeneralLedger as L on L.GlCode=VD.GlCode Left Outer Join SubLedger as SL On SL.SLCode=VD.SLCode ");
                Query = string.Concat(Query, " Where VM.VNo='", VouNo, "' ");
                Query = string.Concat(Query, " Group By VM.VNo,L.GlCode,L.GlDesc,SL.SlCode,SL.SLDesc,Narration ");
                Query = string.Concat(Query, " ) as aa ");
                if (Station != "JV")
                    Query = string.Concat(Query, " Order by LocalDrAmt desc,LocalCrAmt");
                else
                    Query = string.Concat(Query, " Order by VNo,LocalDrAmt desc,LocalCrAmt");
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                ColWidths.Add(50);
                if (DTTemp.Rows.Count <= 0)
                {
                    ColWidths.Add(460);
                    ColWidths.Add(0);
                }
                else
                {
                    ColWidths.Add(230);
                    ColWidths.Add(230);
                }

                ColWidths.Add(125);
                ColWidths.Add(125);
                for (var i = 0; i < ColWidths.Count; i++)
                {
                    sizeF = e.Graphics.MeasureString("SNo.", font, Convert.ToInt16(ColWidths[i].ToString()));
                    iHeaderHeight = (int)sizeF.Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            if (DTVouDetails.Rows.Count > 0)
            {
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 20;
                    var num2 = 0;
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        if (bNewPage)
                        {
                            PrintJVHeader(e);
                            bNewPage = false;
                        }

                        DRo++;
                        num1 += 1;
                        num2 = 0;
                        font = new Font("Arial", 9f, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(string.Concat(DRo.ToString(), "."), font, Brushes.Black,
                            new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                iCellHeight), strFormat);
                        num2++;
                        if (DTVouDetails.Rows[i]["GlDesc"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            if (DTVouDetails.Rows[i]["LocalCrAmt"].ToString() == null
                                    ? true
                                    : Convert.ToDouble(DTVouDetails.Rows[i]["LocalCrAmt"].ToString()) == 0)
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["GlDesc"].ToString(), font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            else
                                e.Graphics.DrawString(string.Concat("     ", DTVouDetails.Rows[i]["GlDesc"].ToString()),
                                    font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["SLDesc"].ToString() == null
                                ? false
                                : DTVouDetails.Rows[i]["SLDesc"].ToString() != string.Empty)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["SLDesc"].ToString(), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["LocalDrAmt"].ToString() != null)
                            if (Convert.ToDecimal(DTVouDetails.Rows[i]["LocalDrAmt"].ToString()) != decimal.Zero)
                            {
                                TotDrAmt += Convert.ToDouble(DTVouDetails.Rows[i]["LocalDrAmt"]);
                                strFormat.Alignment = StringAlignment.Far;
                                var graphics = e.Graphics;
                                num = Convert.ToDecimal(DTVouDetails.Rows[i]["LocalDrAmt"].ToString());
                                graphics.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            }

                        num2++;
                        if (DTVouDetails.Rows[i]["LocalCrAmt"].ToString() != null)
                            if (Convert.ToDecimal(DTVouDetails.Rows[i]["LocalCrAmt"].ToString()) != decimal.Zero)
                            {
                                TotCrAmt += Convert.ToDouble(DTVouDetails.Rows[i]["LocalCrAmt"]);
                                strFormat.Alignment = StringAlignment.Far;
                                var graphic = e.Graphics;
                                num = Convert.ToDecimal(DTVouDetails.Rows[i]["LocalCrAmt"].ToString());
                                graphic.DrawString(num.ToString("0.00"), font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            }

                        num2++;
                        if (DTVouDetails.Rows[i]["Narration"].ToString() == string.Empty
                                ? false
                                : DTVouDetails.Rows[i]["Narration"].ToString() != null)
                        {
                            iRow += 1;
                            iTopMargin += iCellHeight;
                            strFormat.Alignment = StringAlignment.Near;
                            RemarksTotLen = DTVouDetails.Rows[i]["Narration"].ToString().Length;
                            font = new Font("Arial", 8f, FontStyle.Regular);
                            if (DTVouDetails.Rows[0]["Narration"].ToString().Length > 70)
                            {
                                e.Graphics.DrawString(
                                    string.Concat("   Narr : ",
                                        DTVouDetails.Rows[i]["Narration"].ToString().Substring(0, 70)), font,
                                    Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460f, iCellHeight), strFormat);
                                var num3 = iTopMargin;
                                sizeF = e.Graphics.MeasureString("Narr", font);
                                iTopMargin = num3 + (int)sizeF.Height + 5;
                                if (RemarksTotLen <= 140)
                                {
                                    e.Graphics.DrawString(
                                        string.Concat("  ",
                                            DTVouDetails.Rows[i]["Narration"].ToString()
                                                .Substring(71, RemarksTotLen - 71)), font, Brushes.Black,
                                        new RectangleF((int)arrColumnLefts[1], iTopMargin, 460f, iCellHeight),
                                        strFormat);
                                }
                                else
                                {
                                    e.Graphics.DrawString(
                                        string.Concat(" ",
                                            DTVouDetails.Rows[i]["Narration"].ToString()
                                                .Substring(71, RemarksTotLen - 71)), font, Brushes.Black,
                                        new RectangleF((int)arrColumnLefts[1], iTopMargin, 460f, iCellHeight),
                                        strFormat);
                                    var num4 = iTopMargin;
                                    sizeF = e.Graphics.MeasureString("Narr", font);
                                    iTopMargin = num4 + (int)sizeF.Height + 5;
                                    e.Graphics.DrawString(
                                        string.Concat(" ",
                                            DTVouDetails.Rows[i]["Narration"].ToString()
                                                .Substring(141, RemarksTotLen - 141)), font, Brushes.Black,
                                        new RectangleF((int)arrColumnLefts[1], iTopMargin, 460f, iCellHeight),
                                        strFormat);
                                }
                            }
                            else
                            {
                                e.Graphics.DrawString(
                                    string.Concat("  Narr : ", DTVouDetails.Rows[i]["Narration"].ToString()), font,
                                    Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[1], iTopMargin, 460f, iCellHeight), strFormat);
                            }
                        }

                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            if (num1 < N_Line)
                for (var j = num1; j <= N_Line; j += 1)
                {
                    e.Graphics.DrawString(" ", font, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    var num5 = iTopMargin;
                    sizeF = e.Graphics.MeasureString(string.Empty, font);
                    iTopMargin = num5 + (int)sizeF.Height + iCellHeight;
                }

            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100 + 20;
            strFormat.Alignment = StringAlignment.Near;
            font = new Font("Arial", 10f, FontStyle.Regular);
            e.Graphics.DrawString(
                "                                                                                                                        ---------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num6 + (int)sizeF.Height + 1;
            font = new Font("Arial", 10f, FontStyle.Bold);
            e.Graphics.DrawString("Total : ", font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[2], iTopMargin, (int)arrColumnWidths[2], iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Far;
            var graphics1 = e.Graphics;
            num = Convert.ToDecimal(TotDrAmt.ToString());
            graphics1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[3], iTopMargin, (int)arrColumnWidths[3], iCellHeight), strFormat);
            var graphic1 = e.Graphics;
            num = Convert.ToDecimal(TotCrAmt.ToString());
            graphic1.DrawString(num.ToString("0.00"), font, Brushes.Black,
                new RectangleF((int)arrColumnLefts[4], iTopMargin, (int)arrColumnWidths[4], iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Total", font);
            iTopMargin = num7 + (int)sizeF.Height + 1;
            font = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num8 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("Amount In Words : ", ClsMoneyConversion.MoneyConversion(TotDrAmt.ToString()), " "), font,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Amount In Words", font);
            iTopMargin = num9 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num10 + (int)sizeF.Height + 10;
            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length > 120)
            {
                e.Graphics.DrawString(
                    string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120), " "), font,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                var num11 = iTopMargin;
                sizeF = e.Graphics.MeasureString("Remarks", font);
                iTopMargin = num11 + (int)sizeF.Height + 5;
                if (RemarksTotLen <= 220)
                {
                    e.Graphics.DrawString(
                        string.Concat("                     ",
                            DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "), font,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        string.Concat("                     ",
                            DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "), font,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                    var num12 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("Remarks", font);
                    iTopMargin = num12 + (int)sizeF.Height + 5;
                    e.Graphics.DrawString(
                        string.Concat("                     ",
                            DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221), " "), font,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                }
            }
            else
            {
                e.Graphics.DrawString(string.Concat("Remarks : ", DTVouMain.Rows[0]["Remarks"], " "), font,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            }

            if (Station != "RV" ? false : Station == "PV")
            {
                var num13 = iTopMargin;
                sizeF = e.Graphics.MeasureString("On Account Of", font);
                iTopMargin = num13 + (int)sizeF.Height + 50;
            }
            else
            {
                var num14 = iTopMargin;
                sizeF = e.Graphics.MeasureString("On Account Of", font);
                iTopMargin = num14 + (int)sizeF.Height + 30;
            }

            var num15 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num15 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "     --------------------------                                                      ---------------------------                                    -------------------------",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num16 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", font);
            iTopMargin = num16 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "           Prepared By                                                             Checked By \t                                                 Approved By ",
                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num17 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num17 + (int)sizeF.Height + 5;
            if (Station == "RV")
            {
                var num18 = iTopMargin;
                sizeF = e.Graphics.MeasureString("user name", font);
                iTopMargin = num18 + (int)sizeF.Height + 10;
                e.Graphics.DrawString("     --------------------------                             ", font,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                var num19 = iTopMargin;
                sizeF = e.Graphics.MeasureString("------------------------------", font);
                iTopMargin = num19 + (int)sizeF.Height + 1;
                e.Graphics.DrawString("           Paid By                  ", font, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            }
            else if (Station == "PV")
            {
                var num20 = iTopMargin;
                sizeF = e.Graphics.MeasureString("user name", font);
                iTopMargin = num20 + (int)sizeF.Height + 10;
                e.Graphics.DrawString("     --------------------------                             ", font,
                    Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                var num21 = iTopMargin;
                sizeF = e.Graphics.MeasureString("------------------------------", font);
                iTopMargin = num21 + (int)sizeF.Height + 1;
                e.Graphics.DrawString("           Received By                     ", font, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            }

            var num22 = iTopMargin;
            sizeF = e.Graphics.MeasureString("user name", font);
            iTopMargin = num22 + (int)sizeF.Height + 5;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintJVHeader(PrintPageEventArgs e)
    {
        DateTime dateTime;
        try
        {
            var marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100 + 20;
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 15;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            if (Station == "JV")
            {
                e.Graphics.DrawString("Journal Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else if (Station == "RV")
            {
                if (CashVoucher)
                    e.Graphics.DrawString("Cash Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
                else
                    e.Graphics.DrawString("Bank Receipt Voucher", myFont, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else if (Station != "PV")
            {
                e.Graphics.DrawString("Cash/Bank Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else if (CashVoucher)
            {
                e.Graphics.DrawString("Cash Payment Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else
            {
                e.Graphics.DrawString("Bank Payment Voucher", myFont, Brushes.Black,
                    new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            }

            var num3 = iTopMargin;
            sizeF = e.Graphics.MeasureString(string.Empty, myFont);
            iTopMargin = num3 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            e.Graphics.DrawString(string.Concat("Voucher No : ", DTVouMain.Rows[0]["VNo"]) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Voucher No : ", myFont);
            iTopMargin = num4 + (int)sizeF.Height + 5;
            if (Station != "JV")
                if (DTVouMain.Rows[0]["ChqNo"] == null ? false : DTVouMain.Rows[0]["ChqNo"].ToString() != string.Empty)
                {
                    strFormat.Alignment = StringAlignment.Near;
                    e.Graphics.DrawString(
                        string.Concat("Cheque No  : ", DTVouMain.Rows[0]["ChqNo"].ToString()) ?? string.Empty, myFont,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
                    if (DTVouMain.Rows[0]["ChqDate"] == null
                            ? false
                            : DTVouMain.Rows[0]["ChqDate"].ToString() != string.Empty)
                    {
                        if (ObjGlobal.SysDateType != "D")
                        {
                            var graphics = e.Graphics;
                            dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["ChqDate"].ToString());
                            //  graphics.DrawString(string.Concat("Cheque Date(BS)    : ", Global.DateToMiti(dateTime.ToShortDateString())) ?? "", this.myFont, Brushes.Black, new RectangleF(300f, (float)this.iTopMargin, 800f, (float)this.iCellHeight), this.strFormat);
                        }
                        else
                        {
                            var graphic = e.Graphics;
                            dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["ChqDate"].ToString());
                            graphic.DrawString(
                                string.Concat("Cheque Date(AD)  : ", dateTime.ToShortDateString()) ?? string.Empty,
                                myFont, Brushes.Black, new RectangleF(300f, iTopMargin, 800f, iCellHeight), strFormat);
                        }
                    }
                }

            strFormat.Alignment = StringAlignment.Near;
            if (ObjGlobal.SysDateType != "D")
            {
                e.Graphics.DrawString(
                    string.Concat("Date(BS)    : ", DTVouMain.Rows[0]["VMiti"].ToString()) ?? string.Empty, myFont,
                    Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            }
            else
            {
                var graphics1 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["VDate"].ToString());
                graphics1.DrawString(string.Concat("Date(AD)    : ", dateTime.ToShortDateString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            }

            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Date       : ", myFont);
            iTopMargin = num5 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 1;
            myFont = new Font("Arial", 10f, FontStyle.Bold);
            Query = string.Concat("SELECT Distinct SlCode FROM CashBankDetails Where VNo='", DTVouMain.Rows[0]["VNo"],
                "' and SlCode is not null");
            DTTemp.Reset();
            DTTemp = GetConnection.SelectDataTableQuery(Query);
            if (DTTemp.Rows.Count <= 0)
                e.Graphics.DrawString(
                    "S.N.     Particulars                                                                                                                    Debit Amt              Credit Amt",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            else
                e.Graphics.DrawString(
                    "S.N.     Ledger                                              Sub Ledger                                                         Debit Amt              Credit Amt",
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Particulars", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "--------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 1;
            iTopMargin++;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintPatientDischargeDetails(PrintPageEventArgs e)
    {
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            long num = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    "Select DischargeNo,DischargeDate,DischargeMiti, GLP.IPDRegDate,GLP.GLCode,GLP.GlShortName ,GLP.GlDesc,GlP.Catagory, GLP.AddressI, GLP.AddressII ,GLP.Mobile,GLP.TelNoI,GLP.TelNoII ,GLP.ContactPerson, GLP.PanNo,GLP.PatientType, ";
                Query = string.Concat(Query,
                    " GLP.PatientGuardian,GLP.ReferDoctor,GLP.ReportedDoctor, AreaDesc Department,SlDesc DischargeDoctor,DischargeType,Diagnosis,Treatment,ClinicalFindings,Content1,Content2,Remarks From PatientDischargeMaster as PDM Inner Join GeneralLedger as GLP On GLP.GlCode=PDM.PId Left Outer Join Area on Area.AreaCode=GLP.AreaCode ");
                Query = string.Concat(Query, " Left Outer Join SubLedger as SL On SL.SLCode=PDM.DischargeDoctorCode ");
                Query = string.Concat(Query, " Where PDM.DischargeNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
            }

            if (DTVouMain.Rows.Count > 0)
            {
                Query =
                    "Select PDesc,Dose,PerTimes,Convert(Decimal(18,1),Day)[Day],Descriptions,Convert(varchar(10), StartDate,103) StartDate,Convert(varchar(10), EndDate,103) EndDate from PatientDrugHistoryDetails as PDHD Inner Join PatientDrugHistoryMaster as PDHM On PDHM.BillNo=PDHD.BillNo Inner Join Product as P On P.PCode=PDHD.PCode ";
                Query = string.Concat(Query, " Where PDHM.PId = '", DTVouMain.Rows[0]["GLCode"].ToString(), "' ");
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 20;
#pragma warning disable CS0219 // The variable 'num1' is assigned but its value is never used
                    var num1 = 0;
#pragma warning restore CS0219 // The variable 'num1' is assigned but its value is never used
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        if (bNewPage)
                        {
                            PrintPatientDischargeHeader(e);
                            bNewPage = false;
                        }

                        DRo++;
                        num += 1;
                        num1 = 0;
                        font = new Font("Arial", 8f, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            string.Concat(iRow.ToString(), ")  ", DTVouDetails.Rows[i]["PDesc"].ToString(), ", Dose : ",
                                DTVouDetails.Rows[i]["Dose"].ToString(), ",  PerTimes : ",
                                DTVouDetails.Rows[i]["PerTimes"].ToString(), ", Day : ",
                                DTVouDetails.Rows[i]["Day"].ToString(), ", ",
                                DTVouDetails.Rows[i]["Descriptions"].ToString(), " "), font, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                    marginBounds = e.MarginBounds;
                    iLeftMargin = marginBounds.Width * 3 / 100;
                    var num2 = iTopMargin;
                    var sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
                    iTopMargin = num2 + (int)sizeF.Height + 30;
                    RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
                    font = new Font("Arial", 9f, FontStyle.Regular);
                    if (DTVouMain.Rows[0]["Remarks"].ToString().Length > 120)
                    {
                        e.Graphics.DrawString(
                            string.Concat("Advice On Discharge : ",
                                DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120), " "), font, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                        var num3 = iTopMargin;
                        sizeF = e.Graphics.MeasureString("Remarks", font);
                        iTopMargin = num3 + (int)sizeF.Height + 5;
                        if (RemarksTotLen <= 220)
                        {
                            e.Graphics.DrawString(
                                string.Concat("                     ",
                                    DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "),
                                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight),
                                strFormat);
                        }
                        else
                        {
                            e.Graphics.DrawString(
                                string.Concat("                     ",
                                    DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "),
                                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight),
                                strFormat);
                            var num4 = iTopMargin;
                            sizeF = e.Graphics.MeasureString("Remarks", font);
                            iTopMargin = num4 + (int)sizeF.Height + 5;
                            e.Graphics.DrawString(
                                string.Concat("                     ",
                                    DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221), " "),
                                font, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight),
                                strFormat);
                        }
                    }
                    else
                    {
                        e.Graphics.DrawString(
                            string.Concat("Advice On Discharge : ", DTVouMain.Rows[0]["Remarks"], " "), font,
                            Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
                    }

                    var num5 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
                    iTopMargin = num5 + (int)sizeF.Height + 30;
                    e.Graphics.DrawString("Discharge Doctor", font, Brushes.Black,
                        new RectangleF(30f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num6 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
                    iTopMargin = num6 + (int)sizeF.Height + 5;
                    e.Graphics.DrawString(DTVouMain.Rows[0]["DischargeDoctor"].ToString(), font, Brushes.Black,
                        new RectangleF(30f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num7 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
                    iTopMargin = num7 + (int)sizeF.Height + 10;
                }
                else
                {
                    e.HasMorePages = true;
                }
            }
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintPatientDischargeHeader(PrintPageEventArgs e)
    {
        try
        {
            var marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 15;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("Discharge Letter", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Near;
            var graphics = e.Graphics;
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
            var shortDateString = dateTime.ToShortDateString();
            dateTime = DateTime.Now;
            graphics.DrawString(
                string.Concat("Date : ", shortDateString, " ", dateTime.ToShortTimeString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            var num3 = iTopMargin;
            sizeF = e.Graphics.MeasureString(string.Empty, myFont);
            iTopMargin = num3 + (int)sizeF.Height + 20;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            e.Graphics.DrawString(string.Concat("Discharge No   : ", DTVouMain.Rows[0]["DischargeNo"]) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Miti     : ", DTVouMain.Rows[0]["DischargeMiti"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Discharge No : ", myFont);
            iTopMargin = num4 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num5 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("Patient Name   : ", DTVouMain.Rows[0]["GlDesc"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 490f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Age/Sex        : ", DTVouMain.Rows[0]["Mobile"].ToString(), "/",
                    DTVouMain.Rows[0]["AddressII"].ToString()) ?? string.Empty, myFont, Brushes.Black,
                new RectangleF(490f, iTopMargin, 800f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Name    : ", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Address            : ", DTVouMain.Rows[0]["AddressI"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 490f, iCellHeight), strFormat);
            e.Graphics.DrawString(string.Concat("Type Of Discharge: ", DTVouMain.Rows[0]["DischargeType"].ToString()),
                myFont, Brushes.Black, new RectangleF(490f, iTopMargin, 800f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Address : ", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 5;
            if (ObjGlobal.SysDateType != "D")
            {
                var graphic = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["IPDRegDate"].ToString());
                //graphic.DrawString(string.Concat("Admitted Miti  : ", Global.DateToMiti(dateTime.ToString("yyyy/MM/dd"))) ?? "", this.myFont, Brushes.Black, new RectangleF((float)this.iLeftMargin, (float)this.iTopMargin, 490f, (float)this.iCellHeight), this.strFormat);
            }
            else
            {
                var graphics1 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["IPDRegDate"].ToString());
                graphics1.DrawString(string.Concat("Admitted Date  : ", dateTime.ToShortDateString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 490f, iCellHeight), strFormat);
            }

            if (ObjGlobal.SysDateType != "D")
            {
                var graphic1 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
                //graphic1.DrawString(string.Concat("Discharge Miti : ", Global.DateToMiti(dateTime.ToString("yyyy/MM/dd"))) ?? "", this.myFont, Brushes.Black, new RectangleF(490f, (float)this.iTopMargin, 800f, (float)this.iCellHeight), this.strFormat);
            }
            else
            {
                var graphics2 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
                graphics2.DrawString(string.Concat("Discharge Date : ", dateTime.ToShortDateString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(490f, iTopMargin, 800f, iCellHeight), strFormat);
            }

            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Admitted Date : ", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num9 + (int)sizeF.Height + 20;
            e.Graphics.DrawString(
                string.Concat("Diagnosis      : ", DTVouMain.Rows[0]["Diagnosis"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Treatment : ", myFont);
            iTopMargin = num10 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Treatment      : ", DTVouMain.Rows[0]["Treatment"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Treatment : ", myFont);
            iTopMargin = num11 + (int)sizeF.Height + 35;
            e.Graphics.DrawString(
                string.Concat("Clinical & Findings  : ", DTVouMain.Rows[0]["ClinicalFindings"].ToString()) ??
                string.Empty, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight),
                strFormat);
            var num12 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Clinical : ", myFont);
            iTopMargin = num12 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Content           : ", DTVouMain.Rows[0]["Content1"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num13 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Content : ", myFont);
            iTopMargin = num13 + (int)sizeF.Height + 20;
            myFont = new Font("Arial", 9f, FontStyle.Bold);
            e.Graphics.DrawString("Drug Details      : ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num14 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Content : ", myFont);
            iTopMargin = num14 + (int)sizeF.Height + 1;
            iTopMargin++;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintPatientDischargeSaptaRishiDetails(PrintPageEventArgs e)
    {
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            long num = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    "Select DischargeNo,DischargeDate,DischargeMiti, GLP.IPDRegDate,GLP.GLCode,GLP.GlShortName ,GLP.GlDesc,GlP.Catagory, GLP.AddressI, GLP.AddressII ,GLP.Mobile,GLP.TelNoI,GLP.TelNoII ,GLP.ContactPerson, GLP.PanNo,GLP.PatientType, ";
                Query = string.Concat(Query,
                    " GLP.PatientGuardian,GLP.ReferDoctor,GLP.ReportedDoctor, AreaDesc Department,SlDesc DischargeDoctor,DischargeType,Diagnosis,Treatment,ClinicalFindings,Content1,Content2,Remarks From PatientDischargeMaster as PDM Inner Join GeneralLedger as GLP On GLP.GlCode=PDM.PId Left Outer Join Area on Area.AreaCode=GLP.AreaCode ");
                Query = string.Concat(Query, " Left Outer Join SubLedger as SL On SL.SLCode=PDM.DischargeDoctorCode ");
                Query = string.Concat(Query, " Where PDM.DischargeNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
            }

            if (DTVouMain.Rows.Count > 0)
            {
                Query =
                    "Select PDesc,Dose,PerTimes,Convert(Decimal(18,1),Day)[Day],Descriptions,Convert(varchar(10), StartDate,103) StartDate,Convert(varchar(10), EndDate,103) EndDate from PatientDrugHistoryDetails as PDHD Inner Join PatientDrugHistoryMaster as PDHM On PDHM.BillNo=PDHD.BillNo Inner Join Product as P On P.PCode=PDHD.PCode ";
                Query = string.Concat(Query, " Where PDHM.PId = '", DTVouMain.Rows[0]["GLCode"].ToString(), "' ");
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 20;
#pragma warning disable CS0219 // The variable 'num1' is assigned but its value is never used
                    var num1 = 0;
#pragma warning restore CS0219 // The variable 'num1' is assigned but its value is never used
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        if (bNewPage)
                        {
                            PrintPatientDischargeSaptaRishiHeader(e);
                            bNewPage = false;
                        }

                        DRo++;
                        num += 1;
                        num1 = 0;
                        font = new Font("Arial", 8f, FontStyle.Regular);
                        strFormat.Alignment = StringAlignment.Near;
                        e.Graphics.DrawString(
                            string.Concat(iRow.ToString(), ")  ", DTVouDetails.Rows[i]["PDesc"].ToString(), ", Dose : ",
                                DTVouDetails.Rows[i]["Dose"].ToString(), ",  PerTimes : ",
                                DTVouDetails.Rows[i]["PerTimes"].ToString(), ", Day : ",
                                DTVouDetails.Rows[i]["Day"].ToString(), ", ",
                                DTVouDetails.Rows[i]["Descriptions"].ToString(), " "), font, Brushes.Black,
                            new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            var num2 = iTopMargin;
            var sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
            iTopMargin = num2 + (int)sizeF.Height + 30;
            RemarksTotLen = DTVouMain.Rows[0]["Remarks"].ToString().Length;
            font = new Font("Arial", 9f, FontStyle.Bold);
            e.Graphics.DrawString("Advice On Discharge : ", font, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 180f, iCellHeight), strFormat);
            font = new Font("Arial", 9f, FontStyle.Regular);
            if (DTVouMain.Rows[0]["Remarks"].ToString().Length > 120)
            {
                e.Graphics.DrawString(string.Concat(DTVouMain.Rows[0]["Remarks"].ToString().Substring(0, 120), " "),
                    font, Brushes.Black, new RectangleF(180f, iTopMargin, 850f, iCellHeight), strFormat);
                var num3 = iTopMargin;
                sizeF = e.Graphics.MeasureString("Remarks", font);
                iTopMargin = num3 + (int)sizeF.Height + 5;
                if (RemarksTotLen <= 220)
                {
                    e.Graphics.DrawString(
                        string.Concat(DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "),
                        font, Brushes.Black, new RectangleF(180f, iTopMargin, 850f, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        string.Concat(DTVouMain.Rows[0]["Remarks"].ToString().Substring(121, RemarksTotLen - 121), " "),
                        font, Brushes.Black, new RectangleF(180f, iTopMargin, 850f, iCellHeight), strFormat);
                    var num4 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("Remarks", font);
                    iTopMargin = num4 + (int)sizeF.Height + 5;
                    e.Graphics.DrawString(
                        string.Concat(DTVouMain.Rows[0]["Remarks"].ToString().Substring(221, RemarksTotLen - 221), " "),
                        font, Brushes.Black, new RectangleF(180f, iTopMargin, 850f, iCellHeight), strFormat);
                }
            }
            else
            {
                e.Graphics.DrawString(string.Concat(DTVouMain.Rows[0]["Remarks"], " "), font, Brushes.Black,
                    new RectangleF(180f, iTopMargin, 850f, iCellHeight), strFormat);
            }

            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Remarks", font);
            iTopMargin = num5 + (int)sizeF.Height + 30;
            font = new Font("Arial", 9f, FontStyle.Bold);
            e.Graphics.DrawString("Discharge Doctor", font, Brushes.Black,
                new RectangleF(30f, iTopMargin, 800f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
            iTopMargin = num6 + (int)sizeF.Height + 5;
            font = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(DTVouMain.Rows[0]["DischargeDoctor"].ToString(), font, Brushes.Black,
                new RectangleF(30f, iTopMargin, 800f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("DischargeDoctor", font);
            iTopMargin = num7 + (int)sizeF.Height + 10;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintPatientDischargeSaptaRishiHeader(PrintPageEventArgs e)
    {
        try
        {
            var marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Phone"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num3 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Phone"].ToString(), myFont);
            iTopMargin = num3 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Email"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Email"].ToString(), myFont);
            iTopMargin = num4 + (int)sizeF.Height + 15;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("Discharge Letter", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            strFormat.Alignment = StringAlignment.Near;
            var graphics = e.Graphics;
            var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
            var shortDateString = dateTime.ToShortDateString();
            dateTime = DateTime.Now;
            graphics.DrawString(
                string.Concat("Date : ", shortDateString, " ", dateTime.ToShortTimeString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString(string.Empty, myFont);
            iTopMargin = num5 + (int)sizeF.Height + 20;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            e.Graphics.DrawString(string.Concat("Discharge No   : ", DTVouMain.Rows[0]["DischargeNo"]) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Miti     : ", DTVouMain.Rows[0]["DischargeMiti"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(600f, iTopMargin, 800f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Discharge No : ", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("Patient Name   : ", DTVouMain.Rows[0]["GlDesc"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Age/Sex        : ", DTVouMain.Rows[0]["Mobile"].ToString(), "/",
                    DTVouMain.Rows[0]["AddressII"].ToString()) ?? string.Empty, myFont, Brushes.Black,
                new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Name    : ", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                string.Concat("Address            : ", DTVouMain.Rows[0]["AddressI"].ToString()) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            e.Graphics.DrawString(string.Concat("Type Of Discharge: ", DTVouMain.Rows[0]["DischargeType"].ToString()),
                myFont, Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Address : ", myFont);
            iTopMargin = num9 + (int)sizeF.Height + 5;
            if (ObjGlobal.SysDateType == "D")
            {
                if (DTVouMain.Rows[0]["IPDRegDate"].ToString() == string.Empty)
                {
                    var graphic = e.Graphics;
                    dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
                    graphic.DrawString(string.Concat("Admitted Date  : ", dateTime.ToShortDateString()) ?? string.Empty,
                        myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
                }
                else
                {
                    var graphics1 = e.Graphics;
                    dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["IPDRegDate"].ToString());
                    graphics1.DrawString(
                        string.Concat("Admitted Date  : ", dateTime.ToShortDateString()) ?? string.Empty, myFont,
                        Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
                }
            }
            else if (DTVouMain.Rows[0]["IPDRegDate"].ToString() == string.Empty)
            {
                var graphic1 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
                //graphic1.DrawString(string.Concat("Admitted Miti  : ", ObjGlobal.DateToMiti(dateTime.ToString("yyyy/MM/dd"))) ?? "", this.myFont, Brushes.Black, new RectangleF((float)this.iLeftMargin, (float)this.iTopMargin, 400f, (float)this.iCellHeight), this.strFormat);
            }
            else
            {
                var graphics2 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["IPDRegDate"].ToString());
                //graphics2.DrawString(string.Concat("Admitted Miti  : ", ObjGlobal.DateToMiti(dateTime.ToString("yyyy/MM/dd"))) ?? "", this.myFont, Brushes.Black, new RectangleF((float)this.iLeftMargin, (float)this.iTopMargin, 400f, (float)this.iCellHeight), this.strFormat);
            }

            if (ObjGlobal.SysDateType != "D")
            {
                var graphic2 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
                //graphic2.DrawString(string.Concat("Discharge Miti : ", ObjGlobal.DateToMiti(dateTime.ToString("yyyy/MM/dd"))) ?? "", this.myFont, Brushes.Black, new RectangleF(400f, (float)this.iTopMargin, 800f, (float)this.iCellHeight), this.strFormat);
            }
            else
            {
                var graphics3 = e.Graphics;
                dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DischargeDate"].ToString());
                graphics3.DrawString(string.Concat("Discharge Date : ", dateTime.ToShortDateString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            }

            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Admitted Date : ", myFont);
            iTopMargin = num10 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num11 + (int)sizeF.Height + 1;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString("Diagnosis        : ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 120f, iCellHeight), strFormat);
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(DTVouMain.Rows[0]["Diagnosis"].ToString() ?? string.Empty, myFont, Brushes.Black,
                new RectangleF(120f, iTopMargin, 400f, iCellHeight), strFormat);
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            RemarksTotLen = DTVouMain.Rows[0]["Treatment"].ToString().Length;
            if (DTVouMain.Rows[0]["Treatment"].ToString().Length > 50)
            {
                e.Graphics.DrawString(string.Concat(DTVouMain.Rows[0]["Treatment"].ToString().Substring(0, 50), " "),
                    myFont, Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
                var num12 = iTopMargin;
                sizeF = e.Graphics.MeasureString("Treatment", myFont);
                iTopMargin = num12 + (int)sizeF.Height + 5;
                if (RemarksTotLen <= 150)
                {
                    e.Graphics.DrawString(
                        string.Concat(DTVouMain.Rows[0]["Treatment"].ToString().Substring(51, RemarksTotLen - 51), " "),
                        myFont, Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
                }
                else
                {
                    e.Graphics.DrawString(
                        string.Concat(DTVouMain.Rows[0]["Treatment"].ToString().Substring(51, 50), " "), myFont,
                        Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
                    var num13 = iTopMargin;
                    sizeF = e.Graphics.MeasureString("Treatment", myFont);
                    iTopMargin = num13 + (int)sizeF.Height + 5;
                    e.Graphics.DrawString(
                        string.Concat(DTVouMain.Rows[0]["Treatment"].ToString().Substring(101, RemarksTotLen - 101),
                            " "), myFont, Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight),
                        strFormat);
                }
            }
            else
            {
                e.Graphics.DrawString(string.Concat(DTVouMain.Rows[0]["Treatment"], " "), myFont, Brushes.Black,
                    new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            }

            var num14 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Treatment : ", myFont);
            iTopMargin = num14 + (int)sizeF.Height + 25;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num15 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Refer Doctor : ", myFont);
            iTopMargin = num15 + (int)sizeF.Height + 20;
            myFont = new Font("Arial", 9f, FontStyle.Bold);
            e.Graphics.DrawString("History & Examination  : ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 180f, iCellHeight), strFormat);
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(DTVouMain.Rows[0]["ClinicalFindings"].ToString() ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(180f, iTopMargin, 800f, iCellHeight), strFormat);
            var num16 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Clinical : ", myFont);
            iTopMargin = num16 + (int)sizeF.Height + 15;
            myFont = new Font("Arial", 9f, FontStyle.Bold);
            e.Graphics.DrawString("Content           : ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 180f, iCellHeight), strFormat);
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(DTVouMain.Rows[0]["Content1"].ToString() ?? string.Empty, myFont, Brushes.Black,
                new RectangleF(180f, iTopMargin, 800f, iCellHeight), strFormat);
            var num17 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Content : ", myFont);
            iTopMargin = num17 + (int)sizeF.Height + 20;
            myFont = new Font("Arial", 9f, FontStyle.Bold);
            e.Graphics.DrawString("Drug Details      : ", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num18 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Drug Details : ", myFont);
            iTopMargin = num18 + (int)sizeF.Height + 1;
            iTopMargin++;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void PrintPatientLabReportDetails(PrintPageEventArgs e)
    {
        SizeF sizeF;
        try
        {
            var width = e.MarginBounds.Width;
            var marginBounds = e.MarginBounds;
            PageWidth = width - marginBounds.Width * 4 / 100;
            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            iRightMargin = PageWidth - iLeftMargin - 20;
            iTopMargin = e.MarginBounds.Top;
            iTopMargin = 30;
            long num = 0;
            iRow = 1;
            bMorePagesToPrint = false;
            var font = new Font("Arial", 9f, FontStyle.Regular);
            if (bFirstPage)
            {
                TotDrAmt = 0;
                TotCrAmt = 0;
                Query =
                    "Select Gl.GlDesc,DM.SampleNo,DM.DDate,DM.DMiti,gl.Mobile Age, gl.AddressII Sex,SlDesc as ReferDoctor from DMaster as DM Inner Join SCMaster as SCM On SCM.SampleNo = DM.SampleNo";
                Query = string.Concat(Query,
                    " Inner Join GeneralLedger as gl On gl.GlCode = SCM.Patient Left Outer Join SubLedger as Sl On SL.SlCode=SCM.RefBy ");
                Query = string.Concat(Query, " Where DM.SampleNo='", VouNo, "' ");
                DTVouMain.Reset();
                DTVouMain = GetConnection.SelectDataTableQuery(Query);
                Query =
                    " Select Distinct 1 Sno,DM.SampleNo,TH.THName,TH.THName Test,null Result,null ReferenceRange,null Method,null LowerBound,null UpperBound from DMaster as DM Inner Join DDetails as DD On DM.DNo = DD.DNo Inner Join Test On AMSTId = DD.TId Inner Join Testhead as TH On TH.THId = AMSTHId ";
                Query = string.Concat(Query, " Where DM.SampleNo = '", DTVouMain.Rows[0]["SampleNo"].ToString(), "' ");
                Query = string.Concat(Query, " Union All ");
                Query = string.Concat(Query,
                    " Select 2 Sno,DM.SampleNo,TH.THName,AMSTName Test, DD.TValue Result, Case When AddressII = 'F' or AddressII = 'FeMale' Then AMSFLowerBound + '-' + AMSFUpperBound + ' ' + AMSUnit Else AMSLowerBound + '-' + AMSUpperBound + ' ' + AMSUnit End as ReferenceRange,null Method,Case When AddressII = 'F' or AddressII = 'FeMale' Then AMSFLowerBound Else AMSLowerBound End LowerBound,Case When AddressII = 'F' or AddressII = 'FeMale' Then AMSFUpperBound Else AMSUpperBound End from DMaster as DM Inner Join DDetails as DD On DM.DNo = DD.DNo Inner Join Test On AMSTId = DD.TId  Inner Join Testhead as TH On TH.THId = AMSTHId Inner Join SCMaster as SCM On SCM.SampleNo = DM.SampleNo Inner Join GeneralLedger as gl On gl.GlCode = SCM.Patient ");
                Query = string.Concat(Query, " Where DM.SampleNo = '", DTVouMain.Rows[0]["SampleNo"].ToString(), "' ");
                Query = string.Concat(Query, " Union All ");
                Query = string.Concat(Query,
                    " Select 3 Sno,DM.SampleNo,TH.THName,AMSTName Test, DD.TValue Result, TMB.LBound + '-' + TMB.UBound + ' ' + AMSUnit as ReferenceRange,null Method,TMB.LBound,TMB.UBound from DMaster as DM  Inner Join DDetails as DD On DM.DNo = DD.DNo Inner Join Test On AMSTId = DD.TId Inner Join Testhead as TH On TH.THId = AMSTHId Inner Join TestMultyBound as TMB On TMB.TId = AMSTId  Inner Join SCMaster as SCM On SCM.SampleNo = DM.SampleNo Inner Join GeneralLedger as gl On gl.GlCode = SCM.Patient ");
                Query = string.Concat(Query, " Where DM.SampleNo = '", DTVouMain.Rows[0]["SampleNo"].ToString(), "' ");
                Query = string.Concat(Query, " Order by THName,Sno ");
                DTVouDetails.Reset();
                DTVouDetails = GetConnection.SelectDataTableQuery(Query);
                ColWidths.Add(250);
                ColWidths.Add(150);
                ColWidths.Add(230);
                ColWidths.Add(125);
                for (var i = 0; i < ColWidths.Count; i++)
                {
                    sizeF = e.Graphics.MeasureString("SNo.", font, Convert.ToInt16(ColWidths[i].ToString()));
                    iHeaderHeight = (int)sizeF.Height + 1;
                    arrColumnLefts.Add(iLeftMargin);
                    arrColumnWidths.Add(ColWidths[i]);
                    iLeftMargin += Convert.ToInt16(ColWidths[i].ToString());
                }
            }

            var num1 = 0;
            if (DTVouMain.Rows.Count > 0)
            {
                i = DRo;
                while (i < DTVouDetails.Rows.Count)
                {
                    iCellHeight = 20;
                    var num2 = 0;
                    if (iTopMargin + iCellHeight < e.MarginBounds.Height + e.MarginBounds.Top - 15)
                    {
                        if (bNewPage)
                        {
                            PrintPatientLabReportHeader(e);
                            bNewPage = false;
                        }

                        DRo++;
                        num += 1;
                        num2 = 0;
                        font = new Font("Arial", 9f, FontStyle.Regular);
                        if (DTVouDetails.Rows[i]["Test"].ToString() != null)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            if (DTVouDetails.Rows[i]["SNo"].ToString() == "1")
                            {
                                font = new Font("Arial", 10f, FontStyle.Bold);
                                e.Graphics.DrawString(string.Concat("      ", DTVouDetails.Rows[i]["Test"].ToString()),
                                    font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            }
                            else if (DTVouDetails.Rows[i]["SNo"].ToString() == "2")
                            {
                                font = new Font("Arial", 9f, FontStyle.Regular);
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["Test"].ToString(), font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            }
                            else if (DTVouDetails.Rows[i]["SNo"].ToString() != "3" ? false : num1 == 0)
                            {
                                num1++;
                                font = new Font("Arial", 9f, FontStyle.Regular);
                                e.Graphics.DrawString(DTVouDetails.Rows[i]["Test"].ToString(), font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            }
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Result"].ToString() == null
                                ? false
                                : DTVouDetails.Rows[i]["Result"].ToString() != string.Empty)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            if (DTVouDetails.Rows[i]["SNo"].ToString() != "3")
                            {
                                if (!(DTVouDetails.Rows[i]["Result"].ToString() != string.Empty) ||
                                    !(DTVouDetails.Rows[i]["LowerBound"].ToString() != string.Empty)
                                        ? false
                                        : DTVouDetails.Rows[i]["UpperBound"].ToString() != string.Empty)
                                    try
                                    {
                                        font =
                                            (Convert.ToDecimal(
                                                 ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["LowerBound"]
                                                     .ToString())) >
                                             Convert.ToDecimal(
                                                 ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["Result"].ToString()))
                                                ? true
                                                : Convert.ToDecimal(
                                                    ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["Result"]
                                                        .ToString())) > Convert.ToDecimal(
                                                    ObjGlobal.ReturnDecimal(DTVouDetails.Rows[i]["UpperBound"]
                                                        .ToString())))
                                                ? new Font("Arial", 9f, FontStyle.Bold)
                                                : new Font("Arial", 9f, FontStyle.Regular);
                                    }
#pragma warning disable CS0168
                                    // The variable 'exception' is declared but never used
                                    catch (Exception exception)
#pragma warning restore CS0168
                                        // The variable 'exception' is declared but never used
                                    {
                                        font = new Font("Arial", 9f, FontStyle.Bold);
                                    }

                                e.Graphics.DrawString(DTVouDetails.Rows[i]["Result"].ToString(), font, Brushes.Black,
                                    new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                        iCellHeight), strFormat);
                            }
                        }

                        num2++;
                        font = new Font("Arial", 9f, FontStyle.Regular);
                        if (DTVouDetails.Rows[i]["ReferenceRange"].ToString() == null
                                ? false
                                : DTVouDetails.Rows[i]["ReferenceRange"].ToString() != string.Empty)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["ReferenceRange"].ToString(), font,
                                Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        if (DTVouDetails.Rows[i]["Method"].ToString() == null
                                ? false
                                : DTVouDetails.Rows[i]["Method"].ToString() != string.Empty)
                        {
                            strFormat.Alignment = StringAlignment.Near;
                            e.Graphics.DrawString(DTVouDetails.Rows[i]["Method"].ToString(), font, Brushes.Black,
                                new RectangleF((int)arrColumnLefts[num2], iTopMargin, (int)arrColumnWidths[num2],
                                    iCellHeight), strFormat);
                        }

                        num2++;
                        iRow += 1;
                        iTopMargin += iCellHeight;
                        i++;
                    }
                    else
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                }

                if (!bMorePagesToPrint)
                {
                    e.HasMorePages = false;
                }
                else
                {
                    e.HasMorePages = true;
                    return;
                }
            }

            if (num < N_Line)
                for (var j = num; j <= N_Line; j += 1)
                {
                    e.Graphics.DrawString(" ", font, Brushes.Black,
                        new RectangleF(iLeftMargin, iTopMargin, PageWidth, iCellHeight), strFormat);
                    var num3 = iTopMargin;
                    sizeF = e.Graphics.MeasureString(string.Empty, font);
                    iTopMargin = num3 + (int)sizeF.Height + iCellHeight;
                }

            marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            font = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(
                "            ---------------------                                                                                                     ------------------------                   ",
                font, Brushes.Black, new RectangleF(30f, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString("----", font);
            iTopMargin = num4 + (int)sizeF.Height + 5;
            font = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(
                "                    Lab                                                                                                                      Signature                      ",
                font, Brushes.Black, new RectangleF(30f, iTopMargin, 800f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Lab", font);
            iTopMargin = num5 + (int)sizeF.Height + 10;
        }
        catch (Exception exception2)
        {
            var exception1 = exception2;
            Msg = exception1.Message;
            throw new ArgumentException(exception1.Message);
        }
    }

    private void PrintPatientLabReportHeader(PrintPageEventArgs e)
    {
        try
        {
            var marginBounds = e.MarginBounds;
            iLeftMargin = marginBounds.Width * 3 / 100;
            var num = 0;
            num = iLeftMargin + 650;
            if (PD.DefaultPageSettings.Landscape) num += 200;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["CompanyName"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num1 = iTopMargin;
            var sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["CompanyName"].ToString(), myFont);
            iTopMargin = num1 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Address"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num2 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Address"].ToString(), myFont);
            iTopMargin = num2 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Phone"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num3 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Phone"].ToString(), myFont);
            iTopMargin = num3 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 10f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(DTCD.Rows[0]["Email"].ToString(), myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num4 = iTopMargin;
            sizeF = e.Graphics.MeasureString(DTCD.Rows[0]["Email"].ToString(), myFont);
            iTopMargin = num4 + (int)sizeF.Height + 15;
            myFont = new Font("Arial", 12f, FontStyle.Bold);
            strFormat.Alignment = StringAlignment.Center;
            e.Graphics.DrawString("LAB REPORT", myFont, Brushes.Black,
                new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num5 = iTopMargin;
            sizeF = e.Graphics.MeasureString("LAB REPORT", myFont);
            iTopMargin = num5 + (int)sizeF.Height + 5;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            strFormat.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num6 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------- ", myFont);
            iTopMargin = num6 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                string.Concat("Patient Name : ", DTVouMain.Rows[0]["GlDesc"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("S. No.       : ", DTVouMain.Rows[0]["SampleNo"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            var num7 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Patient Name    : ", myFont);
            iTopMargin = num7 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            if (ObjGlobal.SysDateType != "D")
            {
                e.Graphics.DrawString(
                    string.Concat("Miti                     : ", DTVouMain.Rows[0]["DMiti"].ToString()) ?? string.Empty,
                    myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            }
            else
            {
                var graphics = e.Graphics;
                var dateTime = Convert.ToDateTime(DTVouMain.Rows[0]["DDate"].ToString());
                var shortDateString = dateTime.ToShortDateString();
                dateTime = DateTime.Now;
                graphics.DrawString(
                    string.Concat("Date                     : ", shortDateString, " ", dateTime.ToShortTimeString()) ??
                    string.Empty, myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight),
                    strFormat);
            }

            e.Graphics.DrawString(
                string.Concat("Age           : ", DTVouMain.Rows[0]["Age"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            var num8 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Age", myFont);
            iTopMargin = num8 + (int)sizeF.Height + 5;
            strFormat.Alignment = StringAlignment.Near;
            myFont = new Font("Arial", 9f, FontStyle.Regular);
            e.Graphics.DrawString(string.Concat("Refered by Dr. : ", DTVouMain.Rows[0]["ReferDoctor"]) ?? string.Empty,
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 400f, iCellHeight), strFormat);
            e.Graphics.DrawString(
                string.Concat("Sex            : ", DTVouMain.Rows[0]["Sex"].ToString()) ?? string.Empty, myFont,
                Brushes.Black, new RectangleF(400f, iTopMargin, 800f, iCellHeight), strFormat);
            var num9 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Sex : ", myFont);
            iTopMargin = num9 + (int)sizeF.Height + 5;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num10 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------- ", myFont);
            iTopMargin = num10 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "     TEST                                                       RESULT                             REFERENCE RANGE                                                    METHOD",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 800f, iCellHeight), strFormat);
            var num11 = iTopMargin;
            sizeF = e.Graphics.MeasureString("Particulars", myFont);
            iTopMargin = num11 + (int)sizeF.Height + 1;
            e.Graphics.DrawString(
                "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------",
                myFont, Brushes.Black, new RectangleF(iLeftMargin, iTopMargin, 850f, iCellHeight), strFormat);
            var num12 = iTopMargin;
            sizeF = e.Graphics.MeasureString("------------------------------", myFont);
            iTopMargin = num12 + (int)sizeF.Height + 1;
        }
        catch (Exception exception1)
        {
            var exception = exception1;
            Msg = exception.Message;
            throw new ArgumentException(exception.Message);
        }
    }

    private void printPreviewDialog1_Click(object sender, EventArgs e)
    {
        PPD.Document = PD;
        PPD.PrintPreviewControl.Zoom = 1;
        PPD.WindowState = FormWindowState.Maximized;
        PPD.ShowDialog();
    }

    #region --------------- Global Class ---------------

    public string DocDesign_Name { get; set; }
    public string FrmName { get; set; }
    public string FromDate { get; set; }
    public string FromDocNo { get; set; }
    public string InvoiceType { get; set; }
    public int NoOf_Copy { get; set; }
    public string Printer_Name { get; set; }
    public string SelectQuery { get; set; }
    public string Station { get; set; }
    public string ToDate { get; set; }
    public string ToDocNo { get; set; }

#pragma warning disable CS0169 // The field 'DocPrinting_Hospital.str' is never used
    private string str;
#pragma warning restore CS0169 // The field 'DocPrinting_Hospital.str' is never used
    private string Query;
#pragma warning disable CS0169 // The field 'DocPrinting_Hospital.ListCaption' is never used
    private string ListCaption;
#pragma warning restore CS0169 // The field 'DocPrinting_Hospital.ListCaption' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Hospital.FromAdDate' is never used
    private string FromADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Hospital.FromAdDate' is never used
    private string VouNo = string.Empty;
    private string Msg = string.Empty;
#pragma warning disable CS0169 // The field 'DocPrinting_Hospital.ToADDate' is never used
    private string ToADDate;
#pragma warning restore CS0169 // The field 'DocPrinting_Hospital.ToADDate' is never used

    private double TotDrAmt;
    private double TotCrAmt;
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.TotAltQty' is assigned but its value is never used
    private double TotAltQty;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.TotAltQty' is assigned but its value is never used
    private double TotQty;
    private double TotBasicAmt;
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.TotNetAmt' is assigned but its value is never used
    private double TotNetAmt;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.TotNetAmt' is assigned but its value is never used
    private double TotGrandAmt;

    private long iRow;
    private bool bFirstPage;
    private bool bNewPage;
    private bool bMorePagesToPrint;
    private bool CashVoucher;
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.Printed' is assigned but its value is never used
    private bool Printed;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.Printed' is assigned but its value is never used

    public int PageWidth;
    public int PageHeight;
    public long PageNo;
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.columnPosition' is assigned but its value is never used
    private int columnPosition = 25;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.columnPosition' is assigned but its value is never used
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.rowPosition' is assigned but its value is never used
    private int rowPosition = 100;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.rowPosition' is assigned but its value is never used
    private int N_Line;
#pragma warning disable CS0169 // The field 'DocPrinting_Hospital.N_HeadLine' is never used
    private int N_HeadLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Hospital.N_HeadLine' is never used
#pragma warning disable CS0169 // The field 'DocPrinting_Hospital.N_FootLine' is never used
    private int N_FootLine;
#pragma warning restore CS0169 // The field 'DocPrinting_Hospital.N_FootLine' is never used
    private int N_TermDet;
    private int RemarksTotLen;
    private int iCellHeight;
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.iCount' is assigned but its value is never used
    private int iCount;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.iCount' is assigned but its value is never used
    private int iHeaderHeight;
    private int iLeftMargin;
    private int iRightMargin;
    private int iTopMargin;
#pragma warning disable CS0414 // The field 'DocPrinting_Hospital.iTmpWidth' is assigned but its value is never used
    private int iTmpWidth = 0;
#pragma warning restore CS0414 // The field 'DocPrinting_Hospital.iTmpWidth' is assigned but its value is never used
    private int i;
    private int DRo;
    private int SysSalesVatId;

    private DataTable dt = new();
    private DataTable dtsys = new();
    private DataTable DTTemp = new();
    private DataTable DTNoOfVou = new();
    private DataTable DTVouMain = new();
    private DataTable DTVouDetails = new();
    private DataTable DTTermDetails = new();
    private DataTable DTProdTerm = new();
    private DataTable DTBillTerm = new();
    private DataTable DTKOTBOT = new();
    private DataTable DTCD = new();
    private DataTable dtadv = new();

    private DataSet ds = new();

    private StringFormat strFormat;
    private readonly ArrayList ColHeaders = new();
    private readonly ArrayList ColWidths = new();
    private readonly ArrayList ColFormat = new();
    private readonly ArrayList arrColumnLefts = new();
    private readonly ArrayList arrColumnWidths = new();
    private ArrayList HeaderCap = new();
    private ArrayList ColumnWidths = new();
    private readonly System.Drawing.Printing.PrintDocument PD = new();
    private readonly PrintController printController = new StandardPrintController();
    private readonly PrintPreviewDialog PPD = new();
    private Font myFont = new("Arial", 10f);

    #endregion --------------- Global Class ---------------
}