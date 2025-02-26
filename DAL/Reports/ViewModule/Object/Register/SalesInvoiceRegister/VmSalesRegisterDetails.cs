namespace MrDAL.Reports.ViewModule.Object.Register.SalesInvoiceRegister;

public class VmSalesRegisterDetails
{
    public string VoucherNo { get; set; }
    public string VoucherDate { get; set; }
    public string VoucherMiti { get; set; }
    public string VoucherNoWithRef { get; set; }
    public long LedgerId { get; set; }
    public string LedgerDesc { get; set; }
    public string Remarks { get; set; }
    public string Invoice_Mode { get; set; }
    public string Invoice_Type { get; set; }
    public string Payment_Mode { get; set; }
    public string BasicAmount { get; set; }
    public string NetAmount { get; set; }
    public string EnterBy { get; set; }
    public int? CounterId { get; set; }
    public string Counter { get; set; }
    public int? DepartmentId { get; set; }
    public string Department { get; set; }
    public int? LedgerAgentId { get; set; }
    public string LedgerAgent { get; set; }
    public int? DocAgentId { get; set; }
    public string DocAgent { get; set; }
    public int? AreaId { get; set; }
    public string AreaName { get; set; }
}