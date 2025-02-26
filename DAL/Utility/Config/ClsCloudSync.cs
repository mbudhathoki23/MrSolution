using Dapper;
using DatabaseModule.Master.FinanceSetup;
using DatabaseModule.Master.InventorySetup;
using DatabaseModule.Master.LedgerSetup;
using DatabaseModule.Master.ProductSetup;
using DatabaseModule.Setup.DocumentNumberings;
using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Master.Interface.InventorySetup;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.Interface.SystemSetup;
using MrDAL.Master.LedgerSetup;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MrDAL.Utility.Config;

public class ClsCloudSync
{
    public bool GetServerConnection()
    {
        var con = new SqlConnection(GetConnection.CloudMasterConnectionString);
        try
        {
            con.Open();
            if (con.State == ConnectionState.Open)
            {
                GetCloudDatabaseName(con);
            }
            return true;
        }
        catch (Exception ex)
        {
            var errMsg = ex.Message;
            return false;
        }
    }
    public void GetCloudDatabaseName(SqlConnection connection)
    {
        var cmdString = $"SELECT * FROM AMS.GlobalCompany gc WHERE gc.ApiKey = '{ObjGlobal.SyncOrginIdSync}'";
        var dtResult = SqlExtensions.ExecuteDataSet(connection, cmdString);
        if (dtResult.Tables.Count > 0)
        {
            var result = dtResult.Tables[0].Rows[0]["Database_Name"].ToString();
            GetConnection.CloudInitialCatalog = result.IsValueExits() ? result : string.Empty;
        }

    }
    public void ImportFunction(string tableName)
    {
        var result = tableName switch
        {
            "ACCOUNT GROUP" => ImportAccountGroupInCloud().Result,
            "ACCOUNT SUBGROUP" => ImportAccountSubGroupInCloud().Result,
            "PRODUCT GROUP" => ImportProductGroupInCloud().Result,
            "PRODUCT SUBGROUP" => ImportProductSubGroupInCloud().Result,
            "PRODUCT UNIT" => ImportProductUnitInCloud().Result,
            "MAIN AGENT" => ImportMainAgentInCloud().Result,
            "MAIN AREA" => ImportMainAreaInCloud().Result,
            "AREA" => ImportAreaInCloud().Result,
            "RACK" => ImportRackInCloud().Result,
            "CURRENCY" => ImportCurrencyInCloud().Result,
            "GENERAL LEDGER" => ImportGeneralLedgerInCloud().Result,
            "SUB LEDGER" => ImportSubLedgerInCloud().Result,
            "COST CENTER" => ImportCostCenterInCloud().Result,
            "DOCUMENT NUMBERING" => ImportDocumentNumberingInCloud().Result,
            _ => 0
        };

    }
    public void ExportFunction(string tableName)
    {
        var result = tableName switch
        {
            "ACCOUNT GROUP" => ExportAccountGroupInCloud().Result,
            "ACCOUNT SUBGROUP" => ExportAccountSubGroupInCloud().Result,
            "PRODUCT GROUP" => ExportProductGroupInCloud().Result,
            "PRODUCT SUBGROUP" => ExportProductSubGroupInCloud().Result,
            "PRODUCT UNIT" => ExportProductUnitInCloud().Result,
            "MAIN AGENT" => ExportMainAgentInCloud().Result,
            "MAIN AREA" => ExportMainAreaInCloud().Result,
            "AREA" => ExportAreaInCloud().Result,
            "RACK" => ExportRackInCloud().Result,
            "CURRENCY" => ExportCurrencyInCloud().Result,
            "GENERAL LEDGER" => ExportGeneralLedgerInCloud().Result,
            "SUB LEDGER" => ExportSubLedgerInCloud().Result,
            "COST CENTER" => ExportCostCenterInCloud().Result,
            "DOCUMENT NUMBERING" => ExportDocumentNumberingInCloud().Result,
            _ => 0
        };

    }


    //RACK
    #region ** ---------- RACK ----------**
    private async Task<int> ImportRackInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.RACK r";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<RACK>(cmdString);
        var racks = result as RACK[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<RACK>(cmdString);
        var cloudRack = resultCloud as RACK[] ?? resultCloud.ToArray();

        if (racks.ToList().Count > 0)
        {
            foreach (var group in racks)
            {
                _rackRepository.ObjRack.RID = group.RID;

                if (Array.Exists(cloudRack, x => x.RID == group.RID))
                {
                    if (Array.Exists(cloudRack, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _rackRepository.SaveRack("UPDATE");
                    }
                }
                else
                {
                    _rackRepository.SaveRack("SAVE");
                }

            }
        }
        else
        {
            await ImportRackInCloud();
        }
        return racks.ToList().Count;
    }
    private async Task<int> ExportRackInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.RACK r";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<RACK>(cmdString);
        var racks = result as RACK[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<RACK>(cmdString);
        var cloudRack = resultCloud as RACK[] ?? resultCloud.ToArray();
        if (racks.ToList().Count > 0)
        {
            foreach (var group in racks)
            {
                _rack.RID = group.RID;

                if (Array.Exists(cloudRack, x => x.RID == group.RID))
                {
                    if (Array.Exists(cloudRack, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveRack("UPDATE");
                    }
                }
                else
                {
                    SaveRack("SAVE");
                }

            }
        }
        else
        {
            await ExportRackInCloud();
        }
        return racks.ToList().Count;
    }
    private void SaveRack(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {
            cmdString = $@"
                UPDATE AMS.RACK SET RName='{_rack.RName}', RCode='{_rack.RCode}', Location='{_rack.Location}', Status='{_rack.Status}', EnterBy='{_rack.EnterBy}', EnterDate='{_rack.EnterDate}', BranchId='{_rack.BranchId}', CompanyUnitId='{_rack.CompanyUnitId}', SyncBaseId='{_rack.SyncBaseId}', SyncGlobalId='{_rack.SyncGlobalId}', SyncOriginId='{_rack.SyncOriginId}', SyncCreatedOn='{_rack.SyncCreatedOn}', SyncLastPatchedOn='{_rack.SyncLastPatchedOn}', SyncRowVersion='{_rack.SyncRowVersion}'
                WHERE RID ='';";
        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.RACK(RID, RName, RCode, Location, Status, EnterBy, EnterDate, BranchId, CompanyUnitId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                VALUES({_rack.RID}, N'{_rack.RName}', N'{_rack.RCode}', {_rack.Location}, {_rack.Status}, N'{_rack.EnterBy}', GETDATE(){_rack.EnterDate}, {_rack.BranchId}, {_rack.CompanyUnitId}, {_rack.SyncBaseId}, {_rack.SyncGlobalId}, {_rack.SyncOriginId}, {_rack.SyncCreatedOn}, {_rack.SyncLastPatchedOn}, {_rack.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //MAIN AGENT
    #region ** ----------MAIN AGENT ----------**
    private async Task<int> ImportMainAgentInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.MainAgent sa";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<MainAgent>(cmdString);
        var mainAgents = result as MainAgent[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<MainAgent>(cmdString);
        var cloudMainAgents = resultCloud as MainAgent[] ?? resultCloud.ToArray();

        if (mainAgents.ToList().Count > 0)
        {
            foreach (var group in mainAgents)
            {
                _mainAgentRepository.ObjSeniorAgent.SAgentId = group.SAgentId;

                if (Array.Exists(cloudMainAgents, x => x.SAgentId == group.SAgentId))
                {
                    if (Array.Exists(cloudMainAgents, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _mainAgentRepository.SaveSeniorAgent("UPDATE");
                    }
                }
                else
                {
                    _mainAgentRepository.SaveSeniorAgent("SAVE");
                }

            }
        }
        else
        {
            await ImportMainAgentInCloud();
        }
        return mainAgents.ToList().Count;
    }
    private async Task<int> ExportMainAgentInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.MainAgent sa";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<MainAgent>(cmdString);
        var mainAgents = result as MainAgent[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<MainAgent>(cmdString);
        var cloudMainAgents = resultCloud as MainAgent[] ?? resultCloud.ToArray();
        if (mainAgents.ToList().Count > 0)
        {
            foreach (var group in mainAgents)
            {
                _mainAgent.SAgentId = group.SAgentId;

                if (Array.Exists(cloudMainAgents, x => x.SAgentId == group.SAgentId))
                {
                    if (Array.Exists(cloudMainAgents, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveMainAgent("UPDATE");
                    }
                }
                else
                {
                    SaveMainAgent("SAVE");
                }

            }
        }
        else
        {
            await ExportMainAgentInCloud();
        }
        return mainAgents.ToList().Count;
    }
    private void SaveMainAgent(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {
            cmdString = $@"
                UPDATE AMS.SeniorAgent SET NepaliDesc='{_mainAgent.NepaliDesc}', SAgent='{_mainAgent.SAgent}', SAgentCode='{_mainAgent.SAgentCode}', PhoneNo='{_mainAgent.PhoneNo}', Address='{_mainAgent.Address}', Comm='{_mainAgent.Comm}', TagetLimit='{_mainAgent.TagetLimit}',GLID='{_mainAgent.GLID}', Branch_ID='{_mainAgent.Branch_ID}', Company_Id='{_mainAgent.Company_Id}', Status='{_mainAgent.Status}', IsDefault='{_mainAgent.IsDefault}', EnterBy='{_mainAgent.EnterBy}', EnterDate='{_mainAgent.EnterDate}', SyncBaseId='{_mainAgent.SyncBaseId}', SyncGlobalId='{_mainAgent.SyncGlobalId}', SyncOriginId='{_mainAgent.SyncCreatedOn},', SyncCreatedOn='{_mainAgent.SyncCreatedOn}', SyncLastPatchedOn='{_mainAgent.SyncLastPatchedOn}', SyncRowVersion='{_mainAgent.SyncRowVersion}'
                WHERE SAgentId='';";
        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.SeniorAgent(SAgentId, NepaliDesc, SAgent, SAgentCode, PhoneNo, Address, Comm, TagetLimit, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                VALUES({_mainAgent.SAgentId}, {_mainAgent.NepaliDesc}, N'{_mainAgent.SAgent}', N'{_mainAgent.SAgentCode}', {_mainAgent.PhoneNo}, {_mainAgent.Address}, {_mainAgent.Comm}, {_mainAgent.TagetLimit}, {_mainAgent.GLID}, {_mainAgent.Branch_ID}, {_mainAgent.Company_Id}, {_mainAgent.Status}, {_mainAgent.IsDefault}, N'{_mainAgent.EnterBy}', GETDATE(){_mainAgent.EnterDate}, {_mainAgent.SyncBaseId}, {_mainAgent.SyncGlobalId}, {_mainAgent.SyncOriginId}, {_mainAgent.SyncCreatedOn}, {_mainAgent.SyncLastPatchedOn}, {_mainAgent.SyncRowVersion});";
        }
        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //AREA
    #region ** ---------- AREA ----------**
    private async Task<int> ImportAreaInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.Area a";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<Area>(cmdString);
        var areas = result as Area[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<Area>(cmdString);
        var cloudArea = resultCloud as Area[] ?? resultCloud.ToArray();

        if (areas.ToList().Count > 0)
        {
            foreach (var group in areas)
            {
                _areaRepository.ObjArea.AreaId = group.AreaId;

                if (Array.Exists(cloudArea, x => x.AreaId == group.AreaId))
                {
                    if (Array.Exists(cloudArea, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _areaRepository.SaveArea("UPDATE");
                    }
                }
                else
                {
                    _areaRepository.SaveArea("SAVE");
                }

            }
        }
        else
        {
            await ImportAreaInCloud();
        }
        return areas.ToList().Count;
    }
    private async Task<int> ExportAreaInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.Area a";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<Area>(cmdString);
        var areas = result as Area[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<Area>(cmdString);
        var cloudArea = resultCloud as Area[] ?? resultCloud.ToArray();
        if (areas.ToList().Count > 0)
        {
            foreach (var group in areas)
            {
                _area.AreaId = group.AreaId;

                if (Array.Exists(cloudArea, x => x.AreaId == group.AreaId))
                {
                    if (Array.Exists(cloudArea, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveArea("UPDATE");
                    }
                }
                else
                {
                    SaveArea("SAVE");
                }

            }
        }
        else
        {
            await ExportAreaInCloud();
        }
        return areas.ToList().Count;
    }
    private void SaveArea(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {
            cmdString = $@"
                UPDATE AMS.Area SET NepaliDesc='{_area.NepaliDesc}', AreaName='{_area.AreaName}', AreaCode='{_area.AreaCode}', Country='{_area.Country}' Branch_ID='{_area.Branch_Id}', Company_Id='{_area.CompanyId}', Main_Area='{_area.MainArea}', Status='{_area.Status}', IsDefault='{_area.IsDefault}', EnterBy='{_area.EnterBy}', EnterDate='{_area.EnterDate}', SyncBaseId='{_area.SyncBaseId}', SyncGlobalId='{_area.SyncGlobalId}', SyncOriginId='{_area.SyncOriginId}', SyncCreatedOn='{_area.SyncCreatedOn}', SyncLastPatchedOn='{_area.SyncLastPatchedOn}', SyncRowVersion='{_area.SyncRowVersion}'
                WHERE AreaId='';";
        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.Area(AreaId, NepaliDesc, AreaName, AreaCode, Country, Branch_ID, Company_Id, Main_Area, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                VALUES({_area.AreaId}, {_area.NepaliDesc}, N'{_area.AreaName}', N'{_area.AreaCode}', {_area.Country}, {_area.Branch_Id}, {_area.CompanyId}, {_area.MainArea}, {_area.Status}, {_area.IsDefault}, N'{_area.EnterBy}', GETDATE(){_area.EnterDate}, {_area.SyncBaseId}, {_area.SyncGlobalId}, {_area.SyncOriginId}, {_area.SyncCreatedOn}, {_area.SyncLastPatchedOn}, {_area.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);

    }
    #endregion

    //MAIN AREA
    #region ** ---------- MAIN AREA ----------**
    private async Task<int> ImportMainAreaInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.MainArea ma";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<MainArea>(cmdString);
        var mainAreas = result as MainArea[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<MainArea>(cmdString);
        var cloudMainArea = resultCloud as MainArea[] ?? resultCloud.ToArray();

        if (mainAreas.ToList().Count > 0)
        {
            foreach (var group in mainAreas)
            {
                _mainAreaRepository.ObjMainArea.MAreaId = group.MAreaId;

                if (Array.Exists(cloudMainArea, x => x.MAreaId == group.MAreaId))
                {
                    if (Array.Exists(cloudMainArea, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _mainAreaRepository.SaveMainArea("UPDATE");
                    }
                }
                else
                {
                    _mainAreaRepository.SaveMainArea("SAVE");
                }

            }
        }
        else
        {
            await ImportMainAreaInCloud();
        }
        return mainAreas.ToList().Count;
    }
    private async Task<int> ExportMainAreaInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.MainArea ma";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<MainArea>(cmdString);
        var mainArea = result as MainArea[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<MainArea>(cmdString);
        var cloudMainArea = resultCloud as MainArea[] ?? resultCloud.ToArray();
        if (mainArea.ToList().Count > 0)
        {
            foreach (var group in mainArea)
            {
                _mainArea.MAreaId = group.MAreaId;

                if (Array.Exists(cloudMainArea, x => x.MAreaId == group.MAreaId))
                {
                    if (Array.Exists(cloudMainArea, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveMainArea("UPDATE");
                    }
                }
                else
                {
                    SaveMainArea("SAVE");
                }

            }
        }
        else
        {
            await ExportMainAreaInCloud();
        }
        return mainArea.ToList().Count;
    }
    private void SaveMainArea(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {
            cmdString = $@"
                UPDATE AMS.MainArea SET NepaliDesc='{_mainArea.NepaliDesc}', MAreaName='{_mainArea.MAreaName}', MAreaCode='{_mainArea.MAreaCode}', MCountry='{_mainArea.MCountry}', Branch_ID='{_mainArea.Branch_ID}', Company_Id='{_mainArea.Company_Id}', Status='{_mainArea.Status}', IsDefault='{_mainArea.IsDefault}', EnterBy='{_mainArea.EnterBy}', EnterDate='{_mainArea.EnterDate}', SyncBaseId='{_mainArea.SyncBaseId}', SyncGlobalId='{_mainArea.SyncGlobalId}', SyncOriginId='{_mainArea.SyncOriginId}', SyncCreatedOn='{_mainArea.SyncCreatedOn}', SyncLastPatchedOn='{_mainArea.SyncLastPatchedOn}', SyncRowVersion='{_mainArea.SyncRowVersion}'
                WHERE MAreaId='';";
        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.MainArea(MAreaId, NepaliDesc, MAreaName, MAreaCode, MCountry, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                VALUES({_mainArea.MAreaId}, {_mainArea.NepaliDesc}, N'{_mainArea.MAreaName}', N'{_mainArea.MAreaCode}', {_mainArea.MCountry}, {_mainArea.Branch_ID}, {_mainArea.Company_Id}, {_mainArea.Status}, {_mainArea.IsDefault}, N'{_mainArea.EnterBy}', GETDATE(){_mainArea.EnterDate}, {_mainArea.EnterDate},{_mainArea.SyncBaseId},{_mainArea.SyncGlobalId},{_mainArea.SyncOriginId},{_mainArea.SyncCreatedOn},{_mainArea.SyncLastPatchedOn},{_mainArea.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //PRODUCT GROUP
    #region ** ----------PRODUCT GROUP ----------**
    private async Task<int> ImportProductGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.Product Group pg";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<ProductGroup>(cmdString);
        var productGroups = result as ProductGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<ProductGroup>(cmdString);
        var cloudProductGroup = resultCloud as ProductGroup[] ?? resultCloud.ToArray();

        if (productGroups.ToList().Count > 0)
        {
            foreach (var group in productGroups)
            {
                _productGroupRepository.ObjProductGroup.PGrpId = group.PGrpId;

                if (Array.Exists(cloudProductGroup, x => x.PGrpId == group.PGrpId))
                {
                    if (Array.Exists(cloudProductGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _productGroupRepository.SaveProductGroup("UPDATE");
                    }
                }
                else
                {
                    _productGroupRepository.SaveProductGroup("SAVE");
                }

            }
        }
        else
        {
            await ImportProductGroupInCloud();
        }
        return productGroups.ToList().Count;
    }
    private async Task<int> ExportProductGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.ProductGroup pg";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<ProductGroup>(cmdString);
        var productGroups = result as ProductGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<ProductGroup>(cmdString);
        var cloudProductGroup = resultCloud as ProductGroup[] ?? resultCloud.ToArray();
        if (productGroups.ToList().Count > 0)
        {
            foreach (var group in productGroups)
            {
                _accountGroup.GrpId = group.PGrpId;

                if (Array.Exists(cloudProductGroup, x => x.PGrpId == group.PGrpId))
                {
                    if (Array.Exists(cloudProductGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveProductGroup("UPDATE");
                    }
                }
                else
                {
                    SaveProductGroup("SAVE");
                }

            }
        }
        else
        {
            await ExportProductGroupInCloud();
        }
        return productGroups.ToList().Count;
    }
    private void SaveProductGroup(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {
            cmdString = $@"
                UPDATE AMS.ProductGroup SET NepaliDesc='{_productGroup.NepaliDesc}', GrpName='{_productGroup.GrpName}, GrpCode='{_productGroup.GrpCode}', GMargin='{_productGroup.GMargin}', Gprinter='{_productGroup.GPrinter}', PurchaseLedgerId='{_productGroup.PurchaseLedgerId}', PurchaseReturnLedgerId='{_productGroup.PurchaseReturnLedgerId}', SalesLedgerId='{_productGroup.SalesLedgerId}', SalesReturnLedgerId='{_productGroup.SalesReturnLedgerId}', OpeningStockLedgerId='{_productGroup.OpeningStockLedgerId}', ClosingStockLedgerId='{_productGroup.ClosingStockLedgerId}', StockInHandLedgerId='{_productGroup.StockInHandLedgerId}', Branch_ID='{_productGroup.Branch_ID}', Company_Id='{_productGroup.Company_ID}', Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion
                WHERE PGrpId='';";
        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.ProductGroup(PGrpId, NepaliDesc, GrpName, GrpCode, GMargin, Gprinter, PurchaseLedgerId, PurchaseReturnLedgerId, SalesLedgerId, SalesReturnLedgerId, OpeningStockLedgerId, ClosingStockLedgerId, StockInHandLedgerId, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                VALUES({_productGroup.PGrpId}, {_productGroup.NepaliDesc}, N'{_productGroup.GrpName}', N'{_productGroup.GrpCode}', {_productGroup.GMargin}, {_productGroup.GPrinter}, {_productGroup.PurchaseLedgerId}, {_productGroup.PurchaseReturnLedgerId}, {_productGroup.SalesLedgerId}, {_productGroup.SalesReturnLedgerId}, {_productGroup.OpeningStockLedgerId}, {_productGroup.ClosingStockLedgerId}, {_productGroup.StockInHandLedgerId}, {_productGroup.Branch_ID}, {_productGroup.Company_ID}, {_productGroup.Status}, N'{_productGroup.EnterBy}', GETDATE(){_productGroup.EnterDate}, {_productGroup.SyncBaseId}, {_productGroup.SyncGlobalId}, {_productGroup.SyncOriginId}, {_productGroup.SyncCreatedOn}, {_productGroup.SyncLastPatchedOn}, {_productGroup.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //PRODUCT SUB GROUP
    #region ** ---------- PRODUCT SUB GROUP ----------**
    private async Task<int> ImportProductSubGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.ProductSubGroup psg";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<ProductSubGroup>(cmdString);
        var productSubGroups = result as ProductSubGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<ProductSubGroup>(cmdString);
        var cloudProductSubGroup = resultCloud as ProductSubGroup[] ?? resultCloud.ToArray();

        if (productSubGroups.ToList().Count > 0)
        {
            foreach (var group in productSubGroups)
            {
                _productSubGroupRepository.ObjProductSubGroup.PSubGrpId = group.PSubGrpId;

                if (Array.Exists(cloudProductSubGroup, x => x.PSubGrpId == group.PSubGrpId))
                {
                    if (Array.Exists(cloudProductSubGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _productSubGroupRepository.SaveProductSubGroup("UPDATE");
                    }
                }
                else
                {
                    _productSubGroupRepository.SaveProductSubGroup("SAVE");
                }

            }
        }
        else
        {
            await ImportProductGroupInCloud();
        }
        return productSubGroups.ToList().Count;
    }
    private async Task<int> ExportProductSubGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.ProductSubGroup psg";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<ProductSubGroup>(cmdString);
        var productSubGroups = result as ProductSubGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<ProductSubGroup>(cmdString);
        var cloudProductSubGroup = resultCloud as ProductSubGroup[] ?? resultCloud.ToArray();
        if (productSubGroups.ToList().Count > 0)
        {
            foreach (var group in productSubGroups)
            {
                _accountGroup.GrpId = group.GrpId;

                if (Array.Exists(cloudProductSubGroup, x => x.GrpId == group.GrpId))
                {
                    if (Array.Exists(cloudProductSubGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveProductSubGroup("UPDATE");
                    }
                }
                else
                {
                    SaveProductSubGroup("SAVE");
                }

            }
        }
        else
        {
            await ExportProductSubGroupInCloud();
        }
        return productSubGroups.ToList().Count;
    }
    private void SaveProductSubGroup(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.ProductSubGroup(PSubGrpId, NepaliDesc, SubGrpName, ShortName, GrpId, Branch_ID, Company_Id, EnterBy, EnterDate, IsDefault, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                VALUES({_productSubGroup.PSubGrpId}, {_productSubGroup.NepaliDesc}, N'{_productSubGroup.SubGrpName}', N'{_productSubGroup.ShortName}', {_productSubGroup.GrpId}, {_productSubGroup.Branch_ID}, {_productSubGroup.Company_Id}, N'{_productSubGroup.EnterBy}', GETDATE(){_productSubGroup.EnterDate}, {_productSubGroup.IsDefault}, {_productSubGroup.Status}, {_productSubGroup.SyncBaseId}, {_productSubGroup.SyncGlobalId}, {_productSubGroup.SyncOriginId}, {_productSubGroup.SyncCreatedOn}, {_productSubGroup.SyncLastPatchedOn}, {_productSubGroup.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //PRODUCT UNIT
    #region ** ---------- PRODUCT UNIT ----------**
    private async Task<int> ImportProductUnitInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.ProductUnit pu";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<ProductUnit>(cmdString);
        var productUnits = result as ProductUnit[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<ProductUnit>(cmdString);
        var cloudProductUnit = resultCloud as ProductUnit[] ?? resultCloud.ToArray();

        if (productUnits.ToList().Count > 0)
        {
            foreach (var group in productUnits)
            {
                _productUnitRepository.ObjProductUnit.UID = group.UID;

                if (Array.Exists(cloudProductUnit, x => x.UID == group.UID))
                {
                    if (Array.Exists(cloudProductUnit, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _productUnitRepository.SaveProductUnit("UPDATE");
                    }
                }
                else
                {
                    _productUnitRepository.SaveProductUnit("SAVE");
                }

            }
        }
        else
        {
            await ImportProductUnitInCloud();
        }
        return productUnits.ToList().Count;
    }
    private async Task<int> ExportProductUnitInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.ProductUnit pu";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<ProductUnit>(cmdString);
        var productUnits = result as ProductUnit[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<ProductUnit>(cmdString);
        var cloudProductUnit = resultCloud as ProductUnit[] ?? resultCloud.ToArray();
        if (productUnits.ToList().Count > 0)
        {
            foreach (var group in productUnits)
            {
                _productUnit.UID = group.UID;

                if (Array.Exists(cloudProductUnit, x => x.UID == group.UID))
                {
                    if (Array.Exists(cloudProductUnit, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveProductUnit("UPDATE");
                    }
                }
                else
                {
                    SaveProductUnit("SAVE");
                }

            }
        }
        else
        {
            await ExportProductUnitInCloud();
        }
        return productUnits.ToList().Count;
    }
    private void SaveProductUnit(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.ProductUnit(UID, NepaliDesc, UnitName, UnitCode, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                VALUES({_productUnit.UID},{_productUnit.NepaliDesc},N'{_productUnit.UnitName}',N'{_productUnit.UnitCode}',{_productUnit.Branch_ID},{_productUnit.Company_Id},N'{_productUnit.EnterBy}',GETDATE(){_productUnit.EnterDate},{_productUnit.Status},{_productUnit.IsDefault},{_productUnit.SyncBaseId},{_productUnit.SyncGlobalId},{_productUnit.SyncOriginId},{_productUnit.SyncCreatedOn},{_productUnit.SyncLastPatchedOn},{_productUnit.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    // ACCOUNT GROUP
    #region ** ----------  ACCOUNT GROUP ---------- **
    private async Task<int> ImportAccountGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.AccountGroup ag";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<AccountGroup>(cmdString);
        var accountGroups = result as AccountGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<AccountGroup>(cmdString);
        var cloudAccountGroup = resultCloud as AccountGroup[] ?? resultCloud.ToArray();

        if (accountGroups.ToList().Count > 0)
        {
            foreach (var group in accountGroups)
            {
                _groupRepository.ObjAccountGroup.GrpId = group.GrpId;

                if (Array.Exists(cloudAccountGroup, x => x.GrpId == group.GrpId))
                {
                    if (Array.Exists(cloudAccountGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _groupRepository.SaveAccountGroup("UPDATE");
                    }
                }
                else
                {
                    _groupRepository.SaveAccountGroup("SAVE");
                }

            }
        }
        else
        {
            await ImportAccountGroupInCloud();
        }
        return accountGroups.ToList().Count;
    }
    private async Task<int> ExportAccountGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.AccountGroup ag";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<AccountGroup>(cmdString);
        var accountGroups = result as AccountGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<AccountGroup>(cmdString);
        var cloudAccountGroup = resultCloud as AccountGroup[] ?? resultCloud.ToArray();
        if (accountGroups.ToList().Count > 0)
        {
            foreach (var group in accountGroups)
            {
                _accountGroup.GrpId = group.GrpId;

                if (Array.Exists(cloudAccountGroup, x => x.GrpId == group.GrpId))
                {
                    if (Array.Exists(cloudAccountGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveAccountGroup("UPDATE");
                    }
                }
                else
                {
                    SaveAccountGroup("SAVE");
                }

            }
        }
        else
        {
            await ExportAccountGroupInCloud();
        }
        return accountGroups.ToList().Count;
    }
    private void SaveAccountGroup(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.AccountGroup (GrpId,NepaliDesc,GrpName,GrpCode,Schedule,PrimaryGrp,GrpType,Branch_ID,Company_Id,Status,EnterBy,EnterDate,PrimaryGroupId,IsDefault,SyncBaseId,SyncGlobalId,SyncOriginId,SyncCreatedOn,SyncLastPatchedOn,SyncRowVersion)
                VALUES({_accountGroup.GrpId},N'{_accountGroup.NepaliDesc}',N'{_accountGroup.GrpName}',N'{_accountGroup.GrpCode}',{_accountGroup.Schedule},'{_accountGroup.PrimaryGrp}','{_accountGroup.GrpType}',{_accountGroup.Branch_ID},{_accountGroup.Company_Id},{_accountGroup.Status},N'{_accountGroup.EnterBy}',GETDATE(){_accountGroup.PrimaryGroupId},{_accountGroup.IsDefault},{_accountGroup.SyncBaseId},{_accountGroup.SyncGlobalId},{_accountGroup.SyncOriginId},GETDATE(),GETDATE(),{_accountGroup.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    // ACCOUNT SUB GROUP
    #region  **---------- ACCOUNT SUB GROUP --------**
    private async Task<int> ImportAccountSubGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.AccountSubGroup asg";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<AccountSubGroup>(cmdString);
        var accountSubGroups = result as AccountSubGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<AccountSubGroup>(cmdString);
        var cloudAccountSubGroup = resultCloud as AccountSubGroup[] ?? resultCloud.ToArray();

        if (accountSubGroups.ToList().Count > 0)
        {
            foreach (var group in accountSubGroups)
            {
                _subgroupRepository.ObjAccountSubGroup.SubGrpId = group.SubGrpId;

                if (Array.Exists(cloudAccountSubGroup, x => x.SubGrpId == group.SubGrpId))
                {
                    if (Array.Exists(cloudAccountSubGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _subgroupRepository.SaveAccountSubGroup("UPDATE");
                    }
                }
                else
                {
                    _subgroupRepository.SaveAccountSubGroup("SAVE");
                }

            }
        }
        else
        {
            await ImportAccountSubGroupInCloud();
        }
        return accountSubGroups.ToList().Count;
    }
    private async Task<int> ExportAccountSubGroupInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.AccountSubGroup asg";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<AccountSubGroup>(cmdString);
        var accountSubGroups = result as AccountSubGroup[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<AccountSubGroup>(cmdString);
        var cloudAccountSubGroup = resultCloud as AccountSubGroup[] ?? resultCloud.ToArray();
        if (accountSubGroups.ToList().Count > 0)
        {
            foreach (var group in accountSubGroups)
            {
                _accountSubGroup.SubGrpId = group.SubGrpId;

                if (Array.Exists(cloudAccountSubGroup, x => x.SubGrpId == group.SubGrpId))
                {
                    if (Array.Exists(cloudAccountSubGroup, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveAccountSubGroup("UPDATE");
                    }
                }
                else
                {
                    SaveAccountSubGroup("SAVE");
                }

            }
        }
        else
        {
            await ExportAccountSubGroupInCloud();
        }
        return accountSubGroups.ToList().Count;
    }
    private void SaveAccountSubGroup(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                 INSERT INTO AMS.AccountSubGroup(SubGrpId, NepaliDesc, SubGrpName, GrpId, SubGrpCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, PrimaryGroupId, PrimarySubGroupId, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                 VALUES({_accountSubGroup.SubGrpId},{_accountSubGroup.NepaliDesc},N'{_accountSubGroup.SubGrpName}',{_accountSubGroup.GrpId}, N'{_accountSubGroup.SubGrpCode}' , {_accountSubGroup.Branch_ID} , {_accountSubGroup.Company_Id}, {_accountSubGroup.Status},N'{_accountSubGroup.EnterBy}' , GETDATE(){_accountSubGroup.EnterDate},{_accountSubGroup.PrimaryGroupId}, {_accountSubGroup.PrimarySubGroupId}, {_accountSubGroup.IsDefault}, {_accountSubGroup.SyncBaseId}, {_accountSubGroup.SyncGlobalId},{_accountSubGroup.SyncOriginId}, {_accountSubGroup.SyncCreatedOn}, {_accountSubGroup.SyncLastPatchedOn},{_accountSubGroup.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //CURRENCY
    #region ** ---------- CURRENCY ----------**
    private async Task<int> ImportCurrencyInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.Currency c";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<Currency>(cmdString);
        var currencies = result as Currency[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<Currency>(cmdString);
        var cloudCurrency = resultCloud as Currency[] ?? resultCloud.ToArray();

        if (currencies.ToList().Count > 0)
        {
            foreach (var group in currencies)
            {
                _currencyRepository.ObjCurrency.CId = group.CId;

                if (Array.Exists(cloudCurrency, x => x.CId == group.CId))
                {
                    if (Array.Exists(cloudCurrency, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _currencyRepository.SaveCurrency("UPDATE");
                    }
                }
                else
                {
                    _currencyRepository.SaveCurrency("SAVE");
                }

            }
        }
        else
        {
            await ImportCurrencyInCloud();
        }
        return currencies.ToList().Count;
    }
    private async Task<int> ExportCurrencyInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.Currrency c";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<Currency>(cmdString);
        var currencies = result as Currency[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<Currency>(cmdString);
        var cloudCurrency = resultCloud as Currency[] ?? resultCloud.ToArray();
        if (currencies.ToList().Count > 0)
        {
            foreach (var group in currencies)
            {
                _currency.CId = group.CId;

                if (Array.Exists(cloudCurrency, x => x.CId == group.CId))
                {
                    if (Array.Exists(cloudCurrency, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveCurrency("UPDATE");
                    }
                }
                else
                {
                    SaveCurrency("SAVE");
                }

            }
        }
        else
        {
            await ExportCurrencyInCloud();
        }
        return currencies.ToList().Count;
    }
    private void SaveCurrency(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.Currency(CId, NepaliDesc, CName, CCode, CRate, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                VALUES({_currency.CId}, {_currency.NepaliDesc}, N'{_currency.CName}', N'{_currency.CCode}', {_currency.Branch_ID}, {_currency.Company_Id}, {_currency.Status}, {_currency.IsDefault}, {_currency.Status}, N'{_currency.EnterBy}', GETDATE(){_currency.EnterDate}, {_currency.SyncBaseId}, {_currency.SyncGlobalId}, {_currency.SyncOriginId}, {_currency.SyncCreatedOn}, {_currency.SyncLastPatchedOn}, {_currency.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //GENERAL LEDGER
    #region ** ---------- GENERAL LEDGER ----------**
    private async Task<int> ImportGeneralLedgerInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.GeneralLedger gl";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<GeneralLedger>(cmdString);
        var generalLedgers = result as GeneralLedger[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<GeneralLedger>(cmdString);
        var cloudGeneralLedger = resultCloud as GeneralLedger[] ?? resultCloud.ToArray();

        if (generalLedgers.ToList().Count > 0)
        {
            foreach (var group in generalLedgers)
            {
                _generalLedgerRepository.ObjGeneralLedger.GLID = group.GLID;

                if (Array.Exists(cloudGeneralLedger, x => x.GLID == group.GLID))
                {
                    if (Array.Exists(cloudGeneralLedger, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _generalLedgerRepository.SaveGeneralLedger("UPDATE");
                    }
                }
                else
                {
                    _generalLedgerRepository.SaveGeneralLedger("SAVE");
                }

            }
        }
        else
        {
            await ImportGeneralLedgerInCloud();
        }
        return generalLedgers.ToList().Count;
    }
    private async Task<int> ExportGeneralLedgerInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.GeneralLedger gl";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<GeneralLedger>(cmdString);
        var generalLedgers = result as GeneralLedger[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<GeneralLedger>(cmdString);
        var cloudGeneralLedger = resultCloud as GeneralLedger[] ?? resultCloud.ToArray();
        if (generalLedgers.ToList().Count > 0)
        {
            foreach (var group in generalLedgers)
            {
                _generalLedger.GLID = group.GLID;

                if (Array.Exists(cloudGeneralLedger, x => x.GLID == group.GLID))
                {
                    if (Array.Exists(cloudGeneralLedger, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveGeneralLedger("UPDATE");
                    }
                }
                else
                {
                    SaveGeneralLedger("SAVE");
                }

            }
        }
        else
        {
            await ExportGeneralLedgerInCloud();
        }
        return generalLedgers.ToList().Count;
    }
    private void SaveGeneralLedger(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                 INSERT INTO AMS.GeneralLedger(GLID, NepaliDesc, GLName, GLCode, ACCode, GLType, GrpId, PrimaryGroupId, SubGrpId, PrimarySubGroupId, PanNo, AreaId, AgentId, CurrId, CrDays, CrLimit, CrTYpe, IntRate, GLAddress, PhoneNo, LandLineNo, OwnerName, OwnerNumber, Scheme, Email, Branch_ID, Company_Id, EnterBy, EnterDate, Status, IsDefault, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                 VALUES({_generalLedger.GLID}, {_generalLedger.NepaliDesc}, N'{_generalLedger.GLName}', N'{_generalLedger.GLCode}', N'{_generalLedger.ACCode}', N'{_generalLedger.GLType}', {_generalLedger.GrpId}, {_generalLedger.PrimaryGroupId}, {_generalLedger.SubGrpId}, {_generalLedger.PrimarySubGroupId}, {_generalLedger.PanNo}, {_generalLedger.AreaId}, {_generalLedger.AgentId}, {_generalLedger.CurrId}, {_generalLedger.CrDays}, {_generalLedger.CrLimit}, '{_generalLedger.CrTYpe}', {_generalLedger.IntRate}, {_generalLedger.GLAddress}, {_generalLedger.PhoneNo}, {_generalLedger.LandLineNo}, {_generalLedger.OwnerName}, {_generalLedger.OwnerNumber}, {_generalLedger.Scheme}, {_generalLedger.Email}, {_generalLedger.Branch_ID}, {_generalLedger.Company_Id}, N'{_generalLedger.EnterBy}', GETDATE(){_generalLedger.EnterDate}, {_generalLedger.Status}, {_generalLedger.IsDefault}, {_generalLedger.SyncBaseId}, {_generalLedger.SyncGlobalId}, {_generalLedger.SyncOriginId}, {_generalLedger.SyncCreatedOn}, {_generalLedger.SyncLastPatchedOn}, {_generalLedger.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //SUB LEDGER
    #region **---------- SUB LEDGER ----------**
    private async Task<int> ImportSubLedgerInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.SubLedger sl";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<SubLedger>(cmdString);
        var subLedgers = result as SubLedger[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<SubLedger>(cmdString);
        var cloudSubLedger = resultCloud as SubLedger[] ?? resultCloud.ToArray();

        if (subLedgers.ToList().Count > 0)
        {
            foreach (var group in subLedgers)
            {
                _subLedgerRepository.ObjSubLedger.SLId = group.SLId;

                if (Array.Exists(cloudSubLedger, x => x.SLId == group.SLId))
                {
                    if (Array.Exists(cloudSubLedger, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _subLedgerRepository.SaveSubLedger("UPDATE");
                    }
                }
                else
                {
                    _subLedgerRepository.SaveSubLedger("SAVE");
                }

            }
        }
        else
        {
            await ImportAccountGroupInCloud();
        }
        return subLedgers.ToList().Count;
    }
    private async Task<int> ExportSubLedgerInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.SubLedger sl";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<SubLedger>(cmdString);
        var subLedgers = result as SubLedger[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<SubLedger>(cmdString);
        var cloudSubLedger = resultCloud as SubLedger[] ?? resultCloud.ToArray();
        if (subLedgers.ToList().Count > 0)
        {
            foreach (var group in subLedgers)
            {
                _subLedger.SLId = group.SLId;

                if (Array.Exists(cloudSubLedger, x => x.SLId == group.SLId))
                {
                    if (Array.Exists(cloudSubLedger, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveSubLedger("UPDATE");
                    }
                }
                else
                {
                    SaveSubLedger("SAVE");
                }

            }
        }
        else
        {
            await ExportSubLedgerInCloud();
        }
        return subLedgers.ToList().Count;
    }
    private void SaveSubLedger(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.Subledger(SLId, NepalDesc, SLName, SLCode, SLAddress, SLPhoneNo, GLID, Branch_ID, Company_Id, Status, IsDefault, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                VALUES({_subLedger.SLId}, {_subLedger.NepalDesc}, N'{_subLedger.SLName}', N'{_subLedger.SLCode}', {_subLedger.SLAddress}, {_subLedger.SLPhoneNo}, {_subLedger.GLID}, {_subLedger.Branch_ID}, {_subLedger.Company_Id}, {_subLedger.Status}, {_subLedger.IsDefault}, N'{_subLedger.EnterBy}, GETDATE(){_subLedger.EnterDate}, {_subLedger.SyncBaseId}, {_subLedger.SyncGlobalId}, {_subLedger.SyncOriginId}, {_subLedger.SyncCreatedOn},{_subLedger.SyncLastPatchedOn}, {_subLedger.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //COST CENTER
    #region ** ---------- COST CENTER ----------**
    private async Task<int> ImportCostCenterInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.CostCenter cc";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<CostCenter>(cmdString);
        var costCenters = result as CostCenter[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<CostCenter>(cmdString);
        var cloudCostCenter = resultCloud as CostCenter[] ?? resultCloud.ToArray();

        if (costCenters.ToList().Count > 0)
        {
            foreach (var group in costCenters)
            {
                _costCenterRepository.ObjCostCenter.CCId = group.CCId;

                if (Array.Exists(cloudCostCenter, x => x.CCId == group.CCId))
                {
                    if (Array.Exists(cloudCostCenter, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _costCenterRepository.SaveCostCenter("UPDATE");
                    }
                }
                else
                {
                    _costCenterRepository.SaveCostCenter("SAVE");
                }

            }
        }
        else
        {
            await ImportCostCenterInCloud();
        }
        return costCenters.ToList().Count;
    }
    private async Task<int> ExportCostCenterInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.CostCenter cc";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<CostCenter>(cmdString);
        var costCenters = result as CostCenter[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<CostCenter>(cmdString);
        var cloudCostCenter = resultCloud as CostCenter[] ?? resultCloud.ToArray();
        if (costCenters.ToList().Count > 0)
        {
            foreach (var group in costCenters)
            {
                _costCenter.CCId = group.CCId;

                if (Array.Exists(cloudCostCenter, x => x.CCId == group.CCId))
                {
                    if (Array.Exists(cloudCostCenter, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveCostCenter("UPDATE");
                    }
                }
                else
                {
                    SaveCostCenter("SAVE");
                }

            }
        }
        else
        {
            await ExportCostCenterInCloud();
        }
        return costCenters.ToList().Count;
    }
    private void SaveCostCenter(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.CostCenter(CCId, CCName, CCcode, GodownId, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) 
                VALUES({_costCenter.CCId}, N'{_costCenter.CCName}', N'{_costCenter.CCcode}', {_costCenter.GodownId}, {_costCenter.Branch_ID}, {_costCenter.Company_Id}, {_costCenter.Status}, N'{_costCenter.EnterBy}', GETDATE(){_costCenter.EnterDate}, {_costCenter.SyncBaseId}, {_costCenter.SyncGlobalId}, {_costCenter.SyncOriginId}, {_costCenter.SyncCreatedOn}, {_costCenter.SyncLastPatchedOn}, {_costCenter.SyncRowVersion});";
        }

        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }
    #endregion

    //DOCUMENT NUMBERING
    #region ** ---------- DOCUMENT NUMBERING ----------**
    private async Task<int> ImportDocumentNumberingInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.DocumentNumbering dn";
        var conn = new SqlConnection(GetConnection.CloudConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<DocumentNumbering>(cmdString);
        var documentNumberings = result as DocumentNumbering[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.ConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<DocumentNumbering>(cmdString);
        var cloudDocumentNumbering = resultCloud as DocumentNumbering[] ?? resultCloud.ToArray();

        if (documentNumberings.ToList().Count > 0)
        {
            foreach (var group in documentNumberings)
            {
                _documentNumberingRepository.ObjDocumentNumbering.DocId = group.DocId;

                if (Array.Exists(cloudDocumentNumbering, x => x.DocId == group.DocId))
                {
                    if (Array.Exists(cloudDocumentNumbering, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        _documentNumberingRepository.SaveDocumentNumbering("UPDATE");
                    }
                }
                else
                {
                    _documentNumberingRepository.SaveDocumentNumbering("SAVE");
                }

            }
        }
        else
        {
            await ImportDocumentNumberingInCloud();
        }
        return documentNumberings.ToList().Count;
    }
    private async Task<int> ExportDocumentNumberingInCloud()
    {
        const string cmdString = " SELECT * FROM AMS.DocumentNumbering dn";
        var conn = new SqlConnection(GetConnection.ConnectionString);
        await conn.OpenAsync(CancellationToken.None);
        var result = await conn.QueryAsync<DocumentNumbering>(cmdString);
        var documentNumberings = result as DocumentNumbering[] ?? result.ToArray();

        conn = new SqlConnection(GetConnection.CloudConnectionString);

        await conn.OpenAsync(CancellationToken.None);
        var resultCloud = await conn.QueryAsync<DocumentNumbering>(cmdString);
        var cloudDocumentNumbering = resultCloud as DocumentNumbering[] ?? resultCloud.ToArray();
        if (documentNumberings.ToList().Count > 0)
        {
            foreach (var group in documentNumberings)
            {
                _documentNumbering.DocId = group.DocId;

                if (Array.Exists(cloudDocumentNumbering, x => x.DocId == group.DocId))
                {
                    if (Array.Exists(cloudDocumentNumbering, x => x.SyncRowVersion < group.SyncRowVersion))
                    {
                        SaveDocumentNumbering("UPDATE");
                    }
                }
                else
                {
                    SaveDocumentNumbering("SAVE");
                }

            }
        }
        else
        {
            await ExportDocumentNumberingInCloud();
        }
        return documentNumberings.ToList().Count;
    }
    private void SaveDocumentNumbering(string tag)
    {
        var cmdString = string.Empty;
        if (tag is "UPDATE")
        {

        }
        else if (tag is "SAVE")
        {
            cmdString = $@"
                INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocUser, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                VALUES({_documentNumbering.DocId}, '{_documentNumbering.DocModule}', N'{_documentNumbering.DocDesc}', GETDATE(){_documentNumbering.DocStartDate}, '{_documentNumbering.DocStartMiti}', GETDATE(){_documentNumbering.DocEndDate}, '{_documentNumbering.DocEndMiti}', {_documentNumbering.DocUser}, '{_documentNumbering.DocType}', {_documentNumbering.DocPrefix}, {_documentNumbering.DocSufix}, {_documentNumbering.DocBodyLength}, {_documentNumbering.DocTotalLength}, {_documentNumbering.DocBlank}, '{_documentNumbering.DocBlankCh}', {_documentNumbering.DocBranch}, {_documentNumbering.DocUnit}, {_documentNumbering.DocStart}, {_documentNumbering.DocCurr}, {_documentNumbering.DocEnd}, {_documentNumbering.DocDesign}, {_documentNumbering.Status},N'{_documentNumbering.EnterBy}, GETDATE(){_documentNumbering.EnterDate}, {_documentNumbering.FiscalYearId}, {_documentNumbering.SyncBaseId}, {_documentNumbering.SyncGlobalId}, {_documentNumbering.SyncOriginId}, {_documentNumbering.SyncCreatedOn}, {_documentNumbering.SyncLastPatchedOn},{_documentNumbering.SyncRowVersion});";
        }
        var result = SqlExtensions.ExecuteNonTransAsync(GetConnection.CloudConnectionString);
    }


    #endregion




    // OBJECT FOR THIS FORM
    #region ** ---------- OBJECT FOR THIS FORM ---------- **
    //GENERAL LEDGER
    private GeneralLedger _generalLedger;
    private IGeneralLedgerRepository _generalLedgerRepository;

    //SUB LEDGER
    private SubLedger _subLedger;
    private ISubLedgerRepository _subLedgerRepository;

    //WAREHOUSE
    private Currency _currency;
    private ICurrencyRepository _currencyRepository;

    //DEPARTMENT
    private Department _department;
    private IDepartmentRepository _departmentRepository;


    //MAIN AGENT
    private MainAgent _mainAgent;
    private IMainAgentRepository _mainAgentRepository;

    //PRODUCT GROUP
    private ProductGroup _productGroup;
    private IProductGroupRepository _productGroupRepository;

    //PRODUCT SUB GROUP
    private ProductSubGroup _productSubGroup;
    private IProductSubGroupRepository _productSubGroupRepository;

    //PRODUCT UNIT
    private ProductUnit _productUnit;
    private IProductUnitRepository _productUnitRepository;

    // ACCOUNT GROUP
    private AccountGroup _accountGroup = new();
    private IAccountGroupRepository _groupRepository = new AccountGroupRepository();

    // ACCOUNT SUB GROUP
    private AccountSubGroup _accountSubGroup;
    private IAccountSubGroupRepository _subgroupRepository;

    //MAIN AREA
    private MainArea _mainArea;
    private IMainAreaRepository _mainAreaRepository;

    //AREA
    private Area _area;
    private IAreaRepository _areaRepository;

    //RACK
    private RACK _rack;
    private IRackRepository _rackRepository;

    //COST CENTER
    private CostCenter _costCenter;
    private ICostCenterRepository _costCenterRepository;

    //DOCUMENT NUMBERING
    private DocumentNumbering _documentNumbering;
    private IDocumentNumberingRepository _documentNumberingRepository;



    #endregion

}