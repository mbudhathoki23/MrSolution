namespace MrDAL.Global.Control;

internal class PublicVariables
{
    // OBJECT FOR THIS FORM
    public static int BranchId;

    public static int InNoOfDecimalPlaces;

    public static bool IsMessageAdd;
    public static bool IsMessageEdit;
    public static bool IsMessageDelete;
    public static bool IsMessageClose;

    public static decimal DecCurrentUserId;
    public static decimal DecCurrentCompanyId;
    public static decimal DecCurrentFinancialYearId;
    public static decimal DecCurrencyId;

    public static string MessageToShow;
    public static string MessageHeader;
    public static string BranchNameAll;
    public static string BranchCode;

    static PublicVariables()
    {
        DecCurrentUserId = new decimal(1);
        DecCurrentCompanyId = new decimal(0);
        DecCurrentFinancialYearId = new decimal(1);
        BranchId = 1;
        IsMessageAdd = true;
        IsMessageEdit = true;
        IsMessageDelete = true;
        IsMessageClose = true;
        DecCurrencyId = new decimal(1);
        InNoOfDecimalPlaces = 2;
        MessageToShow = string.Empty;
        MessageHeader = string.Empty;
        BranchNameAll = "NA";
        BranchCode = string.Empty;
    }
}