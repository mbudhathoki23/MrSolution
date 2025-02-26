using MrBLL.Domains.DynamicReport;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Reports.CrystalReports.Interface;
using MrDAL.Reports.CrystalReports.RawQuery;
using MrDAL.Reports.Register;
using MrDAL.Reports.ViewModule.Object.Register;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Utility.CrystalReports;

public partial class FrmCrystalReportViewer : Form
{
    public FrmCrystalReportViewer(string reportModule)
    {
        InitializeComponent();
        _registerReport = new ClsRegisterReport();
        _reportModule = reportModule;
        _report = new GetQueryReport();
    }

    private void FrmCrystalReportViewer_Load(object sender, EventArgs e)
    {
        if (!_reportModule.IsValueExits())
        {
            return;
        }

        var result = new FrmFilter("IRD REGISTER", "R");
        result.ShowDialog();
        _registerReport.GetReports.VatTermId = [ObjGlobal.SalesVatTermId.ToString(), 0.ToString()];
        _registerReport.GetReports.FromAdDate = result.MskFrom.Text.GetEnglishDate();
        _registerReport.GetReports.ToAdDate = result.MskToDate.Text.GetEnglishDate();

        var cmdString = _registerReport.GetNewSalesVatRegisterDetailsReportsScript("SB");
        var listResult = QueryUtils.GetList<SalesVatRegisterDetailsDateWise>(cmdString);
        if (listResult.List == null)
        {
            return;
        }
        var report = listResult.List.ToDataTable();
        if (listResult.GetHashCode() != 0)
        {
            _dsReport.Tables.Add(report);
        }
        _document.SetDataSource(_dsReport);
        PrintViewer.ReportSource = _document;
    }

    // OBJECT

    #region ---------- OBJECT FOR THIS FORM ----------

    private readonly string _reportModule;
    private readonly DataSet _dsReport = new();
    private IQueryReport _report;
    private readonly ClsRegisterReport _registerReport;
    public int FiscalYearId;
    public string LedgerId;
    public string LedgerType;

    #endregion ---------- OBJECT FOR THIS FORM ----------
}