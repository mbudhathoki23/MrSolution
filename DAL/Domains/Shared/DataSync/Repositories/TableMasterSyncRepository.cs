using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class TableMasterSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<TableMaster>
{
    public TableMasterSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<TableMaster>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<TableMaster>> GetIncomingNewDataAsync(TableMaster localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<TableMaster>> GetIncomingPatchedDataAsync(TableMaster localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<TableMaster>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<TableMaster>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<TableMaster>> GetOutgoingNewDataAsync(TableMaster externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<TableMaster>> GetOutgoingPatchedDataAsync(TableMaster externalData)
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

    public Task<NonQueryResult> PullNewAsync(TableMaster newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(TableMaster patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(TableMaster localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(TableMaster localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<TableMaster>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<TableMaster>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<TableMaster> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<TableMaster> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<TableMaster> localData)
    {
        return await PushListAsync(localData);
    }
}