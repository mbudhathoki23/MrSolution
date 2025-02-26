using Dapper;
using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
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

public class SalesSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<SalesInvoiceDataSync>
{
    public SalesSyncRepository(DbSyncRepoInjectData injectData)
        : base(injectData)
    {
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetExternalDataAsync()
    {
        return await base.GetExternalDataAsync<SalesInvoiceDataSync>(SyncRepoType.SalesInvoice);
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetLocalDataAsync()
    {
        var result = new InfoResult<SalesInvoiceDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var masters = await conn.QueryAsync<SB_Master>("SELECT * FROM AMS.SB_Master");
            var details = await conn.QueryAsync<SB_Details>("SELECT * FROM AMS.SB_Details");
            var terms = await conn.QueryAsync<SB_Term>("SELECT * FROM AMS.SB_Term");

            result.Model = new SalesInvoiceDataSync
            {
                Masters = masters.AsList(),
                Details = details.AsList(),
                Terms = terms.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetLocalOriginDataAsync()
    {
        var result = new InfoResult<SalesInvoiceDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var masters = await conn.QueryAsync<SB_Master>(
                $@"SELECT * FROM AMS.SB_Master WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var details = await conn.QueryAsync<SB_Details>(
                $@"SELECT * FROM AMS.SB_Details WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var terms = await conn.QueryAsync<SB_Term>(
                $@"SELECT * FROM AMS.SB_Term WHERE SyncOriginId = '{InjectData.LocalOriginId}'");

            result.Model = new SalesInvoiceDataSync
            {
                Masters = masters.AsList(),
                Details = details.AsList(),
                Terms = terms.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetIncomingNewDataAsync(SalesInvoiceDataSync localInvoiceDataSync)
    {
        var result = new InfoResult<SalesInvoiceDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = $"Error fetching the external data. {extResponse.ErrorMessage}";
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

            result.Model = new SalesInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceDataSync>(this);
        }

        result.Model = extResponse.Model;
        result.Success = true;
        return result;
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetIncomingPatchedDataAsync(SalesInvoiceDataSync localInvoiceDataSync)
    {
        var result = new InfoResult<SalesInvoiceDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = $"Error fetching the external data. {extResponse.ErrorMessage}";
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

            result.Model = new SalesInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceDataSync>(this);
        }

        result.Model = extResponse.Model;
        result.Success = true;
        return result;
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetOutgoingNewDataAsync(SalesInvoiceDataSync externalInvoiceDataSync)
    {
        var result = new InfoResult<SalesInvoiceDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = $"Unable to fetch the local data. {localDataRes.ErrorMessage}";
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

            result.Model = new SalesInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<SalesInvoiceDataSync>> GetOutgoingPatchedDataAsync(SalesInvoiceDataSync externalInvoiceDataSync)
    {
        var result = new InfoResult<SalesInvoiceDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = $"Unable to fetch the local data. {localDataRes.ErrorMessage}";
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

            result.Model = new SalesInvoiceDataSync
            {
                Masters = masters,
                Details = details,
                Terms = terms
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<SalesInvoiceDataSync>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> PullNewAsync(SalesInvoiceDataSync newInvoiceDataSync)
    {
        var result = new NonQueryResult();

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                // insert the sales master
                foreach (var master in newInvoiceDataSync.Masters)
                {
                    master.SyncCreatedOn = InjectData.DateTime;
                    await conn.InsertAsync(master, trans, InjectData.SqlTimeOutSeconds);
                }

                // insert the sales details
                foreach (var detail in newInvoiceDataSync.Details)
                {
                    detail.SyncCreatedOn = InjectData.DateTime;
                    await conn.InsertAsync(detail, trans, InjectData.SqlTimeOutSeconds);
                }

                // insert the sales terms
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

    public Task<NonQueryResult> PullPatchedAsync(SalesInvoiceDataSync patchedInvoiceDataSync)
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewAsync(SalesInvoiceDataSync localInvoiceDataSync)
    {
        return await PushAsync(SyncRepoType.SalesInvoice, localInvoiceDataSync);
    }

    public async Task<NonQueryResult> PutNewAsync(SalesInvoiceDataSync localInvoiceDataSync)
    {
        return await PatchAsync(SyncRepoType.SalesInvoice, localInvoiceDataSync);
    }

    public async Task<NonQueryResult> PullAllNewAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage = $"Unable to fetch the sales data from local datasource. {localResponse.ErrorMessage}";
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingNewDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage =
                $"Unable to fetch incoming sales data from external datasource. {incResponse.ErrorMessage}";
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
            result.ErrorMessage = $"Unable to fetch the sales data from local datasource. {localResponse.ErrorMessage}";
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingPatchedDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage =
                $"Unable to fetch incoming sales data from external datasource. {incResponse.ErrorMessage}";
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
            result.ErrorMessage = $"Error fetching the sales data from external datasource. {extResponse.ErrorMessage}";
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingNewDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage =
                $"Error preparing the sales data to push to external data provider. {outResponse.ErrorMessage}";
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
            result.ErrorMessage = $"Error fetching the sales data from external datasource. {extResponse.ErrorMessage}";
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingPatchedDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage =
                $"Error preparing the sales data to push to external data provider. {outResponse.ErrorMessage}";
            result.ResultType = outResponse.ResultType;
            return result;
        }

        return await PutNewAsync(outResponse.Model);
    }

    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ListResult<SalesInvoiceDataSync>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<SalesInvoiceDataSync> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<SalesInvoiceDataSync> localData)
    {
        return await PutListAsync(localData);
    }
}