using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DevExpress.XtraGrid.Views.Base;
using MoreLinq;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Domains.Billing;
using MrDAL.Global.Dialogs;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry;

public partial class FrmSalesInvoices : MrForm
{
    private readonly int _branchId;
    private readonly int? _counterId;
    private readonly SalesInvoiceActionTag? _initActionTag;
    private readonly SalesInvoiceService _invoiceService;
    private bool _accepted;
    private IList<SalesInvoiceProductModel> _barcodeWiseProducts;
    private SB_Master _selectedModel;

    public FrmSalesInvoices(SalesInvoiceActionTag? initActionTag, int? counterId, int branchId)
    {
        InitializeComponent();

        _initActionTag = initActionTag;
        _counterId = counterId;
        _branchId = branchId;
        gvInvoices.OptionsBehavior.ReadOnly = gvInvoiceItems.OptionsBehavior.ReadOnly = true;
        lblInvoiceDateTime.Text = lblInvoiceEnteredBy.Text = lblInvoiceNo.Text = null;

        _invoiceService = new SalesInvoiceService();
        _barcodeWiseProducts = new List<SalesInvoiceProductModel>();
    }

    private async Task RefreshAllAsync()
    {
        bsInvoices.DataSource = null;
        this.NotifyHint("Fetching data...");

        var response = await _invoiceService.GetInvoicesAsync(null, null, _counterId,
            _branchId, _initActionTag);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return;
        }

        bsInvoices.DataSource = _initActionTag == SalesInvoiceActionTag.New
            ? response.List.OrderByDescending(x => x.Enter_Date).ToList()
            : response.List;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnAccept_Click(object sender, EventArgs e)
    {
        if (gvInvoices.GetFocusedRow() is not SB_Master model)
        {
            this.NotifyError("No invoice record selected.");
            return;
        }

        _accepted = true;
        _selectedModel = model;
        Close();
    }

    private async void FrmSalesInvoices_Load(object sender, EventArgs e)
    {
        var productsResponse = await _invoiceService.GetProductsAsync(true);
        if (!productsResponse.Success)
        {
            productsResponse.ShowErrorDialog("Unable to fetch products list.");
            return;
        }

        _barcodeWiseProducts = productsResponse.List.ToList().DistinctBy(x => x.ProductId).ToList();
        await RefreshAllAsync();
    }

    private void gcInvoices_DoubleClick(object sender, EventArgs e)
    {
        if (gvInvoices.GetFocusedRow() is not SB_Master model) return;

        _accepted = true;
        _selectedModel = model;
        Close();
    }

    private void gcInvoices_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Enter && gvInvoices.GetFocusedRow() is SB_Master model)
        {
            e.Handled = true;
            _selectedModel = model;
            _accepted = true;
            Close();
        }
    }

    private async void gvInvoices_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
    {
        bsInvoiceItems.DataSource = null;
        lblInvoiceNo.Text = lblInvoiceDateTime.Text = lblInvoiceEnteredBy.Text = null;
        if (gvInvoices.GetFocusedRow() is not SB_Master model) return;

        var response = await _invoiceService.GetInvoiceAsync(model.SB_Invoice, model.Action_Type);
        if (!response.Success)
        {
            response.ShowErrorDialog("Unable to fetch selected record detail.");
            return;
        }

        lblInvoiceNo.Text = model.SB_Invoice;
        lblInvoiceDateTime.Text = model.Invoice_Time.Date.ToString("dd/MM/yyyy HH:mm tt");
        lblInvoiceEnteredBy.Text = model.Enter_By;

        var items = from item in response.Model.Items
            join bc in _barcodeWiseProducts on item.P_Id equals bc.ProductId
            where item.Unit_Id == bc.UnitId
            select new
            {
                bc.ProductName,
                bc.UnitCode,
                item.Rate,
                item.Qty,
                Total = item.B_Amount,
                NetTotal = item.N_Amount
            };
        bsInvoiceItems.DataSource = items.ToList();
    }

    public static (bool Accepted, SB_Master Model) SelectInvoice(SalesInvoiceActionTag initActionTag,
        int? counterId, int branchId)
    {
        var form = new FrmSalesInvoices(initActionTag, counterId, branchId);
        form.ShowDialog();
        return (form._accepted, form._selectedModel);
    }
}