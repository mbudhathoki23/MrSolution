using System;

namespace DatabaseModule.CloudSync;

public class SyncLogModel : BaseSyncData
{
    public long Id { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public string TableName { get; set; }
    public string JsonData { get; set; }
    public string Action { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public SyncLogDetailModel SyncLogDetail { get; set; }
}