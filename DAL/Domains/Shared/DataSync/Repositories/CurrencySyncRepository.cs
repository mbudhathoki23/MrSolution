using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class CurrencySyncRepository : DbSyncRepositoryBase, IDataSyncRepository<Currency>
{
    public CurrencySyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<Currency>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Currency>> GetIncomingNewDataAsync(Currency localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Currency>> GetIncomingPatchedDataAsync(Currency localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Currency>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Currency>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Currency>> GetOutgoingNewDataAsync(Currency externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Currency>> GetOutgoingPatchedDataAsync(Currency externalData)
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

    public Task<NonQueryResult> PullNewAsync(Currency newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(Currency patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(Currency localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(Currency localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<Currency>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<Currency>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<Currency> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<Currency> localData)
    {
        return await PutListAsync(localData);
    }

}