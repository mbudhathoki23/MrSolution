namespace DatabaseModule.Setup.TermSetup;

public class PT_Term
{
    public int PT_Id { get; set; }
    public int Order_No { get; set; }
    public string PT_Name { get; set; }
    public string Module { get; set; }
    public string PT_Type { get; set; }
    public string PT_Basis { get; set; }
    public string PT_Sign { get; set; }
    public string PT_Condition { get; set; }
    public long Ledger { get; set; }
    public decimal PT_Rate { get; set; }
    public int PT_Branch { get; set; }
    public int? PT_CompanyUnit { get; set; }
    public bool PT_Profitability { get; set; }
    public bool PT_Supess { get; set; }
    public bool PT_Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}