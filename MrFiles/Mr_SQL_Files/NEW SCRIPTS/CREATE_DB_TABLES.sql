IF OBJECT_ID('AMS.AccountDetails') IS NULL
CREATE TABLE [AMS].[AccountDetails] (
[Module] [char](10) NOT NULL
,[Serial_No] [INT] NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [NVARCHAR](50) NOT NULL
,[Voucher_Time] [DATETIME] NOT NULL
,[CbLedger_ID] [BIGINT] NULL
,[Subleder_ID] [INT] NULL
,[Agent_ID] [INT] NULL
,[Department_ID1] [INT] NULL
,[Department_ID2] [INT] NULL
,[Department_ID3] [INT] NULL
,[Department_ID4] [INT] NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [DECIMAL](18, 6) NOT NULL
,[Debit_Amt] [DECIMAL](18, 6) NOT NULL
,[Credit_Amt] [DECIMAL](18, 6) NOT NULL
,[LocalDebit_Amt] [DECIMAL](18, 6) NOT NULL
,[LocalCredit_Amt] [DECIMAL](18, 6) NOT NULL
,[DueDate] [DATETIME] NULL
,[DueDays] [INT] NULL
,[Narration] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[RefNo] [NVARCHAR](50) NULL
,[RefDate] [NVARCHAR](50) NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Authorize_By] [VARCHAR](50) NULL
,[Authorize_Date] [DATETIME] NULL
,[Clearing_By] [NVARCHAR](50) NULL
,[Clearing_Date] [DATETIME] NULL
,[Posted_By] [VARCHAR](50) NULL
,[Posted_Date] [DATETIME] NULL
,[Cheque_No] [NVARCHAR](50) NULL
,[Cheque_Date] [DATETIME] NULL
,[Cheque_Miti] [NVARCHAR](50) NULL
,[PartyName] [NVARCHAR](100) NULL
,[PartyLedger_Id] [INT] NULL
,[Party_PanNo] [NVARCHAR](50) NULL
,[CmpUnit_ID] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[DoctorId] [INT] NULL
,[PatientId] [BIGINT] NULL
,[HDepartmentId] [INT] NULL
,[Branch_ID] [INT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AccountGroup') IS NULL
CREATE TABLE [AMS].[AccountGroup] (
[GrpId] [INT] NOT NULL
,[PrimaryGroupId] [INT] NULL CONSTRAINT [DF__AccountGr__Prima__22A65A4F] DEFAULT ((0))
,[GrpName] [NVARCHAR](200) NOT NULL
,[GrpCode] [NVARCHAR](50) NOT NULL
,[Schedule] [INT] NOT NULL CONSTRAINT [DF_AccountGroup_Schedule] DEFAULT ((1))
,[PrimaryGrp] [NVARCHAR](50) NOT NULL
,[GrpType] [NVARCHAR](50) NOT NULL
,[IsDefault] [char](1) NULL CONSTRAINT [DF__AccountGr__IsDef__239A7E88] DEFAULT ((0))
,[NepaliDesc] [NVARCHAR](200) NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL CONSTRAINT [DF_AccountGroup_Status] DEFAULT ((1))
,[EnterBy] [NVARCHAR](50) NOT NULL CONSTRAINT [DF_AccountGroup_EnterBy] DEFAULT (N'MrSolution')
,[EnterDate] [DATETIME] NOT NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,CONSTRAINT [PK_GROUP_ID] PRIMARY KEY CLUSTERED ([GrpId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [GROUP_DESC] UNIQUE NONCLUSTERED ([GrpName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [GROUP_SHORTNAME] UNIQUE NONCLUSTERED ([GrpCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AccountSubGroup') IS NULL
CREATE TABLE [AMS].[AccountSubGroup] (
[SubGrpId] [INT] NOT NULL
,[SubGrpName] [NVARCHAR](200) NOT NULL
,[Company_Id] [INT] NULL
,[PrimaryGroupId] [INT] NULL CONSTRAINT [DF__AccountSu__Prima__248EA2C1] DEFAULT ((0))
,[PrimarySubGroupId] [INT] NULL CONSTRAINT [DF__AccountSu__Prima__2582C6FA] DEFAULT ((0))
,[IsDefault] [char](1) NULL CONSTRAINT [DF__AccountSu__IsDef__2676EB33] DEFAULT ((0))
,[NepaliDesc] [NVARCHAR](200) NULL
,[GrpId] [INT] NOT NULL
,[SubGrpCode] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Status] [BIT] NOT NULL CONSTRAINT [DF_AccountSubGroup_Status] DEFAULT ((1))
,[EnterBy] [NVARCHAR](50) NOT NULL CONSTRAINT [DF_AccountSubGroup_EnterBy] DEFAULT (N'MrSolution')
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_AccountSubGroup] PRIMARY KEY CLUSTERED ([SubGrpId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_AccountSubGroup] UNIQUE NONCLUSTERED ([SubGrpName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Area') IS NULL
CREATE TABLE [AMS].[Area] (
[AreaId] [INT] NOT NULL
,[AreaName] [NVARCHAR](150) NOT NULL
,[AreaCode] [NVARCHAR](50) NOT NULL
,[Country] [NVARCHAR](50) NOT NULL
,[Main_Area] [INT] NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED ([AreaId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_AreaDesc] UNIQUE NONCLUSTERED ([AreaName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_AreaShortName] UNIQUE NONCLUSTERED ([AreaCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AUDIT_PB_OtherMaster') IS NULL
CREATE TABLE [AMS].[AUDIT_PB_OtherMaster] (
[PAB_Invoice] [NVARCHAR](50) NOT NULL
,[PPNo] [NVARCHAR](50) NOT NULL
,[PPDate] [DATETIME] NOT NULL
,[TaxableAmount] [DECIMAL](18, 6) NOT NULL
,[VatAmount] [DECIMAL](18, 6) NOT NULL
,[CustomAgent] [NVARCHAR](50) NULL
,[Transportation] [NVARCHAR](50) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[Cn_No] [VARCHAR](25) NULL
,[Cn_Date] [DATETIME] NULL
,[BankDoc] [NVARCHAR](50) NULL
,[ModifyAction] [NVARCHAR](10) NULL
,[ModifyBy] [NVARCHAR](50) NULL
,[ModifyDate] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogAccountGroup') IS NULL
CREATE TABLE [AMS].[AuditLogAccountGroup] (
[GrpId] [INT] NOT NULL
,[GrpName] [NVARCHAR](200) NULL
,[GrpCode] [NVARCHAR](50) NULL
,[Schedule] [INT] NULL
,[PrimaryGrp] [NVARCHAR](50) NULL
,[GrpType] [NVARCHAR](50) NULL
,[Company_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_AuditLogAccountGroup] PRIMARY KEY CLUSTERED ([GrpId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogAccountSubGroup') IS NULL
CREATE TABLE [AMS].[AuditLogAccountSubGroup] (
[SubGrpId] [INT] NOT NULL
,[SubGrpName] [NVARCHAR](200) NULL
,[SubGrpCode] [NVARCHAR](50) NULL
,[Company_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,[GrpId] [INT] NOT NULL
,[Branch_ID] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_AuditLogAccountSubGroup] PRIMARY KEY CLUSTERED ([GrpId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogArea') IS NULL
CREATE TABLE [AMS].[AuditLogArea] (
[AreaId] [INT] NOT NULL
,[AreaName] [NVARCHAR](150) NULL
,[AreaCode] [NVARCHAR](50) NULL
,[Country] [NVARCHAR](50) NULL
,[Company_Id] [INT] NULL
,[Main_Area] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogBranch') IS NULL
CREATE TABLE [AMS].[AuditLogBranch] (
[Branch_ID] [INT] NOT NULL
,[Branch_Name] [NVARCHAR](200) NULL
,[Branch_Code] [NVARCHAR](50) NULL
,[Reg_Date] [DATETIME] NULL
,[Address] [NVARCHAR](500) NULL
,[Country] [NVARCHAR](50) NULL
,[State] [NVARCHAR](50) NULL
,[City] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Fax] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](80) NULL
,[ContactPerson] [NVARCHAR](50) NULL
,[ContactPersonAdd] [NVARCHAR](50) NULL
,[ContactPersonPhone] [NVARCHAR](50) NULL
,[Created_By] [NVARCHAR](50) NULL
,[Created_Date] [DATETIME] NULL
,[Modify_By] [NVARCHAR](50) NULL
,[Modify_Date] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,CONSTRAINT [PK_AuditLogBranch] PRIMARY KEY CLUSTERED ([Branch_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCB_Details') IS NULL
CREATE TABLE [AMS].[AuditLogCB_Details] (
[Voucher_No] [NVARCHAR](50) NULL
,[SNo] [INT] NULL
,[Ledger_ID] [BIGINT] NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Debit] [DECIMAL](18, 6) NULL
,[Credit] [DECIMAL](18, 6) NULL
,[LocalDebit] [DECIMAL](18, 6) NULL
,[LocalCredit] [DECIMAL](18, 6) NULL
,[Narration] [VARCHAR](1024) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [BIGINT] NULL
,[PanNo] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCB_Master') IS NULL
CREATE TABLE [AMS].[AuditLogCB_Master] (
[VoucherMode] [char](10) NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NULL
,[Voucher_Miti] [NVARCHAR](10) NULL
,[Voucher_Time] [DATETIME] NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[VoucherType] [NVARCHAR](50) NULL
,[Ledger_ID] [BIGINT] NULL
,[CheqNo] [NVARCHAR](50) NULL
,[CheqDate] [DATETIME] NULL
,[CheqMiti] [NVARCHAR](10) NULL
,[Currency_ID] [INT] NULL
,[Currency_Rate] [DECIMAL](18, 6) NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[ClearingDate] [DATETIME] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[PrintValue] [INT] NULL
,[CBranch_Id] [INT] NULL
,[CUnit_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCompanyInfo') IS NULL
CREATE TABLE [AMS].[AuditLogCompanyInfo] (
[Company_Id] [TINYINT] IDENTITY (1, 1) NOT NULL
,[Company_Name] [NVARCHAR](200) NULL
,[Company_Logo] [IMAGE] NULL
,[CReg_Date] [DATETIME] NULL
,[Address] [NVARCHAR](200) NULL
,[Country] [NVARCHAR](50) NULL
,[State] [NVARCHAR](50) NULL
,[City] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Fax] [NVARCHAR](50) NULL
,[Pan_No] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](50) NULL
,[Website] [NVARCHAR](100) NULL
,[Database_Name] [NVARCHAR](50) NULL
,[Database_Path] [NVARCHAR](100) NULL
,[Version_No] [TINYINT] NULL
,[Status] [BIT] NULL
,[CreateDate] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCompanyUnit') IS NULL
CREATE TABLE [AMS].[AuditLogCompanyUnit] (
[CmpUnit_ID] [INT] NOT NULL
,[CmpUnit_Name] [NVARCHAR](200) NULL
,[CmpUnit_Code] [NVARCHAR](50) NULL
,[Reg_Date] [DATETIME] NULL
,[Address] [NVARCHAR](50) NULL
,[Country] [NVARCHAR](50) NULL
,[State] [NVARCHAR](50) NULL
,[City] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Fax] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](90) NULL
,[ContactPerson] [NVARCHAR](50) NULL
,[ContactPersonAdd] [NVARCHAR](50) NULL
,[ContactPersonPhone] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Created_By] [NVARCHAR](50) NULL
,[Created_Date] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,CONSTRAINT [PK_AuditLogCompanyUnit] PRIMARY KEY CLUSTERED ([CmpUnit_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCostCenter') IS NULL
CREATE TABLE [AMS].[AuditLogCostCenter] (
[CCId] [INT] NOT NULL
,[CCName] [NVARCHAR](200) NULL
,[CCcode] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,CONSTRAINT [PK_AuditLogCostCenter] PRIMARY KEY CLUSTERED ([CCId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCounter') IS NULL
CREATE TABLE [AMS].[AuditLogCounter] (
[CId] [INT] NOT NULL
,[CName] [NVARCHAR](50) NULL
,[CCode] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,CONSTRAINT [PK_AuditLogCounter] PRIMARY KEY CLUSTERED ([CId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogCurrency') IS NULL
CREATE TABLE [AMS].[AuditLogCurrency] (
[CId] [INT] NOT NULL
,[CName] [NVARCHAR](100) NULL
,[CCode] [NVARCHAR](50) NULL
,[CRate] [NUMERIC](18, 6) NULL
,[Company_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_AuditLogCurrency] PRIMARY KEY CLUSTERED ([CId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogDepartment') IS NULL
CREATE TABLE [AMS].[AuditLogDepartment] (
[DId] [INT] NOT NULL
,[DName] [NVARCHAR](50) NOT NULL
,[DCode] [NVARCHAR](50) NOT NULL
,[Dlevel] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogDocumentNumbering') IS NULL
CREATE TABLE [AMS].[AuditLogDocumentNumbering] (
[DocId] [INT] NOT NULL
,[DocModule] [NVARCHAR](50) NULL
,[DocUser] [NVARCHAR](50) NULL
,[DocDesc] [NVARCHAR](50) NULL
,[DocStartDate] [Date] NULL
,[DocStartMiti] [VARCHAR](10) NULL
,[DocEndDate] [Date] NULL
,[DocEndMiti] [VARCHAR](10) NULL
,[DocType] [char](10) NULL
,[DocPrefix] [NVARCHAR](50) NULL
,[DocSufix] [NVARCHAR](50) NULL
,[DocBodyLength] [NUMERIC](18, 0) NULL
,[DocTotalLength] [NUMERIC](18, 0) NULL
,[DocBlank] [BIT] NULL
,[DocBlankCh] [char](1) NULL
,[DocBranch] [INT] NULL
,[DocUnit] [INT] NULL
,[DocStart] [NUMERIC](18, 0) NULL
,[DocCurr] [NUMERIC](18, 0) NULL
,[DocEnd] [NUMERIC](18, 0) NULL
,[DocDesign] [NVARCHAR](50) NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogFloor') IS NULL
CREATE TABLE [AMS].[AuditLogFloor] (
[FloorId] [INT] NOT NULL
,[Description] [NVARCHAR](50) NULL
,[ShortName] [NVARCHAR](50) NULL
,[Type] [char](10) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogGeneralLedger') IS NULL
CREATE TABLE [AMS].[AuditLogGeneralLedger] (
[GLID] [BIGINT] NOT NULL
,[GLName] [NVARCHAR](200) NULL
,[GLCode] [NVARCHAR](50) NULL
,[ACCode] [NVARCHAR](50) NULL
,[GLType] [NVARCHAR](50) NULL
,[GrpId] [INT] NULL
,[SubGrpId] [INT] NULL
,[PanNo] [NVARCHAR](50) NULL
,[AreaId] [INT] NULL
,[AgentId] [INT] NULL
,[CurrId] [INT] NULL
,[CrDays] [NUMERIC](18, 0) NULL
,[CrLimit] [DECIMAL](18, 6) NULL
,[CrTYpe] [NVARCHAR](50) NULL
,[IntRate] [DECIMAL](18, 6) NULL
,[GLAddress] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LandLineNo] [NVARCHAR](50) NULL
,[OwnerName] [NVARCHAR](50) NULL
,[OwnerNumber] [NVARCHAR](50) NULL
,[Scheme] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](50) NULL
,[Company_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogGodown') IS NULL
CREATE TABLE [AMS].[AuditLogGodown] (
[GID] [INT] NOT NULL
,[GName] [NVARCHAR](80) NULL
,[GCode] [NVARCHAR](50) NULL
,[GLocation] [NVARCHAR](50) NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[CompUnit] [INT] NULL
,[BranchUnit] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogGT_Details') IS NULL
CREATE TABLE [AMS].[AuditLogGT_Details] (
[VoucherNo] [NVARCHAR](50) NULL
,[SNo] [INT] NOT NULL
,[ProId] [BIGINT] NULL
,[ToGdn] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[AltQty] [DECIMAL](18, 6) NULL
,[AltUOM] [INT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[UOM] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Narration] [VARCHAR](1024) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_AuditLogGT_Details] PRIMARY KEY CLUSTERED ([SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogGT_Master') IS NULL
CREATE TABLE [AMS].[AuditLogGT_Master] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NULL
,[VoucherMiti] [NVARCHAR](50) NULL
,[FrmGdn] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [VARCHAR](1024) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[CompanyUnit] [INT] NULL
,[BranchId] [INT] NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogJuniorAgent') IS NULL
CREATE TABLE [AMS].[AuditLogJuniorAgent] (
[AgentId] [INT] NOT NULL
,[AgentName] [NVARCHAR](200) NULL
,[AgentCode] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[GLCode] [BIGINT] NULL
,[Commission] [DECIMAL](18, 6) NULL
,[SAgent] [INT] NULL
,[Email] [NVARCHAR](200) NULL
,[CrLimit] [NUMERIC](18, 8) NULL
,[CrDays] [NVARCHAR](50) NULL
,[CrTYpe] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[Status] [BIT] NOT NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogJV_Details') IS NULL
CREATE TABLE [AMS].[AuditLogJV_Details] (
[Voucher_No] [NVARCHAR](50) NULL
,[SNo] [INT] NULL
,[Ledger_ID] [BIGINT] NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CbLedger_ID] [BIGINT] NULL
,[Chq_No] [NVARCHAR](50) NULL
,[Chq_Date] [DATETIME] NULL
,[Debit] [DECIMAL](18, 6) NULL
,[Credit] [DECIMAL](18, 6) NULL
,[LocalDebit] [DECIMAL](18, 6) NULL
,[LocalCredit] [DECIMAL](18, 6) NULL
,[Narration] [VARCHAR](1024) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Vat_Reg] [BIT] NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [NVARCHAR](50) NULL
,[PanNo] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogJV_Master') IS NULL
CREATE TABLE [AMS].[AuditLogJV_Master] (
[VoucherMode] [char](10) NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NULL
,[Voucher_Miti] [NVARCHAR](10) NULL
,[Voucher_Time] [DATETIME] NULL
,[Currency_ID] [INT] NULL
,[Currency_Rate] [DECIMAL](18, 6) NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Ref_VNo] [VARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[Remarks] [VARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[ClearingDate] [DATETIME] NULL
,[PrintValue] [INT] NULL
,[CBranch_Id] [INT] NULL
,[CUnit_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogLedgerOpening') IS NULL
CREATE TABLE [AMS].[AuditLogLedgerOpening] (
[Opening_Id] [INT] NULL
,[Module] [char](10) NULL
,[Voucher_No] [NVARCHAR](50) NULL
,[OP_Date] [DATETIME] NULL
,[OP_Miti] [NVARCHAR](50) NULL
,[Serial_No] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Currency_ID] [INT] NULL
,[Currency_Rate] [DECIMAL](18, 6) NULL
,[Debit] [DECIMAL](18, 6) NULL
,[LocalDebit] [DECIMAL](18, 6) NULL
,[Credit] [DECIMAL](18, 6) NULL
,[LocalCredit] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Company_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogMainArea') IS NULL
CREATE TABLE [AMS].[AuditLogMainArea] (
[MAreaId] [INT] NOT NULL
,[MAreaName] [NVARCHAR](100) NULL
,[MAreaCode] [NVARCHAR](50) NULL
,[MCountry] [NVARCHAR](50) NULL
,[Company_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditlogNotes_Details') IS NULL
CREATE TABLE [AMS].[AuditlogNotes_Details] (
[VoucherMode] [char](10) NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NULL
,[Subledge_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Debit] [DECIMAL](18, 6) NOT NULL
,[Credit] [DECIMAL](18, 6) NOT NULL
,[LocalDebit] [DECIMAL](18, 6) NOT NULL
,[LocalCredit] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [BIGINT] NULL
,[PanNo] [NUMERIC](18, 0) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogNotes_Master') IS NULL
CREATE TABLE [AMS].[AuditLogNotes_Master] (
[VoucherMode] [NVARCHAR](50) NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NULL
,[Voucher_Miti] [NVARCHAR](10) NULL
,[Voucher_Time] [DATETIME] NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[VoucherType] [NVARCHAR](50) NULL
,[Ledger_ID] [BIGINT] NULL
,[CheqNo] [NVARCHAR](50) NULL
,[CheqDate] [DATETIME] NULL
,[CheqMiti] [NVARCHAR](50) NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Currency_ID] [INT] NULL
,[Currency_Rate] [DECIMAL](18, 6) NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[AuditLock] [char](10) NULL
,[ClearingDate] [DATETIME] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[PrintValue] [INT] NULL
,[CBranch_Id] [INT] NULL
,[CUnit_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogPAB_Details') IS NULL
CREATE TABLE [AMS].[AuditLogPAB_Details] (
[PAB_Invoice] [NVARCHAR](50) NULL
,[SNo] [INT] NULL
,[PT_Id] [INT] NULL
,[Ledger_ID] [BIGINT] NULL
,[CbLedger_ID] [BIGINT] NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Product_Id] [BIGINT] NULL
,[Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[Percentage] [DECIMAL](18, 6) NULL
,[Term_Type] [char](2) NULL
,[PAB_Narration] [VARCHAR](1024) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogPAB_Master') IS NULL
CREATE TABLE [AMS].[AuditLogPAB_Master] (
[PAB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[Invoice_Time] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Agent_ID] [INT] NULL
,[PB_Invoice] [NVARCHAR](50) NULL
,[PB_Date] [DATETIME] NULL
,[PB_Miti] [NVARCHAR](50) NULL
,[PB_Qty] [DECIMAL](18, 6) NULL
,[PB_Amount] [DECIMAL](18, 6) NULL
,[LocalAmount] [DECIMAL](18, 0) NULL
,[Cur_Id] [INT] NULL
,[Cur_Rate] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Audit_Lock] [BIT] NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogPB_Details') IS NULL
CREATE TABLE [AMS].[AuditLogPB_Details] (
[PB_Invoice] [NVARCHAR](50) NULL
,[Invoice_SNo] [NUMERIC](18, 0) NULL
,[P_Id] [BIGINT] NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](1024) NULL
,[PO_Invoice] [NVARCHAR](50) NULL
,[PO_Sno] [NUMERIC](18, 0) NULL
,[PC_Invoice] [NVARCHAR](50) NULL
,[PC_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[P_Ledger] [BIGINT] NULL
,[PR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogPB_Master') IS NULL
CREATE TABLE [AMS].[AuditLogPB_Master] (
[PB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](10) NULL
,[Invoice_Time] [DATETIME] NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [NVARCHAR](10) NULL
,[Vendor_ID] [BIGINT] NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_In] [NVARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[PO_Invoice] [NVARCHAR](250) NULL
,[PO_Date] [DATETIME] NULL
,[PC_Invoice] [NVARCHAR](250) NULL
,[PC_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Cur_Id] [INT] NULL
,[Cur_Rate] [DECIMAL](18, 6) NULL
,[Counter_ID] [INT] NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[LN_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Change_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Audit_Lock] [BIT] NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[CancelBy] [NVARCHAR](50) NULL
,[CancelDate] [DATETIME] NULL
,[CancelRemarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogPB_OtherMaster') IS NULL
CREATE TABLE [AMS].[AuditLogPB_OtherMaster] (
[PAB_Invoice] [NVARCHAR](50) NOT NULL
,[PPNo] [NVARCHAR](50) NULL
,[PPDate] [DATETIME] NULL
,[TaxableAmount] [DECIMAL](18, 6) NULL
,[VatAmount] [DECIMAL](18, 6) NULL
,[CustomAgent] [NVARCHAR](50) NULL
,[Transportation] [NVARCHAR](50) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[Cn_No] [VARCHAR](25) NULL
,[Cn_Date] [DATETIME] NULL
,[BankDoc] [NVARCHAR](50) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogPB_Term') IS NULL
CREATE TABLE [AMS].[AuditLogPB_Term] (
[PB_Vno] [NVARCHAR](50) NOT NULL
,[PT_Id] [INT] NULL
,[SNo] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSB_Details') IS NULL
CREATE TABLE [AMS].[AuditLogSB_Details] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [BIGINT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[SO_Invoice] [NVARCHAR](50) NULL
,[SO_Sno] [NUMERIC](18, 0) NULL
,[SC_Invoice] [NVARCHAR](50) NULL
,[SC_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSB_Master') IS NULL
CREATE TABLE [AMS].[AuditLogSB_Master] (
[SB_Invoice] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [VARCHAR](10) NULL
,[Invoice_Time] [DATETIME] NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [VARCHAR](10) NULL
,[Customer_ID] [INT] NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_Mode] [NVARCHAR](50) NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[SO_Invoice] [NVARCHAR](250) NULL
,[SO_Date] [DATETIME] NULL
,[SC_Invoice] [NVARCHAR](250) NULL
,[SC_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[Cur_Id] [INT] NULL
,[Cur_Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[LN_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSB_Term') IS NULL
CREATE TABLE [AMS].[AuditLogSB_Term] (
[SB_VNo] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[ActonType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSO_Details') IS NULL
CREATE TABLE [AMS].[AuditLogSO_Details] (
[SO_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[IND_Invoice] [NVARCHAR](50) NULL
,[IND_Sno] [INT] NULL
,[QOT_Invoice] [NVARCHAR](50) NULL
,[QOT_SNo] [INT] NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[Notes] [NVARCHAR](500) NULL
,[PrintedItem] [BIT] NULL
,[PrintKOT] [BIT] NULL
,[OrderTime] [DATETIME] NULL
,[Print_Time] [DATETIME] NULL
,[Is_Canceled] [BIT] NULL
,[CancelNotes] [NVARCHAR](500) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSO_Master') IS NULL
CREATE TABLE [AMS].[AuditLogSO_Master] (
[SO_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [VARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_Date] [DATETIME] NULL
,[Ref_Miti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_Mode] [NVARCHAR](50) NOT NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[IND_Invoice] [NVARCHAR](250) NULL
,[IND_Date] [DATETIME] NULL
,[QOT_Invoice] [NVARCHAR](250) NULL
,[QOT_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[TableId] [INT] NULL
,[CombineTableId] [NVARCHAR](500) NULL
,[NoOfPerson] [INT] NULL
,[Cur_Id] [INT] NULL
,[Cur_Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[LN_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSO_Term') IS NULL
CREATE TABLE [AMS].[AuditLogSO_Term] (
[SO_Vno] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSR_Details') IS NULL
CREATE TABLE [AMS].[AuditLogSR_Details] (
[SR_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [BIGINT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[SB_Invoice] [NVARCHAR](50) NULL
,[SB_Sno] [NUMERIC](18, 0) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [NVARCHAR](50) NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSR_Master') IS NULL
CREATE TABLE [AMS].[AuditLogSR_Master] (
[SR_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [VARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NULL
,[SB_Invoice] [NVARCHAR](50) NULL
,[SB_Date] [DATETIME] NULL
,[SB_Miti] [VARCHAR](10) NULL
,[Customer_ID] [INT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_Mode] [NVARCHAR](50) NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[Cur_Id] [INT] NULL
,[Cur_Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[LN_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.AuditLogSR_Term') IS NULL
CREATE TABLE [AMS].[AuditLogSR_Term] (
[SR_VNo] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[ActionType] [NVARCHAR](50) NULL
,[ActionBy] [NVARCHAR](50) NULL
,[ActionDate] [DATETIME] NULL
,[SystemUser] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.BillOfMaterial_Details') IS NULL
CREATE TABLE [AMS].[BillOfMaterial_Details] (
[MemoNo] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[GdnId] [INT] NOT NULL
,[CostCenterId] [INT] NOT NULL
,[AltQty] [DECIMAL](18, 6) NULL
,[AltUnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[UnitId] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](1024) NULL
,[Order_No] [NVARCHAR](50) NULL
,[Order_SNo] [INT] NULL
,CONSTRAINT [PK_BillOfMaterial_Details] PRIMARY KEY CLUSTERED ([MemoNo] ASC, [SNo] ASC, [ProductId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.BillOfMaterial_Master') IS NULL
CREATE TABLE [AMS].[BillOfMaterial_Master] (
[MemoNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NOT NULL
,[VoucherMiti] [NVARCHAR](10) NOT NULL
,[MemoDesc] [NVARCHAR](1024) NOT NULL
,[OrderNo] [NVARCHAR](50) NOT NULL
,[OrderDate] [DATETIME] NOT NULL
,[FGProductId] [BIGINT] NOT NULL
,[AltQty] [DECIMAL](18, 6) NOT NULL
,[AltUnitId] [INT] NOT NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[UnitId] [INT] NOT NULL
,[Factor] [DECIMAL](18, 6) NULL
,[FFactor] [DECIMAL](18, 6) NULL
,[CostRate] [DECIMAL](18, 6) NULL
,[GdnId] [INT] NOT NULL
,[CostCenterId] [INT] NOT NULL
,[Dep1] [INT] NULL
,[Dep2] [INT] NULL
,[Dep3] [INT] NULL
,[Dep4] [INT] NULL
,[TotalQty] [DECIMAL](18, 6) NOT NULL
,[NetAmount] [DECIMAL](18, 6) NOT NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Module] [NVARCHAR](50) NOT NULL
,[ActionType] [NVARCHAR](50) NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[AuthorisedBy] [NVARCHAR](50) NULL
,[AuthorisedDate] [DATETIME] NULL
,[CompanyUnitId] [INT] NULL
,[BranchId] [INT] NOT NULL
,CONSTRAINT [PK_BillOfMaterial_Master_1] PRIMARY KEY CLUSTERED ([MemoNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Branch') IS NULL
CREATE TABLE [AMS].[Branch] (
[Branch_ID] [INT] NOT NULL
,[Branch_Name] [NVARCHAR](200) NOT NULL
,[Branch_Code] [NVARCHAR](50) NOT NULL
,[Reg_Date] [DATETIME] NULL
,[Address] [NVARCHAR](500) NULL
,[Country] [NVARCHAR](50) NULL
,[State] [NVARCHAR](50) NULL
,[City] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Fax] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](80) NULL
,[ContactPerson] [NVARCHAR](50) NULL
,[ContactPersonAdd] [NVARCHAR](50) NULL
,[ContactPersonPhone] [NVARCHAR](50) NULL
,[Created_By] [NVARCHAR](50) NULL
,[Created_Date] [DATETIME] NULL
,[Modify_By] [NVARCHAR](50) NULL
,[Modify_Date] [DATETIME] NULL
,CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED ([Branch_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_BranchDesc] UNIQUE NONCLUSTERED ([Branch_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_BranchShortName] UNIQUE NONCLUSTERED ([Branch_Code] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.BranchRights') IS NULL
CREATE TABLE [AMS].[BranchRights] (
[RightsId] [INT] IDENTITY (1, 1) NOT NULL
,[UserId] [INT] NULL
,[BranchId] [INT] NULL
,[Branch] [VARCHAR](255) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.BranchWiseLedger') IS NULL
CREATE TABLE [AMS].[BranchWiseLedger] (
[BranchId] [INT] NOT NULL
,[LedgerId] [BIGINT] NULL
,[Mapped] [BIT] NULL
,[Category] [NVARCHAR](50) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.Budget') IS NULL
CREATE TABLE [AMS].[Budget] (
[BudgetId] [INT] NOT NULL
,[LedgerId] [BIGINT] NOT NULL
,[Dep1] [INT] NULL
,[Dep2] [INT] NULL
,[Dep3] [INT] NULL
,[Dep4] [INT] NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[MonthDesc] [NVARCHAR](50) NOT NULL
,[Date] [DATETIME] NULL
,[Miti] [VARCHAR](10) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED ([BudgetId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.BudgetLedger') IS NULL
CREATE TABLE [AMS].[BudgetLedger] (
[BLID] [INT] NOT NULL
,[LedgerId] [BIGINT] NULL
,[MonthsDesc] [NVARCHAR](50) NULL
,[Amount] [DECIMAL](16, 6) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_BudgetLedger] PRIMARY KEY CLUSTERED ([BLID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CashBankDenomination') IS NULL
CREATE TABLE [AMS].[CashBankDenomination] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NOT NULL
,[VoucherMiti] [NVARCHAR](10) NOT NULL
,[TotalAmount] [DECIMAL](18, 6) NOT NULL
,[R1000] [DECIMAL](18, 6) NOT NULL
,[R500] [DECIMAL](18, 6) NOT NULL
,[R100] [DECIMAL](18, 6) NOT NULL
,[R50] [DECIMAL](18, 6) NOT NULL
,[R20] [DECIMAL](18, 6) NOT NULL
,[R10] [DECIMAL](18, 6) NOT NULL
,[R5] [DECIMAL](18, 6) NOT NULL
,[R2] [DECIMAL](18, 6) NOT NULL
,[R1] [DECIMAL](18, 6) NOT NULL
,[RO] [DECIMAL](18, 6) NOT NULL
,[RC] [DECIMAL](18, 6) NOT NULL
,[RemainAmt] [DECIMAL](18, 6) NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.CashClosing') IS NULL
CREATE TABLE [AMS].[CashClosing] (
[CC_Id] [INT] IDENTITY (1, 1) NOT NULL
,[EnterMiti] [VARCHAR](10) NOT NULL
,[EnterTime] [DATETIME] NOT NULL
,[CB_Balance] [NVARCHAR](50) NOT NULL
,[Cash_Sales] [DECIMAL](18, 6) NOT NULL
,[Cash_Purchase] [DECIMAL](18, 6) NOT NULL
,[TotalCash] [DECIMAL](18, 6) NOT NULL
,[ThauQty] [DECIMAL](18, 6) NOT NULL
,[ThouVal] [DECIMAL](18, 6) NOT NULL
,[FivHunQty] [DECIMAL](18, 6) NOT NULL
,[FivHunVal] [DECIMAL](18, 6) NOT NULL
,[HunQty] [DECIMAL](18, 6) NOT NULL
,[HunVal] [DECIMAL](18, 6) NOT NULL
,[FiFtyQty] [DECIMAL](18, 6) NOT NULL
,[FiftyVal] [DECIMAL](18, 6) NOT NULL
,[TwenteyFiveQty] [DECIMAL](18, 6) NOT NULL
,[TwentyFiveVal] [DECIMAL](18, 6) NOT NULL
,[TwentyQty] [DECIMAL](18, 6) NOT NULL
,[TwentyVal] [DECIMAL](16, 6) NOT NULL
,[TenQty] [DECIMAL](18, 6) NOT NULL
,[TenVal] [DECIMAL](18, 6) NOT NULL
,[FiveQty] [DECIMAL](18, 6) NOT NULL
,[FiveVal] [DECIMAL](18, 6) NOT NULL
,[TwoQty] [DECIMAL](18, 6) NOT NULL
,[TwoVal] [DECIMAL](18, 6) NOT NULL
,[OneQty] [DECIMAL](18, 6) NOT NULL
,[OneVal] [DECIMAL](18, 6) NOT NULL
,[Cash_Diff] [DECIMAL](18, 6) NOT NULL
,[Module] [char](10) NOT NULL
,[HandOverUser] [NVARCHAR](50) NOT NULL
,[Remarks] [VARCHAR](250) NULL
,[EnterBy] [VARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_CashClosing] PRIMARY KEY CLUSTERED ([CC_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CB_Details') IS NULL
CREATE TABLE [AMS].[CB_Details] (
[Voucher_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Debit] [DECIMAL](18, 6) NOT NULL
,[Credit] [DECIMAL](18, 6) NOT NULL
,[LocalDebit] [DECIMAL](18, 6) NOT NULL
,[LocalCredit] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [BIGINT] NULL
,[PanNo] [INT] NULL
,[Vat_Reg] [BIT] NULL
,[CBLedgerId] [BIGINT] NULL
,[CurrencyId] [INT] NULL
,[CurrencyRate] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_CB_Details] PRIMARY KEY CLUSTERED ([Voucher_No] ASC, [SNo] ASC, [Ledger_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CB_Master') IS NULL
CREATE TABLE [AMS].[CB_Master] (
[VoucherMode] [char](10) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [NVARCHAR](10) NOT NULL
,[Voucher_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[VoucherType] [NVARCHAR](50) NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[CheqNo] [NVARCHAR](50) NULL
,[CheqDate] [DATETIME] NULL
,[CheqMiti] [NVARCHAR](10) NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [DECIMAL](18, 6) NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[ClearingDate] [DATETIME] NULL
,[PrintValue] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_CB_Master_1] PRIMARY KEY CLUSTERED ([Voucher_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CompanyInfo') IS NULL
CREATE TABLE [AMS].[CompanyInfo] (
[Company_Id] [TINYINT] IDENTITY (1, 1) NOT NULL
,[Company_Name] [NVARCHAR](200) NOT NULL
,[Company_Logo] [IMAGE] NULL
,[CReg_Date] [DATETIME] NULL
,[Address] [NVARCHAR](200) NULL
,[Country] [NVARCHAR](50) NULL
,[State] [NVARCHAR](50) NULL
,[City] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Fax] [NVARCHAR](50) NULL
,[Pan_No] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](50) NULL
,[Website] [NVARCHAR](100) NULL
,[Database_Name] [NVARCHAR](50) NOT NULL
,[Database_Path] [NVARCHAR](100) NULL
,[Version_No] [NVARCHAR](50) NULL
,[Status] [BIT] NOT NULL CONSTRAINT [DF_CompanyInfo_Status] DEFAULT ((1))
,[CreateDate] [DATETIME] NULL
,[LoginDate] [DATETIME] NULL
,[SoftModule] [NVARCHAR](50) NULL
,[IsSyncOnline] [BIT] NULL
,[PrintDesc] [NVARCHAR](200) NOT NULL
,CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Company_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CompanyDesc] UNIQUE NONCLUSTERED ([Company_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CompanyUnit') IS NULL
CREATE TABLE [AMS].[CompanyUnit] (
[CmpUnit_ID] [INT] NOT NULL
,[CmpUnit_Name] [NVARCHAR](200) NOT NULL
,[CmpUnit_Code] [NVARCHAR](50) NOT NULL
,[Reg_Date] [DATETIME] NOT NULL
,[Address] [NVARCHAR](50) NULL
,[Country] [NVARCHAR](50) NULL
,[State] [NVARCHAR](50) NULL
,[City] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Fax] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](90) NULL
,[ContactPerson] [NVARCHAR](50) NULL
,[ContactPersonAdd] [NVARCHAR](50) NULL
,[ContactPersonPhone] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NOT NULL
,[Created_By] [NVARCHAR](50) NOT NULL
,[Created_Date] [DATETIME] NOT NULL
,CONSTRAINT [PK_CompanyUnit] PRIMARY KEY CLUSTERED ([CmpUnit_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CompanyUnitDesc] UNIQUE NONCLUSTERED ([CmpUnit_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CompanyUnitShortName] UNIQUE NONCLUSTERED ([CmpUnit_Code] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CostCenter') IS NULL
CREATE TABLE [AMS].[CostCenter] (
[CCId] [INT] NOT NULL
,[CCName] [NVARCHAR](200) NOT NULL
,[CCcode] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED ([CCId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CostCenterDesc] UNIQUE NONCLUSTERED ([CCName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CostCenterShortName] UNIQUE NONCLUSTERED ([CCcode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CostCenterExpenses_Details') IS NULL
CREATE TABLE [AMS].[CostCenterExpenses_Details] (
[CostingNo] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[LedgerId] [BIGINT] NOT NULL
,[CostCenterId] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[OrderNo] [NVARCHAR](50) NULL
,[OrderSNo] [INT] NULL
,CONSTRAINT [PK_CostCenterExpenses_Details] PRIMARY KEY CLUSTERED ([CostingNo] ASC, [SNo] ASC, [LedgerId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.CostCenterExpenses_Master') IS NULL
CREATE TABLE [AMS].[CostCenterExpenses_Master] (
[CostingNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NOT NULL
,[VoucherMiti] [NVARCHAR](10) NOT NULL
,[VoucherTime] [DATETIME] NOT NULL
,[OrderNo] [NVARCHAR](50) NULL
,[OrderDate] [DATETIME] NULL
,[FGProductId] [BIGINT] NOT NULL
,[GdnId] [INT] NOT NULL
,[CostCenterId] [INT] NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[ActionType] [NVARCHAR](50) NOT NULL
,[CompanyUnitId] [INT] NULL
,[BranchId] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_CostCenterExpenses_Master] PRIMARY KEY CLUSTERED ([CostingNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Counter') IS NULL
CREATE TABLE [AMS].[Counter] (
[CId] [INT] NOT NULL
,[CName] [NVARCHAR](50) NOT NULL
,[CCode] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Printer] [VARCHAR](250) NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED ([CId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CounterDesc] UNIQUE NONCLUSTERED ([CName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CounterShortName] UNIQUE NONCLUSTERED ([CCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Currency') IS NULL
CREATE TABLE [AMS].[Currency] (
[CId] [INT] NOT NULL
,[CName] [NVARCHAR](100) NOT NULL
,[CCode] [NVARCHAR](50) NOT NULL
,[CRate] [NUMERIC](18, 6) NOT NULL CONSTRAINT [DF_Currency_CRate] DEFAULT ((1))
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL CONSTRAINT [DF_Currency_Status] DEFAULT ((1))
,[EnterBy] [NVARCHAR](50) NOT NULL CONSTRAINT [DF_Currency_EnterBy] DEFAULT (N'MrSolution')
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([CId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CurrencyDesc] UNIQUE NONCLUSTERED ([CName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_CurrencyShortName] UNIQUE NONCLUSTERED ([CCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.DateMiti') IS NULL
CREATE TABLE [AMS].[DateMiti] (
[Date_Id] [BIGINT] NOT NULL
,[BS_Date] [VARCHAR](10) NOT NULL
,[BS_DateDMY] [VARCHAR](10) NOT NULL
,[AD_Date] [DATETIME] NOT NULL
,[BS_Months] [VARCHAR](50) NOT NULL
,[AD_Months] [VARCHAR](50) NOT NULL
,[Days] [VARCHAR](50) NULL
,[Is_Holiday] [BIT] NULL
,[Holiday] [VARCHAR](1024) NULL
,CONSTRAINT [PK_DateMiti] PRIMARY KEY CLUSTERED ([Date_Id] ASC, [BS_Date] ASC, [BS_DateDMY] ASC, [AD_Date] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_DateMiti] UNIQUE NONCLUSTERED ([AD_Date] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_DateMiti_1] UNIQUE NONCLUSTERED ([BS_DateDMY] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_DateMiti_2] UNIQUE NONCLUSTERED ([AD_Date] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Department') IS NULL
CREATE TABLE [AMS].[Department] (
[DId] [INT] NOT NULL
,[DName] [NVARCHAR](50) NOT NULL
,[DCode] [NVARCHAR](50) NOT NULL
,[Dlevel] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([DId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_DepartmentDesc] UNIQUE NONCLUSTERED ([DName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_DepartmentShortName] UNIQUE NONCLUSTERED ([DCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.DllPrintDesigList') IS NULL
CREATE TABLE [AMS].[DllPrintDesigList] (
[ListId] [INT] NOT NULL
,[Module] [char](4) NOT NULL
,[DesignDesc] [NVARCHAR](MAX) NOT NULL
,CONSTRAINT [PK_DllPrintDesigList_ListId] PRIMARY KEY CLUSTERED ([ListId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.DocumentDesignPrint') IS NULL
CREATE TABLE [AMS].[DocumentDesignPrint] (
[DDP_Id] [INT] NOT NULL
,[Module] [char](10) NOT NULL
,[Paper_Name] [NVARCHAR](100) NOT NULL
,[Is_Online] [BIT] NOT NULL
,[NoOfPrint] [INT] NOT NULL
,[Notes] [NVARCHAR](250) NULL
,[DesignerPaper_Name] [NVARCHAR](250) NULL
,[Created_By] [NVARCHAR](250) NULL
,[Created_Date] [DATETIME] NULL
,[Branch_ID] [INT] NOT NULL
,[CmpUnit_ID] [INT] NULL
,[Status] [BIT] NOT NULL
,CONSTRAINT [PK_PrintDocument_Design] PRIMARY KEY CLUSTERED ([DDP_Id] ASC, [Module] ASC, [Paper_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.DocumentNumbering') IS NULL
CREATE TABLE [AMS].[DocumentNumbering] (
[DocId] [INT] NOT NULL
,[DocModule] [NVARCHAR](50) NOT NULL
,[DocUser] [NVARCHAR](50) NULL
,[DocDesc] [NVARCHAR](50) NOT NULL
,[DocStartDate] [Date] NOT NULL
,[DocStartMiti] [VARCHAR](10) NOT NULL
,[DocEndDate] [Date] NOT NULL
,[DocEndMiti] [VARCHAR](10) NOT NULL
,[DocType] [char](10) NOT NULL
,[DocPrefix] [NVARCHAR](50) NULL
,[DocSufix] [NVARCHAR](50) NULL
,[DocBodyLength] [NUMERIC](18, 0) NOT NULL
,[DocTotalLength] [NUMERIC](18, 0) NOT NULL
,[DocBlank] [BIT] NOT NULL
,[DocBlankCh] [char](1) NOT NULL
,[DocBranch] [INT] NOT NULL
,[DocUnit] [INT] NULL
,[DocStart] [NUMERIC](18, 0) NULL
,[DocCurr] [NUMERIC](18, 0) NULL
,[DocEnd] [NUMERIC](18, 0) NULL
,[DocDesign] [NVARCHAR](50) NULL
,[FY_Id] [INT] NULL
,[FiscalYearId] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_DocumentNumbering] PRIMARY KEY CLUSTERED ([DocId] ASC, [DocModule] ASC, [DocDesc] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.FGR_Details') IS NULL
CREATE TABLE [AMS].[FGR_Details] (
[FGR_No] [VARCHAR](15) NOT NULL
,[SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[CC_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[Stock_Qty] [DECIMAL](18, 6) NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[Cnv_Ratio] [DECIMAL](18, 6) NOT NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.FGR_Master') IS NULL
CREATE TABLE [AMS].[FGR_Master] (
[FGR_No] [VARCHAR](15) NOT NULL
,[FGR_Date] [DATETIME] NOT NULL
,[FGR_Time] [DATETIME] NOT NULL
,[FGR_Miti] [VARCHAR](10) NOT NULL
,[Gdn_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[GLID] [BIGINT] NULL
,[CBranch_Id] [INT] NULL
,[CUnit_Id] [INT] NULL
,[CC_Id] [INT] NULL
,[SB_No] [VARCHAR](250) NULL
,[SB_Sno] [INT] NULL
,[SO_No] [VARCHAR](250) NULL
,[Auth_By] [VARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Source] [VARCHAR](15) NULL
,[Export] [char](1) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[FiscalYearId] [INT] NULL
,[Enter_By] [VARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,CONSTRAINT [PK_FGR_Master] PRIMARY KEY CLUSTERED ([FGR_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.FinanceSetting') IS NULL
CREATE TABLE [AMS].[FinanceSetting] (
[FinId] [TINYINT] NOT NULL
,[ProfiLossId] [BIGINT] NULL
,[CashId] [BIGINT] NULL
,[VATLedgerId] [BIGINT] NULL
,[PDCBankLedgerId] [BIGINT] NULL
,[ShortNameWisTransaction] [BIT] NULL
,[WarngNegativeTransaction] [BIT] NULL
,[NegativeTransaction] [NVARCHAR](50) NULL
,[VoucherDate] [BIT] NULL
,[AgentEnable] [BIT] NULL
,[AgentMandetory] [BIT] NULL
,[DepartmentEnable] [BIT] NULL
,[DepartmentMandetory] [BIT] NULL
,[RemarksEnable] [BIT] NULL
,[RemarksMandetory] [BIT] NULL
,[NarrationMandetory] [BIT] NULL
,[CurrencyEnable] [BIT] NULL
,[CurrencyMandetory] [BIT] NULL
,[SubledgerEnable] [BIT] NULL
,[SubledgerMandetory] [BIT] NULL
,[DetailsClassEnable] [BIT] NULL
,[DetailsClassMandetory] [BIT] NULL
,CONSTRAINT [PK_FinanceSetting] PRIMARY KEY CLUSTERED ([FinId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.FiscalYear') IS NULL
CREATE TABLE [AMS].[FiscalYear] (
[FY_Id] [INT] NOT NULL
,[AD_FY] [VARCHAR](5) NOT NULL
,[BS_FY] [VARCHAR](5) NOT NULL
,[Current_FY] [BIT] NOT NULL
,[Start_ADDate] [DATETIME] NOT NULL
,[End_ADDate] [DATETIME] NOT NULL
,[Start_BSDate] [VARCHAR](10) NOT NULL
,[End_BSDate] [VARCHAR](10) NOT NULL
,CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED ([FY_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_FiscalYearAD] UNIQUE NONCLUSTERED ([AD_FY] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_FiscalYearBS] UNIQUE NONCLUSTERED ([BS_FY] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Floor') IS NULL
CREATE TABLE [AMS].[Floor] (
[FloorId] [INT] NOT NULL
,[Description] [NVARCHAR](50) NOT NULL
,[ShortName] [NVARCHAR](50) NOT NULL
,[Type] [char](10) NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Floor] PRIMARY KEY CLUSTERED ([FloorId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_FloorDesc] UNIQUE NONCLUSTERED ([Description] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_FloorShortName] UNIQUE NONCLUSTERED ([ShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.GeneralLedger') IS NULL
CREATE TABLE [AMS].[GeneralLedger] (
[GLID] [BIGINT] NOT NULL
,[GLName] [NVARCHAR](200) NOT NULL
,[GLCode] [NVARCHAR](50) NOT NULL
,[ACCode] [NVARCHAR](50) NOT NULL
,[GLType] [NVARCHAR](50) NOT NULL
,[GrpId] [INT] NOT NULL
,[SubGrpId] [INT] NULL
,[PanNo] [NVARCHAR](50) NULL
,[AreaId] [INT] NULL
,[AgentId] [INT] NULL
,[CurrId] [INT] NULL CONSTRAINT [DF_GeneralLedger_CurrId] DEFAULT ((1))
,[CrDays] [NUMERIC](18, 0) NOT NULL CONSTRAINT [DF_GeneralLedger_CrDays] DEFAULT ((0))
,[CrLimit] [DECIMAL](18, 6) NOT NULL CONSTRAINT [DF_GeneralLedger_CrLimit] DEFAULT ((0.00))
,[CrTYpe] [NVARCHAR](50) NOT NULL CONSTRAINT [DF_GeneralLedger_CrTYpe] DEFAULT (N'Ignore')
,[IntRate] [DECIMAL](18, 6) NOT NULL CONSTRAINT [DF_GeneralLedger_IntRate] DEFAULT ((0.00))
,[GLAddress] [NVARCHAR](500) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LandLineNo] [NVARCHAR](50) NULL
,[OwnerName] [NVARCHAR](50) NULL
,[OwnerNumber] [NVARCHAR](50) NULL
,[Scheme] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL CONSTRAINT [DF_GeneralLedger_EnterBy] DEFAULT (N'MrSolution')
,[EnterDate] [DATETIME] NOT NULL
,[PrimaryGroupId] [INT] NULL CONSTRAINT [DF__GeneralLe__Prima__276B0F6C] DEFAULT ((0))
,[PrimarySubGroupId] [INT] NULL CONSTRAINT [DF__GeneralLe__Prima__285F33A5] DEFAULT ((0))
,[IsDefault] [char](1) NULL CONSTRAINT [DF__GeneralLe__IsDef__295357DE] DEFAULT ((0))
,[NepaliDesc] [NVARCHAR](200) NULL
,[Status] [BIT] NOT NULL CONSTRAINT [DF_GeneralLedger_Status] DEFAULT ((1))
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,CONSTRAINT [PK_GeneralLedger] PRIMARY KEY CLUSTERED ([GLID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_GeneralLedgerAccount] UNIQUE NONCLUSTERED ([ACCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_GeneralLedgerShortName] UNIQUE NONCLUSTERED ([GLCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Godown') IS NULL
CREATE TABLE [AMS].[Godown] (
[GID] [INT] NOT NULL
,[GName] [NVARCHAR](80) NULL
,[GCode] [NVARCHAR](50) NULL
,[GLocation] [NVARCHAR](50) NULL
,[CompUnit] [INT] NULL
,[BranchUnit] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Godown] PRIMARY KEY CLUSTERED ([GID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [GodownDesc] UNIQUE NONCLUSTERED ([GName] ASC, [GCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.GT_Details') IS NULL
CREATE TABLE [AMS].[GT_Details] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[ProId] [BIGINT] NOT NULL
,[ToGdn] [INT] NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[AltQty] [DECIMAL](18, 6) NOT NULL
,[AltUOM] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[UOM] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_GT_Details] PRIMARY KEY CLUSTERED ([VoucherNo] ASC, [SNo] ASC, [ProId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.GT_Master') IS NULL
CREATE TABLE [AMS].[GT_Master] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NOT NULL
,[VoucherMiti] [VARCHAR](10) NOT NULL
,[FrmGdn] [INT] NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [VARCHAR](1024) NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[CompanyUnit] [INT] NULL
,[BranchId] [INT] NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,CONSTRAINT [PK__GodownTr__C5DCEA07636EBA21] PRIMARY KEY CLUSTERED ([VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Inv_Details') IS NULL
CREATE TABLE [AMS].[Inv_Details] (
[Inv_No] [VARCHAR](15) NOT NULL
,[SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[CC_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[Stock_Qty] [DECIMAL](18, 6) NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[Cnv_Ratio] [DECIMAL](18, 6) NOT NULL
,[Req_No] [VARCHAR](15) NULL
,[Req_SNo] [INT] NULL
,[Inv_TypeCode] [VARCHAR](10) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.Inv_Master') IS NULL
CREATE TABLE [AMS].[Inv_Master] (
[Inv_No] [VARCHAR](15) NOT NULL
,[Inv_Date] [DATETIME] NOT NULL
,[Inv_Time] [DATETIME] NOT NULL
,[Inv_Miti] [VARCHAR](10) NOT NULL
,[Gdn_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[GLID] [BIGINT] NULL
,[CBranch_Id] [INT] NULL
,[CUnit_Id] [INT] NULL
,[CC_Id] [INT] NULL
,[SB_No] [VARCHAR](250) NULL
,[SB_Sno] [INT] NULL
,[SO_No] [VARCHAR](250) NULL
,[BOM_Id] [NVARCHAR](50) NULL
,[BOM_Desc] [VARCHAR](500) NULL
,[Auth_By] [VARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Req_No] [VARCHAR](256) NULL
,[Ass_No] [VARCHAR](50) NULL
,[Source] [VARCHAR](50) NULL
,[FGR_No] [VARCHAR](50) NULL
,[FGR_Qty] [DECIMAL](16, 6) NULL
,[Export] [char](1) NULL
,[Remarks] [VARCHAR](1024) NULL
,[Enter_By] [VARCHAR](15) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[FiscalYearId] [INT] NULL
,CONSTRAINT [PK_Inv_Master] PRIMARY KEY CLUSTERED ([Inv_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.InventorySetting') IS NULL
CREATE TABLE [AMS].[InventorySetting] (
[InvId] [TINYINT] NOT NULL
,[OPLedgerId] [BIGINT] NULL
,[CSPLLedgerId] [BIGINT] NULL
,[CSBSLedgerId] [BIGINT] NULL
,[NegativeStock] [char](1) NULL
,[AlternetUnit] [BIT] NULL
,[CostCenterEnable] [BIT] NULL
,[CostCenterMandetory] [BIT] NULL
,[CostCenterItemEnable] [BIT] NULL
,[CostCenterItemMandetory] [BIT] NULL
,[ChangeUnit] [BIT] NULL
,[GodownEnable] [BIT] NULL
,[GodownMandetory] [BIT] NULL
,[RemarksEnable] [BIT] NULL
,[GodownItemEnable] [BIT] NULL
,[GodownItemMandetory] [BIT] NULL
,[NarrationEnable] [BIT] NULL
,[ShortNameWise] [BIT] NULL
,[BatchWiseQtyEnable] [BIT] NULL
,[ExpiryDate] [BIT] NULL
,[FreeQty] [BIT] NULL
,[GodownWiseFilter] [BIT] NULL
,[GodownWiseStock] [BIT] NULL
,CONSTRAINT [PK_InventorySetting] PRIMARY KEY CLUSTERED ([InvId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.IRDAPISetting') IS NULL
CREATE TABLE [AMS].[IRDAPISetting] (
[IRDAPI] [NVARCHAR](MAX) NULL
,[IrdUser] [NVARCHAR](500) NULL
,[IrdUserPassword] [NVARCHAR](500) NULL
,[IrdCompanyPan] [NVARCHAR](50) NULL
,[IsIRDRegister] [TINYINT] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.JuniorAgent') IS NULL
CREATE TABLE [AMS].[JuniorAgent] (
[AgentId] [INT] NOT NULL
,[AgentName] [NVARCHAR](200) NOT NULL
,[AgentCode] [NVARCHAR](50) NOT NULL
,[Address] [NVARCHAR](500) NOT NULL
,[PhoneNo] [NVARCHAR](50) NOT NULL
,[GLCode] [BIGINT] NULL
,[Commission] [DECIMAL](18, 6) NOT NULL
,[SAgent] [INT] NULL
,[Email] [NVARCHAR](200) NULL
,[CrLimit] [NUMERIC](18, 8) NOT NULL
,[CrDays] [NVARCHAR](50) NOT NULL
,[CrTYpe] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED ([AgentId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_JuniorAgentShortName] UNIQUE NONCLUSTERED ([AgentCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.JV_Details') IS NULL
CREATE TABLE [AMS].[JV_Details] (
[Voucher_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CbLedger_ID] [BIGINT] NULL
,[Chq_No] [NVARCHAR](50) NULL
,[Chq_Date] [DATETIME] NULL
,[Debit] [DECIMAL](18, 6) NOT NULL
,[Credit] [DECIMAL](18, 6) NOT NULL
,[LocalDebit] [DECIMAL](18, 6) NOT NULL
,[LocalCredit] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[Vat_Reg] [BIT] NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [BIGINT] NULL
,[PanNo] [INT] NULL
,[CurrencyId] [INT] NULL
,[CurrencyRate] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_JV_Details] PRIMARY KEY CLUSTERED ([Voucher_No] ASC, [SNo] ASC, [Ledger_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.JV_Master') IS NULL
CREATE TABLE [AMS].[JV_Master] (
[VoucherMode] [char](10) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [NVARCHAR](10) NOT NULL
,[Voucher_Time] [DATETIME] NOT NULL
,[Ref_VNo] [VARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [DECIMAL](18, 6) NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[Remarks] [VARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NOT NULL
,[Audit_Lock] [BIT] NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[ClearingDate] [DATETIME] NULL
,[PrintValue] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NULL
,[FiscalYearId] [INT] NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_Voucher_Main] PRIMARY KEY CLUSTERED ([Voucher_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.LedgerOpening') IS NULL
CREATE TABLE [AMS].[LedgerOpening] (
[Opening_Id] [INT] NOT NULL
,[Module] [char](10) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[OP_Date] [DATETIME] NOT NULL
,[OP_Miti] [NVARCHAR](50) NOT NULL
,[Serial_No] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [DECIMAL](18, 6) NOT NULL
,[Debit] [DECIMAL](18, 6) NOT NULL
,[LocalDebit] [DECIMAL](18, 6) NOT NULL
,[Credit] [DECIMAL](18, 6) NOT NULL
,[LocalCredit] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_LedgerOpening] PRIMARY KEY CLUSTERED ([Opening_Id] ASC, [Module] ASC, [Voucher_No] ASC, [Serial_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.MainArea') IS NULL
CREATE TABLE [AMS].[MainArea] (
[MAreaId] [INT] NOT NULL
,[MAreaName] [NVARCHAR](100) NOT NULL
,[MAreaCode] [NVARCHAR](50) NOT NULL
,[MCountry] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_MainArea] PRIMARY KEY CLUSTERED ([MAreaId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_MainAreaDesc] UNIQUE NONCLUSTERED ([MAreaName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_MainAreaShortName] UNIQUE NONCLUSTERED ([MAreaCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.MemberShipSetup') IS NULL
CREATE TABLE [AMS].[MemberShipSetup] (
[MShipId] [INT] NOT NULL
,[MShipDesc] [NVARCHAR](200) NOT NULL
,[MShipShortName] [NVARCHAR](50) NOT NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LedgerId] [BIGINT] NOT NULL
,[EmailAdd] [NVARCHAR](200) NULL
,[MemberTypeId] [INT] NOT NULL
,[MemberId] [NVARCHAR](50) NOT NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,[MValidDate] [DATETIME] NOT NULL
,[MExpireDate] [DATETIME] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[ActiveStatus] [BIT] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_MemberShipSetup] PRIMARY KEY CLUSTERED ([MShipId] ASC, [MShipDesc] ASC, [MShipShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.MemberType') IS NULL
CREATE TABLE [AMS].[MemberType] (
[MemberTypeId] [INT] NOT NULL
,[MemberDesc] [NVARCHAR](200) NOT NULL
,[MemberShortName] [NVARCHAR](50) NOT NULL
,[Discount] [DECIMAL](18, 6) NOT NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,[ActiveStatus] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_MemberType] PRIMARY KEY CLUSTERED ([MemberTypeId] ASC, [MemberDesc] ASC, [MemberShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Notes_Details') IS NULL
CREATE TABLE [AMS].[Notes_Details] (
[VoucherMode] [char](10) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Debit] [DECIMAL](18, 6) NOT NULL
,[Credit] [DECIMAL](18, 6) NOT NULL
,[LocalDebit] [DECIMAL](18, 6) NOT NULL
,[LocalCredit] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [BIGINT] NULL
,[PanNo] [NUMERIC](18, 0) NULL
,[Vat_Reg] [BIT] NULL
,CONSTRAINT [PK_Notes_Details] PRIMARY KEY CLUSTERED ([Voucher_No] ASC, [SNo] ASC, [Ledger_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Notes_Master') IS NULL
CREATE TABLE [AMS].[Notes_Master] (
[VoucherMode] [NVARCHAR](50) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [NVARCHAR](10) NOT NULL
,[Voucher_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[VoucherType] [NVARCHAR](50) NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[CheqNo] [NVARCHAR](50) NULL
,[CheqDate] [DATETIME] NULL
,[CheqMiti] [NVARCHAR](50) NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [DECIMAL](18, 6) NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[ClearingDate] [DATETIME] NULL
,[PrintValue] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_Notes_Master] PRIMARY KEY CLUSTERED ([Voucher_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PAB_Details') IS NULL
CREATE TABLE [AMS].[PAB_Details] (
[PAB_Invoice] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[PT_Id] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[CbLedger_ID] [BIGINT] NOT NULL
,[Subledger_Id] [INT] NOT NULL
,[Agent_ID] [INT] NULL
,[Product_Id] [BIGINT] NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[Percentage] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[PAB_Narration] [VARCHAR](1024) NULL
,CONSTRAINT [PK_PAB_Details] PRIMARY KEY CLUSTERED ([PAB_Invoice] ASC, [SNo] ASC, [PT_Id] ASC, [Ledger_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PAB_Master') IS NULL
CREATE TABLE [AMS].[PAB_Master] (
[PAB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Agent_ID] [INT] NULL
,[PB_Invoice] [NVARCHAR](50) NOT NULL
,[PB_Date] [DATETIME] NOT NULL
,[PB_Miti] [NVARCHAR](50) NOT NULL
,[PB_Qty] [DECIMAL](18, 6) NOT NULL
,[PB_Amount] [DECIMAL](18, 6) NOT NULL
,[LocalAmount] [DECIMAL](18, 0) NOT NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NOT NULL
,[CBranch_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,CONSTRAINT [PK_PAB_Master] PRIMARY KEY CLUSTERED ([PAB_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.PB_Details') IS NULL
CREATE TABLE [AMS].[PB_Details] (
[PB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[Stock_Qty] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[PO_Invoice] [NVARCHAR](50) NULL
,[PO_Sno] [NUMERIC](18, 0) NULL
,[PC_Invoice] [NVARCHAR](50) NULL
,[PC_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Rate] [DECIMAL](18, 6) NOT NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[P_Ledger] [BIGINT] NULL
,[PR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[TaxExempted_Amount] [DECIMAL](18, 6) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PB_Details] PRIMARY KEY CLUSTERED ([PB_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PB_Master') IS NULL
CREATE TABLE [AMS].[PB_Master] (
[PB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [NVARCHAR](10) NULL
,[Vendor_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_In] [NVARCHAR](50) NOT NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[PO_Invoice] [NVARCHAR](250) NULL
,[PO_Date] [DATETIME] NULL
,[PC_Invoice] [NVARCHAR](250) NULL
,[PC_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[Counter_ID] [INT] NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[Tender_Amount] [DECIMAL](18, 6) NOT NULL
,[Change_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[Tbl_Amount] [DECIMAL](18, 6) NOT NULL
,[Action_Type] [NVARCHAR](50) NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[CancelBy] [NVARCHAR](50) NULL
,[CancelDate] [DATETIME] NULL
,[CancelRemarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PB] PRIMARY KEY CLUSTERED ([PB_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.PB_OtherMaster') IS NULL
CREATE TABLE [AMS].[PB_OtherMaster] (
[PAB_Invoice] [NVARCHAR](50) NOT NULL
,[PPNo] [NVARCHAR](50) NOT NULL
,[PPDate] [DATETIME] NOT NULL
,[TaxableAmount] [DECIMAL](18, 6) NOT NULL
,[VatAmount] [DECIMAL](18, 6) NOT NULL
,[CustomAgent] [NVARCHAR](50) NULL
,[Transportation] [NVARCHAR](50) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[Cn_No] [VARCHAR](25) NULL
,[Cn_Date] [DATETIME] NULL
,[BankDoc] [NVARCHAR](50) NULL
,CONSTRAINT [PK_Purchase_ImpDoc] PRIMARY KEY CLUSTERED ([PAB_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [UQ__Purchase__2FF6D3DA42E1EEFE] UNIQUE NONCLUSTERED ([PAB_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PB_Term') IS NULL
CREATE TABLE [AMS].[PB_Term] (
[PB_Vno] [NVARCHAR](50) NOT NULL
,[PT_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NOT NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PB_Term] PRIMARY KEY CLUSTERED ([PB_Vno] ASC, [PT_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PBT_Details') IS NULL
CREATE TABLE [AMS].[PBT_Details] (
[PBT_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NOT NULL
,[Slb_Id] [INT] NOT NULL
,[PGrp_Id] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Flight_Date] [DATETIME] NOT NULL
,[Sector] [NVARCHAR](100) NULL
,[Fare_Amount] [DECIMAL](18, 6) NOT NULL
,[FC_Amount] [DECIMAL](18, 6) NOT NULL
,[PSC_Amount] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[Trm_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](500) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[VAT_Amount] [DECIMAL](18, 6) NULL
,[VAT_Rate] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_PBT_Details] PRIMARY KEY CLUSTERED ([PBT_Invoice] ASC, [Invoice_SNo] ASC, [Slb_Id] ASC, [PGrp_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PBT_Master') IS NULL
CREATE TABLE [AMS].[PBT_Master] (
[PBT_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_Date] [DATETIME] NULL
,[Vendor_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_In] [NVARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[Fare_Amount] [DECIMAL](18, 6) NOT NULL
,[FC_Amount] [DECIMAL](18, 6) NOT NULL
,[PSC_Amount] [DECIMAL](18, 6) NOT NULL
,[Dis_Amount] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[Tbl_Amount] [DECIMAL](18, 6) NOT NULL
,[Action_Type] [NVARCHAR](50) NOT NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[CancelBy] [NVARCHAR](50) NULL
,[CancelDate] [NVARCHAR](50) NULL
,[CancelRemarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[FiscalYearId] [INT] NULL
,CONSTRAINT [PK_PBT] PRIMARY KEY CLUSTERED ([PBT_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PBT_Term') IS NULL
CREATE TABLE [AMS].[PBT_Term] (
[PBT_VNo] [NVARCHAR](50) NOT NULL
,[PT_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Term_Type] [char](2) NOT NULL
,[Product_Id] [BIGINT] NOT NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_PBT_Term] PRIMARY KEY CLUSTERED ([PBT_VNo] ASC, [PT_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PC_Details') IS NULL
CREATE TABLE [AMS].[PC_Details] (
[PC_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[Stock_Qty] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[PO_Invoice] [NVARCHAR](50) NULL
,[PO_Sno] [INT] NULL
,[QOT_Invoice] [NVARCHAR](50) NULL
,[QOT_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Rate] [DECIMAL](18, 6) NOT NULL
,[Issue_Qty] [DECIMAL](18, 6) NOT NULL
,[Free_Unit_Id] [INT] NOT NULL
,[Free_Qty] [DECIMAL](18, 6) NOT NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[P_Ledger] [BIGINT] NULL
,[PR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,CONSTRAINT [PK_PC_Details] PRIMARY KEY CLUSTERED ([PC_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PC_Master') IS NULL
CREATE TABLE [AMS].[PC_Master] (
[PC_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [NVARCHAR](10) NULL
,[Vendor_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_In] [NVARCHAR](50) NOT NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[PO_Invoice] [NVARCHAR](250) NULL
,[PO_Date] [DATETIME] NULL
,[QOT_Invoice] [NVARCHAR](250) NULL
,[QOT_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Cur_Id] [INT] NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[Counter_ID] [INT] NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[Tender_Amount] [DECIMAL](18, 6) NOT NULL
,[Change_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[Tbl_Amount] [DECIMAL](18, 6) NOT NULL
,[Action_Type] [NVARCHAR](50) NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [INT] NOT NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Audit_Lock] [BIT] NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,CONSTRAINT [PK_PC] PRIMARY KEY CLUSTERED ([PC_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PC_Term') IS NULL
CREATE TABLE [AMS].[PC_Term] (
[PC_VNo] [NVARCHAR](50) NOT NULL
,[PT_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_PC_Term] PRIMARY KEY CLUSTERED ([PC_VNo] ASC, [PT_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PIN_Details') IS NULL
CREATE TABLE [AMS].[PIN_Details] (
[PIN_Invoice] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_Unit] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit] [INT] NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[StockQty] [DECIMAL](18, 6) NOT NULL
,[Issue_Qty] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,CONSTRAINT [PK_PIN_Details] PRIMARY KEY CLUSTERED ([PIN_Invoice] ASC, [SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PIN_Master') IS NULL
CREATE TABLE [AMS].[PIN_Master] (
[PIN_Invoice] [NVARCHAR](50) NOT NULL
,[PIN_Date] [DATETIME] NOT NULL
,[PIN_Miti] [NVARCHAR](50) NOT NULL
,[Person] [NVARCHAR](50) NOT NULL
,[Sub_Ledger] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[ActionType] [char](10) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Print_value] [INT] NULL
,[CancelBy] [NVARCHAR](50) NULL
,[CancelDate] [DATETIME] NULL
,[CancelRemarks] [NVARCHAR](1024) NULL
,[FiscalYearId] [INT] NOT NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,CONSTRAINT [PK_PIN_Master] PRIMARY KEY CLUSTERED ([PIN_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PO_Details') IS NULL
CREATE TABLE [AMS].[PO_Details] (
[PO_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[Stock_Qty] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[PIN_Invoice] [NVARCHAR](50) NULL
,[PIN_Sno] [INT] NULL
,[PQT_Invoice] [NVARCHAR](50) NULL
,[PQT_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Issue_Qty] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[P_Ledger] [BIGINT] NULL
,[PR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,CONSTRAINT [PK_PO_Details] PRIMARY KEY CLUSTERED ([PO_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PO_Master') IS NULL
CREATE TABLE [AMS].[PO_Master] (
[PO_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [NVARCHAR](10) NULL
,[Vendor_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_In] [NVARCHAR](50) NOT NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[PIN_Invoice] [NVARCHAR](250) NULL
,[PIN_Date] [DATETIME] NULL
,[PQT_Invoice] [NVARCHAR](250) NULL
,[PQT_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[Tbl_Amount] [DECIMAL](18, 6) NOT NULL
,[Action_Type] [NVARCHAR](50) NOT NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,CONSTRAINT [PK_PO] PRIMARY KEY CLUSTERED ([PO_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.PO_Term') IS NULL
CREATE TABLE [AMS].[PO_Term] (
[PO_VNo] [NVARCHAR](50) NOT NULL
,[PT_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_PO_Term] PRIMARY KEY CLUSTERED ([PO_VNo] ASC, [PT_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PostDateCheque') IS NULL
CREATE TABLE [AMS].[PostDateCheque] (
[PDCId] [INT] IDENTITY (1, 1) NOT NULL
,[VoucherNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NOT NULL
,[VoucherMiti] [NVARCHAR](50) NOT NULL
,[VoucherType] [NVARCHAR](50) NOT NULL
,[BankName] [NVARCHAR](500) NOT NULL
,[BranchName] [NVARCHAR](500) NOT NULL
,[ChequeNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[ChqMiti] [NVARCHAR](50) NULL
,[DrawOn] [NVARCHAR](500) NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[LedgerId] [BIGINT] NOT NULL
,[SubLedgerId] [INT] NULL
,[AgentId] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[DepositedBy] [NVARCHAR](50) NULL
,[DepositeDate] [DATETIME] NULL
,[CancelReason] [NVARCHAR](1024) NULL
,[CancelBy] [NVARCHAR](50) NULL
,[CancelDate] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CompanyUnitId] [INT] NULL
,[BranchId] [INT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[VoucherTime] [Date] NULL
,[BankLedgerId] [BIGINT] NULL
,[Status] [NVARCHAR](50) NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,CONSTRAINT [PK__PDC__A96E03572C1E8537] PRIMARY KEY CLUSTERED ([PDCId] ASC, [VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [UQ__PDC__C5DCEA062EFAF1E2] UNIQUE NONCLUSTERED ([VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.PR_Details') IS NULL
CREATE TABLE [AMS].[PR_Details] (
[PR_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NOT NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NOT NULL
,[Stock_Qty] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[PB_Invoice] [NVARCHAR](50) NULL
,[PB_Sno] [INT] NULL
,[Tax_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Rate] [DECIMAL](18, 6) NOT NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[P_Ledger] [BIGINT] NULL
,[PR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PR_Details] PRIMARY KEY CLUSTERED ([PR_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PR_Master') IS NULL
CREATE TABLE [AMS].[PR_Master] (
[PR_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[PB_Invoice] [NVARCHAR](50) NULL
,[PB_Date] [DATETIME] NULL
,[PB_Miti] [VARCHAR](10) NULL
,[Vendor_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_In] [NVARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Change_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [char](10) NOT NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[CancelBy] [NVARCHAR](50) NULL
,[CancelDate] [DATETIME] NULL
,[CancelRemarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PR] PRIMARY KEY CLUSTERED ([PR_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PR_Term') IS NULL
CREATE TABLE [AMS].[PR_Term] (
[PR_VNo] [NVARCHAR](50) NOT NULL
,[PT_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PR_Term_1] PRIMARY KEY CLUSTERED ([PR_VNo] ASC, [PT_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Product') IS NULL
CREATE TABLE [AMS].[Product] (
[PID] [BIGINT] NOT NULL
,[PName] [NVARCHAR](200) NOT NULL
,[PAlias] [NVARCHAR](200) NOT NULL
,[PShortName] [NVARCHAR](50) NOT NULL
,[PType] [NVARCHAR](50) NOT NULL
,[PCategory] [NVARCHAR](50) NOT NULL
,[PUnit] [INT] NULL
,[PAltUnit] [INT] NULL
,[PQtyConv] [DECIMAL](18, 6) NULL
,[PAltConv] [DECIMAL](18, 6) NULL
,[PValTech] [NVARCHAR](50) NULL
,[PSerialno] [BIT] NULL
,[PSizewise] [BIT] NULL
,[PBatchwise] [BIT] NULL
,[PBuyRate] [DECIMAL](18, 6) NULL
,[PSalesRate] [DECIMAL](18, 6) NULL
,[PMargin1] [DECIMAL](18, 6) NULL
,[TradeRate] [DECIMAL](18, 6) NULL
,[PMargin2] [DECIMAL](18, 6) NULL
,[PMRP] [DECIMAL](18, 6) NULL
,[PGrpId] [INT] NULL
,[PSubGrpId] [INT] NULL
,[PTax] [DECIMAL](18, 6) NOT NULL
,[PMin] [DECIMAL](18, 6) NULL
,[PMax] [DECIMAL](18, 6) NULL
,[CmpId] [INT] NULL
,[CmpId1] [INT] NULL
,[CmpId2] [INT] NULL
,[CmpId3] [INT] NULL
,[Branch_ID] [INT] NOT NULL
,[CmpUnit_ID] [INT] NULL
,[PPL] [BIGINT] NULL
,[PPR] [BIGINT] NULL
,[PSL] [BIGINT] NULL
,[PSR] [BIGINT] NULL
,[PL_Opening] [BIGINT] NULL
,[PL_Closing] [BIGINT] NULL
,[BS_Closing] [BIGINT] NULL
,[PImage] [IMAGE] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[Status] [BIT] NOT NULL
,[BeforeBuyRate] [DECIMAL](18, 6) NULL
,[BeforeSalesRate] [DECIMAL](18, 6) NULL
,[Barcode] [NVARCHAR](100) NULL
,[ChasisNo] [NVARCHAR](100) NULL
,[EngineNo] [NVARCHAR](100) NULL
,[VHColor] [NVARCHAR](100) NULL
,[VHModel] [NVARCHAR](100) NULL
,[VHNumber] [NVARCHAR](100) NULL
,[Barcode1] [NVARCHAR](100) NULL
,[Barcode2] [NVARCHAR](100) NULL
,[Barcode3] [NVARCHAR](100) NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([PID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductDesc] UNIQUE NONCLUSTERED ([PName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductShortName] UNIQUE NONCLUSTERED ([PShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.ProductClosingRate') IS NULL
CREATE TABLE [AMS].[ProductClosingRate] (
[PCRate_Id] [BIGINT] IDENTITY (1, 1) NOT NULL
,[Product_Id] [BIGINT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Date_Type] [VARCHAR](5) NULL
,[Month_Date] [DATETIME] NULL
,[Month_Miti] [VARCHAR](50) NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NOT NULL
,CONSTRAINT [PK_ProductClosingRate] PRIMARY KEY CLUSTERED ([PCRate_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.ProductGroup') IS NULL
CREATE TABLE [AMS].[ProductGroup] (
[PGrpId] [INT] NOT NULL
,[GrpName] [NVARCHAR](200) NOT NULL
,[GrpCode] [NVARCHAR](50) NOT NULL
,[GMargin] [DECIMAL](18, 6) NULL
,[Gprinter] [NVARCHAR](50) NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED ([PGrpId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductGroupDesc] UNIQUE NONCLUSTERED ([GrpName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductGroupShortName] UNIQUE NONCLUSTERED ([GrpCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.ProductOpening') IS NULL
CREATE TABLE [AMS].[ProductOpening] (
[Voucher_No] [NVARCHAR](50) NOT NULL
,[Serial_No] [INT] NOT NULL
,[OP_Date] [DATETIME] NOT NULL
,[OP_Miti] [VARCHAR](10) NOT NULL
,[Product_Id] [BIGINT] NOT NULL
,[Godown_Id] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Currency_ID] [INT] NULL
,[Currency_Rate] [DECIMAL](18, 6) NULL
,[AltQty] [DECIMAL](18, 6) NULL
,[AltUnit] [DECIMAL](18, 6) NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[QtyUnit] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[LocalRate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[LocalAmount] [DECIMAL](18, 6) NOT NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [NVARCHAR](50) NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.ProductSubGroup') IS NULL
CREATE TABLE [AMS].[ProductSubGroup] (
[PSubGrpId] [INT] NOT NULL
,[SubGrpName] [NVARCHAR](80) NOT NULL
,[ShortName] [NVARCHAR](50) NOT NULL
,[GrpId] [INT] NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[Status] [BIT] NOT NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,CONSTRAINT [PK_ProductSubGroup] PRIMARY KEY CLUSTERED ([PSubGrpId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductSubGroupDesc] UNIQUE NONCLUSTERED ([SubGrpName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductSubGroupShortName] UNIQUE NONCLUSTERED ([ShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.ProductUnit') IS NULL
CREATE TABLE [AMS].[ProductUnit] (
[UID] [INT] NOT NULL
,[UnitName] [NVARCHAR](50) NOT NULL
,[UnitCode] [NVARCHAR](50) NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[Status] [BIT] NOT NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,CONSTRAINT [PK_ProductUnit] PRIMARY KEY CLUSTERED ([UID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductUnitDesc] UNIQUE NONCLUSTERED ([UnitName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ProductUnitShortName] UNIQUE NONCLUSTERED ([UnitCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PROV_CB_Details') IS NULL
CREATE TABLE [AMS].[PROV_CB_Details] (
[Voucher_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Debit] [DECIMAL](18, 6) NOT NULL
,[Credit] [DECIMAL](18, 6) NOT NULL
,[LocalDebit] [DECIMAL](18, 6) NOT NULL
,[LocalCredit] [DECIMAL](18, 6) NOT NULL
,[Narration] [VARCHAR](1024) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Party_No] [NVARCHAR](50) NULL
,[Invoice_Date] [DATETIME] NULL
,[Invoice_Miti] [NVARCHAR](50) NULL
,[VatLedger_Id] [BIGINT] NULL
,[PanNo] [INT] NULL
,[Vat_Reg] [BIT] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.PROV_CB_Master') IS NULL
CREATE TABLE [AMS].[PROV_CB_Master] (
[VoucherMode] [char](10) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [NVARCHAR](10) NOT NULL
,[Voucher_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[VoucherType] [NVARCHAR](50) NOT NULL
,[Ledger_ID] [BIGINT] NOT NULL
,[CheqNo] [NVARCHAR](50) NULL
,[CheqDate] [DATETIME] NULL
,[CheqMiti] [NVARCHAR](10) NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [MONEY] NOT NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[ClearingBy] [NVARCHAR](50) NULL
,[ClearingDate] [DATETIME] NULL
,[PrintValue] [INT] NULL
,[IsPosted] [BIT] NULL
,[PostedBy] [NVARCHAR](50) NULL
,[PostedDate] [DATETIME] NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,CONSTRAINT [PK_PROV_CB_Master] PRIMARY KEY CLUSTERED ([Voucher_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PRT_Term') IS NULL
CREATE TABLE [AMS].[PRT_Term] (
[PRT_ID] [INT] NOT NULL
,[Order_No] [INT] NOT NULL
,[Module] [char](2) NOT NULL
,[PT_Name] [NVARCHAR](50) NOT NULL
,[PT_Type] [char](2) NOT NULL
,[Ledger] [BIGINT] NOT NULL
,[PT_Basis] [char](2) NOT NULL
,[PT_Sign] [char](1) NOT NULL
,[PT_Condition] [char](1) NOT NULL
,[PT_Rate] [DECIMAL](18, 6) NULL
,[PT_Branch] [INT] NOT NULL
,[PT_CompanyUnit] [INT] NULL
,[PT_Profitability] [BIT] NULL
,[PT_Supess] [BIT] NULL
,[PT_Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_PR_Term] PRIMARY KEY CLUSTERED ([PRT_ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_PR_Term] UNIQUE NONCLUSTERED ([Order_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PT_Term') IS NULL
CREATE TABLE [AMS].[PT_Term] (
[PT_Id] [INT] NOT NULL
,[Order_No] [INT] NOT NULL
,[Module] [char](4) NOT NULL
,[PT_Name] [NVARCHAR](50) NOT NULL
,[PT_Type] [char](2) NOT NULL
,[Ledger] [BIGINT] NOT NULL
,[PT_Basis] [char](2) NOT NULL
,[PT_Sign] [char](1) NOT NULL
,[PT_Condition] [char](1) NOT NULL
,[PT_Rate] [DECIMAL](18, 6) NULL
,[PT_Branch] [INT] NOT NULL
,[PT_CompanyUnit] [INT] NULL
,[PT_Profitability] [BIT] NOT NULL
,[PT_Supess] [BIT] NOT NULL
,[PT_Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_PT_TermId] PRIMARY KEY CLUSTERED ([PT_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_PT_TermDesc] UNIQUE NONCLUSTERED ([PT_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_PT_TermOrderNo] UNIQUE NONCLUSTERED ([Order_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.PurchaseSetting') IS NULL
CREATE TABLE [AMS].[PurchaseSetting] (
[PurId] [TINYINT] NOT NULL
,[PBLedgerId] [BIGINT] NULL
,[PRLedgerId] [BIGINT] NULL
,[PBVatTerm] [INT] NULL
,[PBDiscountTerm] [INT] NULL
,[PBProductDiscountTerm] [INT] NULL
,[PBAdditionalTerm] [INT] NULL
,[PBDateChange] [BIT] NULL
,[PBCreditDays] [char](1) NULL
,[PBCreditLimit] [char](1) NULL
,[PBCarryRate] [BIT] NULL
,[PBChangeRate] [BIT] NULL
,[PBLastRate] [BIT] NULL
,[POEnable] [BIT] NULL
,[POMandetory] [BIT] NULL
,[PCEnable] [BIT] NULL
,[PCMandetory] [BIT] NULL
,[PBSublegerEnable] [BIT] NULL
,[PBSubledgerMandetory] [BIT] NULL
,[PBAgentEnable] [BIT] NULL
,[PBAgentMandetory] [BIT] NULL
,[PBDepartmentEnable] [BIT] NULL
,[PBDepartmentMandetory] [BIT] NULL
,[PBCurrencyEnable] [BIT] NULL
,[PBCurrencyMandetory] [BIT] NULL
,[PBCurrencyRateChange] [BIT] NULL
,[PBGodownEnable] [BIT] NULL
,[PBGodownMandetory] [BIT] NULL
,[PBAlternetUnitEnable] [BIT] NULL
,[PBIndent] [BIT] NULL
,[PBNarration] [BIT] NULL
,[PBBasicAmount] [BIT] NULL
,CONSTRAINT [PK_PurchaseSetting] PRIMARY KEY CLUSTERED ([PurId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.RACK') IS NULL
CREATE TABLE [AMS].[RACK] (
[RID] [INT] NOT NULL
,[RName] [NVARCHAR](80) NULL
,[RCode] [NVARCHAR](50) NULL
,[Location] [NVARCHAR](50) NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[CompUnit] [INT] NULL
,[BranchUnit] [INT] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,PRIMARY KEY CLUSTERED ([RID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.ReportTemplate') IS NULL
CREATE TABLE [AMS].[ReportTemplate] (
[ID] [INT] IDENTITY (1, 1) NOT NULL
,[FileName] [NVARCHAR](MAX) NOT NULL
,[FullPath] [NVARCHAR](MAX) NOT NULL
,[FromDate] [DATETIME] NULL
,[ToDate] [DATETIME] NULL
,[Reports_Type] [char](1) NULL
,[Report_Name] [NVARCHAR](MAX) NULL
,CONSTRAINT [PK_ReportTemplate] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.SalesSetting') IS NULL
CREATE TABLE [AMS].[SalesSetting] (
[SalesId] [TINYINT] NOT NULL
,[SBLedgerId] [BIGINT] NULL
,[SRLedgerId] [BIGINT] NULL
,[SBVatTerm] [INT] NULL
,[SBDiscountTerm] [INT] NULL
,[SBProductDiscountTerm] [INT] NULL
,[SBAdditionalTerm] [INT] NULL
,[SBServiceCharge] [INT] NULL
,[SBDateChange] [BIT] NULL
,[SBCreditDays] [char](1) NULL
,[SBCreditLimit] [char](1) NULL
,[SBCarryRate] [BIT] NULL
,[SBChangeRate] [BIT] NULL
,[SBLastRate] [BIT] NULL
,[SBQuotationEnable] [BIT] NULL
,[SBQuotationMandetory] [BIT] NULL
,[SBDispatchOrderEnable] [BIT] NULL
,[SBDispatchMandetory] [BIT] NULL
,[SOEnable] [BIT] NULL
,[SOMandetory] [BIT] NULL
,[SCEnable] [BIT] NULL
,[SCMandetory] [BIT] NULL
,[SBSublegerEnable] [BIT] NULL
,[SBSubledgerMandetory] [BIT] NULL
,[SBAgentEnable] [BIT] NULL
,[SBAgentMandetory] [BIT] NULL
,[SBDepartmentEnable] [BIT] NULL
,[SBDepartmentMandetory] [BIT] NULL
,[SBCurrencyEnable] [BIT] NULL
,[SBCurrencyMandetory] [BIT] NULL
,[SBCurrencyRateChange] [BIT] NULL
,[SBGodownEnable] [BIT] NULL
,[SBGodownMandetory] [BIT] NULL
,[SBAlternetUnitEnable] [BIT] NULL
,[SBIndent] [BIT] NULL
,[SBNarration] [BIT] NULL
,[SBBasicAmount] [BIT] NULL
,[SBAviableStock] [BIT] NULL
,[SBReturnValue] [BIT] NULL
,[PartyInfo] [BIT] NULL
,CONSTRAINT [PK_SalesSetting] PRIMARY KEY CLUSTERED ([SalesId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SampleCosting_Details') IS NULL
CREATE TABLE [AMS].[SampleCosting_Details] (
[Costing_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Product_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[CC_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[AltUnit_Id] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](1024) NULL
,[CCExpenses_No] [NVARCHAR](50) NULL
,[CCExpenses_SNo] [INT] NULL
,CONSTRAINT [PK_SampleCosting_Details] PRIMARY KEY CLUSTERED ([Costing_No] ASC, [SNo] ASC, [Product_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SampleCosting_Master') IS NULL
CREATE TABLE [AMS].[SampleCosting_Master] (
[Costing_No] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[CCExpenses_No] [NVARCHAR](50) NULL
,[CCExpenses_Date] [DATETIME] NULL
,[FGProduct_Id] [BIGINT] NOT NULL
,[Cost_Rate] [DECIMAL](18, 6) NULL
,[Gdn_Id] [INT] NULL
,[CC_Id] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Total_Qty] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,CONSTRAINT [PK_SampleCosting_Master] PRIMARY KEY CLUSTERED ([Costing_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SB_Details') IS NULL
CREATE TABLE [AMS].[SB_Details] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[SO_Invoice] [NVARCHAR](50) NULL
,[SO_Sno] [NUMERIC](18, 0) NULL
,[SC_Invoice] [NVARCHAR](50) NULL
,[SC_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[PDiscountRate] [DECIMAL](18, 6) NULL
,[PDiscount] [DECIMAL](18, 6) NULL
,[BDiscountRate] [DECIMAL](18, 6) NULL
,[BDiscount] [DECIMAL](18, 6) NULL
,[ServiceChargeRate] [DECIMAL](18, 6) NULL
,[ServiceCharge] [DECIMAL](18, 6) NULL
,[MaterialPost] [char](1) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_SB_Details] PRIMARY KEY CLUSTERED ([SB_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SB_ExchangeDetails') IS NULL
CREATE TABLE [AMS].[SB_ExchangeDetails] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NULL
,[P_Id] [BIGINT] NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[ExchangeGLD] [BIGINT] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.SB_Master') IS NULL
CREATE TABLE [AMS].[SB_Master] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](50) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_Mode] [NVARCHAR](50) NOT NULL
,[Payment_Mode] [VARCHAR](50) NOT NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[SO_Invoice] [NVARCHAR](250) NULL
,[SO_Date] [DATETIME] NULL
,[SC_Invoice] [NVARCHAR](250) NULL
,[SC_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[FiscalYearId] [INT] NOT NULL
,[DoctorId] [INT] NULL
,[PatientId] [BIGINT] NULL
,[HDepartmentId] [INT] NULL
,[TableId] [INT] NULL
,[MShipId] [INT] NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_SB] PRIMARY KEY CLUSTERED ([SB_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SB_Master_OtherDetails') IS NULL
CREATE TABLE [AMS].[SB_Master_OtherDetails] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Transport] [NVARCHAR](255) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[BiltyNo] [NVARCHAR](50) NULL
,[Package] [NVARCHAR](100) NULL
,[BiltyDate] [Date] NULL
,[BiltyType] [NVARCHAR](50) NULL
,[Driver] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LicenseNo] [NVARCHAR](50) NULL
,[MailingAddress] [NVARCHAR](500) NULL
,[MCity] [NVARCHAR](50) NULL
,[MState] [NVARCHAR](50) NULL
,[MCountry] [NVARCHAR](50) NULL
,[MEmail] [NVARCHAR](50) NULL
,[ShippingAddress] [NVARCHAR](500) NULL
,[SCity] [NVARCHAR](50) NULL
,[SState] [NVARCHAR](50) NULL
,[SCountry] [NVARCHAR](50) NULL
,[SEmail] [NVARCHAR](50) NULL
,[ContractNo] [NVARCHAR](50) NULL
,[ContractDate] [Date] NULL
,[ExportInvoice] [NVARCHAR](50) NULL
,[ExportInvoiceDate] [Date] NULL
,[VendorOrderNo] [NVARCHAR](50) NULL
,[BankDetails] [NVARCHAR](100) NULL
,[LcNumber] [NVARCHAR](50) NULL
,[CustomDetails] [NVARCHAR](100) NULL
,CONSTRAINT [PK_SB_Master_OtherDetails] PRIMARY KEY CLUSTERED ([SB_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SB_Term') IS NULL
CREATE TABLE [AMS].[SB_Term] (
[SB_VNo] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NOT NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.SBT_Details') IS NULL
CREATE TABLE [AMS].[SBT_Details] (
[SBT_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[Slb_Id] [INT] NOT NULL
,[PGrp_Id] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Flight_Date] [DATETIME] NULL
,[Sector] [NVARCHAR](100) NULL
,[Fare_Amount] [DECIMAL](18, 6) NOT NULL
,[FC_Amount] [DECIMAL](18, 6) NOT NULL
,[PSC_Amount] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[Trm_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](500) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[VAT_Amount] [DECIMAL](18, 6) NULL
,[VAT_Rate] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_SBT_Details] PRIMARY KEY CLUSTERED ([SBT_Invoice] ASC, [Invoice_SNo] ASC, [Slb_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SBT_Master') IS NULL
CREATE TABLE [AMS].[SBT_Master] (
[SBT_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_In] [NVARCHAR](50) NOT NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[Fare_Amount] [DECIMAL](18, 6) NOT NULL
,[FC_Amount] [DECIMAL](18, 6) NOT NULL
,[PSC_Amount] [DECIMAL](18, 6) NOT NULL
,[Dis_Amount] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[Remarks] [NVARCHAR](500) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[FiscalYearId] [INT] NOT NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,CONSTRAINT [PK_SBT] PRIMARY KEY CLUSTERED ([SBT_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SBT_Term') IS NULL
CREATE TABLE [AMS].[SBT_Term] (
[SBT_VNo] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_SBT_Term] PRIMARY KEY CLUSTERED ([SBT_VNo] ASC, [ST_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SC_Details') IS NULL
CREATE TABLE [AMS].[SC_Details] (
[SC_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[QOT_Invoice] [NVARCHAR](50) NULL
,[QOT_SNo] [NUMERIC](18, 0) NULL
,[SO_Invoice] [NVARCHAR](50) NULL
,[SO_Sno] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Issue_Qty] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[AltIssue_Qty] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_SC_Details] PRIMARY KEY CLUSTERED ([SC_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SC_Master') IS NULL
CREATE TABLE [AMS].[SC_Master] (
[SC_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [VARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_Date] [DATETIME] NULL
,[Ref_Miti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_Mode] [NVARCHAR](50) NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[QOT_Invoice] [NVARCHAR](250) NULL
,[QOT_Date] [DATETIME] NULL
,[SO_Invoice] [NVARCHAR](250) NULL
,[SO_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 8) NOT NULL
,[B_Amount] [DECIMAL](18, 8) NOT NULL
,[T_Amount] [DECIMAL](18, 8) NOT NULL
,[N_Amount] [DECIMAL](18, 0) NOT NULL
,[LN_Amount] [DECIMAL](18, 0) NOT NULL
,[V_Amount] [DECIMAL](18, 0) NULL
,[Tbl_Amount] [DECIMAL](18, 0) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[R_Invoice] [BIT] NULL
,[No_Print] [DECIMAL](18, 0) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,CONSTRAINT [PK_SC] PRIMARY KEY CLUSTERED ([SC_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SC_Master_OtherDetails') IS NULL
CREATE TABLE [AMS].[SC_Master_OtherDetails] (
[SC_Invoice] [NVARCHAR](50) NOT NULL
,[Transport] [NVARCHAR](255) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[BiltyNo] [NVARCHAR](50) NULL
,[Package] [NVARCHAR](100) NULL
,[BiltyDate] [Date] NULL
,[BiltyType] [NVARCHAR](50) NULL
,[Driver] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LicenseNo] [NVARCHAR](50) NULL
,[MailingAddress] [NVARCHAR](500) NULL
,[MCity] [NVARCHAR](50) NULL
,[MState] [NVARCHAR](50) NULL
,[MCountry] [NVARCHAR](50) NULL
,[MEmail] [NVARCHAR](50) NULL
,[ShippingAddress] [NVARCHAR](500) NULL
,[SCity] [NVARCHAR](50) NULL
,[SState] [NVARCHAR](50) NULL
,[SCountry] [NVARCHAR](50) NULL
,[SEmail] [NVARCHAR](50) NULL
,[ContractNo] [NVARCHAR](50) NULL
,[ContractDate] [Date] NULL
,[ExportInvoice] [NVARCHAR](50) NULL
,[ExportInvoiceDate] [Date] NULL
,[VendorOrderNo] [NVARCHAR](50) NULL
,[BankDetails] [NVARCHAR](100) NULL
,[LcNumber] [NVARCHAR](50) NULL
,[CustomDetails] [NVARCHAR](100) NULL
,CONSTRAINT [PK_SC_Master_OtherDetails] PRIMARY KEY CLUSTERED ([SC_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SC_Term') IS NULL
CREATE TABLE [AMS].[SC_Term] (
[SC_Vno] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_SC_Term] PRIMARY KEY CLUSTERED ([SC_Vno] ASC, [ST_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Scheme_Details') IS NULL
CREATE TABLE [AMS].[Scheme_Details] (
[Scheme_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[P_Code] [BIGINT] NOT NULL
,[P_Group] [INT] NULL
,[P_SubGroup] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[PercentageValue] [DECIMAL](18, 6) NOT NULL
,[DiscountValue] [DECIMAL](18, 6) NOT NULL
,[MinValue] [DECIMAL](18, 6) NOT NULL
,[MaxValue] [DECIMAL](18, 6) NOT NULL
,[FreeQty] [DECIMAL](18, 6) NULL
,[ProductRate] [DECIMAL](18, 6) NOT NULL
,[LessPercentage] [DECIMAL](18, 6) NOT NULL
,[DisPercentage] [DECIMAL](18, 6) NOT NULL
,CONSTRAINT [PK_Scheme_Details] PRIMARY KEY CLUSTERED ([Scheme_Id] ASC, [SNo] ASC, [P_Code] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Scheme_Master') IS NULL
CREATE TABLE [AMS].[Scheme_Master] (
[Scheme_Id] [INT] IDENTITY (1, 1) NOT NULL
,[Scheme_Name] [VARCHAR](64) NOT NULL
,[FromDate] [DATETIME] NULL
,[FromTo] [DATETIME] NULL
,[RatedAs] [VARCHAR](32) NULL
,[Basis] [VARCHAR](32) NULL
,[MannualOveride] [BIT] NULL
,[ConsiderReturn] [BIT] NULL
,[FromMiti] [VARCHAR](10) NOT NULL
,[ToMiti] [VARCHAR](10) NOT NULL
,[SchemeDate] [DATETIME] NOT NULL
,[SchemeMiti] [VARCHAR](10) NOT NULL
,[ProductTerm] [VARCHAR](10) NULL
,[ProdUnit] [VARCHAR](5) NULL
,[BillTerm] [VARCHAR](10) NULL
,[Term_Basis] [VARCHAR](64) NULL
,[ApplyFor] [VARCHAR](32) NULL
,[BranchId] [INT] NULL
,[CompanyUnitId] [INT] NULL
,[Remarks] [VARCHAR](512) NULL
,[FiscalYearId] [INT] NOT NULL
,CONSTRAINT [PK__Scheme_M__5967B8AD2C29EDCC] PRIMARY KEY CLUSTERED ([Scheme_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SectorMaster') IS NULL
CREATE TABLE [AMS].[SectorMaster] (
[SID] [INT] NOT NULL
,[SectorDesc] [NVARCHAR](50) NULL
,CONSTRAINT [PK_SectorMaster] PRIMARY KEY CLUSTERED ([SID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [Description] UNIQUE NONCLUSTERED ([SectorDesc] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SeniorAgent') IS NULL
CREATE TABLE [AMS].[SeniorAgent] (
[SAgentId] [INT] NOT NULL
,[SAgent] [NVARCHAR](200) NOT NULL
,[SAgentCode] [NVARCHAR](50) NOT NULL
,[PhoneNo] [NVARCHAR](50) NOT NULL
,[Address] [NVARCHAR](50) NOT NULL
,[Comm] [DECIMAL](18, 6) NULL
,[GLID] [BIGINT] NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_SeniorAgent] PRIMARY KEY CLUSTERED ([SAgentId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_SeniorAgentDesc] UNIQUE NONCLUSTERED ([SAgent] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_SeniorAgentShortName] UNIQUE NONCLUSTERED ([SAgentCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SMS_CONFIG') IS NULL
CREATE TABLE [AMS].[SMS_CONFIG] (
[SMSCONFIG_ID] [INT] IDENTITY (1, 1) NOT NULL
,[TOKEN] [NVARCHAR](MAX) NOT NULL
,[IsCashBank] [BIT] NULL
,[IsJournalVoucher] [BIT] NULL
,[IsSalesReturn] [BIT] NULL
,[IsSalesInvoice] [BIT] NULL
,[IsPurchaseInvoice] [BIT] NULL
,[IsPurchaseReturn] [BIT] NULL
,[AlternetNumber] [NVARCHAR](MAX) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
IF OBJECT_ID('AMS.SO_Details') IS NULL
CREATE TABLE [AMS].[SO_Details] (
[SO_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[IND_Invoice] [NVARCHAR](50) NULL
,[IND_Sno] [INT] NULL
,[QOT_Invoice] [NVARCHAR](50) NULL
,[QOT_SNo] [INT] NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Issue_Qty] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[Notes] [NVARCHAR](500) NULL
,[PrintedItem] [BIT] NULL
,[PrintKOT] [BIT] NULL
,[OrderTime] [DATETIME] NULL
,[Print_Time] [DATETIME] NULL
,[Is_Canceled] [BIT] NULL
,[CancelNotes] [NVARCHAR](500) NULL
,[PDiscountRate] [DECIMAL](18, 6) NULL
,[PDiscount] [DECIMAL](18, 6) NULL
,[BDiscountRate] [DECIMAL](18, 6) NULL
,[BDiscount] [DECIMAL](18, 6) NULL
,[ServiceChargeRate] [DECIMAL](18, 6) NULL
,[ServiceCharge] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_SO_Details] PRIMARY KEY CLUSTERED ([SO_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SO_Master') IS NULL
CREATE TABLE [AMS].[SO_Master] (
[SO_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [VARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_Date] [DATETIME] NULL
,[Ref_Miti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_Mode] [NVARCHAR](50) NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[IND_Invoice] [NVARCHAR](250) NULL
,[IND_Date] [DATETIME] NULL
,[QOT_Invoice] [NVARCHAR](250) NULL
,[QOT_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[TableId] [INT] NULL
,[CombineTableId] [NVARCHAR](500) NULL
,[NoOfPerson] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NOT NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[Enter_By] [NVARCHAR](50) NULL
,[Enter_Date] [DATETIME] NULL
,CONSTRAINT [PK_SO] PRIMARY KEY CLUSTERED ([SO_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SO_Master_OtherDetails') IS NULL
CREATE TABLE [AMS].[SO_Master_OtherDetails] (
[SO_Invoice] [NVARCHAR](50) NOT NULL
,[Transport] [NVARCHAR](255) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[BiltyNo] [NVARCHAR](50) NULL
,[Package] [NVARCHAR](100) NULL
,[BiltyDate] [Date] NULL
,[BiltyType] [NVARCHAR](50) NULL
,[Driver] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LicenseNo] [NVARCHAR](50) NULL
,[MailingAddress] [NVARCHAR](500) NULL
,[MCity] [NVARCHAR](50) NULL
,[MState] [NVARCHAR](50) NULL
,[MCountry] [NVARCHAR](50) NULL
,[MEmail] [NVARCHAR](50) NULL
,[ShippingAddress] [NVARCHAR](500) NULL
,[SCity] [NVARCHAR](50) NULL
,[SState] [NVARCHAR](50) NULL
,[SCountry] [NVARCHAR](50) NULL
,[SEmail] [NVARCHAR](50) NULL
,[ContractNo] [NVARCHAR](50) NULL
,[ContractDate] [Date] NULL
,[ExportInvoice] [NVARCHAR](50) NULL
,[ExportInvoiceDate] [Date] NULL
,[VendorOrderNo] [NVARCHAR](50) NULL
,[BankDetails] [NVARCHAR](100) NULL
,[LcNumber] [NVARCHAR](50) NULL
,[CustomDetails] [NVARCHAR](100) NULL
,CONSTRAINT [PK_SO_Master_OtherDetails] PRIMARY KEY CLUSTERED ([SO_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SO_Term') IS NULL
CREATE TABLE [AMS].[SO_Term] (
[SO_Vno] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_SO_Term] PRIMARY KEY CLUSTERED ([SO_Vno] ASC, [ST_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SQ_Details') IS NULL
CREATE TABLE [AMS].[SQ_Details] (
[SQ_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[IND_Invoice] [NVARCHAR](50) NULL
,[IND_Sno] [NUMERIC](18, 0) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Issue_Qty] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[PG_Id] [INT] NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,CONSTRAINT [PK_SQ_Details] PRIMARY KEY CLUSTERED ([SQ_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SQ_Master') IS NULL
CREATE TABLE [AMS].[SQ_Master] (
[SQ_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](50) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[Expiry_Date] [DATETIME] NOT NULL
,[Ref_VNo] [NVARCHAR](50) NULL
,[Ref_VDate] [DATETIME] NULL
,[Ref_VMiti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_Mode] [NVARCHAR](50) NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[IND_Invoice] [NVARCHAR](250) NULL
,[IND_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[FiscalYearId] [INT] NOT NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,CONSTRAINT [PK_SQ_Master] PRIMARY KEY CLUSTERED ([SQ_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SQ_Term') IS NULL
CREATE TABLE [AMS].[SQ_Term] (
[SQ_Vno] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,CONSTRAINT [PK_SQ_Term] PRIMARY KEY CLUSTERED ([SQ_Vno] ASC, [ST_Id] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SR_Details') IS NULL
CREATE TABLE [AMS].[SR_Details] (
[SR_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [NUMERIC](18, 0) NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[SB_Invoice] [NVARCHAR](50) NULL
,[SB_Sno] [NUMERIC](18, 0) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_SR_Details] PRIMARY KEY CLUSTERED ([SR_Invoice] ASC, [Invoice_SNo] ASC, [P_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SR_Master') IS NULL
CREATE TABLE [AMS].[SR_Master] (
[SR_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [VARCHAR](10) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[SB_Invoice] [NVARCHAR](50) NULL
,[SB_Date] [DATETIME] NULL
,[SB_Miti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NULL
,[Invoice_Mode] [NVARCHAR](50) NULL
,[Payment_Mode] [VARCHAR](50) NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[FiscalYearId] [INT] NOT NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_SR] PRIMARY KEY CLUSTERED ([SR_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SR_Master_OtherDetails') IS NULL
CREATE TABLE [AMS].[SR_Master_OtherDetails] (
[SR_Invoice] [NVARCHAR](50) NOT NULL
,[Transport] [NVARCHAR](255) NULL
,[VechileNo] [NVARCHAR](50) NULL
,[BiltyNo] [NVARCHAR](50) NULL
,[Package] [NVARCHAR](100) NULL
,[BiltyDate] [Date] NULL
,[BiltyType] [NVARCHAR](50) NULL
,[Driver] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[LicenseNo] [NVARCHAR](50) NULL
,[MailingAddress] [NVARCHAR](500) NULL
,[MCity] [NVARCHAR](50) NULL
,[MState] [NVARCHAR](50) NULL
,[MCountry] [NVARCHAR](50) NULL
,[MEmail] [NVARCHAR](50) NULL
,[ShippingAddress] [NVARCHAR](500) NULL
,[SCity] [NVARCHAR](50) NULL
,[SState] [NVARCHAR](50) NULL
,[SCountry] [NVARCHAR](50) NULL
,[SEmail] [NVARCHAR](50) NULL
,[ContractNo] [NVARCHAR](50) NULL
,[ContractDate] [Date] NULL
,[ExportInvoice] [NVARCHAR](50) NULL
,[ExportInvoiceDate] [Date] NULL
,[VendorOrderNo] [NVARCHAR](50) NULL
,[BankDetails] [NVARCHAR](100) NULL
,[LcNumber] [NVARCHAR](50) NULL
,[CustomDetails] [NVARCHAR](100) NULL
,CONSTRAINT [PK_SR_Master_OtherDetails] PRIMARY KEY CLUSTERED ([SR_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SR_Term') IS NULL
CREATE TABLE [AMS].[SR_Term] (
[SR_VNo] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Term_Type] [char](2) NOT NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_SR_Term] PRIMARY KEY CLUSTERED ([SR_VNo] ASC, [ST_Id] ASC, [SNo] ASC, [Term_Type] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SRT_Term') IS NULL
CREATE TABLE [AMS].[SRT_Term] (
[SRT_ID] [INT] NOT NULL
,[Order_No] [INT] NOT NULL
,[Module] [char](2) NOT NULL
,[ST_Name] [NVARCHAR](50) NOT NULL
,[ST_Type] [char](2) NOT NULL
,[Ledger] [BIGINT] NOT NULL
,[ST_Basis] [char](2) NOT NULL
,[ST_Sign] [char](1) NOT NULL
,[ST_Condition] [char](1) NOT NULL
,[ST_Rate] [DECIMAL](18, 6) NULL
,[ST_Branch] [INT] NOT NULL
,[ST_CompanyUnit] [INT] NULL
,[ST_Profitability] [BIT] NULL
,[ST_Supess] [BIT] NULL
,[ST_Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_SRT_Term] PRIMARY KEY CLUSTERED ([SRT_ID] ASC, [Order_No] ASC, [Module] ASC, [ST_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_SRT_Term] UNIQUE NONCLUSTERED ([Order_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.ST_Term') IS NULL
CREATE TABLE [AMS].[ST_Term] (
[ST_Id] [INT] NOT NULL
,[Order_No] [INT] NOT NULL
,[Module] [char](4) NOT NULL
,[ST_Name] [NVARCHAR](50) NOT NULL
,[ST_Type] [char](2) NOT NULL
,[Ledger] [BIGINT] NOT NULL
,[ST_Basis] [char](2) NOT NULL
,[ST_Sign] [char](1) NOT NULL
,[ST_Condition] [char](1) NOT NULL
,[ST_Rate] [DECIMAL](18, 6) NULL
,[ST_Branch] [INT] NOT NULL
,[ST_CompanyUnit] [INT] NULL
,[ST_Profitability] [BIT] NULL
,[ST_Supess] [BIT] NULL
,[ST_Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_ST_Term] PRIMARY KEY CLUSTERED ([ST_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ST_TermDesc] UNIQUE NONCLUSTERED ([ST_Name] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_ST_TermOrderNo] UNIQUE NONCLUSTERED ([Order_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.STA_Details') IS NULL
CREATE TABLE [AMS].[STA_Details] (
[StockAdjust_No] [NVARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[GodownId] [INT] NULL
,[AdjType] [char](1) NOT NULL
,[AltQty] [DECIMAL](18, 6) NOT NULL
,[AltUnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[UnitId] [INT] NULL
,[AltStockQty] [DECIMAL](18, 6) NOT NULL
,[StockQty] [DECIMAL](18, 6) NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[NetAmount] [DECIMAL](18, 6) NOT NULL
,[AddDesc] [NVARCHAR](1024) NULL
,[DepartmentId] [INT] NULL
,[StConvRatio] [DECIMAL](18, 6) NULL
,[PhyStkNo] [VARCHAR](15) NULL
,[PhyStkSno] [INT] NULL
,CONSTRAINT [PK_STA_Details] PRIMARY KEY CLUSTERED ([StockAdjust_No] ASC, [SNo] ASC, [ProductId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.STA_Master') IS NULL
CREATE TABLE [AMS].[STA_Master] (
[StockAdjust_No] [NVARCHAR](50) NOT NULL
,[VDate] [DATETIME] NOT NULL
,[Vtime] [DATETIME] NOT NULL
,[VMiti] [VARCHAR](10) NULL
,[DepartmentId] [INT] NULL
,[Barcode] [VARCHAR](10) NULL
,[PhyStockNo] [NVARCHAR](50) NULL
,[Posting] [char](1) NULL
,[Export] [char](1) NULL
,[PostedBy] [NVARCHAR](50) NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[AuditBy] [NVARCHAR](50) NULL
,[AuthorizeBy] [NVARCHAR](50) NULL
,[AuditDate] [DATETIME] NULL
,[PostedDate] [DATETIME] NULL
,[AuthorizeDate] [DATETIME] NULL
,[Remarks] [NVARCHAR](1024) NULL
,[Status] [NVARCHAR](50) NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[PrintValue] [INT] NULL
,[AuthorizeRemarks] [NVARCHAR](1024) NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,CONSTRAINT [PK__STA_Mast__C5DCEA07C4246EFA] PRIMARY KEY CLUSTERED ([StockAdjust_No] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.StockBatchDetails') IS NULL
CREATE TABLE [AMS].[StockBatchDetails] (
[Module] [VARCHAR](10) NOT NULL
,[Voucher_No] [VARCHAR](50) NOT NULL
,[SNo] [INT] NOT NULL
,[Product_Id] [BIGINT] NOT NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [VARCHAR](10) NOT NULL
,[Batch_SNo] [NUMERIC](18, 0) NOT NULL
,[Batch] [VARCHAR](25) NOT NULL
,[AltQty] [DECIMAL](18, 6) NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[AltStockQty] [DECIMAL](18, 6) NULL
,[StockQty] [DECIMAL](18, 6) NULL
,[FreeQty] [DECIMAL](18, 6) NULL
,[FreeStockQty] [DECIMAL](18, 6) NULL
,[FreeUnit_Id] [INT] NULL
,[ConvRatio] [DECIMAL](18, 6) NULL
,[Margin] [DECIMAL](18, 6) NULL
,[StockVal] [DECIMAL](18, 6) NULL
,[ExtraFreeQty] [DECIMAL](18, 6) NULL
,[StockExtraFreeQty] [DECIMAL](18, 6) NULL
,[ExtraFreeUnit_Id] [INT] NULL
,[Buy_Rate] [DECIMAL](18, 6) NULL
,[Sales_Rate] [DECIMAL](18, 6) NULL
,[Trade_Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[MRP] [DECIMAL](18, 6) NULL
,[MRP_Rate] [DECIMAL](18, 6) NULL
,[Min_Qty] [DECIMAL](18, 6) NULL
,[Max_Qty] [DECIMAL](18, 6) NULL
,[Min_Disc] [DECIMAL](18, 6) NULL
,[Max_Disc] [DECIMAL](18, 6) NULL
,[Manufacturing_Date] [DATETIME] NULL
,[Expiry_Date] [DATETIME] NULL
,[Transaction_Type] [char](1) NULL
,[CBranch_Id] [INT] NOT NULL
,[CUnit_Id] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,CONSTRAINT [PK_StockBatchDetails] PRIMARY KEY CLUSTERED ([Module] ASC, [Voucher_No] ASC, [SNo] ASC, [Product_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.StockDetails') IS NULL
CREATE TABLE [AMS].[StockDetails] (
[Module] [NVARCHAR](10) NOT NULL
,[Voucher_No] [NVARCHAR](50) NOT NULL
,[Serial_No] [INT] NOT NULL
,[PurRefVno] [NVARCHAR](50) NULL
,[Voucher_Date] [DATETIME] NOT NULL
,[Voucher_Miti] [NVARCHAR](10) NOT NULL
,[Voucher_Time] [DATETIME] NOT NULL
,[Ledger_ID] [BIGINT] NULL
,[Subledger_Id] [INT] NULL
,[Agent_ID] [INT] NULL
,[Department_ID1] [INT] NULL
,[Department_ID2] [INT] NULL
,[Department_ID3] [INT] NULL
,[Department_ID4] [INT] NULL
,[Currency_ID] [INT] NOT NULL
,[Currency_Rate] [DECIMAL](18, 6) NOT NULL
,[Product_Id] [BIGINT] NOT NULL
,[Godown_Id] [INT] NULL
,[CostCenter_Id] [INT] NULL
,[AltQty] [DECIMAL](18, 6) NULL
,[AltUnit_Id] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[Unit_Id] [INT] NULL
,[AltStockQty] [DECIMAL](18, 6) NULL
,[StockQty] [DECIMAL](18, 6) NULL
,[FreeQty] [DECIMAL](18, 6) NULL
,[FreeUnit_Id] [INT] NULL
,[StockFreeQty] [DECIMAL](18, 6) NULL
,[ConvRatio] [DECIMAL](18, 6) NULL
,[ExtraFreeQty] [DECIMAL](18, 6) NULL
,[ExtraFreeUnit_Id] [INT] NULL
,[ExtraStockFreeQty] [DECIMAL](18, 6) NULL
,[Rate] [DECIMAL](18, 6) NULL
,[BasicAmt] [DECIMAL](18, 6) NULL
,[TermAmt] [DECIMAL](18, 6) NULL
,[NetAmt] [DECIMAL](18, 6) NULL
,[BillTermAmt] [DECIMAL](18, 6) NULL
,[TaxRate] [DECIMAL](18, 6) NULL
,[TaxableAmt] [DECIMAL](18, 6) NULL
,[DocVal] [DECIMAL](18, 6) NULL
,[ReturnVal] [DECIMAL](18, 6) NULL
,[StockVal] [DECIMAL](18, 6) NULL
,[AddStockVal] [DECIMAL](18, 6) NULL
,[PartyInv] [NVARCHAR](50) NULL
,[EntryType] [char](1) NOT NULL
,[AuthBy] [NVARCHAR](50) NULL
,[AuthDate] [DATETIME] NULL
,[RecoBy] [NVARCHAR](50) NULL
,[RecoDate] [DATETIME] NULL
,[Counter_ID] [INT] NULL
,[RoomNo] [INT] NULL
,[Branch_ID] [INT] NOT NULL
,[CmpUnit_ID] [INT] NULL
,[Adj_Qty] [DECIMAL](18, 6) NULL
,[Adj_VoucherNo] [NVARCHAR](50) NULL
,[Adj_Module] [char](10) NULL
,[SalesRate] [DECIMAL](18, 6) NULL
,[FiscalYearId] [INT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.Subledger') IS NULL
CREATE TABLE [AMS].[Subledger] (
[SLId] [INT] NOT NULL
,[SLName] [NVARCHAR](200) NOT NULL
,[SLCode] [NVARCHAR](50) NOT NULL
,[SLAddress] [NVARCHAR](500) NULL
,[SLPhoneNo] [NVARCHAR](50) NULL
,[GLID] [BIGINT] NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Subledger] PRIMARY KEY CLUSTERED ([SLId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_SubledgerDesc] UNIQUE NONCLUSTERED ([SLName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_SubledgerShortName] UNIQUE NONCLUSTERED ([SLCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SyncTable') IS NULL
CREATE TABLE [AMS].[SyncTable] (
[SyncId] [INT] NOT NULL
,[IsBranch] [BIT] NULL
,[IsGeneralLedger] [BIT] NULL
,[IsTableId] [BIT] NULL
,[IsArea] [BIT] NULL
,[IsBillingTerm] [BIT] NULL
,[IsAgent] [BIT] NULL
,[IsProduct] [BIT] NULL
,[IsCostCenter] [BIT] NULL
,[IsMember] [BIT] NULL
,[IsCashBank] [BIT] NULL
,[IsJournalVoucher] [BIT] NULL
,[IsNotesRegister] [BIT] NULL
,[IsPDCVoucher] [BIT] NULL
,[IsLedgerOpening] [BIT] NULL
,[IsProductOpening] [BIT] NULL
,[IsSalesQuotation] [BIT] NULL
,[IsSalesOrder] [BIT] NULL
,[IsSalesChallan] [BIT] NULL
,[IsSalesInvoice] [BIT] NULL
,[IsSalesReturn] [BIT] NULL
,[IsSalesAdditional] [BIT] NULL
,[IsPurchaseIndent] [BIT] NULL
,[IsPurchaseOrder] [BIT] NULL
,[IsPurchaseChallan] [BIT] NULL
,[IsPurchaseInvoice] [BIT] NULL
,[IsPurchaseReturn] [BIT] NULL
,[IsPurchaseAdditional] [BIT] NULL
,[IsStockAdjustment] [BIT] NULL
,[SyncAPI] [NVARCHAR](MAX) NULL
,[SyncOrginId] [NVARCHAR](MAX) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_SyncTable] PRIMARY KEY CLUSTERED ([SyncId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SystemConfiguration') IS NULL
CREATE TABLE [AMS].[SystemConfiguration] (
[SC_Id] [TINYINT] NOT NULL
,[Date_Type] [char](1) NULL
,[Audit_Trial] [BIT] NULL
,[Udf] [BIT] NULL
,[AutoPupup] [BIT] NULL
,[CurrentDate] [BIT] NULL
,[ConfirmSave] [BIT] NULL
,[ConfirmCancel] [BIT] NULL
,[Cur_Id] [INT] NULL
,[FY_Id] [INT] NULL
,[DefaultPrinter] [NVARCHAR](100) NULL
,[BackupSch_IntvDays] [INT] NULL
,[Backup_Path] [VARCHAR](255) NULL
,[PL_AC] [BIGINT] NULL
,[Cash_AC] [BIGINT] NULL
,[Vat_AC] [BIGINT] NULL
,[PDCBank_AC] [BIGINT] NULL
,[Transby_Code] [BIT] NULL
,[Negative_Tran] [BIT] NULL
,[Amount_Format] [VARCHAR](15) NULL
,[Rate_Format] [VARCHAR](15) NULL
,[Qty_Format] [VARCHAR](15) NULL
,[AltQty_Format] [VARCHAR](15) NULL
,[Cur_Format] [VARCHAR](15) NULL
,[Font_Name] [VARCHAR](555) NULL
,[Font_Size] [DECIMAL](18, 0) NULL
,[Paper_Size] [VARCHAR](15) NULL
,[ReportFont_Style] [NVARCHAR](50) NULL
,[Printing_DateTime] [BIT] NULL
,[Purchase_AC] [BIGINT] NULL
,[PurchaseReturn_AC] [BIGINT] NULL
,[PurchaseVat_Id] [INT] NULL
,[PurchaseAddVat_Id] [INT] NULL
,[PurchaseProDiscount_Id] [INT] NULL
,[PurchaseDiscount_ID] [INT] NULL
,[PCredit_Balance_War] [INT] NULL
,[PCredit_Days_War] [INT] NULL
,[PCarry_Rate] [BIT] NULL
,[PLast_Rate] [BIT] NULL
,[PBatch_Rate] [BIT] NULL
,[PQuality_Control] [BIT] NULL
,[PPGrpWiseBilling] [BIT] NULL
,[PAdvancePayment] [BIT] NULL
,[Sales_AC] [BIGINT] NULL
,[SalesReturn_AC] [BIGINT] NULL
,[SalesVat_Id] [INT] NULL
,[SalesDiscount_Id] [INT] NULL
,[SalesSpecialDiscount_Id] [INT] NULL
,[SalesServiceCharge_Id] [INT] NULL
,[SCreditBalance_War] [INT] NULL
,[SCreditDays_War] [INT] NULL
,[SChange_Rate] [BIT] NULL
,[SLast_Rate] [BIT] NULL
,[SSalesCarry_Rate] [BIT] NULL
,[SDispatch_Order] [BIT] NULL
,[SCancellationCustomer_Code] [INT] NULL
,[SCancellationProduct_Id] [INT] NULL
,[DefaultInvoiceDesign] [VARCHAR](250) NULL
,[DefaultABTInvoiceDesign] [VARCHAR](250) NULL
,[DefaultInvoicePrinter] [VARCHAR](100) NULL
,[DefaultInvoiceDocNumbering] [VARCHAR](100) NULL
,[DefaultPreInvoiceDesign] [VARCHAR](250) NULL
,[DefaultOrderDesign] [VARCHAR](250) NULL
,[DefaultOrderPrinter] [VARCHAR](100) NULL
,[DefaultOrderDocNumbering] [VARCHAR](100) NULL
,[Stock_ValueInSales_Return] [BIT] NULL
,[Available_Stock] [BIT] NULL
,[Customer_Name] [BIT] NULL
,[SPGrpWiseBilling] [BIT] NULL
,[TenderAmount] [BIT] NULL
,[AdvanceReceipt] [BIT] NULL
,[OpeningStockPL_AC] [BIGINT] NULL
,[ClosingStockPL_AC] [BIGINT] NULL
,[ClosingStockBS_Ac] [BIGINT] NULL
,[Negative_Stock_War] [INT] NULL
,[Godown_Category] [BIT] NULL
,[Product_Code] [BIT] NULL
,[AltQty_Alteration] [BIT] NULL
,[Alteration_Part] [BIT] NULL
,[CarryBatch_Qty] [BIT] NULL
,[Breakup_Qty] [BIT] NULL
,[Mfg_Date] [BIT] NULL
,[Exp_Date] [BIT] NULL
,[Mfg_Date_Validation] [BIT] NULL
,[Exp_Date_Validation] [BIT] NULL
,[Free_Qty] [BIT] NULL
,[Extra_Free_Qty] [BIT] NULL
,[Godown_Wise_Filter] [BIT] NULL
,[Finished_Qty] [BIT] NULL
,[Equal_Qty] [BIT] NULL
,[IGodown_Wise_Filter] [BIT] NULL
,[Debtor_Id] [INT] NULL
,[Creditor_Id] [INT] NULL
,[Salary_Id] [BIGINT] NULL
,[TDS_Id] [BIGINT] NULL
,[PF_Id] [BIGINT] NULL
,[Email_Id] [NVARCHAR](100) NULL
,[Email_Password] [NVARCHAR](50) NULL
,[BeforeBuyRate] [BIT] NULL
,[BeforeSalesRate] [BIT] NULL
,[BarcodePrinter] [VARCHAR](250) NULL
,[DefaultBarcodeDesign] [VARCHAR](250) NULL
,CONSTRAINT [PK_SystemConfiguration] PRIMARY KEY CLUSTERED ([SC_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SystemControlOptions') IS NULL
CREATE TABLE [AMS].[SystemControlOptions] (
[SCOptions_Id] [INT] IDENTITY (1, 1) NOT NULL
,[Header] [VARCHAR](50) NULL
,[Options_Name] [VARCHAR](50) NULL
,[Enable] [BIT] NULL
,[Mandatory] [BIT] NULL
,CONSTRAINT [PK_SystemControlOptions] PRIMARY KEY CLUSTERED ([SCOptions_Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.SystemSetting') IS NULL
CREATE TABLE [AMS].[SystemSetting] (
[SyId] [TINYINT] NOT NULL
,[EnglishDate] [BIT] NULL
,[AuditTrial] [BIT] NULL
,[Udf] [BIT] NULL
,[Autopoplist] [BIT] NULL
,[CurrentDate] [BIT] NULL
,[ConformSave] [BIT] NULL
,[ConformCancel] [BIT] NULL
,[ConformExits] [BIT] NULL
,[CurrencyRate] [FLOAT] NULL
,[CurrencyId] [INT] NULL
,[DefaultPrinter] [NVARCHAR](100) NULL
,[AmountFormat] [FLOAT] NULL
,[RateFormat] [FLOAT] NULL
,[QtyFormat] [FLOAT] NULL
,[CurrencyFormatF] [FLOAT] NULL
,[DefaultFiscalYearId] [INT] NULL
,[DefaultOrderPrinter] [NVARCHAR](100) NULL
,[DefaultInvoicePrinter] [NVARCHAR](100) NULL
,[DefaultOrderNumbering] [NVARCHAR](100) NULL
,[DefaultInvoiceNumbering] [NVARCHAR](100) NULL
,[DefaultAvtInvoiceNumbering] [NVARCHAR](100) NULL
,[DefaultOrderDesign] [NVARCHAR](100) NULL
,[IsOrderPrint] [BIT] NULL
,[DefaultInvoiceDesign] [NVARCHAR](100) NULL
,[IsInvoicePrint] [BIT] NULL
,[DefaultAvtDesign] [NVARCHAR](100) NULL
,[DefaultFontsName] [NVARCHAR](100) NULL
,[DefaultFontsSize] [INT] NULL
,[DefaultPaperSize] [NVARCHAR](100) NULL
,[DefaultReportStyle] [NVARCHAR](100) NULL
,[DefaultPrintDateTime] [BIT] NULL
,[DefaultFormColor] [NVARCHAR](100) NULL
,[DefaultTextColor] [NVARCHAR](100) NULL
,[DebtorsGroupId] [INT] NULL
,[CreditorGroupId] [INT] NULL
,[SalaryLedgerId] [BIGINT] NULL
,[TDSLedgerId] [BIGINT] NULL
,[PFLedgerId] [BIGINT] NULL
,[DefaultEmail] [NVARCHAR](300) NULL
,[DefaultEmailPassword] [NVARCHAR](500) NULL
,[BackupDays] [INT] NULL
,[BackupLocation] [NVARCHAR](500) NULL
,CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED ([SyId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.TableMaster') IS NULL
CREATE TABLE [AMS].[TableMaster] (
[TableId] [INT] NOT NULL
,[TableName] [NVARCHAR](50) NOT NULL
,[TableCode] [NVARCHAR](50) NOT NULL
,[FloorId] [INT] NOT NULL
,[Branch_ID] [INT] NOT NULL
,[Company_Id] [INT] NULL
,[TableStatus] [char](1) NOT NULL
,[Status] [BIT] NOT NULL
,[EnterBy] [VARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[TableType] [VARCHAR](50) NULL
,[Printed] [INT] NULL
,CONSTRAINT [PK_TableMaster] PRIMARY KEY CLUSTERED ([TableId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_TableMasterDesc] UNIQUE NONCLUSTERED ([TableName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_TableMasterShortName] UNIQUE NONCLUSTERED ([TableCode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.TicketRefund') IS NULL
CREATE TABLE [AMS].[TicketRefund] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NOT NULL
,[VoucherMiti] [NVARCHAR](10) NOT NULL
,[RefVno] [NVARCHAR](50) NULL
,[RefDate] [DATETIME] NULL
,[AgencyId] [BIGINT] NOT NULL
,[AirlinesId] [INT] NOT NULL
,[LedgerId] [BIGINT] NOT NULL
,[Sales] [DECIMAL](18, 8) NULL
,[Purchase] [DECIMAL](18, 8) NULL
,[CancellationCharge] [DECIMAL](18, 8) NULL
,[FineCharge] [DECIMAL](18, 8) NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,CONSTRAINT [PK_TicketRefund] PRIMARY KEY CLUSTERED ([VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.Udf') IS NULL
CREATE TABLE [AMS].[Udf] (
[UDF_Code] [VARCHAR](5) NOT NULL
,[Entry_Module] [VARCHAR](10) NOT NULL
,[Field_Name] [VARCHAR](50) NOT NULL
,[Field_Type] [VARCHAR](15) NOT NULL
,[Total_Width] [VARCHAR](256) NOT NULL
,[Field_Decimal] [VARCHAR](1) NULL
,[Date_Format] [VARCHAR](15) NULL
,[List_field_Name] [VARCHAR](1) NULL
,PRIMARY KEY CLUSTERED ([UDF_Code] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.UDF_Entry') IS NULL
CREATE TABLE [AMS].[UDF_Entry] (
[UDF_Code] [VARCHAR](5) NOT NULL
,[Entry_Module] [VARCHAR](256) NOT NULL
,[Field_Name] [VARCHAR](50) NOT NULL
,[Field_Type] [VARCHAR](15) NOT NULL
,[Total_Width] [VARCHAR](256) NOT NULL
,[Mandotary_opt] [char](1) NOT NULL
,[Udf_Schedule] [INT] NULL
,[Duplicate_opt] [char](1) NULL
,[Date_Format] [VARCHAR](15) NULL
,[Field_Decimal] [VARCHAR](1) NULL
,PRIMARY KEY CLUSTERED ([UDF_Code] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.UnitWiseLedger') IS NULL
CREATE TABLE [AMS].[UnitWiseLedger] (
[UnitId] [INT] NULL
,[BranchId] [INT] NULL
,[LedgerId] [BIGINT] NULL
,[Mapped] [BIT] NULL
,[Category] [NVARCHAR](50) NULL
) ON [PRIMARY];
IF OBJECT_ID('AMS.VehicleColors') IS NULL
CREATE TABLE [AMS].[VehicleColors] (
[VHColorsId] [INT] IDENTITY (1, 1) NOT NULL
,[VHColorsDesc] [NVARCHAR](200) NOT NULL
,[VHColorsShortName] [NVARCHAR](50) NOT NULL
,[VHColorsBranchId] [INT] NOT NULL
,[VHColorsCompanyUnitId] [INT] NULL
,[VHColorsActive] [BIT] NOT NULL
,[VHColorsEntryBy] [NVARCHAR](50) NOT NULL
,[VHColorsEnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_VechileColors] PRIMARY KEY CLUSTERED ([VHColorsId] ASC, [VHColorsDesc] ASC, [VHColorsShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_VehicleColors] UNIQUE NONCLUSTERED ([VHColorsDesc] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_VehicleColors_1] UNIQUE NONCLUSTERED ([VHColorsShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.VehicleNumber') IS NULL
CREATE TABLE [AMS].[VehicleNumber] (
[VHNoId] [INT] IDENTITY (1, 1) NOT NULL
,[VNDesc] [NVARCHAR](200) NOT NULL
,[VNShortName] [NVARCHAR](50) NOT NULL
,[VNState] [NVARCHAR](50) NOT NULL
,[VNStatus] [BIT] NOT NULL
,[VNBranchId] [INT] NOT NULL
,[VNCompanyUnitId] [INT] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_VehicleNumber] PRIMARY KEY CLUSTERED ([VHNoId] ASC, [VNDesc] ASC, [VNShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_VehicleNumber] UNIQUE NONCLUSTERED ([VNDesc] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_VehicleNumber_1] UNIQUE NONCLUSTERED ([VNShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('AMS.VehileModel') IS NULL
CREATE TABLE [AMS].[VehileModel] (
[VHModelId] [INT] IDENTITY (1, 1) NOT NULL
,[VHModelDesc] [NVARCHAR](200) NOT NULL
,[VHModelShortName] [NVARCHAR](50) NOT NULL
,[VHModelBranchId] [INT] NOT NULL
,[VHModelCompanyUnitId] [INT] NULL
,[VHModelActive] [BIT] NOT NULL
,[VHModelEntryBy] [NVARCHAR](50) NOT NULL
,[VHModelEnterDate] [DATETIME] NOT NULL
,CONSTRAINT [PK_VehileModel_1] PRIMARY KEY CLUSTERED ([VHModelId] ASC, [VHModelDesc] ASC, [VHModelShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_VehileModel] UNIQUE NONCLUSTERED ([VHModelDesc] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_VehileModel_1] UNIQUE NONCLUSTERED ([VHModelShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

IF OBJECT_ID('HOS.BedMaster') IS NULL
CREATE TABLE [HOS].[BedMaster] (
[BID] [INT] NOT NULL
,[BedDesc] [NVARCHAR](80) NULL
,[BedShortName] [NVARCHAR](50) NULL
,[Bedtype] [INT] NULL
,[WId] [INT] NULL
,[ChargeAmt] [DECIMAL](18, 6) NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_BedMaster] PRIMARY KEY CLUSTERED ([BID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [BedMasterDesc] UNIQUE NONCLUSTERED ([BedDesc] ASC, [BedShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.Bedtype') IS NULL
CREATE TABLE [HOS].[Bedtype] (
[BID] [INT] NOT NULL
,[BDesc] [NVARCHAR](80) NULL
,[BShortName] [NVARCHAR](50) NULL
,[BranchId] [INT] NULL
,[Company_Unit] [INT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[Status] [BIT] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_BedType] PRIMARY KEY CLUSTERED ([BID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [BdType] UNIQUE NONCLUSTERED ([BDesc] ASC, [BShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.Department') IS NULL
CREATE TABLE [HOS].[Department] (
[DId] [INT] NOT NULL
,[DName] [NVARCHAR](80) NULL
,[DCode] [NVARCHAR](50) NULL
,[Dlevel] [char](10) NULL
,[DoctorId] [INT] NULL
,[ItemId] [BIGINT] NULL
,[ChargeAmt] [DECIMAL](18, 6) NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_Department_1] PRIMARY KEY CLUSTERED ([DId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.Doctor') IS NULL
CREATE TABLE [HOS].[Doctor] (
[DrId] [INT] NOT NULL
,[DrName] [NVARCHAR](200) NULL
,[DrShortName] [NVARCHAR](50) NULL
,[DrType] [INT] NULL
,[ContactNo] [NUMERIC](18, 0) NULL
,[Address] [NVARCHAR](150) NULL
,[BranchId] [INT] NULL
,[CompanyUnit] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED ([DrId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [DoctorDesc] UNIQUE NONCLUSTERED ([DrName] ASC, [DrShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.DoctorCommission') IS NULL
CREATE TABLE [HOS].[DoctorCommission] (
[SNo] [INT] NULL
,[DrId] [INT] NULL
,[ProCode] [BIGINT] NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[CommPer] [DECIMAL](18, 6) NULL
,[CommAmount] [DECIMAL](18, 6) NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[FiscalYearId] [INT] NULL
) ON [PRIMARY];
IF OBJECT_ID('HOS.DoctorCommissionItem') IS NULL
CREATE TABLE [HOS].[DoctorCommissionItem] (
[InvoiceNo] [NVARCHAR](50) NULL
,[SNo] [INT] NULL
,[DrId] [INT] NULL
,[ProId] [BIGINT] NULL
,[CustomerDesc] [INT] NULL
,[CommPer] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Dis] [DECIMAL](18, 6) NULL
,[ProSno] [INT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
) ON [PRIMARY];
IF OBJECT_ID('HOS.DoctorType') IS NULL
CREATE TABLE [HOS].[DoctorType] (
[DtID] [INT] NOT NULL
,[DrTypeDesc] [NVARCHAR](80) NULL
,[DrTypeShortName] [NVARCHAR](50) NULL
,[BranchId] [INT] NULL
,[Company_Unit] [INT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,[Status] [BIT] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
,CONSTRAINT [PK_DoctorType] PRIMARY KEY CLUSTERED ([DtID] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [DrType] UNIQUE NONCLUSTERED ([DrTypeDesc] ASC, [DrTypeShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.PatientBillingDetails') IS NULL
CREATE TABLE [HOS].[PatientBillingDetails] (
[VoucherNo] [NVARCHAR](50) NULL
,[SNo] [INT] NULL
,[ProId] [BIGINT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[Rate] [DECIMAL](18, 6) NULL
,[BasicAmt] [DECIMAL](18, 6) NULL
,[TermAmt] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[RouDoctorId] [INT] NULL
) ON [PRIMARY];
IF OBJECT_ID('HOS.PatientBillingMaster') IS NULL
CREATE TABLE [HOS].[PatientBillingMaster] (
[Module] [char](10) NULL
,[VoucherNo] [NVARCHAR](50) NOT NULL
,[VoucherDate] [DATETIME] NULL
,[VoucherMiti] [NVARCHAR](50) NULL
,[PatientId] [BIGINT] NULL
,[LedgerId] [BIGINT] NULL
,[DoctorId] [INT] NULL
,[RefDoctorId] [INT] NULL
,[DepartmentId] [INT] NULL
,[BusinessId] [INT] NULL
,[MemberId] [INT] NULL
,[B_Amount] [DECIMAL](18, 6) NULL
,[T_Amount] [DECIMAL](18, 6) NULL
,[T_Rate] [DECIMAL](18, 6) NULL
,[N_Amount] [DECIMAL](18, 6) NULL
,[TenderAmount] [DECIMAL](18, 6) NULL
,[ChangeAMount] [DECIMAL](18, 6) NULL
,[Remarks] [NVARCHAR](1024) NULL
,[ActionType] [char](10) NULL
,[Is_Printed] [BIT] NULL
,[NoOfPrint] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,CONSTRAINT [PK__Commissi__11F284192EC5E7B8] PRIMARY KEY CLUSTERED ([VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.PatientBillingTerm') IS NULL
CREATE TABLE [HOS].[PatientBillingTerm] (
[SB_VNo] [NVARCHAR](50) NOT NULL
,[ST_Id] [INT] NOT NULL
,[SNo] [INT] NULL
,[Rate] [DECIMAL](18, 6) NULL
,[Amount] [DECIMAL](18, 6) NULL
,[Term_Type] [char](2) NULL
,[Product_Id] [BIGINT] NULL
,[Taxable] [char](1) NULL
) ON [PRIMARY];
IF OBJECT_ID('HOS.PatientMaster') IS NULL
CREATE TABLE [HOS].[PatientMaster] (
[PaitentId] [BIGINT] NOT NULL
,[RefDate] [DATETIME] NULL
,[IPDId] [BIGINT] NULL
,[Title] [char](10) NULL
,[PaitentDesc] [NVARCHAR](100) NULL
,[ShortName] [NVARCHAR](50) NULL
,[TAddress] [NVARCHAR](500) NULL
,[PAddress] [NVARCHAR](500) NULL
,[AccountLedger] [NVARCHAR](150) NULL
,[ContactNo] [NVARCHAR](50) NULL
,[Age] [DECIMAL](18, 2) NULL
,[AgeType] [NVARCHAR](50) NULL
,[DateofBirth] [DATETIME] NULL
,[Gender] [NVARCHAR](50) NULL
,[MaritalStatus] [NVARCHAR](50) NULL
,[RegType] [NVARCHAR](50) NULL
,[Nationality] [NVARCHAR](50) NULL
,[Religion] [VARCHAR](30) NULL
,[BloodGrp] [NVARCHAR](50) NULL
,[DepartmentId] [INT] NULL
,[DrId] [INT] NULL
,[RefDrDesc] [NVARCHAR](50) NULL
,[EmailAdd] [NVARCHAR](150) NULL
,[PastHistory] [NVARCHAR](500) NULL
,[ContactPer] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[Status] [BIT] NULL
,[BranchId] [INT] NULL
,[CompanyUnit] [INT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_PatientMaster] PRIMARY KEY CLUSTERED ([PaitentId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [PatientMasterDesc] UNIQUE NONCLUSTERED ([AccountLedger] ASC, [ShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('HOS.WardMaster') IS NULL
CREATE TABLE [HOS].[WardMaster] (
[WId] [INT] NOT NULL
,[WDesc] [NVARCHAR](80) NULL
,[WShortName] [NVARCHAR](50) NULL
,[DepartmentId] [INT] NULL
,[ChargeAmt] [DECIMAL](18, 6) NULL
,[Branch_ID] [INT] NULL
,[Company_Id] [INT] NULL
,[Status] [BIT] NULL
,[EnterBy] [NVARCHAR](50) NULL
,[EnterDate] [DATETIME] NULL
,CONSTRAINT [PK_WardMaster] PRIMARY KEY CLUSTERED ([WId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [WardMasterDesc] UNIQUE NONCLUSTERED ([WDesc] ASC, [WShortName] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

IF OBJECT_ID('HR.Employee') IS NULL
CREATE TABLE [HR].[Employee] (
[EmployeeId] [INT] NOT NULL
,[Title] [NVARCHAR](50) NULL
,[FirstName] [NVARCHAR](50) NULL
,[MiddleName] [NVARCHAR](50) NULL
,[LastName] [NVARCHAR](50) NULL
,[InNepali] [NVARCHAR](250) NULL
,[Gender] [char](2) NULL
,[MaritalStatus] [char](2) NULL
,[Religion] [char](2) NULL
,[BloodGroup] [char](5) NULL
,[Nationality] [NVARCHAR](50) NULL
,[PhoneNo] [NVARCHAR](50) NULL
,[MobileNo] [NVARCHAR](50) NULL
,[Email] [NVARCHAR](100) NULL
,[Address] [NVARCHAR](250) NULL
,[Qualification] [NVARCHAR](50) NULL
,[PassoutYear] [INT] NULL
,[Experiance] [NVARCHAR](50) NULL
,[AttachedDocuments] [IMAGE] NULL
,CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

IF OBJECT_ID('INV.BOM_Details') IS NULL
CREATE TABLE [INV].[BOM_Details] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[SerialNo] [INT] NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[GodownId] [INT] NULL
,[CostCenterId] [INT] NOT NULL
,[OrderNo] [NVARCHAR](50) NULL
,[OrderSNo] [INT] NULL
,[AltQty] [DECIMAL](18, 6) NOT NULL
,[AltUnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[UnitId] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](500) NULL
,CONSTRAINT [PK_BOM_Details] PRIMARY KEY CLUSTERED ([VoucherNo] ASC, [SerialNo] ASC, [ProductId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('INV.BOM_Master') IS NULL
CREATE TABLE [INV].[BOM_Master] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[VDate] [Date] NOT NULL
,[VMiti] [NVARCHAR](10) NOT NULL
,[Vtime] [DATETIME] NOT NULL
,[FinishedGoodsId] [BIGINT] NOT NULL
,[FinishedGoodsQty] [DECIMAL](18, 6) NOT NULL
,[DepartmentId] [INT] NULL
,[CostCenterId] [INT] NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[InWords] [NVARCHAR](500) NULL
,[Remarks] [NVARCHAR](500) NULL
,[IsAuthorized] [BIT] NOT NULL
,[AuthorizeBy] [NVARCHAR](50) NULL
,[AuthDate] [DATETIME] NULL
,[IsReconcile] [BIT] NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[IsPosted] [BIT] NOT NULL
,[PostedBy] [NVARCHAR](50) NULL
,[PostedDate] [DATETIME] NULL
,[OrderNo] [VARCHAR](500) NULL
,[OrderDate] [Date] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,CONSTRAINT [PK_BOM_Master] PRIMARY KEY CLUSTERED ([VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('INV.Production_Details') IS NULL
CREATE TABLE [INV].[Production_Details] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[SerialNo] [INT] NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[GodownId] [INT] NULL
,[CostCenterId] [INT] NOT NULL
,[BOMNo] [NVARCHAR](50) NULL
,[BOMSNo] [INT] NULL
,[BOMQty] [DECIMAL](18, 6) NOT NULL
,[IssueNo] [NVARCHAR](50) NULL
,[IssueSNo] [INT] NULL
,[OrderNo] [NVARCHAR](50) NULL
,[OrderSNo] [INT] NULL
,[AltQty] [DECIMAL](18, 6) NOT NULL
,[AltUnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NOT NULL
,[UnitId] [INT] NOT NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[Narration] [NVARCHAR](500) NULL
,CONSTRAINT [PK_Production_Details] PRIMARY KEY CLUSTERED ([VoucherNo] ASC, [SerialNo] ASC, [ProductId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
IF OBJECT_ID('INV.Production_Master') IS NULL
CREATE TABLE [INV].[Production_Master] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[BOMVNo] [NVARCHAR](50) NULL
,[BOMDate] [Date] NULL
,[VDate] [Date] NOT NULL
,[VMiti] [NVARCHAR](10) NOT NULL
,[Vtime] [DATETIME] NOT NULL
,[FinishedGoodsId] [BIGINT] NOT NULL
,[FinishedGoodsQty] [DECIMAL](18, 6) NOT NULL
,[Costing] [DECIMAL](18, 6) NOT NULL
,[Machine] [NVARCHAR](50) NOT NULL
,[DepartmentId] [INT] NULL
,[CostCenterId] [INT] NULL
,[Amount] [DECIMAL](18, 6) NOT NULL
,[InWords] [NVARCHAR](500) NULL
,[Remarks] [NVARCHAR](500) NULL
,[IsAuthorized] [BIT] NOT NULL
,[AuthorizeBy] [NVARCHAR](50) NULL
,[AuthDate] [DATETIME] NULL
,[IsCancel] [BIT] NOT NULL
,[IsReturn] [BIT] NOT NULL
,[IsReconcile] [BIT] NOT NULL
,[ReconcileBy] [NVARCHAR](50) NULL
,[ReconcileDate] [DATETIME] NULL
,[IsPosted] [BIT] NOT NULL
,[PostedBy] [NVARCHAR](50) NULL
,[PostedDate] [DATETIME] NULL
,[IssueNo] [NVARCHAR](50) NULL
,[IssueDate] [Date] NULL
,[OrderNo] [VARCHAR](500) NULL
,[OrderDate] [Date] NULL
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,[BranchId] [INT] NOT NULL
,[CompanyUnitId] [INT] NULL
,[FiscalYearId] [INT] NOT NULL
,[Source] [char](10) NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,CONSTRAINT [PK_Production_Master] PRIMARY KEY CLUSTERED ([VoucherNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

IF OBJECT_ID('[AMS].[BarcodeList]') IS NULL
BEGIN
CREATE TABLE [AMS].[BarcodeList] (
[ProductId] [BIGINT] NOT NULL
,[Barcode] [NVARCHAR](50) NOT NULL
,[SalesRate] [DECIMAL](18, 6) NULL
,CONSTRAINT [PK_BarcodeList] PRIMARY KEY CLUSTERED ([ProductId] ASC, [Barcode] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [AMS].[BarcodeList] WITH CHECK ADD CONSTRAINT [FK_BarcodeList_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[BarcodeList] CHECK CONSTRAINT [FK_BarcodeList_ProductId]
END;
IF OBJECT_ID('CTRL.ExcTableErrorLog') IS NULL
BEGIN
CREATE TABLE CTRL.ExcTableErrorLog (
ErrorID INT IDENTITY (1, 1)
,UserName VARCHAR(100)
,ErrorNumber INT
,ErrorState INT
,ErrorSeverity INT
,ErrorLine INT
,ErrorProcedure VARCHAR(MAX)
,ErrorMessage VARCHAR(MAX)
,ErrorDateTime DATETIME
)
END;
IF OBJECT_ID('CTRL.ImportLog') IS NULL
BEGIN
CREATE TABLE [CTRL].[ImportLog] (
[LogId] [INT] IDENTITY (1, 1) NOT NULL
,[ImportType] [NVARCHAR](50) NULL
,[ImportDate] [DATETIME] NULL
,[ServerDesc] [NVARCHAR](MAX) NOT NULL
,[ServerUser] [NVARCHAR](MAX) NULL
,[ServerPassword] [NVARCHAR](MAX) NULL
,[dbInitial] [NVARCHAR](MAX) NULL
,[dbCompanyInfo] [NVARCHAR](MAX) NULL
,[IsSuccess] [BIT] NOT NULL
,CONSTRAINT [PK_ImportLog] PRIMARY KEY CLUSTERED ([LogId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END;
IF OBJECT_ID('AMS.NR_Master') IS NULL
BEGIN
CREATE TABLE [AMS].[NR_Master] (
[NRID] [INT] NOT NULL IDENTITY (1, 1)
,[NRDESC] [NVARCHAR](1024)
,[NRTYPE] [char](2)
,[IsActive] [BIT]
,[EnterBy] [NVARCHAR](50) NOT NULL
,[EnterDate] [DATETIME] NOT NULL
,PRIMARY KEY (NRID)
)
END;

IF OBJECT_ID('AMS.temp_SB_Master') IS NULL
BEGIN
CREATE TABLE [AMS].[temp_SB_Master] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_Date] [DATETIME] NOT NULL
,[Invoice_Miti] [NVARCHAR](50) NOT NULL
,[Invoice_Time] [DATETIME] NOT NULL
,[PB_Vno] [NVARCHAR](50) NULL
,[Vno_Date] [DATETIME] NULL
,[Vno_Miti] [VARCHAR](10) NULL
,[Customer_ID] [BIGINT] NOT NULL
,[Party_Name] [NVARCHAR](100) NULL
,[Vat_No] [NVARCHAR](50) NULL
,[Contact_Person] [NVARCHAR](50) NULL
,[Mobile_No] [NVARCHAR](50) NULL
,[Address] [NVARCHAR](90) NULL
,[ChqNo] [NVARCHAR](50) NULL
,[ChqDate] [DATETIME] NULL
,[Invoice_Type] [NVARCHAR](50) NOT NULL
,[Invoice_Mode] [NVARCHAR](50) NOT NULL
,[Payment_Mode] [VARCHAR](50) NOT NULL
,[DueDays] [INT] NULL
,[DueDate] [DATETIME] NULL
,[Agent_ID] [INT] NULL
,[Subledger_Id] [INT] NULL
,[SO_Invoice] [NVARCHAR](250) NULL
,[SO_Date] [DATETIME] NULL
,[SC_Invoice] [NVARCHAR](250) NULL
,[SC_Date] [DATETIME] NULL
,[Cls1] [INT] NULL
,[Cls2] [INT] NULL
,[Cls3] [INT] NULL
,[Cls4] [INT] NULL
,[CounterId] [INT] NULL
,[Cur_Id] [INT] NOT NULL
,[Cur_Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[LN_Amount] [DECIMAL](18, 6) NOT NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[Tbl_Amount] [DECIMAL](18, 6) NULL
,[Tender_Amount] [DECIMAL](18, 6) NULL
,[Return_Amount] [DECIMAL](18, 6) NULL
,[Action_Type] [NVARCHAR](50) NULL
,[In_Words] [NVARCHAR](1024) NULL
,[Remarks] [NVARCHAR](500) NULL
,[R_Invoice] [BIT] NULL
,[Is_Printed] [BIT] NULL
,[No_Print] [INT] NULL
,[Printed_By] [NVARCHAR](50) NULL
,[Printed_Date] [DATETIME] NULL
,[Audit_Lock] [BIT] NULL
,[Enter_By] [NVARCHAR](50) NOT NULL
,[Enter_Date] [DATETIME] NOT NULL
,[Reconcile_By] [NVARCHAR](50) NULL
,[Reconcile_Date] [DATETIME] NULL
,[Auth_By] [NVARCHAR](50) NULL
,[Auth_Date] [DATETIME] NULL
,[Cleared_By] [NVARCHAR](50) NULL
,[Cleared_Date] [DATETIME] NULL
,[Cancel_By] [NVARCHAR](50) NULL
,[Cancel_Date] [DATETIME] NULL
,[Cancel_Remarks] [NVARCHAR](1024) NULL
,[CUnit_Id] [INT] NULL
,[CBranch_Id] [INT] NOT NULL
,[IsAPIPosted] [BIT] NULL
,[IsRealtime] [BIT] NULL
,[FiscalYearId] [INT] NOT NULL
,[MShipId] [INT] NULL
,[TableId] [INT] NULL
,[DoctorId] [INT] NULL
,[PatientId] [BIGINT] NULL
,[HDepartmentId] [INT] NULL
,[PAttachment1] [IMAGE] NULL
,[PAttachment2] [IMAGE] NULL
,[PAttachment3] [IMAGE] NULL
,[PAttachment4] [IMAGE] NULL
,[PAttachment5] [IMAGE] NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
)
END;
IF OBJECT_ID('AMS.temp_SB_Details') IS NULL
BEGIN
CREATE TABLE [AMS].[temp_SB_Details] (
[SB_Invoice] [NVARCHAR](50) NOT NULL
,[Invoice_SNo] [INT] NOT NULL
,[P_Id] [BIGINT] NOT NULL
,[Gdn_Id] [INT] NULL
,[Alt_Qty] [DECIMAL](18, 6) NULL
,[Alt_UnitId] [INT] NULL
,[Qty] [DECIMAL](18, 6) NULL
,[Unit_Id] [INT] NULL
,[Rate] [DECIMAL](18, 6) NOT NULL
,[B_Amount] [DECIMAL](18, 6) NOT NULL
,[T_Amount] [DECIMAL](18, 6) NOT NULL
,[N_Amount] [DECIMAL](18, 6) NOT NULL
,[AltStock_Qty] [DECIMAL](18, 6) NULL
,[Stock_Qty] [DECIMAL](18, 6) NULL
,[Narration] [NVARCHAR](500) NULL
,[SO_Invoice] [NVARCHAR](50) NULL
,[SO_Sno] [NUMERIC](18, 0) NULL
,[SC_Invoice] [NVARCHAR](50) NULL
,[SC_SNo] [NVARCHAR](50) NULL
,[Tax_Amount] [DECIMAL](18, 6) NULL
,[V_Amount] [DECIMAL](18, 6) NULL
,[V_Rate] [DECIMAL](18, 6) NULL
,[Free_Unit_Id] [INT] NULL
,[Free_Qty] [DECIMAL](18, 6) NULL
,[StockFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraFree_Unit_Id] [INT] NULL
,[ExtraFree_Qty] [DECIMAL](18, 6) NULL
,[ExtraStockFree_Qty] [DECIMAL](18, 6) NULL
,[T_Product] [BIT] NULL
,[S_Ledger] [BIGINT] NULL
,[SR_Ledger] [BIGINT] NULL
,[SZ1] [NVARCHAR](50) NULL
,[SZ2] [NVARCHAR](50) NULL
,[SZ3] [NVARCHAR](50) NULL
,[SZ4] [NVARCHAR](50) NULL
,[SZ5] [NVARCHAR](50) NULL
,[SZ6] [NVARCHAR](50) NULL
,[SZ7] [NVARCHAR](50) NULL
,[SZ8] [NVARCHAR](50) NULL
,[SZ9] [NVARCHAR](50) NULL
,[SZ10] [NVARCHAR](50) NULL
,[Serial_No] [NVARCHAR](50) NULL
,[Batch_No] [NVARCHAR](50) NULL
,[Exp_Date] [DATETIME] NULL
,[Manu_Date] [DATETIME] NULL
,[PDiscountRate] [DECIMAL](18, 6) NULL
,[PDiscount] [DECIMAL](18, 6) NULL
,[BDiscountRate] [DECIMAL](18, 6) NULL
,[BDiscount] [DECIMAL](18, 6) NULL
,[ServiceChargeRate] [DECIMAL](18, 6) NULL
,[ServiceCharge] [DECIMAL](18, 6) NULL
,[MaterialPost] [char](1) NULL
,[SyncGlobalId] [UNIQUEIDENTIFIER] NULL
,[SyncOriginId] [UNIQUEIDENTIFIER] NULL
,[SyncCreatedOn] [DATETIME] NULL
,[SyncLastPatchedOn] [DATETIME] NULL
,[SyncRowVersion] [SMALLINT] NULL
,[SyncBaseId] [UNIQUEIDENTIFIER] NULL
)
END;

IF OBJECT_ID('AMS.BookDetails') IS NULL
BEGIN
CREATE TABLE [AMS].[BookDetails] (
[BookId] [BIGINT] NOT NULL
,[PrintDesc] [NVARCHAR](200) NOT NULL
,[ISBNNo] [NVARCHAR](100) NULL
,[Author] [NVARCHAR](100) NULL
,[Publisher] [NVARCHAR](100) NULL
,CONSTRAINT [PK_BookDetails] PRIMARY KEY CLUSTERED ([BookId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_BookDetails] UNIQUE NONCLUSTERED ([ISBNNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [AMS].[BookDetails] WITH CHECK ADD CONSTRAINT [FK_BookDetails_Product] FOREIGN KEY ([BookId]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[BookDetails] CHECK CONSTRAINT [FK_BookDetails_Product]
END;

IF OBJECT_ID('AMS.BookDetails') IS NULL
BEGIN
CREATE TABLE [AMS].[BookDetails] (
[BookId] [BIGINT] NOT NULL
,[PrintDesc] [NVARCHAR](200) NOT NULL
,[ISBNNo] [NVARCHAR](100) NULL
,[Author] [NVARCHAR](100) NULL
,[Publisher] [NVARCHAR](100) NULL
,CONSTRAINT [PK_BookDetails] PRIMARY KEY CLUSTERED ([BookId] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
,CONSTRAINT [IX_BookDetails] UNIQUE NONCLUSTERED ([ISBNNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [AMS].[BookDetails] WITH CHECK ADD CONSTRAINT [FK_BookDetails_Product] FOREIGN KEY ([BookId]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[BookDetails] CHECK CONSTRAINT [FK_BookDetails_Product]
END;

IF OBJECT_ID('[AMS].[ProductOpeningBatchWise]') IS NULL
BEGIN
CREATE TABLE [AMS].[ProductOpeningBatchWise] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[SNo] [INT] NOT NULL
,[BatchNo] [NVARCHAR](50) NOT NULL
,[MfgDate] [Date] NOT NULL
,[ExpiryDate] [Date] NOT NULL
,[StockQty] [DECIMAL](18, 6) NOT NULL
,[MRPRate] [DECIMAL](18, 6) NULL
,CONSTRAINT [IX_ProductOpeningBatchWise] UNIQUE NONCLUSTERED ([VoucherNo] ASC, [ProductId] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [AMS].[ProductOpeningBatchWise] WITH CHECK ADD CONSTRAINT [FK_ProductOpeningBatchWise_Product] FOREIGN KEY ([ProductId]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[ProductOpeningBatchWise] CHECK CONSTRAINT [FK_ProductOpeningBatchWise_Product]
ALTER TABLE [AMS].[ProductOpeningBatchWise] WITH CHECK ADD CONSTRAINT [FK_ProductOpeningBatchWise_ProductOpening] FOREIGN KEY ([VoucherNo], [ProductId], [SNo]) REFERENCES [AMS].[ProductOpening] ([Voucher_No], [Product_Id], [Serial_No])
ALTER TABLE [AMS].[ProductOpeningBatchWise] CHECK CONSTRAINT [FK_ProductOpeningBatchWise_ProductOpening]
END;

IF OBJECT_ID('[AMS].[PurchaseBatchWise]') IS NULL
BEGIN
CREATE TABLE [AMS].[PurchaseBatchWise] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[SNo] [INT] NOT NULL
,[BatchNo] [NVARCHAR](50) NOT NULL
,[MfgDate] [Date] NOT NULL
,[ExpiryDate] [Date] NOT NULL
,[StockQty] [DECIMAL](18, 6) NOT NULL
,[MRPRate] [DECIMAL](18, 6) NULL
,CONSTRAINT [IX_PurchaseBatchWise] UNIQUE NONCLUSTERED ([VoucherNo] ASC, [ProductId] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [AMS].[PurchaseBatchWise] WITH CHECK ADD CONSTRAINT [FK_PurchaseBatchWise_Product] FOREIGN KEY ([ProductId]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[PurchaseBatchWise] CHECK CONSTRAINT [FK_PurchaseBatchWise_Product]
ALTER TABLE [AMS].[PurchaseBatchWise] WITH CHECK ADD CONSTRAINT [FK_PurchaseBatchWise_Purchase] FOREIGN KEY ([VoucherNo]) REFERENCES [AMS].[PB_Master] ([PB_Invoice])
ALTER TABLE [AMS].[PurchaseBatchWise] CHECK CONSTRAINT [FK_PurchaseBatchWise_Purchase]
END;


IF OBJECT_ID('[AMS].[SalesBatchWise]') IS NULL
BEGIN
CREATE TABLE [AMS].[SalesBatchWise] (
[VoucherNo] [NVARCHAR](50) NOT NULL
,[ProductId] [BIGINT] NOT NULL
,[SNo] [INT] NOT NULL
,[BatchNo] [NVARCHAR](50) NOT NULL
,[MfgDate] [Date] NOT NULL
,[ExpiryDate] [Date] NOT NULL
,[StockQty] [DECIMAL](18, 6) NOT NULL
,[MRPRate] [DECIMAL](18, 6) NULL
,CONSTRAINT [IX_SalesBatchWise] UNIQUE NONCLUSTERED ([VoucherNo] ASC, [ProductId] ASC, [SNo] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
ALTER TABLE [AMS].[SalesBatchWise] WITH CHECK ADD CONSTRAINT [FK_SalesBatchWise_Product] FOREIGN KEY ([ProductId]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[SalesBatchWise] CHECK CONSTRAINT [FK_SalesBatchWise_Product]
ALTER TABLE [AMS].[SalesBatchWise] WITH CHECK ADD CONSTRAINT [FK_SalesBatchWise_Sales] FOREIGN KEY ([VoucherNo]) REFERENCES [AMS].[SB_Master] ([SB_Invoice])
ALTER TABLE [AMS].[SalesBatchWise] CHECK CONSTRAINT [FK_SalesBatchWise_Sales]
END;