using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class SalesMasterOtherDetailsModel : BaseSyncData
{
    public string SB_Invoice { get; set; }
    public string? Transport { get; set; }
    public string? VechileNo { get; set; }
    public string? BiltyNo { get; set; }
    public string? Package { get; set; }
    public DateTime? BiltyDate { get; set; }
    public string? BiltyType { get; set; }
    public string? Driver { get; set; }
    public string? PhoneNo { get; set; }
    public string? LicenseNo { get; set; }
    public string? MailingAddress { get; set; }
    public string? MCity { get; set; }
    public string? MState { get; set; }
    public string? MCountry { get; set; }
    public string? MEmail { get; set; }
    public string? ShippingAddress { get; set; }
    public string? SCity { get; set; }
    public string? SState { get; set; }
    public string? SCountry { get; set; }
    public string? SEmail { get; set; }
    public string? ContractNo { get; set; }
    public DateTime? ContractDate { get; set; }
    public string? ExportInvoice { get; set; }
    public DateTime? ExportInvoiceDate { get; set; }
    public string? VendorOrderNo { get; set; }
    public string? BankDetails { get; set; }
    public string? LcNumber { get; set; }
    public string? CustomDetails { get; set; }
}