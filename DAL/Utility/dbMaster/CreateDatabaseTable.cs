using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Properties;
using MrDAL.Utility.Server;
using System;

namespace MrDAL.Utility.dbMaster;

public class CreateDatabaseTable
{
    // DATA BASE TABLE

    #region **--------------- DATA BASE TABLE ---------------**

    public static bool CheckDatabaseExitsOrNot()
    {
        const string commandText = " SELECT Database_Name FROM AMS.CompanyInfo";
        using var dt = SqlExtensions.ExecuteDataSet(commandText).Tables[0];
        if (dt.Rows.Count <= 0)
        {
            return false;
        }
        var database = dt.Rows[0]["Database_Name"].ToString();
        if (database == ObjGlobal.InitialCatalog)
        {
            return true;
        }
        var query = $"UPDATE AMS.CompanyInfo set Database_Name = '{ObjGlobal.InitialCatalog}'";
        _ = SqlExtensions.ExecuteNonQuery(query);
        return true;
    }

    public static bool CreateTableOnDatabase()
    {
        try
        {
            DropTrigger();
            AlterDatabaseTable.CreateDatabaseTableColumn();
            AlterDatabaseTable.InsertDocumentNumbering();
            CreateTrigger();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(new
            {
                Class = "ClsCreateDatabaseTable",
                Method = "CreateDatabaseTable"
            });
        }

        return true;
    }

    #endregion **--------------- DATA BASE TABLE ---------------**

    // MASTER TABLE

    #region *--------------- Master Table ---------------*

    public static void CreateMasterTable()
    {
        var result = 0;
        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT s.name FROM sys.schemas s WHERE s.name='AMS')BEGIN
                DECLARE @AMS NVARCHAR(MAX);
                SET @AMS='CREATE SCHEMA AMS AUTHORIZATION dbo;';
                EXEC sys.sp_executesql @AMS;
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT name FROM sys.server_principals WHERE name='MrSolution')BEGIN
               CREATE LOGIN [MrSolution] WITH PASSWORD=N'ÁÙi$Øõ/.Ï¢a£à¡IK(h;%ÑÎ', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
            END;
            ALTER LOGIN [MrSolution] DISABLE;  ";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            result = SqlExtensions.ExecuteNonQueryOnMaster(Resources.CreateMasterTableQuery);
            if (result != 0) CreateMasterProc();
        }
        catch
        {
            // ignored
        }
    }

    private static void CreateMasterProc()
    {
        var result = 0;
        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='P' AND name='Usp_IUD_User_Role')BEGIN
                EXEC('CREATE Proc AMS.Usp_IUD_User_Role as Select @@servername;');
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='P' AND name='Usp_IUD_UserInfo')BEGIN
                EXEC('Create Proc AMS.Usp_IUD_UserInfo as select @@servername;');
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='P' AND name='Usp_IUD_UserInfo')BEGIN
                EXEC('Create Proc AMS.Usp_IUD_UserInfo as select @@servername;');
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='P' AND name='Usp_MSelect_Data')BEGIN
                EXEC('Create Proc AMS.Usp_MSelect_Data as select @@servername;');
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        AlterMasterPoc();
    }

    public static void AlterMasterPoc()
    {
        var result = 0;
        try
        {
            const string cmdString = @"
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE name='FK_UserInfo_User_Role')BEGIN
                ALTER TABLE [AMS].[UserInfo] WITH CHECK
                ADD CONSTRAINT [FK_UserInfo_User_Role] FOREIGN KEY([Role_Id])REFERENCES [AMS].[User_Role]([Role_Id]);
                ALTER TABLE [AMS].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_User_Role];
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
			ALTER PROCEDURE [AMS].[Usp_IUD_User_Role] (@Event CHAR(2) = 'I',
            @Id INT = 0,
            @Role VARCHAR(50)=NULL,
            @Status BIT=1,
            @Msg VARCHAR(MAX) OUTPUT,
            @Return_Id INT OUTPUT)
            AS
            IF @Event = 'I'  --for Insert
            BEGIN
	            INSERT INTO AMS.User_Role
	            VALUES(@Role, @Status)
	            SET @Return_Id = @@identity
	            SET @Msg = 'Record Inserted Successfully'

            END
            ELSE IF @Event = 'U'    --- Update
            BEGIN
	            UPDATE AMS.User_Role
	            SET [role] = @Role,
	            [Status] = @Status
	            WHERE [Role_Id] = @Id

	            SET @Msg = 'Record Updated Successfully'
            END
            ELSE IF @Event = 'D'   -- For Delete
            BEGIN
	            DELETE FROM AMS.User_Role
	            WHERE [Role_Id] = @Id
	            SET @Msg = 'Record Deleted Successfully'
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
			ALTER PROCEDURE [AMS].[Usp_IUD_UserInfo](@Event CHAR(2) ='I', @Id INT=0, @Name VARCHAR(255) =NULL, @User_Name VARCHAR(50) =NULL, @Ori_Password NVARCHAR(555) =NULL, @Password NVARCHAR(555) =NULL, @Address VARCHAR(255) =NULL, @Mobile_No VARCHAR(50) =NULL, @Tel_PhoneNo NVARCHAR(50) =NULL, @Email_Id NVARCHAR(255) =NULL, @Role_Id INT=0, @Br_Id INT=0, @Allow_Posting BIT=0, @Posting_StartDate DATETIME=NULL, @Posting_EndDate DATETIME=NULL, @Modify_StartDate DATETIME=NULL, @Modify_EndDate DATETIME=NULL, @Auditors_Lock BIT=0, @Valid_Days INT=0, @Created_By INT=0, @Created_Date DATETIME='01/01/1753', @DEFAULT_DATABASE VARCHAR(50) =NULL, @Status BIT=1, @Msg VARCHAR(MAX) OUTPUT, @Return_Id INT OUTPUT)
            AS
            IF @Event='I' --for Insert
            BEGIN
                INSERT INTO AMS.UserInfo(Full_Name, User_Name, Password, Address, Mobile_No, Phone_No, Email_Id, Role_Id, Branch_Id, Allow_Posting, Posting_StartDate, Posting_EndDate, Modify_StartDate, Modify_EndDate, Auditors_Lock, Valid_Days, Created_By, Created_Date, Status)
                VALUES(@Name, @User_Name, @Password, @Address, @Mobile_No, @Tel_PhoneNo, @Email_Id, @Role_Id, @Br_Id, @Allow_Posting, @Posting_StartDate, @Posting_EndDate, @Modify_StartDate, @Modify_EndDate, @Auditors_Lock, @Valid_Days, @Created_By, @Created_Date, @Status);
                SET @Return_Id=@@identity;
                SET @Msg='RECORD INSERTED SUCCESSFULLY..!!';
            END;
            ELSE IF @Event='U' --- Update
            BEGIN
                     UPDATE AMS.UserInfo
                     SET [Full_Name]=@Name, [Address]=@Address, [Mobile_No]=@Mobile_No, [Phone_No]=@Tel_PhoneNo, [Email_Id]=@Email_Id, [Role_Id]=@Role_Id, [Branch_Id]=@Br_Id, [Allow_Posting]=@Allow_Posting, [Posting_StartDate]=@Posting_StartDate, [Posting_EndDate]=@Posting_EndDate, [Modify_StartDate]=@Modify_StartDate, [Modify_EndDate]=@Modify_EndDate, [Auditors_Lock]=@Auditors_Lock, [Valid_Days]=@Valid_Days, [Status]=@Status
                     WHERE [User_Id]=@Id;
                     SET @Msg='RECORD UPDATED SUCCESSFULLY..!!';
            END;
            ELSE IF @Event='D' -- For Delete
            BEGIN
                     DELETE FROM AMS.UserInfo WHERE [User_Id]=@Id;
                     SET @Msg='RECORD DELETED SUCCESSFULLY..!!';
            END;
            IF @Event<>'D' BEGIN
                EXEC('CREATE LOGIN  ['+@User_Name+'] WITH PASSWORD='''+@Ori_Password+''' , DEFAULT_DATABASE='+@DEFAULT_DATABASE+', DEFAULT_LANGUAGE=[English] ');
                EXEC('ALTER SERVER ROLE [sysadmin] ADD MEMBER ['+@User_Name+']');
            END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
				ALTER PROCEDURE [AMS].[Usp_MSelect_Data]
				(
				@Event int,
				@Code1 varchar(255),
				@Code2 varchar(255)='',
				@Date datetime
				)
				as
				IF @Event=1
				BEGIN
				CREATE TABLE #TEMP1(
				[Id] [int]  NULL,
				[Menu_Id] [int] NULL,
				Menu_Name VARCHAR(255),
				[SubModule_Id] [int] NULL,
				[New] [bit] NULL,
				[Save] [bit] NULL,
				[Update] [bit] NULL,
				[Copy] [bit] NULL,
				[Delete] [bit] NULL,
				[Search] [bit] NULL,
				[Print] [bit] NULL,
				[Approved] [bit] NULL,
				[Reverse] [bit] NULL,
				[Parent] char(1) NULL
				)
				INSERT INTO #TEMP1
				SELECT isnull(MR.MR_Id,0),M.Menu_Id,M.Menu_Name,@Code1 SubModule_Id,isnull(MR.New,0),isnull(MR.[Save],0),isnull(MR.[Update],0),isnull(MR.Copy,0),isnull(MR.[Delete],0),isnull(MR.[Search],0),isnull(MR.[Print],0),isnull(MR.[Approved],0),isnull(MR.[Reverse],0),'C' from AMS.Menu as M LEFT OUTER JOIN
				AMS.Menu_Rights AS MR ON MR.Menu_Id=M.Menu_Id and MR.Role_Id=@Code2 where M.Mast_Menu_Id=@Code1  and M.Menu_Id NOT in (SELECT Mast_Menu_Id FROM AMS.Menu)
				declare @Menu_Id int
				declare @Menu_Name varchar(255)
				DECLARE cM2 INSENSITIVE CURSOR FOR
				select Menu_Id,Menu_Name from AMS.Menu where Mast_Menu_Id=@Code1 and Menu_Id  in (SELECT Mast_Menu_Id FROM AMS.Menu)
				OPEN cM2
				FETCH NEXT FROM cM2
				INTO @Menu_Id,@Menu_Name
				WHILE @@FETCH_STATUS = 0
				BEGIN
				INSERT INTO #TEMP1
				SELECT   0 Id,@Menu_Id Menu_Id,@Menu_Name Menu_Name,@Code1 SubModule_Id,convert(bit, 0)  New,convert(bit, 0)   [Save],convert(bit, 0)   [Update],convert(bit, 0)   Copy,convert(bit, 0)   [Delete],convert(bit, 0)   Search,convert(bit, 0)   [Print],convert(bit, 0)   Approved,convert(bit, 0)   Reverse ,'P'
				Union all
				SELECT isnull(MR.MR_Id,0),M.Menu_Id,M.Menu_Name,@Menu_Id SubModule_Id,isnull(MR.New,0),isnull(MR.[Save],0),isnull(MR.[Update],0),isnull(MR.Copy,0),isnull(MR.[Delete],0),isnull(MR.[Search],0),isnull(MR.[Print],0),isnull(MR.[Approved],0),isnull(MR.[Reverse],0),'C' from AMS.Menu as M LEFT OUTER JOIN
				AMS.Menu_Rights AS MR ON MR.Menu_Id=M.Menu_Id  and MR.Role_Id=@Code2  WHERE  M.Mast_Menu_Id=@Menu_Id
				FETCH NEXT FROM cM2
				INTO  @Menu_Id,@Menu_Name
				END
				DEALLOCATE cM2
				SELECT *FROM #TEMP1
				DROP TABLE #TEMP1
				END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                IF COL_LENGTH('AMS.SoftwareRegistration', 'System_Id') IS NULL BEGIN
                    ALTER TABLE AMS.SoftwareRegistration
                    ADD [Module] [NVARCHAR](50), [System_Id] [NVARCHAR](500);
                END;
                IF COL_LENGTH('AMS.SoftwareRegistration', 'ActivationCode') IS NULL BEGIN
                    ALTER TABLE AMS.SoftwareRegistration ADD [ActivationCode] [NVARCHAR](500);
                END;
                IF COL_LENGTH('AMS.SoftwareRegistration', 'Server_MacAdd') IS NULL BEGIN
                    ALTER TABLE AMS.SoftwareRegistration ADD [Server_MacAdd] [NVARCHAR](500);
                END;
                IF COL_LENGTH('AMS.SoftwareRegistration', 'Server_Desc') IS NULL BEGIN
                    ALTER TABLE AMS.SoftwareRegistration ADD [Server_Desc] [NVARCHAR](50);
                END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                IF COL_LENGTH('AMS.UserInfo', 'Category') IS NULL BEGIN
                    ALTER TABLE AMS.UserInfo ADD [Category] [NVARCHAR](50);
                END;
                IF COL_LENGTH('AMS.UserInfo', 'Authorized') IS NULL BEGIN
                    ALTER TABLE AMS.UserInfo ADD [Authorized] [BIT];
                END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                IF COL_LENGTH('AMS.SoftwareRegistration', 'IsOnline') IS NULL BEGIN
                   ALTER TABLE AMS.SoftwareRegistration ADD IsOnline [bit];
                END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='P' AND name='Usp_IUD_AccTransaction')BEGIN
                    EXEC('CREATE PROC AMS.Usp_IUD_AccTransaction AS SELECT @@servername;');
                END;";
            result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }
    }

    #endregion *--------------- Master Table ---------------*

    // DATA BASE TRIGGER

    #region *--------------- Database Trigger Trigger Table ---------------*

    public static void CreateTrigger()
    {
        if (!ObjGlobal.IsIrdApproved.Equals("YES")) return;
        const string cmdString = @"
			DECLARE @SQL NVARCHAR(MAX);
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Master] ON AMS.SB_Master FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT TOP 1 name FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_UPDATE_FROM_SB_Master')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Master] ON AMS.SB_Master FOR DELETE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT TOP 1 name FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_DELETE_FROM_SB_Master')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Details] ON AMS.SB_Details FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT TOP 1 name FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_UPDATE_FROM_SB_Details')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Details] ON AMS.SB_Details FOR DELETE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_DELETE_FROM_SB_Details')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Term] ON AMS.SB_Term FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_UPDATE_FROM_SB_Term')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Term] ON AMS.SB_Term FOR DELETE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_DELETE_FROM_SB_Term')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Master] ON AMS.SR_Master FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_UPDATE_FROM_SR_Master')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Master] ON AMS.SR_Master FOR DELETE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_DELETE_FROM_SR_Master')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Details] ON AMS.SR_Details FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_UPDATE_FROM_SR_Details')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Details] ON AMS.SR_Details FOR DELETE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_DELETE_FROM_SR_Details')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Term] ON AMS.SR_Term FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_UPDATE_FROM_SR_Term')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;
            SET @SQL='CREATE TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Term] ON AMS.SR_Term FOR DELETE AS BEGIN BEGIN ROLLBACK END END;';
            IF NOT EXISTS (SELECT * FROM sys.objects WHERE type='TR' AND name='TR_PREVENT_DELETE_FROM_SR_Term')BEGIN
                EXEC sys.sp_executesql @SQL;
            END;";
        var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
    }

    public static void DropTrigger()
    {
        try
        {
            const string cmdString = @"
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'PREVENT_UPDATE_SB_Master')BEGIN
                    DROP TRIGGER [AMS].[PREVENT_UPDATE_SB_Master];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'PREVENT_UPDATE_SB_Details')BEGIN
                    DROP TRIGGER [AMS].PREVENT_UPDATE_SB_Details;
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'PREVENT_UPDATE_SB_Term')BEGIN
                    DROP TRIGGER [AMS].PREVENT_UPDATE_SB_Term;
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SB_Master')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Master];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SB_Master')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Master];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SB_Details')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Details];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SB_Details')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Details];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SB_Term')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Term];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SB_Term')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Term];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SR_Master')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Master];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SR_Master')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Master];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SR_Details')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Details];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SR_Details')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Details];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SR_Term')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Term];
                END;
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SR_Term')BEGIN
                    DROP TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Term];
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            //
        }
    }

    public static void AlterDatabaseTrigger()
    {
        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Master] ON [AMS].[SB_Master] FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;	";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Master] ON [AMS].[SB_Master] FOR DELETE AS BEGIN BEGIN ROLLBACK END END;	";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Details] ON [AMS].[SB_Details] FOR UPDATE AS BEGIN BEGIN ROLLBACK END END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Details] ON [AMS].[SB_Details] FOR DELETE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SB_Term] ON [AMS].[SB_Term] FOR UPDATE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SB_Term] ON [AMS].[SB_Term] FOR DELETE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ~ignored~
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Master] ON [AMS].[SR_Master] FOR UPDATE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Master] ON [AMS].[SR_Master] FOR DELETE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ~ignored~
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Details] ON [AMS].[SR_Details] FOR UPDATE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Details] ON [AMS].[SR_Details] FOR DELETE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ~ignored~
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_UPDATE_FROM_SR_Term] ON [AMS].[SR_Term] FOR UPDATE AS BEGIN BEGIN ROLLBACK END END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ~ignored~
        }

        try
        {
            const string cmdString = @"
                ALTER TRIGGER [AMS].[TR_PREVENT_DELETE_FROM_SR_Term] ON [AMS].[SR_Term] FOR DELETE AS BEGIN BEGIN ROLLBACK END END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            // ~~~~ignored~~~~
        }
    }

    public static void DisableTrigger()
    {
        try
        {
            const string cmdString = @"
                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SB_Master')
	                DISABLE TRIGGER TR_PREVENT_UPDATE_FROM_SB_Master ON AMS.SB_Master;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SB_Master')
	                DISABLE TRIGGER TR_PREVENT_DELETE_FROM_SB_Master ON AMS.SB_Master;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SB_Details')
	                DISABLE TRIGGER TR_PREVENT_UPDATE_FROM_SB_Details ON AMS.SB_Details;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SB_Details')
	                DISABLE TRIGGER TR_PREVENT_DELETE_FROM_SB_Details ON AMS.SB_Details;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SB_Term')
                    DISABLE TRIGGER TR_PREVENT_UPDATE_FROM_SB_Term ON AMS.SB_Term;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SB_Term')
	                DISABLE TRIGGER TR_PREVENT_DELETE_FROM_SB_Term ON AMS.SB_Term;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SR_Master')
	                DISABLE TRIGGER TR_PREVENT_UPDATE_FROM_SR_Master ON AMS.SR_Master;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SR_Master')
	                DISABLE TRIGGER TR_PREVENT_DELETE_FROM_SR_Master ON AMS.SR_Master;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SR_Details')
	                DISABLE TRIGGER TR_PREVENT_DELETE_FROM_SR_Details ON AMS.SR_Details;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SR_Details')
	                DISABLE TRIGGER TR_PREVENT_UPDATE_FROM_SR_Details ON AMS.SR_Details;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_DELETE_FROM_SR_Term')
	                DISABLE TRIGGER TR_PREVENT_DELETE_FROM_SR_Term ON AMS.SR_Term;

                IF EXISTS (SELECT name FROM sys.objects WHERE [type]='TR' AND [name]=N'TR_PREVENT_UPDATE_FROM_SR_Term')
	                DISABLE TRIGGER TR_PREVENT_UPDATE_FROM_SR_Term ON AMS.SR_Term; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception)
        {
            //
        }
    }

    #endregion *--------------- Database Trigger Trigger Table ---------------*
}