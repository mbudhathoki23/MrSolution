using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class AccountGroupSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<AccountGroup>
{
    public AccountGroupSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<AccountGroup>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountGroup>> GetIncomingNewDataAsync(AccountGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountGroup>> GetIncomingPatchedDataAsync(AccountGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountGroup>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountGroup>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountGroup>> GetOutgoingNewDataAsync(AccountGroup externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountGroup>> GetOutgoingPatchedDataAsync(AccountGroup externalData)
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

    public Task<NonQueryResult> PullNewAsync(AccountGroup newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(AccountGroup patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(AccountGroup localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(AccountGroup localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<AccountGroup>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<AccountGroup>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<AccountGroup> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<AccountGroup> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<AccountGroup> localData)
    {
        return await PushListAsync(localData);
    }
}