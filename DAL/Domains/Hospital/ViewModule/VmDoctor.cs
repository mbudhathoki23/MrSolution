using System;

namespace MrDAL.Domains.Hospital.ViewModule;

public class VmDoctor
{
    public string _ActionTag { get; set; }
    public int DrId { get; set; }
    public string DrName { get; set; }
    public string DrShortName { get; set; }
    public int? DrType { get; set; }
    public decimal? ContactNo { get; set; }
    public string Address { get; set; }
    public int? BranchId { get; set; }
    public int? CompanyUnit { get; set; }
    public bool? Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime? EnterDate { get; set; }
    public Guid SyncGlobalId { get; set; }
    public Guid SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short? SyncRowVersion { get; set; }
    public Guid SyncBaseId { get; set; }
}