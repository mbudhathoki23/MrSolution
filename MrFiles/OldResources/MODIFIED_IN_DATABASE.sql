

IF OBJECT_ID('[AMS].[Area]') IS NOT NULL
  BEGIN
    EXECUTE ('
ALTER TABLE [AMS].[Area] DROP CONSTRAINT [FK_Area_CompanyUnit];
ALTER TABLE [AMS].[Area] WITH CHECK ADD  CONSTRAINT [FK_Area_CompanyUnit] FOREIGN KEY([Company_Id])REFERENCES [AMS].[CompanyUnit] ([CmpUnit_ID]);
ALTER TABLE [AMS].[Area] CHECK CONSTRAINT [FK_Area_CompanyUnit];
    ') END;



IF OBJECT_ID('[AMS].[[Product]]') IS NULL
  BEGIN
    EXECUTE ('
	ALTER TABLE [AMS].[Product] DROP CONSTRAINT [FK_Product_Department];
    ALTER TABLE [AMS].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Department] FOREIGN KEY([CmpId])REFERENCES [AMS].[Department] ([DId]);
    ALTER TABLE [AMS].[Product] CHECK CONSTRAINT [FK_Product_Department];
    ') END;




EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SB_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SO_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SO_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SO_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SQ_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SQ_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SQ_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill Type: Local,Import' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SR_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Type'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sales,Counter,Abbriviated,Resturant' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SR_Master', @level2type=N'COLUMN',@level2name=N'Invoice_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Cash,Credit,Card' , @level0type=N'SCHEMA',@level0name=N'AMS', @level1type=N'TABLE',@level1name=N'SR_Master', @level2type=N'COLUMN',@level2name=N'Payment_Mode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bill of Materials' , @level0type=N'SCHEMA',@level0name=N'INV', @level1type=N'TABLE',@level1name=N'BOM_Master'