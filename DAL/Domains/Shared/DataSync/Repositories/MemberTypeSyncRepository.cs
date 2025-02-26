using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class MemberTypeSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<MemberType>
{
    public MemberTypeSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<MemberType>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberType>> GetIncomingNewDataAsync(MemberType localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberType>> GetIncomingPatchedDataAsync(MemberType localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberType>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberType>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberType>> GetOutgoingNewDataAsync(MemberType externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberType>> GetOutgoingPatchedDataAsync(MemberType externalData)
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

    public Task<NonQueryResult> PullNewAsync(MemberType newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(MemberType patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(MemberType localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(MemberType localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<MemberType>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<MemberType>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<MemberType> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<MemberType> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<MemberType> localData)
    {
        return await PushListAsync(localData);
    }
}