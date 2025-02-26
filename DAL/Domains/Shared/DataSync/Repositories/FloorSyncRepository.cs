using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class FloorSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<FloorSetup>
{
    public FloorSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<FloorSetup>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FloorSetup>> GetIncomingNewDataAsync(FloorSetup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FloorSetup>> GetIncomingPatchedDataAsync(FloorSetup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FloorSetup>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FloorSetup>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FloorSetup>> GetOutgoingNewDataAsync(FloorSetup externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FloorSetup>> GetOutgoingPatchedDataAsync(FloorSetup externalData)
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

    public Task<NonQueryResult> PullNewAsync(FloorSetup newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(FloorSetup patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(FloorSetup localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(FloorSetup localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<FloorSetup>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<FloorSetup>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<FloorSetup> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<FloorSetup> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<FloorSetup> localData)
    {
        return await PushListAsync(localData);
    }
}