using System;
using System.Collections.Generic;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class SalesReturnModel : BaseSyncData
{
    public string SR_Invoice { get; set; }
    public DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public DateTime Invoice_Time { get; set; }
    public string? SB_Invoice { get; set; }
    public DateTime? SB_Date { get; set; }
    public string? SB_Miti { get; set; }
    public long Customer_ID { get; set; }
    public long? PartyLedgerId { get; set; }
    public string? Party_Name { get; set; }
    public string? Vat_No { get; set; }
    public string? Contact_Person { get; set; }
    public string? Mobile_No { get; set; }
    public string? Address { get; set; }
    public string? ChqNo { get; set; }
    public DateTime? ChqDate { get; set; }
    public string? ChqMiti { get; set; }
    public string Invoice_Type { get; set; }
    public string Invoice_Mode { get; set; }
    public string Payment_Mode { get; set; }
    public int? DueDays { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Agent_Id { get; set; }
    public int? Subledger_Id { get; set; }
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
    public string? Action_Type { get; set; }
    public string? In_Words { get; set; }
    public string? Remarks { get; set; }
    public bool R_Invoice { get; set; }
    public bool Is_Printed { get; set; }
    public string? Cancel_By { get; set; }
    public DateTime? Cancel_Date { get; set; }
    public string? Cancel_Remarks { get; set; }
    public int No_Print { get; set; }
    public string? Printed_By { get; set; }
    public DateTime? Printed_Date { get; set; }
    public bool? Audit_Lock { get; set; }
    public string Enter_By { get; set; }
    public DateTime Enter_Date { get; set; }
    public string? Reconcile_By { get; set; }
    public DateTime? Reconcile_Date { get; set; }
    public string? Auth_By { get; set; }
    public DateTime? Auth_Date { get; set; }
    public string? Cleared_By { get; set; }
    public DateTime? Cleared_Date { get; set; }
    public bool? IsAPIPosted { get; set; }
    public bool? IsRealtime { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public bool? IsSynced { get; set; }
    public List<SalesReturnDetailsModel> SalesReturnDetailsModels { get; set; }
    public List<SalesReturnTermModel> SalesReturnTermModels { get; set; }
}