namespace MrBLL.Utility.Restore.Properties;

public class SqlServerInfo
{
    public SqlServerInfo()
    {
        Server = string.Empty;
        UserName = string.Empty;
        Password = string.Empty;
        IntegratedSecurity = true;
    }

    public bool IntegratedSecurity { get; set; }

    public string Password { get; set; }

    public string Server { get; set; }

    public string UserName { get; set; }
}