using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.ProductionSystem.RawMaterialsIssue;

public class StockIssue_Master : BaseSyncData
{
    public string VoucherNo { get; set; }
    public System.DateTime VoucherDate { get; set; }
    public string VoucherMiti { get; set; }
    public System.DateTime? VoucherTime { get; set; }
    public string BOM_Vno { get; set; }
    public System.DateTime? BOM_Date { get; set; }
    public string BOM_Miti { get; set; }
    public int? DepartmentId { get; set; }
    public int? CostCenterId { get; set; }
    public long? FinishedGoodsId { get; set; }
    public decimal? AltQty { get; set; }
    public int? AltUnitId { get; set; }
    public decimal? Qty { get; set; }
    public int? UnitId { get; set; }
    public decimal? Factor { get; set; }
    public decimal? AdditionalAmount { get; set; }
    public string Remarks { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public string AuthorizeBy { get; set; }
    public System.DateTime? AuthorizeDate { get; set; }
    public string ReconcileBy { get; set; }
    public System.DateTime? ReconcileDate { get; set; }
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
    public DataGridView GetView { get; set; }
    public List<StockIssue_Details> DetailsList { get; set; }
}