using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class SeniorAgentModel : BaseSyncData
{
    public int SAgentId { get; set; }
    public string? NepaliDesc { get; set; }
    public string SAgent { get; set; }
    public string SAgentCode { get; set; }
    public string? PhoneNo { get; set; }
    public string? Address { get; set; }
    public decimal? Comm { get; set; }
    public decimal? TagetLimit { get; set; }
    public long? GLID { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public short IsDefault { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}