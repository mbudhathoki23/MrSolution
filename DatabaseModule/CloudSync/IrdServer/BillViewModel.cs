namespace DatabaseModule.CloudSync.IrdServer;

public class BillViewModel
{
    public string username { get; set; }
    public string password { get; set; }
    public string seller_pan { get; set; }
    public string buyer_pan { get; set; }
    public string fiscal_year { get; set; }
    public string buyer_name { get; set; }
    public string invoice_number { get; set; }
    public string invoice_date { get; set; }
    public decimal total_sales { get; set; }
    public decimal? taxable_sales_vat { get; set; }
    public decimal? vat { get; set; }
    public double? excisable_amount { get; set; }
    public double? excise { get; set; }
    public double? taxable_sales_hst { get; set; }
    public double? hst { get; set; }
    public double? amount_for_esf { get; set; }
    public double? esf { get; set; }
    public double? export_sales { get; set; }
    public double? tax_exempted_sales { get; set; }
    public bool isrealtime { get; set; }
    public string datetimeClient { get; set; }
}