
/****** Object:  UserDefinedFunction [AMS].[Fn_Select_ClassCode]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON



/****** Object:  Table [AMS].[AccountDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[AccountDetails]([Module] [char](10) NOT NULL,	[Serial_No] [int] NOT NULL,	[Voucher_No] [nvarchar](50) NOT NULL,	[Voucher_Date] [datetime] NOT NULL,	[Voucher_Miti] [nvarchar](50) NOT NULL,	[Voucher_Time] [datetime] NOT NULL,	[Ledger_ID] [bigint] NOT NULL,	[CbLedger_ID] [bigint] NULL,	[Subleder_ID] [int] NULL,	[Agent_ID] [int] NULL,	[Department_ID1] [int] NULL,	[Department_ID2] [int] NULL,	[Department_ID3] [int] NULL, 	[Department_ID4] [int] NULL,	[Currency_ID] [int] NOT NULL,	[Currency_Rate] [decimal](18, 6) NOT NULL CONSTRAINT [DF_AccountDetails_Currency_Rate]  DEFAULT ((1)),	[Debit_Amt] [decimal](18, 6) NOT NULL,	[Credit_Amt] [decimal](18, 6) NOT NULL,	[LocalDebit_Amt] [decimal](18, 6) NOT NULL,	[LocalCredit_Amt] [decimal](18, 6) NOT NULL,	[DueDate] [datetime] NULL,	[DueDays] [int] NULL,	[Narration] [nvarchar](1024) NULL,	[Remarks] [nvarchar](1024) NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[RefNo] [nvarchar](50) NULL,	[RefDate] [nvarchar](50) NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Authorize_By] [varchar](50) NULL,	[Authorize_Date] [datetime] NULL,	[Clearing_By] [nvarchar](50) NULL,	[Clearing_Date] [datetime] NULL,	[Posted_By] [varchar](50) NULL,	[Posted_Date] [datetime] NULL,	[Cheque_No] [nvarchar](50) NULL,	[Cheque_Date] [datetime] NULL,	[Cheque_Miti] [nvarchar](50) NULL,	[PartyName] [nvarchar](100) NULL,	[PartyLedger_Id] [int] NULL,	[Party_PanNo] [nvarchar](50) NULL,	[Branch_ID] [int] NOT NULL,	[CmpUnit_ID] [int] NULL,	[FiscalYearId] [int] NOT NULL,	[DoctorId] [int] NULL,	[PatientId] [bigint] NULL,	[HDepartmentId] [int] NULL, CONSTRAINT [PK_AccountDetails] PRIMARY KEY CLUSTERED (	[Module] ASC,	[Serial_No] ASC,	[Voucher_No] ASC,	[Ledger_ID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[AccountGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[AccountGroup]([GrpId] [int] NOT NULL,	[GrpName] [nvarchar](200) NOT NULL,	[GrpCode] [nvarchar](50) NOT NULL,	[Schedule] [int] NOT NULL CONSTRAINT [DF_AccountGroup_Schedule]  DEFAULT ((1)),	[PrimaryGrp] [nvarchar](50) NOT NULL,	[GrpType] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_Id] [int] NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_AccountGroup_Status]  DEFAULT ((1)),	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_AccountGroup_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL,	[PrimaryGroupId] [int] NULL DEFAULT ((0)),	[IsDefault] [char](1) NULL DEFAULT ((0)),	[NepaliDesc] [nvarchar](200) NULL, CONSTRAINT [PK_AccountGroup] PRIMARY KEY CLUSTERED (	[GrpId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_AccountGroup] UNIQUE NONCLUSTERED (	[GrpName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_AccountGroup_1] UNIQUE NONCLUSTERED ([GrpCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[AccountSubGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[AccountSubGroup]([SubGrpId] [int] NOT NULL,	[SubGrpName] [nvarchar](200) NOT NULL,	[GrpID] [int] NOT NULL,	[SubGrpCode] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_AccountSubGroup_Status]  DEFAULT ((1)),	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_AccountSubGroup_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL,	[PrimaryGroupId] [int] NULL DEFAULT ((0)),	[PrimarySubGroupId] [int] NULL DEFAULT ((0)),	[IsDefault] [char](1) NULL DEFAULT ((0)),	[NepaliDesc] [nvarchar](200) NULL, CONSTRAINT [PK_AccountSubGroup] PRIMARY KEY CLUSTERED (	[SubGrpId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_AccountSubGroup] UNIQUE NONCLUSTERED (	[SubGrpName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Area]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Area]([AreaId] [int] NOT NULL,	[AreaName] [nvarchar](150) NOT NULL,	[AreaCode] [nvarchar](50) NOT NULL,	[Country] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[Main_Area] [int] NULL,	[Status] [bit] NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL, CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED (	[AreaId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_AreaDesc] UNIQUE NONCLUSTERED (	[AreaName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_AreaShortName] UNIQUE NONCLUSTERED (	[AreaCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;

SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[BillOfMaterial_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[BillOfMaterial_Details]([MemoNo] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[ProductId] [bigint] NOT NULL,	[GdnId] [int] NOT NULL,	[CostCenterId] [int] NOT NULL,	[AltQty] [decimal](18, 6) NULL,	[AltUnitId] [int] NULL,	[Qty] [decimal](18, 6) NULL,	[UnitId] [int] NULL,	[Rate] [decimal](18, 6) NULL,	[Amount] [decimal](18, 6) NULL,	[Narration] [nvarchar](1024) NULL,	[Order_No] [nvarchar](50) NULL,	[Order_SNo] [int] NULL, CONSTRAINT [PK_BillOfMaterial_Details] PRIMARY KEY CLUSTERED (	[MemoNo] ASC,	[SNo] ASC,	[ProductId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[BillOfMaterial_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[BillOfMaterial_Master](	[MemoNo] [nvarchar](50) NOT NULL,	[VoucherDate] [datetime] NOT NULL,	[VoucherMiti] [nvarchar](10) NOT NULL,	[MemoDesc] [nvarchar](1024) NOT NULL,	[OrderNo] [nvarchar](50) NOT NULL,	[OrderDate] [datetime] NOT NULL,	[FGProductId] [bigint] NOT NULL,	[AltQty] [decimal](18, 6) NOT NULL,	[AltUnitId] [int] NOT NULL,	[Qty] [decimal](18, 6) NOT NULL,	[UnitId] [int] NOT NULL,	[Factor] [decimal](18, 6) NULL,	[FFactor] [decimal](18, 6) NULL,	[CostRate] [decimal](18, 6) NULL,	[GdnId] [int] NOT NULL,	[CostCenterId] [int] NOT NULL,	[Dep1] [int] NULL,	[Dep2] [int] NULL,	[Dep3] [int] NULL,	[Dep4] [int] NULL,	[TotalQty] [decimal](18, 6) NOT NULL,	[NetAmount] [decimal](18, 6) NOT NULL,	[Remarks] [nvarchar](1024) NULL,	[Module] [nvarchar](50) NOT NULL,	[ActionType] [nvarchar](50) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[ReconcileBy] [nvarchar](50) NULL,	[ReconcileDate] [datetime] NULL,	[AuthorisedBy] [nvarchar](50) NULL,	[AuthorisedDate] [datetime] NULL,	[CompanyUnitId] [int] NULL,	[BranchId] [int] NOT NULL, CONSTRAINT [PK_BillOfMaterial_Master_1] PRIMARY KEY CLUSTERED (	[MemoNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[Branch]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Branch]([Branch_Id] [int] NOT NULL,	[Branch_Name] [nvarchar](200) NOT NULL,	[Branch_Code] [nvarchar](50) NOT NULL,	[Reg_Date] [datetime] NULL,	[Address] [nvarchar](500) NULL,	[Country] [nvarchar](50) NULL,	[State] [nvarchar](50) NULL,	[City] [nvarchar](50) NULL,	[PhoneNo] [nvarchar](50) NULL,	[Fax] [nvarchar](50) NULL,	[Email] [nvarchar](80) NULL,	[ContactPerson] [nvarchar](50) NULL,	[ContactPersonAdd] [nvarchar](50) NULL,	[ContactPersonPhone] [nvarchar](50) NULL,	[Created_By] [nvarchar](50) NULL,	[Created_Date] [datetime] NULL,	[Modify_By] [nvarchar](50) NULL,	[Modify_Date] [datetime] NULL, CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED (	[Branch_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_BranchDesc] UNIQUE NONCLUSTERED (	[Branch_Name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_BranchShortName] UNIQUE NONCLUSTERED (	[Branch_Code] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[BranchWiseLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[BranchWiseLedger]([BranchId] [int] NULL,	[LedgerId] [bigint] NULL,	[Mapped] [bit] NULL,	[Catery] [nvarchar](50) NULL,	[EnterBy] [nvarchar](50) NULL,	[EnterDate] [datetime] NULL) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[Budget]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON
CREATE TABLE [AMS].[Budget]([BudgetId] [int] NOT NULL,	[LedgerId] [bigint] NOT NULL,	[Dep1] [int] NULL,	[Dep2] [int] NULL,	[Dep3] [int] NULL,	[Dep4] [int] NULL,	[Amount] [decimal](18, 6) NOT NULL,	[MonthDesc] [nvarchar](50) NOT NULL,	[Date] [datetime] NULL,	[Miti] [varchar](10) NULL,	[EnterBy] [nvarchar](50) NULL,	[EnterDate] [datetime] NULL, CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED (	[BudgetId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[BudgetLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[BudgetLedger](	[BLID] [int] NULL,	[LedgerId] [bigint] NULL,	[MonthsDesc] [nvarchar](50) NULL,	[Amount] [decimal](16, 6) NULL,	[EnterBy] [nvarchar](50) NULL,	[EnterDate] [datetime] NULL) ON [PRIMARY] ; 



/****** Object:  Table [AMS].[CashBankDenomination]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[CashBankDenomination]([VoucherNo] [nvarchar](50) NOT NULL,	[VoucherDate] [datetime] NOT NULL,	[VoucherMiti] [nvarchar](10) NOT NULL,	[TotalAmount] [decimal](18, 6) NOT NULL,	[R1000] [decimal](18, 6) NOT NULL,	[R500] [decimal](18, 6) NOT NULL,	[R100] [decimal](18, 6) NOT NULL,	[R50] [decimal](18, 6) NOT NULL,	[R20] [decimal](18, 6) NOT NULL,	[R10] [decimal](18, 6) NOT NULL,	[R5] [decimal](18, 6) NOT NULL,	[R2] [decimal](18, 6) NOT NULL,	[R1] [decimal](18, 6) NOT NULL,	[RO] [decimal](18, 6) NOT NULL,	[RC] [decimal](18, 6) NOT NULL,	[RemainAmt] [decimal](18, 6) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL) ON [PRIMARY] ; 



/****** Object:  Table [AMS].[CashClosing]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[CashClosing]([CC_Id] [int] IDENTITY(1,1) NOT NULL,	[EnterBy] [varchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[EnterMiti] [varchar](10) NOT NULL,	[EnterTime] [datetime] NOT NULL,	[CB_Balance] [nvarchar](50) NOT NULL,	[Cash_Sales] [decimal](18, 6) NOT NULL,	[Cash_Purchase] [decimal](18, 6) NOT NULL,	[TotalCash] [decimal](18, 6) NOT NULL,	[ThauQty] [decimal](18, 6) NOT NULL,	[ThouVal] [decimal](18, 6) NOT NULL,	[FivHunQty] [decimal](18, 6) NOT NULL,	[FivHunVal] [decimal](18, 6) NOT NULL,	[HunQty] [decimal](18, 6) NOT NULL,	[HunVal] [decimal](18, 6) NOT NULL,	[FiFtyQty] [decimal](18, 6) NOT NULL,	[FiftyVal] [decimal](18, 6) NOT NULL,	[TwenteyFiveQty] [decimal](18, 6) NOT NULL,	[TwentyFiveVal] [decimal](18, 6) NOT NULL,	[TwentyQty] [decimal](18, 6) NOT NULL,	[TwentyVal] [decimal](16, 6) NOT NULL,	[TenQty] [decimal](18, 6) NOT NULL,	[TenVal] [decimal](18, 6) NOT NULL,	[FiveQty] [decimal](18, 6) NOT NULL,	[FiveVal] [decimal](18, 6) NOT NULL,	[TwoQty] [decimal](18, 6) NOT NULL,	[TwoVal] [decimal](18, 6) NOT NULL,	[OneQty] [decimal](18, 6) NOT NULL,	[OneVal] [decimal](18, 6) NOT NULL,	[Cash_Diff] [decimal](18, 6) NOT NULL,	[Module] [char](10) NOT NULL,	[HandOverUser] [nvarchar](50) NOT NULL,	[Remarks] [varchar](250) NULL, CONSTRAINT [PK_CashClosing] PRIMARY KEY CLUSTERED (	[CC_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ; 

SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[CB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[CB_Details]([Voucher_No] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[Ledger_Id] [bigint] NOT NULL,	[Subledger_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Debit] [decimal](18, 6) NOT NULL,	[Credit] [decimal](18, 6) NOT NULL,	[LocalDebit] [decimal](18, 6) NOT NULL,	[LocalCredit] [decimal](18, 6) NOT NULL,	[Narration] [varchar](1024) NULL,	[Tbl_Amount] [decimal](18, 6) NULL,	[V_Amount] [decimal](18, 6) NULL,	[Party_No] [nvarchar](50) NULL,	[Invoice_Date] [datetime] NULL,	[Invoice_Miti] [nvarchar](50) NULL,	[VatLedger_Id] [bigint] NULL,	[PanNo] [int] NULL,	[Vat_Reg] [bit] NULL, CONSTRAINT [PK_CB_Details] PRIMARY KEY CLUSTERED (	[Voucher_No] ASC,	[SNo] ASC,	[Ledger_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[CB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[CB_Master](	[VoucherMode] [char](10) NOT NULL,	[Voucher_No] [nvarchar](50) NOT NULL,	[Voucher_Date] [datetime] NOT NULL,	[Voucher_Miti] [nvarchar](10) NOT NULL,	[Voucher_Time] [datetime] NOT NULL,	[Ref_VNo] [nvarchar](50) NULL,	[Ref_VDate] [datetime] NULL,	[VoucherType] [nvarchar](50) NOT NULL,	[Ledger_Id] [bigint] NOT NULL,	[CheqNo] [nvarchar](50) NULL,	[CheqDate] [datetime] NULL,	[CheqMiti] [nvarchar](10) NULL,	[Currency_Id] [int] NOT NULL,	[Currency_Rate] [decimal](18, 6) NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Remarks] [nvarchar](1024) NULL,	[Action_Type] [nvarchar](50) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[ReconcileBy] [nvarchar](50) NULL,	[ReconcileDate] [datetime] NULL,	[Audit_Lock] [bit] NULL,	[ClearingBy] [nvarchar](50) NULL,	[ClearingDate] [datetime] NULL,	[PrintValue] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[CUnit_Id] [int] NULL,	[FiscalYearId] [int] NOT NULL,	[PAttachment1] [image] NULL,	[PAttachment2] [image] NULL,	[PAttachment3] [image] NULL,	[PAttachment4] [image] NULL,	[PAttachment5] [image] NULL, CONSTRAINT [PK_CB_Master_1] PRIMARY KEY CLUSTERED (	[Voucher_No] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[CompanyInfo]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


CREATE TABLE [AMS].[CompanyInfo]([Company_Id] [tinyint] IDENTITY(1,1) NOT NULL,	[Company_Name] [nvarchar](200) NOT NULL,	[Company_Lo] [image] NULL,	[CReg_Date] [datetime] NULL,	[Address] [nvarchar](200) NULL,	[Country] [nvarchar](50) NULL,	[State] [nvarchar](50) NULL,	[City] [nvarchar](50) NULL,	[PhoneNo] [nvarchar](50) NULL,	[Fax] [nvarchar](50) NULL,	[Pan_No] [nvarchar](50) NULL,	[Email] [nvarchar](50) NULL,	[Website] [nvarchar](100) NULL,	[Database_Name] [nvarchar](50) NOT NULL,	[Database_Path] [nvarchar](100) NULL,	[Version_No] [nvarchar](50) NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_CompanyInfo_Status]  DEFAULT ((1)),	[CreateDate] [datetime] NULL,	[LoginDate] [datetime] NULL,	[SoftModule] [nvarchar](50) NULL, CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED (	[Company_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CompanyDesc] UNIQUE NONCLUSTERED (	[Company_Name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ;

/****** Object:  Table [AMS].[CompanyUnit]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[CompanyUnit]([CmpUnit_Id] [int] NOT NULL,	[CmpUnit_Name] [nvarchar](200) NOT NULL,	[CmpUnit_Code] [nvarchar](50) NOT NULL,	[Reg_Date] [datetime] NOT NULL,	[Address] [nvarchar](50) NULL,	[Country] [nvarchar](50) NULL,	[State] [nvarchar](50) NULL,	[City] [nvarchar](50) NULL,	[PhoneNo] [nvarchar](50) NULL,	[Fax] [nvarchar](50) NULL,	[Email] [nvarchar](90) NULL,	[ContactPerson] [nvarchar](50) NULL,	[ContactPersonAdd] [nvarchar](50) NULL,	[ContactPersonPhone] [nvarchar](50) NULL,	[Branch_Id] [int] NOT NULL,	[Created_By] [nvarchar](50) NOT NULL,	[Created_Date] [datetime] NOT NULL, CONSTRAINT [PK_CompanyUnit] PRIMARY KEY CLUSTERED(	[CmpUnit_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CompanyUnitDesc] UNIQUE NONCLUSTERED (	[CmpUnit_Name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CompanyUnitShortName] UNIQUE NONCLUSTERED (	[CmpUnit_Code] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


/****** Object:  Table [AMS].[CostCenter]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[CostCenter]([CCId] [int] NOT NULL,	[CCName] [nvarchar](200) NOT NULL,	[CCcode] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_Id] [int] NULL,	[Status] [bit] NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL, CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED (	[CCId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CostCenterDesc] UNIQUE NONCLUSTERED (	[CCName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CostCenterShortName] UNIQUE NONCLUSTERED ([CCcode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[CostCenterExpenses_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[CostCenterExpenses_Details]([CostingNo] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[LedgerId] [bigint] NOT NULL,	[CostCenterId] [int] NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Narration] [nvarchar](1024) NULL,	[OrderNo] [nvarchar](50) NULL,	[OrderSNo] [int] NULL, CONSTRAINT [PK_CostCenterExpenses_Details] PRIMARY KEY CLUSTERED (	[CostingNo] ASC,	[SNo] ASC,	[LedgerId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


/****** Object:  Table [AMS].[CostCenterExpenses_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[CostCenterExpenses_Master](	[CostingNo] [nvarchar](50) NOT NULL,	[VoucherDate] [datetime] NOT NULL,	[VoucherMiti] [nvarchar](10) NOT NULL,	[VoucherTime] [datetime] NOT NULL,	[OrderNo] [nvarchar](50) NULL,	[OrderDate] [datetime] NULL,	[FGProductId] [bigint] NOT NULL,	[GdnId] [int] NOT NULL,	[CostCenterId] [int] NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Remarks] [nvarchar](1024) NULL,	[ActionType] [nvarchar](50) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[CompanyUnitId] [int] NULL,	[BranchId] [int] NOT NULL,	[FiscalYearId] [int] NOT NULL, CONSTRAINT [PK_CostCenterExpenses_Master] PRIMARY KEY CLUSTERED (	[CostingNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


/****** Object:  Table [AMS].[Counter]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[Counter]([CId] [int] NOT NULL,	[CName] [nvarchar](50) NOT NULL,	[CCode] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_Counter_Status]  DEFAULT ((1)),	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[Printer] [varchar](250) NULL, CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED (	[CId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CounterDesc] UNIQUE NONCLUSTERED (	[CName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CounterShortName] UNIQUE NONCLUSTERED (	[CCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Currency]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Currency]([CId] [int] NOT NULL,	[CName] [nvarchar](100) NOT NULL,	[Ccode] [nvarchar](50) NOT NULL,	[CRate] [numeric](18, 6) NOT NULL CONSTRAINT [DF_Currency_CRate]  DEFAULT ((1)),	[Branch_Id] [int] NOT NULL,	[Company_Id] [int] NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_Currency_Status]  DEFAULT ((1)),	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_Currency_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL, CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED (	[CId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CurrencyDesc] UNIQUE NONCLUSTERED (	[CName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_CurrencyShortName] UNIQUE NONCLUSTERED (	[Ccode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


/****** Object:  Table [AMS].[DateMiti]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[DateMiti]([Date_Id] [bigint] NOT NULL,	[BS_Date] [varchar](10) NOT NULL,	[BS_DateDMY] [varchar](10) NOT NULL,	[AD_Date] [datetime] NOT NULL,	[BS_Months] [varchar](50) NOT NULL,	[AD_Months] [varchar](50) NOT NULL,	[Days] [varchar](50) NULL,	[Is_Holiday] [bit] NULL,	[Holiday] [varchar](1024) NULL, CONSTRAINT [PK_DateMiti] PRIMARY KEY CLUSTERED (	[Date_Id] ASC,	[BS_Date] ASC,	[BS_DateDMY] ASC,	[AD_Date] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_DateMiti] UNIQUE NONCLUSTERED (	[AD_Date] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_DateMiti_1] UNIQUE NONCLUSTERED (	[BS_DateDMY] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_DateMiti_2] UNIQUE NONCLUSTERED (	[AD_Date] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Department]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Department]([DId] [int] NOT NULL,	[DName] [nvarchar](50) NOT NULL,	[DCode] [nvarchar](50) NOT NULL,[Dlevel] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[Status] [bit] NOT NULL,	[EnterBy] [nvarchar](50) NULL,	[EnterDate] [datetime] NULL, CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED (	[DId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_DepartmentDesc] UNIQUE NONCLUSTERED (	[DName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_DepartmentShortName] UNIQUE NONCLUSTERED (	[DCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


/****** Object:  Table [AMS].[DllPrintDesigList]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[DllPrintDesigList](	[ListId] [int] NOT NULL,	[Module] [char](4) NOT NULL,	[DesignDesc] [nvarchar](max) NOT NULL, CONSTRAINT [PK_DllPrintDesigList_ListId] PRIMARY KEY CLUSTERED (	[ListId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[DocumentDesignPrint]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[DocumentDesignPrint]([DDP_Id] [int] NOT NULL,	[Module] [char](10) NOT NULL,	[Paper_Name] [nvarchar](100) NOT NULL,	[Is_Online] [bit] NOT NULL,	[NoOfPrint] [int] NOT NULL,	[Notes] [nvarchar](250) NULL,	[DesignerPaper_Name] [nvarchar](250) NULL,	[Created_By] [nvarchar](250) NULL,	[Created_Date] [datetime] NULL,	[Status] [bit] NOT NULL,	[Branch_Id] [int] NOT NULL,	[CmpUnit_Id] [int] NULL, CONSTRAINT [PK_PrintDocument_Design] PRIMARY KEY CLUSTERED (	[DDP_Id] ASC,	[Module] ASC,	[Paper_Name] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[DocumentNumbering]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[DocumentNumbering]([DocId] [int] NOT NULL,	[DocModule] [nvarchar](50) NOT NULL,	[DocUser] [nvarchar](50) NULL,	[DocDesc] [nvarchar](50) NOT NULL,	[DocStartDate] [date] NOT NULL,	[DocStartMiti] [varchar](10) NOT NULL,	[DocEndDate] [date] NOT NULL,	[DocEndMiti] [varchar](10) NOT NULL,	[DocType] [char](10) NOT NULL,	[DocPrefix] [nvarchar](50) NULL,	[DocSufix] [nvarchar](50) NULL,	[DocBodyLength] [numeric](18, 0) NOT NULL,	[DocTotalLength] [numeric](18, 0) NOT NULL,	[DocBlank] [bit] NOT NULL CONSTRAINT [DF_DocumentNumbering_DocBlank]  DEFAULT ((1)),	[DocBlankCh] [char](1) NOT NULL CONSTRAINT [DF_DocumentNumbering_DocBlankCh]  DEFAULT ((0)),	[DocBranch] [int] NOT NULL,	[DocUnit] [int] NULL,	[DocStart] [numeric](18, 0) NULL,	[DocCurr] [numeric](18, 0) NULL,	[DocEnd] [numeric](18, 0) NULL,	[DocDesign] [nvarchar](50) NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_DocumentNumbering_Status]  DEFAULT ((1)),	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_DocumentNumbering_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL,	[FY_Id] [int] NULL,	[FiscalYearId] [int] NULL, CONSTRAINT [PK_DocumentNumbering] PRIMARY KEY CLUSTERED (	[DocId] ASC,	[DocModule] ASC,	[DocDesc] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[FGR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[FGR_Details]([FGR_No] [varchar](15) NOT NULL,	[SNo] [int] NOT NULL,	[P_Id] [bigint] NOT NULL,	[Gdn_Id] [int] NULL,	[CC_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Alt_Qty] [decimal](18, 6) NOT NULL,	[Alt_UnitId] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit_Id] [int] NULL,	[AltStock_Qty] [decimal](18, 6) NOT NULL,	[Stock_Qty] [decimal](18, 6) NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[Cnv_Ratio] [decimal](18, 6) NOT NULL) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[FGR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[FGR_Master]([FGR_No] [varchar](15) NOT NULL,	[FGR_Date] [datetime] NOT NULL,	[FGR_Time] [datetime] NOT NULL,	[FGR_Miti] [varchar](10) NOT NULL,	[Gdn_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[GLId] [bigint] NULL,	[CBranch_Id] [int] NULL,	[CUnit_Id] [int] NULL,	[CC_Id] [int] NULL,	[SB_No] [varchar](250) NULL,	[SB_SNo] [int] NULL,	[SO_No] [varchar](250) NULL,	[Auth_By] [varchar](50) NULL,	[Auth_Date] [datetime] NULL,	[Source] [varchar](15) NULL,	[Export] [char](1) NULL,	[Remarks] [nvarchar](1024) NULL,	[Enter_By] [varchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[FiscalYearId] [int] NULL, CONSTRAINT [PK_FGR_Master] PRIMARY KEY CLUSTERED (	[FGR_No] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[FinanceSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[FinanceSetting]([FinId] [tinyint] NOT NULL,	[ProfiLossId] [bigint] NULL,	[CashId] [bigint] NULL,	[VATLedgerId] [bigint] NULL,	[PDCBankLedgerId] [bigint] NULL,	[ShortNameWisTransaction] [bit] NULL,	[WarngNegativeTransaction] [bit] NULL,	[NegativeTransaction] [nvarchar](50) NULL,	[VoucherDate] [bit] NULL,	[AgentEnable] [bit] NULL,	[AgentMandetory] [bit] NULL,	[DepartmentEnable] [bit] NULL,	[DepartmentMandetory] [bit] NULL,	[RemarksEnable] [bit] NULL,	[RemarksMandetory] [bit] NULL,	[NarrationMandetory] [bit] NULL,	[CurrencyEnable] [bit] NULL,	[CurrencyMandetory] [bit] NULL,	[SubledgerEnable] [bit] NULL,	[SubledgerMandetory] [bit] NULL,	[DetailsClassEnable] [bit] NULL,	[DetailsClassMandetory] [bit] NULL, CONSTRAINT [PK_FinanceSetting] PRIMARY KEY CLUSTERED (	[FinId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[FiscalYear]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[FiscalYear]([FY_Id] [int] IDENTITY(1,1) NOT NULL,	[AD_FY] [varchar](5) NOT NULL,	[BS_FY] [varchar](5) NOT NULL,	[Current_FY] [bit] NOT NULL,	[Start_ADDate] [datetime] NOT NULL,	[End_ADDate] [datetime] NOT NULL,	[Start_BSDate] [varchar](10) NOT NULL,	[End_BSDate] [varchar](10) NOT NULL, CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED (	[FY_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_FiscalYearAD] UNIQUE NONCLUSTERED (	[AD_FY] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_FiscalYearBS] UNIQUE NONCLUSTERED (	[BS_FY] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Floor]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[Floor](	[FloorId] [int] NOT NULL,	[Description] [nvarchar](50) NOT NULL,	[ShortName] [nvarchar](50) NOT NULL,	[Type] [char](10) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[Branch_Id] [int] NOT NULL,	[Company_Id] [int] NULL,	[Status] [bit] NOT NULL, CONSTRAINT [PK_Floor] PRIMARY KEY CLUSTERED (	[FloorId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_FloorDesc] UNIQUE NONCLUSTERED ([Description] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_FloorShortName] UNIQUE NONCLUSTERED (	[ShortName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[GeneralLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[GeneralLedger](	[GLID] [bigint] NOT NULL,	[GLName] [nvarchar](200) NOT NULL,	[GLCode] [nvarchar](50) NOT NULL,	[ACCode] [nvarchar](50) NOT NULL,	[GLType] [nvarchar](50) NOT NULL,	[GrpId] [int] NOT NULL,	[SubGrpId] [int] NULL,	[PanNo] [nvarchar](50) NULL,	[AreaId] [int] NULL,	[AgentId] [int] NULL,	[CurrId] [int] NULL CONSTRAINT [DF_GeneralLedger_CurrId]  DEFAULT ((1)),	[CrDays] [numeric](18, 0) NOT NULL CONSTRAINT [DF_GeneralLedger_CrDays]  DEFAULT ((0)),	[CrLimit] [decimal](18, 6) NOT NULL CONSTRAINT [DF_GeneralLedger_CrLimit]  DEFAULT ((0.00)),	[CrTYpe] [nvarchar](50) NOT NULL CONSTRAINT [DF_GeneralLedger_CrTYpe]  DEFAULT (N'Ignore'),	[IntRate] [decimal](18, 6) NOT NULL CONSTRAINT [DF_GeneralLedger_IntRate]  DEFAULT ((0.00)),	[GLAddress] [nvarchar](500) NULL,	[PhoneNo] [nvarchar](50) NULL,	[LandLineNo] [nvarchar](50) NULL,	[OwnerName] [nvarchar](50) NULL,	[OwnerNumber] [nvarchar](50) NULL,	[Scheme] [nvarchar](50) NULL,	[Email] [nvarchar](50) NULL,	[Branch_id] [int] NOT NULL,	[Company_Id] [int] NULL,	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_GeneralLedger_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_GeneralLedger_Status]  DEFAULT ((1)),	[PrimaryGroupId] [int] NULL DEFAULT ((0)),	[PrimarySubGroupId] [int] NULL DEFAULT ((0)),	[IsDefault] [char](1) NULL DEFAULT ((0)),	[NepaliDesc] [nvarchar](200) NULL, CONSTRAINT [PK_GeneralLedger] PRIMARY KEY CLUSTERED (	[GLID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_GeneralLedgerAccount] UNIQUE NONCLUSTERED (	[ACCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_GeneralLedgerShortName] UNIQUE NONCLUSTERED (	[GLCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[down]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[down]([GID] [int] NOT NULL,	[GName] [nvarchar](80) NULL,	[GCode] [nvarchar](50) NULL,	[GLocation] [nvarchar](50) NULL,	[Status] [bit] NULL,	[EnterBy] [nvarchar](50) NULL,	[EnterDate] [datetime] NULL,	[CompUnit] [int] NULL,	[BranchUnit] [int] NULL, CONSTRAINT [PK_down] PRIMARY KEY CLUSTERED (	[GID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [downDesc] UNIQUE NONCLUSTERED (	[GName] ASC,	[GCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[GT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[GT_Details]([VoucherNo] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[ProId] [bigint] NOT NULL,	[ToGdn] [int] NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[AltQty] [decimal](18, 6) NOT NULL,	[AltUOM] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[UOM] [int] NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Narration] [varchar](1024) NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL, CONSTRAINT [PK_GT_Details] PRIMARY KEY CLUSTERED (	[VoucherNo] ASC,	[SNo] ASC,	[ProId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[GT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[GT_Master](	[VoucherNo] [nvarchar](50) NOT NULL,	[VoucherDate] [datetime] NOT NULL,	[VoucherMiti] [varchar](10) NOT NULL,	[FrmGdn] [int] NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Remarks] [varchar](1024) NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[CompanyUnit] [int] NULL,	[BranchId] [int] NOT NULL,	[ReconcileBy] [nvarchar](50) NULL,	[ReconcileDate] [datetime] NULL, CONSTRAINT [PK__downTr__C5DCEA07636EBA21] PRIMARY KEY CLUSTERED (	[VoucherNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Inv_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[Inv_Details]([Inv_No] [varchar](15) NOT NULL,	[SNo] [int] NOT NULL,[P_Id] [bigint] NOT NULL,	[Gdn_Id] [int] NULL,	[CC_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Alt_Qty] [decimal](18, 6) NOT NULL,	[Alt_UnitId] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit_Id] [int] NULL,	[AltStock_Qty] [decimal](18, 6) NOT NULL,	[Stock_Qty] [decimal](18, 6) NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[Cnv_Ratio] [decimal](18, 6) NOT NULL,	[Req_No] [varchar](15) NULL,	[Req_SNo] [int] NULL,	[Inv_TypeCode] [varchar](10) NULL) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Inv_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[Inv_Master]([Inv_No] [varchar](15) NOT NULL,	[Inv_Date] [datetime] NOT NULL,	[Inv_Time] [datetime] NOT NULL,	[Inv_Miti] [varchar](10) NOT NULL,	[Gdn_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[GLId] [bigint] NULL,	[CBranch_Id] [int] NULL,	[CUnit_Id] [int] NULL,	[CC_Id] [int] NULL,	[SB_No] [varchar](250) NULL,	[SB_SNo] [int] NULL,	[SO_No] [varchar](250) NULL,	[BOM_Id] [nvarchar](50) NULL,	[BOM_Desc] [varchar](500) NULL,	[Auth_By] [varchar](50) NULL,	[Auth_Date] [datetime] NULL,	[Req_No] [varchar](256) NULL,	[Ass_No] [varchar](50) NULL,	[Source] [varchar](50) NULL,	[FGR_No] [varchar](50) NULL,	[FGR_Qty] [decimal](16, 6) NULL,	[Export] [char](1) NULL,	[Remarks] [varchar](1024) NULL,	[Enter_By] [varchar](15) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[FiscalYearId] [int] NULL, CONSTRAINT [PK_Inv_Master] PRIMARY KEY CLUSTERED (	[Inv_No] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[InventorySetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING OFF

CREATE TABLE [AMS].[InventorySetting]([InvId] [tinyint] NOT NULL,	[OPLedgerId] [bigint] NULL,	[CSPLLedgerId] [bigint] NULL,	[CSBSLedgerId] [bigint] NULL,	[NegativeStock] [char](1) NULL,	[AlternetUnit] [bit] NULL,	[CostCenterEnable] [bit] NULL,	[CostCenterMandetory] [bit] NULL,	[CostCenterItemEnable] [bit] NULL,	[CostCenterItemMandetory] [bit] NULL,	[ChangeUnit] [bit] NULL,	[downEnable] [bit] NULL,	[downMandetory] [bit] NULL,	[RemarksEnable] [bit] NULL,	[downItemEnable] [bit] NULL,	[downItemMandetory] [bit] NULL,	[NarrationEnable] [bit] NULL,	[ShortNameWise] [bit] NULL,	[BatchWiseQtyEnable] [bit] NULL,	[ExpiryDate] [bit] NULL,	[FreeQty] [bit] NULL,	[downWiseFilter] [bit] NULL,	[downWiseStock] [bit] NULL, CONSTRAINT [PK_InventorySetting] PRIMARY KEY CLUSTERED (	[InvId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]

SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[IRDAPISetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[IRDAPISetting](	[IRDAPI] [nvarchar](max) NULL,	[IrdUser] [nvarchar](500) NULL,	[IrdUserPassword] [nvarchar](500) NULL,	[IrdCompanyPan] [nvarchar](50) NULL,	[IsIRDRegister] [tinyint] NULL) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ; 


/****** Object:  Table [AMS].[JuniorAgent]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[JuniorAgent]([AgentId] [int] NOT NULL,	[AgentName] [nvarchar](200) NOT NULL,	[AgentCode] [nvarchar](50) NOT NULL,	[Address] [nvarchar](500) NOT NULL,	[PhoneNo] [nvarchar](50) NOT NULL,	[GLCode] [bigint] NULL,	[Commission] [decimal](18, 6) NOT NULL,	[SAgent] [int] NULL,	[Email] [nvarchar](200) NULL,	[CRLimit] [numeric](18, 8) NOT NULL,	[CrDays] [nvarchar](50) NOT NULL,	[CrType] [nvarchar](50) NOT NULL,	[Branch_id] [int] NOT NULL,	[Company_ID] [int] NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[Status] [bit] NOT NULL, CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED (	[AgentId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_JuniorAgentShortName] UNIQUE NONCLUSTERED (	[AgentCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[JV_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[JV_Details]([Voucher_No] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[Ledger_Id] [bigint] NOT NULL,	[Subledger_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[CBLedger_Id] [bigint] NULL,	[Chq_No] [nvarchar](50) NULL,	[Chq_Date] [datetime] NULL,	[Debit] [decimal](18, 6) NOT NULL,	[Credit] [decimal](18, 6) NOT NULL,	[LocalDebit] [decimal](18, 6) NOT NULL,	[LocalCredit] [decimal](18, 6) NOT NULL,	[Narration] [varchar](1024) NULL,	[Tbl_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[Vat_Reg] [bit] NULL,	[Party_No] [nvarchar](50) NULL,	[Invoice_Date] [datetime] NULL,	[Invoice_Miti] [nvarchar](50) NULL,	[VatLedger_Id] [nvarchar](50) NULL,	[PanNo] [int] NULL, CONSTRAINT [PK_JV_Details] PRIMARY KEY CLUSTERED (	[Voucher_No] ASC,	[SNo] ASC,	[Ledger_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[JV_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[JV_Master](	[VoucherMode] [char](10) NOT NULL,	[Voucher_No] [nvarchar](50) NOT NULL,	[Voucher_Date] [datetime] NOT NULL,	[Voucher_Miti] [nvarchar](10) NOT NULL,	[Voucher_Time] [datetime] NOT NULL,	[Ref_VNo] [varchar](50) NULL,	[Ref_VDate] [datetime] NULL,	[Currency_Id] [int] NOT NULL,	[Currency_Rate] [decimal](18, 6) NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[Remarks] [varchar](1024) NULL,	[Action_Type] [nvarchar](50) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[Audit_Lock] [bit] NULL,	[ReconcileBy] [nvarchar](50) NULL,	[ReconcileDate] [datetime] NULL,	[ClearingBy] [nvarchar](50) NULL,	[ClearingDate] [datetime] NULL,	[PrintValue] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[CUnit_Id] [int] NULL,	[FiscalYearId] [int] NULL,	[PAttachment1] [image] NULL,	[PAttachment2] [image] NULL,	[PAttachment3] [image] NULL,	[PAttachment4] [image] NULL,	[PAttachment5] [image] NULL, CONSTRAINT [PK_Voucher_Main] PRIMARY KEY CLUSTERED (	[Voucher_No] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[LedgerOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[LedgerOpening](	[Opening_Id] [int] NOT NULL,	[Module] [char](10) NOT NULL,	[Voucher_No] [nvarchar](50) NOT NULL,	[OP_Date] [datetime] NOT NULL,	[OP_Miti] [nvarchar](50) NOT NULL,	[Serial_No] [int] NOT NULL,	[Ledger_Id] [bigint] NOT NULL,	[Subledger_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Currency_Id] [int] NOT NULL,	[Currency_Rate] [decimal](18, 6) NOT NULL,	[Debit] [decimal](18, 6) NOT NULL,	[LocalDebit] [decimal](18, 6) NOT NULL,	[Credit] [decimal](18, 6) NOT NULL,	[LocalCredit] [decimal](18, 6) NOT NULL,	[Narration] [nvarchar](1024) NULL,	[Remarks] [nvarchar](1024) NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Branch_Id] [int] NOT NULL,	[Company_Id] [int] NULL,	[FiscalYearId] [int] NOT NULL, CONSTRAINT [PK_LedgerOpening] PRIMARY KEY CLUSTERED (	[Opening_Id] ASC,	[Module] ASC,	[Voucher_No] ASC,	[Serial_No] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[MainArea]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[MainArea]([MAreaId] [int] NOT NULL,	[MAreaName] [nvarchar](100) NOT NULL,	[MAreaCode] [nvarchar](50) NOT NULL,	[MCountry] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[Status] [bit] NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL, CONSTRAINT [PK_MainArea] PRIMARY KEY CLUSTERED (	[MAreaId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_MainAreaDesc] UNIQUE NONCLUSTERED (	[MAreaName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_MainAreaShortName] UNIQUE NONCLUSTERED (	[MAreaCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[MemberShipSetup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[MemberShipSetup]([MShipId] [int] NOT NULL,	[MShipDesc] [nvarchar](200) NOT NULL,	[MShipShortName] [nvarchar](50) NOT NULL,	[PhoneNo] [nvarchar](50) NULL,	[LedgerId] [bigint] NOT NULL,	[EmailAdd] [nvarchar](200) NULL,	[MemberTypeId] [int] NOT NULL,	[MemberId] [nvarchar](50) NOT NULL,	[BranchId] [int] NOT NULL,	[CompanyUnitId] [int] NULL,	[MValidDate] [datetime] NOT NULL,	[MExpireDate] [datetime] NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[ActiveStatus] [bit] NOT NULL, CONSTRAINT [PK_MemberShipSetup] PRIMARY KEY CLUSTERED ([MShipId] ASC,	[MShipDesc] ASC,	[MShipShortName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[MemberType]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[MemberType]([MemberTypeId] [int] NOT NULL,	[MemberDesc] [nvarchar](200) NOT NULL,	[MemberShortName] [nvarchar](50) NOT NULL,	[Discount] [decimal](18, 6) NOT NULL,	[BranchId] [int] NOT NULL,	[CompanyUnitId] [int] NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[ActiveStatus] [bit] NOT NULL, CONSTRAINT [PK_MemberType] PRIMARY KEY CLUSTERED (	[MemberTypeId] ASC,	[MemberDesc] ASC,	[MemberShortName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[Notes_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[Notes_Details](	[VoucherMode] [char](10) NOT NULL,	[Voucher_No] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[Ledger_Id] [bigint] NOT NULL,	[Subledger_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Debit] [decimal](18, 6) NOT NULL,	[Credit] [decimal](18, 6) NOT NULL,	[LocalDebit] [decimal](18, 6) NOT NULL,	[LocalCredit] [decimal](18, 6) NOT NULL,	[Narration] [varchar](1024) NULL,	[T_Amount] [decimal](18, 6) NULL,	[V_Amount] [decimal](18, 6) NULL,	[Party_No] [nvarchar](50) NULL,	[Invoice_Date] [datetime] NULL,	[Invoice_Miti] [nvarchar](50) NULL,	[VatLedger_Id] [bigint] NULL,	[PanNo] [numeric](18, 0) NULL,	[Vat_Reg] [bit] NULL, CONSTRAINT [PK_Notes_Details] PRIMARY KEY CLUSTERED (	[Voucher_No] ASC,	[SNo] ASC,	[Ledger_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Notes_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Notes_Master](	[VoucherMode] [nvarchar](50) NOT NULL,	[Voucher_No] [nvarchar](50) NOT NULL,	[Voucher_Date] [datetime] NOT NULL,	[Voucher_Miti] [nvarchar](10) NOT NULL,	[Voucher_Time] [datetime] NOT NULL,	[Ref_VNo] [nvarchar](50) NULL,	[Ref_VDate] [datetime] NULL,	[VoucherType] [nvarchar](50) NULL,	[Ledger_Id] [bigint] NOT NULL,	[CheqNo] [nvarchar](50) NULL,	[CheqDate] [datetime] NULL,	[CheqMiti] [nvarchar](50) NULL,	[Subledger_Id] [int] NULL,	[Agent_Id] [int] NULL,	[Currency_Id] [int] NOT NULL,	[Currency_Rate] [decimal](18, 6) NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Remarks] [nvarchar](1024) NULL,	[Action_Type] [nvarchar](50) NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[ReconcileBy] [nvarchar](50) NULL,	[ReconcileDate] [datetime] NULL,	[Audit_Lock] [bit] NULL,	[ClearingBy] [nvarchar](50) NULL,	[ClearingDate] [datetime] NULL,	[PrintValue] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[CUnit_Id] [int] NULL,	[FiscalYearId] [int] NOT NULL, CONSTRAINT [PK_Notes_Master] PRIMARY KEY CLUSTERED (	[Voucher_No] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[PAB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PAB_Details](	[PAB_Invoice] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[PT_Id] [int] NOT NULL,	[Ledger_Id] [bigint] NOT NULL,	[CBLedger_Id] [bigint] NOT NULL,	[Subledger_Id] [int] NOT NULL,	[Agent_Id] [int] NULL,	[Product_Id] [bigint] NULL,	[Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[Percentage] [decimal](18, 6) NOT NULL,	[Term_Type] [char](2) NULL,	[PAB_Narration] [varchar](1024) NULL, CONSTRAINT [PK_PAB_Details] PRIMARY KEY CLUSTERED (	[PAB_Invoice] ASC,	[SNo] ASC,	[PT_Id] ASC,	[Ledger_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PAB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PAB_Master]([PAB_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NOT NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Agent_Id] [int] NULL,	[PB_Invoice] [nvarchar](50) NOT NULL,	[PB_Date] [datetime] NOT NULL,	[PB_Miti] [nvarchar](50) NOT NULL,	[PB_Qty] [decimal](18, 6) NOT NULL,	[PB_Amount] [decimal](18, 6) NOT NULL,	[LocalAmount] [decimal](18, 0) NOT NULL,	[Cur_Id] [int] NOT NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NULL,	[Remarks] [nvarchar](1024) NULL,	[Action_Type] [nvarchar](50) NULL,	[R_Invoice] [bit] NULL,	[No_Print] [decimal](18, 0) NULL,	[In_Words] [nvarchar](1024) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[Cleared_By] [nvarchar](50) NULL,	[Cleared_Date] [datetime] NULL,	[Cancel_By] [nvarchar](50) NULL,	[Cancel_Date] [datetime] NULL,	[Cancel_Remarks] [nvarchar](1024) NULL,	[CUnit_Id] [int] NOT NULL,	[CBranch_Id] [int] NULL,	[FiscalYearId] [int] NOT NULL, CONSTRAINT [PK_PAB_Master] PRIMARY KEY CLUSTERED (	[PAB_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ;


/****** Object:  Table [AMS].[PB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PB_Details]([PB_Invoice] [nvarchar](50) NOT NULL,	[Invoice_SNo] [int] NOT NULL,	[P_Id] [bigint] NOT NULL,	[Gdn_Id] [int] NULL,	[Alt_Qty] [decimal](18, 6) NOT NULL,	[Alt_UnitId] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit_Id] [int] NULL,	[Rate] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[AltStock_Qty] [decimal](18, 6) NOT NULL,	[Stock_Qty] [decimal](18, 6) NOT NULL,	[Narration] [nvarchar](1024) NULL,	[PO_Invoice] [nvarchar](50) NULL,	[PO_Sno] [numeric](18, 0) NULL,	[PC_Invoice] [nvarchar](50) NULL,	[PC_SNo] [nvarchar](50) NULL,	[Tax_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[V_Rate] [decimal](18, 6) NOT NULL,	[Free_Unit_Id] [int] NULL,	[Free_Qty] [decimal](18, 6) NULL,	[StockFree_Qty] [decimal](18, 6) NULL,	[ExtraFree_Unit_Id] [int] NULL,	[ExtraFree_Qty] [decimal](18, 6) NULL,	[ExtraStockFree_Qty] [decimal](18, 6) NULL,	[T_Product] [bit] NULL,	[P_Ledger] [bigint] NULL,	[PR_Ledger] [bigint] NULL,	[SZ1] [nvarchar](50) NULL,	[SZ2] [nvarchar](50) NULL,	[SZ3] [nvarchar](50) NULL,	[SZ4] [nvarchar](50) NULL,	[SZ5] [nvarchar](50) NULL,	[SZ6] [nvarchar](50) NULL,	[SZ7] [nvarchar](50) NULL,	[SZ8] [nvarchar](50) NULL,	[SZ9] [nvarchar](50) NULL,	[SZ10] [nvarchar](50) NULL,	[Serial_No] [nvarchar](50) NULL,	[Batch_No] [nvarchar](50) NULL,	[Exp_Date] [datetime] NULL,	[Manu_Date] [datetime] NULL, [TaxExempted_Amount] [decimal](18, 6) NULL, CONSTRAINT [PK_PB_Details] PRIMARY KEY CLUSTERED (	[PB_Invoice] ASC,	[Invoice_SNo] ASC,	[P_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[PB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PB_Master](	[PB_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NOT NULL,	[PB_Vno] [nvarchar](50) NULL,	[Vno_Date] [datetime] NULL,	[Vno_Miti] [nvarchar](10) NULL,	[Vendor_ID] [bigint] NOT NULL,	[Party_Name] [nvarchar](100) NULL,	[Vat_No] [nvarchar](50) NULL,	[Contact_Person] [nvarchar](50) NULL,	[Mobile_No] [nvarchar](50) NULL,	[Address] [nvarchar](90) NULL,	[ChqNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[Invoice_Type] [nvarchar](50) NOT NULL,	[Invoice_In] [nvarchar](50) NOT NULL,	[DueDays] [int] NULL,	[DueDate] [datetime] NULL,	[Agent_Id] [int] NULL,	[Subledger_Id] [int] NULL,	[PO_Invoice] [nvarchar](250) NULL,	[PO_Date] [datetime] NULL,	[PC_Invoice] [nvarchar](250) NULL,	[PC_Date] [datetime] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Cur_Id] [int] NOT NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[Counter_ID] [int] NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[LN_Amount] [decimal](18, 6) NOT NULL,	[Tender_Amount] [decimal](18, 6) NOT NULL,	[Change_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[Tbl_Amount] [decimal](18, 6) NOT NULL,	[Action_type] [nvarchar](50) NULL,	[R_Invoice] [bit] NULL,	[No_Print] [decimal](18, 0) NULL,	[In_Words] [nvarchar](1024) NULL,	[Remarks] [nvarchar](1024) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[Cleared_By] [nvarchar](50) NULL,	[Cleared_Date] [datetime] NULL,	[CancelBy] [nvarchar](50) NULL,	[CancelDate] [datetime] NULL,	[CancelRemarks] [nvarchar](1024) NULL,	[CUnit_Id] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[FiscalYearId] [int] NOT NULL,	[PAttachment1] [image] NULL,	[PAttachment2] [image] NULL,	[PAttachment3] [image] NULL,	[PAttachment4] [image] NULL,	[PAttachment5] [image] NULL, CONSTRAINT [PK_PB] PRIMARY KEY CLUSTERED(	[PB_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [AMS].[PB_OtherMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PB_OtherMaster]([PAB_Invoice] [nvarchar](50) NOT NULL,	[PPNo] [nvarchar](50) NOT NULL,	[PPDate] [datetime] NOT NULL,	[TaxableAmount] [decimal](18, 6) NOT NULL,	[VatAmount] [decimal](18, 6) NOT NULL,	[CustomAgent] [nvarchar](50) NULL,	[Transportation] [nvarchar](50) NULL,	[VechileNo] [nvarchar](50) NULL,	[Cn_No] [varchar](25) NULL,	[Cn_Date] [datetime] NULL,	[BankDoc] [nvarchar](50) NULL, CONSTRAINT [PK_Purchase_ImpDoc] PRIMARY KEY CLUSTERED (	[PAB_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [UQ__Purchase__2FF6D3DA42E1EEFE] UNIQUE NONCLUSTERED (	[PAB_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PB_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PB_Term]([PB_VNo] [nvarchar](50) NOT NULL,	[PT_Id] [int] NOT NULL,	[SNo] [int] NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Term_Type] [char](2) NOT NULL,	[Product_Id] [bigint] NULL,	[Taxable] [char](1) NULL, CONSTRAINT [PK_PB_Term] PRIMARY KEY CLUSTERED (	[PB_VNo] ASC,	[PT_Id] ASC,	[SNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PBT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PBT_Details]([PBT_Invoice] [nvarchar](50) NOT NULL,	[Invoice_SNo] [numeric](18, 0) NOT NULL,	[Slb_Id] [int] NOT NULL,	[PGrp_Id] [int] NOT NULL,	[P_Id] [bigint] NOT NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Flight_Date] [datetime] NOT NULL,	[Sector] [nvarchar](100) NULL,	[Fare_Amount] [decimal](18, 6) NOT NULL,	[FC_Amount] [decimal](18, 6) NOT NULL,	[PSC_Amount] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[Trm_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[Narration] [nvarchar](500) NULL,	[Tax_Amount] [decimal](18, 6) NULL,	[VAT_Amount] [decimal](18, 6) NULL,	[VAT_Rate] [decimal](18, 6) NULL, CONSTRAINT [PK_PBT_Details] PRIMARY KEY CLUSTERED (	[PBT_Invoice] ASC,	[Invoice_SNo] ASC,	[Slb_Id] ASC,	[PGrp_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[PBT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PBT_Master]([PBT_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NOT NULL,	[Ref_Vno] [nvarchar](50) NULL,	[Ref_Date] [datetime] NULL,	[Vendor_Id] [bigint] NOT NULL,	[Party_Name] [nvarchar](100) NULL,	[Vat_No] [nvarchar](50) NULL,	[Contact_Person] [nvarchar](50) NULL,	[Mobile_No] [nvarchar](50) NULL,	[Address] [nvarchar](90) NULL,	[ChqNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[Invoice_Type] [nvarchar](50) NULL,	[Invoice_In] [nvarchar](50) NULL,	[DueDays] [int] NULL,	[DueDate] [datetime] NULL,	[Cur_Id] [int] NOT NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[Fare_Amount] [decimal](18, 6) NOT NULL,	[FC_Amount] [decimal](18, 6) NOT NULL,	[PSC_Amount] [decimal](18, 6) NOT NULL,	[Dis_Amount] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[LN_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[Tbl_Amount] [decimal](18, 6) NOT NULL,	[Action_type] [nvarchar](50) NOT NULL,	[No_Print] [decimal](18, 0) NULL,	[In_Words] [nvarchar](1024) NULL,	[Remarks] [nvarchar](500) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[CancelBy] [nvarchar](50) NULL,	[CancelDate] [nvarchar](50) NULL,	[CancelRemarks] [nvarchar](1024) NULL,	[CUnit_Id] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[R_Invoice] [bit] NULL,	[Is_Printed] [bit] NULL,	[Printed_By] [nvarchar](50) NULL,	[Printed_Date] [datetime] NULL,	[Cleared_By] [nvarchar](50) NULL,	[Cleared_Date] [datetime] NULL,	[Cancel_By] [nvarchar](50) NULL,	[Cancel_Date] [datetime] NULL,	[Cancel_Remarks] [nvarchar](1024) NULL,	[IsAPIPosted] [bit] NULL,	[IsRealtime] [bit] NULL, CONSTRAINT [PK_PBT] PRIMARY KEY CLUSTERED (	[PBT_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[PBT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PBT_Term]([PBT_VNo] [nvarchar](50) NOT NULL,	[PT_Id] [int] NOT NULL,	[SNo] [int] NOT NULL,	[Rate] [decimal](18, 6) NULL,	[Amount] [decimal](18, 6) NULL,	[Term_Type] [char](2) NOT NULL,	[Product_Id] [bigint] NOT NULL,	[Taxable] [char](1) NULL, CONSTRAINT [PK_PBT_Term] PRIMARY KEY CLUSTERED (	[PBT_VNo] ASC,	[PT_Id] ASC,	[SNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PC_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PC_Details](
	[PC_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NOT NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[AltStock_Qty] [decimal](18, 6) NOT NULL,
	[Stock_Qty] [decimal](18, 6) NOT NULL,
	[Narration] [nvarchar](1024) NOT NULL,
	[PO_Invoice] [nvarchar](50) NOT NULL,
	[PO_Sno] [int] NOT NULL,
	[QOT_Invoice] [nvarchar](50) NULL,
	[QOT_SNo] [nvarchar](50) NULL,
	[Tax_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[V_Rate] [decimal](18, 6) NOT NULL,
	[Issue_Qty] [decimal](18, 6) NOT NULL,
	[Free_Unit_Id] [int] NOT NULL,
	[Free_Qty] [decimal](18, 6) NOT NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [int] NULL,
	[ExtraFree_Qty] [decimal](18, 6) NULL,
	[ExtraStockFree_Qty] [decimal](18, 6) NULL,
	[T_Product] [bit] NULL,
	[P_Ledger] [bigint] NULL,
	[PR_Ledger] [bigint] NULL,
	[SZ1] [nvarchar](50) NULL,
	[SZ2] [nvarchar](50) NULL,
	[SZ3] [nvarchar](50) NULL,
	[SZ4] [nvarchar](50) NULL,
	[SZ5] [nvarchar](50) NULL,
	[SZ6] [nvarchar](50) NULL,
	[SZ7] [nvarchar](50) NULL,
	[SZ8] [nvarchar](50) NULL,
	[SZ9] [nvarchar](50) NULL,
	[SZ10] [nvarchar](50) NULL,	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,	[Exp_Date] [datetime] NULL,	[Manu_Date] [datetime] NULL, CONSTRAINT [PK_PC_Details] PRIMARY KEY CLUSTERED (	[PC_Invoice] ASC,	[Invoice_SNo] ASC,	[P_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[PC_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PC_Master](	[PC_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NOT NULL,	[PB_Vno] [nvarchar](50) NULL,	[Vno_Date] [datetime] NULL,	[Vno_Miti] [nvarchar](10) NULL,	[Vendor_ID] [bigint] NOT NULL,	[Party_Name] [nvarchar](100) NULL,	[Vat_No] [nvarchar](50) NULL,	[Contact_Person] [nvarchar](50) NULL,	[Mobile_No] [nvarchar](50) NULL,	[Address] [nvarchar](90) NULL,	[ChqNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[Invoice_Type] [nvarchar](50) NOT NULL,	[Invoice_In] [nvarchar](50) NOT NULL,	[DueDays] [int] NULL,	[DueDate] [datetime] NULL,	[Agent_Id] [int] NULL,	[Subledger_Id] [int] NULL,	[PO_Invoice] [nvarchar](250) NULL,	[PO_Date] [datetime] NULL,	[QOT_Invoice] [nvarchar](250) NULL,	[QOT_Date] [datetime] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Cur_Id] [int] NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[Counter_ID] [int] NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[LN_Amount] [decimal](18, 6) NOT NULL,	[Tender_Amount] [decimal](18, 6) NOT NULL,	[Change_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[Tbl_Amount] [decimal](18, 6) NOT NULL,	[Action_type] [nvarchar](50) NULL,	[R_Invoice] [bit] NULL,	[No_Print] [int] NOT NULL,	[In_Words] [nvarchar](1024) NULL,	[Remarks] [nvarchar](1024) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NULL,	[Enter_Date] [datetime] NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[CUnit_Id] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[FiscalYearId] [int] NOT NULL,	[PAttachment1] [image] NULL,	[PAttachment2] [image] NULL,	[PAttachment3] [image] NULL,	[PAttachment4] [image] NULL,	[PAttachment5] [image] NULL, CONSTRAINT [PK_PC] PRIMARY KEY CLUSTERED (	[PC_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [AMS].[PC_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PC_Term]([PC_VNo] [nvarchar](50) NOT NULL,	[PT_Id] [int] NOT NULL,	[SNo] [int] NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Term_Type] [char](2) NULL,	[Product_Id] [bigint] NULL,	[Taxable] [char](1) NULL, CONSTRAINT [PK_PC_Term] PRIMARY KEY CLUSTERED (	[PC_VNo] ASC,	[PT_Id] ASC,	[SNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PIN_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PIN_Details]([PIN_Invoice] [nvarchar](50) NOT NULL,	[SNo] [int] NOT NULL,	[P_Id] [bigint] NOT NULL,	[Gdn_Id] [int] NULL,	[Alt_Qty] [decimal](18, 6) NOT NULL,	[Alt_Unit] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit] [int] NULL,	[AltStock_Qty] [decimal](18, 6) NOT NULL,	[StockQty] [decimal](18, 6) NOT NULL,	[Issue_Qty] [decimal](18, 6) NOT NULL,	[Narration] [varchar](1024) NULL, CONSTRAINT [PK_PIN_Details] PRIMARY KEY CLUSTERED (	[PIN_Invoice] ASC,	[SNo] ASC,	[P_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PIN_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PIN_Master]([PIN_Invoice] [nvarchar](50) NOT NULL,	[PIN_Date] [datetime] NOT NULL,	[PIN_Miti] [nvarchar](50) NOT NULL,	[Person] [nvarchar](50) NOT NULL,	[Sub_Ledger] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[EnterBY] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[ActionType] [char](10) NULL,	[Remarks] [nvarchar](1024) NULL,	[Print_value] [int] NULL,	[CancelBy] [nvarchar](50) NULL,	[CancelDate] [datetime] NULL,	[CancelRemarks] [nvarchar](1024) NULL,	[FiscalYearId] [int] NOT NULL,	[BranchId] [int] NOT NULL,	[CompanyUnitId] [int] NULL, CONSTRAINT [PK_PIN_Master] PRIMARY KEY CLUSTERED (	[PIN_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PO_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PO_Details]([PO_Invoice] [nvarchar](50) NOT NULL,	[Invoice_SNo] [numeric](18, 0) NOT NULL,	[P_Id] [bigint] NOT NULL,	[Gdn_Id] [int] NULL,	[Alt_Qty] [decimal](18, 6) NOT NULL,	[Alt_UnitId] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit_Id] [int] NULL,	[Rate] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[AltStock_Qty] [decimal](18, 6) NOT NULL,	[Stock_Qty] [decimal](18, 6) NOT NULL,	[Narration] [nvarchar](1024) NULL,	[PIN_Invoice] [nvarchar](50) NULL,	[PIN_Sno] [int] NULL,	[PQT_Invoice] [nvarchar](50) NULL,	[PQT_SNo] [nvarchar](50) NULL,	[Tax_Amount] [decimal](18, 6) NULL,	[V_Amount] [decimal](18, 6) NULL,	[V_Rate] [decimal](18, 6) NULL,	[Issue_Qty] [decimal](18, 6) NULL,	[Free_Unit_Id] [int] NULL,	[Free_Qty] [decimal](18, 6) NULL,	[StockFree_Qty] [decimal](18, 6) NULL,	[ExtraFree_Unit_Id] [int] NULL,	[ExtraFree_Qty] [decimal](18, 6) NULL,	[ExtraStockFree_Qty] [decimal](18, 6) NULL,	[T_Product] [bit] NULL,	[P_Ledger] [bigint] NULL,	[PR_Ledger] [bigint] NULL,	[SZ1] [nvarchar](50) NULL,	[SZ2] [nvarchar](50) NULL,	[SZ3] [nvarchar](50) NULL,	[SZ4] [nvarchar](50) NULL,	[SZ5] [nvarchar](50) NULL,	[SZ6] [nvarchar](50) NULL,	[SZ7] [nvarchar](50) NULL,	[SZ8] [nvarchar](50) NULL,	[SZ9] [nvarchar](50) NULL,	[SZ10] [nvarchar](50) NULL,	[Serial_No] [nvarchar](50) NULL,	[Batch_No] [nvarchar](50) NULL,	[Exp_Date] [datetime] NULL,	[Manu_Date] [datetime] NULL, CONSTRAINT [PK_PO_Details] PRIMARY KEY CLUSTERED (	[PO_Invoice] ASC,	[Invoice_SNo] ASC,	[P_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[PO_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PO_Master](	[PO_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NOT NULL,	[PB_Vno] [nvarchar](50) NULL,	[Vno_Date] [datetime] NULL,	[Vno_Miti] [nvarchar](10) NULL,	[Vendor_ID] [bigint] NOT NULL,	[Party_Name] [nvarchar](100) NULL,	[Vat_No] [nvarchar](50) NULL,	[Contact_Person] [nvarchar](50) NULL,	[Mobile_No] [nvarchar](50) NULL,	[Address] [nvarchar](90) NULL,	[ChqNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[Invoice_Type] [nvarchar](50) NOT NULL,	[Invoice_In] [nvarchar](50) NOT NULL,	[DueDays] [int] NULL,	[DueDate] [datetime] NULL,	[Agent_Id] [int] NULL,	[Subledger_Id] [int] NULL,	[PIN_Invoice] [nvarchar](250) NULL,	[PIN_Date] [datetime] NULL,	[PQT_Invoice] [nvarchar](250) NULL,	[PQT_Date] [datetime] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Cur_Id] [int] NOT NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[LN_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[Tbl_Amount] [decimal](18, 6) NOT NULL,	[Action_type] [nvarchar](50) NOT NULL,	[R_Invoice] [bit] NULL,	[No_Print] [decimal](18, 0) NULL,	[In_Words] [nvarchar](1024) NULL,	[Remarks] [nvarchar](500) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[CUnit_Id] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[FiscalYearId] [int] NOT NULL,	[PAttachment1] [image] NULL,	[PAttachment2] [image] NULL,	[PAttachment3] [image] NULL,	[PAttachment4] [image] NULL,	[PAttachment5] [image] NULL, CONSTRAINT [PK_PO] PRIMARY KEY CLUSTERED (	[PO_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


/****** Object:  Table [AMS].[PO_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PO_Term]([PO_VNo] [nvarchar](50) NOT NULL,	[PT_Id] [int] NOT NULL,	[SNo] [int] NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Term_Type] [char](2) NULL,	[Product_Id] [bigint] NULL,	[Taxable] [char](1) NULL, CONSTRAINT [PK_PO_Term] PRIMARY KEY CLUSTERED (	[PO_VNo] ASC,	[PT_Id] ASC,	[SNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PostDateCheque]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PostDateCheque]([PDCId] [int] IDENTITY(1,1) NOT NULL,	[VoucherNo] [nvarchar](50) NOT NULL,	[VoucherDate] [datetime] NOT NULL,	[VoucherMiti] [nvarchar](50) NOT NULL,	[VoucherType] [nvarchar](50) NOT NULL,	[BankName] [nvarchar](500) NOT NULL,	[BranchName] [nvarchar](500) NOT NULL,	[ChequeNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[ChqMiti] [nvarchar](50) NULL,	[DrawOn] [nvarchar](500) NULL,	[Amount] [decimal](18, 6) NOT NULL,	[LedgerId] [bigint] NOT NULL,	[SubLedgerId] [int] NULL,	[AgentId] [int] NULL,	[Remarks] [nvarchar](1024) NULL,	[DepositedBy] [nvarchar](50) NULL,	[DepositeDate] [datetime] NULL,	[CancelReason] [nvarchar](1024) NULL,	[CancelBy] [nvarchar](50) NULL,	[CancelDate] [datetime] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[CompanyUnitId] [int] NULL,	[BranchId] [int] NOT NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[FiscalYearId] [int] NOT NULL,	[VoucherTime] [date] NULL,	[BankLedgerId] [bigint] NULL,	[Status] [nvarchar](50) NULL,	[PAttachment1] [image] NULL,	[PAttachment2] [image] NULL,	[PAttachment3] [image] NULL,	[PAttachment4] [image] NULL,	[PAttachment5] [image] NULL, CONSTRAINT [PK__PDC__A96E03572C1E8537] PRIMARY KEY CLUSTERED (	[PDCId] ASC,	[VoucherNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [UQ__PDC__C5DCEA062EFAF1E2] UNIQUE NONCLUSTERED (	[VoucherNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ; 


/****** Object:  Table [AMS].[PR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[PR_Details]([PR_Invoice] [nvarchar](50) NOT NULL,	[Invoice_SNo] [int] NOT NULL,	[P_Id] [bigint] NOT NULL,	[Gdn_Id] [int] NULL,	[Alt_Qty] [decimal](18, 6) NOT NULL,	[Alt_UnitId] [int] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit_Id] [int] NULL,	[Rate] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[AltStock_Qty] [decimal](18, 6) NOT NULL,	[Stock_Qty] [decimal](18, 6) NOT NULL,	[Narration] [nvarchar](1024) NULL,	[PB_Invoice] [nvarchar](50) NULL,	[PB_Sno] [int] NULL,	[Tax_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NOT NULL,	[V_Rate] [decimal](18, 6) NOT NULL,	[Free_Unit_Id] [int] NULL,	[Free_Qty] [decimal](18, 6) NULL,	[StockFree_Qty] [decimal](18, 6) NULL,	[ExtraFree_Unit_Id] [int] NULL,	[ExtraFree_Qty] [decimal](18, 6) NULL,	[ExtraStockFree_Qty] [decimal](18, 6) NULL,	[T_Product] [bit] NULL,	[P_Ledger] [bigint] NULL,	[PR_Ledger] [bigint] NULL,	[SZ1] [nvarchar](50) NULL,	[SZ2] [nvarchar](50) NULL,	[SZ3] [nvarchar](50) NULL,	[SZ4] [nvarchar](50) NULL,	[SZ5] [nvarchar](50) NULL,	[SZ6] [nvarchar](50) NULL,	[SZ7] [nvarchar](50) NULL,	[SZ8] [nvarchar](50) NULL,	[SZ9] [nvarchar](50) NULL,	[SZ10] [nvarchar](50) NULL,	[Serial_No] [nvarchar](50) NULL,	[Batch_No] [nvarchar](50) NULL,	[Exp_Date] [datetime] NULL,	[Manu_Date] [datetime] NULL, CONSTRAINT [PK_PR_Details] PRIMARY KEY CLUSTERED (	[PR_Invoice] ASC,	[Invoice_SNo] ASC,	[P_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[PR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PR_Master]([PR_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NOT NULL,	[PB_Invoice] [nvarchar](50) NULL,	[PB_Date] [datetime] NULL,	[PB_Miti] [varchar](10) NULL,	[Vendor_ID] [bigint] NOT NULL,	[Party_Name] [nvarchar](100) NULL,	[Vat_No] [nvarchar](50) NULL,	[Contact_Person] [nvarchar](50) NULL,	[Mobile_No] [nvarchar](50) NULL,	[Address] [nvarchar](90) NULL,	[ChqNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[Invoice_Type] [nvarchar](50) NULL,	[Invoice_In] [nvarchar](50) NULL,	[DueDays] [int] NULL,	[DueDate] [datetime] NULL,	[Agent_Id] [int] NULL,	[Subledger_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Cur_Id] [int] NOT NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[LN_Amount] [decimal](18, 6) NOT NULL,	[Tender_Amount] [decimal](18, 6) NULL,	[Change_Amount] [decimal](18, 6) NULL,	[V_Amount] [decimal](18, 6) NULL,	[Tbl_Amount] [decimal](18, 6) NULL,	[Action_type] [char](10) NOT NULL,	[No_Print] [decimal](18, 0) NULL,	[In_Words] [nvarchar](1024) NULL,	[Remarks] [nvarchar](1024) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[Cleared_By] [nvarchar](50) NULL,	[Cleared_Date] [datetime] NULL,	[CancelBy] [nvarchar](50) NULL,	[CancelDate] [datetime] NULL,	[CancelRemarks] [nvarchar](1024) NULL,	[CUnit_Id] [int] NULL,	[CBranch_Id] [int] NOT NULL,	[FiscalYearId] [int] NULL, CONSTRAINT [PK_PR] PRIMARY KEY CLUSTERED (	[PR_Invoice] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PR_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PR_Term]([PR_VNo] [nvarchar](50) NOT NULL,	[PT_Id] [int] NOT NULL,	[SNo] [int] NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Term_Type] [char](2) NULL,	[Product_Id] [bigint] NULL,	[Taxable] [char](1) NULL, CONSTRAINT [PK_PR_Term_1] PRIMARY KEY CLUSTERED (	[PR_VNo] ASC,	[PT_Id] ASC,	[SNo] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Product]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Product]([PID] [bigint] NOT NULL,	[PName] [nvarchar](200) NOT NULL,	[PAlias] [nvarchar](200) NOT NULL,	[PShortName] [nvarchar](50) NOT NULL,	[PType] [nvarchar](50) NOT NULL,	[PCatery] [nvarchar](50) NOT NULL,	[PUnit] [int] NULL,	[PAltUnit] [int] NULL,	[PQtyConv] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PQtyConv]  DEFAULT ((0)),	[PAltConv] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PAltConv]  DEFAULT ((0)),	[PValTech] [nvarchar](50) NULL,	[PSerialno] [bit] NULL CONSTRAINT [DF_Product_PSerialno]  DEFAULT ((0)),	[PSizewise] [bit] NULL CONSTRAINT [DF_Product_PSizewise]  DEFAULT ((0)),	[PBatchwise] [bit] NULL CONSTRAINT [DF_Product_PBatchwise]  DEFAULT ((0)),	[PBuyRate] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PBuyRate]  DEFAULT ((0)),	[PSalesRate] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PSalesRate]  DEFAULT ((0)),	[PMargin1] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMargin1]  DEFAULT ((0)),	[TradeRate] [decimal](18, 6) NULL CONSTRAINT [DF_Product_TradeRate]  DEFAULT ((0)),	[PMargin2] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMargin2]  DEFAULT ((0)),	[PMRP] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMRP]  DEFAULT ((0)),	[PGrpId] [int] NULL,	[PSubGrpId] [int] NULL,	[PTax] [decimal](18, 6) NOT NULL CONSTRAINT [DF_Product_PTax]  DEFAULT ((0)),	[PMin] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMin]  DEFAULT ((0.00)),	[PMax] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMax]  DEFAULT ((0)),	[CmpId] [int] NULL,	[CmpId1] [int] NULL,	[CmpId2] [int] NULL,	[CmpId3] [int] NULL,	[Branch_Id] [int] NOT NULL,	[CmpUnit_Id] [int] NULL,	[PPL] [bigint] NULL,	[PPR] [bigint] NULL,	[PSL] [bigint] NULL,	[PSR] [bigint] NULL,	[PL_Opening] [bigint] NULL,	[PL_Closing] [bigint] NULL,	[BS_Closing] [bigint] NULL,	[PImage] [image] NULL,	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_Product_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_Product_Status]  DEFAULT ((1)),	[BeforeBuyRate] [decimal](18, 6) NULL,	[BeforeSalesRate] [decimal](18, 6) NULL,	[Barcode] [nvarchar](100) NULL,	[ChasisNo] [nvarchar](100) NULL,	[EngineNo] [nvarchar](100) NULL,	[VHColor] [nvarchar](100) NULL,	[VHModel] [nvarchar](100) NULL,	[VHNumber] [nvarchar](100) NULL,	[Barcode1] [nvarchar](100) NULL,	[Barcode2] [nvarchar](100) NULL,	[Barcode3] [nvarchar](100) NULL, CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED (	[PID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductDesc] UNIQUE NONCLUSTERED (	[PName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductShortName] UNIQUE NONCLUSTERED (	[PShortName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] ; 


/****** Object:  Table [AMS].[ProductClosingRate]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[ProductClosingRate]([PCRate_Id] [bigint] IDENTITY(1,1) NOT NULL,	[Product_Id] [bigint] NOT NULL,	[Rate] [decimal](18, 6) NOT NULL,	[Date_Type] [varchar](5) NULL,	[Month_Date] [datetime] NULL,	[Month_Miti] [varchar](50) NULL,	[CBranch_Id] [int] NOT NULL,	[CUnit_Id] [int] NOT NULL, CONSTRAINT [PK_ProductClosingRate] PRIMARY KEY CLUSTERED (	[PCRate_Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[ProductGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[ProductGroup]([PGrpID] [int] NOT NULL,	[GrpName] [nvarchar](200) NOT NULL,	[GrpCode] [nvarchar](50) NOT NULL,	[GMargin] [decimal](18, 6) NULL CONSTRAINT [DF_ProductGroup_GMargin]  DEFAULT ((0.00)),	[Gprinter] [nvarchar](50) NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_ProductGroup_Status]  DEFAULT ((1)),	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_ProductGroup_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL, CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED (	[PGrpID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductGroupDesc] UNIQUE NONCLUSTERED (	[GrpName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductGroupShortName] UNIQUE NONCLUSTERED (	[GrpCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[ProductOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[ProductOpening]([Voucher_No] [nvarchar](50) NOT NULL,	[Serial_No] [int] NOT NULL,	[OP_Date] [datetime] NOT NULL,	[OP_Miti] [varchar](10) NOT NULL,	[Product_Id] [bigint] NOT NULL,	[down_Id] [int] NULL,	[Cls1] [int] NULL,	[Cls2] [int] NULL,	[Cls3] [int] NULL,	[Cls4] [int] NULL,	[Currency_Id] [int] NULL,	[Currency_Rate] [decimal](18, 6) NULL,	[AltQty] [decimal](18, 6) NULL,	[AltUnit] [decimal](18, 6) NULL,	[Qty] [decimal](18, 6) NOT NULL,	[QtyUnit] [int] NULL,	[Rate] [decimal](18, 6) NOT NULL,	[LocalRate] [decimal](18, 6) NOT NULL,	[Amount] [decimal](18, 6) NOT NULL,	[LocalAmount] [decimal](18, 6) NOT NULL,	[Enter_By] [nvarchar](50) NOT NULL,	[Enter_Date] [datetime] NOT NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [nvarchar](50) NULL,	[CBranch_Id] [int] NOT NULL,	[CUnit_Id] [int] NULL,	[FiscalYearId] [int] NOT NULL) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[ProductSubGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[ProductSubGroup]([PSubGrpId] [int] NOT NULL,	[SubGrpName] [nvarchar](80) NOT NULL,	[ShortName] [nvarchar](50) NOT NULL,	[GrpId] [int] NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[EnterBy] [nvarchar](50) NOT NULL,	[EnterDate] [datetime] NOT NULL,	[Status] [bit] NOT NULL, CONSTRAINT [PK_ProductSubGroup] PRIMARY KEY CLUSTERED (	[PSubGrpId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductSubGroupDesc] UNIQUE NONCLUSTERED (	[SubGrpName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductSubGroupShortName] UNIQUE NONCLUSTERED (	[ShortName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]


/****** Object:  Table [AMS].[ProductUnit]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[ProductUnit]([UID] [int] NOT NULL,	[UnitName] [nvarchar](50) NOT NULL,	[UnitCode] [nvarchar](50) NOT NULL,	[Branch_ID] [int] NOT NULL,	[Company_ID] [int] NULL,	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_ProductUnit_EnterBy]  DEFAULT (N'MrSolution'),	[EnterDate] [datetime] NOT NULL,	[Status] [bit] NOT NULL CONSTRAINT [DF_ProductUnit_Status]  DEFAULT ((1)), CONSTRAINT [PK_ProductUnit] PRIMARY KEY CLUSTERED (	[UID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductUnitDesc] UNIQUE NONCLUSTERED (	[UnitName] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY], CONSTRAINT [IX_ProductUnitShortName] UNIQUE NONCLUSTERED (	[UnitCode] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ; 


/****** Object:  Table [AMS].[PROV_CB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PROV_CB_Master](
	[VoucherMode] [char](10) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](10) NOT NULL,
	[Voucher_Time] [datetime] NOT NULL,
	[Ref_VNo] [nvarchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[VoucherType] [nvarchar](50) NOT NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[CheqNo] [nvarchar](50) NULL,
	[CheqDate] [datetime] NULL,
	[CheqMiti] [nvarchar](10) NULL,
	[Currency_Id] [int] NOT NULL,
	[Currency_Rate] [money] NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Action_Type] [nvarchar](50) NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[ClearingBy] [nvarchar](50) NULL,
	[ClearingDate] [datetime] NULL,
	[PrintValue] [int] NULL,
	[IsPosted] [bit] NULL,
	[PostedBy] [nvarchar](50) NULL,
	[PostedDate] [datetime] NULL,
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_PROV_CB_Master] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[ProvCB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[ProvCB_Details](
	[VoucherNumber] [nvarchar](50) NOT NULL,
	[Sno] [int] NOT NULL,
	[LedgerId] [bigint] NOT NULL,
	[SubledgerId] [int] NULL,
	[AgentId] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[ReceiptAmt] [decimal](18, 6) NOT NULL,
	[PaymentAmt] [decimal](18, 6) NOT NULL,
	[Naration] [varchar](1024) NULL,
 CONSTRAINT [PK_ProvCB_Details] PRIMARY KEY CLUSTERED 
(
	[VoucherNumber] ASC,
	[Sno] ASC,
	[LedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[ProvCB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[ProvCB_Master](
	[VoucherNumber] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](10) NOT NULL,
	[VoucherTime] [datetime] NOT NULL,
	[Cheque_No] [nvarchar](50) NULL,
	[Cheque_Date] [datetime] NULL,
	[CurrId] [int] NOT NULL,
	[CurrRate] [decimal](18, 6) NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[LedgerId] [bigint] NOT NULL,
	[DrawnOn] [nvarchar](50) NULL,
	[BankName] [nvarchar](50) NULL,
	[BranchName] [nvarchar](50) NULL,
	[Cheque_Miti] [nvarchar](10) NULL,
	[Deposited] [nvarchar](50) NULL,
	[DepositedBy] [nvarchar](50) NULL,
	[DepositedDate] [datetime] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Posting] [char](1) NULL,
	[Remarks] [varchar](1024) NULL,
	[Cancel_Remarks] [varchar](1024) NULL,
	[Status] [varchar](10) NULL,
	[print_Value] [int] NULL,
	[ActionDate] [datetime] NULL,
	[ActionTime] [datetime] NULL,
	[ActionUser] [varchar](20) NULL,
	[PresentDate] [datetime] NULL,
	[CompanyUnit] [int] NULL,
	[BranchId] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK__ProvCash__B81B3E4A2759D01A] PRIMARY KEY CLUSTERED 
(
	[VoucherNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PRT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PRT_Term](
	[PRT_ID] [int] NOT NULL,
	[Order_No] [int] NOT NULL,
	[Module] [char](2) NOT NULL,
	[PT_Name] [nvarchar](50) NOT NULL,
	[PT_Type] [char](2) NOT NULL,
	[Ledger] [bigint] NOT NULL,
	[PT_Basis] [char](2) NOT NULL,
	[PT_Sign] [char](1) NOT NULL,
	[PT_Condition] [char](1) NOT NULL,
	[PT_Rate] [decimal](18, 6) NULL,
	[PT_Branch] [int] NOT NULL,
	[PT_CompanyUnit] [int] NULL,
	[PT_Profitability] [bit] NULL,
	[PT_Supess] [bit] NULL,
	[PT_Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PR_Term] PRIMARY KEY CLUSTERED 
(
	[PRT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PR_Term] UNIQUE NONCLUSTERED 
(
	[Order_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PT_Term](
	[PT_ID] [int] NOT NULL,
	[Order_No] [int] NOT NULL,
	[Module] [char](4) NOT NULL,
	[PT_Name] [nvarchar](50) NOT NULL,
	[PT_Type] [char](2) NOT NULL,
	[Ledger] [bigint] NOT NULL,
	[PT_Basis] [char](2) NOT NULL,
	[PT_Sign] [char](1) NOT NULL,
	[PT_Condition] [char](1) NOT NULL,
	[PT_Rate] [decimal](18, 6) NULL,
	[PT_Branch] [int] NOT NULL,
	[PT_CompanyUnit] [int] NULL,
	[PT_Profitability] [bit] NOT NULL,
	[PT_Supess] [bit] NOT NULL,
	[PT_Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PT_TermId] PRIMARY KEY CLUSTERED 
(
	[PT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PT_TermDesc] UNIQUE NONCLUSTERED 
(
	[PT_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PT_TermOrderNo] UNIQUE NONCLUSTERED 
(
	[Order_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[PurchaseSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[PurchaseSetting](
	[PurId] [tinyint] NOT NULL,
	[PBLedgerId] [bigint] NULL,
	[PRLedgerId] [bigint] NULL,
	[PBVatTerm] [int] NULL,
	[PBDiscountTerm] [int] NULL,
	[PBProductDiscountTerm] [int] NULL,
	[PBAdditionalTerm] [int] NULL,
	[PBDateChange] [bit] NULL,
	[PBCreditDays] [char](1) NULL,
	[PBCreditLimit] [char](1) NULL,
	[PBCarryRate] [bit] NULL,
	[PBChangeRate] [bit] NULL,
	[PBLastRate] [bit] NULL,
	[POEnable] [bit] NULL,
	[POMandetory] [bit] NULL,
	[PCEnable] [bit] NULL,
	[PCMandetory] [bit] NULL,
	[PBSublegerEnable] [bit] NULL,
	[PBSubledgerMandetory] [bit] NULL,
	[PBAgentEnable] [bit] NULL,
	[PBAgentMandetory] [bit] NULL,
	[PBDepartmentEnable] [bit] NULL,
	[PBDepartmentMandetory] [bit] NULL,
	[PBCurrencyEnable] [bit] NULL,
	[PBCurrencyMandetory] [bit] NULL,
	[PBCurrencyRateChange] [bit] NULL,
	[PBdownEnable] [bit] NULL,
	[PBdownMandetory] [bit] NULL,
	[PBAlternetUnitEnable] [bit] NULL,
	[PBIndent] [bit] NULL,
	[PBNarration] [bit] NULL,
	[PBBasicAmount] [bit] NULL,
 CONSTRAINT [PK_PurchaseSetting] PRIMARY KEY CLUSTERED 
(
	[PurId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[ReportTemplate]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[ReportTemplate](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[FullPath] [nvarchar](max) NOT NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[Reports_Type] [char](1) NULL,
	[Report_Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReportTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SalesSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING OFF

CREATE TABLE [AMS].[SalesSetting](
	[SalesId] [tinyint] NOT NULL,
	[SBLedgerId] [bigint] NULL,
	[SRLedgerId] [bigint] NULL,
	[SBVatTerm] [int] NULL,
	[SBDiscountTerm] [int] NULL,
	[SBProductDiscountTerm] [int] NULL,
	[SBAdditionalTerm] [int] NULL,
	[SBServiceCharge] [int] NULL,
	[SBDateChange] [bit] NULL,
	[SBCreditDays] [char](1) NULL,
	[SBCreditLimit] [char](1) NULL,
	[SBCarryRate] [bit] NULL,
	[SBChangeRate] [bit] NULL,
	[SBLastRate] [bit] NULL,
	[SBQuotationEnable] [bit] NULL,
	[SBQuotationMandetory] [bit] NULL,
	[SBDispatchOrderEnable] [bit] NULL,
	[SBDispatchMandetory] [bit] NULL,
	[SOEnable] [bit] NULL,
	[SOMandetory] [bit] NULL,
	[SCEnable] [bit] NULL,
	[SCMandetory] [bit] NULL,
	[SBSublegerEnable] [bit] NULL,
	[SBSubledgerMandetory] [bit] NULL,
	[SBAgentEnable] [bit] NULL,
	[SBAgentMandetory] [bit] NULL,
	[SBDepartmentEnable] [bit] NULL,
	[SBDepartmentMandetory] [bit] NULL,
	[SBCurrencyEnable] [bit] NULL,
	[SBCurrencyMandetory] [bit] NULL,
	[SBCurrencyRateChange] [bit] NULL,
	[SBdownEnable] [bit] NULL,
	[SBdownMandetory] [bit] NULL,
	[SBAlternetUnitEnable] [bit] NULL,
	[SBIndent] [bit] NULL,
	[SBNarration] [bit] NULL,
	[SBBasicAmount] [bit] NULL,
	[SBAviableStock] [bit] NULL,
	[SBReturnValue] [bit] NULL,
	[PartyInfo] [bit] NULL,
 CONSTRAINT [PK_SalesSetting] PRIMARY KEY CLUSTERED 
(
	[SalesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SampleCosting_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SampleCosting_Details](
	[Costing_No] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[CC_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[AltUnit_Id] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Narration] [nvarchar](1024) NULL,
	[CCExpenses_No] [nvarchar](50) NULL,
	[CCExpenses_SNo] [int] NULL,
 CONSTRAINT [PK_SampleCosting_Details] PRIMARY KEY CLUSTERED 
(
	[Costing_No] ASC,
	[SNo] ASC,
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SampleCosting_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SampleCosting_Master](
	[Costing_No] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[CCExpenses_No] [nvarchar](50) NULL,
	[CCExpenses_Date] [datetime] NULL,
	[FGProduct_Id] [bigint] NOT NULL,
	[Cost_Rate] [decimal](18, 6) NULL,
	[Gdn_Id] [int] NULL,
	[CC_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Total_Qty] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_SampleCosting_Master] PRIMARY KEY CLUSTERED 
(
	[Costing_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SB_Details](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](500) NULL,
	[SO_Invoice] [nvarchar](50) NULL,
	[SO_Sno] [numeric](18, 0) NULL,
	[SC_Invoice] [nvarchar](50) NULL,
	[SC_SNo] [nvarchar](50) NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[V_Rate] [decimal](18, 6) NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [int] NULL,
	[ExtraFree_Qty] [decimal](18, 6) NULL,
	[ExtraStockFree_Qty] [decimal](18, 6) NULL,
	[T_Product] [bit] NULL,
	[S_Ledger] [bigint] NULL,
	[SR_Ledger] [bigint] NULL,
	[SZ1] [nvarchar](50) NULL,
	[SZ2] [nvarchar](50) NULL,
	[SZ3] [nvarchar](50) NULL,
	[SZ4] [nvarchar](50) NULL,
	[SZ5] [nvarchar](50) NULL,
	[SZ6] [nvarchar](50) NULL,
	[SZ7] [nvarchar](50) NULL,
	[SZ8] [nvarchar](50) NULL,
	[SZ9] [nvarchar](50) NULL,
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
	[PDiscountRate] [decimal](18, 6) NULL,
	[PDiscount] [decimal](18, 6) NULL,
	[BDiscountRate] [decimal](18, 6) NULL,
	[BDiscount] [decimal](18, 6) NULL,
	[ServiceChargeRate] [decimal](18, 6) NULL,
	[ServiceCharge] [decimal](18, 6) NULL,
	[MaterialPost] [char](1) NULL,
 CONSTRAINT [PK_SB_Details] PRIMARY KEY CLUSTERED 
(
	[SB_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SB_ExchangeDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SB_ExchangeDetails](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NULL,
	[P_Id] [bigint] NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[ExchangeGLD] [bigint] NULL
) ON [PRIMARY]


/****** Object:  Table [AMS].[SB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SB_Master](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](50) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[PB_Vno] [nvarchar](50) NULL,
	[Vno_Date] [datetime] NULL,
	[Vno_Miti] [varchar](10) NULL,
	[Customer_Id] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NOT NULL,
	[Invoice_Mode] [nvarchar](50) NOT NULL,
	[Payment_Mode] [varchar](50) NOT NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[SO_Invoice] [nvarchar](250) NULL,
	[SO_Date] [datetime] NULL,
	[SC_Invoice] [nvarchar](250) NULL,
	[SC_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CounterId] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Tender_Amount] [decimal](18, 6) NULL,
	[Return_Amount] [decimal](18, 6) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](500) NULL,
	[R_Invoice] [bit] NULL,
	[Is_Printed] [bit] NULL,
	[No_Print] [int] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[Cancel_By] [nvarchar](50) NULL,
	[Cancel_Date] [datetime] NULL,
	[Cancel_Remarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
	[FiscalYearId] [int] NOT NULL,
	[DoctorId] [int] NULL,
	[PatientId] [bigint] NULL,
	[HDepartmentId] [int] NULL,
	[TableId] [int] NULL,
	[MShipId] [int] NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_SB] PRIMARY KEY CLUSTERED 
(
	[SB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SB_Master_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SB_Master_OtherDetails](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Transport] [nvarchar](255) NULL,
	[VechileNo] [nvarchar](50) NULL,
	[BiltyNo] [nvarchar](50) NULL,
	[Package] [nvarchar](100) NULL,
	[BiltyDate] [date] NULL,
	[BiltyType] [nvarchar](50) NULL,
	[Driver] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LicenseNo] [nvarchar](50) NULL,
	[MailingAddress] [nvarchar](500) NULL,
	[MCity] [nvarchar](50) NULL,
	[MState] [nvarchar](50) NULL,
	[MCountry] [nvarchar](50) NULL,
	[MEmail] [nvarchar](50) NULL,
	[ShippingAddress] [nvarchar](500) NULL,
	[SCity] [nvarchar](50) NULL,
	[SState] [nvarchar](50) NULL,
	[SCountry] [nvarchar](50) NULL,
	[SEmail] [nvarchar](50) NULL,
	[ContractNo] [nvarchar](50) NULL,
	[ContractDate] [date] NULL,
	[ExportInvoice] [nvarchar](50) NULL,
	[ExportInvoiceDate] [date] NULL,
	[VendorOrderNo] [nvarchar](50) NULL,
	[BankDetails] [nvarchar](100) NULL,
	[LcNumber] [nvarchar](50) NULL,
	[CustomDetails] [nvarchar](100) NULL,
 CONSTRAINT [PK_SB_Master_OtherDetails] PRIMARY KEY CLUSTERED 
(
	[SB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SB_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SB_Term](
	[SB_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NOT NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SBT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SBT_Details](
	[SBT_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [int] NOT NULL,
	[Slb_Id] [int] NOT NULL,
	[PGrp_Id] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Flight_Date] [datetime] NULL,
	[Sector] [nvarchar](100) NULL,
	[Fare_Amount] [decimal](18, 6) NOT NULL,
	[FC_Amount] [decimal](18, 6) NOT NULL,
	[PSC_Amount] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[Trm_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[Narration] [nvarchar](500) NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[VAT_Amount] [decimal](18, 6) NULL,
	[VAT_Rate] [decimal](18, 6) NULL,
 CONSTRAINT [PK_SBT_Details] PRIMARY KEY CLUSTERED 
(
	[SBT_Invoice] ASC,
	[Invoice_SNo] ASC,
	[Slb_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SBT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SBT_Master](
	[SBT_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[Ref_Vno] [nvarchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[Customer_Id] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NOT NULL,
	[Invoice_In] [nvarchar](50) NOT NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[Fare_Amount] [decimal](18, 6) NOT NULL,
	[FC_Amount] [decimal](18, 6) NOT NULL,
	[PSC_Amount] [decimal](18, 6) NOT NULL,
	[Dis_Amount] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Action_type] [nvarchar](50) NULL,
	[Remarks] [nvarchar](500) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[R_Invoice] [bit] NULL,
	[Is_Printed] [bit] NULL,
	[No_Print] [int] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[Cancel_By] [nvarchar](50) NULL,
	[Cancel_Date] [datetime] NULL,
	[Cancel_Remarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_SBT] PRIMARY KEY CLUSTERED 
(
	[SBT_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SBT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SBT_Term](
	[SBT_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_SBT_Term] PRIMARY KEY CLUSTERED 
(
	[SBT_VNo] ASC,
	[ST_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SC_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SC_Details](
	[SC_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](500) NULL,
	[QOT_Invoice] [nvarchar](50) NULL,
	[QOT_Sno] [numeric](18, 0) NULL,
	[SO_Invoice] [nvarchar](50) NULL,
	[SO_SNo] [nvarchar](50) NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[V_Rate] [decimal](18, 6) NULL,
	[Issue_Qty] [decimal](18, 6) NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [int] NULL,
	[ExtraFree_Qty] [decimal](18, 6) NULL,
	[ExtraStockFree_Qty] [decimal](18, 6) NULL,
	[T_Product] [bit] NULL,
	[S_Ledger] [bigint] NULL,
	[SR_Ledger] [bigint] NULL,
	[SZ1] [nvarchar](50) NULL,
	[SZ2] [nvarchar](50) NULL,
	[SZ3] [nvarchar](50) NULL,
	[SZ4] [nvarchar](50) NULL,
	[SZ5] [nvarchar](50) NULL,
	[SZ6] [nvarchar](50) NULL,
	[SZ7] [nvarchar](50) NULL,
	[SZ8] [nvarchar](50) NULL,
	[SZ9] [nvarchar](50) NULL,
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
	[AltIssue_Qty] [decimal](18, 6) NULL,
 CONSTRAINT [PK_SC_Details] PRIMARY KEY CLUSTERED 
(
	[SC_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SC_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SC_Master](
	[SC_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [varchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[Ref_Vno] [nvarchar](50) NULL,
	[Ref_Date] [datetime] NULL,
	[Ref_Miti] [varchar](10) NULL,
	[Customer_Id] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_Mode] [nvarchar](50) NULL,
	[Payment_Mode] [varchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[QOT_Invoice] [nvarchar](250) NULL,
	[QOT_Date] [datetime] NULL,
	[SO_Invoice] [nvarchar](250) NULL,
	[SO_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CounterId] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 8) NOT NULL,
	[B_Amount] [decimal](18, 8) NOT NULL,
	[T_Amount] [decimal](18, 8) NOT NULL,
	[N_Amount] [decimal](18, 0) NOT NULL,
	[LN_Amount] [decimal](18, 0) NOT NULL,
	[V_Amount] [decimal](18, 0) NULL,
	[Tbl_Amount] [decimal](18, 0) NULL,
	[Tender_Amount] [decimal](18, 6) NULL,
	[Return_Amount] [decimal](18, 6) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](500) NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_SC] PRIMARY KEY CLUSTERED 
(
	[SC_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SC_Master_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SC_Master_OtherDetails](
	[SC_Invoice] [nvarchar](50) NOT NULL,
	[Transport] [nvarchar](255) NULL,
	[VechileNo] [nvarchar](50) NULL,
	[BiltyNo] [nvarchar](50) NULL,
	[Package] [nvarchar](100) NULL,
	[BiltyDate] [date] NULL,
	[BiltyType] [nvarchar](50) NULL,
	[Driver] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LicenseNo] [nvarchar](50) NULL,
	[MailingAddress] [nvarchar](500) NULL,
	[MCity] [nvarchar](50) NULL,
	[MState] [nvarchar](50) NULL,
	[MCountry] [nvarchar](50) NULL,
	[MEmail] [nvarchar](50) NULL,
	[ShippingAddress] [nvarchar](500) NULL,
	[SCity] [nvarchar](50) NULL,
	[SState] [nvarchar](50) NULL,
	[SCountry] [nvarchar](50) NULL,
	[SEmail] [nvarchar](50) NULL,
	[ContractNo] [nvarchar](50) NULL,
	[ContractDate] [date] NULL,
	[ExportInvoice] [nvarchar](50) NULL,
	[ExportInvoiceDate] [date] NULL,
	[VendorOrderNo] [nvarchar](50) NULL,
	[BankDetails] [nvarchar](100) NULL,
	[LcNumber] [nvarchar](50) NULL,
	[CustomDetails] [nvarchar](100) NULL,
 CONSTRAINT [PK_SC_Master_OtherDetails] PRIMARY KEY CLUSTERED 
(
	[SC_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SC_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SC_Term](
	[SC_Vno] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_SC_Term] PRIMARY KEY CLUSTERED 
(
	[SC_Vno] ASC,
	[ST_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Scheme_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Scheme_Details](
	[Scheme_Id] [int] NOT NULL,
	[Sno] [int] NOT NULL,
	[P_Code] [bigint] NOT NULL,
	[P_Group] [int] NULL,
	[P_SubGroup] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[PercentageValue] [decimal](18, 6) NOT NULL,
	[DiscountValue] [decimal](18, 6) NOT NULL,
	[MinValue] [decimal](18, 6) NOT NULL,
	[MaxValue] [decimal](18, 6) NOT NULL,
	[FreeQty] [decimal](18, 6) NULL,
	[ProductRate] [decimal](18, 6) NOT NULL,
	[LessPercentage] [decimal](18, 6) NOT NULL,
	[DisPercentage] [decimal](18, 6) NOT NULL,
 CONSTRAINT [PK_Scheme_Details] PRIMARY KEY CLUSTERED 
(
	[Scheme_Id] ASC,
	[Sno] ASC,
	[P_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[Scheme_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[Scheme_Master](
	[Scheme_Id] [int] IDENTITY(1,1) NOT NULL,
	[Scheme_Name] [varchar](64) NOT NULL,
	[FromDate] [datetime] NULL,
	[FromTo] [datetime] NULL,
	[RatedAs] [varchar](32) NULL,
	[Basis] [varchar](32) NULL,
	[MannualOveride] [bit] NULL,
	[ConsiderReturn] [bit] NULL,
	[FromMiti] [varchar](10) NOT NULL,
	[ToMiti] [varchar](10) NOT NULL,
	[SchemeDate] [datetime] NOT NULL,
	[SchemeMiti] [varchar](10) NOT NULL,
	[ProductTerm] [varchar](10) NULL,
	[ProdUnit] [varchar](5) NULL,
	[BillTerm] [varchar](10) NULL,
	[Term_Basis] [varchar](64) NULL,
	[ApplyFor] [varchar](32) NULL,
	[BranchId] [int] NULL,
	[CompanyUnitId] [int] NULL,
	[Remarks] [varchar](512) NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK__Scheme_M__5967B8AD2C29EDCC] PRIMARY KEY CLUSTERED 
(
	[Scheme_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SectorMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SectorMaster](
	[SID] [int] NOT NULL,
	[SectorDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_SectorMaster] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Description] UNIQUE NONCLUSTERED 
(
	[SectorDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SeniorAgent]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SeniorAgent](
	[SAgentId] [int] NOT NULL,
	[SAgent] [nvarchar](200) NOT NULL,
	[SAgentCode] [nvarchar](50) NOT NULL,
	[PhoneNo] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NOT NULL,
	[Comm] [decimal](18, 6) NULL,
	[GLID] [bigint] NULL,
	[Branch_id] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SeniorAgent] PRIMARY KEY CLUSTERED 
(
	[SAgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SeniorAgentDesc] UNIQUE NONCLUSTERED 
(
	[SAgent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SeniorAgentShortName] UNIQUE NONCLUSTERED 
(
	[SAgentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SO_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SO_Details](
	[SO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](500) NULL,
	[IND_Invoice] [nvarchar](50) NULL,
	[IND_Sno] [int] NULL,
	[QOT_Invoice] [nvarchar](50) NULL,
	[QOT_SNo] [int] NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[V_Rate] [decimal](18, 6) NULL,
	[Issue_Qty] [decimal](18, 6) NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [int] NULL,
	[ExtraFree_Qty] [decimal](18, 6) NULL,
	[ExtraStockFree_Qty] [decimal](18, 6) NULL,
	[T_Product] [bit] NULL,
	[S_Ledger] [bigint] NULL,
	[SR_Ledger] [bigint] NULL,
	[SZ1] [nvarchar](50) NULL,
	[SZ2] [nvarchar](50) NULL,
	[SZ3] [nvarchar](50) NULL,
	[SZ4] [nvarchar](50) NULL,
	[SZ5] [nvarchar](50) NULL,
	[SZ6] [nvarchar](50) NULL,
	[SZ7] [nvarchar](50) NULL,
	[SZ8] [nvarchar](50) NULL,
	[SZ9] [nvarchar](50) NULL,
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
	[Notes] [nvarchar](500) NULL,
	[PrintedItem] [bit] NULL,
	[PrintKOT] [bit] NULL,
	[OrderTime] [datetime] NULL,
	[Print_Time] [datetime] NULL,
	[Is_Canceled] [bit] NULL,
	[CancelNotes] [nvarchar](500) NULL,
	[PDiscountRate] [decimal](18, 6) NULL,
	[PDiscount] [decimal](18, 6) NULL,
	[BDiscountRate] [decimal](18, 6) NULL,
	[BDiscount] [decimal](18, 6) NULL,
	[ServiceChargeRate] [decimal](18, 6) NULL,
	[ServiceCharge] [decimal](18, 6) NULL,
 CONSTRAINT [PK_SO_Details] PRIMARY KEY CLUSTERED 
(
	[SO_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SO_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SO_Master](
	[SO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [varchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[Ref_Vno] [nvarchar](50) NULL,
	[Ref_Date] [datetime] NULL,
	[Ref_Miti] [varchar](10) NULL,
	[Customer_Id] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_Mode] [nvarchar](50) NULL,
	[Payment_Mode] [varchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[IND_Invoice] [nvarchar](250) NULL,
	[IND_Date] [datetime] NULL,
	[QOT_Invoice] [nvarchar](250) NULL,
	[QOT_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CounterId] [int] NULL,
	[TableId] [int] NULL,
	[CombineTableId] [nvarchar](500) NULL,
	[NoOfPerson] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Tender_Amount] [decimal](18, 6) NULL,
	[Return_Amount] [decimal](18, 6) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[R_Invoice] [bit] NULL,
	[Is_Printed] [bit] NULL,
	[No_Print] [int] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_SO] PRIMARY KEY CLUSTERED 
(
	[SO_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SO_Master_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SO_Master_OtherDetails](
	[SO_Invoice] [nvarchar](50) NOT NULL,
	[Transport] [nvarchar](255) NULL,
	[VechileNo] [nvarchar](50) NULL,
	[BiltyNo] [nvarchar](50) NULL,
	[Package] [nvarchar](100) NULL,
	[BiltyDate] [date] NULL,
	[BiltyType] [nvarchar](50) NULL,
	[Driver] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LicenseNo] [nvarchar](50) NULL,
	[MailingAddress] [nvarchar](500) NULL,
	[MCity] [nvarchar](50) NULL,
	[MState] [nvarchar](50) NULL,
	[MCountry] [nvarchar](50) NULL,
	[MEmail] [nvarchar](50) NULL,
	[ShippingAddress] [nvarchar](500) NULL,
	[SCity] [nvarchar](50) NULL,
	[SState] [nvarchar](50) NULL,
	[SCountry] [nvarchar](50) NULL,
	[SEmail] [nvarchar](50) NULL,
	[ContractNo] [nvarchar](50) NULL,
	[ContractDate] [date] NULL,
	[ExportInvoice] [nvarchar](50) NULL,
	[ExportInvoiceDate] [date] NULL,
	[VendorOrderNo] [nvarchar](50) NULL,
	[BankDetails] [nvarchar](100) NULL,
	[LcNumber] [nvarchar](50) NULL,
	[CustomDetails] [nvarchar](100) NULL,
 CONSTRAINT [PK_SO_Master_OtherDetails] PRIMARY KEY CLUSTERED 
(
	[SO_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SO_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SO_Term](
	[SO_Vno] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_SO_Term] PRIMARY KEY CLUSTERED 
(
	[SO_Vno] ASC,
	[ST_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SQ_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SQ_Details](
	[SQ_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](500) NULL,
	[IND_Invoice] [nvarchar](50) NULL,
	[IND_Sno] [numeric](18, 0) NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[V_Rate] [decimal](18, 6) NULL,
	[Issue_Qty] [decimal](18, 6) NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [int] NULL,
	[ExtraFree_Qty] [decimal](18, 6) NULL,
	[ExtraStockFree_Qty] [decimal](18, 6) NULL,
	[PG_Id] [int] NULL,
	[T_Product] [bit] NULL,
	[S_Ledger] [bigint] NULL,
	[SR_Ledger] [bigint] NULL,
	[SZ1] [nvarchar](50) NULL,
	[SZ2] [nvarchar](50) NULL,
	[SZ3] [nvarchar](50) NULL,
	[SZ4] [nvarchar](50) NULL,
	[SZ5] [nvarchar](50) NULL,
	[SZ6] [nvarchar](50) NULL,
	[SZ7] [nvarchar](50) NULL,
	[SZ8] [nvarchar](50) NULL,
	[SZ9] [nvarchar](50) NULL,
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
 CONSTRAINT [PK_SQ_Details] PRIMARY KEY CLUSTERED 
(
	[SQ_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SQ_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SQ_Master](
	[SQ_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](50) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[Expiry_Date] [datetime] NOT NULL,
	[Ref_Vno] [nvarchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[Ref_VMiti] [varchar](10) NULL,
	[Customer_Id] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_Mode] [nvarchar](50) NULL,
	[Payment_Mode] [varchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[IND_Invoice] [nvarchar](250) NULL,
	[IND_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Tender_Amount] [decimal](18, 6) NULL,
	[Return_Amount] [decimal](18, 6) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](500) NULL,
	[R_Invoice] [bit] NULL,
	[Is_Printed] [bit] NULL,
	[No_Print] [int] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[Cancel_By] [nvarchar](50) NULL,
	[Cancel_Date] [datetime] NULL,
	[Cancel_Remarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_SQ_Master] PRIMARY KEY CLUSTERED 
(
	[SQ_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SQ_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SQ_Term](
	[SQ_Vno] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_SQ_Term] PRIMARY KEY CLUSTERED 
(
	[SQ_Vno] ASC,
	[ST_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SR_Details](
	[SR_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](500) NULL,
	[SB_Invoice] [nvarchar](50) NULL,
	[SB_Sno] [numeric](18, 0) NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[V_Rate] [decimal](18, 6) NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [int] NULL,
	[ExtraFree_Qty] [decimal](18, 6) NULL,
	[ExtraStockFree_Qty] [decimal](18, 6) NULL,
	[T_Product] [bit] NULL,
	[S_Ledger] [bigint] NULL,
	[SR_Ledger] [bigint] NULL,
	[SZ1] [nvarchar](50) NULL,
	[SZ2] [nvarchar](50) NULL,
	[SZ3] [nvarchar](50) NULL,
	[SZ4] [nvarchar](50) NULL,
	[SZ5] [nvarchar](50) NULL,
	[SZ6] [nvarchar](50) NULL,
	[SZ7] [nvarchar](50) NULL,
	[SZ8] [nvarchar](50) NULL,
	[SZ9] [nvarchar](50) NULL,
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
 CONSTRAINT [PK_SR_Details] PRIMARY KEY CLUSTERED 
(
	[SR_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SR_Master](
	[SR_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [varchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[SB_Invoice] [nvarchar](50) NULL,
	[SB_Date] [datetime] NULL,
	[SB_Miti] [varchar](10) NULL,
	[Customer_ID] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_Mode] [nvarchar](50) NULL,
	[Payment_Mode] [varchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CounterId] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL CONSTRAINT [DF_SR_Master_Cur_Rate]  DEFAULT ((1)),
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Tender_Amount] [decimal](18, 6) NULL,
	[Return_Amount] [decimal](18, 6) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[R_Invoice] [bit] NULL,
	[Is_Printed] [bit] NULL,
	[No_Print] [int] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[Cancel_By] [nvarchar](50) NULL,
	[Cancel_Date] [datetime] NULL,
	[Cancel_Remarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_SR] PRIMARY KEY CLUSTERED 
(
	[SR_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SR_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SR_Term](
	[SR_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NOT NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_SR_Term] PRIMARY KEY CLUSTERED 
(
	[SR_VNo] ASC,
	[ST_Id] ASC,
	[SNo] ASC,
	[Term_Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SRT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SRT_Term](
	[SRT_ID] [int] NOT NULL,
	[Order_No] [int] NOT NULL,
	[Module] [char](2) NOT NULL,
	[ST_Name] [nvarchar](50) NOT NULL,
	[ST_Type] [char](2) NOT NULL,
	[Ledger] [bigint] NOT NULL,
	[ST_Basis] [char](2) NOT NULL,
	[ST_Sign] [char](1) NOT NULL,
	[ST_Condition] [char](1) NOT NULL,
	[ST_Rate] [decimal](18, 6) NULL,
	[ST_Branch] [int] NOT NULL,
	[ST_CompanyUnit] [int] NULL,
	[ST_Profitability] [bit] NULL,
	[ST_Supess] [bit] NULL,
	[ST_Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SRT_Term] PRIMARY KEY CLUSTERED 
(
	[SRT_ID] ASC,
	[Order_No] ASC,
	[Module] ASC,
	[ST_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SRT_Term] UNIQUE NONCLUSTERED 
(
	[Order_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[ST_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[ST_Term](
	[ST_ID] [int] NOT NULL,
	[Order_No] [int] NOT NULL,
	[Module] [char](4) NOT NULL,
	[ST_Name] [nvarchar](50) NOT NULL,
	[ST_Type] [char](2) NOT NULL,
	[Ledger] [bigint] NOT NULL,
	[ST_Basis] [char](2) NOT NULL,
	[ST_Sign] [char](1) NOT NULL,
	[ST_Condition] [char](1) NOT NULL,
	[ST_Rate] [decimal](18, 6) NULL CONSTRAINT [DF_ST_Term_ST_Rate]  DEFAULT ((0.00)),
	[ST_Branch] [int] NOT NULL,
	[ST_CompanyUnit] [int] NULL,
	[ST_Profitability] [bit] NULL,
	[ST_Supess] [bit] NULL,
	[ST_Status] [bit] NOT NULL CONSTRAINT [DF_ST_Term_ST_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_ST_Term_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ST_Term] PRIMARY KEY CLUSTERED 
(
	[ST_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ST_TermDesc] UNIQUE NONCLUSTERED 
(
	[ST_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ST_TermOrderNo] UNIQUE NONCLUSTERED 
(
	[Order_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[STA_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[STA_Details](
	[StockAdjust_No] [nvarchar](50) NOT NULL,
	[Sno] [int] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[downId] [int] NULL,
	[AdjType] [char](1) NOT NULL,
	[AltQty] [decimal](18, 6) NOT NULL,
	[AltUnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[UnitId] [int] NULL,
	[AltStockQty] [decimal](18, 6) NOT NULL,
	[StockQty] [decimal](18, 6) NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[NetAmount] [decimal](18, 6) NOT NULL,
	[AddDesc] [nvarchar](1024) NULL,
	[DepartmentId] [int] NULL,
	[StConvRatio] [decimal](18, 6) NULL,
	[PhyStkNo] [varchar](15) NULL,
	[PhyStkSno] [int] NULL,
 CONSTRAINT [PK_STA_Details] PRIMARY KEY CLUSTERED 
(
	[StockAdjust_No] ASC,
	[Sno] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[STA_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[STA_Master](
	[StockAdjust_No] [nvarchar](50) NOT NULL,
	[VDate] [datetime] NOT NULL,
	[Vtime] [datetime] NOT NULL,
	[VMiti] [varchar](10) NULL,
	[DepartmentId] [int] NULL,
	[BarCode] [varchar](10) NULL,
	[PhyStockNo] [nvarchar](50) NULL,
	[Posting] [char](1) NULL,
	[Export] [char](1) NULL,
	[PostedBy] [nvarchar](50) NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[AuditBy] [nvarchar](50) NULL,
	[AuthorizeBy] [nvarchar](50) NULL,
	[AuditDate] [datetime] NULL,
	[PostedDate] [datetime] NULL,
	[AuthorizeDate] [datetime] NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Status] [nvarchar](50) NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[PrintValue] [int] NULL,
	[AuthorizeRemarks] [nvarchar](1024) NULL,
	[BranchId] [int] NOT NULL,
	[CompanyUnitId] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK__STA_Mast__C5DCEA07C4246EFA] PRIMARY KEY CLUSTERED 
(
	[StockAdjust_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[StockBatchDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[StockBatchDetails](
	[Module] [varchar](10) NOT NULL,
	[Voucher_No] [varchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [varchar](10) NOT NULL,
	[Batch_SNo] [numeric](18, 0) NOT NULL,
	[Batch] [varchar](25) NOT NULL,
	[AltQty] [decimal](18, 6) NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[AltStockQty] [decimal](18, 6) NULL,
	[StockQty] [decimal](18, 6) NULL,
	[FreeQty] [decimal](18, 6) NULL,
	[FreeStockQty] [decimal](18, 6) NULL,
	[FreeUnit_Id] [int] NULL,
	[ConvRatio] [decimal](18, 6) NULL,
	[Margin] [decimal](18, 6) NULL,
	[StockVal] [decimal](18, 6) NULL,
	[ExtraFreeQty] [decimal](18, 6) NULL,
	[StockExtraFreeQty] [decimal](18, 6) NULL,
	[ExtraFreeUnit_Id] [int] NULL,
	[Buy_Rate] [decimal](18, 6) NULL,
	[Sales_Rate] [decimal](18, 6) NULL,
	[Trade_Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[MRP] [decimal](18, 6) NULL,
	[MRP_Rate] [decimal](18, 6) NULL,
	[Min_Qty] [decimal](18, 6) NULL,
	[Max_Qty] [decimal](18, 6) NULL,
	[Min_Disc] [decimal](18, 6) NULL,
	[Max_Disc] [decimal](18, 6) NULL,
	[Manufacturing_Date] [datetime] NULL,
	[Expiry_Date] [datetime] NULL,
	[Transaction_Type] [char](1) NULL,
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_StockBatchDetails] PRIMARY KEY CLUSTERED 
(
	[Module] ASC,
	[Voucher_No] ASC,
	[SNo] ASC,
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[StockDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[StockDetails](
	[Module] [nvarchar](10) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Serial_No] [int] NOT NULL,
	[PurRefVno] [nvarchar](50) NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](10) NOT NULL,
	[Voucher_Time] [datetime] NOT NULL,
	[Ledger_Id] [bigint] NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Department_Id1] [int] NULL,
	[Department_Id2] [int] NULL,
	[Department_Id3] [int] NULL,
	[Department_Id4] [int] NULL,
	[Currency_Id] [int] NOT NULL,
	[Currency_Rate] [decimal](18, 6) NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[down_Id] [int] NULL,
	[CostCenter_Id] [int] NULL,
	[AltQty] [decimal](18, 6) NULL,
	[AltUnit_Id] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[AltStockQty] [decimal](18, 6) NULL,
	[StockQty] [decimal](18, 6) NULL,
	[FreeQty] [decimal](18, 6) NULL,
	[FreeUnit_Id] [int] NULL,
	[StockFreeQty] [decimal](18, 6) NULL,
	[ConvRatio] [decimal](18, 6) NULL,
	[ExtraFreeQty] [decimal](18, 6) NULL,
	[ExtraFreeUnit_Id] [int] NULL,
	[ExtraStockFreeQty] [decimal](18, 6) NULL,
	[Rate] [decimal](18, 6) NULL,
	[BasicAmt] [decimal](18, 6) NULL,
	[TermAmt] [decimal](18, 6) NULL,
	[NetAmt] [decimal](18, 6) NULL,
	[BillTermAmt] [decimal](18, 6) NULL,
	[TaxRate] [decimal](18, 6) NULL,
	[TaxableAmt] [decimal](18, 6) NULL,
	[DocVal] [decimal](18, 6) NULL,
	[ReturnVal] [decimal](18, 6) NULL,
	[StockVal] [decimal](18, 6) NULL,
	[AddStockVal] [decimal](18, 6) NULL,
	[PartyInv] [nvarchar](50) NULL,
	[EntryType] [char](1) NOT NULL,
	[AuthBy] [nvarchar](50) NULL,
	[AuthDate] [datetime] NULL,
	[RecoBy] [nvarchar](50) NULL,
	[RecoDate] [datetime] NULL,
	[Counter_Id] [int] NULL,
	[RoomNo] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Branch_Id] [int] NOT NULL,
	[CmpUnit_Id] [int] NULL,
	[Adj_Qty] [decimal](18, 6) NULL,
	[Adj_VoucherNo] [nvarchar](50) NULL,
	[Adj_Module] [char](10) NULL,
	[SalesRate] [decimal](18, 6) NULL,
	[FiscalYearId] [int] NOT NULL
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[Subledger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[Subledger](
	[SLId] [int] NOT NULL,
	[SLName] [nvarchar](200) NOT NULL,
	[SLCode] [nvarchar](50) NOT NULL,
	[SLAddress] [nvarchar](500) NULL,
	[SLPhoneNo] [nvarchar](50) NULL,
	[GLID] [bigint] NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Subledger] PRIMARY KEY CLUSTERED 
(
	[SLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SubledgerDesc] UNIQUE NONCLUSTERED 
(
	[SLName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SubledgerShortName] UNIQUE NONCLUSTERED 
(
	[SLCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[SystemConfiguration]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SystemConfiguration](
	[SC_Id] [tinyint] NOT NULL CONSTRAINT [DF_SystemConfiguration_SC_Id]  DEFAULT ((1)),
	[Date_Type] [char](1) NULL CONSTRAINT [DF_SystemConfiguration_Date_Type]  DEFAULT ('M'),
	[Audit_Trial] [bit] NULL CONSTRAINT [DF_SystemConfiguration_Audit_Trial]  DEFAULT ((0)),
	[Udf] [bit] NULL CONSTRAINT [DF_SystemConfiguration_Udf]  DEFAULT ((0)),
	[AutoPupup] [bit] NULL CONSTRAINT [DF_SystemConfiguration_AutoPupup]  DEFAULT ((1)),
	[CurrentDate] [bit] NULL CONSTRAINT [DF_SystemConfiguration_CurrentDate]  DEFAULT ((1)),
	[ConfirmSave] [bit] NULL CONSTRAINT [DF_SystemConfiguration_ConfirmSave]  DEFAULT ((0)),
	[ConfirmCancel] [bit] NULL CONSTRAINT [DF_SystemConfiguration_ConfirmCancel]  DEFAULT ((0)),
	[Cur_Id] [int] NULL,
	[FY_Id] [int] NULL,
	[DefaultPrinter] [nvarchar](100) NULL,
	[BackupSch_IntvDays] [int] NULL,
	[Backup_Path] [varchar](255) NULL,
	[PL_AC] [bigint] NULL,
	[Cash_AC] [bigint] NULL,
	[Vat_AC] [bigint] NULL,
	[PDCBank_AC] [bigint] NULL,
	[Transby_Code] [bit] NULL CONSTRAINT [DF_SystemConfiguration_Transby_Code]  DEFAULT ((0)),
	[Negative_Tran] [bit] NULL,
	[Amount_Format] [varchar](15) NULL CONSTRAINT [DF_SystemConfiguration_Amount_Format]  DEFAULT ((0.00)),
	[Rate_Format] [varchar](15) NULL CONSTRAINT [DF_SystemConfiguration_Rate_Format]  DEFAULT ((0.00)),
	[Qty_Format] [varchar](15) NULL CONSTRAINT [DF_SystemConfiguration_Qty_Format]  DEFAULT ((0.00)),
	[AltQty_Format] [varchar](15) NULL CONSTRAINT [DF_SystemConfiguration_AltQty_Format]  DEFAULT ((0.00)),
	[Cur_Format] [varchar](15) NULL CONSTRAINT [DF_SystemConfiguration_Cur_Format]  DEFAULT ((0.00)),
	[Font_Name] [varchar](555) NULL,
	[Font_Size] [decimal](18, 0) NULL,
	[Paper_Size] [varchar](15) NULL,
	[ReportFont_Style] [nvarchar](50) NULL,
	[Printing_DateTime] [bit] NULL,
	[Purchase_AC] [bigint] NULL,
	[PurchaseReturn_AC] [bigint] NULL,
	[PurchaseVat_Id] [int] NULL,
	[PurchaseAddVat_Id] [int] NULL,
	[PurchaseProDiscount_Id] [int] NULL,
	[PurchaseDiscount_ID] [int] NULL,
	[PCredit_Balance_War] [int] NULL,
	[PCredit_Days_War] [int] NULL,
	[PCarry_Rate] [bit] NULL,
	[PLast_Rate] [bit] NULL,
	[PBatch_Rate] [bit] NULL,
	[PQuality_Control] [bit] NULL,
	[PPGrpWiseBilling] [bit] NULL,
	[PAdvancePayment] [bit] NULL,
	[Sales_AC] [bigint] NULL,
	[SalesReturn_AC] [bigint] NULL,
	[SalesVat_Id] [int] NULL,
	[SalesDiscount_Id] [int] NULL,
	[SalesSpecialDiscount_Id] [int] NULL,
	[SalesServiceCharge_Id] [int] NULL,
	[SCreditBalance_War] [int] NULL,
	[SCreditDays_War] [int] NULL,
	[SChange_Rate] [bit] NULL,
	[SLast_Rate] [bit] NULL,
	[SSalesCarry_Rate] [bit] NULL,
	[SDispatch_Order] [bit] NULL,
	[SCancellationCustomer_Code] [int] NULL,
	[SCancellationProduct_Id] [int] NULL,
	[DefaultInvoiceDesign] [varchar](250) NULL,
	[DefaultABTInvoiceDesign] [varchar](250) NULL,
	[DefaultInvoicePrinter] [varchar](100) NULL,
	[DefaultInvoiceDocNumbering] [varchar](100) NULL,
	[DefaultPreInvoiceDesign] [varchar](250) NULL,
	[DefaultOrderDesign] [varchar](250) NULL,
	[DefaultOrderPrinter] [varchar](100) NULL,
	[DefaultOrderDocNumbering] [varchar](100) NULL,
	[Stock_ValueInSales_Return] [bit] NULL,
	[Available_Stock] [bit] NULL,
	[Customer_Name] [bit] NULL,
	[SPGrpWiseBilling] [bit] NULL,
	[TenderAmount] [bit] NULL,
	[AdvanceReceipt] [bit] NULL,
	[OpeningStockPL_AC] [bigint] NULL,
	[ClosingStockPL_AC] [bigint] NULL,
	[ClosingStockBS_Ac] [bigint] NULL,
	[Negative_Stock_War] [int] NULL,
	[down_Catery] [bit] NULL,
	[Product_Code] [bit] NULL,
	[AltQty_Alteration] [bit] NULL,
	[Alteration_Part] [bit] NULL,
	[CarryBatch_Qty] [bit] NULL,
	[Breakup_Qty] [bit] NULL,
	[Mfg_Date] [bit] NULL,
	[Exp_Date] [bit] NULL,
	[Mfg_Date_Validation] [bit] NULL,
	[Exp_Date_Validation] [bit] NULL,
	[Free_Qty] [bit] NULL,
	[Extra_Free_Qty] [bit] NULL,
	[down_Wise_Filter] [bit] NULL,
	[Finished_Qty] [bit] NULL,
	[Equal_Qty] [bit] NULL,
	[Idown_Wise_Filter] [bit] NULL,
	[Debtor_Id] [int] NULL,
	[Creditor_Id] [int] NULL,
	[Salary_Id] [bigint] NULL,
	[TDS_Id] [bigint] NULL,
	[PF_Id] [bigint] NULL,
	[Email_Id] [nvarchar](100) NULL,
	[Email_Password] [nvarchar](50) NULL,
	[BeforeBuyRate] [bit] NULL,
	[BeforeSalesRate] [bit] NULL,
	[BarcodePrinter] [varchar](250) NULL,
	[DefaultBarcodeDesign] [varchar](250) NULL,
 CONSTRAINT [PK_SystemConfiguration] PRIMARY KEY CLUSTERED 
(
	[SC_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SystemControlOptions]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[SystemControlOptions](
	[SCOptions_Id] [int] IDENTITY(1,1) NOT NULL,
	[Header] [varchar](50) NULL,
	[Options_Name] [varchar](50) NULL,
	[Enable] [bit] NULL CONSTRAINT [DF_SystemControlOptions_Enable]  DEFAULT ((0)),
	[Mandatory] [bit] NULL CONSTRAINT [DF_SystemControlOptions_Mandatory]  DEFAULT ((0)),
 CONSTRAINT [PK_SystemControlOptions] PRIMARY KEY CLUSTERED 
(
	[SCOptions_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[SystemSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[SystemSetting](
	[SyId] [tinyint] NOT NULL,
	[EnglishDate] [bit] NULL,
	[AuditTrial] [bit] NULL,
	[UdF] [bit] NULL,
	[Autopoplist] [bit] NULL,
	[CurrentDate] [bit] NULL,
	[ConformSave] [bit] NULL,
	[ConformCancel] [bit] NULL,
	[ConformExits] [bit] NULL,
	[CurrencyRate] [float] NULL,
	[CurrencyId] [int] NULL,
	[DefaultPrinter] [nvarchar](100) NULL,
	[AmountFormat] [float] NULL,
	[RateFormat] [float] NULL,
	[QtyFormat] [float] NULL,
	[CurrencyFormatF] [float] NULL,
	[DefaultFiscalYearId] [int] NULL,
	[DefaultOrderPrinter] [nvarchar](100) NULL,
	[DefaultInvoicePrinter] [nvarchar](100) NULL,
	[DefaultOrderNumbering] [nvarchar](100) NULL,
	[DefaultInvoiceNumbering] [nvarchar](100) NULL,
	[DefaultAvtInvoiceNumbering] [nvarchar](100) NULL,
	[DefaultOrderDesign] [nvarchar](100) NULL,
	[IsOrderPrint] [bit] NULL,
	[DefaultInvoiceDesign] [nvarchar](100) NULL,
	[IsInvoicePrint] [bit] NULL,
	[DefaultAvtDesign] [nvarchar](100) NULL,
	[DefaultFontsName] [nvarchar](100) NULL,
	[DefaultFontsSize] [int] NULL,
	[DefaultPaperSize] [nvarchar](100) NULL,
	[DefaultReportStyle] [nvarchar](100) NULL,
	[DefaultPrintDateTime] [bit] NULL,
	[DefaultFormColor] [nvarchar](100) NULL,
	[DefaultTextColor] [nvarchar](100) NULL,
	[DebtorsGroupId] [int] NULL,
	[CreditorGroupId] [int] NULL,
	[SalaryLedgerId] [bigint] NULL,
	[TDSLedgerId] [bigint] NULL,
	[PFLedgerId] [bigint] NULL,
	[DefaultEmail] [nvarchar](300) NULL,
	[DefaultEmailPassword] [nvarchar](500) NULL,
	[BackupDays] [int] NULL,
	[BackupLocation] [nvarchar](500) NULL,
 CONSTRAINT [PK_SystemSetting] PRIMARY KEY CLUSTERED 
(
	[SyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[TableMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[TableMaster](
	[TableId] [int] NOT NULL,
	[TableName] [nvarchar](50) NOT NULL,
	[TableCode] [nvarchar](50) NOT NULL,
	[FloorId] [int] NOT NULL,
	[Branch_Id] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[TableStatus] [char](1) NOT NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [varchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[TableType] [varchar](50) NULL,
	[Printed] [int] NULL,
 CONSTRAINT [PK_TableMaster] PRIMARY KEY CLUSTERED 
(
	[TableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_TableMasterDesc] UNIQUE NONCLUSTERED 
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_TableMasterShortName] UNIQUE NONCLUSTERED 
(
	[TableCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[TicketRefund]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[TicketRefund](
	[VoucherNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](10) NOT NULL,
	[RefVno] [nvarchar](50) NULL,
	[RefDate] [datetime] NULL,
	[AgencyId] [bigint] NOT NULL,
	[AirlinesId] [int] NOT NULL,
	[LedgerId] [bigint] NOT NULL,
	[Sales] [decimal](18, 8) NULL,
	[Purchase] [decimal](18, 8) NULL,
	[CancellationCharge] [decimal](18, 8) NULL,
	[FineCharge] [decimal](18, 8) NULL,
	[BranchId] [int] NOT NULL,
	[CompanyUnitId] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_TicketRefund] PRIMARY KEY CLUSTERED 
(
	[VoucherNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[UDF]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[UDF](
	[UDF_Code] [varchar](5) NOT NULL,
	[Entry_Module] [varchar](10) NOT NULL,
	[Field_Name] [varchar](50) NOT NULL,
	[Field_Type] [varchar](15) NOT NULL,
	[Total_Width] [varchar](256) NOT NULL,
	[Field_Decimal] [varchar](1) NULL,
	[Date_Format] [varchar](15) NULL,
	[List_field_Name] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[UDF_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[UDF_Entry]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [AMS].[UDF_Entry](
	[UDF_Code] [varchar](5) NOT NULL,
	[Entry_module] [varchar](256) NOT NULL,
	[Field_Name] [varchar](50) NOT NULL,
	[Field_Type] [varchar](15) NOT NULL,
	[Total_Width] [varchar](256) NOT NULL,
	[Mandotary_opt] [char](1) NOT NULL,
	[Udf_Schedule] [int] NULL,
	[Duplicate_opt] [char](1) NULL,
	[Date_Format] [varchar](15) NULL,
	[Field_Decimal] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[UDF_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [AMS].[UnitWiseLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[UnitWiseLedger](
	[UnitId] [int] NULL,
	[BranchId] [int] NULL,
	[LedgerId] [bigint] NULL,
	[Mapped] [bit] NULL,
	[Catery] [nvarchar](50) NULL
) ON [PRIMARY]


/****** Object:  Table [AMS].[VehicleColors]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[VehicleColors](
	[VHColorsId] [int] IDENTITY(1,1) NOT NULL,
	[VHColorsDesc] [nvarchar](200) NOT NULL,
	[VHColorsShortName] [nvarchar](50) NOT NULL,
	[VHColorsBranchId] [int] NOT NULL,
	[VHColorsCompanyUnitId] [int] NULL,
	[VHColorsActive] [bit] NOT NULL,
	[VHColorsEntryBy] [nvarchar](50) NOT NULL,
	[VHColorsEnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_VechileColors] PRIMARY KEY CLUSTERED 
(
	[VHColorsId] ASC,
	[VHColorsDesc] ASC,
	[VHColorsShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_VehicleColors] UNIQUE NONCLUSTERED 
(
	[VHColorsDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_VehicleColors_1] UNIQUE NONCLUSTERED 
(
	[VHColorsShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[VehicleNumber]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[VehicleNumber](
	[VHNoId] [int] IDENTITY(1,1) NOT NULL,
	[VNDesc] [nvarchar](200) NOT NULL,
	[VNShortName] [nvarchar](50) NOT NULL,
	[VNState] [nvarchar](50) NOT NULL,
	[VNStatus] [bit] NOT NULL,
	[VNBranchId] [int] NOT NULL,
	[VNCompanyUnitId] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_VehicleNumber] PRIMARY KEY CLUSTERED 
(
	[VHNoId] ASC,
	[VNDesc] ASC,
	[VNShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_VehicleNumber] UNIQUE NONCLUSTERED 
(
	[VNDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_VehicleNumber_1] UNIQUE NONCLUSTERED 
(
	[VNShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [AMS].[VehileModel]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [AMS].[VehileModel](
	[VHModelId] [int] IDENTITY(1,1) NOT NULL,
	[VHModelDesc] [nvarchar](200) NOT NULL,
	[VHModelShortName] [nvarchar](50) NOT NULL,
	[VHModelBranchId] [int] NOT NULL,
	[VHModelCompanyUnitId] [int] NULL,
	[VHModelActive] [bit] NOT NULL,
	[VHModelEntryBy] [nvarchar](50) NOT NULL,
	[VHModelEnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_VehileModel_1] PRIMARY KEY CLUSTERED 
(
	[VHModelId] ASC,
	[VHModelDesc] ASC,
	[VHModelShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_VehileModel] UNIQUE NONCLUSTERED 
(
	[VHModelDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_VehileModel_1] UNIQUE NONCLUSTERED 
(
	[VHModelShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [HOS].[BedMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[BedMaster](
	[BID] [int] NOT NULL,
	[BedDesc] [nvarchar](80) NULL,
	[BedShortName] [nvarchar](50) NULL,
	[Bedtype] [int] NULL,
	[WId] [int] NULL,
	[ChargeAmt] [decimal](18, 6) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_BedMaster] PRIMARY KEY CLUSTERED 
(
	[BID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [BedMasterDesc] UNIQUE NONCLUSTERED 
(
	[BedDesc] ASC,
	[BedShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [HOS].[BedType]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[BedType](
	[BID] [int] NOT NULL,
	[BDesc] [nvarchar](80) NULL,
	[BShortName] [nvarchar](50) NULL,
	[BranchId] [int] NULL,
	[Company_Unit] [int] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_BedType] PRIMARY KEY CLUSTERED 
(
	[BID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [BdType] UNIQUE NONCLUSTERED 
(
	[BDesc] ASC,
	[BShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [HOS].[Department]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [HOS].[Department](
	[DId] [int] NOT NULL,
	[DName] [nvarchar](80) NULL,
	[DCode] [nvarchar](50) NULL,
	[Dlevel] [char](10) NULL,
	[DoctorId] [int] NULL,
	[ItemId] [bigint] NULL,
	[ChargeAmt] [decimal](18, 6) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Department_1] PRIMARY KEY CLUSTERED 
(
	[DId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [HOS].[Doctor]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[Doctor](
	[DrId] [int] NOT NULL,
	[DrName] [nvarchar](200) NULL,
	[DrShortName] [nvarchar](50) NULL,
	[DrType] [int] NULL,
	[ContactNo] [numeric](18, 0) NULL,
	[Address] [nvarchar](150) NULL,
	[BranchId] [int] NULL,
	[CompanyUnit] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[DrId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [DoctorDesc] UNIQUE NONCLUSTERED 
(
	[DrName] ASC,
	[DrShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [HOS].[DoctorCommission]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[DoctorCommission](
	[Sno] [int] NULL,
	[DrId] [int] NULL,
	[ProCode] [bigint] NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[CommPer] [decimal](18, 6) NULL,
	[CommAmount] [decimal](18, 6) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[FiscalYearId] [int] NULL
) ON [PRIMARY]


/****** Object:  Table [HOS].[DoctorCommissionItem]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[DoctorCommissionItem](
	[InvoiceNo] [nvarchar](50) NULL,
	[SNo] [int] NULL,
	[DrId] [int] NULL,
	[ProId] [bigint] NULL,
	[CustomerDesc] [int] NULL,
	[CommPer] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Dis] [decimal](18, 6) NULL,
	[ProSno] [int] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL
) ON [PRIMARY]


/****** Object:  Table [HOS].[DoctorType]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[DoctorType](
	[DtID] [int] NOT NULL,
	[DrTypeDesc] [nvarchar](80) NULL,
	[DrTypeShortName] [nvarchar](50) NULL,
	[BranchId] [int] NULL,
	[Company_Unit] [int] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_DoctorType] PRIMARY KEY CLUSTERED 
(
	[DtID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [DrType] UNIQUE NONCLUSTERED 
(
	[DrTypeDesc] ASC,
	[DrTypeShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  Table [HOS].[PatientBillingDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[PatientBillingDetails](
	[VoucherNo] [nvarchar](50) NULL,
	[SNo] [int] NULL,
	[ProId] [bigint] NULL,
	[Qty] [decimal](18, 6) NULL,
	[Rate] [decimal](18, 6) NULL,
	[BasicAmt] [decimal](18, 6) NULL,
	[TermAmt] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[RouDoctorId] [int] NULL
) ON [PRIMARY]


/****** Object:  Table [HOS].[PatientBillingMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [HOS].[PatientBillingMaster](
	[Module] [char](10) NULL,
	[VoucherNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NULL,
	[VoucherMiti] [nvarchar](50) NULL,
	[PatientId] [bigint] NULL,
	[LedgerId] [bigint] NULL,
	[DoctorId] [int] NULL,
	[RefDoctorId] [int] NULL,
	[DepartmentId] [int] NULL,
	[BusinessId] [int] NULL,
	[MemberId] [int] NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[T_Rate] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[TenderAmount] [decimal](18, 6) NULL,
	[ChangeAMount] [decimal](18, 6) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[ActionType] [char](10) NULL,
	[Is_Printed] [bit] NULL,
	[NoOfPrint] [int] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Cancel_By] [nvarchar](50) NULL,
	[Cancel_Date] [datetime] NULL,
	[Cancel_Remarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
 CONSTRAINT [PK__Commissi__11F284192EC5E7B8] PRIMARY KEY CLUSTERED 
(
	[VoucherNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [HOS].[PatientBillingTerm]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [HOS].[PatientBillingTerm](
	[SB_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [HOS].[PatientMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

SET ANSI_PADDING ON

CREATE TABLE [HOS].[PatientMaster](
	[PaitentId] [bigint] NOT NULL,
	[RefDate] [datetime] NULL,
	[IPDId] [bigint] NULL,
	[Title] [char](10) NULL,
	[PaitentDesc] [nvarchar](100) NULL,
	[ShortName] [nvarchar](50) NULL,
	[TAddress] [nvarchar](500) NULL,
	[PAddress] [nvarchar](500) NULL,
	[AccountLedger] [nvarchar](150) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Age] [decimal](18, 2) NULL,
	[AgeType] [nvarchar](50) NULL,
	[DateofBirth] [datetime] NULL,
	[Gender] [nvarchar](50) NULL,
	[MaritalStatus] [nvarchar](50) NULL,
	[RegType] [nvarchar](50) NULL,
	[Nationality] [nvarchar](50) NULL,
	[Religion] [varchar](30) NULL,
	[BloodGrp] [nvarchar](50) NULL,
	[DepartmentId] [int] NULL,
	[DrId] [int] NULL,
	[RefDrDesc] [nvarchar](50) NULL,
	[EmailAdd] [nvarchar](150) NULL,
	[PastHistory] [nvarchar](500) NULL,
	[ContactPer] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[BranchId] [int] NULL,
	[CompanyUnit] [int] NULL,
 CONSTRAINT [PK_PatientMaster] PRIMARY KEY CLUSTERED 
(
	[PaitentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [PatientMasterDesc] UNIQUE NONCLUSTERED 
(
	[AccountLedger] ASC,
	[ShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_PADDING OFF

/****** Object:  Table [HOS].[WardMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

CREATE TABLE [HOS].[WardMaster](
	[WId] [int] NOT NULL,
	[WDesc] [nvarchar](80) NULL,
	[WShortName] [nvarchar](50) NULL,
	[DepartmentId] [int] NULL,
	[ChargeAmt] [decimal](18, 6) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_WardMaster] PRIMARY KEY CLUSTERED 
(
	[WId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [WardMasterDesc] UNIQUE NONCLUSTERED 
(
	[WDesc] ASC,
	[WShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


/****** Object:  View [AMS].[Company_Profiles]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON

 

ALTER TABLE [AMS].[Area] ADD  CONSTRAINT [DF_Area_Country]  DEFAULT (N'Nepal') FOR [Country]

ALTER TABLE [AMS].[Area] ADD  CONSTRAINT [DF_Area_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[Area] ADD  CONSTRAINT [DF_Area_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[BillOfMaterial_Master] ADD  CONSTRAINT [DF_BillOfMaterial_Master_Alt_Qty]  DEFAULT ((0)) FOR [AltQty]

ALTER TABLE [AMS].[BillOfMaterial_Master] ADD  CONSTRAINT [DF_BillOfMaterial_Master_Qty]  DEFAULT ((0)) FOR [Qty]

ALTER TABLE [AMS].[CostCenter] ADD  CONSTRAINT [DF_CostCenter_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[CostCenter] ADD  CONSTRAINT [DF_CostCenter_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[Department] ADD  CONSTRAINT [DF_Department_Dlevel]  DEFAULT (N'I') FOR [Dlevel]

ALTER TABLE [AMS].[Department] ADD  CONSTRAINT [DF_Department_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[Department] ADD  CONSTRAINT [DF_Department_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[Floor] ADD  CONSTRAINT [DF_Floor_Type]  DEFAULT ('1St') FOR [Type]

ALTER TABLE [AMS].[Floor] ADD  CONSTRAINT [DF_Floor_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[Floor] ADD  CONSTRAINT [DF_Floor_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_Commission]  DEFAULT ((0.00)) FOR [Commission]

ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_CRLimit]  DEFAULT ((0.00)) FOR [CRLimit]

ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_CrDays]  DEFAULT ((0)) FOR [CrDays]

ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_CrType]  DEFAULT (N'Ignore') FOR [CrType]

ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[MainArea] ADD  CONSTRAINT [DF_MainArea_MCountry]  DEFAULT (N'Nepal') FOR [MCountry]

ALTER TABLE [AMS].[MainArea] ADD  CONSTRAINT [DF_MainArea_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[MainArea] ADD  CONSTRAINT [DF_MainArea_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[MemberType] ADD  CONSTRAINT [DF_MemberType_Discount]  DEFAULT ((0)) FOR [Discount]

ALTER TABLE [AMS].[MemberType] ADD  CONSTRAINT [DF_MemberType_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[MemberType] ADD  CONSTRAINT [DF_MemberType_ActiveStatus]  DEFAULT ((1)) FOR [ActiveStatus]

ALTER TABLE [AMS].[Notes_Master] ADD  CONSTRAINT [DF_Notes_Master_Currency_Rate]  DEFAULT ((1)) FOR [Currency_Rate]

ALTER TABLE [AMS].[Notes_Master] ADD  CONSTRAINT [DF_Notes_Master_Audit_Lock]  DEFAULT ((0)) FOR [Audit_Lock]

ALTER TABLE [AMS].[Notes_Master] ADD  CONSTRAINT [DF_Notes_Master_PrintValue]  DEFAULT ((0)) FOR [PrintValue]

ALTER TABLE [AMS].[ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Rate]  DEFAULT ((0.00)) FOR [PT_Rate]

ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Profitability]  DEFAULT ((1)) FOR [PT_Profitability]

ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Supess]  DEFAULT ((1)) FOR [PT_Supess]

ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Status]  DEFAULT ((1)) FOR [PT_Status]

ALTER TABLE [AMS].[SeniorAgent] ADD  CONSTRAINT [DF_SeniorAgent_Comm]  DEFAULT ((0.00)) FOR [Comm]

ALTER TABLE [AMS].[SeniorAgent] ADD  CONSTRAINT [DF_SeniorAgent_Company_ID]  DEFAULT ((0.00)) FOR [Company_ID]

ALTER TABLE [AMS].[SeniorAgent] ADD  CONSTRAINT [DF_SeniorAgent_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[Subledger] ADD  CONSTRAINT [DF_Subledger_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[Subledger] ADD  CONSTRAINT [DF_Subledger_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[TableMaster] ADD  CONSTRAINT [DF_TableMaster_Status]  DEFAULT ((1)) FOR [Status]

ALTER TABLE [AMS].[TableMaster] ADD  CONSTRAINT [DF_TableMaster_EnterBy]  DEFAULT ('MrSolution') FOR [EnterBy]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Branch]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_CompanyUnit] FOREIGN KEY([CmpUnit_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_CompanyUnit]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Currency] FOREIGN KEY([Currency_ID])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Currency]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Department] FOREIGN KEY([Department_ID1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Department]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_FiscalYear]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_GeneralLedgerCash] FOREIGN KEY([CbLedger_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_GeneralLedgerCash]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_GeneralLedgerId] FOREIGN KEY([Ledger_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_GeneralLedgerId]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_JuniorAgent] FOREIGN KEY([Agent_ID])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_JuniorAgent]

ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Subledger] FOREIGN KEY([Subleder_ID])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Subledger]

ALTER TABLE [AMS].[AccountGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[AccountGroup] CHECK CONSTRAINT [FK_AccountGroup_Branch]

ALTER TABLE [AMS].[AccountGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountGroup_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[AccountGroup] CHECK CONSTRAINT [FK_AccountGroup_CompanyUnit]

ALTER TABLE [AMS].[AccountSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountSubGroup_AccountGroup] FOREIGN KEY([GrpID])
REFERENCES [AMS].[AccountGroup] ([GrpId])

ALTER TABLE [AMS].[AccountSubGroup] CHECK CONSTRAINT [FK_AccountSubGroup_AccountGroup]

ALTER TABLE [AMS].[AccountSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountSubGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[AccountSubGroup] CHECK CONSTRAINT [FK_AccountSubGroup_Branch]

ALTER TABLE [AMS].[AccountSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountSubGroup_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[AccountSubGroup] CHECK CONSTRAINT [FK_AccountSubGroup_CompanyUnit]

ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_Branch]

ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_CompanyUnit] FOREIGN KEY([AreaId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_CompanyUnit]

ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_MainArea] FOREIGN KEY([Main_Area])
REFERENCES [AMS].[MainArea] ([MAreaId])

ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_MainArea]

ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_BillOfMaterial_Master] FOREIGN KEY([MemoNo])
REFERENCES [AMS].[BillOfMaterial_Master] ([MemoNo])

ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_BillOfMaterial_Master]

ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_CostCenter]

ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_down] FOREIGN KEY([GdnId])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_down]

ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_Product] FOREIGN KEY([ProductId])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_Product]

ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_ProductAltUnitId] FOREIGN KEY([AltUnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_ProductAltUnitId]

ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_ProductUnit] FOREIGN KEY([UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_ProductUnit]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_Branch]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_CompanyUnit]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_CostCenter]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_down] FOREIGN KEY([GdnId])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_down]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_Product] FOREIGN KEY([FGProductId])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_Product]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_ProductAltUnitId] FOREIGN KEY([AltUnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_ProductAltUnitId]

ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_ProductUnitId] FOREIGN KEY([UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_ProductUnitId]

ALTER TABLE [AMS].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Department] FOREIGN KEY([Dep1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[Budget] CHECK CONSTRAINT [FK_Budget_Department]

ALTER TABLE [AMS].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Budget] CHECK CONSTRAINT [FK_Budget_GeneralLedger]

ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_CB_Master] FOREIGN KEY([Voucher_No])
REFERENCES [AMS].[CB_Master] ([Voucher_No])

ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_CB_Master]

ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_Department]

ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_GeneralLedger]

ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_JuniorAgent]

ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_Subledger]

ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_Branch]

ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_CompanyUnit]

ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_Currency]

ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_Department]

ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_FiscalYear]

ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_GeneralLedger]

ALTER TABLE [AMS].[CompanyUnit]  WITH CHECK ADD  CONSTRAINT [FK_CompanyUnit_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[CompanyUnit] CHECK CONSTRAINT [FK_CompanyUnit_Branch]

ALTER TABLE [AMS].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_CostCenter_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[CostCenter] CHECK CONSTRAINT [FK_CostCenter_Branch]

ALTER TABLE [AMS].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_CostCenter_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[CostCenter] CHECK CONSTRAINT [FK_CostCenter_CompanyUnit]

ALTER TABLE [AMS].[CostCenterExpenses_Details]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Details_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[CostCenterExpenses_Details] CHECK CONSTRAINT [FK_CostCenterExpenses_Details_CostCenter]

ALTER TABLE [AMS].[CostCenterExpenses_Details]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Details_CostCenterExpenses_Master] FOREIGN KEY([CostingNo])
REFERENCES [AMS].[CostCenterExpenses_Master] ([CostingNo])

ALTER TABLE [AMS].[CostCenterExpenses_Details] CHECK CONSTRAINT [FK_CostCenterExpenses_Details_CostCenterExpenses_Master]

ALTER TABLE [AMS].[CostCenterExpenses_Details]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Details_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[CostCenterExpenses_Details] CHECK CONSTRAINT [FK_CostCenterExpenses_Details_GeneralLedger]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Branch]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_CompanyUnit]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_CostCenter]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Department]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_FiscalYear]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_down] FOREIGN KEY([GdnId])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_down]

ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Product] FOREIGN KEY([FGProductId])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Product]

ALTER TABLE [AMS].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Counter] CHECK CONSTRAINT [FK_Counter_Branch]

ALTER TABLE [AMS].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Counter] CHECK CONSTRAINT [FK_Counter_CompanyUnit]

ALTER TABLE [AMS].[Currency]  WITH CHECK ADD  CONSTRAINT [FK_Currency_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Currency] CHECK CONSTRAINT [FK_Currency_Branch]

ALTER TABLE [AMS].[Currency]  WITH CHECK ADD  CONSTRAINT [FK_Currency_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Currency] CHECK CONSTRAINT [FK_Currency_CompanyUnit]

ALTER TABLE [AMS].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Department] CHECK CONSTRAINT [FK_Department_Branch]

ALTER TABLE [AMS].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Department] CHECK CONSTRAINT [FK_Department_CompanyUnit]

ALTER TABLE [AMS].[DocumentDesignPrint]  WITH CHECK ADD  CONSTRAINT [FK_DocumentDesignPrint_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[DocumentDesignPrint] CHECK CONSTRAINT [FK_DocumentDesignPrint_Branch]

ALTER TABLE [AMS].[DocumentDesignPrint]  WITH CHECK ADD  CONSTRAINT [FK_DocumentDesignPrint_CompanyUnit] FOREIGN KEY([CmpUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[DocumentDesignPrint] CHECK CONSTRAINT [FK_DocumentDesignPrint_CompanyUnit]

ALTER TABLE [AMS].[DocumentNumbering]  WITH CHECK ADD  CONSTRAINT [FK_DocumentNumbering_Branch] FOREIGN KEY([DocBranch])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[DocumentNumbering] CHECK CONSTRAINT [FK_DocumentNumbering_Branch]

ALTER TABLE [AMS].[DocumentNumbering]  WITH CHECK ADD  CONSTRAINT [FK_DocumentNumbering_CompanyUnit] FOREIGN KEY([DocUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[DocumentNumbering] CHECK CONSTRAINT [FK_DocumentNumbering_CompanyUnit]

ALTER TABLE [AMS].[DocumentNumbering]  WITH CHECK ADD  CONSTRAINT [FK_DocumentNumbering_DocumentNumbering] FOREIGN KEY([DocId], [DocModule], [DocDesc])
REFERENCES [AMS].[DocumentNumbering] ([DocId], [DocModule], [DocDesc])

ALTER TABLE [AMS].[DocumentNumbering] CHECK CONSTRAINT [FK_DocumentNumbering_DocumentNumbering]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Branch]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_CompanyUnit]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_CostCenter] FOREIGN KEY([CC_Id])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_CostCenter]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department1] FOREIGN KEY([Cls2])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department1]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department2] FOREIGN KEY([Cls3])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department2]

ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department3] FOREIGN KEY([Cls4])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department3]

ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger] FOREIGN KEY([ProfiLossId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger]

ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger1] FOREIGN KEY([CashId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger1]

ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger2] FOREIGN KEY([VATLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger2]

ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger3] FOREIGN KEY([PDCBankLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger3]

ALTER TABLE [AMS].[Floor]  WITH CHECK ADD  CONSTRAINT [FK_Floor_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Floor] CHECK CONSTRAINT [FK_Floor_Branch]

ALTER TABLE [AMS].[Floor]  WITH CHECK ADD  CONSTRAINT [FK_Floor_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Floor] CHECK CONSTRAINT [FK_Floor_CompanyUnit]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AccountGroup] FOREIGN KEY([GrpId])
REFERENCES [AMS].[AccountGroup] ([GrpId])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AccountGroup]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AccountSubGroup] FOREIGN KEY([SubGrpId])
REFERENCES [AMS].[AccountSubGroup] ([SubGrpId])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AccountSubGroup]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_Area] FOREIGN KEY([AreaId])
REFERENCES [AMS].[Area] ([AreaId])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_Area]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_Branch]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_CompanyUnit]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_Currency] FOREIGN KEY([CurrId])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_Currency]

ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_JuniorAgent] FOREIGN KEY([AgentId])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_JuniorAgent]

ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_Department]

ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_GT_Master] FOREIGN KEY([VoucherNo])
REFERENCES [AMS].[GT_Master] ([VoucherNo])

ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_GT_Master]

ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_Product] FOREIGN KEY([ProId])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_Product]

ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_ProductAltUnit] FOREIGN KEY([AltUOM])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_ProductAltUnit]

ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_ProductUnit] FOREIGN KEY([UOM])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_ProductUnit]

ALTER TABLE [AMS].[GT_Master]  WITH CHECK ADD  CONSTRAINT [FK_GT_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[GT_Master] CHECK CONSTRAINT [FK_GT_Master_Branch]

ALTER TABLE [AMS].[GT_Master]  WITH CHECK ADD  CONSTRAINT [FK_GT_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[GT_Master] CHECK CONSTRAINT [FK_GT_Master_Department]

ALTER TABLE [AMS].[GT_Master]  WITH CHECK ADD  CONSTRAINT [FK_GT_Master_down] FOREIGN KEY([FrmGdn])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[GT_Master] CHECK CONSTRAINT [FK_GT_Master_down]

ALTER TABLE [AMS].[InventorySetting]  WITH CHECK ADD  CONSTRAINT [FK_InventorySetting_GeneralLedger] FOREIGN KEY([OPLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[InventorySetting] CHECK CONSTRAINT [FK_InventorySetting_GeneralLedger]

ALTER TABLE [AMS].[InventorySetting]  WITH CHECK ADD  CONSTRAINT [FK_InventorySetting_GeneralLedger1] FOREIGN KEY([CSBSLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[InventorySetting] CHECK CONSTRAINT [FK_InventorySetting_GeneralLedger1]

ALTER TABLE [AMS].[InventorySetting]  WITH CHECK ADD  CONSTRAINT [FK_InventorySetting_GeneralLedger2] FOREIGN KEY([CSPLLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[InventorySetting] CHECK CONSTRAINT [FK_InventorySetting_GeneralLedger2]

ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_Branch]

ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_CompanyUnit]

ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_GeneralLedger] FOREIGN KEY([GLCode])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_GeneralLedger]

ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_SeniorAgent] FOREIGN KEY([SAgent])
REFERENCES [AMS].[SeniorAgent] ([SAgentId])

ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_SeniorAgent]

ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_Department]

ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_GeneralLedger]

ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_GeneralLedgerCB] FOREIGN KEY([CBLedger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_GeneralLedgerCB]

ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_JuniorAgent]

ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_JV_Master] FOREIGN KEY([Voucher_No])
REFERENCES [AMS].[JV_Master] ([Voucher_No])

ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_JV_Master]

ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_Subledger]

ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_Branch]

ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_CompanyUnit]

ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_Currency]

ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_Department]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Branch]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_CompanyUnit]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Currency]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Department] FOREIGN KEY([Cls1])
REFERENCES [HOS].[Department] ([DId])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Department]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_FiscalYear]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_GeneralLedger]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_JuniorAgent]

ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Subledger]

ALTER TABLE [AMS].[MainArea]  WITH CHECK ADD  CONSTRAINT [FK_MainArea_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[MainArea] CHECK CONSTRAINT [FK_MainArea_Branch]

ALTER TABLE [AMS].[MainArea]  WITH CHECK ADD  CONSTRAINT [FK_MainArea_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[MainArea] CHECK CONSTRAINT [FK_MainArea_CompanyUnit]

ALTER TABLE [AMS].[MemberShipSetup]  WITH CHECK ADD  CONSTRAINT [FK_MemberShipSetup_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[MemberShipSetup] CHECK CONSTRAINT [FK_MemberShipSetup_Branch]

ALTER TABLE [AMS].[MemberShipSetup]  WITH CHECK ADD  CONSTRAINT [FK_MemberShipSetup_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[MemberShipSetup] CHECK CONSTRAINT [FK_MemberShipSetup_CompanyUnit]

ALTER TABLE [AMS].[MemberType]  WITH CHECK ADD  CONSTRAINT [FK_MemberType_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[MemberType] CHECK CONSTRAINT [FK_MemberType_Branch]

ALTER TABLE [AMS].[MemberType]  WITH CHECK ADD  CONSTRAINT [FK_MemberType_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[MemberType] CHECK CONSTRAINT [FK_MemberType_CompanyUnit]

ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_Department]

ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_GeneralLedger]

ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_GeneralLedgerVatLedger] FOREIGN KEY([VatLedger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_GeneralLedgerVatLedger]

ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_JuniorAgent]

ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_Notes_Master] FOREIGN KEY([Voucher_No])
REFERENCES [AMS].[Notes_Master] ([Voucher_No])

ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_Notes_Master]

ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_Subledger]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Branch]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_CompanyUnit]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Currency]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Department]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_FiscalYear]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_GeneralLedger]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_JuniorAgent]

ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Subledger]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_GeneralLedger]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_GeneralLedger1] FOREIGN KEY([CBLedger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_GeneralLedger1]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_JuniorAgent]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_PAB_Master] FOREIGN KEY([PAB_Invoice])
REFERENCES [AMS].[PAB_Master] ([PAB_Invoice])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_PAB_Master]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_Product]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_PT_Term]

ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_Subledger]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_Branch]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_CompanyUnit]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_Currency]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_Department]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_FiscalYear]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_JuniorAgent]

ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_PB_Master] FOREIGN KEY([PB_Invoice])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])

ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_PB_Master]

ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_down]

ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_PB_Master] FOREIGN KEY([PB_Invoice])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])

ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_PB_Master]

ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_Product]

ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductAltUnitId] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductAltUnitId]

ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductUnit]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Branch]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_CompanyUnit]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Counter] FOREIGN KEY([Counter_ID])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Counter]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Currency]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Department]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_FiscalYear]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_GeneralLedger]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_JuniorAgent]

ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Subledger]

ALTER TABLE [AMS].[PB_OtherMaster]  WITH CHECK ADD  CONSTRAINT [FK_PB_OtherMaster_PB_Master] FOREIGN KEY([PAB_Invoice])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])

ALTER TABLE [AMS].[PB_OtherMaster] CHECK CONSTRAINT [FK_PB_OtherMaster_PB_Master]

ALTER TABLE [AMS].[PB_Term]  WITH CHECK ADD  CONSTRAINT [FK_PB_Term_PB_Master] FOREIGN KEY([PB_VNo])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])

ALTER TABLE [AMS].[PB_Term] CHECK CONSTRAINT [FK_PB_Term_PB_Master]

ALTER TABLE [AMS].[PB_Term]  WITH CHECK ADD  CONSTRAINT [FK_PB_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PB_Term] CHECK CONSTRAINT [FK_PB_Term_Product]

ALTER TABLE [AMS].[PB_Term]  WITH CHECK ADD  CONSTRAINT [FK_PB_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PB_Term] CHECK CONSTRAINT [FK_PB_Term_PT_Term]

ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_PBT_Master] FOREIGN KEY([PBT_Invoice])
REFERENCES [AMS].[PBT_Master] ([PBT_Invoice])

ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_PBT_Master]

ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_Product]

ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_ProductGroup] FOREIGN KEY([PGrp_Id])
REFERENCES [AMS].[ProductGroup] ([PGrpID])

ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_ProductGroup]

ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_Subledger] FOREIGN KEY([Slb_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_Subledger]

ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_Branch]

ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_CompanyUnit]

ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_Currency]

ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_GeneralLedger] FOREIGN KEY([Vendor_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_GeneralLedger]

ALTER TABLE [AMS].[PBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PBT_Term] CHECK CONSTRAINT [FK_PBT_Term_Product]

ALTER TABLE [AMS].[PBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PBT_Term] CHECK CONSTRAINT [FK_PBT_Term_PT_Term]

ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_down]

ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_PC_Master] FOREIGN KEY([PC_Invoice])
REFERENCES [AMS].[PC_Master] ([PC_Invoice])

ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_PC_Master]

ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_Product]

ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductAltUnitId] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductAltUnitId]

ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductUnit]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Branch]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_CompanyUnit]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Counter] FOREIGN KEY([Counter_ID])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Counter]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Currency]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Department]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_FiscalYear]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_GeneralLedger]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_JuniorAgent]

ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Subledger]

ALTER TABLE [AMS].[PC_Term]  WITH CHECK ADD  CONSTRAINT [FK_PC_Term_PC_Master] FOREIGN KEY([PC_VNo])
REFERENCES [AMS].[PC_Master] ([PC_Invoice])

ALTER TABLE [AMS].[PC_Term] CHECK CONSTRAINT [FK_PC_Term_PC_Master]

ALTER TABLE [AMS].[PC_Term]  WITH CHECK ADD  CONSTRAINT [FK_PC_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PC_Term] CHECK CONSTRAINT [FK_PC_Term_Product]

ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_down]

ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_PIN_Master] FOREIGN KEY([PIN_Invoice])
REFERENCES [AMS].[PIN_Master] ([PIN_Invoice])

ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_PIN_Master]

ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_Product]

ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_ProductUnit] FOREIGN KEY([Alt_Unit])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_ProductUnit]

ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_ProductUnit1] FOREIGN KEY([Unit])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_ProductUnit1]

ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_Branch]

ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_CompanyUnit]

ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_Department]

ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_FiscalYear]

ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_Subledger] FOREIGN KEY([Sub_Ledger])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_Subledger]

ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_down]

ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_PO_Master] FOREIGN KEY([PO_Invoice])
REFERENCES [AMS].[PO_Master] ([PO_Invoice])

ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_PO_Master]

ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_Product]

ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_ProductUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_ProductUnit]

ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_ProductUnit1]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Branch]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_CompanyUnit]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_CompanyUnit1] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_CompanyUnit1]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Currency]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Department]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_FiscalYear]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_GeneralLedger]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_JuniorAgent]

ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Subledger]

ALTER TABLE [AMS].[PO_Term]  WITH CHECK ADD  CONSTRAINT [FK_PO_Term_PO_Master] FOREIGN KEY([PO_VNo])
REFERENCES [AMS].[PO_Master] ([PO_Invoice])

ALTER TABLE [AMS].[PO_Term] CHECK CONSTRAINT [FK_PO_Term_PO_Master]

ALTER TABLE [AMS].[PO_Term]  WITH CHECK ADD  CONSTRAINT [FK_PO_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PO_Term] CHECK CONSTRAINT [FK_PO_Term_Product]

ALTER TABLE [AMS].[PO_Term]  WITH CHECK ADD  CONSTRAINT [FK_PO_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PO_Term] CHECK CONSTRAINT [FK_PO_Term_PT_Term]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_Branch]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_CompanyUnit]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_Department]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_FiscalYear]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_GeneralLedger]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_JuniorAgent] FOREIGN KEY([AgentId])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_JuniorAgent]

ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_Subledger] FOREIGN KEY([SubLedgerId])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_Subledger]

ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_down]

ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_PR_Master] FOREIGN KEY([PR_Invoice])
REFERENCES [AMS].[PR_Master] ([PR_Invoice])

ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_PR_Master]

ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_Product]

ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_ProductUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_ProductUnit]

ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_ProductUnit1]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Branch]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_CompanyUnit]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Currency]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Department]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_FiscalYear]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_GeneralLedger]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_JuniorAgent]

ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Subledger]

ALTER TABLE [AMS].[PR_Term]  WITH CHECK ADD  CONSTRAINT [FK_PR_Term_PR_Master] FOREIGN KEY([PR_VNo])
REFERENCES [AMS].[PR_Master] ([PR_Invoice])

ALTER TABLE [AMS].[PR_Term] CHECK CONSTRAINT [FK_PR_Term_PR_Master]

ALTER TABLE [AMS].[PR_Term]  WITH CHECK ADD  CONSTRAINT [FK_PR_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[PR_Term] CHECK CONSTRAINT [FK_PR_Term_Product]

ALTER TABLE [AMS].[PR_Term]  WITH CHECK ADD  CONSTRAINT [FK_PR_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PR_Term] CHECK CONSTRAINT [FK_PR_Term_PT_Term]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Branch]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_CompanyUnit] FOREIGN KEY([CmpUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_CompanyUnit]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department] FOREIGN KEY([CmpId])
REFERENCES [HOS].[Department] ([DId])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedger] FOREIGN KEY([PSR])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedger]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerPurchase] FOREIGN KEY([PPL])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerPurchase]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerPurchaseReturn] FOREIGN KEY([PPR])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerPurchaseReturn]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerSales] FOREIGN KEY([PSL])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerSales]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerSalesReturn] FOREIGN KEY([PSR])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerSalesReturn]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductAltUnit] FOREIGN KEY([PAltUnit])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductAltUnit]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY([PGrpId])
REFERENCES [AMS].[ProductGroup] ([PGrpID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductGroup]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductSubGroup] FOREIGN KEY([PSubGrpId])
REFERENCES [AMS].[ProductSubGroup] ([PSubGrpId])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductSubGroup]

ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductUnit] FOREIGN KEY([PUnit])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductUnit]

ALTER TABLE [AMS].[ProductClosingRate]  WITH CHECK ADD  CONSTRAINT [FK_ProductClosingRate_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ProductClosingRate] CHECK CONSTRAINT [FK_ProductClosingRate_Branch]

ALTER TABLE [AMS].[ProductClosingRate]  WITH CHECK ADD  CONSTRAINT [FK_ProductClosingRate_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ProductClosingRate] CHECK CONSTRAINT [FK_ProductClosingRate_CompanyUnit]

ALTER TABLE [AMS].[ProductClosingRate]  WITH CHECK ADD  CONSTRAINT [FK_ProductClosingRate_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[ProductClosingRate] CHECK CONSTRAINT [FK_ProductClosingRate_Product]

ALTER TABLE [AMS].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_Branch]

ALTER TABLE [AMS].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_CompanyUnit]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Branch]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_CompanyUnit]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Currency]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Department]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_FiscalYear]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_down] FOREIGN KEY([down_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_down]

ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Product]

ALTER TABLE [AMS].[ProductSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ProductSubGroup] CHECK CONSTRAINT [FK_ProductSubGroup_Branch]

ALTER TABLE [AMS].[ProductSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubGroup_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ProductSubGroup] CHECK CONSTRAINT [FK_ProductSubGroup_CompanyUnit]

ALTER TABLE [AMS].[ProductSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubGroup_ProductGroup] FOREIGN KEY([GrpId])
REFERENCES [AMS].[ProductGroup] ([PGrpID])

ALTER TABLE [AMS].[ProductSubGroup] CHECK CONSTRAINT [FK_ProductSubGroup_ProductGroup]

ALTER TABLE [AMS].[ProductUnit]  WITH CHECK ADD  CONSTRAINT [FK_ProductUnit_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ProductUnit] CHECK CONSTRAINT [FK_ProductUnit_Branch]

ALTER TABLE [AMS].[ProductUnit]  WITH CHECK ADD  CONSTRAINT [FK_ProductUnit_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ProductUnit] CHECK CONSTRAINT [FK_ProductUnit_CompanyUnit]

ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_Branch]

ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_CompanyUnit]

ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_Currency]

ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_Department]

ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_FiscalYear]

ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_GeneralLedger]

ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_Department]

ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_GeneralLedger]

ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_JuniorAgent] FOREIGN KEY([AgentId])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_JuniorAgent]

ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_ProvCB_Master] FOREIGN KEY([VoucherNumber])
REFERENCES [AMS].[ProvCB_Master] ([VoucherNumber])

ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_ProvCB_Master]

ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_Subledger] FOREIGN KEY([SubledgerId])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_Subledger]

ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_Branch]

ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_CompanyUnit] FOREIGN KEY([CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_CompanyUnit]

ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_Currency] FOREIGN KEY([CurrId])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_Currency]

ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_Department]

ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_FiscalYear]

ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_GeneralLedger]

ALTER TABLE [AMS].[PRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PRT_Term_Branch] FOREIGN KEY([PT_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PRT_Term] CHECK CONSTRAINT [FK_PRT_Term_Branch]

ALTER TABLE [AMS].[PRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PRT_Term_CompanyUnit] FOREIGN KEY([PT_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PRT_Term] CHECK CONSTRAINT [FK_PRT_Term_CompanyUnit]

ALTER TABLE [AMS].[PRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PRT_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PRT_Term] CHECK CONSTRAINT [FK_PRT_Term_GeneralLedger]

ALTER TABLE [AMS].[PT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PT_Term_Branch] FOREIGN KEY([PT_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[PT_Term] CHECK CONSTRAINT [FK_PT_Term_Branch]

ALTER TABLE [AMS].[PT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PT_Term_CompanyUnit] FOREIGN KEY([PT_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[PT_Term] CHECK CONSTRAINT [FK_PT_Term_CompanyUnit]

ALTER TABLE [AMS].[PT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PT_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PT_Term] CHECK CONSTRAINT [FK_PT_Term_GeneralLedger]

ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_GeneralLedger] FOREIGN KEY([PBLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_GeneralLedger]

ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_GeneralLedger1] FOREIGN KEY([PRLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_GeneralLedger1]

ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term] FOREIGN KEY([PBVatTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term]

ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term1] FOREIGN KEY([PBDiscountTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term1]

ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term2] FOREIGN KEY([PBProductDiscountTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term2]

ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term3] FOREIGN KEY([PBAdditionalTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term3]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_GeneralLedger] FOREIGN KEY([SBLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_GeneralLedger]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_GeneralLedger1] FOREIGN KEY([SRLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_GeneralLedger1]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term] FOREIGN KEY([SBVatTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term1] FOREIGN KEY([SBDiscountTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term1]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term2] FOREIGN KEY([SBAdditionalTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term2]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term3] FOREIGN KEY([SBProductDiscountTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term3]

ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term4] FOREIGN KEY([SBServiceCharge])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term4]

ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_CostCenter] FOREIGN KEY([CC_Id])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_CostCenter]

ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_down]

ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_Product]

ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_ProductUnit] FOREIGN KEY([AltUnit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_ProductUnit]

ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_ProductUnit1]

ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_SampleCosting_Master] FOREIGN KEY([Costing_No])
REFERENCES [AMS].[SampleCosting_Master] ([Costing_No])

ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_SampleCosting_Master]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Branch]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_CompanyUnit]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_CostCenter] FOREIGN KEY([CC_Id])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_CostCenter]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Department]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_FiscalYear]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_down]

ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Product] FOREIGN KEY([FGProduct_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Product]

ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_down]

ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_Product]

ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_ProductUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_ProductUnit]

ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_ProductUnit1]

ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])

ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_SB_Master]

ALTER TABLE [AMS].[SB_ExchangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_ExchangeDetails_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SB_ExchangeDetails] CHECK CONSTRAINT [FK_SB_ExchangeDetails_down]

ALTER TABLE [AMS].[SB_ExchangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_ExchangeDetails_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SB_ExchangeDetails] CHECK CONSTRAINT [FK_SB_ExchangeDetails_Product]

ALTER TABLE [AMS].[SB_ExchangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_ExchangeDetails_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])

ALTER TABLE [AMS].[SB_ExchangeDetails] CHECK CONSTRAINT [FK_SB_ExchangeDetails_SB_Master]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Branch]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_CompanyUnit]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Counter]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Currency]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Department]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_FiscalYear]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_GeneralLedger]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_JuniorAgent]

ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Subledger]

ALTER TABLE [AMS].[SB_Master_OtherDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_OtherDetails_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])

ALTER TABLE [AMS].[SB_Master_OtherDetails] CHECK CONSTRAINT [FK_SB_Master_OtherDetails_SB_Master]

ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_Product]

ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_SB_Master] FOREIGN KEY([SB_VNo])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])

ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_SB_Master]

ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_ST_Term]

ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_Product]

ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_ProductGroup] FOREIGN KEY([PGrp_Id])
REFERENCES [AMS].[ProductGroup] ([PGrpID])

ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_ProductGroup]

ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_SBT_Master] FOREIGN KEY([SBT_Invoice])
REFERENCES [AMS].[SBT_Master] ([SBT_Invoice])

ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_SBT_Master]

ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_Subledger] FOREIGN KEY([Slb_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_Subledger]

ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_Branch]

ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_CompanyUnit]

ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_Currency]

ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_FiscalYear]

ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_GeneralLedger]

ALTER TABLE [AMS].[SBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SBT_Term] CHECK CONSTRAINT [FK_SBT_Term_Product]

ALTER TABLE [AMS].[SBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Term_SBT_Master] FOREIGN KEY([SBT_VNo])
REFERENCES [AMS].[SBT_Master] ([SBT_Invoice])

ALTER TABLE [AMS].[SBT_Term] CHECK CONSTRAINT [FK_SBT_Term_SBT_Master]

ALTER TABLE [AMS].[SBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SBT_Term] CHECK CONSTRAINT [FK_SBT_Term_ST_Term]

ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_down]

ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_Product]

ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_ProductUnit]

ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_ProductUnit1] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_ProductUnit1]

ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_SC_Master] FOREIGN KEY([SC_Invoice])
REFERENCES [AMS].[SC_Master] ([SC_Invoice])

ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_SC_Master]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Branch]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_CompanyUnit]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Counter]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Currency]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Department]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_FiscalYear]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_GeneralLedger]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_JuniorAgent]

ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Subledger]

ALTER TABLE [AMS].[SC_Term]  WITH CHECK ADD  CONSTRAINT [FK_SC_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SC_Term] CHECK CONSTRAINT [FK_SC_Term_Product]

ALTER TABLE [AMS].[SC_Term]  WITH CHECK ADD  CONSTRAINT [FK_SC_Term_SC_Master] FOREIGN KEY([SC_Vno])
REFERENCES [AMS].[SC_Master] ([SC_Invoice])

ALTER TABLE [AMS].[SC_Term] CHECK CONSTRAINT [FK_SC_Term_SC_Master]

ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_Product] FOREIGN KEY([P_Code])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_Product]

ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_ProductGroup] FOREIGN KEY([P_Group])
REFERENCES [AMS].[ProductGroup] ([PGrpID])

ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_ProductGroup]

ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_ProductSubGroup] FOREIGN KEY([P_SubGroup])
REFERENCES [AMS].[ProductSubGroup] ([PSubGrpId])

ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_ProductSubGroup]

ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_Scheme_Master] FOREIGN KEY([Scheme_Id])
REFERENCES [AMS].[Scheme_Master] ([Scheme_Id])

ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_Scheme_Master]

ALTER TABLE [AMS].[Scheme_Master]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Scheme_Master] CHECK CONSTRAINT [FK_Scheme_Master_Branch]

ALTER TABLE [AMS].[Scheme_Master]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Scheme_Master] CHECK CONSTRAINT [FK_Scheme_Master_CompanyUnit]

ALTER TABLE [AMS].[Scheme_Master]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[Scheme_Master] CHECK CONSTRAINT [FK_Scheme_Master_FiscalYear]

ALTER TABLE [AMS].[SeniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_SeniorAgent_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SeniorAgent] CHECK CONSTRAINT [FK_SeniorAgent_Branch]

ALTER TABLE [AMS].[SeniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_SeniorAgent_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SeniorAgent] CHECK CONSTRAINT [FK_SeniorAgent_CompanyUnit]

ALTER TABLE [AMS].[SeniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_SeniorAgent_GeneralLedger] FOREIGN KEY([GLID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SeniorAgent] CHECK CONSTRAINT [FK_SeniorAgent_GeneralLedger]

ALTER TABLE [AMS].[SO_Details]  WITH CHECK ADD  CONSTRAINT [FK_SO_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SO_Details] CHECK CONSTRAINT [FK_SO_Details_down]

ALTER TABLE [AMS].[SO_Details]  WITH CHECK ADD  CONSTRAINT [FK_SO_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SO_Details] CHECK CONSTRAINT [FK_SO_Details_Product]

ALTER TABLE [AMS].[SO_Details]  WITH CHECK ADD  CONSTRAINT [FK_SO_Details_SO_Master] FOREIGN KEY([SO_Invoice])
REFERENCES [AMS].[SO_Master] ([SO_Invoice])

ALTER TABLE [AMS].[SO_Details] CHECK CONSTRAINT [FK_SO_Details_SO_Master]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Branch]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_CompanyUnit]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Counter]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Currency]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [HOS].[Department] ([DId])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Department]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_FiscalYear]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_GeneralLedger]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_JuniorAgent]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Subledger]

ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_TableMaster] FOREIGN KEY([TableId])
REFERENCES [AMS].[TableMaster] ([TableId])

ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_TableMaster]

ALTER TABLE [AMS].[SO_Master_OtherDetails]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_OtherDetails_SO_Master] FOREIGN KEY([SO_Invoice])
REFERENCES [AMS].[SO_Master] ([SO_Invoice])

ALTER TABLE [AMS].[SO_Master_OtherDetails] CHECK CONSTRAINT [FK_SO_Master_OtherDetails_SO_Master]

ALTER TABLE [AMS].[SO_Term]  WITH CHECK ADD  CONSTRAINT [FK_SO_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SO_Term] CHECK CONSTRAINT [FK_SO_Term_Product]

ALTER TABLE [AMS].[SO_Term]  WITH CHECK ADD  CONSTRAINT [FK_SO_Term_SO_Master] FOREIGN KEY([SO_Vno])
REFERENCES [AMS].[SO_Master] ([SO_Invoice])

ALTER TABLE [AMS].[SO_Term] CHECK CONSTRAINT [FK_SO_Term_SO_Master]

ALTER TABLE [AMS].[SO_Term]  WITH CHECK ADD  CONSTRAINT [FK_SO_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SO_Term] CHECK CONSTRAINT [FK_SO_Term_ST_Term]

ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_down]

ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_Product]

ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_ProductAltUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_ProductAltUnit]

ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_ProductUnit]

ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_SQ_Master] FOREIGN KEY([SQ_Invoice])
REFERENCES [AMS].[SQ_Master] ([SQ_Invoice])

ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_SQ_Master]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Branch]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_CompanyUnit]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Currency]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [HOS].[Department] ([DId])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Department]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_FiscalYear]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_GeneralLedger]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_JuniorAgent]

ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Subledger]

ALTER TABLE [AMS].[SQ_Term]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SQ_Term] CHECK CONSTRAINT [FK_SQ_Term_Product]

ALTER TABLE [AMS].[SQ_Term]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Term_SQ_Master] FOREIGN KEY([SQ_Vno])
REFERENCES [AMS].[SQ_Master] ([SQ_Invoice])

ALTER TABLE [AMS].[SQ_Term] CHECK CONSTRAINT [FK_SQ_Term_SQ_Master]

ALTER TABLE [AMS].[SQ_Term]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SQ_Term] CHECK CONSTRAINT [FK_SQ_Term_ST_Term]

ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_down] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_down]

ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_Product]

ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_ProductUnit]

ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_ProductUnit1] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_ProductUnit1]

ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_SR_Master] FOREIGN KEY([SR_Invoice])
REFERENCES [AMS].[SR_Master] ([SR_Invoice])

ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_SR_Master]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Branch]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_CompanyUnit]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Counter]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Currency]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Department]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_FiscalYear]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_GeneralLedger] FOREIGN KEY([Customer_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_GeneralLedger]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_JuniorAgent]

ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Subledger]

ALTER TABLE [AMS].[SR_Term]  WITH CHECK ADD  CONSTRAINT [FK_SR_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[SR_Term] CHECK CONSTRAINT [FK_SR_Term_Product]

ALTER TABLE [AMS].[SR_Term]  WITH CHECK ADD  CONSTRAINT [FK_SR_Term_SR_Master] FOREIGN KEY([SR_VNo])
REFERENCES [AMS].[SR_Master] ([SR_Invoice])

ALTER TABLE [AMS].[SR_Term] CHECK CONSTRAINT [FK_SR_Term_SR_Master]

ALTER TABLE [AMS].[SR_Term]  WITH CHECK ADD  CONSTRAINT [FK_SR_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SR_Term] CHECK CONSTRAINT [FK_SR_Term_ST_Term]

ALTER TABLE [AMS].[SRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SRT_Term_Branch] FOREIGN KEY([ST_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[SRT_Term] CHECK CONSTRAINT [FK_SRT_Term_Branch]

ALTER TABLE [AMS].[SRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SRT_Term_CompanyUnit] FOREIGN KEY([ST_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[SRT_Term] CHECK CONSTRAINT [FK_SRT_Term_CompanyUnit]

ALTER TABLE [AMS].[SRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SRT_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SRT_Term] CHECK CONSTRAINT [FK_SRT_Term_GeneralLedger]

ALTER TABLE [AMS].[ST_Term]  WITH CHECK ADD  CONSTRAINT [FK_ST_Term_Branch] FOREIGN KEY([ST_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[ST_Term] CHECK CONSTRAINT [FK_ST_Term_Branch]

ALTER TABLE [AMS].[ST_Term]  WITH CHECK ADD  CONSTRAINT [FK_ST_Term_CompanyUnit] FOREIGN KEY([ST_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[ST_Term] CHECK CONSTRAINT [FK_ST_Term_CompanyUnit]

ALTER TABLE [AMS].[ST_Term]  WITH CHECK ADD  CONSTRAINT [FK_ST_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[ST_Term] CHECK CONSTRAINT [FK_ST_Term_GeneralLedger]

ALTER TABLE [AMS].[STA_Details]  WITH CHECK ADD  CONSTRAINT [FK_STA_Details_down] FOREIGN KEY([downId])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[STA_Details] CHECK CONSTRAINT [FK_STA_Details_down]

ALTER TABLE [AMS].[STA_Details]  WITH CHECK ADD  CONSTRAINT [FK_STA_Details_Product] FOREIGN KEY([ProductId])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[STA_Details] CHECK CONSTRAINT [FK_STA_Details_Product]

ALTER TABLE [AMS].[STA_Details]  WITH CHECK ADD  CONSTRAINT [FK_STA_Details_STA_Master] FOREIGN KEY([StockAdjust_No])
REFERENCES [AMS].[STA_Master] ([StockAdjust_No])

ALTER TABLE [AMS].[STA_Details] CHECK CONSTRAINT [FK_STA_Details_STA_Master]

ALTER TABLE [AMS].[STA_Master]  WITH CHECK ADD  CONSTRAINT [FK_STA_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[STA_Master] CHECK CONSTRAINT [FK_STA_Master_Branch]

ALTER TABLE [AMS].[STA_Master]  WITH CHECK ADD  CONSTRAINT [FK_STA_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[STA_Master] CHECK CONSTRAINT [FK_STA_Master_CompanyUnit]

ALTER TABLE [AMS].[STA_Master]  WITH CHECK ADD  CONSTRAINT [FK_STA_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[STA_Master] CHECK CONSTRAINT [FK_STA_Master_FiscalYear]

ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_Branch]

ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_CompanyUnit]

ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_FiscalYear]

ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_Product]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Branch]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_CompanyUnit] FOREIGN KEY([CmpUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_CompanyUnit]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_CostCenter] FOREIGN KEY([CostCenter_Id])
REFERENCES [AMS].[CostCenter] ([CCId])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_CostCenter]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Counter] FOREIGN KEY([Counter_Id])
REFERENCES [AMS].[Counter] ([CId])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Counter]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Currency]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Department] FOREIGN KEY([Department_Id1])
REFERENCES [AMS].[Department] ([DId])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Department]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_FiscalYear]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_GeneralLedger]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_down] FOREIGN KEY([down_Id])
REFERENCES [AMS].[down] ([GID])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_down]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_JuniorAgent]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Product]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_ProductAltUnit] FOREIGN KEY([AltUnit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_ProductAltUnit]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_ProductUnit]

ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])

ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Subledger]

ALTER TABLE [AMS].[Subledger]  WITH CHECK ADD  CONSTRAINT [FK_Subledger_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[Subledger] CHECK CONSTRAINT [FK_Subledger_Branch]

ALTER TABLE [AMS].[Subledger]  WITH CHECK ADD  CONSTRAINT [FK_Subledger_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[Subledger] CHECK CONSTRAINT [FK_Subledger_CompanyUnit]

ALTER TABLE [AMS].[Subledger]  WITH CHECK ADD  CONSTRAINT [FK_Subledger_GeneralLedger] FOREIGN KEY([GLID])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[Subledger] CHECK CONSTRAINT [FK_Subledger_GeneralLedger]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_CashLedger] FOREIGN KEY([Cash_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_CashLedger]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_ClosingStockBS] FOREIGN KEY([ClosingStockBS_Ac])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_ClosingStockBS]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_ClosingStockPL] FOREIGN KEY([ClosingStockPL_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_ClosingStockPL]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Currency]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_FiscalYear] FOREIGN KEY([FY_Id])
REFERENCES [AMS].[FiscalYear] ([FY_Id])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_FiscalYear]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_OpeningStock] FOREIGN KEY([OpeningStockPL_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_OpeningStock]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PDCBankLedger] FOREIGN KEY([PDCBank_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PDCBankLedger]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_ProfitLoss] FOREIGN KEY([PL_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_ProfitLoss]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurAddVat] FOREIGN KEY([PurchaseAddVat_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurAddVat]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Purchase] FOREIGN KEY([Purchase_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Purchase]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurchaseReturn] FOREIGN KEY([PurchaseReturn_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurchaseReturn]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurDiscount] FOREIGN KEY([PurchaseDiscount_ID])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurDiscount]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurProDiscount] FOREIGN KEY([PurchaseProDiscount_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurProDiscount]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurVat] FOREIGN KEY([PurchaseVat_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurVat]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Sales] FOREIGN KEY([Sales_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Sales]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesDiscount] FOREIGN KEY([SalesDiscount_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesDiscount]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesReturn] FOREIGN KEY([SalesReturn_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesReturn]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesServiceCharge] FOREIGN KEY([SalesServiceCharge_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesServiceCharge]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesSpecialDiscount] FOREIGN KEY([SalesSpecialDiscount_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesSpecialDiscount]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesVat] FOREIGN KEY([SalesVat_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesVat]

ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_VatLedger] FOREIGN KEY([Vat_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_VatLedger]

ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_AccountGroup] FOREIGN KEY([DebtorsGroupId])
REFERENCES [AMS].[AccountGroup] ([GrpId])

ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_AccountGroup]

ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_AccountGroup1] FOREIGN KEY([CreditorGroupId])
REFERENCES [AMS].[AccountGroup] ([GrpId])

ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_AccountGroup1]

ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_GeneralLedger] FOREIGN KEY([PFLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_GeneralLedger]

ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_GeneralLedger1] FOREIGN KEY([SalaryLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_GeneralLedger1]

ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_GeneralLedger2] FOREIGN KEY([TDSLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])

ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_GeneralLedger2]

ALTER TABLE [AMS].[TableMaster]  WITH CHECK ADD  CONSTRAINT [FK_TableMaster_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[TableMaster] CHECK CONSTRAINT [FK_TableMaster_Branch]

ALTER TABLE [AMS].[TableMaster]  WITH CHECK ADD  CONSTRAINT [FK_TableMaster_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[TableMaster] CHECK CONSTRAINT [FK_TableMaster_CompanyUnit]

ALTER TABLE [AMS].[TicketRefund]  WITH CHECK ADD  CONSTRAINT [FK_TicketRefund_TicketRefund] FOREIGN KEY([VoucherNo])
REFERENCES [AMS].[TicketRefund] ([VoucherNo])

ALTER TABLE [AMS].[TicketRefund] CHECK CONSTRAINT [FK_TicketRefund_TicketRefund]

ALTER TABLE [AMS].[VehicleColors]  WITH CHECK ADD  CONSTRAINT [FK_VechileColors_Branch] FOREIGN KEY([VHColorsBranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[VehicleColors] CHECK CONSTRAINT [FK_VechileColors_Branch]

ALTER TABLE [AMS].[VehicleColors]  WITH CHECK ADD  CONSTRAINT [FK_VechileColors_CompanyUnit] FOREIGN KEY([VHColorsCompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[VehicleColors] CHECK CONSTRAINT [FK_VechileColors_CompanyUnit]

ALTER TABLE [AMS].[VehicleNumber]  WITH CHECK ADD  CONSTRAINT [FK_VechileNumber_Branch] FOREIGN KEY([VNBranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[VehicleNumber] CHECK CONSTRAINT [FK_VechileNumber_Branch]

ALTER TABLE [AMS].[VehicleNumber]  WITH CHECK ADD  CONSTRAINT [FK_VechileNumber_CompanyUnit] FOREIGN KEY([VNCompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[VehicleNumber] CHECK CONSTRAINT [FK_VechileNumber_CompanyUnit]

ALTER TABLE [AMS].[VehileModel]  WITH CHECK ADD  CONSTRAINT [FK_VehileModel_Branch] FOREIGN KEY([VHModelBranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])

ALTER TABLE [AMS].[VehileModel] CHECK CONSTRAINT [FK_VehileModel_Branch]

ALTER TABLE [AMS].[VehileModel]  WITH CHECK ADD  CONSTRAINT [FK_VehileModel_CompanyUnit] FOREIGN KEY([VHModelCompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])

ALTER TABLE [AMS].[VehileModel] CHECK CONSTRAINT [FK_VehileModel_CompanyUnit]

/****** Object:  StoredProcedure [AMS].[Usp_IUD_Branch]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON

SET QUOTED_IDENTIFIER ON


