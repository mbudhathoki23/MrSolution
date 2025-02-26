using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class RackModel : BaseSyncData
{
    public int RID { get; set; }
    public string RName { get; set; }
    public string RCode { get; set; }
    public string? Location { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}