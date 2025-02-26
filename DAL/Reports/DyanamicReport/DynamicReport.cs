using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Reports.Interface;
using MrDAL.Reports.ViewModule;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MrDAL.Reports.DyanamicReport;

public class DynamicReport : IDynamicReport
{
    public DynamicReport()
    {
        Model = new VmDynamicReportTemplate();
        GetReports = new VmRegisterReports();
    }

    public VmDynamicReportTemplate Model { get; set; }
    public VmRegisterReports GetReports { get; set; }

    public string IsExistsTemplateName(string moduleName, string templateName)
    {
        var replace = templateName.Trim().Replace("'", "''");
        var cmdString =
            $"SELECT[FileName] from MASTER.AMS.ReportTemplate where [FileName] = '{replace}' and Reports_Type = '{moduleName}'";
        var dt = SqlExtensions.ExecuteDataSetOnMaster(cmdString).Tables[0];
        return dt.Rows.Count == 0 ? string.Empty : dt.Rows[0]["TemplateName"].ToString();
    }

    public int SaveTemplate()
    {
        var cmdString = new StringBuilder();
        if (Model.ActionTag is "NEW" or "SAVE")
        {
            cmdString.Append(
                "  INSERT INTO AMS.ReportTemplate(Report_Name, Reports_Type, FileName, FullPath, FromDate, ToDate, ReportSource, ReportCategory) \n");
            cmdString.Append(
                $" VALUES('{Model.Report_Name.Trim()}','{Model.Reports_Type}',@prFileBytes,'{Model.FullPath}','{Model.FromDate.GetSystemDate()}','{Model.ToDate.GetSystemDate()}','{Model.ReportSource}','{Model.ReportCategory}') \n");
        }
        else if (Model.ActionTag is "EDIT" or "UPDATE")
        {
            cmdString.Append(
                $" UPDATE AMS.ReportTemplate SET Report_Name='{Model.Report_Name}',Reports_Type='{Model.Reports_Type}',FileName= @prFileBytes, FullPath ='{Model.FullPath}', FromDate ='{Model.FromDate.GetSystemDate()}', ToDate ='{Model.ToDate.GetSystemDate()}', ReportSource ='{Model.ReportSource}', ReportCategory='{Model.ReportCategory}'  WHERE ID = {Model.ID} \n");
        }
        else if (Model.ActionTag == "DELETE")
        {
            cmdString.Append($" DELETE FROM AMS.ReportTemplate WHERE ID = {Model.ID} \n");
        }

        //using var conn = GetConnection.GetConnectionMaster();
        //var response = conn.Execute(cmdString.ToString(), new { prFileBytes = Model.FileName });

        var result =
            SqlExtensions.ExecuteNonQueryOnMaster(cmdString.ToString(),
                new SqlParameter("@prFileBytes", Model.FileName));
        return result;
    }

    public DataTable ListTemplateType(string reportCategory, int templateId, string reportsType)
    {
        var cmdString = new StringBuilder();
        cmdString.Append($"SELECT * FROM AMS.ReportTemplate rt WHERE ReportCategory='{reportCategory}'\n");
        if (templateId > 0) cmdString.Append($"and ID={templateId}");
        if (reportsType.IsValueExits()) cmdString.Append($"and Reports_Type='{reportsType}'");
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString.ToString()).Tables[0];
    }

    public DataTable ListAllTemplate()
    {
        var cmdString = new StringBuilder();
        cmdString.Append("SELECT * FROM MASTER.AMS.ReportTemplate rt \n");
        return SqlExtensions.ExecuteDataSetOnMaster(cmdString.ToString()).Tables[0];
    }

    // SALES REGISTER REPORTS
    public DataTable GetSalesInvoiceRegisterSummaryReports()
    {
        var dtMaster = new DataTable();
        var dtTerm = new DataTable();
        var cmdString = $@"
			SELECT sm.SB_Invoice [VOUCHER NO], CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) [VOUCHER DATE], sm.Invoice_Miti [VOUCHER MITI], CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice + ' (' + sm.PB_Vno + ' )' END [PARTY NO], CASE WHEN gl.GLType IN ('Bank','Cash') AND sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN  sm.Party_Name ELSE gl.GLName END [CUSTOMER NAME],  sm.B_Amount [BASIC AMOUNT], sm.LN_Amount [NET AMOUNT], UPPER ( sm.Enter_By ) [ENTER BY],ISNULL ( c.CCode, 'NO-COUNTER' ) [COUNTER], ISNULL ( d.DName, 'NO-DEPARTMENT' ) [DEPARTMENT], ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) [LEDGER AGENT], ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) [DOC AGENT], ISNULL ( a.AreaName, 'NO-AREA' ) [AREA NAME] ,sm.Remarks [REMARKS], sm.Invoice_Type [INVOICE TYPE], UPPER ( sm.Payment_Mode ) [PAYMENT MODE]
			FROM AMS.SB_Master sm
				LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND sm.fiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ( sm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL) AND sm.R_Invoice = 0
			ORDER BY sm.Invoice_Date,CAST(AMS.GetNumericValue(sm.SB_Invoice) AS NUMERIC);

			SELECT st1.SB_VNo [VOUCHER NO], st.ST_Sign, st1.ST_Id TermId, UPPER(st.ST_Name) TermDesc, Rate Rate, SUM(st1.Amount) AMOUNT
			FROM AMS.SB_Term st1
			    LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
			    LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
			WHERE st1.Term_Type <> 'B' and sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({ObjGlobal.SysBranchId}) AND sm.fiscalYearId IN({ObjGlobal.SysFiscalYearId}) AND(sm.CUnit_Id = '{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL) AND sm.R_Invoice = 0
			GROUP BY st1.SB_VNo, st1.ST_Id, st.ST_Name, Rate, st.ST_Sign
			ORDER BY st1.SB_VNo,TermId; ";
        var dsSales = SqlExtensions.ExecuteDataSet(cmdString);
        if (dsSales.Tables.Count > 0)
        {
            var column = 6;
            dtMaster = dsSales.Tables[0];
            if (dsSales.Tables.Count > 1) dtTerm = dsSales.Tables[1];
            if (dtTerm.RowsCount() > 0)
            {
                var term = GetPurchaseSalesTermName();
                if (term is { Rows: { Count: > 0 } })
                    foreach (DataRow row in term.Rows)
                    {
                        if (ObjGlobal.SalesVatTermId.Equals(row["TermId"].GetInt()))
                        {
                            dtMaster.AddColumn("TAXABLE AMOUNT", typeof(decimal)).SetOrdinal(column);
                            column++;
                        }

                        dtMaster.AddColumn(row["TermDesc"].ToString(), typeof(decimal)).SetOrdinal(column);
                        column++;
                    }

                foreach (DataRow drMaster in dtMaster.Rows)
                {
                    var voucherNo = drMaster["VOUCHER NO"].GetString();
                    var exists = dtTerm.AsEnumerable().Any(c => c.Field<string>("VOUCHER NO").Equals(voucherNo));
                    if (!exists) continue;
                    var filterTerm = dtTerm?.Select($"[VOUCHER NO] ='{drMaster["VOUCHER NO"]}' ").CopyToDataTable();
                    foreach (DataRow drTerm in filterTerm.Rows)
                        dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)].SetField(drTerm["TermDesc"].ToString(),
                            drTerm["Amount"].ToString());
                }
            }
        }

        return dtMaster;
    }

    public DataTable GetSalesInvoiceRegisterDetailsReports()
    {
        var dtMaster = new DataTable();
        var dtTerm = new DataTable();
        var cmdString = $@"
			SELECT sm.SB_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice + ' (' + sm.PB_Vno + ' )' END VoucherNoWithRef, sm.Customer_Id LedgerId, CASE WHEN gl.GLType IN ('Bank','Cash') AND sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN  sm.Party_Name ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode, sm.B_Amount BasicAmount, sm.LN_Amount NetAmount, UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName
			 FROM AMS.SB_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND sm.fiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ( sm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL) AND sm.R_Invoice = 0
            ORDER BY sm.Invoice_Date,CAST(AMS.GetNumericValue(sm.SB_Invoice) AS NUMERIC);

			SELECT st1.SB_VNo VoucherNo, st.ST_Sign, st1.ST_Id TermId, UPPER(st.ST_Name) TermDesc, Rate Rate, SUM(st1.Amount) Amount
            FROM AMS.SB_Term st1
                 LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
                 LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
			WHERE st1.Term_Type <> 'B' and sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({ObjGlobal.SysBranchId}) AND sm.fiscalYearId IN({ObjGlobal.SysFiscalYearId}) AND(sm.CUnit_Id = '{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL) AND sm.R_Invoice = 0
			GROUP BY st1.SB_VNo, st1.ST_Id, st.ST_Name, Rate, st.ST_Sign
			ORDER BY st1.SB_VNo,TermId; ";
        var dsSales = SqlExtensions.ExecuteDataSet(cmdString);
        if (dsSales.Tables.Count > 0)
        {
            dtMaster = dsSales.Tables[0];
            if (dsSales.Tables.Count > 1) dtTerm = dsSales.Tables[1];
            if (dtTerm.RowsCount() > 0)
            {
                var term = GetPurchaseSalesTermName();
                if (term is { Rows: { Count: > 0 } })
                    foreach (DataRow row in term.Rows)
                    {
                        if (ObjGlobal.SalesVatTermId.Equals(row["TermId"].GetInt()))
                            dtMaster.AddColumn("dt_Taxable", typeof(decimal));
                        dtMaster.AddColumn(row["TermDesc"].ToString(), typeof(decimal));
                    }

                foreach (DataRow drMaster in dtMaster.Rows)
                {
                    var voucherNo = drMaster["VoucherNo"].GetString();
                    var exists = dtTerm.AsEnumerable().Any(c => c.Field<string>("VoucherNo").Equals(voucherNo));
                    if (!exists) continue;
                    var filterTerm = dtTerm?.Select($"VoucherNo ='{drMaster["VoucherNo"]}' ").CopyToDataTable();
                    foreach (DataRow drTerm in filterTerm.Rows)
                        dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)].SetField(drTerm["TermDesc"].ToString(),
                            drTerm["Amount"].ToString());
                }
            }
        }

        return dtMaster;
    }

    public DataTable GetSalesInvoiceRegisterYearWiseReport()
    {
        const string cmdString = @"
            SELECT CONVERT(NVARCHAR(10), sm.Invoice_Date,103) VoucherDate,sm.Invoice_Miti VoucherMiti,sm.SB_Invoice VoucherNo,gl.GLName LedgerDesc, gl.PanNo LedgerPan, gl.PhoneNo LedgerNumber, gl.GLAddress LedgerAddress, FORMAT(sm.LN_Amount,'##,###.00') InvoiceAmount, sm.Remarks
            FROM AMS.SB_Master sm
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
	             WHERE sm.LN_Amount > 0 AND gl.GLType IN ('Customer','Vendor') AND ISNULL(sm.R_Invoice,0) = 0
            ORDER BY LedgerDesc, sm.FiscalYearId, sm.Invoice_Date; ";
        var report = SqlExtensions.ExecuteDataSet(cmdString);
        return report.Tables.Count > 0 ? report.Tables[0] : new DataTable();
    }

    public DataTable GetSalesInvoiceRegisterPartialPaymentReport()
    {
        var cmdString = $@"
			SELECT sm.SB_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice + ' (' + sm.PB_Vno + ' )' END VoucherNoWithRef,ISNULL(ss.LedgerId,sm.Customer_Id) LedgerId,ISNULL(sm.Party_Name,gl.GLName) LedgerDesc, sm.Remarks Remarks,CASE WHEN sm.Invoice_Mode = 'NORMAL' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER( ISNULL(ss.PaymentMode, sm.Payment_Mode)) Payment_Mode,ISNULL(ss.Amount,sm.LN_Amount) Amount, UPPER ( sm.Enter_By ) EnterBy, sm.CounterId FROM AMS.SB_Master sm
				LEFT OUTER JOIN AMS.InvoiceSettlement ss ON ss.SB_Invoice = sm.SB_Invoice
				LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID = ISNULL(ss.LedgerId,sm.Customer_Id)
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.R_Invoice = 0  AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)
			ORDER BY sm.Invoice_Date,CAST(AMS.GetNumericValue(sm.SB_Invoice) AS NUMERIC); ";
        var report = SqlExtensions.ExecuteDataSet(cmdString);
        return report.Tables.Count > 0 ? report.Tables[0] : new DataTable();
    }

    public DataSet GetSalesInvoiceMasterDetailsReports()
    {
        var cmdString = $@"
			SELECT sm.SB_Invoice VoucherNo, CONVERT ( NVARCHAR, sm.Invoice_Date, 103 ) VoucherDate, sm.Invoice_Miti VoucherMiti, CASE WHEN sm.PB_Vno IS NULL THEN sm.SB_Invoice ELSE sm.SB_Invoice + ' (' + sm.PB_Vno + ' )' END VoucherNoWithRef, sm.Customer_Id ledgerId, CASE WHEN sm.Party_Name IS NOT NULL AND sm.Party_Name <> '' THEN gl.GLName + ' (' + sm.Party_Name + ' )' ELSE gl.GLName END LedgerDesc, sm.Remarks Remarks, CASE WHEN sm.Invoice_Mode = 'Normal' THEN 'SB' ELSE sm.Invoice_Mode END Invoice_Mode, sm.Invoice_Type Invoice_Type, UPPER ( sm.Payment_Mode ) Payment_Mode, sm.B_Amount BasicAmount, sm.LN_Amount NetAmount, UPPER ( sm.Enter_By ) EnterBy, sm.CounterId, ISNULL ( c.CCode, 'NO-COUNTER' ) Counter, sm.Cls1 DepartmentId, ISNULL ( d.DName, 'NO-DEPARTMENT' ) Department, gl.AgentId LedgerAgentId, ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) LedgerAgent, sm.Agent_Id DocAgentId, ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) DocAgent, gl.AreaId, ISNULL ( a.AreaName, 'NO-AREA' ) AreaName
			 FROM AMS.SB_Master sm
				  LEFT OUTER JOIN AMS.GeneralLedger gl ON sm.Customer_Id = gl.GLID
				  LEFT OUTER JOIN AMS.Counter c ON sm.CounterId = c.CId
				  LEFT OUTER JOIN AMS.Department d ON sm.Cls1 = d.DId
				  LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
				  LEFT OUTER JOIN AMS.JuniorAgent ja1 ON sm.Agent_Id = ja1.AgentId
				  LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN ({GetReports.BranchId}) AND sm.fiscalYearId IN ({GetReports.FiscalYearId}) AND ( sm.CUnit_Id ='{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
        cmdString += $@"
			ORDER BY sm.Invoice_Date,sm.SB_Invoice;
		   SELECT sd.SB_Invoice VoucherNo, sd.Invoice_SNo SerialNo, p.PShortName ShortName, sd.P_Id ProductId, p.PName ProductDesc, sd.Qty StockQty, pu.UnitCode UOM, sd.Rate Rate, sd.B_Amount BasicAmount,sd.N_Amount, sd.Narration Narration
			 FROM AMS.SB_Details sd
				  LEFT OUTER JOIN AMS.SB_Master sm ON sd.SB_Invoice = sm.SB_Invoice
				  LEFT OUTER JOIN AMS.Product p ON sd.P_Id = p.PID
				  LEFT OUTER JOIN AMS.ProductUnit pu ON sd.Unit_Id = pu.UID
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)";
        cmdString += @"
			ORDER BY sm.Invoice_Date,sm.SB_Invoice,sd.P_Id ; ";
        cmdString += @"
				SELECT SB_VNo VoucherNo, st1.ST_Id TermId, UPPER ( st.ST_Name ) TermDesc,Rate Rate,st1.Amount Amount, st1.Product_Id ProductId,st.ST_Sign TermSign,st1.Term_Type TermType ";
        cmdString += $@"
				 FROM AMS.SB_Term st1
					  LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo = sm.SB_Invoice
					  LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id = st.ST_ID
				WHERE st1.Term_Type <> 'B' and sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.CBranch_Id IN({GetReports.BranchId}) AND sm.fiscalYearId IN({GetReports.FiscalYearId}) AND(sm.CUnit_Id = '{GetReports.CompanyUnitId}' OR CUnit_Id IS NULL)
				GROUP BY SB_VNo,st1.ST_ID,st.ST_Name,Rate,st.Order_No
            ORDER BY SB_VNo,st.Order_No,st1.Product_Id;";
        var report = SqlExtensions.ExecuteDataSet(cmdString);
        return report;
    }

    public DataTable GetSalesInvoiceRegisterProductLedgerReports()
    {
        var dtMaster = new DataTable();
        var dtTerm = new DataTable();
        var cmdString = $@"
			SELECT gl.GLCode [SHORT NAME],gl.GLName [LEDGER NAME],CAST(SUM(sd.Qty) AS DECIMAL(18,{ObjGlobal.SysQtyLength}))  QTY, CAST(SUM(sd.B_Amount) AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [BASIC AMOUNT], CAST(SUM(sd.N_Amount) AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [NET AMOUNT]
            FROM AMS.SB_Details sd
                 INNER JOIN AMS.SB_Master sm ON sm.SB_Invoice=sd.SB_Invoice
                 LEFT OUTER JOIN AMS.Product p ON sd.P_Id=p.PID
                 LEFT OUTER JOIN AMS.ProductGroup pg ON pg.PGrpId=p.PGrpId
                 LEFT OUTER JOIN AMS.ProductSubGroup psg ON psg.PSubGrpId=p.PSubGrpId
	             LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID= ISNULL(p.PSL,{ObjGlobal.SalesLedgerId})
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.R_Invoice = 0  AND sm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND sm.fiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ( sm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR sm.CUnit_Id IS NULL)
			GROUP BY ISNULL(p.PSL,{ObjGlobal.SalesLedgerId}),gl.GLName,gl.GLCode
            ORDER BY gl.GLName;

			SELECT st.ST_Sign, st1.ST_Id TermId, UPPER(st.ST_Name) TermDesc, SUM(st1.Amount) Amount,ISNULL(p.PSL,{ObjGlobal.SalesLedgerId}) LedgerId,gl.GLName [LEDGER NAME],gl.GLCode [SHORT NAME]
            FROM AMS.SB_Term st1
                 LEFT OUTER JOIN AMS.SB_Master sm ON st1.SB_VNo=sm.SB_Invoice
                 LEFT OUTER JOIN AMS.ST_Term st ON st1.ST_Id=st.ST_ID
                 LEFT OUTER JOIN AMS.Product p ON p.PID=st1.Product_Id
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ISNULL(p.PSL,{ObjGlobal.SalesLedgerId})
			WHERE st1.Term_Type <> 'B' and sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND sm.Invoice_Type <> 'Cancel' AND sm.R_Invoice = 0  AND sm.CBranch_Id IN({ObjGlobal.SysBranchId}) AND sm.fiscalYearId IN({ObjGlobal.SysFiscalYearId}) AND(sm.CUnit_Id = '{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL)
            GROUP BY st1.ST_Id, st.ST_Name, st.ST_Sign,ISNULL(p.PSL,{ObjGlobal.SalesLedgerId}),gl.GLName,gl.GLCode
            ORDER BY gl.GLName; ";
        var dsSales = SqlExtensions.ExecuteDataSet(cmdString);
        if (dsSales.Tables.Count > 0)
        {
            var column = 4;
            dtMaster = dsSales.Tables[0];
            if (dsSales.Tables.Count > 1) dtTerm = dsSales.Tables[1];
            if (dtTerm.RowsCount() > 0)
            {
                var term = GetPurchaseSalesTermName();
                if (term is { Rows: { Count: > 0 } })
                    foreach (DataRow row in term.Rows)
                    {
                        if (ObjGlobal.SalesVatTermId.Equals(row["TermId"].GetInt()))
                        {
                            dtMaster.AddColumn("TAXABLE AMOUNT", typeof(decimal)).SetOrdinal(column);
                            column++;
                        }

                        dtMaster.AddColumn(row["TermDesc"].ToString(), typeof(decimal)).SetOrdinal(column);
                        column++;
                    }

                foreach (DataRow drMaster in dtMaster.Rows)
                {
                    var voucherNo = drMaster["LEDGER NAME"].GetString();
                    var exists = dtTerm.AsEnumerable().Any(c => c.Field<string>("LEDGER NAME").Equals(voucherNo));
                    if (!exists) continue;
                    var filterTerm = dtTerm?.Select($"[LEDGER NAME] ='{drMaster["LEDGER NAME"]}' ").CopyToDataTable();
                    var basicAmount = dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)]["BASIC AMOUNT"].GetDecimal();
                    foreach (DataRow drTerm in filterTerm.Rows)
                    {
                        if (ObjGlobal.SalesVatTermId.Equals(drTerm["TermId"].GetInt()))
                        {
                            var taxable = drTerm["Amount"].GetDecimal() / 0.13.GetDecimal();
                            dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)]
                                .SetField("TAXABLE AMOUNT", taxable.GetDecimalString());
                        }

                        dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)].SetField(drTerm["TermDesc"].ToString(),
                            drTerm["Amount"].GetDecimalString());
                        basicAmount = drTerm["ST_Sign"].ToString().Equals("+")
                            ? basicAmount + drTerm["Amount"].GetDecimal()
                            : basicAmount - drTerm["Amount"].GetDecimal();
                    }

                    dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)]
                        .SetField("NET AMOUNT", basicAmount.GetDecimalString());
                }
            }
        }

        return dtMaster;
    }

    public DataTable GetSalesInvoiceRegisterTableWiseReports()
    {
        var cmdString = $@"
            SELECT sm.Invoice_Date [DATE], sm.Invoice_Miti [MITI], sm.SB_Invoice [BILL NO], CONVERT(TIME, sm.Invoice_Time) [TIMING], CASE WHEN tm.TableType='T' THEN 'TAKE AWAY' WHEN tm.TableType='R' THEN 'DELIVERY' ELSE 'DINING' END [SALES MODE], sm.Payment_Mode [PAYMENT], CASE WHEN tm.TableType='D' THEN CAST(sm.N_Amount AS DECIMAL(18,{ObjGlobal.SysAmountLength})) ELSE CAST(0 AS DECIMAL(18,{ObjGlobal.SysAmountLength})) END [DINING], CASE WHEN tm.TableType='R' THEN CAST(sm.N_Amount AS DECIMAL(18,{ObjGlobal.SysAmountLength})) ELSE 0 END [DELIVERY], CASE WHEN tm.TableType='T' THEN CAST(sm.N_Amount AS DECIMAL(18,{ObjGlobal.SysAmountLength})) ELSE 0 END [TAKE AWAY],CAST( ((ISNULL(vat.VatAmount, 0)/ 0.13)-ISNULL(sc.ServiceCharge, 0)) AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [FOOD & BEVERAGE],CAST(ISNULL(sc.ServiceCharge, 0) AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [SERVICE CHARGE],CAST( ISNULL(vat.VatAmount, 0)/ 0.13 AS DECIMAL(18,2)) [VAT-ABLE SALES],CAST( ISNULL(vat.VatAmount, 0) AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [VAT], CAST(sm.LN_Amount AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [GRAND TOTAL]
			FROM AMS.SB_Master sm
			     LEFT OUTER JOIN(SELECT st.SB_VNo, SUM(st.Amount) ServiceCharge
			                     FROM AMS.SB_Term st
			                          LEFT OUTER JOIN AMS.SB_Master sm1 ON sm1.SB_Invoice=st.SB_VNo
			                     WHERE st.ST_Id IN(SELECT sst.SBServiceCharge FROM AMS.SalesSetting sst)AND st.Term_Type<>'BT' AND sm1.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			                     GROUP BY st.SB_VNo) AS sc ON sc.SB_VNo=sm.SB_Invoice
			     LEFT OUTER JOIN(SELECT st.SB_VNo, SUM(st.Amount) VatAmount
			                     FROM AMS.SB_Term st
			                          LEFT OUTER JOIN AMS.SB_Master sm1 ON sm1.SB_Invoice=st.SB_VNo
			                     WHERE st.ST_Id IN(SELECT sst.SBVatTerm FROM AMS.SalesSetting sst) AND st.Term_Type <> 'BT' AND sm1.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			                     GROUP BY st.SB_VNo) AS vat ON vat.SB_VNo=sm.SB_Invoice
			     LEFT OUTER JOIN AMS.TableMaster tm ON tm.TableId=sm.TableId
			WHERE sm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' ";
        var report = SqlExtensions.ExecuteDataSet(cmdString);
        return report.Tables.Count > 0 ? report.Tables[0] : new DataTable();
    }

    public DataTable GetSalesInvoiceVatRegisterReports()
    {
        var cmdString = $@"
			SELECT CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], dm.AD_Months [AD_MONTHS], dm.BS_Months [BS_MONTHS], pm.Invoice_Miti [VOUCHER MITI], pm.SB_Invoice [VOUCHER NO], gl.GLName AS [PARTY LEDGER], gl.PanNo [PAN NO], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [INVOICE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END))ELSE 0 END AS [TAX EXEMPTED], CASE WHEN pm.Invoice_Type='EXPORT' AND pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [EXPORT SALES], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)))ELSE 0 END AS [VAT AMOUNT]
			FROM AMS.SB_Master pm
			     LEFT OUTER JOIN(SELECT vat.SB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
			                     FROM(SELECT pt.SB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id={ObjGlobal.SalesVatTermId}) THEN SUM(pt.Amount)
			                                            WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id={ObjGlobal.SalesVatTermId}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={ObjGlobal.SalesVatTermId} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
			                          FROM AMS.SB_Term pt
			                               LEFT OUTER JOIN AMS.SB_Master stm ON stm.SB_Invoice=pt.SB_VNo
			                               LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
			                          WHERE pt.Term_Type<>'BT' AND stm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			                          GROUP BY pt.SB_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
			                     GROUP BY vat.SB_VNo) AS term ON pm.SB_Invoice=term.SB_VNo
			     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
			     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			GROUP BY pm.SB_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Customer_ID, gl.GLName, gl.PanNo, pm.Invoice_Type, dm.AD_Months, dm.BS_Months, pm.R_Invoice
			ORDER BY AMS.GetNumericValue(pm.SB_Invoice) ASC, pm.Invoice_Date, pm.Invoice_Miti;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSalesInvoiceVatRegisterCustomerWise()
    {
        var cmdString = $@"
			SELECT gl.GLName AS [PARTY LEDGER], gl.PanNo [PAN NO], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [INVOICE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END))ELSE 0 END AS [TAX EXEMPTED], CASE WHEN pm.Invoice_Type='EXPORT' AND pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [EXPORT SALES], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)))ELSE 0 END AS [VAT AMOUNT]
			FROM AMS.SB_Master pm
			     LEFT OUTER JOIN(SELECT vat.SB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
			                     FROM(SELECT pt.SB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id={ObjGlobal.SalesVatTermId}) THEN SUM(pt.Amount)
			                                            WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id={ObjGlobal.SalesVatTermId}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={ObjGlobal.SalesVatTermId} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
			                          FROM AMS.SB_Term pt
			                               LEFT OUTER JOIN AMS.SB_Master stm ON stm.SB_Invoice=pt.SB_VNo
			                               LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
			                          WHERE pt.Term_Type<>'BT' AND stm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			                          GROUP BY pt.SB_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
			                     GROUP BY vat.SB_VNo) AS term ON pm.SB_Invoice=term.SB_VNo
			     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
			     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			GROUP BY pm.Customer_ID, gl.GLName, gl.PanNo, pm.Invoice_Type,pm.R_Invoice
			ORDER BY gl.GLName;";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetSalesInvoiceVatRegisterIncludeReturnReports()
    {
        var cmdString = $@"
            SELECT vat.[INVOICE TYPE], vat.[VOUCHER DATE], vat.AD_MONTHS, vat.BS_MONTHS, vat.[VOUCHER MITI], vat.[VOUCHER NO], vat.[PARTY LEDGER], vat.[PAN NO],  vat.[INVOICE AMOUNT], vat.[TAX EXEMPTED],vat.[EXPORT SALES], vat.[TAXABLE AMOUNT], vat.[VAT AMOUNT]
			FROM(SELECT 'SALES' [INVOICE TYPE], CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], dm.AD_Months [AD_MONTHS], dm.BS_Months [BS_MONTHS], pm.Invoice_Miti [VOUCHER MITI], pm.SB_Invoice [VOUCHER NO], gl.GLName AS [PARTY LEDGER], gl.PanNo [PAN NO], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [INVOICE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END))ELSE 0 END AS [TAX EXEMPTED], CASE WHEN pm.Invoice_Type='EXPORT' AND pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [EXPORT SALES], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)))ELSE 0 END AS [VAT AMOUNT]
			     FROM AMS.SB_Master pm
			          LEFT OUTER JOIN(SELECT vat.SB_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
			                          FROM(SELECT pt.SB_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id={ObjGlobal.SalesVatTermId}) THEN SUM(pt.Amount)
			                                                 WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id={ObjGlobal.SalesVatTermId}{ObjGlobal.SalesVatTermId}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={ObjGlobal.SalesVatTermId} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
			                               FROM AMS.SB_Term pt
			                                    LEFT OUTER JOIN AMS.SB_Master stm ON stm.SB_Invoice=pt.SB_VNo
			                                    LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
			                               WHERE pt.Term_Type<>'BT' AND stm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			                               GROUP BY pt.SB_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
			                          GROUP BY vat.SB_VNo) AS term ON pm.SB_Invoice=term.SB_VNo
			          LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
			          LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			     WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			     GROUP BY pm.SB_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Customer_ID, gl.GLName, gl.PanNo, pm.Invoice_Type, dm.AD_Months, dm.BS_Months, pm.R_Invoice
			     UNION ALL
			     SELECT 'SALES RETURN' [INVOICE TYPE], CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], dm.AD_Months [AD_MONTHS], dm.BS_Months [BS_MONTHS], pm.Invoice_Miti [VOUCHER MITI], pm.SR_Invoice [VOUCHER NO], gl.GLName AS [PARTY LEDGER], gl.PanNo [PAN NO], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [INVOICE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END))ELSE 0 END AS [TAX EXEMPTED], CASE WHEN pm.Invoice_Type='EXPORT' AND pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [EXPORT SALES], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)))ELSE 0 END AS [VAT AMOUNT]
			     FROM AMS.SR_Master pm
			          LEFT OUTER JOIN(SELECT vat.SR_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
			                          FROM(SELECT pt.SR_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id={ObjGlobal.SalesVatTermId}) THEN SUM(pt.Amount)
			                                                 WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id={ObjGlobal.SalesVatTermId}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={ObjGlobal.SalesVatTermId} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
			                               FROM AMS.SR_Term pt
			                                    LEFT OUTER JOIN AMS.SR_Master stm ON stm.SR_Invoice=pt.SR_VNo
			                                    LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
			                               WHERE pt.Term_Type<>'BT' AND stm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			                               GROUP BY pt.SR_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
			                          GROUP BY vat.SR_VNo) AS term ON pm.SR_Invoice=term.SR_VNo
			          LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
			          LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			     WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			     GROUP BY pm.SR_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Customer_ID, gl.GLName, gl.PanNo, pm.Invoice_Type, dm.AD_Months, dm.BS_Months, pm.R_Invoice)vat
			ORDER BY AMS.GetNumericValue(vat.[VOUCHER NO]) ASC, vat.[VOUCHER DATE], vat.[INVOICE TYPE]; ";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetSalesReturnVatRegisterReports()
    {
        var cmdString = $@"
			SELECT CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], dm.AD_Months [AD_MONTHS], dm.BS_Months [BS_MONTHS], pm.Invoice_Miti [VOUCHER MITI], pm.SR_Invoice [VOUCHER NO], gl.GLName AS [PARTY LEDGER], gl.PanNo [PAN NO], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [INVOICE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(CASE WHEN(pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0))>=1 THEN pm.LN_Amount-ROUND(ISNULL(term.VatAmount, 0)/ 0.13, 18, 0)-ISNULL(term.VatAmount, 0)ELSE 0 END))ELSE 0 END AS [TAX EXEMPTED], CASE WHEN pm.Invoice_Type='EXPORT' AND pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(pm.LN_Amount))ELSE 0 END AS [EXPORT SALES], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.R_Invoice=0 THEN CONVERT(DECIMAL(18, 2), SUM(ISNULL(term.VatAmount, 0)))ELSE 0 END AS [VAT AMOUNT]
			FROM AMS.SR_Master pm
			     LEFT OUTER JOIN(SELECT vat.SR_VNo, SUM(vat.termAmout) AS termAmout, SUM(vat.VatAmount) AS VatAmount
			                     FROM(SELECT pt.SR_VNo, CASE WHEN pt1.Order_No<(SELECT pt3.Order_No FROM AMS.ST_Term pt3 WHERE pt3.ST_Id={ObjGlobal.SalesVatTermId}) THEN SUM(pt.Amount)
			                                            WHEN pt1.ST_Sign='-' AND pt1.Order_No<(SELECT pt2.Order_No FROM AMS.ST_Term pt2 WHERE pt2.ST_Id={ObjGlobal.SalesVatTermId}) THEN -SUM(pt.Amount)ELSE 0 END AS termAmout, CASE WHEN pt.ST_Id={ObjGlobal.SalesVatTermId} THEN SUM(pt.Amount)ELSE 0 END AS VatAmount
			                          FROM AMS.SR_Term pt
			                               LEFT OUTER JOIN AMS.SR_Master stm ON stm.SR_Invoice=pt.SR_VNo
			                               LEFT OUTER JOIN AMS.ST_Term pt1 ON pt.ST_Id=pt1.ST_Id
			                          WHERE pt.Term_Type<>'BT' AND stm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			                          GROUP BY pt.SR_VNo, pt.ST_Id, pt1.ST_Sign, pt1.Order_No) AS vat
			                     GROUP BY vat.SR_VNo) AS term ON pm.SR_Invoice=term.SR_VNo
			     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Customer_ID=gl.GLID
			     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' --AND (pm.R_Invoice = 0 OR pm.Invoice_Type <> 'CANCEL')
			GROUP BY pm.SR_Invoice, pm.Invoice_Date, pm.Invoice_Miti, pm.Customer_ID, gl.GLName, gl.PanNo, pm.Invoice_Type, dm.AD_Months, dm.BS_Months, pm.R_Invoice
			ORDER BY pm.Invoice_Date, AMS.Retrun_Numeric(pm.SR_Invoice);";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    // PURCHASE REGISTER REPORTS
    public DataTable GetPurchaseInvoiceRegisterSummaryReports()
    {
        var dtMaster = new DataTable();
        var dtTerm = new DataTable();
        var cmdString = $@"
			SELECT pm.PB_Invoice [VOUCHER NO], CONVERT ( NVARCHAR, pm.Invoice_Date, 103 ) [VOUCHER DATE], pm.Invoice_Miti [VOUCHER MITI], CASE WHEN pm.PB_Vno IS NULL THEN pm.PB_Invoice ELSE pm.PB_Invoice + ' (' + pm.PB_Vno + ' )' END [PARTY NO], CASE WHEN gl.GLType IN ('Bank','Cash') AND pm.Party_Name IS NOT NULL AND pm.Party_Name <> '' THEN  pm.Party_Name ELSE gl.GLName END [SUPPLIERS NAME],CAST( pm.B_Amount AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [BASIC AMOUNT],CAST( pm.LN_Amount  AS DECIMAL(18,{ObjGlobal.SysAmountLength})) [NET AMOUNT], UPPER ( pm.Enter_By ) [ENTER BY],ISNULL ( d.DName, 'NO-DEPARTMENT' ) [DEPARTMENT], ISNULL ( ja.AgentName, 'NO-LEDGER AGENT' ) [LEDGER AGENT], ISNULL ( ja1.AgentName, 'NO-DOC AGENT' ) [DOC AGENT], ISNULL ( a.AreaName, 'NO-AREA' ) [AREA NAME],pm.Remarks [REMARKS], UPPER(pm.Invoice_Type) [INVOICE TYPE], UPPER ( pm.Invoice_In ) [PAYMENT MODE]
				FROM AMS.PB_Master pm
					LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID = gl.GLID
					LEFT OUTER JOIN AMS.Department d ON pm.Cls1 = d.DId
					LEFT OUTER JOIN AMS.JuniorAgent ja ON gl.AgentId = ja.AgentId
					LEFT OUTER JOIN AMS.JuniorAgent ja1 ON pm.Agent_Id = ja1.AgentId
					LEFT OUTER JOIN AMS.Area a ON gl.AreaId = a.AreaId
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN ({ObjGlobal.SysBranchId}) AND pm.fiscalYearId IN ({ObjGlobal.SysFiscalYearId}) AND ( pm.CUnit_Id ='{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL) AND pm.R_Invoice = 0
			ORDER BY pm.Invoice_Date,CAST(AMS.GetNumericValue(pm.PB_Invoice) AS NUMERIC);

			SELECT pt1.PB_VNo [VOUCHER NO], st.ST_Sign [SIGN],pt1.pT_Id TermId,UPPER(st.ST_Name) [TERM DESC], Rate RATE, SUM(pt1.Amount) AMOUNT
			FROM AMS.PB_Term pt1
			        LEFT OUTER JOIN AMS.PB_Master pm ON pt1.PB_VNo=pm.PB_Invoice
			        LEFT OUTER JOIN AMS.ST_Term st ON pt1.PT_Id=st.ST_ID
			WHERE pt1.Term_Type <> 'B' and pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}' AND pm.Invoice_Type <> 'Cancel' AND pm.CBranch_Id IN({ObjGlobal.SysBranchId}) AND pm.fiscalYearId IN({ObjGlobal.SysFiscalYearId}) AND(pm.CUnit_Id = '{ObjGlobal.SysCompanyUnitId}' OR CUnit_Id IS NULL) AND pm.R_Invoice = 0
			GROUP BY pt1.PB_VNo, pt1.pT_Id, st.ST_Name, Rate, st.ST_Sign
			ORDER BY pt1.PB_VNo; ";
        var dsSales = SqlExtensions.ExecuteDataSet(cmdString);
        if (dsSales.Tables.Count > 0)
        {
            var column = 6;
            dtMaster = dsSales.Tables[0];
            if (dsSales.Tables.Count > 1) dtTerm = dsSales.Tables[1];
            if (dtTerm.RowsCount() > 0)
            {
                var term = GetPurchaseSalesTermName(true);
                if (term is { Rows: { Count: > 0 } })
                    foreach (DataRow row in term.Rows)
                    {
                        if (ObjGlobal.PurchaseVatTermId.Equals(row["TermId"].GetInt()))
                        {
                            dtMaster.AddColumn("TAXABLE AMOUNT", typeof(decimal)).SetOrdinal(column);
                            column++;
                        }

                        dtMaster.AddColumn(row["TermDesc"].ToString(), typeof(decimal)).SetOrdinal(column);
                        column++;
                    }

                foreach (DataRow drMaster in dtMaster.Rows)
                {
                    var voucherNo = drMaster["VOUCHER NO"].GetString();
                    var exists = dtTerm.AsEnumerable().Any(c => c.Field<string>("VOUCHER NO").Equals(voucherNo));
                    if (!exists) continue;
                    var filterTerm = dtTerm?.Select($"[VOUCHER NO] ='{drMaster["VOUCHER NO"]}' ").CopyToDataTable();
                    foreach (DataRow drTerm in filterTerm.Rows)
                    {
                        if (ObjGlobal.PurchaseVatTermId.Equals(drTerm["TermId"].GetInt()))
                        {
                            var taxable = drTerm["Amount"].GetDecimal() / 0.13.GetDecimal();
                            dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)]
                                .SetField("TAXABLE AMOUNT", taxable.GetDecimalString());
                        }

                        dtMaster.Rows[dtMaster.Rows.IndexOf(drMaster)].SetField(drTerm["TERM DESC"].ToString(),
                            drTerm["Amount"].GetDecimalString());
                    }
                }
            }
        }

        return dtMaster;
    }

    public DataTable GetPurchaseInvoiceRegisterDetailsReports()
    {
        throw new NotImplementedException();
    }

    public DataSet GetPurchaseInvoiceMasterDetailsReports()
    {
        throw new NotImplementedException();
    }

    public DataTable GetPurchaseInvoiceVatRegisterReports()
    {
        var cmdString = $@"
			SELECT CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], pm.Invoice_Miti [VOUCHER MITI], CASE WHEN pm.PB_Vno IS NOT NULL AND pm.PB_Vno<>'' THEN (pm.PB_Invoice+' ('+pm.PB_Vno+')')ELSE pm.PB_Invoice END [VOUCHER NO], dm.AD_Months [AD MONTHS], dm.BS_Months [BS MONTHS], gl.GLName [PARTY LEDGER], gl.PanNo [PAN NO], CONVERT(DECIMAL(18, 2), pm.LN_Amount) AS [TOTAL AMOUNT], CASE WHEN pt.VatAmount>0 THEN CONVERT(DECIMAL(18, 2), (pm.LN_Amount-ISNULL(pt.VatAmount, 0)-(ISNULL(pt.VatAmount, 0)/ 0.13)))ELSE CONVERT(DECIMAL(18, 2), pm.LN_Amount)END AS [TAX EXAMPTED], CASE WHEN pm.Invoice_Type='IMPORT' THEN (ISNULL(pt.VatAmount, 0)/ 0.13)ELSE 0 END AS [IMPORT TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='IMPORT' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [IMPORT VAT AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [VAT AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [CAPITAL TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [CAPITAL VAT AMOUNT]
			FROM AMS.PB_Master pm
			     LEFT OUTER JOIN(SELECT pt.PB_Vno, SUM(CASE WHEN pt1.PT_Sign='+' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN pt.Amount WHEN pt1.PT_Sign='-' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN -pt.Amount ELSE 0 END) AS TermAmount, SUM(CASE WHEN pt1.PT_Id={ObjGlobal.PurchaseVatTermId} THEN pt.Amount ELSE 0 END) AS VatAmount
			                     FROM AMS.PB_Term pt
			                          LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
			                          LEFT OUTER JOIN AMS.PB_Master pm ON pt.PB_Vno=pm.PB_Invoice
			                     WHERE pt.Term_Type IN ('B', 'P')AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			                     GROUP BY pt.PB_Vno) AS pt ON pm.PB_Invoice=pt.PB_Vno
			     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
			     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			ORDER BY pm.Invoice_Date, AMS.GetNumericValue(pm.PB_Invoice);";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPurchaseInvoiceVatRegisterIncludeReturnReports()
    {
        var cmdString = $@"
			SELECT * FROM
			(
				SELECT 'PURCHASE' [VAT TYPE],CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], pm.Invoice_Miti [VOUCHER MITI], CASE WHEN pm.PB_Vno IS NOT NULL AND pm.PB_Vno<>'' THEN (pm.PB_Invoice+' ('+pm.PB_Vno+')')ELSE pm.PB_Invoice END [VOUCHER NO], dm.AD_Months [AD MONTHS], dm.BS_Months [BS MONTHS], gl.GLName [PARTY LEDGER], gl.PanNo [PAN NO], CONVERT(DECIMAL(18, 2), pm.LN_Amount) AS [TOTAL AMOUNT], CASE WHEN pt.VatAmount>0 THEN CONVERT(DECIMAL(18, 2), (pm.LN_Amount-ISNULL(pt.VatAmount, 0)-(ISNULL(pt.VatAmount, 0)/ 0.13)))ELSE CONVERT(DECIMAL(18, 2), pm.LN_Amount)END AS [TAX EXAMPTED], CASE WHEN pm.Invoice_Type='IMPORT' THEN (ISNULL(pt.VatAmount, 0)/ 0.13)ELSE 0 END AS [IMPORT TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='IMPORT' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [IMPORT VAT AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [VAT AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [CAPITAL TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [CAPITAL VAT AMOUNT]
				FROM AMS.PB_Master pm
				     LEFT OUTER JOIN(SELECT pt.PB_Vno, SUM(CASE WHEN pt1.PT_Sign='+' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN pt.Amount WHEN pt1.PT_Sign='-' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN -pt.Amount ELSE 0 END) AS TermAmount, SUM(CASE WHEN pt1.PT_Id={ObjGlobal.PurchaseVatTermId} THEN pt.Amount ELSE 0 END) AS VatAmount
				                     FROM AMS.PB_Term pt
				                          LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
				                          LEFT OUTER JOIN AMS.PB_Master pm ON pt.PB_Vno=pm.PB_Invoice
				                     WHERE pt.Term_Type IN ('B', 'P')AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
				                     GROUP BY pt.PB_Vno) AS pt ON pm.PB_Invoice=pt.PB_Vno
				     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
				     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
				UNION ALL
				SELECT 'PURCHASE RETURN' [VAT TYPE], CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], pm.Invoice_Miti [VOUCHER MITI], CASE WHEN pm.PB_Invoice IS NOT NULL AND pm.PB_Invoice<>'' THEN (pm.PR_Invoice+' ('+pm.PB_Invoice+')')ELSE pm.PR_Invoice END [VOUCHER NO], dm.AD_Months [AD MONTHS], dm.BS_Months [BS MONTHS], gl.GLName [PARTY LEDGER], gl.PanNo [PAN NO], CONVERT(DECIMAL(18, 2), pm.LN_Amount) AS [TOTAL AMOUNT], CASE WHEN pt.VatAmount>0 THEN CONVERT(DECIMAL(18, 2), (pm.LN_Amount-ISNULL(pt.VatAmount, 0)-(ISNULL(pt.VatAmount, 0)/ 0.13)))ELSE CONVERT(DECIMAL(18, 2), pm.LN_Amount)END AS [TAX EXAMPTED], CASE WHEN pm.Invoice_Type='IMPORT' THEN (ISNULL(pt.VatAmount, 0)/ 0.13)ELSE 0 END AS [IMPORT TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='IMPORT' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [IMPORT VAT AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [VAT AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [CAPITAL TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [CAPITAL VAT AMOUNT]
				FROM AMS.PR_Master pm
				     LEFT OUTER JOIN(SELECT pt.PR_Vno, SUM(CASE WHEN pt1.PT_Sign='+' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN pt.Amount WHEN pt1.PT_Sign='-' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN -pt.Amount ELSE 0 END) AS TermAmount, SUM(CASE WHEN pt1.PT_Id={ObjGlobal.PurchaseVatTermId} THEN pt.Amount ELSE 0 END) AS VatAmount
				                     FROM AMS.PR_Term pt
				                          LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
				                          LEFT OUTER JOIN AMS.PR_Master pm ON pt.PR_Vno=pm.PR_Invoice
				                     WHERE pt.Term_Type IN ('B', 'P')AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
				                     GROUP BY pt.PR_Vno) AS pt ON pm.PR_Invoice=pt.PR_Vno
				     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
				     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
				WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			) Vat
			ORDER BY Vat.[VAT TYPE],Vat.[VOUCHER DATE]";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetPurchaseReturnVatRegisterReports()
    {
        var cmdString = $@"
			SELECT CONVERT(VARCHAR, pm.Invoice_Date, 102) [VOUCHER DATE], pm.Invoice_Miti [VOUCHER MITI], CASE WHEN pm.PB_Invoice IS NOT NULL AND pm.PB_Invoice<>'' THEN (pm.PR_Invoice+' ('+pm.PB_Invoice+')')ELSE pm.PR_Invoice END [VOUCHER NO], dm.AD_Months [AD MONTHS], dm.BS_Months [BS MONTHS], gl.GLName [PARTY LEDGER], gl.PanNo [PAN NO], CONVERT(DECIMAL(18, 2), pm.LN_Amount) AS [TOTAL AMOUNT], CASE WHEN pt.VatAmount>0 THEN CONVERT(DECIMAL(18, 2), (pm.LN_Amount-ISNULL(pt.VatAmount, 0)-(ISNULL(pt.VatAmount, 0)/ 0.13)))ELSE CONVERT(DECIMAL(18, 2), pm.LN_Amount)END AS [TAX EXAMPTED], CASE WHEN pm.Invoice_Type='IMPORT' THEN (ISNULL(pt.VatAmount, 0)/ 0.13)ELSE 0 END AS [IMPORT TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='IMPORT' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [IMPORT VAT AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type NOT IN ('IMPORT', 'ASSETS') THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [VAT AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), (ISNULL(pt.VatAmount, 0)/ 0.13))ELSE 0 END AS [CAPITAL TAXABLE AMOUNT], CASE WHEN pm.Invoice_Type='ASSETS' THEN CONVERT(DECIMAL(18, 2), ISNULL(pt.VatAmount, 0))ELSE 0 END AS [CAPITAL VAT AMOUNT]
			FROM AMS.PR_Master pm
			     LEFT OUTER JOIN(SELECT pt.PR_Vno, SUM(CASE WHEN pt1.PT_Sign='+' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN pt.Amount WHEN pt1.PT_Sign='-' AND pt.PT_Id<>{ObjGlobal.PurchaseVatTermId} THEN -pt.Amount ELSE 0 END) AS TermAmount, SUM(CASE WHEN pt1.PT_Id={ObjGlobal.PurchaseVatTermId} THEN pt.Amount ELSE 0 END) AS VatAmount
			                     FROM AMS.PR_Term pt
			                          LEFT OUTER JOIN AMS.PT_Term pt1 ON pt.PT_Id=pt1.PT_Id
			                          LEFT OUTER JOIN AMS.PR_Master pm ON pt.PR_Vno=pm.PR_Invoice
			                     WHERE pt.Term_Type IN ('B', 'P')AND pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			                     GROUP BY pt.PR_Vno) AS pt ON pm.PR_Invoice=pt.PR_Vno
			     LEFT OUTER JOIN AMS.GeneralLedger gl ON pm.Vendor_ID=gl.GLID
			     LEFT OUTER JOIN AMS.DateMiti dm ON dm.AD_Date=pm.Invoice_Date
			WHERE pm.Invoice_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			ORDER BY pm.Invoice_Date, AMS.GetNumericValue(pm.PR_Invoice);";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    // FINANCE TRANSACTION
    public DataTable GetPostDatedCheque(string status = "Due")
    {
        var cmdString = @"
			SELECT VoucherNo [VOUCHER NO], VoucherDate [VOUCHER DATE], VoucherMiti [VOUCHER MITI], VoucherType [TYPE], bl.GLName [BANK LEDGER], pdc.Status [CHEQUE STATUS], pdc.BankName [CLIENT BANK], pdc.BranchName [BRANCH], pdc.ChequeNo [CHEQUE NO], pdc.ChqDate [CHEQUE DATE], pdc.ChqMiti [CHEQUE MITI], pdc.DrawOn [ISSUE NAME], CAST(pdc.Amount AS DECIMAL(18, 2)) [CHEQUE AMOUNT], gl.GLName [GENERAL LEDGER], pdc.Remarks [REMARKS]
			FROM AMS.PostDateCheque pdc
			     INNER JOIN AMS.GeneralLedger bl ON bl.GLID=pdc.BankLedgerId
			     INNER JOIN AMS.GeneralLedger gl ON gl.GLID=pdc.LedgerId
			WHERE(pdc.IsReverse=0 OR pdc.IsReverse IS NULL) ";
        cmdString += status.GetUpper().Equals("ALL") ? "" : $" AND pdc.Status='{status}'";
        cmdString += status.GetUpper().Equals("ALL")
            ? $" AND pdc.ChqDate BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND'{GetReports.ToAdDate.GetSystemDate()}';"
            : $" AND pdc.ChqDate <= '{GetReports.ToAdDate.GetSystemDate()}';";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetCashBankVoucherDetails()
    {
        var cmdString = $@"
			SELECT cb.Voucher_No [VOUCHER NO], pl.GLName [MASTER LEDGER], gl.GLName [GENERAL LEDGER], C.CName [CURRENCY], CAST(C.CRate AS DECIMAL(18, 2)) [EXCHANGE RATE], CAST(cb.LocalCredit AS DECIMAL(18, 2)) [RECEIPT], CAST(cb.LocalDebit AS DECIMAL(18, 2)) [PAYMENT],cb.Narration [NARRATION]
			FROM AMS.CB_Details cb
			     INNER JOIN AMS.CB_Master cm ON cm.Voucher_No=cb.Voucher_No
			     INNER JOIN AMS.GeneralLedger gl ON gl.GLID=cb.Ledger_Id
			     INNER JOIN AMS.GeneralLedger pl ON pl.GLID=cb.CBLedgerId
			     LEFT OUTER JOIN AMS.Currency C ON C.CId=ISNULL(cb.CurrencyId, cm.Currency_Id)
				WHERE (cm.IsReverse = 0 or cm.IsReverse IS NULL) AND cm.Voucher_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}'
			ORDER BY AMS.GetNumericValue(cb.Voucher_No) ASC; ";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    private DataTable GetPurchaseSalesTermName(bool isPurchase = false)
    {
        var cmdString = isPurchase
            ? " SELECT pt.PT_Id TermId, pt.PT_Name TermDesc FROM AMS.PT_Term pt WHERE pt.PT_Type <> 'A'  ORDER BY Order_No;"
            : " SELECT st.ST_Id TermId, st.ST_Name TermDesc,st.ST_Sign TermSign FROM AMS.ST_Term  st WHERE st.ST_Type <> 'A' ORDER BY st.Order_No; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetVendorCurrencyLedgerReport()
    {
        var report = new DataTable();
        var cmd = $@"
            SELECT ad.Voucher_Date SDate,gl.GLName SLedger,c.CCode SCurrency,ad.Debit_Amt SAmount,ad.Currency_Rate ExchangeRate,  ad.LocalDebit_Amt Settlement,ad.Narration
            FROM AMS.AccountDetails                ad
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
	             LEFT OUTER JOIN AMS.Currency c ON c.CId = ad.Currency_ID
            WHERE ad.LocalDebit_Amt>0 AND Voucher_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}';

            SELECT ad.Voucher_Date RDate, gl.GLName, ad.LocalCredit_Amt,ad.Narration
            FROM AMS.AccountDetails                ad
                 LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=ad.Ledger_ID
	             LEFT OUTER JOIN AMS.Currency c ON c.CId = ad.Currency_ID
            WHERE  ad.LocalCredit_Amt>0 AND Voucher_Date BETWEEN '{GetReports.FromAdDate.GetSystemDate()}' AND '{GetReports.ToAdDate.GetSystemDate()}';  ";
        var dsReport = SqlExtensions.ExecuteDataSet(cmd);
        return report;
    }
}