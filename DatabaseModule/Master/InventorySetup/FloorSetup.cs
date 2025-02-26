using System;

namespace DatabaseModule.Master.InventorySetup;

public class FloorSetup
{
    public int FloorId { get; set; }
    public string Description { get; set; }
    public string ShortName { get; set; }
    public string Type { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }

    public object GetMasterFloor(string actionTag, int floorId)
    {
        throw new NotImplementedException();
    }
}