namespace DatabaseModule.Master.InventorySetup;

public class RACK
{
    public int RID { get; set; }
    public string RName { get; set; }
    public string RCode { get; set; }
    public string Location { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}