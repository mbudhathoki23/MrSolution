using DevExpress.XtraSplashScreen;
using MoreLinq;
using MrBLL.Utility.SplashScreen;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Reports.CrystalReports.Interface;
using MrDAL.Reports.CrystalReports.Module;
using MrDAL.Reports.CrystalReports.RawQuery;
using PrintControl.PrintDesign.PartyConfirmation;
using System;
using System.Windows.Forms;

namespace MrBLL.Reports.PartyConfirmation;

public partial class PrintPartyConfirmationPrint : Form
{
    // PRINT PARTY CONFIRMATION PRINT
    #region --------------- PRINT PARTY CONFIRMATION PRINT ---------------
    public PrintPartyConfirmationPrint()
    {
        InitializeComponent();
        _rawQuery = new GetQueryReport();
    }
    private void FrmPartyConfirmationPrint_Load(object sender, EventArgs e)
    {

    }
    private void DisplayPartyConfirmationPrint_Shown(object sender, EventArgs e)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(FrmWait));

            var cmdString = LedgerType switch
            {
                "VENDOR" => _rawQuery.GetVendorLedgerBalanceConfirmation(LedgerId, FiscalYearId, AboveAmount),
                _ => _rawQuery.GetCustomerLedgerBalanceConfirmation(LedgerId, FiscalYearId, AboveAmount)
            };
                
            var fiscalYear = _rawQuery.GetFiscalYear(FiscalYearId);
            var listResult = QueryUtils.GetList<PartyLedgerConfirmation>(cmdString);

            if (listResult.List.Count > 0)
            {
                reportDocument1 = EnglishLetter ? new BalanceConfirmationEnglish() : new BalanceConfirmation();
                reportDocument1.SetDataSource(listResult.List.ToDataTable());
                reportDocument1.SetParameterValue("fiscalYear", fiscalYear);
                if (EnglishLetter)
                {
                    reportDocument1.SetParameterValue("invoiceType", LedgerType.Equals("VENDOR") ? "Purchase" : "Sales");
                }
                else
                {
                    reportDocument1.SetParameterValue("invoiceType", LedgerType.Equals("VENDOR") ? "खरिद" : "बिक्री");
                }

                reportDocument1.SetParameterValue("tranType", LedgerType.Equals("VENDOR") ? "Total Purchase" : "Total Sales");
                reportDocument1.SetParameterValue("printHeader", PrintHeader);
                reportDocument1.SetParameterValue("companyName", ObjGlobal.CompanyPrintDesc);
                reportDocument1.SetParameterValue("companyAddress", ObjGlobal.CompanyAddress);
                reportDocument1.SetParameterValue("PanNo", ObjGlobal.CompanyPanNo);
                reportDocument1.SetParameterValue("emailAddress", ObjGlobal.CompanyEmailAddress);
                reportDocument1.SetParameterValue("contactNumber", ObjGlobal.CompanyPhoneNo);
                crystalReportViewer1.ReportSource = reportDocument1;
                crystalReportViewer1.Refresh();
            }
            else
            {
                CustomMessageBox.Information("SELECTED LEDGER RECORDS NOT FOUNDS..!!");
            }
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception ex)
        {
            SplashScreenManager.CloseForm(false);
            ex.DialogResult();
        }
    }
    private void PrintPartyConfirmationPrint_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Escape)
        {
            if (CustomMessageBox.ExitActiveForm() == DialogResult.Yes)
            {
                Close();
            }
        }
        if (e.Control && e.KeyCode is Keys.P)
        {
            crystalReportViewer1.PrintReport();
        }
    }
    #endregion

    // OBJECT FOR THIS CLASS
    #region ---------- OBJECT ----------
    public int FiscalYearId;

    public bool IncludeVat;
    public bool IncludeReturn;
    public bool EnglishLetter;
    public bool PrintHeader;

    public string LedgerType;
    public string LedgerId;
        
    public decimal AboveAmount;
    private IQueryReport _rawQuery;
    #endregion ---------- OBJECT ----------
}