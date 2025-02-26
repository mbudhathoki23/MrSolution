using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.StockTransaction.ProductScheme;
using DatabaseModule.Domains.BarcodePrint;
using DatabaseModule.Domains.SmsConfig;
using DatabaseModule.Master.FinanceSetup;
using DatabaseModule.Master.InventorySetup;
using DatabaseModule.Master.LedgerSetup;
using DatabaseModule.Master.ProductSetup;
using DatabaseModule.Setup.DocumentNumberings;
using DatabaseModule.Setup.LogSetting;
using DatabaseModule.Setup.SecurityRights;
using DatabaseModule.Setup.TermSetup;
using DatabaseModule.Setup.UserSetup;
using DevExpress.XtraPrinting.BarCode.Native;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Global.Common;
using MrDAL.Master.Interface;
using MrDAL.Models.Common;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseModule.Setup.CompanyMaster;

// ReSharper disable All

namespace MrDAL.Master;

public class ClsMasterSetup : IMasterSetup
{

    // MASTER SETUP FORM

    #region --------------- Constructor ---------------

    public ClsMasterSetup()
    {
        GetRights = new List<BranchRights>();
        ObjSms = new SMS_CONFIG();

        ObjCostCenter = new CostCenter();
        ObjDepartment = new Department();
        Currency = new Currency();
        ObjDocumentNumbering = new DocumentNumbering();
        ObjGodown = new Godown();
        ObjJuniorAgent = new JuniorAgent();
        ObjMainArea = new MainArea();
        ObjArea = new Area();
        ObjGeneralLedger = new GeneralLedger();
        ObjProduct = new Product();
        ObjProductGroup = new ProductGroup();
        ObjProductSubGroup = new ProductSubGroup();
        ObjProductUnit = new ProductUnit();
        ObjSeniorAgent = new MainAgent();
        ObjSubLedger = new SubLedger();
        ObjCounter = new Counter();
        ObjMembershipSetup = new MemberShipSetup();
        ObjMemberType = new MemberType();
        Floor = new FloorSetup();
        ObjRack = new RACK();
        ObjSync = new SyncTable();
        ObjBranch = new Branch();
        CompanyUnit = new CompanyUnit();
        BarcodeList = new BarcodeList();
        Narration = new NR_Master();
        BrMaster = new BR_LOG();
        LoginLog = new LOGIN_LOG();
        BookDetails = new BookDetails();
        SchemeMaster = new Scheme_Master();
        PtTerm = new PT_Term();
        StTerm = new ST_Term();
        GiftVoucherList = new GiftVoucherList();
        ObjSyncLogDetail = new SyncLogDetails();
        ObjUserAccessControl = new UserAccessControl();
    }

    #endregion --------------- Constructor ---------------


    // RETURN VALUE IN DATA SET

    #region --------------- RETURN VALUE IN DATA SET ---------------

    public DataSet GetProductScheme(int schemeId)
    {
        var cmdString = @$"
			SELECT SchemeId, SchemeDate, SchemeMiti, SchemeTime, SchemeDesc, ValidFrom, ValidFromMiti, ValidTo, ValidToMiti, EnterBy, EnterDate, IsActive, BranchId, CompanyUnitId, Remarks, FiscalYearId
			 FROM AMS.Scheme_Master sm
			 WHERE SchemeId = '{schemeId}';
			SELECT SchemeId, SNo, ProductId,p.PName,p.PShortName,p.PUnit,pu.UnitCode,GroupId, SubGroupId, Qty, DiscountPercent, DiscountValue, MinValue, MaxValue, SalesRate, MrpRate, OfferRate
			 FROM AMS.Scheme_Details sd
			 LEFT OUTER JOIN AMS.Product p ON p.PID = sd.ProductId
			 LEFT OUTER	JOIN AMS.ProductUnit pu ON pu.UID = p.PUnit
			 WHERE SchemeId = '{schemeId}'; ";
        return SqlExtensions.ExecuteDataSet(cmdString);
    }

    #endregion --------------- RETURN VALUE IN DATA SET ---------------

    public DataTable GetCompanyList()
    {
        var query = string.Empty;
        var user = ObjGlobal.LogInUser.ToUpper();
        if (user is "AMSADMIN" or "ADMIN")
        {
            query = @"
			SELECT G.GComp_Id, G.Company_Name [Description], G.Database_Name, G.PanNo, G.Address, G.DataSyncOriginId, G.DataSyncApiBaseUrl, G.LoginDate, G.ApiKey
			FROM master.AMS.GlobalCompany AS G
				 LEFT OUTER JOIN sys.databases AS SD ON SD.name=G.Database_Name
			WHERE G.Status=1
			ORDER BY G.LoginDate DESC;";
        }
        else
        {
            query = $@"
			SELECT GC.GComp_Id, GC.Company_Name [Description], GC.Database_Name, GC.PanNo, GC.Address, GC.DataSyncOriginId, GC.DataSyncApiBaseUrl, GC.LoginDate, GC.ApiKey
			FROM master.AMS.CompanyRights AS G
				 LEFT OUTER JOIN master.AMS.GlobalCompany AS GC ON G.Company_Id=GC.GComp_Id
			WHERE G.User_Id={ObjGlobal.LogInUserId}
			ORDER BY GC.LoginDate DESC;";
        }

        var result = SqlExtensions.ExecuteDataSetOnMaster(query).Tables[0];
        return result;
    }

    public DataTable GetCompanyRights(int userId)
    {
        var cmdString = $@"
        Select Distinct 1 CmpChk,GC.GComp_Id, U.User_Id,GC.Company_Name,'{ObjGlobal.CfStartAdDate}' as Posting_StartDate,'{ObjGlobal.CfEndAdDate}' as Posting_EndDate,'{ObjGlobal.CfStartAdDate}' as Modify_StartDate,'{ObjGlobal.CfEndAdDate}' as Modify_EndDate,";
        cmdString += " IsNull(CR.Back_Days_Restriction, 0) Back_Days_Restriction from AMS.GlobalCompany GC Inner Join AMS.CompanyRights CR On CR.Company_Id = GC.GComp_Id Inner Join Master.AMS.UserInfo U On Cr.User_Id = U.User_Id";
        cmdString += $" Where U.User_Id = {userId}";
        cmdString += " Union All";
        cmdString += $" Select Distinct 0 CmpChk, GC.GComp_Id,{userId} User_Id,GC.Company_Name,'{ObjGlobal.CfStartAdDate}' as Posting_StartDate,'{ObjGlobal.CfEndAdDate}' as Posting_EndDate,'{ObjGlobal.CfStartAdDate}' as Modify_StartDate,'{ObjGlobal.CfEndAdDate}' as Modify_EndDate,";
        cmdString += $" 0 Back_Days_Restriction from AMS.GlobalCompany GC  where GComp_Id not in (Select Company_Id from  AMS.CompanyRights where User_Id = {userId})";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    public DataTable CheckSmsConfig()
    {
        var cmdString = "Select * From [AMS].[SMS_CONFIG]; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetCompanyUnit(string actionTag, int selectedId = 0)
    {
        var cmdString = selectedId == 0
            ? "SELECT cu.*,b.Branch FROM AMS.CompanyUnit cu LEFT JOIN AMS.Branch b ON cu.Branch_ID = b.Branch_ID "
            : $"SELECT cu.*,b.Branch_Name FROM AMS.CompanyUnit cu LEFT JOIN AMS.Branch b ON cu.Branch_ID = b.Branch_ID  where CmpUnit_Id= {selectedId} ";

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterUser(int selectedId = 0)
    {
        var cmdString = selectedId == 0
            ? "SELECT * FROM MASTER.AMS.UserInfo ui WHERE ui.User_Id NOT IN (1,3,4) \n"
            : $@"
			SELECT ui.User_Id, ui.Full_Name, ui.User_Name, ui.Password, ui.Address, ui.Mobile_No, ui.Phone_No, ui.Email_Id, ui.Role_Id, ui.Branch_Id, ui.Allow_Posting, ui.Posting_StartDate, ui.Posting_EndDate, ui.Modify_StartDate, ui.Modify_EndDate, ui.Auditors_Lock, ui.Valid_Days, ui.Created_By, ui.Created_Date, ui.Status, ui.Ledger_Id,gl.GLName, ui.Category, ui.Authorized, ui.IsDefault, ui.IsModify, ui.IsDeleted, ui.IsRateChange, ui.IsPDCDashBoard, ui.IsQtyChange , ui.IDENTITYCOL, ui.$IDENTITY FROM MASTER.AMS.UserInfo ui
			LEFT OUTER JOIN AMS.GeneralLedger gl ON ui.Ledger_Id = gl.GLID
			WHERE ui.User_Id= '{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterUserInfo(string loginUser)
    {
        var cmdString = $@"
		SELECT TOP 1 ui.User_Id, ui.Full_Name, ui.User_Name, ui.Password, ui.Address, ui.Mobile_No, ui.Phone_No, ui.Email_Id, ui.Role_Id, ui.Branch_Id, ui.Allow_Posting, ui.Posting_StartDate, ui.Posting_EndDate, ui.Modify_StartDate, ui.Modify_EndDate, ui.Auditors_Lock, ui.Valid_Days, ui.Created_By, ui.Created_Date, ui.Status, ui.Ledger_Id,'' GLName, ui.Category, ui.Authorized, ui.IsDefault, ui.IsModify, ui.IsDeleted, ui.IsRateChange, ui.IsPDCDashBoard, ui.IDENTITYCOL, ui.$IDENTITY,ui.IsQtyChange
		FROM MASTER.AMS.UserInfo ui
		WHERE ui.User_Name='{loginUser}'";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    public DataTable GetUserAccessControl(int userRoleId, int userId, bool isCallFromSecurityRight)
    {
        var cmdString = $@"
		SELECT * FROM AMS.UserAccessControl
		WHERE UserRoleId={userRoleId} and UserId={userId}";
        var data = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (data.Rows.Count == 0 && !isCallFromSecurityRight)
        {
            cmdString = $@"SELECT * FROM AMS.UserAccessControl WHERE UserRoleId={userRoleId}";
            data = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        }

        return data;
    }
    public DataTable GetUserAccessControls(int userRoleId, int userId, bool isCallFromSecurityRight)
    {
        var cmdString = $@"
		SELECT * FROM AMS.UserAccessControl
		WHERE UserRoleId={userRoleId} and UserId={userId}";
        var data = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        if (data.Rows.Count == 0 && !isCallFromSecurityRight)
        {
            cmdString = $@"SELECT * FROM AMS.UserAccessControl WHERE UserRoleId={userRoleId}";
            data = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        }

        return data;
    }

    //RETURN STATIC BOOLEAN  VALUE

    #region --------------- RETURN STATIC VALUE IN BOOLEN ---------------

    public static bool CheckValidPan(string actionTag, string panNo, long ledgerId)
    {
        var cmdString = $" SELECT GlId FROM AMS.GeneralLedger AS gl WHERE gl.PanNo='{panNo}' ";
        if (actionTag == "UPDATE")
        {
            cmdString += $" and GlId <> '{ledgerId}'";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0]
            .Rows.Count > 0;
    }

    #endregion --------------- RETURN STATIC VALUE IN BOOLEN ---------------


    // RETURN STATIC LONG VALUE

    #region --------------- RETURN STATIC VALUE IN LONG ---------------

    public static long ReturnMaxLongValue(string table, string column)
    {
        var cmdString = $"SELECT CAST(ISNULL(MAX({column}),0) AS BIGINT) + 1 MaxId FROM {table} ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return ObjGlobal.ReturnLong(dt.Rows[0]["MaxId"].ToString());
    }

    #endregion --------------- RETURN STATIC VALUE IN LONG ---------------


    // INSERT, UPDATE, DELETE FUNCTION OF MASTER

    #region --------------- INSERT, UPDATE ,DELETE FUNCTION ---------------
    public async Task<ListResult<SyncLogModel>> GetUnSynchronizedData(string tableName)

    {
        try
        {
            var resultList = new ListResult<SyncLogModel>();
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return new ListResult<SyncLogModel>();
            }

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
            var getUrl = string.Empty;
            if (tableName == "AMS.AccountGroup")
            {
                getUrl = @$"{configParams.Model.Item2}AccountGroup/GetUnSynchronizedAccountGroups";
            }
            else if (tableName == "AMS.AccountSubGroup")
            {
                getUrl = @$"{configParams.Model.Item2}AccountSubGroup/GetUnSynchronizedAccountSubGroups";
            }
            else if (tableName == "AMS.GeneralLedger")
            {
                getUrl = @$"{configParams.Model.Item2}GeneralLedger/GetUnSynchronizedGeneralLedgers";
            }
            else if (tableName == "AMS.SubLedger")
            {
                getUrl = @$"{configParams.Model.Item2}SubLedger/GetUnSynchronizedSubLedgers";
            }
            else if (tableName == "AMS.Branch")
            {
                getUrl = @$"{configParams.Model.Item2}Branch/GetUnSynchronizedBranchs";
            }
            else if (tableName == "AMS.Department")
            {
                getUrl = @$"{configParams.Model.Item2}Department/GetUnSynchronizedDepartments";
            }
            else if (tableName == "AMS.Currency")
            {
                getUrl = @$"{configParams.Model.Item2}Currency/GetUnSynchronizedCurrencies";
            }
            else if (tableName == "AMS.SeniorAgent")
            {
                getUrl = @$"{configParams.Model.Item2}SeniorAgent/GetUnSynchronizedSeniorAgents";
            }
            else if (tableName == "AMS.JuniorAgent")
            {
                getUrl = @$"{configParams.Model.Item2}JuniorAgent/GetUnSynchronizedJuniorAgents";
            }
            else if (tableName == "AMS.MainArea")
            {
                getUrl = @$"{configParams.Model.Item2}MainArea/GetUnSynchronizedMainAreas";
            }
            else if (tableName == "AMS.Area")
            {
                getUrl = @$"{configParams.Model.Item2}Area/GetUnSynchronizedAreas";
            }
            else if (tableName == "AMS.MemberType")
            {
                getUrl = @$"{configParams.Model.Item2}MemberType/GetUnSynchronizedMemberTypes";
            }
            else if (tableName == "AMS.MemberShipSetup")
            {
                getUrl = @$"{configParams.Model.Item2}MemberShipSetup/GetUnSynchronizedMemberShipSetups";
            }
            else if (tableName == "AMS.GiftVoucherList")
            {
                getUrl = @$"{configParams.Model.Item2}GiftVoucherList/GetUnSynchronizedGiftVoucherLists";
            }
            else if (tableName == "AMS.Counter")
            {
                getUrl = @$"{configParams.Model.Item2}Counter/GetUnSynchronizedCounters";
            }
            else if (tableName == "AMS.Floor")
            {
                getUrl = @$"{configParams.Model.Item2}Floor/GetUnSynchronizedFloors";
            }
            else if (tableName == "AMS.TableMaster")
            {
                getUrl = @$"{configParams.Model.Item2}TableMaster/GetUnSynchronizedTableMasters";
            }
            else if (tableName == "AMS.Godown")
            {
                getUrl = @$"{configParams.Model.Item2}Godown/GetUnSynchronizedGodowns";
            }
            else if (tableName == "AMS.Rack")
            {
                getUrl = @$"{configParams.Model.Item2}Rack/GetUnSynchronizedRacks";
            }
            else if (tableName == "AMS.NrMaster")
            {
                getUrl = @$"{configParams.Model.Item2}NrMaster/GetUnSynchronizedNrMasters";
            }
            else if (tableName == "AMS.ProductGroup")
            {
                getUrl = @$"{configParams.Model.Item2}ProductGroup/GetUnSynchronizedProductGroups";
            }
            else if (tableName == "AMS.ProductSubGroup")
            {
                getUrl = @$"{configParams.Model.Item2}ProductSubGroup/GetUnSynchronizedProductSubGroups";
            }
            else if (tableName == "AMS.ProductUnit")
            {
                getUrl = @$"{configParams.Model.Item2}ProductUnit/GetUnSynchronizedProductUnits";
            }
            else if (tableName == "AMS.Product")
            {
                getUrl = @$"{configParams.Model.Item2}Product/GetUnSynchronizedProducts";
            }
            else if (tableName == "AMS.ProductScheme")
            {
                getUrl = @$"{configParams.Model.Item2}ProductScheme/GetUnSynchronizedProductSchemes";
            }

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = getUrl
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);
            var syncLogRepo = DataSyncProviderFactory.GetRepository<SyncLogModel>(DataSyncManager.GetGlobalInjectData());
            resultList = await syncLogRepo?.GetUnSynchronizedDataAsync()!;
            return resultList;
        }
        catch
        {
            return new ListResult<SyncLogModel>();
        }
    }

    public async Task<string> SaveSyncLogDetails(string actionTag)
    {
        //sync
        var sld = new SyncLogDetailModel();
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return "Setting Missing";
            }

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
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}SyncLogDetail/GetSyncLogDetailById",
                InsertUrl = @$"{configParams.Model.Item2}SyncLogDetail/InsertSyncLogDetail",
                UpdateUrl = @$"{configParams.Model.Item2}SyncLogDetail/UpdateSyncLogDetail"
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var syncLogDetailRepo = DataSyncProviderFactory.GetRepository<SyncLogDetailModel>(DataSyncManager.GetGlobalInjectData());

            sld.BranchId = ObjSyncLogDetail.BranchId;
            sld.SyncLogId = ObjSyncLogDetail.SyncLogId;
            var result = actionTag switch
            {
                "SAVE" => await syncLogDetailRepo?.PushNewAsync(sld),
                "UPDATE" => await syncLogDetailRepo?.PutNewAsync(sld),
                _ => await syncLogDetailRepo?.PutNewAsync(sld)
            };
            return "";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public int SaveBranchRight(string actionTag)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($" DELETE FROM AMS.BranchRights WHERE UserId='{ObjGlobal.LogInUserId}'; \n");
        cmdString.Append("  INSERT INTO AMS.BranchRights (UserId, BranchId, Branch) \n");
        cmdString.Append("  VALUES  \n ");
        var j = 1;
        foreach (var t in GetRights)
        {
            cmdString.Append($"('{t.UserId}','{t.BranchId}','{t.Branch}' ");
            cmdString.Append(j < GetRights.Count ? " ), \n " : " ); \n");
            j++;
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());

        return exe;
    }

    public int SaveClassSetup(string actionTag)
    {

        return 0;
    }

    public int SaveCounter(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(@"
            INSERT INTO AMS.Counter(CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) ");
            cmdString.Append(" \n VALUES \n");
            cmdString.Append($" ({ObjCounter.CId},N'{ObjCounter.CName}',N'{ObjCounter.CCode}', ");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"{ObjGlobal.SysCompanyUnitId}," : "NULL,");
            cmdString.Append($" CAST('{ObjCounter.Status}' AS BIT),'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjCounter.Printer.IsValueExits() ? $"N'{ObjCounter.Printer}'," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE()," : "NULL,");
            cmdString.Append($"GETDATE(),{ObjCounter.SyncRowVersion} );");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.Counter SET ");
            cmdString.Append($"CName = N'{ObjCounter.CName}',");
            cmdString.Append($"CCode = N'{ObjCounter.CCode}',");
            cmdString.Append(ObjCounter.Printer.IsValueExits()
                ? $"Printer = N'{ObjCounter.Printer}',"
                : "Printer = NULL,");
            cmdString.Append($"Status = CAST ('{ObjCounter.Status}' AS BIT),");
            cmdString.Append("SyncLastPatchedOn = GETDATE()");
            cmdString.Append($" WHERE CId = {ObjCounter.CId};");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.Counter where CId = {ObjCounter.CId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());

        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncCounterAsync(actionTag));
        }

        return exe;
    }

    public async Task<int> SyncCounterAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

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
            var deleteUri = @$"{configParams.Model.Item2}Counter/DeleteCounterAsync?id=" + ObjCounter.CId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}Counter/GetMemberCounterById",
                InsertUrl = @$"{configParams.Model.Item2}Counter/InsertCounter",
                UpdateUrl = @$"{configParams.Model.Item2}Counter/UpdateCounter",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);
            var counterRepo =
                DataSyncProviderFactory.GetRepository<Counter>(DataSyncManager.GetGlobalInjectData());

            var cm = new Counter
            {
                CId = ObjCounter.CId,
                CName = ObjCounter.CName,
                CCode = ObjCounter.CCode,
                Branch_ID = ObjGlobal.SysBranchId > 0 ? ObjGlobal.SysBranchId : 0,
                Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                Status = ObjCounter.Status,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                Printer = ObjCounter.Printer.IsValueExits() ? ObjCounter.Printer : null,
                SyncRowVersion = ObjCounter.SyncRowVersion
            };
            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await counterRepo?.PushNewAsync(cm),
                "UPDATE" => await counterRepo?.PutNewAsync(cm),
                "DELETE" => await counterRepo?.DeleteNewAsync(),
                _ => await counterRepo?.PushNewAsync(cm)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int SaveEmployee(string actionTag)
    {
        throw new NotImplementedException();
    }

    public int SaveFloor(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.Floor (FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_Id, Company_Id, Status, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId) ");
            cmdString.Append($"Values({Floor.FloorId}, ");
            cmdString.Append(" \n VALUES \n");
            cmdString.Append(!string.IsNullOrEmpty(Floor.Description)
                ? $"N'{Floor.Description}',"
                : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(Floor.ShortName) ? $"N'{Floor.ShortName}'," : "NULL,");
            cmdString.Append(!string.IsNullOrEmpty(Floor.Type) ? $"N'{Floor.Type}'," : "NULL,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" N'{ObjGlobal.SysBranchId}'," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $"N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(Floor.Status is true ? "1," : "0,");
            cmdString.Append("NEWID(),");
            cmdString.Append(
                ObjGlobal.LocalOriginId.HasValue ? $" '{ObjGlobal.LocalOriginId}'," : "NULL,");
            cmdString.Append($"GetDate(),GetDate(),{Floor.SyncRowVersion} ,NEWID() ); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.Floor SET  ");
            cmdString.Append(!string.IsNullOrEmpty(Floor.Description)
                ? $"Description = N'{Floor.Description}',"
                : "Description = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(Floor.ShortName)
                ? $"ShortName = N'{Floor.ShortName}',"
                : "ShortName = NULL,");
            cmdString.Append(!string.IsNullOrEmpty(Floor.Type)
                ? $"Type = N'{Floor.Type}',"
                : "Type = NULL,");
            cmdString.Append(Floor.Status is true ? "Status = 1," : "Status = 0,");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {Floor.SyncRowVersion}");
            cmdString.Append($" WHERE FloorId = {Floor.FloorId}; ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.Floor where FloorId = {Floor.FloorId}; ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        if (exe <= 0)
        {
            return exe;
        }

        if (ObjGlobal.IsOnlineSync)
        {
            Task.Run(() => SyncFloorAsync(actionTag));
        }

        return exe;
    }

    public async Task<int> SyncFloorAsync(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

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
            var deleteUri = @$"{configParams.Model.Item2}Floor/DeleteFloorAsync?id=" + Floor.FloorId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}Floor/GetFloorById",
                InsertUrl = @$"{configParams.Model.Item2}Floor/InsertFloor",
                UpdateUrl = @$"{configParams.Model.Item2}Floor/UpdateFloor",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);
            var floorRepo = DataSyncProviderFactory.GetRepository<FloorSetup>(DataSyncManager.GetGlobalInjectData());

            var fm = new FloorSetup
            {
                FloorId = Floor.FloorId,
                Description = string.IsNullOrEmpty(Floor.Description) ? null : Floor.Description,
                ShortName = string.IsNullOrEmpty(Floor.ShortName) ? null : Floor.ShortName,
                Type = string.IsNullOrEmpty(Floor.Type) ? null : Floor.Type,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                Branch_ID = ObjGlobal.SysBranchId,
                Company_Id = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                Status = Floor.Status == null ? true : Floor.Status,
                SyncRowVersion = Floor.SyncRowVersion == null ? (short)1 : Floor.SyncRowVersion
            };

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await floorRepo?.PushNewAsync(fm),
                "UPDATE" => await floorRepo?.PutNewAsync(fm),
                "DELETE" => await floorRepo?.DeleteNewAsync(),
                _ => await floorRepo?.PushNewAsync(fm)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int SaveBackupAndRestoreDatabaseLog(string actionTag)
    {
        var cmdString = new StringBuilder();
        cmdString.Append(
            "INSERT INTO AMS.BR_LOG (DB_NAME, COMPANY, LOCATION, USED_BY, USED_ON, ACTION, SyncRowVersion, Log_ID) \n");
        cmdString.Append("VALUES(");
        cmdString.Append(BrMaster.DB_NAME.IsValueExits() ? $"N'{BrMaster.DB_NAME}'," : "NULL,");
        cmdString.Append(BrMaster.COMPANY.IsValueExits() ? $"N'{BrMaster.COMPANY}'," : "NULL,");
        cmdString.Append(BrMaster.LOCATION.IsValueExits() ? $"N'{BrMaster.LOCATION}'," : "NULL,");
        cmdString.Append(BrMaster.USED_BY.IsValueExits() ? $"N'{BrMaster.USED_BY}'," : "NULL,");
        cmdString.Append("GETDATE(),");
        cmdString.Append(BrMaster.ACTION.IsValueExits() ? $"N'{BrMaster.ACTION}'," : "NULL,");
        cmdString.Append($"{BrMaster.SyncRowVersion.GetInt()},");
        cmdString.Append($"{BrMaster.Log_ID})");
        var cmdResult = SqlExtensions.ExecuteNonQueryOnMaster(cmdString.ToString());
        return cmdResult;
    }

    public async Task<int> SaveLoginLog(string actionTag)
    {
        var cmdString = new StringBuilder();
        cmdString.Append("INSERT INTO MASTER.AMS.LOGIN_LOG (OBJECT_ID, LOGIN_USER, COMPANY, LOGIN_DATABASE, DEVICE, DAVICE_MAC, DEVICE_IP, SYSTEM_ID, LOGIN_DATE, LOG_STATUS)\n");
        cmdString.Append($"Values({LoginLog.OBJECT_ID},");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.LOGIN_USER) ? $"N'{LoginLog.LOGIN_USER}'," : "NULL,");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.COMPANY) ? $"N'{LoginLog.COMPANY}'," : "NULL,");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.LOGIN_DATABASE) ? $"N'{LoginLog.LOGIN_DATABASE}'," : "NULL,");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.DEVICE) ? $"N'{LoginLog.DEVICE}'," : "NULL,");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.DAVICE_MAC) ? $"N'{LoginLog.DAVICE_MAC}'," : "NULL,");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.DEVICE_IP) ? $"N'{LoginLog.DEVICE_IP}'," : "NULL,");
        cmdString.Append(!string.IsNullOrEmpty(LoginLog.SYSTEM_ID) ? $"N'{LoginLog.SYSTEM_ID}'," : "NULL,");
        cmdString.Append("GETDATE(),1)");
        var cmdResult = await SqlExtensions.ExecuteNonQueryMasterAsync(cmdString.ToString());
        return cmdResult;
    }

    public async Task<int> SyncLoginLog(string actionTag)
    {
        //sync
        try
        {
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

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
            var deleteUri = @$"{configParams.Model.Item2}LoginLog/DeleteLoginLogAsync?id=" + LoginLog.OBJECT_ID;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}LoginLog/GetLoginLogById",
                InsertUrl = @$"{configParams.Model.Item2}LoginLog/InsertLoginLog",
                UpdateUrl = @$"{configParams.Model.Item2}LoginLog/UpdateLoginLog",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var loginLogRepo = DataSyncProviderFactory.GetRepository<LOGIN_LOG>(DataSyncManager.GetGlobalInjectData());
            var ll = new LOGIN_LOG();
            ll.OBJECT_ID = LoginLog.OBJECT_ID;
            ll.LOGIN_USER = string.IsNullOrEmpty(LoginLog.LOGIN_USER) ? null : LoginLog.LOGIN_USER;
            ll.COMPANY = string.IsNullOrEmpty(LoginLog.COMPANY) ? null : LoginLog.COMPANY;
            ll.LOGIN_DATABASE = string.IsNullOrEmpty(LoginLog.LOGIN_DATABASE) ? null : LoginLog.LOGIN_DATABASE;
            ll.DEVICE = string.IsNullOrEmpty(LoginLog.DEVICE) ? null : LoginLog.DEVICE;
            ll.DAVICE_MAC = string.IsNullOrEmpty(LoginLog.DAVICE_MAC) ? null : LoginLog.DAVICE_MAC;
            ll.DEVICE_IP = string.IsNullOrEmpty(LoginLog.DEVICE_IP) ? null : LoginLog.DEVICE_IP;
            ll.SYSTEM_ID = string.IsNullOrEmpty(LoginLog.SYSTEM_ID) ? null : LoginLog.SYSTEM_ID;
            ll.LOGIN_DATE = DateTime.Now;
            ll.LOG_STATUS = 1;
            var result = await loginLogRepo?.PushNewAsync(ll);
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int ResetLoginInfo(string actionTag)
    {
        var cmdString =
            $"UPDATE AMS.LOGIN_LOG SET LOG_STATUS = 0 WHERE LOGIN_USER='{ObjGlobal.LogInUser}' AND (LOG_STATUS = 1 OR LOG_STATUS IS NULL);";
        var cmdResult = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        return cmdResult;
    }

    public int SaveGuest(string actionTag)
    {
        //var cmdString = new StringBuilder();
        //if (actionTag.ToUpper() == "SAVE")
        //{
        //    cmdString.Append(
        //        " Insert into AMS.ClassSetup(CId,CName,CbSection,Branch_Id,Company_Id,Status,EnterBy,EnterDate) \n");
        //    cmdString.Append(" Values( (Select  IsNull(MAX(CId),0)+1 Cid from AMS.ClassSetup) \n,");
        //    cmdString.Append($"  '{ObjMaster.TxtDescription.GetTrimReplace()}',");
        //    cmdString.Append($"  '{ObjMaster.TxtType.GetTrimReplace()}',");
        //    cmdString.Append($" {ObjMaster.TxtBranchId},");
        //    cmdString.Append(ObjMaster.TxtCompanyUnitId != 0 ? $" {ObjMaster.TxtCompanyUnitId}, " : "Null,");
        //    cmdString.Append(ObjMaster.ChkActive ? "1," : "0,");
        //    cmdString.Append($" '{ObjMaster.TxtEnterBy}',");
        //    cmdString.Append("  GetDate())");
        //}
        //else if (actionTag.ToUpper() == "UPDATE")
        //{
        //    cmdString.Append(" Update AMS.ClassSetup set ");
        //    cmdString.Append($"CName ='{ObjMaster.TxtDescription.GetTrimReplace()}',");
        //    cmdString.Append($"CbSection='{ObjMaster.TxtType.GetTrimReplace()}',");
        //}
        //else if (actionTag.ToUpper() == "DELETE")
        //{
        //    cmdString.Append($"Delete from AMS.ClassSetup where CId = {ObjMaster.MasterId} ");
        //}

        return 0;
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

    public int SaveSmsConfig(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag is "SAVE" or "NEW")
        {
            cmdString.Append(
                " INSERT INTO AMS.SMS_CONFIG (TOKEN, IsCashBank, IsJournalVoucher, IsSalesReturn, IsSalesInvoice, IsPurchaseInvoice, IsPurchaseReturn, AlternetNumber) \n");
            cmdString.Append(" VALUES ( ");
            cmdString.Append(!string.IsNullOrEmpty(ObjSms.TOKEN) ? $" '{ObjSms.TOKEN}'," : "NUll,");
            cmdString.Append(ObjSms.IsCashBank ? "1," : "0,");
            cmdString.Append(ObjSms.IsJournalVoucher ? "1," : "0,");
            cmdString.Append(ObjSms.IsSalesReturn ? "1," : "0,");
            cmdString.Append(ObjSms.IsSalesInvoice ? "1," : "0,");
            cmdString.Append(ObjSms.IsPurchaseInvoice ? "1," : "0,");
            cmdString.Append(ObjSms.IsPurchaseReturn ? "1," : "0,");
            cmdString.Append(!string.IsNullOrEmpty(ObjSms.AlternetNumber) ? $" '{ObjSms.AlternetNumber}'" : "NUll");
            cmdString.Append(" )");
        }
        else if (actionTag == "UPDATE")
        {
            cmdString.Append(" Update AMS.SMS_CONFIG SET ");
            cmdString.Append(!string.IsNullOrEmpty(ObjSms.TOKEN) ? $" TOKEN= '{ObjSms.TOKEN}'," : "TOKEN= NUll,");
            cmdString.Append(ObjSms.IsCashBank ? "IsCashBank = 1," : "IsCashBank = 0,");
            cmdString.Append(ObjSms.IsJournalVoucher ? "IsJournalVoucher= 1," : "IsJournalVoucher= 0,");
            cmdString.Append(ObjSms.IsSalesReturn ? "IsSalesReturn = 1," : "IsSalesReturn= 0,");
            cmdString.Append(ObjSms.IsSalesInvoice ? "IsSalesInvoice = 1," : "IsSalesInvoice =0,");
            cmdString.Append(ObjSms.IsPurchaseInvoice ? "IsPurchaseInvoice= 1," : "IsPurchaseInvoice= 0,");
            cmdString.Append(ObjSms.IsPurchaseReturn ? "IsPurchaseReturn= 1," : "IsPurchaseReturn= 0,");
            cmdString.Append(!string.IsNullOrEmpty(ObjSms.AlternetNumber)
                ? $" AlternetNumber= '{ObjSms.AlternetNumber}'"
                : "AlternetNumber = NUll");
            cmdString.Append($"   WHERE SMSCONFIG_ID = '{ObjSms.SMSCONFIG_ID}' ");
        }

        var exe = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        return exe;
    }

    public int SaveGiftVoucherList(string actionTag)
    {
        var cmdString = new StringBuilder();
        if (actionTag.ToUpper() == "SAVE")
        {
            cmdString.Append(
                "INSERT INTO AMS.GiftVoucherList (CardNo, ExpireDate, Description, IssueAmount, IsUsed, BalanceAmount, BillAmount,VoucherType, BranchId, CompanyUnitId, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion) \n");
            cmdString.Append(
                $"VALUES ({GiftVoucherList.CardNo},'{GiftVoucherList.ExpireDate.GetSystemDate()}',N'{GiftVoucherList.Description}',");
            cmdString.Append($" {GiftVoucherList.IssueAmount},0,0,0,N'{GiftVoucherList.VoucherType}',");
            cmdString.Append(ObjGlobal.SysBranchId > 0 ? $" {ObjGlobal.SysBranchId}," : "NULL,");
            cmdString.Append(ObjGlobal.SysCompanyUnitId > 0 ? $" N'{ObjGlobal.SysCompanyUnitId}'," : "NULL,");
            cmdString.Append(GiftVoucherList.Status ? "1," : "0,");
            cmdString.Append($"'{ObjGlobal.LogInUser}', GETDATE(),");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID(), " : "NULL, ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "NEWID()," : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync && ObjGlobal.LocalOriginId.HasValue
                ? $" '{ObjGlobal.LocalOriginId}',"
                : "NULL,");
            cmdString.Append(ObjGlobal.IsOnlineSync ? $"'{DateTime.Now:yyyy-MM-dd}', " : "NULL, ");
            cmdString.Append(ObjGlobal.IsOnlineSync ? "GETDATE(), " : "NULL, ");
            cmdString.Append($"{GiftVoucherList.SyncRowVersion.GetDecimal(true)} ); ");
        }
        else if (actionTag.ToUpper() == "UPDATE")
        {
            cmdString.Append("UPDATE AMS.GiftVoucherList SET  ");
            cmdString.Append(GiftVoucherList.ExpireDate.IsValueExits()
                ? $"ExpireDate = '{GiftVoucherList.ExpireDate}',"
                : "TableName = NULL,");
            cmdString.Append(GiftVoucherList.Description.IsValueExits()
                ? $"TableCode = N'{GiftVoucherList.Description}',"
                : "TableCode = NULL,");
            cmdString.Append(GiftVoucherList.IssueAmount > 0 ? $"FloorId = {GiftVoucherList.IssueAmount} ," : "0,");
            cmdString.Append(GiftVoucherList.Status ? "Status = 1," : "Status = 0,");
            cmdString.Append(GiftVoucherList.VoucherType.IsValueExits()
                ? $"VoucherType = N'{GiftVoucherList.VoucherType}',"
                : "VoucherType = 'O',");
            cmdString.Append("SyncLastPatchedOn = GETDATE(),");
            cmdString.Append($"SyncRowVersion = {GiftVoucherList.SyncRowVersion.GetDecimal(true)}");
            cmdString.Append($" WHERE VoucherId = {GiftVoucherList.VoucherId}; ");
        }
        else if (actionTag.ToUpper() == "DELETE")
        {
            cmdString.Append($"Delete from AMS.GiftVoucherList where VoucherId = {GiftVoucherList.VoucherId}; ");
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
            var configParams =
                DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
            if (!configParams.Success || configParams.Model.Item1 == null)
            //MessageBox.Show(@"Error fetching local-origin information. " + configParams.ErrorMessage,
            //    configParams.ResultType.SplitCamelCase(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //configParams.ShowErrorDialog();
            {
                return 1;
            }

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
            var deleteUri = @$"{configParams.Model.Item2}GiftVoucherList/DeleteGiftVoucherListAsync?id=" +
                            GiftVoucherList.VoucherId;

            var apiConfig = new SyncApiConfig
            {
                BaseUrl = configParams.Model.Item2,
                Apikey = configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{configParams.Model.Item2}GiftVoucherList/GetGiftVoucherListById",
                InsertUrl = @$"{configParams.Model.Item2}GiftVoucherList/InsertGiftVoucherList",
                UpdateUrl = @$"{configParams.Model.Item2}GiftVoucherList/UpdateGiftVoucherList",
                DeleteUrl = deleteUri
            };

            DataSyncHelper.SetConfig(apiConfig);
            injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(injectData);

            var giftVoucherRepo =
                DataSyncProviderFactory.GetRepository<GiftVoucherList>(DataSyncManager.GetGlobalInjectData());

            var gf = new GiftVoucherList
            {
                CardNo = GiftVoucherList.CardNo,
                ExpireDate = Convert.ToDateTime(GiftVoucherList.ExpireDate.GetSystemDate()),
                Description = GiftVoucherList.Description,
                IssueAmount = GiftVoucherList.IssueAmount,
                IsUsed = false,
                BalanceAmount = 0,
                BillAmount = 0,
                VoucherType = GiftVoucherList.VoucherType,
                BranchId = ObjGlobal.SysBranchId,
                CompanyUnitId = ObjGlobal.SysCompanyUnitId > 0 ? ObjGlobal.SysCompanyUnitId : null,
                Status = GiftVoucherList.Status,
                EnterBy = ObjGlobal.LogInUser,
                EnterDate = DateTime.Now,
                SyncRowVersion = 1
            };

            var result = actionTag.ToUpper() switch
            {
                "SAVE" => await giftVoucherRepo?.PushNewAsync(gf),
                "UPDATE" => await giftVoucherRepo?.PutNewAsync(gf),
                "DELETE" => await giftVoucherRepo?.DeleteNewAsync(),
                _ => await giftVoucherRepo?.PushNewAsync(gf)
            };
            return 1;
        }
        catch (Exception ex)
        {
            return 1;
        }
    }

    public int UpdateProductSalesRate(double value, string product)
    {
        var cmdString = string.Empty;
        if (ObjGlobal.SalesCarryRate)
        {
            cmdString =
                $@"UPDATE AMS.Product SET PSalesRate = {value.GetDecimal()} WHERE PName = N'{product}' AND PSalesRate IS NULL";
        }

        if (ObjGlobal.SalesLastRate || ObjGlobal.SalesUpdateRate)
        {
            cmdString = $@"UPDATE AMS.Product SET PSalesRate = {value.GetDecimal()} WHERE PName = N'{product}'";
        }

        if (cmdString.IsBlankOrEmpty())
        {
            return 0;
        }

        var iResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return iResult;
    }

    public int UpdateProductPurchaseRate(double value, string product)
    {
        var cmdString = string.Empty;
        if (ObjGlobal.PurchaseCarryRate)
        {
            cmdString =
                $@"UPDATE AMS.Product SET PBuyRate = {value.GetDecimal()} WHERE PName = N'{product}' AND PBuyRate IS NULL";
        }

        if (ObjGlobal.PurchaseLastRate || ObjGlobal.PurchaseUpdateRateEnable)
        {
            cmdString = $@"UPDATE AMS.Product SET PBuyRate = {value.GetDecimal()} WHERE PName = N'{product}'";
        }

        if (cmdString.IsBlankOrEmpty())
        {
            return 0;
        }

        var iResult = SqlExtensions.ExecuteNonQuery(cmdString);
        return iResult;
    }

    public int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt)
    {
        var cmdString = $"SELECT  {tableId} SelectedId From {tableName} where {tableColumn}='{filterTxt}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows.Count > 0 ? dt.Rows[0]["SelectedId"].GetInt() : 0;
    }

    public int ReturnMaxIdValue(string module)
    {
        var tableId = module.ToUpper() switch
        {
            "SCHEME" => "SchemeId",
            _ => string.Empty
        };
        var tableName = module.ToUpper() switch
        {
            "SCHEME" => "AMS.Scheme_Master",
            _ => string.Empty
        };
        var cmdString = $"SELECT ISNULL ( MAX ( {tableId} ), 0 ) + 1 ReturnMaxId FROM {tableName}; ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows[0]["ReturnMaxId"].GetInt();
    }

    #endregion --------------- INSERT, UPDATE ,DELETE FUNCTION ---------------



    //RETURN LONG VALUE

    #region --------------- RETURN VALUE IN LONG ---------------

    public long GenerateLedgerAccountNumber(string groupDesc)
    {
        var cmdString = $"SELECT ag.Schedule  FROM AMS.AccountGroup AS ag WHERE ag.GrpName='{groupDesc}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var scheduleNo = dt.Rows[0]["Schedule"].GetInt();
        cmdString = string.Empty;
        dt.Reset();
        cmdString =
            $"SELECT  COUNT(SUBSTRING(ACCode,1,3))+ 1 ACCode  FROM AMS.GeneralLedger  where ACCode LIKE '%{scheduleNo}%'";
        dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var len = scheduleNo.ToString().Length;
        var accountId = scheduleNo.ToString().Substring(0, len) + dt.Rows[0]["ACCode"].ToString().PadLeft(5, '0');
        return accountId.GetLong();
    }

    public long ReturnLongValueFromTable(string tableName, string getId, string filterValue, string filterTxt)
    {
        var cmdString = $"SELECT  {getId} SelectedId From {tableName} where {filterValue}='{filterTxt}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString)
            .Tables[0];
        long.TryParse(dt.Rows[0]["SelectedId"].ToString(), out var selectedId);
        return selectedId;
    }

    public long ReturnMaxMemberId()
    {
        var date = DateTime.Now.ToString("yy");
        const string cmdString = "  SELECT ISNULL(MAX(mss.MemberId),0)+ 1 MemberId  FROM AMS.MemberShipSetup mss;";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        long maxMemberId = 0;
        if (dt.Rows.Count is 0)
        {
            var result = dt.Rows[0]["MemberId"].GetLong();
            var maxId = result.ToString().Length > 2
                ? result.ToString().Substring(2, result.ToString().Length - 2)
                : result.ToString();
            maxMemberId = (date + maxId).GetLong();
        }
        else
        {
            maxMemberId = dt.Rows[0]["MemberId"].GetLong();
        }

        return maxMemberId;
    }

    public long GetLedgerIdFromDescription(string ledgerDesc)
    {
        var cmdString = $"Select GLID  from AMS.GeneralLedger where GLName = '{ledgerDesc}'";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? 0 : dtLedger.Rows[0]["GLID"].GetLong();
    }

    public long GetProductIdFromShortName(string shortName)
    {
        var cmdString = $"SELECT bl.ProductId  FROM AMS.BarcodeList bl WHERE bl.Barcode = '{shortName}'";
        return cmdString.GetQueryData().GetLong();
    }

    public long GetUserLedgerIdFromUser(string userInfo)
    {
        var cmdString = $@"
        SELECT GLID FROM AMS.GeneralLedger WHERE GLID IN (SELECT Ledger_Id FROM master.AMS.UserInfo WHERE User_Name='{userInfo}')";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? 0 : dtLedger.Rows[0]["GLID"].GetLong();
    }

    #endregion --------------- RETURN VALUE IN LONG ---------------


    // RETURN BOOLEAN VALUE

    #region --------------- RETURN VALUE IN BOOLEN ---------------

    public bool IsBillingTermExitsOrNot(string module, string type)
    {
        var cmdString = module switch
        {
            "SB" or "SC" or "SQ" or "SR" or "SO" when type.Equals("P") =>
                "SELECT * FROM AMS.ST_Term WHERE ST_Condition='P'",
            "SB" or "SC" or "SQ" or "SR" or "SO" when type.Equals("B") =>
                "SELECT * FROM AMS.ST_Term WHERE ST_Condition='B'",
            "PB" or "PC" or "PIN" or "PR" or "PO" or "PCR" when type.Equals("B") =>
                "SELECT * FROM AMS.PT_Term WHERE PT_Condition='B'",
            "PB" or "PC" or "PIN" or "PR" or "PO" or "PCR" when type.Equals("P") =>
                "SELECT * FROM AMS.PT_Term WHERE PT_Condition='P'",
            _ => string.Empty
        };
        var dtCheck = cmdString.IsValueExits()
            ? SqlExtensions.ExecuteDataSet(cmdString).Tables[0]
            : new DataTable();
        return dtCheck.Rows.Count > 0;
    }

    public bool GetTaxRate()
    {
        decimal termRate = 0;
        const string cmdString =
            @"SELECT TOP (1) ST_Rate FROM AMS.ST_Term WHERE ST_Id = (SELECT  SBVatTerm FROM AMS.SalesSetting)";
        var dtTerm = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        if (dtTerm.Rows.Count > 0)
        {
            termRate = dtTerm.Rows[0]["ST_Rate"].GetDecimal();
        }

        return termRate > 0;
    }

    public bool IsAdditionalBillingTermExitsOrNot(string module, string type)
    {
        var cmdString = module switch
        {
            "SB" or "SC" or "SQ" or "SR" or "SO" when type.Equals("P") =>
                "SELECT * FROM AMS.ST_Term WHERE ST_Condition='P' AND ST_Type = 'A'",
            "SB" or "SC" or "SQ" or "SR" or "SO" when type.Equals("B") =>
                "SELECT * FROM AMS.ST_Term WHERE ST_Condition='B' AND ST_Type = 'A'",
            "PB" or "PC" or "PIN" or "PR" or "PO" when type.Equals("B") =>
                "SELECT * FROM AMS.PT_Term WHERE PT_Condition='B' AND PT_Type = 'A'",
            "PB" or "PC" or "PIN" or "PR" or "PO" when type.Equals("P") =>
                "SELECT * FROM AMS.PT_Term WHERE PT_Condition='P' AND PT_Type = 'A'",
            _ => string.Empty
        };
        var dtCheck = cmdString.IsValueExits()
            ? SqlExtensions.ExecuteDataSet(cmdString).Tables[0]
            : new DataTable();
        return dtCheck.Rows.Count > 0;
    }

    #endregion --------------- RETURN VALUE IN BOOLEN ---------------


    //RETURN STRING VALUE

    #region --------------- RETURN VALUE IN STRING ---------------

    public string GetLedgerDescription(long ledgerId)
    {
        var cmdString = $"Select GLName from AMS.GeneralLedger where GLid = '{ledgerId}'";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? string.Empty : dtLedger.Rows[0][0].ToString();
    }

    public string GetAccountGroupDescription(int groupId)
    {
        var cmdString = $"SELECT GrpName FROM AMS.AccountGroup WHERE GrpId = '{groupId}'";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? string.Empty : dtLedger.Rows[0][0].ToString();
    }

    public string GetProductGroupDescription(int groupId)
    {
        var cmdString = $"SELECT GrpName FROM AMS.ProductGroup WHERE PGrpId = '{groupId}'";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? string.Empty : dtLedger.Rows[0][0].ToString();
    }

    public string GetLedgerTypeDescription(long ledgerId)
    {
        var cmdString = $"Select GLType from AMS.GeneralLedger where GLid = '{ledgerId}'";
        var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtLedger.Rows.Count <= 0 ? string.Empty : dtLedger.Rows[0]["GLType"].GetUpper();
    }

    public string BindAutoIncrementCode(string tableName, string shortName, string autoIncrementCode)
    {
        var dt = new DataTable();
        autoIncrementCode = autoIncrementCode.PadRight(2, 'A').ToUpper();
        var cmdString =
            $"SELECT  COUNT(SUBSTRING({shortName},1,2))+ 1 ShortName  FROM {tableName}  where SUBSTRING({shortName},1,2)='{autoIncrementCode.Substring(0, 2)}'";
        dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        autoIncrementCode = autoIncrementCode.Substring(0, 2) + dt.Rows[0]["ShortName"].ToString().PadLeft(5, '0');
        return autoIncrementCode;
    }

    #endregion --------------- RETURN VALUE IN STRING ---------------


    #region --------------- RETURN VALUE IN DATA TABLE ---------------

    public DataTable IsExitsBarcode(string inputValue)
    {
        var cmdString = $@"
			SELECT p.PID
			FROM AMS.Product p
			WHERE p.PShortName='{inputValue}' OR p.Barcode='{inputValue}' OR p.Barcode1='{inputValue}' OR p.Barcode2='{inputValue}' OR p.Barcode3='{inputValue}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable LoginBranchDataTable(bool isUser, bool isActive = false)
    {
        string cmdString;
        if (isUser)
        {
            cmdString = @"
			SELECT  * FROM AMS.Branch b where IsActive = 1";
        }
        else
        {
            cmdString = $@"
			SELECT TOP (1) b.Branch_Id, b.Branch_Name, b.Branch_Code, b.Reg_Date, b.Address, b.Country, b.State, b.City, b.PhoneNo, b.Fax, b.Email, b.ContactPerson, b.ContactPersonAdd, b.ContactPersonPhone, b.Created_By, b.Created_Date, b.Modify_By, b.Modify_Date, b.SyncGlobalId, b.SyncOriginId, b.SyncCreatedOn, b.SyncLastPatchedOn, b.SyncRowVersion, b.SyncBaseId, br.RightsId, br.UserId, br.BranchId, br.Branch
			FROM AMS.Branch b
				 LEFT OUTER JOIN AMS.BranchRights br ON br.BranchId = b.Branch_Id
			WHERE br.UserId='{ObjGlobal.LogInUserId}' ";
            cmdString += isActive ? "AND IsActive = 1" : "";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable LoginCompanyDataTable()
    {
        AlterDatabaseTable.AlterCompanyInfoTableQuery();
        var cmdString = @" SELECT TOP (1) * FROM AMS.CompanyInfo";
        var dataset = SqlExtensions.ExecuteDataSet(cmdString);
        var result = new DataTable();
        result = dataset.Tables[0];
        return result;
    }

    public DataTable GetFloorList()
    {
        var cmdString = " select FloorId LedgerId,Description,ShortName,Type from [AMS].[Floor];";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    // GENERAL LEDGER
    public DataTable GetGeneralLedger(string tag, string category, string qtyVal, string amtVal, string loginDate, int selectedId = 0)
    {
        if (string.IsNullOrEmpty(loginDate))
        {
            loginDate = DateTime.Today.ToString(CultureInfo.InvariantCulture);
        }

        var cmdString = @"
			SELECT LedgerId, Particular, ShortName, LedgerCode, CAST(ISNULL(ABS(SUM(Debit-Credit)), 0) AS DECIMAL(18, 2)) Balance, CASE WHEN SUM(Debit-Credit)>0 THEN 'Dr' WHEN SUM(Debit-Credit)<0 THEN 'Cr' ELSE '' END BType, PanNo, GLType, GrpType, GroupDesc, SubGroupDesc, GLAddress, SalesMan, Currency, CrDays, CrLimit, CrTYpe, PhoneNo
			FROM (
				   SELECT GLID LedgerId, GLName Particular, GL.GLCode ShortName, ACCode LedgerCode, AD.LocalDebit_Amt Debit, AD.LocalCredit_Amt Credit, PanNo, GLType, AG.GrpType GrpType, AG.GrpName GroupDesc, ASG.SubGrpName SubGroupDesc, GLAddress, JA.AgentName SalesMan, CU.CCode Currency, GL.CrDays, GL.CrLimit, GL.CrTYpe, GL.PhoneNo
				   FROM AMS.GeneralLedger GL
						LEFT OUTER JOIN AMS.AccountDetails AS AD ON GL.GLID=AD.Ledger_ID
						LEFT OUTER JOIN AMS.AccountGroup AS AG ON GL.GrpId=AG.GrpId
						LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON GL.SubGrpId=ASG.SubGrpId
						LEFT OUTER JOIN AMS.JuniorAgent AS JA ON GL.AgentId=JA.AgentId
						LEFT OUTER JOIN AMS.Currency AS CU ON CU.CId=GL.CurrId
						LEFT OUTER JOIN AMS.PT_Term AS PT ON PT.Ledger=GL.GLID
				 ) AS Ledger
			WHERE 1=1;   ";
        switch (string.IsNullOrEmpty(category))
        {
            case false when category.ToUpper() == "ONLYCUSTOMER":
                {
                    cmdString += "AND GLType ='Customer'";
                    break;
                }
            case false when category.ToUpper() == "CUSTOMER":
                {
                    cmdString += "AND GLType in ('Customer', 'Both', 'Cash', 'Bank') ";
                    break;
                }
            case false when category.ToUpper() == "ONLYVENDOR":
                {
                    cmdString += "AND GLType ='Vendor'";
                    break;
                }
            case false when category.ToUpper() == "VENDOR":
                {
                    cmdString += "AND GLType in ('Vendor', 'Both', 'Cash', 'Bank') ";
                    break;
                }
            case false when category.ToUpper() == "CASH":
                {
                    cmdString += "AND GLType in ('Cash', 'Bank') ";
                    break;
                }
            case false when category.ToUpper() == "BANK":
                {
                    cmdString += "AND GLType in ('Bank','Cash') ";
                    break;
                }
            case false when category.ToUpper() == "ONLYBOTH":
                {
                    cmdString += "AND GLType ='Both'";
                    break;
                }
            case false when category.ToUpper() == "BOTH":
                {
                    cmdString += "AND GLType IN ('Customer','Vendor','Both') ";
                    break;
                }
            case false when category.ToUpper() == "OPENING":
                {
                    cmdString += "AND GLType IN ('OTHER') AND GrpType = 'Expenses' ";
                    break;
                }
            case false when category.ToUpper() == "CLOSING":
                {
                    cmdString += "AND GLType IN ('OTHER') AND GrpType = 'Income' ";
                    break;
                }
        }

        cmdString += @"
			Group By LedgerId,Particular,ShortName,LedgerCode,PanNo,GLType,GrpType,GroupDesc,SubGroupDesc,GLAddress,SalesMan,Currency,CrDays,CrLimit,CrTYpe,PhoneNo
			Order By Particular; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetLedgerBalance(long ledgerId, MaskedTextBox mskDate)
    {
        if (!mskDate.MaskCompleted)
        {
            mskDate.Text = DateTime.Now.GetDateString();
        }

        var cmdString = $@"
			SELECT gl.GLID, gl.GLName, gl.GLCode, gl.ACCode, gl.GLType, gl.GrpId, gl.SubGrpId, gl.PanNo, gl.AreaId, gl.AgentId, gl.CurrId, gl.CrDays, gl.CrLimit, gl.CrTYpe, gl.IntRate, gl.GLAddress, gl.PhoneNo, gl.LandLineNo, gl.OwnerName, gl.OwnerNumber, gl.Scheme, gl.Email, gl.Branch_id, gl.Company_Id, gl.EnterBy, gl.EnterDate, gl.Status, gl.PrimaryGroupId, gl.PrimarySubGroupId, gl.IsDefault, gl.NepaliDesc, gl.SyncBaseId, gl.SyncGlobalId, gl.SyncOriginId, gl.SyncCreatedOn, gl.SyncLastPatchedOn, gl.SyncRowVersion,ISNULL(bl.Balance,0) Balance
			 FROM AMS.GeneralLedger gl
				  LEFT OUTER JOIN (
									SELECT Ledger_ID, SUM ( LocalDebit_Amt - LocalCredit_Amt ) Balance
									 FROM AMS.AccountDetails
									 WHERE Ledger_ID = '{ledgerId}' AND Voucher_Date <= '{mskDate.Text.GetSystemDate()}'
									 GROUP BY Ledger_ID
								  ) AS bl ON bl.Ledger_ID = gl.GLID ";
        cmdString += $"  WHERE gl.GLID = {ledgerId} ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetOpeningLedgerList()
    {
        const string cmd = @"
			SELECT ROW_NUMBER() OVER (ORDER BY gl.GLName) Sno, GLID LedgerId, GLName Ledger, 0 Debit, 0 Credit
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup AG ON AG.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId=gl.AgentId
			WHERE AG.PrimaryGrp IN ('Balance Sheet', 'BS')AND ISNULL(gl.Status, 0)=1
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmd).Tables[0];
    }

    public DataTable GetGiftVoucherNumberInformation(int selectedId)
    {
        var cmdString =
            $@"SELECT MAX(CardNo)  CardNo FROM AMS.GiftVoucherList WHERE BranchId = {ObjGlobal.SysBranchId} AND (CompanyUnitId = '{ObjGlobal.SysCompanyUnitId}' OR CompanyUnitId IS NULL)";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetLastGiftVoucherNumber()
    {
        var cmdString =
            $@"SELECT MAX(CardNo)  CardNo FROM AMS.GiftVoucherList WHERE BranchId = {ObjGlobal.SysBranchId} AND (CompanyUnitId = '{ObjGlobal.SysCompanyUnitId}' OR CompanyUnitId IS NULL)";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }


    public DataTable GetTransactionFiscalYear()
    {
        const string cmdString = @"
			SELECT DISTINCT fy.FY_Id FiscalYearId, fy.BS_FY FiscalYear
			FROM AMS.AccountDetails ad
				LEFT OUTER JOIN AMS.FiscalYear fy ON ad.FiscalYearId=fy.FY_Id
			ORDER BY fy.FY_Id DESC;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterCounter(string actionTag, long selectedId = 0)
    {
        var cmdString = $@" SELECT  * FROM AMS.Counter WHERE CId = {selectedId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterUser()
    {
        const string cmdString =
            "SELECT User_Id, User_Name FROM AMS.UserInfo WHERE User_Name NOT IN ('ADMIN','AMSADMIN','MRDEMO','MRSOLUTION'); ";
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
    }

    public DataTable GetMasterFloor(string actionTag, int selectedId = 0)
    {
        var cmdString =
            $@"select FloorId LedgerId ,Description,ShortName,Type,Status from AMS.Floor where FloorId = '{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterMemberShip(string actionTag, int selectedId = 0)
    {
        var cmdString =
            $@"SELECT MSS.MShipId, MSS.MShipDesc, MSS.MShipShortName, MSS.PhoneNo, MSS.LedgerId,GL.GLName, MSS.EmailAdd, MSS.MemberTypeId,MT.MemberDesc, MSS.MemberId, MSS.BranchId, MSS.CompanyUnitId, MSS.MValidDate, MSS.MExpireDate, MSS.EnterBy, MSS.EnterDate, MSS.ActiveStatus, MSS.SyncGlobalId, MSS.SyncOriginId, MSS.SyncCreatedOn, MSS.SyncLastPatchedOn, MSS.SyncRowVersion, MSS.SyncBaseId from AMS.MemberShipSetup MSS left outer join AMS.GeneralLedger as GL on MSS.LedgerId = GL.GLID left outer join AMS.MemberType as MT on MSS.MemberTypeId = MT.MemberTypeId where MShipId= '{selectedId}'; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterMemberType(string actionTag, int selectedId = 0)
    {
        var cmdString = $@"Select * from AMS.MemberType where MemberTypeId = '{selectedId}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterSystemTagVatTerm(string source)
    {
        var cmdString = source switch
        {
            "SB" =>
                "SELECT pt.ST_Id,pt.ST_Name FROM AMS.ST_Term pt WHERE pt.ST_Id IN (SELECT sc.SBVatTerm FROM AMS.SalesSetting sc)",
            "PB" =>
                "SELECT pt.PT_Id,pt.PT_Name FROM AMS.PT_Term pt WHERE pt.PT_Id IN (SELECT sc.PBVatTerm FROM AMS.PurchaseSetting sc)",
            _ => string.Empty
        };
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterTable(string actionTag, int selectedId = 0)
    {
        var cmdString = string.Empty;
        if (selectedId == 0)
        {
            cmdString = "SELECT TableId,TableName,TableCode,FloorId FROM AMS.TableMaster  \n ";
            cmdString += "where  1=1  AND (Status = 1 or Status is Null)";
        }
        else
        {
            cmdString = $"select * from AMS.TableMaster where TableId = '{selectedId}' ";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterVehicle(string actionTag, long selectedId = 0)
    {
        var cmdString = string.Empty;
        if (selectedId == 0)
        {
            cmdString = @"
				Select P.PID LedgerId,P.PAlias Description,EngineNo,ChasisNo,VHColorsDesc,VHMDesc from AMS.Product as P left outer join AMS.ProductUnit as PU on P.PUnit = PU.UID left outer join AMS.ProductGroup as PG on PG.PGrpID = p.PGrpID left outer join AMS.ProductSubGroup as PSG on PSG.PSubGrpID = p.PSubGrpId left outer join AMS.VehicleColors vc ON p.VHColor = vc.VHColorsId
				LEFT OUTER JOIN AMS.[VehicleNumber] vm ON p.VHModel = vm.VHNoId LEFT OUTER JOIN AMS.[VehileModel] vn ON p.VHNumber = vn.VHModelId
				WHERE PCategory IN('2 Wheeler', '4 Wheeler')";
            if (actionTag.ToUpper() == "DELETE")
            {
                cmdString += "  and P.PID not in (Select Product_Id from AMS.StockDetails) ";
            }
        }
        else
        {
            cmdString = $@"
				Select * from AMS.Product as P left outer join AMS.ProductUnit as PU on P.PUnit = PU.UID left outer join AMS.ProductGroup as PG on PG.PGrpID = p.PGrpID left outer join AMS.ProductSubGroup as PSG on PSG.PSubGrpID = p.PSubGrpId left outer join AMS.VehicleColors vc ON p.VHColor = vc.VHColorsId
				LEFT OUTER JOIN AMS.[VehicleNumber] vm ON p.VHModel = vm.VHNoId LEFT OUTER JOIN AMS.[VehileModel] vn ON p.VHNumber = vn.VHModelId WHERE P.PID = '{selectedId}'";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetOnlineSync()
    {
        var cmdString = "select * from ams.[SyncTable]";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetRackList(string actionTag, int status = 0, int selectedId = 0)
    {
        var cmdString = string.Empty;
        if (selectedId == 0)
        {
            cmdString = "SELECT RID, RName, RCode, Location from AMS.RACK where 1=1 ";
            if (status > 0)
            {
                cmdString += "AND[Status] = 1 OR[Status] IS NULL";
            }
        }
        else
        {
            cmdString = $"SELECT RID, RName, RCode, Location from AMS.RACK where RID  = '{selectedId}'";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSubLedger(string tag, string category, string qtyVal, string amtVal, string loginDate, int selectedId = 0)
    {
        var cmdString = string.Empty;
        if (string.IsNullOrEmpty(loginDate))
        {
            loginDate = DateTime.Today.ToString();
        }

        cmdString = selectedId switch
        {
            0 =>
                @"Select SLName Particular,SLCode ShortName,SLId LedgerId,SLAddress,SLPhoneNo PhoneNo,GLName From AMS.SubLedger as SL Left Outer Join AMS.GeneralLedger as Gl On Gl.GLID=Sl.GLID and (Sl.Status=1 or Sl.Status is Null) order By SLName  ",
            _ => cmdString
        };
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetTagTermOnSalesValue()
    {
        const string cmdString =
            "Select SalesVat_Id,SalesDiscount_Id,SalesSpecialDiscount_Id from [AMS].[SystemConfiguration]";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMemberShipDiscount(string member = "")
    {
        var dt = new DataTable();
        if (member != string.Empty)
        {
            var cmdString =
                $"Select MShipId,LedgerId, MSS.MemberTypeId,Discount from  ams.MemberShipSetup MSS  left join AMS.MemberType MT on MSS.MemberTypeId = MT.MemberTypeId  where MSS.MShipDesc='{member}'";
            dt = SqlExtensions.ExecuteDataSet(cmdString)
                .Tables[0];
        }

        return dt;
    }

    public DataTable GetOpeningLedger(string category, string qtyVal, string amtVal, string loginDate)
    {
        if (loginDate.IsBlankOrEmpty() || loginDate == "  /  /    ")
        {
            loginDate = DateTime.Now.GetSystemDate();
        }

        var cmdString = @$"
			SELECT GLID, GLName, gl.GLCode, PanNo, Balance Balance, GrpName, ISNULL(AgentName, 'No Agent') AgentName, CAST(ISNULL(gl.CrLimit, 0) AS DECIMAL(18, 2)) CrLimit, ISNULL(gl.PhoneNo, 'No Number') PhoneNo, ISNULL(GLAddress, '') GLAddress
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup AG ON AG.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId=gl.AgentId
				 LEFT OUTER JOIN (
						SELECT Ledger_ID, SUM(LocalDebit_Amt -LocalCredit_Amt) Balance FROM AMS.AccountDetails WHERE Voucher_Date <'{Convert.ToDateTime(loginDate):yyyy-MM-dd}'
						GROUP BY Ledger_ID
				 ) b ON b.Ledger_ID = gl.GLID ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPosProductInfo(string filterTxt)
    {
        var cmdString = @"
			SELECT p.*, st.StockQty, pu.UnitCode UOM, pu1.UnitCode AltUOM, br.ProductId, br.Barcode, br.SalesRate, br.MRP, br.Trade, br.Wholesale, br.Retail, br.Dealer, br.Resellar, br.UnitId, br.AltUnitId
			 FROM AMS.Product p
				  LEFT OUTER JOIN (
									SELECT sd.Product_Id, SUM ( CASE WHEN sd.EntryType = 'I' THEN sd.StockQty ELSE -sd.StockQty END ) StockQty
									 FROM AMS.StockDetails sd
									 GROUP BY sd.Product_Id
								  ) AS st ON p.PID = st.Product_Id
				  LEFT OUTER JOIN AMS.ProductUnit pu ON p.PUnit = pu.UID
				  LEFT OUTER JOIN AMS.ProductUnit pu1 ON p.PAltUnit = pu1.UID
				  LEFT OUTER JOIN (
									SELECT bl.ProductId, bl.Barcode, bl.SalesRate, bl.MRP, bl.Trade, bl.Wholesale, bl.Retail, bl.Dealer, bl.Resellar, bl.UnitId, bl.AltUnitId
									 FROM AMS.BarcodeList bl
								  ) AS br ON br.ProductId = p.PID AND br.Barcode <> p.Barcode AND br.SalesRate <> p.PSalesRate ";
        cmdString += filterTxt.IsBlankOrEmpty() ? "; " : $" WHERE p.PID = {filterTxt};";
        var dtTable = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtTable;
    }

    public DataTable GetBookInformation(long selectedId)
    {
        var cmdString = selectedId switch
        {
            0 =>
                @" SELECT bd.ISBNNo ISBN_NO,bd.PrintDesc TITLE,ISNULL(bd.Author,'NO AUTHOR AVAILABLE') AUTHOR,ISNULL(bd.Publisher,'NO PUBLISHER AVAILABLE') PUBLISHER,CAST(p.PSalesRate AS DECIMAL(18,2)) SALES_RATE FROM AMS.Product p LEFT OUTER JOIN AMS.BookDetails bd ON p.PID = bd.BookId  ",
            _ => string.Empty
        };
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPartyInfo()
    {
        var dtPartyInfo = new DataTable();
        dtPartyInfo.AddStringColumns(new[]
        {
            "PartyLedgerId",
            "PartyName",
            "ChequeNo",
            "ChequeDate",
            "ChequeMiti",
            "VatNo",
            "ContactPerson",
            "Address",
            "City",
            "Mob",
            "Email"
        });
        return dtPartyInfo;
    }

    public DataTable GetBillingTerm()
    {
        var dtBillingTerm = new DataTable();
        dtBillingTerm.AddStringColumns(new[]
        {
            "OrderNo",
            "SNo",
            "TermId",
            "TermName",
            "Basis",
            "Sign",
            "ProductId",
            "TermType",
            "TermCondition",
            "TermRate",
            "TermAmt",
            "Source",
            "Formula",
            "ProductSno"
        });
        return dtBillingTerm;
    }

    public DataTable GetProductBatchFormat()
    {
        var dtBatch = new DataTable();
        dtBatch.AddStringColumns(columnNames: new string[]
        {
            "SNo",
            "VoucherType",
            "ProductId",
            "BatchNo",
            "MfDate",
            "ExpDate",
            "SizeNo",
            "SerialNo",
            "ChasisNo",
            "EngineNo",
            "VHModel",
            "VHColor",
            "AltQty",
            "Qty",
            "MRP",
            "Rate",
            "ProductSno"
        });
        return dtBatch;
    }

    public DataTable GetTermCalculationForVoucher(string module, string termType = "B")
    {
        string[] salesStrings = { "SQ", "SO", "SC", "SB", "SR" };
        var cmdString = string.Empty;
        if (salesStrings.Contains(module))
        {
            cmdString =
                $@" Select ST_Id TermId, Order_No OrderNo,ST_Name TermDesc,ST_Type TermType, ST_Condition TermCondition,CASE WHEN ST_Basis='V' THEN 'VALUE' ELSE 'QTY' END TermBasic, ST_Sign TermSign, ST_Rate TermRate from [AMS].ST_Term WHERE ST_Condition='{termType}' AND Module='SB' AND ST_Type <> 'A' ORDER BY Order_No";
        }
        else if (module.Equals("SAB"))
        {
            cmdString =
                $@"Select ST_Id TermId, Order_No OrderNo,ST_Name TermDesc,ST_Type TermType, ST_Condition TermCondition,CASE WHEN ST_Basis='V' THEN 'VALUE' ELSE 'QTY' END TermBasic, ST_Sign TermSign, ST_Rate TermRate from [AMS].ST_Term WHERE ST_Condition='{termType}' AND Module='SB' AND ST_Type = 'A' ORDER BY Order_No";
        }
        else if (module.Equals("PAB"))
        {
            cmdString =
                $@" Select PT_Id TermId, Order_No OrderNo,PT_Name TermDesc,PT_Type TermType, PT_Condition TermCondition,Ledger,PT_Basis TermBasic, PT_Sign TermSign, PT_Rate TermRate from [AMS].PT_Term WHERE PT_Condition='{termType}' AND Module='PB' AND PT_Type = 'A' ORDER BY Order_No ";
        }
        else
        {
            cmdString =
                $@" Select PT_Id TermId, Order_No OrderNo,PT_Name TermDesc,PT_Type TermType, PT_Condition TermCondition,CASE WHEN PT_Basis='V' THEN 'VALUE' ELSE 'QTY' END TermBasic, PT_Sign TermSign, PT_Rate TermRate from [AMS].PT_Term WHERE PT_Condition='{termType}' AND Module='PB' AND PT_Type <> 'A' ORDER BY Order_No ";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable ReturnMemberShipValue(int memberId)
    {
        var cmdString = $@"
		SELECT ms.MShipId, ms.MShipDesc, ms.MShipShortName, ms.PhoneNo, ms.LedgerId, ms.EmailAdd, ms.MemberTypeId,mt.MemberDesc, ms.MemberId, ms.BranchId, ms.CompanyUnitId, ms.MValidDate, ms.MExpireDate, ms.EnterBy, ms.EnterDate, ms.ActiveStatus, ms.SyncGlobalId, ms.SyncOriginId, ms.SyncCreatedOn, ms.SyncLastPatchedOn, ms.SyncRowVersion, ms.SyncBaseId,PriceTag,mt.Discount,
		(SELECT SUM(LN_Amount) Amount FROM AMS.SB_Master WHERE MShipId ='{memberId}' GROUP BY MShipId) Balance FROM AMS.MemberShipSetup ms
		LEFT OUTER JOIN ams.MemberType mt ON mt.MemberTypeId = ms.MemberTypeId
		WHERE ms.MShipId='{memberId}'";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable IsExitsCheckDocumentNumbering(string module)
    {
        var cmdString = $@"
        Select DocDesc from AMS.DocumentNumbering where DocModule = '{module}' and (FiscalYearId = '{ObjGlobal.SysFiscalYearId}' or FiscalYearId is NUll ) and (DocBranch = '{ObjGlobal.SysBranchId}' OR DocBranch IS NULL) and (DocUnit ='{ObjGlobal.SysCompanyUnitId}' or DocUnit is NUll);";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckPbdInvoiceData(string voucherNo, string module, string fiscalYear)
    {
        var cmdString = $@"
		SELECT P.PName,PAlias,P.PShortName, G.GName,G.GCode, P.PAltUnit, ALTU.UID as AltUnitId,P.PUnit ,U.UnitCode, {module}.*
		FROM AMS.{module}_Details as '{module}'\n";
        cmdString += @"
			LEFT OUTER JOIN AMS.Product AS P ON P.PID = PB.P_Id ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPrintVoucherList(string module)
    {
        var cmdString = @"
		SELECT ddp.*,pdd.Paths FROM AMS.DocumentDesignPrint ddp
            LEFT OUTER JOIN MASTER.AMS.PrintDocument_Designer pdd ON ddp.Paper_Name = pdd.Designerpaper_Name 
        WHERE 1=1 ";
        cmdString += module.Equals("SB") ? " AND ddp.Module in ('SB','ATI')" : @$" AND ddp.Module ='{module}' ";
        cmdString += @$" AND ddp.Branch_Id={ObjGlobal.SysBranchId}";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $@"Select * From AMS.{tableName} where {whereValue}='{inputTxt}'";
        cmdString += selectedId.GetLong() > 0 && actionTag != "SAVE" ? $" and {validId} <> {selectedId} " : "";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable IsDuplicate(string actionTag, string module, string inputValue, string inputId)
    {
        var table = module switch
        {
            "ACCOUNTGROUP" => "AMS.AccountGroup",
            "ACCOUNTSUBGROUP" => "AMS.AccountSubGroup",
            "GENERALLEDGER" => "AMS.GeneralLedger",
            "PRODUCT" => "AMS.Product",
            "PRODUCTGROUP" => "AMS.ProductGroup",
            _ => string.Empty
        };
        var column = module switch
        {
            "ACCOUNTGROUP" => "GrpName",
            "ACCOUNTSUBGROUP" => "SubGrpName",
            "GENERALLEDGER" => "GLName",
            "PRODUCT" => "PName",
            _ => string.Empty
        };
        var filterId = module switch
        {
            "ACCOUNTGROUP" => "GrpId",
            "ACCOUNTSUBGROUP" => "SubGrpId",
            "GENERALLEDGER" => "GLID",
            "PRODUCT" => "PID",
            _ => string.Empty
        };
        var cmdString = $@"
		SELECT* FROM {table} WHERE {column} = '{inputValue.GetTrimReplace()}'";
        cmdString += actionTag.Equals("UPDATE") ? $" AND {filterId} <> {inputId}" : " ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckMasterValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId)
    {
        var cmdString = $"Select * From {tableName} where {whereValue}='{inputTxt}'";
        if (selectedId.GetInt() > 0 && actionTag != "SAVE")
        {
            cmdString += $" and {validId} <> {selectedId} ";
        }
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable CheckMemberShipValidData(string value)
    {
        var cmdString = $@" 
        SELECT * FROM AMS.MemberShipSetup 
        WHERE PhoneNo ='{value}'  or MShipDesc ='{value}' ;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterBookList(string actionTag, long selectedId = 0)
    {
        var cmdString = string.Empty;
        if (selectedId == 0)
        {
            cmdString = @"
            SELECT PID,P.PName,P.PShortName,P.Barcode,UID,PU.UnitCode,P.PBuyRate BuyRate,P.PSalesRate PSalesRate,PG.PGrpID,PG.GrpName GrpName,PTax FROM AMS.Product AS P LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit = PU.UID LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpID = P.PGrpID WHERE 1=1 ";
            if (actionTag.ToUpper() == "DELETE")
            {
                cmdString += " and Pid not in (Select Product_Id from AMS.StockDetails) ";
            }
        }
        else
        {
            cmdString = $@"
		    SELECT PID,P.PName,P.PAlias,P.PShortName,P.Barcode,UID,PU.UnitCode,P.PBuyRate,P.PSalesRate ,P.PSalesRate,PMRP,P.TradeRate,P.Barcode,cast (P.PImage AS VARBINARY(MAX)) PImage,PG.PGrpID,PG.GrpName,psg.PSubGrpId,psg.SubGrpName,PTax, bd.Publisher, bd.Author FROM AMS.Product AS P 
                LEFT OUTER HASH JOIN AMS.ProductUnit AS PU ON P.PUnit = PU.UID 
                LEFT OUTER HASH JOIN AMS.ProductGroup AS PG ON PG.PGrpID = P.PGrpId 
                LEFT OUTER HASH JOIN AMS.BookDetails bd ON P.PID = bd.BookId 
                LEFT OUTER HASH JOIN AMS.ProductSubGroup psg ON P.PSubGrpId = psg.PSubGrpId  
            WHERE p.PID = {selectedId}
		    GROUP BY PID,P.PName,P.PAlias,P.PShortName,P.Barcode,UID,PU.UnitCode,P.PBuyRate,P.PSalesRate ,P.PSalesRate,PMRP,P.TradeRate,P.Barcode,cast (P.PImage AS VARBINARY(MAX)),PG.PGrpID,PG.GrpName,psg.PSubGrpId,psg.SubGrpName,PTax, bd.Publisher, bd.Author";
        }

        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- RETURN VALUE IN DATA TABLE ---------------


    // PRODUCT RETURN IN DATA TABLE

    #region --------------- RETURN VALUE IN DATA TABLE ---------------

    public DataTable GetProductListFromLedger(long ledgerId)
    {
        var cmdString = @$"
			SELECT p.PID ProductId, p.PName Product, ISNULL ( bl.Barcode, p.PShortName ) Barcode, p.PUnit, pu.UnitCode UOM, p.PSalesRate PMRP, ISNULL ( sb.Rate, p.PSalesRate ) SalesRate, p.PGrpId, ISNULL ( PG.GrpName, 'NO GROUP' ) PGroup, p.PSubGrpId, ISNULL ( psg.SubGrpName, 'NO SUBGROUP' ) SubGrpName
			 FROM AMS.Product p
				  LEFT OUTER JOIN AMS.SB_Details sb ON sb.P_Id = p.PID
				  LEFT OUTER JOIN AMS.SB_Master SM ON SM.SB_Invoice = sb.SB_Invoice
				  LEFT OUTER JOIN AMS.ProductGroup PG ON PG.PGrpID = p.PGrpId
				  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
				  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID = p.PUnit
				  LEFT OUTER JOIN AMS.BarcodeList bl ON bl.ProductId = p.PID AND bl.Barcode = p.Barcode AND bl.SalesRate <> p.PSalesRate
			 WHERE p.Status = 1 AND SM.Customer_Id='{ledgerId}'";
        cmdString += " ORDER BY p.PName, bl.Barcode, p.PShortName;";
        var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtProduct;
    }

    public DataTable GetProductListWithQty()
    {
        var cmdString = @"
		SELECT P.PShortName PShortName, P.PName, P.PID, ISNULL(PG.GrpName, 'NO CATEGORY') GrpName, ISNULL(PSG.SubGrpName, 'NO SUB-CATEGORY') SubGrpName, PU.UnitName, CAST(ISNULL(Stock.StockQty, 0) AS DECIMAL(18, 2)) StockQty, CONVERT(DECIMAL(18, 2), P.PBuyRate) CostRate, CASE WHEN CONVERT(INT, P.PTax)>0 THEN 'YES' ELSE 'NO' END Taxable, CONVERT(DECIMAL(18, 2), P.PMargin1) Margin, CONVERT(DECIMAL(18, 2), P.PSalesRate) SalesRate
        FROM AMS.Product AS P
             LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockQty ELSE -sd.StockQty END) StockQty FROM AMS.StockDetails sd WHERE sd.Voucher_Date<=GETDATE()GROUP BY sd.Product_Id) AS Stock ON Stock.Product_Id=P.PID
             LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=P.PGrpId
             LEFT OUTER JOIN AMS.ProductSubGroup PSG ON PSG.PSubGrpId=P.PSubGrpId
             LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID
             LEFT OUTER JOIN AMS.BarcodeList bl ON bl.ProductId=P.PID
        WHERE(P.PName<>'' AND P.PName IS NOT NULL)AND P.Status>0
        ORDER BY P.PName, P.PShortName;";
        var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dtProduct;
    }

    public DataTable GetMasterCounterProductList(int selectedId = 0, bool isActive = false)
    {
        var cmdString = @"
		SELECT P.HsCode,P.PShortName PShortName,PName,PID,ISNULL(GrpName,'NO CATEGORY') GrpName,UnitName,CAST(ISNULL(StockQty,0) AS DECIMAL(18,2)) StockQty,CONVERT(DECIMAL(18,2),P.PBuyRate) BuyRate,CASE WHEN CONVERT(INT,PTax) > 0 THEN 'YES' ELSE 'NO' END Taxable,CONVERT(DECIMAL(18,2),P.PMargin1) Margin,CONVERT(DECIMAL(18,2),PSalesRate) SalesRate,P.Barcode,Barcode1,P.Barcode2,P.Barcode3 FROM AMS.Product AS P
	         LEFT OUTER JOIN (SELECT sd.Product_Id,SUM(CASE WHEN sd.EntryType = 'I' THEN sd.StockQty ELSE -sd.StockQty END) StockQty FROM AMS.StockDetails sd
	         GROUP BY sd.Product_Id) AS Stock ON Stock.Product_Id = P.PID
	         LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId = P.PGrpId
	         LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit = PU.UID
	         --LEFT OUTER JOIN AMS.BarcodeList bl ON bl.ProductId = P.PID
        WHERE (PName <> '' AND PName IS NOT NULL) ";
        cmdString += isActive ? " AND P.Status = 1 " : " \n";
        cmdString += " ORDER BY P.PName, P.PShortName ;";

        //         var cmdString = @"
        //SELECT p.Barcode PShortName, PName, PID, ISNULL(GrpName, 'NO CATEGORY') GrpName, UnitName, CAST(ISNULL(StockQty, 0) AS DECIMAL(18, 2)) StockQty, CONVERT(DECIMAL(18, 2), P.PBuyRate) BuyRate, CASE WHEN CONVERT(INT, PTax)>0 THEN 'YES' ELSE 'NO' END Taxable, CONVERT(DECIMAL(18, 2), P.PMargin1) Margin, CONVERT(DECIMAL(18, 2), PSalesRate) SalesRate, P.Barcode, Barcode1
        //FROM AMS.Product AS P
        //	 LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockQty ELSE -sd.StockQty END) StockQty
        //					 FROM AMS.StockDetails sd
        //					 GROUP BY sd.Product_Id) AS Stock ON Stock.Product_Id=P.PID
        //	 LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=P.PGrpId
        //	 LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID
        //WHERE(PName<>'' AND PName IS NOT NULL)";
        //         cmdString += isActive ? " AND P.Status = 1 " : " \n";
        //         cmdString += " ORDER BY P.PName";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductListBarcode(long productId)
    {
        var cmdString = $@"
			SELECT  Pid SelectedId,P.PName,P.PShortName,PU.UnitCode,bl.SalesRate PSalesRate,PG.GrpName GrpName  FROM AMS.BarcodeList bl
			LEFT OUTER JOIN AMS.Product p ON p.PID = bl.ProductId
			left outer join AMS.ProductUnit as PU on  P.PUnit=PU.UID left outer join AMS.ProductGroup as PG on  PG.PGrpID=p.PGrpId
			WHERE bl.ProductId='{productId}'  AND bl.AltUnitId IS NULL AND ABS((BL.SalesRate - P.PSalesRate)) >  0; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetVehicleListWithQty(string qtyVal, string amtVal, string loginDate, bool aviableStock)
    {
        if (string.IsNullOrEmpty(loginDate))
        {
            loginDate = DateTime.Now.ToString("dd/MM/yyyy");
        }

        var cmdString =
            $" Select Pid,PAlias,PName,PShortName,Product.EngineNo,Product.ChasisNo,Product.VHColorsDesc,Product.VHModelDesc,isNull(CAST(Sum(BalanceQty) AS DECIMAL(18,{qtyVal})), 0) BalanceQty,UnitCode,isNull(CAST(PSalesRate AS DECIMAL(18, {amtVal})), 0) PSalesRate,GrpName,SubGrpName FROM (";
        cmdString +=
            " Select Pid, PAlias, P.PName, P.PShortName, P.EngineNo, P.ChasisNo, vc.VHColorsDesc, vm.VHModelDesc, vn.VNDesc , Case When EntryType= 'I' Then isnull(Sum(StockQty),0) Else - isnull(Sum(StockQty), 0) End BalanceQty, PU.UnitCode,P.PBuyRate BuyRate, P.PSalesRate PSalesRate, ISNULL(PG.GrpName, 'No Group') GrpName,ISNULL(PSG.SubGrpName, 'No SubGroup') SubGrpName from AMS.Product as P left outer join AMS.StockDetails as SD on P.PID = SD.Product_Id left outer join AMS.ProductUnit as PU on P.PUnit = PU.UID left outer join AMS.ProductGroup as PG on PG.PGrpID = p.PGrpID left outer join AMS.ProductSubGroup as PSG on PSG.PSubGrpID = p.PSubGrpId left outer join AMS.VehicleColors vc ON p.VHColor = vc.VHColorsId LEFT OUTER JOIN AMS.VehileModel vm ON p.VHModel = vm.VHModelId LEFT OUTER JOIN AMS.VehicleNumber vn ON p.VHNumber = vn.VHNoId  ";
        cmdString +=
            $" WHERE PCategory IN('2 Wheeler', '4 Wheeler') AND(P.Status = 1 OR p.Status IS NULL) AND (P.Branch_ID='{ObjGlobal.SysBranchId}' OR P.Branch_ID IS NULL) AND (P.CmpUnit_Id='{ObjGlobal.CompanyId}' OR P.CmpUnit_Id IS NULL)  "; //AND SD.Voucher_Date <='{ Convert.ToDateTime(LoginDate).ToString("yyyy-MM-dd")}'
        cmdString +=
            " Group By Pid,P.PName,P.PShortName,PU.UnitCode,P.PBuyRate,P.PSalesRate,PG.GrpName,EntryType,PAlias,SubGrpName,P.EngineNo,P.ChasisNo,vc.VHColorsDesc,vm.VHModelDesc,vn.VNDesc ";
        cmdString += " ) as Product ";
        cmdString +=
            " Group By Pid,PName,PShortName,UnitCode,BuyRate,PSalesRate,GrpName,PAlias,SubGrpName,Product.EngineNo,Product.ChasisNo,Product.VHColorsDesc,Product.VHModelDesc  ";
        if (aviableStock)
        {
            cmdString += $" HAVING isNull(CAST(Sum(BalanceQty) AS DECIMAL(18,{qtyVal})), 0) <> 0";
        }

        cmdString += " ORDER BY Product.PAlias ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductInfoWithBarcode(string filterTxt)
    {
        var cmdString = $@"
			SELECT PID SelectedId, P.PName, P.PShortName, PU.UnitCode, P.PSalesRate PSalesRate, PG.GrpName GrpName, CASE WHEN bl.AltUnitId>0 THEN 1 ELSE 0 END IsAltUnit, CASE WHEN bl.AltUnitId>0 THEN bl.AltUnitId ELSE bl.UnitId END Barcode
			FROM AMS.Product AS P
				 LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID
				 LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=P.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup PSG ON P.PSubGrpId=PSG.PSubGrpId
				 LEFT OUTER JOIN AMS.BarcodeList bl ON P.PID=bl.ProductId
			WHERE(P.Status IS NULL OR P.Status<>0)
			AND bl.Barcode ='{filterTxt}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetProductWithBarcode(string filterTxt)
    {
        var cmdString = $@"
			SELECT TOP 1 PID SelectedId, P.PName, P.PShortName, PU.UnitCode, P.PSalesRate PSalesRate, PG.GrpName GrpName, CASE WHEN bl.AltUnitId>0 THEN 1 ELSE 0 END IsAltUnit, CASE WHEN bl.AltUnitId>0 THEN bl.Barcode ELSE bl.ProductId END Barcode
			FROM AMS.Product AS P
				 LEFT OUTER JOIN AMS.ProductUnit AS PU ON P.PUnit=PU.UID
				 LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpId=P.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup PSG ON P.PSubGrpId=PSG.PSubGrpId
				 LEFT OUTER JOIN AMS.BarcodeList bl ON bl.ProductId = P.PID
			WHERE p.PShortName='{filterTxt}' OR P.Barcode='{filterTxt}' OR P.Barcode1='{filterTxt}' OR P.Barcode2='{filterTxt}' OR P.Barcode3='{filterTxt}' or bl.Barcode = '{filterTxt}' ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetMasterProductList(string actionTag, long selectedId = 0)
    {
        var commandText = $@"
			SELECT p.PID, p.PName, p.PAlias, p.PShortName,p.HsCode, p.PType, p.PCategory, p.PUnit, pu.UnitCode, p.PAltUnit, CAST(p.PImage AS VARBINARY(MAX)) PImage, pau.UnitCode AltUnitCode, ISNULL(s.StockQty, 0) StockQty, ISNULL(s.AltStockQty, 0) AltStockQty, p.PQtyConv, p.PAltConv, p.PValTech, p.PSerialno, p.PSizewise, p.PBatchwise, p.PBuyRate, p.PSalesRate, p.PMargin1, p.TradeRate, p.PMargin2, p.PMRP, p.PGrpId, pg.GrpName, p.PSubGrpId, psg.SubGrpName, p.PTax, p.PMin, p.PMax, p.CmpId, d.DName, p.CmpId1, p.CmpId2, p.CmpId3, p.Branch_ID, p.CmpUnit_ID, p.PPL, PPL.GLName PurchaseLedger, p.PPR, PPR.GLName PurchaseReturn, p.PSL, PSL.GLName SalesLedger, p.PSR, PSR.GLName SalesReturn, p.PL_Opening, PLOP.GLName OpeningLedger, p.PL_Closing, PLCL.GLName ClosingLedger, p.BS_Closing, BS.GLName BSClosingLedger, p.EnterBy, p.EnterDate, p.Status, p.BeforeBuyRate, p.BeforeSalesRate, p.Barcode, p.ChasisNo, p.VHModel, Barcode1, Barcode2, Barcode3,AltSalesRate
			FROM AMS.Product p
				 LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId=pg.PGrpId
				 LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
				 LEFT OUTER JOIN AMS.ProductUnit pau ON pau.UID=p.PAltUnit
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId=psg.PSubGrpId
				 LEFT OUTER JOIN AMS.GeneralLedger PPL ON p.PPL=PPL.GLID LEFT OUTER JOIN AMS.GeneralLedger PPR ON p.PPR=PPR.GLID LEFT OUTER JOIN AMS.GeneralLedger PSL ON p.PSL=PSL.GLID LEFT OUTER JOIN AMS.GeneralLedger PSR ON p.PSR=PSR.GLID LEFT OUTER JOIN AMS.GeneralLedger PLOP ON p.PL_Opening=PLOP.GLID LEFT OUTER JOIN AMS.GeneralLedger PLCL ON p.PL_Closing=PLCL.GLID LEFT OUTER JOIN AMS.GeneralLedger BS ON p.BS_Closing=BS.GLID
				 LEFT OUTER JOIN AMS.Department d ON p.CmpId=d.DId
				 LEFT OUTER JOIN(SELECT sd.Product_Id, SUM(CASE WHEN sd.EntryType='I' THEN sd.StockQty ELSE -sd.StockQty END) StockQty, SUM(CASE WHEN sd.EntryType='I' THEN sd.AltStockQty ELSE -sd.AltStockQty END) AltStockQty FROM AMS.StockDetails sd GROUP BY sd.Product_Id) AS s ON s.Product_Id=p.PID
			WHERE p.PID = {selectedId}";
        return SqlExtensions.ExecuteDataSet(commandText).Tables[0];
    }
    
    public DataTable GetBarcodeList(int groupId)
    {
        var cmdString = $@"
			SELECT bl.ProductId, bl.Barcode, bl.SalesRate, bl.MRP, bl.Trade, bl.Wholesale, bl.Retail, bl.Dealer, bl.Resellar, ISNULL(bl.UnitId, bl.AltUnitId) UnitId, ISNULL(pu.UnitCode, pu1.UnitCode) UOM, CASE WHEN bl.AltUnitId>0 THEN 1 ELSE 0 END isAltUnit
            FROM AMS.BarcodeList bl
                 LEFT OUTER JOIN AMS.ProductUnit pu ON bl.UnitId=pu.UID
                 LEFT OUTER JOIN AMS.ProductUnit pu1 ON bl.AltUnitId=pu1.UID
	             LEFT OUTER JOIN AMS.Product p ON p.PID = bl.ProductId
	             WHERE p.PGrpId ={groupId} ;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- RETURN VALUE IN DATA TABLE ---------------


    //RETURN STATIC INT VALUE

    #region --------------- RETURN STATIC VALUE IN INT ---------------

    public static int ReturnMaxIntValue(string table, string column)
    {
        var cmdString = $"SELECT CAST(ISNULL(MAX({column}),0) AS INT) + 1 MaxId FROM {table} ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows[0]["MaxId"].GetInt();
    }

    public static int ReturnMaxSyncBaseIdValue(string table, string column, string tableId, string filterValue)
    {
        var cmdString =
            $"SELECT MAX(CAST(ISNULL({column},0) AS INT))+1 MaxId FROM {table} where {tableId} = '{filterValue}' ";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return dt.Rows[0]["MaxId"].GetInt();
    }

    public static int ReturnMaxCountValue(string column, string table, string filterValue, string tableId)
    {
        var cmdString =
            $"SELECT COUNT({column}) MaxId FROM {table} WHERE DB_NAME = '{filterValue}' AND ACTION = '{tableId}'";
        var dt = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        return ObjGlobal.ReturnInt(dt.Rows[0]["MaxId"].ToString());
    }

    #endregion --------------- RETURN STATIC VALUE IN INT ---------------


    // RETURN STATIC VALUE ON DATA TABLE

    #region --------------- RETURN STATIC VALUE IN DATA TABLE ---------------

    public static DataTable GetDocumentNumberingSchema(string module)
    {
        var cmdString =
            $"Select * from AMS.DocumentNumbering where DocModule = '{module}' and FiscalYearId = '{ObjGlobal.SysFiscalYearId}' and (DocBranch = '{ObjGlobal.SysBranchId}' OR DocBranch IS NULL) ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public static DataTable LedgerInformation(long ledgerId, string vDate)
    {
        var cmdString = new StringBuilder();
        cmdString.AppendFormat($@"
			SELECT gl.GLID, gl.GLName, gl.GLCode, gl.ACCode, gl.GLType, gl.PhoneNo, gl.PanNo, gl.AgentId, ja.AgentName, gl.CrDays, gl.CrLimit, gl.CrTYpe, gl.Status, gl.CurrId, c.CCode, c.CRate,
			  (
				SELECT SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)
				FROM AMS.AccountDetails ad
				WHERE ad.Voucher_Date<='{vDate}' AND ad.Ledger_ID='{ledgerId}'
			  ) Amount
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
				 LEFT OUTER JOIN AMS.Currency c ON gl.CurrId=c.CId
			WHERE gl.GLID='{ledgerId}'; ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public static DataTable PartyInformation(string partyInfo)
    {
        var cmdString = new StringBuilder();
        cmdString.AppendFormat($@"
			SELECT ad.PartyLedger_Id,ad.PartyName,ad.Party_PanNo FROM AMS.AccountDetails ad
			WHERE ad.PartyName = '{partyInfo}' and ad.PartyName IS NOT NULL
			GROUP BY ad.PartyLedger_Id,ad.PartyName,ad.Party_PanNo ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public static DataTable GenerateRestaurantFloor()
    {
        var description = "Select FloorId,Description,ShortName from AMS.Floor Order By Description";
        return SqlExtensions.ExecuteDataSet(description).Tables[0];
    }

    public static DataTable GenerateRestaurantTable(string tableStatus, int floorId, int tableId = 0)
    {
        var commandText = tableStatus switch
        {
            "All" when floorId > 0 =>
                $"SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm WHERE tm.FloorId ={floorId} ORDER BY EnterDate,TableName,TableStatus ",
            "All" when floorId is 0 =>
                "SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm ORDER BY tm.IsPrePaid DESC,TableStatus desc,tm.TableType DESC,EnterDate,TableName",
            "All" when tableId > 0 =>
                $"SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm  WHERE tm.TableId <> {tableId} ORDER BY EnterDate,TableName,TableStatus",
            "Transfer" when tableId > 0 =>
                $"SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm  WHERE tm.TableId <> {tableId} AND tm.TableStatus='A' ORDER BY EnterDate,TableName,TableStatus",
            "Combine" when tableId > 0 =>
                $"SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm  WHERE tm.TableId <> {tableId}  ORDER BY EnterDate,TableName,TableStatus",
            "A" or "O" or "B" =>
                $"SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm WHERE tm.TableStatus='{tableStatus}' ORDER BY EnterDate,TableName,TableStatus",
            _ =>
                "SELECT tm.TableId, tm.TableName,tm.TableStatus,tm.TableType,tm.IsPrePaid FROM AMS.TableMaster tm  ORDER BY tm.IsPrePaid DESC,TableStatus desc,tm.TableType DESC,EnterDate,TableName"
        };
        var dt = SqlExtensions.ExecuteDataSet(commandText).Tables[0];
        return dt;
    }

    #endregion --------------- RETURN STATIC VALUE IN DATA TABLE ---------------


    // RETURN VALUE IN COMBO BOX

    #region --------------- RETURN VALUE IN COMBO BOX ---------------

    public void BindGiftVoucherType(ComboBox cmbType)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("RECURRING", "R"),
            new("ONE-TIME", "O")
        };
        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
    }

    public void BindStockValueType(ComboBox cmbType)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("FIFO", "FIFO"),
            new("LIFO", "LIFO"),
            new("Adjustment", "A"),
            new("WT.Avg", "WT")
        };
        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
    }

    public void BindProductType(ComboBox cmbType)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Inventory", "I"),
            new("Service", "S"),
            new("Assets", "A")
        };
        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
        cmbType.SelectedIndex = 0;
    }

    public void BindProductCategory(ComboBox cmbType)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("Finished Goods", "FG"),
            new("Raw Material", "RM")
        };
        cmbType.DataSource = list;
        cmbType.DisplayMember = "Item1";
        cmbType.ValueMember = "Item2";
    }

    public void BindFiscalYear(ComboBox cmbType)
    {
        var dt = GetTransactionFiscalYear();
        cmbType.DataSource = dt;
        cmbType.DisplayMember = "FiscalYear";
        cmbType.ValueMember = "FiscalYearId";
    }

    public void BindPaymentType(ComboBox box)
    {
        var list = new List<ValueModel<string, string>>
        {
            new("CASH", "CASH"),
            new("CREDIT", "CREDIT"),
            new("CARD", "CARD"),
            new("AMEX", "AMEX"),
            new("MASTER CARD", "MASTER CARD"),
            new("UNION PAY", "UNION PAY"),
            new("SCT CARD", "SCT CARD"),
            new("VISA CARD", "VISA CARD"),
            new("BANK", "BANK"),
            new("PHONE PAY", "PHONE PAY"),
            new("E-SEWA", "E-SEWA"),
            new("KHALTI", "KHALTI"),
            new("REMIT", "REMIT"),
            new("CONNECTIPS", "CONNECTIPS"),
            new("PARTIAL", "PARTIAL"),
            new("ADVANCE", "ADVANCE"),
            new("GIFT VOUCHER", "GIFT"),
            new("OTHER", "OTHER")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = 0;
    }

    public void BindVoucherType(ComboBox box, string module = "SB")
    {
        var list = new List<ValueModel<string, string>>
        {
            new("NORMAL", "NORMAL"),
            new(module.Equals("PB") ? "IMPORT" : "EXPORT", module.Equals("PB") ? "IMPORT" : "EXPORT"),
            new("ASSETS", "ASSETS"),
            new("ABBREVIATION", "AVT"),
            new("P VAT", "POS")
        };
        box.DataSource = list;
        box.DisplayMember = "Item1";
        box.ValueMember = "Item2";
        box.SelectedIndex = 0;
    }

    #endregion --------------- RETURN VALUE IN COMBO BOX ---------------


    // OBJECT FOR MASTER

    #region --------------- GLOBAL VALUE ---------------
    public List<BranchRights> GetRights { get; set; }
    public AccountGroup ObjAccountGroup { get; set; }
    public AccountSubGroup ObjAccountSubGroup { get; set; }
    public SyncLogDetails ObjSyncLogDetail { get; set; }
    public UserAccessControl ObjUserAccessControl { get; set; }
    public Branch ObjBranch { get; set; }
    public CostCenter ObjCostCenter { get; set; }
    public Counter ObjCounter { get; set; }
    public Currency Currency { get; set; }
    public Department ObjDepartment { get; set; }
    public DocumentNumbering ObjDocumentNumbering { get; set; }
    public FloorSetup Floor { get; set; }
    public GeneralLedger ObjGeneralLedger { get; set; }
    public Godown ObjGodown { get; set; }
    public MainAgent ObjSeniorAgent { get; set; }
    public JuniorAgent ObjJuniorAgent { get; set; }
    public MainArea ObjMainArea { get; set; }
    public Area ObjArea { get; set; }
    public MemberShipSetup ObjMembershipSetup { get; set; }
    public MemberType ObjMemberType { get; set; }
    public Product ObjProduct { get; set; }
    public ProductGroup ObjProductGroup { get; set; }
    public ProductSubGroup ObjProductSubGroup { get; set; }
    public ProductUnit ObjProductUnit { get; set; }
    public RACK ObjRack { get; set; }
    public SMS_CONFIG ObjSms { get; set; }
    public SubLedger ObjSubLedger { get; set; }
    public SyncTable ObjSync { get; set; }

    public CompanyUnit CompanyUnit { get; set; }
    public BarcodeList BarcodeList { get; set; }
    public NR_Master Narration { get; set; }
    public BR_LOG BrMaster { get; set; }
    public LOGIN_LOG LoginLog { get; set; }
    public BookDetails BookDetails { get; set; }
    public Scheme_Master SchemeMaster { get; set; }
    public PT_Term PtTerm { get; set; }
    public ST_Term StTerm { get; set; }
    public GiftVoucherList GiftVoucherList { get; set; }

    public FloorSetup ObjFloor { get; set; }
    #endregion --------------- GLOBAL VALUE ---------------
}