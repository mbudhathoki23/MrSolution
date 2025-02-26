using MrDAL.Global.Common;

namespace MrDAL.Reports.ViewModule;

public class VmRegisterReports
{
    public int ColumnsNo { get; set; }
    public int AgingDays { get; set; }

    public int GroupBy { get; set; }
    public int SortOn { get; set; }

    public bool IsDate { get; set; }
    public bool IsDetails { get; set; }
    public bool IsAgentOnly { get; set; }
    public bool IsBillAgent { get; set; }
    public bool IncludingSales { get; set; }
    public bool IncludeRemarks { get; set; }
    public bool IncludeAdjustment { get; set; }
    public bool IncludeNarration { get; set; }
    public bool IncludeReturn { get; set; }
    public bool IsCreditLimit { get; set; }
    public bool PartyLedger { get; set; }
    public bool IncludePdc { get; set; }
    public bool IsCustomer { get; set; }
    public bool IncludeSalesOrder { get; set; }
    public bool IncludeSalesChallan { get; set; }
    public bool IncludeGodown { get; set; }
    public bool IncludeAltQty { get; set; }
    public bool IsHorizon { get; set; }
    public bool IsAdditionalTerm { get; set; }
    public bool AgentOnly { get; set; }
    public bool BillAgent { get; set; }
    public bool CreditLimit { get; set; }
    public bool IncludeFreeQty { get; set; }
    public short Columns { get; set; }
    public short Slab { get; set; }
    public string EntryUser { get; set; }
    public string FilterValue { get; set; }
    public string FilterMode { get; set; }
    public string AccountType { get; set; }
    public string InvoiceType { get; set; }
    public string InvoiceCategory { get; set; }
    public string Module { get; set; }
    public string CurrencyType { get; set; }
    public string RptMode { get; set; }
    public string RptType { get; set; }
    public string RptName { get; set; }
    public string RptDate { get; set; }
    public string PartyType { get; set; }
    public string Opening { get; set; }
    public string AsOnAdDate { get; set; }
    public string FromAdDate { get; set; }
    public string ToAdDate { get; set; }
    public string DateOn { get; set; }
    public string SlabType { get; set; }
    public string CurrencyId { get; set; }
    public string BranchId { get; set; } = ObjGlobal.SysBranchId.ToString();
    public string CompanyUnitId { get; set; } = ObjGlobal.SysCompanyUnitId.ToString();
    public string FiscalYearId { get; set; } = ObjGlobal.SysFiscalYearId.ToString();
    public string AgentId { get; set; }
    public string DocAgentId { get; set; }
    public string LedgerId { get; set; }

    public string[] VatTermId { get; set; } =
    {
        ObjGlobal.SalesVatTermId.ToString()
    };

    public string AreaId { get; set; }
    public string GodownId { get; set; }
    public string ProductId { get; set; }
    public string PGroupId { get; set; }
    public string PSubGroupId { get; set; }
    public string DepartmentId { get; set; }
    public string CounterId { get; set; }
    public double CurrencyRate { get; set; }
}