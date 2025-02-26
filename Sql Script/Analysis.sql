SELECT cb.Module, CONVERT(NVARCHAR(10), cb.Voucher_Date, 103) Voucher_Date, cb.Voucher_Miti, cb.Voucher_No, cb.Ledger, cb.PartyLedger, cb.Narration, cb.CCode, CASE WHEN cb.Currency_Rate>0 THEN FORMAT(cb.Currency_Rate, '##,##0.00')ELSE '' END Currency_Rate, CASE WHEN cb.Receipt>0 THEN FORMAT(cb.Receipt, '##,##0.00')ELSE '' END Receipt, CASE WHEN cb.Payment>0 THEN FORMAT(cb.Payment, '##,##0.00')ELSE '' END Payment, FORMAT(SUM(cb.Balance) OVER (PARTITION BY cb.Ledger
																																																																																																															ORDER BY cb.Ledger, cb.Voucher_Date
																																																																																																															ROWS UNBOUNDED PRECEDING), '##,##0.00') Balance, '' BalanceType, cb.Remarks, 0 IsGroup
FROM(
SELECT 'OB' Module, 'OB' Voucher_No, '' Voucher_Date, '' Voucher_Miti, gl.GLName Ledger, 'OPENING BALANCE' PartyLedger, '' Narration, 'NPR' CCode, 1 Currency_Rate, CASE WHEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)>0 THEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)ELSE 0 END Receipt, CASE WHEN SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt)>0 THEN SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt)ELSE 0 END Payment, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Balance, '' Remarks
	FROM AMS.AccountDetails ad
		INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
		INNER JOIN AMS.GeneralLedger pl ON pl.GLID=ad.CbLedger_ID
	WHERE ad.Ledger_ID IN (1)AND ad.Voucher_Date<'2022-07-16' AND ad.Branch_ID=1
	GROUP BY gl.GLName
	UNION ALL
	SELECT ad.Module, Voucher_No, CAST(Voucher_Date AS DATE) Voucher_Date, Voucher_Miti, gl.GLName Ledger, pl.GLName PartyLedger, ad.Narration, c.CCode, ad.Currency_Rate, ad.LocalDebit_Amt Receipt, ad.LocalCredit_Amt Payment, ad.LocalDebit_Amt-ad.LocalCredit_Amt Balance, ad.Remarks
	FROM AMS.AccountDetails ad
		INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
		INNER JOIN AMS.GeneralLedger pl ON pl.GLID=ad.CbLedger_ID
		INNER JOIN AMS.Currency c ON c.CId=ad.Currency_ID
	WHERE ad.Ledger_ID IN (1)AND ad.FiscalYearId=13 AND ad.Voucher_Date BETWEEN '2022-07-16' AND '2023-07-15' AND ad.Branch_ID=1)cb
GROUP BY cb.Module, cb.Voucher_No, cb.Voucher_Date, cb.Voucher_Miti, cb.Ledger, cb.PartyLedger, cb.Narration, cb.Receipt, cb.Payment, cb.Balance, cb.Remarks, cb.CCode, cb.Currency_Rate
ORDER BY cb.Voucher_Date, cb.Voucher_No, cb.PartyLedger, cb.Receipt;