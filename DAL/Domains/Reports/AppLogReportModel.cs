using System;

namespace MrDAL.Domains.Reports;

public class AppLogReportModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string LogType { get; set; }
    public byte LogTypeAlias { get; set; }
    public string LogGroup { get; set; }
    public byte LogGroupAlias { get; set; }
    public string ActionType { get; set; }
    public byte ActionTypeAlias { get; set; }
    public DateTime ActionTime { get; set; }
    public int? BranchId { get; set; }
    public string EnterBy { get; set; }
    public string OldValueXml { get; set; }
    public string NewValueXml { get; set; }
    public string RefNo { get; set; }
    public string RefId { get; set; }
    public string RefType { get; set; }
    public byte RefTypeAlias { get; set; }
    public bool IsAudit { get; set; }
}