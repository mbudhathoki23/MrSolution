using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PurchaseChallanSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<PC_Master>
{
    public PurchaseChallanSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<PC_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PC_Master>> GetIncomingNewDataAsync(PC_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PC_Master>> GetIncomingPatchedDataAsync(PC_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PC_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PC_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PC_Master>> GetOutgoingNewDataAsync(PC_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PC_Master>> GetOutgoingPatchedDataAsync(PC_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(PC_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(PC_Master patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(PC_Master localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(PC_Master localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<PC_Master>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<PC_Master>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<PC_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<PC_Master> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<PC_Master> localData)
    {
        return await PushListAsync(localData);
    }
}