using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.ProductionSystem.Production;

public class Production_Master : BaseSyncData
{
    public string VoucherNo { get; set; }

    public System.DateTime VDate { get; set; }
    public string VMiti { get; set; }
    public System.DateTime VTime { get; set; }
    public string BOMVNo { get; set; }
    public System.DateTime? BOMDate { get; set; }
    public long FinishedGoodsId { get; set; }
    public decimal FinishedGoodsQty { get; set; }
    public decimal Costing { get; set; }
    public string Machine { get; set; }
    public int? DepartmentId { get; set; }
    public int? CostCenterId { get; set; }
    public decimal Amount { get; set; }
    public string InWords { get; set; }
    public string Remarks { get; set; }
    public bool IsAuthorized { get; set; }
    public string AuthorizeBy { get; set; }
    public System.DateTime? AuthDate { get; set; }
    public bool IsCancel { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string CancelReason { get; set; }
    public bool IsReturn { get; set; }
    public bool IsReconcile { get; set; }
    public string ReconcileBy { get; set; }
    public System.DateTime? ReconcileDate { get; set; }
    public bool IsPosted { get; set; }
    public string PostedBy { get; set; }
    public System.DateTime? PostedDate { get; set; }
    public string IssueNo { get; set; }
    public System.DateTime? IssueDate { get; set; }
    public string OrderNo { get; set; }
    public System.DateTime? OrderDate { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
    public string Source { get; set; }
    public byte[] PAttachment1 { get; set; }
    public byte[] PAttachment2 { get; set; }
    public byte[] PAttachment3 { get; set; }
    public byte[] PAttachment4 { get; set; }
    public byte[] PAttachment5 { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public DataGridView GetView { get; set; }
    public List<Production_Details> DetailsList { get; set; }
}