using MrDAL.Utility.Server;

namespace MrDAL.Utility.dbMaster;

public class CreateViewTable
{
    public static void CreateViewOnDatabase()
    {
        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE SCHEMA TVIEW AUTHORIZATION dbo;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_INVOICE_MASTER AS SELECT '' INVOICE_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_INVOICE_DETAILS AS SELECT '' INVOICE_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_INVOICE_PRODUCT_TERM AS SELECT '' INVOICE_NO; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_INVOICE_BILL_TERM AS SELECT '' INVOICE_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_BILLOFMATERIAL_DETAILS AS SELECT '' MEME_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_BILLOFMATERIAL_MASTER AS SELECT '' MEME_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_CASH_BANK_MASTER AS SELECT '' INVOICE_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_CASH_BANK_DETAILS AS SELECT '' INVOICE_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_COSTCENTEREXPENSES_Details AS SELECT '' COSTING_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_COSTCENTEREXPENSES_MASTER AS SELECT '' COSTING_NO;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_FGR_DETAILS AS SELECT '' FGR_No ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_FGR_MASTER AS SELECT ''  FGR_No ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_GT_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_GT_MASTER AS SELECT ''  INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_INVENTORY_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_INVENTORY_MASTER AS SELECT ''  INVOICE_NO  ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_JOURNAL_VOUCHER_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_JOURNAL_VOUCHER_MASTER AS SELECT ''  INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_NOTES_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_NOTES_MASTER AS SELECT ''  INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PAB_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PAB_MASTER AS SELECT ''  INVOICE_NO  ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PB_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PB_MASTER AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PB_OTHERMASTER AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PB_TERM AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PBT_MASTER AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PBT_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_CHALLAN_DETAILS AS SELECT '' CHALLAN_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_CHALLAN_MASTER AS SELECT '' CHALLAN_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_CHALLAN_TERM AS SELECT '' CHALLAN_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PIN_DETAILS AS SELECT ''  INDENT_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PIN_MASTER AS SELECT '' INDENT_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_ORDER_MASTER AS SELECT '' ORDER_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_ORDER_DETAILS AS SELECT '' ORDER_NO  ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_ORDER_TERM AS SELECT '' ORDER_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_RETURN_DETAILS AS SELECT '' RETURN_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_RETURN_TERM AS SELECT '' RETURN_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_QUOTATION_TERM AS SELECT '' QUOTATION_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_QUOTATION_DETAILS AS SELECT '' QUOTATION_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_QUOTATION_MASTER AS SELECT '' QUOTATION_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PROVISION_CASHBANK_DETAILS AS SELECT '' PROVISION_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PROVISION_CASHBANK_MASTER AS SELECT '' PROVISION_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PRT_TERM AS SELECT '' PRT_ID ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PT_TERM AS SELECT '' PT_ID ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_QUOTATION_DETAILS AS SELECT '' QUOT_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_PURCHASE_QUOTATION_MASTER AS SELECT '' QUOT_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SAMPLECOSTING_DETAILS AS SELECT '' COSTING_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SAMPLECOSTING_MASTER AS SELECT '' COSTING_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SB_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SB_EXCHANGE_DETAILS AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SB_MASTER AS SELECT '' INVOICE_NO ;");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SB_TERM AS SELECT '' INVOICE_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SBT_DETAILS AS SELECT '' INVOICE_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SBT_MASTER AS SELECT '' INVOICE_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SBT_TERM AS SELECT '' INVOICE_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_CHALLAN_MASTER AS SELECT '' CHALLAN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_CHALLAN_DETAILS AS SELECT '' CHALLAN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_CHALLAN_TERM AS SELECT '' CHALLAN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_CHALLAN_MASTER_OTHER_DETAILS AS SELECT '' CHALLAN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_ORDER_DETAILS AS SELECT '' ORDER_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_ORDER_MASTER AS SELECT '' ORDER_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_ORDER_MASTER_OTHER_DETAILS AS SELECT '' ORDER_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_ORDER_TERM AS SELECT '' ORDER_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_QUOTATION_TERM AS SELECT '' QUOTATION_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_QUOTATION_DETAILS AS SELECT '' QUOTATION_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_QUOTATION_MASTER AS SELECT '' QUOTATION_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_RETURN_MASTER AS SELECT '' RETURN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_RETURN_DETAILS AS SELECT '' RETURN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_RETURN_TERM AS SELECT '' RETURN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SALES_RETURN_OTHER_MASTER AS SELECT '' RETURN_NO ; ");
        }
        catch
        {
        }

        try
        {
            var result = SqlExtensions.ExecuteNonQuery(
                "CREATE VIEW TVIEW.VIEW_SRT_TERM AS SELECT '' INVOICE_NO ; AS SELECT '' SRT_ID ; ");
        }
        catch
        {
        }
    }
}