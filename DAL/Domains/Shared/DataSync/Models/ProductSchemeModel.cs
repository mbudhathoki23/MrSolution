using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class ProductSchemeModel : BaseSyncData
{
    public int SchemeId { get; set; }
    public DateTime SchemeDate { get; set; }
    public string SchemeMiti { get; set; }
    public DateTime SchemeTime { get; set; }
    public string SchemeDesc { get; set; }
    public DateTime? ValidFrom { get; set; }
    public string ValidFromMiti { get; set; }
    public DateTime? ValidTo { get; set; }
    public string ValidToMiti { get; set; }
    public string EnterBy { get; set; }
    public DateTime? EnterDate { get; set; }
    public bool IsActive { get; set; }
    public string? Remarks { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
}