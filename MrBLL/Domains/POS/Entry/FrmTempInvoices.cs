using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Billing;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry;

public partial class FrmTempInvoices : MrForm
{
    private readonly SalesInvoiceService _salesInvoiceService;
    private bool _accepted;
    private string voucherNo = string.Empty;
    private Temp_SB_Master SbMaster;
    public FrmTempInvoices()
    {
        InitializeComponent();
        SbMaster = new Temp_SB_Master();
        _salesInvoiceService = new SalesInvoiceService();
        lblInvoiceNo.Text = lblInvoiceDateTime.Text = lblInvoiceEnteredBy.Text = null;
    }

    private async Task RefreshAllAsync()
    {
        var response = await _salesInvoiceService.GetHoldInvoicesAsync(ObjGlobal.SysBranchId);
        if (!response.Success)
        {
            response.ShowErrorDialog("UNABLE TO FETCH HOLD INVOICES..!!");
            return;
        }
        var list = response.List.OrderByDescending(x => x.Enter_Date).ToList();
        foreach (var master in list)
        {

        }
        DGridMaster.DataSource = list;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnAccept_Click(object sender, EventArgs e)
    {
        _accepted = true;
        Close();

    }

    private async void FrmTempInvoices_Load(object sender, EventArgs e)
    {
        await RefreshAllAsync();
    }
    public static (bool Accepted, string voucherNo) SelectTempInvoice()
    {
        var form = new FrmTempInvoices();
        form.ShowDialog();
        return (form._accepted, form.SbMaster.SB_Invoice);
    }

    private async void DGridMaster_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (DGridMaster.CurrentRow == null)
        {
            return;
        }

        bsInvoiceItems.DataSource = null;
        lblInvoiceNo.Text = lblInvoiceDateTime.Text = lblInvoiceEnteredBy.Text = null;
        SbMaster.SB_Invoice = DGridMaster.CurrentRow.Cells["GVoucherNo"].Value.ToString();
        SbMaster.Enter_By = DGridMaster.CurrentRow.Cells["GEnterUser"].ToString();

        var response = await _salesInvoiceService.GetTempInvoice(SbMaster.SB_Invoice);
        if (!response.Success)
        {
            response.ShowErrorDialog("UNABLE TO FETCH SELECTED RECORD DETAIL..!!");
            return;
        }

        lblInvoiceNo.Text = SbMaster.SB_Invoice;
        lblInvoiceDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
        lblInvoiceEnteredBy.Text = SbMaster.Enter_By;
        var items = await _salesInvoiceService.GetHoldInvoicesDetailsAsync(SbMaster.SB_Invoice);
        bsInvoiceItems.DataSource = items;
    }
}