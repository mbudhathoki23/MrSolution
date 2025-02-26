using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class FiscalYearSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<FiscalYear>
{
    public FiscalYearSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<FiscalYear>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FiscalYear>> GetIncomingNewDataAsync(FiscalYear localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FiscalYear>> GetIncomingPatchedDataAsync(FiscalYear localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FiscalYear>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FiscalYear>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FiscalYear>> GetOutgoingNewDataAsync(FiscalYear externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<FiscalYear>> GetOutgoingPatchedDataAsync(FiscalYear externalData)
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

    public Task<NonQueryResult> PullNewAsync(FiscalYear newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(FiscalYear patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(FiscalYear localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(FiscalYear localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<FiscalYear>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<FiscalYear>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<FiscalYear> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<FiscalYear> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<FiscalYear> localData)
    {
        return await PushListAsync(localData);
    }
}