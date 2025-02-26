using Microsoft.Office.Interop.Excel;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Models.Common;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Application = Microsoft.Office.Interop.Excel.Application;
using DataTable = System.Data.DataTable;
using TextBox = System.Windows.Forms.TextBox;

namespace MrDAL.Core.Extensions;

public static class DataTableExt
{
    // LIST OF DATA TABLE
    public static List<T> ToList<T>(this DataTable table) where T : class, new()
    {
        try
        {
            var list = new List<T>();

            foreach (var row in table.AsEnumerable())
            {
                var obj = new T();

                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        var propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo?.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType),
                            null);
                    }
                    catch
                    {
                        // ignored
                    }
                }

                list.Add(obj);
            }

            return list;
        }
        catch
        {
            return null;
        }
    }
    public static List<DataColumn> AddColumns(this DataTable table, IList<ValueModel<string, Type>> columnParams)
    {
        var columns = columnParams.Select(x => new DataColumn(x.Item1, x.Item2)).ToList();
        table.Columns.AddRange(columns.ToArray());
        return columns;
    }
    public static List<DataColumn> AddStringColumns(this DataTable table, string[] columnNames)
    {
        var columns = columnNames.Select(x => new DataColumn(x, typeof(string))).ToList();
        table.Columns.AddRange(columns.ToArray());
        return columns;
    }



    // DATA COLUMN FUNCTION
    public static DataColumn AddColumn(this DataTable table, string name, Type columnType)
    {
        var column = new DataColumn(name, columnType);
        table.Columns.Add(column);
        return column;
    }
    public static DataColumn AddStringColumn(this DataTable table, string name)
    {
        var column = new DataColumn(name, typeof(string));
        table.Columns.Add(column);
        return column;
    }
   
    



    //RETURN VALUE IN DATA TABLE AND DATA SET
    public static DataSet GetQueryDataSet(this string query)
    {
        try
        {
            var table = SqlExtensions.ExecuteDataSet(query);
            return table;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return new DataSet();
        }
    }



    //RETURN VALUE IN DATA TABLE AND DATA SET
    public static DataTable GetQueryDataTable(this string query)
    {
        try
        {
            var table = SqlExtensions.ExecuteDataSet(query).Tables[0];
            return table;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return new DataTable();
        }
    }
    public static DataTable CheckValueExits(this string value, string actionTag, string module, string filterColumn, long selectedId)
    {
        try
        {
            var (table, column, addColumn) = StringExt.GetTableInfo(module);
            var cmdString = $"SELECT {filterColumn} FROM {table} WHERE {filterColumn} = '{value}' ";
            cmdString += !actionTag.Equals("SAVE") ? $"AND {column} <> '{selectedId}'" : " ";
            var dtRecords = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
            return dtRecords;
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return new DataTable();
        }
    }
    public static DataTable CheckValueExits(this TextBox textBox, string actionTag, string module, string filterColumn, long selectedId)
    {
        try
        {
            return CheckValueExits(textBox.Text, actionTag, module, filterColumn, selectedId);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return new DataTable();
        }
    }
    public static DataTable CheckValueExits(this TextBox textBox, string actionTag, string module, string filterColumn, int selectedId)
    {
        try
        {
            return CheckValueExits(textBox.Text, actionTag, module, filterColumn, selectedId);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return new DataTable();
        }
    }
    public static DataTable GetSoftwareModule(this DataTable table)
    {
        table.Columns.Add("MODULE", typeof(string));
        table.Columns.Add("DESCRIPTION", typeof(string));

        table.Rows.Add("FREE", "FREE SOFTWARE LICENSING");
        table.Rows.Add("AIMS", "ACCOUNT & INVENTORY MANAGEMENT SYSTEM");
        table.Rows.Add("POS", "POINT OF SALES");
        table.Rows.Add("RESTRO", "HOSPITALITY MANAGEMENT SYSTEM");
        table.Rows.Add("HOSPITAL", "HOSPITAL MANAGEMENT SYSTEM");
        table.Rows.Add("HOTEL", "HOTEL & HOSPITALITY MANAGEMENT SYSTEM");
        table.Rows.Add("TRAVEL", "TRAVEL & TOUR MANAGEMENT SYSTEM");
        table.Rows.Add("VEHICLE", "VEHICLE MANAGEMENT SYSTEM");
        table.Rows.Add("PHARMA", "PHARMACY MANAGEMENT SYSTEM");
        table.Rows.Add("CINEMA", "BOX OFFICE MANAGEMENT SYSTEM");
        table.Rows.Add("SCHOOL-TIME", "SCHOOL MANAGEMENT SYSTEM");
        table.Rows.Add("ASSETS", "ASSETS MANAGEMENT SYSTEM");
        table.Rows.Add("HR-MGMT", "HUMAN RESOURCES PLANNING");
        table.Rows.Add("PAYROLL", "STAFF MANAGEMENT SYSTEM");
        table.Rows.Add("ERP", "ENTERPRISE RESOURCE PLANNING");

        return table;
    }
    public static DataTable MasterImportModule(this DataTable table)
    {
        table.Columns.Add("DESCRIPTION", typeof(string));
        table.Columns.Add("MODULE", typeof(string));

        table.Rows.Add("BRANCH", "BRANCH");
        table.Rows.Add("COMPANY UNIT", "COMPANY UNIT");
        table.Rows.Add("CURRENCY", "CURRENCY");

        table.Rows.Add("SENIOR AGENT", "SENIOR AGENT");
        table.Rows.Add("JUNIOR AGENT", "JUNIOR AGENT");

        table.Rows.Add("MAIN AREA", "MAIN AREA");
        table.Rows.Add("AREA", "AREA");

        table.Rows.Add("ACCOUNT GROUP", "ACCOUNT GROUP");
        table.Rows.Add("ACCOUNT SUBGROUP", "ACCOUNT SUBGROUP");

        table.Rows.Add("GENERAL LEDGER", "GENERAL LEDGER");
        table.Rows.Add("SUB LEDGER", "SUB LEDGER");

        table.Rows.Add("MEMBER TYPE", "MEMBER TYPE");
        table.Rows.Add("MEMBERSHIP SETUP", "MEMBERSHIP SETUP");

        table.Rows.Add("DEPARTMENT", "DEPARTMENT");

        if (ObjGlobal.SoftwareModule != "AIMS")
        {
            table.Rows.Add("COUNTER", "COUNTER");
        }
        if (ObjGlobal.SoftwareModule is "RESTRO")
        {
            table.Rows.Add("FLOOR", "FLOOR");
            table.Rows.Add("TABLE", "TABLE");
        }

        table.Rows.Add("GODOWN", "GODOWN");
        table.Rows.Add("COST CENTER", "COST CENTER");
        table.Rows.Add("RACK", "RACK");

        table.Rows.Add("PRODUCT GROUP", "PRODUCT GROUP");
        table.Rows.Add("PRODUCT SUBGROUP", "PRODUCT SUBGROUP");
        table.Rows.Add("PRODUCT UNIT", "PRODUCT UNIT");
        table.Rows.Add("PRODUCT", "PRODUCT");

        if (ObjGlobal.SoftwareModule is "POS")
        {
            table.Rows.Add("BARCODE LIST", "BARCODE LIST");
        }
        table.Rows.Add("DOCUMENT NUMBERING", "DOCUMENT NUMBERING");
        table.Rows.Add("PURCHASE TERM", "PURCHASE TERM");
        table.Rows.Add("SALES TERM", "SALES TERM");

        if (ObjGlobal.SoftwareModule is "HOSPITAL")
        {
            table.Rows.Add("DOCTOR TYPE", "DOCTOR TYPE");
            table.Rows.Add("DOCTOR SETUP", "DOCTOR SETUP");
            table.Rows.Add("HOSPITAL DEPARTMENT", "HOSPITAL DEPARTMENT");
            table.Rows.Add("WARD MASTER", "WARD MASTER");
            table.Rows.Add("BED TYPE", "BED TYPE");
            table.Rows.Add("BED NUMBER", "BED NUMBER");
        }
        return table;
    }
    public static DataTable MasterResetModule(this DataTable table)
    {
        table.Columns.Add("DESCRIPTION", typeof(string));
        table.Columns.Add("MODULE", typeof(string));
        table.Rows.Add("SYSTEM SETTING");

        table.Rows.Add("BARCODE LIST");
        table.Rows.Add("PRODUCT SUBGROUP");
        table.Rows.Add("PRODUCT GROUP");
        table.Rows.Add("PRODUCT");
        table.Rows.Add("PRODUCT UNIT");

        table.Rows.Add("DOCUMENT NUMBERING");
        table.Rows.Add("PURCHASE TERM");
        table.Rows.Add("SALES TERM");


        table.Rows.Add("JUNIOR AGENT");
        table.Rows.Add("SENIOR AGENT");

        table.Rows.Add("DEPARTMENT");
        table.Rows.Add("COUNTER");
        table.Rows.Add("MEMBERSHIP SETUP");
        table.Rows.Add("MEMBER TYPE");

        table.Rows.Add("SUB LEDGER");
        table.Rows.Add("GENERAL LEDGER");

        table.Rows.Add("ACCOUNT SUBGROUP");
        table.Rows.Add("ACCOUNT GROUP");

        table.Rows.Add("TABLE");
        table.Rows.Add("FLOOR");
        table.Rows.Add("COST CENTER");
        table.Rows.Add("GODOWN");
        table.Rows.Add("RACK");

        table.Rows.Add("CURRENCY");
        table.Rows.Add("COMPANY UNIT");
        table.Rows.Add("BRANCH");
        return table;
    }
    public static DataTable DataEntryImportModule(this DataTable table)
    {
        table.Columns.Add("DESCRIPTION", typeof(string));
        table.Columns.Add("MODULE", typeof(string));

        //OPENING
        table.Rows.Add("LEDGER OPENING", "LOB");
        table.Rows.Add("PRODUCT OPENING", "POB");

        //CASH BANK VOUCHER
        table.Rows.Add("CASH BANK VOUCHER", "CB");
        table.Rows.Add("JOURNAL VOUCHER", "JV");
        table.Rows.Add("CREDIT NOTE", "CN");
        table.Rows.Add("DEBIT NOTE", "DN");
        table.Rows.Add("BANK RECONCILIATION", "BRN");
        table.Rows.Add("POST DATED CHEQUE", "PDC");

        //IPD
        table.Rows.Add("IPD BILLING", "IPDB");
        table.Rows.Add("IPD PATIENT REGISTRATION", "IPGL");
        table.Rows.Add("PATIENT REGISTRATION", "PGL");
        table.Rows.Add("HOSPITAL", "HS");
        table.Rows.Add("PATIENT DISCHARGE", "HPD");
        table.Rows.Add("EMR PATIENT REGISTRATION", "EPR");
        table.Rows.Add("PATIENT FOLLOW UP", "FPR");
        table.Rows.Add("PATIENT DRUG HISTORY", "PDH");
        table.Rows.Add("OPD BILLING", "OPDB");
        table.Rows.Add("OPD PATIENT REGISTRATION", "OPGL");
        table.Rows.Add("REFUND ENTRY", "NN");

        //PRODUCTION
        table.Rows.Add("SAMPLE COSTING", "PSC");
        table.Rows.Add("BILL OF MATERIALS", "BOM");
        table.Rows.Add("ASSEMBLY MASTER", "ASM");
        table.Rows.Add("COST CENTER EXPENSES", "PCCE");
        table.Rows.Add("PRODUCTION MEMO", "IBOM");
        table.Rows.Add("PRODUCTION ORDER", "IPO");
        table.Rows.Add("PRODUCTION PLANNING", "IPP");
        table.Rows.Add("PRODUCTION REQUISITION", "IREQ");
        table.Rows.Add("PRODUCTION MASTER MEMO", "MBOM");
        table.Rows.Add("PRODUCTION ISSUE", "RMI");
        table.Rows.Add("PRODUCTION ISSUE RETURN", "RMIR");
        table.Rows.Add("FINISHED GOOD RECEIVED", "FGR");
        table.Rows.Add("FINISHED GOOD RECEIVED RETURN", "FGRR");

        //INVENTORY
        table.Rows.Add("INVENTORY ISSUE", "MI");
        table.Rows.Add("INVENTORY ISSUE RETURN", "MIR");
        table.Rows.Add("INVENTORY RECEIVED", "MR");
        table.Rows.Add("INVENTORY RECEIVED RETURN", "MRR");
        table.Rows.Add("INVENTORY REQUISITION", "SREQ");

        //PURCHASE
        table.Rows.Add("PURCHASE QUOTATION", "PQ");
        table.Rows.Add("PURCHASE INDENT", "PIN");
        table.Rows.Add("PURCHASE ORDER", "PO");
        table.Rows.Add("PURCHASE CHALLAN", "PC");
        table.Rows.Add("PURCHASE CHALLAN RETURN", "PCR");
        table.Rows.Add("GOODS IN TRANSIT", "GIT");
        table.Rows.Add("PURCHASE QUALITY CONTROL	", "PQC");
        table.Rows.Add("PURCHASE INVOICE", "PB");
        table.Rows.Add("PURCHASE TRAVEL & TOUR", "PBT");
        table.Rows.Add("PURCHASE ADDITIONAL INVOICE", "PAB");
        table.Rows.Add("PURCHASE RETURN", "PR");
        table.Rows.Add("PURCHASE EXPIRY/BREAKAGE RETURN", "PEB");

        //INTER BRANCH
        table.Rows.Add("PURCHASE INTER BRANCH", "PIB");
        table.Rows.Add("SALES INTER BRANCH", "SIB");

        //SALES
        table.Rows.Add("SALES QUOTATION", "SQ");
        table.Rows.Add("SALES ORDER", "SO");
        table.Rows.Add("SALES ORDER CANCELLATION", "SOC");
        table.Rows.Add("SALES DISPATCH ORDER", "SDO");
        table.Rows.Add("SALES DISPATCH ORDER CANCELLATION", "SDOC");
        table.Rows.Add("SALES CHALLAN", "SC");
        table.Rows.Add("SALES INVOICE", "SB");
        table.Rows.Add("ABBREVIATED TAX INVOICE", "ATI");
        table.Rows.Add("POINT OF SALES", "POS");
        table.Rows.Add("SALES ADDITIONAL INVOICE", "SAB");
        table.Rows.Add("SALES TOURS & TRAVEL", "SBT");
        table.Rows.Add("TEMP SALES INVOICE", "TSB");
        table.Rows.Add("SALES RETURN", "SR");
        table.Rows.Add("SALES EXPIRY/BREAKAGE RETURN", "SEB");

        //STOCK
        table.Rows.Add("PHYSICAL STOCK", "PSA");
        table.Rows.Add("STOCK ADJUSTMENT", "SA");
        table.Rows.Add("TRANSFER EXPIRY/BREAKAGE", "STEB");
        table.Rows.Add("GODOWN TRANSFER", "GT");

        if (ObjGlobal.SoftwareModule.Equals("ERP"))
        {
            table.Rows.Add("ASSETS LOG", "ASL");
        }
        return table;
    }
    public static DataTable DataEntryResetModule(this DataTable table)
    {
        table.Columns.Add("DESCRIPTION", typeof(string));
        table.Columns.Add("MODULE", typeof(string));

        //OPENING
        table.Rows.Add("OPENING BALANCE", "LOB");
        table.Rows.Add("PRODUCT OPENING", "POB");
        table.Rows.Add("CHARTS OF ACCOUNT OPENING", "COA");

        //CASH BANK VOUCHER
        table.Rows.Add("CASH BANK VOUCHER", "CB");
        table.Rows.Add("JOURNAL VOUCHER", "JV");
        table.Rows.Add("CREDIT NOTE", "CN");
        table.Rows.Add("DEBIT NOTE", "DN");
        table.Rows.Add("BANK RECONCILIATION", "BRN");
        table.Rows.Add("POST DATED CHEQUE", "PDC");

        //IPD
        table.Rows.Add("IPD BILLING", "IPDB");
        table.Rows.Add("IPD PATIENT REGISTRATION", "IPGL");
        table.Rows.Add("PATIENT REGISTRATION", "PGL");
        table.Rows.Add("HOSPITAL", "HS");
        table.Rows.Add("PATIENT DISCHARGE", "HPD");
        table.Rows.Add("EMR PATIENT REGISTRATION", "EPR");
        table.Rows.Add("PATIENT FOLLOW UP", "FPR");
        table.Rows.Add("PATIENT DRUG HISTORY", "PDH");
        table.Rows.Add("OPD BILLING", "OPDB");
        table.Rows.Add("OPD PATIENT REGISTRATION", "OPGL");
        table.Rows.Add("REFUND ENTRY", "NN");

        //PRODUCTION
        table.Rows.Add("SAMPLE COSTING", "PSC");
        table.Rows.Add("BILL OF MATERIALS", "BOM");
        table.Rows.Add("ASSEMBLY MASTER", "ASM");
        table.Rows.Add("COST CENTER EXPENSES", "PCCE");
        table.Rows.Add("PRODUCTION MEMO", "IBOM");
        table.Rows.Add("PRODUCTION ORDER", "IPO");
        table.Rows.Add("PRODUCTION PLANNING", "IPP");
        table.Rows.Add("PRODUCTION REQUISITION", "IREQ");
        table.Rows.Add("PRODUCTION MASTER MEMO", "MBOM");
        table.Rows.Add("PRODUCTION ISSUE", "RMI");
        table.Rows.Add("PRODUCTION ISSUE RETURN", "RMIR");
        table.Rows.Add("FINISHED GOOD RECEIVED", "FGR");
        table.Rows.Add("FINISHED GOOD RECEIVED RETURN", "FGRR");

        //INVENTORY
        table.Rows.Add("INVENTORY ISSUE", "MI");
        table.Rows.Add("INVENTORY ISSUE RETURN", "MIR");
        table.Rows.Add("INVENTORY RECEIVED", "MR");
        table.Rows.Add("INVENTORY RECEIVED RETURN", "MRR");
        table.Rows.Add("INVENTORY REQUISITION", "SREQ");

        //PURCHASE
        table.Rows.Add("PURCHASE EXPIRY/BREAKAGE RETURN", "PEB");
        table.Rows.Add("PURCHASE RETURN", "PR");
        table.Rows.Add("PURCHASE ADDITIONAL INVOICE", "PAB");
        table.Rows.Add("PURCHASE TRAVEL & TOUR", "PBT");
        table.Rows.Add("PURCHASE QUALITY CONTROL	", "PQC");
        table.Rows.Add("GOODS IN TRANSIT", "GIT");
        table.Rows.Add("PURCHASE CHALLAN RETURN", "PCR");
        table.Rows.Add("PURCHASE CHALLAN", "PC");
        table.Rows.Add("PURCHASE ORDER", "PO");
        table.Rows.Add("PURCHASE INDENT", "PIN");
        table.Rows.Add("PURCHASE QUOTATION", "PQ");

        //INTER BRANCH
        table.Rows.Add("PURCHASE INTER BRANCH", "PIB");
        table.Rows.Add("SALES INTER BRANCH", "SIB");

        //SALES
        table.Rows.Add("SALES EXPIRY/BREAKAGE RETURN", "SEB");
        table.Rows.Add("SALES RETURN", "SR");
        table.Rows.Add("TEMP SALES INVOICE", "TSB");
        table.Rows.Add("SALES TOURS & TRAVEL", "SBT");
        table.Rows.Add("SALES ADDITIONAL INVOICE", "SAB");
        table.Rows.Add("POINT OF SALES", "POS");
        table.Rows.Add("ABBREVIATED TAX INVOICE", "ATI");
        table.Rows.Add("SALES INVOICE", "SB");
        table.Rows.Add("SALES CHALLAN", "SC");
        table.Rows.Add("SALES DISPATCH ORDER CANCELLATION", "SDOC");
        table.Rows.Add("SALES DISPATCH ORDER", "SDO");
        table.Rows.Add("SALES ORDER CANCELLATION", "SOC");
        table.Rows.Add("SALES ORDER", "SO");
        table.Rows.Add("SALES QUOTATION", "SQ");

        //STOCK
        table.Rows.Add("PHYSICAL STOCK", "PSA");
        table.Rows.Add("STOCK ADJUSTMENT", "SA");
        table.Rows.Add("TRANSFER EXPIRY/BREAKAGE", "TEB");
        table.Rows.Add("GODOWN TRANSFER", "GT");
        if (ObjGlobal.SoftwareModule.Equals("ERP"))
        {
            table.Rows.Add("ASSETS LOG", "ASL");
        }
        return table;
    }
    public static DataTable GetLoginUserInfo(this DataTable table, bool filterUser = false)
    {
        var cmd = filterUser
            ? @$" SELECT User_Name,User_Id from MASTER.AMS.UserInfo WHERE User_Id <>'{ObjGlobal.LogInUserId}' AND User_Name NOT IN ( 'AMSADMIN','MRSOLUTION','MRDEMO');"
            : @$" SELECT User_Name, Ledger_Id FROM master.AMS.UserInfo WHERE User_Id = '{ObjGlobal.LogInUserId}'  ";
        table = SqlExtensions.ExecuteDataSet(cmd).Tables[0];
        return table;
    }
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection, string tableName = "DataTable")
    {
        var dataTable = new DataTable(tableName);
        var type = typeof(T);
        var properties = type.GetProperties();

        //Inspect the properties and create the columns in the DataTable
        foreach (var property in properties)
        {
            var columnType = property.PropertyType;
            if (columnType.IsGenericType) columnType = columnType.GetGenericArguments()[0];
            dataTable.Columns.Add(property.Name, columnType);
        }

        //Populate the data table
        foreach (var item in collection)
        {
            var dr = dataTable.NewRow();
            dr.BeginEdit();
            foreach (var property in properties)
                if (property.GetValue(item, null) != null)
                    dr[property.Name] = property.GetValue(item, null);

            dr.EndEdit();
            dataTable.Rows.Add(dr);
        }

        return dataTable;
    }
    public static DataTable ConvertColumnType(this DataTable dt)
    {
        // Get and convert the values of the old column, and insert them into the new
        foreach (DataColumn dc in dt.Columns)
            dc.ColumnName = (string)Convert.ChangeType(dc.ColumnName, TypeCode.String);
        return dt;
    }


    // EXPORT FROM DATA TABLE
    public static void ExportToExcel(this DataTable tbl, string excelFilePath = null)
    {
        try
        {
            if (tbl == null || tbl.Columns.Count == 0)
            {
                throw new Exception("ExportToExcel: Null or empty input table..!! \n");
            }

            // load excel, and create a new workbook
            var excelApp = new Application();
            excelApp.Workbooks.Add();

            // single worksheet
            _Worksheet workSheet = excelApp.ActiveSheet;

            // column headings
            for (var i = 0; i < tbl.Columns.Count; i++)
            {
                workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
            }

            // rows
            for (var i = 0; i < tbl.Rows.Count; i++)
            // to do: format datetime values before printing
            {
                for (var j = 0; j < tbl.Columns.Count; j++)
                {
                    workSheet.Cells[i + 2, j + 1] = tbl.Rows[i][j];
                }
            }

            // check file path
            if (!string.IsNullOrEmpty(excelFilePath))
            {
                try
                {
                    workSheet.SaveAs(excelFilePath);
                    excelApp.Quit();
                    MessageBox.Show(@"Excel file saved..!!");
                }
                catch (Exception ex)
                {
                    throw new Exception($"ExportToExcel: Excel file could not be saved! Check filepath.\n{ex.Message}");
                }
            }
            else
            // no file path is given
            {
                excelApp.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("ExportToExcel: \n" + ex.Message);
        }
    }
}