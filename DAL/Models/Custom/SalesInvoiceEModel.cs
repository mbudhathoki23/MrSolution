using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using System;
using System.Collections.Generic;

// ReSharper disable InconsistentNaming

namespace MrDAL.Models.Custom;

public class SalesInvoiceEModel
{
    public string RefTempInvoiceId { get; set; }
    public long LedgerId { get; set; }
    public string PartyName { get; set; }
    public string VatNo { get; set; }
    public string ContactPerson { get; set; }
    public string MobileNo { get; set; }
    public string Address { get; set; }
    public string ChqNo { get; set; }
    public DateTime? ChqDate { get; set; }
    public string InvoiceType { get; set; }
    public string InvoiceMode { get; set; }
    public string PaymentMode { get; set; }
    public int? DueDays { get; set; }
    public DateTime? DueDate { get; set; }
    public int? AgentId { get; set; }
    public int? SubLedgerId { get; set; }
    public int? CounterId { get; set; }
    public int? CurrencyId { get; set; }
    public decimal? CurrencyRate { get; set; }
    public decimal NAmount { get; set; }
    public decimal TaxableAmount { get; set; }
    public decimal NonTaxableAmount { get; set; }
    public decimal NetAmount { get; set; }
    public decimal LocalNetAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TenderAmount { get; set; }
    public decimal ReturnAmount { get; set; }
    public string ActionType { get; set; }
    public string Remarks { get; set; }
    public bool Printed { get; set; }
    public string EnteredBy { get; set; }
    public int CBranchId { get; set; }
    public bool? IsAPIPosted { get; set; }
    public bool? IsRealtime { get; set; }
    public int FiscalYearId { get; set; }
    public int? MembershipId { get; set; }
    public DateTime CurrentDateTime { get; set; }
    public decimal TermAmount { get; set; }
    public Guid? SyncOriginId { get; set; }
    public SalesInvoicePaymentMode PaymentModeE { get; set; }
    public IList<SB_Term> Terms { get; set; }
    public IList<SB_Details> Items { get; set; }
}