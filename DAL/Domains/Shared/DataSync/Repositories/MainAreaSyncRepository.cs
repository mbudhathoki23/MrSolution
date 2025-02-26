using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class MainAreaSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<MainArea>
{
    public MainAreaSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<MainArea>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainArea>> GetIncomingNewDataAsync(MainArea localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainArea>> GetIncomingPatchedDataAsync(MainArea localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainArea>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainArea>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainArea>> GetOutgoingNewDataAsync(MainArea externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainArea>> GetOutgoingPatchedDataAsync(MainArea externalData)
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

    public Task<NonQueryResult> PullNewAsync(MainArea newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(MainArea patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(MainArea localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(MainArea localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<MainArea>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<MainArea>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<MainArea> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<MainArea> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<MainArea> localData)
    {
        return await PushListAsync(localData);
    }
}