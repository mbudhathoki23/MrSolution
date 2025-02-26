using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class JuniorAgentSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<JuniorAgent>
{
    public JuniorAgentSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<JuniorAgent>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<JuniorAgent>> GetIncomingNewDataAsync(JuniorAgent localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<JuniorAgent>> GetIncomingPatchedDataAsync(JuniorAgent localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<JuniorAgent>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<JuniorAgent>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<JuniorAgent>> GetOutgoingNewDataAsync(JuniorAgent externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<JuniorAgent>> GetOutgoingPatchedDataAsync(JuniorAgent externalData)
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

    public Task<NonQueryResult> PullNewAsync(JuniorAgent newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(JuniorAgent patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(JuniorAgent localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(JuniorAgent localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<JuniorAgent>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<JuniorAgent>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<JuniorAgent> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<JuniorAgent> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<JuniorAgent> localData)
    {
        return await PushListAsync(localData);
    }
}