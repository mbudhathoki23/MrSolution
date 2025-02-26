using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class ProductAddInfoModel : BaseSyncData
{
    public string Module { get; set; }
    public string VoucherNo { get; set; }
    public string VoucherType { get; set; }
    public long ProductId { get; set; }
    public int Sno { get; set; }
    public string? SizeNo { get; set; }
    public string? SerialNo { get; set; }
    public string? BatchNo { get; set; }
    public string? ChasisNo { get; set; }
    public string? EngineNo { get; set; }
    public string? VHModel { get; set; }
    public string? VHColor { get; set; }
    public DateTime? MFDate { get; set; }
    public DateTime? ExpDate { get; set; }
    public decimal? Mrp { get; set; }
    public decimal? Rate { get; set; }
    public decimal? AltQty { get; set; }
    public decimal Qty { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}