using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SalesSyncNewRepository : DbSyncRepositoryBase, IDataSyncRepository<SB_Master>
{
    public SalesSyncNewRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<SB_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SB_Master>> GetIncomingNewDataAsync(SB_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SB_Master>> GetIncomingPatchedDataAsync(SB_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SB_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SB_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SB_Master>> GetOutgoingNewDataAsync(SB_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SB_Master>> GetOutgoingPatchedDataAsync(SB_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(SB_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SB_Master patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SB_Master localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(SB_Master localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<SB_Master>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<SB_Master>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SB_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SB_Master> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<SB_Master> localData)
    {
        return await PushListAsync(localData);
    }
}