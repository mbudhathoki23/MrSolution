using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.WinControl;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Master;

public partial class FrmInvoiceItemSelection : MrForm
{
    public enum InvoiceDupItemSelectKind
    {
        Append,
        UpdateSelected
    }

    private bool _accepted;
    private ValueModel<InvoiceDupItemSelectKind, SalesInvoiceItemModel> _selectionResult;

    public FrmInvoiceItemSelection(string product, string unit, decimal qty,
        IList<SalesInvoiceItemModel> invoiceItems)
    {
        InitializeComponent();
        bsInvoiceItems.DataSource = invoiceItems;
        lblProductInfo.Text = $@"{qty:N2} {unit} {product}";
    }

    private void BtnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void BtnAccept_Click(object sender, EventArgs e)
    {
        if (rdoAppend.Checked)
        {
            _accepted = true;
            _selectionResult =
                new ValueModel<InvoiceDupItemSelectKind, SalesInvoiceItemModel>(InvoiceDupItemSelectKind.Append,
                    null);
            Close();
            return;
        }

        if (DGrid.SelectedRows.Count == 0 && rdoModifyExisting.Checked)
        {
            this.NotifyError("No record selected.");
            return;
        }

        _accepted = true;
        _selectionResult = new ValueModel<InvoiceDupItemSelectKind, SalesInvoiceItemModel>
        {
            Item1 = InvoiceDupItemSelectKind.UpdateSelected,
            Item2 = (SalesInvoiceItemModel)DGrid.SelectedRows[0].DataBoundItem
        };
        Close();
    }

    private void rdoModifyExisting_Paint(object sender, PaintEventArgs e)
    {
        ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle, Color.Gray, ButtonBorderStyle.Solid);
    }

    private void OnRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        DGrid.Visible = rdoModifyExisting.Checked;
    }

    private void DGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (DGrid.SelectedRows.Count == 0 ||
            DGrid.SelectedRows[0].DataBoundItem is not SalesInvoiceItemModel) return;
        BtnAccept.PerformClick();
    }

    private void DGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        if (DGrid.SelectedRows.Count == 0 ||
            DGrid.SelectedRows[0].DataBoundItem is not SalesInvoiceItemModel) return;
        BtnAccept.PerformClick();
    }

    public static (bool Accepted, ValueModel<InvoiceDupItemSelectKind, SalesInvoiceItemModel> Result) SelectItem(string product, string unit, decimal qty, IList<SalesInvoiceItemModel> invoiceItems)
    {
        var form = new FrmInvoiceItemSelection(product, unit, qty, invoiceItems);
        form.ShowDialog();
        return (form._accepted, form._selectionResult);
    }
}