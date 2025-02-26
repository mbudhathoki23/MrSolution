using System.Windows.Forms;

namespace MrDAL.Reports.Interface;

public interface IStockReportDesign
{
    DataGridView GetStockLedgerSummaryDesign(DataGridView rGrid);

    DataGridView GetStockLedgerDetailsDesign(DataGridView rGrid);

    DataGridView GetMasterProductListDesign(DataGridView rGrid);

    DataGridView GetStockValuationDesign(DataGridView rGrid, bool inclueVat = false);

    DataGridView GetBillOfMaterialsDesign(DataGridView rGrid);

    DataGridView GetProfitabilityReportDesign(DataGridView rGrid);
}