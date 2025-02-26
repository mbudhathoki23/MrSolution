using System.Collections.Generic;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public class UserRolePermissionConfigModel
{
    public List<UacAction> Actions { get; set; }
}