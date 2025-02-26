namespace MrDAL.Reports.ViewModule;

public class VmFinanceReports
{
    public int GroupBy { get; set; } = 0;

    public bool IsNormal { get; set; } = false;
    public bool IsCustomer { get; set; } = false;
    public bool IsProfitLoss { get; set; } = false;
    public bool IsGroupWise { get; set; } = false;
    public bool IsDetails { get; set; } = false;
    public bool IsSummary { get; set; } = false;
    public bool IsAdditionalTerm { get; set; } = false;
    public bool IsHorizontal { get; set; } = false;
    public bool IsSubLedger { get; set; } = false;
    public bool IsTFormat { get; set; } = false;
    public bool IsCombineSales { get; set; } = false;
    public bool IsCombineCustomerVendor { get; set; } = false;
    public bool IsRemarks { get; set; } = false;
    public bool IsNarration { get; set; } = false;
    public bool IsAll { get; set; } = false;
    public bool IsDate { get; set; } = false;
    public bool IsPostingDetails { get; set; } = false;
    public bool IsProductDetails { get; set; } = false;
    public bool IncludeUdf { get; set; } = false;
    public bool IsDnCnDetails { get; set; } = false;
    public bool IsIncludePdc { get; set; } = false;
    public bool IsOpeningOnly { get; set; } = false;
    public bool IsZeroBalance { get; set; } = false;
    public bool IsClosingStock { get; set; } = false;
    public bool IsRePostValue { get; set; } = false;
    public bool IncludeVoucherTotal { get; set; } = false;
    public bool IncludeLedger { get; set; } = false;
    public bool IncludeShortName { get; set; } = false;
    public bool IsTrialBalance { get; set; } = false;
    public bool IsBalanceSheet { get; set; } = false;
    public bool IsLedgerContactDetails { get; set; } = false;
    public bool IsLedgerScheme { get; set; } = false;


    public string ReportType { get; set; } = string.Empty;
    public string ReportDesc { get; set; } = string.Empty;
    public string BranchId { get; set; } = string.Empty;
    public string CompanyUnitId { get; set; } = string.Empty;
    public string FiscalYearId { get; set; } = string.Empty;
    public string AccountGroupId { get; set; } = string.Empty;
    public string AccountSubGroupId { get; set; } = string.Empty;
    public string LedgerId { get; set; } = string.Empty;
    public string SubLedgerId { get; set; } = string.Empty;
    public string DepartmentId { get; set; } = string.Empty;
    public string MainAgentId { get; set; } = string.Empty;
    public string AgentId { get; set; } = string.Empty;
    public string MainAreaId { get; set; } = string.Empty;
    public string AreaId { get; set; } = string.Empty;
    public string ReportDate { get; set; } = string.Empty;
    public string PartyType { get; set; } = string.Empty;
    public string AsOnDate { get; set; } = string.Empty;
    public string FromDate { get; set; } = string.Empty;
    public string ToDate { get; set; } = string.Empty;
    public string OpenFor { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public string InvoiceType { get; set; } = string.Empty;
    public string Find { get; set; } = string.Empty;
    public string AccountType { get; set; } = string.Empty;
    public string SlCode { get; set; } = string.Empty;
    public string AgentCode { get; set; } = string.Empty;
    public string VoucherNo { get; set; } = string.Empty;
    public string InvoiceNo { get; set; } = string.Empty;
    public string ProductId { get; set; } = string.Empty;
    public string GroupId { get; set; } = string.Empty;
    public string SubGroupId { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string CurrencyType { get; set; } = string.Empty;
    public string FilterFor { get; set; } = string.Empty;
    public string PLAcDesc { get; set; } = string.Empty;
    public string SortOn { get; set; } = "DESCRIPTION";

    public double Cur_Rate { get; set; } = 0;
    public double PlAmount { get; set; } = 0;
}