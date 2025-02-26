using MrDAL.Core.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace MrDAL.Data;

// The SqlHelper class is intended to encapsulate high performance, scalable best practices for
// common uses of SqlClient.
public static class SqlCore
{
    private const string SenderName = "SqlCore";

    /// <summary>
    ///     This method is used to attach array of SqlParameters to a SqlCommand.
    ///     This method is used to attach array of SqlParameters to a SqlCommand.
    ///     This method will assign a value of DbNull to any parameter with a direction of InputOutput and a value of null.
    ///     This behavior will prevent default values from being used, but this will be the less common case than an intended
    ///     pure output parameter (derived as InputOutput)
    ///     where the user provided no input value.
    /// </summary>
    /// <param name="command">The command to which the parameters will be added</param>
    /// <param name="commandParameters">an array of SqlParameters tho be added to command</param>
    private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
    {
        foreach (var p in commandParameters)
        {
            // check for derived output value with no value assigned
            if ((p.Direction == ParameterDirection.Output) & (p.Value == null))
            {
                command.Parameters.Add(p);
                continue;
            }

            p.Value ??= DBNull.Value;

            command.Parameters.AddWithValue(p.ParameterName, p.Value);
        }
    } // AttachParameters

    /// <summary>
    ///     This method assigns an array of values to an array of SqlParameters.
    /// </summary>
    /// <param name="commandParameters">array of SqlParameters to be assigned values</param>
    /// <param name="parameterValues">array of objects holding the values to be assigned</param>
    private static void AssignParameterValues(SqlParameter[] commandParameters, IReadOnlyList<object> parameterValues)
    {
        if (commandParameters == null)
            throw new ArgumentNullException(nameof(commandParameters));

        int i;

        // we must have the same number of values as we pave parameters to put them in
        if (commandParameters.Length != parameterValues.Count)
            throw new ArgumentException("Parameter count does not match Parameter Value count.");

        // value array
        var j = commandParameters.Length - 1;
        for (i = 0; i <= j; i++)
            commandParameters[i].Value = parameterValues[i];
    }

    /// <summary>
    ///     This method opens (if necessary) and assigns a connection, transaction, command type and parameters to the provided
    ///     command.
    /// </summary>
    /// <param name="command">the SqlCommand to be prepared</param>
    /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
    /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">
    ///     an array of SqlParameters to be associated with the command or 'null' if no parameters
    ///     are required
    /// </param>
    private static void PrepareCommand(SqlCommand command, SqlConnection connection,
        SqlTransaction transaction, CommandType commandType, string commandText,
        SqlParameter[] commandParameters)
    {
        // if the provided connection is not open, we will open it
        if (connection.State != ConnectionState.Open)
            connection.Open();

        // associate the connection with the command
        command.Connection = connection;

        // set the command text (stored procedure name or SQL statement)
        command.CommandText = commandText;

        // if we were provided a transaction, assign it.
        if (transaction != null)
            command.Transaction = transaction;

        // set the command type
        command.CommandType = commandType;

        // attach the command parameters if they are provided
        if (commandParameters != null)
            AttachParameters(command, commandParameters);
    }

    /// <summary>
    ///     Execute a SqlCommand (that returns no result set and takes no parameters) against the database specified in the
    ///     connection string.
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(string connectionString, CommandType commandType,
        string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(connectionString, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    }

    /// <summary>
    ///     Execute a SqlCommand (that returns no resultset) against the database specified in the connection string
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters"></param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(string connectionString, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        // create & open a SqlConnection, and dispose of it after we are done.
        var cn = new SqlConnection(connectionString);
        try
        {
            cn.Open();

            // call the overload that takes a connection in place of the connection string
            return ExecuteNonQuery(cn, commandType, commandText, commandParameters);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(SenderName);
        }

        return 0;
    }

    /// <summary>
    ///     Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in
    ///     the connection string using the provided parameter values.  This method will discover the parameters for the
    ///     stored procedure, and assign the values based on parameter order.
    ///     This method provides no access to output parameters or the stored procedure's return value parameter.
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="spName">the name of the stored procedure</param>
    /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(string connectionString, string spName,
        params object[] parameterValues)
    {
        // if we receive parameter values, we need to figure out where they go
        if ((parameterValues != null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)

            var commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
    }

    /// <summary>
    ///     Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection.
    /// </summary>
    /// <param name="connection">a valid SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command </param>
    /// <returns>an int representing the number of rows affected by the command</returns>
    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType,
        string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(connection, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteNonQuery

    /// <summary>
    ///     Execute a SqlCommand (that returns no resultset) against the specified SqlConnection using the provided parameters.
    /// </summary>
    /// <param name="connection">a valid SqlConnection </param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command </param>
    /// <param name="commandParameters">an array of SqlParamters used to execute the command </param>
    /// <returns>an int representing the number of rows affected by the command </returns>
    public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();

        PrepareCommand(cmd, connection, null, commandType, commandText,
            commandParameters);

        // finally, execute the command.
        var returnValue = cmd.ExecuteNonQuery();

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return returnValue;
    } // ExecuteNonQuery

    /// <summary>
    ///     Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection using
    ///     the provided parameter values.
    ///     This method will discover the parameters for the stored procedure, and assign the values based on parameter order.
    ///     This method provides no access to output parameters or the stored procedure's return value parameter.
    /// </summary>
    /// <param name="connection">a valid SqlConnection</param>
    /// <param name="spName">the name of the stored procedure </param>
    /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure </param>
    /// <returns>an int representing the number of rows affected by the command </returns>
    public static int ExecuteNonQuery(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQuery(connection, CommandType.StoredProcedure, spName);
    }

    /// <summary>
    ///     a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlTransaction.
    /// </summary>
    /// <param name="transaction">a valid SqlTransaction associated with the connection </param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.) </param>
    /// <param name="commandText">the stored procedure name or T-SQL command </param>
    /// <returns>
    ///     an int representing the number of rows affected by the command
    /// </returns>
    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteNonQuery(transaction, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteNonQuery

    /// <summary>
    ///     Execute a SqlCommand (that returns no resultset) against the specified SqlTransaction using the provided
    ///     parameters.
    /// </summary>
    /// <param name="transaction">a valid SqlTransaction </param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.) </param>
    /// <param name="commandText">the stored procedure name or T-SQL command </param>
    /// <param name="commandParameters">an array of SqlParameters used to execute the command </param>
    /// <returns>an int representing the number of rows affected by the command </returns>
    public static int ExecuteNonQuery(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        int retval;

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

        // finally, execute the command.
        cmd.CommandTimeout = 0;

        retval = cmd.ExecuteNonQuery();

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return retval;
    } // ExecuteNonQuery

    /// <summary>
    ///     Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlTransaction
    ///     using the provided parameter values.  This method will discover the parameters for the
    ///     stored procedure, and assign the values based on parameter order.
    ///     This method provides no access to output parameters or the stored procedure's return value parameter.
    /// </summary>
    /// <param name="transaction">a valid SqlTransaction </param>
    /// <param name="spName">the name of the stored procedure </param>
    /// <param name="parameterValues"></param>
    /// <returns>an int representing the number of rows affected by the command </returns>
    public static int ExecuteNonQuery(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteNonQuery(transaction, CommandType.StoredProcedure, spName);
    } // ExecuteNonQuery

    /// <summary>
    ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in the
    ///     connection string.
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <returns>Returns: a dataset containing the resultset generated by the command</returns>
    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteDataset(connectionString, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteDataSet

    /// <summary>
    ///     Execute a SqlCommand (that returns a resultset) against the database specified in the connection string using the
    ///     provided parameters.
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
    /// <returns>A dataset containing the resultset generated by the command</returns>
    public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create & open a SqlConnection, and dispose of it after we are done.
        var cn = new SqlConnection(connectionString);
        try
        {
            cn.Open();
            // call the overload that takes a connection in place of the connection string
            return ExecuteDataset(cn, commandType, commandText, commandParameters);
        }
        finally
        {
            cn.Dispose();
        }
    } // ExecuteDataSet

    /// <summary>
    ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in the
    ///     connection string using the provided parameter values.
    ///     This method will discover the parameters for the stored procedure, and assign the values based on parameter order.
    ///     This method provides no access to output parameters or the stored procedure's return value parameter.
    /// </summary>
    /// <param name="connectionString">a valid connection string for a SqlConnection</param>
    /// <param name="spName">the name of the stored procedure</param>
    /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
    /// <returns>a dataset containing the resultset generated by the command</returns>
    public static DataSet ExecuteDataset(string connectionString, string spName,
        params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
    } // ExecuteDataSet

    /// <summary>
    ///     Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
    /// </summary>
    /// <param name="connection">a valid SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <returns>a dataset containing the resultset generated by the command</returns>
    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteDataset(connection, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteDataSet

    /// <summary>
    ///     Execute a SqlCommand (that returns a resultset) against the specified SqlConnection using the provided parameters.
    /// </summary>
    /// <param name="connection">a valid SqlConnection</param>
    /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
    /// <param name="commandText">the stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">an array of SqlParameters used to execute the command</param>
    /// <returns>a dataset containing the resultset generated by the command</returns>
    public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        var ds = new DataSet();
        SqlDataAdapter da;

        PrepareCommand(cmd, connection, null /* TODO Change to default(_) if this is not a reference type */,
            commandType, commandText, commandParameters);
        cmd.CommandTimeout = 0;
        // create the DataAdapter & DataSet
        da = new SqlDataAdapter(cmd);

        // fill the DataSet using default values for DataTable names, etc.
        da.Fill(ds);

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        // return the dataset
        return ds;
    } // ExecuteDataSet

    /// <summary>
    ///     Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection using
    ///     the provided parameter values.
    ///     This method will discover the parameters for the stored procedure, and assign the values based on parameter order.
    ///     This method provides no access to output parameters or the stored procedure's return value parameter.
    /// </summary>
    /// <param name="connection">a valid SqlConnection</param>
    /// <param name="spName">the name of the stored procedure</param>
    /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
    /// <returns>a dataset containing the resultset generated by the command</returns>
    public static DataSet ExecuteDataset(SqlConnection connection, string spName, params object[] parameterValues)
    {
        // Return ExecuteDataSet(connection, spName, parameterValues)
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteDataset(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataset(connection, CommandType.StoredProcedure, spName);
    } // ExecuteDataSet

    // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
    // e.g.:
    // Dim ds As Dataset = ExecuteDataSet(trans, CommandType.StoredProcedure, "GetOrders")
    // Parameters
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: a dataset containing the resultset generated by the command
    public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteDataset(transaction, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteDataSet

    // Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
    // using the provided parameters.
    // e.g.:
    // Dim ds As Dataset = ExecuteDataSet(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    // Parameters
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: a dataset containing the resultset generated by the command
    public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        var ds = new DataSet();
        SqlDataAdapter da;

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

        // create the DataAdapter & DataSet
        da = new SqlDataAdapter(cmd);

        // fill the DataSet using default values for DataTable names, etc.
        da.Fill(ds);

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        // return the dataset
        return ds;
    } // ExecuteDataSet

    // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified
    // SqlTransaction using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim ds As Dataset = ExecuteDataSet(trans, "GetOrders", 24, 36)
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: a dataset containing the resultset generated by the command
    public static DataSet ExecuteDataset(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
    } // ExecuteDataSet

    // Create and prepare a SqlCommand, and call ExecuteReader with the appropriate CommandBehavior.
    // If we created and opened the connection, we want the connection to be closed when the DataReader is closed.
    // If the caller provided the connection, we want to leave it to them to manage.
    // Parameters:
    // -connection - a valid SqlConnection, on which to execute this command
    // -transaction - a valid SqlTransaction, or 'null'
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParameters to be associated with the command or 'null' if no parameters are required
    // -connectionOwnership - indicates whether the connection parameter was provided by the caller, or created by SqlHelper
    // Returns: SqlDataReader containing the results of the command
    private static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction,
        CommandType commandType, string commandText, SqlParameter[] commandParameters,
        SqlConnectionOwnership connectionOwnership)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        // create a reader
        SqlDataReader dr;

        PrepareCommand(cmd, connection, transaction, commandType, commandText, commandParameters);

        // call ExecuteReader with the appropriate CommandBehavior
        if (connectionOwnership == SqlConnectionOwnership.External)
            dr = cmd.ExecuteReader();
        else
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return dr;
    } // ExecuteReader

    // Execute a SqlCommand (that returns a resultset and takes no parameters) against the database specified in
    // the connection string.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders")
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteReader(connectionString, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteReader

    // Execute a SqlCommand (that returns a resultset) against the database specified in the connection string
    // using the provided parameters.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(connString, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create & open a SqlConnection
        var cn = new SqlConnection(connectionString);
        cn.Open();

        try
        {
            // call the private overload that takes an internally owned connection in place of the connection string
            return ExecuteReader(cn, null /* TODO Change to default(_) if this is not a reference type */,
                commandType, commandText, commandParameters, SqlConnectionOwnership.Internal);
        }
        catch
        {
            // if we fail to return the SqlDatReader, we need to close the connection ourselves
            cn.Dispose();
            return null;
        }
    } // ExecuteReader

    // Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in
    // the connection string using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(connString, "GetOrders", 24, 36)
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(string connectionString, string spName,
        params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteReader(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
    } // ExecuteReader

    // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders")
    // Parameters:
    // -connection - a valid SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        return ExecuteReader(connection, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteReader

    // Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
    // using the provided parameters.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    // Parameters:
    // -connection - a valid SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // pass through the call to private overload using a null transaction value
        return ExecuteReader(connection, null /* TODO Change to default(_) if this is not a reference type */,
            commandType, commandText, commandParameters, SqlConnectionOwnership.External);
    } // ExecuteReader

    // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
    // using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(conn, "GetOrders", 24, 36)
    // Parameters:
    // -connection - a valid SqlConnection
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        // pass through the call using a null transaction value
        // Return ExecuteReader(connection, CType(Nothing, SqlTransaction), spName, parameterValues)

        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReader(connection, CommandType.StoredProcedure, spName);
    } // ExecuteReader

    // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders")
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteReader(transaction, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteReader

    // Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
    // using the provided parameters.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        // pass through to private overload, indicating that the connection is owned by the caller
        return ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters,
            SqlConnectionOwnership.External);
    } // ExecuteReader

    // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction
    // using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim dr As SqlDataReader = ExecuteReader(trans, "GetOrders", 24, 36)
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: a SqlDataReader containing the resultset generated by the command
    public static SqlDataReader ExecuteReader(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteReader(transaction, CommandType.StoredProcedure, spName);
    } // ExecuteReader

    // Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the database specified in
    // the connection string.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount"))
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(connectionString, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteScalar

    // Execute a SqlCommand (that returns a 1x1 resultset) against the database specified in the connection string
    // using the provided parameters.
    // e.g.:
    // Dim orderCount As Integer = Cint(ExecuteScalar(connString, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(string connectionString, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create & open a SqlConnection, and dispose of it after we are done.
        var cn = new SqlConnection(connectionString);
        try
        {
            cn.Open();

            // call the overload that takes a connection in place of the connection string
            return ExecuteScalar(cn, commandType, commandText, commandParameters);
        }
        finally
        {
            cn.Dispose();
        }
    } // ExecuteScalar

    // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in
    // the connection string using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(connString, "GetOrderCount", 24, 36))
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(string connectionString, string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalar(connectionString, CommandType.StoredProcedure, spName);
    } // ExecuteScalar

    // Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlConnection.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount"))
    // Parameters:
    // -connection - a valid SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(connection, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteScalar

    // Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
    // using the provided parameters.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(conn, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
    // Parameters:
    // -connection - a valid SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        object retval;

        PrepareCommand(cmd, connection, null /* TODO Change to default(_) if this is not a reference type */,
            commandType, commandText, commandParameters);

        // execute the command & return the results
        retval = cmd.ExecuteScalar();

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return retval;
    } // ExecuteScalar

    // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlConnection
    // using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(conn, "GetOrderCount", 24, 36))
    // Parameters:
    // -connection - a valid SqlConnection
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(SqlConnection connection, string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteScalar(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalar(connection, CommandType.StoredProcedure, spName);
    } // ExecuteScalar

    // Execute a SqlCommand (that returns a 1x1 resultset and takes no parameters) against the provided SqlTransaction.
    // e.g.:
    // Dim orderCount As Integer  = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount"))
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteScalar(transaction, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteScalar

    // Execute a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
    // using the provided parameters.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(trans, CommandType.StoredProcedure, "GetOrderCount", new SqlParameter("@prodid", 24)))
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(SqlTransaction transaction, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        object retval;

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

        // execute the command & return the results
        retval = cmd.ExecuteScalar();

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return retval;
    } // ExecuteScalar

    // Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the specified SqlTransaction
    // using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim orderCount As Integer = CInt(ExecuteScalar(trans, "GetOrderCount", 24, 36))
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: an object containing the value in the 1x1 resultset generated by the command
    public static object ExecuteScalar(SqlTransaction transaction, string spName, params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteScalar(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteScalar(transaction, CommandType.StoredProcedure, spName);
    } // ExecuteScalar

    // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlConnection.
    // e.g.:
    // Dim r As XmlReader = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders")
    // Parameters:
    // -connection - a valid SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command using "FOR XML AUTO"
    // Returns: an XmlReader containing the resultset generated by the command
    public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteXmlReader(connection, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteXmlReader

    // Execute a SqlCommand (that returns a resultset) against the specified SqlConnection
    // using the provided parameters.
    // e.g.:
    // Dim r As XmlReader = ExecuteXmlReader(conn, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    // Parameters:
    // -connection - a valid SqlConnection
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command using "FOR XML AUTO"
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: an XmlReader containing the resultset generated by the command
    public static XmlReader ExecuteXmlReader(SqlConnection connection, CommandType commandType, string commandText,
        params SqlParameter[] commandParameters)
    {
        // pass through the call using a null transaction value
        // Return ExecuteXmlReader(connection, CType(Nothing, SqlTransaction), commandType, commandText, commandParameters)
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        XmlReader retval;

        PrepareCommand(cmd, connection, null /* TODO Change to default(_) if this is not a reference type */,
            commandType, commandText, commandParameters);

        // create the DataAdapter & DataSet
        retval = cmd.ExecuteXmlReader();

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return retval;
    } // ExecuteXmlReader

    // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection
    // using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim r As XmlReader = ExecuteXmlReader(conn, "GetOrders", 24, 36)
    // Parameters:
    // -connection - a valid SqlConnection
    // -spName - the name of the stored procedure using "FOR XML AUTO"
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: an XmlReader containing the resultset generated by the command
    public static XmlReader ExecuteXmlReader(SqlConnection connection, string spName,
        params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters = SqlHelperParameterCache.GetSpParameterSet(connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteXmlReader(connection, CommandType.StoredProcedure, spName);
    } // ExecuteXmlReader

    // Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction
    // e.g.:
    // Dim r As XmlReader = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders")
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command using "FOR XML AUTO"
    // Returns: an XmlReader containing the resultset generated by the command
    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType,
        string commandText)
    {
        // pass through the call providing null for the set of SqlParameters
        return ExecuteXmlReader(transaction, commandType, commandText,
            null /* TODO Change to default(_) if this is not a reference type */);
    } // ExecuteXmlReader

    // Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
    // using the provided parameters.
    // e.g.:
    // Dim r As XmlReader = ExecuteXmlReader(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24))
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -commandType - the CommandType (stored procedure, text, etc.)
    // -commandText - the stored procedure name or T-SQL command using "FOR XML AUTO"
    // -commandParameters - an array of SqlParamters used to execute the command
    // Returns: an XmlReader containing the resultset generated by the command
    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, CommandType commandType,
        string commandText, params SqlParameter[] commandParameters)
    {
        // create a command and prepare it for execution
        var cmd = new SqlCommand();
        XmlReader retval;

        PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

        // create the DataAdapter & DataSet
        retval = cmd.ExecuteXmlReader();

        // detach the SqlParameters from the command object, so they can be used again
        cmd.Parameters.Clear();

        return retval;
    } // ExecuteXmlReader

    // Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlTransaction
    // using the provided parameter values.  This method will discover the parameters for the
    // stored procedure, and assign the values based on parameter order.
    // This method provides no access to output parameters or the stored procedure's return value parameter.
    // e.g.:
    // Dim r As XmlReader = ExecuteXmlReader(trans, "GetOrders", 24, 36)
    // Parameters:
    // -transaction - a valid SqlTransaction
    // -spName - the name of the stored procedure
    // -parameterValues - an array of objects to be assigned as the input values of the stored procedure
    // Returns: a dataset containing the resultset generated by the command
    public static XmlReader ExecuteXmlReader(SqlTransaction transaction, string spName,
        params object[] parameterValues)
    {
        SqlParameter[] commandParameters;

        // if we receive parameter values, we need to figure out where they go
        if (!(parameterValues == null) & (parameterValues.Length > 0))
        {
            // pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
            commandParameters =
                SqlHelperParameterCache.GetSpParameterSet(transaction.Connection.ConnectionString, spName);

            // assign the provided values to these parameters based on parameter order
            AssignParameterValues(commandParameters, parameterValues);

            // call the overload that takes an array of SqlParameters
            return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        return ExecuteXmlReader(transaction, CommandType.StoredProcedure, spName);
    } // ExecuteXmlReader

    // this enum is used to indicate whether the connection was provided by the caller, or created by SqlHelper, so that
    // we can set the appropriate CommandBehavior when calling ExecuteReader()
    private enum SqlConnectionOwnership
    {
        // Connection is owned and managed by SqlHelper
        Internal,

        // Connection is owned and managed by the caller
        External
    } // SqlConnectionOwnership
} // SqlHelper

// SqlHelperParameterCache provides functions to leverage a static cache of procedure parameters, and the
// ability to discover parameters for stored procedures at run-time.
// SqlHelperParameterCache