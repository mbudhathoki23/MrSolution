using System.ComponentModel;

namespace MrDAL.Models.Custom;

public enum MembershipPriceTag
{
    [Description("MRP")] Mrp,

    [Description("Trade")] Trade,

    [Description("Wholesale")] Wholesale,

    [Description("Retail")] Retail,

    [Description("Dealer")] Dealer,

    [Description("Resellar")] Reseller,

    [Description("Sales Rate")] SalesRate
}