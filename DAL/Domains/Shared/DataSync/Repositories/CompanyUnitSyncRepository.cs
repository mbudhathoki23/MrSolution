using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseModule.Setup.CompanyMaster;


namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class CompanyUnitSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<CompanyUnit>
{
    public CompanyUnitSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public Task<InfoResult<CompanyUnit>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CompanyUnit>> GetIncomingNewDataAsync(CompanyUnit localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CompanyUnit>> GetIncomingPatchedDataAsync(CompanyUnit localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CompanyUnit>> GetLocalDataAsync()
    {
        throw new System.NotImplementedException();
    }

    public Task<InfoResult<CompanyUnit>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CompanyUnit>> GetOutgoingNewDataAsync(CompanyUnit externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<CompanyUnit>> GetOutgoingPatchedDataAsync(CompanyUnit externalData)
    {
        throw new NotImplementedException();
    }

    public async Task<ListResult<CompanyUnit>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<CompanyUnit>();
    }

    public Task<NonQueryResult> PullAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullNewAsync(CompanyUnit newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(CompanyUnit patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(CompanyUnit localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<CompanyUnit> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(CompanyUnit localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<CompanyUnit> localData)
    {
        return await PutListAsync(localData);
    }


}