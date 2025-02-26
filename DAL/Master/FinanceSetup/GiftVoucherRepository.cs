using DatabaseModule.CloudSync;
using DatabaseModule.Master.FinanceSetup;
using MrDAL.Core.Extensions;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.Master.FinanceSetup;

public class GiftVoucherRepository : IGiftVoucherRepository
{
    public GiftVoucherRepository()
    {
        ObjGiftVoucher = new GiftVoucherList();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
        _injectData = new DbSyncRepoInjectData();
    }

    public int SaveGiftVoucherList(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(@"
                INSERT INTO AMS.GiftVoucherList(CardNo, ExpireDate, Description, VoucherType, IssueAmount, IsUsed, BalanceAmount, BillAmount, BranchId, CompanyUnitId, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append("\n VALUES \n");
            cmdString.Append($" ({ObjGiftVoucher.CardNo},");
            cmdString.Append(
                $"'{ObjGiftVoucher.ExpireDate.GetSystemDate()}',N'{ObjGiftVoucher.Description}',N'{ObjGiftVoucher.VoucherType}',");
            cmdString.Append($" {ObjGiftVoucher.IssueAmount},");
            cmdString.Append(ObjGiftVoucher.IsUsed ? $"1," : "0,");
            cmdString.Append($"{ObjGiftVoucher.BalanceAmount},");
            cmdString.Append($"{ObjGiftVoucher.BillAmount},");
            cmdString.Append(ObjGiftVoucher.BranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(ObjGiftVoucher.Status ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID(), " : "NULL, ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? $"'{DateTime.Now.GetSystemDate()}', " : "NULL, ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE(), " : "NULL, ");
            cmdString.Append($"{ObjGiftVoucher.SyncRowVersion.GetDecimal(true)} ); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.GiftVoucherList SET  ");
            cmdString.Append(ObjGiftVoucher.ExpireDate.IsValueExits()
                ? $"ExpireDate = '{ObjGiftVoucher.ExpireDate.GetSystemDate()}',"
                : "ExpireDate = NULL,");
            cmdString.Append(ObjGiftVoucher.Description.IsValueExits()
                ? $"Description = N'{ObjGiftVoucher.Description}',"
                : "Description = NULL,");
            cmdString.Append(ObjGiftVoucher.IssueAmount > 0 ? $"IssueAmount = {ObjGiftVoucher.IssueAmount} ," : "0,");
            cmdString.Append(ObjGiftVoucher.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append(ObjGiftVoucher.VoucherType.IsValueExits()
                ? $"VoucherType = N'{ObjGiftVoucher.VoucherType}',"
                : "VoucherType = 'O',");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {ObjGiftVoucher.SyncRowVersion.GetDecimal(true)}");
            cmdString.Append($" WHERE VoucherId = {ObjGiftVoucher.VoucherId}; ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.GiftVoucherList where VoucherId = {ObjGiftVoucher.VoucherId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe > 0 && ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncGiftVoucherListAsync(actionTag));
        }

        return exe <= 0 ? exe : exe;
    }
    public async Task<int> SyncGiftVoucherListAsync(string actionTag)
    {
        //sync
        try
        {
            _configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
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
                GetUrl = @$"{_configParams.Model.Item2}GiftVoucherList/GetGiftVoucherListsByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}GiftVoucherList/InsertGiftVoucherList",
                UpdateUrl = @$"{_configParams.Model.Item2}GiftVoucherList/UpdateGiftVoucher"
            };


            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);

            var giftVoucherRepo =
                DataSyncProviderFactory.GetRepository<GiftVoucherList>(_injectData);

            var giftVouchers = new List<GiftVoucherList>
            {
                ObjGiftVoucher
            };

            // push realtime gift voucher details to server
            await giftVoucherRepo.PushNewListAsync(giftVouchers);

            // update gift voucher SyncGlobalId to local
            if (giftVoucherRepo.GetHashCode() > 0)
            {
                await SyncUpdateGiftVoucher(ObjGiftVoucher.VoucherId);
            }
            return giftVoucherRepo.GetHashCode();
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public Task<int> SyncUpdateGiftVoucher(int voucherId)
    {
        var commandText = $@"
            UPDATE AMS.GiftVoucherList SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (voucherId > 0)
        {
            commandText += $" WHERE VoucherId = {voucherId}";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    public async Task<bool> PullCurrencyServerToClientByRowCounts(IDataSyncRepository<GiftVoucherList> giftRepository, int callCount)
    {
        try
        {

            var pullResponse = await giftRepository.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetGiftVoucherScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var gift in pullResponse.List)
            {
                ObjGiftVoucher = gift;

                var alreadyExistData = alldata.Select("VoucherId= " + gift.VoucherId + "");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (gift.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveGiftVoucherList("UPDATE");
                    }
                }
                else
                {

                    var result = SaveGiftVoucherList("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullCurrencyServerToClientByRowCounts(giftRepository, callCount);
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public string GetGiftVoucherScript(int voucherId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.GiftVoucherList g ";
        cmdString += voucherId > 0 ? $"WHERE g.SyncGlobalId IS NULL AND g.VoucherId= {voucherId} " : "";
        return cmdString;
    }

    // RETURN VALUE IN DATA TABLE
    public DataTable GetGiftVoucherNumberInformation(int selectedId)
    {
        var cmdString = $"SELECT  * FROM AMS.GiftVoucherList WHERE VoucherId= {selectedId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // OBJECT FOR THIS FORM
    public GiftVoucherList ObjGiftVoucher { get; set; }
    private DbSyncRepoInjectData _injectData;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}