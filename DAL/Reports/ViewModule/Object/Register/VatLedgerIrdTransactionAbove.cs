using MrDAL.Core.Extensions;

namespace MrDAL.Reports.ViewModule.Object.Register;

public class VatLedgerIrdTransactionAbove
{
    public long LedgerId { get; set; }
    public string Ledger { get; set; }
    public string PanNo { get; set; }
    public string TradeNameType { get; set; }
    public string PurchaseSales { get; set; }
    public decimal? Taxable { get; set; }
    public decimal? TaxFree { get; set; }
    public string TaxableString => Taxable.GetDecimalString();
    public string TaxFreeString => TaxFree.GetDecimalString();
    public int IsGroup { get; set; } = 0;
}