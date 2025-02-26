using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductUnitSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ProductUnit>
{
    public ProductUnitSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<ProductUnit>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductUnit>> GetIncomingNewDataAsync(ProductUnit localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductUnit>> GetIncomingPatchedDataAsync(ProductUnit localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductUnit>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductUnit>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductUnit>> GetOutgoingNewDataAsync(ProductUnit externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductUnit>> GetOutgoingPatchedDataAsync(ProductUnit externalData)
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

    public Task<NonQueryResult> PullNewAsync(ProductUnit newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(ProductUnit patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(ProductUnit localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(ProductUnit localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<ProductUnit>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<ProductUnit>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ProductUnit> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ProductUnit> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<ProductUnit> localData)
    {
        return await PushListAsync(localData);
    }
}