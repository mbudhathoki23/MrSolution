using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductSubGroupSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ProductSubGroup>
{
    public ProductSubGroupSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<ProductSubGroup>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductSubGroup>> GetIncomingNewDataAsync(ProductSubGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductSubGroup>> GetIncomingPatchedDataAsync(ProductSubGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductSubGroup>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductSubGroup>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductSubGroup>> GetOutgoingNewDataAsync(ProductSubGroup externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<ProductSubGroup>> GetOutgoingPatchedDataAsync(ProductSubGroup externalData)
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

    public Task<NonQueryResult> PullNewAsync(ProductSubGroup newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(ProductSubGroup patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(ProductSubGroup localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(ProductSubGroup localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<ProductSubGroup>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<ProductSubGroup>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ProductSubGroup> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ProductSubGroup> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<ProductSubGroup> localData)
    {
        return await PushListAsync(localData);
    }
}