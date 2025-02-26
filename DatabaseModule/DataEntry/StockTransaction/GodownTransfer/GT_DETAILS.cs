namespace DatabaseModule.DataEntry.StockTransaction.GodownTransfer;

public class GT_DETAILS
{
    public string VoucherNo { get; set; }
    public int SNo { get; set; }
    public long ProId { get; set; }
    public int ToGdn { get; set; }
    public int? Cls1 { get; set; }
    public int? Cls2 { get; set; }
    public int? Cls3 { get; set; }
    public int? Cls4 { get; set; }
    public decimal AltQty { get; set; }
    public int? AltUOM { get; set; }
    public decimal Qty { get; set; }
    public int? UOM { get; set; }
    public decimal Rate { get; set; }
    public decimal Amount { get; set; }
    public string Narration { get; set; }
    public string EnterBy { get; set; }
    public System.DateTime EnterDate { get; set; }
    public string ModifyAction { get; set; }
    public string ModifyBy { get; set; }
    public System.DateTime? ModifyDate { get; set; }

}