using System;

namespace MrDAL.Reports.ViewModule.Object.Finance;

public class VmPartyLedgerReport
{
    public string EntryType { get; set; }
    public string VoucherNo { get; set; }
    public string VoucherMiti { get; set; }
    public DateTime? VoucherDate { get; set; }
    public long LedgerId { get; set; }
    public string Ledger { get; set; }
    public decimal DebitAmount { get; set; }
    public decimal CreditAmount { get; set; }
    public decimal? BalanceAmount { get; set; }
}