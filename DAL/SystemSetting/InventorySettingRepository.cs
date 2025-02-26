using DatabaseModule.CloudSync;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.SystemSetting.Interface;
using MrDAL.Utility.Server;
using System;
using System.Threading.Tasks;

namespace MrDAL.SystemSetting;

public class InventorySettingRepository : IInventorySettingRepository
{
    public InventorySettingRepository()
    {
        VmStock = new DatabaseModule.Setup.SystemSetting.InventorySetting();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<String, string, Guid>>();
    }

    public int SaveInventorySetting(string actionTag)
    {
        var cmdString = $@"
			TRUNCATE TABLE AMS.InventorySetting;
			INSERT INTO AMS.InventorySetting (InvId, OPLedgerId, CSPLLedgerId, CSBSLedgerId, NegativeStock, AlternetUnit, CostCenterEnable, CostCenterMandetory, CostCenterItemEnable, CostCenterItemMandetory, ChangeUnit, GodownEnable, GodownMandetory, RemarksEnable, GodownItemEnable, GodownItemMandetory, NarrationEnable, ShortNameWise, BatchWiseQtyEnable, ExpiryDate, FreeQty, GroupWiseFilter, GodownWiseStock)
			VALUES({VmStock.InvId}, ";
        cmdString += VmStock.OPLedgerId > 0 ? $"{VmStock.OPLedgerId}," : "NULL,";
        cmdString += VmStock.OPLedgerId > 0 ? $"{VmStock.CSPLLedgerId}, " : "NULL,";
        cmdString += VmStock.OPLedgerId > 0 ? $"{VmStock.CSBSLedgerId}, " : "NULL,";
        cmdString +=
            $" '{VmStock.NegativeStock}', CAST('{VmStock.AlternetUnit}' AS BIT), CAST('{VmStock.CostCenterEnable}' AS BIT), CAST('{VmStock.CostCenterMandetory}' AS BIT), CAST('{VmStock.CostCenterItemEnable}' AS BIT), CAST('{VmStock.CostCenterItemMandetory}' AS BIT), CAST('{VmStock.ChangeUnit}' AS BIT), CAST('{VmStock.GodownEnable}' AS BIT), CAST('{VmStock.GodownMandetory}' AS BIT), CAST('{VmStock.RemarksEnable}' AS BIT), CAST('{VmStock.GodownItemEnable}' AS BIT), CAST('{VmStock.GodownItemMandetory}' AS BIT), CAST('{VmStock.NarrationEnable}' AS BIT), CAST('{VmStock.ShortNameWise}' AS BIT), CAST('{VmStock.BatchWiseQtyEnable}' AS BIT), CAST('{VmStock.ExpiryDate}' AS BIT), CAST('{VmStock.FreeQty}' AS BIT), CAST('{VmStock.GroupWiseFilter}' AS BIT), CAST('{VmStock.GodownWiseStock}' AS BIT));";
        return SqlExtensions.ExecuteNonTrans(cmdString);
    }

    public string GetInventoryScript(int settingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.InventorySetting i";
        cmdString += settingId > 0 ? $" WHERE i.InvId= {settingId} " : "";
        return cmdString;
    }

    public async Task<bool> PullInventorySettingServerToClientByRowCounts(
        IDataSyncRepository<DatabaseModule.Setup.SystemSetting.InventorySetting> inventoryRepo, int callCount)
    {
        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            return false;
        }

        _injectData.ApiConfig = new SyncApiConfig();
        _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}InventorySetting/GetInventorySettingByCallCount?callCount={callCount}";
        var pullResponse = await inventoryRepo.GetUnSynchronizedDataAsync();
        if (!pullResponse.Success)
        {
            return false;
        }
        else
        {
            var query = GetInventoryScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var settingData in pullResponse.List)
            {
                VmStock = settingData;

                var alreadyExistData = alldata.Select("InvId='" + settingData.InvId + "'");
                if (alreadyExistData.Length > 0)
                {
                    var result = SaveInventorySetting("UPDATE");
                }
                else
                {
                    var result = SaveInventorySetting("SAVE");
                }
            }
        }

        if (pullResponse.IsReCall)
        {
            callCount++;
            await PullInventorySettingServerToClientByRowCounts(inventoryRepo, callCount);
        }

        return true;
    }

    public DatabaseModule.Setup.SystemSetting.InventorySetting VmStock { get; set; }
    public DbSyncRepoInjectData _injectData;
    public InfoResult<ValueModel<string, string, Guid>> _configParams;
}