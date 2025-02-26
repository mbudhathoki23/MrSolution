using DatabaseModule.CloudSync;
using DatabaseModule.Setup.TermSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SalesBillingTermSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ST_Term>
{
    public SalesBillingTermSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<ST_Term>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ST_Term>> GetIncomingNewDataAsync(ST_Term localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ST_Term>> GetIncomingPatchedDataAsync(ST_Term localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ST_Term>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ST_Term>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ST_Term>> GetOutgoingNewDataAsync(ST_Term externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ST_Term>> GetOutgoingPatchedDataAsync(ST_Term externalData)
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

    public Task<NonQueryResult> PullNewAsync(ST_Term newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(ST_Term patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(ST_Term localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(ST_Term localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<ST_Term>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<ST_Term>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ST_Term> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ST_Term> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<ST_Term> localData)
    {
        return await PushListAsync(localData);
    }
}