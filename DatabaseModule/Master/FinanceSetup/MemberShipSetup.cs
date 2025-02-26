using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.FinanceSetup;

public class MemberShipSetup : BaseSyncData
{
    public object PrimaryGrp;

    public int MShipId { get; set; }
    public int MemberId { get; set; }
    public string NepaliDesc { get; set; }
    public string MShipDesc { get; set; }
    public string MShipShortName { get; set; }
    public string PhoneNo { get; set; }
    public string PriceTag { get; set; }
    public long LedgerId { get; set; }
    public string EmailAdd { get; set; }
    public int MemberTypeId { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public System.DateTime MValidDate { get; set; }
    public System.DateTime MExpireDate { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public bool ActiveStatus { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}