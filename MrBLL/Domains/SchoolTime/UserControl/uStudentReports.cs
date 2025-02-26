using DevExpress.XtraEditors;
using MrDAL.Global.Common;
using MrDAL.Reports.Register;
using System;
using System.Data;
using System.Windows.Forms;

namespace MrBLL.Domains.SchoolTime.UserControl;

public partial class uStudentReports : XtraUserControl
{
    private readonly string _ReportDesc;
    private DataTable _rTable = new("REPORT");
    private readonly ClsRegisterReport ObjRegister = new();

    public uStudentReports(string ReportDesc)
    {
        InitializeComponent();
        _ReportDesc = ReportDesc;
    }

    private void uReports_Load(object sender, EventArgs e)
    {
        BindSelectedReport(_ReportDesc);
    }

    private void BindSelectedReport(string Search)
    {
        if (string.IsNullOrEmpty(Search))
        {
            MessageBox.Show(@"DATA NOT FOUND..!!", ObjGlobal.Caption);
        }
        else if (!string.IsNullOrEmpty(Search))
        {
            ObjRegister.GetReports.FromAdDate = Convert.ToDateTime(ObjGlobal.CfStartAdDate).ToString("yyyy-MM-dd");
            ObjRegister.GetReports.ToAdDate = Convert.ToDateTime(ObjGlobal.CfEndAdDate).ToString("yyyy-MM-dd");
            _rTable = ObjRegister.GenerateSalesInvoiceRegisterSummaryReports();
            if (_rTable.Rows.Count == 0) return;
            RGrid.DataSource = _rTable;
        }
    }

    private void uReports_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Escape)
            if (MessageBox.Show(@"Do you Want to Closed..??", ObjGlobal.Caption, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                Dispose();
    }
}