using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductAddInfoSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ProductAddInfo>
{
    public ProductAddInfoSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<ProductAddInfo>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductAddInfo>> GetIncomingNewDataAsync(ProductAddInfo localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductAddInfo>> GetIncomingPatchedDataAsync(ProductAddInfo localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductAddInfo>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductAddInfo>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductAddInfo>> GetOutgoingNewDataAsync(ProductAddInfo externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductAddInfo>> GetOutgoingPatchedDataAsync(ProductAddInfo externalData)
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

    public Task<NonQueryResult> PullNewAsync(ProductAddInfo newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(ProductAddInfo patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(ProductAddInfo localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(ProductAddInfo localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<ProductAddInfo>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ProductAddInfo> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ProductAddInfo> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<ProductAddInfo> localData)
    {
        return await PushListAsync(localData);
    }
}