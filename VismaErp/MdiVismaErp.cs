using DevExpress.Utils.Extensions;
using DevExpress.XtraSplashScreen;
using MrBLL.DataEntry.Common;
using MrBLL.DataEntry.FinanceMaster;
using MrBLL.DataEntry.OpeningMaster;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.DataEntry.SalesMaster;
using MrBLL.DataEntry.StockMaster;
using MrBLL.Domains.DynamicReport;
using MrBLL.Domains.POS.Entry;
using MrBLL.Domains.POS.Master;
using MrBLL.Domains.Restro.Entry;
using MrBLL.Master.Import;
using MrBLL.Master.ProductSetup;
using MrBLL.Master.TermSetup;
using MrBLL.Reports.Finance_Report.FinalReport;
using MrBLL.Reports.Reconcile.BankReconcile;
using MrBLL.Reports.Register_Report.Analysis_Report;
using MrBLL.Reports.Register_Report.Analysis_Report.Sales;
using MrBLL.Reports.Register_Report.OutStanding_Report;
using MrBLL.Reports.Register_Report.Purchase_Register;
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
using MrBLL.Utility.MrLicense;
using MrBLL.Utility.Restore;
using MrBLL.Utility.ServerConnection;
using MrBLL.Utility.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Shared.UserAccessControl;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Models.Custom;
using MrDAL.Setup.BackupRestore;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Setup.UserSetup;
using MrDAL.Utility.Analytics;
using MrDAL.Utility.Config;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using VismaErp.About;

namespace VismaErp
{
    public partial class MdiVismaErp : MrForm
    {
        // VISMA ERP | ACCOUNT & INVENTORY MANAGEMENT SYSTEM
        #region ---------- VISMA ERP | ACCOUNT & INVENTORY MANAGEMENT SYSTEM ----------
        [Obsolete]
        public MdiVismaErp()
        {
            InitializeComponent();
            _masterSetup = new ClsMasterSetup();
            _backup = new BackupRestoreRepository();
            _salesEntry = new ClsSalesEntry();
            _userSetup = new UserSetupRepository();
        }

        [Obsolete]
        private void MdiVismaErp_Load(object sender, EventArgs e)
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
        private void MdiVismaErp_KeyDown(object sender, KeyEventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                return;
            }
            if (e.KeyCode is Keys.F10)
            {
                var frm = new FrmSearchCProduct(@"StockReports");
                frm.ShowDialog(this);
            }
            else if (e.KeyCode is Keys.F11)
            {
                SearchPanel.Visible = true;
                SearchGrid.Visible = true;
                TxtSearchGrid.Focus();
            }
            else if (e.KeyCode is Keys.Escape)
            {
                if (SearchPanel.Visible)
                {
                    SearchPanel.Visible = false;
                    SearchGrid.Visible = false;
                }
            }
            else if (SearchPanel.Visible)
            {
                if (e.KeyCode is Keys.Up or Keys.Down)
                {
                    SearchGrid.Focus();
                }
                else
                {
                    TxtSearchGrid.Focus();
                }
            }
        }
        private void TsWebSites_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://mrsolution.com.np/");
        }
        private void MnuCompanySetup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateCompanySetup();
        }
        private void MnuBranchSetup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateBranch();
        }

        private void MnuCompanyUnitSetup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateCompanyUnit();
        }

        private void MnuUpdateCompany_Click(object sender, EventArgs e)
        {
            try
            {
                SplashScreenManager.ShowForm(typeof(FrmWait));
                CompanySetupRepository.UpdateDatabase();
                if (ObjGlobal.IsIrdRegister)
                {
                    CreateDatabaseTable.CreateTrigger();
                }
                SplashScreenManager.CloseForm(false);
                MessageBox.Show(@"DATABASE UPDATE SUCCESSFULLY..!!", ObjGlobal.Caption);
            }
            catch (Exception exception)
            {
                SplashScreenManager.CloseForm(false);
                Console.WriteLine(exception);
                MessageBox.Show(exception.HelpLink);
            }
        }

        private void MnuBackupCompany_Click(object sender, EventArgs e)
        {
            var result = new FrmBackUpDataBase(@"BackUp");
            result.ShowDialog();
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
            var result = new FrmUserRight();
            result.ShowDialog();
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
            var result = new FrmChangePasswords();
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
            if (CustomMessageBox.ExitActiveForm("DO YOU WANT TO EXITS APPLICATION..??") == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void MnuAccountGroup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateAccountGroup();
        }

        private void MnuAccountSubGroup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateAccountSubGroup();
        }

        private void MnuGeneralLedger_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateGeneralLedger();
        }

        private void MnuSubLedger_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateSubLedger();
        }

        private void MnuLedgerImport_Click(object sender, EventArgs e)
        {
            var result = new FrmLedgerImportFromExcel
            {
                Owner = this
            };
            result.ShowDialog();
        }

        private void MnuSeniorAgent_Click(object sender, EventArgs e)
        {
            _ = GetMasterList.CreateMainAgent();
        }

        private void MnuSubAgent_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateAgent();
        }

        private void MnuMainArea_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateMainArea();
        }

        private void MnuSubArea_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateArea();
        }

        private void MnuMemberType_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateMemberShip();
        }

        private void MnuMemberShipType_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateMemberType();
        }

        private void MnuDepartment_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateDepartment();
        }

        private void MnuClass_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateDepartment();
        }

        private void MnuSection_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateDepartment();
        }

        private void MnuTerminalSetup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateCounter();
        }

        private void MnuCurrencyMaster_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateCurrency();
        }

        private void MnuCostCenterMaster_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateCostCenter();
        }

        private void MnuProductGroup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateProductGroup();
        }

        private void MnuProductSubGroup_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateProductSubGroup();
        }

        private void MnuProductUnit_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateProductUnit();
        }

        private void MnuProduct_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateProduct();
        }

        private void MnuProductScheme_Click(object sender, EventArgs e)
        {
            var result = new FrmProductScheme
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuProductMapping_Click(object sender, EventArgs e)
        {
            var result = new FrmProductMapping
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuProductLock_Click(object sender, EventArgs e)
        {
            var result = new FrmProductLock
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuProductUpdate_Click(object sender, EventArgs e)
        {
            var result = new FrmProductUpdate
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuBarcodePrint_Click(object sender, EventArgs e)
        {
            var result = new FrmBarcodePrint
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuProductAssemble_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");
        }

        private void MnuProductImport_Click(object sender, EventArgs e)
        {
            var result = new FrmProductUpdate
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuGodownMaster_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateGodown();
        }

        private void MnuNarration_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateNarration();
        }

        private void MnuRemarks_Click(object sender, EventArgs e)
        {
            GetMasterList.CreateNarration();
        }

        private void MnuCopyMaster_Click(object sender, EventArgs e)
        {
            var result = new FrmCopyMaster();
            result.ShowDialog();
        }

        private void MnuLedgerOpeningImport_Click(object sender, EventArgs e)
        {
            var result = new FrmProductOpeningFromExcel
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuProductOpeningImport_Click(object sender, EventArgs e)
        {
            var result = new FrmProductOpeningFromExcel
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuPurchaseTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseBillingTerm("PB")
            {
                Owner = this
            };
            result.ShowDialog();
        }

        private void MnuSalesTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesBillingTerm("SB")
            {
                Owner = this
            };
            result.ShowDialog();
        }

        private void MnuPurchaseReturnTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmPurchaseBillingTerm("PR")
            {
                Owner = this
            };
            result.ShowDialog();
        }

        private void MnuSalesReturnTerm_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesBillingTerm("SR")
            {
                Owner = this
            };
            result.ShowDialog();
        }
        private void MnuSalesQuotationEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesQuotationEntry(false, "")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesQuotationRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SQ")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesQuotationOutStanding_Click(object sender, EventArgs e)
        {
            var order = new FrmOutstandingSalesOrder
            {
                MdiParent = this
            };
            order.Show();
        }

        private void MnuSalesOrderEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesOrderEntry(false, "")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesOrderRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SO")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesOrderOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingSalesOrder
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesChallanEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesChallanEntry(false, "")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesChallanRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SC")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesChallanOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingSalesChallan
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntry(false, "")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SB")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceOutStanding_Click(object sender, EventArgs e)
        {
            var result = new FrmOutstandingPartyLedger("CUSTOMER")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesInvoiceAnalysis_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesAnalysis
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesReturnEntry_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesReturnEntry(false, "")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesReturnRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesInvoiceEntryRegister("SR")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesReturnVatRegister_Click(object sender, EventArgs e)
        {
            var result = new FrmSalesVatRegister("SR")
            {
                Owner = this
            };
            result.Show();
        }

        private void MnuSalesAdditionalEntry_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT..!!");
        }

        private void MnuSalesAdditionalRegister_Click(object sender, EventArgs e)
        {
            new FrmSalesAdditionalRegister
            {
                MdiParent = this
            }.Show();
        }

        private void MnuSalesExpiryBreakageEntry_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT..!!");
        }

        private void MnuSalesExpiryBreakageRegister_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT..!!");
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

        [Obsolete("Obsolete")]
        private void MnuSalesMisReports_Click(object sender, EventArgs e)
        {
            var dynamic = new FrmDynamicRegisterReports();
            dynamic.ShowDialog();
        }

        private void MnuSalesTopTenCustomer_Click(object sender, EventArgs e)
        {
            var frm = new FrmTopNAnalysis("C")
            {
                MdiParent = this
            };
            frm.Show();
        }

        private void MnuSalesTopTenProduct_Click(object sender, EventArgs e)
        {
            var frm = new FrmTopNAnalysis("P")
            {
                MdiParent = this
            };
            frm.Show();
        }

        [Obsolete]
        private void MnuSalesDynamicReports_Click(object sender, EventArgs e)
        {
            var reports = new FrmDynamicRegisterReports();
            reports.ShowDialog();
        }
        private void cASHBANKENTRYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ObjGlobal.UserAuthorized)
            {
                var result = new FrmJournalVoucherEntry(false, "", true);
                result.ShowDialog();
            }
            else
            {
                MessageBox.Show(@"YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void cASHBANKVOUCHERToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void jOURNALVOUCHERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var entry = new FrmJournalVoucherEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }

        private void jOURNALENTRYToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var entry = new FrmJournalVoucherEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }

        private void MnuLedgerOpeningEntry_Click(object sender, EventArgs e)
        {
            new FrmLedgerOpeningEntry
            {
                MdiParent = this,

            }.Show();
        }

        private void MnuOutStandingLedgerOpeningEntry_Click(object sender, EventArgs e)
        {
            new FrmLedgerOpeningBillWiseEntry(false, string.Empty) { MdiParent = this }.Show();


        }

        private void MnuOpeningTrial_Click(object sender, EventArgs e)
        {
            var balance = new FrmTrialBalance(@"OB")
            {
                MdiParent = this
            };
            balance.Show();

        }

        private void jOURNALBOOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");
        }

        private void cASHBANKBOOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ObjGlobal.UserAuthorized)
            {
                var entry = new FrmCashBankEntry(false, string.Empty, string.Empty, true)
                {
                    MdiParent = this
                };
                entry.Show();
            }
            else
            {
                MessageBox.Show(@"YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

        }

        private void bANKRECONCILEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmBankReconciliationStatement { MdiParent = this }.Show();


        }

        private void cASHBANKSUMMARYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");

        }

        private void netFundPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");
        }
        private void mISReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("under construction");
        }

        private void jOURNALENTRYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var entry = new FrmJournalVoucherEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();

        }

        private void outStandingReportsToolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }


        private void MnuPurchaseIndentOutStanding_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");
        }

        private void registerReportsToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");


        }

        private void MnuPurchaseMisReports_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void physicalStockToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var result = new FrmPhysicalStockEntry(false, "");
            result.ShowDialog();
        }

        private void MnuPurchaseInvoiceRegister_Click(object sender, EventArgs e)
        {
            new FrmPurchaseRegister(((ToolStripMenuItem)sender).Tag?.ToString())
            {
                MdiParent = this
            }.Show();
        }

        private void voucherEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmPurchaseAdditionalRegister
            {
                MdiParent = this
            }.Show();
        }

        [Obsolete]
        private void MnuSystemSetting_Click(object sender, EventArgs e)
        {
            var result = new FrmSystemSettings();
            result.ShowDialog(this);
            BindFooterDetails();
        }

        private void MnuFinanceSetting_Click(object sender, EventArgs e)
        {
            new FrmFinanceSetting().ShowDialog();

        }

        private void MnuPurchaseSetting_Click(object sender, EventArgs e)
        {

            var dialog = new FrmPurchaseSetting();
            dialog.ShowDialog(this);
        }

        private void MnuSalesSetting_Click(object sender, EventArgs e)
        {
            var dialog = new FrmSalesSetting();
            dialog.ShowDialog(this);
        }

        private void MnuInventorySetting_Click(object sender, EventArgs e)
        {
            var result = new FrmInventorySetting();
            result.ShowDialog(this);
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

        private void MnuServerInformation_Click(object sender, EventArgs e)
        {
            var result = new FrmMultiServer(true);
            result.ShowDialog();
        }

        private void MnuBackUp_Click(object sender, EventArgs e)
        {
            var result = new FrmBackUpDataBase(@"BackUp");
            result.ShowDialog();
        }

        private void MnuRecalculate_Click(object sender, EventArgs e)
        {

            var dialogResult = new XFrmRecalculate();
            dialogResult.ShowDialog();
        }

        private void MnuRestore_Click(object sender, EventArgs e)
        {
            var login = new FrmLockScreen(false, @"RESTORE");
            login.ShowDialog();
            if (login.DialogResult is DialogResult.Yes)
            {
                var result = new FrmRestore();
                result.ShowDialog();
                MnuLogOut.PerformClick();
            }
        }

        private void Mn_ExternalDeviceUtility_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");
        }

        private void MnuSMSConfig_Click(object sender, EventArgs e)
        {
            new FrmSMSConfig().ShowDialog();

        }

        private void MnuImportData_Click(object sender, EventArgs e)
        {

        }

        private void MnuLocalImport_Click(object sender, EventArgs e)
        {
            var result = new FrmImportData(@"LOCAL");
            result.ShowDialog();
        }

        private void MnuSecondaryServer_Click(object sender, EventArgs e)
        {
            new FrmImportData("SECONDARY").ShowDialog();

        }

        private void MnuOnlineSyncData_Click(object sender, EventArgs e)
        {
            var result = new FrmCloudDataSync();
            result.ShowDialog();
        }
        private void MnuReconcileData_Click(object sender, EventArgs e)
        {
            var result = new FrmDataReconciliation();
            result.ShowDialog();
        }

        private void auditTrialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var trial = new FrmAuditTrial
            {
                MdiParent = this
            };
            trial.Show();
        }

        private void MnuOnlineTools_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");


        }

        private void MnuStockAdjustmentEntry_Click(object sender, EventArgs e)
        {
            var dialog = new FrmStockAdjustment(false, string.Empty)
            {
                MdiParent = this
            };
            dialog.Show();
        }
        private void MnuGodownTransferEntry_Click(object sender, EventArgs e)
        {

        }
        private void MnuUserDefineField_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");
        }

        private void voucherEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuSalesMonthlyReports_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");
        }

        private void MnuPurchaseIndentEntry_Click(object sender, EventArgs e)
        {
            var entry = new FrmPurchaseIndentEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }

        private void MnuPurchaseIndentRegister_Click(object sender, EventArgs e)
        {
            var entry = new FrmPurchaseIndentEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }

        private void MnuPurchaseOrderEntry_Click(object sender, EventArgs e)
        {
            var entry = new FrmPurchaseOrderEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }

        private void MnuPurchaseOrderRegister_Click(object sender, EventArgs e)
        {
            var register = new FrmSalesInvoiceEntryRegister(@"SO")
            {
                MdiParent = this
            };
            register.Show();

        }

        private void MnuPurchaseOrderOutStanding_Click(object sender, EventArgs e)
        {
            var order = new FrmOutstandingPurchaseOrder
            {
                MdiParent = this
            };
            order.Show();
        }

        private void MnuPurchaseChallanEntry_Click(object sender, EventArgs e)
        {
            var entry = new FrmPurchaseChallanEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }

        private void MnuPurchaseInvoiceEntry_Click(object sender, EventArgs e)
        {
            if (ObjGlobal.PurchaseLedgerId > 0)
            {
                new FrmPurchaseInvoiceEntry(false, string.Empty) { MdiParent = this }.Show();
            }
            else
            {
                MessageBox.Show(@"DEFAULT PURCHASE A/C IS NOT TAG ON SYSTEM CONFIG..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                new FrmSystemConfiguration().ShowDialog();
            }

        }

        private void MnuPurchaseInvoiceOutStanding_Click(object sender, EventArgs e)
        {
            var order = new FrmOutstandingPurchaseOrder
            {
                MdiParent = this
            };
            order.Show();
        }

        private void MnuPurchaseInvoiceAnalysis_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER dEVELOPMENT");
        }

        private void MnuPurchaseReturnEntry_Click(object sender, EventArgs e)
        {
            if (ObjGlobal.PurchaseReturnLedgerId > 0)
            {
                new FrmPurchaseReturnEntry(false, string.Empty) { MdiParent = this }.Show();
            }
            else
            {
                MessageBox.Show(@"DEFAULT PURCHASE RETURN A/C IS NOT TAG ON SYSTEM CONFIG..!!", ObjGlobal.Caption,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                new FrmSystemConfiguration().ShowDialog();
            }
        }

        private void MnuPurchaseReturnRegister_Click(object sender, EventArgs e)
        {
            var register = new FrmPurchaseRegister(((ToolStripMenuItem)sender).Tag?.ToString())
            {
                MdiParent = this
            };
            register.Show();
        }

        private void MnuPurchaseDynamicReports_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");

        }

        private void maskebariReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");

        }

        private void voucherEntryToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");

        }

        private void voucherEntryToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER CONSTRUCTION");

        }

        private void MnuMultiCashBankEntry_Click(object sender, EventArgs e)
        {
            if (!ObjGlobal.UserAuthorized)
            {
                var entry = new FrmCashBankEntry(false, string.Empty, @"CASH", false, true, true)
                {
                    MdiParent = this
                };
                entry.Show();
            }
            else
            {
                CustomMessageBox.Warning(@"YOU ARE NOT A VALID USER FOR THIS FUNCTION..!!");
            }

        }

        private void MnuStockAdjustmentVoucherReports(object sender, EventArgs e)
        {
            var report = new FrmStockManagement()
            {
                MdiParent = this
            };
            report.Show();
        }
        private void MnuPhysicalStockEntry_Click(object sender, EventArgs e)
        {
            var entry = new FrmPhysicalStockEntry(false, string.Empty)
            {
                MdiParent = this
            };
            entry.Show();
        }
        private void MnuPhysicalStockRegisterReport_Click(object sender, EventArgs e)
        {
            var report = new FrmStockManagement()
            {
                MdiParent = this
            };
            report.Show();
        }
        private void MnuCompanyProfile_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuHelpFile_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuOnlineRegistration_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");


        }

        private void MnuJobVacancyPost_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuIrdRequiredDocuments_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");


        }

        private void MnuWebSites_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuLicenseDetails_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuVersionUpdate_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuSocial_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }

        private void MnuCheckUpdate_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");


        }

        private void MnuLastYearClosing_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNCER DEVELOPMENT");
        }

        private void MnuVoucherNumbering_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");
        }

        private void MnuVoucherReNumbering_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");
        }

        private void registerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomMessageBox.Information("UNDER DEVELOPMENT");

        }
        private void MnuMaterializeView_Click(object sender, EventArgs e)
        {
            var view = new FrmMaterializeView
            {
                MdiParent = this
            };
            view.Show();
        }

        private void MnuVatRegister_Click(object sender, EventArgs e)
        {
            var register = new FrmVatRegister
            {
                MdiParent = this
            };
            register.Show();

        }

        private void MnuSalesVatRegister_Click(object sender, EventArgs e)
        {
            var register = new FrmSalesVatRegister(@"SB")
            {
                MdiParent = this
            };
            register.Show();
        }

        private void SearchGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
            var model = SearchGrid.Rows[e.RowIndex].DataBoundItem as GridMenuSearch;
            SearchPanel.Visible = false;
            TxtSearchGrid.Clear();
            model?.MenuItem.PerformClick();
        }

        private void registerReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }
        private void SearchGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion



        // METHOD FOR THIS FORM

        #region ---------- METHOD FOR THIS FORM ----------

        [Obsolete]
        private void BindFooterDetails()
        {
            try
            {
                HearderMenuList.Visible = true;
                if (ObjGlobal.SoftwareModule.IsBlankOrEmpty())
                {
                    var frm = new FrmSoftwareModule();
                    frm.ShowDialog();
                    ObjGlobal.SoftwareModule = frm.SoftwareModule.ToUpper();
                    tsModule.Text = $@"MODULE :-[{frm.SoftwareModule.ToUpper()}]";
                    MnuLogOut.PerformClick();
                }

                ObjGlobal.GetFiscalYearDetails();
                TsInitial.Text = ObjGlobal.InitialCatalog;
                TsLogInUser.Text = $@"USER_INFO :- {ObjGlobal.LogInUser.ToUpper()}";
                TsCompanyInfo.Text = $@"COMPANY :- {ObjGlobal.LogInCompany?.ToUpper()}";
                TsStartDate.Text = ObjGlobal.SysDateType is "M" or null
                    ? $@"FROM_DATE :-  {ObjGlobal.CfStartBsDate}"
                    : $@"FROM_DATE :-  {ObjGlobal.CfStartAdDate.ToShortDateString()}";
                TsEndDate.Text = ObjGlobal.SysDateType is "M" or null
                    ? $@"TO_DATE :- {ObjGlobal.CfEndBsDate}"
                    : $@"TO_DATE :- {ObjGlobal.CfEndAdDate.ToShortDateString()}";
                TsBranchInfo.Text = $@"BRANCH :- {ObjGlobal.SysBranchName}";
                TsFiscalYears.Text = $@"FISCAL_YEAR :- {(ObjGlobal.SysDateType is "M" ? ObjGlobal.SysBsFiscalYear : ObjGlobal.SysFiscalYear)}";
                TsWebSites.Text = @"www.mrsolution.com.np";
                TsCopyRights.Text = @"COPYRIGHTS RESERVE : M&&R Solution Business It Solution Pvt. ltd © 2023 ";
                Text = @"VISMA ERP | ACCOUNT & INVENTORY MANAGEMENT SYSTEM";
                tsModule.Text = $@"MODULE :-{ObjGlobal.SoftwareModule.GetUpper()}";
                MenuAccessControl();
                SaveLoginAuditInDatabase();
                if (ObjGlobal.LogInUserCategory is "TERMINAL")
                {
                    var invoice = new FrmPSalesInvoice
                    {
                        MdiParent = this,
                        WindowState = FormWindowState.Normal,
                        Dock = DockStyle.Fill
                    };
                    invoice.Show();
                }

                if (ObjGlobal.IsNewInstallation)
                {
                    tsRegistration.Text = @" REGISTRATION INFO : YOUR LICENSE IS DEMO VERSION...!!";
                    tsRegistration.ForeColor = Color.Blue;
                }
                else
                {
                    tsRegistration.Text = ObjGlobal.IsLicenseExpire
                        ? @$" REGISTRATION INFO : YOUR LICENSE HAS BEEN EXPIRED [{DateTime.Now.GetDateString()}]"
                        : @"  REGISTRATION INFO : YOUR LICENSE HAS BEEN REGISTER";
                    if (ObjGlobal.RemainingDays < 15)
                    {
                        var result = new FrmLicenseExpiredMessage();
                        result.ShowDialog();
                        tsRegistration.Text = $@"LICENSE WILL BE EXPIRE AFTER [{ObjGlobal.RemainingDays}] DAYS";
                        tsRegistration.ForeColor = Color.IndianRed;
                    }
                    tsRegistration.ForeColor = ObjGlobal.IsLicenseExpire ? Color.Red : Color.DarkGreen;
                }

                tmrLicenses.Enabled = true;
                _ = DailyBackupAsync();
                MenuSearch();
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult(e);
            }
        }
        private void ProjectFocusMenuVisible()
        {
            if (Debugger.IsAttached)
            {
                return;
            }
            ObjGlobal.SoftwareModule = ObjGlobal.SoftwareModule.GetUpper();
            if (ObjGlobal.SoftwareModule.Equals(@"DEVELOPER") || ObjGlobal.LogInUser is "AMSADMIN")
            {
                return;
            }
            //MnuHospital.Enabled = ObjGlobal.SoftwareModule is "HOSPITAL";
            //tsHospital.Visible = tsHospital.Enabled = MnuHospital.Visible = MnuHospital.Enabled;

            //MnuPOS.Enabled = ObjGlobal.SoftwareModule.ToUpper() is "POS";
            //TsPOS.Visible = TsPOS.Enabled = MnuPOS.Visible = MnuPOS.Enabled;

            //MnuHotelManagement.Visible = ObjGlobal.SoftwareModule is "HOTEL";
            //tsHotel.Visible = tsHotel.Enabled = MnuHotelManagement.Enabled = MnuHotelManagement.Visible;

            //MnuVehicleManagement.Visible = ObjGlobal.SoftwareModule is "VEHICLE";
            //TsVehicle.Visible = MnuVehicleManagement.Enabled = MnuVehicleManagement.Visible;

            //MnuSchoolTime.Visible = ObjGlobal.SoftwareModule is "SCHOOLTIME" or "SCHOOL-TIME";
            //MnuSchoolTime.Enabled = TsSchoolTime.Visible = MnuSchoolTime.Visible;

            //MnuStationaryManagement.Enabled = ObjGlobal.SoftwareModule.Equals("SCHOOL-TIME");
            //MnuStationaryManagement.Visible = MnuStationaryManagement.Enabled;

            //MnuRestaurant.Enabled = ObjGlobal.SoftwareModule is "RESTRO";
            //tsRestaurant.Visible = MnuRestaurant.Visible = MnuRestaurant.Enabled;

            //MnuTravel.Visible = ObjGlobal.SoftwareModule.ToUpper() is "TRAVEL";
            //TsTravel.Visible = MnuTravel.Enabled = MnuTravel.Visible;

            //MnuPharma.Visible = ObjGlobal.SoftwareModule.ToUpper() is "PHARMA";
            //TsPharma.Visible = MnuPharma.Enabled = MnuPharma.Visible;

            //MnuCinema.Visible = ObjGlobal.SoftwareModule.ToUpper() is "CINEMA";
            //TsCinema.Visible = MnuCinema.Enabled = MnuCinema.Visible;

            //MnuAdvanceProduction.Visible = ObjGlobal.SoftwareModule.ToUpper() is "ERP";
            //MnuAdvanceProduction.Enabled = MnuAdvanceProduction.Visible;

            //MnuPayroll.Enabled = ObjGlobal.SoftwareModule.ToUpper() is "ERP";
            //MnuPayroll.Visible = MnuPayroll.Enabled;

            MnuCompanySetup.Enabled = !ObjGlobal.IsOnlineMode;
            MnuCompanyRights.Enabled = !ObjGlobal.IsOnlineMode;
            MnuUserCreate.Enabled = !ObjGlobal.IsOnlineMode;
            MnuUserControl.Enabled = !ObjGlobal.IsOnlineMode;
            MnuAddCrystalDesign.Enabled = !ObjGlobal.IsOnlineMode;

            //MnuSqlServerConnection.Enabled = !ObjGlobal.IsOnlineMode;

            MnuBackUp.Enabled = !ObjGlobal.IsOnlineMode;
            MnuRestore.Enabled = !ObjGlobal.IsOnlineMode;

            //MnuExternalDeviceUtility.Enabled = !ObjGlobal.IsOnlineMode;
            MnuImportData.Enabled = !ObjGlobal.IsOnlineMode;

            //MnuOnlineConfig.Enabled = !ObjGlobal.IsOnlineMode;
            //MnuOnlineDataSync.Enabled = !ObjGlobal.IsOnlineMode;

            MnuRestore.Enabled = !ObjGlobal.IsIrdRegister && !ObjGlobal.IsOnlineMode;

            MnuChangePassword.Enabled = !ObjGlobal.LogInUser.GetUpper().Equals(@"AMSADMIN");
            MnuChangePassword.Visible = MnuChangePassword.Enabled;
        }
        private void DisableMenuList(bool isEnable = false)
        {
            HearderMenuList.Items.OfType<ToolStripMenuItem>().Where(i => i.DropDownItems.Count > 0).ForEach(x =>
            {
                x.Enabled = isEnable;
                x.Visible = isEnable;
                x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
                {
                    di.Enabled = isEnable;
                    di.Visible = isEnable;

                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Enabled = isEnable;
                            child.Visible = isEnable;
                        });
                    }
                });
            });
        }
        private void UserFocusMenuVisible()
        {
            DisableMenuList();
            if (ObjGlobal.LogInUserCategory is "SALES")
            {
                MnFinanceMaster.Visible = MnFinanceMaster.Enabled = true;
                MnSalesMaster.Visible = MnSalesMaster.Enabled = true;
                MnuSalesInvoiceMaster.Enabled = MnuSalesInvoiceMaster.Visible = true;
                MnuPointOfSalesInvoice.Enabled = MnuPointOfSalesInvoice.Checked = true;
                MnuSalesInvoiceEntry.Enabled = MnuSalesInvoiceEntry.Visible = true;
                MnuSalesInvoiceRegister.Enabled = true;
                return;
            }
            if (ObjGlobal.LogInUserCategory is "TERMINAL")
            {
                //HMnuRegisterReport.Visible = HMnuRegisterReport.Enabled = true;
                //HMnuEntery.Visible = HMnuEntery.Enabled = true;
                //MnuSalesMaster.Enabled = MnuSalesMaster.Visible = true;
                //MnuPOS.Visible = MnuPOS.Enabled = true;
                //MnuSalesPosInvoice.Enabled = MnuSalesPosInvoice.Visible = true;
                //MnuSalesAvtInvoice.Enabled = MnuSalesAvtInvoice.Visible = true;
                //MnuProvision.Enabled = MnuProvision.Visible = true;
                //MnuPorvSalesInvoice.Enabled = MnuPorvSalesInvoice.Visible = true;
                //MnuFinance.Visible = MnuFinance.Enabled = true;
                //MnusalesInvoiceToolStripMenuItem1.Enabled = MnusalesInvoiceToolStripMenuItem1.Visible = true;
                //MnuAvtSales.Enabled = MnuAvtSales.Visible = true;
                return;
            }
            if (ObjGlobal.LogInUserCategory is "ORDER")
            {
                //HMnuRegisterReport.Visible = HMnuRegisterReport.Enabled = true;
                //HMnuEntery.Visible = HMnuEntery.Enabled = true;
                //MnuPurchaseMaster.Enabled = MnuPurchaseMaster.Visible = true;
                //MnuAdvanceProduction.Enabled = MnuAdvanceProduction.Visible = true;
                //MnuPartyOrderToolStripMenuItem.Enabled = MnuPartyOrderToolStripMenuItem.Visible = true;
                //MnuRestaurant.Enabled = MnuRestaurant.Visible = true;
                //MnuRestroOrder.Enabled = MnuRestroOrder.Visible = true;
                //MnuSalesMaster.Enabled = MnuSalesMaster.Visible = true;
                //MnuSalesOrder.Enabled = MnuSalesOrder.Visible = true;
                return;
            }
            if (ObjGlobal.LogInUserCategory is "REPORT")
            {
                //HMnuFinanceReport.Enabled = HMnuFinanceReport.Visible = true;
                //MnuCashBankReport.Enabled = MnuCashBankReport.Visible = true;
                //MnuCashFlowReport.Enabled = MnuCashFlowReport.Visible = true;
                //MnuCashFlowReport.Enabled = MnuCashFlowReport.Visible = true;
                //MnuBankReconcileReport.Enabled = MnuBankReconcileReport.Visible = true;
                //MnuDayBookReport.Enabled = MnuDayBookReport.Visible = true;
                //MnuJournalVoucherReport.Enabled = MnuJournalVoucherReport.Visible = true;
                //MnuGeneralLedgerReport.Enabled = MnuGeneralLedgerReport.Visible = true;
                //MnuJoinLedger.Enabled = MnuJoinLedger.Visible = true;
                //MnuMergeLedger.Enabled = MnuMergeLedger.Visible = true;
                //MnutrialBalanceTBToolStripMenuItem.Enabled = MnutrialBalanceTBToolStripMenuItem.Visible = true;
                //MnuOpeningTrailBalance.Enabled = MnuOpeningTrailBalance.Visible = true;
                //MnuNormalTrialBalance.Enabled = MnuNormalTrialBalance.Visible = true;
                //MnuPerodicTrialBalance.Enabled = MnuPerodicTrialBalance.Visible = true;
                //MnuprofitLossPLToolStripMenuItem.Enabled = MnuprofitLossPLToolStripMenuItem.Visible = true;
                //MnuNormalProfitLoss.Enabled = MnuNormalProfitLoss.Visible = true;
                //MnuPerodicProfitLoss.Enabled = MnuPerodicProfitLoss.Visible = true;
                //MnubalanceSheetBSToolStripMenuItem.Enabled = MnubalanceSheetBSToolStripMenuItem.Visible = true;
                //MnuOpeningBalanceSheet.Enabled = MnuOpeningBalanceSheet.Visible = true;
                //MnuNormalBalanceSheet.Enabled = MnuNormalBalanceSheet.Visible = true;
                //MnuPerodicBalanceSheet.Enabled = MnuPerodicBalanceSheet.Visible = true;
                //MnuDaynamicReports.Enabled = MnuDaynamicReports.Visible = true;
                //MnuNetFundPosition.Enabled = MnuNetFundPosition.Visible = true;
                //MnuDepartmentWiseReports.Enabled = MnuDepartmentWiseReports.Visible = true;
                //MnuDocumentPrinting.Enabled = MnuDocumentPrinting.Visible = true;
                //MnuDPCashBank.Enabled = MnuDPCashBank.Visible = true;
                //MnuDPJournalVoucher.Enabled = MnuDPJournalVoucher.Visible = true;
                //MnuDPDebitNotes.Enabled = MnuDPDebitNotes.Visible = true;
                //MnuDPCreditNotes.Enabled = MnuDPCreditNotes.Visible = true;
                //MnuListOfMaster.Enabled = MnuListOfMaster.Visible = true;
                //MnuListofGeneralLedger.Enabled = MnuListofGeneralLedger.Visible = true;


                //HMnuRegisterReport.Enabled = HMnuRegisterReport.Visible = true;
                //MnuPurchasseRegister.Enabled = MnuPurchasseRegister.Visible = true;
                //MnuPurchaseIndentRegister.Enabled = MnuPurchaseIndentRegister.Visible = true;
                //MnuPurchaseOrderRegistration.Enabled = MnuPurchaseOrderRegistration.Visible = true;
                //MnuPuchaseGoodsInTransitRegister.Enabled = MnuPuchaseGoodsInTransitRegister.Visible = true;
                //MnuPurchaseChallanRegister.Enabled = MnuPurchaseChallanRegister.Visible = true;
                //MnuPurchaseInvoiceRegister.Enabled = MnuPurchaseInvoiceRegister.Visible = true;
                //MnuPurchaseTicketRegister.Enabled = MnuPurchaseTicketRegister.Visible = true;
                //MnuPurchaseAdditionalRegister.Enabled = MnuPurchaseAdditionalRegister.Visible = true;
                //MnuPurchaseReturnRegister.Enabled = MnuPurchaseReturnRegister.Visible = true;
                //MnuPurchaseExpiryBreakageRegsiter.Enabled = MnuPurchaseExpiryBreakageRegsiter.Visible = true;
                //MnuSalesRegister.Enabled = MnuSalesRegister.Visible = true;
                //MnuSBR_Quotation.Enabled = MnuSBR_Quotation.Visible = true;
                //MnuSBR_Order.Enabled = MnuSBR_Order.Visible = true;
                //MnuSBR_Challan.Enabled = MnuSBR_Challan.Visible = true;
                //MnuSBR_Invoice.Enabled = MnuSBR_Invoice.Visible = true;
                //MnuSBR_Additional.Enabled = MnuSBR_Additional.Visible = true;
                //MnuSBR_Return.Enabled = MnuSBR_Return.Visible = true;
                //MnuSBR_ExpBrk.Enabled = MnuSBR_ExpBrk.Visible = true;
                //Mnu_TicketSalesRegister.Enabled = Mnu_TicketSalesRegister.Visible = true;
                //MnupurchaseAnaylsisToolStripMenuItem.Enabled = MnupurchaseAnaylsisToolStripMenuItem.Visible = true;
                //MnuyearToDateToolStripMenuItem.Enabled = MnuyearToDateToolStripMenuItem.Visible = true;
                //MnupurchasePerodicToolStripMenuItem.Enabled = MnupurchasePerodicToolStripMenuItem.Visible = true;
                //MnupurchaseTop10WiseToolStripMenuItem.Enabled = MnupurchaseTop10WiseToolStripMenuItem.Visible = true;
                //MnuSalesAnaylsisToolStripMenuItem.Enabled = MnuSalesAnaylsisToolStripMenuItem.Visible = true;
                //MnuyearToDateToolStripMenuItem1.Enabled = MnuyearToDateToolStripMenuItem1.Visible = true;
                //MnuSalesPerodicToolStripMenuItem.Enabled = MnuSalesPerodicToolStripMenuItem.Visible = true;
                //MnuSalesTop10WiseToolStripMenuItem.Enabled = MnuSalesTop10WiseToolStripMenuItem.Visible = true;
                //MnuMemberReports.Enabled = MnuMemberReports.Visible = true;
                //MnuNotesToolStripMenuItem.Enabled = MnuNotesToolStripMenuItem.Visible = true;
                //MnuDebitNotesToolStripMenuItem2.Enabled = MnuDebitNotesToolStripMenuItem2.Visible = true;
                //MnuCreditNotesToolStripMenuItem2.Enabled = MnuCreditNotesToolStripMenuItem2.Visible = true;
                //MnuReceiptPaymentRegisterToolStripMenuItem.Enabled = MnuReceiptPaymentRegisterToolStripMenuItem.Visible = true;
                //MnuCustomerWiseToolStripMenuItem.Enabled = MnuCustomerWiseToolStripMenuItem.Visible = true;
                //MnuVendorWiseToolStripMenuItem.Enabled = MnuVendorWiseToolStripMenuItem.Visible = true;
                //MnuBothWiseToolStripMenuItem.Enabled = MnuBothWiseToolStripMenuItem.Visible = true;
                //MnuPartyLedgerToolStripMenuItem.Enabled = MnuPartyLedgerToolStripMenuItem.Visible = true;
                //MnuCustomerToolStripMenuItem.Enabled = MnuCustomerToolStripMenuItem.Visible = true;
                //MnuVendorToolStripMenuItem.Enabled = MnuVendorToolStripMenuItem.Visible = true;
                //MnuBothToolStripMenuItem.Enabled = MnuBothToolStripMenuItem.Visible = true;
                //MnuPartyConfirmationPrint.Enabled = MnuPartyConfirmationPrint.Visible = true;
                //MnuPartyLedgerReconcileToolStripMenuItem.Enabled = MnuPartyLedgerReconcileToolStripMenuItem.Visible = true;
                //MnuCustomerToolStripMenuItem1.Enabled = MnuCustomerToolStripMenuItem1.Visible = true;
                //MnuVendorToolStripMenuItem1.Enabled = MnuVendorToolStripMenuItem1.Visible = true;
                //MnuBothToolStripMenuItem1.Enabled = MnuBothToolStripMenuItem1.Visible = true;
                //MnuLedgerReconcileToolStripMenuItem.Enabled = MnuLedgerReconcileToolStripMenuItem.Visible = true;
                //MnuAgeingReports.Enabled = MnuAgeingReports.Visible = true;
                //MnuCustomerWiseAgeing.Enabled = MnuCustomerWiseAgeing.Visible = true;
                //MnuVendorWiseAgeing.Enabled = MnuVendorWiseAgeing.Visible = true;
                //MnuBothWiseAgeing.Enabled = MnuBothWiseAgeing.Visible = true;
                //MnuLedgerAgeing.Enabled = MnuLedgerAgeing.Visible = true;
                //MnuBranchWiseOutStanding.Enabled = MnuBranchWiseOutStanding.Visible = true;
                //MnuOutStandingReportsToolStripMenuItem.Enabled = MnuOutStandingReportsToolStripMenuItem.Visible = true;
                //MnuIndentToolStripMenuItem2.Enabled = MnuIndentToolStripMenuItem2.Visible = true;
                //MnuQuotationToolStripMenuItem4.Enabled = MnuQuotationToolStripMenuItem4.Visible = true;
                //MnuOrderToolStripMenuItem4.Enabled = MnuOrderToolStripMenuItem4.Visible = true;
                //MnuGoodsInTransitToolStripMenuItem2.Enabled = MnuGoodsInTransitToolStripMenuItem2.Visible = true;
                //MnuChallanToolStripMenuItem2.Enabled = MnuChallanToolStripMenuItem2.Visible = true;
                //MnuSuotationToolStripMenuItem5.Enabled = MnuSuotationToolStripMenuItem5.Visible = true;
                //MnuOrderToolStripMenuItem5.Enabled = MnuOrderToolStripMenuItem5.Visible = true;
                //MnuChallanToolStripMenuItem3.Enabled = MnuChallanToolStripMenuItem3.Visible = true;
                //MnuCustomerToolStripMenuItem3.Enabled = MnuCustomerToolStripMenuItem3.Visible = true;
                //MnuVendorToolStripMenuItem3.Enabled = MnuVendorToolStripMenuItem3.Visible = true;
                //MnuBothToolStripMenuItem3.Enabled = MnuBothToolStripMenuItem3.Visible = true;
                //MnuVatRegisterToolStripMenuItem.Enabled = MnuVatRegisterToolStripMenuItem.Visible = true;
                //MnuPurchaseToolStripMenuItem2.Enabled = MnuPurchaseToolStripMenuItem2.Visible = true;
                //MnuPurchaseReturnToolStripMenuItem.Enabled = MnuPurchaseReturnToolStripMenuItem.Visible = true;
                //MnuSalesToolStripMenuItem2.Enabled = MnuSalesToolStripMenuItem2.Visible = true;
                //MnuSalesReturnToolStripMenuItem1.Enabled = MnuSalesReturnToolStripMenuItem1.Visible = true;
                //MnuVatToolStripMenuItem.Enabled = MnuVatToolStripMenuItem.Visible = true;
                //MnuMaterializedViewToolStripMenuItem.Enabled = MnuMaterializedViewToolStripMenuItem.Visible = true;
                //MnuAuditTrialToolStripMenuItem.Enabled = MnuAuditTrialToolStripMenuItem.Visible = true;
                //MnuEntryLogRegisterToolStripMenuItem.Enabled = MnuEntryLogRegisterToolStripMenuItem.Visible = true;
                //MnuIrdSync.Enabled = MnuIrdSync.Visible = true;
                //MnuMaskeBariReportsToolStripMenuItem.Enabled = MnuMaskeBariReportsToolStripMenuItem.Visible = true;
                //MnuMaskeBariReports.Enabled = MnuMaskeBariReports.Visible = true;
                //MnuYearPartyConformation.Enabled = MnuYearPartyConformation.Visible = true;
                //MnuVatRegisterIrdFormat.Enabled = MnuVatRegisterIrdFormat.Visible = true;
                //MnuRegisterDynamicReports.Enabled = MnuRegisterDynamicReports.Visible = true;
                //MnuMISReportsToolStripMenuItem.Enabled = MnuMISReportsToolStripMenuItem.Visible = true;
                //MnuTopNCustomerList.Enabled = MnuTopNCustomerList.Visible = true;
                //MnuTopNVendorList.Enabled = MnuTopNVendorList.Visible = true;
                //MnuTopNProductList.Enabled = MnuTopNProductList.Visible = true;
                //MnuPartyAnalysisToolStripMenuItem.Enabled = MnuPartyAnalysisToolStripMenuItem.Visible = true;
                //MnuListOfMasterToolStripMenuItem2.Enabled = MnuListOfMasterToolStripMenuItem2.Visible = true;
                //MnuDocumentNumberingToolStripMenuItem1.Enabled = MnuDocumentNumberingToolStripMenuItem1.Visible = true;
                //MnuDocumentReNumberingToolStripMenuItem.Enabled = MnuDocumentReNumberingToolStripMenuItem.Visible = true;
                //MnuPurchaseTermToolStripMenuItem1.Enabled = MnuPurchaseTermToolStripMenuItem1.Visible = true;
                //MnuSalesTermToolStripMenuItem1.Enabled = MnuSalesTermToolStripMenuItem1.Visible = true;
                //MnuBranchToolStripMenuItem3.Enabled = MnuBranchToolStripMenuItem3.Visible = true;
                //MnuUnitToolStripMenuItem2.Enabled = MnuUnitToolStripMenuItem2.Visible = true;

                //HmnuStockReport.Enabled = HmnuStockReport.Visible = true;
                //MnuStockDetails.Enabled = MnuStockDetails.Visible = true;
                //MnuProductOpeningReport.Enabled = MnuProductOpeningReport.Visible = true;
                //MnuStockLedger.Enabled = MnuStockLedger.Visible = true;
                //MnuStockLedgerWithValue.Enabled = MnuStockLedgerWithValue.Visible = true;
                //MnuSpecialReports.Enabled = MnuSpecialReports.Visible = true;
                //MnuSpecialReportsProductWise.Enabled = MnuSpecialReportsProductWise.Visible = true;
                //MnuStockMomentAnaylsis.Enabled = MnuStockMomentAnaylsis.Visible = true;
                //MnuStockValuation.Enabled = MnuStockValuation.Visible = true;
                //MnuStockStatus.Enabled = MnuStockStatus.Visible = true;
                //MnuOverStockStatus.Enabled = MnuOverStockStatus.Visible = true;
                //MnuStockAnalysis.Enabled = MnuStockAnalysis.Visible = true;
                //MnuStockAgeing.Enabled = MnuStockAgeing.Visible = true;
                //MnuGodownWiseStock.Enabled = MnuGodownWiseStock.Visible = true;
                //MnuGodownWiseValuation.Enabled = MnuGodownWiseValuation.Visible = true;
                //MnuStockLedgerProductGodownWise.Enabled = MnuStockLedgerProductGodownWise.Visible = true;
                //MnuStockLedgerGodownProductWise.Enabled = MnuStockLedgerGodownProductWise.Visible = true;
                //MnuProductCosting.Enabled = MnuProductCosting.Visible = true;
                //MnuLandedCostRate.Enabled = MnuLandedCostRate.Visible = true;
                //MnuProductWiseCostRate.Enabled = MnuProductWiseCostRate.Visible = true;
                //MnuBillWiseCostRate.Enabled = MnuBillWiseCostRate.Visible = true;
                //MnuProfitabilityReport.Enabled = MnuProfitabilityReport.Visible = true;
                //MnuConsumptionReport.Enabled = MnuConsumptionReport.Visible = true;
                //MnuBillOfMaterialsReports.Enabled = MnuBillOfMaterialsReports.Visible = true;
                //MnuStoreRequisitionReports.Enabled = MnuStoreRequisitionReports.Visible = true;
                //MnuIssueRegisterReports.Enabled = MnuIssueRegisterReports.Visible = true;
                //MnuIssueReturnRegisterReports.Enabled = MnuIssueReturnRegisterReports.Visible = true;
                //MnuFinishedGoodsReceivedRegisterReports.Enabled = MnuFinishedGoodsReceivedRegisterReports.Visible = true;
                //MnuFinishedGoodsReturnRegisterReports.Enabled = MnuFinishedGoodsReturnRegisterReports.Visible = true;
                //MnuStockRegister.Enabled = MnuStockRegister.Visible = true;
                //MnuGodownTransferRegisterReports.Enabled = MnuGodownTransferRegisterReports.Visible = true;
                //MnuStockAdjustmentRegisterReports.Enabled = MnuStockAdjustmentRegisterReports.Visible = true;
                //MnuPhysicalStockRegisterReports.Enabled = MnuPhysicalStockRegisterReports.Visible = true;
                //MnuExpiryAndBreakageRegisterReports.Enabled = MnuExpiryAndBreakageRegisterReports.Visible = true;
                //MnuManufacturing.Enabled = MnuManufacturing.Visible = true;
                //MnuCostCenterExpensesRegisterReports.Enabled = MnuCostCenterExpensesRegisterReports.Visible = true;
                //MnuProductOrderRegisterReports.Enabled = MnuProductOrderRegisterReports.Visible = true;
                //MnuSampleCostingRegisterReports.Enabled = MnuSampleCostingRegisterReports.Visible = true;
                //MnuProductionBillOfMaterialsRegisterReports.Enabled = MnuProductionBillOfMaterialsRegisterReports.Visible = true;
                //MnuProductionIssueRegisterReports.Enabled = MnuProductionIssueRegisterReports.Visible = true;
                //MnuProductionTransferRegisterReports.Enabled = MnuProductionTransferRegisterReports.Visible = true;
                //MnuProductionReceivedRegisterReports.Enabled = MnuProductionReceivedRegisterReports.Visible = true;
                //MnuCostCenterIssueRegisterReports.Enabled = MnuCostCenterIssueRegisterReports.Visible = true;
                //MnuCostCenterTransferRegisterReports.Enabled = MnuCostCenterTransferRegisterReports.Visible = true;
                //MnuCostCenterReceivedRegisterReports.Enabled = MnuCostCenterReceivedRegisterReports.Visible = true;
                //MnuDynamicStockReports.Enabled = MnuDynamicStockReports.Visible = true;
                //MnuListOfMasterProduct.Enabled = MnuListOfMasterProduct.Visible = true;
                //MnuProductGroupList.Enabled = MnuProductGroupList.Visible = true;
                //MnuProductSubGroupList.Enabled = MnuProductSubGroupList.Visible = true;
                //MnuProductUnitList.Enabled = MnuProductUnitList.Visible = true;
                //MnuProductList.Enabled = MnuProductList.Visible = true;
                //MnuProductListWithImage.Enabled = MnuProductListWithImage.Visible = true;
                //MnuProductSchemeList.Enabled = MnuProductSchemeList.Visible = true;
                //MnuGodownLocation.Enabled = MnuGodownLocation.Visible = true;
                //MnuGodownList.Enabled = MnuGodownList.Visible = true;
                //MnuCostCenterList.Enabled = MnuCostCenterList.Visible = true;
                //MnuColastarList.Enabled = MnuColastarList.Visible = true;
                //MnuSpecialProduct.Enabled = MnuSpecialProduct.Visible = true;
                return;
            }
            DisableMenuList(true);
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
                //  _masterSetup.LoginLog.DEVICE = FrmLicenseGenerator.GetServerName();
                //  _masterSetup.LoginLog.DAVICE_MAC = ObjGlobal.GetMacAddress();
                // _masterSetup.LoginLog.DEVICE_IP = FrmLicenseGenerator.GetIpAddress();
                // _masterSetup.LoginLog.SYSTEM_ID = FrmLicenseGenerator.GetSerialNo().Result;
                //_masterSetup.LoginLog.LOG_STATUS = '1';
                //_masterSetup.SaveLoginLog("SAVE");
            }
            catch (Exception e)
            {
                e.DialogResult();
            }
        }

        [Obsolete]
        private void UserTerminated()
        {
            _deviceName = ObjGlobal.GetServerName();
            _deviceIp = ObjGlobal.GetIpAddress();
            _userSetup.ResetLoginLog(ObjGlobal.LogInUser);
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
        private void MenuAccessControl()
        {
            if (ObjGlobal.LogInUserCategory is "ADMIN" or "ADMINISTRATOR")
            {
                ProjectFocusMenuVisible();
            }
            else if (ObjGlobal.LogInUser is "NORMAL")
            {
                var alreadySavedData = _masterSetup.GetUserAccessControl(ObjGlobal.RoleId, ObjGlobal.LogInUserId);
                if (alreadySavedData.Rows.Count > 0)
                {
                    var xmlData = alreadySavedData.Rows[0].ItemArray[8].ToString();
                    if (!string.IsNullOrEmpty(xmlData))
                    {
                        ObjGlobal.MenuUserAccessControlData = xmlData;
                        var doc = new XmlDocument();
                        doc.LoadXml(xmlData);

                        var xmlStream = new MemoryStream();
                        doc.Save(xmlStream);

                        xmlStream.Flush();//Adjust this if you want read your data
                        xmlStream.Position = 0;
                        var rd = XmlReader.Create(xmlStream);
                        while (rd.Read())
                        {
                            var menuName = rd.Name;
                            if (rd.NodeType == XmlNodeType.Element && menuName != "accessMenuList")
                            {
                                //Console.WriteLine(rd.ReadElementContentAsString());

                                var value = rd.ReadElementContentAsBoolean();
                                var menu = HearderMenuList.Items.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
                                if (menu == null)
                                {
                                    var items = HearderMenuList.Items.OfType<ToolStripMenuItem>();
                                    var isExist = true;
                                    var menuItems = items as ToolStripMenuItem[] ?? items.ToArray();
                                    foreach (var it in menuItems)
                                    {
                                        var subMenu = it.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
                                        if (subMenu != null)
                                        {
                                            subMenu.Enabled = value;
                                            subMenu.Visible = value;
                                            isExist = true;
                                            break;
                                        }
                                        isExist = false;
                                    }
                                    if (isExist) continue;
                                    {
                                        foreach (var ch in menuItems)
                                        {
                                            var child = ch.DropDownItems.OfType<ToolStripMenuItem>();
                                            foreach (var chi in child)
                                            {
                                                var subMenu = chi.DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(x => x.Name == menuName);
                                                if (subMenu != null)
                                                {
                                                    subMenu.Enabled = value;
                                                    subMenu.Visible = value;
                                                    isExist = true;
                                                    break;
                                                }
                                                isExist = false;
                                            }
                                        }
                                    }
                                }
                                //menu.Enabled = value;
                            }
                            else
                            {
                                rd.Read();
                            }
                        }
                    }
                    else
                    {
                        ObjGlobal.MenuUserAccessControlData = string.Empty;
                    }

                    //form control xml
                    var formXmlData = alreadySavedData.Rows[0].ItemArray[9].ToString();
                    if (!string.IsNullOrEmpty(formXmlData))
                    {
                        var formXml = XmlUtils.XmlDeserialize<List<UserAccessFormControl>>(formXmlData);
                        ObjGlobal.FormUserAccessControlData = formXml;
                    }
                    else
                    {
                        ObjGlobal.FormUserAccessControlData = null;
                    }
                }
                else
                {
                    HearderMenuList.Items.OfType<ToolStripMenuItem>().Where(i => i.DropDownItems.Count > 0).ForEach(x =>
                    {
                        x.Enabled = true;
                        x.Visible = true;
                        x.DropDownItems.OfType<ToolStripMenuItem>().ForEach(di =>
                        {
                            di.Enabled = true;
                            di.Visible = true;
                            if (di.DropDownItems.Count > 0)
                            {
                                di.DropDownItems.OfType<ToolStripMenuItem>().ForEach(child =>
                                {
                                    child.Enabled = true;
                                    child.Visible = true;
                                });
                            }
                        });
                    });
                    ObjGlobal.FormUserAccessControlData = null;
                }
            }
            else
            {
                UserFocusMenuVisible();
            }
        }
        private void MenuSearch()
        {
            var finalSearchList = new List<GridMenuSearch>();
            var headerItems = HearderMenuList.Items.OfType<ToolStripMenuItem>().ToList();
            headerItems.ForEach(x =>
            {
                if (!x.Visible || !x.Enabled) return;
                var children = GetAllChildren(x).OfType<ToolStripMenuItem>();
                finalSearchList.AddRange(from child in children
                                         where child.Enabled
                                         where !child.HasDropDownItems
                                         select new GridMenuSearch
                                         {
                                             HeaderMenu = x.Name.Contains(@"&") ? x.Text.Replace(@"&", "") : x.Text,
                                             Menu = child.OwnerItem.Text,
                                             SubMenu = child.Text.StartsWith(@"&")
                                                 ? x.Text.Replace(@"&", "") + "/" + child.OwnerItem.Text.Replace(@"&", "").GetUpper() + @"/" +
                                                   x.Text.Replace(@"&", "").GetUpper()
                                                 : x.Text.Replace(@"&", "") + "/" + child.OwnerItem.Text.Replace(@"&", "").GetUpper() + @"/" +
                                                   child.Text.GetUpper(),
                                             MenuItem = child
                                         });
            });
            _menuSearches = finalSearchList;
            SearchGrid.DataSource = finalSearchList;
        }
        private static IEnumerable<ToolStripItem> GetAllChildren(ToolStripItem item)
        {
            var items = new List<ToolStripItem> { item };
            switch (item)
            {
                case ToolStripMenuItem menuItem:
                    {
                        foreach (ToolStripItem i in menuItem.DropDownItems)
                        {
                            items.AddRange(GetAllChildren(i));
                        }
                        break;
                    }
                case ToolStripSplitButton button:
                    {
                        foreach (ToolStripItem i in button.DropDownItems)
                        {
                            items.AddRange(GetAllChildren(i));
                        }
                        break;
                    }
                case ToolStripDropDownButton button:
                    {
                        foreach (ToolStripItem i in button.DropDownItems)
                        {
                            items.AddRange(GetAllChildren(i));
                        }
                        break;
                    }
            }

            return items.ToArray();
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
        private void BranchLogin()
        {
            try
            {
                var dtBranch = _masterSetup.LoginBranchDataTable(ObjGlobal.LogInUser.ToUpper() is "ADMIN" || ObjGlobal.LogInUser.ToUpper() is "AMSADMIN");
                if (dtBranch?.Rows.Count > 1)
                {
                    var branchListForm = new FrmBranchList();
                    branchListForm.ShowDialog();
                    if (branchListForm.DialogResult != DialogResult.OK)
                    {
                        return;
                    }
                    var dt = _masterSetup.GetCompanyUnit(@"SAVE");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        return;
                    }
                    var frm1 = new FrmCompanyUnitList();
                    frm1.ShowDialog();
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
        private bool DailyBackupAsync()
        {
            try
            {
                if (ObjGlobal.SysBackupDays > 0)
                {
                    var backDateTime = DateTime.Now;
                    var location = GetConnection.GetQueryData(@"SELECT ss.BackupLocation FROM AMS.SystemSetting ss");
                    var exits = Directory.Exists(location);
                    if (!exits)
                    {
                        return false;
                    }

                    var date = DateTime.Now;
                    var backUpFile = $@"{location}\{ObjGlobal.InitialCatalog}_{ObjGlobal.SysBranchName.Replace(" ", "").ToUpper()}_{date.Day.ToString().PadLeft(2, '0')}_{date.Month.ToString().PadLeft(2, '0')}_{date.Year.ToString().PadLeft(2, '0')}.bak";
                    if (File.Exists(backUpFile))
                    {
                        return false;
                    }

                    var backUpDays = (backDateTime - DateTime.Now).Days;
                    if (ObjGlobal.SysBackupDays < backUpDays || date.Hour is <= 16 or >= 18)
                    {
                        return false;
                    }

                    var question = CustomMessageBox.Question(@"DAILY BACKUP IS REQUIRE..?? DO YOU WANT TO CONTINUE..??");
                    if (question != DialogResult.Yes)
                    {
                        return false;
                    }

                    var result = ObjGlobal.DataBaseBackup(ObjGlobal.InitialCatalog, location);
                    if (result == 0)
                    {
                        return false;
                    }
                    DataBackupLog(location);
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                e.ToNonQueryErrorResult(e);
                return false;
            }
        }
        private void DataBackupLog(string location)
        {
            _backup.BackupLog.Log_ID = ClsMasterSetup.ReturnMaxIntValue(@"MASTER.AMS.BR_LOG", @"Log_ID").ToString().GetInt();
            _backup.BackupLog.DB_NAME = ObjGlobal.InitialCatalog;
            _backup.BackupLog.COMPANY = ObjGlobal.LogInCompany?.ToUpper();
            _backup.BackupLog.LOCATION = location;
            _backup.BackupLog.USED_BY = ObjGlobal.LogInUser.ToUpper();
            _backup.BackupLog.ACTION = @"B";
            _backup.BackupLog.SyncRowVersion = 1;
            _backup.BackupLog.USED_BY = ObjGlobal.LogInUser;
            _backup.BackupLog.USED_ON = DateTime.Now;
            _backup.BackupLog.STATUS = "Backup";
            _backup.SaveBackupAndRestoreDatabaseLog(location);
        }
        #endregion ---------- METHOD FOR THIS FORM ----------



        // OBJECT FOR THIS FORM
        #region --------------- OBJECT ---------------

        private int _loginId;
        private string _deviceName = string.Empty;
        private string _deviceIp = string.Empty;
        public string DbSelect = string.Empty;
        private IBackupRestoreRepository _backup;
        private readonly ISalesEntry _salesEntry;
        private readonly IMasterSetup _masterSetup;
        private IUserSetupRepository _userSetup;
        private List<GridMenuSearch> _menuSearches = new();
        private readonly IRecalculate _recalculate;



        #endregion --------------- OBJECT ---------------


    }
}