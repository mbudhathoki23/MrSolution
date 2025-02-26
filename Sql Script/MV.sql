WITH VatRegister AS (SELECT vat.SB_Invoice, SUM(ISNULL(BasicAmount, 0)) BasicAmount, SUM(vat.TaxAmount) TaxAmount, SUM(ISNULL(vat.Discount, 0)) Discount, SUM(vat.Tax_FreeSales) Tax_FreeSales, SUM(vat.TaxableSales) TaxableSales
						FROM(SELECT sd.SB_Invoice, sd.P_Id, ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0) BasicAmount, ISNULL(sd.T_Amount, 0) TermAmount, ISNULL(sd.N_Amount, 0) NetAmout, ISNULL(v.TaxAmount, 0) TaxAmount, ISNULL(p.PDiscount, 0)+ISNULL(b.BDiscount, 0) Discount, CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0) -ISNULL(p.PDiscount,0))END Tax_FreeSales, CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0) - ISNULL(p.PDiscount,0))END TaxableSales
							FROM AMS.SB_Details sd
								LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
												FROM AMS.SB_Term
												WHERE ST_Id=2 AND Term_Type<>'B' --AND SB_VNo='POS/0000319/78-79'
												GROUP BY SB_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=v.SNo
								LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) PDiscount
												FROM AMS.SB_Term
												WHERE ST_Id IN (3)AND Term_Type='P' --AND SB_VNo='POS/0000319/78-79'
												GROUP BY SB_VNo, SNo, Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=p.SNo
								LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) BDiscount
												FROM AMS.SB_Term
												WHERE ST_Id IN (1)AND Term_Type='BT'-- AND SB_VNo='POS/0000319/78-79'
												GROUP BY SB_VNo, SNo, Product_Id) AS b ON b.Product_Id=sd.P_Id AND b.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=b.SNo
							WHERE 1=1 --AND sd.SB_Invoice='POS/0000319/78-79'
							)vat
						GROUP BY vat.SB_Invoice)
SELECT fy.BS_FY Fiscal_Year, v.SB_Invoice FromBill_No, sm.Invoice_Miti, gl.GLName Customer_Name, gl.PanNo Customer_PAN,sm.Invoice_Date Bill_Date,
CASE WHEN sm.R_Invoice=0 AND ISNULL(v.BasicAmount, 0)>0 THEN FORMAT(v.BasicAmount, '##,##0.00')ELSE '' END Amount, 
CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 THEN FORMAT(sm.N_Amount, '##,##0.00')ELSE '' END TotalAmount, 
CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Discount, 0)>0 THEN FORMAT(v.Discount, '##,##0.00')ELSE '' END Discount, 
CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Tax_FreeSales, 0)>0 THEN FORMAT(v.Tax_FreeSales, '##,##0.00')ELSE '' END TaxFree_Sales, 
CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxableSales, 0)>0 THEN FORMAT(v.TaxableSales, '##,###.00')ELSE '' END Taxable_Amount, 
CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxAmount, 0)>0 THEN FORMAT(v.TaxAmount, '##,###.00')ELSE '' END Tax_Amount, sm.Is_Printed Is_Printed, sm.IsAPIPosted, (CASE WHEN Action_Type IN ('Cancel', 'Return') THEN 'N' ELSE 'Y' END) Is_Active, sm.Printed_Date, sm.Enter_By Entered_By, sm.Payment_Mode, sm.Printed_By Printed_By, sm.IsRealtime, CASE WHEN sm.R_Invoice>0 THEN 12 ELSE 0 END IsGroup
FROM VatRegister v
	LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=v.SB_Invoice
	LEFT OUTER JOIN AMS.FiscalYear fy ON fy.FY_Id=sm.FiscalYearId
	LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
--WHERE sm.SB_Invoice='POS/0000319/78-79'
ORDER BY sm.Invoice_Date, sm.SB_Invoice;

--Fiscal_Year	FromBill_No	Customer_Name	Invoice_Miti	Customer_PAN	Bill_Date	Amount	Discount	TaxFree_Sales	Taxable_Amount	Tax_Amount	Is_Printed	IsAPIPosted	Is_Active	Printed_Time	Entered_By	Payment_Mode	Printed_By	IsRealtime	IsGroup
--78/79	POS/0000001/78-79	LAXMI CASH A/C	01/04/2078	NULL	2021-07-16	280.00	32.21	183.36	96.64		True	False	Y	2021-07-16 10:54:36.990	PUJA	Cash      	PUJA	NULL

--SELECT * FROM AMS.ST_Term