using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.ProductionSystem.BillOfMaterials;

public class BOM_Master
{
    public string VoucherNo { get; set; }
    public System.DateTime VDate { get; set; }
    public string VMiti { get; set; }
    public System.DateTime VTime { get; set; }
    public long FinishedGoodsId { get; set; }
    public decimal FinishedGoodsQty { get; set; }
    public int? DepartmentId { get; set; }
    public int? CostCenterId { get; set; }
    public decimal Amount { get; set; }
    public string InWords { get; set; }
    public string Remarks { get; set; }
    public bool IsAuthorized { get; set; }
    public string AuthorizeBy { get; set; }
    public System.DateTime? AuthDate { get; set; }
    public bool IsReconcile { get; set; }
    public string ReconcileBy { get; set; }
    public System.DateTime? ReconcileDate { get; set; }
    public bool IsPosted { get; set; }
    public string PostedBy { get; set; }
    public System.DateTime? PostedDate { get; set; }
    public string OrderNo { get; set; }
    public System.DateTime? OrderDate { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public int FiscalYearId { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public DataGridView GetView { get; set; }
    public List<BOM_Details> DetailsList { get; set; }
}