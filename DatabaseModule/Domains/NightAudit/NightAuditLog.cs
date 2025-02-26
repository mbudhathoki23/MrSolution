using DatabaseModule.CloudSync;

namespace DatabaseModule.Domains.NightAudit;

public class NightAuditLog : BaseSyncData
{
    public int LogId { get; set; }
    public System.DateTime? AuditDate { get; set; }
    public bool? IsAudited { get; set; }
    public string AuditUser { get; set; }
    public System.DateTime? AuditedDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
}