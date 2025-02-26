using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.FinanceSetup;

public class NR_Master : BaseSyncData
{
    public int NRID { get; set; }
    public string NRDESC { get; set; }
    public string NRTYPE { get; set; }
    public bool? IsActive { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}