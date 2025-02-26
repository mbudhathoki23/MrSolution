using System;
using System.Collections.Generic;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class JournalVoucherModel : BaseSyncData
{
    public string VoucherMode { get; set; }
    public string Voucher_No { get; set; }
    public DateTime Voucher_Date { get; set; }
    public string Voucher_Miti { get; set; }
    public DateTime Voucher_Time { get; set; }
    public string? Ref_VNo { get; set; }
    public DateTime? Ref_VDate { get; set; }
    public int Currency_Id { get; set; }
    public decimal Currency_Rate { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public decimal N_Amount { get; set; }
    public string? Remarks { get; set; }
    public string Action_Type { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public bool Audit_Lock { get; set; }
    public bool IsReverse { get; set; }
    public string? CancelBy { get; set; }
    public DateTime? CancelDate { get; set; }
    public string? CancelReason { get; set; }
    public string? ReconcileBy { get; set; }
    public DateTime? ReconcileDate { get; set; }
    public string? ClearingBy { get; set; }
    public DateTime? ClearingDate { get; set; }
    public int? PrintValue { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public byte[]? PAttachment1 { get; set; }
    public byte[]? PAttachment2 { get; set; }
    public byte[]? PAttachment3 { get; set; }
    public byte[]? PAttachment4 { get; set; }
    public byte[]? PAttachment5 { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public string? CancelRemarks { get; set; }
    public List<JournalVoucherDetailsModel> JournalVoucherDetailsModels { get; set; }
}