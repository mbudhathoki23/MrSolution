using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SyncLogDetailRepository : DbSyncRepositoryBase, IDataSyncRepository<SyncLogDetailModel>
{
    public SyncLogDetailRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetIncomingNewDataAsync(SyncLogDetailModel localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetIncomingPatchedDataAsync(SyncLogDetailModel localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetOutgoingNewDataAsync(SyncLogDetailModel externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SyncLogDetailModel>> GetOutgoingPatchedDataAsync(SyncLogDetailModel externalData)
    {
        throw new NotImplementedException();
    }

    public Task<ListResult<SyncLogDetailModel>> GetUnSynchronizedDataAsync()
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

    public Task<NonQueryResult> PullNewAsync(SyncLogDetailModel newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SyncLogDetailModel patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SyncLogDetailModel localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SyncLogDetailModel> localData)
    {
        return await PushListAsync(localData);
    }

    public Task<NonQueryResult> PutNewAsync(SyncLogDetailModel localData)
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SyncLogDetailModel> localData)
    {
        return await PutListAsync(localData);
    }
}