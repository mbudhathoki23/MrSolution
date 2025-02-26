using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class GodownSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<Godown>
{
    public GodownSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<Godown>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Godown>> GetIncomingNewDataAsync(Godown localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Godown>> GetIncomingPatchedDataAsync(Godown localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Godown>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Godown>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Godown>> GetOutgoingNewDataAsync(Godown externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Godown>> GetOutgoingPatchedDataAsync(Godown externalData)
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

    public Task<NonQueryResult> PullNewAsync(Godown newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(Godown patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(Godown localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(Godown localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<Godown>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<Godown>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<Godown> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<Godown> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<Godown> localData)
    {
        return await PushListAsync(localData);
    }
}