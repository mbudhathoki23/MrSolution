using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PurchaseChallanReturnSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<PCR_Master>
{
    public PurchaseChallanReturnSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<PCR_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PCR_Master>> GetIncomingNewDataAsync(PCR_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PCR_Master>> GetIncomingPatchedDataAsync(
        PCR_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PCR_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PCR_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PCR_Master>> GetOutgoingNewDataAsync(PCR_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PCR_Master>> GetOutgoingPatchedDataAsync(
        PCR_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(PCR_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(PCR_Master patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(PCR_Master localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(PCR_Master localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<PCR_Master>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<PCR_Master>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<PCR_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<PCR_Master> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<PCR_Master> localData)
    {
        return await PushListAsync(localData);
    }
}