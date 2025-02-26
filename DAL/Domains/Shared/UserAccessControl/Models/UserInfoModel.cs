using System;

namespace MrDAL.Domains.Shared.UserAccessControl.Models;

public class UserInfoModel
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string EmailId { get; set; }
    public int? BranchId { get; set; }
    public int? RoleId { get; set; }
    public string Role { get; set; }
    public bool IsAdmin { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Active { get; set; }
}