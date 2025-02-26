using System.ComponentModel;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public enum AppLogGroup
{
    [Description("Administrative")] Administrative = 1,
    [Description("POS")] Pos = 2,
    [Description("Inventory")] Inventory = 3,
    [Description("Account")] Account = 4,
    [Description("Master")] Master = 5,
    [Description("Other")] Other = 100
}