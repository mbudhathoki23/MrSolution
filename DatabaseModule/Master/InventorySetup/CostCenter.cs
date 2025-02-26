using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.InventorySetup;

public class CostCenter : BaseSyncData
{
    public int CCId { get; set; }
    public string CCName { get; set; }
    public string CCcode { get; set; }
    public int? GodownId { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}