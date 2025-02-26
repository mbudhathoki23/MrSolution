namespace MrDAL.Reports.ViewModule.Object.Register;

public class SalesVatRegisterDetailsDateWise
{
    public string VoucherNo { get; set; }
    public string VoucherDate { get; set; }
    public string VoucherMiti { get; set; }
    public long LedgerId { get; set; }
    public string Ledger { get; set; }
    public string PanNo { get; set; }
    public string Category { get; set; }
    public decimal Qty { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal TaxFree { get; set; }
    public int ExportSales { get; set; }
    public decimal Taxable { get; set; }
    public decimal VatAmount { get; set; }
    public bool R_Invoice { get; set; }
}