using DatabaseModule.CloudSync;
using System;

namespace MrDAL.Domains.Shared.DataSync.Common;

public static class DataSyncManager
{
    private static SyncApiConfig _apiConfig;
    private static DbSyncRepoInjectData _injectData;

    public static SyncApiConfig GetConfig()
    {
        return _apiConfig ?? new SyncApiConfig
        {
            BaseUrl = string.Empty,
            Username = "admin",
            Password = "password"
        };
    }

    public static DbSyncRepoInjectData GetGlobalInjectData()
    {
        if (_injectData == null) return _injectData;
        _injectData.DateTime = DateTime.Now;
        return _injectData;
        //throw new NullReferenceException("The global inject data has not been initialized");
    }

    public static void SetConfig(SyncApiConfig config)
    {
        _apiConfig = config;
    }

    public static void SetGlobalInjectData(DbSyncRepoInjectData injectData)
    {
        const string msg = "The parameter must not be null..";
        _injectData = injectData ?? throw new ArgumentNullException(msg, nameof(injectData));
    }
}