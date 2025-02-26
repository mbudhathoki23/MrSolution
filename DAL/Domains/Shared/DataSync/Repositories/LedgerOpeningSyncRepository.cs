using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.OpeningMaster;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class LedgerOpeningSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<LedgerOpening>
{
    public LedgerOpeningSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<LedgerOpening>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LedgerOpening>> GetIncomingNewDataAsync(LedgerOpening localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LedgerOpening>> GetIncomingPatchedDataAsync(LedgerOpening localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LedgerOpening>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LedgerOpening>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LedgerOpening>> GetOutgoingNewDataAsync(LedgerOpening externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LedgerOpening>> GetOutgoingPatchedDataAsync(LedgerOpening externalData)
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

    public Task<NonQueryResult> PullNewAsync(LedgerOpening newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(LedgerOpening patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(LedgerOpening localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<LedgerOpening> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(LedgerOpening localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<LedgerOpening>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<LedgerOpening>();
    }

    public async Task<NonQueryResult> PutNewListAsync(List<LedgerOpening> localData)
    {
        return await PutListAsync(localData);
    }
}