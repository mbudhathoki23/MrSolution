namespace DatabaseModule.DataEntry.ProductionSystem.Production;

public class Production_Details
{
    public string VoucherNo { get; set; }
    public int SerialNo { get; set; }
    public long ProductId { get; set; }
    public int? GodownId { get; set; }
    public int CostCenterId { get; set; }
    public string BOMNo { get; set; }
    public int? BOMSNo { get; set; }
    public decimal BOMQty { get; set; }
    public string IssueNo { get; set; }
    public int? IssueSNo { get; set; }
    public string OrderNo { get; set; }
    public int? OrderSNo { get; set; }
    public decimal AltQty { get; set; }
    public int? AltUnitId { get; set; }
    public decimal Qty { get; set; }
    public int UnitId { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public string Narration { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}