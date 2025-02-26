namespace MrDAL.Reports.ViewModule;

public class VmStockReports
{
    public int Event { get; set; }
    public bool Godown { get; set; }
    public bool WithValue { get; set; }
    public bool AltQty { get; set; }
    public bool IncludeZeroBalance { get; set; }
    public bool NegativeBal { get; set; }
    public bool IncludeNegative { get; set; }
    public bool NegativeStockOnly { get; set; }
    public bool ExcludeNegative { get; set; }
    public bool IncludeAltQty { get; set; }
    public bool IncludeVAT { get; set; }
    public bool BuyingPrice { get; set; }
    public bool SalesOrder { get; set; }
    public bool NetRequirement { get; set; }
    public bool IsGroupWise { get; set; }
    public bool IsSubGroupWise { get; set; }
    public bool IsSummary { get; set; }
    public bool IsRemarks { get; set; }
    public bool IsDate { get; set; }
    public bool IncludeReturn { get; set; }
    public bool IncludeValue { get; set; }
    public bool IncludeProduct { get; set; }
    public bool IncludeBatch { get; set; }
    public bool IncludeRefBill { get; set; }
    public bool DepartmentWise { get; set; }
    public bool RePostValue { get; set; }
    public bool BalanceQty { get; set; }
    public bool CostRatio { get; set; }

    public string SubGroupBy { get; set; }
    public string OnBasis { get; set; }
    public string OpenFor { get; set; }
    public string GroupBy { get; set; }
    public string RptType { get; set; }
    public string RptName { get; set; }
    public string RptDate { get; set; }
    public string RptMode { get; set; }
    public string BranchId { get; set; }
    public string CompanyUnitId { get; set; }
    public string FiscalYearId { get; set; }
    public string AsOnDate { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string FromDateFc { get; set; }
    public string ToDateFc { get; set; }
    public string Currency { get; set; }
    public string FilterValue { get; set; }
    public string LedgerId { get; set; }
    public string DepartmentId { get; set; }
    public string GodownId { get; set; }
    public string CostCenterId { get; set; }
    public string AreaId { get; set; }
    public string ProductGroupId { get; set; }
    public string ProductSubGroupId { get; set; }
    public string ProductId { get; set; }
    public string OrderNo { get; set; }
    public string Module { get; set; }
    public string NegativeBalance { get; set; }
    public string SortOn { get; set; }
}