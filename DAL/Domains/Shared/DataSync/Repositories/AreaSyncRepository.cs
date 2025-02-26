using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class AreaSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<Area>
{
    public AreaSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<Area>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Area>> GetIncomingNewDataAsync(Area localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Area>> GetIncomingPatchedDataAsync(Area localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Area>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Area>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Area>> GetOutgoingNewDataAsync(Area externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Area>> GetOutgoingPatchedDataAsync(Area externalData)
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

    public Task<NonQueryResult> PullNewAsync(Area newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(Area patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(Area localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(Area localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<Area>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<Area>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<Area> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<Area> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<Area> localData)
    {
        return await PushListAsync(localData);
    }
}