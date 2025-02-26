using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SubLedgerSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<SubLedger>
{
    public SubLedgerSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<SubLedger>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SubLedger>> GetIncomingNewDataAsync(SubLedger localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SubLedger>> GetIncomingPatchedDataAsync(SubLedger localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SubLedger>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SubLedger>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SubLedger>> GetOutgoingNewDataAsync(SubLedger externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SubLedger>> GetOutgoingPatchedDataAsync(SubLedger externalData)
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

    public Task<NonQueryResult> PullNewAsync(SubLedger newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SubLedger patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SubLedger localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(SubLedger localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<SubLedger>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<SubLedger>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SubLedger> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SubLedger> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<SubLedger> localData)
    {
        return await PushListAsync(localData);
    }
}