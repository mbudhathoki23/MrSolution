using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SalesReturnSyncNewRepository : DbSyncRepositoryBase, IDataSyncRepository<SR_Master>
{
    public SalesReturnSyncNewRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<SR_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SR_Master>> GetIncomingNewDataAsync(SR_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SR_Master>> GetIncomingPatchedDataAsync(SR_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SR_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SR_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SR_Master>> GetOutgoingNewDataAsync(SR_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SR_Master>> GetOutgoingPatchedDataAsync(SR_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(SR_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SR_Master patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SR_Master localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(SR_Master localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<SR_Master>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<SR_Master>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SR_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SR_Master> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<SR_Master> localData)
    {
        return await PushListAsync(localData);
    }
}