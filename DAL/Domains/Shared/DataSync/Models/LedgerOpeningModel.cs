using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class LedgerOpeningModel : BaseSyncData
{
    public int Opening_Id { get; set; }
    public string Module { get; set; }
    public int Serial_No { get; set; }
    public string Voucher_No { get; set; }
    public DateTime OP_Date { get; set; }
    public string OP_Miti { get; set; }
    public long Ledger_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Agent_Id { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int Currency_Id { get; set; }
    public decimal Currency_Rate { get; set; }
    public decimal Debit { get; set; }
    public decimal LocalDebit { get; set; }
    public decimal Credit { get; set; }
    public decimal LocalCredit { get; set; }
    public string? Narration { get; set; }
    public string? Remarks { get; set; }
    public string Enter_By { get; set; }
    public DateTime Enter_Date { get; set; }
    public string? Reconcile_By { get; set; }
    public DateTime? Reconcile_Date { get; set; }
    public int Branch_Id { get; set; }
    public int? Company_Id { get; set; }
    public int FiscalYearId { get; set; }
    public bool IsReverse { get; set; }
    public string? CancelRemarks { get; set; }
    public string? CancelBy { get; set; }
    public DateTime? CancelDate { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}