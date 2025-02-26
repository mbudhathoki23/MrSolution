using System.ComponentModel;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public enum UacRoleType
{
    [Description("Administrator")] Administrator,
    [Description("Normal")] Normal
}