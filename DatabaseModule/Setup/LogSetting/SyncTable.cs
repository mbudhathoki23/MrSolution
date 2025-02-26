using System;

namespace DatabaseModule.Setup.LogSetting;

public class SyncTable
{
    public int SyncId { get; set; }
    public bool? IsBranch { get; set; }
    public bool? IsGeneralLedger { get; set; }
    public bool? IsTableId { get; set; }
    public bool? IsArea { get; set; }
    public bool? IsBillingTerm { get; set; }
    public bool? IsAgent { get; set; }
    public bool? IsProduct { get; set; }
    public bool? IsCostCenter { get; set; }
    public bool? IsMember { get; set; }
    public bool? IsCashBank { get; set; }
    public bool? IsJournalVoucher { get; set; }
    public bool? IsNotesRegister { get; set; }
    public bool? IsPDCVoucher { get; set; }
    public bool? IsLedgerOpening { get; set; }
    public bool? IsProductOpening { get; set; }
    public bool? IsSalesQuotation { get; set; }
    public bool? IsSalesOrder { get; set; }
    public bool? IsSalesChallan { get; set; }
    public bool? IsSalesInvoice { get; set; }
    public bool? IsSalesReturn { get; set; }
    public bool? IsSalesAdditional { get; set; }
    public bool? IsPurchaseIndent { get; set; }
    public bool? IsPurchaseOrder { get; set; }
    public bool? IsPurchaseChallan { get; set; }
    public bool? IsPurchaseInvoice { get; set; }
    public bool? IsPurchaseReturn { get; set; }
    public bool? IsPurchaseAdditional { get; set; }
    public bool? IsStockAdjustment { get; set; }
    public string SyncAPI { get; set; }
    public string SyncOriginId { get; set; }
    public Guid? ApiKey { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime? EnterDate { get; set; }
}