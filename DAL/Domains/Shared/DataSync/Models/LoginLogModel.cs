using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class LoginLogModel : BaseSyncData
{
    public long OBJECT_ID { get; set; }
    public string LOGIN_USER { get; set; }
    public string COMPANY { get; set; }
    public string LOGIN_DATABASE { get; set; }
    public string? DEVICE { get; set; }
    public string? DAVICE_MAC { get; set; }
    public string? DEVICE_IP { get; set; }
    public string? SYSTEM_ID { get; set; }
    public DateTime? LOGIN_DATE { get; set; }
    public int? LOG_STATUS { get; set; }
}