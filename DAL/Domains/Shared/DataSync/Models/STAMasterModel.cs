using System;
using System.Collections.Generic;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class STAMasterModel : BaseSyncData
{
    public string StockAdjust_No { get; set; }
    public DateTime VDate { get; set; }
    public string VMiti { get; set; }
    public DateTime Vtime { get; set; }
    public int? DepartmentId { get; set; }
    public string? BarCode { get; set; }
    public string? PhyStockNo { get; set; }
    public string? Posting { get; set; }
    public string? Export { get; set; }
    public string? ReconcileBy { get; set; }
    public string? AuditBy { get; set; }
    public DateTime? AuditDate { get; set; }
    public string? AuthorizeBy { get; set; }
    public DateTime? AuthorizeDate { get; set; }
    public string? AuthorizeRemarks { get; set; }
    public string? PostedBy { get; set; }
    public DateTime? PostedDate { get; set; }
    public string? Remarks { get; set; }
    public string Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public int? PrintValue { get; set; }
    public bool IsReverse { get; set; }
    public string? CancelBy { get; set; }
    public DateTime? CancelDate { get; set; }
    public string? CancelReason { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public List<STADetailsModel> STADetailsModels { get; set; }
}