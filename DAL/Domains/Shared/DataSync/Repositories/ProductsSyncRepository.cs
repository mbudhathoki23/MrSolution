using Dapper;
using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using MoreLinq;
using MrDAL.Core.Abstractions;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MrDAL.Domains.Shared.DataSync.Repositories;

public class ProductsSyncRepository : DbSyncRepositoryBase, IDataSyncRepository<ProductDataSync>
{
    private ProductDataSync _externalDataSync;

    public ProductsSyncRepository(DbSyncRepoInjectData injectData)
        : base(injectData)
    {
    }

    public async Task<InfoResult<ProductDataSync>> GetExternalDataAsync()
    {
        var response = await base.GetExternalDataAsync<ProductDataSync>(SyncRepoType.Product);
        _externalDataSync = response.Model;
        return response;
    }

    public async Task<InfoResult<ProductDataSync>> GetLocalDataAsync()
    {
        var result = new InfoResult<ProductDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var products = await conn.GetAllAsync<Product>();
            var productGroups = await conn.GetAllAsync<ProductGroup>();
            var productSubGroups = await conn.GetAllAsync<ProductSubGroup>();
            var units = await conn.GetAllAsync<ProductUnit>();

            result.Model = new ProductDataSync
            {
                Products = products.AsList(),
                Units = units.AsList(),
                ProductGroups = productGroups.AsList(),
                SubGroups = productSubGroups.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ProductDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<ProductDataSync>> GetLocalOriginDataAsync()
    {
        var result = new InfoResult<ProductDataSync>();

        var conn = new SqlConnection(InjectData.LocalConnectionString);
        try
        {
            var products = await conn.QueryAsync<Product>(
                $@"SELECT * FROM AMS.Product WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var productGroups = await conn.QueryAsync<ProductGroup>(
                $@"SELECT * FROM AMS.ProductGroup WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var productSubGroups = await conn.QueryAsync<ProductSubGroup>(
                $@"SELECT * FROM AMS.ProductSubGroup WHERE SyncOriginId = '{InjectData.LocalOriginId}'");
            var units = await conn.QueryAsync<ProductUnit>(
                $@"SELECT * FROM AMS.ProductUnit WHERE SyncOriginId = '{InjectData.LocalOriginId}'");

            result.Model = new ProductDataSync
            {
                Products = products.AsList(),
                Units = units.AsList(),
                ProductGroups = productGroups.AsList(),
                SubGroups = productSubGroups.AsList()
            };
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ProductDataSync>(this);
        }
        finally
        {
            conn.Dispose();
        }

        return result;
    }

    public async Task<InfoResult<ProductDataSync>> GetIncomingNewDataAsync(
        ProductDataSync localDataSync)
    {
        var result = new InfoResult<ProductDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage =
                $"Error fetching data from external source. {Environment.NewLine}{extResponse.ErrorMessage}";
            return result;
        }

        var data = new ProductDataSync
        {
            Units = new List<ProductUnit>(),
            Products = new List<Product>(),
            ProductGroups = new List<ProductGroup>(),
            SubGroups = new List<ProductSubGroup>()
        };
        var extData = extResponse.Model;

        try
        {
            extData.Units.DistinctBy(u => u.SyncBaseId).ToList().ForEach(e =>
            {
                var latest = extData.Units.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.Units.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.Units.Add(latest);
            });

            extData.ProductGroups.DistinctBy(g => g.SyncBaseId).ForEach(e =>
            {
                var latest = extData.ProductGroups.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.ProductGroups.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.ProductGroups.Add(latest);
            });

            extData.SubGroups.DistinctBy(g => g.SyncBaseId).ForEach(e =>
            {
                var latest = extData.SubGroups.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.SubGroups.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.SubGroups.Add(latest);
            });

            extData.Products.DistinctBy(p => p.SyncBaseId).ForEach(e =>
            {
                var latest = extData.Products.Where(x => x.SyncBaseId == e.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .First();
                var lRow = localDataSync.Products.FirstOrDefault(l => l.SyncBaseId == e.SyncBaseId);
                if (lRow == null) data.Products.Add(latest);
            });

            result.Success = true;
            result.Model = data;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ProductDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<ProductDataSync>> GetIncomingPatchedDataAsync(ProductDataSync localDataSync)
    {
        var result = new InfoResult<ProductDataSync>();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage = $"Error fetching the external data. {extResponse.ErrorMessage}";
            return result;
        }

        var data = new ProductDataSync
        {
            ProductGroups = new List<ProductGroup>(),
            Products = new List<Product>(),
            SubGroups = new List<ProductSubGroup>(),
            Units = new List<ProductUnit>()
        };

        try
        {
            //  fetch the patched product groups
            localDataSync.ProductGroups.ForEach(l =>
            {
                var eGroup = extResponse.Model.ProductGroups
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eGroup != null && eGroup.SyncRowVersion > l.SyncRowVersion)
                    data.ProductGroups.Add(eGroup);
            });

            // fetch the patch product sub-groups
            localDataSync.SubGroups.ForEach(l =>
            {
                var eSubGroup = extResponse.Model.SubGroups
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eSubGroup != null && eSubGroup.SyncRowVersion > l.SyncRowVersion)
                    data.SubGroups.Add(eSubGroup);
            });

            // fetch the units
            localDataSync.Units.ForEach(l =>
            {
                var eUnit = extResponse.Model.Units
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eUnit != null && eUnit.SyncRowVersion > l.SyncRowVersion)
                    data.Units.Add(eUnit);
            });

            // fetch the products
            localDataSync.Products.ForEach(l =>
            {
                var eProduct = extResponse.Model.Products
                    .Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();

                if (eProduct != null && eProduct.SyncRowVersion > l.SyncRowVersion)
                    data.Products.Add(eProduct);
            });

            result.Model = data;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ProductDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<ProductDataSync>> GetOutgoingNewDataAsync(
        ProductDataSync externalDataSync)
    {
        var result = new InfoResult<ProductDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = "Unable to fetch the local data. " + localDataRes.ErrorMessage;
            return localDataRes;
        }

        var localData = localDataRes.Model;
        var data = new ProductDataSync
        {
            ProductGroups = new List<ProductGroup>(),
            Products = new List<Product>(),
            SubGroups = new List<ProductSubGroup>(),
            Units = new List<ProductUnit>()
        };

        try
        {
            //  fetch the patched product groups
            localData.ProductGroups.ForEach(l =>
            {
                var eRow = externalDataSync.ProductGroups.FirstOrDefault(x => x.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.ProductGroups.Add(l);
            });

            // fetch the patch product sub-groups
            localData.SubGroups.ForEach(l =>
            {
                var eRow = externalDataSync.SubGroups.FirstOrDefault(x => x.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.SubGroups.Add(l);
            });

            // fetch the units
            localData.Units.ForEach(l =>
            {
                var eRow = externalDataSync.Units.FirstOrDefault(x => x.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.Units.Add(l);
            });

            // fetch the products
            localData.Products.ForEach(l =>
            {
                var eRow = externalDataSync.Products.FirstOrDefault(x => x.SyncBaseId == l.SyncBaseId);
                if (eRow == null) data.Products.Add(l);
            });

            result.Model = data;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ProductDataSync>(this);
        }

        return result;
    }

    public async Task<InfoResult<ProductDataSync>> GetOutgoingPatchedDataAsync(ProductDataSync externalDataSync)
    {
        var result = new InfoResult<ProductDataSync>();

        var localDataRes = await GetLocalOriginDataAsync();
        if (!localDataRes.Success)
        {
            localDataRes.ErrorMessage = "Unable to fetch the local data. " + localDataRes.ErrorMessage;
            return localDataRes;
        }

        var localData = localDataRes.Model;
        var data = new ProductDataSync
        {
            ProductGroups = new List<ProductGroup>(),
            Products = new List<Product>(),
            SubGroups = new List<ProductSubGroup>(),
            Units = new List<ProductUnit>()
        };

        try
        {
            //  fetch the patched product groups
            localData.ProductGroups.ForEach(l =>
            {
                var eRow = externalDataSync.ProductGroups.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion) data.ProductGroups.Add(l);
            });

            // fetch the patch product sub-groups
            localData.SubGroups.ForEach(l =>
            {
                var eRow = externalDataSync.SubGroups.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion) data.SubGroups.Add(l);
            });

            // fetch the units
            localData.Units.ForEach(l =>
            {
                var eRow = externalDataSync.Units.Where(x => x.SyncBaseId == l.SyncBaseId).MaxBy(m => m.SyncRowVersion)
                    .FirstOrDefault();
                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.Units.Add(l);
            });

            // fetch the products
            localData.Products.ForEach(l =>
            {
                var eRow = externalDataSync.Products.Where(x => x.SyncBaseId == l.SyncBaseId)
                    .MaxBy(m => m.SyncRowVersion).FirstOrDefault();
                if (eRow != null && l.SyncRowVersion > eRow.SyncRowVersion)
                    data.Products.Add(l);
            });

            result.Model = data;
            result.Success = true;
        }
        catch (Exception e)
        {
            result = e.ToInfoErrorResult<ProductDataSync>(this);
        }

        return result;
    }

    public async Task<NonQueryResult> PullNewAsync(ProductDataSync newDataSync)
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
            result.ErrorMessage =
                $"Error fetching the products data from external source. {extDataResponse.ErrorMessage}";
            result.ResultType = extDataResponse.ResultType;
            return result;
        }

        var localModel = localDataResponse.Model;
        var extModel = extDataResponse.Model;

        const string unitTable = "AMS.ProductUnit";
        const string productTable = "AMS.Product";
        const string groupTable = "AMS.ProductGroup";
        const string subGroupTable = "AMS.ProductSubGroup";

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                // sync unit
                foreach (var unit in newDataSync.Units)
                {
                    var newUnitId = await conn.NewIntegerIdAsync(unitTable, "UID", trans);
                    if (newUnitId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new Unit.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var nUnit = unit.DeepCopy();
                    nUnit.UID = newUnitId;
                    nUnit.Company_Id = unit.Company_Id.HasValue ? InjectData.LocalCompanyUnitId : null;
                    nUnit.EnterDate = InjectData.DateTime;
                    nUnit.EnterBy = InjectData.Username;
                    nUnit.Branch_ID = InjectData.LocalBranchId;
                    //nUnit.SyncOriginId = InjectData.LocalOriginId;
                    nUnit.SyncGlobalId = Guid.NewGuid();
                    nUnit.SyncCreatedOn = InjectData.DateTime;
                    nUnit.SyncRowVersion = unit.SyncRowVersion;
                    nUnit.SyncBaseId = unit.SyncBaseId;

                    await conn.InsertAsync(nUnit, trans, InjectData.SqlTimeOutSeconds);
                    localModel.Units.Add(nUnit);
                }

                // sync product group
                foreach (var group in newDataSync.ProductGroups)
                {
                    var newGroupId = await conn.NewIntegerIdAsync(groupTable, "PGrpID", trans);
                    if (newGroupId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new ProductGroup.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var nGroup = group.DeepCopy();
                    nGroup.PGrpId = newGroupId;
                    nGroup.EnterDate = InjectData.DateTime;
                    nGroup.EnterBy = InjectData.Username;
                    nGroup.Branch_ID = group.Branch_ID.IsValueExits() ? InjectData.LocalBranchId : 0;
                    nGroup.Company_ID = group.Company_ID.HasValue ? InjectData.LocalCompanyUnitId : 0;
                    // nGroup.SyncOriginId = InjectData.LocalOriginId;
                    nGroup.SyncGlobalId = Guid.NewGuid();
                    nGroup.SyncCreatedOn = InjectData.DateTime;
                    nGroup.SyncRowVersion = group.SyncRowVersion;
                    nGroup.SyncBaseId = group.SyncBaseId;

                    var insertAsync = await conn.InsertAsync(nGroup, trans, InjectData.SqlTimeOutSeconds);
                    localModel.ProductGroups.Add(nGroup);
                }

                // sync product sub-group
                foreach (var sGroup in newDataSync.SubGroups)
                {
                    var newSGroupId = await conn.NewIntegerIdAsync(subGroupTable, "PSubGrpId", trans);
                    if (newSGroupId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new ProductSubGroup.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var extGroup = extModel.ProductGroups.FirstOrDefault(x =>
                        x.PGrpId == sGroup.GrpId && x.SyncOriginId == sGroup.SyncOriginId);

                    var nSubGroup = sGroup.DeepCopy();
                    nSubGroup.PSubGrpId = newSGroupId;
                    nSubGroup.EnterDate = InjectData.DateTime;
                    nSubGroup.EnterBy = InjectData.Username;
                    nSubGroup.GrpId = (int)(extGroup != null
                        ? localModel.ProductGroups.FirstOrDefault(x => x.SyncBaseId == extGroup.SyncBaseId)?.PGrpId.GetHashCode()
                        : 0);
                    //&& x.SyncOriginId == InjectData.LocalOriginId)

                    nSubGroup.Branch_ID = sGroup.Branch_ID.IsValueExits() ? InjectData.LocalBranchId : 0;
                    nSubGroup.Company_Id = sGroup.Company_Id.HasValue ? InjectData.LocalCompanyUnitId : null;
                    //nSubGroup.SyncOriginId = InjectData.LocalOriginId;
                    nSubGroup.SyncGlobalId = Guid.NewGuid();
                    nSubGroup.SyncCreatedOn = InjectData.DateTime;
                    nSubGroup.SyncRowVersion = sGroup.SyncRowVersion;
                    nSubGroup.SyncBaseId = sGroup.SyncBaseId;

                    await conn.InsertAsync(nSubGroup, trans, InjectData.SqlTimeOutSeconds);
                    localModel.SubGroups.Add(nSubGroup);
                }

                // sync product
                foreach (var product in newDataSync.Products)
                {
                    var newId = await conn.NewBigIntIdAsync(productTable, "PID", trans);
                    if (newId == 0)
                    {
                        result.ErrorMessage = "Error generating Id for new product.";
                        result.ResultType = ResultType.InternalError;
                        return result;
                    }

                    var extUnit = extModel.Units.FirstOrDefault(x =>
                        x.UID == product.PUnit && x.SyncOriginId == product.SyncOriginId);
                    var extAltUnit = extModel.Units.FirstOrDefault(x =>
                        x.UID == product.PAltUnit && x.SyncOriginId == product.SyncOriginId);
                    var extGroup = extModel.ProductGroups.FirstOrDefault(x =>
                        x.PGrpId == product.PGrpId && x.SyncOriginId == product.SyncOriginId);
                    var extSubgroup = extModel.SubGroups.FirstOrDefault(x =>
                        x.PSubGrpId == product.PSubGrpId && x.SyncOriginId == product.SyncOriginId);

                    var nProduct = product.DeepCopy();
                    nProduct.PID = newId;
                    nProduct.PUnit = extUnit == null
                        ? 0
                        : localModel.Units.FirstOrDefault(x =>
                            x.SyncBaseId == extUnit.SyncBaseId)?.UID ?? 0;
                    //&& x.SyncOriginId == InjectData.LocalOriginId)?.UID ?? 0;
                    nProduct.PAltUnit = extAltUnit == null
                        ? null
                        : localModel.Units.FirstOrDefault(x =>
                            x.SyncBaseId == extAltUnit.SyncBaseId)?.UID;
                    // && x.SyncOriginId == InjectData.LocalOriginId)?.UID;

                    nProduct.PSubGrpId = extSubgroup == null
                        ? null
                        : localModel.SubGroups.FirstOrDefault(x =>
                            x.SyncBaseId == extSubgroup.SyncBaseId)?.PSubGrpId;
                    //&& x.SyncOriginId == InjectData.LocalOriginId)?.PSubGrpId;

                    nProduct.EnterBy = InjectData.Username;
                    nProduct.EnterDate = InjectData.DateTime;
                    nProduct.PGrpId = extGroup == null
                        ? null
                        : localModel.ProductGroups.FirstOrDefault(x =>
                            x.SyncBaseId == extGroup.SyncBaseId)?.PGrpId;
                    //&& x.SyncOriginId == InjectData.LocalOriginId)?.PGrpID;

                    nProduct.PL_Closing = null;
                    nProduct.PL_Opening = null;
                    nProduct.Branch_Id = InjectData.LocalBranchId;
                    // nProduct.SyncOriginId = InjectData.LocalOriginId;
                    nProduct.SyncGlobalId = Guid.NewGuid();
                    nProduct.SyncCreatedOn = InjectData.DateTime;
                    nProduct.SyncRowVersion = product.SyncRowVersion;
                    nProduct.SyncBaseId = product.SyncBaseId;

                    await conn.InsertAsync(nProduct, trans, InjectData.SqlTimeOutSeconds);
                    localModel.Products.Add(nProduct);
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

    public async Task<NonQueryResult> PullPatchedAsync(ProductDataSync patchedDataSync)
    {
        var result = new NonQueryResult();

        try
        {
            var conn = new SqlConnection(InjectData.LocalConnectionString);
            await conn.OpenAsync();

            var trans = conn.BeginTransaction();
            try
            {
                // patch the product groups
                foreach (var eGroup in patchedDataSync.ProductGroups)
                {
                    var lGroup = await conn.QueryFirstOrDefaultAsync<ProductGroup>(
                        @"SELECT * FROM AMS.ProductGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eGroup.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    if (lGroup == null) continue;
                    lGroup.GrpName = eGroup.GrpName;
                    lGroup.GrpCode = eGroup.GrpCode;
                    lGroup.GPrinter = eGroup.GPrinter;
                    lGroup.Status = eGroup.Status;
                    lGroup.EnterBy = InjectData.Username;
                    lGroup.EnterDate = InjectData.DateTime;
                    lGroup.Branch_ID = InjectData.LocalBranchId;
                    lGroup.Company_ID = eGroup.Company_ID == null ? null : InjectData.LocalCompanyUnitId;
                    lGroup.SyncLastPatchedOn = InjectData.DateTime;
                    lGroup.SyncRowVersion = eGroup.SyncRowVersion;
                    await conn.UpdateAsync(lGroup, trans, InjectData.SqlTimeOutSeconds);
                }

                // patch product sub-groups
                foreach (var eSubGroup in patchedDataSync.SubGroups)
                {
                    var lSubGroup = await conn.QueryFirstOrDefaultAsync<ProductSubGroup>(
                        @"SELECT * FROM AMS.ProductSubGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eSubGroup.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    if (lSubGroup == null) continue;
                    var extGroup = _externalDataSync.ProductGroups.FirstOrDefault(x => x.PGrpId == lSubGroup.GrpId);
                    var groupId = extGroup == null
                        ? null
                        : await conn.QueryFirstOrDefaultAsync<int?>(
                            @"SELECT * FROM AMS.ProductGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                            new
                            {
                                baseId = extGroup.SyncBaseId,
                                originId = InjectData.LocalOriginId
                            }, trans);

                    lSubGroup.SubGrpName = eSubGroup.SubGrpName;
                    lSubGroup.ShortName = eSubGroup.ShortName;
                    lSubGroup.Status = eSubGroup.Status;
                    lSubGroup.EnterDate = InjectData.DateTime;
                    lSubGroup.SyncLastPatchedOn = InjectData.DateTime;
                    lSubGroup.SyncRowVersion = eSubGroup.SyncRowVersion;
                    lSubGroup.EnterBy = eSubGroup.EnterBy;
                    lSubGroup.Company_Id = eSubGroup.Company_Id == null ? null : InjectData.LocalCompanyUnitId;
                    lSubGroup.Branch_ID = InjectData.LocalBranchId;
                    lSubGroup.GrpId = groupId.GetHashCode();

                    await conn.UpdateAsync(lSubGroup, trans, InjectData.SqlTimeOutSeconds);
                }

                // patch product units
                foreach (var eUnit in patchedDataSync.Units)
                {
                    var lUnit = await conn.QueryFirstOrDefaultAsync<ProductUnit>(
                        @"SELECT * FROM AMS.ProductUnit WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eUnit.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    if (lUnit == null) continue;
                    lUnit.UnitName = eUnit.UnitName;
                    lUnit.UnitCode = eUnit.UnitCode;
                    lUnit.Status = eUnit.Status;
                    lUnit.EnterDate = InjectData.DateTime;
                    lUnit.EnterBy = InjectData.Username;
                    lUnit.Branch_ID = InjectData.LocalBranchId;
                    lUnit.SyncLastPatchedOn = InjectData.DateTime;
                    lUnit.SyncRowVersion = eUnit.SyncRowVersion;
                    lUnit.Company_Id = eUnit.Company_Id == null ? null : InjectData.LocalCompanyUnitId;
                    await conn.UpdateAsync(lUnit, trans, InjectData.SqlTimeOutSeconds);
                }

                // patch products
                foreach (var eProduct in patchedDataSync.Products)
                {
                    var lProduct = await conn.QueryFirstOrDefaultAsync<Product>(
                        @"SELECT * FROM AMS.Product WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                        new
                        {
                            baseId = eProduct.SyncBaseId,
                            originId = InjectData.LocalOriginId
                        }, trans);

                    var extUnit = _externalDataSync.Units.FirstOrDefault(x =>
                        x.UID == eProduct.PUnit && x.SyncOriginId == eProduct.SyncOriginId);
                    var unitId = extUnit == null
                        ? null
                        : await conn.QueryFirstOrDefaultAsync<int?>(
                            @"SELECT * FROM AMS.ProductUnit WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                            new
                            {
                                baseId = extUnit.SyncBaseId,
                                originId = InjectData.LocalOriginId
                            }, trans);

                    var altEdtUnit = _externalDataSync.Units.FirstOrDefault(x =>
                        x.UID == eProduct.PAltUnit && x.SyncOriginId == eProduct.SyncOriginId);
                    var altUnitId = altEdtUnit == null
                        ? null
                        : await conn.QueryFirstOrDefaultAsync<int?>(
                            @"SELECT * FROM AMS.ProductUnit WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                            new
                            {
                                baseId = altEdtUnit.SyncBaseId,
                                originId = InjectData.LocalOriginId
                            }, trans);

                    var extGroup = _externalDataSync.ProductGroups.FirstOrDefault(x => x.PGrpId == eProduct.PGrpId);
                    var groupId = extGroup == null
                        ? null
                        : await conn.QueryFirstOrDefaultAsync<int?>(
                            @"SELECT * FROM AMS.ProductGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                            new
                            {
                                baseId = extGroup.SyncBaseId,
                                originId = InjectData.LocalOriginId
                            }, trans);

                    var extSubGroup =
                        _externalDataSync.SubGroups.FirstOrDefault(x => x.PSubGrpId == eProduct.PSubGrpId);
                    var subGroupId = extSubGroup == null
                        ? null
                        : await conn.QueryFirstOrDefaultAsync<int?>(
                            @"SELECT * FROM AMS.ProductSubGroup WHERE SyncBaseId = @baseId AND SyncOriginId = @originId ",
                            new
                            {
                                baseId = extSubGroup.SyncBaseId,
                                originId = InjectData.LocalOriginId
                            }, trans);

                    if (lProduct == null) continue;
                    var uProduct = eProduct.DeepCopy();
                    uProduct.PID = lProduct.PID;
                    uProduct.Branch_Id = lProduct.Branch_Id;
                    uProduct.EnterBy = InjectData.Username;
                    uProduct.PUnit = unitId.GetValueOrDefault(0);
                    uProduct.PAltUnit = altUnitId;
                    uProduct.PGrpId = groupId;
                    uProduct.PSubGrpId = subGroupId;
                    uProduct.SyncLastPatchedOn = InjectData.DateTime;
                    uProduct.SyncOriginId = lProduct.SyncOriginId;
                    uProduct.SyncGlobalId = lProduct.SyncGlobalId;

                    await conn.UpdateAsync(uProduct, trans, InjectData.SqlTimeOutSeconds);
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

    public async Task<NonQueryResult> PushNewAsync(ProductDataSync localDataSync)
    {
        return await PushAsync(SyncRepoType.Product, localDataSync);
    }

    public async Task<NonQueryResult> PutNewAsync(ProductDataSync localDataSync)
    {
        return await PatchAsync(SyncRepoType.Product, localDataSync);
    }

    public async Task<NonQueryResult> PullAllNewAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage = "Unable to fetch the products data from local datasource. " +
                                  localResponse.ErrorMessage;
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingNewDataAsync(localResponse.Model);
        if (incResponse.Success) return await PullNewAsync(incResponse.Model);
        result.ErrorMessage =
            $"Unable to fetch incoming purchase return data from external datasource. {incResponse.ErrorMessage}";
        result.ResultType = incResponse.ResultType;
        return result;
    }

    public async Task<NonQueryResult> PullAllPatchedAsync()
    {
        var result = new NonQueryResult();

        var localResponse = await GetLocalDataAsync();
        if (!localResponse.Success)
        {
            result.ErrorMessage =
                $"Unable to fetch the products data from local datasource. {localResponse.ErrorMessage}";
            result.ResultType = localResponse.ResultType;
            return result;
        }

        var incResponse = await GetIncomingPatchedDataAsync(localResponse.Model);
        if (incResponse.Success) return await PullPatchedAsync(incResponse.Model);
        result.ErrorMessage =
            $"Unable to fetch incoming products data from external datasource. {incResponse.ErrorMessage}";
        result.ResultType = incResponse.ResultType;
        return result;
    }

    public async Task<NonQueryResult> PushAllNewAsync()
    {
        var result = new NonQueryResult();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage =
                $"Error fetching the products data from external datasource. {extResponse.ErrorMessage}";
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingNewDataAsync(extResponse.Model);
        if (outResponse.Success) return await PushNewAsync(outResponse.Model);
        result.ErrorMessage =
            $"Error preparing the products data to push to external data provider. {outResponse.ErrorMessage}";
        result.ResultType = outResponse.ResultType;
        return result;
    }

    public async Task<NonQueryResult> PushAllPatchedAsync()
    {
        var result = new NonQueryResult();

        var extResponse = await GetExternalDataAsync();
        if (!extResponse.Success)
        {
            result.ErrorMessage =
                $"Error fetching the products data from external datasource. {extResponse.ErrorMessage}";
            result.ResultType = extResponse.ResultType;
            return result;
        }

        var outResponse = await GetOutgoingPatchedDataAsync(extResponse.Model);
        if (outResponse.Success) return await PutNewAsync(outResponse.Model);
        result.ErrorMessage = "Error preparing the products data to push to external data provider. " +
                              outResponse.ErrorMessage;
        result.ResultType = outResponse.ResultType;
        return result;
    }

    public Task<NonQueryResult> DeleteNewAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ListResult<ProductDataSync>> GetUnSynchronizedDataAsync()
    {
        return await base.GetUnSynchronizedDataAsync<ProductDataSync>();
    }

    public async Task<NonQueryResult> PushNewListAsync(List<ProductDataSync> localData)
    {
        return await PushListAsync(localData);
    }

    public async Task<NonQueryResult> PutNewListAsync(List<ProductDataSync> localData)
    {
        return await PutListAsync(localData);
    }
}