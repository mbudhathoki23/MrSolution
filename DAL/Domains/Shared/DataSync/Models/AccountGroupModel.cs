using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class AccountGroupModel : BaseSyncData
{
    public int GrpId { get; set; }
    public string NepaliDesc { get; set; }
    public string GrpName { get; set; }
    public string GrpCode { get; set; }
    public int Schedule { get; set; }
    public string PrimaryGrp { get; set; }
    public string GrpType { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public int? PrimaryGroupId { get; set; }
    public short? IsDefault { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}