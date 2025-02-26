using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Reports.Interface;
using MrDAL.Reports.ViewModule;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MrDAL.Reports.Finance;

public class ClsFinanceReport : IFinanceReport
{
    // FINANCE REPORTS

    #region --------------- FINANCE REPORTS ---------------

    public ClsFinanceReport()
    {
        GetReports = new VmFinanceReports();
    }

    #endregion --------------- FINANCE REPORTS ---------------

    //
    // BALANCE SHEET

    #region --------------- TRIAL BALANCE ---------------

    //NORMAL TRIAL BALANCE

    #region **----- NORMAL TRIAL BALANCE -----**

    private DataTable GetTrialBalanceReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dt_LedgerId",
            "dt_ShortName",
            "dt_Desc",
            "dt_Debit",
            "dt_LocalDebit",
            "dt_Credit",
            "dt_LocalCredit",
            "dt_LedgerPan",
            "dt_LedgerPhone",
            "dt_LedgerAddress"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnTrialBalanceLedgerWise(DataTable dtTrial)
    {
        DataRow newRow;
        var dtTotal = dtTrial.Select("LedgerId <> 0").CopyToDataTable();

        newRow = dtTrial.NewRow();

        newRow["LedgerName"] = "GRAND TOTAL :- ";
        var totalDebit = dtTotal.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var totalCredit = dtTotal.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        newRow["DebitAmount"] = totalDebit.GetDecimalComma();
        newRow["CreditAmount"] = totalCredit.GetDecimalComma();
        newRow["IsGroup"] = 99;
        dtTrial.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        if (!totalDebit.Equals(totalCredit))
        {
            var diffAmount = totalDebit - totalCredit;
            newRow = dtTrial.NewRow();
            newRow["LedgerName"] = "DIFFERENCE IN TB :- ";
            newRow["DebitAmount"] = diffAmount < 0 ? Math.Abs(diffAmount).GetDecimalComma() : string.Empty;
            newRow["CreditAmount"] = diffAmount > 0 ? diffAmount.GetDecimalComma() : string.Empty;
            newRow["IsGroup"] = 88;
            dtTrial.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        }

        return dtTrial;
    }

    private DataTable ReturnTrialBalanceLedgerWiseWithSubledger(DataTable dtTrial)
    {
        DataRow newRow;
        var dtTotal = dtTrial.Select("LedgerId <> 0").CopyToDataTable();

        newRow = dtTrial.NewRow();

        newRow["LedgerName"] = "GRAND TOTAL :- ";
        var totalDebit = dtTotal.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var totalCredit = dtTotal.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        newRow["DebitAmount"] = totalDebit.GetDecimalComma();
        newRow["CreditAmount"] = totalCredit.GetDecimalComma();
        newRow["IsGroup"] = 99;
        dtTrial.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        if (!totalDebit.Equals(totalCredit))
        {
            var diffAmount = totalDebit - totalCredit;
            newRow = dtTrial.NewRow();
            newRow["LedgerName"] = "DIFFERENCE IN TB :- ";
            newRow["DebitAmount"] = diffAmount < 0 ? Math.Abs(diffAmount).GetDecimalComma() : string.Empty;
            newRow["CreditAmount"] = diffAmount > 0 ? diffAmount.GetDecimalComma() : string.Empty;
            newRow["IsGroup"] = 88;
            dtTrial.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        }

        return dtTrial;
    }

    private DataTable ReturnTrialBalanceAccountGroupWise(DataTable dtTrial)
    {
        DataRow newRow;
        var dtReport = dtTrial.Clone();
        var dtGroup = dtTrial.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"],
            Col2 = r["GrpId"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();

        foreach (DataRow groupRow in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = groupRow["GrpId"];
            newRow["ShortName"] = groupRow["GrpCode"];
            newRow["LedgerName"] = groupRow["GrpName"];
            var grpDetails = dtTrial.Select($"GrpName='{groupRow["GrpName"]}'");
            if (grpDetails is { Length: > 0 })
            {
                newRow["DebitAmount"] =
                    grpDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["CreditAmount"] =
                    grpDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal()).GetDecimalComma();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
        }

        var dtTotal = dtTrial.Select("LedgerId <> 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerName"] = "GRAND TOTAL :- ";
        var totalDebit = dtTotal.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var totalCredit = dtTotal.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        newRow["DebitAmount"] = totalDebit.GetDecimalComma();
        newRow["CreditAmount"] = totalCredit.GetDecimalComma();
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        if (!totalDebit.Equals(totalCredit))
        {
            var diffAmount = totalDebit - totalCredit;
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = "DIFFERENCE IN TB :- ";
            newRow["DebitAmount"] = diffAmount < 0 ? Math.Abs(diffAmount).GetDecimalComma() : string.Empty;
            newRow["CreditAmount"] = diffAmount > 0 ? diffAmount.GetDecimalComma() : string.Empty;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnTrialBalanceAccountGroupWiseWithLedger(DataTable dtTrial)
    {
        DataRow newRow;
        var dtReport = dtTrial.Clone();
        var dtGroup = dtTrial.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();

        foreach (DataRow groupRow in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = groupRow["GrpId"];
            newRow["ShortName"] = groupRow["GrpCode"];
            newRow["LedgerName"] = groupRow["GrpName"];
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var grpDetails = dtTrial.Select($"GrpName='{groupRow["GrpName"]}'");
            if (grpDetails is { Length: > 0 })
            {
                grpDetails.AsEnumerable().Take(grpDetails.Length)
                    .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
                newRow = dtReport.NewRow();
                newRow["LedgerName"] = $"[{groupRow["GrpName"]}] GROUP TOTAL :- ";
                newRow["DebitAmount"] =
                    grpDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["CreditAmount"] =
                    grpDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal()).GetDecimalComma();
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
        }

        var dtTotal = dtTrial.Select("LedgerId <> 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerName"] = "GRAND TOTAL :- ";
        var totalDebit = dtTotal.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var totalCredit = dtTotal.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        newRow["DebitAmount"] = totalDebit.GetDecimalComma();
        newRow["CreditAmount"] = totalCredit.GetDecimalComma();
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        if (!totalDebit.Equals(totalCredit))
        {
            var diffAmount = totalDebit - totalCredit;
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = "DIFFERENCE IN TB :- ";
            newRow["DebitAmount"] = diffAmount < 0 ? Math.Abs(diffAmount).GetDecimalComma() : string.Empty;
            newRow["CreditAmount"] = diffAmount > 0 ? diffAmount.GetDecimalComma() : string.Empty;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        foreach (DataRow row in dtReport.Rows)
        {
            if (row["IsGroup"].GetInt() != 0)
            {
                continue;
            }

            row.SetField("LedgerName", "               " + row["LedgerName"]);
        }

        return dtReport;
    }

    private DataTable ReturnTrialBalanceAccountSubGroupWise(DataTable dtTrial)
    {
        DataRow newRow;
        var dtTotal = dtTrial.Select("LedgerId <> 0").CopyToDataTable();

        newRow = dtTrial.NewRow();

        newRow["LedgerName"] = "GRAND TOTAL :- ";
        var totalDebit = dtTotal.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var totalCredit = dtTotal.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        newRow["DebitAmount"] = totalDebit.GetDecimalComma();
        newRow["CreditAmount"] = totalCredit.GetDecimalComma();
        newRow["IsGroup"] = 99;
        dtTrial.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        if (!totalDebit.Equals(totalCredit))
        {
            var diffAmount = totalDebit - totalCredit;
            newRow = dtTrial.NewRow();
            newRow["LedgerName"] = "DIFFERENCE IN TB :- ";
            newRow["DebitAmount"] = diffAmount < 0 ? Math.Abs(diffAmount).GetDecimalComma() : string.Empty;
            newRow["CreditAmount"] = diffAmount > 0 ? diffAmount.GetDecimalComma() : string.Empty;
            newRow["IsGroup"] = 88;
            dtTrial.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        }

        return dtTrial;
    }

    private DataTable ReturnTrialBalanceAccountSubGroupWiseAccountGroupLedger(DataTable dtTrial)
    {
        DataRow newRow;
        var dtReport = dtTrial.Clone();
        var dtGroup = dtTrial.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();

        foreach (DataRow groupRow in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = groupRow["GrpId"];
            newRow["ShortName"] = groupRow["GrpCode"];
            newRow["LedgerName"] = groupRow["GrpName"];
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var grpDetails = dtTrial.Select($"GrpName='{groupRow["GrpName"]}'").CopyToDataTable();
            if (grpDetails.Rows.Count > 0)
            {
                var dtSubGroup = grpDetails.AsEnumerable().GroupBy(r => new
                {
                    Col1 = r["SubGrpName"]
                }).Select(g => g.OrderBy(r => r["SubGrpName"]).First()).CopyToDataTable();

                foreach (DataRow subRow in dtSubGroup.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["LedgerId"] = subRow["SubGrpId"];
                    newRow["ShortName"] = subRow["SubGrpCode"];
                    newRow["LedgerName"] = subRow["SubGrpName"];
                    newRow["IsGroup"] = 2;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var subGroupDetails = grpDetails.Select($"SubGrpName='{subRow["SubGrpName"]}'").CopyToDataTable();

                    subGroupDetails.AsEnumerable().Take(subGroupDetails.Rows.Count)
                        .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                    newRow = dtReport.NewRow();
                    newRow["LedgerName"] = $"[{subRow["SubGrpName"]}] SUB-GROUP TOTAL :- ";
                    newRow["DebitAmount"] = subGroupDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal())
                        .GetDecimalComma();
                    newRow["CreditAmount"] = subGroupDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal())
                        .GetDecimalComma();
                    newRow["IsGroup"] = 22;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["LedgerName"] = $"[{groupRow["GrpName"]}] GROUP TOTAL :- ";
                newRow["DebitAmount"] =
                    grpDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["CreditAmount"] =
                    grpDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal()).GetDecimalComma();
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
        }

        var dtTotal = dtTrial.Select("LedgerId <> 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerName"] = "GRAND TOTAL :- ";
        var totalDebit = dtTotal.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var totalCredit = dtTotal.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        newRow["DebitAmount"] = totalDebit.GetDecimalComma();
        newRow["CreditAmount"] = totalCredit.GetDecimalComma();
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        if (!totalDebit.Equals(totalCredit))
        {
            var diffAmount = totalDebit - totalCredit;
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = "DIFFERENCE IN TB :- ";
            newRow["DebitAmount"] = diffAmount < 0 ? Math.Abs(diffAmount).GetDecimalComma() : string.Empty;
            newRow["CreditAmount"] = diffAmount > 0 ? diffAmount.GetDecimalComma() : string.Empty;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtTrial.Rows.Count + 1);
        }

        foreach (DataRow row in dtReport.Rows)
        {
            if (row["IsGroup"].GetInt() == 0)
            {
                row.SetField("LedgerName", "               " + row["LedgerName"]);
            }

            if (row["IsGroup"].GetInt() == 2)
            {
                row.SetField("LedgerName", "          " + row["LedgerName"]);
            }
        }

        return dtReport;
    }

    public DataTable GetOpeningTrialBalanceReport()
    {
        var dtReport = new DataTable();
        try
        {
            var cmdString = new StringBuilder();
            cmdString.Append($@"
				WITH OpeningTrialBalance AS (SELECT gl.GLID LedgerId, gl.GLName LedgerName, gl.GLCode ShortName, ag.GrpId, ISNULL(ag.GrpName, 'NO-GROUP') GrpName,ag.GrpCode,ag.Schedule,  asg.SubGrpId, ISNULL(asg.SubGrpName, 'NO-SUB GROUP') SubGrpName,asg.SubGrpCode,
				CASE WHEN(ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0) )>0 THEN (ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0)) ELSE 0 END DebitAmount,
				CASE WHEN(ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0) )<0 THEN ABS((ISNULL(b.Balance, 0)+ ISNULL(s.OpeningBalance,0)+ISNULL(O.Opening, 0) - ISNULL(P.Profit,0) ))ELSE 0 END CreditAmount
								FROM AMS.GeneralLedger gl
									LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
									LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
									LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) Balance
													FROM AMS.AccountDetails ad1
													WHERE ad1.FiscalYearId= {ObjGlobal.SysFiscalYearId} AND ad1.Branch_ID={ObjGlobal.SysBranchId} AND ad1.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}'
													GROUP BY ad1.Ledger_ID) AS b ON b.Ledger_ID=gl.GLID
									LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) Opening
													FROM AMS.AccountDetails ad1
													WHERE ad1.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND ad1.Branch_ID={ObjGlobal.SysBranchId} AND ad1.Ledger_ID IN(SELECT GLID FROM AMS.GeneralLedger WHERE GrpId IN(SELECT GrpId FROM AMS.AccountGroup WHERE PrimaryGrp IN ('Balance Sheet', 'BS')))
													GROUP BY ad1.Ledger_ID) AS O ON o.Ledger_ID=gl.GLID
									LEFT OUTER JOIN (SELECT ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId}) LedgerId, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) OpeningBalance
													 FROM AMS.StockDetails sd
															LEFT OUTER JOIN AMS.Product p ON p.PID=sd.Product_Id
													 WHERE sd.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}'
													 GROUP BY ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId})
                                                     HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) > 0 ) AS s ON s.LedgerId = gl.GLID
									LEFT OUTER JOIN (SELECT {ObjGlobal.FinanceProfitLossLedgerId} LedgerId, (SELECT SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) OpeningBalance
																		FROM AMS.StockDetails sd
																			LEFT OUTER JOIN AMS.Product p ON p.PID=sd.Product_Id
																		WHERE sd.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}')-SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt) Profit
													FROM AMS.AccountDetails ad
														LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
														LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
													WHERE ag.PrimaryGrp IN ('PL', 'Profit & Loss') AND ad.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}') AS p ON p.LedgerId = gl.GLID
								WHERE ABS((ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0) ))>0)
				SELECT ot.LedgerId, ot.LedgerName, ot.ShortName, ot.GrpId, ot.GrpName,ot.GrpCode,ot.Schedule, ot.SubGrpId, ot.SubGrpName,ot.SubGrpCode, CASE WHEN DebitAmount > 0 THEN  FORMAT(ot.DebitAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END DebitAmount,CASE WHEN ot.CreditAmount > 0 THEN  FORMAT(ot.CreditAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END CreditAmount, 0 IsGroup
				FROM OpeningTrialBalance ot ");
            cmdString.Append(GetReports.SortOn switch
            {
                "SHORTNAME" => "\n ORDER BY ot.ShortName,ot.LedgerName,ot.GrpName,ot.SubGrpName; ",
                "SCHEDULE" => "\n ORDER BY ot.Schedule,ot.GrpName,ot.LedgerName,ot.SubGrpName; ",
                _ => "\n ORDER BY ot.LedgerName,ot.GrpName,ot.SubGrpName;  "
            });
            var dtTrial = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            dtReport = GetReports.FilterFor switch
            {
                "Ledger" => ReturnTrialBalanceLedgerWise(dtTrial),
                "Account Group" => ReturnTrialBalanceAccountGroupWise(dtTrial),
                "Account Group/Ledger" => ReturnTrialBalanceAccountGroupWiseWithLedger(dtTrial),
                "Account Group/Sub Group" => ReturnTrialBalanceAccountSubGroupWise(dtTrial),
                "Account Group/Sub Group/Ledger" => ReturnTrialBalanceAccountSubGroupWiseAccountGroupLedger(dtTrial),
                _ => dtReport
            };

            return dtReport;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            var msg = e.Message;
            return dtReport;
        }
    }

    public DataTable GetNormalTrialBalanceReport()
    {
        var dtReport = new DataTable();
        try
        {
            var cmdString = new StringBuilder();
            cmdString.Append($@"
				WITH TrialBalance AS (SELECT gl.GLID LedgerId, gl.GLName LedgerName, gl.GLCode ShortName, ag.GrpId, ISNULL(ag.GrpName, 'NO-GROUP') GrpName,ag.GrpCode,ag.Schedule,  asg.SubGrpId, ISNULL(asg.SubGrpName, 'NO-SUB GROUP') SubGrpName,asg.SubGrpCode,
				CASE WHEN(ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0) )>0 THEN (ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0)) ELSE 0 END DebitAmount,
				CASE WHEN(ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0) )<0 THEN ABS((ISNULL(b.Balance, 0)+ ISNULL(s.OpeningBalance,0)+ISNULL(O.Opening, 0) - ISNULL(P.Profit,0) ))ELSE 0 END CreditAmount
								FROM AMS.GeneralLedger gl
									LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
									LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
									LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) Balance
													FROM AMS.AccountDetails ad1
													WHERE ad1.FiscalYearId= {ObjGlobal.SysFiscalYearId} AND ad1.Branch_ID={ObjGlobal.SysBranchId} AND ad1.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}'
													GROUP BY ad1.Ledger_ID) AS b ON b.Ledger_ID=gl.GLID
									LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) Opening
													FROM AMS.AccountDetails ad1
													WHERE ad1.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND ad1.Branch_ID={ObjGlobal.SysBranchId} AND ad1.Ledger_ID IN(SELECT GLID FROM AMS.GeneralLedger WHERE GrpId IN(SELECT GrpId FROM AMS.AccountGroup WHERE PrimaryGrp IN ('Balance Sheet', 'BS')))
													GROUP BY ad1.Ledger_ID) AS O ON o.Ledger_ID=gl.GLID
									LEFT OUTER JOIN (SELECT ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId}) LedgerId, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) OpeningBalance
													 FROM AMS.StockDetails sd
															LEFT OUTER JOIN AMS.Product p ON p.PID=sd.Product_Id
													 WHERE sd.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}'
													 GROUP BY ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId})
                                                     HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) > 0 ) AS s ON s.LedgerId = gl.GLID
									LEFT OUTER JOIN (SELECT {ObjGlobal.FinanceProfitLossLedgerId} LedgerId, (SELECT SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) OpeningBalance
																		FROM AMS.StockDetails sd
																			LEFT OUTER JOIN AMS.Product p ON p.PID=sd.Product_Id
																		WHERE sd.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}' )-SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt) Profit
													FROM AMS.AccountDetails ad
														LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
														LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
													WHERE ag.PrimaryGrp IN ('PL', 'Profit & Loss') AND ad.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}') AS p ON p.LedgerId = gl.GLID
								WHERE ABS((ISNULL(b.Balance, 0)+ISNULL(O.Opening, 0) + ISNULL(s.OpeningBalance,0) - ISNULL(P.Profit,0) ))>0 ");
            if (GetReports.IsCombineCustomerVendor)
            {
                cmdString.Append($@"
								AND gl.GLType NOT IN ('Customer','Vendor','Both')

								UNION ALL
								SELECT	ag.GrpId LedgerId, ISNULL(ag.GrpName, 'NO-GROUP') LedgerName, ag.GrpCode ShortName, ag.GrpId, ISNULL(ag.GrpName, 'NO-GROUP') GrpName, ag.GrpCode, ag.Schedule, asg.SubGrpId, ISNULL(asg.SubGrpName, 'NO-SUB GROUP') SubGrpName, asg.SubGrpCode, SUM( CASE WHEN ISNULL(B.Balance,0) > 0 THEN ISNULL(B.Balance,0) ELSE 0 END	) DebitAmount,SUM( CASE WHEN ISNULL(B.Balance,0) < 0 THEN ISNULL(B.Balance,0) ELSE 0 END	) CreditAmount
								FROM AMS.GeneralLedger gl
									LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
									LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
									LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) Balance
													FROM AMS.AccountDetails ad1
													WHERE ad1.FiscalYearId={ObjGlobal.SysFiscalYearId} AND ad1.Branch_ID={ObjGlobal.SysBranchId} AND ad1.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}'
													GROUP BY ad1.Ledger_ID) AS b ON b.Ledger_ID=gl.GLID
								WHERE gl.GLType IN ('Customer','Vendor','Both')
								GROUP BY ISNULL(ag.GrpName, 'NO-GROUP'), ISNULL(asg.SubGrpName, 'NO-SUB GROUP'),ag.GrpId, ag.GrpCode, ag.Schedule, asg.SubGrpId, asg.SubGrpCode
								HAVING SUM(ISNULL(b.Balance,0)) <> 0 ");
            }

            cmdString.Append($@"
				)
				SELECT ot.LedgerId, ot.LedgerName, ot.ShortName, ot.GrpId, ot.GrpName,ot.GrpCode,ot.Schedule, ot.SubGrpId, ot.SubGrpName,ot.SubGrpCode, CASE WHEN DebitAmount > 0 THEN  FORMAT(ot.DebitAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END DebitAmount,CASE WHEN ot.CreditAmount > 0 THEN  FORMAT(ot.CreditAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END CreditAmount, 0 IsGroup
				FROM TrialBalance ot ");
            cmdString.Append(GetReports.SortOn switch
            {
                "SHORTNAME" => " ORDER BY ot.ShortName,ot.LedgerName,ot.GrpName,ot.SubGrpName; ",
                "SCHEDULE" => " ORDER BY ot.Schedule,ot.GrpName,ot.LedgerName,ot.SubGrpName; ",
                _ => " ORDER BY ot.LedgerName,ot.GrpName,ot.SubGrpName;  "
            });
            var dtTrial = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            if (dtTrial.Rows.Count is 0)
            {
                return new DataTable();
            }

            dtReport = GetReports.FilterFor switch
            {
                "Ledger" => ReturnTrialBalanceLedgerWise(dtTrial),
                "Account Group" => ReturnTrialBalanceAccountGroupWise(dtTrial),
                "Account Group/Ledger" => ReturnTrialBalanceAccountGroupWiseWithLedger(dtTrial),
                "Account Group/Sub Group" => ReturnTrialBalanceAccountSubGroupWise(dtTrial),
                "Account Group/Sub Group/Ledger" => ReturnTrialBalanceAccountSubGroupWiseAccountGroupLedger(dtTrial),
                _ => dtReport
            };
            return dtReport;
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
            var msg = e.Message;
            return dtReport;
        }
    }

    #endregion **----- NORMAL TRIAL BALANCE -----**

    //PERIODIC TRIAL BALANCE

    #region **----- PERODIC REPORT -----**

    private DataTable GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dt_LedgerId",
            "dt_ShortName",
            "dt_Ledger",
            "dt_OpeningDebit",
            "dt_OpeningLocalDebit",
            "dt_OpeningCredit",
            "dt_OpeningLocalCredit",
            "dt_PeriodicDebit",
            "dt_PeriodicLocalDebit",
            "dt_PeriodicCredit",
            "dt_PeriodicLocalCredit",
            "dt_Balance",
            "dt_LocalBalance",
            "dt_BalanceType",
            "dt_ClosingDebit",
            "dt_ClosingLocalDebit",
            "dt_ClosingCredit",
            "dt_ClosingLocalCredit",
            "dt_PanNo",
            "dt_PhoneNo"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnPeriodicTrialBalanceAccountGroupReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        var dtGroup = dtFinance.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();
        foreach (DataRow roGroup in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
            newRow["dt_ShortName"] = roGroup["GrpCode"].ToString();
            newRow["dt_Ledger"] = roGroup["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtFinance.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();
            foreach (DataRow item in dtGroupDetails.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Ledger"] = item["GLName"].ToString();

                double.TryParse(item["OpeningDebit"].ToString(), out var openingDebit);
                double.TryParse(item["OpeningCredit"].ToString(), out var openingCredit);
                double.TryParse(item["PrDebitAmt"].ToString(), out var prDebit);
                double.TryParse(item["PrCreditAmt"].ToString(), out var prCredit);
                var balance = prDebit - prCredit;

                newRow["dt_OpeningDebit"] = openingDebit;
                newRow["dt_OpeningCredit"] = openingCredit;
                newRow["dt_PeriodicDebit"] = prDebit;
                newRow["dt_PeriodicCredit"] = prCredit;
                newRow["dt_Balance"] = Math.Abs(balance);
                newRow["dt_BalanceType"] = balance > 0 ? "Dr" : balance < 0 ? "Cr" : string.Empty;

                balance = balance + openingDebit - openingCredit;
                newRow["dt_ClosingDebit"] = balance > 0 ? balance : string.Empty;
                newRow["dt_ClosingCredit"] = balance < 0 ? Math.Abs(balance) : string.Empty;
                newRow["IsGroup"] = "0";
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = $"[{roGroup["GrpName"]}] GROUP TOTAL => ";

            var sumOpeningDebit = dtGroupDetails.AsEnumerable().Sum(x => x["OpeningDebit"].GetDecimal());
            var sumOpeningCredit = dtGroupDetails.AsEnumerable().Sum(x => x["OpeningCredit"].GetDecimal());

            var sumDebit = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
            var sumCredit = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());

            newRow["dt_OpeningDebit"] = sumOpeningDebit;
            newRow["dt_OpeningCredit"] = sumOpeningCredit;
            newRow["dt_PeriodicDebit"] = sumDebit;
            newRow["dt_PeriodicCredit"] = sumCredit;
            var sumbalance = sumOpeningDebit + sumDebit - sumCredit - sumOpeningCredit;
            newRow["dt_ClosingDebit"] = sumbalance > 0 ? sumbalance : string.Empty;
            newRow["dt_ClosingCredit"] = sumbalance < 0 ? Math.Abs(sumbalance) : string.Empty;
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());

        newRow["dt_PeriodicDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        newRow["dt_PeriodicCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());

        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());

        newRow["IsGroup"] = "99";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnPeriodicTrialBalanceAccountGroupWithSubGroupReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        var dtGroup = dtFinance.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();

        foreach (DataRow roGroup in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
            newRow["dt_Desc"] = roGroup["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtLedger = dtFinance.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();
            foreach (DataRow item in dtLedger.Rows)
            {
                newRow = dtReport.NewRow();

                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = item["GLName"].ToString();

                newRow["dt_OpeningDebit"] = item["OpeningDebit"].GetDecimal();
                newRow["dt_OpeningLocalDebit"] = item["OpeningDebit"].GetDecimal();

                newRow["dt_OpeningCredit"] = item["OpeningCredit"].GetDecimal();
                newRow["dt_OpeningLocalCredit"] = item["OpeningCredit"].GetDecimal();

                newRow["dt_ClosingDebit"] = item["PrDebitAmt"].GetDecimal();
                newRow["dt_ClosingLocalDebit"] = item["PrDebitAmt"].GetDecimal();

                newRow["dt_ClosingCredit"] = item["PrCreditAmt"].GetDecimal();
                newRow["dt_ClosingLocalCredit"] = item["PrCreditAmt"].GetDecimal();

                newRow["dt_GroupBy"] = roGroup["GrpName"];
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtLedgerTotal = dtReport.Select($"dt_GroupBy =  '{roGroup["GrpName"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roGroup["GrpName"]} TOTAL :- ";
            newRow["dt_OpeningDebit"] = dtLedgerTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            newRow["dt_OpeningCredit"] = dtLedgerTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            newRow["dt_ClosingDebit"] = dtLedgerTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
            newRow["dt_ClosingCredit"] = dtLedgerTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
            newRow["IsGroup"] = "11";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        newRow["IsGroup"] = "99";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnPeriodicTrialBalanceAccountGroupLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        var dtGroup = dtFinance.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();
        foreach (DataRow roGroup in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
            newRow["dt_ShortName"] = roGroup["GrpCode"].ToString();
            newRow["dt_Ledger"] = roGroup["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtFinance.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();
            foreach (DataRow item in dtGroupDetails.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Ledger"] = "          " + item["GLName"];

                double.TryParse(item["OpeningDebit"].ToString(), out var openingDebitAmount);
                double.TryParse(item["OpeningCredit"].ToString(), out var openingCreditAmount);
                double.TryParse(item["PrDebitAmt"].ToString(), out var prDebit);
                double.TryParse(item["PrCreditAmt"].ToString(), out var prCredit);
                var balance = prDebit - prCredit;

                newRow["dt_OpeningDebit"] = openingDebitAmount;
                newRow["dt_OpeningCredit"] = openingCreditAmount;
                newRow["dt_PeriodicDebit"] = prDebit;
                newRow["dt_PeriodicCredit"] = prCredit;
                newRow["dt_Balance"] = Math.Abs(balance);
                newRow["dt_BalanceType"] = balance > 0 ? "Dr" : balance < 0 ? "Cr" : string.Empty;

                balance = balance + openingDebitAmount - openingCreditAmount;
                newRow["dt_ClosingDebit"] = balance > 0 ? balance : string.Empty;
                newRow["dt_ClosingCredit"] = balance < 0 ? Math.Abs(balance) : string.Empty;
                newRow["IsGroup"] = "0";
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = $"[{roGroup["GrpName"]}] GROUP TOTAL => ";
            var sumOpeningDebit = dtGroupDetails.AsEnumerable().Sum(x => x["OpeningDebit"].GetDecimal());
            var sumOpeningCredit = dtGroupDetails.AsEnumerable().Sum(x => x["OpeningCredit"].GetDecimal());

            var sumDebit = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
            var sumCredit = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());

            newRow["dt_OpeningDebit"] = sumOpeningDebit;
            newRow["dt_OpeningCredit"] = sumOpeningCredit;
            newRow["dt_PeriodicDebit"] = sumDebit;
            newRow["dt_PeriodicCredit"] = sumCredit;
            var sumbalance = sumOpeningDebit + sumDebit - sumCredit - sumOpeningCredit;
            newRow["dt_ClosingDebit"] = sumbalance > 0 ? sumbalance : string.Empty;
            newRow["dt_ClosingCredit"] = sumbalance < 0 ? Math.Abs(sumbalance) : string.Empty;
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();

        var openingDebit = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        var openingCredit = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());
        var debitAmount = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        var creditAmount = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
        var closingDebit = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        var closingCredit = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());

        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = openingDebit;
        newRow["dt_OpeningCredit"] = openingCredit;

        newRow["dt_PeriodicDebit"] = debitAmount;
        newRow["dt_PeriodicCredit"] = creditAmount;

        newRow["dt_ClosingDebit"] = closingDebit;
        newRow["dt_ClosingCredit"] = closingCredit;

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        var result = openingDebit + debitAmount + closingDebit - openingCredit - closingCredit - creditAmount;
        if (result != 0)
        {
            var openingDifferenc = openingDebit - openingCredit;
            var peroidicDifferenc = debitAmount - creditAmount;
            var closingDifferenc = closingDebit - closingCredit;

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = "DIFFERENCE IN TB :- ";
            newRow["dt_OpeningDebit"] = openingDifferenc < 0 ? Math.Abs(openingDifferenc) : string.Empty;
            newRow["dt_OpeningCredit"] = openingDifferenc > 0 ? openingDifferenc : string.Empty;

            newRow["dt_PeriodicDebit"] = peroidicDifferenc < 0 ? Math.Abs(peroidicDifferenc) : string.Empty;
            newRow["dt_PeriodicCredit"] = peroidicDifferenc > 0 ? peroidicDifferenc : string.Empty;

            newRow["dt_ClosingDebit"] = closingDifferenc < 0 ? Math.Abs(closingDifferenc) : string.Empty;
            newRow["dt_ClosingCredit"] = closingDifferenc > 0 ? closingDifferenc : string.Empty;

            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicTrialBalanceAccountSubGroupLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        var dtGroup = dtFinance.AsEnumerable().GroupBy(r => new
        {
            Col1 = r["GrpName"]
        }).Select(g => g.OrderBy(r => r["GrpName"]).First()).CopyToDataTable();
        foreach (DataRow roGroup in dtGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
            newRow["dt_ShortName"] = roGroup["GrpCode"].ToString();
            newRow["dt_Ledger"] = roGroup["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtFinance.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();

            var dtSubGroup = dtGroupDetails.AsEnumerable().GroupBy(r => new
            {
                Col1 = r["SubGrpCode"]
            }).Select(g => g.OrderBy(r => r["SubGrpCode"]).First()).CopyToDataTable();
            foreach (DataRow roSubGroup in dtSubGroup.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roSubGroup["SubGrpId"].ToString();
                newRow["dt_ShortName"] = roSubGroup["SubGrpCode"].ToString();
                newRow["dt_Ledger"] = "     " + roSubGroup["SubGrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var dtSubGroupDetails = dtGroupDetails.Select($"SubGrpName = '{roGroup["SubGrpName"]}'")
                    .CopyToDataTable();
                foreach (DataRow item in dtSubGroupDetails.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                    newRow["dt_ShortName"] = item["ShortName"].ToString();
                    newRow["dt_Ledger"] = "          " + item["GLName"];

                    double.TryParse(item["OpeningDebit"].ToString(), out var openingDebitAmount);
                    double.TryParse(item["OpeningCredit"].ToString(), out var openingCreditAmount);
                    double.TryParse(item["PrDebitAmt"].ToString(), out var prDebit);
                    double.TryParse(item["PrCreditAmt"].ToString(), out var prCredit);
                    var balance = prDebit - prCredit;

                    newRow["dt_OpeningDebit"] = openingDebitAmount;
                    newRow["dt_OpeningCredit"] = openingCreditAmount;
                    newRow["dt_PeriodicDebit"] = prDebit;
                    newRow["dt_PeriodicCredit"] = prCredit;
                    newRow["dt_Balance"] = Math.Abs(balance);
                    newRow["dt_BalanceType"] = balance > 0 ? "Dr" : balance < 0 ? "Cr" : string.Empty;

                    balance = balance + openingDebitAmount - openingCreditAmount;
                    newRow["dt_ClosingDebit"] = balance > 0 ? balance : string.Empty;
                    newRow["dt_ClosingCredit"] = balance < 0 ? Math.Abs(balance) : string.Empty;
                    newRow["IsGroup"] = "0";
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Ledger"] = $"[{roSubGroup["SubGrpName"]}] SUBGROUP TOTAL => ";
                var sumSubOpeningDebit = dtSubGroupDetails.AsEnumerable().Sum(x => x["OpeningDebit"].GetDecimal());
                var sumSubOpeningCredit =
                    dtSubGroupDetails.AsEnumerable().Sum(x => x["OpeningCredit"].GetDecimal());

                var sumSubDebit = dtSubGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                var sumSubCredit = dtSubGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());

                newRow["dt_OpeningDebit"] = sumSubOpeningDebit;
                newRow["dt_OpeningCredit"] = sumSubOpeningCredit;
                newRow["dt_PeriodicDebit"] = sumSubDebit;
                newRow["dt_PeriodicCredit"] = sumSubCredit;

                var sumSubbalance = sumSubOpeningDebit + sumSubDebit - sumSubOpeningCredit - sumSubCredit;
                newRow["dt_ClosingDebit"] = sumSubbalance > 0 ? sumSubbalance : string.Empty;
                newRow["dt_ClosingCredit"] = sumSubbalance < 0 ? Math.Abs(sumSubbalance) : string.Empty;
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = $"[{roGroup["GrpName"]}] GROUP TOTAL => ";
            var sumOpeningDebit = dtGroupDetails.AsEnumerable().Sum(x => x["OpeningDebit"].GetDecimal());
            var sumOpeningCredit = dtGroupDetails.AsEnumerable().Sum(x => x["OpeningCredit"].GetDecimal());

            var sumDebit = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
            var sumCredit = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());

            newRow["dt_OpeningDebit"] = sumOpeningDebit;
            newRow["dt_OpeningCredit"] = sumOpeningCredit;
            newRow["dt_PeriodicDebit"] = sumDebit;
            newRow["dt_PeriodicCredit"] = sumCredit;
            var sumbalance = sumOpeningDebit + sumDebit - sumCredit - sumOpeningCredit;
            newRow["dt_ClosingDebit"] = sumbalance > 0 ? sumbalance : string.Empty;
            newRow["dt_ClosingCredit"] = sumbalance < 0 ? Math.Abs(sumbalance) : string.Empty;
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();

        var openingDebit = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        var openingCredit = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());
        var debitAmount = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        var creditAmount = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
        var closingDebit = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        var closingCredit = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());

        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = openingDebit;
        newRow["dt_OpeningCredit"] = openingCredit;

        newRow["dt_PeriodicDebit"] = debitAmount;
        newRow["dt_PeriodicCredit"] = creditAmount;

        newRow["dt_ClosingDebit"] = closingDebit;
        newRow["dt_ClosingCredit"] = closingCredit;

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        var result = openingDebit + debitAmount + closingDebit - openingCredit - closingCredit - creditAmount;
        if (result != 0)
        {
            var openingDifferenc = openingDebit - openingCredit;
            var peroidicDifferenc = debitAmount - creditAmount;
            var closingDifferenc = closingDebit - closingCredit;

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = "DIFFERENCE IN TB :- ";
            newRow["dt_OpeningDebit"] = openingDifferenc < 0 ? Math.Abs(openingDifferenc) : string.Empty;
            newRow["dt_OpeningCredit"] = openingDifferenc > 0 ? openingDifferenc : string.Empty;

            newRow["dt_PeriodicDebit"] = peroidicDifferenc < 0 ? Math.Abs(peroidicDifferenc) : string.Empty;
            newRow["dt_PeriodicCredit"] = peroidicDifferenc > 0 ? peroidicDifferenc : string.Empty;

            newRow["dt_ClosingDebit"] = closingDifferenc < 0 ? Math.Abs(closingDifferenc) : string.Empty;
            newRow["dt_ClosingCredit"] = closingDifferenc > 0 ? closingDifferenc : string.Empty;

            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicTrialBalanceLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        foreach (DataRow item in dtFinance.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
            newRow["dt_ShortName"] = item["ShortName"].ToString();
            newRow["dt_Ledger"] = item["GLName"].ToString();

            double.TryParse(item["OpeningDebit"].ToString(), out var openingDebitAmount);
            double.TryParse(item["OpeningCredit"].ToString(), out var openingCreditAmount);
            double.TryParse(item["PrDebitAmt"].ToString(), out var prDebit);
            double.TryParse(item["PrCreditAmt"].ToString(), out var prCredit);
            var balance = prDebit - prCredit;

            newRow["dt_OpeningDebit"] = openingDebitAmount;
            newRow["dt_OpeningCredit"] = openingCreditAmount;
            newRow["dt_PeriodicDebit"] = prDebit;
            newRow["dt_PeriodicCredit"] = prCredit;
            newRow["dt_Balance"] = Math.Abs(balance);
            newRow["dt_BalanceType"] = balance > 0 ? "Dr" : balance < 0 ? "Cr" : string.Empty;

            balance = balance + openingDebitAmount - openingCreditAmount;
            newRow["dt_ClosingDebit"] = balance > 0 ? balance : string.Empty;
            newRow["dt_ClosingCredit"] = balance < 0 ? Math.Abs(balance) : string.Empty;
            newRow["IsGroup"] = "0";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();

        var openingDebit = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        var openingCredit = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());
        var debitAmount = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        var creditAmount = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
        var closingDebit = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        var closingCredit = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());

        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = openingDebit;
        newRow["dt_OpeningCredit"] = openingCredit;

        newRow["dt_PeriodicDebit"] = debitAmount;
        newRow["dt_PeriodicCredit"] = creditAmount;

        newRow["dt_ClosingDebit"] = closingDebit;
        newRow["dt_ClosingCredit"] = closingCredit;

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        var result = openingDebit + debitAmount + closingDebit - openingCredit - closingCredit - creditAmount;
        if (result != 0)
        {
            var openingDifferenc = openingDebit - openingCredit;
            var peroidicDifferenc = debitAmount - creditAmount;
            var closingDifferenc = closingDebit - closingCredit;

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = "DIFFERENCE IN TB :- ";
            newRow["dt_OpeningDebit"] = openingDifferenc < 0 ? Math.Abs(openingDifferenc) : string.Empty;
            newRow["dt_OpeningCredit"] = openingDifferenc > 0 ? openingDifferenc : string.Empty;

            newRow["dt_PeriodicDebit"] = peroidicDifferenc < 0 ? Math.Abs(peroidicDifferenc) : string.Empty;
            newRow["dt_PeriodicCredit"] = peroidicDifferenc > 0 ? peroidicDifferenc : string.Empty;

            newRow["dt_ClosingDebit"] = closingDifferenc < 0 ? Math.Abs(closingDifferenc) : string.Empty;
            newRow["dt_ClosingCredit"] = closingDifferenc > 0 ? closingDifferenc : string.Empty;

            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    public DataTable GetPeriodicTrialBalanceReport()
    {
        var dtReport = new DataTable();
        var cmdString = string.Empty;
        if (GetReports.IsSubLedger)
        {
            cmdString = @$"
				WITH FinanceReport
				  AS
				  (
					SELECT Ledger_ID, tb.Subleder_ID, CASE WHEN SUM ( OpeningDebit - OpeningCredit ) > 0 THEN SUM ( OpeningDebit - OpeningCredit ) ELSE 0 END OpeningDebit, CASE WHEN SUM ( OpeningDebit - OpeningCredit ) < 0 THEN ABS ( SUM ( OpeningDebit - OpeningCredit )) ELSE 0 END OpeningCredit, SUM ( PrDebitAmt ) PrDebitAmt, SUM ( PrCreditAmt ) PrCreditAmt
					 FROM (
							SELECT ad1.Ledger_ID, ad1.Subleder_ID, ISNULL ( CASE WHEN ag.PrimaryGrp = 'Balance Sheet' THEN SUM ( LocalDebit_Amt ) END, 0 ) OpeningDebit, ISNULL ( CASE WHEN ag.PrimaryGrp = 'Balance Sheet' THEN SUM ( LocalCredit_Amt ) END, 0 ) OpeningCredit, 0 PrDebitAmt, 0 PrCreditAmt
							 FROM AMS.AccountDetails ad1
							 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ad1.Ledger_ID
							 LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
							 WHERE ad1.FiscalYearId < {ObjGlobal.SysFiscalYearId} AND ad.Branch_ID IN ({ObjGlobal.SysBranchId})
							 GROUP BY ad1.Ledger_ID, ag.PrimaryGrp, ad1.Subleder_ID
							UNION ALL
							SELECT ad.Ledger_ID, ad.Subleder_ID, ISNULL ( CASE WHEN ad.Voucher_Date < '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' THEN SUM ( LocalDebit_Amt ) END, 0 ) OpeningDebit, ISNULL ( CASE WHEN ad.Voucher_Date < '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' THEN SUM ( LocalCredit_Amt ) END, 0 ) OpeningCredit, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' and '{DateTime.Parse(GetReports.ToDate):yyyy-MM-dd}' THEN SUM ( LocalDebit_Amt ) END, 0 ) PrDebitAmt, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' and '{DateTime.Parse(GetReports.ToDate):yyyy-MM-dd}' THEN SUM ( LocalCredit_Amt ) END, 0 ) PrCreditAmt
							 FROM AMS.AccountDetails ad
							 WHERE ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId})
							 GROUP BY ad.Ledger_ID, ad.Voucher_Date, ad.Subleder_ID
						  ) AS tb
					 GROUP BY Ledger_ID, Subleder_ID
				  )
				 SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName, ISNULL ( fr.Subleder_ID, 0 ) Subleder_ID, ISNULL ( sl.SLName, 'NO-SUBLEDGER' ) SLName, ISNULL ( gl.GrpId, 0 ) GrpId, ISNULL ( ag.GrpName, 'NO- GROUP' ) GrpName, ISNULL ( gl.SubGrpId, 0 ) SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUBGROUP' ) SubGrpName, CASE WHEN SUM ( fr.OpeningDebit - fr.OpeningCredit ) > 0 THEN SUM ( fr.OpeningDebit - fr.OpeningCredit ) ELSE 0 END OpeningDebit, CASE WHEN SUM ( fr.OpeningCredit - fr.OpeningDebit ) > 0 THEN SUM ( fr.OpeningCredit - fr.OpeningDebit ) ELSE 0 END OpeningCredit, CASE WHEN SUM ( fr.PrDebitAmt - fr.PrCreditAmt ) > 0 THEN SUM ( fr.PrDebitAmt - fr.PrCreditAmt ) ELSE 0 END PrDebitAmt, CASE WHEN SUM ( fr.PrCreditAmt - fr.PrDebitAmt ) > 0 THEN SUM ( fr.PrCreditAmt - fr.PrDebitAmt ) ELSE 0 END PrCreditAmt
				  FROM FinanceReport fr
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
				  LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
				  LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
				  LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId = fr.Subleder_ID
				  GROUP BY fr.Ledger_ID, fr.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, gl.SubGrpId, asg.SubGrpName, fr.Subleder_ID, sl.SLName ";
            cmdString += !GetReports.IsZeroBalance
                ? " HAVING ABS(SUM (fr.OpeningDebit + fr.PrDebitAmt - fr.PrCreditAmt - fr.OpeningCredit)) > 0 "
                : " HAVING (ABS(SUM(fr.OpeningDebit - fr.OpeningCredit)) + ABS(SUM(fr.PrDebitAmt - fr.PrCreditAmt))) > 0 ";
            cmdString += " ORDER BY GLName ; ";
        }
        else
        {
            cmdString = @$"
				WITH FinanceReport AS
				(
					SELECT Ledger_ID,CASE WHEN SUM(OpeningDebit - OpeningCredit) > 0 THEN SUM(OpeningDebit - OpeningCredit)  ELSE 0 END OpeningDebit,CASE WHEN SUM(OpeningDebit - OpeningCredit) < 0 THEN ABS(SUM(OpeningDebit - OpeningCredit))  ELSE 0 END OpeningCredit,
					SUM(PrDebitAmt) PrDebitAmt, SUM(PrCreditAmt) PrCreditAmt
					FROM
					(
						SELECT ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId}) Ledger_ID, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) OpeningDebit, 0 OpeningCredit,0 PrDebitAmt, 0 PrCreditAmt
						FROM AMS.StockDetails sd
							 INNER JOIN AMS.Product p ON p.PID=sd.Product_Id
						WHERE Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' AND sd.Branch_Id={ObjGlobal.SysBranchId} 
                        GROUP BY ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId})
						HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END)>0
						UNION ALL
						SELECT ad1.Ledger_ID,IsNull(Case When ag.PrimaryGrp IN ( 'Balance Sheet', 'BS') Then SUM(LocalDebit_Amt) End, 0) OpeningDebit,ISNULL (Case When ag.PrimaryGrp IN( 'Balance Sheet', 'BS') Then SUM(LocalCredit_Amt) End, 0) OpeningCredit,0 PrDebitAmt, 0 PrCreditAmt FROM AMS.AccountDetails ad1
							LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID= ad1.Ledger_ID
							LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						WHERE ad1.FiscalYearId < {ObjGlobal.SysFiscalYearId} ";
            cmdString += !GetReports.IsIncludePdc ? @" AND ad1.Module NOT IN ('PDC','PROV') " : "";
            cmdString += @$"
						GROUP BY ad1.Ledger_ID,ag.PrimaryGrp
						UNION ALL
						SELECT ad.Ledger_ID,IsNull(Case When ad.Voucher_Date < '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' Then SUM(LocalDebit_Amt) End, 0) OpeningDebit,IsNull(Case When ad.Voucher_Date < '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' Then SUM(LocalCredit_Amt) End, 0) OpeningCredit,IsNull(Case When ad.Voucher_Date Between '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' and '{DateTime.Parse(GetReports.ToDate):yyyy-MM-dd}' Then SUM(LocalDebit_Amt) End, 0) PrDebitAmt,IsNull(Case When ad.Voucher_Date  Between '{DateTime.Parse(GetReports.FromDate):yyyy-MM-dd}' and '{DateTime.Parse(GetReports.ToDate):yyyy-MM-dd}' Then SUM(LocalCredit_Amt) End, 0) PrCreditAmt
						FROM AMS.AccountDetails ad
						WHERE ad.FiscalYearId in ({ObjGlobal.SysFiscalYearId}) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
            cmdString += !GetReports.IsIncludePdc ? @" AND ad.Module NOT IN ('PDC','PROV') " : "";
            cmdString += @$"
                 GROUP BY ad.Ledger_ID,ad.Voucher_Date
					) ProfitLoss
					GROUP BY Ledger_ID
				  )
					SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName, ISNULL ( gl.GrpId, 0 ) GrpId, ISNULL ( ag.GrpName, 'NO- GROUP' ) GrpName,ag.GrpCode GrpCode, ISNULL ( gl.SubGrpId, 0 ) SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUBGROUP' ) SubGrpName,asg.SubGrpCode SubGrpCode, CASE WHEN SUM ( fr.OpeningDebit - fr.OpeningCredit ) > 0 THEN SUM ( fr.OpeningDebit - fr.OpeningCredit ) ELSE 0 END OpeningDebit, CASE WHEN SUM ( fr.OpeningCredit - fr.OpeningDebit ) > 0 THEN SUM ( fr.OpeningCredit - fr.OpeningDebit ) ELSE 0 END OpeningCredit, SUM ( fr.PrDebitAmt ) PrDebitAmt, SUM ( fr.PrCreditAmt ) PrCreditAmt
					  FROM FinanceReport fr
						  LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
						  LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						  LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
					  GROUP BY fr.Ledger_ID, fr.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, gl.SubGrpId, asg.SubGrpName,ag.GrpCode,asg.SubGrpCode ,ag.Schedule ";
            cmdString += GetReports.IsZeroBalance switch
            {
                false => @"
					  HAVING ABS(SUM (fr.OpeningDebit + fr.PrDebitAmt + fr.PrCreditAmt + fr.OpeningCredit)) > 0 ",
                _ => @"
					  HAVING (ABS(SUM(fr.OpeningDebit - fr.OpeningCredit)) + ABS(SUM(fr.PrDebitAmt - fr.PrCreditAmt))) > 0 "
            };
            cmdString += GetReports.SortOn switch
            {
                "SCHEDULE" => @"
					  ORDER BY ag.Schedule,gl.GLName",
                _ => @"
					  ORDER BY GLName"
            };
        }

        var dtFinance = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        dtReport = GetReports.FilterFor switch
        {
            "Ledger" => ReturnPeriodicTrialBalanceLedgerReport(dtFinance),
            "Ledger/SubLedger" => ReturnPeriodicTrialBalanceLedgerReport(dtFinance),
            "SubLedger/Ledger" => ReturnPeriodicTrialBalanceLedgerReport(dtFinance),
            "Account Group" => ReturnPeriodicTrialBalanceAccountGroupReport(dtFinance),
            "Account Group/Ledger" => ReturnPeriodicTrialBalanceAccountGroupLedgerReport(dtFinance),
            "Account Group/Sub Group" => ReturnPeriodicTrialBalanceAccountGroupWithSubGroupReport(dtFinance),
            "Account Group/Sub Group/Ledger" => ReturnPeriodicTrialBalanceAccountSubGroupLedgerReport(dtFinance),
            _ => dtReport
        };
        return dtReport;
    }

    #endregion **----- PERODIC REPORT -----**

    #endregion --------------- TRIAL BALANCE ---------------

    // PROFIT & LOSS REPORT

    #region --------------- PROFIT & LOSS ---------------

    //NORMAL PROFIT & LOSS

    #region **----- NORMAL REPORT -----**

    private DataTable ReturnProfitLossLedgerReport(DataTable dtProfit)
    {
        DataRow newRow;
        var dtReport = dtProfit.Clone();
        var dtGroupType = dtProfit.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = groupStrings.Contains(roType["GrpType"].GetString()) ? "EXPENDITURE" : "INCOME";
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProfit.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in dtGroupDetails.Rows)
            {
                newRow = dtReport.NewRow();
                var balanceAmount = item["Balance"].GetDecimal();
                newRow["LedgerId"] = item["LedgerId"].ToString();
                newRow["ShortName"] = item["ShortName"].ToString();
                newRow["LedgerName"] = "         " + item["LedgerName"];
                newRow["Balance"] = Math.Abs(balanceAmount).GetDecimalComma();
                newRow["BalanceType"] = balanceAmount > 0 ? "Dr" : "Cr";
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            var groupName = groupStrings.Contains(roType["GrpType"].GetString()) ? "EXPENDITURE" : "INCOME";
            newRow["LedgerName"] = $"[{groupName}] TOTAL >>";
            newRow["Balance"] = Math.Abs(dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal()))
                .GetDecimalComma();
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var incomeSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "I")
            .Sum(row => Math.Abs(row["Balance"].GetDecimal()));
        var expenseSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "E")
            .Sum(row => row["Balance"].GetDecimal());
        var netProfit = incomeSum - expenseSum;
        if (Math.Abs(netProfit) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = netProfit switch
            {
                > 0 when GetReports.IsClosingStock => "NET PROFIT INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "NET PROFIT EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "NET LOSS INCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "NET LOSS EXCLUDING CLOSING STOCK",
                _ => "NET PROFIT"
            };
            newRow["Balance"] = Math.Abs(netProfit).GetDecimalComma();
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnProfitLossLedgerIncludeSubLedgerReport(DataTable dtProfit)
    {
        DataRow newRow;
        var dtReport = GetProfitLossBalanceSheetReportFormat();
        var dtGroupType = dtProfit.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper().Equals("EXPENDITURE")
                ? "EXPENSES"
                : roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProfit.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in dtGroupDetails.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "         " + item["GLName"];
                newRow["dt_Amount"] = item["Amount"].ToString();
                newRow["dt_Type"] = roType["AmountType"].ToString().ToUpper();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["dt_Amount"] = dtGroupDetails.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var incomeSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "INCOME")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var expenseSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "EXPENSES")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var netProfit = incomeSum - expenseSum;
        if (Math.Abs(netProfit) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = netProfit switch
            {
                > 0 when GetReports.IsClosingStock => "NET PROFIT INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "NET PROFIT EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "NET LOSS INCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "NET LOSS EXCLUDING CLOSING STOCK",
                _ => "NET PROFIT"
            };
            newRow["dt_Amount"] = Math.Abs(netProfit);
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnProfitLossLedgerAccountGroupOnlyReport(DataTable dtProfit)
    {
        DataRow newRow;
        var dtReport = GetProfitLossBalanceSheetReportFormat();
        var dtGroupType = dtProfit.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper().Equals("EXPENDITURE")
                ? "EXPENSES"
                : roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProfit.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in dtGroupDetails.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "         " + item["GLName"];
                newRow["dt_Amount"] = item["Amount"].ToString();
                newRow["dt_Type"] = roType["AmountType"].ToString().ToUpper();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["dt_Amount"] = dtGroupDetails.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var incomeSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "INCOME")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var expenseSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "EXPENSES")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var netProfit = incomeSum - expenseSum;
        if (Math.Abs(netProfit) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = netProfit switch
            {
                > 0 when GetReports.IsClosingStock => "NET PROFIT INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "NET PROFIT EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "NET LOSS INCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "NET LOSS EXCLUDING CLOSING STOCK",
                _ => "NET PROFIT"
            };
            newRow["dt_Amount"] = Math.Abs(netProfit);
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnProfitLossLedgerAccountSubGroupOnlyReport(DataTable dtProfit)
    {
        DataRow newRow;
        var dtReport = GetProfitLossBalanceSheetReportFormat();
        var dtGroupType = dtProfit.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper().Equals("EXPENDITURE")
                ? "EXPENSES"
                : roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtProfit.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in dtGroupDetails.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "         " + item["GLName"];
                newRow["dt_Amount"] = item["Amount"].ToString();
                newRow["dt_Type"] = roType["AmountType"].ToString().ToUpper();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["dt_Amount"] = dtGroupDetails.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var incomeSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "INCOME")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var expenseSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "EXPENSES")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var netProfit = incomeSum - expenseSum;
        if (Math.Abs(netProfit) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = netProfit switch
            {
                > 0 when GetReports.IsClosingStock => "NET PROFIT INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "NET PROFIT EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "NET LOSS INCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "NET LOSS EXCLUDING CLOSING STOCK",
                _ => "NET PROFIT"
            };
            newRow["dt_Amount"] = Math.Abs(netProfit);
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnProfitLossLedgerAccountGroupWiseReport(DataTable dtProfit)
    {
        DataRow newRow;
        var dtReport = dtProfit.Clone();
        var dtGroupType = dtProfit.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            var type = roType["GrpType"].GetString();
            newRow = dtReport.NewRow();
            var ledger = groupStrings.Contains(type) ? "EXPENDITURE" : "INCOME";
            newRow["LedgerName"] = ledger;
            newRow["IsGroup"] = 111;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var groupDetails = dtProfit.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();

            var groupWise = groupDetails.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();

            foreach (DataRow roGroup in groupWise.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["LedgerId"] = roGroup["GrpId"];
                newRow["ShortName"] = roGroup["GrpCode"];
                newRow["LedgerName"] = roGroup["GrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var dtGroupDetails = groupDetails
                    .Select($"GrpType = '{roType["GrpType"]}' and GrpName='{roGroup["GrpName"]}' ").CopyToDataTable();

                foreach (DataRow item in dtGroupDetails.Rows)
                {
                    newRow = dtReport.NewRow();
                    var balanceAmount = item["Balance"].GetDecimal();
                    newRow["LedgerId"] = item["LedgerId"].ToString();
                    newRow["ShortName"] = item["ShortName"].ToString();
                    newRow["LedgerName"] = "         " + item["LedgerName"];
                    newRow["Balance"] = Math.Abs(balanceAmount).GetDecimalComma();
                    newRow["BalanceType"] = balanceAmount > 0 ? "Dr" : "Cr";
                    newRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["LedgerName"] = $"[{roGroup["GrpName"]}] TOTAL >>";
                newRow["Balance"] = Math.Abs(dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal()))
                    .GetDecimalComma();
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            var groupName = groupStrings.Contains(roType["GrpType"].GetString()) ? "EXPENDITURE" : "INCOME";
            newRow["LedgerName"] = $"[{groupName}] TOTAL >>";
            newRow["Balance"] = Math.Abs(groupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal()))
                .GetDecimalComma();
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var incomeSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "I")
            .Sum(row => Math.Abs(row["Balance"].GetDecimal()));
        var expenseSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "E")
            .Sum(row => row["Balance"].GetDecimal());
        var netProfit = incomeSum - expenseSum;
        if (Math.Abs(netProfit) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = netProfit switch
            {
                > 0 when GetReports.IsClosingStock => "NET PROFIT INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "NET PROFIT EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "NET LOSS INCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "NET LOSS EXCLUDING CLOSING STOCK",
                _ => "NET PROFIT"
            };
            newRow["Balance"] = Math.Abs(netProfit).GetDecimalComma();
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnProfitLossLedgerAccountSubGroupWiseReport(DataTable dtProfit)
    {
        DataRow newRow;
        var dtReport = dtProfit.Clone();
        var dtGroupType = dtProfit.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = groupStrings.Contains(roType["GrpType"].GetString()) ? "EXPENDITURE" : "INCOME";
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var groupDetails = dtProfit.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();

            var groupWise = groupDetails.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();

            foreach (DataRow roGroup in groupWise.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["LedgerId"] = roGroup["GrpId"];
                newRow["ShortName"] = roGroup["GrpCode"];
                newRow["LedgerName"] = roGroup["GrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var dtGroupDetails = groupDetails
                    .Select($"GrpType = '{roType["GrpType"]}' and GrpName='{roGroup["GrpName"]}' ").CopyToDataTable();

                var subGroupDetails = dtGroupDetails.AsEnumerable().GroupBy(row => new
                {
                    grpType = row.Field<string>("SubGrpName")
                }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("SubGrpName"))
                    .CopyToDataTable();
                foreach (DataRow roSubGroup in groupWise.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["LedgerId"] = roSubGroup["SubGrpId"];
                    newRow["ShortName"] = roSubGroup["SubGrpCode"];
                    newRow["LedgerName"] = roSubGroup["SubGrpName"];
                    newRow["IsGroup"] = 3;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var dtSubGroupDetails = dtGroupDetails.Select($"SubGrpName='{roSubGroup["SubGrpName"]}' ")
                        .CopyToDataTable();

                    foreach (DataRow item in dtSubGroupDetails.Rows)
                    {
                        newRow = dtReport.NewRow();
                        var balanceAmount = item["Balance"].GetDecimal();
                        newRow["LedgerId"] = item["LedgerId"].ToString();
                        newRow["ShortName"] = item["ShortName"].ToString();
                        newRow["LedgerName"] = "         " + item["LedgerName"];
                        newRow["Balance"] = Math.Abs(balanceAmount).GetDecimalComma();
                        newRow["BalanceType"] = balanceAmount > 0 ? "Dr" : "Cr";
                        newRow["IsGroup"] = 0;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                    }

                    newRow = dtReport.NewRow();
                    newRow["LedgerName"] = $"[{roSubGroup["SubGrpName"]}] TOTAL >>";
                    newRow["Balance"] = Math.Abs(dtSubGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal()))
                        .GetDecimalComma();
                    newRow["IsGroup"] = 33;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["LedgerName"] = $"[{roGroup["GrpName"]}] TOTAL >>";
                newRow["Balance"] = Math.Abs(dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal()))
                    .GetDecimalComma();
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            var groupName = groupStrings.Contains(roType["GrpType"].GetString()) ? "EXPENDITURE" : "INCOME";
            newRow["LedgerName"] = $"[{groupName}] TOTAL >>";
            newRow["Balance"] = Math.Abs(groupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal()))
                .GetDecimalComma();
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var incomeSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "I")
            .Sum(row => Math.Abs(row["Balance"].GetDecimal()));
        var expenseSum = dtProfit.AsEnumerable().Where(row => row.Field<string>("GrpType").GetUpper() is "E")
            .Sum(row => row["Balance"].GetDecimal());
        var netProfit = incomeSum - expenseSum;
        if (Math.Abs(netProfit) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = netProfit switch
            {
                > 0 when GetReports.IsClosingStock => "NET PROFIT INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "NET PROFIT EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "NET LOSS INCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "NET LOSS EXCLUDING CLOSING STOCK",
                _ => "NET PROFIT"
            };
            newRow["Balance"] = Math.Abs(netProfit).GetDecimalComma();
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private string GetProfitLossNormalReportScript()
    {
        var cmdString = $@"
			WITH ProfitLoss AS (SELECT	gl.GLID LedgerId, gl.GLName LedgerName, gl.GLCode ShortName, ag.GrpId, ISNULL(ag.GrpName, 'NO-GROUP') GrpName, ag.GrpCode, ag.Schedule,ag.GrpType, asg.SubGrpId, ISNULL(asg.SubGrpName, 'NO-SUB GROUP') SubGrpName, asg.SubGrpCode,ISNULL(b.Balance,0) Balance, CASE WHEN (ISNULL(b.Balance, 0) + ISNULL(s.OpeningStock,0) + ISNULL(c.ClosingStock,0) )>0 THEN ISNULL(b.Balance, 0) + ISNULL(s.OpeningStock,0) + ISNULL(c.ClosingStock,0) ELSE 0 END DebitAmount, CASE WHEN (ISNULL(b.Balance, 0) + ISNULL(s.OpeningStock,0) + ISNULL(c.ClosingStock,0)) <0 THEN ABS( ISNULL(b.Balance, 0) + ISNULL(s.OpeningStock,0) + ISNULL(c.ClosingStock,0)) ELSE 0 END CreditAmount
									FROM AMS.GeneralLedger gl
										LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
										LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
										LEFT OUTER JOIN(SELECT Ledger_ID, SUM(LocalDebit_Amt-LocalCredit_Amt) Balance
														FROM AMS.AccountDetails ad1
														WHERE ad1.FiscalYearId={ObjGlobal.SysFiscalYearId} AND ad1.Branch_ID={ObjGlobal.SysBranchId} AND ad1.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}' ";
        if (GetReports.IsIncludePdc)
        {
            cmdString += " AND ad1.Module NOT IN ( 'PDC','PROV') ";
        }

        cmdString += $@"
														GROUP BY ad1.Ledger_ID) AS b ON b.Ledger_ID=gl.GLID
										LEFT OUTER JOIN(SELECT ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId}) LedgerId, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) OpeningStock
														FROM AMS.StockDetails sd
															LEFT OUTER JOIN AMS.Product p ON p.PID=sd.Product_Id
														WHERE sd.Voucher_Date<'{ObjGlobal.CfStartAdDate.GetSystemDate()}'
														GROUP BY ISNULL(p.PL_Opening, {ObjGlobal.StockOpeningStockLedgerId})) AS s ON s.LedgerId=gl.GLID
										LEFT OUTER JOIN(SELECT ISNULL(p.PL_Closing, {ObjGlobal.StockClosingStockLedgerId}) LedgerId, ";
        cmdString += GetReports.IsClosingStock
            ? @" -SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) ClosingStock "
            : " 0 ClosingStock ";
        cmdString += $@"
														FROM AMS.StockDetails sd
															LEFT OUTER JOIN AMS.Product p ON p.PID=sd.Product_Id
														WHERE sd.Voucher_Date<'{GetReports.ToDate.GetSystemDate()}'
														GROUP BY ISNULL(p.PL_Closing, {ObjGlobal.StockClosingStockLedgerId})) AS c ON c.LedgerId=gl.GLID

									WHERE ag.PrimaryGrp IN ('Profit & Loss', 'PL', 'TA', 'Trading Account')AND (ISNULL(b.Balance, 0) + ISNULL(s.OpeningStock,0) + ISNULL(c.ClosingStock,0) ) <>0)
			SELECT ot.LedgerId, ot.LedgerName, ot.ShortName, ot.GrpId, ot.GrpName, ot.GrpCode, ot.Schedule,ot.GrpType, ot.SubGrpId, ot.SubGrpName, ot.SubGrpCode, FORMAT((ot.DebitAmount - ot.CreditAmount), '##,##,##0.00') Balance,'' BalanceType, 0 IsGroup
			FROM ProfitLoss ot ";
        cmdString += GetReports.SortOn switch
        {
            "SCHEDULE" => @"
			ORDER BY  ot.GrpType DESC,ot.Schedule,ot.LedgerName, ot.GrpName, ot.SubGrpName; ",
            _ => @"
			ORDER BY  ot.GrpType DESC,ot.LedgerName, ot.GrpName, ot.SubGrpName;"
        };
        return cmdString;
    }

    private string GetProfitLossSubledgerNormalReportScript()
    {
        var cmdString = $@"
			WITH FinanceReport
			  AS
			  (
				SELECT OBS.Ledger_ID,OBS.Subleder_ID ,OBS.Amount,OBS.AmountType
				 FROM (
						SELECT ad.Ledger_ID, CASE WHEN AG.GrpType='Expenses' THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) END Amount,
						CASE
							WHEN AG.GrpType = 'Expenses' AND SUM(ad.LocalDebit_Amt -ad.LocalCredit_Amt) > 0 THEN 'Dr'
							WHEN AG.GrpType = 'Expenses' AND SUM(ad.LocalDebit_Amt -ad.LocalCredit_Amt) < 0 THEN 'Cr'
							WHEN AG.GrpType = 'Income' AND SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN 'Cr'
							WHEN AG.GrpType = 'Income' AND SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) < 0 THEN 'Dr'
							ELSE ''
						END  AmountType
						 FROM AMS.AccountDetails ad
							LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ad.Ledger_ID
							LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						 WHERE ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND ag.PrimaryGrp IN ('Profit & Loss','PL') AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} ";
        cmdString += !GetReports.IsIncludePdc
            ? @"
						 AND ad.Module NOT IN ('PDC','PROV') "
            : string.Empty;
        cmdString += @"
						 GROUP BY ad.Ledger_ID, ad.Subleder_ID,AG.GrpType,ad.Subleder_ID
						 HAVING  SUM(ad.LocalDebit_Amt -ad.LocalCredit_Amt) <> 0
					  ) OBS
			  )
			 SELECT fr.Ledger_ID,gl.GLName, gl.GLCode ShortName,fr.Subleder_ID,ISNULL(sl.SLName,'NO-SUBLEDGER') SLName,sl.SLCode, gl.GrpId, GrpName, ag.GrpCode, ag.GrpType, ISNULL ( gl.SubGrpId, 0 ) SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUBGROUP' ) SubGrpName, asg.SubGrpCode,ABS ( fr.Amount ) Amount, fr.Amount ActualAmount, fr.AmountType
			  FROM FinanceReport fr
				   LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
				   LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
				   LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
				   LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId = fr.Subleder_ID
			ORDER BY ag.GrpType DESC, gl.GLName;";
        return cmdString;
    }

    public DataTable GetNormalProfitLossReport()
    {
        var dtReport = new DataTable();
        try
        {
            if (GetReports.IsClosingStock)
            {
                if (GetReports.IsRePostValue)
                {
                    const string cmdRePost = "AMS.USP_PostStockValue";
                    var result = SqlExtensions.ExecuteNonQuery(cmdRePost, new SqlParameter("@PCode", null));
                }
            }

            var cmdString = GetReports.FilterFor switch
            {
                "Ledger/SubLedger" => GetProfitLossSubledgerNormalReportScript(),
                _ => GetProfitLossNormalReportScript()
            };
            var dtProfit = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            if (dtProfit.Rows.Count <= 0)
            {
                return dtProfit;
            }

            dtReport = GetReports.FilterFor switch
            {
                "Ledger" => ReturnProfitLossLedgerReport(dtProfit),
                "Ledger/SubLedger" => ReturnProfitLossLedgerIncludeSubLedgerReport(dtProfit),
                "Account Group" => ReturnProfitLossLedgerAccountGroupOnlyReport(dtProfit),
                "Account Group/Ledger" => ReturnProfitLossLedgerAccountGroupWiseReport(dtProfit),
                "Account Group/Sub Group" => ReturnProfitLossLedgerAccountSubGroupOnlyReport(dtProfit),
                "Account Group/Sub Group/Ledger" => ReturnProfitLossLedgerAccountSubGroupWiseReport(dtProfit),
                _ => new DataTable("Blank")
            };
            return dtReport;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
            return dtReport;
        }
    }

    #endregion **----- NORMAL REPORT -----**

    //PERIODIC PROFIT & LOSS

    #region **----- PERODIC REPORT -----**

    private DataTable GetPeriodicProfitLossAndBalanceSheetReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dt_LedgerId",
            "dt_ShortName",
            "dt_Desc",
            "dt_Opening",
            "dt_PeriodicDebit",
            "dt_PeriodicCredit",
            "dt_Balance",
            "dt_ActualBalance",
            "dt_Type",
            "dt_GroupBy",
            "dt_FilterBy",
            "dt_SubGroup"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnPeriodicProfitLossLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            foreach (var item in dtFinance.Select($"GrpType = '{roType["GrpType"]}'"))
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "         " + item["GLName"];

                var _Opening = item["Opening"].GetDecimal();
                var _PrDebit = item["PrDebitAmt"].GetDecimal();
                var _PrCredit = item["PrCreditAmt"].GetDecimal();
                var balance = item["Balance"].GetDecimal();
                newRow["dt_Opening"] = _Opening;
                newRow["dt_PeriodicDebit"] = _PrDebit;
                newRow["dt_PeriodicCredit"] = _PrCredit;
                newRow["dt_Balance"] = Math.Abs(balance);
                newRow["dt_ActualBalance"] = balance;
                newRow["dt_Type"] = item["BalanceType"].ToString();
                newRow["IsGroup"] = 0;
                newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Income")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Income")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Income")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Income")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Expenses")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Expenses")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Expenses")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Expenses")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = GetReports.IsClosingStock
                ? "NET PROFIT & LOSS INCLUDE CLOSING STOCK >> "
                : "NET PROFIT & LOSS EXCLUDING CLOSING STOCK >> ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicProfitLossLedgerIncludeSubLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = "1";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            newRow = dtReport.NewRow();

            foreach (var item in dtFinance.Select($"GrpType = '{roType["GrpType"]}'"))
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = item["GLName"].ToString();

                double.TryParse(item["OpeningDebit"].ToString(), out var _OpeningDebit);
                double.TryParse(item["OpeningCredit"].ToString(), out var _OpeningCredit);
                double.TryParse(item["PrDebitAmt"].ToString(), out var _PrDebit);
                double.TryParse(item["PrCreditAmt"].ToString(), out var _PrCredit);

                newRow["dt_OpeningDebit"] = _OpeningDebit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_OpeningCredit"] = _OpeningCredit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_ClosingDebit"] = _PrDebit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_ClosingCredit"] = _PrCredit.ToString(ObjGlobal.SysAmountFormat);
                newRow["IsGroup"] = "0";
                newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();

            newRow["dt_Desc"] = $"{roType["GrpType"]} TOTAL :- ";
            newRow["dt_OpeningDebit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            newRow["dt_OpeningCredit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            newRow["dt_ClosingDebit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
            newRow["dt_ClosingCredit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
            newRow["IsGroup"] = "11";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var OpeningDebit = new decimal();
        var Debit = new decimal();
        var OpeningCredit = new decimal();
        var Credit = new decimal();

        var dtExpense = dtReport.Select("dt_GroupBy = 'Expenses' or dt_GroupBy = 'Expenditure' ").CopyToDataTable();
        if (dtExpense.Rows.Count > 0)
        {
            OpeningDebit = dtExpense.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            Debit = dtExpense.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        }

        var dtIncome = dtReport.Select("dt_GroupBy = 'Income' or dt_GroupBy = 'Income' ").CopyToDataTable();
        if (dtIncome.Rows.Count > 0)
        {
            OpeningCredit = dtIncome.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            Credit = dtIncome.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        }

        var OB_Profit = OpeningCredit - OpeningDebit;
        var CB_Profit = Credit - Debit;

        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "NET PROFIT & LOSS :- ";
        newRow["dt_OpeningDebit"] = OB_Profit > 0 ? OB_Profit.ToString() : 0.00.ToString();
        newRow["dt_OpeningCredit"] = OB_Profit < 0 ? Math.Abs(OB_Profit).ToString() : 0.00.ToString();
        newRow["dt_ClosingDebit"] = CB_Profit > 0 ? CB_Profit.ToString() : 0.00.ToString();
        newRow["dt_ClosingCredit"] = CB_Profit < 0 ? Math.Abs(CB_Profit).ToString() : 0.00.ToString();
        newRow["IsGroup"] = "88";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        var dtGrand = dtReport.Select("IsGroup = 0 or IsGroup = 88 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        newRow["IsGroup"] = "99";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnPeriodicProfitLossLedgerAccountGroupOnlyReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = "1";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            newRow = dtReport.NewRow();

            foreach (var item in dtFinance.Select($"GrpType = '{roType["GrpType"]}'"))
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = item["GLName"].ToString();

                double.TryParse(item["OpeningDebit"].ToString(), out var _OpeningDebit);
                double.TryParse(item["OpeningCredit"].ToString(), out var _OpeningCredit);
                double.TryParse(item["PrDebitAmt"].ToString(), out var _PrDebit);
                double.TryParse(item["PrCreditAmt"].ToString(), out var _PrCredit);

                newRow["dt_OpeningDebit"] = _OpeningDebit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_OpeningCredit"] = _OpeningCredit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_ClosingDebit"] = _PrDebit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_ClosingCredit"] = _PrCredit.ToString(ObjGlobal.SysAmountFormat);
                newRow["IsGroup"] = "0";
                newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();

            newRow["dt_Desc"] = $"{roType["GrpType"]} TOTAL :- ";
            newRow["dt_OpeningDebit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            newRow["dt_OpeningCredit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            newRow["dt_ClosingDebit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
            newRow["dt_ClosingCredit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
            newRow["IsGroup"] = "11";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var OpeningDebit = new decimal();
        var Debit = new decimal();
        var OpeningCredit = new decimal();
        var Credit = new decimal();

        var dtExpense = dtReport.Select("dt_GroupBy = 'Expenses' or dt_GroupBy = 'Expenditure' ").CopyToDataTable();
        if (dtExpense.Rows.Count > 0)
        {
            OpeningDebit = dtExpense.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            Debit = dtExpense.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        }

        var dtIncome = dtReport.Select("dt_GroupBy = 'Income' or dt_GroupBy = 'Income' ").CopyToDataTable();
        if (dtIncome.Rows.Count > 0)
        {
            OpeningCredit = dtIncome.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            Credit = dtIncome.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        }

        var OB_Profit = OpeningCredit - OpeningDebit;
        var CB_Profit = Credit - Debit;

        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "NET PROFIT & LOSS :- ";
        newRow["dt_OpeningDebit"] = OB_Profit > 0 ? OB_Profit.ToString() : 0.00.ToString();
        newRow["dt_OpeningCredit"] = OB_Profit < 0 ? Math.Abs(OB_Profit).ToString() : 0.00.ToString();
        newRow["dt_ClosingDebit"] = CB_Profit > 0 ? CB_Profit.ToString() : 0.00.ToString();
        newRow["dt_ClosingCredit"] = CB_Profit < 0 ? Math.Abs(CB_Profit).ToString() : 0.00.ToString();
        newRow["IsGroup"] = "88";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        var dtGrand = dtReport.Select("IsGroup = 0 or IsGroup = 88 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        newRow["IsGroup"] = "99";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnPeriodicProfitLossLedgerAccountGroupWiseReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroup = dtFinance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroupBy = dtGroup.AsEnumerable().GroupBy(row => new
            {
                groupBy = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();

            foreach (DataRow roGroup in dtGroupBy.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_ShortName"] = roGroup["GrpCode"].ToString();
                newRow["dt_Desc"] = "     " + roGroup["GrpName"].ToString().ToUpper();
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                var dtGroupDetails = dtGroup.Select($"GrpName= '{roGroup["GrpName"]}'").CopyToDataTable();
                foreach (DataRow item in dtGroupDetails.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                    newRow["dt_ShortName"] = item["ShortName"].ToString();
                    newRow["dt_Desc"] = "         " + item["GLName"];

                    var _Opening = item["Opening"].GetDecimal();
                    var _PrDebit = item["PrDebitAmt"].GetDecimal();
                    var _PrCredit = item["PrCreditAmt"].GetDecimal();
                    var balance = item["Balance"].GetDecimal();

                    newRow["dt_Opening"] = _Opening;
                    newRow["dt_PeriodicDebit"] = _PrDebit;
                    newRow["dt_PeriodicCredit"] = _PrCredit;
                    newRow["dt_Balance"] = Math.Abs(balance);
                    newRow["dt_ActualBalance"] = balance;
                    newRow["dt_Type"] = item["BalanceType"].ToString();
                    newRow["IsGroup"] = 0;
                    newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                    newRow["dt_FilterBy"] = roGroup["GrpName"].ToString();
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                var dtGroupTotal = dtReport.Select($"dt_FilterBy ='{roGroup["GrpName"]}'").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = $"[{roGroup["GrpName"].GetUpper()}] TOTAL :- ";
                newRow["dt_Opening"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
                newRow["dt_PeriodicCredit"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
                newRow["dt_Balance"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = GetReports.IsClosingStock
                ? "NET PROFIT & LOSS INCLUDE CLOSING STOCK >> "
                : "NET PROFIT & LOSS EXCLUDING CLOSING STOCK >> ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicProfitLossLedgerAccountSubGroupOnlyReport(DataTable dtFinance)
    {
        var view = new DataView(dtFinance);
        var dtReport = new DataTable();
        var DistinctGrpType = view.ToTable(true, "GrpType");
        DistinctGrpType.DefaultView.Sort = "GrpType DESC";
        DistinctGrpType = DistinctGrpType.DefaultView.ToTable();
        DataRow newRow;

        dtReport.Columns.Add("dt_LedgerId", typeof(string));
        dtReport.Columns.Add("dt_ShortName", typeof(string));
        dtReport.Columns.Add("dt_Desc", typeof(string));
        dtReport.Columns.Add("dt_OpeningDebit", typeof(string));
        dtReport.Columns.Add("dt_OpeningCredit", typeof(string));
        dtReport.Columns.Add("dt_ClosingDebit", typeof(string));
        dtReport.Columns.Add("dt_ClosingCredit", typeof(string));
        dtReport.Columns.Add("dt_GroupBy", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        foreach (DataRow roType in DistinctGrpType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = "1";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            newRow = dtReport.NewRow();

            foreach (var item in dtFinance.Select($"GrpType = '{roType["GrpType"]}'"))
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = item["GLName"].ToString();

                double.TryParse(item["OpeningDebit"].ToString(), out var _OpeningDebit);
                double.TryParse(item["OpeningCredit"].ToString(), out var _OpeningCredit);
                double.TryParse(item["PrDebitAmt"].ToString(), out var _PrDebit);
                double.TryParse(item["PrCreditAmt"].ToString(), out var _PrCredit);

                newRow["dt_OpeningDebit"] = _OpeningDebit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_OpeningCredit"] = _OpeningCredit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_ClosingDebit"] = _PrDebit.ToString(ObjGlobal.SysAmountFormat);
                newRow["dt_ClosingCredit"] = _PrCredit.ToString(ObjGlobal.SysAmountFormat);
                newRow["IsGroup"] = "0";
                newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();

            newRow["dt_Desc"] = $"{roType["GrpType"]} TOTAL :- ";
            newRow["dt_OpeningDebit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            newRow["dt_OpeningCredit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            newRow["dt_ClosingDebit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
            newRow["dt_ClosingCredit"] = dtGrpTotal.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
            newRow["IsGroup"] = "11";
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var OpeningDebit = new decimal();
        var Debit = new decimal();
        var OpeningCredit = new decimal();
        var Credit = new decimal();

        var dtExpense = dtReport.Select("dt_GroupBy = 'Expenses' or dt_GroupBy = 'Expenditure' ").CopyToDataTable();
        if (dtExpense.Rows.Count > 0)
        {
            OpeningDebit = dtExpense.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
            Debit = dtExpense.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        }

        var dtIncome = dtReport.Select("dt_GroupBy = 'Income' or dt_GroupBy = 'Income' ").CopyToDataTable();
        if (dtIncome.Rows.Count > 0)
        {
            OpeningCredit = dtIncome.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
            Credit = dtIncome.AsEnumerable().Sum(x =>
                Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        }

        var OB_Profit = OpeningCredit - OpeningDebit;
        var CB_Profit = Credit - Debit;

        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "NET PROFIT & LOSS :- ";
        newRow["dt_OpeningDebit"] = OB_Profit > 0 ? OB_Profit.ToString() : 0.00.ToString();
        newRow["dt_OpeningCredit"] = OB_Profit < 0 ? Math.Abs(OB_Profit).ToString() : 0.00.ToString();
        newRow["dt_ClosingDebit"] = CB_Profit > 0 ? CB_Profit.ToString() : 0.00.ToString();
        newRow["dt_ClosingCredit"] = CB_Profit < 0 ? Math.Abs(CB_Profit).ToString() : 0.00.ToString();
        newRow["IsGroup"] = "88";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        var dtGrand = dtReport.Select("IsGroup = 0 or IsGroup = 88 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Desc"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningDebit"] == DBNull.Value ? 0 : x["dt_OpeningDebit"]));
        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_OpeningCredit"] == DBNull.Value ? 0 : x["dt_OpeningCredit"]));
        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingDebit"] == DBNull.Value ? 0 : x["dt_ClosingDebit"]));
        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x =>
            Convert.ToDecimal(x["dt_ClosingCredit"] == DBNull.Value ? 0 : x["dt_ClosingCredit"]));
        newRow["IsGroup"] = "99";
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnPeriodicProfitLossLedgerAccountSubGroupWiseReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroup = dtFinance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroupBy = dtGroup.AsEnumerable().GroupBy(row => new
            {
                groupBy = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();

            foreach (DataRow roGroup in dtGroupBy.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_ShortName"] = roGroup["GrpCode"].ToString();
                newRow["dt_Desc"] = "     " + roGroup["GrpName"].ToString().ToUpper();
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var dtGroupDetails = dtGroup.Select($"GrpName= '{roGroup["GrpName"]}'").CopyToDataTable();
                var dtSubGroup = dtGroupDetails.AsEnumerable().GroupBy(row => new
                {
                    groupBy = row.Field<string>("SubGrpName")
                }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("SubGrpName"))
                    .CopyToDataTable();

                foreach (DataRow roSubGroup in dtSubGroup.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_ShortName"] = roSubGroup["GrpCode"].ToString();
                    newRow["dt_Desc"] = "          " + roSubGroup["SubGrpName"].ToString().ToUpper();
                    newRow["IsGroup"] = 3;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var dtSubGroupDetails = dtGroupDetails
                        .Select($"GrpName= '{roGroup["GrpName"]}' and SubGrpName= '{roSubGroup["SubGrpName"]}'")
                        .CopyToDataTable();
                    foreach (DataRow item in dtSubGroupDetails.Rows)
                    {
                        newRow = dtReport.NewRow();
                        newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                        newRow["dt_ShortName"] = item["ShortName"].ToString();
                        newRow["dt_Desc"] = "         " + item["GLName"];

                        var _Opening = item["Opening"].GetDecimal();
                        var _PrDebit = item["PrDebitAmt"].GetDecimal();
                        var _PrCredit = item["PrCreditAmt"].GetDecimal();
                        var balance = item["Balance"].GetDecimal();

                        newRow["dt_Opening"] = _Opening;
                        newRow["dt_PeriodicDebit"] = _PrDebit;
                        newRow["dt_PeriodicCredit"] = _PrCredit;
                        newRow["dt_Balance"] = Math.Abs(balance);
                        newRow["dt_ActualBalance"] = balance;
                        newRow["dt_Type"] = item["BalanceType"].ToString();
                        newRow["IsGroup"] = 0;
                        newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                        newRow["dt_FilterBy"] = roGroup["GrpName"].ToString();
                        newRow["dt_SubGroup"] = roSubGroup["SubGrpName"].ToString();
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                    }

                    var dtSubGroupTotal = dtReport
                        .Select($"dt_FilterBy ='{roGroup["GrpName"]}' and dt_SubGroup= '{roSubGroup["SubGrpName"]}'")
                        .CopyToDataTable();
                    newRow = dtReport.NewRow();
                    newRow["dt_Desc"] = $"[{roSubGroup["SubGrpName"].GetUpper()}] TOTAL :- ";
                    newRow["dt_Opening"] = dtSubGroupTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
                    newRow["dt_PeriodicDebit"] =
                        dtSubGroupTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
                    newRow["dt_PeriodicCredit"] =
                        dtSubGroupTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
                    newRow["dt_Balance"] = dtSubGroupTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
                    newRow["IsGroup"] = 33;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                var dtGroupTotal = dtReport.Select($"dt_FilterBy ='{roGroup["GrpName"]}'").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = $"[{roGroup["GrpName"].GetUpper()}] TOTAL :- ";
                newRow["dt_Opening"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
                newRow["dt_PeriodicCredit"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
                newRow["dt_Balance"] = dtGroupTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "INCOME")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "EXPENSES")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = GetReports.IsClosingStock
                ? "NET PROFIT & LOSS INCLUDE CLOSING STOCK >> "
                : "NET PROFIT & LOSS EXCLUDING CLOSING STOCK >> ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private string GetProfitAndLossPeriodicReportScript()
    {
        var cmd = string.Empty;
        if (GetReports.IsSubLedger)
        {
            cmd = $@"
				WITH FinanceReport
				  AS
				  (
					SELECT ProfitLoss.Ledger_ID,ProfitLoss.Subleder_ID, SUM ( ProfitLoss.Opening ) Opening, SUM ( ProfitLoss.PrDebitAmt ) PrDebitAmt, SUM ( ProfitLoss.PrCreditAmt ) PrCreditAmt
					 FROM (
							SELECT ad.Ledger_ID,ad.Subleder_ID, ISNULL ( CASE WHEN ad.Voucher_Date < '2020-07-16' THEN SUM ( LocalDebit_Amt - ad.Credit_Amt ) END, 0 ) Opening, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '2020-07-16' AND '2021-07-15' THEN SUM ( LocalDebit_Amt ) END, 0 ) PrDebitAmt, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '2020-07-16' AND '2021-07-15' THEN SUM ( LocalCredit_Amt ) END, 0 ) PrCreditAmt
							 FROM AMS.AccountDetails ad
								LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ad.Ledger_ID
								LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
								LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
							 WHERE ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId})  AND  ag.PrimaryGrp IN ('Profit & Loss') ";
            cmd += !GetReports.IsIncludePdc
                ? @"    AND ad.Module NOT IN ('PDC', 'PROV') "
                : string.Empty;
            cmd += @"
							 GROUP BY ad.Ledger_ID, ad.Voucher_Date,ad.Subleder_ID
						  ) ProfitLoss
					 GROUP BY ProfitLoss.Ledger_ID,ProfitLoss.Subleder_ID
				  )
				 SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName, gl.GrpId, GrpName,ag.GrpCode,fr.Subleder_ID,ISNULL(sl.SLName,'NO-SUBLEDGER') SLName,sl.SLCode, ag.GrpType, ISNULL(gl.SubGrpId, 0) SubGrpId, ISNULL(asg.SubGrpName, 'NO SUBGROUP') SubGrpName,asg.SubGrpCode, SUM(fr.Opening) Opening, SUM(PrDebitAmt) PrDebitAmt, SUM(PrCreditAmt) PrCreditAmt, CASE WHEN ag.GrpType='Income' THEN SUM(fr.PrCreditAmt-fr.PrDebitAmt - fr.Opening)ELSE SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)END Balance, CASE WHEN ag.GrpType='Income' AND SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)>0 THEN 'Cr' WHEN ag.GrpType='Income' AND SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)<0 THEN 'Dr' WHEN ag.GrpType='Assets' AND SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)>0 THEN 'Dr' WHEN ag.GrpType='Assets' AND SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)<0 THEN 'Cr' ELSE '' END BalanceType
				  FROM FinanceReport fr
					   LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
					   LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					   LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
					   LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId = fr.Subleder_ID
				  GROUP BY fr.Ledger_ID, fr.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, ag.GrpType, gl.SubGrpId, asg.SubGrpName,ag.GrpCode,asg.SubGrpCode,ISNULL(sl.SLName,'NO-SUBLEDGER'),sl.SLCode,fr.Subleder_ID
				  ORDER BY ag.GrpType DESC, gl.GLName;  ";
        }
        else
        {
            cmd = $@"
				WITH FinanceReport
				  AS
				  (
					SELECT ProfitLoss.Ledger_ID, SUM ( ProfitLoss.Opening ) Opening, SUM ( ProfitLoss.PrDebitAmt ) PrDebitAmt, SUM ( ProfitLoss.PrCreditAmt ) PrCreditAmt,SUM( ProfitLoss.Balance) Balance
					 FROM (
							SELECT ad.Ledger_ID, ISNULL ( CASE WHEN ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' THEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt) END, 0 ) Opening,
								ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' THEN SUM ( ad.LocalDebit_Amt ) END, 0 ) PrDebitAmt,
								ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' THEN SUM ( ad.LocalCredit_Amt ) END, 0 ) PrCreditAmt,
								ISNULL	(CASE WHEN ad.Voucher_Date < '{GetReports.ToDate.GetSystemDate()}' AND ag.GrpType IN ('I','Income')  THEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt) WHEN ad.Voucher_Date < '{GetReports.ToDate.GetSystemDate()}' AND ag.GrpType IN ('E' ,'Expenses')  THEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt)       ELSE 0  END, 0 ) Balance
								 FROM AMS.AccountDetails ad
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ad.Ledger_ID
									  LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
									  LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
							 WHERE ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId})  AND  ag.PrimaryGrp IN ('PL','Profit & Loss') ";
            cmd += !GetReports.IsIncludePdc
                ? @"    AND ad.Module NOT IN ('PDC', 'PROV') "
                : string.Empty;
            cmd += @"
							GROUP BY ad.Ledger_ID, ad.Voucher_Date,ag.GrpType
						  ) ProfitLoss
					 GROUP BY ProfitLoss.Ledger_ID
				  )
				 SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName, gl.GrpId, GrpName, ag.GrpCode, CASE WHEN ag.GrpType = 'I' THEN 'INCOME' WHEN ag.GrpType='E' THEN 'EXPENSES' ELSE ag.GrpType END GrpType, ISNULL ( gl.SubGrpId, 0 ) SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUBGROUP' ) SubGrpName, asg.SubGrpCode, SUM ( fr.Opening ) Opening, SUM ( PrDebitAmt ) PrDebitAmt, SUM ( PrCreditAmt ) PrCreditAmt, SUM(fr.Balance) Balance,
				 CASE WHEN ag.GrpType IN ('I','Income') AND SUM (Balance) > 0 THEN 'Cr'
					WHEN ag.GrpType IN ('I','Income') AND SUM ( Balance) < 0 THEN 'Dr'
					WHEN ag.GrpType IN ('E' ,'Expenses') AND SUM ( Balance) > 0 THEN 'Dr'
					WHEN ag.GrpType IN ('E' ,'Expenses') AND SUM ( Balance) < 0 THEN 'Cr' ELSE '' END BalanceType
				  FROM FinanceReport fr
					   LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
					   LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					   LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
				  GROUP BY fr.Ledger_ID, fr.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, ag.GrpType, gl.SubGrpId, asg.SubGrpName, ag.GrpCode, asg.SubGrpCode
				  ORDER BY ag.GrpType DESC, gl.GLName; ";
        }

        return cmd;
    }

    public DataTable GetPeriodicProfitLossReport()
    {
        var dtReport = new DataTable();
        var cmdString = string.Empty;
        if (GetReports.IsClosingStock)
        {
            if (GetReports.IsRePostValue)
            {
                var result = SqlExtensions.ExecuteNonQuery("AMS.USP_PostStockValue",
                    new SqlParameter("@PCode", string.Empty));
            }

            var stockValue = $@"
				DELETE AMS.AccountDetails WHERE Module='PLCS' AND Voucher_No=N'00000';
				INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, EnterBy, EnterDate, Branch_ID, FiscalYearId)
				SELECT 'PLCS' Module, ROW_NUMBER() OVER (ORDER BY p.PL_Closing) Serial_No, N'00000' Voucher_No, '{GetReports.FromDate.GetSystemDate()}' Voucher_Date, N'{GetReports.FromDate.GetNepaliDate()}' Voucher_Miti, GETDATE() Voucher_Time, ISNULL(p.PL_Closing, {ObjGlobal.StockStockInHandLedgerId}) Ledger_ID, ISNULL(p.PL_Closing, {ObjGlobal.StockStockInHandLedgerId}) CbLedger_ID, {ObjGlobal.SysCurrencyId}, 1, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) StockValue, 0, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) LocalStockValue, 0, '{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId}
				FROM AMS.StockDetails sd
						INNER JOIN AMS.Product p ON p.PID=sd.Product_Id
				WHERE Voucher_Date <'{GetReports.FromDate.GetSystemDate()}' AND sd.Branch_Id= {ObjGlobal.SysBranchId}
				GROUP BY p.PL_Closing
				HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) > 0
				UNION ALL
				SELECT 'PLCS' Module, ROW_NUMBER() OVER (ORDER BY p.PL_Closing) Serial_No, N'00000' Voucher_No, '{GetReports.ToDate.GetSystemDate()}' Voucher_Date, N'{GetReports.ToDate.GetNepaliDate()}' Voucher_Miti, GETDATE() Voucher_Time, ISNULL(p.PL_Closing, {ObjGlobal.StockStockInHandLedgerId}) Ledger_ID, ISNULL(p.PL_Closing, {ObjGlobal.StockStockInHandLedgerId}) CbLedger_ID, {ObjGlobal.SysCurrencyId}, 1, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) StockValue, 0, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) LocalStockValue, 0, '{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId}
				FROM AMS.StockDetails sd
						INNER JOIN AMS.Product p ON p.PID=sd.Product_Id
				WHERE Voucher_Date BETWEEN  '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND sd.Branch_Id= {ObjGlobal.SysBranchId}
				GROUP BY p.PL_Closing
				HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) > 0";
            var result1 = SqlExtensions.ExecuteNonQuery(stockValue);
        }

        cmdString = GetProfitAndLossPeriodicReportScript();
        var dtFinance = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var deleteProfit = "DELETE AMS.AccountDetails WHERE Module='PLCS' AND Voucher_No=N'00000'";
        SqlExtensions.ExecuteNonQuery(deleteProfit);
        if (dtFinance.RowsCount() is 0)
        {
            return dtReport;
        }

        dtReport = GetReports.FilterFor switch
        {
            "Ledger" => ReturnPeriodicProfitLossLedgerReport(dtFinance),
            "Ledger/SubLedger" => ReturnPeriodicProfitLossLedgerIncludeSubLedgerReport(dtFinance),
            "Account Group" => ReturnPeriodicProfitLossLedgerAccountGroupOnlyReport(dtFinance),
            "Account Group/Ledger" => ReturnPeriodicProfitLossLedgerAccountGroupWiseReport(dtFinance),
            "Account Group/Sub Group" => ReturnPeriodicProfitLossLedgerAccountSubGroupOnlyReport(dtFinance),
            "Account Group/Sub Group/Ledger" => ReturnPeriodicProfitLossLedgerAccountSubGroupWiseReport(dtFinance),
            _ => new DataTable("Blank")
        };
        return dtReport;
    }

    #endregion **----- PERODIC REPORT -----**

    #endregion --------------- PROFIT & LOSS ---------------

    // BALANCE SHEET REPORT

    #region --------------- BALANCE SHEET ---------------

    // NORMAL BALANCE SHEET
    private DataTable GetProfitLossBalanceSheetReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dt_LedgerId",
            "dt_ShortName",
            "dt_Desc",
            "dt_Amount",
            "dt_Type"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnBalanceSheetLedgerReport(DataTable dtBalance)
    {
        DataRow newRow;
        var dtReport = dtBalance.Clone();
        var dtGroupType = dtBalance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var details = dtBalance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in details.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["LedgerId"] = item["LedgerId"].ToString();
                newRow["ShortName"] = item["ShortName"].ToString();
                newRow["LedgerName"] = "          " + item["LedgerName"];
                newRow["Balance"] = item["Balance"].ToString();
                newRow["BalanceType"] = item["BalanceType"].GetUpper();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["LedgerName"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["Balance"] = details.AsEnumerable().Sum(x => x["ActualBalance"].GetDecimal()).GetDecimalComma();
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var liabAmount = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "LIABILITIES");
        var astsAmount = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "ASSETS");

        var creditSum = liabAmount.Sum(row => row["ActualBalance"].GetDecimal());
        var debitSum = astsAmount.Sum(row => row["ActualBalance"].GetDecimal());

        var Difference = debitSum - creditSum;
        if (Math.Abs(Difference) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = Difference switch
            {
                > 0 when GetReports.IsClosingStock => "DIFFERENCE IN ASSETS INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "DIFFERENCE IN ASSETS EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES NCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES EXCLUDING CLOSING STOCK",
                _ => "DIFFERENCE BALANCE"
            };
            newRow["Balance"] = Math.Abs(Difference).GetDecimalComma();
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnBalanceSheetLedgerIncludeSubLedgerReport(DataTable dtBalance)
    {
        DataRow newRow;
        var dtReport = GetProfitLossBalanceSheetReportFormat();
        var dtGroupType = dtBalance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var details = dtBalance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in details.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "          " + item["GLName"];
                newRow["dt_Amount"] = item["Amount"].ToString();
                newRow["dt_Type"] = roType["AmountType"].GetUpper();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["dt_Amount"] = details.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var creditSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "LIABILITIES")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var debitSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "ASSETS")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var Difference = debitSum - creditSum;
        if (Math.Abs(Difference) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = Difference switch
            {
                > 0 when GetReports.IsClosingStock => "DIFFERENCE IN ASSETS INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "DIFFERENCE IN ASSETS EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES NCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES EXCLUDING CLOSING STOCK",
                _ => "DIFFERENCE BALANCE"
            };
            newRow["dt_Amount"] = Math.Abs(Difference);
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnBalanceSheetLedgerAccountGroupOnlyReport(DataTable dtBalance)
    {
        DataRow newRow;
        var dtReport = GetProfitLossBalanceSheetReportFormat();
        var dtGroupType = dtBalance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var details = dtBalance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroup = details.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpName"))
                .CopyToDataTable();
            foreach (DataRow roGroup in dtGroup.Rows)
            {
                var dtGroupDetails = details.Select($"GrpName = '{roGroup["GrpName"]}'").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_ShortName"] = roGroup["GrpCode"].ToString().ToUpper();
                newRow["dt_Desc"] = roGroup["GrpName"].ToString().ToUpper();
                newRow["dt_Amount"] = dtGroupDetails.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["dt_Amount"] = details.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var creditSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "LIABILITIES")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var debitSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "ASSETS")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var Difference = debitSum - creditSum;
        if (Math.Abs(Difference) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = Difference switch
            {
                > 0 when GetReports.IsClosingStock => "DIFFERENCE IN ASSETS INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "DIFFERENCE IN ASSETS EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES NCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES EXCLUDING CLOSING STOCK",
                _ => "DIFFERENCE BALANCE"
            };
            newRow["dt_Amount"] = Math.Abs(Difference);
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnBalanceSheetLedgerAccountGroupWiseReport(DataTable dtBalance)
    {
        DataRow newRow;
        var dtReport = dtBalance.Clone();
        var dtGroupType = dtBalance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var detailsGroup = dtBalance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();

            var dtGroupBy = detailsGroup.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpName"))
                .CopyToDataTable();
            foreach (DataRow groupRow in dtGroupBy.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["ShortName"] = groupRow["GrpCode"].ToString();
                newRow["LedgerName"] = groupRow["GrpName"].GetUpper();
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var details = detailsGroup.Select($"GrpName = '{groupRow["GrpName"]}'").CopyToDataTable();

                foreach (DataRow item in details.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["LedgerId"] = item["LedgerId"].ToString();
                    newRow["ShortName"] = item["ShortName"].ToString();
                    newRow["LedgerName"] = "          " + item["LedgerName"];
                    newRow["Balance"] = item["Balance"].ToString();
                    newRow["BalanceType"] = item["BalanceType"].GetUpper();
                    newRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["LedgerName"] = $"[{groupRow["GrpName"].ToString().ToUpper()}] TOTAL :- ";
                newRow["Balance"] = details.AsEnumerable().Sum(x => x["Balance"].GetDecimal()).GetDecimalComma();
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["LedgerName"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["Balance"] = detailsGroup.AsEnumerable().Sum(x => x["Balance"].GetDecimal()).GetDecimalComma();
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var creditSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "LIABILITIES")
            .Sum(row => row["Balance"].GetDecimal());
        var debitSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "ASSETS")
            .Sum(row => row["Balance"].GetDecimal());
        var Difference = debitSum - creditSum;
        if (Math.Abs(Difference) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = Difference switch
            {
                > 0 when GetReports.IsClosingStock => "DIFFERENCE IN ASSETS INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "DIFFERENCE IN ASSETS EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES NCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES EXCLUDING CLOSING STOCK",
                _ => "DIFFERENCE BALANCE"
            };
            newRow["Balance"] = Math.Abs(Difference).GetDecimalComma();
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnBalanceSheetLedgerAccountSubGroupOnlyReport(DataTable dtBalance)
    {
        DataRow newRow;
        var dtReport = GetProfitLossBalanceSheetReportFormat();
        var dtGroupType = dtBalance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var details = dtBalance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            foreach (DataRow item in details.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "          " + item["GLName"];
                newRow["dt_Amount"] = item["Amount"].ToString();
                newRow["dt_Type"] = roType["AmountType"].GetUpper();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["dt_Amount"] = details.AsEnumerable().Sum(x => x["ActualAmount"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var creditSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "LIABILITIES")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var debitSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "ASSETS")
            .Sum(row => row["ActualAmount"].GetDecimal());
        var Difference = debitSum - creditSum;
        if (Math.Abs(Difference) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = Difference switch
            {
                > 0 when GetReports.IsClosingStock => "DIFFERENCE IN ASSETS INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "DIFFERENCE IN ASSETS EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES NCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES EXCLUDING CLOSING STOCK",
                _ => "DIFFERENCE BALANCE"
            };
            newRow["dt_Amount"] = Math.Abs(Difference);
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnBalanceSheetLedgerAccountSubGroupWiseReport(DataTable dtBalance)
    {
        DataRow newRow;
        var dtReport = dtBalance.Clone();
        var dtGroupType = dtBalance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var detailsGroup = dtBalance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();

            var dtGroupBy = detailsGroup.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpName"))
                .CopyToDataTable();
            foreach (DataRow groupRow in dtGroupType.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["ShortName"] = groupRow["GrpCode"].ToString();
                newRow["LedgerName"] = groupRow["GrpName"].GetUpper();
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var detailsSubGroup = detailsGroup.Select($"GrpName = '{groupRow["GrpName"]}'").CopyToDataTable();

                var dtSubGroup = detailsSubGroup.AsEnumerable().GroupBy(row => new
                {
                    grpType = row.Field<string>("GrpName")
                }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpName"))
                    .CopyToDataTable();

                foreach (DataRow subRow in dtSubGroup.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["ShortName"] = groupRow["SubGrpCode"].ToString();
                    newRow["LedgerName"] = groupRow["SubGrpName"].GetUpper();
                    newRow["IsGroup"] = 3;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var details = detailsSubGroup
                        .Select($"GrpName = '{groupRow["GrpName"]}' AND SubGrpName= '{subRow["SubGrpName"]}'")
                        .CopyToDataTable();

                    foreach (DataRow item in details.Rows)
                    {
                        newRow = dtReport.NewRow();
                        newRow["LedgerId"] = item["LedgerId"].ToString();
                        newRow["ShortName"] = item["ShortName"].ToString();
                        newRow["LedgerName"] = "          " + item["LedgerName"];
                        newRow["Balance"] = item["Balance"].ToString();
                        newRow["BalanceType"] = item["BalanceType"].GetUpper();
                        newRow["IsGroup"] = 0;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                    }

                    newRow = dtReport.NewRow();
                    newRow["LedgerName"] = $"[{subRow["SubGrpName"].ToString().ToUpper()}] TOTAL :- ";
                    newRow["Balance"] = details.AsEnumerable().Sum(x => x["Balance"].GetDecimal()).GetDecimalComma();
                    newRow["IsGroup"] = 22;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["LedgerName"] = $"[{groupRow["GrpName"].ToString().ToUpper()}] TOTAL :- ";
                newRow["Balance"] = detailsGroup.AsEnumerable().Sum(x => x["Balance"].GetDecimal()).GetDecimalComma();
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["LedgerName"] = $"[{roType["GrpType"].ToString().ToUpper()}] TOTAL :- ";
            newRow["Balance"] = detailsGroup.AsEnumerable().Sum(x => x["Balance"].GetDecimal()).GetDecimalComma();
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var creditSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "LIABILITIES")
            .Sum(row => row["Balance"].GetDecimal());
        var debitSum = dtBalance.AsEnumerable().Where(row => row.Field<string>("GrpType") is "ASSETS")
            .Sum(row => row["Balance"].GetDecimal());
        var Difference = debitSum - creditSum;
        if (Math.Abs(Difference) > 0)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerName"] = Difference switch
            {
                > 0 when GetReports.IsClosingStock => "DIFFERENCE IN ASSETS INCLUDE CLOSING STOCK",
                > 0 when !GetReports.IsClosingStock => "DIFFERENCE IN ASSETS EXCLUDING CLOSING STOCK",
                < 0 when GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES NCLUDE CLOSING STOCK",
                < 0 when !GetReports.IsClosingStock => "DIFFERENCE IN LIABILITIES EXCLUDING CLOSING STOCK",
                _ => "DIFFERENCE BALANCE"
            };
            newRow["Balance"] = Math.Abs(Difference).GetDecimalComma();
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private string GetBalanceSheetNormalReportScript()
    {
        var cmdString = string.Empty;
        if (GetReports.IsSubLedger)
        {
            cmdString = $@"
				WITH FinanceReport
				  AS
				  (
					SELECT OBS.Ledger_ID,OBS.Subleder_ID, OBS.Amount,OBS.AmountType
					 FROM (
							SELECT ad.Ledger_ID,ad.Subleder_ID, CASE WHEN AG.GrpType IN ('A','Assets') THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) END Amount,
							CASE WHEN AG.GrpType IN ('A','Assets') AND SUM(ad.LocalDebit_Amt -ad.LocalCredit_Amt) > 0 THEN 'Dr'
							WHEN AG.GrpType IN ('A','Assets') AND SUM(ad.LocalDebit_Amt -ad.LocalCredit_Amt) > 0 THEN 'Cr'
							WHEN AG.GrpType IN ('L','Liabilities', 'Liability') AND SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN 'Cr'
							WHEN AG.GrpType IN ('L','Liabilities', 'Liability') AND SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) < 0 THEN 'Dr'
							ELSE '' END  AmountType
							 FROM AMS.AccountDetails ad
								LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ad.Ledger_ID
								LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
							 WHERE ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND ag.PrimaryGrp IN ('BS','Balance Sheet') ";
            cmdString += !GetReports.IsIncludePdc ? " AND ad.Module NOT IN ('PDC','PROV')" : string.Empty;
            cmdString += @"
							 GROUP BY ad.Ledger_ID, ad.Subleder_ID,AG.GrpType
							HAVING  SUM(ad.LocalDebit_Amt -ad.LocalCredit_Amt) <> 0
						  ) OBS
				  )
				 SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName,fr.Subleder_ID,ISNULL(sl.SLName,'NO-SUBLEDGER')SLName, sl.SLCode,gl.GrpId, GrpName, ag.GrpCode, CASE WHEN ag.GrpType ='L' THEN 'LIABILITIES' WHEN ag.GrpType ='A' THEN 'ASSETS' ELSE ag.GrpType END GrpType, ISNULL ( gl.SubGrpId, 0 ) SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUBGROUP' ) SubGrpName, asg.SubGrpCode,ABS ( fr.Amount ) Amount, fr.Amount ActualAmount, fr.AmountType
				  FROM FinanceReport fr
					   LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
					   LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					   LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
					   LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId = fr.Subleder_ID ";
            cmdString += GetReports.SortOn.Equals("SCHEDULE")
                ? "ORDER BY ag.GrpType DESC,ag.Schedule, gl.GLName;"
                : "ORDER BY ag.GrpType DESC, gl.GLName;";
        }
        else
        {
            cmdString = $@"
				WITH BalanceSheet AS (	SELECT gl.GLID Ledger_ID,SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) Balance FROM AMS.AccountDetails ad
	                                         LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_Id = gl.GLID
	                                         LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
                                        WHERE ag.GrpType = 'A' AND ad.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}'
                                        GROUP BY gl.GLID
										HAVING SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) <> 0
                                        UNION ALL
                                        SELECT gl.GLID Ledger_ID,SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) Balance FROM AMS.AccountDetails ad
	                                         LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
	                                         LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
                                        WHERE ag.GrpType = 'L' AND ad.Voucher_Date <= '{GetReports.ToDate.GetSystemDate()}'   ";
            cmdString += $@"
                                        GROUP BY gl.GLID
                                        HAVING SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) <> 0
										UNION ALL
										SELECT {ObjGlobal.FinanceProfitLossLedgerId} LedgerId, ISNULL(SUM(ad1.LocalDebit_Amt-ad1.LocalCredit_Amt), 0)+(SELECT ISNULL(SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END), 0) Balance FROM AMS.StockDetails sd WHERE sd.Voucher_Date<='{GetReports.FromDate.GetSystemDate()}')-(SELECT ISNULL(SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END), 0) Balance FROM AMS.StockDetails sd WHERE sd.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}') Balance
										FROM AMS.AccountDetails ad1
											LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=ad1.Ledger_ID
											LEFT OUTER JOIN AMS.AccountGroup ag1 ON ag1.GrpId=pl.GrpId
										WHERE ag1.PrimaryGrp IN ('PL', 'Profit & Loss', 'TA', 'Trading Account')AND ad1.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}' AND ad1.FiscalYearId={ObjGlobal.SysFiscalYearId}
										UNION ALL
										SELECT {ObjGlobal.StockStockInHandLedgerId} LedgerId, ISNULL(SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END), 0) Balance
										FROM AMS.StockDetails sd
										WHERE sd.Voucher_Date<='{GetReports.ToDate.GetSystemDate()}')
					SELECT bs.Ledger_ID LedgerId, gl.GLCode ShortName, gl.GLName LedgerName,CASE WHEN ag.GrpType = 'A' THEN 'ASSETS' ELSE 'LIABILITIES' END GrpType,ag.GrpName,ag.GrpCode,asg.SubGrpCode,ISNULL(asg.SubGrpName,'NO - SUB GROUP') SubGrpName,FORMAT(ABS(ISNULL(bs.Balance,0)),'{ObjGlobal.SysAmountCommaFormat}') Balance,FORMAT((ISNULL(bs.Balance,0)),'##,##,##0.00') ActualBalance,
                    CASE WHEN ISNULL(bs.Balance,0) > 0  AND ag.GrpType ='L' THEN 'Cr' 
                    WHEN ISNULL(bs.Balance,0)  < 0  AND ag.GrpType ='L' THEN 'Dr' 
                    WHEN ISNULL(bs.Balance,0)  > 0  AND ag.GrpType ='A' THEN 'Dr' 
                    WHEN ISNULL(bs.Balance,0) < 0 AND ag.GrpType ='A' THEN 'Cr' ELSE '' END BalanceType, 0 IsGroup
					FROM BalanceSheet bs
						LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=bs.Ledger_ID
						LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
						LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
					ORDER BY GrpType DESC,gl.GLName; ";
        }

        return cmdString;
    }

    public DataTable GetNormalBalanceSheetReport()
    {
        var dtReport = new DataTable();
        try
        {
            if (GetReports.IsClosingStock)
            {
                if (GetReports.IsRePostValue)
                {
                    var pro = "AMS.USP_PostStockValue";
                    var proResult = SqlExtensions.ExecuteNonQuery(pro, new SqlParameter("@PCode", string.Empty));
                }
            }

            var cmdString = GetBalanceSheetNormalReportScript();
            var resultTables = SqlExtensions.ExecuteDataSet(cmdString);

            if (resultTables != null && resultTables.Tables.Count > 0)
            {
                var dtBalance = resultTables.Tables[0];
                dtReport = GetReports.FilterFor switch
                {
                    "Ledger" => ReturnBalanceSheetLedgerReport(dtBalance),
                    "Ledger/SubLedger" => ReturnBalanceSheetLedgerReport(dtBalance),
                    "Account Group" => ReturnBalanceSheetLedgerAccountGroupOnlyReport(dtBalance),
                    "Account Group/Ledger" => ReturnBalanceSheetLedgerAccountGroupWiseReport(dtBalance),
                    "Account Group/Sub Group" => ReturnBalanceSheetLedgerAccountSubGroupOnlyReport(dtBalance),
                    "Account Group/Sub Group/Ledger" => ReturnBalanceSheetLedgerAccountSubGroupWiseReport(dtBalance),
                    _ => dtReport
                };
            }

            return dtReport;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
            return dtReport;
        }
    }

    //PERIODIC BALANCE SHEET
    private DataTable ReturnPeriodicBalanceSheetLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            foreach (var item in dtFinance.Select($"GrpType = '{roType["GrpType"]}'"))
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = item["ShortName"].ToString();
                newRow["dt_Desc"] = "         " + item["GLName"];

                var _Opening = item["Opening"].GetDecimal();
                var _PrDebit = item["PrDebitAmt"].GetDecimal();
                var _PrCredit = item["PrCreditAmt"].GetDecimal();
                var balance = item["Balance"].GetDecimal();
                newRow["dt_Opening"] = _Opening;
                newRow["dt_PeriodicDebit"] = _PrDebit;
                newRow["dt_PeriodicCredit"] = _PrCredit;
                newRow["dt_Balance"] = Math.Abs(balance);
                newRow["dt_ActualBalance"] = balance;
                newRow["dt_Type"] = item["BalanceType"].ToString();
                newRow["IsGroup"] = 0;
                newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var result = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities");

        var lOpening = result.Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = result.Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = result.Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = result.Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = result.Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = result.Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = result.Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = result.Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "DIFFERENCE IN BALANCE SHEET :- ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicBalanceSheetLedgerIncludeSubLedgerReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();

        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].GetUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupTypeDetails = dtFinance.Select($"GrpType='{roType["GrpType"]}'").CopyToDataTable();
            var dtLedgerGroup = dtGroupTypeDetails.AsEnumerable().GroupBy(row => new
            {
                ledger = row.Field<string>("GLName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GLName")).CopyToDataTable();

            foreach (DataRow roLedger in dtLedgerGroup.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roLedger["Ledger_ID"].ToString();
                newRow["dt_ShortName"] = roLedger["ShortName"].ToString();
                newRow["dt_Desc"] = "    " + roLedger["GLName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                var dtLedgerDetails = dtGroupTypeDetails.Select($"GLName='{roLedger["GLName"]}'").CopyToDataTable();
                foreach (DataRow item in dtLedgerDetails.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_LedgerId"] = item["Subleder_ID"].ToString();
                    newRow["dt_ShortName"] = item["SLCode"].ToString();
                    newRow["dt_Desc"] = "         " + item["SLName"];

                    var _Opening = item["Opening"].GetDecimal();
                    var _PrDebit = item["PrDebitAmt"].GetDecimal();
                    var _PrCredit = item["PrCreditAmt"].GetDecimal();
                    var balance = item["Balance"].GetDecimal();
                    newRow["dt_Opening"] = _Opening;
                    newRow["dt_PeriodicDebit"] = _PrDebit;
                    newRow["dt_PeriodicCredit"] = _PrCredit;
                    newRow["dt_Balance"] = Math.Abs(balance);
                    newRow["dt_ActualBalance"] = balance;
                    newRow["dt_Type"] = item["BalanceType"].ToString();
                    newRow["IsGroup"] = 0;
                    newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = $"{roLedger["GLName"].GetUpper()} TOTAL :- ";
                newRow["dt_Opening"] = dtLedgerDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtLedgerDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                newRow["dt_PeriodicCredit"] =
                    dtLedgerDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                newRow["dt_Balance"] = dtLedgerDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "DIFFERENCE IN BALANCE SHEET :- ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicBalanceSheetLedgerAccountGroupOnlyReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroupTypeDetails = dtFinance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroup = dtGroupTypeDetails.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();
            foreach (DataRow roGroup in dtGroupType.Rows)
            {
                var dtGroupDetails = dtGroupTypeDetails.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
                newRow["dt_ShortName"] = roGroup["ShortName"].ToString();
                newRow["dt_Desc"] = "  " + roGroup["GrpName"];
                newRow["dt_Opening"] = dtGroupDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                newRow["dt_Balance"] = dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "DIFFERENCE IN BALANCE SHEET :- ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicBalanceSheetLedgerAccountGroupWiseReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroupTypeDetails = dtFinance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroup = dtGroupTypeDetails.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();
            foreach (DataRow roGroup in dtGroup.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
                newRow["dt_ShortName"] = roGroup["ShortName"].ToString();
                newRow["dt_Desc"] = "  " + roGroup["GrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                var dtGroupDetails = dtGroupTypeDetails.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
                foreach (DataRow item in dtGroupDetails.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                    newRow["dt_ShortName"] = item["ShortName"].ToString();
                    newRow["dt_Desc"] = "         " + item["GLName"];

                    var _Opening = item["Opening"].GetDecimal();
                    var _PrDebit = item["PrDebitAmt"].GetDecimal();
                    var _PrCredit = item["PrCreditAmt"].GetDecimal();
                    var _balance = item["Balance"].GetDecimal();

                    newRow["dt_Opening"] = _Opening;
                    newRow["dt_PeriodicDebit"] = _PrDebit;
                    newRow["dt_PeriodicCredit"] = _PrCredit;
                    newRow["dt_Balance"] = Math.Abs(_balance);
                    newRow["dt_ActualBalance"] = _balance;
                    newRow["dt_Type"] = item["BalanceType"].ToString();
                    newRow["IsGroup"] = 0;
                    newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = $"{roGroup["GrpName"].GetUpper()} TOTAL :- ";
                newRow["dt_Opening"] = dtGroupDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                newRow["dt_Balance"] = dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "DIFFERENCE IN BALANCE SHEET :- ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicBalanceSheetLedgerAccountSubGroupOnlyReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroupTypeDetails = dtFinance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroup = dtGroupTypeDetails.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();
            foreach (DataRow roGroup in dtGroup.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
                newRow["dt_ShortName"] = roGroup["ShortName"].ToString();
                newRow["dt_Desc"] = "  " + roGroup["GrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                var dtGroupDetails = dtGroupTypeDetails.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
                var dtSubGroup = dtGroupDetails.AsEnumerable().GroupBy(row => new
                {
                    grpType = row.Field<string>("SubGrpName")
                }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("SubGrpName"))
                    .CopyToDataTable();
                foreach (DataRow roSub in dtSubGroup.Rows)
                {
                    var dtSubGroupDetails = dtGroupDetails
                        .Select($"GrpName='{roGroup["GrpName"]}' and SubGrpName='{roSub["SubGrpName"]}'")
                        .CopyToDataTable();
                    newRow["dt_LedgerId"] = roGroup["SubGrpId"].ToString();
                    newRow["dt_ShortName"] = roGroup["SubGrpCode"].ToString();
                    newRow["dt_Desc"] = "    " + roGroup["SubGrpName"];
                    newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                    newRow["dt_Desc"] = $"{roGroup["GrpName"].GetUpper()} TOTAL :- ";
                    newRow["dt_Opening"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                    newRow["dt_PeriodicDebit"] =
                        dtSubGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                    newRow["dt_PeriodicCredit"] =
                        dtSubGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                    newRow["dt_Balance"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                    newRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = $"{roGroup["GrpName"].GetUpper()} TOTAL :- ";
                newRow["dt_Opening"] = dtGroupDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                newRow["dt_Balance"] = dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "DIFFERENCE IN BALANCE SHEET :- ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private DataTable ReturnPeriodicBalanceSheetLedgerAccountSubGroupWiseReport(DataTable dtFinance)
    {
        DataRow newRow;
        var dtReport = GetPeriodicProfitLossAndBalanceSheetReportFormat();
        var dtGroupType = dtFinance.AsEnumerable().GroupBy(row => new
        {
            grpType = row.Field<string>("GrpType")
        }).Select(rows => rows.FirstOrDefault()).OrderByDescending(row => row.Field<string>("GrpType"))
            .CopyToDataTable();
        foreach (DataRow roType in dtGroupType.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GrpType"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroupTypeDetails = dtFinance.Select($"GrpType = '{roType["GrpType"]}'").CopyToDataTable();
            var dtGroup = dtGroupTypeDetails.AsEnumerable().GroupBy(row => new
            {
                grpType = row.Field<string>("GrpName")
            }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("GrpName")).CopyToDataTable();
            foreach (DataRow roGroup in dtGroup.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
                newRow["dt_ShortName"] = roGroup["ShortName"].ToString();
                newRow["dt_Desc"] = "  " + roGroup["GrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                var dtGroupDetails = dtGroupTypeDetails.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
                var dtSubGroup = dtGroupDetails.AsEnumerable().GroupBy(row => new
                {
                    grpType = row.Field<string>("SubGrpName")
                }).Select(rows => rows.FirstOrDefault()).OrderBy(row => row.Field<string>("SubGrpName"))
                    .CopyToDataTable();
                foreach (DataRow roSub in dtSubGroup.Rows)
                {
                    newRow["dt_LedgerId"] = roGroup["SubGrpId"].ToString();
                    newRow["dt_ShortName"] = roGroup["SubGrpCode"].ToString();
                    newRow["dt_Desc"] = "    " + roGroup["SubGrpName"];
                    newRow["IsGroup"] = 3;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var dtSubGroupDetails = dtGroupDetails
                        .Select($"GrpName='{roGroup["GrpName"]}' and SubGrpName='{roSub["SubGrpName"]}'")
                        .CopyToDataTable();

                    foreach (DataRow item in dtSubGroupDetails.Rows)
                    {
                        newRow = dtReport.NewRow();
                        newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                        newRow["dt_ShortName"] = item["ShortName"].ToString();
                        newRow["dt_Desc"] = "         " + item["GLName"];

                        var _Opening = item["Opening"].GetDecimal();
                        var _PrDebit = item["PrDebitAmt"].GetDecimal();
                        var _PrCredit = item["PrCreditAmt"].GetDecimal();
                        var _balance = item["Balance"].GetDecimal();

                        newRow["dt_Opening"] = _Opening;
                        newRow["dt_PeriodicDebit"] = _PrDebit;
                        newRow["dt_PeriodicCredit"] = _PrCredit;
                        newRow["dt_Balance"] = Math.Abs(_balance);
                        newRow["dt_ActualBalance"] = _balance;
                        newRow["dt_Type"] = item["BalanceType"].ToString();
                        newRow["IsGroup"] = 0;
                        newRow["dt_GroupBy"] = roType["GrpType"].ToString();
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                    }

                    newRow = dtReport.NewRow();
                    newRow["dt_Desc"] = $"{roGroup["GrpName"].GetUpper()} TOTAL :- ";
                    newRow["dt_Opening"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                    newRow["dt_PeriodicDebit"] =
                        dtSubGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                    newRow["dt_PeriodicCredit"] =
                        dtSubGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                    newRow["dt_Balance"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                    newRow["IsGroup"] = 33;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = $"{roGroup["GrpName"].GetUpper()} TOTAL :- ";
                newRow["dt_Opening"] = dtGroupDetails.AsEnumerable().Sum(x => x["Opening"].GetDecimal());
                newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrDebitAmt"].GetDecimal());
                newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["PrCreditAmt"].GetDecimal());
                newRow["dt_Balance"] = dtGroupDetails.AsEnumerable().Sum(x => x["Balance"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            var dtGrpTotal = dtReport.Select($"dt_GroupBy ='{roType["GrpType"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = $"{roType["GrpType"].GetUpper()} TOTAL :- ";
            newRow["dt_Opening"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_Opening"].GetDecimal());
            newRow["dt_PeriodicDebit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
            newRow["dt_PeriodicCredit"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
            newRow["dt_Balance"] = dtGrpTotal.AsEnumerable().Sum(x => x["dt_ActualBalance"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var lOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var lDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var lCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var lBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Liabilities")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var aOpening = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_Opening"].GetDecimal());
        var aDebit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicDebit"].GetDecimal());
        var aCredit = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_PeriodicCredit"].GetDecimal());
        var aBalance = dtReport.AsEnumerable().Where(row => row.Field<string>("dt_GroupBy") is "Assets")
            .Sum(row => row["dt_ActualBalance"].GetDecimal());

        var dOpening = lOpening - aOpening;
        var pAmount = lCredit - lDebit - (aDebit - aCredit);
        var pBalance = lBalance - aBalance;

        if (pBalance.GetAbs() > 0 || pAmount.GetAbs() > 0 || dOpening.GetAbs() > 0)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "DIFFERENCE IN BALANCE SHEET :- ";
            newRow["dt_Opening"] = dOpening;
            newRow["dt_PeriodicDebit"] = pAmount < 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_PeriodicCredit"] = pAmount > 0 ? Math.Abs(pAmount) : string.Empty;
            newRow["dt_Balance"] = pBalance;
            newRow["IsGroup"] = 88;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        return dtReport;
    }

    private string GetPeriodicBalanceSheetReportScript()
    {
        var cmdString = string.Empty;
        if (GetReports.IsSubLedger)
        {
            cmdString = @$"
				WITH FinanceReport
				  AS
				  (
					SELECT ProfitLoss.Ledger_ID,ProfitLoss.Subleder_ID, SUM ( ProfitLoss.Opening ) Opening, SUM ( ProfitLoss.PrDebitAmt ) PrDebitAmt, SUM ( ProfitLoss.PrCreditAmt ) PrCreditAmt
					 FROM (
							SELECT Ledger_ID,Subleder_ID, ISNULL ( CASE WHEN Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' THEN SUM ( LocalDebit_Amt - Credit_Amt ) END, 0 ) Opening, 0 PrDebitAmt, 0 PrCreditAmt
							 FROM AMS.AccountDetails
							  WHERE FiscalYearId < {ObjGlobal.SysFiscalYearId} ";
            cmdString += !GetReports.IsIncludePdc ? @" AND Module NOT IN ('PDC', 'PROV')" : " ";
            cmdString = $@"
				GROUP BY Ledger_ID, Voucher_Date,Subleder_ID
							UNION ALL
							SELECT ad.Ledger_ID,ad.Subleder_ID, ISNULL ( CASE WHEN ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' THEN SUM ( LocalDebit_Amt - ad.Credit_Amt ) END, 0 ) Opening, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' THEN SUM ( LocalDebit_Amt ) END, 0 ) PrDebitAmt, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' THEN SUM ( LocalCredit_Amt ) END, 0 ) PrCreditAmt
							 FROM AMS.AccountDetails ad
							 WHERE ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
            cmdString += !GetReports.IsIncludePdc ? @"AND ad.Module NOT IN ('PDC', 'PROV')" : " ";
            cmdString += @"
							 GROUP BY ad.Ledger_ID, ad.Voucher_Date,Subleder_ID
						  ) ProfitLoss
					 GROUP BY ProfitLoss.Ledger_ID,ProfitLoss.Subleder_ID
				  )
				 SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName,ISNULL(fr.Subleder_ID,0) Subleder_ID,ISNULL(sl.SLName,'NO SUBLEDGER') SLName,sl.SLCode, gl.GrpId, GrpName,ag.GrpCode, CASE WHEN ag.GrpType ='L' THEN 'LIABILITIES' WHEN ag.GrpType ='A' THEN 'ASSETS' ELSE ag.GrpType END GrpType, ISNULL ( gl.SubGrpId, 0 ) SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUBGROUP' ) SubGrpName, asg.SubGrpCode, SUM(fr.Opening) Opening, SUM(PrDebitAmt) PrCreditAmt, SUM(PrCreditAmt) PrCreditAmt, CASE WHEN ag.GrpType='Liabilities' THEN SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)ELSE SUM(fr.PrDebitAmt-fr.PrCreditAmt - fr.Opening)END Balance, CASE WHEN ag.GrpType='Liabilities' AND SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)>0 THEN 'Cr' WHEN ag.GrpType='Liabilities' AND SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)<0 THEN 'Dr' WHEN ag.GrpType='Assets' AND SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)>0 THEN 'Dr' WHEN ag.GrpType='Assets' AND SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)<0 THEN 'Cr' ELSE '' END BalanceType
				  FROM FinanceReport fr
					   LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
					   LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					   LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
					   LEFT OUTER JOIN AMS.SubLedger sl ON sl.SLId = fr.Subleder_ID
				  WHERE ag.PrimaryGrp IN ('BS','Balance Sheet')
				  GROUP BY fr.Ledger_ID, fr.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, ag.GrpType, gl.SubGrpId, asg.SubGrpName,fr.Subleder_ID,sl.SLName,sl.SLCode,ag.GrpCode,asg.SubGrpCode
				  ORDER BY ag.GrpType DESC, gl.GLName;";
        }
        else
        {
            cmdString = $@"
				WITH FinanceReport
				  AS
				  (
					SELECT ProfitLoss.Ledger_ID, SUM ( ProfitLoss.Opening ) Opening, SUM ( ProfitLoss.PrDebitAmt ) PrDebitAmt, SUM ( ProfitLoss.PrCreditAmt ) PrCreditAmt
					 FROM (
							SELECT Ledger_ID, ISNULL ( CASE WHEN Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' THEN SUM ( LocalDebit_Amt - Credit_Amt ) END, 0 ) Opening, 0 PrDebitAmt, 0 PrCreditAmt
							 FROM AMS.AccountDetails
							 WHERE FiscalYearId < {ObjGlobal.SysFiscalYearId} ";
            cmdString += !GetReports.IsIncludePdc ? @" AND Module NOT IN ('PDC', 'PROV')" : " ";
            cmdString += $@"
							 GROUP BY Ledger_ID, Voucher_Date
							UNION ALL
							SELECT ad.Ledger_ID, ISNULL ( CASE WHEN ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' THEN SUM ( LocalDebit_Amt - ad.Credit_Amt ) END, 0 ) Opening, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' THEN SUM ( LocalDebit_Amt ) END, 0 ) PrDebitAmt, ISNULL ( CASE WHEN ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' THEN SUM ( LocalCredit_Amt ) END, 0 ) PrCreditAmt
							 FROM AMS.AccountDetails ad
							 WHERE ad.FiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
            cmdString += !GetReports.IsIncludePdc ? @"AND ad.Module NOT IN ('PDC', 'PROV')" : " ";
            cmdString += @"
							 GROUP BY ad.Ledger_ID, ad.Voucher_Date
						  ) ProfitLoss
					 GROUP BY ProfitLoss.Ledger_ID
				  )
				 SELECT fr.Ledger_ID, gl.GLName, gl.GLCode ShortName, gl.GrpId, GrpName,ag.GrpCode, CASE WHEN ag.GrpType ='L' THEN 'LIABILITIES' WHEN ag.GrpType ='A' THEN 'ASSETS' ELSE ag.GrpType END GrpType, ISNULL(gl.SubGrpId, 0) SubGrpId, ISNULL(asg.SubGrpName, 'NO SUBGROUP') SubGrpName,asg.SubGrpCode, SUM(fr.Opening) Opening, SUM(PrDebitAmt) PrDebitAmt, SUM(PrCreditAmt) PrCreditAmt, CASE WHEN ag.GrpType IN ('L','Liabilities') THEN SUM(fr.PrCreditAmt-fr.PrDebitAmt - fr.Opening)ELSE SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)END Balance, CASE WHEN ag.GrpType IN ('L','Liabilities') AND SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)>0 THEN 'Cr' WHEN ag.GrpType IN ('L','Liabilities') AND SUM(fr.PrCreditAmt-fr.PrDebitAmt+fr.Opening)<0 THEN 'Dr' WHEN ag.GrpType IN ('A','Assets') AND SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)>0 THEN 'Dr' WHEN ag.GrpType IN ('A','Assets') AND SUM(fr.PrDebitAmt-fr.PrCreditAmt+fr.Opening)<0 THEN 'Cr' ELSE '' END BalanceType
				  FROM FinanceReport fr
					   LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = fr.Ledger_ID
					   LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					   LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
				  WHERE ag.PrimaryGrp IN ('BS','Balance Sheet')
				  GROUP BY fr.Ledger_ID, fr.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, ag.GrpType, gl.SubGrpId, asg.SubGrpName,ag.GrpCode,asg.SubGrpCode
				  ORDER BY ag.GrpType DESC, gl.GLName; ";
        }

        return cmdString;
    }

    public DataTable GetPeriodicBalanceSheetReport()
    {
        var voucherDate = Convert.ToDateTime(GetReports.FromDate).AddDays(-1);
        if (GetReports.IsClosingStock)
        {
            if (GetReports.IsRePostValue)
            {
                var pro = "AMS.USP_PostStockValue";
                var proResult = SqlExtensions.ExecuteNonQuery(pro, new SqlParameter("@PCode", string.Empty));
            }

            var cmdStock1 = $@"
				DELETE AMS.AccountDetails WHERE Module='PLCS' AND Voucher_No=N'00000';
				INSERT INTO AMS.AccountDetails(Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID, Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt, EnterBy, EnterDate, Branch_ID, FiscalYearId)
				SELECT 'BSCS' Module, ROW_NUMBER() OVER (ORDER BY p.BS_Closing) Serial_No, N'00000' Voucher_No, '{GetReports.FromDate.GetSystemDate()}' Voucher_Date, N'{GetReports.FromDate.GetNepaliDate()}' Voucher_Miti, GETDATE() Voucher_Time, ISNULL(p.BS_Closing, {ObjGlobal.StockStockInHandLedgerId}) Ledger_ID, ISNULL(p.BS_Closing, {ObjGlobal.StockStockInHandLedgerId}) CbLedger_ID, {ObjGlobal.SysCurrencyId}, 1, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) StockValue, 0, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) LocalStockValue, 0, '{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId}
				FROM AMS.StockDetails sd
						INNER JOIN AMS.Product p ON p.PID=sd.Product_Id
				WHERE Voucher_Date <'{GetReports.FromDate.GetSystemDate()}' AND sd.Branch_Id= {ObjGlobal.SysBranchId}
				GROUP BY p.BS_Closing
				HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) > 0
				UNION ALL
				SELECT 'BSCS' Module, ROW_NUMBER() OVER (ORDER BY p.BS_Closing) Serial_No, N'00000' Voucher_No, '{GetReports.ToDate.GetSystemDate()}' Voucher_Date, N'{GetReports.ToDate.GetNepaliDate()}' Voucher_Miti, GETDATE() Voucher_Time, ISNULL(p.BS_Closing, {ObjGlobal.StockStockInHandLedgerId}) Ledger_ID, ISNULL(p.BS_Closing, {ObjGlobal.StockStockInHandLedgerId}) CbLedger_ID, {ObjGlobal.SysCurrencyId}, 1, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) StockValue, 0, SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) LocalStockValue, 0, '{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId}
				FROM AMS.StockDetails sd
						INNER JOIN AMS.Product p ON p.PID=sd.Product_Id
				WHERE Voucher_Date BETWEEN  '{GetReports.FromDate.GetSystemDate()}'  AND '{GetReports.ToDate.GetSystemDate()}' AND sd.Branch_Id= {ObjGlobal.SysBranchId}
				GROUP BY p.BS_Closing
				HAVING SUM(CASE WHEN EntryType='I' THEN StockVal ELSE -StockVal END) > 0";
            var result1 = SqlExtensions.ExecuteNonQuery(cmdStock1);
        }

        var cmdProfit1 =
            $"SELECT SUM(sd.LocalCredit_Amt - sd.LocalDebit_Amt) StockValue FROM AMS.AccountDetails sd LEFT OUTER JOIN AMS.GeneralLedger gl ON sd.Ledger_ID = gl.GLID LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId WHERE ag.PrimaryGrp='Profit & Loss' AND  sd.Voucher_Date < '{Convert.ToDateTime(GetReports.FromDate).ToString("yyyy-MM-dd")}'";
        var profit1 = cmdProfit1.GetQueryData().GetDecimal();
        var cmdProfit2 =
            $"SELECT SUM(sd.LocalCredit_Amt - sd.LocalDebit_Amt) StockValue FROM AMS.AccountDetails sd LEFT OUTER JOIN AMS.GeneralLedger gl ON sd.Ledger_ID = gl.GLID LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId WHERE ag.PrimaryGrp='Profit & Loss' AND  sd.Voucher_Date BETWEEN '{Convert.ToDateTime(GetReports.FromDate).ToString("yyyy-MM-dd")}' AND '{Convert.ToDateTime(GetReports.ToDate).ToString("yyyy-MM-dd")}'";
        var profit2 = cmdProfit2.GetQueryData().GetDecimal();

        var updateProfit = $@"
			DELETE AMS.AccountDetails WHERE Module='OPL' AND Voucher_No = N'00000' AND Ledger_ID='{ObjGlobal.FinanceProfitLossLedgerId}';
			INSERT INTO AMS.AccountDetails (Module, Serial_No, Voucher_No, Voucher_Date, Voucher_Miti, Voucher_Time, Ledger_ID, CbLedger_ID,Currency_ID, Currency_Rate, Debit_Amt, Credit_Amt, LocalDebit_Amt, LocalCredit_Amt,EnterBy, EnterDate, Branch_ID, FiscalYearId)
			VALUES ('OPL', 1, N'00000', '{voucherDate:yyyy-MM-dd}', N'{ObjGlobal.ReturnNepaliDate(voucherDate.ToString())}', GETDATE(), {ObjGlobal.FinanceProfitLossLedgerId}, {ObjGlobal.FinanceProfitLossLedgerId}, {ObjGlobal.SysCurrencyId}, 1,";
        updateProfit += profit1 < 0 ? $" {Math.Abs(profit1)}," : "0,";
        updateProfit += profit1 > 0 ? $" {profit1}," : "0,";
        updateProfit += profit1 < 0 ? $" {Math.Abs(profit1)}," : "0,";
        updateProfit += profit1 > 0 ? $" {profit1}," : "0,";
        updateProfit += $"'{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId}), ";

        updateProfit +=
            $" ('OPL', 2, N'00000', '{ObjGlobal.CfStartAdDate.GetSystemDate()}', N'{ObjGlobal.CfStartBsDate}', GETDATE(), {ObjGlobal.FinanceProfitLossLedgerId}, {ObjGlobal.FinanceProfitLossLedgerId}, {ObjGlobal.SysCurrencyId}, 1,";
        updateProfit += profit2 < 0 ? $" {Math.Abs(profit2)}," : "0,";
        updateProfit += profit2 > 0 ? $" {profit2}," : "0,";
        updateProfit += profit2 < 0 ? $" {Math.Abs(profit2)}," : "0,";
        updateProfit += profit2 > 0 ? $" {profit2}," : "0,";
        updateProfit += $"'{ObjGlobal.LogInUser}',GETDATE(),{ObjGlobal.SysBranchId},{ObjGlobal.SysFiscalYearId}); ";
        var result = SqlExtensions.ExecuteNonQuery(updateProfit);

        var cmdString = GetPeriodicBalanceSheetReportScript();
        var dtFinance = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        var deleteProfit = $@"
			DELETE AMS.AccountDetails WHERE Module='OPL' AND Voucher_No = N'00000' AND Ledger_ID='{ObjGlobal.FinanceProfitLossLedgerId}';
			DELETE AMS.AccountDetails WHERE Module='PLCS' AND Voucher_No = N'00000' AND Ledger_ID='{ObjGlobal.StockStockInHandLedgerId}'";
        SqlExtensions.ExecuteNonQuery(deleteProfit);
        if (dtFinance.Rows.Count is 0)
        {
            return new DataTable();
        }

        var dtReport = GetReports.FilterFor switch
        {
            "Ledger" => ReturnPeriodicBalanceSheetLedgerReport(dtFinance),
            "Ledger/SubLedger" => ReturnPeriodicBalanceSheetLedgerIncludeSubLedgerReport(dtFinance),
            "Account Group" => ReturnPeriodicBalanceSheetLedgerAccountGroupOnlyReport(dtFinance),
            "Account Group/Ledger" => ReturnPeriodicBalanceSheetLedgerAccountGroupWiseReport(dtFinance),
            "Account Group/Sub Group" => ReturnPeriodicBalanceSheetLedgerAccountSubGroupOnlyReport(dtFinance),
            "Account Group/Sub Group/Ledger" => ReturnPeriodicBalanceSheetLedgerAccountSubGroupWiseReport(dtFinance),
            _ => new DataTable("Blank")
        };

        return dtReport;
    }

    #endregion --------------- BALANCE SHEET ---------------

    // PROVISION VOUCHER LIST REPORTS

    #region --------------- PROVISON VOUCHER LIST ---------------

    public DataTable GetProvisionCashBankVoucher()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			SELECT cm.Voucher_No,CASE WHEN (SELECT sc.Date_Type FROM AMS.SystemConfiguration sc) ='M' then cm.Voucher_Miti ELSE CONVERT(varchar(10),cm.Voucher_Date,103) END VoucherDate,gl.GLName CashLedger,gl1.GLName Ledger,CAST(SUM(cd.LocalCredit) AS DECIMAL(18,2)) RECEIPT, CAST(SUM(cd.LocalDebit) AS DECIMAL(18,2)) PAYMENT
			FROM AMS.CB_Details cd
			LEFT OUTER JOIN AMS.CB_Master cm ON cd.Voucher_No = cm.Voucher_No LEFT OUTER JOIN AMS.GeneralLedger gl ON cm.Ledger_ID = gl.GLID LEFT OUTER JOIN AMS.GeneralLedger gl1 ON cd.Ledger_ID = gl1.GLID
			WHERE cm.VoucherMode = 'PROV'
			GROUP BY cm.Voucher_No,cm.Voucher_Miti,cm.Voucher_Date,gl.GLName,gl1.GLName ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable GetProvisionJournalVoucher()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			SELECT jd.Voucher_No,CASE WHEN (SELECT sc.Date_Type FROM AMS.SystemConfiguration sc) ='M' then Voucher_Miti ELSE CONVERT(varchar(10),Voucher_Date,103) END VoucherDate,
			gl.GLName Ledger, CAST(SUM(jd.LocalDebit) AS DECIMAL(18,2)) PAYMENT ,CAST(SUM(jd.LocalCredit) AS DECIMAL(18,2)) RECEIPT FROM AMS.JV_Details jd
			LEFT OUTER JOIN AMS.JV_Master jm ON jd.Voucher_No = jM.Voucher_No
			LEFT OUTER JOIN AMS.GeneralLedger gl ON jd.Ledger_ID = gl.GLID
			WHERE jm.VoucherMode ='PROV'
			GROUP BY jd.Voucher_No,Voucher_Miti,Voucher_Date,gl.GLName
			");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    #endregion --------------- PROVISON VOUCHER LIST ---------------

    // GENERAL LEDGER REPORTS

    #region ---------------  GENERAL LEDGER REPORT ---------------

    // GENERAL LEDGER DETAILS REPORTS

    #region **---------- DETTAILS ----------**

    private DataTable GetLedgerDetailsReportFormat()
    {
        var dtReport = new DataTable();
        dtReport.AddStringColumns(new[]
        {
            "dt_Date",
            "dt_Miti",
            "dt_LedgerId",
            "dt_Desc",
            "dt_Currency",
            "dt_CurrencyRate",
            "dt_Debit",
            "dt_LocalDebit",
            "dt_Credit",
            "dt_LocalCredit",
            "dt_Balance",
            "dt_LocalBalance",
            "dt_AmountType",
            "dt_Module",
            "dt_VoucherNo"
        });
        dtReport.AddColumn("IsGroup", typeof(int));
        return dtReport;
    }

    private DataTable ReturnLedgerDetailsReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        decimal ActualBalance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var ledgerDetails = roType["LedgerDesc"].GetString();
            var exitDataRows = dtLedger.Select($"LedgerDesc = '{ledgerDetails}'");
            if (exitDataRows.Length > 0)
            {
                var dtLedgerDetails = exitDataRows.CopyToDataTable();

                var UpdateLedgerDetails = dtLedgerDetails.Copy();
                var index = 0;
                foreach (DataRow item in dtLedgerDetails.Rows)
                {
                    Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                    ActualBalance = ActualBalance + item["ActualDebitAmount"].GetDecimal() - item["ActualCreditAmount"].GetDecimal();
                    UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance).GetDecimalComma());
                    UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                    UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                    if (item["Cheque_No"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }

                    if (GetReports.IsRemarks)
                    {
                        if (item["Remarks"].IsValueExits())
                        {
                            newRow = UpdateLedgerDetails.NewRow();
                            newRow["PartyLedger"] = item["Remarks"].ToString();
                            newRow["IsGroup"] = 10;
                            UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                            index += 1;
                        }
                    }

                    if (GetReports.IsNarration)
                    {
                        if (item["Narration"].IsValueExits())
                        {
                            newRow = UpdateLedgerDetails.NewRow();
                            newRow["PartyLedger"] = item["Narration"].ToString();
                            newRow["IsGroup"] = 10;
                            UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                            index += 1;
                        }
                    }

                    var vModule = item["Module"].ToString().Trim();
                    //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                    string[] productStrings = { "PB", "SB", "SR", "PR" };

                    if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                    {
                        var cmdString = $@"
							SELECT SPACE(8) + isnull(AMS.ProperCase(PName),'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
							FROM AMS.StockDetails sd
								INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
							WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["VoucherNo"].ToString().Trim()}' ";
                        var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                        if (dtProduct.Rows.Count <= 0)
                        {
                            continue;
                        }

                        foreach (DataRow roProduct in dtProduct.Rows)
                        {
                            newRow = UpdateLedgerDetails.NewRow();
                            newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                            newRow["IsGroup"] = 10;
                            UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                            index += 1;
                        }
                    }

                    index++;
                }

                var collection = UpdateLedgerDetails.AsEnumerable();
                collection.Take(UpdateLedgerDetails.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                newRow = dtReport.NewRow();
                newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";
                newRow["DebitAmount"] = collection.Sum(x => x["DebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["ActualDebitAmount"] = collection.Sum(x => x["ActualDebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["CreditAmount"] = collection.Sum(x => x["CreditAmount"].GetDecimal()).GetDecimalComma();
                newRow["ActualCreditAmount"] = collection.Sum(x => x["ActualCreditAmount"].GetDecimal()).GetDecimalComma();

                var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";
                var resultActual = Math.Abs(ActualBalance) > 0 ? Math.Abs(ActualBalance).ToString() : "NIL";

                newRow["Balance"] = result.GetDecimalComma();
                newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                Balance = 0;
            }
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";

        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var actualDebit = dtGrand.AsEnumerable().Sum(x => x["ActualDebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        var actualCredit = dtGrand.AsEnumerable().Sum(x => x["ActualCreditAmount"].GetDecimal());

        var totalBalance = debit - credit;
        newRow["DebitAmount"] = debit.GetDecimalComma();
        newRow["DebitAmount"] = actualDebit.GetDecimalComma();
        newRow["CreditAmount"] = credit.GetDecimalComma();
        newRow["CreditAmount"] = actualCredit.GetDecimalComma();

        newRow["Balance"] = Math.Abs(totalBalance) > 0 ? Math.Abs(totalBalance).GetDecimalComma() : "NIL";
        newRow["BalanceType"] = totalBalance > 0 ? "Dr" : totalBalance < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerDetailsCurrencyTypeReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        decimal ActualBalance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var ledgerDetails = roType["LedgerDesc"].GetString();
            var exitDataRows = dtLedger.Select($"LedgerDesc = '{ledgerDetails}'");
            if (exitDataRows.Length > 0)
            {
                var dtLedgerDetails = exitDataRows.CopyToDataTable();
                var dtCurrencyType = dtLedgerDetails.AsEnumerable().GroupBy(r => new
                {
                    ledgerDesc = r.Field<string>("CCode")
                }).Select(g => g.First()).OrderBy(r => r.Field<string>("CCode")).CopyToDataTable();

                foreach (DataRow type in dtCurrencyType.Rows)
                {
                    var currency = type["CCode"].ToString();

                    var currencyDetails = dtLedgerDetails.Select($"CCode='{currency}'").CopyToDataTable();
                    var UpdateLedgerDetails = currencyDetails.Copy();
                    var index = 0;
                    foreach (DataRow item in currencyDetails.Rows)
                    {
                        Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                        ActualBalance = ActualBalance + item["ActualDebitAmount"].GetDecimal() -
                                        item["ActualCreditAmount"].GetDecimal();
                        UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance).GetDecimalComma());
                        UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                        UpdateLedgerDetails.Rows[index]
                            .SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                        if (item["Cheque_No"].IsValueExits())
                        {
                            newRow = UpdateLedgerDetails.NewRow();
                            newRow["PartyLedger"] =
                                $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                            newRow["IsGroup"] = 10;
                            UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                            index += 1;
                        }

                        if (GetReports.IsRemarks)
                        {
                            if (item["Remarks"].IsValueExits())
                            {
                                newRow = UpdateLedgerDetails.NewRow();
                                newRow["PartyLedger"] = item["Remarks"].ToString();
                                newRow["IsGroup"] = 10;
                                UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                index += 1;
                            }
                        }

                        if (GetReports.IsNarration)
                        {
                            if (item["Narration"].IsValueExits())
                            {
                                newRow = UpdateLedgerDetails.NewRow();
                                newRow["PartyLedger"] = item["Narration"].ToString();
                                newRow["IsGroup"] = 10;
                                UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                index += 1;
                            }
                        }

                        var vModule = item["Module"].ToString().Trim();
                        //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                        string[] productStrings = { "PB", "SB", "SR", "PR" };

                        if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                        {
                            var cmdString = $@"
							    SELECT SPACE(8) + isnull(AMS.ProperCase(PName),'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
							    FROM AMS.StockDetails sd
								    INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
							    WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["VoucherNo"].ToString().Trim()}' ";
                            var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                            if (dtProduct.Rows.Count <= 0)
                            {
                                continue;
                            }

                            foreach (DataRow roProduct in dtProduct.Rows)
                            {
                                newRow = UpdateLedgerDetails.NewRow();
                                newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                                newRow["IsGroup"] = 10;
                                UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                index += 1;
                            }
                        }

                        index++;
                    }

                    UpdateLedgerDetails.AsEnumerable().Take(UpdateLedgerDetails.Rows.Count)
                        .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                    newRow = dtReport.NewRow();
                    newRow["PartyLedger"] = $"[{type["CCode"].GetUpper()}] TOTAL >>";
                    newRow["DebitAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal())
                        .GetDecimalComma();
                    newRow["ActualDebitAmount"] = UpdateLedgerDetails.AsEnumerable()
                        .Sum(x => x["ActualDebitAmount"].GetDecimal()).GetDecimalComma();
                    newRow["CreditAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal())
                        .GetDecimalComma();
                    newRow["ActualCreditAmount"] = UpdateLedgerDetails.AsEnumerable()
                        .Sum(x => x["ActualCreditAmount"].GetDecimal()).GetDecimalComma();

                    var currencyBalance = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";
                    var currencyActualBalance = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";

                    newRow["Balance"] = currencyBalance.GetDecimalComma();
                    newRow["BalanceType"] = ActualBalance > 0 ? "Dr" : ActualBalance < 0 ? "Cr" : string.Empty;
                    newRow["IsGroup"] = 33;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    Balance = 0;
                    ActualBalance = 0;
                }

                var sumLedger = dtLedgerDetails.AsEnumerable();
                var sumDebit = sumLedger.Sum(x => x["DebitAmount"].GetDecimal());
                var sumActualDebit = sumLedger.Sum(x => x["ActualDebitAmount"].GetDecimal());

                var sumCredit = sumLedger.Sum(x => x["CreditAmount"].GetDecimal());
                var sumActualCredit = sumLedger.Sum(x => x["ActualCreditAmount"].GetDecimal());


                newRow = dtReport.NewRow();
                newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";
                newRow["DebitAmount"] = sumDebit.GetDecimalComma();
                newRow["ActualDebitAmount"] = sumActualDebit.GetDecimalComma();
                newRow["CreditAmount"] = sumCredit.GetDecimalComma();
                newRow["ActualCreditAmount"] = sumActualCredit.GetDecimalComma();

                var ledgerBalance = sumDebit - sumCredit;
                var ledgerActualBalance = sumActualDebit - sumActualCredit;

                var result = Math.Abs(ledgerBalance) > 0 ? Math.Abs(ledgerBalance).ToString() : "NIL";
                var resultActual = Math.Abs(ActualBalance) > 0 ? Math.Abs(ActualBalance).ToString() : "NIL";

                newRow["Balance"] = result.GetDecimalComma();
                newRow["BalanceType"] = ledgerBalance > 0 ? "Dr" : ledgerBalance < 0 ? "Cr" : string.Empty;
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";

        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var actualDebit = dtGrand.AsEnumerable().Sum(x => x["ActualDebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        var actualCredit = dtGrand.AsEnumerable().Sum(x => x["ActualCreditAmount"].GetDecimal());

        var totalBalance = debit - credit;
        newRow["DebitAmount"] = debit.GetDecimalComma();
        newRow["DebitAmount"] = actualDebit.GetDecimalComma();
        newRow["CreditAmount"] = credit.GetDecimalComma();
        newRow["CreditAmount"] = actualCredit.GetDecimalComma();

        newRow["Balance"] = Math.Abs(totalBalance) > 0 ? totalBalance.GetDecimalComma() : "NIL";
        newRow["BalanceType"] = totalBalance > 0 ? "Dr" : totalBalance < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerDetailsIncludeSubLedgerReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        decimal ActualBalance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var ledgerDetails = roType["LedgerDesc"].GetString();
            var exitDataRows = dtLedger.Select($"LedgerDesc = '{ledgerDetails}'");
            if (exitDataRows.Length > 0)
            {
                var dtLedgerDetails = exitDataRows.CopyToDataTable();

                var dtSubLedger = dtLedgerDetails.AsEnumerable().GroupBy(r => new
                {
                    ledgerDesc = r.Field<string>("SLName")
                }).Select(g => g.First()).OrderBy(r => r.Field<string>("SLName")).CopyToDataTable();

                foreach (DataRow roSub in dtSubLedger.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["VoucherDate"] = roSub["SLCode"].ToString();
                    newRow["VoucherMiti"] = roSub["SLCode"].ToString();
                    newRow["PartyLedger"] = roSub["SLName"].ToString().ToUpper();
                    newRow["IsGroup"] = 2;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var subLedgerDetails = roSub["SLName"].GetString();
                    var exitSubLedger = dtLedgerDetails.Select($"SLName = '{subLedgerDetails}'");

                    if (exitSubLedger.Length > 0)
                    {
                        var dtSubLedgerDetails = exitSubLedger.CopyToDataTable();

                        var UpdateLedgerDetails = dtSubLedgerDetails.Copy();
                        var index = 0;
                        foreach (DataRow item in dtSubLedgerDetails.Rows)
                        {
                            Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                            ActualBalance = ActualBalance + item["ActualDebitAmount"].GetDecimal() - item["ActualCreditAmount"].GetDecimal();
                            UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance).GetDecimalComma());
                            UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                            UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                            if (item["Cheque_No"].IsValueExits())
                            {
                                newRow = UpdateLedgerDetails.NewRow();
                                newRow["PartyLedger"] = $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                                newRow["IsGroup"] = 10;
                                UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                index += 1;
                            }

                            if (GetReports.IsRemarks)
                            {
                                if (item["Remarks"].IsValueExits())
                                {
                                    newRow = UpdateLedgerDetails.NewRow();
                                    newRow["PartyLedger"] = item["Remarks"].ToString();
                                    newRow["IsGroup"] = 10;
                                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                    index += 1;
                                }
                            }

                            if (GetReports.IsNarration)
                            {
                                if (item["Narration"].IsValueExits())
                                {
                                    newRow = UpdateLedgerDetails.NewRow();
                                    newRow["PartyLedger"] = item["Narration"].ToString();
                                    newRow["IsGroup"] = 10;
                                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                    index += 1;
                                }
                            }

                            var vModule = item["Module"].ToString().Trim();
                            //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                            string[] productStrings = { "PB", "SB", "SR", "PR" };

                            if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                            {
                                var cmdString = $@"
							    SELECT SPACE(8) + isnull(AMS.ProperCase(PName),'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
							    FROM AMS.StockDetails sd
								    INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
							    WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["VoucherNo"].ToString().Trim()}' ";
                                var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                                if (dtProduct.Rows.Count <= 0)
                                {
                                    continue;
                                }

                                foreach (DataRow roProduct in dtProduct.Rows)
                                {
                                    newRow = UpdateLedgerDetails.NewRow();
                                    newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                                    newRow["IsGroup"] = 10;
                                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                    index += 1;
                                }
                            }

                            index++;
                        }

                        var collection = UpdateLedgerDetails.AsEnumerable();
                        collection.Take(UpdateLedgerDetails.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                        newRow = dtReport.NewRow();
                        newRow["PartyLedger"] = $"[{roSub["SLName"].GetUpper()}] TOTAL >>";
                        newRow["DebitAmount"] = collection.Sum(x => x["DebitAmount"].GetDecimal()).GetDecimalComma();
                        newRow["ActualDebitAmount"] = collection.Sum(x => x["ActualDebitAmount"].GetDecimal()).GetDecimalComma();
                        newRow["CreditAmount"] = collection.Sum(x => x["CreditAmount"].GetDecimal()).GetDecimalComma();
                        newRow["ActualCreditAmount"] = collection.Sum(x => x["ActualCreditAmount"].GetDecimal()).GetDecimalComma();

                        var nil = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";
                        var actual = Math.Abs(ActualBalance) > 0 ? Math.Abs(ActualBalance).ToString() : "NIL";

                        newRow["Balance"] = nil.GetDecimalComma();
                        newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
                        newRow["IsGroup"] = 22;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                        Balance = 0;

                    }
                }

                newRow = dtReport.NewRow();
                newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";

                var sumLedger = dtLedgerDetails.AsEnumerable();
                var ledgerDebit = sumLedger.Sum(x => x["DebitAmount"].GetDecimal());
                var ledgerCredit = sumLedger.Sum(x => x["CreditAmount"].GetDecimal());

                Balance = ledgerDebit - ledgerCredit;

                newRow["DebitAmount"] = ledgerDebit.GetDecimalComma();
                newRow["ActualDebitAmount"] = sumLedger.Sum(x => x["ActualDebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["CreditAmount"] = ledgerCredit.GetDecimalComma();
                newRow["ActualCreditAmount"] = sumLedger.Sum(x => x["ActualCreditAmount"].GetDecimal()).GetDecimalComma();

                var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";
                var resultActual = Math.Abs(ActualBalance) > 0 ? Math.Abs(ActualBalance).ToString() : "NIL";

                newRow["Balance"] = result.GetDecimalComma();
                newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                Balance = 0;
            }
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";

        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var actualDebit = dtGrand.AsEnumerable().Sum(x => x["ActualDebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        var actualCredit = dtGrand.AsEnumerable().Sum(x => x["ActualCreditAmount"].GetDecimal());

        var totalBalance = debit - credit;
        newRow["DebitAmount"] = debit.GetDecimalComma();
        newRow["DebitAmount"] = actualDebit.GetDecimalComma();
        newRow["CreditAmount"] = credit.GetDecimalComma();
        newRow["CreditAmount"] = actualCredit.GetDecimalComma();

        newRow["Balance"] = Math.Abs(totalBalance) > 0 ? Math.Abs(totalBalance).GetDecimalComma() : "NIL";
        newRow["BalanceType"] = totalBalance > 0 ? "Dr" : totalBalance < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnSubLedgerDetailsIncludeLedgerReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        decimal ActualBalance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var ledgerDetails = roType["LedgerDesc"].GetString();
            var exitDataRows = dtLedger.Select($"LedgerDesc = '{ledgerDetails}'");
            if (exitDataRows.Length > 0)
            {
                var dtLedgerDetails = exitDataRows.CopyToDataTable();

                var dtSubLedger = dtLedgerDetails.AsEnumerable().GroupBy(r => new
                {
                    ledgerDesc = r.Field<string>("SLName")
                }).Select(g => g.First()).OrderBy(r => r.Field<string>("SLName")).CopyToDataTable();

                foreach (DataRow roSub in dtSubLedger.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["VoucherDate"] = roSub["SLCode"].ToString();
                    newRow["VoucherMiti"] = roSub["SLCode"].ToString();
                    newRow["PartyLedger"] = "     " + roSub["SLName"].ToString().ToUpper();
                    newRow["IsGroup"] = 2;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                    var subLedgerDetails = roSub["SLName"].GetString();
                    var exitSubLedger = dtLedgerDetails.Select($"SLName = '{subLedgerDetails}'");

                    if (exitSubLedger.Length > 0)
                    {
                        var dtSubLedgerDetails = exitSubLedger.CopyToDataTable();

                        var UpdateLedgerDetails = dtSubLedgerDetails.Copy();
                        var index = 0;
                        foreach (DataRow item in dtSubLedgerDetails.Rows)
                        {
                            Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                            ActualBalance = ActualBalance + item["ActualDebitAmount"].GetDecimal() - item["ActualCreditAmount"].GetDecimal();
                            UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance).GetDecimalComma());
                            UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                            UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                            if (item["Cheque_No"].IsValueExits())
                            {
                                newRow = UpdateLedgerDetails.NewRow();
                                newRow["PartyLedger"] = $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                                newRow["IsGroup"] = 10;
                                UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                index += 1;
                            }

                            if (GetReports.IsRemarks)
                            {
                                if (item["Remarks"].IsValueExits())
                                {
                                    newRow = UpdateLedgerDetails.NewRow();
                                    newRow["PartyLedger"] = item["Remarks"].ToString();
                                    newRow["IsGroup"] = 10;
                                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                    index += 1;
                                }
                            }

                            if (GetReports.IsNarration)
                            {
                                if (item["Narration"].IsValueExits())
                                {
                                    newRow = UpdateLedgerDetails.NewRow();
                                    newRow["PartyLedger"] = item["Narration"].ToString();
                                    newRow["IsGroup"] = 10;
                                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                    index += 1;
                                }
                            }

                            var vModule = item["Module"].ToString().Trim();
                            //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                            string[] productStrings = { "PB", "SB", "SR", "PR" };

                            if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                            {
                                var cmdString = $@"
							    SELECT SPACE(8) + isnull(AMS.ProperCase(PName),'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
							    FROM AMS.StockDetails sd
								    INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
							    WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["VoucherNo"].ToString().Trim()}' ";
                                var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                                if (dtProduct.Rows.Count <= 0)
                                {
                                    continue;
                                }

                                foreach (DataRow roProduct in dtProduct.Rows)
                                {
                                    newRow = UpdateLedgerDetails.NewRow();
                                    newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                                    newRow["IsGroup"] = 10;
                                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                                    index += 1;
                                }
                            }

                            index++;
                        }

                        var collection = UpdateLedgerDetails.AsEnumerable();
                        collection.Take(UpdateLedgerDetails.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

                        newRow = dtReport.NewRow();
                        newRow["PartyLedger"] = $"[{roSub["SLName"].GetUpper()}] TOTAL >>";
                        newRow["DebitAmount"] = collection.Sum(x => x["DebitAmount"].GetDecimal()).GetDecimalComma();
                        newRow["ActualDebitAmount"] = collection.Sum(x => x["ActualDebitAmount"].GetDecimal()).GetDecimalComma();
                        newRow["CreditAmount"] = collection.Sum(x => x["CreditAmount"].GetDecimal()).GetDecimalComma();
                        newRow["ActualCreditAmount"] = collection.Sum(x => x["ActualCreditAmount"].GetDecimal()).GetDecimalComma();

                        var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";
                        var resultActual = Math.Abs(ActualBalance) > 0 ? Math.Abs(ActualBalance).ToString() : "NIL";

                        newRow["Balance"] = result.GetDecimalComma();
                        newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
                        newRow["IsGroup"] = 22;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                        Balance = 0;

                    }
                }

                newRow = dtReport.NewRow();
                newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";

                var sumLedger = dtLedgerDetails.AsEnumerable();
                var ledgerDebit = sumLedger.Sum(x => x["DebitAmount"].GetDecimal());
                var ledgerCredit = sumLedger.Sum(x => x["CreditAmount"].GetDecimal());

                Balance = ledgerDebit - ledgerCredit;

                newRow["DebitAmount"] = ledgerDebit.GetDecimalComma();
                newRow["ActualDebitAmount"] = sumLedger.Sum(x => x["ActualDebitAmount"].GetDecimal()).GetDecimalComma();
                newRow["CreditAmount"] = ledgerCredit.GetDecimalComma();
                newRow["ActualCreditAmount"] = sumLedger.Sum(x => x["ActualCreditAmount"].GetDecimal()).GetDecimalComma();

                var nil = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";
                var actual = Math.Abs(ActualBalance) > 0 ? Math.Abs(ActualBalance).ToString() : "NIL";

                newRow["Balance"] = nil.GetDecimalComma();
                newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                Balance = 0;
            }
        }

        var dtGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";

        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var actualDebit = dtGrand.AsEnumerable().Sum(x => x["ActualDebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());
        var actualCredit = dtGrand.AsEnumerable().Sum(x => x["ActualCreditAmount"].GetDecimal());

        var totalBalance = debit - credit;
        newRow["DebitAmount"] = debit.GetDecimalComma();
        newRow["DebitAmount"] = actualDebit.GetDecimalComma();
        newRow["CreditAmount"] = credit.GetDecimalComma();
        newRow["CreditAmount"] = actualCredit.GetDecimalComma();

        newRow["Balance"] = Math.Abs(totalBalance) > 0 ? Math.Abs(totalBalance).GetDecimalComma() : "NIL";
        newRow["BalanceType"] = totalBalance > 0 ? "Dr" : totalBalance < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;

    }

    private DataTable ReturnLedgerDetailsAccountGroupOnlyReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtLedgerDetails = dtLedger.Select($"LedgerDesc = '{roType["LedgerDesc"]}'").CopyToDataTable();

            var UpdateLedgerDetails = dtLedgerDetails.Copy();
            var index = 0;
            foreach (DataRow item in dtLedgerDetails.Rows)
            {
                var ledgerDetails = string.Empty;
                Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance));
                UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                if (item["Cheque_No"].IsValueExits())
                {
                    newRow = UpdateLedgerDetails.NewRow();
                    newRow["PartyLedger"] =
                        $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                    newRow["IsGroup"] = 10;
                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                    index += 1;
                }

                if (GetReports.IsRemarks)
                {
                    if (item["Remarks"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Remarks"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                if (GetReports.IsNarration)
                {
                    if (item["Narration"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Narration"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                var vModule = item["Module"].ToString().Trim();
                //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                string[] productStrings = { "PB", "SB", "SR", "PR" };

                if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                {
                    var cmdString = $@"
						SELECT SPACE(8) + isnull(PName,'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
						FROM AMS.StockDetails sd
							INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
						WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["Voucher_No"].ToString().Trim()}' ";
                    var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    if (dtProduct.Rows.Count <= 0)
                    {
                        continue;
                    }

                    foreach (DataRow roProduct in dtProduct.Rows)
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                index++;
            }

            UpdateLedgerDetails.AsEnumerable().Take(UpdateLedgerDetails.Rows.Count)
                .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

            newRow = dtReport.NewRow();
            newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";
            newRow["DebitAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
            newRow["CreditAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

            var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";

            newRow["Balance"] = result;
            newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
            newRow["IsGroup"] = 22;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            Balance = 0;
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";
        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

        newRow["DebitAmount"] = debit;
        newRow["CreditAmount"] = credit;
        newRow["Balance"] = Math.Abs(debit - credit);
        newRow["BalanceType"] = debit - credit > 0 ? "Dr" : debit - credit < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerDetailsAccountGroupSubGroupOnlyReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtLedgerDetails = dtLedger.Select($"LedgerDesc = '{roType["LedgerDesc"]}'").CopyToDataTable();

            var UpdateLedgerDetails = dtLedgerDetails.Copy();
            var index = 0;
            foreach (DataRow item in dtLedgerDetails.Rows)
            {
                var ledgerDetails = string.Empty;
                Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance));
                UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                if (item["Cheque_No"].IsValueExits())
                {
                    newRow = UpdateLedgerDetails.NewRow();
                    newRow["PartyLedger"] =
                        $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                    newRow["IsGroup"] = 10;
                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                    index += 1;
                }

                if (GetReports.IsRemarks)
                {
                    if (item["Remarks"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Remarks"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                if (GetReports.IsNarration)
                {
                    if (item["Narration"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Narration"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                var vModule = item["Module"].ToString().Trim();
                //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                string[] productStrings = { "PB", "SB", "SR", "PR" };

                if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                {
                    var cmdString = $@"
						SELECT SPACE(8) + isnull(PName,'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
						FROM AMS.StockDetails sd
							INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
						WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["Voucher_No"].ToString().Trim()}' ";
                    var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    if (dtProduct.Rows.Count <= 0)
                    {
                        continue;
                    }

                    foreach (DataRow roProduct in dtProduct.Rows)
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                index++;
            }

            UpdateLedgerDetails.AsEnumerable().Take(UpdateLedgerDetails.Rows.Count)
                .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

            newRow = dtReport.NewRow();
            newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";
            newRow["DebitAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
            newRow["CreditAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

            var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";

            newRow["Balance"] = result;
            newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
            newRow["IsGroup"] = 22;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            Balance = 0;
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";
        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

        newRow["DebitAmount"] = debit;
        newRow["CreditAmount"] = credit;
        newRow["Balance"] = Math.Abs(debit - credit);
        newRow["BalanceType"] = debit - credit > 0 ? "Dr" : debit - credit < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerDetailsAccountGroupWiseReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtLedgerDetails = dtLedger.Select($"LedgerDesc = '{roType["LedgerDesc"]}'").CopyToDataTable();

            var UpdateLedgerDetails = dtLedgerDetails.Copy();
            var index = 0;
            foreach (DataRow item in dtLedgerDetails.Rows)
            {
                var ledgerDetails = string.Empty;
                Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance));
                UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                if (item["Cheque_No"].IsValueExits())
                {
                    newRow = UpdateLedgerDetails.NewRow();
                    newRow["PartyLedger"] =
                        $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                    newRow["IsGroup"] = 10;
                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                    index += 1;
                }

                if (GetReports.IsRemarks)
                {
                    if (item["Remarks"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Remarks"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                if (GetReports.IsNarration)
                {
                    if (item["Narration"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Narration"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                var vModule = item["Module"].ToString().Trim();
                //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                string[] productStrings = { "PB", "SB", "SR", "PR" };

                if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                {
                    var cmdString = $@"
						SELECT SPACE(8) + isnull(PName,'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
						FROM AMS.StockDetails sd
							INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
						WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["Voucher_No"].ToString().Trim()}' ";
                    var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    if (dtProduct.Rows.Count <= 0)
                    {
                        continue;
                    }

                    foreach (DataRow roProduct in dtProduct.Rows)
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                index++;
            }

            UpdateLedgerDetails.AsEnumerable().Take(UpdateLedgerDetails.Rows.Count)
                .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

            newRow = dtReport.NewRow();
            newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";
            newRow["DebitAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
            newRow["CreditAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

            var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";

            newRow["Balance"] = result;
            newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
            newRow["IsGroup"] = 22;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            Balance = 0;
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";
        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

        newRow["DebitAmount"] = debit;
        newRow["CreditAmount"] = credit;
        newRow["Balance"] = Math.Abs(debit - credit);
        newRow["BalanceType"] = debit - credit > 0 ? "Dr" : debit - credit < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerDetailsAccountSubGroupWiseReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            ledgerDesc = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();
        decimal Balance = 0;
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["LedgerId"] = roType["LedgerId"].ToString();
            newRow["VoucherDate"] = roType["GLCode"].ToString();
            newRow["VoucherMiti"] = roType["GLCode"].ToString();
            newRow["PartyLedger"] = roType["LedgerDesc"].ToString().ToUpper();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtLedgerDetails = dtLedger.Select($"LedgerDesc = '{roType["LedgerDesc"]}'").CopyToDataTable();

            var UpdateLedgerDetails = dtLedgerDetails.Copy();
            var index = 0;
            foreach (DataRow item in dtLedgerDetails.Rows)
            {
                var ledgerDetails = string.Empty;
                Balance = Balance + item["DebitAmount"].GetDecimal() - item["CreditAmount"].GetDecimal();
                UpdateLedgerDetails.Rows[index].SetField("Balance", Math.Abs(Balance));
                UpdateLedgerDetails.Rows[index].SetField("PartyLedger", "     " + item["PartyLedger"]);
                UpdateLedgerDetails.Rows[index].SetField("BalanceType", Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : "");

                if (item["Cheque_No"].IsValueExits())
                {
                    newRow = UpdateLedgerDetails.NewRow();
                    newRow["PartyLedger"] =
                        $"     {item["Cheque_No"]} - {item["Cheque_Date"].GetDateString()} - {item["Cheque_Miti"]}";
                    newRow["IsGroup"] = 10;
                    UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                    index += 1;
                }

                if (GetReports.IsRemarks)
                {
                    if (item["Remarks"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Remarks"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                if (GetReports.IsNarration)
                {
                    if (item["Narration"].IsValueExits())
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = item["Narration"].ToString();
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                var vModule = item["Module"].ToString().Trim();
                //string[] financeStrings = { "JV", "CB", "CV", "PV", "DN", "CN" };
                string[] productStrings = { "PB", "SB", "SR", "PR" };

                if (GetReports.IsProductDetails && productStrings.Contains(vModule))
                {
                    var cmdString = $@"
						SELECT SPACE(8) + isnull(PName,'') + Case When len(left(isnull(PName,''),70)) > 50 then SPACE(len(left(Isnull(PName,''),70))- 50) else	SPACE(50-len(left(IsNull(PName,''),70))) end + CAST(CAST(sd.StockQty AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.Rate AS DECIMAL(18,2))  AS NVARCHAR) + SPACE(5) + CAST(CAST(sd.NetAmt AS DECIMAL(18,2)) AS NVARCHAR) ProductDetails
						FROM AMS.StockDetails sd
							INNER JOIN AMS.Product p ON sd.Product_Id = p.PID
						WHERE sd.Module='{vModule}' and sd.Voucher_No='{item["Voucher_No"].ToString().Trim()}' ";
                    var dtProduct = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
                    if (dtProduct.Rows.Count <= 0)
                    {
                        continue;
                    }

                    foreach (DataRow roProduct in dtProduct.Rows)
                    {
                        newRow = UpdateLedgerDetails.NewRow();
                        newRow["PartyLedger"] = "     " + roProduct["ProductDetails"];
                        newRow["IsGroup"] = 10;
                        UpdateLedgerDetails.Rows.InsertAt(newRow, index + 1);
                        index += 1;
                    }
                }

                index++;
            }

            UpdateLedgerDetails.AsEnumerable().Take(UpdateLedgerDetails.Rows.Count)
                .CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

            newRow = dtReport.NewRow();
            newRow["PartyLedger"] = $"[{roType["LedgerDesc"].GetUpper()}] TOTAL >>";
            newRow["DebitAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
            newRow["CreditAmount"] = UpdateLedgerDetails.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

            var result = Math.Abs(Balance) > 0 ? Math.Abs(Balance).ToString() : "NIL";

            newRow["Balance"] = result;
            newRow["BalanceType"] = Balance > 0 ? "Dr" : Balance < 0 ? "Cr" : string.Empty;
            newRow["IsGroup"] = 22;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            Balance = 0;
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["PartyLedger"] = "[GRAND TOTAL] ";
        var debit = dtGrand.AsEnumerable().Sum(x => x["DebitAmount"].GetDecimal());
        var credit = dtGrand.AsEnumerable().Sum(x => x["CreditAmount"].GetDecimal());

        newRow["DebitAmount"] = debit;
        newRow["CreditAmount"] = credit;
        newRow["Balance"] = Math.Abs(debit - credit);
        newRow["BalanceType"] = debit - credit > 0 ? "Dr" : debit - credit < 0 ? "Cr" : "";
        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private string GetDocAgentLedgerDetailsReportScript()
    {
        var cmdString = $@"
				WITH LedgerReport AS
				(
					SELECT Ledger_ID, 0 Agent_ID, Module, Voucher_No, RefVno, Voucher_Date, Voucher_Miti, '' Cheque_No, '' Cheque_Date, '' Cheque_Miti, Currency_ID, Currency_Rate, SUM ( DEBIT ) DEBIT, SUM ( LOCAL_DEBIT ) LOCAL_DEBIT, SUM ( CREDIT ) CREDIT, SUM ( LOCAL_CREDIT ) LOCAL_CREDIT, '' Remarks
					FROM
					(
						SELECT ad.Ledger_ID,'OB' Module,'0000' Voucher_No,'' RefVno,'' Voucher_Date,'' Voucher_Miti,'' Currency_ID, 1 Currency_Rate,CASE WHEN SUM(ad.Debit_Amt - ad.Credit_Amt) > 0 THEN SUM(ad.Debit_Amt - ad.Credit_Amt) ELSE 0 END DEBIT ,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END LOCAL_DEBIT,CASE WHEN SUM(ad.Credit_Amt - ad.Debit_Amt) > 0 THEN SUM(ad.Credit_Amt- ad.Debit_Amt) ELSE 0 END CREDIT,CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END LOCAL_CREDIT
						FROM AMS.AccountDetails ad
						LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						WHERE ag.PrimaryGrp IN ('Balance Sheet','BS') AND ad.FiscalYearId < {ObjGlobal.SysFiscalYearId} AND ad.Branch_ID IN ({ObjGlobal.SysBranchId})  ";
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.AgentId)
            ? $@" AND ad.Subleder_ID in ({GetReports.SubLedgerId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc
            ? @" AND ad.Module NOT IN ('PDC', 'PROV')  "
            : string.Empty;
        cmdString += $@"
					GROUP BY ad.Ledger_ID
					UNION ALL
					SELECT ad.Ledger_ID,'OB' Module,'0000' Voucher_No,'' RefVno,'' Voucher_Date,'' Voucher_Miti,'' Currency_ID, 1 Currency_Rate,CASE WHEN SUM(ad.Debit_Amt - ad.Credit_Amt) > 0 THEN SUM(ad.Debit_Amt - ad.Credit_Amt) ELSE 0 END DEBIT ,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END LOCAL_DEBIT,CASE WHEN SUM(ad.Credit_Amt - ad.Debit_Amt) > 0 THEN SUM(ad.Credit_Amt- ad.Debit_Amt) ELSE 0 END CREDIT,CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END LOCAL_CREDIT
					FROM AMS.AccountDetails ad
					WHERE ad.Voucher_Date <'{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}  AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@"AND ad.Ledger_ID in ({GetReports.LedgerId})  "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.AgentId)
            ? $@"AND ad.Subleder_ID in ({GetReports.SubLedgerId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc
            ? @"AND ad.Module NOT IN ('PDC', 'PROV')  "
            : string.Empty;
        cmdString += $@"
						GROUP BY ad.Ledger_ID
					)  OpeningLedger
					GROUP BY Ledger_ID, Module, Voucher_No, Voucher_Date, Voucher_Miti, Currency_ID, Currency_Rate,OpeningLedger.RefVno
					HAVING SUM (OpeningLedger.LOCAL_DEBIT - OpeningLedger.LOCAL_CREDIT) <> 0
					UNION ALL
					SELECT ad.Ledger_ID, ad.Agent_ID, ad.Module, ad.Voucher_No, ad.RefNo, ad.Voucher_Date, ad.Voucher_Miti, ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti, ad.Currency_ID, ad.Currency_Rate, SUM ( ad.Debit_Amt ) LocalDebit_Amt, SUM ( ad.LocalDebit_Amt ) LocalDebit_Amt, SUM ( ad.Credit_Amt ) Credit_Amt, SUM ( ad.LocalCredit_Amt ) LocalCredit_Amt, ad.Remarks
					FROM AMS.AccountDetails ad
					WHERE ad.Voucher_Date >= '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND ad.Voucher_Date <= '{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}'  AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@"
					AND ad.Ledger_ID in ({GetReports.LedgerId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.AgentId)
            ? $@"
					AND ad.Subleder_ID in ({GetReports.SubLedgerId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc
            ? @"
					AND ad.Module NOT IN ('PDC', 'PROV')  "
            : string.Empty;
        cmdString += @"
					GROUP BY ad.Ledger_ID, Agent_ID, ad.Module, ad.Voucher_No, ad.RefNo, ad.Voucher_Date, ad.Voucher_Miti, ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti, ad.Currency_ID, ad.Currency_Rate, ad.Remarks
				) SELECT lr.Module, lr.Voucher_No, lr.RefVno, lr.Voucher_Date, lr.Voucher_Miti, lr.Cheque_No, lr.Cheque_Date, lr.Cheque_Miti, lr.Ledger_ID, gl.GLCode, gl.GLName, ISNULL ( lr.Agent_ID, 0 ) Subleder_ID, ISNULL ( sl.AgentName, 'NO-DEPARTMENT' ) SLName, sl.AgentCode SLCode, gl.GrpId, ag.GrpName, gl.SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUB-GROUP' ) SubGrpName, lr.Currency_ID, c.CCode, CAST(lr.Currency_Rate AS DECIMAL(18, 2)) Currency_Rate, CAST(lr.DEBIT AS DECIMAL(18, 2)) DEBIT, CAST(lr.LOCAL_DEBIT AS DECIMAL(18, 2)) LOCAL_DEBIT, CAST(lr.CREDIT AS DECIMAL(18, 2)) CREDIT, CAST(lr.LOCAL_CREDIT AS DECIMAL(18, 2)) LOCAL_CREDIT, lr.Remarks
				FROM LedgerReport lr
				LEFT OUTER JOIN AMS.GeneralLedger gl ON lr.Ledger_ID = gl.GLID
				LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
				LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
				LEFT OUTER JOIN AMS.Currency c ON lr.Currency_ID = c.CId
				LEFT OUTER JOIN AMS.JuniorAgent sl ON lr.Agent_ID = sl.AgentId
				WHERE 1=1  ";
        cmdString += !string.IsNullOrEmpty(GetReports.GroupId)
            ? $@" AND ag.GrpId in ({GetReports.GroupId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.SubGroupId)
            ? $@"AND asg.SubGrpId in ({GetReports.GroupId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@" AND lr.Ledger_ID in ({GetReports.LedgerId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.AgentId)
            ? $@" AND lr.Agent_ID in ({GetReports.SubLedgerId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc ? @" AND lr.Module NOT IN ('PDC', 'PROV') " : string.Empty;
        if (!string.IsNullOrEmpty(GetReports.AccountType) && string.IsNullOrEmpty(GetReports.LedgerId))
        {
            cmdString += GetReports.AccountType.ToUpper() switch
            {
                "CUSTOMER" => @" AND gl.GLType IN ('Customer','Both') ",
                "VENDOR" => @" AND gl.GLType IN ('Vendor','Both') ",
                "CASH" => @" AND gl.GLType ='Cash' ",
                "BANK" => @" AND gl.GLType ='Bank' ",
                "OTHER" => @" AND gl.GLType ='Other' ",
                _ => string.Empty
            };
        }

        cmdString += @"
				ORDER BY gl.GLName, lr.Voucher_Date ";
        return cmdString;
    }

    private string GetDepartmentLedgerDetailsReportScript()
    {
        var cmdString = $@"
				WITH LedgerReport AS
				(
					SELECT Ledger_ID, 0 Department_ID1, Module, Voucher_No, RefVno, Voucher_Date, Voucher_Miti, '' Cheque_No, '' Cheque_Date, '' Cheque_Miti, Currency_ID, Currency_Rate, SUM ( DEBIT ) DEBIT, SUM ( LOCAL_DEBIT ) LOCAL_DEBIT, SUM ( CREDIT ) CREDIT, SUM ( LOCAL_CREDIT ) LOCAL_CREDIT, '' Remarks
					FROM
					(
						SELECT ad.Ledger_ID,'OB' Module,'0000' Voucher_No,'' RefVno,'' Voucher_Date,'' Voucher_Miti,'' Currency_ID, 1 Currency_Rate,CASE WHEN SUM(ad.Debit_Amt - ad.Credit_Amt) > 0 THEN SUM(ad.Debit_Amt - ad.Credit_Amt) ELSE 0 END DEBIT ,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END LOCAL_DEBIT,CASE WHEN SUM(ad.Credit_Amt - ad.Debit_Amt) > 0 THEN SUM(ad.Credit_Amt- ad.Debit_Amt) ELSE 0 END CREDIT,CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END LOCAL_CREDIT
						FROM AMS.AccountDetails ad
						LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						WHERE ag.PrimaryGrp IN ('Balance Sheet','BS') AND ad.FiscalYearId < {ObjGlobal.SysFiscalYearId} AND ad.Branch_ID IN ({ObjGlobal.SysBranchId})  ";
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@"
						AND ad.Ledger_ID in ({GetReports.LedgerId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.DepartmentId)
            ? $@"
						AND ad.Department_ID1 in ({GetReports.DepartmentId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc
            ? @"
						AND ad.Module NOT IN ('PDC', 'PROV')  "
            : string.Empty;
        cmdString += $@"
						GROUP BY ad.Ledger_ID
						UNION ALL
						SELECT ad.Ledger_ID,'OB' Module,'0000' Voucher_No,'' RefVno,'' Voucher_Date,'' Voucher_Miti,'' Currency_ID, 1 Currency_Rate,CASE WHEN SUM(ad.Debit_Amt - ad.Credit_Amt) > 0 THEN SUM(ad.Debit_Amt - ad.Credit_Amt) ELSE 0 END DEBIT ,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END LOCAL_DEBIT,CASE WHEN SUM(ad.Credit_Amt - ad.Debit_Amt) > 0 THEN SUM(ad.Credit_Amt- ad.Debit_Amt) ELSE 0 END CREDIT,CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END LOCAL_CREDIT
						FROM AMS.AccountDetails ad
						WHERE ad.Voucher_Date <'{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}  AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@"
						AND ad.Ledger_ID in ({GetReports.LedgerId})  "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.DepartmentId)
            ? $@"
						AND ad.Department_ID1 in ({GetReports.DepartmentId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc
            ? @"
						AND ad.Module NOT IN ('PDC', 'PROV')  "
            : string.Empty;
        cmdString += $@"
						GROUP BY ad.Ledger_ID
					)  OpeningLedger
					GROUP BY Ledger_ID, Module, Voucher_No, Voucher_Date, Voucher_Miti, Currency_ID, Currency_Rate,OpeningLedger.RefVno
					HAVING SUM (OpeningLedger.LOCAL_DEBIT - OpeningLedger.LOCAL_CREDIT) <> 0
					UNION ALL
					SELECT ad.Ledger_ID, ad.Department_ID1, ad.Module, ad.Voucher_No, ad.RefNo, ad.Voucher_Date, ad.Voucher_Miti, ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti, ad.Currency_ID, ad.Currency_Rate, SUM ( ad.Debit_Amt ) LocalDebit_Amt, SUM ( ad.LocalDebit_Amt ) LocalDebit_Amt, SUM ( ad.Credit_Amt ) Credit_Amt, SUM ( ad.LocalCredit_Amt ) LocalCredit_Amt, ad.Remarks
					FROM AMS.AccountDetails ad
					WHERE ad.Voucher_Date >= '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND ad.Voucher_Date <= '{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}'  AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ";
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@"
					AND ad.Ledger_ID in ({GetReports.LedgerId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.DepartmentId)
            ? $@"
						AND ad.Department_ID1 in ({GetReports.DepartmentId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc
            ? @"
					AND ad.Module NOT IN ('PDC', 'PROV')  "
            : string.Empty;
        cmdString += @"
					GROUP BY ad.Ledger_ID, Department_ID1, ad.Module, ad.Voucher_No, ad.RefNo, ad.Voucher_Date, ad.Voucher_Miti, ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti, ad.Currency_ID, ad.Currency_Rate, ad.Remarks
				) SELECT lr.Module, lr.Voucher_No, lr.RefVno, lr.Voucher_Date, lr.Voucher_Miti, lr.Cheque_No, lr.Cheque_Date, lr.Cheque_Miti, lr.Ledger_ID, gl.GLCode, gl.GLName, ISNULL ( lr.Department_ID1, 0 ) Subleder_ID, ISNULL ( sl.DName, 'NO-DEPARTMENT' ) SLName, sl.DCode SLCode, gl.GrpId, ag.GrpName, gl.SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUB-GROUP' ) SubGrpName, lr.Currency_ID, c.CCode, CAST(lr.Currency_Rate AS DECIMAL(18, 2)) Currency_Rate, CAST(lr.DEBIT AS DECIMAL(18, 2)) DEBIT, CAST(lr.LOCAL_DEBIT AS DECIMAL(18, 2)) LOCAL_DEBIT, CAST(lr.CREDIT AS DECIMAL(18, 2)) CREDIT, CAST(lr.LOCAL_CREDIT AS DECIMAL(18, 2)) LOCAL_CREDIT, lr.Remarks
				FROM LedgerReport lr
				LEFT OUTER JOIN AMS.GeneralLedger gl ON lr.Ledger_ID = gl.GLID
				LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
				LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
				LEFT OUTER JOIN AMS.Currency c ON lr.Currency_ID = c.CId
				LEFT OUTER JOIN AMS.Department sl ON lr.Department_ID1 = sl.DId
				WHERE 1=1  ";
        cmdString += !string.IsNullOrEmpty(GetReports.GroupId)
            ? $@" AND ag.GrpId in ({GetReports.GroupId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.SubGroupId)
            ? $@"AND asg.SubGrpId in ({GetReports.GroupId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.LedgerId)
            ? $@" AND lr.Ledger_ID in ({GetReports.LedgerId}) "
            : string.Empty;
        cmdString += !string.IsNullOrEmpty(GetReports.DepartmentId)
            ? $@" AND lr.Department_ID1 in ({GetReports.DepartmentId}) "
            : string.Empty;
        cmdString += !GetReports.IsIncludePdc ? @" AND lr.Module NOT IN ('PDC', 'PROV') " : string.Empty;
        if (!string.IsNullOrEmpty(GetReports.AccountType) && string.IsNullOrEmpty(GetReports.LedgerId))
        {
            cmdString += GetReports.AccountType.ToUpper() switch
            {
                "CUSTOMER" => @" AND gl.GLType IN ('Customer','Both') ",
                "VENDOR" => @" AND gl.GLType IN ('Vendor','Both') ",
                "CASH" => @" AND gl.GLType ='Cash' ",
                "BANK" => @" AND gl.GLType ='Bank' ",
                "OTHER" => @" AND gl.GLType ='Other' ",
                _ => string.Empty
            };
        }

        cmdString += @"
				ORDER BY gl.GLName, lr.Voucher_Date ";
        return cmdString;
    }

    private string GetSubledgerLedgerDetailsReportScript()
    {
        var cmdString = $@"
			WITH LedgerDetails AS (SELECT o.Module, o.Voucher_No, o.Ledger_ID,o.Subleder_ID, 0 CbLedger_ID,o.CurrencyId,o.CurrencyRate, CASE WHEN SUM(ISNULL(o.ActualOpening,0))>0 THEN SUM(o.Opening) ELSE 0 END ActualDebitAmount,CASE WHEN SUM(ISNULL(o.Opening,0))>0 THEN SUM(o.Opening) ELSE 0 END DebitAmount,CASE WHEN SUM(ISNULL(o.ActualOpening,0))<0 THEN SUM(ABS(o.Opening))ELSE 0 END ActualCreditAmount, CASE WHEN SUM(ISNULL(o.Opening,0))<0 THEN SUM(ABS(o.Opening))ELSE 0 END CreditAmount, '' RefNo, '' Cheque_No, '' Cheque_Miti, '' Cheque_Date, '' Voucher_Date, '' Voucher_Miti, '' Narration, '' Remarks, '' PartyName
								   FROM(SELECT 'OB' Module, '' Voucher_No, ad.Ledger_ID,ad.Subleder_ID,ad.Currency_ID CurrencyId,ad.Currency_Rate CurrencyRate,SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening,SUM(ad.Debit_Amt-ad.Credit_Amt) ActualOpening
										FROM AMS.AccountDetails ad
											 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
											 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
										WHERE ad.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp='BS' ";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += GetReports.SubLedgerId.IsValueExits() ? $@" AND ad.Subleder_ID in ({GetReports.SubLedgerId})" : " ";
        cmdString += $@"
										GROUP BY ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate,ad.Subleder_ID
										UNION ALL
										SELECT 'OB' Module, '' Voucher_No, ad.Ledger_ID,ad.Subleder_ID,ad.Currency_ID,ad.Currency_Rate,SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening,SUM(ad.Debit_Amt-ad.Credit_Amt) ActualOpening
										FROM AMS.AccountDetails ad
										WHERE ad.FiscalYearId={ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}' ";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += GetReports.SubLedgerId.IsValueExits() ? $@" AND ad.Subleder_ID in ({GetReports.LedgerId})" : " ";
        cmdString += $@"
										GROUP BY ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate,ad.Subleder_ID) o
								   GROUP BY o.Module, o.Voucher_No, o.Ledger_ID,o.CurrencyId,o.CurrencyRate,o.Subleder_ID
								   HAVING SUM(o.Opening)<>0
								   UNION ALL
								   SELECT ad.Module, ad.Voucher_No, ad.Ledger_ID, ad.Subleder_ID,ad.CbLedger_ID,ad.Currency_ID,ad.Currency_Rate,ad.Debit_Amt ActualDebitAmount, ad.LocalDebit_Amt DebitAmount,ad.Credit_Amt ActualCreditAmount,  ad.LocalCredit_Amt CreditAmount, ad.RefNo, ad.Cheque_No, ad.Cheque_Miti, ad.Cheque_Date, ad.Voucher_Date , ad.Voucher_Miti, ad.Narration, ad.Remarks, ad.PartyName
								   FROM AMS.AccountDetails ad
								   WHERE ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += GetReports.SubLedgerId.IsValueExits() ? $@" AND ad.Subleder_ID in ({GetReports.SubLedgerId})" : " ";
        cmdString += @" )
			SELECT ld.Module,";
        cmdString += GetReports.IsPostingDetails
            ? " CASE WHEN ISNULL(ld.Voucher_No,'') =''  THEN AMS.ProperCase(md.ModuleType) ELSE ISNULL(pl.GLName,AMS.ProperCase(md.ModuleType)) + ' >> : ['+ld.Voucher_No+'] 'END PartyLedger,  "
            : " CASE WHEN ISNULL(ld.RefNo,'') = '' THEN  ( CASE WHEN ISNULL(ld.Voucher_No,'') = ''  THEN AMS.ProperCase(md.ModuleType) ELSE AMS.ProperCase(md.ModuleType) + ' >> : ['+ld.Voucher_No+']' END) ELSE AMS.ProperCase(md.ModuleType) + ' >> : ['+ld.Voucher_No+']' + ' - [ REF VNO  - ' + ld.RefNo + ' ]' END PartyLedger, ";
        cmdString +=
            $@" ld.Voucher_No VoucherNo,CASE WHEN ld.Module = 'OB' THEN '' ELSE CONVERT(NVARCHAR(10), ld.Voucher_Date, 103) END VoucherDate, ld.Voucher_Miti VoucherMiti, ld.Ledger_ID LedgerId, gl.GLName LedgerDesc, gl.GLCode,ld.Subleder_ID SubLedgerId,s.SLName,s.SLCode, ld.RefNo, ld.Cheque_No, ld.Cheque_Miti, ld.Cheque_Date, ld.Voucher_Date, ld.Voucher_Miti, ld.Narration, ld.Remarks, ld.PartyName,ld.CurrencyId,c.CCode,ld.CurrencyRate,CASE WHEN ISNULL(ld.ActualDebitAmount,0) > 0 THEN  FORMAT(ld.ActualDebitAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END ActualDebitAmount, CASE WHEN ISNULL(ld.DebitAmount,0) > 0 THEN  FORMAT(ld.DebitAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END DebitAmount, CASE WHEN ISNULL(ld.ActualCreditAmount,0) > 0 THEN  FORMAT(ld.ActualCreditAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END ActualCreditAmount,CASE WHEN ISNULL(ld.CreditAmount,0) > 0 THEN  FORMAT(ld.CreditAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END CreditAmount, '' Balance,'' BalanceType,0 IsGroup
			FROM LedgerDetails ld
				 LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ld.Module
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ld.Ledger_ID
				 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=ld.CbLedger_ID
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Currency c ON c.CId = ld.CurrencyId
	             LEFT OUTER JOIN AMS.Subledger s ON s.SLId = ld.Subleder_ID
			WHERE 1=1 ";
        if (GetReports.AccountType.IsValueExits())
        {
            cmdString += GetReports.AccountType.ToUpper() switch
            {
                "CUSTOMER" => @" AND gl.GLType IN ('Customer','Both') ",
                "VENDOR" => @" AND gl.GLType IN ('Vendor','Both') ",
                "CASH" => @" AND gl.GLType ='Cash' ",
                "BANK" => @" AND gl.GLType ='Bank' ",
                "OTHER" => @" AND gl.GLType ='Other' ",
                _ => string.Empty
            };
        }

        cmdString += GetReports.FilterFor switch
        {
            "Account Group/Ledger" => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC,ld.Voucher_No;",
            "Account Group/Sub Group/Ledger" => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC, ld.Voucher_No; ",
            _ => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC, ld.Voucher_No; "
        };
        return cmdString;
    }

    private string GetLedgerDetailsReportScript()
    {
        var cmdString = $@"
			WITH LedgerDetails AS (SELECT o.Module, o.Voucher_No, o.Ledger_ID, 0 CbLedger_ID,o.CurrencyId,o.CurrencyRate, CASE WHEN SUM(ISNULL(o.ActualOpening,0))>0 THEN SUM(o.Opening) ELSE 0 END ActualDebitAmount,CASE WHEN SUM(ISNULL(o.Opening,0))>0 THEN SUM(o.Opening) ELSE 0 END DebitAmount,CASE WHEN SUM(ISNULL(o.ActualOpening,0))<0 THEN SUM(o.Opening)ELSE 0 END ActualCreditAmount, CASE WHEN SUM(ISNULL(o.Opening,0))<0 THEN SUM(o.Opening)ELSE 0 END CreditAmount, '' RefNo, '' Cheque_No, '' Cheque_Miti, '' Cheque_Date, '' Voucher_Date, '' Voucher_Miti, '' Narration, '' Remarks, '' PartyName
								   FROM(SELECT 'OB' Module, '' Voucher_No, ad.Ledger_ID,ad.Currency_ID CurrencyId,ad.Currency_Rate CurrencyRate,SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening,SUM(ad.Debit_Amt-ad.Credit_Amt) ActualOpening
										FROM AMS.AccountDetails ad
											 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
											 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
										WHERE ad.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp='BS' ";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += $@"
										GROUP BY ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate
										UNION ALL
										SELECT 'OB' Module, '' Voucher_No, ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate,SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening,SUM(ad.Debit_Amt-ad.Credit_Amt) ActualOpening
										FROM AMS.AccountDetails ad
										WHERE ad.FiscalYearId={ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}' ";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += $@"
										GROUP BY ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate) o
								   GROUP BY o.Module, o.Voucher_No, o.Ledger_ID,o.CurrencyId,o.CurrencyRate
								   HAVING SUM(o.Opening)<>0
								   UNION ALL
								   SELECT ad.Module, ad.Voucher_No, ad.Ledger_ID, ad.CbLedger_ID,ad.Currency_ID,ad.Currency_Rate,ad.Debit_Amt ActualDebitAmount, ad.LocalDebit_Amt DebitAmount,ad.Credit_Amt ActualCreditAmount,  ad.LocalCredit_Amt CreditAmount, ad.RefNo, ad.Cheque_No, ad.Cheque_Miti, ad.Cheque_Date, ad.Voucher_Date , ad.Voucher_Miti, ad.Narration, ad.Remarks, ad.PartyName
								   FROM AMS.AccountDetails ad
								   WHERE ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += @" )
			SELECT ld.Module,";
        cmdString += GetReports.IsPostingDetails
            ? " CASE WHEN ISNULL(ld.Voucher_No,'') =''  THEN AMS.ProperCase(md.ModuleType) ELSE ISNULL(pl.GLName,AMS.ProperCase(md.ModuleType)) + ' >> : ['+ld.Voucher_No+'] 'END PartyLedger,  "
            : " CASE WHEN ISNULL(ld.RefNo,'') = '' THEN  ( CASE WHEN ISNULL(ld.Voucher_No,'') = ''  THEN AMS.ProperCase(md.ModuleType) ELSE AMS.ProperCase(md.ModuleType) + ' >> : ['+ld.Voucher_No+']' END) ELSE AMS.ProperCase(md.ModuleType) + ' >> : ['+ld.Voucher_No+']' + ' - [ REF VNO  - ' + ld.RefNo + ' ]' END PartyLedger, ";
        cmdString +=
             $@" ld.Voucher_No VoucherNo,CASE WHEN ld.Module = 'OB' THEN '' ELSE CONVERT(NVARCHAR(10), ld.Voucher_Date, 103) END VoucherDate, ld.Voucher_Miti VoucherMiti, ld.Ledger_ID LedgerId, gl.GLName LedgerDesc, gl.GLCode, ld.RefNo, ld.Cheque_No, ld.Cheque_Miti, ld.Cheque_Date, ld.Voucher_Date, ld.Voucher_Miti, ld.Narration, ld.Remarks, ld.PartyName,ld.CurrencyId,c.CCode,ld.CurrencyRate,
                CASE WHEN ISNULL(ld.ActualDebitAmount,0) > 0 THEN  CAST(ld.ActualDebitAmount AS VARCHAR) ELSE '' END ActualDebitAmount, 
                CASE WHEN ISNULL(ld.DebitAmount,0) > 0 THEN  CAST(ld.DebitAmount AS VARCHAR) ELSE '' END DebitAmount, 
                CASE WHEN ISNULL(ABS(ld.ActualCreditAmount),0) > 0 THEN  CAST(ABS(ld.ActualCreditAmount) AS VARCHAR) ELSE '' END ActualCreditAmount,
                CASE WHEN ISNULL(ABS(ld.CreditAmount),0) > 0 THEN  CAST(ABS(ld.CreditAmount) AS VARCHAR) ELSE '' END CreditAmount, '' Balance,'' BalanceType,0 IsGroup
              FROM LedgerDetails ld
            	 LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ld.Module
            	 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ld.Ledger_ID
            	 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=ld.CbLedger_ID
                 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
            	 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
            	 LEFT OUTER JOIN AMS.Currency c ON c.CId = ld.CurrencyId
                 WHERE 1=1 ";
        if (GetReports.AccountType.IsValueExits())
        {
            cmdString += GetReports.AccountType.ToUpper() switch
            {
                "CUSTOMER" => @" AND gl.GLType IN ('Customer','Both') ",
                "VENDOR" => @" AND gl.GLType IN ('Vendor','Both') ",
                "CASH" => @" AND gl.GLType ='Cash' ",
                "BANK" => @" AND gl.GLType ='Bank' ",
                "OTHER" => @" AND gl.GLType ='Other' ",
                _ => string.Empty
            };
        }

        cmdString += GetReports.FilterFor switch
        {
            "Account Group/Ledger" => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC,ld.Voucher_No;",
            "Account Group/Sub Group/Ledger" => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC, ld.Voucher_No; ",
            _ => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC, ld.Voucher_No; "
        };
        return cmdString;
    }
    private string GetLedgerDetailsReportOpeningScript()
    {
        var cmdString = $@"
			WITH LedgerDetails AS (SELECT o.Module, o.Voucher_No, o.Ledger_ID, 0 CbLedger_ID,o.CurrencyId,o.CurrencyRate, CASE WHEN SUM(ISNULL(o.ActualOpening,0))>0 THEN SUM(o.Opening) ELSE 0 END ActualDebitAmount,CASE WHEN SUM(ISNULL(o.Opening,0))>0 THEN SUM(o.Opening) ELSE 0 END DebitAmount,CASE WHEN SUM(ISNULL(o.ActualOpening,0))<0 THEN SUM(o.Opening)ELSE 0 END ActualCreditAmount, CASE WHEN SUM(ISNULL(o.Opening,0))<0 THEN SUM(o.Opening)ELSE 0 END CreditAmount, '' RefNo, '' Cheque_No, '' Cheque_Miti, '' Cheque_Date, '' Voucher_Date, '' Voucher_Miti, '' Narration, '' Remarks, '' PartyName
								   FROM(SELECT 'OB' Module, '' Voucher_No, ad.Ledger_ID,ad.Currency_ID CurrencyId,ad.Currency_Rate CurrencyRate,SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening,SUM(ad.Debit_Amt-ad.Credit_Amt) ActualOpening
										FROM AMS.AccountDetails ad
											 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
											 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
										WHERE ad.FiscalYearId<{ObjGlobal.SysFiscalYearId - 1} AND ag.PrimaryGrp='BS' ";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += $@"
										GROUP BY ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate
										UNION ALL
										SELECT 'OB' Module, '' Voucher_No, ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate,SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening,SUM(ad.Debit_Amt-ad.Credit_Amt) ActualOpening
										FROM AMS.AccountDetails ad
										WHERE ad.FiscalYearId={ObjGlobal.SysFiscalYearId - 1} AND ad.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}' ";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += $@"
										GROUP BY ad.Ledger_ID,ad.Currency_ID,ad.Currency_Rate) o
								   GROUP BY o.Module, o.Voucher_No, o.Ledger_ID,o.CurrencyId,o.CurrencyRate
								   HAVING SUM(o.Opening)<>0
								   UNION ALL
								   SELECT ad.Module, ad.Voucher_No, ad.Ledger_ID, ad.CbLedger_ID,ad.Currency_ID,ad.Currency_Rate,ad.Debit_Amt ActualDebitAmount, ad.LocalDebit_Amt DebitAmount,ad.Credit_Amt ActualCreditAmount,  ad.LocalCredit_Amt CreditAmount, ad.RefNo, ad.Cheque_No, ad.Cheque_Miti, ad.Cheque_Date, ad.Voucher_Date , ad.Voucher_Miti, ad.Narration, ad.Remarks, ad.PartyName
								   FROM AMS.AccountDetails ad
								   WHERE ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}";
        cmdString += GetReports.IsIncludePdc ? @" " : " AND ad.Module NOT IN ('PDC','PROV')";
        cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND ad.Ledger_ID in ({GetReports.LedgerId})" : " ";
        cmdString += @" )
			SELECT ld.Module,";
        cmdString += GetReports.IsPostingDetails
            ? " CASE WHEN ISNULL(ld.Voucher_No,'') =''  THEN AMS.ProperCase(md.ModuleType) ELSE ISNULL(pl.GLName,AMS.ProperCase(md.ModuleType)) + ' >> : ['+ld.Voucher_No+'] 'END PartyLedger,  "
            : " CASE WHEN ISNULL(ld.RefNo,'') = '' THEN  ( CASE WHEN ISNULL(ld.Voucher_No,'') = ''  THEN AMS.ProperCase(md.ModuleType) ELSE AMS.ProperCase(md.ModuleType) + ' >> : ['+ld.Voucher_No+']' END) ELSE AMS.ProperCase(md.ModuleType) + ' >> : ['+ld.Voucher_No+']' + ' - [ REF VNO  - ' + ld.RefNo + ' ]' END PartyLedger, ";
        cmdString +=
             $@" ld.Voucher_No VoucherNo,CASE WHEN ld.Module = 'OB' THEN '' ELSE CONVERT(NVARCHAR(10), ld.Voucher_Date, 103) END VoucherDate, ld.Voucher_Miti VoucherMiti, ld.Ledger_ID LedgerId, gl.GLName LedgerDesc, gl.GLCode, ld.RefNo, ld.Cheque_No, ld.Cheque_Miti, ld.Cheque_Date, ld.Voucher_Date, ld.Voucher_Miti, ld.Narration, ld.Remarks, ld.PartyName,ld.CurrencyId,c.CCode,ld.CurrencyRate,
                CASE WHEN ISNULL(ld.ActualDebitAmount,0) > 0 THEN  CAST(ld.ActualDebitAmount AS VARCHAR) ELSE '' END ActualDebitAmount, 
                CASE WHEN ISNULL(ld.DebitAmount,0) > 0 THEN  CAST(ld.DebitAmount AS VARCHAR) ELSE '' END DebitAmount, 
                CASE WHEN ISNULL(ABS(ld.ActualCreditAmount),0) > 0 THEN  CAST(ABS(ld.ActualCreditAmount) AS VARCHAR) ELSE '' END ActualCreditAmount,
                CASE WHEN ISNULL(ABS(ld.CreditAmount),0) > 0 THEN  CAST(ABS(ld.CreditAmount) AS VARCHAR) ELSE '' END CreditAmount, '' Balance,'' BalanceType,0 IsGroup
              FROM LedgerDetails ld
            	 LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ld.Module
            	 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ld.Ledger_ID
            	 LEFT OUTER JOIN AMS.GeneralLedger pl ON pl.GLID=ld.CbLedger_ID
                 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
            	 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
            	 LEFT OUTER JOIN AMS.Currency c ON c.CId = ld.CurrencyId
                 WHERE 1=1 ";
        if (GetReports.AccountType.IsValueExits())
        {
            cmdString += GetReports.AccountType.ToUpper() switch
            {
                "CUSTOMER" => @" AND gl.GLType IN ('Customer','Both') ",
                "VENDOR" => @" AND gl.GLType IN ('Vendor','Both') ",
                "CASH" => @" AND gl.GLType = 'Cash' ",
                "BANK" => @" AND gl.GLType = 'Bank' ",
                "OTHER" => @" AND gl.GLType = 'Other' ",
                _ => string.Empty
            };
        }

        cmdString += GetReports.FilterFor switch
        {
            "Account Group/Ledger" => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC,ld.Voucher_No;",
            "Account Group/Sub Group/Ledger" => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC, ld.Voucher_No; ",
            _ => @"
			ORDER BY gl.GLName, ld.Voucher_Date,CASE WHEN ag.GrpType IN ('A','E') THEN ld.DebitAmount ELSE ld.CreditAmount END DESC, ld.Voucher_No; "
        };
        return cmdString;
    }
    public DataTable GetGeneralLedgerDetailsReport()
    {
        var cmdString = GetReports.FilterFor switch
        {
            "Ledger/SubLedger" or "SubLedger/Ledger" => GetSubledgerLedgerDetailsReportScript(),
            "Ledger/Agent" or "Agent/Ledger" => GetDocAgentLedgerDetailsReportScript(),
            "Ledger/Department" or "Department/Ledger" => GetDepartmentLedgerDetailsReportScript(),
            _ => GetLedgerDetailsReportScript()
        };
        var dtReport = new DataTable();
        try
        {

            var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            if (dtLedger.Rows.Count <= 0)
            {
                return dtReport;
            }

            if (GetReports.FilterFor is "Ledger" || string.IsNullOrEmpty(GetReports.FilterFor))
            {
                dtReport = GetReports.CurrencyType switch
                {
                    "Foreign" => ReturnLedgerDetailsCurrencyTypeReport(dtLedger),
                    _ => ReturnLedgerDetailsReport(dtLedger)
                };
            }
            else
            {
                dtReport = GetReports.FilterFor switch
                {
                    "Ledger" => ReturnLedgerDetailsReport(dtLedger),
                    "Ledger/SubLedger" => ReturnLedgerDetailsIncludeSubLedgerReport(dtLedger),
                    "SubLedger/Ledger" => ReturnSubLedgerDetailsIncludeLedgerReport(dtLedger),
                    "Account Group" => ReturnLedgerDetailsAccountGroupOnlyReport(dtLedger),
                    "Account Group/Ledger" => ReturnLedgerDetailsAccountGroupWiseReport(dtLedger),
                    "Account Group/Sub Group" => ReturnLedgerDetailsAccountGroupSubGroupOnlyReport(dtLedger),
                    "Account Group/Sub Group/Ledger" => ReturnLedgerDetailsAccountSubGroupWiseReport(dtLedger),
                    _ => dtReport
                };
            }

            return dtReport;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
            return dtReport;
        }
    }

    #endregion **---------- DETTAILS ----------**

    // GENERAL LEDGER SUMMARY REPORTS

    #region **---------- SUMMARY ----------**

    private DataTable ReturnLedgerSummaryReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Copy();

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerDesc"] = "GRAND TOTAL :- ";

        newRow["OpeningDebit"] = dtGrand.AsEnumerable().Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
        newRow["OpeningCredit"] = dtGrand.AsEnumerable().Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

        newRow["LocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
        newRow["LocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

        newRow["ClosingDebit"] = dtGrand.AsEnumerable().Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
        newRow["ClosingCredit"] = dtGrand.AsEnumerable().Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnLedgerSummaryAccountGroupOnlyReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        using var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("GrpName")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow item in dtLedgerGroup.Rows)
        {
            var dtGroupDetails = dtLedger.Select($"GrpName='{item["GrpName"]}'");
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = item["GrpId"].ToString();
            newRow["dt_ShortName"] = item["GrpCode"].ToString();
            newRow["dt_Ledger"] = item["GrpName"].ToString();

            newRow["dt_OpeningDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_DEBIT"].GetDecimal());
            newRow["dt_OpeningLocalDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_DEBIT"].GetDecimal());

            newRow["dt_OpeningCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_CREDIT"].GetDecimal());
            newRow["dt_OpeningLocalCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_CREDIT"].GetDecimal());

            newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
            newRow["dt_PeriodicLocalDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_DEBIT"].GetDecimal());

            newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
            newRow["dt_PeriodicLocalCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_CREDIT"].GetDecimal());

            newRow["dt_ClosingDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_DEBIT"].GetDecimal());
            newRow["dt_ClosingLocalDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_DEBIT"].GetDecimal());

            newRow["dt_ClosingCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_CREDIT"].GetDecimal());
            newRow["dt_ClosingLocalCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_CREDIT"].GetDecimal());
            newRow["IsGroup"] = 0;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        newRow["dt_OpeningLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningLocalDebit"].GetDecimal());

        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());
        newRow["dt_OpeningLocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningLocalCredit"].GetDecimal());

        newRow["dt_PeriodicDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        newRow["dt_PeriodicLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicLocalDebit"].GetDecimal());

        newRow["dt_PeriodicCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
        newRow["dt_PeriodicLocalCredit"] =
            dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicLocalCredit"].GetDecimal());

        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        newRow["dt_ClosingLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingLocalDebit"].GetDecimal());

        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());
        newRow["dt_ClosingLocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingLocalCredit"].GetDecimal());

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerSummaryAccountGroupAccountSubGroupOnlyReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        using var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("GrpName")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow item in dtLedgerGroup.Rows)
        {
            var dtGroupDetails = dtLedger.Select($"GrpName='{item["GrpName"]}'").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = item["GrpId"].ToString();
            newRow["dt_ShortName"] = item["GrpCode"].ToString();
            newRow["dt_Ledger"] = item["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtLedgerSubGroup = dtGroupDetails.AsEnumerable().GroupBy(r => new
            {
                LOGSTEP = r.Field<string>("SubGrpName")
            }).Select(g => g.First()).OrderBy(r => r.Field<string>("SubGrpName")).CopyToDataTable();
            foreach (DataRow roSub in dtLedgerSubGroup.Rows)
            {
                var dtSubGroupDetails =
                    dtGroupDetails.Select($"SubGrpName='{roSub["SubGrpName"]}'").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roSub["SubGrpId"].ToString();
                newRow["dt_ShortName"] = roSub["SubGrpCode"].ToString();
                newRow["dt_Ledger"] = roSub["SubGrpName"].ToString();

                newRow["dt_OpeningDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_DEBIT"].GetDecimal());
                newRow["dt_OpeningLocalDebit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_DEBIT"].GetDecimal());

                newRow["dt_OpeningCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_CREDIT"].GetDecimal());
                newRow["dt_OpeningLocalCredit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_CREDIT"].GetDecimal());

                newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                newRow["dt_PeriodicLocalDebit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_DEBIT"].GetDecimal());

                newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
                newRow["dt_PeriodicLocalCredit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_CREDIT"].GetDecimal());

                newRow["dt_ClosingDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_DEBIT"].GetDecimal());
                newRow["dt_ClosingLocalDebit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_DEBIT"].GetDecimal());

                newRow["dt_ClosingCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_CREDIT"].GetDecimal());
                newRow["dt_ClosingLocalCredit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_CREDIT"].GetDecimal());
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = $"{item["GrpName"]} TOTAL >>";
            newRow["dt_OpeningDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_DEBIT"].GetDecimal());
            newRow["dt_OpeningLocalDebit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_DEBIT"].GetDecimal());

            newRow["dt_OpeningCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_CREDIT"].GetDecimal());
            newRow["dt_OpeningLocalCredit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_CREDIT"].GetDecimal());

            newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
            newRow["dt_PeriodicLocalDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_DEBIT"].GetDecimal());

            newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
            newRow["dt_PeriodicLocalCredit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_CREDIT"].GetDecimal());

            newRow["dt_ClosingDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_DEBIT"].GetDecimal());
            newRow["dt_ClosingLocalDebit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_DEBIT"].GetDecimal());

            newRow["dt_ClosingCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_CREDIT"].GetDecimal());
            newRow["dt_ClosingLocalCredit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_CREDIT"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        newRow["dt_OpeningLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningLocalDebit"].GetDecimal());

        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());
        newRow["dt_OpeningLocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningLocalCredit"].GetDecimal());

        newRow["dt_PeriodicDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        newRow["dt_PeriodicLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicLocalDebit"].GetDecimal());

        newRow["dt_PeriodicCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
        newRow["dt_PeriodicLocalCredit"] =
            dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicLocalCredit"].GetDecimal());

        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        newRow["dt_ClosingLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingLocalDebit"].GetDecimal());

        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());
        newRow["dt_ClosingLocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingLocalCredit"].GetDecimal());

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private DataTable ReturnLedgerSummaryIncludeSubLedgerReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();

        using var subLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("SLName")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("SLName")).CopyToDataTable();

        foreach (DataRow roGroup in subLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["ShortName"] = roGroup["SLCode"].ToString();
            newRow["LedgerDesc"] = roGroup["SLName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var subLedgerDetails = dtLedger.Select($"SLName='{roGroup["SLName"]}'").CopyToDataTable();
            var subLedgerTotal = subLedgerDetails.AsEnumerable();

            decimal amount = 0;
            decimal balance = 0;
            foreach (DataRow row in subLedgerDetails.Rows)
            {
                var debitAmount = row["OpeningDebit"].GetDecimal() + row["LocalDebit"].GetDecimal();
                var creditAmount = row["OpeningCredit"].GetDecimal() + row["LocalCredit"].GetDecimal();
                amount = debitAmount - creditAmount;
                balance = row["LocalDebit"].GetDecimal() - row["LocalCredit"].GetDecimal();

                row["LedgerDesc"] = "          " + row["LedgerDesc"];
                row["ClosingDebit"] = amount > 0 ? amount.GetDecimalComma() : "";
                row["ClosingCredit"] = amount < 0 ? Math.Abs(amount).GetDecimalComma() : "";
                row["Balance"] = Math.Abs(balance) > 0 ? Math.Abs(balance).GetDecimalComma() : "";
                row["BalanceType"] = balance > 0 ? "Dr" : balance < 0 ? "Cr" : "";
            }

            subLedgerTotal.Take(subLedgerDetails.Rows.Count).CopyToDataTable(dtReport, LoadOption.PreserveChanges);

            newRow = dtReport.NewRow();
            newRow["LedgerDesc"] = $"[{roGroup["SLName"]}] TOTAL >> ";
            newRow["OpeningDebit"] = subLedgerTotal.Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
            newRow["OpeningCredit"] = subLedgerTotal.Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

            newRow["LocalDebit"] = subLedgerTotal.Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
            newRow["LocalCredit"] = subLedgerTotal.Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

            newRow["ClosingDebit"] = subLedgerTotal.Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
            newRow["ClosingCredit"] = subLedgerTotal.Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }


        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerDesc"] = "[GRAND TOTAL]>> ";

        var grand = dtGrand.AsEnumerable();
        newRow["OpeningDebit"] = grand.Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
        newRow["OpeningCredit"] = grand.Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

        newRow["LocalDebit"] = grand.Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
        newRow["LocalCredit"] = grand.Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

        newRow["ClosingDebit"] = grand.Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
        newRow["ClosingCredit"] = grand.Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnSubLedgerSummaryIncludeLedgerReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = dtLedger.Clone();

        using var subLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("LedgerDesc")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("LedgerDesc")).CopyToDataTable();

        foreach (DataRow roGroup in subLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["ShortName"] = roGroup["ShortName"].ToString();
            newRow["LedgerDesc"] = roGroup["LedgerDesc"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var subLedgerDetails = dtLedger.Select($"LedgerDesc='{roGroup["LedgerDesc"]}'").CopyToDataTable();
            var subLedgerTotal = subLedgerDetails.AsEnumerable();

            decimal amount = 0;
            decimal balance = 0;
            foreach (DataRow row in subLedgerDetails.Rows)
            {
                var debitAmount = row["OpeningDebit"].GetDecimal() + row["LocalDebit"].GetDecimal();
                var creditAmount = row["OpeningCredit"].GetDecimal() + row["LocalCredit"].GetDecimal();
                amount = debitAmount - creditAmount;
                balance = row["LocalDebit"].GetDecimal() - row["LocalCredit"].GetDecimal();

                row["ShortName"] = row["SLCode"];
                row["LedgerDesc"] = "          " + row["SLName"];
                row["ClosingDebit"] = amount > 0 ? amount.GetDecimalComma() : "";
                row["ClosingCredit"] = amount < 0 ? Math.Abs(amount).GetDecimalComma() : "";
                row["Balance"] = Math.Abs(balance) > 0 ? Math.Abs(balance).GetDecimalComma() : "";
                row["BalanceType"] = balance > 0 ? "Dr" : balance < 0 ? "Cr" : "";
            }

            subLedgerTotal.Take(subLedgerDetails.Rows.Count).CopyToDataTable(dtReport, LoadOption.PreserveChanges);

            newRow = dtReport.NewRow();
            newRow["LedgerDesc"] = $"[{roGroup["LedgerDesc"]}] TOTAL >> ";
            newRow["OpeningDebit"] = subLedgerTotal.Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
            newRow["OpeningCredit"] = subLedgerTotal.Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

            newRow["LocalDebit"] = subLedgerTotal.Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
            newRow["LocalCredit"] = subLedgerTotal.Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

            newRow["ClosingDebit"] = subLedgerTotal.Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
            newRow["ClosingCredit"] = subLedgerTotal.Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }


        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerDesc"] = "[GRAND TOTAL]>> ";

        var grand = dtGrand.AsEnumerable();
        newRow["OpeningDebit"] = grand.Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
        newRow["OpeningCredit"] = grand.Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

        newRow["LocalDebit"] = grand.Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
        newRow["LocalCredit"] = grand.Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

        newRow["ClosingDebit"] = grand.Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
        newRow["ClosingCredit"] = grand.Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnLedgerSummaryAccountGroupWiseReport(DataTable dtLedger)
    {
        DataRow newRow;

        var dtReport = dtLedger.Clone();

        using var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("GrpName")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("GrpName")).CopyToDataTable();

        foreach (DataRow roGroup in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["ShortName"] = roGroup["GrpCode"].ToString();
            newRow["LedgerDesc"] = roGroup["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

            var dtGroupDetails = dtLedger.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
            var groupDetail = dtGroupDetails.AsEnumerable();
            foreach (DataRow row in dtGroupDetails.Rows)
            {
                row["LedgerDesc"] = "          " + row["LedgerDesc"];
            }

            groupDetail.Take(dtGroupDetails.Rows.Count).CopyToDataTable(dtReport, LoadOption.PreserveChanges);

            newRow = dtReport.NewRow();
            newRow["LedgerDesc"] = $"{roGroup["GrpName"]} TOTAL >> ";
            newRow["OpeningDebit"] = groupDetail.Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
            newRow["OpeningCredit"] = groupDetail.Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

            newRow["LocalDebit"] = groupDetail.Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
            newRow["LocalCredit"] = groupDetail.Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

            newRow["ClosingDebit"] = groupDetail.Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
            newRow["ClosingCredit"] = groupDetail.Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["LedgerDesc"] = "GRAND TOTAL :- ";

        newRow["OpeningDebit"] = dtGrand.AsEnumerable().Sum(x => x["OpeningDebit"].GetDecimal()).GetDecimalComma();
        newRow["OpeningCredit"] = dtGrand.AsEnumerable().Sum(x => x["OpeningCredit"].GetDecimal()).GetDecimalComma();

        newRow["LocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["LocalDebit"].GetDecimal()).GetDecimalComma();
        newRow["LocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["LocalCredit"].GetDecimal()).GetDecimalComma();

        newRow["ClosingDebit"] = dtGrand.AsEnumerable().Sum(x => x["ClosingDebit"].GetDecimal()).GetDecimalComma();
        newRow["ClosingCredit"] = dtGrand.AsEnumerable().Sum(x => x["ClosingCredit"].GetDecimal()).GetDecimalComma();

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

        return dtReport;
    }

    private DataTable ReturnLedgerSummaryAccountSubgropWiseReport(DataTable dtLedger)
    {
        DataRow newRow;
        var dtReport = GetLedgerSummaryAndTrialPeriodicTrialBalanceReportFormat();
        var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("GrpName")
        }).Select(g => g.First()).OrderBy(r => r.Field<string>("GrpName")).CopyToDataTable();
        foreach (DataRow roGroup in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_LedgerId"] = roGroup["GrpId"].ToString();
            newRow["dt_ShortName"] = roGroup["GrpCode"].ToString();
            newRow["dt_Ledger"] = roGroup["GrpName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            var dtGroupDetails = dtLedger.Select($"GrpName='{roGroup["GrpName"]}'").CopyToDataTable();
            var dtSubLedgerGroup = dtGroupDetails.AsEnumerable().GroupBy(r => new
            {
                LOGSTEP = r.Field<string>("SubGrpName")
            }).Select(g => g.First()).OrderBy(r => r.Field<string>("SubGrpName")).CopyToDataTable();
            foreach (DataRow roSub in dtSubLedgerGroup.Rows)
            {
                newRow = dtReport.NewRow();
                newRow["dt_LedgerId"] = roSub["SubGrpId"].ToString();
                newRow["dt_ShortName"] = roSub["SubGrpCode"].ToString();
                newRow["dt_Ledger"] = "     " + roSub["SubGrpName"];
                newRow["IsGroup"] = 2;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var dtSubGroupDetails =
                    dtGroupDetails.Select($"SubGrpName='{roSub["SubGrpName"]}'").CopyToDataTable();
                foreach (DataRow item in dtSubGroupDetails.Rows)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_LedgerId"] = item["Ledger_ID"].ToString();
                    newRow["dt_ShortName"] = item["ShortName"].ToString();
                    newRow["dt_Ledger"] = "          " + item["GLName"];

                    newRow["dt_OpeningDebit"] = item["OB_DEBIT"].ToString();
                    newRow["dt_OpeningLocalDebit"] = item["OB_LOCAL_DEBIT"].ToString();

                    newRow["dt_OpeningCredit"] = item["OB_CREDIT"].ToString();
                    newRow["dt_OpeningLocalCredit"] = item["OB_LOCAL_CREDIT"].ToString();

                    newRow["dt_PeriodicDebit"] = item["DEBIT"].ToString();
                    newRow["dt_PeriodicLocalDebit"] = item["LOCAL_DEBIT"].ToString();

                    newRow["dt_PeriodicCredit"] = item["CREDIT"].ToString();
                    newRow["dt_PeriodicLocalCredit"] = item["LOCAL_CREDIT"].ToString();

                    newRow["dt_Balance"] = item["BALANCE"].ToString();
                    newRow["dt_LocalBalance"] = item["LOCAL_BALANCE"].ToString();

                    newRow["dt_BalanceType"] = item["BType"].ToString();

                    newRow["dt_ClosingDebit"] = item["CB_DEBIT"].ToString();
                    newRow["dt_ClosingLocalDebit"] = item["CB_LOCAL_DEBIT"].ToString();

                    newRow["dt_ClosingCredit"] = item["CB_CREDIT"].ToString();
                    newRow["dt_ClosingLocalCredit"] = item["CB_LOCAL_CREDIT"].ToString();

                    newRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Ledger"] = $"{roSub["SubGrpName"]} TOTAL >> ";
                newRow["dt_OpeningDebit"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["OB_DEBIT"].GetDecimal());
                newRow["dt_OpeningLocalDebit"] =
                    dtSubGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_DEBIT"].GetDecimal());

                newRow["dt_OpeningCredit"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["OB_CREDIT"].GetDecimal());
                newRow["dt_OpeningLocalCredit"] =
                    dtSubGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_CREDIT"].GetDecimal());

                newRow["dt_PeriodicDebit"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                newRow["dt_PeriodicLocalDebit"] =
                    dtSubGroupDetails.AsEnumerable().Sum(x => x["LOCAL_DEBIT"].GetDecimal());

                newRow["dt_PeriodicCredit"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
                newRow["dt_PeriodicLocalCredit"] =
                    dtSubGroupDetails.AsEnumerable().Sum(x => x["LOCAL_CREDIT"].GetDecimal());

                newRow["dt_ClosingDebit"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["CB_DEBIT"].GetDecimal());
                newRow["dt_ClosingLocalDebit"] =
                    dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_DEBIT"].GetDecimal());

                newRow["dt_ClosingCredit"] = dtSubGroupDetails.AsEnumerable().Sum(x => x["CB_CREDIT"].GetDecimal());
                newRow["dt_ClosingLocalCredit"] =
                    dtSubGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_CREDIT"].GetDecimal());
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            newRow = dtReport.NewRow();
            newRow["dt_Ledger"] = $"{roGroup["GrpName"]} TOTAL >> ";
            newRow["dt_OpeningDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_DEBIT"].GetDecimal());
            newRow["dt_OpeningLocalDebit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_DEBIT"].GetDecimal());

            newRow["dt_OpeningCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["OB_CREDIT"].GetDecimal());
            newRow["dt_OpeningLocalCredit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["OB_LOCAL_CREDIT"].GetDecimal());

            newRow["dt_PeriodicDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
            newRow["dt_PeriodicLocalDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_DEBIT"].GetDecimal());

            newRow["dt_PeriodicCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
            newRow["dt_PeriodicLocalCredit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["LOCAL_CREDIT"].GetDecimal());

            newRow["dt_ClosingDebit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_DEBIT"].GetDecimal());
            newRow["dt_ClosingLocalDebit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_DEBIT"].GetDecimal());

            newRow["dt_ClosingCredit"] = dtGroupDetails.AsEnumerable().Sum(x => x["CB_CREDIT"].GetDecimal());
            newRow["dt_ClosingLocalCredit"] =
                dtGroupDetails.AsEnumerable().Sum(x => x["CB_LOCAL_CREDIT"].GetDecimal());
            newRow["IsGroup"] = 11;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        }

        var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
        newRow = dtReport.NewRow();
        newRow["dt_Ledger"] = "GRAND TOTAL :- ";
        newRow["dt_OpeningDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningDebit"].GetDecimal());
        newRow["dt_OpeningLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningLocalDebit"].GetDecimal());

        newRow["dt_OpeningCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningCredit"].GetDecimal());
        newRow["dt_OpeningLocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_OpeningLocalCredit"].GetDecimal());

        newRow["dt_PeriodicDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicDebit"].GetDecimal());
        newRow["dt_PeriodicLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicLocalDebit"].GetDecimal());

        newRow["dt_PeriodicCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicCredit"].GetDecimal());
        newRow["dt_PeriodicLocalCredit"] =
            dtGrand.AsEnumerable().Sum(x => x["dt_PeriodicLocalCredit"].GetDecimal());

        newRow["dt_ClosingDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingDebit"].GetDecimal());
        newRow["dt_ClosingLocalDebit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingLocalDebit"].GetDecimal());

        newRow["dt_ClosingCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingCredit"].GetDecimal());
        newRow["dt_ClosingLocalCredit"] = dtGrand.AsEnumerable().Sum(x => x["dt_ClosingLocalCredit"].GetDecimal());

        newRow["IsGroup"] = 99;
        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
        return dtReport;
    }

    private string GetDocAgentLedgerSummaryReportScript()
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			WITH LedgerSummery
			  AS
			  (
				SELECT LedgerOpening.Ledger_ID,0 AgentId, CASE WHEN SUM ( LedgerOpening.OB_DEBIT - LedgerOpening.OB_CREDIT ) > 0 THEN SUM ( LedgerOpening.OB_DEBIT - LedgerOpening.OB_CREDIT ) ELSE 0 END OB_DEBIT, CASE WHEN SUM ( LedgerOpening.OB_CREDIT - LedgerOpening.OB_DEBIT ) > 0 THEN SUM ( LedgerOpening.OB_CREDIT - LedgerOpening.OB_DEBIT ) ELSE 0 END OB_CREDIT, SUM ( LedgerOpening.OB_LOCAL_DEBIT ) OB_LOCAL_DEBIT, SUM ( LedgerOpening.OB_LOCAL_CREDIT ) OB_LOCAL_CREDIT, SUM ( LedgerOpening.DEBIT ) DEBIT, SUM ( LedgerOpening.CREDIT ) CREDIT, SUM ( LedgerOpening.LOCAL_DEBIT ) LOCAL_DEBIT, SUM ( LedgerOpening.LOCAL_CREDIT ) LOCAL_CREDIT
				 FROM (
						SELECT ad.Ledger_ID, CASE WHEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) > 0 THEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) ELSE 0 END OB_DEBIT, CASE WHEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) > 0 THEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) ELSE 0 END OB_CREDIT, CASE WHEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) > 0 THEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) ELSE 0 END OB_LOCAL_DEBIT, CASE WHEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) > 0 THEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) ELSE 0 END OB_LOCAL_CREDIT, 0 DEBIT, 0 LOCAL_DEBIT, 0 CREDIT, 0 LOCAL_CREDIT
						 FROM AMS.AccountDetails ad
							  LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
							  LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						 WHERE ad.FiscalYearId < {ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp <> 'Profit & LOSS' AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ");
        if (!GetReports.IsIncludePdc)
        {
            cmdString.Append(@"
				AND Module NOT IN ('PDC', 'PROV') ");
        }

        cmdString.Append($@"
						 GROUP BY ad.Ledger_ID
						UNION ALL
						SELECT ad.Ledger_ID,CASE WHEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) > 0 THEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) ELSE 0 END OB_DEBIT, CASE WHEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) > 0 THEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) ELSE 0 END OB_CREDIT, CASE WHEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) > 0 THEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) ELSE 0 END OB_LOCAL_DEBIT, CASE WHEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) > 0 THEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) ELSE 0 END OB_LOCAL_CREDIT, 0 DEBIT, 0 LOCAL_DEBIT, 0 CREDIT, 0 LOCAL_CREDIT
						 FROM AMS.AccountDetails ad
						 WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date < '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND Module NOT IN ('PDC', 'PROV')
						 GROUP BY ad.Ledger_ID
					  ) AS LedgerOpening
				 GROUP BY LedgerOpening.Ledger_ID
				UNION ALL
				SELECT ad.Ledger_ID,ad.Agent_ID, 0 OB_DEBIT, 0 OB_LOCAL_DEBIT, 0 OB_CREDIT, 0 OB_LOCAL_CREDIT, SUM ( ad.Debit_Amt ) DEBIT, SUM ( ad.Credit_Amt ) CREDIT, SUM ( ad.LocalDebit_Amt ) LOCAL_DEBIT, SUM ( ad.LocalCredit_Amt ) LOCAL_CREDIT
				 FROM AMS.AccountDetails ad
				 WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date >= '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}'  AND ad.Voucher_Date <='{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}' AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ");
        if (!GetReports.IsIncludePdc)
        {
            cmdString.Append(@"
				AND Module NOT IN ('PDC', 'PROV') ");
        }

        cmdString.Append(@"
			GROUP BY ad.Ledger_ID,ad.Agent_ID
			  )
			 SELECT ls.Ledger_ID, gl.GLCode ShortName, gl.GLName,ls.AgentId Subleder_ID ,ISNULL(sl.AgentName,'NO-AGENT') SLName,gl.GrpId, ag.GrpName, ag.GrpCode, gl.SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUB-GROUP' ) SubGrpName, asg.SubGrpCode, SUM ( ls.OB_DEBIT ) OB_DEBIT, SUM ( ls.OB_CREDIT ) OB_CREDIT, SUM ( ls.OB_LOCAL_DEBIT ) OB_LOCAL_DEBIT, SUM ( ls.OB_LOCAL_CREDIT ) OB_LOCAL_CREDIT, SUM ( ls.DEBIT ) DEBIT, SUM ( ls.CREDIT ) CREDIT, SUM ( ls.LOCAL_DEBIT ) LOCAL_DEBIT, SUM ( ls.LOCAL_CREDIT ) LOCAL_CREDIT, ABS ( SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT )) BALANCE, ABS ( SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT )) LOCAL_BALANCE, CASE WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) > 0 THEN 'Dr' WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) < 0 THEN 'Cr' ELSE '' END BType, CASE WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) > 0 THEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) ELSE 0 END CB_DEBIT, CASE WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) < 0 THEN ABS ( SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT )) ELSE 0 END CB_CREDIT, CASE WHEN SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT ) > 0 THEN SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT ) ELSE 0 END CB_LOCAL_DEBIT, CASE WHEN SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT ) < 0 THEN ABS ( SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT )) ELSE 0 END CB_LOCAL_CREDIT
			  FROM LedgerSummery ls
					LEFT OUTER JOIN AMS.GeneralLedger gl ON ls.Ledger_ID = gl.GLID
					LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
					LEFT OUTER JOIN AMS.JuniorAgent sl ON ls.AgentId = sl.AgentId
			  WHERE 1 = 1 ");
        if (!string.IsNullOrEmpty(GetReports.GroupId))
        {
            cmdString.Append($@" AND ag.GrpId in ({GetReports.GroupId})");
        }

        if (!string.IsNullOrEmpty(GetReports.SubGroupId))
        {
            cmdString.Append($@"AND asg.SubGrpId in ({GetReports.SubGroupId}) ");
        }

        if (!string.IsNullOrEmpty(GetReports.LedgerId))
        {
            cmdString.Append($@"AND ls.Ledger_ID in ({GetReports.LedgerId}) ");
        }

        if (!string.IsNullOrEmpty(GetReports.AgentId))
        {
            cmdString.Append($@"AND ls.AgentId in ({GetReports.AgentId}) ");
        }

        if (!string.IsNullOrEmpty(GetReports.AccountType) && string.IsNullOrEmpty(GetReports.LedgerId))
        {
            cmdString.Append(GetReports.AccountType.ToUpper() == "CUSTOMER"
                ? @" AND gl.GLType IN ('Customer','Both')"
                : GetReports.AccountType.ToUpper() == "VENDOR"
                    ? @" AND gl.GLType IN ('Vendor','Both') "
                    : GetReports.AccountType.ToUpper() == "CASH"
                        ? @" AND gl.GLType IN ('Cash') "
                        : GetReports.AccountType.ToUpper() == "BANK"
                            ? @" AND gl.GLType IN ('Bank') "
                            : GetReports.AccountType.ToUpper() == "OTHER"
                                ? @" AND gl.GLType IN ('Other') "
                                : string.Empty);
        }

        cmdString.Append(@"
			  GROUP BY ls.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, ag.GrpType, gl.SubGrpId, asg.SubGrpName, ag.GrpCode, asg.SubGrpCode,ls.AgentId,sl.AgentName ");
        cmdString.Append(GetReports.IsZeroBalance
            ? " HAVING ABS(SUM(ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT)) > 0 \n"
            : " HAVING ABS(SUM(ls.OB_DEBIT + ls.OB_CREDIT + ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT + ls.OB_LOCAL_CREDIT + ls.LOCAL_CREDIT)) <> 0  \n");
        cmdString.Append(@"
			  ORDER BY gl.GLName;");
        return cmdString.ToString();
    }

    private string GetDepartmentLedgerSummaryReportScript()
    {
        var cmdString = new StringBuilder();
        cmdString.Append($@"
			WITH LedgerSummery
			  AS
			  (
				SELECT ad.Ledger_ID,ad.Department_ID1, 0 OB_DEBIT, 0 OB_LOCAL_DEBIT, 0 OB_CREDIT, 0 OB_LOCAL_CREDIT, SUM ( ad.Debit_Amt ) DEBIT, SUM ( ad.Credit_Amt ) CREDIT, SUM ( ad.LocalDebit_Amt ) LOCAL_DEBIT, SUM ( ad.LocalCredit_Amt ) LOCAL_CREDIT
				 FROM (
						SELECT ad.Ledger_ID, CASE WHEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) > 0 THEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) ELSE 0 END OB_DEBIT, CASE WHEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) > 0 THEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) ELSE 0 END OB_CREDIT, CASE WHEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) > 0 THEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) ELSE 0 END OB_LOCAL_DEBIT, CASE WHEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) > 0 THEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) ELSE 0 END OB_LOCAL_CREDIT, 0 DEBIT, 0 LOCAL_DEBIT, 0 CREDIT, 0 LOCAL_CREDIT
						 FROM AMS.AccountDetails ad
							  LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
							  LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
						 WHERE ad.FiscalYearId < {ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp <> 'Profit & LOSS' AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ");
        if (!GetReports.IsIncludePdc)
        {
            cmdString.Append(@"
				AND Module NOT IN ('PDC', 'PROV') ");
        }

        cmdString.Append($@"
						 GROUP BY ad.Ledger_ID
						UNION ALL
						SELECT ad.Ledger_ID,CASE WHEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) > 0 THEN SUM ( ad.Debit_Amt - ad.Credit_Amt ) ELSE 0 END OB_DEBIT, CASE WHEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) > 0 THEN SUM ( ad.Credit_Amt - ad.Debit_Amt ) ELSE 0 END OB_CREDIT, CASE WHEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) > 0 THEN SUM ( ad.LocalDebit_Amt - ad.LocalCredit_Amt ) ELSE 0 END OB_LOCAL_DEBIT, CASE WHEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) > 0 THEN SUM ( ad.LocalCredit_Amt - ad.LocalDebit_Amt ) ELSE 0 END OB_LOCAL_CREDIT, 0 DEBIT, 0 LOCAL_DEBIT, 0 CREDIT, 0 LOCAL_CREDIT
						 FROM AMS.AccountDetails ad
						 WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date < '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND Module NOT IN ('PDC', 'PROV')
						 GROUP BY ad.Ledger_ID
					  ) AS LedgerOpening
				 GROUP BY LedgerOpening.Ledger_ID
				UNION ALL
				SELECT ad.Ledger_ID,ad.Department_ID1, 0 OB_DEBIT, 0 OB_LOCAL_DEBIT, 0 OB_CREDIT, 0 OB_LOCAL_CREDIT, SUM ( ad.Debit_Amt ) DEBIT, SUM ( ad.Credit_Amt ) CREDIT, SUM ( ad.LocalDebit_Amt ) LOCAL_DEBIT, SUM ( ad.LocalCredit_Amt ) LOCAL_CREDIT
				 FROM AMS.AccountDetails ad
				 WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND  ad.Voucher_Date >= '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}'  AND ad.Voucher_Date <='{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}' AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) ");
        if (!GetReports.IsIncludePdc)
        {
            cmdString.Append(@"
				AND Module NOT IN ('PDC', 'PROV') ");
        }

        cmdString.Append(@"
			GROUP BY ad.Ledger_ID,ad.Department_ID1
			  )
			 SELECT ls.Ledger_ID, gl.GLCode ShortName, gl.GLName,ls.DepartmentId Subleder_ID ,ISNULL(sl.DName,'NO-DEPARTMENT') SLName,gl.GrpId, ag.GrpName, ag.GrpCode, gl.SubGrpId, ISNULL ( asg.SubGrpName, 'NO SUB-GROUP' ) SubGrpName, asg.SubGrpCode, SUM ( ls.OB_DEBIT ) OB_DEBIT, SUM ( ls.OB_CREDIT ) OB_CREDIT, SUM ( ls.OB_LOCAL_DEBIT ) OB_LOCAL_DEBIT, SUM ( ls.OB_LOCAL_CREDIT ) OB_LOCAL_CREDIT, SUM ( ls.DEBIT ) DEBIT, SUM ( ls.CREDIT ) CREDIT, SUM ( ls.LOCAL_DEBIT ) LOCAL_DEBIT, SUM ( ls.LOCAL_CREDIT ) LOCAL_CREDIT, ABS ( SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT )) BALANCE, ABS ( SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT )) LOCAL_BALANCE, CASE WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) > 0 THEN 'Dr' WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) < 0 THEN 'Cr' ELSE '' END BType, CASE WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) > 0 THEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) ELSE 0 END CB_DEBIT, CASE WHEN SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT ) < 0 THEN ABS ( SUM ( ls.OB_DEBIT + ls.DEBIT - ls.OB_CREDIT - ls.CREDIT )) ELSE 0 END CB_CREDIT, CASE WHEN SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT ) > 0 THEN SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT ) ELSE 0 END CB_LOCAL_DEBIT, CASE WHEN SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT ) < 0 THEN ABS ( SUM ( ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT )) ELSE 0 END CB_LOCAL_CREDIT
			  FROM LedgerSummery ls
					LEFT OUTER JOIN AMS.GeneralLedger gl ON ls.Ledger_ID = gl.GLID
					LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
					LEFT OUTER JOIN AMS.AccountSubGroup asg ON gl.SubGrpId = asg.SubGrpId
					LEFT OUTER JOIN AMS.Department sl ON ls.DepartmentId = sl.DId
			  WHERE 1 = 1 ");
        if (!string.IsNullOrEmpty(GetReports.GroupId))
        {
            cmdString.Append($@" AND ag.GrpId in ({GetReports.GroupId})");
        }

        if (!string.IsNullOrEmpty(GetReports.SubGroupId))
        {
            cmdString.Append($@"AND asg.SubGrpId in ({GetReports.SubGroupId}) ");
        }

        if (!string.IsNullOrEmpty(GetReports.LedgerId))
        {
            cmdString.Append($@"AND ls.Ledger_ID in ({GetReports.LedgerId}) ");
        }

        if (!string.IsNullOrEmpty(GetReports.DepartmentId))
        {
            cmdString.Append($@"AND ls.DepartmentId in ({GetReports.DepartmentId}) ");
        }

        if (!string.IsNullOrEmpty(GetReports.AccountType) && string.IsNullOrEmpty(GetReports.LedgerId))
        {
            cmdString.Append(GetReports.AccountType.ToUpper() == "CUSTOMER"
                ? @" AND gl.GLType IN ('Customer','Both')"
                : GetReports.AccountType.ToUpper() == "VENDOR"
                    ? @" AND gl.GLType IN ('Vendor','Both') "
                    : GetReports.AccountType.ToUpper() == "CASH"
                        ? @" AND gl.GLType IN ('Cash') "
                        : GetReports.AccountType.ToUpper() == "BANK"
                            ? @" AND gl.GLType IN ('Bank') "
                            : GetReports.AccountType.ToUpper() == "OTHER"
                                ? @" AND gl.GLType IN ('Other') "
                                : string.Empty);
        }

        cmdString.Append(@"
			  GROUP BY ls.Ledger_ID, gl.GLName, gl.GLCode, gl.GrpId, GrpName, ag.GrpType, gl.SubGrpId, asg.SubGrpName, ag.GrpCode, asg.SubGrpCode,ls.DepartmentId,sl.DName ");
        cmdString.Append(GetReports.IsZeroBalance
            ? " HAVING ABS(SUM(ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT - ls.OB_LOCAL_CREDIT - ls.LOCAL_CREDIT)) > 0 \n"
            : " HAVING ABS(SUM(ls.OB_DEBIT + ls.OB_CREDIT + ls.OB_LOCAL_DEBIT + ls.LOCAL_DEBIT + ls.OB_LOCAL_CREDIT + ls.LOCAL_CREDIT)) <> 0  \n");
        cmdString.Append(@"
			  ORDER BY gl.GLName;");
        return cmdString.ToString();
    }

    private string GetSubledgerLedgerSummaryReportScript()
    {
        var cmdString = string.Empty;
        cmdString = @$"
        WITH SubledgerReport AS (SELECT sl.SubLederId,sl.LedgerId,SUM(sl.OpeningDebitAmount) OpeningDebit,SUM(sl.OpeningCreditAmount) OpeningCredit,SUM(sl.DebitAmount) DebitAmount,SUM(sl.CreditAmount) CreditAmount FROM (SELECT ISNULL(ad.Subleder_ID,0) SubLederId,ad.Ledger_ID LedgerId,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END OpeningDebitAmount,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) < 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END OpeningCreditAmount,0 DebitAmount,0 CreditAmount FROM AMS.AccountDetails ad
	         LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
	         LEFT OUTER JOIN AMS.AccountGroup ag ON gl.GrpId = ag.GrpId
	         LEFT OUTER JOIN AMS.Subledger s ON ad.Subleder_ID = s.SLId
        WHERE ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' AND ad.FiscalYearId < {ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp IN ('BS','Balance Sheet') ";
        if (GetReports.SubLedgerId.IsValueExits())
        {
            cmdString += $" AND ad.Subleder_ID IN ({GetReports.SubLedgerId})";
        }
        if (GetReports.LedgerId.IsValueExits())
        {
            cmdString += $" AND ad.Ledger_ID IN ({GetReports.LedgerId})";
        }

        cmdString += @$"
        GROUP BY ad.Subleder_ID,ad.Ledger_ID UNION ALL
        SELECT ISNULL(ad.Subleder_ID,0) SubLederId,ad.Ledger_ID LedgerId,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END OpeningDebitAmount,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) < 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END OpeningCreditAmount,0 DebitAmount,0 CreditAmount FROM AMS.AccountDetails ad
        WHERE ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}";
        if (GetReports.SubLedgerId.IsValueExits())
        {
            cmdString += $" AND ad.Subleder_ID IN ({GetReports.SubLedgerId})";
        }
        if (GetReports.LedgerId.IsValueExits())
        {
            cmdString += $" AND ad.Ledger_ID IN ({GetReports.LedgerId})";
        }
        cmdString += @$"
        GROUP BY ad.Subleder_ID,ad.Ledger_ID UNION ALL
        SELECT ISNULL(ad.Subleder_ID,0) SubLederId,ad.Ledger_ID LedgerId,0 OpeningDebitAmount,0 OpeningCreditAmount,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END DebitAmount,CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) < 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END CreditAmount FROM AMS.AccountDetails ad
        WHERE ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' ";
        if (GetReports.SubLedgerId.IsValueExits())
        {
            cmdString += $" AND ad.Subleder_ID IN ({GetReports.SubLedgerId})";
        }
        if (GetReports.LedgerId.IsValueExits())
        {
            cmdString += $" AND ad.Ledger_ID IN ({GetReports.LedgerId})";
        }
        cmdString += @$"
        GROUP BY ad.Subleder_ID,ad.Ledger_ID) sl
        GROUP BY sl.SubLederId,sl.LedgerId)
        SELECT sr.SubLederId,ISNULL(s.SLName,'NO-SUB LEDGER') SLName,s.SLCode,sr.LedgerId,gl.GLName LedgerDesc,gl.GLCode ShortName,gl.ACCode AccountingCode,
        CASE when  sr.OpeningDebit > 0 THEN FORMAT(sr.OpeningDebit,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END OpeningDebit,
        CASE when  sr.OpeningCredit > 0 THEN FORMAT(sr.OpeningCredit,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END OpeningCredit,
        CASE when  sr.DebitAmount > 0 THEN FORMAT(sr.DebitAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END LocalDebit,
        CASE when  sr.CreditAmount > 0 THEN FORMAT(sr.CreditAmount,'{ObjGlobal.SysAmountCommaFormat}') ELSE '' END LocalCredit,'' Balance,'' BalanceType,'' ClosingDebit, '' ClosingCredit, 0 IsGroup, gl.PanNo, gl.GLAddress,gl.PhoneNo FROM SubledgerReport sr
	         LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = sr.LedgerId
	         LEFT OUTER JOIN AMS.Subledger s ON sr.SubLederId = s.SLId ";
        cmdString += GetReports.FilterFor switch
        {
            "SubLedger/Ledger" => @"
            ORDER BY SLName,gl.GLName; ",
            _ => @"
			ORDER BY gl.GLName,SLName; "
        };

        return cmdString;
    }

    private string GetLedgerSummartReportScript()
    {
        var cmdString = string.Empty;
        cmdString = @$"
			WITH LedgerReport AS (SELECT gl.GLID LedgerId, gl.GLName LedgerDesc, gl.GLCode ShortName, gl.ACCode AccountingCode, ISNULL(ob.Opening, 0) Opening, ISNULL(p.LocalDebit, 0) LocalDebit, ISNULL(p.LocalCredit, 0) LocalCredit, ISNULL(ob.Opening, 0)+ISNULL(p.LocalDebit, 0)-ISNULL(p.LocalCredit, 0) Balance
								   FROM AMS.GeneralLedger gl
									   LEFT OUTER JOIN(SELECT o.LedgerId, SUM(ISNULL(o.Opening, 0)) Opening
													   FROM(SELECT ad.Ledger_ID LedgerId, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening
															FROM AMS.AccountDetails ad
																 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
																 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
															WHERE ad.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp='BS' ";
        cmdString += GetReports.IsIncludePdc ? "" : " AND ad.Module NOT IN ('PROV','PDC') ";
        cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ({GetReports.LedgerId})" : "";
        cmdString += $@"
															GROUP BY ad.Ledger_ID
														UNION ALL
														SELECT ad.Ledger_ID LedgerId, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening
														FROM AMS.AccountDetails ad
														WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' ";
        cmdString += GetReports.IsIncludePdc ? "" : " AND ad.Module NOT IN ('PROV','PDC') ";
        cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ({GetReports.LedgerId})" : "";
        cmdString += $@"
														GROUP BY ad.Ledger_ID) o
													   GROUP BY o.LedgerId
													   HAVING SUM(ISNULL(o.Opening, 0))<>0) ob ON ob.LedgerId=gl.GLID
									   LEFT OUTER JOIN(SELECT ad.Ledger_ID LedgerId,SUM(ad.LocalDebit_Amt) LocalDebit, SUM(ad.LocalCredit_Amt) LocalCredit
													   FROM AMS.AccountDetails ad
													   WHERE ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND ad.FiscalYearId={ObjGlobal.SysFiscalYearId} ";
        cmdString += GetReports.IsIncludePdc ? "" : " AND ad.Module NOT IN ('PROV','PDC') ";
        cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ({GetReports.LedgerId})" : "";
        cmdString += @"
													   GROUP BY ad.Ledger_ID) p ON p.LedgerId=gl.GLID
													   WHERE 1=1 ";
        cmdString += GetReports.AccountType.ToUpper() switch
        {
            "CUSTOMER" => " AND gl.GLType IN ('Customer','Both') ",
            "VENDOR" => " AND gl.GLType IN ('Vendor','Both') ",
            "CASH" => " AND gl.GLType = 'Cash' ",
            "BANK" => " AND gl.GLType = 'Bank' ",
            "OTHER" => " AND gl.GLType = 'Other' ",
            _ => ""
        };
        cmdString += $@" )
			SELECT lr.LedgerId, lr.LedgerDesc, lr.ShortName, lr.AccountingCode,ag.GrpName,ag.GrpCode,ISNULL(asg.SubGrpName,'NO-SUB GROUP') SubGrpName,asg.SubGrpCode, 
            CASE WHEN lr.Opening>0 THEN FORMAT(lr.Opening, '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningDebit, 
            CASE WHEN lr.Opening<0 THEN FORMAT(ABS(lr.Opening), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningCredit, 
            FORMAT(lr.LocalDebit, '{ObjGlobal.SysAmountCommaFormat}') LocalDebit, 
            FORMAT(lr.LocalCredit, '{ObjGlobal.SysAmountCommaFormat}') LocalCredit, 
            FORMAT(ABS(lr.LocalDebit-lr.LocalCredit), '{ObjGlobal.SysAmountCommaFormat}') Balance, CASE WHEN (lr.LocalDebit-lr.LocalCredit)<0 THEN 'Cr' WHEN (lr.LocalDebit-lr.LocalCredit)>0 THEN 'Dr' ELSE '' END BalanceType, 
            CASE WHEN lr.Balance>0 THEN FORMAT(lr.Balance, '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ClosingDebit, 
            CASE WHEN lr.Balance<0 THEN FORMAT(ABS(lr.Balance), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ClosingCredit, 0 IsGroup,gl.PanNo,gl.GLAddress,gl.PhoneNo
			FROM LedgerReport lr
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON lr.LedgerId=gl.GLID
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
			WHERE ";
        cmdString += GetReports.IsZeroBalance
            ? " lr.LedgerId in (SELECT Ledger_ID FROM AMS.AccountDetails) "
            : @" lr.Balance <> 0  ";
        cmdString += GetReports.FilterFor switch
        {
            "Account Group/Ledger" => @"
			ORDER BY ag.GrpName,lr.LedgerDesc; ",
            "Account Group/Sub Group/Ledger" => @"
			ORDER BY ag.GrpName,ISNULL(asg.SubGrpName,'NO-SUB GROUP'),lr.LedgerDesc; ",
            _ => @"
			ORDER BY lr.LedgerDesc; "
        };

        return cmdString;
    }

    private string GetLedgerSummartReportOpeningScript()
    {
        var cmdString = string.Empty;
        cmdString = @$"
			WITH LedgerReport AS (SELECT gl.GLID LedgerId, gl.GLName LedgerDesc, gl.GLCode ShortName, gl.ACCode AccountingCode, ISNULL(ob.Opening, 0) Opening, ISNULL(p.LocalDebit, 0) LocalDebit, ISNULL(p.LocalCredit, 0) LocalCredit, ISNULL(ob.Opening, 0)+ISNULL(p.LocalDebit, 0)-ISNULL(p.LocalCredit, 0) Balance
								   FROM AMS.GeneralLedger gl
									   LEFT OUTER JOIN(SELECT o.LedgerId, SUM(ISNULL(o.Opening, 0)) Opening
													   FROM(SELECT ad.Ledger_ID LedgerId, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening
															FROM AMS.AccountDetails ad
																 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
																 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
															WHERE ad.FiscalYearId<{ObjGlobal.SysFiscalYearId} AND ag.PrimaryGrp='BS' ";
        cmdString += GetReports.IsIncludePdc ? "" : " AND ad.Module NOT IN ('PROV','PDC') ";
        cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ({GetReports.LedgerId})" : "";
        cmdString += $@"
															GROUP BY ad.Ledger_ID
														UNION ALL
														SELECT ad.Ledger_ID LedgerId, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Opening
														FROM AMS.AccountDetails ad
														WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date < '{GetReports.FromDate.GetSystemDate()}' ";
        cmdString += GetReports.IsIncludePdc ? "" : " AND ad.Module NOT IN ('PROV','PDC') ";
        cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ({GetReports.LedgerId})" : "";
        cmdString += $@"
														GROUP BY ad.Ledger_ID) o
													   GROUP BY o.LedgerId
													   HAVING SUM(ISNULL(o.Opening, 0))<>0) ob ON ob.LedgerId=gl.GLID
									   LEFT OUTER JOIN(SELECT ad.Ledger_ID LedgerId,SUM(ad.LocalDebit_Amt) LocalDebit, SUM(ad.LocalCredit_Amt) LocalCredit
													   FROM AMS.AccountDetails ad
													   WHERE ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND ad.FiscalYearId={ObjGlobal.SysFiscalYearId} ";
        cmdString += GetReports.IsIncludePdc ? "" : " AND ad.Module NOT IN ('PROV','PDC') ";
        cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ({GetReports.LedgerId})" : "";
        cmdString += @"
													   GROUP BY ad.Ledger_ID) p ON p.LedgerId=gl.GLID
													   WHERE 1=1 ";
        cmdString += GetReports.AccountType.ToUpper() switch
        {
            "CUSTOMER" => " AND gl.GLType IN ('Customer','Both') ",
            "VENDOR" => " AND gl.GLType IN ('Vendor','Both') ",
            "CASH" => " AND gl.GLType = 'Cash' ",
            "BANK" => " AND gl.GLType = 'Bank' ",
            "OTHER" => " AND gl.GLType = 'Other' ",
            _ => ""
        };
        cmdString += $@" )
			SELECT lr.LedgerId, lr.LedgerDesc, lr.ShortName, lr.AccountingCode,ag.GrpName,ag.GrpCode,ISNULL(asg.SubGrpName,'NO-SUB GROUP') SubGrpName,asg.SubGrpCode, 
            CASE WHEN lr.Opening>0 THEN FORMAT(lr.Opening, '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningDebit, 
            CASE WHEN lr.Opening<0 THEN FORMAT(ABS(lr.Opening), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END OpeningCredit, 
            FORMAT(lr.LocalDebit, '{ObjGlobal.SysAmountCommaFormat}') LocalDebit, 
            FORMAT(lr.LocalCredit, '{ObjGlobal.SysAmountCommaFormat}') LocalCredit, 
            FORMAT(ABS(lr.LocalDebit-lr.LocalCredit), '{ObjGlobal.SysAmountCommaFormat}') Balance, CASE WHEN (lr.LocalDebit-lr.LocalCredit)<0 THEN 'Cr' WHEN (lr.LocalDebit-lr.LocalCredit)>0 THEN 'Dr' ELSE '' END BalanceType, 
            CASE WHEN lr.Balance>0 THEN FORMAT(lr.Balance, '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ClosingDebit, 
            CASE WHEN lr.Balance<0 THEN FORMAT(ABS(lr.Balance), '{ObjGlobal.SysAmountCommaFormat}')ELSE '' END ClosingCredit, 0 IsGroup,gl.PanNo,gl.GLAddress,gl.PhoneNo
			FROM LedgerReport lr
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON lr.LedgerId=gl.GLID
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
			WHERE ";
        cmdString += GetReports.IsZeroBalance
            ? " lr.LedgerId in (SELECT Ledger_ID FROM AMS.AccountDetails) "
            : @" lr.Balance <> 0  ";
        cmdString += GetReports.FilterFor switch
        {
            "Account Group/Ledger" => @"
			ORDER BY ag.GrpName,lr.LedgerDesc; ",
            "Account Group/Sub Group/Ledger" => @"
			ORDER BY ag.GrpName,ISNULL(asg.SubGrpName,'NO-SUB GROUP'),lr.LedgerDesc; ",
            _ => @"
			ORDER BY lr.LedgerDesc; "
        };

        return cmdString;
    }

    public DataTable GetGeneralLedgerSummaryReport()
    {
        var dtReport = new DataTable();
        try
        {
            var cmdString = GetReports.FilterFor switch
            {
                "Ledger/SubLedger" or "SubLedger/Ledger" => GetSubledgerLedgerSummaryReportScript(),
                "Ledger/Agent" or "Agent/Ledger" => GetDocAgentLedgerSummaryReportScript(),
                "Ledger/Department" or "Department/Ledger" => GetDocAgentLedgerSummaryReportScript(),
                _ => GetLedgerSummartReportScript()
            };
            var dtLedger = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            dtReport = dtLedger.Rows.Count switch
            {
                > 0 => GetReports.FilterFor switch
                {
                    "Ledger" => ReturnLedgerSummaryReport(dtLedger),
                    "Ledger/SubLedger" => ReturnLedgerSummaryIncludeSubLedgerReport(dtLedger),
                    "SubLedger/Ledger" => ReturnSubLedgerSummaryIncludeLedgerReport(dtLedger),
                    "SubLedger" => ReturnSubLedgerSummaryIncludeLedgerReport(dtLedger),
                    "Account Group" => ReturnLedgerSummaryAccountGroupOnlyReport(dtLedger),
                    "Account Group/Ledger" => ReturnLedgerSummaryAccountGroupWiseReport(dtLedger),
                    "Account Group/Sub Group" => ReturnLedgerSummaryAccountGroupAccountSubGroupOnlyReport(dtLedger),
                    "Account Group/Sub Group/Ledger" => ReturnLedgerSummaryAccountSubgropWiseReport(dtLedger),
                    _ => dtReport
                },
                _ => dtReport
            };
            return dtReport;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
            return dtReport;
        }
    }

    #endregion **---------- SUMMARY ----------**

    #endregion ---------------  GENERAL LEDGER REPORT ---------------

    // DAY BOOK REPORTS

    #region --------------- DAY BOOK  ---------------

    private DataTable ReturnTFormatDayBookDateWise(DataTable dtDayBook)
    {
        DataRow dataRow;
        string[] moduleStrings = { "OB", "CCB" };
        var dtReport = new DataTable();
        dtReport.Columns.Add("dt_Date", typeof(string));
        dtReport.Columns.Add("dt_Miti", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo1", typeof(string));
        dtReport.Columns.Add("dt_Desc1", typeof(string));
        dtReport.Columns.Add("dt_Debit", typeof(string));
        dtReport.Columns.Add("dt_LocalDebit", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo2", typeof(string));
        dtReport.Columns.Add("dt_Desc2", typeof(string));
        dtReport.Columns.Add("dt_Credit", typeof(string));
        dtReport.Columns.Add("dt_LocalCredit", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo", typeof(string));
        dtReport.Columns.Add("dt_Module", typeof(string));
        dtReport.Columns.Add("dtFilterDate", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        var dtGroupDate = dtDayBook.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<DateTime>("VOUCHERDATE")
        }).Select(g => g.First()).OrderBy(g => g.Field<DateTime>("VOUCHERDATE")).CopyToDataTable();
        foreach (DataRow roType in dtGroupDate.Rows)
        {
            var dtDateDetails = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
            var dtModuleGroup = dtDateDetails.AsEnumerable().GroupBy(r => new
            {
                groupByType = r.Field<DateTime>("VOUCHERDATE")
            }).Select(g => g.First()).CopyToDataTable();
            foreach (DataRow drModule in dtModuleGroup.Rows)
                if (moduleStrings.Contains(drModule["MODULE"].ToString()))
                {
                    var dtLedgerTypeDetails =
                        dtDateDetails.Select($"MODULE = '{drModule["MODULE"]}' ").CopyToDataTable();
                    var debitRows = dtLedgerTypeDetails.Select("DEBIT > 0 ");
                    if (debitRows.Length > 0)
                    {
                        foreach (var drDebit in debitRows)
                        {
                            dataRow = dtReport.NewRow();
                            dataRow["dt_VoucherNo1"] = drDebit["VOUCHERNO"].ToString();
                            dataRow["dt_Desc1"] = drDebit["GlName"].IsValueExits()
                                ? drDebit["GlName"].ToString()
                                : drDebit["LEDGERTYPE"].ToString();
                            dataRow["dt_Debit"] = drDebit["DEBIT"].ToString();
                            dataRow["dt_LocalDebit"] = drDebit["DEBIT"].ToString();
                            dataRow["dt_Module"] = drDebit["MODULE"].ToString();
                            dataRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                        }
                    }

                    var index = -1;
                    var roDebit = dtReport.AsEnumerable().FirstOrDefault(tt =>
                        tt.Field<string>("dt_Module") == drModule["MODULE"].ToString());
                    index = roDebit != null ? dtReport.Rows.IndexOf(roDebit) : index;
                    var rowDebit = debitRows.Length;
                    var result = 0;
                    var rows = dtLedgerTypeDetails.Select("CREDIT > 0 ");
                    if (rows.Length <= 0)
                    {
                        continue;
                    }

                    foreach (var drCredit in rows)
                        if (index != -1 && result < rowDebit)
                        {
                            dtReport.Rows[index].SetField("dt_VoucherNo2", drCredit["VOUCHERNO"].ToString());
                            dtReport.Rows[index].SetField("dt_Desc2", drCredit["GlName"].IsValueExits()
                                ? drCredit["GlName"].ToString()
                                : drCredit["LEDGERTYPE"].ToString());
                            dtReport.Rows[index].SetField("dt_Credit", drCredit["CREDIT"].ToString());
                            dtReport.Rows[index].SetField("dt_LocalCredit", drCredit["CREDIT"].ToString());
                            index++;
                            result++;
                        }
                        else
                        {
                            dataRow = dtReport.NewRow();
                            dataRow["dt_VoucherNo2"] = drCredit["VOUCHERNO"].ToString();
                            dataRow["dt_Desc2"] = drCredit["GlName"].IsValueExits()
                                ? drCredit["GlName"].ToString()
                                : drCredit["LEDGERTYPE"].ToString();
                            dataRow["dt_Credit"] = drCredit["CREDIT"].ToString();
                            dataRow["dt_LocalCredit"] = drCredit["CREDIT"].ToString();
                            dataRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                        }
                }
                else
                {
                    dataRow = dtReport.NewRow();
                    dataRow["dt_Date"] = roType["VOUCHERDATE"].GetDateString();
                    dataRow["dt_Miti"] = roType["VOUCHERMITI"].ToString();
                    dataRow["IsGroup"] = 1;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                    foreach (DataRow drType in dtModuleGroup.Rows)
                    {
                        dataRow = dtReport.NewRow();
                        dataRow["dt_Desc1"] = drType["LEDGERTYPE"].ToString();
                        dataRow["dt_Desc2"] = drType["LEDGERTYPE"].ToString();
                        dataRow["IsGroup"] = 2;
                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                        var dtLedgerTypeDetails =
                            dtDateDetails.Select($"MODULE = '{drType["MODULE"]}' ").CopyToDataTable();
                        var dtGroupByVoucher = dtLedgerTypeDetails.AsEnumerable().GroupBy(r => new
                        {
                            groupByVoucher = r.Field<string>("VOUCHERNO")
                        }).Select(g => g.First()).CopyToDataTable();

                        foreach (DataRow drLedger in dtGroupByVoucher.Rows)
                        {
                            var voucherNo = drLedger["VOUCHERNO"].ToString();
                            var dtDetails = dtLedgerTypeDetails.Select($"VOUCHERNO = '{voucherNo}'").CopyToDataTable();
                            var dtVoucherGroup = dtDetails.AsEnumerable().GroupBy(r => new
                            {
                                LOGVoucher = r.Field<string>("VOUCHERNO")
                            }).Select(g => g.First()).CopyToDataTable();
                            foreach (DataRow drVoucher in dtVoucherGroup.Rows)
                            {
                                var debitRows = dtDetails.Select("DEBIT > 0 ");
                                if (debitRows.Length > 0)
                                {
                                    var rIndex = 0;
                                    foreach (var drDebit in debitRows)
                                    {
                                        dataRow = dtReport.NewRow();
                                        dataRow["dt_VoucherNo1"] =
                                            rIndex is 0 ? drDebit["VOUCHERNO"].ToString() : string.Empty;
                                        dataRow["dt_Desc1"] = drDebit["GlName"].IsValueExits()
                                            ? drDebit["GlName"].ToString()
                                            : drDebit["LEDGERTYPE"].ToString();
                                        dataRow["dt_Debit"] = drDebit["DEBIT"].ToString();
                                        dataRow["dt_LocalDebit"] = drDebit["DEBIT"].ToString();
                                        dataRow["dt_Module"] = drDebit["MODULE"].ToString();
                                        dataRow["dt_VoucherNo"] = drDebit["VOUCHERNO"].ToString();
                                        dataRow["dtFilterDate"] = drDebit["VOUCHERDATE"].GetDateString();
                                        dataRow["IsGroup"] = 0;
                                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                                        rIndex++;
                                    }
                                }

                                var creditRows = dtDetails.Select("CREDIT > 0 ");
                                if (creditRows.Length <= 0)
                                {
                                    continue;
                                }

                                var index = -1;
                                var roDebit = dtReport.AsEnumerable()
                                    .FirstOrDefault(tt => tt.Field<string>("dt_VoucherNo") == voucherNo);
                                index = roDebit != null ? dtReport.Rows.IndexOf(roDebit) : index;
                                var rowDebit = debitRows.Length;
                                var result = 0;
                                //var dtCreditSide = dtDetails.Select($"CREDIT > 0 ").CopyToDataTable();

                                if (creditRows.Length > 0)
                                {
                                    var rIndex = 0;
                                    foreach (var drCredit in creditRows)
                                    {
                                        if (index < dtReport.Rows.Count && result < rowDebit)
                                        {
                                            dtReport.Rows[index].SetField("dt_VoucherNo2",
                                                rIndex is 0 ? drCredit["VOUCHERNO"].ToString() : string.Empty);
                                            dtReport.Rows[index].SetField("dt_Desc2",
                                                drCredit["GlName"].IsValueExits()
                                                    ? drCredit["GlName"].ToString()
                                                    : drCredit["LEDGERTYPE"].ToString());
                                            dtReport.Rows[index].SetField("dt_Credit", drCredit["CREDIT"].ToString());
                                            dtReport.Rows[index].SetField("dt_LocalCredit",
                                                drCredit["CREDIT"].ToString());
                                        }
                                        else
                                        {
                                            dataRow = dtReport.NewRow();
                                            dataRow["dt_VoucherNo2"] = rIndex is 0
                                                ? drCredit["VOUCHERNO"].ToString()
                                                : string.Empty;
                                            dataRow["dt_Desc2"] = drCredit["GlName"].IsValueExits()
                                                ? drCredit["GlName"].ToString()
                                                : drCredit["LEDGERTYPE"].ToString();
                                            dataRow["dt_Credit"] = drCredit["CREDIT"].ToString();
                                            dataRow["dt_LocalCredit"] = drCredit["CREDIT"].ToString();
                                            dataRow["dt_VoucherNo"] = drCredit["VOUCHERNO"].ToString();
                                            dataRow["dt_Module"] = drCredit["MODULE"].ToString();
                                            dataRow["dtFilterDate"] = drCredit["VOUCHERDATE"].GetDateString();
                                            dataRow["IsGroup"] = 0;
                                            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                                        }

                                        index++;
                                        result++;
                                        rIndex++;
                                    }
                                }

                                dataRow = dtReport.NewRow();
                                dataRow["dt_Desc1"] = $"[{voucherNo}] VOUCHER TOTAL >> ";
                                dataRow["dt_Desc2"] = $"[{voucherNo}] VOUCHER TOTAL : ";
                                var debit = dtDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                                var credit = dtDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
                                dataRow["dt_Debit"] = debit;
                                dataRow["dt_LocalDebit"] = debit;
                                dataRow["dt_Credit"] = credit;
                                dataRow["dt_LocalCredit"] = credit;
                                dataRow["IsGroup"] = 10;
                                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                            }
                        }
                    }
                }

            if (!moduleStrings.Contains(roType["MODULE"].ToString()))
            {
                var dtTotalDate = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
                dataRow = dtReport.NewRow();
                dataRow["dt_Desc1"] = "DAY TOTAL : ";
                dataRow["dt_Desc2"] = "DAY TOTAL : ";
                var debit = dtTotalDate.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                var credit = dtTotalDate.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
                dataRow["dt_Debit"] = debit;
                dataRow["dt_LocalDebit"] = debit;
                dataRow["dt_Credit"] = credit;
                dataRow["dt_LocalCredit"] = credit;
                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    private DataTable ReturnNormalDayBook(DataTable dtDayBook)
    {
        var dtReport = new DataTable();
        dtReport.Columns.Add("dt_Date", typeof(string));
        dtReport.Columns.Add("dt_Miti", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo", typeof(string));
        dtReport.Columns.Add("dt_Desc", typeof(string));
        dtReport.Columns.Add("dt_Currency", typeof(string));
        dtReport.Columns.Add("dt_CurrencyRate", typeof(string));
        dtReport.Columns.Add("dt_Debit", typeof(string));
        dtReport.Columns.Add("dt_LocalDebit", typeof(string));
        dtReport.Columns.Add("dt_Credit", typeof(string));
        dtReport.Columns.Add("dt_LocalCredit", typeof(string));
        dtReport.Columns.Add("dt_Balance", typeof(string));
        dtReport.Columns.Add("dt_Module", typeof(string));
        dtReport.Columns.Add("dtFilterDate", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        DataRow newRow;
        var dtGroupDate = dtDayBook.AsEnumerable().GroupBy(r => new
        {
            voucherDate = r.Field<DateTime>("VOUCHERDATE")
        }).Select(g => g.First()).CopyToDataTable();
        decimal balanceDecimal = 0;
        foreach (DataRow roType in dtGroupDate.Rows)
        {
            string[] moduleStrings = { "OB", "CCB" };
            var dtDate = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
            dtDate = dtDate.AsEnumerable().GroupBy(r => new
            {
                voucherDate = r.Field<DateTime>("VOUCHERDATE")
            }).Select(g => g.First()).CopyToDataTable();
            foreach (DataRow drType in dtDate.Rows)
            {
                decimal openingDebit = 0;
                decimal openingCredit = 0;
                var details = dtDayBook.Select($"VOUCHERDATE = '{drType["VOUCHERDATE"]}' ").CopyToDataTable();
                if (moduleStrings.Contains(drType["MODULE"].ToString()))
                {
                    foreach (DataRow drDetailsRow in details.Rows)
                        if (moduleStrings.Contains(drType["MODULE"].ToString()))
                        {
                            newRow = dtReport.NewRow();
                            newRow["dt_VoucherNo"] = drDetailsRow["MODULE"].ToString() is "OB"
                                ? "OP_BALANCE"
                                : "CB_BALANCE";
                            newRow["dt_Desc"] = !string.IsNullOrEmpty(drDetailsRow["GlName"].ToString())
                                ? drDetailsRow["GlName"].ToString()
                                : drDetailsRow["LEDGERTYPE"].ToString();

                            openingDebit += drDetailsRow["DEBIT"].GetDecimal();
                            openingCredit += drDetailsRow["CREDIT"].GetDecimal();

                            newRow["dt_Debit"] = drDetailsRow["DEBIT"].GetDecimalComma();
                            newRow["dt_LocalDebit"] = drDetailsRow["DEBIT"].GetDecimalComma();
                            newRow["dt_Credit"] = drDetailsRow["CREDIT"].GetDecimalComma();
                            newRow["dt_LocalCredit"] = drDetailsRow["CREDIT"].GetDecimalComma();

                            balanceDecimal += openingDebit - openingCredit;

                            newRow["dt_Balance"] = newRow["dt_Module"] = drDetailsRow["MODULE"].ToString();
                            newRow["dtFilterDate"] = drDetailsRow["VOUCHERDATE"].ToString();
                            newRow["IsGroup"] = 1;
                            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                        }
                }
                else
                {
                    var dtLedgerType = details.AsEnumerable().GroupBy(r => new
                    {
                        ledgerType = r.Field<string>("LEDGERTYPE"),
                        LOGVoucher = r.Field<string>("VOUCHERNO")
                    }).Select(g => g.First()).CopyToDataTable();

                    foreach (DataRow drLedger in dtLedgerType.Rows)
                    {
                        newRow = dtReport.NewRow();
                        newRow["dt_Date"] = drLedger["VOUCHERDATE"].GetDateString();
                        newRow["dt_Miti"] = drLedger["VOUCHERMITI"].ToString();
                        newRow["dt_VoucherNo"] = drLedger["VOUCHERNO"].ToString();
                        newRow["dt_Desc"] = drLedger["LEDGERTYPE"].ToString();
                        newRow["IsGroup"] = 1;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                        var voucherNo = drLedger["VOUCHERNO"].ToString();
                        var voucherType = drLedger["LEDGERTYPE"].ToString();

                        var dtDetails = dtDayBook.Select($"LEDGERTYPE = '{voucherType}' AND VOUCHERNO = '{voucherNo}' AND VOUCHERDATE = '{drLedger["VOUCHERDATE"]}' ").CopyToDataTable();
                        foreach (DataRow drVoucher in dtDetails.Rows)
                        {
                            newRow = dtReport.NewRow();
                            newRow["dt_Desc"] = !string.IsNullOrEmpty(drVoucher["GlName"].ToString())
                                ? drVoucher["GlName"].ToString()
                                : drVoucher["LEDGERTYPE"].ToString();

                            var debitDetail = drVoucher["DEBIT"].GetDecimal();
                            var creditDetails = drVoucher["CREDIT"].GetDecimal();

                            newRow["dt_Debit"] = debitDetail.GetDecimalComma();
                            newRow["dt_LocalDebit"] = debitDetail.GetDecimalComma();
                            newRow["dt_Credit"] = creditDetails.GetDecimalComma();
                            newRow["dt_LocalCredit"] = creditDetails.GetDecimalComma();

                            balanceDecimal += drVoucher["DEBIT"].GetDecimal() + drVoucher["CREDIT"].GetDecimal();

                            newRow["dt_Module"] = drVoucher["MODULE"].ToString();
                            newRow["dtFilterDate"] = drVoucher["VOUCHERDATE"].ToString();
                            newRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                            var chequeNo = string.Empty;
                            if (!chequeNo.IsBlankOrEmpty())
                            {
                                newRow = dtReport.NewRow();
                                newRow["dt_Desc"] = " ";
                                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                            }
                        }

                        newRow = dtReport.NewRow();
                        newRow["dt_Desc"] = "VOUCHER TOTAL : ";
                        var debit = dtDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                        var credit = dtDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());

                        newRow["dt_Debit"] = debit.GetDecimalComma();
                        newRow["dt_LocalDebit"] = debit.GetDecimalComma();
                        newRow["dt_Credit"] = credit.GetDecimalComma();
                        newRow["dt_LocalCredit"] = credit.GetDecimalComma();
                        newRow["IsGroup"] = 11;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                    }
                }
            }

            if (!moduleStrings.Contains(roType["MODULE"].ToString()))
            {
                var dtTotalDate = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = "DAY TOTAL : ";

                var debit = dtTotalDate.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                var credit = dtTotalDate.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());

                newRow["dt_Debit"] = debit.GetDecimalComma();
                newRow["dt_LocalDebit"] = debit.GetDecimalComma();
                newRow["dt_Credit"] = credit.GetDecimalComma();
                newRow["dt_LocalCredit"] = credit.GetDecimalComma();
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    public DataTable GetDayBookReport()
    {
        var dtReport = new DataTable();
        var cmdBuilder = new StringBuilder();
        cmdBuilder.Append($@"
			WITH DAYBOOK AS
			(
				SELECT OPENING.MODULE, OPENING.VOUCHERNO, OPENING.VOUCHERDATE, OPENING.VOUCHERMITI, OPENING.LEDGERTYPE, OPENING.LEDGER_ID, OPENING.PARTYNAME,'' Cheque_No, '' Cheque_Date, '' Cheque_Miti,
				CASE WHEN SUM(OPENING.DEBIT - OPENING.CREDIT) > 0 THEN SUM(OPENING.DEBIT - OPENING.CREDIT) ELSE 0 END DEBIT,CASE WHEN SUM( OPENING.CREDIT - OPENING.DEBIT) > 0 THEN  SUM( OPENING.CREDIT - OPENING.DEBIT) ELSE 0 END CREDIT
				FROM
				(
					SELECT 'OB' MODULE,'OPENING BALANCE' VOUCHERNO,'' VOUCHERDATE,'' VOUCHERMITI,CASE WHEN gl.GLType='Cash' THEN 'CASH OPENING' WHEN gl.GLType= 'Bank' THEN 'BANK OPENING' ELSE '' END LEDGERTYPE ,CASE WHEN gl.GLType='Cash' THEN  0 ELSE gl.GLID END LEDGER_ID,'' PARTYNAME,	CASE WHEN SUM(LocalDebit_Amt - LocalCredit_Amt)> 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE  0  END DEBIT,
					CASE WHEN SUM(LocalDebit_Amt - LocalCredit_Amt)< 0 THEN SUM( ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE  0  END CREDIT
					FROM AMS.AccountDetails ad LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
					WHERE gl.GLType IN ('Cash', 'Bank') AND ad.FiscalYearId <  {ObjGlobal.SysFiscalYearId}
					GROUP BY GLType,gl.GLID
					UNION ALL
					SELECT 'OB' MODULE,'OPENING BALANCE' VOUCHERNO,'' VOUCHERDATE,'' VOUCHERMITI,
					CASE WHEN gl.GLType='Cash' THEN 'CASH OPENING' WHEN gl.GLType= 'Bank' THEN 'BANK OPENING' ELSE '' END LEDGERTYPE ,
					CASE WHEN gl.GLType='Cash' THEN  0 ELSE gl.GLID END LEDGER_ID,'' PartyName,CASE WHEN SUM(LocalDebit_Amt - LocalCredit_Amt)> 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE  0  END DEBIT,	CASE WHEN SUM(LocalDebit_Amt - LocalCredit_Amt) < 0 THEN SUM( ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE  0  END CREDIT
					FROM AMS.AccountDetails ad	LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
					WHERE gl.GLType IN ('Cash', 'Bank') AND ad.FiscalYearId =  {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date < '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}'
					GROUP BY GLType,gl.GLID
				) AS OPENING
				GROUP BY OPENING.MODULE, OPENING.VOUCHERNO, OPENING.VOUCHERDATE, OPENING.VOUCHERMITI, OPENING.LEDGERTYPE, OPENING.LEDGER_ID, OPENING.PARTYNAME
				UNION ALL
				SELECT ad.Module, ad.Voucher_No,AD.Voucher_Date, AD.Voucher_Miti,CASE WHEN AD.Module ='SB' THEN 'SALES INVOICE BILLING' WHEN AD.Module ='SR' THEN 'SALES RETURN BILLING'WHEN AD.Module ='PB' THEN 'PURCHASE INVOICE BILLING' WHEN AD.Module ='PR' THEN 'PURCHASE RETURN BILLING' WHEN AD.Module ='JV' THEN 'JOURNAL VOUCHER'	WHEN AD.Module ='CB' THEN 'CASH/BANK VOUCHER' WHEN AD.Module ='PDC' THEN 'POST DATED CHEQUE'	WHEN AD.Module ='DN' THEN 'DEBIT NOTES'	WHEN AD.Module ='CN' THEN 'CREDIT NOTES'ELSE '' END AS LEDGERTYPE,ad.Ledger_ID,ad.PartyName,ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti, SUM(ad.LocalDebit_Amt) LocalDebit_Amt, SUM(ad.LocalCredit_Amt) LocalCredit_Amt
				FROM AMS.AccountDetails ad WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date between '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' and '{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}'
				AND Module IN (SELECT Value FROM AMS.fn_Split('{GetReports.AccountType}', ',')) AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (ad.CmpUnit_ID ='{ObjGlobal.SysCompanyUnitId} ' OR ad.CmpUnit_ID IS NULL)
				GROUP BY ad.Module,ad.Voucher_No,AD.Voucher_Date, AD.Voucher_Miti,ad.Ledger_ID,ad.PartyName,ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti  ");
        if (GetReports.IsCombineSales)
        {
            cmdBuilder.AppendLine($@"
				UNION ALL
				SELECT 'SB' Module,'DAYS SALES' Voucher_No,AD.Voucher_Date, AD.Voucher_Miti,'SALES INVOICE BILLING' LEDGERTYPE,ad.Ledger_ID,'' PartyName,'' Cheque_No, '' Cheque_Date, '' Cheque_Miti,
				CASE WHEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) > 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END LocalDebit_Amt, CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0 THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt ) ELSE 0 END LocalCredit_Amt
				FROM AMS.AccountDetails ad WHERE ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date between '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' and '{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}'
				AND Module IN ('SB')  AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (ad.CmpUnit_ID ='{ObjGlobal.SysCompanyUnitId} ' OR ad.CmpUnit_ID IS NULL)
				GROUP BY AD.Voucher_Date, AD.Voucher_Miti,ad.Ledger_ID");
        }

        cmdBuilder.AppendLine(@$"
				UNION ALL
				SELECT CB.MODULE, CB.VOUCHERNO, CB.VOUCHERDATE, CB.VOUCHERMITI, CB.LEDGERTYPE, CB.LEDGER_ID, CB.PARTYNAME, CB.Cheque_No, CB.Cheque_Date, CB.Cheque_Miti,
				CASE WHEN SUM( CB.DEBIT -CB.CREDIT) > 0 THEN SUM( CB.DEBIT - CB.CREDIT) ELSE 0 END DEBIT,
				CASE WHEN SUM( CB.CREDIT -CB.DEBIT) > 0 THEN SUM( CB.CREDIT - CB.DEBIT) ELSE 0 END CREDIT  FROM
				(
					SELECT 'CCB' MODULE,'CLOSING BALANCE' VOUCHERNO,'{Convert.ToDateTime(GetReports.ToDate).AddDays(1):yyyy-MM-dd}' VOUCHERDATE,'' VOUCHERMITI,
					CASE WHEN gl.GLType='Cash' THEN 'CASH CLOSING' WHEN gl.GLType= 'Bank' THEN 'BANK CLOSING' ELSE '' END LEDGERTYPE ,
					CASE WHEN gl.GLType='Cash' THEN  0 ELSE gl.GLID END LEDGER_ID,'' PARTYNAME,'' Cheque_No, '' Cheque_Date, '' Cheque_Miti,
					CASE WHEN SUM(LocalDebit_Amt - LocalCredit_Amt)> 0 THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE  0  END DEBIT,
					CASE WHEN SUM(LocalDebit_Amt - LocalCredit_Amt)< 0 THEN SUM( ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE  0  END CREDIT
					FROM AMS.AccountDetails ad
					LEFT OUTER JOIN AMS.GeneralLedger gl ON ad.Ledger_ID = gl.GLID
					WHERE gl.GLType IN ('Cash', 'Bank') AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId} AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (ad.CmpUnit_ID ='{ObjGlobal.SysCompanyUnitId} ' OR ad.CmpUnit_ID IS NULL)
					GROUP BY GLType,GLID
				) AS CB
				GROUP BY CB.MODULE, CB.VOUCHERNO, CB.VOUCHERDATE, CB.VOUCHERMITI, CB.LEDGERTYPE, CB.LEDGER_ID, CB.PARTYNAME, CB.Cheque_No, CB.Cheque_Date, CB.Cheque_Miti
			) SELECT db.MODULE, db.VOUCHERNO, db.VOUCHERDATE, db.VOUCHERMITI, db.LEDGERTYPE, db.LEDGER_ID,
			CASE WHEN gl.GLType IN ('Cash','Bank') AND ISNULL(db.PartyName,'') <> ''  then gl.GLName + ' ( ' + db.PartyName + ' )' ELSE gl.GLName END GlName,db.Cheque_No, db.Cheque_Date, db.Cheque_Miti, db.DEBIT, db.CREDIT FROM DAYBOOK db
			LEFT OUTER JOIN AMS.GeneralLedger gl ON db.Ledger_ID = gl.GLID  WHERE 1=1");
        if (!string.IsNullOrEmpty(GetReports.FilterFor))
        {
            cmdBuilder.AppendLine(
                $@" AND VOUCHERNO IN (SELECT Value FROM AMS.fn_Split('{GetReports.FilterFor}', ',')) ");
        }

        cmdBuilder.AppendLine(
            "  ORDER BY db.VOUCHERDATE,db.VOUCHERNO, db.Module,db.DEBIT DESC,db.CREDIT ASC,GlName ");
        var dtDayBook = SqlExtensions.ExecuteDataSet(cmdBuilder.ToString()).Tables[0];

        dtReport = GetReports.IsTFormat switch
        {
            true => ReturnTFormatDayBookDateWise(dtDayBook),
            _ => ReturnNormalDayBook(dtDayBook)
        };
        return dtReport;
    }

    #endregion --------------- DAY BOOK  ---------------

    // DAY BOOK REPORTS

    #region --------------- DAY BOOK  ---------------

    private DataTable ReturnTFormatJournalVoucherDateWise(DataTable dtDayBook)
    {
        DataRow dataRow;
        string[] moduleStrings = { "OB", "CCB" };
        var dtReport = new DataTable();
        dtReport.Columns.Add("dt_Date", typeof(string));
        dtReport.Columns.Add("dt_Miti", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo1", typeof(string));
        dtReport.Columns.Add("dt_Desc1", typeof(string));
        dtReport.Columns.Add("dt_Debit", typeof(string));
        dtReport.Columns.Add("dt_LocalDebit", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo2", typeof(string));
        dtReport.Columns.Add("dt_Desc2", typeof(string));
        dtReport.Columns.Add("dt_Credit", typeof(string));
        dtReport.Columns.Add("dt_LocalCredit", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo", typeof(string));
        dtReport.Columns.Add("dt_Module", typeof(string));
        dtReport.Columns.Add("dtFilterDate", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        var dtGroupDate = dtDayBook.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<DateTime>("VOUCHERDATE")
        }).Select(g => g.First()).OrderBy(g => g.Field<DateTime>("VOUCHERDATE")).CopyToDataTable();
        foreach (DataRow roType in dtGroupDate.Rows)
        {
            var dtDateDetails = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
            var dtModuleGroup = dtDateDetails.AsEnumerable().GroupBy(r => new
            {
                groupByType = r.Field<DateTime>("VOUCHERDATE")
            }).Select(g => g.First()).CopyToDataTable();
            foreach (DataRow drModule in dtModuleGroup.Rows)
                if (moduleStrings.Contains(drModule["MODULE"].ToString()))
                {
                    var dtLedgerTypeDetails =
                        dtDateDetails.Select($"MODULE = '{drModule["MODULE"]}' ").CopyToDataTable();
                    var debitRows = dtLedgerTypeDetails.Select("DEBIT > 0 ");
                    if (debitRows.Length > 0)
                    {
                        foreach (var drDebit in debitRows)
                        {
                            dataRow = dtReport.NewRow();
                            dataRow["dt_VoucherNo1"] = drDebit["VOUCHERNO"].ToString();
                            dataRow["dt_Desc1"] = drDebit["GlName"].IsValueExits()
                                ? drDebit["GlName"].ToString()
                                : drDebit["LEDGERTYPE"].ToString();
                            dataRow["dt_Debit"] = drDebit["DEBIT"].ToString();
                            dataRow["dt_LocalDebit"] = drDebit["DEBIT"].ToString();
                            dataRow["dt_Module"] = drDebit["MODULE"].ToString();
                            dataRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                        }
                    }

                    var index = -1;
                    var roDebit = dtReport.AsEnumerable().FirstOrDefault(tt =>
                        tt.Field<string>("dt_Module") == drModule["MODULE"].ToString());
                    index = roDebit != null ? dtReport.Rows.IndexOf(roDebit) : index;
                    var rowDebit = debitRows.Length;
                    var result = 0;
                    var rows = dtLedgerTypeDetails.Select("CREDIT > 0 ");
                    if (rows.Length <= 0)
                    {
                        continue;
                    }

                    foreach (var drCredit in rows)
                        if (index != -1 && result < rowDebit)
                        {
                            dtReport.Rows[index].SetField("dt_VoucherNo2", drCredit["VOUCHERNO"].ToString());
                            dtReport.Rows[index].SetField("dt_Desc2", drCredit["GlName"].IsValueExits()
                                ? drCredit["GlName"].ToString()
                                : drCredit["LEDGERTYPE"].ToString());
                            dtReport.Rows[index].SetField("dt_Credit", drCredit["CREDIT"].ToString());
                            dtReport.Rows[index].SetField("dt_LocalCredit", drCredit["CREDIT"].ToString());
                            index++;
                            result++;
                        }
                        else
                        {
                            dataRow = dtReport.NewRow();
                            dataRow["dt_VoucherNo2"] = drCredit["VOUCHERNO"].ToString();
                            dataRow["dt_Desc2"] = drCredit["GlName"].IsValueExits()
                                ? drCredit["GlName"].ToString()
                                : drCredit["LEDGERTYPE"].ToString();
                            dataRow["dt_Credit"] = drCredit["CREDIT"].ToString();
                            dataRow["dt_LocalCredit"] = drCredit["CREDIT"].ToString();
                            dataRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                        }
                }
                else
                {
                    dataRow = dtReport.NewRow();
                    dataRow["dt_Date"] = roType["VOUCHERDATE"].GetDateString();
                    dataRow["dt_Miti"] = roType["VOUCHERMITI"].ToString();
                    dataRow["IsGroup"] = 1;
                    dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                    foreach (DataRow drType in dtModuleGroup.Rows)
                    {
                        dataRow = dtReport.NewRow();
                        dataRow["dt_Desc1"] = drType["LEDGERTYPE"].ToString();
                        dataRow["dt_Desc2"] = drType["LEDGERTYPE"].ToString();
                        dataRow["IsGroup"] = 2;
                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

                        var dtLedgerTypeDetails =
                            dtDateDetails.Select($"MODULE = '{drType["MODULE"]}' ").CopyToDataTable();
                        var dtGroupByVoucher = dtLedgerTypeDetails.AsEnumerable().GroupBy(r => new
                        {
                            groupByVoucher = r.Field<string>("VOUCHERNO")
                        }).Select(g => g.First()).CopyToDataTable();

                        foreach (DataRow drLedger in dtGroupByVoucher.Rows)
                        {
                            var voucherNo = drLedger["VOUCHERNO"].ToString();
                            var dtDetails = dtLedgerTypeDetails.Select($"VOUCHERNO = '{voucherNo}'").CopyToDataTable();
                            var dtVoucherGroup = dtDetails.AsEnumerable().GroupBy(r => new
                            {
                                LOGVoucher = r.Field<string>("VOUCHERNO")
                            }).Select(g => g.First()).CopyToDataTable();
                            foreach (DataRow drVoucher in dtVoucherGroup.Rows)
                            {
                                var debitRows = dtDetails.Select("DEBIT > 0 ");
                                if (debitRows.Length > 0)
                                {
                                    var rIndex = 0;
                                    foreach (var drDebit in debitRows)
                                    {
                                        dataRow = dtReport.NewRow();
                                        dataRow["dt_VoucherNo1"] =
                                            rIndex is 0 ? drDebit["VOUCHERNO"].ToString() : string.Empty;
                                        dataRow["dt_Desc1"] = drDebit["GlName"].IsValueExits()
                                            ? drDebit["GlName"].ToString()
                                            : drDebit["LEDGERTYPE"].ToString();
                                        dataRow["dt_Debit"] = drDebit["DEBIT"].ToString();
                                        dataRow["dt_LocalDebit"] = drDebit["DEBIT"].ToString();
                                        dataRow["dt_Module"] = drDebit["MODULE"].ToString();
                                        dataRow["dt_VoucherNo"] = drDebit["VOUCHERNO"].ToString();
                                        dataRow["dtFilterDate"] = drDebit["VOUCHERDATE"].GetDateString();
                                        dataRow["IsGroup"] = 0;
                                        dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                                        rIndex++;
                                    }
                                }

                                var creditRows = dtDetails.Select("CREDIT > 0 ");
                                if (creditRows.Length <= 0)
                                {
                                    continue;
                                }

                                var index = -1;
                                var roDebit = dtReport.AsEnumerable()
                                    .FirstOrDefault(tt => tt.Field<string>("dt_VoucherNo") == voucherNo);
                                index = roDebit != null ? dtReport.Rows.IndexOf(roDebit) : index;
                                var rowDebit = debitRows.Length;
                                var result = 0;
                                //var dtCreditSide = dtDetails.Select($"CREDIT > 0 ").CopyToDataTable();

                                if (creditRows.Length > 0)
                                {
                                    var rIndex = 0;
                                    foreach (var drCredit in creditRows)
                                    {
                                        if (index < dtReport.Rows.Count && result < rowDebit)
                                        {
                                            dtReport.Rows[index].SetField("dt_VoucherNo2",
                                                rIndex is 0 ? drCredit["VOUCHERNO"].ToString() : string.Empty);
                                            dtReport.Rows[index].SetField("dt_Desc2",
                                                drCredit["GlName"].IsValueExits()
                                                    ? drCredit["GlName"].ToString()
                                                    : drCredit["LEDGERTYPE"].ToString());
                                            dtReport.Rows[index].SetField("dt_Credit", drCredit["CREDIT"].ToString());
                                            dtReport.Rows[index].SetField("dt_LocalCredit",
                                                drCredit["CREDIT"].ToString());
                                        }
                                        else
                                        {
                                            dataRow = dtReport.NewRow();
                                            dataRow["dt_VoucherNo2"] = rIndex is 0
                                                ? drCredit["VOUCHERNO"].ToString()
                                                : string.Empty;
                                            dataRow["dt_Desc2"] = drCredit["GlName"].IsValueExits()
                                                ? drCredit["GlName"].ToString()
                                                : drCredit["LEDGERTYPE"].ToString();
                                            dataRow["dt_Credit"] = drCredit["CREDIT"].ToString();
                                            dataRow["dt_LocalCredit"] = drCredit["CREDIT"].ToString();
                                            dataRow["dt_VoucherNo"] = drCredit["VOUCHERNO"].ToString();
                                            dataRow["dt_Module"] = drCredit["MODULE"].ToString();
                                            dataRow["dtFilterDate"] = drCredit["VOUCHERDATE"].GetDateString();
                                            dataRow["IsGroup"] = 0;
                                            dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                                        }

                                        index++;
                                        result++;
                                        rIndex++;
                                    }
                                }

                                dataRow = dtReport.NewRow();
                                dataRow["dt_Desc1"] = $"[{voucherNo}] VOUCHER TOTAL >> ";
                                dataRow["dt_Desc2"] = $"[{voucherNo}] VOUCHER TOTAL : ";
                                var debit = dtDetails.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                                var credit = dtDetails.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
                                dataRow["dt_Debit"] = debit;
                                dataRow["dt_LocalDebit"] = debit;
                                dataRow["dt_Credit"] = credit;
                                dataRow["dt_LocalCredit"] = credit;
                                dataRow["IsGroup"] = 10;
                                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
                            }
                        }
                    }
                }

            if (!moduleStrings.Contains(roType["MODULE"].ToString()))
            {
                var dtTotalDate = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
                dataRow = dtReport.NewRow();
                dataRow["dt_Desc1"] = "DAY TOTAL : ";
                dataRow["dt_Desc2"] = "DAY TOTAL : ";
                var debit = dtTotalDate.AsEnumerable().Sum(x => x["DEBIT"].GetDecimal());
                var credit = dtTotalDate.AsEnumerable().Sum(x => x["CREDIT"].GetDecimal());
                dataRow["dt_Debit"] = debit;
                dataRow["dt_LocalDebit"] = debit;
                dataRow["dt_Credit"] = credit;
                dataRow["dt_LocalCredit"] = credit;
                dataRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    private DataTable ReturnNormalJournalVoucher(DataTable dtDayBook)
    {
        var dtReport = new DataTable();
        dtReport.Columns.Add("dt_Date", typeof(string));
        dtReport.Columns.Add("dt_Miti", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo", typeof(string));
        dtReport.Columns.Add("dt_Desc", typeof(string));
        dtReport.Columns.Add("dt_Currency", typeof(string));
        dtReport.Columns.Add("dt_CurrencyRate", typeof(string));
        dtReport.Columns.Add("dt_Debit", typeof(string));
        dtReport.Columns.Add("dt_LocalDebit", typeof(string));
        dtReport.Columns.Add("dt_Credit", typeof(string));
        dtReport.Columns.Add("dt_LocalCredit", typeof(string));
        dtReport.Columns.Add("dt_Balance", typeof(string));
        dtReport.Columns.Add("dt_Module", typeof(string));
        dtReport.Columns.Add("dtFilterDate", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        DataRow newRow;
        var dtGroupDate = dtDayBook.AsEnumerable().GroupBy(r => new
        {
            voucherDate = r.Field<DateTime>("VOUCHERDATE")
        }).Select(g => g.First()).CopyToDataTable();
        decimal balanceDecimal = 0;
        foreach (DataRow roType in dtGroupDate.Rows)
        {
            string[] moduleStrings = { "OB", "CCB" };
            var dtDate = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
            dtDate = dtDate.AsEnumerable().GroupBy(r => new
            {
                voucherDate = r.Field<DateTime>("VOUCHERDATE")
            }).Select(g => g.First()).CopyToDataTable();
            foreach (DataRow drType in dtDate.Rows)
            {
                decimal openingDebit = 0;
                decimal openingCredit = 0;
                var details = dtDayBook.Select($"VOUCHERDATE = '{drType["VOUCHERDATE"]}' ").CopyToDataTable();
                if (moduleStrings.Contains(drType["MODULE"].ToString()))
                {
                    foreach (DataRow drDetailsRow in details.Rows)
                        if (moduleStrings.Contains(drType["MODULE"].ToString()))
                        {
                            newRow = dtReport.NewRow();
                            newRow["dt_VoucherNo"] =
                                drDetailsRow["MODULE"].ToString() is "OB" ? "OP_BALANCE" : "CB_BALANCE";
                            newRow["dt_Desc"] = !string.IsNullOrEmpty(drDetailsRow["GlName"].ToString())
                                ? drDetailsRow["GlName"].ToString()
                                : drDetailsRow["LEDGERTYPE"].ToString();

                            openingDebit += drDetailsRow["DEBIT"].GetDecimal();
                            openingCredit += drDetailsRow["CREDIT"].GetDecimal();

                            newRow["dt_Debit"] = drDetailsRow["DEBIT"].ToString();
                            newRow["dt_LocalDebit"] = drDetailsRow["DEBIT"].ToString();
                            newRow["dt_Credit"] = drDetailsRow["CREDIT"].ToString();
                            newRow["dt_LocalCredit"] = drDetailsRow["CREDIT"].ToString();
                            balanceDecimal += openingDebit - openingCredit;
                            newRow["dt_Balance"] = newRow["dt_Module"] = drDetailsRow["MODULE"].ToString();
                            newRow["dtFilterDate"] = drDetailsRow["VOUCHERDATE"].ToString();
                            newRow["IsGroup"] = 1;
                            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                        }
                }
                else
                {
                    var dtLedgerType = details.AsEnumerable().GroupBy(r => new
                    {
                        ledgerType = r.Field<string>("LEDGERTYPE"),
                        LOGVoucher = r.Field<string>("VOUCHERNO")
                    }).Select(g => g.First()).CopyToDataTable();
                    foreach (DataRow drLedger in dtLedgerType.Rows)
                    {
                        newRow = dtReport.NewRow();
                        newRow["dt_Date"] = drLedger["VOUCHERDATE"].ToString();
                        newRow["dt_Miti"] = drLedger["VOUCHERMITI"].ToString();
                        newRow["dt_VoucherNo"] = drLedger["VOUCHERNO"].ToString();
                        newRow["dt_Desc"] = drLedger["LEDGERTYPE"].ToString();
                        newRow["IsGroup"] = 1;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                        var voucherNo = drLedger["VOUCHERNO"].ToString();
                        var voucherType = drLedger["LEDGERTYPE"].ToString();

                        var dtDetails = dtDayBook
                            .Select(
                                $"LEDGERTYPE = '{voucherType}' AND VOUCHERNO = '{voucherNo}' AND VOUCHERDATE = '{drLedger["VOUCHERDATE"]}' ")
                            .CopyToDataTable();
                        foreach (DataRow drVoucher in dtDetails.Rows)
                        {
                            newRow = dtReport.NewRow();
                            newRow["dt_Desc"] = !string.IsNullOrEmpty(drVoucher["GlName"].ToString())
                                ? drVoucher["GlName"].ToString()
                                : drVoucher["LEDGERTYPE"].ToString();
                            newRow["dt_Debit"] = drVoucher["DEBIT"].ToString();
                            newRow["dt_LocalDebit"] = drVoucher["DEBIT"].ToString();
                            newRow["dt_Credit"] = drVoucher["CREDIT"].ToString();
                            newRow["dt_LocalCredit"] = drVoucher["CREDIT"].ToString();
                            balanceDecimal += drVoucher["DEBIT"].GetDecimal() + drVoucher["CREDIT"].GetDecimal();
                            newRow["dt_Module"] = drVoucher["MODULE"].ToString();
                            newRow["dtFilterDate"] = drVoucher["VOUCHERDATE"].ToString();
                            newRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                            var chequeNo = string.Empty;
                            if (!chequeNo.IsBlankOrEmpty())
                            {
                                newRow = dtReport.NewRow();
                                newRow["dt_Desc"] = " ";
                                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                            }
                        }

                        newRow = dtReport.NewRow();
                        newRow["dt_Desc"] = "VOUCHER TOTAL : ";
                        var debit = dtDetails.AsEnumerable().Sum(x =>
                            Convert.ToDecimal(x["DEBIT"] == DBNull.Value ? 0 : x["DEBIT"]));
                        var credit = dtDetails.AsEnumerable().Sum(x =>
                            Convert.ToDecimal(x["CREDIT"] == DBNull.Value ? 0 : x["CREDIT"]));
                        newRow["dt_Debit"] = debit;
                        newRow["dt_LocalDebit"] = debit;
                        newRow["dt_Credit"] = credit;
                        newRow["dt_LocalCredit"] = credit;
                        newRow["IsGroup"] = 10;
                        dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                    }
                }
            }

            if (!moduleStrings.Contains(roType["MODULE"].ToString()))
            {
                var dtTotalDate = dtDayBook.Select($"VOUCHERDATE = '{roType["VOUCHERDATE"]}' ").CopyToDataTable();
                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = "DAY TOTAL : ";
                var debit = dtTotalDate.AsEnumerable()
                    .Sum(x => Convert.ToDecimal(x["DEBIT"] == DBNull.Value ? 0 : x["DEBIT"]));
                var credit = dtTotalDate.AsEnumerable()
                    .Sum(x => Convert.ToDecimal(x["CREDIT"] == DBNull.Value ? 0 : x["CREDIT"]));
                newRow["dt_Debit"] = debit;
                newRow["dt_LocalDebit"] = debit;
                newRow["dt_Credit"] = credit;
                newRow["dt_LocalCredit"] = credit;
                newRow["IsGroup"] = 11;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
        }

        return dtReport;
    }

    public DataTable GetJournalVoucherReport()
    {
        var dtReport = new DataTable();
        var cmdBuilder = new StringBuilder();
        cmdBuilder.Append($@"
			WITH DAYBOOK AS
			   (SELECT ad.Module, ad.Voucher_No VOUCHERNO, AD.Voucher_Date VOUCHERDATE, AD.Voucher_Miti VOUCHERMITI, CASE WHEN AD.Module='SB' THEN 'SALES INVOICE BILLING'
																						WHEN AD.Module='SR' THEN 'SALES RETURN BILLING'
																						WHEN AD.Module='PB' THEN 'PURCHASE INVOICE BILLING'
																						WHEN AD.Module='PR' THEN 'PURCHASE RETURN BILLING'
																						WHEN AD.Module='JV' THEN 'JOURNAL VOUCHER'
																						WHEN AD.Module='CB' THEN 'CASH/BANK VOUCHER'
																						WHEN AD.Module='PDC' THEN 'POST DATED CHEQUE'
																						WHEN AD.Module='DN' THEN 'DEBIT NOTES'
																						WHEN AD.Module='CN' THEN 'CREDIT NOTES' ELSE '' END AS LEDGERTYPE, ad.Ledger_ID, ad.PartyName, ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti, SUM(ad.LocalDebit_Amt) DEBIT, SUM(ad.LocalCredit_Amt) CREDIT
				  FROM AMS.AccountDetails ad
				 WHERE ad.FiscalYearId={ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND Module IN ('JV') AND ad.Branch_ID IN ({ObjGlobal.SysBranchId}) AND (ad.CmpUnit_ID='{ObjGlobal.SysCompanyUnitId}' OR ad.CmpUnit_ID IS NULL)
				 GROUP BY ad.Module, ad.Voucher_No, AD.Voucher_Date, AD.Voucher_Miti, ad.Ledger_ID, ad.PartyName, ad.Cheque_No, ad.Cheque_Date, ad.Cheque_Miti
				)
			SELECT db.MODULE, db.VOUCHERNO, db.VOUCHERDATE, db.VOUCHERMITI, db.LEDGERTYPE, db.LEDGER_ID, CASE WHEN gl.GLType IN ( 'Cash', 'Bank' )AND ISNULL(db.PartyName, '') <> '' THEN gl.GLName+' ( '+db.PartyName+' )' ELSE gl.GLName END GlName, db.Cheque_No, db.Cheque_Date, db.Cheque_Miti, db.DEBIT, db.CREDIT
			  FROM DAYBOOK db
				   LEFT OUTER JOIN AMS.GeneralLedger gl ON db.Ledger_ID=gl.GLID
			 ORDER BY db.VOUCHERDATE, db.VOUCHERNO, db.Module, db.DEBIT DESC, db.CREDIT ASC, GlName; ");
        var dtDayBook = SqlExtensions.ExecuteDataSet(cmdBuilder.ToString()).Tables[0];

        dtReport = GetReports.IsTFormat switch
        {
            true => ReturnTFormatJournalVoucherDateWise(dtDayBook),
            _ => ReturnNormalJournalVoucher(dtDayBook)
        };
        return dtReport;
    }

    #endregion --------------- DAY BOOK  ---------------

    // CASH & BANK REPORTS

    #region --------------- CASH & BANK REPORTS ---------------

    // CASH BANK REPORT IN DETAILS

    #region --------------- DETAILS ---------------

    private DataTable ReturnNormalCashbook(DataTable dtCashBook)
    {
        DataRow newRow;
        var dtReport = dtCashBook.Clone();

        var dtLedgerGroup = dtCashBook.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("Ledger")
        }).Select(g => g.First()).CopyToDataTable();

        foreach (DataRow row in dtLedgerGroup.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["PartyLedger"] = row["Ledger"];
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, dtReport.RowsCount() + 1);
            decimal balanceAmount = 0;
            var rowOpening =
                (from r in dtCashBook.Rows.OfType<DataRow>()
                 where r["Module"].ToString() == "OB" && r["Ledger"].ToString() == row["Ledger"].GetString()
                 select r).FirstOrDefault();
            if (rowOpening != null)
            {
                balanceAmount = rowOpening["Balance"].GetDecimal();
                newRow = dtReport.NewRow();
                newRow["PartyLedger"] = rowOpening["PartyLedger"];
                newRow["Receipt"] = rowOpening["Receipt"];
                newRow["Payment"] = rowOpening["Payment"];
                newRow["Balance"] = Math.Abs(balanceAmount);
                newRow["BalanceType"] = balanceAmount > 0 ? "Dr" : balanceAmount < 0 ? "Cr" : "";
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, dtReport.RowsCount() + 1);
            }

            var exitsDetails = dtCashBook.Select($"Ledger='{row["Ledger"]}' and Module <> 'OB'");
            if (exitsDetails.Length > 0)
            {
                var details = exitsDetails.CopyToDataTable();
                if (details != null && details.Rows.Count > 0)
                {
                    var dtDate = details.AsEnumerable().GroupBy(r => new
                    {
                        LOGSTEP = r.Field<string>("Voucher_Date")
                    }).Select(g => g.First()).CopyToDataTable();

                    foreach (DataRow dr in dtDate.Rows)
                    {
                        var voucherDate = dr["Voucher_Date"];
                        var voucherMiti = dr["Voucher_Miti"];
                        var dateDetails = dtCashBook.Select($"Voucher_Date = '{voucherDate}'")
                            .OrderBy(dataRow => "Voucher_Date ASC").CopyToDataTable();
                        if (dateDetails != null && dateDetails.Rows.Count > 0)
                        {
                            var isFirst = true;
                            foreach (DataRow drRow in dateDetails.Rows)
                            {
                                newRow = dtReport.NewRow();
                                balanceAmount += drRow["Receipt"].GetDecimal() - drRow["Payment"].GetDecimal();
                                newRow["Module"] = drRow["Module"];
                                if (isFirst)
                                {
                                    newRow["Voucher_Date"] = voucherDate;
                                    newRow["Voucher_Miti"] = voucherMiti;
                                    isFirst = false;
                                }

                                newRow["Voucher_No"] = drRow["Voucher_No"];
                                newRow["PartyLedger"] = drRow["PartyLedger"];
                                newRow["CCode"] = drRow["CCode"];
                                newRow["Currency_Rate"] = drRow["Currency_Rate"];
                                newRow["Receipt"] = drRow["Receipt"];
                                newRow["Payment"] = drRow["Payment"];
                                newRow["Balance"] = Math.Abs(balanceAmount);
                                newRow["BalanceType"] = balanceAmount > 0 ? "Dr" : balanceAmount < 0 ? "Cr" : "";
                                newRow["IsGroup"] = 0;
                                dtReport.Rows.InsertAt(newRow, dtReport.RowsCount() + 1);
                            }

                            newRow = dtReport.NewRow();
                            newRow["PartyLedger"] = "DAY CLOSING >>";
                            newRow["Receipt"] = balanceAmount < 0
                                ? Math.Abs(balanceAmount).GetDecimalComma()
                                : string.Empty;
                            newRow["Payment"] = balanceAmount > 0
                                ? Math.Abs(balanceAmount).GetDecimalComma()
                                : string.Empty;
                            newRow["IsGroup"] = 13;
                            dtReport.Rows.InsertAt(newRow, dtReport.RowsCount() + 1);

                            newRow = dtReport.NewRow();
                            newRow["PartyLedger"] = "DAY OPENING >>";
                            newRow["Receipt"] = balanceAmount > 0
                                ? Math.Abs(balanceAmount).GetDecimalComma()
                                : string.Empty;
                            newRow["Payment"] = balanceAmount < 0
                                ? Math.Abs(balanceAmount).GetDecimalComma()
                                : string.Empty;
                            newRow["IsGroup"] = 13;
                            dtReport.Rows.InsertAt(newRow, dtReport.RowsCount() + 1);
                            voucherMiti = string.Empty;
                            voucherDate = string.Empty;
                        }
                    }
                }
            }
        }

        return dtReport;
    }

    private DataTable ReturnTFormatCashbook(DataTable dtCashBook)
    {
        DataRow newRow;
        var dtReport = new DataTable();
        dtReport.Columns.Add("dt_Date1", typeof(string));
        dtReport.Columns.Add("dt_Miti1", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo1", typeof(string));
        dtReport.Columns.Add("dt_Desc1", typeof(string));
        dtReport.Columns.Add("dt_Receipt", typeof(string));

        dtReport.Columns.Add("dt_Date2", typeof(string));
        dtReport.Columns.Add("dt_Miti2", typeof(string));
        dtReport.Columns.Add("dt_VoucherNo2", typeof(string));
        dtReport.Columns.Add("dt_Desc2", typeof(string));
        dtReport.Columns.Add("dt_Payment", typeof(string));
        dtReport.Columns.Add("dt_Module", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        var dtLedgerGroup = dtCashBook.AsEnumerable().GroupBy(r => new
        {
            LOGSTEP = r.Field<string>("CashLedger")
        }).Select(g => g.First()).CopyToDataTable();
        foreach (DataRow roType in dtLedgerGroup.Rows)
        {
            decimal amount = 0;
            var module = string.Empty;
            module = roType["Module"].ToString();
            amount += roType["Receipt"].GetDecimal();
            amount -= roType["Payment"].GetDecimal();

            if (amount > 0 || amount is 0)
            {
                newRow = dtReport.NewRow();
                newRow["dt_Desc1"] = roType["CashLedger"].ToString();
                newRow["IsGroup"] = 1;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }
            else
            {
                newRow = dtReport.NewRow();
                newRow["dt_Desc2"] = roType["CashLedger"].ToString();
                newRow["IsGroup"] = 1;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            }

            if (module.Equals("OB"))
            {
                if (amount > 0)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_VoucherNo1"] = "OB_BAL";
                    newRow["dt_Desc1"] = "OPENING_BALANCE";
                    newRow["dt_Receipt"] = roType["Receipt"].ToString();
                    newRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }
                else if (amount < 0)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_VoucherNo2"] = "OB_BAL";
                    newRow["dt_Desc2"] = "OPENING_BALANCE";
                    newRow["dt_Payment"] = roType["Payment"].ToString();
                    newRow["IsGroup"] = 0;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }
            }

            var dtFilterLedger = dtCashBook.Select($"CashLedger= '{roType["CashLedger"]}' and Voucher_No <> '' ")
                .CopyToDataTable();

            var groupByDate = dtFilterLedger.AsEnumerable().GroupBy(r => new
            {
                groupByDate = r.Field<DateTime>("Voucher_Date")
            }).Select(g => g.First()).CopyToDataTable();
            groupByDate.DefaultView.Sort = "Voucher_Date";

            foreach (DataRow drDate in groupByDate.Rows)
            {
                if (amount > 0)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_Desc1"] = "DAY OPENING >> ";
                    newRow["dt_Receipt"] = amount > 0 ? amount : string.Empty;
                    newRow["IsGroup"] = 44;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }
                else if (amount < 0)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_Desc2"] = "DAY OPENING >> ";
                    newRow["dt_Payment"] = amount < 0 ? Math.Abs(amount) : string.Empty;
                    newRow["IsGroup"] = 44;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Date1"] = drDate["Voucher_Date"].ToString();
                newRow["dt_Date2"] = drDate["Voucher_Date"].ToString();
                newRow["dt_Miti1"] = drDate["Voucher_Miti"].ToString();
                newRow["dt_Miti2"] = drDate["Voucher_Miti"].ToString();
                newRow["IsGroup"] = 1;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                var dtFilterByDate = dtFilterLedger.Select($"Voucher_Date= '{drDate["Voucher_Date"]}' ")
                    .CopyToDataTable();
                var groupByVoucher = dtFilterByDate.AsEnumerable().GroupBy(r => new
                {
                    groupByDate = r.Field<DateTime>("Voucher_Date"),
                    groupByVoucher = r.Field<string>("Voucher_No")
                }).Select(g => g.First()).CopyToDataTable();
                groupByVoucher.DefaultView.Sort = "Voucher_No";
                foreach (DataRow drVoucher in groupByVoucher.Rows)
                {
                    var voucherNo = string.Empty;
                    var dtVoucherFilter = dtFilterByDate.Select($"Voucher_No= '{drVoucher["Voucher_No"]}' ")
                        .CopyToDataTable();
                    foreach (DataRow details in dtVoucherFilter.Rows)
                    {
                        amount += details["Receipt"].GetDecimal();
                        amount -= details["Payment"].GetDecimal();
                        if (details["Receipt"].GetDecimal() > 0)
                        {
                            newRow = dtReport.NewRow();
                            newRow["dt_VoucherNo"] = voucherNo.IsBlankOrEmpty()
                                ? details["Voucher_No"].ToString()
                                : string.Empty;
                            newRow["dt_Desc"] = details["Ledger"].ToString();
                            newRow["dt_Receipt"] = details["Receipt"].ToString();
                            newRow["dt_Payment"] = details["Payment"].ToString();
                            newRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                        }
                        else if (details["Payment"].GetDecimal() > 0)
                        {
                            newRow = dtReport.NewRow();
                            newRow["dt_VoucherNo"] = voucherNo.IsBlankOrEmpty()
                                ? details["Voucher_No"].ToString()
                                : string.Empty;
                            newRow["dt_Desc"] = details["Ledger"].ToString();
                            newRow["dt_Receipt"] = details["Receipt"].ToString();
                            newRow["dt_Payment"] = details["Payment"].ToString();
                            newRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                        }
                    }

                    voucherNo = string.Empty;
                    if (!GetReports.IncludeVoucherTotal)
                    {
                        continue;
                    }

                    newRow = dtReport.NewRow();
                    newRow["dt_Desc"] = "VOUCHER TOTAL : ";
                    var debit = dtVoucherFilter.AsEnumerable().Sum(x =>
                        Convert.ToDecimal(x["Receipt"] == DBNull.Value ? 0 : x["Receipt"]));
                    var credit = dtVoucherFilter.AsEnumerable().Sum(x =>
                        Convert.ToDecimal(x["Payment"] == DBNull.Value ? 0 : x["Payment"]));
                    newRow["dt_Receipt"] = debit;
                    newRow["dt_Payment"] = credit;
                    newRow["IsGroup"] = 11;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }

                newRow = dtReport.NewRow();
                newRow["dt_Desc"] = "DAY TOTAL : ";
                var receipt = dtFilterByDate.AsEnumerable().Sum(x =>
                    Convert.ToDecimal(x["Receipt"] == DBNull.Value ? 0 : x["Receipt"]));
                var payment = dtFilterByDate.AsEnumerable().Sum(x =>
                    Convert.ToDecimal(x["Payment"] == DBNull.Value ? 0 : x["Payment"]));
                newRow["dt_Receipt"] = receipt;
                newRow["dt_Payment"] = payment;
                newRow["dt_Balance"] = Math.Abs(amount) > 0 ? Math.Abs(amount) : "NIL";
                newRow["dt_BalanceType"] = amount > 0 ? "Dr" : amount < 0 ? "Cr" : string.Empty;
                newRow["IsGroup"] = 22;
                dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

                if (Math.Abs(amount) > 0)
                {
                    newRow = dtReport.NewRow();
                    newRow["dt_Desc"] = "DAY CLOSING >> ";
                    newRow["dt_Receipt"] = amount < 0 ? Math.Abs(amount) : string.Empty;
                    newRow["dt_Payment"] = amount > 0 ? Math.Abs(amount) : string.Empty;
                    //newRow["dt_Balance"] = Math.Abs(amount);
                    //newRow["dt_BalanceType"] = amount > 0 ? "Dr" : amount < 0 ? "Cr" : string.Empty;
                    newRow["IsGroup"] = 33;
                    dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
                }
            }
        }

        return dtReport;
    }

    public DataTable GetCashBankDetailsReports()
    {
        var dtReport = new DataTable();
        var cmdString = $@"
			SELECT cb.Module , CONVERT(NVARCHAR(10), cb.Voucher_Date, 103) Voucher_Date, cb.Voucher_Miti,cb.Voucher_No, cb.Ledger, cb.PartyLedger, cb.Narration, cb.CCode,CASE when cb.Currency_Rate > 0 THEN FORMAT(cb.Currency_Rate,'##,##0.00') ELSE '' END Currency_Rate, CASE WHEN cb.Receipt>0 THEN FORMAT(cb.Receipt, '##,##0.00')ELSE '' END Receipt, CASE WHEN cb.Payment>0 THEN FORMAT(cb.Payment, '##,##0.00')ELSE '' END Payment, FORMAT(SUM(cb.Balance) OVER (PARTITION BY cb.Ledger ORDER BY cb.Ledger, cb.Voucher_Date ROWS UNBOUNDED PRECEDING), '##,##0.00') Balance,'' BalanceType, cb.Remarks,0 IsGroup
			FROM(SELECT 'OB' Module,'OB' Voucher_No, '' Voucher_Date, '' Voucher_Miti, gl.GLName Ledger, 'OPENING BALANCE' PartyLedger, '' Narration, 'NPR' CCode, 1 Currency_Rate, CASE WHEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)>0 THEN SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt)ELSE 0 END Receipt, CASE WHEN SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt)>0 THEN SUM(ad.LocalCredit_Amt-ad.LocalDebit_Amt)ELSE 0 END Payment, SUM(ad.LocalDebit_Amt-ad.LocalCredit_Amt) Balance, '' Remarks
				FROM AMS.AccountDetails ad
					INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
					INNER JOIN AMS.GeneralLedger pl ON pl.GLID=ad.CbLedger_ID
				WHERE ad.Ledger_ID IN ({GetReports.LedgerId}) AND ad.Voucher_Date<'{GetReports.FromDate.GetSystemDate()}' AND ad.Branch_ID = {ObjGlobal.SysBranchId}
				GROUP BY gl.GLName
				UNION ALL
				SELECT ad.Module,Voucher_No, CAST(Voucher_Date AS DATE) Voucher_Date, Voucher_Miti, gl.GLName Ledger, pl.GLName PartyLedger, ad.Narration, c.CCode, ad.Currency_Rate, ad.LocalDebit_Amt Receipt, ad.LocalCredit_Amt Payment, ad.LocalDebit_Amt-ad.LocalCredit_Amt Balance, ad.Remarks
				FROM AMS.AccountDetails ad
					INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
					INNER JOIN AMS.GeneralLedger pl ON pl.GLID=ad.CbLedger_ID
					INNER JOIN AMS.Currency c ON c.CId = ad.Currency_ID
				WHERE ad.Ledger_ID IN ({GetReports.LedgerId}) AND ad.FiscalYearId={ObjGlobal.SysFiscalYearId} AND ad.Voucher_Date BETWEEN '{GetReports.FromDate.GetSystemDate()}' AND '{GetReports.ToDate.GetSystemDate()}' AND ad.Branch_ID = {ObjGlobal.SysBranchId} )cb
			GROUP BY cb.Module,cb.Voucher_No, cb.Voucher_Date, cb.Voucher_Miti, cb.Ledger, cb.PartyLedger, cb.Narration, cb.Receipt, cb.Payment, cb.Balance, cb.Remarks, cb.CCode, cb.Currency_Rate
			ORDER BY cb.Voucher_Date, cb.Voucher_No, cb.PartyLedger, cb.Receipt;";
        var dtCashBook = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        dtReport = dtCashBook.Rows.Count switch
        {
            > 0 => ReturnNormalCashbook(dtCashBook),
            _ => dtReport
        };
        return dtReport;
    }

    #endregion --------------- DETAILS ---------------

    // CASH BANK REPORTS IN SUMMARY

    #region --------------- SUMMARY ---------------

    private DataTable ReturnSummaryCashBook(DataTable dtCashBook)
    {
        var dtReport = new DataTable();
        dtReport.Columns.Add("dt_Date", typeof(string));
        dtReport.Columns.Add("dt_Miti", typeof(string));
        dtReport.Columns.Add("dt_Desc", typeof(string));
        dtReport.Columns.Add("dt_Opening", typeof(string));
        dtReport.Columns.Add("dt_Receipt", typeof(string));
        dtReport.Columns.Add("dt_Payment", typeof(string));
        dtReport.Columns.Add("dt_Balance", typeof(string));
        dtReport.Columns.Add("dt_Filter", typeof(string));
        dtReport.Columns.Add("IsGroup", typeof(int));

        var view = new DataView(dtCashBook);
        var dtGroupBy = view.ToTable(true, "GLName");
        dtGroupBy.DefaultView.Sort = "GLName ASC";
        dtGroupBy = dtGroupBy.DefaultView.ToTable();

        DataRow newRow;
        var r = 0;
        foreach (DataRow roType in dtGroupBy.Rows)
        {
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = roType["GLName"].ToString();
            newRow["IsGroup"] = 1;
            dtReport.Rows.InsertAt(newRow, r + 1);
            r++;
            var dtVoucherDate = dtCashBook.Select($"GLName = '{roType["GLName"]}' ").CopyToDataTable();
            var Opening = 0.00;
            var Receipt = 0.00;
            var Payment = 0.00;
            var Balance = 0.00;

            foreach (DataRow drDate in dtVoucherDate.Rows)
            {
                newRow = dtReport.NewRow();
                Opening = ObjGlobal.ReturnDouble(drDate["OPENING"].ToString());

                newRow["dt_Date"] = Opening is 0 ? drDate["Voucher_Date"].ToString() : "OPENING";
                newRow["dt_Miti"] = Opening is 0 ? drDate["Voucher_Miti"].ToString() : "OPENING";
                newRow["dt_Opening"] = Opening != 0 ? Opening : Balance;
                Receipt = ObjGlobal.ReturnDouble(drDate["RECEIPT"].ToString());
                newRow["dt_Receipt"] = Receipt;
                Payment = ObjGlobal.ReturnDouble(drDate["PAYMENT"].ToString());
                newRow["dt_Payment"] = Payment;
                Balance += Opening + Receipt - Payment;
                newRow["dt_Balance"] = Balance;
                newRow["dt_Filter"] = roType["GLName"].ToString();
                newRow["IsGroup"] = 0;
                dtReport.Rows.InsertAt(newRow, r + 1);
                r++;
                Opening = 0;
                Receipt = 0;
                Payment = 0;
            }

            var dtLedgerTotal = dtReport.Select($"dt_Filter = '{roType["GLName"]}' ").CopyToDataTable();
            newRow = dtReport.NewRow();
            newRow["dt_Desc"] = "TOTAL BALANCE : ";
            newRow["dt_Receipt"] = dtLedgerTotal.AsEnumerable().Sum(x => x["dt_Receipt"].GetDecimal());
            newRow["dt_Payment"] = dtLedgerTotal.AsEnumerable().Sum(x => x["dt_Payment"].GetDecimal());
            newRow["dt_Balance"] = Balance;
            newRow["IsGroup"] = 99;
            dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
            Balance = 0;
        }

        return dtReport;
    }

    public DataTable GetCashBankSummaryReports()
    {
        var dtReport = new DataTable();
        var Query = $@"
			WITH CashBankSummary  AS
			(
				SELECT OB.Voucher_Date, OB.Voucher_Miti,OB.CbLedger_ID,
				CASE WHEN ( OB.LocalDebit_Amt -OB.LocalCredit_Amt) > 0 THEN  ( OB.LocalDebit_Amt -OB.LocalCredit_Amt) WHEN (OB.LocalCredit_Amt- OB.LocalDebit_Amt) > 0 THEN (OB.LocalCredit_Amt- OB.LocalDebit_Amt)  ELSE 0 end OPENING,0 RECEIPT,0 PAYMENT  FROM
				(
					SELECT  '' Voucher_Date,'' Voucher_Miti,ad.CbLedger_ID,SUM(ad.LocalDebit_Amt) LocalDebit_Amt,SUM(ad.LocalCredit_Amt) LocalCredit_Amt
					FROM AMS.AccountDetails ad WHERE ad.CbLedger_ID in ({GetReports.LedgerId})  AND  ad.Branch_id in ({GetReports.BranchId}) AND ad.Module in ('LOB','OB')
					GROUP BY ad.Ledger_ID,ad.CbLedger_ID
					UNION ALL
					SELECT '' Voucher_Date,'' Voucher_Miti,ad.CbLedger_ID,SUM(ad.LocalCredit_Amt) LocalDebit_Amt,SUM(ad.LocalDebit_Amt) LocalCredit_Amt
					FROM AMS.AccountDetails ad WHERE ad.CbLedger_ID in ({GetReports.LedgerId})  AND ad.FiscalYearId < {ObjGlobal.SysFiscalYearId}  AND  ad.Branch_id in ({GetReports.BranchId}) and Module not in ( 'LOB','OB')
					GROUP BY ad.Ledger_ID,ad.CbLedger_ID
					UNION ALL
					SELECT '' Voucher_Date,'' Voucher_Miti,ad.CbLedger_ID,SUM(ad.LocalCredit_Amt) LocalDebit_Amt,SUM(ad.LocalDebit_Amt) LocalCredit_Amt
					FROM AMS.AccountDetails ad WHERE ad.CbLedger_ID in ({GetReports.LedgerId})  AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}  AND  ad.Branch_id in ({GetReports.BranchId}) AND ad.Voucher_Date < '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' and Module not in ( 'LOB','OB')
					GROUP BY ad.Ledger_ID,ad.CbLedger_ID
				)  OB
				   UNION ALL
				SELECT ad.Voucher_Date, ad.Voucher_Miti,ad.CbLedger_ID, 0 OPENING,
				CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) > 0  THEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) ELSE 0 END RECEIPT,
				CASE WHEN SUM(ad.LocalCredit_Amt - ad.LocalDebit_Amt) < 0  THEN SUM(ad.LocalDebit_Amt - ad.LocalCredit_Amt) ELSE 0 END PAYMENT FROM AMS.AccountDetails ad
				WHERE ad.CbLedger_ID in ({GetReports.LedgerId})  AND ad.FiscalYearId = {ObjGlobal.SysFiscalYearId}  AND  ad.Branch_id in ({GetReports.BranchId}) AND ad.Voucher_Date BETWEEN '{Convert.ToDateTime(GetReports.FromDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToDate):yyyy-MM-dd}' and Module not in ( 'LOB','OB')
				GROUP BY ad.Voucher_Date, ad.Voucher_Miti,ad.CbLedger_ID
			) SELECT cbs.Voucher_Date, cbs.Voucher_Miti, cbs.CbLedger_ID,gl.GLName, cbs.OPENING, cbs.RECEIPT, cbs.PAYMENT FROM CashBankSummary cbs
			LEFT OUTER JOIN AMS.GeneralLedger gl ON cbs.CbLedger_ID = gl.GLID	";
        var dtCashBook = SqlExtensions.ExecuteDataSet(Query).Tables[0];
        if (dtCashBook.Rows.Count > 0)
        {
            dtReport = ReturnSummaryCashBook(dtCashBook);
        }

        return dtReport;
    }

    public DataTable GetMemberShipListSummary()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			SELECT mss.MShipDesc AS DESCRIPTION, mss.MShipShortName AS SHORTNAME, mss.PhoneNo AS PHONE_NO, mss.PriceTag AS OFFER_PRICE,mt.MemberDesc MEMBER_TYPE,mss.MValidDate START_FROM, mss.MExpireDate EXPIRE_ON,sb.AMOUNT
			FROM AMS.MemberShipSetup mss
			LEFT OUTER JOIN AMS.MemberType mt ON mss.MemberTypeId = mt.MemberTypeId
			LEFT OUTER JOIN
			( SELECT sm.MShipId ,SUM(sm.LN_Amount) AMOUNT FROM AMS.SB_Master sm GROUP BY sm.MShipId) sb ON mss.MShipId = sb.MShipId
			ORDER BY mss.MShipDesc; ");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    public DataTable GetMemberShipListDetails()
    {
        var cmdString = new StringBuilder();
        cmdString.Append(@"
			SELECT jd.Voucher_No,CASE WHEN (SELECT sc.Date_Type FROM AMS.SystemConfiguration sc) ='M' then Voucher_Miti ELSE CONVERT(varchar(10),Voucher_Date,103) END VoucherDate,
			gl.GLName Ledger, CAST(SUM(jd.LocalDebit) AS DECIMAL(18,2)) PAYMENT ,CAST(SUM(jd.LocalCredit) AS DECIMAL(18,2)) RECEIPT FROM AMS.JV_Details jd
			LEFT OUTER JOIN AMS.JV_Master jm ON jd.Voucher_No = jM.Voucher_No
			LEFT OUTER JOIN AMS.GeneralLedger gl ON jd.Ledger_ID = gl.GLID
			WHERE jm.VoucherMode ='PROV'
			GROUP BY jd.Voucher_No,Voucher_Miti,Voucher_Date,gl.GLName
			");
        return SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
    }

    #endregion --------------- SUMMARY ---------------

    #endregion --------------- CASH & BANK REPORTS ---------------

    // LIST OF MASTER

    #region --------------- LIST OF MASTER ---------------

    public DataTable GetGeneralLedgerListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP], ISNULL(a.AreaName, 'NO AREA') [LEDGER AREA], ISNULL(ja.AgentName, 'NO AGENT') [LEDGER AGENT], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT], CAST(gl.CrDays AS INT) [LIMIT DAYS],";
        cmdString += GetReports.IsLedgerContactDetails
            ? @"
			gl.GLAddress[ADDRESS], gl.PhoneNo[PHONE NO], gl.OwnerName[CONTACT PERSON], gl.LandLineNo[OFFICE NUMBER], gl.OwnerNumber[CONTACT NUMBER],"
            : "";
        cmdString += GetReports.IsLedgerScheme
            ? @"
			sm.SchemeDesc[SCHEME TAG], "
            : "";
        cmdString += @"
			gl.Status STATUS, GL.EnterBy[CREATED BY], CAST(GL.EnterDate AS DATE)[CREATED DATE],GL.SyncRowVersion[MODIFY TIMES], 0 IsGroup
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId = gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId = gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId = gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
				 LEFT OUTER JOIN AMS.Scheme_Master sm ON gl.Scheme = sm.SchemeId
			ORDER BY gl.GLName; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAccountGroupListMaster()
    {
        var cmdString = GetReports.IncludeLedger switch
        {
            true => @"
				SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status,0 IsGroup
				FROM AMS.GeneralLedger gl
					 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
					 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
					 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
					 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
				ORDER BY gl.GLName;",
            _ => @" 
				SELECT ag.GrpName [PARTICULAR],ag.NepaliDesc [NAME IN NEPALI], ag.GrpCode [GROUP SHORTNAME], ag.Schedule [REPORT SCHEDULE], 
				CASE WHEN ag.PrimaryGrp = 'BS' THEN 'BALANCE SHEET' WHEN ag.PrimaryGrp = 'PL' THEN 'PROFIT & LOSS' WHEN ag.PrimaryGrp = 'TA' THEN 'TRANDING ACCOUNT' ELSE '' END [PRIMARY GROUP],
				CASE WHEN ag.GrpType='L' THEN 'LAIBILITIES' WHEN ag.GrpType='E' THEN 'EXPENDITURE' WHEN ag.GrpType='A' THEN 'ASSETS' WHEN ag.GrpType='I' THEN 'INCOME' ELSE '' END  [GROUP TYPE], ag.Status, ag.EnterBy [CREATED BY], CAST(ag.EnterDate AS DATE) [CREATED DATE], ag.SyncRowVersion [MODIFY TIMES], 0 IsGroup
				FROM AMS.AccountGroup ag
				ORDER BY ag.GrpName;"
        };
        var result = new DataTable();
        if (GetReports.IncludeLedger)
        {
            var data = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            if (data.RowsCount() > 0)
            {
                using var dtLedgerGroup = data.AsEnumerable().GroupBy(r => new
                {
                    LOGSTEP = r.Field<string>("ACCOUNT GROUP")
                }).Select(g => g.First()).OrderBy(r => r.Field<string>("ACCOUNT GROUP")).CopyToDataTable();

                foreach (DataRow row in dtLedgerGroup.Rows)
                {
                }
            }
        }
        else
        {
            result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }

        return result;
    }

    public DataTable GetAccountSubGroupListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAreaListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetAgentListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSubLedgerListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetLedgerSubLedgerListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDepartmentListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDepartmentLedgerListMaster()
    {
        var cmdString = @"
			SELECT gl.GLCode [SHORT NAME], gl.GLName [LEDGER DESCRIPTION], gl.ACCode [ACCOUNT CODE], UPPER(gl.GLType) [LEDGER TYPE], ag.GrpName [ACCOUNT GROUP], ISNULL(asg.SubGrpName, 'NO ACCOUNT SUB GROP') [ACCOUNT SUB GROUP],ISNULL(a.AreaName,'NO AREA') [LEDGER AREA],ISNULL(JA.AgentName,'NO AGENT') [LEDGER AGENT], gl.GLAddress [ADDRESS], gl.PanNo [LEDGER PAN], CAST(gl.CrLimit AS DECIMAL(18, 2)) [CREDIT LIMIT],CAST(gl.CrDays AS INT) [LIMIT DAYS], GL.PhoneNo [PHONE NO],gl.OwnerName [CONTACT PERSON],gl.LandLineNo [OFFICE NUMBER],gl.OwnerNumber [CONTACT NUMBER],GL.Status
			FROM AMS.GeneralLedger gl
				 LEFT OUTER JOIN AMS.AccountGroup ag ON ag.GrpId=gl.GrpId
				 LEFT OUTER JOIN AMS.AccountSubGroup asg ON asg.SubGrpId=gl.SubGrpId
				 LEFT OUTER JOIN AMS.Area a ON a.AreaId=gl.AreaId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON ja.AgentId = gl.AgentId
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    #endregion --------------- LIST OF MASTER ---------------

    // OBJECT FOR FINANCE REPORTS

    #region --------------- OBJECT ---------------

    public VmFinanceReports GetReports { get; set; }
    public string PLAcDesc;
    public decimal PlAmount;

    private readonly string[] groupStrings =
    {
        "EXPENDITURE",
        "EXPENSES",
        "E"
    };

    #endregion --------------- OBJECT ---------------
}