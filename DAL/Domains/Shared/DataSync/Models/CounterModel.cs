using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class CounterModel : BaseSyncData
{
    public int CId { get; set; }
    public string CName { get; set; }
    public string CCode { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public string? Printer { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}