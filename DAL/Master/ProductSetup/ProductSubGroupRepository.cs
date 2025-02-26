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

public class ProductSubGroupRepository : IProductSubGroupRepository
{
    public ProductSubGroupRepository()
    {
        ObjProductSubGroup = new ProductSubGroup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    public int SaveProductSubGroup(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(" INSERT INTO AMS.ProductSubGroup(PSubGrpId, NepaliDesc, SubGrpName, ShortName, GrpId, Branch_ID, Company_Id, EnterBy, EnterDate, IsDefault, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append($" VALUES ( {ObjProductSubGroup.PSubGrpId}, ");
            cmdString.Append($" N'{ObjProductSubGroup.NepaliDesc}',N'{ObjProductSubGroup.SubGrpName}',N'{ObjProductSubGroup.ShortName}',N'{ObjProductSubGroup.GrpId}',");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append($" '{ObjGlobal.LogInUser}', GETDATE(),0,");
            cmdString.Append(ObjProductSubGroup.Status ? "1," : "0,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"{ObjProductSubGroup.SyncRowVersion.GetDecimal(true)} ); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.ProductSubGroup SET \n");
            cmdString.Append($"NepaliDesc = N'{ObjProductSubGroup.SubGrpName}', SubGrpName = N'{ObjProductSubGroup.SubGrpName}',ShortName = N'{ObjProductSubGroup.ShortName}',GrpId = {ObjProductSubGroup.GrpId} ");
            cmdString.Append($" WHERE PSubGrpId = {ObjProductSubGroup.PSubGrpId};");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            SaveProductSubGroupAuditLog(actionTag);
            cmdString.Append($"DELETE FROM AMS.ProductSubGroup where PSubGrpId = {ObjProductSubGroup.PSubGrpId};");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncProductSubGroupAsync(actionTag));
            }
        }

        return exe;
    }

    public async Task<int> SyncProductSubGroupAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}ProductSubGroup/GetProductSubGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductSubGroup/InsertProductSubGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductSubGroup/UpdateProductSubGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productSubGroupRepo = DataSyncProviderFactory.GetRepository<ProductSubGroup>(_injectData);
        var productSubGroups = new List<ProductSubGroup>
        {
            ObjProductSubGroup
        };

        // push realtime details to server
        await productSubGroupRepo.PushNewListAsync(productSubGroups);

        // update main area SyncGlobalId to local
        if (productSubGroupRepo.GetHashCode() > 0)
        {
            await SyncUpdateProductSubGroup(ObjProductSubGroup.PSubGrpId);
        }
        return productSubGroupRepo.GetHashCode();

    }

    public async Task<bool> SyncProductSubGroupDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}ProductSubGroup/GetProductSubGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductSubGroup/InsertProductSubGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductSubGroup/UpdateProductSubGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productSubGroupRepo = DataSyncProviderFactory.GetRepository<ProductSubGroup>(_injectData);

        // pull all new product sub group data
        var pullResponse = await PullProductSubGroupsServerToClientByRowCount(productSubGroupRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new product sub group data
        var sqlQuery = GetProductSubGroupScript();
        var queryResponse = await QueryUtils.GetListAsync<ProductSubGroup>(sqlQuery);
        var psgList = queryResponse.List.ToList();
        if (psgList.Count > 0)
        {
            var pushResponse = await productSubGroupRepo.PushNewListAsync(psgList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateProductSubGroup(long productSubGroupId = 0)
    {
        var commandText = $@"
            UPDATE AMS.ProductSubGroup SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (productSubGroupId > 0)
        {
            commandText += $" WHERE PSubGrpId = '{productSubGroupId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public int SaveProductSubGroupAuditLog(string actionTag)
    {
        var cmdString = $@"
            INSERT INTO AUD.AUDIT_PRODUCTSUBGROUP(PSubGrpId, SubGrpName, ShortName, GrpId, Branch_ID, Company_ID, EnterBy, EnterDate, Status, ModifyAction, ModifyBy, ModifyDate)
            SELECT PSubGrpId, SubGrpName, ShortName, GrpId, Branch_ID, Company_ID, EnterBy, EnterDate, Status,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate
            FROM AMS.ProductSubGroup
            WHERE PSubGrpId='{ObjProductSubGroup.PSubGrpId}'";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    public string GetProductSubGroupScript(int productSubGroupId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.ProductSubGroup";
        cmdString += productSubGroupId > 0 ? $" WHERE SyncGlobalId IS NULL AND PSubGrpId= {productSubGroupId} " : "";
        return cmdString;
    }


    // PULL PRODUCT SUB GROUP
    #region ---------- PULL PRODUCT SUB GROUP ----------

    public async Task<bool> PullProductSubGroupsServerToClientByRowCount(IDataSyncRepository<ProductSubGroup> productSubGroupRepo, int callCount)
    {
        try
        {
            var pullResponse = await productSubGroupRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetProductSubGroupScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);
            foreach (var productSubGroupData in pullResponse.List)
            {
                ObjProductSubGroup = productSubGroupData;

                var alreadyExistData = alldata.Select("PSubGrpId= '" + productSubGroupData.PSubGrpId + "'");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (productSubGroupData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveProductSubGroup("UPDATE");
                    }
                }
                else
                {
                    var result = SaveProductSubGroup("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullProductSubGroupsServerToClientByRowCount(productSubGroupRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    // RETURN DATA IN DATA TABLE
    public DataTable GetMasterProductSubGroup(string tag, string category, bool status = false, int selectedId = 0, int groupId = 0)
    {
        var cmdString =
            $"SELECT PSubGrpId,SubGrpName,ShortName,GrpId, pg.GrpName,psg.Status From AMS.ProductSubGroup psg left outer join ams.ProductGroup as pg on pg.PGrpID= psg.GrpId where PSubGrpId= '{selectedId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public ProductSubGroup ObjProductSubGroup { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;

}