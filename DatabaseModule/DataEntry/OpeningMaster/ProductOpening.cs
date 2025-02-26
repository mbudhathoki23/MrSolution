using DatabaseModule.Master.ProductSetup;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
namespace DatabaseModule.DataEntry.OpeningMaster;

public class ProductOpening
{
    public int OpeningId { get; set; }
    public string Voucher_No { get; set; }
    public int Serial_No { get; set; }
    public System.DateTime OP_Date { get; set; }
    public string OP_Miti { get; set; }
    public long Product_Id { get; set; }
    public int? Godown_Id { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int? Currency_Id { get; set; }
    public decimal? Currency_Rate { get; set; }
    public decimal? AltQty { get; set; }
    public int? AltUnit { get; set; }
    public decimal Qty { get; set; }
    public int? QtyUnit { get; set; }
    public decimal Rate { get; set; }
    public decimal? LocalRate { get; set; }
    public decimal Amount { get; set; }
    public decimal? LocalAmount { get; set; }
    public bool IsReverse { get; set; }
    public string CancelRemarks { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string Remarks { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime Enter_Date { get; set; }
    public string Reconcile_By { get; set; }
    public System.DateTime? Reconcile_Date { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public DataGridView GetView { get; set; }
    public List<ProductAddInfo> ProductAddInfos { get; set; }
    public DataTable ProductBatch { get; set; }
    public List<ProductOpening> Details { get; set; }
}