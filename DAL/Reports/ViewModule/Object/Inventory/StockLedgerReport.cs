using MrDAL.Core.Extensions;

namespace MrDAL.Reports.ViewModule.Object.Inventory;

public class StockLedgerReport
{
    public string EntryType { get; set; }
    public string VoucherNo { get; set; }
    public string VoucherDate { get; set; }
    public string VoucherMiti { get; set; }
    public string Module { get; set; }
    public long ProductId { get; set; }
    public string ShortName { get; set; }
    public string PName { get; set; }
    public int? PGrpId { get; set; }
    public string GroupCode { get; set; }
    public string GrpName { get; set; }
    public int? PSubGrpId { get; set; }
    public string SubGroupCode { get; set; }
    public string SubGrpName { get; set; }

    public decimal OpeningAltStockQty { get; set; }
    public decimal OpeningStockQty { get; set; }
    public decimal OpeningStockValue { get; set; }
    public decimal ReceiptAltStockQty { get; set; }
    public decimal ReceiptStockQty { get; set; }
    public decimal ReceiptStockValue { get; set; }
    public decimal IssueAltStockQty { get; set; }
    public decimal IssueStockQty { get; set; }
    public decimal IssueStockValue { get; set; }

    public decimal BalanceAltStockQty { get; set; }
    public decimal BalanceStockQty { get; set; }
    public decimal BalanceStockValue { get; set; }
    public decimal BalanceStockRate { get; set; }
    public decimal PTax { get; set; }

    public string StringOpeningAltStockQty => OpeningAltStockQty.GetDecimalString();
    public string StringOpeningStockQty => OpeningStockQty.GetDecimalString();
    public string StringOpeningStockValue => OpeningStockValue.GetDecimalString();
    public string StringReceiptAltStockQty => ReceiptAltStockQty.GetDecimalString();
    public string StringReceiptStockQty => ReceiptStockQty.GetDecimalString();
    public string StringReceiptStockValue => ReceiptStockValue.GetDecimalString();
    public string StringIssueAltStockQty => IssueAltStockQty.GetDecimalString();
    public string StringIssueStockQty => IssueStockQty.GetDecimalString();
    public string StringIssueStockValue => IssueStockValue.GetDecimalString();
    public string StringBalanceAltStockQty => BalanceAltStockQty.GetDecimalString();
    public string StringBalanceStockQty => BalanceStockQty.GetDecimalString();
    public string StringBalanceStockRate => BalanceStockRate.GetDecimalString();
    public string StringBalanceStockValue => BalanceStockValue.GetDecimalString();

    public string Ledger { get; set; }
    public int IsGroup { get; set; } = 0;
}