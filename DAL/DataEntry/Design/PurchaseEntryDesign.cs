using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Design;

public class PurchaseEntryDesign : IPurchaseDesign
{
    DataGridView IPurchaseDesign.GetPurchaseEntryDesign(DataGridView dGrid, string module)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSNo", "SNO", "GTxtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORTNAME", "GTxtShortName", 110, 2, false);
        dGrid.AddColumn("GTxtProduct", "PRODUCT", "GTxtProduct", 425, 250, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodown", 110, 2, false);
        dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 75, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltUOMId", "AltUnitId", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtAltUOM", "UOM", "GTxtAltUOM", 65, 2, true);
        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 75, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUOMId", "UnitId", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtUOM", "UOM", "GTxtUOM", 65, 2, true);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtTermAmount", "TERM", "GTxtTermAmount", 70, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNetAmount", "NET_AMOUNT", "GTxtNetAmount", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn("IsTaxable", "IsTaxable", "IsTaxable", 0, 2, false);
        dGrid.AddColumn("GTxtPBLedgerId", "PurchaseLedgerId", "GTxtPBLedgerId", 0, 2, false);
        dGrid.AddColumn("GTxtPRLedgerId", "PurchaseReturnLedgerId", "GTxtPRLedgerId", 0, 2, false);

        if (module is "PC" or "PB")
        {
            dGrid.AddColumn("GTxtOrderNo", "OrderNo", "GTxtOrderNo", 0, 2, false);
            dGrid.AddColumn("GTxtOrderSno", "OrderSNo", "GTxtOrderSno", 0, 2, false);
        }

        if (module != "PIN")
        {
            var headerText = module switch
            {
                "PR" => "INVOICE_NO",
                "PB" => "CHALLAN_NO",
                _ => "INDENT_NO"
            };
            var name = module switch
            {
                "PR" => "GTxtInvoiceNo",
                "PB" => "GTxtChallanNo",
                _ => "GTxtIndentNo"
            };

            dGrid.AddColumn(name, headerText, name, 0, 2, false);

            headerText = module switch
            {
                "PR" => "INVOICE_SNO",
                "PB" => "CHALLAN_SNO",
                _ => "INDENT_SNO"
            };
            name = module switch
            {
                "PR" => "GTxtInvoiceSno",
                "PB" => "GTxtChallanSno",
                _ => "GTxtIndentSno"
            };
            dGrid.AddColumn(name, headerText, name, 0, 2, false);
        }

        dGrid.AddColumn("GTxtConFraction", "Con_Factor", "GTxtConFraction", 0, 2, false);
        dGrid.AddColumn("GTxtAltStockQty", "AltStockQty", "GTxtAltStockQty", 0, 2, false);
        dGrid.AddColumn("GTxtStockQty", "StockQty", "GTxtStockQty", 0, 2, false);
        dGrid.AddColumn("GTxtFreeUnitId", "FreeUnitId", "GTxtFreeUnitId", 0, 2, false);
        dGrid.AddColumn("GTxtFreeQty", "FreeQty", "GTxtFreeQty", 0, 2, false);
        dGrid.AddColumn("GTxtStockFreeQty", "StockFreeQty", "GTxtStockFreeQty", 0, 2, false);
        dGrid.AddColumn("GTxtExtraFreeUnitId", "ExtraFreeUnitId", "GTxtExtraFreeUnitId", 0, 2, false);
        dGrid.AddColumn("GTxtExtraFreeQty", "ExtraFreeQty", "GTxtExtraFreeQty", 0, 2, false);
        dGrid.AddColumn("GTxtTaxPriceRate", "TaxPriceRate", "GTxtTaxPriceRate", 0, 2, false);
        dGrid.AddColumn("GTxtTaxGroupId", "TaxGroupId", "GTxtTaxGroupId", 0, 2, false);

        dGrid.AddColumn("GTxtVatAmount", "VatAmount", "GTxtVatAmount", 0, 2, false);
        dGrid.AddColumn("GTxtTaxableAmount", "TaxableAmount", "GTxtTaxableAmount", 0, 2, false);

        dGrid.AddColumn("GTxtBatchNo", "BatchNo", "GTxtBatchNo", 0, 2, false);
        dGrid.AddColumn("GTxtMFGDate", "MFGDate", "GTxtMFGDate", 0, 2, false);
        dGrid.AddColumn("GMskEXPDate", "EXPDate", "GMskEXPDate", 0, 2, false);
        return dGrid;
    }
}