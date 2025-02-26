using MrBLL.DataEntry.FinanceMaster;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.DataEntry.SalesMaster;
using MrBLL.DataEntry.StockMaster;
using MrBLL.Domains.DynamicReport;
using MrBLL.Domains.POS.Entry;
using MrBLL.Domains.Restro.Entry;
using MrBLL.Master.DocumentNumbering;
using MrBLL.Master.TermSetup;
using MrBLL.Reports.Finance_Report.Analysis;
using MrBLL.Reports.Finance_Report.DayBook;
using MrBLL.Reports.Finance_Report.FinalReport;
using MrBLL.Reports.Inventory_Report.Analysis;
using MrBLL.Reports.Inventory_Report.StockLedger;
using MrBLL.Reports.Reconcile.BankReconcile;
using MrBLL.Reports.Register_Report.Analysis_Report.Purchase;
using MrBLL.Reports.Register_Report.Analysis_Report.Sales;
using MrBLL.Reports.Register_Report.OutStanding_Report;
using MrBLL.Reports.Register_Report.Purchase_Register;
using MrBLL.Reports.Register_Report.Register_Report;
using MrBLL.Reports.Register_Report.Sales_Register;
using MrBLL.Reports.Register_Report.Vat_Report;
using MrBLL.Setup.BranchSetup;
using MrBLL.Setup.BusinessUnit;
using MrBLL.Setup.CompanySetup;
using MrBLL.Setup.UserSetup;
using MrBLL.SystemSetting;
using MrBLL.SystemSetting.PrintSetting;
using MrBLL.Utility.Common;
using MrBLL.Utility.Common.Class;
using MrBLL.Utility.CrystalReports;
using MrBLL.Utility.Database;
using MrBLL.Utility.ImportUtility;
using MrBLL.Utility.MIS;
using MrBLL.Utility.Restore;
using MrBLL.Utility.ServerConnection;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.Domains.Shared.UserAccessControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Utility.Analytics;
using MrDAL.Utility.Config;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FrmCopyMaster = MrBLL.Master.Import.FrmCopyMaster;

namespace SiyZo
{
    public partial class MdiSiyZo : MrForm
    {
        // SIYZO

        #region --------------- SIYZO ---------------

        [Obsolete]
        public MdiSiyZo()
        {
            _masterSetup = new ClsMasterSetup();
            _recalculate = new ClsRecalculate();
            InitializeComponent();
        }

        [Obsolete]
        private void MDISiyzo_Load(object sender, EventArgs e)
        {
            var result = new FrmLogin();
            result.ShowDialog();
            if (result.DialogResult != DialogResult.OK)
            {
                return;
            }
            CompanyLogin();
            BindFooterDetails();
        }

        private void MnuCompanySetup_Click(object sender, System.EventArgs e)
        {
            var valueTuple = GetMasterList.CreateCompanySetup();
        }

        private void MnuBranchSetup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateBranch();
        }

        private void MnuCompanyUnitSetup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateCompanyUnit();
        }

        private void MnuUpdateCompany_Click(object sender, EventArgs e)
        {
        }

        private void MnuBackupCompany_Click(object sender, EventArgs e)
        {
        }

        [Obsolete]
        private void MnuBranchList_Click(object sender, EventArgs e)
        {
            var result = new FrmBranchList();
            result.ShowDialog();
            if (result.DialogResult is DialogResult.OK)
            {
                BindFooterDetails();
            }
        }

        [Obsolete]
        private void MnuCompanyList_Click(object sender, EventArgs e)
        {
            var result = new FrmCompanyList(false);
            result.ShowDialog();
            if (result.DialogResult is DialogResult.OK)
            {
                BindFooterDetails();
            }
        }

        private void MnuCompanyUnitList_Click(object sender, EventArgs e)
        {
            var result = new FrmCompanyUnitList();
            result.ShowDialog();
        }

        private void MnuFiscalYear_Click(object sender, EventArgs e)
        {
            var result = new FrmFiscalYear();
            result.ShowDialog();
        }

        private void MnuUserGroup_Click(object sender, EventArgs e)
        {
            var result = new FrmUserRoleSetup();
            result.ShowDialog();
        }

        private void MnuUserCreate_Click(object sender, EventArgs e)
        {
            var result = new FrmUserSetup();
            result.ShowDialog();
        }

        private void MnuUserControl_Click(object sender, EventArgs e)
        {
            var result = new FrmUserConfig();
            result.ShowDialog();
        }

        private void MnuSecurityRights_Click(object sender, EventArgs e)
        {
        }

        private void MnuCompanyRights_Click(object sender, EventArgs e)
        {
            var result = new FrmCompanyRights();
            result.ShowDialog();
        }

        private void MnuBranchRights_Click(object sender, EventArgs e)
        {
            var result = new FrmBranchRights();
            result.ShowDialog();
        }

        private void MnuCompanyUnitRights_Click(object sender, EventArgs e)
        {
            var result = new FrmCompanyUnitRights();
            result.ShowDialog();
        }

        private void MnuChangePassword_Click(object sender, EventArgs e)
        {
            var result = new FrmChangePassword();
            result.ShowDialog();
        }

        private void MnuAddCrystalDesign_Click(object sender, EventArgs e)
        {
            var result = new FrmPrintDesignAdd();
            result.ShowDialog();
        }

        private void MnuDesignSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmPrintDesignSetting();
            result.ShowDialog();
        }

        [Obsolete]
        private void MnuLogOut_Click(object sender, EventArgs e)
        {
            UserTerminated();
            CleanLoginInformation();
            var result = new FrmLogin();
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {
                BindFooterDetails();
                var service = new AnalyticsService();
                service.CleanUpAsync();
            }
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MnuAccountGroup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateAccountGroup();
        }

        private void MnuAccountSubGroup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateAccountSubGroup();
        }

        private void MnuGeneralLedger_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateGeneralLedger();
        }

        private void MnuSubLedger_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateSubLedger();
        }

        private void MnuLedgerImport_Click(object sender, EventArgs e)
        {
        }

        private void MnuSeniorAgent_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateMainAgent();
        }

        private void MnuSubAgent_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateAgent();
        }

        private void MnuMainArea_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateMainArea();
        }

        private void MnuSubArea_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateArea();
        }

        private void MnuMemberType_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateMemberShip();
        }

        private void MnuMemberShipType_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateMemberType();
        }

        private void MnuDepartment_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateDepartment();
        }

        private void MnuClass_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateDepartment();
        }

        private void MnuSection_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateDepartment();
        }

        private void MnuTerminalSetup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateCounter();
        }

        private void MnuCurrencyMaster_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateCurrency();
        }

        private void MnuCostCenterMaster_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateCostCenter();
        }

        private void MnuProductGroup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateProductGroup();
        }

        private void MnuProductSubGroup_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateProductSubGroup();
        }

        private void MnuProductUnit_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateProductUnit();
        }

        private void MnuProduct_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateProduct();
        }

        private void MnuProductScheme_Click(object sender, EventArgs e)
        {
        }

        private void MnuProductMapping_Click(object sender, EventArgs e)
        {
        }

        private void MnuProductLock_Click(object sender, EventArgs e)
        {
        }

        private void MnuProductUpdate_Click(object sender, EventArgs e)
        {
        }

        private void MnuBarcodePrint_Click(object sender, EventArgs e)
        {
        }

        private void MnuProductAssemble_Click(object sender, EventArgs e)
        {
        }

        private void MnuProductImport_Click(object sender, EventArgs e)
        {
        }

        private void MnuGodownMaster_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateGodown();
        }

        private void MnuNarration_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateNarration();
        }

        private void MnuRemarks_Click(object sender, EventArgs e)
        {
            var valueTuple = GetMasterList.CreateNarration();
        }

        private void MnuCopyMaster_Click(object sender, EventArgs e)
        {
            var result = new FrmCopyMaster();
            result.ShowDialog();
        }

        private void MnuLedgerOpeningImport_Click(object sender, EventArgs e)
        {
            //var result = new FrmOpeningLedgerImport();
            //result.ShowDialog();
        }

        private void MnuProductOpeningImport_Click(object sender, EventArgs e)
        {
            //var result = new FrmOpeningProductImport();
            //result.ShowDialog();
        }

        private void MnuPurchaseTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseBillingTerm("PB");
            result.ShowDialog();
        }

        private void MnuSalesTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesBillingTerm("SB");
            result.ShowDialog();
        }

        private void MnuPurchaseReturnTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseBillingTerm("PR");
            result.ShowDialog();
        }

        private void MnuSalesReturnTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesBillingTerm("SR");
            result.ShowDialog();
        }

        private void MnuDocumentNumbering_Click(object sender, EventArgs e)
        {
            var result = new FrmDocumentNumbering();
            result.ShowDialog();
        }

        private void MnuSalesQuotationEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesQuotationEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesQuotationRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SQ")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesQuotationOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingSalesOrder()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesOrderEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesOrderEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesOrderRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SO")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesOrderOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingSalesOrder()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesChallanEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesChallanEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesChallanRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SC")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesChallanOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingSalesChallan()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingPartyLedger("CUSTOMER")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceAnalysis_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesAnalysis()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceVatRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesVatRegister("SB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceTopTenCustomer_Click(object sender, EventArgs e)
        {
        }

        private void MnuSalesInvoiceTopTenProduct_Click(object sender, EventArgs e)
        {
        }

        private void MnuSalesReturnEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesReturnEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesReturnRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SR")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesReturnVatRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesVatRegister("SR")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuSalesAdditionalEntry_Click(object sender, EventArgs e)
        {
        }

        private void MnuSalesAdditionalRegister_Click(object sender, EventArgs e)
        {
        }

        private void MnuSalesExpiryBreakageEntry_Click(object sender, EventArgs e)
        {
        }

        private void MnuSalesExpiryBreakageRegister_Click(object sender, EventArgs e)
        {
        }

        private void MnuPointOfSalesInvoice_Click(object sender, EventArgs e)
        {
            var result = new FrmPSalesInvoice();
            result.ShowDialog();
        }

        private void MnuSalesRestaurantManagementSystem_Click(object sender, EventArgs e)
        {
            var result = new FrmTablesList();
            result.ShowDialog();
        }

        [Obsolete]
        private void MnuSalesMisReports_Click(object sender, EventArgs e)
        {
            var result = new FrmDynamicRegisterReports();
            result.ShowDialog();
        }

        private void MnuPurchaseIndentEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseIndentEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseIndentRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseRegister("PIN")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseIndentOutstanding_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseRegister("PIN")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseOrderEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseOrderEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseOrderRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseRegister("PO")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseOrderOutstanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingPurchaseOrder()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseChallanEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseChallanEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseChallanReports_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseRegister("PC")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseChallanOutstanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingPurchaseChallan()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseInvoiceEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseInvoiceEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseInvoiceRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseRegister("PB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseInvoiceVatRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseVatRegister("PB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseInvoiceOutStandingReports_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingPartyLedger("VENDOR")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseInvoiceAnalysisReports_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseAnalysis()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseInvoiceTopNProduct_Click(object sender, EventArgs e)
        {
        }

        private void MnuPurchaseInvoiceTopNVendor_Click(object sender, EventArgs e)
        {
        }

        private void MnuPurchaseReturnEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseReturnEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseReturnRegisterReports_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseRegister("PR")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseReturnVatRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseVatRegister("PR")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseAdditionalEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseAdditional(true, "", "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseAdditionalRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseAdditionalRegister()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseAdditionalProductCosting_Click(object sender, EventArgs e)
        {
            var result = new FrmProductCosting()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPurchaseExpiryBreakageEntry_Click(object sender, EventArgs e)
        {
        }

        private void MnuPurchaseExpiryBreakageRegister_Click(object sender, EventArgs e)
        {
        }

        [Obsolete]
        private void MnuPurchaseMisReports_Click(object sender, EventArgs e)
        {
            var result = new FrmDynamicRegisterReports();
            result.ShowDialog();
        }

        private void MnuCashBankVoucherEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmCashBankEntry()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuCashBankVoucherReports_Click(object sender, EventArgs e)
        {
            var result = new FrmCashBook()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuBankReconcileReports_Click(object sender, EventArgs e)
        {
            var result = new FrmBankReconciliationStatement()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuCashBankVoucherSummaryReports_Click(object sender, EventArgs e)
        {
            var result = new FrmCashBook()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuJournalVoucherEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmJournalVoucherEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuJournalVoucherBook_Click(object sender, EventArgs e)
        {
            var result = new FrmJournalBook()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuDebitNotesEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmVoucherNotesEntry()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuCreditNotesEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmVoucherNotesEntry(false, "", "CN")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuDebitNotesRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmDebitNoteRegister()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuCreditNotesRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmDebitNoteRegister()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPostDatedChequeEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPDCVoucher(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuMultiCurrencyCashBankVoucherEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmCashBankEntry(false, "", "CB", false, true, true)
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuMultiCurrencyJournalVoucherEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmJournalVoucherEntry(false, "", false, true)
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuCashFlowReports_Click(object sender, EventArgs e)
        {
            var result = new FrmCashFlowStatement()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuGeneralLedgerReports_Click(object sender, EventArgs e)
        {
            var result = new FrmLedger("ALL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuOpeningTrialBalanceReport_Click(object sender, EventArgs e)
        {
            var result = new FrmTrialBalance("OB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuTrialBalanceNormalFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmTrialBalance("TB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuTrialBalancePeriodicFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmTrialBalance("PTB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuTrialBalanceTFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmTrialBalance("TB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuTradingAccountNormalFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmProfitNLoss("PL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuTradingAccountPeriodicFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmProfitNLoss("PPL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuTradingAccountTFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmProfitNLoss("PL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuProfitAndLossNormalFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmProfitNLoss("PL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuProfitAndLossPeriodicFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmProfitNLoss("PPL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuProfitAndLossTFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmProfitNLoss("PL")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuBalanceSheetOpeningBalance_Click(object sender, EventArgs e)
        {
            var result = new FrmBalanceSheet("OB")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuBalanceSheetNormalFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmBalanceSheet("BS")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuBalanceSheetPeriodicFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmBalanceSheet("PBS")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuBalanceSheetTFormat_Click(object sender, EventArgs e)
        {
            var result = new FrmBalanceSheet("BS")
            {
                MdiParent = this
            };
            result.Show();
        }

        [Obsolete]
        private void MnuFinanceMisReports_Click(object sender, EventArgs e)
        {
            var result = new FrmDynamicFinanceReports();
            result.ShowDialog();
        }

        private void MnuSystemSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmSystemSettings();
            result.ShowDialog();
        }

        private void MnuFinanceSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmFinanceSetting();
            result.ShowDialog();
        }

        private void MnuPurchaseSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseSetting();
            result.ShowDialog();
        }

        private void MnuSalesSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesSetting();
            result.ShowDialog();
        }

        private void MnuInventorySetting_Click(object sender, EventArgs e)
        {
            var result = new FrmInventorySetting();
            result.ShowDialog();
        }

        private void MnuPaymentSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmPaymentSetting();
            result.ShowDialog();
        }

        private void MnuIrdApiConfig_Click(object sender, EventArgs e)
        {
            var result = new FrmIrdApiConfig();
            result.ShowDialog();
        }

        private void MnuServerConfig_Click(object sender, EventArgs e)
        {
            var result = new FrmMultiServer(true);
            result.ShowDialog();
        }

        private void MnuBackUp_Click(object sender, EventArgs e)
        {
            var result = new FrmBackUpDataBase("BackUp");
            result.ShowDialog();
        }

        private void MnuRestore_Click(object sender, EventArgs e)
        {
            var result = new FrmRestore();
            result.ShowDialog();
        }

        private void MnuRecalculate_Click(object sender, EventArgs e)
        {
            var result = new XFrmRecalculate();
            result.ShowDialog();
        }

        [Obsolete]
        private void MnuExternalDeviceUtility_Click(object sender, EventArgs e)
        {
            var result = new FrmAttachDeAttach();
            result.ShowDialog();
        }

        private void MnuSMSConfig_Click(object sender, EventArgs e)
        {
            var result = new FrmSMSConfig();
            result.ShowDialog();
        }

        private void MnuLockFiscalYear_Click(object sender, EventArgs e)
        {
        }

        private void MnuImportData_Click(object sender, EventArgs e)
        {
        }

        private void MnuLocalImport_Click(object sender, EventArgs e)
        {
            var result = new FrmImportData("LOCAL");
            result.ShowDialog();
        }

        private void MnuSecondaryServer_Click(object sender, EventArgs e)
        {
        }

        private void MnuOnlineSyncData_Click(object sender, EventArgs e)
        {
        }

        private void MnuLastYearClosing_Click(object sender, EventArgs e)
        {
        }

        private void MnuOnlineDataSync_Click(object sender, EventArgs e)
        {
        }

        private void MnuShrinkDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                //SplashScreenManager.ShowForm(typeof(FrmWait));
                _recalculate.ShrinkDatabase(ObjGlobal.InitialCatalog);
                _recalculate.ShrinkDatabaseLog(ObjGlobal.InitialCatalog);
                MessageBox.Show($@"{ObjGlobal.InitialCatalog} SHRINK DATABASE SUCCESSFULLY..!!", ObjGlobal.Caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                // SplashScreenManager.CloseForm(false);
            }
            catch (Exception ex)
            {

                ex.ToNonQueryErrorResult(ex);
            }
        }

        private void MnuDatabaseReset_Click(object sender, EventArgs e)
        {
            var result = new FrmDataBaseReset();
            result.ShowDialog();
        }

        private void MnuReconcileData_Click(object sender, EventArgs e)
        {
            var result = new FrmDataReconciliation();
            result.ShowDialog();
        }

        private void MnuStockAdjustmentEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmStockAdjustment(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuStockAdjustmentRegister_Click(object sender, EventArgs e)
        {
            //var result = new FrmStockAdjustment(false, "")
            //{
            //    MdiParent = this
            //};
            //result.Show();
        }

        private void MnuGodownTransferEntry_Click(object sender, EventArgs e)
        {
            //var result = new FrmGodown(false, "")
            //{
            //    MdiParent = this
            //};
            //result.Show();
        }

        private void MnuGodownTransferRegister_Click(object sender, EventArgs e)
        {
        }

        private void MnuPhysicalStockEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmPhysicalStockEntry(false, "")
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuPhysicalStockReports_Click(object sender, EventArgs e)
        {
        }

        private void MnuStockLedgerReports_Click(object sender, EventArgs e)
        {
            var result = new FrmStockLedger(false)
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuStockLedgerWithValue_Click(object sender, EventArgs e)
        {
            var result = new FrmStockLedger(true)
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuClosingStockValue_Click(object sender, EventArgs e)
        {
            var result = new FrmStockValuation()
            {
                MdiParent = this
            };
            result.Show();
        }

        private void MnuHelpFiles_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.CurrentDirectory + @"\Help\MrSolutionUserMannual.chm"))
            {
                Process.Start(Environment.CurrentDirectory + @"\Help\MrSolutionUserMannual.chm");
            }
        }

        #endregion --------------- SIYZO ---------------

        // METHOD FOR THIS FORM

        #region ---------- METHOD FOR THIS FORM ----------

        [Obsolete]
        private void BindFooterDetails()
        {
            HearderMenuList.Visible = true;
            TsInitial.Text = !string.IsNullOrEmpty(TsInitial.Text) ? string.Empty : TsInitial.Text;
            TsLogInUser.Text = !string.IsNullOrEmpty(TsLogInUser.Text) ? string.Empty : TsLogInUser.Text;
            TsBranchInfo.Text = !string.IsNullOrEmpty(TsBranchInfo.Text) ? string.Empty : TsBranchInfo.Text;
            var isOnline = "SELECT sr.IsOnline FROM Master.AMS.SoftwareRegistration sr".GetQueryData();
            GetConnection.LoginInitialCatalog = ObjGlobal.InitialCatalog;
            if (isOnline.GetBool() && ObjGlobal.LogInUser.ToUpper() != "SIYZO")
            {
                OnlineMenu();
            }
            ObjGlobal.GetFiscalYearDetails();
            TsInitial.Text = ObjGlobal.InitialCatalog;
            TsLogInUser.Text = $@"USER_INFO :- {ObjGlobal.LogInUser.ToUpper()}";
            TsCompanyInfo.Text = $@"COMPANY :- {ObjGlobal.LogInCompany?.ToUpper()}";
            TsStartDate.Text = ObjGlobal.SysDateType is "M" or null ? $"FROM_DATE :-  {ObjGlobal.CfStartBsDate}" : $"FROM_DATE :-  {ObjGlobal.CfStartAdDate.GetDateString()}";
            TsBranchInfo.Text = $@" BRANCH :- {ObjGlobal.SysBranchName}";
            TsFiscalYears.Text = $@" FISCAL_YEAR :- {(ObjGlobal.SysDateType is "M" ? ObjGlobal.SysBsFiscalYear : ObjGlobal.SysFiscalYear)}";
            TsWebSites.Text = @"www.siyzo.com";
            TsCopyRights.Text = $@"CopyRights Reserve : SiyZo Solution Pvt. Ltd © {DateTime.Now.Year} ";
            Text = @"SiyZo | Account & Inventory Management System";
            ObjGlobal.SoftwareModule = GetConnection.GetQueryData("Select SoftModule from AMS.CompanyInfo");
            tsModule.Text = $@"MODULE :-{ObjGlobal.SoftwareModule.ToUpper()}";
            MenuRights();
            ProjectFocusMenuVisible();
            SaveLoginAuditInDatabase();
            if (ObjGlobal.LogInUserCategory is "TERMINAL")
            {
                var sales = new FrmPSalesInvoice
                {
                    MdiParent = this,
                    WindowState = FormWindowState.Normal,
                    Dock = DockStyle.Fill
                };
                sales.Show();
            }
            tsRegistration.Text = ObjGlobal.IsLicenseExpire ? $"REGISTRATION INFO : YOUR LICENSE HAS BEEN EXPIRED [{DateTime.Now.GetDateString()}]" : @"REGISTRATION INFO : YOUR LICENSE HAS BEEN REGISTER";
            tmrLicenses.Enabled = true;
            if (!string.IsNullOrEmpty(ObjGlobal.SoftwareModule))
            {
                return;
            }
            var frm = new FrmSoftwareModule();
            frm.ShowDialog();
            tsModule.Text = $@"MODULE :-[{frm.SoftwareModule.ToUpper()}]";
        }

        private void OnlineMenu()
        {
            HMnDataManage.Visible = MnuUserMaster.Visible = MnuCompanyMaster.Visible = false;
        }

        [Obsolete]
        private void SaveLoginAuditInDatabase()
        {
            try
            {
                _loginId = ClsMasterSetup.ReturnMaxIntValue("MASTER.AMS.LOGIN_LOG", "OBJECT_ID").GetInt();
                //_masterSetup.LoginLog.OBJECT_ID = _loginId;
                //_masterSetup.LoginLog.LOGIN_USER = ObjGlobal.LogInUser;
                //_masterSetup.LoginLog.COMPANY = ObjGlobal.LogInCompany?.ToUpper();
                //_masterSetup.LoginLog.LOGIN_DATABASE = ObjGlobal.InitialCatalog;
                ////_masterSetup.LoginLog.DEVICE = FrmLicenseGenerator.GetServerName();
                //_masterSetup.LoginLog.DAVICE_MAC = ObjGlobal.GetMacAddress();
                ////_masterSetup.LoginLog.DEVICE_IP = FrmLicenseGenerator.GetIpAddress();
                ////_masterSetup.LoginLog.SYSTEM_ID = FrmLicenseGenerator.GetSerialNo().Result;
                //_masterSetup.LoginLog.LOG_STATUS = 1;
                //_masterSetup.SaveLoginLog("SAVE");
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult(e);
                e.DialogResult();
            }
        }

        [Obsolete]
        private void UserTerminated()
        {
            //_deviceName = FrmLicenseGenerator.GetServerName();
            //_deviceIp = FrmLicenseGenerator.GetIpAddress();
            //_masterSetup.ResetLoginInfo("SAVE");
        }

        private void ProjectFocusMenuVisible()
        {
            if (Debugger.IsAttached)
            {
            }
            else
            {
                MnuRestore.Enabled = ObjGlobal.IsIrdApproved != "YES";
                MnuDatabaseReset.Enabled = ObjGlobal.IsIrdApproved != "YES";
            }
        }

        private void CleanLoginInformation()
        {
            ObjGlobal.LogInUserId = 0;
            ObjGlobal.LogInUser = string.Empty;
            ObjGlobal.LogInUserPassword = string.Empty;
            ObjGlobal.LogInUserPostingEndDate = DateTime.Now;
            ObjGlobal.LogInUserPostingEndDate = DateTime.Now;
            ObjGlobal.LogInUserModifyStartDate = DateTime.Now;
            ObjGlobal.LogInUserModifyEndDate = DateTime.Now;
            ObjGlobal.LogInUserValidDays = 0;
            ObjGlobal.LogInUserCategory = string.Empty;
            ObjGlobal.UserAuthorized = false;
            ObjGlobal.UserAllowPosting = false;
            ObjGlobal.UserModify = false;
            ObjGlobal.UserDelete = false;
            ObjGlobal.UserPdcDashBoard = false;
            ObjGlobal.UserChangeRate = false;
            ObjGlobal.UserAuditLog = false;
            ObjGlobal.ChangeQty = false;
            ObjGlobal.UserLedgerId = 0;

            TsInitial.Text = string.Empty;
            TsLogInUser.Text = string.Empty;
            TsBranchInfo.Text = string.Empty;
            TsInitial.Text = string.Empty;
            TsLogInUser.Text = string.Empty;
            TsCompanyInfo.Text = string.Empty;
            TsStartDate.Text = string.Empty;
            TsEndDate.Text = string.Empty;
            TsBranchInfo.Text = string.Empty;
            TsFiscalYears.Text = string.Empty;
            TsWebSites.Text = string.Empty;
            TsCopyRights.Text = string.Empty;
            tsModule.Text = string.Empty;
        }

        public async void MenuRights()
        {
            if (ObjGlobal.LogInUser.GetUpper().Equals("SIYZO"))
            {
                return;
            }
            var setPermResponse = await UacManager.SetPermissionValues(ObjGlobal.LogInUser);
            if (!setPermResponse.Value)
            {
                //setPermResponse.ShowErrorDialog();
            }
            //MnuPointOfSalesInvoiceEntry.Visible = UacManager.IsPermissionAllowed(UacAccessFeature.TaxInvoice, UacAction.Create, ObjGlobal.SysBranchId);
            //dayClosingToolStripMenuItem.Visible = UacManager.IsPermissionAllowed(UacAccessFeature.DayClosing, UacAction.Create, ObjGlobal.SysBranchId);
        }

        private void BranchLogin()
        {
            try
            {
                var dtBranch = _masterSetup.LoginBranchDataTable(ObjGlobal.LogInUser.ToUpper() is "ADMIN" || ObjGlobal.LogInUser.ToUpper() is "AMSADMIN");
                if (dtBranch?.Rows.Count > 1)
                {
                    var branchListForm = new FrmBranchList();
                    branchListForm.ShowDialog();
                    if (branchListForm.DialogResult == DialogResult.OK)
                    {
                        var dt = GetConnection.SelectDataTableQuery(@"SELECT * FROM AMS.CompanyUnit ");
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return;
                        }
                        var frm1 = new FrmCompanyUnitList();
                        frm1.ShowDialog();
                    }
                }
                else
                {
                    if (dtBranch?.Rows.Count is 1)
                    {
                        ObjGlobal.SysBranchId = dtBranch.Rows[0]["Branch_Id"].GetInt();
                        ObjGlobal.SysBranchName = dtBranch.Rows[0]["Branch_Name"].ToString();
                    }
                    else if (dtBranch?.Rows is { Count: 0 })
                    {
                        const string branch = "Select * from AMS.Branch b";
                        var dtSingleBranch = GetConnection.SelectDataTableQuery(branch);
                        if (dtSingleBranch.Rows.Count > 0)
                        {
                            ObjGlobal.SysBranchId = dtSingleBranch.Rows[0]["Branch_Id"].GetInt();
                            ObjGlobal.SysBranchName = dtSingleBranch.Rows[0]["Branch_Name"].ToString();
                        }
                    }
                    const string companyUnit = "SELECT * FROM AMS.CompanyUnit ";
                    var dtCompanyUnit = GetConnection.SelectDataTableQuery(companyUnit);
                    if (dtCompanyUnit.Rows.Count > 0)
                    {
                        var result = new FrmCompanyUnitList();
                        result.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToNonQueryErrorResult(ex);
            }
        }

        [Obsolete]
        private void CompanyLogin()
        {
            const string query = @"Select * from Master.AMS.GlobalCompany ";
            var dtCompany = ExecuteCommand.ExecuteDataSetOnMaster(query).Tables[0];
            if (dtCompany.Rows.Count == 0)
            {
                var setup = new FrmCompanySetup(true);
                setup.ShowDialog();
                if (setup.DialogResult != DialogResult.OK)
                {
                    var login = new FrmLogin();
                    login.ShowDialog();
                    return;
                }
                DialogResult = setup.DialogResult;
                return;
            }
            var getList = new FrmCompanyList(false);
            getList.ShowDialog();
            var result = getList.DialogResult;
            if (result == DialogResult.OK)
            {
                BranchLogin();
            }
        }

        #endregion ---------- METHOD FOR THIS FORM ----------

        // OBJECT FOR THIS FORM

        #region --------------- OBJECT ---------------

        private int _loginId;
        private string _deviceName = string.Empty;
        private string _deviceIp = string.Empty;
        public string DbSelect = string.Empty;
        private readonly IRecalculate _recalculate;
        private readonly IMasterSetup _masterSetup;

        #endregion --------------- OBJECT ---------------
    }
}