using Dapper;
using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
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

public class PurchaseSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<PurchaseInvoiceDataSync>
{
    public PurchaseSyncRepository(DbSyncRepoInjectData injectData)
        : base(injectData)
    {
    }

    public Task<InfoResult<PurchaseInvoiceDataSync>> GetExternalDataAsync()
    {
        //return base.GetExternalDataAsync<PurchaseInvoiceDataSync>(SyncRepoType.Purchase);
        return null;
    }

    public async Task<InfoResult<PurchaseInvoiceDataSync>> GetLocalDataAsync()
    {
        var result = new InfoResult<PurchaseInvoiceDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var masters = await conn.QueryAsync<PB_Master>("SELECT * FROM AMS.PB_Master");
            var details = await conn.QueryAsync<PB_Details>("SELECT * FROM AMS.PB_Details");
            var terms = await conn.QueryAsync<PB_Term>("SELECT * FROM AMS.PB_Term");

            result.Model = new PurchaseInvoiceDataSync
            {
                Masters = masters.AsList(),
                Details = details.AsList(),
                Terms = terms.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseInvoiceDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<PurchaseInvoiceDataSync>> GetLocalOriginDataAsync()
    {
        var result = new InfoResult<PurchaseInvoiceDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var masters = await conn.QueryAsync<PB_Master>(
                $@"SELECT * FROM AMS.PB_Master WHERE SyncOriginId = '{InjectData.LocalOriginId}'", new
                {
                    myBranchId = InjectData.LocalOriginId
                });
            var details =
                await conn.QueryAsync<PB_Details>(
                    $@"SELECT * FROM AMS.PB_Details WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var terms = await conn.QueryAsync<PB_Term>(
                $@"SELECT * FROM AMS.PB_Term WHERE SyncOriginId = '{InjectData.LocalOriginId}'");

            result.Model = new PurchaseInvoiceDataSync
            {
                Masters = masters.AsList(),
                Details = details.AsList(),
                Terms = terms.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseInvoiceDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<PurchaseInvoiceDataSync>> GetIncomingNewDataAsync(PurchaseInvoiceDataSync localInvoiceDataSync)
    {
        var result = new InfoResult<PurchaseInvoiceDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the external data. " + extResponse.ErrorMessage;
            return result;
        }

        try
        {
            var masters = extResponse.Model.Masters.Where(x =>
                !localInvoiceDataSync.Masters.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !localInvoiceDataSync.Masters.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            var details = extResponse.Model.Details.Where(x =>
                !localInvoiceDataSync.Details.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !localInvoiceDataSync.Details.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            var terms = extResponse.Model.Terms.Where(x =>
                !localInvoiceDataSync.Terms.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !localInvoiceDataSync.Terms.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            result.Model = new PurchaseInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseInvoiceDataSync>(this);
        }

        result.Model = extResponse.Model;
        result.Success = true;
        return result;
    }

    public async Task<InfoResult<PurchaseInvoiceDataSync>> GetIncomingPatchedDataAsync(PurchaseInvoiceDataSync localInvoiceDataSync)
    {
        var result = new InfoResult<PurchaseInvoiceDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the external data. " + extResponse.ErrorMessage;
            return result;
        }

        try
        {
            var localMasterIds = localInvoiceDataSync.Masters.Select(e => e.SyncGlobalId).AsList();
            var masters = extResponse.Model.Masters.Where(x =>
                    localMasterIds.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > localInvoiceDataSync.Masters.First(m => m.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var detailIds = localInvoiceDataSync.Details.Select(e => e.SyncGlobalId).AsList();
            var details = extResponse.Model.Details.Where(x =>
                    detailIds.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > localInvoiceDataSync.Details.First(d => d.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var termIdList = localInvoiceDataSync.Terms.Select(e => e.SyncGlobalId);
            var terms = extResponse.Model.Terms.Where(x =>
                    termIdList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > localInvoiceDataSync.Terms.First(t => t.SyncGlobalId == x.SyncGlobalId).SyncRowVersion)
                .ToList();

            result.Model = new PurchaseInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseInvoiceDataSync>(this);
        }

        result.Model = extResponse.Model;
        result.Success = true;
        return result;
    }

    public async Task<InfoResult<PurchaseInvoiceDataSync>> GetOutgoingNewDataAsync(PurchaseInvoiceDataSync externalInvoiceDataSync)
    {
        var result = new InfoResult<PurchaseInvoiceDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = "Unable to fetch the local data. " + localDataRes.ErrorMessage;
            return localDataRes;
        }

        try
        {
            var masters = localDataRes.Model.Masters.Where(x =>
                    !externalInvoiceDataSync.Masters.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                    !externalInvoiceDataSync.Masters.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId))
                .ToList();

            var details = localDataRes.Model.Details.Where(x =>
                    !externalInvoiceDataSync.Details.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                    !externalInvoiceDataSync.Details.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId))
                .ToList();

            var terms = localDataRes.Model.Terms.Where(x =>
                !externalInvoiceDataSync.Terms.Select(e => e.SyncGlobalId).Contains(x.SyncGlobalId) &&
                !externalInvoiceDataSync.Terms.Select(e => e.SyncOriginId.ToString()).Contains(InjectData.LocalOriginId)).ToList();

            result.Model = new PurchaseInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseInvoiceDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<PurchaseInvoiceDataSync>> GetOutgoingPatchedDataAsync(PurchaseInvoiceDataSync externalInvoiceDataSync)
    {
        var result = new InfoResult<PurchaseInvoiceDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = "Unable to fetch the local data. " + localDataRes.ErrorMessage;
            return localDataRes;
        }

        try
        {
            var mastersList = externalInvoiceDataSync.Masters.Select(e => e.SyncGlobalId).AsList();
            var masters = localDataRes.Model.Masters.Where(x =>
                    mastersList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > externalInvoiceDataSync.Masters.First(m => m.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var detailList = externalInvoiceDataSync.Details.Select(e => e.SyncGlobalId).AsList();
            var details = localDataRes.Model.Details.Where(x =>
                    detailList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > externalInvoiceDataSync.Details.First(d => d.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            var termList = externalInvoiceDataSync.Terms.Select(e => e.SyncGlobalId).AsList();
            var terms = localDataRes.Model.Terms.Where(x =>
                    termList.Contains(x.SyncGlobalId) &&
                    x.SyncRowVersion > externalInvoiceDataSync.Terms.First(d => d.SyncGlobalId == x.SyncGlobalId)
                        .SyncRowVersion)
                .ToList();

            result.Model = new PurchaseInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<PurchaseInvoiceDataSync>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> PullNewAsync(PurchaseInvoiceDataSync newInvoiceDataSync)
    {
        var result = new NonQueryResult();

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                foreach (var master in newInvoiceDataSync.Masters)
                {
                    master.SyncCreatedOn = InjectData.DateTime;
                    await conn.InsertAsync(master, trans, InjectData.SqlTimeOutSeconds);
                }

                foreach (var detail in newInvoiceDataSync.Details)
                {
                    detail.SyncCreatedOn = InjectData.DateTime;
                    await conn.InsertAsync(detail, trans, InjectData.SqlTimeOutSeconds);
                }

                foreach (var term in newInvoiceDataSync.Terms)
                {
                    term.SyncCreatedOn = InjectData.DateTime;
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

    public Task<NonQueryResult> PullPatchedAsync(PurchaseInvoiceDataSync patchedInvoiceDataSync)
    {
        throw new NotImplementedException();
    }

    public Task<NonQueryResult> PushNewAsync(PurchaseInvoiceDataSync localInvoiceDataSync)
    {
        return PushAsync(SyncRepoType.PurchaseInvoice, localInvoiceDataSync);
    }

    public Task<NonQueryResult> PutNewAsync(PurchaseInvoiceDataSync localInvoiceDataSync)
    {
        return PatchAsync(SyncRepoType.PurchaseInvoice, localInvoiceDataSync);
    }

    public async Task<NonQueryResult> PullAllNewAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch the purchase data from local datasource. " +
                                  localResponse.ErrorMessage;
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingNewDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch incoming purchase data from external datasource. " +
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
            result.ErrorMessage = "Unable to fetch the purchase data from local datasource. " +
                                  localResponse.ErrorMessage;
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingPatchedDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch incoming purchase data from external datasource. " +
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
            result.ErrorMessage = "Error fetching the purchase data from external datasource. " +
                                  extResponse.ErrorMessage;
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingNewDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage = "Error preparing the purchase data to push to external data provider. " +
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
            result.ErrorMessage = "Error fetching the purchase data from external datasource. " +
                                  extResponse.ErrorMessage;
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingPatchedDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage = "Error preparing the purchase data to push to external data provider. " +
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

    public Task<ListResult<PurchaseInvoiceDataSync>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<PurchaseInvoiceDataSync> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<PurchaseInvoiceDataSync> localData)
    {
        return await PutListAsync(localData);
    }
}