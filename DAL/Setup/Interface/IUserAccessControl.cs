using DatabaseModule.Setup.UserSetup;

namespace MrDAL.Setup.Interface;

public interface IUserAccessControl
{
    int SaveSecurityRights(string actionTag);
    UserAccessControl AccessControl { get; set; }
}