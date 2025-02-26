using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class GeneralLedgerSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<GeneralLedger>
{
    public GeneralLedgerSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<GeneralLedger>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GeneralLedger>> GetIncomingNewDataAsync(GeneralLedger localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GeneralLedger>> GetIncomingPatchedDataAsync(GeneralLedger localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GeneralLedger>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GeneralLedger>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GeneralLedger>> GetOutgoingNewDataAsync(GeneralLedger externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<GeneralLedger>> GetOutgoingPatchedDataAsync(GeneralLedger externalData)
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

    public Task<NonQueryResult> PullNewAsync(GeneralLedger newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(GeneralLedger patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(GeneralLedger localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(GeneralLedger localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<GeneralLedger>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<GeneralLedger>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<GeneralLedger> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<GeneralLedger> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<GeneralLedger> localData)
    {
        return await PushListAsync(localData);
    }
}