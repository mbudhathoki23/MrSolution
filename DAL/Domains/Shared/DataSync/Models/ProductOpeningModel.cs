using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class ProductOpeningModel : BaseSyncData
{
    public int OpeningId { get; set; }
    public string Voucher_No { get; set; }
    public int Serial_No { get; set; }
    public DateTime OP_Date { get; set; }
    public string OP_Miti { get; set; }
    public long Product_Id { get; set; }
    public int? Godown_Id { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int Currency_Id { get; set; }
    public decimal Currency_Rate { get; set; }
    public decimal AltQty { get; set; }
    public int? AltUnit { get; set; }
    public decimal Qty { get; set; }
    public int? QtyUnit { get; set; }
    public decimal Rate { get; set; }
    public decimal LocalRate { get; set; }
    public decimal Amount { get; set; }
    public decimal LocalAmount { get; set; }
    public bool IsReverse { get; set; }
    public string? CancelRemarks { get; set; }
    public string? CancelBy { get; set; }
    public DateTime? CancelDate { get; set; }
    public string? Remarks { get; set; }
    public string Enter_By { get; set; }
    public DateTime Enter_Date { get; set; }
    public string? Reconcile_By { get; set; }
    public DateTime? Reconcile_Date { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}