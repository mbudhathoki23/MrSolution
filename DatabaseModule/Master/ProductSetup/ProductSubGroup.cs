using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.ProductSetup;

public class ProductSubGroup : BaseSyncData
{
    public int PSubGrpId { get; set; }
    public string NepaliDesc { get; set; }
    public string SubGrpName { get; set; }
    public string ShortName { get; set; }
    public int GrpId { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public short IsDefault { get; set; }
    public bool Status { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}