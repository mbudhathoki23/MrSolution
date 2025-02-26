using Dapper;
using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseReturn;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class PurchaseReturnSyncRepository : DbSyncRepositoryBase,
    IDataSyncRepository<PurchaseReturnDataSync>
{
    public PurchaseReturnSyncRepository(DbSyncRepoInjectData injectData)
        : base(injectData)
    {
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetExternalDataAsync()
    {
        return await base.GetExternalDataAsync<PurchaseReturnDataSync>(SyncRepoType.PurchaseReturn);
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetLocalDataAsync()
    {
        var result = new InfoResult<PurchaseReturnDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var masters = await conn.QueryAsync<PR_Master>("SELECT * FROM AMS.PR_Master");
            var details = await conn.QueryAsync<PR_Details>("SELECT * FROM AMS.PR_Details");
            var terms = await conn.QueryAsync<PR_Term>("SELECT * FROM AMS.PR_Term");

            result.Model = new PurchaseReturnDataSync
            {
                Masters = masters.AsList(),
                Details = details.AsList(),
                Terms = terms.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseReturnDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetLocalOriginDataAsync()
    {
        var result = new InfoResult<PurchaseReturnDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var masters = await conn.QueryAsync<PR_Master>(
                $@"SELECT * FROM AMS.PR_Master WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var details = await conn.QueryAsync<PR_Details>(
                $@"SELECT * FROM AMS.PR_Details WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var terms = await conn.QueryAsync<PR_Term>(
                $@"SELECT * FROM AMS.PR_Term WHERE SyncOriginId = '{InjectData.LocalOriginId}'");

            result.Model = new PurchaseReturnDataSync()
            {
                Masters = masters.AsList(),
                Details = details.AsList(),
                Terms = terms.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseReturnDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetIncomingNewDataAsync(
        PurchaseReturnDataSync localData)
    {
        var result = new InfoResult<PurchaseReturnDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the external data. " + extResponse.ErrorMessage;
            return result;
        }

        try
        {
            var masters = extResponse.Model.Masters.Where(x =>
                    !localData.Masters.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId)
                    && !localData.Masters.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId))
                .ToList();

            var details = extResponse.Model.Details.Where(x =>
                !localData.Details.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !localData.Details.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            var terms = extResponse.Model.Terms.Where(x =>
                !localData.Terms.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !localData.Terms.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            result.Model = new PurchaseReturnDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseReturnDataSync>(this);
        }

        result.Model = extResponse.Model;
        result.Success = true;
        return result;
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetIncomingPatchedDataAsync(
        PurchaseReturnDataSync localData)
    {
        var result = new InfoResult<PurchaseReturnDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the external data. " + extResponse.ErrorMessage;
            return result;
        }

        try
        {
            var localMasterIds = localData.Masters.Select(e => e.SyncGlobalId).AsList();
            var masters = extResponse.Model.Masters.Where(x =>
                    localMasterIds.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > localData.Masters.First(m => m.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var detailIds = localData.Details.Select(e => e.SyncGlobalId).AsList();
            var details = extResponse.Model.Details.Where(x =>
                    detailIds.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > localData.Details.First(d => d.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var termIdList = localData.Terms.Select(e => e.SyncGlobalId);
            var terms = extResponse.Model.Terms.Where(x =>
                    termIdList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > localData.Terms.First(t => t.SyncGlobalId == x.SyncGlobalId).SyncRowVersion)
                .ToList();

            result.Model = new PurchaseReturnDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseReturnDataSync>(this);
        }

        result.Model = extResponse.Model;
        result.Success = true;
        return result;
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetOutgoingNewDataAsync(
        PurchaseReturnDataSync externalData)
    {
        var result = new InfoResult<PurchaseReturnDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = "Unable to fetch the local data. " + localDataRes.ErrorMessage;
            return localDataRes;
        }

        try
        {
            var masters = localDataRes.Model.Masters.Where(x =>
                    !externalData.Masters.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                    !externalData.Masters.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId))
                .ToList();

            var details = localDataRes.Model.Details.Where(x =>
                    !externalData.Details.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                    !externalData.Details.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId))
                .ToList();

            var terms = localDataRes.Model.Terms.Where(x =>
                !externalData.Terms.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !externalData.Terms.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            result.Model = new PurchaseReturnDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseReturnDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<PurchaseReturnDataSync>> GetOutgoingPatchedDataAsync(PurchaseReturnDataSync externalData)
    {
        var result = new InfoResult<PurchaseReturnDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = "Unable to fetch the local data. " + localDataRes.ErrorMessage;
            return localDataRes;
        }

        try
        {
            var mastersList = externalData.Masters.Select(e => e.SyncGlobalId).AsList();
            var masters = localDataRes.Model.Masters.Where(x =>
                    mastersList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > externalData.Masters.First(m => m.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var detailList = externalData.Details.Select(e => e.SyncGlobalId).AsList();
            var details = localDataRes.Model.Details.Where(x =>
                    detailList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > externalData.Details.First(d => d.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var termList = externalData.Terms.Select(e => e.SyncGlobalId).AsList();
            var terms = localDataRes.Model.Terms.Where(x =>
                    termList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > externalData.Terms.First(d => d.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            result.Model = new PurchaseReturnDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseReturnDataSync>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> PullNewAsync(PurchaseReturnDataSync newData)
    {
        var result = new NonQueryResult();

        var localDataResponse = await GetLocalOriginDataAsync();
        if (!localDataResponse.Success)
        {
            result.ErrorMessage = "Error fetching local data. " + localDataResponse.ErrorMessage;
            result.ResultType = localDataResponse.ResultType;
            return result;
        }

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                foreach (var master in newData.Masters)
                {
                    master.SyncCreatedOn = InjectData.DateTime;
                    await conn.InsertAsync(master, trans, InjectData.SqlTimeOutSeconds);
                }

                foreach (var detail in newData.Details)
                {
                    detail.SyncCreatedOn = detail.SyncCreatedOn;
                    await conn.InsertAsync(detail, trans, InjectData.SqlTimeOutSeconds);
                }

                foreach (var term in newData.Terms)
                {
                    term.SyncCreatedOn = term.SyncCreatedOn;
                    await conn.InsertAsync(term, trans, InjectData.SqlTimeOutSeconds);
                }

                trans.Commit();
                result.Completed = result.Value = true;
            }
            catch (Exception e)
            {
                result = e.ToNonQueryErrorResult(e.StackTrace);
            }
            finally
            {
                trans.Dispose();
                conn.Dispose();
            }
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }

    public Task<NonQueryResult> PullPatchedAsync(PurchaseReturnDataSync patchedData)
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewAsync(PurchaseReturnDataSync localData)
    {
        return await PushAsync(SyncRepoType.PurchaseReturn, localData);
    }

    public async Task<NonQueryResult> PutNewAsync(PurchaseReturnDataSync localData)
    {
        return await PatchAsync(SyncRepoType.PurchaseReturn, localData);
    }

    public async Task<NonQueryResult> PullAllNewAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch the purchase return data from local datasource. " +
                                  localResponse.ErrorMessage;
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingNewDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch incoming purchase return data from external datasource. " +
                                  incResponse.ErrorMessage;
            result.ResultType = incResponse.ResultType;
            return result;
        }

        return await PullNewAsync(incResponse.Model);
    }

    public async Task<NonQueryResult> PullAllPatchedAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch the purchase return data from local datasource. " +
                                  localResponse.ErrorMessage;
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingPatchedDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch incoming purchase return data from external datasource. " +
                                  incResponse.ErrorMessage;
            result.ResultType = incResponse.ResultType;
            return result;
        }

        return await PullPatchedAsync(incResponse.Model);
    }

    public async Task<NonQueryResult> PushAllNewAsync()
    {
        var result = new NonQueryResult();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the purchase return data from external datasource. " +
                                  extResponse.ErrorMessage;
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingNewDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage = "Error preparing the purchase return data to push to external data provider. " +
                                  outResponse.ErrorMessage;
            result.ResultType = outResponse.ResultType;
            return result;
        }

        return await PushNewAsync(outResponse.Model);
    }

    public async Task<NonQueryResult> PushAllPatchedAsync()
    {
        var result = new NonQueryResult();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the purchase return data from external datasource. " +
                                  extResponse.ErrorMessage;
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingPatchedDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage = "Error preparing the purchase return data to push to external data provider. " +
                                  outResponse.ErrorMessage;
            result.ResultType = outResponse.ResultType;
            return result;
        }

        return await PutNewAsync(outResponse.Model);
    }

    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ListResult<PurchaseReturnDataSync>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<PurchaseReturnDataSync> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<PurchaseReturnDataSync> localData)
    {
        return await PutListAsync(localData);
    }
}