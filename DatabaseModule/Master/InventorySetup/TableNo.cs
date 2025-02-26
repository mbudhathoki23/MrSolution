namespace DatabaseModule.Master.InventorySetup;

public class TableNo
{
    public int TableId { get; set; }
    public int Branch_ID { get; set; }
    public string TableName { get; set; }
    public string TableCode { get; set; }
    public int FloorId { get; set; }
    public int? Company_Id { get; set; }
    public string TableStatus { get; set; }
    public string TableType { get; set; }
    public bool IsPrePaid { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int? Printed { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}