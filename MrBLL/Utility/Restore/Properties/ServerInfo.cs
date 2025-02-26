using System;
using System.Data;
using System.Data.SqlClient;

namespace MrBLL.Utility.Restore.Properties;

internal class ServerInfo
{
    public enum InfoState
    {
        Created,
        Loaded,
        Error
    }

    private const string FieldDatabaseName = "DatabaseName";

    public Exception Error { get; private set; }

    public string Name { get; private set; }

    public InfoState State { get; private set; }

    public static ServerInfo Load(SqlServerInfo serverInfo, string backupFile)
    {
        ServerInfo dbInfo;
        var item = new ServerInfo
        {
            State = InfoState.Created
        };
        var str = $"RESTORE HEADERONLY FROM DISK = {"@Path"}";
        var sqlCommand = SqlServer.GetSqlCommand(serverInfo, str, null);
        sqlCommand.Parameters.Add("@Path", SqlDbType.VarChar, 4096).Value = backupFile;
        try
        {
            sqlCommand.Connection.Open();
            try
            {
                try
                {
                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        if (sqlDataReader.Read()) item.Name = sqlDataReader["DatabaseName"] as string;
                    }

                    item.State = InfoState.Loaded;
                }
                catch (Exception exception)
                {
                    item.Error = exception;
                    item.State = InfoState.Error;
                }
            }
            finally
            {
                sqlCommand.Connection.Close();
            }

            return item;
        }
        catch (SqlException sqlException)
        {
            item.Error = sqlException;
            item.State = InfoState.Error;
            dbInfo = item;
        }

        return dbInfo;
    }
}