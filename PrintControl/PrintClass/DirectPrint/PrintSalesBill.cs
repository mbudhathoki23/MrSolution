using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using PrintControl.RawPrintFunction;
using static PrintControl.PrintClass.DirectPrint.RawPrinterHelper;

namespace PrintControl.PrintClass.DirectPrint;

public class PrintSalesBill
{
    public PrintSalesBill()
    {
        _entry = new ClsFinanceEntry();
        _salesEntry = new ClsSalesEntry();
        _myFont = new Font("Courier New", 9, FontStyle.Bold);
        _companyInfo = new DataTable();
        _printer = new RawPrinter(Printer);
    }

    public void PrintDocumentDesign()
    {
        if (Printer.IsBlankOrEmpty())
        {
            string defaultPrinter;
            var printerName = new PrinterSettings();
            defaultPrinter = printerName.PrinterName;
            Printer = defaultPrinter;
        }

        var commandText = @"
        SELECT ISNULL(PrintDesc,Company_Name) Company_Name,Address,Country,City,State ,PhoneNo,Pan_No,Email,Website  
        FROM AMS.CompanyInfo";
        _companyInfo = SqlExtensions.ExecuteDataSet(commandText).Tables[0];
        switch (Printdesign)
        {
            case "DefaultInvoice":
                {
                    DirectDefaultInvoiceDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultPerformaInvoice":
                {
                    DirectDefaultPerformaInvoiceDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultInvoiceWithNotes":
                {
                    DirectDefaultInvoiceDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultReturnInvoice":
                {
                    DirectDefaultReturnInvoiceDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultReturnPerformaInvoice":
                {
                    DirectDefaultReturnPerformaInvoiceDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultReturnInvoiceWithVAT":
                {
                    DirectDefaultReturnInvoiceWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultInvoiceWithVAT":
                {
                    DirectDefaultInvoiceWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultInvoiceWithPAN":
                {
                    DirectDefaultInvoiceWithPAN(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultInvoiceWithVATWithOutNotes":
                {
                    DirectDefaultInvoiceWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "MorselDesignWithVAT":
                {
                    MorselDesignWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "BhabSagarSalesInvoice":
                {
                    DirectBhabsagarInvoiceWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "BanepaMiniMart":
                {
                    DirectBanepaMiniMartDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "RestaurantDesignWithVAT":
                {
                    DirectRestaurantDesignWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "ThermalRestaurantDesignWithVAT":
                {
                    DirectThermalRestaurantDesignWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "RestaurantDesignWithPAN":
                {
                    DirectRestaurantDesignWithPAN(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "ThermalRestaurantDesignWithPAN":
                {
                    DirectRestaurantDesignWithPAN(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "RestaurantReturnDesignWithVAT":
                {
                    DirectRestaurantReturnDesignWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "RestaurantReturnDesignWithPan":
                {
                    DirectRestaurantReturnDesignWithPAN(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "ThermalRestaurantReturnDesignWithVAT":
                {
                    DirectThermalRestaurantReturnDesignWithVAT(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "ThermalRestaurantReturnDesignWithPan":
                {
                    DirectThermalRestaurantReturnDesignWithPAN(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DirectCancelInvoice3Inch":
                {
                    RestaurantDesignWithPAN(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "RestaurantDesignWithVAT5Inch":
                {
                    RestaurantDesignWithVAT5Inch(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "RestaurantDesignWithPAN5Inch":
                {
                    RestaurantDesignWithPAN5Inch(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "AbbreviatedTaxInvoice3inch":
                {
                    AbbreviatedTaxInvoice3inch(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DefaultOrder":
                {
                    DefaultOrderDesign(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "KOT/BOT":
                {
                    KOTBOTDesign(BillNo, Printer, null);
                    break;
                }
            case "CONFORMATION":
                {
                    ConformationPrint(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "KOT/BOT RePrint":
                {
                    KOTBOTReprintDesign(BillNo, Printer, null);
                    break;
                }
            case "PrintDefaultReturnInvoice":
                {
                    DefaultReturnInvoice(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "AbbreviatedTaxReturnInvoice3inch":
                {
                    AbbreviatedTaxReturnInvoice3inch(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "DayClosing":
                {
                    CashClosing(BillNo, Printer);
                    break;
                }
            case "Proforma Invoice":
                {
                    DirectProformaInvoicePrint(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
            case "CancelInvoice":
            case "InvoiceCancel":
                {
                    CancelInvoicePrint(BillNo, Printer, ObjGlobal.SysBranchId);
                    break;
                }
        }
    }


    //DEFAULT INVOICE
    public void DefaultInvoice(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += DefaultInvoiceDesign;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }

    private void DefaultInvoiceDesign(object sender, PrintPageEventArgs e)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((40 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)),
                        PrintFontType.Contract));
                    if (!ObjGlobal.SysBranchName.IsBlankOrEmpty())
                    {
                        strData.Append(GetPrintString(
                            ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                              Convert.ToInt32((30 - ObjGlobal.SysBranchName.Length) /
                                                                              2)), PrintFontType.Contract));
                    }

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32(
                                                                      (40 - roCom["Address"].ToString().Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(50, '-') + string.Empty, PrintFontType.Contract));
                    if (i == 1)
                    {
                        if (Convert.ToDecimal(roMaster["LN_Amount"]) > 5000)
                        {
                            strData.Append(Convert.ToInt32(roMaster["No_Print"].ToString()) == 0
                                ? GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice", PrintFontType.Contract)
                                : GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                        }
                        else
                        {
                            strData.Append(GetPrintString($"{string.Empty.MyPadRight(10)}Abbreviated Tax Invoice",
                                PrintFontType.Contract));
                        }
                    }
                    else
                    {
                        strData.Append(
                            GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                    }

                    if (roMaster["No_Print"].GetInt() > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) && roMaster["Party_Name"].ToString() != "Cash")
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() != ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() == ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) && string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}", PrintFontType.Contract));
                    }
                    else if (roMaster["PanNo"].IsValueExits())
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}", PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}", PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) && !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}", PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract)); //roMaster["Payment_Mode"].ToString())
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                        "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                            (Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]))
                            .ToString("0.00").MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]);
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                            DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                            SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                    Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                    strData.Append(GetPrintString(
                        "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                        string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    strData.Append(GetPrintString(
                        "Tender Amount :".MyPadLeft(30) +
                        Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Change Amount :".MyPadLeft(30) +
                        Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length switch
                    {
                        >= 28 => rupees.Substring(0, 28),
                        _ => rupees
                    };

                    var rupees2 = (rupees.Length - 28) switch
                    {
                        >= 38 => rupees.Substring(28, 38),
                        _ => rupees.Remove(0, rupees1.Length)
                    };

                    var rupees3 = (rupees.Length - 66) switch
                    {
                        >= 38 => rupees.Substring(66, 38),
                        _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                    };

                    var rupees4 = (rupees.Length - 104) switch
                    {
                        >= 38 => rupees.Substring(104, 38),
                        _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                    };
                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                        PrintFontType.Contract));
                    strData.Append("\n\n");
                    BillNo = roMaster["SB_Invoice"].ToString();
                }

                var result = SendStringToPrinter(Printer, strData.ToString());
                if (result)
                {
                    PrintUpdate(BillNo);
                }

                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
    public void DirectDefaultHsCodeInvoiceDesign(string billno, string printer, int branch)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            Printer = printer;
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((40 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)),
                        PrintFontType.Contract));
                    if (ObjGlobal.SysIsBranchPrint)
                    {
                        strData.Append(GetPrintString(
                            ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                              Convert.ToInt32((40 - ObjGlobal.SysBranchName.Length) /
                                                                              2)), PrintFontType.Contract));
                    }

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32(
                                                                      (40 - roCom["Address"].ToString().Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(10)}Abbreviated Tax Invoice",
                        PrintFontType.Contract));
                    if (roMaster["No_Print"].GetInt() > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    var paymentMode = roMaster["Payment_Mode"].GetUpper();
                    var partyInfo = roMaster["Party_Name"].IsValueExits();
                    if (partyInfo)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}",
                            PrintFontType.Contract));
                        if (roMaster["Vat_No"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}",
                                PrintFontType.Contract));
                        }

                        if (roMaster["Address"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                                PrintFontType.Contract));
                        }
                    }
                    else
                    {
                        if (paymentMode.Equals("CASH"))
                        {
                            strData.Append(GetPrintString("Purchaser Name: CASH A/C", PrintFontType.Contract));
                        }
                        else
                        {
                            strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}",
                                PrintFontType.Contract));
                            if (roMaster["PanNo"].IsValueExits())
                            {
                                strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}",
                                    PrintFontType.Contract));
                            }

                            if (roMaster["GlAddress"].IsValueExits())
                            {
                                strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                                    PrintFontType.Contract));
                            }
                        }
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {paymentMode}", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"Date: {roMaster["Invoice_Date"].GetDateString()}{string.Empty.MyPadLeft(7)} Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(28)}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                        "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = ((object)s[1]).GetInt() <= 0 ? s[0] : qty;
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            roDetail["Rate"].GetDecimal().ToString("0.00").MyPadLeft(10) +
                            (roDetail["Qty"].GetDecimal() * roDetail["Rate"].GetDecimal()).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += roDetail["Qty"].GetDecimal() * roDetail["Rate"].GetDecimal();
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));
                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        DisTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                            .Sum(roTerm => roTerm["Amount"].GetDecimal());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        SpTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                            .Sum(roTerm => roTerm["Amount"].GetDecimal());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = roMaster["B_Amount"].GetDecimal();
                    Total = roMaster["N_Amount"].GetDecimal();

                    strData.Append(GetPrintString("Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                    strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + roMaster["Tender_Amount"].GetDecimal().ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + roMaster["Return_Amount"].GetDecimal().ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                    var rupees2 = rupees.Length - 28 >= 38
                        ? rupees.Substring(28, 38)
                        : rupees.Remove(0, rupees1.Length);
                    var rupees3 = rupees.Length - 66 >= 38
                        ? rupees.Substring(66, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length);
                    var rupees4 = rupees.Length - 104 >= 38
                        ? rupees.Substring(104, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);

                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25), PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days", PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****", PrintFontType.Contract));
                    strData.Append("\n\n\n\n");
                    BillNo = roMaster["SB_Invoice"].ToString();
                }
                var result = SendStringToPrinter(printer, strData.ToString());
                if (result)
                {
                    PrintUpdate(BillNo);
                }
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
    public void DirectDefaultInvoiceDesign(string billno, string printer, int branch)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            Printer = printer;
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    var companyName = roCom["Company_Name"].ToString();

                    var cmdValue = GetPrintString(companyName.MyPadLeft(companyName.Length + (40 - roCom["Company_Name"].ToString().Length).GetInt() / 2), PrintFontType.Contract);
                    strData.Append(cmdValue);
                    if (ObjGlobal.SysIsBranchPrint)
                    {
                        cmdValue = GetPrintString(ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length + Convert.ToInt32((40 - ObjGlobal.SysBranchName.Length) / 2)), PrintFontType.Contract);
                        strData.Append(cmdValue);
                    }

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32(
                                                                      (40 - roCom["Address"].ToString().Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(10)}Abbreviated Tax Invoice",
                        PrintFontType.Contract));
                    if (roMaster["No_Print"].GetInt() > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    var paymentMode = roMaster["Payment_Mode"].GetUpper();
                    var partyInfo = roMaster["Party_Name"].IsValueExits();
                    if (partyInfo)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}",
                            PrintFontType.Contract));
                        if (roMaster["Vat_No"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}",
                                PrintFontType.Contract));
                        }

                        if (roMaster["Address"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                                PrintFontType.Contract));
                        }
                    }
                    else
                    {
                        if (paymentMode.Equals("CASH"))
                        {
                            strData.Append(GetPrintString("Purchaser Name: CASH A/C", PrintFontType.Contract));
                        }
                        else
                        {
                            strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}",
                                PrintFontType.Contract));
                            if (roMaster["PanNo"].IsValueExits())
                            {
                                strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}",
                                    PrintFontType.Contract));
                            }

                            if (roMaster["GlAddress"].IsValueExits())
                            {
                                strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                                    PrintFontType.Contract));
                            }
                        }
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {paymentMode}", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"Date: {roMaster["Invoice_Date"].GetDateString()}{string.Empty.MyPadLeft(7)} Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(28)}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                        "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = ((object)s[1]).GetInt() <= 0 ? s[0] : qty;
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            roDetail["Rate"].GetDecimal().ToString("0.00").MyPadLeft(10) +
                            (roDetail["Qty"].GetDecimal() * roDetail["Rate"].GetDecimal()).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += roDetail["Qty"].GetDecimal() * roDetail["Rate"].GetDecimal();
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        var cmdString = $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'";
                        DisTerm += dtTerm.Select(cmdString).Sum(roTerm => roTerm["Amount"].GetDecimal());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString("Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        SpTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                            .Sum(roTerm => roTerm["Amount"].GetDecimal());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = roMaster["B_Amount"].GetDecimal();
                    Total = roMaster["N_Amount"].GetDecimal();

                    strData.Append(GetPrintString("Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                    strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + roMaster["Tender_Amount"].GetDecimal().ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + roMaster["Return_Amount"].GetDecimal().ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                    var rupees2 = rupees.Length - 28 >= 38
                        ? rupees.Substring(28, 38)
                        : rupees.Remove(0, rupees1.Length);
                    var rupees3 = rupees.Length - 66 >= 38
                        ? rupees.Substring(66, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length);
                    var rupees4 = rupees.Length - 104 >= 38
                        ? rupees.Substring(104, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);

                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25), PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days", PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(50, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****", PrintFontType.Contract));
                    strData.Append("\n\n\n\n");
                    BillNo = roMaster["SB_Invoice"].ToString();
                }
                var result = SendStringToPrinter(printer, strData.ToString());
                if (result)
                {
                    PrintUpdate(BillNo);
                }
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }
    public void DirectDefaultPerformaInvoiceDesign(string billno, string printer, int branch)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            Printer = printer;
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((40 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)),
                        PrintFontType.Contract));
                    if (ObjGlobal.SysIsBranchPrint)
                    {
                        strData.Append(GetPrintString(
                            ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                              Convert.ToInt32((40 - ObjGlobal.SysBranchName.Length) /
                                                                              2)), PrintFontType.Contract));
                    }

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32(
                                                                      (40 - roCom["Address"].ToString().Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(10)}Abbreviated Performa Invoice",
                        PrintFontType.Contract));
                    if (roMaster["No_Print"].GetInt() > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    var paymentMode = roMaster["Payment_Mode"].GetUpper();
                    var partyInfo = roMaster["Party_Name"].IsValueExits();
                    if (partyInfo)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}",
                            PrintFontType.Contract));
                        if (roMaster["Vat_No"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}",
                                PrintFontType.Contract));
                        }

                        if (roMaster["Address"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                                PrintFontType.Contract));
                        }
                    }
                    else
                    {
                        if (paymentMode.Equals("CASH"))
                        {
                            strData.Append(GetPrintString("Purchaser Name: CASH A/C", PrintFontType.Contract));
                        }
                        else
                        {
                            strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}",
                                PrintFontType.Contract));
                            if (roMaster["PanNo"].IsValueExits())
                            {
                                strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}",
                                    PrintFontType.Contract));
                            }

                            if (roMaster["GlAddress"].IsValueExits())
                            {
                                strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                                    PrintFontType.Contract));
                            }
                        }
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {paymentMode}", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"Date: {roMaster["Invoice_Date"].GetDateString()}{string.Empty.MyPadLeft(7)} Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(28)}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                        "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = ((object)s[1]).GetInt() <= 0 ? s[0] : qty;
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            roDetail["Rate"].GetDecimal().ToString("0.00").MyPadLeft(10) +
                            (roDetail["Qty"].GetDecimal() * roDetail["Rate"].GetDecimal()).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += roDetail["Qty"].GetDecimal() * roDetail["Rate"].GetDecimal();
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));
                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        DisTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                            .Sum(roTerm => roTerm["Amount"].GetDecimal());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        SpTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                            .Sum(roTerm => roTerm["Amount"].GetDecimal());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = roMaster["B_Amount"].GetDecimal();
                    Total = roMaster["N_Amount"].GetDecimal();

                    strData.Append(GetPrintString("Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                    strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + roMaster["Tender_Amount"].GetDecimal().ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + roMaster["Return_Amount"].GetDecimal().ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                    var rupees2 = rupees.Length - 28 >= 38
                        ? rupees.Substring(28, 38)
                        : rupees.Remove(0, rupees1.Length);
                    var rupees3 = rupees.Length - 66 >= 38
                        ? rupees.Substring(66, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length);
                    var rupees4 = rupees.Length - 104 >= 38
                        ? rupees.Substring(104, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);

                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25), PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days", PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****", PrintFontType.Contract));
                    strData.Append("\n\n\n\n");
                    BillNo = roMaster["SB_Invoice"].ToString();
                }
                var result = SendStringToPrinter(printer, strData.ToString());
                if (result)
                {
                    PrintUpdate(BillNo);
                }
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    //RESTAURANTDESIGNWITHPAN
    public void RestaurantDesignWithPAN(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += RestaurantDesignWithPAN;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }

    private void RestaurantDesignWithPAN(object sender, PrintPageEventArgs e)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            decimal BasicAmt = 0;
            int noofcopy =
                NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((35 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)), PrintFontType.Bold));
                    if (!ObjGlobal.SysBranchName.IsBlankOrEmpty())
                    {
                        strData.Append(GetPrintString(
                            ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                              Convert.ToInt32((30 - ObjGlobal.SysBranchName.Length) /
                                                                              2)), PrintFontType.Contract));
                    }

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32(
                                                                      (35 - roCom["Address"].ToString().Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                    if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString($"Waiter: {roMaster["WaiterName"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Table No: {roMaster["TableCode"]}{string.Empty}",
                        PrintFontType.Contract));

                    if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                        roMaster["Party_Name"].ToString() != "Cash")
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() !=
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() ==
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                        string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));

                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) +
                        "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                            roDetail["BAmount"].GetDecimalString().MyPadLeft(9) + string.Empty,
                            PrintFontType.Contract));
                        BasicAmt += roDetail["BAmount"].GetDecimal();
                        //Print Total
                        strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                            PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            $"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                            PrintFontType.Contract));

                        decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                        if (PDiscountId != string.Empty)
                        {
                            foreach (var roTerm in dtTerm.Select(
                                         $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                                DisTerm = DisTerm + roTerm["Amount"].GetDecimal();

                            if (DisTerm > 0)
                            {
                                strData.Append(GetPrintString(
                                    "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                    PrintFontType.Contract));
                            }
                        }

                        if (BDiscountId != string.Empty)
                        {
                            foreach (var roTerm in dtTerm.Select(
                                         $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                                SpTerm = SpTerm + roTerm["Amount"].GetDecimal();

                            if (SpTerm > 0)
                            {
                                SpTerm = SpTerm / 1.13.GetDecimal();
                                strData.Append(GetPrintString(
                                    "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                    PrintFontType.Contract));
                            }
                        }

                        var termAmt = DisTerm + SpTerm - Svterm;
                        strData.Append(GetPrintString(
                            "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                            string.Empty, PrintFontType.Contract));
                        if (SalesVatTermId != string.Empty)
                        {
                            foreach (var roTerm in dtTerm.Select(
                                         $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                                vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        }

                        if (vatterm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }

                        decimal Total = 0;
                        decimal Basic = 0;
                        Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                        Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                        strData.Append(GetPrintString(
                            "Total :".MyPadLeft(30) +
                            (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) + string.Empty,
                            PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            $"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                            PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            "Tender Amount :".MyPadLeft(30) +
                            roMaster["Tender_Amount"].GetDecimalString().MyPadLeft(10) + string.Empty,
                            PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            "Change Amount :".MyPadLeft(30) +
                            roMaster["Return_Amount"].GetDecimalString().MyPadLeft(10) + string.Empty,
                            PrintFontType.Contract));
                        strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                            PrintFontType.Contract));
                        var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                        var rupees1 = rupees.Length switch
                        {
                            >= 28 => rupees.Substring(0, 28),
                            _ => rupees
                        };
                        var rupees2 = (rupees.Length - 28) switch
                        {
                            >= 38 => rupees.Substring(28, 38),
                            _ => rupees.Remove(0, rupees1.Length)
                        };

                        var rupees3 = (rupees.Length - 66) switch
                        {
                            >= 38 => rupees.Substring(66, 38),
                            _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                        };

                        var rupees4 = (rupees.Length - 104) switch
                        {
                            >= 38 => rupees.Substring(104, 38),
                            _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                        };
                        strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                            PrintFontType.Contract));
                        strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                        strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                            PrintFontType.Contract));
                        strData.Append("\n\n\n\n");
                    }

                    var result = SendStringToPrinter(Printer, strData.ToString());
                    if (result)
                    {
                        PrintUpdate(BillNo);
                    }

                    strData.Clear();
                    var GS = Convert.ToString((char)29);
                    var ESC = Convert.ToString((char)27);
                    var COMMAND = string.Empty;
                    COMMAND = $"{ESC}@";
                    COMMAND += $"{GS}V{(char)1}";
                    SendStringToPrinter(Printer, COMMAND);
                }
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }

    private void DirectRestaurantDesignWithPAN(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        var result =
            false; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length + Convert.ToInt32((25 - roCom["Company_Name"].ToString().Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length + Convert.ToInt32((30 - roCom["Address"].ToString().Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)), PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Invoice", PrintFontType.Contract));
                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}", PrintFontType.Contract));
                }

                strData.Append(GetPrintString($"Table No:[{roMaster["tableName"]}]", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() != ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() == ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) && !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}", PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString("SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString((roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) + Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00").MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(13, ' ')}---------------------------{string.Empty}", PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'").Aggregate(pDiscount, (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString("Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'").Aggregate(bDiscount, (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0) //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString("Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'").Aggregate(serviceCharge, (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0) //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString("Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString("Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(13, ' ')}---------------------------{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(13, ' ')}--------------------------{string.Empty}", PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };
                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };
                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };
                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25), PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****", PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }



    //DefaultInvoiceDesignWithVAT
    public void DefaultInvoiceWithVAT(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += DefaultInvoiceWithVAT;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void DefaultInvoiceWithVAT(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];

        decimal BasicAmt = 0;

        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                if (!ObjGlobal.SysBranchName.IsBlankOrEmpty())
                {
                    strData.Append(GetPrintString(
                        ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                          Convert.ToInt32((30 - ObjGlobal.SysBranchName.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                if (i == 1)
                {
                    if (ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0)
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice",
                            PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                        PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Payment_Mode"].ToString()) &&
                    roMaster["Payment_Mode"].ToString() == "Cash" && roMaster["Customer_Id"].GetLong() ==
                    ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(30)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(30)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                        DisTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                        SpTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                        vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days", PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                    PrintFontType.Contract));
                strData.Append("\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    public void DirectBhabsagarInvoiceWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        Printer = printer;
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                if (!ObjGlobal.SysBranchName.IsBlankOrEmpty())
                {
                    strData.Append(GetPrintString(
                        ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                          Convert.ToInt32((30 - ObjGlobal.SysBranchName.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    strData.Append(ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0
                        ? GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice", PrintFontType.Contract)
                        : GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                        PrintFontType.Contract));
                }

                if (ObjGlobal.ReturnInt(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                    roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(23)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(23)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                            DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                            SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                            vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    }
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                var rupees2 = rupees.Length - 28 >= 38
                    ? rupees.Substring(28, 38)
                    : rupees.Remove(0, rupees1.Length);
                var rupees3 = rupees.Length - 66 >= 38
                    ? rupees.Substring(66, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length);
                var rupees4 = rupees.Length - 104 >= 38
                    ? rupees.Substring(104, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);
                strData.Append(GetPrintString(
                    ("Cashier : " + roMaster["Enter_By"].ToString().ToUpper()).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}Goods once sold couldnot be taken Back.!!", PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    public void DirectDefaultInvoiceWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        Printer = printer;
        decimal BasicAmt = 0;

        int noofcopy = NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";


                strData.Append(GetPrintString(roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length + Convert.ToInt32((40 - roCom["Company_Name"].ToString().Length) / 2)), PrintFontType.Contract));
                if (ObjGlobal.SysIsBranchPrint)
                {
                    strData.Append(GetPrintString(ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length + Convert.ToInt32((40 - ObjGlobal.SysBranchName.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (40 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    strData.Append(GetPrintString(
                        roMaster["No_Print"].GetInt() is 0
                            ? $"{string.Empty.MyPadRight(13)}Tax Invoice"
                            : $"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                var paymentMode = roMaster["Payment_Mode"].GetUpper();
                var partyInfo = roMaster["Party_Name"].IsValueExits();
                if (partyInfo)
                {
                    strData.Append(
                        GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}", PrintFontType.Contract));
                    if (roMaster["Vat_No"].IsValueExits())
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}",
                            PrintFontType.Contract));
                    }

                    if (roMaster["Address"].IsValueExits())
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    if (paymentMode.Equals("CASH"))
                    {
                        strData.Append(GetPrintString("Purchaser Name: CASH A/C", PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(
                            GetPrintString($"Purchaser Name: {roMaster["GLName"]}", PrintFontType.Contract));
                        if (roMaster["PanNo"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}",
                                PrintFontType.Contract));
                        }

                        if (roMaster["GlAddress"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                                PrintFontType.Contract));
                        }
                    }
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(
                    GetPrintString($"Mode of Payment: {paymentMode}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"Date: {roMaster["Invoice_Date"].GetDateString()}{string.Empty.MyPadLeft(7)} Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(33)}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{"SN.".MyPadRight(3)}{"Particulars".MyPadRight(14)}{"Qty".MyPadLeft(5)}{"Rate".MyPadLeft(9)}{"Amount".MyPadLeft(9)}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = s[1].GetInt() <= 0 ? s[0] : qty;
                    strData.Append(GetPrintString(
                        $"{(roDetail["Invoice_Sno"] + ".").MyPadRight(3)}{roDetail["PName"].ToString().MyPadRight(14)}{qty.MyPadLeft(4)}{Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10)}{Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00").MyPadLeft(9)}{string.Empty}",
                        PrintFontType.Contract));
                    BasicAmt += roDetail["B_Amount"].GetDecimal();
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        DisTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        SpTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (!SalesVatTermId.IsBlankOrEmpty())
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        vatterm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (vatterm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = roMaster["B_Amount"].GetDecimal();
                Total = roMaster["N_Amount"].GetDecimal();
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length is >= 28 ? rupees.Substring(0, 28) : rupees;
                var rupees2 = rupees.Length - 28 is >= 38 ? rupees.Substring(28, 38) : rupees.Remove(0, rupees1.Length);
                var rupees3 = rupees.Length - 66 is >= 38
                    ? rupees.Substring(66, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length);
                var rupees4 = rupees.Length - 104 is >= 38
                    ? rupees.Substring(104, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);
                strData.Append(GetPrintString(
                    ("Cashier : " + roMaster["Enter_By"].ToString().ToUpper()).MyPadRight(25), PrintFontType.Contract));
                strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days",
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(printer, COMMAND);
        }
    }
    public void DirectDefaultHsCodeInvoiceWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        Printer = printer;
        decimal BasicAmt = 0;

        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((40 - roCom["Company_Name"].ToString()
                                                                   .Length) / 2)), PrintFontType.Contract));
                if (ObjGlobal.SysIsBranchPrint)
                {
                    strData.Append(GetPrintString(
                        ObjGlobal.SysBranchName.MyPadLeft(ObjGlobal.SysBranchName.Length +
                                                          Convert.ToInt32((40 - ObjGlobal.SysBranchName.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (40 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    strData.Append(GetPrintString(
                        roMaster["No_Print"].GetInt() is 0
                            ? $"{string.Empty.MyPadRight(13)}Tax Invoice"
                            : $"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                var paymentMode = roMaster["Payment_Mode"].GetUpper();
                var partyInfo = roMaster["Party_Name"].IsValueExits();
                if (partyInfo)
                {
                    strData.Append(
                        GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}", PrintFontType.Contract));
                    if (roMaster["Vat_No"].IsValueExits())
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}",
                            PrintFontType.Contract));
                    }

                    if (roMaster["Address"].IsValueExits())
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    if (paymentMode.Equals("CASH"))
                    {
                        strData.Append(GetPrintString("Purchaser Name: CASH A/C", PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(
                            GetPrintString($"Purchaser Name: {roMaster["GLName"]}", PrintFontType.Contract));
                        if (roMaster["PanNo"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}",
                                PrintFontType.Contract));
                        }

                        if (roMaster["GlAddress"].IsValueExits())
                        {
                            strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                                PrintFontType.Contract));
                        }
                    }
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(
                    GetPrintString($"Mode of Payment: {paymentMode}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"Date: {roMaster["Invoice_Date"].GetDateString()}{string.Empty.MyPadLeft(7)} Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(33)}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{"SN.".MyPadRight(3)}{"Particulars".MyPadRight(14)}{"Qty".MyPadLeft(5)}{"Rate".MyPadLeft(9)}{"Amount".MyPadLeft(9)}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = s[1].GetInt() <= 0 ? s[0] : qty;
                    strData.Append(GetPrintString(
                        $"{(roDetail["Invoice_Sno"] + ".").MyPadRight(3)}{roDetail["PName"].ToString().MyPadRight(14)}{qty.MyPadLeft(4)}{Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10)}{Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00").MyPadLeft(9)}{string.Empty}",
                        PrintFontType.Contract));
                    BasicAmt += roDetail["B_Amount"].GetDecimal();
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        DisTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        SpTerm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (!SalesVatTermId.IsBlankOrEmpty())
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        vatterm += dtTerm
                            .Select(
                                $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (vatterm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = roMaster["B_Amount"].GetDecimal();
                Total = roMaster["N_Amount"].GetDecimal();
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length is >= 28 ? rupees.Substring(0, 28) : rupees;
                var rupees2 = rupees.Length - 28 is >= 38 ? rupees.Substring(28, 38) : rupees.Remove(0, rupees1.Length);
                var rupees3 = rupees.Length - 66 is >= 38
                    ? rupees.Substring(66, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length);
                var rupees4 = rupees.Length - 104 is >= 38
                    ? rupees.Substring(104, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);
                strData.Append(GetPrintString(
                    ("Cashier : " + roMaster["Enter_By"].ToString().ToUpper()).MyPadRight(25), PrintFontType.Contract));
                strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Exchange & Return will be Within 7 Days",
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(printer, COMMAND);
        }
    }
    public void DirectBanepaMiniMartDesign(string billno, string printer, int branch)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            Printer = printer;
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((40 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)),
                        PrintFontType.Contract));
                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32((40 - roCom["Address"].ToString()
                                                                      .Length) / 2)), PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    if (i == 1)
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(10)}Abbreviated Tax Invoice",
                            PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                            PrintFontType.Contract));
                    }

                    if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                        roMaster["Party_Name"].ToString() != "Cash")
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() !=
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() ==
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                        string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract)); //roMaster["Payment_Mode"].ToString())
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) +
                        "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                            (Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]))
                            .ToString("0.00").MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]);
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                            DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                            SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                    Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                    strData.Append(GetPrintString(
                        "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                        string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Tender Amount :".MyPadLeft(30) +
                        Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Change Amount :".MyPadLeft(30) +
                        Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));
                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length switch
                    {
                        >= 28 => rupees.Substring(0, 28),
                        _ => rupees
                    };
                    var rupees2 = (rupees.Length - 28) switch
                    {
                        >= 38 => rupees.Substring(28, 38),
                        _ => rupees.Remove(0, rupees1.Length)
                    };
                    var rupees3 = (rupees.Length - 66) switch
                    {
                        >= 38 => rupees.Substring(66, 38),
                        _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                    };
                    var rupees4 = (rupees.Length - 104) switch
                    {
                        >= 38 => rupees.Substring(104, 38),
                        _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                    };
                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.PadLeft(0)}Exchange & Return will be Within 3 Days",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.PadLeft(0)}SalesBill Required for Return & Exchange",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.PadLeft(0)}* This Invoice is Only for Refrence", PrintFontType.Contract));
                    strData.Append("\n\n");
                    BillNo = roMaster["SB_Invoice"].ToString();
                }

                var result = SendStringToPrinter(printer, strData.ToString());
                if (result)
                {
                    PrintUpdate(BillNo);
                }

                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }
    //Morsel<>Design<>WithVAT
    public void MorselDesignWithVAT(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += MorselDesignWithVAT;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void MorselDesignWithVAT(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                if (i == 1)
                {
                    if (ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0)
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice",
                            PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                        PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                    roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                        DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                        SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        SpTerm = SpTerm / Convert.ToDecimal(1.13);
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                        vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}Goods Once sold will not be Exchanged or returned",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    //RestaurantDesignWithVAT
    public void RestaurantDesignWithVAT(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += RestaurantDesignWithVAT;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void RestaurantDesignWithVAT(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"].ToString()
                                                                   .Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    if (ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0)
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice",
                            PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                        PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                    roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                        DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                        SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        SpTerm = SpTerm / Convert.ToDecimal(1.13);
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                        vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    private void DirectRestaurantDesignWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        var result =
            false; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"].ToString()
                                                                   .Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    if (ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0)
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Tax Invoice",
                            PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Invoice",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Invoice", PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString($"Table No:[{roMaster["tableName"]}]", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                        .Aggregate(pDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                        .Aggregate(bDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                        .Aggregate(serviceCharge,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());

                if (SalesVatTermId != string.Empty)
                {
                    vatTerm = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                        .Aggregate(vatTerm,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                }

                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (Total - vatTerm).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                if (vatTerm > 0)
                {
                    strData.Append(GetPrintString("Vat @13%:".MyPadLeft(30) + vatTerm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    private void DirectThermalRestaurantDesignWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        var result = false;
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = roCom["City"].IsValueExits()
                    ? $"{roCom["City"]}{','}{roCom["PhoneNo"]}"
                    : roCom["PhoneNo"].ToString();
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32(20 - roCom["Company_Name"].ToString()
                                                                   .Length / 2)), PrintFontType.Expand));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    if (ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0)
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Tax Invoice",
                            PrintFontType.Contract));
                    }
                    else
                    {
                        strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Invoice",
                            PrintFontType.Contract));
                    }
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Invoice", PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString($"Table No:[{roMaster["tableName"]}]", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                        .Aggregate(pDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                        .Aggregate(bDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                        .Aggregate(serviceCharge,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());

                if (SalesVatTermId != string.Empty)
                {
                    vatTerm = dtTerm
                        .Select(
                            $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                        .Aggregate(vatTerm,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                }

                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (Total - vatTerm).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                if (vatTerm > 0)
                {
                    strData.Append(GetPrintString("Vat @13%:".MyPadLeft(30) + vatTerm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    //RestaurantDesignWithPAN
    public void DefaultInvoiceWithPAN(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += DefaultInvoiceWithPAN;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void DefaultInvoiceWithPAN(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)), PrintFontType.Bold));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (35 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString($"Table No: {roMaster["TableCode"]}{string.Empty}",
                    PrintFontType.Contract));
                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                    roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                        DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                        SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        SpTerm = SpTerm / Convert.ToDecimal(1.13);
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                        vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };
                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    private void DirectDefaultInvoiceWithPAN(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                var Company = roCom["Company_Name"].ToString();
                strData.Append(GetPrintString(Company.MyPadLeft(Company.Length + Convert.ToInt32((35 - Company.Length) / 2)), PrintFontType.Bold));
                if (!string.IsNullOrWhiteSpace(roCom["Address"].ToString()))
                {
                    strData.Append(GetPrintString(roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length + Convert.ToInt32((35 - roCom["Address"].ToString().Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["City"].IsValueExits())
                {
                    strData.Append(GetPrintString(city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].IsValueExits())
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)), PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) && roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() != ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() == ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ", PrintFontType.Contract));
                }

                if (roMaster["PanNo"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Vat_No"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}", PrintFontType.Contract));
                }

                if (roMaster["GlAddress"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["GlAddress"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString((roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) + roDetail["BeforeVAT"].GetDecimalString().MyPadLeft(10) + roDetail["NetAmount"].GetDecimalString().MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += roDetail["NetAmount"].GetDecimal();
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    DisTerm += dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'").Sum(roTerm => roTerm["Amount"].GetDecimal());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString("Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    SpTerm += dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'").Sum(roTerm => roTerm["Amount"].GetDecimal());
                    if (SpTerm > 0)
                    {
                        strData.Append(GetPrintString("Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());

                strData.Append(GetPrintString("Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = string.Empty;
                rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                var rupees2 = string.Empty;
                rupees2 = rupees.Length - 28 >= 38 ? rupees.Substring(28, 38) : rupees.Remove(0, rupees1.Length);
                var rupees3 = string.Empty;
                rupees3 = rupees.Length - 66 >= 38
                    ? rupees.Substring(66, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length);
                var rupees4 = string.Empty;
                rupees4 = rupees.Length - 104 >= 38
                    ? rupees.Substring(104, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);

                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25), PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****", PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(printer, COMMAND);
        }
    }

    //AbbreviatedSalesInvoiceDesign
    public void AbbreviatedTaxInvoice3inch(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += AbbreviatedTaxInvoice3inch;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void AbbreviatedTaxInvoice3inch(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy =
            NoofPrint; // Convert.ToInt32(GetConnection.GetQueryData("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = string.Empty;
                if (roCom["City"].ToString() != string.Empty)
                {
                    city = $"Contact No: {roCom["City"]}{','}{roCom["PhoneNo"]}";
                }
                else if (roCom["PhoneNo"].ToString() != string.Empty)
                {
                    city = $"Contact No: {roCom["PhoneNo"]}";
                }

                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length + Convert.ToInt32((40 - roCom["Company_Name"].ToString().Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length + Convert.ToInt32((30 - roCom["Address"].ToString().Length) / 2)), PrintFontType.Contract));
                }

                if (city != string.Empty)
                {
                    strData.Append(GetPrintString(city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (pan != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)), PrintFontType.Contract));
                }

                if (i == 1)
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(8)}Abbreviated Tax Invoice", PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(12)}Copy of Original{roMaster["No_Print"]}", PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (ObjGlobal.SysDateType == "D")
                {
                    strData.Append(GetPrintString($"{("Bill No : " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}", PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{("Bill No : " + roMaster["SB_Invoice"]).MyPadRight(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) && roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() != ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() == ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ", PrintFontType.Contract));
                }

                if (roMaster["PanNo"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Vat_No"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}", PrintFontType.Contract));
                }

                if (roMaster["GlAddress"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Address"].IsValueExits())
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}", PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                if (dtDetail.Rows.Count > 0)
                {
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };

                        strData.Append(GetPrintString(
                            (roDetail["Invoice_SNo"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                            Convert.ToDecimal(roDetail["B_Amount"]).ToString("0.00").MyPadLeft(9) + string.Empty,
                            PrintFontType.Contract));
                        BasicAmt += Convert.ToDecimal(roDetail["B_Amount"]);
                    }
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Total :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));

                if (dtTerm.Rows.Count > 0)
                {
                    decimal DisTerm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        DisTerm = dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type= 'P' and ST_Id ='{PDiscountId}'").Aggregate(DisTerm, (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    }

                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString("Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                        strData.Append(GetPrintString("Net Total :".MyPadLeft(30) + (BasicAmt - DisTerm).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                        strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                    }
                }

                strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Counter : Terminal  " + roMaster["CCode"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString(string.Empty.MyPadLeft(40) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    ("User : " + roMaster["Enter_By"] + "      " + DateTime.Now.ToShortTimeString()).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}Exchange Within 7 Days with Receipt Only", PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                    PrintFontType.Contract));
                strData.Append("\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    //DefaultOrderDesign
    public void DefaultOrderDesign(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += DefaultOrderDesign;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void DefaultOrderDesign(object sender, PrintPageEventArgs e)
    {
        var strData = new StringBuilder();
        //DAL.Master.SalesTerm objSalesTerm = new DAL.Master.SalesTerm();

        var dsSB = new DataSet();
        dsSB = _salesEntry.GetConfirmationSalesDetails(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];

        var DTCD = GetConnection.SelectDataTableQuery(
            "SELECT Company_Name,Address,Country,City,State ,PhoneNo,Pan_No,Email,Website  FROM AMS.CompanyInfo");
        var roCom = DTCD.Rows[0];

        decimal roundoff = 0; //objOrder.getRoundOffVal(BillNo);
        decimal BasicAmt = 0;

        foreach (DataRow roMaster in dtMaster.Rows)
        {
            //Print Header Part from roMaster
            BasicAmt = 0;
            var city = $"{roCom["City"]}{','}{roCom["Phone"]}";
            var pan = $"PAN NO : {roCom["PanNo"]}";
            strData.Append(GetPrintString(
                roCom["CompanyName"].ToString().MyPadLeft(roCom["CompanyName"].ToString().Length +
                                                          Convert.ToInt32((30 - roCom["CompanyName"].ToString()
                                                              .Length) / 2)), PrintFontType.Contract));
            strData.Append(GetPrintString(
                roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                      Convert.ToInt32((30 - roCom["Address"].ToString().Length) /
                                                                      2)), PrintFontType.Contract));
            strData.Append(GetPrintString(city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                PrintFontType.Contract));
            strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(
                $"{("Order No: " + roMaster["VNo"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["VDate"]).ToShortDateString()}{string.Empty}",
                PrintFontType.Contract));
            strData.Append(GetPrintString(
                $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["VMiti"]}{string.Empty}", PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(
                "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            foreach (var roDetail in dtDetail.Select($"VNo = '{roMaster["VNo"]}'"))
            {
                //Print Detail Part from roDetail
                var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                var s = qty.Split('.');
                qty = Convert.ToInt32(s[1]) switch
                {
                    <= 0 => s[0],
                    _ => qty
                };

                strData.Append(GetPrintString(
                    (roDetail["Sno"] + ".").MyPadRight(3) + roDetail["PDesc"].ToString().MyPadRight(14) +
                    qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["CurrRate"]).ToString("0.00").MyPadLeft(10) +
                    Convert.ToDecimal(roDetail["CurrAmt"]).ToString("0.00").MyPadLeft(9) + string.Empty,
                    PrintFontType.Contract));
                BasicAmt += Convert.ToDecimal(roDetail["CurrAmt"]);
            }

            //Print Total
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(
                "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
            strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                PrintFontType.Contract));

            decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
            foreach (var roTerm in dtTerm.Select(
                         $"VNo = '{roMaster["VNo"]}' and TrmType= 'P' and STCode ='{PDiscountId}'"))
                DisTerm = DisTerm + Convert.ToDecimal(roTerm["CurrAmt"].ToString());
            if (DisTerm > 0)
            {
                strData.Append(GetPrintString(
                    "Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
            }

            foreach (var roTerm in dtTerm.Select(
                         $"VNo = '{roMaster["VNo"]}' and TrmType= 'P' and STCode ='{BDiscountId}'"))
                SpTerm = SpTerm + Convert.ToDecimal(roTerm["CurrAmt"].ToString());
            if (SpTerm > 0)
            {
                strData.Append(GetPrintString(
                    "Special Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
            }

            foreach (var roTerm in dtTerm.Select(
                         $"VNo = '{roMaster["VNo"]}' and TrmType= 'P' and STCode ='{ServiceChargeId}'"))
                Svterm += Convert.ToDecimal(roTerm["CurrAmt"].ToString());
            if (Svterm > 0)
            {
                strData.Append(GetPrintString(
                    "Service Charge :".MyPadLeft(30) + Svterm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
            }

            var termAmt = DisTerm + SpTerm - Svterm;
            strData.Append(GetPrintString(
                "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                string.Empty, PrintFontType.Contract));
            foreach (var roTerm in dtTerm.Select(
                         $"VNo = '{roMaster["VNo"]}' and TrmType= 'P' and STCode ='{SalesVatTermId}'"))
                vatterm = vatterm + Convert.ToDecimal(roTerm["CurrAmt"].ToString());
            if (vatterm > 0)
            {
                strData.Append(GetPrintString("Vat 13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
            }

            if (roundoff > 0)
            {
                if (roundoff > 0)
                {
                    strData.Append(GetPrintString(
                        "Roundoff :".MyPadLeft(30) + roundoff.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }
            }

            strData.Append(GetPrintString(
                "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                PrintFontType.Contract));
            strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                PrintFontType.Contract));
            var rupees = ClsMoneyConversion.MoneyConversion(Convert
                .ToDecimal(ObjGlobal.ReturnDecimal(roMaster["N_Amount"].ToString())).ToString());
            var rupees1 = rupees.Length switch
            {
                >= 28 => rupees.Substring(0, 28),
                _ => rupees
            };

            var rupees2 = (rupees.Length - 28) switch
            {
                >= 38 => rupees.Substring(28, 38),
                _ => rupees.Remove(0, rupees1.Length)
            };

            var rupees3 = (rupees.Length - 66) switch
            {
                >= 38 => rupees.Substring(66, 38),
                _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
            };

            var rupees4 = (rupees.Length - 104) switch
            {
                >= 38 => rupees.Substring(104, 38),
                _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
            };

            strData.Append(GetPrintString($"In word : {rupees1}", PrintFontType.Contract));
            if (!string.IsNullOrEmpty(rupees2))
            {
                strData.Append(GetPrintString(rupees2, PrintFontType.Contract));
            }

            if (!string.IsNullOrEmpty(rupees3))
            {
                strData.Append(GetPrintString(rupees3, PrintFontType.Contract));
            }

            if (!string.IsNullOrEmpty(rupees4))
            {
                strData.Append(GetPrintString(rupees4, PrintFontType.Contract));
            }

            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString($"Party Name: {roMaster["PartyName"]}{string.Empty}",
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(("Cashier : " + roMaster["UserCode"]).MyPadRight(25),
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("This Billis not Tax Invoice", PrintFontType.Contract));
            strData.Append(GetPrintString($"{string.Empty.PadLeft(10)}THANK YOU", PrintFontType.Contract));
            strData.Append("\n\n\n\n\n\n\n");
        }

        var result = SendStringToPrinter(Printer, strData.ToString());
        if (result)
        {
            PrintUpdate(BillNo);
        }

        strData.Clear();
        var GS = Convert.ToString((char)29);
        var ESC = Convert.ToString((char)27);
        var COMMAND = string.Empty;
        COMMAND = $"{ESC}@";
        COMMAND += $"{GS}V{(char)1}";
        SendStringToPrinter(Printer, COMMAND);
    }
    //PROFORMA INVOICE
    private void DirectProformaInvoicePrint(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                var Company = roCom["Company_Name"].ToString();
                strData.Append(GetPrintString(Company.MyPadLeft(Company.Length + Convert.ToInt32((35 - Company.Length) / 2)), PrintFontType.Bold));
                if (!string.IsNullOrWhiteSpace(roCom["Address"].ToString()))
                {
                    strData.Append(GetPrintString(roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length + Convert.ToInt32((35 - roCom["Address"].ToString().Length) / 2)), PrintFontType.Contract));
                }

                if (!string.IsNullOrWhiteSpace(roCom["City"].ToString()))
                {
                    strData.Append(GetPrintString(city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (!string.IsNullOrWhiteSpace(roCom["Pan_No"].ToString()))
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)), PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice", PrintFontType.Contract));
                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) && roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() != ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() == ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) && !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}", PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}", PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) && !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}", PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString((roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) + Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["NetAmount"].ToString())).ToString("0.00").MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += roDetail["NetAmount"].GetDecimal();
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    DisTerm += dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'").Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString("Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    SpTerm += dtTerm.Select($"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'").Sum(roTerm => roTerm["Amount"].GetDecimal());
                    if (SpTerm > 0)
                    {
                        strData.Append(GetPrintString("Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                decimal Total = 0;
                decimal Basic = 0;
                Basic = roMaster["B_Amount"].GetDecimal();
                Total = roMaster["N_Amount"].GetDecimal();
                strData.Append(GetPrintString("Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));
                strData.Append(GetPrintString("Tender Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Change Amount :".MyPadLeft(30) + Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}", PrintFontType.Contract));

                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };
                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };
                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };
                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };

                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25), PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****", PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                PrintUpdate(BillNo);
            }

            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(printer, COMMAND);
        }
    }
    private void ConformationPrint(string billno, string printer, int branch)
    {
        var strData = new StringBuilder();
        decimal BasicAmt = 0;
        var dsSales = _salesEntry.GetConfirmationSalesDetails(BillNo);
        var dtMaster = dsSales.Tables[0];
        var dtDetail = dsSales.Tables[1];
        var roCom = _companyInfo.Rows[0];
        foreach (DataRow roMaster in dtMaster.Rows)
        {
            //Print Header Part from roMaster
            var city = roCom["City"].IsValueExits() ? $"{roCom["City"]}{','}{roCom["PhoneNo"]}" : $"{roCom["PhoneNo"]}";
            var pan = roCom["Pan_No"].IsValueExits() ? $"PAN NO : {roCom["Pan_No"]}" : "";
            strData.Append(GetPrintString(
                roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                           Convert.ToInt32(
                                                               (30 - roCom["Company_Name"].ToString().Length) / 2)),
                PrintFontType.Contract));
            if (roCom["Address"].IsValueExits())
            {
                strData.Append(GetPrintString(
                    roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                          Convert.ToInt32((30 - roCom["Address"].ToString().Length) /
                                                                          2)), PrintFontType.Contract));
            }

            if (city.IsValueExits())
            {
                strData.Append(GetPrintString(city.MyPadLeft(city.Length + ((30 - city.Length) / 2).GetInt()),
                    PrintFontType.Contract));
            }

            if (pan.IsValueExits())
            {
                strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + ((30 - pan.Length) / 2).GetInt()),
                    PrintFontType.Contract));
            }

            strData.Append(GetPrintString($"{"".MyPadRight(20)} [ORDER CONFIRMATION]", PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString($"{"".MyPadRight(24)} [Table No] : {roMaster["TableName"]}",
                PrintFontType.Contract));
            strData.Append(GetPrintString(
                $"{("Order No: " + roMaster["SO_Invoice"]).MyPadRight(24)} Date: {roMaster["Invoice_Date"].GetDateString()}",
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(
                "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            foreach (var roDetail in dtDetail.Select($"SO_Invoice = '{roMaster["SO_Invoice"]}'"))
            {
                //Print Detail Part from roDetail
                var qty = roDetail["Qty"].GetDecimalString();
                var s = qty.Split('.');
                qty = s[0].GetInt() is <= 0 ? s[0] : qty;
                strData.Append(GetPrintString(
                    (roDetail["Invoice_SNo"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                    qty.MyPadLeft(4) + roDetail["Rate"].GetDecimalString().MyPadLeft(10) +
                    roDetail["B_Amount"].GetDecimalString().MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                BasicAmt += roDetail["B_Amount"].GetDecimal();
            }

            //Print Total
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                PrintFontType.Contract));
            strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                PrintFontType.Contract));

            decimal Svterm = 0, vatterm = 0;
            if (DiscountAmount.GetDecimal() > 0)
            {
                BasicAmt -= DiscountAmount.GetDecimal();
                strData.Append(GetPrintString(
                    "Discount:".MyPadLeft(30) + DiscountAmount.GetDecimal().ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
            }

            if (!roMaster["TableType"].Equals("T"))
            {
                if (ObjGlobal.SalesServiceChargeTermId > 0 && IsServiceApplicable)
                {
                    Svterm = BasicAmt * 0.10.GetDecimal();
                    BasicAmt += Svterm;
                    strData.Append(GetPrintString(
                        "Service Charge :".MyPadLeft(30) + Svterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }
            }

            if (ObjGlobal.SalesVatTermId > 0)
            {
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                vatterm = BasicAmt * 0.13.GetDecimal();
                strData.Append(GetPrintString("Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                BasicAmt += vatterm;
            }

            strData.Append(GetPrintString(
                "TOTAL :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10) + string.Empty,
                PrintFontType.Contract));

            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append("\n\n\n\n\n\n\n");
        }

        var result = SendStringToPrinter(Printer, strData.ToString());
        if (result)
        {
            PrintUpdate(BillNo);
        }

        strData.Clear();
        var GS = Convert.ToString((char)29);
        var ESC = Convert.ToString((char)27);
        var COMMAND = string.Empty;
        COMMAND = $"{ESC}@";
        COMMAND += $"{GS}V{(char)1}";
        SendStringToPrinter(Printer, COMMAND);
    }

    //KOTBOT
    public void KOTBOT(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += KOTBOTDesignNew;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    public void CashClosing(string billno, string printer)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += DayClosingPrint;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void KOTBOTDesignNew(object sender, PrintPageEventArgs e)
    {
        var strData = new StringBuilder();
        var dsSaleBill = new DataSet();
        dsSaleBill = _salesEntry.GetConfirmationSalesDetails(BillNo);

        var dtMaster = dsSaleBill.Tables[0];
        var dtDetail = dsSaleBill.Tables[1];
        var dtTerm = dsSaleBill.Tables[2];
        var dtgroup = dsSaleBill.Tables[3];

        var roMaster = dtMaster.Rows[0];
        dtDetail.DefaultView.ToTable(true, "Grpcode");

        foreach (DataRow ro in dtgroup.Rows)
        {
            var dtt = dtgroup.Select($"GrpCode= '{ro["GrpCode"]}' ");
            if (dtt.Length > 0)
            {
                strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    ro["Grpcode"].ToString().MyPadLeft(ro["Grpcode"].ToString().Length +
                                                       Convert.ToInt32((30 - ro["Grpcode"].ToString().Length) / 2)),
                    PrintFontType.Bold));
                strData.Append(GetPrintString($"Waiter: {roMaster["WaiterName"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"Table No: {roMaster["TableName"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Order No: " + roMaster["SO_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Particulars".MyPadRight(25) + "Qty".MyPadLeft(5) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                foreach (var dr in dtDetail.Select($"GrpCode= '{ro["GrpCode"]}' "))
                {
                    var qty = Convert.ToDecimal(dr["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        dr["PName"].ToString().MyPadRight(25) + qty.MyPadLeft(4) + string.Empty,
                        PrintFontType.Contract));
                }
            }
        }

        strData.Append("\n\n\n");
        strData.Append(GetPrintString(("PrintBy : " + roMaster["Enter_BY"]).MyPadRight(25), PrintFontType.Contract));
        strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
        strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
        strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
        strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
        strData.Append("\n\n\n\n");
        var result = SendStringToPrinter(Printer, strData.ToString());
        if (result)
        {
            PrintUpdate(BillNo);
        }

        strData.Clear();
        var GS = Convert.ToString((char)29);
        var ESC = Convert.ToString((char)27);
        var COMMAND = string.Empty;
        COMMAND = $"{ESC}@";
        COMMAND += $"{GS}V{(char)1}";
        SendStringToPrinter(Printer, COMMAND);
        e.HasMorePages = false;
    }
    private void KOTBOTDesign(string BillNo, string printer, PrintPageEventArgs e)
    {
        var strData = new StringBuilder();
        var dsSaleBill = _salesEntry.ReturnPrintKotDetailsInDataSet(BillNo);
        var dtMaster = dsSaleBill.Tables[0];
        var dtDetail = dsSaleBill.Tables[1];
        var dtTerm = dsSaleBill.Tables[2];

        var tblFiltered = new DataTable();
        //DataTable dtgroup = dsSaleBill.Tables[3];

        if (dtDetail.Rows.Count == 0)
        {
            return;
        }

        var roMaster = dtMaster.Rows[0];
        var dtgroup = dtDetail.DefaultView.ToTable(true, "Grpcode", "Gprinter");

        for (var i = 0; i < dtgroup.Rows.Count; i++)
        {
            var _sqlWhere = $"Grpcode = '{dtgroup.Rows[i]["Grpcode"]}'";
            var _sqlOrder = "Grpcode ASC";
            var exits = dtDetail.Select(_sqlWhere, _sqlOrder);
            if (exits.Length > 0)
            {
                tblFiltered = exits.CopyToDataTable();
            }
            else
            {
                CustomMessageBox.Information("THIS ORDER IS ALREADY PRINTED..!!");
                return;
            }

            Printer = dtgroup.Rows[i]["Gprinter"].ToString();
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(dtgroup.Rows[i]["Grpcode"].ToString().MyPadLeft(dtgroup.Rows[i]["Grpcode"].ToString().Length + Convert.ToInt32((30 - dtgroup.Rows[i]["Grpcode"].ToString().Length) / 2)), PrintFontType.Bold));
            strData.Append(GetPrintString($"Table No: {roMaster["TableName"]}{string.Empty}", PrintFontType.Contract));
            strData.Append(GetPrintString($"{("Order No: " + roMaster["SO_Invoice"]).MyPadRight(27)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}", PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("Particulars".MyPadRight(25) + "Qty".MyPadLeft(5) + string.Empty + "Time".MyPadLeft(5), PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            foreach (DataRow dr in tblFiltered.Rows)
            {
                var qty = Convert.ToDecimal(dr["Qty"]).ToString("0.00");
                var s = qty.Split('.');
                qty = Convert.ToInt32(s[1]) switch
                {
                    <= 0 => s[0],
                    _ => qty
                };
                strData.Append(GetPrintString(dr["PName"].ToString().MyPadRight(25) + qty.MyPadLeft(4) + string.Empty.PadLeft(2) + dr["OrderTime"].GetDateTime().TimeOfDay.ToString().MyPadRight(4), PrintFontType.Contract));
                if (dr["Narration"].IsValueExits())
                {
                    strData.Append(GetPrintString(("Narr : " + dr["Enter_BY"]).MyPadRight(25), PrintFontType.Contract));
                }
            }

            strData.Append("\n\n");
            strData.Append(GetPrintString(("PrintBy : " + roMaster["Enter_BY"]).MyPadRight(25), PrintFontType.Contract));
            strData.Append(GetPrintString(("PrintTime : " + DateTime.Now).MyPadRight(27), PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append("\n\n\n\n\n");
            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                UpdateOrderPrint(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    private void DayClosingPrint(object sender, PrintPageEventArgs e)
    {
        var CashClosingId = Convert.ToInt32(BillNo);
        var settings = new PrinterSettings();
        var printer = settings.PrinterName;
        var roCom = _companyInfo.Rows[0];
        var strData = new StringBuilder();
        var dtMaster = _entry.GetCashClosing(CashClosingId);
        foreach (DataRow roMaster in dtMaster.Rows)
        {
            double.TryParse(roMaster["TotalCash"].ToString(), out var _TotalValue);
            var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
            var pan = $"PAN NO : {roCom["Pan_No"]}";
            strData.Append(GetPrintString(
                roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                           Convert.ToInt32((40 - roCom["Company_Name"].ToString()
                                                               .Length) / 2)), PrintFontType.Contract));

            if (roCom["Address"].ToString() != string.Empty)
            {
                strData.Append(GetPrintString(
                    roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                          Convert.ToInt32(
                                                              (40 - roCom["Address"].ToString().Length) / 2)),
                    PrintFontType.Contract));
            }

            if (roCom["City"].ToString() != string.Empty)
            {
                strData.Append(GetPrintString(
                    city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)), PrintFontType.Contract));
            }

            if (roCom["Pan_No"].ToString() != string.Empty)
            {
                strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                    PrintFontType.Contract));
            }

            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Cash Closing", PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));

            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("Particulars".MyPadRight(15) + "Amount".MyPadLeft(15) + string.Empty,
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

            if (Convert.ToDouble(roMaster["ThauQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["ThouVal"].ToString(), out var _Value);
                double.TryParse(roMaster["ThauQty"].ToString(), out var _Qty);
                strData.Append(GetPrintString(
                    $"1000 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["FivHunQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["FivHunQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["FivHunVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"500 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["HunQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["HunQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["HunVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"100 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["FiFtyQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["FiFtyQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["FiftyVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"50 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["TwenteyFiveQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["TwenteyFiveQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["TwentyFiveVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"25 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["TwentyQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["TwentyQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["TwentyVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"20 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["TenQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["TenQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["TenVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"10 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["FiveQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["FiveQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["FiveVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"5 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["TwoQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["TwoQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["TwoVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"2 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            if (Convert.ToDouble(roMaster["OneQty"].ToString()) > 0)
            {
                double.TryParse(roMaster["OneQty"].ToString(), out var _Qty);
                double.TryParse(roMaster["OneVal"].ToString(), out var _Value);
                strData.Append(GetPrintString(
                    $"1 x {_Qty}".MyPadRight(15) + _Value.ToString().MyPadLeft(15) + string.Empty,
                    PrintFontType.Contract));
            }

            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(
                "Total".MyPadRight(15) + _TotalValue.ToString().MyPadLeft(15) + string.Empty,
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(("PrintBy : " + roMaster["EnterBy"]).MyPadRight(25),
                PrintFontType.Contract));
            strData.Append(
                GetPrintString(("PrintTime : " + DateTime.Now).MyPadRight(25), PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append("\n\n");
            var result = SendStringToPrinter(printer, strData.ToString());
            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }
    private void KOTBOTReprintDesign(string BillNo, string printer, PrintPageEventArgs e)
    {
        var strData = new StringBuilder();
        var dsSaleOrder = _salesEntry.GetConfirmationSalesDetails(BillNo);

        var dtMaster = dsSaleOrder.Tables[0];
        var dtDetail = dsSaleOrder.Tables[1];
        var dtTerm = dsSaleOrder.Tables[2];
        var dtgroup = dsSaleOrder.Tables[3];

        var roMaster = dtMaster.Rows[0];
        dtDetail.DefaultView.ToTable(true, "Grpcode");

        foreach (DataRow ro in dtgroup.Rows)
        {
            var dtt = dtgroup.Select($"GrpCode= '{ro["GrpCode"]}' ");
            if (dtt.Length > 0)
            {
                //Printer = ro["Gprinter"].ToString();
                strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    ro["Grpcode"].ToString().MyPadLeft(ro["Grpcode"].ToString().Length +
                                                       Convert.ToInt32((30 - ro["Grpcode"].ToString().Length) / 2)),
                    PrintFontType.Bold));
                strData.Append(GetPrintString($"Table No: {roMaster["TableName"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Order No: " + roMaster["SO_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Particulars".MyPadRight(25) + "Qty".MyPadLeft(5) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (DataRow dr in dtDetail.Rows)
                {
                    var qty = Convert.ToDecimal(dr["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        dr["PName"].ToString().MyPadRight(25) + qty.MyPadLeft(4) + string.Empty,
                        PrintFontType.Contract));
                    //BasicAmt += Convert.ToDecimal(roDetail["CurrAmt"]);
                }
            }

            strData.Append("\n\n\n");
            strData.Append(GetPrintString(("PrintBy : " + roMaster["Enter_BY"]).MyPadRight(25),
                PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(" ".MyPadLeft(40, ' ') + string.Empty, PrintFontType.Contract));
            strData.Append("\n\n\n\n");
            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
    }

    //DefaultReturnInvoiceDesign
    public void DefaultReturnInvoice(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += DefaultReturnInvoice;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void DefaultReturnInvoice(object sender, PrintPageEventArgs e)
    {
        var objSalesReturn = new ClsSalesEntry();
        var dsSR = objSalesReturn.ReturnSalesReturnDetailsInDataSet(BillNo);
        var dtMaster = dsSR.Tables[0];
        var dtDetail = dsSR.Tables[1];
        var dtTerm = dsSR.Tables[2];
        var roCom = _companyInfo.Rows[0];

        decimal BasicAmt = 0;

        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((30 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                          Convert.ToInt32(
                                                              (30 - roCom["Address"].ToString().Length) / 2)),
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(i == 1
                    ? GetPrintString($"{string.Empty.MyPadRight(13)}TAX INVOICE", PrintFontType.Contract)
                    : GetPrintString($"{string.Empty.MyPadRight(13)}INVOICE", PrintFontType.Contract));

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                strData.Append(!string.IsNullOrEmpty(roMaster["Party_Name"].ToString())
                    ? GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract)
                    : GetPrintString("Purchaser Name: Cash", PrintFontType.Contract));

                strData.Append(!string.IsNullOrEmpty(roMaster["PanNo"].ToString())
                    ? GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract)
                    : GetPrintString("Purchaser PAN: ", PrintFontType.Contract));

                strData.Append(!string.IsNullOrEmpty(roMaster["GlAddress"].ToString())
                    ? GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}", PrintFontType.Contract)
                    : GetPrintString("Address", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                //

                strData.Append(GetPrintString("Mode of Payment:", PrintFontType.Contract));

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                //
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));

                //   strData.Append(RawPrinterHelper.GetPrintString("".MyPadLeft(26) + "Tax Rate : 13%" + "", RawPrinterHelper.PrintFontType.Contract, true));

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };

                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(roDetail["B_Amount"]).ToString("0.00").MyPadLeft(9) + string.Empty,
                        PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(roDetail["B_Amount"]);
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type= 'P' and ST_Id ='{PDiscountId}'"))
                        DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type= 'P' and ST_Id ='{BDiscountId}'"))
                        SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Special Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type= 'P' and ST_Id ='{ServiceChargeId}'"))
                        Svterm = Svterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (Svterm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + Svterm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type= 'P' and ST_Id ='{SalesVatTermId}'"))
                        vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (vatterm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Vat :".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = string.Empty;
                rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                var rupees2 = string.Empty;
                rupees2 = rupees.Length - 28 >= 38 ? rupees.Substring(28, 38) : rupees.Remove(0, rupees1.Length);
                var rupees3 = string.Empty;
                rupees3 = rupees.Length - 66 >= 38
                    ? rupees.Substring(66, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length);
                var rupees4 = string.Empty;
                rupees4 = rupees.Length - 104 >= 38
                    ? rupees.Substring(104, 38)
                    : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);

                strData.Append(GetPrintString($"In word : {rupees1}", PrintFontType.Contract));

                if (!string.IsNullOrEmpty(rupees2))
                {
                    strData.Append(GetPrintString(rupees2, PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(rupees3))
                {
                    strData.Append(GetPrintString(rupees3, PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(rupees4))
                {
                    strData.Append(GetPrintString(rupees4, PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(10)}THANK YOU FOR VISIT",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
    }

    //AbbreviatedSalesReturnInvoiceDesign
    public void AbbreviatedTaxReturnInvoice3inch(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += AbbreviatedTaxReturnInvoice3inch;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void AbbreviatedTaxReturnInvoice3inch(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy =
            NoofPrint; // Convert.ToInt32(GetConnection.GetQueryData("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = string.Empty;
                city = roCom["City"].ToString() != string.Empty
                    ? $"{roCom["City"]}{','}{roCom["PhoneNo"]}"
                    : roCom["PhoneNo"].ToString();

                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((40 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                          Convert.ToInt32(
                                                              (40 - roCom["Address"].ToString().Length) / 2)),
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)), PrintFontType.Contract));
                strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                    PrintFontType.Contract));

                strData.Append(i == 1
                    ? GetPrintString($"{string.Empty.MyPadRight(8)}Abbreviated Tax Invoice",
                        PrintFontType.Contract)
                    : GetPrintString($"{string.Empty.MyPadRight(13)}INVOICE",
                        PrintFontType.Contract));

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(ObjGlobal.SysDateType == "D"
                    ? GetPrintString(
                        $"{("Bill No : " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                        PrintFontType.Contract)
                    : GetPrintString(
                        $"{("Bill No : " + roMaster["SR_Invoice"]).MyPadRight(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));

                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()))
                {
                    strData.Append(GetPrintString($"Customer: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Customer: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString("Customer : Cash", PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                if (dtDetail.Rows.Count > 0)
                {
                    foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };

                        strData.Append(GetPrintString(
                            (roDetail["Invoice_SNo"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                            Convert.ToDecimal(roDetail["B_Amount"]).ToString("0.00").MyPadLeft(9) + string.Empty,
                            PrintFontType.Contract));
                        BasicAmt += Convert.ToDecimal(roDetail["B_Amount"]);
                    }
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("Total :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));

                if (dtTerm.Rows.Count > 0)
                {
                    decimal DisTerm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type= 'P' and ST_Id ='{PDiscountId}'"))
                            DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    }

                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            "Net Total :".MyPadLeft(30) + (BasicAmt - DisTerm).ToString("0.00").MyPadLeft(10) +
                            string.Empty, PrintFontType.Contract));
                        strData.Append(GetPrintString(
                            $"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                            PrintFontType.Contract));
                    }
                }

                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = string.Empty;

                rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };

                strData.Append(GetPrintString($"In word : {rupees1}", PrintFontType.Contract));
                if (!string.IsNullOrEmpty(rupees2))
                {
                    strData.Append(GetPrintString(rupees2, PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(rupees3))
                {
                    strData.Append(GetPrintString(rupees3, PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(rupees4))
                {
                    strData.Append(GetPrintString(rupees4, PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                //strData.Append(RawPrinterHelper.GetPrintString(("Counter : Terminal  " + roMaster["CCode"].ToString()).MyPadRight(25), RawPrinterHelper.PrintFontType.Contract, true));
                strData.Append(GetPrintString(string.Empty.MyPadLeft(40) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    ("User : " + roMaster["Enter_By"] + "      " + DateTime.Now.ToShortTimeString()).MyPadRight(25),
                    PrintFontType.Contract));

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}***Goods one sold will not be return***", PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.PadLeft(0)}Exchange Within 7 Days with Receipt Only", PrintFontType.Contract));
                strData.Append(GetPrintString(string.Empty.MyPadLeft(40) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
    }
    //RestaurantDesignWithVAT5inch
    public void RestaurantDesignWithVAT5Inch(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += RestaurantDesignWithVAT5Inch;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void RestaurantDesignWithVAT5Inch(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;

        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    strData.Append(ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0
                        ? GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice",
                            PrintFontType.Contract)
                        : GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                            PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                        PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Payment_Mode"].ToString()) &&
                    roMaster["Payment_Mode"].ToString() == "Cash" && roMaster["Customer_Id"].GetLong() ==
                    ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                        DisTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                        SpTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        SpTerm /= Convert.ToDecimal(1.13);
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                        vatterm += Convert.ToDecimal(roTerm["Amount"].ToString());
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                //strData.Append(RawPrinterHelper.GetPrintString("".PadLeft(0) + "Exchange & Return will be Within 7 Days", RawPrinterHelper.PrintFontType.Contract, true));
                //strData.Append(RawPrinterHelper.GetPrintString("".PadLeft(0) + "Goods Once sold will not be Exchanged or returned", RawPrinterHelper.PrintFontType.Contract, true));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                //strData.Append(RawPrinterHelper.GetPrintString("".PadLeft(10) + "THANK YOU FOR VISIT", RawPrinterHelper.PrintFontType.Contract, true));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
    }

    //RestaurantDesignWithPAN5inch
    public void RestaurantDesignWithPAN5Inch(string billno, string printer, int branch)
    {
        BillNo = billno;
        Printer = printer;
        Document = new System.Drawing.Printing.PrintDocument();
        Document.PrintPage += RestaurantDesignWithPAN5Inch;
        Document.PrinterSettings.PrinterName = printer;
        Document.Print();
    }
    private void RestaurantDesignWithPAN5Inch(object sender, PrintPageEventArgs e)
    {
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];

        decimal BasicAmt = 0;
        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;

                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"]
                                                                   .ToString().Length) / 2)),
                    PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                if (i == 1)
                {
                    strData.Append(ObjGlobal.ReturnDouble(roMaster["No_Print"].ToString()) is 0
                        ? GetPrintString($"{string.Empty.MyPadRight(13)}Tax Invoice",
                            PrintFontType.Contract)
                        : GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                            PrintFontType.Contract));
                }
                else
                {
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Invoice",
                        PrintFontType.Contract));
                }

                if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                {
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["Payment_Mode"].ToString()) &&
                    roMaster["Payment_Mode"].ToString() == "Cash" && roMaster["Customer_Id"].GetLong() ==
                    ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SB_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SB_Invoice = '{roMaster["SB_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                        roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                        Convert.ToDecimal(roDetail["BeforeVAT"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["BAmount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                if (PDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                        DisTerm = DisTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (DisTerm > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                        SpTerm = SpTerm + Convert.ToDecimal(roTerm["Amount"].ToString());
                    if (SpTerm > 0)
                    {
                        SpTerm = SpTerm / Convert.ToDecimal(1.13);
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                var termAmt = DisTerm + SpTerm - Svterm;
                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (BasicAmt - termAmt).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                if (SalesVatTermId != string.Empty)
                {
                    foreach (var roTerm in dtTerm.Select(
                                 $"SB_VNo = '{roMaster["SB_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'"))
                        vatterm = vatterm + Convert.ToDecimal(roTerm["Amount"].ToString());
                }

                if (vatterm > 0)
                {
                    strData.Append(GetPrintString(
                        "Vat @13%:".MyPadLeft(30) + vatterm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                    string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Tender Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Tender_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Change Amount :".MyPadLeft(30) +
                    Convert.ToDecimal(roMaster["Return_Amount"]).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(Printer, strData.ToString());
            if (result)
            {
                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
    }
    private void Inch5SalesTaxInvoicePrint(string voucherNo, string printer, string branch, int noofcopy)
    {
        DataRow dataRow;
        DateTime now;
        int j;
        decimal num;
        var stringBuilder = new StringBuilder();
        var dsSB = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        for (var i = 1; i <= noofcopy; i++)
        {
            var num1 = new decimal(0);
            var num2 = new decimal(0);
            foreach (DataRow row in dtMaster.Rows)
            {
                num1 = new decimal(0);

                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                stringBuilder.Append(GetPrintString(roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length + Convert.ToInt32((65 - roCom["Company_Name"].ToString().Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString(roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length + Convert.ToInt32((65 - roCom["Address"].ToString().Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString(city.MyPadLeft(city.Length + Convert.ToInt32((65 - city.Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((65 - pan.Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString("KOT/BOT".MyPadLeft("KOT/BOT".Length + Convert.ToInt32((65 - "KOT/BOT".Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString($"{$"O/A No:{row["Sb_OrderNo"]}".Trim().MyPadRight(48)}Miti:{row["Sb_OrderMiti"]}", PrintFontType.Contract));
                if (!string.IsNullOrEmpty(row["Party_Name"].ToString()))
                {
                    stringBuilder.Append(GetPrintString($"{$"Customer : {row["Party_Name"]}".MyPadRight(48)}Table: {row["Table_Name"]}", PrintFontType.Contract));
                    var str2 = $"Vat No : {row["Vat_No"]}".MyPadRight(48);
                    now = DateTime.Now;
                    stringBuilder.Append(GetPrintString($"{str2}Time: {now.ToShortTimeString()}", PrintFontType.Contract));
                }
                else
                {
                    stringBuilder.Append(GetPrintString($"{$"Customer : {row["Gl_Desc"]}".MyPadRight(48)}Table: {row["Table_Name"]}", PrintFontType.Contract));
                    var str3 = $"Vat No : {row["Vat_No"]}".MyPadRight(48);
                    now = DateTime.Now;
                    stringBuilder.Append(GetPrintString($"{str3}Time: {now.ToShortTimeString()}", PrintFontType.Contract));
                }

                stringBuilder.Append(GetPrintString("-".MyPadLeft(65, '-') ?? string.Empty, PrintFontType.Contract));
                string[] strArrays =
                {
                    "SN.".MyPadRight(3), "Particulars".MyPadRight(37), "Qty".MyPadLeft(3), "Rate".MyPadLeft(12), "Amount".MyPadLeft(9)
                };
                stringBuilder.Append(GetPrintString(string.Concat(strArrays), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString("-".MyPadLeft(65, '-') ?? string.Empty, PrintFontType.Contract));
                var dataRowArray = dtDetail.Select($"Sb_OrderNo = '{row["Sb_OrderNo"]}'");
                for (j = 0; j < dataRowArray.Length; j++)
                {
                    var dataRow1 = dataRowArray[j];
                    num = Convert.ToDecimal(dataRow1["Qty"]);
                    var str4 = num.ToString("0.00");
                    var strArrays1 = str4.Split('.');
                    str4 = Convert.ToInt32(strArrays1[1]) switch
                    {
                        <= 0 => strArrays1[0],
                        _ => str4
                    };
                    if (!string.IsNullOrEmpty(dataRow1["Remarks"].ToString().Trim()))
                    {
                        strArrays = new[]
                        {
                            $"{dataRow1["Sno"]}.".MyPadRight(3),
                            dataRow1["Remarks"].ToString().Trim().MyPadRight(37), str4.MyPadLeft(3), null, null
                        };
                        num = Convert.ToDecimal(dataRow1["Rate"]);
                        strArrays[3] = num.ToString("0.00").MyPadLeft(12);
                        num = Convert.ToDecimal(dataRow1["Rate"]) * Convert.ToDecimal(str4);
                        strArrays[4] = num.ToString("0.00").MyPadLeft(9);
                        stringBuilder.Append(GetPrintString(string.Concat(strArrays), PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(dataRow1["Add_Desc"].ToString().Trim()))
                    {
                        strArrays = new[]
                        {
                            $"{dataRow1["Sno"]}.".MyPadRight(3),
                            dataRow1["P_Desc"].ToString().Trim().MyPadRight(37), str4.MyPadLeft(3), null, null
                        };
                        num = Convert.ToDecimal(dataRow1["Rate"]);
                        strArrays[3] = num.ToString("0.00").MyPadLeft(12);
                        num = Convert.ToDecimal(dataRow1["Rate"]) * Convert.ToDecimal(str4);
                        strArrays[4] = num.ToString("0.00").MyPadLeft(9);
                        stringBuilder.Append(GetPrintString(string.Concat(strArrays), PrintFontType.Contract));
                    }
                    else
                    {
                        strArrays = new[]
                        {
                            $"{dataRow1["Sno"]}.".MyPadRight(3),
                            dataRow1["Add_Desc"].ToString().Trim().MyPadRight(37), str4.MyPadLeft(3), null, null
                        };
                        num = Convert.ToDecimal(dataRow1["Rate"]);
                        strArrays[3] = num.ToString("0.00").MyPadLeft(12);
                        num = Convert.ToDecimal(dataRow1["Rate"]) * Convert.ToDecimal(str4);
                        strArrays[4] = num.ToString("0.00").MyPadLeft(9);
                        stringBuilder.Append(GetPrintString(string.Concat(strArrays), PrintFontType.Contract));
                    }

                    num1 = num1 + Convert.ToDecimal(dataRow1["Rate"]) * Convert.ToDecimal(str4);
                }

                stringBuilder.Append(GetPrintString("-".MyPadLeft(65, '-') ?? string.Empty, PrintFontType.Contract));
                stringBuilder.Append(GetPrintString($"{"Sub Amount ".MyPadLeft(53)}:{num1.ToString("0.00").MyPadLeft(10)}", PrintFontType.Contract));
                stringBuilder.Append(GetPrintString($"{"-".MyPadLeft(42, ' ')}-----------------------", PrintFontType.Contract));
                num2 = num1;
                //dataRowArray = item1.Select(string.Concat("ST_ShortName < '", systemControl.ShortNameforVat, "'"));
                for (j = 0; j < dataRowArray.Length; j++)
                {
                    dataRow = dataRowArray[j];
                    if (Convert.ToDecimal(dataRow["Term_Amt"]) != new decimal(0))
                    {
                        var str5 = dataRow["ST_Desc"].ToString().MyPadLeft(53);
                        num = Math.Abs(Convert.ToDecimal(dataRow["Term_Amt"]));
                        stringBuilder.Append(GetPrintString($"{str5}:{num.ToString("0.00").MyPadLeft(10)}", PrintFontType.Contract));
                    }

                    num2 += Convert.ToDecimal(dataRow["Term_Amt"]);
                }

                var str6 = "TAXABLE VALUE ".MyPadLeft(53);
                num = Convert.ToDecimal(num2);
                stringBuilder.Append(GetPrintString($"{str6}:{num.ToString("0.00").MyPadLeft(10)}", PrintFontType.Contract));
                //dataRowArray = item1.Select(string.Concat("ST_ShortName = '", systemControl.ShortNameforVat, "'"));
                for (j = 0; j < dataRowArray.Length; j++)
                {
                    dataRow = dataRowArray[j];
                    var str7 = dataRow["ST_Desc"].ToString().MyPadLeft(53);
                    num = Math.Abs(Convert.ToDecimal(dataRow["Term_Amt"]));
                    stringBuilder.Append(GetPrintString($"{str7}:{num.ToString("0.00").MyPadLeft(10)}", PrintFontType.Contract));
                }

                ////dataRowArray = item1.Select(string.Concat("ST_ShortName > '", systemControl.ShortNameforVat, "'"));
                for (j = 0; j < dataRowArray.Length; j++)
                {
                    dataRow = dataRowArray[j];
                    if (Convert.ToDecimal(dataRow["Term_Amt"]) != new decimal(0))
                    {
                        var str8 = dataRow["ST_Desc"].ToString().MyPadLeft(53);
                        num = Math.Abs(Convert.ToDecimal(dataRow["Term_Amt"]));
                        stringBuilder.Append(GetPrintString($"{str8}:{num.ToString("0.00").MyPadLeft(10)}", PrintFontType.Contract));
                    }
                }

                stringBuilder.Append(GetPrintString("-".MyPadLeft(65, '-') ?? string.Empty, PrintFontType.Contract));
                var str9 = "Net Total ".MyPadLeft(53);
                num = Convert.ToDecimal(row["Net_Amt"]);
                stringBuilder.Append(GetPrintString($"{str9}:{num.ToString("0.00").MyPadLeft(10)}", PrintFontType.Contract));
                stringBuilder.Append(GetPrintString("-".MyPadLeft(65, '-') ?? string.Empty, PrintFontType.Contract));
                //string words = NumberToWord.ToWords(Convert.ToDecimal(row["Net_Amt"]));
                var str10 = string.Empty;
                //str10 = (words.Length < 53 ? words : words.Substring(0, 53));
                var str11 = string.Empty;
                //str11 = (words.Length - 53 < 63 ? words.Remove(0, str10.Length) : words.Substring(116, 63));
                var str12 = string.Empty;
                //str12 = (words.Length - 116 < 63 ? words.Remove(0, str10.Length + str11.Length) : words.Substring(116, 63));
                var str13 = string.Empty;
                //str13 = (words.Length - 179 < 63 ? words.Remove(0, str10.Length + str11.Length + str12.Length) : words.Substring(179, 63));
                stringBuilder.Append(GetPrintString($"In word : {str10}", PrintFontType.Contract));
                if (!string.IsNullOrEmpty(str11))
                {
                    stringBuilder.Append(GetPrintString(str11, PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(str12))
                {
                    stringBuilder.Append(GetPrintString(str12, PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(str13))
                {
                    stringBuilder.Append(GetPrintString(str13, PrintFontType.Contract));
                }

                stringBuilder.Append(GetPrintString("-".MyPadLeft(65, '-') ?? string.Empty, PrintFontType.Contract));
                stringBuilder.Append(GetPrintString("Final Invoice will be generated after Receipt.".MyPadLeft("Final Invoice will be generated after Receipt.".Length + Convert.ToInt32((65 - "Final Invoice will be generated after Receipt.".Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString("This is not Tax Invoice".MyPadLeft("This is not Tax Invoice.".Length + Convert.ToInt32((65 - "This is not Tax Invoice.".Length) / 2)), PrintFontType.Contract));
                stringBuilder.Append(GetPrintString($"{$"Prepared By: {row["UserName"]}".MyPadRight(42)}Staff: {row["Agent_Desc"]}", PrintFontType.Contract));
                stringBuilder.Append("\n\n\n\n\n\n\n\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, stringBuilder.ToString());
            if (result)
            {
                stringBuilder.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
    }

    //SALES RETURN PRINT IN DEFAULT
    public void DirectDefaultReturnInvoiceDesign(string billno, string printer, int branch)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            Printer = printer;
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((40 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)),
                        PrintFontType.Contract));

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32((40 - roCom["Address"].ToString()
                                                                      .Length) / 2)), PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Credit Note",
                        PrintFontType.Contract));
                    if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                        roMaster["Party_Name"].ToString() != "Cash")
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() !=
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() ==
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                        string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract)); //roMaster["Payment_Mode"].ToString())
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) +
                        "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));

                    foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                            (Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]))
                            .ToString("0.00").MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]);
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                            DisTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                            SpTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                    Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                    strData.Append(GetPrintString(
                        "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                        string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                    var rupees2 = rupees.Length - 28 >= 38
                        ? rupees.Substring(28, 38)
                        : rupees.Remove(0, rupees1.Length);
                    var rupees3 = rupees.Length - 66 >= 38
                        ? rupees.Substring(66, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length);
                    var rupees4 = rupees.Length - 104 >= 38
                        ? rupees.Substring(104, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);
                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                        PrintFontType.Contract));
                    strData.Append("\n\n\n\n");
                    BillNo = roMaster["SR_Invoice"].ToString();
                }

                if (printer.IsBlankOrEmpty())
                {
                    printer = Printer;
                }

                var result = SendStringToPrinter(printer, strData.ToString());
                if (result)
                {
                    PrintSalesReturnUpdate(BillNo);
                }

                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }
    public void DirectDefaultReturnPerformaInvoiceDesign(string billno, string printer, int branch)
    {
        try
        {
            var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(BillNo);
            var dtMaster = dsSB.Tables[0];
            var dtDetail = dsSB.Tables[1];
            var dtTerm = dsSB.Tables[2];
            var roCom = _companyInfo.Rows[0];
            Printer = printer;
            decimal BasicAmt = 0;

            int noofcopy = NoofPrint;

            for (var i = 1; i <= noofcopy; i++)
            {
                var strData = new StringBuilder();
                foreach (DataRow roMaster in dtMaster.Rows)
                {
                    //Print Header Part from roMaster
                    BasicAmt = 0;
                    var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                    var pan = $"PAN NO : {roCom["Pan_No"]}";
                    strData.Append(GetPrintString(
                        roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                                   Convert.ToInt32((40 - roCom["Company_Name"]
                                                                       .ToString().Length) / 2)),
                        PrintFontType.Contract));

                    if (roCom["Address"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                                  Convert.ToInt32((40 - roCom["Address"].ToString()
                                                                      .Length) / 2)), PrintFontType.Contract));
                    }

                    if (roCom["City"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            city.MyPadLeft(city.Length + Convert.ToInt32((40 - city.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    if (roCom["Pan_No"].ToString() != string.Empty)
                    {
                        strData.Append(GetPrintString(
                            pan.MyPadLeft(pan.Length + Convert.ToInt32((40 - pan.Length) / 2)),
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Performa Credit Note",
                        PrintFontType.Contract));
                    if (Convert.ToInt32(roMaster["No_Print"].ToString()) > 0)
                    {
                        strData.Append(GetPrintString(
                            $"{string.Empty.MyPadRight(12)}Copy of Original {roMaster["No_Print"]}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                        roMaster["Party_Name"].ToString() != "Cash")
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() !=
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (roMaster["Customer_Id"].GetLong() ==
                             ObjGlobal.FinanceCashLedgerId)
                    {
                        strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                        string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                    {
                        strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                            PrintFontType.Contract));
                    }
                    else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                             !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                    {
                        strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                            PrintFontType.Contract));
                    }

                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                        PrintFontType.Contract)); //roMaster["Payment_Mode"].ToString())
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                        PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) +
                        "Rate".MyPadLeft(9) + "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));

                    foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                    {
                        //Print Detail Part from roDetail
                        var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                        var s = qty.Split('.');
                        qty = Convert.ToInt32(s[1]) switch
                        {
                            <= 0 => s[0],
                            _ => qty
                        };
                        strData.Append(GetPrintString(
                            (roDetail["Invoice_Sno"] + ".").MyPadRight(3) +
                            roDetail["PName"].ToString().MyPadRight(14) + qty.MyPadLeft(4) +
                            Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                            (Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]))
                            .ToString("0.00").MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                        BasicAmt += Convert.ToDecimal(roDetail["Qty"]) * Convert.ToDecimal(roDetail["Rate"]);
                    }

                    //Print Total
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString(
                        "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    decimal DisTerm = 0, SpTerm = 0, Svterm = 0, vatterm = 0;
                    if (PDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'"))
                            DisTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (DisTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + DisTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    if (BDiscountId != string.Empty)
                    {
                        foreach (var roTerm in dtTerm.Select(
                                     $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'"))
                            SpTerm += Convert.ToDecimal(roTerm["Amount"].ToString());
                        if (SpTerm > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + SpTerm.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }

                    var termAmt = DisTerm + SpTerm - Svterm;
                    decimal Total = 0;
                    decimal Basic = 0;
                    Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                    Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                    strData.Append(GetPrintString(
                        "Total :".MyPadLeft(30) + (BasicAmt - termAmt + vatterm).ToString("0.00").MyPadLeft(10) +
                        string.Empty, PrintFontType.Contract));
                    strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                        PrintFontType.Contract));

                    var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                    var rupees1 = rupees.Length >= 28 ? rupees.Substring(0, 28) : rupees;
                    var rupees2 = rupees.Length - 28 >= 38
                        ? rupees.Substring(28, 38)
                        : rupees.Remove(0, rupees1.Length);
                    var rupees3 = rupees.Length - 66 >= 38
                        ? rupees.Substring(66, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length);
                    var rupees4 = rupees.Length - 104 >= 38
                        ? rupees.Substring(104, 38)
                        : rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length);
                    strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                    strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty,
                        PrintFontType.Contract));
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}****Thank You Visit Again****",
                        PrintFontType.Contract));
                    strData.Append("\n\n\n\n");
                    BillNo = roMaster["SR_Invoice"].ToString();
                }

                if (printer.IsBlankOrEmpty())
                {
                    printer = Printer;
                }

                var result = SendStringToPrinter(printer, strData.ToString());
                if (result)
                {
                    PrintSalesReturnUpdate(BillNo);
                }

                strData.Clear();
                var GS = Convert.ToString((char)29);
                var ESC = Convert.ToString((char)27);
                var COMMAND = string.Empty;
                COMMAND = $"{ESC}@";
                COMMAND += $"{GS}V{(char)1}";
                SendStringToPrinter(Printer, COMMAND);
            }
        }
        catch (Exception ex)
        {
            var Msg = ex.Message;
            throw new ArgumentException(ex.Message);
        }
    }
    public void DirectDefaultReturnInvoiceWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(BillNo);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        Printer = printer;
        decimal BasicAmt = 0;

        int noofcopy =
            NoofPrint; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));

        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"].ToString()
                                                                   .Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].IsValueExits())
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].IsValueExits())
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].IsValueExits())
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)}Credit Notes", PrintFontType.Contract));
                if (!string.IsNullOrEmpty(roMaster["Party_Name"].ToString()) &&
                    roMaster["Party_Name"].ToString() != "Cash")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(23)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(23)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = roDetail["Qty"].GetDecimalString();
                    var s = qty.Split('.');
                    qty = s[1].GetInt() switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + roDetail["BeforeVAT"].GetDecimalString().MyPadLeft(10) +
                        roDetail["B_Amount"].GetDecimalString().MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;
                if (PDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        pDiscount += dtTerm
                            .Select(
                                $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (pDiscount > 0)
                        {
                            strData.Append(GetPrintString(
                                "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        bDiscount += dtTerm
                            .Select(
                                $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (bDiscount > 0)
                        {
                            strData.Append(GetPrintString(
                                "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        serviceCharge += dtTerm
                            .Select(
                                $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                            .Sum(roTerm => Convert.ToDecimal(roTerm["Amount"].ToString()));
                        if (serviceCharge > 0)
                        {
                            strData.Append(GetPrintString(
                                "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                                PrintFontType.Contract));
                        }
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());

                if (SalesVatTermId != string.Empty)
                {
                    if (dtTerm.Rows.Count > 0)
                    {
                        vatTerm = dtTerm
                            .Select(
                                $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                            .Aggregate(vatTerm,
                                (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    }
                }

                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (Total - vatTerm).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                if (vatTerm > 0)
                {
                    strData.Append(GetPrintString("Vat @13%:".MyPadLeft(30) + vatTerm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }
                //BasicAmt - vatTerm or BasicAmt + vatTerm
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + (BasicAmt + vatTerm).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString(
                    ("Cashier : " + roMaster["Enter_By"].ToString().ToUpper()).MyPadRight(25), PrintFontType.Contract));
                strData.Append(GetPrintString($"Print Time : {DateTime.Now}", PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
            }

            var result = SendStringToPrinter(printer, strData.ToString());
            if (result)
            {
                PrintSalesReturnUpdate(BillNo);
            }

            strData.Clear();
            var gs = Convert.ToString((char)29);
            var esc = Convert.ToString((char)27);
            var command = string.Empty;
            command = $"{esc}@";
            command += $"{gs}V{(char)1}";
            SendStringToPrinter(Printer, command);
        }
    }
    private void DirectRestaurantReturnDesignWithPAN(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        var result =
            false; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"].ToString()
                                                                   .Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(9)} Credit Notes", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                        .Aggregate(pDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                        .Aggregate(bDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                        .Aggregate(serviceCharge,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Remarks : " + roMaster["Remarks"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }

    private void DirectRestaurantReturnDesignWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        var result =
            false; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= NoofPrint; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32((35 - roCom["Company_Name"].ToString()
                                                                   .Length) / 2)), PrintFontType.Contract));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Credit Notes", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                        .Aggregate(pDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                        .Aggregate(bDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                        .Aggregate(serviceCharge,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());

                if (SalesVatTermId != string.Empty)
                {
                    vatTerm = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                        .Aggregate(vatTerm,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                }

                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (Total - vatTerm).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                if (vatTerm > 0)
                {
                    strData.Append(GetPrintString("Vat @13%:".MyPadLeft(30) + vatTerm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Remarks : " + roMaster["Remarks"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }

    private void DirectThermalRestaurantReturnDesignWithPAN(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        int noofcopy = NoofPrint;
        var result =
            false; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= noofcopy; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32(20 - roCom["Company_Name"].ToString()
                                                                   .Length / 2)), PrintFontType.Expand));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(9)} Credit Notes", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                        .Aggregate(pDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                        .Aggregate(bDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                        .Aggregate(serviceCharge,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());
                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Remarks : " + roMaster["Remarks"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            if (result)
            {
                PrintUpdate(BillNo);
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }

    private void DirectThermalRestaurantReturnDesignWithVAT(string billno, string printer, int branch)
    {
        var dsSB = _salesEntry.GetSalesReturnDetailsForPrint(billno);
        var dtMaster = dsSB.Tables[0];
        var dtDetail = dsSB.Tables[1];
        var dtTerm = dsSB.Tables[2];
        var roCom = _companyInfo.Rows[0];
        decimal BasicAmt = 0;
        var result =
            false; // Convert.ToInt32 ( GetConnection.GetQueryData ("Select Noofcopy from AMS.SystemConfiguration"));
        for (var i = 1; i <= NoofPrint; i++)
        {
            var strData = new StringBuilder();
            foreach (DataRow roMaster in dtMaster.Rows)
            {
                //Print Header Part from roMaster
                BasicAmt = 0;
                var city = $"{roCom["City"]}{','}{roCom["PhoneNo"]}";
                var pan = $"PAN NO : {roCom["Pan_No"]}";
                strData.Append(GetPrintString(
                    roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                               Convert.ToInt32(20 - roCom["Company_Name"].ToString()
                                                                   .Length / 2)), PrintFontType.Expand));
                if (roCom["Address"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                              Convert.ToInt32(
                                                                  (30 - roCom["Address"].ToString().Length) / 2)),
                        PrintFontType.Contract));
                }

                if (roCom["City"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(
                        city.MyPadLeft(city.Length + Convert.ToInt32((30 - city.Length) / 2)), PrintFontType.Contract));
                }

                if (roCom["Pan_No"].ToString() != string.Empty)
                {
                    strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + Convert.ToInt32((30 - pan.Length) / 2)),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.MyPadRight(13)} Credit Notes", PrintFontType.Contract));
                var customer = roMaster["Party_Name"].ToString();
                if (customer.IsValueExits() && roMaster["Party_Name"].GetUpper() != "CASH")
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["Party_Name"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() !=
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"Purchaser Name: {roMaster["GLName"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (roMaster["Customer_Id"].GetLong() ==
                         ObjGlobal.FinanceCashLedgerId)
                {
                    strData.Append(GetPrintString($"{string.Empty.PadLeft(0)}Purchaser Name: Cash A/c ",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                    string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["PanNo"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["PanNo"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Vat_No"].ToString()))
                {
                    strData.Append(GetPrintString($"Purchaser PAN: {roMaster["Vat_No"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                if (!string.IsNullOrEmpty(roMaster["GlAddress"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["GlAddress"]}{string.Empty}",
                        PrintFontType.Contract));
                }
                else if (string.IsNullOrEmpty(roMaster["GlAddress"].ToString()) &&
                         !string.IsNullOrEmpty(roMaster["Address"].ToString()))
                {
                    strData.Append(GetPrintString($"Address: {roMaster["Address"]}{string.Empty}",
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"Mode of Payment: {roMaster["Payment_Mode"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{("Inv No: " + roMaster["SR_Invoice"]).MyPadRight(24)}Date: {Convert.ToDateTime(roMaster["Invoice_Date"]).ToShortDateString()}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString(
                    $"{string.Empty.MyPadLeft(24)}Miti: {roMaster["Invoice_Miti"]}{string.Empty}",
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

                strData.Append(GetPrintString(
                    "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                    "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                foreach (var roDetail in dtDetail.Select($"SR_Invoice = '{roMaster["SR_Invoice"]}'"))
                {
                    //Print Detail Part from roDetail
                    var qty = Convert.ToDecimal(roDetail["Qty"]).ToString("0.00");
                    var s = qty.Split('.');
                    qty = Convert.ToInt32(s[1]) switch
                    {
                        <= 0 => s[0],
                        _ => qty
                    };
                    strData.Append(GetPrintString(
                        (roDetail["Invoice_Sno"] + ".").MyPadRight(3) + roDetail["PName"].ToString().MyPadRight(14) +
                        qty.MyPadLeft(4) + Convert.ToDecimal(roDetail["Rate"]).ToString("0.00").MyPadLeft(10) +
                        Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString())).ToString("0.00")
                            .MyPadLeft(9) + string.Empty, PrintFontType.Contract));
                    BasicAmt += Convert.ToDecimal(ObjGlobal.ReturnDecimal(roDetail["B_Amount"].ToString()));
                }

                //Print Total
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(
                    "Basic Amount :".MyPadLeft(30) + BasicAmt.ToString("0.00").MyPadLeft(10), PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));

                decimal pDiscount = 0, bDiscount = 0, serviceCharge = 0, vatTerm = 0;

                if (PDiscountId != string.Empty)
                {
                    pDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type  in ( 'P','BT') and ST_Id ='{PDiscountId}'")
                        .Aggregate(pDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (pDiscount > 0)
                    {
                        strData.Append(GetPrintString(
                            "Pro Discount :".MyPadLeft(30) + pDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (BDiscountId != string.Empty)
                {
                    bDiscount = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{BDiscountId}'")
                        .Aggregate(bDiscount,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (bDiscount > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Discount :".MyPadLeft(30) + bDiscount.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                if (ServiceChargeId != string.Empty)
                {
                    serviceCharge = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{ServiceChargeId}'")
                        .Aggregate(serviceCharge,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                    if (serviceCharge > 0)
                    //bDiscount = bDiscount / Convert.ToDecimal(1.13);
                    {
                        strData.Append(GetPrintString(
                            "Service Charge :".MyPadLeft(30) + serviceCharge.ToString("0.00").MyPadLeft(10),
                            PrintFontType.Contract));
                    }
                }

                decimal Total = 0;
                decimal Basic = 0;
                Basic = Convert.ToDecimal(roMaster["B_Amount"].ToString());
                Total = Convert.ToDecimal(roMaster["N_Amount"].ToString());

                if (SalesVatTermId != string.Empty)
                {
                    vatTerm = dtTerm
                        .Select(
                            $"SR_VNo = '{roMaster["SR_Invoice"]}' and Term_Type in ( 'P','BT') and ST_Id ='{SalesVatTermId}'")
                        .Aggregate(vatTerm,
                            (current, roTerm) => current + Convert.ToDecimal(roTerm["Amount"].ToString()));
                }

                strData.Append(GetPrintString(
                    "Taxable Value :".MyPadLeft(30) + (Total - vatTerm).ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                if (vatTerm > 0)
                {
                    strData.Append(GetPrintString("Vat @13%:".MyPadLeft(30) + vatTerm.ToString("0.00").MyPadLeft(10),
                        PrintFontType.Contract));
                }

                strData.Append(GetPrintString(
                    "Total :".MyPadLeft(30) + Total.ToString("0.00").MyPadLeft(10) + string.Empty,
                    PrintFontType.Contract));
                strData.Append(GetPrintString($"{"-".MyPadLeft(20, ' ')}--------------------{string.Empty}",
                    PrintFontType.Contract));
                var rupees = ClsMoneyConversion.MoneyConversion(roMaster["N_Amount"].GetDecimalString());
                var rupees1 = rupees.Length switch
                {
                    >= 28 => rupees.Substring(0, 28),
                    _ => rupees
                };

                var rupees2 = (rupees.Length - 28) switch
                {
                    >= 38 => rupees.Substring(28, 38),
                    _ => rupees.Remove(0, rupees1.Length)
                };

                var rupees3 = (rupees.Length - 66) switch
                {
                    >= 38 => rupees.Substring(66, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length)
                };

                var rupees4 = (rupees.Length - 104) switch
                {
                    >= 38 => rupees.Substring(104, 38),
                    _ => rupees.Remove(0, rupees1.Length + rupees2.Length + rupees3.Length)
                };
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Remarks : " + roMaster["Remarks"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                    PrintFontType.Contract));
                strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
                strData.Append(GetPrintString($"{string.Empty.PadLeft(5)}**** Thank You Visit Again ****",
                    PrintFontType.Contract));
                strData.Append("\n\n\n\n");
                result = SendStringToPrinter(Printer, strData.ToString());
            }

            strData.Clear();
            var GS = Convert.ToString((char)29);
            var ESC = Convert.ToString((char)27);
            var COMMAND = string.Empty;
            COMMAND = $"{ESC}@";
            COMMAND += $"{GS}V{(char)1}";
            SendStringToPrinter(Printer, COMMAND);
        }
    }



    //UPDATE PRINT BILL
    public static void UpdateOrderPrint(string invoiceNo)
    {
        var cmdString = $@" UPDATE AMS.SO_Details SET PrintKOT = 1,PrintedItem = 1,Print_Time = GETDATE() WHERE SO_Invoice = '{invoiceNo}' AND ISNULL(PrintKOT,0) = 0 ";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
    }

    public static void PrintUpdate(string invoiceNo)
    {
        CreateDatabaseTable.DropTrigger();
        var cmdString =
            $@" UPDATE AMS.SB_Master set IS_Printed = 1 , No_Print=isNUll(No_Print,0)+1,Printed_By='{ObjGlobal.LogInUser}' , Printed_Date= GetDate() where SB_Invoice='{invoiceNo}'; ";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        CreateDatabaseTable.CreateTrigger();
    }

    public static void PrintSalesReturnUpdate(string invoiceNo)
    {
        CreateDatabaseTable.DropTrigger();
        var cmdString =
            $@"UPDATE AMS.SR_Master set Is_Printed = 1, No_Print= isNUll(No_Print,0)+1, Printed_By='{ObjGlobal.LogInUser}',Printed_Date= GetDate() where SB_Invoice='{invoiceNo}'; ";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        CreateDatabaseTable.CreateTrigger();
    }

    // CANCEL INVOICE PRINT
    private void CancelInvoicePrint(string billno, string printer, int branch)
    {
        var strData = new StringBuilder();
        decimal BasicAmt = 0;
        var dsSales = _salesEntry.GetSalesInvoiceDetailsForPrint(BillNo);
        var dtMaster = dsSales.Tables[0];
        var dtDetail = dsSales.Tables[1];
        var roCom = _companyInfo.Rows[0];
        foreach (DataRow roMaster in dtMaster.Rows)
        {
            //Print Header Part from roMaster
            var city = roCom["City"].IsValueExits() ? $"{roCom["City"]}{','}{roCom["PhoneNo"]}" : $"{roCom["PhoneNo"]}";
            var pan = roCom["Pan_No"].IsValueExits() ? $"PAN NO : {roCom["Pan_No"]}" : "";
            strData.Append(GetPrintString(
                roCom["Company_Name"].ToString().MyPadLeft(roCom["Company_Name"].ToString().Length +
                                                           Convert.ToInt32(
                                                               (30 - roCom["Company_Name"].ToString().Length) / 2)),
                PrintFontType.Contract));
            if (roCom["Address"].IsValueExits())
            {
                strData.Append(GetPrintString(
                    roCom["Address"].ToString().MyPadLeft(roCom["Address"].ToString().Length +
                                                          Convert.ToInt32((30 - roCom["Address"].ToString().Length) /
                                                                          2)), PrintFontType.Contract));
            }

            if (city.IsValueExits())
            {
                strData.Append(GetPrintString(city.MyPadLeft(city.Length + ((30 - city.Length) / 2).GetInt()),
                    PrintFontType.Contract));
            }

            if (pan.IsValueExits())
            {
                strData.Append(GetPrintString(pan.MyPadLeft(pan.Length + ((30 - pan.Length) / 2).GetInt()),
                    PrintFontType.Contract));
            }

            strData.Append(GetPrintString($"{"".MyPadRight(10)} [CANCEL INVOICE]", PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString($"Purchaser Name: Cancel {string.Empty}", PrintFontType.Contract));
            strData.Append(GetPrintString(
                $"{("Invoice No: " + roMaster["SB_Invoice"]).MyPadRight(24)} [Table No] : {roMaster["TableName"]}",
                PrintFontType.Contract));
            strData.Append(GetPrintString(
                $"{("Order No: " + roMaster["SO_Invoice"]).MyPadRight(24)} Date: {roMaster["Invoice_Date"].GetDateString()}",
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(
                "SN.".MyPadRight(3) + "Particulars".MyPadRight(14) + "Qty".MyPadLeft(5) + "Rate".MyPadLeft(9) +
                "Amount".MyPadLeft(9) + string.Empty, PrintFontType.Contract));

            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));

            strData.Append(GetPrintString(
                "1".MyPadRight(3) + "Cancel Invoice".MyPadRight(14) + "".MyPadLeft(4) + "".MyPadLeft(10) +
                "".MyPadLeft(9) + string.Empty, PrintFontType.Contract));
            //Print Total
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString("TOTAL :".MyPadLeft(30) + 0.ToString("0.00").MyPadLeft(10) + string.Empty,
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(("Reason : " + roMaster["Cancel_Remarks"]).MyPadRight(25),
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append(GetPrintString(("Cashier : " + roMaster["Enter_By"]).MyPadRight(25),
                PrintFontType.Contract));
            strData.Append(GetPrintString("-".MyPadLeft(40, '-') + string.Empty, PrintFontType.Contract));
            strData.Append("\n\n\n\n\n\n\n");
        }

        var result = SendStringToPrinter(Printer, strData.ToString());
        if (result)
        {
            PrintUpdate(BillNo);
        }

        strData.Clear();
        var GS = Convert.ToString((char)29);
        var ESC = Convert.ToString((char)27);
        var COMMAND = string.Empty;
        COMMAND = $"{ESC}@";
        COMMAND += $"{GS}V{(char)1}";
        SendStringToPrinter(Printer, COMMAND);
    }

    #region --------------- Global ---------------

    public string Printer { get; set; }
    public string BillNo { get; set; }
    public string Printdesign { get; set; }
    public string PrintedBy { get; set; }
    public string PDiscountId { get; set; } = ObjGlobal.SalesDiscountTermId.GetIntString();
    public string BDiscountId { get; set; } = ObjGlobal.SalesSpecialDiscountTermId.GetIntString();
    public string ServiceChargeId { get; set; } = ObjGlobal.SalesServiceChargeTermId.GetIntString();
    public string SalesVatTermId { get; set; } = ObjGlobal.SalesVatTermId.GetIntString();
    public string DiscountAmount { get; set; }
    public short NoofPrint { get; set; } = 1;
    public bool IsServiceApplicable { get; set; }

    private readonly IFinanceEntry _entry;
    private readonly ISalesEntry _salesEntry;

    private Font _myFont;
    private DataTable _companyInfo;
    private System.Drawing.Printing.PrintDocument Document;
    private readonly RawPrinter _printer;
    #endregion --------------- Global ---------------
}