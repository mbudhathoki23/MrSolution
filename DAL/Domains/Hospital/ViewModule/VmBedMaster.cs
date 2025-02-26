using System;

namespace MrDAL.Domains.Hospital.ViewModule;

public class VmBedMaster
{
    public string _ActionTag { get; set; }
    public int BID { get; set; }
    public string BedDesc { get; set; }
    public string BedShortName { get; set; }
    public int? Bedtype { get; set; }
    public int? WId { get; set; }
    public decimal? ChargeAmt { get; set; }
    public int? Branch_ID { get; set; }
    public int? Company_ID { get; set; }
    public bool? Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime? EnterDate { get; set; }
    public Guid SyncGlobalId { get; set; }
    public Guid SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short? SyncRowVersion { get; set; }
    public Guid SyncBaseId { get; set; }
}