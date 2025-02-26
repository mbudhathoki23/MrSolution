using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.PostDateCheque;
using DatabaseModule.DataEntry.OpeningMaster;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseOrder;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseReturn;
using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesOrder;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using DatabaseModule.DataEntry.StockTransaction.ProductScheme;
using DatabaseModule.DataEntry.StockTransaction.StockAdjustment;
using DatabaseModule.Master.FinanceSetup;
using DatabaseModule.Master.InventorySetup;
using DatabaseModule.Master.LedgerSetup;
using DatabaseModule.Master.ProductSetup;
using DatabaseModule.Setup.DocumentNumberings;
using DatabaseModule.Setup.LogSetting;
using DatabaseModule.Setup.SoftwareRegistration;
using DatabaseModule.Setup.TermSetup;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Repositories;
using System;
using DatabaseModule.Setup.CompanyMaster;

namespace MrDAL.Domains.Shared.DataSync.Factories;
public abstract class DataSyncProviderFactory
{
    public static IDataSyncRepository<T> GetRepository<T>(DbSyncRepoInjectData injectData)
    {
        if (typeof(T) == typeof(ProductDataSync))
        {
            return (IDataSyncRepository<T>)new ProductsSyncRepository(injectData);
        }

        if (typeof(T) == typeof(AccountLedgerDataSync))
        {
            return (IDataSyncRepository<T>)new AccountLedgerSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SalesInvoiceDataSync))
        {
            return (IDataSyncRepository<T>)new SalesSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SO_Master))
        {
            return (IDataSyncRepository<T>)new SalesOrderSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SalesReturnData))
        {
            return (IDataSyncRepository<T>)new SalesReturnSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PurchaseInvoiceDataSync))
        {
            return (IDataSyncRepository<T>)new PurchaseSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PurchaseReturnDataSync))
        {
            return (IDataSyncRepository<T>)new PurchaseReturnSyncRepository(injectData);
        }

        if (typeof(T) == typeof(AccountGroup))
        {
            return (IDataSyncRepository<T>)new AccountGroupSyncRepository(injectData);
        }

        if (typeof(T) == typeof(AccountSubGroup))
        {
            return (IDataSyncRepository<T>)new AccountSubGroupSyncRepository(injectData);
        }

        if (typeof(T) == typeof(GeneralLedger))
        {
            return (IDataSyncRepository<T>)new GeneralLedgerSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SubLedger))
        {
            return (IDataSyncRepository<T>)new SubLedgerSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SyncLogModel))
        {
            return (IDataSyncRepository<T>)new SyncLogRepository(injectData);
        }

        if (typeof(T) == typeof(SyncLogDetailModel))
        {
            return (IDataSyncRepository<T>)new SyncLogDetailRepository(injectData);
        }

        if (typeof(T) == typeof(LOGIN_LOG))
        {
            return (IDataSyncRepository<T>)new LoginLogSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SoftwareRegistration))
        {
            return (IDataSyncRepository<T>)new SoftwareRegistrationSyncRepository(injectData);
        }

        if (typeof(T) == typeof(LicenseInfo))
        {
            return (IDataSyncRepository<T>)new LicenseInfoSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Branch))
        {
            return (IDataSyncRepository<T>)new BranchSyncRepository(injectData);
        }

        if (typeof(T) == typeof(CompanyUnit))
        {
            return (IDataSyncRepository<T>)new CompanyUnitSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Department))
        {
            return (IDataSyncRepository<T>)new DepartmentSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Currency))
        {
            return (IDataSyncRepository<T>)new CurrencySyncRepository(injectData);
        }

        if (typeof(T) == typeof(MainAgent))
        {
            return (IDataSyncRepository<T>)new SeniorAgentSyncRepository(injectData);
        }

        if (typeof(T) == typeof(JuniorAgent))
        {
            return (IDataSyncRepository<T>)new JuniorAgentSyncRepository(injectData);
        }

        if (typeof(T) == typeof(MainArea))
        {
            return (IDataSyncRepository<T>)new MainAreaSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Area))
        {
            return (IDataSyncRepository<T>)new AreaSyncRepository(injectData);
        }

        if (typeof(T) == typeof(MemberType))
        {
            return (IDataSyncRepository<T>)new MemberTypeSyncRepository(injectData);
        }

        if (typeof(T) == typeof(MemberShipSetup))
        {
            return (IDataSyncRepository<T>)new MemberShipSetupSyncRepository(injectData);
        }

        if (typeof(T) == typeof(GiftVoucherList))
        {
            return (IDataSyncRepository<T>)new GiftVoucherListSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Counter))
        {
            return (IDataSyncRepository<T>)new CounterSyncRepository(injectData);
        }

        if (typeof(T) == typeof(FloorSetup))
        {
            return (IDataSyncRepository<T>)new FloorSyncRepository(injectData);
        }

        if (typeof(T) == typeof(TableMaster))
        {
            return (IDataSyncRepository<T>)new TableMasterSyncRepository(injectData);
        }

        if (typeof(T) == typeof(CostCenter))
        {
            return (IDataSyncRepository<T>)new CostCenterSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Godown))
        {
            return (IDataSyncRepository<T>)new GodownSyncRepository(injectData);
        }

        if (typeof(T) == typeof(RACK))
        {
            return (IDataSyncRepository<T>)new RackSyncRepository(injectData);
        }

        if (typeof(T) == typeof(NR_Master))
        {
            return (IDataSyncRepository<T>)new NrMasterSyncRepository(injectData);
        }

        if (typeof(T) == typeof(ProductGroup))
        {
            return (IDataSyncRepository<T>)new ProductGroupSyncRepository(injectData);
        }

        if (typeof(T) == typeof(ProductSubGroup))
        {
            return (IDataSyncRepository<T>)new ProductSubGroupSyncRepository(injectData);
        }

        if (typeof(T) == typeof(ProductUnit))
        {
            return (IDataSyncRepository<T>)new ProductUnitSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Product))
        {
            return (IDataSyncRepository<T>)new ProductSyncRepository(injectData);
        }

        if (typeof(T) == typeof(Scheme_Master))
        {
            return (IDataSyncRepository<T>)new ProductSchemeSyncRepository(injectData);
        }

        if (typeof(T) == typeof(LedgerOpening))
        {
            return (IDataSyncRepository<T>)new LedgerOpeningSyncRepository(injectData);
        }

        if (typeof(T) == typeof(ProductOpening))
        {
            return (IDataSyncRepository<T>)new ProductOpeningSyncRepository(injectData);
        }

        if (typeof(T) == typeof(ProductAddInfo))
        {
            return (IDataSyncRepository<T>)new ProductAddInfoSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PO_Master))
        {
            return (IDataSyncRepository<T>)new PurchaseOrderSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PC_Master))
        {
            return (IDataSyncRepository<T>)new PurchaseChallanSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PCR_Master))
        {
            return (IDataSyncRepository<T>)new PurchaseChallanReturnSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PB_Master))
        {
            return (IDataSyncRepository<T>)new PurchaseInvoiceSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PR_Master))
        {
            return (IDataSyncRepository<T>)new PurchaseReturnSyncNewRepository(injectData);
        }

        if (typeof(T) == typeof(SC_Master))
        {
            return (IDataSyncRepository<T>)new SalesChallanSyncRepository(injectData);
        }

        if (typeof(T) == typeof(SB_Master))
        {
            return (IDataSyncRepository<T>)new SalesSyncNewRepository(injectData);
        }

        if (typeof(T) == typeof(SR_Master))
        {
            return (IDataSyncRepository<T>)new SalesReturnSyncNewRepository(injectData);
        }

        if (typeof(T) == typeof(STA_Master))
        {
            return (IDataSyncRepository<T>)new StockAdjustmentSyncRepository(injectData);
        }

        if (typeof(T) == typeof(PostDateCheque))
        {
            return (IDataSyncRepository<T>)new PostDateChequeSyncRepository(injectData);
        }

        if (typeof(T) == typeof(JV_Master))
        {
            return (IDataSyncRepository<T>)new JournalVoucherSyncRepository(injectData);
        }

        if (typeof(T) == typeof(CB_Master))
        {
            return (IDataSyncRepository<T>)new CashBankVoucherSyncRepository(injectData);
        }
        if (typeof(T) == typeof(ST_Term))
        {
            return (IDataSyncRepository<T>)new SalesBillingTermSyncRepository(injectData);
        }
        if (typeof(T) == typeof(PT_Term))
        {
            return (IDataSyncRepository<T>)new PurchaseBillingTermSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DocumentNumbering))
        {
            return (IDataSyncRepository<T>)new DocumentNumberingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(FiscalYear))
        {
            return (IDataSyncRepository<T>)new FiscalYearSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.SystemSetting))
        {
            return (IDataSyncRepository<T>)new SystemSettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.FinanceSetting))
        {
            return (IDataSyncRepository<T>)new FinanceSettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.InventorySetting))
        {
            return (IDataSyncRepository<T>)new InventorySettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.PaymentSetting))
        {
            return (IDataSyncRepository<T>)new PaymentSettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.PurchaseSetting))
        {
            return (IDataSyncRepository<T>)new PurchaseSettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.SalesSetting))
        {
            return (IDataSyncRepository<T>)new SalesSettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(DatabaseModule.Setup.SystemSetting.IncomeTaxSetting))
        {
            return (IDataSyncRepository<T>)new IncomeTaxSettingSyncRepository(injectData);
        }
        if (typeof(T) == typeof(StockDetail))
        {
            return (IDataSyncRepository<T>)new StockDetailSyncRepository(injectData);
        }
        throw new ArgumentOutOfRangeException(nameof(T), @"The initialization of target type has not been implemented in factory class.");
    }
}