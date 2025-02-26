using System;

namespace DatabaseModule.CloudSync;

public class SyncApiConfig
{
    public string BaseUrl { get; set; }
    public string InsertUrl { get; set; }
    public string UpdateUrl { get; set; }
    public string GetUrl { get; set; }
    public string DeleteUrl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Guid Apikey { get; set; }
    public int BranchId { get; set; }
}