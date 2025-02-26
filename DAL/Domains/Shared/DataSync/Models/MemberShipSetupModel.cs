using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class MemberShipSetupModel : BaseSyncData
{
    public int MShipId { get; set; }
    public int MemberId { get; set; }
    public string? NepaliDesc { get; set; }
    public string MShipDesc { get; set; }
    public string MShipShortName { get; set; }
    public string? PhoneNo { get; set; }
    public string? PriceTag { get; set; }
    public long LedgerId { get; set; }
    public string? EmailAdd { get; set; }
    public int MemberTypeId { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public DateTime MValidDate { get; set; }
    public DateTime MExpireDate { get; set; }
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