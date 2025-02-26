using System;

namespace DatabaseModule.Print.SalesInvoice;

public class SalesInvoiceMaster
{
    public string SB_Invoice { get; set; }
    public DateTime Invoice_Date { get; set; }
    public string Invoice_Miti { get; set; }
    public string PB_Vno { get; set; }
    public string GLName { get; set; }
    public string NepaliDesc { get; set; }
    public string PanNo { get; set; }
    public string GLAddress { get; set; }
    public string GLCode { get; set; }
    public string ACCode { get; set; }
    public string GLType { get; set; }
    public long? PartyLedgerId { get; set; }
    public string Party_Name { get; set; }
    public string Vat_No { get; set; }
    public string Contact_Person { get; set; }
    public string Mobile_No { get; set; }
    public string Address { get; set; }
    public string ChqNo { get; set; }
    public DateTime? ChqDate { get; set; }
    public string ChqMiti { get; set; }
    public string Invoice_Type { get; set; }
    public string Invoice_Mode { get; set; }
    public string Payment_Mode { get; set; }
    public int DueDays { get; set; }
    public DateTime? DueDate { get; set; }
    public string AgentName { get; set; }
    public string SubLedgerName { get; set; }
    public string SO_Invoice { get; set; }
    public DateTime? SO_Date { get; set; }
    public string SC_Invoice { get; set; }
    public DateTime? SC_Date { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public string CounterName { get; set; }
    public string CurrencyName { get; set; }
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
    public string In_Words { get; set; }
    public string Remarks { get; set; }
    public bool Is_Printed { get; set; }
    public int No_Print { get; set; }
    public string Printed_By { get; set; }
    public DateTime? Printed_Date { get; set; }
    public string Enter_By { get; set; }
    public DateTime Enter_Date { get; set; }
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public string PaitentContactNo { get; set; }
    public string PaitentTAddress { get; set; }
    public string HospitalDepartmentName { get; set; }
    public string MemberName { get; set; }
    public int? MemberId { get; set; }
    public string TableName { get; set; }
}