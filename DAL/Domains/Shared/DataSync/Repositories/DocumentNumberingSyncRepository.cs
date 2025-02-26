using DatabaseModule.CloudSync;
using DatabaseModule.Setup.DocumentNumberings;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class DocumentNumberingSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<DocumentNumbering>
{
    public DocumentNumberingSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {

    }

    public Task<InfoResult<DocumentNumbering>> GetExternalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DocumentNumbering>> GetIncomingNewDataAsync(DocumentNumbering localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DocumentNumbering>> GetIncomingPatchedDataAsync(DocumentNumbering localData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DocumentNumbering>> GetLocalDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DocumentNumbering>> GetLocalOriginDataAsync()
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DocumentNumbering>> GetOutgoingNewDataAsync(DocumentNumbering externalData)
    {
        throw new NotImplementedException();
    }

    public Task<InfoResult<DocumentNumbering>> GetOutgoingPatchedDataAsync(DocumentNumbering externalData)
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

    public Task<NonQueryResult> PullNewAsync(DocumentNumbering newData)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PullPatchedAsync(DocumentNumbering patchedData)
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

    public async Task<NonQueryResult> PushNewAsync(DocumentNumbering localData)
    {
        return await PushAsync(localData);
    }

    public async Task<NonQueryResult> PutNewAsync(DocumentNumbering localData)
    {
        return await PutAsync(localData);
    }

    public async Task<NonQueryResult> DeleteNewAsync()
    {
        return await DeleteAsync();
    }

    public async Task<ListResult<DocumentNumbering>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<DocumentNumbering>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<DocumentNumbering> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<DocumentNumbering> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewlistAsync(List<DocumentNumbering> localData)
    {
        return await PushListAsync(localData);
    }
}