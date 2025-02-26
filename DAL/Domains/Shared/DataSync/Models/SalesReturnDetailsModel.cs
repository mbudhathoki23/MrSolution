using System;
using MrDAL.Domains.Shared.DataSync.Abstractions;

namespace MrDAL.Domains.Shared.DataSync.Models;

public class SalesReturnDetailsModel : BaseSyncData
{
    public string SR_Invoice { get; set; }
    public decimal Invoice_SNo { get; set; }
    public long P_Id { get; set; }
    public int? Gdn_Id { get; set; }
    public decimal? Alt_Qty { get; set; }
    public int? Alt_UnitId { get; set; }
    public decimal Qty { get; set; }
    public int? Unit_Id { get; set; }
    public decimal Rate { get; set; }
    public decimal B_Amount { get; set; }
    public decimal T_Amount { get; set; }
    public decimal N_Amount { get; set; }
    public decimal AltStock_Qty { get; set; }
    public decimal Stock_Qty { get; set; }
    public string? Narration { get; set; }
    public string? SB_Invoice { get; set; }
    public decimal? SB_Sno { get; set; }
    public decimal Tax_Amount { get; set; }
    public decimal V_Amount { get; set; }
    public decimal V_Rate { get; set; }
    public int? Free_Unit_Id { get; set; }
    public decimal Free_Qty { get; set; }
    public decimal StockFree_Qty { get; set; }
    public int? ExtraFree_Unit_Id { get; set; }
    public decimal ExtraFree_Qty { get; set; }
    public decimal ExtraStockFree_Qty { get; set; }
    public bool? T_Product { get; set; }
    public long? S_Ledger { get; set; }
    public long? SR_Ledger { get; set; }
    public string? SZ1 { get; set; }
    public string? SZ2 { get; set; }
    public string? SZ3 { get; set; }
    public string? SZ4 { get; set; }
    public string? SZ5 { get; set; }
    public string? SZ6 { get; set; }
    public string? SZ7 { get; set; }
    public string? SZ8 { get; set; }
    public string? SZ9 { get; set; }
    public string? SZ10 { get; set; }
    public string? Serial_No { get; set; }
    public string? Batch_No { get; set; }
    public DateTime? Exp_Date { get; set; }
    public DateTime? Manu_Date { get; set; }
    public decimal PDiscountRate { get; set; }
    public decimal PDiscount { get; set; }
    public decimal BDiscountRate { get; set; }
    public decimal BDiscount { get; set; }
    public decimal ServiceChargeRate { get; set; }
    public decimal ServiceCharge { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid? SyncGlobalId { get; set; }
    public Guid? SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short? SyncRowVersion { get; set; }
}