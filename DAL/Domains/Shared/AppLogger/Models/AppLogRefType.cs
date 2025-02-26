using System.ComponentModel;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public enum AppLogRefType
{
    // master setup table references 101-199
    [Description("Company Detail")] CompanyDetail = 101,

    [Description("Branch Detail")] BranchDetail = 102,
    [Description("User Detail")] UserDetail = 103,
    [Description("Document Numbering")] DocumentNumbering = 104,
    [Description("Terminal")] Terminal = 105,
    [Description("Sales Term")] SalesTerm = 106,
    [Description("Sales Reutrn Term")] SalesReturnTerm = 107,
    [Description("User Role")] UserRole = 509,
    [Description("User Access Control")] UserAccessControl = 108,
    [Description("CBMS Configuration")] CbmsConfiguration = 109,
    [Description("Master Entry Other")] MasterEntryOther = 110,
    [Description("Branch Rights")] BranchRights = 111,
    [Description("Company Rights")] CompanyRights = 112,
    [Description("CompanyUnitSetup Rights")] CompanyUnitRights = 113,
    [Description("Membership")] Membership = 114,

    // account table references 201-299
    [Description("General Ledger")] GeneralLedger = 201,

    [Description("Ledger Opening")] LedgerOpening = 202,
    [Description("Product Opening")] ProductOpening = 209,
    [Description("Credit Note")] CreditNote = 203,
    [Description("Debit Note")] DebitNote = 204,
    [Description("Journal Voucher")] JournalVoucher = 205,
    [Description("Account Group")] AccountGroup = 206,
    [Description("Account SubGroup")] AccountSubGroup = 207,
    [Description("Account SubLedger")] SubLedger = 208,
    [Description("Account Other")] AccountOther = 299,

    // pos table references 301-399
    [Description("Sales Quotation")] SalesQuotation = 301,

    [Description("Sales Order")] SalesOrder = 302,
    [Description("Sales Challan")] SalesChallan = 303,
    [Description("Sales Invoice")] SalesMaster = 304,
    [Description("Sales Return")] SalesReturnMaster = 305,
    [Description("Temp Sales Invoice")] TempSalesInvoice = 306,
    [Description("Sales Additional")] SalesAdditional = 307,

    // inventory table references 401-499
    [Description("Purchase Indent")] PurchaseIndent = 401,

    [Description("Purchase Order")] PurchaseOrder = 402,
    [Description("Purchase Challan")] PurchaseChallan = 403,
    [Description("Goods In Transit")] GoodsInTransit = 404,
    [Description("Purchase Invoice")] PurchaseInvoice = 405,
    [Description("Purchase Return")] PurchaseReturn = 406,

    [Description("Purchase Additional")] PurchaseAdditional = 407
    // other table references 901-999
}