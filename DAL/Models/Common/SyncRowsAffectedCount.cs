namespace MrDAL.Models.Common;

public class SyncRowsAffectedCount
{
    public int rowsInserted { get; set; }
    public int rowsUpdated { get; set; }
    public int rowsAffecated { get; set; }
    public bool Success { get; set; }
}