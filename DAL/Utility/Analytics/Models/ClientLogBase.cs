using System;

namespace MrDAL.Utility.Analytics.Models;

public abstract class ClientLogBase
{
    public DateTime LogTime { get; set; }
    public string Message { get; set; }
    public string LogType { get; set; }
    public string Dump { get; set; }
}