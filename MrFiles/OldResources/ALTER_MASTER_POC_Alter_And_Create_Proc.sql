IF OBJECT_ID ( 'AMS.SoftwareRegistration' ) IS NULL BEGIN
EXECUTE ('Alter Table AMS.SoftwareRegistration add Module nvarchar (50), System_Id nvarchar (500), ActivationCode nvarchar (500), Server_MacAdd  nvarchar (500), Server_Desc nvarchar (50);')
END;

IF OBJECT_ID ( 'AMS.UserInfo' ) IS NULL BEGIN
EXECUTE ('Alter table AMS.UserInfo add Category  nvarchar  (50), Authorized   bit ;')
END;

IF OBJECT_ID ( 'AMS.SoftwareRegistration' ) IS NULL BEGIN
EXECUTE ('ALTER TABLE AMS.SoftwareRegistration ADD IsOnline  bit;')
END;

IF OBJECT_ID ( 'AMS.Usp_IUD_AccTransaction' ) IS NULL BEGIN
EXECUTE ('CREATE PROC AMS.Usp_IUD_AccTransaction  AS SELECT @@servername;')
END;

