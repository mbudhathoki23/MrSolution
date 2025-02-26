using DatabaseModule.CloudSync;
using DatabaseModule.Setup.TermSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PurchaseBillingTermSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<PT_Term>
{
    public PurchaseBillingTermSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<PT_Term>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PT_Term>> GetIncomingNewDataAsync(PT_Term localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PT_Term>> GetIncomingPatchedDataAsync(PT_Term localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PT_Term>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PT_Term>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PT_Term>> GetOutgoingNewDataAsync(PT_Term externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<PT_Term>> GetOutgoingPatchedDataAsync(PT_Term externalData)
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

    public Task<NonQueryResult> PullNewAsync(PT_Term newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(PT_Term patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(PT_Term localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(PT_Term localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<PT_Term>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<PT_Term>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<PT_Term> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<PT_Term> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<PT_Term> localData)
    {
        return await PushListAsync(localData);
    }
}