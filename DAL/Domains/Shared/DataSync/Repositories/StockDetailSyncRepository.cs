using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class StockDetailSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<StockDetail>
{
    public StockDetailSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<StockDetail>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<StockDetail>> GetIncomingNewDataAsync(StockDetail localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<StockDetail>> GetIncomingPatchedDataAsync(StockDetail localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<StockDetail>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<StockDetail>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<StockDetail>> GetOutgoingNewDataAsync(StockDetail externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<StockDetail>> GetOutgoingPatchedDataAsync(StockDetail externalData)
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

    public Task<NonQueryResult> PullNewAsync(StockDetail newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(StockDetail patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(StockDetail localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(StockDetail localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<StockDetail>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<StockDetail>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<StockDetail> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<StockDetail> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<StockDetail> localData)
    {
        return await PushListAsync(localData);
    }
}