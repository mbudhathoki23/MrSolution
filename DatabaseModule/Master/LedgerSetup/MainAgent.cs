using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.LedgerSetup;

public class MainAgent : BaseSyncData
{
    public int SAgentId { get; set; }
    public string NepaliDesc { get; set; }
    public string SAgent { get; set; }
    public string SAgentCode { get; set; }
    public string PhoneNo { get; set; }
    public string Address { get; set; }
    public decimal? Comm { get; set; }
    public decimal? TagetLimit { get; set; }
    public long? GLID { get; set; }
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