IF OBJECT_ID('AMS.BR_LOG') IS NULL BEGIN
	CREATE TABLE AMS.BR_LOG
		( [DB_NAME]	 [VARCHAR](50)  NOT NULL,
		  [COMPANY]	 [VARCHAR](50)  NOT NULL,
		  [LOCATION] [VARCHAR](200) NOT NULL,
		  [USED_BY]	 [VARCHAR](50)  NOT NULL,
		  [USED_ON]	 [DATETIME],
		  [ACTION]	 [VARCHAR](1) )
END;
IF OBJECT_ID('AMS.ReportTemplate') IS NULL BEGIN
	CREATE TABLE [AMS].[ReportTemplate]
		( [ID]			 [INT]			 IDENTITY (1, 1) NOT NULL,
		  [FileName]	 [NVARCHAR](MAX) NOT NULL,
		  [FullPath]	 [NVARCHAR](MAX) NOT NULL,
		  [FromDate]	 [DATETIME]		 NULL,
		  [ToDate]		 [DATETIME]		 NULL,
		  [Reports_Type] [char](1)		 NULL,
		  [Report_Name]	 [NVARCHAR](MAX) NULL,
		  CONSTRAINT [PK_ReportTemplate] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] )
	ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END;
IF COL_LENGTH('AMS.BR_LOG', 'SyncRowVersion') IS NULL BEGIN
	ALTER TABLE [AMS].[BR_LOG] ADD SyncRowVersion SMALLINT
END;
IF COL_LENGTH('AMS.BR_LOG', 'Log_ID') IS NULL BEGIN
	ALTER TABLE [AMS].[BR_LOG] ADD Log_ID INT
END;

IF COL_LENGTH('AMS.BR_LOG', 'STATUS') IS NULL BEGIN
	ALTER TABLE [AMS].[BR_LOG] ADD STATUS VARCHAR(50)
END;

IF COL_LENGTH('AMS.BR_LOG', 'COMPANY') IS NULL BEGIN
	ALTER TABLE [AMS].[BR_LOG] ALTER COLUMN [COMPANY] VARCHAR(200) NOT NULL
END;

IF COL_LENGTH('AMS.BR_LOG', 'LOCATION') IS NULL BEGIN
	ALTER TABLE AMS.BR_LOG ALTER COLUMN [LOCATION] NVARCHAR(1024);
END;

IF COL_LENGTH('AMS.BR_LOG', 'COMPANY') IS NULL BEGIN
	ALTER TABLE [AMS].[BR_LOG] ALTER COLUMN [COMPANY] NVARCHAR(325);
END;

IF OBJECT_ID('AMS.LOGIN_LOG') IS NULL BEGIN
	CREATE TABLE [AMS].[LOGIN_LOG]
		( OBJECT_ID		 BIGINT		   NOT NULL,
		  LOGIN_USER	 VARCHAR(50)   NOT NULL,
		  COMPANY		 NVARCHAR(200) NOT NULL,
		  LOGIN_DATABASE NVARCHAR(50)  NOT NULL,
		  DEVICE		 NVARCHAR(100),
		  DAVICE_MAC	 NVARCHAR(500),
		  DEVICE_IP		 NVARCHAR(500),
		  SYSTEM_ID		 NVARCHAR(500),
		  LOGIN_DATE	 DATETIME )
END;

IF COL_LENGTH('AMS.LOGIN_LOG', 'LOG_STATUS') IS NULL BEGIN
	ALTER TABLE AMS.LOGIN_LOG ADD LOG_STATUS INT NULL
END;

IF COL_LENGTH('AMS.UserInfo', 'IsQtyChange') IS NULL BEGIN
	ALTER TABLE AMS.UserInfo ADD IsQtyChange BIT
END;
IF COL_LENGTH('AMS.UserInfo', 'IsDefault') IS NULL BEGIN
	ALTER TABLE AMS.UserInfo ADD IsDefault BIT
END;
IF COL_LENGTH('AMS.UserInfo', 'IsModify') IS NULL BEGIN
	ALTER TABLE AMS.UserInfo ADD IsModify BIT
END;
IF COL_LENGTH('AMS.UserInfo', 'IsDeleted') IS NULL BEGIN
	ALTER TABLE AMS.UserInfo ADD IsDeleted BIT
END;
IF COL_LENGTH('AMS.UserInfo', 'IsRateChange') IS NULL BEGIN
	ALTER TABLE AMS.UserInfo ADD IsRateChange BIT
END;
IF COL_LENGTH('AMS.UserInfo', 'IsPDCDashBoard') IS NULL BEGIN
	ALTER TABLE AMS.UserInfo ADD IsPDCDashBoard BIT
END;
IF COL_LENGTH('AMS.UserInfo', 'Created_By') IS NOT NULL BEGIN
	ALTER TABLE AMS.UserInfo ALTER COLUMN Created_By NVARCHAR(50) NOT NULL
END;

IF COL_LENGTH('AMS.DataServerInfo', 'ClServerUser') IS NULL BEGIN
	ALTER TABLE AMS.DataServerInfo ADD ClServerUser NVARCHAR(MAX)
END;
IF COL_LENGTH('AMS.DataServerInfo', 'ClServerPassword') IS NULL BEGIN
	ALTER TABLE AMS.DataServerInfo ADD ClServerPassword NVARCHAR(MAX)
END;
IF COL_LENGTH('AMS.DataServerInfo', 'InitialCatelog') IS NULL BEGIN
	ALTER TABLE AMS.DataServerInfo ADD InitialCatelog NVARCHAR(MAX)
END;

IF COL_LENGTH('AMS.GlobalCompany', 'LoginDate') IS NULL BEGIN
	ALTER TABLE AMS.GlobalCompany ADD LoginDate DATETIME
END;
IF COL_LENGTH('AMS.GlobalCompany', 'DataSyncOriginId') IS NULL BEGIN
	ALTER TABLE master.AMS.GlobalCompany ADD DataSyncOriginId UNIQUEIDENTIFIER NULL
END;
IF COL_LENGTH('AMS.GlobalCompany', 'DataSyncApiBaseUrl') IS NULL BEGIN
	ALTER TABLE master.AMS.GlobalCompany ADD DataSyncApiBaseUrl NVARCHAR(MAX) NULL
END;

IF COL_LENGTH('AMS.GlobalCompany', 'DataSyncApiBaseUrl') IS NULL BEGIN
	ALTER TABLE AMS.GlobalCompany ALTER COLUMN DataSyncApiBaseUrl NVARCHAR(MAX)
END;
IF COL_LENGTH('AMS.GlobalCompany', 'DataSyncOriginId') IS NOT NULL BEGIN
	ALTER TABLE AMS.GlobalCompany ALTER COLUMN DataSyncOriginId NVARCHAR(MAX)
END;