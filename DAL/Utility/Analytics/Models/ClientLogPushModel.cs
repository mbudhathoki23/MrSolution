using System;

namespace MrDAL.Utility.Analytics.Models;

public class ClientLogPushModel
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public DateTime LogTime { get; set; }
    public Guid? ImageId { get; set; }
    public string Dump { get; set; }
    public string LogType { get; set; }
    public string OtherData { get; set; }
    public Guid? ClientId { get; set; }
    public string Machine { get; set; }
    public string MachineUser { get; set; }
    public DateTime? ClientLastUpdated { get; set; }
    public string GeoInfo { get; set; }
    public string HwId { get; set; }
    public Guid? OutletUqId { get; set; }
}