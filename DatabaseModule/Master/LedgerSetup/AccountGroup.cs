using DatabaseModule.CloudSync;
namespace DatabaseModule.Master.LedgerSetup;

public class AccountGroup : BaseSyncData
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
    public System.DateTime EnterDate { get; set; }
    public int? PrimaryGroupId { get; set; }
    public short? IsDefault { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}