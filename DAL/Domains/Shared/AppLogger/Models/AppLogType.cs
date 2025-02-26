using System.ComponentModel;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public enum AppLogType
{
    // administrative  101-199
    [Description("Database Backup")] DatabaseBackUp = 101,

    [Description("Database Restore")] DatabaseRestore = 102,
    [Description("Logged In")] LoggedIn = 103,
    [Description("Logged Out")] LoggedOut = 104,
    [Description("Document Reprint")] DocumentReprint = 105,
    [Description("Shift Closed")] ShiftClosed = 106,
    [Description("Administrative Other")] AdministrativeOther = 199,

    // pos 201-299
    [Description("Sales Invoice")] SalesInvoice = 201,

    [Description("Sales Challan")] SalesChallan = 202,
    [Description("Sales Quotation")] SalesQuotation = 203,
    [Description("Sales Challan Return")] SalesChallanReturn = 204,
    [Description("Bar Code Print")] BarCodePrint = 205,
    [Description("Sales Return")] SalesReturn = 206,
    [Description("Membership")] Membership = 207,
    [Description("Temporary Invoice")] TemporaryInvoice = 208,
    [Description("POS Other")] PosOther = 299,

    // inventory 301-399
    [Description("Purchase")] Purchase = 301,

    [Description("Purchase Return")] PurchaseReturn = 302,
    [Description("Stock Adjustment")] StockAdjustment = 303, //CREATE, EDIT, DELETED

    [Description("Product BOM Configuration")]
    ProductBomConfig = 304,

    [Description("Purchase Quotation")] PurchaseQuotation = 305,
    [Description("Purchase Challan")] PurchaseChallan = 306,
    [Description("Purchase Order")] PurchaseOrder = 307,

    [Description("Purchase Challan Return")]
    PurchaseChallanReturn = 308,

    [Description("Stock Indent")] StockIndent = 309,
    [Description("Stock Transfer")] StockTransfer = 310,
    [Description("Stock Consumption")] StockConsumption = 311,
    [Description("Product Detail")] ProductDetail = 312,
    [Description("Product Unit")] ProductUnit = 313,
    [Description("Godown")] Godown = 314,
    [Description("Product Exchange")] ProductExchange = 315,
    [Description("Expiry/Breakage")] ExpiryBreakage = 316,
    [Description("Production")] Production = 316,
    [Description("Stock Requisition")] StockRequisition = 317,
    [Description("Raw Material Issue")] RawMaterialIssue = 318,
    [Description("Raw Material Return")] RawMaterialReturn = 319,
    [Description("Finished Goods Return")] FinishedGoodsReturn = 320,
    [Description("Finished Good Receive")] FinishedGoodReceive = 321,
    [Description("Opening Stock")] OpeningStock = 322,
    [Description("Inventory Other")] InventoryOther = 323,

    // account 401-499
    [Description("General Ledger")] GeneralLedger = 401,

    [Description("Ledger Opening")] LedgerOpening = 402,
    [Description("Credit Note")] CreditNote = 403,
    [Description("Debit Note")] DebitNote = 404,
    [Description("Journal Voucher")] JournalVoucher = 405,
    [Description("Account Group")] AccountGroup = 406,
    [Description("Account SubGroup")] AccountSubGroup = 407,
    [Description("Account SubLedger")] SubLedger = 408,
    [Description("Account Other")] AccountOther = 499,

    // master entry 501-599
    //[Description("")]
    //PartyInfoEdited = 501,

    [Description("Company Detail")] CompanyDetail = 502,
    [Description("Branch Detail")] BranchDetail = 503,
    [Description("User Detail")] UserDetail = 504,
    [Description("Document Numbering")] DocumentNumbering = 505,
    [Description("Terminal")] Terminal = 506,
    [Description("Sales Term")] SalesTerm = 507,
    [Description("Sales Reutrn Term")] SalesReturnTerm = 508,
    [Description("User Role")] UserRole = 509,
    [Description("User Access Control")] UserAccessControl = 510,
    [Description("CBMS Configuration")] CbmsConfiguration = 511,
    [Description("Master Entry Other")] MasterEntryOther = 512,
    [Description("Branch Rights Detail")] BranchRightsDetail = 513,
    [Description("Company Rights Detail")] CompanyRightsDetail = 514,

    [Description("CompanyUnitSetup Rights Detail")]
    CompanyUnitRightsDetail = 515,

    // other or un-sorted 801-899
    [Description("Data Export")] GridExport = 801,

    [Description("Report Print")] ReportPrinted = 802,
    [Description("Other")] Other = 899
}