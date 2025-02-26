using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class SalesTermModel : BaseSyncData
{
    public string SB_VNo { get; set; }
    public int ST_Id { get; set; }
    public int SNo { get; set; }
    public string Term_Type { get; set; }
    public long? Product_Id { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public string Taxable { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}