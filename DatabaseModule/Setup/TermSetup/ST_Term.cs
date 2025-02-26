namespace DatabaseModule.Setup.TermSetup;

public class ST_Term
{
    public int ST_ID { get; set; }
    public int Order_No { get; set; }
    public string ST_Name { get; set; }
    public string Module { get; set; }
    public string ST_Type { get; set; }
    public string ST_Basis { get; set; }
    public string ST_Sign { get; set; }
    public string ST_Condition { get; set; }
    public long Ledger { get; set; }
    public decimal? ST_Rate { get; set; }
    public int ST_Branch { get; set; }
    public int? ST_CompanyUnit { get; set; }
    public bool ST_Profitability { get; set; }
    public bool ST_Supess { get; set; }
    public bool ST_Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}