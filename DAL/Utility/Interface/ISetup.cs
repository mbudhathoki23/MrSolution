using DatabaseModule.Setup.IrdConfig;
using DatabaseModule.Setup.PrintSetting;
using DatabaseModule.Setup.SystemSetting;
using System.Data;

namespace MrDAL.Utility.Interface;

public interface ISetup
{
    // OBJECT FOR THIS CLASS
    DocumentDesignPrint VmDocument { get; set; }

    PurchaseSetting VmPurchase { get; set; }
    SalesSetting VmSales { get; set; }
    FinanceSetting VmFinance { get; set; }
    DatabaseModule.Setup.SystemSetting.SystemSetting VmSystem { get; set; }
    InventorySetting VmStock { get; set; }
    PaymentSetting VmPayment { get; set; }

    IRDAPISetting VmIrd { get; set; }

    // SAVE SYSTEM CONFIG
    int SaveDocumentDesignPrint();

    int SaveIrdApiSetting();

    int GenerateSqlAuditLog(string location);


    // RETURN VALUE IN INT
    int GetReturnMaxValue(string table);

    // RETURN VALUE IN DATA TABLE
    DataTable GetSystemSetting();

    DataTable GetIrdApiSetting();

    DataTable GetPurchaseSetting();

    DataTable GetSalesSetting();

    DataTable GetInventorySetting();

    DataTable GetFinanceSetting();

    DataTable GetPaymentSetting();
}