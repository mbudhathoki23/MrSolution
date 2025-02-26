using DatabaseModule.CloudSync;

namespace DatabaseModule.Master.ProductSetup;

public class ProductGroup : BaseSyncData
{
    public int PGrpId { get; set; }
    public string NepaliDesc { get; set; }
    public string GrpName { get; set; }
    public string GrpCode { get; set; }
    public decimal? GMargin { get; set; }
    public string GPrinter { get; set; }
    public long? PurchaseLedgerId { get; set; }
    public long? PurchaseReturnLedgerId { get; set; }
    public long? SalesLedgerId { get; set; }
    public long? SalesReturnLedgerId { get; set; }
    public long? OpeningStockLedgerId { get; set; }
    public long? ClosingStockLedgerId { get; set; }
    public long? StockInHandLedgerId { get; set; }
    public int Branch_ID { get; set; }
    public int? Company_ID { get; set; }
    public bool Status { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid? SyncBaseId { get; set; }
    public System.Guid? SyncGlobalId { get; set; }
    public System.Guid? SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}