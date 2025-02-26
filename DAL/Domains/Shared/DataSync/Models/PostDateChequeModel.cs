using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class PostDateChequeModel : BaseSyncData
{
    public int PDCId { get; set; }
    public string VoucherNo { get; set; }
    public DateTime VoucherDate { get; set; }
    public DateTime VoucherTime { get; set; }
    public string VoucherMiti { get; set; }
    public long BankLedgerId { get; set; }
    public string VoucherType { get; set; }
    public string Status { get; set; }
    public string BankName { get; set; }
    public string BranchName { get; set; }
    public string? ChequeNo { get; set; }
    public DateTime? ChqDate { get; set; }
    public string? ChqMiti { get; set; }
    public string? DrawOn { get; set; }
    public decimal Amount { get; set; }
    public long LedgerId { get; set; }
    public int? SubLedgerId { get; set; }
    public int? AgentId { get; set; }
    public string? Remarks { get; set; }
    public string? DepositedBy { get; set; }
    public DateTime? DepositeDate { get; set; }
    public bool IsReverse { get; set; }
    public string? CancelReason { get; set; }
    public string? CancelBy { get; set; }
    public DateTime? CancelDate { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
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
}