using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SalesOrderSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<SO_Master>
{
    public SalesOrderSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }
    public Task<InfoResult<SO_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SO_Master>> GetIncomingNewDataAsync(SO_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SO_Master>> GetIncomingPatchedDataAsync(SO_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SO_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SO_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SO_Master>> GetOutgoingNewDataAsync(SO_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SO_Master>> GetOutgoingPatchedDataAsync(SO_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(SO_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SO_Master patchedData)
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
    public async Task<NonQueryResult> PushNewAsync(SO_Master localData)
    {
        return await PushAsync(localData);
    }
    public async Task<NonQueryResult> PutNewAsync(SO_Master localData)
    {
        return await PutAsync(localData);
    }
    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }
    public async Task<ListResult<SO_Master>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<SO_Master>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SO_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SO_Master> localData)
    {
        return await PutListAsync(localData);
    }

}