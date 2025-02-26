using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class ProductSubGroupModel : BaseSyncData
{
    public int PSubGrpId { get; set; }
    public string? NepaliDesc { get; set; }
    public string SubGrpName { get; set; }
    public string ShortName { get; set; }
    public int GrpId { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public short IsDefault { get; set; }
    public bool Status { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}