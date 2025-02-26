namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseAdditional;

public class PAB_Details
{
    public string PAB_Invoice { get; set; }
    public int SNo { get; set; }
    public int PT_Id { get; set; }
    public long Ledger_Id { get; set; }
    public long? CBLedger_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Agent_Id { get; set; }
    public int? DepartmentId { get; set; }
    public long? Product_Id { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? Amount { get; set; }
    public decimal? N_Amount { get; set; }
    public string Term_Type { get; set; }
    public string PAB_Narration { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public int? FiscalYearId { get; set; }
}