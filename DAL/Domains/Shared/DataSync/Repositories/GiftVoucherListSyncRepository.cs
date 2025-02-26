using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class GiftVoucherListSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<GiftVoucherList>
{
    public GiftVoucherListSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<GiftVoucherList>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GiftVoucherList>> GetIncomingNewDataAsync(GiftVoucherList localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GiftVoucherList>> GetIncomingPatchedDataAsync(GiftVoucherList localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GiftVoucherList>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GiftVoucherList>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GiftVoucherList>> GetOutgoingNewDataAsync(GiftVoucherList externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GiftVoucherList>> GetOutgoingPatchedDataAsync(GiftVoucherList externalData)
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

    public Task<NonQueryResult> PullNewAsync(GiftVoucherList newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(GiftVoucherList patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(GiftVoucherList localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(GiftVoucherList localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<GiftVoucherList>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<GiftVoucherList>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<GiftVoucherList> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<GiftVoucherList> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<GiftVoucherList> localData)
    {
        return await PushListAsync(localData);
    }
}