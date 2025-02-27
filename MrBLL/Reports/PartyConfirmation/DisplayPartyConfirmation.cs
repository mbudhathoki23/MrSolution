﻿using DevExpress.XtraSplashScreen;
using MrBLL.Utility.Common;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.SplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Reports.CrystalReports.Interface;
using MrDAL.Reports.CrystalReports.RawQuery;
using MrDAL.Reports.Design;
using MrDAL.Reports.Interface;
using MrDAL.Utility.GridControl;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using PrintControl.PrintClass.Domains;

namespace MrBLL.Reports.PartyConfirmation;

public partial class DisplayPartyConfirmation : Form
{
    public DisplayPartyConfirmation()
    {
        InitializeComponent();
        _design = new FinanceReportDesign();
        _rawQuery = new GetQueryReport();
    }

    private void DisplayPartyConfirmation_Load(object sender, EventArgs e)
    {
        HeaderValue();
        GeneratePartyConfirmation();
        RGrid.Focus();
    }

    private void DisplayPartyConfirmation_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
    }

    private void RGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        var result = RGrid.CurrentRow?.Cells["GTxtLedgerId"].Value.GetLong();
        if (!(result > 0))
        {
            return;
        }

        var print = new PrintPartyConfirmationPrint()
        {
            LedgerId = result.ToString(),
            LedgerType = LedgerType,
            EnglishLetter = EnglishLetter,
            FiscalYearId = FiscalYearId,
            PrintHeader = PrintHeader
        };

        print.ShowDialog();
    }

    private void tsBtnPrintReport_Click(object sender, EventArgs e)
    {
        CustomPrintFunction();
    }

    private void tsBtnExportReport_Click(object sender, EventArgs e)
    {
        ExportReport();
    }

    private void tsBtnSearchReport_Click(object sender, EventArgs e)
    {
        RGrid.ClearSelection();
        var rptSearch = new FrmRptSearch(RGrid);
        if (rptSearch.ShowDialog() != DialogResult.OK)
        {
        }
    }

    private void tsBtnRefreshReport_Click(object sender, EventArgs e)
    {
        GeneratePartyConfirmation();
    }

    private void tsBtnAutoColumnSizeReport_Click(object sender, EventArgs e)
    {
        FitGridColumn();
    }

    private void PrintDocument1_BeginPrint(object sender, System.ComponentModel.CancelEventArgs e)
    {
        _control.PrintDocument1_BeginPrint(sender, e);
    }

    private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        _control.PrintDocument1_PrintPage(sender, e);
    }

    // PARTY CONFIRMATION

    #region ** --------------- PARTY CONFIRMATION --------------- **

    private void HeaderValue()
    {
        LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartBsDate} to {ObjGlobal.CfEndBsDate}";
        lbl_DateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:MM");
        lbl_ComanyName.Text = ObjGlobal.CompanyPrintDesc;
        LblCompanyAddress.Text = ObjGlobal.CompanyAddress;
        LblCompanyPANVATNo.Text = $@"PAN/VATNo : {ObjGlobal.CompanyPanNo}";

        if (ObjGlobal.SysDateType is "M")
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartBsDate} - {ObjGlobal.CfEndBsDate}";
            lbl_DateTime.Text = $@"{DateTime.Now.GetNepaliDate()}";
        }
        else
        {
            LblAccPeriodDate.Text = $@"{ObjGlobal.CfStartAdDate.GetDateString()} - {ObjGlobal.CfEndAdDate.GetDateString()}";
            lbl_DateTime.Text = DateTime.Now.GetDateString();
        }

        lbl_PageNo.Text = @"Page 1 of 1";

        LblReportName.Text = @$"{LedgerType} WISE PARTY CONFIRMATION REPORT";
        LblReportDate.Text = ObjGlobal.SysDateType is "M" ? $@"FROM {ObjGlobal.CfStartBsDate} TO {ObjGlobal.CfEndBsDate}" : $@"FROM {ObjGlobal.CfStartAdDate} TO {ObjGlobal.CfEndAdDate}";
    }

    private bool GeneratePartyConfirmation()
    {
        RGrid.DataSource = null;
        _design.GetPartyConfirmationDesign(RGrid, IncludeReturn);
        if (RGrid == null)
        {
            return false;
        }
        var cmdString = LedgerType switch
        {
            "VENDOR" => _rawQuery.GetVendorLedgerBalanceConfirmation(LedgerId, FiscalYearId, AboveAmount),
            _ => _rawQuery.GetCustomerLedgerBalanceConfirmation(LedgerId, FiscalYearId, AboveAmount)
        };
        var dtLedger = cmdString.GetQueryDataTable();
        if (dtLedger.Rows.Count > 0)
        {
            var dataRow = dtLedger.NewRow();
            var totalOpening = dtLedger.AsEnumerable().Sum(x => x["OpeningBalance"].GetDecimal());
            var totalNetAmount = dtLedger.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
            var totalTax = dtLedger.AsEnumerable().Sum(x => x["TaxAmount"].GetDecimal());
            var totalTaxable = dtLedger.AsEnumerable().Sum(x => x["TaxableAmount"].GetDecimal());
            var totalTaxExempted = dtLedger.AsEnumerable().Sum(x => x["TaxExempted"].GetDecimal());
            var totalReturnAmount = dtLedger.AsEnumerable().Sum(x => x["NetReturnAmount"].GetDecimal());
            var totalReturnTax = dtLedger.AsEnumerable().Sum(x => x["TaxReturnAmount"].GetDecimal());
            var totalTaxableReturn = dtLedger.AsEnumerable().Sum(x => x["TaxableReturn"].GetDecimal());
            var totalTaxReturnExempted = dtLedger.AsEnumerable().Sum(x => x["TaxReturnExempted"].GetDecimal());
            var totalClosing = dtLedger.AsEnumerable().Sum(x => x["ClosingBalance"].GetDecimal());

            dataRow["LedgerName"] = "TOTAL AMOUNT >>";
            dataRow["OpeningBalance"] = totalOpening.GetDecimalComma();
            dataRow["NetAmount"] = totalNetAmount.GetDecimalComma();
            dataRow["TaxAmount"] = totalTax.GetDecimalComma();
            dataRow["TaxableAmount"] = totalTaxable.GetDecimalComma();
            dataRow["TaxExempted"] = totalTaxExempted.GetDecimalComma();
            dataRow["NetReturnAmount"] = totalReturnAmount.GetDecimalComma();
            dataRow["TaxReturnAmount"] = totalReturnTax.GetDecimalComma();
            dataRow["TaxableReturn"] = totalTaxableReturn.GetDecimalComma();
            dataRow["TaxReturnExempted"] = totalTaxReturnExempted.GetDecimalComma();
            dataRow["ClosingBalance"] = totalClosing.GetDecimalComma();
            dataRow["IsGroup"] = 99;
            dtLedger.Rows.InsertAt(dataRow, dtLedger.Rows.Count);

            RGrid.DataSource = dtLedger;
        }
        else
        {
            MessageBox.Show(@$"RECORD NOT FOUND FOR PARTY CONFIRMATION..!!", ObjGlobal.Caption);
            Close();
        }

        return RGrid.RowCount > 0;
    }

    private void CustomPrintFunction()
    {
        _control.RGrid = RGrid;
        _control.AccPeriodDate = LblAccPeriodDate.Text;
        _control.CompanyAddress = LblCompanyAddress.Text;
        _control.CompanyPanNo = LblCompanyPANVATNo.Text;
        _control.ReportDate = LblReportDate.Text;
        _control.ReportName = LblReportName.Text;
        var printDialog = new PrintDialog
        {
            Document = printDocument1,
            UseEXDialog = true
        };
        if (DialogResult.OK == printDialog.ShowDialog())
        {
            printDocument1.DocumentName = LblReportName.Text;
            printDocument1.DefaultPageSettings.Margins = new Margins(20, 20, 50, 20);
            printDocument1.Print();
            var result = printDialog.AllowPrintToFile;
            if (result.GetHashCode() > 0)
            {
                MessageBox.Show($@"{LblReportName.Text} PRINT SUCCESSFULLY..!!", ObjGlobal.Caption);
            }
        }
    }

    private void FitGridColumn()
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(PleaseWait));
            var column = RGrid.Columns.Cast<DataGridViewColumn>().ToList();
            column.ForEach(f => f.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells);
            SplashScreenManager.CloseForm(true);
        }
        catch
        {
            SplashScreenManager.CloseForm(true);
        }
    }

    private void ExportReport()
    {
        saveFileDialog1.Title = @"Save";
        saveFileDialog1.Filter = @"Excel File 1997-07 |*.Xls|Excel File 2010-13 |*.xlsx|Word |*.Doc|Html Page |*.Html";
        saveFileDialog1.ShowDialog();
        var fileName = saveFileDialog1.FileName;
        var reports = new ClsExportReports
        {
            FileName = fileName,
            CompanyName = lbl_ComanyName.Text,
            AccountingPeriod = lbl_AccountingPeriod.Text,
            AccPeriodDate = LblAccPeriodDate.Text,
            CompanyAddress = LblCompanyAddress.Text,
            CompanyPanVatNo = LblCompanyPANVATNo.Text,
            ReportName = LblReportName.Text,
            ReportDate = LblReportDate.Text
        };
        reports.ExportReport(RGrid);
        this.NotifySuccess($"{LblReportName.Text} EXPORT SUCCESSFULLY..!!");
        if (CustomMessageBox.Question(@"DO YOU WANT TO OPEN THE EXPORT FILE..??") is DialogResult.Yes)
        {
            Process.Start(fileName);
        }
    }

    #endregion ** --------------- PARTY CONFIRMATION --------------- **

    //  OBJECT FOR THIS FORM

    #region ** --------------- PARTY CONFIRMATION --------------- **

    public int FiscalYearId;

    public bool IncludeVat;
    public bool IncludeReturn;
    public bool EnglishLetter;
    public bool PrintHeader;

    public string LedgerType;
    public string LedgerId;

    public decimal AboveAmount;

    private readonly IFinanceDesign _design;
    private readonly IQueryReport _rawQuery;
    private readonly ClsPrintControl _control = new();

    #endregion ** --------------- PARTY CONFIRMATION --------------- **
}