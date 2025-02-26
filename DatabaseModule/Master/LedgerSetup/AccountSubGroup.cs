using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.LedgerSetup;

public class AccountSubGroup : BaseSyncData
{
    public int SubGrpId { get; set; }
    public string NepaliDesc { get; set; }
    public string SubGrpName { get; set; }
    public int GrpId { get; set; }
    public string SubGrpCode { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public string? EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int? PrimaryGroupId { get; set; }
    public int? PrimarySubGroupId { get; set; }
    public short IsDefault { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}