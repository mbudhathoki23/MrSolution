using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.ProductSetup;

public class ProductUnit : BaseSyncData
{
    public int UID { get; set; }
    public string NepaliDesc { get; set; }
    public string UnitName { get; set; }
    public string UnitCode { get; set; }
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