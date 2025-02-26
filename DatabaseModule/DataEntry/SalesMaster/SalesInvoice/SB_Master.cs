using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModule.DataEntry.SalesMaster.SalesInvoice;

public class SB_Master : BaseSyncData
{
    public string SB_Invoice { get; set; }
    public System.DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public System.DateTime Invoice_Time { get; set; }
    public string PB_Vno { get; set; }
    public System.DateTime? Vno_Date { get; set; }
    public string Vno_Miti { get; set; }
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
    public int DueDays { get; set; }
    public System.DateTime? DueDate { get; set; }
    public int? Agent_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public string SO_Invoice { get; set; }
    public System.DateTime? SO_Date { get; set; }
    public string SC_Invoice { get; set; }
    public System.DateTime? SC_Date { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public int? CounterId { get; set; }
    public int Cur_Id { get; set; }
    public decimal Cur_Rate { get; set; }
    public decimal B_Amount { get; set; }
    public decimal T_Amount { get; set; }
    public decimal TermRate { get; set; }
    public decimal SpecialDiscount { get; set; }
    public decimal SpecialDiscountRate { get; set; }
    public decimal ServiceCharge { get; set; }
    public decimal ServiceChargeRate { get; set; }
    public decimal VatRate { get; set; }
    public decimal VatAmount { get; set; }
    public decimal N_Amount { get; set; }
    public decimal LN_Amount { get; set; }
    public decimal V_Amount { get; set; }
    public decimal Tbl_Amount { get; set; }
    public decimal Tender_Amount { get; set; }
    public decimal Return_Amount { get; set; }
    public string Action_Type { get; set; }
    public string In_Words { get; set; }
    public string Remarks { get; set; }
    public bool R_Invoice { get; set; }
    public string Cancel_By { get; set; }
    public System.DateTime? Cancel_Date { get; set; }
    public string Cancel_Remarks { get; set; }
    public bool Is_Printed { get; set; }
    public int No_Print { get; set; }
    public string Printed_By { get; set; }
    public System.DateTime? Printed_Date { get; set; }
    public bool? Audit_Lock { get; set; }
    public string Enter_By { get; set; }
    public System.DateTime Enter_Date { get; set; }
    public string Reconcile_By { get; set; }
    public System.DateTime? Reconcile_Date { get; set; }
    public string Auth_By { get; set; }
    public System.DateTime? Auth_Date { get; set; }
    public string Cleared_By { get; set; }
    public System.DateTime? Cleared_Date { get; set; }
    public int? DoctorId { get; set; }
    public long? PatientId { get; set; }
    public int? HDepartmentId { get; set; }
    public int? MShipId { get; set; }
    public int? TableId { get; set; }
    public int CBranch_Id { get; set; }
    public int? CUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public bool? IsAPIPosted { get; set; }
    public bool? IsRealtime { get; set; }
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

    //[NotMapped] public DataGridView GetView { get; set; }

    //[NotMapped] public DataGridView GetPaymentInformation { get; set; }

    [NotMapped] public List<SB_Details> DetailsList { get; set; } = new();

    [NotMapped] public List<SB_Term> Terms { get; set; } = new();

    [NotMapped] public List<SB_ExchangeDetails> ExchangeDetailsList { get; set; } = new();
    [NotMapped] public List<SB_Master_OtherDetails> OtherDetailsList { get; set; } = new();
    [NotMapped] public List<ProductAddInfo> ProductAddInfoModels { get; set; } = new();
    [NotMapped] public List<SalesPaymentMode> SalesPaymentModes { get; set; }
    [NotMapped] public List<InvoiceSettlement> Settlements { get; set; }
    //[NotMapped] public DataTable dtPTerm { get; set; }
    //[NotMapped] public DataTable dtBTerm { get; set; }
    //[NotMapped] public DataTable ProductBatch { get; set; }
}