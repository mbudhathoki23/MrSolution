using System.ComponentModel;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public enum UacModule
{
    [Description("Administrative")] Administrative = 1,
    [Description("Master Setup")] Master = 2,
    [Description("Account")] Account = 3,
    [Description("Inventory")] Inventory = 4,
    [Description("Point of Sale")] Pos = 5,
    [Description("Reports")] Reports = 6
}