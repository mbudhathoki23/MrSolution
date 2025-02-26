namespace DatabaseModule.Master.ProductSetup;

public class Godown
{
    public int GID { get; set; }
    public string NepaliDesc { get; set; }
    public string GName { get; set; }
    public string GCode { get; set; }
    public string GLocation { get; set; }
    public bool? Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int BranchUnit { get; set; }
    public int? CompUnit { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}