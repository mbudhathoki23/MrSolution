using DatabaseModule.Setup.UserSetup;
using System.Data;

namespace MrDAL.Setup.Interface;

public interface IUserSetupRepository
{
    int SaveUserInfo(string actionTag);
    int SaveUserConfig();

    int ResetLoginLog(string userName);
    int ChangePassword(string userName, string userPassword);

    bool CheckUsernameExits(string userName);
    bool CheckUsernamePassword(string userName, string userPassword);
    bool CheckUsernamePasswordUserType(int userId, string password, string userType);
    (bool auditLog, string serverName) CheckLoginAuditDetails(string userName);

    DataTable GetUserInformationUsingId(int userId);
    DataTable GetUserType(string userName, string password);

    UserInfo UserSetup { get; set; }
}