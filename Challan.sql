CREATE TABLE [AMS].[PC_Master](	[PC_Invoice] [nvarchar](50) NOT NULL,	[Invoice_Date] [datetime] NOT NULL,	[Invoice_Miti] [nvarchar](10) NOT NULL,	[Invoice_Time] [datetime] NULL,	[PB_Vno] [nvarchar](50) NULL,	[Vno_Date] [datetime] NULL,	[Vno_Miti] [nvarchar](10) NULL,	[Vendor_ID] [bigint] NOT NULL,	[Party_Name] [nvarchar](100) NULL,	[Vat_No] [nvarchar](50) NULL,	[Contact_Person] [nvarchar](50) NULL,	[Mobile_No] [nvarchar](50) NULL,	[Address] [nvarchar](90) NULL,	[ChqNo] [nvarchar](50) NULL,	[ChqDate] [datetime] NULL,	[Invoice_Type] [nvarchar](50) NOT NULL,	[Invoice_In] [nvarchar](50) NOT NULL,	[DueDays] [int] NULL,	[DueDate] [datetime] NULL,	[Agent_Id] [bigint] NULL,	[Subledger_Id] [bigint] NULL,	[PQT_Invoice] [nvarchar](250) NULL,	[PQT_Date] [datetime] NULL,	[PO_Invoice] [nvarchar](250) NULL,	[PO_Date] [datetime] NULL,	[Cls1] [bigint] NULL,	[Cls2] [bigint] NULL,	[Cls3] [bigint] NULL,	[Cls4] [bigint] NULL,	[Cur_Id] [int] NOT NULL,	[Cur_Rate] [decimal](18, 6) NOT NULL,	[B_Amount] [decimal](18, 6) NOT NULL,	[T_Amount] [decimal](18, 6) NOT NULL,	[N_Amount] [decimal](18, 6) NOT NULL,	[LN_Amount] [decimal](18, 6) NOT NULL,	[V_Amount] [decimal](18, 6) NULL,	[Tbl_Amount] [decimal](18, 6) NOT NULL,	[Action_type] [nvarchar](50) NOT NULL,	[R_Invoice] [bit] NULL,	[No_Print] [decimal](18, 0) NOT NULL,	[In_Words] [nvarchar](1024) NULL,	[Remarks] [nvarchar](500) NULL,	[Audit_Lock] [bit] NULL,	[Enter_By] [nvarchar](50) NULL,	[Enter_Date] [datetime] NULL,	[Reconcile_By] [nvarchar](50) NULL,	[Reconcile_Date] [datetime] NULL,	[Auth_By] [nvarchar](50) NULL,	[Auth_Date] [datetime] NULL,	[CUnit_Id] [bigint] NULL,	[CBranch_Id] [bigint] NOT NULL, CONSTRAINT [PK_PC] PRIMARY KEY CLUSTERED 
(	[PC_Invoice] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Branch] FOREIGN KEY([CBranch_Id])REFERENCES [AMS].[Branch] ([Branch_Id])
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Branch]
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_CompanyUnit] FOREIGN KEY([CUnit_Id])REFERENCES [AMS].[CompanyUnit] ([CmpUnit_Id])
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_CompanyUnit]
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_Currency] FOREIGN KEY([Cur_Id])REFERENCES [AMS].[Currency] ([CId])
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_Currency]
ALTER TABLE [AMS].[PC_Master]  WITH CHECK ADD  CONSTRAINT [FK_PC_Master_GeneralLedger] FOREIGN KEY([Vendor_ID])REFERENCES [AMS].[GeneralLedger] ([GLID])
ALTER TABLE [AMS].[PC_Master] CHECK CONSTRAINT [FK_PC_Master_GeneralLedger]



CREATE TABLE [AMS].[PC_Details](	[PC_Invoice] [nvarchar](50) NOT NULL,	[Invoice_SNo] [numeric](18, 0) NOT NULL,	[P_Id] [bigint] NOT NULL,	[Gdn_Id] [bigint] NULL,	[Alt_Qty] [decimal](18, 6) NULL,	[Alt_UnitId] [bigint] NULL,	[Qty] [decimal](18, 6) NOT NULL,	[Unit_Id] [bigint] NULL,	[Rate] [decimal](18, 6) NULL,	[B_Amount] [decimal](18, 6) NULL,	[T_Amount] [decimal](18, 6) NULL,	[N_Amount] [decimal](18, 6) NULL,	[AltStock_Qty] [decimal](18, 6) NULL,	[Stock_Qty] [decimal](18, 6) NULL,	[Narration] [nvarchar](500) NULL,	[PQT_Invoice] [nvarchar](50) NULL,	[PQT_Sno] [numeric](18, 0) NULL,	[PO_Invoice] [nvarchar](50) NULL,	[PO_SNo] [nvarchar](50) NULL,	[Tax_Amount] [decimal](18, 6) NULL,	[V_Amount] [decimal](18, 6) NULL,	[V_Rate] [decimal](18, 6) NULL,	[Free_Unit_Id] [bigint] NULL,	[Free_Qty] [decimal](18, 6) NULL,	[StockFree_Qty] [decimal](18, 6) NULL,	[ExtraFree_Unit_Id] [bigint] NULL,	[ExtraFree_Qty] [decimal](18, 6) NULL,	[ExtraStockFree_Qty] [decimal](18, 6) NULL,	[T_Product] [bit] NULL,	[P_Ledger] [bigint] NULL,	[PR_Ledger] [bigint] NULL,	[SZ1] [nvarchar](50) NULL,	[SZ2] [nvarchar](50) NULL,	[SZ3] [nvarchar](50) NULL,	[SZ4] [nvarchar](50) NULL,	[SZ5] [nvarchar](50) NULL,	[SZ6] [nvarchar](50) NULL,	[SZ7] [nvarchar](50) NULL,	[SZ8] [nvarchar](50) NULL,	[SZ9] [nvarchar](50) NULL,	[SZ10] [nvarchar](50) NULL,	[Serial_No] [nvarchar](50) NULL,	[Batch_No] [nvarchar](50) NULL,	[Exp_Date] [datetime] NULL,	[Manu_Date] [datetime] NULL) ON [PRIMARY]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_Godown] FOREIGN KEY([Gdn_Id])REFERENCES [AMS].[Godown] ([GID])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_Godown]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_PC_Master] FOREIGN KEY([PC_Invoice])REFERENCES [AMS].[PC_Master] ([PC_Invoice])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_PC_Master]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_Product] FOREIGN KEY([P_Id])REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_Product]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductUnit] FOREIGN KEY([Unit_Id])REFERENCES [AMS].[ProductUnit] ([UID])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductUnit]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductUnit1] FOREIGN KEY([Alt_UnitId])REFERENCES [AMS].[ProductUnit] ([UID])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductUnit1]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductUnit2] FOREIGN KEY([Free_Unit_Id])REFERENCES [AMS].[ProductUnit] ([UID])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductUnit2]
ALTER TABLE [AMS].[PC_Details]  WITH CHECK ADD  CONSTRAINT [FK_PC_Details_ProductUnit3] FOREIGN KEY([ExtraFree_Unit_Id])REFERENCES [AMS].[ProductUnit] ([UID])
ALTER TABLE [AMS].[PC_Details] CHECK CONSTRAINT [FK_PC_Details_ProductUnit3]



CREATE TABLE [AMS].[PC_Term](	[PC_VNo] [nvarchar](50) NOT NULL,	[PT_Id] [int] NOT NULL,	[SNo] [int] NULL,[Rate] [decimal](18, 6) NULL,	[Amount] [decimal](18, 6) NOT NULL,	[Term_Type] [char](2) NULL,	[Product_Id] [bigint] NULL,	[Taxable] [char](1) NULL) ON [PRIMARY]SET ANSI_PADDING OFF
ALTER TABLE [AMS].[PC_Term]  WITH CHECK ADD  CONSTRAINT [FK_PC_Term_Product] FOREIGN KEY([Product_Id]) REFERENCES [AMS].[Product] ([PID])
ALTER TABLE [AMS].[PC_Term] CHECK CONSTRAINT [FK_PC_Term_Product]
ALTER TABLE [AMS].[PC_Term]  WITH CHECK ADD  CONSTRAINT [FK_PC_Term_PT_Term] FOREIGN KEY([PT_Id]) REFERENCES [AMS].[PT_Term] ([PT_ID])
ALTER TABLE [AMS].[PC_Term] CHECK CONSTRAINT [FK_PC_Term_PT_Term]
