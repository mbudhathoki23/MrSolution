using DatabaseModule.CloudSync;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.SalesMaster.SalesChallan;

public class SC_Master : BaseSyncData
{
    public string SC_Invoice { get; set; }
    public System.DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public System.DateTime Invoice_Time { get; set; }
    public string Ref_Vno { get; set; }
    public System.DateTime? Ref_Date { get; set; }
    public string Ref_Miti { get; set; }
    public long Customer_Id { get; set; }
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
    public string Invoice_Mode { get; set; }
    public string Payment_Mode { get; set; }
    public int? DueDays { get; set; }
    public System.DateTime? DueDate { get; set; }
    public int? Agent_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public string QOT_Invoice { get; set; }
    public System.DateTime? QOT_Date { get; set; }
    public string SO_Invoice { get; set; }
    public System.DateTime? SO_Date { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int? CounterId { get; set; }
    public int Cur_Id { get; set; }
    public decimal Cur_Rate { get; set; }
    public decimal B_Amount { get; set; }
    public decimal T_Amount { get; set; }
    public decimal N_Amount { get; set; }
    public decimal LN_Amount { get; set; }
    public decimal V_Amount { get; set; }
    public decimal Tbl_Amount { get; set; }
    public decimal Tender_Amount { get; set; }
    public decimal Return_Amount { get; set; }
    public string Action_Type { get; set; }
    public bool R_Invoice { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; }
    public string CancelReason { get; set; }
    public decimal? No_Print { get; set; }
    public string In_Words { get; set; }
    public string Remarks { get; set; }
    public bool? Audit_Lock { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime Enter_Date { get; set; }
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
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public bool? IsSynced { get; set; }
    public DataGridView GetView { get; set; }
    public List<SC_Details> DetailsList { get; set; }
    public List<SC_Term> Terms { get; set; }
    public List<SC_Master_OtherDetails> OtherDetails { get; set; }
    public DataTable dtPTerm { get; set; }
    public DataTable dtBTerm { get; set; }
}