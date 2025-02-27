/****** Object:  Schema [AMS]    Script Date: 06/08/2020 10:12:01 PM ******/
CREATE SCHEMA [AMS]
GO
/****** Object:  Schema [HOS]    Script Date: 06/08/2020 10:12:01 PM ******/
CREATE SCHEMA [HOS]
GO
/****** Object:  Schema [HR]    Script Date: 06/08/2020 10:12:01 PM ******/
CREATE SCHEMA [HR]
GO
/****** Object:  UserDefinedFunction [AMS].[Fn_Select_ClassCode]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [AMS].[Fn_Select_ClassCode] (@Class_Code1 varchar(50),	@Class_Code2 varchar(50))returns varchar(50)as begin DECLARE @Class_Code3 varchar(50) SET @Class_Code3 = ''   declare @Value varchar(50)  declare @Value1 varchar(50) Declare c0 Cursor For  SELECT Value FROM fn_Split(@Class_Code2, ',')    Open c0     Fetch Next From c0 Into @Value 	while @@FETCH_STATUS =0 Begin Declare c1 Cursor For SELECT Value FROM fn_Split(@Class_Code1, ',')  Open c1 Fetch Next From c1 Into @Value1 while @@FETCH_STATUS =0 Begin IF @Value1=@Value BEGIN SET @Class_Code3 = @Class_Code3 + @Value1 + ',' END Fetch Next From c1 Into @Value1 end CLOSE c1 DEALLOCATE c1 Fetch Next From c0 Into @Value   end CLOSE c0 DEALLOCATE c0 set @Class_Code3=(SELECT SUBSTRING(@Class_Code3,0,LEN(@Class_Code3)))	return @Class_Code3 end				
GO
/****** Object:  UserDefinedFunction [AMS].[fn_Split]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [AMS].[fn_Split] (@text varchar(8000), @delimiter varchar(20) = ' ') RETURNS @Strings TABLE(position int IDENTITY PRIMARY KEY, value varchar(8000)  ) AS BEGIN DECLARE @index int SET @index = -1 WHILE(LEN(@text) > 0)   BEGIN SET @index = CHARINDEX(@delimiter , @text) IF(@index = 0) AND(LEN(@text) > 0) BEGIN INSERT INTO @Strings VALUES(@text)           BREAK END  IF(@index > 1)       BEGIN INSERT INTO @Strings VALUES(LEFT(@text, @index - 1))          SET @text = RIGHT(@text, (LEN(@text) - @index))       END ELSE       SET @text = RIGHT(@text, (LEN(@text) - @index))    END RETURN END			
GO
/****** Object:  UserDefinedFunction [AMS].[fn_Splitstring]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [AMS].[fn_Splitstring] (@RowData nvarchar(2000),@SplitOn nvarchar(5)) RETURNS @ReturnTable table(Id int, Value Varchar(1024))             AS Begin                 Declare @i int Set @i = 0 While @RowData<> ''  Begin Set @i = @i + 1 if CharIndex(@SplitOn, @RowData) > 0  Begin Insert into @ReturnTable (ID, Value) Values (@i, substring(@RowData,1, CharIndex(@SplitOn, @RowData)-1) ) Set @RowData = substring(@RowData, CharIndex(@SplitOn, @RowData) + 1, len(@RowData))  End Else  Begin Insert into @ReturnTable(ID, Value) Values(@i, @RowData) Set @RowData = ''  End End  Return END				
GO
/****** Object:  UserDefinedFunction [AMS].[GetNumericValue]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE FUNCTION [AMS].[GetNumericValue]  
	                (@strAlphaNumeric VARCHAR(256))  
                RETURNS VARCHAR(256)  
                AS  
	                BEGIN  
                DECLARE @intAlpha INT  
                SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)  
	                BEGIN  
                WHILE @intAlpha > 0  
	                BEGIN  
                SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' )  
	                SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric )  
	                END  
                END  
	                RETURN ISNULL(@strAlphaNumeric,0)  
                END
                ;
GO
/****** Object:  UserDefinedFunction [AMS].[Retrun_Numeric]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [AMS].[Retrun_Numeric]
                        (
                        @strAlphaNumeric VARCHAR(256))
                        RETURNS VARCHAR(256)
                        AS
                        BEGIN
                        DECLARE @intAlpha INT
                        SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
                        BEGIN
                        WHILE @intAlpha > 0
                        BEGIN
                        SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '')
                        SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
                        END
                        END
                        RETURN ISNULL(@strAlphaNumeric,0)
                        END 
				
GO
/****** Object:  UserDefinedFunction [AMS].[Return_NepaliDate]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [AMS].[Return_NepaliDate] (@Date nvarchar(20) ) returns nvarchar(20) as begin set @Date = replace(@Date,'-','/')  declare @AD_Date nvarchar(32)   declare @Return nvarchar(20)    declare @Day int declare @Month int declare @Year int set @Month = (select top(1) * from AMS.split(@Date,'/',1)) 	if(@Month< 13) 	begin       set @Day = (select top(1) * from AMS.split(@Date,'/',2)) 		set @Year = (select top(1) * from AMS.Split(@Date,'/',3)) 	end 	else 	begin set @Year  = (select top(1) * from AMS.split(@Date,'/',1)) 		set @Month = (select top(1) * from AMS.split(@Date,'/',2)) 		set @Day = (select top(1) * from AMS.split(@Date,'/',3)) 	end set @AD_Date = cast(cast(@Month as int) as varchar(20))+'/'+cast(cast(@Day as int) as varchar(20))+'/'+cast(cast(@Year as int) as varchar(20)) 	set @Return = (select BS_DateDMY from AMS.DateMiti where AD_Date=@AD_Date) return @Return end				
GO
/****** Object:  UserDefinedFunction [AMS].[Split]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE FUNCTION [AMS].[Split] (@DataStr varchar(8000), @Delimiter char (1), @Count int)        returns @TemporaryTable TABLE(Items varchar(8000))         as         begin declare @Index int declare @Slice varchar(8000) declare @I int set @I = 1  select @Index = 1   if len(@DataStr)<1 or @DataStr is null  return     while @Index!= 0             begin set @Index = charindex(@Delimiter, @DataStr)                 if @Index!=0                     set @Slice = left(@DataStr, @Index - 1)  else  set @Slice = @DataStr if(len(@Slice)>0) begin if(@Count = @I) begin insert into @TemporaryTable(Items) values(@Slice)             end end      set @I = @I + 1        set @DataStr = right(@DataStr, len(@DataStr) - @Index) if len(@DataStr) = 0 break  end  return  end 					
GO
/****** Object:  UserDefinedFunction [AMS].[StrDate]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [AMS].[StrDate] (@Date1 as datetime) returns varchar(10) as begin DECLARE @strymd as varchar(10)     set @strymd = cast(year(@date1) as varchar(4)) + '-' + right('0' + cast(month(@date1) as varchar), 2) + '-' + right('0' + cast(day(@date1) as varchar), 2) 	return (@strymd) End 
GO
/****** Object:  UserDefinedFunction [dbo].[Return_Password]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[Return_Password]  ( @txt varchar(50)  )
				returns Varchar(100) --WITH ENCRYPTION
				as  begin
				declare @i int
				declare @txt1 varchar(100)
				declare @txt2 varchar(100)
				set @i=1
				set @txt2=''
				--print LEN(@txt)
				while @i <= LEN(@txt)
				begin
					set @txt1=char(249-ASCII(substring(@txt,@i,1)))
					set @i=@i+1
					set @txt2=@txt2 + @txt1
				end
					return @txt2
				end
GO
/****** Object:  Table [AMS].[AccountDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AccountDetails](
	[Module] [char](10) NOT NULL,
	[Serial_No] [int] NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](50) NOT NULL,
	[Voucher_Time] [datetime] NOT NULL,
	[Ledger_ID] [bigint] NOT NULL,
	[CbLedger_ID] [bigint] NULL,
	[Subleder_ID] [int] NULL,
	[Agent_ID] [int] NULL,
	[Department_ID1] [int] NULL,
	[Department_ID2] [int] NULL,
	[Department_ID3] [int] NULL,
	[Department_ID4] [int] NULL,
	[Currency_ID] [int] NOT NULL,
	[Currency_Rate] [decimal](18, 6) NOT NULL CONSTRAINT [DF_AccountDetails_Currency_Rate]  DEFAULT ((1)),
	[Debit_Amt] [decimal](18, 6) NOT NULL,
	[Credit_Amt] [decimal](18, 6) NOT NULL,
	[LocalDebit_Amt] [decimal](18, 6) NOT NULL,
	[LocalCredit_Amt] [decimal](18, 6) NOT NULL,
	[DueDate] [datetime] NULL,
	[DueDays] [int] NULL,
	[Narration] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[RefNo] [nvarchar](50) NULL,
	[RefDate] [nvarchar](50) NULL,
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
	[Cheque_Miti] [nvarchar](50) NULL,
	[PartyName] [nvarchar](100) NULL,
	[PartyLedger_Id] [int] NULL,
	[Party_PanNo] [nvarchar](50) NULL,
	[Branch_ID] [int] NOT NULL,
	[CmpUnit_ID] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
	[DoctorId] [int] NULL,
	[PatientId] [bigint] NULL,
	[HDepartmentId] [int] NULL,
 CONSTRAINT [PK_AccountDetails] PRIMARY KEY CLUSTERED 
(
	[Module] ASC,
	[Serial_No] ASC,
	[Voucher_No] ASC,
	[Ledger_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AccountGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AccountGroup](
	[GrpId] [int] NOT NULL,
	[GrpName] [nvarchar](200) NOT NULL,
	[GrpCode] [nvarchar](50) NOT NULL,
	[Schedule] [int] NOT NULL CONSTRAINT [DF_AccountGroup_Schedule]  DEFAULT ((1)),
	[PrimaryGrp] [nvarchar](50) NOT NULL,
	[GrpType] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_AccountGroup_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_AccountGroup_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
	[PrimaryGroupId] [int] NULL DEFAULT ((0)),
	[IsDefault] [char](1) NULL DEFAULT ((0)),
	[NepaliDesc] [nvarchar](200) NULL,
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AccountSubGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AccountSubGroup](
	[SubGrpId] [int] NOT NULL,
	[SubGrpName] [nvarchar](200) NOT NULL,
	[GrpID] [int] NOT NULL,
	[SubGrpCode] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_AccountSubGroup_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_AccountSubGroup_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
	[PrimaryGroupId] [int] NULL DEFAULT ((0)),
	[PrimarySubGroupId] [int] NULL DEFAULT ((0)),
	[IsDefault] [char](1) NULL DEFAULT ((0)),
	[NepaliDesc] [nvarchar](200) NULL,
 CONSTRAINT [PK_AccountSubGroup] PRIMARY KEY CLUSTERED 
(
	[SubGrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AccountSubGroup] UNIQUE NONCLUSTERED 
(
	[SubGrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Area]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Area](
	[AreaId] [int] NOT NULL,
	[AreaName] [nvarchar](150) NOT NULL,
	[AreaCode] [nvarchar](50) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Main_Area] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Area] PRIMARY KEY CLUSTERED 
(
	[AreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AreaDesc] UNIQUE NONCLUSTERED 
(
	[AreaName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_AreaShortName] UNIQUE NONCLUSTERED 
(
	[AreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogAccountGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogAccountGroup](
	[GrpId] [int] NOT NULL,
	[GrpName] [nvarchar](200) NULL,
	[GrpCode] [nvarchar](50) NULL,
	[Schedule] [int] NULL,
	[PrimaryGrp] [nvarchar](50) NULL,
	[GrpType] [nvarchar](50) NULL,
	[Branch_ID] [int] NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogAccountSubGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogAccountSubGroup](
	[SubGrpId] [int] NOT NULL,
	[SubGrpName] [nvarchar](200) NULL,
	[GrpID] [int] NULL,
	[SubGrpCode] [nvarchar](50) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogArea]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogArea](
	[AreaId] [int] NOT NULL,
	[AreaName] [nvarchar](150) NULL,
	[AreaCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Main_Area] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogBranch]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogBranch](
	[Branch_Id] [int] NOT NULL,
	[Branch_Name] [nvarchar](200) NULL,
	[Branch_Code] [nvarchar](50) NULL,
	[Reg_Date] [datetime] NULL,
	[Address] [nvarchar](500) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](80) NULL,
	[ContactPerson] [nvarchar](50) NULL,
	[ContactPersonAdd] [nvarchar](50) NULL,
	[ContactPersonPhone] [nvarchar](50) NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[Modify_By] [nvarchar](50) NULL,
	[Modify_Date] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogCB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogCB_Details](
	[Voucher_No] [nvarchar](50) NULL,
	[SNo] [int] NULL,
	[Ledger_Id] [bigint] NULL,
	[Subledger_Id] [int] NULL,
	[Agent_ID] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Debit] [decimal](18, 6) NULL,
	[Credit] [decimal](18, 6) NULL,
	[LocalDebit] [decimal](18, 6) NULL,
	[LocalCredit] [decimal](18, 6) NULL,
	[Narration] [varchar](1024) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Party_No] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[VatLedger_Id] [bigint] NULL,
	[PanNo] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogCB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogCB_Master](
	[VoucherMode] [char](10) NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NULL,
	[Voucher_Miti] [nvarchar](10) NULL,
	[Voucher_Time] [datetime] NULL,
	[Ref_VNo] [nvarchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[VoucherType] [nvarchar](50) NULL,
	[Ledger_Id] [bigint] NULL,
	[CheqNo] [nvarchar](50) NULL,
	[CheqDate] [datetime] NULL,
	[CheqMiti] [nvarchar](10) NULL,
	[Currency_Id] [int] NULL,
	[Currency_Rate] [decimal](18, 6) NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[ClearingDate] [datetime] NULL,
	[ClearingBy] [nvarchar](50) NULL,
	[PrintValue] [int] NULL,
	[CBranch_Id] [int] NULL,
	[CUnit_Id] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogCompanyInfo]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogCompanyInfo](
	[Company_Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Company_Name] [nvarchar](200) NULL,
	[Company_Logo] [image] NULL,
	[CReg_Date] [datetime] NULL,
	[Address] [nvarchar](200) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Pan_No] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Website] [nvarchar](100) NULL,
	[Database_Name] [nvarchar](50) NULL,
	[Database_Path] [nvarchar](100) NULL,
	[Version_No] [tinyint] NULL,
	[Status] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogCompanyUnit]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogCompanyUnit](
	[CmpUnit_Id] [int] NOT NULL,
	[CmpUnit_Name] [nvarchar](200) NULL,
	[CmpUnit_Code] [nvarchar](50) NULL,
	[Reg_Date] [datetime] NULL,
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
	[Branch_Id] [int] NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogCostCenter]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogCostCenter](
	[CCId] [int] NOT NULL,
	[CCName] [nvarchar](200) NULL,
	[CCcode] [nvarchar](50) NULL,
	[Branch_ID] [int] NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogCounter]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogCounter](
	[CId] [int] NOT NULL,
	[CName] [nvarchar](50) NULL,
	[CCode] [nvarchar](50) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogCurrency]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogCurrency](
	[CId] [int] NOT NULL,
	[CName] [nvarchar](100) NULL,
	[Ccode] [nvarchar](50) NULL,
	[CRate] [numeric](18, 6) NULL,
	[Branch_Id] [int] NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogDepartment]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogDepartment](
	[DId] [int] NOT NULL,
	[DName] [nvarchar](50) NOT NULL,
	[DCode] [nvarchar](50) NOT NULL,
	[Dlevel] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogDocumentNumbering]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogDocumentNumbering](
	[DocId] [int] NOT NULL,
	[DocModule] [nvarchar](50) NULL,
	[DocUser] [nvarchar](50) NULL,
	[DocDesc] [nvarchar](50) NULL,
	[DocStartDate] [date] NULL,
	[DocStartMiti] [varchar](10) NULL,
	[DocEndDate] [date] NULL,
	[DocEndMiti] [varchar](10) NULL,
	[DocType] [char](10) NULL,
	[DocPrefix] [nvarchar](50) NULL,
	[DocSufix] [nvarchar](50) NULL,
	[DocBodyLength] [numeric](18, 0) NULL,
	[DocTotalLength] [numeric](18, 0) NULL,
	[DocBlank] [bit] NULL,
	[DocBlankCh] [char](1) NULL,
	[DocBranch] [int] NULL,
	[DocUnit] [int] NULL,
	[DocStart] [numeric](18, 0) NULL,
	[DocCurr] [numeric](18, 0) NULL,
	[DocEnd] [numeric](18, 0) NULL,
	[DocDesign] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogFloor]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogFloor](
	[FloorId] [int] NOT NULL,
	[Description] [nvarchar](50) NULL,
	[ShortName] [nvarchar](50) NULL,
	[Type] [char](10) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Branch_Id] [int] NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogGeneralLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogGeneralLedger](
	[GLID] [bigint] NOT NULL,
	[GLName] [nvarchar](200) NULL,
	[GLCode] [nvarchar](50) NULL,
	[ACCode] [nvarchar](50) NULL,
	[GLType] [nvarchar](50) NULL,
	[GrpId] [int] NULL,
	[SubGrpId] [int] NULL,
	[PanNo] [nvarchar](50) NULL,
	[AreaId] [int] NULL,
	[AgentId] [int] NULL,
	[CurrId] [int] NULL,
	[CrDays] [numeric](18, 0) NULL,
	[CrLimit] [decimal](18, 6) NULL,
	[CrTYpe] [nvarchar](50) NULL,
	[IntRate] [decimal](18, 6) NULL,
	[GLAddress] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LandLineNo] [nvarchar](50) NULL,
	[OwnerName] [nvarchar](50) NULL,
	[OwnerNumber] [nvarchar](50) NULL,
	[Scheme] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Branch_id] [int] NULL,
	[Company_Id] [int] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogGodown]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogGodown](
	[GID] [int] NOT NULL,
	[GName] [nvarchar](80) NULL,
	[GCode] [nvarchar](50) NULL,
	[GLocation] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[CompUnit] [int] NULL,
	[BranchUnit] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogGT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogGT_Details](
	[VoucherNo] [nvarchar](50) NULL,
	[SNo] [int] NULL,
	[ProId] [bigint] NULL,
	[ToGdn] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[AltQty] [decimal](18, 6) NULL,
	[AltUOM] [int] NULL,
	[Qty] [decimal](18, 6) NULL,
	[UOM] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Narration] [varchar](1024) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogGT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogGT_Master](
	[VoucherNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NULL,
	[VoucherMiti] [nvarchar](50) NULL,
	[FrmGdn] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Remarks] [varchar](1024) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[CompanyUnit] [int] NULL,
	[BranchId] [int] NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogJuniorAgent]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogJuniorAgent](
	[AgentId] [int] NOT NULL,
	[AgentName] [nvarchar](200) NULL,
	[AgentCode] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[GLCode] [bigint] NULL,
	[Commission] [decimal](18, 6) NULL,
	[SAgent] [int] NULL,
	[Email] [nvarchar](200) NULL,
	[CRLimit] [numeric](18, 8) NULL,
	[CrDays] [nvarchar](50) NULL,
	[CrType] [nvarchar](50) NULL,
	[Branch_id] [int] NULL,
	[Company_ID] [int] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogJV_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogJV_Details](
	[Voucher_No] [nvarchar](50) NULL,
	[SNo] [int] NULL,
	[Ledger_Id] [bigint] NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CBLedger_Id] [bigint] NULL,
	[Chq_No] [nvarchar](50) NULL,
	[Chq_Date] [datetime] NULL,
	[Debit] [decimal](18, 6) NULL,
	[Credit] [decimal](18, 6) NULL,
	[LocalDebit] [decimal](18, 6) NULL,
	[LocalCredit] [decimal](18, 6) NULL,
	[Narration] [varchar](1024) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Vat_Reg] [bit] NULL,
	[Party_No] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[VatLedger_Id] [nvarchar](50) NULL,
	[PanNo] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogJV_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogJV_Master](
	[VoucherMode] [char](10) NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NULL,
	[Voucher_Miti] [nvarchar](10) NULL,
	[Voucher_Time] [datetime] NULL,
	[Currency_Id] [int] NULL,
	[Currency_Rate] [decimal](18, 6) NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Ref_VNo] [varchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[Remarks] [varchar](1024) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[Audit_Lock] [bit] NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[ClearingBy] [nvarchar](50) NULL,
	[ClearingDate] [datetime] NULL,
	[PrintValue] [int] NULL,
	[CBranch_Id] [int] NULL,
	[CUnit_Id] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogLedgerOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogLedgerOpening](
	[Opening_Id] [int] NULL,
	[Module] [char](10) NULL,
	[Voucher_No] [nvarchar](50) NULL,
	[OP_Date] [datetime] NULL,
	[OP_Miti] [nvarchar](50) NULL,
	[Serial_No] [int] NULL,
	[Ledger_Id] [bigint] NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Currency_Id] [int] NULL,
	[Currency_Rate] [decimal](18, 6) NULL,
	[Debit] [decimal](18, 6) NULL,
	[LocalDebit] [decimal](18, 6) NULL,
	[Credit] [decimal](18, 6) NULL,
	[LocalCredit] [decimal](18, 6) NULL,
	[Narration] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Branch_Id] [int] NULL,
	[Company_Id] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogMainArea]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogMainArea](
	[MAreaId] [int] NOT NULL,
	[MAreaName] [nvarchar](100) NULL,
	[MAreaCode] [nvarchar](50) NULL,
	[MCountry] [nvarchar](50) NULL,
	[Branch_ID] [int] NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditlogNotes_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditlogNotes_Details](
	[VoucherMode] [char](10) NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[Ledger_Id] [bigint] NULL,
	[Subledge_Id] [int] NULL,
	[Agent_ID] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Debit] [decimal](18, 6) NOT NULL,
	[Credit] [decimal](18, 6) NOT NULL,
	[LocalDebit] [decimal](18, 6) NOT NULL,
	[LocalCredit] [decimal](18, 6) NOT NULL,
	[Narration] [varchar](1024) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Party_No] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[VatLedger_Id] [bigint] NULL,
	[PanNo] [numeric](18, 0) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogNotes_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogNotes_Master](
	[VoucherMode] [nvarchar](50) NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NULL,
	[Voucher_Miti] [nvarchar](10) NULL,
	[Voucher_Time] [datetime] NULL,
	[Ref_VNo] [nvarchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[VoucherType] [nvarchar](50) NULL,
	[Ledger_Id] [bigint] NULL,
	[CheqNo] [nvarchar](50) NULL,
	[CheqDate] [datetime] NULL,
	[CheqMiti] [nvarchar](50) NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Currency_Id] [int] NULL,
	[Currency_Rate] [decimal](18, 6) NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[AuditLock] [char](10) NULL,
	[ClearingDate] [datetime] NULL,
	[ClearingBy] [nvarchar](50) NULL,
	[PrintValue] [int] NULL,
	[CBranch_Id] [int] NULL,
	[CUnit_Id] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogPAB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogPAB_Details](
	[PAB_Invoice] [nvarchar](50) NULL,
	[SNo] [int] NULL,
	[PT_Id] [int] NULL,
	[Ledger_Id] [bigint] NULL,
	[CBLedger_Id] [bigint] NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Product_Id] [bigint] NULL,
	[Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[Percentage] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[PAB_Narration] [varchar](1024) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogPAB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogPAB_Master](
	[PAB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[Invoice_Time] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Agent_Id] [int] NULL,
	[PB_Invoice] [nvarchar](50) NULL,
	[PB_Date] [datetime] NULL,
	[PB_Miti] [nvarchar](50) NULL,
	[PB_Qty] [decimal](18, 6) NULL,
	[PB_Amount] [decimal](18, 6) NULL,
	[LocalAmount] [decimal](18, 0) NULL,
	[Cur_Id] [int] NULL,
	[Cur_Rate] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NULL,
	[In_Words] [nvarchar](1024) NULL,
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
	[CBranch_Id] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogPB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogPB_Details](
	[PB_Invoice] [nvarchar](50) NULL,
	[Invoice_SNo] [numeric](18, 0) NULL,
	[P_Id] [bigint] NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[AltStock_Qty] [decimal](18, 6) NULL,
	[Stock_Qty] [decimal](18, 6) NULL,
	[Narration] [nvarchar](1024) NULL,
	[PO_Invoice] [nvarchar](50) NULL,
	[PO_Sno] [numeric](18, 0) NULL,
	[PC_Invoice] [nvarchar](50) NULL,
	[PC_SNo] [nvarchar](50) NULL,
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
	[Manu_Date] [datetime] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogPB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogPB_Master](
	[PB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](10) NULL,
	[Invoice_Time] [datetime] NULL,
	[PB_Vno] [nvarchar](50) NULL,
	[Vno_Date] [datetime] NULL,
	[Vno_Miti] [nvarchar](10) NULL,
	[Vendor_ID] [bigint] NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_In] [nvarchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[PO_Invoice] [nvarchar](250) NULL,
	[PO_Date] [datetime] NULL,
	[PC_Invoice] [nvarchar](250) NULL,
	[PC_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Cur_Id] [int] NULL,
	[Cur_Rate] [decimal](18, 6) NULL,
	[Counter_ID] [int] NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[LN_Amount] [decimal](18, 6) NULL,
	[Tender_Amount] [decimal](18, 6) NULL,
	[Change_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Action_type] [nvarchar](50) NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[CancelRemarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogPB_OtherMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogPB_OtherMaster](
	[PAB_Invoice] [nvarchar](50) NOT NULL,
	[PPNo] [nvarchar](50) NULL,
	[PPDate] [datetime] NULL,
	[TaxableAmount] [decimal](18, 6) NULL,
	[VatAmount] [decimal](18, 6) NULL,
	[CustomAgent] [nvarchar](50) NULL,
	[Transportation] [nvarchar](50) NULL,
	[VechileNo] [nvarchar](50) NULL,
	[Cn_No] [varchar](25) NULL,
	[Cn_Date] [datetime] NULL,
	[BankDoc] [nvarchar](50) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogPB_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogPB_Term](
	[PB_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogSB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogSB_Details](
	[SB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [bigint] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
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
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogSB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogSB_Master](
	[SB_Invoice] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [varchar](10) NULL,
	[Invoice_Time] [datetime] NULL,
	[PB_Vno] [nvarchar](50) NULL,
	[Vno_Date] [datetime] NULL,
	[Vno_Miti] [varchar](10) NULL,
	[Customer_ID] [int] NULL,
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
	[Cur_Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[LN_Amount] [decimal](18, 6) NULL,
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
	[CBranch_Id] [int] NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogSB_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogSB_Term](
	[SB_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
	[ActonType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogSO_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogSO_Details](
	[SO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
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
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogSO_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogSO_Master](
	[SO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [varchar](10) NOT NULL,
	[Invoice_Time] [datetime] NULL,
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
	[Invoice_Type] [nvarchar](50) NOT NULL,
	[Invoice_Mode] [nvarchar](50) NOT NULL,
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
	[Cur_Id] [int] NULL,
	[Cur_Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[LN_Amount] [decimal](18, 6) NULL,
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
	[Enter_By] [nvarchar](50) NULL,
	[Enter_Date] [datetime] NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[Actiontype] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogSO_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogSO_Term](
	[SO_Vno] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogSR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[AuditLogSR_Details](
	[SR_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [bigint] NULL,
	[Alt_Qty] [decimal](18, 6) NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
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
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [nvarchar](50) NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[AuditLogSR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogSR_Master](
	[SR_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [varchar](10) NOT NULL,
	[Invoice_Time] [datetime] NULL,
	[SB_Invoice] [nvarchar](50) NULL,
	[SB_Date] [datetime] NULL,
	[SB_Miti] [varchar](10) NULL,
	[Customer_ID] [int] NOT NULL,
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
	[Cur_Id] [int] NULL,
	[Cur_Rate] [decimal](18, 6) NULL,
	[B_Amount] [decimal](18, 6) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[N_Amount] [decimal](18, 6) NULL,
	[LN_Amount] [decimal](18, 6) NULL,
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
	[CBranch_Id] [int] NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[AuditLogSR_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[AuditLogSR_Term](
	[SR_VNo] [nvarchar](50) NOT NULL,
	[ST_Id] [int] NOT NULL,
	[SNo] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
	[ActionType] [nvarchar](50) NULL,
	[ActionBy] [nvarchar](50) NULL,
	[ActionDate] [datetime] NULL,
	[SystemUser] [nvarchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[BillOfMaterial_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[BillOfMaterial_Details](
	[MemoNo] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[GdnId] [int] NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[AltQty] [decimal](18, 6) NULL,
	[AltUnitId] [int] NULL,
	[Qty] [decimal](18, 6) NULL,
	[UnitId] [int] NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Narration] [nvarchar](1024) NULL,
	[Order_No] [nvarchar](50) NULL,
	[Order_SNo] [int] NULL,
 CONSTRAINT [PK_BillOfMaterial_Details] PRIMARY KEY CLUSTERED 
(
	[MemoNo] ASC,
	[SNo] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[BillOfMaterial_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[BillOfMaterial_Master](
	[MemoNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](10) NOT NULL,
	[MemoDesc] [nvarchar](1024) NOT NULL,
	[OrderNo] [nvarchar](50) NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[FGProductId] [bigint] NOT NULL,
	[AltQty] [decimal](18, 6) NOT NULL,
	[AltUnitId] [int] NOT NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[UnitId] [int] NOT NULL,
	[Factor] [decimal](18, 6) NULL,
	[FFactor] [decimal](18, 6) NULL,
	[CostRate] [decimal](18, 6) NULL,
	[GdnId] [int] NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[Dep1] [int] NULL,
	[Dep2] [int] NULL,
	[Dep3] [int] NULL,
	[Dep4] [int] NULL,
	[TotalQty] [decimal](18, 6) NOT NULL,
	[NetAmount] [decimal](18, 6) NOT NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Module] [nvarchar](50) NOT NULL,
	[ActionType] [nvarchar](50) NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[AuthorisedBy] [nvarchar](50) NULL,
	[AuthorisedDate] [datetime] NULL,
	[CompanyUnitId] [int] NULL,
	[BranchId] [int] NOT NULL,
 CONSTRAINT [PK_BillOfMaterial_Master_1] PRIMARY KEY CLUSTERED 
(
	[MemoNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Branch]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Branch](
	[Branch_Id] [int] NOT NULL,
	[Branch_Name] [nvarchar](200) NOT NULL,
	[Branch_Code] [nvarchar](50) NOT NULL,
	[Reg_Date] [datetime] NULL,
	[Address] [nvarchar](500) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](80) NULL,
	[ContactPerson] [nvarchar](50) NULL,
	[ContactPersonAdd] [nvarchar](50) NULL,
	[ContactPersonPhone] [nvarchar](50) NULL,
	[Created_By] [nvarchar](50) NULL,
	[Created_Date] [datetime] NULL,
	[Modify_By] [nvarchar](50) NULL,
	[Modify_Date] [datetime] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Branch_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_BranchDesc] UNIQUE NONCLUSTERED 
(
	[Branch_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_BranchShortName] UNIQUE NONCLUSTERED 
(
	[Branch_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[BranchWiseLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[BranchWiseLedger](
	[BranchId] [int] NULL,
	[LedgerId] [bigint] NULL,
	[Mapped] [bit] NULL,
	[Category] [nvarchar](50) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Budget]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Budget](
	[BudgetId] [int] NOT NULL,
	[LedgerId] [bigint] NOT NULL,
	[Dep1] [int] NULL,
	[Dep2] [int] NULL,
	[Dep3] [int] NULL,
	[Dep4] [int] NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[MonthDesc] [nvarchar](50) NOT NULL,
	[Date] [datetime] NULL,
	[Miti] [varchar](10) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[BudgetLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[BudgetLedger](
	[BLID] [int] NULL,
	[LedgerId] [bigint] NULL,
	[MonthsDesc] [nvarchar](50) NULL,
	[Amount] [decimal](16, 6) NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CashBankDenomination]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CashBankDenomination](
	[VoucherNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](10) NOT NULL,
	[TotalAmount] [decimal](18, 6) NOT NULL,
	[R1000] [decimal](18, 6) NOT NULL,
	[R500] [decimal](18, 6) NOT NULL,
	[R100] [decimal](18, 6) NOT NULL,
	[R50] [decimal](18, 6) NOT NULL,
	[R20] [decimal](18, 6) NOT NULL,
	[R10] [decimal](18, 6) NOT NULL,
	[R5] [decimal](18, 6) NOT NULL,
	[R2] [decimal](18, 6) NOT NULL,
	[R1] [decimal](18, 6) NOT NULL,
	[RO] [decimal](18, 6) NOT NULL,
	[RC] [decimal](18, 6) NOT NULL,
	[RemainAmt] [decimal](18, 6) NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CashClosing]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[CashClosing](
	[CC_Id] [int] IDENTITY(1,1) NOT NULL,
	[EnterBy] [varchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[EnterMiti] [varchar](10) NOT NULL,
	[EnterTime] [datetime] NOT NULL,
	[CB_Balance] [nvarchar](50) NOT NULL,
	[Cash_Sales] [decimal](18, 6) NOT NULL,
	[Cash_Purchase] [decimal](18, 6) NOT NULL,
	[TotalCash] [decimal](18, 6) NOT NULL,
	[ThauQty] [decimal](18, 6) NOT NULL,
	[ThouVal] [decimal](18, 6) NOT NULL,
	[FivHunQty] [decimal](18, 6) NOT NULL,
	[FivHunVal] [decimal](18, 6) NOT NULL,
	[HunQty] [decimal](18, 6) NOT NULL,
	[HunVal] [decimal](18, 6) NOT NULL,
	[FiFtyQty] [decimal](18, 6) NOT NULL,
	[FiftyVal] [decimal](18, 6) NOT NULL,
	[TwenteyFiveQty] [decimal](18, 6) NOT NULL,
	[TwentyFiveVal] [decimal](18, 6) NOT NULL,
	[TwentyQty] [decimal](18, 6) NOT NULL,
	[TwentyVal] [decimal](16, 6) NOT NULL,
	[TenQty] [decimal](18, 6) NOT NULL,
	[TenVal] [decimal](18, 6) NOT NULL,
	[FiveQty] [decimal](18, 6) NOT NULL,
	[FiveVal] [decimal](18, 6) NOT NULL,
	[TwoQty] [decimal](18, 6) NOT NULL,
	[TwoVal] [decimal](18, 6) NOT NULL,
	[OneQty] [decimal](18, 6) NOT NULL,
	[OneVal] [decimal](18, 6) NOT NULL,
	[Cash_Diff] [decimal](18, 6) NOT NULL,
	[Module] [char](10) NOT NULL,
	[HandOverUser] [nvarchar](50) NOT NULL,
	[Remarks] [varchar](250) NULL,
 CONSTRAINT [PK_CashClosing] PRIMARY KEY CLUSTERED 
(
	[CC_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[CB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[CB_Details](
	[Voucher_No] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Debit] [decimal](18, 6) NOT NULL,
	[Credit] [decimal](18, 6) NOT NULL,
	[LocalDebit] [decimal](18, 6) NOT NULL,
	[LocalCredit] [decimal](18, 6) NOT NULL,
	[Narration] [varchar](1024) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Party_No] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[VatLedger_Id] [bigint] NULL,
	[PanNo] [int] NULL,
	[Vat_Reg] [bit] NULL,
 CONSTRAINT [PK_CB_Details] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC,
	[SNo] ASC,
	[Ledger_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[CB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[CB_Master](
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
	[Currency_Rate] [decimal](18, 6) NOT NULL,
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
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_CB_Master_1] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[CompanyInfo]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CompanyInfo](
	[Company_Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Company_Name] [nvarchar](200) NOT NULL,
	[Company_Logo] [image] NULL,
	[CReg_Date] [datetime] NULL,
	[Address] [nvarchar](200) NULL,
	[Country] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Pan_No] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Website] [nvarchar](100) NULL,
	[Database_Name] [nvarchar](50) NOT NULL,
	[Database_Path] [nvarchar](100) NULL,
	[Version_No] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_CompanyInfo_Status]  DEFAULT ((1)),
	[CreateDate] [datetime] NULL,
	[LoginDate] [datetime] NULL,
	[SoftModule] [nvarchar](50) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Company_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyDesc] UNIQUE NONCLUSTERED 
(
	[Company_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CompanyUnit]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CompanyUnit](
	[CmpUnit_Id] [int] NOT NULL,
	[CmpUnit_Name] [nvarchar](200) NOT NULL,
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
	[Branch_Id] [int] NOT NULL,
	[Created_By] [nvarchar](50) NOT NULL,
	[Created_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_CompanyUnit] PRIMARY KEY CLUSTERED 
(
	[CmpUnit_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyUnitDesc] UNIQUE NONCLUSTERED 
(
	[CmpUnit_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CompanyUnitShortName] UNIQUE NONCLUSTERED 
(
	[CmpUnit_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CostCenter]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CostCenter](
	[CCId] [int] NOT NULL,
	[CCName] [nvarchar](200) NOT NULL,
	[CCcode] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED 
(
	[CCId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CostCenterDesc] UNIQUE NONCLUSTERED 
(
	[CCName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CostCenterShortName] UNIQUE NONCLUSTERED 
(
	[CCcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CostCenterExpenses_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CostCenterExpenses_Details](
	[CostingNo] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[LedgerId] [bigint] NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Narration] [nvarchar](1024) NULL,
	[OrderNo] [nvarchar](50) NULL,
	[OrderSNo] [int] NULL,
 CONSTRAINT [PK_CostCenterExpenses_Details] PRIMARY KEY CLUSTERED 
(
	[CostingNo] ASC,
	[SNo] ASC,
	[LedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[CostCenterExpenses_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[CostCenterExpenses_Master](
	[CostingNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](10) NOT NULL,
	[VoucherTime] [datetime] NOT NULL,
	[OrderNo] [nvarchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[FGProductId] [bigint] NOT NULL,
	[GdnId] [int] NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Remarks] [nvarchar](1024) NULL,
	[ActionType] [nvarchar](50) NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[CompanyUnitId] [int] NULL,
	[BranchId] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_CostCenterExpenses_Master] PRIMARY KEY CLUSTERED 
(
	[CostingNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Counter]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Counter](
	[CId] [int] NOT NULL,
	[CName] [nvarchar](50) NOT NULL,
	[CCode] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Counter_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Printer] [varchar](250) NULL,
 CONSTRAINT [PK_Counter] PRIMARY KEY CLUSTERED 
(
	[CId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CounterDesc] UNIQUE NONCLUSTERED 
(
	[CName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CounterShortName] UNIQUE NONCLUSTERED 
(
	[CCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Currency]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Currency](
	[CId] [int] NOT NULL,
	[CName] [nvarchar](100) NOT NULL,
	[Ccode] [nvarchar](50) NOT NULL,
	[CRate] [numeric](18, 6) NOT NULL CONSTRAINT [DF_Currency_CRate]  DEFAULT ((1)),
	[Branch_Id] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Currency_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_Currency_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[CId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CurrencyDesc] UNIQUE NONCLUSTERED 
(
	[CName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_CurrencyShortName] UNIQUE NONCLUSTERED 
(
	[Ccode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[DateMiti]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[DateMiti](
	[Date_Id] [bigint] NOT NULL,
	[BS_Date] [varchar](10) NOT NULL,
	[BS_DateDMY] [varchar](10) NOT NULL,
	[AD_Date] [datetime] NOT NULL,
	[BS_Months] [varchar](50) NOT NULL,
	[AD_Months] [varchar](50) NOT NULL,
	[Days] [varchar](50) NULL,
	[Is_Holiday] [bit] NULL,
	[Holiday] [varchar](1024) NULL,
 CONSTRAINT [PK_DateMiti] PRIMARY KEY CLUSTERED 
(
	[Date_Id] ASC,
	[BS_Date] ASC,
	[BS_DateDMY] ASC,
	[AD_Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
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
/****** Object:  Table [AMS].[Department]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Department](
	[DId] [int] NOT NULL,
	[DName] [nvarchar](50) NOT NULL,
	[DCode] [nvarchar](50) NOT NULL,
	[Dlevel] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DepartmentDesc] UNIQUE NONCLUSTERED 
(
	[DName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_DepartmentShortName] UNIQUE NONCLUSTERED 
(
	[DCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[DllPrintDesigList]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[DllPrintDesigList](
	[ListId] [int] NOT NULL,
	[Module] [char](4) NOT NULL,
	[DesignDesc] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DllPrintDesigList_ListId] PRIMARY KEY CLUSTERED 
(
	[ListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[DocumentDesignPrint]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[DocumentDesignPrint](
	[DDP_Id] [int] NOT NULL,
	[Module] [char](10) NOT NULL,
	[Paper_Name] [nvarchar](100) NOT NULL,
	[Is_Online] [bit] NOT NULL,
	[NoOfPrint] [int] NOT NULL,
	[Notes] [nvarchar](250) NULL,
	[DesignerPaper_Name] [nvarchar](250) NULL,
	[Created_By] [nvarchar](250) NULL,
	[Created_Date] [datetime] NULL,
	[Status] [bit] NOT NULL,
	[Branch_Id] [int] NOT NULL,
	[CmpUnit_Id] [int] NULL,
 CONSTRAINT [PK_PrintDocument_Design] PRIMARY KEY CLUSTERED 
(
	[DDP_Id] ASC,
	[Module] ASC,
	[Paper_Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[DocumentNumbering]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[DocumentNumbering](
	[DocId] [int] NOT NULL,
	[DocModule] [nvarchar](50) NOT NULL,
	[DocUser] [nvarchar](50) NULL,
	[DocDesc] [nvarchar](50) NOT NULL,
	[DocStartDate] [date] NOT NULL,
	[DocStartMiti] [varchar](10) NOT NULL,
	[DocEndDate] [date] NOT NULL,
	[DocEndMiti] [varchar](10) NOT NULL,
	[DocType] [char](10) NOT NULL,
	[DocPrefix] [nvarchar](50) NULL,
	[DocSufix] [nvarchar](50) NULL,
	[DocBodyLength] [numeric](18, 0) NOT NULL,
	[DocTotalLength] [numeric](18, 0) NOT NULL,
	[DocBlank] [bit] NOT NULL CONSTRAINT [DF_DocumentNumbering_DocBlank]  DEFAULT ((1)),
	[DocBlankCh] [char](1) NOT NULL CONSTRAINT [DF_DocumentNumbering_DocBlankCh]  DEFAULT ((0)),
	[DocBranch] [int] NOT NULL,
	[DocUnit] [int] NULL,
	[DocStart] [numeric](18, 0) NULL,
	[DocCurr] [numeric](18, 0) NULL,
	[DocEnd] [numeric](18, 0) NULL,
	[DocDesign] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_DocumentNumbering_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_DocumentNumbering_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
	[FY_Id] [int] NULL,
	[FiscalYearId] [int] NULL,
 CONSTRAINT [PK_DocumentNumbering] PRIMARY KEY CLUSTERED 
(
	[DocId] ASC,
	[DocModule] ASC,
	[DocDesc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[FGR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[FGR_Details](
	[FGR_No] [varchar](15) NOT NULL,
	[SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[CC_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NOT NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[AltStock_Qty] [decimal](18, 6) NOT NULL,
	[Stock_Qty] [decimal](18, 6) NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[Cnv_Ratio] [decimal](18, 6) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[FGR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[FGR_Master](
	[FGR_No] [varchar](15) NOT NULL,
	[FGR_Date] [datetime] NOT NULL,
	[FGR_Time] [datetime] NOT NULL,
	[FGR_Miti] [varchar](10) NOT NULL,
	[Gdn_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[GLId] [bigint] NULL,
	[CBranch_Id] [int] NULL,
	[CUnit_Id] [int] NULL,
	[CC_Id] [int] NULL,
	[SB_No] [varchar](250) NULL,
	[SB_SNo] [int] NULL,
	[SO_No] [varchar](250) NULL,
	[Auth_By] [varchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Source] [varchar](15) NULL,
	[Export] [char](1) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Enter_By] [varchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[FiscalYearId] [int] NULL,
 CONSTRAINT [PK_FGR_Master] PRIMARY KEY CLUSTERED 
(
	[FGR_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[FinanceSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[FinanceSetting](
	[FinId] [tinyint] NOT NULL,
	[ProfiLossId] [bigint] NULL,
	[CashId] [bigint] NULL,
	[VATLedgerId] [bigint] NULL,
	[PDCBankLedgerId] [bigint] NULL,
	[ShortNameWisTransaction] [bit] NULL,
	[WarngNegativeTransaction] [bit] NULL,
	[NegativeTransaction] [nvarchar](50) NULL,
	[VoucherDate] [bit] NULL,
	[AgentEnable] [bit] NULL,
	[AgentMandetory] [bit] NULL,
	[DepartmentEnable] [bit] NULL,
	[DepartmentMandetory] [bit] NULL,
	[RemarksEnable] [bit] NULL,
	[RemarksMandetory] [bit] NULL,
	[NarrationMandetory] [bit] NULL,
	[CurrencyEnable] [bit] NULL,
	[CurrencyMandetory] [bit] NULL,
	[SubledgerEnable] [bit] NULL,
	[SubledgerMandetory] [bit] NULL,
	[DetailsClassEnable] [bit] NULL,
	[DetailsClassMandetory] [bit] NULL,
 CONSTRAINT [PK_FinanceSetting] PRIMARY KEY CLUSTERED 
(
	[FinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[FiscalYear]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[FiscalYear](
	[FY_Id] [int] IDENTITY(1,1) NOT NULL,
	[AD_FY] [varchar](5) NOT NULL,
	[BS_FY] [varchar](5) NOT NULL,
	[Current_FY] [bit] NOT NULL,
	[Start_ADDate] [datetime] NOT NULL,
	[End_ADDate] [datetime] NOT NULL,
	[Start_BSDate] [varchar](10) NOT NULL,
	[End_BSDate] [varchar](10) NOT NULL,
 CONSTRAINT [PK_FiscalYear] PRIMARY KEY CLUSTERED 
(
	[FY_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYearAD] UNIQUE NONCLUSTERED 
(
	[AD_FY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FiscalYearBS] UNIQUE NONCLUSTERED 
(
	[BS_FY] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Floor]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Floor](
	[FloorId] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[Type] [char](10) NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Branch_Id] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Floor] PRIMARY KEY CLUSTERED 
(
	[FloorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FloorDesc] UNIQUE NONCLUSTERED 
(
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_FloorShortName] UNIQUE NONCLUSTERED 
(
	[ShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[GeneralLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[GeneralLedger](
	[GLID] [bigint] NOT NULL,
	[GLName] [nvarchar](200) NOT NULL,
	[GLCode] [nvarchar](50) NOT NULL,
	[ACCode] [nvarchar](50) NOT NULL,
	[GLType] [nvarchar](50) NOT NULL,
	[GrpId] [int] NOT NULL,
	[SubGrpId] [int] NULL,
	[PanNo] [nvarchar](50) NULL,
	[AreaId] [int] NULL,
	[AgentId] [int] NULL,
	[CurrId] [int] NULL CONSTRAINT [DF_GeneralLedger_CurrId]  DEFAULT ((1)),
	[CrDays] [numeric](18, 0) NOT NULL CONSTRAINT [DF_GeneralLedger_CrDays]  DEFAULT ((0)),
	[CrLimit] [decimal](18, 6) NOT NULL CONSTRAINT [DF_GeneralLedger_CrLimit]  DEFAULT ((0.00)),
	[CrTYpe] [nvarchar](50) NOT NULL CONSTRAINT [DF_GeneralLedger_CrTYpe]  DEFAULT (N'Ignore'),
	[IntRate] [decimal](18, 6) NOT NULL CONSTRAINT [DF_GeneralLedger_IntRate]  DEFAULT ((0.00)),
	[GLAddress] [nvarchar](500) NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LandLineNo] [nvarchar](50) NULL,
	[OwnerName] [nvarchar](50) NULL,
	[OwnerNumber] [nvarchar](50) NULL,
	[Scheme] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Branch_id] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_GeneralLedger_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_GeneralLedger_Status]  DEFAULT ((1)),
	[PrimaryGroupId] [int] NULL DEFAULT ((0)),
	[PrimarySubGroupId] [int] NULL DEFAULT ((0)),
	[IsDefault] [char](1) NULL DEFAULT ((0)),
	[NepaliDesc] [nvarchar](200) NULL,
 CONSTRAINT [PK_GeneralLedger] PRIMARY KEY CLUSTERED 
(
	[GLID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_GeneralLedgerAccount] UNIQUE NONCLUSTERED 
(
	[ACCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_GeneralLedgerShortName] UNIQUE NONCLUSTERED 
(
	[GLCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Godown]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Godown](
	[GID] [int] NOT NULL,
	[GName] [nvarchar](80) NULL,
	[GCode] [nvarchar](50) NULL,
	[GLocation] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[EnterBy] [nvarchar](50) NULL,
	[EnterDate] [datetime] NULL,
	[CompUnit] [int] NULL,
	[BranchUnit] [int] NULL,
 CONSTRAINT [PK_Godown] PRIMARY KEY CLUSTERED 
(
	[GID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [GodownDesc] UNIQUE NONCLUSTERED 
(
	[GName] ASC,
	[GCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[GT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[GT_Details](
	[VoucherNo] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[ProId] [bigint] NOT NULL,
	[ToGdn] [int] NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[AltQty] [decimal](18, 6) NOT NULL,
	[AltUOM] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[UOM] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Narration] [varchar](1024) NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GT_Details] PRIMARY KEY CLUSTERED 
(
	[VoucherNo] ASC,
	[SNo] ASC,
	[ProId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[GT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[GT_Master](
	[VoucherNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [varchar](10) NOT NULL,
	[FrmGdn] [int] NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Remarks] [varchar](1024) NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[CompanyUnit] [int] NULL,
	[BranchId] [int] NOT NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
 CONSTRAINT [PK__GodownTr__C5DCEA07636EBA21] PRIMARY KEY CLUSTERED 
(
	[VoucherNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Inv_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Inv_Details](
	[Inv_No] [varchar](15) NOT NULL,
	[SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[CC_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NOT NULL,
	[Alt_UnitId] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit_Id] [int] NULL,
	[AltStock_Qty] [decimal](18, 6) NOT NULL,
	[Stock_Qty] [decimal](18, 6) NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[Cnv_Ratio] [decimal](18, 6) NOT NULL,
	[Req_No] [varchar](15) NULL,
	[Req_SNo] [int] NULL,
	[Inv_TypeCode] [varchar](10) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Inv_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Inv_Master](
	[Inv_No] [varchar](15) NOT NULL,
	[Inv_Date] [datetime] NOT NULL,
	[Inv_Time] [datetime] NOT NULL,
	[Inv_Miti] [varchar](10) NOT NULL,
	[Gdn_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[GLId] [bigint] NULL,
	[CBranch_Id] [int] NULL,
	[CUnit_Id] [int] NULL,
	[CC_Id] [int] NULL,
	[SB_No] [varchar](250) NULL,
	[SB_SNo] [int] NULL,
	[SO_No] [varchar](250) NULL,
	[BOM_Id] [nvarchar](50) NULL,
	[BOM_Desc] [varchar](500) NULL,
	[Auth_By] [varchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Req_No] [varchar](256) NULL,
	[Ass_No] [varchar](50) NULL,
	[Source] [varchar](50) NULL,
	[FGR_No] [varchar](50) NULL,
	[FGR_Qty] [decimal](16, 6) NULL,
	[Export] [char](1) NULL,
	[Remarks] [varchar](1024) NULL,
	[Enter_By] [varchar](15) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[FiscalYearId] [int] NULL,
 CONSTRAINT [PK_Inv_Master] PRIMARY KEY CLUSTERED 
(
	[Inv_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[InventorySetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [AMS].[InventorySetting](
	[InvId] [tinyint] NOT NULL,
	[OPLedgerId] [bigint] NULL,
	[CSPLLedgerId] [bigint] NULL,
	[CSBSLedgerId] [bigint] NULL,
	[NegativeStock] [char](1) NULL,
	[AlternetUnit] [bit] NULL,
	[CostCenterEnable] [bit] NULL,
	[CostCenterMandetory] [bit] NULL,
	[CostCenterItemEnable] [bit] NULL,
	[CostCenterItemMandetory] [bit] NULL,
	[ChangeUnit] [bit] NULL,
	[GodownEnable] [bit] NULL,
	[GodownMandetory] [bit] NULL,
	[RemarksEnable] [bit] NULL,
	[GodownItemEnable] [bit] NULL,
	[GodownItemMandetory] [bit] NULL,
	[NarrationEnable] [bit] NULL,
	[ShortNameWise] [bit] NULL,
	[BatchWiseQtyEnable] [bit] NULL,
	[ExpiryDate] [bit] NULL,
	[FreeQty] [bit] NULL,
	[GodownWiseFilter] [bit] NULL,
	[GodownWiseStock] [bit] NULL,
 CONSTRAINT [PK_InventorySetting] PRIMARY KEY CLUSTERED 
(
	[InvId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[IRDAPISetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[IRDAPISetting](
	[IRDAPI] [nvarchar](max) NULL,
	[IrdUser] [nvarchar](500) NULL,
	[IrdUserPassword] [nvarchar](500) NULL,
	[IrdCompanyPan] [nvarchar](50) NULL,
	[IsIRDRegister] [tinyint] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[JuniorAgent]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[JuniorAgent](
	[AgentId] [int] NOT NULL,
	[AgentName] [nvarchar](200) NOT NULL,
	[AgentCode] [nvarchar](50) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[PhoneNo] [nvarchar](50) NOT NULL,
	[GLCode] [bigint] NULL,
	[Commission] [decimal](18, 6) NOT NULL,
	[SAgent] [int] NULL,
	[Email] [nvarchar](200) NULL,
	[CRLimit] [numeric](18, 8) NOT NULL,
	[CrDays] [nvarchar](50) NOT NULL,
	[CrType] [nvarchar](50) NOT NULL,
	[Branch_id] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED 
(
	[AgentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_JuniorAgentShortName] UNIQUE NONCLUSTERED 
(
	[AgentCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[JV_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[JV_Details](
	[Voucher_No] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CBLedger_Id] [bigint] NULL,
	[Chq_No] [nvarchar](50) NULL,
	[Chq_Date] [datetime] NULL,
	[Debit] [decimal](18, 6) NOT NULL,
	[Credit] [decimal](18, 6) NOT NULL,
	[LocalDebit] [decimal](18, 6) NOT NULL,
	[LocalCredit] [decimal](18, 6) NOT NULL,
	[Narration] [varchar](1024) NULL,
	[Tbl_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[Vat_Reg] [bit] NULL,
	[Party_No] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[VatLedger_Id] [nvarchar](50) NULL,
	[PanNo] [int] NULL,
 CONSTRAINT [PK_JV_Details] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC,
	[SNo] ASC,
	[Ledger_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[JV_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[JV_Master](
	[VoucherMode] [char](10) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](10) NOT NULL,
	[Voucher_Time] [datetime] NOT NULL,
	[Ref_VNo] [varchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[Currency_Id] [int] NOT NULL,
	[Currency_Rate] [decimal](18, 6) NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[Remarks] [varchar](1024) NULL,
	[Action_Type] [nvarchar](50) NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Audit_Lock] [bit] NULL,
	[ReconcileBy] [nvarchar](50) NULL,
	[ReconcileDate] [datetime] NULL,
	[ClearingBy] [nvarchar](50) NULL,
	[ClearingDate] [datetime] NULL,
	[PrintValue] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NULL,
	[FiscalYearId] [int] NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_Voucher_Main] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[LedgerOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[LedgerOpening](
	[Opening_Id] [int] NOT NULL,
	[Module] [char](10) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[OP_Date] [datetime] NOT NULL,
	[OP_Miti] [nvarchar](50) NOT NULL,
	[Serial_No] [int] NOT NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Currency_Id] [int] NOT NULL,
	[Currency_Rate] [decimal](18, 6) NOT NULL,
	[Debit] [decimal](18, 6) NOT NULL,
	[LocalDebit] [decimal](18, 6) NOT NULL,
	[Credit] [decimal](18, 6) NOT NULL,
	[LocalCredit] [decimal](18, 6) NOT NULL,
	[Narration] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Branch_Id] [int] NOT NULL,
	[Company_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_LedgerOpening] PRIMARY KEY CLUSTERED 
(
	[Opening_Id] ASC,
	[Module] ASC,
	[Voucher_No] ASC,
	[Serial_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[MainArea]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[MainArea](
	[MAreaId] [int] NOT NULL,
	[MAreaName] [nvarchar](100) NOT NULL,
	[MAreaCode] [nvarchar](50) NOT NULL,
	[MCountry] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_MainArea] PRIMARY KEY CLUSTERED 
(
	[MAreaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_MainAreaDesc] UNIQUE NONCLUSTERED 
(
	[MAreaName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_MainAreaShortName] UNIQUE NONCLUSTERED 
(
	[MAreaCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[MemberShipSetup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[MemberShipSetup](
	[MShipId] [int] NOT NULL,
	[MShipDesc] [nvarchar](200) NOT NULL,
	[MShipShortName] [nvarchar](50) NOT NULL,
	[PhoneNo] [nvarchar](50) NULL,
	[LedgerId] [bigint] NOT NULL,
	[EmailAdd] [nvarchar](200) NULL,
	[MemberTypeId] [int] NOT NULL,
	[MemberId] [nvarchar](50) NOT NULL,
	[BranchId] [int] NOT NULL,
	[CompanyUnitId] [int] NULL,
	[MValidDate] [datetime] NOT NULL,
	[MExpireDate] [datetime] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[ActiveStatus] [bit] NOT NULL,
 CONSTRAINT [PK_MemberShipSetup] PRIMARY KEY CLUSTERED 
(
	[MShipId] ASC,
	[MShipDesc] ASC,
	[MShipShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[MemberType]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[MemberType](
	[MemberTypeId] [int] NOT NULL,
	[MemberDesc] [nvarchar](200) NOT NULL,
	[MemberShortName] [nvarchar](50) NOT NULL,
	[Discount] [decimal](18, 6) NOT NULL,
	[BranchId] [int] NOT NULL,
	[CompanyUnitId] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[ActiveStatus] [bit] NOT NULL,
 CONSTRAINT [PK_MemberType] PRIMARY KEY CLUSTERED 
(
	[MemberTypeId] ASC,
	[MemberDesc] ASC,
	[MemberShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[Notes_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[Notes_Details](
	[VoucherMode] [char](10) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Debit] [decimal](18, 6) NOT NULL,
	[Credit] [decimal](18, 6) NOT NULL,
	[LocalDebit] [decimal](18, 6) NOT NULL,
	[LocalCredit] [decimal](18, 6) NOT NULL,
	[Narration] [varchar](1024) NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Party_No] [nvarchar](50) NULL,
	[Invoice_Date] [datetime] NULL,
	[Invoice_Miti] [nvarchar](50) NULL,
	[VatLedger_Id] [bigint] NULL,
	[PanNo] [numeric](18, 0) NULL,
	[Vat_Reg] [bit] NULL,
 CONSTRAINT [PK_Notes_Details] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC,
	[SNo] ASC,
	[Ledger_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Notes_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[Notes_Master](
	[VoucherMode] [nvarchar](50) NOT NULL,
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Voucher_Date] [datetime] NOT NULL,
	[Voucher_Miti] [nvarchar](10) NOT NULL,
	[Voucher_Time] [datetime] NOT NULL,
	[Ref_VNo] [nvarchar](50) NULL,
	[Ref_VDate] [datetime] NULL,
	[VoucherType] [nvarchar](50) NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[CheqNo] [nvarchar](50) NULL,
	[CheqDate] [datetime] NULL,
	[CheqMiti] [nvarchar](50) NULL,
	[Subledger_Id] [int] NULL,
	[Agent_Id] [int] NULL,
	[Currency_Id] [int] NOT NULL,
	[Currency_Rate] [decimal](18, 6) NOT NULL,
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
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_Notes_Master] PRIMARY KEY CLUSTERED 
(
	[Voucher_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PAB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PAB_Details](
	[PAB_Invoice] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[PT_Id] [int] NOT NULL,
	[Ledger_Id] [bigint] NOT NULL,
	[CBLedger_Id] [bigint] NOT NULL,
	[Subledger_Id] [int] NOT NULL,
	[Agent_Id] [int] NULL,
	[Product_Id] [bigint] NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[Percentage] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[PAB_Narration] [varchar](1024) NULL,
 CONSTRAINT [PK_PAB_Details] PRIMARY KEY CLUSTERED 
(
	[PAB_Invoice] ASC,
	[SNo] ASC,
	[PT_Id] ASC,
	[Ledger_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PAB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PAB_Master](
	[PAB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Agent_Id] [int] NULL,
	[PB_Invoice] [nvarchar](50) NOT NULL,
	[PB_Date] [datetime] NOT NULL,
	[PB_Miti] [nvarchar](50) NOT NULL,
	[PB_Qty] [decimal](18, 6) NOT NULL,
	[PB_Amount] [decimal](18, 6) NOT NULL,
	[LocalAmount] [decimal](18, 0) NOT NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Action_Type] [nvarchar](50) NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NULL,
	[In_Words] [nvarchar](1024) NULL,
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
	[CUnit_Id] [int] NOT NULL,
	[CBranch_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL,
 CONSTRAINT [PK_PAB_Master] PRIMARY KEY CLUSTERED 
(
	[PAB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PB_Details](
	[PB_Invoice] [nvarchar](50) NOT NULL,
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
	[Narration] [nvarchar](1024) NULL,
	[PO_Invoice] [nvarchar](50) NULL,
	[PO_Sno] [numeric](18, 0) NULL,
	[PC_Invoice] [nvarchar](50) NULL,
	[PC_SNo] [nvarchar](50) NULL,
	[Tax_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[V_Rate] [decimal](18, 6) NOT NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
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
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
	[TaxExempted_Amount] [decimal](18, 6) NULL,
 CONSTRAINT [PK_PB_Details] PRIMARY KEY CLUSTERED 
(
	[PB_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PB_Master](
	[PB_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
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
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[PO_Invoice] [nvarchar](250) NULL,
	[PO_Date] [datetime] NULL,
	[PC_Invoice] [nvarchar](250) NULL,
	[PC_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Cur_Id] [int] NOT NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[Counter_ID] [int] NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[Tender_Amount] [decimal](18, 6) NOT NULL,
	[Change_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[Tbl_Amount] [decimal](18, 6) NOT NULL,
	[Action_type] [nvarchar](50) NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[CancelRemarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK_PB] PRIMARY KEY CLUSTERED 
(
	[PB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PB_OtherMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PB_OtherMaster](
	[PAB_Invoice] [nvarchar](50) NOT NULL,
	[PPNo] [nvarchar](50) NOT NULL,
	[PPDate] [datetime] NOT NULL,
	[TaxableAmount] [decimal](18, 6) NOT NULL,
	[VatAmount] [decimal](18, 6) NOT NULL,
	[CustomAgent] [nvarchar](50) NULL,
	[Transportation] [nvarchar](50) NULL,
	[VechileNo] [nvarchar](50) NULL,
	[Cn_No] [varchar](25) NULL,
	[Cn_Date] [datetime] NULL,
	[BankDoc] [nvarchar](50) NULL,
 CONSTRAINT [PK_Purchase_ImpDoc] PRIMARY KEY CLUSTERED 
(
	[PAB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Purchase__2FF6D3DA42E1EEFE] UNIQUE NONCLUSTERED 
(
	[PAB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PB_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PB_Term](
	[PB_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NOT NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_PB_Term] PRIMARY KEY CLUSTERED 
(
	[PB_VNo] ASC,
	[PT_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PBT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PBT_Details](
	[PBT_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
	[Slb_Id] [int] NOT NULL,
	[PGrp_Id] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Flight_Date] [datetime] NOT NULL,
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
 CONSTRAINT [PK_PBT_Details] PRIMARY KEY CLUSTERED 
(
	[PBT_Invoice] ASC,
	[Invoice_SNo] ASC,
	[Slb_Id] ASC,
	[PGrp_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PBT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PBT_Master](
	[PBT_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[Ref_Vno] [nvarchar](50) NULL,
	[Ref_Date] [datetime] NULL,
	[Vendor_Id] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_In] [nvarchar](50) NULL,
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
	[V_Amount] [decimal](18, 6) NOT NULL,
	[Tbl_Amount] [decimal](18, 6) NOT NULL,
	[Action_type] [nvarchar](50) NOT NULL,
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
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [nvarchar](50) NULL,
	[CancelRemarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[R_Invoice] [bit] NULL,
	[Is_Printed] [bit] NULL,
	[Printed_By] [nvarchar](50) NULL,
	[Printed_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[Cancel_By] [nvarchar](50) NULL,
	[Cancel_Date] [datetime] NULL,
	[Cancel_Remarks] [nvarchar](1024) NULL,
	[IsAPIPosted] [bit] NULL,
	[IsRealtime] [bit] NULL,
 CONSTRAINT [PK_PBT] PRIMARY KEY CLUSTERED 
(
	[PBT_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PBT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PBT_Term](
	[PBT_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NULL,
	[Amount] [decimal](18, 6) NULL,
	[Term_Type] [char](2) NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_PBT_Term] PRIMARY KEY CLUSTERED 
(
	[PBT_VNo] ASC,
	[PT_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PC_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
 CONSTRAINT [PK_PC_Details] PRIMARY KEY CLUSTERED 
(
	[PC_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PC_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PC_Master](
	[PC_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
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
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[PO_Invoice] [nvarchar](250) NULL,
	[PO_Date] [datetime] NULL,
	[QOT_Invoice] [nvarchar](250) NULL,
	[QOT_Date] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Cur_Id] [int] NULL,
	[Cur_Rate] [decimal](18, 6) NOT NULL,
	[Counter_ID] [int] NULL,
	[B_Amount] [decimal](18, 6) NOT NULL,
	[T_Amount] [decimal](18, 6) NOT NULL,
	[N_Amount] [decimal](18, 6) NOT NULL,
	[LN_Amount] [decimal](18, 6) NOT NULL,
	[Tender_Amount] [decimal](18, 6) NOT NULL,
	[Change_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[Tbl_Amount] [decimal](18, 6) NOT NULL,
	[Action_type] [nvarchar](50) NULL,
	[R_Invoice] [bit] NULL,
	[No_Print] [int] NOT NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
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
 CONSTRAINT [PK_PC] PRIMARY KEY CLUSTERED 
(
	[PC_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PC_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PC_Term](
	[PC_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_PC_Term] PRIMARY KEY CLUSTERED 
(
	[PC_VNo] ASC,
	[PT_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PIN_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PIN_Details](
	[PIN_Invoice] [nvarchar](50) NOT NULL,
	[SNo] [int] NOT NULL,
	[P_Id] [bigint] NOT NULL,
	[Gdn_Id] [int] NULL,
	[Alt_Qty] [decimal](18, 6) NOT NULL,
	[Alt_Unit] [int] NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[Unit] [int] NULL,
	[AltStock_Qty] [decimal](18, 6) NOT NULL,
	[StockQty] [decimal](18, 6) NOT NULL,
	[Issue_Qty] [decimal](18, 6) NOT NULL,
	[Narration] [varchar](1024) NULL,
 CONSTRAINT [PK_PIN_Details] PRIMARY KEY CLUSTERED 
(
	[PIN_Invoice] ASC,
	[SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PIN_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PIN_Master](
	[PIN_Invoice] [nvarchar](50) NOT NULL,
	[PIN_Date] [datetime] NOT NULL,
	[PIN_Miti] [nvarchar](50) NOT NULL,
	[Person] [nvarchar](50) NOT NULL,
	[Sub_Ledger] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[EnterBY] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[ActionType] [char](10) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Print_value] [int] NULL,
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[CancelRemarks] [nvarchar](1024) NULL,
	[FiscalYearId] [int] NOT NULL,
	[BranchId] [int] NOT NULL,
	[CompanyUnitId] [int] NULL,
 CONSTRAINT [PK_PIN_Master] PRIMARY KEY CLUSTERED 
(
	[PIN_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PO_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PO_Details](
	[PO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_SNo] [numeric](18, 0) NOT NULL,
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
	[Narration] [nvarchar](1024) NULL,
	[PIN_Invoice] [nvarchar](50) NULL,
	[PIN_Sno] [int] NULL,
	[PQT_Invoice] [nvarchar](50) NULL,
	[PQT_SNo] [nvarchar](50) NULL,
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
	[Manu_Date] [datetime] NULL,
 CONSTRAINT [PK_PO_Details] PRIMARY KEY CLUSTERED 
(
	[PO_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PO_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PO_Master](
	[PO_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
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
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
	[PIN_Invoice] [nvarchar](250) NULL,
	[PIN_Date] [datetime] NULL,
	[PQT_Invoice] [nvarchar](250) NULL,
	[PQT_Date] [datetime] NULL,
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
	[V_Amount] [decimal](18, 6) NOT NULL,
	[Tbl_Amount] [decimal](18, 6) NOT NULL,
	[Action_type] [nvarchar](50) NOT NULL,
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
 CONSTRAINT [PK_PO] PRIMARY KEY CLUSTERED 
(
	[PO_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PO_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PO_Term](
	[PO_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_PO_Term] PRIMARY KEY CLUSTERED 
(
	[PO_VNo] ASC,
	[PT_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PostDateCheque]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PostDateCheque](
	[PDCId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNo] [nvarchar](50) NOT NULL,
	[VoucherDate] [datetime] NOT NULL,
	[VoucherMiti] [nvarchar](50) NOT NULL,
	[VoucherType] [nvarchar](50) NOT NULL,
	[BankName] [nvarchar](500) NOT NULL,
	[BranchName] [nvarchar](500) NOT NULL,
	[ChequeNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[ChqMiti] [nvarchar](50) NULL,
	[DrawOn] [nvarchar](500) NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[LedgerId] [bigint] NOT NULL,
	[SubLedgerId] [int] NULL,
	[AgentId] [int] NULL,
	[Remarks] [nvarchar](1024) NULL,
	[DepositedBy] [nvarchar](50) NULL,
	[DepositeDate] [datetime] NULL,
	[CancelReason] [nvarchar](1024) NULL,
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[CompanyUnitId] [int] NULL,
	[BranchId] [int] NOT NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[FiscalYearId] [int] NOT NULL,
	[VoucherTime] [date] NULL,
	[BankLedgerId] [bigint] NULL,
	[Status] [nvarchar](50) NULL,
	[PAttachment1] [image] NULL,
	[PAttachment2] [image] NULL,
	[PAttachment3] [image] NULL,
	[PAttachment4] [image] NULL,
	[PAttachment5] [image] NULL,
 CONSTRAINT [PK__PDC__A96E03572C1E8537] PRIMARY KEY CLUSTERED 
(
	[PDCId] ASC,
	[VoucherNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__PDC__C5DCEA062EFAF1E2] UNIQUE NONCLUSTERED 
(
	[VoucherNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[PR_Details](
	[PR_Invoice] [nvarchar](50) NOT NULL,
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
	[Narration] [nvarchar](1024) NULL,
	[PB_Invoice] [nvarchar](50) NULL,
	[PB_Sno] [int] NULL,
	[Tax_Amount] [decimal](18, 6) NOT NULL,
	[V_Amount] [decimal](18, 6) NOT NULL,
	[V_Rate] [decimal](18, 6) NOT NULL,
	[Free_Unit_Id] [int] NULL,
	[Free_Qty] [decimal](18, 6) NULL,
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
	[SZ10] [nvarchar](50) NULL,
	[Serial_No] [nvarchar](50) NULL,
	[Batch_No] [nvarchar](50) NULL,
	[Exp_Date] [datetime] NULL,
	[Manu_Date] [datetime] NULL,
 CONSTRAINT [PK_PR_Details] PRIMARY KEY CLUSTERED 
(
	[PR_Invoice] ASC,
	[Invoice_SNo] ASC,
	[P_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PR_Master](
	[PR_Invoice] [nvarchar](50) NOT NULL,
	[Invoice_Date] [datetime] NOT NULL,
	[Invoice_Miti] [nvarchar](10) NOT NULL,
	[Invoice_Time] [datetime] NOT NULL,
	[PB_Invoice] [nvarchar](50) NULL,
	[PB_Date] [datetime] NULL,
	[PB_Miti] [varchar](10) NULL,
	[Vendor_ID] [bigint] NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[Vat_No] [nvarchar](50) NULL,
	[Contact_Person] [nvarchar](50) NULL,
	[Mobile_No] [nvarchar](50) NULL,
	[Address] [nvarchar](90) NULL,
	[ChqNo] [nvarchar](50) NULL,
	[ChqDate] [datetime] NULL,
	[Invoice_Type] [nvarchar](50) NULL,
	[Invoice_In] [nvarchar](50) NULL,
	[DueDays] [int] NULL,
	[DueDate] [datetime] NULL,
	[Agent_Id] [int] NULL,
	[Subledger_Id] [int] NULL,
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
	[Tender_Amount] [decimal](18, 6) NULL,
	[Change_Amount] [decimal](18, 6) NULL,
	[V_Amount] [decimal](18, 6) NULL,
	[Tbl_Amount] [decimal](18, 6) NULL,
	[Action_type] [char](10) NOT NULL,
	[No_Print] [decimal](18, 0) NULL,
	[In_Words] [nvarchar](1024) NULL,
	[Remarks] [nvarchar](1024) NULL,
	[Audit_Lock] [bit] NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [datetime] NULL,
	[Auth_By] [nvarchar](50) NULL,
	[Auth_Date] [datetime] NULL,
	[Cleared_By] [nvarchar](50) NULL,
	[Cleared_Date] [datetime] NULL,
	[CancelBy] [nvarchar](50) NULL,
	[CancelDate] [datetime] NULL,
	[CancelRemarks] [nvarchar](1024) NULL,
	[CUnit_Id] [int] NULL,
	[CBranch_Id] [int] NOT NULL,
	[FiscalYearId] [int] NULL,
 CONSTRAINT [PK_PR] PRIMARY KEY CLUSTERED 
(
	[PR_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PR_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[PR_Term](
	[PR_VNo] [nvarchar](50) NOT NULL,
	[PT_Id] [int] NOT NULL,
	[SNo] [int] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[Term_Type] [char](2) NULL,
	[Product_Id] [bigint] NULL,
	[Taxable] [char](1) NULL,
 CONSTRAINT [PK_PR_Term_1] PRIMARY KEY CLUSTERED 
(
	[PR_VNo] ASC,
	[PT_Id] ASC,
	[SNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Product]    Script Date: 06/08/2020 10:12:01 PM ******/
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
	[PCategory] [nvarchar](50) NOT NULL,
	[PUnit] [int] NULL,
	[PAltUnit] [int] NULL,
	[PQtyConv] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PQtyConv]  DEFAULT ((0)),
	[PAltConv] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PAltConv]  DEFAULT ((0)),
	[PValTech] [nvarchar](50) NULL,
	[PSerialno] [bit] NULL CONSTRAINT [DF_Product_PSerialno]  DEFAULT ((0)),
	[PSizewise] [bit] NULL CONSTRAINT [DF_Product_PSizewise]  DEFAULT ((0)),
	[PBatchwise] [bit] NULL CONSTRAINT [DF_Product_PBatchwise]  DEFAULT ((0)),
	[PBuyRate] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PBuyRate]  DEFAULT ((0)),
	[PSalesRate] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PSalesRate]  DEFAULT ((0)),
	[PMargin1] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMargin1]  DEFAULT ((0)),
	[TradeRate] [decimal](18, 6) NULL CONSTRAINT [DF_Product_TradeRate]  DEFAULT ((0)),
	[PMargin2] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMargin2]  DEFAULT ((0)),
	[PMRP] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMRP]  DEFAULT ((0)),
	[PGrpId] [int] NULL,
	[PSubGrpId] [int] NULL,
	[PTax] [decimal](18, 6) NOT NULL CONSTRAINT [DF_Product_PTax]  DEFAULT ((0)),
	[PMin] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMin]  DEFAULT ((0.00)),
	[PMax] [decimal](18, 6) NULL CONSTRAINT [DF_Product_PMax]  DEFAULT ((0)),
	[CmpId] [int] NULL,
	[CmpId1] [int] NULL,
	[CmpId2] [int] NULL,
	[CmpId3] [int] NULL,
	[Branch_Id] [int] NOT NULL,
	[CmpUnit_Id] [int] NULL,
	[PPL] [bigint] NULL,
	[PPR] [bigint] NULL,
	[PSL] [bigint] NULL,
	[PSR] [bigint] NULL,
	[PL_Opening] [bigint] NULL,
	[PL_Closing] [bigint] NULL,
	[BS_Closing] [bigint] NULL,
	[PImage] [image] NULL,
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_Product_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_Product_Status]  DEFAULT ((1)),
	[BeforeBuyRate] [decimal](18, 6) NULL,
	[BeforeSalesRate] [decimal](18, 6) NULL,
	[Barcode] [nvarchar](100) NULL,
	[ChasisNo] [nvarchar](100) NULL,
	[EngineNo] [nvarchar](100) NULL,
	[VHColor] [nvarchar](100) NULL,
	[VHModel] [nvarchar](100) NULL,
	[VHNumber] [nvarchar](100) NULL,
	[Barcode1] [nvarchar](100) NULL,
	[Barcode2] [nvarchar](100) NULL,
	[Barcode3] [nvarchar](100) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductDesc] UNIQUE NONCLUSTERED 
(
	[PName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductShortName] UNIQUE NONCLUSTERED 
(
	[PShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ProductClosingRate]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[ProductClosingRate](
	[PCRate_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[Date_Type] [varchar](5) NULL,
	[Month_Date] [datetime] NULL,
	[Month_Miti] [varchar](50) NULL,
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NOT NULL,
 CONSTRAINT [PK_ProductClosingRate] PRIMARY KEY CLUSTERED 
(
	[PCRate_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[ProductGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[ProductGroup](
	[PGrpID] [int] NOT NULL,
	[GrpName] [nvarchar](200) NOT NULL,
	[GrpCode] [nvarchar](50) NOT NULL,
	[GMargin] [decimal](18, 6) NULL CONSTRAINT [DF_ProductGroup_GMargin]  DEFAULT ((0.00)),
	[Gprinter] [nvarchar](50) NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_ProductGroup_Status]  DEFAULT ((1)),
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_ProductGroup_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED 
(
	[PGrpID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductGroupDesc] UNIQUE NONCLUSTERED 
(
	[GrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductGroupShortName] UNIQUE NONCLUSTERED 
(
	[GrpCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ProductOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[ProductOpening](
	[Voucher_No] [nvarchar](50) NOT NULL,
	[Serial_No] [int] NOT NULL,
	[OP_Date] [datetime] NOT NULL,
	[OP_Miti] [varchar](10) NOT NULL,
	[Product_Id] [bigint] NOT NULL,
	[Godown_Id] [int] NULL,
	[Cls1] [int] NULL,
	[Cls2] [int] NULL,
	[Cls3] [int] NULL,
	[Cls4] [int] NULL,
	[Currency_Id] [int] NULL,
	[Currency_Rate] [decimal](18, 6) NULL,
	[AltQty] [decimal](18, 6) NULL,
	[AltUnit] [decimal](18, 6) NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[QtyUnit] [int] NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[LocalRate] [decimal](18, 6) NOT NULL,
	[Amount] [decimal](18, 6) NOT NULL,
	[LocalAmount] [decimal](18, 6) NOT NULL,
	[Enter_By] [nvarchar](50) NOT NULL,
	[Enter_Date] [datetime] NOT NULL,
	[Reconcile_By] [nvarchar](50) NULL,
	[Reconcile_Date] [nvarchar](50) NULL,
	[CBranch_Id] [int] NOT NULL,
	[CUnit_Id] [int] NULL,
	[FiscalYearId] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[ProductSubGroup]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[ProductSubGroup](
	[PSubGrpId] [int] NOT NULL,
	[SubGrpName] [nvarchar](80) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
	[GrpId] [int] NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL,
	[EnterDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_ProductSubGroup] PRIMARY KEY CLUSTERED 
(
	[PSubGrpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductSubGroupDesc] UNIQUE NONCLUSTERED 
(
	[SubGrpName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductSubGroupShortName] UNIQUE NONCLUSTERED 
(
	[ShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[ProductUnit]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[ProductUnit](
	[UID] [int] NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
	[UnitCode] [nvarchar](50) NOT NULL,
	[Branch_ID] [int] NOT NULL,
	[Company_ID] [int] NULL,
	[EnterBy] [nvarchar](50) NOT NULL CONSTRAINT [DF_ProductUnit_EnterBy]  DEFAULT (N'MrSolution'),
	[EnterDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL CONSTRAINT [DF_ProductUnit_Status]  DEFAULT ((1)),
 CONSTRAINT [PK_ProductUnit] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductUnitDesc] UNIQUE NONCLUSTERED 
(
	[UnitName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_ProductUnitShortName] UNIQUE NONCLUSTERED 
(
	[UnitCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[PROV_CB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[ProvCB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[ProvCB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PRT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[PurchaseSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[PBGodownEnable] [bit] NULL,
	[PBGodownMandetory] [bit] NULL,
	[PBAlternetUnitEnable] [bit] NULL,
	[PBIndent] [bit] NULL,
	[PBNarration] [bit] NULL,
	[PBBasicAmount] [bit] NULL,
 CONSTRAINT [PK_PurchaseSetting] PRIMARY KEY CLUSTERED 
(
	[PurId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[ReportTemplate]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SalesSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
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
	[SBGodownEnable] [bit] NULL,
	[SBGodownMandetory] [bit] NULL,
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SampleCosting_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SampleCosting_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SB_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SB_ExchangeDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SB_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SB_Master_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SB_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SBT_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SBT_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SBT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SC_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SC_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SC_Master_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SC_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Scheme_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[Scheme_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SectorMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SeniorAgent]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SO_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SO_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SO_Master_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SO_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SQ_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SQ_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SQ_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SR_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SR_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SR_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SRT_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[ST_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[STA_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [AMS].[STA_Details](
	[StockAdjust_No] [nvarchar](50) NOT NULL,
	[Sno] [int] NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[GodownId] [int] NULL,
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[STA_Master]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[StockBatchDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[StockDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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
	[Godown_Id] [int] NULL,
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[Subledger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[SystemConfiguration]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SystemControlOptions]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[SystemSetting]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[TableMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[TicketRefund]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[UDF]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[UDF_Entry]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [AMS].[UnitWiseLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [AMS].[UnitWiseLedger](
	[UnitId] [int] NULL,
	[BranchId] [int] NULL,
	[LedgerId] [bigint] NULL,
	[Mapped] [bit] NULL,
	[Category] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [AMS].[VehicleColors]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[VehicleNumber]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [AMS].[VehileModel]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[BedMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[BedType]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[Department]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [HOS].[Doctor]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[DoctorCommission]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[DoctorCommissionItem]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[DoctorType]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[PatientBillingDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  Table [HOS].[PatientBillingMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [HOS].[PatientBillingTerm]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [HOS].[PatientMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
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

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [HOS].[WardMaster]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
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

GO
/****** Object:  View [AMS].[Company_Profiles]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[Company_Profiles] as Select Database_Name [DATABASE_ID],Company_Name as [COMPANY DESC],Company_Logo [lOGO],CReg_Date [REGISTRATION DATE], Address [COMPANY ADDRESS],Country [COUNTRY],State [STATE],City [CITY],PhoneNo [OFFICE NUMBER], Fax [FAX NO],Pan_No [TPAN/VAT] from AMS.CompanyInfo
				
GO
/****** Object:  View [AMS].[MaterializedView]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[MaterializedView] as
						Select (Select Top 1 'F/Y ' + BS_FY from AMS.FiscalYear where Current_FY=1)as Fiscal_Year, SBM.[SB_Invoice]as Bill_No, Gl.[GlName]as [Customer_Name],
						gl.PanNo [Customer_PAN] ,  SBM.[Invoice_Date] as Bill_Date,SBM.[Invoice_Miti] as Bill_Miti,Isnull(convert(decimal(16,2),Sum(SBD.[B_Amount])*SBM.[Cur_Rate]),0)as Amount,
						convert(decimal(16,2),IsNull(discount.LocalAmt,0)) as Discount,
						case when Vat.LocalAmt<> 0 then isnull(convert(decimal(16,2),Sum(SBD.[B_Amount]* SBM.[Cur_Rate])+IsNull(discount.LocalAmt,0)),0) else 0 end
						as Taxable_Amount,
						(isnull(convert(decimal(16,2),Vat.LocalAmt),0)) as Tax_Amount,
						isnull(No_Print,0) as Is_Printed,  (case when Enter_By = 'Cancel' then 'N' else 'Y' end) as Is_Active ,
						Printed_Date [Printed_Time],  Enter_By [Entered_By], isnull(Printed_By,'') [Printed_By],case when IsAPIPosted IS NULL then 'NO' when IsAPIPosted = 0 then 'NO' else 'YES' end as [SyncWithIRD],case when IsRealtime is null then 'NO' when IsRealtime = '0' then 'NO' else 'YES' end as [IsRealtime]
						from AMS.SB_Master as SBM Inner Join AMS.SB_Details as SBD On SBD.SB_Invoice=SBM.SB_Invoice
						Left outer join AMS.GeneralLedger as Gl on Gl.GLID=SBM.Customer_ID
						left outer join(
						Select SB_VNo, sum(case when ST_Sign = '+' then Amount else -Amount end) as LocalAmt from AMS.ST_Term as ST,AMS.SB_Term as SBT
						Where ST.ST_Id<(Select SalesVat_Id from AMS.SystemConfiguration  )
						and ST.ST_Id = SBT.ST_Id and Term_Type in ('P','BT') Group by SB_VNo )  as Discount on discount.SB_VNo=SBM.SB_Invoice
						Left outer join(Select SB_VNo, sum(case when ST_Sign = '+' then Amount else -Amount end) as LocalAmt
						from AMS.ST_Term as ST,AMS.SB_Term as SBT
						Where ST.ST_Id in (Select SalesVat_Id from AMS.SystemConfiguration ) and ST.ST_Id = SBT.ST_Id and Term_Type in ('P','BT')
						Group by SB_VNo ) as  Vat on Vat.SB_VNo=SBM.SB_Invoice
						Group by SBM.SB_Invoice,Gl.GLName,Gl.PanNo,SBM.Invoice_Date,Invoice_Miti,Discount.LocalAmt,Vat.LocalAmt,
						SBM.No_Print,SBM.Action_Type,SBM.Printed_Date,SBM.Enter_By, SBM.Printed_By ,SBM.[Cur_Rate],IsAPIPosted,IsRealtime
					
GO
/****** Object:  View [AMS].[MaterializedViewSalesReturn]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[MaterializedViewSalesReturn] as
				Select(Select Top 1 'F/Y ' + BS_FY from AMS.FiscalYear where Current_FY = 1) as Fiscal_Year,
				(Select top 1 Pan_No from AMS.CompanyInfo) as [Seller Pan No], SRH.[SR_Invoice] as Bill_No, SRH.SB_Invoice RefNo, Gl.GLName as [Customer_Name], Gl.PanNo[Customer_PAN] ,
				SRH.[Invoice_Date] as Bill_Date,SRH.[Invoice_Miti] as Bill_Miti,
				Isnull(convert(decimal(16, 2), Sum(SRD.[B_Amount]) * SRH.[Cur_Rate]), 0) as Amount,
				(isnull(convert(decimal(16, 2), IsNull(discount.LocalAmt, 0) + IsNull(Details.LocalAmt, 0)), 0)) as Discount,
				isnull(convert(decimal(16, 2), Sum(SRD.[B_Amount]) + IsNull(Details.LocalAmt, 0) + IsNull(discount.LocalAmt, 0)), 0) as Taxable_Amount,
				(isnull(convert(decimal(16, 2), Vat.LocalAmt), 0)) as Tax_Amount,
				(case when Action_Type = 'Cancel' then 'N' else 'Y' end) as Is_Active ,  Enter_By[Entered_By]
				from AMS.SR_Master as SRH Inner Join AMS.SR_Details as SRD On SRD.SR_Invoice = SRH.SR_Invoice Left outer join AMS.GeneralLedger as Gl on Gl.GLID = SRH.Customer_ID
				left outer join(Select SR_VNo, sum(case when ST_Sign = '+' then Amount else -Amount end) as LocalAmt from AMS.ST_Term as ST, AMS.SR_Term as SBT
				Where ST.ST_Id in (Select SalesDiscount_Id from AMS.SystemConfiguration  ) and ST.ST_Id = SBT.ST_Id and Term_Type in ('P', 'BT') Group by SR_VNo )
				as Discount on discount.SR_VNo = SRH.SR_Invoice
				Left outer join(Select SR_VNo, sum(case when ST_Sign = '+' then Amount else -Amount end) as LocalAmt
				from AMS.ST_Term as ST, AMS.SR_Term as SBT Where ST.ST_Id in (select SalesVat_Id from AMS.SystemConfiguration ) and ST.ST_Id = SBT.ST_Id and Term_Type in ('P', 'BT')
				Group by SR_VNo ) as Vat on Vat.SR_VNo = SRH.SR_Invoice  Left Outer join(
				Select SR_VNo, sum(case when ST_Sign = '+' then Amount else -Amount end) as LocalAmt from AMS.ST_Term as ST, AMS.SR_Term as SBT
				Where ST.ST_Id < (select SalesVat_Id from AMS.SystemConfiguration ) and ST.ST_Id not in (Select SalesDiscount_Id from AMS.SystemConfiguration  )
				and ST.ST_Id = SBT.ST_Id and Term_Type in ('P', 'BT') Group by SR_VNo ) as details on details.SR_VNo = SRH.SR_Invoice
				Group by SRH.SR_Invoice,Gl.GLName,Gl.PanNo,SRH.SB_Invoice,Invoice_Date,Invoice_Miti,Discount.LocalAmt,details.LocalAmt,Vat.LocalAmt,
				SRH.Enter_By,Action_Type,SRH.[Cur_Rate]
				
GO
/****** Object:  View [AMS].[ViewCASHBANK_DETAILS]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewCASHBANK_DETAILS]  AS SELECT [Voucher_No] V_VOUCHERNO,[SNo] V_SNO,[Ledger_Id] V_LEDGERID,GLName V_LEDGERDESC,[Subledger_Id] V_SUBLEDGERIF,SLName V_SUBLEDGERDESC,[Agent_Id] V_AGENTID,AgentName V_AGENTDESC,[Cls1] V_DEPARTMENTID,DName V_DEPARTMENTDESC,DCode V_DEPARTMENTCODE,[Cls2] V_DEPARTMENT2,[Cls3] V_DEPARTMENT3,[Cls4] V_DEPARTMENT4,[Debit] V_DEBIT,[Credit] V_CREDIT,[LocalDebit] V_LOCALDEBIT,[LocalCredit] V_LOCALCREDIT,[Narration] V_NARRATION,[Tbl_Amount] V_TAXABLEAMOUNT,[V_Amount] V_VATAMOUNT,[Party_No] V_PARTYNO,[Invoice_Date] V_PARTYINVOICEDATE,[Invoice_Miti] V_INVOICEMITI,[VatLedger_Id] V_LEDGERDID,CD.[PanNo] V_PANNO,[Vat_Reg] V_VATREGISTRATION FROM [AMS].[CB_Details] AS CD LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID=CD.Ledger_Id LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId=CD.Subledger_Id LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId=CD.Agent_Id LEFT OUTER JOIN AMS.Department AS D ON D.DId=CD.Cls1
				
GO
/****** Object:  View [AMS].[ViewCASHBANK_HEADER]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewCASHBANK_HEADER] AS SELECT [VoucherMode] V_MODE,[Voucher_No] V_NUMBER,[Voucher_Date] V_VOUCHERDATE,[Voucher_Miti] V_MITI,[Voucher_Time] V_TIME,[Ref_VNo] V_REFNO,[Ref_VDate] V_DATE,[VoucherType] V_TYPE,[Ledger_Id] V_LEDGERID,GLName AS V_LEDGER,[CheqNo] V_CHEQUENO,[CheqDate] V_CHEQUEDATE,[CheqMiti] V_CHEQUEMITI,[Currency_Id] V_CURRENCYID,CName V_CURRNECYDESC,Ccode V_CURRENCYCODE,[Currency_Rate] V_CURRENCYRATE, [Cls1] V_DEPARTMENTID,DName V_DEPARTMENTDESC,DCode V_DEPARTMENTCODE,[Cls2] V_DEPARTMENT2,[Cls3] V_DEPARTMENT3,[Cls4] V_DEPARTMENT4,[Remarks] V_REMARKS,[Action_Type] V_ACTIONTYPE,CM.[EnterBy] V_ENTRYUSER,CM.[EnterDate] V_ENTERDATE,[ReconcileBy] V_RECONCILEBY,[ReconcileDate] V_RECONCILEDATE,[Audit_Lock] V_AUDITLOCK,[ClearingBy],[ClearingDate] V_CLEARINGDATE,[PrintValue] V_PRINT,[CBranch_Id] V_BRANCHID,Branch_Name V_BRANCHDESC,Branch_Code V_BRANCHCODE ,[CUnit_Id] V_COMPANYUNITID,CmpUnit_Name V_COMPANYUNITDESC,CmpUnit_Code V_COMPANYUNITCODE FROM [AMS].[CB_Master] AS CM                         LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID=CM.Ledger_Id LEFT OUTER JOIN AMS.Department AS D ON D.DId=CM.Cls1 LEFT OUTER JOIN AMS.Branch AS B ON B.Branch_Id=CM.CBranch_Id LEFT OUTER JOIN AMS.Currency AS C ON C.CId=CM.Currency_Id LEFT OUTER JOIN AMS.CompanyUnit CU ON CU.CmpUnit_Id=CM.CUnit_Id 
				
GO
/****** Object:  View [AMS].[ViewGeneralLedger]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewGeneralLedger] AS
                           SELECT GL.GLID [LEDGERID], GL.GLName [LEDGER DESC], GL.GLCode [SHORTNAME],AG.GrpName [ACGROUP DESC],GL.ACCode [ACCODE], AG.GrpType [AC TYPE], ASG.SubGrpName [SUB ACGROUP DESC], ASG.SubGrpCode [SUB ACCODE],
	                       GL.PanNo [PAN NO],AR.AreaName [AREA DESC], JA.AgentName [AGENT DESC], CU.CName [CURRENCRY],CU.CRate [CURRENCY RATE] ,GL.CrDays [CREDIT DAYS], GL.CrLimit [CREDIT LIMIT], GL.CrTYpe [CREDIT TYPE],  GL.IntRate [RATE], 
	                       GL.GLAddress [ADDRESS],GL.PhoneNo [PHONE NO],GL.LandLineNo [LANDLINE NO], GL.OwnerName [OWNER NAME], GL.OwnerNumber [OWNER NUMBER], GL.Scheme [SCHEME],GL.Email [EMAIL], BA.Branch_Name [BRANCH DESC],CM.CmpUnit_Name [COMPANY DESC],
                           GL.EnterBy [ENTER BY],GL.EnterDate [ENTER DATE],GL.Status [STATUS], GL.IsDefault [DEFAULT],GL.NepaliDesc [NEPALI DESC] FROM AMS.GeneralLedger GL
	                       LEFT OUTER JOIN AMS.AccountGroup AS AG ON AG.GrpId = GL.GrpId
	                       LEFT OUTER JOIN AMS.AccountSubGroup AS ASG ON ASG.SubGrpId = GL.SubGrpId
	                       LEFT OUTER JOIN AMS.Area AS AR ON AR.AreaId = GL.AreaId
	                       LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId = GL.AgentId
	                       LEFT OUTER JOIN AMS.Currency AS CU ON CU.CId = GL.CurrId
	                       LEFT OUTER JOIN AMS.Branch AS BA ON BA.Branch_Id = GL.Branch_id
	                       LEFT OUTER JOIN AMS.CompanyUnit CM ON CM.CmpUnit_Id = GL.Company_ID;
GO
/****** Object:  View [AMS].[ViewJournalVoucherAc]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 Create View [AMS].[ViewJournalVoucherAc] as SELECT ROW_NUMBER() OVER (ORDER BY Voucher_No,Debit_Amt DESC,Credit_Amt asc,Serial_No,Voucher_Miti,Voucher_Time) AS ROW_NO,Serial_No SNO,Module as SOURCE,Voucher_No [VOUCHER NO],Voucher_Date [VOUCHER DATE],Voucher_Miti [VOUCHER MITI],GL.GLName [LEDGER DESC],GLC.GLName [CASH LEDGER],ISNULL(SL.SLName,'No Subledger') [SUBLEDGER DESC],AgentName [SALES MAN],DP.DName [DEPARTMENT 1ST],DP1.DName [DEPARTMENT 2ND],DP2.DName [DEPARTMENT 3RD],DP3.DName [DEPARTMENT 4TH],
							Currency_Rate [CURRENCY RATE],Debit_Amt [DEBIT AMOUNT],LocalDebit_Amt [NPR DEBIT AMOUNT],Credit_Amt [CREDIT AMOUNT],LocalCredit_Amt [NPR CREDIT AMOUNT], DueDays [DUE DAYS], DueDate [DUE DATE], Narration [NARRATION],
							Remarks [REMARKS], RefNo [REF NO],RefDate[REF DATE],Reconcile_By [RECONCILE BY], Reconcile_Date [RECONCILE DATE],Authorize_By [AUTHORISIED BY],Clearing_By [CLEARING BY],Clearing_Date [CLEARING DATE],Branch_Name [BRANCH DESC],CmpUnit_Name [COMPANYUNIT DESC],AD.EnterBy [ENTERY USER],AD.EnterDate [ENTRY DATE] FROM AMS.AccountDetails  AS AD
							LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID=AD.Ledger_ID LEFT OUTER JOIN AMS.GeneralLedger AS GLC ON GLC.GLID=AD.CbLedger_ID LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId=AD.Subleder_ID LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId=AD.Agent_ID LEFT OUTER JOIN AMS.Department AS DP ON DP.DId =AD.Department_ID1 LEFT OUTER JOIN AMS.Department AS DP1 ON DP1.DId =AD.Department_ID2 LEFT OUTER JOIN AMS.Department AS DP2 ON DP2.DId =AD.Department_ID3
							LEFT OUTER JOIN AMS.Department AS DP3 ON DP3.DId =AD.Department_ID4 LEFT OUTER JOIN AMS.Currency AS C ON C.CId =AD.Currency_ID LEFT OUTER JOIN AMS.Branch AS B ON B.Branch_Id =AD.Branch_ID LEFT OUTER JOIN AMS.CompanyUnit AS CU ON CU.CmpUnit_Id =AD.CmpUnit_ID 
							
GO
/****** Object:  View [AMS].[ViewLedgerOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewLedgerOpening] AS
                            SELECT LO.Opening_Id [LEDGERID], LO.Module [SOURCE],LO.Voucher_No [VOUCHER NO],LO.OP_Date [VOUCHER DATE],LO.OP_Miti [MITI DATE],LO.Serial_No [REF NO],LO.Ledger_Id [LEDGER],GL.GLName [LEDGERNAME],LO.Subledger_Id [SUBLEDGER],SL.SLName [SUBLEDGERNAME],JA.AgentName [SALESMAN],
                            DP1.DName [DEPARTMENT1],DP2.DName [DEPARTMENT2], DP3.DName [DEPARTMENT3],DP4.DName [DEPARTMENT4], CUR.Ccode [CURRENCY RATE],LO.Debit [DEBIT AMOUNT], LO.LocalDebit [NPR DEBIT AMOUNT],LO.Credit [CREDIT AMOUNT],LO.LocalCredit [NPR CREDIT AMOUNT], LO.Narration [NARRATION],
                            LO.Remarks [REMARKS],LO.Enter_By [ENTER BY],LO.Enter_Date [ENTER DATE],LO.Reconcile_By [RECONCILE BY], LO.Reconcile_Date [RECONCILE DATE], Branch_Name [BRANCH DESC], CU.CmpUnit_Name [COMPANYUNIT DESC],FY.BS_FY [FISCAL YEAR] FROM AMS.LedgerOpening LO
	                        LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID = LO.Ledger_Id
	                        LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId = LO.Subledger_Id 
	                        LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId = LO.Agent_Id
	                        LEFT OUTER JOIN AMS.Department AS DP1 ON DP1.DId = LO.Cls1
	                        LEFT OUTER JOIN AMS.Department AS DP2 ON DP2.DId = LO.Cls2
	                        LEFT OUTER JOIN AMS.Department AS DP3 ON DP3.DId = LO.Cls3
	                        LEFT OUTER JOIN AMS.Department AS DP4 ON DP4.DId = LO.Cls4
	                        LEFT OUTER JOIN AMS.Currency AS CUR ON CUR.CId = LO.Currency_Id
	                        LEFT OUTER JOIN AMS.Branch AS BR ON BR.Branch_Id = LO.Branch_Id
	                        LEFT OUTER JOIN AMS.CompanyUnit AS CU ON CU.CmpUnit_Id = LO.Company_Id
	                        LEFT OUTER JOIN AMS.FiscalYear AS FY ON FY.FY_Id = LO.FiscalYearId;
GO
/****** Object:  View [AMS].[ViewProduct]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [AMS].[ViewProduct] as Select P.PName as Description,P.PShortName as SHortName,PU.UnitCode as Unit,P.PBuyRate As BuyRate,P.PSalesRate SalesRate, PG.GrpName GroupDecsription from AMS.Product  as P left outer join AMS.ProductUnit as PU on P.PUnit = PU.UID left outer join AMS.ProductGroup as PG on PG.PGrpID = p.PGrpId
				
GO
/****** Object:  View [AMS].[ViewProduct_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewProduct_Details] AS
                            SELECT PD.PID [LEDGERID], PD.PName [PRODUCT DESC],PD.PAlias [ALIAS],PD.PShortName [SHORTNAME],PD.PType [PRODUCT TYPE],PD.PCategory [PRODUCT CATEGORY],
                            PU.UnitName [UNITNAME],PD.PAltUnit [ALT UNIT], PD.PQtyConv [QTY], PD.PAltConv [ALT CONV],PD.PValTech [VAL TECH],
                            PD.PSerialno [SERIAL NO],PD.PSizewise [SIZEWISE],PD.PBatchwise [BITCHWISE], PD.PBuyRate [BUY RATE], PD.PSalesRate [SALES RATE], 
	                        PD.PMargin1 [MARGIN], PD.TradeRate [TRADE RATE], PD.PMargin2 [MARGIN 2],PD.PMRP [MRP],PG.GrpName [PRODUCT GROUP],
                            PSG.SubGrpName [PRODUCT SUBGROUP],PD.PTax [TAX AMOUNT], PD.PMin [MIN], PD.PMax [MAX], DP.DName [DEPARTMENT 1],  DP1.DName [DEPARTMENT 2],  DP2.DName [DEPARTMENT 3],  DP3.DName [DEPARTMENT 4], 
                            BH.Branch_Name [BRANCH DESC],  CM.CmpUnit_Name [COMPANY DESC],PD.PPL [PPL], PD.PPR [PPR],PD.PSL PSL, PD.PSR [PSR],PD.PL_Opening [OPENING], PD.PL_Closing [CLOSING], PD.BS_Closing [BS CLOSING], 
                            PD.PImage [PRODUCT IMAGE], PD.EnterBy [ENTER BY], PD.EnterDate [ENTER DATE], PD.Status [STATUS], PD.BeforeBuyRate [BEFORE BUY RATE], PD.BeforeSalesRate [BEFORE SALES RATE], PD.Barcode [BARCODE] FROM AMS.Product PD
	                        LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID = PD.PUnit
	                        LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpID = PD.PGrpId
                            LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId = PD.PSubGrpId
	                        LEFT OUTER JOIN AMS.Department AS DP ON DP.DId = PD.CmpId
                            LEFT OUTER JOIN AMS.Department AS DP1 ON DP1.DId = PD.CmpId1
	                        LEFT OUTER JOIN AMS.Department AS DP2 ON DP2.DId = PD.CmpId2
	                        LEFT OUTER JOIN AMS.Department AS DP3 ON DP3.DId = PD.CmpId3
	                        LEFT OUTER JOIN AMS.Branch AS BH ON BH.Branch_Id = PD.Branch_Id
	                        LEFT OUTER JOIN AMS.CompanyUnit AS CM ON CM.CmpUnit_Id = PD.CmpUnit_Id; 
GO
/****** Object:  View [AMS].[ViewProductOpening]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewProductOpening] AS
                           SELECT PO.Voucher_No [VOUCHER_NO],PO.Serial_No [REF_NO], PO.OP_Date [VOUCHER DATE], PO.OP_Miti [VOUCHER MITI], PO.Product_Id [PRODUCT NO],PR.PName [PRODUCTDESC], GD.GName [GOODNAME],GD.GCode [CODE],
                           DP1.DName [DEPARTMENT1],DP2.DName [DEPARTMENT2], DP3.DName [DEPARTMENT3],DP4.DName [DEPARTMENT4],
                           CUR.Ccode [CURRENCY], PO.Currency_Rate [CURRENCY RATE],PO.AltQty [ALT QTY],PO.AltUnit [ALT UNIT],PO.Qty [QTY],PO.QtyUnit [QTY UNIT],PO.Rate [RATE],
	                       PO.LocalRate [NPR RATE], PO.Amount [AMOUNT], PO.LocalAmount [NPR AMOUNT], PO.Enter_By [ENTER BY],PO.Enter_Date [ENTER DATE],PO.Reconcile_By [RECONCILE BY],PO.Reconcile_Date [RECONCILE DATE],BR.Branch_Name [BRANCHDESC], CU.CmpUnit_Name [COMPANYDESC],FY.BS_FY [FISCAL YEAR] FROM AMS.ProductOpening PO
	                       LEFT OUTER JOIN AMS.Product AS PR ON pr.PID = PO.Product_Id
	                       LEFT OUTER JOIN AMS.Godown AS GD ON GD.GID = PO.Godown_Id 
	                       LEFT OUTER JOIN AMS.Department AS DP1 ON DP1.DId = PO.Cls1
	                       LEFT OUTER JOIN AMS.Department AS DP2 ON DP2.DId = PO.Cls2
	                       LEFT OUTER JOIN AMS.Department AS DP3 ON DP3.DId = PO.Cls3
	                       LEFT OUTER JOIN AMS.Department AS DP4 ON DP4.DId = PO.Cls4
	                       LEFT OUTER JOIN AMS.Currency AS CUR ON CUR.CId = PO.Currency_Id
	                       LEFT OUTER JOIN AMS.Branch AS BR ON BR.Branch_Id = PO.CBranch_Id
	                       LEFT OUTER JOIN AMS.CompanyUnit AS CU ON CU.CmpUnit_Id = PO.CUnit_Id
	                       LEFT OUTER JOIN AMS.FiscalYear AS FY ON FY.FY_Id = PO.FiscalYearId; 
GO
/****** Object:  View [AMS].[ViewPurchase_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewPurchase_Details] AS
                         SELECT PB.PB_Invoice [INVOICE NO], PB.Invoice_SNo [SNO], PB.P_Id [PRODUCT CODE], PO.PShortName [PRODUCT SHORTNAME], PO.PName [PRODUCT],PSG.SubGrpName [PRODUCT SUBGROUP],GDN.GName [GODOWN DESC],GDN.GCode [GDN SHORTNAME],GDN.GLocation [GODOWN ADDRESS],
                         PB.Alt_Qty [ALT QTY], PAU.UnitCode [ALT UNIT], PB.Qty [QTY], PAU.UnitName [UNITDESC], PB.Rate [PURCHASE RATE], PB.B_Amount [BASIC AMOUNT], PB.T_Amount [TERM AMOUNT],
                         PB.N_Amount [NET AMOUNT], PB.AltStock_Qty [ACTUAL ALTQTY], PB.Stock_Qty [ACTUAL QTY], PB.Narration [NARRATION], PB.PO_Invoice [ORDER_INVOICE NO], PB.PO_Sno [ORDER_SNO], PB.PC_Invoice [PURCHASE CHALLA NO], PB.PC_SNo [CHALLA SNO], PB.Tax_Amount [TAXABLE AMOUNT], PB.V_Amount [VAT AMOUNT],
                           PB.V_Rate [VAT RATE], PFU.UID [FREE UNIT], PB.Free_Qty [FREE QTY], PB.StockFree_Qty [FREE STOCK QTY],
                           PB.T_Product [PRODUCT P], PB.P_Ledger [PRODUCT LEDGER], PB.PR_Ledger [PR LEDGER], PB.Serial_No [SERIAL NO], PB.Batch_No [BATCH NO], PB.Exp_Date [EXP DATE], PB.Manu_Date [MANU DATE], PB.TaxExempted_Amount [AMOUNT] FROM AMS.PB_Details PB
	                       LEFT OUTER JOIN AMS.Product AS PO ON PO.PID = PB.P_Id
	                       LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId = PO.PSubGrpId
	                       LEFT OUTER JOIN AMS.Godown AS GDN ON GDN.GID = PB.Gdn_Id
	                       LEFT OUTER JOIN AMS.ProductUnit AS PAU ON PB.Alt_UnitId = PAU.UID
	                       LEFT OUTER JOIN AMS.ProductUnit AS PFU ON PB.Free_Unit_Id = PFU.UID; 
GO
/****** Object:  View [AMS].[ViewPurchase_Header]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewPurchase_Header] AS
                           SELECT PBM.PB_Invoice [INVOICE NO], PBM.Invoice_Date [INVOICE DATE],PBM.Invoice_Miti [INVOICE MITI],PBM.Invoice_Time [INVOICE],PBM.PB_Vno [VOUCHER NO],
	                       PBM.Vno_Date [VOUCHER DATE], PBM.Vno_Miti [VOUCHER MITI], PBM.Vendor_ID [LEDGERID], GL.GLName [LEDGERDESC],PBM.Party_Name [PARTYNAME], 
	                       PBM.Vat_No [VAT NO],PBM.Contact_Person [CONTACT PARTY], PBM.Mobile_No [PARTY MOBILE], PBM.Address [PARTY ADDRESS], PBM.ChqNo [CHEQUE NO],
	                       PBM.ChqDate [CHEQUE DATE], PBM.Invoice_Type [INVOICE TYPE], PBM.Invoice_In [INVOICE IN], PBM.DueDays [BILL DUE DAYS], PBM.DueDate [BILL DUE DATE], 
	                       JA.AgentName [SALESMAN],PBM.Subledger_Id [SUBLEDGERID], SL.SLName [SUBLEDGER DESC],PBM.PO_Invoice [PURCHASE ORDER], PBM.PO_Date [PURCHASE ORDERDATE], 
	                       PBM.PC_Invoice [PURCHASE CHALLAN NO], PBM.PC_Date [PURCHASE CHALLAN DATE], DP1.DName [DEPARTMENT1],DP2.DName [DEPARTMENT2], DP3.DName [DEPARTMENT3],DP4.DName [DEPARTMENT4], 
	                       CUR.Ccode [CURRENCY], CUR.CRate [CURRENCY RATE], CON.CName [COUNTER NAME], PBM.B_Amount [BASIC AMOUNT], PBM.T_Amount [TERM AMOUNT], PBM.N_Amount [NET AMOUNT], 
	                       PBM.LN_Amount [LOCAL NET AMOUNT], PBM.Tender_Amount [TENDER AMOUNT], PBM.Change_Amount [CHANGE AMOUNT], PBM.V_Amount [VAT AMOUNT], PBM.Tbl_Amount [TAXABLE AMOUNT],
                           PBM.Action_type [ACTION TYPE], PBM.R_Invoice [RETURN INVOICE] , PBM.No_Print [NO OF PRINT], PBM.In_Words [NUMINWORDS], PBM.Remarks [REMARKS], 
	                       PBM.Audit_Lock  [VOUCHER LOCK], PBM.Enter_By [ENTER BY], PBM.Enter_Date [ENTER DATE], PBM.Reconcile_By [RECONCILE BY],
                           PBM.Reconcile_Date [RECONCILE], PBM.Auth_By [AUTHORIZED BY], PBM.Auth_Date [AUTHORIZED DATE], PBM.Cleared_By [CLEARED BY], PBM.Cleared_Date [CLEARED DATE], 
	                       PBM.CancelBy [CANCEL BY], PBM.CancelDate [CANCEL DATE], PBM.CancelRemarks [CANCEL REMARKS], CU.CmpUnit_Name [COMPANY DESC], BR.Branch_Name [BRANCH DESC], FY.BS_FY [FISCAL YEAR] FROM AMS.PB_Master PBM
	                       LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID = PBM.Vendor_ID
	                       LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId = PBM.Agent_Id
	                       LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId = PBM.Subledger_Id 
	                       LEFT OUTER JOIN AMS.Department AS DP1 ON DP1.DId = PBM.Cls1
	                       LEFT OUTER JOIN AMS.Department AS DP2 ON DP2.DId = PBM.Cls2
	                       LEFT OUTER JOIN AMS.Department AS DP3 ON DP3.DId = PBM.Cls3
	                       LEFT OUTER JOIN AMS.Department AS DP4 ON DP4.DId = PBM.Cls4
	                       LEFT OUTER JOIN AMS.Currency AS CUR ON CUR.CId = PBM.Cur_Id
	                       LEFT OUTER JOIN AMS.Counter AS CON ON CON.CId = PBM.Counter_ID
	                       LEFT OUTER JOIN AMS.Branch AS BR ON BR.Branch_Id = PBM.CBranch_Id
	                       LEFT OUTER JOIN AMS.CompanyUnit AS CU ON CU.CmpUnit_Id = PBM.CUnit_Id
	                       LEFT OUTER JOIN AMS.FiscalYear AS FY ON FY.FY_Id = PBM.FiscalYearId;
GO
/****** Object:  View [AMS].[ViewPurchaseBillWise_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE VIEW [AMS].[ViewPurchaseBillWise_Term] AS
                           SELECT PT.PB_VNo[INVOICE NO],PT_Name[TERM DESE],TM.PT_Sign[SIGN],TM.Order_No[TERM SHORTNAME],PT.SNo[SNO], PT.Rate[TERM RATE],PT.Amount[TERM AMOUNT] FROM AMS.PB_Term AS PT
                           LEFT OUTER JOIN AMS.PT_Term AS TM ON PT.PT_Id = TM.PT_ID WHERE PT.Term_Type = 'B' 
GO
/****** Object:  View [AMS].[ViewPurchaseReturn_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewPurchaseReturn_Details] AS
                            SELECT PRD.PR_Invoice [INVOICE NO], PRD.Invoice_SNo [SNO], PRD.P_Id [PRODUCT CODE], PD.PShortName [PRODUCT SHORTNAME],PD.PName [PRODUCT],
                            PG.GrpName [PRODUCT GROUP],PSG.SubGrpName [PRODUCT SUBGROUP],GD.GName [GODOWN DESC], GD.GCode [GDN SHORTNAME],GD.GLocation [GDN ADDRESS],
                            PRD.Qty [QTY],PU.UnitCode [UNIT DESC],PRD.Alt_Qty [ALT QTY],PAU.UnitCode [ALT UNIT], PRD.AltStock_Qty [ACTUAL ALTQTY], PRD.Stock_Qty [ACTUAL QTY],
                            PRD.Free_Qty [FREE QTY],PFU.UID [FREE UNIT], PRD.StockFree_Qty,PD.PMRP [MRP],PD.PSalesRate [COST RATE],PD.TradeRate [TRADE RATE],PRD.Rate [INVOICE RATE], PRD.B_Amount [BASIC AMOUNT], 
                            PRD.T_Amount [TERM AMOUNT], PRD.N_Amount [NET AMOUNT], PRD.Narration [NARRATION], PRD.Tax_Amount [TAX AMOUNT], PRD.V_Amount [VAT AMOUNT], PRD.V_Rate [VAT RATE], 
                            PAU.UnitCode [UNIT DECE], PRD.ExtraFree_Qty [EXTRAFREE QTY], PRD.ExtraStockFree_Qty [STOCK FREE], PRD.T_Product [T PRODUCT], PRD.P_Ledger [PURCHASE LEDGER], PRD.PR_Ledger [PR LEDGER],
                            PRD.SZ1 [SIZE 1], PRD.SZ2 [SIZE 2], PRD.SZ3 [SIZE 3], PRD.SZ4, PRD.SZ5, PRD.SZ6, PRD.SZ7, PRD.SZ8, PRD.SZ9, PRD.SZ10, 
                            PRD.Serial_No [SERIAL NO],PRD.Batch_No [BATCH NO], PRD.Exp_Date [EXP DATE], PRD.Manu_Date [MANU DATE] FROM AMS.PR_Details PRD
	                           LEFT OUTER JOIN AMS.Product AS PD ON PD.PID = PRD.P_Id
	                           LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpID = PD.PID
	                           LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId = PD.PSubGrpId
	                           LEFT OUTER JOIN AMS.Godown AS GD ON GD.GID = PRD.Gdn_Id
	                           LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID =PRD.Unit_Id
	                           LEFT OUTER JOIN AMS.ProductUnit AS PAU ON PAU.UID =PRD.Alt_UnitId
                               LEFT OUTER JOIN AMS.ProductUnit AS PFU ON PFU.UID =PRD.Free_Unit_Id
	                           LEFT OUTER JOIN AMS.ProductUnit AS PEF ON PEF.UID = PRD.ExtraFree_Unit_Id
GO
/****** Object:  View [AMS].[ViewPurchaseReturn_Header]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewPurchaseReturn_Header] AS
                            SELECT SR.PR_Invoice [INVOICE NO],  SR.Invoice_Date [INVOICE DATE],  SR.Invoice_Miti [INVOICE MITI],  SR.Invoice_Time [INVOICE TIME], SR.PB_Invoice [REF INVOICE], SR.PB_Date [REF DATE], SR.PB_Miti [REF MITI],
                                   GL.GLName [VENDOR DESC], SR.Party_Name [PARTY DESC],  SR.Vat_No [VAT NO], SR.Contact_Person [PARTY CONTACT PERSON],  SR.Mobile_No [PARTY MOBILE], SR.Address [PARTY ADDRESS], SR.ChqNo [CHEQUE NO],
	                               SR.ChqDate [CHEQUE], SR.Invoice_Type [INVOICE TYPE], SR.Invoice_In [INVOICE IN], SR.DueDays [BILL DUE DAYS], SR.DueDate [BILL DUE DATE], JA.AgentName [SALESMAN],
	                               SL.SLName [SUBVENDOR DESC], DP.DName [DEPARTMENT 1],  DP1.DName [DEPARTMENT 2],  DP2.DName [DEPARTMENT 3],  DP3.DName [DEPARTMENT 4],
                                   CU.CName [CURRENCY],CU.CRate [CURRENCY RATE],SR.B_Amount [BAISC AMOUNT], SR.T_Amount [TERM AMOUNT], SR.N_Amount [NET AMOUNT], SR.LN_Amount [LOCAL NET AMOUNT], 
	                               SR.Tender_Amount [TENDER AMOUNT], SR.Change_Amount [CHANGE AMOUNT], SR.V_Amount [VAT AMOUNT],
                                   SR.Tbl_Amount [TAXABLE AMOUNT], SR.Action_type [ACTION TYPE], SR.No_Print [NO OF PRINT], SR.In_Words [IN WORDS], SR.Remarks [REMARK], SR.Audit_Lock [VOUCHER LOCK], 
	                               SR.Enter_By [ENTER BY], SR.Enter_Date [ENTER DATE], SR.Reconcile_By [RECONCILE BY], SR.Reconcile_Date [RECONCILE DATE], SR.Auth_By [AUTHORIZED BY], SR.Auth_Date [AUTHORIZED DATE], 
	                               SR.Cleared_By [CLEARED BY],  SR.Cleared_Date [CLEARED DATE], SR.CancelBy [CANCEL BY], SR.CancelDate [CANCEL DATE], SR.CancelRemarks [CANCEL REMARKS],
                                   BH.Branch_Name [BRANCH DESC], CM.CmpUnit_Name [COMPANY DESC],FY.Current_FY [FISCAL YEAR]	 FROM AMS.PR_Master SR
	                               LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID = SR.Vendor_ID 
	                               LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId = SR.Agent_Id
	                               LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId = SR.Subledger_Id
	                               LEFT OUTER JOIN AMS.Currency AS CU ON CU.CId = SR.Cur_Id
	                               LEFT OUTER JOIN AMS.Department AS DP ON DP.DId = SR.Cls1
	                               LEFT OUTER JOIN AMS.Department AS DP1 ON DP1.DId = SR.Cls2
	                               LEFT OUTER JOIN AMS.Department AS DP2 ON DP2.DId = SR.Cls3
	                               LEFT OUTER JOIN AMS.Department AS DP3 ON DP3.DId = SR.Cls4
	                               LEFT OUTER JOIN AMS.Branch AS BH ON BH.Branch_Id = SR.CBranch_Id
	                               LEFT OUTER JOIN AMS.CompanyUnit AS CM ON CM.CmpUnit_Id = SR.CUnit_Id 
	                               LEFT OUTER JOIN AMS.FiscalYear AS FY ON FY.FY_Id = SR.FiscalYearId
GO
/****** Object:  View [AMS].[ViewSales_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSales_Details] as 
				Select SBD.SB_Invoice as [INVOICE NO], SBD.Invoice_SNo as [SNO], SBD.P_Id as [PRODUCT CODE],PD.PShortName [PRODUCT SHORTNAME], PD.PName as [PRODUCT] , PrGrp.GrpName as [PRODUCT GROUP],
				PrSGrp.SubGrpName as [PRODUCT SUBGROUP], GDN.GName as [GODOWN DESC], GDN.GCode as [GDN SHORTNAME], GDN.GLocation as [Godown Address], SBD.Alt_Qty as [ALT QTY], PAU.UnitCode as [ALT UNIT],
				SBD.Qty as [QTY],PU.UnitName [UNIT DESC],SBD.AltStock_Qty as [ACTUAL ALTQTY],SBD.Stock_Qty as [ACTUAL QTY], SBD.Free_Qty as [FREE QTY],PFU.UID as [FREE UNIT],PD.PMRP as [MRP], 
				PD.PBuyRate as [COST RATE], PD.PSalesRate as [PSALES RATE],SBD.Rate as [SALES RATE], PD.TradeRate as [TRADE PRICE], SBD.Rate as [INVOICE RATE], SBD.B_Amount as [BASIC AMOUNT], SBD.T_Amount  as [TERM AMOUNT], SBD.N_Amount as [NET AMOUNT], SBD.B_Amount* SBM.Cur_Rate as [LOCAL BASIC], SBD.T_Amount * SBM.Cur_Rate  as [LOCAL TERM], SBD.N_Amount*SBM.Cur_Rate as [LOCAL NETAMOUNT], SBD.Narration as [NARRATION], SBD.SO_Invoice as [SALES ORDERNO], SBD.SO_Sno as [ORDER SNO],SBD.SC_Invoice as [SALES CHALLANNO], SBD.SC_SNo as [CHALLAN SNO], SBD.altStock_Qty as [AltStock Qty],SBD.Tax_Amount [TAXABLE AMOUNT],SBD.V_Amount [VAT AMOUNT],SBD.V_Rate [VAT RATE] from AMS.SB_Details as SBD Left outer Join AMS.SB_Master as SBM On SBM.SB_Invoice = SBD.SB_Invoice Left outer Join AMS.Product as PD On SBD.P_Id= PD.PID Left outer Join AMS.ProductUnit as PU On SBD.Unit_Id= PU.UID Left outer Join AMS.ProductUnit as PAU On SBD.Alt_UnitId= PAU.UID Left outer Join AMS.ProductUnit as PFU On SBD.Free_Unit_Id= PFU.UID Left Outer Join AMS.ProductGroup as PrGrp on PD.PGrpId=PrGrp.PGrpID Left Outer Join AMS.ProductSubGroup as PrsGrp on PrsGrp.PSubGrpId=PD.PSubGrpId Left outer Join AMS.Godown as GDN On SBD.Gdn_Id = GDN.GID Left Outer Join AMS.SC_Master SCM on  SCM.SC_Invoice=SBD.SC_SNo; 
GO
/****** Object:  View [AMS].[ViewSales_Header]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSales_Header] as 
				Select SB_Invoice [INVOICE NUMBER],SB.Invoice_Date [INVOICE DATE],SB.Invoice_Miti [INVOICE MITI],SB.Invoice_Time [INVOICE TIME],PB_Vno [REF VOUCHERNO],SB.Vno_Date [REF DATE],
				SB.Vno_Miti [REF MITI],GL.GLName [INVOICE CUSTOMER],GL.PANNO [CUSTOMER PAN],Gl.GLAddress[CUSTOMER ADDRESS],GL.PhoneNo [CUSTOMER PHONENO],Gl.LandLineNo [OFFICE NUMBER],
				GL.CrLimit [CUSTOMER CRLIMIT],GL.CrDays [CUSTOMER CRDAYS],Gl.IntRate [INTERST RATE],GL.OwnerName [OWNER'S NAME],GL.Email[CUSTOMER EMAIL],SB.Party_Name [PARTY DESC], SB.Address PARTYADDRESS,SB.Mobile_No PARTYMOBILENO,SB.Vat_No [Party Vat],SB.Contact_Person [PARTY CONTACTPERSON],
				SB.Invoice_Type [BILL CATEGORY],SB.Invoice_Mode [INVOICE MODE],SB.DueDays [BILL DUE DAYS],d1.DName HDEPARTMENT,PM.PaitentDesc PATIENTDESC,D.DrName DoctorDesc,TM.TableCode TABLEID,TM.TableName TABLENAME,
				SB.DueDate [BILL DUE DATE],JA.AgentId [SALES MANID],JA.AgentName [SALES MANI], Sl.SLName AS [SUBLEDGER], SB.SO_Invoice [SALES ORDERNO], SB.SO_Date [SALES ORDERDATE],SB.SC_Invoice [SALES CHALLANNO],
				SB.SC_Date [SALES CHALLANDATE],Dep1.DName [CLS1 DESC], Dep2.DName [CLS2 DESC],Dep3.DName [CLS3 DESC],  Dep4.DName [CLS4 DESC],COU.CName,COU.CCode,Cur.Ccode [CURRENCY CODE], 
				SB.Cur_Rate [CURRENCY RATE],SB.B_Amount [BASIC AMT],SB.T_Amount [TERM AMT],SB.N_Amount [NET AMT],SB.LN_Amount [LOCAL NETAMT], SB.V_Amount [VAT AMOUNT],SB.Tbl_Amount [TAXABLE AMT],
				SB.Tender_Amount [TENDER AMOUNT],SB.Return_Amount[RETURN AMOUNT],SB.Action_Type [ACTION TYPE],SB.In_Words [NUMINWORDS],SB.Is_Printed [PRINTED],SB.No_Print [NO OF PRINT],SB.Printed_By [PRINT BY], 
				SB.Printed_Date [PRINT DATE],SB.Audit_Lock [VOUCHER LOCK],SB.Enter_By [ENTER BY],SB.Enter_Date [ENTER DATE],SB.Reconcile_By [RECONCILE BY],SB.Reconcile_Date [RECONCILE DATE],
				SB.Auth_By [AUTHORIZED BY],SB.Auth_Date [AUTHORIZED DATE],SB.Cleared_By [CLEARED BY],SB.Cleared_Date [CLEAR DATE],SB.Cancel_By [CANCEL BY], SB.Cancel_Date [CANCEL DATE],
				SB.Cancel_Remarks [CANCEL REMARKS],CU.CmpUnit_Name[COMPANY UNITDESC],B.Branch_Name [BRANCH DESC],IsAPIPosted,IsRealtime From AMS.SB_Master as SB left outer JOIN  AMS.Branch B ON SB.CBranch_Id = B.Branch_Id  left outer JOIN  AMS.CompanyUnit CU ON SB.CUnit_Id = CU.CmpUnit_Id  Left outer Join AMS.GeneralLedger as Gl On SB.Customer_ID = Gl.GLID  Left outer Join AMS.Currency as Cur On SB.Cur_Id = Cur.CId  Left outer Join AMS.JuniorAgent JA On SB.Agent_Id = JA.AgentId Left Outer Join AMS.SO_Master as SO on SB.SO_Invoice=SO.SO_Invoice Left Outer Join AMS.SC_Master as SC on SB.SC_Invoice=SC.SC_Invoice Left Outer join AMS.Counter as COU on COU.CId=SB.CounterId Left Outer join AMS.Department as Dep1 on Dep1.DId=SB.Cls1 Left Outer join AMS.Department as Dep2 on Dep2.DId=SB.Cls2  Left Outer join AMS.Department as Dep3 on Dep3.DId=SB.Cls3 Left Outer join AMS.Department as Dep4 on Dep4.DId=SB.Cls4 Left Outer join AMS.Subledger as Sl on Sl.SLId=SB.Subledger_Id Left Outer Join AMS.Area As Area On GL.AreaId = Area.AreaId LEFT OUTER JOIN HOS.Doctor d ON SB.DoctorId=D.DrId LEFT OUTER JOIN HOS.PatientMaster pm ON SB.PatientId=PM.PaitentId LEFT OUTER JOIN HOS.Department d1 ON SB.HDepartmentId=d1.DId left outer join AMS.TableMaster as TM on SB.TableId=TM.TableId; 
GO
/****** Object:  View [AMS].[ViewSalesBillwise_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

						Create View [AMS].[ViewSalesBillwise_Term] as Select SB_VNo as [Invoice No], ST_Name as [Term Description], ST_Sign as [Sign], Order_No as [Term ShortName], Sno as [Sno], ST_Rate as [Term Rate], ST.Amount As [Term Amount]  from AMS.SB_Term as ST Left outer Join AMS.ST_Term as TM On ST.ST_Id = TM.ST_ID where Term_Type = 'B'
							
GO
/****** Object:  View [AMS].[ViewSalesBillWiseHorizontal_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesBillWiseHorizontal_Term] as Select * from ( SELECT STD.SB_VNo,STD.SNo,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=STD.SB_VNo WHERE Term_Type in('B') ) as d Pivot(max(Amount) for ST_Name in ([Discount]) ) as pid 
GO
/****** Object:  View [AMS].[ViewSalesBillWiseHorizontalWithRate_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesBillWiseHorizontalWithRate_Term] as Select AMT.*,[Discount Rate] from ( SELECT STD.SB_VNo,STD.SNo,STD.Product_Id,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=STD.SB_VNo WHERE Term_Type in('B') ) as d  Pivot(max(Amount) for ST_Name in ([Discount]) ) as Amt  Left Outer Join(select * from(SELECT STD.SB_VNo, STD.SNo, Rate Term_Rate, SBT.ST_Name + ' Rate'[ST_Name] FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo  WHERE Term_Type in ('B')) as d Pivot(max(Term_Rate) for ST_Name in ([Discount Rate]) ) as pid ) as Rate  On Amt.SB_VNo = Rate.SB_VNo and Amt.SNo = Rate.SNo 
GO
/****** Object:  View [AMS].[ViewSalesChallan_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE VIEW [AMS].[ViewSalesChallan_Details] AS 
                SELECT SC.SC_Invoice [CHALLAN_NO], SC.Invoice_SNo [CHALLAN_SNO], PD.PName [PRODUCT], PD.PShortName [PRODUCT_SHORTNAME],pd.PBuyRate[PBUY_RATE],PD.PSalesRate [PSALES_RATE],GD.GName [GODOWN_NAME], GD.GCode [GODOWN_CODE],GD.GLocation [GODOWN_ADDRESS],SC.Alt_Qty [ALTQTY], SC.Alt_UnitId [ALT_UOMID],PAU.UnitCode [ALT_UOM],SC.Qty [QTY], PD.PUnit [UOMID],PU.UnitCode [UOM],SC.Rate [RATE],SC.B_Amount [BASIC_AMT], SC.T_Amount [TERM_AMT], SC.N_Amount [NET_AMT],SC.AltStock_Qty [ALTSTOCK_QYT], SC.Stock_Qty [STOCK_QTY],  SC.Narration [NARRATION],SC.QOT_Invoice [QUOTATION_INVOICE],SC.QOT_Sno [QUOTATION_SNO],SC.SO_Invoice [SO_INVOIVES],  SC.SO_SNo [SO_SNO],SC.Tax_Amount [TAXABLE_AMT],  SC.V_Amount [VAT_AMT],  SC.V_Rate [VAT_RATE],  SC.Issue_Qty [ISSUE_QTY],SC.Free_Unit_Id [FREEUNITID], SC.Free_Qty [FREE_QTY],  SC.StockFree_Qty [STOCKFREE_QTY],  SC.ExtraFree_Unit_Id [EXTRAFREEEUNITID],  SC.ExtraFree_Qty [EXTRAFREES_QTY],SC.ExtraStockFree_Qty [EXTRASTOCKFREE_QTY], SC.T_Product [TENDER_PRODUCT], SC.S_Ledger [S_LEDGER],SC.SR_Ledger [SR_LEDGER],SC.Serial_No [SERIAL_NO], SC.Batch_No [BATCH_NO],SC.Exp_Date [EXPIRY_DATE],SC.Manu_Date [MANU_DATE],SC.AltIssue_Qty [ALTISSUE]	 FROM AMS.SC_Details SC
                LEFT OUTER JOIN AMS.Product AS PD ON PD.PID = SC.P_Id LEFT OUTER JOIN AMS.Godown AS GD ON GD.GID = SC.Gdn_Id 
                LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID = SC.Unit_Id LEFT OUTER JOIN AMS.ProductUnit AS PAU ON PAU.UID = SC.Alt_UnitId; 
GO
/****** Object:  View [AMS].[ViewSalesChallan_Header]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
                CREATE VIEW [AMS].[ViewSalesChallan_Header] AS 
                SELECT SC.SC_Invoice [CHALLAN_NO],SC.Invoice_Date [CHALLAN_DATE], SC.Invoice_Miti [CHALLAN_MITI], SC.Invoice_Time [CHALLAN_TIME],SC.Ref_Vno [REF_VNO], SC.Ref_Date [REF_DATE],  SC.Ref_Miti [REF_MITI],
                GL.GLName [CUSTOMER], GL.PanNo [PANNO],gl.GLAddress [LEDGER_ADDRESS],GL.PhoneNo [LEDGER_PHONENO],GL.CrDays [LEDGER_CREDITDAYS],GL.CRLimit [LEDGER_CREDITLIMIT],GL.Email [LEDGER_EMAIL],SC.Party_Name [PARTY_NAME], SC.Vat_No [VAT_NO],SC.Contact_Person [PARTY_CONTACT_PERSON], SC.Mobile_No [PARTY_MOBILE],  SC.Address [PARTY_ADDRESS],  SC.ChqNo [CHEQUE_NO],  SC.ChqDate [CHEQUE_DATE], SC.Invoice_Type [INVOICE_TYPE], SC.Invoice_Mode [INVOICE_MODE],  SC.Payment_Mode [PAYMENT], SC.DueDays [DUE_DAYS], SC.DueDate [DUE_DATE], JA.AgentName [SALESMAN], SL.SLName [SUBLEDGER_DESC], SC.QOT_Invoice [QUOTATION_NO],  SC.QOT_Date [QUOTATION_DATE],  SC.SO_Invoice [SO_INVOICE],   SC.SO_Date [SO_DATE],  Dep1.DName [DEPARTMENT 1], Dep2.DName [DEPARTMENT 2],Dep3.DName [DEPARTMENT 3],  Dep4.DName [DEPARTMENT 4], SC.CounterId,  SC.Cur_Id,  SC.Cur_Rate,  SC.B_Amount [BASIC_AMT],  SC.T_Amount [TERM_AMT], SC.N_Amount [NET_AMT],SC.LN_Amount [LOCALNET_AMT], SC.V_Amount [VAT_AMT], SC.Tbl_Amount [TAXABLE_AMT], SC.Tender_Amount [TENDER_AMT],SC.Return_Amount [RETURN_AMT],SC.Action_Type [ACTIONTAG],SC.R_Invoice [RETURN_INVOICE],SC.No_Print [NO_PRINT], SC.In_Words [IN_WORDS],SC.Remarks [REMARKS], SC.Audit_Lock [AUDIT_LOCK],SC.Enter_By [ENTER_BY],SC.Enter_Date [ENTER_DATE], SC.Reconcile_By [RECONCILE_BY], SC.Reconcile_Date [RECONILE_DATE], SC.Auth_By [AUTH_BY],SC.Auth_Date [AUTH_DATE],cmp.CmpUnit_Name [COMPANY_DESC],B.Branch_Name [BRANCH_DESC],SC.FiscalYearId [FISCALYEAR]
                FROM AMS.SC_Master SC
                LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID = SC.Customer_Id LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId = SC.Agent_Id LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId = SC.Subledger_Id LEFT OUTER JOIN AMS.Department AS Dep1 ON Dep1.DId=SC.Cls1  LEFT OUTER JOIN AMS.Department AS Dep2 ON Dep2.DId=SC.Cls2  LEFT OUTER JOIN AMS.Department AS Dep3 ON Dep3.DId=SC.Cls3 LEFT OUTER JOIN AMS.Department AS Dep4 ON Dep4.DId=SC.Cls4 LEFT OUTER JOIN AMS.Counter AS CO ON CO.CId = SC.CounterId LEFT OUTER JOIN AMS.CompanyUnit AS cmp ON cmp.CmpUnit_Id = SC.CUnit_Id LEFT OUTER JOIN AMS.Branch AS B ON B.Branch_Id = SC.CBranch_Id LEFT OUTER JOIN AMS.FiscalYear AS FY ON FY.FY_Id = SC.FiscalYearId; 
GO
/****** Object:  View [AMS].[ViewSalesChallan_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
               CREATE VIEW [AMS].[ViewSalesChallan_OtherDetails] as  SELECT * FROM AMS.SC_Master_OtherDetails  
GO
/****** Object:  View [AMS].[ViewSalesInvoice_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
               CREATE VIEW [AMS].[ViewSalesInvoice_OtherDetails] as  SELECT * FROM AMS.SB_Master_OtherDetails  
GO
/****** Object:  View [AMS].[ViewSalesOrder_Detail]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE VIEW [AMS].[ViewSalesOrder_Detail] AS 
                                          SELECT SO.SO_Invoice [INVOICE_NO],SO.Invoice_SNo [SNO],SO.P_Id [PRODUCT_CODE], PD.PName [PRODUCT_DESC],PD.PShortName [PRODUCT_SHORT],
	                    PG.GrpName [PRODUCT_GROUP],PSG.SubGrpName [PRODUCT_SUBGROUP],SO.Gdn_Id [GODOWN_CODE],GD.GName [GODOWN_DESC],GD.GLocation [LOCATION],
	                    SO.Alt_Qty [ALT_QTY], PAU.UID [ALT_UNIT], SO.Qty [QTY], PU.UnitName [UNIT_DESC], SO.Rate [RATE], SO.B_Amount [BASIC_AMT],
                        SO.T_Amount [TERM_AMT], SO.N_Amount [NET_AMT],SO.AltStock_Qty [ALTSTOCK_QTY], SO.Stock_Qty [STOCK_QTY], SO.Narration [NARRATION],
	                    SO.IND_Invoice [IND_INVOICENO], SO.IND_Sno [IND_SNO], SO.QOT_Invoice [QUOTATION_INVOICE], SO.QOT_SNo [QUOTATION_SNO],
                       SO.Tax_Amount [TAXABLE_AMT], SO.V_Amount [VAT_AMT],  SO.V_Rate [VAT_RATE], SO.Issue_Qty [ISSUE_QTY],  PAU.UID [FREE_UNIT],  
	                   SO.Free_Qty [FREE_QTY],SO.StockFree_Qty [STOCKFREE_QTY], SO.ExtraFree_Unit_Id,
                       SO.ExtraFree_Qty [EXTRAFREE_QTY],SO.ExtraStockFree_Qty, SO.T_Product [TERM_PRODUCT], SO.S_Ledger [SELL_LEDGER], SO.SR_Ledger [SR_LEDGER], 
                       SO.Serial_No [SERIAL_NO], SO.Batch_No [BATCH_NO], SO.Exp_Date [EXPIRY_DATE], SO.Manu_Date, SO.Notes, SO.PrintedItem,SO.PrintKOT,
                       SO.OrderTime [ORDERTIME], SO.Print_Time [PRINT_TIME],SO.Is_Canceled [ISCANCELED],SO.CancelNotes [CANCELNOTE], SO.PDiscountRate [PURCHASE_DISCOUNT_RATE], SO.PDiscount [PURCHASE_DISCOUNT],
	                   SO.BDiscountRate [BILL_DISCOUNTRATE], SO.BDiscount [BILL_DISCOUNT],SO.ServiceChargeRate [SERVICECHARGERATE],
                       SO.ServiceCharge [SERVICE_CHARGE] FROM AMS.SO_Details SO
	                   LEFT OUTER JOIN AMS.Product AS PD ON PD.PID = SO.P_Id
	                   LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpID = PD.PGrpId
	                   LEFT OUTER JOIN AMS.ProductSubGroup AS PSG ON PSG.PSubGrpId = PD.PSubGrpId
	                   LEFT OUTER JOIN AMS.Godown AS GD ON GD.GID = SO.Gdn_Id
	                   LEFT OUTER JOIN AMS.ProductUnit AS PAU ON PAU.UID = SO.Alt_UnitId
	                   LEFT OUTER JOIN AMS.ProductUnit AS PU ON PU.UID = SO.Unit_Id
	                   LEFT OUTER JOIN AMS.ProductUnit AS PFU ON PFU.UID = SO.Free_Unit_Id
GO
/****** Object:  View [AMS].[ViewSalesOrder_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [AMS].[ViewSalesOrder_Details] AS 
                        SELECT SO.SO_Invoice [INVOICE NO],SO.Invoice_SNo [SNO],SO.P_Id [PRODUCT CODE],PD.PName [PRODUCT],PD.PShortName [PRODUCT SHORTNAME],
                        PD.PName [PRODUCT GROUP],PS.SubGrpName [PRODUCT SUBGROUP],GD.GName [GODOWN DESC],GD.GLocation [GODOWN ADDRESS],
                        SO.Alt_Qty [ALT QTY],PAU.UnitCode [ALT UNIT],SO.Qty [QTY] ,PAU.UnitName [UNIT DESC],SO.AltStock_Qty [ACTUAL ALTQTY], SO.Stock_Qty [ACTUAL QTY],
                        SO.Issue_Qty [ISSUE QTY],PFU.UnitCode [FREE UNIT],SO.Free_Qty [FREE QTY],SO.StockFree_Qty [FREE STOCK],PEU.UID [EXRA FREE UNIT],SO.ExtraFree_Qty [EXTRAFREE QTY],SO.ExtraStockFree_Qty [EXTRASTOCKFREE],
                        PD.PMRP [MRP],SO.Rate [INVOICE RATE],SO.B_Amount [BASIC AMOUNT],SO.T_Amount [TERM AMOUNT], SO.N_Amount [NET AMOUNT],SO.Tax_Amount [TAXABLE AMOUNT],SO.V_Amount [VAT AMOUNT],SO.V_Rate [VAT RATE], 
                        SO.Narration [NARRATION], SO.IND_Invoice [IND INVOICE],SO.IND_Sno [IND SNO],SO.QOT_Invoice [QOT INVOICE],SO.QOT_SNo [QOT],SO.T_Product, SO.S_Ledger, SO.SR_Ledger, SO.SZ1, SO.SZ2,SO.SZ3,SO.SZ4,SO.SZ5,SO.SZ6,SO.SZ7,SO.SZ8, SO.SZ9,SO.SZ10, 
                        SO.Serial_No [SERIAL NO], SO.Batch_No [BATCH NO],SO.Exp_Date [EXP DATE], SO.Manu_Date [MANU DATE], SO.Notes [NOTES], SO.PrintedItem [PRINTEDITEMS], SO.PrintKOT [PRINTKOT],
                        SO.OrderTime [ORDER TIME], SO.Print_Time [PRINT TIME], SO.Is_Canceled [CANCELED],SO.CancelNotes [CANCELNOTES], SO.PDiscountRate [PRODUCTWISE DIS. RATE],SO.PDiscount [PRODUCTWISE DISCOUNT], SO.BDiscountRate [BILLWISE DISCOUNT RATE], SO.BDiscount [BILLWISE DISCOUNT], SO.ServiceChargeRate [SERVICE CHARGE RATE],SO.ServiceCharge [SERVICE CHARGE] FROM AMS.SO_Details SO
		                LEFT OUTER JOIN AMS.Product AS PD ON PD.PID = SO.P_Id
		                LEFT OUTER JOIN AMS.ProductGroup AS PG ON PG.PGrpID = PD.PGrpId
                        LEFT OUTER JOIN AMS.ProductSubGroup AS PS ON PS.PSubGrpId = PD.PSubGrpId
		                LEFT OUTER JOIN AMS.Godown AS GD ON GD.GID = SO.Gdn_Id
		                LEFT OUTER JOIN AMS.ProductUnit AS PAU ON PAU.UID = SO.Unit_Id
		                LEFT OUTER JOIN AMS.ProductUnit AS PFU ON PFU.UID = SO.Free_Unit_Id
		                LEFT OUTER JOIN AMS.ProductUnit AS PEU ON PEU.UID = SO.ExtraFree_Unit_Id 
GO
/****** Object:  View [AMS].[ViewSalesOrder_Header]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE VIEW [AMS].[ViewSalesOrder_Header] AS
                            SELECT SO.SO_Invoice [INVOICE_NO], SO.Invoice_Date [INVOICE_DATE], SO.Invoice_Miti [INVOICE_MITI], SO.Invoice_Time [INVOICE_TIME], 
	                               SO.Ref_Vno [REF_VNO], SO.Ref_Date [REF_DATE], SO.Ref_Miti [REF_MITI], GL.GLName [CUSTOMER_NAME],GL.PanNo [CUSTOMER_PANNO],
                                   SO.Party_Name [PARTY_NAME], SO.Vat_No [VAT_NO], SO.Contact_Person [PARTY_CONTACTNO], SO.Mobile_No [PARTY_MOBILENO], SO.Address [PARTY_ADDRESS], 
	                               SO.ChqNo [CHEQUE_NO], SO.ChqDate [CHEQUE_DATE], SO.Invoice_Type [INVOICE_TYPE], SO.Invoice_Mode [INVOICE_MODE], SO.Payment_Mode [PAYMENT_MODE], 
	                               SO.DueDays [DUE_DAYS], SO.DueDate [DUE_DATE],  JA.AgentName [SALESMAN], SL.SLName [SUBLEDGER_DESC], SO.IND_Invoice [IND_INVOICE], SO.IND_Date [IND_INVOICEDATE],
                                   SO.QOT_Invoice [QUOTATION_INVOICE], SO.QOT_Date [QUOTATION_DATE], Dep1.DName [DEPARTMENT 1], Dep2.DName [DEPARTMENT 2],Dep3.DName [DEPARTMENT 3],  Dep4.DName [DEPARTMENT 4], 
	                               CO.CName [COUNTER], SO.NoOfPerson [NO OF PERSON],SO.Cur_Id,  SO.Cur_Rate, SO.B_Amount [BASIC_AMT],  SO.T_Amount [TERM_AMT], SO.N_Amount [NET_AMT], SO.LN_Amount [LOCAL_NETAMT], SO.V_Amount [VAT_AMT], SO.Tbl_Amount [TAXABLE_AMT],
	                               SO.Tender_Amount [TENDER_AMT],SO.Return_Amount [RETURN_AMT],SO.Action_Type [ACTIONTAG], SO.In_Words [INWORD], SO.Remarks [REMARKS], SO.R_Invoice [R_INVOICE], SO.Is_Printed [ISPRINTED], SO.No_Print [NOPRINT], SO.Printed_By [PRINTBY], 
	                               SO.Printed_Date [PRINTDATE], SO.Audit_Lock [AUDIT_LOCK], SO.Enter_By [ENTER_BY], SO.Enter_Date [ENTER_DATE], SO.Reconcile_By [RECONILE_BY],SO.Reconcile_Date [RECONCILE_DATE],SO.Auth_By [AUTH_BY], SO.Auth_Date [AUTH_DATE], 
	                               cmp.CmpUnit_Name [COMPANY_DESC], B.Branch_Name [BRACH_DESC],FY.Current_FY [FISCALYEAR] FROM AMS.SO_Master SO
	                               LEFT OUTER JOIN AMS.GeneralLedger AS GL ON GL.GLID = SO.Customer_Id 
	                               LEFT OUTER JOIN AMS.JuniorAgent AS JA ON JA.AgentId = SO.Agent_Id
	                               LEFT OUTER JOIN AMS.Subledger AS SL ON SL.SLId = SO.Subledger_Id
	                               LEFT OUTER JOIN AMS.Department AS Dep1 ON Dep1.DId=SO.Cls1 
	                               LEFT OUTER JOIN AMS.Department AS Dep2 ON Dep2.DId=SO.Cls2  
	                               LEFT OUTER JOIN AMS.Department AS Dep3 ON Dep3.DId=SO.Cls3 
	                               LEFT OUTER JOIN AMS.Department AS Dep4 ON Dep4.DId=SO.Cls4
                                   LEFT OUTER JOIN AMS.Counter AS CO ON CO.CId = SO.CounterId
	                               LEFT OUTER JOIN AMS.CompanyUnit AS cmp ON cmp.CmpUnit_Id = SO.CUnit_Id
	                               LEFT OUTER JOIN AMS.Branch AS B ON B.Branch_Id = SO.CBranch_Id
	                               LEFT OUTER JOIN AMS.FiscalYear AS FY ON FY.FY_Id = SO.FiscalYearId 
GO
/****** Object:  View [AMS].[ViewSalesOrder_OtherDetails]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
               CREATE VIEW [AMS].[ViewSalesOrder_OtherDetails] as  SELECT * FROM AMS.SO_Master_OtherDetails  
GO
/****** Object:  View [AMS].[ViewSalesProductWise_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

						Create View [AMS].[ViewSalesProductWise_Term] as Select SB_VNo as [Invoice No], ST_Name as [Term Description], ST_Sign as [Sign], Order_No as [Term ShortName], Sno as [Sno], ST_Rate as [Term Rate], ST.Amount As [Term Amount]  from AMS.SB_Term as ST Left outer Join AMS.ST_Term as TM On ST.ST_Id = TM.ST_ID where Term_Type = 'P'
							
GO
/****** Object:  View [AMS].[ViewSalesProductWiseHorizontal_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesProductWiseHorizontal_Term] as Select * from ( SELECT STD.SB_VNo,STD.SNo,STD.Product_Id,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=STD.SB_VNo WHERE Term_Type in('P') ) as d Pivot(max(Amount) for ST_Name in ([Prod-Discount], [VAT]) ) as pid 
GO
/****** Object:  View [AMS].[ViewSalesProductWiseHorizontalWithRate_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesProductWiseHorizontalWithRate_Term] as Select AMT.*,[Prod-Discount Rate], [VAT Rate] from ( SELECT STD.SB_VNo,STD.SNo,STD.Product_Id,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice=STD.SB_VNo WHERE Term_Type in('P') ) as d  Pivot(max(Amount) for ST_Name in ([Prod-Discount], [VAT]) ) as Amt  Left Outer Join(select * from(SELECT STD.SB_VNo, STD.SNo, Rate Term_Rate, SBT.ST_Name + ' Rate'[ST_Name] FROM AMS.SB_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SB_Master as SIM On SIM.SB_Invoice = STD.SB_VNo  WHERE Term_Type in ('P')) as d Pivot(max(Term_Rate) for ST_Name in ([Prod-Discount Rate], [VAT Rate]) ) as pid ) as Rate  On Amt.SB_VNo = Rate.SB_VNo and Amt.SNo = Rate.SNo; 
GO
/****** Object:  View [AMS].[ViewSalesReturn_Details]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [AMS].[ViewSalesReturn_Details] as Select SBD.SR_Invoice as [INVOICE NO], SBD.Invoice_SNo as [SNO],SBD.SB_Invoice as [REF INVOICE],Sbd.SB_Sno as [REF SNO],SBD.P_Id as [PRODUCT CODE],PD.PShortName [PRODUCT SHORTNAME], PD.PName as [PRODUCT] , PrGrp.GrpName as [PRODUCT GROUP],PrSGrp.SubGrpName as [PRODUCT SUBGROUP], GDN.GName as [GODOWN DESC], GDN.GCode as [GDN SHORTNAME], GDN.GLocation as [GDN ADDRESS], SBD.Alt_Qty as [ALT QTY], PAU.UnitCode as [ALT UNIT],SBD.Qty as [QTY],PU.UnitName [UNIT DESC],SBD.AltStock_Qty as [ACTUAL ALTQTY],SBD.Stock_Qty as [ACTUAL QTY], SBD.Free_Qty as [FREE QTY],PFU.UID as [FREE UNIT],PD.PMRP as [MRP], PD.PBuyRate as [COST RATE], PD.PSalesRate as [SALES RATE], PD.TradeRate as [TRADE PRICE], SBD.Rate as [INVOICE RATE], SBD.B_Amount as [BASIC AMOUNT], SBD.T_Amount  as [TERM AMOUNT], SBD.N_Amount as [NET AMOUNT], SBD.B_Amount* SBM.Cur_Rate as [LOCAL BASIC], SBD.T_Amount * SBM.Cur_Rate  as [LOCAL TERM], SBD.N_Amount*SBM.Cur_Rate as [LOCAL NETAMOUNT], SBD.Narration as [NARRATION], SBD.altStock_Qty as [AltStock Qty],SBD.Tax_Amount [TAXABLE AMOUNT],SBD.V_Amount [VAT AMOUNT],SBD.V_Rate [VAT RATE] from AMS.SR_Details as SBD  Left outer Join AMS.SR_Master as SBM On SBM.SR_Invoice = SBD.SR_Invoice  Left outer Join AMS.Product as PD On SBD.P_Id= PD.PID  Left outer Join AMS.ProductUnit as PU On SBD.Unit_Id= PU.UID  Left outer Join AMS.ProductUnit as PAU On SBD.Alt_UnitId= PAU.UID  Left outer Join AMS.ProductUnit as PFU On SBD.Free_Unit_Id= PFU.UID  Left Outer Join AMS.ProductGroup as PrGrp on PD.PGrpId=PrGrp.PGrpID  Left Outer Join AMS.ProductSubGroup as PrsGrp on PrsGrp.PSubGrpId=PD.PSubGrpId  Left outer Join AMS.Godown as GDN On SBD.Gdn_Id = GDN.GID 
GO
/****** Object:  View [AMS].[ViewSalesReturn_Header]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 Create View [AMS].[ViewSalesReturn_Header] as Select SR_Invoice [INVOICE NUMBER],SB.Invoice_Date [INVOICE DATE],SB.Invoice_Miti [INVOICE MITI],SB.Invoice_Time [INVOICE TIME],SB_Invoice [REF VOUCHERNO],SB.SB_Date [REF DATE],SB.SB_Miti [REF MITI],GL.GLName [INVOICE CUSTOMER],GL.PANNO [CUSTOMER PAN],Gl.GLAddress[CUSTOMER ADDRESS],GL.PhoneNo [CUSTOMER PHONENO],Gl.LandLineNo [OFFICE NUMBER],GL.CrLimit [CUSTOMER CRLIMIT],GL.CrDays [CUSTOMER CRDAYS],Gl.IntRate [INTERST RATE],GL.OwnerName [OWNER'S NAME],GL.Email[CUSTOMER EMAIL],SB.Party_Name [PARTY DESC], SB.Vat_No [Party Vat],SB.Contact_Person [PARTY CONTACTPERSON],SB.Invoice_Type [BILL CATEGORY],SB.Invoice_Mode [INVOICE MODE],SB.DueDays [BILL DUE DAYS], SB.DueDate [BILL DUE DATE],JA.AgentId [SALES MAN], Sl.SLName AS [SUBLEDGER],Dep1.DName [CLS1 DESC], Dep2.DName [CLS2 DESC],Dep3.DName [CLS3 DESC],  Dep4.DName [CLS4 DESC],COU.CName,COU.CCode,Cur.Ccode [CURRENCY CODE], SB.Cur_Rate [CURRENCY RATE],SB.B_Amount [BASIC AMT],SB.T_Amount [TERM AMT],SB.N_Amount [NET AMT],SB.LN_Amount [LOCAL NETAMT], SB.V_Amount [VAT AMOUNT],SB.Tbl_Amount [TAXABLE AMT],SB.Tender_Amount [TENDER AMOUNT],SB.Return_Amount[RETURN AMOUNT],SB.Action_Type [ACTION TYPE],SB.In_Words [NUMINWORDS],SB.Is_Printed [PRINTED],SB.No_Print [NO OF PRINT],SB.Printed_By [PRINT BY], SB.Printed_Date [PRINT DATE],SB.Audit_Lock [VOUCHER LOCK],SB.Enter_By [ENTER BY],SB.Enter_Date [ENTER DATE],SB.Reconcile_By [RECONCILE BY],SB.Reconcile_Date [RECONCILE DATE],SB.Auth_By [AUTHORIZED BY],SB.Auth_Date [AUTHORIZED DATE],SB.Cleared_By [CLEARED BY],SB.Cleared_Date [CLEAR DATE],SB.Cancel_By [CANCEL BY], SB.Cancel_Date [CANCEL DATE],SB.Cancel_Remarks [CANCEL REMARKS],CU.CmpUnit_Name[COMPANY UNITDESC],B.Branch_Name [BRANCH DESC],IsAPIPosted,IsRealtime From AMS.SR_Master as SB left outer JOIN  AMS.Branch B ON SB.CBranch_Id = B.Branch_Id  left outer JOIN  AMS.CompanyUnit CU ON SB.CUnit_Id = CU.CmpUnit_Id  Left outer Join AMS.GeneralLedger as Gl On SB.Customer_ID = Gl.GLID  Left outer Join AMS.Currency as Cur On SB.Cur_Id = Cur.CId  Left outer Join AMS.JuniorAgent JA On SB.Agent_Id = JA.AgentId   Left Outer join AMS.Counter as COU on COU.CId=SB.CounterId Left Outer join AMS.Department as Dep1 on Dep1.DId=SB.Cls1 Left Outer join AMS.Department as Dep2 on Dep2.DId=SB.Cls2  Left Outer join AMS.Department as Dep3 on Dep3.DId=SB.Cls3 Left Outer join AMS.Department as Dep4 on Dep4.DId=SB.Cls4 Left Outer join AMS.Subledger as Sl on Sl.SLId=SB.Subledger_Id Left Outer Join AMS.Area As Area On GL.AreaId = Area.AreaId
GO
/****** Object:  View [AMS].[ViewSalesReturnBillWiseHorizontal_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesReturnBillWiseHorizontal_Term] as Select * from ( SELECT STD.SR_VNo,STD.SNo,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice=STD.SR_VNo WHERE Term_Type in('B') ) as d Pivot(max(Amount) for ST_Name in ([Discount]) ) as pid 
GO
/****** Object:  View [AMS].[ViewSalesReturnBillWiseHorizontalWithRate_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesReturnBillWiseHorizontalWithRate_Term] as Select AMT.*,[Discount Rate] from ( SELECT STD.SR_VNo,STD.SNo,STD.Product_Id,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice=STD.SR_VNo WHERE Term_Type in('B') ) as d  Pivot(max(Amount) for ST_Name in ([Discount]) ) as Amt  Left Outer Join(select * from(SELECT STD.SR_VNo, STD.SNo, Rate Term_Rate, SBT.ST_Name + ' Rate'[ST_Name] FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice = STD.SR_VNo  WHERE Term_Type in ('B')) as d Pivot(max(Term_Rate) for ST_Name in ([Discount Rate]) ) as pid ) as Rate  On Amt.SR_VNo = Rate.SR_VNo and Amt.SNo = Rate.SNo 
GO
/****** Object:  View [AMS].[ViewSalesReturnProductWiseHorizontal_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 Create View [AMS].[ViewSalesReturnProductWiseHorizontal_Term] as select @@servername[ServerName]
GO
/****** Object:  View [AMS].[ViewSalesReturnProductWiseHorizontalWithRate_Term]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [AMS].[ViewSalesReturnProductWiseHorizontalWithRate_Term] as Select AMT.*,[Prod-Discount Rate], [VAT Rate] from ( SELECT STD.SR_VNo,STD.SNo,STD.Product_Id,CASE WHEN ST_Sign = '+' then Amount else -Amount end as Amount,SBT.ST_Name FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice=STD.SR_VNo WHERE Term_Type in('P') ) as d  Pivot(max(Amount) for ST_Name in ([Prod-Discount], [VAT]) ) as Amt  Left Outer Join(select * from (SELECT STD.SR_VNo, STD.SNo, Rate Term_Rate, SBT.ST_Name + ' Rate'[ST_Name] FROM AMS.SR_Term as STD inner join AMS.ST_Term as SBT on STD.ST_Id = SBT.ST_Id Inner Join AMS.SR_Master as SIM On SIM.SR_Invoice = STD.SR_VNo  WHERE Term_Type in ('P')) as d Pivot(max(Term_Rate) for ST_Name in ([Prod-Discount Rate], [VAT Rate]) ) as pid ) as Rate  On Amt.SR_VNo = Rate.SR_VNo and Amt.SNo = Rate.SNo 
GO
/****** Object:  View [dbo].[ViewBarcode]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view[dbo].[ViewBarcode] as Select PB_Invoice, PB_Details.P_Id, Qty, Product.PName,PShortName,PRODUCT.PSalesRate from ams.PB_Details left join ams.product on product.PId= PB_Details.P_Id; 
GO
ALTER TABLE [AMS].[Area] ADD  CONSTRAINT [DF_Area_Country]  DEFAULT (N'Nepal') FOR [Country]
GO
ALTER TABLE [AMS].[Area] ADD  CONSTRAINT [DF_Area_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[Area] ADD  CONSTRAINT [DF_Area_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] ADD  CONSTRAINT [DF_BillOfMaterial_Master_Alt_Qty]  DEFAULT ((0)) FOR [AltQty]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] ADD  CONSTRAINT [DF_BillOfMaterial_Master_Qty]  DEFAULT ((0)) FOR [Qty]
GO
ALTER TABLE [AMS].[CostCenter] ADD  CONSTRAINT [DF_CostCenter_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[CostCenter] ADD  CONSTRAINT [DF_CostCenter_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[Department] ADD  CONSTRAINT [DF_Department_Dlevel]  DEFAULT (N'I') FOR [Dlevel]
GO
ALTER TABLE [AMS].[Department] ADD  CONSTRAINT [DF_Department_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[Department] ADD  CONSTRAINT [DF_Department_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[Floor] ADD  CONSTRAINT [DF_Floor_Type]  DEFAULT ('1St') FOR [Type]
GO
ALTER TABLE [AMS].[Floor] ADD  CONSTRAINT [DF_Floor_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[Floor] ADD  CONSTRAINT [DF_Floor_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_Commission]  DEFAULT ((0.00)) FOR [Commission]
GO
ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_CRLimit]  DEFAULT ((0.00)) FOR [CRLimit]
GO
ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_CrDays]  DEFAULT ((0)) FOR [CrDays]
GO
ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_CrType]  DEFAULT (N'Ignore') FOR [CrType]
GO
ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[JuniorAgent] ADD  CONSTRAINT [DF_JuniorAgent_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[MainArea] ADD  CONSTRAINT [DF_MainArea_MCountry]  DEFAULT (N'Nepal') FOR [MCountry]
GO
ALTER TABLE [AMS].[MainArea] ADD  CONSTRAINT [DF_MainArea_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[MainArea] ADD  CONSTRAINT [DF_MainArea_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[MemberType] ADD  CONSTRAINT [DF_MemberType_Discount]  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [AMS].[MemberType] ADD  CONSTRAINT [DF_MemberType_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[MemberType] ADD  CONSTRAINT [DF_MemberType_ActiveStatus]  DEFAULT ((1)) FOR [ActiveStatus]
GO
ALTER TABLE [AMS].[Notes_Master] ADD  CONSTRAINT [DF_Notes_Master_Currency_Rate]  DEFAULT ((1)) FOR [Currency_Rate]
GO
ALTER TABLE [AMS].[Notes_Master] ADD  CONSTRAINT [DF_Notes_Master_Audit_Lock]  DEFAULT ((0)) FOR [Audit_Lock]
GO
ALTER TABLE [AMS].[Notes_Master] ADD  CONSTRAINT [DF_Notes_Master_PrintValue]  DEFAULT ((0)) FOR [PrintValue]
GO
ALTER TABLE [AMS].[ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[ProductSubGroup] ADD  CONSTRAINT [DF_ProductSubGroup_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Rate]  DEFAULT ((0.00)) FOR [PT_Rate]
GO
ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Profitability]  DEFAULT ((1)) FOR [PT_Profitability]
GO
ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Supess]  DEFAULT ((1)) FOR [PT_Supess]
GO
ALTER TABLE [AMS].[PT_Term] ADD  CONSTRAINT [DF_PT_Term_PT_Status]  DEFAULT ((1)) FOR [PT_Status]
GO
ALTER TABLE [AMS].[SeniorAgent] ADD  CONSTRAINT [DF_SeniorAgent_Comm]  DEFAULT ((0.00)) FOR [Comm]
GO
ALTER TABLE [AMS].[SeniorAgent] ADD  CONSTRAINT [DF_SeniorAgent_Company_ID]  DEFAULT ((0.00)) FOR [Company_ID]
GO
ALTER TABLE [AMS].[SeniorAgent] ADD  CONSTRAINT [DF_SeniorAgent_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[Subledger] ADD  CONSTRAINT [DF_Subledger_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[Subledger] ADD  CONSTRAINT [DF_Subledger_EnterBy]  DEFAULT (N'MrSolution') FOR [EnterBy]
GO
ALTER TABLE [AMS].[TableMaster] ADD  CONSTRAINT [DF_TableMaster_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [AMS].[TableMaster] ADD  CONSTRAINT [DF_TableMaster_EnterBy]  DEFAULT ('MrSolution') FOR [EnterBy]
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
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_FiscalYear]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_GeneralLedgerCash] FOREIGN KEY([CbLedger_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_GeneralLedgerCash]
GO
ALTER TABLE [AMS].[AccountDetails]  WITH CHECK ADD  CONSTRAINT [FK_AccountDetails_GeneralLedgerId] FOREIGN KEY([Ledger_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[AccountDetails] CHECK CONSTRAINT [FK_AccountDetails_GeneralLedgerId]
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
ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_CompanyUnit] FOREIGN KEY([AreaId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_CompanyUnit]
GO
ALTER TABLE [AMS].[Area]  WITH CHECK ADD  CONSTRAINT [FK_Area_MainArea] FOREIGN KEY([Main_Area])
REFERENCES [AMS].[MainArea] ([MAreaId])
GO
ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_MainArea]
GO
ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_BillOfMaterial_Master] FOREIGN KEY([MemoNo])
REFERENCES [AMS].[BillOfMaterial_Master] ([MemoNo])
GO
ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_BillOfMaterial_Master]
GO
ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_CostCenter]
GO
ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_Godown] FOREIGN KEY([GdnId])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_Godown]
GO
ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_Product] FOREIGN KEY([ProductId])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_Product]
GO
ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_ProductAltUnitId] FOREIGN KEY([AltUnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_ProductAltUnitId]
GO
ALTER TABLE [AMS].[BillOfMaterial_Details]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Details_ProductUnit] FOREIGN KEY([UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Details] CHECK CONSTRAINT [FK_BillOfMaterial_Details_ProductUnit]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_Branch]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_CostCenter]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_Godown] FOREIGN KEY([GdnId])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_Godown]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_Product] FOREIGN KEY([FGProductId])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_Product]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_ProductAltUnitId] FOREIGN KEY([AltUnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_ProductAltUnitId]
GO
ALTER TABLE [AMS].[BillOfMaterial_Master]  WITH CHECK ADD  CONSTRAINT [FK_BillOfMaterial_Master_ProductUnitId] FOREIGN KEY([UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[BillOfMaterial_Master] CHECK CONSTRAINT [FK_BillOfMaterial_Master_ProductUnitId]
GO
ALTER TABLE [AMS].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Department] FOREIGN KEY([Dep1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Budget] CHECK CONSTRAINT [FK_Budget_Department]
GO
ALTER TABLE [AMS].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Budget] CHECK CONSTRAINT [FK_Budget_GeneralLedger]
GO
ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_CB_Master] FOREIGN KEY([Voucher_No])
REFERENCES [AMS].[CB_Master] ([Voucher_No])
GO
ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_CB_Master]
GO
ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_Department]
GO
ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_GeneralLedger]
GO
ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_JuniorAgent]
GO
ALTER TABLE [AMS].[CB_Details]  WITH CHECK ADD  CONSTRAINT [FK_CB_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[CB_Details] CHECK CONSTRAINT [FK_CB_Details_Subledger]
GO
ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_Branch]
GO
ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_Currency]
GO
ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_Department]
GO
ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_FiscalYear]
GO
ALTER TABLE [AMS].[CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_CB_Master_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[CB_Master] CHECK CONSTRAINT [FK_CB_Master_GeneralLedger]
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
ALTER TABLE [AMS].[CostCenterExpenses_Details]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Details_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Details] CHECK CONSTRAINT [FK_CostCenterExpenses_Details_CostCenter]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Details]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Details_CostCenterExpenses_Master] FOREIGN KEY([CostingNo])
REFERENCES [AMS].[CostCenterExpenses_Master] ([CostingNo])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Details] CHECK CONSTRAINT [FK_CostCenterExpenses_Details_CostCenterExpenses_Master]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Details]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Details_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Details] CHECK CONSTRAINT [FK_CostCenterExpenses_Details_GeneralLedger]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Branch]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_CostCenter]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Department]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_FiscalYear]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Godown] FOREIGN KEY([GdnId])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Godown]
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master]  WITH CHECK ADD  CONSTRAINT [FK_CostCenterExpenses_Master_Product] FOREIGN KEY([FGProductId])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[CostCenterExpenses_Master] CHECK CONSTRAINT [FK_CostCenterExpenses_Master_Product]
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
ALTER TABLE [AMS].[DocumentDesignPrint]  WITH CHECK ADD  CONSTRAINT [FK_DocumentDesignPrint_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[DocumentDesignPrint] CHECK CONSTRAINT [FK_DocumentDesignPrint_Branch]
GO
ALTER TABLE [AMS].[DocumentDesignPrint]  WITH CHECK ADD  CONSTRAINT [FK_DocumentDesignPrint_CompanyUnit] FOREIGN KEY([CmpUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[DocumentDesignPrint] CHECK CONSTRAINT [FK_DocumentDesignPrint_CompanyUnit]
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
ALTER TABLE [AMS].[DocumentNumbering]  WITH CHECK ADD  CONSTRAINT [FK_DocumentNumbering_DocumentNumbering] FOREIGN KEY([DocId], [DocModule], [DocDesc])
REFERENCES [AMS].[DocumentNumbering] ([DocId], [DocModule], [DocDesc])
GO
ALTER TABLE [AMS].[DocumentNumbering] CHECK CONSTRAINT [FK_DocumentNumbering_DocumentNumbering]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Branch]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_CostCenter] FOREIGN KEY([CC_Id])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_CostCenter]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department1] FOREIGN KEY([Cls2])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department1]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department2] FOREIGN KEY([Cls3])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department2]
GO
ALTER TABLE [AMS].[FGR_Master]  WITH CHECK ADD  CONSTRAINT [FK_FGR_Master_Department3] FOREIGN KEY([Cls4])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[FGR_Master] CHECK CONSTRAINT [FK_FGR_Master_Department3]
GO
ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger] FOREIGN KEY([ProfiLossId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger]
GO
ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger1] FOREIGN KEY([CashId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger1]
GO
ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger2] FOREIGN KEY([VATLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger2]
GO
ALTER TABLE [AMS].[FinanceSetting]  WITH CHECK ADD  CONSTRAINT [FK_FinanceSetting_GeneralLedger3] FOREIGN KEY([PDCBankLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[FinanceSetting] CHECK CONSTRAINT [FK_FinanceSetting_GeneralLedger3]
GO
ALTER TABLE [AMS].[Floor]  WITH CHECK ADD  CONSTRAINT [FK_Floor_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Floor] CHECK CONSTRAINT [FK_Floor_Branch]
GO
ALTER TABLE [AMS].[Floor]  WITH CHECK ADD  CONSTRAINT [FK_Floor_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Floor] CHECK CONSTRAINT [FK_Floor_CompanyUnit]
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
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_Area] FOREIGN KEY([AreaId])
REFERENCES [AMS].[Area] ([AreaId])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_Area]
GO
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_Branch]
GO
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_CompanyUnit]
GO
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_Currency] FOREIGN KEY([CurrId])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_Currency]
GO
ALTER TABLE [AMS].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_JuniorAgent] FOREIGN KEY([AgentId])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_JuniorAgent]
GO
ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_Department]
GO
ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_GT_Master] FOREIGN KEY([VoucherNo])
REFERENCES [AMS].[GT_Master] ([VoucherNo])
GO
ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_GT_Master]
GO
ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_Product] FOREIGN KEY([ProId])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_Product]
GO
ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_ProductAltUnit] FOREIGN KEY([AltUOM])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_ProductAltUnit]
GO
ALTER TABLE [AMS].[GT_Details]  WITH CHECK ADD  CONSTRAINT [FK_GT_Details_ProductUnit] FOREIGN KEY([UOM])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[GT_Details] CHECK CONSTRAINT [FK_GT_Details_ProductUnit]
GO
ALTER TABLE [AMS].[GT_Master]  WITH CHECK ADD  CONSTRAINT [FK_GT_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[GT_Master] CHECK CONSTRAINT [FK_GT_Master_Branch]
GO
ALTER TABLE [AMS].[GT_Master]  WITH CHECK ADD  CONSTRAINT [FK_GT_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[GT_Master] CHECK CONSTRAINT [FK_GT_Master_Department]
GO
ALTER TABLE [AMS].[GT_Master]  WITH CHECK ADD  CONSTRAINT [FK_GT_Master_Godown] FOREIGN KEY([FrmGdn])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[GT_Master] CHECK CONSTRAINT [FK_GT_Master_Godown]
GO
ALTER TABLE [AMS].[InventorySetting]  WITH CHECK ADD  CONSTRAINT [FK_InventorySetting_GeneralLedger] FOREIGN KEY([OPLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[InventorySetting] CHECK CONSTRAINT [FK_InventorySetting_GeneralLedger]
GO
ALTER TABLE [AMS].[InventorySetting]  WITH CHECK ADD  CONSTRAINT [FK_InventorySetting_GeneralLedger1] FOREIGN KEY([CSBSLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[InventorySetting] CHECK CONSTRAINT [FK_InventorySetting_GeneralLedger1]
GO
ALTER TABLE [AMS].[InventorySetting]  WITH CHECK ADD  CONSTRAINT [FK_InventorySetting_GeneralLedger2] FOREIGN KEY([CSPLLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[InventorySetting] CHECK CONSTRAINT [FK_InventorySetting_GeneralLedger2]
GO
ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_Branch] FOREIGN KEY([Branch_id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_Branch]
GO
ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_CompanyUnit]
GO
ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_GeneralLedger] FOREIGN KEY([GLCode])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_GeneralLedger]
GO
ALTER TABLE [AMS].[JuniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_JuniorAgent_SeniorAgent] FOREIGN KEY([SAgent])
REFERENCES [AMS].[SeniorAgent] ([SAgentId])
GO
ALTER TABLE [AMS].[JuniorAgent] CHECK CONSTRAINT [FK_JuniorAgent_SeniorAgent]
GO
ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_Department]
GO
ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_GeneralLedger]
GO
ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_GeneralLedgerCB] FOREIGN KEY([CBLedger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_GeneralLedgerCB]
GO
ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_JuniorAgent]
GO
ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_JV_Master] FOREIGN KEY([Voucher_No])
REFERENCES [AMS].[JV_Master] ([Voucher_No])
GO
ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_JV_Master]
GO
ALTER TABLE [AMS].[JV_Details]  WITH CHECK ADD  CONSTRAINT [FK_JV_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[JV_Details] CHECK CONSTRAINT [FK_JV_Details_Subledger]
GO
ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_Branch]
GO
ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_Currency]
GO
ALTER TABLE [AMS].[JV_Master]  WITH CHECK ADD  CONSTRAINT [FK_JV_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[JV_Master] CHECK CONSTRAINT [FK_JV_Master_Department]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Branch]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_CompanyUnit]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Currency]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Department] FOREIGN KEY([Cls1])
REFERENCES [HOS].[Department] ([DId])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Department]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_FiscalYear]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_GeneralLedger]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_JuniorAgent]
GO
ALTER TABLE [AMS].[LedgerOpening]  WITH CHECK ADD  CONSTRAINT [FK_LedgerOpening_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[LedgerOpening] CHECK CONSTRAINT [FK_LedgerOpening_Subledger]
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
ALTER TABLE [AMS].[MemberShipSetup]  WITH CHECK ADD  CONSTRAINT [FK_MemberShipSetup_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[MemberShipSetup] CHECK CONSTRAINT [FK_MemberShipSetup_Branch]
GO
ALTER TABLE [AMS].[MemberShipSetup]  WITH CHECK ADD  CONSTRAINT [FK_MemberShipSetup_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[MemberShipSetup] CHECK CONSTRAINT [FK_MemberShipSetup_CompanyUnit]
GO
ALTER TABLE [AMS].[MemberType]  WITH CHECK ADD  CONSTRAINT [FK_MemberType_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[MemberType] CHECK CONSTRAINT [FK_MemberType_Branch]
GO
ALTER TABLE [AMS].[MemberType]  WITH CHECK ADD  CONSTRAINT [FK_MemberType_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[MemberType] CHECK CONSTRAINT [FK_MemberType_CompanyUnit]
GO
ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_Department]
GO
ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_GeneralLedger]
GO
ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_GeneralLedgerVatLedger] FOREIGN KEY([VatLedger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_GeneralLedgerVatLedger]
GO
ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_JuniorAgent]
GO
ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_Notes_Master] FOREIGN KEY([Voucher_No])
REFERENCES [AMS].[Notes_Master] ([Voucher_No])
GO
ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_Notes_Master]
GO
ALTER TABLE [AMS].[Notes_Details]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[Notes_Details] CHECK CONSTRAINT [FK_Notes_Details_Subledger]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Branch]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Currency]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Department]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_FiscalYear]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[Notes_Master]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[Notes_Master] CHECK CONSTRAINT [FK_Notes_Master_Subledger]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_GeneralLedger]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_GeneralLedger1] FOREIGN KEY([CBLedger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_GeneralLedger1]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_JuniorAgent]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_PAB_Master] FOREIGN KEY([PAB_Invoice])
REFERENCES [AMS].[PAB_Master] ([PAB_Invoice])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_PAB_Master]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_Product]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_PT_Term]
GO
ALTER TABLE [AMS].[PAB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Details_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PAB_Details] CHECK CONSTRAINT [FK_PAB_Details_Subledger]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_Branch]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_Currency]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_Department]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[PAB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PAB_Master_PB_Master] FOREIGN KEY([PB_Invoice])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])
GO
ALTER TABLE [AMS].[PAB_Master] CHECK CONSTRAINT [FK_PAB_Master_PB_Master]
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
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductAltUnitId] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductAltUnitId]
GO
ALTER TABLE [AMS].[PB_Details]  WITH CHECK ADD  CONSTRAINT [FK_PB_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PB_Details] CHECK CONSTRAINT [FK_PB_Details_ProductUnit]
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
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Counter] FOREIGN KEY([Counter_ID])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Counter]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Currency]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Department]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[PB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PB_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PB_Master] CHECK CONSTRAINT [FK_PB_Master_Subledger]
GO
ALTER TABLE [AMS].[PB_OtherMaster]  WITH CHECK ADD  CONSTRAINT [FK_PB_OtherMaster_PB_Master] FOREIGN KEY([PAB_Invoice])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])
GO
ALTER TABLE [AMS].[PB_OtherMaster] CHECK CONSTRAINT [FK_PB_OtherMaster_PB_Master]
GO
ALTER TABLE [AMS].[PB_Term]  WITH CHECK ADD  CONSTRAINT [FK_PB_Term_PB_Master] FOREIGN KEY([PB_VNo])
REFERENCES [AMS].[PB_Master] ([PB_Invoice])
GO
ALTER TABLE [AMS].[PB_Term] CHECK CONSTRAINT [FK_PB_Term_PB_Master]
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
ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_PBT_Master] FOREIGN KEY([PBT_Invoice])
REFERENCES [AMS].[PBT_Master] ([PBT_Invoice])
GO
ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_PBT_Master]
GO
ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_Product]
GO
ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_ProductGroup] FOREIGN KEY([PGrp_Id])
REFERENCES [AMS].[ProductGroup] ([PGrpID])
GO
ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_ProductGroup]
GO
ALTER TABLE [AMS].[PBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Details_Subledger] FOREIGN KEY([Slb_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PBT_Details] CHECK CONSTRAINT [FK_PBT_Details_Subledger]
GO
ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_Branch]
GO
ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_Currency]
GO
ALTER TABLE [AMS].[PBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Master_GeneralLedger] FOREIGN KEY([Vendor_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PBT_Master] CHECK CONSTRAINT [FK_PBT_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[PBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PBT_Term] CHECK CONSTRAINT [FK_PBT_Term_Product]
GO
ALTER TABLE [AMS].[PBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PBT_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PBT_Term] CHECK CONSTRAINT [FK_PBT_Term_PT_Term]
GO
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_Godown]
GO
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_PC_Master] FOREIGN KEY([PC_Invoice])
REFERENCES [AMS].[PC_Master] ([PC_Invoice])
GO
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_PC_Master]
GO
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_Product]
GO
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductAltUnitId] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductAltUnitId]
GO
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductUnit]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Branch]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Counter] FOREIGN KEY([Counter_ID])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Counter]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Currency]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Department]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Subledger]
GO
ALTER TABLE [AMS].[PC_Term]  WITH CHECK ADD  CONSTRAINT [FK_PC_Term_PC_Master] FOREIGN KEY([PC_VNo])
REFERENCES [AMS].[PC_Master] ([PC_Invoice])
GO
ALTER TABLE [AMS].[PC_Term] CHECK CONSTRAINT [FK_PC_Term_PC_Master]
GO
ALTER TABLE [AMS].[PC_Term]  WITH CHECK ADD  CONSTRAINT [FK_PC_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PC_Term] CHECK CONSTRAINT [FK_PC_Term_Product]
GO
ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_Godown]
GO
ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_PIN_Master] FOREIGN KEY([PIN_Invoice])
REFERENCES [AMS].[PIN_Master] ([PIN_Invoice])
GO
ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_PIN_Master]
GO
ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_Product]
GO
ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_ProductUnit] FOREIGN KEY([Alt_Unit])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_ProductUnit]
GO
ALTER TABLE [AMS].[PIN_Details]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Details_ProductUnit1] FOREIGN KEY([Unit])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PIN_Details] CHECK CONSTRAINT [FK_PIN_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_Branch]
GO
ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_Department]
GO
ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PIN_Master]  WITH CHECK ADD  CONSTRAINT [FK_PIN_Master_Subledger] FOREIGN KEY([Sub_Ledger])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PIN_Master] CHECK CONSTRAINT [FK_PIN_Master_Subledger]
GO
ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_Godown]
GO
ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_PO_Master] FOREIGN KEY([PO_Invoice])
REFERENCES [AMS].[PO_Master] ([PO_Invoice])
GO
ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_PO_Master]
GO
ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_Product]
GO
ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_ProductUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_ProductUnit]
GO
ALTER TABLE [AMS].[PO_Details]  WITH CHECK ADD  CONSTRAINT [FK_PO_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PO_Details] CHECK CONSTRAINT [FK_PO_Details_ProductUnit1]
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
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_CompanyUnit1] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_CompanyUnit1]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Currency]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Department]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[PO_Master]  WITH CHECK ADD  CONSTRAINT [FK_PO_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PO_Master] CHECK CONSTRAINT [FK_PO_Master_Subledger]
GO
ALTER TABLE [AMS].[PO_Term]  WITH CHECK ADD  CONSTRAINT [FK_PO_Term_PO_Master] FOREIGN KEY([PO_VNo])
REFERENCES [AMS].[PO_Master] ([PO_Invoice])
GO
ALTER TABLE [AMS].[PO_Term] CHECK CONSTRAINT [FK_PO_Term_PO_Master]
GO
ALTER TABLE [AMS].[PO_Term]  WITH CHECK ADD  CONSTRAINT [FK_PO_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PO_Term] CHECK CONSTRAINT [FK_PO_Term_Product]
GO
ALTER TABLE [AMS].[PO_Term]  WITH CHECK ADD  CONSTRAINT [FK_PO_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PO_Term] CHECK CONSTRAINT [FK_PO_Term_PT_Term]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_Branch]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_CompanyUnit]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_Department]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_FiscalYear]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_GeneralLedger]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_JuniorAgent] FOREIGN KEY([AgentId])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_JuniorAgent]
GO
ALTER TABLE [AMS].[PostDateCheque]  WITH CHECK ADD  CONSTRAINT [FK_PostDateCheque_Subledger] FOREIGN KEY([SubLedgerId])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PostDateCheque] CHECK CONSTRAINT [FK_PostDateCheque_Subledger]
GO
ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_Godown]
GO
ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_PR_Master] FOREIGN KEY([PR_Invoice])
REFERENCES [AMS].[PR_Master] ([PR_Invoice])
GO
ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_PR_Master]
GO
ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_Product]
GO
ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_ProductUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_ProductUnit]
GO
ALTER TABLE [AMS].[PR_Details]  WITH CHECK ADD  CONSTRAINT [FK_PR_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[PR_Details] CHECK CONSTRAINT [FK_PR_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Branch]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Currency]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Department]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[PR_Master]  WITH CHECK ADD  CONSTRAINT [FK_PR_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[PR_Master] CHECK CONSTRAINT [FK_PR_Master_Subledger]
GO
ALTER TABLE [AMS].[PR_Term]  WITH CHECK ADD  CONSTRAINT [FK_PR_Term_PR_Master] FOREIGN KEY([PR_VNo])
REFERENCES [AMS].[PR_Master] ([PR_Invoice])
GO
ALTER TABLE [AMS].[PR_Term] CHECK CONSTRAINT [FK_PR_Term_PR_Master]
GO
ALTER TABLE [AMS].[PR_Term]  WITH CHECK ADD  CONSTRAINT [FK_PR_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[PR_Term] CHECK CONSTRAINT [FK_PR_Term_Product]
GO
ALTER TABLE [AMS].[PR_Term]  WITH CHECK ADD  CONSTRAINT [FK_PR_Term_PT_Term] FOREIGN KEY([PT_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PR_Term] CHECK CONSTRAINT [FK_PR_Term_PT_Term]
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
REFERENCES [HOS].[Department] ([DId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedger] FOREIGN KEY([PSR])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedger]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerPurchase] FOREIGN KEY([PPL])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerPurchase]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerPurchaseReturn] FOREIGN KEY([PPR])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerPurchaseReturn]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerSales] FOREIGN KEY([PSL])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerSales]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_GeneralLedgerSalesReturn] FOREIGN KEY([PSR])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_GeneralLedgerSalesReturn]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductAltUnit] FOREIGN KEY([PAltUnit])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductAltUnit]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY([PGrpId])
REFERENCES [AMS].[ProductGroup] ([PGrpID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductGroup]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductSubGroup] FOREIGN KEY([PSubGrpId])
REFERENCES [AMS].[ProductSubGroup] ([PSubGrpId])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductSubGroup]
GO
ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductUnit] FOREIGN KEY([PUnit])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_ProductUnit]
GO
ALTER TABLE [AMS].[ProductClosingRate]  WITH CHECK ADD  CONSTRAINT [FK_ProductClosingRate_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ProductClosingRate] CHECK CONSTRAINT [FK_ProductClosingRate_Branch]
GO
ALTER TABLE [AMS].[ProductClosingRate]  WITH CHECK ADD  CONSTRAINT [FK_ProductClosingRate_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ProductClosingRate] CHECK CONSTRAINT [FK_ProductClosingRate_CompanyUnit]
GO
ALTER TABLE [AMS].[ProductClosingRate]  WITH CHECK ADD  CONSTRAINT [FK_ProductClosingRate_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[ProductClosingRate] CHECK CONSTRAINT [FK_ProductClosingRate_Product]
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
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Branch]
GO
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_CompanyUnit]
GO
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Currency]
GO
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Department]
GO
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_FiscalYear]
GO
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Godown] FOREIGN KEY([Godown_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Godown]
GO
ALTER TABLE [AMS].[ProductOpening]  WITH CHECK ADD  CONSTRAINT [FK_ProductOpening_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[ProductOpening] CHECK CONSTRAINT [FK_ProductOpening_Product]
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
ALTER TABLE [AMS].[ProductUnit]  WITH CHECK ADD  CONSTRAINT [FK_ProductUnit_Branch] FOREIGN KEY([Branch_ID])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ProductUnit] CHECK CONSTRAINT [FK_ProductUnit_Branch]
GO
ALTER TABLE [AMS].[ProductUnit]  WITH CHECK ADD  CONSTRAINT [FK_ProductUnit_CompanyUnit] FOREIGN KEY([Company_ID])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ProductUnit] CHECK CONSTRAINT [FK_ProductUnit_CompanyUnit]
GO
ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_Branch]
GO
ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_Currency] FOREIGN KEY([Currency_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_Currency]
GO
ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_Department]
GO
ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_FiscalYear]
GO
ALTER TABLE [AMS].[PROV_CB_Master]  WITH CHECK ADD  CONSTRAINT [FK_PROV_CB_Master_GeneralLedger] FOREIGN KEY([Ledger_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PROV_CB_Master] CHECK CONSTRAINT [FK_PROV_CB_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_Department]
GO
ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_GeneralLedger]
GO
ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_JuniorAgent] FOREIGN KEY([AgentId])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_JuniorAgent]
GO
ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_ProvCB_Master] FOREIGN KEY([VoucherNumber])
REFERENCES [AMS].[ProvCB_Master] ([VoucherNumber])
GO
ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_ProvCB_Master]
GO
ALTER TABLE [AMS].[ProvCB_Details]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Details_Subledger] FOREIGN KEY([SubledgerId])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[ProvCB_Details] CHECK CONSTRAINT [FK_ProvCB_Details_Subledger]
GO
ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_Branch]
GO
ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_CompanyUnit] FOREIGN KEY([CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_Currency] FOREIGN KEY([CurrId])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_Currency]
GO
ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_Department]
GO
ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_FiscalYear]
GO
ALTER TABLE [AMS].[ProvCB_Master]  WITH CHECK ADD  CONSTRAINT [FK_ProvCB_Master_GeneralLedger] FOREIGN KEY([LedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[ProvCB_Master] CHECK CONSTRAINT [FK_ProvCB_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[PRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PRT_Term_Branch] FOREIGN KEY([PT_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[PRT_Term] CHECK CONSTRAINT [FK_PRT_Term_Branch]
GO
ALTER TABLE [AMS].[PRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PRT_Term_CompanyUnit] FOREIGN KEY([PT_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[PRT_Term] CHECK CONSTRAINT [FK_PRT_Term_CompanyUnit]
GO
ALTER TABLE [AMS].[PRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_PRT_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PRT_Term] CHECK CONSTRAINT [FK_PRT_Term_GeneralLedger]
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
ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_GeneralLedger] FOREIGN KEY([PBLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_GeneralLedger]
GO
ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_GeneralLedger1] FOREIGN KEY([PRLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_GeneralLedger1]
GO
ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term] FOREIGN KEY([PBVatTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term]
GO
ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term1] FOREIGN KEY([PBDiscountTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term1]
GO
ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term2] FOREIGN KEY([PBProductDiscountTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term2]
GO
ALTER TABLE [AMS].[PurchaseSetting]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseSetting_PT_Term3] FOREIGN KEY([PBAdditionalTerm])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[PurchaseSetting] CHECK CONSTRAINT [FK_PurchaseSetting_PT_Term3]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_GeneralLedger] FOREIGN KEY([SBLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_GeneralLedger]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_GeneralLedger1] FOREIGN KEY([SRLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_GeneralLedger1]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term] FOREIGN KEY([SBVatTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term1] FOREIGN KEY([SBDiscountTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term1]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term2] FOREIGN KEY([SBAdditionalTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term2]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term3] FOREIGN KEY([SBProductDiscountTerm])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term3]
GO
ALTER TABLE [AMS].[SalesSetting]  WITH CHECK ADD  CONSTRAINT [FK_SalesSetting_ST_Term4] FOREIGN KEY([SBServiceCharge])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SalesSetting] CHECK CONSTRAINT [FK_SalesSetting_ST_Term4]
GO
ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_CostCenter] FOREIGN KEY([CC_Id])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_CostCenter]
GO
ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_Godown]
GO
ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_Product]
GO
ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_ProductUnit] FOREIGN KEY([AltUnit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_ProductUnit]
GO
ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_ProductUnit1] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[SampleCosting_Details]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Details_SampleCosting_Master] FOREIGN KEY([Costing_No])
REFERENCES [AMS].[SampleCosting_Master] ([Costing_No])
GO
ALTER TABLE [AMS].[SampleCosting_Details] CHECK CONSTRAINT [FK_SampleCosting_Details_SampleCosting_Master]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Branch]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_CostCenter] FOREIGN KEY([CC_Id])
REFERENCES [AMS].[CostCenter] ([CCId])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_CostCenter]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Department]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Godown]
GO
ALTER TABLE [AMS].[SampleCosting_Master]  WITH CHECK ADD  CONSTRAINT [FK_SampleCosting_Master_Product] FOREIGN KEY([FGProduct_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SampleCosting_Master] CHECK CONSTRAINT [FK_SampleCosting_Master_Product]
GO
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
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
ALTER TABLE [AMS].[SB_Details]  WITH CHECK ADD  CONSTRAINT [FK_SB_Details_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])
GO
ALTER TABLE [AMS].[SB_Details] CHECK CONSTRAINT [FK_SB_Details_SB_Master]
GO
ALTER TABLE [AMS].[SB_ExchangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_ExchangeDetails_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SB_ExchangeDetails] CHECK CONSTRAINT [FK_SB_ExchangeDetails_Godown]
GO
ALTER TABLE [AMS].[SB_ExchangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_ExchangeDetails_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SB_ExchangeDetails] CHECK CONSTRAINT [FK_SB_ExchangeDetails_Product]
GO
ALTER TABLE [AMS].[SB_ExchangeDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_ExchangeDetails_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])
GO
ALTER TABLE [AMS].[SB_ExchangeDetails] CHECK CONSTRAINT [FK_SB_ExchangeDetails_SB_Master]
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
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SB_Master] CHECK CONSTRAINT [FK_SB_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SB_Master]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
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
ALTER TABLE [AMS].[SB_Master_OtherDetails]  WITH CHECK ADD  CONSTRAINT [FK_SB_Master_OtherDetails_SB_Master] FOREIGN KEY([SB_Invoice])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])
GO
ALTER TABLE [AMS].[SB_Master_OtherDetails] CHECK CONSTRAINT [FK_SB_Master_OtherDetails_SB_Master]
GO
ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_Product]
GO
ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_SB_Master] FOREIGN KEY([SB_VNo])
REFERENCES [AMS].[SB_Master] ([SB_Invoice])
GO
ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_SB_Master]
GO
ALTER TABLE [AMS].[SB_Term]  WITH CHECK ADD  CONSTRAINT [FK_SB_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SB_Term] CHECK CONSTRAINT [FK_SB_Term_ST_Term]
GO
ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_Product]
GO
ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_ProductGroup] FOREIGN KEY([PGrp_Id])
REFERENCES [AMS].[ProductGroup] ([PGrpID])
GO
ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_ProductGroup]
GO
ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_SBT_Master] FOREIGN KEY([SBT_Invoice])
REFERENCES [AMS].[SBT_Master] ([SBT_Invoice])
GO
ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_SBT_Master]
GO
ALTER TABLE [AMS].[SBT_Details]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Details_Subledger] FOREIGN KEY([Slb_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[SBT_Details] CHECK CONSTRAINT [FK_SBT_Details_Subledger]
GO
ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_Branch]
GO
ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_Currency]
GO
ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SBT_Master]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SBT_Master] CHECK CONSTRAINT [FK_SBT_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[SBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SBT_Term] CHECK CONSTRAINT [FK_SBT_Term_Product]
GO
ALTER TABLE [AMS].[SBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Term_SBT_Master] FOREIGN KEY([SBT_VNo])
REFERENCES [AMS].[SBT_Master] ([SBT_Invoice])
GO
ALTER TABLE [AMS].[SBT_Term] CHECK CONSTRAINT [FK_SBT_Term_SBT_Master]
GO
ALTER TABLE [AMS].[SBT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SBT_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SBT_Term] CHECK CONSTRAINT [FK_SBT_Term_ST_Term]
GO
ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_Godown]
GO
ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_Product]
GO
ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_ProductUnit]
GO
ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_ProductUnit1] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[SC_Details]  WITH CHECK ADD  CONSTRAINT [FK_SC_Details_SC_Master] FOREIGN KEY([SC_Invoice])
REFERENCES [AMS].[SC_Master] ([SC_Invoice])
GO
ALTER TABLE [AMS].[SC_Details] CHECK CONSTRAINT [FK_SC_Details_SC_Master]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Branch]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Counter]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Currency]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Department]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[SC_Master]  WITH CHECK ADD  CONSTRAINT [FK_SC_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[SC_Master] CHECK CONSTRAINT [FK_SC_Master_Subledger]
GO
ALTER TABLE [AMS].[SC_Term]  WITH CHECK ADD  CONSTRAINT [FK_SC_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SC_Term] CHECK CONSTRAINT [FK_SC_Term_Product]
GO
ALTER TABLE [AMS].[SC_Term]  WITH CHECK ADD  CONSTRAINT [FK_SC_Term_SC_Master] FOREIGN KEY([SC_Vno])
REFERENCES [AMS].[SC_Master] ([SC_Invoice])
GO
ALTER TABLE [AMS].[SC_Term] CHECK CONSTRAINT [FK_SC_Term_SC_Master]
GO
ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_Product] FOREIGN KEY([P_Code])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_Product]
GO
ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_ProductGroup] FOREIGN KEY([P_Group])
REFERENCES [AMS].[ProductGroup] ([PGrpID])
GO
ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_ProductGroup]
GO
ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_ProductSubGroup] FOREIGN KEY([P_SubGroup])
REFERENCES [AMS].[ProductSubGroup] ([PSubGrpId])
GO
ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_ProductSubGroup]
GO
ALTER TABLE [AMS].[Scheme_Details]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Details_Scheme_Master] FOREIGN KEY([Scheme_Id])
REFERENCES [AMS].[Scheme_Master] ([Scheme_Id])
GO
ALTER TABLE [AMS].[Scheme_Details] CHECK CONSTRAINT [FK_Scheme_Details_Scheme_Master]
GO
ALTER TABLE [AMS].[Scheme_Master]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[Scheme_Master] CHECK CONSTRAINT [FK_Scheme_Master_Branch]
GO
ALTER TABLE [AMS].[Scheme_Master]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[Scheme_Master] CHECK CONSTRAINT [FK_Scheme_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[Scheme_Master]  WITH CHECK ADD  CONSTRAINT [FK_Scheme_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[Scheme_Master] CHECK CONSTRAINT [FK_Scheme_Master_FiscalYear]
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
ALTER TABLE [AMS].[SeniorAgent]  WITH CHECK ADD  CONSTRAINT [FK_SeniorAgent_GeneralLedger] FOREIGN KEY([GLID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SeniorAgent] CHECK CONSTRAINT [FK_SeniorAgent_GeneralLedger]
GO
ALTER TABLE [AMS].[SO_Details]  WITH CHECK ADD  CONSTRAINT [FK_SO_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SO_Details] CHECK CONSTRAINT [FK_SO_Details_Godown]
GO
ALTER TABLE [AMS].[SO_Details]  WITH CHECK ADD  CONSTRAINT [FK_SO_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SO_Details] CHECK CONSTRAINT [FK_SO_Details_Product]
GO
ALTER TABLE [AMS].[SO_Details]  WITH CHECK ADD  CONSTRAINT [FK_SO_Details_SO_Master] FOREIGN KEY([SO_Invoice])
REFERENCES [AMS].[SO_Master] ([SO_Invoice])
GO
ALTER TABLE [AMS].[SO_Details] CHECK CONSTRAINT [FK_SO_Details_SO_Master]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Branch]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Counter]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Currency]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [HOS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Department]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_Subledger]
GO
ALTER TABLE [AMS].[SO_Master]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_TableMaster] FOREIGN KEY([TableId])
REFERENCES [AMS].[TableMaster] ([TableId])
GO
ALTER TABLE [AMS].[SO_Master] CHECK CONSTRAINT [FK_SO_Master_TableMaster]
GO
ALTER TABLE [AMS].[SO_Master_OtherDetails]  WITH CHECK ADD  CONSTRAINT [FK_SO_Master_OtherDetails_SO_Master] FOREIGN KEY([SO_Invoice])
REFERENCES [AMS].[SO_Master] ([SO_Invoice])
GO
ALTER TABLE [AMS].[SO_Master_OtherDetails] CHECK CONSTRAINT [FK_SO_Master_OtherDetails_SO_Master]
GO
ALTER TABLE [AMS].[SO_Term]  WITH CHECK ADD  CONSTRAINT [FK_SO_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SO_Term] CHECK CONSTRAINT [FK_SO_Term_Product]
GO
ALTER TABLE [AMS].[SO_Term]  WITH CHECK ADD  CONSTRAINT [FK_SO_Term_SO_Master] FOREIGN KEY([SO_Vno])
REFERENCES [AMS].[SO_Master] ([SO_Invoice])
GO
ALTER TABLE [AMS].[SO_Term] CHECK CONSTRAINT [FK_SO_Term_SO_Master]
GO
ALTER TABLE [AMS].[SO_Term]  WITH CHECK ADD  CONSTRAINT [FK_SO_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SO_Term] CHECK CONSTRAINT [FK_SO_Term_ST_Term]
GO
ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_Godown]
GO
ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_Product]
GO
ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_ProductAltUnit] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_ProductAltUnit]
GO
ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_ProductUnit]
GO
ALTER TABLE [AMS].[SQ_Details]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Details_SQ_Master] FOREIGN KEY([SQ_Invoice])
REFERENCES [AMS].[SQ_Master] ([SQ_Invoice])
GO
ALTER TABLE [AMS].[SQ_Details] CHECK CONSTRAINT [FK_SQ_Details_SQ_Master]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Branch]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Currency]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [HOS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Department]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_GeneralLedger] FOREIGN KEY([Customer_Id])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[SQ_Master]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[SQ_Master] CHECK CONSTRAINT [FK_SQ_Master_Subledger]
GO
ALTER TABLE [AMS].[SQ_Term]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SQ_Term] CHECK CONSTRAINT [FK_SQ_Term_Product]
GO
ALTER TABLE [AMS].[SQ_Term]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Term_SQ_Master] FOREIGN KEY([SQ_Vno])
REFERENCES [AMS].[SQ_Master] ([SQ_Invoice])
GO
ALTER TABLE [AMS].[SQ_Term] CHECK CONSTRAINT [FK_SQ_Term_SQ_Master]
GO
ALTER TABLE [AMS].[SQ_Term]  WITH CHECK ADD  CONSTRAINT [FK_SQ_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SQ_Term] CHECK CONSTRAINT [FK_SQ_Term_ST_Term]
GO
ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_Godown] FOREIGN KEY([Gdn_Id])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_Godown]
GO
ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_Product] FOREIGN KEY([P_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_Product]
GO
ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_ProductUnit]
GO
ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_ProductUnit1] FOREIGN KEY([Alt_UnitId])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_ProductUnit1]
GO
ALTER TABLE [AMS].[SR_Details]  WITH CHECK ADD  CONSTRAINT [FK_SR_Details_SR_Master] FOREIGN KEY([SR_Invoice])
REFERENCES [AMS].[SR_Master] ([SR_Invoice])
GO
ALTER TABLE [AMS].[SR_Details] CHECK CONSTRAINT [FK_SR_Details_SR_Master]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Branch]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Counter] FOREIGN KEY([CounterId])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Counter]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Currency]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Department] FOREIGN KEY([Cls1])
REFERENCES [AMS].[Department] ([DId])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Department]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_FiscalYear]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_GeneralLedger] FOREIGN KEY([Customer_ID])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_GeneralLedger]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_JuniorAgent] FOREIGN KEY([Agent_Id])
REFERENCES [AMS].[JuniorAgent] ([AgentId])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_JuniorAgent]
GO
ALTER TABLE [AMS].[SR_Master]  WITH CHECK ADD  CONSTRAINT [FK_SR_Master_Subledger] FOREIGN KEY([Subledger_Id])
REFERENCES [AMS].[Subledger] ([SLId])
GO
ALTER TABLE [AMS].[SR_Master] CHECK CONSTRAINT [FK_SR_Master_Subledger]
GO
ALTER TABLE [AMS].[SR_Term]  WITH CHECK ADD  CONSTRAINT [FK_SR_Term_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[SR_Term] CHECK CONSTRAINT [FK_SR_Term_Product]
GO
ALTER TABLE [AMS].[SR_Term]  WITH CHECK ADD  CONSTRAINT [FK_SR_Term_SR_Master] FOREIGN KEY([SR_VNo])
REFERENCES [AMS].[SR_Master] ([SR_Invoice])
GO
ALTER TABLE [AMS].[SR_Term] CHECK CONSTRAINT [FK_SR_Term_SR_Master]
GO
ALTER TABLE [AMS].[SR_Term]  WITH CHECK ADD  CONSTRAINT [FK_SR_Term_ST_Term] FOREIGN KEY([ST_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SR_Term] CHECK CONSTRAINT [FK_SR_Term_ST_Term]
GO
ALTER TABLE [AMS].[SRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SRT_Term_Branch] FOREIGN KEY([ST_Branch])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[SRT_Term] CHECK CONSTRAINT [FK_SRT_Term_Branch]
GO
ALTER TABLE [AMS].[SRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SRT_Term_CompanyUnit] FOREIGN KEY([ST_CompanyUnit])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[SRT_Term] CHECK CONSTRAINT [FK_SRT_Term_CompanyUnit]
GO
ALTER TABLE [AMS].[SRT_Term]  WITH CHECK ADD  CONSTRAINT [FK_SRT_Term_GeneralLedger] FOREIGN KEY([Ledger])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SRT_Term] CHECK CONSTRAINT [FK_SRT_Term_GeneralLedger]
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
ALTER TABLE [AMS].[STA_Details]  WITH CHECK ADD  CONSTRAINT [FK_STA_Details_Godown] FOREIGN KEY([GodownId])
REFERENCES [AMS].[Godown] ([GID])
GO
ALTER TABLE [AMS].[STA_Details] CHECK CONSTRAINT [FK_STA_Details_Godown]
GO
ALTER TABLE [AMS].[STA_Details]  WITH CHECK ADD  CONSTRAINT [FK_STA_Details_Product] FOREIGN KEY([ProductId])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[STA_Details] CHECK CONSTRAINT [FK_STA_Details_Product]
GO
ALTER TABLE [AMS].[STA_Details]  WITH CHECK ADD  CONSTRAINT [FK_STA_Details_STA_Master] FOREIGN KEY([StockAdjust_No])
REFERENCES [AMS].[STA_Master] ([StockAdjust_No])
GO
ALTER TABLE [AMS].[STA_Details] CHECK CONSTRAINT [FK_STA_Details_STA_Master]
GO
ALTER TABLE [AMS].[STA_Master]  WITH CHECK ADD  CONSTRAINT [FK_STA_Master_Branch] FOREIGN KEY([BranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[STA_Master] CHECK CONSTRAINT [FK_STA_Master_Branch]
GO
ALTER TABLE [AMS].[STA_Master]  WITH CHECK ADD  CONSTRAINT [FK_STA_Master_CompanyUnit] FOREIGN KEY([CompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[STA_Master] CHECK CONSTRAINT [FK_STA_Master_CompanyUnit]
GO
ALTER TABLE [AMS].[STA_Master]  WITH CHECK ADD  CONSTRAINT [FK_STA_Master_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[STA_Master] CHECK CONSTRAINT [FK_STA_Master_FiscalYear]
GO
ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_Branch] FOREIGN KEY([CBranch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_Branch]
GO
ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_CompanyUnit] FOREIGN KEY([CUnit_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_CompanyUnit]
GO
ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_FiscalYear]
GO
ALTER TABLE [AMS].[StockBatchDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockBatchDetails_Product] FOREIGN KEY([Product_Id])
REFERENCES [AMS].[Product] ([PID])
GO
ALTER TABLE [AMS].[StockBatchDetails] CHECK CONSTRAINT [FK_StockBatchDetails_Product]
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
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_Counter] FOREIGN KEY([Counter_Id])
REFERENCES [AMS].[Counter] ([CId])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_Counter]
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
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_FiscalYear] FOREIGN KEY([FiscalYearId])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_FiscalYear]
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
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_ProductAltUnit] FOREIGN KEY([AltUnit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_ProductAltUnit]
GO
ALTER TABLE [AMS].[StockDetails]  WITH CHECK ADD  CONSTRAINT [FK_StockDetails_ProductUnit] FOREIGN KEY([Unit_Id])
REFERENCES [AMS].[ProductUnit] ([UID])
GO
ALTER TABLE [AMS].[StockDetails] CHECK CONSTRAINT [FK_StockDetails_ProductUnit]
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
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_CashLedger] FOREIGN KEY([Cash_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_CashLedger]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_ClosingStockBS] FOREIGN KEY([ClosingStockBS_Ac])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_ClosingStockBS]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_ClosingStockPL] FOREIGN KEY([ClosingStockPL_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_ClosingStockPL]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Currency] FOREIGN KEY([Cur_Id])
REFERENCES [AMS].[Currency] ([CId])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Currency]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_FiscalYear] FOREIGN KEY([FY_Id])
REFERENCES [AMS].[FiscalYear] ([FY_Id])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_FiscalYear]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_OpeningStock] FOREIGN KEY([OpeningStockPL_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_OpeningStock]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PDCBankLedger] FOREIGN KEY([PDCBank_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PDCBankLedger]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_ProfitLoss] FOREIGN KEY([PL_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_ProfitLoss]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurAddVat] FOREIGN KEY([PurchaseAddVat_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurAddVat]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Purchase] FOREIGN KEY([Purchase_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Purchase]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurchaseReturn] FOREIGN KEY([PurchaseReturn_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurchaseReturn]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurDiscount] FOREIGN KEY([PurchaseDiscount_ID])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurDiscount]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurProDiscount] FOREIGN KEY([PurchaseProDiscount_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurProDiscount]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_PurVat] FOREIGN KEY([PurchaseVat_Id])
REFERENCES [AMS].[PT_Term] ([PT_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_PurVat]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_Sales] FOREIGN KEY([Sales_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_Sales]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesDiscount] FOREIGN KEY([SalesDiscount_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesDiscount]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesReturn] FOREIGN KEY([SalesReturn_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesReturn]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesServiceCharge] FOREIGN KEY([SalesServiceCharge_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesServiceCharge]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesSpecialDiscount] FOREIGN KEY([SalesSpecialDiscount_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesSpecialDiscount]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_SalesVat] FOREIGN KEY([SalesVat_Id])
REFERENCES [AMS].[ST_Term] ([ST_ID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_SalesVat]
GO
ALTER TABLE [AMS].[SystemConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_SystemConfiguration_VatLedger] FOREIGN KEY([Vat_AC])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemConfiguration] CHECK CONSTRAINT [FK_SystemConfiguration_VatLedger]
GO
ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_AccountGroup] FOREIGN KEY([DebtorsGroupId])
REFERENCES [AMS].[AccountGroup] ([GrpId])
GO
ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_AccountGroup]
GO
ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_AccountGroup1] FOREIGN KEY([CreditorGroupId])
REFERENCES [AMS].[AccountGroup] ([GrpId])
GO
ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_AccountGroup1]
GO
ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_GeneralLedger] FOREIGN KEY([PFLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_GeneralLedger]
GO
ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_GeneralLedger1] FOREIGN KEY([SalaryLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_GeneralLedger1]
GO
ALTER TABLE [AMS].[SystemSetting]  WITH CHECK ADD  CONSTRAINT [FK_SystemSetting_GeneralLedger2] FOREIGN KEY([TDSLedgerId])
REFERENCES [AMS].[GeneralLedger] ([GLID])
GO
ALTER TABLE [AMS].[SystemSetting] CHECK CONSTRAINT [FK_SystemSetting_GeneralLedger2]
GO
ALTER TABLE [AMS].[TableMaster]  WITH CHECK ADD  CONSTRAINT [FK_TableMaster_Branch] FOREIGN KEY([Branch_Id])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[TableMaster] CHECK CONSTRAINT [FK_TableMaster_Branch]
GO
ALTER TABLE [AMS].[TableMaster]  WITH CHECK ADD  CONSTRAINT [FK_TableMaster_CompanyUnit] FOREIGN KEY([Company_Id])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[TableMaster] CHECK CONSTRAINT [FK_TableMaster_CompanyUnit]
GO
ALTER TABLE [AMS].[TicketRefund]  WITH CHECK ADD  CONSTRAINT [FK_TicketRefund_TicketRefund] FOREIGN KEY([VoucherNo])
REFERENCES [AMS].[TicketRefund] ([VoucherNo])
GO
ALTER TABLE [AMS].[TicketRefund] CHECK CONSTRAINT [FK_TicketRefund_TicketRefund]
GO
ALTER TABLE [AMS].[VehicleColors]  WITH CHECK ADD  CONSTRAINT [FK_VechileColors_Branch] FOREIGN KEY([VHColorsBranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[VehicleColors] CHECK CONSTRAINT [FK_VechileColors_Branch]
GO
ALTER TABLE [AMS].[VehicleColors]  WITH CHECK ADD  CONSTRAINT [FK_VechileColors_CompanyUnit] FOREIGN KEY([VHColorsCompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[VehicleColors] CHECK CONSTRAINT [FK_VechileColors_CompanyUnit]
GO
ALTER TABLE [AMS].[VehicleNumber]  WITH CHECK ADD  CONSTRAINT [FK_VechileNumber_Branch] FOREIGN KEY([VNBranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[VehicleNumber] CHECK CONSTRAINT [FK_VechileNumber_Branch]
GO
ALTER TABLE [AMS].[VehicleNumber]  WITH CHECK ADD  CONSTRAINT [FK_VechileNumber_CompanyUnit] FOREIGN KEY([VNCompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[VehicleNumber] CHECK CONSTRAINT [FK_VechileNumber_CompanyUnit]
GO
ALTER TABLE [AMS].[VehileModel]  WITH CHECK ADD  CONSTRAINT [FK_VehileModel_Branch] FOREIGN KEY([VHModelBranchId])
REFERENCES [AMS].[Branch] ([Branch_Id])
GO
ALTER TABLE [AMS].[VehileModel] CHECK CONSTRAINT [FK_VehileModel_Branch]
GO
ALTER TABLE [AMS].[VehileModel]  WITH CHECK ADD  CONSTRAINT [FK_VehileModel_CompanyUnit] FOREIGN KEY([VHModelCompanyUnitId])
REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
GO
ALTER TABLE [AMS].[VehileModel] CHECK CONSTRAINT [FK_VehileModel_CompanyUnit]
GO
/****** Object:  StoredProcedure [AMS].[Usp_IUD_Branch]    Script Date: 06/08/2020 10:12:01 PM ******/
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
						set @Branch_Id = (Select Isnull(Max(Branch_Id),0)+1 Branch_Id from AMS.Branch)
						Insert Into AMS.Branch(Branch_Id,Branch_Name, Branch_Code,  Reg_Date,  Address, Country,  State,  City,PhoneNo, Fax,Email, ContactPerson, ContactPersonAdd,     ContactPersonPhone    )
						values(@Branch_Id, @Branch_Name, @Branch_Code,  @Reg_Date, @Address,  @Country,  @State,@City, @PhoneNo, @Fax,@Email, @ContactPerson, @ContactPersonAdd,	@ContactPersonPhone  )
						SET @Msg='Record Inserted Successfully !'
						Set @Return_Id=@Branch_Id--@@IDENTITY
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
					END CATCH;
GO
/****** Object:  StoredProcedure [AMS].[Usp_IUD_CompanyInfo]    Script Date: 06/08/2020 10:12:01 PM ******/
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
				update AMS.CompanyInfo SET Company_Logo=@Company_Logo WHERE Company_Id=@Company_Id
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
				END CATCH;
GO
/****** Object:  StoredProcedure [AMS].[USP_PostStockValue]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [AMS].[USP_PostStockValue]
				@PCode VARCHAR(MAX) = ''  AS BEGIN
				Update AMS.StockDetails Set StockVal = (MonthRate.Rate * (Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) + Isnull(AMS.StockDetails.ExtraStockFreeQty,0))) from (
				Select PID,Min(AD_Date) as StDate,Max(AD_Date) as EnDate,NMonth,PCR.Rate from (
				Select Case When Substring(Month_Miti,1,3) = 'Bai' then '01' + Replace(Month_Miti,'Bai','') When Substring(Month_Miti,1,3) = 'Jes' then '02' + Replace(Month_Miti,'Jes','')
				When Substring(Month_Miti,1,3) = 'Ash' then '03' + Replace(Month_Miti,'Ash','') When Substring(Month_Miti,1,3) = 'Shr' then '04' + Replace(Month_Miti,'Shr','')
				When Substring(Month_Miti,1,3) = 'Bha' then '05' + Replace(Month_Miti,'Bha','') When Substring(Month_Miti,1,3) = 'Aas' then '06' + Replace(Month_Miti,'Aas','')
				When Substring(Month_Miti,1,3) = 'Kar' then '07' + Replace(Month_Miti,'Kar','') When Substring(Month_Miti,1,3) = 'Mag' then '08' + Replace(Month_Miti,'Mag','')
				When Substring(Month_Miti,1,3) = 'Pou' then '09' + Replace(Month_Miti,'Pou','') When Substring(Month_Miti,1,3) = 'Mar' then '10' + Replace(Month_Miti,'Mar','')
				When Substring(Month_Miti,1,3) = 'Fal' then '11' + Replace(Month_Miti,'Fal','') When Substring(Month_Miti,1,3) = 'Cha' then '12' + Replace(Month_Miti,'Cha','')
				End as NMonth,Product.PID,PCR.Rate from AMS.ProductClosingRate as PCR Inner Join AMS.Product on PCR.Product_Id = Product.PID
				where  PValTech in ('M','R') and (@PCode = '' or Product.PID in (Select Value from AMS.fn_Splitstring(@PCode,',')))
				) as PCR
				Inner join AMS.DateMiti on PCR.NMonth like Substring(DateMiti.BS_DateDMY,4,7)  Group by NMonth,PID,Rate
				) as MonthRate  Where AMS.StockDetails.Product_Id = MonthRate.PID
				and AMS.StockDetails.Voucher_Date between MonthRate.StDate and MonthRate.EnDate

				DECLARE @IsGodownWise BIT SET @IsGodownWise = 0  SELECT  IDENTITY(INT,1,1 ) AS Id,Product_Id AS PCode, ISNULL(CONVERT(nvarchar(100),StockDetails.Branch_Id), 'No Branch') AS Br_Code ,
				(CASE WHEN @IsGodownWise = 1 THEN ISNULL(CONVERT(nvarchar(100),Godown_Id), 'No Godown') ELSE 'No Godown' END) AS Gdn_Code ,
				CONVERT(nvarchar(100),Product_Id) + '_' + ISNULL(CONVERT(nvarchar(100),StockDetails.Branch_Id), 'No Branch') + '_' + (CASE WHEN @IsGodownWise =1 THEN ISNULL(CONVERT(nvarchar(100),Godown_Id), 'No Godown') ELSE 'No Godown' END) AS PBG_Code,
				Product.PValTech
				INTO #Product FROM AMS.StockDetails
				INNER JOIN AMS.Product ON AMS.StockDetails.Product_Id = Product.PID  WHERE (Product_Id IN (SELECT Value FROM AMS.fn_Splitstring(@PCode, ',')) OR @PCode IS NULL OR @PCode = '')
				AND PValTech IN ('FIFO', 'X', 'W', 'R')  GROUP BY Product_Id,StockDetails.Branch_Id,(CASE WHEN @IsGodownWise = 1 THEN ISNULL(CONVERT(nvarchar(100),Godown_Id), 'No Godown') ELSE 'No Godown' END),
				Product.PValTech  ORDER BY Product_Id,Br_Code,(CASE WHEN @IsGodownWise = 1 THEN ISNULL(CONVERT(nvarchar(100),Godown_Id), 'No Godown') ELSE 'No Godown' END)
				CREATE INDEX #Product_Index_PBG_Code
				ON #Product(PBG_Code)  IF @IsGodownWise = 1
				begin
				UPDATE AMS.StockDetails SET StockVal = 0  WHERE EXISTS (SELECT * FROM #Product AS P WHERE AMS.StockDetails.Product_Id = P.PCode
				AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Branch_Id),'No Branch') = P.Br_Code  AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Godown_Id),'No Godown') = P.Gdn_Code )  AND EntryType = 'O'
				End
				Else
				BEGIN
				UPDATE AMS.StockDetails SET StockVal = 0  WHERE EXISTS (SELECT * FROM #Product AS P WHERE AMS.StockDetails.Product_Id = P.PCode
				AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Branch_Id),'No Branch') = p.Br_Code)  AND EntryType = 'O' and (Module <> 'ST' and Module <>'SII')
				End
				DECLARE @PBGCode VARCHAR(100)
				CREATE TABLE #StockTransIN  (Id INT IDENTITY(1, 1)PRIMARY KEY,V_No VARCHAR(30),V_Date DATETIME,V_Time DATETIME,Source VARCHAR(30),Sno INT,PBG_Code VARCHAR(100),Qty DECIMAL(18, 6),Value DECIMAL(18, 6),RedimQty DECIMAL(18, 6))
				CREATE INDEX #StockTransIN_Index_PBG_Code ON #StockTransIN(PBG_Code)
				CREATE TABLE #StockTransOUT(Id INT IDENTITY(1, 1) PRIMARY KEY,V_No VARCHAR(30),V_Date DATETIME,V_Time DATETIME,V_Miti VARCHAR(10),Source VARCHAR(30),Sno INT,PBG_Code VARCHAR(100),Qty DECIMAL(18, 6),AdjustQty DECIMAL(18, 6),StockVal DECIMAL(18, 6))
				CREATE INDEX #StockTransOUT_Index_PBG_Code ON #StockTransOUT(PBG_Code)
				CREATE TABLE #Stock_Adjustment(Id INT IDENTITY(1, 1),In_Id INT,RemainQty DECIMAL(18, 6),Out_Id INT,Qty DECIMAL(18, 6))  IF @IsGodownWise = 1
				begin
				INSERT INTO #StockTransIN (V_No,V_Date,V_Time,Source,Sno,PBG_Code,Qty,Value,RedimQty)
				SELECT  Voucher_No,Voucher_Date,Voucher_Time,Module,Serial_No, CONVERT(nvarchar(100),Product_Id) + '_' + ISNULL(CONVERT(nvarchar(100),Branch_Id),'No Branch') + '_' + ISNULL(CONVERT(nvarchar(100),Godown_Id), 'No Godown'),
				Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) + Isnull(AMS.StockDetails.ExtraStockFreeQty,0) as StockQty,
				(CASE WHEN StockQty > 0 THEN StockVal / (Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) + Isnull(AMS.StockDetails.ExtraStockFreeQty,0))
				ELSE 0 END ),0  From AMS.StockDetails
				WHERE EXISTS (SELECT * FROM #Product AS P  Where AMS.StockDetails.Product_Id = P.PCode  AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Branch_Id),'No Branch') = p.Br_Code
				AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Godown_Id),'No Godown') = p.Gdn_Code)  AND EntryType = 'I'  ORDER BY Voucher_Date,EntryType,Voucher_Time,Voucher_No
				INSERT INTO #StockTransOUT(V_No,Source,V_Date,V_Time,V_Miti,Sno,PBG_Code,Qty,AdjustQty,StockVal)
				SELECT  Voucher_No,Module,Voucher_Date,Voucher_Time,Voucher_Miti, Serial_No,CONVERT(nvarchar(100),Product_Id) + '_' + ISNULL(Branch_Id,'No Branch') + '_' + ISNULL(Godown_Id, 'No Godown'),
				Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) + Isnull(AMS.StockDetails.ExtraStockFreeQty,0) as StockQty,0,0
				From AMS.StockDetails WHERE EXISTS (SELECT * FROM #Product AS P WHERE AMS.StockDetails.Product_Id = P.PCode  AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Branch_Id),'No Branch') = P.Br_Code
				AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Godown_Id),'No Godown') = p.Gdn_Code)  AND EntryType = 'O'  ORDER BY Voucher_Date,EntryType,Voucher_Time,Voucher_No
				End
				Else
				BEGIN
				INSERT  INTO #StockTransIN(V_No,V_Date,V_Time,Source,Sno,PBG_Code,Qty,Value,RedimQty)
				SELECT Voucher_No,Voucher_Date,Voucher_Time,Module,Serial_no, CONVERT(nvarchar(100),Product_Id) + '_' + ISNULL(CONVERT(nvarchar(100),Branch_Id),'No Branch') + '_' + 'No Godown',
				Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) + Isnull(AMS.StockDetails.ExtraStockFreeQty,0) as StockQty,
				(CASE WHEN AMS.StockDetails.StockQty > 0 THEN StockVal/(Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) +
				Isnull(AMS.StockDetails.ExtraStockFreeQty,0)) ELSE 0 END),0  From AMS.StockDetails
				WHERE   EXISTS ( SELECT * FROM #Product AS P WHERE  AMS.StockDetails.Product_Id = P.PCode  AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Branch_Id),'No Branch') = P.Br_Code )
				AND EntryType = 'I' AND (Module <> 'ST' and Module <>'SII')  ORDER BY Voucher_Date,EntryType,Voucher_Time,Voucher_No
				INSERT INTO #StockTransOUT(V_No,Source,V_Date,V_Time,V_Miti,Sno,PBG_Code,Qty,AdjustQty,StockVal)
				SELECT Voucher_No,Module,Voucher_Date,Voucher_Time,Voucher_Miti,Serial_No,CONVERT(nvarchar(100),Product_Id) + '_' + ISNULL(CONVERT(nvarchar(100),Branch_Id),'No Branch') + '_' + 'No Godown',
				Isnull(AMS.StockDetails.StockQty,0) + Isnull(AMS.StockDetails.StockFreeQty,0) + Isnull(AMS.StockDetails.ExtraStockFreeQty,0) as StockQty,0,0
				From AMS.StockDetails  WHERE   EXISTS (SELECT * FROM #Product AS P  Where AMS.StockDetails.Product_Id = P.PCode
				AND ISNULL(CONVERT(nvarchar(100),AMS.StockDetails.Branch_Id),'No Branch') = P.Br_Code)  AND EntryType = 'O' AND (Module <> 'ST' and Module <>'SII')
				ORDER BY Voucher_Date,EntryType,Voucher_Time,Voucher_No
				End
				DECLARE @Id INT,@STINId INT, @OutId INT     SET @Id = 0 SET @STINId = 0 SET @OutId = 0  DECLARE @INQty DECIMAL(18, 6),@OUTQty DECIMAL(18, 6) , @Value DECIMAL(18, 6) ,@UnAdjustQty
				DECIMAL(18, 6)  DECLARE @OutDate DATETIME  WHILE EXISTS (SELECT * FROM #Product WHERE Id > @Id AND PValTech in ('FIFO','R'))
				BEGIN    set @Value=0  SELECT TOP 1 @Id = Id, @PBGCode = PBG_Code FROM #Product WHERE Id > @Id ORDER BY Id
				SET @STINId = 0  WHILE   EXISTS ( SELECT * FROM #StockTransOUT WHERE PBG_Code = @PBGCode AND Qty > AdjustQty )  AND EXISTS (
				SELECT * FROM   #StockTransIN WHERE  PBG_Code = @PBGCode AND Id > @STINId )  begin SELECT TOP 1 @STINId = Id,@INQty = Qty,@Value = Value FROM #StockTransIN
				WHERE   PBG_Code = @PBGCode AND Id > @STINId  ORDER BY Id  WHILE ( @INQty > 0)  AND EXISTS (SELECT * FROM #StockTransOUT WHERE  PBG_Code = @PBGCode AND Qty > AdjustQty)
				begin
				SELECT TOP 1 @OutId = Id,@UnAdjustQty = Qty - AdjustQty FROM #StockTransOUT WHERE   PBG_Code = @PBGCode AND Qty > AdjustQty  ORDER BY Id  IF ( @INQty > @UnAdjustQty )
				begin
				UPDATE  #StockTransOUT SET AdjustQty = Qty,StockVal = StockVal + (@Value * @UnAdjustQty)  WHERE Id = @OutId
				INSERT  INTO #Stock_Adjustment(In_Id,RemainQty,Out_Id,Qty)  VALUES (@STINId,@INQty,@OutId,@UnAdjustQty)  SET @INQty = @INQty - @UnAdjustQty   End  Else  begin
				UPDATE  #StockTransOUT  SET AdjustQty = AdjustQty + @INQty, StockVal = StockVal + (@Value * @INQty)  WHERE Id = @OutId
				INSERT INTO #Stock_Adjustment (In_Id,RemainQty,Out_Id,Qty)VALUES (@STINId,@INQty,@OutId,@INQty ) SET @INQty = 0  End  End  End
				Update #StockTransOUT Set StockVal = Isnull(StockVal,0) + ((Qty - AdjustQty) * @Value), AdjustQty = Qty Where Qty > AdjustQty and PBG_Code = @PBGCode  End
				WHILE EXISTS (SELECT * FROM #Product WHERE Id > @Id AND PValTech = 'X' )
				BEGIN SELECT TOP 1 @Id = Id,@PBGCode = PBG_Code FROM #Product WHERE Id > @Id
				ORDER BY Id  SET @OutId = 0 WHILE
				EXISTS (SELECT * FROM #StockTransIN WHERE PBG_Code = @PBGCode AND Qty > RedimQty )  AND EXISTS (SELECT * FROM #StockTransOUT WHERE PBG_Code = @PBGCode AND Id > @OutId )
				begin
				SELECT TOP 1 @OutId = Id,@OutDate = V_Date FROM #StockTransOUT  WHERE   PBG_Code = @PBGCode AND Id > @OutId ORDER BY Id
				SELECT  @Value = SUM(Value) / COUNT(*) FROM    #StockTransIN  WHERE   PBG_Code = @PBGCode AND V_Date <= @OutDate AND Qty > RedimQty
				SELECT @OUTQty = SUM(Qty) FROM #StockTransOUT  WHERE PBG_Code = @PBGCode AND V_Date = @OutDate AND StockVal > 0
				UPDATE #StockTransOUT SET StockVal = ( @Value * Qty ),AdjustQty = Qty  WHERE   PBG_Code = @PBGCode AND V_Date = @OutDate  WHILE ( @OUTQty > 0 )
				AND EXISTS (SELECT * FROM #StockTransIN WHERE PBG_Code = @PBGCode AND Qty > RedimQty )  begin  SELECT TOP 1 @STINId = Id,@UnAdjustQty = Qty - RedimQty FROM #StockTransIN
				WHERE   PBG_Code = @PBGCode AND Qty > RedimQty
				ORDER BY Id
				IF ( @OUTQty > @UnAdjustQty )
				BEGIN
				UPDATE  #StockTransIN SET RedimQty = Qty WHERE   Id = @STINId  SET @OUTQty = @OUTQty - @UnAdjustQty
				End
				Else
				begin
				UPDATE #StockTransIN SET RedimQty = RedimQty + @OUTQty WHERE   Id = @STINId  SET @OUTQty = 0
				End
				End
				End
				Update #StockTransOUT Set StockVal = Isnull(StockVal,0) + ((Qty - AdjustQty) * @Value), AdjustQty = Qty Where Qty > AdjustQty and PBG_Code = @PBGCode  End
				Update #StockTransOUT Set StockVal = (AverageValue * Qty) from  (Select PIn.PBG_Code, Sum(Value * Qty)/Sum(Qty) as AverageValue  from #StockTransIn as PIn
				Inner join #Product as P on PIn.PBG_Code = P.PBG_Code  Where P.PValTech = 'W' Group By PIn.PBG_Code) as InQty  Where #StockTransOUT.PBG_Code = InQty.PBG_Code

				Update AMS.StockBatchDetails Set StockVal = (Isnull(StockQty,0) + Isnull(FreeStockQty,0) + Isnull(StockExtraFreeQty,0)) * OutVal.StockVal from (
				SELECT Product_Id,STPD.SNo,  SUM(StockVal)/Sum(Isnull(StockQty,0) + Isnull(FreeStockQty,0) + Isnull(StockExtraFreeQty,0)) as StockVal
				from AMS.StockBatchDetails as STPD Inner join AMS.Product on Product_Id = Product.PID Where Product.PBatchwise = 1 and Transaction_Type = 'I'
				and (Module <> 'ST' and Module <>'SII')  Group by Product_Id,STPD.SNo  ) as OutVal  Where StockBatchDetails.Product_Id = OutVal.Product_Id
				and StockBatchDetails.SNo = OutVal.SNo  and Transaction_Type = 'O'  and (Module <> 'ST' and Module <>'SII')

				Update AMS.StockDetails Set StockVal = Batch.StockVal  from (Select Voucher_No,SNo,Module,Sum(StockVal) as StockVal from AMS.StockBatchDetails
				Inner join AMS.Product on Product_Id = Product.PID  Where Product.PBatchwise = 1 and Transaction_Type = 'O'  Group by Voucher_No,SNo,Module) as Batch
				Where StockDetails.Voucher_No = Batch.Voucher_No  and StockDetails.Serial_No = Batch.SNo and StockDetails.Module = Batch.Module  and StockDetails.EntryType = 'O'

				Update AMS.StockDetails Set StockVal = (Isnull(StockQty,0) + Isnull(StockFreeQty,0) + Isnull(ExtraStockFreeQty,0)) * OutVal.StockVal  from (
				Select Product_Id,Sum(StockVal) / Sum(Isnull(StockQty,0) + Isnull(StockFreeQty,0) + Isnull(ExtraStockFreeQty,0)) as StockVal  from AMS.StockDetails as ST
				Inner join AMS.Product on Product_Id = Product.PID  Where Product.PBatchwise = 1  and not exists (Select * from AMS.StockBatchDetails as STPD
				Where ST.Voucher_No = STPD.Voucher_No and ST.Serial_No = STPD.SNo and ST.Module = STPD.Module)  and EntryType = 'I'  Group by Product_Id) as OutVal
				Where StockDetails.Product_Id = OutVal.Product_Id  and not exists (Select * from AMS.StockBatchDetails as STPD)

				Update AMS.StockDetails  Set StockVal = tmp.StockVal FROM  #StockTransOUT AS tmp  Where StockDetails.Voucher_No = tmp.V_No
				AND AMS.StockDetails.Module = tmp.Source  AND AMS.StockDetails.Serial_No = tmp.Sno  AND StockDetails.EntryType = 'O'
				End;
GO
/****** Object:  StoredProcedure [AMS].[Usp_Select_CashBankBook]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
                CREATE Proc[AMS].[Usp_Select_CashBankBook]
					(
						@EVENT INT,
						@From_Date datetime,
						@To_Date datetime,
						@Currency varchar(50)='0',
						@Invoice_No varchar(50)='',
						@Ledger_Id varchar(max)='',
						@SubLedger bit = 0,
						@Remarks bit=0,
						@Narration bit = 0,
						@CombineSales bit = 0,
						@BranchId varchar(512)='0'
					)
					as
					--AMS.Usp_Select_CashBankBook 1,'2017/07/16','2018/07/15','0','','1',0,0,0,0,'0'
					IF @EVENT = 1---- Cash/Bank Book Details Between Date Range
					BEGIN
					create table #temp ( Id bigInt IDENTITY(1,1) NOT NULL,VoucherDate varchar(10),VoucherMiti varchar(10),VoucherNo varchar(50),
					Particulars varchar(255), DebitAmt decimal (18,2), CreditAmt Decimal(18,2), LocalDrAmt Decimal(18,2),LocalCrAmt Decimal(18,2),
					Currency varchar(10), Module varchar(10), Narration varchar(1024), Invoice_Date Datetime, Invoice_No varchar(50) )
					declare @VDate datetime
					declare @VMiti varchar(10)
					declare @PrevVDate datetime
					declare @VNo varchar(50)
					declare @DrAmt decimal (18,2)
					declare @CrAmt decimal (18,2)
					declare @Module varchar(100)
					declare @ModuleType varchar(10)
					declare @CBLedger_Id bigint
					declare @CBLedger varchar(50)
					declare @I int
					SET @I=0
					DECLARE cb INSENSITIVE CURSOR FOR
					select distinct GlId, GLName from AMS.GeneralLedger as GL Inner Join AMS.AccountDetails as AT On GL.GLId= AT.Ledger_Id Left Outer Join AMS.Currency as C On C.CID= AT.Currency_Id
						where (AT.Ledger_ID IN (SELECT Value FROM AMS.fn_Splitstring (@Ledger_Id, ',')) or @Ledger_Id = '') and(AT.Branch_Id in (@BranchId) or @BranchId= '0')
					Order by GLName asc
					OPEN cb
					FETCH NEXT FROM cb
					INTO @CBLedger_Id, @CBLedger
					WHILE @@FETCH_STATUS = 0
					BEGIN
						INSERT INTO #temp  SELECT  '' VDate ,'' VMiti,'' VoucherNo,@CBLedger,0,0,0,0,'' Currency,'' ModuleType,'Cash/Bank' Narration,null InvoiceDate,'' InvoiceNo
						insert into #temp
						Select'' as VDate, '' VMiti,'' VNo, 'Opening -->>' as Particulars,
						case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) )>0 then 0 else (sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) ) end Debit_Amt,
						case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) ) else 0 end Credit_Amt,
						case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then 0 else (sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) end LocalDrAmt,
						case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) ) else 0 end LocalCrAmt,
						'' Currency,'' Module,'' Narration, Null Invoice_Date, Null Invoice_No
							from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CID=AT.Currency_Id
							where(AT.Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date<@From_Date

							and AT.Ledger_Id = @CBLedger_Id and (AT.Currency_Id in (@Currency) or @Currency= '0') having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0
					DECLARE c1 INSENSITIVE CURSOR FOR
					select distinct Voucher_Date from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CID= AT.Currency_Id where AT.Ledger_Id = @CBLedger_Id and (AT.Branch_Id in (@BranchId) or @BranchId= '0')
					and Voucher_Date between @From_Date and @To_Date and(@Invoice_No= '' or Voucher_No like '%'+@Invoice_No) and(AT.Currency_Id in (@Currency) or @Currency= '0') Order by Voucher_Date asc
					OPEN c1
					FETCH NEXT FROM c1
					INTO @VDate
					WHILE @@FETCH_STATUS = 0
					BEGIN
					DECLARE c2 INSENSITIVE CURSOR FOR
					select distinct Voucher_No,Module from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_Id where AT.Ledger_Id = @CBLedger_Id and Voucher_Date=@VDate and(@Invoice_No= '' or Voucher_No like '%'+@Invoice_No) and(AT.Currency_Id in (@Currency) or @Currency= '0')
					OPEN c2
					FETCH NEXT FROM c2
					INTO @VNo, @Module
					WHILE @@FETCH_STATUS = 0
					BEGIN
						set @VMiti=(select top 1 Voucher_Miti from AMS.AccountDetails where Voucher_Date=@VDate and Voucher_No=@VNo AND(Branch_Id in (@BranchId) or @BranchId= '0'))
						set @ModuleType = @Module
						if not exists(select VoucherDate from #temp where VoucherDate=CONVERT(varchar(10),@VDate,103) )
						BEGIN

							IF @I = 0

							Begin
								INSERT INTO #temp  SELECT  CONVERT(varchar(10),@VDate,103) ,@VMiti,'','',0,0,0,0,'' Currency,@ModuleType,'' Narration,@VDate InvoiceDate,'' InvoiceNo
								IF @CombineSales= 1

								BEGIN
									INSERT INTO #temp
									Select '','','', GL.GLName, Sum(AT.Debit_Amt) Debit_Amt, Sum(AT.Credit_Amt) Credit_Amt, Sum(AT.LocalDebit_Amt) LocalDrAmt, Sum(AT.LocalCredit_Amt) LocalCrAmt, ISNULL(C.Ccode,'')Cur_Code,AT.Module,'' Narration,@VDate InvoiceDate,'' InvoiceNo from AMS.AccountDetails AS AT
									INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID

									WHERE EXISTS(Select Voucher_No from AMS.AccountDetails as SAC where AT.Voucher_No = SAC.Voucher_No and AT.Module = Sac.Module and
									SAC.Ledger_ID = @CBLedger_Id) and LocalDebit_Amt-LocalCredit_Amt<> 0 AND AT.Ledger_ID<>@CBLedger_Id And (AT.Currency_ID in (@Currency) or @Currency= '0')

									and AT.Module<>'CB' and AT.Module = 'SB'

									and (AT.Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date = @VDate

									Group BY GL.GLName,AT.Module,C.Ccode,GL.GLCode,AT.Narration
									End

								end
								ELSE

								Begin
									INSERT INTO #temp  SELECT  CONVERT(varchar(10),@VDate,103) ,@VMiti,'','Opening -->>',
									(select CASE WHEN SUM(Debit_Amt)-SUM(Credit_Amt)>0 THEN 0 ELSE abs(SUM(Debit_Amt)-SUM(Credit_Amt)) END from AMS.AccountDetails where Voucher_Date<=@PrevVDate and  Ledger_ID=@CBLedger_Id and(Branch_ID in (@BranchId) or @BranchId= '0')),
									(select CASE WHEN SUM(Debit_Amt)-SUM(Credit_Amt)>0 THEN abs(SUM(Debit_Amt)-SUM(Credit_Amt)) ELSE 0 END from AMS.AccountDetails where Voucher_Date<=@PrevVDate and  Ledger_ID=@CBLedger_Id and(Branch_ID in (@BranchId) or @BranchId= '0')),
									(select CASE WHEN SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)>0 THEN 0 ELSE abs(SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)) END from AMS.AccountDetails where Voucher_Date<=@PrevVDate and  Ledger_ID=@CBLedger_Id and(Branch_ID in (@BranchId) or @BranchId= '0')),
									(select CASE WHEN SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)>0 THEN abs(SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)) ELSE 0 END from AMS.AccountDetails where Voucher_Date<=@PrevVDate and  Ledger_ID=@CBLedger_Id and(Branch_ID in (@BranchId) or @BranchId= '0')),
									'' Currency,'' Source,'' Narration,null BillDate,Null BillNo

									IF @CombineSales = 1

									BEGIN
										INSERT INTO #temp
										Select '','','', GL.GLName,Sum(AT.Debit_Amt) Debit_Amt,Sum(AT.Credit_Amt)CurrCrAmt,Sum(AT.LocalDebit_Amt) LocalDrAmt,Sum(AT.LocalCredit_Amt)LocalCrAmt,ISNULL(C.Ccode,'')Cur_Code,AT.Module,'' Narration,@VDate InvoiceDate,'' InvoiceNo from AMS.AccountDetails AS AT
										INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID

										WHERE EXISTS(Select Voucher_No from AMS.AccountDetails as Sac where AT.Voucher_No = Sac.Voucher_No and AT.Module = Sac.Module and
										Sac.Ledger_ID = @CBLedger_Id) and LocalDebit_Amt-LocalCredit_Amt<> 0 AND AT.Ledger_ID<>@CBLedger_Id And (AT.Currency_ID in (@Currency) or @Currency= '0')

										and AT.Module<>'CB' --and AT.Source = 'SB'

										and (AT.Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date = @VDate

										Group BY GL.GLName,AT.Module,C.Ccode,GL.GLCode,AT.Narration
									End

								End
								SET @I=1
								END

								INSERT INTO #temp
								select '','', Case When ROW_NUMBER() OVER(ORDER BY GL.GlName ASC)=1 Then @VNo else '' End VNo,
								Case When(Select Top 1 Transby_Code from AMS.SystemConfiguration)=1 Then
								isnull(GL.GLCode + ' ' + GL.GLName , isnull(GL.GLCode, GL.GLID) + ' ' + GL.GLName) else isnull(GL.GLName , GL.GLName) End GlDesc,
								Sum(AT.Debit_Amt) Debit_Amt,Sum(AT.Credit_Amt)Credit_Amt,Sum(AT.LocalDebit_Amt) LocalDrAmt,Sum(AT.LocalCredit_Amt)LocalCrAmt,
								ISNULL(C.Ccode, '')Cur_Code,AT.Module,AT.Narration,@VDate BillDate, @VNo BillNo from AMS.AccountDetails AS AT
								INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID

								WHERE AT.Voucher_No= @VNo and AT.Module= @Module and AT.CbLedger_ID= @CBLedger_Id And(CbLedger_ID<> AT.Ledger_ID)

								and(AT.Currency_ID in (@Currency) or @Currency= '0') and(AT.Branch_ID in (@BranchId) or @BranchId= '0')

								Group BY GL.GLName,AT.Module,C.Ccode,GL.GLID,GL.GLCode,AT.Narration
								IF @CombineSales=1
								BEGIN
									INSERT INTO #temp
									Select '','', Case When ROW_NUMBER() OVER(ORDER BY GL.GlName ASC)=1 Then @VNo else '' End VNo, GL.GLName,
									CASE WHEN SUM(AT.Debit_Amt)-SUM(AT.Credit_Amt)>0 THEN abs(SUM(AT.Debit_Amt)-SUM(AT.Credit_Amt)) ELSE 0  END Debit_Amt,
									CASE WHEN SUM(AT.Debit_Amt)-SUM(AT.Credit_Amt)>0 THEN 0 ELSE abs(SUM(AT.Debit_Amt)-SUM(AT.Credit_Amt)) END Credit_Amt,
									CASE WHEN SUM(AT.LocalDebit_Amt)-SUM(AT.LocalCredit_Amt)>0 THEN abs(SUM(AT.LocalDebit_Amt)-SUM(AT.LocalCredit_Amt)) ELSE 0 END LocalDrAmt,
									CASE WHEN SUM(AT.LocalDebit_Amt)-SUM(AT.LocalCredit_Amt)>0 THEN 0 ELSE abs(SUM(AT.LocalDebit_Amt)-SUM(AT.LocalCredit_Amt)) END LocalCrAmt,
									ISNULL(C.Ccode, '')Cur_Code,AT.Module,'' Narration,@VDate BillDate, @VNo BillNo from AMS.AccountDetails AS AT
									INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID

									WHERE EXISTS(Select Voucher_No from AMS.AccountDetails as Sac where AT.Voucher_No = Sac.Voucher_No and AT.Module = Sac.Module and
									Sac.Ledger_ID = @CBLedger_Id) and LocalDebit_Amt-LocalCredit_Amt<> 0 AND AT.Ledger_ID<>@CBLedger_Id And (AT.Currency_ID in (@Currency) or @Currency= '0')

									and AT.Module<>'CB' and AT.Module = 'SB'

									and (AT.Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date = @VDate and Voucher_No = @VNo

									Group BY GL.GLName,AT.Module,C.Ccode,GL.GLCode,AT.Narration
								End

							INSERT INTO #temp
							select top 1 '','','','Chq No : ' + Cheque_No + ' Chq Date : ' + Case When(select Date_Type from AMS.SystemConfiguration)='M' Then Cheque_Miti Else Convert(varchar(10), Cheque_Date,103) End,0,0,0,0,'' Currency,'' Source,'' Narration,@VDate InvoiceDate, @VNo InvoiceNo from AMS.AccountDetails
							WHERE Voucher_No = @VNo and Cheque_No<>'' and Module = @Module
							--if @Narration = 1---- - Condition Of Narration//Not Use, it has show Only one row of top 1 , but we have multiple line Narration
							--begin
							--INSERT INTO #temp
							--select top 1 '','','Narration :', AT.Narration,0,0,0,0,'' Currency,'' Source, '' Narration,@VDate BillDate from AMS.AccountDetails AS AT
							--INNER JOIN AMS.GeneralLedger AS GL ON GL.GlId = AT.Ledger_ID WHERE AT.Voucher_No = @VNo and Module = @Source  and(LTRIM(RTRIM(AT.Narration)) <> '' and AT.Narration is not null) and AT.Ledger_Id = @CBLedger_Id
							--and(AT.Branch_ID in (@BranchId) or @BranchId = '')
							--end
							if @Remarks= 1---- - Condition Of Remarks
							begin

							INSERT INTO #temp select top 1 '','','Remarks :',AT.Remarks,0,0,0,0,'' Currency,'' Source, '' Narration,@VDate BillDate,@VNo BillNo from AMS.AccountDetails AS  AT
							INNER JOIN AMS.GeneralLedger AS GL ON GL.GlId= AT.Ledger_ID WHERE AT.Voucher_No= @VNo and Module = @Module and(LTRIM(RTRIM(AT.Remarks))<>'' and AT.Remarks is not null) and AT.Ledger_Id=CBLedger_Id
							and (AT.Branch_ID in (@BranchId) or @BranchId= '0')

							end
						FETCH NEXT FROM c2
						INTO   @VNo,@Module
					END
					DEALLOCATE c2

							INSERT INTO #temp
							SELECT Distinct '' ,'','','Day Total -->>',
							abs(SUM(DebitAmt)), abs(SUM(CreditAmt)), abs(SUM(LocalDrAmt)), abs(SUM(LocalCrAmt)), '' Currency,'' Source,'' Narration,null InvoiceDate,Null InvoiceNo

							from #temp where Invoice_Date= @VDate
							INSERT INTO #temp  SELECT  Distinct '' ,'','','Day Closing -->>',
						(select CASE WHEN SUM(Debit_Amt)-SUM(Credit_Amt)>0 THEN abs(SUM(Debit_Amt)-SUM(Credit_Amt)) ELSE 0 END from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID where Voucher_Date<=@VDate and  Ledger_ID=@CBLedger_Id and(AT.Currency_ID in (@Currency) or @Currency= '0') and(AT.Branch_ID in (@BranchId) or @BranchId= '0')) Debit_Amt,
						(select CASE WHEN SUM(Debit_Amt)-SUM(Credit_Amt)>0 THEN 0 ELSE abs(SUM(Debit_Amt)-SUM(Credit_Amt)) END from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID where Voucher_Date<=@VDate and  Ledger_ID=@CBLedger_Id and(AT.Currency_ID in (@Currency) or @Currency= '0') and(AT.Branch_ID in (@BranchId) or @BranchId= '0')) Credit_Amt,
						(select CASE WHEN SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)>0 THEN abs(SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)) ELSE 0 END from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID where Voucher_Date<=@VDate and  Ledger_ID=@CBLedger_Id and(AT.Currency_ID in (@Currency) or @Currency= '0') and(AT.Branch_ID in (@BranchId) or @BranchId= '0')) LocalDrAmt,
						(select CASE WHEN SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)>0 THEN 0 ELSE abs(SUM(LocalDebit_Amt)-SUM(LocalCredit_Amt)) END from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID where Voucher_Date<=@VDate and  Ledger_ID=@CBLedger_Id and(AT.Currency_ID in (@Currency) or @Currency= '0') and(AT.Branch_ID in (@BranchId) or @BranchId= '0')) LocalCrAmt,
						'' Currency,'' Source,'' Narration,null BillDate,Null BillNo
						set @PrevVDate = @VDate
						FETCH NEXT FROM c1
						INTO @VDate
					END
					DEALLOCATE c1

					FETCH NEXT FROM cb
						INTO   @CBLedger_Id ,@CBLedger
					END
					DEALLOCATE cb
					select* from #temp order by Id
					drop table #temp
					End
					IF @EVENT=2  ----Cash/Bank Book Summary Between Date Range
					BEGIN

						select distinct '' as Voucher_Date,GLName Voucher_Miti, GLID,0 Openings,0 Receipts,0 Payments,0 Balances,'Cash/Bank' Narration from AMS.GeneralLedger as GL Inner Join AMS.AccountDetails as AT On GL.GLId= AT.Ledger_Id Left Outer Join AMS.Currency as C On C.CID= AT.Currency_Id

						where (AT.Ledger_ID IN (SELECT Value FROM AMS.fn_Splitstring (@Ledger_Id, ',')) or @Ledger_Id = '') and(AT.Branch_Id in (@BranchId) or @BranchId= '0')
						Union All
						Select '' as Voucher_Date, '' Voucher_Miti,GL.GLID,
						case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then 0 else (sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) end Openings,
						0 Receipts, 0 Payments,  0 Balances,'' from AMS.AccountDetails as AT Inner Join AMS.GeneralLedger as GL On GL.GLID= AT.Ledger_ID
						where(AT.Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date<@From_Date and (AT.Ledger_ID IN (SELECT Value FROM AMS.fn_Splitstring (@Ledger_Id, ',')) or @Ledger_Id = '')
						Group By GL.GLID
						having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0
						Union All
						Select Voucher_Date, Voucher_Miti, GL.GLID, 0 Openings, sum(isnull(LocalDebit_Amt,0)) Receipts, sum(isnull(LocalCredit_Amt,0)) Payments, 0 Balances ,''
						from AMS.AccountDetails as AT Inner Join AMS.GeneralLedger as GL On GL.GLID= AT.Ledger_ID
						Where(AT.Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date between @From_Date and @To_Date and(AT.Ledger_ID IN (SELECT Value FROM AMS.fn_Splitstring (@Ledger_Id, ',')) or @Ledger_Id = '')
						Group By Voucher_Date, Voucher_Miti, GL.GLID
						Order By Voucher_Date, GLID
					End;
GO
/****** Object:  StoredProcedure [AMS].[Usp_Select_DayBook]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc[AMS].[Usp_Select_DayBook]
				(
				@EVENT INT,
				@From_Date datetime,
				@To_Date datetime,
				@Currency_Id int,
				@Invoice_No varchar(20)='',
				@Module varchar(max)='',
				@Remarks bit = 0,
				@Branch_Id varchar(100)='0',
				@Audit varchar(10)=''
				)
				as
				--[AMS].[Usp_Select_DayBook] 1,'2017/07/17','2018/07/16','0','','JV,CB,DN,CN,PB,PA,PR,SB,SBT,SA,SR',0,0,''

				DECLARE @CASH_AC bigint
				SET @CASH_AC=(SELECT TOP 1 Cash_AC FROM AMS.SystemConfiguration)

				Declare @VoucherNoList varchar(max)=''
				Declare @Voucher_Date datetime
				Declare @Voucher_Miti varchar(10)
				Declare @Voucher_No varchar(50)
				Declare @Ledger_Id  varchar(100)
				Declare @Dr_Amt decimal (18,2)
				Declare @Cr_Amt decimal (18,2)
				Declare @Source varchar(100)
				Declare @Module_Type varchar(10)
				Declare @I int
				Declare @Audit_By varchar(10)
				Declare @Audit_Date varchar(10)
				IF @EVENT = 1---- Between Date Range
				BEGIN
				create table #temp ( Id bigInt IDENTITY(1,1) NOT NULL,VoucherDate varchar(10),VoucherMiti varchar(10),VoucherNo varchar(50),Particulars varchar(255),
				Debit_Amt decimal (18,2),Credit_Amt Decimal(18,2),LocalDebit_Amt Decimal(18,2),LocalCredit_Amt Decimal(18,2),Currency varchar(10),Module varchar(10),InvoiceNo varchar(50))

				 insert into #temp
				 select'' as VoucherDate, '' VoucherMiti,'' VoucherNo,'Opening' as Particulars,
				 case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) )>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) ) else 0 end Dr_Amt,
				 case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) ) end Cr_Amt,
				  case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) else 0 end LocalDebit_Amt,
				 case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) ) end LocalCredit_Amt, ISNULL(Ccode, '')CurCode,'' Module,'' InvoiceNo
				from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_Id where(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') and Voucher_Date<@From_Date  and Ledger_ID = @CASH_AC

				and (AT.Currency_Id in (@Currency_Id) or @Currency_Id= 0) group By C.Ccode having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				DECLARE c1 INSENSITIVE CURSOR FOR
				Select Distinct Voucher_Date from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_Id where (AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Currency_Id in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and(@Invoice_No= '' or Voucher_No like '%'+@Invoice_No)
				and Voucher_Date between @From_Date and @To_Date order by Voucher_Date asc
				OPEN c1
				FETCH NEXT FROM c1
				INTO @Voucher_Date
				WHILE @@FETCH_STATUS = 0
				BEGIN

				DECLARE c2 INSENSITIVE CURSOR FOR
				Select Distinct Voucher_No,Voucher_Miti,Module from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_Id where Voucher_Date=@Voucher_Date and(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Currency_Id in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')
				OPEN c2
				FETCH NEXT FROM c2
				INTO @Voucher_No, @Voucher_Miti, @Module_Type
				WHILE @@FETCH_STATUS = 0
				BEGIN
					if @Module_Type='JV'
						set @Source = 'Journal Voucher'
					else if @Module_Type='CB'
						set @Source = 'Cash/Bank Voucher'
					else if @Module_Type='RV'
						set @Source = 'Receipt Voucher'
					else if @Module_Type='PV'
						set @Source = 'Payment Voucher'
					else if @Module_Type='CV'
						set @Source = 'Contra Voucher'
					else if @Module_Type='RPV'
						set @Source = 'Receipt/Payment Voucher'
					else if @Module_Type='DN'
						set @Source = 'Debit Note'
					else if @Module_Type='CN'
						set @Source = 'Credit Note'
					else if @Module_Type='PB'
						set @Source = 'Purchase Invoice'
					else if @Module_Type='PAB'
						set @Source = 'Purchase Additional Invoice'
					else if @Module_Type='PIB'
						set @Source = 'Purchase Inter Branch'
					else if @Module_Type='PR'
						set @Source = 'Purchase Return Invoice'
					else if @Module_Type='PEB'
						set @Source = 'Purchase Ex/Br Return'
					else if @Module_Type='SB'
						set @Source = 'Sales Invoice'
					else if @Module_Type='SBT'
						set @Source = 'Ticket Sales'
					else if @Module_Type='SAB'
						set @Source = 'Sales Additional Invoice'
					else if @Module_Type='SIB'
						set @Source = 'Sales Inter Branch'
					else if @Module_Type='SR'
						set @Source = 'Sales Return Invoice'
					else if @Module_Type='SEB'
						set @Source = 'Sale Ex/Br Return'
					else if @Module_Type='HBN'
						set @Source = 'Hotel Booking'
					else if @Module_Type='HBCN'
						set @Source = 'Hotel Booking Cancel'
					else if @Module_Type='HCI'
						set @Source = 'Hotel Check In'
					else if @Module_Type='HCO'
						set @Source = 'Hotel Check Out'
					else
						set @Source = 'Other'

						if not exists(select VoucherDate from #temp where VoucherDate=CONVERT(varchar(10),@Voucher_Date,103) )
						INSERT INTO #temp  SELECT  CONVERT(varchar(10),@Voucher_Date,103) ,@Voucher_Miti,@Voucher_No,@Source,0,0,0,0,'' Currency,@Module_Type,@Voucher_No
						else
						INSERT INTO #temp  SELECT  '' ,'',@Voucher_No,@Source,0,0,0,0,'' Currency,@Module_Type,@Voucher_No

						--if @Remarks= 0---- - Condition Of Narration
						--begin
						INSERT INTO #temp
						Select '','','', GL.GLName, Sum(AT.Debit_Amt) Dr_Amt, Sum(AT.Credit_Amt) Cr_Amt, Sum(AT.LocalDebit_Amt) LocalDebit_Amt, Sum(AT.LocalCredit_Amt) LocalCredit_Amt, ISNULL(Ccode,'')Cur_Code,AT.Ledger_ID Station, AT.Voucher_No from AMS.AccountDetails AS  AT INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID
						Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID WHERE AT.Voucher_No= @Voucher_No and AT.Voucher_Date= @Voucher_Date and AT.Module= @Module_Type and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)
						Group BY AT.Voucher_No,GL.GLName,AT.Ledger_ID,C.Ccode,AT.EnterDate
						Order By LocalDebit_Amt Desc ,CONVERT(varchar(10), AT.EnterDate,108) asc
						--end
						--else
						--begin
						--DECLARE c3 INSENSITIVE CURSOR FOR
						--select distinct AT.Ledger_Code from AMS.AccountDetails AS  AT INNER JOIN
						--AMS.GeneralLedger AS L ON L.Ledger_Code=AT.Ledger_Code WHERE  AT.Voucher_No=@Voucher_No and L.IS_LEDGER=1
						--OPEN c3
						--FETCH NEXT FROM c3
						--INTO @Ledger_Code
						--WHILE @@FETCH_STATUS = 0
						--BEGIN

						--INSERT INTO #temp select '','','',L.GLName,AT.LocalDebit_Amt,AT.LocalCredit_Amt,'' Station from AMS.AccountDetails AS  AT INNER JOIN
						--AMS.GeneralLedger AS L ON L.Ledger_Code=AT.Ledger_Code WHERE  AT.Ledger_Code=@Ledger_Code and L.IS_LEDGER=1
						--INSERT INTO #temp select TOP 1 '','','Narr :',AT.Narration,0,0,'' Station from AMS.AccountDetails AS  AT INNER JOIN
						--AMS.GeneralLedger AS L ON L.Ledger_Code=AT.Ledger_Code WHERE  AT.Ledger_Code=@Ledger_Code and L.IS_LEDGER=1
						--	FETCH NEXT FROM c3
						--	INTO @Ledger_Code
						--END
						--DEALLOCATE c3
						--end

						if @Remarks=1 ----- Condition Of Remarks
						begin
							INSERT INTO #temp
							Select top 1 '','','Remarks :', AT.Remarks,0,0,0,0,'' Currency,'' Module,AT.Voucher_No from AMS.AccountDetails AS  AT
							 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID WHERE AT.Voucher_No= @Voucher_No and AT.Module= @Module_Type  and AT.Remarks<>''
						 end

						 INSERT INTO #temp  SELECT  '' ,'','','Total -->>',
						(Select SUM(Debit_Amt) from AMS.AccountDetails Where Voucher_No= @Voucher_No and Voucher_Date = @Voucher_Date and Module = @Module_Type and (Branch_Id in (@Branch_Id) or @Branch_Id= 0)),
						(Select SUM(Credit_Amt) from AMS.AccountDetails Where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date and Module=@Module_Type and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)),
						(Select SUM(LocalDebit_Amt) from AMS.AccountDetails Where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date and Module=@Module_Type and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)),
						(Select SUM(LocalCredit_Amt) from AMS.AccountDetails Where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date and Module=@Module_Type and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)),'' Currency,'' Station,@Voucher_No

					FETCH NEXT FROM c2
					INTO  @Voucher_No,@Voucher_Miti,@Module_Type
				END
				DEALLOCATE c2

					INSERT INTO #temp  SELECT  '' ,'','','Day Total -->>',
					(Select isnull(SUM(Debit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Dr_Amt,
					(Select isnull(SUM(Credit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Cr_Amt,
					(Select isnull(SUM(LocalDebit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) LocalDebit_Amt,
					(Select isnull(SUM(LocalCredit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) LocalCredit_Amt,
					'' Currency,'' Station	,'' Bill_No

					FETCH NEXT FROM c1
					INTO   @Voucher_Date
				END
				DEALLOCATE c1

				Insert into #temp
				Select'' as Addate, '' Bsdate,'' Voucher_No,'Closing' as Particulars,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0))) else 0 end Dr_Amt,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) )  end Cr_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0))) else 0 end LocalDebit_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) )  end LocalCredit_Amt,
				'' Currency,'' Module,'' Bill_No from AMS.AccountDetails Where(Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and Voucher_Date <= @To_Date and Ledger_ID=@CASH_AC having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				select* from #temp
				drop table #temp
				end
				IF @EVENT=2  ---- Between Date Range T Format
				BEGIN
				create table #tempTF(Id bigInt IDENTITY(1,1) NOT NULL,VoucherDate varchar(10),VoucherMiti varchar(10),Voucher_No1 varchar(50),Particulars1 varchar(255),Dr_Amt decimal(18,2),LocalDebit_Amt Decimal(18,2),Voucher_No2 varchar(50),Particulars2 varchar(255),Cr_Amt Decimal(18,2),LocalCredit_Amt Decimal(18,2),Currency varchar(10),Module varchar(10),InvoiceNo varchar(50))

				Insert Into #tempTF
				Select'' as Addate, '' Bsdate,'' Voucher_No1, 'Opening' as Particulars1,
				Case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) )>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) ) else 0 end Dr_Amt,
				Case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) else 0 end LocalDebit_Amt,
				'' Voucher_No2,'Opening' as Particulars2,
				Case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) ) end Cr_Amt,
				Case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) ) end LocalCredit_Amt, ISNULL(Ccode, '')Cur_Code,'' Module,''InvoiceNo
				From AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID where(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') and Voucher_Date<@From_Date  and Ledger_ID = @CASH_AC
				and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) group By C.Ccode having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				DECLARE CDBT INSENSITIVE CURSOR FOR
				Select Distinct Voucher_Date from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_Id where  (AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Currency_Id in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and(@Invoice_No= '' or Voucher_No like '%'+@Invoice_No)
				and Voucher_Date between @From_Date and @To_Date order by Voucher_Date asc
				OPEN CDBT
				FETCH NEXT FROM CDBT
				INTO @Voucher_Date
				WHILE @@FETCH_STATUS = 0
				BEGIN

				DECLARE c2 INSENSITIVE CURSOR FOR
				Select distinct Voucher_No,Voucher_Miti,Module from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_Id where Voucher_Date=@Voucher_Date and(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Currency_Id in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')
				OPEN c2
				FETCH NEXT FROM c2
				INTO @Voucher_No, @Voucher_Miti, @Module_Type
				WHILE @@FETCH_STATUS = 0
				BEGIN

					if @Module_Type='JV'
						set @Source = 'Journal Voucher'
					else if @Module_Type='CB'
						set @Source = 'Cash/Bank Voucher'
					else if @Module_Type='RV'
						set @Source = 'Receipt Voucher'
					else if @Module_Type='PV'
						set @Source = 'Payment Voucher'
					else if @Module_Type='CV'
						set @Source = 'Contra Voucher'
					else if @Module_Type='RPV'
						set @Source = 'Receipt/Payment Voucher'
					else if @Module_Type='DN'
						set @Source = 'Debit Note'
					else if @Module_Type='CN'
						set @Source = 'Credit Note'
					else if @Module_Type='PB'
						set @Source = 'Purchase Invoice'
					else if @Module_Type='PAB'
						set @Source = 'Purchase Additional Invoice'
					else if @Module_Type='PIB'
						set @Source = 'Purchase Inter Branch'
					else if @Module_Type='PR'
						set @Source = 'Purchase Return Invoice'
					else if @Module_Type='PEB'
						set @Source = 'Purchase Ex/Br Return'
					else if @Module_Type='SB'
						set @Source = 'Sales Invoice'
					else if @Module_Type='SBT'
						set @Source = 'Ticket Sales'
					else if @Module_Type='SAB'
						set @Source = 'Sales Additional Invoice'
					else if @Module_Type='SIB'
						set @Source = 'Sales Inter Branch'
					else if @Module_Type='SR'
						set @Source = 'Sales Return Invoice'
					else if @Module_Type='SEB'
						set @Source = 'Sale Ex/Br Return'
					else if @Module_Type='HBN'
						set @Source = 'Hotel Booking'
					else if @Module_Type='HBCN'
						set @Source = 'Hotel Booking Cancel'
					else if @Module_Type='HCI'
						set @Source = 'Hotel Check In'
					else if @Module_Type='HCO'
						set @Source = 'Hotel Check Out'
					else

						set @Source = 'Other'

						if not exists(select VoucherDate from #tempTF where VoucherDate=CONVERT(varchar(10),@Voucher_Date,103) )
						Begin
							INSERT INTO #tempTF  SELECT  CONVERT(varchar(10),@Voucher_Date,103) ,@Voucher_Miti,'' Voucher_No1,'' Particular1,0,0,'' Voucher_No2,'' Particular2,0,0,'' Currency,'' Station_Type,'' InvoiceNo
							INSERT INTO #tempTF  SELECT  '' ,'',@Voucher_No,@Source,0,0,@Voucher_No,@Source,0,0,'' Currency,@Module_Type,@Voucher_No Bill_No
						End
						else
						Begin
							INSERT INTO #tempTF  SELECT  '' ,'',@Voucher_No,@Source,0,0,@Voucher_No,@Source,0,0,'' Currency,@Module_Type,@Voucher_No Bill_No
						End

						INSERT INTO #tempTF
						Select '','','' Voucher_No, GL.GLName, Sum(AT.Debit_Amt) Dr_Amt, Sum(AT.LocalDebit_Amt) LocalDebit_Amt,'' Voucher_No,'' GLName, 0 Cr_Amt,0 LocalCredit_Amt, ISNULL(Ccode,'')Cur_Code,AT.Ledger_ID Station, AT.Voucher_No as InvoiceNo from AMS.AccountDetails AS  AT
						INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID
						Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID WHERE AT.Voucher_No= @Voucher_No and AT.Voucher_Date= @Voucher_Date and AT.Module= @Module_Type and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)
						Group BY AT.Voucher_No,GL.GLName,AT.Ledger_ID,C.Ccode
						Having SUM(AT.LocalDebit_Amt)>0

						INSERT INTO #tempTF
						Select '','','' Voucher_No,'' GLName,0 Dr_Amt,0 LocalDebit_Amt,'' Voucher_No,GL.GLName, Sum(AT.Credit_Amt)Cr_Amt,Sum(AT.LocalCredit_Amt)LocalCredit_Amt,ISNULL(Ccode,'')Cur_Code,AT.Ledger_ID Station, AT.Voucher_No as InvoiceNo from AMS.AccountDetails AS  AT
						INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID
						 Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID WHERE AT.Voucher_No= @Voucher_No and AT.Voucher_Date= @Voucher_Date and AT.Module= @Module_Type and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)
						Group BY AT.Voucher_No,GL.GLName,AT.Ledger_ID,C.Ccode
						Having SUM(AT.LocalCredit_Amt)>0

						INSERT INTO #tempTF  SELECT  '' ,'','','Total -->>',
						(Select SUM(Debit_Amt) from AMS.AccountDetails where Voucher_No= @Voucher_No and Voucher_Date = @Voucher_Date and Module = @Module_Type and (Branch_Id in (@Branch_Id) or @Branch_Id= 0)),
						(Select SUM(LocalDebit_Amt) from AMS.AccountDetails where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date and Module=@Module_Type and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)),
						'' Voucher_No2,'' Particular2,
						(Select SUM(Credit_Amt) from AMS.AccountDetails where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date and Module=@Module_Type and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)),
						(Select SUM(LocalCredit_Amt) from AMS.AccountDetails where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date and Module=@Module_Type and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)),'' Currency,'' Module,'' InvoiceNo

					FETCH NEXT FROM c2
					INTO   @Voucher_No,@Voucher_Miti,@Module_Type
				END
				DEALLOCATE c2

					INSERT INTO #tempTF  SELECT  '' ,'','','Day Total -->>',
					(Select isnull(SUM(Debit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Dr_Amt,
					(Select isnull(SUM(LocalDebit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) LocalDebit_Amt,
					'' Voucher_No2,'' Particular2,
					(Select isnull(SUM(Credit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Cr_Amt,
					(Select isnull(SUM(LocalCredit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) LocalCredit_Amt,
					'' Currency,'' Station	,'' Bill_No

					FETCH NEXT FROM CDBT
					INTO   @Voucher_Date
				END
				DEALLOCATE CDBT

				insert into #tempTF select'' as VoucherDate, '' VoucherMiti,'' Voucher_No,'Closing' as Particulars,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0))) else 0 end Dr_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0))) else 0 end LocalDebit_Amt,
				'' Voucher_No2,'' Particular2,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) )  end Cr_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) )  end LocalCredit_Amt,
				'' Currency,'' Module ,'' InvoiceNo from AMS.AccountDetails where(Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and Voucher_Date <= @To_Date and Ledger_ID=@CASH_AC
				having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				select* from #tempTF
				drop table #tempTF
				end

				IF @EVENT=3  ---- Auditors Lock and UnLock
				BEGIN
				create table #temp1(VoucherDate varchar(10),VoucherMiti varchar(10),VoucherNo varchar(50),Particulars varchar(255),Debit_Amt decimal(18,2),Credit_Amt Decimal(18,2),LocalDebit_Amt Decimal(18,2),LocalCredit_Amt Decimal(18,2),Currency varchar(10),Module varchar(10),Audit_By varchar(10),Audit_Date varchar(10))

				 insert into #temp1
				 select'' as Addate, '' Bsdate,'' Voucher_No,
				 'Opening' as Particulars,
				 case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) )>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) ) else 0 end Dr_Amt,
				 case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) ) end Cr_Amt,
				  case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) else 0 end LocalDebit_Amt,
				 case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) ) end LocalCredit_Amt, ISNULL(Ccode, '')Cur_Code,'' Station,'' Audit_By,'' Audit_Date
				from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID where(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') and Voucher_Date<@From_Date  and Ledger_ID = @CASH_AC
				and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) group By C.Ccode having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				DECLARE c1 INSENSITIVE CURSOR FOR
				Select distinct Voucher_Date from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_Id
				Inner Join (Select Audit_By, CONVERT(varchar(10), Audit_Date,103) Audit_Date,Voucher_No,Station from(
				 Select Voucher_No, Voucher_Date, Station, Audit, Audit_By, Audit_Date From AMS.Voucher_Main Where (Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select Invoice_No Voucher_No, Invoice_Date Voucher_Date,'PB' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseInvoice_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select PurReturn_Invoice_No Voucher_No, ReturnInvoice_Date Voucher_Date,'PR' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseReturn_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select ExpiryBreakage_No Voucher_No, ExpiryBreakage_Date Voucher_Date,'PEB' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseExpiryBreakage_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select PurchaseAdd_No Voucher_No, PurchaseAdd_Date Voucher_Date,'PA' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseAdd_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select Invoice_No Voucher_No, Invoice_Date Voucher_Date,'SB' Station,Audit,Audit_By,Audit_Date From AMS.SalesInvoice_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select SalesReturn_Invoice_No Voucher_No, ReturnInvoice_Date Voucher_Date,'SR' Station,Audit,Audit_By,Audit_Date From AMS.SalesReturn_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select ExpiryBreakage_No Voucher_No, ExpiryBreakage_Date Voucher_Date,'SEB' Station,Audit,Audit_By,Audit_Date From AMS.SalesExpiryBreakage_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select SalesAdd_No Voucher_No, SalesAdd_Date Voucher_Date,'SAB' Station,Audit,Audit_By,Audit_Date From AMS.SalesAdd_Main Where(Audit= @Audit or @Audit = '') and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				) as aa ) as AB On AB.Voucher_No=AT.Voucher_No and AB.Station=AT.Station
				Where(Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Cur_Id in (@Currency_Id) or @Currency_Id= 0) and(AT.Station IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and(@Invoice_No= '' or AT.Voucher_No like '%'+@Invoice_No)
				and Voucher_Date between @From_Date and @To_Date order by Voucher_Date asc
				OPEN c1
				FETCH NEXT FROM c1
				INTO @Voucher_Date
				WHILE @@FETCH_STATUS = 0
				BEGIN

				DECLARE c2 INSENSITIVE CURSOR FOR
				Select Distinct AT.Voucher_No,Audit_By,CONVERT(varchar(10), Audit_Date,103) Audit_Date from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_Id
				Inner Join(Select Audit_By, CONVERT(varchar(10), Audit_Date,103) Audit_Date,Voucher_No,Station from(
				Select Voucher_No, Voucher_Date, Station, Audit, Audit_By, Audit_Date From AMS.Voucher_Main Where (Audit= @Audit or @Audit = '') and Voucher_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select Invoice_No Voucher_No, Invoice_Date Voucher_Date,'PB' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseInvoice_Main Where(Audit= @Audit or @Audit = '') and Invoice_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select PurReturn_Invoice_No Voucher_No, ReturnInvoice_Date Voucher_Date,'PR' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseReturn_Main Where(Audit= @Audit or @Audit = '') and ReturnInvoice_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select ExpiryBreakage_No Voucher_No, ExpiryBreakage_Date Voucher_Date,'PEB' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseExpiryBreakage_Main Where(Audit= @Audit or @Audit = '') and ExpiryBreakage_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select PurchaseAdd_No Voucher_No, PurchaseAdd_Date Voucher_Date,'PA' Station,Audit,Audit_By,Audit_Date From AMS.PurchaseAdd_Main Where(Audit= @Audit or @Audit = '') and PurchaseAdd_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select Invoice_No Voucher_No, Invoice_Date Voucher_Date,'SB' Station,Audit,Audit_By,Audit_Date From AMS.SalesInvoice_Main Where(Audit= @Audit or @Audit = '') and Invoice_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select SalesReturn_Invoice_No Voucher_No, ReturnInvoice_Date Voucher_Date,'SR' Station,Audit,Audit_By,Audit_Date From AMS.SalesReturn_Main Where(Audit= @Audit or @Audit = '') and ReturnInvoice_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select ExpiryBreakage_No Voucher_No, ExpiryBreakage_Date Voucher_Date,'SEB' Station,Audit,Audit_By,Audit_Date From AMS.SalesExpiryBreakage_Main Where(Audit= @Audit or @Audit = '') and ExpiryBreakage_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				Union All Select SalesAdd_No Voucher_No, SalesAdd_Date Voucher_Date,'SAB' Station,Audit,Audit_By,Audit_Date From AMS.SalesAdd_Main Where(Audit= @Audit or @Audit = '') and SalesAdd_Date = @Voucher_Date and(Branch_Id in (@Branch_Id) or @Branch_Id= 0)
				) as aa ) as AB On AB.Voucher_No=AT.Voucher_No and AB.Station=AT.Station
				Where(Branch_Id in (@Branch_Id) or @Branch_Id= 0) and Voucher_Date = @Voucher_Date and(AT.Cur_Id in (@Currency_Id) or @Currency_Id= 0) and(AT.Station IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')

				OPEN c2
				FETCH NEXT FROM c2
				INTO @Voucher_No, @Audit_By, @Audit_Date
				WHILE @@FETCH_STATUS = 0
				BEGIN
					if (@VoucherNoList='')
						set @VoucherNoList = @Voucher_No
					else
						set @VoucherNoList = @VoucherNoList + ',' + @Voucher_No

					set @Voucher_Date = (select top 1 Voucher_Miti from AMS.AccountDetails where  Voucher_Date=@Voucher_Date and Voucher_No=@Voucher_No AND(Branch_Id in (@Branch_Id) or @Branch_Id= 0))
					set @Source = (select top 1  Module from AMS.AccountDetails where Voucher_Date=@Voucher_Date and Voucher_No=@Voucher_No and(Branch_Id in (@Branch_Id) or @Branch_Id= 0))
					set @Module_Type = @Source

					if @Source='JV'
					set @Source = 'Journal Voucher'
					else if @Source='RV'
					set @Source = 'Receipt Voucher'
					else if @Source='PV'
					set @Source = 'Payment Voucher'
					else if @Source='CV'
					set @Source = 'Contra Voucher'
					else if @Source='RPV'
					set @Source = 'Receipt/Payment Voucher'
					else if @Source='DN'
					set @Source = 'Debit Note'
					else if @Source='CN'
					set @Source = 'Credit Note'
					else if @Source='PB'
					set @Source = 'Purchase Invoice'
					else if @Source='PA'
					set @Source = 'Purchase Additional Invoice'
					else if @Source='PIB'
					set @Source = 'Purchase Inter Branch'
					else if @Source='PR'
					set @Source = 'Purchase Return Invoice'
					else if @Source='PEB'
					set @Source = 'Purchase Ex/Br Return'
					else if @Source='SB'
						set @Source = 'Sales Invoice'
					else if @Source='SBT'
						set @Source = 'Ticket Sales'
					else if @Source='SA'
					set @Source = 'Sales Additional Invoice'
					else if @Source='SIB'
					set @Source = 'Sales Inter Branch'
					else if @Source='SR'
					set @Source = 'Sales Return Invoice'
					else if @Source='SEB'
					set @Source = 'Sale Ex/Br Return'
					else if @Source='HBN'
					set @Source = 'Hotel Booking'
					else if @Source='HBCN'
					set @Source = 'Hotel Booking Cancel'
					else if @Source='HCI'
					set @Source = 'Hotel Check In'
					else if @Source='HCO'
					set @Source = 'Hotel Check Out'
					else

					set @Source = 'Other'

						if not exists(select VoucherDate from #temp1 where VoucherDate=CONVERT(varchar(10),@Voucher_Date,103) )
						Begin

							INSERT INTO #temp1
							SELECT CONVERT(varchar(10),@Voucher_Date,103) ,@Voucher_Miti,@Voucher_No,@Source,0,0,0,0,'' Currency,@Module_Type,@Audit_By,CONVERT(varchar(10), @Audit_Date,103)Audit_Date
					   End
						else

						Begin
							INSERT INTO #temp1
							Select '' ,'', @Voucher_No, @Source,0,0,0,0,'' Currency,@Module_Type,@Audit_By,CONVERT(varchar(10), @Audit_Date,103)Audit_Date
						  End
						--if @Remarks=0  ----- Condition Of Narration
						--begin
						INSERT INTO #temp1 Select '','','',GL.GlName,Sum(AT.Debit_Amt) Debit_Amt,Sum(AT.Credit_Amt)Credit_Amt,Sum(AT.LocalDebit_Amt) LocalDebit_Amt,Sum(AT.LocalCredit_Amt)LocalCredit_Amt,ISNULL(Ccode,'')Cur_Code,AT.Ledger_ID Module,'' Audit_By,'' Audit_Date from AMS.AccountDetails AS  AT INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=AT.Ledger_ID
						Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID WHERE  AT.Voucher_No=@Voucher_No and AT.Voucher_Date=@Voucher_Date and(AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) Group BY GL.GLName,AT.Ledger_ID,C.Ccode
						--end
						--else
						--begin
						--DECLARE c3 INSENSITIVE CURSOR FOR
						--select distinct AT.Ledger_ID from AMS.AccountDetails AS  AT INNER JOIN
						--AMS.GeneralLedger AS GL ON GL.GLID=AT.Ledger_ID WHERE  AT.Voucher_No=@Voucher_No
						--OPEN c3
						--FETCH NEXT FROM c3
						--INTO @Ledger_ID
						--WHILE @@FETCH_STATUS = 0
						--BEGIN

						--INSERT INTO #temp1 select '','','',L.GLName,AT.LocalDebit_Amt,AT.LocalCredit_Amt,'' Station,'' Audit_By,'' Audit_Date from AMS.AccountDetails AS  AT INNER JOIN
						--AMS.GeneralLedger AS GL ON L.GLID=AT.Ledger_ID WHERE  AT.Ledger_Code=@Ledger_Code and L.IS_LEDGER=1

						--INSERT INTO #temp1 select TOP 1 '','','Narr :',AT.Narration,0,0,'' Station,'' Audit_By,'' Audit_Date from AMS.AccountDetails AS  AT INNER JOIN
						--AMS.GeneralLedger AS GL ON L.GLID=AT.Ledger_ID WHERE  AT.Ledger_Code=@Ledger_Code and L.IS_LEDGER=1

						--	FETCH NEXT FROM c3
						--	INTO @Ledger_ID
						--END
						--DEALLOCATE c3
						--end

						if @Remarks=1 ----- Condition Of Remarks
						begin

							INSERT INTO #temp1 select top 1 '','','Remarks :',AT.Remarks,0,0,0,0,'' Currency,'' Station,'' Audit_By,'' Audit_Date from AMS.AccountDetails AS  AT
							INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID=AT.Ledger_ID WHERE  AT.Voucher_No=@Voucher_No and AT.Remarks<>''
						end

						INSERT INTO #temp1  SELECT  '' ,'','','Total -->>',
						(select SUM(Debit_Amt) from AMS.AccountDetails where Voucher_No= @Voucher_No and Voucher_Date = @Voucher_Date),
						(select SUM(Credit_Amt) from AMS.AccountDetails where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date),
						(select SUM(LocalDebit_Amt) from AMS.AccountDetails where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date),
						(select SUM(LocalCredit_Amt) from AMS.AccountDetails where Voucher_No=@Voucher_No and Voucher_Date=@Voucher_Date),'' Currency,'' Module,'' Audit_By,'' Audit_Date

					FETCH NEXT FROM c2
					INTO   @Voucher_No,@Audit_By,@Audit_Date
				END
				DEALLOCATE c2

					INSERT INTO #temp1  SELECT  '' ,'','','Day Total -->>',
					(select isnull(SUM(Debit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where (AT.Voucher_No IN (SELECT Value FROM fn_Split(@VoucherNoList, ',')) or @VoucherNoList = '') and Voucher_Date = @Voucher_Date and(AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)) Dr_Amt,
					(select isnull(SUM(Credit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where (AT.Voucher_No IN (SELECT Value FROM fn_Split(@VoucherNoList, ',')) or @VoucherNoList = '') and Voucher_Date = @Voucher_Date and(AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)) Cr_Amt,
					(select isnull(SUM(LocalDebit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where (AT.Voucher_No IN (SELECT Value FROM fn_Split(@VoucherNoList, ',')) or @VoucherNoList = '') and Voucher_Date = @Voucher_Date and(AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)) Local_DrAmt,
					(select isnull(SUM(LocalCredit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where (AT.Voucher_No IN (SELECT Value FROM fn_Split(@VoucherNoList, ',')) or @VoucherNoList = '') and Voucher_Date = @Voucher_Date and(AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0)) Local_CrAmt,
					'' Currency,'' Module	,'' Audit_By,'' Audit_Date

					set @VoucherNoList=''
					FETCH NEXT FROM c1

					INTO @Voucher_Date
				END
				DEALLOCATE c1

				Insert Into #temp1 select'' as VoucherDate, '' VoucherMiti,'' Voucher_No,'Closing' as Particulars,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0))) else 0 end Debit_Amt,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) )  end Credit_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0))) else 0 end LocalDebit_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) )  end LocalCredit_Amt,
				'' Currency,'' Module,'' Audit_By,'' Audit_Date from AMS.AccountDetails where(Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module in (@Module) or @Module= '') and Voucher_Date <= @To_Date and Ledger_ID=@CASH_AC having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				select* from #temp1
				drop table #temp1

				End

				IF @EVENT=4  ---- Summary Normal
				BEGIN
				create table #tempSN (Id bigInt IDENTITY(1,1) NOT NULL,VoucherDate varchar(10),VoucherMiti varchar(10),Particulars varchar(255),Debit_Amt decimal(18,2),Credit_Amt Decimal(18,2),LocalDebit_Amt Decimal(18,2),LocalCredit_Amt Decimal(18,2),Currency varchar(10),Ledger_Id varchar(10))

				 insert into #tempSN
				 select '' as VoucherDate, '' VoucherMiti, 'Opening' as Particulars,
				 case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) )>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) ) else 0 end Debit_Amt,
				 case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) ) end Credit_Amt,
				 case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) else 0 end LocalDebit_Amt,
				 case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) ) end LocalCredit_Amt,
				ISNULL(Ccode, '')CurCode,'' Ledger_Id
				from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId=AT.Currency_ID Where(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') and Voucher_Date<@From_Date  and Ledger_ID = @CASH_AC

				and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) group By C.Ccode having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				DECLARE c1 INSENSITIVE CURSOR FOR
				Select distinct Voucher_Date, Voucher_Miti from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where (AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and(@Invoice_No= '' or Voucher_No like '%'+@Invoice_No)
				and Voucher_Date between @From_Date and @To_Date order by Voucher_Date asc
				OPEN c1
				FETCH NEXT FROM c1
				INTO @Voucher_Date, @Voucher_Miti
				WHILE @@FETCH_STATUS = 0
				BEGIN

					if not exists(select VoucherDate from #tempSN where VoucherDate=CONVERT(varchar(10),@Voucher_Date,103) )
						INSERT INTO #tempSN  SELECT  CONVERT(varchar(10),@Voucher_Date,103) ,@Voucher_Miti,'',0,0,0,0,'' Currency,'' Ledger_Id
					else
						INSERT INTO #tempSN  SELECT  '' ,'','',0,0,0,0,'' Currency,'' Ledger_Id

					INSERT INTO #tempSN
					Select '','', GL.GLName,
					case when (sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) )>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)) ) else 0 end Debit_Amt,
					case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) ) end Credit_Amt,
					case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) )>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) ) else 0 end LocalDebit_Amt,
					case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) ) end LocalCredit_Amt,
					ISNULL(Ccode, '')CurCode,AT.Ledger_Id from AMS.AccountDetails AS  AT INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID= AT.Ledger_ID
					Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID WHERE AT.Voucher_Date= @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(AT.Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(AT.Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')
					Group BY GL.GLName,AT.Ledger_Id,C.Ccode
					Order By LocalDebit_Amt Desc

					INSERT INTO #tempSN  SELECT  '' ,'','Day Total -->>',
					(Select isnull(SUM(Debit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Debit_Amt,
					(Select isnull(SUM(Credit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Credit_Amt,
					(Select isnull(SUM(LocalDebit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Local_DrAmt,
					(Select isnull(SUM(LocalCredit_Amt),0) from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId= AT.Currency_ID where Voucher_Date = @Voucher_Date and (AT.Currency_ID in (@Currency_Id) or @Currency_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '') ) Local_CrAmt,
					'' Currency,'' Ledger_Id

					FETCH NEXT FROM c1
					INTO   @Voucher_Date ,@Voucher_Miti
				END
				DEALLOCATE c1

				Insert into #tempSN
				Select '' as VoucherDate, '' VoucherMiti,'Closing' as Particulars,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0))) else 0 end Debit_Amt,
				case when(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(Debit_Amt,0))-sum(isnull(Credit_Amt,0)),0) )  end Credit_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0))) else 0 end LocalDebit_Amt,
				case when(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)))>0 then 0 else  abs(ISNULL(sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)),0) )  end LocalCredit_Amt,
				'' Currency,'' Ledger_Id from AMS.AccountDetails Where(Branch_Id in (@Branch_Id) or @Branch_Id= 0) and(Module IN (SELECT Value FROM fn_Split(@Module, ',')) or @Module = '')  and Voucher_Date <= @To_Date and Ledger_Id=@CASH_AC having abs(SUM(LocalDebit_Amt-LocalCredit_Amt))>0

				Select* from #tempSN
				drop table #tempSN
				end ;
GO
/****** Object:  StoredProcedure [AMS].[Usp_Select_EntryLogRegister]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [AMS].[Usp_Select_EntryLogRegister]
				(
					@EVENT INT,
					@From_Date datetime,
					@To_Date datetime,
					@Currency_Id int,
					@Invoice_No varchar(20)='',
					@Station varchar(max)='',
					@Remarks bit=0,
					@Branch_Id int=0,
					@Audit varchar(10)=''
				)
				as
				--declare  @Station varchar(max)='SB,JV'
				--[AMS].[Usp_Select_EntryLogRegister]1,'2017/01/01','2018/12/10',0,'','SB',0,0,''
				declare @tblEntryLogRegister as Table(DocumentNo Varchar(128),Voucher_Date DateTime,Voucher_Miti Varchar(10),Action Varchar(10),ActionDateTime DateTime,Descriptions Varchar(128),Amount Decimal(18,2),Remarks Varchar(1024), Is_Printed bit,No_Print int,Printed_By Varchar(50),Printed_Date datetime  )

				Declare @Station_Type Varchar(8)='',@Source Varchar(32)='',@noRow int=0
				Declare Cur_Station Cursor For
				SELECT Value FROM fn_Split(@Station, ',')
				Open Cur_Station
				Fetch Next From Cur_Station Into @Station_Type
				While @@fetch_Status=0
				Begin
					if @Station_Type='SB'
					Begin
						set @Source='Sales Invoice'
							if(Select Count(SB_Invoice) From AMS.AuditLogSB_Master)>0
							Begin
								Insert Into @tblEntryLogRegister(DocumentNo,Voucher_Miti) values(@Source,'b')

								Insert Into @tblEntryLogRegister
								Select '    ' +Main.SB_Invoice,GL.Invoice_Date,Invoice_Miti,Main.ActionType,Main.Action_Date,GL.GlName,NETAMT.Net_Amount,Remarks,IsNull(Is_Printed,0) Is_Printed,No_Print,Printed_By,Printed_Date From
								(
									Select Distinct SB_Invoice,ActionType,Max(ActionDate)Action_Date From [AMS].[AuditLogSB_Master] HSM  Join AMS.GeneralLedger L On HSM.Customer_ID=L.GLID
									Group By SB_Invoice,ActionType
								)as Main
								Join
								(
									Select Distinct SB_Invoice,ActionDate,Sum(N_Amount)Net_Amount  From [AMS].[AuditLogSB_Master] HSM Join AMS.GeneralLedger as L On HSM.Customer_ID=L.GLID
									Group By SB_Invoice,ActionDate
								) NETAMT On Main.SB_Invoice=NETAMT.SB_Invoice and Main.Action_Date=NETAMT.ActionDate
								Join
								(
									Select Distinct SB_Invoice,L.GlName,ActionDate,Invoice_Date,Invoice_Miti,Remarks,IsNull(Is_Printed,0) Is_Printed,No_Print,Printed_By,Printed_Date  From [AMS].[AuditLogSB_Master] HSM  Join AMS.GeneralLedger as L On HSM.Customer_ID=L.GLID
								)GL On Main.SB_Invoice=GL.SB_Invoice and Main.Action_Date=GL.ActionDate

								Insert Into @tblEntryLogRegister(Descriptions,Amount)
								Select 'Total Number Of Entries For ' + @Source +' is ' +Convert(varchar(10),Count(HSM.SB_Invoice))NoRow,Sum(N_Amount)Net_Amount
								From [AMS].[AuditLogSB_Master] HSM

								Select @noRow=@noRow +Count(HSM.SB_Invoice)
								From [AMS].[AuditLogSB_Master] HSM
							End
					End
					Fetch Next From Cur_Station Into @Station_Type
				End
				Close Cur_Station
				Deallocate Cur_Station
				Select @noRow nrow,* From @tblEntryLogRegister;
GO
/****** Object:  StoredProcedure [AMS].[Usp_Select_JournalBook]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 
                CREATE Proc [AMS].[Usp_Select_JournalBook]
					(
					 @From_Date datetime,
					 @To_Date datetime,
					 @Currency  varchar(20) = '0' ,
					 @Find varchar(20)= ''  ,
					 @Source varchar(max)= '' ,
					 @SubLedger bit = 0,
					 @Remarks bit = 0 ,
					 @Narration bit = 0,
					 @All bit = 0,
					 @BranchId varchar(20) = '0'
					)
					as
					--AMS.[Usp_Select_JournalBook] '2017/07/15','2018/07/15','0','','JV,DN,CN,PB,PAB,PR,SB,SA,SR',0,0,0,1,'0'
					 DECLARE @CASH_AC VARCHAR(50)
					 SET @CASH_AC = (SELECT TOP 1 Cash_AC FROM AMS.SystemConfiguration)
					 Create table #temp (Id bigInt IDENTITY(1,1) NOT NULL, VoucherDate varchar(10),VoucherMiti varchar(10),VoucherNo varchar(50),Particulars varchar(255),DebitAmt decimal(18, 2),
					 CreditAmt Decimal(18,2),LocalDebitAmt Decimal(18,2),LocalCreditAmt Decimal(18,2),Currency varchar(10),Module varchar(10),Narration varchar(1024),)
					declare @VDate datetime
					declare @VMiti varchar(10)
					declare @VNo varchar(50)
					declare @Source1 varchar(10)
					declare @GlCode  varchar(100)
					declare @DrAmt decimal(18, 2)
					declare @CrAmt decimal(18, 2)
					declare @SourceType varchar(100)
					declare @I int
					IF @All = 0
					BEGIN
					 set @Source = 'JV'
					END
					ELSE
					BEGIN
					 set @Source = 'JV,DN,CN,PB,PAB,PIB,PR,PEB,SB,SBT,SA,SIB,SR,SEB,HBN,HBCN,HCI,HCO'--all station but Not In(CB,RV, PV, CP, RPV)
					END
					DECLARE c1 INSENSITIVE CURSOR FOR
					Select Distinct Voucher_Date from AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId = AT.Currency_Id where(AT.Branch_Id in (@BranchId)or @BranchId = '0') and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '')  and(AT.Currency_Id in (@Currency)or @Currency = '0')  and(@Find = '' or Voucher_No like '%' + @Find) and Voucher_Date between @From_Date and @To_Date order by Voucher_Date asc
					OPEN c1
					FETCH NEXT FROM c1
					INTO @VDate
					WHILE @@FETCH_STATUS = 0
					BEGIN
					  DECLARE c2 INSENSITIVE CURSOR FOR
					  select distinct Voucher_No,Module from  AMS.AccountDetails as AT Left Outer Join AMS.Currency as C On C.CId = AT.Currency_Id where(AT.Branch_Id in (@BranchId)or @BranchId = '0') and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '')  and(AT.Currency_Id in (@Currency)or @Currency = '0')  and(@Find = '' or Voucher_No like '%' + @Find) and Voucher_Date = @VDate
					  OPEN c2
					  FETCH NEXT FROM c2
					  INTO @VNo, @Source1
					  WHILE @@FETCH_STATUS = 0
					  BEGIN
						set @VMiti = (select top 1 Voucher_Miti from  AMS.AccountDetails where(Branch_ID in (@BranchId) or @BranchId= '0') and Voucher_Date = @VDate and Voucher_No = @VNo )
						if @Source1 = 'OB'
						  set @SourceType = 'Opening Balance'
						else if @Source1 = 'JV'
						  set @SourceType = 'Journal Voucher'
						else if @Source1 = 'RV'
						  set @SourceType = 'Receipt Voucher'
						else if @Source1 = 'PV'
						  set @SourceType = 'Payment Voucher'
						else if @Source1 = 'CV'
						  set @SourceType = 'Contra Voucher'
						else if @Source1 = 'DN'
						  set @SourceType = 'Debit Note'
						else if @Source1 = 'CN'
						  set @SourceType = 'Credit Note'
						else if @Source1 = 'PB'
						  set @SourceType = 'Purchase Invoice'
						else if @Source1 = 'PAB'
						  set @SourceType = 'Purchase Additional Invoice'
						else if @Source1 = 'PIB'
						  set @SourceType = 'Purchase Inter Branch'
						else if @Source1 = 'PR'
						  set @SourceType = 'Purchase Return Invoice'
						else if @Source1 = 'PEB'
						  set @SourceType = 'Purchase Ex/Br Return'
						else if @Source1 = 'SB'
						  set @SourceType = 'Sales Invoice'
						else if @Source1='SBT'
							set @SourceType = 'Ticket Sales'
						else if @Source1 = 'SAB'
						  set @SourceType = 'Sales Additional Invoice'
						else if @Source1 = 'SIB'
						  set @SourceType = 'Sales Inter Branch'
						else if @Source1 = 'SR'
						  set @SourceType = 'Sales Return Invoice'
						else if @Source1 = 'SEBR'
						  set @SourceType = 'Sale Ex/Br Return'
						else if @Source1 = 'HBN'
						  set @SourceType = 'Hotel Booking'
						else if @Source1 = 'HBCN'
						  set @SourceType = 'Hotel Booking Cancel'
						else if @Source1 = 'HCI'
						  set @SourceType = 'Hotel Check In'
						else if @Source1 = 'HCO'
						  set @SourceType = 'Hotel Check Out'
						else
							set @SourceType = 'Other'
					  if not exists(select VoucherDate from #temp where VoucherDate=CONVERT(varchar(10),@VDate,103) )
					  INSERT INTO #temp  SELECT  CONVERT(varchar(10),@VDate,103) ,@VMiti,@VNo,@SourceType,0,0,0,0,'' Currency,@Source1,'' Narration
					  else
						 INSERT INTO #temp  SELECT  '' ,'',@VNo,@SourceType,0,0,0,0,'' Currency ,@Source1,'' Narration
					  if @Narration = 1---- - Condition Of Remarks
					  begin
						 INSERT INTO #temp Select '','','',GL.GLName,Sum(AT.Debit_Amt) DrAmt,Sum(AT.Credit_Amt)CrAmt,Sum(AT.LocalDebit_Amt) LocalDrAmt,Sum(AT.LocalCredit_Amt)LocalCrAmt,ISNULL(C.Ccode,'')Cur_Code,AT.Ledger_ID Source,AT.Narration from  AMS.AccountDetails AS  AT
						 INNER JOIN AMS.GeneralLedger AS GL ON GL.GLID = AT.Ledger_ID Left Outer Join  AMS.Currency as C On C.CId = AT.Currency_ID
						 WHERE  AT.Voucher_No = @VNo  and AT.Module = @Source1
						 Group BY GL.GLName, AT.Module, C.Ccode, AT.Ledger_ID, GL.GLCode, AT.Narration
					  end
					  begin
						 INSERT INTO #temp Select '','','',GL.GLName,Sum(AT.Debit_Amt) DrAmt,Sum(AT.Credit_Amt)CrAmt,Sum(AT.LocalDebit_Amt) LocalDrAmt,Sum(AT.LocalCredit_Amt)LocalCrAmt,ISNULL(C.Ccode,'')Cur_Code,AT.Ledger_ID Source,'' Narration from  AMS.AccountDetails AS  AT
						 INNER JOIN GeneralLedger AS GL ON GL.GLID = AT.Ledger_ID Left Outer Join  AMS.Currency as C On C.CId = AT.Currency_ID
						 WHERE  AT.Voucher_No = @VNo  and AT.Module = @Source1
						 Group BY GL.GLName, AT.Module, C.Ccode, AT.Ledger_ID, GL.GLCode
					  end
					  if @Remarks = 1---- - Condition Of Remarks
					  begin
						 INSERT INTO #temp select top 1 '','','Remarks :',AT.Remarks,0,0,0,0,'' Currency,AT.Ledger_ID Station,'' Narration from  AMS.AccountDetails AS  AT
					  INNER JOIN GeneralLedger AS GL ON GL.GLID = AT.Ledger_ID WHERE AT.Voucher_No = @VNo  and AT.Module = @Source1 and(AT.Remarks <> '' and AT.Remarks is not null)
					  end
					  INSERT INTO #temp  SELECT  '' ,'','','Voucher Total -->>',
					 (select SUM(Debit_Amt) from AMS.AccountDetails as AT Left Outer Join  Currency as C On C.CId = AT.Currency_ID  where Voucher_No = @VNo and AT.Module = @Source1 and(AT.Currency_ID in (@Currency)or @Currency = '0')) DrAmt,
					 (select SUM(Credit_Amt) from AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID  where Voucher_No = @VNo and AT.Module = @Source1 and(AT.Currency_ID in (@Currency)or @Currency = '0')) CrAmt,
					 (select SUM(LocalDebit_Amt) from AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID  where Voucher_No = @VNo and AT.Module = @Source1 and(AT.Currency_ID in (@Currency)or @Currency = '0')) LocalDrAmt,
					 (select SUM(LocalCredit_Amt) from AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID  where Voucher_No = @VNo and AT.Module = @Source1 and(AT.Currency_ID in (@Currency)or @Currency = '0')) LocalCrAmt,
					 '' Currency,'' Module,'' Narration
					FETCH NEXT FROM c2
					INTO   @VNo,@Source1
					  END
					DEALLOCATE c2
					INSERT INTO #temp  SELECT  '' ,'','','Day Total -->>',
					(select SUM(Debit_Amt) from  AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID where Voucher_Date = @VDate and(AT.Currency_ID in (@Currency)or @Currency = '0') and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '') ) DrAmt,
					(select SUM(Credit_Amt) from AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID where Voucher_Date = @VDate and(AT.Currency_ID in (@Currency)or @Currency = '0') and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '') ) CrAmt,
					(select SUM(LocalDebit_Amt) from AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID where Voucher_Date = @VDate and(AT.Currency_ID in (@Currency)or @Currency = '0') and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '') ) LocalDrAmt,
					(select SUM(LocalCredit_Amt) from AMS.AccountDetails as AT Left Outer Join Currency as C On C.CId = AT.Currency_ID where Voucher_Date = @VDate and(AT.Currency_ID in (@Currency)or @Currency = '0') and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '') ) LocalCrAmt,
					'' Currency,'' Module,'' Narration
					  FETCH NEXT FROM c1
					  INTO   @VDate
					END
					DEALLOCATE c1
					Insert into #temp
					Select '' as VDate, '' VMiti,'' VNo,'Grand Total -->>' as Particulars,
					sum(isnull(Debit_Amt, 0)) CurrDrAmt,sum(isnull(Credit_Amt, 0)) CurrCrAmt,
					sum(isnull(LocalDebit_Amt, 0)) LocalDrAmt,sum(isnull(LocalCredit_Amt, 0)) LocalCrAmt,
					'' Currency,'' Source,'' Narration from AMS.AccountDetails as AT Inner Join GeneralLedger as GL On GL.GLID = AT.Ledger_ID Where(AT.Branch_ID in (@BranchId)or @BranchId = '0')
					and(Module IN(SELECT Value FROM fn_Splitstring(@Source, ',')) or @Source = '')
					and Voucher_Date between @From_Date and @To_Date
					select* from #temp order By Id
					drop table #temp ;
GO
/****** Object:  StoredProcedure [AMS].[Usp_Select_LedgerStatement]    Script Date: 06/08/2020 10:12:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE Procedure [AMS].[Usp_Select_LedgerStatement]
					(
					@EVENT INT,
					@FromDate datetime ,
					@ToDate datetime ,
					@GlCode Varchar(max)='',
					@SlCode  varchar(MAX)='',
					@AgentCode Varchar(MAX)='',
					@AccountType varchar(100)='' ,
					@Currency varchar(20)='' ,
					@Find varchar(20)=''  ,
					@SubLedger bit=0 ,
					@IsRemarks bit=0 ,
					@IsNarration bit=0 ,
					@Posting_Details bit=0,
					@Product_Details bit=0,
					@DNCN_Details bit=0,
					@Include_UDF bit=0,
					@Include_PDC bit=0,
					@All bit=0 ,
					@BranchCode varchar(100)=''
					)
					as
					--AMS.Usp_Select_LedgerStatement 1,'2017/07/16','2018/07/16','','','','','','',0,0,0,0,0,0,0,0,0,''
					--go
					--AMS.Usp_Select_LedgerStatement 2,'2017/07/16','2018/07/16','','','','','','',0,1,1,0,1,0,0,0,0,''
					IF @EVENT=1  ----Ledger Summary
					BEGIN
					Create table #temp (Id bigInt IDENTITY(1,1) NOT NULL, GlId varchar(50),GlCode varchar(50),GlName varchar(max),OpDebitAmt decimal(18,2),OpCreditAmt decimal(18,2),DebitAmt decimal(18,2),CreditAmt decimal(18,2),Balance decimal(18,2),ClDebitAmt decimal(18,2), ClCreditAmt decimal(18,2) )
					insert into #temp
					Select GlId,GLCode,GlName,ISNULL(sum(isnull(Convert(decimal(18,2),OpDebitAmt),0)),0)  OpDebitAmt,ISNULL(sum(isnull(Convert(decimal(18,2),OpCreditAmt),0)),0)  OpCreditAmt ,
					ISNULL(sum(isnull(Convert(decimal(18,2),DebitAmt),0)),0)  DebitAmt,ISNULL(sum(isnull(Convert(decimal(18,2),CreditAmt),0)),0)  CreditAmt,ISNULL(sum(isnull(Convert(decimal(18,2),Balance),0)),0)  Balance ,
					ISNULL(Case When ISNULL(sum(isnull(ClDebitAmt,0)),0)-ISNULL(sum(isnull(ClCreditAmt,0)),0)>0 Then ISNULL(sum(isnull(Convert(decimal(18,2),ClDebitAmt),0)),0)-ISNULL(sum(isnull(Convert(decimal(18,2),ClCreditAmt),0)),0) End,0) as ClDebitAmt ,
					ISNULL(Case When ISNULL(sum(isnull(ClDebitAmt,0)),0)-ISNULL(sum(isnull(ClCreditAmt,0)),0)<0 Then Abs(ISNULL(sum(isnull(Convert(decimal(18,2),ClCreditAmt),0)),0)-ISNULL(sum(isnull(Convert(decimal(18,2),ClDebitAmt),0)),0)) End,0) as ClCreditAmt
					from (
					Select Gl.GlId,Gl.GLCode, GlName, ISNULL(sum(isnull(LocalDebit_Amt,0)),0)OpDebitAmt, ISNULL(sum(isnull(LocalCredit_Amt,0)),0) as OpCreditAmt,
					0 DebitAmt,0 CreditAmt,0 Balance ,Case When ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0)>0 Then ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0) End as ClDebitAmt ,
					Case When ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0)<0 Then Abs(ISNULL(sum(isnull(LocalCredit_Amt,0)),0)-ISNULL(sum(isnull(LocalDebit_Amt,0)),0)) End as ClCreditAmt
					From AMS.AccountDetails AS AT  inner join AMS.GeneralLedger GL on GL.GLID=AT.Ledger_ID
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and (AT.Ledger_ID IN (SELECT Value FROM fn_Splitstring(@GlCode, ',')) or @GlCode='') AND AT.Voucher_Date < @FromDate
					and (GL.GLType IN (SELECT Value FROM AMS.fn_Splitstring(@AccountType, ',')) or @AccountType='')    and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')
					Group by  Gl.GlId, GL.GlName,Gl.GlCode
					Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0
					Union All
					Select Gl.GlId,Gl.GLCode, GlName,0 OpDebitAmt,0 OpCreditAmt,sum(isnull(LocalDebit_Amt,0))DebitAmt, sum(isnull(LocalCredit_Amt,0)) as CreditAmt,sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) as Balance ,
					Case When ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0)>0 Then ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0) End as ClDebitAmt ,
					Case When ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0)<0 Then Abs(ISNULL(sum(isnull(LocalCredit_Amt,0)),0)-ISNULL(sum(isnull(LocalDebit_Amt,0)),0)) End as ClCreditAmt
					From AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger GL on GL.GLID=AT.Ledger_ID
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and (AT.Ledger_ID IN (SELECT Value FROM AMS.fn_Splitstring(@GlCode, ',')) or @GlCode='') AND AT.Voucher_Date between @FromDate and @ToDate
					and (GL.GLType IN (SELECT Value FROM AMS.fn_Splitstring(@AccountType, ',')) or @AccountType='')   and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')
					Group by  Gl.GlId, GL.GlName,Gl.GlCode
					Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0
					) as aa Group By GLID,GLCode,GlName
					Order By GlName
					insert into #temp
					Select '' GlId,'' GLCode,'Grand Total -->>' GlName,Sum(IsNull(OpDebitAmt,0)) OpDebitAmt,Sum(IsNull(OpCreditAmt,0)) OpCreditAmt,Sum(IsNull(DebitAmt,0)) DebitAmt, Sum(IsNull(CreditAmt,0)) CreditAmt,Sum(IsNull(Balance,0)) Balance,Sum(IsNull(ClDebitAmt,0)) ClDebitAmt,Sum(IsNull(ClCreditAmt,0)) ClCreditAmt from #temp
					insert into #temp
					Select '' GlId,'' GLCode,'Closing Total -->>' GlName,0,0,0,0,0,Case When ISNULL(sum(isnull(ClDebitAmt,0)),0)-ISNULL(sum(isnull(ClCreditAmt,0)),0)>0 Then ISNULL(sum(isnull(Convert(decimal(18,2),ClDebitAmt),0)),0)-ISNULL(sum(isnull(Convert(decimal(18,2),ClCreditAmt),0)),0) End as ClDebitAmt ,
					ISNULL(Case When ISNULL(sum(isnull(ClDebitAmt,0)),0)-ISNULL(sum(isnull(ClCreditAmt,0)),0)<0 Then Abs(ISNULL(sum(isnull(Convert(decimal(18,2),ClCreditAmt),0)),0)-ISNULL(sum(isnull(Convert(decimal(18,2),ClDebitAmt),0)),0)) End,0) as ClCreditAmt  from #temp where GlName='Grand Total -->>'
					Select * from #temp order by id
					End
					Else
					IF @EVENT=2  ---- Ledger Details
					BEGIN
					Declare @Sql varchar(max)
					DECLARE @Voucher_No varchar(50)
                    DECLARE @Ref_VNo varchar(50)
					DECLARE @Ledger_Name2 varchar(MAX)
					DECLARE @STRLedger_Name2 varchar(255)
					DECLARE @DAmt decimal(18,4)
					DECLARE @CAmt decimal(18,4)
					DECLARE @Ac_TranId BIGINT
					DECLARE @I INT
					DECLARE @Ii INT
					DECLARE @LEDGER_NAME1 VARCHAR(255)
					DECLARE @Ledger_Code1 VARCHAR(50)
					DECLARE @Station varchar(5)
					declare @Cheque_No varchar(50)
					set @Ii=0
					DECLARE @Udf_Id BIGINT
					DECLARE @Column_Name varchar(250)
					DECLARE @Data_Type varchar(50)
					DECLARE @Size varchar(50)
					DECLARE @Transaction_Data varchar(MAX)
					DECLARE @Remarks1 varchar(MAX)
					DECLARE @Narration1 varchar(MAX)
					DECLARE @FLedger_Code VARCHAR(50)
					DECLARE @FLEDGER_NAME VARCHAR(50)
					DECLARE @FSubLedger_Id INT
					DECLARE @FSubLedger_NAME VARCHAR(255)
					DECLARE @fVN VARCHAR(255)
					DECLARE @fVId bigint
					DECLARE @LocalCrAmt DECIMAL(18,2)
					DECLARE @LocalDrAmt DECIMAL(18,2)
					DECLARE @Balance DECIMAL(18,2)
					Create table #Ttemp2 (Id bigInt IDENTITY(1,1) NOT NULL,VoucherDate Date,VoucherMiti varchar(10),Details varchar(max),Currency VARCHAR(50),CurRate DECIMAL(18,2),DebitAmt decimal(18,2), CreditAmt decimal(18,2),LocalDrAmt decimal(18,2), LocalCrAmt decimal(18,2),Balance decimal(18,2), Module varchar(50), GlId varchar(50),VoucherNo varchar(50),Narration varchar(max)  )
					--If (@Include_Udf=1)
					--Begin
					--	Declare c0 Cursor For
					--	Select Distinct U.Udf_Id, Column_Name,Data_Type,Size from UDF as U Inner Join UDF_Transaction as UT On UT.UDF_Id=U.Udf_Id
					--	Where U.Status=1
					--	and UT.VNo in (
					--		Select DISTINCT AT.VNo from AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger L on L.GlCode=AT.GlCode
					--		Where (AT.GlCode IN (SELECT Value FROM fn_Splitstring(@GlCode, ',')) or @GlCode='') and (L.[Type] IN (SELECT Value FROM fn_Splitstring(@AccountType, ',')) or @AccountType='')
					--		and  AT.VDate<=@ToDate and  )
					--	Order By U.Udf_Id
					--	set @I=1
					--	Open c0
					--	Fetch Next From c0 Into @Udf_Id,@Column_Name,@Data_Type,@Size
					--	while @@FETCH_STATUS =0
					--	Begin
					--		Exec('Alter Table #Ttemp2 Add [' + @Column_Name + '-'+ @I +']  Nvarchar(555)')
					--		Set @I = @I+1
					--	Fetch Next From c0 Into @Udf_Id,@Column_Name,@Data_Type,@Size
					--	end
					--	CLOSE c0
					--	DEALLOCATE c0
					--End
					DECLARE c1 INSENSITIVE CURSOR FOR
					Select DISTINCT AT.Ledger_Id,GlName from AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger GL on GL.GlId=AT.Ledger_Id
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and (AT.Ledger_Id IN (SELECT Value FROM AMS.fn_Splitstring(@GlCode, ',')) or @GlCode='') and (GL.GlType IN (SELECT Value FROM AMS.fn_Splitstring(@AccountType, ',')) or @AccountType='') and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')  and  AT.Voucher_Date<=@ToDate
					Order by GLName
					OPEN c1
					FETCH NEXT FROM c1
					INTO @Ledger_Code1,@LEDGER_NAME1
					WHILE @@FETCH_STATUS = 0
					BEGIN

					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate, DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt, Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti,'A/c : ' + @LEDGER_NAME1 as Details,'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,'' Station,@Ledger_Code1 Ledger_Code,'Account Head' Voucher_No
					Union all
					Select NULL as VDate, ''  as VMiti,'Opening Balance ' as Details,'' Currency,0 CurRate,
					Case When ISNULL(sum(isnull(Debit_Amt,0)),0)-ISNULL(sum(isnull(Credit_Amt,0)),0)>0 Then ISNULL(sum(isnull(Debit_Amt,0)),0)-ISNULL(sum(isnull(Credit_Amt,0)),0) else 0 End as DebitAmt ,Case When ISNULL(sum(isnull(Debit_Amt,0)),0)-ISNULL(sum(isnull(Credit_Amt,0)),0)<0 Then Abs(ISNULL(sum(isnull(Credit_Amt,0)),0)-ISNULL(sum(isnull(Debit_Amt,0)),0)) else 0 End as CreditAmt,
					Case When ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0)>0 Then ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0) else 0 End as LocalDrAmt ,Case When ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0)<0 Then Abs(ISNULL(sum(isnull(LocalCredit_Amt,0)),0)-ISNULL(sum(isnull(LocalDebit_Amt,0)),0)) else 0 End as LocalCrAmt,ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0) as Balance
					,'' Station,'' Ledger_Code,'' Voucher_No from AMS.AccountDetails AS AT  inner join AMS.GeneralLedger GL on GL.GLID=AT.Ledger_ID
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and   AT.Ledger_ID IN (@Ledger_Code1) and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0') and  AT.Voucher_Date < @FromDate
					Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0
					set @Balance=ISNULL((Select ISNULL(sum(isnull(LocalDebit_Amt,0)),0)-ISNULL(sum(isnull(LocalCredit_Amt,0)),0) as Balance from AMS.AccountDetails AS AT  inner join AMS.GeneralLedger GL on GL.GLID=AT.Ledger_ID
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and   AT.Ledger_ID IN (@Ledger_Code1) and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0') and  AT.Voucher_Date < @FromDate
					Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0),0)
					Declare @TVoucher_Date datetime
					DECLARE c2 INSENSITIVE CURSOR FOR
					SELECT DISTINCT Voucher_No,Voucher_Date,Module,RefNo FROM AMS.AccountDetails as AT where Ledger_Id=@Ledger_Code1 and ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0') and Voucher_Date between @FromDate and @ToDate
					order BY Voucher_Date asc
					OPEN c2
					FETCH NEXT FROM c2
					INTO @Voucher_No,@TVoucher_Date,@Station,@Ref_VNo
					WHILE @@FETCH_STATUS = 0
					BEGIN
					 SET @STRLedger_Name2=''
					 SET @Ledger_Name2=''
					If(@Station = 'OB')
					 SET @Ledger_Name2 = 'Opening Balance @: '
                    else If(@Station = 'N')
					 SET @Ledger_Name2 = 'Opening Balance @: ' + @Voucher_No
					ELSE If  (@Station='JV')
					 SET @Ledger_Name2  = 'Journal Voucher @: ' + @Voucher_No
					else If  (@Station='CB')
					 SET @Ledger_Name2  = 'Cash/Bank Voucher @: ' + @Voucher_No
					else If  (@Station='RV')
					 SET @Ledger_Name2  = 'Receipt Voucher @: ' + @Voucher_No
					else If  (@Station='PV')
					 SET @Ledger_Name2  = 'Payment Voucher @: ' + @Voucher_No
					else If  (@Station='CV')
					 SET @Ledger_Name2  = 'Contra Voucher @: ' + @Voucher_No
					else If  (@Station='RPV')
					 SET @Ledger_Name2  = 'Rec/Pay Voucher @: ' + @Voucher_No
					else If  (@Station='DN')
					 SET @Ledger_Name2  = 'Debit Note Voucher @: ' + @Voucher_No
					else If  (@Station='CN')
					 SET @Ledger_Name2  = 'Credit Note Voucher @: ' + @Voucher_No
					else If  (@Station='SB')
					 SET @Ledger_Name2  = 'Sales Bill @:' + @Voucher_No
					else if @Station='SBT'
						set @Ledger_Name2 = 'Ticket Sales @:' + @Voucher_No
					else if @Station='PBT'
						set @Ledger_Name2 = 'Ticket Purchase @:' + @Voucher_No
					else If  (@Station='SAB')
					 SET @Ledger_Name2  = 'Sales Additional Bill @:' + @Voucher_No
					else If  (@Station='SR')
					 SET @Ledger_Name2  = 'Sales Return Bill @: ' + @Voucher_No
					else If  (@Station='SEB')
					 SET @Ledger_Name2  = 'Sales Expiry/Breakage Bill @: ' + @Voucher_No
					else If  (@Station='PB')
					begin
						if (@Ref_VNo<>'')
						begin
							SET @Ledger_Name2  = 'Purchase Bill @: ' + @Voucher_No + ' :: '  + 'Ref No @: ' + @Ref_VNo
						End
						else
							SET @Ledger_Name2  = 'Purchase Bill @: ' + @Voucher_No						
					end
					else If  (@Station='PAB')
					begin
						if (@Ref_VNo<>'')
						begin
							SET @Ledger_Name2  = 'Purchase Additional Bill @: ' + @Voucher_No + ' :: '  + 'Ref No @: ' + @Ref_VNo
						End
						else
							SET @Ledger_Name2  = 'Purchase Additional Bill @:' + @Voucher_No						
					end	
					else If  (@Station='PR')
					 SET @Ledger_Name2  = 'Purchase Return Bill @: ' + @Voucher_No
					else If  (@Station='PEB')
					 SET @Ledger_Name2  = 'Purchase Expiry/Breakage Bill @: ' + @Voucher_No
					set @Balance = @Balance + ISNULL((Select sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) as Balance  From AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger GL on GL.GLID=AT.Ledger_ID
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and AT.Ledger_ID=@Ledger_Code1 and AT.Voucher_No=@Voucher_No and AT.Voucher_Date=@TVoucher_Date and AT.Module=@Station   and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')
					Group by AT.Voucher_Date,AT.Voucher_Miti,AT.Module,AT.Ledger_ID,AT.Cheque_No,AT.Voucher_No
					Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0),0)
					If (@IsNarration=1)
					Begin
					 Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate, DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo,Narration)
					 Select  AT.Voucher_Date as VDate,  AT.Voucher_Miti as VMiti, '   '+@Ledger_Name2   AS  Details,C.Ccode,AT.Currency_Rate,
                     sum(isnull(Debit_Amt,0))DebitAmt, sum(isnull(Credit_Amt,0)) as CreditAmt,sum(isnull(LocalDebit_Amt,0)) LocalDrAmt, sum(isnull(LocalCredit_Amt,0)) as LocalCrAmt,
					 @Balance as Balance, AT.Module,AT.Ledger_ID,AT.Voucher_No,AT.Narration From AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger L on L.GLID=AT.Ledger_ID LEFT OUTER JOIN AMS.Currency C ON C.CId=AT.Currency_ID
					 Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and AT.Ledger_ID=@Ledger_Code1 and AT.Voucher_No=@Voucher_No and AT.Voucher_Date=@TVoucher_Date and AT.Module=@Station  and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')
					 Group by AT.Voucher_Date,AT.Voucher_Miti,AT.Module,AT.Ledger_ID,AT.Cheque_No,AT.Voucher_No,AT.Narration,C.Ccode,AT.Currency_Rate
					 Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0  ORDER BY AT.Voucher_Date ASC
					End Else
					Begin
					 Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate, DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo,Narration)
					 Select  AT.Voucher_Date as VDate,  AT.Voucher_Miti as VMiti, '   '+@Ledger_Name2   AS  Details,C.Ccode,AT.Currency_Rate,  
                     sum(isnull(Debit_Amt,0))DebitAmt, sum(isnull(Credit_Amt,0)) as CreditAmt,sum(isnull(LocalDebit_Amt,0)) LocalDrAmt, sum(isnull(LocalCredit_Amt,0)) as LocalCrAmt,
					 @Balance as Balance,AT.Module,AT.Ledger_ID,AT.Voucher_No,'' Narration From AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger L on L.GLID=AT.Ledger_ID LEFT OUTER JOIN AMS.Currency C ON C.CId=AT.Currency_ID
					 Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and AT.Ledger_ID=@Ledger_Code1 and AT.Voucher_No=@Voucher_No and AT.Voucher_Date=@TVoucher_Date and AT.Module=@Station   and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')
					 Group by AT.Voucher_Date,AT.Voucher_Miti,AT.Module,AT.Ledger_ID,AT.Cheque_No,AT.Voucher_No,C.Ccode,AT.Currency_Rate
					 Having	SUM(LocalDebit_Amt+LocalCredit_Amt)<>0  ORDER BY AT.Voucher_Date ASC
					End

					If  (@Station='CB')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate, DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select top 1 NULL as VoucherDate, ''  as VoucherMiti,+ SPACE(8) + '  Chq No : ' + Cheque_No + '   Chq Date : ' + IsNull(CONVERT(varchar(10), Cheque_Date,103),'') as Details ,'' Currency,0 CurRate, 0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,@Ledger_Code1 Ledger_Code,'Cheque Details' Voucher_No From AMS.AccountDetails as AT
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and AT.Ledger_ID=@Ledger_Code1 and AT.Voucher_No=@Voucher_No and AT.Voucher_Date=@TVoucher_Date and AT.Module=@Station   and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0')
					and (Cheque_No is not null and Cheque_No<>'')
					End
					--If (@Include_Udf=1)
					--Begin
					--	Declare cudfdt Cursor For
					--	Select Distinct U.Udf_Id, Column_Name,Data_Type,Size from UDF as U Inner Join UDFTransaction as UT On UT.UDF_Id=U.Udf_Id
					--	Where U.Status=1
					--	and UT.Invoice_No in (
					--		Select DISTINCT AT.VNo from AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger L on L.GlCode=AT.GlCode
					--		Where (AT.GlCode IN (SELECT Value FROM fn_Splitstring(@GlCode, ',')) or @GlCode='') and (L.Catagory IN (SELECT Value FROM AMS.fn_Splitstring(@AccountType, ',')) or @AccountType='')
					--		and  AT.VDate<=@ToDate  )
					--	Order By U.Udf_Id
					--	set @I=1
					--	Open cudfdt
					--	Fetch Next From cudfdt Into @Udf_Id,@Column_Name,@Data_Type,@Size
					--	while @@FETCH_STATUS =0
					--	Begin
					--		SET @Transaction_Data  =''
					--		DECLARE c3 INSENSITIVE CURSOR FOR
					--		Select Distinct Transaction_Data from UDF as U Inner Join UDF_Transaction as UT On UT.UDF_Id=U.Udf_Id
					--							Where U.Udf_Id=@Udf_Id and U.Status=1 and Invoice_No = @Voucher_No and Station= @Station
					--		OPEN c3
					--		FETCH NEXT FROM c3
					--		INTO @Transaction_Data
					--		WHILE @@FETCH_STATUS = 0
					--		BEGIN
					--			if (@Transaction_Data  ='')
					--				SET @Transaction_Data  = (SELECT CONVERT(varchar(MAX),@Transaction_Data) +' ,'+ CONVERT(varchar(MAX),@Transaction_Data))
					--			else
					--				SET @Transaction_Data  = (SELECT CONVERT(varchar(MAX),@Transaction_Data))
					--		FETCH NEXT FROM c3
					--		INTO   @Transaction_Data
					--		END
					--		DEALLOCATE c3
					--		select @Sql = 'Update #Ttemp2 Set [' + @Column_Name + '-'+ cast(@I as varchar(5)) +'] = ''' + cast( @Transaction_Data as varchar(MAX)) + ''' Where Voucher_No =''' +  cast(@Voucher_No as varchar(255)) + ''' and Station =''' +  cast(@Station as varchar(255)) + ''''
					--		exec (@Sql)
					--		Set @I = @I+1

					--	Fetch Next From cudfdt Into @Udf_Id,@Column_Name,@Data_Type,@Size
					--	end
					--	CLOSE cudfdt
					--	DEALLOCATE cudfdt
					--End
					------Product Details---------
					If (@Product_Details=1)
					Begin
					If  (@Station='SB')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti, + SPACE(8) + isnull(PName,'') + Case When len(left(isnull(PName,''),70))>50 then SPACE(len(left(Isnull(PName,''),70))-50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + ' ' + convert(varchar, convert(decimal(18,3),IsNull(SID.Qty,0))) + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.Rate,0))) + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.B_Amount,0))) as Details,
					'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,SIM.Customer_ID,'Product Details' Voucher_No From AMS.SB_Details as SID Inner Join AMS.SB_Master as SIM ON SIM.SB_Invoice=SID.SB_Invoice Inner Join AMS.Product as P On P.PID=SID.P_Id
					Where  SIM.SB_Invoice=@Voucher_No and  SIM.Invoice_Date=@TVoucher_Date
					End
					If  (@Station='SBT')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti,
					+ SPACE(8) + ' Cust : ' + Isnull(Sl.SlName,'') + ' Airlines : ' + Isnull(PG.GrpName,'')+ ' Tkt No: ' + Isnull(P.PName,'')
					+ ' Pax: ' + convert(varchar, convert(decimal(18,0),IsNull(SID.Qty,0))) + ' Fl Dt : ' + Convert(varchar(10), Flight_date, 103) + ' Sct: ' + Isnull(Sector,'')
					--+ SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.Fare_Amount,0))) + + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.FC_Amount,0))) + + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.PSC_Amount,0))) + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.B_Amount,0)))
					as Details,
					'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,SIM.Customer_ID,'Product Details' Voucher_No
					from AMS.SBT_Details AS SID	Inner Join AMS.SBT_Master AS SIM on SID.SBT_Invoice = SIM.SBT_Invoice Left Outer Join AMS.GeneralLedger AS GL on SIM.Customer_Id = GL.GlId
					Left Outer Join AMS.Subledger AS Sl on SID.Slb_Id = Sl.SlId left outer join AMS.ProductGroup as PG on SID.PGrp_Id = PG.PGrpId Left Outer Join AMS.Product AS P on SID.P_Id = P.PId
					Where  SIM.SBT_Invoice=@Voucher_No and  SIM.Invoice_Date=@TVoucher_Date
					End
					If  (@Station='SR')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti, + SPACE(8) + PName + Case When len(left(PName,70))>50 then SPACE(len(left(PName,70))-50) else	SPACE(50-len(left(PName,70))) end + ' ' + convert(varchar, convert(decimal(18,3),SRD.Qty)) + SPACE(5) + convert(varchar,convert(decimal(18,2),SRD.Rate)) + SPACE(5) + convert(varchar,convert(decimal(18,2),SRD.B_Amount)) as Details,
					'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,SRM.Customer_ID,'Product Details' Voucher_No From AMS.SR_Details as SRD Inner Join AMS.SR_Master as SRM ON SRM.SR_Invoice=SRD.SR_Invoice Inner Join AMS.Product as P On P.PID=SRD.P_Id
					Where  SRM.SR_Invoice=@Voucher_No and  SRM.Invoice_Date=@TVoucher_Date
					End
					--If  (@Station='SEB')
					--Begin
					--Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					--Select NULL as VDate, ''  as VMiti, + SPACE(8) + PName + Case When len(left(PName,70))>50 then SPACE(len(left(PName,70))-50) else	SPACE(50-len(left(PName,70))) end + ' ' + convert(varchar, convert(decimal(18,3),SEBD.Qty)) + SPACE(5) + convert(varchar,convert(decimal(18,2),SEBD.Rate)) + SPACE(5) + convert(varchar,convert(decimal(18,2),SEBD.B_Amount)) as Details,
					--'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,SEBM.Customer_ID,'Product Details' Voucher_No From AMS.SEB_Details as SEBD Inner Join AMS.SEB_Master as SEBM ON SEBM.SEB_Invoice=SEBD.SEB_Invoice Inner Join AMS.Product as P On P.PID=SEBD.P_Id
					--Where  SEBM.SEB_Invoice=@Voucher_No and  SEBM.Invoice_Date=@TVoucher_Date
					--End
					If  (@Station='PB')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti, + SPACE(8) + PName + Case When len(left(PName,70))>50 then SPACE(len(left(PName,70))-50) else	SPACE(50-len(left(PName,70))) end + ' ' + convert(varchar, convert(decimal(18,3),PID.Qty)) + SPACE(5) + convert(varchar,convert(decimal(18,2),PID.Rate)) + SPACE(5) + convert(varchar,convert(decimal(18,2),PID.B_Amount)) as Details,
					'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,PIM.Vendor_ID,'Product Details' Voucher_No From AMS.PB_Details as PID Inner Join AMS.PB_Master as PIM ON PIM.PB_Invoice=PID.PB_Invoice Inner Join Product as P On P.PID=PID.P_Id
					Where  PIM.PB_Invoice=@Voucher_No and  PIM.Invoice_Date=@TVoucher_Date
					End
					If  (@Station='PBT')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti,
					+ SPACE(8) + ' Cust : ' + Isnull(Sl.SlName,'') + ' Airlines : ' + Isnull(PG.GrpName,'')+ ' Tkt No: ' + Isnull(P.PName,'')
					+ ' Pax: ' + convert(varchar, convert(decimal(18,0),IsNull(SID.Qty,0))) + ' Fl Dt : ' + Convert(varchar(10), Flight_date, 103) + ' Sct: ' + Isnull(Sector,'')
					--+ SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.Fare_Amount,0))) + + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.FC_Amount,0))) + + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.PSC_Amount,0))) + SPACE(5) + convert(varchar,convert(decimal(18,2),IsNull(SID.B_Amount,0)))
					as Details,
					'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,SIM.Vendor_Id,'Product Details' Voucher_No
					from AMS.PBT_Details AS SID	Inner Join AMS.PBT_Master AS SIM on SID.PBT_Invoice = SIM.PBT_Invoice Left Outer Join AMS.GeneralLedger AS GL on SIM.Vendor_Id = GL.GlId
					Left Outer Join AMS.Subledger AS Sl on SID.Slb_Id = Sl.SlId left outer join AMS.ProductGroup as PG on SID.PGrp_Id = PG.PGrpId Left Outer Join AMS.Product AS P on SID.P_Id = P.PId
					Where  SIM.PBT_Invoice=@Voucher_No and  SIM.Invoice_Date=@TVoucher_Date
					End
					If  (@Station='PR')
					Begin
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti, + SPACE(8) + PName + Case When len(left(PName,70))>50 then SPACE(len(left(PName,70))-50) else	SPACE(50-len(left(PName,70))) end + ' ' + convert(varchar, convert(decimal(18,3),PRD.Qty)) + SPACE(5) + convert(varchar,convert(decimal(18,2),PRD.Rate)) + SPACE(5) + convert(varchar,convert(decimal(18,2),PRD.B_Amount)) as Details,
					'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,PRM.Vendor_ID,'Product Details' Voucher_No From AMS.PR_Details as PRD Inner Join AMS.PR_Master as PRM ON PRM.PR_Invoice=PRD.PR_Invoice  Inner Join AMS.Product as P On P.PID=PRD.P_Id
					Where  PRM.PR_Invoice=@Voucher_No and  PRM.Invoice_Date=@TVoucher_Date
					End
					--If  (@Station='PEB')
					--Begin
					--	Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					--	Select NULL as Date, ''  as VMiti, + SPACE(8) + PName + Case When len(left(PName,70))>50 then SPACE(len(left(PName,70))-50) else	SPACE(50-len(left(PName,70))) end + ' ' + convert(varchar, convert(decimal(18,3),PEBD.Qty)) + SPACE(5) + convert(varchar,convert(decimal(18,2),PEBD.Rate)) + SPACE(5) + convert(varchar,convert(decimal(18,2),PEBD.B_Amount)) as Details,
					--	'' Currency,0 CurRate,0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt, 0 as LocalCrAmt,0 as Balance,@Station Station,PEBM.GlCode,'Product Details' Voucher_No From AMS.PEB_Details as PRD Inner Join AMS.PEB_Master as PRM as PEBM ON PEBM.PEB_Invoice=PEBD.PEB_Invoice  Inner Join AMS.Product as P On P.PID=PEBD.P_Id
					--	Where  PEBM.PEB_Invoice=@Voucher_No and  PEBM.Invoice_Date=@TVoucher_Date
					--End
					End
					-----------End------------
					-----------Remarks------------
					If (@IsRemarks=1)
					Begin
					--Set @Narration1=''
					--If (@Station='JV' or @Station='RV' or @Station='PV' or @Station='RPV' or @Station='CV' or @Station='DN' or @Station='CN')
					--Begin
					--Set @Narration1 =(Select top 1 Narration from AMS.AccountDetails Where  VNo=@Voucher_No and Source=@Station and GlCode=@Ledger_Code1 and (Narration is not null or Narration<>''))
					--if (@Narration1<>'')
					--begin
					--Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,Balance,Module,GlId,VoucherNo)
					--Select top 1 NULL as Date, ''  as BsDate,'     Narr : ' + @Narration1 as Details ,'' Currency,0 CurRate, 0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt,0 LocalCrAmt,0 as Balance,@Station Station,@Ledger_Code1 Ledger_Code,@Voucher_No Voucher_No
					--End
					--End
					 Set @Remarks1=''
					 Set @Remarks1 =(Select Top 1 Remarks from AMS.AccountDetails Where Voucher_No=@Voucher_No and Module=@Station and Ledger_ID=@Ledger_Code1 and (Remarks is not null and Remarks<>''))
					if (@Remarks1<>'')
					begin
					 Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					 Select NULL as Date, 'Rem : '  as BsDate,@Remarks1 as Details,'' Currency,0 CurRate, 0 DebitAmt, 0 as CreditAmt,0 LocalDrAmt,0 LocalCrAmt,0 as Balance,@Station Station,@Ledger_Code1 Ledger_Code,@Voucher_No Voucher_No
					End
					End
					----------End-------------
					FETCH NEXT FROM c2
					INTO   @Voucher_No,@TVoucher_Date,@Station,@Ref_VNo
					END
					DEALLOCATE c2
					set @Balance=0
					Insert INTO #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select NULL as VDate, ''  as VMiti,'Ledger Total -->>','' Currency,0 CurRate,Sum(DebitAmt) DebitAmt,Sum(CreditAmt)CreditAmt,Sum(LocalDrAmt) LocalDrAmt,Sum(LocalCrAmt)LocalCrAmt, (Sum(LocalDrAmt) -Sum(LocalCrAmt)) Balance,'' Module,GlId,'' VoucherNo from  #Ttemp2
					Where GlId=@Ledger_Code1
					Group By GlId
					having (Sum(DebitAmt) -Sum(CreditAmt))<>0
					FETCH NEXT FROM c1
					INTO   @Ledger_Code1,@LEDGER_NAME1
					END
					DEALLOCATE c1
					insert into #Ttemp2	(VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select Null VDate,'' VMiti, 'Grand Period Total -->>' Details,'' Currency,0 CurRate,sum(isnull(Debit_Amt,0))DebitAmt, sum(isnull(Credit_Amt,0)) as CreditAmt,sum(isnull(LocalDebit_Amt,0))LocalDrAmt, sum(isnull(LocalCredit_Amt,0)) as LocalCrAmt,sum(isnull(LocalDebit_Amt,0))-sum(isnull(LocalCredit_Amt,0)) as Balance, '' Source,'' GlCode,'' VNo from AMS.AccountDetails  AS AT Inner join AMS.GeneralLedger L on L.GLID=AT.Ledger_ID
					Where ((@Include_PDC=1 and (''='' or Module='')) or (@Include_PDC=0 and Module<>'PDC')) and (AT.Ledger_ID IN (SELECT Value FROM AMS.fn_Splitstring(@GlCode, ',')) or @GlCode='') and (L.GLType IN (SELECT Value FROM fn_Splitstring(@AccountType, ',')) or @AccountType='') and (AT.Branch_Id IN (SELECT Value FROM AMS.fn_Splitstring(@BranchCode, ',')) or @BranchCode='0') and  AT.Voucher_Date Between @FromDate and @ToDate
					insert into #Ttemp2 (VoucherDate,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo)
					Select Null VDate,'' VMiti, 'Grand Total -->>' Details,'' Currency,0 CurRate,Sum(IsNull(DebitAmt,0)) DebitAmt, Sum(IsNull(CreditAmt,0)) CreditAmt,Sum(IsNull(LocalDrAmt,0)) LocalDrAmt, Sum(IsNull(LocalCrAmt,0)) LocalCrAmt,(Sum(LocalDrAmt) -Sum(LocalCrAmt)) Balance,'' Source,'' GlId,'' VoucherNo from #Ttemp2 where Details Not in ('Ledger Total -->>','Grand Period Total -->>')
					If (@Include_Udf=1)
					Select * from #Ttemp2  order by id
					else
					Select Id, CONVERT(varchar(10), VoucherDate,103) VoucherDate ,VoucherMiti,Details,Currency,CurRate,DebitAmt,CreditAmt,LocalDrAmt,LocalCrAmt,Balance,Module,GlId,VoucherNo,Narration from #Ttemp2  order by id
					drop table #Ttemp2
					End;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SO_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SO_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SO_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SQ_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SQ_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SQ_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SR_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SR_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SR_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
GO

