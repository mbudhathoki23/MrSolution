namespace MrDAL.Domains.Shared.DataSync.Common;

public enum SyncRepoType
{
    Branch,
    CompanyUnit,
    Currency,

    AccountGroup,
    AccountSubGroup,

    MainAgent,
    Agent,

    MainArea,
    Area,

    GeneralLedger,
    SubLedger,

    MemberType,
    MemberShipSetUp,
    GiftVoucherGenerate,

    Department,
    Terminal,
    Floor,
    Table,
    Godown,
    CostCenter,

    ProductGroup,
    ProductSubGroup,
    ProductUnit,
    Product,

    Rack,
    NarrationAndRemarks,

    LedgerOpening,
    ProductOpening,

    PurchaseIndent,
    PurchaseOrder,
    PurchaseChallan,
    PurchaseChallanReturn,
    PurchaseInvoice,
    PurchaseReturn,
    PurchaseAdditionalInvoice,

    SalesQuotation,
    SalesOrder,
    SalesChallan,
    SalesInvoice,
    SalesReturns,
    SalesAdditionalInvoice,

    JournalVoucher,
    PostDatedCheque,
    CashBankVoucher,

    StockAdjustment,
    GodownTransfer,
    BillOfMaterials,
    Production
}