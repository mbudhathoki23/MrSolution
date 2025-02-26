using System.ComponentModel;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public enum UacAccessFeature
{
    // administrative  1001-1999
    [Description("Database Backup")] DatabaseBackUp = 1001,

    [Description("Database Restore")] DatabaseRestore = 1002,
    [Description("Document Reprint")] DocumentReprint = 1003,
    [Description("Shift Close")] ShiftClose = 1004,

    // Master 2001- 2999
    [Description("Terminal")] Terminal = 2001,

    [Description("Currency Setup")] CurrencySetup = 2002,
    [Description("Currency Rates")] CurrencyRate = 2003,
    [Description("Product Groups")] ProductGroups = 2004,
    [Description("Product SubGroups")] ProductSubGroups = 2005,
    [Description("Product Units")] ProductUnits = 2006,
    [Description("Products")] Products = 2007,
    [Description("Counter Products")] CounterProducts = 2008,
    [Description("Godowns")] Godowns = 2009,
    [Description("Warehouses")] WareHouses = 2010,
    [Description("Racks")] Racks = 2011,
    [Description("Membership")] PosMembership = 2012,
    [Description("Membership Types")] PosMembershipTypes = 2013,

    // openings 3001-3999
    [Description("Ledgers Openings")] LedgerOpening = 3001,

    [Description("Products Openings")] ProductOpening = 3002,

    // Billing 4001-4999
    [Description("Tax Invoice")] TaxInvoice = 4001,

    [Description("Abbreviated Tax Invoice")]
    AbbreviatedTaxInvoice = 4002,
    [Description("Day Audit")] DayClosing = 4003,
    [Description("Sales Quotation")] SalesQuotation = 4004,
    [Description("Sales Order")] SalesOrder = 4005,
    [Description("Sales Return")] SalesReturn = 4006,
    [Description("Sales Challan")] SalesChallan = 4007,

    // Inventory operations 5001-5999
    [Description("Purchase")] Purchase = 5001,

    [Description("Purchase Return")] PurchaseReturn = 5002,
    [Description("Stock Adjustment")] StockAdjustment = 5003,

    [Description("Product BOM Configuration")]
    ProductBomConfig = 5004,
    [Description("Purchase Quotation")] PurchaseQuotation = 5005,
    [Description("Purchase Challan")] PurchaseChallan = 5006,
    [Description("Purchase Order")] PurchaseOrder = 5007,

    [Description("Purchase Challan Return")]
    PurchaseChallanReturn = 5008,
    [Description("Stock Indent")] StockIndent = 5009,
    [Description("Stock Transfer")] StockTransfer = 5010,
    [Description("Stock Consumption")] StockConsumption = 5011,
    [Description("Product Exchange")] ProductExchange = 5012,
    [Description("Expiry/Breakage")] ExpiryBreakage = 5013,
    [Description("Production")] Production = 5014,
    [Description("Stock Requisition")] StockRequisition = 5015,
    [Description("Raw Material Issue")] RawMaterialIssue = 5016,
    [Description("Raw Material Return")] RawMaterialReturn = 5017,
    [Description("Finished Goods Return")] FinishedGoodsReturn = 5018,
    [Description("Finished Good Receive")] FinishedGoodReceive = 5019,
    [Description("Print Bar Codes")] PrintBarCodes = 5019
}