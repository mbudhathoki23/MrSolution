namespace DatabaseModule.Setup.LogSetting;

public class BR_LOG
{
    public string DB_NAME { get; set; }
    public string COMPANY { get; set; }
    public string LOCATION { get; set; }
    public string USED_BY { get; set; }
    public System.DateTime? USED_ON { get; set; }
    public string ACTION { get; set; }
    public short? SyncRowVersion { get; set; }
    public int? Log_ID { get; set; }
    public string STATUS { get; set; }
}