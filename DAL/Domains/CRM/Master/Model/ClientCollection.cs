using System;

namespace MrDAL.Domains.CRM.Master.Model;

public class ClientCollection
{
    public string ActionTag { get; set; }
    public long ClientId { get; set; }
    public string ClientDescription { get; set; }
    public decimal? PanNo { get; set; }
    public string ClientAddress { get; set; }
    public string EmailAddress { get; set; }
    public string ContactNo { get; set; }
    public string PhoneNo { get; set; }
    public string CollectionSource { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
}