using System;
using System.Collections.Generic;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class PurchaseReturnModel : BaseSyncData
{
    public string PR_Invoice { get; set; }
    public DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public DateTime Invoice_Time { get; set; }
    public string? PB_Invoice { get; set; }
    public DateTime? PB_Date { get; set; }
    public string? PB_Miti { get; set; }
    public long Vendor_ID { get; set; }
    public long? PartyLedgerId { get; set; }
    public string? Party_Name { get; set; }
    public string? Vat_No { get; set; }
    public string? Contact_Person { get; set; }
    public string? Mobile_No { get; set; }
    public string? Address { get; set; }
    public string? ChqNo { get; set; }
    public DateTime? ChqDate { get; set; }
    public string? ChqMiti { get; set; }
    public string? Invoice_Type { get; set; }
    public string? Invoice_In { get; set; }
    public int? DueDays { get; set; }
    public DateTime? DueDate { get; set; }
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
    public string In_Words { get; set; }
    public string? Remarks { get; set; }
    public bool? Audit_Lock { get; set; }
    public string Enter_By { get; set; }
    public DateTime Enter_Date { get; set; }
    public string? Reconcile_By { get; set; }
    public DateTime? Reconcile_Date { get; set; }
    public string? Auth_By { get; set; }
    public DateTime? Auth_Date { get; set; }
    public string? Cleared_By { get; set; }
    public DateTime? Cleared_Date { get; set; }
    public bool R_Invoice { get; set; }
    public string? CancelBy { get; set; }
    public DateTime? CancelDate { get; set; }
    public string? CancelRemarks { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public byte[]? PAttachment1 { get; set; }
    public byte[]? PAttachment2 { get; set; }
    public byte[]? PAttachment3 { get; set; }
    public byte[]? PAttachment4 { get; set; }
    public byte[]? PAttachment5 { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public string? ReturnChallanNo { get; set; }
    public DateTime? ReturnChallanDate { get; set; }
    public bool? IsSynced { get; set; }
    public List<PurchaseReturnDetailsModel> PurchaseReturnDetailsModels { get; set; }
    public List<PurchaseReturnTermModel> PurchaseReturnTermModels { get; set; }
    public List<ProductAddInfoModel> ProductAddInfoModels { get; set; }
}