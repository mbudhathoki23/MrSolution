#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseModule.CloudSync;

public class StockDetail
{
    [Key]
    public int Id { get; set; }
    public string Module { get; set; }
    public string Voucher_No { get; set; }
    public int Serial_No { get; set; }
    public string PurRefVno { get; set; }
    public DateTime Voucher_Date { get; set; }
    public string Voucher_Miti { get; set; }
    public DateTime Voucher_Time { get; set; }
    public long? Ledger_Id { get; set; }
    public int? Subledger_Id { get; set; }
    public int? Agent_Id { get; set; }
    public int? Department_Id1 { get; set; }
    public int? Department_Id2 { get; set; }
    public int? Department_Id3 { get; set; }
    public int? Department_Id4 { get; set; }
    public int Currency_Id { get; set; }
    public decimal Currency_Rate { get; set; }
    public long Product_Id { get; set; }
    public int? Godown_Id { get; set; }
    public int? CostCenter_Id { get; set; }
    public decimal AltQty { get; set; }
    public int? AltUnit_Id { get; set; }
    public decimal Qty { get; set; }
    public int? Unit_Id { get; set; }
    public decimal AltStockQty { get; set; }
    public decimal StockQty { get; set; }
    public decimal FreeQty { get; set; }
    public int FreeUnit_Id { get; set; }
    public decimal StockFreeQty { get; set; }
    public decimal ConvRatio { get; set; }
    public decimal ExtraFreeQty { get; set; }
    public int? ExtraFreeUnit_Id { get; set; }
    public decimal ExtraStockFreeQty { get; set; }
    public decimal Rate { get; set; }
    public decimal BasicAmt { get; set; }
    public decimal TermAmt { get; set; }
    public decimal NetAmt { get; set; }
    public decimal BillTermAmt { get; set; }
    public decimal TaxRate { get; set; }
    public decimal TaxableAmt { get; set; }
    public decimal DocVal { get; set; }
    public decimal ReturnVal { get; set; }
    public decimal StockVal { get; set; }
    public decimal AddStockVal { get; set; }
    public string? PartyInv { get; set; }
    public string EntryType { get; set; }
    public string? AuthBy { get; set; }
    public DateTime? AuthDate { get; set; }
    public string? RecoBy { get; set; }
    public DateTime? RecoDate { get; set; }
    public int? Counter_Id { get; set; }
    public int? RoomNo { get; set; }
    public string? EnterBy { get; set; }
    public DateTime? EnterDate { get; set; }
    public decimal? Adj_Qty { get; set; }
    public string? Adj_VoucherNo { get; set; }
    public string? Adj_Module { get; set; }
    public decimal? SalesRate { get; set; }
    public int Branch_Id { get; set; }
    public int? CmpUnit_Id { get; set; }
    public int FiscalYearId { get; set; }
    public Guid? SyncBaseId { get; set; }
    public Guid SyncGlobalId { get; set; }
    public Guid SyncOriginId { get; set; }
    public DateTime? SyncCreatedOn { get; set; }
    public DateTime? SyncLastPatchedOn { get; set; }
    public short? SyncRowVersion { get; set; }

}