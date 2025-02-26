using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class InventorySettingSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<DatabaseModule.Setup.SystemSetting.InventorySetting>
{
    public InventorySettingSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetIncomingNewDataAsync(DatabaseModule.Setup.SystemSetting.InventorySetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetIncomingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.InventorySetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetOutgoingNewDataAsync(DatabaseModule.Setup.SystemSetting.InventorySetting externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetOutgoingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.InventorySetting externalData)
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

    public Task<NonQueryResult> PullNewAsync(DatabaseModule.Setup.SystemSetting.InventorySetting newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(DatabaseModule.Setup.SystemSetting.InventorySetting patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(DatabaseModule.Setup.SystemSetting.InventorySetting localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(DatabaseModule.Setup.SystemSetting.InventorySetting localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<DatabaseModule.Setup.SystemSetting.InventorySetting>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<DatabaseModule.Setup.SystemSetting.InventorySetting>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<DatabaseModule.Setup.SystemSetting.InventorySetting> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<DatabaseModule.Setup.SystemSetting.InventorySetting> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<DatabaseModule.Setup.SystemSetting.InventorySetting> localData)
    {
        return await PushListAsync(localData);
    }
}