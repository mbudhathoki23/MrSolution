using ClosedXML.Excel;
using Dapper;
using DatabaseModule.Master.ProductSetup;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Lib.Dapper.Contrib;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Models.Common;
using MrDAL.Models.Custom;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
namespace MrDAL.Master.SystemSetup;

public class SparePartsImportRepository : ISparePartsImportRepository
{
    public SparePartsImportRepository()
    {

    }
    public async Task<NonQueryResult> UpdateProductImportAsync(IList<ImportSparePartsEModel> rows, int branchId,
        string username)
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
                    var product = await conn.QueryFirstOrDefaultAsync<DatabaseModule.Master.ProductSetup.Product>(
                        "SELECT * FROM AMS.Product WHERE PName = @pName ",
                        new { pName = model.Description }, trans);

                    if (product != null)
                    {
                        await conn.ExecuteAsync(@"
								UPDATE AMS.Product SET
									PShortName = @ShortName,
									NepaliDesc = @Description,
									PUnit = @PUnit,
									PBuyRate = @BuyRate,
									PSalesRate = @SalesRate,
									PTax = @TaxRate,
									PMin = @MinStock,
									PMax = @MaxStock,
									PMRP = @MRPRate
								    WHERE PID = @pProductId ",
                            new
                            {
                                ShortName = model.ShortName,
                                Description = model.Description,
                                PGrpId = model.GroupId,
                                PSubGrpId = model.SubGroupId,
                                PUnit = model.UId,
                                PQtyConv = model.QtyConv,
                                PAltConv = model.AltConv,
                                BuyRate = model.BuyRate,
                                SalesRate = model.SalesRate,
                                TaxRate = model.TaxRate,
                                MinStock = model.MinStock,
                                MaxStock = model.MaxStock,
                                MRPRate = model.MRPRate,
                                Branch_Id = branchId,
                                pProductId = product.PID
                            }, trans);
                    }
                    else
                    {
                        var newId = await conn.NewBigIntIdAsync("AMS.Product", "PID", trans);


                        await conn.InsertAsync(new DatabaseModule.Master.ProductSetup.ImportProduct
                        {
                            PID = newId,
                            Barcode = "BC",
                            PName = model.Description,
                            NepaliDesc = model.Description,
                            PType = "I",
                            PGrpId = model.GroupId,
                            PSubGrpId = model.SubGroupId,
                            PUnit = model.UId,
                            Branch_Id = branchId,
                            EnterBy = username,
                            EnterDate = curDateTime,
                            PBuyRate = model.BuyRate,
                            PTax = model.TaxRate,
                            PSalesRate = model.SalesRate,
                            PMargin1 = 0,
                            TradeRate = 0,
                            PMRP = model.MRPRate,
                            PAlias = model.Description,
                            PShortName = model.ShortName,
                            PQtyConv = model.QtyConv,
                            PAltConv = model.AltConv,
                            PValTech = "FIFO",
                            PSerialno = false,
                            PSizewise = false,
                            PBatchwise = false,
                            PMargin2 = 0,
                            PMin = model.MinStock,
                            PMax = model.MaxStock,
                            Status = true,
                            BeforeBuyRate = model.BuyRate,
                            BeforeSalesRate = model.SalesRate,
                            PCategory = "FG",
                            SyncRowVersion = 1,
                            SyncCreatedOn = curDateTime,
                            SyncGlobalId = Guid.NewGuid(),
                            AltSalesRate = 0,
                            CmpId = null
                        }, trans, 300, null, "AMS.Product");
                    }
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