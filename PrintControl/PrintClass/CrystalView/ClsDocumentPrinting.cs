using System;
using System.Data;
using MrDAL.Global.Common;
using MrDAL.Utility.Server;

namespace PrintControl.PrintClass.CrystalView;

public class ClsDocumentPrinting
{
    #region --------------- PRINTING CLASS ---------------

    public DataSet GetVoucherDetailsForPrinting(string fromVoucherNo, string toVoucherNo, string module, bool isDate = false)
    {
        var cmdString = " SELECT * FROM VM.VIEW_COMPANY_PROFILES ";

        cmdString += module switch
        {
            "JV" => $@"
                            SELECT * from VM.VIEW_JV_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * from VM.VIEW_JV_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' order by VOUCHER_DATE, VOUCHER_NO;",
            "CB" => $@"
                            SELECT * FROM VM.VIEW_CB_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_CB_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;",
            "NM" => $@"
                            SELECT * FROM VM.VIEW_NOTES_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_NOTES_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO",
            "PB" => $@"
                            SELECT * FROM VM.VIEW_PB_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}'  ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PB_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_PB_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PB_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PB_OTHER_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO,VOUCHER_PURCHASE_NO;",
            "PC" => $@"
                            SELECT * FROM VM.VIEW_PC_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PC_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_PC_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PC_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;",
            "PO" => $@"
                            SELECT * FROM VM.VIEW_PO_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PO_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_PO_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO ;
                            SELECT * FROM VM.VIEW_PO_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO ",
            "PR" => $@"
                            SELECT * FROM VM.VIEW_PR_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PR_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_PR_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PR_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;",
            "PIN" => $@"
                            SELECT * FROM VM.VIEW_PIN_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_PIN_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;",
            "SB" or "ATI" or "POS" or "RESTRO" => $@"
                            SELECT * FROM VM.VIEW_SB_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SB_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_SB_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SB_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SB_EXCHANGE_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_ID,PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_SB_MASTER_OTHER_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VECHILE_NO; ",
            "SC" => $@"
                            SELECT * FROM VM.VIEW_SC_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SC_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_SC_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SC_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SC_MASTER_OTHER_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}'  ORDER BY VECHILE_NO; ",
            "SO" => $@"
                            SELECT * FROM VM.VIEW_SO_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SO_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_SO_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SO_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SO_MASTER_OTHER_DETAILS  WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}'  ORDER BY VECHILE_NO; ",
            "SQ" => $@"
                            SELECT * FROM VM.VIEW_SQ_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SQ_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_SQ_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SQ_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO",
            "SR" => $@"
                            SELECT * FROM VM.VIEW_SR_MASTER WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_DATE, VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SR_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO, VOUCHER_SNO, PRODUCT_DESC;
                            SELECT * FROM VM.VIEW_SR_TERM_BILL WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SR_TERM_PRODUCT WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VOUCHER_NO;
                            SELECT * FROM VM.VIEW_SR_MASTER_OTHER_DETAILS WHERE VOUCHER_NO >= '{fromVoucherNo}' AND VOUCHER_NO <= '{toVoucherNo}' ORDER BY VECHILE_NO;",
            _ => string.Empty
        };

        var dataSet = SqlExtensions.ExecuteDataSet(cmdString);
        dataSet.Tables[0].TableName = "VIEW_COMPANY_PROFILES";
        dataSet.Tables[1].TableName = module switch
        {
            "JV" => "VIEW_JV_MASTER",
            "CB" => "VIEW_CB_MASTER",
            "NM" => "VIEW_NOTES_MASTER",
            "PB" => "VIEW_PB_MASTER",
            "PC" => "VIEW_PC_MASTER",
            "PO" => "VIEW_PO_MASTER",
            "PR" => "VIEW_PR_MASTER",
            "PIN" => "VIEW_PIN_MASTER",
            "SB" or "ATI" or "POS" or "RESTRO" => "VIEW_SB_MASTER",
            "SC" => "VIEW_SC_MASTER",
            "SO" => "VIEW_SO_MASTER",
            "SQ" => "VIEW_SQ_MASTER",
            "SR" => "VIEW_SR_MASTER",
            _ => dataSet.Tables[1].TableName
        };
        if (dataSet.Tables.Count <= 2) return dataSet;

        dataSet.Tables[2].TableName = module switch
        {
            "JV" => "VIEW_JV_DETAILS",
            "CB" => "VIEW_CB_DETAILS",
            "NM" => "VIEW_NOTES_DETAILS",
            "PB" => "VIEW_PB_DETAILS",
            "PC" => "VIEW_PC_DETAILS",
            "PO" => "VIEW_PO_DETAILS",
            "PR" => "VIEW_PR_DETAILS",
            "PIN" => "VIEW_PIN_DETAILS",
            "SB" or "ATI" or "POS" or "RESTRO" => "VIEW_SB_DETAILS",
            "SC" => "VIEW_SC_DETAILS",
            "SO" => "VIEW_SO_DETAILS",
            "SQ" => "VIEW_SQ_DETAILS",
            "SR" => "VIEW_SR_DETAILS",
            _ => dataSet.Tables[2].TableName
        };
        if (dataSet.Tables.Count <= 3) return dataSet;

        dataSet.Tables[3].TableName = module switch
        {
            "PB" => "VIEW_PB_TERM_BILL",
            "PC" => "VIEW_PC_TERM_BILL",
            "PO" => "VIEW_PO_TERM_BILL",
            "PR" => "VIEW_PR_TERM_BILL",
            "SB" or "ATI" or "POS" or "RESTRO" => "VIEW_SB_TERM_BILL",
            "SC" => "VIEW_SC_TERM_BILL",
            "SO" => "VIEW_SO_TERM_BILL",
            "SQ" => "VIEW_SQ_TERM_BILL",
            "SR" => "VIEW_SR_TERM_BILL",
            _ => dataSet.Tables[3].TableName
        };
        if (dataSet.Tables.Count <= 4) return dataSet;

        dataSet.Tables[4].TableName = module switch
        {
            "PB" => "VIEW_PB_TERM_PRODUCT",
            "PC" => "VIEW_PC_TERM_PRODUCT",
            "PO" => "VIEW_PO_TERM_PRODUCT",
            "PR" => "VIEW_PR_TERM_PRODUCT",
            "SB" or "ATI" or "POS" or "RESTRO" => "VIEW_SB_TERM_PRODUCT",
            "SC" => "VIEW_SC_TERM_PRODUCT",
            "SO" => "VIEW_SO_TERM_PRODUCT",
            "SQ" => "VIEW_SQ_TERM_PRODUCT",
            "SR" => "VIEW_SR_TERM_PRODUCT",
            _ => dataSet.Tables[4].TableName
        };
        if (dataSet.Tables.Count <= 5) return dataSet;

        dataSet.Tables[5].TableName = module switch
        {
            "PB" => "VIEW_PB_OTHER_MASTER",
            "SB" or "ATI" or "POS" or "RESTRO" => "VIEW_SC_MASTER_OTHER_DETAILS",
            "SC" => "VIEW_SC_MASTER_OTHER_DETAILS",
            "SO" => "VIEW_SO_MASTER_OTHER_DETAILS",
            "SR" => "VIEW_SR_MASTER_OTHER_DETAILS",
            _ => dataSet.Tables[5].TableName
        };
        if (dataSet.Tables.Count <= 6) return dataSet;

        dataSet.Tables[6].TableName = module switch
        {
            "SB" or "ATI" or "POS" or "RESTRO" => "VIEW_SB_EXCHANGE_DETAILS",
            _ => dataSet.Tables[6].TableName
        };
        return dataSet;
    }
    public DataTable GetPrintVoucherName(string module)
    {
        var result = new DataTable();
        try
        {
            var cmdString = $@"
                SELECT Paper_Name AS Design,Paths AS DDP_Id,NoOfPrint FROM
                AMS.DocumentDesignPrint AS DDP
                     LEFT OUTER JOIN MASTER.AMS.PrintDocument_Designer AS PDD On DDP.Module=PDD.Station AND DDP.Paper_Name=PDD.DesignerPaper_Name
                WHERE  (Branch_Id={ObjGlobal.SysBranchId} or Branch_Id is Null) ";
            cmdString += module.Equals("SB") ? " AND Module in ('SB','ATI')" : $" AND Module ='{module}' ";
            result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }
        catch (Exception e)
        {
            var cmdString = $@"
                SELECT dp.Paper_Name AS Design, pd.Paths COLLATE SQL_Latin1_General_CP1_CI_AS AS DDP_Id, dp.NoOfPrint
                FROM AMS.DocumentDesignPrint AS dp
                     LEFT OUTER JOIN master.AMS.PrintDocument_Designer AS pd ON dp.Module=pd.Station COLLATE SQL_Latin1_General_CP1_CI_AS AND dp.Paper_Name=pd.DesignerPaper_Name COLLATE SQL_Latin1_General_CP1_CI_AS
                WHERE  (Branch_Id={ObjGlobal.SysBranchId} or Branch_Id is Null) ";
            cmdString += module.Equals("SB") ? " AND Module in ('SB','ATI')" : $" AND Module ='{module}' ";
            result = SqlExtensions.ExecuteDataSet(cmdString).Tables[0];
        }

        return result;
    }

    #endregion --------------- PRINTING CLASS ---------------

    #region --------------- OBJECT ---------------

    public string FrmName { get; set; }
    public string Module { get; set; }
    public string SelectQuery { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string FromDocNo { get; set; }
    public string ToDocNo { get; set; }
    public string InvoiceType { get; set; }
    public string PrinterName { get; set; }
    public string DocDesignName { get; set; }
    public int NoOfCopy { get; set; }
    public string DesignLocation { get; set; }
    private string _query = string.Empty;
    public string BillNo = null, BillCategory = null, BillStatus = null;

    #endregion --------------- OBJECT ---------------
}