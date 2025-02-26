using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Properties;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MrDAL.Utility.dbMaster;

public class AlterDatabaseTable
{
    // CREATE DATABASE TABLE AND ALTER TABLE

    #region *--------------- Alter Database Table ---------------*

    protected AlterDatabaseTable()
    {

    }

    private static void CreateProcedure()
    {
        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE name='FifoValueCalculation' AND type in (N'P', N'PC')) BEGIN
                    EXEC(' Create Proc AMS.FifoValueCalculation as select @@servername ;');
                END;
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE name='StockValueCalculation' AND type in (N'P', N'PC')) BEGIN
                    EXEC(' Create Proc AMS.StockValueCalculation as select @@servername ;');
                END;
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE name='USP_PostStockValue' AND type in (N'P', N'PC')) BEGIN
                    EXEC('Create Proc AMS.USP_PostStockValue   as select @@servername ;');
                END;";
            var exception = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            if (exception <= 0)
            {
            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(new { Class = "ClsCreateDatabaseTable", Method = "CreateProcedure" });
        }
    }

    private static void CreateDatabaseFunction()
    {
        // FN_SPLIT
        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='Fn_Split' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[Fn_Split](@text VARCHAR(8000), @delimiter VARCHAR(20) ='''')
	                RETURNS @Strings TABLE(position INT IDENTITY PRIMARY KEY, value VARCHAR(8000))
	                AS BEGIN
		                DECLARE @index INT;
		                SET @index=-1;
		                WHILE(LEN(@text)>0)BEGIN
			                SET @index=CHARINDEX(@delimiter, @text);
			                IF(@index=0)AND(LEN(@text)>0)BEGIN
				                INSERT INTO @Strings VALUES(@text);
				                BREAK;
			                END;
			                IF(@index>1)BEGIN
				                INSERT INTO @Strings VALUES(LEFT(@text, @index-1));
				                SET @text=RIGHT(@text, (LEN(@text)-@index));
			                END;
			                ELSE SET @text=RIGHT(@text, (LEN(@text)-@index));
		                END;
		                RETURN;
	                END;');
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='Fn_SplitString' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[Fn_SplitString](@RowData NVARCHAR(2000), @SplitOn NVARCHAR(5))
	                RETURNS @ReturnTable TABLE(Id INT, Value VARCHAR(1024))
	                AS BEGIN
		                DECLARE @i INT;
		                SET @i=0;
		                WHILE @RowData<>'''' BEGIN
			                SET @i=@i+1;
			                IF CHARINDEX(@SplitOn, @RowData)>0 BEGIN
				                INSERT INTO @ReturnTable(Id, Value)
				                VALUES(@i, SUBSTRING(@RowData, 1, CHARINDEX(@SplitOn, @RowData)-1));
				                SET @RowData=SUBSTRING(@RowData, CHARINDEX(@SplitOn, @RowData)+1, LEN(@RowData));
			                END;
			                ELSE BEGIN
				                INSERT INTO @ReturnTable(Id, Value)VALUES(@i, @RowData);
				                SET @RowData='''';
			                END;
		                END;
		                RETURN;
	                END;');
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='Split' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[Split](@DataStr VARCHAR(8000), @Delimiter CHAR(1), @Count INT)
	                RETURNS @TemporaryTable TABLE(Items VARCHAR(8000))
	                AS BEGIN
		                DECLARE @Index INT;
		                DECLARE @Slice VARCHAR(8000);
		                DECLARE @I INT;
		                SET @I=1;
		                SELECT @Index=1;
		                IF LEN(@DataStr)<1 OR @DataStr IS NULL RETURN;
		                WHILE @Index !=0 BEGIN
			                SET @Index=CHARINDEX(@Delimiter, @DataStr);
			                IF @Index !=0 SET @Slice=LEFT(@DataStr, @Index-1);
			                ELSE SET @Slice=@DataStr;
			                IF(LEN(@Slice)>0)BEGIN
				                IF(@Count=@I)BEGIN
					                INSERT INTO @TemporaryTable(Items)VALUES(@Slice);
				                END;
			                END;
			                SET @I=@I+1;
			                SET @DataStr=RIGHT(@DataStr, LEN(@DataStr)-@Index);
			                IF LEN(@DataStr)=0 BREAK;
		                END;
		                RETURN;
	                END;');
                END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='FnSplitDepartment' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT')) BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[FnSplitDepartment](@Class_Code1 VARCHAR(50), @Class_Code2 VARCHAR(50))
	                RETURNS VARCHAR(50)
	                AS BEGIN
		                DECLARE @Class_Code3 VARCHAR(50);
		                SET @Class_Code3='''';
		                DECLARE @Value VARCHAR(50);
		                DECLARE @Value1 VARCHAR(50);
		                DECLARE c0 CURSOR FOR SELECT Value FROM fn_Split(@Class_Code2, '','');
		                OPEN c0;
		                FETCH NEXT FROM c0
		                INTO @Value;
		                WHILE @@FETCH_STATUS=0 BEGIN
			                DECLARE c1 CURSOR FOR SELECT Value FROM fn_Split(@Class_Code1, '','');
			                OPEN c1;
			                FETCH NEXT FROM c1
			                INTO @Value1;
			                WHILE @@FETCH_STATUS=0 BEGIN
				                IF @Value1=@Value BEGIN
					                SET @Class_Code3=@Class_Code3+@Value1+'','';
				                END;
				                FETCH NEXT FROM c1
				                INTO @Value1;
			                END;
			                CLOSE c1;
			                DEALLOCATE c1;
			                FETCH NEXT FROM c0
			                INTO @Value;
		                END;
		                CLOSE c0;
		                DEALLOCATE c0;
		                SET @Class_Code3=(SELECT SUBSTRING(@Class_Code3, 0, LEN(@Class_Code3)));
		                RETURN @Class_Code3;
	                END;');
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='GetNumericValue' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[GetNumericValue](@strAlphaNumeric VARCHAR(256))
	                RETURNS VARCHAR(256)
	                AS BEGIN
		                DECLARE @intAlpha INT;
		                SET @intAlpha=PATINDEX(''%[^0-9]%'', @strAlphaNumeric);
		                BEGIN
			                WHILE @intAlpha>0 BEGIN
				                SET @strAlphaNumeric=STUFF(@strAlphaNumeric, @intAlpha, 1, '''');
				                SET @intAlpha=PATINDEX(''%[^0-9]%'', @strAlphaNumeric);
			                END;
		                END;
		                RETURN ISNULL(@strAlphaNumeric, 0);
	                END;');
                END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='ReturnNumeric' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[ReturnNumeric](@strAlphaNumeric VARCHAR(256))
	                RETURNS VARCHAR(256)
	                AS BEGIN
		                DECLARE @intAlpha INT;
		                SET @intAlpha=PATINDEX(''%[^0-9]%'', @strAlphaNumeric);
		                BEGIN
			                WHILE @intAlpha>0 BEGIN
				                SET @strAlphaNumeric=STUFF(@strAlphaNumeric, @intAlpha, 1, '''');
				                SET @intAlpha=PATINDEX(''%[^0-9]%'', @strAlphaNumeric);
			                END;
		                END;
		                RETURN ISNULL(@strAlphaNumeric, 0);
	                END;');
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='ReturnNepaliDate' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[ReturnNepaliDate](@Date NVARCHAR(20))
	                RETURNS NVARCHAR(20)
	                AS BEGIN
		                SET @Date=REPLACE(@Date, ''-'', ''/'');
		                DECLARE @AD_Date NVARCHAR(32);
		                DECLARE @Return NVARCHAR(20);
		                DECLARE @Day INT;
		                DECLARE @Month INT;
		                DECLARE @Year INT;
		                SET @Month=(SELECT TOP(1)* FROM AMS.Split(@Date, ''/'', 1) );
		                IF(@Month<13)BEGIN
			                SET @Day=(SELECT TOP(1)* FROM AMS.Split(@Date, ''/'', 2) );
			                SET @Year=(SELECT TOP(1)* FROM AMS.Split(@Date, ''/'', 3) );
		                END;
		                ELSE BEGIN
			                SET @Year=(SELECT TOP(1)* FROM AMS.Split(@Date, ''/'', 1) );
			                SET @Month=(SELECT TOP(1)* FROM AMS.Split(@Date, ''/'', 2) );
			                SET @Day=(SELECT TOP(1)* FROM AMS.Split(@Date, ''/'', 3) );
		                END;
		                SET @AD_Date=CAST(CAST(@Month AS INT) AS VARCHAR(20))+N''/''+CAST(CAST(@Day AS INT) AS VARCHAR(20))+N''/''+CAST(CAST(@Year AS INT) AS VARCHAR(20));
		                SET @Return=(SELECT BS_DateDMY FROM AMS.DateMiti WHERE AD_Date=@AD_Date);
		                RETURN @Return;
	                END;');
                END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='SystemDate' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('
	                CREATE FUNCTION [AMS].[SystemDate](@Date1 AS DATETIME)
	                RETURNS VARCHAR(10)
	                AS BEGIN
		                DECLARE @strymd AS VARCHAR(10);
		                SET @strymd=CAST(YEAR(@Date1) AS VARCHAR(4))+''-''+RIGHT(''0''+CAST(MONTH(@Date1) AS VARCHAR), 2)+''-''+RIGHT(''0''+CAST(DAY(@Date1) AS VARCHAR), 2);
		                RETURN (@strymd)
	                END;');
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }

        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE name='ProperCase' AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))BEGIN
                    EXEC('CREATE FUNCTION AMS.ProperCase(@Text AS NVARCHAR(MAX))
			                RETURNS NVARCHAR(MAX)
			                AS BEGIN
				                DECLARE @Reset BIT;
				                DECLARE @Ret VARCHAR(8000);
				                DECLARE @i INT;
				                DECLARE @c CHAR(1);
				                IF @Text IS NULL RETURN NULL;
				                SELECT @Reset=1, @i=1, @Ret='''';
				                WHILE(@i<=LEN(@Text))
				                SELECT @c=SUBSTRING(@Text, @i, 1), @Ret=@Ret+CASE WHEN @Reset=1 THEN UPPER(@c)ELSE LOWER(@c)END, @Reset=CASE WHEN @c LIKE ''[a-zA-Z]'' THEN 0 ELSE 1 END, @i  =@i+1;
				                RETURN @Ret;
			                END;');
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }
    }

    public static bool CreateDatabaseTableColumn()
    {
        #region *--------------- AlterTable *---------------

        try
        {
            var scheme = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.CreateSchemaInDatabase);
            if (scheme <= 0)
            {
            }

            var table = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.CreateDatabaseTableQuery);
            if (table <= 0)
            {
            }

            var alterColumn = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.AlterColumnType);
            if (alterColumn <= 0)
            {
            }

            var alterTable = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.AlterDatabaseTableQuery);
            if (alterTable <= 0)
            {
            }

            var auditLog = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.AuditLogTableQuery);
            if (auditLog <= 0)
            {
            }

            var addDesign = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.DefaultDesign);
            if (addDesign <= 0)
            {
            }

            var view = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.CreateViewTableQuery);
            if (view <= 0)
            {
            }

            var alterView = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.AlterViewTableQuery);
            if (alterView <= 0)
            {
            }

            var addModule = SqlExtensions.ExecuteNonQueryIgnoreException(Resources.AddModuleDescription);
            if (addModule <= 0)
            {
            }

            CreateDatabaseFunction();
            CreateProcedure();
            AlterDatabaseView();
            AlterDatabaseProc();
            AlterDatabaseTableQuery();
            AlterDatabaseIndexQuery();
        }
        catch
        {
            // ignored
        }

        try
        {
            var masterMiti = SqlExtensions.ExecuteDataSet("SELECT * FROM AMS.DateMiti dm").Tables[0];
            if (masterMiti?.Rows.Count == 0)
            {
                SqlExtensions.ExecuteNonQueryIgnoreException(Resources.DefaultDateMiti);
            }
            var table = SqlExtensions.ExecuteDataSet("SELECT * FROM AMS.DateMiti dm").Tables[0];
            if (table.Rows.Count is 0)
            {
                const string cmdString = @"
                INSERT INTO ams.DateMiti (Date_Id, BS_Date, BS_DateDMY, AD_Date, BS_Months, AD_Months, Days, Is_Holiday, Holiday)
                SELECT Date_Id, BS_Date, BS_DateDMY, AD_Date, BS_Months, AD_Months, Days, Is_Holiday, Holiday FROM MASTER.AMS.DateMiti dm
                WHERE dm.Date_Id NOT IN (SELECT dm.Date_Id FROM AMS.DateMiti)";
                SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        try
        {
            if (ObjGlobal.SoftwareModule is "POS")
            {
                const string cmdString = @"
					DELETE AMS.BarcodeList WHERE Barcode IN (SELECT CAST(p.PID AS NVARCHAR)  FROM AMS.Product p);
                    DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode  FROM AMS.Product p);
                    DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.PShortName  FROM AMS.Product p);
                    DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode1  FROM AMS.Product p);
                    DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode2  FROM AMS.Product p);
                    DELETE AMS.BarcodeList WHERE Barcode IN (SELECT p.Barcode3  FROM AMS.Product p);

                    INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
                    SELECT p.PID,p.PShortName,p.PSalesRate,p.PMRP,p.TradeRate,0 Wholesale,0 Retail,0 Dealer,0 Resellar,p.PUnit,p.PAltUnit,0
                    FROM   AMS.Product p
                    WHERE PShortName NOT IN (SELECT Barcode	FROM AMS.BarcodeList bl WHERE  bl.ProductId = p.PID ) AND PShortName  IS NOT NULL  AND p.PShortName <> ''

                    INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
                    SELECT p.PID,p.Barcode,p.PSalesRate,p.PMRP,p.TradeRate,0 Wholesale,0 Retail,0 Dealer,0 Resellar,p.PUnit,p.PAltUnit,0
                    FROM   AMS.Product p WHERE p.Barcode NOT IN (SELECT Barcode FROM AMS.BarcodeList bl WHERE  bl.ProductId = p.PID ) AND p.Barcode  IS NOT NULL  AND p.Barcode <> ''

                    INSERT INTO AMS.BarcodeList(ProductId, Barcode, SalesRate, MRP, Trade, Wholesale, Retail, Dealer, Resellar, UnitId, AltUnitId, DailyRateChange)
                    SELECT p.PID,p.Barcode1,p.PSalesRate,p.PMRP,p.TradeRate,0 Wholesale,0 Retail,0 Dealer,0 Resellar,p.PUnit,p.PAltUnit,0
                    FROM   AMS.Product p WHERE p.Barcode1 NOT IN (SELECT bl.Barcode FROM AMS.BarcodeList bl WHERE  bl.ProductId = p.PID ) AND p.Barcode1  IS NOT NULL  AND p.Barcode1 <> ''  ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        try
        {
            var insertDocument = InsertDocumentNumbering();
            if (insertDocument != 0)
            {
            }
        }
        catch
        {
            // ignored
        }

        return true;

        #endregion *--------------- AlterTable *---------------
    }

    public static void AlterCompanyInfoTableQuery()
    {
        var cmdString = "IF COL_LENGTH('AMS.CompanyInfo', 'PrintDesc') IS NULL BEGIN\r\n    ALTER TABLE AMS.CompanyInfo ADD PrintDesc NVARCHAR(200) NULL;\r\nEND;\r\nIF COL_LENGTH('AMS.CompanyInfo', 'IsSyncOnline') IS NULL BEGIN\r\n    ALTER TABLE AMS.CompanyInfo ADD IsSyncOnline BIT NULL;\r\nEND;" +
                        "IF COL_LENGTH('AMS.CompanyInfo', 'ApiKey') IS NULL BEGIN\r\n    ALTER TABLE AMS.CompanyInfo ADD ApiKey UNIQUEIDENTIFIER NULL;\r\nEND;";
        var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
    }

    public static int InsertDocumentNumbering()
    {
        try
        {
            string cmdString;
            var fiscalYearId = "SELECT DefaultFiscalYearId FROM AMS.SystemSetting".GetQueryData().GetInt();
            if (fiscalYearId is 0) return 0;
            var fiscalYear = GetConnection.GetQueryData(
                $"SELECT RIGHT(fy.StartBSDate,2) + '-'+ RIGHT(fy.EndBSDate,2) FROM master.AMS.FiscalYear fy WHERE fy.FiscalYearId ='{fiscalYearId}'");
            var year = GetConnection.GetQueryData(
                $"SELECT RIGHT(fy.StartBSDate,4) FROM MASTER.AMS.FiscalYear fy WHERE fy.FiscalYearId ='{fiscalYearId}' ");
            var cmd = $"SELECT * FROM AMS.DocumentNumbering WHERE FiscalYearId = {ObjGlobal.SysFiscalYearId}";
            var dt = SqlExtensions.ExecuteDataSet(cmd).Tables[0];
            if (dt.Rows.Count > 0)
                cmdString = $@"
                    IF NOT EXISTS( SELECT * FROM AMS.DocumentNumbering WHERE FiscalYearId = 13)
                    BEGIN
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocUser, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT MAX(DocId)
		                        FROM AMS.DocumentNumbering)+ROW_NUMBER() OVER (ORDER BY DocId) DocId, DocModule, REPLACE(DocDesc,'{year.GetInt() - 1}','') + ' {year}' DocDesc, '{ObjGlobal.CfStartAdDate:yyyy-MM-dd}' DocStartDate, '{ObjGlobal.CfStartBsDate}' DocStartMiti, '{ObjGlobal.CfEndAdDate:yyyy-MM-dd}' DocEndDate, '{ObjGlobal.CfEndBsDate}' DocEndMiti, '{ObjGlobal.LogInUser}' DocUser, DocType, DocPrefix,'/{fiscalYear}' DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate,{fiscalYearId} FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, ISNULL(SyncRowVersion,1)
                        FROM AMS.DocumentNumbering
                        WHERE FiscalYearId={ObjGlobal.SysFiscalYearId}
                    END; ";
            else
                cmdString = $@"
                    IF NOT EXISTS (SELECT * FROM AMS.DocumentNumbering where FiscalYearId='{fiscalYearId}' ) BEGIN
                        DECLARE @FiscalYear VARCHAR(50) = '/{fiscalYear}';
                        DECLARE @FiscalYearId INT = {fiscalYearId};
                        DECLARE @DocStartDate VARCHAR(10) = '{ObjGlobal.CfStartAdDate:yyyy-MM-dd}';
                        DECLARE @DocStartMiti VARCHAR(10) = '{ObjGlobal.CfStartBsDate}';
                        DECLARE @DocEndDate VARCHAR(10) = '{ObjGlobal.CfEndAdDate:yyyy-MM-dd}';
                        DECLARE @DocEndMiti VARCHAR(10) = '{ObjGlobal.CfEndBsDate}';
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SB', N'{ObjGlobal.LogInUser}', N'INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'ASB/',@FiscalYear, 5, 15, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'TSB', N'{ObjGlobal.LogInUser}', N'HOLD INVOICE {year}', @DocStartDate, @DocEndMiti, @DocStartDate, @DocEndMiti, N'A', N'ASB/',@FiscalYear, 5, 15, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'RSO', N'{ObjGlobal.LogInUser}', N'ORDER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'RSO/',@FiscalYear, 5, 15, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SR', N'{ObjGlobal.LogInUser}', N'RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SR/',@FiscalYear, 5, 14, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SO', N'{ObjGlobal.LogInUser}', N'ORDER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SO/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PB', N'{ObjGlobal.LogInUser}', N'INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PB/',@FiscalYear, 5, 14, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PR', N'{ObjGlobal.LogInUser}', N'RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PR/',@FiscalYear, 5, 14, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'CV', N'{ObjGlobal.LogInUser}', N'VOUCHER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'CV/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'JV', N'{ObjGlobal.LogInUser}', N'VOUCHER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'JV/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'DN', N'{ObjGlobal.LogInUser}', N'DEBIT NOTES {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'DN/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'CN', N'{ObjGlobal.LogInUser}', N'NOTES {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'CN/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'COA', N'{ObjGlobal.LogInUser}', N'LEDGER OPENING {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'COA/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'POP', N'{ObjGlobal.LogInUser}', N'PRODUCT OPENING {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'POP/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PDC', N'{ObjGlobal.LogInUser}', N'PDC {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PDC/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PI', N'{ObjGlobal.LogInUser}', N'INDENT {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PI/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PQ', N'{ObjGlobal.LogInUser}', N'QUOTATION {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PQ/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PO', N'{ObjGlobal.LogInUser}', N'ORDER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PO/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'GIT', N'{ObjGlobal.LogInUser}', N'TRANSIT {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'GIT/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PC', N'{ObjGlobal.LogInUser}', N'CHALLAN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PC/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PIB', N'{ObjGlobal.LogInUser}', N'INTERBRANCH INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PIB/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PQC', N'{ObjGlobal.LogInUser}', N'QUALITY CONTROL {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PQC/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PAB', N'{ObjGlobal.LogInUser}', N'ADDITIONAL INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PAB/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PBT', N'{ObjGlobal.LogInUser}', N'TRAVEL & TOUR INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PBT/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PEB', N'{ObjGlobal.LogInUser}', N'EXPIRY & BREAKAGE RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PEB/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SQ', N'{ObjGlobal.LogInUser}', N'QUOTATION {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SQ/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SOC', N'{ObjGlobal.LogInUser}', N'ORDER CANCELLATION {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SOC/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SDO', N'{ObjGlobal.LogInUser}', N'DISPATCH ORDER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SDO/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SDOC', N'{ObjGlobal.LogInUser}', N'DISPATCH ORDER CANCELLATION {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SDOC/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SC', N'{ObjGlobal.LogInUser}', N'CHALLAN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SC/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SIB', N'{ObjGlobal.LogInUser}', N'INTER BRANCH {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SIB/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SAB', N'{ObjGlobal.LogInUser}', N'ADDITIONAL INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SAB/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'POS', N'{ObjGlobal.LogInUser}', N'POS BILLING {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'POS/',@FiscalYear, 5, 15, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'RSB', N'{ObjGlobal.LogInUser}', N'RESTRO INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'RSB/',@FiscalYear, 5, 15, 1, N'0', 1, NULL, 1, 1, 99999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'ATI', N'{ObjGlobal.LogInUser}', N'ABBREVIATE TAX INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'ATI/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SEB', N'{ObjGlobal.LogInUser}', N'EXPIRY & BREAKAGE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SEB/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SBT', N'{ObjGlobal.LogInUser}', N'TRAVEL & TOUR INVOICE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SBT/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'GT', N'{ObjGlobal.LogInUser}', N'GODOWN TRANSFER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'GT/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SA', N'{ObjGlobal.LogInUser}', N'STOCK ADJUSTMENT {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SA/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'PSA', N'{ObjGlobal.LogInUser}', N'PHYSICAL STOCK {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'PSA/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'STEB', N'{ObjGlobal.LogInUser}', N'TRANSFER EXPIRY & BREAKAGE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'STEB/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'ASSM', N'{ObjGlobal.LogInUser}', N'ASSEMBLY MASTER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'ASSM/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'BOM', N'{ObjGlobal.LogInUser}', N'MEMO {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'BOM/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'SREQ', N'{ObjGlobal.LogInUser}', N'INVENTORY REQUOSITION {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'SREQ/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'MI', N'{ObjGlobal.LogInUser}', N'INVENTORY ISSUE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'MI/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'MIR', N'{ObjGlobal.LogInUser}', N'INVENTORY ISSUE RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'MIR/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'MR', N'{ObjGlobal.LogInUser}', N'INVENTORY RECEIVE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'MR/',@FiscalYear, 4, 13, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'MRR', N'{ObjGlobal.LogInUser}', N'INVENTORY RECEIVE RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'MRR/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'MBOM', N'{ObjGlobal.LogInUser}', N'MEMO MASTER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'MBOM/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'IBOM', N'{ObjGlobal.LogInUser}', N'PRODUCTION MEMO {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'IBOM/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'IREQ', N'{ObjGlobal.LogInUser}', N'PRODUCTION REQUISITION {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'IREQ/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'RMI', N'{ObjGlobal.LogInUser}', N'PRODUCTION ISSUE {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'RMI/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'RMIR', N'{ObjGlobal.LogInUser}', N'PRODUCTION ISSUE RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'RMIR/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'FGR', N'{ObjGlobal.LogInUser}', N'FINISHED GOODS RECEIVED {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'FGR/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'FGRR', N'{ObjGlobal.LogInUser}', N'FINISHED GOODS RETURN {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'FGRR/',@FiscalYear, 4, 15, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                        INSERT INTO AMS.DocumentNumbering(DocId, DocModule, DocUser, DocDesc, DocStartDate, DocStartMiti, DocEndDate, DocEndMiti, DocType, DocPrefix, DocSufix, DocBodyLength, DocTotalLength, DocBlank, DocBlankCh, DocBranch, DocUnit, DocStart, DocCurr, DocEnd, DocDesign, Status, EnterBy, EnterDate, FiscalYearId, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                        SELECT (SELECT ISNULL(MAX(dn.DocId), 0)+1 FROM AMS.DocumentNumbering dn), N'IPO', N'{ObjGlobal.LogInUser}', N'PRODUCTION ORDER {year}', @DocStartDate, @DocStartMiti, @DocStartDate, @DocEndMiti, N'A', N'IPO/',@FiscalYear, 4, 14, 1, N'0', 1, NULL, 1, 1, 9999, NULL, 1, N'{ObjGlobal.LogInUser}', GETDATE(), @FiscalYearId, NULL, NULL, NULL, NULL, NULL, 1;
                    END; ";
            var fiscalResult = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            return fiscalResult;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public static int DefaultTermValueForTrading()
    {
        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT * FROM AMS.ST_Term)BEGIN
				    INSERT INTO AMS.ST_Term(ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 10,  N'DISCOUNT','SB', 'G',  'V', '-', 'B',6, 0, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (4, 25,  N'VAT@13%','SB', 'G',  'V', '+', 'B',41, 13, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.PT_Term)BEGIN
                    INSERT INTO AMS.PT_Term(PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 5,  N'DISCOUNT', 'PB','G',  'V', '-', 'B',8, 0, 1, NULL, 1, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (2, 15,  N'VAT@13%','PB', 'G',  'V', '+', 'B',41, 13, 1, NULL, 0, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (3, 30,  N'DECLARATION FEE','PB', 'A',  'V', '+', 'B',17, 0, 1, NULL, 1, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (4, 35,  N'IMPORT DUTY','PB', 'A', 'V', '+', 'B',4,  0, 1, NULL, 1, 0, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (5, 40,  N'IMPORT VAT@13%','PB', 'A',  'V', '+', 'B', 41,0, 1, NULL, 0, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1);
                END;";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            return result;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public static int DefaultValueRestaurant()
    {
        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT * FROM AMS.Counter)BEGIN
                    INSERT INTO AMS.Counter(CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 'COUNTER 1', 'COUNTER 1', 1, NULL, 1, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL, 1);
                END;
				IF NOT EXISTS (SELECT * FROM AMS.Floor)BEGIN
                    INSERT INTO AMS.Floor(FloorId, Description, ShortName, Type, EnterBy, EnterDate, Branch_ID, Company_Id, Status, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, N'Floor1', N'Floor1', N'1st', 'MrSolution', GETDATE(), 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, 1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.TableMaster)BEGIN
                    INSERT INTO AMS.TableMaster(TableId, TableName, TableCode, FloorId, Branch_ID, Company_Id, TableStatus,Status, EnterBy, EnterDate,TableType, IsPrePaid, Printed, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, N'TAKEAWAY', N'TAKEAWAY', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'T', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (2, N'TAKEAWAY1', N'TAKEAWAY1', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'T', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (3, N'TAKEAWAY2', N'TAKEAWAY2', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'T', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (4, N'TAKEAWAY3', N'TAKEAWAY3', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'T', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (5, N'TAKEAWAY4', N'TAKEAWAY4', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'T', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (6, N'SPLIT1', N'SPLIT1', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'S', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (7, N'SPLIT2', N'SPLIT2', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'S', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (8, N'SPLIT3', N'SPLIT3', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'S', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (9, N'SPLIT4', N'SPLIT4', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'S', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (10, N'PRE-PAID', N'PRE-PAID', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'P', 1, NULL, NULL, NULL, NULL, NULL, NULL, 1),
                        (11, N'TABLE 1', N'TABLE 1', 1, 1, NULL, N'A', 1, N'MrSolution', GETDATE(), N'N', 0, NULL, NULL, NULL, NULL, NULL, NULL, 1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.ST_Term)BEGIN
                    INSERT INTO AMS.ST_Term(ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 10,  N'DISCOUNT','SB', 'G',  'V', '-', 'P',6, 0, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (2, 15,  N'SPECIAL DISCOUNT','SB', 'G',  'V', '-', 'B',6, 0, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (3, 20,  N'SERVICE CHARGE','SB', 'G',  'V', '+', 'B',6, 10, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (4, 25,  N'VAT@13%','SB', 'G',  'V', '+', 'B',41, 13, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.PT_Term)BEGIN
                    INSERT INTO AMS.PT_Term(PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 5,  N'DISCOUNT', 'PB','G',  'V', '-', 'B',8, 0, 1, NULL, 1, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (2, 15,  N'VAT@13%','PB', 'G',  'V', '+', 'B',41, 13, 1, NULL, 0, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (3, 30,  N'DECLARATION FEE','PB', 'A',  'V', '+', 'B',17, 0, 1, NULL, 1, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (4, 35,  N'IMPORT DUTY','PB', 'A', 'V', '+', 'B',4,  0, 1, NULL, 1, 0, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1),
                        (5, 40,  N'IMPORT VAT@13%','PB', 'A',  'V', '+', 'B', 41,0, 1, NULL, 0, 1, 1, N'MrSolution', GETDATE(),NULL,NULL,NULL,NULL,NULL,1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.ProductGroup)BEGIN
                    INSERT INTO AMS.ProductGroup(PGrpId, NepaliDesc, GrpName, GrpCode, GMargin, Gprinter, Branch_ID, Company_Id, Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, PurchaseLedgerId, PurchaseReturnLedgerId, SalesLedgerId, SalesReturnLedgerId, OpeningStockLedgerId, ClosingStockLedgerId, StockInHandLedgerId)
                    VALUES(1, N'KOT', N'KOT', N'KOT', 0, NULL, 1, NULL, 1, 'MrSolution', GETDATE(),NULL, NULL, NULL, GETDATE(), GETDATE(), 1,NULL, NULL,NULL, NULL, NULL, NULL, NULL ),
                        (2, N'BOT', N'BOT', N'KOT', 0, NULL, 1, NULL, 1, 'MrSolution', GETDATE(),NULL, NULL, NULL, GETDATE(), GETDATE(), 1,NULL, NULL,NULL, NULL, NULL, NULL, NULL ),
                        (3, N'COT', N'COT', N'KOT', 0, NULL, 1, NULL, 1, 'MrSolution', GETDATE(),NULL, NULL, NULL, GETDATE(), GETDATE(), 1,NULL, NULL,NULL, NULL, NULL, NULL, NULL );
                END;";
            return SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public static int DefaultValuePointOfSales()
    {
        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT * FROM AMS.Counter)BEGIN
                    INSERT INTO AMS.Counter(CId, CName, CCode, Branch_ID, Company_Id, Status, EnterBy, EnterDate, Printer, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 'COUNTER 1', 'COUNTER 1', 1, NULL, 1, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, NULL, 1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.ST_Term)BEGIN
                    INSERT INTO AMS.ST_Term(ST_ID, Order_No, ST_Name, Module, ST_Type, ST_Basis, ST_Sign, ST_Condition, Ledger, ST_Rate, ST_Branch, ST_CompanyUnit, ST_Profitability, ST_Supess, ST_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 10, N'DISCOUNT', 'SB', 'G', 'V', '-', 'P', 6, 0, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1),
                        (2, 15, N'SPECIAL DISCOUNT', 'SB', 'G', 'V', '-', 'B', 6, 0, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1),
                        (4, 25, N'VAT@13%', 'SB', 'G', 'V', '+', 'B', 41, 13, 1, NULL, 1, 1, 1, 'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1);
                END;
                IF NOT EXISTS (SELECT * FROM AMS.PT_Term)BEGIN
                    INSERT INTO AMS.PT_Term(PT_Id, Order_No, PT_Name, Module, PT_Type, PT_Basis, PT_Sign, PT_Condition, Ledger, PT_Rate, PT_Branch, PT_CompanyUnit, PT_Profitability, PT_Supess, PT_Status, EnterBy, EnterDate, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
                    VALUES(1, 5, N'DISCOUNT', 'PB', 'G', 'V', '-', 'B', 8, 0, 1, NULL, 1, 1, 1, N'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1),
                        (2, 15, N'VAT@13%', 'PB', 'G', 'V', '+', 'B', 41, 13, 1, NULL, 0, 1, 1, N'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1),
                        (3, 30, N'DECLARATION FEE', 'PB', 'A', 'V', '+', 'B', 17, 0, 1, NULL, 1, 1, 1, N'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1),
                        (4, 35, N'IMPORT DUTY', 'PB', 'A', 'V', '+', 'B', 4, 0, 1, NULL, 1, 0, 1, N'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1),
                        (5, 40, N'IMPORT VAT@13%', 'PB', 'A', 'V', '+', 'B', 41, 0, 1, NULL, 0, 1, 1, N'MrSolution', GETDATE(), NULL, NULL, NULL, NULL, NULL, 1);
                END;";
            return SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            return 0;
        }
    }

    public static void UpdateSystemConfiguration(string module)
    {
        var cmdTxt = string.Empty;
        try
        {
            if (ObjGlobal.SysFiscalYearId is 0)
            {
                const string cmdString = @"
                SELECT TOP 1 FY_Id FROM AMS.FiscalYear WHERE GETDATE() BETWEEN Start_ADDate AND End_ADDate";
                var year = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                ObjGlobal.SysFiscalYearId = year.Rows[0]["FY_Id"].GetInt();
            }

            cmdTxt += module switch
            {
                "RESTRO" => $@"
                    IF EXISTS (SELECT * FROM AMS.SystemSetting) BEGIN
                        UPDATE AMS.SystemSetting SET EnglishDate=CONVERT(BIT, 'False'), AuditTrial=CONVERT(BIT, 'False'), Udf=CONVERT(BIT, 'False'), Autopoplist=CONVERT(BIT, 'False'), CurrentDate=CONVERT(BIT, 'False'), ConformSave=CONVERT(BIT, 'False'), ConformCancel=CONVERT(BIT, 'False'), ConformExits=CONVERT(BIT, 'False'), CurrencyRate=1, CurrencyId=1, DefaultPrinter=N'OneNote (Desktop)', AmountFormat=N'0.00', RateFormat=N'0.00', QtyFormat=N'0.00', CurrencyFormatF=N'0.00', DefaultFiscalYearId={ObjGlobal.SysFiscalYearId}, DefaultOrderPrinter=N'OneNote (Desktop)', DefaultInvoicePrinter=N'OneNote (Desktop)', DefaultOrderNumbering=NULL, DefaultInvoiceNumbering=NULL, DefaultAvtInvoiceNumbering=NULL, DefaultOrderDesign=N'DefaultOrder', IsOrderPrint=CONVERT(BIT, 'False'), DefaultInvoiceDesign=N'DefaultInvoiceWithVAT', IsInvoicePrint=CONVERT(BIT, 'False'), DefaultAvtDesign=N'DefaultInvoice', DefaultFontsName=N'Agency FB', DefaultFontsSize=17, DefaultPaperSize=NULL, DefaultReportStyle=NULL, DefaultPrintDateTime=CONVERT(BIT, 'False'), DefaultFormColor=N'ActiveBorder', DefaultTextColor=N'ActiveBorder', DebtorsGroupId=14, CreditorGroupId=13, SalaryLedgerId=79, TDSLedgerId=38, PFLedgerId=80, DefaultEmail=N'info@mrsolution.com.np', DefaultEmailPassword=N'123456789', BackupDays=0, BackupLocation=NULL;
                    END;
                    IF EXISTS (SELECT * FROM AMS.FinanceSetting) BEGIN
                        UPDATE AMS.FinanceSetting SET ProfiLossId=22, CashId=1, VATLedgerId=41, PDCBankLedgerId=77, ShortNameWisTransaction=0, WarngNegativeTransaction=0, NegativeTransaction='I', VoucherDate=0, AgentEnable=0, AgentMandetory=0, DepartmentEnable=0, DepartmentMandetory=0, RemarksEnable=0, RemarksMandetory=0, NarrationMandetory=0, CurrencyEnable=0, CurrencyMandetory=0, SubledgerEnable=0, SubledgerMandetory=0, DetailsClassEnable=0, DetailsClassMandetory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.PurchaseSetting) BEGIN
                        UPDATE AMS.PurchaseSetting SET PBLedgerId=24, PRLedgerId=25, PBVatTerm=2, PBDiscountTerm=1, PBProductDiscountTerm=NULL, PBAdditionalTerm=5, PBDateChange=0, PBCreditDays='I', PBCreditLimit='I', PBCarryRate=1, PBChangeRate=1, PBLastRate=1, POEnable=0, POMandetory=0, PCEnable=0, PCMandetory=0, PBSublegerEnable=0, PBSubledgerMandetory=0, PBAgentEnable=0, PBAgentMandetory=0, PBDepartmentEnable=0, PBDepartmentMandetory=0, PBCurrencyEnable=0, PBCurrencyMandetory=0, PBCurrencyRateChange=0, PBGodownEnable=0, PBGodownMandetory=0, PBAlternetUnitEnable=0, PBIndent=0, PBNarration=0, PBBasicAmount=0, PBRemarksEnable=0, PBRemarksMandatory=0, PBIndentMandatory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.SalesSetting) BEGIN
                        UPDATE AMS.SalesSetting SET SBLedgerId=34, SRLedgerId=35, SBVatTerm=4, SBDiscountTerm=2, SBProductDiscountTerm=1, SBAdditionalTerm=NULL, SBServiceCharge=3, SBDateChange=0, SBCreditDays='I', SBCreditLimit='I', SBCarryRate=1, SBChangeRate=1, SBLastRate=1, SBQuotationEnable=0, SBQuotationMandetory=0, SBDispatchOrderEnable=0, SBDispatchMandetory=0, SOEnable=0, SOMandetory=0, SCEnable=0, SCMandetory=0, SBSublegerEnable=0, SBSubledgerMandetory=0, SBAgentEnable=0, SBAgentMandetory=0, SBDepartmentEnable=0, SBDepartmentMandetory=0, SBCurrencyEnable=0, SBCurrencyMandetory=0, SBCurrencyRateChange=0, SBGodownEnable=0, SBGodownMandetory=0, SBAlternetUnitEnable=0, SBIndent=0, SBNarration=0, SBBasicAmount=0, SBAviableStock=0, SBReturnValue=0, PartyInfo=0, SBRemarksEnable=0, SBRemarksMandatory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.InventorySetting) BEGIN
                        UPDATE AMS.InventorySetting SET OPLedgerId=19, CSPLLedgerId=3, CSBSLedgerId=37, NegativeStock='I', AlternetUnit=0, CostCenterEnable=0, CostCenterMandetory=0, CostCenterItemEnable=0, CostCenterItemMandetory=0, ChangeUnit=0, GodownEnable=0, GodownMandetory=0, RemarksEnable=0, GodownItemEnable=0, GodownItemMandetory=0, NarrationEnable=0, ShortNameWise=0, BatchWiseQtyEnable=0, ExpiryDate=0, FreeQty=0, GroupWiseFilter=0, GodownWiseStock=0;
                    END; ",
                "POS" => $@"
                    IF EXISTS (SELECT * FROM AMS.SystemSetting) BEGIN
                        UPDATE AMS.SystemSetting SET EnglishDate=CONVERT(BIT, 'False'), AuditTrial=CONVERT(BIT, 'False'), Udf=CONVERT(BIT, 'False'), Autopoplist=CONVERT(BIT, 'False'), CurrentDate=CONVERT(BIT, 'False'), ConformSave=CONVERT(BIT, 'False'), ConformCancel=CONVERT(BIT, 'False'), ConformExits=CONVERT(BIT, 'False'), CurrencyRate=1, CurrencyId=1, DefaultPrinter=N'OneNote (Desktop)', AmountFormat=N'0.00', RateFormat=N'0.00', QtyFormat=N'0.00', CurrencyFormatF=N'0.00', DefaultFiscalYearId={ObjGlobal.SysFiscalYearId}, DefaultOrderPrinter=N'OneNote (Desktop)', DefaultInvoicePrinter=N'OneNote (Desktop)', DefaultOrderNumbering=NULL, DefaultInvoiceNumbering=NULL, DefaultAvtInvoiceNumbering=NULL, DefaultOrderDesign=N'DefaultOrder', IsOrderPrint=CONVERT(BIT, 'False'), DefaultInvoiceDesign=N'DefaultInvoiceWithVAT', IsInvoicePrint=CONVERT(BIT, 'False'), DefaultAvtDesign=N'DefaultInvoice', DefaultFontsName=N'Agency FB', DefaultFontsSize=17, DefaultPaperSize=NULL, DefaultReportStyle=NULL, DefaultPrintDateTime=CONVERT(BIT, 'False'), DefaultFormColor=N'ActiveBorder', DefaultTextColor=N'ActiveBorder', DebtorsGroupId=14, CreditorGroupId=13, SalaryLedgerId=79, TDSLedgerId=38, PFLedgerId=80, DefaultEmail=N'info@mrsolution.com.np', DefaultEmailPassword=N'123456789', BackupDays=0, BackupLocation=NULL;
                    END;
                    IF EXISTS (SELECT * FROM AMS.FinanceSetting) BEGIN
                        UPDATE AMS.FinanceSetting SET ProfiLossId=22, CashId=1, VATLedgerId=41, PDCBankLedgerId=77, ShortNameWisTransaction=0, WarngNegativeTransaction=0, NegativeTransaction='I', VoucherDate=0, AgentEnable=0, AgentMandetory=0, DepartmentEnable=0, DepartmentMandetory=0, RemarksEnable=0, RemarksMandetory=0, NarrationMandetory=0, CurrencyEnable=0, CurrencyMandetory=0, SubledgerEnable=0, SubledgerMandetory=0, DetailsClassEnable=0, DetailsClassMandetory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.PurchaseSetting) BEGIN
                        UPDATE AMS.PurchaseSetting SET PBLedgerId=24, PRLedgerId=25, PBVatTerm=2, PBDiscountTerm=1, PBProductDiscountTerm=NULL, PBAdditionalTerm=5, PBDateChange=0, PBCreditDays='I', PBCreditLimit='I', PBCarryRate=1, PBChangeRate=1, PBLastRate=1, POEnable=0, POMandetory=0, PCEnable=0, PCMandetory=0, PBSublegerEnable=0, PBSubledgerMandetory=0, PBAgentEnable=0, PBAgentMandetory=0, PBDepartmentEnable=0, PBDepartmentMandetory=0, PBCurrencyEnable=0, PBCurrencyMandetory=0, PBCurrencyRateChange=0, PBGodownEnable=0, PBGodownMandetory=0, PBAlternetUnitEnable=0, PBIndent=0, PBNarration=0, PBBasicAmount=0, PBRemarksEnable=0, PBRemarksMandatory=0, PBIndentMandatory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.SalesSetting) BEGIN
                        UPDATE AMS.SalesSetting SET SBLedgerId=34, SRLedgerId=35, SBVatTerm=4, SBDiscountTerm=2, SBProductDiscountTerm=1, SBAdditionalTerm=NULL, SBServiceCharge=NULL, SBDateChange=0, SBCreditDays='I', SBCreditLimit='I', SBCarryRate=1, SBChangeRate=1, SBLastRate=1, SBQuotationEnable=0, SBQuotationMandetory=0, SBDispatchOrderEnable=0, SBDispatchMandetory=0, SOEnable=0, SOMandetory=0, SCEnable=0, SCMandetory=0, SBSublegerEnable=0, SBSubledgerMandetory=0, SBAgentEnable=0, SBAgentMandetory=0, SBDepartmentEnable=0, SBDepartmentMandetory=0, SBCurrencyEnable=0, SBCurrencyMandetory=0, SBCurrencyRateChange=0, SBGodownEnable=0, SBGodownMandetory=0, SBAlternetUnitEnable=0, SBIndent=0, SBNarration=0, SBBasicAmount=0, SBAviableStock=0, SBReturnValue=0, PartyInfo=0, SBRemarksEnable=0, SBRemarksMandatory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.InventorySetting) BEGIN
                        UPDATE AMS.InventorySetting SET OPLedgerId=19, CSPLLedgerId=3, CSBSLedgerId=37, NegativeStock='I', AlternetUnit=0, CostCenterEnable=0, CostCenterMandetory=0, CostCenterItemEnable=0, CostCenterItemMandetory=0, ChangeUnit=0, GodownEnable=0, GodownMandetory=0, RemarksEnable=0, GodownItemEnable=0, GodownItemMandetory=0, NarrationEnable=0, ShortNameWise=0, BatchWiseQtyEnable=0, ExpiryDate=0, FreeQty=0, GroupWiseFilter=0, GodownWiseStock=0;
                    END; ",
                _ => $@"
                    IF EXISTS (SELECT * FROM AMS.SystemSetting) BEGIN
                        UPDATE AMS.SystemSetting SET EnglishDate=CONVERT(BIT, 'False'), AuditTrial=CONVERT(BIT, 'False'), Udf=CONVERT(BIT, 'False'), Autopoplist=CONVERT(BIT, 'False'), CurrentDate=CONVERT(BIT, 'False'), ConformSave=CONVERT(BIT, 'False'), ConformCancel=CONVERT(BIT, 'False'), ConformExits=CONVERT(BIT, 'False'), CurrencyRate=1, CurrencyId=1, DefaultPrinter=N'OneNote (Desktop)', AmountFormat=N'0.00', RateFormat=N'0.00', QtyFormat=N'0.00', CurrencyFormatF=N'0.00', DefaultFiscalYearId={ObjGlobal.SysFiscalYearId}, DefaultOrderPrinter=N'OneNote (Desktop)', DefaultInvoicePrinter=N'OneNote (Desktop)', DefaultOrderNumbering=NULL, DefaultInvoiceNumbering=NULL, DefaultAvtInvoiceNumbering=NULL, DefaultOrderDesign=N'DefaultOrder', IsOrderPrint=CONVERT(BIT, 'False'), DefaultInvoiceDesign=N'DefaultInvoiceWithVAT', IsInvoicePrint=CONVERT(BIT, 'False'), DefaultAvtDesign=N'DefaultInvoice', DefaultFontsName=N'Agency FB', DefaultFontsSize=17, DefaultPaperSize=NULL, DefaultReportStyle=NULL, DefaultPrintDateTime=CONVERT(BIT, 'False'), DefaultFormColor=N'ActiveBorder', DefaultTextColor=N'ActiveBorder', DebtorsGroupId=14, CreditorGroupId=13, SalaryLedgerId=79, TDSLedgerId=38, PFLedgerId=80, DefaultEmail=N'info@mrsolution.com.np', DefaultEmailPassword=N'123456789', BackupDays=0, BackupLocation=NULL;
                    END;
                    IF EXISTS (SELECT * FROM AMS.FinanceSetting) BEGIN
                        UPDATE AMS.FinanceSetting SET ProfiLossId=22, CashId=1, VATLedgerId=41, PDCBankLedgerId=77, ShortNameWisTransaction=0, WarngNegativeTransaction=0, NegativeTransaction='I', VoucherDate=0, AgentEnable=0, AgentMandetory=0, DepartmentEnable=0, DepartmentMandetory=0, RemarksEnable=0, RemarksMandetory=0, NarrationMandetory=0, CurrencyEnable=0, CurrencyMandetory=0, SubledgerEnable=0, SubledgerMandetory=0, DetailsClassEnable=0, DetailsClassMandetory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.PurchaseSetting) BEGIN
                        UPDATE AMS.PurchaseSetting SET PBLedgerId=24, PRLedgerId=25, PBVatTerm=2, PBDiscountTerm=1, PBProductDiscountTerm=NULL, PBAdditionalTerm=5, PBDateChange=0, PBCreditDays='I', PBCreditLimit='I', PBCarryRate=1, PBChangeRate=1, PBLastRate=1, POEnable=0, POMandetory=0, PCEnable=0, PCMandetory=0, PBSublegerEnable=0, PBSubledgerMandetory=0, PBAgentEnable=0, PBAgentMandetory=0, PBDepartmentEnable=0, PBDepartmentMandetory=0, PBCurrencyEnable=0, PBCurrencyMandetory=0, PBCurrencyRateChange=0, PBGodownEnable=0, PBGodownMandetory=0, PBAlternetUnitEnable=0, PBIndent=0, PBNarration=0, PBBasicAmount=0, PBRemarksEnable=0, PBRemarksMandatory=0, PBIndentMandatory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.SalesSetting) BEGIN
                        UPDATE AMS.SalesSetting SET SBLedgerId=34, SRLedgerId=35, SBVatTerm=4, SBDiscountTerm=1, SBProductDiscountTerm=NULL, SBAdditionalTerm=NULL, SBServiceCharge=NULL, SBDateChange=0, SBCreditDays='I', SBCreditLimit='I', SBCarryRate=1, SBChangeRate=1, SBLastRate=1, SBQuotationEnable=0, SBQuotationMandetory=0, SBDispatchOrderEnable=0, SBDispatchMandetory=0, SOEnable=0, SOMandetory=0, SCEnable=0, SCMandetory=0, SBSublegerEnable=0, SBSubledgerMandetory=0, SBAgentEnable=0, SBAgentMandetory=0, SBDepartmentEnable=0, SBDepartmentMandetory=0, SBCurrencyEnable=0, SBCurrencyMandetory=0, SBCurrencyRateChange=0, SBGodownEnable=0, SBGodownMandetory=0, SBAlternetUnitEnable=0, SBIndent=0, SBNarration=0, SBBasicAmount=0, SBAviableStock=0, SBReturnValue=0, PartyInfo=0, SBRemarksEnable=0, SBRemarksMandatory=0;
                    END;
                    IF EXISTS (SELECT * FROM AMS.InventorySetting) BEGIN
                        UPDATE AMS.InventorySetting SET OPLedgerId=19, CSPLLedgerId=3, CSBSLedgerId=37, NegativeStock='I', AlternetUnit=0, CostCenterEnable=0, CostCenterMandetory=0, CostCenterItemEnable=0, CostCenterItemMandetory=0, ChangeUnit=0, GodownEnable=0, GodownMandetory=0, RemarksEnable=0, GodownItemEnable=0, GodownItemMandetory=0, NarrationEnable=0, ShortNameWise=0, BatchWiseQtyEnable=0, ExpiryDate=0, FreeQty=0, GroupWiseFilter=0, GodownWiseStock=0;
                    END; "
            };
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdTxt);
            if (result <= 0)
            {

            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(new
            {
                Class = "ClsCreateDatabaseTable",
                Method = "CreateDatabaseFunction"
            });
        }
    }

    public static void SaveSystemConfiguration()
    {
        const string cmdString = @"
        IF NOT EXISTS (SELECT * FROM AMS.SystemSetting) BEGIN
            INSERT INTO AMS.SystemSetting (SyId) VALUES(1)
        END;
        IF NOT EXISTS (SELECT * FROM AMS.FinanceSetting) BEGIN
            INSERT INTO AMS.FinanceSetting (FinId) VALUES(1)
        END;
        IF NOT EXISTS (SELECT * FROM AMS.PurchaseSetting) BEGIN
            INSERT INTO AMS.PurchaseSetting (PurId) VALUES(1)
        END;
        IF NOT EXISTS (SELECT * FROM AMS.SalesSetting) BEGIN
            INSERT INTO AMS.SalesSetting (SalesId) VALUES(1)
        END;
        IF NOT EXISTS (SELECT * FROM AMS.InventorySetting) BEGIN
            INSERT INTO AMS.InventorySetting (InvId) VALUES(1)
        END;";
        var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        if (result <= 0)
        {
        }
    }

    private static void AlterDatabaseView()
    {
        // SALES TERM VIEW SETUP
        #region ---------------ST_TERM---------------

        // VIEW_SQ_TERM_PRODUCT
        #region SQ_TERM(PRODUCT AND BILL)
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='P' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SQ_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.SQ_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SQ_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SQ_Master AS SIM ON SIM.SQ_Invoice=STD.SQ_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }


        //VIEW_SQ_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='B' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SQ_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.SQ_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SQ_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SQ_Master AS SIM ON SIM.SQ_Invoice=STD.SQ_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }
        #endregion SQ_TERM(PRODUCT AND BILL)

        #region SO_TERM(PRODUCT AND BILL)
        // VIEW_SO_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='P' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SO_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.SO_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SO_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SO_Master AS SIM ON SIM.SO_Invoice=STD.SO_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }


        // VIEW_SO_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='B' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SO_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.SO_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SO_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SO_Master AS SIM ON SIM.SO_Invoice=STD.SO_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }
        #endregion SO_TERM(PRODUCT AND BILL)

        #region SC_TERM(PRODUCT AND BILL)

        // VIEW_SC_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='P' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SC_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.SC_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SC_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SC_Master AS SIM ON SIM.SC_Invoice=STD.SC_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }


        // VIEW_SC_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='B' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SC_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.SC_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SC_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SC_Master AS SIM ON SIM.SC_Invoice=STD.SC_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }
        #endregion SC_TERM(PRODUCT AND BILL)

        #region SB_TERM(PRODUCT AND BILL)

        // VIEW_SB_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='P' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SB_TERM_PRODUCT
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum}
                FROM
                       (
                         SELECT STD.SB_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SB_Term AS STD INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id INNER JOIN AMS.SB_Master AS SIM ON SIM.SB_Invoice=STD.SB_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        // VIEW_SB_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='B' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SB_TERM_BILL
                AS SELECT pid.VOUCHER_NO,{nullColum}
                FROM
                       (
                         SELECT STD.SB_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SB_Term AS STD INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id INNER JOIN AMS.SB_Master AS SIM ON SIM.SB_Invoice=STD.SB_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        #endregion SB_TERM(PRODUCT AND BILL)

        #region SR_TERM(PRODUCT AND BILL)

        // VIEW_SR_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='P' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SR_TERM_PRODUCT
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum}
                FROM
                       (
                         SELECT STD.SR_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SR_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SR_Master AS SIM ON SIM.SR_Invoice=STD.SR_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }


        // VIEW_SR_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT ST_Name FROM AMS.ST_Term WHERE ST_Condition='B' AND ST_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_SR_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.SR_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.SR_Term AS STD 
                            INNER JOIN AMS.ST_Term AS SBT ON STD.ST_Id=SBT.ST_Id 
                            INNER JOIN AMS.SR_Master AS SIM ON SIM.SR_Invoice=STD.SR_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR ST_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }
        #endregion SR_TERM(PRODUCT AND BILL)

        #endregion ---------------ST_TERM---------------


        // PURCHASE TERM VIEW SETUP
        #region ---------------PT_TERM---------------

        // ALTER PURCHASE ORDER PRODUCT WISE TERM

        #region PO_TERM(PRODUCT AND BILL)

        // VIEW_PO_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='P' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PO_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.PO_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PO_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PO_Master AS SIM ON SIM.PO_Invoice=STD.PO_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        // VIEW_PO_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='B' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PO_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.PO_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PO_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PO_Master AS SIM ON SIM.PO_Invoice=STD.PO_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }

        #endregion PO_TERM(PRODUCT AND BILL)

        // ALTER PURCHASE CHALLAN TERM  BILLING TERM

        #region PC_TERM(PRODUCT AND BILL)
        
        // VIEW_PC_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='P' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PC_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.PC_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PC_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PC_Master AS SIM ON SIM.PC_Invoice=STD.PC_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        // ALTER PURCHASE CHALLAN BILL WISE TERM
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='B' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PC_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.PC_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PC_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PC_Master AS SIM ON SIM.PC_Invoice=STD.PC_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }

        #endregion PC_TERM(PRODUCT AND BILL)

        #region PB_TERM(PRODUCT AND BILL)

        // VIEW_PB_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='P' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PB_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.PB_VNo VOUCHER_NO,STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PB_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PB_Master AS SIM ON SIM.PB_Invoice=STD.PB_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        // VIEW_PB_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='B' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PB_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.PB_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PB_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PB_Master AS SIM ON SIM.PB_Invoice=STD.PB_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }

        #endregion PB_TERM(PRODUCT AND BILL)

        // ALTER PURCHASE RETURN PRODUCT WISE TERM

        #region PR_TERM(PRODUCT AND BILL)

        // VIEW_PR_TERM_PRODUCT
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='P' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PR_TERM_PRODUCT 
                AS SELECT pid.VOUCHER_NO, pid.VOUCHER_SNO, pid.PRODUCT_ID, {nullColum} 
                FROM
                       (
                         SELECT STD.PR_VNo VOUCHER_NO, STD.SNo VOUCHER_SNO, STD.Product_Id PRODUCT_ID,CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PR_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PR_Master AS SIM ON SIM.PR_Invoice=STD.PR_VNo
                         WHERE  Term_Type IN ('P')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            // ignored
        }

        // VIEW_PR_TERM_BILL
        try
        {
            var cmdString = @"
            SELECT PT_Name FROM AMS.PT_Term WHERE PT_Condition='B' AND PT_Type = 'G' ORDER BY Order_No; ";
            var columnName = SqlExtensions.ExecuteDataSetSql(cmdString);
            if (columnName.Rows.Count > 0)
            {
                var stringColum = string.Empty;
                var nullColum = string.Empty;
                foreach (DataRow row in columnName.Rows)
                {
                    if (columnName.Rows.IndexOf(row) == columnName.Rows.Count - 1)
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0)" + "[" + row[0] + "]";
                        stringColum = stringColum + "[" + row[0] + "]";
                    }
                    else
                    {
                        nullColum = nullColum + "ISNULL([" + row[0] + "],0) " + "[" + row[0] + "],";
                        stringColum = stringColum + "[" + row[0] + "],";
                    }
                }
                cmdString = $@"
                ALTER VIEW VM.VIEW_PR_TERM_BILL 
                AS SELECT pid.VOUCHER_NO,{nullColum} 
                FROM
                       (
                         SELECT STD.PR_VNo VOUCHER_NO, CASE WHEN ST_Sign='+' THEN Amount ELSE -Amount END AS Amount, SBT.ST_Name
                         FROM   AMS.PR_Term AS STD 
                            INNER JOIN AMS.PT_Term AS SBT ON STD.PT_Id=SBT.PT_Id 
                            INNER JOIN AMS.PR_Master AS SIM ON SIM.PR_Invoice=STD.PR_VNo
                         WHERE  Term_Type IN ('B')
                       ) AS d
                PIVOT(MAX(Amount) FOR PT_Name IN({stringColum})) AS pid; ";
                _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
        }
        catch
        {
            //IGNORED
        }

        #endregion PR_TERM(PRODUCT AND BILL)

        #endregion ---------------PT_TERM---------------
    }

    private static void AlterDatabaseProc()
    {
        try
        {
            const string cmdString = @"
            ALTER PROCEDURE [AMS].[USP_PostStockValue] @PCode VARCHAR(MAX) =''
            WITH ENCRYPTION
            AS BEGIN
                UPDATE AMS.StockDetails
                SET StockVal=(MonthRate.Rate *(ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0)))
                FROM(   SELECT PCR.PID, MIN(d.AD_Date) AS StDate, MAX(d.AD_Date) AS EnDate, PCR.NMonth, PCR.Rate
                        FROM(   SELECT CASE WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Bai' THEN '01'+REPLACE(PCR.Month_Miti, 'Bai', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Jes' THEN '02'+REPLACE(PCR.Month_Miti, 'Jes', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Ash' THEN '03'+REPLACE(PCR.Month_Miti, 'Ash', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Shr' THEN '04'+REPLACE(PCR.Month_Miti, 'Shr', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Bha' THEN '05'+REPLACE(PCR.Month_Miti, 'Bha', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Aas' THEN '06'+REPLACE(PCR.Month_Miti, 'Aas', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Kar' THEN '07'+REPLACE(PCR.Month_Miti, 'Kar', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Mag' THEN '08'+REPLACE(PCR.Month_Miti, 'Mag', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Pou' THEN '09'+REPLACE(PCR.Month_Miti, 'Pou', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Mar' THEN '10'+REPLACE(PCR.Month_Miti, 'Mar', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Fal' THEN '11'+REPLACE(PCR.Month_Miti, 'Fal', '')
                                       WHEN SUBSTRING(PCR.Month_Miti, 1, 3)='Cha' THEN '12'+REPLACE(PCR.Month_Miti, 'Cha', '')ELSE '' END AS NMonth, p.PID, PCR.Rate
                                FROM AMS.ProductClosingRate AS PCR
                                     INNER JOIN AMS.Product p ON PCR.Product_Id=p.PID
                                WHERE p.PValTech COLLATE DATABASE_DEFAULT IN ('M', 'R')AND(@PCode='' OR p.PID IN(SELECT Value FROM AMS.fn_Splitstring(@PCode, ',') ))) AS PCR
                            INNER JOIN AMS.DateMiti d ON PCR.NMonth LIKE SUBSTRING(d.BS_DateDMY COLLATE DATABASE_DEFAULT, 4, 7)
                        GROUP BY PCR.NMonth, PCR.PID, PCR.Rate) AS MonthRate
                WHERE Product_Id=MonthRate.PID AND Voucher_Date BETWEEN MonthRate.StDate AND MonthRate.EnDate;
                DECLARE @IsGodownWise BIT;
                SET @IsGodownWise=0;
                SELECT IDENTITY(INT, 1, 1) AS Id, st.Product_Id AS PCode, ISNULL(CONVERT(NVARCHAR(100), st.Branch_Id), 'No Branch') AS Br_Code, (CASE WHEN @IsGodownWise=1 THEN ISNULL(CONVERT(NVARCHAR(100), st.Godown_Id), 'No Godown')ELSE 'No Godown' END) AS Gdn_Code, CONVERT(NVARCHAR(100), st.Product_Id)+'_'+ISNULL(CONVERT(NVARCHAR(100), st.Branch_Id), 'No Branch')+'_'+(CASE WHEN @IsGodownWise=1 THEN ISNULL(CONVERT(NVARCHAR(100), st.Godown_Id), 'No Godown')ELSE 'No Godown' END) AS PBG_Code, p.PValTech
                INTO #Product
                FROM AMS.StockDetails st
                     INNER JOIN AMS.Product p ON st.Product_Id=p.PID
                WHERE(st.Product_Id IN(SELECT Value FROM AMS.fn_Splitstring(@PCode, ',') )OR @PCode IS NULL OR @PCode='')AND p.PValTech IN ('FIFO', 'X', 'W', 'R', 'F')
                GROUP BY st.Product_Id, st.Branch_Id, (CASE WHEN @IsGodownWise=1 THEN ISNULL(CONVERT(NVARCHAR(100), st.Godown_Id), 'No Godown')ELSE 'No Godown' END), p.PValTech
                ORDER BY st.Product_Id, Br_Code, (CASE WHEN @IsGodownWise=1 THEN ISNULL(CONVERT(NVARCHAR(100), st.Godown_Id), 'No Godown')ELSE 'No Godown' END);
                CREATE INDEX #Product_Index_PBG_Code ON #Product(PBG_Code);
                IF @IsGodownWise=1 BEGIN
                    UPDATE AMS.StockDetails
                    SET StockVal=0
                    WHERE EXISTS (SELECT * FROM #Product AS P WHERE Product_Id=P.PCode AND ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')=P.Br_Code AND ISNULL(CONVERT(NVARCHAR(100), Godown_Id), 'No Godown')=P.Gdn_Code)AND EntryType='O';
                END;
                ELSE BEGIN
                    UPDATE AMS.StockDetails
                    SET StockVal=0
                    WHERE EXISTS (SELECT * FROM #Product AS P WHERE Product_Id=P.PCode AND ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')=P.Br_Code)AND EntryType='O' AND(Module<>'ST' AND Module<>'SII');
                END;
                DECLARE @PBGCode VARCHAR(100);
                CREATE TABLE #StockTransIN (Id BIGINT         IDENTITY(1, 1) PRIMARY KEY,
                    VoucherNo                  NVARCHAR(50)   COLLATE DATABASE_DEFAULT NOT NULL,
                    VoucherDate                DATETIME       NOT NULL,
                    VoucherTime                DATETIME       NOT NULL,
                    Module                     NVARCHAR(50)   COLLATE DATABASE_DEFAULT NOT NULL,
                    VoucherSno                 INT            NOT NULL,
                    PBG_Code                   VARCHAR(100)   COLLATE DATABASE_DEFAULT NOT NULL,
                    Qty                        DECIMAL(18, 6) NOT NULL,
                    Value                      DECIMAL(18, 6) NOT NULL,
                    RedimQty                   DECIMAL(18, 6) NOT NULL);
                CREATE INDEX #StockTransIN_Index_PBG_Code ON #StockTransIN(PBG_Code);
                CREATE TABLE #StockTransOUT (Id BIGINT         IDENTITY(1, 1) PRIMARY KEY,
                    VoucherNo                   NVARCHAR(50)   COLLATE DATABASE_DEFAULT NOT NULL,
                    VoucherDate                 DATETIME       NOT NULL,
                    VoucherTime                 DATETIME       NOT NULL,
                    VoucherMiti                 VARCHAR(10)    COLLATE DATABASE_DEFAULT NOT NULL,
                    Module                      NVARCHAR(50)   COLLATE DATABASE_DEFAULT NOT NULL,
                    VoucherSno                  INT            NOT NULL,
                    PBG_Code                    VARCHAR(100)   COLLATE DATABASE_DEFAULT NOT NULL,
                    Qty                         DECIMAL(18, 6) NOT NULL,
                    AdjustQty                   DECIMAL(18, 6) NOT NULL,
                    StockVal                    DECIMAL(18, 6) NOT NULL);
                CREATE INDEX #StockTransOUT_Index_PBG_Code ON #StockTransOUT(PBG_Code);
                CREATE TABLE #Stock_Adjustment (Id INT            IDENTITY(1, 1),
                    In_Id                          INT            NOT NULL,
                    RemainQty                      DECIMAL(18, 6) NOT NULL,
                    Out_Id                         INT            NOT NULL,
                    Qty                            DECIMAL(18, 6) NOT NULL);
                IF @IsGodownWise=1 BEGIN
                    INSERT INTO #StockTransIN(VoucherNo, VoucherDate, VoucherTime, Module, VoucherSno, PBG_Code, Qty, Value, RedimQty)
                    SELECT Voucher_No, Voucher_Date, Voucher_Time, sd.Module, Serial_No, CONVERT(NVARCHAR(100), Product_Id)+'_'+ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')+'_'+ISNULL(CONVERT(NVARCHAR(100), Godown_Id), 'No Godown'), ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0) AS StockQty, (CASE WHEN StockQty>0 THEN StockVal /(ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0))ELSE 0 END), 0
                    FROM AMS.StockDetails sd
                    WHERE EXISTS (SELECT * FROM #Product AS P WHERE Product_Id=P.PCode AND ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')=P.Br_Code AND ISNULL(CONVERT(NVARCHAR(100), Godown_Id), 'No Godown')=P.Gdn_Code)AND EntryType='I'
                    ORDER BY Voucher_Date, EntryType, Voucher_Time, Voucher_No;
                    INSERT INTO #StockTransOUT(VoucherNo, Module, VoucherDate, VoucherTime, VoucherMiti, VoucherSno, PBG_Code, Qty, AdjustQty, StockVal)
                    SELECT Voucher_No, sd.Module, Voucher_Date, Voucher_Time, Voucher_Miti, Serial_No, CONVERT(NVARCHAR(100), Product_Id)+'_'+ISNULL(Branch_Id, 'No Branch')+'_'+ISNULL(Godown_Id, 'No Godown'), ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0) AS StockQty, 0, 0
                    FROM AMS.StockDetails sd
                    WHERE EXISTS (SELECT * FROM #Product AS P WHERE Product_Id=P.PCode AND ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')=P.Br_Code AND ISNULL(CONVERT(NVARCHAR(100), Godown_Id), 'No Godown')=P.Gdn_Code)AND EntryType='O'
                    ORDER BY Voucher_Date, EntryType, Voucher_Time, Voucher_No;
                END;
                ELSE BEGIN
                    INSERT INTO #StockTransIN(VoucherNo, VoucherDate, VoucherTime, Module, VoucherSno, PBG_Code, Qty, Value, RedimQty)
                    SELECT Voucher_No, Voucher_Date, Voucher_Time, Module, Serial_No, CONVERT(NVARCHAR(100), Product_Id)+'_'+ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')+'_'+'No Godown', ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0) AS StockQty, (CASE WHEN StockQty>0 THEN StockVal /(ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0))ELSE 0 END), 0
                    FROM AMS.StockDetails
                    WHERE EXISTS (SELECT * FROM #Product AS P WHERE Product_Id=P.PCode AND ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')=P.Br_Code)AND EntryType='I' AND(Module<>'ST' AND Module<>'SII')
                    ORDER BY Voucher_Date, EntryType, Voucher_Time, Voucher_No;
                    INSERT INTO #StockTransOUT(VoucherNo, Module, VoucherDate, VoucherTime, VoucherMiti, VoucherSno, PBG_Code, Qty, AdjustQty, StockVal)
                    SELECT Voucher_No, Module, Voucher_Date, Voucher_Time, Voucher_Miti, Serial_No, CONVERT(NVARCHAR(100), Product_Id)+'_'+ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')+'_'+'No Godown', ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0) AS StockQty, 0, 0
                    FROM AMS.StockDetails
                    WHERE EXISTS (SELECT * FROM #Product AS P WHERE Product_Id=P.PCode AND ISNULL(CONVERT(NVARCHAR(100), Branch_Id), 'No Branch')=P.Br_Code)AND EntryType='O' AND(Module<>'ST' AND Module<>'SII')
                    ORDER BY Voucher_Date, EntryType, Voucher_Time, Voucher_No;
                END;
                DECLARE @Id INT, @STINId INT, @OutId INT;
                SET @Id=0;
                SET @STINId=0;
                SET @OutId=0;
                DECLARE @INQty DECIMAL(18, 6), @OUTQty DECIMAL(18, 6), @Value DECIMAL(18, 6), @UnAdjustQty DECIMAL(18, 6);
                DECLARE @OutDate DATETIME;
                WHILE EXISTS (SELECT * FROM #Product WHERE Id>@Id AND PValTech IN ('FIFO', 'R', 'F'))BEGIN
                    SET @Value=0;
                    SELECT TOP(1)@Id=Id, @PBGCode=PBG_Code FROM #Product WHERE Id>@Id ORDER BY Id;
                    SET @STINId=0;
                    WHILE EXISTS (SELECT * FROM #StockTransOUT WHERE PBG_Code=@PBGCode AND Qty>AdjustQty)AND EXISTS (SELECT * FROM #StockTransIN WHERE PBG_Code=@PBGCode AND Id>@STINId)BEGIN
                        SELECT TOP(1)@STINId=Id, @INQty=Qty, @Value=Value FROM #StockTransIN WHERE PBG_Code=@PBGCode AND Id>@STINId ORDER BY Id;
                        WHILE(@INQty>0)AND EXISTS (SELECT * FROM #StockTransOUT WHERE PBG_Code=@PBGCode AND Qty>AdjustQty)BEGIN
                            SELECT TOP(1)@OutId=Id, @UnAdjustQty=Qty-AdjustQty FROM #StockTransOUT WHERE PBG_Code=@PBGCode AND Qty>AdjustQty ORDER BY Id;
                            IF(@INQty>@UnAdjustQty)BEGIN
                                UPDATE #StockTransOUT SET AdjustQty=Qty, StockVal=StockVal+(@Value * @UnAdjustQty)WHERE Id=@OutId;
                                INSERT INTO #Stock_Adjustment(In_Id, RemainQty, Out_Id, Qty)VALUES(@STINId, @INQty, @OutId, @UnAdjustQty);
                                SET @INQty=@INQty-@UnAdjustQty;
                            END;
                            ELSE BEGIN
                                UPDATE #StockTransOUT SET AdjustQty=AdjustQty+@INQty, StockVal=StockVal+(@Value * @INQty)WHERE Id=@OutId;
                                INSERT INTO #Stock_Adjustment(In_Id, RemainQty, Out_Id, Qty)VALUES(@STINId, @INQty, @OutId, @INQty);
                                SET @INQty=0;
                            END;
                        END;
                    END;
                    UPDATE #StockTransOUT SET StockVal=ISNULL(StockVal, 0)+((Qty-AdjustQty)* @Value), AdjustQty=Qty WHERE Qty>AdjustQty AND PBG_Code=@PBGCode;
                END;
                WHILE EXISTS (SELECT * FROM #Product WHERE Id>@Id AND PValTech='X')BEGIN
                    SELECT TOP(1)@Id=Id, @PBGCode=PBG_Code FROM #Product WHERE Id>@Id ORDER BY Id;
                    SET @OutId=0;
                    WHILE EXISTS (SELECT * FROM #StockTransIN WHERE PBG_Code=@PBGCode AND Qty>RedimQty)AND EXISTS (SELECT * FROM #StockTransOUT WHERE PBG_Code=@PBGCode AND Id>@OutId)BEGIN
                        SELECT TOP(1)@OutId=Id, @OutDate=VoucherDate FROM #StockTransOUT WHERE PBG_Code=@PBGCode AND Id>@OutId ORDER BY Id;
                        SELECT @Value=SUM(Value)/ COUNT(*)FROM #StockTransIN WHERE PBG_Code=@PBGCode AND VoucherDate<=@OutDate AND Qty>RedimQty;
                        SELECT @OUTQty=SUM(Qty)FROM #StockTransOUT WHERE PBG_Code=@PBGCode AND VoucherDate=@OutDate AND StockVal>0;
                        UPDATE #StockTransOUT SET StockVal=(@Value * Qty), AdjustQty=Qty WHERE PBG_Code=@PBGCode AND VoucherDate=@OutDate;
                        WHILE(@OUTQty>0)AND EXISTS (SELECT * FROM #StockTransIN WHERE PBG_Code=@PBGCode AND Qty>RedimQty)BEGIN
                            SELECT TOP(1)@STINId=Id, @UnAdjustQty=Qty-RedimQty FROM #StockTransIN WHERE PBG_Code=@PBGCode AND Qty>RedimQty ORDER BY Id;
                            IF(@OUTQty>@UnAdjustQty)BEGIN
                                UPDATE #StockTransIN SET RedimQty=Qty WHERE Id=@STINId;
                                SET @OUTQty=@OUTQty-@UnAdjustQty;
                            END;
                            ELSE BEGIN
                                UPDATE #StockTransIN SET RedimQty=RedimQty+@OUTQty WHERE Id=@STINId;
                                SET @OUTQty=0;
                            END;
                        END;
                    END;
                    UPDATE #StockTransOUT SET StockVal=ISNULL(StockVal, 0)+((Qty-AdjustQty)* @Value), AdjustQty=Qty WHERE Qty>AdjustQty AND PBG_Code COLLATE DATABASE_DEFAULT=@PBGCode;
                END;
                UPDATE #StockTransOUT
                SET StockVal=(AverageValue * Qty)
                FROM(   SELECT PIn.PBG_Code, SUM(PIn.Value * PIn.Qty)/ SUM(PIn.Qty) AS AverageValue
                        FROM #StockTransIN AS PIn
                             INNER JOIN #Product AS P ON PIn.PBG_Code=P.PBG_Code
                        WHERE P.PValTech COLLATE DATABASE_DEFAULT='W'
                        GROUP BY PIn.PBG_Code) AS InQty
                WHERE #StockTransOUT.PBG_Code=InQty.PBG_Code;
                UPDATE AMS.StockBatchDetails
                SET StockVal=(ISNULL(StockQty, 0)+ISNULL(FreeStockQty, 0)+ISNULL(StockExtraFreeQty, 0))* OutVal.StockVal
                FROM(   SELECT STPD.Product_Id, STPD.SNo, SUM(StockVal)/ SUM(ISNULL(STPD.StockQty, 0)+ISNULL(STPD.FreeStockQty, 0)+ISNULL(STPD.StockExtraFreeQty, 0)) AS StockVal
                        FROM AMS.StockBatchDetails AS STPD
                             INNER JOIN AMS.Product p ON STPD.Product_Id=p.PID
                        WHERE p.PBatchwise=1 AND STPD.Transaction_Type COLLATE DATABASE_DEFAULT='I' AND(STPD.Module COLLATE DATABASE_DEFAULT<>'ST' AND STPD.Module COLLATE DATABASE_DEFAULT<>'SII')
                        GROUP BY STPD.Product_Id, STPD.SNo) AS OutVal
                WHERE StockBatchDetails.Product_Id=OutVal.Product_Id AND StockBatchDetails.SNo=OutVal.SNo AND Transaction_Type COLLATE DATABASE_DEFAULT='O' AND(StockBatchDetails.Module COLLATE DATABASE_DEFAULT<>'ST' AND StockBatchDetails.Module COLLATE DATABASE_DEFAULT<>'SII');
                UPDATE AMS.StockDetails
                SET StockVal=Batch.StockVal
                FROM(   SELECT sbd.Voucher_No, sbd.SNo, sbd.Module, SUM(StockVal) AS StockVal
                        FROM AMS.StockBatchDetails sbd
                             INNER JOIN AMS.Product p ON sbd.Product_Id=p.PID
                        WHERE p.PBatchwise=1 AND sbd.Transaction_Type COLLATE DATABASE_DEFAULT='O'
                        GROUP BY sbd.Voucher_No, sbd.SNo, sbd.Module) AS Batch
                WHERE StockDetails.Voucher_No COLLATE DATABASE_DEFAULT=Batch.Voucher_No COLLATE DATABASE_DEFAULT AND StockDetails.Serial_No=Batch.SNo AND StockDetails.Module COLLATE DATABASE_DEFAULT=Batch.Module COLLATE DATABASE_DEFAULT AND StockDetails.EntryType COLLATE DATABASE_DEFAULT='O';
                UPDATE AMS.StockDetails
                SET StockVal=(ISNULL(StockQty, 0)+ISNULL(StockFreeQty, 0)+ISNULL(ExtraStockFreeQty, 0))* OutVal.StockVal
                FROM(   SELECT ST.Product_Id, SUM(StockVal)/ SUM(ISNULL(ST.StockQty, 0)+ISNULL(ST.StockFreeQty, 0)+ISNULL(ST.ExtraStockFreeQty, 0)) AS StockVal
                        FROM AMS.StockDetails AS ST
                             INNER JOIN AMS.Product p ON ST.Product_Id=p.PID
                        WHERE p.PBatchwise=1 AND NOT EXISTS (   SELECT *
                                                                FROM AMS.StockBatchDetails AS STPD
                                                                WHERE ST.Voucher_No COLLATE DATABASE_DEFAULT=STPD.Voucher_No COLLATE DATABASE_DEFAULT AND ST.Serial_No=STPD.SNo AND ST.Module COLLATE DATABASE_DEFAULT=STPD.Module COLLATE DATABASE_DEFAULT)AND ST.EntryType COLLATE DATABASE_DEFAULT='I'
                        GROUP BY ST.Product_Id) AS OutVal
                WHERE StockDetails.Product_Id=OutVal.Product_Id AND NOT EXISTS (SELECT * FROM AMS.StockBatchDetails AS STPD);
                UPDATE AMS.StockDetails
                SET StockVal=tmp.StockVal
                FROM #StockTransOUT AS tmp
                WHERE StockDetails.Voucher_No COLLATE DATABASE_DEFAULT=tmp.VoucherNo COLLATE DATABASE_DEFAULT AND StockDetails.Module COLLATE DATABASE_DEFAULT=tmp.Module COLLATE DATABASE_DEFAULT AND Serial_No=tmp.VoucherSno AND StockDetails.EntryType COLLATE DATABASE_DEFAULT='O';
            END;";
            _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER PROCEDURE [AMS].[FifoValueCalculation] @Product_Id VARCHAR(10), @Voucher_Date DATETIME, @FreeQty CHAR(1) = ''
                WITH ENCRYPTION
                AS
                DECLARE @Module VARCHAR(10), @Voucher_No VARCHAR(15), @SNO INT, @OutQty DECIMAL(18, 8), @OutVal DECIMAL(18, 8);
                DECLARE @InQty DECIMAL(18, 8), @InVal DECIMAL(18, 8), @InDate DATETIME;
                DECLARE @UpdateVal DECIMAL(18, 8), @OutDate DATETIME;
                IF NOT EXISTS
                 (
                   SELECT name
                   FROM sysobjects
                   WHERE id=OBJECT_ID(N'[dbo].[CalculateStockValue]')AND type IN (N'U')
                 ) BEGIN
                  SELECT *
                  INTO CalculateStockValue
                  FROM AMS.StockDetails
                  WHERE Product_Id=@Product_Id;
                END;
                UPDATE CalculateStockValue
                SET StockVal=0
                WHERE EntryType='O' AND Product_Id=@Product_Id;
                IF ISDATE(@Voucher_Date)=1 BEGIN
                  IF @FreeQty=''
                    DECLARE InStock CURSOR LOCAL FOR
                    SELECT Voucher_Date, StockQty, CASE WHEN (StockQty+StockFreeQty) <>0 THEN (StockVal / (StockQty+StockFreeQty)) ELSE 0 END AS StockVal
                    FROM CalculateStockValue
                    WHERE EntryType='I' AND Product_Id=@Product_Id AND Voucher_Date<=@Voucher_Date
                    ORDER BY Voucher_Date, EntryType, Voucher_No;
                  ELSE
                    DECLARE InStock CURSOR LOCAL FOR
                    SELECT Voucher_Date, StockQty, CASE WHEN (StockQty) <>0 THEN (StockVal / StockQty) ELSE 0 END AS StockVal
                    FROM CalculateStockValue
                    WHERE EntryType='I' AND Product_Id=@Product_Id AND Voucher_Date<=@Voucher_Date
                    ORDER BY Voucher_Date, EntryType, Voucher_No;
                END;
                ELSE BEGIN
                  IF @FreeQty=''
                    DECLARE InStock CURSOR LOCAL FOR
                    SELECT Voucher_Date, StockQty, CASE WHEN (StockQty+StockFreeQty) <>0 THEN (StockVal / (StockQty+StockFreeQty)) ELSE 0 END AS StockVal
                    FROM CalculateStockValue
                    WHERE EntryType='I' AND Product_Id=@Product_Id
                    ORDER BY Voucher_Date, EntryType, Voucher_No;
                  ELSE
                    DECLARE InStock CURSOR LOCAL FOR
                    SELECT Voucher_Date, StockQty, CASE WHEN (StockQty) <>0 THEN (StockVal / StockQty) ELSE 0 END AS StockVal
                    FROM CalculateStockValue
                    WHERE EntryType='I' AND Product_Id=@Product_Id
                    ORDER BY Voucher_Date, EntryType, Voucher_No;
                END;
                OPEN InStock;
                SET @InQty=0;
                SET @OutQty=0;
                DECLARE OutStock CURSOR LOCAL FOR
                SELECT Voucher_Date, Module, Voucher_No, Serial_No, (StockQty+StockFreeQty), StockVal
                FROM CalculateStockValue
                WHERE EntryType='O' AND Product_Id=@Product_Id
                ORDER BY Voucher_Date, EntryType, Voucher_No;
                OPEN OutStock;
                FETCH FROM OutStock
                INTO @OutDate, @Module, @Voucher_No, @SNO, @OutQty, @OutVal;
                WHILE (@@FETCH_STATUS=0) BEGIN
                  SET @UpdateVal=0;
                  UP:
                  IF @InQty<=0 FETCH NEXT FROM InStock INTO @InDate, @InQty, @InVal;
                  IF @OutDate>=DATEADD(DAY, -1, @InDate)BEGIN
                    IF @InQty>=@OutQty BEGIN
                      SET @UpdateVal=@UpdateVal+ (@OutQty * @InVal);
                      SET @InQty=@InQty-@OutQty;
                    END;
                    ELSE BEGIN
                      SET @UpdateVal=@UpdateVal+ (@InQty * @InVal);
                      SET @OutQty=@OutQty-@InQty;
                      SET @InQty=0;
                      IF (@@FETCH_STATUS=0) GOTO UP;
                    END;
                    UPDATE CalculateStockValue
                    SET StockVal=@UpdateVal
                    WHERE Module=@Module AND Voucher_No=@Voucher_No AND Serial_No=@SNO;
                  END;
                  FETCH NEXT FROM OutStock
                  INTO @OutDate, @Module, @Voucher_No, @SNO, @OutQty, @OutVal;
                END;
                IF EXISTS
                 (
                   SELECT *
                   FROM sysobjects
                   WHERE id=OBJECT_ID(N'[dbo].[CalculateStockValue]')AND type IN (N'U')
                 )
                  DELETE FROM AMS.StockDetails
                  WHERE Product_Id=@Product_Id;
                INSERT INTO AMS.StockDetails
                SELECT *
                FROM CalculateStockValue
                WHERE Product_Id=@Product_Id;
                IF EXISTS
                 (
                   SELECT *
                   FROM sysobjects
                   WHERE id=OBJECT_ID(N'[dbo].[CalculateStockValue]')AND type IN (N'U')
                 ) BEGIN
                  DROP TABLE CalculateStockValue;
                END;
                CLOSE OutStock;
                DEALLOCATE OutStock;
                CLOSE InStock;
                DEALLOCATE InStock;";
            _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch
        {
            // ignored
        }

        try
        {
            const string cmdString = @"
                ALTER PROCEDURE [AMS].[StockValueCalculation] @Product_Id VARCHAR(10), @Voucher_Date VARCHAR(15) ='', @ValueMethod CHAR(1) ='', @DateType CHAR(1) ='', @Recalculate CHAR(1) ='F', @Trans_type CHAR(1) ='I'
                WITH ENCRYPTION AS
                DECLARE @cmdString VARCHAR(8000);
                SET @cmdString='';
                IF @Recalculate='F' AND @Trans_type='O' BEGIN
                  SELECT @Voucher_Date=Voucher_Date
                  FROM AMS.StockDetails
                  WHERE Product_Id=''+@Product_Id+'';
                END;
                IF @ValueMethod IN ('F','FIFO') EXEC AMS.FifoValueCalculation @Product_Id, @Voucher_Date;
                IF @ValueMethod='W' BEGIN
                  SET @cmdString='UPDATE AMS.StockDetails set StockVal = (StockQty *  AvgRate) from (';
                  SET @cmdString=@cmdString+' select Product_Id , sum(StockVal) / sum(StockQty) as AvgRate from AMS.StockDetails where StockQty >0 and  Product_Id='''+@Product_Id+'''  and EntryType=''I'' group by Product_Id) as TblAvgrate';
                  SET @cmdString=@cmdString+' where AMS.StockDetails.Product_Id='''+@Product_Id+'''';
                  SET @cmdString=@cmdString+' and EntryType = ''O''';
                  IF @Voucher_Date<>'' SET @cmdString=@cmdString+' and Voucher_Date >='''+@Voucher_Date+'''';
                  SET @cmdString=@cmdString+' and TblAvgrate.Product_Id = AMS.StockDetails.Product_Id and AMS.StockDetails.Product_Id ='''+@Product_Id+'''';
                  EXEC (@cmdString);
                END;";
            _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch
        {
            // ignored
        }
    }

    public static void DefaultFiscalYearValueOnDatabase()
    {
        try
        {
            var cmdString = @$"
                Update AMS.FiscalYear set Current_FY=0 From AMS.FiscalYear;
                Update AMS.FiscalYear set Current_FY=1 From AMS.FiscalYear where FY_Id= '{ObjGlobal.SysFiscalYearId}' ; ";
            _ = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch
        {
            // ~ignored~
        }

        var numbering = InsertDocumentNumbering();
        if (numbering <= 0)
        {
        }
    }

    public static void AlterRestoreDatabaseInitial()
    {
        try
        {
            var cmdText = $"SELECT * FROM sys.database_files df WHERE df.name='{GetConnection.LoginInitialCatalog}'";
            var dtCheck = SqlExtensions.ExecuteDataSet(cmdText);
            if (dtCheck.Tables[0].Rows.Count is 0)
            {
                var oldInitial =
                    GetConnection.GetQueryData("SELECT DISTINCT df.name FROM sys.database_files df WHERE df.file_id=1");
                var oldInitialLog =
                    GetConnection.GetQueryData("SELECT DISTINCT df.name FROM sys.database_files df WHERE df.file_id=2");
                var cmdString = $@"
					ALTER DATABASE {ObjGlobal.InitialCatalog} SET SINGLE_USER WITH ROLLBACK IMMEDIATE
					ALTER DATABASE {ObjGlobal.InitialCatalog} MODIFY FILE (NAME=N'{oldInitial}', NEWNAME=N'{ObjGlobal.InitialCatalog}')
					ALTER DATABASE {ObjGlobal.InitialCatalog} MODIFY FILE (NAME=N'{oldInitialLog}', NEWNAME=N'{ObjGlobal.InitialCatalog}_Log')
					ALTER DATABASE {ObjGlobal.InitialCatalog} SET MULTI_USER WITH ROLLBACK IMMEDIATE";
                var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
                if (result <= 0)
                {
                }
            }
        }
        catch
        {
            // ignored
        }
    }

    private static void AlterDatabaseTableQuery()
    {
        try
        {
            var cmdString = $@"
            UPDATE AMS.DocumentNumbering SET FiscalYearId='{ObjGlobal.SysFiscalYearId}' WHERE FiscalYearId IS NULL;
            UPDATE AMS.Product SET Barcode1=PID WHERE Barcode1 IS NULL;
            UPDATE AMS.Product SET Barcode2=(100000000000000+p.PID)FROM AMS.Product p WHERE Barcode2 IS NULL;
            UPDATE AMS.Product SET Barcode3=(100000000+p.PID)FROM AMS.Product p WHERE Barcode3 IS NULL;
            UPDATE AMS.Product
            SET BeforeBuyRate=CASE WHEN (PTax=0 OR PTax IS NULL) THEN PBuyRate ELSE PBuyRate / 1.13 END, BeforeSalesRate=CASE WHEN (PTax=0 OR PTax IS NULL) THEN PSalesRate ELSE PSalesRate / 1.13 END
            WHERE BeforeBuyRate IS NULL OR BeforeSalesRate IS NULL;
            UPDATE AMS.SC_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate;
            UPDATE AMS.Scheme_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE SchemeDate BETWEEN Start_ADDate AND End_ADDate;
            UPDATE AMS.SO_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.SQ_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate;
            UPDATE AMS.StockDetails SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Voucher_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.AccountDetails SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Voucher_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.PB_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.SB_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.CB_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Voucher_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.Notes_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Voucher_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.PAB_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.PC_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.PIN_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE PIN_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.PO_Master SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE Invoice_Date BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.PostDateCheque SET FiscalYearId=FY.FY_Id FROM AMS.FiscalYear AS FY WHERE VoucherDate BETWEEN Start_ADDate AND End_ADDate AND FiscalYearId IS NULL;
            UPDATE AMS.TableMaster SET TableType='D' WHERE(TableType IS NULL OR TableType='');
            UPDATE AMS.TableMaster SET Printed=0 WHERE Printed IS NULL;
            UPDATE AMS.FiscalYear SET End_BSDate='32/03/2079', End_ADDate='2022-07-16' WHERE FY_Id=12 AND End_BSDate<>'32/03/2079';
            UPDATE AMS.FiscalYear SET Start_ADDate='2023-07-17' WHERE FY_Id=14 AND Start_ADDate<>'2023-07-17';';
            ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            _ = e.Message;
        }

        try
        {
            var cmdString = $@"
            IF EXISTS (SELECT * FROM AMS.FiscalYear WHERE AD_FY='0')BEGIN
                UPDATE AMS.FiscalYear
                SET AD_FY=fy.ADFiscalYear, BS_FY=fy.BSFiscalYear, Start_BSDate=fy.StartBSDate, End_BSDate=fy.EndBSDate
                FROM master.AMS.FiscalYear fy
                WHERE FY_Id=FiscalYearId;
            END; ";
            var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
        }
        catch (Exception e)
        {
            _ = e.Message;
        }

        try
        {
            if (ObjGlobal.IsIrdRegister)
            {
                var cmdString = $@"
                IF EXISTS (SELECT * FROM AMS.CompanyInfo WHERE IsTaxRegister=0)
                UPDATE AMS.CompanyInfo SET IsTaxRegister=1;";
                var result = SqlExtensions.ExecuteNonQueryIgnoreException(cmdString);
            }
            
        }
        catch (Exception e)
        {
            _ = e.Message;
        }

        try
        {
            var con = new SqlConnection(GetConnection.ConnectionString);
            con.Open();
            var cmd = new SqlCommand("sys.sp_rename", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@objname", "AMS.SyncTable.SyncOrginId").SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.AddWithValue("@newname", "SyncOriginId").SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.AddWithValue("@objtype", "COLUMN").SqlDbType = SqlDbType.NVarChar;
            var result = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            _ = e.Message;
        }
        try
        {
            var con = new SqlConnection(GetConnection.ConnectionStringMaster);
            con.Open();
            var cmd = new SqlCommand("sys.sp_rename", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@objname", "AMS.SoftwareRegistration.Reguestby").SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.AddWithValue("@newname", "RequestBy").SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.AddWithValue("@objtype", "COLUMN").SqlDbType = SqlDbType.NVarChar;
            var result = cmd.ExecuteNonQuery();
        }
        catch (Exception e)
        {
            _ = e.Message;
        }

        try
        {
            var con = new SqlConnection(GetConnection.ConnectionString);
            con.Open();
            var cmd = new SqlCommand("sys.sp_rename", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@objname", "AMS.InventorySetting.GodownWiseFilter").SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.AddWithValue("@newname", "GroupWiseFilter").SqlDbType = SqlDbType.NVarChar;
            cmd.Parameters.AddWithValue("@objtype", "COLUMN").SqlDbType = SqlDbType.NVarChar;
            var result = cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            //
        }
    }

    private static void AlterDatabaseIndexQuery()
    {
        //try
        //{
        //    const string cmd = @"
        //    IF NOT EXISTS( SELECT * FROM sys.indexes WHERE type_desc='NONCLUSTERED' AND name='IX_SO_Master_MasterKeyId')
        //    CREATE UNIQUE NONCLUSTERED INDEX [IX_SO_Master_MasterKeyId]
        //    ON [AMS].[SO_Master] ([MasterKeyId] ASC)
        //    WITH (PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)
        //    ON [PRIMARY];";
        //    var result = ExecuteCommand.ExecuteNonQueryIgnoreException(cmd);
        //}
        //catch (Exception e)
        //{
        //    var errMsg = e.Message;
        //}

        //try
        //{
        //    const string cmd = @"
        //        IF EXISTS( SELECT * FROM sys.indexes WHERE type_desc='CLUSTERED' AND name='IX_SO_Details')
        //        DROP INDEX [IX_SO_Details] ON [AMS].[SO_Details];";
        //    var result = ExecuteCommand.ExecuteNonQueryIgnoreException(cmd);
        //}
        //catch (Exception e)
        //{
        //    var errMsg = e.Message;
        //}

        //try
        //{
        //    const string cmd = @"
        //        IF NOT EXISTS( SELECT * FROM sys.indexes WHERE type_desc='CLUSTERED' AND name='IX_SO_Details')
        //        CREATE UNIQUE NONCLUSTERED INDEX [IX_SO_Details_MasterKey]
        //        ON [AMS].[SO_Details] ([SO_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC, [MasterKeyId] ASC)
        //        WITH (PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)
        //        ON [PRIMARY];";
        //    var result = ExecuteCommand.ExecuteNonQueryIgnoreException(cmd);
        //}
        //catch (Exception e)
        //{
        //    var errMsg = e.Message;
        //}

        //try
        //{
        //    const string cmd = @"
        //        IF COL_LENGTH('AMS.SO_Details', 'MasterKeyId') IS NOT NULL BEGIN
        //            ALTER TABLE [AMS].[SO_Details] WITH CHECK
        //            ADD CONSTRAINT[FK_SO_Details_SO_Master_MasterKeyId] FOREIGN KEY([MasterKeyId]) REFERENCES[AMS].[SO_Master]([MasterKeyId]);
        //            ALTER TABLE[AMS].[SO_Details] CHECK CONSTRAINT[FK_SO_Details_SO_Master_MasterKeyId]
        //        END;";
        //    var result = ExecuteCommand.ExecuteNonQueryIgnoreException(cmd);
        //}
        //catch (Exception e)
        //{
        //    var errMsg = e.Message;
        //}
    }

    #endregion *--------------- Alter Database Table ---------------*

    // ALTER MASTER DATABASE

    #region *--------------- Master Database ---------------*

    public static void AlterMasterTable()
    {
        try
        {
            var result = SqlExtensions.ExecuteNonQueryOnMaster(Resources.CreateMasterTableQuery);
            if (result <= 0)
            {

            }
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
        try
        {
            var cmd = string.Empty;
            cmd = @"
            SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS  ifc WHERE TABLE_NAME = 'SoftwareRegistration' and ifc.COLUMN_NAME = 'RegistrationId' AND ifc.DATA_TYPE <> 'UNIQUEIDENTIFIER'";
            var exits = SqlExtensions.ExecuteDataSet(cmd).Tables[0];
            if (exits.Rows.Count > 0)
            {
                cmd = "SELECT * FROM AMS.SoftwareRegistration";
                var result = SqlExtensions.ExecuteDataSet(cmd);
                cmd = @"
                    IF (NOT EXISTS (  SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS  ifc WHERE TABLE_NAME = 'SoftwareRegistration' and ifc.COLUMN_NAME = 'RegistrationId' AND ifc.DATA_TYPE = 'UNIQUEIDENTIFIER'))
                    BEGIN
                            ALTER TABLE AMS.SoftwareRegistration DROP CONSTRAINT [PK_SoftwareRegistration];
                    END;";
                var exceptionMaster = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
                cmd = @"
                    IF (NOT EXISTS (  SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS  ifc WHERE TABLE_NAME = 'SoftwareRegistration' and ifc.COLUMN_NAME = 'RegistrationId' AND ifc.DATA_TYPE = 'UNIQUEIDENTIFIER'))
                    BEGIN
                            ALTER TABLE AMS.SoftwareRegistration ALTER COLUMN RegistrationId NVARCHAR(MAX) NULL;
                    END;";
                var master = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
                if (result.Tables[0].Rows.Count > 0)
                {
                    cmd = "UPDATE AMS.SoftwareRegistration SET RegistrationId = NULL";
                    var updateNull = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);

                    cmd = "ALTER TABLE AMS.SoftwareRegistration ALTER COLUMN RegistrationId UNIQUEIDENTIFIER NULL;";
                    var uni = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);

                    cmd = "UPDATE AMS.SoftwareRegistration SET RegistrationId = NEWID();";
                    var newId = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
                }
                else
                {
                    cmd = " ALTER TABLE AMS.SoftwareRegistration ALTER COLUMN RegistrationId UNIQUEIDENTIFIER NULL; ";
                    var uni = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
                }

                cmd = @"
                    IF (EXISTS (  SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS  ifc WHERE TABLE_NAME = 'SoftwareRegistration' and ifc.COLUMN_NAME = 'RegistrationId' AND ifc.DATA_TYPE = 'UNIQUEIDENTIFIER'))
                    BEGIN
                        ALTER TABLE AMS.SoftwareRegistration ALTER COLUMN RegistrationId UNIQUEIDENTIFIER NOT NULL;
                    END;";
                var ignoreExceptionMaster = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
                cmd = @"
                    IF (EXISTS (  SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS  ifc WHERE TABLE_NAME = 'SoftwareRegistration' and ifc.COLUMN_NAME = 'RegistrationId' AND ifc.DATA_TYPE = 'UNIQUEIDENTIFIER'))
                    BEGIN
                        ALTER TABLE AMS.SoftwareRegistration ADD CONSTRAINT PK_SoftwareRegistration PRIMARY KEY (RegistrationId);
                    END;";
                var unified = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
            }
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }
        try
        {
            const string cmd = @"
           IF NOT EXISTS (SELECT * FROM AMS.LicenseInfo) BEGIN
	            DROP TABLE AMS.LicenseInfo;
	            CREATE TABLE [AMS].[LicenseInfo] ([LicenseId] [UNIQUEIDENTIFIER] NOT NULL,
		            [InstalDate] [NVARCHAR](MAX) NULL,
		            [RegistrationId] [UNIQUEIDENTIFIER] NULL,
		            [RegistrationDate] [NVARCHAR](MAX) NULL,
		            [SerialNo] [UNIQUEIDENTIFIER] NULL,
		            [ActivationId] [UNIQUEIDENTIFIER] NULL,
		            [LicenseExpireDate] [NVARCHAR](MAX) NULL,
		            [ExpireGuid] [UNIQUEIDENTIFIER] NULL,
		            [LicenseTo] [NVARCHAR](MAX) NULL,
		            [RegAddress] [NVARCHAR](MAX) NULL,
		            [ParentCompany] [NVARCHAR](MAX) NULL,
		            [ParentAddress] [NVARCHAR](MAX) NULL,
		            [CustomerId] [NVARCHAR](MAX) NULL,
		            [ServerMacAddress] [NVARCHAR](MAX) NULL,
		            [ServerName] [NVARCHAR](MAX) NULL,
		            [RequestBy] [NVARCHAR](MAX) NULL,
		            [RegisterBy] [NVARCHAR](MAX) NULL,
		            [LicenseDays] [NVARCHAR](MAX) NULL,
		            [LicenseModule] [NVARCHAR](MAX) NULL,
		            [NodesNo] [NVARCHAR](MAX) NULL,
		            [SystemId] [NVARCHAR](MAX) NULL,
		            CONSTRAINT [PK_LicenseInfo] PRIMARY KEY CLUSTERED ([LicenseId] ASC) WITH (PAD_INDEX = OFF,STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON,ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
            END; ";
            var unified = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
        }
        catch (Exception e)
        {
            var errMsg = e.Message;
        }
        try
        {
            const string cmdString = @"
                IF COL_LENGTH('AMS.UserAccessControl','UserId') IS NULL BEGIN
                    ALTER TABLE AMS.UserAccessControl ADD UserId INT DEFAULT 0
                END;
                IF COL_LENGTH('AMS.UserAccessControl','ConfigFormsXml') IS NULL BEGIN
                    ALTER TABLE AMS.UserAccessControl ADD ConfigFormsXml NVARCHAR(MAX)
                END;
                IF COL_LENGTH('AMS.GlobalCompany','ApiKey') IS NULL BEGIN
                    ALTER TABLE AMS.GlobalCompany ADD ApiKey UNIQUEIDENTIFIER
                END;
                IF COL_LENGTH('AMS.GlobalCompany','PrintingDesc') IS NULL BEGIN
                    ALTER TABLE AMS.GlobalCompany ADD PrintingDesc NVARCHAR(200)
                END;
                IF COL_LENGTH('AMS.ReportTemplate','ReportSource') IS NULL BEGIN
                   ALTER TABLE AMS.ReportTemplate add  ReportSource char(4) NULL;
                END;
                IF COL_LENGTH('AMS.ReportTemplate','ReportCategory') IS NULL BEGIN
                   ALTER TABLE AMS.ReportTemplate add ReportCategory NVARCHAR(50) NULL
                END;
                IF COL_LENGTH('AMS.ReportTemplate','FileName') IS NOT NULL BEGIN
                    ALTER TABLE AMS.ReportTemplate ALTER COLUMN FileName XML
                END;
                IF COL_LENGTH('AMS.BR_LOG','COMPANY') IS NOT NULL BEGIN
                    ALTER TABLE AMS.BR_LOG ALTER COLUMN COMPANY NVARCHAR(200)
                END;
                IF COL_LENGTH('AMS.FiscalYear','EndBSDate') IS NOT NULL BEGIN
                    UPDATE AMS.FiscalYear SET EndBSDate = '32/03/2079',EndADDate='2022-07-16' WHERE FiscalYearId = 12
                END;
                IF COL_LENGTH('AMS.ReportTemplate','FileName') IS NOT NULL BEGIN
                    ALTER TABLE AMS.ReportTemplate ALTER COLUMN FileName VARBINARY(MAX);
                END;";
            var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
            if (result <= 0)
            {
            }
        }
        catch
        {
            // ignore
        }

        try
        {
            const string cmdString = @"
            UPDATE AMS.UserInfo SET Category = 'ADMINISTRATOR'	WHERE User_Id IN (1,2) AND Category = 'NORMAL'";
            var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
            if (result <= 0)
            {
            }
        }
        catch
        {
            // ignore
        }
        try
        {
            const string cmd = @"
            IF(NOT EXISTS (SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS ifc WHERE TABLE_NAME='UserAccessControl' AND ifc.COLUMN_NAME='ConfigFormsXml' AND ifc.DATA_TYPE='XML'))BEGIN
                ALTER TABLE AMS.UserAccessControl ALTER COLUMN ConfigFormsXml XML;
            END;";
            var master = SqlExtensions.ExecuteNonQueryIgnoreExceptionMaster(cmd);
        }
        catch (Exception e)
        {

        }
    }
    public static void DefaultValueOnMasterDatabase()
    {
        try
        {
            const string cmdString = @"
				TRUNCATE TABLE AMS.FiscalYear;
                INSERT INTO AMS.FiscalYear(FiscalYearId, ADFiscalYear, BSFiscalYear, DefaultYear, StartADDate, EndADDate, StartBSDate, EndBSDate)
                VALUES
                    (1, '10/11', '67/68', 0, '2010-07-16','2011-07-15',  '01/04/2067', '32/03/2068'),
                    (2, '11/12', '68/69', 0, '2011-07-17', '2012-07-16', '01/04/2068', '32/03/2069'),
                    (3, '12/13', '69/70', 0, '2012-07-16', '2013-07-15', '01/04/2069', '31/03/2070'),
                    (4, '13/14', '70/71', 0, '2013-07-16', '2014-07-15', '01/04/2070', '31/03/2071'),
                    (5, '14/15', '71/72', 0, '2014-07-16', '2015-07-15', '01/04/2071', '31/03/2072'),
                    (6, '15/16', '72/73', 0, '2015-07-16', '2016-07-15', '01/04/2072', '31/03/2073'),
                    (7, '16/17', '73/74', 0, '2016-07-16', '2017-07-15', '01/04/2073', '31/03/2074'),
                    (8, '17/18', '74/75', 0, '2017-07-16', '2018-07-16', '01/04/2074', '31/03/2075'),
                    (9, '18/19', '75/76', 0, '2018-07-17', '2019-07-16', '01/04/2075', '31/03/2076'),
                    (10,'20/21', '76/77', 0, '2019-07-17', '2020-07-15', '01/04/2076', '31/03/2077'),
                    (11,'21/22', '77/78', 0, '2020-07-16', '2021-07-15', '01/04/2077', '31/03/2078'),
                    (12,'22/23', '78/79', 0, '2021-07-16', '2022-07-16', '01/04/2078', '32/03/2079'),
                    (13,'23/24', '79/80', 0, '2022-07-17', '2023-07-16', '01/04/2079', '31/03/2080'),
                    (14,'24/25', '80/81', 0, '2023-07-17', '2024-07-15', '01/04/2080', '31/03/2081'),
                    (15,'25/26', '81/82', 0, '2024-07-16', '2025-07-15', '01/04/2081', '31/03/2082'),
                    (16,'26/27', '82/83', 0, '2025-07-16', '2026-07-15', '01/04/2082', '31/03/2083'),
                    (17,'27/28', '83/84', 0, '2026-07-16', '2027-07-15', '01/04/2083', '31/03/2084'),
                    (18,'28/29', '84/85', 0, '2027-07-16', '2028-07-15', '01/04/2084', '31/03/2085'),
                    (19,'29/30', '85/86', 0, '2028-07-16', '2029-07-15', '01/04/2085', '31/03/2086'); ";
            var result = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
            if (result <= 0)
            {
            }
        }
        catch
        {
            // ignored
        }
        try
        {
            const string cmdString = @"
                IF NOT EXISTS (SELECT * FROM AMS.User_Role) BEGIN
                    INSERT INTO [AMS].[User_Role](Role, Status)
                    VALUES('Administrator', 1),('Admin', 1),('Normal User', 1);
                END; ";
            _ = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }
        try
        {
            const string cmdString = @"
				IF NOT EXISTS (SELECT * FROM AMS.UserInfo)BEGIN
                    INSERT INTO AMS.UserInfo(Full_Name, User_Name, Password, Address, Mobile_No, Phone_No, Email_Id, Role_Id, Branch_Id, Allow_Posting, Posting_StartDate, Posting_EndDate, Modify_StartDate, Modify_EndDate, Auditors_Lock, Valid_Days, Created_By, Created_Date, Status, Ledger_Id, Category, Authorized, IsQtyChange, IsDefault, IsModify, IsDeleted, IsRateChange, IsPDCDashBoard)
                    VALUES('BUSINESS AMS', 'AMSADMIN', 'yznXPFWt8Qut34mNXgKZqA==', 'KUPANDOLE', '9851193561', '01-4430676', 'info@mrsolution.com.np', 1, 0, 1, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, 5000, '1', GETDATE(), 1, NULL, 'NORMAL', 0, 0, 1, 0, 0, 0, 0),
                        ('Administrator', 'admin', 'yznXPFWt8Qut34mNXgKZqA==', 'KUPANDOLE', '9851193561', '01-4430676', 'info@mrsolution.com.np', 1, 0, 1, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, 5000, 1, GETDATE(), 1, NULL, 'NORMAL', 0, 0, 1, 0, 0, 0, 0),
                        ('MrSolution', 'MrSolution', 'yznXPFWt8Qut34mNXgKZqA==', 'KUPANDOLE', '9851193561', '01-4430676', 'info@mrsolution.com.np', 1, 0, 1, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, 5000, 1, GETDATE(), 1, NULL, 'NORMAL', 0, 0, 1, 0, 0, 0, 0),
                        ('MrDemo', 'MrDemo', 'yznXPFWt8Qut34mNXgKZqA==', 'KUPANDOLE', '9851193561', '01-4430676', 'info@mrsolution.com.np', 1, 0, 1, GETDATE(), GETDATE(), GETDATE(), GETDATE(), 0, 5000, 1, GETDATE(), 1, NULL, 'NORMAL', 0, 0, 1, 0, 0, 0, 0);
                END;";
            _ = SqlExtensions.ExecuteNonQueryOnMaster(cmdString);
        }
        catch
        {
            // ignored
        }
    }
    #endregion *--------------- Master Database ---------------*
}