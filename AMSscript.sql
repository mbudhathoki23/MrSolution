USE [AMS_MRSO01]
GO
/****** Object:  Schema [AMS]    Script Date: 23/02/2018 2:06:38 PM ******/
CREATE SCHEMA [AMS]
GO
/****** Object:  Table [AMS].[AccountDetails]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AccountDetails](
	[Module] [nvarchar](5) NOT NULL,
	[Serial_No] [int] NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](10) NOT NULL,
	[Voucher_Time] [datetime] NULL,
	[Ledger_ID] [bigint] NOT NULL,
	[CbLedger_ID] [bigint] NULL,
	[Subleder_ID] [bigint] NULL,
	[Agent_ID] [bigint] NULL,
	[Department_ID1] [int] NULL,
	[Department_ID2] [int] NULL,
	[Department_ID3] [int] NULL,
	[Department_ID4] [int] NULL,
	[Currency_ID] [int] NULL,
	[Currency_Rate] [decimal](18, 8) NULL,
	[Debit_Amt] [decimal](18, 8) NOT NULL,
	[Credit_Amt] [decimal](18, 8) NOT NULL,
	[LocalDebit_Amt] [decimal](18, 8) NOT NULL,
	[LocalCredit_Amt] [decimal](18, 8) NOT NULL,
	[DueDate] [datetime] NULL,
	[DueDays] [int] NULL,
	[Narration] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NULL,
	[RefNo] [nvarchar](50) NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Authorize_By] [varchar](50) NULL,
	[Authorize_Date] [datetime] NULL,
	[Clearing_By] [nvarchar](50) NULL,
	[Clearing_Date] [datetime] NULL,
	[Posted_By] [varchar](50) NULL,
	[Posted_Date] [datetime] NULL,
	[Cheque_No] [nvarchar](50) NULL,
	[Cheque_Date] [datetime] NULL,
	[PartyName] [nvarchar](50) NULL,
	[PartyLedger_Id] [bigint] NULL,
	[Party_PanNo] [nvarchar](50) NULL,
	[Branch_ID] [bigint] NOT NULL,
	[CmpUnit_ID] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AccountGroup]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AccountGroup](
	[GrpId] [bigint] NOT NULL,
	[GrpName] [nvarchar](150) NOT NULL,
	[GrpCode] [nvarchar](50) NOT NULL,
	[Schedule] [int] NULL,
	[PrimaryGrp] [nvarchar](50) NOT NULL,
	[GrpType] [nvarchar](50) NOT NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_Id] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_AccountGroup] PRIMARY KEY CLUSTERED 
(
	[GrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountGroup] UNIQUE NONCLUSTERED 
(
	[GrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountGroup_1] UNIQUE NONCLUSTERED 
(
	[GrpCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AccountSubGroup]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AccountSubGroup](
	[SubGrpId] [bigint] NOT NULL,
	[SubGrpName] [nvarchar](80) NOT NULL,
	[GrpID] [bigint] NOT NULL,
	[SubGrpCode] [nvarchar](50) NOT NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_AccountSubGroup] PRIMARY KEY CLUSTERED 
(
	[SubGrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountSubGroup] UNIQUE NONCLUSTERED 
(
	[SubGrpCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__AccountS__B6E65C097B5B524B] UNIQUE NONCLUSTERED 
(
	[SubGrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Area]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Area](
	[AreaId] [bigint] NOT NULL,
	[AreaName] [nvarchar](80) NOT NULL,
	[AreaCode] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[Main_Area] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Area] UNIQUE NONCLUSTERED 
(
	[AreaName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Area_1] UNIQUE NONCLUSTERED 
(
	[AreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Branch]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Branch](
	[Branch_Id] [bigint] NOT NULL,
	[Branch_Name] [varchar](100) NOT NULL,
	[Branch_Code] [varchar](10) NOT NULL,
	[Reg_Date] [datetime] NULL,
	[Address] [varchar](255) NULL,
	[Country] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[PhoneNo] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[ContactPerson] [varchar](50) NULL,
	[ContactPersonAdd] [varchar](255) NULL,
	[ContactPersonPhone] [varchar](50) NULL,
	[Created_By] [smallint] NULL,
	[Created_Date] [datetime] NULL,
	[Modify_By] [smallint] NULL,
	[Modify_Date] [datetime] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Branch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Branch] UNIQUE NONCLUSTERED 
(
	[Branch_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Branch_1] UNIQUE NONCLUSTERED 
(
	[Branch_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[CompanyInfo]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[CompanyInfo](
	[Company_Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Company_Name] [varchar](255) NOT NULL,
	[Company_Logo] [image] NULL,
	[CReg_Date] [datetime] NULL,
	[Address] [varchar](255) NULL,
	[Country] [varchar](255) NULL,
	[State] [varchar](255) NULL,
	[City] [varchar](255) NULL,
	[PhoneNo] [varchar](50) NULL,
	[Fax] [varchar](50) NULL,
	[Pan_No] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Website] [nvarchar](255) NULL,
	[Database_Name] [nvarchar](255) NULL,
	[Database_Path] [nvarchar](255) NULL,
	[Version_No] [tinyint] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Company_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyInfo] UNIQUE NONCLUSTERED 
(
	[Company_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyInfo_1] UNIQUE NONCLUSTERED 
(
	[Database_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[CompanyUnit]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CompanyUnit](
	[CmpUnit_Id] [bigint] NOT NULL,
	[CmpUnit_Name] [nvarchar](80) NOT NULL,
	[CmpUnit_Code] [nvarchar](50) NOT NULL,
	[Reg_Date] [datetime] NOT NULL,
	[Address] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](90) NULL,
	[ContactPerson] [nvarchar](50) NULL,
	[ContactPersonAdd] [nvarchar](50) NULL,
	[ContactPersonPhone] [nvarchar](50) NULL,
	[Branch_Id] [bigint] NOT NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
 CONSTRAINT [PK_CompanyUnit] PRIMARY KEY CLUSTERED 
(
	[CmpUnit_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyUnit] UNIQUE NONCLUSTERED 
(
	[CmpUnit_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyUnit_1] UNIQUE NONCLUSTERED 
(
	[CmpUnit_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CostCenter]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CostCenter](
	[CCId] [bigint] NOT NULL,
	[CCName] [nvarchar](50) NOT NULL,
	[CCcode] [nvarchar](50) NOT NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_Id] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED 
(
	[CCId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CostCenter] UNIQUE NONCLUSTERED 
(
	[CCName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CostCenter_1] UNIQUE NONCLUSTERED 
(
	[CCcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Counter]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Counter](
	[CId] [int] NOT NULL,
	[CName] [nvarchar](50) NOT NULL,
	[CCode] [nvarchar](50) NOT NULL,
	[Branch_ID] [bigint] NULL,
	[Company_ID] [bigint] NOT NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED 
(
	[CId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Counter__66C1C08232E0915F] UNIQUE NONCLUSTERED 
(
	[CCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Counter__85D445AA300424B4] UNIQUE NONCLUSTERED 
(
	[CName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Currency]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Currency](
	[CId] [int] NOT NULL,
	[CName] [nvarchar](50) NOT NULL,
	[Ccode] [nvarchar](50) NOT NULL,
	[CRate] [numeric](18, 8) NULL,
	[Branch_Id] [bigint] NOT NULL,
	[Company_Id] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[CId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Currency__1D67C6F81A14E395] UNIQUE NONCLUSTERED 
(
	[Ccode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Currency__85D445AA173876EA] UNIQUE NONCLUSTERED 
(
	[CName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[DateMiti]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[DateMiti](
	[Date_Id] [bigint] NOT NULL,
	[BS_Date] [varchar](10) NULL,
	[BS_DateDMY] [varchar](10) NULL,
	[AD_Date] [datetime] NULL,
	[BS_Months] [varchar](50) NULL,
	[AD_Months] [varchar](50) NULL,
	[Days] [varchar](50) NULL,
	[Is_Holiday] [bit] NULL,
	[Holiday] [varchar](1024) NULL,
 CONSTRAINT [IX_DateMiti] UNIQUE NONCLUSTERED 
(
	[AD_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DateMiti_1] UNIQUE NONCLUSTERED 
(
	[BS_DateDMY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DateMiti_2] UNIQUE NONCLUSTERED 
(
	[AD_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Department]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Department](
	[DId] [int] NOT NULL,
	[DName] [nvarchar](50) NOT NULL,
	[DCode] [nvarchar](50) NOT NULL,
	[Dlevel] [nvarchar](50) NOT NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Departme__48779E0B37A5467C] UNIQUE NONCLUSTERED 
(
	[DName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Departme__7AB5BC783A81B327] UNIQUE NONCLUSTERED 
(
	[DCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[DocumentNumbering]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[DocumentNumbering](
	[DocId] [int] NOT NULL,
	[DocModule] [nvarchar](50) NOT NULL,
	[DocUser] [nvarchar](50) NOT NULL,
	[DocDesc] [nvarchar](50) NOT NULL,
	[DocStartDate] [date] NULL,
	[DocStartMiti] [nvarchar](50) NULL,
	[DocEndDate] [date] NULL,
	[DocEndMiti] [nvarchar](50) NULL,
	[DocType] [char](10) NULL,
	[DocPrefix] [nvarchar](50) NULL,
	[DocSufix] [nvarchar](50) NULL,
	[DocBodyLength] [numeric](18, 0) NOT NULL,
	[DocTotalLength] [numeric](18, 0) NULL,
	[DocBlank] [char](10) NULL,
	[DocBlankCh] [numeric](18, 0) NULL,
	[DocBranch] [bigint] NOT NULL,
	[DocUnit] [bigint] NULL,
	[DocStart] [numeric](18, 0) NULL,
	[DocCurr] [numeric](18, 0) NULL,
	[DocEnd] [numeric](18, 0) NULL,
	[DocDesign] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_DocumentNumbering_1] PRIMARY KEY CLUSTERED 
(
	[DocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Document__4E97401A1ED998B2] UNIQUE NONCLUSTERED 
(
	[DocDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[FiscalYear]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[FiscalYear](
	[FY_Id] [int] IDENTITY(1,1) NOT NULL,
	[AD_FY] [varchar](5) NULL,
	[BS_FY] [varchar](5) NOT NULL,
	[Current_FY] [bit] NULL,
	[Start_ADDate] [datetime] NULL,
	[End_ADDate] [datetime] NULL,
	[Start_BSDate] [varchar](10) NULL,
	[End_BSDate] [varchar](10) NULL,
 CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED 
(
	[FY_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYear] UNIQUE NONCLUSTERED 
(
	[AD_FY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYear_1] UNIQUE NONCLUSTERED 
(
	[BS_FY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYear_2] UNIQUE NONCLUSTERED 
(
	[End_ADDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYear_3] UNIQUE NONCLUSTERED 
(
	[End_BSDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYear_4] UNIQUE NONCLUSTERED 
(
	[Start_ADDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYear_5] UNIQUE NONCLUSTERED 
(
	[Start_BSDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[GeneralLedger]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[GeneralLedger](
	[GLID] [bigint] NOT NULL,
	[GLName] [nvarchar](80) NOT NULL,
	[GLCode] [nvarchar](50) NOT NULL,
	[ACCode] [nvarchar](50) NOT NULL,
	[GLType] [nvarchar](50) NOT NULL,
	[GrpId] [bigint] NOT NULL,
	[SubGrpId] [bigint] NULL,
	[PanNo] [nvarchar](50) NULL,
	[AreaId] [bigint] NULL,
	[AgentId] [bigint] NULL,
	[CurrId] [int] NOT NULL,
	[CrDays] [numeric](18, 0) NULL,
	[CrLimit] [decimal](18, 6) NULL,
	[CrTYpe] [nvarchar](50) NULL,
	[IntRate] [numeric](18, 8) NULL,
	[GLAddress] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LandLineNo] [nvarchar](50) NULL,
	[OwnerName] [nvarchar](50) NULL,
	[OwnerNumber] [nvarchar](50) NULL,
	[Scheme] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Branch_id] [bigint] NOT NULL,
	[Company_Id] [bigint] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_GeneralLedger] PRIMARY KEY CLUSTERED 
(
	[GLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__GeneralL__3A2536D4239E4DCF] UNIQUE NONCLUSTERED 
(
	[GLName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__GeneralL__70F04E65267ABA7A] UNIQUE NONCLUSTERED 
(
	[GLCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Godown]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Godown](
	[GID] [bigint] NOT NULL,
	[GName] [nvarchar](80) NOT NULL,
	[GCode] [nvarchar](50) NOT NULL,
	[GLocation] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[CompUnit] [bigint] NULL,
	[BranchUnit] [bigint] NOT NULL,
 CONSTRAINT [PK_Godown] PRIMARY KEY CLUSTERED 
(
	[GID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Godown] UNIQUE NONCLUSTERED 
(
	[GName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Godown_1] UNIQUE NONCLUSTERED 
(
	[GCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[JuniorAgent]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[JuniorAgent](
	[AgentId] [bigint] NOT NULL,
	[AgentName] [nvarchar](80) NOT NULL,
	[AgentCode] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[GLCode] [bigint] NULL,
	[Commission] [decimal](18, 6) NULL,
	[SAgent] [int] NULL,
	[Branch_id] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Email] [nvarchar](50) NULL,
	[CRLimit] [numeric](18, 8) NULL,
	[CrDays] [nvarchar](50) NULL,
	[CrType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[AgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Agent] UNIQUE NONCLUSTERED 
(
	[AgentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Agent__B265ECF51273C1CD] UNIQUE NONCLUSTERED 
(
	[AgentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[MainArea]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[MainArea](
	[MAreaId] [int] NOT NULL,
	[MAreaName] [nvarchar](80) NOT NULL,
	[MAreaCode] [nvarchar](50) NOT NULL,
	[MCountry] [nvarchar](50) NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_MainArea] PRIMARY KEY CLUSTERED 
(
	[MAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_MainArea] UNIQUE NONCLUSTERED 
(
	[MAreaName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_MainArea_1] UNIQUE NONCLUSTERED 
(
	[MAreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PB_Details]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PB_Details](
	[PB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [bigint] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [bigint] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [bigint] NULL,
	[Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](500) NULL,
	[PO_Invoice] [nvarchar](50) NULL,
	[PO_Sno] [numeric](18, 0) NULL,
	[PC_Invoice] [nvarchar](50) NULL,
	[PC_SNo] [nvarchar](50) NULL,
	[Tax_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[V_Rate] [decimal](18, 6) NULL,
	[Free_Unit_Id] [bigint] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [bigint] NULL,
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
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PB_Master]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PB_Master](
	[PB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NULL,
	[PB_Vno] [nvarchar](50) NULL,
	[Vno_Date] [datetime] NULL,
	[Vno_Miti] [nvarchar](10) NULL,
	[Vendor_ID] [bigint] NOT NULL,
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
	[Agent_Id] [bigint] NULL,
	[Subledger_Id] [bigint] NULL,
	[PO_Invoice] [nvarchar](250) NULL,
	[PO_Date] [datetime] NULL,
	[PC_Invoice] [nvarchar](250) NULL,
	[PC_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 8) NOT NULL,
	[B_Amount] [decimal](18, 8) NOT NULL,
	[T_Amount] [decimal](18, 8) NOT NULL,
	[N_Amount] [decimal](18, 0) NOT NULL,
	[LN_Amount] [decimal](18, 0) NOT NULL,
	[V_Amount] [decimal](18, 0) NULL,
	[Tbl_Amount] [decimal](18, 0) NOT NULL,
	[Action_type] [nvarchar](50) NOT NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NOT NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](500) NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[CUnit_Id] [bigint] NOT NULL,
	[CBranch_Id] [bigint] NOT NULL,
 CONSTRAINT [PK_PB] PRIMARY KEY CLUSTERED 
(
	[PB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PB_Term]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PB_Term](
	[PB_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NOT NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PO_Master]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PO_Master](
	[PO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [datetime] NOT NULL,
	[Invoice_Time] [datetime] NULL,
	[PB_Vno] [nvarchar](50) NULL,
	[Vno_Date] [datetime] NULL,
	[Vno_Miti] [datetime] NULL,
	[Vendor_ID] [bigint] NOT NULL,
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
	[Agent_Id] [bigint] NULL,
	[Subledger_Id] [bigint] NULL,
	[PIN_Invoice] [nvarchar](250) NULL,
	[PIN_Date] [datetime] NULL,
	[PQO_Invoice] [nvarchar](250) NULL,
	[PQO_Date] [datetime] NULL,
	[Cls1] [bigint] NULL,
	[Cls2] [bigint] NULL,
	[Cls3] [bigint] NULL,
	[Cls4] [bigint] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 8) NOT NULL,
	[CUnit_Id] [bigint] NULL,
	[CBranch_Id] [bigint] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[B_Amount] [decimal](18, 8) NOT NULL,
	[T_Amount] [decimal](18, 8) NOT NULL,
	[N_Amount] [decimal](18, 0) NOT NULL,
	[LN_Amount] [decimal](18, 0) NOT NULL,
	[V_Amount] [decimal](18, 0) NULL,
	[Tbl_Amount] [decimal](18, 0) NOT NULL,
	[Action_type] [nvarchar](50) NOT NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NOT NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](500) NULL,
	[Audit_Lock] [bit] NULL,
	[Entry_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
 CONSTRAINT [PK_PO] PRIMARY KEY CLUSTERED 
(
	[PO_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PO_Master] UNIQUE NONCLUSTERED 
(
	[PB_Vno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Product]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Product](
	[PID] [bigint] NOT NULL,
	[PName] [nvarchar](200) NOT NULL,
	[PAlias] [nvarchar](200) NOT NULL,
	[PShortName] [nvarchar](50) NOT NULL,
	[PType] [nvarchar](50) NOT NULL,
	[PUnit] [bigint] NULL,
	[PAltUnit] [bigint] NULL,
	[PQtyConv] [numeric](18, 8) NULL,
	[PAltConv] [numeric](18, 8) NULL,
	[PValTech] [nvarchar](50) NULL,
	[PSerialno] [bit] NULL,
	[PSizewise] [bit] NULL,
	[PBatchwise] [bit] NULL,
	[PBuyRate] [numeric](18, 8) NULL,
	[PSalesRate] [numeric](18, 8) NULL,
	[PMargin1] [numeric](18, 8) NULL,
	[TradeRate] [numeric](18, 8) NULL,
	[PMargin2] [numeric](18, 8) NULL,
	[PMRP] [numeric](18, 8) NULL,
	[PGrpId] [bigint] NULL,
	[PSubGrpId] [bigint] NULL,
	[PTax] [numeric](18, 0) NULL,
	[PMin] [numeric](18, 8) NULL,
	[PMax] [numeric](18, 8) NULL,
	[CmpId] [int] NULL,
	[CmpId1] [int] NULL,
	[CmpId2] [int] NULL,
	[CmpId3] [int] NULL,
	[Branch_Id] [bigint] NOT NULL,
	[CmpUnit_Id] [bigint] NULL,
	[PPL] [bigint] NULL,
	[PPR] [bigint] NULL,
	[PSL] [bigint] NULL,
	[PSR] [bigint] NULL,
	[PImage] [image] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Product] UNIQUE NONCLUSTERED 
(
	[PName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Product_1] UNIQUE NONCLUSTERED 
(
	[PShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ProductGroup]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[ProductGroup](
	[PGrpID] [bigint] NOT NULL,
	[GrpName] [nvarchar](50) NOT NULL,
	[GrpCode] [nvarchar](50) NOT NULL,
	[GMargin] [numeric](18, 8) NULL,
	[Gprinter] [nvarchar](50) NULL,
	[Branch_ID] [bigint] NULL,
	[Company_ID] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED 
(
	[PGrpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductGroup] UNIQUE NONCLUSTERED 
(
	[GrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductGroup_1] UNIQUE NONCLUSTERED 
(
	[GrpCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ProductSubGroup]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[ProductSubGroup](
	[PSubGrpId] [bigint] NOT NULL,
	[SubGrpName] [nvarchar](80) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[GrpId] [bigint] NOT NULL,
	[Branch_ID] [bigint] NULL,
	[Company_ID] [bigint] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ProductSubGroup] PRIMARY KEY CLUSTERED 
(
	[PSubGrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductSubGroup] UNIQUE NONCLUSTERED 
(
	[ShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductSubGroup_1] UNIQUE NONCLUSTERED 
(
	[SubGrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ProductUnit]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[ProductUnit](
	[UID] [bigint] NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
	[UnitCode] [nvarchar](50) NOT NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ProductUnit] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductUnit] UNIQUE NONCLUSTERED 
(
	[UnitCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductUnit_1] UNIQUE NONCLUSTERED 
(
	[UnitName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PT_Term]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PT_Term](
	[PT_ID] [int] NOT NULL,
	[Order_No] [int] NOT NULL,
	[Module] [char](2) NOT NULL,
	[PT_Name] [nvarchar](50) NOT NULL,
	[PT_Type] [char](2) NOT NULL,
	[Ledger] [bigint] NOT NULL,
	[PT_Basis] [char](2) NOT NULL,
	[PT_Sign] [char](1) NOT NULL,
	[PT_Condition] [char](1) NOT NULL,
	[PT_Rate] [decimal](18, 6) NULL,
	[PT_Branch] [bigint] NOT NULL,
	[PT_CompanyUnit] [bigint] NULL,
	[PT_Profitability] [bit] NULL,
	[PT_Supess] [bit] NULL,
	[PT_Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_PT_Term] PRIMARY KEY CLUSTERED 
(
	[PT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_PT_Term] UNIQUE NONCLUSTERED 
(
	[Order_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SB_Details]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[SB_Details](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [bigint] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [bigint] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [bigint] NULL,
	[Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
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
	[Free_Unit_Id] [bigint] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
	[StockFree_Qty] [decimal](18, 6) NULL,
	[ExtraFree_Unit_Id] [bigint] NULL,
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
	[Manu_Date] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[SB_Master]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[SB_Master](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [varchar](10) NOT NULL,
	[Invoice_Time] [datetime] NULL,
	[PB_Vno] [nvarchar](50) NULL,
	[Vno_Date] [datetime] NULL,
	[Vno_Miti] [datetime] NULL,
	[Customer_ID] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NOT NULL,
	[Invoice_Mode] [nvarchar](50) NOT NULL,
	[Payment_Mode] [varchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [bigint] NULL,
	[Subledger_Id] [bigint] NULL,
	[SO_Invoice] [nvarchar](250) NULL,
	[SO_Date] [datetime] NULL,
	[SC_Invoice] [nvarchar](250) NULL,
	[SC_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CounterId] [int] NULL,
	[Cur_Id] [int] NULL,
	[Cur_Rate] [decimal](18, 8) NULL,
	[B_Amount] [decimal](18, 8) NULL,
	[T_Amount] [decimal](18, 8) NULL,
	[N_Amount] [decimal](18, 0) NULL,
	[LN_Amount] [decimal](18, 0) NULL,
	[V_Amount] [decimal](18, 0) NULL,
	[Tbl_Amount] [decimal](18, 0) NULL,
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
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[CUnit_Id] [bigint] NOT NULL,
	[CBranch_Id] [bigint] NOT NULL,
 CONSTRAINT [PK_SB] PRIMARY KEY CLUSTERED 
(
	[SB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SB_Term]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[SB_Term](
	[SB_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SeniorAgent]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[SeniorAgent](
	[SAgentId] [int] NOT NULL,
	[SAgent] [nvarchar](50) NOT NULL,
	[SAgentCode] [nvarchar](50) NOT NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Comm] [decimal](18, 6) NULL,
	[GLID] [bigint] NULL,
	[Branch_id] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_SeniorAgent] PRIMARY KEY CLUSTERED 
(
	[SAgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SeniorAgent] UNIQUE NONCLUSTERED 
(
	[SAgentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_SeniorAgent_1] UNIQUE NONCLUSTERED 
(
	[SAgent] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ST_Term]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[ST_Term](
	[ST_ID] [int] NOT NULL,
	[Order_No] [int] NOT NULL,
	[Module] [char](2) NOT NULL,
	[ST_Name] [nvarchar](50) NOT NULL,
	[ST_Type] [char](2) NOT NULL,
	[Ledger] [bigint] NOT NULL,
	[ST_Basis] [char](2) NOT NULL,
	[ST_Sign] [char](1) NOT NULL,
	[ST_Condition] [char](1) NOT NULL,
	[ST_Rate] [decimal](18, 6) NULL,
	[ST_Branch] [bigint] NOT NULL,
	[ST_CompanyUnit] [bigint] NULL,
	[ST_Profitability] [bit] NULL,
	[ST_Supess] [bit] NULL,
	[ST_Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_ST_Term] PRIMARY KEY CLUSTERED 
(
	[ST_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ST_Term] UNIQUE NONCLUSTERED 
(
	[Order_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[StockDetails]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[StockDetails](
	[Module] [nvarchar](5) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Serial_No] [int] NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](10) NOT NULL,
	[Voucher_Time] [datetime] NULL,
	[Ledger_Id] [bigint] NULL,
	[Subledger_Id] [bigint] NULL,
	[Agent_Id] [bigint] NULL,
	[Department_Id1] [int] NULL,
	[Department_Id2] [int] NULL,
	[Department_Id3] [int] NULL,
	[Department_Id4] [int] NULL,
	[Currency_Id] [int] NOT NULL,
	[Currency_Rate] [decimal](18, 6) NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[Godown_Id] [bigint] NULL,
	[CostCenter_Id] [bigint] NULL,
	[AltQty] [decimal](18, 6) NULL,
	[AltUnit_Id] [bigint] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [bigint] NULL,
	[AltStockQty] [decimal](18, 6) NULL,
	[StockQty] [decimal](18, 6) NULL,
	[FreeQty] [decimal](18, 6) NULL,
	[FreeUnit_Id] [bigint] NULL,
	[StockFreeQty] [decimal](18, 6) NULL,
	[ConvRatio] [decimal](18, 6) NULL,
	[Rate] [decimal](18, 6) NULL,
	[BasicAmt] [decimal](18, 6) NULL,
	[TermAmt] [decimal](18, 6) NULL,
	[NetAmt] [decimal](18, 6) NULL,
	[TaxRate] [decimal](18, 6) NULL,
	[TaxableAmt] [decimal](18, 6) NULL,
	[DocVal] [decimal](18, 6) NULL,
	[ReturnVal] [decimal](18, 6) NULL,
	[StockVal] [decimal](18, 6) NULL,
	[PartyInv] [nvarchar](50) NULL,
	[EntryType] [char](1) NULL,
	[AuthBy] [nvarchar](50) NULL,
	[AuthDate] [datetime] NULL,
	[RecoBy] [nvarchar](50) NULL,
	[RecoDate] [datetime] NULL,
	[Counter_Id] [int] NULL,
	[RoomNo] [int] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Branch_Id] [bigint] NOT NULL,
	[CmpUnit_Id] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Subledger]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Subledger](
	[SLId] [bigint] NOT NULL,
	[SLName] [nvarchar](80) NOT NULL,
	[SLCode] [nvarchar](50) NOT NULL,
	[SLAddress] [nvarchar](50) NULL,
	[SLPhoneNo] [nvarchar](50) NULL,
	[GLID] [bigint] NULL,
	[Branch_ID] [bigint] NOT NULL,
	[Company_ID] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Subledger] PRIMARY KEY CLUSTERED 
(
	[SLId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Subledger] UNIQUE NONCLUSTERED 
(
	[SLName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Subledger_1] UNIQUE NONCLUSTERED 
(
	[SLCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[SystemConfiguration]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[SystemConfiguration](
	[SC_Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Date_Type] [char](1) NULL,
	[Audit_Trial] [bit] NULL,
	[Udf] [bit] NULL,
	[AutoPupup] [bit] NULL,
	[CurrentDate] [bit] NULL,
	[ConfirmSave] [bit] NULL,
	[ConfirmCancel] [bit] NULL,
	[Cur_Id] [int] NULL,
	[FY_Id] [int] NULL,
	[DefaultPrinter] [nvarchar](100) NULL,
	[BackupSch_IntvDays] [int] NULL,
	[Backup_Path] [varchar](255) NULL,
	[PL_AC] [bigint] NULL,
	[Cash_AC] [bigint] NULL,
	[Vat_AC] [bigint] NULL,
	[PDCBank_AC] [bigint] NULL,
	[Transby_Code] [bit] NULL,
	[Negative_Tran] [bit] NULL,
	[Amount_Format] [varchar](15) NULL,
	[Rate_Format] [varchar](15) NULL,
	[Qty_Format] [varchar](15) NULL,
	[AltQty_Format] [varchar](15) NULL,
	[Cur_Format] [varchar](15) NULL,
	[Font_Name] [varchar](555) NULL,
	[Font_Size] [decimal](18, 0) NULL,
	[Paper_Size] [varchar](15) NULL,
	[ReportFont_Style] [nvarchar](50) NULL,
	[Printing_DateTime] [bit] NULL,
	[Purchase_AC] [bigint] NULL,
	[PurchaseReturn_AC] [bigint] NULL,
	[PurchaseVat_Id] [int] NULL,
	[PurchaseAddVat_Id] [int] NULL,
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
	[SCancellationCustomer_Code] [bigint] NULL,
	[SCancellationProduct_Id] [bigint] NULL,
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
	[Godown_Category] [bit] NULL,
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
	[Godown_Wise_Filter] [bit] NULL,
	[Finished_Qty] [bit] NULL,
	[Equal_Qty] [bit] NULL,
	[IGodown_Wise_Filter] [bit] NULL,
	[Email_Id] [varchar](50) NULL,
	[Email_Password] [nvarchar](100) NULL,
 CONSTRAINT [PK_SystemConfiguration] PRIMARY KEY CLUSTERED 
(
	[SC_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SystemControlOptions]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[SystemControlOptions](
	[SCOptions_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Header] [varchar](50) NULL,
	[Options_Name] [varchar](50) NULL,
	[Enable] [bit] NULL,
	[Mandatory] [bit] NULL,
 CONSTRAINT [PK_SystemControlOptions] PRIMARY KEY CLUSTERED 
(
	[SCOptions_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Branch]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_CompanyUnit] FOREIGN KEY([CmpUnit_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_CompanyUnit]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Currency] FOREIGN KEY([Currency_ID])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Currency]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Department] FOREIGN KEY([Department_ID1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Department]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Department1] FOREIGN KEY([Department_ID2])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Department1]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Department2] FOREIGN KEY([Department_ID3])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Department2]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Department3] FOREIGN KEY([Department_ID4])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Department3]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_GeneralLedger] FOREIGN KEY([Ledger_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_GeneralLedger]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_GeneralLedger1] FOREIGN KEY([CbLedger_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_GeneralLedger1]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_JuniorAgent] FOREIGN KEY([Agent_ID])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_JuniorAgent]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_Subledger] FOREIGN KEY([Subleder_ID])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_Subledger]
GO
ALTER TABLE [AMS].[AccountGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[AccountGroup] CHECK CONSTRAINT [FK_AccountGroup_Branch]
GO
ALTER TABLE [AMS].[AccountGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountGroup_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[AccountGroup] CHECK CONSTRAINT [FK_AccountGroup_CompanyUnit]
GO
ALTER TABLE [AMS].[AccountSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountSubGroup_AccountGroup] FOREIGN KEY([GrpID])
REFERENCES [AMS].[AccountGroup] ([GrpId])
GO
ALTER TABLE [AMS].[AccountSubGroup] CHECK CONSTRAINT [FK_AccountSubGroup_AccountGroup]
GO
ALTER TABLE [AMS].[AccountSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountSubGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[AccountSubGroup] CHECK CONSTRAINT [FK_AccountSubGroup_Branch]
GO
ALTER TABLE [AMS].[AccountSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_AccountSubGroup_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[AccountSubGroup] CHECK CONSTRAINT [FK_AccountSubGroup_CompanyUnit]
GO
ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_Branch]
GO
ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_CompanyUnit]
GO
ALTER TABLE [AMS].[CompanyUnit]  WITH CHECK ADD  CONSTRAINT [FK_CompanyUnit_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[CompanyUnit] CHECK CONSTRAINT [FK_CompanyUnit_Branch]
GO
ALTER TABLE [AMS].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_CostCenter_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[CostCenter] CHECK CONSTRAINT [FK_CostCenter_Branch]
GO
ALTER TABLE [AMS].[CostCenter]  WITH CHECK ADD  CONSTRAINT [FK_CostCenter_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[CostCenter] CHECK CONSTRAINT [FK_CostCenter_CompanyUnit]
GO
ALTER TABLE [AMS].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Counter] CHECK CONSTRAINT [FK_Counter_Branch]
GO
ALTER TABLE [AMS].[Counter]  WITH CHECK ADD  CONSTRAINT [FK_Counter_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Counter] CHECK CONSTRAINT [FK_Counter_CompanyUnit]
GO
ALTER TABLE [AMS].[Currency]  WITH CHECK ADD  CONSTRAINT [FK_Currency_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Currency] CHECK CONSTRAINT [FK_Currency_Branch]
GO
ALTER TABLE [AMS].[Currency]  WITH CHECK ADD  CONSTRAINT [FK_Currency_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Currency] CHECK CONSTRAINT [FK_Currency_CompanyUnit]
GO
ALTER TABLE [AMS].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Department] CHECK CONSTRAINT [FK_Department_Branch]
GO
ALTER TABLE [AMS].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Department] CHECK CONSTRAINT [FK_Department_CompanyUnit]
GO
ALTER TABLE [AMS].[DocumentNumbering]  WITH CHECK ADD  CONSTRAINT [FK_DocumentNumbering_Branch] FOREIGN KEY([DocBranch])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[DocumentNumbering] CHECK CONSTRAINT [FK_DocumentNumbering_Branch]
GO
ALTER TABLE [AMS].[DocumentNumbering]  WITH CHECK ADD  CONSTRAINT [FK_DocumentNumbering_CompanyUnit] FOREIGN KEY([DocUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[DocumentNumbering] CHECK CONSTRAINT [FK_DocumentNumbering_CompanyUnit]
GO
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AccountGroup] FOREIGN KEY([GrpId])
REFERENCES [AMS].[AccountGroup] ([GrpId])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AccountGroup]
GO
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AccountSubGroup] FOREIGN KEY([SubGrpId])
REFERENCES [AMS].[AccountSubGroup] ([SubGrpId])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AccountSubGroup]
GO
ALTER TABLE [AMS].[Godown]  WITH CHECK ADD  CONSTRAINT [FK_Godown_Branch] FOREIGN KEY([BranchUnit])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Godown] CHECK CONSTRAINT [FK_Godown_Branch]
GO
ALTER TABLE [AMS].[Godown]  WITH CHECK ADD  CONSTRAINT [FK_Godown_CompanyUnit] FOREIGN KEY([CompUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Godown] CHECK CONSTRAINT [FK_Godown_CompanyUnit]
GO
ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_Agent_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_Agent_Branch]
GO
ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_Agent_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_Agent_CompanyUnit]
GO
ALTER TABLE [AMS].[MainArea]  WITH CHECK ADD  CONSTRAINT [FK_MainArea_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[MainArea] CHECK CONSTRAINT [FK_MainArea_Branch]
GO
ALTER TABLE [AMS].[MainArea]  WITH CHECK ADD  CONSTRAINT [FK_MainArea_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[MainArea] CHECK CONSTRAINT [FK_MainArea_CompanyUnit]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_Godown]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_PB_Master] FOREIGN KEY([PB_Invoice])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_PB_Master]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_Product]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductUnit]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductUnit1] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductUnit2] FOREIGN KEY([Free_Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductUnit2]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductUnit3] FOREIGN KEY([ExtraFree_Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductUnit3]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Currency]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Department]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Department1] FOREIGN KEY([Cls2])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Department1]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_GeneralLedger]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_JuniorAgent]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Branch]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Department] FOREIGN KEY([Cls3])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Department]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Department1] FOREIGN KEY([Cls4])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Department1]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Subledger]
GO
ALTER TABLE [AMS].[PB_Term]  WITH CHECK ADD  CONSTRAINT [FK_PB_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PB_Term] CHECK CONSTRAINT [FK_PB_Term_Product]
GO
ALTER TABLE [AMS].[PB_Term]  WITH CHECK ADD  CONSTRAINT [FK_PB_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PB_Term] CHECK CONSTRAINT [FK_PB_Term_PT_Term]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Branch]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Currency]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_AccountGroup] FOREIGN KEY([PGrpId])
REFERENCES [AMS].[AccountGroup] ([GrpId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_AccountGroup]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_AccountSubGroup] FOREIGN KEY([PSubGrpId])
REFERENCES [AMS].[AccountSubGroup] ([SubGrpId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_AccountSubGroup]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Branch]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_CompanyUnit] FOREIGN KEY([CmpUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_CompanyUnit]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department] FOREIGN KEY([CmpId])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department1] FOREIGN KEY([CmpId1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department1]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department2] FOREIGN KEY([CmpId2])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department2]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department3] FOREIGN KEY([CmpId3])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department3]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedger] FOREIGN KEY([PSL])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedger]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedger1] FOREIGN KEY([PPL])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedger1]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedger2] FOREIGN KEY([PPR])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedger2]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedger3] FOREIGN KEY([PSR])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedger3]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductUnit2] FOREIGN KEY([PUnit])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductUnit2]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductUnit3] FOREIGN KEY([PAltUnit])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductUnit3]
GO
ALTER TABLE [AMS].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_Branch]
GO
ALTER TABLE [AMS].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_CompanyUnit]
GO
ALTER TABLE [AMS].[ProductGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductGroup_ProductGroup] FOREIGN KEY([PGrpID])
REFERENCES [AMS].[ProductGroup] ([PGrpID])
GO
ALTER TABLE [AMS].[ProductGroup] CHECK CONSTRAINT [FK_ProductGroup_ProductGroup]
GO
ALTER TABLE [AMS].[ProductSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubGroup_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ProductSubGroup] CHECK CONSTRAINT [FK_ProductSubGroup_Branch]
GO
ALTER TABLE [AMS].[ProductSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubGroup_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ProductSubGroup] CHECK CONSTRAINT [FK_ProductSubGroup_CompanyUnit]
GO
ALTER TABLE [AMS].[ProductSubGroup]  WITH CHECK ADD  CONSTRAINT [FK_ProductSubGroup_ProductGroup] FOREIGN KEY([GrpId])
REFERENCES [AMS].[ProductGroup] ([PGrpID])
GO
ALTER TABLE [AMS].[ProductSubGroup] CHECK CONSTRAINT [FK_ProductSubGroup_ProductGroup]
GO
ALTER TABLE [AMS].[PT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PT_Term_Branch] FOREIGN KEY([PT_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PT_Term] CHECK CONSTRAINT [FK_PT_Term_Branch]
GO
ALTER TABLE [AMS].[PT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PT_Term_CompanyUnit] FOREIGN KEY([PT_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PT_Term] CHECK CONSTRAINT [FK_PT_Term_CompanyUnit]
GO
ALTER TABLE [AMS].[PT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PT_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PT_Term] CHECK CONSTRAINT [FK_PT_Term_GeneralLedger]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_Godown] FOREIGN KEY([ExtraFree_Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_Godown]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_Product]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_ProductUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_ProductUnit]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_ProductUnit2] FOREIGN KEY([Free_Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_ProductUnit2]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_SB_Master]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Branch]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Counter]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Currency]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Department]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Department1] FOREIGN KEY([Cls2])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Department1]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Department2] FOREIGN KEY([Cls3])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Department2]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Department3] FOREIGN KEY([Cls4])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Department3]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_GeneralLedger] FOREIGN KEY([Customer_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_Subledger]
GO
ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_Product]
GO
ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_ST_Term]
GO
ALTER TABLE [AMS].[SeniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_SeniorAgent_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SeniorAgent] CHECK CONSTRAINT [FK_SeniorAgent_Branch]
GO
ALTER TABLE [AMS].[SeniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_SeniorAgent_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SeniorAgent] CHECK CONSTRAINT [FK_SeniorAgent_CompanyUnit]
GO
ALTER TABLE [AMS].[ST_Term]  WITH CHECK ADD  CONSTRAINT [FK_ST_Term_Branch] FOREIGN KEY([ST_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ST_Term] CHECK CONSTRAINT [FK_ST_Term_Branch]
GO
ALTER TABLE [AMS].[ST_Term]  WITH CHECK ADD  CONSTRAINT [FK_ST_Term_CompanyUnit] FOREIGN KEY([ST_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ST_Term] CHECK CONSTRAINT [FK_ST_Term_CompanyUnit]
GO
ALTER TABLE [AMS].[ST_Term]  WITH CHECK ADD  CONSTRAINT [FK_ST_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[ST_Term] CHECK CONSTRAINT [FK_ST_Term_GeneralLedger]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Branch]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_CompanyUnit] FOREIGN KEY([CmpUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_CompanyUnit]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_CostCenter] FOREIGN KEY([CostCenter_Id])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_CostCenter]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Currency]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Department] FOREIGN KEY([Department_Id1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Department]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Department1] FOREIGN KEY([Department_Id2])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Department1]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Department2] FOREIGN KEY([Department_Id3])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Department2]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Department3] FOREIGN KEY([Department_Id4])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Department3]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_GeneralLedger]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Godown] FOREIGN KEY([Godown_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Godown]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_JuniorAgent]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Product]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Subledger]
GO
ALTER TABLE [AMS].[Subledger]  WITH CHECK ADD  CONSTRAINT [FK_Subledger_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Subledger] CHECK CONSTRAINT [FK_Subledger_Branch]
GO
ALTER TABLE [AMS].[Subledger]  WITH CHECK ADD  CONSTRAINT [FK_Subledger_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Subledger] CHECK CONSTRAINT [FK_Subledger_CompanyUnit]
GO
ALTER TABLE [AMS].[Subledger]  WITH CHECK ADD  CONSTRAINT [FK_Subledger_GeneralLedger] FOREIGN KEY([GLID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Subledger] CHECK CONSTRAINT [FK_Subledger_GeneralLedger]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [CK_PB_Master] CHECK  (([PB_Invoice] IS NULL OR [PB_Invoice]<>''))
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [CK_PB_Master]
GO
/****** Object:  StoredProcedure [AMS].[Usp_IUD_Branch]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                    CREATE PROCEDURE [AMS].[Usp_IUD_Branch]
                    (@Event char(2), @Branch_Id smallint OUTPUT,	@Branch_Name VARCHAR(100),@Branch_Code VARCHAR(10),@Reg_Date DATETIME='01/01/1753',@Address VARCHAR(255)=NULL,@Country VARCHAR(50)=NULL,@State VARCHAR(50)=NULL,@City VARCHAR(50)=NULL,@PhoneNo VARCHAR(50)=NULL,@Fax VARCHAR(50)=NULL,@Email  VARCHAR(50)=NULL,@ContactPerson  VARCHAR(50)=NULL,@ContactPersonAdd  VARCHAR(255)=NULL,	@ContactPersonPhone  VARCHAR(255)=NULL,@Msg nvarchar(255) output,@Return_Id int =0 output)
                    as BEGIN TRY
                    BEGIN TRANSACTION     
                    begin
                    if @Event='I' begin
                    Insert Into AMS.Branch(Branch_Name, Branch_Code,  Reg_Date,  Address, Country,  State,  City,PhoneNo, Fax,Email, ContactPerson, ContactPersonAdd,     ContactPersonPhone    )
                    values(	 @Branch_Name, @Branch_Code,  @Reg_Date, @Address,  @Country,  @State,@City, @PhoneNo, @Fax,@Email, @ContactPerson, @ContactPersonAdd,	@ContactPersonPhone  )	
                    SET @Msg='Record Inserted Successfully !'
                    Set @Return_Id=@@IDENTITY	
                    end
                    else if @Event='U'
                    begin
                    Update AMS.Branch SET   Branch_Name=@Branch_Name,  Branch_Code=@Branch_Code,Reg_Date =@Reg_Date,     Address=@Address,Country=@Country,  PhoneNo=@PhoneNo,Fax=@Fax,      Email=@Email,ContactPerson=@ContactPerson,   ContactPersonAdd=@ContactPersonAdd,ContactPersonPhone=@ContactPersonPhone	WHERE Branch_Id=@Branch_Id SET @Msg='Record Updated Successfully !'
                    Set @Return_Id=@Branch_Id
                    end
                    ELSE IF @Event='D'
                    BEGIN
                    DELETE FROM AMS.Branch WHERE Branch_Id=@Branch_Id
                    SET @Msg='Record Deleted Successfully !'
                    Set @Return_Id=@Branch_Id
                    END
                    END
                    COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                    set @Msg=(select ERROR_MESSAGE() AS ErrorMessage)
                    ROLLBACK
                    END CATCH
                    
GO
/****** Object:  StoredProcedure [AMS].[Usp_IUD_CompanyInfo]    Script Date: 23/02/2018 2:06:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   
                    CREATE PROCEDURE [AMS].[Usp_IUD_CompanyInfo]
                    (@Event char(2),@Company_Id INT OUTPUT,@Company_Name VARCHAR(255),@Company_Logo IMAGE= NULL,@CReg_Date datetime=NULL,@Address VARCHAR(255)=NULL,@Country VARCHAR(255)=NULL,@State VARCHAR(255)=NULL,@City VARCHAR(255)=NULL,@PhoneNo VARCHAR(50)=NULL,@Fax VARCHAR(50)=NULL,@Pan_No  VARCHAR(50)=NULL,@Email  VARCHAR(50)=NULL,@Website VARCHAR(50)=NULL,@Database_Name  VARCHAR(255)=NULL,@Database_Path  VARCHAR(255)=NULL,@Version_No INT=0, @Status bit=0,@Msg nvarchar(255) output)
                    as BEGIN TRY
                    BEGIN TRANSACTION     
                    begin
                    if @Event='I' 
                    begin
                    IF NOT EXISTS(SELECT Company_Name FROM AMS.CompanyInfo WHERE  Company_Name=@Company_Name)
                    BEGIN
                    Insert Into AMS.CompanyInfo( Company_Name,Company_Logo,CReg_Date,Address,Country,State, City, PhoneNo, Fax,Pan_No,Email,Website, Database_Name, Database_Path, Version_No,  Status    )
                    values(@Company_Name, @Company_Logo,  @CReg_Date,  @Address,  @Country,  @State, @City,@PhoneNo,  @Fax, @Pan_No,@Email, @Website,  @Database_Name,  @Database_Path,  @Version_No,  @Status)	
                    SET @Company_Id=@@identity
                    SET @Msg='Record Inserted Successfully !'
                    END
                    ELSE
                    SET @Msg='All Ready Exists !'
                    end
                    else if @Event='U'
                    begin
                    IF NOT EXISTS(SELECT Company_Name FROM AMS.CompanyInfo WHERE  Company_Name=@Company_Name AND Company_Id<>@Company_Id)
                    BEGIN
                    if @Company_Logo IS NOT NULL
                    update AMS.Company SET Company_Logo=@Company_Logo WHERE Company_Id=@Company_Id 
                    update master.AMS.GlobalCompany SET Company_Name=@Company_Name WHERE Company_Name = (SELECT TOP 1 Company_Name FROM AMS.CompanyInfo WHERE  Company_Id=@Company_Id)
                    update AMS.CompanyInfo SET
                    Company_Name=@Company_Name,    
                    CReg_Date=@CReg_Date, Address=@Address,
                    Country=@Country,  PhoneNo=@PhoneNo,
                    Fax=@Fax,  Email=@Email,
                    Website=@Website,
                    Database_Name=@Database_Name,
                    Database_Path=@Database_Path,
                    Pan_No=@Pan_No,	                        
                    Status=@Status
                    WHERE Company_Id=@Company_Id
                    SET @Msg='Record Updated Successfully !'
                    END
                    ELSE
                    SET @Msg='All Ready Exists !'
                    end
                    ELSE IF @Event='D'
                    BEGIN                       
                    update master.AMS.GlobalCompany SET Status=0 where  Company_Name = (SELECT TOP 1 Company_Name FROM AMS.CompanyInfo WHERE  Company_Id=@Company_Id)
                    update  AMS.CompanyInfo SET Status=0 where  Company_Id=@Company_Id
                    SET @Msg='Record Deleted Successfully !'
                    END
                    END
                    COMMIT TRANSACTION
                    END TRY
                    BEGIN CATCH
                    set @Msg=(select ERROR_MESSAGE() AS ErrorMessage)
                    ROLLBACK
                    END CATCH                     
                        
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
GO
