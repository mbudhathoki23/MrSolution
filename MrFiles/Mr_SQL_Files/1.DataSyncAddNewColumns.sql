----------- account ledger
ALTER TABLE AMS.GeneralLedger ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.AccountGroup ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 



------------ products----------------
ALTER TABLE AMS.Product ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.ProductUnit ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

INSERT INTO ms.OriginGroup
SELECT NEWID(),'M&R SOLUTION','M&R SOLUTION',GETDATE()

ALTER TABLE AMS.ProductGroup ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.ProductSubGroup ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

select NEWID()



---------- Sales ----------------

ALTER TABLE AMS.SB_Master ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.SB_Details ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.SB_Term ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 


------- sales returns ----------
ALTER TABLE AMS.SR_Master ADD SyncGlobalId UNIQUEIDENTIFIER  NULL, SyncOriginId UNIQUEIDENTIFIER  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO

ALTER TABLE AMS.SR_Details ADD SyncGlobalId UNIQUEIDENTIFIER  NULL, SyncOriginId UNIQUEIDENTIFIER  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.SR_Term ADD SyncGlobalId UNIQUEIDENTIFIER  NULL, SyncOriginId UNIQUEIDENTIFIER  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 


---------- purchase ---------------------
ALTER TABLE AMS.PB_Master ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.PB_Details ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.PB_Term ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 


----------purchase returns ---------------
ALTER TABLE AMS.PR_Master ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.PR_Details ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 

ALTER TABLE AMS.PR_Term ADD SyncGlobalId UniqueIdentifier  NULL, SyncOriginId UniqueIdentifier  NULL, 
SyncCreatedOn DATETIME  NULL, SyncLastPatchedOn DATETIME NULL, SyncRowVersion SMALLINT  NULL
GO 