using System.ComponentModel;

namespace MrDAL.Domains.Shared.AppLogger.Models;

public enum AppLogContext
{
    [Description("User Activity")] UserActivity = 1,
    [Description("Audit")] Audit = 2,

    [Description("User Activity With Audit")]
    UserActivityWithAudit = 3
}