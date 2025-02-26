using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PurchaseSettingSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<DatabaseModule.Setup.SystemSetting.PurchaseSetting>
{
    public PurchaseSettingSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetIncomingNewDataAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetIncomingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetOutgoingNewDataAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetOutgoingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting externalData)
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

    public Task<NonQueryResult> PullNewAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(DatabaseModule.Setup.SystemSetting.PurchaseSetting localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<DatabaseModule.Setup.SystemSetting.PurchaseSetting>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<DatabaseModule.Setup.SystemSetting.PurchaseSetting>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<DatabaseModule.Setup.SystemSetting.PurchaseSetting> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<DatabaseModule.Setup.SystemSetting.PurchaseSetting> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<DatabaseModule.Setup.SystemSetting.PurchaseSetting> localData)
    {
        return await PushListAsync(localData);
    }
}