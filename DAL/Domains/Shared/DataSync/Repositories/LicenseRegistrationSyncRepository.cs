using DatabaseModule.CloudSync;
using DatabaseModule.Setup.SoftwareRegistration;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class LicenseRegistrationSyncRepository(DbSyncRepoInjectData injectData) : DbSyncRepositoryBase(injectData), IDataSyncRepository<LicenseInfo>
{
    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<InfoResult<LicenseInfo>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LicenseInfo>> GetIncomingNewDataAsync(LicenseInfo localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LicenseInfo>> GetIncomingPatchedDataAsync(LicenseInfo localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LicenseInfo>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LicenseInfo>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LicenseInfo>> GetOutgoingNewDataAsync(LicenseInfo externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<LicenseInfo>> GetOutgoingPatchedDataAsync(
        LicenseInfo externalData)
    {
        throw new NotImplementedException();
    }

    public async Task<ListResult<LicenseInfo>> GetUnSynchronizedDataAsync()
    {
        return await GetUnSynchronizedDataAsync<LicenseInfo>().ConfigureAwait(false);
    }

    public Task<NonQueryResult> PullAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullNewAsync(LicenseInfo newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(LicenseInfo patchedData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PushAllPatchedAsync()
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PushAllNewAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewAsync(LicenseInfo localData)
    {
        return await PushAsync(localData).ConfigureAwait(false);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<LicenseInfo> localData)
    {
        return await PushListAsync(localData).ConfigureAwait(false);
    }

    public async Task<NonQueryResult> PutNewAsync(LicenseInfo localData)
    {
        return await PutAsync(localData).ConfigureAwait(false);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<LicenseInfo> localData)
    {
        return await PutListAsync(localData).ConfigureAwait(false);
    }
}