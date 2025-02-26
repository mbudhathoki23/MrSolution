using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmBarcodeProductSelection : MrForm
{
    private bool _accepted;
    private SalesInvoiceProductModel _selectedModel;

    public FrmBarcodeProductSelection(IList<SalesInvoiceProductModel> products)
    {
        InitializeComponent();
        bsProducts.DataSource = products;
        ActiveControl = gridProducts;
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnAccept_Click(object sender, EventArgs e)
    {
        if (gridProducts.SelectedRows.Count == 0 ||
            !(gridProducts.SelectedRows[0].DataBoundItem is SalesInvoiceProductModel model))
        {
            this.NotifyError("No product selected");
            return;
        }

        _accepted = true;
        _selectedModel = model;
        Close();
    }

    private void gridProducts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (gridProducts.SelectedRows.Count == 0 ||
            !(gridProducts.SelectedRows[0].DataBoundItem is SalesInvoiceProductModel model))
            return;

        _accepted = true;
        _selectedModel = model;
        Close();
    }

    private void gridProducts_EnterKeyPressed(object sender, EventArgs e)
    {
        if (gridProducts.SelectedRows.Count == 1) BtnAccept.PerformClick();
    }

    public static (bool Accepted, SalesInvoiceProductModel Model) SelectProduct(
        IList<SalesInvoiceProductModel> products)
    {
        var form = new FrmBarcodeProductSelection(products);
        form.ShowDialog();
        return (form._accepted, form._selectedModel);
    }
}