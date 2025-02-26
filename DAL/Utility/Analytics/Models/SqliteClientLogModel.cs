using System;

namespace MrDAL.Utility.Analytics.Models;

public class SqliteClientLogModel : ClientLogBase
{
    public Guid Id { get; set; }
    public Guid? ImageId { get; set; }
    public DateTime? SyncedOn { get; set; }
    public int LogTypeAlias { get; set; }
    public string OtherData { get; set; }
    public string Machine { get; set; }
    public string MachineUser { get; set; }
    public DateTime? LastUpdateOn { get; set; }
}