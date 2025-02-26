using System;

namespace MrDAL.Utility.Analytics.Models;

public class SqliteClientLogImageModel
{
    public Guid Id { get; set; }
    public byte[] Image { get; set; }
    public DateTime DateTime { get; set; }
    public string Machine { get; set; }
    public string MachineUser { get; set; }
    public DateTime? SyncedOn { get; set; }
    public DateTime? LastUpdateOn { get; set; }
}