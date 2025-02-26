using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Reports.CrystalReports.Interface;
namespace MrDAL.Reports.CrystalReports.RawQuery;
public class GetQueryReport : IQueryReport
{
    public string GetCustomerLedgerBalanceConfirmation(string partyLedgerId, int fiscalYearId, decimal aboveAmount, bool includeVat = false)
    {
        var cmdString = $@"
        SELECT gl.GLID LedgerId, gl.GLName LedgerName,gl.GLAddress LedgerAddress, gl.GLCode ShortName, gl.PanNo LedgerPanNo,
        CASE WHEN ABS(ob.OpeningBalance) > 0 THEN FORMAT(ABS(ob.OpeningBalance),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END OpeningBalance,
        CASE WHEN ISNULL(ob.OpeningBalance,0) > 0 THEN 'Dr' WHEN ISNULL(ob.OpeningBalance,0) < 0 THEN 'Cr' ELSE '' END OpeningType,
        CASE WHEN ISNULL(sb.NetAmount,0) > 0 THEN FORMAT(ISNULL(sb.NetAmount,0) + ISNULL(sb.TaxAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TotalNetAmount,
        CASE WHEN ISNULL(sb.NetAmount,0) > 0 THEN FORMAT(ISNULL(sb.NetAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END NetAmount,
        CASE WHEN ISNULL(sb.TaxAmount,0) > 0 THEN FORMAT(ISNULL(sb.TaxAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxAmount,
        CASE WHEN ISNULL(sb.TaxableAmount,0) > 0 THEN FORMAT(ISNULL(sb.TaxableAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxableAmount,
        CASE WHEN ISNULL(sb.TaxExempted,0) > 0 THEN FORMAT(ISNULL(sb.TaxExempted,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END  TaxExempted,
		CASE WHEN ISNULL(sr.NetReturnAmount,0) > 0 THEN FORMAT(ISNULL(sr.NetReturnAmount,0) + ISNULL(sr.TaxReturnAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END  TotalNetReturnAmount,
		CASE WHEN ISNULL(sr.NetReturnAmount,0) > 0 THEN FORMAT(ISNULL(sr.NetReturnAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END  NetReturnAmount,
		CASE WHEN ISNULL(sr.TaxReturnAmount,0) > 0 THEN FORMAT(ISNULL(sr.TaxReturnAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxReturnAmount,
		CASE WHEN ISNULL(sr.TaxableReturn,0) > 0 THEN FORMAT(ISNULL(sr.TaxableReturn,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxableReturn,
		CASE WHEN ISNULL(sr.TaxReturnExempted,0) > 0 THEN FORMAT(ISNULL(sr.TaxReturnExempted,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxReturnExempted,
        CASE WHEN ABS(cb.ClosingBalance) > 0 THEN FORMAT(ABS(cb.ClosingBalance),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END ClosingBalance,
        CASE WHEN ISNULL(cb.ClosingBalance,0) > 0 THEN 'Dr' WHEN ISNULL(cb.ClosingBalance,0) < 0 THEN 'Cr' ELSE '' END ClosingType,0 IsGroup
        FROM AMS.GeneralLedger gl
	        LEFT OUTER JOIN(SELECT sm.Customer_Id LedgerId, SUM(ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0)) BasicAmount, SUM(ISNULL(sd.T_Amount, 0)) TermAmount, SUM(ISNULL(sd.N_Amount, 0) - ISNULL(b.BDiscount, 0)) NetAmount, SUM(ISNULL(v.TaxAmount, 0)) TaxAmount, SUM(ISNULL(p.PDiscount, 0)+ISNULL(b.BDiscount, 0)) Discount, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxExempted, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxableAmount
					        FROM AMS.SB_Details sd
						        LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
										        FROM AMS.SB_Term
										        WHERE ST_Id={ObjGlobal.SalesVatTermId} AND Term_Type<>'B'
										        GROUP BY SB_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=v.SNo
						        LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) PDiscount
										        FROM AMS.SB_Term
										        WHERE ST_Id IN ({ObjGlobal.SalesDiscountTermId})AND Term_Type='P'
										        GROUP BY SB_VNo, SNo, Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=p.SNo
						        LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) BDiscount
										        FROM AMS.SB_Term
										        WHERE ST_Id IN ({ObjGlobal.SalesSpecialDiscountTermId})AND Term_Type='BT'
										        GROUP BY SB_VNo, SNo, Product_Id) AS b ON b.Product_Id=sd.P_Id AND b.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=b.SNo
						        LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
					        WHERE sm.FiscalYearId={fiscalYearId} AND ISNULL(sm.R_Invoice, 0)=0
					        GROUP BY sm.Customer_Id) AS sb ON sb.LedgerId=gl.GLID
	        LEFT OUTER JOIN(SELECT sm.Customer_Id LedgerId, SUM(ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0)) BasicAmount, SUM(ISNULL(sd.T_Amount, 0)) TermAmount, SUM(ISNULL(sd.N_Amount, 0) - ISNULL(b.BDiscount, 0)) NetReturnAmount, SUM(ISNULL(v.TaxAmount, 0)) TaxReturnAmount, SUM(ISNULL(p.PDiscount, 0)+ISNULL(b.BDiscount, 0)) Discount, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxReturnExempted, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxableReturn
					        FROM AMS.SR_Details sd
						        LEFT OUTER JOIN(SELECT SR_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
										        FROM AMS.SR_Term
										        WHERE ST_Id={ObjGlobal.SalesVatTermId} AND Term_Type<>'B'
										        GROUP BY SR_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SR_VNo=sd.SR_Invoice AND sd.Invoice_SNo=v.SNo
						        LEFT OUTER JOIN(SELECT SR_VNo, SNo, Product_Id, SUM(Amount) PDiscount
										        FROM AMS.SR_Term
										        WHERE ST_Id IN ({ObjGlobal.SalesDiscountTermId})AND Term_Type='P'
										        GROUP BY SR_VNo, SNo, Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SR_VNo=sd.SR_Invoice AND sd.Invoice_SNo=p.SNo
						        LEFT OUTER JOIN(SELECT SR_VNo, SNo, Product_Id, SUM(Amount) BDiscount
										        FROM AMS.SR_Term
										        WHERE ST_Id IN ({ObjGlobal.SalesSpecialDiscountTermId})AND Term_Type='BT'
										        GROUP BY SR_VNo, SNo, Product_Id) AS b ON b.Product_Id=sd.P_Id AND b.SR_VNo=sd.SR_Invoice AND sd.Invoice_SNo=b.SNo
						        LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=sd.SR_Invoice
					        WHERE sm.FiscalYearId={fiscalYearId} AND ISNULL(sm.R_Invoice, 0)=0
					        GROUP BY sm.Customer_Id) AS sr ON sr.LedgerId=gl.GLID
	        LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) OpeningBalance
					        FROM AMS.AccountDetails ad1
					        WHERE ad1.Voucher_Date < (SELECT TOP(1) Start_ADDate FROM AMS.FiscalYear WHERE FY_Id =  {fiscalYearId}  ORDER BY FY_Id)
					        GROUP BY Ledger_ID)ob ON ob.Ledger_ID=gl.GLID
	        LEFT OUTER JOIN(SELECT ad.Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) ClosingBalance
					        FROM AMS.AccountDetails ad
					        WHERE ad.FiscalYearId <= {fiscalYearId}
					        GROUP BY ad.Ledger_ID) AS cb ON cb.Ledger_ID=gl.GLID
        WHERE gl.GLType IN ('Customer', 'Both')AND ISNULL(sb.NetAmount, 0)+ISNULL(sr.NetReturnAmount, 0)>0 ";
        cmdString += partyLedgerId.IsValueExits() ? $" AND gl.GLID in ({partyLedgerId})" : "";
        cmdString += " ORDER BY gl.GLName; ";
        return cmdString;
    }

    public string GetVendorLedgerBalanceConfirmation(string partyLedgerId, int fiscalYearId, decimal aboveAmount, bool includeVat = false)
    {
        var cmdString = $@"
        SELECT gl.GLID LedgerId, gl.GLName LedgerName,gl.GLAddress LedgerAddress, gl.GLCode ShortName, gl.PanNo LedgerPanNo,
        CASE WHEN ABS(ob.OpeningBalance) > 0 THEN FORMAT(ABS(ob.OpeningBalance),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END OpeningBalance,
        CASE WHEN ISNULL(ob.OpeningBalance,0) > 0 THEN 'Dr' WHEN ISNULL(ob.OpeningBalance,0) < 0 THEN 'Cr' ELSE '' END OpeningType,
        CASE WHEN ISNULL(sb.NetAmount,0) > 0 THEN FORMAT(ISNULL(sb.NetAmount,0) + ISNULL(sb.TaxAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TotalNetAmount,
        CASE WHEN ISNULL(sb.NetAmount,0) > 0 THEN FORMAT(ISNULL(sb.NetAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END NetAmount,
        CASE WHEN ISNULL(sb.TaxAmount,0) > 0 THEN FORMAT(ISNULL(sb.TaxAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxAmount,
        CASE WHEN ISNULL(sb.TaxableAmount,0) > 0 THEN FORMAT(ISNULL(sb.TaxableAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxableAmount,
        CASE WHEN ISNULL(sb.TaxExempted,0) > 0 THEN FORMAT(ISNULL(sb.TaxExempted,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END  TaxExempted,
		CASE WHEN ISNULL(sr.NetReturnAmount,0) > 0 THEN FORMAT(ISNULL(sr.NetReturnAmount,0) + ISNULL(sr.TaxReturnAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END  TotalNetReturnAmount,
		CASE WHEN ISNULL(sr.NetReturnAmount,0) > 0 THEN FORMAT(ISNULL(sr.NetReturnAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END  NetReturnAmount,
		CASE WHEN ISNULL(sr.TaxReturnAmount,0) > 0 THEN FORMAT(ISNULL(sr.TaxReturnAmount,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxReturnAmount,
		CASE WHEN ISNULL(sr.TaxableReturn,0) > 0 THEN FORMAT(ISNULL(sr.TaxableReturn,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxableReturn,
		CASE WHEN ISNULL(sr.TaxReturnExempted,0) > 0 THEN FORMAT(ISNULL(sr.TaxReturnExempted,0),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END TaxReturnExempted,
        CASE WHEN ABS(cb.ClosingBalance) > 0 THEN FORMAT(ABS(cb.ClosingBalance),'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END ClosingBalance,
        CASE WHEN ISNULL(cb.ClosingBalance,0) > 0 THEN 'Dr' WHEN ISNULL(cb.ClosingBalance,0) < 0 THEN 'Cr' ELSE '' END ClosingType,0 IsGroup
        FROM AMS.GeneralLedger gl
	        LEFT OUTER JOIN(SELECT sm.Vendor_ID LedgerId, SUM(ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0)) BasicAmount, SUM(ISNULL(sd.T_Amount, 0)) TermAmount, SUM(ISNULL(sd.N_Amount, 0) - ISNULL(b.BDiscount, 0)) NetAmount, SUM(ISNULL(v.TaxAmount, 0)) TaxAmount, SUM(ISNULL(p.PDiscount, 0)+ISNULL(b.BDiscount, 0)) Discount, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxExempted, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxableAmount
					        FROM AMS.PB_Details sd
						        LEFT OUTER JOIN(SELECT PB_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
										        FROM AMS.PB_Term
										        WHERE PT_Id={ObjGlobal.PurchaseVatTermId} AND Term_Type<>'B'
										        GROUP BY PB_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.PB_VNo=sd.PB_Invoice AND sd.Invoice_SNo=v.SNo
						        LEFT OUTER JOIN(SELECT PB_VNo, SNo, Product_Id, SUM(Amount) PDiscount
										        FROM AMS.PB_Term
										        WHERE PT_Id IN ({ObjGlobal.PurchaseProductDiscountTermId})AND Term_Type='P'
										        GROUP BY PB_VNo, SNo, Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.PB_VNo=sd.PB_Invoice AND sd.Invoice_SNo=p.SNo
						        LEFT OUTER JOIN(SELECT PB_VNo, SNo, Product_Id, SUM(Amount) BDiscount
										        FROM AMS.PB_Term
										        WHERE PT_Id IN ({ObjGlobal.PurchaseDiscountTermId})AND Term_Type='BT'
										        GROUP BY PB_VNo, SNo, Product_Id) AS b ON b.Product_Id=sd.P_Id AND b.PB_VNo=sd.PB_Invoice AND sd.Invoice_SNo=b.SNo
						        LEFT OUTER JOIN AMS.PB_Master sm ON sm.PB_Invoice=sd.PB_Invoice
					        WHERE sm.FiscalYearId={fiscalYearId} AND ISNULL(sm.R_Invoice, 0)=0
					        GROUP BY sm.Vendor_ID) AS sb ON sb.LedgerId=gl.GLID
	        LEFT OUTER JOIN(SELECT sm.Vendor_ID LedgerId, SUM(ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0)) BasicAmount, SUM(ISNULL(sd.T_Amount, 0)) TermAmount, SUM(ISNULL(sd.N_Amount, 0) - ISNULL(b.BDiscount, 0)) NetReturnAmount, SUM(ISNULL(v.TaxAmount, 0)) TaxReturnAmount, SUM(ISNULL(p.PDiscount, 0)+ISNULL(b.BDiscount, 0)) Discount, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxReturnExempted, SUM(CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount-ISNULL(b.BDiscount, 0)-ISNULL(p.PDiscount, 0))END) TaxableReturn
					        FROM AMS.PR_Details sd
						        LEFT OUTER JOIN(SELECT PR_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
										        FROM AMS.PR_Term
										        WHERE PT_Id={ObjGlobal.PurchaseVatTermId} AND Term_Type<>'B'
										        GROUP BY PR_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.PR_VNo=sd.PR_Invoice AND sd.Invoice_SNo=v.SNo
						        LEFT OUTER JOIN(SELECT PR_VNo, SNo, Product_Id, SUM(Amount) PDiscount
										        FROM AMS.PR_Term
										        WHERE PT_Id IN ({ObjGlobal.PurchaseProductDiscountTermId})AND Term_Type='P'
										        GROUP BY PR_VNo, SNo, Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.PR_VNo=sd.PR_Invoice AND sd.Invoice_SNo=p.SNo
						        LEFT OUTER JOIN(SELECT PR_VNo, SNo, Product_Id, SUM(Amount) BDiscount
										        FROM AMS.PR_Term
										        WHERE PT_Id IN ({ObjGlobal.PurchaseDiscountTermId})AND Term_Type='BT'
										        GROUP BY PR_VNo, SNo, Product_Id) AS b ON b.Product_Id=sd.P_Id AND b.PR_VNo=sd.PR_Invoice AND sd.Invoice_SNo=b.SNo
						        LEFT OUTER JOIN AMS.PR_Master sm ON sm.PR_Invoice=sd.PR_Invoice
					        WHERE sm.FiscalYearId={fiscalYearId} AND ISNULL(sm.R_Invoice, 0)=0
					        GROUP BY sm.Vendor_ID) AS sr ON sr.LedgerId=gl.GLID
	        LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) OpeningBalance
					        FROM AMS.AccountDetails
					        WHERE Voucher_Date < (SELECT TOP(1) Start_ADDate FROM AMS.FiscalYear WHERE FY_Id =  {fiscalYearId}  ORDER BY FY_Id)
					        GROUP BY Ledger_ID)ob ON ob.Ledger_ID=gl.GLID
	        LEFT OUTER JOIN(SELECT ad.Ledger_ID, SUM(ad.LocalDebit_Amt  - ad.LocalCredit_Amt) ClosingBalance
					        FROM AMS.AccountDetails ad
					        WHERE ad.FiscalYearId <={fiscalYearId}
					        GROUP BY ad.Ledger_ID) AS cb ON cb.Ledger_ID=gl.GLID
        WHERE gl.GLType IN ('Vendor', 'Both')AND ISNULL(sb.NetAmount, 0)+ISNULL(sr.NetReturnAmount, 0)>0  ";
        cmdString += partyLedgerId.IsValueExits() ? $" AND gl.GLID in ({partyLedgerId})" : "";
        cmdString += " ORDER BY gl.GLName; ";
        return cmdString;
    }

    public string GetFiscalYear(int fiscalYearId)
    {
        var cmdString = @$"
        SELECT SUBSTRING(Start_BSDate,7,4) + '/' + SUBSTRING(End_BSDate,9,2) FiscalYear  FROM AMS.FiscalYear WHERE FY_Id = {fiscalYearId}";
        return cmdString.GetQueryData();
    }

    public string GetPartyLedgerTransaction(long ledgerId, int fiscalYearId)
    {
        var cmdString = @$"
        SELECT l.Module,l.Voucher_Date,l.Voucher_Miti, l.Voucher_No, l.GLName, l.DebitAmount, l.CreditAmount,SUM(l.DebitAmount - l.CreditAmount) OVER (ORDER BY l.Voucher_Date,l.Voucher_No ROWS UNBOUNDED PRECEDING) Balance
        FROM(SELECT 'OB' Module, '' Voucher_No,'' Voucher_Date,'' Voucher_Miti, gl.GLName, CASE WHEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)>0 THEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)ELSE 0 END DebitAmount, CASE WHEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)<0 THEN SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt)ELSE 0 END CreditAmount
             FROM AMS.AccountDetails ad
                  LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
             WHERE ad.Ledger_ID={ledgerId} AND ad.Voucher_Date<(SELECT Start_ADDate FROM AMS.FiscalYear WHERE FY_Id  = {fiscalYearId})
             GROUP BY ad.Ledger_ID, gl.GLName
             UNION ALL
             SELECT ad.Module, ad.Voucher_No,ad.Voucher_Date,ad.Voucher_Miti, gl.GLName, ad.LocalDebit_Amt, ad.LocalCredit_Amt
             FROM AMS.AccountDetails ad
                  LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
             WHERE ad.Ledger_ID={ledgerId} AND ad.FiscalYearId={fiscalYearId} AND ad.Voucher_Date >= (SELECT Start_ADDate FROM AMS.FiscalYear WHERE FY_Id  = {fiscalYearId})) AS l
	         GROUP BY l.Module, l.Voucher_No, l.GLName, l.DebitAmount, l.CreditAmount,l.Voucher_Date,l.Voucher_Miti";
        return cmdString;
    }

    public string GetLedgerOutStandingReport(string partyLedgerId)
    {
        var cmdString = @"
        WITH LedgerOutstanding AS (SELECT ISNULL(o.RowNo,P.RowNo) RowNo,ISNULL( o.Ledger_ID,P.Ledger_ID)Ledger_ID, ISNULL(o.GLName,p.GLName) GLName, o.Voucher_No, ISNULL(o.Voucher_Date,P.Voucher_Date) Voucher_Date, o.Voucher_Miti, o.LocalDebit_Amt, p.Voucher_No AdjustVoucher, p.Voucher_Date AdjustVoucherDate, p.Voucher_Miti AdjustVoucherMiti, p.LocalCredit_Amt, o.LocalDebit_Amt-ISNULL(p.LocalCredit_Amt, 0) Balance
                                   FROM(SELECT ROW_NUMBER() OVER (PARTITION BY ad.Ledger_ID ORDER BY ad.Ledger_ID,ad.Voucher_Date) RowNo, ad.Ledger_ID, gl.GLName, ad.Voucher_No, ad.Voucher_Date, ad.Voucher_Miti, ad.LocalDebit_Amt
                                        FROM AMS.AccountDetails ad
                                             INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
                                        WHERE gl.GLType IN ('Customer', 'Both')AND ad.LocalDebit_Amt>0)o
                                       FULL OUTER JOIN(SELECT ROW_NUMBER() OVER (PARTITION BY ad.Ledger_ID ORDER BY ad.Ledger_ID,ad.Voucher_Date) RowNo, ad.Ledger_ID, gl.GLName, ad.Voucher_No, ad.Voucher_Date, ad.Voucher_Miti, ad.LocalCredit_Amt
                                                       FROM AMS.AccountDetails ad
                                                            INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
                                                       WHERE gl.GLType IN ('Customer', 'Both')AND ad.LocalCredit_Amt>0)p ON p.Ledger_ID=o.Ledger_ID AND p.RowNo=o.RowNo)
        SELECT lo.Ledger_ID, lo.GLName, lo.Voucher_No, lo.Voucher_Date, lo.Voucher_Miti, lo.LocalDebit_Amt, lo.AdjustVoucher, lo.AdjustVoucherDate, lo.AdjustVoucherMiti, lo.LocalCredit_Amt,
        SUM(lo.Balance) OVER (PARTITION BY lo.GLName ORDER BY lo.GLName,lo.Voucher_Date ROWS UNBOUNDED PRECEDING) Balance
        FROM LedgerOutstanding lo
        GROUP BY lo.RowNo, lo.Ledger_ID, lo.GLName, lo.Voucher_No, lo.Voucher_Date, lo.Voucher_Miti, lo.LocalDebit_Amt, lo.AdjustVoucher, lo.AdjustVoucherDate, lo.AdjustVoucherMiti, lo.LocalCredit_Amt,lo.Balance
        ORDER BY lo.GLName,lo.Voucher_Date,lo.Voucher_No ";
        return cmdString;
    }
}