using System;

namespace MrDAL.Domains.CRM.Master.Model;

public class ClientSource
{
    public string ActionTag { get; set; }
    public int SourceId { get; set; }
    public string SDescription { get; set; }
    public bool IsActive { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
}