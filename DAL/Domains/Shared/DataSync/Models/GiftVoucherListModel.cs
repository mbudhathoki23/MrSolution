using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class GiftVoucherListModel : BaseSyncData
{
    public int VoucherId { get; set; }
    public long CardNo { get; set; }
    public DateTime ExpireDate { get; set; }
    public string Description { get; set; }
    public string VoucherType { get; set; }
    public decimal IssueAmount { get; set; }
    public bool IsUsed { get; set; }
    public decimal BalanceAmount { get; set; }
    public decimal BillAmount { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public byte SyncRowVersion { get; set; }
}