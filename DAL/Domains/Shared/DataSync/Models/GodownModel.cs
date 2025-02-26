using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class GodownModel : BaseSyncData
{
    public int GID { get; set; }
    public string? NepaliDesc { get; set; }
    public string GName { get; set; }
    public string GCode { get; set; }
    public string? GLocation { get; set; }
    public bool? Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public int BranchUnit { get; set; }
    public int? CompUnit { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}