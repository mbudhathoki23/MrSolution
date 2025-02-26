using System.ComponentModel;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public enum UacAction
{
    [Description("All")] All = 0,

    [Description("Create or Register new")]
    Create = 1,
    [Description("Modify existing")] Modify = 2,
    [Description("Delete")] Delete = 3,

    [Description("Print the document")] Print = 4,

    [Description("Reprint the document or voucher")]
    Reprint = 5,
    [Description("Reverse")] Reverse = 6,
    [Description("Import From External")] ImportExternal = 7,
    [Description("Allow Rate Change")] ChangeRate = 8,

    [Description("Allow Item-Wise Discount")]
    ItemWiseDiscount = 9,
    [Description("Allow Bill Discount")] BillDiscount = 10,

    [Description("Hold Or Create Temporary Record")]
    CreateTemp = 11,

    [Description("Export the document or voucher")]
    Export = 12
    //[Description("Change Rate")] ChangeRate = 8,
    //[Description("Change Rate")] ChangeRate = 8,
}