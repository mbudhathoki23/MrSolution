using MoreLinq.Extensions;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Control;
using MrDAL.Reports.Interface;
using MrDAL.Reports.ViewModule;
using MrDAL.Reports.ViewModule.Object.Register;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using static MrDAL.Global.Common.ObjGlobal;

namespace MrDAL.Reports.Register;

public class ClsRegisterReport : IRegisterReport
{
	// CONSTRUCTER
	#region --------------- REGISTER REPORT ---------------
	public ClsRegisterReport()
	{
		GetReports = new VmRegisterReports();
	}
	#endregion --------------- REGISTER REPORT ---------------


	// MASTER VALUE ASSIGN
	#region --------------- MASTER VALUE ASSIGN ---------------

	public DataTable GetPurchaseSalesTermName(bool isPurchase)
	{
		var cmdString = isPurchase
			? " SELECT pt.PT_Id TermId, pt.PT_Name TermDesc, pt.PT_Sign TermSign FROM AMS.PT_Term pt WHERE pt.PT_Type <> 'A'  ORDER BY Order_No;"
			: " SELECT st.ST_Id TermId, st.ST_Name TermDesc,st.ST_Sign TermSign FROM AMS.ST_Term  st WHERE st.ST_Type <> 'A' ORDER BY st.Order_No; ";
		return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
	}

	#endregion --------------- MASTER VALUE ASSIGN ---------------


	// PURCHASE SUMMARY REPORTS
	#region ** ---------- SUMMARY ---------- **

	// PURCHASE INDENT REGISTER
	private string GeneratePurchaseIndentSummaryRegisterProductWiseScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','BT') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT pd.PB_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Vno IS NULL THEN pm.PB_Invoice ELSE pm.PB_Invoice+' ('+pm.PB_Vno+' )' END VoucherNoWithRef, pd.P_Id ProductId,p.PShortName ShortName, p.PName ProductDesc, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'PB' ELSE pm.Invoice_In END InvoiceMode, pm.Invoice_Type InvoiceType, UPPER(pm.Invoice_In) PaymentMode, pd.B_Amount BasicAmount, pd.N_Amount NetAmount, UPPER(pm.Enter_By) EnterBy
			FROM AMS.PB_Details pd
				 LEFT OUTER JOIN AMS.PB_Master pm ON pd.PB_Invoice = pm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON p.PID = pd.P_Id
				 LEFT OUTER JOIN AMS.ProductUnit pu ON pd.Unit_Id = pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON pd.Alt_UnitId = au.UID
				 LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId = pg.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId = psg.PSubGrpId
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID = gl.GLID
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.FiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND pd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY p.PName, pm.Invoice_Date, pd.PB_Invoice;

			SELECT * FROM
			(
				SELECT st1.PB_Vno VoucherNo, st1.PT_Id TermId, UPPER(st.PT_Name) TermDesc, Rate Rate,st1.Product_Id ProductId,p.PName ProductDesc, FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Amount
				FROM AMS.PB_Term st1
						LEFT OUTER JOIN AMS.PB_Master pm ON st1.PB_Vno=pm.PB_Invoice
						LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
						LEFT OUTER JOIN AMS.Product p ON st1.Product_Id = p.PID
				WHERE st1.Term_Type <> 'BT' and pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.FiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND st1.Product_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
				GROUP BY st1.PB_Vno, st1.PT_Id, st.PT_Name, Rate, st.PT_Sign,st1.Product_Id,p.PName
			) as d Pivot(max(Amount) FOR TermDesc in ({columnName}) ) as pid
			ORDER BY PName,VoucherNo;";
		return cmdString;
	}
	private string GeneratePurchaseIndentSummaryRegisterScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','B') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT sm.PB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_VNo IS NULL THEN sm.PB_Invoice ELSE sm.PB_Invoice+' ('+sm.PB_VNo+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Type='Normal' THEN 'PB' ELSE sm.Invoice_In END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Invoice_In) Payment_Mode,FORMAT(CAST(sd.BasicAmount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount, UPPER(sm.Enter_By) EnterBy, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
			FROM AMS.PB_Master sm
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID=gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				 LEFT OUTER JOIN(SELECT PB_Invoice, SUM(B_Amount) BasicAmount FROM AMS.PB_Details sd GROUP BY sd.PB_Invoice) AS sd ON sd.PB_Invoice=sm.PB_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";

		cmdString += GetReports.RptMode.ToUpper().Equals("VOUCHER WISE")
			? @" ORDER BY sm.PB_Invoice,sm.Invoice_Date; "
			: @" ORDER BY sm.Invoice_Date, sm.PB_Invoice; ";
		cmdString += @$"
			SELECT * FROM
			(
				SELECT sm.PB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_VNo IS NULL THEN sm.PB_Invoice ELSE sm.PB_Invoice+' ('+sm.PB_VNo+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Invoice_Type Invoice_Type, UPPER(sm.Invoice_In) Payment_Mode, UPPER(sm.Enter_By) EnterBy, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup,UPPER(st.PT_Name) TermDesc,FORMAT(CAST(SUM(st1.Amount) AS DECIMAL (18,2)),'##,##,###.00') Amount
				FROM AMS.PB_Term st1
					 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_Vno=sm.PB_Invoice
					 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID=gl.GLID
					 LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					 LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND st1.PB_Vno in (Select Value from [AMS].[fn_SplitString]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
				GROUP BY CONVERT(NVARCHAR, sm.Invoice_Date, 103), CASE WHEN sm.PB_VNo IS NULL THEN sm.PB_Invoice ELSE sm.PB_Invoice+' ('+sm.PB_VNo+' )' END, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END, UPPER(sm.Invoice_In), UPPER(sm.Enter_By), ISNULL(d.DName, 'NO-DEPARTMENT'), ISNULL(ja1.AgentName, 'NO-DOC AGENT'), ISNULL(a.AreaName, 'NO-AREA'), UPPER(st.PT_Name), sm.PB_Invoice, sm.Invoice_Miti, sm.Vendor_ID, sm.Invoice_Type, sm.Cls1, sm.Agent_ID, gl.AreaId
			) as d Pivot(max(Amount) FOR TermDesc in ({columnName} )) as pid
			ORDER BY VoucherNo ;";
		return cmdString;
	}
	public DataTable GetPurchaseIndentRegisterSummary()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseIndentSummaryRegisterProductWiseScript(),
			_ => GeneratePurchaseIndentSummaryRegisterScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//PURCHASE ORDER REGISTER
	private string GeneratePurchaseOrderSummaryRegisterProductWiseScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','BT') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT pd.PB_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Vno IS NULL THEN pm.PB_Invoice ELSE pm.PB_Invoice+' ('+pm.PB_Vno+' )' END VoucherNoWithRef, pd.P_Id ProductId,p.PShortName ShortName, p.PName ProductDesc, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'PB' ELSE pm.Invoice_In END InvoiceMode, pm.Invoice_Type InvoiceType, UPPER(pm.Invoice_In) PaymentMode, pd.B_Amount BasicAmount, pd.N_Amount NetAmount, UPPER(pm.Enter_By) EnterBy
			FROM AMS.PB_Details pd
				 LEFT OUTER JOIN AMS.PB_Master pm ON pd.PB_Invoice = pm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON p.PID = pd.P_Id
				 LEFT OUTER JOIN AMS.ProductUnit pu ON pd.Unit_Id = pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON pd.Alt_UnitId = au.UID
				 LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId = pg.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId = psg.PSubGrpId
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID = gl.GLID
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.FiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND pd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY p.PName, pm.Invoice_Date, pd.PB_Invoice;

			SELECT * FROM
			(
				SELECT st1.PB_Vno VoucherNo, st1.PT_Id TermId, UPPER(st.PT_Name) TermDesc, Rate Rate,st1.Product_Id ProductId,p.PName ProductDesc, FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Amount
				FROM AMS.PB_Term st1
						LEFT OUTER JOIN AMS.PB_Master pm ON st1.PB_Vno=pm.PB_Invoice
						LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
						LEFT OUTER JOIN AMS.Product p ON st1.Product_Id = p.PID
				WHERE st1.Term_Type <> 'BT' and pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.FiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND st1.Product_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
				GROUP BY st1.PB_Vno, st1.PT_Id, st.PT_Name, Rate, st.PT_Sign,st1.Product_Id,p.PName
			) as d Pivot(max(Amount) FOR TermDesc in ({columnName}) ) as pid
			ORDER BY PName,VoucherNo;";
		return cmdString;
	}
	private string GeneratePurchaseOrderSummaryRegisterScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','B') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT sm.PB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_VNo IS NULL THEN sm.PB_Invoice ELSE sm.PB_Invoice+' ('+sm.PB_VNo+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Type='Normal' THEN 'PB' ELSE sm.Invoice_In END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Invoice_In) Payment_Mode,FORMAT(CAST(sd.BasicAmount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount, UPPER(sm.Enter_By) EnterBy, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
			FROM AMS.PB_Master sm
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID=gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				 LEFT OUTER JOIN(SELECT PB_Invoice, SUM(B_Amount) BasicAmount FROM AMS.PB_Details sd GROUP BY sd.PB_Invoice) AS sd ON sd.PB_Invoice=sm.PB_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";

		cmdString += GetReports.RptMode.ToUpper().Equals("VOUCHER WISE")
			? @" ORDER BY sm.PB_Invoice,sm.Invoice_Date; "
			: @" ORDER BY sm.Invoice_Date, sm.PB_Invoice; ";
		cmdString += @$"
			SELECT * FROM
			(
				SELECT sm.PB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_VNo IS NULL THEN sm.PB_Invoice ELSE sm.PB_Invoice+' ('+sm.PB_VNo+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Invoice_Type Invoice_Type, UPPER(sm.Invoice_In) Payment_Mode, UPPER(sm.Enter_By) EnterBy, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup,UPPER(st.PT_Name) TermDesc,FORMAT(CAST(SUM(st1.Amount) AS DECIMAL (18,2)),'##,##,###.00') Amount
				FROM AMS.PB_Term st1
					 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_Vno=sm.PB_Invoice
					 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID=gl.GLID
					 LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					 LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND st1.PB_Vno in (Select Value from [AMS].[fn_SplitString]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
				GROUP BY CONVERT(NVARCHAR, sm.Invoice_Date, 103), CASE WHEN sm.PB_VNo IS NULL THEN sm.PB_Invoice ELSE sm.PB_Invoice+' ('+sm.PB_VNo+' )' END, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END, UPPER(sm.Invoice_In), UPPER(sm.Enter_By), ISNULL(d.DName, 'NO-DEPARTMENT'), ISNULL(ja1.AgentName, 'NO-DOC AGENT'), ISNULL(a.AreaName, 'NO-AREA'), UPPER(st.PT_Name), sm.PB_Invoice, sm.Invoice_Miti, sm.Vendor_ID, sm.Invoice_Type, sm.Cls1, sm.Agent_ID, gl.AreaId
			) as d Pivot(max(Amount) FOR TermDesc in ({columnName} )) as pid
			ORDER BY VoucherNo ;";
		return cmdString;
	}
	public DataTable GetPurchaseOrderRegisterSummary()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseOrderSummaryRegisterProductWiseScript(),
			_ => GeneratePurchaseOrderSummaryRegisterScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//PURCHASE CHALLAN REGISTER
	private string GeneratePurchaseChallanSummaryRegisterProductWiseScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','BT') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT pd.PB_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Vno IS NULL THEN pm.PB_Invoice ELSE pm.PB_Invoice+' ('+pm.PB_Vno+' )' END VoucherNoWithRef, pd.P_Id ProductId,p.PShortName ShortName, p.PName ProductDesc, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'PB' ELSE pm.Invoice_In END InvoiceMode, pm.Invoice_Type InvoiceType, UPPER(pm.Invoice_In) PaymentMode, pd.B_Amount BasicAmount, pd.N_Amount NetAmount, UPPER(pm.Enter_By) EnterBy
			FROM AMS.PB_Details pd
				 LEFT OUTER JOIN AMS.PB_Master pm ON pd.PB_Invoice = pm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON p.PID = pd.P_Id
				 LEFT OUTER JOIN AMS.ProductUnit pu ON pd.Unit_Id = pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON pd.Alt_UnitId = au.UID
				 LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId = pg.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId = psg.PSubGrpId
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID = gl.GLID
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.FiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND pd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY p.PName, pm.Invoice_Date, pd.PB_Invoice;

			SELECT * FROM
			(
				SELECT st1.PB_Vno VoucherNo, st1.PT_Id TermId, UPPER(st.PT_Name) TermDesc, Rate Rate,st1.Product_Id ProductId,p.PName ProductDesc, FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Amount
				FROM AMS.PB_Term st1
						LEFT OUTER JOIN AMS.PB_Master pm ON st1.PB_Vno=pm.PB_Invoice
						LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
						LEFT OUTER JOIN AMS.Product p ON st1.Product_Id = p.PID
				WHERE st1.Term_Type <> 'BT' and pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.FiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND st1.Product_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
				GROUP BY st1.PB_Vno, st1.PT_Id, st.PT_Name, Rate, st.PT_Sign,st1.Product_Id,p.PName
			) as d Pivot(max(Amount) FOR TermDesc in ({columnName}) ) as pid
			ORDER BY PName,VoucherNo;";
		return cmdString;
	}
	private string GeneratePurchaseChallanSummaryRegisterScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','B') AND PT_Type IN ('G','R') ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = @"
			SELECT *
			FROM(SELECT pm.PC_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Vno IS NULL THEN pm.PC_Invoice ELSE pm.PC_Invoice +' ('+pm.PB_Vno+' )' END VoucherNoWithRef, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'PB' ELSE pm.Invoice_Type END Invoice_Mode, pm.Invoice_Type Invoice_Type, UPPER(pm.Invoice_In) Payment_Mode,";
		cmdString += ServerVersion < 10
			? $@" CAST(pd.BasicAmount AS DECIMAL(18, {SysAmountLength})) BasicAmount, CAST(pm.LN_Amount AS DECIMAL(18, {SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST(pd.BasicAmount AS DECIMAL(18, {SysAmountLength})), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(pm.LN_Amount AS DECIMAL(18, {SysAmountLength})), '{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			@" UPPER(pm.Enter_By) EnterBy, pm.Counter_ID, ISNULL(c.CCode, 'NO-COUNTER') Counter, pm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, pm.Agent_ID DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.PC_Master pm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON pm.Counter_ID=c.CId
					  LEFT OUTER JOIN AMS.Department d ON pm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_ID=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT PC_Invoice, SUM(B_Amount) BasicAmount FROM AMS.PC_Details pd GROUP BY pd.PC_Invoice) AS pd ON pd.PC_Invoice=pm.PC_Invoice";
		cmdString += $@"
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND pm.R_Invoice = 1 "
			: " AND (pm.R_Invoice=0 OR pm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND pm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND pm.Counter_ID in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND pm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND pm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND pm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pm.PC_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  pm.Invoice_Type = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" pm sm.Invoice_In in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @" ) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += @"
				LEFT OUTER JOIN(SELECT * FROM(SELECT pm.PC_Invoice TermVoucherNo, UPPER(pt.PT_Name) TermDesc,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, {SysAmountLength})) Amount "
				: $@" FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount ";
			cmdString += $@"
								 FROM AMS.PC_Term st1
									  LEFT OUTER JOIN AMS.PC_Master pm ON st1.PC_VNo=pm.PC_Invoice
									  LEFT OUTER JOIN AMS.PT_Term pt ON st1.PT_Id=pt.PT_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
									  LEFT OUTER JOIN AMS.Counter c ON pm.Counter_ID=c.CId
									  LEFT OUTER JOIN AMS.Department d ON pm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				WHERE st1.Term_Type <> 'BT' AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.FiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND pm.R_Invoice = 1 "
				: " AND (pm.R_Invoice=0 OR pm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND pm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits()
				? $@" AND pm.Counter_ID in ({GetReports.CounterId}) "
				: " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND pm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND pm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND pm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND pm.PC_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  pm.Invoice_Type = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" pm sm.Invoice_In in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
				GROUP BY pm.PC_Invoice, UPPER(pt.PT_Name)) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode switch
		{
			"VOUCHER WISE" => @"
				ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;",
			_ => @"
				ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);"
		};
		return cmdString;
	}
	public DataTable GetPurchaseChallanRegisterSummary()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseChallanSummaryRegisterProductWiseScript(),
			_ => GeneratePurchaseChallanSummaryRegisterScript()
		};

		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//GET PURCHASE INVOICE REGISTER SUMMARY
	private string GeneratePurchaseInvoiceSummaryRegisterProductWiseScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','B') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = @"
			SELECT *,'' NetAmount
			FROM(SELECT sd.P_Id LedgerId, p.PName LedgerDesc, p.PShortName, pg.GrpCode, ISNULL(pg.GrpName, 'NO GROUP') GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP') SubGrpName,";
		cmdString +=
			$@" CAST(SUM(sd.Alt_Qty) AS DECIMAL(18, {SysQtyLength})) AltQty, au.UnitCode AltUom, CAST(SUM(sd.Qty) AS DECIMAL(18,{SysQtyLength})) Qty, pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(SUM(sd.B_Amount) AS DECIMAL(18,{SysAmountLength})) BasicAmount,"
			: $@" FORMAT(CAST(SUM(sd.B_Amount) AS DECIMAL(18,{SysAmountLength})), '{SysAmountCommaFormat}') BasicAmount, ";
		cmdString += $@" 0 IsGroup
				 FROM AMS.PB_Details sd
					  LEFT OUTER JOIN AMS.PB_Master sm ON sm.PB_Invoice = sd.PB_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID = sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID = p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({SysBranchId}) AND sm.FiscalYearId IN({SysFiscalYearId})AND(sm.CUnit_Id = '{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice = 0 OR sm.R_Invoice IS NULL) ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId})" : "";
		cmdString += GetReports.PGroupId.IsValueExits() ? $" AND pg.PGrpId IN ({GetReports.PGroupId})" : "";
		cmdString += GetReports.PSubGroupId.IsValueExits() ? $"AND psg.PSubGrpId IN ({GetReports.PSubGroupId}) " : "";
		cmdString += @"
				 GROUP BY ISNULL(pg.GrpName, 'NO GROUP'), ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP'), sd.P_Id, p.PName, p.PShortName, pg.GrpCode, psg.ShortName, au.UnitCode, pu.UnitCode)ma ";
		if (columnName.IsValueExits())
		{
			cmdString += @"
					 LEFT OUTER JOIN(SELECT *
										FROM(SELECT st1.Product_Id ProductId, UPPER(st.PT_Name) TermDesc,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, {SysAmountLength})) Amount "
				: $@" FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount ";
			cmdString += $@"
											 FROM AMS.PB_Term st1
												  LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
												  LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_ID
											 WHERE st1.Term_Type<>'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<> 'Cancel' AND sm.CBranch_Id IN ({SysBranchId}) AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
											 GROUP BY UPPER(st.PT_Name), st1.Product_Id) AS d
										PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.ProductId=ma.LedgerId";
		}

		cmdString += @"
			ORDER BY ma.LedgerDesc; ";
		return cmdString;
	}
	private string GeneratePurchaseInvoiceSummaryRegisterScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','B') AND PT_Type IN ('G','R') ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = @"
			SELECT *
			FROM(SELECT pm.PB_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Vno IS NULL THEN pm.PB_Invoice ELSE pm.PB_Invoice+' ('+pm.PB_Vno+' )' END VoucherNoWithRef, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'PB' ELSE pm.Invoice_Type END Invoice_Mode, pm.Invoice_Type Invoice_Type, UPPER(pm.Invoice_In) Payment_Mode,";
		cmdString += ServerVersion < 10
			? $@" CAST(pd.BasicAmount AS DECIMAL(18, {SysAmountLength})) BasicAmount, CAST(pm.LN_Amount AS DECIMAL(18, {SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST(pd.BasicAmount AS DECIMAL(18, {SysAmountLength})), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(pm.LN_Amount AS DECIMAL(18, {SysAmountLength})), '{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			@" UPPER(pm.Enter_By) EnterBy, pm.Counter_ID, ISNULL(c.CCode, 'NO-COUNTER') Counter, pm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, pm.Agent_ID DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.PB_Master pm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON pm.Counter_ID=c.CId
					  LEFT OUTER JOIN AMS.Department d ON pm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_ID=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT PB_Invoice, SUM(B_Amount) BasicAmount FROM AMS.PB_Details pd GROUP BY pd.PB_Invoice) AS pd ON pd.PB_Invoice=pm.PB_Invoice";
		cmdString += $@"
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND pm.R_Invoice = 1 "
			: " AND (pm.R_Invoice=0 OR pm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND pm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND pm.Counter_ID in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND pm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND pm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND pm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pm.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  pm.Invoice_Type = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" pm sm.Invoice_In in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @" ) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += @"
				LEFT OUTER JOIN(SELECT * FROM(SELECT pm.PB_Invoice TermVoucherNo, UPPER(pt.PT_Name) TermDesc,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, {SysAmountLength})) Amount "
				: $@" FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount ";
			cmdString += $@"
								 FROM AMS.PB_Term st1
									  LEFT OUTER JOIN AMS.PB_Master pm ON st1.PB_VNo=pm.PB_Invoice
									  LEFT OUTER JOIN AMS.PT_Term pt ON st1.PT_Id=pt.PT_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
									  LEFT OUTER JOIN AMS.Counter c ON pm.Counter_ID=c.CId
									  LEFT OUTER JOIN AMS.Department d ON pm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				WHERE st1.Term_Type <> 'BT' AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.fiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND pm.R_Invoice = 1 "
				: " AND (pm.R_Invoice=0 OR pm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND pm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits()
				? $@" AND pm.Counter_ID in ({GetReports.CounterId}) "
				: " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND pm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND pm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND pm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND pm.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  pm.Invoice_Type = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() && ("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" pm sm.Invoice_In in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
				GROUP BY pm.PB_Invoice, UPPER(pt.PT_Name)) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode switch
		{
			"VOUCHER WISE" => @"
				 ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;",
			_ => @"
				ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);"
		};
		return cmdString;
	}
	public DataTable GetPurchaseInvoiceRegisterSummary()
	{
		var cmdString = string.Empty;
		if (GetReports.RptMode.ToUpper() is "PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE")
			cmdString = GeneratePurchaseInvoiceSummaryRegisterProductWiseScript();
		else
			cmdString = GeneratePurchaseInvoiceSummaryRegisterScript();

		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsProductWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsProductGroupWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsProductSubGroupWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//GET PURCHASE RETURN REGISTER SUMMARY
	private string GeneratePurchaseReturnSummaryRegisterProductWiseScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','BT') AND PT_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT pd.PB_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Vno IS NULL THEN pm.PB_Invoice ELSE pm.PB_Invoice+' ('+pm.PB_Vno+' )' END VoucherNoWithRef, pd.P_Id ProductId,p.PShortName ShortName, p.PName ProductDesc, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'PB' ELSE pm.Invoice_In END InvoiceMode, pm.Invoice_Type InvoiceType, UPPER(pm.Invoice_In) PaymentMode, pd.B_Amount BasicAmount, pd.N_Amount NetAmount, UPPER(pm.Enter_By) EnterBy
			FROM AMS.PB_Details pd
				 LEFT OUTER JOIN AMS.PB_Master pm ON pd.PB_Invoice = pm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON p.PID = pd.P_Id
				 LEFT OUTER JOIN AMS.ProductUnit pu ON pd.Unit_Id = pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON pd.Alt_UnitId = au.UID
				 LEFT OUTER JOIN AMS.ProductGroup pg ON p.PGrpId = pg.PGrpId
				 LEFT OUTER JOIN AMS.ProductSubGroup psg ON p.PSubGrpId = psg.PSubGrpId
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID = gl.GLID
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.FiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND pd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY p.PName, pm.Invoice_Date, pd.PB_Invoice;

			SELECT * FROM
			(
				SELECT st1.PB_Vno VoucherNo, st1.PT_Id TermId, UPPER(st.PT_Name) TermDesc, Rate Rate,st1.Product_Id ProductId,p.PName ProductDesc, FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Amount
				FROM AMS.PB_Term st1
						LEFT OUTER JOIN AMS.PB_Master pm ON st1.PB_Vno=pm.PB_Invoice
						LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
						LEFT OUTER JOIN AMS.Product p ON st1.Product_Id = p.PID
				WHERE st1.Term_Type <> 'BT' and pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.FiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.ProductId.IsValueExits() ? $@" AND st1.Product_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pd.PB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
				GROUP BY st1.PB_Vno, st1.PT_Id, st.PT_Name, Rate, st.PT_Sign,st1.Product_Id,p.PName
			) as d Pivot(max(Amount) FOR TermDesc in ({columnName}) ) as pid
			ORDER BY PName,VoucherNo;";
		return cmdString;
	}
	private string GeneratePurchaseReturnSummaryRegisterScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + PT_Name + ']' FROM AMS.PT_Term WHERE PT_Condition IN ('P','B') AND PT_Type IN ('G','R') ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = @"
			SELECT *
			FROM(SELECT pm.PR_Invoice VoucherNo, CONVERT(NVARCHAR, pm.Invoice_Date, 103) VoucherDate, pm.Invoice_Miti VoucherMiti, CASE WHEN pm.PB_Invoice IS NULL THEN pm.PR_Invoice ELSE pm.PR_Invoice+' ('+pm.PB_Invoice+' )' END VoucherNoWithRef, pm.Vendor_ID LedgerId, CASE WHEN pm.Party_Name IS NOT NULL AND pm.Party_Name<>'' THEN gl.GLName+' ('+pm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, pm.Remarks Remarks, CASE WHEN pm.Invoice_Type='Normal' THEN 'SB' ELSE pm.Invoice_Type END Invoice_Mode, pm.Invoice_Type Invoice_Type, UPPER(pm.Invoice_In) Payment_Mode,";
		cmdString += ServerVersion < 10
			? $@" CAST(pd.BasicAmount AS DECIMAL(18, {SysAmountLength})) BasicAmount, CAST(pm.LN_Amount AS DECIMAL(18, {SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST(pd.BasicAmount AS DECIMAL(18, {SysAmountLength})), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(pm.LN_Amount AS DECIMAL(18, {SysAmountLength})), '{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			@" UPPER(pm.Enter_By) EnterBy, pm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, pm.Agent_ID DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.PR_Master pm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
					  LEFT OUTER JOIN AMS.Department d ON pm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_ID=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT PR_Invoice, SUM(B_Amount) BasicAmount FROM AMS.PR_Details pd GROUP BY pd.PR_Invoice) AS pd ON pd.PR_Invoice=pm.PR_Invoice";
		cmdString += $@"
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({GetReports.BranchId}) AND pm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( pm.CUnit_Id ='{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND pm.R_Invoice = 1 "
			: " AND (pm.R_Invoice=0 OR pm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND pm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND pm.Counter_ID in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND pm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND pm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND pm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND pm.PR_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  pm.Invoice_Type = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" pm sm.Invoice_In in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @" ) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += @"
				LEFT OUTER JOIN(SELECT *
							FROM(SELECT pm.PR_Invoice TermVoucherNo, UPPER(pt.PT_Name) TermDesc,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, {SysAmountLength})) Amount "
				: $@" FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount ";
			cmdString += $@"
								 FROM AMS.PR_Term st1
									  LEFT OUTER JOIN AMS.PR_Master pm ON st1.PR_VNo=pm.PR_Invoice
									  LEFT OUTER JOIN AMS.PT_Term pt ON st1.PT_Id=pt.PT_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
									  LEFT OUTER JOIN AMS.Department d ON pm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
				WHERE st1.Term_Type <> 'BT' AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({GetReports.BranchId}) AND pm.fiscalYearId IN({GetReports.FiscalYearId}) AND(pm.CUnit_Id = '{GetReports.CompanyUnitId}' OR pm.CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND pm.R_Invoice = 1 "
				: " AND (pm.R_Invoice=0 OR pm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND pm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND pm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND pm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND pm.Vendor_ID IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND pm.PR_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  pm.Invoice_Type = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" pm sm.Invoice_In in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
				GROUP BY pm.PR_Invoice, UPPER(pt.PT_Name)) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode switch
		{
			"VOUCHER WISE" => @"
				 ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;",
			_ => @"
				ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);"
		};
		return cmdString;
	}
	public DataTable GetPurchaseReturnRegisterSummary()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" => GeneratePurchaseReturnSummaryRegisterProductWiseScript(),
			"PRODUCT GROUP" => GeneratePurchaseReturnSummaryRegisterProductWiseScript(),
			"PRODUCT SUBGROUP WSE" => GeneratePurchaseReturnSummaryRegisterProductWiseScript(),
			_ => GeneratePurchaseReturnSummaryRegisterScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}

	#endregion ** ---------- SUMMARY ---------- **


	// PURCHASE DETAILS REPORTS
	#region ** ---------- DETAILS ---------- **


	// PURCHASE INDENT REGISTER
	private string GeneratePurchaseIndentDetailsRegisterProductWiseScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.AppendLine($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}
	private string GeneratePurchaseIndentDetailsRegisterScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign,st.Order_No
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}
	public DataTable GetPurchaseIndentRegisterDetails()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseIndentDetailsRegisterProductWiseScript(),
			_ => GeneratePurchaseIndentDetailsRegisterScript()
		};

		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister, true),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//PURCHASE ORDER REGISTER
	private string GeneratePurchaseOrderDetailsRegisterProductWiseScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.AppendLine($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}
	private string GeneratePurchaseOrderDetailsRegisterScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign,st.Order_No
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}
	public DataTable GetPurchaseOrderRegisterDetails()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" => GeneratePurchaseOrderDetailsRegisterProductWiseScript(),
			"PRODUCT GROUP" => GeneratePurchaseOrderDetailsRegisterProductWiseScript(),
			"PRODUCT SUBGROUP WSE" => GeneratePurchaseOrderDetailsRegisterProductWiseScript(),
			_ => GeneratePurchaseOrderDetailsRegisterScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"VENDOR WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//PURCHASE CHALLAN REGISTER
	private string GeneratePurchaseChallanDetailsRegisterProductWiseScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.AppendLine($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}
	private string GeneratePurchaseChallanDetailsRegisterScript()
	{
		var cmdString = @"
			SELECT sm.PC_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PC_Invoice ELSE sm.PC_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,";
		cmdString += ServerVersion < 10
			? $@" CAST( sm.LN_Amount  AS DECIMAL(18,{SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST( sm.LN_Amount  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			$@" UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PC_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PC_Invoice like '%{GetReports.FilterValue}%'  "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.PC_Invoice;

			SELECT sd.PC_Invoice VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, {SysQtyLength})) StockQty, pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(sd.Rate  AS DECIMAL(18,{SysAmountLength})) Rate,CAST(sd.B_Amount  AS DECIMAL(18,{SysAmountLength})) BasicAmount,"
			: $@" FORMAT(CAST( sd.Rate  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  BasicAmount,";
		cmdString +=
			$@" sd.Narration Narration,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName
			FROM AMS.PC_Details sd
				 LEFT OUTER JOIN AMS.PC_Master sm ON sd.PC_Invoice=sm.PC_Invoice
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PC_Invoice like '%{GetReports.FilterValue}%' "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.PC_Invoice,Invoice_SNo;

			SELECT st1.PC_VNo VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc, ";
		cmdString += ServerVersion < 10
			? $@" CAST(SUM(st1.Rate)  AS DECIMAL(18,{SysAmountLength})) Rate, CAST(SUM(st1.Amount)  AS DECIMAL(18,{SysAmountLength})) Amount "
			: $@" FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount ";
		cmdString += @$"
			,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName
			FROM AMS.PC_Term st1
				 LEFT OUTER JOIN AMS.PC_Master sm ON st1.PC_VNo=sm.PC_Invoice
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PC_Invoice like '%{GetReports.FilterValue}%' "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += @"
			GROUP BY st1.PC_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign,st.Order_No,sm.Party_Name,gl.GLName,Sm.Cls1,ISNULL(d.DName,'NO-DEPARTMENT'), gl.AgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT'),sm.Agent_Id,ISNULL(ja1.AgentName,'NO-DOC AGENT'),gl.AreaId,ISNULL(a.AreaName,'NO-AREA')
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PC_VNo,st.Order_No;";
		return cmdString;
	}
	public DataTable GetPurchaseChallanRegisterDetails()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseChallanDetailsRegisterProductWiseScript(),
			_ => GeneratePurchaseChallanDetailsRegisterScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"VENDOR WISE" => ReturnSalesPurchaseRegisterDetailsReportsLedgerWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	// GET PURCHASE INVOICE REGISTER DETAILS
	private string GeneratePurchaseInvoiceDetailsRegisterProductWiseScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(GetReports.ProductId.IsValueExits()
		   ? $@"
			AND sm.PB_Invoice IN (SELECT PB_Invoice FROM AMS.PB_Details WHERE P_Id = {GetReports.ProductId})  "
		   : " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(GetReports.ProductId.IsValueExits()
		   ? $@"
			AND sd.P_Id IN ({GetReports.ProductId})  "
		   : " ");
		cmdString.AppendLine($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(GetReports.ProductId.IsValueExits()
		   ? $@"
			AND sm.PB_Invoice IN (SELECT PB_Invoice FROM AMS.PB_Details WHERE P_Id = {GetReports.ProductId})  "
		   : " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign,st.Order_No
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}
	private string GeneratePurchaseInvoiceDetailsRegisterScript()
	{
		var cmdString = @"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,";
		cmdString += ServerVersion < 10
			? $@" CAST( sm.LN_Amount  AS DECIMAL(18,{SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST( sm.LN_Amount  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			$@" UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,1 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, {SysQtyLength})) StockQty, pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(sd.Rate  AS DECIMAL(18,{SysAmountLength})) Rate,CAST(sd.B_Amount  AS DECIMAL(18,{SysAmountLength})) BasicAmount,"
			: $@" FORMAT(CAST( sd.Rate  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  BasicAmount,";
		cmdString +=
			$@" sd.Narration Narration,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc, ";
		cmdString += ServerVersion < 10
			? $@" CAST(SUM(st1.Rate)  AS DECIMAL(18,{SysAmountLength})) Rate, CAST(SUM(st1.Amount)  AS DECIMAL(18,{SysAmountLength})) Amount "
			: $@" FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount ";
		cmdString += @$"
			,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += @"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign,st.Order_No,sm.Party_Name,gl.GLName,Sm.Cls1,ISNULL(d.DName,'NO-DEPARTMENT'), gl.AgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT'),sm.Agent_Id,ISNULL(ja1.AgentName,'NO-DOC AGENT'),gl.AreaId,ISNULL(a.AreaName,'NO-AREA')
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;";
		return cmdString;
	}
	public DataTable GetPurchaseInvoiceRegisterDetails()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseInvoiceDetailsRegisterProductWiseScript(),
			_ => GeneratePurchaseInvoiceDetailsRegisterScript()
		};

		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables.Count is 0)
		{
			return new DataTable();
		}
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"VENDOR WISE" => ReturnSalesPurchaseRegisterDetailsReportsLedgerWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}


	//GET PURCHASE RETURN REGISTER DETAILS
	private string GeneratePurchaseReturnDetailsRegisterProductWiseScript()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			SELECT sm.PB_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL then sm.PB_Invoice ELSE sm.PB_Invoice+' (' + sm.PB_Vno+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST( sm.LN_Amount  AS DECIMAL),'##,###.00') NetAmount,UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%'  "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice;

			SELECT sd.PB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18, 2)) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, 2)) StockQty, pu.UnitCode Uom, FORMAT(CAST( sd.Rate  AS DECIMAL),'##,###.00') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL),'##,###.00')  BasicAmount, sd.Narration Narration
			FROM AMS.PB_Details sd
				 LEFT OUTER JOIN AMS.PB_Master sm ON sd.PB_Invoice=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.AppendLine($@"
			ORDER BY sm.Invoice_Date,sm.PB_Invoice,Invoice_SNo;

			SELECT st1.PB_VNo VoucherNo,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc,FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL),'##,###.00')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL),'##,###.00')  Amount
			FROM AMS.PB_Term st1
				 LEFT OUTER JOIN AMS.PB_Master sm ON st1.PB_VNo=sm.PB_Invoice
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ");
		cmdString.Append(GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PB_Invoice like '%{GetReports.FilterValue}%' "
			: " ");
		cmdString.Append(GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ");
		cmdString.Append(@"
			GROUP BY st1.PB_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PB_VNo,st.Order_No;  ");
		return cmdString.ToString();
	}

	private string GeneratePurchaseReturnDetailsRegisterScript()
	{
		var cmdString = @"
			SELECT sm.PR_Invoice VoucherNo,CONVERT(NVARCHAR, sm.Invoice_Date,103) VoucherDate  ,sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Invoice IS NULL then sm.PR_Invoice ELSE sm.PR_Invoice+' (' + sm.PB_Invoice+' )' END VoucherNoWithRef, sm.Vendor_ID LedgerId,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Type ='NORMAL' THEN 'PB' ELSE sm.Invoice_Type END Invoice_Mode,sm.Invoice_Type,UPPER(sm.Invoice_In) Payment_Mode ,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,";
		cmdString += ServerVersion < 10
			? $@" CAST( sm.LN_Amount  AS DECIMAL(18,{SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST( sm.LN_Amount  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			$@" UPPER(sm.Enter_By) EnterBy,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName,0 IsGroup
			FROM AMS.PR_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PR_Invoice like '%{GetReports.FilterValue}%'  "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.PR_Invoice;

			SELECT sd.PR_Invoice VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty, au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18, {SysQtyLength})) StockQty, pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(sd.Rate  AS DECIMAL(18,{SysAmountLength})) Rate,CAST(sd.B_Amount  AS DECIMAL(18,{SysAmountLength})) BasicAmount,"
			: $@" FORMAT(CAST( sd.Rate  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,FORMAT(CAST(  sd.B_Amount  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  BasicAmount,";
		cmdString +=
			$@" sd.Narration Narration,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName
			FROM AMS.PR_Details sd
				 LEFT OUTER JOIN AMS.PR_Master sm ON sd.PR_Invoice=sm.PR_Invoice
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
				 LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id=pu.UID
				 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=sd.Alt_UnitId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PR_Invoice like '%{GetReports.FilterValue}%' "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.PR_Invoice,Invoice_SNo;

			SELECT st1.PR_VNo VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' then gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc,st.PT_Sign TermSign, st1.PT_Id TermId, st.PT_Sign, UPPER(st.PT_Name) TermDesc, ";
		cmdString += ServerVersion < 10
			? $@" CAST(SUM(st1.Rate)  AS DECIMAL(18,{SysAmountLength})) Rate, CAST(SUM(st1.Amount)  AS DECIMAL(18,{SysAmountLength})) Amount "
			: $@" FORMAT(CAST( SUM(st1.Rate)  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Rate, FORMAT(CAST( SUM(st1.Amount)  AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount ";
		cmdString += @$"
			,Sm.Cls1 DepartmentId,ISNULL(d.DName,'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT') LedgerAgent,sm.Agent_Id DocAgentId,ISNULL(ja1.AgentName,'NO-DOC AGENT') DocAgent,gl.AreaId,ISNULL(a.AreaName,'NO-AREA') AreaName
			FROM AMS.PR_Term st1
				 LEFT OUTER JOIN AMS.PR_Master sm ON st1.PR_VNo=sm.PR_Invoice
				 LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Vendor_ID = gl.GLID
				 LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				 LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				 LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id =ja1.AgentId
				 LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				 LEFT OUTER JOIN AMS.PT_Term st ON st1.PT_Id=st.PT_Id
			WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@"
			AND sm.PR_Invoice like '%{GetReports.FilterValue}%' "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits()
			? $@"
			AND sm.Vendor_ID in ({GetReports.LedgerId})  "
			: " ";
		cmdString += @"
			GROUP BY st1.PR_VNo,st1.PT_Id,st.PT_Name,Rate,st.PT_Sign,st.Order_No,sm.Party_Name,gl.GLName,Sm.Cls1,ISNULL(d.DName,'NO-DEPARTMENT'), gl.AgentId,ISNULL( ja.AgentName,'NO-LEDGER AGENT'),sm.Agent_Id,ISNULL(ja1.AgentName,'NO-DOC AGENT'),gl.AreaId,ISNULL(a.AreaName,'NO-AREA')
			HAVING SUM(st1.Amount) <> 0
			ORDER BY st1.PR_VNo,st.Order_No;";
		return cmdString;
	}

	public DataTable GetPurchaseReturnRegisterDetails()
	{
		var cmdString = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GeneratePurchaseReturnDetailsRegisterProductWiseScript(),
			_ => GeneratePurchaseReturnDetailsRegisterScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"VENDOR WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"USER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister, true),
			_ => new DataTable()
		};
		return dtReports;
	}

	#endregion ** ---------- DETAILS ---------- **


	// SALES SUMMARY REPORTS
	#region ** ---------- SUMMARY ---------- **

	// SALES QUOTATION REGISTER
	private string GetSalesQuotationRegisterProductSummaryReportsScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = columnName.IsValueExits()
			? @"
			SELECT *,'' NetAmount "
			: @"
			SELECT ma.LedgerId, ma.LedgerDesc, ma.PShortName, ma.GrpCode, ma.GrpName, ma.SubGroupCode, ma.SubGrpName, ma.AltQty, ma.AltUom, ma.Qty, ma.Uom, ma.BasicAmount,ma.BasicAmount NetAmount,ma.IsGroup ";

		cmdString += $@"
			FROM(SELECT sd.P_Id LedgerId, p.PName LedgerDesc, p.PShortName, pg.GrpCode, ISNULL(pg.GrpName, 'NO GROUP') GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP') SubGrpName, CAST(SUM(sd.Alt_Qty) AS DECIMAL(18, 2)) AltQty, au.UnitCode AltUom, CAST(SUM(sd.Qty) AS DECIMAL(18, 2)) Qty, pu.UnitCode Uom, FORMAT(CAST(SUM(sd.B_Amount) AS DECIMAL(18, 6)), '##,##,###.00') BasicAmount, 0 IsGroup
				 FROM AMS.SB_Details sd
					  LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<> 'Cancel' AND sm.CBranch_Id IN ({SysBranchId}) AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
				 GROUP BY ISNULL(pg.GrpName, 'NO GROUP'), ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP'), sd.P_Id, p.PName, p.PShortName, pg.GrpCode, psg.ShortName, au.UnitCode, pu.UnitCode)ma ";
		cmdString += columnName.IsValueExits()
			? $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '##,##,###.00') Amount
									 FROM AMS.SB_Term st1
										  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									 WHERE st1.Term_Type<>'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
									 GROUP BY UPPER(st.ST_Name), st1.Product_Id) AS d
								PIVOT(MAX(Amount)
									  FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.ProductId=ma.LedgerId"
			: " ";
		cmdString += @"
			ORDER BY ma.LedgerDesc; ";
		return cmdString;
	}

	private string GetSalesQuotationSummaryRegisterVoucherWise()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode='Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Payment_Mode) Payment_Mode, FORMAT(CAST(sd.BasicAmount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(sm.LN_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') NetAmount, UPPER(sm.Enter_By) EnterBy, sm.CounterId, ISNULL(c.CCode, 'NO-COUNTER') Counter, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.SB_Master sm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
					  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT SB_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SB_Details sd GROUP BY sd.SB_Invoice) AS sd ON sd.SB_Invoice=sm.SB_Invoice ";
		cmdString += $@"
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND sm.R_Invoice = 1 "
			: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @" ) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
							FROM(SELECT sm.SB_Invoice TermVoucherNo, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
								 FROM AMS.SB_Term st1
									  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
									  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
									  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
									  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId ";
			cmdString +=
				$@" WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND sm.R_Invoice = 1 "
				: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString +=
				$@" GROUP BY sm.SB_Invoice, UPPER(st.ST_Name)) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode.Equals("VOUCHER WISE")
			? @" ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;"
			: @" ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);";
		return cmdString;
	}

	public DataTable GetSalesQuotationRegisterSummary()
	{
		var _getQuery = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" => GetSalesQuotationRegisterProductSummaryReportsScript(),
			"PRODUCT GROUP" => GetSalesQuotationRegisterProductSummaryReportsScript(),
			"PRODUCT SUBGROUP WSE" => GetSalesQuotationRegisterProductSummaryReportsScript(),
			_ => GetSalesQuotationSummaryRegisterVoucherWise()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(_getQuery);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES ORDER REGISTER
	private string GetSalesOrderRegisterProductSummaryReportsScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = columnName.IsValueExits()
			? @"
			SELECT *,'' NetAmount "
			: @"
			SELECT ma.LedgerId, ma.LedgerDesc, ma.PShortName, ma.GrpCode, ma.GrpName, ma.SubGroupCode, ma.SubGrpName, ma.AltQty, ma.AltUom, ma.Qty, ma.Uom, ma.BasicAmount,ma.BasicAmount NetAmount,ma.IsGroup ";

		cmdString += $@"
			FROM(SELECT sd.P_Id LedgerId, p.PName LedgerDesc, p.PShortName, pg.GrpCode, ISNULL(pg.GrpName, 'NO GROUP') GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP') SubGrpName, CAST(SUM(sd.Alt_Qty) AS DECIMAL(18, 2)) AltQty, au.UnitCode AltUom, CAST(SUM(sd.Qty) AS DECIMAL(18, 2)) Qty, pu.UnitCode Uom, FORMAT(CAST(SUM(sd.B_Amount) AS DECIMAL(18, 6)), '##,##,###.00') BasicAmount, 0 IsGroup
				 FROM AMS.SB_Details sd
					  LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<> 'Cancel' AND sm.CBranch_Id IN ({SysBranchId}) AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
				 GROUP BY ISNULL(pg.GrpName, 'NO GROUP'), ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP'), sd.P_Id, p.PName, p.PShortName, pg.GrpCode, psg.ShortName, au.UnitCode, pu.UnitCode)ma ";
		cmdString += columnName.IsValueExits()
			? $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '##,##,###.00') Amount
									 FROM AMS.SB_Term st1
										  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									 WHERE st1.Term_Type<>'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
									 GROUP BY UPPER(st.ST_Name), st1.Product_Id) AS d
								PIVOT(MAX(Amount)
									  FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.ProductId=ma.LedgerId"
			: " ";
		cmdString += @"
			ORDER BY ma.LedgerDesc; ";
		return cmdString;
	}

	private string GetSalesOrderSummaryRegisterVoucherWise()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SO_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.Ref_Vno IS NULL THEN sm.SO_Invoice ELSE sm.SO_Invoice+' ('+sm.Ref_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode='Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Payment_Mode) Payment_Mode, FORMAT(CAST(sd.BasicAmount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(sm.LN_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') NetAmount, UPPER(sm.Enter_By) EnterBy, sm.CounterId, ISNULL(c.CCode, 'NO-COUNTER') Counter, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.SO_Master sm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
					  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT SO_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SO_Details sd GROUP BY sd.SO_Invoice) AS sd ON sd.SO_Invoice=sm.SO_Invoice ";
		cmdString += $@"
				WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND sm.R_Invoice = 1 "
			: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SO_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT sm.SO_Invoice TermVoucherNo, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SO_Term st1
									  LEFT OUTER JOIN AMS.SO_Master sm ON st1.SO_VNo=sm.SO_Invoice
									  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
									  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
									  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId ";
			cmdString +=
				$@" WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND sm.R_Invoice = 1 "
				: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
				  GROUP BY sm.SO_Invoice, UPPER(st.ST_Name)) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo  ";
		}

		cmdString += GetReports.RptMode.Equals("VOUCHER WISE")
			? @" ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;"
			: @" ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);";
		return cmdString;
	}

	public DataTable GetSalesOrderRegisterSummary()
	{
		var _getQuery = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" => GetSalesOrderRegisterProductSummaryReportsScript(),
			"PRODUCT GROUP" => GetSalesOrderRegisterProductSummaryReportsScript(),
			"PRODUCT SUBGROUP WSE" => GetSalesOrderRegisterProductSummaryReportsScript(),
			_ => GetSalesOrderSummaryRegisterVoucherWise()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(_getQuery);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES CHALLAN REGISTER
	public string GetSalesChallanRegisterProductSummaryReportsScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = columnName.IsValueExits()
			? @"
			SELECT *,'' NetAmount "
			: @"
			SELECT ma.LedgerId, ma.LedgerDesc, ma.PShortName, ma.GrpCode, ma.GrpName, ma.SubGroupCode, ma.SubGrpName, ma.AltQty, ma.AltUom, ma.Qty, ma.Uom, ma.BasicAmount,ma.BasicAmount NetAmount,ma.IsGroup ";

		cmdString += $@"
			FROM(SELECT sd.P_Id LedgerId, p.PName LedgerDesc, p.PShortName, pg.GrpCode, ISNULL(pg.GrpName, 'NO GROUP') GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP') SubGrpName, CAST(SUM(sd.Alt_Qty) AS DECIMAL(18, 2)) AltQty, au.UnitCode AltUom, CAST(SUM(sd.Qty) AS DECIMAL(18, 2)) Qty, pu.UnitCode Uom, FORMAT(CAST(SUM(sd.B_Amount) AS DECIMAL(18, 6)), '##,##,###.00') BasicAmount, 0 IsGroup
				 FROM AMS.SB_Details sd
					  LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<> 'Cancel' AND sm.CBranch_Id IN ({SysBranchId}) AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
				 GROUP BY ISNULL(pg.GrpName, 'NO GROUP'), ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP'), sd.P_Id, p.PName, p.PShortName, pg.GrpCode, psg.ShortName, au.UnitCode, pu.UnitCode)ma ";
		cmdString += columnName.IsValueExits()
			? $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '##,##,###.00') Amount
									 FROM AMS.SB_Term st1
										  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									 WHERE st1.Term_Type<>'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
									 GROUP BY UPPER(st.ST_Name), st1.Product_Id) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.ProductId=ma.LedgerId"
			: " ";
		cmdString += @"
			ORDER BY ma.LedgerDesc; ";
		return cmdString;
	}

	public string GetSalesChallanRegisterSummaryReportsScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SC_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.Ref_Vno IS NULL THEN sm.SC_Invoice ELSE sm.SC_Invoice+' ('+sm.Ref_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode='Normal' THEN 'SO' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Payment_Mode) Payment_Mode, FORMAT(CAST(sd.BasicAmount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(sm.LN_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') NetAmount, UPPER(sm.Enter_By) EnterBy, sm.CounterId, ISNULL(c.CCode, 'NO-COUNTER') Counter, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.SC_Master sm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
					  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT SC_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SC_Details sd GROUP BY sd.SC_Invoice) AS sd ON sd.SC_Invoice=sm.SC_Invoice ";
		cmdString += $@"
				WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND sm.R_Invoice = 1 "
			: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SC_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
							FROM(SELECT sm.SC_Invoice TermVoucherNo, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
								 FROM AMS.SC_Term st1
									  LEFT OUTER JOIN AMS.SC_Master sm ON st1.SC_VNo=sm.SC_Invoice
									  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
									  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
									  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId  ";
			cmdString += $@"
							WHERE st1.Term_Type <> 'BT' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND sm.R_Invoice = 1 "
				: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND sm.SC_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
							GROUP BY sm.SC_Invoice, UPPER(st.ST_Name)) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode switch
		{
			"VOUCHER WISE" => @"
				ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;",
			_ => @"
				ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);"
		};
		return cmdString;
	}

	public DataTable GetSalesChallanRegisterSummary()
	{
		var _getQuery = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GetSalesChallanRegisterProductSummaryReportsScript(),
			"PRODUCT GROUP" => GetSalesChallanRegisterProductSummaryReportsScript(),
			"PRODUCT SUBGROUP WSE" => GetSalesChallanRegisterProductSummaryReportsScript(),
			_ => GetSalesChallanRegisterSummaryReportsScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(_getQuery);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES INVOICE REGISTER
	public string GetSalesRegisterProductSummaryReportsScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = columnName.IsValueExits()
			? @"
			SELECT *,'' NetAmount "
			: @"
			SELECT ma.LedgerId, ma.LedgerDesc, ma.PShortName, ma.GrpCode, ma.GrpName, ma.SubGroupCode, ma.SubGrpName, ma.AltQty, ma.AltUom, ma.Qty, ma.Uom, ma.BasicAmount,ma.BasicAmount NetAmount,ma.IsGroup ";

		cmdString += $@"
			FROM(SELECT sd.P_Id LedgerId, p.PName LedgerDesc, p.PShortName, pg.GrpCode, ISNULL(pg.GrpName, 'NO GROUP') GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP') SubGrpName, CAST(SUM(sd.Alt_Qty) AS DECIMAL(18, {SysAmountLength})) AltQty, au.UnitCode AltUom, CAST(SUM(sd.Qty) AS DECIMAL(18, {SysAmountLength})) Qty, pu.UnitCode Uom, FORMAT(CAST(SUM(sd.B_Amount) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, 0 IsGroup
				 FROM AMS.SB_Details sd
					  LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<> 'CANCEL' AND sm.CBranch_Id IN ({SysBranchId}) AND sm.FiscalYearId IN ({SysFiscalYearId})AND ISNULL(sm.CUnit_Id,0) = {SysCompanyUnitId} AND ISNULL(sm.R_Invoice,0) = 0 ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId})" : "";
		cmdString += @"
				 GROUP BY ISNULL(pg.GrpName, 'NO GROUP'), ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP'), sd.P_Id, p.PName, p.PShortName, pg.GrpCode, psg.ShortName, au.UnitCode, pu.UnitCode)ma ";
		if (columnName.IsValueExits())
		{
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SB_Term st1
										  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									 WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'  ";
			cmdString += GetReports.ProductId.IsValueExits() ? $" AND st1.Product_Id IN ({GetReports.ProductId}) " : "";
			cmdString += $@"
									 GROUP BY UPPER(st.ST_Name), st1.Product_Id) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.ProductId = ma.LedgerId ";
		}

		cmdString += @"
			ORDER BY ma.LedgerDesc; ";
		return cmdString;
	}

	public string GetSalesRegisterSummaryReportsScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = @"
			SELECT *
			FROM(SELECT sm.SB_Invoice VoucherNo, CONVERT(NVARCHAR(10), sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN sm.Party_Name ELSE gl.GLName END LedgerDesc, gl.PanNo, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode='Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Payment_Mode) Payment_Mode,";
		cmdString +=
			$@" CASE WHEN ISNULL(sm.R_Invoice, 0)>0 THEN '' ELSE FORMAT(CAST(sd.BasicAmount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') END BasicAmount,CASE WHEN ISNULL(sm.R_Invoice, 0)>0 THEN '' ELSE FORMAT(CAST(sd.Qty AS DECIMAL(18, 6)), '{SysQtyCommaFormat}') END Qty,  CASE WHEN ISNULL(sm.R_Invoice, 0)>0 THEN '' ELSE FORMAT(CAST(sm.LN_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') END NetAmount,";
		cmdString +=
			@" UPPER(sm.Enter_By) EnterBy, sm.CounterId, ISNULL(c.CCode, 'NO-COUNTER') Counter, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.SB_Master sm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
					  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN(SELECT SB_Invoice, SUM(B_Amount) BasicAmount, SUM(sd.Qty) Qty  FROM AMS.SB_Details sd GROUP BY sd.SB_Invoice) AS sd ON sd.SB_Invoice=sm.SB_Invoice ";
		cmdString += $@"
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.FiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR sm.CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND sm.R_Invoice = 1 "
			: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @" ) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += @"
				LEFT OUTER JOIN(SELECT * FROM ( SELECT sm.SB_Invoice TermVoucherNo, UPPER(st.ST_Name) TermDesc,";
			cmdString +=
				$@" FORMAT(CAST(SUM(ISNULL(CASE WHEN ISNULL(sm.R_Invoice, 0)>0 THEN 0 ELSE st1.Amount END, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount ";
			cmdString += @" FROM AMS.SB_Term st1
									  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
									  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
									  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
									  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
									  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
									  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
									  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId ";
			cmdString +=
				$@" WHERE st1.Term_Type <> 'BT' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.FiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
							   GROUP BY sm.SB_Invoice, UPPER(st.ST_Name)) AS d
				PIVOT(MAX(Amount)
					FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode.Equals("VOUCHER WISE")
			? @"
			ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;"
			: @"
			ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);";
		return cmdString;
	}

	public string GetSalesRegisterSummaryPartialReportsScript()
	{
		var cmdString = string.Empty;
		switch (GetReports.Module)
		{
			case "SB":
				{
					cmdString = $@"
						SELECT sm.SB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, ISNULL(ss.LedgerId, sm.Customer_Id) LedgerId, ISNULL(sm.Party_Name, gl.GLName) LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode='NORMAL' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(ISNULL(ss.PaymentMode, sm.Payment_Mode)) Payment_Mode, ISNULL(ss.Amount, sm.LN_Amount) NetAmount, UPPER(sm.Enter_By) EnterBy, sm.CounterId,0 IsGroup
						FROM AMS.SB_Master sm
							 LEFT OUTER JOIN AMS.InvoiceSettlement ss ON ss.SB_Invoice=sm.SB_Invoice
							 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ISNULL(ss.LedgerId, sm.Customer_Id)
						WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
					cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
						? @" AND sm.R_Invoice = 1 "
						: " AND (sm.R_Invoice = 0 OR sm.R_Invoice IS NULL) ";
					cmdString += GetReports.EntryUser.IsValueExits()
						? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
						: " ";
					cmdString += @"
						ORDER BY sm.Invoice_Date,CAST(AMS.GetNumericValue(sm.SB_Invoice) AS NUMERIC); ";
					break;
				}
		}

		return cmdString;
	}

	public DataTable GenerateSalesInvoiceRegisterSummaryReports()
	{
		var _getQuery = string.Empty;
		if (GetReports.InvoiceCategory.GetUpper().Equals("PARTIAL"))
			_getQuery = GetSalesRegisterSummaryPartialReportsScript();
		else
			_getQuery = GetReports.RptMode switch
			{
				"PRODUCT WISE" => GetSalesRegisterProductSummaryReportsScript(),
				"PRODUCT GROUP" => GetSalesRegisterProductSummaryReportsScript(),
				"PRODUCT SUBGROUP WSE" => GetSalesRegisterProductSummaryReportsScript(),
				_ => GetSalesRegisterSummaryReportsScript()
			};
		var dsRegister = SqlExtensions.ExecuteDataSet(_getQuery);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister),
			"USER WISE" when GetReports.InvoiceCategory.GetUpper() != "PARTIAL" =>
				ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister),
			"USER WISE" when GetReports.InvoiceCategory.GetUpper() == "PARTIAL" =>
				ReturnSalesPurchaseRegisterSummaryReportsUserWisePartialPayment(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsProductWise(dsRegister),
			"LEDGER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	public DataTable GetSalesInvoiceRegisterInvoiceTypeSummary()
	{
		var _getQuery = GetReports.RptMode.GetUpper() switch
		{
			"PRODUCT WISE" or "PRODUCT GROUP" or "PRODUCT SUBGROUP WSE" =>
				GetSalesRegisterProductSummaryReportsScript(),
			"USER WISE" when GetReports.InvoiceCategory.GetUpper() is "PARTIAL" =>
				GetSalesRegisterSummaryPartialReportsScript(),
			_ => GetSalesRegisterSummaryReportsScript()
		};

		var dsRegister = SqlExtensions.ExecuteDataSet(_getQuery);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister),
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"USER WISE" when GetReports.InvoiceCategory.GetUpper() != "PARTIAL" =>
				ReturnSalesPurchaseRegisterSummaryReportsUserWiseIncludeInvoiceType(dsRegister),
			"USER WISE" when GetReports.InvoiceCategory.GetUpper() is "PARTIAL" =>
				ReturnSalesPurchaseRegisterSummaryReportsUserWisePartialPayment(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES RETURN REGISTER
	private string GetSalesReturnSummaryRegisterVoucherWise()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SR_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.SB_Invoice IS NULL THEN sm.SR_Invoice ELSE sm.SR_Invoice+' ('+sm.SB_Invoice+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode='Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER(sm.Payment_Mode) Payment_Mode, FORMAT(CAST(sd.BasicAmount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, FORMAT(CAST(sm.LN_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') NetAmount, UPPER(sm.Enter_By) EnterBy, sm.CounterId, ISNULL(c.CCode, 'NO-COUNTER') Counter, sm.Cls1 DepartmentId, ISNULL(d.DName, 'NO-DEPARTMENT') Department, gl.AgentId LedgerAgentId, ISNULL(ja.AgentName, 'NO-LEDGER AGENT') LedgerAgent, sm.Agent_Id DocAgentId, ISNULL(ja1.AgentName, 'NO-DOC AGENT') DocAgent, gl.AreaId, ISNULL(a.AreaName, 'NO-AREA') AreaName, 0 IsGroup
				 FROM AMS.SR_Master sm
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
					  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
					  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
					  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
					  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId
					  LEFT OUTER JOIN
					  (
						SELECT SR_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SR_Details sd GROUP BY sd.SR_Invoice
					  ) AS sd ON sd.SR_Invoice=sm.SR_Invoice";
		cmdString += $@"
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
			? @" AND sm.R_Invoice = 1 "
			: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SR_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString +=
			GetReports.InvoiceType.IsValueExits() && ("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
				? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
				: " ";
		cmdString +=
			GetReports.InvoiceCategory.IsValueExits() &&
			!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
				? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
				: " ";
		cmdString += @" ) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT sm.SR_Invoice TermVoucherNo, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SR_Term st1
										  LEFT OUTER JOIN AMS.SR_Master sm ON st1.SR_VNo=sm.SR_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
										  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
										  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId=c.CId
										  LEFT OUTER JOIN AMS.Department d ON sm.Cls1=d.DId
										  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId=ja.AgentId
										  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id=ja1.AgentId
										  LEFT OUTER JOIN AMS.Area a ON gl.AreaId=a.AreaId";
			cmdString +=
				$@" WHERE st1.Term_Type <> 'BT' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
			cmdString += GetReports.InvoiceType.IsValueExits() && GetReports.InvoiceType.Equals("CANCEL")
				? @" AND sm.R_Invoice = 1 "
				: " AND (sm.R_Invoice=0 OR sm.R_Invoice IS NULL)  ";
			cmdString += GetReports.DepartmentId.IsValueExits()
				? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) "
				: " ";
			cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
			cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
			cmdString += GetReports.AreaId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
			cmdString += GetReports.EntryUser.IsValueExits()
				? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
				: " ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
			cmdString += GetReports.FilterValue.IsValueExits()
				? $@" AND sm.SR_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
				: " ";
			cmdString +=
				GetReports.InvoiceType.IsValueExits() &&
				("RETURN", "CANCEL").ToString().Contains(GetReports.InvoiceType)
					? $@" AND  sm.Invoice_Mode = '{GetReports.InvoiceType}' "
					: " ";
			cmdString +=
				GetReports.InvoiceCategory.IsValueExits() &&
				!("ALL", "NORMAL").ToString().Contains(GetReports.InvoiceCategory)
					? $@" AND sm.Payment_Mode in (Select Value from [AMS].[fn_SplitString]('{GetReports.InvoiceCategory}', ',')) "
					: " ";
			cmdString += $@"
				GROUP BY UPPER(st.ST_Name), sm.SR_Invoice) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.TermVoucherNo=ma.VoucherNo ";
		}

		cmdString += GetReports.RptMode switch
		{
			"VOUCHER WISE" => @"
				ORDER BY CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC), CONVERT(DATE, ma.VoucherDate,105)  asc;",
			_ => @"
				ORDER BY CONVERT(DATE, ma.VoucherDate,105)  asc, CAST(AMS.GetNumericValue(ma.VoucherNo) AS NUMERIC);"
		};
		return cmdString;
	}

	private string GetSalesReturnRegisterProductSummaryReportsScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString += $@"
			SELECT *,'' NetAmount
			FROM(SELECT sd.P_Id LedgerId, p.PName LedgerDesc, p.PShortName, pg.GrpCode, ISNULL(pg.GrpName, 'NO GROUP') GrpName, psg.ShortName SubGroupCode, ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP') SubGrpName, CAST(SUM(sd.Alt_Qty) AS DECIMAL(18, 2)) AltQty, au.UnitCode AltUom, CAST(SUM(sd.Qty) AS DECIMAL(18, 2)) Qty, pu.UnitCode Uom, FORMAT(CAST(SUM(sd.B_Amount) AS DECIMAL(18, 6)), '##,##,###.00') BasicAmount, 0 IsGroup
				 FROM AMS.SR_Details sd
					  LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=sd.SR_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<> 'Cancel' AND sm.CBranch_Id IN ({SysBranchId}) AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
				 GROUP BY ISNULL(pg.GrpName, 'NO GROUP'), ISNULL(psg.SubGrpName, 'NO SUB-SUBGROUP'), sd.P_Id, p.PName, p.PShortName, pg.GrpCode, psg.ShortName, au.UnitCode, pu.UnitCode)ma ";
		cmdString += columnName.IsValueExits()
			? $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '##,##,###.00') Amount
									 FROM AMS.SR_Term st1
										  LEFT OUTER JOIN AMS.SR_Master sm ON st1.SR_VNo=sm.SR_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
									 WHERE st1.Term_Type<>'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
									 GROUP BY UPPER(st.ST_Name), st1.Product_Id) AS d PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.ProductId=ma.LedgerId"
			: " ";
		cmdString += @"
			ORDER BY ma.LedgerDesc; ";
		return cmdString;
	}

	public DataTable GetSalesReturnRegisterSummary()
	{
		var _getQuery = GetReports.RptMode.ToUpper() switch
		{
			"PRODUCT WISE" => GetSalesReturnRegisterProductSummaryReportsScript(),
			"PRODUCT GROUP" => GetSalesReturnRegisterProductSummaryReportsScript(),
			"PRODUCT SUBGROUP WSE" => GetSalesReturnRegisterProductSummaryReportsScript(),
			_ => GetSalesReturnSummaryRegisterVoucherWise()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(_getQuery);
		if (dsRegister.Tables[0].Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsUserWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	#endregion ** ---------- SUMMARY ---------- **


	// SALES DETAILS REPORTS
	#region *||* ---------- DETAILS ---------- *||*

	// SALES QUOTATION REGISTER
	public string GetSalesQuotationRegisterDetailsReportProductWiseScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SO_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SO_Invoice ELSE sm.SO_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sd.P_Id ProductId, p.PShortName, p.PName,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltQty,au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18,{SysQtyLength})) Qty,pu.UnitCode Uom,FORMAT(CAST(sd.Rate AS DECIMAL(18, 6)) , '{SysAmountCommaFormat}') Rate, FORMAT(CAST(sd.B_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, '' NetAmount, 0 IsGroup
				 FROM AMS.SO_Details sd
					  LEFT OUTER JOIN AMS.SO_Master sm ON sm.SO_Invoice=sd.SO_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL) ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.PGroupId.IsValueExits() ? $" AND pg.PGrpId IN ({GetReports.PGroupId}) " : " ";
		cmdString += GetReports.PSubGroupId.IsValueExits() ? $" AND psg.PSubGrpId IN ({GetReports.PSubGroupId}) " : " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT sm.SO_Invoice VoucherNo, st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SO_Term st1
										  LEFT OUTER JOIN AMS.SO_Master sm ON st1.SO_VNo=sm.SO_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
										  LEFT OUTER JOIN AMS.Product p ON p.PID=st1.Product_Id
									  WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
									 GROUP BY UPPER(st.ST_Name), sm.SO_Invoice, st1.Product_Id ) AS d
								PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.VoucherNo=ma.VoucherNo AND tc.ProductId=ma.ProductId ";
		cmdString += @"
			ORDER BY ma.PName,CONVERT(DATE, ma.VoucherDate,105) ASC,AMS.GetNumericValue(ma.VoucherNo) ASC ";
		return cmdString;
	}

	public string GetSalesQuotationRegisterDetailsReportScript()
	{
		var cmdString = $@"
			SELECT sm.SO_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.Ref_Vno IS NULL THEN sm.SO_Invoice ELSE sm.SO_Invoice + ' (' + sm.Ref_Vno + ' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount, UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName,0 IsGroup
			 FROM AMS.SO_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				  LEFT OUTER JOIN(SELECT SO_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SO_Details sd GROUP BY sd.SO_Invoice) AS sd ON sd.SO_Invoice=sm.SO_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SO_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.SO_Invoice;

			SELECT sd.SO_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty,au.UnitCode AltUom, CAST( sd.Qty AS DECIMAL(18,{SysQtyLength})) StockQty, pu.UnitCode Uom, FORMAT(CAST(sd.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate, FORMAT(CAST( sd.B_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') BasicAmount,FORMAT(CAST( sd.N_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') N_Amount, sd.Narration Narration
			 FROM AMS.SO_Details sd
				  LEFT OUTER JOIN AMS.SO_Master sm ON sd.SO_Invoice = sm.SO_Invoice
				  LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id = pu.UID
				  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sd.SO_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.SO_Invoice,sd.P_Id ; ";

		cmdString += $@"
			SELECT SO_VNo VoucherNo,st.ST_Sign TermSign , st1.ST_Id TermId, UPPER (st.ST_Name) TermDesc, FORMAT(CAST(st1.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,";
		cmdString += !GetReports.IsHorizon
			? @$" FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount  "
			: $"FORMAT(CAST(st1.Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType ";
		cmdString += $@"
			FROM AMS.SO_Term st1
				  LEFT OUTER JOIN AMS.SO_Master sm ON st1.SO_VNo = sm.SO_Invoice
				  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id = st.ST_ID
			WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{Convert.ToDateTime(GetReports.FromAdDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToAdDate):yyyy-MM-dd}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND st1.SO_VNo in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += !GetReports.IsHorizon
			? @"
			GROUP BY SO_VNo,st1.ST_ID,st.ST_Name,Rate,st.Order_No,st.ST_Sign "
			: " ";
		cmdString += GetReports.IsHorizon
			? @"
			ORDER BY SO_VNo,st.Order_No,st1.Product_Id;"
			: @"ORDER BY SO_VNo,st.Order_No;";
		return cmdString;
	}

	public DataTable GetSalesQuotationRegisterDetails()
	{
		var cmdString = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GetSalesOrderRegisterDetailsReportProductWiseScript(),
			"PRODUCT GROUP" => GetSalesOrderRegisterDetailsReportProductWiseScript(),
			"PRODUCT SUBGROUP WISE" => GetSalesOrderRegisterDetailsReportProductWiseScript(),
			_ => GetSalesOrderRegisterDetailsReportScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES ORDER REGISTER
	public string GetSalesOrderRegisterDetailsReportProductWiseScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SO_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SO_Invoice ELSE sm.SO_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sd.P_Id ProductId, p.PShortName, p.PName,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltQty,au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18,{SysQtyLength})) Qty,pu.UnitCode Uom,FORMAT(CAST(sd.Rate AS DECIMAL(18, 6)) , '{SysAmountCommaFormat}') Rate, FORMAT(CAST(sd.B_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, '' NetAmount, 0 IsGroup
				 FROM AMS.SO_Details sd
					  LEFT OUTER JOIN AMS.SO_Master sm ON sm.SO_Invoice=sd.SO_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL) ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.PGroupId.IsValueExits() ? $" AND pg.PGrpId IN ({GetReports.PGroupId}) " : " ";
		cmdString += GetReports.PSubGroupId.IsValueExits() ? $" AND psg.PSubGrpId IN ({GetReports.PSubGroupId}) " : " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT sm.SO_Invoice VoucherNo, st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SO_Term st1
										  LEFT OUTER JOIN AMS.SO_Master sm ON st1.SO_VNo=sm.SO_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
										  LEFT OUTER JOIN AMS.Product p ON p.PID=st1.Product_Id
									  WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
									 GROUP BY UPPER(st.ST_Name), sm.SO_Invoice, st1.Product_Id ) AS d
								PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.VoucherNo=ma.VoucherNo AND tc.ProductId=ma.ProductId ";
		cmdString += @"
			ORDER BY ma.PName,CONVERT(DATE, ma.VoucherDate,105) ASC,AMS.GetNumericValue(ma.VoucherNo) ASC ";
		return cmdString;
	}

	public string GetSalesOrderRegisterDetailsReportScript()
	{
		var cmdString = $@"
			SELECT sm.SO_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.Ref_Vno IS NULL THEN sm.SO_Invoice ELSE sm.SO_Invoice + ' (' + sm.Ref_Vno + ' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount, UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName,0 IsGroup
			 FROM AMS.SO_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				  LEFT OUTER JOIN(SELECT SO_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SO_Details sd GROUP BY sd.SO_Invoice) AS sd ON sd.SO_Invoice=sm.SO_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SO_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.SO_Invoice;

			SELECT sd.SO_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty,au.UnitCode AltUom, CAST( sd.Qty AS DECIMAL(18,{SysQtyLength})) StockQty, pu.UnitCode Uom, FORMAT(CAST(sd.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate, FORMAT(CAST( sd.B_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') BasicAmount,FORMAT(CAST( sd.N_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') N_Amount, sd.Narration Narration
			 FROM AMS.SO_Details sd
				  LEFT OUTER JOIN AMS.SO_Master sm ON sd.SO_Invoice = sm.SO_Invoice
				  LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id = pu.UID
				  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sd.SO_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.SO_Invoice,sd.P_Id ; ";

		cmdString += $@"
			SELECT SO_VNo VoucherNo,st.ST_Sign TermSign , st1.ST_Id TermId, UPPER (st.ST_Name) TermDesc, FORMAT(CAST(st1.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,";
		cmdString += !GetReports.IsHorizon
			? @$" FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount  "
			: $"FORMAT(CAST(st1.Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType ";
		cmdString += $@"
			FROM AMS.SO_Term st1
				  LEFT OUTER JOIN AMS.SO_Master sm ON st1.SO_VNo = sm.SO_Invoice
				  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id = st.ST_ID
			WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{Convert.ToDateTime(GetReports.FromAdDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToAdDate):yyyy-MM-dd}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND st1.SO_VNo in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += !GetReports.IsHorizon
			? @"
			GROUP BY SO_VNo,st1.ST_ID,st.ST_Name,Rate,st.Order_No,st.ST_Sign "
			: " ";
		cmdString += GetReports.IsHorizon
			? @"
			ORDER BY SO_VNo,st.Order_No,st1.Product_Id;"
			: @"ORDER BY SO_VNo,st.Order_No;";
		return cmdString;
	}

	public DataTable GetSalesOrderRegisterDetails()
	{
		var cmdString = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GetSalesOrderRegisterDetailsReportProductWiseScript(),
			"PRODUCT GROUP" => GetSalesOrderRegisterDetailsReportProductWiseScript(),
			"PRODUCT SUBGROUP WISE" => GetSalesOrderRegisterDetailsReportProductWiseScript(),
			_ => GetSalesOrderRegisterDetailsReportScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterSummaryReportsDateWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES CHALLAN REGISTER
	public string GetSalesChallanRegisterDetailsReportProductWiseScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SC_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SC_Invoice ELSE sm.SC_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sd.P_Id ProductId, p.PShortName, p.PName,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltQty,au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18,{SysQtyLength})) Qty,pu.UnitCode Uom,FORMAT(CAST(sd.Rate AS DECIMAL(18, 6)) , '{SysAmountCommaFormat}') Rate, FORMAT(CAST(sd.B_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, '' NetAmount, 0 IsGroup
				 FROM AMS.SC_Details sd
					  LEFT OUTER JOIN AMS.SC_Master sm ON sm.SC_Invoice=sd.SC_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL) ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.PGroupId.IsValueExits() ? $" AND pg.PGrpId IN ({GetReports.PGroupId}) " : " ";
		cmdString += GetReports.PSubGroupId.IsValueExits() ? $" AND psg.PSubGrpId IN ({GetReports.PSubGroupId}) " : " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT sm.SC_Invoice VoucherNo, st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SC_Term st1
										  LEFT OUTER JOIN AMS.SC_Master sm ON st1.SC_VNo=sm.SC_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
										  LEFT OUTER JOIN AMS.Product p ON p.PID=st1.Product_Id
									  WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
									 GROUP BY UPPER(st.ST_Name), sm.SC_Invoice, st1.Product_Id ) AS d
								PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.VoucherNo=ma.VoucherNo AND tc.ProductId=ma.ProductId ";
		cmdString += @"
			ORDER BY ma.PName,CONVERT(DATE, ma.VoucherDate,105) ASC,AMS.GetNumericValue(ma.VoucherNo) ASC ";
		return cmdString;
	}

	public string GetSalesChallanRegisterDetailsReportScript()
	{
		var cmdString = $@"
			SELECT sm.SC_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SC_Invoice ELSE sm.SC_Invoice + ' (' + sm.PB_Vno + ' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount, UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName,0 IsGroup
			 FROM AMS.SC_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				  LEFT OUTER JOIN(SELECT SC_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SC_Details sd GROUP BY sd.SC_Invoice) AS sd ON sd.SC_Invoice=sm.SC_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SC_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.SC_Invoice;

			SELECT sd.SC_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty,au.UnitCode AltUom, CAST( sd.Qty AS DECIMAL(18,{SysQtyLength})) StockQty, pu.UnitCode Uom, FORMAT(CAST(sd.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate, FORMAT(CAST( sd.B_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') BasicAmount,FORMAT(CAST( sd.N_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') N_Amount, sd.Narration Narration
			 FROM AMS.SC_Details sd
				  LEFT OUTER JOIN AMS.SC_Master sm ON sd.SC_Invoice = sm.SC_Invoice
				  LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id = pu.UID
				  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sd.SC_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.SC_Invoice,sd.P_Id ; ";

		cmdString += $@"
			SELECT SC_VNo VoucherNo,st.ST_Sign TermSign , st1.ST_Id TermId, UPPER (st.ST_Name) TermDesc, FORMAT(CAST(st1.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,";
		cmdString += !GetReports.IsHorizon
			? @$" FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount  "
			: $"FORMAT(CAST(st1.Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType ";
		cmdString += $@"
			FROM AMS.SC_Term st1
				  LEFT OUTER JOIN AMS.SC_Master sm ON st1.SC_VNo = sm.SC_Invoice
				  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id = st.ST_ID
			WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{Convert.ToDateTime(GetReports.FromAdDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToAdDate):yyyy-MM-dd}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND st1.SC_VNo in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += !GetReports.IsHorizon
			? @"
			GROUP BY SC_VNo,st1.ST_ID,st.ST_Name,Rate,st.Order_No,st.ST_Sign "
			: " ";
		cmdString += GetReports.IsHorizon
			? @"
			ORDER BY SC_VNo,st.Order_No,st1.Product_Id;"
			: @"ORDER BY SC_VNo,st.Order_No;";
		return cmdString;
	}

	public DataTable GetSalesChallanRegisterDetails()
	{
		var cmdString = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GetSalesChallanRegisterDetailsReportProductWiseScript(),
			"PRODUCT GROUP" => GetSalesChallanRegisterDetailsReportProductWiseScript(),
			"PRODUCT SUBGROUP WISE" => GetSalesChallanRegisterDetailsReportProductWiseScript(),
			_ => GetSalesChallanRegisterDetailsReportScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES INVOICE REGISTER
	public string GetSalesInvoiceRegisterDetailsReportProductWiseScript()
	{
		var cmdString = string.Empty;
		cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SB_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sd.P_Id ProductId, p.PShortName, p.PName,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltQty,au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18,{SysQtyLength})) Qty,pu.UnitCode Uom,FORMAT(CAST(sd.Rate AS DECIMAL(18, 6)) , '{SysAmountCommaFormat}') Rate, FORMAT(CAST(sd.B_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount, '' NetAmount, 0 IsGroup
				 FROM AMS.SB_Details sd
					  LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL) ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.PGroupId.IsValueExits() ? $" AND pg.PGrpId IN ({GetReports.PGroupId}) " : " ";
		cmdString += GetReports.PSubGroupId.IsValueExits() ? $" AND psg.PSubGrpId IN ({GetReports.PSubGroupId}) " : " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
			cmdString += $@"
				LEFT OUTER JOIN(SELECT *
								FROM(SELECT sm.SB_Invoice VoucherNo, st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount
									 FROM AMS.SB_Term st1
										  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
										  LEFT OUTER JOIN AMS.Product p ON p.PID=st1.Product_Id
									  WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
									 GROUP BY UPPER(st.ST_Name), sm.SB_Invoice, st1.Product_Id ) AS d
								PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.VoucherNo=ma.VoucherNo AND tc.ProductId=ma.ProductId ";
		cmdString += @"
			ORDER BY ma.PName,CONVERT(DATE, ma.VoucherDate,105) ASC,AMS.GetNumericValue(ma.VoucherNo) ASC ";
		return cmdString;
	}

	public string GetSalesInvoiceRegisterDetailsReportScript()
	{
		var cmdString = @"
			SELECT sm.SB_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice + ' (' + sm.PB_Vno + ' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,";
		cmdString += ServerVersion < 10
			? $@" CAST(sm.LN_Amount AS DECIMAL(18,{SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount,";
		cmdString +=
			$@" UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName,0 IsGroup
			 FROM AMS.SB_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				  LEFT OUTER JOIN(SELECT SB_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SB_Details sd GROUP BY sd.SB_Invoice) AS sd ON sd.SB_Invoice=sm.SB_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.SB_Invoice;

			SELECT sd.SB_Invoice VoucherNo,CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty,au.UnitCode AltUom, CAST( sd.Qty AS DECIMAL(18,{SysQtyLength})) StockQty, pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(sd.Rate AS DECIMAL(18,{SysRateFormat})) Rate, CAST( sd.B_Amount AS DECIMAL(18,{SysAmountLength}))  BasicAmount,CAST( sd.N_Amount AS DECIMAL(18,{SysAmountLength})) N_Amount,"
			: $@" FORMAT(CAST(sd.Rate AS DECIMAL(18,{SysAmountLength})),'{SysAmountCommaFormat}') Rate, FORMAT(CAST( sd.B_Amount AS DECIMAL(18,{SysAmountLength})),'{SysAmountCommaFormat}') BasicAmount,FORMAT(CAST( sd.N_Amount AS DECIMAL(18,{SysAmountLength})),'{SysAmountCommaFormat}') N_Amount,";
		cmdString +=
			$@" sd.Narration Narration,ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName
			 FROM AMS.SB_Details sd
				  LEFT OUTER JOIN AMS.SB_Master sm ON sd.SB_Invoice = sm.SB_Invoice
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				  LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id = pu.UID
				  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.SB_Invoice,sd.P_Id ; ";

		cmdString += @"
			SELECT SB_VNo VoucherNo,st.ST_Sign TermSign , st1.ST_Id TermId, UPPER (st.ST_Name) TermDesc,";
		if (!GetReports.IsHorizon)
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(st1.Amount) AS DECIMAL(18,{SysAmountLength})) Amount "
				: $@" FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount  ";
		else
			cmdString += ServerVersion < 10
				? $@" CAST(st1.Amount AS DECIMAL(18,{SysAmountLength}))  Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType "
				: $@" FORMAT(CAST(st1.Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}')  Amount, st1.Product_Id ProductId, st.ST_Sign TermSign, st1.Term_Type TermType ";
		cmdString += $@"
			FROM AMS.SB_Term st1
				  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo = sm.SB_Invoice
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id = st.ST_ID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{Convert.ToDateTime(GetReports.FromAdDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToAdDate):yyyy-MM-dd}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SB_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += !GetReports.IsHorizon
			? @"
			GROUP BY SB_VNo,st1.ST_ID,st.ST_Name,st.Order_No,st.ST_Sign,sm.Party_Name,gl.GLName,ISNULL (c.CCode, 'NO-COUNTER' ),sm.Cls1, ISNULL ( d.DName, 'NO-DEPARTMENT' ), gl.AgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ), sm.Agent_Id, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ), gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' )  "
			: " ";
		cmdString += GetReports.IsHorizon switch
		{
			true => @"
				ORDER BY SB_VNo,st.Order_No,st1.Product_Id;",
			_ => @"
				ORDER BY SB_VNo,st.Order_No;"
		};
		return cmdString;
	}

	public DataTable GetSalesInvoiceRegisterDetails()
	{
		var cmdString = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GetSalesInvoiceRegisterDetailsReportProductWiseScript(),
			"PRODUCT GROUP" => GetSalesInvoiceRegisterDetailsReportProductWiseScript(),
			"PRODUCT SUBGROUP WISE" => GetSalesInvoiceRegisterDetailsReportProductWiseScript(),
			_ => GetSalesInvoiceRegisterDetailsReportScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterDetailsReportsLedgerWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterDetailsReportProductWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportProductGroupWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportProductSubGroupWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	// SALES RETURN REGISTER
	public string GetSalesReturnRegisterDetailsReportProductWiseScript()
	{
		var cmdString = @"
			DECLARE @terms AS NVARCHAR(MAX), @query AS NVARCHAR(MAX)
			SET @terms = (SELECT SUBSTRING((SELECT ', [' + ST_Name + ']' FROM AMS.ST_Term WHERE ST_Condition IN ('P','B') AND ST_Type = 'G' ORDER BY CONVERT(INT, Order_No) FOR XML PATH ('')), 3, 200000) AS Terms)
			SELECT @terms";
		var columnName = GetConnection.GetQueryData(cmdString);

		cmdString = $@"
			SELECT *
			FROM(SELECT sm.SR_Invoice VoucherNo, CONVERT(NVARCHAR, sm.Invoice_Date, 103) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SR_Invoice ELSE sm.SR_Invoice+' ('+sm.PB_Vno+' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name<>'' THEN gl.GLName+' ('+sm.Party_Name+' )' ELSE gl.GLName END LedgerDesc, sd.P_Id ProductId, p.PShortName, p.PName,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltQty,au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18,{SysQtyLength})) Qty,pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(sd.Rate AS DECIMAL(18, {SysAmountLength})) Rate, CAST(sd.B_Amount AS DECIMAL(18, {SysAmountLength})) BasicAmount,"
			: $@" FORMAT(CAST(sd.Rate AS DECIMAL(18, 6)) , '{SysAmountCommaFormat}') Rate, FORMAT(CAST(sd.B_Amount AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') BasicAmount,";
		cmdString = $@" '' NetAmount, 0 IsGroup
				 FROM AMS.SR_Details sd
					  LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=sd.SR_Invoice
					  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id=gl.GLID
					  LEFT OUTER JOIN AMS.Product p ON p.PID=sd.P_Id
					  LEFT OUTER JOIN AMS.ProductUnit pu ON pu.UID=p.PUnit
					  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
					  LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
					  LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
				 WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL) ";
		cmdString += GetReports.ProductId.IsValueExits() ? $" AND sd.P_Id IN ({GetReports.ProductId}) " : " ";
		cmdString += GetReports.PGroupId.IsValueExits() ? $" AND pg.PGrpId IN ({GetReports.PGroupId}) " : " ";
		cmdString += GetReports.PSubGroupId.IsValueExits() ? $" AND psg.PSubGrpId IN ({GetReports.PSubGroupId}) " : " ";
		cmdString += @"
				) AS ma ";
		if (columnName.IsValueExits())
		{
			cmdString += @"
				LEFT OUTER JOIN(SELECT * FROM(SELECT sm.SR_Invoice VoucherNo, st1.Product_Id ProductId, UPPER(st.ST_Name) TermDesc, ";
			cmdString += ServerVersion < 10
				? $@"  CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, {SysAmountLength})) Amount "
				: $@"  FORMAT(CAST(SUM(ISNULL(st1.Amount, 0)) AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Amount ";
			cmdString += $@"
									 FROM AMS.SR_Term st1
										  LEFT OUTER JOIN AMS.SR_Master sm ON st1.SR_VNo=sm.SR_Invoice
										  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
										  LEFT OUTER JOIN AMS.Product p ON p.PID=st1.Product_Id
									  WHERE st1.Term_Type <> 'B' AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type<>'Cancel' AND sm.CBranch_Id IN ({SysBranchId})AND sm.FiscalYearId IN ({SysFiscalYearId})AND(sm.CUnit_Id='{SysCompanyUnitId}' OR CUnit_Id IS NULL)AND(sm.R_Invoice=0 OR sm.R_Invoice IS NULL)
									 GROUP BY UPPER(st.ST_Name), sm.SR_Invoice, st1.Product_Id ) AS d
								PIVOT(MAX(Amount) FOR TermDesc IN({columnName})) AS pid) AS tc ON tc.VoucherNo=ma.VoucherNo AND tc.ProductId=ma.ProductId ";
		}

		cmdString += @"
			ORDER BY ma.PName,CONVERT(DATE, ma.VoucherDate,105) ASC,AMS.GetNumericValue(ma.VoucherNo) ASC ";
		return cmdString;
	}

	public string GetSalesReturnRegisterDetailsReportScript()
	{
		var cmdString = @"
			SELECT sm.SR_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.SB_Invoice IS NULL THEN sm.SR_Invoice ELSE sm.SR_Invoice + ' (' + sm.SB_Invoice + ' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode,'' AltQty,'' AltUom,'' Qty,'' Uom,'' Rate, '' BasicAmount,";
		cmdString += ServerVersion < 10
			? $@" CAST(sm.LN_Amount AS DECIMAL(18,{SysAmountLength})) NetAmount,"
			: $@" FORMAT(CAST(sm.LN_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') NetAmount,";

		cmdString +=
			$@" UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName,0 IsGroup
			 FROM AMS.SR_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
				  LEFT OUTER JOIN(SELECT SR_Invoice, SUM(B_Amount) BasicAmount FROM AMS.SR_Details sd GROUP BY sd.SR_Invoice) AS sd ON sd.SR_Invoice=sm.SR_Invoice
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sm.SR_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.SR_Invoice;

			SELECT sd.SR_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltStockQty,au.UnitCode AltUom, CAST( sd.Qty AS DECIMAL(18,{SysQtyLength})) StockQty, pu.UnitCode Uom,";
		cmdString += ServerVersion < 10
			? $@" CAST(sd.Rate AS DECIMAL(18,{SysAmountLength})) Rate, CAST( sd.B_Amount AS DECIMAL(18,{SysAmountLength})) BasicAmount,CAST( sd.N_Amount AS DECIMAL(18,{SysAmountLength})) N_Amount,"
			: $@" FORMAT(CAST(sd.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate, FORMAT(CAST( sd.B_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') BasicAmount,FORMAT(CAST( sd.N_Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}') N_Amount,";
		cmdString += $@" sd.Narration Narration
			 FROM AMS.SR_Details sd
				  LEFT OUTER JOIN AMS.SR_Master sm ON sd.SR_Invoice = sm.SR_Invoice
				  LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id = pu.UID
				  LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND sd.SR_Invoice in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += @"
			ORDER BY sm.Invoice_Date,sm.SR_Invoice,sd.P_Id ; ";

		cmdString += $@"
			SELECT SR_VNo VoucherNo,st.ST_Sign TermSign , st1.ST_Id TermId, UPPER (st.ST_Name) TermDesc, FORMAT(CAST(st1.Rate AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Rate,";
		if (!GetReports.IsHorizon)
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(st1.Amount) AS DECIMAL(18,{SysAmountLength}))  Amount  "
				: $@" FORMAT(CAST(SUM(st1.Amount) AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount  ";
		else
			cmdString += ServerVersion < 10
				? $"CAST(st1.Amount AS DECIMAL(18,{SysAmountLength})) Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType "
				: $"FORMAT(CAST(st1.Amount AS DECIMAL(18,6)),'{SysAmountCommaFormat}')  Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType ";

		cmdString += $@"
			FROM AMS.SR_Term st1
				  LEFT OUTER JOIN AMS.SR_Master sm ON st1.SR_VNo = sm.SR_Invoice
				  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id = st.ST_ID
			WHERE st1.Term_Type <> 'BT' and sm.Invoice_Date BETWEEN '{Convert.ToDateTime(GetReports.FromAdDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToAdDate):yyyy-MM-dd}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL) ";
		cmdString += GetReports.DepartmentId.IsValueExits() ? $@" AND sm.Cls1 in ({GetReports.DepartmentId}) " : " ";
		cmdString += GetReports.CounterId.IsValueExits() ? $@" AND sm.CounterId in ({GetReports.CounterId}) " : " ";
		cmdString += GetReports.AreaId.IsValueExits() ? $@" AND gl.AreaId in ({GetReports.AreaId}) " : " ";
		cmdString += GetReports.AgentId.IsValueExits() ? $@" AND gl.AgentId in ({GetReports.AgentId}) " : " ";
		cmdString += GetReports.DocAgentId.IsValueExits() ? $@" AND sm.Agent_ID in ({GetReports.DocAgentId}) " : " ";
		cmdString += GetReports.EntryUser.IsValueExits()
			? $@" AND sm.Enter_By in (Select Value from [AMS].[fn_Splitstring]('{GetReports.EntryUser}', ',')) "
			: " ";
		cmdString += GetReports.LedgerId.IsValueExits() ? $@" AND sm.Customer_Id IN ({GetReports.LedgerId}) " : " ";
		cmdString += GetReports.FilterValue.IsValueExits()
			? $@" AND st1.SR_VNo in (Select Value from [AMS].[fn_Splitstring]('{GetReports.FilterValue}', ',')) "
			: " ";
		cmdString += !GetReports.IsHorizon
			? @"
			GROUP BY SR_VNo,st1.ST_ID,st.ST_Name,Rate,st.Order_No,st.ST_Sign "
			: " ";
		cmdString += GetReports.IsHorizon
			? @"
			ORDER BY SR_VNo,st.Order_No,st1.Product_Id;"
			: @"ORDER BY SR_VNo,st.Order_No;";
		return cmdString;
	}

	public DataTable GetSalesReturnRegisterDetails()
	{
		var cmdString = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GetSalesReturnRegisterDetailsReportProductWiseScript(),
			"PRODUCT GROUP" => GetSalesReturnRegisterDetailsReportProductWiseScript(),
			"PRODUCT SUBGROUP WISE" => GetSalesReturnRegisterDetailsReportProductWiseScript(),
			_ => GetSalesReturnRegisterDetailsReportScript()
		};
		var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
		if (dsRegister.Tables.Count <= 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"CUSTOMER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"VOUCHER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"USER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"MAIN AGENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"MAIN AREA WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"COUNTER WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"DEPARTMENT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT GROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			"PRODUCT SUBGROUP WISE" => ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(dsRegister),
			_ => new DataTable()
		};
		return dtReports;
	}

	#endregion *||* ---------- DETAILS ---------- *||*


	// DETAILS REPORT
	#region *||* ---------- DETAILS METHOD ---------- *||*

	private DataTable ReturnSalesPurchaseRegisterDetailsReportsVoucherWise(DataSet dsReport, bool isPurchase = false)
	{
		DataRow newRow;
		var dtReport = new DataTable();
		if (dsReport.Tables.Count > 0)
		{
			dtReport = dsReport.Tables[0].Copy();
			foreach (DataRow dataRow in dsReport.Tables[0].Rows)
			{
				var voucherNo = dataRow["VoucherNo"].GetString();
				var result = dtReport.Select($"VoucherNo = '{voucherNo}'");
				if (result.Length == 0) continue;
				var index = dtReport.Rows.IndexOf(result[0]);
				var dtDetails = dsReport.Tables[1].Select($"VoucherNo='{voucherNo}'");
				if (dtDetails != null && dtDetails.Length > 0)
				{
					var invoiceMode = dataRow["Invoice_Mode"].GetUpper();
					foreach (var row in dtDetails)
					{
						newRow = dtReport.NewRow();
						newRow["VoucherNoWithRef"] = row["ShortName"];
						newRow["LedgerDesc"] = row["ProductDesc"];
						newRow["Qty"] = row["StockQty"].GetDecimalComma();
						newRow["Uom"] = row["Uom"];
						var rate = invoiceMode is "POS" or "RSB"
							? row["BasicAmount"].GetDecimal() / row["StockQty"].GetDecimal()
							: 0;
						newRow["Rate"] = rate > 0 ? rate.GetRateComma() : row["Rate"].GetRateComma();
						newRow["BasicAmount"] = row["BasicAmount"].GetDecimalComma();
						newRow["IsGroup"] = 0;
						dtReport.Rows.InsertAt(newRow, index + 1);
						index++;
					}
				}

				newRow = dtReport.NewRow();
				newRow["LedgerDesc"] = "[BASIC AMOUNT]";
				newRow["Qty"] = dtDetails.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
				var basicAmount = dtDetails.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
				newRow["BasicAmount"] = basicAmount.GetDecimalComma();
				newRow["IsGroup"] = 11;
				dtReport.Rows.InsertAt(newRow, index + 1);
				index++;

				if (dsReport.Tables.Count > 2)
				{
					var dtTerm = dsReport.Tables[2].Select($"VoucherNo='{voucherNo}'");
					if (dtTerm.Length > 0)
					{
						var taxable = basicAmount;
						foreach (var row in dtTerm)
						{
							var vatId = isPurchase ? PurchaseVatTermId : SalesVatTermId;
							var IsTax = row["TermId"].GetInt() == vatId;
							var amount = row["Amount"].GetDecimal();
							if (IsTax)
							{
								newRow = dtReport.NewRow();
								newRow["LedgerDesc"] = "[TAXABLE AMOUNT]";
								var returnTaxable = ("POS", "RSB").Equals(dataRow["Invoice_Mode"].ToString())
									? amount / 0.13.GetDecimal()
									: taxable;

								newRow["BasicAmount"] = returnTaxable.GetDecimalComma();
								newRow["IsGroup"] = 13;
								dtReport.Rows.InsertAt(newRow, index + 1);
								index++;
							}

							taxable += row["TermSign"].Equals("-") ? -amount : +amount;
							newRow = dtReport.NewRow();
							newRow["LedgerDesc"] = row["TermDesc"];
							newRow["BasicAmount"] = row["Amount"];
							newRow["IsGroup"] = 13;
							dtReport.Rows.InsertAt(newRow, index + 1);
							index++;
						}
					}

					newRow = dtReport.NewRow();
					newRow["LedgerDesc"] = "[NET AMOUNT]";
					newRow["BasicAmount"] = dataRow["NetAmount"].GetDecimalComma();
					newRow["IsGroup"] = 99;
					dtReport.Rows.InsertAt(newRow, index + 1);
					index++;
				}
			}

			var dtTotal = dtReport.Select("IsGroup = 99").CopyToDataTable();
			newRow = dtReport.NewRow();
			newRow["LedgerDesc"] = "[NET PURCHASE AMOUNT]";
			newRow["BasicAmount"] = dtTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal()).GetDecimalComma();
			newRow["IsGroup"] = 99;
			dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		}

		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterDetailsReportsLedgerWise(DataSet dsReport, bool isPurchase = false)
	{
		DataRow newRow;
		var dtReport = new DataTable();
		if (dsReport.Tables.Count > 0)
		{
			dtReport = dsReport.Tables[0].Clone();

			var ledgerInfo = dsReport.Tables[0].AsEnumerable().GroupBy(r => new
			{
				voucherNo = r.Field<string>("LedgerDesc")
			}).Select(g => g.First()).CopyToDataTable();

			foreach (DataRow infoRow in ledgerInfo.Rows)
			{
				newRow = dtReport.NewRow();
				newRow["LedgerDesc"] = infoRow["LedgerDesc"];
				newRow["IsGroup"] = 0;
				dtReport.Rows.InsertAt(newRow, dtReport.RowsCount() + 1);

				var table = dsReport.Tables[0].Select($"LedgerDesc = '{infoRow["LedgerDesc"]}'").CopyToDataTable();

				table.AsEnumerable().Take(table.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

				foreach (DataRow dataRow in table.Rows)
				{
					var voucherNo = dataRow["VoucherNo"].GetString();
					var result = dtReport.Select($"VoucherNo = '{voucherNo}'");
					var index = dtReport.Rows.IndexOf(result[0]);
					var dtDetails = dsReport.Tables[1].Select($"VoucherNo='{voucherNo}'");
					if (dtDetails != null && dtDetails.Length > 0)
                    {
                        foreach (var row in dtDetails)
                        {
                            newRow = dtReport.NewRow();
                            newRow["VoucherNoWithRef"] = row["ShortName"];
                            newRow["LedgerDesc"] = row["ProductDesc"];
                            newRow["Qty"] = row["StockQty"];
                            newRow["Uom"] = row["Uom"];
                            newRow["Rate"] = row.Equals("POS")
                                ? (row["BasicAmount"].GetDecimal() / row["StockQty"].GetDecimal()).GetDecimalComma()
                                : row["Rate"];
                            newRow["BasicAmount"] = row["BasicAmount"];
                            newRow["IsGroup"] = 0;
                            dtReport.Rows.InsertAt(newRow, index + 1);
                            index++;
                        }
                    }

                    newRow = dtReport.NewRow();
					newRow["LedgerDesc"] = "[BASIC AMOUNT]";
					newRow["Qty"] = dtDetails.AsEnumerable().Sum(x => x["StockQty"].GetDecimal()).GetDecimalComma();
					newRow["BasicAmount"] = dtDetails.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal()).GetDecimalComma();
					newRow["IsGroup"] = 11;
					dtReport.Rows.InsertAt(newRow, index + 1);
					index++;

					if (dsReport.Tables.Count > 2)
					{
						var dtTerm = dsReport.Tables[2].Select($"VoucherNo='{voucherNo}'");
						var taxable = dataRow["NetAmount"].GetDecimal();
						foreach (var row in dtTerm)
						{
							var vatId = isPurchase ? PurchaseVatTermId : SalesVatTermId;
							var IsTax = row["TermId"].GetInt() == vatId;
							var amount = row["Amount"].GetDecimal();
							if (IsTax)
							{
								newRow = dtReport.NewRow();
								newRow["LedgerDesc"] = "[TAXABLE AMOUNT]";
								newRow["BasicAmount"] =
									(row["Amount"].GetDecimal() / 0.13.GetDecimal()).GetDecimalComma();
								newRow["IsGroup"] = 13;
								dtReport.Rows.InsertAt(newRow, index + 1);
								index++;
							}

							newRow = dtReport.NewRow();
							newRow["LedgerDesc"] = row["TermDesc"];
							newRow["Rate"] = row["Rate"];
							newRow["BasicAmount"] = row["Amount"];
							newRow["IsGroup"] = 13;
							dtReport.Rows.InsertAt(newRow, index + 1);
							index++;
						}

						newRow = dtReport.NewRow();
						newRow["LedgerDesc"] = "[NET AMOUNT]";
						newRow["BasicAmount"] = dataRow["NetAmount"];
						newRow["IsGroup"] = 99;
						dtReport.Rows.InsertAt(newRow, index + 1);
						index++;
					}
				}

				newRow = dtReport.NewRow();
				newRow["LedgerDesc"] = $"[{infoRow["LedgerDesc"]}] [LEDGER NET SALES AMOUNT]";
				newRow["BasicAmount"] = table.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
				newRow["IsGroup"] = 99;
				dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
			}

			var dtTotal = dtReport.Select("IsGroup = 99").CopyToDataTable();
			newRow = dtReport.NewRow();
			newRow["LedgerDesc"] = "[NET PURCHASE AMOUNT]";
			newRow["BasicAmount"] = dtTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			newRow["IsGroup"] = 99;
			dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		}

		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterDetailsReportProductWise(DataSet dsReport, bool isPurchase = false)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsReport.Tables[0];
		var dtMerge = new DataTable();
		var ledgerInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("PName")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in ledgerInfo.Rows)
		{
			var master = dtMaster.Select($"PName='{row["PName"]}'").CopyToDataTable();
			if (dtMerge.Columns.Count == 0) dtMerge = master.Clone();
			if (termColumn != null && termColumn.Rows.Count > 0)
				foreach (DataRow details in master.Rows)
				{
					var basicAmount = details["BasicAmount"].GetDecimal();
					foreach (DataRow term in termColumn.Rows)
						if (term["TermSign"].Equals("+"))
							basicAmount += details[$"{term["TermDesc"]}"].GetDecimal();
						else if (term["TermSign"].Equals("-"))
							basicAmount -= details[$"{term["TermDesc"]}"].GetDecimal();
					details.SetField("NetAmount", basicAmount.GetDecimalComma());
				}

			newRow = dtMerge.NewRow();
			newRow["VoucherNoWithRef"] = row["PShortName"];
			newRow["LedgerId"] = row["ProductId"];
			newRow["LedgerDesc"] = row["PName"];
			newRow["IsGroup"] = 1;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

			var userTotal = dtMerge.Select($"PName='{row["PName"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["PName"].GetTrimReplace()}] TOTAL AMOUNT =>";
			newRow["BasicAmount"] = userTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			if (termColumn != null && termColumn.Rows.Count > 0)
			{
				foreach (DataRow drTRow in termColumn.Rows)
					newRow[drTRow["TermDesc"].ToString()] = userTotal.AsEnumerable()
						.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
				newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			}

			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] =
					dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		return dtMerge;
	}

	private DataTable ReturnSalesPurchaseRegisterDetailsReportProductGroupWise(DataSet dsReport, bool isPurchase = false)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsReport.Tables[0];
		var dtMerge = new DataTable();
		var ledgerInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("LedgerDesc")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in ledgerInfo.Rows)
		{
			var master = dtMaster.Select($"LedgerDesc='{row["LedgerDesc"]}'").CopyToDataTable();

			if (termColumn != null && termColumn.Rows.Count > 0)
				foreach (DataRow details in master.Rows)
				{
					var basicAmount = details["BasicAmount"].GetDecimal();
					foreach (DataRow term in termColumn.Rows)
						if (term["TermSign"].Equals("+"))
							basicAmount += details[$"{term["TermDesc"]}"].GetDecimal();
						else if (term["TermSign"].Equals("-"))
							basicAmount -= details[$"{term["TermDesc"]}"].GetDecimal();

					details.SetField("NetAmount", basicAmount.GetDecimalComma());
				}

			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = row["LedgerDesc"];
			newRow["IsGroup"] = 1;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

			var userTotal = dtMerge.Select($"LedgerDesc='{row["LedgerDesc"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["LedgerDesc"].GetTrimReplace()}] TOTAL AMOUNT =>";
			newRow["BasicAmount"] = userTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			if (termColumn != null && termColumn.Rows.Count > 0)
			{
				foreach (DataRow drTRow in termColumn.Rows)
					newRow[drTRow["TermDesc"].ToString()] = userTotal.AsEnumerable()
						.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());

				newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			}

			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] = dtNGrand.AsEnumerable()
					.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());

			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		var dtReport = dtMerge.Clone();
		foreach (DataColumn col in dtReport.Columns) col.DataType = typeof(string);

		dtMerge.AsEnumerable().Take(dtMerge.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterDetailsReportProductSubGroupWise(DataSet dsReport, bool isPurchase = false)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsReport.Tables[0];
		var dtMerge = new DataTable();
		var ledgerInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("LedgerDesc")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in ledgerInfo.Rows)
		{
			var master = dtMaster.Select($"LedgerDesc='{row["LedgerDesc"]}'").CopyToDataTable();

			if (termColumn != null && termColumn.Rows.Count > 0)
				foreach (DataRow details in master.Rows)
				{
					var basicAmount = details["BasicAmount"].GetDecimal();
					foreach (DataRow term in termColumn.Rows)
						if (term["TermSign"].Equals("+"))
							basicAmount += details[$"{term["TermDesc"]}"].GetDecimal();
						else if (term["TermSign"].Equals("-"))
							basicAmount -= details[$"{term["TermDesc"]}"].GetDecimal();
					details.SetField("NetAmount", basicAmount.GetDecimalComma());
				}

			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = row["LedgerDesc"];
			newRow["IsGroup"] = 1;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

			var userTotal = dtMerge.Select($"LedgerDesc='{row["LedgerDesc"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["LedgerDesc"].GetTrimReplace()}] TOTAL AMOUNT =>";
			newRow["BasicAmount"] = userTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			if (termColumn != null && termColumn.Rows.Count > 0)
			{
				foreach (DataRow drTRow in termColumn.Rows)
					newRow[drTRow["TermDesc"].ToString()] = userTotal.AsEnumerable()
						.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
				newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			}

			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
			{
				newRow[drTRow["TermDesc"].ToString()] = dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			}
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		var dtReport = dtMerge.Clone();
		foreach (DataColumn col in dtReport.Columns) col.DataType = typeof(string);
		dtMerge.AsEnumerable().Take(dtMerge.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
		return dtReport;
	}

	#endregion *||* ---------- DETAILS METHOD ---------- *||*


	// SUMMARY REPORT
	#region *||* ---------- SUMMARY METHOD ---------- *||*

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsDateWise(DataSet dsRegister, bool isPurchase = false)
	{
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtReport = dsRegister.Tables[0];
		var dtNGrand = dtReport.Select("IsGroup = 0").CopyToDataTable();
		var newRow = dtReport.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		if (ServerVersion < 10)
			newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		else
			newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal()).GetDecimalComma();

		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			if (dtNGrand.Rows.Count > 0)
				foreach (DataRow drTRow in termColumn.Rows)
					if (ServerVersion < 10)
						newRow[drTRow["TermDesc"].ToString()] = dtNGrand.AsEnumerable()
							.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
					else
						newRow[drTRow["TermDesc"].ToString()] = dtNGrand.AsEnumerable()
							.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal()).GetDecimalComma();
			if (ServerVersion < 10)
				newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			else
				newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal()).GetDecimalComma();
		}

		newRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsUserWise(DataSet dsRegister, bool isPurchase = false)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsRegister.Tables[0];
		var dtMerge = new DataTable();
		var dtUserInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("EnterBy")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in dtUserInfo.Rows)
		{
			var master = dtMaster.Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();

			if (dsRegister.Tables.Count > 1)
			{
				var dtTerm = dsRegister.Tables[1].Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();
				master.PrimaryKey = new[]
				{
					master.Columns["VoucherNo"]
				};
				dtTerm.PrimaryKey = new[]
				{
					dtTerm.Columns["VoucherNo"]
				};
				master.Merge(dtTerm);
			}

			if (dtMerge.Columns.Count == 0)
			{
				dtMerge = master.Clone();
				dtMerge.PrimaryKey = null;
				dtMerge.Columns[0].AllowDBNull = true;
			}

			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = row["EnterBy"];
			newRow["IsGroup"] = 1;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

			var userTotal = dtMerge.Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["EnterBy"].GetTrimReplace()}] USER TOTAL AMOUNT =>";
			newRow["BasicAmount"] = userTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			if (termColumn != null && termColumn.Rows.Count > 0)
			{
				foreach (DataRow drTRow in termColumn.Rows)
					newRow[drTRow["TermDesc"].ToString()] = userTotal.AsEnumerable()
						.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
				newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			}

			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] =
					dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		var dtReport = dtMerge.Clone();
		foreach (DataColumn col in dtReport.Columns) col.DataType = typeof(string);
		dtMerge.AsEnumerable().Take(dtMerge.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsUserWiseIncludeInvoiceType(DataSet dsRegister)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(false);
		var dtMaster = dsRegister.Tables[0];
		var dtMerge = new DataTable();
		var dtUserInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("EnterBy")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in dtUserInfo.Rows)
		{
			var dtPayment = dtMaster.Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();

			var paymentMode = dtPayment.AsEnumerable().GroupBy(r => new
			{
				voucherNo = r.Field<string>("Payment_Mode")
			}).Select(g => g.First()).CopyToDataTable();

			foreach (DataRow pMode in paymentMode.Rows)
			{
				var master = dtMaster.Select($"EnterBy='{row["EnterBy"]}' and Payment_Mode='{pMode["Payment_Mode"]}'")
					.CopyToDataTable();
				if (dsRegister.Tables.Count > 1)
				{
					var dtTerm = dsRegister.Tables[1]
						.Select($"EnterBy='{row["EnterBy"]}' and Payment_Mode='{pMode["Payment_Mode"]}' ")
						.CopyToDataTable();
					master.PrimaryKey = new[]
					{
						master.Columns["VoucherNo"]
					};
					dtTerm.PrimaryKey = new[]
					{
						dtTerm.Columns["VoucherNo"]
					};
					master.Merge(dtTerm);
				}

				if (dtMerge.Columns.Count == 0)
				{
					dtMerge = master.Clone();
					dtMerge.PrimaryKey = null;
					dtMerge.Columns[0].AllowDBNull = true;
				}

				newRow = dtMerge.NewRow();
				newRow["LedgerDesc"] = row["EnterBy"] + $" => [{pMode["Payment_Mode"].GetTrimReplace()}]";
				newRow["IsGroup"] = 1;
				dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
				master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

				newRow = dtMerge.NewRow();
				newRow["LedgerDesc"] = $"[{pMode["Payment_Mode"].GetTrimReplace()}] TOTAL AMOUNT =>";
				newRow["BasicAmount"] = master.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
				if (termColumn != null && termColumn.Rows.Count > 0)
				{
					foreach (DataRow drTRow in termColumn.Rows)
						newRow[drTRow["TermDesc"].ToString()] = master.AsEnumerable()
							.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());

					newRow["NetAmount"] = master.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
				}

				newRow["IsGroup"] = 22;
				dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			}

			var userTotal = dtMerge.Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["EnterBy"]}] USER TOTAL AMOUNT =>";
			newRow["BasicAmount"] = userTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			if (termColumn != null && termColumn.Rows.Count > 0)
			{
				foreach (DataRow drTRow in termColumn.Rows)
					newRow[drTRow["TermDesc"].ToString()] = userTotal.AsEnumerable()
						.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());

				newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			}

			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] = dtNGrand.AsEnumerable()
					.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());

			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		var dtReport = dtMerge.Clone();
		foreach (DataColumn col in dtReport.Columns) col.DataType = typeof(string);
		dtMerge.AsEnumerable().Take(dtMerge.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsUserWisePartialPayment(DataSet dsRegister)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(false);
		var dtMaster = dsRegister.Tables[0];
		var dtMerge = new DataTable();
		var dtUserInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("EnterBy")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in dtUserInfo.Rows)
		{
			var dtPayment = dtMaster.Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();

			var paymentMode = dtPayment.AsEnumerable().GroupBy(r => new
			{
				voucherNo = r.Field<string>("Payment_Mode")
			}).Select(g => g.First()).CopyToDataTable();

			foreach (DataRow pMode in paymentMode.Rows)
			{
				var master = dtMaster.Select($"EnterBy='{row["EnterBy"]}' and Payment_Mode='{pMode["Payment_Mode"]}'")
					.CopyToDataTable();
				if (dtMerge.Columns.Count == 0) dtMerge = master.Clone();
				newRow = dtMerge.NewRow();
				newRow["LedgerDesc"] = row["EnterBy"] + $" => [{pMode["Payment_Mode"].GetTrimReplace()}]";
				newRow["IsGroup"] = 1;
				dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
				master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

				newRow = dtMerge.NewRow();
				newRow["LedgerDesc"] = $"[{pMode["Payment_Mode"].GetTrimReplace()}] TOTAL AMOUNT =>";
				newRow["NetAmount"] = master.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
				newRow["IsGroup"] = 22;
				dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			}

			var userTotal = dtMerge.Select($"EnterBy='{row["EnterBy"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["EnterBy"]}] USER TOTAL AMOUNT =>";
			newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		var dtReport = dtMerge.Clone();
		foreach (DataColumn col in dtReport.Columns) col.DataType = typeof(string);
		dtMerge.AsEnumerable().Take(dtMerge.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsLedgerWise(DataSet dsRegister, bool isPurchase = false)
	{
		DataRow newRow;
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsRegister.Tables[0];
		var dtMerge = new DataTable();
		var ledgerInfo = dtMaster.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("LedgerDesc")
		}).Select(g => g.First()).CopyToDataTable();

		foreach (DataRow row in ledgerInfo.Rows)
		{
			var master = dtMaster.Select($"LedgerDesc='{row["LedgerDesc"]}'").CopyToDataTable();

			if (dsRegister.Tables.Count > 1)
			{
				var dataRows = dsRegister.Tables[1].Select($"LedgerDesc='{row["LedgerDesc"]}'");

				master.PrimaryKey = new[]
				{
					master.Columns["VoucherNo"]
				};
				if (dataRows != null && dataRows.Length > 0)
				{
					var dtTerm = dataRows.CopyToDataTable();
					if (dtTerm.Rows.Count > 0)
					{
						dtTerm.PrimaryKey = new[]
						{
							dtTerm.Columns["VoucherNo"]
						};
						master.Merge(dtTerm);
					}
				}
			}

			if (dtMerge.Columns.Count == 0)
			{
				dtMerge = master.Clone();
				dtMerge.PrimaryKey = null;
				dtMerge.Columns[0].AllowDBNull = true;
			}

			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = row["LedgerDesc"];
			newRow["IsGroup"] = 1;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
			master.AsEnumerable().Take(master.Rows.Count).CopyToDataTable(dtMerge, LoadOption.OverwriteChanges);

			var userTotal = dtMerge.Select($"LedgerDesc='{row["LedgerDesc"]}'").CopyToDataTable();
			newRow = dtMerge.NewRow();
			newRow["LedgerDesc"] = $"[{row["LedgerDesc"].GetTrimReplace()}] TOTAL AMOUNT =>";
			newRow["BasicAmount"] = userTotal.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
			if (termColumn != null && termColumn.Rows.Count > 0)
			{
				foreach (DataRow drTRow in termColumn.Rows)
					newRow[drTRow["TermDesc"].ToString()] = userTotal.AsEnumerable()
						.Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
				newRow["NetAmount"] = userTotal.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
			}

			newRow["IsGroup"] = 11;
			dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		}

		var dtNGrand = dtMerge.Select("IsGroup = 0").CopyToDataTable();
		newRow = dtMerge.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] =
					dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMerge.Rows.InsertAt(newRow, dtMerge.Rows.Count + 1);
		var dtReport = dtMerge.Clone();
		foreach (DataColumn col in dtReport.Columns) col.DataType = typeof(string);
		dtMerge.AsEnumerable().Take(dtMerge.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);
		return dtReport;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsProductWise(DataSet dsRegister, bool isPurchase = false)
	{
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsRegister.Tables[0];

		if (termColumn != null && termColumn.Rows.Count > 0)
			foreach (DataRow row in dtMaster.Rows)
			{
				var basicAmount = row["BasicAmount"].GetDecimal();
				foreach (DataRow term in termColumn.Rows)
					if (term["TermSign"].Equals("+"))
						basicAmount += row[$"{term["TermDesc"]}"].GetDecimal();
					else if (term["TermSign"].Equals("-")) basicAmount -= row[$"{term["TermDesc"]}"].GetDecimal();
				row.SetField("NetAmount", basicAmount.GetDecimalComma());
			}

		var dtNGrand = dtMaster.Select("IsGroup = 0").CopyToDataTable();
		var newRow = dtMaster.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] =
					dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMaster.Rows.InsertAt(newRow, dtMaster.Rows.Count + 1);
		return dtMaster;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsProductGroupWise(DataSet dsRegister, bool isPurchase = false)
	{
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsRegister.Tables[0];

		if (termColumn != null && termColumn.Rows.Count > 0)
			foreach (DataRow row in dtMaster.Rows)
			{
				var basicAmount = row["BasicAmount"].GetDecimal();
				foreach (DataRow term in termColumn.Rows)
					if (term["TermSign"].Equals("+"))
						basicAmount += row[$"{term["TermDesc"]}"].GetDecimal();
					else if (term["TermSign"].Equals("-")) basicAmount -= row[$"{term["TermDesc"]}"].GetDecimal();
				row.SetField("NetAmount", basicAmount.GetDecimalComma());
			}

		var dtNGrand = dtMaster.Select("IsGroup = 0").CopyToDataTable();
		var newRow = dtMaster.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] =
					dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMaster.Rows.InsertAt(newRow, dtMaster.Rows.Count + 1);
		return dtMaster;
	}

	private DataTable ReturnSalesPurchaseRegisterSummaryReportsProductSubGroupWise(DataSet dsRegister, bool isPurchase = false)
	{
		var termColumn = GetPurchaseSalesTermName(isPurchase);
		var dtMaster = dsRegister.Tables[0];

		if (termColumn != null && termColumn.Rows.Count > 0)
			foreach (DataRow row in dtMaster.Rows)
			{
				var basicAmount = row["BasicAmount"].GetDecimal();
				foreach (DataRow term in termColumn.Rows)
					if (term["TermSign"].Equals("+"))
						basicAmount += row[$"{term["TermDesc"]}"].GetDecimal();
					else if (term["TermSign"].Equals("-")) basicAmount -= row[$"{term["TermDesc"]}"].GetDecimal();
				row.SetField("NetAmount", basicAmount.GetDecimalComma());
			}

		var dtNGrand = dtMaster.Select("IsGroup = 0").CopyToDataTable();
		var newRow = dtMaster.NewRow();
		newRow["LedgerDesc"] = "TOTAL NET AMOUNT =>";
		newRow["BasicAmount"] = dtNGrand.AsEnumerable().Sum(x => x["BasicAmount"].GetDecimal());
		if (termColumn != null && termColumn.Rows.Count > 0)
		{
			foreach (DataRow drTRow in termColumn.Rows)
				newRow[drTRow["TermDesc"].ToString()] =
					dtNGrand.AsEnumerable().Sum(x => x[drTRow["TermDesc"].ToString()].GetDecimal());
			newRow["NetAmount"] = dtNGrand.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());
		}

		newRow["IsGroup"] = 99;
		dtMaster.Rows.InsertAt(newRow, dtMaster.Rows.Count + 1);
		return dtMaster;
	}

	#endregion *||* ---------- SUMMARY METHOD ---------- *||*


	// VAT REGISTER
	#region *||*--------------- VAR REGISTER REPORT ---------------*||*

	public DataTable GetVatRegisterNormal()
	{
		var dtSalesRegister = GetNewSalesVatRegisterDetailsReportsScript("SB").GetQueryDataTable();
		var dtPurchaseRegister = GetPurchaseVatRegisterDayWiseReportsScript("PB").GetQueryDataTable();
		var dtVatRegister = ReturnVatRegisterReportNormal(dtSalesRegister, dtPurchaseRegister);
		return dtVatRegister;
	}

	private DataTable GenerateVatRegisterColumns()
	{
		var dtReport = new DataTable();
		dtReport.AddStringColumns(new[]
		{
			"dt_SalesDate",
			"dt_SalesMiti",
			"dt_SalesNo",
			"dt_CustomerLedger",
			"dt_CustomerPanNo",
			"dt_TotalSales",
			"dt_TaxFreeSales",
			"dt_ExportSales",
			"dt_TaxableSales",
			"dt_VatSales"
		});
		dtReport.AddStringColumns(new[]
		{
			"dt_PurchaseDate",
			"dt_PurchaseMiti",
			"dt_PurchaseNo",
			"dt_VendorLedger",
			"dt_VendorPanNo",
			"dt_TotalPurchase",
			"dt_TaxFreePurchase",
			"dt_ImportPurchase",
			"dt_ImportVatPurchase",
			"dt_TaxablePurchase",
			"dt_VatPurchase",
			"dt_CapitalPurchase",
			"dt_CapitalVatPurchase"
		});
		dtReport.AddColumn("IsGroup", typeof(int));
		return dtReport;
	}

	private DataTable ReturnVatRegisterTransactionValue(string cmdString)
	{
		var listResult = QueryUtils.GetList<VatLedgerIrdTransactionAbove>(cmdString);

		var finalList = new List<VatLedgerIrdTransactionAbove>();
		var productDetails = listResult.List.ToList();
		finalList.AddRange(productDetails);
		finalList.Add(new VatLedgerIrdTransactionAbove
		{
			Ledger = "[GRAND TOTAL] ",
			Taxable = listResult.List.Sum(y => y.Taxable),
			TaxFree = listResult.List.Sum(y => y.TaxFree),
			IsGroup = 99
		});
		var dtReport = finalList.ToDataTable();
		return dtReport;
	}

	private DataTable ReturnVatRegisterReportNormal(DataTable dtSales, DataTable dtPurchase)
	{
		DataRow dataRow;
		var dtReport = GenerateVatRegisterColumns();
		if (dtSales.Rows.Count > 0)
			foreach (DataRow ro in dtSales.Rows)
			{
				var IsReverse = ro["R_Invoice"].GetBool();
				dataRow = dtReport.NewRow();
				dataRow["dt_SalesDate"] = ro["VoucherDate"].ToString();
				dataRow["dt_SalesMiti"] = ro["VoucherMiti"].GetIrdDateString();
				dataRow["dt_SalesNo"] = ro["VoucherNo"].ToString();
				dataRow["dt_CustomerLedger"] = ro["Ledger"].ToString();
				dataRow["dt_CustomerPanNo"] = ro["PanNo"].ToString();
				dataRow["dt_TotalSales"] = !IsReverse ? ro["TotalAmount"].ToString() : string.Empty;
				dataRow["dt_TaxFreeSales"] = !IsReverse ? ro["TaxFree"].ToString() : string.Empty;
				dataRow["dt_ExportSales"] = !IsReverse ? ro["ExportSales"].ToString() : string.Empty;
				dataRow["dt_TaxableSales"] = !IsReverse ? ro["Taxable"].ToString() : string.Empty;
				dataRow["dt_VatSales"] = !IsReverse ? ro["VatAmount"].ToString() : string.Empty;
				dataRow["IsGroup"] = 0;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
			}

		if (dtPurchase.Columns.Count > 0)
		{
			var rowCount = dtReport.Rows.Count - 1;
			foreach (DataRow ro in dtPurchase.Rows)
			{
				var IsReverse = ro["IsReverse"].GetBool();
				var index = dtPurchase.Rows.IndexOf(ro);
				var invoiceType = ro["VoucherType"].GetUpper();
				string[] type = { "IMPORT", "ASSETS" };
				var voucherNo = ro["RefVoucherNo"].IsValueExits()
					? ro["VoucherNo"] + $" ({ro["RefVoucherNo"]})"
					: ro["VoucherNo"].ToString();
				if (index <= rowCount)
				{
					dtReport.Rows[index].SetField("dt_PurchaseDate", ro["VoucherDate"].ToString());
					dtReport.Rows[index].SetField("dt_PurchaseMiti", ro["VoucherMiti"].ToString());
					dtReport.Rows[index].SetField("dt_PurchaseNo", voucherNo);
					dtReport.Rows[index].SetField("dt_VendorLedger", ro["Ledger"].ToString());
					dtReport.Rows[index].SetField("dt_VendorPanNo", ro["PanNo"].ToString());

					dtReport.Rows[index].SetField("dt_TotalPurchase",
						!IsReverse ? ro["TotalAmount"].ToString() : string.Empty);
					dtReport.Rows[index].SetField("dt_TaxFreePurchase",
						!IsReverse ? ro["TaxExampted"].ToString() : string.Empty);

					dtReport.Rows[index].SetField("dt_ImportPurchase",
						!IsReverse && invoiceType.Equals("IMPORT") ? ro["TaxableAmount"].ToString() : string.Empty);
					dtReport.Rows[index].SetField("dt_ImportVatPurchase",
						!IsReverse && invoiceType.Equals("IMPORT") ? ro["VatAmount"].ToString() : string.Empty);

					dtReport.Rows[index].SetField("dt_TaxablePurchase",
						!IsReverse && !type.Contains(invoiceType) ? ro["TaxableAmount"].ToString() : string.Empty);
					dtReport.Rows[index].SetField("dt_VatPurchase",
						!IsReverse && !type.Contains(invoiceType) ? ro["VatAmount"].ToString() : string.Empty);

					dtReport.Rows[index].SetField("dt_CapitalPurchase",
						!IsReverse && invoiceType.Equals("ASSETS") ? ro["TaxableAmount"].ToString() : string.Empty);
					dtReport.Rows[index].SetField("dt_CapitalVatPurchase",
						!IsReverse && invoiceType.Equals("ASSETS") ? ro["VatAmount"].ToString() : string.Empty);
				}
				else
				{
					dataRow = dtReport.NewRow();
					dataRow["dt_PurchaseDate"] = ro["VoucherDate"].ToString();
					dataRow["dt_PurchaseMiti"] = ro["VoucherMiti"].GetIrdDateString();
					dataRow["dt_PurchaseNo"] = ro["VoucherNo"].ToString();
					dataRow["dt_VendorLedger"] = ro["Ledger"].ToString();
					dataRow["dt_VendorPanNo"] = ro["PanNo"].ToString();

					dataRow["dt_TotalPurchase"] = !IsReverse ? ro["TotalAmount"].ToString() : string.Empty;
					dataRow["dt_TaxFreePurchase"] = !IsReverse ? ro["TaxExampted"].ToString() : string.Empty;

					dataRow["dt_ImportPurchase"] = !IsReverse && invoiceType.Equals("IMPORT")
						? ro["TaxableAmount"].ToString()
						: string.Empty;
					dataRow["dt_ImportVatPurchase"] = !IsReverse && invoiceType.Equals("IMPORT")
						? ro["VatAmount"].ToString()
						: string.Empty;

					dataRow["dt_TaxablePurchase"] = !IsReverse && !type.Contains(invoiceType)
						? ro["TaxableAmount"].ToString()
						: string.Empty;
					dataRow["dt_VatPurchase"] = !IsReverse && !type.Contains(invoiceType)
						? ro["VatAmount"].ToString()
						: string.Empty;

					dataRow["dt_CapitalPurchase"] = !IsReverse && invoiceType.Equals("ASSETS")
						? ro["TaxableAmount"].ToString()
						: string.Empty;
					dataRow["dt_CapitalVatPurchase"] = !IsReverse && invoiceType.Equals("ASSETS")
						? ro["VatAmount"].ToString()
						: string.Empty;

					dataRow["IsGroup"] = 0;
					dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
				}
			}
		}

		if (dtPurchase.Rows.Count is 0 && dtSales.Rows.Count is 0) return new DataTable();
		dataRow = dtReport.NewRow();

		dataRow["dt_CustomerLedger"] = "GRAND TOTAL =>";
		dataRow["dt_TotalSales"] = dtReport.AsEnumerable().Sum(x => x["dt_TotalSales"].GetDecimal());
		dataRow["dt_TaxFreeSales"] = dtReport.AsEnumerable().Sum(x => x["dt_TaxFreeSales"].GetDecimal());
		dataRow["dt_ExportSales"] = dtReport.AsEnumerable().Sum(x => x["dt_ExportSales"].GetDecimal());
		dataRow["dt_TaxableSales"] = dtReport.AsEnumerable().Sum(x => x["dt_TaxableSales"].GetDecimal());
		dataRow["dt_VatSales"] = dtReport.AsEnumerable().Sum(x => x["dt_VatSales"].GetDecimal());

		dataRow["dt_VendorLedger"] = "GRAND TOTAL =>";
		dataRow["dt_TotalPurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_TotalPurchase"].GetDecimal());
		dataRow["dt_TaxFreePurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_TaxFreePurchase"].GetDecimal());
		dataRow["dt_ImportPurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_ImportPurchase"].GetDecimal());
		dataRow["dt_ImportVatPurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_ImportVatPurchase"].GetDecimal());
		dataRow["dt_TaxablePurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_TaxablePurchase"].GetDecimal());
		dataRow["dt_VatPurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_VatPurchase"].GetDecimal());
		dataRow["dt_CapitalPurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_CapitalPurchase"].GetDecimal());
		dataRow["dt_CapitalVatPurchase"] = dtReport.AsEnumerable().Sum(x => x["dt_CapitalVatPurchase"].GetDecimal());
		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

		var dtDifference = dtReport.Select("IsGroup='99'").CopyToDataTable();
		if (dtDifference.Rows.Count > 0)
		{
			var purchaseVat = dtDifference.Rows[0]["dt_CapitalVatPurchase"].GetDecimal() +
							  dtDifference.Rows[0]["dt_ImportVatPurchase"].GetDecimal() +
							  dtDifference.Rows[0]["dt_VatPurchase"].GetDecimal();
			var salesVat = dtDifference.Rows[0]["dt_VatSales"].GetDecimal();
			var difference = salesVat - purchaseVat;
			if (Math.Abs(difference) > 0)
			{
				dataRow = dtReport.NewRow();
				if (difference > 0)
				{
					dataRow["dt_VendorLedger"] = "VAT AMOUNT PAYABLE =>";
					dataRow["dt_TotalPurchase"] = Math.Abs(difference);
				}
				else if (difference < 0)
				{
					dataRow["dt_CustomerLedger"] = "VAT AMOUNT RECEIVABLE =>";
					dataRow["dt_VatSales"] = Math.Abs(difference);
				}

				dataRow["IsGroup"] = 88;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
			}
		}

		return dtReport;
	}

	public DataTable GetVatRegisterTransactionValue()
	{
		var cmdString = @$"
			WITH VatLedger AS
			(
			  SELECT pm.Customer_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, 'E' TradeNameType, 'S' PurchaseSales, SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS Taxable, SUM(ISNULL(term.VatAmount, 0)) AS VatAmount, SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)) >=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS TaxFree
			  FROM AMS.SB_Master pm
				   LEFT OUTER JOIN (
									 SELECT vat.SB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM (
											SELECT pt.SB_VNo, CASE WHEN pt1.Order_No< (SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id={GetReports.VatTermId[0]}) THEN SUM(pt.Amount) WHEN pt1.ST_Sign='-' AND pt1.Order_No< (SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id={GetReports.VatTermId[0]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={GetReports.VatTermId[0]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
											FROM AMS.SB_Term pt
												 LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
											WHERE pt.Term_Type<>'BT'
											GROUP BY pt.SB_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No
										  ) AS vat
									 GROUP BY vat.SB_VNo
								   ) AS term ON pm.SB_Invoice=term.SB_VNo
				   LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
				   LEFT OUTER JOIN (
									 SELECT pd.SB_Invoice, SUM(pd.Qty) AS Qty FROM AMS.SB_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
									 GROUP BY pd.SB_Invoice
								   ) AS d ON d.SB_Invoice=pm.SB_Invoice
			  WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.R_Invoice = 0
			  GROUP BY pm.Customer_ID, gl.GLName, gl.PanNo
			  UNION ALL
			  SELECT pm.Vendor_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, 'E' TradeNameType, 'P' PurchaseSales, SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS Taxable, SUM(ISNULL(term.VatAmount, 0)) AS VatAmount, SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)) >=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS TaxFree
			  FROM AMS.PB_Master pm
				   LEFT OUTER JOIN (
									 SELECT vat.PB_Vno, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM (
											SELECT pt.PB_Vno, CASE WHEN pt1.Order_No< (SELECT pt3.Order_No FROM AMS.PT_Term pt3 WHERE pt3.PT_Id={GetReports.VatTermId[1]}) THEN SUM(pt.Amount) WHEN pt1.PT_Sign='-' AND pt1.Order_No< (SELECT pt2.Order_No FROM AMS.PT_Term pt2 WHERE pt2.PT_Id={GetReports.VatTermId[1]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.PT_Id={GetReports.VatTermId[1]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
											FROM AMS.PB_Term pt
												 LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
											WHERE pt.Term_Type<>'BT'
											GROUP BY pt.PB_Vno, pt.PT_Id, pt1.PT_Sign, pt1.Order_No
										  ) AS vat
									 GROUP BY vat.PB_Vno
								   ) AS term ON pm.PB_Invoice=term.PB_Vno
				   LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
				   LEFT OUTER JOIN ( SELECT pd.PB_Invoice, SUM(pd.Qty) AS Qty FROM AMS.PB_Details AS pd LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id GROUP BY pd.PB_Invoice ) AS d ON d.PB_Invoice=pm.PB_Invoice
			  WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'  AND pm.R_Invoice = 0
			  GROUP BY pm.Vendor_ID, gl.GLName, gl.PanNo
			)
			SELECT vl.LedgerId, vl.Ledger, vl.PanNo, vl.TradeNameType,vl.PurchaseSales,CAST(vl.Taxable AS DECIMAL(18,{SysAmountLength})) Taxable,CAST(vl.TaxFree AS DECIMAL(18,{SysAmountLength})) TaxFree
			FROM VatLedger vl ";
		if (GetReports.FilterValue.GetDecimal() > 0)
			cmdString += @$"
				WHERE CAST(vl.Taxable AS DECIMAL(18,{SysAmountLength})) >= {GetReports.FilterValue.GetDecimal()}";
		return ReturnVatRegisterTransactionValue(cmdString);
	}

	public DataTable GetMaterializeViewRegister()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
			WITH VatRegister AS (SELECT vat.SB_Invoice, SUM(ISNULL(BasicAmount, 0)) BasicAmount, SUM(vat.TaxAmount) TaxAmount, SUM(ISNULL(vat.Discount, 0)) Discount, SUM(vat.Tax_FreeSales) Tax_FreeSales, SUM(vat.TaxableSales) TaxableSales
					 FROM(SELECT sd.SB_Invoice, sd.P_Id, ISNULL(sd.N_Amount,0) BasicAmount, ISNULL(sd.T_Amount, 0) TermAmount, ISNULL(sd.N_Amount, 0) NetAmout, ISNULL(v.TaxAmount, 0) TaxAmount, ISNULL(p.PDiscount, 0) Discount, CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.N_Amount+ISNULL(p.PDiscount, 0))END Tax_FreeSales, CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE ( sd.N_Amount + ISNULL(p.PDiscount, 0))END TaxableSales
						  FROM AMS.SB_Details sd
							   LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
											   FROM AMS.SB_Term
											   WHERE ST_Id={SalesVatTermId} AND Term_Type<>'B'
											   GROUP BY SB_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=v.SNo
							   LEFT OUTER JOIN(SELECT st.SB_VNo, st.SNo, st.Product_Id, SUM(CASE WHEN s1.ST_Sign='+' THEN  st.Amount ELSE -st.Amount END) PDiscount
											   FROM AMS.SB_Term st
											   LEFT OUTER JOIN AMS.ST_Term s1 ON s1.ST_ID = st.ST_Id
											   WHERE st.Product_Id IS NOT NULL	AND st.ST_Id IN(SELECT st1.ST_ID
															  FROM AMS.ST_Term st1
															  WHERE st1.Order_No<(SELECT st2.Order_No FROM AMS.ST_Term st2 WHERE st2.ST_ID={SalesVatTermId}))
											   GROUP BY st.SB_VNo, st.SNo, st.Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=p.SNo
						  WHERE 1=1) vat
					 GROUP BY vat.SB_Invoice)
			SELECT fy.BS_FY Fiscal_Year, v.SB_Invoice FromBill_No, sm.Invoice_Miti, gl.GLName Customer_Name, gl.PanNo Customer_PAN, CONVERT(NVARCHAR(10), sm.Invoice_Date, 103) Bill_Date,
			CASE WHEN sm.R_Invoice=0 AND ISNULL(v.BasicAmount, 0)>0 THEN FORMAT(v.BasicAmount, '{SysAmountCommaFormat}')ELSE '' END Amount,
			CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 THEN FORMAT(sm.N_Amount, '{SysAmountCommaFormat}')ELSE '' END TotalAmount,
			CASE WHEN sm.R_Invoice=0 AND ABS(ISNULL(v.Discount, 0))>0 THEN FORMAT(v.Discount, '{SysAmountCommaFormat}')ELSE '' END Discount,
			CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Tax_FreeSales, 0)>0 THEN FORMAT(v.Tax_FreeSales, '{SysAmountCommaFormat}')ELSE '' END TaxFree_Sales,
			CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxableSales, 0)>0 THEN FORMAT(v.TaxableSales, '{SysAmountCommaFormat}')ELSE '' END Taxable_Amount,
			CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxAmount, 0)>0 THEN FORMAT(v.TaxAmount, '{SysAmountCommaFormat}')ELSE '' END Tax_Amount, sm.Is_Printed Is_Printed, sm.IsAPIPosted, (CASE WHEN sm.Action_Type IN ('Cancel', 'Return') THEN 'N' ELSE 'Y' END) Is_Active, sm.Printed_Date, sm.Enter_By Entered_By, sm.Payment_Mode, sm.Printed_By Printed_By, sm.IsRealtime, CASE WHEN sm.R_Invoice>0 THEN 12 ELSE 0 END IsGroup
			FROM VatRegister v
			LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=v.SB_Invoice
			LEFT OUTER JOIN AMS.FiscalYear fy ON fy.FY_Id=sm.FiscalYearId
			LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			ORDER BY sm.Invoice_Date, sm.SB_Invoice;");
		var dtReport = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		if (dtReport.Rows.Count > 0)
		{
			var dataRow = dtReport.NewRow();
			dataRow["Customer_Name"] = "GRAND TOTAL >>";
			dataRow["Amount"] = dtReport.AsEnumerable().Sum(row => row["Amount"].GetDecimal()).GetDecimalComma();
			dataRow["Discount"] = dtReport.AsEnumerable().Sum(row => row["Discount"].GetDecimal()).GetDecimalComma();
			dataRow["TaxFree_Sales"] = dtReport.AsEnumerable().Sum(row => row["TaxFree_Sales"].GetDecimal()).GetDecimalComma();
			dataRow["Taxable_Amount"] = dtReport.AsEnumerable().Sum(row => row["Taxable_Amount"].GetDecimal()).GetDecimalComma();
			dataRow["Tax_Amount"] = dtReport.AsEnumerable().Sum(row => row["Tax_Amount"].GetDecimal()).GetDecimalComma();
			dataRow["TotalAmount"] = dtReport.AsEnumerable().Sum(row => row["TotalAmount"].GetDecimal()).GetDecimalComma();
			dataRow["IsGroup"] = 99;
			dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
		}

		return dtReport;
	}

	//public DataTable GetMaterializeViewRegister()
	//{
	//    var cmdString = new StringBuilder();
	//    cmdString.Append($@"
	//        WITH VatRegister AS (SELECT vat.SB_Invoice, SUM(ISNULL(BasicAmount, 0)) BasicAmount, SUM(vat.TaxAmount) TaxAmount, SUM(ISNULL(vat.Discount, 0)) Discount, SUM(vat.Tax_FreeSales) Tax_FreeSales, SUM(vat.TaxableSales) TaxableSales
	//                 FROM(SELECT sd.SB_Invoice, sd.P_Id, ISNULL(sd.N_Amount,0) BasicAmount, ISNULL(sd.T_Amount, 0) TermAmount, ISNULL(sd.N_Amount, 0) NetAmout, ISNULL(v.TaxAmount, 0) TaxAmount, ISNULL(p.PDiscount, 0) Discount, CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.N_Amount+ISNULL(p.PDiscount, 0))END Tax_FreeSales, CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE ( sd.N_Amount + ISNULL(p.PDiscount, 0) - ISNULL(v.TaxAmount,0))END TaxableSales
	//                      FROM AMS.SB_Details sd
	//                           LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
	//                                           FROM AMS.SB_Term
	//                                           WHERE ST_Id={SalesVatTermId} AND Term_Type<>'B'
	//                                           GROUP BY SB_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=v.SNo
	//                           LEFT OUTER JOIN(SELECT st.SB_VNo, st.SNo, st.Product_Id, SUM(CASE WHEN s1.ST_Sign='+' THEN  st.Amount ELSE -st.Amount END) PDiscount
	//                                           FROM AMS.SB_Term st
	//				               LEFT OUTER JOIN AMS.ST_Term s1 ON s1.ST_ID = st.ST_Id
	//                                           WHERE st.Product_Id IS NOT NULL	AND st.ST_Id IN(SELECT st1.ST_ID
	//                                                          FROM AMS.ST_Term st1
	//                                                          WHERE st1.Order_No<(SELECT st2.Order_No FROM AMS.ST_Term st2 WHERE st2.ST_ID={SalesVatTermId}))
	//                                           GROUP BY st.SB_VNo, st.SNo, st.Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=p.SNo
	//                      WHERE 1=1) vat
	//                 GROUP BY vat.SB_Invoice)
	//        SELECT fy.BS_FY Fiscal_Year, v.SB_Invoice FromBill_No, sm.Invoice_Miti, gl.GLName Customer_Name, gl.PanNo Customer_PAN, CONVERT(NVARCHAR(10), sm.Invoice_Date, 103) Bill_Date,
	//        CASE WHEN sm.R_Invoice=0 AND ISNULL(v.BasicAmount, 0)>0 THEN FORMAT(v.BasicAmount, '{SysAmountCommaFormat}')ELSE '' END Amount,
	//        CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 THEN FORMAT(sm.N_Amount, '{SysAmountCommaFormat}')ELSE '' END TotalAmount,
	//        CASE WHEN sm.R_Invoice=0 AND ABS(ISNULL(v.Discount, 0))>0 THEN FORMAT(v.Discount, '{SysAmountCommaFormat}')ELSE '' END Discount,
	//        CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Tax_FreeSales, 0)>0 THEN FORMAT(v.Tax_FreeSales, '{SysAmountCommaFormat}')ELSE '' END TaxFree_Sales,
	//        CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxableSales, 0)>0 THEN FORMAT(v.TaxableSales, '{SysAmountCommaFormat}')ELSE '' END Taxable_Amount,
	//        CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxAmount, 0)>0 THEN FORMAT(v.TaxAmount, '{SysAmountCommaFormat}')ELSE '' END Tax_Amount, sm.Is_Printed Is_Printed, sm.IsAPIPosted, (CASE WHEN sm.Action_Type IN ('Cancel', 'Return') THEN 'N' ELSE 'Y' END) Is_Active, sm.Printed_Date, sm.Enter_By Entered_By, sm.Payment_Mode, sm.Printed_By Printed_By, sm.IsRealtime, CASE WHEN sm.R_Invoice>0 THEN 12 ELSE 0 END IsGroup
	//        FROM VatRegister v
	//        LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=v.SB_Invoice
	//        LEFT OUTER JOIN AMS.FiscalYear fy ON fy.FY_Id=sm.FiscalYearId
	//        LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
	//        WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
	//        ORDER BY sm.Invoice_Date, sm.SB_Invoice;");
	//    var dtReport = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
	//    if (dtReport.Rows.Count > 0)
	//    {
	//        var dataRow = dtReport.NewRow();
	//        dataRow["Customer_Name"] = "GRAND TOTAL >>";
	//        dataRow["Amount"] = dtReport.AsEnumerable().Sum(row => row["Amount"].GetDecimal()).GetDecimalComma();
	//        dataRow["Discount"] = dtReport.AsEnumerable().Sum(row => row["Discount"].GetDecimal()).GetDecimalComma();
	//        dataRow["TaxFree_Sales"] =
	//            dtReport.AsEnumerable().Sum(row => row["TaxFree_Sales"].GetDecimal()).GetDecimalComma();
	//        dataRow["Taxable_Amount"] = dtReport.AsEnumerable().Sum(row => row["Taxable_Amount"].GetDecimal())
	//            .GetDecimalComma();
	//        dataRow["Tax_Amount"] =
	//            dtReport.AsEnumerable().Sum(row => row["Tax_Amount"].GetDecimal()).GetDecimalComma();
	//        dataRow["TotalAmount"] =
	//            dtReport.AsEnumerable().Sum(row => row["TotalAmount"].GetDecimal()).GetDecimalComma();
	//        dataRow["IsGroup"] = 99;
	//        dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
	//    }

	//    return dtReport;
	//}

	public DataTable GetEntryLogRegister()
	{
		var cmdString = new StringBuilder();
		cmdString.Append($@"
		WITH ENTRY_LOG_REGISTER AS (SELECT 'SALES INVOICE' VOUCHER_MODULE, sm.SB_Invoice VOUCHER_NO, sm.Invoice_Date VOUCHER_DATE, sm.Invoice_Miti VOUCHER_MITI, sm.Invoice_Time VOUCHER_TIME, sm.Action_Type VOUCHER_TYPE, sm.N_Amount AMOUNT, sm.ENTER_BY ENTER_BY, sm.ENTER_DATE ENTER_DATE
			FROM AMS.SB_Master sm
		UNION ALL
		SELECT 'SALES RETURN' VOUCHER_MODULE, sm.SB_Invoice Voucher_No, sm.Invoice_Date VOUCHER_DATE, sm.Invoice_Miti VOUCHER_MITI, sm.Invoice_Time VOUCHER_TIME, sm.Action_Type VOUCHER_TYPE, sm.N_Amount AMOUNT, sm.ENTER_BY ENTER_BY, sm.Enter_Date Enter_Date
			FROM AMS.SR_Master sm
		UNION ALL
		SELECT 'PURCHASE INVOICE' VOUCHER_MODULE, pm.PB_Invoice Voucher_No, pm.Invoice_Date VOUCHER_DATE, pm.Invoice_Miti VOUCHER_MITI, pm.Invoice_Time VOUCHER_TIME, pm.Action_Type VOUCHER_TYPE, pm.N_Amount AMOUNT, pm.ENTER_BY ENTER_BY, pm.Enter_Date Enter_Date
			FROM AMS.PB_Master pm
		UNION ALL
		SELECT 'PURCHASE RETURN' VOUCHER_MODULE, pm.PB_Invoice Voucher_No, pm.Invoice_Date VOUCHER_DATE, pm.Invoice_Miti VOUCHER_MITI, pm.Invoice_Time VOUCHER_TIME, pm.Action_Type VOUCHER_TYPE, pm.N_Amount AMOUNT, pm.ENTER_BY ENTER_BY, pm.Enter_Date Enter_Date
			FROM AMS.PR_Master pm
		UNION ALL
		SELECT 'CASH/BANK VOUCHER' VOUCHER_MODULE, cm.Voucher_No Voucher_No, cm.VOUCHER_DATE VOUCHER_DATE, cm.VOUCHER_MITI VOUCHER_MITI, cm.VOUCHER_TIME VOUCHER_TIME, cm.Action_Type VOUCHER_TYPE, ABS(SUM(cd.LocalDebit - cd.LocalCredit)) AMOUNT, cm.EnterBy ENTER_BY, cm.EnterDate Enter_Date
			FROM AMS.CB_Master cm
			LEFT OUTER JOIN AMS.CB_Details cd ON cm.Voucher_No = cd.Voucher_No
			GROUP BY cm.Voucher_No, cm.VOUCHER_DATE, cm.VOUCHER_MITI, cm.VOUCHER_TIME, cm.Action_Type, cm.EnterBy, cm.EnterDate
		UNION ALL
		SELECT 'CASH/BANK VOUCHER' VOUCHER_MODULE, cm.Voucher_No Voucher_No, cm.VOUCHER_DATE VOUCHER_DATE, cm.VOUCHER_MITI VOUCHER_MITI, cm.VOUCHER_TIME VOUCHER_TIME, cm.Action_Type VOUCHER_TYPE, ABS(SUM(cd.LocalDebit)) AMOUNT, cm.EnterBy ENTER_BY, cm.EnterDate Enter_Date
			FROM AMS.JV_Master cm
			LEFT OUTER JOIN AMS.JV_Details cd ON cm.Voucher_No = cd.Voucher_No
			GROUP BY cm.Voucher_No, cm.VOUCHER_DATE, cm.VOUCHER_MITI, cm.VOUCHER_TIME, cm.Action_Type, cm.EnterBy, cm.EnterDate)
		SELECT elg.VOUCHER_MODULE, elg.Voucher_No, elg.VOUCHER_DATE, elg.VOUCHER_MITI, elg.VOUCHER_TIME, elg.VOUCHER_TYPE, elg.Amount, elg.Enter_By, elg.Enter_Date
		FROM ENTRY_LOG_REGISTER elg
		WHERE elg.VOUCHER_DATE BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'; ");
		var dtReport = SqlExtensions.ExecuteDataSet(cmdString.ToString()).Tables[0];
		return dtReport;
	}

	#endregion *||*--------------- VAR REGISTER REPORT ---------------*||*


	// SALES VAT REGISTER REPORT
	#region *||* --------------- SALES VAT REGISTER REPORT --------------- *||*

	// GET SALES VAT REGISTER SUMMARY
	private string GetSalesVatRegisterSummaryCustomerWise(string module)
	{
		var cmdString = string.Empty;
		if (module.Equals("SB"))
		{
			cmdString = $@"
				SELECT ROW_NUMBER() OVER( ORDER BY gl.GLName) Sno, pm.Customer_Id AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category, CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty,";
			if (ServerVersion < 10)
				cmdString +=
					$@" CAST(SUM(pm.LN_Amount) AS DECIMAL(18,{SysAmountLength})) AS TotalAmount, CAST( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS DECIMAL(18,{SysAmountLength}) AS TaxFree, CAST(0 AS DECIMAL(18,{SysAmountLength})) AS ExportSales, CAST(SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS DECIMAL(18,{SysAmountLength})) AS Taxable, CAST(SUM(ISNULL(term.VatAmount, 0)) AS DECIMAL(18,{SysAmountLength})) AS VatAmount,0 IsGroup ";
			else
				cmdString +=
					$@" FORMAT(SUM(pm.LN_Amount),'{SysAmountCommaFormat}') AS TotalAmount, FORMAT( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END),'{SysAmountCommaFormat}') AS TaxFree, FORMAT(0,'{SysAmountCommaFormat}') AS ExportSales, FORMAT(SUM(ISNULL(term.VatAmount, 0)/ 0.13),'{SysAmountCommaFormat}') AS Taxable, FORMAT(SUM(ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') AS VatAmount,0 IsGroup ";
			cmdString += $@"
				FROM AMS.SB_Master pm
					 LEFT OUTER JOIN(SELECT vat.SB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM(SELECT pt.SB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID={GetReports.VatTermId[0]}) THEN SUM(pt.Amount)
															WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_ID={GetReports.VatTermId[0]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={GetReports.VatTermId[0]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
										  FROM AMS.SB_Term pt
											   LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_ID
										  WHERE pt.Term_Type<>'BT'
										  GROUP BY pt.SB_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
									 GROUP BY vat.SB_VNo) AS term ON pm.SB_Invoice=term.SB_VNo
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_Id=gl.GLID
					 LEFT OUTER JOIN(SELECT pd.SB_Invoice, SUM(pd.Qty) AS Qty
									 FROM AMS.SB_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
									 GROUP BY pd.SB_Invoice) AS d ON d.SB_Invoice=pm.SB_Invoice
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND (pm.R_Invoice = 0 OR pm.R_Invoice IS NULL)
				GROUP BY pm.Customer_Id, gl.GLName, gl.PanNo";
			if (GetReports.FilterValue.GetDecimal() > 0)
				cmdString += $@"
				HAVING SUM(pm.LN_Amount) >= {GetReports.FilterValue}";
			cmdString += @"
				ORDER BY gl.GLName ";
		}
		else if (module.Equals("SR"))
		{
			cmdString = $@"
				SELECT pm.Customer_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category,CAST(SUM(d.Qty)  AS DECIMAL(18,{SysQtyLength})) Qty,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(pm.LN_Amount) AS DECIMAL(18,{SysAmountLength}) AS TotalAmount,CAST(SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0) ELSE 0 END) AS DECIMAL(18,{SysAmountLength})) AS TaxFree, CAST(0 AS DECIMAL(18,{SysAmountLength})) AS ExportSales,CAST(SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS DECIMAL(18,{SysAmountLength})) AS Taxable,CAST(SUM(ISNULL(term.VatAmount, 0)) AS DECIMAL(18,{SysAmountLength})) AS VatAmount, 0 IsGroup "
				: $@" FORMAT(SUM(pm.LN_Amount),'{SysAmountCommaFormat}') AS TotalAmount,FORMAT(SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0) ELSE 0 END),'{SysAmountCommaFormat}') AS TaxFree,FORMAT( 0,'{SysAmountCommaFormat}') AS ExportSales, FORMAT(SUM(ISNULL(term.VatAmount, 0)/ 0.13),'{SysAmountCommaFormat}') AS Taxable, FORMAT(SUM(ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') AS VatAmount, 0 IsGroup ";
			cmdString += $@"
				FROM AMS.SR_Master pm
					 LEFT OUTER JOIN (SELECT vat.SR_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
						 FROM (SELECT pt.SR_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id='{GetReports.VatTermId[0]}') THEN SUM(pt.Amount) WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id='{GetReports.VatTermId[0]}') THEN -SUM(pt.Amount) ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id='{GetReports.VatTermId[0]}' THEN SUM(pt.Amount) ELSE 0 END AS VatAmount
							 FROM AMS.SR_Term pt
							 LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
							 WHERE pt.Term_Type<>'BT'
							 GROUP BY pt.SR_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
						 GROUP BY vat.SR_VNo) AS term ON pm.SR_Invoice=term.SR_VNo
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
					 LEFT OUTER JOIN (SELECT pd.SR_Invoice, SUM(pd.Qty) AS Qty
						 FROM AMS.SR_Details AS pd
						 LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
						 GROUP BY pd.SR_Invoice) AS d ON d.SR_Invoice=pm.SR_Invoice
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND (pm.R_Invoice = 0 OR pm.R_Invoice IS NULL)
				GROUP BY pm.Customer_ID, gl.GLName, gl.PanNo, pm.R_Invoice  ";
			cmdString += GetReports.FilterValue.GetDecimal() > 0
				? $@" HAVING SUM(pm.LN_Amount) > {GetReports.FilterValue}"
				: "";
			cmdString += @"
				ORDER BY gl.GLName ";
		}

		return cmdString;
	}

	private string GetSalesVatRegisterSummaryDateWise(string module)
	{
		var cmdString = string.Empty;
		if (module.Equals("SB"))
		{
			cmdString = $@"
				SELECT pm.Invoice_Date VoucherDate,pm.Invoice_Miti VoucherMiti,'' VoucherNo,'' NoOfBill,0 LedgerId,'Day Total Sales' Ledger,'' PanNo,'' Category,CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(pm.LN_Amount) AS DECIMAL(18,{SysAmountLength}) ) AS TotalAmount, CAST( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS DECIMAL(18,{SysAmountLength})) AS TaxFree, CAST(0 AS DECIMAL(18,{SysAmountLength})) AS ExportSales, CAST(SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS DECIMAL(18,{SysAmountLength}) ) AS Taxable, CAST(SUM(ISNULL(term.VatAmount, 0)) AS DECIMAL(18,{SysAmountLength}) ) AS VatAmount,0 IsGroup"
				: $@" FORMAT(SUM(pm.LN_Amount),'{SysAmountCommaFormat}') AS TotalAmount, FORMAT( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END),'{SysAmountCommaFormat}') AS TaxFree, FORMAT(0,'{SysAmountCommaFormat}') AS ExportSales, FORMAT(SUM(ISNULL(term.VatAmount, 0)/ 0.13),'{SysAmountCommaFormat}') AS Taxable, FORMAT(SUM(ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') AS VatAmount,0 IsGroup";
			cmdString += $@"
				FROM AMS.SB_Master pm
					 LEFT OUTER JOIN(SELECT vat.SB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM(SELECT pt.SB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID={GetReports.VatTermId[0]}) THEN SUM(pt.Amount)
															WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_ID={GetReports.VatTermId[0]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={GetReports.VatTermId[0]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
										  FROM AMS.SB_Term pt
											   LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_ID
										  WHERE pt.Term_Type<>'BT'
										  GROUP BY pt.SB_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
									 GROUP BY vat.SB_VNo) AS term ON pm.SB_Invoice=term.SB_VNo
					 LEFT OUTER JOIN(SELECT pd.SB_Invoice, SUM(pd.Qty) AS Qty
									 FROM AMS.SB_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
									 GROUP BY pd.SB_Invoice) AS d ON d.SB_Invoice=pm.SB_Invoice
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND (pm.R_Invoice = 0 OR pm.R_Invoice IS NULL)
				GROUP BY  pm.Invoice_Date, pm.Invoice_Miti";
			cmdString += @"
				ORDER BY pm.Invoice_Date ";
		}
		else if (module.Equals("SR"))
		{
			cmdString = $@"
				SELECT pm.Invoice_Date VoucherDate,pm.Invoice_Miti VoucherMiti,'' VoucherNo,'' NoOfBill,0 LedgerId,'Day Total Sales Return' Ledger,'' PanNo,'' Category,CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty,";
			cmdString += ServerVersion < 10
				? $@" CAST(SUM(pm.LN_Amount) AS DECIMAL(18,{SysAmountLength})) AS TotalAmount, CAST( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS DECIMAL(18,{SysAmountLength}) ) AS TaxFree, CAST(0 AS DECIMAL(18,{SysAmountLength})) AS ExportSales, CAST(SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS DECIMAL(18,{SysAmountLength})) AS Taxable, CAST(SUM(ISNULL(term.VatAmount, 0)) AS DECIMAL(18,{SysAmountLength}) ) AS VatAmount,0 IsGroup "
				: $@" FORMAT(SUM(pm.LN_Amount),'{SysAmountCommaFormat}') AS TotalAmount, FORMAT( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END),'{SysAmountCommaFormat}') AS TaxFree, FORMAT(0,'{SysAmountCommaFormat}') AS ExportSales, FORMAT(SUM(ISNULL(term.VatAmount, 0)/ 0.13),'{SysAmountCommaFormat}') AS Taxable, FORMAT(SUM(ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') AS VatAmount,0 IsGroup ";
			cmdString += $@"
				FROM AMS.SR_Master pm
					 LEFT OUTER JOIN(SELECT vat.SR_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM(SELECT pt.SR_VNo, CASE WHEN pt1.Order_No < (SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID ={GetReports.VatTermId[0]}) THEN SUM(pt.Amount) WHEN pt1.ST_Sign = '-' AND pt1.Order_No < (SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_ID ={GetReports.VatTermId[0]}) THEN - SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id ={GetReports.VatTermId[0]}
											THEN SUM(pt.Amount)ELSE 0 END AS VatAmount FROM AMS.SR_Term pt
											   LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id = pt1.ST_ID
										  WHERE pt.Term_Type <> 'BT'
										  GROUP BY pt.SR_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
									 GROUP BY vat.SR_VNo) AS term ON pm.SR_Invoice = term.SR_VNo
					 LEFT OUTER JOIN(SELECT pd.SR_Invoice, SUM(pd.Qty) AS Qty FROM AMS.SR_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID = pd.P_Id
									 GROUP BY pd.SR_Invoice) AS d ON d.SR_Invoice = pm.SR_Invoice
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND(pm.R_Invoice = 0 OR pm.R_Invoice IS NULL)
				GROUP BY  pm.Invoice_Date, pm.Invoice_Miti";
			cmdString += @"
				ORDER BY pm.Invoice_Date ";
		}

		return cmdString;
	}

	private string GetNewSalesVatRegisterSummaryDateWise(string module)
	{
		var cmdString = string.Empty;
		if (module.Equals("SB"))
		{
			cmdString = @"
				SELECT vt.VoucherNo, vt.VoucherDate, vt.VoucherMiti, vt.LedgerId, vt.Ledger, vt.PanNo, vt.Category, CAST(vt.Qty AS DECIMAL(18, 2)) Qty, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TotalAmount ELSE 0 END AS DECIMAL(18,6)), '##,##,##0.00') TotalAmount,0 IsGroup
				FROM(SELECT pm.SB_Invoice VoucherNo, CONVERT(VARCHAR, pm.Invoice_Date, 102) VoucherDate, CONVERT(VARCHAR, RIGHT(Invoice_Miti, 4))+'.'+CONVERT(VARCHAR, RIGHT(LEFT(Invoice_Miti, 5), 2))+'.'+CONVERT(VARCHAR, LEFT(Invoice_Miti, 2)) VoucherMiti, pm.Customer_Id AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category, SUM(d.Qty) Qty, SUM(pm.LN_Amount) AS TotalAmount,
				pm.R_Invoice
					 FROM AMS.SB_Master pm
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_Id=gl.GLID
					 LEFT OUTER JOIN(SELECT pd.SB_Invoice,SUM(pd.Qty) AS Qty
									 FROM AMS.SB_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
									 GROUP BY pd.SB_Invoice) AS d ON d.SB_Invoice=pm.SB_Invoice
				WHERE pm.Invoice_Date BETWEEN '2021-07-16' AND '2022-07-15'
				GROUP BY pm.SB_Invoice,pm.Invoice_Date,pm.Invoice_Miti,pm.Customer_Id, gl.GLName, gl.PanNo,pm.R_Invoice ) AS vt
				ORDER BY AMS.GetNumericValue(vt.VoucherNo) ASC, vt.VoucherDate, vt.VoucherMiti;

				SELECT vat.SB_VNo,SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM(SELECT pt.SB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID=2) THEN SUM(pt.Amount) WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_ID=2) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id=2 THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
										  FROM AMS.SB_Term pt
											   LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_ID
											   left outer join AMS.SB_Master pm on  pm.SB_Invoice = pt.SB_VNo
										  WHERE pt.Term_Type<>'BT' and pm.Invoice_Date BETWEEN '2021-07-16' AND '2022-07-15'
										  GROUP BY pt.SB_VNo,pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
									 GROUP BY vat.SB_VNo; ";
		}
		else if (module.Equals("SR"))
		{
		}

		return cmdString;
	}

	public DataTable GetSalesVatRegisterSummary()
	{
		var cmdString = GetReports.RptMode switch
		{
			"CUSTOMER WISE" => GetSalesVatRegisterSummaryCustomerWise(GetReports.Module),
			//"DATE WISE" when GetReports.Module.Equals("SB") => GetSalesVatRegisterSummaryDateWise(GetReports.Module),
			"DATE WISE" when GetReports.Module.Equals("SB") => GetNewSalesVatRegisterSummaryDateWise(GetReports.Module),
			_ => GetSalesVatRegisterSummaryCustomerWise(GetReports.Module)
		};
		var dtVatRegister = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		var dtReport = GetReports.RptMode switch
		{
			"CUSTOMER WISE" => ReturnSalesVatRegisterSummary(dtVatRegister),
			"DATE WISE" => ReturnSalesVatRegisterSummary(dtVatRegister),
			_ => new DataTable()
		};
		return dtReport;
	}

	//GET SALES VAT REGISTER DETAILS REPORTS FORMAT
	public string GetNewSalesVatRegisterDetailsReportsScript(string module)
	{
		var cmdString = string.Empty;
		if (module is "SB")
		{
			cmdString = @"
				WITH VatRegister AS (SELECT vat.SB_Invoice, SUM(vat.Qty) Qty, SUM(ISNULL(BasicAmount, 0)) BasicAmount, SUM(vat.TaxAmount) TaxAmount, SUM(ISNULL(vat.Discount, 0)) Discount, SUM(vat.Tax_FreeSales) Tax_FreeSales, SUM(vat.TaxableSales) TaxableSales
				FROM(SELECT sd.SB_Invoice, sd.P_Id, sd.Qty, ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0) BasicAmount, ISNULL(sd.T_Amount, 0) TermAmount, ISNULL(sd.N_Amount, 0) NetAmout, ISNULL(v.TaxAmount, 0) TaxAmount, ISNULL(p.PDiscount, 0) Discount, CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.N_Amount+ISNULL(p.PDiscount, 0))END Tax_FreeSales,";
			cmdString += SoftwareModule.Equals("POS")
				? @" CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.N_Amount + ISNULL(p.PDiscount,0) - ISNULL(v.TaxAmount,0))END TaxableSales "
				: @" CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount + ISNULL(p.PDiscount,0))END TaxableSales ";
			cmdString += $@"FROM AMS.SB_Details sd
					   LEFT OUTER JOIN(SELECT SB_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
									   FROM AMS.SB_Term
									   WHERE ST_Id={GetReports.VatTermId[0]} AND Term_Type<>'B'
									   GROUP BY SB_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=v.SNo
					   LEFT OUTER JOIN(SELECT st.SB_VNo, st.SNo, st.Product_Id, SUM(CASE WHEN s1.ST_Sign='+' THEN st.Amount ELSE -st.Amount END) PDiscount
								   FROM AMS.SB_Term st
										LEFT OUTER JOIN AMS.ST_Term s1 ON s1.ST_ID=st.ST_Id
								   WHERE st.Product_Id IS NOT NULL AND st.ST_Id IN(SELECT st1.ST_ID
																				   FROM AMS.ST_Term st1
																				   WHERE st1.Order_No<(SELECT st2.Order_No FROM AMS.ST_Term st2 WHERE st2.ST_ID={GetReports.VatTermId[0]}) AND st.Term_Type = 'BT')
								   GROUP BY st.SB_VNo, st.SNo, st.Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SB_VNo=sd.SB_Invoice AND sd.Invoice_SNo=p.SNo
				WHERE 1=1) vat
				GROUP BY vat.SB_Invoice)
				SELECT v.SB_Invoice VoucherNo, CONVERT(VARCHAR, sm.Invoice_Date, 102) VoucherDate, SUBSTRING(sm.Invoice_Miti, 7, 4)+'.'+SUBSTRING(sm.Invoice_Miti, 4, 2)+'.'+SUBSTRING(sm.Invoice_Miti, 1, 2) VoucherMiti, sm.Customer_Id LedgerId, ISNULL(sm.Party_Name, gl.GLName) Ledger, ISNULL(sm.Vat_No, gl.PanNo) PanNo, 'MIS' Category,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Qty, 0)>0 THEN FORMAT(v.Qty, '{SysQtyCommaFormat}')ELSE '' END Qty,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.BasicAmount, 0)>0 THEN FORMAT(v.BasicAmount, '{SysAmountCommaFormat}')ELSE '' END Amount,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 THEN FORMAT(sm.N_Amount, '{SysAmountCommaFormat}')ELSE '' END TotalAmount,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 AND sm.Invoice_Type='Export' THEN FORMAT(sm.N_Amount, '{SysAmountCommaFormat}')ELSE '' END ExportSales,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Discount, 0)>0 THEN FORMAT(v.Discount, '{SysAmountCommaFormat}')ELSE '' END Discount,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Tax_FreeSales, 0)>0 THEN FORMAT(v.Tax_FreeSales, '{SysAmountCommaFormat}')ELSE '' END TaxFree,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxableSales, 0)>0 THEN FORMAT(v.TaxableSales, '{SysAmountCommaFormat}')ELSE '' END Taxable,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxAmount, 0)>0 THEN FORMAT(v.TaxAmount, '{SysAmountCommaFormat}')ELSE '' END VatAmount, '' ExportCountry, '' ExportVoucherNo, '' ExportMiti, CASE WHEN sm.R_Invoice>0 THEN 12 ELSE 0 END IsGroup, dm.AD_Months, dm.BS_Months
				FROM VatRegister v
					LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice=v.SB_Invoice
					LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
					LEFT OUTER JOIN AMS.DateMiti dm ON sm.Invoice_Date  = dm.AD_Date
				WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" and sm.Customer_Id in ({GetReports.LedgerId}) " : "";
			cmdString += @"
				ORDER BY sm.Invoice_Date, v.SB_Invoice;";
		}
		else if (module is "SR")
		{
			cmdString = @"
				WITH VatRegister AS (SELECT vat.SR_Invoice, SUM(vat.Qty) Qty, SUM(ISNULL(BasicAmount, 0)) BasicAmount, SUM(vat.TaxAmount) TaxAmount, SUM(ISNULL(vat.Discount, 0)) Discount, SUM(vat.Tax_FreeSales) Tax_FreeSales, SUM(vat.TaxableSales) TaxableSales
				FROM(SELECT sd.SR_Invoice, sd.P_Id, sd.Qty, ISNULL(sd.N_Amount, 0)+ISNULL(p.PDiscount, 0) BasicAmount, ISNULL(sd.T_Amount, 0) TermAmount, ISNULL(sd.N_Amount, 0) NetAmout, ISNULL(v.TaxAmount, 0) TaxAmount, ISNULL(p.PDiscount, 0) Discount, CASE WHEN ISNULL(v.TaxAmount, 0)>0 THEN 0 ELSE (sd.N_Amount+ISNULL(p.PDiscount, 0))END Tax_FreeSales,";
			cmdString += SoftwareModule.Equals("POS")
				? @" CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.N_Amount + ISNULL(p.PDiscount,0) - ISNULL(v.TaxAmount,0))END TaxableSales "
				: @" CASE WHEN ISNULL(v.TaxAmount, 0)=0 THEN 0 ELSE (sd.B_Amount + ISNULL(p.PDiscount,0))END TaxableSales ";
			cmdString += $@"FROM AMS.SR_Details sd
					   LEFT OUTER JOIN(SELECT SR_VNo, SNo, Product_Id, SUM(Amount) TaxAmount
									   FROM AMS.SR_Term
									   WHERE ST_Id={GetReports.VatTermId[0]} AND Term_Type<>'B'
									   GROUP BY SR_VNo, SNo, Product_Id) AS v ON v.Product_Id=sd.P_Id AND v.SR_VNo=sd.SR_Invoice AND sd.Invoice_SNo=v.SNo
					   LEFT OUTER JOIN(SELECT st.SR_VNo, st.SNo, st.Product_Id, SUM(CASE WHEN s1.ST_Sign='+' THEN st.Amount ELSE -st.Amount END) PDiscount
								   FROM AMS.SR_Term st
										LEFT OUTER JOIN AMS.ST_Term s1 ON s1.ST_ID=st.ST_Id
								   WHERE st.Product_Id IS NOT NULL AND st.ST_Id IN(SELECT st1.ST_ID
																				   FROM AMS.ST_Term st1
																				   WHERE st1.Order_No<(SELECT st2.Order_No FROM AMS.ST_Term st2 WHERE st2.ST_ID={GetReports.VatTermId[0]}) AND st.Term_Type = 'BT')
								   GROUP BY st.SR_VNo, st.SNo, st.Product_Id) AS p ON p.Product_Id=sd.P_Id AND p.SR_VNo=sd.SR_Invoice AND sd.Invoice_SNo=p.SNo
				WHERE 1=1) vat
				GROUP BY vat.SR_Invoice)
				SELECT v.SR_Invoice VoucherNo, CONVERT(VARCHAR, sm.Invoice_Date, 102) VoucherDate, SUBSTRING(sm.Invoice_Miti, 7, 4)+'.'+SUBSTRING(sm.Invoice_Miti, 4, 2)+'.'+SUBSTRING(sm.Invoice_Miti, 1, 2) VoucherMiti, sm.Customer_Id LedgerId, ISNULL(sm.Party_Name, gl.GLName) Ledger, ISNULL(sm.Vat_No, gl.PanNo) PanNo, 'MIS' Category,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Qty, 0)>0 THEN FORMAT(v.Qty, '{SysQtyCommaFormat}')ELSE '' END Qty,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.BasicAmount, 0)>0 THEN FORMAT(v.BasicAmount, '{SysAmountCommaFormat}')ELSE '' END Amount,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 THEN FORMAT(sm.N_Amount, '{SysAmountCommaFormat}')ELSE '' END TotalAmount,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(sm.N_Amount, 0)>0 AND sm.Invoice_Type='Export' THEN FORMAT(sm.N_Amount, '{SysAmountCommaFormat}')ELSE '' END ExportSales,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Discount, 0)>0 THEN FORMAT(v.Discount, '{SysAmountCommaFormat}')ELSE '' END Discount,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.Tax_FreeSales, 0)>0 THEN FORMAT(v.Tax_FreeSales, '{SysAmountCommaFormat}')ELSE '' END TaxFree,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxableSales, 0)>0 THEN FORMAT(v.TaxableSales, '{SysAmountCommaFormat}')ELSE '' END Taxable,
				CASE WHEN sm.R_Invoice=0 AND ISNULL(v.TaxAmount, 0)>0 THEN FORMAT(v.TaxAmount, '{SysAmountCommaFormat}')ELSE '' END VatAmount, '' ExportCountry, '' ExportVoucherNo, '' ExportMiti, CASE WHEN sm.R_Invoice>0 THEN 12 ELSE 0 END IsGroup, dm.AD_Months, dm.BS_Months
				FROM VatRegister v
					LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice=v.SR_Invoice
					LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
					LEFT OUTER JOIN AMS.DateMiti dm ON sm.Invoice_Date  = dm.AD_Date
				WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" and sm.Customer_Id in ({GetReports.LedgerId}) " : "";
			cmdString += @"
				ORDER BY sm.Invoice_Date, v.SR_Invoice;";
		}

		return cmdString;
	}

	public DataTable ReturnSalesVatRegisterSummary(DataTable dtVatRegister)
	{
		DataRow dataRow;
		var dtReport = dtVatRegister.Copy();

		dataRow = dtReport.NewRow();
		dataRow["Ledger"] = GetReports.Module switch
		{
			"PB" when GetReports.IncludeReturn => "TOTAL PURCHASE AMOUNT >>",
			"SB" when GetReports.IncludeReturn => "TOTAL SALES AMOUNT >>",
			_ => "GRAND TOTAL >>"
		};

		var salesQty = dtReport.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
		var salesAmount = dtReport.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
		var salesExempted = dtReport.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
		var salesExport = dtReport.AsEnumerable().Sum(x => x["ExportSales"].GetDecimal());
		var salesTaxable = dtReport.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
		var salesVat = dtReport.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());

		if (ServerVersion < 10)
		{
			dataRow["Qty"] = salesQty;
			dataRow["TotalAmount"] = salesAmount;
			dataRow["TaxFree"] = salesExempted;
			dataRow["ExportSales"] = salesExport;
			dataRow["Taxable"] = salesTaxable;
			dataRow["VatAmount"] = salesVat;
		}
		else
		{
			dataRow["Qty"] = salesQty.GetDecimalComma();
			dataRow["TotalAmount"] = salesAmount.GetDecimalComma();
			dataRow["TaxFree"] = salesExempted.GetDecimalComma();
			dataRow["ExportSales"] = salesExport.GetDecimalComma();
			dataRow["Taxable"] = salesTaxable.GetDecimalComma();
			dataRow["VatAmount"] = salesVat.GetDecimalComma();
		}

		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

		if (GetReports.IncludeReturn)
		{
			var cmdString = GetReports.RptMode.Equals("DATE WISE")
				? GetPurchaseVatRegisterDayWiseReportsScript("PR")
				: GetPurchaseVatRegisterVendorWiseReportsScript("PR");

			var dtReturn = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
			if (dtReturn.Rows.Count > 0)
			{
				dtReturn.AsEnumerable().Take(dtReturn.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

				dataRow = dtReport.NewRow();

				dataRow["Ledger"] = GetReports.Module switch
				{
					"PB" when GetReports.IncludeReturn => "TOTAL PURCHASE RETURN AMOUNT >>",
					_ => "TOTAL SALES RETURN AMOUNT >>"
				};

				var returnQty = dtReturn.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
				var returnAmount = dtReturn.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
				var returnExempted = dtReturn.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
				var returnExport = dtReturn.AsEnumerable().Sum(x => x["ExportSales"].GetDecimal());
				var returnTaxable = dtReturn.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
				var returnVat = dtReturn.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());

				if (ServerVersion < 10)
				{
					dataRow["Qty"] = returnQty;
					dataRow["TotalAmount"] = returnAmount;
					dataRow["TaxFree"] = returnExempted;
					dataRow["ExportSales"] = returnExport;
					dataRow["Taxable"] = returnTaxable;
					dataRow["VatAmount"] = returnVat;
				}
				else
				{
					dataRow["Qty"] = returnQty.GetDecimalComma();
					dataRow["TotalAmount"] = returnAmount.GetDecimalComma();
					dataRow["TaxFree"] = returnExempted.GetDecimalComma();
					dataRow["ExportSales"] = returnExport.GetDecimalComma();
					dataRow["Taxable"] = returnTaxable.GetDecimalComma();
					dataRow["VatAmount"] = returnVat.GetDecimalComma();
				}

				dataRow["IsGroup"] = 99;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

				if (salesAmount > 0 && returnAmount > 0)
				{
					dataRow = dtReport.NewRow();
					dataRow["Ledger"] = GetReports.Module switch
					{
						"PB" when GetReports.IncludeReturn => "TOTAL NET PURCHASE AMOUNT",
						_ => "TOTAL NET SALES AMOUNT >>"
					};
					if (ServerVersion < 10)
					{
						dataRow["Qty"] = salesQty - returnQty;
						dataRow["TotalAmount"] = salesAmount - returnAmount;
						dataRow["TaxFree"] = salesExempted - returnExempted;
						dataRow["ExportSales"] = salesExport - returnExport;
						dataRow["Taxable"] = salesTaxable - returnTaxable;
						dataRow["VatAmount"] = salesVat - returnVat;
					}
					else
					{
						dataRow["Qty"] = (salesQty - returnQty).GetDecimalComma();
						dataRow["TotalAmount"] = (salesAmount - returnAmount).GetDecimalComma();
						dataRow["TaxFree"] = (salesExempted - returnExempted).GetDecimalComma();
						dataRow["ExportSales"] = (salesExport - returnExport).GetDecimalComma();
						dataRow["Taxable"] = (salesTaxable - returnTaxable).GetDecimalComma();
						dataRow["VatAmount"] = (salesVat - returnVat).GetDecimalComma();
					}

					dataRow["IsGroup"] = 99;
					dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
				}
			}
		}

		return dtReport;
	}

	public DataTable GetSalesVatRegisterDateWiseSummary(DataSet dsRegister)
	{
		try
		{
			var dtReport = GetSalesVatRegisterDateWiseSummary(dsRegister);
			return dtReport;
		}
		catch (Exception ex)
		{
			ex.ToNonQueryErrorResult(ex.StackTrace);
			return new DataTable();
		}
	}

	public DataTable GetSalesVatRegisterVoucherWise()
	{
		try
		{
			var cmdString = GetNewSalesVatRegisterDetailsReportsScript(GetReports.Module);
			var dsRegister = SqlExtensions.ExecuteDataSet(cmdString);
			var dtReport = GetSalesVatRegisterDateWiseSummary(dsRegister);
			return dtReport;
		}
		catch (Exception ex)
		{
			ex.ToNonQueryErrorResult(ex.StackTrace);
			return new DataTable();
		}
	}

	public DataTable GetSalesVatRegisterDateWiseReport()
	{
		var cmdString = GetNewSalesVatRegisterDetailsReportsScript(GetReports.Module);
		var dtReport = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		if (dtReport.Rows.Count is 0)
		{
			return new DataTable();
		}
		var existingRows = dtReport.AsEnumerable().Where(row => row["IsGroup"].GetInt() == 0);
		if (existingRows.Count() > 0)
		{
			var dtGrandTotal = dtReport.Select("IsGroup= 0").CopyToDataTable();
			var dataRow = dtReport.NewRow();
			dataRow["Ledger"] = GetReports.IncludeReturn ? "TOTAL SALES AMOUNT" : "TOTAL AMOUNT =>";

			var salesQty = dtGrandTotal.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
			var salesAmount = dtGrandTotal.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
			var salesExempted = dtGrandTotal.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
			var salesExport = dtGrandTotal.AsEnumerable().Sum(x => x["ExportSales"].GetDecimal());
			var salesTaxable = dtGrandTotal.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
			var salesVat = dtGrandTotal.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());

			dataRow["Qty"] = salesQty.GetDecimalComma();
			dataRow["TotalAmount"] = salesAmount.GetDecimalComma();
			dataRow["TaxFree"] = salesExempted.GetDecimalComma();
			dataRow["ExportSales"] = salesExport.GetDecimalComma();
			dataRow["Taxable"] = salesTaxable.GetDecimalComma();
			dataRow["VatAmount"] = salesVat.GetDecimalComma();
			dataRow["IsGroup"] = 99;
			dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

			if (GetReports.IncludeReturn)
			{
				cmdString = GetNewSalesVatRegisterDetailsReportsScript("SR");
				var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
				if (result.Rows.Count > 0)
				{
					result.AsEnumerable().Take(result.Rows.Count)
						.CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

					dataRow = dtReport.NewRow();
					dataRow["Ledger"] = "TOTAL RETURM AMOUNT =>";

					var returnQty = result.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
					var returnAmount = result.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
					var returnExempted = result.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
					var returnExport = result.AsEnumerable().Sum(x => x["ExportSales"].GetDecimal());
					var returnTaxable = result.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
					var returnVat = result.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());

					dataRow["Qty"] = returnQty.GetDecimalComma();
					dataRow["TotalAmount"] = returnAmount.GetDecimalComma();
					dataRow["TaxFree"] = returnExempted.GetDecimalComma();
					dataRow["ExportSales"] = returnExport.GetDecimalComma();
					dataRow["Taxable"] = returnTaxable.GetDecimalComma();
					dataRow["VatAmount"] = returnVat.GetDecimalComma();
					dataRow["IsGroup"] = 99;
					dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

					if (salesAmount > 0 && returnAmount > 0)
					{
						dataRow = dtReport.NewRow();
						dataRow["Ledger"] = "TOTAL NET SALES AMOUNT >>";
						dataRow["Qty"] = (salesQty - returnQty).GetDecimalComma();
						dataRow["TotalAmount"] = (salesAmount - returnAmount).GetDecimalComma();
						dataRow["TaxFree"] = (salesExempted - returnExempted).GetDecimalComma();
						dataRow["ExportSales"] = (salesExport - returnExport).GetDecimalComma();
						dataRow["Taxable"] = (salesTaxable - returnTaxable).GetDecimalComma();
						dataRow["VatAmount"] = (salesVat - returnVat).GetDecimalComma();
						dataRow["IsGroup"] = 99;
						dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
					}
				}
			}
		}
		else
		{
			var dataRow = dtReport.NewRow();
			dataRow["Ledger"] = GetReports.IncludeReturn ? "TOTAL SALES AMOUNT" : "TOTAL AMOUNT =>";
			dataRow["IsGroup"] = 99;
			dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
		}

		return dtReport;
	}

	public DataTable GetSalesVatRegisterReportMonthly()
	{
		var dtReport = new DataTable();
		var cmdString = GetNewSalesVatRegisterDetailsReportsScript("SB");
		var dtVatRegister = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];

		dtReport = dtVatRegister.Clone();
		if (dtVatRegister.RowsCount() > 0)
		{
			var dtLedgerGroup = dtVatRegister.AsEnumerable().GroupBy(r => new
			{
				ledger = GetReports.IsDate ? r.Field<string>("AD_Months") : r.Field<string>("BS_Months")
			}).Select(g => g.First()).OrderBy(g => g.Field<string>("VoucherDate").GetDateTime()).CopyToDataTable();
			foreach (DataRow row in dtLedgerGroup.Rows)
			{
				var months = GetReports.IsDate ? row["AD_Months"].ToString() : row["BS_Months"].ToString();

				var dataRow = dtReport.NewRow();
				dataRow["Ledger"] = months.GetUpper();
				dataRow["IsGroup"] = 1;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

				var dtReturn = dtVatRegister.Select(GetReports.IsDate ? $" AD_Months='{months}'" : $" BS_Months='{months}'").CopyToDataTable();
				dtReturn.AsEnumerable().Take(dtReturn.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

				dataRow = dtReport.NewRow();
				dataRow["Ledger"] = $"TOTAL AMOUNT [{months.GetUpper()}] =>";

				var salesQty = dtReturn.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
				var salesAmount = dtReturn.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
				var salesExempted = dtReturn.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
				var salesExport = dtReturn.AsEnumerable().Sum(x => x["ExportSales"].GetDecimal());
				var salesTaxable = dtReturn.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
				var salesVat = dtReturn.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());

				dataRow["Qty"] = salesQty.GetDecimalComma();
				dataRow["TotalAmount"] = salesAmount.GetDecimalComma();
				dataRow["TaxFree"] = salesExempted.GetDecimalComma();
				dataRow["ExportSales"] = salesExport.GetDecimalComma();
				dataRow["Taxable"] = salesTaxable.GetDecimalComma();
				dataRow["VatAmount"] = salesVat.GetDecimalComma();
				dataRow["IsGroup"] = 99;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
			}

		}
		return ReturnSalesVatRegisterSummary(dtReport);
	}

	public DataTable GetSalesVatRegisterCustomer()
	{
		var dtReport = new DataTable();
		var cmdString = GetNewSalesVatRegisterDetailsReportsScript("SB");
		var dtVatRegister = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		dtReport = dtVatRegister.Clone();
		if (dtVatRegister.Rows.Count > 0)
		{
			var dtLedgerGroup = dtVatRegister.AsEnumerable().GroupBy(r => new
			{
				ledger = r.Field<long>("LedgerId")
			}).Select(g => g.First()).OrderBy(g => g.Field<string>("Ledger")).CopyToDataTable();

			foreach (DataRow row in dtLedgerGroup.Rows)
			{
				var months = row["Ledger"].ToString();

				var dataRow = dtReport.NewRow();
				dataRow["LedgerId"] = row["LedgerId"];
				dataRow["Ledger"] = months;
				dataRow["IsGroup"] = 1;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

				var dtReturn = dtVatRegister.Select($"Ledger='{months}'").CopyToDataTable();
				dtReturn.AsEnumerable().Take(dtReturn.Rows.Count).CopyToDataTable(dtReport, LoadOption.OverwriteChanges);


				dataRow = dtReport.NewRow();
				dataRow["Ledger"] = $"TOTAL AMOUNT [{months}] =>";

				var salesQty = dtReturn.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
				var salesAmount = dtReturn.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
				var salesExempted = dtReturn.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
				var salesExport = dtReturn.AsEnumerable().Sum(x => x["ExportSales"].GetDecimal());
				var salesTaxable = dtReturn.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
				var salesVat = dtReturn.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());

				dataRow["Qty"] = salesQty.GetDecimalComma();
				dataRow["TotalAmount"] = salesAmount.GetDecimalComma();
				dataRow["TaxFree"] = salesExempted.GetDecimalComma();
				dataRow["ExportSales"] = salesExport.GetDecimalComma();
				dataRow["Taxable"] = salesTaxable.GetDecimalComma();
				dataRow["VatAmount"] = salesVat.GetDecimalComma();
				dataRow["IsGroup"] = 99;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

			}
			return dtReport;
		}

		else return dtReport;

	}

	public DataTable GetSalesVatRegisterProductWise()
	{
		var dtReport = new DataTable();
		var cmdString = GetNewSalesVatRegisterDetailsReportsScript("SB");
		var dtVatRegister = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		return ReturnSalesVatRegisterSummary(dtVatRegister);
	}

	#endregion *||* --------------- SALES VAT REGISTER REPORT --------------- *||*


	// PURCHASE VAT REGISTER
	#region **---------- PURCHASE VAT REGISTER ----------**

	public string GetPurchaseVatRegisterDayWiseReportsScript(string module)
	{
		var cmdString = string.Empty;
		if (module.Equals("PB"))
		{
			cmdString = @"
				SELECT vt.VoucherNo, vt.RefVno, CONVERT(VARCHAR, vt.VoucherDate, 102) VoucherDate, vt.VoucherMiti, vt.LedgerId, vt.Ledger, vt.PanNo, vt.Category,";
			cmdString += ServerVersion < 10
				? $@" CAST(vt.Qty AS DECIMAL(18, {SysQtyLength})) Qty, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TotalAmount ELSE 0 END AS DECIMAL(18,{SysAmountLength})) TotalAmount, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TaxFree ELSE 0 END AS DECIMAL(18,{SysAmountLength})) TaxFree, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, {SysAmountLength})) ImportTaxable, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, {SysAmountLength})) ImportVatAmount, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('Local', 'Normal') THEN vt.Taxable ELSE 0 END AS DECIMAL(18, {SysAmountLength})) Taxable, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, {SysAmountLength})) VatAmount, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, {SysAmountLength})) CapitalTaxable, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, {SysAmountLength})) CapitalVatAmount,"
				: $@" CAST(vt.Qty AS DECIMAL(18, {SysQtyLength})) Qty, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TotalAmount ELSE 0 END AS DECIMAL(18,6)), '{SysAmountCommaFormat}') TotalAmount, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TaxFree ELSE 0 END AS DECIMAL(18,6)), '{SysAmountCommaFormat}') TaxFree, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') ImportTaxable, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') ImportVatAmount, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.Taxable ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Taxable, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') VatAmount, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') CapitalTaxable, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') CapitalVatAmount,";
			cmdString += $@" vt.EntryType, vt.R_Invoice IsReverse, 0 IsGroup
				FROM(SELECT pm.PB_Invoice VoucherNo, pm.PB_Vno RefVno, pm.Invoice_Date VoucherDate, CONVERT(VARCHAR, RIGHT(Invoice_Miti, 4))+'.'+CONVERT(VARCHAR, RIGHT(LEFT(Invoice_Miti, 5), 2))+'.'+CONVERT(VARCHAR, LEFT(Invoice_Miti, 2)) VoucherMiti, pm.Vendor_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category, SUM(d.Qty) Qty, SUM(pm.LN_Amount) AS TotalAmount, SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS TaxFree, 0 AS ExportSales, SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS Taxable, SUM(ISNULL(term.VatAmount, 0)) AS VatAmount, pm.R_Invoice, pm.Invoice_Type EntryType
					 FROM AMS.PB_Master pm
						  LEFT OUTER JOIN(SELECT vat.PB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
										  FROM(SELECT pt.PB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.PT_Term pt3 WHERE pt3.PT_ID={GetReports.VatTermId[1]}) THEN SUM(pt.Amount) WHEN pt1.PT_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.PT_Term pt2 WHERE pt2.PT_ID={GetReports.VatTermId[1]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.PT_Id={GetReports.VatTermId[1]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
											   FROM AMS.PB_Term pt
													LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_ID
											   WHERE pt.Term_Type<>'BT'
											   GROUP BY pt.PB_VNo, pt.PT_Id, pt1.PT_Sign, pt1.Order_No) AS vat
										  GROUP BY vat.PB_VNo) AS term ON pm.PB_Invoice=term.PB_VNo
						  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
						  LEFT OUTER JOIN(SELECT pd.PB_Invoice, SUM(pd.Qty) AS Qty
										  FROM AMS.PB_Details AS pd
											   LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
										  GROUP BY pd.PB_Invoice) AS d ON d.PB_Invoice=pm.PB_Invoice
					 WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $"\n AND pm.Vendor_ID = '{GetReports.LedgerId}' " : "";
			cmdString += @"
					 GROUP BY pm.PB_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Vendor_ID, gl.GLName, gl.PanNo, pm.R_Invoice, pm.PB_Vno, pm.Invoice_Type) AS vt
				ORDER BY VoucherDate ASC, AMS.GetNumericValue(vt.VoucherNo) ASC, vt.VoucherMiti; ";
		}
		else if (module.Equals("PR"))
		{
			cmdString = @"
				SELECT vt.VoucherNo, vt.RefVno, CONVERT(VARCHAR, vt.VoucherDate, 102) VoucherDate, vt.VoucherMiti, vt.LedgerId, vt.Ledger, vt.PanNo, vt.Category,";
			cmdString += ServerVersion < 10
				? $@" CAST(vt.Qty AS DECIMAL(18, {SysQtyLength})) Qty, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TotalAmount ELSE 0 END AS DECIMAL(18,{SysAmountLength})) TotalAmount, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TaxFree ELSE 0 END AS DECIMAL(18,{SysAmountLength})) TaxFree, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, {SysAmountLength})) ImportTaxable, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, {SysAmountLength})) ImportVatAmount, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.Taxable ELSE 0 END AS DECIMAL(18, {SysAmountLength})) Taxable, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, {SysAmountLength})) VatAmount, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, {SysAmountLength})) CapitalTaxable, CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, {SysAmountLength})) CapitalVatAmount,"
				: $@" CAST(vt.Qty AS DECIMAL(18, {SysQtyLength})) Qty, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TotalAmount ELSE 0 END AS DECIMAL(18,6)), '{SysAmountCommaFormat}') TotalAmount, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 THEN vt.TaxFree ELSE 0 END AS DECIMAL(18,6)), '{SysAmountCommaFormat}') TaxFree, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') ImportTaxable, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Import' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') ImportVatAmount, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.Taxable ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') Taxable, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType IN ('LOCAL', 'NORMAL','POSTED') THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') VatAmount, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.Taxable ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') CapitalTaxable, FORMAT(CAST(CASE WHEN ISNULL(vt.R_Invoice, 0)<=0 AND vt.EntryType='Assets' THEN vt.VatAmount ELSE 0 END AS DECIMAL(18, 6)), '{SysAmountCommaFormat}') CapitalVatAmount,";
			cmdString += $@" vt.EntryType, vt.R_Invoice IsReverse, 0 IsGroup
				FROM(SELECT pm.PR_Invoice VoucherNo, pm.PB_Invoice RefVno, pm.Invoice_Date VoucherDate, CONVERT(VARCHAR, RIGHT(Invoice_Miti, 4))+'.'+CONVERT(VARCHAR, RIGHT(LEFT(Invoice_Miti, 5), 2))+'.'+CONVERT(VARCHAR, LEFT(Invoice_Miti, 2)) VoucherMiti, pm.Vendor_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category, SUM(d.Qty) Qty, SUM(pm.LN_Amount) AS TotalAmount, SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS TaxFree, 0 AS ExportSales, SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS Taxable, SUM(ISNULL(term.VatAmount, 0)) AS VatAmount, pm.R_Invoice, pm.Invoice_Type EntryType
					 FROM AMS.PR_Master pm
						  LEFT OUTER JOIN(SELECT vat.PR_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
										  FROM(SELECT pt.PR_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID={GetReports.VatTermId[1]}) THEN SUM(pt.Amount) WHEN pt1.PT_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.PT_Term pt2 WHERE pt2.PT_ID={GetReports.VatTermId[1]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.PT_Id={GetReports.VatTermId[1]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
											   FROM AMS.PR_Term pt
													LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_ID
											   WHERE pt.Term_Type<>'BT'
											   GROUP BY pt.PR_VNo, pt.PT_Id, pt1.PT_Sign, pt1.Order_No) AS vat
										  GROUP BY vat.PR_VNo) AS term ON pm.PR_Invoice=term.PR_VNo
						  LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
						  LEFT OUTER JOIN(SELECT pd.PR_Invoice, SUM(pd.Qty) AS Qty
										  FROM AMS.PR_Details AS pd
											   LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
										  GROUP BY pd.PR_Invoice) AS d ON d.PR_Invoice=pm.PR_Invoice
					 WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'RETURN' ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $"\n AND pm.Vendor_ID='{GetReports.LedgerId}' " : "";
			cmdString += @"
					 GROUP BY pm.PR_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Vendor_ID, gl.GLName, gl.PanNo, pm.R_Invoice, pm.PB_Invoice, pm.Invoice_Type) AS vt
				ORDER BY VoucherDate ASC, AMS.GetNumericValue(vt.VoucherNo) ASC, vt.VoucherMiti";
		}

		return cmdString;
	}

	public string GetPurchaseVatRegisterVendorWiseReportsScript(string module)
	{
		var cmdString = new StringBuilder();
		if (module.Equals("PB"))
		{
			cmdString.Append(@"
				SELECT ROW_NUMBER() OVER( ORDER BY gl.GLName) Sno, pm.Vendor_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category, ");
			cmdString.Append(ServerVersion < 10
				? $@" CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty, CAST(SUM(pm.LN_Amount) AS DECIMAL(18,{SysAmountLength})) AS TotalAmount, CAST(SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS DECIMAL(18,{SysAmountLength}) ) AS TaxFree, CAST(0 AS DECIMAL(18,{SysAmountLength}) ) AS ExportSales, CAST(SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS DECIMAL(18,{SysAmountLength})) AS Taxable, CAST(SUM(ISNULL(term.VatAmount, 0)) AS DECIMAL(18,{SysAmountLength})) AS VatAmount,"
				: $@" CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty, FORMAT(SUM(pm.LN_Amount),'{SysAmountCommaFormat}') AS TotalAmount, FORMAT( SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END),'{SysAmountCommaFormat}') AS TaxFree, FORMAT(0,'{SysAmountCommaFormat}') AS ExportSales, FORMAT(SUM(ISNULL(term.VatAmount, 0)/ 0.13),'{SysAmountCommaFormat}') AS Taxable, FORMAT(SUM(ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') AS VatAmount,");
			cmdString.Append($@" 0 IsGroup
				FROM AMS.PB_Master pm
					 LEFT OUTER JOIN(SELECT vat.PB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM(SELECT pt.PB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID={GetReports.VatTermId[1]}) THEN SUM(pt.Amount) WHEN pt1.PT_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_ID={GetReports.VatTermId[1]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.PT_Id={GetReports.VatTermId[1]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
										  FROM AMS.PB_Term pt
											   LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_ID
										  WHERE pt.Term_Type<>'BT'
										  GROUP BY pt.PB_VNo, pt.PT_Id, pt1.PT_Sign, pt1.Order_No) AS vat
									 GROUP BY vat.PB_VNo) AS term ON pm.PB_Invoice=term.PB_VNo
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
					 LEFT OUTER JOIN(SELECT pd.PB_Invoice, SUM(pd.Qty) AS Qty
									 FROM AMS.PB_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
									 GROUP BY pd.PB_Invoice) AS d ON d.PB_Invoice=pm.PB_Invoice
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND (pm.R_Invoice = 0 OR pm.R_Invoice IS NULL)
				GROUP BY pm.Vendor_ID, gl.GLName, gl.PanNo ");
			if (GetReports.FilterValue.GetDecimal() > 0)
				cmdString.Append($@"
				HAVING SUM(pm.LN_Amount) >= {GetReports.FilterValue}");
			cmdString.Append(@"
				ORDER BY gl.GLName  ");
		}
		else if (module.Equals("PR"))
		{
			cmdString.Append(@"
				SELECT ROW_NUMBER() OVER( ORDER BY gl.GLName) Sno, pm.Vendor_ID AS LedgerId, gl.GLName AS Ledger, gl.PanNo, '' Category, ");
			cmdString.Append(ServerVersion < 10
				? $@" CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty, CAST(SUM(pm.LN_Amount) AS DECIMAL(18,{SysQtyLength})) AS TotalAmount, CAST(SUM(CASE WHEN (pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END) AS DECIMAL(18,{SysAmountLength})) AS TaxFree, CAST(0 AS DECIMAL(18,{SysAmountLength})) AS ExportSales, CAST(SUM(ISNULL(term.VatAmount, 0)/ 0.13) AS DECIMAL(18,{SysAmountLength})) AS Taxable, CAST(SUM(ISNULL(term.VatAmount, 0)) AS DECIMAL(18,{SysAmountLength})) AS VatAmount"
				: $@" CAST(SUM(d.Qty) AS DECIMAL(18,{SysQtyLength})) Qty, FORMAT(SUM(pm.LN_Amount),'{SysAmountCommaFormat}') AS TotalAmount, CASE WHEN FORMAT(SUM((pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') ELSE '' END AS TaxFree, '' AS ExportSales, FORMAT(SUM(ISNULL(term.VatAmount, 0)/ 0.13),'{SysAmountCommaFormat}') AS Taxable, FORMAT(SUM(ISNULL(term.VatAmount, 0)),'{SysAmountCommaFormat}') AS VatAmount");
			cmdString.Append($@" ,0 IsGroup
				FROM AMS.PR_Master pm
					 LEFT OUTER JOIN(SELECT vat.PR_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
									 FROM(SELECT pt.PR_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_ID={GetReports.VatTermId[1]}) THEN SUM(pt.Amount) WHEN pt1.PT_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_ID={GetReports.VatTermId[1]}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.PT_Id={GetReports.VatTermId[1]} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
										  FROM AMS.PR_Term pt
											   LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_ID
										  WHERE pt.Term_Type<>'BT'
										  GROUP BY pt.PR_VNo, pt.PT_Id, pt1.PT_Sign, pt1.Order_No) AS vat
									 GROUP BY vat.PR_VNo) AS term ON pm.PR_Invoice=term.PR_VNo
					 LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
					 LEFT OUTER JOIN(SELECT pd.PR_Invoice, SUM(pd.Qty) AS Qty
									 FROM AMS.PR_Details AS pd
										  LEFT OUTER JOIN AMS.Product AS p ON p.PID=pd.P_Id
									 GROUP BY pd.PR_Invoice) AS d ON d.PR_Invoice=pm.PR_Invoice
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND (pm.R_Invoice = 0 OR pm.R_Invoice IS NULL)
				GROUP BY pm.Vendor_ID, gl.GLName, gl.PanNo  ");
			if (GetReports.FilterValue.GetDecimal() > 0)
				cmdString.Append($@"
				HAVING SUM(pm.LN_Amount) > {GetReports.FilterValue}");
			cmdString.Append(@"
				ORDER BY gl.GLName ");
		}

		return cmdString.ToString();
	}

	private DataTable ReturnPurchaseVatRegisterNormal(DataTable dtVatRegister)
	{
		var dtReport = dtVatRegister.Copy();

		var dtGrandTotal = dtReport.Select("IsGroup= 0").CopyToDataTable();
		var dataRow = dtReport.NewRow();
		dataRow["Ledger"] = GetReports.IncludeReturn ? "TOTAL PURCHASE AMOUNT=>" : "TOTAL AMOUNT =>";

		var purchaseQty = dtGrandTotal.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
		var purchaseAmount = dtGrandTotal.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
		var purchaseExempted = dtGrandTotal.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
		var purchaseImport = dtGrandTotal.AsEnumerable().Sum(x => x["ImportTaxable"].GetDecimal());
		var purchaseImportVat = dtGrandTotal.AsEnumerable().Sum(x => x["ImportVatAmount"].GetDecimal());
		var purchaseTaxable = dtGrandTotal.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
		var purchaseVat = dtGrandTotal.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());
		var purchaseCapital = dtGrandTotal.AsEnumerable().Sum(x => x["CapitalTaxable"].GetDecimal());
		var purchaseCapitaVat = dtGrandTotal.AsEnumerable().Sum(x => x["CapitalVatAmount"].GetDecimal());

		if (ServerVersion < 10)
		{
			dataRow["Qty"] = purchaseQty;
			dataRow["TotalAmount"] = purchaseAmount;
			dataRow["TaxFree"] = purchaseExempted;
			dataRow["ImportTaxable"] = purchaseImport;
			dataRow["ImportVatAmount"] = purchaseImportVat;
			dataRow["Taxable"] = purchaseTaxable;
			dataRow["VatAmount"] = purchaseVat;
			dataRow["CapitalTaxable"] = purchaseCapital;
			dataRow["CapitalVatAmount"] = purchaseCapitaVat;
		}
		else
		{
			dataRow["Qty"] = purchaseQty.GetDecimalComma();
			dataRow["TotalAmount"] = purchaseAmount.GetDecimalComma();
			dataRow["TaxFree"] = purchaseExempted.GetDecimalComma();
			dataRow["ImportTaxable"] = purchaseImport.GetDecimalComma();
			dataRow["ImportVatAmount"] = purchaseImportVat.GetDecimalComma();
			dataRow["Taxable"] = purchaseTaxable.GetDecimalComma();
			dataRow["VatAmount"] = purchaseVat.GetDecimalComma();
			dataRow["CapitalTaxable"] = purchaseCapital.GetDecimalComma();
			dataRow["CapitalVatAmount"] = purchaseCapitaVat.GetDecimalComma();
		}

		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

		if (GetReports.IncludeReturn)
		{
			var cmdString = GetPurchaseVatRegisterDayWiseReportsScript("PR");
			var dtReturn = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
			if (dtReturn.Rows.Count > 0)
			{
				dtReturn.AsEnumerable().Take(dtReturn.Rows.Count)
					.CopyToDataTable(dtReport, LoadOption.OverwriteChanges);

				dataRow = dtReport.NewRow();
				dataRow["Ledger"] = "TOTAL PURCHASE RETURN =>";

				var returnQty = dtReturn.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
				var returnAmount = dtReturn.AsEnumerable().Sum(x => x["TotalAmount"].GetDecimal());
				var returnExempted = dtReturn.AsEnumerable().Sum(x => x["TaxFree"].GetDecimal());
				var returnImport = dtReturn.AsEnumerable().Sum(x => x["ImportTaxable"].GetDecimal());
				var returnImportVat = dtReturn.AsEnumerable().Sum(x => x["ImportVatAmount"].GetDecimal());
				var returnTaxable = dtReturn.AsEnumerable().Sum(x => x["Taxable"].GetDecimal());
				var returnVat = dtReturn.AsEnumerable().Sum(x => x["VatAmount"].GetDecimal());
				var returnCapital = dtReturn.AsEnumerable().Sum(x => x["CapitalTaxable"].GetDecimal());
				var returnCapitaVat = dtReturn.AsEnumerable().Sum(x => x["CapitalVatAmount"].GetDecimal());

				if (ServerVersion < 10)
				{
					dataRow["Qty"] = returnQty;
					dataRow["TotalAmount"] = returnAmount;
					dataRow["TaxFree"] = returnExempted;
					dataRow["ImportTaxable"] = returnImport;
					dataRow["ImportVatAmount"] = returnImportVat;
					dataRow["Taxable"] = returnTaxable;
					dataRow["VatAmount"] = returnVat;
					dataRow["CapitalTaxable"] = returnCapital;
					dataRow["CapitalVatAmount"] = returnCapitaVat;
				}
				else
				{
					dataRow["Qty"] = returnQty.GetDecimalComma();
					dataRow["TotalAmount"] = returnAmount.GetDecimalComma();
					dataRow["TaxFree"] = returnExempted.GetDecimalComma();
					dataRow["ImportTaxable"] = returnImport.GetDecimalComma();
					dataRow["ImportVatAmount"] = returnImportVat.GetDecimalComma();
					dataRow["Taxable"] = returnTaxable.GetDecimalComma();
					dataRow["VatAmount"] = returnVat.GetDecimalComma();
					dataRow["CapitalTaxable"] = returnCapital.GetDecimalComma();
					dataRow["CapitalVatAmount"] = returnCapitaVat.GetDecimalComma();
				}

				dataRow["IsGroup"] = 99;
				dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);

				if (purchaseAmount > 0 && returnAmount > 0)
				{
					dataRow = dtReport.NewRow();
					dataRow["Ledger"] = "TOTAL NET PURCHASE =>";

					if (ServerVersion < 10)
					{
						dataRow["Qty"] = purchaseQty - returnQty;
						dataRow["TotalAmount"] = purchaseAmount - returnAmount;
						dataRow["TaxFree"] = purchaseExempted - returnExempted;
						dataRow["ImportTaxable"] = purchaseImport - returnImport;
						dataRow["ImportVatAmount"] = purchaseImportVat - returnImportVat;
						dataRow["Taxable"] = purchaseTaxable - returnTaxable;
						dataRow["VatAmount"] = purchaseVat - returnVat;
						dataRow["CapitalTaxable"] = purchaseCapital - returnCapital;
						dataRow["CapitalVatAmount"] = purchaseCapitaVat - returnCapitaVat;
					}
					else
					{
						dataRow["Qty"] = (purchaseQty - returnQty).GetDecimalComma();
						dataRow["TotalAmount"] = (purchaseAmount - returnAmount).GetDecimalComma();
						dataRow["TaxFree"] = (purchaseExempted - returnExempted).GetDecimalComma();
						dataRow["ImportTaxable"] = (purchaseImport - returnImport).GetDecimalComma();
						dataRow["ImportVatAmount"] = (purchaseImportVat - returnImportVat).GetDecimalComma();
						dataRow["Taxable"] = (purchaseTaxable - returnTaxable).GetDecimalComma();
						dataRow["VatAmount"] = (purchaseVat - returnVat).GetDecimalComma();
						dataRow["CapitalTaxable"] = (purchaseCapital - returnCapital).GetDecimalComma();
						dataRow["CapitalVatAmount"] = (purchaseCapitaVat - returnCapitaVat).GetDecimalComma();
					}

					dataRow["IsGroup"] = 99;
					dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
				}
			}
		}

		return dtReport;
	}

	public DataTable GetPurchaseVatRegisterDetailsReport()
	{
		var cmdString = GetReports.RptMode switch
		{
			"VENDOR WISE" => GetPurchaseVatRegisterVendorWiseReportsScript(GetReports.Module),
			"DATE WISE" or "NORMAL" => GetPurchaseVatRegisterDayWiseReportsScript(GetReports.Module),
			_ => GetPurchaseVatRegisterDayWiseReportsScript(GetReports.Module)
		};
		var dtVatRegister = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		var dtReport = GetReports.RptMode switch
		{
			"VENDOR WISE" => ReturnSalesVatRegisterSummary(dtVatRegister),
			"DATE WISE" or "NORMAL" => ReturnPurchaseVatRegisterNormal(dtVatRegister),
			_ => new DataTable()
		};
		return dtReport;
	}

	#endregion **---------- PURCHASE VAT REGISTER ----------**


	// ANALYSIS REPORTS
	#region --------------- ANALYSIS REPORTS ---------------

	public string GetPartyLedgerAgingReportScript(bool isCustomer)
	{
		var asOnDate = GetReports.ToAdDate.GetDateTime();
		var firstSlab = asOnDate.AddDays(-GetReports.AgingDays);
		var seconSlab = firstSlab.AddDays(-GetReports.AgingDays);
		var thirdSlab = seconSlab.AddDays(-GetReports.AgingDays);
		var forthSlab = thirdSlab.AddDays(-GetReports.AgingDays);
		var fifthSlab = forthSlab.AddDays(-GetReports.AgingDays);
		var sixthSlab = fifthSlab.AddDays(-GetReports.AgingDays);
		var sevenSlab = sixthSlab.AddDays(-GetReports.AgingDays);

		var cmdString = string.Empty;
		if (isCustomer)
		{
			cmdString += $@"
				WITH AgingReport AS
				(
					SELECT ad.Ledger_ID, gl.GLName, (ad.LocalDebit_Amt-ad.LocalCredit_Amt) DueAmount,
					CASE WHEN ad.Voucher_Date < '{firstSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{seconSlab.GetSystemDate()}'  THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [30Days],
					CASE WHEN ad.Voucher_Date < '{seconSlab.GetSystemDate()}' AND ad.Voucher_Date >='{thirdSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [60Days],
					CASE WHEN ad.Voucher_Date < '{thirdSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{forthSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [90Days], ";
			if (GetReports.ColumnsNo is 4)
				cmdString +=
					$@" CASE WHEN ad.Voucher_Date < '{forthSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [Above90Days] ";
			else
				cmdString += $@"
					CASE WHEN ad.Voucher_Date < '{fifthSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{sixthSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [120Days],
					CASE WHEN ad.Voucher_Date < '{sixthSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{sevenSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [150Days],
					CASE WHEN ad.Voucher_Date < '{sevenSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{sevenSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [180Days],
					CASE WHEN ad.Voucher_Date < '{sevenSlab.GetSystemDate()}' THEN ad.LocalDebit_Amt-ad.LocalCredit_Amt ELSE 0 END [Above180Days] ";

			cmdString += $@"
					FROM AMS.AccountDetails ad
						INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
					WHERE gl.GLType IN ('Customer', 'Both')AND ad.Voucher_Date <='{asOnDate.GetSystemDate()}'
				)
				SELECT ag.Ledger_ID, ag.GLName,";

			cmdString += ServerVersion < 10
				? $@" SUM(CAST(ag.DueAmount AS DECIMAL(18,{SysAmountLength}))) DueAmount, SUM( CAST(ag.[30Days] AS DECIMAL(18,{SysAmountLength}))) FirstSlab,SUM(CAST( ag.[60Days] AS DECIMAL(18,{SysAmountLength}))) SecondSlab,SUM( CAST(ag.[90Days]AS DECIMAL(18,{SysAmountLength}))) ThirdSlab, "
				: $@" FORMAT(SUM(ag.DueAmount),'{SysAmountCommaFormat}') DueAmount,FORMAT(SUM(ag.[30Days]),'{SysAmountCommaFormat}') FirstSlab,FORMAT(SUM(ag.[60Days]),'{SysAmountCommaFormat}') SecondSlab,FORMAT(SUM( ag.[90Days]),'{SysAmountCommaFormat}') ThirdSlab,";
			if (ServerVersion < 10)
				cmdString += GetReports.ColumnsNo > 4
					? $@" CAST(SUM(ag.[120Days]) AS DECIMAL(18,{SysAmountLength})) FourthSlab,CAST(SUM(ag.[150Days]) AS DECIMAL(18,{SysAmountLength}) ) FifthSlab,FORMAT(SUM(ag.[180Days]),'{SysAmountCommaFormat}') SixthSlab,FORMAT(SUM(ag.[Above180Days]),'{SysAmountCommaFormat}') LastSlab,0 IsGroup "
					: $@" CAST(SUM(ag.Above90Days) AS DECIMAL(18,{SysAmountLength})) LastSlab, 0 IsGroup ";
			else
				cmdString += GetReports.ColumnsNo > 4
					? $@" FORMAT(SUM(ag.[120Days]),'{SysAmountCommaFormat}') FourthSlab,FORMAT(SUM(ag.[150Days]),'{SysAmountCommaFormat}') FifthSlab,FORMAT(SUM(ag.[180Days]),'{SysAmountCommaFormat}') SixthSlab,FORMAT(SUM(ag.[Above180Days]),'{SysAmountCommaFormat}') LastSlab,0 IsGroup "
					: $@" FORMAT(SUM(ag.Above90Days),'{SysAmountCommaFormat}') LastSlab, 0 IsGroup ";

			cmdString += @"
				FROM AgingReport ag
				WHERE 1=1 ";
			if (GetReports.LedgerId.IsValueExits()) cmdString += $" AND ag.Ledger_ID IN ({GetReports.LedgerId}) ";
			cmdString += @"
				GROUP BY ag.Ledger_ID, ag.GLName
				HAVING Sum(ag.DueAmount) <> 0
				ORDER BY ag.GLName; ";
		}
		else
		{
			cmdString += $@"
				WITH AgingReport AS
				(
					SELECT ad.Ledger_ID, gl.GLName, (ad.LocalCredit_Amt-ad.LocalDebit_Amt) DueAmount,
					CASE WHEN ad.Voucher_Date < '{firstSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{seconSlab.GetSystemDate()}'  THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [30Days],
					CASE WHEN ad.Voucher_Date < '{seconSlab.GetSystemDate()}' AND ad.Voucher_Date >='{thirdSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [60Days],
					CASE WHEN ad.Voucher_Date < '{thirdSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{forthSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [90Days], ";
			if (GetReports.ColumnsNo is 4)
				cmdString +=
					$@" CASE WHEN ad.Voucher_Date < '{forthSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [Above90Days] ";
			else
				cmdString += $@"
					CASE WHEN ad.Voucher_Date < '{fifthSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{sixthSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [120Days],
					CASE WHEN ad.Voucher_Date < '{sixthSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{sevenSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [150Days],
					CASE WHEN ad.Voucher_Date < '{sevenSlab.GetSystemDate()}' AND ad.Voucher_Date >= '{sevenSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [180Days],
					CASE WHEN ad.Voucher_Date < '{sevenSlab.GetSystemDate()}' THEN ad.LocalCredit_Amt-ad.LocalDebit_Amt ELSE 0 END [Above180Days] ";
			cmdString += $@"
					FROM AMS.AccountDetails ad
						INNER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
					WHERE gl.GLType IN ('Vendor', 'Both')AND ad.Voucher_Date <='{asOnDate.GetSystemDate()}'
				)
				SELECT ag.Ledger_ID, ag.GLName,";

			cmdString += ServerVersion < 10
				? $@" SUM(CAST(ag.DueAmount AS DECIMAL(18,{SysAmountLength}))) DueAmount, SUM( CAST(ag.[30Days] AS DECIMAL(18,{SysAmountLength}))) FirstSlab,SUM(CAST( ag.[60Days] AS DECIMAL(18,{SysAmountLength}))) SecondSlab,SUM( CAST(ag.[90Days]AS DECIMAL(18,{SysAmountLength}))) ThirdSlab, "
				: $@" FORMAT(SUM(ag.DueAmount),'{SysAmountCommaFormat}') DueAmount,FORMAT(SUM(ag.[30Days]),'{SysAmountCommaFormat}') FirstSlab,FORMAT(SUM(ag.[60Days]),'{SysAmountCommaFormat}') SecondSlab,FORMAT(SUM( ag.[90Days]),'{SysAmountCommaFormat}') ThirdSlab,";
			if (ServerVersion < 10)
				cmdString += GetReports.ColumnsNo > 4
					? $@" CAST(SUM(ag.[120Days]) AS DECIMAL(18,{SysAmountLength})) FourthSlab,CAST(SUM(ag.[150Days]) AS DECIMAL(18,{SysAmountLength}) ) FifthSlab,FORMAT(SUM(ag.[180Days]),'{SysAmountCommaFormat}') SixthSlab,FORMAT(SUM(ag.[Above180Days]),'{SysAmountCommaFormat}') LastSlab,0 IsGroup "
					: $@" CAST(SUM(ag.Above90Days) AS DECIMAL(18,{SysAmountLength})) LastSlab, 0 IsGroup ";
			else
				cmdString += GetReports.ColumnsNo > 4
					? $@" FORMAT(SUM(ag.[120Days]),'{SysAmountCommaFormat}') FourthSlab,FORMAT(SUM(ag.[150Days]),'{SysAmountCommaFormat}') FifthSlab,FORMAT(SUM(ag.[180Days]),'{SysAmountCommaFormat}') SixthSlab,FORMAT(SUM(ag.[Above180Days]),'{SysAmountCommaFormat}') LastSlab,0 IsGroup "
					: $@" FORMAT(SUM(ag.Above90Days),'{SysAmountCommaFormat}') LastSlab, 0 IsGroup ";

			cmdString += @"
				FROM AgingReport ag
				WHERE 1=1 ";
			if (GetReports.LedgerId.IsValueExits()) cmdString += $" AND ag.Ledger_ID IN ({GetReports.LedgerId}) ";
			cmdString += @"
				GROUP BY ag.Ledger_ID, ag.GLName
				HAVING Sum(ag.DueAmount) <> 0
				ORDER BY ag.GLName; ";
		}

		return cmdString;
	}

	private DataTable ReturnPartyAgingReportNormal(DataTable dtAging)
	{
		DataRow dataRow;
		var dtReport = dtAging.Copy();

		dataRow = dtReport.NewRow();
		dataRow["GLName"] = "TOTAL AMOUNT >>";
		if (ServerVersion < 10)
		{
			dataRow["DueAmount"] = dtReport.AsEnumerable().Sum(row => row["DueAmount"].GetDecimal());
			dataRow["FirstSlab"] = dtReport.AsEnumerable().Sum(row => row["FirstSlab"].GetDecimal());
			dataRow["SecondSlab"] = dtReport.AsEnumerable().Sum(row => row["SecondSlab"].GetDecimal());
			dataRow["ThirdSlab"] = dtReport.AsEnumerable().Sum(row => row["ThirdSlab"].GetDecimal());
		}
		else
		{
			dataRow["DueAmount"] = dtReport.AsEnumerable().Sum(row => row["DueAmount"].GetDecimal()).GetDecimalComma();
			dataRow["FirstSlab"] = dtReport.AsEnumerable().Sum(row => row["FirstSlab"].GetDecimal()).GetDecimalComma();
			dataRow["SecondSlab"] =
				dtReport.AsEnumerable().Sum(row => row["SecondSlab"].GetDecimal()).GetDecimalComma();
			dataRow["ThirdSlab"] = dtReport.AsEnumerable().Sum(row => row["ThirdSlab"].GetDecimal()).GetDecimalComma();
		}

		if (GetReports.ColumnsNo > 4)
		{
			if (ServerVersion < 10)
			{
				dataRow["FourthSlab"] = dtReport.AsEnumerable().Sum(row => row["FourthSlab"].GetDecimal());
				dataRow["FifthSlab"] = dtReport.AsEnumerable().Sum(row => row["FifthSlab"].GetDecimal());
				dataRow["SixthSlab"] = dtReport.AsEnumerable().Sum(row => row["SixthSlab"].GetDecimal());
				dataRow["LastSlab"] = dtReport.AsEnumerable().Sum(row => row["LastSlab"].GetDecimal());
			}
			else
			{
				dataRow["FourthSlab"] =
					dtReport.AsEnumerable().Sum(row => row["FourthSlab"].GetDecimal()).GetDecimalComma();
				dataRow["FifthSlab"] =
					dtReport.AsEnumerable().Sum(row => row["FifthSlab"].GetDecimal()).GetDecimalComma();
				dataRow["SixthSlab"] =
					dtReport.AsEnumerable().Sum(row => row["SixthSlab"].GetDecimal()).GetDecimalComma();
				dataRow["LastSlab"] =
					dtReport.AsEnumerable().Sum(row => row["LastSlab"].GetDecimal()).GetDecimalComma();
			}
		}
		else
		{
			if (ServerVersion < 10)
				dataRow["LastSlab"] = dtReport.AsEnumerable().Sum(row => row["LastSlab"].GetDecimal());
			else
				dataRow["LastSlab"] =
					dtReport.AsEnumerable().Sum(row => row["LastSlab"].GetDecimal()).GetDecimalComma();
		}

		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
		return dtReport;
	}

	public DataTable GetPartyAgingReport(bool isCustomer)
	{
		var cmdString = GetPartyLedgerAgingReportScript(isCustomer);
		var result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];

		var dtReport = GetReports.RptMode switch
		{
			_ => ReturnPartyAgingReportNormal(result)
		};
		return dtReport;
	}

	public DataTable GetTopNRegisterReport()
	{
		var cmdString = GetReports.Module switch
		{
			"V" => $@"
						SELECT TOP {GetReports.RptMode} Ledger_ID LedgerId,GL.GLCode ShortName,GL.GLName Ledger, GL.PanNo PanNo,
						SUM (CASE WHEN sd.Module='PB' then sd.LocalCredit_Amt ELSE 0 end) Amount,
						SUM (CASE WHEN sd.Module='PR' then sd.LocalDebit_Amt ELSE 0 end) ReturnAmount,
						SUM (CASE WHEN sd.Module='PB' then sd.LocalCredit_Amt WHEN sd.Module='SR' then -sd.LocalDebit_Amt ELSE 0 END ) NetAmount
						 FROM AMS.AccountDetails sd
							  LEFT OUTER JOIN AMS.GeneralLedger GL ON GL.GLID = sd.Ledger_ID
						 WHERE GL.GLType IN ('Vendor', 'Both') AND sd.Voucher_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
						 GROUP BY sd.Ledger_ID,GL.GLCode, GL.GLName,GL.PanNo
						 ORDER BY SUM ( sd.LocalDebit_Amt ) DESC, GL.GLName;",
			_ => $@"
						SELECT TOP {GetReports.RptMode} Ledger_ID LedgerId,GL.GLCode ShortName,GL.GLName Ledger, GL.PanNo PanNo,
						SUM (CASE WHEN sd.Module='SB' then sd.LocalDebit_Amt ELSE 0 end) Amount,
						SUM (CASE WHEN sd.Module='SR' then sd.LocalCredit_Amt ELSE 0 end) ReturnAmount,
						SUM (CASE WHEN sd.Module='SB' then sd.LocalDebit_Amt WHEN sd.Module='SR' then -sd.LocalCredit_Amt ELSE 0 END ) NetAmount
						 FROM AMS.AccountDetails sd
							  LEFT OUTER JOIN AMS.GeneralLedger GL ON GL.GLID = sd.Ledger_ID
						 WHERE GL.GLType IN ('Customer', 'Both') AND sd.Voucher_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
						 GROUP BY sd.Ledger_ID,GL.GLCode, GL.GLName,GL.PanNo
						 ORDER BY SUM ( sd.LocalDebit_Amt ) DESC, GL.GLName;"
		};

		var dtReports = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		return dtReports;
	}

	public DataTable GetSalesAnalysisReportCustomerProductWise()
	{
		throw new NotImplementedException();
	}

	private string GetSalesAnalysisProductWiseReportScript(string module)
	{
		var cmdString = module switch
		{
			"SB" => @$"
					SELECT p.PID [ProductId], p.PShortName [ShortName], p.PName [Description], pg.GrpCode [GroupCode], pg.GrpName [Group], psg.ShortName [SubGroupCode], psg.SubGrpName [SubGroup],SUM(sd.Alt_Qty) SalesAltQty, au.UnitCode SalesAltUnit,  SUM(sd.Qty) SalesQty, u.UnitCode SalesUnit,SUM(sd.B_Amount + ISNULL(term.TermAmount,0)) SalesAmount
					FROM AMS.SB_Details sd
						 INNER JOIN AMS.Product p ON p.PID=sd.P_Id
						 LEFT OUTER JOIN AMS.SB_Master sm ON sm.SB_Invoice = sd.SB_Invoice
						 LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
						 LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
						 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID=p.PAltUnit
						 LEFT OUTER JOIN AMS.ProductUnit u ON u.UID=p.PUnit
						 LEFT OUTER JOIN (
											SELECT sbt.Product_Id, CASE WHEN st.ST_Sign = '-' THEN -sbt.Amount ELSE sbt.Amount END TermAmount
											FROM AMS.SB_Term sbt
													INNER JOIN AMS.ST_Term st ON st.ST_ID=sbt.ST_Id
													LEFT OUTER JOIN AMS.SB_Master sm1 ON sm1.SB_Invoice = sbt.SB_Vno
											WHERE Product_Id IS NOT NULL AND sbt.ST_Id NOT IN(SELECT SBVatTerm FROM AMS.SalesSetting) AND sm1.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.FromAdDate.GetSystemDate()}'
										) AS term ON term.Product_Id = p.PID
					WHERE sm.FiscalYearId = {SysFiscalYearId} AND sm.CBranch_Id = {SysBranchId} AND (sm.CUnit_Id = '{SysCompanyUnitId}' OR sm.CUnit_Id IS NULL) AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.FromAdDate.GetSystemDate()}'
					GROUP BY p.PID, p.PShortName, p.PName, pg.GrpCode, pg.GrpName, psg.ShortName, psg.SubGrpName, au.UnitCode, u.UnitCode
					ORDER BY p.PName;",
			"SR" => $@"
						SELECT p.PID[ProductId], p.PShortName[ShortName], p.PName[Description], pg.GrpCode[GroupCode], pg.GrpName[Group], psg.ShortName[SubGroupCode], psg.SubGrpName[SubGroup],SUM(sd.Alt_Qty) ReturnAltQty, au.UnitCode ReturnAltUnit, SUM(sd.Qty) ReturnQty, u.UnitCode ReturnUnit, SUM(sd.B_Amount + ISNULL(term.TermAmount, 0)) ReturnAmount
						FROM AMS.SR_Details sd
								INNER JOIN AMS.Product p ON p.PID = sd.P_Id
								LEFT OUTER JOIN AMS.SR_Master sm ON sm.SR_Invoice = sd.SR_Invoice
								LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId = p.PGrpId
								LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId = p.PSubGrpId
								LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
								LEFT OUTER JOIN AMS.ProductUnit u ON u.UID = p.PUnit
								LEFT OUTER JOIN(
												SELECT sbt.Product_Id, CASE WHEN st.ST_Sign = '-' THEN - sbt.Amount ELSE sbt.Amount END TermAmount
												FROM AMS.SR_Term sbt
													INNER JOIN AMS.ST_Term st ON st.ST_ID = sbt.ST_Id
													LEFT OUTER JOIN AMS.SR_Master sm1 ON sm1.SR_Invoice = sbt.SR_Vno
												WHERE Product_Id IS NOT NULL AND sbt.ST_Id NOT IN(SELECT SBVatTerm FROM AMS.SalesSetting) AND sm1.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.FromAdDate.GetSystemDate()}'
												) AS term ON term.Product_Id = p.PID
						WHERE sm.FiscalYearId = {SysFiscalYearId} AND sm.CBranch_Id = {SysBranchId} AND(sm.CUnit_Id = '{SysCompanyUnitId}' OR sm.CUnit_Id IS NULL) AND sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.FromAdDate.GetSystemDate()}'
						GROUP BY p.PID, p.PShortName, p.PName, pg.GrpCode, pg.GrpName, psg.ShortName, psg.SubGrpName, au.UnitCode, u.UnitCode
						ORDER BY p.PName;
				",
			_ => string.Empty
		};
		return cmdString;
	}

	private DataTable GenerateSalesAnalysisReportProductWise()
	{
		var cmdString = GetSalesAnalysisProductWiseReportScript("SB");
		var sales = QueryUtils.GetList<SalesAnalysisReportProductWise>(cmdString);

		cmdString = GetSalesAnalysisProductWiseReportScript("SR");
		var salesReturn = QueryUtils.GetList<SalesReturnAnalysisReportProductWise>(cmdString);

		if (!sales.Success || !salesReturn.Success) return null;

		var joinedData = new List<FinalSalesAnalysisReportProductWise>();

		foreach (var t in sales.List.GroupJoin(salesReturn.List, si => si.ProductId, sr => sr.ProductId,
					 (si, temp) => new { si, temp }))
			joinedData.AddRange(t.temp.DefaultIfEmpty().Select(nSr => new FinalSalesAnalysisReportProductWise
			{
				ProductId = t.si.ProductId,
				ShortName = t.si.ShortName,
				Description = t.si.Description,
				GroupCode = t.si.GroupCode,
				Group = t.si.Group,
				SubGroupCode = t.si.SubGroupCode,
				SubGroup = t.si.SubGroup,
				SalesAltQty = t.si.SalesAltQty,
				SalesAltUnit = t.si.SalesAltUnit,
				SalesQty = t.si.SalesQty,
				SalesUnit = t.si.SalesUnit,
				SalesAmount = t.si.SalesAmount,
				ReturnAltQty = nSr?.ReturnAltQty?.ToString(),
				ReturnAltUnit = nSr?.ReturnAltUnit,
				ReturnQty = nSr?.ReturnQty?.ToString(),
				ReturnUnit = nSr?.ReturnUnit,
				ReturnAmount = nSr?.ReturnAmount?.ToString()
			}));
		var dtProduct = joinedData.ToDataTable();
		return dtProduct;
	}

	private DataTable GenerateSalesAnalysisReportProductGroupWise()
	{
		var cmdString = GetSalesAnalysisProductWiseReportScript("SB");
		var sales = QueryUtils.GetList<SalesAnalysisReportProductWise>(cmdString);

		cmdString = GetSalesAnalysisProductWiseReportScript("SR");
		var salesReturn = QueryUtils.GetList<SalesReturnAnalysisReportProductWise>(cmdString);

		if (!sales.Success || !salesReturn.Success) return null;

		var joinedData = new List<FinalSalesAnalysisReportProductWise>();
		foreach (var t in sales.List.GroupJoin(salesReturn.List, si => si.ProductId, sr => sr.ProductId,
					 (si, temp) => new { si, temp }))
			joinedData.AddRange(t.temp.DefaultIfEmpty().Select(nSr => new FinalSalesAnalysisReportProductWise
			{
				ProductId = t.si.ProductId,
				ShortName = t.si.ShortName,
				Description = t.si.Description,
				GroupCode = t.si.GroupCode,
				Group = t.si.Group,
				SubGroupCode = t.si.SubGroupCode,
				SubGroup = t.si.SubGroup,
				SalesAltQty = t.si.SalesAltQty,
				SalesAltUnit = t.si.SalesAltUnit,
				SalesQty = t.si.SalesQty,
				SalesUnit = t.si.SalesUnit,
				SalesAmount = t.si.SalesAmount,
				ReturnAltQty = nSr?.ReturnAltQty?.ToString(),
				ReturnAltUnit = nSr?.ReturnAltUnit,
				ReturnQty = nSr?.ReturnQty?.ToString(),
				ReturnUnit = nSr?.ReturnUnit,
				ReturnAmount = nSr?.ReturnAmount?.ToString()
			}));

		var groupedList = new List<FinalSalesAnalysisReportProductWise>();
		var distinctGroups = joinedData.GroupBy(x => x.Group).Select(g => new
		{
			GroupName = g.Key,
			SalesAltQty = g.Sum(s => s.SalesAltQty.GetDecimal()),
			SalesQty = g.Sum(s => s.SalesQty.GetDecimal()),
			SalesAmount = g.Sum(s => s.SalesAmount.GetDecimal()),

			ReturnAltQty = g.Sum(s => s.ReturnAltQty.GetDecimal()),
			ReturnQty = g.Sum(s => s.ReturnQty.GetDecimal()),
			ReturnAmount = g.Sum(s => s.ReturnAmount.GetDecimal()),

			NetSalesAltQty = g.Sum(s => s.NetSalesAltQty.GetDecimal()),
			NetSalesQty = g.Sum(s => s.NetSalesQty.GetDecimal()),
			NetSalesAmount = g.Sum(s => s.NetSalesAmount.GetDecimal()),

			IsGroup = 1
		});
		foreach (var grp in distinctGroups)
		{
			groupedList.Add(new FinalSalesAnalysisReportProductWise
			{
				Description = grp.GroupName
			});
			groupedList.AddRange(joinedData.Where(x => x.Group == grp.GroupName));
			groupedList.Add(new FinalSalesAnalysisReportProductWise
			{
				Description = "[GROUP TOTAL] " + grp.GroupName,
				SalesAltQty = grp.SalesAltQty.ToString(),
				SalesQty = grp.SalesQty.ToString(),
				SalesAmount = grp.SalesAmount.ToString(),

				ReturnAltQty = grp.ReturnAltQty.ToString(),
				ReturnQty = grp.ReturnQty.ToString(),
				ReturnAmount = grp.ReturnAmount.ToString(),
				IsGroup = 11
			});
		}

		var dtGroupBy = groupedList.ToDataTable();
		return dtGroupBy;
	}

	public DataTable GetSalesAnalysisReport()
	{
		var dtReport = GetReports.RptMode switch
		{
			"PRODUCT WISE" => GenerateSalesAnalysisReportProductWise(),
			"PRODUCT GROUP WISE" => GenerateSalesAnalysisReportProductGroupWise(),
			"PRODUCT SUB GROUP WISE" => GenerateSalesAnalysisReportProductGroupWise(),
			_ => new DataTable()
		};
		return dtReport;
	}

	public DataTable GetSalesAnalysisReportProductCustomerWise()
	{
		throw new NotImplementedException();
	}

	public DataTable GetSalesAnalysisReportCustomerWise()
	{
		throw new NotImplementedException();
	}

	#endregion --------------- ANALYSIS REPORTS ---------------


	// SALES, PURCHASE & PARTY OUTSTANDING REGISTER SUMMARY
	#region **---------- RETURN DT OUTSTANDING REPORT SUMMARY ----------**

	public DataTable ReturnOutstandingVoucherDateWiseReportSummary(DataTable dsRegister)
	{
		var dtReport = dsRegister.Clone();
		DataRow dataRow;
		var dtVoucher = dsRegister.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("VoucherNo")
		}).Select(g => g.First()).OrderBy(g => g.Field<string>("VoucherNo")).CopyToDataTable();

		foreach (DataRow ro in dtVoucher.Rows)
		{
			var voucherNo = ro["VoucherNo"];
			dataRow = dtReport.NewRow();
			dataRow["VoucherNo"] = voucherNo;
			dataRow["VoucherDate"] = ro["VoucherDate"];
			dataRow["VoucherMiti"] = ro["VoucherMiti"];
			dataRow["LedgerId"] = ro["LedgerId"];
			dataRow["Ledger"] = ro["Ledger"];
			dataRow["DateDiff"] = ro["DateDiff"];
			dataRow["IsGroup"] = 1;
			dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);

			var details = dsRegister.Select($"VoucherNo = '{voucherNo}'");
			foreach (var dr in details)
			{
				dataRow = dtReport.NewRow();
				dataRow["VoucherNo"] = dr["PShortName"];
				dataRow["Ledger"] = dr["Product"];
				dataRow["LedgerId"] = dr["ProductId"];
				dataRow["AltQty"] = dr["AltQty"];
				dataRow["AltUom"] = dr["AltUom"];
				dataRow["Qty"] = dr["Qty"];
				dataRow["Uom"] = dr["Uom"];
				dataRow["Rate"] = dr["Rate"];
				dataRow["Amount"] = dr["Amount"];
				dataRow["AdjustVoucher"] = dr["AdjustVoucher"];
				dataRow["AdjustQty"] = dr["AdjustQty"];
				dataRow["AdjustDate"] = dr["AdjustDate"];
				dataRow["AdjustMiti"] = dr["AdjustMiti"];
				dataRow["BalanceQty"] = dr["BalanceQty"];
				dataRow["IsGroup"] = 0;
				dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
			}

			dataRow = dtReport.NewRow();
			dataRow["Ledger"] = "[VOUCHER TOTAL] =>";
			dataRow["AltQty"] = details.AsEnumerable().Sum(row => row["AltQty"].GetDecimal()).GetDecimalQtyString();
			dataRow["Qty"] = details.AsEnumerable().Sum(row => row["Qty"].GetDecimal()).GetDecimalQtyString();
			dataRow["Amount"] = details.AsEnumerable().Sum(row => row["Amount"].GetDecimal()).GetDecimalComma();
			dataRow["AdjustQty"] = details.AsEnumerable().Sum(row => row["AdjustQty"].GetDecimal()).GetDecimalComma();
			dataRow["BalanceQty"] = details.AsEnumerable().Sum(row => row["BalanceQty"].GetDecimal()).GetDecimalComma();
			dataRow["IsGroup"] = 11;
			dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
		}

		dataRow = dtReport.NewRow();
		dataRow["Ledger"] = "[GRAND TOTAL] =>";
		dataRow["AltQty"] = dsRegister.AsEnumerable().Sum(row => row["AltQty"].GetDecimal()).GetDecimalQtyString();
		dataRow["Qty"] = dsRegister.AsEnumerable().Sum(row => row["Qty"].GetDecimal()).GetDecimalQtyString();
		dataRow["Amount"] = dsRegister.AsEnumerable().Sum(row => row["Amount"].GetDecimal()).GetDecimalComma();
		dataRow["AdjustQty"] = dsRegister.AsEnumerable().Sum(row => row["AdjustQty"].GetDecimal()).GetDecimalComma();
		dataRow["BalanceQty"] = dsRegister.AsEnumerable().Sum(row => row["BalanceQty"].GetDecimal()).GetDecimalComma();
		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
		return dtReport;
	}
	public DataTable ReturnOutstandingLedgerWiseReportSummary(DataTable dsRegister)
	{
		DataRow dataRow;
		var dtCheckTerm = new DataTable();
		var dtReport = new DataTable("REPORT");
		dtReport.AddStringColumns(new[]
		{
			"dt_Date",
			"dt_Miti",
			"dt_VoucherNo",
			"dt_Ledger",
			"dt_Basic",
			"dt_InvoiceNo",
			"dt_Module",
			"dt_FilterBy",
			"dt_GroupBy",
			"IsGroup"
		});

		var dtCustomer = dsRegister.AsEnumerable().GroupBy(r => new
		{
			ledger = r.Field<string>("Ledger")
		}).Select(g => g.First()).OrderBy(r => r.Field<string>("Ledger")).CopyToDataTable();
		foreach (DataRow drCustomer in dtCustomer.Rows) dataRow = dtReport.NewRow();
		return dtReport;
	}

	#endregion **---------- RETURN DT OUTSTANDING REPORT SUMMARY ----------**


	// SALES, PURCHASE & PARTY OUTSTANDING REGISTER DETAILS
	#region **---------- RETURN DT OUTSTANDING REPORT DETAILS ----------**

	public string GetScriptForOutstandingDetailsReports(bool isCustomer)
	{
		var cmdString = string.Empty;
		if (isCustomer)
		{
			cmdString = @$"
				WITH LedgerOutStanding AS
				(   SELECT lo.Module, lo.ModuleType, lo.VoucherNo, lo.VoucherDate, lo.VoucherMiti, lo.LedgerId, lo.LedgerDesc, lo.VoucherAmount, lo.BalanceAmount, SUM(ISNULL(lo.VoucherAmount,0)-ISNULL(lo.BalanceAmount,0)) OVER (PARTITION BY lo.LedgerDesc ORDER BY lo.LedgerDesc, lo.VoucherDate,lo.VoucherNo) OutStanding, lo.DateAge
					FROM (SELECT '' Module, '' VoucherNo, '' ModuleType, '' VoucherDate, '' VoucherMiti, ad.Ledger_ID LedgerId, gl.GLName LedgerDesc, 0 VoucherAmount, SUM(ad.LocalDebit_Amt) BalanceAmount, 0 DateAge
						  FROM AMS.AccountDetails                    ad
							   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
							   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
						  WHERE gl.GLType IN ('Customer', 'Both') AND ad.LocalDebit_Amt>0 AND ad.Voucher_Date <= '{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.IncludePdc ? "" : " AND ad.Module NOT IN ('PDC','PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += @$"
						  GROUP BY ad.Ledger_ID, gl.GLName
						  UNION ALL
						  SELECT ad.Module, ad.Voucher_No VoucherNo, md.ModuleType, ad.Voucher_Date VoucherDate, ad.Voucher_Miti VoucherMiti, ad.Ledger_ID, gl.GLName, ad.LocalCredit_Amt VoucherAmount, 0 BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, '{GetReports.ToAdDate.GetSystemDate()}') DateAge
						  FROM AMS.AccountDetails                    ad
							   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
							   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
						  WHERE gl.GLType IN ('Customer', 'Both') AND ad.LocalCredit_Amt>0 AND ad.Voucher_Date <= '{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.IncludePdc ? "" : " AND ad.Module NOT IN ('PDC','PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += @$"   ) AS lo )
				SELECT ls.Module, ls.ModuleType, ls.VoucherNo,CONVERT(NVARCHAR(10),ls.VoucherDate,103) VoucherDate, ls.VoucherMiti, ls.LedgerId, ls.LedgerDesc,FORMAT(ls.VoucherAmount,'{SysAmountCommaFormat}') VoucherAmount, FORMAT(ls.BalanceAmount,'{SysAmountCommaFormat}') BalanceAmount, FORMAT(ls.OutStanding,'{SysAmountCommaFormat}') OutStanding, ls.DateAge,0 IsGroup
				FROM LedgerOutStanding ls
				WHERE ls.OutStanding > 0
				ORDER BY ls.LedgerDesc, ls.VoucherDate,ls.VoucherNo;";
		}
		else
		{
			cmdString = @$"
				WITH LedgerOutStanding AS
				(   SELECT lo.Module, lo.ModuleType, lo.VoucherNo, lo.VoucherDate, lo.VoucherMiti, lo.LedgerId, lo.LedgerDesc, lo.VoucherAmount, lo.BalanceAmount, SUM(ISNULL(lo.VoucherAmount,0)-ISNULL(lo.BalanceAmount,0)) OVER (PARTITION BY lo.LedgerDesc ORDER BY lo.LedgerDesc, lo.VoucherDate,lo.VoucherNo) OutStanding, lo.DateAge
					FROM (SELECT '' Module, '' VoucherNo, '' ModuleType, '' VoucherDate, '' VoucherMiti, ad.Ledger_ID LedgerId, gl.GLName LedgerDesc, 0 VoucherAmount, SUM(ad.LocalDebit_Amt) BalanceAmount, 0 DateAge
						  FROM AMS.AccountDetails                    ad
							   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
							   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
						  WHERE gl.GLType IN ('Vendor', 'Both') AND ad.LocalDebit_Amt>0 AND ad.Voucher_Date <= '{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.IncludePdc ? "" : " AND ad.Module NOT IN ('PDC','PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += @$"
						  GROUP BY ad.Ledger_ID, gl.GLName
						  UNION ALL
						  SELECT ad.Module, ad.Voucher_No VoucherNo, md.ModuleType, ad.Voucher_Date VoucherDate, ad.Voucher_Miti VoucherMiti, ad.Ledger_ID, gl.GLName, ad.LocalCredit_Amt VoucherAmount, 0 BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, '{GetReports.ToAdDate.GetSystemDate()}') DateAge
						  FROM AMS.AccountDetails                    ad
							   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
							   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
						  WHERE gl.GLType IN ('Vendor', 'Both') AND ad.LocalCredit_Amt>0 AND ad.Voucher_Date <= '{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.IncludePdc ? "" : " AND ad.Module NOT IN ('PDC','PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += @$"   ) AS lo )
				SELECT ls.Module, ls.ModuleType, ls.VoucherNo,CONVERT(NVARCHAR(10),ls.VoucherDate,103) VoucherDate, ls.VoucherMiti, ls.LedgerId, ls.LedgerDesc,FORMAT(ls.VoucherAmount,'{SysAmountCommaFormat}') VoucherAmount, FORMAT(ls.BalanceAmount,'{SysAmountCommaFormat}') BalanceAmount, FORMAT(ls.OutStanding,'{SysAmountCommaFormat}') OutStanding, ls.DateAge,0 IsGroup
				FROM LedgerOutStanding ls
				WHERE ls.OutStanding > 0
				ORDER BY ls.LedgerDesc, ls.VoucherDate, ls.VoucherNo;  ";
		}

		return cmdString;
	}
	public string GetScriptForOutstandingDetailsIncludeAdjustmentReports(bool isCustomer)
	{
		var cmdString = string.Empty;
		if (isCustomer)
		{
			cmdString = @$"
				WITH LedgerOutStanding AS
				(   SELECT lo.Module, lo.ModuleType, lo.VoucherNo, ISNULL(lo.VoucherDate, a.VoucherDate) VoucherDate, lo.VoucherMiti, ISNULL(lo.LedgerId, a.LedgerId) LedgerId, ISNULL(lo.LedgerDesc, a.LedgerDesc) LedgerDesc, ISNULL(lo.VoucherAmount,0) VoucherAmount, a.VoucherDate AdjustDate, a.VoucherMiti AdjustMiti, a.VoucherNo AdjustVoucherNo, ISNULL(a.BalanceAmount, 0) BalanceAmount, SUM(ISNULL(lo.VoucherAmount, 0)-ISNULL(a.BalanceAmount, 0)) OVER (PARTITION BY ISNULL(lo.LedgerDesc, a.LedgerDesc) ORDER BY ISNULL(lo.LedgerDesc, a.LedgerDesc), ISNULL(lo.VoucherDate, a.VoucherDate), ISNULL(lo.VoucherNo, a.VoucherNo)) OutStanding, ISNULL(lo.DateAge,a.DateAge) DateAge
					FROM (SELECT ROW_NUMBER() OVER (PARTITION BY gl.GLName ORDER BY gl.GLName, ad.Voucher_Date) RowNumber, ad.Module, ad.Voucher_No VoucherNo, md.ModuleType, ad.Voucher_Date VoucherDate, ad.Voucher_Miti VoucherMiti, ad.Ledger_ID LedgerId, gl.GLName LedgerDesc, ad.LocalCredit_Amt VoucherAmount, 0 BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, GETDATE()) DateAge
						  FROM AMS.AccountDetails                    ad
							   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
							   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
						  WHERE gl.GLType IN ('Customer', 'Both') AND ad.LocalCredit_Amt>0 AND ad.Voucher_Date<='{GetReports.ToAdDate.GetSystemDate()}' ";
			cmdString += GetReports.IncludePdc ? " " : @" AND ad.Module NOT IN ('PDC', 'PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += $@" )AS lo
						 FULL OUTER JOIN (SELECT ROW_NUMBER() OVER (PARTITION BY gl.GLName ORDER BY gl.GLName, ad.Voucher_Date,ad.Voucher_No) AdjustRow, ad.Module, ad.Voucher_No VoucherNo, md.ModuleType, ad.Voucher_Date VoucherDate, ad.Voucher_Miti VoucherMiti, ad.Ledger_ID LedgerId, gl.GLName LedgerDesc, ad.LocalDebit_Amt BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, GETDATE()) DateAge
										  FROM AMS.AccountDetails                    ad
											   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
											   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
										  WHERE gl.GLType IN ('Customer', 'Both') AND ad.LocalDebit_Amt>0 AND ad.Voucher_Date<='{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.IncludePdc ? " " : @" AND ad.Module NOT IN ('PDC', 'PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += @"   ) AS a ON a.LedgerId=lo.LedgerId AND a.AdjustRow=lo.RowNumber)
				SELECT ls.Module, ls.ModuleType, ls.VoucherNo, ls.VoucherDate, ls.VoucherMiti, ls.LedgerId, ls.LedgerDesc, ls.VoucherAmount, ls.AdjustDate, ls.AdjustMiti, ls.AdjustVoucherNo, ls.BalanceAmount, ls.OutStanding, ls.DateAge, 0 IsGroup
				FROM LedgerOutStanding ls
				ORDER BY ls.LedgerDesc, ls.VoucherDate, ls.VoucherNo;";
		}
		else
		{
			cmdString = @$"
				WITH LedgerOutStanding AS
				(   SELECT lo.Module, lo.ModuleType, lo.VoucherNo, ISNULL(lo.VoucherDate, a.VoucherDate) VoucherDate, lo.VoucherMiti, ISNULL(lo.LedgerId, a.LedgerId) LedgerId, ISNULL(lo.LedgerDesc, a.LedgerDesc) LedgerDesc, ISNULL(lo.VoucherAmount,0) VoucherAmount, a.VoucherDate AdjustDate, a.VoucherMiti AdjustMiti, a.VoucherNo AdjustVoucherNo, ISNULL(a.BalanceAmount, 0) BalanceAmount, SUM(ISNULL(lo.VoucherAmount, 0)-ISNULL(a.BalanceAmount, 0)) OVER (PARTITION BY ISNULL(lo.LedgerDesc, a.LedgerDesc) ORDER BY ISNULL(lo.LedgerDesc, a.LedgerDesc), ISNULL(lo.VoucherDate, a.VoucherDate), ISNULL(lo.VoucherNo, a.VoucherNo)) OutStanding, ISNULL(lo.DateAge,a.DateAge) DateAge
					FROM (SELECT ROW_NUMBER() OVER (PARTITION BY gl.GLName ORDER BY gl.GLName, ad.Voucher_Date) RowNumber, ad.Module, ad.Voucher_No VoucherNo, md.ModuleType, ad.Voucher_Date VoucherDate, ad.Voucher_Miti VoucherMiti, ad.Ledger_ID LedgerId, gl.GLName LedgerDesc, ad.LocalCredit_Amt VoucherAmount, 0 BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, GETDATE()) DateAge
						  FROM AMS.AccountDetails                    ad
							   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
							   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
						  WHERE gl.GLType IN ('Vendor', 'Both') AND ad.LocalCredit_Amt>0 AND ad.Voucher_Date<='{GetReports.ToAdDate.GetSystemDate()}' ";
			cmdString += GetReports.IncludePdc ? " " : @" AND ad.Module NOT IN ('PDC', 'PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += $@" )AS lo
						 FULL OUTER JOIN (SELECT ROW_NUMBER() OVER (PARTITION BY gl.GLName ORDER BY gl.GLName, ad.Voucher_Date,ad.Voucher_No) AdjustRow, ad.Module, ad.Voucher_No VoucherNo, md.ModuleType, ad.Voucher_Date VoucherDate, ad.Voucher_Miti VoucherMiti, ad.Ledger_ID LedgerId, gl.GLName LedgerDesc, ad.LocalDebit_Amt BalanceAmount, DATEDIFF(DAY, ad.Voucher_Date, GETDATE()) DateAge
										  FROM AMS.AccountDetails                    ad
											   LEFT OUTER JOIN AMS.GeneralLedger     gl ON gl.GLID=ad.Ledger_ID
											   LEFT OUTER JOIN AMS.ModuleDescription md ON md.Module=ad.Module
										  WHERE gl.GLType IN ('Vendor', 'Both') AND ad.LocalDebit_Amt>0 AND ad.Voucher_Date<='{GetReports.ToAdDate.GetSystemDate()}'";
			cmdString += GetReports.IncludePdc ? " " : @" AND ad.Module NOT IN ('PDC', 'PROV') ";
			cmdString += GetReports.LedgerId.IsValueExits() ? $" AND ad.Ledger_ID IN ('{GetReports.LedgerId}')  " : " ";
			cmdString += @"   ) AS a ON a.LedgerId=lo.LedgerId AND a.AdjustRow=lo.RowNumber)
				SELECT ls.Module, ls.ModuleType, ls.VoucherNo, ls.VoucherDate, ls.VoucherMiti, ls.LedgerId, ls.LedgerDesc, ls.VoucherAmount, ls.AdjustDate, ls.AdjustMiti, ls.AdjustVoucherNo, ls.BalanceAmount, ls.OutStanding, ls.DateAge, 0 IsGroup
				FROM LedgerOutStanding ls
				ORDER BY ls.LedgerDesc, ls.VoucherDate, ls.VoucherNo;";
		}

		return cmdString;
	}
	public DataTable GetPartyOutstandingReportFormat()
	{
		var dtReport = new DataTable();
		dtReport.AddStringColumns(new[]
		{
			"dtDate",
			"dtMiti",
			"dtLedgerId",
			"dtLedger",
			"dtAmount",
			"dtAdjustVoucher",
			"dtAdjustDate",
			"dtAdjustMiti",
			"dtAdjustment",
			"dtBalance",
			"dtVoucherNo",
			"dtModule",
			"dtDueDays",
			"dtFilterBy"
		});
		dtReport.AddColumn("IsGroup", typeof(int));
		dtReport.AddColumn("IsActive", typeof(int));
		return dtReport;
	}
	public DataTable ReturnOutstandingReportPartyWiseSummaryReports(DataTable dtLedger)
	{
		DataRow dataRow;
		var dtReport = dtLedger.Clone();

		var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
		{
			ledger = r.Field<long>("LedgerId")
		}).Select(g => g.First()).OrderBy(g => g.Field<string>("LedgerDesc")).CopyToDataTable();
		decimal outStanding = 0;
		foreach (DataRow rowCustomer in dtLedgerGroup.Rows)
		{
			var ledegerId = rowCustomer["LedgerId"].GetLong();
			dataRow = dtReport.NewRow();
			dataRow["LedgerId"] = ledegerId;
			dataRow["LedgerDesc"] = rowCustomer["LedgerDesc"];
			dataRow["IsGroup"] = 1;
			dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
			var ledgerDetails = dtLedger.Select($"LedgerId={ledegerId}").CopyToDataTable();

			foreach (DataRow detailsRow in ledgerDetails.Rows)
			{
				outStanding = detailsRow["OutStanding"].GetDecimal();
				dataRow["VoucherDate"] = detailsRow["VoucherDate"];
				dataRow["VoucherMiti"] = detailsRow["VoucherMiti"];
				dataRow["LedgerId"] = detailsRow["LedgerId"];
				dataRow["LedgerDesc"] =
					detailsRow["LedgerDesc"].GetProperCase() + "[ " + detailsRow["VoucherNo"] + " ]";
				dataRow["VoucherAmount"] = detailsRow["VoucherAmount"].GetDecimal();
				dataRow["BalanceAmount"] = detailsRow["BalanceAmount"].GetDecimal();
				dataRow["OutStanding"] = detailsRow["OutStanding"].GetDecimal();
				dataRow["DateAge"] = detailsRow["DateAge"].GetDecimal();
				dataRow["VoucherNo"] = detailsRow["VoucherNo"];
				dataRow["IsGroup"] = 0;
				dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
			}

			dataRow = dtReport.NewRow();
			dataRow["LedgerDesc"] = $"[{rowCustomer["LedgerDesc"]}] TOTAL >>";
			dataRow["VoucherAmount"] =
				ledgerDetails.AsEnumerable().Sum(x => x["VoucherAmount"].GetDecimal()).GetDecimalComma();
			dataRow["OutStanding"] = outStanding.GetDecimalComma();
			dataRow["IsGroup"] = 99;
			dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
			outStanding = 0;
		}

		var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
		var outBalance = dtReport.Select("IsGroup = 11").CopyToDataTable();
		dataRow = dtReport.NewRow();
		dataRow["LedgerDesc"] = "GRAND TOTAL :- ";
		dataRow["VoucherAmount"] = dtGrand.AsEnumerable().Sum(x => x["VoucherAmount"].GetDecimal());
		dataRow["OutStanding"] = outBalance.AsEnumerable().Sum(x => x["BalanceAmount"].GetDecimal());
		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
		return dtReport;
	}

	public DataTable ReturnOutstandingReportPartyWiseDetails(DataTable dtLedger)
	{
		DataRow dataRow;
		var dtReport = dtLedger.Clone();

		var dtLedgerGroup = dtLedger.AsEnumerable().GroupBy(r => new
		{
			ledger = r.Field<long>("LedgerId")
		}).Select(g => g.First()).OrderBy(g => g.Field<string>("LedgerDesc")).CopyToDataTable();
		decimal outStanding = 0;
		decimal adjustment = 0;
		foreach (DataRow rowCustomer in dtLedgerGroup.Rows)
		{
			var ledegerId = rowCustomer["LedgerId"].GetLong();
			dataRow = dtReport.NewRow();
			dataRow["LedgerId"] = ledegerId;
			dataRow["LedgerDesc"] = rowCustomer["LedgerDesc"];
			dataRow["IsGroup"] = 1;
			dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
			var ledgerDetails = dtLedger.Select($"LedgerId={ledegerId}").CopyToDataTable();
			var isFirst = false;
			foreach (DataRow detailsRow in ledgerDetails.Rows)
			{
				dataRow = dtReport.NewRow();
				outStanding = detailsRow["OutStanding"].GetDecimal();
				var voucherAmount = detailsRow["VoucherAmount"].GetDecimal();

				if (!GetReports.IncludeAdjustment && adjustment is 0)
				{
					adjustment = voucherAmount - outStanding;
					isFirst = true;
				}

				var ledgerOutStanding = adjustment;
				dataRow["VoucherDate"] = detailsRow["VoucherDate"];
				dataRow["VoucherMiti"] = detailsRow["VoucherMiti"];
				dataRow["LedgerId"] = detailsRow["LedgerId"];
				dataRow["LedgerDesc"] =
					detailsRow["ModuleType"].GetProperCase() + " - [" + detailsRow["VoucherNo"] + "]";
				dataRow["VoucherAmount"] = detailsRow["VoucherAmount"];
				dataRow["BalanceAmount"] = isFirst && adjustment > 0
					? adjustment.GetDecimalComma()
					: detailsRow["BalanceAmount"];
				dataRow["OutStanding"] = detailsRow["OutStanding"];
				dataRow["DateAge"] = detailsRow["DateAge"].GetDecimal();
				dataRow["VoucherNo"] = detailsRow["VoucherNo"];
				dataRow["IsGroup"] = 0;
				dtReport.Rows.InsertAt(dataRow, dtReport.RowsCount() + 1);
				isFirst = false;
			}

			dataRow = dtReport.NewRow();
			dataRow["LedgerDesc"] = $"[{rowCustomer["LedgerDesc"]}] TOTAL >>";
			dataRow["VoucherAmount"] =
				ledgerDetails.AsEnumerable().Sum(x => x["VoucherAmount"].GetDecimal()).GetDecimalComma();
			dataRow["OutStanding"] = outStanding.GetDecimalComma();
			dataRow["BalanceAmount"] = adjustment.GetDecimalComma();
			dataRow["IsGroup"] = 11;
			dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
			outStanding = 0;
			adjustment = 0;
		}

		var dtGrand = dtReport.Select("IsGroup = 0 ").CopyToDataTable();
		var outBalance = dtReport.Select("IsGroup = 11").CopyToDataTable();
		dataRow = dtReport.NewRow();
		dataRow["LedgerDesc"] = "GRAND TOTAL :- ";
		dataRow["VoucherAmount"] = dtGrand.AsEnumerable().Sum(x => x["VoucherAmount"].GetDecimal()).GetDecimalComma();
		dataRow["BalanceAmount"] =
			outBalance.AsEnumerable().Sum(x => x["BalanceAmount"].GetDecimal()).GetDecimalComma();
		dataRow["OutStanding"] = outBalance.AsEnumerable().Sum(x => x["OutStanding"].GetDecimal()).GetDecimalComma();
		dataRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(dataRow, dtReport.Rows.Count + 1);
		return dtReport;
	}

	public DataTable ReturnOutstandingVoucherDateWiseReportDetails(DataTable dsRegister)
	{
		DataRow newRow;
		var dtReport = new DataTable("REPORT");
		dtReport.AddStringColumns(new[]
		{
			"GTxtDate",
			"GTxtMiti",
			"GTxtVoucherNo",
			"GTxtLedgerId",
			"GTxtLedger",
			"GTxtQty",
			"GTxtAdjustDate",
			"GTxtAdjustMiti",
			"GTxtAdjustVoucher",
			"GTxtAdjustQty",
			"GTxtBalanceQty",
			"GTxtRate",
			"GTxtAmount",
			"GTxtDueDays",
			"IsGroup"
		});

		var dtGroupVoucher = dsRegister.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("VoucherNo")
		}).Select(g => g.First()).CopyToDataTable();
		foreach (DataRow drMaster in dtGroupVoucher.Rows)
		{
			newRow = dtReport.NewRow();
			newRow["GTxtDate"] = drMaster["VoucherDate"].ToString();
			newRow["GTxtMiti"] = drMaster["VoucherMiti"].ToString();
			newRow["GTxtVoucherNo"] = drMaster["VoucherNo"].ToString();
			newRow["GTxtLedgerId"] = drMaster["LedgerId"].ToString();
			newRow["GTxtLedger"] = drMaster["Ledger"].ToString();
			newRow["GTxtDueDays"] =
				DateTime.Now.Date.Subtract(drMaster["VoucherDate"].ToString().GetDateTime()).Days;
			newRow["IsGroup"] = 1;
			dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

			var voucherNo = drMaster["VoucherNo"].ToString();
			var dtVoucherDetails = dsRegister.Select($"VoucherNo = '{voucherNo}'").CopyToDataTable();
			foreach (DataRow drDetails in dtVoucherDetails.Rows)
			{
				newRow = dtReport.NewRow();
				newRow["GTxtLedgerId"] = drDetails["ProductId"].ToString();
				newRow["GTxtVoucherNo"] = drDetails["ShortName"].ToString();
				newRow["GTxtLedger"] = drDetails["ProductDesc"].ToString();

				var qty = ReturnDouble(drDetails["Qty"].ToString());
				var returnDouble = ReturnDouble(drDetails["AdjustQty"].ToString());

				newRow["GTxtQty"] = qty;
				newRow["GTxtAdjustVoucher"] = drDetails["AdjustVoucherNo"].ToString();
				newRow["GTxtAdjustQty"] = returnDouble;
				newRow["GTxtBalanceQty"] = qty - returnDouble > 0 ? qty - returnDouble : 0.ToString();

				newRow["GTxtRate"] = drDetails["Rate"].ToString();
				newRow["GTxtAmount"] = drDetails["NetAmount"].ToString();
				newRow["IsGroup"] = 0;
				dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
				if (!GetReports.IncludeNarration) continue;
				newRow = dtReport.NewRow();
				newRow["GTxtLedger"] = "  Narr => " + drDetails["Narration"].GetString();
				newRow["IsGroup"] = 10;
				dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
			}

			var sumQty = dtVoucherDetails.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
			var sumAdjustQty = dtVoucherDetails.AsEnumerable().Sum(x => x["AdjustQty"].GetDecimal());
			var sumBalanceQty = sumAdjustQty - sumQty > 0 ? sumAdjustQty - sumQty : 0;
			var sumAmount = dtVoucherDetails.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());

			newRow = dtReport.NewRow();
			newRow["GTxtLedger"] = " VOUCHER TOTAL => ";
			newRow["GTxtQty"] = sumQty;
			newRow["GTxtAdjustQty"] = sumAdjustQty;
			newRow["GTxtBalanceQty"] = sumBalanceQty;
			newRow["GTxtAmount"] = sumAmount;
			newRow["IsGroup"] = 11;
			dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		}

		var dtTotal = dtReport.Select("IsGroup = '0' ").CopyToDataTable();
		var sumTotalQty = dtTotal.AsEnumerable().Sum(x => x["GTxtQty"].GetDecimal());
		var sumTotalAdjustQty = dtTotal.AsEnumerable().Sum(x => x["GTxtAdjustQty"].GetDecimal());
		var sumTotalBalanceQty = sumTotalAdjustQty - sumTotalQty > 0 ? sumTotalAdjustQty - sumTotalQty : 0;
		var sumTotalAmount = dtTotal.AsEnumerable().Sum(x => x["GTxtAmount"].GetDecimal());

		newRow = dtReport.NewRow();
		newRow["GTxtLedger"] = " GRAND TOTAL =>";
		newRow["GTxtQty"] = sumTotalQty;
		newRow["GTxtBalanceQty"] = sumTotalBalanceQty;
		newRow["GTxtAdjustQty"] = sumTotalAdjustQty;
		newRow["GTxtAmount"] = sumTotalAmount;
		newRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		return dtReport;
	}

	public DataTable ReturnOutstandingLedgerWiseReportDetails(DataTable dsRegister)
	{
		DataRow newRow;
		var dtReport = new DataTable("REPORT");
		dtReport.AddStringColumns(new[]
		{
			"GTxtDate",
			"GTxtMiti",
			"GTxtVoucherNo",
			"GTxtLedgerId",
			"GTxtLedger",
			"GTxtQty",
			"GTxtAdjustDate",
			"GTxtAdjustMiti",
			"GTxtAdjustVoucher",
			"GTxtAdjustQty",
			"GTxtBalanceQty",
			"GTxtRate",
			"GTxtAmount",
			"GTxtDueDays",
			"IsGroup"
		});

		var dtGroupLedger = dsRegister.AsEnumerable().GroupBy(r => new
		{
			voucherNo = r.Field<string>("Ledger")
		}).Select(g => g.First()).CopyToDataTable();
		foreach (DataRow drLedger in dtGroupLedger.Rows)
		{
			newRow = dtReport.NewRow();
			newRow["GTxtLedgerId"] = drLedger["LedgerId"].ToString();
			newRow["GTxtLedger"] = drLedger["Ledger"].ToString();
			newRow["IsGroup"] = 1;
			dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);

			var dtLedgerDetails = dsRegister.Select($"Ledger = '{drLedger["Ledger"]}'").CopyToDataTable();
			var dtGroupVoucher = dtLedgerDetails.AsEnumerable().GroupBy(r => new
			{
				voucherNo = r.Field<string>("VoucherNo")
			}).Select(g => g.First()).CopyToDataTable();
			foreach (DataRow drMaster in dtGroupVoucher.Rows)
			{
				var isFirst = true;
				newRow = dtReport.NewRow();
				newRow["GTxtDate"] = drMaster["VoucherDate"].ToString();
				newRow["GTxtMiti"] = drMaster["VoucherMiti"].ToString();
				newRow["GTxtVoucherNo"] = drMaster["VoucherNo"].ToString();
				newRow["IsGroup"] = 1;
				newRow["GTxtDueDays"] =
					DateTime.Now.Date.Subtract(drMaster["VoucherDate"].ToString().GetDateTime()).Days;
				var voucherNo = drMaster["VoucherNo"].ToString();
				var dtVoucherDetails = dsRegister.Select($"VoucherNo = '{voucherNo}'").CopyToDataTable();
				foreach (DataRow drDetails in dtVoucherDetails.Rows)
				{
					if (!isFirst) newRow = dtReport.NewRow();
					newRow["GTxtLedgerId"] = drDetails["ProductId"].ToString();
					newRow["GTxtLedger"] = drDetails["ProductDesc"].ToString();
					var qty = ReturnDouble(drDetails["Qty"].ToString());
					var returnDouble = ReturnDouble(drDetails["AdjustQty"].ToString());
					newRow["GTxtQty"] = qty;
					newRow["GTxtAdjustVoucher"] = drDetails["AdjustVoucherNo"].ToString();
					newRow["GTxtAdjustQty"] = returnDouble;
					newRow["GTxtBalanceQty"] = qty - returnDouble > 0 ? qty - returnDouble : 0.ToString();
					newRow["GTxtRate"] = drDetails["Rate"].ToString();
					newRow["GTxtAmount"] = drDetails["NetAmount"].ToString();
					newRow["IsGroup"] = 0;
					isFirst = false;
					dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
					var value = drDetails["Narration"].IsValueExits();
					if (GetReports.IncludeNarration && value)
					{
						newRow = dtReport.NewRow();
						newRow["GTxtLedger"] = "  Narr => " + drDetails["Narration"].GetString();
						newRow["IsGroup"] = 10;
						dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
					}
				}

				var sumQty = dtVoucherDetails.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
				var sumAdjustQty = dtVoucherDetails.AsEnumerable().Sum(x => x["AdjustQty"].GetDecimal());
				var sumBalanceQty = sumAdjustQty - sumQty > 0 ? sumAdjustQty - sumQty : 0;
				var sumAmount = dtVoucherDetails.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());

				newRow = dtReport.NewRow();
				newRow["GTxtLedger"] = " VOUCHER TOTAL => ";
				newRow["GTxtQty"] = sumQty;
				newRow["GTxtAdjustQty"] = sumAdjustQty;
				newRow["GTxtBalanceQty"] = sumBalanceQty;
				newRow["GTxtAmount"] = sumAmount;
				newRow["IsGroup"] = 22;
				dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
			}

			var sumLedgerQty = dtLedgerDetails.AsEnumerable().Sum(x => x["Qty"].GetDecimal());
			var sumLedgerAdjustQty = dtLedgerDetails.AsEnumerable().Sum(x => x["AdjustQty"].GetDecimal());
			var sumLedgerBalanceQty = sumLedgerQty - sumLedgerAdjustQty;
			sumLedgerBalanceQty = sumLedgerBalanceQty > 0 ? sumLedgerBalanceQty : 0;
			var sumLedgerAmount = dtLedgerDetails.AsEnumerable().Sum(x => x["NetAmount"].GetDecimal());

			newRow = dtReport.NewRow();
			newRow["GTxtLedger"] = " LEDGER TOTAL => ";
			newRow["GTxtQty"] = sumLedgerQty;
			newRow["GTxtAdjustQty"] = sumLedgerAdjustQty;
			newRow["GTxtBalanceQty"] = sumLedgerBalanceQty;
			newRow["GTxtAmount"] = sumLedgerAmount;
			newRow["IsGroup"] = 11;
			dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		}

		var dtTotal = dtReport.Select("IsGroup = '0' ").CopyToDataTable();
		var sumTotalQty = dtTotal.AsEnumerable().Sum(x => x["GTxtQty"].GetDecimal());
		var sumTotalAdjustQty = dtTotal.AsEnumerable().Sum(x => x["GTxtAdjustQty"].GetDecimal());
		var sumTotalBalanceQty = sumTotalAdjustQty - sumTotalQty > 0 ? sumTotalAdjustQty - sumTotalQty : 0;
		var sumTotalAmount = dtTotal.AsEnumerable().Sum(x => x["GTxtAmount"].GetDecimal());

		newRow = dtReport.NewRow();
		newRow["GTxtLedger"] = " GRAND TOTAL =>";
		newRow["GTxtQty"] = sumTotalQty;
		newRow["GTxtBalanceQty"] = sumTotalBalanceQty;
		newRow["GTxtAdjustQty"] = sumTotalAdjustQty;
		newRow["GTxtAmount"] = sumTotalAmount;
		newRow["IsGroup"] = 99;
		dtReport.Rows.InsertAt(newRow, dtReport.Rows.Count + 1);
		return dtReport;
	}

	public DataTable GetPartyOutstandingSummaryReport(bool isCustomer)
	{
		try
		{
			var cmdString = GetScriptForOutstandingDetailsReports(isCustomer);
			var dtOutStanding = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
			if (dtOutStanding.Rows.Count is 0) return new DataTable();
			var dtReport = ReturnOutstandingReportPartyWiseSummaryReports(dtOutStanding);
			return dtReport;
		}
		catch (Exception ex)
		{
			ex.DialogResult();
			return new DataTable();
		}
	}

	public DataTable GetPartyOutstandingDetailsReport(bool isCustomer)
	{
		try
		{
			var cmdString = GetReports.IncludeAdjustment
				? GetScriptForOutstandingDetailsIncludeAdjustmentReports(isCustomer)
				: GetScriptForOutstandingDetailsReports(isCustomer);
			var dtOutStanding = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
			if (dtOutStanding.Rows.Count is 0) return new DataTable();
			var dtReport = ReturnOutstandingReportPartyWiseDetails(dtOutStanding);
			return dtReport;
		}
		catch (Exception ex)
		{
			ex.DialogResult();
			return new DataTable();
		}
	}

	public DataTable GetOutstandingSummaryReport()
	{
		var cmdString = string.Empty;
		switch (GetReports.Module)
		{
			case "SQ":
				{
					cmdString = @$"
						SELECT sd.SO_Invoice VoucherNo,CONVERT(NVARCHAR(10),sm.Invoice_Date,103) VoucherDate,sm.Invoice_Miti VoucherMiti,sbm.SB_Invoice AdjustVoucher,sbm.Invoice_Date AdjustDate,sbm.Invoice_Miti AdjustMiti,gl.GLID LedgerId , gl.GLName Ledger, sd.P_Id ProductId,p.PShortName,p.PName Product,CAST(sd.Alt_Qty AS DECIMAL(18,{SysQtyLength})) AltQty,au.UnitCode AltUom, CAST(sd.Qty AS DECIMAL(18,{SysQtyLength})) Qty,u.UnitCode Unit,FORMAT(sd.Rate,'{SysAmountCommaFormat}') Rate, FORMAT(sd.B_Amount,'{SysAmountCommaFormat}') Amount,CAST(ISNULL(sb.Qty,0) AS DECIMAL(18,{SysQtyLength})) AdjustQty,CAST((sd.Qty -ISNULL(sb.Qty,0)) AS DECIMAL(18,{SysQtyLength})) BalanceQty
						FROM AMS.SO_Details sd
							 INNER JOIN AMS.SO_Master sm ON sm.SO_Invoice=sd.SO_Invoice
							 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
							 LEFT OUTER JOIN AMS.Product p ON p.PID = sd.P_Id
							 LEFT OUTER JOIN AMS.ProductUnit u ON u.UID = p.PUnit
							 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
							 LEFT OUTER JOIN AMS.SB_Details sb ON sb.SO_Invoice = sd.SO_Invoice  AND sb.P_Id = p.PID
							 LEFT OUTER JOIN AMS.SB_Master sbm ON sbm.SO_Invoice = sd.SO_Invoice
						WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}'  AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.CBranch_Id = {SysBranchId} AND sm.FiscalYearId = {SysFiscalYearId} ";
					cmdString += !GetReports.IncludeAdjustment ? @" AND sm.Invoice_Type <> 'POSTED' " : "";
					cmdString += @"
						ORDER BY sm.Invoice_Date ";
					break;
				}
			case "SO":
				{
					cmdString =
						@" SELECT sd.SO_Invoice VoucherNo,CONVERT(NVARCHAR(10),sm.Invoice_Date,103) VoucherDate,sm.Invoice_Miti VoucherMiti,sbm.SB_Invoice AdjustVoucher,sbm.Invoice_Date AdjustDate,sbm.Invoice_Miti AdjustMiti,gl.GLID LedgerId , gl.GLName Ledger, sd.P_Id ProductId,p.PShortName,p.PName Product, ";
					cmdString += !GetReports.IncludeAdjustment
						? @$" FORMAT(CAST( (sd.Alt_Qty - ISNULL(sb.Alt_Qty,0)) AS DECIMAL(18,6)),'{SysAmountCommaFormat}') AltQty, "
						: @$" FORMAT(CAST(sd.Alt_Qty AS DECIMAL(18,6)),'{SysAmountCommaFormat}') AltQty, ";
					cmdString += @" au.UnitCode AltUom, ";
					cmdString += !GetReports.IncludeAdjustment
						? @$" FORMAT(CAST( (sd.Qty - ISNULL(sb.Qty,0))  AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Qty,"
						: @$" FORMAT(CAST(sd.Qty AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Qty,";
					cmdString += @$" u.UnitCode Uom,FORMAT(sd.Rate,'{SysAmountCommaFormat}') Rate, ";
					cmdString += !GetReports.IncludeAdjustment
						? @$"FORMAT( ((sd.Qty - ISNULL(sb.Qty,0)) * sd.Rate) ,'{SysAmountCommaFormat}') Amount,"
						: @$"FORMAT(sd.B_Amount,'{SysAmountCommaFormat}') Amount,";
					cmdString += @$"
					FORMAT(CAST(ISNULL(sb.Qty,0) AS DECIMAL(18,{SysQtyLength})),'{SysAmountCommaFormat}') AdjustQty,FORMAT(CAST((sd.Qty -ISNULL(sb.Qty,0)) AS DECIMAL(18,{SysQtyLength})),'{SysAmountCommaFormat}') BalanceQty,0 IsGroup,DATEDIFF(DAY, sm.Invoice_Date, GETDATE()) AS DateDiff
						FROM AMS.SO_Details sd
							 INNER JOIN AMS.SO_Master sm ON sm.SO_Invoice=sd.SO_Invoice
							 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
							 LEFT OUTER JOIN AMS.Product p ON p.PID = sd.P_Id
							 LEFT OUTER JOIN AMS.ProductUnit u ON u.UID = p.PUnit
							 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
							 LEFT OUTER JOIN AMS.SB_Details sb ON sb.SO_Invoice = sd.SO_Invoice  AND sb.P_Id = p.PID
							 LEFT OUTER JOIN AMS.SB_Master sbm ON sbm.SO_Invoice = sd.SO_Invoice
						WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}'  AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.CBranch_Id = {SysBranchId} AND sm.FiscalYearId = {SysFiscalYearId} ";
					cmdString += !GetReports.IncludeAdjustment ? @" AND (sd.Qty - ISNULL(sb.Qty,0)) > 0 " : "";
					cmdString += @"
						ORDER BY sm.Invoice_Date,sd.SO_Invoice ";
					break;
				}
			case "SC":
				{
					cmdString = @$"
						SELECT sd.SO_Invoice VoucherNo,CONVERT(NVARCHAR(10),sm.Invoice_Date,103) VoucherDate,sm.Invoice_Miti VoucherMiti,sbm.SB_Invoice AdjustVoucher,sbm.Invoice_Date AdjustDate,sbm.Invoice_Miti AdjustMiti,gl.GLID LedgerId , gl.GLName Ledger, sd.P_Id ProductId,p.PShortName,p.PName Product, FORMAT(CAST(sd.Alt_Qty AS DECIMAL(18,6)),'{SysAmountCommaFormat}') AltQty,au.UnitCode AltUom, FORMAT(CAST(sd.Qty AS DECIMAL(18,6)),'{SysAmountCommaFormat}') Qty,u.UnitCode Uom,FORMAT(sd.Rate,'{SysAmountCommaFormat}') Rate, FORMAT(sd.B_Amount,'{SysAmountCommaFormat}') Amount,FORMAT(CAST(ISNULL(sb.Qty,0) AS DECIMAL(18,{SysQtyLength})),'{SysAmountCommaFormat}') AdjustQty,FORMAT(CAST((sd.Qty -ISNULL(sb.Qty,0)) AS DECIMAL(18,{SysQtyLength})),'{SysAmountCommaFormat}') BalanceQty,0 IsGroup,DATEDIFF(DAY, sm.Invoice_Date, GETDATE()) AS DateDiff
						FROM AMS.SO_Details sd
							 INNER JOIN AMS.SO_Master sm ON sm.SO_Invoice=sd.SO_Invoice
							 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
							 LEFT OUTER JOIN AMS.Product p ON p.PID = sd.P_Id
							 LEFT OUTER JOIN AMS.ProductUnit u ON u.UID = p.PUnit
							 LEFT OUTER JOIN AMS.ProductUnit au ON au.UID = p.PAltUnit
							 LEFT OUTER JOIN AMS.SB_Details sb ON sb.SO_Invoice = sd.SO_Invoice  AND sb.P_Id = p.PID
							 LEFT OUTER JOIN AMS.SB_Master sbm ON sbm.SO_Invoice = sd.SO_Invoice
						WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}'  AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.CBranch_Id = {SysBranchId} AND sm.FiscalYearId = {SysFiscalYearId} ";
					cmdString += !GetReports.IncludeAdjustment ? @" AND sm.Invoice_Type <> 'POSTED' " : "";
					cmdString += @"
						ORDER BY sm.Invoice_Date ";
					break;
				}
		}

		var dtOutStanding = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		if (dtOutStanding.Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" => ReturnOutstandingVoucherDateWiseReportSummary(dtOutStanding),
			"VOUCHER WISE" => ReturnOutstandingVoucherDateWiseReportSummary(dtOutStanding),
			"CUSTOMER WISE" or "VENDOR WISE" => ReturnOutstandingLedgerWiseReportSummary(dtOutStanding),
			_ => dtOutStanding
		};
		return dtReports;
	}

	public DataTable GetOutstandingDetailsReport()
	{
		var cmdString = string.Empty;
		switch (GetReports.Module)
		{
			case "SQ" when !GetReports.IncludeAdjustment:
				{
					break;
				}
			case "SO" when GetReports.IncludeAdjustment:
				{
					break;
				}
			case "SC":
				{
					cmdString = @$"
						SELECT sd.SC_Invoice VoucherNo,CONVERT(NVARCHAR,sm.DueDate,103) DueDate,CONVERT(NVARCHAR,sm.Invoice_Date,103) VoucherDate, sm.Invoice_Miti VoucherMiti, sm.Customer_ID LedgerId, gl.GLName Ledger, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, sd.Qty,CONVERT(NVARCHAR, sb1.Invoice_Date,103) AdjustDate,sb1.Invoice_Miti AdjustMiti ,sd1.SB_Invoice AdjustVoucherNo, ISNULL( sd1.Qty, 0 ) AdjustQty,
						  ISNULL( sd1.Rate, 0 ) Rate, ISNULL( sd1.B_Amount, 0 ) NetAmount, sd.Narration
						  FROM AMS.SC_Details sd
							   LEFT OUTER JOIN AMS.SC_Master sm ON sd.SC_Invoice = sm.SC_Invoice
							   LEFT OUTER JOIN AMS.SB_Details sd1 ON sd1.SC_Invoice = sd.SC_Invoice AND sd.P_Id = sd1.P_Id
							   LEFT OUTER JOIN	 AMS.SB_Master sb1 ON sb1.SB_Invoice = sd1.SB_Invoice
							   LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
							   LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_ID = gl.GLID
						WHERE sm.CBranch_Id IN ({SysBranchId}) AND SM.Invoice_Date BETWEEN '{Convert.ToDateTime(GetReports.FromAdDate):yyyy-MM-dd}' AND '{Convert.ToDateTime(GetReports.ToAdDate):yyyy-MM-dd}'";
					if (!GetReports.IncludeAdjustment)
						cmdString +=
							" AND sd.SC_Invoice NOT IN (SELECT sd2.SC_Invoice FROM AMS.SB_Details sd2 WHERE sd2.SC_Invoice IS NOT NULL AND sd2.SC_Invoice <>'') AND sm.Invoice_Type <>'POSTED' ";
					if (!string.IsNullOrEmpty(GetReports.LedgerId))
						cmdString += $" AND sm.Customer_Id IN ({GetReports.LedgerId}) ";
					cmdString += @"
						ORDER BY sm.Invoice_Date,sd.SC_Invoice ASC";
					break;
				}
		}

		var dtOutStanding = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
		if (dtOutStanding.Rows.Count is 0) return new DataTable();
		var dtReports = GetReports.RptMode.ToUpper() switch
		{
			"DATE WISE" or "VOUCHER NO" => ReturnOutstandingVoucherDateWiseReportDetails(dtOutStanding),
			"CUSTOMER WISE" => ReturnOutstandingLedgerWiseReportDetails(dtOutStanding),
			_ => new DataTable("Empty")
		};
		return dtReports;
	}

	#endregion **---------- RETURN DT OUTSTANDING REPORT DETAILS ----------**


	// OBJECT FOR THIS CLASS
	#region --------------- OBJECT ---------------
	public VmRegisterReports GetReports { get; set; }
	#endregion --------------- OBJECT ---------------
}