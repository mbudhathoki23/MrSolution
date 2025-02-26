using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.StockTransaction.StockAdjustment;

public class STA_Master : BaseSyncData
{
    public string StockAdjust_No { get; set; }
    public System.DateTime VDate { get; set; }
    public string VMiti { get; set; }
    public System.DateTime Vtime { get; set; }
    public int? DepartmentId { get; set; }
    public string BarCode { get; set; }
    public string PhyStockNo { get; set; }
    public string Posting { get; set; }
    public string Export { get; set; }
    public string ReconcileBy { get; set; }
    public string AuditBy { get; set; }
    public System.DateTime? AuditDate { get; set; }
    public string AuthorizeBy { get; set; }
    public System.DateTime? AuthorizeDate { get; set; }
    public string AuthorizeRemarks { get; set; }
    public string PostedBy { get; set; }
    public System.DateTime? PostedDate { get; set; }
    public string Remarks { get; set; }
    public string Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int? PrintValue { get; set; }
    public bool IsReverse { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string CancelReason { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public bool? IsSynced { get; set; }
    public DataGridView GetView { get; set; }
    public List<STA_Details> DetailsList { get; set; }
}