namespace DatabaseModule.CloudSync;

public class SyncLogDetailModel : BaseSyncData
{
    public long Id { get; set; }
    public long SyncLogId { get; set; }
    public int BranchId { get; set; }
}