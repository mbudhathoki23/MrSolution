using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductGroupSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ProductGroup>
{
    public ProductGroupSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<ProductGroup>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductGroup>> GetIncomingNewDataAsync(ProductGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductGroup>> GetIncomingPatchedDataAsync(ProductGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductGroup>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductGroup>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductGroup>> GetOutgoingNewDataAsync(ProductGroup externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductGroup>> GetOutgoingPatchedDataAsync(ProductGroup externalData)
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

    public Task<NonQueryResult> PullNewAsync(ProductGroup newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(ProductGroup patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(ProductGroup localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(ProductGroup localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<ProductGroup>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<ProductGroup>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ProductGroup> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ProductGroup> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<ProductGroup> localData)
    {
        return await PushListAsync(localData);
    }
}