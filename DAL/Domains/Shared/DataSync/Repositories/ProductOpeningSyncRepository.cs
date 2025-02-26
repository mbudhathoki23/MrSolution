using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.OpeningMaster;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductOpeningSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ProductOpening>
{
    public ProductOpeningSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<ProductOpening>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductOpening>> GetIncomingNewDataAsync(ProductOpening localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductOpening>> GetIncomingPatchedDataAsync(ProductOpening localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductOpening>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductOpening>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductOpening>> GetOutgoingNewDataAsync(ProductOpening externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductOpening>> GetOutgoingPatchedDataAsync(ProductOpening externalData)
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

    public Task<NonQueryResult> PullNewAsync(ProductOpening newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(ProductOpening patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(ProductOpening localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ProductOpening> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(ProductOpening localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<ProductOpening>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ProductOpening> localData)
    {
        return await PutListAsync(localData);
    }
}