using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseAdditional;

public class PAB_Master : BaseSyncData
{
    public string PAB_Invoice { get; set; }
    public System.DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public System.DateTime Invoice_Time { get; set; }
    public string PB_Invoice { get; set; }
    public System.DateTime PB_Date { get; set; }
    public string PB_Miti { get; set; }
    public decimal PB_Qty { get; set; }
    public decimal PB_Amount { get; set; }
    public decimal LocalAmount { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int? Agent_Id { get; set; }
    public int Cur_Id { get; set; }
    public decimal Cur_Rate { get; set; }
    public decimal T_Amount { get; set; }
    public string Remarks { get; set; }
    public string Action_Type { get; set; }
    public int No_Print { get; set; }
    public string In_Words { get; set; }
    public bool? Audit_Lock { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime? Enter_Date { get; set; }
    public string Reconcile_By { get; set; }
    public System.DateTime? Reconcile_Date { get; set; }
    public string Auth_By { get; set; }
    public System.DateTime? Auth_Date { get; set; }
    public string Cleared_By { get; set; }
    public System.DateTime? Cleared_Date { get; set; }
    public bool? R_Invoice { get; set; }
    public string Cancel_By { get; set; }
    public System.DateTime? Cancel_Date { get; set; }
    public string Cancel_Remarks { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public byte[] PAttachment1 { get; set; }
    public byte[] PAttachment2 { get; set; }
    public byte[] PAttachment3 { get; set; }
    public byte[] PAttachment4 { get; set; }
    public byte[] PAttachment5 { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public bool? IsSynced { get; set; }
    public short SyncRowVersion { get; set; }
    public List<PAB_Details> DetailsList { get; set; }
    public DataTable ProductTerm { get; set; }
    public DataTable BillTerm { get; set; }
    public DataTable ProductBatch { get; set; }
    public DataGridView GetView { get; set; }
}