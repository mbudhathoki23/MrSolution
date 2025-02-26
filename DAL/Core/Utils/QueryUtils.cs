using Dapper;
using DatabaseModule.CloudSync;
using MrDAL.Data;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace MrDAL.Core.Utils;
public static class QueryUtils
{
    public static async Task<InfoResult<T>> GetEntityAsync<T>(string sql, object parameters = null) where T : AppBaseEntity
    {
        var result = new InfoResult<T>();
        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var model = await conn.QueryFirstOrDefaultAsync<T>(sql, parameters);
            result.Model = model;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<T>("MrDAL.Core.Utils.QueryUtils");
        }
        return result;
    }

    public static async Task<InfoResult<T>> GetFirstOrDefaultAsync<T>(string sql, object parameters = null)
    {
        var result = new InfoResult<T>();
        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var model = await conn.QueryFirstOrDefaultAsync<T>(sql, parameters);
            result.Model = model;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<T>("MrDAL.Core.Utils.QueryUtils");
        }
        return result;
    }

    public static async Task<InfoResult<T>> GetEntityAsync<T>(object id) where T : AppBaseEntity
    {
        var result = new InfoResult<T>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var model = await conn.GetAsync<T>(id);
            result.Model = model;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<T>("MrDAL.Core.Utils.QueryUtils");
        }
        return result;
    }

    public static async Task<ListResult<T>> GetListAsync<T>(string sql, object parameters = null)
    {
        var result = new ListResult<T>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.QueryAsync<T>(sql, parameters);
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<T>("MrDAL.Core.Utils.QueryUtils");
        }

        return result;
    }
    public static ListResult<T> GetList<T>(string sql, object parameters = null)
    {
        var result = new ListResult<T>();

        try
        {
            using var conn = GetConnection.ReturnConnection();
            var records = conn.Query<T>(sql, parameters);
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<T>("MrDAL.Core.Utils.QueryUtils");
        }

        return result;
    }
    public static async Task<ListResult<T>> GetListAsync<T>() where T : BaseSyncData
    {
        var result = new ListResult<T>();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            var records = await conn.GetAllAsync<T>();
            result.List = records.AsList();
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<T>("MrDAL.Core.Utils.QueryUtils");
        }

        return result;
    }
    public static async Task<NonQueryResult> ExecNonQueryAsync(string sql, object parameters = null)
    {
        var result = new NonQueryResult();

        try
        {
            using var conn = new SqlConnection(GetConnection.ConnectionString);
            result.Value = await conn.ExecuteAsync(sql, parameters) > 0;
            result.Completed = true;
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult("MrDAL.Core.Utils.QueryUtils");
        }

        return result;
    }
}