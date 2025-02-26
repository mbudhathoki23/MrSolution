using ClosedXML.Excel;
using Dapper;
using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.OpeningMaster;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.OpeningMaster;

public class ProductOpeningRepository : IProductOpeningRepository
{
    // INSERT UPDATE DELETE
    public async Task<NonQueryResult> UpdateProductOpeningImportAsync(IList<ImportProductOpeningEModel> rows, int branchId, string username)
    {
        var result = new NonQueryResult();
        var curDateTime = DateTime.Now;

        try
        {
            using var conn = GetConnection.ReturnConnection();
            using var trans = conn.BeginTransaction();
            try
            {
                foreach (var model in rows)
                {
                    //                var product = await conn.QueryFirstOrDefaultAsync<DatabaseModule.DataEntry.OpeningMaster.ProductOpening>(
                    //                    "SELECT * FROM AMS.ProductOpening WHERE Product_Id = @ProductId ",
                    //                    new { ProductId = model.ProductId }, trans);

                    //                if (product != null)
                    //                {
                    //                    await conn.ExecuteAsync(@"
                    //UPDATE AMS.ProductOpening SET
                    //	Product_Id = @Product_Id,
                    //	Qty = @Qty,
                    //	QtyUnit = @QtyUnit,
                    //	Rate = @Rate,
                    //	Amount = @Amount,
                    //	CBranch_Id = @CBranch_Id
                    //    WHERE OpeningId = @OpeningId",
                    //                        new
                    //                        {
                    //                            Product_Id = model.ProductId,
                    //                            Qty = model.Quantity,
                    //                            QtyUnit = model.UId,
                    //                            Rate = model.Rate,
                    //                            Amount = model.Amount,
                    //                            CBranch_Id = branchId,
                    //                            OpeningId = product.OpeningId
                    //                        }, trans);
                    //                }
                    //                else
                    //                {
                    var newId = await conn.NewBigIntIdAsync("AMS.ProductOpening", "OpeningId", trans);
                    await conn.InsertAsync(new DatabaseModule.DataEntry.OpeningMaster.ImportProductOpening
                    {
                        OpeningId = Convert.ToInt32(newId),
                        Voucher_No = "DVN00012",
                        Serial_No = 0,
                        OP_Date = curDateTime,
                        OP_Miti = "I",
                        Product_Id = model.ProductId,
                        Godown_Id = model.GodownId == 0 ? null : model.GodownId,
                        Cls1 = null,
                        Cls2 = null,
                        Cls3 = null,
                        Cls4 = null,
                        Currency_Id = ObjGlobal.SysCurrencyId,
                        Currency_Rate = Convert.ToDecimal(ObjGlobal.SysCurrencyRate),
                        AltQty = 0,
                        AltUnit = null,
                        Qty = model.Quantity,
                        QtyUnit = model.UId,
                        Rate = model.Rate,
                        LocalRate = model.Rate,
                        Amount = model.Amount,
                        LocalAmount = model.Amount,
                        IsReverse = true,
                        CancelRemarks = null,
                        CancelBy = null,
                        CancelDate = curDateTime,
                        Remarks = null,
                        Enter_By = ObjGlobal.LogInUserId.ToString(),
                        Enter_Date = curDateTime,
                        Reconcile_By = null,
                        Reconcile_Date = null,
                        CBranch_Id = branchId,
                        CUnit_Id = null,
                        FiscalYearId = ObjGlobal.SysFiscalYearId,
                        SyncBaseId = Guid.NewGuid(),
                        SyncGlobalId = Guid.NewGuid(),
                        SyncOriginId = Guid.NewGuid(),
                        SyncCreatedOn = curDateTime,
                        SyncLastPatchedOn = curDateTime,
                        SyncRowVersion = 1,
                    }, trans, 300, null, "AMS.ProductOpening");
                    //}
                }
                // if everything saved fine; commit the transaction
                trans.Commit();
                result.Completed = result.Value = true;
            }
            catch (Exception e)
            {
                result = e.ToNonQueryErrorResult(e.StackTrace);
            }
        }
        catch (Exception e)
        {
            result = e.ToNonQueryErrorResult(e.StackTrace);
        }

        return result;
    }
    public int SaveProductOpeningSetup(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (!actionTag.Equals("SAVE"))
        {
            AuditLogProductOpening(actionTag);
        }
        if (actionTag.Equals("REVERSE"))
        {
            cmdString.Append($@" 
                DELETE AMS.StockDetails WHERE Voucher_No='{VmProductOpening.Voucher_No}';
                UPDATE AMS.ProductOpening SET IsReverse = 1 WHERE Voucher_No='{VmProductOpening.Voucher_No}'; ");
        }
        if (actionTag is "UPDATE" or "DELETE")
        {
            cmdString.Append($" DELETE AMS.ProductOpening WHERE Voucher_No='{VmProductOpening.Voucher_No}'; \n");
            cmdString.Append($" DELETE AMS.StockDetails WHERE Voucher_No='{VmProductOpening.Voucher_No}'; \n");
        }
        if (!actionTag.Equals("REVERSE"))
        {

            if (!_tagStrings.Contains(actionTag))
            {
                if (Details.Count > 0)
                {
                    foreach (var item in Details)
                    {
                        var index = VmProductOpening.Details.IndexOf(item);

                        cmdString.Append(@" 
                            INSERT INTO AMS.ProductOpening(OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                        cmdString.Append(" \n VALUES  \n");
                        cmdString.Append($" ({item.OpeningId}, "); // OpeningId
                        cmdString.Append($" N'{item.Voucher_No}', "); //VoucherNo
                        cmdString.Append($" {item.Serial_No}, "); //SerialNo
                        cmdString.Append($" '{item.OP_Date.GetSystemDate()}', N'{item.OP_Miti}', "); // Miti
                        cmdString.Append($" {item.Product_Id}, "); //ProductId
                        cmdString.Append(item.Godown_Id > 0 ? $" {item.Godown_Id}, " : "Null, "); //GoDownId
                        cmdString.Append(item.Cls1 > 0 ? $" {item.Cls1}, " : "Null ,"); //Cls1
                        cmdString.Append(item.Cls2 > 0 ? $" {item.Cls2} ," : "Null, "); //Cls2
                        cmdString.Append(item.Cls3 > 0 ? $" {item.Cls3} ," : "Null, "); //Cls3
                        cmdString.Append(item.Cls4 > 0 ? $" {item.Cls4} ," : "Null, "); //Cls4
                        cmdString.Append(item.Currency_Id > 0 ? $" {item.Currency_Id}, " : $" {ObjGlobal.SysCurrencyId} ,"); //CurrencyId
                        cmdString.Append(item.Currency_Rate > 0 ? $" {item.Currency_Rate}," : "1,"); //CurrencyRate
                        cmdString.Append($" {item.AltQty}, "); //AltQty
                        cmdString.Append(item.AltUnit > 0 ? $" {item.AltUnit}, " : "Null, "); //AltUnit
                        cmdString.Append($" {item.Qty}, "); //Qty
                        cmdString.Append(item.QtyUnit > 0 ? $" {item.QtyUnit}, " : "Null, "); //QtyUnit
                        cmdString.Append($" {item.Rate}, "); //Rate
                        cmdString.Append($" {item.LocalRate}, "); //LocalRate
                        cmdString.Append($" {item.Amount}, "); //Amount
                        cmdString.Append($" {item.LocalAmount}, "); //LocalAmount
                        cmdString.Append($" CAST('{item.IsReverse}' AS BIT), '{item.CancelRemarks}', N'{item.CancelBy}', '{item.CancelDate.GetSystemDate()}', '{item.Remarks}', N'{item.Enter_By}', '{item.Enter_Date.GetSystemDate()}', N'{item.Reconcile_By}', '{item.Reconcile_Date.GetSystemDate()}', ");
                        cmdString.Append($" {item.CBranch_Id}, ");
                        cmdString.Append(item.CUnit_Id > 0 ? $" {item.CUnit_Id} ," : "Null, ");
                        cmdString.Append($" {ObjGlobal.SysFiscalYearId},Null, Null, Null, Null, Null, {item.SyncRowVersion.GetDecimal(true)}");
                        cmdString.Append(index == VmProductOpening.Details.Count - 1 ? "); \n" : "), \n");
                    }
                }
                else
                {
                    cmdString.Append(@" 
                        INSERT INTO AMS.ProductOpening(OpeningId, Voucher_No, Serial_No, OP_Date, OP_Miti, Product_Id, Godown_Id, Cls1, Cls2, Cls3, Cls4, Currency_Id, Currency_Rate, AltQty, AltUnit, Qty, QtyUnit, Rate, LocalRate, Amount, LocalAmount, IsReverse, CancelRemarks, CancelBy, CancelDate, Remarks, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
                    cmdString.Append(" \n VALUES  \n");
                    cmdString.Append($" ({VmProductOpening.OpeningId}, "); // OpeningId
                    cmdString.Append($" N'{VmProductOpening.Voucher_No}', "); //VoucherNo
                    cmdString.Append($" {VmProductOpening.Serial_No}, "); //SerialNo
                    cmdString.Append($" '{VmProductOpening.OP_Date.GetSystemDate()}', N'{VmProductOpening.OP_Miti}', "); // Miti
                    cmdString.Append($" {VmProductOpening.Product_Id}, "); //ProductId
                    cmdString.Append(VmProductOpening.Godown_Id > 0 ? $" {VmProductOpening.Godown_Id}, " : "Null, "); //GoDownId
                    cmdString.Append(VmProductOpening.Cls1 > 0 ? $" {VmProductOpening.Cls1}, " : "Null ,"); //Cls1
                    cmdString.Append(VmProductOpening.Cls2 > 0 ? $" {VmProductOpening.Cls2} ," : "Null, "); //Cls2
                    cmdString.Append(VmProductOpening.Cls3 > 0 ? $" {VmProductOpening.Cls3} ," : "Null, "); //Cls3
                    cmdString.Append(VmProductOpening.Cls4 > 0 ? $" {VmProductOpening.Cls4} ," : "Null, "); //Cls4
                    cmdString.Append(VmProductOpening.Currency_Id > 0 ? $" {VmProductOpening.Currency_Id}, " : $" {ObjGlobal.SysCurrencyId} ,"); //CurrencyId
                    cmdString.Append(VmProductOpening.Currency_Rate > 0 ? $" {VmProductOpening.Currency_Rate}," : "1,"); //CurrencyRate
                    cmdString.Append($" {VmProductOpening.AltQty}, "); //AltQty
                    cmdString.Append(VmProductOpening.AltUnit > 0 ? $" {VmProductOpening.AltUnit}, " : "Null, "); //AltUnit
                    cmdString.Append($" {VmProductOpening.Qty}, "); //Qty
                    cmdString.Append(VmProductOpening.QtyUnit > 0 ? $" {VmProductOpening.QtyUnit}, " : "Null, "); //QtyUnit
                    cmdString.Append($" {VmProductOpening.Rate}, "); //Rate
                    cmdString.Append($" {VmProductOpening.LocalRate}, "); //LocalRate
                    cmdString.Append($" {VmProductOpening.Amount}, "); //Amount
                    cmdString.Append($" {VmProductOpening.LocalAmount}, "); //LocalAmount
                    cmdString.Append($" CAST('{VmProductOpening.IsReverse}' AS BIT), '{VmProductOpening.CancelRemarks}', N'{VmProductOpening.CancelBy}', '{VmProductOpening.CancelDate.GetSystemDate()}', '{VmProductOpening.Remarks}', N'{VmProductOpening.Enter_By}', '{VmProductOpening.Enter_Date.GetSystemDate()}', N'{VmProductOpening.Reconcile_By}', '{VmProductOpening.Reconcile_Date.GetSystemDate()}', ");
                    cmdString.Append($" {VmProductOpening.CBranch_Id}, ");
                    cmdString.Append(VmProductOpening.CUnit_Id > 0 ? $" {VmProductOpening.CUnit_Id} ," : "Null, ");
                    cmdString.Append($" {ObjGlobal.SysFiscalYearId},Null, Null, Null, Null, Null, {VmProductOpening.SyncRowVersion.GetDecimal(true)} )");
                }
            }
        }
        var exe = SqlExtensions.ExecuteNonTrans(cmdString);
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncProductOpeningSetupAsync(actionTag));
            if (VmProductOpening.ProductBatch != null && VmProductOpening.ProductBatch.Rows.Count > 0)
            {
                Task.Run(() => SyncProductAddInfoAsync(actionTag));
            }
        }

        ProductOpeningStockPosting();

        return exe;
    }
    private int AuditLogProductOpening(string actionTag)
    {
        var cmdString = @$"
		    INSERT INTO [AUD].[AUDIT_PRODUCT_OPENING]([Voucher_No], [Serial_No], [OP_Date], [OP_Miti], [Product_Id], [Godown_Id], [Cls1], [Cls2], [Cls3], [Cls4], [Currency_Id], [Currency_Rate], [AltQty], [AltUnit], [Qty], [QtyUnit], [Rate], [LocalRate], [Amount], [LocalAmount], [Enter_By], [Enter_Date], [Reconcile_By], [Reconcile_Date], [CBranch_Id], [CUnit_Id], [FiscalYearId], [ModifyAction], [ModifyBy], [ModifyDate])
            SELECT [Voucher_No], [Serial_No], [OP_Date], [OP_Miti], [Product_Id], [Godown_Id], [Cls1], [Cls2], [Cls3], [Cls4], [Currency_ID], [Currency_Rate], [AltQty], [AltUnit], [Qty], [QtyUnit], [Rate], [LocalRate], [Amount], [LocalAmount], [Enter_By], [Enter_Date], [Reconcile_By], [Reconcile_Date], [CBranch_Id], [CUnit_Id], [FiscalYearId], '{actionTag}', '{ObjGlobal.LogInUser}', GETDATE()
            FROM AMS.ProductOpening
            WHERE Voucher_No='{VmProductOpening.Voucher_No}';

            INSERT INTO AMS.ProductAddInfo(Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT Module, VoucherNo, VoucherType, ProductId, Sno, SizeNo, SerialNo, BatchNo, ChasisNo, EngineNo, VHModel, VHColor, MFDate, ExpDate, Mrp, Rate, AltQty, Qty, BranchId, CompanyUnitId, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM AMS.ProductAddInfo
            WHERE VoucherNo = '{VmProductOpening.Voucher_No}' AND Module ='OB'; ";
        return SqlExtensions.ExecuteNonTrans(cmdString);
    }
    public async Task<int> SyncProductOpeningSetupAsync(string actionTag)
    {
        //sync
        string deleteUrl;
        string getUrl;
        string insertUrl;
        string updateUrl;
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                return 1;

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            if (VmProductOpening.GetView != null)
            {
                getUrl = @$"{configParams.Model.Item2}ProductOpening/GetProductOpeningById";
                insertUrl = @$"{configParams.Model.Item2}ProductOpening/InsertProductOpeningList";
                updateUrl = @$"{configParams.Model.Item2}ProductOpening/UpdateProductOpeningList";
                deleteUrl = $@"{configParams.Model.Item2}ProductOpening/DeleteProductOpeningAsync?id={VmProductOpening.OpeningId}";
            }
            else
            {
                getUrl = @$"{configParams.Model.Item2}ProductOpening/GetProductOpeningById";
                insertUrl = @$"{configParams.Model.Item2}ProductOpening/InsertProductOpening";
                updateUrl = @$"{configParams.Model.Item2}ProductOpening/UpdateProductOpening";
                deleteUrl = $@"{configParams.Model.Item2}ProductOpening/DeleteProductOpeningAsync?id={VmProductOpening.OpeningId}";
            }

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var productOpeningRepo = DataSyncProviderFactory.GetRepository<ProductOpening>(injectData);

            // push realtime product opening details to server
            await productOpeningRepo.PushNewListAsync(VmProductOpening.Details);

            // update product opening SyncGlobalId to local
            if (productOpeningRepo.GetHashCode() > 0)
            {
                await SyncUpdateProductOpening(VmProductOpening.OpeningId);
            }

            return productOpeningRepo.GetHashCode();

        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    public Task<int> SyncUpdateProductOpening(int openingId)
    {
        var commandText = $@"
            UPDATE AMS.ProductOpening SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (openingId > 0)
        {
            commandText += $" WHERE OpeningId = '{openingId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }
    public async Task<int> SyncProductAddInfoAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
                //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
                //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //configParams.ShowErrorDialog();
                return 1;

            var injectData = new DbSyncRepoInjectData
            {
                ExternalConnectionString = null,
                DateTime = DateTime.Now,
                LocalOriginId = configParams.Model.Item1,
                LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
                Username = ObjGlobal.LogInUser,
                LocalConnectionString = GetConnection.ConnectionString,
                LocalBranchId = ObjGlobal.SysBranchId,
                ApiConfig = DataSyncHelper.GetConfig()
            };
            var deleteUrl = string.Empty;
            var getUrl = string.Empty;
            var insertUrl = string.Empty;
            var updateUrl = string.Empty;

            getUrl = @$"{configParams.Model.Item2}ProductAddInfo/GetProductAddInfoById";
            insertUrl = @$"{configParams.Model.Item2}ProductAddInfo/InsertProductAddInfoList";
            updateUrl = @$"{configParams.Model.Item2}ProductAddInfo/UpdateProductAddInfoList";
            deleteUrl = @$"{configParams.Model.Item2}ProductAddInfo/DeleteProductAddInfoAsync?id=" +
                        VmProductOpening.OpeningId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl,
                InsertUrl = insertUrl,
                UpdateUrl = updateUrl,
                DeleteUrl = deleteUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var productAddInfoRepo =
                DataSyncProviderFactory.GetRepository<ProductAddInfo>(DataSyncManager.GetGlobalInjectData());

            var poList = (from DataRow dr in VmProductOpening.ProductBatch.Rows
                select new ProductAddInfo
                {
                    Module = "OB",
                    VoucherNo = VmProductOpening.Voucher_No,
                    VoucherType = "I",
                    ProductId = dr["ProductId"].GetLong(),
                    SerialNo = dr["ProductSno"].GetString(),
                    SizeNo = null,
                    BatchNo = dr["BatchNo"].IsValueExits() ? dr["BatchNo"].GetString() : null,
                    MFDate = Convert.ToDateTime(dr["MfDate"].GetSystemDate()),
                    ExpDate = Convert.ToDateTime(dr["ExpDate"].GetSystemDate()),
                    Mrp = dr["MRP"].GetDecimal(),
                    Rate = dr["Rate"].GetDecimal(),
                    AltQty = 0,
                    Qty = dr["Qty"].GetDecimal(),
                    BranchId = ObjGlobal.SysBranchId,
                    CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                    EnterBy = ObjGlobal.LogInUser,
                    EnterDate = DateTime.Now,
                    SyncRowVersion = VmProductOpening.SyncRowVersion
                }).ToList();

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await productAddInfoRepo?.PushNewListAsync(poList)!,
                "UPDATE" => await productAddInfoRepo?.PutNewListAsync(poList)!,
                "DELETE" => await productAddInfoRepo?.DeleteNewAsync()!,
                _ => await productAddInfoRepo?.PushNewListAsync(poList)!
            };

            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }
    public int ProductOpeningStockPosting()
    {
        var cmdString = @"Delete from AMS.StockDetails Where Module in ('POB','N','OB')";
        cmdString += VmProductOpening.Voucher_No.IsValueExits() ? $@" AND Voucher_No='{VmProductOpening.Voucher_No}'" : " ";
        cmdString += @"
			INSERT INTO AMS.StockDetails(Module, Voucher_No, Serial_No, PurRefVno, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_Id, Subledger_Id, Agent_Id, Department_Id1, Department_Id2, Department_Id3, Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id, CostCenter_Id, AltQty, AltUnit_Id, Qty, Unit_Id, AltStockQty, StockQty, FreeQty, FreeUnit_Id, StockFreeQty, ConvRatio, ExtraFreeQty, ExtraFreeUnit_Id, ExtraStockFreeQty, Rate, BasicAmt, TermAmt, NetAmt, BillTermAmt, TaxRate, TaxableAmt, DocVal, ReturnVal, StockVal, AddStockVal, PartyInv, EntryType, AuthBy, AuthDate, RecoBy, RecoDate, Counter_Id, RoomNo, EnterBy, EnterDate, Adj_Qty, Adj_VoucherNo, Adj_Module, SalesRate, Branch_Id, CmpUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT 'POB' Module, Voucher_No, Serial_No,NULL PurRefVno,po.OP_Date Voucher_Date,po.OP_Miti Voucher_Miti,po.Enter_Date Voucher_Time,NULL Ledger_Id,NULL Subledger_Id,NULL Agent_Id,po.Cls1 Department_Id1,po.Cls2 Department_Id2,po.Cls3 Department_Id3,po.Cls4 Department_Id4, Currency_Id, Currency_Rate, Product_Id, Godown_Id,NULL CostCenter_Id, AltQty,po.AltUnit AltUnit_Id, Qty,po.QtyUnit Unit_Id,po.AltQty AltStockQty,po.Qty StockQty,0 FreeQty,NULL FreeUnit_Id,0 StockFreeQty,0 ConvRatio,0 ExtraFreeQty,NULL ExtraFreeUnit_Id,0 ExtraStockFreeQty, Rate,po.Amount BasicAmt, 0 TermAmt,po.Amount NetAmt,0 BillTermAmt,0 TaxRate,0 TaxableAmt, po.LocalAmount DocVal,0 ReturnVal,po.LocalAmount StockVal,0 AddStockVal,NULL PartyInv,'I' EntryType,NULL AuthBy,NULL AuthDate,NULL RecoBy,NULL RecoDate,NULL Counter_Id,NULL RoomNo,po.Enter_By EnterBy,po.Enter_Date EnterDate,0 Adj_Qty,NULL Adj_VoucherNo,NULL Adj_Module,0 SalesRate,po.CBranch_Id Branch_Id,po.CUnit_Id CmpUnit_Id, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
            FROM AMS.ProductOpening AS po  ";
        cmdString += VmProductOpening.Voucher_No.IsValueExits() ? $@" WHERE Voucher_No='{VmProductOpening.Voucher_No}'" : " ";
        return SqlExtensions.ExecuteNonQuery(cmdString);
    }
    // RETURN VALUE IN DATA TABLE
    public DataSet GetProductOpeningVoucherDetails(string voucherNo)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
            SELECT Voucher_No, OP_Date, OP_Miti, DName, Cls1, Cls2, Cls3, Cls4, Ccode, Currency_Id, Currency_Rate,Remarks
            FROM AMS.ProductOpening AS OP
	             LEFT OUTER JOIN AMS.Department AS D ON D.DId = OP.Cls1
	             LEFT OUTER JOIN AMS.Currency AS C ON C.CId = OP.Currency_Id
            WHERE OP.Voucher_No ='{voucherNo}' AND ISNULL(OP.IsReverse,0) = 0
            GROUP BY Voucher_No, OP_Date, OP_Miti, DName, Cls1, Cls2, Cls3, Cls4, Ccode, Currency_Id, Currency_Rate,Remarks
            ORDER BY Voucher_No

            SELECT Voucher_No, Serial_No, OP_Date, OP_Miti, P.PShortName, PName, Product_Id, GName, Godown_Id, DName, Cls2, Cls3, Cls4, Ccode, Currency_Rate, AltQty, AltUnit, ALTU.UnitCode AS AltUnitCode, Qty, U.UnitCode AS QtyUnitCode, QtyUnit, Rate, LocalRate, Amount, LocalAmount, Enter_By, Enter_Date, Reconcile_By, Reconcile_Date, CBranch_Id, CUnit_Id
            FROM AMS.ProductOpening AS OP
	             INNER JOIN AMS.Product AS P ON P.PID = OP.Product_Id
	             LEFT OUTER JOIN AMS.Department AS D ON D.DId = OP.Cls1
	             LEFT OUTER JOIN AMS.Currency AS C ON C.CId = OP.Currency_Id
	             LEFT OUTER JOIN AMS.Godown AS G ON G.GID = OP.Godown_Id
	             LEFT OUTER JOIN AMS.ProductUnit AS ALTU ON ALTU.UID = OP.AltUnit
	             LEFT OUTER JOIN AMS.ProductUnit AS U ON U.UID = OP.QtyUnit
            WHERE OP.Voucher_No ='{voucherNo}' AND ISNULL(OP.IsReverse,0) = 0
            ORDER BY OP.Voucher_No, OP.Serial_No;	 	 ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString());
    }
    public string GetProductOpening(int openingId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.ProductOpening ";
        cmdString += openingId > 0 ? $" WHERE OpeningId IS NULL AND OpeningId.= {openingId} " : "";
        return cmdString;
    }
    // OBJECT FOR THIS FORM
    public ProductOpening VmProductOpening { get; set; } = new();
    public List<ProductOpening> Details { get; set; } = new();
    private readonly string[] _tagStrings = { "DELETE", "REVERSE" };
    public DataTable ReadExcelFile(string path, string sheetName)
    {
        var r = 2;
        var firstRow = true;
        using var table = new DataTable();
        using var workBook = new XLWorkbook(path);
        var workSheet = workBook.Worksheet(1);
        try
        {
            foreach (var row in workSheet.Rows())
                if (firstRow)
                {
                    foreach (var cell in row.Cells()) table.Columns.Add(cell.Value.GetString());
                    firstRow = false;
                }
                else
                {
                    table.Rows.Add();
                    for (var c = 1; c < table.Columns.Count + 1; c++)
                        table.Rows[table.Rows.Count - 1][c - 1] = workSheet.Cell(r, c).Value.ToString();
                    r++;
                }
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            return null;
        }

        return table;
    }
    public async Task<ListResult<IntLongValueModel>> CreateAndFetchProductAsync(int branchId, IList<string> pName, string username)
    {
        var result = new ListResult<IntLongValueModel>();
        var list = new List<IntLongValueModel>();
        try
        {
            using var conn = GetConnection.ReturnConnection();
            using var trans = conn.BeginTransaction();
            try
            {
                foreach (var PName in pName)
                {
                    var cmdString =
                        "SELECT * FROM AMS.Product WHERE PName = @PName AND Branch_ID = @pBranchId ";
                    var uom = await conn.QueryFirstOrDefaultAsync<Product>(cmdString, new
                    {
                        PName = PName,
                        pBranchId = branchId
                    }, trans);
                    if (uom == null)
                    {
                        var newProductId = await conn.ExecuteScalarAsync<int>(@"
							DECLARE @maxId INT = (Select Isnull(Max(PId),0)+1 PId from AMS.Product )
							INSERT INTO AMS.Product (PID, NepaliDesc, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, 
                                    PSerialno, PSizewise, PBatchwise, PVehicleWise, PublicationWise, PBuyRate, AltSalesRate, PSalesRate, PMargin1, TradeRate, PMargin2,
                                    PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening,
                                    PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, 
                                    BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) VALUES
                                    (@maxId, @PName, @PName, @PName, @PName, 'I', 'FG', '3', null, 0, 0, 'FIFO', 'false', 'false', 'false', 'false',
                                    'false', 0, 0, 0, 0, 0, 0, 0, null, null, 0, 0, 0, null, null, null, null,
                                    @pBranchId, null, null, null, null, null, null, null, null, null, @pUser, @pDate, 0, 'true', null, null, null, null, 
                                    null, 0, 0, null, null, null, null, null, null, 
                                    null, null, null, 1)
								SELECT @maxId ", new
                        {
                            PName = PName,
                            pBranchId = branchId,
                            pUser = username,
                            pDate = DateTime.Now
                        }, trans);

                        if (newProductId == 0)
                        {
                            continue;
                        }
                        list.Add(new IntLongValueModel(newProductId, PName));
                    }
                    else
                    {
                        list.Add(new IntLongValueModel(uom.PID, PName));
                    }
                }
                trans.Commit();
                result.Success = true;
                result.List = list;
            }
            catch (Exception e)
            {
                result = e.ToListErrorResult<IntLongValueModel>(this);
            }
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<IntLongValueModel>(this);
        }
        return result;
    }
    public async Task<ListResult<IntValueModel>> CreateAndFetchUnitsAsync(int branchId, IList<string> units, string username)
    {
        var result = new ListResult<IntValueModel>();
        var list = new List<IntValueModel>();
        try
        {
            using var conn = GetConnection.ReturnConnection();
            using var trans = conn.BeginTransaction();
            try
            {
                foreach (var unit in units)
                {
                    var cmdString =
                        "SELECT * FROM AMS.ProductUnit WHERE UnitName = @pUnit AND Branch_ID = @pBranchId ";
                    var uom = await conn.QueryFirstOrDefaultAsync<ProductUnit>(cmdString, new
                    {
                        pUnit = unit,
                        pBranchId = branchId
                    }, trans);
                    if (uom == null)
                    {
                        var newUnitId = await conn.ExecuteScalarAsync<int>(@"
							DECLARE @maxId INT = (Select Isnull(Max(UId),0)+1 UId from AMS.ProductUnit )
							INSERT INTO AMS.ProductUnit
								(UID, UnitName, UnitCode, Branch_ID, EnterBy, EnterDate, Status, IsDefault, SyncRowVersion)
								VALUES (@maxId, @pUnit, @pUnit, @pBranchId, @pUser, @pDate, 1, 1, 1)
								SELECT @maxId ", new
                        {
                            pUnit = unit,
                            pBranchId = branchId,
                            pUser = username,
                            pDate = DateTime.Now
                        }, trans);

                        if (newUnitId == 0)
                        {
                            continue;
                        }
                        list.Add(new IntValueModel(newUnitId, unit));
                    }
                    else
                    {
                        list.Add(new IntValueModel(uom.UID, unit));
                    }
                }
                trans.Commit();
                result.Success = true;
                result.List = list;
            }
            catch (Exception e)
            {
                result = e.ToListErrorResult<IntValueModel>(this);
            }
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<IntValueModel>(this);
        }
        return result;
    }
    public async Task<ListResult<IntValueModel>> CreateAndFetchGoDownAsync(int branchId, IList<string> gName, string username)
    {
        var result = new ListResult<IntValueModel>();
        var list = new List<IntValueModel>();
        try
        {
            using var conn = GetConnection.ReturnConnection();
            using var trans = conn.BeginTransaction();
            try
            {
                foreach (var GName in gName)
                {
                    var cmdString =
                        "SELECT * FROM AMS.Godown WHERE GName = @GName AND BranchUnit = @pBranchId ";
                    var uom = await conn.QueryFirstOrDefaultAsync<Godown>(cmdString, new
                    {
                        GName = GName,
                        pBranchId = branchId
                    }, trans);
                    if (uom == null)
                    {
                        var newGIDId = await conn.ExecuteScalarAsync<int>(@"
							DECLARE @maxId INT = (Select Isnull(Max(GID),0)+1 GID from AMS.Godown )
							INSERT INTO AMS.Godown
								(GID, NepaliDesc, GName,GCode,GLocation,Status, EnterBy, EnterDate, BranchUnit,CompUnit,
                                SyncBaseId, SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
								VALUES (@maxId, @GName, @GName, @GName, null, 'true', @pUser, @pDate, @pBranchId,
                                null,null,null,null,@pDate,@pDate,1)
								SELECT @maxId ", new
                        {
                            GName = GName,
                            pBranchId = branchId,
                            pUser = username,
                            pDate = DateTime.Now
                        }, trans);

                        if (newGIDId == 0)
                        {
                            continue;
                        }

                        list.Add(new IntValueModel(newGIDId, GName));
                    }
                    else
                    {
                        list.Add(new IntValueModel(uom.GID, GName));
                    }
                }
                trans.Commit();
                result.Success = true;
                result.List = list;
            }
            catch (Exception e)
            {
                result = e.ToListErrorResult<IntValueModel>(this);
            }
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<IntValueModel>(this);
        }
        return result;
    }

    public async Task<ListResult<IntValueModel>> CreateAndFetchProductGroupsAsync(int branchId, IList<string> groups, string username)
    {
        var result = new ListResult<IntValueModel>();
        var list = new List<IntValueModel>();

        try
        {
            using var conn = GetConnection.ReturnConnection();
            using var trans = conn.BeginTransaction();

            try
            {
                foreach (var group in groups)
                {
                    var eGroup = await conn
                        .QueryFirstOrDefaultAsync<ProductGroup>(
                            "SELECT * FROM AMS.ProductGroup WHERE GrpName = @pName AND Branch_ID = @pBranchId ",
                            new { pName = group, pBranchId = branchId }, trans);
                    if (eGroup == null)
                    {
                        var newUnitId = await conn.ExecuteScalarAsync<int>(@"
								DECLARE @maxId INT = (Select Isnull(Max(PGrpID),0)+1 UId from AMS.ProductGroup )
								INSERT INTO AMS.ProductGroup
								(PGrpId,GrpName,GrpCode,Branch_ID,Status,EnterBy,EnterDate, SyncRowVersion)
								VALUES  (@maxId, @pGroupName ,@pGroupName , @pBranchId , 1 , @pUser , @pDate, 1)

								SELECT @maxId", new
                        {
                            pGroupName = group,
                            pBranchId = branchId,
                            pUser = username,
                            pDate = DateTime.Now
                        }, trans);

                        if (newUnitId == 0)
                        {
                            continue;
                        }

                        list.Add(new IntValueModel(newUnitId, group));
                    }
                    else
                    {
                        list.Add(new IntValueModel(eGroup.PGrpId, group));
                    }
                }

                trans.Commit();
                result.Success = true;
                result.List = list;
            }
            catch (Exception e)
            {
                result = e.ToListErrorResult<IntValueModel>(this);
            }
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<IntValueModel>(this);
        }

        return result;
    }
    public async Task<ListResult<ProductSubGroupEModel>> CreateAndFetchProductSubgroupsAsync(int branchId, IList<ProductSubGroupEModel> subGroups, string username)
    {
        var result = new ListResult<ProductSubGroupEModel>();
        var list = new List<ProductSubGroupEModel>();
        try
        {
            using var conn = GetConnection.ReturnConnection();
            using var trans = conn.BeginTransaction();
            try
            {
                foreach (var sGroup in subGroups)
                {
                    var eGroup = await conn.QueryFirstOrDefaultAsync<ProductSubGroup>(
                        "SELECT * FROM AMS.ProductSubGroup WHERE SubGrpName = @pName AND Branch_ID = @pBranchId ",
                        new { pName = sGroup.Name, pBranchId = branchId }, trans);

                    if (eGroup != null)
                    {
                        list.Add(new ProductSubGroupEModel(eGroup.PSubGrpId, eGroup.GrpId, sGroup.Name));
                        continue;
                    }

                    var newUnitId = await conn.ExecuteScalarAsync<int>(@"
								DECLARE @maxId INT = (Select Isnull(Max(PSubGrpId),0)+1 UId from AMS.ProductSubGroup )
								INSERT INTO AMS.ProductSubGroup
									(PSubGrpId, SubGrpName, ShortName, GrpId, Branch_ID, EnterBy, EnterDate, IsDefault, Status, SyncRowVersion)
								VALUES
									(@maxId, @pGroupName,@pGroupName,@pGroupId,@pBranchId,@pUser,GETDATE(), 1, 1, 1)

								SELECT @maxId ", new
                    {
                        pGroupName = sGroup.Name,
                        pBranchId = branchId,
                        pUser = username,
                        pGroupId = sGroup.GroupId
                    }, trans);

                    if (newUnitId != 0)
                    {
                        list.Add(new ProductSubGroupEModel(newUnitId, sGroup.GroupId, sGroup.Name));
                    }
                }

                trans.Commit();
                result.Success = true;
                result.List = list;
            }
            catch (Exception e)
            {
                result = e.ToListErrorResult<ProductSubGroupEModel>(this);
            }
        }
        catch (Exception e)
        {
            result = e.ToListErrorResult<ProductSubGroupEModel>(this);
        }

        return result;
    }
    // METHOD FOR DATA TABLE
    #region --------------- METHOD FOR THIS CLASS ---------------
    public void DataGridToExcel(DataTable dt, string folderPath, string fileName)
    {
        var excelApp = OfficeOpenXml.GetInstance();
        var stream = excelApp.GetExcelStream(dt);
        var fs = new FileStream(@$"{folderPath}\\{fileName}.xlsx", FileMode.Create);
        stream.CopyTo(fs);
        fs.Flush();
    }
    #endregion --------------- METHOD FOR THIS CLASS ---------------
    // RETURN VALUE IN DATA TABLE
    public DataTable GetProductListForImportFormat()
    {
        var cmdString =
            @"SELECT PID,PName,PShortName,PType,PG.GrpName,PSG.SubGrpName,PU.UnitCode,PBuyRate,PSalesRate FROM AMS.Product as P LEFT OUTER JOIN AMS.ProductGroup AS PG ON P.PGrpId=PG.PGrpID LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON P.PSubGrpId=PSG.PSubGrpId LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID ORDER BY P.PName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }
    public DataTable DownloadFormat()
    {
        using var table = new DataTable();
        table.Columns.Add("BarCode", typeof(int));
        table.Columns.Add("Description", typeof(string));
        table.Columns.Add("Barcode1", typeof(string));
        table.Columns.Add("Barcode2", typeof(string));
        table.Columns.Add("Type", typeof(string));
        table.Columns.Add("Group", typeof(string));
        table.Columns.Add("SubGroup", typeof(string));
        table.Columns.Add("UOM", typeof(string));
        table.Columns.Add("BuyRate", typeof(decimal));
        table.Columns.Add("SalesRate", typeof(decimal));
        table.Columns.Add("TaxRate", typeof(decimal));
        table.Columns.Add("Margin", typeof(decimal));
        table.Columns.Add("TradePrice", typeof(decimal));
        table.Columns.Add("MRP", typeof(decimal));

        #region ---------- Sample Data ----------

        table.Rows.Add("101", "MrSolution | AIMS", "AIMS", "Accounting", "Inventory", "Billing Software",
            "Restaurant Software", "Nodes", "45000", "50000", "13", "10", "65000", "95000");
        table.Rows.Add("201", "MrSolution | RESTRO", "RESTRO", "Restaurant", "Inventory", "Billing Software",
            "Restaurant Software", "Nodes", "45000", "50000", "13", "10", "65000", "95000");
        table.Rows.Add("301", "MrSolution | POS", "POS", "DepartmentStore", "Inventory", "Billing Software",
            "Accounting Software", "Nodes", "25000", "30000", "13", "10", "65000", "95000");
        table.Rows.Add("401", "MrSolution | HOTEL", "HOTEL", "HotelManagement", "Inventory", "Billing Software",
            "POS Billing Software", "Nodes", "35000", "40000", "13", "10", "65000", "95000");
        table.Rows.Add("501", "MrSolution | HOSPITAL", "HOSPITAL", "HospitalManagement", "Inventory",
            "Billing Software", "POS Billing Software", "Nodes", "35000", "40000", "13", "10", "65000", "95000");
        table.Rows.Add("601", "MrSolution | VEHICLE", "VEHICLE", "Showroom", "Inventory", "Billing Software",
            "POS Billing Software", "Nodes", "35000", "40000", "13", "10", "65000", "95000");
        table.Rows.Add("701", "MrSolution | PHARMA", "PHARMA", "Pharmacy", "Inventory", "Billing Software",
            "POS Billing Software", "Nodes", "35000", "40000", "13", "10", "65000", "95000");
        table.Rows.Add("801", "MrSolution | CINEMA", "CINEMA", "MovieHall", "Inventory", "Billing Software",
            "POS Billing Software", "Nodes", "35000", "40000", "13", "10", "65000", "95000");
        table.Rows.Add("901", "MrSolution | ERP", "ERP", "Corporate", "Inventory", "Billing Software",
            "POS Billing Software", "Nodes", "35000", "40000", "13", "10", "65000", "95000");

        #endregion ---------- Sample Data ----------

        return table;
    }
}