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

public class ProductGroupRepository : IProductGroupRepository
{
    public ProductGroupRepository()
    {
        ObjProductGroup = new ProductGroup();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();

    }

    public int SaveProductGroup(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "DELETE")
        {
            SaveProductGroupAuditLog(actionTag);
            cmdString.Append($"Delete from AMS.ProductGroup where PGrpId = {ObjProductGroup.PGrpId} ");
        }

        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append("INSERT INTO AMS.ProductGroup (PGrpId, NepaliDesc, GrpName, GrpCode, GMargin, GPrinter, Branch_ID, Company_ID, Status, EnterBy, EnterDate, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
            cmdString.Append($" VALUES({ObjProductGroup.PGrpId = ClsMasterSetup.ReturnMaxIntValue("AMS.ProductGroup", "PGrpID")},");
            cmdString.Append($" N'{ObjProductGroup.NepaliDesc}',");
            cmdString.Append($" N'{ObjProductGroup.GrpName}',N'{ObjProductGroup.GrpCode}',");
            cmdString.Append(ObjProductGroup.GMargin.GetDecimal() > 0 ? $"{ObjProductGroup.GMargin.GetDecimal()}," : "NULL,");
            cmdString.Append(ObjProductGroup.GPrinter.IsValueExits() ? $"N'{ObjProductGroup.GPrinter}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append(ObjProductGroup.Status.GetBool() ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append($"GETDATE(),GETDATE(),{ObjProductGroup.SyncRowVersion.GetDecimal()},");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()); \n" : "NULL ); \n");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" UPDATE AMS.ProductGroup SET ");
            cmdString.Append($" NepaliDesc= N'{ObjProductGroup.NepaliDesc}', GrpName = N'{ObjProductGroup.GrpName}',GrpCode = N'{ObjProductGroup.GrpCode}',");
            cmdString.Append(ObjProductGroup.GMargin.GetDecimal() > 0
                ? $" GMargin = {ObjProductGroup.GMargin.GetDecimal()},"
                : "GMargin =NULL,");
            cmdString.Append(ObjProductGroup.GPrinter.IsValueExits()
                ? $"GPrinter = N'{ObjProductGroup.GPrinter}',"
                : "GPrinter = NULL,");
            cmdString.Append(ObjProductGroup.Status is true ? "Status= 1," : "Status = 0,");
            cmdString.Append(
                $"SyncLastPatchedOn = GETDATE(),SyncRowVersion = {ObjProductGroup.SyncRowVersion.GetDecimal()} \n");
            cmdString.Append($"WHERE PGrpID = {ObjProductGroup.PGrpId}");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0)
        {
            SaveProductGroupAuditLog(actionTag);
            if (ObjGlobal.IsOnlineSync)
            {
                Task.Run(() => SyncProductGroupAsync(actionTag));
            }
        }

        return exe;
    }
    public async Task<int> SyncProductGroupAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}ProductGroup/GetProductGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductGroup/InsertProductGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductGroup/UpdateProductGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productGroupRepo = DataSyncProviderFactory.GetRepository<ProductGroup>(_injectData);
        var productGroups = new List<ProductGroup>
        {
            ObjProductGroup
        };

        // push realtime details to server
        await productGroupRepo.PushNewListAsync(productGroups);

        // update product group SyncGlobalId to local
        if (productGroupRepo.GetHashCode() > 0)
        {
            await SyncUpdateProductGroup(ObjProductGroup.PGrpId);
        }

        return productGroupRepo.GetHashCode();
    }

    public async Task<bool> SyncProductGroupDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}ProductGroup/GetProductGroupsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}ProductGroup/InsertProductGroupList",
            UpdateUrl = @$"{_configParams.Model.Item2}ProductGroup/UpdateProductGroup"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productGroupRepo = DataSyncProviderFactory.GetRepository<ProductGroup>(_injectData);

        // pull all new product group data
        var pullResponse = await PullProductGroupsServerToClientByRowCount(productGroupRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm();
            return false;
        }

        // push all new product group data
        var sqlQuery = GetProductGroupScript();
        var queryResponse = await QueryUtils.GetListAsync<ProductGroup>(sqlQuery);
        var pgList = queryResponse.List.ToList();
        if (pgList.Count > 0)
        {
            var pushResponse = await productGroupRepo.PushNewListAsync(pgList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public int SaveProductGroupAuditLog(string actionTag)
    {
        var cmdString = @$"
            INSERT INTO AUD.AUDIT_PRODUCTGROUP(PGrpID, GrpName, GrpCode, GMargin, Gprinter, Branch_ID, Company_ID, Status, EnterBy, EnterDate, ModifyAction, ModifyBy, ModifyDate)
            SELECT PGrpID, GrpName, GrpCode, GMargin, Gprinter, Branch_ID, Company_ID, Status, EnterBy, EnterDate,'{actionTag}' ModifyAction,'{ObjGlobal.LogInUser}' ModifyBy,GETDATE() ModifyDate 
            FROM AMS.ProductGroup
            WHERE PGrpID={ObjProductGroup.PGrpId} ";
        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }
    public string GetProductGroupScript(int productGroupId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.ProductGroup";
        cmdString += productGroupId > 0 ? $" WHERE SyncGlobalId IS NULL AND PGrpId= {productGroupId} " : "";
        return cmdString;
    }
    public Task<int> SyncUpdateProductGroup(long productGroupId = 0)
    {
        var commandText = $@"
            UPDATE AMS.ProductGroup SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (productGroupId > 0)
        {
            commandText += $" WHERE PGrpId = '{productGroupId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    // PULL PRODUCT GROUP 
    #region ---------- PULL PRODUCT GROUP ----------

    public async Task<bool> PullProductGroupsServerToClientByRowCount(IDataSyncRepository<ProductGroup> productGroupRepo, int callCount)
    {
        try
        {
            var pullResponse = await productGroupRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = $@"select * from AMS.ProductGroup";
            var allData = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var productGroupData in pullResponse.List)
            {
                ObjProductGroup = productGroupData;

                var alreadyExistData = allData.Select("PGrpId= '" + productGroupData.PGrpId + "'");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (productGroupData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveProductGroup("UPDATE");
                    }
                }
                else
                {
                    var result = SaveProductGroup("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullProductGroupsServerToClientByRowCount(productGroupRepo, callCount);
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
    public DataTable GetProductGroupLedgerDetails(int groupId)
    {
        var cmdString = @$"
			SELECT pg.PGrpId, pg.NepaliDesc, pg.GrpName, pg.GrpCode, pg.GMargin, pg.Gprinter,pg.Branch_ID, pg.Company_Id, pg.Status, pg.EnterBy, pg.EnterDate, pg.SyncBaseId, pg.SyncGlobalId, pg.SyncOriginId, pg.SyncCreatedOn, pg.SyncLastPatchedOn, pg.SyncRowVersion, pg.PurchaseLedgerId, pl.GLName PurchaseLedger, pg.PurchaseReturnLedgerId, pr.GLName PurchaseReturnLedger,pg.SalesLedgerId, sl.GLName SalesLedger,pg.SalesReturnLedgerId, sr.GLName SalesReturnLedger,pg.OpeningStockLedgerId,op.GLName OpeningStockLedger,pg.ClosingStockLedgerId,cl.GLName ClosingStockLedger,pg.StockInHandLedgerId,si.GLName StockInHandLedger
			FROM AMS.ProductGroup pg
				LEFT OUTER JOIN AMS.GeneralLedger cl ON cl.GLID=pg.ClosingStockLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=pg.PurchaseLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger pr ON pr.GLID=pg.PurchaseReturnLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger sl ON sl.GLID=pg.SalesLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger sr ON sr.GLID=pg.SalesReturnLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger op ON op.GLID=pg.OpeningStockLedgerId
				LEFT OUTER JOIN AMS.GeneralLedger si ON si.GLID=pg.StockInHandLedgerId
			WHERE PGrpId='{groupId}';";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // OBJECT FOR THIS FROM 
    public ProductGroup ObjProductGroup { get; set; }
    private IMasterSetup _master;
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}