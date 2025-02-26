using System;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public class UserRoleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }

    public bool IsAdmin => Name.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                           Name.Equals("Administrator", StringComparison.OrdinalIgnoreCase);
}