using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class RackSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<RACK>
{
    public RackSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<RACK>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<RACK>> GetIncomingNewDataAsync(RACK localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<RACK>> GetIncomingPatchedDataAsync(RACK localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<RACK>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<RACK>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<RACK>> GetOutgoingNewDataAsync(RACK externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<RACK>> GetOutgoingPatchedDataAsync(RACK externalData)
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

    public Task<NonQueryResult> PullNewAsync(RACK newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(RACK patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(RACK localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(RACK localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<RACK>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<RACK>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<RACK> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<RACK> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<RACK> localData)
    {
        return await PushListAsync(localData);
    }
}