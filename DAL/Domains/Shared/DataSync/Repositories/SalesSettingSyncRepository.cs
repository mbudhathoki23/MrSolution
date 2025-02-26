using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class SalesSettingSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<DatabaseModule.Setup.SystemSetting.SalesSetting>
{
    public SalesSettingSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetIncomingNewDataAsync(DatabaseModule.Setup.SystemSetting.SalesSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetIncomingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.SalesSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetOutgoingNewDataAsync(DatabaseModule.Setup.SystemSetting.SalesSetting externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetOutgoingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.SalesSetting externalData)
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

    public Task<NonQueryResult> PullNewAsync(DatabaseModule.Setup.SystemSetting.SalesSetting newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(DatabaseModule.Setup.SystemSetting.SalesSetting patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(DatabaseModule.Setup.SystemSetting.SalesSetting localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(DatabaseModule.Setup.SystemSetting.SalesSetting localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<DatabaseModule.Setup.SystemSetting.SalesSetting>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<DatabaseModule.Setup.SystemSetting.SalesSetting>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<DatabaseModule.Setup.SystemSetting.SalesSetting> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<DatabaseModule.Setup.SystemSetting.SalesSetting> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<DatabaseModule.Setup.SystemSetting.SalesSetting> localData)
    {
        return await PushListAsync(localData);
    }
}