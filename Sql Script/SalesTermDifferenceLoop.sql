DECLARE @voucherNo VARCHAR(MAX), @basicAmount DECIMAL(18,6), @discount DECIMAL(18,6), @vatAmount DECIMAL(18,6), @actualVat DECIMAL(18,6);
DECLARE cursor_product CURSOR FOR
SELECT vat.SB_Invoice VoucherNo, vat.BasicAmount, vat.Discount, vat.VatAmount, vat.ActualVat
FROM(SELECT sm.SB_Invoice, sm.B_Amount BasicAmount, ISNULL(st.Amount, 0) Discount, ISNULL(vt.Amount, 0) VatAmount, ROUND(CASE WHEN ISNULL(vt.Rate, 0)>0 THEN ((sm.B_Amount-ISNULL(st.Amount, 0))* 0.13)ELSE 0 END, 2) ActualVat
     FROM AMS.SB_Master sm
          LEFT OUTER JOIN AMS.SB_Term st ON st.SB_VNo=sm.SB_Invoice AND st.ST_Id=6 AND st.Term_Type='B'
          LEFT OUTER JOIN AMS.SB_Term vt ON vt.SB_VNo=sm.SB_Invoice AND vt.ST_Id=4 AND vt.Term_Type='B') vat
WHERE vat.VatAmount<>vat.ActualVat;
OPEN cursor_product;
FETCH NEXT FROM cursor_product
INTO @voucherNo, @basicAmount, @discount, @vatAmount, @actualVat;
WHILE @@FETCH_STATUS=0 BEGIN
	--PRINT @voucherNo + '-' + CAST ( @basicAmount AS NVARCHAR) + '-' + CAST ( @discount AS NVARCHAR) + '-' + CAST( @vatAmount AS NVARCHAR) + '-' + CAST(@actualVat AS NVARCHAR);
    UPDATE AMS.SB_Term SET Amount = @actualVat WHERE SB_VNo = @voucherNo AND ST_Id = 4 AND Term_Type ='B'
	UPDATE AMS.SB_Master SET T_Amount = @vatAmount - @discount WHERE SB_Invoice =@voucherNo
	UPDATE AMS.SB_Master SET N_Amount = B_Amount + T_Amount,LN_Amount = B_Amount + T_Amount WHERE SB_Invoice =@voucherNo
	UPDATE AMS.SB_Master SET In_Words =  dbo.fNumToWords(LN_Amount) WHERE SB_Invoice = @voucherNo
    FETCH NEXT FROM cursor_product
    INTO @voucherNo, @basicAmount, @discount, @vatAmount, @actualVat;
END;
CLOSE cursor_product;
DEALLOCATE cursor_product;

--DROP TRIGGER AMS.TR_PREVENT_UPDATE_FROM_SB_Master
--DROP TRIGGER AMS.TR_PREVENT_UPDATE_FROM_SB_Term

--SELECT * FROM AMS.SB_Term WHERE SB_VNo = 'ASB/00923/79-80'

--SELECT * FROM AMS.SB_Master WHERE SB_Invoice = 'ASB/00923/79-80'

--UPDATE AMS.SB_Master SET In_Words = dbo.fNumToWords(LN_Amount) WHERE SB_Invoice = 'ASB/00923/79-80'

--SELECT CONVERT(NVARCHAR(10),TRY_CAST(Invoice_Miti AS DATE),102) FROM AMS.SB_Master

--SELECT SUBSTRING(Invoice_Miti,7,4) + '-' + SUBSTRING(Invoice_Miti,4,2) + '.' + SUBSTRING(Invoice_Miti,1,2)  FROM AMS.SB_Master;

--SELECT * FROM AMS.ST_Term



SELECT * FROM AMS