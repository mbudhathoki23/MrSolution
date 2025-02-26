using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class MemberTypeModel : BaseSyncData
{
    public int MemberTypeId { get; set; }
    public string? NepaliDesc { get; set; }
    public string MemberDesc { get; set; }
    public string MemberShortName { get; set; }
    public decimal Discount { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public bool ActiveStatus { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}