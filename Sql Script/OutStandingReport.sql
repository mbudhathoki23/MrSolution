WITH LedgerOutStanding AS (SELECT lo.Module, lo.ModuleType, lo.Voucher_Date, lo.Ledger_ID, lo.GLName, lo.VoucherAmount, lo.BalanceAmount, SUM(lo.VoucherAmount-lo.BalanceAmount) OVER (PARTITION BY lo.GLName ORDER BY lo.GLName, lo.Voucher_Date) OutStanding, lo.DateAge
                           FROM(SELECT '' Module, '' ModuleType, '' Voucher_Date, ad.Ledger_ID, gl.GLName, 0 VoucherAmount, SUM(ad.LocalDebit_Amt) BalanceAmount, 0 DateAge
                                FROM AMS.AccountDetails ad
                                     LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
                                     LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
                                WHERE gl.GLType IN ('Vendor', 'Both')AND ad.LocalDebit_Amt>0
                                GROUP BY ad.Ledger_ID, gl.GLName
                                UNION ALL
                                SELECT ad.Module, md.ModuleType, ad.Voucher_Date, ad.Ledger_ID, gl.GLName, ad.LocalCredit_Amt VoucherAmount, 0 BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, GETDATE()) DateAge
                                FROM AMS.AccountDetails ad
                                     LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
                                     LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
                                WHERE gl.GLType IN ('Vendor', 'Both')AND ad.LocalCredit_Amt>0) AS lo )
SELECT * FROM LedgerOutStanding ls
WHERE ls.OutStanding > 0;
