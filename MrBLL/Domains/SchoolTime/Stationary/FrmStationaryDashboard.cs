using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.Domains.POS.Entry;
using MrDAL.Master;
using MrDAL.Master.Interface;
using System;

namespace MrBLL.Domains.SchoolTime.Stationary;

public partial class FrmStationaryDashboard : RibbonForm
{
    private readonly IMasterSetup _objSetup = new ClsMasterSetup();

    public FrmStationaryDashboard()
    {
        InitializeComponent();
    }

    private void bbiPrintPreview_ItemClick(object sender, ItemClickEventArgs e)
    {
        gridControl.ShowRibbonPrintPreview();
    }

    private void bbiNew_ItemClick(object sender, ItemClickEventArgs e)
    {
        new FrmBook("SAVE").ShowDialog();
    }

    private void FrmBookSetup_Load(object sender, EventArgs e)
    {
        BindBookData();
    }

    private void bbiEdit_ItemClick(object sender, ItemClickEventArgs e)
    {
        var result = new FrmBook("UPDATE").ShowDialog();
    }

    private void bbiDelete_ItemClick(object sender, ItemClickEventArgs e)
    {
        var result = new FrmBook("DELETE").ShowDialog();
    }

    private void bBtnPurchase_ItemClick(object sender, ItemClickEventArgs e)
    {
        new FrmPurchaseInvoiceEntry(false, "", false, "STATIONARY").ShowDialog(this);
    }

    private void bBtnSales_ItemClick(object sender, ItemClickEventArgs e)
    {
        new FrmPSalesInvoice().ShowDialog(this);
    }

    private void BindBookData()
    {
        var dtBook = _objSetup.GetBookInformation(0);
        gridControl.DataSource = dtBook;
    }

    private void bbiRefresh_ItemClick(object sender, ItemClickEventArgs e)
    {
        BindBookData();
    }
}