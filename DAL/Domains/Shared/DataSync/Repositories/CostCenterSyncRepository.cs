using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class CostCenterSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<CostCenter>
{
    public CostCenterSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<CostCenter>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CostCenter>> GetIncomingNewDataAsync(CostCenter localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CostCenter>> GetIncomingPatchedDataAsync(CostCenter localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CostCenter>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CostCenter>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CostCenter>> GetOutgoingNewDataAsync(CostCenter externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CostCenter>> GetOutgoingPatchedDataAsync(CostCenter externalData)
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

    public Task<NonQueryResult> PullNewAsync(CostCenter newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(CostCenter patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(CostCenter localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(CostCenter localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<CostCenter>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<CostCenter> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<CostCenter> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<CostCenter> localData)
    {
        return await PushListAsync(localData);
    }
}