using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using DatabaseModule.DataEntry.SalesMaster.SalesQuotation;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using DatabaseModule.Domains.NightAudit;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MrDAL.DataEntry.Interface;

public interface ISalesEntry
{
    #region --------------- I_U_D VALUE ---------------

    #region **---------- SALES INVOICE ---------**

    // INSERT UPDATE DELETE EVENTS


    int SaveUnSyncSalesFromServerAsync(SB_Master salesModel, string actionTag);

    Task<int> UpdateSyncSalesColumnInServerAsync();

    int SavePosInvoice(string actionTag);

    int SaveOpdBillingInvoice(string actionTag);

    int SavePosReturnInvoice(string actionTag);

    int SaveTempSalesInvoice(string actionTag);

    int SaveSalesQuotation(string actionTag);

    int SaveUnSyncSalesChallanFromServerAsync(SC_Master salesChallanModel, string actionTag);

    int SaveUnSyncSalesReturnFromServerAsync(SR_Master salesReturnModel, string actionTag);

    int UpdateDocumentNumbering(string module, string documentDesc);


    // RETURN SYNC ROW VALUE IN SHORT
    short ReturnSyncRowVersionVoucher(string module, string voucherNo);

    // ACCOUNTING POSTING EVENTS
    int SalesInvoiceAccountPosting();

    int SalesReturnAccountPosting();

    int SalesTermPostingAsync();

    int SalesReturnTermPosting();



    // STOCK POSTING EVENTS
    int SalesChallanStockPosting();

    int SalesInvoiceStockPosting();

    int SalesReturnStockPosting();

    int SaveNightAuditLog(string actionTag);

    #endregion **---------- SALES INVOICE ---------**



    // RETURN VALUE IN BOOLEAN
    bool UpdateTransferTableSalesOrderMaster(string orderNo, int tableId, int fromTableId);

    #endregion --------------- I_U_D VALUE ---------------

    // RETURN VALUE IN DATA TABLE

    #region --------------- RETURN DATATABLE ---------------

    DataTable GetTableOrderDetails(int tableId);

    DataTable GetOrderData(string orderNo);

    DataTable GetTodaySales();

    DataTable GetSalesData();

    DataTable GetSalesDataReportPaymentType(string date);

    DataTable CheckTableOrderExitsOrNot(int tableId);

    DataTable CheckVoucherNoExitsOrNot(string tableName, string tableVoucherNo, string inputVoucherNo);

    DataTable CheckPrintDocument(string module);

    DataTable IsNightAuditExits(string date);

    DataTable ReturnLastNightAuditLog();

    #endregion --------------- RETURN DATATABLE ---------------

    // RETURN VALUE IN DATA SET

    #region --------------- RETURN DATASET ---------------

    DataSet ReturnSalesQuotationDetailsInDataSet(string voucherNo);

    DataSet ReturnSalesChallanDetailsInDataSet(string voucherNo);

    DataSet ReturnPrintKotDetailsInDataSet(string voucherNo);

    DataSet ReturnSalesOrderDetailsInDataSet(string voucherNo);

    DataSet ReturnSalesInvoiceDetailsInDataSet(string voucherNo);

    DataSet ReturnSalesReturnDetailsInDataSet(string voucherNo);

    DataSet ReturnTempSalesInvoiceDetailsInDataSet(string voucherNo);

    DataSet GetSalesInvoiceDetailsForPrint(string billNo);

    DataSet GetSalesReturnDetailsForPrint(string billNo);

    DataSet GetConfirmationSalesDetails(string billNo);

    #endregion --------------- RETURN DATASET ---------------

    // API POSTING EVENTS

    #region --------------- API POSTING ---------------

    void PostingSalesReturnToApi(string invoiceNo);

    void PostingBillToApi(string invoiceNo);

    #endregion --------------- API POSTING ---------------

    //RETURN STRING VALUE

    #region --------------- RETURN STRING VALUE ---------------

    string GetInvoicePaymentMode(long ledgerId);

    (string invoice, string avtInvoice) GetPointOfSalesDesign();

    #endregion --------------- RETURN STRING VALUE ---------------

    // TABLE MASTER

    #region --------------- TABLE MASTER  ---------------

    public bool CheckTableMasterExist(int tableId);

    #endregion

    // OBJECT

    #region --------------- DECLARE LOCAL VARIABLE  ---------------

    SO_Details SoDetails { get; set; }
    SQ_Master SqMaster { get; set; }
    SO_Master SoMaster { get; set; }
    SC_Master ScMaster { get; set; }
    SB_Master SbMaster { get; set; }
    Temp_SB_Master TsbMaster { get; set; }
    List<SB_Details> SbDetails { get; set; }
    List<SB_Term> SbTerms { get; set; }
    SB_Master_OtherDetails SbOther { get; set; }
    SR_Master SrMaster { get; set; }
    List<SR_Details> SrDetails { get; set; }
    List<SR_Term> SrTerms { get; set; }
    NightAuditLog AuditLog { get; set; }

    #endregion --------------- DECLARE LOCAL VARIABLE  ---------------
}