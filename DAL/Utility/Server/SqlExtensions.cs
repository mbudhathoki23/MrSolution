using Dapper;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MrDAL.Utility.Server;

public static class SqlExtensions
{
    #region UpdateDataSet

    public static void UpdateDataSet(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
    {
        if (insertCommand == null) throw new ArgumentNullException(nameof(insertCommand));
        if (deleteCommand == null) throw new ArgumentNullException(nameof(deleteCommand));
        if (updateCommand == null) throw new ArgumentNullException(nameof(updateCommand));
        if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

        // Create a SqlDataAdapter, and dispose of it after we are done
        using var dataAdapter = new SqlDataAdapter
        {
            UpdateCommand = updateCommand,
            InsertCommand = insertCommand,
            DeleteCommand = deleteCommand
        };
        // Set the data adapter commands

        // Update the data set changes in the data source
        dataAdapter.Update(dataSet, tableName);

        // Commit all the changes made to the DataSet
        dataSet.AcceptChanges();
    }

    #endregion UpdateDataSet

    #region CreateCommand

    public static SqlCommand CreateCommand(SqlConnection connection, string spName, params string[] sourceColumns)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // Create a SqlCommand
        var cmd = new SqlCommand(spName, connection) { CommandType = CommandType.StoredProcedure };

        // If we receive parameter values, we need to figure out where they go
        if (sourceColumns is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided source columns to these parameters based on parameter order
            for (var index = 0; index < sourceColumns.Length; index++)
                commandParameters[index].SourceColumn = sourceColumns[index];

            // Attach the discovered parameters to the SqlCommand object
            AttachParameters(cmd, commandParameters);
        }

        return cmd;
    }

    #endregion CreateCommand

    // CUSTOMIZED CONTROL
    public static DataSet DataSet(string query)
    {
        var ds = new DataSet();
        var con = new SqlConnection(GetConnection.ConnectionString);
        ;
        try
        {
            using var da = new SqlDataAdapter(query, con)
            {
                SelectCommand =
                {
                    CommandTimeout = 15000
                }
            };
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            //MessageBox.Show(ex.Message, ObjGlobal.Caption);
            //throw;
            return ds;
        }
        finally
        {
            con.Close();
            con.Dispose();
            SqlConnection.ClearPool(con);
        }
    }

    public static async Task<DataTable> DataTableAsync(string cmdString)
    {
        var tableAsync = new DataTable();
        var con = GetConnection.GetConnectionMaster();
        try
        {
            if (con == null) return tableAsync;
            var reader = await con.CreateCommand().ExecuteReaderAsync();
            tableAsync.Load(reader);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
        finally
        {
            con?.Close();
            if (con != null)
            {
                con.Dispose();
                SqlConnection.ClearPool(con);
            }
        }

        return tableAsync;
    }

    public static async Task<DataTable> GetDataTableAsync(DbCommand command, CancellationToken cancellationToken, string tableName = null)
    {
        var source = new TaskCompletionSource<DataTable>();
        var resultTable = new DataTable(tableName ?? command.CommandText);
        DbDataReader dataReader = null;

        if (cancellationToken.IsCancellationRequested)
        {
            source.SetCanceled();
            await source.Task;
        }

        try
        {
            await command.Connection.OpenAsync(cancellationToken);
            dataReader = await command.ExecuteReaderAsync(CommandBehavior.Default, cancellationToken);
            resultTable.Load(dataReader);
            source.SetResult(resultTable);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            source.SetException(ex);
        }
        finally
        {
            dataReader?.Close();
            command.Connection.Close();
        }

        return resultTable;
    }

    //UTILITY METHODS & CONSTRUCTORS

    #region utility methods & constructors

    private static void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        if (commandParameters == null) return;
        foreach (var p in commandParameters.Where(p => p != null))
        {
            // Check for derived output value with no value assigned
            if ((p.Direction == ParameterDirection.InputOutput ||
                 p.Direction == ParameterDirection.Input) &&
                p.Value == null)
                p.Value = DBNull.Value;
            command.Parameters.Add(p);
        }
    }

    private static void AssignParameterValues(IEnumerable<SqlParameter> commandParameters, DataRow dataRow)
    {
        if (commandParameters == null || dataRow == null)
            // Do nothing if we get no data
            return;

        var i = 0;
        // Set the parameters values
        foreach (var commandParameter in commandParameters)
        {
            // Check the parameter name
            if (commandParameter.ParameterName is not { Length: > 1 })
                throw new Exception(
                    $"PLEASE PROVIDE A VALID PARAMETER NAME ON THE PARAMETER #{i}, THE PARAMETER NAME PROPERTY HAS THE FOLLOWING VALUE: '{commandParameter.ParameterName}'.");
            if (dataRow.Table.Columns.IndexOf(commandParameter.ParameterName.Substring(1)) != -1)
                commandParameter.Value = dataRow[commandParameter.ParameterName.Substring(1)];
            i++;
        }
    }

    private static void AssignParameterValues(IReadOnlyList<SqlParameter> commandParameters, object[] parameterValues)
    {
        if (parameterValues == null) throw new ArgumentNullException(nameof(parameterValues));
        if (commandParameters == null) return;
        if (commandParameters.Count != parameterValues.Length)
            throw new ArgumentException("PARAMETER COUNT DOES NOT MATCH PARAMETER VALUE COUNT.");

        // Iterate through the SqlParameters, assigning the values from the corresponding position in the
        // value array
        for (int i = 0, j = commandParameters.Count; i < j; i++)
            commandParameters[i].Value = parameterValues[i] switch
            {
                // If the current array value derives from IDbDataParameter, then assign its Value property
                IDbDataParameter value => value.Value ?? DBNull.Value,
                null => DBNull.Value,
                _ => parameterValues[i]
            };

        // We must have the same number of values as we pave parameters to put them in
    }

    private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction,
        CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters,
        out bool mustCloseConnection)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            connection.Open();
        }
        else
        {
            mustCloseConnection = false;
        }

        // Associate the connection with the command
        command.Connection = connection;
        // Set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        // If we were provided a transaction, assign it
        if (transaction != null)
            command.Transaction = transaction.Connection switch
            {
                null => throw new ArgumentException(
                    @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                    ObjGlobal.Caption),
                _ => transaction
            };
        // Set the command type
        command.CommandType = commandType;

        // Attach the command parameters if they are provided
        if (commandParameters != null) AttachParameters(command, commandParameters);
    }

    private static async Task<bool> PrepareCommandAsync(SqlCommand command, SqlConnection connection,
        SqlTransaction transaction, CommandType commandType, string commandText,
        IEnumerable<SqlParameter> commandParameters)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException(nameof(commandText));
        var mustCloseConnection = false;
        // If the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
        {
            mustCloseConnection = true;
            await connection.OpenAsync();
        }

        // Associate the connection with the command
        command.Connection = connection;

        // Set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        // If we were provided a transaction, assign it
        if (transaction != null)
        {
            if (transaction.Connection == null)
                throw new ArgumentException(
                    @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                    ObjGlobal.Caption);
            command.Transaction = transaction;
        }

        // Set the command type
        command.CommandType = commandType;

        // Attach the command parameters if they are provided
        if (commandParameters != null) AttachParameters(command, commandParameters);
        return mustCloseConnection;
    }

    #endregion utility methods & constructors

    //  EXECUTE NON QUERY

    #region ExecuteNonQuery

    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(connectionString, commandType, commandText, null);
    }
    public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(GetConnection.ConnectionString, CommandType.Text, commandText, commandParameters);
    }

    public static int ExecuteNonQuery(string commandText)
    {
        try
        {
            return ExecuteNonQuery(GetConnection.ConnectionString, CommandType.Text, commandText);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public static int ExecuteNonQueryIgnoreException(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryIgnoreException(GetConnection.ConnectionString, CommandType.Text, commandText, null);
    }

    public static int ExecuteNonQueryIgnoreExceptionMaster(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryIgnoreException(GetConnection.ConnectionStringMaster, CommandType.Text, commandText, null);
    }

    public static int ExecuteNonQueryIgnoreException(string commandText, params SqlParameter[] commandParameters)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryIgnoreException(GetConnection.ConnectionString, CommandType.Text, commandText,
            commandParameters);
    }

    public static int ExecuteNonQueryOnMaster(string commandText)
    {
        var connectionString = GetConnection.ConnectionStringMaster;
        const CommandType commandType = CommandType.Text;
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(commandText));
        try
        {
            // Create & open a SqlConnection, and dispose of it after we are done
            var connection = GetConnection.GetConnectionMaster();

            // Call the overload that takes a connection in place of the connection string
            return ExecuteNonQuery(connection, commandType, commandText);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            MessageBox.Show(e.Message);
            return 0;
        }
    }
    public static int ExecNonQuery(string commandText)
    {
        var conn = new SqlConnection(GetConnection.ConnectionString);
        SqlDataReader rdr = null;
        var cmd = new SqlCommand(commandText, conn);
        try
        {
            conn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            rdr = cmd.ExecuteReader();
        }
        finally
        {
            conn?.Close();
            rdr?.Close();
        }

        return rdr.GetHashCode();
    }
    public static int ExecuteNonQueryOnMaster(string commandText, params SqlParameter[] commandParameters)
    {
        var connectionString = GetConnection.ConnectionStringMaster;
        const CommandType commandType = CommandType.Text;
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(commandText));
        try
        {
            // Create & open a SqlConnection, and dispose of it after we are done
            var connection = GetConnection.GetConnectionMaster();
            // Call the overload that takes a connection in place of the connection string
            return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            MessageBox.Show(e.Message);
            return 0;
        }
    }

    public static int ExecuteNonQuery(string commandText, CommandType commandParameter, params SqlParameter[] commandParameters)
    {
        var connectionString = GetConnection.ConnectionString;
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(commandText));
        try
        {
            // Create & open a SqlConnection, and dispose of it after we are done
            using var connection = new SqlConnection(GetConnection.ConnectionString);
            // Call the overload that takes a connection in place of the connection string
            return ExecuteNonQuery(connection, commandParameter, commandText, commandParameters);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            MessageBox.Show(e.Message);
            return 0;
        }
    }

    public static int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        try
        {
            // Create & open a SqlConnection, and dispose of it after we are done
            using var connection = ReturnConnectionString();
            // Call the overload that takes a connection in place of the connection string
            return ExecuteNonQuery(connection, commandType, commandText, commandParameters);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            e.DialogResult();
            return 0;
        }
    }

    public static int ExecuteNonQueryIgnoreException(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        try
        {
            var connection = ReturnConnectionString();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return ExecuteNonQueryNonException(connection, commandType, commandText, commandParameters);
        }
        catch (Exception e)
        {
            var unused = e.Message;
            return 0;
        }
    }

    public static int ExecuteNonTrans(StringBuilder commandText)
    {
        return ExecuteNonTrans(commandText.ToString());
    }
    public static int ExecuteNonTrans(string commandText)
    {
        using var conn = ReturnConnectionString();
        if (conn.State != ConnectionState.Open) conn.Open();
        using var trans = conn.BeginTransaction();
        try
        {
            using var cmd = conn.CreateCommand();
            cmd.Transaction = trans;
            cmd.CommandText = commandText;
            var nonTrans = cmd.ExecuteNonQuery();
            trans.Commit();
            conn.Close();
            return nonTrans;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            trans.Rollback();
            MessageBox.Show(ex.Message);
            return 0;
        }
    }

    public static int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(connection, commandType, commandText, null);
    }

    public static int ExecuteNonQueryNonException(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        // Create a command and prepare it for execution
        try
        {
            using var cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out var mustCloseConnection);
            cmd.CommandTimeout = 0;
            var rNonQuery = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (!mustCloseConnection)
            {
                return rNonQuery;
            }
            connection.Close();
            return rNonQuery;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            connection.Close();
        }

        return 0;
    }

    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null)
        {
            throw new ArgumentNullException(nameof(connection));
        }
        // Create a command and prepare it for execution
        try
        {
            using var cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters, out var mustCloseConnection);
            cmd.CommandTimeout = 0;
            var rNonQuery = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            if (!mustCloseConnection) return rNonQuery;
            connection.Close();
            return rNonQuery;
        }
        catch (Exception e)
        {
            e.DialogResult();
        }

        return 0;
    }

    public static int ExecuteNonQuery(SqlConnection connection, string spName, params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
    }

    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(transaction, commandType, commandText, null);
    }

    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
        {
            const string msg = "The transaction was roll backed or committed, please provide an open transaction.";
            throw new ArgumentException(msg, nameof(transaction));
        }

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters, out _);

        // Finally, execute the command
        var executeNonQuery = cmd.ExecuteNonQuery();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return executeNonQuery;
    }

    public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                ObjGlobal.Caption);
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));
        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);

        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters =
            SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, parameterValues.GetBool());

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    #endregion ExecuteNonQuery

    // EXECUTE NON QUERY ASYNC

    #region ExecuteNonQueryAsync
    public static async Task<int> ExecuteNonTransAsync(string connectionString, string commandText)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            using var sqlTransaction = connection.BeginTransaction();
            var iResult = connection.ExecuteAsync(commandText, sqlTransaction);
            if (await iResult > 0) sqlTransaction.Commit();
            connection.Close();
            return await iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }
    public static async Task<int> ExecuteNonTransAsync(string commandText)
    {
        try
        {
            using var connection = new SqlConnection(GetConnection.ConnectionString);
            await connection.OpenAsync();
            using var sqlTransaction = connection.BeginTransaction();
            var iResult = connection.ExecuteAsync(commandText, sqlTransaction);
            if (await iResult > 0) sqlTransaction.Commit();
            connection.Close();
            return await iResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    private static Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryAsync(connectionString, commandType, commandText, null);
    }

    public static Task<int> ExecuteNonQueryMasterAsync(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryAsync(GetConnection.ConnectionStringMaster, CommandType.Text, commandText, null);
    }

    public static Task<int> ExecuteNonQueryAsync(string commandText)
    {
        return ExecuteNonQueryAsync(GetConnection.ConnectionString, CommandType.Text, commandText, null);
    }

    public static async Task<int> ExecuteNonQueryNonExceptionAsync(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        try
        {
            return await ExecuteNonQueryAsync(GetConnection.ConnectionString, CommandType.Text, commandText, null);
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private static async Task<int> ExecuteNonQueryAsync(string connectionString, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        try
        {
            // Create & open a SqlConnection, and dispose of it after we are done
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Call the overload that takes a connection in place of the connection string
            return await ExecuteNonQueryAsync(connection, commandType, commandText, commandParameters);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            return 0;
        }
    }

    public static Task<int> ExecuteNonQueryAsync(string spName, params object[] parameterValues)
    {
        return ExecuteNonQueryAsync(GetConnection.ConnectionString, spName, parameterValues);
    }

    public static Task<int> ExecuteNonQueryAsync(string connectionString, string spName, params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteNonQueryAsync(connectionString, CommandType.StoredProcedure, spName);
        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteNonQueryAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    private static Task<int> ExecuteNonQueryAsync(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryAsync(connection, commandType, commandText, null);
    }

    private static async Task<int> ExecuteNonQueryAsync(SqlConnection connection, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        var mustCloseConnection =
            await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters);

        // Finally, execute the command
        var retrieval = await cmd.ExecuteNonQueryAsync();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        if (mustCloseConnection)
            connection.Close();
        return retrieval;
    }

    public static Task<int> ExecuteNonQueryAsync(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, spName);
        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    private static Task<int> ExecuteNonQueryAsync(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteNonQueryAsync(transaction, commandType, commandText, null);
    }

    private static async Task<int> ExecuteNonQueryAsync(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                ObjGlobal.Caption);

        try
        {
            // Create a command and prepare it for execution
            using var cmd = new SqlCommand();
            await PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText,
                commandParameters);

            // Finally, execute the command
            var queryAsync = await cmd.ExecuteNonQueryAsync();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();
            return queryAsync;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            return 0;
        }
    }

    public static Task<int> ExecuteNonQueryAsync(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteNonQueryAsync(transaction, CommandType.StoredProcedure, spName);

        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters =
            SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, parameterValues.GetBool());

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteNonQueryAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    #endregion ExecuteNonQueryAsync

    //EXECUTE DATA SET

    #region ExecuteDataSet

    private static DataSet ExecuteDataSet(string connectionString, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSet(commandType, commandText, null);
    }

    public static DataSet ExecuteDataSet(StringBuilder commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSet(commandText.ToString());
    }

    public static DataSet ExecuteDataSet(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSet(CommandType.Text, commandText, null);
    }

    public static DataSet ExecuteDataSetOnMaster(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSetOnMaster(CommandType.Text, commandText, null);
    }

    private static DataSet ExecuteDataSet(CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // Create & open a SqlConnection, and dispose of it after we are done
        var connection = ReturnConnectionString();
        // Call the overload that takes a connection in place of the connection string
        return ExecuteDataSet(connection, commandType, commandText, commandParameters);
    }

    private static DataSet ExecuteDataSetOnMaster(CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        var connection = ReturnMasterConnectionString();
        // Call the overload that takes a connection in place of the connection string
        return ExecuteDataSet(connection, commandType, commandText, commandParameters);
    }

    public static DataSet ExecuteDataSet(string connectionString, string spName, params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName);
        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteDataSet(CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    private static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSet(connection, commandType, commandText, null);
    }

    private static DataSet ExecuteDataSet(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        try
        {
            // Create a command and prepare it for execution
            var sqlCommand = new SqlCommand();
            PrepareCommand(sqlCommand, connection, null, commandType, commandText, commandParameters,
                out var mustCloseConnection);

            // Create the DataAdapter & DataSet
            var adapter = new SqlDataAdapter(sqlCommand);
            var dataSet = new DataSet();

            // Fill the DataSet using default values for DataTable names, etc
            adapter.Fill(dataSet);

            // Detach the SqlParameters from the command object, so they can be used again
            sqlCommand.Parameters.Clear();

            if (mustCloseConnection) connection.Close();
            // Return the data set
            return dataSet;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            return new DataSet();
        }
    }

    public static DataSet ExecuteDataSet(SqlConnection connection, string spName, params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteDataSet(connection, CommandType.StoredProcedure, spName);

        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteDataSet(connection, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    private static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSet(transaction, commandType, commandText, null);
    }

    private static DataSet ExecuteDataSet(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters,
            out _);

        // Create the DataAdapter & DataSet
        using var da = new SqlDataAdapter(cmd);
        var ds = new DataSet();

        // Fill the DataSet using default values for DataTable names, etc
        da.Fill(ds);

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        // Return the dataset
        return ds;
    }

    public static DataSet ExecuteDataSet(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());
            SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteDataSet

    //EXECUTE DATA SET ASYNC

    #region ExecuteDataSetAsync

    public static Task<DataSet> ExecuteDataSetAsync(string connectionString, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSetAsync(connectionString, commandType, commandText, null);
    }

    public static Task<DataSet> ExecuteDataSetAsync(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSetAsync(GetConnection.ConnectionString, CommandType.Text, commandText, null);
    }

    public static async Task<DataSet> ExecuteDataSetOnMasterAsync(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return await ExecuteDataSetAsync(GetConnection.ConnectionStringMaster, CommandType.Text, commandText, null);
    }

    private static Task<DataSet> ExecuteDataSetAsync(string connectionString, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

        // Create & open a SqlConnection, and dispose of it after we are done
        var connection = new SqlConnection(connectionString);
        connection.OpenAsync();

        // Call the overload that takes a connection in place of the connection string
        return ExecuteDataSetAsync(connection, commandType, commandText, commandParameters);
    }

    public static Task<DataSet> ExecuteDataSetAsync(string connectionString, string spName,
        params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteDataSetAsync(connectionString, CommandType.StoredProcedure, spName);
        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteDataSetAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    public static Task<DataSet> ExecuteDataSetAsync(SqlConnection connection, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSetAsync(connection, commandType, commandText, null);
    }

    public static async Task<DataSet> ExecuteDataSetAsync(SqlConnection connection, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        var mustCloseConnection =
            PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters);

        // Create the DataAdapter & DataSet
        using var da = new SqlDataAdapter(cmd);
        var ds = new DataSet();

        // Fill the DataSet using default values for DataTable names, etc
        da.Fill(ds);

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        if (await mustCloseConnection) connection.Close();

        // Return the dataset
        return ds;
    }

    public static Task<DataSet> ExecuteDataSetAsync(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is not { Length: > 0 })
            return ExecuteDataSetAsync(connection, CommandType.StoredProcedure, spName);
        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

        // Assign the provided values to these parameters based on parameter order
        AssignParameterValues(commandParameters, parameterValues);

        // Call the overload that takes an array of SqlParameters
        return ExecuteDataSetAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        // Otherwise we can just call the SP without params
    }

    public static Task<DataSet> ExecuteDataSetAsync(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteDataSetAsync(transaction, commandType, commandText, null);
    }

    public static Task<DataSet> ExecuteDataSetAsync(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        _ = PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText,
            commandParameters);

        // Create the DataAdapter & DataSet
        using var da = new SqlDataAdapter(cmd);
        var ds = new DataSet();

        // Fill the DataSet using default values for DataTable names, etc
        da.Fill(ds);

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        // Return the dataset
        return Task.FromResult(ds);
    }

    public static Task<DataSet> ExecuteDataSetAsync(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteDataSetAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteDataSetAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteDataSetAsync

    //EXECUTE READER

    #region ExecuteReader

    private enum SqlConnectionOwnership
    {
        /// <summary>Connection is owned and managed by SqlHelper</summary>
        Internal,

        /// <summary>Connection is owned and managed by the caller</summary>
        External
    }

    private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction,
        CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters,
        SqlConnectionOwnership connectionOwnership)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        var mustCloseConnection = false;
        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters,
                out mustCloseConnection);

            // Create a reader

            // Call ExecuteReader with the appropriate CommandBehavior
            var dataReader = connectionOwnership == SqlConnectionOwnership.External
                ? cmd.ExecuteReader()
                : cmd.ExecuteReader(CommandBehavior.CloseConnection);

            // Detach the SqlParameters from the command object, so they can be used again.
            // HACK: There is a problem here, the output parameter values are fletched
            // when the reader is closed, so if the parameters are detached from the command
            // then the SqlReader can´t set its values.
            // When this happen, the parameters can´t be used again in other command.
            var canClear = true;
            foreach (SqlParameter commandParameter in cmd.Parameters)
                if (commandParameter.Direction != ParameterDirection.Input)
                    canClear = false;

            if (canClear) cmd.Parameters.Clear();

            return dataReader;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteReader(connectionString, commandType, commandText, null);
    }

    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        SqlConnection connection = null;
        try
        {
            connection = new SqlConnection(connectionString);
            connection.Open();

            // Call the private overload that takes an internally owned connection in place of the connection string
            return ExecuteReader(connection, null, commandType, commandText, commandParameters,
                SqlConnectionOwnership.Internal);
        }
        catch
        {
            // If we fail to return the SqlDatReader, we need to close the connection ourselves
            if (connection != null) connection.Close();
            throw;
        }
    }

    public static SqlDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
    }

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteReader(connection, commandType, commandText, null);
    }

    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // Pass through the call to the private overload using a null transaction value and an externally owned connection
        return ExecuteReader(connection, null, commandType, commandText, commandParameters,
            SqlConnectionOwnership.External);
    }

    public static SqlDataReader ExecuteReader(SqlConnection connection, string spName, params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteReader(connection, CommandType.StoredProcedure, spName);
    }

    public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteReader(transaction, commandType, commandText, null);
    }

    public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Pass through to private overload, indicating that the connection is owned by the caller
        return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters,
            SqlConnectionOwnership.External);
    }

    public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteReader

    //EXECUTE READER ASYNC

    #region ExecuteReaderAsync

    private static async Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, SqlTransaction transaction,
        CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters,
        SqlConnectionOwnership connectionOwnership)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        var mustCloseConnection = false;
        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        try
        {
            mustCloseConnection = await PrepareCommandAsync(cmd, connection, transaction, commandType, commandText,
                commandParameters);

            // Create a reader
            SqlDataReader dataReader;

            // Call ExecuteReaderAsync with the appropriate CommandBehavior
            if (connectionOwnership == SqlConnectionOwnership.External)
                dataReader = await cmd.ExecuteReaderAsync();
            else
                dataReader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

            // Detach the SqlParameters from the command object, so they can be used again.
            // HACK: There is a problem here, the output parameter values are fletched
            // when the reader is closed, so if the parameters are detached from the command
            // then the SqlReader can´t set its values.
            // When this happen, the parameters can´t be used again in other command.
            var canClear = true;
            foreach (SqlParameter commandParameter in cmd.Parameters)
                if (commandParameter.Direction != ParameterDirection.Input)
                    canClear = false;

            if (canClear) cmd.Parameters.Clear();

            return dataReader;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteReaderAsync(connectionString, commandType, commandText, null);
    }

    public static async Task<SqlDataReader> ExecuteReaderAsync(string connectionString, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        SqlConnection connection = null;
        try
        {
            connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            // Call the private overload that takes an internally owned connection in place of the connection string
            return await ExecuteReaderAsync(connection, null, commandType, commandText, commandParameters,
                SqlConnectionOwnership.Internal);
        }
        catch
        {
            // If we fail to return the SqlDatReader, we need to close the connection ourselves
            if (connection != null) connection.Close();
            throw;
        }
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(string connectionString, string spName,
        params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReaderAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteReaderAsync(connectionString, CommandType.StoredProcedure, spName);
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteReaderAsync(connection, commandType, commandText, null);
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        // Pass through the call to the private overload using a null transaction value and an externally owned connection
        return ExecuteReaderAsync(connection, null, commandType, commandText, commandParameters,
            SqlConnectionOwnership.External);
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReaderAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteReaderAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteReaderAsync(transaction, commandType, commandText, null);
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Pass through to private overload, indicating that the connection is owned by the caller
        return ExecuteReaderAsync(transaction.Connection, transaction, commandType, commandText, commandParameters,
            SqlConnectionOwnership.External);
    }

    public static Task<SqlDataReader> ExecuteReaderAsync(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReaderAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteReaderAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteReaderAsync

    //EXECUTE SCALAR

    #region ExecuteScalar

    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(connectionString, commandType, commandText, null);
    }

    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        // Call the overload that takes a connection in place of the connection string
        return ExecuteScalar(connection, commandType, commandText, commandParameters);
    }

    public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(connection, commandType, commandText, null);
    }

    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();

        PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters,
            out var mustCloseConnection);

        // Execute the command & return the results
        var retval = cmd.ExecuteScalar();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        if (mustCloseConnection)
            connection.Close();

        return retval;
    }

    public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
    }

    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(transaction, commandType, commandText, null);
    }

    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        bool mustCloseConnection;
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters,
            out mustCloseConnection);

        // Execute the command & return the results
        var retval = cmd.ExecuteScalar();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return retval;
    }

    public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteScalar

    //EXECUTE SCALAR ASYNC

    #region ExecuteScalarAsync
    public static Task<object> CloudExecuteScalarAsync(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalarAsync(GetConnection.CloudConnectionString, CommandType.Text, commandText, null);
    }
    public static Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalarAsync(connectionString, commandType, commandText, null);
    }

    public static async Task<object> ExecuteScalarAsync(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString));
        }
        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        // Call the overload that takes a connection in place of the connection string
        return await ExecuteScalarAsync(connection, commandType, commandText, commandParameters);
    }

    public static Task<object> ExecuteScalarAsync(string connectionString, string spName,
        params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteScalarAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteScalarAsync(connectionString, CommandType.StoredProcedure, spName);
    }

    public static Task<object> ExecuteScalarAsync(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalarAsync(connection, commandType, commandText, null);
    }

    public static async Task<object> ExecuteScalarAsync(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();

        var mustCloseConnection = await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters);

        // Execute the command & return the results
        var retrieval = await cmd.ExecuteScalarAsync();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        if (mustCloseConnection)
            connection.Close();

        return retrieval;
    }

    public static Task<object> ExecuteScalarAsync(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteScalarAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteScalarAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<object> ExecuteScalarAsync(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteScalarAsync(transaction, commandType, commandText, null);
    }

    public static async Task<object> ExecuteScalarAsync(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        await PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText,
            commandParameters);

        // Execute the command & return the results
        var retval = await cmd.ExecuteScalarAsync();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return retval;
    }

    public static Task<object> ExecuteScalarAsync(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // PPull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteScalarAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteScalarAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteScalarAsync

    //EXECUTE XML READER

    #region ExecuteXmlReader

    public static XmlReader ExecuteXmlReader(string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        var conn = new SqlConnection(GetConnection.ConnectionString);
        ;
        return ExecuteXmlReader(conn, CommandType.Text, commandText);
    }

    public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteXmlReader(connection, commandType, commandText, null);
    }

    public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        var mustCloseConnection = false;
        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        try
        {
            PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters,
                out mustCloseConnection);

            // Create the DataAdapter & DataSet
            var retval = cmd.ExecuteXmlReader();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            return retval;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName, params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
    }

    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteXmlReader(transaction, commandType, commandText, null);
    }

    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        bool mustCloseConnection;
        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters,
            out mustCloseConnection);

        // Create the DataAdapter & DataSet
        var retval = cmd.ExecuteXmlReader();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return retval;
    }

    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteXmlReader

    // EXECUTE XML READER ASYNC

    #region ExecuteXmlReaderAsync

    public static Task<XmlReader> ExecuteXmlReaderAsync(SqlConnection connection, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteXmlReaderAsync(connection, commandType, commandText, null);
    }

    public static async Task<XmlReader> ExecuteXmlReaderAsync(SqlConnection connection, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));

        var mustCloseConnection = false;
        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        try
        {
            mustCloseConnection =
                await PrepareCommandAsync(cmd, connection, null, commandType, commandText, commandParameters);

            // Create the DataAdapter & DataSet
            var retval = await cmd.ExecuteXmlReaderAsync();

            // Detach the SqlParameters from the command object, so they can be used again
            cmd.Parameters.Clear();

            return retval;
        }
        catch
        {
            if (mustCloseConnection)
                connection.Close();
            throw;
        }
    }

    public static Task<XmlReader> ExecuteXmlReaderAsync(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteXmlReaderAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteXmlReaderAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<XmlReader> ExecuteXmlReaderAsync(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // Pass through the call providing null for the set of SqlParameters
        return ExecuteXmlReaderAsync(transaction, commandType, commandText, null);
    }

    public static async Task<XmlReader> ExecuteXmlReaderAsync(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        // Create a command and prepare it for execution
        var cmd = new SqlCommand();
        await PrepareCommandAsync(cmd, transaction.Connection, transaction, commandType, commandText,
            commandParameters);

        // Create the DataAdapter & DataSet
        var retval = await cmd.ExecuteXmlReaderAsync();

        // Detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();
        return retval;
    }

    public static Task<XmlReader> ExecuteXmlReaderAsync(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return ExecuteXmlReaderAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        // Otherwise we can just call the SP without params
        return ExecuteXmlReaderAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteXmlReaderAsync

    //FILL DATA SET

    #region FillDataSet

    public static void FillDataSet(string connectionString, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        // Call the overload that takes a connection in place of the connection string
        FillDataSet(connection, commandType, commandText, dataSet, tableNames);
    }

    public static void FillDataSet(string connectionString, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        // Call the overload that takes a connection in place of the connection string
        FillDataSet(connection, commandType, commandText, dataSet, tableNames, commandParameters);
    }

    public static void FillDataSet(string connectionString, string spName, DataSet dataSet, string[] tableNames,
        params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        connection.Open();

        // Call the overload that takes a connection in place of the connection string
        FillDataSet(connection, spName, dataSet, tableNames, parameterValues);
    }

    public static void FillDataSet(SqlConnection connection, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames)
    {
        FillDataSet(connection, commandType, commandText, dataSet, tableNames, null);
    }

    public static void FillDataSet(SqlConnection connection, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        FillDataSet(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
    }

    public static void FillDataSet(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            FillDataSet(connection, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
        }
        else
        {
            // Otherwise we can just call the SP without params
            FillDataSet(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
        }
    }

    public static void FillDataSet(SqlTransaction transaction, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames)
    {
        FillDataSet(transaction, commandType, commandText, dataSet, tableNames, null);
    }

    public static void FillDataSet(SqlTransaction transaction, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        FillDataSet(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames,
            commandParameters);
    }

    public static void FillDataSet(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            FillDataSet(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames, commandParameters);
        }
        else
        {
            // Otherwise we can just call the SP without params
            FillDataSet(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
        }
    }

    private static void FillDataSet(SqlConnection connection, SqlTransaction transaction, CommandType commandType,
        string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

        // Create a command and prepare it for execution
        var command = new SqlCommand();
        PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters,
            out var mustCloseConnection);

        // Create the DataAdapter & DataSet
        using (var dataAdapter = new SqlDataAdapter(command))
        {
            // Add the table mappings specified by the user
            if (tableNames is { Length: > 0 })
            {
                var tableName = "Table";
                for (var index = 0; index < tableNames.Length; index++)
                {
                    if (tableNames[index] == null || tableNames[index].Length == 0)
                        throw new ArgumentException(
                            "The tableNames parameter must contain a list of tables, a value was provided as null or empty string.",
                            nameof(tableNames));
                    dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                    tableName += (index + 1).ToString(CultureInfo.InvariantCulture);
                }
            }

            // Fill the DataSet using default values for DataTable names, etc
            dataAdapter.Fill(dataSet);

            // Detach the SqlParameters from the command object, so they can be used again
            command.Parameters.Clear();
        }

        if (mustCloseConnection)
            connection.Close();
    }

    #endregion FillDataSet

    #region FillDataSetAsync

    public static async Task FillDataSetAsync(string connectionString, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        // Call the overload that takes a connection in place of the connection string
        await FillDataSetAsync(connection, commandType, commandText, dataSet, tableNames);
    }

    public static async Task FillDataSetAsync(string connectionString, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        // Call the overload that takes a connection in place of the connection string
        await FillDataSetAsync(connection, commandType, commandText, dataSet, tableNames, commandParameters);
    }

    public static async Task FillDataSetAsync(string connectionString, string spName, DataSet dataSet,
        string[] tableNames, params object[] parameterValues)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        // Create & open a SqlConnection, and dispose of it after we are done
        using var connection = new SqlConnection(connectionString);
        await connection.OpenAsync();

        // Call the overload that takes a connection in place of the connection string
        await FillDataSetAsync(connection, spName, dataSet, tableNames, parameterValues);
    }

    public static Task FillDataSetAsync(SqlConnection connection, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames)
    {
        return FillDataSetAsync(connection, commandType, commandText, dataSet, tableNames, null);
    }

    public static Task FillDataSetAsync(SqlConnection connection, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        return FillDataSetAsync(connection, null, commandType, commandText, dataSet, tableNames, commandParameters);
    }

    public static Task FillDataSetAsync(SqlConnection connection, string spName, DataSet dataSet, string[] tableNames,
        params object[] parameterValues)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return FillDataSetAsync(connection, CommandType.StoredProcedure, spName, dataSet, tableNames,
                commandParameters);
        }

        // Otherwise we can just call the SP without params
        return FillDataSetAsync(connection, CommandType.StoredProcedure, spName, dataSet, tableNames);
    }

    public static Task FillDataSetAsync(SqlTransaction transaction, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames)
    {
        return FillDataSetAsync(transaction, commandType, commandText, dataSet, tableNames, null);
    }

    public static Task FillDataSetAsync(SqlTransaction transaction, CommandType commandType, string commandText,
        DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
    {
        return FillDataSetAsync(transaction.Connection, transaction, commandType, commandText, dataSet, tableNames,
            commandParameters);
    }

    public static Task FillDataSetAsync(SqlTransaction transaction, string spName, DataSet dataSet, string[] tableNames,
        params object[] parameterValues)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If we receive parameter values, we need to figure out where they go
        if (parameterValues is { Length: > 0 })
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName,
                    parameterValues.GetBool());
            // Assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // Call the overload that takes an array of SqlParameters
            return FillDataSetAsync(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames,
                commandParameters);
        }

        // Otherwise we can just call the SP without params
        return FillDataSetAsync(transaction, CommandType.StoredProcedure, spName, dataSet, tableNames);
    }

    private static async Task FillDataSetAsync(SqlConnection connection, SqlTransaction transaction,
        CommandType commandType, string commandText, DataSet dataSet, string[] tableNames,
        params SqlParameter[] commandParameters)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (dataSet == null) throw new ArgumentNullException(nameof(dataSet));

        // Create a command and prepare it for execution
        var command = new SqlCommand();

        var mustCloseConnection = await PrepareCommandAsync(command, connection, transaction, commandType,
            commandText, commandParameters);

        // Create the DataAdapter & DataSet
        using (var dataAdapter = new SqlDataAdapter(command))
        {
            // Add the table mappings specified by the user
            if (tableNames is { Length: > 0 })
            {
                var tableName = "Table";
                for (var index = 0; index < tableNames.Length; index++)
                {
                    if (tableNames[index] == null || tableNames[index].Length == 0)
                        throw new ArgumentException(
                            "The tableNames parameter must contain a list of tables, a value was provided as null or empty string.",
                            nameof(tableNames));
                    dataAdapter.TableMappings.Add(tableName, tableNames[index]);
                    tableName += (index + 1).ToString(CultureInfo.InvariantCulture);
                }
            }

            // Fill the DataSet using default values for DataTable names, etc
            dataAdapter.Fill(dataSet);

            // Detach the SqlParameters from the command object, so they can be used again
            command.Parameters.Clear();
        }

        if (mustCloseConnection)
            connection.Close();
    }

    #endregion FillDataSetAsync

    #region ExecuteNonQueryTypedParams

    public static int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
    }

    public static int ExecuteNonQueryTypedParams(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
    }

    public static int ExecuteNonQueryTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        if (transaction is { Connection: null })
            throw new ArgumentException(
                "The transaction was roll backed or committed, please provide an open transaction.",
                nameof(transaction));

        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // Sf the row has values, the store procedure parameters must be initialized
        if (dataRow == null || dataRow.ItemArray.Length <= 0)
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
        // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
        var commandParameters =
            SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

        // Set the parameters values
        AssignParameterValues(commandParameters, dataRow);

        return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
    }

    #endregion ExecuteNonQueryTypedParams

    #region ExecuteNonQueryTypedParamsAsync

    public static Task<int> ExecuteNonQueryTypedParamsAsync(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteNonQueryAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQueryAsync(connectionString, CommandType.StoredProcedure, spName);
    }

    public static Task<int> ExecuteNonQueryTypedParamsAsync(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQueryAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<int> ExecuteNonQueryTypedParamsAsync(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // Sf the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());
            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteNonQueryAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQueryAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteNonQueryTypedParamsAsync

    #region ExecuteDataSetTypedParams

    public static DataSet ExecuteDataSetTypedParams(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        //If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteDataSet(CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataSet(connectionString, CommandType.StoredProcedure, spName);
    }

    public static DataSet ExecuteDataSetTypedParams(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteDataSet(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataSet(connection, CommandType.StoredProcedure, spName);
    }

    public static DataSet ExecuteDataSetTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataSet(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteDataSetTypedParams

    #region ExecuteDataSetTypedParamsAsync

    public static Task<DataSet> ExecuteDataSetTypedParamsAsync(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        //If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteDataSetAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataSetAsync(connectionString, CommandType.StoredProcedure, spName);
    }

    public static Task<DataSet> ExecuteDataSetTypedParamsAsync(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteDataSetAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataSetAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<DataSet> ExecuteDataSetTypedParamsAsync(SqlTransaction transaction, string spName,
        DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteDataSetAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataSetAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteDataSetTypedParamsAsync

    #region ExecuteReaderTypedParams

    public static SqlDataReader ExecuteReaderTypedParams(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
    }

    public static SqlDataReader ExecuteReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReader(connection, CommandType.StoredProcedure, spName);
    }

    public static SqlDataReader ExecuteReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteReaderTypedParams

    #region ExecuteReaderTypedParamsAsync

    public static Task<SqlDataReader> ExecuteReaderTypedParamsAsync(string connectionString, string spName,
        DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteReaderAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReaderAsync(connectionString, CommandType.StoredProcedure, spName);
    }

    public static Task<SqlDataReader> ExecuteReaderTypedParamsAsync(SqlConnection connection, string spName,
        DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteReaderAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReaderAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<SqlDataReader> ExecuteReaderTypedParamsAsync(SqlTransaction transaction, string spName,
        DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteReaderAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReaderAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteReaderTypedParamsAsync

    //EXECUTE SCALAR TYPED PARAMS

    #region ExecuteScalarTypedParams

    public static object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
    }

    public static object ExecuteScalarTypedParams(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
    }

    public static object ExecuteScalarTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteScalarTypedParams

    //EXECUTE SCALAR TYPED PARAMS ASYNC

    #region ExecuteScalarTypedParamsAsync

    public static Task<object> ExecuteScalarTypedParamsAsync(string connectionString, string spName, DataRow dataRow)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteScalarAsync(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalarAsync(connectionString, CommandType.StoredProcedure, spName);
    }

    public static Task<object> ExecuteScalarTypedParamsAsync(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteScalarAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalarAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<object> ExecuteScalarTypedParamsAsync(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteScalarAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalarAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteScalarTypedParamsAsync

    //EXECUTE XML READER TYPED PARAMS

    #region ExecuteXmlReaderTypedParams

    public static XmlReader ExecuteXmlReaderTypedParams(SqlConnection connection, string spName, DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
    }

    public static XmlReader ExecuteXmlReaderTypedParams(SqlTransaction transaction, string spName, DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction is { Connection: null })
            throw new ArgumentException(
                "THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteXmlReaderTypedParams

    //EXECUTE XML READER TYPED PARAMS ASYNC

    #region ExecuteXmlReaderTypedParamsAsync

    public static Task<XmlReader> ExecuteXmlReaderTypedParamsAsync(SqlConnection connection, string spName,
        DataRow dataRow)
    {
        if (connection == null) throw new ArgumentNullException(nameof(connection));
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ToString(), spName);

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteXmlReaderAsync(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteXmlReaderAsync(connection, CommandType.StoredProcedure, spName);
    }

    public static Task<XmlReader> ExecuteXmlReaderTypedParamsAsync(SqlTransaction transaction, string spName,
        DataRow dataRow)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));

        if (transaction is { Connection: null })
            throw new ArgumentException(
                @"THE TRANSACTION WAS ROLL BACKED OR COMMITTED, PLEASE PROVIDE AN OPEN TRANSACTION..!!",
                nameof(transaction));

        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException(nameof(spName));

        // If the row has values, the store procedure parameters must be initialized
        if (dataRow != null && dataRow.ItemArray.Length > 0)
        {
            // Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            var commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.ToString(), spName, dataRow.GetBool());

            // Set the parameters values
            AssignParameterValues(commandParameters, dataRow);

            return ExecuteXmlReaderAsync(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteXmlReaderAsync(transaction, CommandType.StoredProcedure, spName);
    }

    #endregion ExecuteXmlReaderTypedParamsAsync

    #region --------------- Custom Execute Query ---------------

    public static int ExecuteProc(string query)
    {
        var connectionString = GetConnection.ConnectionString;
        var table = new DataTable();
        using var cn = new SqlConnection(connectionString);
        cn.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.StoredProcedure;
        var result = cmd.ExecuteNonQuery();
        cn.Close();
        return result;
    }

    public static DataTable ExecuteDataSetSql(string query)
    {
        var connectionString = GetConnection.ConnectionString;
        var table = new DataTable();
        using var cn = new SqlConnection(connectionString);
        cn.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = query;
        // your SQL statement here.
        using (var adapter = new SqlDataAdapter(cmd))
        {
            adapter.Fill(table);
        } // dispose adapter

        cn.Close();
        return table;
    }
    public static DataTable ExecuteDataSetSqlMaster(string query)
    {
        var connectionString = GetConnection.ConnectionStringMaster;
        var table = new DataTable();
        using var cn = new SqlConnection(connectionString);
        cn.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = query;
        // your SQL statement here.
        using (var adapter = new SqlDataAdapter(cmd))
        {
            adapter.Fill(table);
        } // dispose adapter

        cn.Close();
        return table;
    }
    public static DataTable GetDataTable(string cSql)
    {
        var resultTable = new DataTable();
        try
        {
            using var da = new SqlDataAdapter(cSql, GetConnection.ConnectionString);
            da.Fill(resultTable);
        }
        catch (Exception ex)
        {
            ex.DialogResult();
        }

        return resultTable;
    }

    public static async Task<DataTable> GetDataTableAsync(string cSql)
    {
        var oCnn = new SqlConnection(GetConnection.ConnectionString);
        var command = new SqlCommand(cSql, oCnn, null);
        var source = new TaskCompletionSource<DataTable>();
        var resultTable = new DataTable(command.CommandText);

        try
        {
            // Command Behavior.SingleRow - This is the secret to Execute the data Reader to return only one row
            using var dataReader = await command.ExecuteReaderAsync(CommandBehavior.SingleRow);
            resultTable.Load(dataReader);
            source.SetResult(resultTable);
        }
        catch (Exception ex)
        {
            source.SetException(ex);
        }

        return resultTable;
    }

    private static SqlConnection ReturnConnectionString()
    {
        var sqlConnection = new SqlConnection(GetConnection.ConnectionString);
        //var sqlConnection = new SqlConnection(GetConnection.ConnectionString);
        // Create & open a SqlConnection, and dispose of it after we are done
        try
        {
            if (sqlConnection.State == ConnectionState.Connecting)
            {
                sqlConnection.Dispose();
            }

            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            return sqlConnection;
        }
        catch (Exception ex)
        {
            sqlConnection = new SqlConnection(GetConnection.WinConnection);
            sqlConnection.Open();
            return sqlConnection;
        }
    }

    private static SqlConnection ReturnMasterConnectionString()
    {
        SqlConnection sqlConnection;
        // Create & open a SqlConnection, and dispose of it after we are done
        try
        {
            sqlConnection = new SqlConnection(GetConnection.ConnectionStringMaster);
            sqlConnection.Open();
            return sqlConnection;
        }
        catch (Exception ex)
        {
            sqlConnection = new SqlConnection(GetConnection.WinMasterConnection);
            sqlConnection.Open();
            return sqlConnection;
        }
    }

    #endregion --------------- Custom Execute Query ---------------
}