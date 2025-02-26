using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class IncomeTaxSettingSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>
{
    public IncomeTaxSettingSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetIncomingNewDataAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetIncomingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetOutgoingNewDataAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetOutgoingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting externalData)
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

    public Task<NonQueryResult> PullNewAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<DatabaseModule.Setup.SystemSetting.IncomeTaxSetting> localData)
    {
        return await PushListAsync(localData);
    }
}