using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SeniorAgentSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<MainAgent>
{
    public SeniorAgentSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<MainAgent>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainAgent>> GetIncomingNewDataAsync(MainAgent localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainAgent>> GetIncomingPatchedDataAsync(MainAgent localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainAgent>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainAgent>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainAgent>> GetOutgoingNewDataAsync(MainAgent externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MainAgent>> GetOutgoingPatchedDataAsync(MainAgent externalData)
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

    public Task<NonQueryResult> PullNewAsync(MainAgent newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(MainAgent patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(MainAgent localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(MainAgent localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<MainAgent>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<MainAgent>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<MainAgent> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<MainAgent> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<MainAgent> localData)
    {
        return await PushListAsync(localData);
    }
}