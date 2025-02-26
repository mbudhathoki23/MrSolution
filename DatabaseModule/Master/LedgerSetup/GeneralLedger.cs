using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.LedgerSetup;

public class GeneralLedger : BaseSyncData
{
    public long GLID { get; set; }
    public string? NepaliDesc { get; set; }
    public string GLName { get; set; }
    public string GLCode { get; set; }
    public string ACCode { get; set; }
    public string GLType { get; set; }
    public int GrpId { get; set; }
    public int? PrimaryGroupId { get; set; }
    public int? SubGrpId { get; set; }
    public int? PrimarySubGroupId { get; set; }
    public string? PanNo { get; set; }
    public int? AreaId { get; set; }
    public int? AgentId { get; set; }
    public int? CurrId { get; set; }
    public decimal CrDays { get; set; }
    public decimal CrLimit { get; set; }
    public string CrTYpe { get; set; }
    public decimal IntRate { get; set; }
    public string? GLAddress { get; set; }
    public string? PhoneNo { get; set; }
    public string? LandLineNo { get; set; }
    public string? OwnerName { get; set; }
    public string? OwnerNumber { get; set; }
    public int? Scheme { get; set; }
    public string Email { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public bool Status { get; set; }
    public short IsDefault { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}