using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SalesChallanSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<SC_Master>
{
    public SalesChallanSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<SC_Master>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SC_Master>> GetIncomingNewDataAsync(SC_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SC_Master>> GetIncomingPatchedDataAsync(SC_Master localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SC_Master>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SC_Master>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SC_Master>> GetOutgoingNewDataAsync(SC_Master externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SC_Master>> GetOutgoingPatchedDataAsync(SC_Master externalData)
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

    public Task<NonQueryResult> PullNewAsync(SC_Master newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SC_Master patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SC_Master localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(SC_Master localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<SC_Master>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<SC_Master>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SC_Master> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SC_Master> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<SC_Master> localData)
    {
        return await PushListAsync(localData);
    }
}