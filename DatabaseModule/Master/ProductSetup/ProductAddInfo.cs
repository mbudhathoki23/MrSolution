namespace DatabaseModule.Master.ProductSetup;

public class ProductAddInfo
{
    public string Module { get; set; }
    public string VoucherNo { get; set; }
    public string VoucherType { get; set; }
    public long ProductId { get; set; }
    public int Sno { get; set; }
    public string SizeNo { get; set; }
    public string SerialNo { get; set; }
    public string BatchNo { get; set; }
    public string ChasisNo { get; set; }
    public string EngineNo { get; set; }
    public string VHModel { get; set; }
    public string VHColor { get; set; }
    public System.DateTime? MFDate { get; set; }
    public System.DateTime? ExpDate { get; set; }
    public decimal? Mrp { get; set; }
    public decimal? Rate { get; set; }
    public decimal? AltQty { get; set; }
    public decimal Qty { get; set; }
    public int BranchId { get; set; }
    public int? CompanyUnitId { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public System.Guid SyncBaseId { get; set; }
    public System.Guid SyncGlobalId { get; set; }
    public System.Guid SyncOriginId { get; set; }
    public System.DateTime? SyncCreatedOn { get; set; }
    public System.DateTime? SyncLastPatchedOn { get; set; }
    public short SyncRowVersion { get; set; }
}