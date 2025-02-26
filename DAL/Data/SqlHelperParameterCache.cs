using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace MrDAL.Data;

public sealed class SqlHelperParameterCache
{
    private static readonly Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

    // Since this class provides only static methods, make the default constructor private to prevent
    // instances from being created with "new SqlHelperParameterCache()".
    private SqlHelperParameterCache()
    {
    } // New

    // resolve at run time the appropriate set of SqlParameters for a stored procedure
    // Parameters:
    // - connectionString - a valid connection string for a SqlConnection
    // - spName - the name of the stored procedure
    // - includeReturnValueParameter - whether or not to include their return value parameter>
    // Returns: SqlParameter()
    private static SqlParameter[] DiscoverSpParameterSet(string connectionString, string spName,
        bool includeReturnValueParameter, params object[] parameterValues)
    {
        var cn = new SqlConnection(connectionString);
        var cmd = new SqlCommand(spName, cn);
        SqlParameter[] discoveredParameters;

        try
        {
            cn.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommandBuilder.DeriveParameters(cmd);
            if (!includeReturnValueParameter)
                cmd.Parameters.RemoveAt(0);

            discoveredParameters = new SqlParameter[cmd.Parameters.Count - 1 + 1];
            cmd.Parameters.CopyTo(discoveredParameters, 0);
        }
        finally
        {
            cmd.Dispose();
            cn.Dispose();
        }

        return discoveredParameters;
    } // DiscoverSpParameterSet

    // deep copy of cached SqlParameter array
    private static SqlParameter[] CloneParameters(SqlParameter[] originalParameters)
    {
        short i;
        var j = originalParameters.Length - 1;
        var clonedParameters = new SqlParameter[j + 1];

        for (i = 0; i <= j; i++)
            clonedParameters[i] = (SqlParameter)((ICloneable)originalParameters[i]).Clone();

        return clonedParameters;
    } // CloneParameters

    // add parameter array to the cache
    // Parameters
    // -connectionString - a valid connection string for a SqlConnection
    // -commandText - the stored procedure name or T-SQL command
    // -commandParameters - an array of SqlParamters to be cached
    public static void CacheParameterSet(string connectionString, string commandText,
        params SqlParameter[] commandParameters)
    {
        var hashKey = connectionString + ":" + commandText;

        paramCache[hashKey] = commandParameters;
    } // CacheParameterSet

    // retrieve a parameter array from the cache
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -commandText - the stored procedure name or T-SQL command
    // Returns: an array of SqlParamters
    public static SqlParameter[] GetCachedParameterSet(string connectionString, string commandText)
    {
        var hashKey = connectionString + ":" + commandText;
        var cachedParameters = (SqlParameter[])paramCache[hashKey];

        if (cachedParameters == null)
            return null;
        return CloneParameters(cachedParameters);
    } // GetCachedParameterSet

    // Retrieves the set of SqlParameters appropriate for the stored procedure
    //
    // This method will query the database for this information, and then store it in a cache for future requests.
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -spName - the name of the stored procedure
    // Returns: an array of SqlParameters
    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName)
    {
        return GetSpParameterSet(connectionString, spName, false);
    } // GetSpParameterSet

    // Retrieves the set of SqlParameters appropriate for the stored procedure
    //
    // This method will query the database for this information, and then store it in a cache for future requests.
    // Parameters:
    // -connectionString - a valid connection string for a SqlConnection
    // -spName - the name of the stored procedure
    // -includeReturnValueParameter - a bool value indicating whether the return value parameter should be included in the results
    // Returns: an array of SqlParameters
    public static SqlParameter[] GetSpParameterSet(string connectionString, string spName,
        bool includeReturnValueParameter)
    {
        // todo implement => uncomment these lines and perform tunings

        throw new NotImplementedException();

        //SqlParameter[] cachedParameters;
        //string hashKey;

        //hashKey = connectionString + ":" + spName + Interaction.IIf(includeReturnValueParameter == true, ":include ReturnValue Parameter", "");

        //cachedParameters = (SqlParameter[])paramCache[hashKey];

        //if ((cachedParameters == null))
        //{
        //    paramCache[hashKey] = DiscoverSpParameterSet(connectionString, spName, includeReturnValueParameter);
        //    cachedParameters = (SqlParameter[])paramCache[hashKey];
        //}

        //return CloneParameters(cachedParameters);
    } // GetSpParameterSet
}