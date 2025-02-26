namespace DatabaseModule.Setup.DocumentNumberings;

public class DocumentNumbering
{
    public int DocId { get; set; }
    public string DocModule { get; set; }
    public string DocDesc { get; set; }
    public System.DateTime DocStartDate { get; set; }
    public string DocStartMiti { get; set; }
    public System.DateTime DocEndDate { get; set; }
    public string DocEndMiti { get; set; }
    public string DocUser { get; set; }
    public string DocType { get; set; }
    public string DocPrefix { get; set; }
    public string DocSufix { get; set; }
    public decimal DocBodyLength { get; set; }
    public decimal DocTotalLength { get; set; }
    public bool DocBlank { get; set; }
    public string DocBlankCh { get; set; }
    public int DocBranch { get; set; }
    public int? DocUnit { get; set; }
    public decimal? DocStart { get; set; }
    public decimal? DocCurr { get; set; }
    public decimal? DocEnd { get; set; }
    public string DocDesign { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int? FiscalYearId { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}