using MrDAL.Core.Extensions;
using MrDAL.Global.Common;
using MrDAL.Reports.Interface;
using MrDAL.Reports.Register;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MrDAL.Reports.Design;

public class RegisterReportDesign : IRegisterDesign
{
    // OBJECT FOR THIS FORM

    #region **--------------- OBJECT ---------------**

    public IRegisterReport RegisterReport { get; } = new ClsRegisterReport();

    #endregion **--------------- OBJECT ---------------**

    // AGING REPORT DESIGN

    #region **--------------- AGING REPORT DESIGN ---------------**

    public DataGridView GetPartyAgingReportDesign(DataGridView rGrid, int days = 30, int noColumn = 4)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "LEDGER_ID", "Ledger_ID", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", "DESCRIPTION", "GLName", 200, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAmount", "DUE AMOUNT", "DueAmount", 110, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSlab1", $"[{days}] DAYS DUE", "FirstSlab", 110, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSlab2", $"[{days + days}] DAYS DUE", "SecondSlab", 110, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSlab3", $"[{days + days + days}] DAYS DUE", "ThirdSlab", 110, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        if (noColumn is 7)
        {
            rGrid.AddColumn("GTxtSlab4", $"[{days + days + days + days}] DAYS DUE", "FourthSlab", 110, 110, true,
                DataGridViewContentAlignment.MiddleRight);
            rGrid.AddColumn("GTxtSlab5", $"[{days + days + days + days + days}] DAYS DUE", "FifthSlab", 110, 110, true,
                DataGridViewContentAlignment.MiddleRight);
            rGrid.AddColumn("GTxtSlab5", $"[{days + days + days + days + days + days}] DAYS DUE", "SixthSlab", 110, 110,
                true, DataGridViewContentAlignment.MiddleRight);
            rGrid.AddColumn("GTxtSlab6", $"ABOVE [{days + days + days + days + days + days + days}] DUE", "LastSlab",
                110, 110, true, DataGridViewContentAlignment.MiddleRight);
        }
        else
        {
            rGrid.AddColumn("GTxtSlab4", $"ABOVE [{days + days + days}] DUE", "LastSlab", 110, 110, true,
                DataGridViewContentAlignment.MiddleRight);
        }

        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    #endregion **--------------- AGING REPORT DESIGN ---------------**

    //GetVatRegisterTransactionDesign

    #region **--------------- VAT REGISTER ---------------**

    public DataGridView GetVatRegisterTransactionDesign(DataGridView rGrid, bool isEnglish = true)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid = new DataGridView();
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;

        rGrid.AddColumn("GTxtLedgerId", "LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtPanNo", !isEnglish ? "खरिदकर्ताको स्थायी लेखा नम्बर" : "PAN", "PanNo", 110, 110, true,
            DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtLedger", !isEnglish ? "खरिदकर्ताको नाम" : "NAME OF TAX PAYER", "Ledger", 200, 200, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtTradeNameType", !isEnglish ? "ट्रेड नाम प्रकार" : "TRADE NAME TYPE", "TradeNameType", 110,
            110, true);
        rGrid.AddColumn("GTxtPurchaseSales", !isEnglish ? "खरिद / बिक्री" : "PURCHASE/SALE", "PurchaseSales", 110, 110,
            true);
        rGrid.AddColumn("GTxtTaxableSalesValue", !isEnglish ? "करयोग्य बिक्री मूल्य (रु)" : "TAXABLE AMOUNT",
            "TaxableString", 150, 150, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxSalesAmount", !isEnglish ? "करयोग्य बिक्री कर (रु)" : "EXEMPTED AMOUNT",
            "TaxFreeString", 150, 150, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);

        return rGrid;
    }

    public DataGridView GetNormalVatRegisterDesign(DataGridView rGrid, bool isEnglish = true)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid = new DataGridView();
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtSalesDate", !isEnglish ? "मिति" : "DATE", "dt_SalesDate", 90, 90);
        rGrid.AddColumn("GTxtSalesMiti", !isEnglish ? "मिति" : "MITI", "dt_SalesMiti", 90, 90);
        rGrid.AddColumn("GTxtSalesNo", !isEnglish ? "बीजक नम्बर" : "BILL_NO", "dt_SalesNo", 120, 120, true);
        rGrid.AddColumn("GTxtCustomerLedger", !isEnglish ? "खरिदकर्ताको नाम" : "PURCHASER_NAME", "dt_CustomerLedger",
            200, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtCustomerPanNo", !isEnglish ? "खरिदकर्ताको स्थायी लेखा नम्बर" : "PURCHASER_PAN",
            "dt_CustomerPanNo", 110, 110, true, DataGridViewContentAlignment.MiddleCenter);

        rGrid.AddColumn("GTxtSalesAmount", !isEnglish ? "जम्मा बिक्री / निकासी (रु)" : "NET_AMOUNT", "dt_TotalSales",
            110, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExemptedSales", !isEnglish ? "स्थानीय कर छुटको बिक्री  मूल्य (रु)" : "EXEMPTED_SALES",
            "dt_TaxFreeSales", 110, 110, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtTaxableSalesValue", !isEnglish ? "करयोग्य बिक्री मूल्य (रु)" : "TAXABLE_VALUE",
            "dt_TaxableSales", 110, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxSalesAmount", !isEnglish ? "करयोग्य बिक्री कर (रु)" : "TAX_AMOUNT", "dt_VatSales", 110,
            110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExportSales", !isEnglish ? "निकासी गरेको वस्तु वा सेवाको मूल्य (रु)" : "EXPORT_SALES",
            "dt_ExportSales", 110, 110, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtPurchaseDate", !isEnglish ? @"मिति" : "DATE", "dt_PurchaseDate", 120, 90, true);
        rGrid.AddColumn("GTxtPurchaseMiti", !isEnglish ? @"मिति" : "MITI", "dt_PurchaseMiti", 120, 90, true);
        rGrid.AddColumn("GTxtPurchaseNo", !isEnglish ? @"बीजक नं./ प्रज्ञापनपत्र नं." : "BILL_NO", "dt_PurchaseNo", 120,
            90, true);
        rGrid.AddColumn("GTxtVendorLedger", !isEnglish ? @"आपूर्तिकर्ताको नाम" : "PURCHASER_NAME", "dt_VendorLedger",
            400, 300, true);
        rGrid.AddColumn("GTxtVendorPanNo", !isEnglish ? @"आपूर्तिकर्ताको स्थायी लेखा नम्बर" : "PURCHASE_PAN",
            "dt_VendorPanNo", 100, 75, true, DataGridViewContentAlignment.BottomCenter);

        rGrid.AddColumn("GTxtPurchaseAmount", !isEnglish ? @"जम्मा खरिद मूल्य (रु)" : "NET_AMOUNT", "dt_TotalPurchase",
            120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExemptedPurchase",
            !isEnglish ? @"कर छुट हुने वस्तु वा सेवाको खरिद / पैठारी मूल्य (रु)" : "EXEMPTED_PURCHASE",
            "dt_TaxFreePurchase", 120, 90, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtImportTaxablePurchase",
            !isEnglish ? @"करयोग्य पैठारी (पूंजीगत बाहेक) मूल्य (रु)" : "IMPORT_TAXABLE", "dt_ImportPurchase", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAdditionalVatPurchase",
            !isEnglish ? @"करयोग्य पैठारी (पूंजीगत बाहेक) कर (रु)" : "IMPORT_VAT", "dt_ImportVatPurchase", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtTaxablePurchase",
            !isEnglish ? @"करयोग्य खरिद (पूंजीगत बाहेक) मूल्य (रु)" : "TAXABLE_VALUE", "dt_TaxablePurchase", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVatPurchaseAmount", !isEnglish ? @"करयोग्य खरिद (पूंजीगत बाहेक) कर (रु)" : "TAX_AMOUNT",
            "dt_VatPurchase", 120, 90, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtCapitalTaxablePurchase",
            !isEnglish ? @"पूंजीगत करयोग्य खरिद / पैठारी  मूल्य (रु)" : "CAPITAL_TAXABLE", "dt_CapitalPurchase", 120,
            90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCapitalVatPurchase", !isEnglish ? @"पूंजीगत करयोग्य खरिद / पैठारी कर (रु)" : "CAPITAL_VAT",
            "dt_CapitalVatPurchase", 120, 90, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        //rGrid.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
        //rGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        return rGrid;
    }

    #endregion **--------------- VAT REGISTER ---------------**

    // SALES & PURCHASE REGISTER

    #region **--------------- SALES / PURCHASE REGISTER ---------------**

    public DataGridView GetSalesVatDetailsRegisterDesign(DataGridView rGrid, bool isEnglish = true)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid = new DataGridView();
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.ColumnHeadersHeight = 65;
        rGrid.AddColumn("GTxtVoucherDate", !isEnglish ? "मिति" : "DATE", "VoucherDate", 120, 90);
        rGrid.AddColumn("GTxtVoucherMiti", !isEnglish ? "मिति" : "MITI", "VoucherMiti", 120, 90);
        rGrid.AddColumn("GTxtVoucherNo", !isEnglish ? "बीजक नम्बर" : "BILL NO", "VoucherNo", 150, 120, true);
        rGrid.AddColumn("GTxtNoOfBills", !isEnglish ? "बीजक नम्बर" : "NO OF BILLS", "NoOfBill", 40, 40, false);
        rGrid.AddColumn("GTxtLedgerId", "LEDGER_ID", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", !isEnglish ? "खरिदकर्ताको नाम" : "PURCHASER NAME", "Ledger", 200, 200, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtPanNo", !isEnglish ? "खरिदकर्ताको स्थायी लेखा नम्बर" : "PURCHASER PAN", "PanNo", 120, 110,
            true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtInvoiceType", !isEnglish ? "वस्तु वा सेवाको नाम" : "PRODUCT TYPE", "Category", 90, 110,
            true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtInvoiceQty", !isEnglish ? "वस्तु वा सेवाको परिमाण" : "PRODUCT QTY", "Qty", 90, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", !isEnglish ? "जम्मा बिक्री / निकासी (रु)" : "TOTAL SALES", "TotalAmount", 150,
            110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExemptedSales", !isEnglish ? "स्थानीय कर छुटको बिक्री  मूल्य (रु)" : "EXEMPTED SALES",
            "TaxFree", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxableValue", !isEnglish ? "करयोग्य बिक्री मूल्य (रु)" : "TAXABLE VALUE", "Taxable", 150,
            110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxAmount", !isEnglish ? "करयोग्य बिक्री कर (रु)" : "TAX AMOUNT", "VatAmount", 150, 110,
            true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExportSales", !isEnglish ? "निकासी गरेको वस्तु वा सेवाको मूल्य (रु)" : "EXPORT SALES",
            "ExportSales", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExportCountry", !isEnglish ? "निकासी गरेको देश" : "EXPORT COUNTRY", "ExportCountry", 150,
            110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExportVoucherNo", !isEnglish ? "निकासी प्रज्ञापनपत्र नम्बर" : "EXPORT VOUCHER NO",
            "ExportVoucherNo", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExportMiti", !isEnglish ? "निकासी प्रज्ञापनपत्र मिति" : "EXPORT MITI", "ExportMiti", 150,
            110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        foreach (DataGridViewColumn column in rGrid.Columns)
        {
            if (!column.Visible) continue;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = column.DefaultCellStyle.Alignment switch
            {
                DataGridViewContentAlignment.MiddleRight => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        rGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        return rGrid;
    }

    public DataGridView GetPurchaseVatDetailsRegisterDesign(DataGridView rGrid, bool isEnglish = true)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.ColumnHeadersHeight = 65;
        rGrid.AddColumn("GTxtDate", !isEnglish ? @"मिति" : "DATE", "VoucherDate", 120, 90, true);
        rGrid.AddColumn("GTxtMiti", !isEnglish ? @"मिति" : "MITI", "VoucherMiti", 120, 90, true);
        rGrid.AddColumn("GTxtVoucherNo", !isEnglish ? @"बीजक नं./ प्रज्ञापनपत्र नं." : "BILL_NO", "VoucherNo", 120, 90,
            true);
        rGrid.AddColumn("GTxtRefVno", !isEnglish ? @"बीजक नं./ प्रज्ञापनपत्र नं." : "REF_BILL_NO", "RefVno", 120, 90,
            true);
        rGrid.AddColumn("GTxtLedgerId", @"LEDGER_ID", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", !isEnglish ? @"आपूर्तिकर्ताको नाम" : "PURCHASER_NAME", "Ledger", 400, 300, true);
        rGrid.AddColumn("GTxtPanNo", !isEnglish ? @"आपूर्तिकर्ताको स्थायी लेखा नम्बर" : "PURCHASE_PAN", "PanNo", 100,
            75, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtInvoiceType", !isEnglish ? @"खरिद/पैठारी गरिएका वस्तु वा सेवाको विवरण" : "PRODUCT_TYPE",
            "Category", 100, 75, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtInvoiceQty", !isEnglish ? @"खरिद/पैठारी गरिएका वस्तु वा सेवाको परिमाण" : "TOTAL_QTY",
            "Qty", 100, 75, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtQtyUnit", !isEnglish ? @"वस्तु वा सेवाको एकाई" : "QTY UNIT", "Unit", 100, 75, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", !isEnglish ? @"जम्मा खरिद मूल्य (रु)" : "NET_AMOUNT", "TotalAmount", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxFreeAmount",
            !isEnglish ? @"कर छुट हुने वस्तु वा सेवाको खरिद / पैठारी मूल्य (रु)" : "TAX_EXEMPTED", "TaxFree", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtTaxable", !isEnglish ? @"करयोग्य खरिद (पूंजीगत बाहेक) मूल्य (रु)" : "TAXABLE", "Taxable",
            120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVatAmount", !isEnglish ? @"करयोग्य खरिद (पूंजीगत बाहेक) कर (रु)" : "VAT", "VatAmount", 120,
            90, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtImportTaxable",
            !isEnglish ? @"करयोग्य पैठारी (पूंजीगत बाहेक) मूल्य (रु)" : "IMPORT_TAXABLE", "ImportTaxable", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAdditionalVat", !isEnglish ? @"करयोग्य पैठारी (पूंजीगत बाहेक) कर (रु)" : "IMPORT_VAT",
            "ImportVatAmount", 120, 90, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtCapitalTaxable",
            !isEnglish ? @"पूंजीगत करयोग्य खरिद / पैठारी  मूल्य (रु)" : "CAPITAL_TAXABLE", "CapitalTaxable", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCapitalVat", !isEnglish ? @"पूंजीगत करयोग्य खरिद / पैठारी कर (रु)" : "CAPITAL_VAT",
            "CapitalVatAmount", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        rGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        foreach (DataGridViewColumn column in rGrid.Columns)
        {
            if (!column.Visible) continue;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = column.DefaultCellStyle.Alignment switch
            {
                DataGridViewContentAlignment.MiddleRight => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        rGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        return rGrid;
    }

    public DataGridView GetPurchaseSalesVatRegisterSummary(DataGridView rGrid, bool isPurchase)
    {
        if (rGrid.ColumnCount > 0)
        {
            rGrid.Columns.Clear();
            rGrid.DataSource = null;
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtSNo", "SNO", "Sno", 45, 2, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtLedgerId", "LEDGER_ID", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", "BUYER_NAME", "Ledger", 400, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtPanNo", "BUYER_PAN", "PanNo", 120, 90, true);
        rGrid.AddColumn("GTxtNetAmount", "NET_AMOUNT", "TotalAmount", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExemptedAmount", "EXEMPTED", "TaxFree", 150, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtExemptedExport", isPurchase ? "IMPORT_EXEMPTED" : "EXPORT_EXEMPTED", "ExportSales", 150,
            90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxable", "TAXABLE", "Taxable", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTax", "VAT", "VatAmount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "GROUP", "IsGroup", 0, 2, false);
        if (!isPurchase) return rGrid;
        rGrid.AddColumn("GTxtImport", "IMPORT", "dt_ImportAmount", 100, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtImportTax", "VAT", "dt_ImportTax", 100, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCapital", "CAPITAL", "dt_CapitalAmount", 100, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtCapitalTax", "VAT", "dt_CapitalTax", 100, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        return rGrid;
    }

    public DataGridView GetPurchaseSalesSummaryRegisterDesign(DataGridView rGrid, string mode = "PB",
        bool isProduct = false)
    {
        string[] regModule = { "PIN", "PO", "PC", "PB", "PR" };
        if (rGrid.ColumnCount > 0)
        {
            rGrid.Columns.Clear();
            rGrid.DataSource = null;
        }

        rGrid.AutoGenerateColumns = false;
        var dtTerm = RegisterReport.GetPurchaseSalesTermName(regModule.Contains(mode));
        rGrid.AddColumn("GTxtDate", @"DATE", "VoucherDate", 120, 90, !isProduct, DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtMiti", @"MITI", "VoucherMiti", 120, 90, !isProduct, DataGridViewAutoSizeColumnMode.DisplayedCells);

        rGrid.AddColumn("GTxtInvoiceNo", isProduct ? "SHORT NAME" : @"VOUCHER NO", !isProduct ? "VoucherNoWithRef" : "PShortName", 200, 90, true, DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "LedgerDesc", 400, 300, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtPanNo", @"PAN NO", "PanNo", 100, 2, true);
        if (isProduct)
        {
            rGrid.AddColumn("GTxtAltQty", @"ALT. QTY", "AltQty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
            rGrid.AddColumn("GTxtAltUnit", @"UOM", "AltUom", 120, 90, true, DataGridViewContentAlignment.MiddleCenter);
        }
        rGrid.AddColumn("GTxtQty", isProduct ? @"QTY" : "TOTAL QTY", "Qty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        if (isProduct)
        {
            rGrid.AddColumn("GTxtUnit", @"UOM", "Uom", 120, 90, true, DataGridViewContentAlignment.MiddleCenter);
        }
        rGrid.AddColumn("GTxtBasic", dtTerm.Rows.Count > 0 ? @"BASIC AMOUNT" : "BILL AMOUNT", "BasicAmount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        if (dtTerm.Rows.Count > 0)
        {
            foreach (DataRow row in dtTerm.Rows)
            {
                var col = row["TermDesc"].GetUpper();
                rGrid.AddColumn(col, col, col, 150, 90, true, DataGridViewContentAlignment.MiddleRight);
            }
            rGrid.AddColumn("GTxtNetAmount", "NET AMOUNT", "NetAmount", 150, 90, true, DataGridViewContentAlignment.MiddleRight);
        }

        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        if (!isProduct)
        {
            rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "VoucherNo", 0, 2, false);
            rGrid.AddColumn("InvoiceMode", @"InvoiceMode", "Invoice_Mode", 0, 2, false);
            rGrid.AddColumn("PaymentMode", @"MODE", "Payment_Mode", 50, 2, true, DataGridViewContentAlignment.MiddleCenter);
        }

        rGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        foreach (DataGridViewColumn column in rGrid.Columns)
        {
            if (!column.Visible)
            {
                continue;
            }
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = column.DefaultCellStyle.Alignment switch
            {
                DataGridViewContentAlignment.MiddleRight => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        rGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        return rGrid;
    }

    public DataGridView GetPurchaseSalesDetailsRegisterDesign(DataGridView rGrid, string mode = "SB",
        bool isHorizon = false, bool isProduct = false)
    {
        string[] regModule = { "PIN", "PO", "PC", "PB", "PR" };
        if (rGrid.ColumnCount > 0)
        {
            rGrid.Columns.Clear();
            rGrid.DataSource = null;
        }

        rGrid.AutoGenerateColumns = false;
        var isPurchase = regModule.Contains(mode);
        var dtTerm = RegisterReport.GetPurchaseSalesTermName(isPurchase);
        var vatId = isPurchase ? ObjGlobal.PurchaseVatTermId : ObjGlobal.SalesVatTermId;
        rGrid.AddColumn("GTxtDate", @"DATE", "VoucherDate", 120, 90, true);
        rGrid.AddColumn("GTxtMiti", @"MITI", "VoucherMiti", 120, 90, true);
        rGrid.AddColumn("GTxtInvoiceNo", @"VOUCHER_NO", "VoucherNoWithRef", 150, 90, true,
            DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader);
        rGrid.AddColumn("GTxtLedgerId", @"LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", @"PARTICULARS", "LedgerDesc", 400, 300, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAltQty", @"ALT_QTY", "AltQty", 120, 90, false, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAltUOM", @"ALT_UOM", "AltUom", 75, 75, false);
        rGrid.AddColumn("GTxtQty", @"QTY", "Qty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtUOM", @"UOM", "Uom", 75, 75, true);
        rGrid.AddColumn("GTxtRate", @"RATE", "Rate", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBasic", dtTerm.Rows.Count > 0 ? @"BASIC_AMOUNT" : "NET_AMOUNT", "BasicAmount", 120, 90,
            true, DataGridViewContentAlignment.MiddleRight, DataGridViewColumnSortMode.NotSortable,
            DataGridViewAutoSizeColumnMode.ColumnHeader);
        if ((isHorizon && dtTerm.Rows.Count > 0) || (isProduct && dtTerm.Rows.Count > 0))
        {
            foreach (DataRow row in dtTerm.Rows)
            {
                if (vatId == row["TermId"].GetInt())
                    rGrid.AddColumn("GTxtTaxable", "TAXABLE", "Taxable", 120, 90, true,
                        DataGridViewContentAlignment.MiddleRight);
                rGrid.AddColumn(row["TermDesc"].ToString(), row["TermDesc"].ToString(), row["TermDesc"].ToString(), 120,
                    90, true, DataGridViewContentAlignment.MiddleRight);
            }

            rGrid.AddColumn("GTxtNetAmount", "NET_AMOUNT", "NetAmount", 120, 90, true,
                DataGridViewContentAlignment.MiddleRight);
        }

        rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "VoucherNo", 0, 2, false);
        rGrid.AddColumn("GTxtModule", @"MODULE", "Invoice_Mode", 0, 2, false);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    #endregion **--------------- SALES / PURCHASE REGISTER ---------------**

    //AUDIT LOG REGISTER DESIGN

    #region **--------------- AUDIT LOG REGISTER ---------------**

    public DataGridView GetMaterializeViewRegisterDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.ColumnHeadersHeight = 65;
        rGrid.AddColumn("GTxtFiscalYear", @"FISCAL YEAR", "Fiscal_Year", 120, 90, true);
        rGrid.AddColumn("GTxtVoucherNo", @"BILL NO", "FromBill_No", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtLedger", @"CUSTOMER NAME", "Customer_Name", 275, 200, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtPanNo", @"CUSTOMER PAN", "Customer_PAN", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtMiti", @"BILL MITI", "Invoice_Miti", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtDate", @"BILL DATE", "Bill_Date", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtAmount", @"AMOUNT", "Amount", 90, 75, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDiscount", @"DISCOUNT", "Discount", 90, 75, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxFreeAmount", @"TAX EXEMPTED", "TaxFree_Sales", 90, 75, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTaxable", @"TAXABLE AMOUNT", "Taxable_Amount", 90, 75, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVatAmount", @"TAX AMOUNT", "Tax_Amount", 90, 75, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtTotalAmount", @"TOTAL AMOUNT", "TotalAmount", 90, 75, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtSyncWithIRD", @"SYNC WITH IRD", "IsAPIPosted", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtIsPrinted", @"IS BILL PRINTED", "Is_Printed", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtIsActive", @"IS BILL ACTIVE", "Is_Active", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtPrintedTime", @"PRINTED TIME", "Printed_Date", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtEnteredBy", @"ENTERED BY", "Entered_By", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtPrintedBy", @"PRINTED BY", "Printed_By", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtIsRealtime", @"IS REALTIME", "IsRealtime", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtPaymentMode", @"PAYMENT METHOD", "Payment_Mode", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtVatRefundAmount", @"VAT REFUND AMOUNT", "VatRefundAmount", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("GTxtTransactionId", @"TRANSACTION ID", "TransactionId", 90, 75, true,
            DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.AddColumn("IsGroup", @"IsGroup", "IsGroup", 0, 2, false, DataGridViewAutoSizeColumnMode.DisplayedCells);
        rGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        foreach (DataGridViewColumn column in rGrid.Columns)
        {
            if (!column.Visible) continue;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            column.DefaultCellStyle.Alignment = column.DefaultCellStyle.Alignment switch
            {
                DataGridViewContentAlignment.MiddleRight => DataGridViewContentAlignment.MiddleRight,
                _ => DataGridViewContentAlignment.MiddleLeft
            };
        }

        rGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        return rGrid;
    }

    public DataGridView GetEntryLogRegisterDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtModule", @"VOUCHER_MODULE", "VOUCHER_MODULE", 120, 90, true);
        rGrid.AddColumn("GTxtVoucherNo", @"VOUCHER_NO", "VOUCHER_NO", 120, 90, true);
        rGrid.AddColumn("GTxtMiti", @"VOUCHER_MITI", "VOUCHER_MITI", 120, 90, true);
        rGrid.AddColumn("GTxtDate", @"VOUCHER_DATE", "VOUCHER_DATE", 120, 90, true);
        rGrid.AddColumn("GTxtVoucherTime", @"VOUCHER_TIME", "VOUCHER_TIME", 120, 90, true);
        rGrid.AddColumn("GTxtVoucherTime", @"VOUCHER_TIME", "VOUCHER_TIME", 120, 90, true);
        rGrid.AddColumn("GTxtEntryType", @"VOUCHER_TYPE", "VOUCHER_TYPE", 120, 90, true);
        rGrid.AddColumn("GTxtAmount", @"AMOUNT", "AMOUNT", 120, 90, true);
        rGrid.AddColumn("GTxtEnterBy", @"ENTER_BY", "ENTER_BY", 120, 90, true);
        rGrid.AddColumn("GTxtEnterDate", @"ENTER_DATE", "ENTER_DATE", 120, 90, true);
        return rGrid;
    }

    #endregion **--------------- AUDIT LOG REGISTER ---------------**

    // OUTSTANDING REPORT DESIGN

    #region **--------------- OUTSTANDING REPORT DESIGN ---------------**

    public DataGridView GetPartyOutstandingDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", "DATE", "VoucherDate", 120, 90, false);
        rGrid.AddColumn("GTxtMiti", "MITI", "VoucherMiti", 120, 90, false);
        rGrid.AddColumn("GTxtLedgerId", "LEDGER_ID", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", "DESCRIPTION", "LedgerDesc", 200, 200, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAmount", "INVOICE AMOUNT", "VoucherAmount", 180, 110, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAdjVoucher", "ADJ_VOUCHER", "AdjustVoucherNo", 120, 110, false);
        rGrid.AddColumn("GTxtAdjDate", "ADJ_DATE", "AdjustDate", 120, 90, false);
        rGrid.AddColumn("GTxtAdjMiti", "ADJ_MITI", "AdjustMiti", 120, 90, false);
        rGrid.AddColumn("GTxtAdjustment", "ADJUSTMENT", "BalanceAmount", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalance", "BALANCE", "OutStanding", 150, 150, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtDueAge", "DUE_DAYS", "DateAge", 150, 110, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtVoucherNo", "VoucherNo", "VoucherNo", 0, 2, false);
        rGrid.AddColumn("GTxtModule", "Module", "Module", 150, 110, false, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false);
        return rGrid;
    }

    public DataGridView GetOutstandingRegisterDesign(DataGridView rGrid, string mode = "SB")
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtDate", @"DATE", "VoucherDate", 120, 90, true);
        rGrid.AddColumn("GTxtMiti", @"MITI", "VoucherMiti", 120, 90, true);
        rGrid.AddColumn("GTxtVoucherNo", "VOUCHER NO", "VoucherNo", 120, 90, true,
            DataGridViewAutoSizeColumnMode.AllCells);
        rGrid.AddColumn("GTxtLedgerId", "LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", "PARTICULARS", "Ledger", 275, 190, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtAltQty", "ALT QTY", "AltQty", 120, 90, false, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAltUom", "UOM", "AltUom", 120, 90, false, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtQty", "QTY", "Qty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtUOM", "UOM", "Uom", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtRate", "RATE", "Rate", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", "AMOUNT", "Amount", 120, 90, true, DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtAdjustDate", "ADJ_DATE", "AdjustDate", 120, 90, false);
        rGrid.AddColumn("GTxtAdjustMiti", "ADJ_MITI", "AdjustMiti", 120, 90, false);
        rGrid.AddColumn("GTxtAdjustVoucher", "ADJ_VOU", "AdjustVoucher", 120, 90, true,
            DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader);
        rGrid.AddColumn("GTxtAdjustQty", "ADJ_QTY", "AdjustQty", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceQty", "BAL_QTY", "BalanceQty", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);

        rGrid.AddColumn("GTxtDueDays", "DUE_DAYS", "DateDiff", 100, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("IsGroup", "IsGroup", "IsGroup", 0, 2, false, DataGridViewContentAlignment.MiddleRight);
        rGrid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        foreach (DataGridViewColumn column in rGrid.Columns)
            rGrid.Columns[column.Index].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
        return rGrid;
    }

    #endregion **--------------- OUTSTANDING REPORT DESIGN ---------------**

    // ANALYSIS REPORT DESIGN

    #region **--------------- ANALYSIS REPORT DESIGN ---------------**

    public DataGridView GetTopNRegisterDesign(DataGridView rGrid, string mode = "C")
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtLedgerId", "LedgerId", "LedgerId", 0, 2, false);
        rGrid.AddColumn("GTxtLedger", "SHORTNAME", "ShortName", 100, 100, true);
        rGrid.AddColumn("GTxtLedger", "PARTICULARS", "Ledger", 275, 190, true, DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtPan", "PAN_NO", "PanNo", 90, 80, true, DataGridViewContentAlignment.MiddleCenter);
        rGrid.AddColumn("GTxtQty", "QTY", "Qty", 120, 90, mode.Equals("P"), DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", "AMOUNT", "Amount", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReturnQty", "RETURN_QTY", "ReturnQty", 120, 90, mode.Equals("P"),
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReturnAmount", "RETURN", "ReturnAmount", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReturnQty", "RETURN_QTY", "GTxtRate", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceQty", "BALANCE_QTY", "BalanceQty", 120, 90, mode.Equals("P"),
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceAmount", "BALANCE", "NetAmount", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.NotSet;
        rGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        return rGrid;
    }

    public DataGridView GetSalesAnalysisDesign(DataGridView rGrid)
    {
        rGrid.ReadOnly = true;
        if (rGrid.ColumnCount > 0)
        {
            rGrid.DataSource = null;
            rGrid.Columns.Clear();
        }

        rGrid.AutoGenerateColumns = false;
        rGrid.AddColumn("GTxtParticular", "PARTICULARS", "Description", 275, 190, true,
            DataGridViewAutoSizeColumnMode.Fill);
        rGrid.AddColumn("GTxtQty", "SALES_QTY", "SalesQty", 120, 90, true, DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtAmount", "sALES_AMOUNT", "SalesAmount", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReturnQty", "RETURN_QTY", "ReturnQty", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtReturnAmount", "RETURN_AMOUNT", "ReturnAmount", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceQty", "NET_SALES_QTY", "NetSalesQty", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        rGrid.AddColumn("GTxtBalanceAmount", "NET_SALES_AMOUNT", "NetSalesAmount", 120, 90, true,
            DataGridViewContentAlignment.MiddleRight);
        return rGrid;
    }

    #endregion **--------------- ANALYSIS REPORT DESIGN ---------------**
}