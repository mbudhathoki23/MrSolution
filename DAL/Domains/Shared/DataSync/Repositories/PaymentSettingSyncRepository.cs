using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PaymentSettingSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<DatabaseModule.Setup.SystemSetting.PaymentSetting>
{
    public PaymentSettingSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetIncomingNewDataAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetIncomingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetOutgoingNewDataAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetOutgoingPatchedDataAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting externalData)
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

    public Task<NonQueryResult> PullNewAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(DatabaseModule.Setup.SystemSetting.PaymentSetting localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<DatabaseModule.Setup.SystemSetting.PaymentSetting>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<DatabaseModule.Setup.SystemSetting.PaymentSetting>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<DatabaseModule.Setup.SystemSetting.PaymentSetting> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<DatabaseModule.Setup.SystemSetting.PaymentSetting> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<DatabaseModule.Setup.SystemSetting.PaymentSetting> localData)
    {
        return await PushListAsync(localData);
    }
}