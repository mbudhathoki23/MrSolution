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
	[R_Invoice] [bit] NULL,
	[No_Print] [decimal](18, 0) NULL,
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
 CONSTRAINT [PK_SB] PRIMARY KEY CLUSTERED 
(
	[SB_Invoice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
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

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
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