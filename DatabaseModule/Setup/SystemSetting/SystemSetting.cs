using System;

namespace DatabaseModule.Setup.SystemSetting;

public class SystemSetting
{
    public byte SyId { get; set; }
    public bool? EnglishDate { get; set; }
    public bool? AuditTrial { get; set; }
    public bool? Udf { get; set; }
    public bool? Autopoplist { get; set; }
    public bool? CurrentDate { get; set; }
    public bool? ConformSave { get; set; }
    public bool? ConformCancel { get; set; }
    public bool? ConformExits { get; set; }
    public decimal? CurrencyRate { get; set; }
    public int? CurrencyId { get; set; }
    public string DefaultPrinter { get; set; }
    public string AmountFormat { get; set; }
    public string RateFormat { get; set; }
    public string QtyFormat { get; set; }
    public string CurrencyFormatF { get; set; }
    public int? DefaultFiscalYearId { get; set; }
    public string DefaultOrderPrinter { get; set; }
    public string DefaultInvoicePrinter { get; set; }
    public string DefaultOrderNumbering { get; set; }
    public string DefaultInvoiceNumbering { get; set; }
    public string DefaultAvtInvoiceNumbering { get; set; }
    public string DefaultOrderDesign { get; set; }
    public bool? IsOrderPrint { get; set; }
    public bool? IsPrintBranch { get; set; }
    public string DefaultInvoiceDesign { get; set; }
    public bool? IsInvoicePrint { get; set; }
    public string DefaultAvtDesign { get; set; }
    public string DefaultFontsName { get; set; }
    public int? DefaultFontsSize { get; set; }
    public string DefaultPaperSize { get; set; }
    public string DefaultReportStyle { get; set; }
    public bool? DefaultPrintDateTime { get; set; }
    public string DefaultFormColor { get; set; }
    public string DefaultTextColor { get; set; }
    public int? DebtorsGroupId { get; set; }
    public int? CreditorGroupId { get; set; }
    public long? SalaryLedgerId { get; set; }
    public long? TDSLedgerId { get; set; }
    public long? PFLedgerId { get; set; }
    public string DefaultEmail { get; set; }
    public string DefaultEmailPassword { get; set; }
    public int? BackupDays { get; set; }
    public string BackupLocation { get; set; }
    public bool? IsNightAudit { get; set; }
    public bool? SearchAlpha { get; set; }
    public bool? BarcodeAutoSearch { get; set; }
    public TimeSpan? EndTime { get; set; }
}