using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class AccountSubGroupSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<AccountSubGroup>
{
    public AccountSubGroupSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<AccountSubGroup>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountSubGroup>> GetIncomingNewDataAsync(AccountSubGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountSubGroup>> GetIncomingPatchedDataAsync(AccountSubGroup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountSubGroup>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountSubGroup>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountSubGroup>> GetOutgoingNewDataAsync(AccountSubGroup externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<AccountSubGroup>> GetOutgoingPatchedDataAsync(AccountSubGroup externalData)
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

    public Task<NonQueryResult> PullNewAsync(AccountSubGroup newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(AccountSubGroup patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(AccountSubGroup localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(AccountSubGroup localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<AccountSubGroup>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<AccountSubGroup>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<AccountSubGroup> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<AccountSubGroup> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<AccountSubGroup> localData)
    {
        return await PushListAsync(localData);
    }
}