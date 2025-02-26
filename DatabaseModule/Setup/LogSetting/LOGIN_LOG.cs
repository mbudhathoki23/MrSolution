using DatabaseModule.CloudSync;

namespace DatabaseModule.Setup.LogSetting;

public class LOGIN_LOG : BaseSyncData
{
    public long OBJECT_ID { get; set; }
    public string LOGIN_USER { get; set; }
    public string COMPANY { get; set; }
    public string LOGIN_DATABASE { get; set; }
    public string DEVICE { get; set; }
    public string DAVICE_MAC { get; set; }
    public string DEVICE_IP { get; set; }
    public string SYSTEM_ID { get; set; }
    public System.DateTime? LOGIN_DATE { get; set; }
    public int? LOG_STATUS { get; set; }
}