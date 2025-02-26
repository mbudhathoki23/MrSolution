using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModule.CloudSync;

[Table("AMS.AppLog")]
[Serializable]
[JsonObject]
public class AppLog
{
    [Key] public int AuditLogId { get; set; }

    public string LogDescription { get; set; }
    public string LogType { get; set; }
    public byte LogTypeAlias { get; set; }
    public string LogGroup { get; set; }
    public byte LogGroupAlias { get; set; }
    public string ActionType { get; set; }
    public byte ActionTypeAlias { get; set; }
    public DateTime ActionTime { get; set; }
    public int? BranchId { get; set; }
    public string EnterBy { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public string RefVno { get; set; }
    public string RefId { get; set; }
    public string RefType { get; set; }
    public byte RefTypeAlias { get; set; }
    public bool IsAudit { get; set; }
}