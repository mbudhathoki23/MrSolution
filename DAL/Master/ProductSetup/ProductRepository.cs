using DatabaseModule.CloudSync;
using DatabaseModule.Domains.BarcodePrint;
using DatabaseModule.Master.ProductSetup;
using DevExpress.XtraPrinting.BarCode.Native;
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MrDAL.Master.ProductSetup;

public class ProductRepository : IProductRepository
{
    public ProductRepository()
    {
        ObjProduct = new Product();
        BarcodeLists = new List<BarcodeList>();
        _master = new ClsMasterSetup();
        _injectData = new DbSyncRepoInjectData();
        _configParams = new InfoResult<ValueModel<string, string, Guid>>();
    }

    // INSERT UPDATE DELETE
    public async Task<int> SyncProductAsync(string actionTag)
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
            GetUrl = @$"{_configParams.Model.Item2}Product/GetProductsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Product/InsertProductList",
            UpdateUrl = @$"{_configParams.Model.Item2}Product/UpdateProduct"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productsRepo = DataSyncProviderFactory.GetRepository<Product>(_injectData);
        var products = new List<Product>
        {
            ObjProduct
        };

        // push realtime details to server
        await productsRepo.PushNewListAsync(products);

        // update main area SyncGlobalId to local
        if (productsRepo.GetHashCode() > 0)
        {
            await SyncUpdateProduct(ObjProduct.PID);
        }

        return productsRepo.GetHashCode();
    }

    public async Task<bool> SyncProductDetailsAsync()
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
            GetUrl = @$"{_configParams.Model.Item2}Product/GetProductsByCallCount",
            InsertUrl = @$"{_configParams.Model.Item2}Product/InsertProductList",
            UpdateUrl = @$"{_configParams.Model.Item2}Product/UpdateProduct"
        };

        DataSyncHelper.SetConfig(apiConfig);
        _injectData.ApiConfig = apiConfig;
        DataSyncManager.SetGlobalInjectData(_injectData);
        var productsRepo = DataSyncProviderFactory.GetRepository<Product>(_injectData);

        // pull all new product data
        var pullResponse = await PullProductsServerToClientByRowCount(productsRepo, 1);
        if (!pullResponse)
        {
            SplashScreenManager.CloseForm(true);
            return false;
        }

        // push all new product data
        var sqlQuery = GetProductScript();
        var queryResponse = await QueryUtils.GetListAsync<Product>(sqlQuery);
        var prList = queryResponse.List.ToList();
        if (prList.Count > 0)
        {
            var pushResponse = await productsRepo.PushNewListAsync(prList);
            if (!pushResponse.Value)
            {
                SplashScreenManager.CloseForm();
                return false;
            }
        }

        return true;
    }
    public Task<int> SyncUpdateProduct(long productId = 0)
    {
        var commandText = $@"
            UPDATE AMS.Product SET SyncGlobalId = '{ObjGlobal.SyncOrginIdSync}',SyncCreatedOn = GETDATE(),SyncLastPatchedOn = GETDATE() ";
        if (productId > 0)
        {
            commandText += $" WHERE PID = '{productId}'";
        }
        var result = SqlExtensions.ExecuteNonQueryAsync(commandText);
        return result;
    }

    public int SaveProductInfo(string actionTag)
    {
        var cmdString = string.Empty;

        if (actionTag.ToUpper() is "DELETE")
        {
            SaveProductAuditLog(actionTag);
            cmdString = $"DELETE FROM AMS.BookDetails WHERE BookId ='{ObjProduct.PID}'; \n";
            cmdString += $"DELETE FROM AMS.BarcodeList WHERE ProductId='{ObjProduct.PID}'; \n";
            cmdString += $"DELETE FROM AMS.Product where Pid='{ObjProduct.PID}'; \n";
        }
        var barcode3 = GenerateBarcode3(ObjProduct.PID);
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString = @" 
                INSERT INTO AMS.Product(PID, NepaliDesc, PName, PAlias, PShortName,HsCode, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PVehicleWise, PublicationWise, PBuyRate, PSalesRate,AltSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_ID, CmpUnit_ID, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, IsDefault, Status, ChasisNo, EngineNo, VHModel, VHColor, VHNumber, BeforeBuyRate, BeforeSalesRate, Barcode, Barcode1, Barcode2, Barcode3, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ";
            cmdString += "\n VALUES(";
            cmdString += $"{ObjProduct.PID},";
            cmdString += ObjProduct.PName.Length > 0 ? $"N'{ObjProduct.PName.GetTrimReplace()}'," : "NULL,";
            cmdString += ObjProduct.PName.Length > 0 ? $"N'{ObjProduct.PName.GetTrimReplace()}'," : "NULL,";
            cmdString += ObjProduct.PAlias.Length > 0 ? $"N'{ObjProduct.PAlias.GetTrimReplace()}'," : "NULL,";
            cmdString += ObjProduct.PShortName.Length > 0 ? $"N'{ObjProduct.PShortName}'," : "NULL,";
            cmdString += ObjProduct.HsCode.Length > 0 ? $"N'{ObjProduct.HsCode}'," : "NULL,";
            cmdString += ObjProduct.PType.Trim().Length > 0 ? $"N'{ObjProduct.PType.Trim()}'," : "N'I',";
            cmdString += ObjProduct.PCategory.Trim().Length > 0 ? $"N'{ObjProduct.PCategory.Trim()}'," : "N'FG',";
            cmdString += ObjProduct.PUnit > 0 ? $" {ObjProduct.PUnit}," : "NULL ,";
            cmdString += ObjProduct.PAltUnit > 0 ? $" {ObjProduct.PAltUnit}," : "NULL ,";
            cmdString += $"{ObjProduct.PQtyConv.GetDecimal()}, {ObjProduct.PAltConv.GetDecimal()},";
            cmdString += ObjProduct.PValTech.Trim().Length > 0 ? $"N'{ObjProduct.PValTech.Trim()}'," : "N'FIFO',";
            cmdString += $" CAST('{ObjProduct.PSerialno}' AS BIT),CAST('{ObjProduct.PSizewise}' AS BIT),CAST('{ObjProduct.PBatchwise}' AS BIT),";
            cmdString += $" CAST('{ObjProduct.PVehicleWise}' AS BIT),CAST('{ObjProduct.PublicationWise}' AS BIT),";
            cmdString += $" {ObjProduct.PBuyRate.GetDecimal()},{ObjProduct.PSalesRate.GetDecimal()},{ObjProduct.AltSalesRate.GetDecimal()},";
            cmdString += $" {ObjProduct.PMargin1.GetDecimal()},{ObjProduct.TradeRate.GetDecimal()},{ObjProduct.PMargin2.GetDecimal()},{ObjProduct.PMRP.GetDecimal()},";
            cmdString += ObjProduct.PGrpId > 0 ? $"{ObjProduct.PGrpId} ," : "NULL ,";
            cmdString += ObjProduct.PSubGrpId > 0 ? $"{ObjProduct.PSubGrpId}," : "NULL ,";
            cmdString += $"{ObjProduct.PTax.GetDecimal()},{ObjProduct.PMin.GetDecimal()},{ObjProduct.PMax.GetDecimal()},";
            cmdString += ObjProduct.CmpId > 0 ? $" {ObjProduct.CmpId}," : "NULL ,";
            cmdString += "Null ,Null, Null,";
            cmdString += ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL ,";
            cmdString += ObjGlobal.SysCompanyUnitId.GetInt() > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL ,";
            cmdString += ObjProduct.PPL > 0 ? $" {ObjProduct.PPL}," : "NULL,";
            cmdString += ObjProduct.PPR > 0 ? $" {ObjProduct.PPR}," : "NULL,";
            cmdString += ObjProduct.PSL > 0 ? $" {ObjProduct.PSL}," : "NULL,";
            cmdString += ObjProduct.PSR > 0 ? $" {ObjProduct.PSR}," : "NULL,";
            cmdString += ObjProduct.PL_Opening > 0 ? $" {ObjProduct.PL_Opening}," : "NULL,";
            cmdString += ObjProduct.PL_Closing > 0 ? $" {ObjProduct.PL_Closing}," : "NULL,";
            cmdString += ObjProduct.BS_Closing > 0 ? $" {ObjProduct.BS_Closing}," : "NULL,";
            cmdString += $"Null, N'{ObjGlobal.LogInUser}', GETDATE(),0,";
            cmdString += ObjProduct.Status ? " 1," : "0,";
            cmdString += ObjProduct.ChasisNo.IsValueExits() ? $"N'{ObjProduct.ChasisNo}'," : "NULL,";
            cmdString += ObjProduct.EngineNo.IsValueExits() ? $"N'{ObjProduct.EngineNo}'," : "NULL,";
            cmdString += ObjProduct.VHModel.IsValueExits() ? $"N'{ObjProduct.VHModel}'," : "NULL,";
            cmdString += ObjProduct.VHColor.IsValueExits() ? $"N'{ObjProduct.VHColor}', " : "NULL, ";
            cmdString += ObjProduct.VHNumber.IsValueExits() ? $"N'{ObjProduct.VHNumber}', " : "NULL, ";
            cmdString += $"{ObjProduct.BeforeBuyRate},{ObjProduct.BeforeSalesRate},";
            cmdString += ObjProduct.Barcode.IsValueExits() ? $"N'{ObjProduct.Barcode}'," : $"NULL,";
            cmdString += ObjProduct.Barcode1.IsValueExits() ? $"N'{ObjProduct.Barcode1}', " : $"{ObjProduct.PID},";
            cmdString += ObjProduct.Barcode2.IsValueExits() ? $"N'{ObjProduct.Barcode2}', " : $"{100000000000000 + ObjProduct.PID},";
            cmdString += ObjProduct.Barcode3.IsValueExits() ? $"N'{ObjProduct.Barcode3}', " : $"{barcode3},";
            cmdString += ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,";
            cmdString += ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.IsValueExits() ? $"N'{ObjGlobal.LocalOriginId}', " : "Null, ";
            cmdString += ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,";
            cmdString += $"GETDATE(),GETDATE(),{ObjProduct.SyncRowVersion.GetDecimal(true)} ); ";
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString += " UPDATE AMS.Product SET \n";
            cmdString += $"PName = N'{ObjProduct.PName}',";
            cmdString += $"PAlias= N'{ObjProduct.PAlias}',";
            cmdString += $"PShortName = N'{ObjProduct.PShortName}',";
            cmdString += $"HsCode = N'{ObjProduct.HsCode}',";
            cmdString += $"PType= N'{ObjProduct.PType.Trim()}', PCategory= N'{ObjProduct.PCategory.Trim()}',";
            cmdString += ObjProduct.PUnit > 0 ? $"PUnit=  {ObjProduct.PUnit}," : "PUnit = Null ,";
            cmdString += ObjProduct.PAltUnit > 0 ? $"PAltUnit=  {ObjProduct.PAltUnit}," : "PAltUnit= Null ,";
            cmdString += $"PQtyConv= {ObjProduct.PQtyConv}, PAltConv= {ObjProduct.PAltConv},PValTech= N'{ObjProduct.PValTech.Trim()}',";
            cmdString += ObjProduct.PSerialno ? "PSerialno=  1," : "PSerialno=0,";
            cmdString += ObjProduct.PSizewise ? " PSizewise= 1," : "PSizewise= 0,";
            cmdString += ObjProduct.PBatchwise ? "PBatchwise= 1," : "PBatchwise=0,";
            cmdString += $" PBuyRate= {ObjProduct.PBuyRate.GetDecimal()},PSalesRate= {ObjProduct.PSalesRate.GetDecimal()},AltSalesRate= {ObjProduct.AltSalesRate.GetDecimal()},PMargin1 = {ObjProduct.PMargin1.GetDecimal()},TradeRate= {ObjProduct.TradeRate.GetDecimal()},PMargin2= {ObjProduct.PMargin2.GetDecimal()},PMRP= {ObjProduct.PMRP.GetDecimal()},";
            cmdString += ObjProduct.PGrpId > 0 ? $"PGrpId= {ObjProduct.PGrpId} ," : "PGrpId= Null ,";
            cmdString += ObjProduct.PSubGrpId > 0 ? $"PSubGrpId= {ObjProduct.PSubGrpId}," : "PSubGrpId= Null ,";
            cmdString += $"PTax={ObjProduct.PTax.GetDecimal()},";
            cmdString += $"PMin={ObjProduct.PMin.GetDecimal()},";
            cmdString += $"PMax={ObjProduct.PMax.GetDecimal()},";
            cmdString += ObjProduct.CmpId > 0 ? $" CmpId= {ObjProduct.CmpId}," : "CmpId= Null ,";
            cmdString += ObjProduct.PPL > 0 ? $"PPL=  {ObjProduct.PPL}," : "PPL= Null ,";
            cmdString += ObjProduct.PPR > 0 ? $" PPR= {ObjProduct.PPR}," : "PPR= Null ,";
            cmdString += ObjProduct.PSL > 0 ? $" PSL= {ObjProduct.PSL}," : "PSL= Null ,";
            cmdString += ObjProduct.PSR > 0 ? $" PSR= {ObjProduct.PSR}," : "PSR= Null ,";
            cmdString += ObjProduct.PL_Opening > 0 ? $" PL_Opening= {ObjProduct.PL_Opening}," : "PL_Opening= Null ,";
            cmdString += ObjProduct.PL_Closing > 0 ? $"PL_Closing=  {ObjProduct.PL_Closing}," : " PL_Closing= Null ,";
            cmdString += ObjProduct.BS_Closing > 0 ? $" BS_Closing= {ObjProduct.BS_Closing}," : "BS_Closing= Null ,";
            cmdString += ObjProduct.Status ? " Status= 1," : "Status= 0,";
            cmdString += $"BeforeBuyRate= {ObjProduct.BeforeBuyRate.GetDecimal()},";
            cmdString += $"BeforeSalesRate= {ObjProduct.BeforeSalesRate.GetDecimal()},";
            cmdString += $"ChasisNo= N'{ObjProduct.ChasisNo}',VHColor = '{ObjProduct.VHColor}',VHModel = '{ObjProduct.VHModel}',";
            cmdString += ObjProduct.Barcode.IsValueExits() ? $"Barcode = N'{ObjProduct.Barcode.Trim()}'," : "Barcode =NULL,";
            cmdString += ObjProduct.Barcode1.IsValueExits() ? $" Barcode1 = N'{ObjProduct.Barcode1}', " : $"Barcode1={ObjProduct.PID},";
            cmdString += ObjProduct.Barcode2.IsValueExits() ? $"Barcode2 = N'{ObjProduct.Barcode2}'," : $"Barcode2={100000000000000 + ObjProduct.PID},";
            cmdString += ObjProduct.Barcode3.IsValueExits() ? $" Barcode3 = N'{ObjProduct.Barcode3}'," : $"Barcode3 ={barcode3},";
            cmdString += $"SyncRowVersion = {ObjProduct.SyncRowVersion.GetDecimal(true)} , SyncLastPatchedOn =  GETDATE() ";
            cmdString += $" WHERE PID ='{ObjProduct.PID}'; \n ";
        }

        if (BarcodeLists != null && actionTag != "DELETE" && BarcodeLists.Count > 0)
        {
            cmdString += $" DELETE FROM AMS.BarcodeList WHERE ProductId='{ObjProduct.PID}'; \n";
            cmdString += @" 
                INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange) ";
            cmdString += "\n VALUES ";
            foreach (var list in BarcodeLists)
            {
                cmdString += $"({ObjProduct.PID}, N'{list.Barcode}',{list.SalesRate},";
                cmdString += $"{list.MRP},{list.Trade},{list.Wholesale},{list.Retail},{list.Dealer},{list.Resellar},";

                cmdString += list.UnitId > 0 ? $"{list.UnitId}," : "NULL,";
                cmdString += list.AltUnitId > 0 ? $"{list.AltUnitId}," : "NULL,";
                cmdString += "0),";
            }

            cmdString = cmdString.Substring(0, cmdString.Length - 1);
            cmdString += ";";
        }

        if (actionTag != "DELETE" && ObjProduct.PublicationWise)
        {
            cmdString += $" DELETE FROM AMS.BookDetails WHERE Bookid ='{ObjProduct.PID}'; \n";
            cmdString += " INSERT INTO AMS.BookDetails (BookId, PrintDesc, ISBNNo, Author, Publisher) \n";
            cmdString += " VALUES \n";
            cmdString += $"({BookDetails.BookId}, N'{BookDetails.PrintDesc.GetTrimReplace()}', ";
            cmdString += BookDetails.ISBNNo.Trim().Length > 0 ? $"N'{BookDetails.ISBNNo.Trim()}'," : "Null,";
            cmdString += BookDetails.Author.Trim().Length > 0 ? $"N'{BookDetails.Author.GetTrimReplace()}'," : "Null,";
            cmdString += BookDetails.Publisher.Trim().Length > 0 ? $"N'{BookDetails.Publisher.GetTrimReplace()}'); \n" : "Null); \n";
        }

        var exe = SqlExtensions.ExecuteNonTrans(cmdString);
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncProductAsync(actionTag));
        }

        SaveProductAuditLog(actionTag);
        if (ObjProduct.PImage is { Length: > 0 })
        {
            cmdString = $"UPDATE AMS.Product SET PImage = @PImage  WHERE PID = {ObjProduct.PID} ";
            using var cmd = new SqlCommand(cmdString.ToString(), GetConnection.ReturnConnection());
            cmd.Parameters.Add("@PImage", SqlDbType.VarBinary).Value = ObjProduct.PImage != null ? ObjProduct.PImage : DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        if (BarcodeLists == null || BarcodeLists.Count == 0 && ObjGlobal.SoftwareModule.Equals("POS"))
        {
            AlterBarcodeList();
        }

        return exe;
    }
    public int SaveProductAuditLog(string actionTag)
    {
        var cmdString = $@"
			INSERT INTO AUD.AUDIT_PRODUCT(PID, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PBuyRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, Status, BeforeBuyRate, BeforeSalesRate, Barcode, ChasisNo, EngineNo, VHColor, VHModel, Barcode1, Barcode2, Barcode3, ModifyAction, ModifyBy, ModifyDate)
			SELECT PID, PName, PAlias, PShortName, PType, PCategory, PUnit, PAltUnit, PQtyConv, PAltConv, PValTech, PSerialno, PSizewise, PBatchwise, PBuyRate, PSalesRate, PMargin1, TradeRate, PMargin2, PMRP, PGrpId, PSubGrpId, PTax, PMin, PMax, CmpId, CmpId1, CmpId2, CmpId3, Branch_Id, CmpUnit_Id, PPL, PPR, PSL, PSR, PL_Opening, PL_Closing, BS_Closing, PImage, EnterBy, EnterDate, Status, BeforeBuyRate, BeforeSalesRate, Barcode, ChasisNo, EngineNo, VHColor, VHModel, Barcode1, Barcode2, Barcode3, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
			FROM AMS.Product
			WHERE PID='{ObjProduct.PID}';

			INSERT INTO AUD.AUDIT_BOOK_DETAILS(BookId, PrintDesc, ISBNNo, Author, Publisher, ModifyAction, ModifyBy, ModifyDate)
			SELECT BookId, PrintDesc, ISBNNo, Author, Publisher, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
			FROM AMS.BookDetails
			WHERE BookId='{ObjProduct.PID}';

			INSERT INTO AUD.AUDIT_BARCODE_LIST(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, ModifyAction, ModifyBy, ModifyDate)
			SELECT ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, '{actionTag}' ModifyAction, '{ObjGlobal.LogInUser}' ModifyBy, GETDATE() ModifyDate
			FROM AMS.BarcodeList
			WHERE ProductId='{ObjProduct.PID}';
            ";
        var result = SqlExtensions.ExecuteNonTrans(cmdString);
        return result;
    }
    public string GenerateBarcode3(long productId, BarCodeSymbology barCodeSymbology = BarCodeSymbology.Code128)
    {
        BarCodePrintConfigModel barcodeSetting = null;
        var response = Task.Run(() => QueryUtils.GetFirstOrDefaultAsync<string>(@"SELECT barcode_print_config FROM ams.InventorySetting"));
        if (response.Result.Success && !string.IsNullOrWhiteSpace(response.Result.Model))
        {
            barcodeSetting = XmlUtils.XmlDeserialize<BarCodePrintConfigModel>(response.Result.Model);
        }

        var barcode3 = (100000000 + productId).ToString(); //code128
        if (barcodeSetting != null && (barcodeSetting.Symbology == BarCodeSymbology.UPCA || barCodeSymbology == BarCodeSymbology.UPCA))
        {
            barcode3 = (10000000000 + productId).ToString();
            long resultOutput = 0;
            for (var i = 0; i < barcode3.Length; i++)
            {
                var c = barcode3[i];
                if ((i + 1) % 2 == 0)
                    //even position
                {
                    resultOutput += c.GetLong();
                }
                else
                    //odd position
                {
                    resultOutput += 3 * c.GetLong();
                }
            }

            if (resultOutput % 10 == 0)
            {
                barcode3 = barcode3 + "0";
            }
            else
            {
                var mod = resultOutput % 10;
                barcode3 = barcode3 + (10 - mod);
            }
        }

        return barcode3;
    }
    private int AlterBarcodeList()
    {
        var cmdString = $@"
	        DELETE AMS.BarcodeList WHERE Barcode IN (SELECT CAST(p.PID AS NVARCHAR(50))  FROM AMS.Product p) AND ProductId= {ObjProduct.PID};
	        DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode FROM AMS.Product p) AND ProductId= {ObjProduct.PID};
	        DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.PShortName FROM AMS.Product p) AND ProductId= {ObjProduct.PID};
	        DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode1 FROM AMS.Product p) AND ProductId= {ObjProduct.PID};
	        DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode2 FROM AMS.Product p) AND ProductId= {ObjProduct.PID};
	        DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode3 FROM AMS.Product p) AND ProductId= {ObjProduct.PID};
	        
            INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
	        SELECT p.PID, p.PShortName, p.PSalesRate, p.PMRP, p.TradeRate, 0 Wholesale, 0 Retail, 0 Dealer, 0 Resellar, p.PUnit, p.PAltUnit,0
	        FROM AMS.Product p
	        WHERE PID={ObjProduct.PID} AND PShortName NOT IN(SELECT Barcode FROM AMS.BarcodeList bl WHERE bl.ProductId=p.PID)AND PShortName IS NOT NULL AND p.PShortName<>'';
	        
            INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
	        SELECT p.PID, p.Barcode, p.PSalesRate, p.PMRP, p.TradeRate, 0 Wholesale, 0 Retail, 0 Dealer, 0 Resellar, p.PUnit, p.PAltUnit,0
	        FROM AMS.Product p
	        WHERE PID={ObjProduct.PID} AND p.Barcode NOT IN(SELECT Barcode FROM AMS.BarcodeList bl WHERE bl.ProductId=p.PID)AND p.Barcode IS NOT NULL AND p.Barcode<>'';
	        
            INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
	        SELECT p.PID, p.Barcode1, p.PSalesRate, p.PMRP, p.TradeRate, 0 Wholesale, 0 Retail, 0 Dealer, 0 Resellar, p.PUnit, p.PAltUnit,0
	        FROM AMS.Product p
	        WHERE PID={ObjProduct.PID} AND p.Barcode1 NOT IN(SELECT bl.Barcode FROM AMS.BarcodeList bl WHERE bl.ProductId=p.PID)AND p.Barcode1 IS NOT NULL AND p.Barcode1<>'';";
        return SqlExtensions.ExecuteNonQuery(cmdString);

        //INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
        //SELECT p.PID, p.Barcode2, p.PSalesRate, p.PMRP, p.TradeRate, 0 Wholesale, 0 Retail, 0 Dealer, 0 Resellar, p.PUnit, p.PAltUnit,0
        //FROM AMS.Product p
        //WHERE PID={ObjProduct.PID} AND p.Barcode2 NOT IN(SELECT bl.Barcode FROM AMS.BarcodeList bl WHERE bl.ProductId=p.PID)AND p.Barcode2 IS NOT NULL AND p.Barcode2<>'';

        //INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
        //SELECT p.PID, p.Barcode3, p.PSalesRate, p.PMRP, p.TradeRate, 0 Wholesale, 0 Retail, 0 Dealer, 0 Resellar, p.PUnit, p.PAltUnit,0
        //FROM AMS.Product p
        //WHERE PID={ObjProduct.PID} AND p.Barcode3 NOT IN(SELECT bl.Barcode FROM AMS.BarcodeList bl WHERE bl.ProductId=p.PID)AND p.Barcode3 IS NOT NULL AND p.Barcode3<>'';
    }
    public string GetProductScript(int productId = 0)
    {
        var cmdString = $@"SELECT * FROM AMS.Product";
        cmdString += productId > 0 ? $" WHERE SyncGlobalId IS NULL AND PID= {productId} " : "";
        return cmdString;
    }


    // PULL PRODUCT
    #region ---------- PULL PRODUCT ----------
    public async Task<bool> GetAndSaveUnSynchronizedProducts()
    {
        try
        {
            var productList = await _master.GetUnSynchronizedData("AMS.Product");
            if (productList.List != null)
            {
                foreach (var data in productList.List)
                {
                    var productData = JsonConvert.DeserializeObject<Product>(data.JsonData);
                    var actionTag = data.Action;
                    ObjProduct.PID = productData.PID;
                    ObjProduct.NepaliDesc = productData.NepaliDesc;
                    ObjProduct.PName = productData.PName;
                    ObjProduct.PAlias = productData.PAlias;
                    ObjProduct.PShortName = productData.PShortName;
                    ObjProduct.PType = productData.PType;
                    ObjProduct.PCategory = productData.PCategory;
                    ObjProduct.PUnit = productData.PUnit;
                    ObjProduct.PAltUnit = productData.PAltUnit;
                    ObjProduct.PQtyConv = productData.PQtyConv;
                    ObjProduct.PAltConv = productData.PAltConv;
                    ObjProduct.PValTech = productData.PValTech;
                    ObjProduct.PSerialno = productData.PSerialno;
                    ObjProduct.PSizewise = productData.PSizewise;
                    ObjProduct.PBatchwise = productData.PBatchwise;
                    ObjProduct.PBuyRate = productData.PBuyRate;
                    ObjProduct.PSalesRate = productData.PSalesRate;
                    ObjProduct.PMargin1 = productData.PMargin1;
                    ObjProduct.TradeRate = productData.TradeRate;
                    ObjProduct.PMargin2 = productData.PMargin2;
                    ObjProduct.PMRP = productData.PMRP;
                    ObjProduct.PGrpId = productData.PGrpId;
                    ObjProduct.PSubGrpId = productData.PSubGrpId;
                    ObjProduct.PTax = productData.PTax;
                    ObjProduct.PMin = productData.PMin;
                    ObjProduct.PMax = productData.PMax;
                    ObjProduct.CmpId = productData.CmpId;
                    ObjProduct.CmpId1 = productData.CmpId1;
                    ObjProduct.CmpId2 = productData.CmpId2;
                    ObjProduct.CmpId3 = productData.CmpId3;
                    ObjProduct.Branch_Id = productData.Branch_Id;
                    ObjProduct.CmpUnit_Id = productData.CmpUnit_Id;
                    ObjProduct.PPL = productData.PPL;
                    ObjProduct.PPR = productData.PPR;
                    ObjProduct.PSL = productData.PSL;
                    ObjProduct.PSR = productData.PSR;
                    ObjProduct.PL_Opening = productData.PL_Opening;
                    ObjProduct.PL_Closing = productData.PL_Closing;
                    ObjProduct.BS_Closing = productData.BS_Closing;
                    ObjProduct.PImage = productData.PImage;
                    ObjProduct.EnterBy = ObjGlobal.LogInUser;
                    ObjProduct.EnterDate = DateTime.Now;
                    ObjProduct.IsDefault = productData.IsDefault;
                    ObjProduct.Status = productData.Status;
                    ObjProduct.ChasisNo = productData.ChasisNo;
                    ObjProduct.EngineNo = productData.EngineNo;
                    ObjProduct.VHModel = productData.VHModel;
                    ObjProduct.VHColor = productData.VHColor;
                    ObjProduct.VHNumber = productData.VHNumber;
                    ObjProduct.BeforeBuyRate = productData.BeforeBuyRate;
                    ObjProduct.BeforeSalesRate = productData.BeforeSalesRate;
                    ObjProduct.Barcode = productData.Barcode;
                    ObjProduct.Barcode1 = productData.Barcode1;
                    ObjProduct.Barcode2 = productData.Barcode2;
                    ObjProduct.Barcode3 = productData.Barcode3;
                    ObjProduct.SyncRowVersion = productData.SyncRowVersion;
                    ObjProduct.AltSalesRate = productData.AltSalesRate == null ? 0 : productData.AltSalesRate;
                    ObjProduct.PVehicleWise = productData.PVehicleWise == null ? false : productData.PVehicleWise;
                    ObjProduct.PublicationWise = productData.PublicationWise == null ? false : productData.PublicationWise;
                    var result = SaveProductInfo(actionTag);
                    if (result > 0)
                    {
                        //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                        //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                        //actionTag = "SAVE";
                        //var response = await _master.SaveSyncLogDetails(actionTag);
                    }
                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }

    public async Task<bool> PullProductsServerToClientByRowCount(IDataSyncRepository<Product> productRepo, int callCount)
    {
        try
        {
            var pullResponse = await productRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                return false;
            }

            var query = GetProductScript();
            var alldata = SqlExtensions.ExecuteDataSetSql(query);

            foreach (var productData in pullResponse.List)
            {
                ObjProduct = productData;

                var alreadyExistData = alldata.Select("PID= '" + productData.PID + "'");
                if (alreadyExistData.Length > 0)
                {
                    //get SyncRowVersion from client database table
                    int ClientSyncRowVersionId = 1;
                    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //update only server SyncRowVersion is greater than client database while data pulling from server
                    if (productData.SyncRowVersion > ClientSyncRowVersionId)
                    {
                        var result = SaveProductInfo("UPDATE");
                    }
                }
                else
                {
                    var result = SaveProductInfo("SAVE");
                }
            }


            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullProductsServerToClientByRowCount(productRepo, callCount);
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

    // OBJECT FOR THIS FORM
    public Product ObjProduct { get; set; }
    public List<BarcodeList> BarcodeLists { get; set; }
    public BookDetails BookDetails { get; set; }
    private DbSyncRepoInjectData _injectData;
    private IMasterSetup _master;
    private InfoResult<ValueModel<string, string, Guid>> _configParams;
}