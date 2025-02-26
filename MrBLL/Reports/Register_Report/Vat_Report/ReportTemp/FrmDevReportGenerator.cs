using DevExpress.XtraBars;
using MrBLL.Domains.DynamicReport;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Reports.CrystalReports.Interface;
using MrDAL.Reports.CrystalReports.RawQuery;
using MrDAL.Reports.Register;
using MrDAL.Reports.ViewModule.Object.Register;
using System.Linq;

namespace MrBLL.Reports.Register_Report.Vat_Report.ReportTemp;

public partial class FrmDevReportGenerator : MrForm
{
    // DEV REPORT PRINT FORM

    #region --------------- Method to Print ---------------

    public FrmDevReportGenerator(string reportSource)
    {
        InitializeComponent();
        _reportSource = reportSource;
        _queryReport = new GetQueryReport();
    }

    private void BbiLoadReport_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (_reportSource.Equals("PARTY CONFIRMATION"))
        {
            GetPartyConfirmationPrintDesign();
        }
        else if (_reportSource.Equals("SB"))
        {
            GetSalesVatRegisterPrintDesign();
        }
    }

    private void FrmRegisterReport_Load(object sender, System.EventArgs e)
    {
        BbiLoadReport_ItemClick(sender, null);
    }

    #endregion --------------- Method to Print ---------------

    // METHOD FOR THIS FORM

    #region --------------- Method to Print ---------------

    private async void GetPartyConfirmationPrintDesign()
    {
        var cmdString = LedgerType switch
        {
            "VENDOR" => _queryReport.GetVendorLedgerBalanceConfirmation(LedgerId, FiscalYearId, AboveAmount),
            _ => _queryReport.GetCustomerLedgerBalanceConfirmation(LedgerId, FiscalYearId, AboveAmount)
        };

        var dsReport = await QueryUtils.GetListAsync<SalesVatRegisterDetailsDateWise>(cmdString);
        if (!dsReport.Success)
        {
            this.NotifyError(dsReport.ErrorMessage);
            return;
        }
        //reportViewer.DocumentSource = new RptPartyConfirmationPrint
        //{
        //    DataSource = dsReport.List.ToList()
        //};
        reportViewer.InitiateDocumentCreation();
    }

    private async void GetSalesVatRegisterPrintDesign()
    {
        var result = new FrmFilter("IRD REGISTER", "R");
        result.ShowDialog();
        reportViewer.DocumentSource = null;
        _report.GetReports.VatTermId = _reportSource is "SB" ?
            [
                ObjGlobal.SalesVatTermId.ToString(), 0.ToString()
            ]
            : [0.ToString(), ObjGlobal.PurchaseVatTermId.ToString()];
        _report.GetReports.FromAdDate = result.MskFrom.Text.GetEnglishDate();
        _report.GetReports.ToAdDate = result.MskToDate.Text.GetEnglishDate();

        var cmdString = _report.GetNewSalesVatRegisterDetailsReportsScript("SB");
        var dsReport = await QueryUtils.GetListAsync<SalesVatRegisterDetailsDateWise>(cmdString);

        if (!dsReport.Success)
        {
            this.NotifyError(dsReport.ErrorMessage);
            return;
        }
        reportViewer.DocumentSource = new RptSalesVatRegisterDetailsDateWise
        {
            DataSource = dsReport.List.ToList()
        };
        reportViewer.InitiateDocumentCreation();
    }

    #endregion --------------- Method to Print ---------------

    // OBJECT FOR THIS FORM

    #region --------------- OBJECT  ---------------

    private string _reportSource;
    private ClsRegisterReport _report = new();
    public string[] VatTermId;
    public string FromDate;
    public string ToDate;
    private IQueryReport _queryReport;
    public string LedgerId { get; set; }
    public int FiscalYearId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string LedgerType { get; set; }
    public decimal AboveAmount { get; set; }

    #endregion --------------- OBJECT  ---------------
}