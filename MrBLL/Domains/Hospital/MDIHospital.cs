using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using MrBLL.DataEntry.FinanceMaster;
using MrBLL.Domains.Hospital.Entry;
using MrBLL.Utility.Common.Class;
using MrDAL.Core.Extensions;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Reports.Interface;
using MrDAL.Reports.Register;
using System;

namespace MrBLL.Domains.Hospital;

public partial class MdiHospital : RibbonForm
{
    public MdiHospital()
    {
        InitializeComponent();
        Shown += (sender, e) =>
        {
            BindPatientDesc();
        };
    }

    private void MnuDoctorType_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateDoctorType();
    }

    private void MnuDoctorSetup_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateDoctor();
    }

    private void MnuDepartment_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateHospitalDepartment();
    }

    private void MnuWardMaster_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateWard();
    }

    private void MnuBedType_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateBedType();
    }

    private void MnuBedNumber_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateBedNumber();
    }

    private void MnuBedMaster_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateDoctor();
    }

    private void MnuCategory_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateProductGroup();
    }

    private void MnuSubCategory_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateProductSubGroup();
    }

    private void MnuItemMaster_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreateHospitalDepartment();
    }

    private void MnuPatientRegistration_ItemClick(object sender, ItemClickEventArgs e)
    {
        GetMasterList.CreatePatient();
    }

    private void MnuOPDBilling_ItemClick(object sender, ItemClickEventArgs e)
    {
        var frm = new FrmHSalesInvoice();
        frm.ShowDialog();
    }

    private void MnuIPDBilling_ItemClick(object sender, ItemClickEventArgs e)
    {
        var frm = new FrmHSalesInvoice(true);
        frm.ShowDialog();
    }

    private void MnuCashReceipt_ItemClick(object sender, ItemClickEventArgs e)
    {
        var frm = new FrmCashBankEntry();
        frm.ShowDialog();
    }

    private void MnuCashRefund_ItemClick(object sender, ItemClickEventArgs e)
    {
        var frm = new FrmHSalesInvoice(true, "RETURN");
        frm.ShowDialog();
    }

    private void MnuSalesRegister_ItemClick(object sender, ItemClickEventArgs e)
    {
        DGridControl.DataSource = null;
        _report.GetReports.RptMode = "DATE WISE";
        _report.GetReports.FromAdDate = DateTime.Now.GetSystemDate();
        _report.GetReports.ToAdDate = DateTime.Now.GetSystemDate();
        _report.GetReports.Module = "SB";
        var dt = _report.GenerateSalesInvoiceRegisterSummaryReports();
        DGridControl.DataSource = dt;
    }

    // METHOD FOR THIS FORM
    private void BindPatientDesc()
    {
        var dt = _master.GetPatientListOnDashboard();
        DGridControl.DataSource = dt;
    }

    // OBJECT FOR THIS FORM
    private readonly IHMaster _master = new ClsHMaster();

    private readonly IRegisterReport _report = new ClsRegisterReport();
}