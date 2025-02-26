using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class BranchSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<Branch>
{
    public BranchSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<Branch>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Branch>> GetIncomingNewDataAsync(Branch localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Branch>> GetIncomingPatchedDataAsync(Branch localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Branch>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Branch>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Branch>> GetOutgoingNewDataAsync(Branch externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<Branch>> GetOutgoingPatchedDataAsync(Branch externalData)
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

    public Task<NonQueryResult> PullNewAsync(Branch newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(Branch patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(Branch localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(Branch localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<Branch>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<Branch>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<Branch> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<Branch> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<Branch> localData)
    {
        return await PushListAsync(localData);
    }
}