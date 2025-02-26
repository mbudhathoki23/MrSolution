using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;

public class PCR_Master : BaseSyncData
{
    public string PCR_Invoice { get; set; }
    public System.DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public System.DateTime Invoice_Time { get; set; }
    public string PB_Vno { get; set; }
    public System.DateTime? Vno_Date { get; set; }
    public string Vno_Miti { get; set; }
    public long Vendor_ID { get; set; }
    public long? PartyLedgerId { get; set; }
    public string Party_Name { get; set; }
    public string Vat_No { get; set; }
    public string Contact_Person { get; set; }
    public string Mobile_No { get; set; }
    public string Address { get; set; }
    public string ChqNo { get; set; }
    public System.DateTime? ChqDate { get; set; }
    public string ChqMiti { get; set; }
    public string Invoice_Type { get; set; }
    public string Invoice_In { get; set; }
    public int? DueDays { get; set; }
    public System.DateTime? DueDate { get; set; }
    public int? Agent_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public string PO_Invoice { get; set; }
    public System.DateTime? PO_Date { get; set; }
    public string QOT_Invoice { get; set; }
    public System.DateTime? QOT_Date { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int? Cur_Id { get; set; }
    public decimal Cur_Rate { get; set; }
    public int? Counter_ID { get; set; }
    public decimal B_Amount { get; set; }
    public decimal T_Amount { get; set; }
    public decimal N_Amount { get; set; }
    public decimal LN_Amount { get; set; }
    public decimal Tender_Amount { get; set; }
    public decimal Change_Amount { get; set; }
    public decimal V_Amount { get; set; }
    public decimal Tbl_Amount { get; set; }
    public string Action_type { get; set; }
    public bool? R_Invoice { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string CancelReason { get; set; }
    public int No_Print { get; set; }
    public string In_Words { get; set; }
    public string Remarks { get; set; }
    public bool Audit_Lock { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime? Enter_Date { get; set; }
    public string Reconcile_By { get; set; }
    public System.DateTime? Reconcile_Date { get; set; }
    public string Auth_By { get; set; }
    public System.DateTime? Auth_Date { get; set; }
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
    public bool? IsSynced { get; set; }
    public List<PCR_Details> DetailsList { get; set; }
    public List<PCR_Term> Terms { get; set; }
    public DataTable ProductTerm { get; set; }
    public DataTable BillTerm { get; set; }
    public DataTable ProductBatch { get; set; }
    public DataGridView GetView { get; set; }
}