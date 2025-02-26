using MrDAL.Core.Logging;
using MrDAL.Core.Utils;
using MrDAL.Models.Common;
using MrDAL.Utility.Analytics.Models;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable All

namespace MrDAL.Utility.Analytics;

public class SqlLiteClientLogService
{
    public async Task<ListResult<SqliteClientLogModel>> GetLogsAsync(int? logTypeAlias, bool? synced)
    {
        var result = new ListResult<SqliteClientLogModel>();
        var sql = new StringBuilder(@"SELECT * FROM Log ");

        var whereAdded = false;

        using var conn = new SQLiteConnection(Common.SqliteConnString);
        using var cmd = new SQLiteCommand(conn)
        {
            CommandType = CommandType.Text
        };

        if (logTypeAlias.HasValue)
        {
            sql.Append("WHERE log_type_alias = @prAlias ");
            cmd.Parameters.Add("prAlias", DbType.Int32).Value = logTypeAlias.Value;
            whereAdded = true;
        }

        if (synced.HasValue)
        {
            sql.Append(whereAdded ? "AND " : "WHERE ");
            sql.Append("synced_on IS " + (synced.Value ? "NOT NULL" : "NULL"));
        }

        cmd.CommandText = sql.ToString();

        try
        {
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            var logs = new List<SqliteClientLogModel>();

            while (await reader.ReadAsync())
                logs.Add(new SqliteClientLogModel
                {
                    Dump = reader["dump"] == DBNull.Value ? null : (string)reader["dump"],
                    LogTime = (DateTime)reader["log_time"],
                    ImageId = reader["image_id"] == DBNull.Value ? null : (Guid?)reader["image_id"],
                    Id = (Guid)reader["Id"],
                    SyncedOn = reader["Synced_on"] == DBNull.Value ? null : (DateTime?)reader["Synced_on"],
                    OtherData = reader["other_data"] == DBNull.Value ? null : (string)reader["other_data"],
                    LogType = (string)reader["log_type"],
                    LogTypeAlias = (int)reader["log_type_alias"],
                    Machine = (string)reader["machine"],
                    MachineUser = (string)reader["machine_user"],
                    Message = (string)reader["message"],
                    LastUpdateOn = reader["last_update_on"] == DBNull.Value
                        ? null
                        : (DateTime?)reader["last_update_on"]
                });

            result.List = logs;
            result.Success = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            result = e.ToListErrorResult<SqliteClientLogModel>(this);
        }

        return result;
    }

    public async Task<ListResult<SqliteClientLogImageModel>> GetImagesAsync()
    {
        var result = new ListResult<SqliteClientLogImageModel>();

        try
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand("SELECT * FROM ImgContent ", conn)
            {
                CommandType = CommandType.Text
            };

            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            var images = new List<SqliteClientLogImageModel>();

            while (await reader.ReadAsync())
                images.Add(new SqliteClientLogImageModel
                {
                    Id = (Guid)reader["Id"],
                    Image = reader["image"] == DBNull.Value ? new byte[0] : (byte[])reader["image"],
                    DateTime = (DateTime)reader["datetime"],
                    LastUpdateOn = reader["last_update_on"] == DBNull.Value
                        ? null
                        : (DateTime?)reader["last_update_on"],
                    Machine = (string)reader["machine"],
                    MachineUser = (string)reader["machine_user"],
                    SyncedOn = reader["synced_on"] == DBNull.Value ? null : (DateTime?)reader["synced_on"]
                });

            result.List = images;
            result.Success = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            result = e.ToListErrorResult<SqliteClientLogImageModel>(this);
        }

        return result;
    }

    public async Task<ListResult<ClientIssueReportModel>> GetIssuesReportsAsync()
    {
        var result = new ListResult<ClientIssueReportModel>();

        try
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand("SELECT * FROM IssueReport ", conn);
            cmd.CommandType = CommandType.Text;

            await conn.OpenAsync();
            var reader = await cmd.ExecuteReaderAsync();

            var images = new List<ClientIssueReportModel>();

            while (await reader.ReadAsync())
                images.Add(new ClientIssueReportModel
                {
                    Id = (Guid)reader["Id"],
                    CreatedOn = (DateTime)reader["created_on"],
                    LastUpdated = SqlUtils.GetValueOrNull<DateTime>(reader, "last_update_on"),
                    Machine = (string)reader["machine"],
                    MachineUser = (string)reader["machine_user"],
                    SyncedOn = SqlUtils.GetValueOrNull<DateTime>(reader, "synced_on"),
                    Description = reader["description"] == DBNull.Value ? null : (string)reader["description"],
                    Frequency = SqlUtils.GetValueOrNull2<string>(reader, "frequency"),
                    IssueType = SqlUtils.GetValueOrNull2<string>(reader, "issue_type"),
                    OtherData = reader["other_data"] == DBNull.Value ? null : (string)reader["other_data"],
                    Person = reader["person_identity"] == DBNull.Value ? null : (string)reader["person_identity"],
                    Steps = reader["steps_to_produce"] == DBNull.Value ? null : (string)reader["steps_to_produce"],
                    Title = reader["title"] == DBNull.Value ? null : (string)reader["title"],
                    ErrorMsg = reader["error_msg"] == DBNull.Value ? null : (string)reader["error_msg"],
                    ErrorDump = reader["error_dump"] == DBNull.Value ? null : (string)reader["error_dump"]
                });

            result.List = images;
            result.Success = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            result = e.ToListErrorResult<ClientIssueReportModel>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> LogIssueReport(Guid clientId, ClientIssueReportModel model)
    {
        var result = new NonQueryResult();

        const string sql = @"
                    INSERT INTO IssueReport VALUES (
                            @prId, @prCreatedOn, @prSyncedOn, @prTitle, @prIssueType,
                            @prFreq, @personId, @prDescription, @prSteps, @prMachine,
                            @prUser, @prClientId, @prLastUpd, @prOtherData, @prErrorMsg, @prErrorDump )";

        try
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add("prId", DbType.Guid).Value = Guid.NewGuid();
            cmd.Parameters.Add("prCreatedOn", DbType.DateTime).Value = model.CreatedOn;
            cmd.Parameters.Add("prSyncedOn", DbType.DateTime).Value = DBNull.Value;
            cmd.Parameters.Add("prTitle", DbType.String).Value = model.Title;
            cmd.Parameters.Add("prIssueType", DbType.String).Value = model.IssueType;
            cmd.Parameters.Add("prFreq", DbType.String).Value = model.Frequency;
            cmd.Parameters.Add("personId", DbType.String).Value = model.Person;
            cmd.Parameters.Add("prDescription", DbType.String).Value = model.Description;
            cmd.Parameters.Add("prSteps", DbType.String).Value = model.Steps;
            cmd.Parameters.Add("prMachine", DbType.String).Value = model.Machine;
            cmd.Parameters.Add("prClientId", DbType.Guid).Value = clientId;
            cmd.Parameters.Add("prUser", DbType.String).Value = model.MachineUser;
            cmd.Parameters.Add("prLastUpd", DbType.DateTime).Value = DBNull.Value;
            cmd.Parameters.Add("prOtherData", DbType.String).Value = model.OtherData;
            cmd.Parameters.Add("prErrorMsg", DbType.String).Value = model.ErrorMsg ?? string.Empty;
            cmd.Parameters.Add("prErrorDump", DbType.String).Value = model.ErrorDump ?? string.Empty;

            await conn.OpenAsync();
            cmd.ExecuteNonQuery();

            result.Completed = result.Value = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public async Task<NonQueryResult> FlushSyncedLogsAsync(IList<Guid> idList = null)
    {
        var result = new NonQueryResult();

        var sql = idList == null
            ? "DELETE FROM Log "
            : idList.Any()
                ? $"DELETE FROM Log where Id IN (X'{string.Join("',x'", idList.Select(x => ByteArrayToString(x.ToByteArray())))}')"
                : string.Empty;

        if (string.IsNullOrWhiteSpace(sql))
        {
            result.Completed = result.Value = true;
            return result;
        }

        try
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn) { CommandType = CommandType.Text };

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            result.Completed = result.Value = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public async Task<NonQueryResult> FlushSyncedBinaryContentsAsync(IList<Guid> idList = null)
    {
        var result = new NonQueryResult();

        var sql = idList == null
            ? "DELETE FROM ImgContent "
            : idList.Any()
                ? $"DELETE FROM ImgContent where Id IN (X'{string.Join("',x'", idList.Select(x => ByteArrayToString(x.ToByteArray())))}')"
                : string.Empty;

        if (string.IsNullOrWhiteSpace(sql))
        {
            result.Completed = result.Value = true;
            return result;
        }

        try
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn) { CommandType = CommandType.Text };

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            result.Completed = result.Value = true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
        }

        return result;
    }

    public async Task<NonQueryResult> FlushSyncedIssueReportsAsync(IList<Guid> idList = null)
    {
        var result = new NonQueryResult();

        var sql = idList == null
            ? "DELETE FROM IssueReport "
            : idList.Any()
                ? $"DELETE FROM IssueReport WHERE Id IN (X'{string.Join("',x'", idList.Select(x => ByteArrayToString(x.ToByteArray())))}')"
                : string.Empty;

        if (string.IsNullOrWhiteSpace(sql))
        {
            result.Completed = result.Value = true;
            return result;
        }

        try
        {
            using var conn = new SQLiteConnection(Common.SqliteConnString);
            using var cmd = new SQLiteCommand(sql, conn) { CommandType = CommandType.Text };

            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();

            result.Completed = result.Value = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public async Task<NonQueryResult> CleanAllAsync()
    {
        var result = new NonQueryResult();

        try
        {
            using var conn = new SQLiteConnection(GetConnection.ConnectionStringMaster);
            using var cmd = new SQLiteCommand("VACUUM", conn)
            {
                CommandType = CommandType.Text
            };
            await conn.OpenAsync().ContinueWith(delegate { });
            await cmd.ExecuteNonQueryAsync();

            result.Completed = result.Value = true;
        }
        catch (Exception e)
        {
            //Common.EventLogger.WriteEventLog(EventLogEntryType.Error, e.ToString());
            //result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    private static string ByteArrayToString(IReadOnlyCollection<byte> ba)
    {
        var hex = new StringBuilder(ba.Count * 2);
        foreach (var b in ba) hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }

    public void Dispose()
    {
    }
}