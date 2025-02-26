using MrDAL.Core.Extensions;

namespace MrDAL.Reports.ViewModule.Object.Inventory;

public class StockLedgerSummaryReport
{
    public string SNo { get; set; }
    public long ProductId { get; set; }
    public string VoucherNo { get; set; }
    public string PName { get; set; }
    public int? PGrpId { get; set; }
    public string GroupCode { get; set; }
    public string GrpName { get; set; }
    public int? PSubGrpId { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGrpName { get; set; }
    public string AltUnit { get; set; }
    public string Unit { get; set; }

    public decimal OpeningAltStockQty { get; set; }
    public decimal OpeningStockQty { get; set; }
    public decimal OpeningStockValue { get; set; }
    public decimal ReceiptAltStockQty { get; set; }
    public decimal ReceiptStockQty { get; set; }
    public decimal ReceiptStockValue { get; set; }
    public decimal IssueAltStockQty { get; set; }
    public decimal IssueStockQty { get; set; }
    public decimal IssueStockValue { get; set; }
    private decimal SalesStockValue { get; set; }
    private decimal BalanceAltStockQty => OpeningAltStockQty + ReceiptAltStockQty - IssueAltStockQty;
    private decimal BalanceStockQty => OpeningStockQty + ReceiptStockQty - IssueStockQty;

    private decimal BalanceStockValue =>
        BalanceStockQty > 0 ? OpeningStockValue + ReceiptStockValue - IssueStockValue : 0;

    private decimal BalanceStockRate => BalanceStockQty > 0 ? BalanceStockValue / BalanceStockQty : 0;
    public string StringOpeningAltStockQty => OpeningAltStockQty.GetDecimalString();
    public string StringOpeningStockQty => OpeningStockQty.GetDecimalString();
    public string StringOpeningStockValue => OpeningStockValue.GetDecimalString();
    public string StringReceiptAltStockQty => ReceiptAltStockQty.GetDecimalString();
    public string StringReceiptStockQty => ReceiptStockQty.GetDecimalString();
    public string StringReceiptStockValue => ReceiptStockValue.GetDecimalString();
    public string StringIssueAltStockQty => IssueAltStockQty.GetDecimalString();
    public string StringIssueStockQty => IssueStockQty.GetDecimalString();
    public string StringIssueStockValue => IssueStockValue.GetDecimalString();
    public string StringSalesStockValue => SalesStockValue.GetDecimalString();
    public string StringBalanceAltStockQty => BalanceAltStockQty.GetDecimalString();
    public string StringBalanceStockQty => BalanceStockQty.GetDecimalString();
    public string StringBalanceStockRate => BalanceStockRate.GetDecimalString();
    public string StringBalanceStockValue => BalanceStockValue.GetDecimalString();
    public string Ledger { get; set; }
    public int IsGroup { get; set; } = 0;
}