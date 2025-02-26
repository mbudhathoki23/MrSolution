namespace MrDAL.Reports.ViewModule.Object.Register.SalesInvoiceRegister;

public class VmSalesRegisterProductDetails
{
    public string VoucherNo { get; set; }
    public int SerialNo { get; set; }
    public string ShortName { get; set; }
    public long ProductId { get; set; }
    public string ProductDesc { get; set; }
    public decimal? StockQty { get; set; }
    public string Uom { get; set; }
    public string Rate { get; set; }
    public string BasicAmount { get; set; }
    public string NetAmount { get; set; }
    public string Narration { get; set; }
}