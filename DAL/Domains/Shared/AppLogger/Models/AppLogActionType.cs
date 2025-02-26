using System.ComponentModel;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public enum AppLogActionType
{
    [Description("NEW")] New = 1,
    [Description("UPDATE")] Update = 2,
    [Description("DELETE")] Delete = 3,
    [Description("REVERSE")] Reverse = 4,
    [Description("RE-PRINT")] Reprint = 5,
    [Description("MIXED")] Mixed = 6,
    [Description("OTHER")] Other = 100
}