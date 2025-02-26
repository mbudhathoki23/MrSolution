using DatabaseModule.CloudSync.IrdServer;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Server;
using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MrDAL.Master;

public class ClsIrdApiSync
{
    public bool IsSuccess;

    //Install-Package System.Net.Http.Formatting.Extension -Version 5.2.3
    public string SyncSalesBillApiAsync(string voucherNo)
    {
        try
        {
            var cmdBuilder = new StringBuilder();
            var tblFiltered = GetSalesInvoiceForApi(voucherNo);
            var salesBilling = new IrdSyncSalesBilling
            {
                username = ObjGlobal.IrdUser,
                password = ObjGlobal.IrdUserPassword,
                seller_pan = ObjGlobal.IrdCompanyPan,
                buyer_pan = tblFiltered.Rows[0]["Customer_PAN"].ToString(),
                fiscal_year = tblFiltered.Rows[0]["Fiscal_Year"].ToString(),
                buyer_name = tblFiltered.Rows[0]["Customer_Name"].ToString(),
                invoice_number = voucherNo,
                invoice_date = $"{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(6, 4)}.{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(3, 2)}.{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(0, 2)}",
                total_sales = tblFiltered.Rows[0]["Amount"].GetDouble(),
                taxable_sales_vat = tblFiltered.Rows[0]["Taxable_Amount"].GetDouble(),
                vat = tblFiltered.Rows[0]["Tax_Amount"].GetDouble(),
                excisable_amount = 0,
                excise = 0,
                taxable_sales_hst = 0,
                hst = 0,
                amount_for_esf = 0,
                esf = 0,
                export_sales = 0,
                tax_exempted_sales = 0,
                isrealtime = true,
                datetimeClient = DateTime.Now
            };
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //add reference nuget Microsoft.AspNet.WebApi.Client
                httpClient.BaseAddress = new Uri(ObjGlobal.IrdApiAddress);
                var response = httpClient.PostAsJsonAsync("api/bill", salesBilling).Result;
                if (response.ReasonPhrase == "Not Found")
                {
                    CustomMessageBox.Warning("ERROR WHILE SYNC DATA TO IRD SERVER ... MAY BE SERVER IS DOWN..!!");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseCode = response.Content.ReadAsStringAsync();
                    var intResult = responseCode.Result.GetInt();
                    var cmdString = intResult switch
                    {
                        200 => $"{voucherNo}{intResult} >> INVOICE SYNC SUCCESSFULLY..!!",
                        100 => $"{voucherNo}{intResult} >> SERVER API CREDENTIALS INVALID..!!",
                        101 => $"{voucherNo}{intResult} >> INVOICE ALREADY EXITS IN SERVER..!!",
                        102 => $"{voucherNo}{intResult} >> INVALID INVOICE DETAILS..!!",
                        103 => $"{voucherNo}{intResult} >> INPUT VALUE OR API CREDENTIALS IS INVALID..!!",
                        104 => $"{voucherNo}{intResult} >> API CREDENTIALS VALUE OR PARAMETERS IS INVALID..!!",
                        _ => $"{voucherNo}{intResult} >> SERVER API CREDENTIALS INVALID..!!"
                    };
                    cmdBuilder.Append(cmdString);
                    cmdBuilder.AppendFormat(Environment.NewLine);
                    if (intResult.Equals(200) || intResult.Equals(101))
                    {
                        IsSuccess = UpdatePostSalesBillToApi(voucherNo, 1) != 0;

                    }

                    return cmdBuilder.ToString();
                }

                Console.Write(@"Error");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult("MrDAL.Master.ClsIrdApiSync.SyncSalesBillApiAsync");
                //ignore
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
        }

        return string.Empty;
    }

    public string SyncSalesReturnApi(string voucherNo)
    {
        try
        {
            var cmdBuilder = new StringBuilder();
            var tblFiltered = GetDataSalesReturnToApi(voucherNo);
            var salesReturn = new IrdSyncSalesReturnBill
            {
                username = ObjGlobal.IrdUser,
                password = ObjGlobal.IrdUserPassword,
                seller_pan = ObjGlobal.IrdCompanyPan,
                buyer_pan = tblFiltered.Rows[0]["Customer_PAN"].ToString(),
                fiscal_year = tblFiltered.Rows[0]["Fiscal_Year"].ToString(),
                buyer_name = tblFiltered.Rows[0]["Customer_Name"].ToString(),
                ref_invoice_number = tblFiltered.Rows[0]["RefNo"].ToString(),
                credit_note_number = voucherNo,
                credit_note_date = $"{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(6, 4)}.{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(3, 2)}.{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(0, 2)}",
                total_sales = tblFiltered.Rows[0]["Amount"].GetDouble(),
                taxable_sales_vat = tblFiltered.Rows[0]["Taxable_Amount"].GetDouble(),
                vat = tblFiltered.Rows[0]["Tax_Amount"].GetDouble(),
                excisable_amount = 0,
                excise = 0,
                taxable_sales_hst = 0,
                hst = 0,
                amount_for_esf = 0,
                esf = 0,
                export_sales = 0,
                tax_exempted_sales = 0,
                isrealtime = true,
                datetimeClient = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add reference nuget Microsoft.AspNet.WebApi.Client
            httpClient.BaseAddress = new Uri(ObjGlobal.IrdApiAddress);
            var response = httpClient.PostAsJsonAsync("api/billreturn", salesReturn).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseCode = response.Content.ReadAsStringAsync();
                var intResult = responseCode.Result.GetInt();

                var cmdString = intResult switch
                {
                    200 => $"{voucherNo}{intResult} >> RETURN INVOICE SYNC SUCCESSFULLY..!!",
                    100 => $"{voucherNo}{intResult} >> SERVER API CREDENTIALS INVALID..!!",
                    101 => $"{voucherNo}{intResult} >> RETURN INVOICE ALREADY EXITS IN SERVER..!!",
                    102 => $"{voucherNo}{intResult} >> INVALID RETURN INVOICE DETAILS..!!",
                    103 => $"{voucherNo}{intResult} >> INPUT VALUE OR API CREDENTIALS IS INVALID..!!",
                    104 => $"{voucherNo}{intResult} >> API CREDENTIALS VALUE OR PARAMETERS IS INVALID..!!",
                    _ => $"{voucherNo}{intResult} >> SERVER API CREDENTIALS INVALID..!!"
                };
                cmdBuilder.Append(cmdString);
                cmdBuilder.AppendFormat(Environment.NewLine);
                if (intResult.Equals(200) || intResult.Equals(101))
                {
                    IsSuccess = UpdatePostSalesReturnToApi(voucherNo, 1) != 0;
                }
                return cmdBuilder.ToString();
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
        }

        return string.Empty;
    }

    public string SyncSalesCancelApi(string voucherNo)
    {
        try
        {
            var cmdBuilder = new StringBuilder();
            var tblFiltered = GetSalesInvoiceForApi(voucherNo);
            var salesReturn = new IrdSyncSalesReturnBill
            {
                username = ObjGlobal.IrdUser,
                password = ObjGlobal.IrdUserPassword,
                seller_pan = ObjGlobal.IrdCompanyPan,
                buyer_pan = tblFiltered.Rows[0]["Customer_PAN"].ToString(),
                fiscal_year = tblFiltered.Rows[0]["Fiscal_Year"].ToString(),
                buyer_name = tblFiltered.Rows[0]["Customer_Name"].ToString(),
                ref_invoice_number = tblFiltered.Rows[0]["RefNo"].ToString(),
                credit_note_number = voucherNo,
                credit_note_date =
                    $"{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(6, 4)}.{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(3, 2)}.{tblFiltered.Rows[0]["Bill_Miti"].ToString().Substring(0, 2)}",
                total_sales = tblFiltered.Rows[0]["Amount"].GetDouble(),
                taxable_sales_vat = tblFiltered.Rows[0]["Taxable_Amount"].GetDouble(),
                vat = tblFiltered.Rows[0]["Tax_Amount"].GetDouble(),
                excisable_amount = 0,
                excise = 0,
                taxable_sales_hst = 0,
                hst = 0,
                amount_for_esf = 0,
                esf = 0,
                export_sales = 0,
                tax_exempted_sales = 0,
                isrealtime = true,
                datetimeClient = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //add reference nuget Microsoft.AspNet.WebApi.Client
            httpClient.BaseAddress = new Uri(ObjGlobal.IrdApiAddress);
            var response = httpClient.PostAsJsonAsync("api/billreturn", salesReturn);
            if (response.Result.IsSuccessStatusCode)
            {
            }

            var result = response.Result.Content.ReadAsStringAsync();
            var intResult = result.Result.GetInt();
            var cmdString = intResult switch
            {
                200 => $"{voucherNo}{result.Result} >> INVOICE SYNC SUCCESSFULLY..!!",
                100 => $"{voucherNo}{result.Result} >> SERVER API CREDENTIALS INVALID..!!",
                101 => $"{voucherNo}{result.Result} >> INVOICE ALREADY EXITS IN SERVER..!!",
                102 => $"{voucherNo}{result.Result} >> INVALID INVOICE DETAILS..!!",
                103 => $"{voucherNo}{result.Result} >> INPUT VALUE OR API CREDENTIALS IS INVALID..!!",
                104 => $"{voucherNo}{result.Result} >> API CREDENTIALS VALUE OR PARAMETERS IS INVALID..!!",
                _ => $"{voucherNo}{result.Result} >> SERVER API CREDENTIALS INVALID..!!"
            };
            cmdBuilder.Append(cmdString);
            cmdBuilder.AppendFormat(Environment.NewLine);
            if (intResult.Equals(200) || intResult.Equals(101))
                IsSuccess = UpdatePostSalesBillToApi(voucherNo, 1) != 0;
            return cmdBuilder.ToString();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            var msg = ex.Message;
        }

        return string.Empty;
    }

    public static int UpdatePostSalesReturnToApi(string voucherNo, int isRealtime)
    {
        CreateDatabaseTable.DisableTrigger();
        var cmdString = $@"
        UPDATE AMS.SR_Master SET IsAPIPosted=1, IsRealtime={isRealtime} WHERE SR_Invoice = '{voucherNo}'; ";
        var result = SqlExtensions.ExecuteNonQuery(cmdString);
        CreateDatabaseTable.AlterDatabaseTrigger();
        return result;
    }

    public int UpdatePostSalesBillToApi(string voucherNo, int isRealtime)
    {
        CreateDatabaseTable.DisableTrigger();
        var cmdString = new StringBuilder();
        cmdString.Append($"UPDATE AMS.SB_Master SET IsAPIPosted=1, IsRealtime={isRealtime} WHERE SB_Invoice = '{voucherNo}'");
        var iResult = SqlExtensions.ExecuteNonQuery(cmdString.ToString());
        CreateDatabaseTable.AlterDatabaseTrigger();
        return iResult;
    }

    private DataTable GetSalesInvoiceForApi(string voucherNo)
    {
        var cmdString = @$"
		SELECT RIGHT(sm.Invoice_Miti, 4)+'/'+CAST((CAST(RIGHT(RIGHT(sm.Invoice_Miti, 4), 2) AS NUMERIC)+1) AS NVARCHAR) Fiscal_Year, sm.SB_Invoice Bill_No,PB_Vno RefNo, gl.PanNo Customer_PAN, gl.GLName Customer_Name, sm.Invoice_Miti Bill_Miti, sm.LN_Amount Amount,CASE WHEN ISNULL(sbt.VatAmount,0) > 0 then sbt.VatAmount/0.13 ELSE 0 END Taxable_Amount, ISNULL(sbt.VatAmount,0) Tax_Amount
        FROM AMS.SB_Master sm
             LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_ID
	         LEFT OUTER JOIN
	         (
		        SELECT st.SB_VNo,SUM(st.Amount) VatAmount FROM AMS.SB_Term st WHERE st.ST_Id = {ObjGlobal.SalesVatTermId} AND st.Term_Type <> 'BT'
		        GROUP BY st.SB_VNo
	         ) sbt ON sbt.SB_VNo = sm.SB_Invoice
        WHERE sm.SB_Invoice='{voucherNo}';";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetDataSalesReturnToApi(string voucherNo)
    {
        var cmdString = @$"
		SELECT RIGHT(sm.Invoice_Miti, 4)+'/'+CAST((CAST(RIGHT(RIGHT(sm.Invoice_Miti, 4), 2) AS NUMERIC)+1) AS NVARCHAR) Fiscal_Year, sm.SR_Invoice Bill_No,sm.SB_Invoice RefNo, gl.PanNo Customer_PAN, gl.GLName Customer_Name, sm.Invoice_Miti Bill_Miti, sm.LN_Amount Amount,CASE WHEN ISNULL(sbt.VatAmount,0) > 0 then sbt.VatAmount/0.13 ELSE 0 END Taxable_Amount, ISNULL(sbt.VatAmount,0) Tax_Amount
        FROM AMS.SR_Master sm
             LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_ID
	         LEFT OUTER JOIN
	         (
		        SELECT st.SR_VNo,SUM(st.Amount) VatAmount FROM AMS.SR_Term st WHERE st.ST_Id = {ObjGlobal.SalesVatTermId} AND st.Term_Type <> 'BT'
		        GROUP BY st.SR_VNo
	         ) sbt ON sbt.SR_VNo = sm.SR_Invoice
        WHERE sm.SR_Invoice='{voucherNo}'; ";
        return SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
    }

    public DataTable GetOutstandingSalesInvoice()
    {
        var cmdString = $@"
        SELECT 0 [Check], SB_Invoice VoucherNo, gl.GLName Customer, gl.PanNo PanNo, CAST(sm.LN_Amount AS decimal(18,2)) NetAmount, sm.IsAPIPosted
        FROM AMS.SB_Master sm
             LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
        WHERE sm.FiscalYearId={ObjGlobal.SysFiscalYearId} AND (sm.IsAPIPosted=0 OR sm.IsRealtime IS NULL);";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetOutstandingSalesReturnInvoice()
    {
        var cmdString = $@"
        SELECT 0 [Check], SR_Invoice VoucherNo, gl.GLName Customer, gl.PanNo PanNo, CAST(sm.LN_Amount AS decimal(18,2)) NetAmount, sm.IsAPIPosted
        FROM AMS.SR_Master sm
            LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
        WHERE sm.FiscalYearId={ObjGlobal.SysFiscalYearId} AND (sm.IsAPIPosted=0 OR sm.IsRealtime IS NULL);";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetOutstandingSalesReturnCancelInvoice()
    {
        var cmdString = $@"
        SELECT 0 [Check], SR_Invoice VoucherNo, gl.GLName Customer, gl.PanNo PanNo, CAST(sm.LN_Amount AS decimal(18,2)) NetAmount, sm.IsAPIPosted
        FROM AMS.SR_Master sm
            LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
        WHERE sm.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sm.R_Invoice = 1 AND (sm.IsAPIPosted=0 OR sm.IsRealtime IS NULL);";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }

    public DataTable GetOutstandingSalesCancelInvoice()
    {
        var cmdString = $@"
        SELECT 0 [Check], SB_Invoice VoucherNo, gl.GLName Customer, gl.PanNo PanNo, CAST(sm.LN_Amount AS decimal(18,2)) NetAmount, sm.IsAPIPosted
        FROM AMS.SB_Master sm
             LEFT OUTER JOIN AMS.GeneralLedger gl ON gl.GLID=sm.Customer_Id
        WHERE sm.FiscalYearId={ObjGlobal.SysFiscalYearId} AND sm.R_Invoice = 1 AND (sm.IsAPIPosted=0 OR sm.IsRealtime IS NULL);";
        var result = SqlExtensions.ExecuteDataSet(cmdString);
        return result.Tables[0];
    }
}