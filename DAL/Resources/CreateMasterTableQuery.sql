IF NOT EXISTS (SELECT s.name FROM sys.schemas s WHERE s.name='AMS')BEGIN
    DECLARE @AMS NVARCHAR(MAX);
    SET @AMS=N'CREATE SCHEMA AMS AUTHORIZATION dbo;';
    EXEC sys.sp_executesql @AMS;
END;
IF OBJECT_ID('AMS.BR_LOG') IS NULL BEGIN
    CREATE TABLE [AMS].[BR_LOG] ([DB_NAME] [VARCHAR](50)  NOT NULL,
        [COMPANY]                          [VARCHAR](50)  NOT NULL,
        [LOCATION]                         [VARCHAR](200) NOT NULL,
        [USED_BY]                          [VARCHAR](50)  NOT NULL,
        [USED_ON]                          [DATETIME]     NULL,
        [ACTION]                           [VARCHAR](1)   NULL,
        [SyncRowVersion]                   [SMALLINT]     NULL,
        [Log_ID]                           [INT]          NULL,
        [STATUS]                           [VARCHAR](50)  NULL) ON [PRIMARY];
END;
IF OBJECT_ID('AMS.GlobalCompany') IS NULL BEGIN
    CREATE TABLE [AMS].[GlobalCompany] ([GComp_Id] [INT]              IDENTITY(1, 1) NOT NULL,
        [Database_Name]                            [NVARCHAR](255)    NOT NULL,
        [Company_Name]                             [VARCHAR](255)     NOT NULL,
        [PrintingDesc]                             [VARCHAR](255)     NULL,
        [Database_Path]                            [NVARCHAR](255)    NULL,
        [Status]                                   [BIT]              NULL,
        [PanNo]                                    [NVARCHAR](50)     NULL,
        [Address]                                  [NVARCHAR](500)    NULL,
        [CurrentFiscalYear]                        [NVARCHAR](50)     NULL,
        [LoginDate]                                [DATETIME]         NULL,
        [DataSyncOriginId]                         [NVARCHAR](MAX)    NULL,
        [DataSyncApiBaseUrl]                       [NVARCHAR](MAX)    NULL,
        [ApiKey]                                   [UNIQUEIDENTIFIER] NULL,
        CONSTRAINT [PK_GlobalCompany] PRIMARY KEY CLUSTERED([GComp_Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY],
        CONSTRAINT [IX_GlobalCompany] UNIQUE NONCLUSTERED([Company_Name] ASC, [Database_Name] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
END;
IF OBJECT_ID('AMS.FiscalYear') IS NULL BEGIN
    CREATE TABLE [AMS].[FiscalYear] ([FiscalYearId] [INT]         NOT NULL,
        [ADFiscalYear]                              [VARCHAR](5)  NOT NULL,
        [BSFiscalYear]                              [VARCHAR](5)  NOT NULL,
        [DefaultYear]                               [BIT]         NOT NULL,
        [StartADDate]                               [DATETIME]    NOT NULL,
        [EndADDate]                                 [DATETIME]    NOT NULL,
        [StartBSDate]                               [VARCHAR](10) NOT NULL,
        [EndBSDate]                                 [VARCHAR](10) NOT NULL,
        CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED([FiscalYearId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY],
        CONSTRAINT [IX_FiscalYear] UNIQUE NONCLUSTERED([ADFiscalYear] ASC, [BSFiscalYear] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('AMS.User_Role') IS NULL BEGIN
    CREATE TABLE [AMS].[User_Role] ([Role_Id] [INT]         IDENTITY(1, 1) NOT NULL,
        [Role]                                [VARCHAR](50) NOT NULL,
        [Status]                              [BIT]         NULL,
        CONSTRAINT [PK_User_Role] PRIMARY KEY CLUSTERED([Role_Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY],
        CONSTRAINT [IX_User_Role] UNIQUE NONCLUSTERED([Role] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('AMS.UserInfo') IS NULL BEGIN
    CREATE TABLE [AMS].[UserInfo] ([User_Id] [INT]           IDENTITY(1, 1) NOT NULL,
        [Full_Name]                          [VARCHAR](255)  NULL,
        [User_Name]                          [VARCHAR](50)   NOT NULL,
        [Password]                           [NVARCHAR](555) NULL,
        [Address]                            [VARCHAR](255)  NULL,
        [Mobile_No]                          [VARCHAR](50)   NULL,
        [Phone_No]                           [NVARCHAR](50)  NULL,
        [Email_Id]                           [NVARCHAR](255) NULL,
        [Role_Id]                            [INT]           NULL,
        [Branch_Id]                          [INT]           NULL,
        [Allow_Posting]                      [BIT]           NULL,
        [Posting_StartDate]                  [DATE]          NULL,
        [Posting_EndDate]                    [DATE]          NULL,
        [Modify_StartDate]                   [DATE]          NULL,
        [Modify_EndDate]                     [DATE]          NULL,
        [Auditors_Lock]                      [BIT]           NOT NULL,
        [Valid_Days]                         [INT]           NOT NULL,
        [Created_By]                         [NVARCHAR](50)  NOT NULL,
        [Created_Date]                       [DATETIME]      NOT NULL,
        [Status]                             [BIT]           NULL,
        [Ledger_Id]                          [BIGINT]        NULL,
        [Category]                           [NVARCHAR](50)  NULL,
        [Authorized]                         [BIT]           NULL,
        [IsQtyChange]                        [BIT]           NULL,
        [IsDefault]                          [BIT]           NULL,
        [IsModify]                           [BIT]           NULL,
        [IsDeleted]                          [BIT]           NULL,
        [IsRateChange]                       [BIT]           NULL,
        [IsPDCDashBoard]                     [BIT]           NULL,
        CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED([User_Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
    ALTER TABLE [AMS].[UserInfo] WITH CHECK
    ADD CONSTRAINT [FK_UserInfo_User_Role] FOREIGN KEY([Role_Id])REFERENCES [AMS].[User_Role]([Role_Id]);
    ALTER TABLE [AMS].[UserInfo] CHECK CONSTRAINT [FK_UserInfo_User_Role];
END;
IF OBJECT_ID('AMS.UserAccessControl') IS NULL BEGIN
    CREATE TABLE [AMS].[UserAccessControl] ([Id] [INT] IDENTITY(1, 1) NOT NULL,
    [UserRoleId] [INT] NOT NULL,
    [FeatureAlias] [INT] NOT NULL,
    [BranchId] [INT] NULL,
    [IsValid] [BIT] NOT NULL,
    [CreatedOn] [DATETIME] NOT NULL,
    [ModifiedOn] [DATETIME] NULL,
    [UpdatedBy] [NVARCHAR](50) NULL,
    [ConfigXml] [XML] NULL,
    [UserId] [INT] NULL,
    [ConfigFormsXml] [XML] NULL,
    CONSTRAINT [PK_UserAccessControl] PRIMARY KEY CLUSTERED([Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
    ALTER TABLE [AMS].[UserAccessControl] WITH CHECK
    ADD CONSTRAINT [FK_UserAccessControl_User_Role] FOREIGN KEY([UserRoleId])REFERENCES [AMS].[User_Role]([Role_Id]);
    ALTER TABLE [AMS].[UserAccessControl] CHECK CONSTRAINT [FK_UserAccessControl_User_Role];
END;
IF OBJECT_ID('AMS.ReportTemplate') IS NULL BEGIN
    CREATE TABLE [AMS].[ReportTemplate] ([ID] [INT]          IDENTITY(1, 1) NOT NULL,
        [Report_Name]                         [VARCHAR](255) NULL,
        [Reports_Type]                        [CHAR](1)      NOT NULL,
        [FileName]                            [VARCHAR](255) NOT NULL,
        [FullPath]                            [VARCHAR](255) NOT NULL,
        [FromDate]                            [DATETIME]     NULL,
        [ToDate]                              [DATETIME]     NULL,
        CONSTRAINT [PK_ReportTemplate] PRIMARY KEY CLUSTERED([ID] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('AMS.SoftwareRegistration') IS NULL BEGIN
    CREATE TABLE [AMS].[SoftwareRegistration] ([RegistrationId] [INT]           NOT NULL,
        [CustomerId]                                            [NVARCHAR](512) NOT NULL,
        [Server_MacAdd]                                         [NVARCHAR](512) NULL,
        [Server_Desc]                                           [NVARCHAR](512) NULL,
        [ClientDescription]                                     [NVARCHAR](512) NULL,
        [ClientAddress]                                         [NVARCHAR](512) NULL,
        [ClientSerialNo]                                        [NVARCHAR](512) NULL,
        [Reguestby]                                             [NVARCHAR](512) NULL,
        [RegisterBy]                                            [NVARCHAR](512) NULL,
        [RegistrationDate]                                      [NVARCHAR](512) NULL,
        [RegistrationDays]                                      [NVARCHAR](512) NULL,
        [ExpiredDate]                                           [NVARCHAR](MAX) NULL,
        [ProductDescription]                                    [NVARCHAR](512) NULL,
        [NoOfNodes]                                             [NVARCHAR](512) NULL,
        [Module]                                                [NVARCHAR](512) NULL,
        [System_Id]                                             [NVARCHAR](512) NULL,
        [ActivationCode]                                        [NVARCHAR](512) NULL,
        [IsOnline]                                              [BIT]           NULL,
        CONSTRAINT [PK_SoftwareRegistration] PRIMARY KEY CLUSTERED([RegistrationId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
END;
IF OBJECT_ID('AMS.PrintDocument_Designer') IS NULL BEGIN
    CREATE TABLE [AMS].[PrintDocument_Designer] ([DocDesigner_Id] [INT]            IDENTITY(1, 1) NOT NULL,
        [Designerpaper_Name]                                      [VARCHAR](250)   NOT NULL,
        [Station]                                                 [VARCHAR](10)    NOT NULL,
        [Type]                                                    [VARCHAR](50)    NOT NULL,
        [Paths]                                                   [NVARCHAR](525)  NOT NULL,
        [Paper_Size]                                              [INT]            NULL,
        [Orientation]                                             [INT]            NULL,
        [Height]                                                  [DECIMAL](20, 4) NULL,
        [Width]                                                   [DECIMAL](20, 4) NULL,
        [Margin_Left]                                             [DECIMAL](20, 4) NULL,
        [Margin_Right]                                            [DECIMAL](20, 4) NULL,
        [Margin_Top]                                              [DECIMAL](20, 4) NULL,
        [Margin_Bottom]                                           [DECIMAL](20, 4) NULL,
        CONSTRAINT [PK_PrintDocument_Designer] PRIMARY KEY CLUSTERED([DocDesigner_Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('AMS.Menu_Rights') IS NULL BEGIN
    CREATE TABLE [AMS].[Menu_Rights] ([MR_Id] [INT]           IDENTITY(1, 1) NOT NULL,
        [Role_Id]                             [INT]           NULL,
        [UserId]                              [INT]           NULL,
        [Menu_Id]                             [INT]           NULL,
        [Menu_Code]                           [NVARCHAR](255) NULL,
        [Menu_Name]                           [NVARCHAR](255) NULL,
        [Form_Name]                           [NVARCHAR](255) NULL,
        [Module_Id]                           [INT]           NULL,
        [SubModule_Id]                        [INT]           NULL,
        [New]                                 [BIT]           NULL,
        [Save]                                [BIT]           NULL,
        [Update]                              [BIT]           NULL,
        [Delete]                              [BIT]           NULL,
        [Copy]                                [BIT]           NULL,
        [Search]                              [BIT]           NULL,
        [Print]                               [BIT]           NULL,
        [Approved]                            [BIT]           NULL,
        [Reverse]                             [BIT]           NULL,
        [Isparent]                            [BIT]           NULL,
        [Created_By]                          [INT]           NULL,
        [Created_Date]                        [DATETIME]      NULL,
        [Branch_Id]                           [INT]           NULL,
        [FiscalYear_Id]                       [INT]           NULL) ON [PRIMARY];
END;
IF OBJECT_ID('AMS.Menu') IS NULL BEGIN
    CREATE TABLE [AMS].[Menu] ([Menu_Id] [BIGINT]       IDENTITY(1, 1) NOT NULL,
        [Menu_Name]                      [VARCHAR](255) NULL,
        [Form_Code]                      [VARCHAR](255) NULL,
        [Form_Name]                      [VARCHAR](255) NULL,
        [Mast_Menu_Id]                   [BIGINT]       NULL,
        [Level]                          [INT]          NULL,
        [Sn]                             [INT]          NULL,
        [Status]                         [BIT]          NULL,
        [Free]                           [VARCHAR](5)   NULL,
        [Basic]                          [VARCHAR](5)   NULL,
        [Professional]                   [VARCHAR](5)   NULL,
        [Business]                       [VARCHAR](5)   NULL,
        [ERP]                            [VARCHAR](5)   NULL,
        [POS]                            [VARCHAR](5)   NULL,
        [Restaurent]                     [VARCHAR](5)   NULL,
        [Hotel]                          [VARCHAR](5)   NULL,
        [Pharmacy]                       [VARCHAR](5)   NULL,
        [Developer]                      [VARCHAR](5)   NULL) ON [PRIMARY];
END;
IF OBJECT_ID('AMS.DateMiti') IS NULL BEGIN
    CREATE TABLE [AMS].[DateMiti] ([Date_Id] [BIGINT]        NOT NULL,
        [BS_Date]                            [VARCHAR](10)   NULL,
        [BS_DateDMY]                         [VARCHAR](10)   NULL,
        [AD_Date]                            [DATETIME]      NULL,
        [BS_Months]                          [VARCHAR](50)   NULL,
        [AD_Months]                          [VARCHAR](50)   NULL,
        [Days]                               [VARCHAR](50)   NULL,
        [Is_Holiday]                         [BIT]           NULL,
        [Holiday]                            [VARCHAR](1024) NULL,
        CONSTRAINT [PK_DateMiti] PRIMARY KEY CLUSTERED([Date_Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY],
        CONSTRAINT [IX_DateMiti_ADDate] UNIQUE NONCLUSTERED([AD_Date] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY],
        CONSTRAINT [IX_DateMiti_BsDate] UNIQUE NONCLUSTERED([BS_Date] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY],
        CONSTRAINT [IX_DateMiti_BsDateDMY] UNIQUE NONCLUSTERED([BS_DateDMY] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('AMS.DataServerInfo') IS NULL BEGIN
    CREATE TABLE [AMS].[DataServerInfo] ([DSId] [INT]           IDENTITY(1, 1) NOT NULL,
        [ServerName]                            [VARCHAR](50)   NULL,
        [UserName]                              [VARCHAR](50)   NULL,
        [Password]                              [VARCHAR](50)   NULL,
        [MultipleDatasource]                    [BIT]           NULL,
        [ClServerUser]                          [NVARCHAR](MAX) NULL,
        [ClServerPassword]                      [NVARCHAR](MAX) NULL,
        [InitialCatelog]                        [NVARCHAR](MAX) NULL,
        CONSTRAINT [PK_DataServerInfo] PRIMARY KEY CLUSTERED([DSId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
END;
IF OBJECT_ID('AMS.LicenseInfo') IS NULL BEGIN
    CREATE TABLE [AMS].[LicenseInfo] ([AMSLId] [INT]           IDENTITY(1, 1) NOT NULL,
        [Created_Date]                         [NVARCHAR](255) NULL,
        [Reg_No]                               [VARCHAR](20)   NULL,
        [License_No]                           [NVARCHAR](555) NULL,
        [LicenseRenew_Date]                    [NVARCHAR](255) NULL,
        [IAgreeToRTOSandPP]                    [BIT]           NULL,
        CONSTRAINT [PK_LicenseInfo] PRIMARY KEY CLUSTERED([AMSLId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('AMS.LOGIN_LOG') IS NULL BEGIN
    CREATE TABLE [AMS].[LOGIN_LOG] ([OBJECT_ID] [BIGINT]        NOT NULL,
        [LOGIN_USER]                            [VARCHAR](50)   NOT NULL,
        [COMPANY]                               [NVARCHAR](200) NOT NULL,
        [LOGIN_DATABASE]                        [NVARCHAR](50)  NOT NULL,
        [DEVICE]                                [NVARCHAR](100) NULL,
        [DAVICE_MAC]                            [NVARCHAR](500) NULL,
        [DEVICE_IP]                             [NVARCHAR](500) NULL,
        [SYSTEM_ID]                             [NVARCHAR](500) NULL,
        [LOGIN_DATE]                            [DATETIME]      NULL,
        [LOG_STATUS]                            [INT]           NULL) ON [PRIMARY];
END;
IF OBJECT_ID('AMS.CompanyRights') IS NULL BEGIN
    CREATE TABLE [AMS].[CompanyRights] ([CompanyRights_Id] [INT]          IDENTITY(1, 1) NOT NULL,
        [User_Id]                                          [INT]          NOT NULL,
        [Company_Id]                                       [INT]          NOT NULL,
        [Company_Name]                                     [VARCHAR](255) NOT NULL,
        [Start_AdDate]                                     [DATETIME]     NULL,
        [End_AdDate]                                       [DATETIME]     NULL,
        [Modify_Start_AdDate]                              [DATETIME]     NULL,
        [Modify_End_AdDate]                                [DATETIME]     NULL,
        [Back_Days_Restriction]                            [BIT]          NULL,
        CONSTRAINT [PK_CompanyRights] PRIMARY KEY CLUSTERED([CompanyRights_Id] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
    ALTER TABLE [AMS].[CompanyRights] WITH CHECK
    ADD CONSTRAINT [FK_CompanyRights_GlobalCompany] FOREIGN KEY([Company_Id])REFERENCES [AMS].[GlobalCompany]([GComp_Id]);
    ALTER TABLE [AMS].[CompanyRights] CHECK CONSTRAINT [FK_CompanyRights_GlobalCompany];
    ALTER TABLE [AMS].[CompanyRights] WITH CHECK
    ADD CONSTRAINT [FK_CompanyRights_UserInfo] FOREIGN KEY([User_Id])REFERENCES [AMS].[UserInfo]([User_Id]);
    ALTER TABLE [AMS].[CompanyRights] CHECK CONSTRAINT [FK_CompanyRights_UserInfo];
END;
IF OBJECT_ID('AMS.SyncApiErrorLog') IS NULL BEGIN
   CREATE TABLE [AMS].[SyncApiErrorLog](
	[Id]            [INT] IDENTITY(1,1) NOT NULL, 
	[Module]        [VARCHAR](50) NULL,
	[Created]       [DATETIME2](7) NULL,
	[ErrorMessage]  [NVARCHAR](500) NULL,
	[ErrorType]     [NVARCHAR](50) NULL,
 CONSTRAINT [PK_SyncApiErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END;