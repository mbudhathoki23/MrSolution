using System.ComponentModel;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public enum UacFeatureGroup
{
    // Master Setup
    [Description("Opening Master")] OpeningMaster,

    [Description("Currency Master")] CurrencyMaster,
    [Description("Product Master")] ProductMaster,
    [Description("Godown Master")] GodownMaster,
    [Description("Membership Master")] MembershipMaster,

    // Account

    // Inventory
    [Description("Inventory Operations")] InventoryOperations,

    // POS
    [Description("Sales Quotation")] SalesQuotation,

    [Description("Sales Order")] SalesOrder,
    [Description("Billing")] Billing,
    [Description("Sales Return")] SalesReturn,
    [Description("Sales Challan")] SalesChallan

    // _Reports
}