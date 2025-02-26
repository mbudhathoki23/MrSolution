using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.LedgerSetup;

public class SubLedger : BaseSyncData
{
    public int SLId { get; set; }
    public string NepalDesc { get; set; }
    public string SLName { get; set; }
    public string SLCode { get; set; }
    public string SLAddress { get; set; }
    public string SLPhoneNo { get; set; }
    public string GLID { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public short IsDefault { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}