using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Properties;
using MrDAL.Setup.Interface;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using DatabaseModule.Setup.CompanyMaster;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;

namespace MrDAL.Setup.CompanySetup;

public class CompanySetupRepository : ICompanySetup
{
    // METHODS FOR THIS FORM
    public bool NewCreateDatabase(string databasePath, string initialCatalog, string actionTag)
    {
        var excString = new StringBuilder();
        var newCreateDatabase = false;
        try
        {
            switch (actionTag)
            {
                case "SAVE":
                {
                    var cmdString = $@"
							 CREATE DATABASE[{initialCatalog}] 
                             CONTAINMENT = NONE ON PRIMARY (NAME = N'{databasePath}', FILENAME = N'{databasePath}{initialCatalog}.mdf' , SIZE = 35584KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB)
							 LOG ON (NAME = N'{databasePath}Log', FILENAME = N'{databasePath}{initialCatalog}.ldf' , SIZE = 5696KB , MAXSIZE = 2048GB , FILEGROWTH = 100% )
                             COLLATE SQL_Latin1_General_CP1_CI_AS;
							 ALTER DATABASE[{initialCatalog}] SET COMPATIBILITY_LEVEL = 100;
							 IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
							 begin
							 EXEC[{initialCatalog}].[dbo].[sp_fulltext_database] @action = 'enable'
							 end ";
                    var sResult = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
                    if (sResult != 0)
                    {
                        GetConnection.LoginInitialCatalog = ObjGlobal.InitialCatalog = initialCatalog;
                        ObjGlobal.GetFiscalYearDetails();
                        CreateDatabaseTable.CreateTableOnDatabase();
                        SaveCompanyInfo(actionTag);
                        AlterDatabaseTable.DefaultFiscalYearValueOnDatabase();
                        RunDateMiti();
                        RunDefaultValue();
                        newCreateDatabase = true;
                    }

                    break;
                }

                case "UPDATE":
                {
                    SaveCompanyInfo(actionTag);
                    return true;
                }

                case "DELETE":
                {
                    var excQuery = $@"
                            DELETE FROM MASTER.AMS.CompanyRights WHERE Company_Id ='{ObjGlobal.CompanyId}';
							DELETE FROM MASTER.AMS.GlobalCompany WHERE GComp_Id ='{ObjGlobal.CompanyId}';";
                    var result = SqlExtensions.ExecuteNonTrans(excQuery);
                    if (result > 0)
                    {
                        excQuery = @$"
                                EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'{GetConnection.LoginInitialCatalog}'";
                        var i = SqlExtensions.ExecuteNonQuery(excQuery);
                        var drop = $"DROP DATABASE {GetConnection.LoginInitialCatalog}";
                        KillAllConnection(GetConnection.LoginInitialCatalog);
                        var k = new SqlCommand(drop, GetConnection.GetConnectionMaster()).ExecuteNonQuery();
                    }

                    newCreateDatabase = true;
                    break;
                }
            }

            return newCreateDatabase;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return false;
        }
    }
    public int SaveCompanyInfo(string actionTag)
    {
        var cmdString = string.Empty;

        try
        {
            if (actionTag is "SAVE")
            {
                cmdString = $@"
					DELETE FROM AMS.CompanyInfo ;
					INSERT INTO AMS.CompanyInfo(Company_Name, PrintDesc, Company_Logo, CReg_Date, Address, Country, State, City, PhoneNo, Fax, Pan_No, Email, Website, Database_Name, Database_Path, Version_No, Status, CreateDate, SoftModule, LoginDate, IsSyncOnline, ApiKey)
					VALUES(N'{Info.Company_Name}',N'{Info.PrintDesc}', NULL, '{Info.CReg_Date.GetSystemDate()}',";
                cmdString += Info.Address.IsValueExits() ? $" N'{Info.Address}'," : "NULL,";
                cmdString += Info.Country.IsValueExits() ? $" N'{Info.Country}', " : "NULL,";
                cmdString += Info.State.IsValueExits() ? $" N'{Info.State}', " : "NULL,";
                cmdString += Info.City.IsValueExits() ? $" N'{Info.City}', " : "NULL,";
                cmdString += Info.PhoneNo.IsValueExits() ? $" N'{Info.PhoneNo}', " : "NULL,";
                cmdString += Info.Fax.IsValueExits() ? $" N'{Info.Fax}', " : "NULL,";
                cmdString += Info.Pan_No.IsValueExits() ? $" N'{Info.Pan_No}'," : "NULL,";
                cmdString += Info.Email.IsValueExits() ? $" N'{Info.Email}'," : "NULL,";
                cmdString += Info.Website.IsValueExits() ? $" N'{Info.Website}', " : "NULL,";
                cmdString += $" N'{Info.Database_Name}', N'{Info.Database_Path}', 0, 1, GETDATE(), NUll, GETDATE(), CAST('{Info.IsSyncOnline}' AS BIT) ,";
                cmdString += Info.ApiKey.IsGuidExits() ? $" '{Info.ApiKey}'); " : " NULL);";
            }


            else if (actionTag is "UPDATE")
            {
                cmdString = $@"
					UPDATE AMS.CompanyInfo SET Company_Name = N'{Info.Company_Name}',";
                cmdString += Info.Address.IsValueExits() ? $" Address = N'{Info.Address}'," : "Address = NULL,";
                cmdString += Info.Country.IsValueExits() ? $" Country = N'{Info.Country}', " : "Country  = NULL,";
                cmdString += Info.State.IsValueExits() ? $" State = N'{Info.State}', " : "State = NULL,";
                cmdString += Info.City.IsValueExits() ? $" City = N'{Info.City}', " : "City = NULL,";
                cmdString += Info.PhoneNo.IsValueExits() ? $" PhoneNo = N'{Info.PhoneNo}', " : "PhoneNo = NULL,";
                cmdString += Info.Fax.IsValueExits() ? $" Fax = N'{Info.Fax}', " : "Fax = NULL,";
                cmdString += Info.Pan_No.IsValueExits() ? $" Pan_No = N'{Info.Pan_No}'," : "Pan_No = NULL,";
                cmdString += Info.Email.IsValueExits() ? $" Email = N'{Info.Email}'," : "Email = NULL,";
                cmdString += Info.Website.IsValueExits() ? $" Website = N'{Info.Website}', " : "Website = NULL,";
                cmdString += $" IsSyncOnline = CAST('{Info.IsSyncOnline}' AS BIT) ,";
                cmdString += $" Database_Name = '{Info.Database_Name}' ,";
                cmdString += Info.ApiKey.IsGuidExits() ? $" ApiKey = '{Info.ApiKey}' " : " ApiKey = NULL";
            }
            var result = SqlExtensions.ExecuteNonQuery(cmdString);
            if (result != 0)
            {
                SaveGlobalCompany(actionTag);
            }
            return result;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public int MaxCompanyRightsId()
    {
        var cmdString = @$"
            SELECT ISNULL(MAX(CompanyRights_Id),0) +1 CompanyRightsId FROM AMS.CompanyRights; ";
        var result = SqlExtensions.ExecuteDataSetSqlMaster(cmdString);
        if (result.RowsCount() > 0)
        {
            return result.Rows[0].GetInt();
        }
        return 1;

    }

    public void RunDefaultValue()
    {
        try
        {
            var isOk = SqlExtensions.ExecuteNonQuery(Resources.DefaultValueInDatabase);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }
    public int SaveGlobalCompany(string actionTag)
    {
        var cmdString = string.Empty;
        if (actionTag is "SAVE")
        {
            cmdString = $@"
			INSERT INTO AMS.GlobalCompany (Database_Name, Company_Name, PrintingDesc, Database_Path, Status, PanNo, Address, CurrentFiscalYear, LoginDate, DataSyncOriginId, DataSyncApiBaseUrl,ApiKey)
			VALUES ('{Setup.Database_Name}','{Setup.Company_Name.GetTrimReplace()}','{Setup.PrintingDesc.GetTrimReplace()}','{Setup.Database_Path}',1,";
            cmdString += Setup.PanNo.IsValueExits() ? $"'{Setup.PanNo}'," : "NULL,";
            cmdString += Setup.Address.IsValueExits() ? $"'{Setup.Address.GetTrimReplace()}'," : "NULL,";
            cmdString += Setup.CurrentFiscalYear.IsValueExits() ? $"'{Setup.CurrentFiscalYear}', " : "NULL,";
            cmdString += $"GETDATE(),NULL,NULL,'{Setup.ApiKey}' ); \n";
        }
        else if (actionTag is "UPDATE")
        {
            cmdString = $@"
            UPDATE AMS.GlobalCompany
			SET Company_Name = '{Setup.Company_Name.GetTrimReplace()}'
				,PanNo = N'{Setup.PanNo}'
				,Address = N'{Setup.Address.GetTrimReplace()}'
				,LoginDate = GETDATE()
			WHERE Database_Name = '{Setup.Database_Name}';
            UPDATE AMS.GlobalCompany set ApiKey= NewId() where ApiKey is null AND Database_Name = '{Info.Database_Name}'; ";
        }
        else if (actionTag is "DELETE")
        {
            SaveCompanyRights(actionTag);
            cmdString = $@" DELETE AMS.GlobalCompany WHERE Database_Name = '{Setup.Database_Name}' or GComp_Id = {Setup.GComp_Id};";
        }

        var isOk = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        if (isOk != 0 && actionTag != "DELETE")
        {
            cmdString = @$"
            SELECT gc.GComp_Id FROM MASTER.AMS.GlobalCompany gc WHERE gc.Database_Name='{Setup.Database_Name}'";
            var companyId= cmdString.GetQueryData().GetInt();
            foreach (var list in RightsList)
            {
                list.Company_Id = companyId;
            }
            SaveCompanyRights(actionTag);
        }

        return isOk;
    }
    public int SaveCompanyRights(string actionTag)
    {
        var cmdString = string.Empty;
        if (actionTag is "SAVE")
        {
            foreach (var list in RightsList)
            {
                cmdString = $@"DELETE MASTER.AMS.CompanyRights where User_Id = '{list.User_Id}' ";
                cmdString += list.Company_Id > 0 ? $" AND Company_Id = {list.Company_Id}; \n" : " \n";
                cmdString += $@"
                    INSERT INTO MASTER.[AMS].[CompanyRights] ([User_Id],[Company_Id],[Company_Name],[Start_AdDate],[End_AdDate],[Modify_Start_AdDate],[Modify_End_AdDate],[Back_Days_Restriction])
				    VALUES('{list.User_Id}',{list.Company_Id},'{list.Company_Name}',";
                cmdString += list.Start_AdDate.IsValueExits() ? $"'{list.Start_AdDate.GetSystemDate()}', " : "NULL,";
                cmdString += list.End_AdDate.IsValueExits() ? $"'{list.End_AdDate.GetSystemDate()}', " : "NULL,";
                cmdString += $" NULL,NULL,0);";

            }
        }
        else if (actionTag is "UPDATE")
        {
            foreach (var list in RightsList)
            {
                cmdString = $@"DELETE AMS.CompanyRights where User_Id = '{list.User_Id}' ";
                cmdString += list.Company_Id > 0 ? $" AND Company_Id = {list.Company_Id}; \n" : " \n";
                cmdString += $@"
                INSERT INTO [AMS].[CompanyRights] ([User_Id],[Company_Id],[Company_Name],[Start_AdDate],[End_AdDate],[Modify_Start_AdDate],[Modify_End_AdDate],[Back_Days_Restriction])
				VALUES('{list.User_Id}',{list.Company_Id},N'{list.Company_Name}',";
                cmdString += list.Start_AdDate.IsValueExits() ? $"'{list.Start_AdDate.GetSystemDate()}', " : " NULL,";
                cmdString += list.End_AdDate.IsValueExits() ? $"'{list.End_AdDate.GetSystemDate()}', " : " NULL,";
                cmdString += $" NULL,NULL,0);";

            }
        }
        else if (actionTag is "DELETE")
        {
            foreach (var list in RightsList)
            {
                cmdString = @$"DELETE AMS.CompanyRights WHERE Company_Id = '{list.Company_Id}' ";
                cmdString += ObjGlobal.LogInUser.GetUpper() is "ADMIN" ? "" :  $"AND User_Id = {list.User_Id}; ";
            }
        }

        var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        return result;
    }
    public void KillAllConnection(string dbName)
    {
        var kQuery = new StringBuilder();
        kQuery.Append($@"
			USE MASTER
			DECLARE @SQL AS VARCHAR(20), @SPID AS INT
			SELECT @SPID = MIN(SPID)  FROM MASTER..SYSPROCESSES  WHERE DBID = DB_ID('{dbName}') AND SPID != @@SPID
			WHILE (@SPID IS NOT NULL)
			BEGIN
			-- PRINT 'KILLING PROCESS ' + CAST(@SPID AS VARCHAR) + ' ...'
			SET @SQL = 'KILL ' + CAST(@SPID AS VARCHAR)
			EXEC (@SQL)
			SELECT
				@SPID = MIN(SPID)
			FROM
				MASTER..SYSPROCESSES
			WHERE
				DBID = DB_ID('{dbName}')
				AND SPID != @@SPID
			END
			--PRINT 'PROCESS COMPLETED...'");
        SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(kQuery.ToString());
    }
    public int RunDateMiti()
    {
        try
        {
            var isQuery = SqlExtensions.ExecuteNonQuery(Resources.DefaultDateMiti);
            if (isQuery == 0)
            {
                return isQuery;
            }

            var table = GetConnection.SelectDataTableQuery("SELECT * FROM MASTER.AMS.DATEMITI");
            if (table.Rows.Count is not 0)
            {
                return isQuery;
            }

            const string cmd = @"INSERT MASTER.AMS.DateMiti SELECT * FROM AMS.DateMiti dm";
            var inMasterDate = GetConnection.ImpExecuteNonQuery(cmd);

            return isQuery;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public static int UpdateCurrentVersion(string version)
    {
        var result = 0;
        var vQuery = new StringBuilder();
        try
        {
            vQuery.AppendFormat($@"Update AMS.CompanyInfo Set Version_No ='{version}'");
            result = SqlExtensions.ExecuteNonQuery(vQuery.ToString());
            return result;
        }
        catch
        {
            IRecalculate objUtility = new ClsRecalculate();
            objUtility.ShrinkDatabase(GetConnection.LoginInitialCatalog);
            objUtility.ShrinkDatabaseLog(GetConnection.LoginInitialCatalog);
            return result;
        }
    }
    public static bool UpdateDatabase()
    {
        var result = true;
        try
        {
            CreateDatabaseTable.CheckDatabaseExitsOrNot();
            CreateDatabaseTable.CreateTableOnDatabase();
        }
        catch
        {
            IRecalculate objRecalculate = new ClsRecalculate();
            objRecalculate.ShrinkDatabase(GetConnection.LoginInitialCatalog);
            objRecalculate.ShrinkDatabaseLog(GetConnection.LoginInitialCatalog);
            result = false;
        }

        return result;
    }
    public static int UpdateLogInDate()
    {
        try
        {
            var cmdBuilder = new StringBuilder();
            cmdBuilder.Append(" Update AMS.CompanyInfo Set LoginDate = GetDate(); \n");
            cmdBuilder.Append(
                $" UPDATE MASTER.AMS.GlobalCompany SET LoginDate = GETDATE() WHERE Database_Name='{GetConnection.LoginInitialCatalog}'; \n");
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdBuilder.ToString());
            return result;
        }
        catch
        {
            return 0;
        }
    }


    // RETURN VALUE IN DATA TABLE
    public DataTable GetCompanyInfo(int companyId)
    {
        var cmd = @$"
            SELECT cr.Company_Id CompanyId, cr.Company_Name CompanyName, gc.Database_Name FileName
            FROM   AMS.CompanyRights cr JOIN AMS.GlobalCompany gc ON gc.GComp_Id=cr.Company_Id
            WHERE cr.Company_Id = {companyId};";
        var report = SqlExtensions.ExecuteDataSetSqlMaster(cmd);

        return report;
    }
    public DataTable GetCompanyRightList(int userId)
    {
        var report = new DataTable();
        var cmd = string.Empty;

        cmd = @$"
            SELECT cr.Company_Id CompanyId, cr.Company_Name CompanyName, gc.Database_Name FileName
            FROM   AMS.CompanyRights cr JOIN AMS.GlobalCompany gc ON gc.GComp_Id=cr.Company_Id
            WHERE cr.User_Id = {userId};";
        report = SqlExtensions.ExecuteDataSetSqlMaster(cmd);

        return report;
    }

    // OBJECT FOR THIS FORM
    public CompanyInfo Info { get; set; } = new();
    public GlobalCompany Setup { get; set; } = new();
    public List<CompanyRights> RightsList { get; set; } = [];
}