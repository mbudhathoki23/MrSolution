using Dapper;
using MrDAL.Core.Utils;
using MrDAL.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace MrDAL.Core.Extensions;

public static class DapperExt
{
    public static int NewBigIntId(this SqlConnection conn, string tableName, string idField, IDbTransaction trans = null)
    {
        var newId = 0;

        try
        {
            var sql = $@"SELECT ISNULL(MAX({idField}),0)+1 AS Id FROM {tableName}";
            newId = conn.QueryFirstOrDefault<int>(sql, transaction: trans);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            //
        }

        return newId;
    }

    public static async Task<long> NewBigIntIdAsync(this SqlConnection conn, string tableName, string idField, IDbTransaction trans = null)
    {
        long newId = 0;
        try
        {
            newId = await conn.QueryFirstOrDefaultAsync<long>(
                $@"SELECT ISNULL(MAX({idField}),0)+ 1 AS idd FROM {tableName} ", transaction: trans);
        }
        catch (Exception e)
        {
            e.ToInfoErrorResult<long>("DapperExt");
        }

        return newId;
    }

    public static int NewIntegerId(this SqlConnection conn, string tableName, string idField, IDbTransaction trans = null)
    {
        var newId = 0;
        try
        {
            newId = conn.QueryFirstOrDefault<int>($@"SELECT ISNULL(MAX({idField}),0)+1 AS Id FROM {tableName}",
                transaction: trans);
        }
        catch (Exception e)
        {
            e.ToInfoErrorResult<int>("DapperExt");
        }

        return newId;
    }

    public static async Task<int> NewIntegerIdAsync(this SqlConnection conn, string tableName, string idField, IDbTransaction trans = null)
    {
        var newId = 0;

        try
        {
            newId = await conn.QueryFirstOrDefaultAsync<int>(
                $@"SELECT ISNULL(MAX({idField}),0)+1 AS Id FROM {tableName}", transaction: trans);
        }
        catch (Exception e)
        {
            e.ToInfoErrorResult<int>("DapperExt");
        }

        return newId;
    }

    public static T GetModel<T>(string tableName) where T : AppBaseEntity
    {
        throw new NotImplementedException();
    }
}