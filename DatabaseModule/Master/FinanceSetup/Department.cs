namespace DatabaseModule.Master.FinanceSetup;

public class Department
{
    public int DId { get; set; }
    public string NepaliDesc { get; set; }
    public string DName { get; set; }
    public string DCode { get; set; }
    public string Dlevel { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public string? EnterBy { get; set; }
    public System.DateTime? EnterDate { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}