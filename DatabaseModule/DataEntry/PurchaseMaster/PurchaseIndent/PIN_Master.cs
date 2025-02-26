using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseIndent;

public class PIN_Master : BaseSyncData
{
    public string PIN_Invoice { get; set; }
    public System.DateTime PIN_Date { get; set; }
    public string PIN_Miti { get; set; }
    public string Person { get; set; }
    public int? Sub_Ledger { get; set; }
    public int? AgentId { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public string EnterBY { get; set; }
    public System.DateTime EnterDate { get; set; }
    public string ActionType { get; set; }
    public string Remarks { get; set; }
    public int Print_value { get; set; }
    public bool? R_Invoice { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string CancelRemarks { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
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
    public List<PIN_Details> DetailsList { get; set; }
    public DataTable ProductTerm { get; set; }
    public DataTable BillTerm { get; set; }
    public DataTable ProductBatch { get; set; }
    public DataGridView GetView { get; set; }
}