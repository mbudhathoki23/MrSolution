using DatabaseModule.CloudSync;
using DatabaseModule.Setup.SoftwareRegistration;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SoftwareRegistrationSyncRepository(DbSyncRepoInjectData injectData) : DbSyncRepositoryBase(injectData), IDataSyncRepository<SoftwareRegistration>
{
    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<InfoResult<SoftwareRegistration>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SoftwareRegistration>> GetIncomingNewDataAsync(SoftwareRegistration localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SoftwareRegistration>> GetIncomingPatchedDataAsync(SoftwareRegistration localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SoftwareRegistration>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SoftwareRegistration>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SoftwareRegistration>> GetOutgoingNewDataAsync(SoftwareRegistration externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<SoftwareRegistration>> GetOutgoingPatchedDataAsync(
        SoftwareRegistration externalData)
    {
        throw new NotImplementedException();
    }

    public async Task<ListResult<SoftwareRegistration>> GetUnSynchronizedDataAsync()
    {
        return await GetUnSynchronizedDataAsync<SoftwareRegistration>();
    }

    public Task<NonQueryResult> PullAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullNewAsync(SoftwareRegistration newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(SoftwareRegistration patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(SoftwareRegistration localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SoftwareRegistration> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(SoftwareRegistration localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SoftwareRegistration> localData)
    {
        return await PutListAsync(localData);
    }
}