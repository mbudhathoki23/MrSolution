

IF OBJECT_ID('AMS.Usp_IUD_CompanyInfo') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_IUD_CompanyInfo as select @@servername')END;

IF OBJECT_ID('AMS.Usp_IUD_Branch') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_IUD_Branch as select @@servername')END;

IF OBJECT_ID('AMS.Usp_Select_EntryLogRegister') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_Select_EntryLogRegister as select @@servername')END;

IF OBJECT_ID('AMS.Usp_Select_CashBankBook') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_Select_CashBankBook  as select @@servername ;')END;

IF OBJECT_ID('AMS.Usp_Select_DayBook') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_Select_DayBook  as select @@servername')END;

IF OBJECT_ID('AMS.Usp_Select_JournalBook') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_Select_JournalBook   as select @@servername')END;

IF OBJECT_ID('AMS.Usp_Select_LedgerStatement') IS NULL 
BEGIN EXECUTE(' Create Proc AMS.Usp_Select_LedgerStatement   as select @@servername')END;

IF OBJECT_ID('AMS.USP_PostStockValue') IS NULL 
BEGIN EXECUTE('Create Proc AMS.USP_PostStockValue   as select @@servername')END;