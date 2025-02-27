--CREATE DATABASE CRM_MBL_BANK

USE CRM_MBL_BANK;
IF NOT EXISTS (SELECT s.name FROM sys.schemas s WHERE s.name='CRM')BEGIN
    DECLARE @AMS NVARCHAR(MAX);
    SET @AMS='CREATE SCHEMA CRM AUTHORIZATION dbo;';
    EXEC sys.sp_executesql @AMS;
END;
IF OBJECT_ID('CRM.Branch') IS NULL BEGIN
    CREATE TABLE [CRM].[Branch] ([BranchId] [INT] NOT NULL,
    [BranchDesc] [NVARCHAR](50) NOT NULL,
    [IsActive] [BIT] NOT NULL,
    [EnterBy] [NVARCHAR](50) NOT NULL,
    [EnterDate] [DATETIME] NOT NULL,
    [IsDefault] [BIT] NOT NULL,
    CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED([BranchId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Branch_Desc]
    ON [CRM].[Branch]([BranchDesc] ASC)
    WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON);
END;
IF OBJECT_ID('CRM.BranchRights') IS NULL BEGIN
    CREATE TABLE [CRM].[BranchRights] ([UserId] [INT] NOT NULL,
    [BranchId] [INT] NOT NULL,
    [EnterBy] [NVARCHAR](50) NULL,
    [EnterDate] [DATETIME] NULL) ON [PRIMARY];
    ALTER TABLE [CRM].[BranchRights] WITH CHECK
    ADD CONSTRAINT [FK_BranchRights_Branch] FOREIGN KEY([BranchId])REFERENCES [CRM].[Branch]([BranchId]);
    ALTER TABLE [CRM].[BranchRights] CHECK CONSTRAINT [FK_BranchRights_Branch];
END;
IF OBJECT_ID('CRM.BranchRights') IS NULL BEGIN
    CREATE TABLE [CRM].[CardServices] ([ServiceId] [INT] NOT NULL,
    [ServicesDesc] [NVARCHAR](255) NOT NULL,
    [EnterBy] [NVARCHAR](50) NOT NULL,
    [EnterDate] [DATETIME] NOT NULL,
    [IsActive] [BIT] NOT NULL,
    [IsDefault] [BIT] NOT NULL) ON [PRIMARY];
END;
IF OBJECT_ID('CRM.BranchRights') IS NULL BEGIN
    CREATE TABLE [CRM].[CreditCardHolder] ([CardHolderId] [BIGINT] IDENTITY(1, 1) NOT NULL,
    [CardHolderName] [NVARCHAR](255) NOT NULL,
    [CardHolderNumber] [NVARCHAR](16) NOT NULL,
    [AccountNumber] [NVARCHAR](50) NOT NULL,
    [ClientNumber] [NVARCHAR](50) NOT NULL,
    [ClientEmailAddress] [NVARCHAR](50) NOT NULL,
    [IsActive] [BIT] NOT NULL,
    [IsDefault] [BIT] NOT NULL,
    [EnterBy] [NVARCHAR](50) NOT NULL,
    [EnterDate] [DATETIME] NOT NULL,
    CONSTRAINT [PK_CreditCardHolder] PRIMARY KEY CLUSTERED([CardHolderId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
    CREATE UNIQUE NONCLUSTERED INDEX [IX_CreditCardHolder_Account]
    ON [CRM].[CreditCardHolder]([AccountNumber] ASC)
    WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON);
    CREATE UNIQUE NONCLUSTERED INDEX [IX_CreditCardHolder_CardNo]
    ON [CRM].[CreditCardHolder]([CardHolderNumber] ASC)
    WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, SORT_IN_TEMPDB=OFF, IGNORE_DUP_KEY=OFF, DROP_EXISTING=OFF, ONLINE=OFF, ALLOW_ROW_LOCKS=ON, ALLOW_PAGE_LOCKS=ON);
END;
IF OBJECT_ID('CRM.CreditCardHolder') IS NULL BEGIN
    CREATE TABLE [CRM].[CreditCardHolder] ([CardHolderId] [BIGINT] IDENTITY(1, 1) NOT NULL,
    [CardHolderName] [NVARCHAR](255) NOT NULL,
    [CardHolderNumber] [NVARCHAR](16) NOT NULL,
    [AccountNumber] [NVARCHAR](50) NOT NULL,
    [ClientNumber] [NVARCHAR](50) NOT NULL,
    [ClientEmailAddress] [NVARCHAR](50) NOT NULL,
    [IsActive] [BIT] NOT NULL,
    [IsDefault] [BIT] NOT NULL,
    [EnterBy] [NVARCHAR](50) NOT NULL,
    [EnterDate] [DATETIME] NOT NULL,
    CONSTRAINT [PK_CreditCardHolder] PRIMARY KEY CLUSTERED([CardHolderId] ASC)WITH(PAD_INDEX=OFF, STATISTICS_NORECOMPUTE=OFF, IGNORE_DUP_KEY=OFF, ALLOW_ROW_LOCKS= ON, ALLOW_PAGE_LOCKS=ON)ON [PRIMARY])ON [PRIMARY];
END;
IF OBJECT_ID('CRM.CreditCardHolder') IS NULL BEGIN
    CREATE TABLE [CRM].[TransactionLog] ([TranasctionId] [BIGINT] NOT NULL,
    [CardHolderId] [BIGINT] NOT NULL,
    [AgentId] [NVARCHAR](50) NULL,
    [BranchId] [INT] NULL,
    [BillReportDate] [DATETIME] NULL,
    [ServicesId] [INT] NULL) ON [PRIMARY];
END;