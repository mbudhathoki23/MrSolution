
DECLARE @voucherNo VARCHAR(MAX) 
DECLARE cursor_product CURSOR FOR
	SELECT SB_Invoice FROM AMS.SB_Master WHERE T_Amount > 0 AND SB_Invoice NOT IN (SELECT SB_VNo FROM  AMS.SB_Term WHERE ST_Id = 6)
OPEN cursor_product;
FETCH NEXT FROM cursor_product
INTO @voucherNo
WHILE @@FETCH_STATUS=0 BEGIN
		INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
		SELECT SB_Invoice,6,1,'B',NULL,0,T_Amount,'N',NULL,NULL,NULL,NULL,NULL,SyncRowVersion FROM AMS.SB_Master WHERE SB_Invoice = @voucherNo
       FETCH NEXT FROM cursor_product
    INTO @voucherNo
END;
CLOSE cursor_product;
DEALLOCATE cursor_product;



SELECT * FROM AMS.SB_Details WHERE SB_Invoice ='SB-001479-77\78'
SELECT * FROM AMS.SB_Term WHERE SB_VNo ='SB-001479-77\78'



INSERT INTO AMS.SB_Term(SB_VNo, ST_Id, SNo, Term_Type, Product_Id, Rate, Amount, Taxable, SyncBaseId, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion)
            SELECT sbt.SB_VNo SB_VNo, sbt.ST_Id ST_Id, sd.Invoice_SNo AS SNo, 'BT' Term_Type, sd.P_Id Product_Id,CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = 4  THEN 0 ELSE sbt.Rate END Rate,
            CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = 4  THEN 0 ELSE (sbt.Amount / (CASE WHEN sm.Invoice_Mode = 'POS' THEN sm.LN_Amount ELSE sm.B_Amount END))* sd.N_Amount END  Amount, CASE WHEN sd.T_Product=1 THEN 'Y' ELSE 'N' END Taxable, sbt.SyncBaseId, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, ISNULL(sbt.SyncRowVersion, 1) SyncRowVersion
            FROM AMS.SB_Details sd
	            LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
	            LEFT OUTER JOIN AMS.SB_Term sbt ON sd.SB_Invoice=sbt.SB_VNo
	            LEFT OUTER JOIN AMS.Product P ON P.PID = sd.P_Id
            WHERE sbt.Term_Type='B' AND Product_Id IS NULL
			AND sbt.SB_VNo = 'SB-001479-77\78'
            
SELECT * FROM AMS.SB_Master WHERE SB_Invoice='SB-000025-77\78'
SELECT * FROM AMS.SB_Details WHERE SB_Invoice = 'SB-000025-77\78'
SELECT * FROM AMS.SB_Term  WHERE SB_VNo = 'SB-000025-77\78'
SELECT * FROM AMS.SB_Details sd 
