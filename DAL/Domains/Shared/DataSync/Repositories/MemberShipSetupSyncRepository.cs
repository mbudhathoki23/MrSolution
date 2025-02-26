using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class MemberShipSetupSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<MemberShipSetup>
{
    public MemberShipSetupSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public Task<InfoResult<MemberShipSetup>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberShipSetup>> GetIncomingNewDataAsync(MemberShipSetup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberShipSetup>> GetIncomingPatchedDataAsync(MemberShipSetup localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberShipSetup>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberShipSetup>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberShipSetup>> GetOutgoingNewDataAsync(MemberShipSetup externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<MemberShipSetup>> GetOutgoingPatchedDataAsync(MemberShipSetup externalData)
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

    public Task<NonQueryResult> PullNewAsync(MemberShipSetup newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(MemberShipSetup patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(MemberShipSetup localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(MemberShipSetup localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<MemberShipSetup>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<MemberShipSetup>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<MemberShipSetup> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<MemberShipSetup> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<MemberShipSetup> localData)
    {
        return await PushListAsync(localData);
    }
}