namespace DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;

public class CB_Details
{
    public string Voucher_No { get; set; }
    public int SNo { get; set; }
    public long? CBLedgerId { get; set; }
    public long Ledger_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Agent_Id { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int? CurrencyId { get; set; }
    public decimal CurrencyRate { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal LocalDebit { get; set; }
    public decimal LocalCredit { get; set; }
    public decimal Tbl_Amount { get; set; }
    public decimal V_Amount { get; set; }
    public string Narration { get; set; }
    public string Party_No { get; set; }
    public System.DateTime? Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public long? VatLedger_Id { get; set; }
    public int? PanNo { get; set; }
    public bool? Vat_Reg { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}