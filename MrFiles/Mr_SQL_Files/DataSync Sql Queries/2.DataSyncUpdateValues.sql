DECLARE @localOriginId UNIQUEIDENTIFIER = CONVERT(UNIQUEIDENTIFIER,N'86C2D93A-C367-451C-949D-06509117AD55')


--------------- account ledgers
UPDATE AMS.GeneralLedger SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(),  
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL


UPDATE AMS.AccountGroup SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.AccountSubGroup SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.JuniorAgent SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.Currency SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

---------------- products
UPDATE AMS.Product SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.ProductUnit SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL


UPDATE AMS.ProductGroup SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.ProductSubGroup SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = EnterDate, SyncBaseId = NEWID(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL



----------------- Sales
UPDATE AMS.SB_Master SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = Enter_Date, 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.SB_Details SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = (SELECT TOP 1 Invoice_Date FROM AMS.SB_Master m WHERE m.SB_Invoice = SB_Invoice), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.SB_Term SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = GETDATE(), SyncOriginId = @localOriginId
WHERE SyncGlobalId IS NULL


------------------ Sales Returns
UPDATE AMS.SR_Master SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = Enter_Date, 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.SR_Master SET 
	SyncGlobalId = NEWID(), 
	SyncRowVersion = 1, 
	SyncCreatedOn = GETDATE(),
	SyncOriginId = @localOriginId
WHERE SyncGlobalId IS NULL

UPDATE AMS.SR_Term SET 
	SyncGlobalId = NEWID(), 
	SyncRowVersion = 1, 
	SyncCreatedOn = GETDATE(),
	SyncOriginId =  @localOriginId
WHERE SyncGlobalId IS NULL





---------------  Purchases
UPDATE AMS.PB_Master SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = Enter_Date, 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.PB_Details SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = GETDATE(), SyncOriginId = @localOriginId
WHERE SyncGlobalId IS NULL

UPDATE AMS.PB_Term SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = GETDATE(),
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL


----------------- Purchase Returns
UPDATE AMS.PR_Master SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = Enter_Date, 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.PR_Details SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = GETDATE(), 
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL

UPDATE AMS.PB_Term SET SyncGlobalId = NEWID(), SyncRowVersion = 1, SyncCreatedOn = GETDATE(),
SyncOriginId = @localOriginId WHERE SyncGlobalId IS NULL