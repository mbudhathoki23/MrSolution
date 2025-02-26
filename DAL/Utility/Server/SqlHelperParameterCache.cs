using MrDAL.Core.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MrDAL.Utility.Server;

public static class SqlHelperParameterCache
{
    #region private methods, variables, and constructors

    private static readonly Hashtable ParamCache = Hashtable.Synchronized(new Hashtable());

    /// <summary>
    ///     Resolve at run time the appropriate set of SqlParameters for a stored procedure
    /// </summary>
    /// <param name="connection">A valid SqlConnection object</param>
    /// <param name="spName">The name of the stored procedure</param>
    /// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
    /// <returns>The parameter array discovered.</returns>
    private static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName,
        bool includeReturnValueParameter)
    {
        try
        {
            if (connection == null) throw new ArgumentNullException("connection");
            if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException("spName");

            var cmd = new SqlCommand(spName, connection) { CommandType = CommandType.StoredProcedure };

            connection.Open();
            SqlCommandBuilder.DeriveParameters(cmd);
            connection.Close();

            if (!includeReturnValueParameter) cmd.Parameters.RemoveAt(0);

            var discoveredParameters = new SqlParameter[cmd.Parameters.Count];

            cmd.Parameters.CopyTo(discoveredParameters, 0);

            // Init the parameters with a DBNull value
            foreach (var discoveredParameter in discoveredParameters) discoveredParameter.Value = DBNull.Value;
            return discoveredParameters;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult("ExecuteCommand.cs");
            return new SqlParameter[0];
        }
    }

    /// <summary>
    ///     Deep copy of cached SqlParameter array
    /// </summary>
    /// <param name="originalParameters"></param>
    /// <returns></returns>
    private static SqlParameter[] CloneParameters(IList<SqlParameter> originalParameters)
    {
        var clonedParameters = new SqlParameter[originalParameters.Count];

        for (int i = 0, j = originalParameters.Count; i < j; i++)
            clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();

        return clonedParameters;
    }

    #endregion private methods, variables, and constructors

    #region caching functions

    /// <summary>
    ///     Add parameter array to the cache
    /// </summary>
    /// <param name="connectionString">A valid connection string for a SqlConnection</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <param name="commandParameters">An array of SqlParamters to be cached</param>
    public static void CacheParameterSet(string connectionString, string commandText,
        params SqlParameter[] commandParameters)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
        if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

        var hashKey = connectionString + ":" + commandText;

        ParamCache[hashKey] = commandParameters;
    }

    /// <summary>
    ///     Retrieve a parameter array from the cache
    /// </summary>
    /// <param name="connectionString">A valid connection string for a SqlConnection</param>
    /// <param name="commandText">The stored procedure name or T-SQL command</param>
    /// <returns>An array of SqlParamters</returns>
    public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
        if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText");

        var hashKey = connectionString + ":" + commandText;

        var cachedParameters = ParamCache[hashKey] as SqlParameter[];
        if (cachedParameters == null) return null;
        return CloneParameters(cachedParameters);
    }

    #endregion caching functions

    #region Parameter Discovery Functions

    /// <summary>
    ///     Retrieves the set of SqlParameters appropriate for the stored procedure
    /// </summary>
    /// <remarks>
    ///     This method will query the database for this information, and then store it in a cache for future requests.
    /// </remarks>
    /// <param name="connectionString">A valid connection string for a SqlConnection</param>
    /// <param name="spName">The name of the stored procedure</param>
    /// <returns>An array of SqlParameters</returns>
    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
    {
        return GetSpParameterSet(connectionString, spName, false);
    }

    /// <summary>
    ///     Retrieves the set of SqlParameters appropriate for the stored procedure
    /// </summary>
    /// <remarks>
    ///     This method will query the database for this information, and then store it in a cache for future requests.
    /// </remarks>
    /// <param name="connectionString">A valid connection string for a SqlConnection</param>
    /// <param name="spName">The name of the stored procedure</param>
    /// <param name="includeReturnValueParameter">
    ///     A bool value indicating whether the return value parameter should be included
    ///     in the results
    /// </param>
    /// <returns>An array of SqlParameters</returns>
    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName,
        bool includeReturnValueParameter)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("connectionString");
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException("spName");

        using var connection = new SqlConnection(connectionString);
        return GetSpParameterSetInternal(connection, spName, includeReturnValueParameter);
    }

    /// <summary>
    ///     Retrieves the set of SqlParameters appropriate for the stored procedure
    /// </summary>
    /// <remarks>
    ///     This method will query the database for this information, and then store it in a cache for future requests.
    /// </remarks>
    /// <param name="connection">A valid SqlConnection object</param>
    /// <param name="spName">The name of the stored procedure</param>
    /// <returns>An array of SqlParameters</returns>
    internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName)
    {
        return GetSpParameterSet(connection, spName, false);
    }

    /// <summary>
    ///     Retrieves the set of SqlParameters appropriate for the stored procedure
    /// </summary>
    /// <remarks>
    ///     This method will query the database for this information, and then store it in a cache for future requests.
    /// </remarks>
    /// <param name="connection">A valid SqlConnection object</param>
    /// <param name="spName">The name of the stored procedure</param>
    /// <param name="includeReturnValueParameter">
    ///     A bool value indicating whether the return value parameter should be included
    ///     in the results
    /// </param>
    /// <returns>An array of SqlParameters</returns>
    internal static SqlParameter[] GetSpParameterSet(SqlConnection connection, string spName,
        bool includeReturnValueParameter)
    {
        if (connection == null) throw new ArgumentNullException("connection");
        using var clonedConnection = (SqlConnection)((ICloneable)connection).Clone();
        return GetSpParameterSetInternal(clonedConnection, spName, includeReturnValueParameter);
    }

    /// <summary>
    ///     Retrieves the set of SqlParameters appropriate for the stored procedure
    /// </summary>
    /// <param name="connection">A valid SqlConnection object</param>
    /// <param name="spName">The name of the stored procedure</param>
    /// <param name="includeReturnValueParameter">
    ///     A bool value indicating whether the return value parameter should be included
    ///     in the results
    /// </param>
    /// <returns>An array of SqlParameters</returns>
    private static SqlParameter[] GetSpParameterSetInternal(SqlConnection connection, string spName,
        bool includeReturnValueParameter)
    {
        if (connection == null) throw new ArgumentNullException("connection");
        if (string.IsNullOrEmpty(spName)) throw new ArgumentNullException("spName");
        var hashKey = connection.ConnectionString + ":" + spName +
                      (includeReturnValueParameter ? ":include ReturnValue Parameter" : string.Empty);
        if (ParamCache[hashKey] is SqlParameter[] cachedParameters) return CloneParameters(cachedParameters);
        var spParameters = DiscoverSpParameterSet(connection, spName, includeReturnValueParameter);
        ParamCache[hashKey] = spParameters;
        cachedParameters = spParameters;
        return CloneParameters(cachedParameters);
    }

    #endregion Parameter Discovery Functions
}