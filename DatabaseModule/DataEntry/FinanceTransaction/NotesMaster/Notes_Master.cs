using DatabaseModule.CloudSync;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.FinanceTransaction.NotesMaster;

public class Notes_Master : BaseSyncData
{
    public string VoucherMode { get; set; }
    public string Voucher_No { get; set; }
    public System.DateTime Voucher_Date { get; set; }
    public string Voucher_Miti { get; set; }
    public System.DateTime Voucher_Time { get; set; }
    public string Ref_VNo { get; set; }
    public System.DateTime? Ref_VDate { get; set; }
    public string VoucherType { get; set; }
    public long Ledger_Id { get; set; }
    public string CheqNo { get; set; }
    public System.DateTime? CheqDate { get; set; }
    public string CheqMiti { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Agent_Id { get; set; }
    public int Currency_Id { get; set; }
    public decimal Currency_Rate { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public string Remarks { get; set; }
    public string Action_Type { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public string ReconcileBy { get; set; }
    public System.DateTime? ReconcileDate { get; set; }
    public bool? Audit_Lock { get; set; }
    public string ClearingBy { get; set; }
    public System.DateTime? ClearingDate { get; set; }
    public int PrintValue { get; set; }
    public bool IsReverse { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string CancelReason { get; set; }
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
    public short SyncRowVersion { get; set; }
    public DataGridView GetView { get; set; }

}