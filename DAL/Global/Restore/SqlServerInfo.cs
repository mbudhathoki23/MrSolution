using MrDAL.Utility.Server;

namespace MrDAL.Global.Restore;

public class SqlServerInfo
{
    public SqlServerInfo()
    {
        Server = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
        IntegratedSecurity = true;
    }

    public string Server { get; set; } = GetConnection.ServerDesc;
    public string UserName { get; set; } = GetConnection.ServerUserId;
    public string Password { get; set; } = GetConnection.ServerUserPsw;
    public bool IntegratedSecurity { get; set; } = true;
}