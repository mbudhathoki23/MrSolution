using System.Windows.Forms;

namespace MrDAL.Reports.Interface;

public interface IRegisterDesign
{
    // SALES & PURCHASE INVOICE REGISTER

    #region --------------- SALES_PURCHASE INVOICE REGISTER ---------------

    DataGridView GetPurchaseSalesDetailsRegisterDesign(DataGridView rGrid, string mode, bool isHorizon,
        bool isProduct = false);

    DataGridView GetPurchaseSalesSummaryRegisterDesign(DataGridView rGrid, string mode, bool isProduct = false);

    #endregion --------------- SALES_PURCHASE INVOICE REGISTER ---------------

    // PARTY OUTSTANDING REPORTS

    #region --------------- PARTY OUTSTANDING REPORT ---------------

    DataGridView GetPartyOutstandingDesign(DataGridView rGrid);

    DataGridView GetSalesAnalysisDesign(DataGridView rGrid);

    DataGridView GetOutstandingRegisterDesign(DataGridView rGrid, string mode = "SB");

    DataGridView GetTopNRegisterDesign(DataGridView rGrid, string mode = "C");

    DataGridView GetPartyAgingReportDesign(DataGridView rGrid, int days = 30, int noColumn = 4);

    #endregion --------------- PARTY OUTSTANDING REPORT ---------------

    // SALES & PURCHASE VAT REGISTER

    #region --------------- SALES & PURCHASE VAT REGISTER ---------------

    DataGridView GetNormalVatRegisterDesign(DataGridView rGrid, bool isEnglish = true);

    DataGridView GetVatRegisterTransactionDesign(DataGridView rGrid, bool isEnglish = true);

    DataGridView GetSalesVatDetailsRegisterDesign(DataGridView rGrid, bool isEnglish = true);

    DataGridView GetPurchaseVatDetailsRegisterDesign(DataGridView rGrid, bool isEnglish = true);

    DataGridView GetMaterializeViewRegisterDesign(DataGridView rGrid);

    DataGridView GetEntryLogRegisterDesign(DataGridView rGrid);

    DataGridView GetPurchaseSalesVatRegisterSummary(DataGridView rGrid, bool isPurchase);

    #endregion --------------- SALES & PURCHASE VAT REGISTER ---------------
}