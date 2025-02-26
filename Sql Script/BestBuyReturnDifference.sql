
CREATE TABLE #temp 
(
	VoucherNo NVARCHAR(50) NOT NULL
)
INSERT INTO #temp (VoucherNo) 
SELECT Voucher_No Amount FROM AMS.AccountDetails
WHERE Module = 'SR'
GROUP BY Voucher_No
HAVING SUM(LocalDebit_Amt - LocalCredit_Amt) <> 0

UPDATE AMS.SR_Details SET T_Amount = ISNULL(a.TermAmount,0),N_Amount = B_Amount + ISNULL(a.TermAmount,0)  FROM (
SELECT sd.SR_Invoice,p.Product_Id,p.SNo,ISNULL(p.TermAmount,0) TermAmount FROM AMS.SR_Details sd
LEFT OUTER JOIN 
(
	SELECT st.SR_VNo, st.Product_Id,st.SNo, SUM(CASE WHEN S.ST_Sign='+' then st.Amount ELSE -st.Amount END) TermAmount
	FROM AMS.SR_Term st
		LEFT OUTER JOIN AMS.ST_Term s ON s.ST_ID=st.ST_Id
	WHERE st.Term_Type IN  ('P','BT')
	GROUP BY st.SR_VNo, st.Product_Id,st.SNo
) AS p ON p.SR_VNo = sd.SR_Invoice AND p.Product_Id = sd.P_Id
WHERE sd.SR_Invoice IN (SELECT * FROM #temp)
) a WHERE a.SR_Invoice = SR_Details.SR_Invoice AND a.Product_Id = P_Id AND a.SNo = Invoice_SNo 
and SR_Details.SR_Invoice IN (SELECT * FROM #temp)

UPDATE AMS.SR_Master SET B_Amount = 

DECLARE @voucherNo VARCHAR(MAX) 
DECLARE cursor_product CURSOR FOR
	SELECT VoucherNo FROM #temp
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


--UPDATE AMS.SR_Term SET Term_Type = 'B' WHERE Term_Type = 'P' AND Product_Id IS NULL

SELECT * FROM AMS.SR_Master WHERE SR_Invoice = 'SSSR/00023/79-80'

SELECT * FROM AMS.SR_Details WHERE SR_Invoice ='SSSR/00023/79-80'

SELECT * FROM AMS.SR_Term WHERE SR_VNo = 'SSSR/00023/79-80'


SELECT * FROM AMS.ST_Term

DELETE AMS.SR_Term WHERE Term_Type = 'BT'
INSERT INTO AMS.SR_Term(SR_VNo, ST_Id, SNo, Rate, Amount, Term_Type, Product_Id, Taxable, SyncGlobalId, SyncOriginId, SyncCreatedOn, SyncLastPatchedOn, SyncRowVersion, SyncBaseId)
SELECT sbt.SR_VNo, sbt.ST_Id, sd.Invoice_SNo AS SERIAL_NO,CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = 4 THEN 0 ELSE sbt.Rate END Rate,
CASE WHEN sm.Invoice_Type = 'P VAT' AND ISNULL(p.PTax,0) = 0 AND sbt.ST_Id = 4 THEN 0 ELSE (sbt.Amount/(CASE WHEN sm.Invoice_Mode = 'POS' THEN SM.B_Amount ELSE sm.LN_Amount END)) * sd.N_Amount END Amount, 'BT' Term_Type, sd.P_Id Product_Id, sbt.Taxable, sbt.SyncGlobalId, sbt.SyncOriginId, sbt.SyncCreatedOn, sbt.SyncLastPatchedOn, sbt.SyncRowVersion, sbt.SyncBaseId
FROM AMS.SR_Details sd
	LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=sd.SR_Invoice
	LEFT OUTER JOIN AMS.SR_Term sbt ON sd.SR_Invoice=sbt.SR_VNo
    LEFT OUTER JOIN AMS.Product P ON P.PID = sd.P_Id
WHERE sbt.Term_Type='B' AND Product_Id IS NULL