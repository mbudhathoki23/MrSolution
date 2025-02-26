namespace DatabaseModule.CloudSync.IrdServer;

public class SqlQuerySyncViewModel
{
    public string QueryLogId { get; set; }
    public string SqlQuery { get; set; }
    public string Module { get; set; }
    public string VoucherNo { get; set; }
    public string QueryType { get; set; }
    public string IsSync { get; set; }
    public string EntryDate { get; set; }
    public string DbName { get; set; }
}