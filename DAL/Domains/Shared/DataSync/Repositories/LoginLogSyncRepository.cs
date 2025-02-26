using DatabaseModule.CloudSync;
using DatabaseModule.Setup.LogSetting;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class LoginLogSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<LOGIN_LOG>
{
    public LoginLogSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetIncomingNewDataAsync(LOGIN_LOG localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetIncomingPatchedDataAsync(LOGIN_LOG localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetOutgoingNewDataAsync(LOGIN_LOG externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LOGIN_LOG>> GetOutgoingPatchedDataAsync(LOGIN_LOG externalData)
    {
        throw new NotImplementedException();
    }

    public Task<ListResult<LOGIN_LOG>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullNewAsync(LOGIN_LOG newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(LOGIN_LOG patchedData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PushAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PushAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewAsync(LOGIN_LOG localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<LOGIN_LOG> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(LOGIN_LOG localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<LOGIN_LOG> localData)
    {
        return await PutListAsync(localData);
    }
}