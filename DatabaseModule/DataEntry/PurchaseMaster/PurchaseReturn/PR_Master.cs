using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace DatabaseModule.DataEntry.PurchaseMaster.PurchaseReturn;

public class PR_Master : BaseSyncData
{
    public string PR_Invoice { get; set; }
    public System.DateTime Invoice_Date { get; set; } = DateTime.Now;
    public string Invoice_Miti { get; set; }
    public System.DateTime Invoice_Time { get; set; } = DateTime.Now;
    public string PB_Invoice { get; set; }
    public System.DateTime? PB_Date { get; set; } = DateTime.Now;
    public string PB_Miti { get; set; }
    public long Vendor_ID { get; set; }
    public long? PartyLedgerId { get; set; }
    public string Party_Name { get; set; }
    public string Vat_No { get; set; }
    public string Contact_Person { get; set; }
    public string Mobile_No { get; set; }
    public string Address { get; set; }
    public string ChqNo { get; set; }
    public System.DateTime? ChqDate { get; set; } = DateTime.Now;
    public string ChqMiti { get; set; }
    public string Invoice_Type { get; set; }
    public string Invoice_In { get; set; }
    public int? DueDays { get; set; }
    public System.DateTime? DueDate { get; set; } = DateTime.Now;
    public int? Agent_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int Cur_Id { get; set; }
    public decimal Cur_Rate { get; set; }
    public decimal B_Amount { get; set; }
    public decimal T_Amount { get; set; }
    public decimal N_Amount { get; set; }
    public decimal LN_Amount { get; set; }
    public decimal Tender_Amount { get; set; }
    public decimal Change_Amount { get; set; }
    public decimal V_Amount { get; set; }
    public decimal Tbl_Amount { get; set; }
    public string Action_type { get; set; }
    public decimal No_Print { get; set; }
    public string? In_Words { get; set; }
    public string? Remarks { get; set; }
    public bool? Audit_Lock { get; set; }
    public string Enter_By { get; set; } = "ADMIN";
    public System.DateTime Enter_Date { get; set; } = DateTime.Now;
    public string Reconcile_By { get; set; }
    public System.DateTime? Reconcile_Date { get; set; } = DateTime.Now;
    public string Auth_By { get; set; }
    public System.DateTime? Auth_Date { get; set; } = DateTime.Now;
    public string Cleared_By { get; set; }
    public System.DateTime? Cleared_Date { get; set; } = DateTime.Now;
    public bool R_Invoice { get; set; }
    public string CancelBy { get; set; }
    public System.DateTime? CancelDate { get; set; } = DateTime.Now;
    public string CancelRemarks { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public byte[] PAttachment1 { get; set; }
    public byte[] PAttachment2 { get; set; }
    public byte[] PAttachment3 { get; set; }
    public byte[] PAttachment4 { get; set; }
    public byte[] PAttachment5 { get; set; }
    public System.Guid SyncBaseId { get; set; } = Guid.NewGuid();
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; } = DateTime.Now;
    public System.DateTime? SyncLastPatchedOn { get; set; } = DateTime.Now;
    public short SyncRowVersion { get; set; }
    public string ReturnChallanNo { get; set; }
    public System.DateTime? ReturnChallanDate { get; set; } = DateTime.Now;
    public bool? IsSynced { get; set; }
    public List<PR_Details> DetailsList { get; set; }
    public List<PR_Term> Terms { get; set; }
    public DataTable ProductTerm { get; set; }
    public DataTable BillTerm { get; set; }
    public DataTable ProductBatch { get; set; }
    public DataGridView GetView { get; set; }
    public List<ProductAddInfo> ProductAddInfos { get; set; }
}