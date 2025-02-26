using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.LedgerSetup;

public class Area : BaseSyncData
{
    public int AreaId { get; set; }
    public string NepaliDesc { get; set; }
    public string AreaName { get; set; }
    public string AreaCode { get; set; }
    public string Country { get; set; }
    public int Branch_Id { get; set; }
    public int? CompanyId { get; set; }
    public int? MainArea { get; set; }
    public bool Status { get; set; }
    public short IsDefault { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}