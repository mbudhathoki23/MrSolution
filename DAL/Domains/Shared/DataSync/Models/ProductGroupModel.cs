using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class ProductGroupModel : BaseSyncData
{
    public int PGrpId { get; set; }
    public string? NepaliDesc { get; set; }
    public string GrpName { get; set; }
    public string GrpCode { get; set; }
    public decimal? GMargin { get; set; }
    public string? Gprinter { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_Id { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public DateTime EnterDate { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
    public long? PurchaseLedgerId { get; set; }
    public long? PurchaseReturnLedgerId { get; set; }
    public long? SalesLedgerId { get; set; }
    public long? SalesReturnLedgerId { get; set; }
    public long? OpeningStockLedgerId { get; set; }
    public long? ClosingStockLedgerId { get; set; }
    public long? StockInHandLedgerId { get; set; }
}