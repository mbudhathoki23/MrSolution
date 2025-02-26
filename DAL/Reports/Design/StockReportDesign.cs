using MrDAL.Core.Extensions;
using MrDAL.Reports.Interface;
using System.Windows.Forms;

namespace MrDAL.Reports.Design;

public class StockReportDesign : IStockReportDesign
{
    public DataGridView GetMasterProductListDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount is 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtSerialNo", "SNo.", "dtSNo", 50, 50, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtProductId", "ProductId", "dtProductId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", "SHORTNAME", "dtShortName", 120, 90, true);
        rGrid.AddColumn("GTxtProduct", "DESCRIPTION", "dtProduct", 375, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAltUOM", "ALT_UOM", "dtAltUnit", 90, 90, true);
        rGrid.AddColumn("GTxtUOM", "UOM", "dtUnit", 90, 90, true);
        rGrid.AddColumn("GTxtMRP", "MRP", "dtMrpRate", 90, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBuyRate", "BUY_RATE", "dtBuyRate", 90, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSalesRate", "SALES_RATE", "dtSalesRate", 90, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtStatus", "STATUS", "dtStatus", 90, 90, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtType", "TYPE", "dtType", 120, 90, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtCategory", "CATEGORY", "dtCategory", 150, 90, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtGroup", "GROUP", "dtGrpName", 150, 90, true);
        rGrid.AddColumn("GTxtSubGroup", "SUB_GROUP", "dtSubGrpName", 150, 90, true);
        rGrid.AddColumn("GTxtTaxRate", "VAT_RATE", "dtPTax", 90, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxable", "TAX_TYPE", "dtTaxable", 120, 90, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtMinQty", "MIN_QTY", "dtPMin", 75, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtMaxQty", "MAX_QTY", "dtPMax", 75, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSalesLedger", "SALES_LEDGER", "dtSalesLedger", 150, 90, true);
        rGrid.AddColumn("GTxtSalesReturnLedger", "SALES_RETURN_LEDGER", "dtSalesReturnLedger", 150, 90, true);
        rGrid.AddColumn("GTxtPurchaseLedger", "PURCHASE_LEDGER", "dtPurchaseLedger", 150, 90, true);
        rGrid.AddColumn("GTxtPurchaseReturnLedger", "PURCHASE_RETURN_LEDGER", "dtPurchaseReturnLedger", 150, 90, true);
        rGrid.AddColumn("GTxtPLOpeningLedger", "OPENING_LEDGER", "dtOpeningLedger", 150, 90, true);
        rGrid.AddColumn("GTxtPlClosingLedger", "CLOSING_LEDGER", "dtClosingLedger", 150, 90, true);
        rGrid.AddColumn("GTxtBsClosingLedger", "STOCK_IN_HAND_LEDGER", "dtStockInHandLedger", 150, 90, true);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetStockLedgerSummaryDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtSno", "SNO", "Sno", 65, 2, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtProductId", "ProductId", "PID", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", "SHORTNAME", "PShortName", 150, 2, true);
        rGrid.AddColumn("GTxtProduct", "PARTICULARS", "PName", 350, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAltUnit", "ALT_UOM", "AltUom", 110, 2, false);
        rGrid.AddColumn("GTxtUnit", "UOM", "Uom", 110, 2, true);
        rGrid.AddColumn("GTxtOpeningAltQty", "OP_ALT_QTY", "OpeningAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtOpeningQty", "OP_QTY", "OpeningQty", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtOpeningValue", "OP_VALUE", "OpeningValue", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtInAltStock", "IN_ALT_QTY", "ReceiptAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtInStock", "IN_QTY", "ReceiptQty", 110, 2, true, DataGridViewContentAlignment.MiddleRight,
            DataGridViewColumnSortMode.NotSortable, DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtInValue", "IN_VALUE", "ReceiptValue", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtIssueAltQty", "ISSUE_ALT_QTY", "IssueAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtIssueQty", "ISSUE_QTY", "IssueQty", 150, 2, true, DataGridViewContentAlignment.MiddleRight,
            DataGridViewColumnSortMode.NotSortable, DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtIssueValue", "ISSUE_VALUE", "IssueValue", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtSalesValue", "SALES_VALUE", "SalesValue", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtBalanceAltQty", "BALANCE_ALT_QTY", "BalanceAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtBalanceQty", "BALANCE_QTY", "BalanceQty", 150, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtBalanceRate", "RATE", "BalanceRate", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtBalanceAmount", "AMOUNT", "BalanceValue", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetStockLedgerDetailsDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtProductId", "ProductId", "ProductId", 0, 2, false);
        rGrid.AddColumn("GTxtDate", "DATE", "VoucherDate", 120, 2, true);
        rGrid.AddColumn("GTxtMiti", "MITI", "VoucherMiti", 120, 2, true);
        rGrid.AddColumn("GTxtShortName", "VOUCHER_NO", "VoucherNo", 160, 150, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtProduct", "PARTICULARS", "Ledger", 350, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtOpeningAltQty", "ALT_QTY", "OpeningAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningQty", "OB_QTY", "OpeningQty", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtOpeningValue", "OB_VALUE", "OpeningVal", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReceiptAltQty", "ATL_RECEIPT", "ReceiptAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReceiptQty", "RECEIPT", "ReceiptQty", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReceiptValue", "VALUE", "ReceiptValue", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtIssueAltQty", "ALT_ISSUE", "IssueAltQty", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtIssueQty", "ISSUE", "IssueQty", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtIssueValue", "VALUE", "IssueValue", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceAltQty", "ALT_BALANCE", "BalanceAlt", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceQty", "BALANCE", "BalanceQty", 130, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceRate", "RATE", "BalanceRate", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceValue", "AMOUNT", "BalanceValue", 130, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtModule", "MODULE", "Module", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVoucherNo", "VoucherNo", "VoucherNo", 0, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        return rGrid;
    }

    public DataGridView GetStockValuationDesign(DataGridView rGrid, bool isIncludeVat = false)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtSno", "SNO", "Sno", 65, 2, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtProductId", "ProductId", "ProductId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", "SHORTNAME", "PShortName", 150, 2, true,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtProduct", "PARTICULARS", "PName", 350, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtStockAltQty", "ALT_QTY", "StockAltQty", 110, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAltUnit", "ALT_UOM", "AltUom", 110, 2, false);
        rGrid.AddColumn("GTxtStockQty", "QTY", "StockQty", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtUnit", "UOM", "Uom", 110, 2, true, DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtRate", "RATE", isIncludeVat ? "StockRateWithVat" : "StockRate", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", "AMOUNT", isIncludeVat ? "StockValueWithVat" : "StockVal", 110, 2, true,
            DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetBillOfMaterialsDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", "DATE", "dt_Date", 120, 2, true);
        rGrid.AddColumn("GTxtMiti", "MITI", "dt_Miti", 120, 2, true);
        rGrid.AddColumn("GTxtVoucherNo", "VOUCHER_NO", "dt_VoucherNo", 150, 2, true);
        rGrid.AddColumn("GTxtProductId", "ProductId", "dt_ProductId", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", "SHORTNAME", "dt_ShortName", 160, 150, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtProduct", "PARTICULARS", "dt_ProductName", 350, 300, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtQty", "QTY", "dt_Qty", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtUnit", "UOM", "dt_Unit", 65, 2, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtBalance", "AMOUNT", "dt_Balance", 130, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSalesRate", "SALES_RATE", "dt_SalesRate", 130, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCostRatio", "COST_RATIO", "dt_CostRatio", 130, 2, false,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        return rGrid;
    }

    public DataGridView GetProfitabilityReportDesign(DataGridView rGrid)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtProductId", "ProductId", "Product_Id", 0, 2, false);
        rGrid.AddColumn("GTxtShortName", "SHORTNAME", "PShortName", 160, 150, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtProduct", "PARTICULARS", "PName", 350, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtQty", "QTY", "Qty", 110, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtUnit", "UOM", "UOM", 65, 2, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtPurchaseValue", "COGS", "Cogs", 130, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSalesValue", "REVENUE", "SalesAmount", 130, 2, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtProfit", "PROFIT", "Profit", 130, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtProfitRatio", "MARGIN", "Ratio", 130, 2, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        return rGrid;
    }
}