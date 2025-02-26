using MrDAL.Utility.Server;
using System;
using System.Data.SqlClient;

namespace MrDAL.Domains.MrPayroll;

public class ClsDataCreate
{
    public static void CreateTable()
    {
        var Sql = string.Empty;

        #region Common

        try
        {
            Sql = @"CREATE TABLE [dbo].[UserGroup]( [UGId] [int] IDENTITY(1,1) NOT NULL, [Name] [nvarchar](50) NULL,
	                    [Code] [nvarchar](20) NULL, [Remarks] [nvarchar](550) NULL, [CreatedBy] [nvarchar](50) NULL, [CreatedDate] [datetime] NULL,
                         CONSTRAINT [PK_UserGroup] PRIMARY KEY CLUSTERED
                        (
	                        [UGId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
        }
        catch
        {
        }

        try
        {
            Sql =
                @"CREATE TABLE [dbo].[UserMaster]( [UserId] [nvarchar](50) NOT NULL, [UName] [varchar](100) NOT NULL,[ULId] [varchar](50) NOT NULL,
	                    [UPws] [varchar](50) NULL, [UType] [varchar](50) NULL, [UDepartment] [varchar](50) NULL, [UAddress] [varchar](250) NULL,
	                    [ContactNo] [varchar](50) NULL,[MobileNo] [varchar](50) NULL, [Email] [varchar](100) NULL, [Active] [char](1) NULL, [Remarks] [varchar](100) NULL,
                     CONSTRAINT [PK__UserMast__1788CC4C0E6E26BF] PRIMARY KEY CLUSTERED
                    (
	                    [UserId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                     CONSTRAINT [UQ__UserMast__B7A49D90114A936A] UNIQUE NONCLUSTERED
                    (
	                    [ULId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Menu_Rights](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [Role_Id] [int] NULL,
	                [UserId] [nvarchar](50) NULL,
	                [Menu_Id] [int] NULL,
	                [Menu_Code] [nvarchar](255) NULL,
	                [Menu_Name] [nvarchar](255) NULL,
	                [Form_Name] [nvarchar](255) NULL,
	                [Module_Id] [int] NULL,
	                [SubModule_Id] [int] NULL,
	                [New] [bit] NULL,
	                [Save] [bit] NULL,
	                [Update] [bit] NULL,
	                [Delete] [bit] NULL,
	                [Copy] [bit] NULL,
	                [Search] [bit] NULL,
	                [Print] [bit] NULL,
	                [Approved] [bit] NULL,
	                [Reverse] [bit] NULL,
	                [Isparent] [bit] NULL,
	                [Created_By] [int] NULL,
	                [Created_Date] [datetime] NULL,
	                [Branch_Id] [int] NULL,
	                [FiscalYear_Id] [int] NULL
                ) ON [PRIMARY]";
        }
        catch
        {
        }

        try
        {
            Sql =
                @"CREATE TABLE [dbo].[SystemSetting]([DateType] [char](1) NULL, [SalaryCal] [varchar](10) NULL, [DBAccess_Path] [nvarchar](1024) NULL,
	                [MaxDeductPremium] [decimal](18, 6) NULL,[AcPostingDatabase] [nvarchar](50) NULL,[PostingAccountDr] [nvarchar](50) NULL,
	                [PostingAccountCr] [nvarchar](50) NULL
                ) ON [PRIMARY]
                ";
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[SystemSetting] ADD  DEFAULT ('D') FOR [DateType]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            ///Company Info Table
            Sql = @"CREATE TABLE [dbo].[LabInfo](
	                [LId] [int] IDENTITY(1,1) NOT NULL,
	                [LName] [varchar](200) NOT NULL,
	                [Code] [varchar](50) NOT NULL,
	                [LAddress] [varchar](200) NULL,
	                [ContactNo] [varchar](50) NULL,
	                [MobileNo] [varchar](50) NULL,
	                [Email] [varchar](100) NULL,
	                [Url] [varchar](100) NULL,
	                [PanNo] [varchar](50) NULL,
	                [Remarks] [varchar](200) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [LId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                        CREATE TABLE [dbo].[Branch](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [BranchCode] [nvarchar](50) NOT NULL,
	                    [Name] [nvarchar](250) NOT NULL,
	                    [Company] [varchar](200) NOT NULL,
	                    [ContactNo] [nvarchar](250) NULL,
	                    [Address] [nvarchar](250) NULL,
	                    [Remarks] [varchar](250) NULL,
	                    [Active] [char](1) NULL,
	                    [CreatedBy] [varchar](100) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [OMSCmpUnitCode] [nvarchar](56) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [ID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [BranchCode] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [Name] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[AutoNumbering](
	                        [ANId] [int] IDENTITY(1,1) NOT NULL,
	                        [Descriptions] [varchar](80) NULL,
	                        [Source] [varchar](10) NULL,
	                        [UId] [int] NULL,
	                        [StartingWord] [varchar](4) NULL,
	                        [StartingNo] [bigint] NULL,
	                        [EndingWord] [varchar](4) NULL,
	                        [PaddingLength] [tinyint] NULL,
	                        [PaddingChar] [varchar](1) NULL,
	                        [Active] [bit] NULL,
	                        [CreatedBy] [nvarchar](50) NULL,
	                        [CreatedDate] [datetime] NULL,
                         CONSTRAINT [PK_Auto_Generate] PRIMARY KEY CLUSTERED
                        (
	                        [ANId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DateMiti](
	                    [M_Date] [datetime] NULL,
	                    [M_Miti] [varchar](10) NULL,
	                    [Month_Name] [varchar](20) NULL,
	                    [Days] [varchar](20) NULL,
	                    [Holiday] [bit] NULL,
	                    [Remarks] [nvarchar](1024) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmailSetting](
	                    [Username] [varchar](200) NULL,
	                    [UserPws] [varchar](100) NULL
                    ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SendSms](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [MobileNo] [varchar](20) NULL,
	                [Name] [varchar](200) NULL,
	                [sms] [varchar](max) NULL,
	                [Date] [datetime] NULL
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SmsTemplate](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [sms] [varchar](max) NULL,
	                [Tname] [varchar](250) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SmsMailTemplate](
	                [SmsTemplateId] [int] IDENTITY(1,1) NOT NULL,
	                [Registration] [varchar](max) NULL,
	                [Appointment] [varchar](max) NULL,
	                [emailRegistration] [varchar](max) NULL,
	                [emailAppointment] [varchar](max) NULL,
	                [GroupMessage] [varchar](max) NULL
                ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SmsImport](
	                [CName] [varchar](500) NULL,
	                [MobileNo] [varchar](60) NULL,
	                [SMessage] [varchar](max) NULL,
	                [SStatus] [char](1) NULL
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[SmsImport] ADD  DEFAULT ('N') FOR [SStatus]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[tblGlobal](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [ItemName] [nvarchar](50) NULL,
	                    [ItemValue] [int] NULL,
	                    [Status] [nchar](10) NULL,
	                    [prefix] [nvarchar](50) NULL,
	                    [TotalCount] [int] NULL,
	                    [StartingNo] [int] NULL,
	                    [appendCharacter] [nvarchar](50) NULL,
                     CONSTRAINT [PK_tblGlobal] PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion Common

        #region Payroll

        try
        {
            Sql = @"CREATE TABLE [dbo].[GradeMaster](
	                    [GId] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](100) NULL,
	                    [Code] [varchar](50) NULL,
	                    [Remarks] [varchar](200) NULL,
	                    [CreatedBy] [varchar](100) NULL,
	                    [CreatedDate] [datetime] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [GId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Postmaster](
	                [PostId] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [varchar](100) NULL,
	                [Code] [varchar](50) NULL,
	                [Remarks] [varchar](200) NULL,
	                [CreatedBy] [varchar](100) NULL,
	                [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [PostId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DepartmentMaster](
	                    [DId] [int] IDENTITY(1,1) NOT NULL,
	                    [DName] [varchar](100) NULL,
	                    [DCode] [varchar](50) NULL,
	                    [Remarks] [varchar](100) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [DId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Department](
	                    [DId] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](200) NOT NULL,
	                    [Code] [varchar](50) NOT NULL,
	                    [Adress] [varchar](200) NULL,
	                    [ContactNo] [varchar](50) NULL,
	                    [Active] [char](1) NULL,
	                    [Remarks] [varchar](200) NULL,
	                    [CreatedBy] [varchar](100) NULL,
	                    [CreatedDate] [datetime] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [DId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [Name] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [Code] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Shift](
	                [SId] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [varchar](100) NULL,
	                [Code] [varchar](50) NULL,
	                [DId] [int] NULL,
	                [Datefrom] [datetime] NULL,
	                [Dateto] [datetime] NULL,
	                [StartTime] [time](7) NULL,
	                [EndTime] [time](7) NULL,
	                [WorkingHour] [time](7) NULL,
	                [OverTime] [time](7) NULL,
	                [LunchInTime] [time](7) NULL,
	                [LunchOutTime] [time](7) NULL,
	                [GraceInTime] [time](7) NULL,
	                [GraceOutTime] [time](7) NULL,
	                [Remarks] [varchar](200) NULL,
	                [CreatedBy] [varchar](100) NULL,
	                [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [SId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Shift]  WITH CHECK ADD FOREIGN KEY([DId]) REFERENCES [dbo].[Department] ([DId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[MonthlyDaysSetup](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [MDate] [datetime] NULL,
	            [MMonth] [varchar](20) NULL,
	            [MDays] [int] NULL,
	            [Mworking] [int] NULL,
	            [MHoliday] [int] NULL
                ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[LeaveMaster](
	                [LId] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [varchar](100) NULL,
	                [Code] [varchar](50) NULL,
	                [NoOfDays] [int] NULL,
	                [Carry] [char](1) NULL,
	                [Remarks] [varchar](200) NULL,
	                [CreatedBy] [varchar](100) NULL,
	                [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [LId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[HOP](
	                    [HID] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](100) NULL,
	                    [Code] [varchar](20) NULL,
	                    [FValue] [money] NULL,
	                    [Value] [varchar](50) NULL,
	                    [Type] [varchar](50) NULL,
	                    [Remarks] [varchar](200) NULL,
	                    [Tax] [char](1) NULL,
	                    [CreatdBy] [varchar](100) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [Position] [int] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [HID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BusinessCategory](
	                        [BC_Id] [Int] IDENTITY(1,1) NOT NULL,
	                        [Name] [nvarchar](100) NULL,
	                        [Code] [varchar](25) NULL,
	                        [Notes] [nvarchar](1024) NULL,
	                        [BranchId] [int] NULL,
	                        [CreatedBy] [nvarchar](50) NULL,
	                        [CreatedDate] [datetime] NULL,
                         CONSTRAINT [PK_BusinessCategory] PRIMARY KEY CLUSTERED
                        (
	                        [BC_Id] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeType](
	                    [ET_Id] [Int] IDENTITY(1,1) NOT NULL,
	                    [Name] [nvarchar](100) NULL,
	                    [Code] [varchar](25) NULL,
	                    [Notes] [nvarchar](1024) NULL,
	                    [BranchId] [int] NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_EmployeeType] PRIMARY KEY CLUSTERED
                    (
	                    [ET_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BusinessSiteMaster](
	                    [BSM_ID] [bigint] IDENTITY(1,1) NOT NULL,
	                    [BSM_Name] [nvarchar](100) NULL,
	                    [BSM_Code] [nvarchar](50) NULL,
	                    [BC_Id] [int] NULL,
	                    [Area] [nvarchar](50) NULL,
	                    [Contract_Person] [nvarchar](50) NULL,
	                    [Contract_No] [nvarchar](50) NULL,
	                    [Contract_EMail] [nvarchar](50) NULL,
	                    [Working_Hour] [time](7) NULL,
	                    [CPStart_From] [datetime] NULL,
	                    [CPEnd_To] [datetime] NULL,
	                    [Days] [decimal](18, 2) NULL,
	                    [Billing_Date] [datetime] NULL,
	                    [UpToBilled_Dated] [datetime] NULL,
	                    [Gl_Code] [nvarchar](50) NULL,
	                    [GL_Name] [nvarchar](100) NULL,
	                    [Remarks] [nvarchar](1024) NULL,
	                    [Attachment_1] [varbinary](max) NULL,
	                    [Attachment_Path1] [nvarchar](100) NULL,
	                    [Attachment_2] [varbinary](max) NULL,
	                    [Attachment_Path2] [nvarchar](100) NULL,
	                    [Active] [bit] NULL,
                        [BranchId] [int] NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_BusinessSiteMaster] PRIMARY KEY CLUSTERED
                    (
	                    [BSM_ID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]

                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteMaster]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteMaster_BusinessCategory] FOREIGN KEY([BC_Id])
                        REFERENCES [dbo].[BusinessCategory] ([BC_Id])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[BusinessSiteMaster] CHECK CONSTRAINT [FK_BusinessSiteMaster_BusinessCategory]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BusinessSiteDetails](
	                    [BSD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [BSM_Id] [bigint] NOT NULL,
                        [SNo] [int] NULL,
	                    [ET_Id] [int] NULL,
	                    [Noof_Emp] [int] NULL,
	                    [Rate_Type] [nvarchar](50) NULL,
	                    [Billing_Rate] [decimal](18, 6) NULL,
	                    [BillingOT_Rate] [decimal](18, 6) NULL,
	                    [Rate] [decimal](18, 6) NULL,
	                    [OT_Rate] [decimal](18, 6) NULL,
	                    [Period] [decimal](18, 2) NULL,
	                    [Effective_Date] [datetime] NULL,
	                    [UpdatedBy] [nvarchar](50) NULL,
	                    [UpdatedDate] [datetime] NULL,
	                    [UpdatedEffDate] [datetime] NULL,
                     CONSTRAINT [PK_BusinessSiteDetails] PRIMARY KEY CLUSTERED
                    (
	                    [BSD_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BusinessSiteDetailsLog](
	                    [BSD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [BSM_Id] [bigint] NOT NULL,
                        [SNo] [int] NULL,
	                    [ET_Id] [int] NULL,
	                    [Noof_Emp] [int] NULL,
	                    [Rate_Type] [nvarchar](50) NULL,
	                    [Billing_Rate] [decimal](18, 6) NULL,
	                    [BillingOT_Rate] [decimal](18, 6) NULL,
	                    [Rate] [decimal](18, 6) NULL,
	                    [OT_Rate] [decimal](18, 6) NULL,
	                    [Period] [decimal](18, 2) NULL,
	                    [Effective_Date] [datetime] NULL,
	                    [UpdatedBy] [nvarchar](50) NULL,
	                    [UpdatedDate] [datetime] NULL,
	                    [UpdatedEffDate] [datetime] NULL,
                     CONSTRAINT [PK_BusinessSiteDetailsLog] PRIMARY KEY CLUSTERED
                    (
	                    [BSD_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteDetails]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteDetails_BusinessSiteMaster] FOREIGN KEY([BSM_Id])
                        REFERENCES [dbo].[BusinessSiteMaster] ([BSM_ID])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteDetails] CHECK CONSTRAINT [FK_BusinessSiteDetails_BusinessSiteMaster]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteDetails]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteDetails_EmployeeType] FOREIGN KEY([ET_Id])
                        REFERENCES [dbo].[EmployeeType] ([ET_Id])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[BusinessSiteDetails] CHECK CONSTRAINT [FK_BusinessSiteDetails_EmployeeType]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeSiteTransferMaster](
	                    [ESTM_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [VNo] [nvarchar](50) NOT NULL,
	                    [VDate] [datetime] NOT NULL,
	                    [VMiti] [nvarchar](10) NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [BranchId] [int] NULL,
	                    [Remarks] [nvarchar](1024) NULL,
                     CONSTRAINT [PK_EmployeeSiteTransferMaster] PRIMARY KEY CLUSTERED
                    (
	                    [ESTM_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeSiteTransferDetails](
	                    [ESTD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [ESTM_Id] [bigint] NOT NULL,
	                    [VNo] [nvarchar](50) NOT NULL,
	                    [SNo] [int] NULL,
	                    [EId] [int] NOT NULL,
	                    [ET_Id] [int] NULL,
	                    [BSM_Id] [bigint] NULL,
	                    [Rate_Type] [nvarchar](50) NULL,
	                    [Rate] [decimal](18, 6) NULL,
	                    [Amount] [decimal](18, 6) NULL,
	                    [Descriptions] [nvarchar](512) NULL,
                     CONSTRAINT [PK_EmployeeSiteTransferDetails] PRIMARY KEY CLUSTERED
                    (
	                    [ESTD_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeSiteTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSiteTransferDetails_BusinessSiteMaster] FOREIGN KEY([BSM_Id])
                        REFERENCES [dbo].[BusinessSiteMaster] ([BSM_ID])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeSiteTransferDetails] CHECK CONSTRAINT [FK_EmployeeSiteTransferDetails_BusinessSiteMaster]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeSiteTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSiteTransferDetails_Employee] FOREIGN KEY([EId])
                        REFERENCES [dbo].[Employee] ([Id])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeSiteTransferDetails] CHECK CONSTRAINT [FK_EmployeeSiteTransferDetails_Employee]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeSiteTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeSiteTransferDetails_EmployeeType] FOREIGN KEY([ET_Id])
                        REFERENCES [dbo].[EmployeeType] ([ET_Id])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeSiteTransferDetails] CHECK CONSTRAINT [FK_EmployeeSiteTransferDetails_EmployeeType]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SalaryStructureMaster](
	            [SId] [int] IDENTITY(1,1) NOT NULL,
	            [Name] [varchar](100) NOT NULL,
	            [Code] [varchar](100) NULL,
	            [Active] [char](1) NULL,
	            [Remarks] [varchar](200) NULL,
	            [CreatedBy] [varchar](100) NULL,
	            [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [SId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SalaryStructureDetails](
	            [Name] [varchar](100) NOT NULL,
	            [HopCode] [varchar](100) NULL,
	            [HopName] [varchar](200) NULL,
	            [Type] [varchar](100) NULL,
	            [Value] [money] NULL
            ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Overtime](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [Name] [varchar](100) NULL,
	            [Code] [varchar](30) NULL,
	            [DId] [int] NULL,
	            [PId] [int] NULL,
	            [GId] [int] NULL,
	            [OtRate] [money] NULL,
	            [Active] [char](1) NULL,
	            [Remarks] [varchar](200) NULL,
	            [CreatedBy] [varchar](100) NULL,
	            [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Overtime]  WITH CHECK ADD  CONSTRAINT [FK_Overtime_Department] FOREIGN KEY([DId])
                        REFERENCES [dbo].[Department] ([DId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Overtime] CHECK CONSTRAINT [FK_Overtime_Department]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Overtime]  WITH CHECK ADD  CONSTRAINT [FK_Overtime_GradeMaster] FOREIGN KEY([GId])
                        REFERENCES [dbo].[GradeMaster] ([GId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Overtime] CHECK CONSTRAINT [FK_Overtime_GradeMaster]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Overtime]  WITH CHECK ADD  CONSTRAINT [FK_Overtime_Postmaster] FOREIGN KEY([PId])
                    REFERENCES [dbo].[Postmaster] ([PostId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Overtime] CHECK CONSTRAINT [FK_Overtime_Postmaster]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TaxMaster](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](100) NULL,
	                    [Code] [varchar](30) NULL,
	                    [Remarks] [varchar](200) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TaxDetails](
	                [Name] [varchar](100) NULL,
	                [AF] [money] NULL,
	                [Type] [varchar](50) NULL,
	                [AT] [money] NULL,
	                [Per] [money] NULL
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Employee](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [varchar](200) NULL,
	                [Code] [varchar](30) NULL,
	                [AccountingCode] [nvarchar](50) NULL,
	                [MCode] [varchar](30) NULL,
	                [OId] [int] NULL,
	                [DId] [int] NULL,
	                [PId] [int] NULL,
	                [GId] [int] NULL,
	                [SId] [int] NULL,
	                [TId] [int] NULL,
	                [ShId] [int] NULL,
	                [Joindate] [datetime] NULL,
	                [DOB] [datetime] NULL,
	                [Age] [int] NULL,
	                [FatherName] [varchar](200) NULL,
	                [SuposeName] [varchar](200) NULL,
	                [Email] [varchar](200) NULL,
	                [Sex] [varchar](20) NULL,
	                [BloodGroup] [varchar](5) NULL,
	                [ContactNo] [varchar](100) NULL,
	                [MobileNo] [nvarchar](50) NULL,
	                [Adress] [varchar](500) NULL,
	                [Contract] [varchar](50) NULL,
	                [Premium] [money] NULL,
	                [PanNo] [nvarchar](50) NULL,
	                [Qualification] [varchar](500) NULL,
	                [Training] [varchar](500) NULL,
	                [Remarks] [varchar](500) NULL,
	                [Active] [char](1) NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
	                [SysBranchName] [varchar](500) NULL,
	                [BankAc] [varchar](100) NULL,
	                [CitNo] [varchar](100) NULL,
	                [BranchId] [int] NULL,
	                [MCard_No] [nvarchar](50) NULL,
	                [MMachine_Id] [int] NULL,
	                [MPrivilege] [nvarchar](50) NULL,
                     CONSTRAINT [PK__Employee__3214EC073864608B] PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
        catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__DId__3B40CD36] FOREIGN KEY([DId]) REFERENCES [dbo].[Department] ([DId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__DId__3B40CD36]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__GId__3D2915A8] FOREIGN KEY([GId]) REFERENCES [dbo].[GradeMaster] ([GId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__GId__3D2915A8]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__OId__3A4CA8FD] FOREIGN KEY([OId]) REFERENCES [dbo].[Overtime] ([Id])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__OId__3A4CA8FD]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__PId__3C34F16F] FOREIGN KEY([PId]) REFERENCES [dbo].[Postmaster] ([PostId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__PId__3C34F16F]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__ShId__40058253] FOREIGN KEY([ShId]) REFERENCES [dbo].[Shift] ([SId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__ShId__40058253]
                    ";
            using (var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection()))
            {
                cmd.CommandTimeout = 500;
                cmd.ExecuteNonQuery();
            }
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__SId__3E1D39E1] FOREIGN KEY([SId]) REFERENCES [dbo].[SalaryStructureMaster] ([SId])   ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__SId__3E1D39E1]   ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__TId__3F115E1A] FOREIGN KEY([TId]) REFERENCES [dbo].[TaxMaster] ([Id])  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__TId__3F115E1A]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Department] FOREIGN KEY([DId]) REFERENCES [dbo].[Department] ([DId])  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Department]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_ShId] FOREIGN KEY([ShId]) REFERENCES [dbo].[Shift] ([SId])   ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_ShId] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_SId] FOREIGN KEY([SId]) REFERENCES [dbo].[SalaryStructureMaster] ([SId])  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_SId] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeHop](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Eid] [int] NULL,
	                    [HopCode] [varchar](20) NULL,
	                    [HopName] [varchar](100) NULL,
	                    [HopType] [varchar](50) NULL,
	                    [Value] [money] NULL
                    ) ON [PRIMARY]

                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeHop]  WITH CHECK ADD  CONSTRAINT [FK__EmployeeHop__Eid__41EDCAC5] FOREIGN KEY([Eid]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EmployeeHop] CHECK CONSTRAINT [FK__EmployeeHop__Eid__41EDCAC5] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[EmployeeHop]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeHop] FOREIGN KEY([Eid]) REFERENCES [dbo].[Employee] ([Id])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EmployeeHop] CHECK CONSTRAINT [FK_EmployeeHop] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeLoanDetails](
	                [ELD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [EId] [int] NOT NULL,
	                [Bank_Name] [nvarchar](80) NULL,
	                [Bank_GlCode] [varchar](25) NULL,
	                [Loan_Type] [nvarchar](25) NULL,
	                [Loan_Amount] [decimal](18, 8) NULL,
	                [Interest_Rate] [decimal](18, 8) NULL,
	                [Loan_Period] [int] NULL,
	                [NoofInstallment_PerYear] [int] NULL,
	                [LoanIssue_Date] [datetime] NULL,
	                [Total_Interest] [decimal](18, 8) NULL,
	                [Total_RepaymentAmt] [decimal](18, 8) NULL,
	                [EMI_Amount] [decimal](18, 8) NULL,
	                [Attachment_1] [varbinary](max) NULL,
	                [Attachment_Path1] [nvarchar](100) NULL,
	                [Attachment_2] [varbinary](max) NULL,
	                [Attachment_Path2] [nvarchar](100) NULL,
	                [Remarks] [nvarchar](1024) NULL,
                    [BranchId] [int] NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_EmployeeLoanDetails] PRIMARY KEY CLUSTERED
                (
	                [ELD_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLoanDetails_Employee] FOREIGN KEY([EId])
                REFERENCES [dbo].[Employee] ([Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EmployeeLoanDetails] CHECK CONSTRAINT [FK_EmployeeLoanDetails_Employee]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeLoanInstallment](
	                [ELI_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [ELD_Id] [bigint] NOT NULL,
	                [EId] [int] NULL,
	                [SNo] [int] NULL,
	                [Month_Year] [nvarchar](50) NULL,
	                [EMI_Date] [datetime] NULL,
	                [Monthly_Installment] [decimal](18, 8) NULL,
	                [Interest] [decimal](18, 8) NULL,
	                [Principal] [decimal](18, 8) NULL,
	                [Balance] [decimal](18, 8) NULL,
	                [Paid] [bit] NULL,
                 CONSTRAINT [PK_EmployeeLoanInstallment] PRIMARY KEY CLUSTERED
                (
	                [ELI_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInstallment] ADD  CONSTRAINT [Constraint_name]  DEFAULT ((0)) FOR [Paid]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInstallment]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLoanInstallment_Employee] FOREIGN KEY([EId])
                    REFERENCES [dbo].[Employee] ([Id])
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInstallment] CHECK CONSTRAINT [FK_EmployeeLoanInstallment_Employee]
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInstallment]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLoanInstallment_EmployeeLoanInstallment] FOREIGN KEY([ELD_Id])
                REFERENCES [dbo].[EmployeeLoanDetails] ([ELD_Id])
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInstallment] CHECK CONSTRAINT [FK_EmployeeLoanInstallment_EmployeeLoanInstallment]
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        //-----------
        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeAdvanceVoucher](
	                    [VNo] [nvarchar](50) NOT NULL,
	                    [VDate] [datetime] NOT NULL,
	                    [VMiti] [nvarchar](10) NULL,
	                    [VType] [nvarchar](50) NULL,
	                    [BankName] [nvarchar](100) NULL,
	                    [Bank_GlCode] [varchar](25) NULL,
	                    [BankBranch] [nvarchar](100) NULL,
	                    [ChqNo] [nvarchar](50) NULL,
	                    [ChqDate] [datetime] NULL,
	                    [ChqMiti] [nvarchar](10) NULL,
	                    [EId] [int] NULL,
	                    [PayAmount] [decimal](18, 8) NULL,
	                    [RecAmount] [decimal](18, 8) NULL,
	                    [DepositedBy] [nvarchar](50) NULL,
	                    [Notes] [nvarchar](1024) NULL,
	                    [BranchId] [int] NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_EmployeeAdvanceVoucher] PRIMARY KEY CLUSTERED
                    (
	                    [VNo] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeAdvanceVoucher]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAdvanceVoucher_Employee] FOREIGN KEY([EId])
                REFERENCES [dbo].[Employee] ([Id])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeAdvanceVoucher] CHECK CONSTRAINT [FK_EmployeeAdvanceVoucher_Employee]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeAdvanceAdjustmentVoucher](
	                    [VNo] [nvarchar](50) NOT NULL,
	                    [VDate] [datetime] NOT NULL,
	                    [VMiti] [nvarchar](10) NULL,
	                    [Adv_VNo] [nvarchar](50) NULL,
	                    [Adv_VDate] [datetime] NULL,
	                    [Adv_VMiti] [nvarchar](10) NULL,
	                    [VType] [nvarchar](50) NULL,
	                    [BankName] [nvarchar](100) NULL,
	                    [Bank_GlCode] [varchar](25) NULL,
	                    [BankBranch] [nvarchar](100) NULL,
	                    [ChqNo] [nvarchar](50) NULL,
	                    [ChqDate] [datetime] NULL,
	                    [ChqMiti] [nvarchar](10) NULL,
	                    [EId] [int] NULL,
	                    [PayAmount] [decimal](18, 8) NULL,
	                    [RecAmount] [decimal](18, 8) NULL,
	                    [ReceivedBy] [nvarchar](50) NULL,
	                    [Notes] [nvarchar](1024) NULL,
	                    [BranchId] [int] NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_EmployeeAdvanceAdjustmentVoucher] PRIMARY KEY CLUSTERED
                    (
	                    [VNo] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeAdvanceAdjustmentVoucher]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeAdvanceAdjustmentVoucher_Employee] FOREIGN KEY([EId])
                    REFERENCES [dbo].[Employee] ([Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeAdvanceAdjustmentVoucher] CHECK CONSTRAINT [FK_EmployeeAdvanceAdjustmentVoucher_Employee]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        //-----------
        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeLoanInsPaidVoucher](
	            [VNo] [nvarchar](50) NOT NULL,
	            [VDate] [datetime] NOT NULL,
	            [VMiti] [nvarchar](10) NULL,
	            [VType] [nvarchar](50) NULL,
	            [BankName] [nvarchar](100) NULL,
	            [Bank_GlCode] [varchar](25) NULL,
	            [BankBranch] [nvarchar](100) NULL,
	            [ChqNo] [nvarchar](50) NULL,
	            [ChqDate] [datetime] NULL,
	            [ChqMiti] [nvarchar](10) NULL,
	            [Amount] [decimal](18, 8) NULL,
	            [DepositedBy] [nvarchar](50) NULL,
	            [Notes] [nvarchar](1024) NULL,
	            [EId] [int] NULL,
	            [ELI_Id] [bigint] NULL,
                 CONSTRAINT [PK_EmployeeLoanInsPaidVoucher] PRIMARY KEY CLUSTERED
                (
	                [VNo] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInsPaidVoucher]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLoanInsPaidVoucher_Employee] FOREIGN KEY([EId]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInsPaidVoucher] CHECK CONSTRAINT [FK_EmployeeLoanInsPaidVoucher_Employee] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInsPaidVoucher]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeLoanInsPaidVoucher_EmployeeLoanInstallment] FOREIGN KEY([ELI_Id]) REFERENCES [dbo].[EmployeeLoanInstallment] ([ELI_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeLoanInsPaidVoucher] CHECK CONSTRAINT [FK_EmployeeLoanInsPaidVoucher_EmployeeLoanInstallment]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        //
        try
        {
            Sql = @"CREATE TABLE [dbo].[TimeSetting](
	                        [Id] [int] IDENTITY(1,1) NOT NULL,
	                        [StartTime] [time](7) NULL,
	                        [EndTime] [time](7) NULL,
	                        [AddTime] [time](7) NULL
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Event](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [Title] [nvarchar](1024) NULL,
	                [Details] [nvarchar](max) NULL,
	                [BackColor] [nvarchar](50) NULL,
	                [ForeColor] [nvarchar](50) NULL,
	                [Location] [nvarchar](512) NULL,
	                [StartDate] [datetime] NULL,
	                [EndDate] [datetime] NULL,
	                [Duration] [int] NULL,
	                [EmpId] [int] NULL,
	                [Remarks] [nvarchar](1024) NULL,
	                [Status] [bit] NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
	                [Branch_Id] [int] NULL,
                 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Employee] FOREIGN KEY([EmpId]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Employee] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EventParticipants](
	                [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [EvId] [bigint] NULL,
	                [EmpId] [int] NULL,
                 CONSTRAINT [PK_EventParticipants] PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD  CONSTRAINT [FK_EventParticipants_Employee] FOREIGN KEY([EmpId]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EventParticipants] CHECK CONSTRAINT [FK_EventParticipants_Employee] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EventParticipants]  WITH CHECK ADD  CONSTRAINT [FK_EventParticipants_Event] FOREIGN KEY([EvId]) REFERENCES [dbo].[Event] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EventParticipants] CHECK CONSTRAINT [FK_EventParticipants_Event] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #region Machine Info & Atten

        try
        {
            Sql = @"CREATE TABLE [dbo].[MachineInfo](
	                    [Machine_Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Machine_Name] [varchar](50) NOT NULL,
	                    [Company_Id] [int] NULL,
	                    [Port_Address] [nvarchar](50) NULL,
	                    [Device_No] [nvarchar](50) NULL,
	                    [Communication_Key] [nvarchar](50) NULL,
	                    [IP] [nvarchar](50) NULL,
	                    [Branch_Id] [int] NULL,
	                    [Status] [bit] NULL,
                     CONSTRAINT [PK_MachineInfo] PRIMARY KEY CLUSTERED
                    (
	                    [Machine_Name] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[MachineFingerPrint](
	                [FingurePrint_Id] [int] IDENTITY(1,1) NOT NULL,
	                [Employee_Id] [int] NULL,
	                [FingurePrint] [text] NULL,
	                [Machine_Id] [int] NULL,
	                [Branch_Id] [int] NULL,
                 CONSTRAINT [PK_MachineFingerPrint] PRIMARY KEY CLUSTERED
                (
	                [FingurePrint_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[MachineEmployeeInfo](
	                [MEmployee_Id] [int] NOT NULL,
	                [Name] [nvarchar](50) NULL,
	                [EmployeeCode] [nvarchar](50) NULL,
	                [Privilege] [nvarchar](50) NULL,
	                [Machine_Id] [int] NULL,
	                [Branch_Id] [int] NULL
                ) ON [PRIMARY]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[MachineAttendance](
	                    [ATId] [bigint] IDENTITY(1,1) NOT NULL,
	                    [EmpId] [int] NULL,
	                    [WorkCode] [int] NULL,
	                    [Company] [varchar](50) NULL,
	                    [MonthNameAD] [varchar](50) NULL,
	                    [MonthNameBS] [varchar](50) NULL,
	                    [Date] [datetime] NULL,
	                    [Miti] [varchar](10) NULL,
	                    [Time] [time](7) NULL,
	                    [Type] [varchar](50) NULL,
	                    [Remarks] [varchar](50) NULL,
	                    [BranchId] [int] NULL,
	                    [Status] [bit] NULL,
	                    [IP] [varchar](50) NULL,
	                    [CreatedBy] [varchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                         CONSTRAINT [PK_CHECKINOUT] PRIMARY KEY CLUSTERED
                        (
	                        [ATId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[MachineAttendanceLog](
	                    [ATId] [bigint] IDENTITY(1,1) NOT NULL,
	                    [EmpId] [int] NULL,
	                    [WorkCode] [int] NULL,
	                    [Company] [varchar](50) NULL,
	                    [MonthNameAD] [varchar](50) NULL,
	                    [MonthNameBS] [varchar](50) NULL,
	                    [Date] [datetime] NULL,
	                    [Miti] [varchar](10) NULL,
	                    [Time] [time](7) NULL,
	                    [Type] [varchar](50) NULL,
	                    [Remarks] [varchar](50) NULL,
	                    [LastFetched] [bit] NULL,
	                    [BranchId] [int] NULL,
	                    [Status] [bit] NULL,
	                    [IP] [varchar](50) NULL,
	                    [CreatedBy] [varchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_MachineAttendanceLog] PRIMARY KEY CLUSTERED
                    (
	                    [ATId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion Machine Info & Atten

        try
        {
            Sql = @" CREATE TABLE [dbo].[LeaveOpeningEntry](
	                    [LOE_Id] [int] IDENTITY(1,1) NOT NULL,
	                    [EId] [int] NOT NULL,
	                    [EntryDate] [datetime] NOT NULL,
	                    [SNo] [int] NULL,
	                    [LId] [int] NOT NULL,
	                    [TotalCFLeave] [decimal](18, 8) NULL,
	                    [TotalOBLeave] [decimal](18, 8) NULL,
	                    [Narration] [varchar](max) NULL,
	                    [Remarks] [varchar](max) NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK__LeaveOpe__B290B28D4E739D3B] PRIMARY KEY CLUSTERED
                    (
	                    [LOE_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[LeaveOpeningEntry]  WITH CHECK ADD  CONSTRAINT [FK__LeaveOpeningEntry__EId__6F7F8B4B] FOREIGN KEY([EId]) REFERENCES [dbo].[Employee] ([Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[LeaveOpeningEntry] CHECK CONSTRAINT [FK__LeaveOpeningEntry__EId__6F7F8B4B]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[LeaveOpeningEntry]  WITH CHECK ADD  CONSTRAINT [FK_LeaveOpeningEntry] FOREIGN KEY([EId]) REFERENCES [dbo].[Employee] ([Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[LeaveOpeningEntry] CHECK CONSTRAINT [FK_LeaveOpeningEntry]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[LeaveOpeningEntry]  WITH CHECK ADD  CONSTRAINT [FK_LeaveOpeningEntry_LeaveMaster] FOREIGN KEY([LId]) REFERENCES [dbo].[LeaveMaster] ([LId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[LeaveOpeningEntry] CHECK CONSTRAINT [FK_LeaveOpeningEntry_LeaveMaster]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" CREATE TABLE [dbo].[LeaveEntry](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [EId] [int] NULL,
	            [Apply_LId] [int] NULL,
	            [Apply_Date] [datetime] NULL,
	            [Apply_FromDate] [datetime] NULL,
	            [Apply_ToDate] [datetime] NULL,
	            [Reason] [nvarchar](200) NULL,
	            [Approved_Date] [datetime] NULL,
	            [Approved_FromDate] [datetime] NULL,
	            [Approved_ToDate] [datetime] NULL,
	            [Approved_By] [nvarchar](200) NULL,
	            [Approved_LId] [int] NULL,
	            [Remarks] [varchar](max) NULL,
	            [CreatedBy] [nvarchar](50) NULL,
	            [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[LeaveEntry]  WITH CHECK ADD  CONSTRAINT [FK__LeaveEntry__EId__6F7F8B4B] FOREIGN KEY([EId]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[LeaveEntry] CHECK CONSTRAINT [FK__LeaveEntry__EId__6F7F8B4B] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[LeaveEntry]  WITH CHECK ADD  CONSTRAINT [FK_LeaveEntry] FOREIGN KEY([EId]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[LeaveEntry] CHECK CONSTRAINT [FK_LeaveEntry] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[MonthlyAtt](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [enterydate] [datetime] NULL,
	                [MonthName] [varchar](50) NULL,
	                [Ndays] [int] NULL,
	                [wdays] [int] NULL,
	                [eCode] [varchar](50) NULL,
	                [pdays] [int] NULL,
	                [adays] [int] NULL,
	                [Remarks] [varchar](500) NULL,
	                [Eid] [int] NULL
                    ) ON [PRIMARY]

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[MonthlyAtt]  WITH CHECK ADD  CONSTRAINT [FK_MonthlyAtt] FOREIGN KEY([Eid]) REFERENCES [dbo].[Employee] ([Id])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[MonthlyAtt] CHECK CONSTRAINT [FK_MonthlyAtt] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DailyAttendance](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [DMonth] [varchar](50) NULL,
	                    [DDate] [datetime] NULL,
	                    [MMonth] [varchar](50) NULL,
	                    [MDate] [nvarchar](10) NULL,
	                    [EId] [int] NULL,
	                    [Intime] [time](7) NULL,
	                    [LOut] [time](7) NULL,
	                    [LIn] [time](7) NULL,
	                    [TOut] [time](7) NULL,
	                    [Tin] [time](7) NULL,
	                    [OutTime] [time](7) NULL,
	                    [Total] [time](7) NULL,
	                    [Remarks] [varchar](1000) NULL,
	                    [EOut] [time](7) NULL,
	                    [EIn] [time](7) NULL,
	                    [OOut] [time](7) NULL,
	                    [OIn] [time](7) NULL,
	                    [SheetNo] [int] NULL,
	                    [Intime1] [time](7) NULL,
	                    [LOut1] [time](7) NULL,
	                    [LIn1] [time](7) NULL,
	                    [TOut1] [time](7) NULL,
	                    [Tin1] [time](7) NULL,
	                    [OutTime1] [time](7) NULL,
	                    [Total1] [time](7) NULL,
	                    [EOut1] [time](7) NULL,
	                    [EIn1] [time](7) NULL,
	                    [OOut1] [time](7) NULL,
	                    [OIn1] [time](7) NULL
                    ) ON [PRIMARY]
                     ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BusinessSiteWiseDailyAttendance](
	                [BSDA_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [Date] [datetime] NOT NULL,
	                [Miti] [varchar](12) NULL,
	                [BC_Id] [int] NULL,
	                [BSM_Id] [bigint] NULL,
	                [ET_Id] [int] NULL,
	                [Emp_Id] [int] NOT NULL,
	                [Absent] [bit] NULL,
	                [Present] [bit] NULL,
	                [HalfDay] [bit] NULL,
	                [OverTime] [bit] NULL,
	                [Descriptions] [nvarchar](512) NULL,
	                [Remarks] [nvarchar](1024) NULL,
	                [BranchId] [int] NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_BusinessSiteWiseDailyAttendance] PRIMARY KEY CLUSTERED
                (
	                [BSDA_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_BusinessCategory] FOREIGN KEY([BC_Id])
                        REFERENCES [dbo].[BusinessCategory] ([BC_Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance] CHECK CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_BusinessCategory]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_BusinessSiteMaster] FOREIGN KEY([BSM_Id])
                    REFERENCES [dbo].[BusinessSiteMaster] ([BSM_ID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance] CHECK CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_BusinessSiteMaster]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_Employee] FOREIGN KEY([Emp_Id])
                            REFERENCES [dbo].[Employee] ([Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance] CHECK CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_Employee]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance]  WITH CHECK ADD  CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_EmployeeType] FOREIGN KEY([ET_Id])
                        REFERENCES [dbo].[EmployeeType] ([ET_Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BusinessSiteWiseDailyAttendance] CHECK CONSTRAINT [FK_BusinessSiteWiseDailyAttendance_EmployeeType]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EmployeeTravelDetails](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [Entry_Date] [datetime] NOT NULL,
	            [EId] [int] NOT NULL,
	            [Destination] [nvarchar](255) NULL,
	            [Purpose] [nvarchar](255) NULL,
	            [From_Date] [datetime] NULL,
	            [To_Date] [datetime] NULL,
	            [ModeOf_Travel] [nvarchar](100) NULL,
	            [Narration] [nvarchar](512) NULL,
	            [Travel_Expenses] [decimal](18, 8) NULL,
	            [Recommended_By] [int] NULL,
	            [Approved_By] [int] NULL,
	            [Branch_Id] [int] NULL,
	            [Created_By] [nvarchar](50) NULL,
	            [Created_Date] [datetime] NULL,
             CONSTRAINT [PK_EmployeeTravelDetails] PRIMARY KEY CLUSTERED
            (
	            [Id] ASC
            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
            ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeTravelDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTravelDetails_Branch] FOREIGN KEY([Branch_Id])
                    REFERENCES [dbo].[Branch] ([ID])
             ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EmployeeTravelDetails] CHECK CONSTRAINT [FK_EmployeeTravelDetails_Branch]
             ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EmployeeTravelDetails]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeTravelDetails_Employee] FOREIGN KEY([EId])
                    REFERENCES [dbo].[Employee] ([Id])
             ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EmployeeTravelDetails] CHECK CONSTRAINT [FK_EmployeeTravelDetails_Employee]
             ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[WorkingDays](
	                    [DDate] [datetime] NULL,
	                    [MName] [varchar](50) NULL,
	                    [EId] [int] NULL,
	                    [EName] [varchar](250) NULL,
	                    [DaysInMonth] [int] NULL,
	                    [WorkingDays] [int] NULL,
	                    [Value] [decimal](16, 6) NULL,
	                    [DwHour] [time](7) NULL,
	                    [WDays] [decimal](16, 6) NULL,
	                    [OTrate] [decimal](16, 6) NULL,
	                    [PayableSalary] [decimal](16, 6) NULL,
	                    [OTAmount] [decimal](16, 6) NULL,
	                    [TotalAmt] [decimal](18, 6) NULL,
	                    [DeductionAmt] [decimal](18, 6) NULL,
	                    [TaxableAmt] [decimal](18, 6) NULL,
	                    [TaxAmt] [decimal](18, 6) NULL,
	                    [NetSalary] [decimal](18, 6) NULL
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" Alter Table WorkingDays Add [WDM_Id] [bigint] IDENTITY(1,1) NOT NULL
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" Alter Table WorkingDays Add [AllowanceDays] [decimal](18, 6) null
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE WorkingDays ADD  CONSTRAINT [DF_WorkingDays_AllowanceDays]  DEFAULT ((0)) FOR [AllowanceDays]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[WorkingDaysDetails](
	                [WDD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [WDM_Id] [bigint] NOT NULL,
	                [DMonth_Name] [nvarchar](50) NULL,
	                [MMonth_Name] [nvarchar](50) NULL,
	                [Emp_Id] [int] NULL,
	                [HOP_Id] [int] NULL,
	                [Payable_Value] [decimal](18, 6) NULL,
                 CONSTRAINT [PK_WorkingDaysDetails] PRIMARY KEY CLUSTERED
                (
	                [WDD_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[WorkingDaysDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkingDaysDetails_Employee] FOREIGN KEY([Emp_Id])
                    REFERENCES [dbo].[Employee] ([Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[WorkingDaysDetails] CHECK CONSTRAINT [FK_WorkingDaysDetails_Employee]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[WorkingDaysDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkingDaysDetails_HOP] FOREIGN KEY([HOP_Id])
                REFERENCES [dbo].[HOP] ([HID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[WorkingDaysDetails] CHECK CONSTRAINT [FK_WorkingDaysDetails_HOP]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[WorkingDaysDetails]  WITH CHECK ADD  CONSTRAINT [FK_WorkingDaysDetails_WorkingDays] FOREIGN KEY([WDM_Id])
                        REFERENCES [dbo].[WorkingDays] ([WDM_Id])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[WorkingDaysDetails] CHECK CONSTRAINT [FK_WorkingDaysDetails_WorkingDays]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion Payroll

        #region FixedAssets

        try
        {
            Sql = @"CREATE TABLE [dbo].[PoolMaster](
	                [PID] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [nvarchar](80) NULL,
	                [Code] [nvarchar](20) NULL,
	                [DPRate] [decimal](18, 6) NULL,
	                [Remarks] [nvarchar](550) NULL,
	                [CreatedBy] [varchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_PoolMaster] PRIMARY KEY CLUSTERED
                    (
	                    [PID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[PoolMaster] ADD  CONSTRAINT [DF_PoolMaster_DPRate]  DEFAULT ((0)) FOR [DPRate]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductGroup](
	                    [PGID] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [nvarchar](50) NULL,
	                    [GrpCode] [varchar](15) NULL,
	                    [Code] [nvarchar](20) NULL,
	                    [Remarks] [nvarchar](550) NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_ProductGroup] PRIMARY KEY CLUSTERED
                    (
	                    [PGID] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Product](
	                    [PId] [int] IDENTITY(1,1) NOT NULL,
	                    [PCode] [varchar](15) NULL,
	                    [Name] [nvarchar](250) NULL,
	                    [Code] [nvarchar](20) NULL,
	                    [PGId] [int] NULL,
	                    [GrpCode] [varchar](15) NULL,
	                    [Specification] [nvarchar](1024) NULL,
	                    [ManufactureCompany] [nvarchar](1024) NULL,
	                    [LifeofAssets] [decimal](18, 8) NULL,
	                    [Size] [nvarchar](50) NULL,
	                    [Unit] [nvarchar](15) NULL,
	                    [Price] [decimal](18, 8) NULL,
	                    [PoolId] [int] NULL,
	                    [DateOfPurchase] [datetime] NULL,
	                    [MitiOfPurchase] [nvarchar](10) NULL,
	                    [PurchaseAcuDate] [datetime] NULL,
	                    [PurchaseAcuMiti] [nvarchar](10) NULL,
	                    [EngineNo] [varchar](50) NULL,
	                    [ChassisNo] [varchar](50) NULL,
	                    [ServiceType] [varchar](50) NULL,
	                    [ServiceRate] [decimal](18, 8) NULL,
	                    [ProductForService] [varchar](50) NULL,
	                    [NoOfKMOrHour] [int] NULL,
	                    [Remarks] [nvarchar](550) NULL,
	                    [PImage] [varbinary](max) NULL,
	                    [DrGlCode] [nvarchar](50) NULL,
	                    [CrGlCode] [nvarchar](50) NULL,
	                    [Active] [bit] NULL,
	                    [BranchId] [int] NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                        CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED
                        (
	                        [PId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]

                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Branch] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch] ([ID]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Branch] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_PoolMaster] FOREIGN KEY([PoolId]) REFERENCES [dbo].[PoolMaster] ([PID]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_PoolMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductGroup] FOREIGN KEY([PGId]) REFERENCES [dbo].[ProductGroup] ([PGID]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductGroup] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductRenuwalDetails](
	            [PRD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	            [PId] [int] NOT NULL,
	            [PCode] [varchar](15) NOT NULL,
	            [Insurance_Amount] [decimal](18, 8) NULL,
	            [Insurence_Date] [datetime] NULL,
	            [Insurance_UpToDate] [datetime] NULL,
	            [Insurance_Company] [nvarchar](80) NULL,
	            [Insurance_CmpAddress] [nvarchar](512) NULL,
	            [Insurance_BankName] [nvarchar](250) NULL,
	            [Insurance_BankBranch] [nvarchar](250) NULL,
	            [Renuwal_Paid] [bit] NULL,
	            [Renuwal_Notes] [nvarchar](512) NULL,
	            [Renuwal_Amount] [decimal](18, 8) NULL,
	            [Renuwal_ExpiryDate] [datetime] NULL,
	            [Renuwal_NextExpiryDate] [datetime] NULL,
	            [Renuwal_Office] [nvarchar](250) NULL,
	            [Attachment_1] [varbinary](max) NULL,
	            [Attachment_Path1] [nvarchar](100) NULL,
	            [Attachment_2] [varbinary](max) NULL,
	            [Attachment_Path2] [nvarchar](100) NULL,
	            [Remarks] [nvarchar](1024) NULL,
                 CONSTRAINT [PK_ProductRenuwalDetails] PRIMARY KEY CLUSTERED
                (
	                [PRD_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[ProductRenuwalDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductRenuwalDetails_Product] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[ProductRenuwalDetails] CHECK CONSTRAINT [FK_ProductRenuwalDetails_Product]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductLoanDetails](
	                [PLD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [PId] [int] NOT NULL,
	                [PCode] [varchar](15) NOT NULL,
	                [Bank_Name] [nvarchar](80) NULL,
	                [Bank_GlCode] [varchar](25) NULL,
	                [Loan_Type] [nvarchar](25) NULL,
	                [Loan_Amount] [decimal](18, 8) NULL,
	                [Interest_Rate] [decimal](18, 8) NULL,
	                [Loan_Period] [int] NULL,
	                [NoofInstallment_PerYear] [int] NULL,
	                [LoanIssue_Date] [datetime] NULL,
	                [Total_Interest] [decimal](18, 8) NULL,
	                [Total_RepaymentAmt] [decimal](18, 8) NULL,
	                [EMI_Amount] [decimal](18, 8) NULL,
	                [Attachment_1] [varbinary](max) NULL,
	                [Attachment_Path1] [nvarchar](100) NULL,
	                [Attachment_2] [varbinary](max) NULL,
	                [Attachment_Path2] [nvarchar](100) NULL,
	                [Remarks] [nvarchar](1024) NULL,
                     CONSTRAINT [PK_ProductLoanDetails] PRIMARY KEY CLUSTERED
                    (
	                    [PLD_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductLoanDetails_Product] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[ProductLoanDetails] CHECK CONSTRAINT [FK_ProductLoanDetails_Product]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" CREATE TABLE [dbo].[ProductLoanInstallment](
	                [PLI_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [PLD_Id] [bigint] NOT NULL,
	                [PId] [int] NULL,
	                [PCode] [varchar](15) NULL,
	                [SNo] [int] NULL,
	                [Month_Year] [nvarchar](50) NULL,
	                [EMI_Date] [datetime] NULL,
	                [Monthly_Installment] [decimal](18, 8) NULL,
	                [Interest] [decimal](18, 8) NULL,
	                [Principal] [decimal](18, 8) NULL,
	                [Balance] [decimal](18, 8) NULL,
	                [Paid] [bit] NULL,
                    CONSTRAINT [PK_ProductLoanInstallment] PRIMARY KEY CLUSTERED
                    (
	                    [PLI_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
        catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInstallment] ADD  CONSTRAINT [Constraint_name]  DEFAULT ((0)) FOR [Paid]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInstallment]  WITH CHECK ADD  CONSTRAINT [FK_ProductLoanInstallment_Product] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInstallment] CHECK CONSTRAINT [FK_ProductLoanInstallment_Product] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInstallment]  WITH CHECK ADD  CONSTRAINT [FK_ProductLoanInstallment_ProductLoanInstallment] FOREIGN KEY([PLD_Id]) REFERENCES [dbo].[ProductLoanDetails] ([PLD_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInstallment] CHECK CONSTRAINT [FK_ProductLoanInstallment_ProductLoanInstallment] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Person](
	                [PId] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [nvarchar](80) NULL,
	                [Code] [nvarchar](20) NULL,
	                [Remarks] [nvarchar](550) NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                        CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED
                    (
	                    [PId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                        CONSTRAINT [IX_Person] UNIQUE NONCLUSTERED
                    (
	                    [Code] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Supplier](
	                    [SId] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [nvarchar](80) NULL,
	                    [Code] [nvarchar](20) NULL,
	                    [Address] [nvarchar](100) NULL,
	                    [PhoneNo] [nvarchar](50) NULL,
	                    [MobileNo] [nvarchar](50) NULL,
	                    [EmailId] [nvarchar](50) NULL,
	                    [PanNo] [nvarchar](50) NULL,
	                    [Remarks] [nvarchar](550) NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED
                    (
	                    [SId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductBranchTransfer](
	                    [PBT_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [PId] [int] NOT NULL,
	                    [PreviousBranchId] [int] NULL,
	                    [BranchId] [int] NOT NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_ProductBranchTransfer] PRIMARY KEY CLUSTERED
                    (
	                    [PBT_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductBranchTransfer]  WITH CHECK ADD  CONSTRAINT [FK_ProductBranchTransfer_Branch] FOREIGN KEY([BranchId]) REFERENCES [dbo].[Branch] ([ID]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductBranchTransfer] CHECK CONSTRAINT [FK_ProductBranchTransfer_Branch]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductBranchTransfer]  WITH CHECK ADD  CONSTRAINT [FK_ProductBranchTransfer_Branch1] FOREIGN KEY([PreviousBranchId]) REFERENCES [dbo].[Branch] ([ID])
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductBranchTransfer] CHECK CONSTRAINT [FK_ProductBranchTransfer_Branch1]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductBranchTransfer]  WITH CHECK ADD  CONSTRAINT [FK_ProductBranchTransfer_Product] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId])  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductBranchTransfer] CHECK CONSTRAINT [FK_ProductBranchTransfer_Product] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductTransferMaster](
	                    [PTMId] [bigint] IDENTITY(1,1) NOT NULL,
	                    [VNo] [nvarchar](50) NOT NULL,
	                    [VDate] [date] NULL,
	                    [VTime] [time](7) NULL,
	                    [VMiti] [nvarchar](20) NULL,
	                    [PersonId] [int] NULL,
	                    [ToBranchId] [int] NULL,
	                    [NetQty] [decimal](18, 6) NULL,
	                    [NetAmount] [decimal](18, 6) NULL,
	                    [Remarks] [nvarchar](550) NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [BranchId] [int] NULL,
                     CONSTRAINT [PK_ProductTransferMaster] PRIMARY KEY CLUSTERED
                    (
	                    [PTMId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferMaster_Branch] FOREIGN KEY([ToBranchId])
                    REFERENCES [dbo].[Branch] ([ID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductTransferMaster] CHECK CONSTRAINT [FK_ProductTransferMaster_Branch]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferMaster_Branch1] FOREIGN KEY([BranchId])
                        REFERENCES [dbo].[Branch] ([ID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductTransferMaster] CHECK CONSTRAINT [FK_ProductTransferMaster_Branch1]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferMaster_Person] FOREIGN KEY([PersonId])
                        REFERENCES [dbo].[Person] ([PId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductTransferMaster] CHECK CONSTRAINT [FK_ProductTransferMaster_Person]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductTransferDetails](
	                [PTDId] [bigint] IDENTITY(1,1) NOT NULL,
	                [PTMId] [bigint] NULL,
	                [VNo] [nvarchar](50) NOT NULL,
	                [SNo] [int] NULL,
	                [PId] [int] NOT NULL,
	                [PersonId] [int] NULL,
	                [ToBranchId] [int] NULL,
	                [Qty] [decimal](18, 6) NULL,
	                [Rate] [decimal](18, 6) NULL,
	                [Amount] [decimal](18, 6) NULL,
	                [Descriptions] [nvarchar](550) NULL,
                 CONSTRAINT [PK_ProductTransfer] PRIMARY KEY CLUSTERED
                (
	                [PTDId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferDetails_Branch] FOREIGN KEY([ToBranchId])
                        REFERENCES [dbo].[Branch] ([ID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductTransferDetails] CHECK CONSTRAINT [FK_ProductTransferDetails_Branch]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferDetails_Person] FOREIGN KEY([PersonId])
                        REFERENCES [dbo].[Person] ([PId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductTransferDetails] CHECK CONSTRAINT [FK_ProductTransferDetails_Person]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferDetails_Product] FOREIGN KEY([PId])
                        REFERENCES [dbo].[Product] ([PId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductTransferDetails] CHECK CONSTRAINT [FK_ProductTransferDetails_Product]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductTransferDetails_ProductTransferMaster] FOREIGN KEY([PTMId])
                REFERENCES [dbo].[ProductTransferMaster] ([PTMId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductTransferDetails] CHECK CONSTRAINT [FK_ProductTransferDetails_ProductTransferMaster]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductDeprecationMaster](
	                [PDMId] [bigint] IDENTITY(1,1) NOT NULL,
	                [VNo] [nvarchar](50) NOT NULL,
	                [VDate] [date] NULL,
	                [VTime] [time](7) NULL,
	                [VMiti] [nvarchar](20) NULL,
	                [PGId] [int] NULL,
	                [PoolId] [int] NULL,
	                [NetDepAmt] [decimal](18, 6) NULL,
	                [NetBalAmt] [decimal](18, 6) NULL,
	                [Remarks] [nvarchar](550) NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
	                [BranchId] [int] NULL,
                 CONSTRAINT [PK_ProductDeprecationMaster] PRIMARY KEY CLUSTERED
                (
	                [PDMId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationMaster_Branch] FOREIGN KEY([BranchId])
                REFERENCES [dbo].[Branch] ([ID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationMaster] CHECK CONSTRAINT [FK_ProductDeprecationMaster_Branch]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationMaster_PoolMaster] FOREIGN KEY([PoolId])
                    REFERENCES [dbo].[PoolMaster] ([PID])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationMaster] CHECK CONSTRAINT [FK_ProductDeprecationMaster_PoolMaster]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationMaster]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationMaster_ProductGroup] FOREIGN KEY([PGId])
                REFERENCES [dbo].[ProductGroup] ([PGID])
            ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationMaster] CHECK CONSTRAINT [FK_ProductDeprecationMaster_ProductGroup]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductDeprecationDetails](
	                    [PDDId] [bigint] IDENTITY(1,1) NOT NULL,
	                    [PDMId] [bigint] NOT NULL,
	                    [VNo] [nvarchar](50) NOT NULL,
	                    [SNo] [int] NOT NULL,
	                    [PId] [int] NOT NULL,
	                    [PGId] [int] NULL,
	                    [PoolId] [int] NOT NULL,
	                    [DrGlCode] [nvarchar](50) NOT NULL,
	                    [CrGlCode] [nvarchar](50) NOT NULL,
	                    [LastValue] [decimal](18, 6) NULL,
	                    [DPRate] [decimal](18, 6) NULL,
	                    [DPAmount] [decimal](18, 6) NULL,
	                    [BalAmount] [decimal](18, 6) NULL,
	                    [Descriptions] [nvarchar](550) NULL,
                     CONSTRAINT [PK_ProductDeprecationDetails] PRIMARY KEY CLUSTERED
                    (
	                    [PDDId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationDetails_PoolMaster] FOREIGN KEY([PoolId])
                        REFERENCES [dbo].[PoolMaster] ([PID])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails] CHECK CONSTRAINT [FK_ProductDeprecationDetails_PoolMaster]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationDetails_Product] FOREIGN KEY([PId])
                        REFERENCES [dbo].[Product] ([PId])
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails] CHECK CONSTRAINT [FK_ProductDeprecationDetails_Product]
                         ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationDetails_ProductDeprecationMaster] FOREIGN KEY([PDMId])
                        REFERENCES [dbo].[ProductDeprecationMaster] ([PDMId])
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails] CHECK CONSTRAINT [FK_ProductDeprecationDetails_ProductDeprecationMaster]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails]  WITH CHECK ADD  CONSTRAINT [FK_ProductDeprecationDetails_ProductGroup] FOREIGN KEY([PGId])
                REFERENCES [dbo].[ProductGroup] ([PGID])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductDeprecationDetails] CHECK CONSTRAINT [FK_ProductDeprecationDetails_ProductGroup]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[OpeningBalance](
	                [OBId] [int] IDENTITY(1,1) NOT NULL,
	                [VNo] [nvarchar](50) NOT NULL,
	                [VDate] [datetime] NOT NULL,
	                [VTime] [time](7) NULL,
	                [VMiti] [nvarchar](20) NULL,
	                [SNo] [int] NULL,
	                [PId] [int] NOT NULL,
	                [PersonId] [int] NULL,
	                [Qty] [decimal](18, 6) NULL,
	                [Rate] [decimal](18, 6) NULL,
	                [Amount] [decimal](18, 6) NULL,
	                [Remarks] [nvarchar](550) NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_OpeningBalance] PRIMARY KEY CLUSTERED
                (
	                [OBId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[OpeningBalance]  WITH CHECK ADD  CONSTRAINT [FK_OpeningBalance_OpeningBalance] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[OpeningBalance] CHECK CONSTRAINT [FK_OpeningBalance_OpeningBalance] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[OpeningBalance]  WITH CHECK ADD  CONSTRAINT [FK_OpeningBalance_Person] FOREIGN KEY([PersonId]) REFERENCES [dbo].[Person] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[OpeningBalance] CHECK CONSTRAINT [FK_OpeningBalance_Person] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[PurchaseMaster](
	            [PMId] [bigint] IDENTITY(1,1) NOT NULL,
	            [VNo] [nvarchar](50) NOT NULL,
	            [VDate] [date] NOT NULL,
	            [VTime] [time](7) NULL,
	            [VMiti] [nvarchar](50) NULL,
	            [RefNo] [nvarchar](50) NULL,
	            [RefDate] [datetime] NULL,
	            [SId] [int] NULL,
	            [NetQty] [decimal](18, 6) NULL,
	            [NetAmount] [decimal](18, 6) NULL,
	            [Remarks] [nvarchar](550) NULL,
	            [CreatedBy] [nvarchar](50) NULL,
	            [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_PurchaseMaster] PRIMARY KEY CLUSTERED
                (
	                [PMId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PurchaseMaster]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseMaster_PurchaseMaster] FOREIGN KEY([SId]) REFERENCES [dbo].[Supplier] ([SId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PurchaseMaster] CHECK CONSTRAINT [FK_PurchaseMaster_PurchaseMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[PurchaseDetails](
	            [PDId] [bigint] IDENTITY(1,1) NOT NULL,
	            [PMId] [bigint] NULL,
	            [VNo] [nvarchar](50) NOT NULL,
	            [SNo] [int] NULL,
	            [PId] [int] NOT NULL,
	            [PersonId] [int] NULL,
	            [Qty] [decimal](18, 6) NULL,
	            [Rate] [decimal](18, 6) NULL,
	            [Amount] [decimal](18, 6) NULL,
	            [Descriptions] [nvarchar](550) NULL,
                 CONSTRAINT [PK_PurchaseDetails] PRIMARY KEY CLUSTERED
                (
	                [PDId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PurchaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetails_Person] FOREIGN KEY([PersonId]) REFERENCES [dbo].[Person] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PurchaseDetails] CHECK CONSTRAINT [FK_PurchaseDetails_Person] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PurchaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetails_PurchaseDetails] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PurchaseDetails] CHECK CONSTRAINT [FK_PurchaseDetails_PurchaseDetails] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PurchaseDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetails_PurchaseMaster] FOREIGN KEY([PMId]) REFERENCES [dbo].[PurchaseMaster] ([PMId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PurchaseDetails] CHECK CONSTRAINT [FK_PurchaseDetails_PurchaseMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[IssueMaster](
	            [IMId] [bigint] IDENTITY(1,1) NOT NULL,
	            [VNo] [nvarchar](50) NOT NULL,
	            [VDate] [date] NULL,
	            [VTime] [time](7) NULL,
	            [VMiti] [nvarchar](20) NULL,
	            [PersonId] [int] NULL,
	            [NetQty] [decimal](18, 6) NULL,
	            [NetAmount] [decimal](18, 6) NULL,
	            [Remarks] [nvarchar](550) NULL,
	            [CreatedBy] [nvarchar](50) NULL,
	            [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_IssueMaster] PRIMARY KEY CLUSTERED
                (
	                [IMId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[IssueMaster]  WITH CHECK ADD  CONSTRAINT [FK_IssueMaster_Person] FOREIGN KEY([PersonId]) REFERENCES [dbo].[Person] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[IssueMaster] CHECK CONSTRAINT [FK_IssueMaster_Person] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[IssueDetails](
	            [DId] [bigint] IDENTITY(1,1) NOT NULL,
	            [IMId] [bigint] NULL,
	            [VNo] [nvarchar](50) NOT NULL,
	            [SNo] [int] NULL,
	            [PId] [int] NOT NULL,
	            [PersonId] [int] NULL,
	            [Qty] [decimal](18, 6) NULL,
	            [Rate] [decimal](18, 6) NULL,
	            [Amount] [decimal](18, 6) NULL,
	            [Descriptions] [nvarchar](550) NULL,
                 CONSTRAINT [PK_IssueDetails] PRIMARY KEY CLUSTERED
                (
	                [DId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[IssueDetails]  WITH CHECK ADD  CONSTRAINT [FK_IssueDetails_IssueMaster] FOREIGN KEY([IMId]) REFERENCES [dbo].[IssueMaster] ([IMId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ALTER TABLE [dbo].[IssueDetails] CHECK CONSTRAINT [FK_IssueDetails_IssueMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[IssueDetails]  WITH CHECK ADD  CONSTRAINT [FK_IssueDetails_Person] FOREIGN KEY([PersonId]) REFERENCES [dbo].[Person] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[IssueDetails] CHECK CONSTRAINT [FK_IssueDetails_Person] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[IssueDetails]  WITH CHECK ADD  CONSTRAINT [FK_IssueDetails_Product] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[IssueDetails] CHECK CONSTRAINT [FK_IssueDetails_Product] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductLoanInsPaidVoucher](
	            [VNo] [nvarchar](50) NOT NULL,
	            [VDate] [datetime] NOT NULL,
	            [VMiti] [nvarchar](10) NULL,
	            [VType] [nvarchar](50) NULL,
	            [BankName] [nvarchar](100) NULL,
	            [Bank_GlCode] [varchar](25) NULL,
	            [BankBranch] [nvarchar](100) NULL,
	            [ChqNo] [nvarchar](50) NULL,
	            [ChqDate] [datetime] NULL,
	            [ChqMiti] [nvarchar](10) NULL,
	            [Amount] [decimal](18, 8) NULL,
	            [DepositedBy] [nvarchar](50) NULL,
	            [Notes] [nvarchar](1024) NULL,
	            [PId] [int] NULL,
	            [PLI_Id] [bigint] NULL,
                 CONSTRAINT [PK_ProductLoanInsPaidVoucher] PRIMARY KEY CLUSTERED
                (
	                [VNo] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInsPaidVoucher]  WITH CHECK ADD  CONSTRAINT [FK_ProductLoanInsPaidVoucher_Product] FOREIGN KEY([PId]) REFERENCES [dbo].[Product] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInsPaidVoucher] CHECK CONSTRAINT [FK_ProductLoanInsPaidVoucher_Product] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInsPaidVoucher]  WITH CHECK ADD  CONSTRAINT [FK_ProductLoanInsPaidVoucher_ProductLoanInstallment] FOREIGN KEY([PLI_Id]) REFERENCES [dbo].[ProductLoanInstallment] ([PLI_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductLoanInsPaidVoucher] CHECK CONSTRAINT [FK_ProductLoanInsPaidVoucher_ProductLoanInstallment]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DailyLogMaster](
	                    [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [Logno] [bigint] NOT NULL,
	                    [DDate] [datetime] NULL,
	                    [DMiti] [varchar](10) NULL,
	                    [BranchId] [int] NULL,
	                    [Remarks] [varchar](1000) NULL,
	                    [CreatedBy] [varchar](100) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [Active] [bit] NULL
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DailyLogHH](
	                        [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                        [MId] [int] NULL,
	                        [SNo] [int] NULL,
	                        [PId] [int] NULL,
	                        [RunHour] [decimal](16, 6) NULL,
	                        [Rate] [decimal](16, 6) NULL,
	                        [Amount] [decimal](16, 6) NULL,
	                        [Narration] [varchar](1000) NULL,
	                        [LogNo] [bigint] NULL
                        ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DailyLogKM](
	                        [Id] [bigint] IDENTITY(1,1) NOT NULL,
	                        [MId] [int] NULL,
	                        [SNo] [int] NULL,
	                        [PId] [int] NULL,
	                        [StartKm] [decimal](16, 6) NULL,
	                        [EndKm] [decimal](16, 6) NULL,
	                        [RunningKm] [decimal](16, 6) NULL,
	                        [Narration] [varchar](1000) NULL,
	                        [LogNo] [bigint] NULL
                        ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ProductRenewal](
	                    [PR_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                    [EntryDate] [datetime] NULL,
	                    [PId] [int] NOT NULL,
	                    [RAmount] [decimal](16, 6) NULL,
	                    [RDate] [datetime] NULL,
	                    [RValid] [datetime] NULL,
	                    [RMiti] [varchar](10) NULL,
	                    [RValidMiti] [varchar](10) NULL,
	                    [Office] [varchar](512) NULL,
	                    [IAmt] [decimal](16, 6) NULL,
	                    [IDate] [datetime] NULL,
	                    [IMiti] [varchar](10) NULL,
	                    [IValidMiti] [varchar](10) NULL,
	                    [IValid] [datetime] NULL,
	                    [IinCom] [varchar](512) NULL,
	                    [Address] [varchar](512) NULL,
	                    [BankName] [varchar](512) NULL,
	                    [SysBranchName] [varchar](512) NULL,
	                    [Branch_Id] [int] NULL,
	                    [CreatedBy] [nvarchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                     CONSTRAINT [PK_ProductRenewal] PRIMARY KEY CLUSTERED
                    (
	                    [PR_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[ProductRenewal]  WITH CHECK ADD  CONSTRAINT [FK_ProductRenewal_Product] FOREIGN KEY([PId])REFERENCES [dbo].[Product] ([PId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[ProductRenewal] CHECK CONSTRAINT [FK_ProductRenewal_Product] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                        CREATE TABLE [dbo].[DailyLogAccountPostMaster](
	                        [DLMId] [bigint] IDENTITY(1,1) NOT NULL,
	                        [VNo] [nvarchar](50) NOT NULL,
	                        [VDate] [date] NULL,
	                        [VTime] [time](7) NULL,
	                        [VMiti] [nvarchar](20) NULL,
	                        [DMonth] [nvarchar](20) NULL,
	                        [MMonth] [nvarchar](20) NULL,
	                        [PId] [int] NULL,
	                        [NetRun] [decimal](18, 6) NULL,
	                        [NetAmt] [decimal](18, 6) NULL,
	                        [Remarks] [nvarchar](550) NULL,
	                        [CreatedBy] [nvarchar](50) NULL,
	                        [CreatedDate] [datetime] NULL,
	                        [BranchId] [int] NULL,
                         CONSTRAINT [PK_DailyLogAccountPostMaster] PRIMARY KEY CLUSTERED
                        (
	                        [DLMId] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                        ) ON [PRIMARY]

                        ALTER TABLE [dbo].[DailyLogAccountPostMaster]  WITH CHECK ADD  CONSTRAINT [FK_DailyLogAccountPostMaster_Branch] FOREIGN KEY([BranchId])
                        REFERENCES [dbo].[Branch] ([ID])
                        ALTER TABLE [dbo].[DailyLogAccountPostMaster] CHECK CONSTRAINT [FK_DailyLogAccountPostMaster_Branch]

                        ALTER TABLE [dbo].[DailyLogAccountPostMaster]  WITH CHECK ADD  CONSTRAINT [FK_DailyLogAccountPostMaster_Product] FOREIGN KEY([PId])
                        REFERENCES [dbo].[Product] ([PID])
                        ALTER TABLE [dbo].[DailyLogAccountPostMaster] CHECK CONSTRAINT [FK_DailyLogAccountPostMaster_Product]

                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                        CREATE TABLE [dbo].[DailyLogAccountPostDetails](
	                        [DLDId] [bigint] IDENTITY(1,1) NOT NULL,
	                        [DLMId] [bigint] NOT NULL,
	                        [VNo] [nvarchar](50) NOT NULL,
	                        [SNo] [int] NOT NULL,
	                        [PId] [int] NOT NULL,
	                        [PGId] [int] NULL,
	                        [PoolId] [int] NOT NULL,
	                        [DrGlCode] [nvarchar](50) NOT NULL,
	                        [CrGlCode] [nvarchar](50) NOT NULL,
	                        [RunTime] [decimal](18, 6) NULL,
	                        [Unit] [nvarchar](50) NOT NULL,
	                        [Rate] [decimal](18, 6) NULL,
	                        [Amount] [decimal](18, 6) NULL,
	                        [Descriptions] [nvarchar](550) NULL,
	                        [Post] [bit] NULL,
                         CONSTRAINT [PK_DailyLogAccountPostDetails] PRIMARY KEY CLUSTERED
                        (
	                        [DLDId] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                        ) ON [PRIMARY]

                        ALTER TABLE [dbo].[DailyLogAccountPostDetails]  WITH CHECK ADD  CONSTRAINT [FK_DailyLogAccountPostDetails_PoolMaster] FOREIGN KEY([PoolId])
                        REFERENCES [dbo].[PoolMaster] ([PID])
                        ALTER TABLE [dbo].[DailyLogAccountPostDetails] CHECK CONSTRAINT [FK_DailyLogAccountPostDetails_PoolMaster]

                        ALTER TABLE [dbo].[DailyLogAccountPostDetails]  WITH CHECK ADD  CONSTRAINT [FK_DailyLogAccountPostDetails_Product] FOREIGN KEY([PId])
                        REFERENCES [dbo].[Product] ([PId])
                        ALTER TABLE [dbo].[DailyLogAccountPostDetails] CHECK CONSTRAINT [FK_DailyLogAccountPostDetails_Product]

                        ALTER TABLE [dbo].[DailyLogAccountPostDetails]  WITH CHECK ADD  CONSTRAINT [FK_DailyLogAccountPostDetails_DailyLogAccountPostMaster] FOREIGN KEY([DLMId])
                        REFERENCES [dbo].[DailyLogAccountPostMaster] ([DLMId])
                        ALTER TABLE [dbo].[DailyLogAccountPostDetails] CHECK CONSTRAINT [FK_DailyLogAccountPostDetails_DailyLogAccountPostMaster]

                        ALTER TABLE [dbo].[DailyLogAccountPostDetails]  WITH CHECK ADD  CONSTRAINT [FK_DailyLogAccountPostDetails_ProductGroup] FOREIGN KEY([PGId])
                        REFERENCES [dbo].[ProductGroup] ([PGID])
                        ALTER TABLE [dbo].[DailyLogAccountPostDetails] CHECK CONSTRAINT [FK_DailyLogAccountPostDetails_ProductGroup]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"

                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion FixedAssets

        #region Construction Business

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderMaster](
	            [TId] [int] IDENTITY(1,1) NOT NULL,
	            [TName] [varchar](200) NOT NULL,
	            [TCode] [varchar](50) NOT NULL,
	            [TTitle] [varchar](1024) NOT NULL,
	            [ProjectDuration] [int] NULL,
	            [Remarks] [nvarchar](1024) NULL,
	            [Active] [bit] NULL,
	            [CreatedBy] [varchar](50) NULL,
	            [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK__TenderMa__3213E83F5090EFD7] PRIMARY KEY CLUSTERED
                (
	                [TId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                 CONSTRAINT [UQ__TenderMa__06DB2B815649C92D] UNIQUE NONCLUSTERED
                (
	                [TName] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                 CONSTRAINT [UQ__TenderMa__1E1FC61B536D5C82] UNIQUE NONCLUSTERED
                (
	                [TCode] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BiddingWorkSchedulingMaster](
	                [BWSId] [int] IDENTITY(1,1) NOT NULL,
	                [Work_Name] [nvarchar](100) NOT NULL,
	                [OutLineNo] [int] NULL,
	                [Remarks] [nvarchar](1024) NULL,
	                [Active] [bit] NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_BiddingWorkSchedulingMaster] PRIMARY KEY CLUSTERED
                (
	                [BWSId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BiddingWorkSubschedulingMaster](
	                [BWSSId] [int] IDENTITY(1,1) NOT NULL,
	                [SubWork_Name] [nvarchar](100) NOT NULL,
	                [BWSId] [int] NOT NULL,
	                [OutLineNo] [decimal](18, 2) NULL,
	                [Remarks] [nvarchar](1024) NULL,
	                [Active] [bit] NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                    CONSTRAINT [PK_BiddingWorkSubschedulingMaster] PRIMARY KEY CLUSTERED
                (
	                [BWSSId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BiddingWorkSubschedulingMaster]  WITH CHECK ADD  CONSTRAINT [FK_BiddingWorkSubschedulingMaster_BiddingWorkSchedulingMaster] FOREIGN KEY([BWSId])
                        REFERENCES [dbo].[BiddingWorkSchedulingMaster] ([BWSId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[BiddingWorkSubschedulingMaster] CHECK CONSTRAINT [FK_BiddingWorkSubschedulingMaster_BiddingWorkSchedulingMaster]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingMaster](
	                [TBM_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [Bidding_Date] [datetime] NULL,
	                [Bidding_Miti] [nvarchar](10) NULL,
	                [TId] [int] NULL,
	                [Tender_No] [nvarchar](50) NULL,
	                [Job_Location] [nvarchar](250) NULL,
	                [Publish_Date] [datetime] NULL,
	                [Publish_Miti] [nvarchar](10) NULL,
	                [BidForm_FileName] [nvarchar](250) NULL,
	                [Drowing_FileName] [nvarchar](250) NULL,
	                [Agreement_FileName] [nvarchar](250) NULL,
	                [Schedule_FileName] [nvarchar](250) NULL,
	                [Attachment1_FileName] [nvarchar](250) NULL,
	                [Attachment2_FileName] [nvarchar](250) NULL,
	                [Attachment3_FileName] [nvarchar](250) NULL,
	                [Attachment4_FileName] [nvarchar](250) NULL,
	                [Total_NetQty] [decimal](18, 8) NULL,
	                [Total_NetAmount] [decimal](18, 8) NULL,
	                [Special_Note] [nvarchar](1024) NULL,
	                [Branch_Id] [int] NULL,
	                [Created_By] [nvarchar](50) NULL,
	                [Created_Date] [datetime] NULL,
                 CONSTRAINT [PK_TenderBiddingMaster] PRIMARY KEY CLUSTERED
                (
	                [TBM_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[TenderBiddingMaster]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingMaster_TenderMaster] FOREIGN KEY([TId]) REFERENCES [dbo].[TenderMaster] ([TId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingMaster] CHECK CONSTRAINT [FK_TenderBiddingMaster_TenderMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingDetails](
	                [TBD_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [TBM_Id] [bigint] NOT NULL,
	                [SNo] [int] NULL,
	                [Item] [nvarchar](250) NOT NULL,
	                [Qty] [decimal](18, 8) NOT NULL,
	                [Unit] [nvarchar](50) NULL,
	                [Rate] [decimal](18, 8) NULL,
	                [Amount] [decimal](18, 8) NULL,
	                [Narration] [nvarchar](512) NULL,
                 CONSTRAINT [PK_TenderBiddingDetails] PRIMARY KEY CLUSTERED
                (
	                [TBD_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[TenderBiddingDetails]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingDetails_TenderBiddingDetails] FOREIGN KEY([TBM_Id]) REFERENCES [dbo].[TenderBiddingMaster] ([TBM_Id])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[TenderBiddingDetails] CHECK CONSTRAINT [FK_TenderBiddingDetails_TenderBiddingDetails]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingMaterialDetails](
	                [TBDM_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [TBM_Id] [bigint] NOT NULL,
	                [SNo] [int] NULL,
	                [Item] [nvarchar](250) NOT NULL,
	                [Qty] [decimal](18, 8) NOT NULL,
	                [Unit] [nvarchar](50) NULL,
	                [Rate] [decimal](18, 8) NULL,
	                [Amount] [decimal](18, 8) NULL,
	                [Narration] [nvarchar](512) NULL,
                     CONSTRAINT [PK_TenderBiddingMaterialDetails] PRIMARY KEY CLUSTERED
                    (
	                    [TBDM_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingMaterialDetails]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingMaterialDetails_TenderBiddingMaster] FOREIGN KEY([TBM_Id]) REFERENCES [dbo].[TenderBiddingMaster] ([TBM_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingMaterialDetails] CHECK CONSTRAINT [FK_TenderBiddingMaterialDetails_TenderBiddingMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingSubContractDetails](
	                [TBDSC_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [TBM_Id] [bigint] NOT NULL,
	                [SNo] [int] NULL,
	                [Item] [nvarchar](250) NOT NULL,
	                [Qty] [decimal](18, 8) NOT NULL,
	                [Unit] [nvarchar](50) NULL,
	                [Rate] [decimal](18, 8) NULL,
	                [Amount] [decimal](18, 8) NULL,
	                [Narration] [nvarchar](512) NULL,
                 CONSTRAINT [PK_TenderBiddingSubContractDetails] PRIMARY KEY CLUSTERED
                (
	                [TBDSC_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingSubContractDetails]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingSubContractDetails_TenderBiddingMaster] FOREIGN KEY([TBM_Id]) REFERENCES [dbo].[TenderBiddingMaster] ([TBM_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[TenderBiddingSubContractDetails] CHECK CONSTRAINT [FK_TenderBiddingSubContractDetails_TenderBiddingMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingLaborDetails](
	                [TBDL_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [TBM_Id] [bigint] NOT NULL,
	                [SNo] [int] NULL,
	                [Item] [nvarchar](250) NOT NULL,
	                [Qty] [decimal](18, 8) NOT NULL,
	                [Unit] [nvarchar](50) NULL,
	                [Rate] [decimal](18, 8) NULL,
	                [Amount] [decimal](18, 8) NULL,
	                [Narration] [nvarchar](512) NULL,
                     CONSTRAINT [PK_TenderBiddingLaborDetails] PRIMARY KEY CLUSTERED
                    (
	                    [TBDL_Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingLaborDetails]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingLaborDetails_TenderBiddingLaborDetails] FOREIGN KEY([TBM_Id]) REFERENCES [dbo].[TenderBiddingMaster] ([TBM_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingLaborDetails] CHECK CONSTRAINT [FK_TenderBiddingLaborDetails_TenderBiddingLaborDetails] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingEquipmentDetails](
	                [TBDE_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [TBM_Id] [bigint] NOT NULL,
	                [SNo] [int] NULL,
	                [Item] [nvarchar](250) NOT NULL,
	                [Qty] [decimal](18, 8) NOT NULL,
	                [Unit] [nvarchar](50) NULL,
	                [Rate] [decimal](18, 8) NULL,
	                [Amount] [decimal](18, 8) NULL,
	                [Narration] [nvarchar](512) NULL,
                 CONSTRAINT [PK_TenderBiddingEquipmentDetails] PRIMARY KEY CLUSTERED
                (
	                [TBDE_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingEquipmentDetails]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingEquipmentDetails_TenderBiddingEquipmentDetails] FOREIGN KEY([TBM_Id]) REFERENCES [dbo].[TenderBiddingMaster] ([TBM_Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[TenderBiddingEquipmentDetails] CHECK CONSTRAINT [FK_TenderBiddingEquipmentDetails_TenderBiddingEquipmentDetails]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TenderBiddingSpecialNote](
	                [TBSN_Id] [bigint] IDENTITY(1,1) NOT NULL,
	                [TBM_Id] [bigint] NOT NULL,
	                [Remarks] [nvarchar](1024) NULL,
	                [CreatedBy] [nvarchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK_TenderBiddingSpecialNote] PRIMARY KEY CLUSTERED
                (
	                [TBSN_Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[TenderBiddingSpecialNote]  WITH CHECK ADD  CONSTRAINT [FK_TenderBiddingSpecialNote_TenderBiddingMaster] FOREIGN KEY([TBM_Id]) REFERENCES [dbo].[TenderBiddingMaster] ([TBM_Id])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[TenderBiddingSpecialNote] CHECK CONSTRAINT [FK_TenderBiddingSpecialNote_TenderBiddingMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE FUNCTION [dbo].[FPass1]  ( @txt varchar(50)  )
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
                    end ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion Construction Business

        #region LAB

        try
        {
            Sql = @"CREATE TABLE [dbo].[DoctorMaster](
	                [DId] [int] IDENTITY(1,1) NOT NULL,
	                [DName] [varchar](250) NOT NULL,
	                [NMCNo] [varchar](100) NOT NULL,
	                [DType] [varchar](100) NULL,
	                [Specialization] [varchar](100) NULL,
	                [DAddress] [varchar](250) NULL,
	                [ContactNo] [varchar](50) NULL,
	                [MobileNo] [varchar](50) NULL,
	                [Email] [varchar](50) NULL,
	                [Remarks] [varchar](100) NULL,
	                [Active] [char](1) NULL,
	                [CreatedBy] [varchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [DId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                UNIQUE NONCLUSTERED
                (
	                [NMCNo] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]   ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Occuption](
	                    [Oid] [int] IDENTITY(1,1) NOT NULL,
	                    [OName] [varchar](100) NOT NULL,
	                    [OCode] [varchar](50) NOT NULL,
	                    [Remarks] [varchar](100) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Oid] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [OName] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"  CREATE TABLE [dbo].[Relation](
	                [Rid] [int] IDENTITY(1,1) NOT NULL,
	                [RName] [varchar](100) NOT NULL,
	                [RCode] [varchar](50) NOT NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Rid] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                UNIQUE NONCLUSTERED
                (
	                [RName] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" CREATE TABLE [dbo].[MemberType](
	                [MTId] [int] IDENTITY(1,1) NOT NULL,
	                [MTName] [varchar](100) NOT NULL,
	                [MTCode] [varchar](50) NOT NULL,
	                [MType] [varchar](100) NULL,
	                [Remarks] [varchar](100) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [MTId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                UNIQUE NONCLUSTERED
                (
	                [MTName] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" CREATE TABLE [dbo].[Member](
	                [MId] [int] IDENTITY(1,1) NOT NULL,
	                [MName] [varchar](250) NOT NULL,
	                [MemberNo] [varchar](50) NOT NULL,
	                [MType] [varchar](100) NULL,
	                [Occuption] [varchar](100) NULL,
	                [MAddress] [varchar](200) NULL,
	                [ContactNo] [varchar](50) NULL,
	                [MobileNo] [varchar](50) NULL,
	                [Email] [varchar](50) NULL,
	                [Remarks] [varchar](200) NULL,
	                [CreatedBy] [varchar](50) NULL,
	                [CreatedDate] [datetime] NULL,
	                [Active] [char](1) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [MId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                UNIQUE NONCLUSTERED
                (
	                [MemberNo] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[PatientMaster](
	                [PId] [int] IDENTITY(1,1) NOT NULL,
	                [PName] [varchar](200) NULL,
	                [PCode] [varchar](50) NULL,
	                [MId] [int] NULL,
	                [Relation] [varchar](50) NULL,
	                [FatherName] [varchar](200) NULL,
	                [SpouseName] [varchar](200) NULL,
	                [Age] [decimal](5, 2) NULL,
	                [Sex] [varchar](10) NULL,
	                [BloodGroup] [varchar](5) NULL,
	                [ContactNo] [varchar](50) NULL,
	                [Email] [varchar](100) NULL,
	                [PAddress] [varchar](200) NULL,
	                [DID] [int] NULL,
	                [Occuption] [varchar](50) NULL,
	                [Active] [char](1) NULL,
	                [History] [varchar](250) NULL,
	                [Comments] [varchar](250) NULL,
	                [Remarks] [varchar](250) NULL,
	                [CreatedBy] [nvarchar](50) NOT NULL,
	                [CreatedDate] [datetime] NULL,
                 CONSTRAINT [PK__PatientM__C57755402BFE89A6] PRIMARY KEY CLUSTERED
                (
	                [PId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PatientMaster]  WITH CHECK ADD  CONSTRAINT [FK__PatientMast__DID__339FAB6E] FOREIGN KEY([DID]) REFERENCES [dbo].[DoctorMaster] ([DId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PatientMaster] CHECK CONSTRAINT [FK__PatientMast__DID__339FAB6E] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PatientMaster]  WITH CHECK ADD  CONSTRAINT [FK__PatientMast__DID__6DCC4D03] FOREIGN KEY([DID]) REFERENCES [dbo].[DoctorMaster] ([DId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PatientMaster] CHECK CONSTRAINT [FK__PatientMast__DID__6DCC4D03] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[PatientMaster]  WITH CHECK ADD  CONSTRAINT [FK__PatientMast__MId__3493CFA7] FOREIGN KEY([MId]) REFERENCES [dbo].[Member] ([MId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PatientMaster] CHECK CONSTRAINT [FK__PatientMast__MId__3493CFA7] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[PatientMaster]  WITH CHECK ADD  CONSTRAINT [FK__PatientMast__MId__6EC0713C] FOREIGN KEY([MId]) REFERENCES [dbo].[Member] ([MId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[PatientMaster] CHECK CONSTRAINT [FK__PatientMast__MId__6EC0713C] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TestType](
	                        [Tid] [int] IDENTITY(1,1) NOT NULL,
	                        [TName] [varchar](100) NOT NULL,
	                        [TCode] [varchar](50) NOT NULL,
	                        [Remarks] [varchar](100) NULL,
	                        [CreatedBy] [varchar](100) NULL,
	                        [CreatedDate] [datetime] NULL,
                        PRIMARY KEY CLUSTERED
                        (
	                        [Tid] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                        UNIQUE NONCLUSTERED
                        (
	                        [TName] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[TestHead](
	                        [THId] [int] IDENTITY(1,1) NOT NULL,
	                        [THName] [varchar](200) NULL,
	                        [THCode] [varchar](50) NULL,
	                        [CreatedBy] [varchar](50) NULL,
	                        [CreatedDate] [datetime] NULL,
                        PRIMARY KEY CLUSTERED
                        (
	                        [THId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Test](
	                [TId] [int] IDENTITY(1,1) NOT NULL,
	                [TName] [varchar](200) NOT NULL,
	                [TCode] [varchar](50) NOT NULL,
	                [TestType] [varchar](100) NULL,
	                [THId] [int] NOT NULL,
	                [RangeFrom] [varchar](10) NULL,
	                [RangeTo] [varchar](10) NULL,
	                [Rate] [money] NULL,
	                [Unit] [varchar](10) NULL,
	                [Formula] [varchar](100) NULL,
	                [LowerBound] [varchar](10) NULL,
	                [UpperBound] [varchar](100) NULL,
	                [DefaultValue] [varchar](10) NULL,
	                [Condition] [varchar](100) NULL,
	                [Method] [varchar](100) NULL,
	                [PreSample] [varchar](100) NULL,
	                [Active] [char](1) NULL,
	                [Comments] [varchar](200) NULL,
	                [Remarks] [varchar](200) NULL,
	                [CreatedBy] [varchar](100) NULL,
	                [CreatedDate] [datetime] NULL,
	                [C1M] [int] NULL,
	                [C2M] [int] NULL,
	                [C1F] [int] NULL,
	                [C2F] [int] NULL,
	                [C1ML] [varchar](50) NULL,
	                [C1MU] [varchar](50) NULL,
	                [C2ML] [varchar](50) NULL,
	                [C2MU] [varchar](50) NULL,
	                [C1FL] [varchar](50) NULL,
	                [C1FU] [varchar](50) NULL,
	                [C2FL] [varchar](50) NULL,
	                [C2FU] [varchar](50) NULL,
	                [M1] [int] NULL,
	                [M1L] [varchar](50) NULL,
	                [M1U] [varchar](50) NULL,
	                [F1] [int] NULL,
	                [F1L] [varchar](50) NULL,
	                [F1U] [varchar](50) NULL,
	                [M2] [int] NULL,
	                [M2L] [varchar](50) NULL,
	                [M2U] [varchar](50) NULL,
	                [F2] [int] NULL,
	                [F2L] [varchar](50) NULL,
	                [F2U] [varchar](50) NULL,
	                [M3] [int] NULL,
	                [M3L] [varchar](50) NULL,
	                [M3U] [varchar](50) NULL,
	                [F3] [int] NULL,
	                [F3L] [varchar](50) NULL,
	                [F3U] [varchar](50) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [TId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                UNIQUE NONCLUSTERED
                (
	                [TName] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[Test]  WITH CHECK ADD FOREIGN KEY([THId]) REFERENCES [dbo].[TestHead] ([THId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[Test]  WITH CHECK ADD FOREIGN KEY([THId]) REFERENCES [dbo].[TestHead] ([THId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[AdvanceTest](
	                [ATId] [int] IDENTITY(1,1) NOT NULL,
	                [TId] [int] NOT NULL,
	                [MaleChild1] [int] NULL,
	                [FemaleChild1] [int] NULL,
	                [MaleChildLower1] [varchar](10) NULL,
	                [MaleChildUpper1] [varchar](10) NULL,
	                [FemaleChildLower1] [varchar](10) NULL,
	                [FemaleChildUpper1] [varchar](10) NULL,
	                [MaleChild2] [int] NULL,
	                [FemaleChild2] [int] NULL,
	                [MaleChildLower2] [varchar](10) NULL,
	                [MaleChildUpper2] [varchar](10) NULL,
	                [FemaleChildLower2] [varchar](10) NULL,
	                [FemaleChildUpper2] [varchar](10) NULL,
	                [Male1] [int] NULL,
	                [Female1] [int] NULL,
	                [MaleLower1] [varchar](10) NULL,
	                [MaleUpper1] [varchar](10) NULL,
	                [FemaleLower1] [varchar](10) NULL,
	                [FemaleUpper1] [varchar](10) NULL,
	                [Male2] [int] NULL,
	                [Female2] [int] NULL,
	                [MaleLower2] [varchar](10) NULL,
	                [MaleUpper2] [varchar](10) NULL,
	                [FemaleLower2] [varchar](10) NULL,
	                [FemaleUpper2] [varchar](10) NULL,
	                [Male3] [int] NULL,
	                [Female3] [int] NULL,
	                [MaleLower3] [varchar](10) NULL,
	                [MaleUpper3] [varchar](10) NULL,
	                [FemaleLower3] [varchar](10) NULL,
	                [FemaleUpper3] [varchar](10) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [ATId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[AdvanceTest]  WITH CHECK ADD FOREIGN KEY([TId]) REFERENCES [dbo].[Test] ([TId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[AdvanceTest]  WITH CHECK ADD FOREIGN KEY([TId]) REFERENCES [dbo].[Test] ([TId]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Agent](
	                        [AgentId] [int] IDENTITY(1,1) NOT NULL,
	                        [AgentCode] [varchar](50) NULL,
	                        [AgentName] [varchar](200) NULL,
	                        [ContactNo] [varchar](100) NULL,
	                        [MobileNo] [varchar](100) NULL,
	                        [Email] [varchar](100) NULL,
	                        [CreatedBy] [varchar](100) NULL,
	                        [CreatedDate] [datetime] NULL,
	                        [IsActive] [char](1) NULL,
	                        [AgentGroup] [varchar](200) NULL,
                         CONSTRAINT [PK_Agent] PRIMARY KEY CLUSTERED
                        (
	                        [AgentId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Techinician](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](250) NOT NULL,
	                    [Code] [varchar](100) NOT NULL,
	                    [Type] [varchar](100) NULL,
	                    [Specialization] [varchar](100) NULL,
	                    [Address] [varchar](250) NULL,
	                    [ContactNo] [varchar](50) NULL,
	                    [MobileNo] [varchar](50) NULL,
	                    [Email] [varchar](50) NULL,
	                    [Remarks] [varchar](100) NULL,
	                    [Active] [char](1) NULL,
	                    [CreatedBy] [varchar](50) NULL,
	                    [CreatedDate] [datetime] NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            //Sample ClientInfo
            Sql = @"CREATE TABLE [dbo].[SCMaster](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [SampleNo] [varchar](50) NULL,
	                [Sdate] [datetime] NULL,
	                [RefBy] [int] NULL,
	                [Patient] [int] NULL,
	                [PatientName] [varchar](250) NULL,
	                [Member] [varchar](250) NULL,
	                [Techinician] [varchar](250) NULL,
	                [DelDate] [datetime] NULL,
	                [DelTime] [time](7) NULL,
	                [Agent] [varchar](250) NULL,
	                [Remarks] [varchar](500) NULL,
	                [Amount] [money] NULL,
	                [Createdby] [varchar](100) NULL
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SCDetails](
	            [SampleNo] [varchar](50) NULL,
	            [Sno] [int] NULL,
	            [Test] [varchar](250) NULL,
	            [Techinician] [varchar](250) NULL,
	            [DelDate] [datetime] NULL,
	            [DelTime] [time](7) NULL
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            //Delivery
            Sql = @"CREATE TABLE [dbo].[DMaster](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [DNo] [varchar](50) NULL,
	                [DDate] [datetime] NULL,
	                [SampleNo] [varchar](50) NULL,
	                [TName] [varchar](250) NULL,
	                [EntryBy] [varchar](250) NULL,
	                [Edate] [datetime] NULL,
	                [ETime] [time](7) NULL,
	                [Remarks] [varchar](500) NULL
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DDetails](
	                    [DNo] [varchar](50) NULL,
	                    [Sno] [int] NULL,
	                    [TId] [int] NULL,
	                    [TValue] [varchar](500) NULL,
	                    [TLower] [varchar](500) NULL,
	                    [TUpper] [varchar](500) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion LAB

        #region CRM

        try
        {
            Sql = @"CREATE TABLE [dbo].[Traffic](
	                        [Id] [int] IDENTITY(1,1) NOT NULL,
	                        [Name] [varchar](200) NULL,
	                        [Code] [varchar](50) NULL,
	                        [Remarks] [varchar](500) NULL,
                        PRIMARY KEY CLUSTERED
                        (
	                        [Id] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[SubTraffic](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](200) NULL,
	                    [Code] [varchar](50) NULL,
	                    [MId] [int] NULL,
	                    [Remarks] [varchar](500) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Driver](
	                [DType] [varchar](20) NULL,
	                [DName] [varchar](100) NULL
                ) ON [PRIMARY]  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[CSubject](
	                        [Id] [int] IDENTITY(1,1) NOT NULL,
	                        [Name] [varchar](200) NULL,
	                        [Code] [varchar](50) NULL,
	                        [Remarks] [varchar](500) NULL,
                        PRIMARY KEY CLUSTERED
                        (
	                        [Id] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                    CREATE TABLE [dbo].[Customer](
	                    [CId] [int] IDENTITY(1,1) NOT NULL,
	                    [Lastname] [varchar](50) NULL,
	                    [Firstname] [varchar](50) NULL,
	                    [MiddleName] [varchar](50) NULL,
	                    [Fathername] [varchar](100) NULL,
	                    [AgentId] [int] NULL,
	                    [Traffic] [varchar](100) NULL,
	                    [Source] [varchar](100) NULL,
	                    [Refby] [varchar](100) NULL,
	                    [History] [varchar](200) NULL,
	                    [MobileNo] [varchar](20) NULL,
	                    [dob] [datetime] NULL,
	                    [Email] [varchar](200) NULL,
	                    [EnteryDate] [datetime] NULL,
	                    [Adress] [varchar](100) NULL,
	                    [MainTraffic] [varchar](50) NULL,
	                    [MemberType] [varchar](100) NULL,
	                    [Company] [varchar](100) NULL,
	                    [pan] [varchar](30) NULL,
	                    [Name] [varchar](250) NULL,
	                    [RegNo] [varchar](50) NULL,
	                    [RegDate] [datetime] NULL,
	                    [Expdate] [datetime] NULL,
	                    [PackageType] [varchar](50) NULL,
	                    [Area] [varchar](100) NULL,
	                    [ContactNo] [varchar](50) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [CId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                 ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([AgentId]) REFERENCES [dbo].[Agent] ([AgentId])";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BusinessActivity](
	                [BId] [int] IDENTITY(1,1) NOT NULL,
	                [IDate] [datetime] NULL,
	                [IAmount] [money] NULL,
	                [Remarks] [varchar](100) NULL,
	                [CreatedBy] [varchar](100) NULL,
	                [CreatedDate] [datetime] NULL,
	                [Cid] [int] NULL,
	                [Aid] [int] NULL,
	                [BillNo] [varchar](50) NULL,
	                [Delivery] [char](1) NULL,
	                [Orderno] [varchar](50) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [BId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DeliveryDetails](
	                        [DId] [int] IDENTITY(1,1) NOT NULL,
	                        [Vech] [varchar](100) NULL,
	                        [DName] [varchar](100) NULL,
	                        [TName] [varchar](100) NULL,
	                        [Feedback] [varchar](200) NULL,
	                        [CreatedBy] [varchar](100) NULL,
	                        [CreatedDate] [datetime] NULL,
	                        [BillNo] [varchar](50) NULL,
	                        [Billdate] [datetime] NULL,
	                        [Cid] [int] NULL,
	                        [DeliveryDate] [datetime] NULL,
	                        [DeliveryTime] [time](7) NULL,
	                        [DAddress] [varchar](250) NULL,
	                        [Remarks] [varchar](max) NULL,
                        PRIMARY KEY CLUSTERED
                        (
	                        [DId] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DeliveryClose](
	                    [DId] [int] IDENTITY(1,1) NOT NULL,
	                    [Vech] [varchar](100) NULL,
	                    [DName] [varchar](100) NULL,
	                    [TName] [varchar](100) NULL,
	                    [Feedback] [varchar](200) NULL,
	                    [CreatedBy] [varchar](100) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [BillNo] [varchar](50) NULL,
	                    [Billdate] [datetime] NULL,
	                    [Cid] [int] NULL,
	                    [DeliveryDate] [datetime] NULL,
	                    [DeliveryTime] [time](7) NULL,
	                    [DAddress] [varchar](250) NULL,
	                    [Remarks] [varchar](max) NULL,
	                    [returnTime] [time](7) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [DId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Area](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Name] [varchar](250) NULL,
	                    [Code] [varchar](50) NULL,
	                    [ContactNo] [varchar](250) NULL,
	                    [MobileNo] [varchar](250) NULL,
	                    [Email] [varchar](250) NULL,
	                    [CreatedBy] [varchar](50) NULL,
	                    [CreatedDate] [datetime] NULL
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DeliveryEntry](
	            [DId] [int] IDENTITY(1,1) NOT NULL,
	            [CId] [int] NULL,
	            [AgentId] [int] NULL,
	            [Mobile] [varchar](50) NULL,
	            [Subject] [varchar](100) NULL,
	            [Remarks] [varchar](200) NULL,
	            [CreatedBy] [varchar](100) NULL,
	            [CreatedDate] [datetime] NULL,
	            [DDate] [datetime] NULL,
                PRIMARY KEY CLUSTERED
                (
	                [DId] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[DeliveryEntry]  WITH CHECK ADD FOREIGN KEY([AgentId]) REFERENCES [dbo].[Agent] ([AgentId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[DeliveryEntry]  WITH CHECK ADD FOREIGN KEY([CId]) REFERENCES [dbo].[Customer] ([CId])
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" CREATE TABLE [dbo].[PackageType](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [varchar](250) NULL,
	                [Code] [varchar](50) NULL,
	                [CreatedBy] [varchar](200) NULL,
	                [Createddate] [datetime] NULL
                ) ON [PRIMARY] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Item](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [IName] [varchar](500) NULL,
	                    [ICode] [varchar](50) NULL,
	                    [Amount] [money] NULL,
	                    [Remarks] [varchar](max) NULL,
                     CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                       ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BillingMaster](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [RegNo] [varchar](50) NULL,
	                    [BillNo] [varchar](50) NULL,
	                    [Billdate] [datetime] NULL,
	                    [BillTime] [datetime] NULL,
	                    [BasicAmount] [money] NULL,
	                    [Tax] [money] NULL,
	                    [Discount] [money] NULL,
	                    [Net] [money] NULL,
	                    [Remarks] [varchar](500) NULL,
	                    [CreatedBy] [varchar](50) NULL,
	                    [Createddate] [datetime] NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[BillDetails](
	                    [BillNo] [varchar](50) NULL,
	                    [Code] [varchar](50) NULL,
	                    [qty] [money] NULL,
	                    [Rate] [money] NULL,
	                    [Tax] [money] NULL,
	                    [Discount] [money] NULL,
	                    [Total] [money] NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion CRM

        #region Document Mangt

        try
        {
            Sql = @"Create table callRegister
                (
                Id int identity(1,1) Primary Key,
                EnteredDate DateTime,
                PhoneNumber varchar(20),
                Mobile varchar(40),
                FullName Varchar(500),
                Company Varchar(500),
                CAdd varchar(500),
                Purposeofcall varchar(500),
                ToWhom Varchar(100),
                email varchar(200),
                CreatedBy Varchar(100),
                Remarks Varchar(1000)
                )
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc Sp_CalRegister @PhNo varchar(50),@Mob Varchar(50)
                        as
                        Select EnteredDate,PhoneNumber,Mobile,FullName,Company,CAdd as Address,Purposeofcall,ToWhom,email,Remarks from callRegister
                        where PhoneNumber=@phNo and Mobile=@Mob
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Table DocumentType
                (
                Id int identity(1,1),
                DType Varchar(500),
                DCode Varchar(50),
                Remarks Varchar(500)
                )
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Table DocumentMgt
                (
                Id Int Identity(1,1) Primary key,
                DDate DatetIme,
                FileType Varchar(50),
                Department int,
                DocumentType Int,
                RivisionComment Varchar(max),
                DDesc Varchar(Max),
                Comment Varchar(Max),
                AssignUser Varchar(500),
                CompanyName Varchar(500),
                CAdd Varchar(500),
                ContactPerson Varchar(500),
                ContactNo Varchar(1000),
                CourierName Varchar(1000),
                CourierNO Varchar(1000)
                )
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc Sp_DocMgt @Desc varchar(500) as
                    select Id,DDate as Date,FileType,RivisionComment,DDesc as Description,Comment,AssignUser from DocumentMgt
                    where DDesc like @Desc + '%'
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion Document Mangt

        try
        {
            Sql = @"CREATE TABLE [dbo].[LLevel](
	                [LId] [int] IDENTITY(1,1) NOT NULL,
	                [LName] [varchar](100) NOT NULL,
	                [LCode] [varchar](50) NOT NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [LId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [LName] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Post](
	                    [PId] [int] IDENTITY(1,1) NOT NULL,
	                    [PName] [varchar](100) NOT NULL,
	                    [PCode] [varchar](50) NOT NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [PId] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
                    UNIQUE NONCLUSTERED
                    (
	                    [PName] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EPMaster](
	            [Id] [int] IDENTITY(1,1) NOT NULL,
	            [TNo] [varchar](10) NULL,
	            [Tdate] [datetime] NULL,
	            [Eid] [int] NULL,
	            [MName] [varchar](10) NULL,
	            [Yname] [varchar](4) NULL,
	            [Remarks] [varchar](max) NULL,
                PRIMARY KEY CLUSTERED
                (
	                [Id] ASC
                )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                ) ON [PRIMARY]
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EPMaster]  WITH CHECK ADD  CONSTRAINT [FK__EPMaster__Eid__214BF109] FOREIGN KEY([Eid]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EPMaster] CHECK CONSTRAINT [FK__EPMaster__Eid__214BF109] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"ALTER TABLE [dbo].[EPMaster]  WITH CHECK ADD  CONSTRAINT [FK_EPMaster] FOREIGN KEY([Eid]) REFERENCES [dbo].[Employee] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER TABLE [dbo].[EPMaster] CHECK CONSTRAINT [FK_EPMaster] ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[EPDetails](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [TNo] [varchar](10) NULL,
	                    [Tdate] [datetime] NULL,
	                    [Pid] [int] NULL,
	                    [Qty] [money] NULL,
	                    [Rate] [money] NULL,
	                    [Amount] [money] NULL,
	                    [Narration] [varchar](500) NULL,
                    PRIMARY KEY CLUSTERED
                    (
	                    [Id] ASC
                    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                    ) ON [PRIMARY]

                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER TABLE [dbo].[EPDetails]  WITH CHECK ADD FOREIGN KEY([Pid]) REFERENCES [dbo].[Item] ([Id]) ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[WR](
	                        [name] [varchar](500) NULL,
	                        [wh] [time](7) NULL,
	                        [whr] [time](7) NULL,
	                        [LT] [time](7) NULL,
	                        [TT] [time](7) NULL
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[temptable1](
	                    [Ram] [varchar](5) NULL,
	                    [F2] [varchar](8) NULL,
	                    [test] [varchar](7) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[temptable2](
	                    [Ram] [varchar](5) NULL,
	                    [F2] [varchar](8) NULL,
	                    [test] [varchar](7) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[temptable](
	                        [Ram] [varchar](5) NULL,
	                        [F2] [varchar](8) NULL,
	                        [test] [varchar](7) NULL
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[Copier](
	                        [ID] [int] IDENTITY(1,1) NOT NULL,
	                        [FileName] [nvarchar](max) NULL,
	                        [FullPath] [nvarchar](max) NULL,
	                        [FromDate] [datetime] NULL,
	                        [ToDate] [datetime] NULL,
	                        [Reports_Type] [char](1) NULL,
	                        [Report_Name] [nvarchar](max) NULL,
                         CONSTRAINT [PK_Copier] PRIMARY KEY CLUSTERED
                        (
	                        [ID] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[CashReceipt](
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [VNo] [varchar](50) NULL,
	                    [Vdate] [datetime] NULL,
	                    [BillNo] [varchar](50) NULL,
	                    [Amount] [money] NULL,
	                    [PayType] [varchar](50) NULL,
	                    [BankName] [varchar](200) NULL,
	                    [Holder] [varchar](250) NULL,
	                    [cardNo] [varchar](50) NULL,
	                    [ExpDate] [datetime] NULL,
	                    [Remarks] [varchar](1000) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[CashPayment](
	                    [PaymentNo] [varchar](50) NULL,
	                    [PaymentDate] [date] NULL,
	                    [PaymentTime] [time](7) NULL,
	                    [OpdBillNo] [varchar](50) NULL,
	                    [PatientCode] [varchar](50) NULL,
	                    [PaymentAmount] [money] NULL,
	                    [PaymentMode] [varchar](50) NULL,
	                    [DepartmentCode] [varchar](50) NULL,
	                    [BankName] [varchar](1000) NULL,
	                    [CardHolder] [varchar](1000) NULL,
	                    [CardNo] [varchar](100) NULL,
	                    [ExpDate] [date] NULL,
	                    [Status] [char](10) NULL,
	                    [CreatedDate] [datetime] NULL,
	                    [CreatedBy] [varchar](50) NULL,
	                    [ModifyBy] [varchar](50) NULL,
	                    [ModifyDate] [datetime] NULL,
	                    [AuditLog] [varchar](max) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[App](
	                    [CustomerName] [varchar](100) NULL,
	                    [AgentName] [varchar](100) NULL,
	                    [Traffic] [varchar](100) NULL,
	                    [Subject] [varchar](100) NULL,
	                    [AppDate] [datetime] NULL,
	                    [AppTime] [time](7) NULL,
	                    [purchase] [char](1) NULL,
	                    [Remarks] [varchar](500) NULL,
	                    [Type] [varchar](50) NULL,
	                    [Id] [int] IDENTITY(1,1) NOT NULL,
	                    [Address] [varchar](512) NULL,
	                    [Package] [varchar](512) NULL,
	                    [PhoneNo] [varchar](20) NULL,
	                    [MobileNo] [varchar](20) NULL,
	                    [Dob] [datetime] NULL,
	                    [RegDate] [datetime] NULL,
	                    [ExpDate] [datetime] NULL,
	                    [Area] [varchar](100) NULL,
	                    [RegNo] [varchar](50) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[AgentImport](
	                    [Name] [varchar](500) NULL,
	                    [ContactNo] [varchar](50) NULL,
	                    [MobileNo] [varchar](50) NULL,
	                    [Email] [varchar](200) NULL,
	                    [AgentGroup] [varchar](500) NULL,
	                    [Designation] [varchar](200) NULL,
	                    [Active] [char](1) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DeliveryDetailsVech](
	                    [BillNo] [varchar](50) NULL,
	                    [DName] [varchar](100) NULL
                    ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DeliveryDetailsTech](
	                        [BillNo] [varchar](50) NULL,
	                        [DName] [varchar](100) NULL
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[DeliveryDetailsDriver](
	                        [BillNo] [varchar](50) NULL,
	                        [DName] [varchar](100) NULL
                        ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"CREATE TABLE [dbo].[ItemProduct](
	                [Id] [int] IDENTITY(1,1) NOT NULL,
	                [Name] [varchar](250) NULL,
	                [Code] [varchar](50) NULL,
	                [Rate] [money] NULL,
	                [CreatedBy] [varchar](200) NULL,
	                [Createddate] [datetime] NULL
                ) ON [PRIMARY]
                        ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
        catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
        {
        }
    }

    public static void CreateStoreProcedure()
    {
        var Sql = string.Empty;
        try
        {
            Sql = @"Create Proc [dbo].[sp_InsertUserMaster]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_InserttblGlobal]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_DailyAttendance]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[RepAtt1]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[RepAtt]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_DailyAttendanceSheet1]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_DailyAttendanceSheet]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_LoadAtt]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        //            try
        //            {
        //                Sql = @"Create Proc [dbo].[Sp_LoadAttIndivisualWorkingDaysNOverTime]  as select @@servername
        //                    ";
        //                SqlCommand cmd = new SqlCommand(Sql, ClsSeverConnection.GetConnection());
        //                cmd.CommandTimeout = 500;
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch
        //            { }
        try
        {
            Sql = @"Create Proc [dbo].[Usp_Select_Data]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_InsertCashReceipt] as select @@servername  ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_InsertCashPayment]  as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_InsertBillMaster] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_InsertBillDetails] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_BillingList] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_DueBillingList] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_InsertLabInfo] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_InsertDoctorMaster] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_FetchLabInfo] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_FetchDoctorMaster] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_CustomerList] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_CustomerExp] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_CashReceipt] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[sp_FetchUserMaster] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Create Proc [dbo].[Sp_FetchUser] as select @@servername
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }
    }

    public static void CreateAlterTable()
    {
        var Sql = string.Empty;
        try
        {
            Sql = @"Alter Table Employee ADD [MCard_No] [nvarchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Employee ADD [MMachine_Id] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Employee ADD [MPrivilege] [nvarchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Employee ADD [Image] [varbinary](max) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Employee ADD [StartOTTime] [time](7) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        //            try
        //            {
        //                Sql = @"Alter Table Employee ADD [OTRate]  [decimal](18, 6) NULL
        //                    ";
        //                SqlCommand cmd = new SqlCommand(Sql, ClsSeverConnection.GetConnection());
        //                cmd.CommandTimeout = 500;
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch
        //            { }
        try
        {
            Sql =
                @"Alter Table Employee ADD [ET_Id] [int] NULL,[BSM_Id] [bigint] NULL,	[Attachment_1] [varbinary](max) NULL,
	                [Attachment_Path1] [nvarchar](100) NULL,[Attachment_2] [varbinary](max) NULL,[Attachment_Path2] [nvarchar](100) NULL
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Employee ADD [DHEId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table Employee ADD [AdvanceAccountCr] [nvarchar](50) NULL,[LoanAccountCr] [nvarchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Employee ADD TADA_Rate Decimal(18,6) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table Employee ADD [Attachment_3] [varbinary](max) NULL,[Attachment_Path3] [nvarchar](100) NULL
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table UserMaster ADD UGId int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table UserMaster ADD [UDepartmentId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table UserMaster ADD [UEId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [MEmpId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [WorkCode] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [Company] [varchar](80) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [BranchId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [IPAddress] [varchar](250) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [CreatedBy] [varchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [CreatedDate] [datetime] NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [Type] [varchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table DailyAttendance ADD [Narration] [varchar](512) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table MonthlyAtt ADD [OTHourInMonth] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table MonthlyAtt ADD [Narration] [varchar](512) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table EmployeeTravelDetails ADD [Type] [nvarchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table EmployeeTravelDetails ADD Travel_Days Decimal(18,2) NULL,TADA_Rate Decimal(18,6) NULL,TADA_Amount Decimal(18,6) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table LeaveMaster ADD Active char(1) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table LeaveEntry ADD [Leave_Days]  [decimal](18, 6) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table LeaveEntry ADD [PaidLeave_Days]  [decimal](18, 2) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table systemsetting ADD [Email] [nvarchar](150) NULL,[Password] [nvarchar](100) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table SystemSetting ADD [AdvanceAccountCr] [nvarchar](50) NULL,[LoanAccountCr] [nvarchar](50) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table HOP ADD [FormulaType] [nvarchar](50) NULL,[SelectedHOPId] [nvarchar](1024) NULL,[SelectedHOPValue] [nvarchar](1024) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table HOP ADD Active char(1) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table SalaryStructureDetails ADD SId int NULL, HId int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table SalaryStructureDetails ADD  [ValType] [varchar](50) NULL, [FormulaType] [nvarchar](50) NULL,[SelectedHOPId] [nvarchar](1024) NULL,[SelectedHOPValue] [nvarchar](1024) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table EmployeeHop ADD  HId int NULL, [ValType] [varchar](50) NULL, [FormulaType] [nvarchar](50) NULL,[SelectedHOPId] [nvarchar](1024) NULL,[SelectedHOPValue] [nvarchar](1024) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table OpeningBalance ADD [BranchId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table PurchaseMaster ADD [BranchId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table IssueMaster ADD [BranchId] int NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table PoolMaster ADD [Deprecation_Method] [nvarchar](25) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Product ADD [Deprecation_Method] [nvarchar](25) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table Product ADD Deprecation_Date Datetime Null,InitialBranch_Id int Null
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"Alter Table ProductLoanDetails ADD [Loan_GlName] [nvarchar](50) NULL,[Loan_GlCode] [varchar](25) NULL
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Table ProductDeprecationDetails ADD Post bit
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }
    }

    public static void CreateAlterStoreProcedure()
    {
        var Sql = string.Empty;
        try
        {
            Sql = @"ALTER Proc [dbo].[sp_InsertUserMaster]
                    @Flag int,
                    @UserId int,
                    @UName varchar(100),
                    @ULid Varchar(50),
                    @UPws varchar(50),
                    @UType varchar(50),
                    @UDepartment varchar(50),
                    @UAddress varchar(250),
                    @ContactNo Varchar(50),
                    @MobileNo varchar(50),
                    @Email varchar(100),
                    @Active Char(1),
                    @Remarks varchar(100)
                    as
                    if @Flag=1
                    begin
	                    Insert Into UserMaster(UName,ULid,UPws,UType,UDepartment,UAddress,ContactNo,MobileNo,Email,Active,Remarks)
		                Values
		                (@UName,@ULid,@UPws,@UType,@UDepartment,@UAddress,@ContactNo,@MobileNo,@Email,@Active,@Remarks)
                    end
                    if @Flag=2
                    begin
	                     Update UserMaster set
		                      UName=@UName,

		                      ULid=@ULid,
		                      UPws=@UPws,
		                      UType=@UType,
		                      UDepartment=@UDepartment,
		                      UAddress=@UAddress,
		                      ContactNo=@ContactNo,
		                      MobileNo=@MobileNo,
		                      Email=@Email,
		                      Active=@Active,
		                      Remarks=@Remarks
		                      where UserId=@UserId
                    end
                    if @Flag=3
                    Begin
	                    Delete from UserMaster where UserId=@UserId
                    end
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[Sp_InserttblGlobal]
                        @Flag int,
                        @Id int,
                        @ItemName varchar(50),
                        @ItemValue int,
                        @Status nvarchar(10),
                        @prefix varchar(50),
                        @TotalCount int,
                        @StartingNo int,
                        @appendCharacter nvarchar(50),
                        @CurrentNo int
                        as
                        if @Flag=1 --New
                        begin
                        delete from tblGlobal where ItemName=@ItemName
                         insert into tblGlobal(ItemName,prefix,TotalCount,StartingNo,appendCharacter,Status,ItemValue) values
                         (@ItemName,@prefix,@TotalCount,@StartingNo,@appendCharacter,'A',@CurrentNo)

                        end
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #region Payroll

        try
        {
            Sql = @"ALTER Proc [dbo].[sp_DailyAttendance] @Event int,@EmpId varchar(10),@month varchar(50)
                        as
                        DECLARE @qry NVARCHAR(4000)
                        DECLARE @cols NVARCHAR(4000)
                        DECLARE @DateType NVARCHAR(5)
                        set @DateType=(Select Top 1 DateType from systemsetting)

                        if @Event=1 ----Old Display English MonthYear name
                        Begin
                            SELECT  @cols =  COALESCE(@cols + ',[' + CONVERT(varchar, DDate, 106)+ ']','[' + CONVERT(varchar, DDate, 106) + ']')
                            FROM  DailyAttendance where DMonth=@month and Year(DDate)=Year(GetDate())
                            Group by DDate
                            ORDER BY DDate

                            Print @Cols

                            SET @qry =
                            'SELECT Name,PaymentAmount, ' + @cols + ' FROM
                            (SELECT name,PaymentAmount, DDate, Total FROM DailyAttendance,employee
                            Left outer join CashPayment on Patientcode=Employee.Id
                            where employee.Id=DailyAttendance.Eid and (''''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' ))p
                            PIVOT (MAX(Total) FOR DDate IN (' + @cols + ')) AS Pvt'

                            -- Executing the query
                            EXEC(@qry)
                        end
                        else if @Event=2
                        Begin
                            If @DateType='D'
                            Begin
                                SELECT  @cols =  COALESCE(@cols + ',[' +  CONVERT(nvarchar(5),Day(DDate)) + ']','[' + CONVERT(nvarchar(5),Day(DDate)) + ']')
                                FROM  DailyAttendance where DMonth=@month and Year(DDate)=Year(GetDate())
                                Group by DDate
                                ORDER BY DDate

                                Print @Cols

                                SET @qry =
                                'SELECT Name,DMonth MonthName,PaymentAmount, ' + @cols + ' FROM
                                (SELECT Name,DMonth,PaymentAmount,CONVERT(nvarchar(5),Day(DDate)) MDay ,Total FROM DailyAttendance Inner Join Employee On employee.Id=DailyAttendance.Eid
                                Left outer join CashPayment on Patientcode=Employee.Id
                                Where (''''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' ) and DMonth=''' + @month + ''' and Year(DDate)=Year(GetDate()) )p
                                PIVOT (MAX(Total) FOR MDay IN (' + @cols + ')) AS Pvt'
                                print @qry
                            End
                            Else
                            Begin
                                SELECT  @cols =  COALESCE(@cols + ',[' +  CONVERT(nvarchar(5),Convert(int,Left(M_Miti,2))) + ']','[' + CONVERT(nvarchar(5),Convert(int,Left(M_Miti,2))) + ']')
                                FROM  DailyAttendance as DA Inner Join DateMiti as DM On M_Date=DDate where MMonth=@month and Year(DDate)=Year(GetDate())
                                Group by DDate,M_Miti
                                ORDER BY DDate

                                Print @Cols

                                SET @qry =
                                'SELECT Name,MMonth MonthName,PaymentAmount, ' + @cols + ' FROM
                                (SELECT Name,MMonth,PaymentAmount, CONVERT(nvarchar(5),Convert(int,Left(M_Miti,2))) MDay, Total	FROM DailyAttendance
                                Inner Join Employee On employee.Id=DailyAttendance.Eid
                                Left outer join CashPayment on Patientcode=Employee.Id Inner Join DateMiti as DM On M_Date=DDate
                                where (''''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' ) and MMonth=''' + @month + ''' and Year(DDate)=Year(GetDate()))p
                                PIVOT (MAX(Total) FOR MDay IN (' + @cols + ')) AS Pvt'
                                Print @qry
                            End
                            -- Executing the query
                            EXEC(@qry)
                        End
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[RepAtt1] @Month varchar(30) as
                        DECLARE @MaxCount INTEGER
                        DECLARE @Count INTEGER
                        DECLARE @Txt VARCHAR(MAX)
                        declare @query varchar(max)
                        declare @Type varchar(100)
                        SET @Count = 1
                        --set @Month='a'
                        SET @Txt = ''
                        set @Type='Addition'
                        SET @MaxCount = (select MAX(Position) from HOP)
                        WHILE @Count<=@MaxCount
                        BEGIN
                        IF @Txt!=''
                        SET @Txt=@Txt+',' + (select '['+ Name +']' from HOP where Position=@Count )
                        ELSE
                        SET @Txt=(select '['+ Name +']' from HOP where Position=@Count )
                        SET @Count=@Count+1
                        END
                        --SELECT @Txt AS Txt

                        SET @query = 'SELECT *
                        FROM (
                        Select DISTINCT  Main.Name,MName as[Month],DaysInMonth,WorkingDays,
                        case when WorkingDays > WDays then WDays else WorkingDays end
                         as [Present Days],
                         case when WorkingDays < WDays then WDays-WorkingDays else 0 end
                         as [OverTime Days],
                        HopName,Value,convert(decimal(16,2),aa) AS TOTAL from
                        (
                        SELECT
                        Name,MName,EName,DaysInMonth,WorkingDays,WDays,HopName,convert(decimal(16,2),(EmployeeHop.VALUE/WorkingDays)*Wdays) AS VALUE
                        FROM Employee,EmployeeHop,WorkingDays
                        where Employee.Id=EmployeeHop.EId
                        and WorkingDays.Ename=Employee.Name and  [Mname]='''+ @Month +'''
                        ) as Main
                        Left outer join
                        (
                        select
                        Name,
                        sum(Case when HopType='''+ @Type +''' then (EmployeeHop.VALUE/WorkingDays)*Wdays else -(EmployeeHop.VALUE/WorkingDays)*Wdays end)as aa

                        FROM Employee,EmployeeHop,WorkingDays
                        where Employee.Id=EmployeeHop.EId and WorkingDays.Ename=Employee.Name
                        and  [Mname]='''+ @Month +'''
                        group by Name
                        ) as det on det.Name=Main.Name
                        ) as s
                         '
                        SET @query = @query  +
                        'PIVOT
                                    (
                                    Max(Value)
                                    FOR HopName IN ('+ @Txt +')
                        )AS pivott'
                        exec (@query)
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER Proc [dbo].[RepAtt] @Event int,
                        @EmpId varchar(10)='0',
                        @DepartmentId varchar(10)='0',
                        @PostId varchar(10)='0',
                        @ShiftId varchar(10)='0',
                        @Month varchar(30) as
                        --exec [RepAtt] 1,0,0,0,0,'May'
                        --exec [RepAtt] 2,0,0,0,0,'April'
                        DECLARE @MaxCount INTEGER
                        DECLARE @Count INTEGER
                        DECLARE @Position INTEGER
                        DECLARE @HId INTEGER
                        declare @HName varchar(100)
                        declare @HopType varchar(100)
                        DECLARE @Txt VARCHAR(MAX)
                        declare @query varchar(max)
                        declare @Type varchar(100)
                        SET @Count = 1
                        --set @Month='a'
                        SET @Txt = ''
                        set @Type='Addition'
                        --SET @MaxCount = (select MAX(Position) from HOP  )
                        --WHILE @Count<=@MaxCount
                        --BEGIN
                        -- IF @Txt!=''
                            --  SET @Txt=@Txt+',' + (select Distinct '['+ HOP.Name +']' from HOP where Position=@Count )
                        -- ELSE
                            --  SET @Txt=(select Distinct '['+ HOP.Name +']' from HOP where Position=@Count )
                        -- SET @Count=@Count+1
                        --END

                        DECLARE c2 INSENSITIVE CURSOR FOR
                        Select Distinct Hop.HId,Hop.Name,Position from HOP Inner Join EmployeeHOP as EHP On EHP.HId=HOP.HID Where (HOP.Active='Y' or HOP.Active is Null ) and HOP.Type='Addition' Order By HOP.Position
                        OPEN c2
                        FETCH NEXT FROM c2
                        INTO @HId,@HName,@Position
                        WHILE @@FETCH_STATUS = 0
                        BEGIN
                            IF @Txt!=''
                                SET @Txt=@Txt + ',' + '['+ @HName +']'
                            Else
                                SET @Txt= '['+ @HName +']'
                        FETCH NEXT FROM c2
                        INTO   @HId,@HName,@Position
                        END
                        DEALLOCATE c2

                        SET @Txt= @Txt + ',' + '[OT Amount]' --,[Addition Total]'

                        DECLARE c3 INSENSITIVE CURSOR FOR
                        Select Distinct Hop.HId,Hop.Name,Position from HOP Inner Join EmployeeHOP as EHP On EHP.HId=HOP.HID Where (HOP.Active='Y' or HOP.Active is Null )  and HOP.Type='Deduction' Order By HOP.Position
                        OPEN c3
                        FETCH NEXT FROM c3
                        INTO @HId,@HName,@Position
                        WHILE @@FETCH_STATUS = 0
                        BEGIN
                            IF @Txt!=''
                                SET @Txt=@Txt + ',' + '['+ @HName +']'
                            Else
                                SET @Txt= '['+ @HName +']'
                        FETCH NEXT FROM c3
                        INTO   @HId,@HName,@Position
                        END
                        DEALLOCATE c3

                        If @Event=1
                        Begin
                        SET @query = 'SELECT * FROM ( SELECT * FROM (
                        Select DISTINCT Main.Id,TId, Main.Name,MName as[Month],DaysInMonth,WorkingDays,
                        Round( (convert(decimal(16,2),(case when WorkingDays > WDays then WDays else WorkingDays end ))),1) as [Present Days],
                        Round( (convert(decimal(16,2),(case when WorkingDays < WDays then WDays-WorkingDays else 0 end )) ),1) as [OverTime Days],
	                    Round( (convert(decimal(16,2),AllowanceDays )),1) as [Allowance Days],
                        HopName,Value,convert(decimal(16,2),aa) AS TOTAL from
                        (
                        SELECT Employee.Id,TId,Name,MName,EName,DaysInMonth,WorkingDays,WDays,AllowanceDays,HopName,
                        convert(decimal(16,2),Case When EmployeeHop.ValType=''Fixed Value'' Then EmployeeHop.VALUE When EmployeeHop.ValType=''Daily Allowance'' Then EmployeeHop.Value*convert(Numeric,IsNull(AllowanceDays, Wdays)) Else (EmployeeHop.VALUE/DaysInMonth)* Case When Wdays<=WorkingDays Then (Wdays + (DaysInMonth-WorkingDays )) Else Wdays End End) AS VALUE
                        FROM Employee,EmployeeHop,WorkingDays
                        Where Employee.Id=EmployeeHop.EId and WorkingDays.Ename=Employee.Name and  [Mname]='''+ @Month +'''
                        and (''0''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' )
                        and (''0''=''' + @DepartmentId +''' or Employee.DId=''' + @DepartmentId +''' )
                        and (''0''=''' + @PostId +''' or Employee.PId=''' + @PostId +''' )
                        and (''0''=''' + @ShiftId +''' or Employee.ShId=''' + @ShiftId +''' )
                        Union All
                        SELECT Employee.Id,TId,Employee.Name,MName,EName,DaysInMonth,WorkingDays,WDays,AllowanceDays,''OT Amount'' HopName,
                        convert(decimal(16,2),Case When WDays-WorkingDays>0 Then (WDays-WorkingDays) * Overtime.OTrate Else 0 End) AS VALUE
                        FROM Employee,EmployeeHop,WorkingDays,Overtime
                        Where Employee.Id=EmployeeHop.EId and WorkingDays.Ename=Employee.Name and Overtime.Id=Employee.OId and  [Mname]='''+ @Month +'''
                        and (''0''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' )
                        and (''0''=''' + @DepartmentId +''' or Employee.DId=''' + @DepartmentId +''' )
                        and (''0''=''' + @PostId +''' or Employee.PId=''' + @PostId +''' )
                        and (''0''=''' + @ShiftId +''' or Employee.ShId=''' + @ShiftId +''' )

                        ) as Main
                        Left outer join
                        (
                        Select Name,
                        convert(decimal(16,2),Sum(Case when HopType='''+ @Type +''' then Case When EmployeeHop.ValType=''Fixed Value'' Then EmployeeHop.VALUE When EmployeeHop.ValType=''Daily Allowance'' Then EmployeeHop.Value*convert(Numeric,Wdays) Else (EmployeeHop.VALUE/DaysInMonth)* Case When Wdays<=WorkingDays Then (Wdays + (DaysInMonth-WorkingDays )) Else WDays End End else
                        - Case When EmployeeHop.ValType=''Fixed Value'' Then EmployeeHop.VALUE When EmployeeHop.ValType=''Daily Allowance'' Then EmployeeHop.Value*convert(Numeric,Wdays) Else (EmployeeHop.VALUE/DaysInMonth)* Case When Wdays<=WorkingDays Then (Wdays + (DaysInMonth-WorkingDays )) Else WDays End end End ))as aa
                        FROM Employee,EmployeeHop,WorkingDays
                        Where Employee.Id=EmployeeHop.EId and WorkingDays.Ename=Employee.Name
                        and  [Mname]='''+ @Month +''' and (''0''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' )
                        and (''0''=''' + @DepartmentId +''' or Employee.DId=''' + @DepartmentId +''' )
                        and (''0''=''' + @PostId +''' or Employee.PId=''' + @PostId +''' )
                        and (''0''=''' + @ShiftId +''' or Employee.ShId=''' + @ShiftId +''' )
                        Group by Name
                        ) as det on det.Name=Main.Name
                        ) as s
                            '
                        SET @query = @query  +
                        'PIVOT
                                    (
                                    Max(Value)
                                    FOR HopName IN ('+ @Txt +')
                        )AS pivott ) as hk Order By Name '
                        print @query
                        exec (@query)

                        END
                        If @Event=2
                        Begin
                        SET @query = 'SELECT * FROM (SELECT * FROM (
                            Select DISTINCT Main.Id,TId, Main.Name,MName as[Month],DaysInMonth,WorkingDays,
                            convert(decimal(16,2),(case when WorkingDays > WDays then WDays else WorkingDays end )) as [Present Days],
                            convert(decimal(16,2),( case when WorkingDays < WDays then WDays-WorkingDays else 0 end )) as [OverTime Days],
                            HopName,Value,convert(decimal(16,2),aa) AS TOTAL from
                            (
                            SELECT Employee.Id,TId,Name,MName,EName,DaysInMonth,WorkingDays,WDays,HopName,convert(decimal(16,2),(EmployeeHop.VALUE/WorkingDays)*(Wdays + (DaysInMonth-WorkingDays ))) AS VALUE
                            FROM Employee,EmployeeHop,WorkingDays Where Employee.Id=EmployeeHop.EId
                            and WorkingDays.Ename=Employee.Name and  [Mname]='''+ @Month +'''
                            and (''0''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' )
                            and (''0''=''' + @DepartmentId +''' or Employee.DId=''' + @DepartmentId +''' )
                            and (''0''=''' + @PostId +''' or Employee.PId=''' + @PostId +''' )
                            and (''0''=''' + @ShiftId +''' or Employee.ShId=''' + @ShiftId +''' )
                            ) as Main
                            Left outer join
                            (
                            Select Name,
                            sum(Case when HopType='''+ @Type +''' then (EmployeeHop.VALUE/WorkingDays)*(Wdays + (DaysInMonth-WorkingDays )) else -(EmployeeHop.VALUE/WorkingDays)*(Wdays + (DaysInMonth-WorkingDays )) end)as aa
                            FROM Employee,EmployeeHop,WorkingDays
                            Where Employee.Id=EmployeeHop.EId and WorkingDays.Ename=Employee.Name
                            and  [Mname]='''+ @Month +''' and (''0''=''' + @EmpId +''' or Employee.Id=''' + @EmpId +''' )
                            and (''0''=''' + @DepartmentId +''' or Employee.DId=''' + @DepartmentId +''' )
                            and (''0''=''' + @PostId +''' or Employee.PId=''' + @PostId +''' )
                            and (''0''=''' + @ShiftId +''' or Employee.ShId=''' + @ShiftId +''' )
                            Group by Name
                            ) as det on det.Name=Main.Name
                            ) as s

                            '
                        SET @query = @query  +
                        'PIVOT
                            (
                            Max(Value)
                            FOR HopName IN ('+ @Txt +')
                        )AS pivott ) as hk Order By Name'

                        exec (@query)
                        END
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                Alter proc [dbo].[sp_DailyAttendanceSheet1] @MonthName varchar(20)
                as
                Select distinct sheetno,  M_Miti [Miti],DailyAttendance.DDate [Date],DATENAME(dw,DailyAttendance.DDate)as [Days] ,Employee.Name [Name],WorkingHour [Working Hour],Intime [In Time],LOut  [Lunch Out] ,LIn [Lunch In],Tin [Tea In],TOut [Tea Out],EOut [Out],EIn [In]
                ,OOut [Other Out],OIn [Other In],Intime1,LOut1,LIn1,TOut1,Tin1,OutTime1,Total1,EOut1,EIn1,OOut1,OIn1,Total,OutTime,Total [Working Hour],
                CONVERT(varchar(6), DATEDIFF(second, WorkingHour, Total)/3600) + ':' + RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second, WorkingHour, Total) % 3600) / 60), 2) +
                ':' + RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, WorkingHour, Total) % 60), 2) [Over Time]
                from DailyAttendance,Employee,Shift,DateMiti where Employee.Id=DailyAttendance.Eid and Employee.ShId=Shift.SId and DateMiti.M_Date=DailyAttendance.DDate and DMonth=@MonthName
                Order by M_Miti,Employee.Name";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"ALTER proc [dbo].[sp_DailyAttendanceSheet] @Event int,@EmpId varchar(10),@month varchar(50)
                        as
                        if @Event=1
                        Begin
	                        if ((Select top 1 DateType from systemsetting)='M')
	                        Begin
		                        Select Distinct E.Id, Month_Name [Month Name],E.Name as [Employee Name],DDate [Date],M_miti Miti, InTime [InTime],OutTime [Out Time],Convert(nvarchar(5),Total) Total --,D.Name as Department,P.PName as Post,
		                        from DateMiti as DM Left Join DailyAttendance as DA On M_Date=DDate Inner Join Employee as E On DA.EId=E.Id
		                        --Left Outer Join Shift as S on S.SId=E.ShId Left Outer Join Department as D On D.DId=E.DId Left Outer Join Post as P On P.PId=E.PId
		                        Where MMonth=@month
		                        Union All
		                        Select E.Id,MMonth [Month Name],E.Name as [Employee Name],'4000/01/01' Date, Null M_miti,Null InTime ,Null OutTime,
		                        RTRIM(SUM(DATEDIFF(MINUTE, '0:00:00', Total))/60) + ':' + RIGHT('0' +RTRIM(SUM(DATEDIFF(MINUTE, '0:00:00', Total))%60),2) as Total from DailyAttendance as DA
		                        Inner Join Employee as E On DA.EId=E.Id
		                        Where MMonth=@month and (''= @EmpId  or E.Id= @EmpId ) and Year(DDate)=Year(GetDate())
		                        Group By E.Id,E.Name,MMonth
		                        Order By Name,DDate
                            End
                            Else
                            Begin
		                        Select Distinct E.Id, DMonth [Month Name],E.Name as [Employee Name],DDate [Date],M_miti Miti, InTime [InTime],OutTime [Out Time],Convert(nvarchar(5),Total) Total --,D.Name as Department,P.PName as Post,
		                        from DateMiti as DM Left Join DailyAttendance as DA On M_Date=DDate Inner Join Employee as E On DA.EId=E.Id
		                        --Left Outer Join Shift as S on S.SId=E.ShId Left Outer Join Department as D On D.DId=E.DId Left Outer Join Post as P On P.PId=E.PId
		                        Where DMonth=@month
		                        Union All
		                        Select E.Id,DMonth [Month Name],E.Name as [Employee Name],'4000/01/01' Date, Null M_miti,Null InTime ,Null OutTime,
		                        RTRIM(SUM(DATEDIFF(MINUTE, '0:00:00', Total))/60) + ':' + RIGHT('0' +RTRIM(SUM(DATEDIFF(MINUTE, '0:00:00', Total))%60),2) as Total from DailyAttendance as DA
		                        Inner Join Employee as E On DA.EId=E.Id
		                        Where DMonth=@month and (''= @EmpId  or E.Id= @EmpId ) and Year(DDate)=Year(GetDate())
		                        Group By E.Id,E.Name,DMonth
		                        Order By Name,DDate
                            End

                        End
                        if @Event=2
                        Begin
                            Select distinct E.Id,Case When (Select top 1 DateType from systemsetting)='M' Then MMonth Else DMonth End as [Month Name],E.Name [Employee Name],DA.DDate Date,M_Miti Miti,WorkingHour,Intime,LOut [Lunch Out] ,LIn [Lunch In],Tin [Tea In],TOut [Tea Out],EOut [Out],EIn [In]
                            ,OOut [Other Out],OIn [Other In],Intime1,LOut1,LIn1,TOut1,Tin1,OutTime1,Total1,EOut1,EIn1,OOut1,OIn1,Total,OutTime,Total [Working Hour],
                            CONVERT(varchar(6), DATEDIFF(second, WorkingHour, Total)/3600) + ':' +
                            RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second, WorkingHour, Total) % 3600) / 60), 2) +
                            ':' + RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, WorkingHour, Total) % 60), 2) [Over Time]
                            from DailyAttendance DA,Employee as E,Shift as S,DateMiti as DM
                            Where E.Id=DA.Eid and E.ShId=S.SId and DM.M_Date=DA.DDate
                            and (DMonth=@month or MMonth=@month) and (''= @EmpId  or E.Id= @EmpId ) and Year(DDate)=Year(GetDate())
                            Order by E.Name --,DMonth ,MMonth
                        End
                        if @Event=3
                        Begin
                            Select distinct sheetno,  M_Miti [Miti],DailyAttendance.DDate [Date],DATENAME(dw,DailyAttendance.DDate)as [Days] ,Employee.Name [Name],WorkingHour [Working Hour],Intime [In Time],LOut  [Lunch Out] ,LIn [Lunch In],Tin [Tea In],TOut [Tea Out],EOut [Out],EIn [In]
                            ,OOut [Other Out],OIn [Other In],Intime1,LOut1,LIn1,TOut1,Tin1,OutTime1,Total1,EOut1,EIn1,OOut1,OIn1,Total,OutTime,Total [Working Hour],
                            CONVERT(varchar(6), DATEDIFF(second, WorkingHour, Total)/3600) + ':' +
                            RIGHT('0' + CONVERT(varchar(2), (DATEDIFF(second, WorkingHour, Total) % 3600) / 60), 2) +
                            ':' + RIGHT('0' + CONVERT(varchar(2), DATEDIFF(second, WorkingHour, Total) % 60), 2) [Over Time]
                            from DailyAttendance,Employee,Shift,DateMiti
                            where Employee.Id=DailyAttendance.Eid
                            and Employee.ShId=Shift.SId and DateMiti.M_Date=DailyAttendance.DDate
                            and (DMonth=@month or MMonth=@month) and (''= @EmpId  or Employee.Id= @EmpId ) and Year(DDate)=Year(GetDate())
                            Order by M_Miti,Employee.Name
                        End";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @" ALTER Proc [dbo].[Sp_LoadAtt] @Event Varchar(5),@EmpId Varchar(50),@MonthName Varchar(50),@DepartmentId int =0,@BranchId int =0
                        as
                        --[Sp_LoadAtt] 'GS','0','May'
                        declare @w int
                        set @w=(select top(1) convert(numeric,(DATEPART(hour, WorkingHour) * 60) + (DATEPART(minute, WorkingHour)) )  from Shift)

                        if @Event='MA' ---Modified Att and Generate Salary
                        Begin
                        select * from
                        (
                            select [Month Name],Main.EId,[Employee Name],[Days In Month],[Working Days In Month],[Daily Working Hour],convert(decimal(16,2),workingday) as [Working Days],
                            case when convert(decimal(16,2),workingday)-[Working Days In Month] > 0 then convert(decimal(16,2),workingday)- [Working Days In Month] else 0 end as [OT Days],
                            convert(decimal(16,2),case when [Month Working Minute] >  [Work In Minute] then (Value/[Working Days In Month])*workingday
                            else (Value/[Working Days In Month])*workingday*([Month Working Minute]-[Work In Minute]) end) as [Payable salary],
                            case when [Month Working Minute] < [Work In Minute] then MinuteRate*([Work In Minute]-[Month Working Minute]) else 0 end as [OT Amount],Value,
                            convert(decimal(16,2),case when [Month Working Minute] >  [Work In Minute] then (Value/[Working Days In Month])*workingday
                            else (Value/[Working Days In Month])*workingday*([Month Working Minute]-[Work In Minute]) end)+case when [Month Working Minute] < [Work In Minute] then MinuteRate*([Work In Minute]-[Month Working Minute]) else 0 end
                            NetAmt
                            from
                            (
                                select  distinct DA.Eid,Dmonth as [Month Name],E.Name as [Employee Name],Mdays as [Days In Month] ,
                                Mworking as [Working Days In Month],SUM(Value) as Salary,WorkingHour as [Daily Working Hour],
                                convert(numeric,(DATEPART(hour, WorkingHour) * 60) + (DATEPART(minute, WorkingHour)) )* Mworking as [Month Working Minute]
                                from DailyAttendance DA  Inner Join Employee as E On DA.Eid=E.Id Left Outer Join Shift as S On E.ShId=S.SId
                                Left Outer Join MonthlyDaysSetup as MDS On DA.DMonth=MDS .MMonth Left Outer Join EmployeeHop as EH On EH.Eid=E.Id and DA.Eid=EH.Eid
                                Where YEAR(DA.DDate)=YEAR(GETDATE()) and DMonth=@MonthName  and (@DepartmentId=0 or E.DId=@DepartmentId ) and (@BranchId=0 or E.BranchId=@BranchId )
                                Group by DA.Eid,DMonth,E.Name,MDays,Mworking,WorkingHour
                                ) as Main
                                Left outer join
                                (
                                select dmonth,eid,sum(convert(numeric,(DATEPART(hour, total) * 60) + (DATEPART(minute, total)) ))/@w as workingday,
                                sum(convert(numeric,(DATEPART(hour, total) * 60) + (DATEPART(minute, total)) ))  as [Work In Minute]
                                from DailyAttendance group by Eid,dmonth
                                ) as Salary on salary.eid=main.Eid and Salary.DMonth=main.[Month Name]
                                left outer join
                                (
                                select OtRate,OtRate/60 as MinuteRate,Employee.Id from Overtime ,Employee where Overtime.Id=Employee.OId
                                ) as Ot on Ot.Id=main.Eid
                                left outer join
                                (
                                select Eid,SUM(Value) as Value from EmployeeHop Group by Eid
                                ) as Hop on Hop.Eid=Main.Eid
                            Union all
                            Select [Month Name],EId,[Employee Name],[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],
                            [OT Days],sum([Payable Salary])[Payable Salary],sum([OT Amount])[OT Amount],sum(Value) Value ,sum([Payable Salary])+ sum([OT Amount]) NetAmt
                            From (
                            Select [MonthName] as [Month Name],Employee.Id EId,Employee.Name [Employee Name],Ndays [Days In Month],Wdays [Working Days In Month],
                            WorkingHour as [Daily Working Hour],pdays  as [Working Days],
                            case when Wdays-pdays < 0 then pdays-wdays else 0 end  as  [OT Days],
                            case when wdays-pdays <= 0 then (Case When HopType='Addition' Then sum(Value) Else -sum(Value) End/Ndays)*Ndays else (Case When HopType='Addition' Then sum(Value) Else -sum(Value) End/Ndays)*pdays end [Payable Salary],
                            case when Wdays-PDays <= 0 then (Case When HopType='Addition' Then sum(Value) Else -sum(Value) End/NDays)*(PDays-Wdays) else 0 end as [OT Amount],
                            Case When HopType='Addition' Then sum(Value) Else -sum(Value) End as Value
                            from MonthlyAtt,Employee,EmployeeHop,Shift
                            Where MonthlyAtt.EId=Employee.Id and Employee.Shid=Shift.SId and Employee.Id=EmployeeHop.EId
                            and MonthName=@MonthName and (@DepartmentId=0 or Employee.DId=@DepartmentId ) and (@BranchId=0 or Employee.BranchId=@BranchId )
                            Group by MonthName,Employee.Id,Employee.Name,NDays,Wdays,WorkingHour,PDays,HopType
                            ) as aa Group By [Month Name],EId,[Employee Name],[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],[OT Days]

                        ) as aa where [Month Name]=@MonthName Order By [Employee Name]
                        End
                        if @Event='GS' ----Generate Salary
                        Begin

                        Select Distinct [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Grade,[Days In Month],[Working Days In Month],[Daily Working Hour],Round (Sum([Working Days]),0) [Working Days] ,Round (Sum([Allowance Days]),0) [Allowance Days],Round (Sum([OT Days]) ,0) [OT Days],Sum(Value) Value,Convert(decimal(16,2),Sum([Payable Salary])) [Payable Salary],
                        Convert(decimal(16,2),Sum([OT Amount])) [OT Amount],Convert(decimal(16,2),Sum(NetAmt)) NetAmt,Convert(decimal(16,2),Sum([Deduction Salary])+Premium/12) [Deduction Salary],Convert(decimal(16,2),Sum([Taxable Amt])-Premium/12) [Taxable Amt],
                        Convert(decimal(16,2),Sum([Taxable 12 Month])-Premium) [Taxable 12 Month] from
                            (
                            Select Distinct [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Grade,Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],Round ([Working Days],0) [Working Days] ,Round ([Allowance Days],0) [Allowance Days],Round ([OT Days] ,0) [OT Days],Sum(Value) Value,Convert(decimal(16,2),Sum([Payable Salary])) [Payable Salary],
                            Convert(decimal(16,2),[OT Amount]) [OT Amount],Convert(decimal(16,2),sum([Payable Salary])+ sum([OT Amount])) NetAmt,Convert(decimal(16,2),Sum([Deduction Salary])) [Deduction Salary],Convert(decimal(16,2),Sum([Taxable Amt])+sum([OT Amount])) [Taxable Amt],
                            Convert(decimal(16,2),Sum([Taxable 12 Month])+sum([OT Amount])*12) [Taxable 12 Month] from
                                (
                                    Select [Month Name],Main.EId,TId,[Employee Name],EMail,DId,Department,Grade, Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],convert(Numeric,workingday) as [Working Days],convert(Numeric,AllowanceDays) as [Allowance Days],
                                    Case when convert(decimal(16,2),workingday)-[Working Days In Month] > 0 then convert(decimal(16,2),workingday)- [Working Days In Month] else 0 end as [OT Days],
                                    Case When Type='Addition' Then sum(Value) Else -sum(Value) End as Value,

                                    Convert(decimal(16,2),Case when [Month Working Minute] >  [Work In Minute] then (Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,AllowanceDays) Else (Sum(Value)/[Days In Month])*(convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End else 0 End)
                                    Else ((Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,AllowanceDays) Else Sum(Value)/[Days In Month]*[Days In Month] End else 0 End)) end) as [Payable Salary],

                                    --Case when [Month Working Minute] < [Work In Minute] then Isnull(MinuteRate,0)*([Work In Minute]-[Month Working Minute]) else 0 end as [OT Amount],
                                    Case when convert(decimal(16,2),workingday)-[Working Days In Month] > 0 then Isnull(otRate,0) * (convert(decimal(16,2),workingday)- [Working Days In Month]) Else 0 end as [OT Amount],

                                    --Convert(decimal(16,2),Case when [Month Working Minute] >  [Work In Minute] then (Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value)/[Days In Month])* (convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End else 0 End)
                                    --Else (Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value) /[Days In Month])*[Days In Month] End else 0 End) End)+
                                    --Case When [Month Working Minute] < [Work In Minute] then Isnull(MinuteRate,0)*([Work In Minute]-[Month Working Minute]) Else 0 End NetAmt,

                                    Convert(decimal(16,2),Case when [Month Working Minute] >  [Work In Minute] then (Case When Type='Deduction' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value)/[Days In Month])*(convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End else 0 End)
                                    Else (Case When Type='Deduction' Then Case When ValType='Fixed Value' Then Sum(Value) Else (Sum(Value) /[Days In Month])*[Days In Month] End else 0 End) End)+ Case When [Month Working Minute] < [Work In Minute] then Isnull(MinuteRate,0)*([Work In Minute]-[Month Working Minute]) Else 0 End [Deduction Salary],

                                    IsNull(Convert(decimal(16,2),Case when [Month Working Minute] >  [Work In Minute] then
                                    (Case When Tax='Y' Then Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value)/[Days In Month])*(convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End
                                        else -Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value)/[Days In Month])*(convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End End End )
                                    Else (Case When Tax='Y' Then Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else Sum(Value)/[Days In Month]*[Days In Month] End
                                        else - Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else Sum(Value)/[Days In Month]*[Days In Month] End End End )
                                    +(Isnull(MinuteRate,0)*([Work In Minute] -[Month Working Minute]))end),0) [Taxable Amt],

                                    IsNull(Convert(decimal(16,2),Case when [Month Working Minute] >  [Work In Minute] then
                                    (Case When Tax='Y' Then Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value)/[Days In Month])*(convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End
                                        else -Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else (Sum(Value)/[Days In Month])*(convert(Numeric,Workingday) + ( [Days In Month] - [Working Days In Month])) End End End )
                                    Else (Case When Tax='Y' Then Case When Type='Addition' Then Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else Sum(Value)/[Days In Month]*[Days In Month] End
                                        else - Case When ValType='Fixed Value' Then Sum(Value) When ValType='Daily Allowance' Then Sum(Value)*convert(Numeric,Workingday) Else Sum(Value)/[Days In Month]*[Days In Month] End End End )
                                    +(Isnull(MinuteRate,0)*([Work In Minute] -[Month Working Minute]))end),0) *12 [Taxable 12 Month]
                                    from (
                                    Select  distinct DA.Eid,Dmonth as [Month Name],E.TId,E.Name as [Employee Name],EMail,E.DId,D.Name Department,GM.Name Grade,Isnull(Premium,0)Premium, Mdays as [Days In Month] ,
                                    Mworking as [Working Days In Month],SUM(EH.Value) as Salary,WorkingHour as [Daily Working Hour],
                                    convert(numeric,(DATEPART(hour, WorkingHour) * 60) + (DATEPART(minute, WorkingHour)) )* Mworking as [Month Working Minute]
                                    from DailyAttendance as DA Inner Join Employee as E On DA.Eid=E.Id Inner Join EmployeeHop as EH On EH.Eid=E.Id Left Outer Join HOP On HOP.Code=EH.HopCode
                                    Left Outer Join Department as D On D.DId=E.DId Left Outer Join GradeMaster as GM On E.GId=GM.GId
			                        Left Outer Join Shift as S On E.ShId=S.SId Left Outer Join MonthlyDaysSetup as MDS On DA.DMonth=MDS.MMonth and YEAR(DA.DDate)= YEAR(MDS.MDate)
                                    Where YEAR(DA.DDate)=YEAR(GETDATE()) and Dmonth=@MonthName and (@EmpId='0' or DA.EId=@EmpId) and (@DepartmentId=0 or E.DId=@DepartmentId ) and (@BranchId=0 or E.BranchId=@BranchId )
                                    Group by DA.Eid,DMonth,E.TId,E.Name,EMail,E.DId,D.Name,GM.Name, Premium,MDays,Mworking,WorkingHour
                                    ) as Main
                                    Left outer join (
                                    Select dmonth,EId,Sum(Workingday) WorkingDay,Sum(AllowanceDays) AllowanceDays,Sum([Work In Minute]) [Work In Minute] from(
			                        Select dmonth,Eid,sum(convert(numeric,(DATEPART(hour, total) * 60) + (DATEPART(minute, total)) ))/convert(numeric,(DATEPART(hour, WorkingHour) * 60) + (DATEPART(minute, WorkingHour))) as Workingday,COUNT(Distinct DDate) AllowanceDays,
			                        sum(convert(numeric,(DATEPART(hour, total) * 60) + (DATEPART(minute, total)) ))  as [Work In Minute]
			                        from DailyAttendance as DA Inner Join Employee as E On DA.Eid=E.Id Left Outer Join Shift as S On E.ShId=S.SId Where YEAR(DA.DDate)=YEAR(GETDATE())
			                        and Dmonth=@MonthName and (@EmpId='0' or DA.EId=@EmpId) and (@DepartmentId=0 or E.DId=@DepartmentId ) and (@BranchId=0 or E.BranchId=@BranchId )
			                        Group by Eid,dmonth,WorkingHour
			                        ---------Paid Leave ----------
			                        Union All
			                        Select MMonth,LE.Eid,PaidLeave_Days  as [Working Days],0 [Allowance Days],0 [Work In Minute]
			                        from LeaveEntry as LE Inner Join Employee as E On LE.EId=E.Id Inner Join EmployeeHop as EH On E.Id=EH.EId
			                        Left Outer Join HOP On HOP.Code=EH.HopCode Left Outer Join Shift as S On E.Shid=S.SId
			                        Left Outer Join MonthlyDaysSetup as MDS On MDS.MMonth=DATENAME(month, LE.Approved_FromDate) and YEAR(LE.Approved_FromDate)= YEAR(MDS.MDate)
			                        Where PaidLeave_Days<>0 and DATENAME(month, LE.Approved_FromDate)=@MonthName and (@EmpId='0' or LE.EId=@EmpId)
			                        and (@DepartmentId=0 or E.DId=@DepartmentId ) and (@BranchId=0 or E.BranchId=@BranchId )
			                        Group by MMonth,DATENAME(month, LE.Approved_FromDate),LE.Eid,TId,MDays,Mworking,WorkingHour,PaidLeave_Days
			                        ) as aa Group by dmonth,EId
			                        ----------
                                    ) as Salary on salary.eid=main.Eid and Salary.DMonth=main.[Month Name]
                                    left outer join (
                                    Select OtRate,OtRate/60 as MinuteRate,E.Id from Overtime as OT ,Employee as E where OT.Id=E.OId
                                    ) as Ot on Ot.Id=main.Eid
                                    left outer join (
                                    Select Eid,Type,ValType,Tax,EH.Value from EmployeeHop as EH Inner Join HOP On HOP.Code=EH.HopCode
                                    ) as Hop on Hop.Eid=Main.Eid
                                    Group By Type,TId,Tax,OtRate,MinuteRate,[Work In Minute],[Month Working Minute],[Month Name],Main.EId,[Employee Name],EMail,DId,Department,Grade,Premium,
                                    [Days In Month],[Working Days In Month],[Daily Working Hour],[workingday],AllowanceDays,ValType--,[OT Days]
                                -------Monthly Attendance-----
                                Union all
                                Select [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Grade,Isnull(Premium,0)Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],[Allowance Days],
                                [OT Days],sum(Value) Value ,sum([Payable Salary])[Payable Salary],[OT Amount],Sum([Deduction Salary]) [Deduction Salary],Sum([Taxable Amt]) [Taxable Amt],(Sum([Taxable Amt])) * 12 as [Taxable 12 Month]
                                From (
                                Select [MonthName] as [Month Name],E.Id as EId,TId,E.Name [Employee Name],EMail,E.DId,D.Name Department,GM.Name Grade, Isnull(Premium,0)Premium,Ndays [Days In Month],Wdays [Working Days In Month],
                                WorkingHour as [Daily Working Hour],pdays  as [Working Days],pdays [Allowance Days],
                                Case when Wdays-pdays < 0 then pdays-wdays else 0 end  as  [OT Days],
                                Case When Type='Addition' Then sum(EH.Value) Else -sum(EH.Value) End as Value,
                                Case when Wdays-Pdays <= 0 then
                                    (Case When Type='Addition' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PDays) Else sum(EH.Value)/Ndays*Ndays End Else 0 End)
                                else
                                    (Case When Type='Addition' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PDays) Else sum(EH.Value)/Ndays*(NDays-WDays+ Pdays) End Else 0 End)
                                End [Payable Salary],
                                --Case when Wdays-PDays <= 0 then (Case When Type='Addition' Then sum(EH.Value) Else 0 End/NDays)*(PDays-Wdays) else 0 end as [OT Amount],
                                Case when Wdays-PDays <= 0 then Isnull(otRate,0) * (PDays-Wdays) else 0 end as [OT Amount],
                                Case when Wdays-Pdays <= 0 then
                                (Case When Type='Deduction' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) Else sum(EH.Value)/Ndays*Ndays End Else 0 End) else
                                (Case When Type='Deduction' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) Else sum(EH.Value)/Ndays*(NDays-WDays+ Pdays) End Else 0 End)
                                end [Deduction Salary],
                                Case when Wdays-Pdays <= 0 then (
                                    Case When Tax='Y' Then Case When Type='Addition' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PDays) Else sum(EH.Value)/Ndays*Ndays end Else - Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PDays) Else sum(EH.Value)/Ndays*Ndays End End End )
                                else (
                                    Case When Tax='Y' Then Case When Type='Addition' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PDays) Else sum(EH.Value)/Ndays*(NDays-WDays+ Pdays) End Else - Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PDays) Else  sum(EH.Value)/Ndays*(NDays-WDays+ Pdays)  End End End )
                                end [Taxable Amt]
                                from MonthlyAtt as MA Inner Join Employee as E On MA.EId=E.Id Inner Join EmployeeHop as EH On E.Id=EH.EId
                                Left Outer Join HOP On HOP.Code=EH.HopCode Left Outer Join GradeMaster as GM On E.GId=GM.GId
		                        Left Outer Join Shift as S On E.Shid=S.SId Left Outer Join Department as D On D.DId=E.DId
                                Left Outer Join Overtime as OT On OT.Id=E.OId
                                Where MonthName=@MonthName and (@EmpId='0' or MA.EId=@EmpId) and (@DepartmentId=0 or E.DId=@DepartmentId ) and (@BranchId=0 or E.BranchId=@BranchId )
                                Group by MonthName,E.Id,TId,E.Name,EMail,E.DId,D.Name,GM.Name,Premium,NDays,Wdays,WorkingHour,PDays,OtRate,Type,Tax ,EH.ValType
                                ) as aa Group By [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Grade,Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],[Allowance Days],[OT Days],[OT Amount]
                        --                        -------Paid Leave ----------Calcluate worng then comment----
                        --                        Union all
                        --                        Select [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Isnull(Premium,0)Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],[Allowance Days],
                        --                        [OT Days],sum(Value) Value ,sum([Payable Salary])[Payable Salary],sum([OT Amount])[OT Amount],Sum([Deduction Salary]) [Deduction Salary],Sum([Taxable Amt]) [Taxable Amt],(Sum([Taxable Amt])) * 12 as [Taxable 12 Month]
                        --                        From (
                        --                        Select DATENAME(month, LE.Approved_FromDate) as [Month Name],E.Id as EId,TId,E.Name [Employee Name],EMail,E.DId,D.Name Department,Isnull(Premium,0)Premium,MDays [Days In Month],Mworking [Working Days In Month],
                        --                        WorkingHour as [Daily Working Hour],PaidLeave_Days  as [Working Days],0 [Allowance Days],
                        --                        Case when Mworking-PaidLeave_Days < 0 then PaidLeave_Days-Mworking else 0 end  as  [OT Days],
                        --                        Case When HopType='Addition' Then sum(EH.Value) Else -sum(EH.Value) End as Value,
                        --                        Case when Mworking-PaidLeave_Days <= 0 then (Case When HopType='Addition' Then sum(EH.Value) Else 0 End/MDays)*MDays else (Case When HopType='Addition' Then sum(EH.Value) Else 0 End/MDays)*PaidLeave_Days end [Payable Salary],
                        --                        Case when Mworking-PaidLeave_Days <= 0 then (Case When HopType='Addition' Then sum(EH.Value) Else 0 End/MDays)*(PaidLeave_Days-Mworking) else 0 end as [OT Amount],
                        --                        Case when Mworking-PaidLeave_Days <= 0 then
                        --                            (Case When HopType='Deduction' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PaidLeave_Days) Else sum(EH.Value)/MDays*MDays End Else 0 End)
                        --                        else
                        --                            (Case When HopType='Deduction' Then Case When EH.ValType='Fixed Value' Then sum(EH.Value) When EH.ValType='Daily Allowance' Then Sum(EH.Value)*convert(Numeric,PaidLeave_Days) Else sum(EH.Value)/MDays*PaidLeave_Days End Else 0 End)
                        --                        end [Deduction Salary],
                        --                        Case when Mworking-PaidLeave_Days <= 0 then (
		                        --	Case When Tax='Y' Then (Case When HopType='Addition' Then (Case When EH.ValType='Value' Then (sum(EH.Value)/MDays)*MDays else 0 end) Else (Case When EH.ValType='Value' Then -(sum(EH.Value)/MDays)*MDays else 0 End) End) End )
		                        --else (
		                        --	Case When Tax='Y' Then (Case When HopType='Addition' Then (Case When EH.ValType='Value' Then (sum(EH.Value)/MDays)*PaidLeave_Days else 0 End) Else (Case When EH.ValType='Value' Then -(sum(EH.Value)/MDays)*PaidLeave_Days else 0 End) End) End )
		                        --end [Taxable Amt]
                        --                        from LeaveEntry as LE Inner Join Employee as E On LE.EId=E.Id Inner Join EmployeeHop as EH On E.Id=EH.EId
                        --                        Left Outer Join HOP On HOP.Code=EH.HopCode Left Outer Join Shift as S On E.Shid=S.SId Left Outer Join Department as D On D.DId=E.DId
                        --                        Left Outer Join MonthlyDaysSetup as MDS On MDS.MMonth=DATENAME(month, LE.Approved_FromDate) and YEAR(LE.Approved_FromDate)= YEAR(MDS.MDate)
                        --                        Where PaidLeave_Days<>0 and DATENAME(month, LE.Approved_FromDate)=@MonthName and (@EmpId='0' or LE.EId=@EmpId) and (@DepartmentId=0 or E.DId=@DepartmentId ) and (@BranchId=0 or E.BranchId=@BranchId )
                        --                        Group by DATENAME(month, LE.Approved_FromDate),E.Id,TId,E.Name,EMail,E.DId,D.Name,Premium,MDays,Mworking,WorkingHour,PaidLeave_Days,HopType,Tax,EH.ValType
                        --                        ) as aa Group By [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],[Allowance Days],[OT Days],[OT Amount]

                            ) as aa
                            Where [Month Name]=@MonthName
                            Group By [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Grade,Premium,[Days In Month],[Working Days In Month],[Daily Working Hour],[Working Days],[Allowance Days],[OT Days],[OT Amount]
                        ) as aa11
                        Group By [Month Name],EId,TId,[Employee Name],EMail,DId,Department,Grade,Premium,[Days In Month],[Working Days In Month],[Daily Working Hour]
                        Order By [Employee Name]
                        End

                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter  Proc [dbo].[Usp_Select_Data]
                    (
                    @Event int,
                    @Code1 varchar(255),
                    @Code2 varchar(255)='A',
                    @Code3 varchar(255)='A',
                    @Code4 varchar(255)='A',
                    @Code5 varchar(255)='A',
                    @Code6 varchar(255)='B',
                    @Code7 varchar(50)='A',
                    @Code8 varchar(50)='A',
                    @Code9 varchar(50)='A',
                    @Code10 varchar(50)='A',
                    @Date datetime='1753/01/01',
                    @Date1 datetime='1753/01/01',
                    @Branch_Id int=1
                    )
                    as
                    BEGIN
                    declare @SNo int
                    DECLARE @DateType char(1)
                    set @DateType=(select top 1 DateType from systemsetting)
                    if @Event=1
                    BEGIN
	                    Select * from MachineInfo
                    End
                    End
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion Payroll

        try
        {
            Sql = @"Alter proc [dbo].[Sp_InsertCashReceipt]
                (
                    @Flag int,
                    @Id int,
                    @Vno varchar(50),
                    @Vdate Datetime,
                    @BillNo varchar(50),
                    @Amount Money,
                    @PayType Varchar(50),
                    @BankName varchar(200),
                    @Holder Varchar(50),
                    @CardNo Varchar(50),
                    @ExpDate DateTime,
                    @Remarks Varchar(1000)
                )
                as
                if @Flag=1
                Begin
	                Insert into CashReceipt(VNo,Vdate,BillNo,Amount,PayType,BankName,Holder,cardNo,ExpDate,Remarks)
		                Values (@VNo,@Vdate,@BillNo,@Amount,@PayType,@BankName,@Holder,@cardNo,@ExpDate,@Remarks)
                end
                if @Flag=2
                Begin
	                Update CashReceipt set VNo=@VNo,Vdate=@Vdate,BillNo=@BillNo,Amount=@Amount,PayType=@PayType,BankName=@BankName,Holder=@Holder,cardNo=cardNo,ExpDate=@ExpDate,Remarks=@Remarks
					 Where Id=@Id
                end
                if @Flag=3
                Begin
	                delete from CashReceipt where Id=@Id
                end ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                    Alter Proc [dbo].[Sp_InsertCashPayment]
                    (
                        @PaymentNo Varchar(50),
                        @PaymentDate nvarchar(20),
                        @PaymentTime nvarchar(20),
                        @OpdBillNo varchar(50),
                        @PatientCode Varchar(50),
                        @PaymentAmount Money,
                        @PaymentMode Varchar(50),
                        @DepartmentCode Varchar(50),
                        @BankName Varchar(1000),
                        @CardHolder Varchar(1000),
                        @CardNo Varchar(100),
                        @ExpDate nvarchar(20),
                        @Status char(10),
                        @CreatedDate nvarchar(10),
                        @CreatedBy Varchar(50),
                        @ModifyBy Varchar(50),
                        @ModifyDate nvarchar(20),
                        @AuditLog Varchar(Max)

                    )as

                    Insert Into CashPayment
                    (
                    PaymentNo ,PaymentDate ,PaymentTime ,OpdBillNo ,PatientCode ,PaymentAmount ,PaymentMode ,DepartmentCode ,BankName ,CardHolder ,CardNo ,ExpDate ,Status ,
                    CreatedDate ,CreatedBy ,ModifyBy ,ModifyDate ,AuditLog )values(
                    @PaymentNo ,convert(datetime, convert(nvarchar(11),@PaymentDate,101),103),getdate(),@OpdBillNo
                    , @PatientCode ,@PaymentAmount ,@PaymentMode ,@DepartmentCode ,@BankName ,@CardHolder ,@CardNo ,convert(datetime, convert(nvarchar(11),@ExpDate,101),103),
                    @Status ,getdate(), @CreatedBy ,@ModifyBy ,getdate(),@AuditLog )
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                    Alter Proc [dbo].[Sp_InsertBillMaster]
                    @RegNo varchar(50),
                    @BillNo varchar(50),
                    @Billdate DateTime,
                    @BillTime DateTime,
                    @BasicAmount Money,
                    @Tax Money,
                    @Discount Money,
                    @Net Money,
                    @Remarks Varchar(500),
                    @CreatedBy varchar(50),
                    @Createddate Datetime
                    as
                    Begin
                        Insert Into BillingMaster(RegNo,BillNo,BillDate,BillTime,BasicAmount,Tax,Discount,Net,Remarks,CreatedBy,CreatedDate)
	                    Values(@RegNo,@BillNo,@BillDate,@BillTime,@BasicAmount,@Tax,@Discount,@Net,@Remarks,@CreatedBy,@CreatedDate)
                    end
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @" Alter Proc [dbo].[sp_InsertBillDetails]
                @BillNo Varchar(50),
                @Code Varchar(50),
                @qty Money,
                @Rate money,
                @Tax Money,
                @Discount Money,
                @Total Money
                as
                Begin
	                Insert into BillDetails(BillNo,Code,qty,Rate,Tax,Discount,Total)
	                Values (@BillNo,@Code,@qty,@Rate,@Tax,@Discount,@Total)
                end";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"
                Alter Proc [dbo].[sp_BillingList]
                @Cid int,
                @stdate datetime,
                @endate datetime
                as
                if @Cid=0
	                begin
		                Select BillingMaster.BillNo,Billdate,Name ,IName,qty,Rate,BillDetails.Tax,BillDetails.Discount,Total from BillingMaster,BillDetails,Item,Customer
		                where Item.ICode=BillDetails.Code and BillDetails.BillNo=BillingMaster.BillNo and Customer.RegNo=BillingMaster.RegNo
		                and RegDate between @stdate and @endate
		                Order by Billdate
	                end
                else
	            begin
		            Select BillingMaster.BillNo,Billdate,Name ,IName,qty,Rate,BillDetails.Tax,BillDetails.Discount,Total from BillingMaster,BillDetails,Item,Customer
		            where Item.ICode=BillDetails.Code and BillDetails.BillNo=BillingMaster.BillNo and Customer.RegNo=BillingMaster.RegNo
		            and RegDate between @stdate and @endate
		            and CId=@Cid
		            Order by Billdate

	            end
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[sp_DueBillingList]
                @Cid int,
                @stdate datetime,
                @endate datetime
                as
                if @Cid=0
	                begin
		                Select BillingMaster.BillNo [Bill No],Billdate [Bill date],Name [Customer Name] ,IName [Item Name],qty [Quantity],Rate,BillDetails.Tax,BillDetails.Discount,Total from BillingMaster,BillDetails,Item,Customer
		                where Item.ICode=BillDetails.Code and BillDetails.BillNo=BillingMaster.BillNo and Customer.RegNo=BillingMaster.RegNo
		                and RegDate between @stdate and @endate
		                and BillingMaster.BillNo not in (select BillNo from CashReceipt)
		                Order by Billdate
	                end
                else
                begin
	                Select BillingMaster.BillNo [Bill No],Billdate [Bill date],Name [Customer Name] ,IName [Item Name],qty [Quantity],Rate,BillDetails.Tax,BillDetails.Discount,Total from BillingMaster,BillDetails,Item,Customer
	                where Item.ICode=BillDetails.Code and BillDetails.BillNo=BillingMaster.BillNo and Customer.RegNo=BillingMaster.RegNo
	                and RegDate between @stdate and @endate
	                and CId=@Cid
	                and BillingMaster.BillNo not in (select BillNo from CashReceipt)
	                Order by Billdate
                end";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #region LAB

        try
        {
            Sql = @"Alter Proc [dbo].[Sp_InsertLabInfo]
                    @Flag int,
                    @LId int,
                    @LName varchar(200),
                    @Code varchar(50),
                    @LAddress varchar(200),
                    @ContactNo varchar(50),
                    @MobileNo Varchar(50),
                    @Email varchar(100),
                    @Url varchar(100),
                    @PanNo varchar(50),
                    @Remarks varchar(200)
                    as
                    if @Flag=1
                    begin
	                    Insert into LabInfo(LName,Code,LAddress,ContactNo,MobileNo,Email,Url,PanNo,Remarks)
	                    values
	                    (@LName,@Code,@LAddress,@ContactNo,@MobileNo,@Email,@Url,@PanNo,@Remarks)
                    end
                    if @flag=2
                    begin
	                    delete from LabInfo
	                    Insert into LabInfo(LName,Code,LAddress,ContactNo,MobileNo,Email,Url,PanNo,Remarks)
	                    values
	                    (@LName,@Code,@LAddress,@ContactNo,@MobileNo,@Email,@Url,@PanNo,@Remarks)
                    end
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[sp_InsertDoctorMaster]
                        @Flag int,
                        @DId int,
                        @DName Varchar(250),
                        @NMCNo varchar(100),
                        @DType varchar(100),
                        @Specialization varchar(100),
                        @DAddress varchar(250),
                        @ContactNo varchar(50),
                        @MobileNo Varchar(50),
                        @Email varchar(50),
                        @Remarks varchar(100),
                        @Active char(1),
                        @CreatedBy varchar(100),
                        @CreatedDate NVarchar(100)
                        as
                        SET @CreatedBy=(Select UserId from UserMaster where UserId=@CreatedBy)
                        if @Flag=1
                        Begin
	                        Insert Into DoctorMaster(DName,NMCNo,DType,Specialization,DAddress,ContactNo,MobileNo,Email,Remarks,Active,CreatedBy,CreatedDate)
		                        values
		                        (@DName,@NMCNo,@DType,@Specialization,@DAddress,@ContactNo,@MobileNo,@Email,@Remarks,@Active,@CreatedBy,@CreatedDate)
                        end
                        if @Flag=2
                        begin
	                        Update DoctorMaster set
			                DName=@DName,
			                NMCNo=@NMCNo,
			                DType=@DType,
			                Specialization=@Specialization,
			                DAddress=@DAddress,
			                ContactNo=@ContactNo,
			                MobileNo=@MobileNo,
			                Email=@Email,
			                Remarks=@Remarks,
			                Active=@Active
				            where Did=@DId
                        end
                        if @flag=3
                        begin
	                        delete from DoctorMaster where Did=@DId
                        end ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter proc [dbo].[sp_FetchLabInfo]
                as
	            Select * from LabInfo
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[sp_FetchDoctorMaster]
                @DId int
                as
                select * from DoctorMaster where Did=@DId
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        #endregion LAB

        try
        {
            Sql = @"Alter Proc [dbo].[sp_CustomerList]
                    @Cid int,
                    @stdate datetime,
                    @endate datetime
                    as
                    if @Cid=0
                    begin
	                    select RegNo,Name [Customer Name],RegDate [Registration Date],PackageType [Package Type ],Area,Agent.AgentName as [Agent Name],Expdate [Expiry Date],Company,pan,Adress as [Customer Address],Customer.MobileNo,Customer.Email,dob [Date of Birth] from Customer Left outer join Agent on Agent.AgentId=Customer.AgentId
	                    where RegDate between @stdate and @endate
	                    Order by RegDate
                    end
                    else
                    begin
	                    select RegNo,Name [Customer Name],RegDate [Registration Date],PackageType [Package Type ],Area,Agent.AgentName as [Agent Name],Expdate [Expiry Date],Company,pan,Adress as [Customer Address],Customer.MobileNo,Customer.Email,dob [Date of Birth] from Customer Left outer join Agent on Agent.AgentId=Customer.AgentId
	                    where RegDate between @stdate and @endate
	                    and CId=@Cid
	                    Order by RegDate

                    end ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[sp_CustomerExp]
                    @Cid int,
                    @stdate datetime,
                    @endate datetime
                    as
                    if @Cid=0
                    begin
	                    select RegNo,Name [Customer Name],RegDate [Registration Date],PackageType [Package Type ],Area,Agent.AgentName as [Agent Name],Expdate [Expiry Date],Company,pan,Adress as [Customer Address],Customer.MobileNo,Customer.Email,dob [Date of Birth] from Customer Left outer join Agent on Agent.AgentId=Customer.AgentId
	                    where Expdate between @stdate and @endate
	                    Order by Expdate
                    end
                    else
                    begin
	                    select RegNo,Name [Customer Name],RegDate [Registration Date],PackageType [Package Type ],Area,Agent.AgentName as [Agent Name],Expdate [Expiry Date],Company,pan,Adress as [Customer Address],Customer.MobileNo,Customer.Email,dob [Date of Birth] from Customer Left outer join Agent on Agent.AgentId=Customer.AgentId
	                    where Expdate between @stdate and @endate
	                    and CId=@Cid
	                    Order by Expdate

                    end ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[sp_CashReceipt]
                    @Cid int,
                    @stdate datetime,
                    @endate datetime
                    as
                    if @Cid=0
	                begin
		                select VNo [Voucher No],Vdate [Voucher Date],CashReceipt.BillNo,Billdate,Name,Amount from CashReceipt,BillingMaster,Customer where CashReceipt.BillNo=BillingMaster.BillNo and Customer.RegNo=BillingMaster.RegNo
		                and Vdate between @stdate and @endate
		                Order by Billdate
	                end
                    else
	                begin
		                select VNo [Voucher No],Vdate [Voucher Date],CashReceipt.BillNo,Billdate,Name,Amount from CashReceipt,BillingMaster,Customer where CashReceipt.BillNo=BillingMaster.BillNo and Customer.RegNo=BillingMaster.RegNo
		                and Vdate between @stdate and @endate
		                and Cid=@Cid
		                Order by Billdate
	                end ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[sp_FetchUserMaster]
                    @UserId int
                    as
                    begin
	                    select * from UserMaster where UserId=@UserId
                    end
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql = @"Alter Proc [dbo].[Sp_FetchUser]
                    @UserName Varchar(100),
                    @UserPws Varchar(100)
                    as
                    Select Ulid,UPws from UserMaster where ulid=@UserName and Upws =@UserPws
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }
    }

    public static void InsertDefaultData()
    {
        var Sql = string.Empty;
        try
        {
            Sql =
                @"INSERT [dbo].[UserGroup] ([Name], [Code], [Remarks], [CreatedBy], [CreatedDate]) VALUES (N'ADMINISTRATOR', N'AD00001', NULL, N'OMS', NULL)
                        INSERT [dbo].[UserGroup] ([Name], [Code], [Remarks], [CreatedBy], [CreatedDate]) VALUES (N'ADMIN', N'AD00002', NULL, N'OMS', NULL)
                        INSERT [dbo].[UserGroup] ([Name], [Code], [Remarks], [CreatedBy], [CreatedDate]) VALUES (N'USER', N'US0001', NULL, N'OMS', NULL)

                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"INSERT [dbo].[UserMaster] ([UserId], [UName], [ULId], [UPws], [UType], [UDepartment], [UAddress], [ContactNo], [MobileNo], [Email], [Active], [Remarks],UGId) VALUES (N'1', N'Admin', N'Admin', N'˜•Œ‹', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,1)
                        INSERT [dbo].[UserMaster] ([UserId], [UName], [ULId], [UPws], [UType], [UDepartment], [UAddress], [ContactNo], [MobileNo], [Email], [Active], [Remarks],UGId) VALUES (N'2', N'OMS', N'OMS', N'ŠŒ†', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL,2)
                ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }

        try
        {
            Sql =
                @"INSERT [dbo].[systemsetting] ([DateType], [SalaryCal], [DBAccess_Path], [MaxDeductPremium], [AcPostingDatabase], [PostingAccountDr], [PostingAccountCr]) VALUES (N'D', N'M', N'', CAST(20000.000000 AS Decimal(18, 6)), N'0', N'0', N'0')
                    ";
            var cmd = new SqlCommand(Sql, GetConnection.ReturnConnection());
            cmd.CommandTimeout = 500;
            cmd.ExecuteNonQuery();
        }
        catch
        {
        }
    }
}