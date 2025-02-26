using System;
using System.Data;
using System.Data.SqlClient;

namespace MrBLL.Utility.Restore.Properties;

internal class DbExecStatus
{
    private const string RequestCommand =
        "SELECT r.session_id,CONVERT(NUMERIC(6,2),r.percent_complete) AS [Percent Complete],CONVERT(VARCHAR(20),DATEADD(ms,r.estimated_completion_time,GetDate()),20) AS [ETA Completion Time], CONVERT(NUMERIC(10,2),r.total_elapsed_time/1000.0/60.0) AS [Elapsed Min], CONVERT(NUMERIC(10,2),r.estimated_completion_time/1000.0/60.0) AS [ETA Min], CONVERT(NUMERIC(10,2),r.estimated_completion_time/1000.0/60.0/60.0) AS [ETA Hours], CONVERT(VARCHAR(1000),(SELECT SUBSTRING(text,r.statement_start_offset/2, CASE WHEN r.statement_end_offset = -1 THEN 1000 ELSE (r.statement_end_offset-r.statement_start_offset)/2 END) FROM sys.dm_exec_sql_text(sql_handle))) FROM sys.dm_exec_requests r WHERE r.session_id = {0} AND r.command LIKE '%{1}%'";

    public DbExecStatus(SqlConnection connection, short sessionId)
    {
        Connection = connection;
        SessionId = sessionId;
    }

    public DbExecStatus(SqlConnection connection, short sessionId, string commandTemplate) : this(connection,
        sessionId)
    {
        CommandTemplate = commandTemplate;
    }

    public string CommandTemplate { get; set; }

    public SqlConnection Connection { get; }

    public decimal PercentComplete { get; private set; }

    public short SessionId { get; }

    public void UpdateStatus()
    {
        var state = Connection.State;
        if (state == ConnectionState.Closed || state == ConnectionState.Broken) Connection.Open();
        var str =
            $"SELECT r.session_id,CONVERT(NUMERIC(6,2),r.percent_complete) AS [Percent Complete],CONVERT(VARCHAR(20),DATEADD(ms,r.estimated_completion_time,GetDate()),20) AS [ETA Completion Time], CONVERT(NUMERIC(10,2),r.total_elapsed_time/1000.0/60.0) AS [Elapsed Min], CONVERT(NUMERIC(10,2),r.estimated_completion_time/1000.0/60.0) AS [ETA Min], CONVERT(NUMERIC(10,2),r.estimated_completion_time/1000.0/60.0/60.0) AS [ETA Hours], CONVERT(VARCHAR(1000),(SELECT SUBSTRING(text,r.statement_start_offset/2, CASE WHEN r.statement_end_offset = -1 THEN 1000 ELSE (r.statement_end_offset-r.statement_start_offset)/2 END) FROM sys.dm_exec_sql_text(sql_handle))) FROM sys.dm_exec_requests r WHERE r.session_id = {SessionId} AND r.command LIKE '%{"RESTORE DATABASE"}%'";
        var sqlCommand = Connection.CreateCommand();
        sqlCommand.CommandText = str;
        try
        {
            using var sqlDataReader = sqlCommand.ExecuteReader();
            if (sqlDataReader.Read()) PercentComplete = (decimal)sqlDataReader["Percent Complete"];
        }
        finally
        {
            switch (state)
            {
                case ConnectionState.Closed:
                case ConnectionState.Broken:
                    Connection.Close();
                    break;

                case ConnectionState.Open:
                    break;

                case ConnectionState.Connecting:
                    break;

                case ConnectionState.Executing:
                    break;

                case ConnectionState.Fetching:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}