using MrDAL.Core.Extensions;
using MrDAL.DataEntry.Interface;
using System.Windows.Forms;

namespace MrDAL.DataEntry.Design;

public class GetStockDesign : IStockEntryDesign
{
    // BILL OF MATERIALS GRID DESIGN
    public DataGridView GetBillOfMaterialsEntryDesign(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSNo", "SNo.", "GTxtSNo", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORT_NAME", "GTxtShortName", 90, 90, false);
        dGrid.AddColumn("GTxtProduct", "RAW_PRODUCT", "GTxtProduct", 200, 150, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtCostCenterId", "CostCenterId", "GTxtCostCenterId", 50, 2, false);
        dGrid.AddColumn("GTxtCostCenter", "COST_CENTER", "GTxtCostCenter", 90, 90, true);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodownId", 90, 2, false);
        dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltUOMId", "AltUnitId", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtAltUOM", "UOM", "GTxtAltUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUOMId", "UnitId", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtUOM", "UOM", "GTxtUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn("GTxtOrderNo", "OrderNo", "GTxtOrderNo", 0, 2, false);
        dGrid.AddColumn("GTxtOrderSNo", "OrderSno", "GTxtOrderSNo", 0, 2, false);
        return dGrid;
    }

    // PRODUCT STOCK ADJUSTMENT GRID DESIGN
    public DataGridView GetStockAdjustmentEntryDesign(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSNo", "SNo.", "GTxtSNo", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORT_NAME", "GTxtShortName", 90, 90, false);
        dGrid.AddColumn("GTxtProduct", "RAW_PRODUCT", "GTxtProduct", 200, 150, true,
            DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodownId", 90, 2, false);
        dGrid.AddColumn("GTxtType", "TYPE", "GTxtType", 65, 2, true);
        dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltUOMId", "AltUnitId", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtAltUOM", "UOM", "GTxtAltUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUOMId", "UnitId", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtUOM", "UOM", "GTxtUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn("GTxtConvRatio", "CONV_RATIO", "GTxtConvRatio", 0, 2, false);
        return dGrid;
    }

    // PHYSICAL STOCK ADJUSTMENT GRID DESIGN
    public DataGridView GetPhysicalStockEntry(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSNo", "SNo.", "GTxtSNo", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORTNAME", "GTxtShortName", 90, 90, false);
        dGrid.AddColumn("GTxtProduct", "RAW_PRODUCT", "GTxtProduct", 200, 150, true,
            DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodownId", 90, 2, false);
        dGrid.AddColumn("GTxtType", "TYPE", "GTxtType", 65, 2, false);

        //dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        //dGrid.AddColumn("GTxtAltUOMId", "AltUnitId", "GTxtAltUOMId", 0, 2, false);
        //dGrid.AddColumn("GTxtAltUOM", "UOM", "GTxtAltUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);

        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUOMId", "UnitId", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtUOM", "UOM", "GTxtUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtActualQty", "QTY", "GTxtActualQty", 90, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtActualAmount", "QTY", "GTxtActualAmount", 90, 2, false,
            DataGridViewContentAlignment.MiddleRight);

        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        dGrid.AddColumn("GTxtConvRatio", "CONV_RATIO", "GTxtConvRatio", 0, 2, false);
        return dGrid;
    }

    // PRODUCTION STOCK ENTRY GRID DESIGN
    public DataGridView GetProductionEntryDesign(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSno", "SNo.", "GTxtSNo", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORTNAME", "GTxtShortName", 90, 90, false);
        dGrid.AddColumn("GTxtProduct", "RAW_PRODUCT", "GTxtProduct", 200, 150, true,
            DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtCostCenterId", "CostCenterId", "GTxtCostCenterId", 50, 2, false);
        dGrid.AddColumn("GTxtCostCenter", "COST_CENTER", "GTxtCostCenter", 90, 90, false);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodownId", 90, 2, false);
        dGrid.AddColumn("GTxtBOMVno", "BomVNo", "GTxtBOMVno", 0, 2, false);
        dGrid.AddColumn("GTxtBOMSNo", "BomSno", "GTxtBOMSNo", 0, 2, false);
        dGrid.AddColumn("GTxtIssueVno", "IssueVNo", "GTxtIssueVno", 0, 2, false);
        dGrid.AddColumn("GTxtIssueSNo", "IssueSNo", "GTxtIssueSNo", 0, 2, false);
        dGrid.AddColumn("GTxtOrderNo", "OrderNo", "GTxtOrderNo", 0, 2, false);
        dGrid.AddColumn("GTxtOrderSNo", "OrderSno", "GTxtOrderSNo", 0, 2, false);
        dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltUOMId", "AltUnitId", "GTxtAltUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtAltUOM", "UOM", "GTxtAltUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtActualQty", "ActualQty", "GTxtActualQty", 0, 2, false);
        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUOMId", "UnitId", "GTxtUOMId", 0, 2, false);
        dGrid.AddColumn("GTxtUOM", "UOM", "GTxtUOM", 75, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtNarration", "NARRATION", "GTxtNarration", 0, 2, false);
        return dGrid;
    }

    // PRODUCT OPENING GRID CONTROL
    public DataGridView GetProductOpeningEntry(DataGridView dGrid)
    {
        if (dGrid.ColumnCount > 0)
        {
            dGrid.DataSource = null;
            dGrid.Columns.Clear();
        }

        dGrid.AddColumn("GTxtSno", "SNo.", "GTxtSno", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtProductId", "ProductId", "GTxtProductId", 0, 2, false);
        dGrid.AddColumn("GTxtShortName", "SHORTNAME", "GTxtShortName", 110, 2, false);
        dGrid.AddColumn("GTxtProduct", "PRODUCT", "GTxtProduct", 200, 2, true, DataGridViewAutoSizeColumnMode.Fill);
        dGrid.AddColumn("GTxtGodownId", "GodownId", "GTxtGodownId", 0, 2, false);
        dGrid.AddColumn("GTxtGodown", "GODOWN", "GTxtGodown", 120, 2, false);
        dGrid.AddColumn("GTxtAltQty", "ALT_QTY", "GTxtAltQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAltUnitId", "AltUnitId", "GTxtAltUnitId", 0, 2, false);
        dGrid.AddColumn("GTxtAltUnit", "UOM", "GTxtAltUnit", 65, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtQty", "QTY", "GTxtQty", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtUnitId", "UnitId", "GTxtUnitId", 0, 2, false);
        dGrid.AddColumn("GTxtUnit", "UOM", "GTxtUnit", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        dGrid.AddColumn("GTxtRate", "RATE", "GTxtRate", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtAmount", "AMOUNT", "GTxtAmount", 90, 2, true, DataGridViewContentAlignment.MiddleRight);
        dGrid.AddColumn("GTxtLocalAmount", "AMOUNT", "GTxtLocalAmount", 0, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        return dGrid;
    }
}