

IF OBJECT_ID('AMS.Usp_IUD_User_Role') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_IUD_User_Role as select @@servername;')END;

IF OBJECT_ID('AMS.Usp_IUD_UserInfo') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_IUD_UserInfo as select @@servername;')END;

IF OBJECT_ID('AMS.Usp_IUD_Menu_Rights') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_IUD_Menu_Rights as select @@servername;')END;

IF OBJECT_ID('AMS.Usp_MSelect_Data') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_MSelect_Data as select @@servername;')END;

IF OBJECT_ID('AMS.Usp_MSelect_Data') IS NULL 
BEGIN EXECUTE('Create Proc AMS.Usp_MSelect_Data as select @@servername;')END;

