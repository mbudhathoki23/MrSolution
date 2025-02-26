using System;

namespace DatabaseModule.Setup.LogSetting;

public class ImportLog
{
    public int LogId { get; set; }
    public string ImportType { get; set; }
    public DateTime? ImportDate { get; set; }
    public string ServerDesc { get; set; }
    public string ServerUser { get; set; }
    public string ServerPassword { get; set; }
    public string dbInitial { get; set; }
    public string dbCompanyInfo { get; set; }
    public bool IsSuccess { get; set; }
}