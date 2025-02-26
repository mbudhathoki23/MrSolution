using Dapper;
using DatabaseModule.CloudSync;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using System;
using System.Data.SqlClient;

namespace MrDAL.Domains.Shared.DataSync.Common;

public static class DataSyncHelper
{
    private static SyncApiConfig _apiConfig;

    public static SyncApiConfig GetConfig()
    {
        return _apiConfig;
    }

    public static InfoResult<ValueModel<string, string, Guid>> GetConfigParams(int globalCompanyId, string connString)
    {
        var result = new InfoResult<ValueModel<string, string, Guid>>();
        var conn = new SqlConnection(connString);
        try
        {
            var cmdString = @$"SELECT * FROM master.AMS.GlobalCompany WHERE GComp_Id='{globalCompanyId}'";
            conn.Open();
            var data = conn.QueryFirstOrDefault<dynamic>(cmdString);

            if (data != null)
            {
                if (string.IsNullOrEmpty(data.DataSyncApiBaseUrl))
                {
                    result.Success = false;
                }
                else
                {
                    result.Model = new ValueModel<string, string, Guid>(ObjGlobal.Decrypt(data.DataSyncOriginId),
                        ObjGlobal.Decrypt(data.DataSyncApiBaseUrl), data.ApiKey);
                    result.Success = true;
                }
            }
            else
            {
                result.ErrorMessage = "Configuration Doesn't exist.";
                result.ResultType = ResultType.EntityNotExists;
            }
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ValueModel<string, string, Guid>>("DataSyncHelper");
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public static void SetConfig(SyncApiConfig config)
    {
        _apiConfig = config ?? throw new ArgumentNullException(nameof(config));
    }
}