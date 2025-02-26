using DatabaseModule.CloudSync;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.ProductSetup;

public class ProductUnitRepository : IProductUnitRepository
{
    public ProductUnitRepository()
    {
        ObjProductUnit = new ProductUnit();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }
    public int SaveProductUnit(string actionTag)
    {
        var cmdString = new StringBuilder();
        switch (actionTag.ToUpper())
        {
            case "SAVE":
            {
                cmdString.Append(
                    "INSERT INTO AMS.ProductUnit(UID, NepaliDesc, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)\n");
                cmdString.Append($"Values({ObjProductUnit.UID}, ");
                cmdString.Append(ObjProductUnit.UnitName.IsValueExits() ? $"N'{ObjProductUnit.UnitName}'," : "NULL,");
                cmdString.Append(ObjProductUnit.UnitName.IsValueExits() ? $"N'{ObjProductUnit.UnitName}'," : "NULL,");
                cmdString.Append(ObjProductUnit.UnitCode.IsValueExits() ? $"N'{ObjProductUnit.UnitCode}'," : "NULL,");
                cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
                cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
                cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
                cmdString.Append(ObjProductUnit.Status ? "1," : "0,");
                cmdString.Append("0,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                    ? $" '{ObjGlobal.LocalOriginId}',"
                    : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
                cmdString.Append($"{ObjProductUnit.SyncRowVersion}); \n");
                break;
            }
            case "UPDATE":
            {
                cmdString.Append("UPDATE AMS.ProductUnit SET \n");
                cmdString.Append(ObjProductUnit.UnitName.IsValueExits()
                    ? $"UnitName= N'{ObjProductUnit.UnitName}',"
                    : "UnitName= NULL,");
                cmdString.Append(ObjProductUnit.UnitCode.IsValueExits()
                    ? $"UnitCode= N'{ObjProductUnit.UnitCode}',"
                    : "UnitCode = NULL,");
                cmdString.Append(ObjProductUnit.Status ? "Status= 1," : "Status= 0,");
                cmdString.Append("SyncLastPatchedOn = GETDATE(),");
                cmdString.Append($"SyncRowVersion = {ObjProductUnit.SyncRowVersion}");
                cmdString.Append($"WHERE UID = {ObjProductUnit.UID}");
                break;
            }
            case "DELETE":
            {
                SaveProductUnitAuditLog(actionTag);
                cmdString.Append($"Delete from AMS.ProductUnit where UID = {ObjProductUnit.UID}");
                break;
            }
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncProductUnitAsync(actionTag));
            }
        }

        return exe;
    }

    public async Task<bool> SyncProductUnitDetailsAsync()
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item1 == null)
        {
            return true;
        }
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}ProductUnit/GetProductUnitsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductUnit/InsertProductUnitList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductUnit/UpdateProductUnit"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productUnitRepo = DataSyncProviderFactory.GetRepository<ProductUnit>(_injectData);
        // pull all new product unit data
        var pullResponse = await PullProductUnitsServerToClientByRowCount(productUnitRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new product unit data
        var sqlQuery = GetProductUnitScript();
        var queryResponse = await QueryUtils.GetListAsync<ProductUnit>(sqlQuery);
        var uList = queryResponse.List.ToList();
        if (uList.Count > 0)
        {
            var pushResponse = await productUnitRepo.PushNewListAsync(uList);
            if (!pushResponse.Value)
            {
                //SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public async Task<int> SyncProductUnitAsync(string actionTag)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item1 == null)
        {
            return 1;
        }
        var apiConfig = new SyncApiConfig
        {
            BaseUrl = _configParams.Model.Item2,
            Apikey = _configParams.Model.Item3,
            Username = ObjGlobal.LogInUser,
            BranchId = ObjGlobal.SysBranchId,
            GetUrl = @$"{_configParams.Model.Item2}ProductUnit/GetProductUnitsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductUnit/InsertProductUnitList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductUnit/UpdateProductUnit"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productUnitRepo = DataSyncProviderFactory.GetRepository<ProductUnit>(_injectData);
        var productUnits = new List<ProductUnit>
        {
            ObjProductUnit
        };

        // push realtime details to server
        await productUnitRepo.PushNewListAsync(productUnits);

        // update main area SyncGlobalId to local
        if (productUnitRepo.GetHashCode() > 0)
        {
            await SyncUpdateProductUnit(ObjProductUnit.UID);
        }

        return productUnitRepo.GetHashCode();
    }
    public Task<int> SyncUpdateProductUnit(long productUnitId = 0)
    {
        var commandText = $@"
            UPDATE AMS.ProductUnit SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (productUnitId > 0)
        {
            commandText += $" WHERE UID = '{productUnitId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public int SaveProductUnitAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_PRODUCTUNIT(UID, UnitName, UnitCode, Branch_ID, Company_ID, EnterBy, EnterDate, Status, ModifyAction, ModifyBy, ModifyDate)
            SELECT UID, UnitName, UnitCode, Branch_ID, Company_ID, EnterBy, EnterDate, Status,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate 
            FROM AMS.ProductUnit
            WHERE UID='{ObjProductUnit.UID}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    public string GetProductUnitScript(int productUnitId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.ProductUnit";
        cmdString += productUnitId > 0 ? $" WHERE SyncGlobalId IS NULL AND UID= {productUnitId} " : "";
        return cmdString;
    }


    // PULL PRODUCT UNIT
    #region ---------- PULL PRODUCT UNIT ----------

    public async Task<bool> PullProductUnitsServerToClientByRowCount(IDataSyncRepository<ProductUnit> productUnitRepo, int callCount)
    {
        try
        {
            var pullResponse = await productUnitRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetProductUnitScript();
            var dataSetSql = SqlExtensions.ExecuteDataSetSql(query);
            foreach (var productUnitData in pullResponse.List)
            {
                ObjProductUnit = productUnitData;

                var alreadyExistData = dataSetSql.Select("UID= '" + productUnitData.UID + "'");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    var rowVersionId = alreadyExistData[0]["SyncRowVersion"].GetInt();

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (productUnitData.SyncRowVersion > rowVersionId)
                    {
                        var result = SaveProductUnit("UPDATE");
                    }
                }
                else
                {
                    var result = SaveProductUnit("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullProductUnitsServerToClientByRowCount(productUnitRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    // RETURN VALUE IN DATA TABLE
    public DataTable GetMasterProductUnit(string actionTag, string category, int status = 0, int selectedId = 0)
    {
        var cmdString = $"Select * from AMS.ProductUnit  where UID='{selectedId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }




    // OBJECT FOR THIS FORM
    public ProductUnit ObjProductUnit { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}