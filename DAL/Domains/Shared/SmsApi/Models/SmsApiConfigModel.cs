namespace MrDAL.Domains.Shared.SmsApi.Models;

public class SmsApiConfigModel
{
    public string BaseUrl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ReportUrl { get; set; }
    public string SendUrl { get; set; }
    public string CheckUpdatesUrl { get; set; }
    public string CheckBalanceUrl { get; set; }
}