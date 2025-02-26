using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PostDateChequeSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<PostDateCheque>
{
    public PostDateChequeSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<PostDateCheque>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PostDateCheque>> GetIncomingNewDataAsync(PostDateCheque localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PostDateCheque>> GetIncomingPatchedDataAsync(PostDateCheque localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PostDateCheque>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PostDateCheque>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PostDateCheque>> GetOutgoingNewDataAsync(PostDateCheque externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PostDateCheque>> GetOutgoingPatchedDataAsync(PostDateCheque externalData)
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

    public Task<NonQueryResult> PullNewAsync(PostDateCheque newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(PostDateCheque patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(PostDateCheque localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(PostDateCheque localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<PostDateCheque>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<PostDateCheque> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<PostDateCheque> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<PostDateCheque> localData)
    {
        return await PushListAsync(localData);
    }
}