using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.StockTransaction.ProductScheme;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductSchemeSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<Scheme_Master>
{
    public ProductSchemeSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<Scheme_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Scheme_Master>> GetIncomingNewDataAsync(Scheme_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Scheme_Master>> GetIncomingPatchedDataAsync(Scheme_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Scheme_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Scheme_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Scheme_Master>> GetOutgoingNewDataAsync(Scheme_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Scheme_Master>> GetOutgoingPatchedDataAsync(Scheme_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(Scheme_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(Scheme_Master patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(Scheme_Master localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(Scheme_Master localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<Scheme_Master>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<Scheme_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<Scheme_Master> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<Scheme_Master> localData)
    {
        return await PushListAsync(localData);
    }
}