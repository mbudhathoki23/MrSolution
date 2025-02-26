using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class JuniorAgentModel : BaseSyncData
{
    public int AgentId { get; set; }
    public string? NepaliDesc { get; set; }
    public string AgentName { get; set; }
    public string AgentCode { get; set; }
    public string? Address { get; set; }
    public string? PhoneNo { get; set; }
    public long? GLCode { get; set; }
    public decimal Commission { get; set; }
    public decimal CrLimit { get; set; }
    public decimal CrDays { get; set; }
    public string CrTYpe { get; set; }
    public decimal TargetLimit { get; set; }
    public int? SAgent { get; set; }
    public string? Email { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public bool Status { get; set; }
    public short? IsDefault { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}