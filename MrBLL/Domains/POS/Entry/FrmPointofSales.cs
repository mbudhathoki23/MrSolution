using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.Master.InventorySetup;
using DevExpress.XtraEditors.Repository;
using MoreLinq.Extensions;
using MrBLL.DataEntry.Common;
using MrBLL.Domains.POS.Master;
using MrDAL.Control.ControlsEx.NotifyPanel;
using MrDAL.Control.Dialogs;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Billing;
using MrDAL.Global.Common;
using MrDAL.Global.Dialogs;
using MrDAL.Models.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Domains.POS.Entry;

public partial class FrmPointOfSales : MrForm
{
    #region --------------- FORM EVENTS ---------------

    public FrmPointOfSales()
    {
        InitializeComponent();
        ObjGlobal.DGridColorCombo(DGrid);

        _salesEntry = new ClsSalesEntry();
        _barcodeWiseProducts = new List<SalesInvoiceProductModel>();
        _invoiceService = new SalesInvoiceService();
        _counters = new List<Counter>();
        bsInvoiceItems.DataSource = new List<SalesInvoiceItemModel>();

        // ReSharper disable once PossibleNullReferenceException
        _counterComboBox = new ToolStripComboBox
        {
            ComboBox = { DisplayMember = "CName", ValueMember = "CId", DropDownStyle = ComboBoxStyle.DropDownList },
            Width = 100
        };
        statusStripMain.Items.Insert(2, _counterComboBox);

        glkupMembers.Properties.SearchMode = glkupProducts.Properties.SearchMode = GridLookUpSearchMode.AutoSearch;
        glkupMembers.Properties.PopupFormWidth = 850;
        glkupProducts.Properties.PopupFormWidth = 1150;
    }

    private async void FrmPointOfSales_Shown(object sender, EventArgs e)
    {
        if (_initError) return;
        _initError = await LoadCountersAsync() == false;

        if (_initError) return;
        var confirm = FrmCounterSelection.SelectCounter(_counters);
        if (!confirm.Accept)
        {
            Close();
            return;
        }

        _counterComboBox.ComboBox.SelectedItem = confirm.Model;
        splitContainerMain.Panel2Collapsed = true;
    }

    private void FrmPointOfSales_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F6)
        {
            glkupMembers.Focus();
            glkupMembers.ShowPopup();
        }

        if (e.KeyCode == Keys.F8)
            if (!splitContainerMain.Panel2Collapsed)
                splitContainerMain.Panel2Collapsed = false;
        //splitContainerMain.Panel2Collapsed= 0;
    }

    private async void FrmPointOfSales_Load(object sender, EventArgs e)
    {
        UseWaitCursor = true;
        InitializeValues();

        //var response = await _invoiceService.GetInvoiceForPrintAsync("OSSPOS/00001/77-78");
        //if (response.Model != null)
        //{
        //    var report = new XtraReport1 { DataSource = null };
        //    report.DataSource = new List<AbbrInvoiceForPrintModel>() { response.Model };

        //    var tool = new ReportPrintTool(report);
        //    tool.ShowPreviewDialog();
        //}
        if (await LoadNextInvoiceNoAsync() == false)
        {
            _initError = true;
            Close();
            return;
        }

        if (await LoadCurrentCashLedgerAsync() == false)
        {
            _initError = true;
            Close();
            return;
        }

        if (await LoadProductsAsync() == false)
        {
            _initError = true;
            Close();
            return;
        }

        if (await LoadMembershipsAsync() == false)
        {
            _initError = true;
            Close();
            return;
        }

        TxtBarcode.Focus();
        UseWaitCursor = false;
    }

    #endregion --------------- FORM EVENTS ---------------

    #region --------------- FORM EVENTS OF THIS FORM ---------------

    private async void OnCounterComboSelectedIndexChanged(object sender, EventArgs e)
    {
        if (!(_counterComboBox.SelectedItem is Counter counter)) return;
        await LoadLastInvoiceAsync();
    }

    private async void glkupMembers_EditValueChanged(object sender, EventArgs e)
    {
        LblMemberName.Text = LblMemberShortName.Text = LblMemberType.Text = TxtDiscountPercentage.Text =
            TxtDiscount.Text = LblMemberAmount.Text = LblPriceTag.Text = LblMemberPhone.Text = string.Empty;

        if (glkupMembers.EditValue == null || !(glkupMembers.GetSelectedDataRow() is SalesMembershipModel model))
        {
            TxtDiscount.ReadOnly = true;
            CalculateInvoiceSummary();
            return;
        }

        TxtDiscount.ReadOnly = false;
        await PopulateMembershipDetailAsync(model.MShipId);
    }

    private void TxtAltQty_TextChanged(object sender, EventArgs e)
    {
        if (!TxtAltQty.Focused) return;
        if (!(glkupProducts.GetSelectedDataRow() is SalesInvoiceProductModel { HasAltUnit: true } selectedProduct))
            return;

        TxtQty.Clear();
        if (string.IsNullOrWhiteSpace(TxtAltQty.Text)) return;

        var altQty = TxtAltQty.Text.GetPositiveDecimalOrZero();
        if (altQty <= 0)
        {
            this.NotifyValidationError(TxtAltQty, @"Invalid alternate qty value.");
            return;
        }

        var convQty = selectedProduct.QtyConversionRate.GetValueOrDefault();
        TxtQty.Text = convQty > 0 && altQty > 0 ? (convQty * altQty).ToString(ObjGlobal.SysQtyFormat) : null;
    }

    private void TxtQty_TextChanged(object sender, EventArgs e)
    {
        if (!TxtQty.Focused) return;
        if (!(glkupProducts.GetSelectedDataRow() is SalesInvoiceProductModel selectedProduct))
            return;

        TxtAltQty.Clear();
        if (string.IsNullOrWhiteSpace(TxtQty.Text)) return;

        var qty = TxtQty.Text.GetPositiveDecimalOrZero();
        if (qty == 0)
        {
            this.NotifyValidationError(TxtQty, @"Invalid qty value.");
            return;
        }

        if (selectedProduct.AltQtyConversionRate.GetValueOrDefault() == 0) return;

        if (selectedProduct.HasAltUnit)
        {
            var ratio = selectedProduct.QtyConversionRate ?? 0 / selectedProduct.AltQtyConversionRate ?? 0;
            TxtAltQty.Text = (qty / ratio).ToString(ObjGlobal.SysQtyFormat);
        }
    }

    private void bsInvoiceItems_ListChanged(object sender, ListChangedEventArgs e)
    {
        //splitContainerMain.Panel2Collapsed = DGrid.Rows.Count == 0;
        for (var i = 1; i <= DGrid.Rows.Count; i++)
        {
            var model = (SalesInvoiceItemModel)DGrid.Rows[i - 1].DataBoundItem;
            model.SNo = i;
        }

        CalculateInvoiceSummary();
    }

    private void bsInvoiceItems_CurrentItemChanged(object sender, EventArgs e)
    {
        CalculateInvoiceSummary();
    }

    private void glkupProducts_EditValueChanged(object sender, EventArgs e)
    {
        TxtQty.Clear();
        TxtUOM.Clear();
        TxtAltQty.Clear();
        TxtAltUOM.Clear();

        if (!glkupProducts.Focused) TxtBarcode.Clear();

        if (glkupProducts.EditValue == null) return;
        if (!(glkupProducts.GetSelectedDataRow() is SalesInvoiceProductModel model)) return;

        //LblAltRate.Text = model.SalesRate.ToString("0.00");
        LoadSelectedProductInfo(model.ProductId);
    }

    private void TxtBarcode_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Escape) return;

        e.Handled = true;
        if (e.KeyChar == (char)Keys.Escape)
        {
            ResetInvoiceItemEntryFields();
            return;
        }

        if (e.KeyChar is (char)Keys.F1)
        {
        }

        if (TxtBarcode.Text.IsBlankOrEmpty() && DGrid.RowCount is 0)
        {
            this.NotifyValidationError(TxtBarcode, @"BARCODE CAN'T BE LEFT BLANK");
            TxtBarcode.Focus();
            return;
        }

        var barCode = TxtBarcode.Text.Trim();
        var bcProducts = _barcodeWiseProducts.Where(x => x.BarCode == barCode).ToList();
        if (!bcProducts.Any())
        {
            this.NotifyValidationError(TxtBarcode, @"No product found for input barcode.");
            TxtBarcode.Focus();
            return;
        }

        // if multiple products exist for entered barcode, ask to select a product
        SalesInvoiceProductModel product;
        if (bcProducts.Count > 1)
        {
            var confirm = FrmBarcodeProductSelection.SelectProduct(bcProducts);
            if (!confirm.Accepted) return;
            product = confirm.Model;
        }
        else
        {
            product = bcProducts[0];
        }

        glkupProducts.EditValueChanged -= glkupProducts_EditValueChanged;
        glkupProducts.EditValue = null;
        LoadSelectedProductInfo(product.ProductId);
        glkupProducts.EditValue = product.ProductId;

        var gridItems = (IList<SalesInvoiceItemModel>)bsInvoiceItems.List;
        var dupItem = gridItems.FirstOrDefault(x => x.ProductId == product.ProductId && x.UnitId == product.UnitId);
        if (dupItem != null)
        {
            if (dupItem.HasAltUnit && dupItem.AltUnitEntered)
            {
                TxtAltQty.Text = (dupItem.AltQty.GetValueOrDefault() + 1).ToString(ObjGlobal.SysQtyFormat);
                TxtAltQty_KeyDown(TxtBarcode, new KeyEventArgs(Keys.Enter));
            }
            else
            {
                TxtQty.Text = (dupItem.Qty + 1).ToString(ObjGlobal.SysQtyFormat);
                TxtQty_KeyDown(TxtBarcode, new KeyEventArgs(Keys.Enter));
            }
        }
        else if (!product.HasAltUnit)
        {
            TxtQty.Text = @"1";
            TxtQty_KeyDown(TxtBarcode, new KeyEventArgs(Keys.Enter));
        }

        glkupProducts.EditValueChanged += glkupProducts_EditValueChanged;
    }

    private void TxtDiscount_TextChanged(object sender, EventArgs e)
    {
        var discount = TxtDiscount.Text.GetDecimal();
        var billAmount = TxtBillAmount.Text.GetPositiveDecimalOrZero();

        TxtDiscount.BackColor = discount > 0 && discount > billAmount * (decimal)0.5 ? Color.Orange : Color.White;
        if (!TxtDiscount.Focused) return;

        if (discount < 0 || discount >= billAmount && discount > 0)
        {
            this.NotifyError(@"Invalid discount amount.");
            TxtDiscount.Clear();
            TxtDiscountPercentage.Clear();
            CalculateInvoiceSummary();
            return;
        }

        var grandTotal = billAmount - discount;
        if (glkupMembers.GetSelectedDataRow() == null)
            TxtDiscountPercentage.Text = discount == 0
                ? "0"
                : (discount / billAmount * 100).ToString(ObjGlobal.SysAmountFormat);
        TxtGrandTotal.Text = grandTotal > 0 ? grandTotal.ToString(ObjGlobal.SysAmountFormat) : @"0";
        CalculateInvoiceSummary();
    }

    private void TxtDiscountPercentage_TextChanged(object sender, EventArgs e)
    {
        if (!TxtDiscountPercentage.Focused) return;
        var disPercent = TxtDiscountPercentage.Text.GetDecimal();
        var billAmount = TxtBillAmount.Text.GetPositiveDecimalOrZero();

        if (billAmount == 0 && disPercent > 0)
        {
            this.NotifyError(@"Invalid bill amount.");
            TxtDiscountPercentage.Clear();
            TxtDiscount.Clear();
            CalculateInvoiceSummary();
            return;
        }

        if (disPercent < 0 || disPercent >= 100)
        {
            this.NotifyError(@"Invalid discount percent. The value must be between 0 and 100.");
            return;
        }

        var discount = disPercent / 100 * billAmount;
        TxtDiscount.Text = discount.ToString(ObjGlobal.SysAmountFormat);
        TxtGrandTotal.Text = (billAmount - discount).ToString(ObjGlobal.SysAmountFormat);
        CalculateInvoiceSummary();
    }

    private void glkupProducts_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == (char)Keys.Escape)
        {
            e.Handled = true;
            ResetInvoiceItemEntryFields();
            glkupProducts.EditValue = null;
            glkupProducts.Focus();
        }
    }

    private void TxtQty_KeyDown(object sender, KeyEventArgs e)
    {
        if (TxtQty.Text.GetDecimal() is 0) return;
        if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.F2)
        {
            if (e.KeyCode != Keys.Back && ((char)e.KeyCode != '.' || ((TextBox)sender).Text.Contains(".")))
                e.Handled = !decimal.TryParse(((char)e.KeyCode).ToString(), out _);
            return;
        }

        e.Handled = true;
        var qty = TxtQty.Text.GetDecimal();
        if (e.KeyCode == Keys.Enter && qty <= 0) return;

        if (!(glkupProducts.GetSelectedDataRow() is SalesInvoiceProductModel model)) return;
        var selectedInvoiceItemModel = DGrid.SelectedRows.Count == 0
            ? null
            : DGrid.SelectedRows[0].DataBoundItem as SalesInvoiceItemModel;
        var listItems = bsInvoiceItems.List.OfType<SalesInvoiceItemModel>().ToList();
        var updateMode = GrpProductInfo.Visible;

        if (updateMode && selectedInvoiceItemModel == null)
        {
            this.NotifyError("No invoice item selected for modification.");
            return;
        }

        // if user tried to change the qty, rate or discount
        if (e.KeyCode == Keys.F2)
        {
            //var confirm = FrmProductInfo.SetProductQtyRate(model.ProductId, TxtAltQty.Text.GetDecimal(),
            //    qty, model.SalesRate, lblItemDisP.Text.GetDecimal(), lblItemDis.Text.GetDecimal());
            //if (!confirm.Accepted) return;

            //TxtAltQty.TextChanged -= TxtAltQty_TextChanged;
            //TxtQty.TextChanged -= TxtQty_TextChanged;

            //TxtAltQty.Text = confirm.ChangedAltQty.GetDecimalQtyString();
            //TxtQty.Text = confirm.ChangedQty.GetDecimalQtyString();
            //lblItemDis.Text = confirm.Discount.GetDecimalString();
            //lblItemDisP.Text = confirm.DiscountPercent.GetDecimalString();
            //lblItemRate.Text = confirm.ChangedRate.GetDecimalString();

            //TxtAltQty.TextChanged += TxtAltQty_TextChanged;
            //TxtQty.TextChanged += TxtQty_TextChanged;
            return;
        }

        var iItemDisP = lblItemDisP.Text.GetDecimal();
        var dup = listItems.FirstOrDefault(x =>
            x.ProductId == model.ProductId && x.UnitId == model.UnitId && x.Rate == model.SalesRate &&
            x.ItemDisP == iItemDisP);
        var conflictedModel = updateMode
            ? listItems.Where(x =>
                    x.ProductId == model.ProductId && x.UnitId == model.UnitId &&
                    x.SNo != selectedInvoiceItemModel.SNo)
                .ToList()
            : listItems.Where(x => x.ProductId == model.ProductId && x.UnitId == model.UnitId).ToList();

        // if the user is explicitly entering qty
        if ((TextBox)sender == TxtQty && TxtQty.Focused)
            if (conflictedModel.Any() && !GrpProductInfo.Visible)
            {
                var confirm =
                    FrmInvoiceItemSelection.SelectItem(model.ProductName, model.UnitCode, qty, conflictedModel);
                if (!confirm.Accepted) return;

                if (confirm.Result.Item1 == FrmInvoiceItemSelection.InvoiceDupItemSelectKind.Append)
                {
                    bsInvoiceItems.Add(new SalesInvoiceItemModel
                    {
                        Qty = TxtQty.Text.GetDecimal(),
                        UnitName = model.UnitCode,
                        AltUnitId = model.AltUnitId,
                        UnitId = model.UnitId,
                        AltQty = model.HasAltUnit ? TxtAltQty.Text.GetDecimal() : null,
                        AltUnitName = TxtAltUOM.Text,
                        Rate = lblItemRate.Text.GetDecimal(),
                        FreeQty = 0,
                        ProductId = model.ProductId,
                        ProductName = model.ProductName,
                        ProductShortName = model.ProductShortName,
                        TaxPercent = model.TaxPercent,
                        ItemDis = lblItemDis.Text.GetDecimal(),
                        AltUnitEntered = false
                    });
                    ResetInvoiceItemEntryFields();
                    return;
                }

                var selectedItem = listItems.First(x => x.SNo == confirm.Result.Item2.SNo);
                selectedItem.Qty = TxtQty.Text.GetDecimal();
                selectedItem.AltQty = model.HasAltUnit ? TxtAltQty.Text.GetDecimal() : null;
                selectedItem.Rate = lblItemRate.Text.GetDecimal();
                selectedItem.ItemDis = lblItemDis.Text.GetDecimal();
                bsInvoiceItems.ResetBindings(false);

                ResetInvoiceItemEntryFields();
                return;
            }

        // update the selected product when entered through barcode
        if (dup != null && (TextBox)sender == TxtBarcode || dup != null && updateMode)
        {
            dup.Qty = TxtQty.Text.GetDecimal();
            dup.AltQty = model.HasAltUnit ? TxtAltQty.Text.GetDecimal() : null;
            dup.Rate = lblItemRate.Text.GetDecimal();
            dup.ItemDis = lblItemDis.Text.GetDecimal();
            bsInvoiceItems.ResetBindings(false);

            ResetInvoiceItemEntryFields();
            return;
        }

        bsInvoiceItems.Add(new SalesInvoiceItemModel
        {
            Qty = TxtQty.Text.GetDecimal(),
            UnitName = model.UnitCode,
            AltUnitId = model.AltUnitId,
            UnitId = model.UnitId,
            AltQty = model.HasAltUnit ? TxtAltQty.Text.GetDecimal() : null,
            AltUnitName = TxtAltUOM.Text,
            Rate = lblItemRate.Text.GetDecimal(),
            FreeQty = 0,
            ProductId = model.ProductId,
            ProductName = model.ProductName,
            ProductShortName = model.ProductShortName,
            TaxPercent = model.TaxPercent,
            ItemDis = lblItemDis.Text.GetDecimal(),
            AltUnitEntered = false
        });
        ResetInvoiceItemEntryFields();
    }

    private void TxtAltQty_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter && e.KeyCode != Keys.F2)
        {
            if (e.KeyCode != Keys.Back && ((char)e.KeyCode != '.' || ((TextBox)sender).Text.Contains(".")))
                e.Handled = !decimal.TryParse(((char)e.KeyCode).ToString(), out _);
            return;
        }

        e.Handled = true;
        var altQty = TxtAltQty.Text.GetDecimal();
        if (altQty <= 0)
        {
            this.NotifyValidationError(TxtAltQty, @"Invalid alternate qty for the selected product.");
            return;
        }

        if (!(glkupProducts.GetSelectedDataRow() is SalesInvoiceProductModel model)) return;
        if (!model.HasAltUnit) return;

        var selectedInvoiceItemModel = DGrid.SelectedRows.Count == 0
            ? null
            : DGrid.SelectedRows[0].DataBoundItem as SalesInvoiceItemModel;
        var listItems = bsInvoiceItems.List.OfType<SalesInvoiceItemModel>().ToList();
        var updateMode = GrpProductInfo.Visible;

        if (updateMode && selectedInvoiceItemModel == null)
        {
            this.NotifyError("No invoice item selected for modification.");
            return;
        }

        // if user tried to change the qty, rate or discount
        if (e.KeyCode == Keys.F2)
        {
            //var confirm = FrmProductInfo.SetProductQtyRate(model.ProductId, TxtAltQty.Text.GetDecimal(),
            //    TxtQty.Text.GetDecimal(), model.SalesRate, lblItemDisP.Text.GetDecimal(),
            //    lblItemDis.Text.GetDecimal());
            //if (!confirm.Accepted) return;

            //TxtAltQty.TextChanged -= TxtAltQty_TextChanged;
            //TxtQty.TextChanged -= TxtQty_TextChanged;

            //TxtAltQty.Text = confirm.ChangedAltQty.ToString();
            //TxtQty.Text = confirm.ChangedQty.ToString();
            //lblItemDis.Text = confirm.Discount.ToString();
            //lblItemDisP.Text = confirm.DiscountPercent.ToString();
            //lblItemRate.Text = confirm.ChangedRate.ToString();

            //TxtAltQty.TextChanged += TxtAltQty_TextChanged;
            //TxtQty.TextChanged += TxtQty_TextChanged;
            //return;
        }

        var iItemDisP = lblItemDisP.Text.GetDecimal();
        var dup = listItems.FirstOrDefault(x =>
            x.ProductId == model.ProductId && x.UnitId == model.UnitId && x.Rate == model.SalesRate &&
            x.ItemDisP == iItemDisP);
        var conflictedModel = updateMode
            ? listItems.Where(x =>
                    x.ProductId == model.ProductId && x.UnitId == model.UnitId &&
                    x.SNo != selectedInvoiceItemModel.SNo)
                .ToList()
            : listItems.Where(x => x.ProductId == model.ProductId && x.UnitId == model.UnitId).ToList();

        // if the user is explicitly entering qty
        if ((TextBox)sender == TxtAltQty && TxtAltQty.Focused)
            if (conflictedModel.Any() && !GrpProductInfo.Visible)
            {
                var confirm = FrmInvoiceItemSelection.SelectItem(model.ProductName, model.AltUnitCode, altQty,
                    conflictedModel);
                if (!confirm.Accepted) return;

                if (confirm.Result.Item1 == FrmInvoiceItemSelection.InvoiceDupItemSelectKind.Append)
                {
                    bsInvoiceItems.Add(new SalesInvoiceItemModel
                    {
                        Qty = TxtQty.Text.GetDecimal(),
                        UnitName = model.UnitCode,
                        AltUnitId = model.AltUnitId,
                        UnitId = model.UnitId,
                        AltQty = model.HasAltUnit ? TxtAltQty.Text.GetDecimal() : null,
                        AltUnitName = TxtAltUOM.Text,
                        Rate = lblItemRate.Text.GetDecimal(),
                        FreeQty = 0,
                        ProductId = model.ProductId,
                        ProductName = model.ProductName,
                        ProductShortName = model.ProductShortName,
                        TaxPercent = model.TaxPercent,
                        ItemDis = lblItemDis.Text.GetDecimal(),
                        AltUnitEntered = true
                    });
                    ResetInvoiceItemEntryFields();
                    return;
                }

                var selectedItem = listItems.First(x => x.SNo == confirm.Result.Item2.SNo);
                selectedItem.Qty = TxtQty.Text.GetDecimal();
                selectedItem.AltQty = model.HasAltUnit ? TxtAltQty.Text.GetDecimal() : null;
                selectedItem.Rate = lblItemRate.Text.GetDecimal();
                selectedItem.ItemDis = lblItemDis.Text.GetDecimal();
                bsInvoiceItems.ResetBindings(false);

                ResetInvoiceItemEntryFields();
                return;
            }

        if (dup != null && (TextBox)sender == TxtBarcode || dup != null && updateMode)
        {
            dup.Qty = TxtQty.Text.GetDecimal();
            dup.AltQty = TxtAltQty.Text.GetDecimal();
            dup.AltUnitEntered = true;
            bsInvoiceItems.ResetBindings(false);

            ResetInvoiceItemEntryFields();
            return;
        }

        bsInvoiceItems.Add(new SalesInvoiceItemModel
        {
            Qty = TxtQty.Text.GetDecimal(),
            UnitName = model.UnitCode,
            AltUnitId = model.AltUnitId,
            UnitId = model.UnitId,
            AltQty = TxtAltQty.Text.GetDecimal(),
            AltUnitName = TxtAltUOM.Text,
            Rate = model.SalesRate,
            FreeQty = 0,
            ProductId = model.ProductId,
            ProductName = model.ProductName,
            ProductShortName = model.ProductShortName,
            TaxPercent = model.TaxPercent,
            AltUnitEntered = true,
            ItemDis = lblItemDis.Text.GetDecimal()
        });
        ResetInvoiceItemEntryFields();
    }

    private void TxtDiscountPercentage_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    private void TxtDiscount_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
            BtnCash.Focus();
        else if (e.KeyChar != (char)Keys.Back && (e.KeyChar != '.' || ((TextBox)sender).Text.Contains(".")))
            e.Handled = !decimal.TryParse(e.KeyChar.ToString(), out _);
    }

    #endregion --------------- FORM EVENTS OF THIS FORM ---------------

    #region --------------- METHOD FOR THIS FORM ---------------

    private void GlobalEnter_Event(object sender, EventArgs e)
    {
        BackColor = Color.Thistle;
    }

    private void GlobalLeave_Event(object sender, EventArgs e)
    {
        BackColor = Color.Thistle;
    }

    private void ResetAll()
    {
        DGrid.Rows.Clear();
        bsInvoiceItems.Clear();
        glkupMembers.EditValue = null;

        LblMemberName.Text = LblMemberAmount.Text =
            LblMemberType.Text = LblMemberShortName.Text = LblMemberPhone.Text = string.Empty;
        TxtBarcode.Focus();

        TxtBillAmount.Clear();
        TxtDiscount.Clear();
        TxtDiscountPercentage.Clear();
        TxtGrandTotal.Clear();

        LblItemsTotalQty.Text = LblItemsTotal.Text =
            LblItemsDiscountSum.Text = LblItemsNetAmount.Text = lblRefInvoiceId.Text = string.Empty;
        GrpInvoiceTotalDetails.Visible = true;

        GrpProductInfo.Visible = false;
        TxtBarcode.Clear();
        TxtQty.Clear();
        TxtUOM.Clear();
        TxtAltQty.Clear();
        TxtAltUOM.Clear();
        lblItemDis.Text = lblItemDisP.Text = string.Empty;

        //LblActualPrice.Text = string.Empty;
        LblRate.Text = LblProduct.Text = LblRate.Text = LblStockQty.Text =
            lblTaxable.Text = lblNonTaxable.Text = lblTax.Text = lblTermAmount.Text = string.Empty;

        _ = LoadLastInvoiceAsync();
        _ = LoadNextInvoiceNoAsync();
    }

    private void InitializeValues()
    {
        TxtUOM.Enabled = TxtAltQty.Enabled =
            TxtAltUOM.Enabled = TxtGrandTotal.Enabled = TxtBillAmount.Enabled = false;
        lblTaxable.Text = lblNonTaxable.Text = lblTax.Text = lblTermAmount.Text = lblRefInvoiceId.Text = null;

        slBranch.Text = ObjGlobal.SysBranchName;
        slUser.Text = ObjGlobal.LogInUser;
        slToday.Text = DateTime.Today.ToString("dd/MM/yyyy");
        slMiti.Text = ObjGlobal.ReturnNepaliDate(slToday.Text);

        glkupMembers.KeyPress += (_, args) =>
        {
            if (args.KeyChar == (char)Keys.Escape)
            {
                args.Handled = true;
                glkupMembers.EditValue = null;
                glkupMembers.Focus();
            }
        };
    }

    private bool InputFieldsValid()
    {
        if (DGrid.Rows.Count == 0)
        {
            this.NotifyError(@"No item in the list to generate sales invoice.");
            return false;
        }

        if (TxtGrandTotal.Text.GetPositiveDecimalOrZero() == 0 ||
            bsInvoiceItems.List.OfType<SalesInvoiceItemModel>().Sum(x => x.ActualAmount) == 0)
        {
            this.NotifyError(@"The sum of entered items is not valid for sales invoice.", 4);
            return false;
        }

        return true;
    }

    private async Task<bool> LoadCountersAsync()
    {
        // ReSharper disable once PossibleNullReferenceException
        _counterComboBox.ComboBox.DataSource = null;
        _counters?.Clear();

        _counterComboBox.ComboBox.SelectedIndexChanged -= OnCounterComboSelectedIndexChanged;
        var response = await new SalesInvoiceService().GetCountersAsync(ObjGlobal.SysBranchId);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return false;
        }

        _counterComboBox.ComboBox.DataSource = _counters = response.List.ToList();
        _counterComboBox.ComboBox.DisplayMember = "CName";
        _counterComboBox.ComboBox.ValueMember = "CId";

        _counterComboBox.ComboBox.SelectedIndex = -1;
        _counterComboBox.ComboBox.SelectedIndexChanged += OnCounterComboSelectedIndexChanged;
        return true;
    }

    private async Task<bool> LoadLastInvoiceAsync()
    {
        slLastInvoice.Text = slLastGrandTotal.Text = slLastTender.Text = slLastReturn.Text = null;

        var response = await _invoiceService.GetLastInvoiceAsync(ObjGlobal.SysBranchId, null, null);
        if (!response.Success) return false;

        if (response.Model == null)
        {
            slLastInvoice.Text = @"(NA)";
            return false;
        }

        slLastInvoice.Text = response.Model.SB_Invoice;
        slLastGrandTotal.Text = response.Model.LN_Amount.ToString(ObjGlobal.SysAmountFormat);
        slLastTender.Text = response.Model.Tender_Amount.ToString(ObjGlobal.SysAmountFormat);
        slLastReturn.Text = response.Model.Return_Amount.ToString(ObjGlobal.SysAmountFormat);
        return true;
    }

    private async Task<bool> LoadNextInvoiceNoAsync()
    {
        slNextInvoice.Text = null;

        var response = await _invoiceService.GetNextInvoiceNoAsync(ObjGlobal.SysBranchId);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return false;
        }

        slNextInvoice.Text = response.Model;
        return true;
    }

    private async Task<bool> LoadProductsAsync()
    {
        bsProducts.DataSource = null;
        var response = await _invoiceService.GetProductsAsync(true);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return false;
        }

        _barcodeWiseProducts = (IList<SalesInvoiceProductModel>)response.List;
        bsProducts.DataSource = _barcodeWiseProducts.DistinctBy(x => x.ProductId).ToList();
        glkupProducts.Properties.DisplayMember = "ProductName";
        glkupProducts.Properties.ValueMember = "ProductId";
        return true;
    }

    private async Task<bool> LoadMembershipsAsync()
    {
        bsMembersList.DataSource = null;

        var response = await _invoiceService.GetMembersAsync(ObjGlobal.SysBranchId);
        if (!response.Success)
        {
            response.ShowErrorDialog("Error fetching members list.");
            return false;
        }

        bsMembersList.DataSource = response.List.ToList();
        glkupMembers.Properties.DisplayMember = "MShipDesc";
        glkupMembers.Properties.ValueMember = "MShipId";
        return true;
    }

    private async Task<bool> LoadCurrentCashLedgerAsync()
    {
        var response = await _invoiceService.GetCashLedgerAsync(ObjGlobal.LogInUser);
        if (response.Model == null)
        {
            response.ShowErrorDialog("Unable to fetch current user cash ledger.");
            return false;
        }

        return true;
    }

    private async Task<bool> PopulateMembershipDetailAsync(int memberId)
    {
        LblMemberName.Text = LblMemberShortName.Text = LblMemberType.Text = TxtDiscountPercentage.Text =
            TxtDiscount.Text = LblMemberAmount.Text = LblPriceTag.Text = string.Empty;

        var response = await _invoiceService.GetMembershipDetailAsync(memberId);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return false;
        }

        if (response.Model == null) return true;

        LblMemberName.Text = response.Model.MShipDesc;
        LblMemberShortName.Text = response.Model.MShipShortName;
        LblMemberPhone.Text = response.Model.PhoneNo;
        LblMemberType.Text = response.Model.MemberType;
        TxtDiscountPercentage.Text = response.Model.DiscountPercent.ToString(ObjGlobal.SysAmountFormat);
        TxtDiscountPercentage.Enabled = response.Model.DiscountPercent <= 0;
        var discountAmount = response.Model.DiscountPercent > 0
            ? response.Model.DiscountPercent * ObjGlobal.ReturnDecimal(TxtBillAmount.Text) / 100
            : 0;
        TxtDiscount.Text = discountAmount.ToString(ObjGlobal.SysAmountFormat);
        LblMemberAmount.Text = response.Model.Balance.ToString(ObjGlobal.SysAmountFormat);
        LblPriceTag.Text = response.Model.PriceTag;

        CalculateInvoiceSummary();
        return true;
    }

    private void ResetInvoiceItemEntryFields()
    {
        TxtBarcode.Clear();
        glkupProducts.EditValue = null;
        TxtQty.Clear();
        TxtAltQty.Clear();
        TxtUOM.Clear();
        TxtAltUOM.Clear();

        LblRate.Text = LblProduct.Text = LblRate.Text =
            LblStockQty.Text = lblItemDis.Text = lblItemDisP.Text = lblItemRate.Text = string.Empty;
        GrpProductInfo.Visible = false;
        TxtBarcode.Enabled = glkupProducts.Enabled = true;
        TxtBarcode.Focus();
    }

    private void LoadSelectedProductInfo(long productId)
    {
        LblProduct.Text = LblStockQty.Text = LblRate.Text = string.Empty;

        var product = _barcodeWiseProducts.FirstOrDefault(x => x.ProductId == productId);
        if (product == null) return;

        LblProduct.Text = product.ProductName;
        TxtBarcode.Text = product.ProductShortName;
        LblRate.Text = product.SalesRate.ToString(ObjGlobal.SysAmountFormat);

        if (product.HasAltUnit)
            TxtAltQty.Text = product.AltQtyConversionRate.GetValueOrDefault().ToString(ObjGlobal.SysQtyFormat);
        TxtAltQty.Enabled = product.HasAltUnit;

        TxtUOM.Text = product.UnitCode;
        TxtAltUOM.Text = product.AltUnitCode;
        TxtQty.Text = product.QtyConversionRate.GetValueOrDefault().ToString(ObjGlobal.SysQtyFormat);
        lblItemDis.Text = lblItemDisP.Text = @"0.00";
        lblItemRate.Text = product.SalesRate.GetDecimalString();

        if (product.HasAltUnit)
        {
            TxtAltQty.Focus();
        }
        else TxtQty.Focus();
    }

    private SalesInvoiceEModel GetInvoiceEntryModel()
    {
        // grab from cash window
        const SalesInvoicePaymentMode paymentMode = SalesInvoicePaymentMode.Cash;
        var invoiceItems = bsInvoiceItems.List.OfType<SalesInvoiceItemModel>().ToList();

        var model = new SalesInvoiceEModel
        {
            CurrentDateTime = DateTime.Now,
            InvoiceMode = "POS",
            InvoiceType = "Local",
            NetAmount = TxtGrandTotal.Text.GetDecimal(),
            LocalNetAmount = TxtGrandTotal.Text.GetDecimal(),
            ActionType = "NEW",
            AgentId = null,
            ChqDate = null,
            ChqNo = null,
            CBranchId = ObjGlobal.SysBranchId,
            CurrencyId = ObjGlobal.SysCurrencyId,
            DueDate = null,
            CurrencyRate = 1,
            FiscalYearId = ObjGlobal.SysFiscalYearId,
            CounterId = ((Counter)_counterComboBox.SelectedItem).CId,
            MembershipId = (glkupMembers.EditValue as SalesMembershipModel)?.MShipId,
            TenderAmount = 0,
            ReturnAmount = 0,
            SyncOriginId = ObjGlobal.LocalOriginId,
            EnteredBy = ObjGlobal.LogInUser,
            NAmount = TxtBillAmount.Text.GetDecimal(),
            DueDays = null,
            PaymentModeE = paymentMode,
            TaxableAmount = lblTaxable.Text.GetDecimal(),
            NonTaxableAmount = lblNonTaxable.Text.GetDecimal(),
            PaymentMode = paymentMode.ToString(),
            TermAmount = lblTermAmount.Text.GetDecimal(),
            TaxAmount = lblTax.Text.GetDecimal(),
            Printed = false,
            RefTempInvoiceId = string.IsNullOrWhiteSpace(lblRefInvoiceId.Text) ? null : lblRefInvoiceId.Text,
            Terms = new List<SB_Term>(),
            Items = invoiceItems.Select(x => new SB_Details
            {
                P_Id = x.ProductId,
                Unit_Id = x.UnitId,
                AltStock_Qty = x.AltQty,
                Alt_UnitId = x.AltUnitId,
                Alt_Qty = x.AltQty.GetValueOrDefault(0),
                Invoice_SNo = x.SNo,
                Batch_No = null,
                Exp_Date = null,
                Gdn_Id = null,
                Qty = x.Qty,
                Rate = x.Rate,
                B_Amount = x.ActualBasicAmount,
                T_Amount = x.ProductTermAmount,
                N_Amount = x.ActualAmountAfterDiscount,
                Stock_Qty = x.StockQty,
                Narration = string.Empty,
                SO_Invoice = string.Empty,
                SC_Invoice = string.Empty,
                Tax_Amount = x.TaxableAmount,
                V_Amount = x.TaxAmount,
                V_Rate = x.TaxPercent,
                Free_Qty = x.FreeQty,
                StockFree_Qty = x.FreeQty,
                ExtraFree_Qty = 0,
                ExtraStockFree_Qty = 0,
                PDiscountRate = x.ItemDisP,
                PDiscount = x.ItemDis,
                BDiscountRate = x.BillDisP,
                BDiscount = x.BillDis,
                SyncGlobalId = Guid.NewGuid()
            }).ToList()
        };

        invoiceItems.ForEach(x =>
        {
            // add tax term for taxable amount
            if (x.TaxAmount > 0)
                model.Terms.Add(new SB_Term
                {
                    SNo = x.SNo,
                    ST_Id = ObjGlobal.SalesVatTermId,
                    Rate = x.TaxPercent,
                    Amount = x.TaxAmount,
                    Term_Type = "P",
                    Product_Id = x.ProductId
                });

            // add term for item-wise discount if exists
            if (x.ItemDis > 0)
                model.Terms.Add(new SB_Term
                {
                    SNo = x.SNo,
                    ST_Id = ObjGlobal.SalesDiscountTermId,
                    Rate = x.ItemDisP,
                    Amount = x.ItemDis,
                    Term_Type = "P",
                    Product_Id = x.ProductId
                });

            // add term for bill discount if exists
            if (x.BillDis > 0)
            {
                model.Terms.Add(new SB_Term
                {
                    SNo = x.SNo,
                    ST_Id = ObjGlobal.SalesSpecialDiscountTermId,
                    Rate = x.BillDisP,
                    Amount = x.BillDis,
                    Term_Type = "B",
                    Product_Id = x.ProductId
                });
                model.Terms.Add(new SB_Term
                {
                    SNo = x.SNo,
                    ST_Id = ObjGlobal.SalesSpecialDiscountTermId,
                    Rate = x.BillDisP,
                    Amount = x.BillDis * x.ActualAmountAfterDiscount / model.NAmount,
                    Term_Type = "BT",
                    Product_Id = x.ProductId
                });
            }
        });

        return model;
    }

    private void PopulateInvoiceItemForEdit(SalesInvoiceItemModel model)
    {
        ResetInvoiceItemEntryFields();

        GrpProductInfo.Visible = true;
        LblProduct.Text = model.ProductName;
        TxtBarcode.Text = model.ProductShortName;
        glkupProducts.EditValue = model.ProductId;
        LblRate.Text = model.Rate.ToString(ObjGlobal.SysAmountFormat);
        TxtAltQty.Text = model.HasAltUnit ? model.AltQty.GetValueOrDefault().ToString(ObjGlobal.SysQtyFormat) : "";
        TxtAltQty.Enabled = model.HasAltUnit;
        TxtUOM.Text = model.UnitName;
        TxtAltUOM.Text = model.AltUnitName;
        TxtQty.Text = model.Qty.ToString(ObjGlobal.SysQtyFormat);
        TxtBarcode.Enabled = glkupProducts.Enabled = false;
        lblItemDis.Text = model.ItemDis.GetDecimalString();
        lblItemDisP.Text = model.ItemDisP.GetDecimalString();
        lblItemRate.Text = model.Rate.GetDecimalString();
    }

    private void CalculateInvoiceSummary()
    {
        TxtDiscountPercentage.Enabled = true;

        // reset init data if no item present in the datagridview
        if (DGrid.Rows.Count == 0)
        {
            lblTax.Text = LblItemsTotalQty.Text = LblItemsTotal.Text = LblItemsDiscountSum.Text =
                LblItemsNetAmount.Text = TxtBillAmount.Text = TxtDiscount.Text = TxtGrandTotal.Text = @"0.00";
            BtnCash.Enabled = BtnNext.Enabled = BtnHold.Enabled = false;
            return;
        }

        var items = bsInvoiceItems.List.OfType<SalesInvoiceItemModel>().ToList();
        var itemWiseDiscountSum = items.Sum(x => x.ItemDis);
        var itemNetAmount = items.Sum(x => x.ActualAmountAfterDiscount);

        LblItemsTotalQty.Text = items.Sum(x => x.Qty).ToString(ObjGlobal.SysQtyFormat);
        LblItemsTotal.Text = (itemNetAmount + itemWiseDiscountSum).ToString(ObjGlobal.SysAmountFormat);
        LblItemsNetAmount.Text = itemNetAmount.ToString(ObjGlobal.SysAmountFormat);
        var masterBasicAmount = items.Sum(x => x.ActualAmountAfterDiscount);

        //var actualAmountSum = items.Sum(x => x.ActualAmount);
        decimal billDisP = 0, billDis = 0, grandTotal = 0;
        if (glkupMembers.GetSelectedDataRow() is SalesMembershipModel membership)
        {
            billDisP = membership.DiscountPercent;
            billDis = billDisP == 0 ? 0 : masterBasicAmount * (billDisP / 100);
            TxtDiscountPercentage.Text = billDisP.ToString(ObjGlobal.SysAmountFormat);
            TxtDiscountPercentage.Enabled = false;
            items.ForEach(x => { x.BillDis = billDis; });
        }
        else
        {
            billDis = TxtDiscount.Text.GetDecimal();
            items.ForEach(x =>
                x.BillDis = billDis is 0 ? 0 : billDis * x.ActualAmountAfterDiscount / itemNetAmount);
        }

        var masterNetAmount = masterBasicAmount - billDis;
        TxtBillAmount.Text = ((object)LblItemsNetAmount.Text).GetDecimalString();
        TxtDiscount.Text = billDis.ToString(ObjGlobal.SysAmountFormat);
        TxtGrandTotal.Text = masterNetAmount.ToString(ObjGlobal.SysAmountFormat);
        lblTaxable.Text = items.Sum(x => x.TaxableAmount).ToString(ObjGlobal.SysQtyFormat);
        lblNonTaxable.Text = items.Sum(x => x.TaxExempted).ToString(ObjGlobal.SysQtyFormat);
        lblTax.Text = items.Sum(x => x.TaxAmount).ToString(ObjGlobal.SysQtyFormat);
        lblTermAmount.Text = billDis.ToString(ObjGlobal.SysAmountFormat);

        BtnCash.Enabled = BtnNext.Enabled = BtnHold.Enabled = true;
    }

    private void OpenBarcodeList()
    {
    }

    #endregion --------------- METHOD FOR THIS FORM ---------------

    #region --------------- DATA GRID EVENTS ---------------

    private void DGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter || DGrid.SelectedRows.Count == 0) return;
        e.SuppressKeyPress = true;

        if (!(DGrid.SelectedRows[0].DataBoundItem is SalesInvoiceItemModel model)) return;

        PopulateInvoiceItemForEdit(model);
        TxtBarcode.Focus();
    }

    private void DGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (DGrid.SelectedRows.Count == 0 ||
            !(DGrid.SelectedRows[0].DataBoundItem is SalesInvoiceItemModel model)) return;
        DGrid_KeyDown(DGrid, new KeyEventArgs(Keys.Enter));
    }

    private void DGrid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
        DGrid_KeyDown(DGrid, new KeyEventArgs(Keys.Enter));
    }

    private void DGrid_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
    {
        bsInvoiceItems.ResetBindings(false);
    }

    #endregion --------------- DATA GRID EVENTS ---------------

    #region --------------- BUTTON CLICK EVENTS ---------------

    private void BtnCancelItemEdit_Click(object sender, EventArgs e)
    {
        ResetInvoiceItemEntryFields();
    }

    private void BtnReset_Click(object sender, EventArgs e)
    {
        ResetAll();
    }

    private async void BtnHold_Click(object sender, EventArgs e)
    {
        if (!InputFieldsValid()) return;

        var model = GetInvoiceEntryModel();
        var response = await _invoiceService.HoldSalesInvoiceAsync(model);
        if (!response.Success)
        {
            response.ShowErrorDialog();
            return;
        }

        ResetAll();
        this.NotifySuccess("SAVED SUCCESSFULLY..!!");
    }

    private async void BtnCash_Click(object sender, EventArgs e)
    {
        //if (!InputFieldsValid()) return;

        //var model = GetInvoiceEntryModel();
        //var pmtResponse = FrmPointOfSalesPayment.GetPayment(TxtGrandTotal.Text.GetDecimal(),
        //    glkupMembers.GetSelectedDataRow() as SalesMembershipModel, SalesInvoicePaymentMode.Cash, _currentCashLedgerId);
        //if (!pmtResponse.Accepted) return;

        //model.PartyName = pmtResponse.Model.InvoiceToName;
        //model.Address = pmtResponse.Model.InvoiceToAddress;
        //model.MobileNo = pmtResponse.Model.InvoiceToPhone;
        //model.VatNo = pmtResponse.Model.InvoiceToPan;
        //model.ContactPerson = pmtResponse.Model.ContactPerson;
        //model.LedgerId = pmtResponse.Model.LedgerId;
        //model.PaymentModeE = SalesInvoicePaymentMode.Cash;
        //model.TenderAmount = pmtResponse.Model.TenderAmount;
        //model.ReturnAmount = pmtResponse.Model.ReturnAmount;
        //model.Printed = pmtResponse.Model.PrintInvoice;
        //model.Remarks = pmtResponse.Model.Remarks;
        //model.PaymentMode = pmtResponse.Model.PaymentMode.GetDescription();

        //var response = await new SalesInvoiceService().SaveInvoiceAsync(model);
        //if (response.Model == null || !response.Success)
        //{
        //    MessageBox.Show(response.ErrorMessage, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    return;
        //}

        //slLastInvoice.Text = "";
        //ResetAll();

        //// print the invoice if user has chosen to print it
        //if (model.Printed)
        //    _salesEntry.PrintDirectSalesInvoice("SAVE", ObjGlobal.ReturnDecimal(TxtGrandTotal.Text.Trim()),
        //        response.Model, _invoicePrinter);

        //this.NotifySuccess($"SALES INVOICE {response.Model} SAVED SUCCESSFULLY..!!");

        //_salesEntry.SbMaster.SB_Invoice = response.Model;
        //_ = _salesEntry.SalesInvoiceStockPosting();
        //_ = _salesEntry.SalesInvoiceAccountPosting();
    }

    private async void BtnNext_Click(object sender, EventArgs e)
    {
        //var gtValid = TxtGrandTotal.Text.IsPositiveDecimal(false);
        //if (!gtValid)
        //{
        //    this.NotifyError(@"THE GRAND TOTAL (NET) AMOUNT IS NOT VALID FOR BILL PROCESSING..??");
        //    return;
        //}

        //if (!InputFieldsValid()) return;
        //var model = GetInvoiceEntryModel();

        //var pmtResponse = FrmPointOfSalesPayment.GetPayment(TxtGrandTotal.Text.GetDecimal(),
        //    glkupMembers.GetSelectedDataRow() as SalesMembershipModel, SalesInvoicePaymentMode.Card,
        //    _currentCashLedgerId);
        //if (!pmtResponse.Accepted) return;

        //model.PartyName = pmtResponse.Model.InvoiceToName;
        //model.Address = pmtResponse.Model.InvoiceToAddress;
        //model.MobileNo = pmtResponse.Model.InvoiceToPhone;
        //model.VatNo = pmtResponse.Model.InvoiceToPan;
        //model.ContactPerson = pmtResponse.Model.ContactPerson;
        //model.LedgerId = pmtResponse.Model.LedgerId;
        //model.Printed = pmtResponse.Model.PrintInvoice;
        //model.Remarks = pmtResponse.Model.Remarks;
        //model.PaymentModeE = pmtResponse.Model.PaymentMode;
        //model.PaymentMode = pmtResponse.Model.PaymentMode.GetDescription();

        //var response = await new SalesInvoiceService().SaveInvoiceAsync(model);
        //if (response.Model == null || !response.Success)
        //{
        //    MessageBox.Show(response.ErrorMessage, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    return;
        //}

        //slLastInvoice.Text = "";
        //ResetAll();

        //// print the invoice if user has chosen to print it
        //if (model.Printed)
        //    _salesEntry.PrintDirectSalesInvoice("SAVE", ObjGlobal.ReturnDecimal(TxtGrandTotal.Text.Trim()),
        //        response.Model, _invoicePrinter);

        //this.NotifySuccess($"SALES INVOICE {response.Model} SAVED SUCCESSFULLY");

        //_ = _salesEntry.SalesInvoiceAccountPosting();
        //_ = _salesEntry.SalesInvoiceStockPosting();
    }

    private void btnReprintLast_Click(object sender, EventArgs e)
    {
        var confirm = FrmSalesInvoices.SelectInvoice(SalesInvoiceActionTag.New, null, ObjGlobal.SysBranchId);
        if (!confirm.Accepted) return;

        new FrmDocumentPrint("DLL", "SB", string.Empty, confirm.Model.SB_Invoice, confirm.Model.SB_Invoice,
                string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, ObjGlobal.SysDefaultInvoiceDesign)
            .ShowDialog();
    }

    private async void btnReverse_Click(object sender, EventArgs e)
    {
        var confirm = FrmSalesInvoices.SelectInvoice(SalesInvoiceActionTag.New, null, ObjGlobal.SysBranchId);
        if (!confirm.Accepted) return;

        var delConfirm = FrmDeleteConfirm.Confirmed("Reverse Confirmation",
            $@"Selected invoice {confirm.Model.SB_Invoice} will be reversed and can't be reverted. continue?",
            true);
        if (!delConfirm.Accepted) return;

        var response = await _invoiceService.ReverseInvoiceAsync(confirm.Model.SB_Invoice, ObjGlobal.LogInUser,
            delConfirm.Remarks, DateTime.Now);
        if (!response.Value)
        {
            response.ShowErrorDialog();
            return;
        }

        this.NotifySuccess($@"Invoice {confirm.Model.SB_Invoice} reversed successfully.", 4);
    }

    private async void BtnHoldList_Click(object sender, EventArgs e)
    {
        var (accepted, model) = FrmTempInvoices.SelectTempInvoice();
        if (!accepted)
        {
            return;
        }

        var response = await _invoiceService.GetTempInvoice(model);
        if (response.Model == null)
        {
            response.ShowErrorDialog("UNABLE TO FETCH SELECTED TEMPORARY INVOICE DETAIL..!!");
            return;
        }

        ResetAll();
        var joinedItems = (from item in response.Model.Items
            join bc in _barcodeWiseProducts on new
                    { ProductId = item.P_Id, UnitId = item.Unit_Id, AltUnitId = item.Alt_UnitId }
                equals new { bc.ProductId, bc.UnitId, bc.AltUnitId }
            select new { item, bc }).ToList();
        joinedItems.ForEach(x =>
        {
            bsInvoiceItems.Add(new SalesInvoiceItemModel
            {
                ProductId = x.bc.ProductId,
                AltQty = x.item.Alt_Qty,
                Qty = x.item.Qty.GetValueOrDefault(),
                Rate = x.item.Rate,
                AltUnitId = x.item.Alt_UnitId,
                AltUnitName = x.bc.AltUnitCode,
                ProductShortName = x.bc.ProductShortName,
                UnitId = x.item.Unit_Id,
                TaxPercent = x.bc.TaxPercent,
                ProductName = x.bc.ProductName,
                UnitName = x.bc.UnitCode,
                ItemDis = x.item.PDiscount.GetValueOrDefault(),
                BillDis = x.item.BDiscount.GetValueOrDefault(),
                FreeQty = x.item.Free_Qty.GetValueOrDefault(),
                SNo = x.item.Invoice_SNo
            });
        });
        lblRefInvoiceId.Text = response.Model.Master.SB_Invoice;
        BtnHold.Enabled = false;
    }

    #endregion --------------- BUTTON CLICK EVENTS ---------------

    #region --------------- OBJECT ---------------

    private readonly SalesInvoiceService _invoiceService;
    private IList<SalesInvoiceProductModel> _barcodeWiseProducts;
    private IList<Counter> _counters;
    private readonly ToolStripComboBox _counterComboBox;
    private readonly ClsSalesEntry _salesEntry;
    private bool _initError;

    #endregion --------------- OBJECT ---------------
}