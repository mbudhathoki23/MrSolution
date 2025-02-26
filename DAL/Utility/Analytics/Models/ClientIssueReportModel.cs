using System;

namespace MrDAL.Utility.Analytics.Models;

public class ClientIssueReportModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; }
    public string IssueType { get; set; }
    public string Frequency { get; set; }
    public string Person { get; set; }
    public string Steps { get; set; }
    public string Machine { get; set; }
    public string MachineUser { get; set; }
    public DateTime? LastUpdated { get; set; }
    public string OtherData { get; set; }
    public DateTime? SyncedOn { get; set; }
    public string ErrorMsg { get; set; }
    public string ErrorDump { get; set; }
}