using Dapper;
using DatabaseModule.CloudSync;
using DatabaseModule.Master.LedgerSetup;
using MoreLinq;
using MrDAL.Core.Extensions;
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

public class AccountLedgerSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<AccountLedgerDataSync>
{
    private AccountLedgerDataSync _extLedgerDataSync;

    public AccountLedgerSyncRepository(DbSyncRepoInjectData injectData) : base(injectData)
    {
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetExternalDataAsync()
    {
        var response = await base.GetExternalDataAsync<AccountLedgerDataSync>(SyncRepoType.GeneralLedger);
        _extLedgerDataSync = response.Model;
        return response;
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetLocalDataAsync()
    {
        var result = new InfoResult<AccountLedgerDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var accountGroups = await conn.GetAllAsync<AccountGroup>();
            var generalLedgers = await conn.GetAllAsync<GeneralLedger>();
            var subGroups = await conn.GetAllAsync<AccountSubGroup>();
            var agents = await conn.GetAllAsync<JuniorAgent>();
            var currencies = await conn.GetAllAsync<Currency>();

            result.Model = new AccountLedgerDataSync
            {
                AccountGroups = accountGroups.AsList(),
                GeneralLedgers = generalLedgers.AsList(),
                AccountSubGroups = subGroups.AsList(),
                Agents = agents.AsList(),
                Currencies = currencies.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AccountLedgerDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetLocalOriginDataAsync()
    {
        var result = new InfoResult<AccountLedgerDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var accountGroups =
                await conn.QueryAsync<AccountGroup>(
                    $@"SELECT * FROM AMS.AccountGroup WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var generalLedgers = await conn.QueryAsync<GeneralLedger>(
                $@"SELECT * FROM AMS.GeneralLedger WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var subGroups = await conn.QueryAsync<AccountSubGroup>(
                $@"SELECT * FROM AMS.AccountSubGroup WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var agents =
                await conn.QueryAsync<JuniorAgent>(
                    $@"SELECT * FROM AMS.JuniorAgent WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var currencies =
                await conn.QueryAsync<Currency>(
                    $@"SELECT * FROM AMS.Currency WHERE SyncOriginId = '{InjectData.LocalOriginId}' ");

            result.Model = new AccountLedgerDataSync
            {
                AccountGroups = accountGroups.AsList(),
                GeneralLedgers = generalLedgers.AsList(),
                Currencies = currencies.AsList(),
                Agents = agents.AsList(),
                AccountSubGroups = subGroups.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AccountLedgerDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetIncomingNewDataAsync(
        AccountLedgerDataSync localDataSync)
    {
        var result = new InfoResult<AccountLedgerDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = "Error fetching the external data. " + extResponse.ErrorMessage;
            return result;
        }

        var data = new AccountLedgerDataSync
        {
            AccountGroups = new List<AccountGroup>(),
            AccountSubGroups = new List<AccountSubGroup>(),
            Agents = new List<JuniorAgent>(),
            Currencies = new List<Currency>(),
            GeneralLedgers = new List<GeneralLedger>()
        };
        var extData = extResponse.Model;

        try
        {
            extData.AccountGroups.DistinctBy(x => x.SyncBaseId).ForEach(e =>
            {
                var latest = extData.AccountGroups.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.AccountGroups.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.AccountGroups.Add(latest);
            });

            extData.AccountSubGroups.DistinctBy(x => x.SyncBaseId).ForEach(e =>
            {
                var latest = extData.AccountSubGroups.Where(x => x.SyncBaseId == e.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).First();
                var lRow = localDataSync.AccountSubGroups.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.AccountSubGroups.Add(latest);
            });

            extData.Agents.DistinctBy(x => x.SyncBaseId).ForEach(e =>
            {
                var latest = extData.Agents.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.Agents.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.Agents.Add(latest);
            });

            extData.Currencies.DistinctBy(x => x.SyncBaseId).ForEach(e =>
            {
                var latest = extData.Currencies.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.Currencies.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.Currencies.Add(latest);
            });

            extData.GeneralLedgers.DistinctBy(x => x.SyncBaseId).ForEach(e =>
            {
                var latest = extData.GeneralLedgers.Where(x => x.SyncBaseId == e.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).First();
                var lRow = localDataSync.GeneralLedgers.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.GeneralLedgers.Add(latest);
            });

            result.Success = true;
            result.Model = data;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AccountLedgerDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetIncomingPatchedDataAsync(AccountLedgerDataSync localDataSync)
    {
        var result = new InfoResult<AccountLedgerDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = $"Error fetching the external data. {extResponse.ErrorMessage}";
            return result;
        }

        var data = new AccountLedgerDataSync
        {
            AccountGroups = new List<AccountGroup>(),
            AccountSubGroups = new List<AccountSubGroup>(),
            Agents = new List<JuniorAgent>(),
            Currencies = new List<Currency>(),
            GeneralLedgers = new List<GeneralLedger>()
        };
        var extData = extResponse.Model;

        try
        {
            localDataSync.AccountGroups.ForEach(l =>
            {
                var eRow = extData.AccountGroups.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && eRow.SyncRowVersion > l.SyncRowVersion)
                    data.AccountGroups.Add(eRow);
            });

            localDataSync.AccountSubGroups.ForEach(l =>
            {
                var eRow = extData.AccountSubGroups.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && eRow.SyncRowVersion > l.SyncRowVersion)
                    data.AccountSubGroups.Add(eRow);
            });

            localDataSync.Currencies.ForEach(l =>
            {
                var eRow = extData.Currencies.Where(x => x.SyncBaseId == l.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .FirstOrDefault();
                if (eRow != null && eRow.SyncRowVersion > l.SyncRowVersion)
                    data.Currencies.Add(eRow);
            });

            localDataSync.Agents.ForEach(l =>
            {
                var eRow = extData.Agents.Where(x => x.SyncBaseId == l.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .FirstOrDefault();
                if (eRow != null && eRow.SyncRowVersion > l.SyncRowVersion)
                    data.Agents.Add(eRow);
            });

            localDataSync.GeneralLedgers.ForEach(l =>
            {
                var eRow = extData.GeneralLedgers.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && eRow.SyncRowVersion > l.SyncRowVersion)
                    data.GeneralLedgers.Add(eRow);
            });

            result.Success = true;
            result.Model = data;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AccountLedgerDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetOutgoingNewDataAsync(AccountLedgerDataSync externalDataSync)
    {
        var result = new InfoResult<AccountLedgerDataSync>();
        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = $"Unable to fetch the local data. {localDataRes.ErrorMessage}";
            return localDataRes;
        }

        var data = new AccountLedgerDataSync
        {
            AccountGroups = new List<AccountGroup>(),
            AccountSubGroups = new List<AccountSubGroup>(),
            Agents = new List<JuniorAgent>(),
            Currencies = new List<Currency>(),
            GeneralLedgers = new List<GeneralLedger>()
        };
        var localData = localDataRes.Model;

        try
        {
            localData.AccountGroups.ForEach(l =>
            {
                var eRow = externalDataSync.AccountGroups.FirstOrDefault(e => e.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.AccountGroups.Add(l);
            });

            localData.AccountSubGroups.ForEach(l =>
            {
                var eRow = externalDataSync.AccountSubGroups.FirstOrDefault(e => e.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.AccountSubGroups.Add(l);
            });

            localData.Agents.ForEach(l =>
            {
                var eRow = externalDataSync.Agents.FirstOrDefault(e => e.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.Agents.Add(l);
            });

            localData.Currencies.ForEach(l =>
            {
                var eRow = externalDataSync.Currencies.FirstOrDefault(e => e.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.Currencies.Add(l);
            });

            localData.GeneralLedgers.ForEach(l =>
            {
                var lRow = externalDataSync.GeneralLedgers.FirstOrDefault(e => e.SyncBaseId == l.SyncBaseId);
                if (lRow == null) data.GeneralLedgers.Add(l);
            });

            result.Model = data;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AccountLedgerDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<AccountLedgerDataSync>> GetOutgoingPatchedDataAsync(AccountLedgerDataSync externalDataSync)
    {
        var result = new InfoResult<AccountLedgerDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = $"Unable to fetch the local data. {localDataRes.ErrorMessage}";
            return localDataRes;
        }

        var data = new AccountLedgerDataSync
        {
            AccountGroups = new List<AccountGroup>(),
            AccountSubGroups = new List<AccountSubGroup>(),
            Agents = new List<JuniorAgent>(),
            Currencies = new List<Currency>(),
            GeneralLedgers = new List<GeneralLedger>()
        };
        var localData = localDataRes.Model;

        try
        {
            localData.AccountGroups.ForEach(l =>
            {
                var eRow = externalDataSync.AccountGroups.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.AccountGroups.Add(l);
            });

            localData.AccountSubGroups.ForEach(l =>
            {
                var eRow = externalDataSync.AccountSubGroups.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.AccountSubGroups.Add(l);
            });

            localData.Currencies.ForEach(l =>
            {
                var eRow = externalDataSync.Currencies
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.Currencies.Add(l);
            });

            localData.Agents.ForEach(l =>
            {
                var eRow = externalDataSync.Agents
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.Agents.Add(l);
            });

            localData.GeneralLedgers.ForEach(l =>
            {
                var eRow = externalDataSync.GeneralLedgers
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.GeneralLedgers.Add(l);
            });

            result.Success = true;
            result.Model = data;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<AccountLedgerDataSync>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> PullNewAsync(AccountLedgerDataSync newDataSync)
    {
        var result = new NonQueryResult();

        var localDataResponse = await GetLocalOriginDataAsync();
        if (!localDataResponse.Success)
        {
            result.ErrorMessage = "Error fetching local data. " + localDataResponse.ErrorMessage;
            result.ResultType = localDataResponse.ResultType;
            return result;
        }

        var extDataResponse = await GetExternalDataAsync();
        if (!extDataResponse.Success)
        {
            result.ErrorMessage = "Error fetching the ledger data from external source. " +
                                  extDataResponse.ErrorMessage;
            result.ResultType = extDataResponse.ResultType;
            return result;
        }

        var localModel = localDataResponse.Model;
        var extModel = extDataResponse.Model;

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                // save account groups
                foreach (var eGroup in newDataSync.AccountGroups)
                {
                    var newId = await conn.NewIntegerIdAsync("AMS.AccountGroup", "GrpId", trans);
                    if (newId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new AccountGroup.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var nGroup = eGroup.DeepCopy();
                    nGroup.GrpId = newId;
                    nGroup.Company_Id = eGroup.Company_Id.HasValue ? InjectData.LocalCompanyUnitId : null;
                    nGroup.EnterDate = InjectData.DateTime;
                    nGroup.EnterBy = InjectData.Username;
                    //nGroup.SyncOriginId = InjectData.LocalOriginId;
                    nGroup.SyncGlobalId = Guid.NewGuid();
                    nGroup.SyncCreatedOn = InjectData.DateTime;

                    await conn.InsertAsync(nGroup, trans, InjectData.SqlTimeOutSeconds);
                    localModel.AccountGroups.Add(nGroup);
                }

                // save the currency details
                foreach (var eCurrency in newDataSync.Currencies)
                {
                    var newId = await conn.NewIntegerIdAsync("AMS.Currency", "CId", trans);
                    if (newId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new Currency.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var nCurrency = eCurrency.DeepCopy();
                    nCurrency.CId = newId;
                    nCurrency.Company_Id = eCurrency.Company_Id.HasValue ? InjectData.LocalCompanyUnitId : null;
                    nCurrency.EnterDate = InjectData.DateTime;
                    nCurrency.EnterBy = InjectData.Username;
                    //nCurrency.SyncOriginId = InjectData.LocalOriginId;
                    nCurrency.SyncGlobalId = Guid.NewGuid();
                    nCurrency.SyncCreatedOn = InjectData.DateTime;

                    await conn.InsertAsync(nCurrency, trans, InjectData.SqlTimeOutSeconds);
                    localModel.Currencies.Add(nCurrency);
                }

                // save the agents data
                foreach (var eAgent in newDataSync.Agents)
                {
                    var newId = await conn.NewIntegerIdAsync("AMS.JuniorAgent", "AgentId", trans);
                    if (newId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new JuniorAgent.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var nAgent = eAgent.DeepCopy();
                    nAgent.AgentId = newId;
                    nAgent.Company_Id = eAgent.Company_Id.HasValue ? InjectData.LocalCompanyUnitId : null;
                    nAgent.EnterDate = InjectData.DateTime;
                    nAgent.EnterBy = InjectData.Username;
                    //nAgent.SyncOriginId = InjectData.LocalOriginId;
                    nAgent.SyncGlobalId = Guid.NewGuid();
                    nAgent.SyncCreatedOn = InjectData.DateTime;

                    await conn.InsertAsync(nAgent, trans, InjectData.SqlTimeOutSeconds);
                    localModel.Agents.Add(nAgent);
                }

                // save the account sub-group
                foreach (var eSubGroup in newDataSync.AccountSubGroups)
                {
                    var newId = await conn.NewIntegerIdAsync("AMS.AccountSubGroup", "SubGrpId", trans);
                    if (newId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new AccountSubGroup.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var extGroup = extModel.AccountGroups.FirstOrDefault(x =>
                        x.GrpId == eSubGroup.GrpId && x.SyncOriginId == eSubGroup.SyncOriginId);
                    var groupId = extGroup == null
                        ? null
                        : localModel.AccountGroups.FirstOrDefault(x =>
                            x.SyncBaseId == extGroup.SyncBaseId)?.GrpId;
                    //&& x.SyncOriginId == InjectData.LocalOriginId)
                    //

                    var nSubGroup = eSubGroup.DeepCopy();
                    nSubGroup.SubGrpId = newId;
                    nSubGroup.Company_Id = eSubGroup.Company_Id.HasValue ? InjectData.LocalCompanyUnitId : null;
                    nSubGroup.EnterDate = InjectData.DateTime;
                    nSubGroup.EnterBy = InjectData.Username;
                    nSubGroup.GrpId = groupId.GetInt();
                    // nSubGroup.SyncOriginId = InjectData.LocalOriginId;
                    nSubGroup.SyncGlobalId = Guid.NewGuid();
                    nSubGroup.SyncCreatedOn = InjectData.DateTime;

                    await conn.InsertAsync(nSubGroup, trans, InjectData.SqlTimeOutSeconds);
                    localModel.AccountSubGroups.Add(nSubGroup);
                }

                // save the general ledgers
                foreach (var eLedger in newDataSync.GeneralLedgers)
                {
                    var newId = await conn.NewBigIntIdAsync("AMS.GeneralLedger", "GLID", trans);
                    if (newId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new GeneralLedger.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var group = extModel.AccountGroups.FirstOrDefault(x =>
                        x.GrpId == eLedger.GrpId && x.SyncOriginId == eLedger.SyncOriginId);
                    var lGroupId = group == null
                        ? null
                        : localModel.AccountGroups.FirstOrDefault(x =>
                            x.SyncBaseId == group.SyncBaseId)?.GrpId;
                    //&& x.SyncOriginId == InjectData.LocalOriginId)?.GrpId;

                    var subGroup = extModel.AccountSubGroups.FirstOrDefault(x =>
                        x.SubGrpId == eLedger.SubGrpId && x.SyncOriginId == eLedger.SyncOriginId);
                    var lSubGroupId = subGroup == null
                        ? null
                        : localModel.AccountSubGroups.FirstOrDefault(x =>
                            x.SyncBaseId == subGroup.SyncBaseId)?.SubGrpId;
                    // && subGroup.SyncOriginId == InjectData.LocalOriginId)?.SubGrpId;

                    var currency = extModel.Currencies.FirstOrDefault(x =>
                        x.CId == eLedger.CurrId && x.SyncOriginId == eLedger.SyncOriginId);
                    var lCurrencyId = currency == null
                        ? null
                        : localModel.Currencies.FirstOrDefault(x =>
                            x.SyncBaseId == currency.SyncBaseId && x.SyncOriginId == eLedger.SyncOriginId)?.CId;

                    var nLedger = eLedger.DeepCopy();
                    nLedger.GLID = newId;
                    nLedger.GrpId = lGroupId.Value;
                    nLedger.SubGrpId = lSubGroupId;
                    //nLedger.AreaId =,
                    nLedger.CurrId = lCurrencyId;
                    nLedger.Branch_ID = InjectData.LocalBranchId;
                    nLedger.Company_Id = InjectData.LocalCompanyUnitId;
                    nLedger.EnterDate = InjectData.DateTime;
                    nLedger.EnterBy = InjectData.Username;
                    // nLedger.SyncOriginId = InjectData.LocalOriginId;
                    nLedger.SyncGlobalId = Guid.NewGuid();
                    nLedger.SyncCreatedOn = InjectData.DateTime;

                    await conn.InsertAsync(nLedger, trans, InjectData.SqlTimeOutSeconds);
                    localModel.GeneralLedgers.Add(nLedger);
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

    public async Task<NonQueryResult> PullPatchedAsync(AccountLedgerDataSync patchedDataSync)
    {
        var result = new NonQueryResult();

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                // update the account groups
                foreach (var eGroup in patchedDataSync.AccountGroups)
                {
                    var lGroup = await conn.QueryFirstOrDefaultAsync<AccountGroup>(
                        @"SELECT * FROM AMS.AccountGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eGroup.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    if (lGroup != null)
                    {
                        lGroup.GrpName = eGroup.GrpName;
                        lGroup.GrpCode = eGroup.GrpCode;
                        lGroup.Schedule = eGroup.Schedule;
                        lGroup.PrimaryGrp = eGroup.PrimaryGrp;
                        lGroup.GrpType = eGroup.GrpType;
                        lGroup.Status = eGroup.Status;
                        lGroup.EnterBy = InjectData.Username;
                        lGroup.EnterDate = InjectData.DateTime;
                        lGroup.SyncLastPatchedOn = InjectData.DateTime;
                        lGroup.SyncRowVersion = eGroup.SyncRowVersion;

                        await conn.UpdateAsync(lGroup, trans, InjectData.SqlTimeOutSeconds);
                    }
                }

                // update the account sub-groups
                foreach (var eSubGroup in patchedDataSync.AccountSubGroups)
                {
                    var lSubGroup = await conn.QueryFirstOrDefaultAsync<AccountSubGroup>(
                        @"SELECT * FROM AMS.AccountSubGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eSubGroup.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    if (lSubGroup != null)
                    {
                        var eGroup = _extLedgerDataSync.AccountGroups.FirstOrDefault(x =>
                            x.GrpId == eSubGroup.GrpId && x.SyncOriginId == eSubGroup.SyncOriginId);
                        var eGroupId = eGroup == null
                            ? null
                            : await conn.QueryFirstOrDefaultAsync<int?>(
                                @"SELECT * FROM AMS.AccountGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                                new
                                {
                                    baseId = eGroup.SyncBaseId,
                                    originId = InjectData.LocalOriginId
                                }, trans);

                        lSubGroup.SubGrpName = eSubGroup.SubGrpName;
                        lSubGroup.GrpId = eGroupId.GetHashCode();
                        lSubGroup.SubGrpCode = eSubGroup.SubGrpCode;
                        lSubGroup.EnterDate = InjectData.DateTime;
                        lSubGroup.EnterBy = InjectData.Username;
                        lSubGroup.SyncLastPatchedOn = InjectData.DateTime;
                        lSubGroup.SyncRowVersion = eSubGroup.SyncRowVersion;

                        await conn.UpdateAsync(lSubGroup, trans, InjectData.SqlTimeOutSeconds);
                    }
                }

                // patch the junior agents
                foreach (var eAgent in patchedDataSync.Agents)
                {
                    var lAgent = await conn.QueryFirstOrDefaultAsync<JuniorAgent>(
                        @"SELECT * FROM AMS.JuniorAgent WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eAgent.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);
                    if (lAgent != null)
                    {
                        lAgent.AgentName = eAgent.AgentName;
                        lAgent.AgentCode = eAgent.AgentCode;
                        lAgent.Address = eAgent.Address;
                        lAgent.PhoneNo = eAgent.PhoneNo;
                        lAgent.Commission = eAgent.Commission;
                        lAgent.Email = eAgent.Email;
                        lAgent.CrLimit = eAgent.CrLimit;
                        lAgent.CrDays = eAgent.CrDays;
                        lAgent.CrTYpe = eAgent.CrTYpe;
                        lAgent.EnterDate = InjectData.DateTime;
                        lAgent.EnterDate = InjectData.DateTime;
                        lAgent.SyncLastPatchedOn = InjectData.DateTime;
                        lAgent.SyncRowVersion = eAgent.SyncRowVersion;

                        await conn.UpdateAsync(lAgent, trans, InjectData.SqlTimeOutSeconds);
                    }
                }

                // patch the general ledgers
                foreach (var eLedger in patchedDataSync.GeneralLedgers)
                {
                    var lLedger = await conn.QueryFirstOrDefaultAsync<GeneralLedger>(
                        @"SELECT * FROM AMS.GeneralLedger WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eLedger.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    if (lLedger != null)
                    {
                        var eGroup = _extLedgerDataSync.AccountGroups.FirstOrDefault(x =>
                            x.GrpId == eLedger.GrpId && x.SyncOriginId == eLedger.SyncOriginId);
                        var lGroupId = eGroup == null
                            ? null
                            : await conn.QueryFirstOrDefaultAsync<int?>(
                                @"SELECT GrpId FROM AMS.GeneralLedger WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                                new
                                {
                                    baseId = eGroup.SyncBaseId,
                                    originId = InjectData.LocalOriginId
                                }, trans);

                        var eSubGroup = _extLedgerDataSync.AccountSubGroups.FirstOrDefault(x =>
                            x.SubGrpId == eLedger.SubGrpId && x.SyncOriginId == eLedger.SyncOriginId);
                        var lSubGroupId = eSubGroup == null
                            ? null
                            : await conn.QueryFirstOrDefaultAsync<int?>(
                                @"SELECT SubGrpId FROM AMS.AccountSubGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                                new
                                {
                                    baseId = eSubGroup.SyncBaseId,
                                    originId = InjectData.LocalOriginId
                                }, trans);

                        lLedger.GLName = eLedger.GLName;
                        lLedger.GLCode = eLedger.GLCode;
                        lLedger.ACCode = eLedger.ACCode;
                        lLedger.GLType = eLedger.GLType;
                        lLedger.PanNo = eLedger.PanNo;
                        lLedger.CrDays = eLedger.CrDays;
                        lLedger.CrLimit = eLedger.CrLimit;
                        lLedger.CrTYpe = eLedger.CrTYpe;
                        lLedger.GLAddress = eLedger.GLAddress;
                        lLedger.PhoneNo = eLedger.PhoneNo;
                        lLedger.LandLineNo = eLedger.LandLineNo;
                        lLedger.OwnerName = eLedger.OwnerName;
                        lLedger.OwnerNumber = eLedger.OwnerNumber;
                        lLedger.Scheme = eLedger.Scheme;
                        lLedger.Email = eLedger.Email;
                        lLedger.EnterDate = InjectData.DateTime;
                        lLedger.EnterBy = InjectData.Username;
                        lLedger.SyncLastPatchedOn = InjectData.DateTime;
                        lLedger.GrpId = lGroupId.GetInt();
                        lLedger.SubGrpId = lSubGroupId;
                        lLedger.SyncRowVersion = eLedger.SyncRowVersion;

                        await conn.UpdateAsync(lLedger, trans, InjectData.SqlTimeOutSeconds);
                    }
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

    public async Task<NonQueryResult> PushNewAsync(AccountLedgerDataSync localDataSync)
    {
        return await PushAsync(SyncRepoType.GeneralLedger, localDataSync);
    }

    public async Task<NonQueryResult> PutNewAsync(AccountLedgerDataSync localDataSync)
    {
        return await PatchAsync(SyncRepoType.GeneralLedger, localDataSync);
    }

    public async Task<NonQueryResult> PullAllNewAsync()
    {
        return await Task.FromResult(new NonQueryResult(true, true, null, ResultType.NoError));
    }

    public async Task<NonQueryResult> PullAllPatchedAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch the account data from local datasource. " +
                                  localResponse.ErrorMessage;
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingPatchedDataAsync(localResponse.Model);
        if (!incResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch incoming account data from external datasource. " +
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
            result.ErrorMessage = "Error fetching the sales data from external datasource. " +
                                  extResponse.ErrorMessage;
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingNewDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage = "Error preparing the sales data to push to external data provider. " +
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
            result.ErrorMessage = "Error fetching the account data from external datasource. " +
                                  extResponse.ErrorMessage;
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingPatchedDataAsync(extResponse.Model);
        if (!outResponse.Success)
        {
            result.ErrorMessage = "Error preparing the account data to push to external data provider. " +
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

    public Task<ListResult<AccountLedgerDataSync>> GetUnSynchronizedDataAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<NonQueryResult> PutNewListAsync(List<AccountLedgerDataSync> localData)
    {
        return await PutListAsync(localData);
    }

    public async Task<NonQueryResult> PushNewListAsync(List<AccountLedgerDataSync> localData)
    {
        return await PushListAsync(localData);
    }
    public async Task<NonQueryResult> PushNewListAsync(List<StockDetail> localData)
    {
        return await PushListAsync(localData);
    }
}