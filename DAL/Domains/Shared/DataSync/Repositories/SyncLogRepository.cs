using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SyncLogRepository : DbSyncRepositoryBase, IDataSyncRepository<SyncLogModel>
{
    public SyncLogRepository
        (DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetIncomingNewDataAsync(SyncLogModel localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetIncomingPatchedDataAsync(SyncLogModel localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetOutgoingNewDataAsync(SyncLogModel externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogModel>> GetOutgoingPatchedDataAsync(SyncLogModel externalData)
    {
        throw new NotImplementedException();
    }

    public async Task<ListResult<SyncLogModel>> GetUnSynchronizedDataAsync()
    {
        var response = await base.GetUnSynchronizedDataAsync<SyncLogModel>();
        return response;
    }

    public Task<NonQueryResult> PullAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullNewAsync(SyncLogModel newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SyncLogModel patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SyncLogModel localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SyncLogModel> localData)
    {
        return await PushListAsync(localData);
    }

    public Task<NonQueryResult> PutNewAsync(SyncLogModel localData)
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SyncLogModel> localData)
    {
        return await PutListAsync(localData);
    }
}