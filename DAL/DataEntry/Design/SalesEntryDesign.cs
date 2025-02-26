using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using MrDAL.Global.Common;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Design;

public class SalesEntryDesign : ISalesDesign
{
    public DataGridView GetSalesEntryGridDesign(DataGridView dGrid, string module)
    {
        dGrid.ReadOnly = true;
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        //dGrid.RowHeadersVisible = true;
        //dGrid.RowHeadersWidth = 100;
        //dGrid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;

        dGrid.AddColumn("GTxtSNo", "SNo", "GTxtSNo", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORTNAME", "GTxtShortName", 100, 2, ObjGlobal.StockShortNameWise);
        dGrid.AddColumn("GTxtProduct", "PRODUCT", "GTxtProduct", 250, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodown", 100, 2, ObjGlobal.SalesGodownEnable);
        dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 100, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltUOMId", "ALT_UOM_ID", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtAltUOM", "UOM", "GTxtAltUOM", 80, 2, true);
        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 80, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUOMId", "UOM_ID", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtUOM", "UOM", "GTxtUOM", 80, 2, true);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtTermAmount", "TERM", "GTxtTermAmount", 90, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNetAmount", "NET_AMOUNT", "GTxtNetAmount", 120, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltStockQty", "ALT_STOCK_QTY", "GTxtAltStockQty", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtStockQty", "STOCK_QTY", "GTxtStockQty", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);

        if (module != "SQ" && module != "SO")
        {
            dGrid.AddColumn(module is "SR" ? "GTxtInvoiceNo" : "GTxtOrderNo", module is "SR" ? "SB_INVOICE" : "ORDER_NO", "GTxtOrderNo", 0, 2, false);
            dGrid.AddColumn(module is "SR" ? "GTxtInvoiceSno" : "GTxtOrderSno", module is "SR" ? "INVOICE_SNO" : "ORDER_SNO", "GTxtOrderNo", 0, 2, false);
        }

        if (module != "SR" && module != "SQ")
        {
            dGrid.AddColumn(module is "SC" or "SO" ? "GTxtQuotNo" : "GTxtChallanNo", module is "SC" ? "QUOT_NO" : "CHALLAN_NO", "GTxtChallanNo", 0, 2, false);
            dGrid.AddColumn(module is "SC" or "SO" ? "GTxtQuotSno" : "GTxtChallanSno", module is "SC" ? "QUOT_SNO" : "CHALLAN_SNO", "GTxtChallanSno", 0, 2, false);
        }

        dGrid.AddColumn("GTxtConFraction", "CON_FACTOR", "GTxtConFraction", 0, 2, false);
        dGrid.AddColumn("IsTaxable", "IS_TAXABLE", "IsTaxable", 0, 2, false);
        dGrid.AddColumn("GTxtSBLedgerId", "SB_LEDGER", "GTxtSBLedgerId", 0, 2, false);
        dGrid.AddColumn("GTxtSRLedgerId", "SR_LEDGER", "GTxtSRLedgerId", 0, 2, false);
        dGrid.AddColumn("GTxtFreeQty", "FREE_QTY", "GTxtFreeQty", 0, 2, false);
        dGrid.AddColumn("GTxtStockFreeQty", "STOCK_FREE_QTY", "GTxtStockFreeQty", 0, 2, false);
        dGrid.AddColumn("GTxtFreeUnitId", "FREE_UOM_ID", "GTxtFreeUnitId", 0, 2, false);
        dGrid.AddColumn("GTxtExtraFreeQty", "EXTRA_FREE_QTY", "GTxtExtraFreeQty", 0, 2, false);
        dGrid.AddColumn("GTxtExtraStockQty", "EXTRA_STOCK_FREE_QTY", "GTxtExtraStockQty", 0, 2, false);
        dGrid.AddColumn("GTxtExtraFreeUnitId", "EXTRA_FREE_UOM_ID", "GTxtExtraFreeUnitId", 0, 2, false);
        dGrid.AddColumn("GTxtTaxPriceRate", "TAX_PRICE_RATE", "GTxtTaxPriceRate", 0, 2, false);
        dGrid.AddColumn("GTxtTaxGroupId", "TAX_GROUP_ID", "GTxtTaxGroupId", 0, 2, false);
        dGrid.AddColumn("GTxtVatAmount", "VAT_AMOUNT", "GTxtVatAmount", 0, 2, false);
        dGrid.AddColumn("GTxtTaxableAmount", "TAXABLE_AMOUNT", "GTxtTaxableAmount", 0, 2, false);
        dGrid.AddColumn("GTxtSerialNo", "SERIAL_NO", "GTxtSerialNo", 0, 2, false);
        dGrid.AddColumn("GTxtBatchNo", "BATCH_NO", "GTxtBatchNo", 0, 2, false);
        dGrid.AddColumn("GTxtExpiryDate", "EXPIRY_DATE", "GTxtExpiryDate", 0, 2, false);
        dGrid.AddColumn("GTxtMfgDate", "MFG_DATE", "GTxtMfgDate", 0, 2, false);
        return dGrid;
    }

    public DataGridView GetPointOfSalesInvoiceDesign(DataGridView dGrid, string module)
    {
        dGrid.ReadOnly = true;
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn(@"GTxtSNo", "SNO", "GTxtSNo", 65, 55, true);
        dGrid.AddColumn(@"GTxtProductId", "PRODUCT_ID", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn(@"GTxtShortName", "BARCODE", "GTxtShortName", 130, 110, true);
        dGrid.AddColumn(@"GTxtProduct", "PRODUCT", "GTxtProduct", 220, 220, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn(@"GTxtGodownId", "GODOWN_ID", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn(@"GTxtGodown", "GODOWN", "GTxtGodown", 100, 100, ObjGlobal.SalesGodownEnable);
        dGrid.AddColumn(@"GTxtAltQty", "ALT_QTY", "GTxtAltQty", 110, 100, true);
        dGrid.AddColumn(@"GTxtAltUOMId", "ALT_UOM_ID", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn(@"GTxtAltUOM", "ALT_UOM", "GTxtAltUOM", 90, 90, true);
        dGrid.AddColumn(@"GTxtQty", "QTY", "GTxtQty", 90, 90, true);
        dGrid.AddColumn(@"GTxtUOMId", "UOM_ID", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn(@"GTxtMRP", "MRP", "GTxtMRP", 0, 2, false);
        dGrid.AddColumn(@"GTxtUOM", "UOM", "GTxtUOM", 90, 90, true);
        dGrid.AddColumn(@"GTxtDisplayRate", "RATE", "GTxtDisplayRate", 90, 90, true);
        dGrid.AddColumn(@"GTxtValueRate", "RATE", "GTxtValueRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtDisplayAmount", "AMOUNT", "GTxtDisplayAmount", 90, 90, true);
        dGrid.AddColumn(@"GTxtDiscountRate", "DISCOUNT_RATE", "GTxtDiscountRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtPDiscount", "DISCOUNT", "GTxtPDiscount", 90, 90, ObjGlobal.SalesDiscountTermId > 0);
        dGrid.AddColumn(@"GTxtValueBDiscount", "B_DISCOUNT", "GTxtValueBDiscount", 0, 2, false);
        dGrid.AddColumn(@"GTxtValueServiceChange", "SERVICE_CHARGE", "GTxtValueServiceChange", 0, 2, false);
        dGrid.AddColumn(@"GTxtDisplayNetAmount", "NET_AMOUNT", "GTxtDisplayNetAmount", 125, 125, true);
        dGrid.AddColumn(@"GTxtValueNetAmount", "NET_AMOUNT", "GTxtValueNetAmount", 0, 2, false);
        dGrid.AddColumn(@"GTxtIsTaxable", "IS_TAXABLE", "GTxtIsTaxable", 0, 2, false);
        dGrid.AddColumn(@"GTxtTaxPriceRate", "TAX_PRICE_RATE", "GTxtTaxPriceRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtValueVatAmount", "VAT_AMOUNT", "GTxtValueVatAmount", 0, 2, false);
        dGrid.AddColumn(@"GTxtValueTaxableAmount", "TAXABLE_AMOUNT", "GTxtValueTaxableAmount", 0, 2, false);
        dGrid.AddColumn(@"GTxtValueExemptedAmount", "EXEMPTED_AMOUNT", "GTxtValueExemptedAmount", 0, 2, false);
        dGrid.AddColumn(@"GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn(@"GTxtFreeQty", "FREE_QTY", "GTxtFreeQty", 0, 2, false);
        dGrid.AddColumn(@"GTxtFreeUnitId", "FREE_UOM_ID", "GTxtFreeUnitId", 0, 2, false);
        return dGrid;
    }

    public DataGridView GetPointOfSalesDesign(DataGridView dGrid, string module)
    {
        dGrid.ReadOnly = true;
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn(@"GTxtSNo", "SNO", "GTxtSNo", 65, 30, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn(@"GTxtProductId", "PRODUCT_ID", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn(@"GTxtHsCode", "HS CODE", "GTxtHsCode", 130, 100, module.Equals("POS"));
        dGrid.AddColumn(@"GTxtShortName", "BARCODE", "GTxtShortName", 130, 100, module.Equals("POS"));
        dGrid.AddColumn(@"GTxtProduct", "PRODUCT", "GTxtProduct", 220, 220, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn(@"GTxtGodownId", "GODOWN_ID", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn(@"GTxtGodown", "GODOWN", "GTxtGodown", 150, 70, ObjGlobal.SalesGodownEnable);
        dGrid.AddColumn(@"GTxtAltQty", "ALT_QTY", "GTxtAltQty", 140, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtAltUOMId", "ALT_UOM_ID", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn(@"GTxtAltUOM", "UOM", "GTxtAltUOM", 90, 2, true);
        dGrid.AddColumn(@"GTxtQty", "QTY", "GTxtQty", 110, 80, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtUOMId", "UOM_ID", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn(@"GTxtMRP", "MRP", "GTxtMRP", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtUOM", "UOM", "GTxtUOM", 90, 80, true);
        dGrid.AddColumn(@"GTxtDisplayRate", "RATE", "GTxtDisplayRate", 90, 80, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueRate", "RATE", "GTxtValueRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtDisplayAmount", "AMOUNT", "GTxtDisplayAmount", 90, 90, true, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtDiscountRate", "DISCOUNT_RATE", "GTxtDiscountRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtPDiscount", "DISCOUNT", "GTxtPDiscount", 90, 80, ObjGlobal.SalesDiscountTermId > 0, DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.Programmatic, DataGridViewAutoSizeColumnMode.DisplayedCells);

        dGrid.AddColumn(@"GTxtValueBRate", "B_DISCOUNT_RATE", "GTxtValueBRate", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueBDiscount", "B_DISCOUNT", "GTxtValueBDiscount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtValueServiceRate", "SERVICE_RATE", "GTxtValueServiceChange", 0, 2, false);
        dGrid.AddColumn(@"GTxtValueServiceChange", "SERVICE_CHARGE", "GTxtValueServiceChange", 0, 2, false);

        dGrid.AddColumn(@"GTxtDisplayNetAmount", "NET AMOUNT", "GTxtDisplayNetAmount", 125, 125, true, DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.Programmatic, DataGridViewAutoSizeColumnMode.DisplayedCells);
        dGrid.AddColumn(@"GTxtValueNetAmount", "NET_AMOUNT", "GTxtValueNetAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtIsTaxable", "IS_TAXABLE", "GTxtIsTaxable", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtTaxPriceRate", "TAX_PRICE_RATE", "GTxtTaxPriceRate", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtValueVatAmount", "VAT_AMOUNT", "GTxtValueVatAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueTaxableAmount", "TAXABLE_AMOUNT", "GTxtValueTaxableAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueExemptedAmount", "EXEMPTED_AMOUNT", "GTxtValueExemptedAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn(@"GTxtFreeQty", "FREE_QTY", "GTxtFreeQty", 0, 2, false);
        dGrid.AddColumn(@"GTxtFreeUnitId", "FREE_UOM_ID", "GTxtFreeUnitId", 0, 2, false);
        dGrid.AddColumn(@"GTxtInvoiceNo", "InvoiceNo", "GTxtInvoiceNo", 0, 2, false);
        dGrid.AddColumn(@"GTxtInvoiceSNo", "InvoiceSNo", "GTxtInvoiceSNo", 0, 2, false);
        return dGrid;
    }

    public DataGridView GetRestroInvoiceDesign(DataGridView dGrid, string module)
    {
        dGrid.ReadOnly = true;
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn(@"GTxtSNo", "SNO", "GTxtSNo", 65, 30, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn(@"GTxtProductId", "PRODUCT_ID", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn(@"GTxtOrderTime", "Time", "GTxtOrderTime", 130, 100, true);
        dGrid.AddColumn(@"GTxtShortName", "CODE", "GTxtShortName", 130, 100, module.Equals("RESTRO"));
        dGrid.AddColumn(@"GTxtProduct", "PRODUCT", "GTxtProduct", 220, 220, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn(@"GTxtGodownId", "GODOWN_ID", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn(@"GTxtGodown", "GODOWN", "GTxtGodown", 150, 70, ObjGlobal.SalesGodownEnable);
        dGrid.AddColumn(@"GTxtAltQty", "ALT_QTY", "GTxtAltQty", 140, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtAltUOMId", "ALT_UOM_ID", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn(@"GTxtAltUOM", "UOM", "GTxtAltUOM", 90, 2, true);
        dGrid.AddColumn(@"GTxtQty", "QTY", "GTxtQty", 110, 80, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtUOMId", "UOM_ID", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn(@"GTxtMRP", "MRP", "GTxtMRP", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtUOM", "UOM", "GTxtUOM", 90, 80, true);
        dGrid.AddColumn(@"GTxtDisplayRate", "RATE", "GTxtDisplayRate", 90, 80, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueRate", "RATE", "GTxtValueRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtDisplayAmount", "AMOUNT", "GTxtDisplayAmount", 90, 90, true, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtDiscountRate", "DISCOUNT_RATE", "GTxtDiscountRate", 0, 2, false);
        dGrid.AddColumn(@"GTxtPDiscount", "DISCOUNT", "GTxtPDiscount", 90, 80, ObjGlobal.SalesDiscountTermId > 0, DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.Programmatic, DataGridViewAutoSizeColumnMode.DisplayedCells);
        dGrid.AddColumn(@"GTxtValueBDiscount", "B_DISCOUNT", "GTxtValueBDiscount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueServiceChange", "SERVICE_CHARGE", "GTxtValueServiceChange", 0, 2, false);

        dGrid.AddColumn(@"GTxtDisplayNetAmount", "NET AMOUNT", "GTxtDisplayNetAmount", 125, 125, true, DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.Programmatic, DataGridViewAutoSizeColumnMode.DisplayedCells);
        dGrid.AddColumn(@"GTxtValueNetAmount", "NET_AMOUNT", "GTxtValueNetAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtIsTaxable", "IS_TAXABLE", "GTxtIsTaxable", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtTaxPriceRate", "TAX_PRICE_RATE", "GTxtTaxPriceRate", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtValueVatAmount", "VAT_AMOUNT", "GTxtValueVatAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueTaxableAmount", "TAXABLE_AMOUNT", "GTxtValueTaxableAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn(@"GTxtValueExemptedAmount", "EXEMPTED_AMOUNT", "GTxtValueExemptedAmount", 0, 2, false, DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn(@"GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn(@"GTxtFreeQty", "FREE_QTY", "GTxtFreeQty", 0, 2, false);
        dGrid.AddColumn(@"GTxtFreeUnitId", "FREE_UOM_ID", "GTxtFreeUnitId", 0, 2, false);
        dGrid.AddColumn(@"GTxtInvoiceNo", "InvoiceNo", "GTxtInvoiceNo", 0, 2, false);
        dGrid.AddColumn(@"GTxtInvoiceSNo", "InvoiceSNo", "GTxtInvoiceSNo", 0, 2, false);
        return dGrid;
    }
}