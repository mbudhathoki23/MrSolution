using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class CounterSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<Counter>
{
    public CounterSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<Counter>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Counter>> GetIncomingNewDataAsync(Counter localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Counter>> GetIncomingPatchedDataAsync(Counter localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Counter>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Counter>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Counter>> GetOutgoingNewDataAsync(Counter externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Counter>> GetOutgoingPatchedDataAsync(Counter externalData)
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

    public Task<NonQueryResult> PullNewAsync(Counter newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(Counter patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(Counter localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(Counter localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<Counter>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<Counter>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<Counter> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<Counter> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<Counter> localData)
    {
        return await PushListAsync(localData);
    }
}