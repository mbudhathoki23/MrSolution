using DatabaseModule.CloudSync;
using DatabaseModule.Master.InventorySetup;
using DatabaseModule.Setup.LogSetting;
using MrDAL.Models.Common;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MrDAL.Master.Interface;
public interface IMasterSetup
{
    Task<string> SaveSyncLogDetails(string actionTag);
    Task<ListResult<SyncLogModel>> GetUnSynchronizedData(string tableName);
    // RETURN VALUE OF MASTER LIST IN DATA TABLE
    #region --------------- RETURN DATATABLE VALUE ---------------
    DataTable CheckSmsConfig();
    DataTable GetPrintVoucherList(string module);
    DataTable CheckMasterValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);
    DataTable CheckIsValidData(string actionTag, string tableName, string whereValue, string validId, string inputTxt, string selectedId);
    DataTable GetLastGiftVoucherNumber();
    DataTable GetGiftVoucherNumberInformation(int selectedId);

    //RETURN VALUE OF PRODUCT IN DATA TABLE

    #region--------------- Product ---------------
    DataTable IsDuplicate(string actionTag, string module, string inputValue, string inputId);
    DataTable GetMasterProductList(string actionTag, long selectedId = 0);
    DataTable GetMasterBookList(string actionTag, long selectedId = 0);
    DataTable GetMasterUser();

    DataTable GetMasterCounter(string actionTag, long selectedId = 0);

    DataTable GetRackList(string actionTag, int status = 0, int selectedId = 0);

    DataTable GetMasterSystemTagVatTerm(string source);

    DataTable GetTermCalculationForVoucher(string module, string termType = "B");

    DataTable GetLedgerBalance(long ledgerId, MaskedTextBox mskDate);

    DataTable GetOpeningLedgerList();

    DataTable GetMasterMemberShip(string actionTag, int selectedId = 0);

    DataTable GetMasterUserInfo(string loginUser);

    DataTable GetUserAccessControl(int userRoleId, int userId, bool isCallFromSecurityRight = false);
    DataTable GetUserAccessControls(int userRoleId, int userId, bool isCallFromSecurityRight = false);

    DataTable GetMasterMemberType(string actionTag, int selectedId = 0);

    DataTable GetMasterFloor(string action, int selectedId = 0);

    DataTable GetMasterTable(string actionTag, int selectedId = 0);

    DataTable GetCompanyUnit(string actionTag, int selectedId = 0);

    DataTable IsExitsCheckDocumentNumbering(string module);

    DataTable IsExitsBarcode(string inputValue);

    DataTable CheckMemberShipValidData(string value);

    DataTable ReturnMemberShipValue(int memberId);

    DataTable LoginBranchDataTable(bool isUser, bool isActive = false);

    DataTable LoginCompanyDataTable();

    DataTable GetPartyInfo();

    DataTable GetBillingTerm();

    DataTable GetProductBatchFormat();

    DataTable GetCompanyRights(int userId);

    DataTable GetCompanyList();


    // PRODUCT RETURN IN DATA TABLE

    DataTable GetProductListWithQty();

    DataTable GetProductListFromLedger(long ledgerId);

    DataTable GetBarcodeList(int groupId);
    DataTable GetMasterCounterProductList(int selectedId = 0, bool isActive = false);
    #endregion --------------- RETURN DATATABLE VALUE ---------------


    // RESTAURANT MANAGEMENT SYSTEM MODULE

    #region **----- Restro -----**

    DataTable GetFloorList();

    #endregion **----- Restro -----**

    #endregion --------------- Account Group---------------



    // TRANSACTION MASTER
    #region--------------- TRANSATION MASTER ---------------

    DataTable GetMemberShipDiscount(string member);

    DataTable GetVehicleListWithQty(string qtyVal, string amtVal, string loginDate, bool availableStock);

    DataTable GetOpeningLedger(string category, string qtyVal, string amtVal, string loginDate);

    DataTable GetGeneralLedger(string tag, string category, string qtyVal, string amtVal, string loginDate, int selectedId = 0);

    DataTable GetSubLedger(string tag, string category, string qtyVal, string amtVal, string loginDate, int selectedId = 0);

    DataTable CheckPbdInvoiceData(string voucherNo, string module, string fiscalYear);

    DataTable GetProductInfoWithBarcode(string filterTxt);

    DataTable GetProductWithBarcode(string filterTxt);

    DataTable GetProductListBarcode(long productId);

    DataTable GetPosProductInfo(string filterTxt);

    DataTable GetBookInformation(long selectedId);

    DataTable GetOnlineSync();
    #endregion



    // RETURN VALUE OF MASTER LIST IN DATA SET
    #region --------------- RETURN VALUE IN DATA SET ---------------
    DataSet GetProductScheme(int schemeId);
    #endregion --------------- RETURN VALUE IN DATA SET ---------------



    // RETURN VALUE IN INT
    #region --------------- REUTN VALUE ---------------
    int ReturnMaxIdValue(string module);

    int UpdateProductPurchaseRate(double value, string product);
    int UpdateProductSalesRate(double value, string product);
    int ReturnIntValueFromTable(string tableName, string tableId, string tableColumn, string filterTxt);

    bool GetTaxRate();

    bool IsBillingTermExitsOrNot(string module, string type);

    bool IsAdditionalBillingTermExitsOrNot(string module, string type);

    long ReturnMaxMemberId();

    long GetUserLedgerIdFromUser(string userInfo);

    long GetProductIdFromShortName(string shortName);

    long GetLedgerIdFromDescription(string ledgerDesc);

    long GenerateLedgerAccountNumber(string groupDesc);

    long ReturnLongValueFromTable(string tableName, string getId, string filterValue, string filterTxt);

    void BindFiscalYear(ComboBox box);

    void BindProductType(ComboBox box);

    void BindPaymentType(ComboBox box);

    void BindStockValueType(ComboBox box);

    void BindProductCategory(ComboBox box);

    void BindGiftVoucherType(ComboBox cmbType);

    void BindVoucherType(ComboBox box, string module = "SB");

    string GetLedgerDescription(long ledgerId);

    string GetAccountGroupDescription(int groupId);

    string GetProductGroupDescription(int groupId);

    string GetLedgerTypeDescription(long ledgerId);

    string BindAutoIncrementCode(string tableName, string shortName, string autoIncrementCode);

    #endregion


    SyncTable ObjSync { get; set; }
    FloorSetup ObjFloor { get; set; }

}