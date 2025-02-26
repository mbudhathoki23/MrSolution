using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Interface;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Domains.Restro.Master;

public class TableMasterRepository : ITableMasterRepository
{
    public TableMasterRepository()
    {
        Table = new TableMaster();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
    }

    public int SaveTable(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append("INSERT INTO AMS.TableMaster(TableId, TableName, TableCode, FloorId, Branch_ID, Company_Id, TableStatus, TableType, IsPrePaid, Status, EnterBy, EnterDate, Printed, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append($" VALUES ");
            cmdString.Append($" ({Table.TableId},");
            cmdString.Append(Table.TableName.Contains("'") ? $"CONCAT(N'{Table.TableName.Replace("' S", "")}','''','S')," : $"N'{Table.TableName}',");
            cmdString.Append($"N'{Table.TableCode}',");
            cmdString.Append(Table.FloorId > 0 ? $" {Table.FloorId} ," : "NULL, ");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(Table.TableStatus.IsValueExits() ? $"N'{Table.TableStatus}'," : "NULL,");
            cmdString.Append(Table.TableType.IsValueExits() ? $"N'{Table.TableType}'," : "NULL,");
            cmdString.Append(Table.IsPrePaid ? "1," : "0,");
            cmdString.Append(Table.Status ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(Table.Printed.IsValueExits() ? $"N'{Table.Printed}', " : "NULL");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID(), " : "NULL, ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? $"'{DateTime.Now.GetSystemDate()}', " : "NULL, ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE(), " : "NULL, ");
            cmdString.Append($"{Table.SyncRowVersion.GetDecimal(true)} ); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.TableMaster SET  ");
            cmdString.Append(Table.TableName.IsValueExits() ? $"TableName = N'{Table.TableName}'," : "TableName = NULL,");
            cmdString.Append(Table.TableCode.IsValueExits() ? $"TableCode = N'{Table.TableCode}'," : "TableCode = NULL,");
            cmdString.Append(Table.FloorId > 0 ? $"FloorId = {Table.FloorId} ," : "NULL,");
            cmdString.Append(Table.TableStatus.IsValueExits() ? $"TableStatus = N'{Table.TableStatus}'," : "NULL,");
            cmdString.Append(Table.TableType.IsValueExits() ? $"TableType = N'{Table.TableType}'," : "NULL,");
            cmdString.Append(Table.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append(Table.IsPrePaid ? "IsPrepaid = 1," : "IsPrepaid = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {Table.SyncRowVersion.GetDecimal(true)}");
            cmdString.Append($" WHERE TableId = {Table.TableId}; ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.TableMaster where TableId = {Table.TableId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncTableMasterAsync(actionTag));
        }

        return exe;
    }
    public async Task<int> SyncTableMasterAsync(string actionTag)
    {
        try
        {
            _configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!_configParams.Success || _configParams.Model.Item1 == null)
            {
                return 1;
            }

            _injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = _configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUri = @$"{_configParams.Model.Item2}TableMaster/DeleteTableMasterAsync?id=" + Table.TableId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}TableMaster/GetTableMasterById",
                InsertUrl = @$"{_configParams.Model.Item2}TableMaster/InsertTableMaster",
                UpdateUrl = @$"{_configParams.Model.Item2}TableMaster/UpdateTableMaster",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var tableMasterRepo =
                DataSyncProviderFactory.GetRepository<TableMaster>(DataSyncManager.GetGlobalInjectData());

            var tm = new TableMaster
            {
                TableId = Table.TableId,
                TableName = Table.TableName,
                TableCode = Table.TableCode,
                FloorId = Table.FloorId,
                Branch_ID = ObjGlobal.SysBranchId,
                Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                TableStatus = Table.TableStatus.IsValueExits() ? Table.TableStatus : null,
                TableType = Table.TableType.IsValueExits() ? Table.TableType : null,
                IsPrePaid = Table.IsPrePaid,
                Status = Table.Status,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                Printed = Table.Printed.IsValueExits() ? Table.Printed : null,
                SyncRowVersion = Table.SyncRowVersion
            };

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await tableMasterRepo?.PushNewAsync(tm),
                "UPDATE" => await tableMasterRepo?.PutNewAsync(tm),
                "DELETE" => await tableMasterRepo?.DeleteNewAsync(),
                _ => await tableMasterRepo?.PushNewAsync(tm)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    public string GetMasterTable(int selectedId = 0)
    {
        var cmdString = "SELECT * FROM AMS.TableMaster t ";
        cmdString += selectedId > 0 ? $"WHERE c.SyncGlobalId IS NULL AND c.TableId= {selectedId} " : "";

        return cmdString;
    }

    public async Task<bool> PullTableMasterServerToClientByRowCounts(IDataSyncRepository<TableMaster> tableMasterRepository, int callCount)
    {
        try
        {
            var pullResponse = await tableMasterRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }
            else
            {
                var query = GetMasterTable();
                var alldata = SqlExtensions.ExecuteDataSetSql(query);

                foreach (var table in pullResponse.List)
                {
                    Table = table;

                    var alreadyExistData = alldata.Select("TableId= " + table.TableId + "");
                    if (alreadyExistData.Length > 0)
                    {
                        //get SyncRowVersion from client database table
                        int ClientSyncRowVersionId = 1;
                        ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                        //update only server SyncRowVersion is greater than client database while data pulling from server
                        if (table.SyncRowVersion > ClientSyncRowVersionId)
                        {
                            var result = SaveTable("UPDATE");
                        }
                    }
                    else
                    {
                        var result = SaveTable("SAVE");
                    }
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullTableMasterServerToClientByRowCounts(tableMasterRepository, callCount);
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    // OBJECT
    public TableMaster Table { get; set; }
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private DbSyncRepoInjectData _injectData;
}