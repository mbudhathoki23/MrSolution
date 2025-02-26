

SELECT * FROM AMS.DataServerInfo

SELECT * FROM AMS.GlobalCompany  

ALTER TABLE AMS.GlobalCompany ADD DataSyncOriginId UNIQUEIDENTIFIER NULL
ALTER TABLE AMS.GlobalCompany ADD DataSyncApiBaseUrl NVARCHAR(100) NULL

http://merotest-001-site1.htempurl.com
 
UPDATE AMS.GlobalCompany SET DataSyncOriginId=newid(),DataSyncApiBaseUrl = 'http://merotest-001-site1.htempurl.com' WHERE  DataSyncOriginId IS NULL	
