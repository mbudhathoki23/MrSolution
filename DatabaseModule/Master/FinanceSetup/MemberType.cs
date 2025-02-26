using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.FinanceSetup;

public class MemberType : BaseSyncData
{
    public int MemberTypeId { get; set; }
    public string NepaliDesc { get; set; }
    public string MemberDesc { get; set; }
    public string MemberShortName { get; set; }
    public decimal Discount { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public bool ActiveStatus { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}