using System;
using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class ProductUnitModel : BaseSyncData
{
    public int UID { get; set; }
    public string? NepaliDesc { get; set; }
    public string UnitName { get; set; }
    public string UnitCode { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public bool Status { get; set; }
    public short IsDefault { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}