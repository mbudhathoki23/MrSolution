using MrDAL.Reports.ViewModule;
using System.Data;

namespace MrDAL.Reports.Interface;

public interface IStockReport
{
    VmStockReports GetReports { get; set; }

    DataTable GetMasterProductList();

    DataTable GetProductOpeningOnly();

    DataTable GetProductOpeningLedgerWise();

    DataTable GetStockLedgerSummaryReport();

    DataTable GetStockLedgerDetailsReport();

    DataTable GetClosingStockValue();

    DataTable GetClosingStockValueLedgerWise();

    DataTable GetBillOfMaterialsReports();

    DataTable GetProfitabilityReports();
}