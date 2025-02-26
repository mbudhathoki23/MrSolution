using DevExpress.Utils.Extensions;
using DevExpress.XtraSplashScreen;
using Microsoft.Win32;
using MrBLL.DataEntry.Common;
using MrBLL.DataEntry.FinanceMaster;
using MrBLL.DataEntry.OpeningMaster;
using MrBLL.DataEntry.ProductionMaster;
using MrBLL.DataEntry.PurchaseMaster;
using MrBLL.DataEntry.SalesMaster;
using MrBLL.DataEntry.StockMaster;
using MrBLL.Domains.DynamicReport;
using MrBLL.Domains.Hospital;
using MrBLL.Domains.Hospital.Master;
using MrBLL.Domains.MoneyExchange;
using MrBLL.Domains.Movie.Master;
using MrBLL.Domains.POS.Entry;
using MrBLL.Domains.POS.Master;
using MrBLL.Domains.Production;
using MrBLL.Domains.Production.Entry;
using MrBLL.Domains.Remit;
using MrBLL.Domains.Restro.Entry;
using MrBLL.Domains.Restro.Master;
using MrBLL.Domains.SchoolTime.Stationary;
using MrBLL.Domains.Services;
using MrBLL.Domains.VehicleManagement.Servicing;
using MrBLL.Master.DocumentNumbering;
using MrBLL.Master.Import;
using MrBLL.Master.LedgerSetup;
using MrBLL.Master.ProductSetup;
using MrBLL.Master.TermSetup;
using MrBLL.Reports.Finance_Report.Analysis;
using MrBLL.Reports.Finance_Report.DayBook;
using MrBLL.Reports.Finance_Report.FinalReport;
using MrBLL.Reports.Finance_Report.ListOfMaster;
using MrBLL.Reports.Inventory_Report.Analysis;
using MrBLL.Reports.Inventory_Report.GodownWise;
using MrBLL.Reports.Inventory_Report.ListOfMaster;
using MrBLL.Reports.Inventory_Report.Production;
using MrBLL.Reports.Inventory_Report.StockLedger;
using MrBLL.Reports.PartyConfirmation;
using MrBLL.Reports.Reconcile.BankReconcile;
using MrBLL.Reports.Register_Report.Aging_Report;
using MrBLL.Reports.Register_Report.Analysis_Report;
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
using MrBLL.SystemSetting.PayrollSetting;
using MrBLL.SystemSetting.PrintSetting;
using MrBLL.Utility.Common;
using MrBLL.Utility.Common.Class;
using MrBLL.Utility.CrystalReports;
using MrBLL.Utility.Database;
using MrBLL.Utility.DataSync;
using MrBLL.Utility.ImportUtility;
using MrBLL.Utility.MIS;
using MrBLL.Utility.MrLicense;
using MrBLL.Utility.Restore;
using MrBLL.Utility.ServerConnection;
using MrBLL.Utility.Social;
using MrBLL.Utility.SplashScreen;
using MrDAL.Control.WinControl;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Master;
using MrDAL.Master.Interface;
using MrDAL.Setup.BackupRestore;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Analytics;
using MrDAL.Utility.Config;
using MrDAL.Utility.dbMaster;
using MrDAL.Utility.Interface;
using MrDAL.Utility.Server;
using MrSolution.About;
using MrSolution.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MrSolution;

public sealed partial class MdiMrSolution : MrForm
{
    // MAIN DASH BOARD
    #region --------------- FORM ---------------

    [Obsolete]
    public MdiMrSolution()
    {
        InitializeComponent();
        HearderMenuList.Visible = false;
        BackgroundImage = Resources._1920MDI;
        _master = new ClsMasterSetup();
        _salesEntry = new ClsSalesEntry();
        _recalculate = new ClsRecalculate();
        _backup = new BackupRestoreRepository();
        SearchGrid.AutoGenerateColumns = false;
    }

    [Obsolete]
    private void FrmDashBoard_Load(object sender, EventArgs e)
    {
        var result = new FrmLogin();
        result.ShowDialog();
        if (result.DialogResult != DialogResult.OK)
        {
            return;
        }
        var logIn = CompanyLogin();
        BindFooterDetails();
    }

    private void FrmDashBoard_KeyDown(object sender, KeyEventArgs e)
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

    private void FrmDashBoard_Shown(object sender, EventArgs e)
    {
        HearderMenuList.Visible = true;
        var result = ObjGlobal.LogInUserCategory switch
        {
            "ORDER" => new FrmTablesList(),
            "TERMINAL" => new FrmPSalesInvoice(),
            _ => new Form()
        };
        if (result.Text.IsValueExits())
        {
            result.ShowDialog();
        }
    }

    private void MdiMrSolution_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (SearchPanel.Visible)
        {
            return;
        }
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            //popupMenu1.ShowPopup(e.Location);
            //m_poperContainerForForm.Show(this, e.Location);
        }

        base.OnMouseClick(e);
    }

    #endregion --------------- Form ---------------


    // MENU CLICK EVENTS
    #region --------------- MENU CLICK ---------------
    private void DashBoardToolStripMenuItem2_Click(object sender, EventArgs e)
    {
        //var result = new FrmEmplloyee
        //{
        //    MdiParent = this
        //};
        //result.Show();
    }

    private void MnuRackSetup_Click(object sender, EventArgs e)
    {
        var rack = new FrmRack(false)
        {
            MdiParent = this
        };
        rack.Show();
    }

    private void MnuSalesNormalInvoice_Click(object sender, EventArgs e)
    {
    }

    private void MnuNormalPurchaseInvoice_Click(object sender, EventArgs e)
    {
        var entry = new FrmPurchaseInvoiceEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void DocumentNumberingToolStripMenuItem1_Click_1(object sender, EventArgs e)
    {
    }

    private void HMnuRegisterReport_Click(object sender, EventArgs e)
    {
    }

    private void DocumentReNumberingToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    private void PurchaseTermToolStripMenuItem1_Click(object sender, EventArgs e)
    {
    }

    private void SalesTermToolStripMenuItem1_Click(object sender, EventArgs e)
    {
    }

    private void BranchToolStripMenuItem3_Click(object sender, EventArgs e)
    {
    }

    private void UnitToolStripMenuItem2_Click(object sender, EventArgs e)
    {
    }

    private void MnuProductGroupList_Click_1(object sender, EventArgs e)
    {

        var list = new FrmProductList(@"LIST")
        {
            MdiParent = this
        };
        list.Show();
    }

    private void MnuProductSubGroupList_Click_1(object sender, EventArgs e)
    {
        GetMasterList.GetProductSubGroups("VIEW");
        //var list = new frmsub  FrmProductGroupList(@"LIST")
        //{
        //    MdiParent = this
        //};
        //list.Show(this);
    }

    private void MnuProductUnitList_Click_1(object sender, EventArgs e)
    {

    }

    private void MnuGodownList_Click_1(object sender, EventArgs e)
    {

    }

    private void MnuCostCenterList_Click_1(object sender, EventArgs e)
    {
    }

    [Obsolete]
    private async void MnuExit_Click(object sender, EventArgs e)
    {
        try
        {
            var result = CustomMessageBox.Question(@"DO YOU WANT TO CLOSED THE APPLICATION");
            if (result is not DialogResult.Yes) return;
            UserTerminated();
            var analyticService = new AnalyticsService();
            await analyticService.CleanUpAsync();
            Application.Exit();
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void MnuPovCashBankVoucher_Click(object sender, EventArgs e)
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

    private void MnuProvJournalVoucher_Click(object sender, EventArgs e)
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

    private void MnuProvisionDebitNotes_Click(object sender, EventArgs e)
    {
        //if (ObjGlobal.UserAuthorized)
        //    new FrmDebitNotes(false, string.Empty, true).ShowDialog();
        //else
        //    MessageBox.Show(@"YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //        MessageBoxIcon.Warning);
    }

    private void MnuProvisionCreditNotes_Click(object sender, EventArgs e)
    {
        //if (ObjGlobal.UserAuthorized)
        //    new FrmCreditNotes(false, string.Empty, true).ShowDialog();
        //else
        //    MessageBox.Show("YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
        //        MessageBoxIcon.Warning);
    }

    private void MnuProvisionPurchaseReturn_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.UserAuthorized)
            new FrmPurchaseReturnEntry(false, string.Empty).ShowDialog();
        else
            MessageBox.Show(@"YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    private void MnuPovSalesReturn_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.UserAuthorized)
        {
            new FrmSalesReturnEntry(false, string.Empty).ShowDialog();
        }
        else
            MessageBox.Show(@"YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    private void MnuPovPurchaseInvoice_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.UserAuthorized)
            new FrmPurchaseInvoiceEntry(false, string.Empty, true).ShowDialog();
        else
            MessageBox.Show(@"YOU ARE NOT A AUTHORIZED USER ..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    private void MnuSMSConfig_Click(object sender, EventArgs e)
    {
        new FrmSMSConfig().ShowDialog();
    }

    private void MnuUserControl_Click(object sender, EventArgs e)
    {
        new FrmUserConfig().ShowDialog();
    }

    private void MnuJournalVoucherMultiCurrency_Click(object sender, EventArgs e)
    {
        var entry = new FrmJournalVoucherEntry(false, string.Empty, false, true)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuJournalVoucherEntry_Click(object sender, EventArgs e)
    {
        var entry = new FrmJournalVoucherEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuCashBankMultiCurrency_Click(object sender, EventArgs e)
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
    private void MnuCurrencyExchange_Click(object sender, EventArgs e)
    {
        var result = new FrmCurrencyExchange();
        result.Show();
    }

    private void MnuPovSalesInvoice_Click(object sender, EventArgs e)
    {

    }

    private void MnuProduction_Click(object sender, EventArgs e)
    {
        new FrmProduction(false, string.Empty)
        {
            MdiParent = this
        }.Show();
    }

    private void MnuCopyMaster_Click(object sender, EventArgs e)
    {

    }

    private void MnuLocalImport_Click(object sender, EventArgs e)
    {
        var result = new FrmImportData(@"LOCAL");
        result.ShowDialog();
    }

    private void MnuSecondaryServer_Click(object sender, EventArgs e)
    {
        var result = new FrmImportData("SECONDARY");
        result.ShowDialog();
    }

    private void MnuJobCardManagement_Click(object sender, EventArgs e)
    {
        var result = new MDIServiceDashboard();
        result.ShowDialog();
    }

    private void MnuBarcodePrint_Click(object sender, EventArgs e)
    {
        var result = new FrmBarCode
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuCounterProductMini_Click(object sender, EventArgs e)
    {
        var result = new FrmCProduct();
        result.ShowDialog();
    }

    private void MnuDatabaseReset_Click(object sender, EventArgs e)
    {
        var login = new FrmLockScreen(false, @"RESET");
        login.ShowDialog();
        if (login.DialogResult is DialogResult.Yes)
        {
            var result = new FrmDataBaseReset();
            result.ShowDialog();
        }
    }

    private void MnuMemberShipType_Click(object sender, EventArgs e)
    {
        new FrmMemberShip
        {
            MdiParent = this
        }.Show();
    }

    private void MnuMemberType_Click(object sender, EventArgs e)
    {
        new FrmMemberType(false)
        {
            MdiParent = this
        }.Show();
    }

    private void MnuGiftVoucher_Click(object sender, EventArgs e)
    {
        var result = new FrmGiftVoucher();
        result.ShowDialog();
    }

    private void MnuSingleBankEntry_Click(object sender, EventArgs e)
    {
        new FrmCashBankEntry(false, string.Empty, "Bank")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuSingleCashEntry_Click_1(object sender, EventArgs e)
    {
        var entry = new FrmCashBankEntry(false, string.Empty, @"CASH")
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuSingleJournalVoucher_Click(object sender, EventArgs e)
    {
        var entry = new FrmJournalVoucherEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    [Obsolete("Obsolete")]
    private void MnuCompanyCreate_Click(object sender, EventArgs e)
    {
        var frmSetup = new FrmCompanySetup(false);
        frmSetup.ShowDialog();
        if (frmSetup.IsSetupSuccess)
        {
            var result = new FrmLogin();
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {
                CompanyLogin();
                BindFooterDetails();
            }
        }
    }

    private void MnuBranchCreate_Click(object sender, EventArgs e)
    {
        var result = new FrmBranchSetup(false);
        result.ShowDialog();
    }

    private void MnuCompanyUnitCreate_Click(object sender, EventArgs e)
    {
        var result = new FrmCompanyUnitSetup();
        result.ShowDialog();
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
    private void MnuCompanyList_Click(object sender, EventArgs e)
    {
        try
        {
            string[] dxform = { "FrmWait", "FrmWaitForm", "PleaseWait", "DXSplashScreen", "FrmSplashScreen" };
            string[] mainForm = { "MdiMrSolution", "FrmDashBoard" };
            if (ObjGlobal.CheckOpened())
                for (var i = 0; i < Application.OpenForms.Count; i++)
                {
                    if (mainForm.Contains(Application.OpenForms[i].Name)) continue;
                    if (dxform.Contains(Application.OpenForms[i].Name))
                    {
                        SplashScreenManager.CloseForm(false);
                    }
                    else
                    {
                        Application.OpenForms[i].Dispose();
                    }

                }

            var result = new FrmCompanyList(true);
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {
                BranchLogin();
                BindFooterDetails();
            }
        }
        catch (Exception ex)
        {
            ex.DialogResult();
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    [Obsolete]
    private void MnuBranchList_Click(object sender, EventArgs e)
    {
        new FrmBranchList().ShowDialog();
        BindFooterDetails();
    }

    [Obsolete]
    private void MnuCompanyUnitList_Click(object sender, EventArgs e)
    {
        var control = new FrmCompanyUnitList();
        control.ShowDialog();
        if (control.DialogResult is DialogResult.OK)
        {
            BindFooterDetails();
        }
    }

    private void MnuUserRoleSetup_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.UserAuthorized)
        {
            new FrmUserRoleSetup().ShowDialog();
        }
        else
        {
            CustomMessageBox.Warning(@"YOU ARE NOT A VALID USER FOR THIS FUNCTION..!!");
        }
    }

    private void MnuUserSetup_Click(object sender, EventArgs e)
    {
        if (!ObjGlobal.UserAuthorized)
        {
            new FrmUserSetup().ShowDialog();
        }
        else
        {
            CustomMessageBox.Warning(@"YOU ARE NOT A VALID USER FOR THIS FUNCTION..!!");
        }
    }

    private void MnuMenuRights_Click(object sender, EventArgs e)
    {
        var result = new About.FrmUserRight();
        result.ShowDialog();
    }

    private void MnuCompanyRights_Click(object sender, EventArgs e)
    {
        var control = new FrmCompanyRights();
        control.ShowDialog();
    }

    private void MnuBranchRights_Click(object sender, EventArgs e)
    {
        var control = new FrmBranchRights();
        control.ShowDialog();
    }

    private void MnuCompanyUnitRights_Click(object sender, EventArgs e)
    {
        var control = new FrmCompanyUnitRights();
        control.ShowDialog();
    }

    private void MnuChangePassword_Click(object sender, EventArgs e)
    {
        new FrmChangePassword().ShowDialog();
    }

    private void MnuDocumentNumbering_Click(object sender, EventArgs e)
    {
        var dialog = new FrmDocumentNumbering();
        dialog.ShowDialog();
    }

    private void MnuDocumentReNumbering_Click(object sender, EventArgs e)
    {
        var control = new FrmDocumentReNumbering();
        control.ShowDialog();
    }

    private void MnuPurchaseTerm_Click(object sender, EventArgs e)
    {
        new FrmPurchaseBillingTerm("PB").ShowDialog();
    }

    private void MnuSalesTerm_Click(object sender, EventArgs e)
    {
        new FrmSalesBillingTerm("SB").ShowDialog();
    }

    private void MnuPurchaseReturnTerm_Click(object sender, EventArgs e)
    {
        new FrmPurchaseBillingTerm("PR").ShowDialog();
    }

    private void MnuSalesReturnTerm_Click(object sender, EventArgs e)
    {
        new FrmSalesBillingTerm("SR").ShowDialog();
    }

    private void MnuPrintDesignAdd_Click(object sender, EventArgs e)
    {
        new FrmPrintDesignAdd().ShowDialog();
    }

    private void MnuPrintDesignSetting_Click(object sender, EventArgs e)
    {
        new FrmPrintDesignSetting { MdiParent = this }.Show();
    }

    [Obsolete("Obsolete")]
    private void MnuLogOut_Click(object sender, EventArgs e)
    {
        UserTerminated();
        CleanLoginInformation();
        var result = new FrmLogin();
        result.ShowDialog();
        if (result.DialogResult == DialogResult.OK)
        {
            CompanyLogin();
            BindFooterDetails();
            MenuAccessControl();
            var service = new AnalyticsService();
            service.CleanUpAsync();
        }
    }

    private void MnuAccountGroup_Click(object sender, EventArgs e)
    {
        var group = new FrmAccountGroup
        {
            MdiParent = this
        };
        group.Show();
    }

    private void MnuAccountSubGroup_Click(object sender, EventArgs e)
    {
        var group = new FrmAccountSubGroup
        {
            MdiParent = this
        };
        group.Show();
    }

    private void MnuGeneralLedger_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.ReturnInt(ObjGlobal.SysCurrencyId.ToString()) > 0)
        {
            var ledger = new FrmGeneralLedger
            {
                MdiParent = this
            };
            ledger.Show();
        }
        else
        {
            CustomMessageBox.Warning(@"DEFAULT CURRENCY IS NOT TAG ON SYSTEM CONFIG..!!");
            var dialog = new FrmSystemSettings();
            dialog.ShowDialog();
        }
    }

    private void MnuLedgerLockSetup_Click(object sender, EventArgs e)
    {
        var result = new FrmLedgerLock();
        result.ShowDialog();
    }

    private void MnuSubLedger_Click(object sender, EventArgs e)
    {
        var ledger = new FrmSubLedger
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuSeniorAgent_Click(object sender, EventArgs e)
    {
        var agent = new FrmSeniorAgent
        {
            MdiParent = this
        };
        agent.Show();
    }

    private void MnuSubAgent_Click(object sender, EventArgs e)
    {
        var agent = new FrmJuniorAgent
        {
            MdiParent = this
        };
        agent.Show();
    }

    private void MnuMainArea_Click(object sender, EventArgs e)
    {
        var area = new FrmMainArea(false)
        {
            MdiParent = this
        };
        area.Show();
    }

    private void MnuSubArea_Click(object sender, EventArgs e)
    {
        var area = new FrmSubArea
        {
            MdiParent = this
        };
        area.Show();
    }

    private void MnuCounter_Click(object sender, EventArgs e)
    {
        var name = new FrmCounterName
        {
            MdiParent = this
        };
        name.Show();
    }

    private void MnuFloor_Click(object sender, EventArgs e)
    {
        var floor = new FrmFloor
        {
            MdiParent = this
        };
        floor.Show();
    }

    private void MnuRoom_Click(object sender, EventArgs e)
    {
        new FrmRoom
        {
            MdiParent = this
        }.Show();
    }

    private void MnuTable_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.CreateTable();
    }

    private void MnuGodown_Click(object sender, EventArgs e)
    {
        new FrmGodownName(false) { MdiParent = this }.Show();
    }

    private void MnuWareHouse_Click(object sender, EventArgs e)
    {
        new FrmWareHouse(false) { MdiParent = this }.Show();
    }

    private void MnuDepartment_Click(object sender, EventArgs e)
    {
        new FrmDepartmentSetup { MdiParent = this }.Show();
    }

    private void MnuClass_Click(object sender, EventArgs e)
    {
        new FrmClass { MdiParent = this }.Show();
    }

    private void MnuSection_Click(object sender, EventArgs e)
    {
    }

    private void MnuCostCenter_Click(object sender, EventArgs e)
    {
        var centre = new FrmCostCentre(false) { MdiParent = this };
        centre.Show();
    }

    private void MnuColastar_Click(object sender, EventArgs e)
    {
        var colastar = new FrmColastar
        {
            MdiParent = this
        };
        colastar.Show();
    }

    private void MnuProductGroup_Click(object sender, EventArgs e)
    {
        var productGroup = new FrmProductGroup
        {
            MdiParent = this
        };
        productGroup.Show();
    }

    private void MnuProductSubGroup_Click(object sender, EventArgs e)
    {
        var subGroup = new FrmProductSubGroup
        {
            MdiParent = this
        };
        subGroup.Show();
    }

    private void MnuMenu_Click(object sender, EventArgs e)
    {
        var menu = new FrmProductGroup
        {
            MdiParent = this
        };
        menu.Show();
    }

    private void MnuSubMenuItem_Click(object sender, EventArgs e)
    {
        var show = new FrmProductSubGroup
        {
            MdiParent = this
        };
        show.Show();
    }

    private void MnuProductUnit_Click(object sender, EventArgs e)
    {
        var unit = new FrmProductUnit
        {
            MdiParent = this
        };
        unit.Show();
    }

    private void MnuProduct_Click(object sender, EventArgs e)
    {
        var product = new FrmProduct(false)
        {
            MdiParent = this
        };
        product.Show();
    }

    private void MnuSparePartsImport_Click(object sender, EventArgs e)
    {
        var result = new FrmSparePartsImport();
        result.ShowDialog();
    }

    private void MnuCounterProduct_Click(object sender, EventArgs e)
    {
        var product = new FrmPosProduct(false)
        {
            MdiParent = this
        };
        product.Show();
    }

    private void MnuRestaurantProduct_Click(object sender, EventArgs e)
    {
        var product = new FrmRestaurantProduct(false)
        {
            MdiParent = this
        };
        product.Show();
    }

    private void MnuMovie_Click(object sender, EventArgs e)
    {
        new FrmMovie
        {
            MdiParent = this
        }.Show();
    }

    private void MnuProductMapping_Click(object sender, EventArgs e)
    {
        new FrmProductMapping
        {
            MdiParent = this
        }.Show();
    }

    private void MnuProductLock_Click(object sender, EventArgs e)
    {
        new FrmProductLock
        {
            MdiParent = this
        }.Show();
    }

    private void MnuProductUpdate_Click(object sender, EventArgs e)
    {
        new FrmProductUpdate
        {
            MdiParent = this
        }.Show();
    }

    private void MnuCurrency_Click(object sender, EventArgs e)
    {
        new FrmCurrency { MdiParent = this }.Show();
    }

    private void MnuCurrencyRate_Click(object sender, EventArgs e)
    {
        var list = new FrmCurrencyRateList();
        list.MdiParent = this;
        list.Show();
    }

    private void MnuNarration_Click(object sender, EventArgs e)
    {
        new FrmNarrationMaster(true) { MdiParent = this }.Show();
    }

    private void MnuRemarks_Click(object sender, EventArgs e)
    {
        new FrmNarrationMaster(true) { MdiParent = this }.Show();
    }

    private void MnuLedgerOpening_Click(object sender, EventArgs e)
    {
        var result = new FrmLedgerImportFromExcel()
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuProductOpening_Click(object sender, EventArgs e)
    {
        var result = new FrmProductOpeningFromExcel()
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuLedgerOpeningEntry_Click(object sender, EventArgs e)
    {
        new FrmLedgerOpeningEntry
        {
            MdiParent = this
        }.Show();
    }

    private void MnuLedgerBillWiseOpeningEntry_Click(object sender, EventArgs e)
    {
        new FrmLedgerOpeningBillWiseEntry(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuProductOpeningEntry_Click(object sender, EventArgs e)
    {
        new FrmProductOpeningEntry(false, string.Empty)
        {
            MdiParent = this
        }.Show();
    }

    private void MnuProductListWiseOpeningEntry_Click(object sender, EventArgs e)
    {
    }

    private void MnuProductOpeningImport_Click(object sender, EventArgs e)
    {
        var result = new FrmProductOpeningFromExcel()
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuLedgerOpeningImport_Click(object sender, EventArgs e)
    {
        var result = new FrmLedgerImportFromExcel()
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuPurchaseIndent_Click(object sender, EventArgs e)
    {
        var entry = new FrmPurchaseIndentEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuPurchaseOrder_Click(object sender, EventArgs e)
    {
        var entry = new FrmPurchaseOrderEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuPurchaseGoodsInTransit_Click(object sender, EventArgs e)
    {
        var entry = new FrmPurchaseGoodsInTransitEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuPurchaseChallan_Click(object sender, EventArgs e)
    {
        var entry = new FrmPurchaseChallanEntry(false, string.Empty)
        {
            MdiParent = this
        };
        entry.Show();
    }

    private void MnuPurchaseChallanReturnEntry_Click(object sender, EventArgs e)
    {
        var result = new FrmPurchaseReturnChallanEntry(false, string.Empty)
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuPurchaseInvoice_Click(object sender, EventArgs e)
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

    private void MnuPurchaseAvtInvoice_Click(object sender, EventArgs e)
    {
        // new FrmPurchaseAbbreviateInvoice(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuPurchaseAdditional_Click(object sender, EventArgs e)
    {
        var result = new FrmPurchaseAdditional(false, string.Empty, "ADDITIONAL")
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuPurchaseReturn_Click(object sender, EventArgs e)
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

    private void MnuPurchaseExpBrk_Click(object sender, EventArgs e)
    {
        //new FrmPurchaseExpBrk(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuSalesQuotation_Click(object sender, EventArgs e)
    {
        new FrmSalesQuotationEntry(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuSalesOrder_Click(object sender, EventArgs e)
    {
        new FrmSalesOrderEntry(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuSalesChallan_Click(object sender, EventArgs e)
    {
        new FrmSalesChallanEntry(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuEstimateNormalInvoice_Click(object sender, EventArgs e)
    {
        var result = new FrmEstimateSalesInvoiceEntry(false, string.Empty);
        result.ShowDialog();
    }

    private void MnuSalesInvoice_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.SalesLedgerId > 0)
        {
            if (ObjGlobal.IsIrdRegister && DateTime.Now.Date < ObjGlobal.CompanyLoginDateTime.Date)
            {
                CustomMessageBox.Warning(@"YOU ARE USING SOFTWARE IN BACK DATE ..!!");
                return;
            }
            var result = new FrmSalesInvoiceEntry(false, string.Empty)
            {
                MdiParent = this
            };
            result.Show();
        }
        else
        {
            CustomMessageBox.Warning(@"DEFAULT SALES A/C IS NOT TAG ON SYSTEM CONFIG..!!");
            var result = new FrmSystemConfiguration();
            result.ShowDialog();
            if (result.DialogResult == DialogResult.OK)
            {

            }
        }
    }

    private void MnuPointOfSalesInvoice_Click(object sender, EventArgs e)
    {
        //new FrmAbbreviatedTaxInvoice(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuSalesAbbreviateInvoice_Click(object sender, EventArgs e)
    {
        //new FrmAbbreviatedTaxInvoice(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuCompanySetup_Click(object sender, EventArgs e)
    {
        new FrmCompanySetup(false) { MdiParent = this }.Show();
    }

    private void MnuSalesReturn_Click(object sender, EventArgs e)
    {
        if (ObjGlobal.SalesReturnLedgerId > 0)
        {
            new FrmSalesReturnEntry(false, string.Empty) { MdiParent = this }.Show();
        }
        else
        {
            MessageBox.Show(@"DEFAULT SALES  RETURN A/C IS NOT TAG ON SYSTEM CONFIG..!!", ObjGlobal.Caption,
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            new FrmSystemConfiguration().ShowDialog();
        }
    }

    private void MnuSalesExpBrk_Click(object sender, EventArgs e)
    {
    }

    private void MnuGodownTransfer_Click(object sender, EventArgs e)
    {
        //new FrmGodownTransfer(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuStockAdjustment_Click(object sender, EventArgs e)
    {
        new FrmStockAdjustment(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuPhysicalStock_Click(object sender, EventArgs e)
    {
        var result = new FrmPhysicalStockEntry(false, "");
        result.ShowDialog();
    }

    private void MnuExpBrk_Click(object sender, EventArgs e)
    {
        //new FrmExpBrk(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuBillOfMaterials_Click(object sender, EventArgs e)
    {
        new FrmBillOfMaterials(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuRawMaterialIssue_Click(object sender, EventArgs e)
    {
        new FrmRawMaterialIssue(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuRawMaterialReturn_Click(object sender, EventArgs e)
    {
        new FrmRawMaterialReturn(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuFinishedGoodReceived_Click(object sender, EventArgs e)
    {
        new FrmFinishedGoodsReceived(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuFinishedGoodsReturn_Click(object sender, EventArgs e)
    {
        new FrmFinishedGoodsReturn(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuDoctor_Click(object sender, EventArgs e)
    {
        new FrmDoctor(false) { MdiParent = this }.Show();
    }

    private void MnuHDepartment_Click(object sender, EventArgs e)
    {
        new FrmHDepartment(false) { MdiParent = this }.Show();
    }

    private void MnuWard_Click(object sender, EventArgs e)
    {
        new FrmWard(false) { MdiParent = this }.Show();
    }

    private void MnuBedType_Click(object sender, EventArgs e)
    {
        new FrmBedType(false) { MdiParent = this }.Show();
    }

    private void ToolStripMenuItem4_Click(object sender, EventArgs e)
    {
        new FrmBedNo(false) { MdiParent = this }.Show();
    }

    private void MnuBedNo_Click(object sender, EventArgs e)
    {
        new FrmBedNo(false) { MdiParent = this }.Show();
    }

    private void MnuHosProductGroup_Click(object sender, EventArgs e)
    {
        new FrmProductGroup { MdiParent = this }.Show();
    }

    private void MnuHOSProductSubGroup_Click(object sender, EventArgs e)
    {
        new FrmProductSubGroup { MdiParent = this }.Show();
    }

    private void MnuItem_Click(object sender, EventArgs e)
    {
        new FrmHospitalServices(false) { MdiParent = this }.Show();
    }

    private void MnuPatientRegistration_Click(object sender, EventArgs e)
    {
        new FrmPatient(false) { MdiParent = this }.Show();
    }

    private void MnuPatient_Click(object sender, EventArgs e)
    {
        new FrmPatient(false) { MdiParent = this }.Show();
    }

    private void MnuIPDPatient_Click(object sender, EventArgs e)
    {
        //new FrmIPDPaitent(false) { MdiParent = this }.Show();
    }

    private void MnuOPDBilling_Click(object sender, EventArgs e)
    {
        //new FrmOPDInvoice(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuIPDBilling_Click(object sender, EventArgs e)
    {
        //new FrmIPDBilling(false) { MdiParent = this }.Show();
    }

    private void MnuCashReceipt_Click(object sender, EventArgs e)
    {
        //new FrmCashReceipt(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuRefund_Click(object sender, EventArgs e)
    {
        //new FrmRefund(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuTableList_Click(object sender, EventArgs e)
    {
        while (true)
        {
            if (ObjGlobal.SysIsNightAudit)
            {
                var exits = _salesEntry.IsNightAuditExits(DateTime.Now.GetDateString());
                if (exits.Rows.Count == 0)
                {
                    var isAudit = _salesEntry.ReturnLastNightAuditLog();
                    if (isAudit.Rows.Count > 0)
                    {
                        var audit = isAudit.Rows[0]["IsAudited"].GetBool();
                        if (audit)
                        {
                            SaveNightAudit();
                            continue;
                        }
                        else
                        {
                            if (DateTime.Now.TimeOfDay > ObjGlobal.NightAuditEndTime)
                            {
                                var nightLog = new FrmNightAuditLog();
                                nightLog.ShowDialog();
                                if (nightLog.DialogResult == DialogResult.OK)
                                {
                                    continue;
                                }
                                else
                                {
                                    CustomMessageBox.Warning(@"NIGHT AUDIT IS REMAIN TO CLOSED..!!");
                                    return;
                                }
                            }
                            else
                            {
                                var result = new FrmTablesList();
                                result.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        SaveNightAudit();
                        continue;
                    }
                }
                else
                {
                    ObjGlobal.NightAuditDate = exits.Rows[0]["AuditDate"].GetDateTime();
                    var result = new FrmTablesList();
                    result.ShowDialog();
                }
            }
            else
            {
                var result = new FrmTablesList();
                result.ShowDialog();
            }

            break;
        }
    }

    private void MnuRestaurantOrder_Click(object sender, EventArgs e)
    {
        //new FrmRestroOrder { MdiParent = this }.Show();
    }

    private void MnuPurchaseInvoicePOS_Click(object sender, EventArgs e)
    {
        //new FrmPOSPurchase(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuPurchaseAvt_Click(object sender, EventArgs e)
    {
        //new FrmPurchaseAbbreviateInvoice(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuPointOfSales_Click(object sender, EventArgs e)
    {
        var dialogResult = new FrmSalesSetting();
        if (ObjGlobal.SalesVatTermId is 0)
        {
            CustomMessageBox.Warning(@"SALES VAT TERM ID NOT TAG IN SALES SETTING..!!");
            dialogResult.ShowDialog();
            return;
        }

        if (ObjGlobal.SalesDiscountTermId is 0)
        {
            CustomMessageBox.Warning(@"SALES DISCOUNT TERM IS NOT TAG IN SALES SETTING..!!");
            dialogResult.ShowDialog();
            return;
        }

        if (ObjGlobal.SalesSpecialDiscountTermId is 0)
        {
            CustomMessageBox.Warning(@"SALES SPECIAL DISCOUNT TERM NOT TAG IN SYSTEM SETTING..!!");
            dialogResult.ShowDialog();
            return;
        }

        var result = new FrmPSalesInvoice();
        result.ShowDialog();
    }

    private void MnuPosSalesReturn_Click(object sender, EventArgs e)
    {
        //new FrmDepartmentReturnBill { MdiParent = this }.Show();
    }

    private void MnuSalesAbbreviate_Click(object sender, EventArgs e)
    {
        //new FrmAbbreviatedTaxInvoice(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuCashBook_Click(object sender, EventArgs e)
    {
        new FrmCashBook { MdiParent = this }.Show();
    }

    private void MnuCashFlow_Click(object sender, EventArgs e)
    {
        new FrmCashFlowStatement { MdiParent = this }.Show();
    }

    private void MnuBankReconcile_Click(object sender, EventArgs e)
    {
        new FrmBankReconciliationStatement { MdiParent = this }.Show();
    }

    private void MnuDayBook_Click(object sender, EventArgs e)
    {
        var report = new FrmDayBook
        {
            MdiParent = this
        };
        report.Show();
    }

    private void MnuJournalVoucherReport_Click(object sender, EventArgs e)
    {
        var report = new FrmJournalBook
        {
            MdiParent = this
        };
        report.Show();
    }

    private void MnuLedgerReport_Click(object sender, EventArgs e)
    {
        var ledger = new FrmLedger(string.Empty);
        ledger.MdiParent = this;
        ledger.Show();
    }

    private void MnuOpeningTrialBalance_Click(object sender, EventArgs e)
    {
        var balance = new FrmTrialBalance(@"OB")
        {
            MdiParent = this
        };
        balance.Show();
    }

    private void MnuTrialBalance_Click(object sender, EventArgs e)
    {
        new FrmTrialBalance("TB") { MdiParent = this }.Show();
    }

    private void MnuTrialBalancePeriodic_Click(object sender, EventArgs e)
    {
        new FrmTrialBalance("PTB") { MdiParent = this }.Show();
    }

    private void MnuNormalTradingAccountReport_Click(object sender, EventArgs e)
    {
        new FrmProfitNLoss("PL")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuPeriodicTradingAccountReport_Click(object sender, EventArgs e)
    {
        new FrmProfitNLoss("PPL")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuProfitNLoss_Click(object sender, EventArgs e)
    {
        new FrmProfitNLoss("PL")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuProfitAndLossPeriodic_Click(object sender, EventArgs e)
    {
        new FrmProfitNLoss("PPL")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuOpeningBalanceSheet_Click(object sender, EventArgs e)
    {
        new FrmBalanceSheet("OB")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuBalanceSheet_Click(object sender, EventArgs e)
    {
        new FrmBalanceSheet("BS")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuBalanceSheetPeriodic_Click(object sender, EventArgs e)
    {
        new FrmBalanceSheet("PBS")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuFundNetPosition_Click(object sender, EventArgs e)
    {
        new FrmFundNetPosition { MdiParent = this }.Show();
    }

    private void MnuCBDocumentPrinting_Click(object sender, EventArgs e)
    {
        new FrmDocumentPrint("Document Printing - Cash/Bank Voucher", "CB", string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, string.Empty)
        { MdiParent = this }.Show();
    }

    private void MnuJVDocumentPrinting_Click(object sender, EventArgs e)
    {
        new FrmDocumentPrint("Document Printing - Journal Voucher", "JV", string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, string.Empty)
        { MdiParent = this }.Show();
    }

    private void MnuDNDocumentPrinting_Click(object sender, EventArgs e)
    {
        new FrmDocumentPrint("Document Printing - Debit Note Voucher", "DN", string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, string.Empty)
        { MdiParent = this }.Show();
    }

    private void MnuCNDocumentPrinting_Click(object sender, EventArgs e)
    {
        new FrmDocumentPrint("Document Printing - Credit Note Voucher", "CN", string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, ObjGlobal.SysDefaultPrinter, string.Empty)
        { MdiParent = this }.Show();
    }

    private void MnuDocumentPrinting_Click(object sender, EventArgs e)
    {
        new FrmDocumentPrint { MdiParent = this }.Show();
    }

    // PURCHASE REGISTER MENU EVENTS

    private void MnuPurchaseInvoiceRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmPurchaseRegister(((ToolStripMenuItem)sender).Tag.ToString())
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuPurchaseAdditionalRegister_Click(object sender, EventArgs e)
    {
        new FrmPurchaseAdditionalRegister
        {
            MdiParent = this
        }.Show();
    }

    //SALES REGISTER MENU EVENTS
    private void MnuSalesQuotationRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesInvoiceEntryRegister(@"SQ")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuSalesOrderRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesInvoiceEntryRegister(@"SO")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuSalesChallanRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesInvoiceEntryRegister(@"SC")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuSalesInvoiceRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesInvoiceEntryRegister(@"SB")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuTicketSalesRegister_Click(object sender, EventArgs e)
    {
        new FrmTicketSalesRegister
        {
            MdiParent = this
        }.Show();
    }

    private void MnuSalesAdditionalRegister_Click(object sender, EventArgs e)
    {
        new FrmSalesAdditionalRegister
        {
            MdiParent = this
        }.Show();
    }

    private void MnuSalesReturnRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesInvoiceEntryRegister(@"SR")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void ExpBrkToolStripMenuItem1_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesInvoiceEntryRegister(@"SEB")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void YearToDateToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new FrmPurchaseAnalysis { MdiParent = this }.Show();
    }

    private void PurchasePeriodicToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new FrmPurchaseAnalysisPerodic { MdiParent = this }.Show();
    }

    private void PurchaseTop10WiseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new FrmPurchaseProductTopNwise { MdiParent = this }.Show();
    }

    private void MnuSalesAnalysis_Click(object sender, EventArgs e)
    {
        new FrmSalesAnalysis { MdiParent = this }.Show();
    }

    private void MnuSalesAnalysisPeriodic_Click(object sender, EventArgs e)
    {
        new FrmSalesAnaylsisPerodic { MdiParent = this }.Show();
    }

    private void MnuTOPTenSalesProduct_Click(object sender, EventArgs e)
    {
        new FrmSalesProductTopNwise { MdiParent = this }.Show();
    }

    private void MnuDebitNoteRegister_Click(object sender, EventArgs e)
    {
        new FrmDebitNoteRegister { MdiParent = this }.Show();
    }

    private void MnuCreditNoteRegister_Click(object sender, EventArgs e)
    {
        new FrmCreditNoteRegister { MdiParent = this }.Show();
    }

    private void MnuNormalPaymentReceipt_Click(object sender, EventArgs e)
    {
        new FrmReceiptPaymentRegister { MdiParent = this }.Show();
    }

    private void MnuVendorReceiptPaymentRegister_Click(object sender, EventArgs e)
    {
        new FrmReceiptPaymentRegister { MdiParent = this }.Show();
    }

    private void MnuCustomerReceiptPaymentRegister_Click(object sender, EventArgs e)
    {
        new FrmReceiptPaymentRegister { MdiParent = this }.Show();
    }

    private void MnuCustomerLedgerReport_Click(object sender, EventArgs e)
    {
        var ledger = new FrmLedger(@"CUSTOMER")
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuVenderLedgerReport_Click(object sender, EventArgs e)
    {
        var ledger = new FrmLedger(@"VENDOR")
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuBothLedgerReport_Click(object sender, EventArgs e)
    {
        var ledger = new FrmLedger(@"BOTH")
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuCustomerLedgerReconcile_Click(object sender, EventArgs e)
    {
        var reconcile = new FrmPartyReconcile
        {
            MdiParent = this
        };
        reconcile.Show();
    }

    private void MnuVendorLedgerReconcile_Click(object sender, EventArgs e)
    {
        var reconcile = new FrmPartyReconcile
        {
            MdiParent = this
        };
        reconcile.Show();
    }

    private void MnuPartyReconcile_Click(object sender, EventArgs e)
    {
        var reconcile = new FrmPartyReconcile
        {
            MdiParent = this
        };
        reconcile.Show();
    }

    private void MnuLedgerReconcile_Click(object sender, EventArgs e)
    {
        var reconcile = new FrmLedgerReconcile
        {
            MdiParent = this
        };
        reconcile.Show();
    }

    private void MnuCustomerWiseAging_Click(object sender, EventArgs e)
    {
        var report = new FrmAgingReport
        {
            MdiParent = this
        };
        report.Show();
    }

    private void MnuVendorWiseAging_Click(object sender, EventArgs e)
    {
        var report = new FrmAgingReport(false)
        {
            MdiParent = this
        };
        report.Show();
    }

    private void MnuBothAgingReport_Click(object sender, EventArgs e)
    {
        var report = new FrmAgingReport
        {
            MdiParent = this
        };
        report.Show();
    }

    private void MnuPurchaseOrderOutStanding_Click(object sender, EventArgs e)
    {
        var order = new FrmOutstandingPurchaseOrder
        {
            MdiParent = this
        };
        order.Show();
    }

    private void MnuGoodsInTransit_Click(object sender, EventArgs e)
    {
        var order = new FrmOutstandingPurchaseOrder
        {
            MdiParent = this
        };
        order.Show();
    }

    private void MnuPurchaseChallanOutStanding_Click(object sender, EventArgs e)
    {
        var challan = new FrmOutstandingPurchaseChallan
        {
            MdiParent = this
        };
        challan.Show();
    }

    private void MnuVendorOutStanding_Click(object sender, EventArgs e)
    {
        var ledger = new FrmOutstandingPartyLedger(@"VENDOR")
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuSalesQuotationOutStanding_Click(object sender, EventArgs e)
    {
        var order = new FrmOutstandingSalesOrder
        {
            MdiParent = this
        };
        order.Show();
    }

    private void MnuSalesOrderOutStanding_Click(object sender, EventArgs e)
    {
        var order = new FrmOutstandingSalesOrder
        {
            MdiParent = this
        };
        order.Show();
    }

    private void MnuSalesChallanOutStanding_Click(object sender, EventArgs e)
    {
        var challan = new FrmOutstandingSalesChallan
        {
            MdiParent = this
        };
        challan.Show();
    }

    private void MnuCustomerOutStanding_Click(object sender, EventArgs e)
    {
        var ledger = new FrmOutstandingPartyLedger(@"CUSTOMER")
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuPartyConfirmationPrint_Click(object sender, EventArgs e)
    {
        var result = new FrmPartyConfirmation();
        result.ShowDialog();
    }

    private void MnuPurchaseVatRegister_Click(object sender, EventArgs e)
    {
        new FrmPurchaseVatRegister(@"PB")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuPurchaseReturnVatRegister_Click(object sender, EventArgs e)
    {
        new FrmPurchaseVatRegister(@"PR")
        {
            MdiParent = this
        }.Show();
    }

    private void MnuSalesVatRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesVatRegister(@"SB")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuSalesReturnVatRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmSalesVatRegister(@"SR")
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuVatRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmVatRegister
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuMaterializedViewReport_Click(object sender, EventArgs e)
    {
        var view = new FrmMaterializeView
        {
            MdiParent = this
        };
        view.Show();
    }

    private void MnuAuditTrial_Click(object sender, EventArgs e)
    {
        var trial = new FrmAuditTrial
        {
            MdiParent = this
        };
        trial.Show();
    }

    private void MnuEntryLogRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmEntryLogRegister
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuProfitabilityReport_Click(object sender, EventArgs e)
    {
        var result = new FrmProfitability
        {
            MdiParent = this
        };
        result.Show();
    }

    private void MnuStockLedger_Click(object sender, EventArgs e)
    {
        var ledger = new FrmStockLedger(false)
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuStockLedgerValue_Click(object sender, EventArgs e)
    {
        var ledger = new FrmStockLedger(true)
        {
            MdiParent = this
        };
        ledger.Show();
    }

    private void MnuStockValuation_Click(object sender, EventArgs e)
    {
        var valuation = new FrmStockValuation
        {
            MdiParent = this
        };
        valuation.Show();
    }

    private void ProductWiseGodownToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var product = new FrmGodownProduct
        {
            MdiParent = this
        };
        product.Show();
    }

    private void MnuGodownProduct_Click(object sender, EventArgs e)
    {
        var product = new FrmGodownProduct
        {
            MdiParent = this
        };
        product.Show();
    }

    private void MnuLandedCostRegister_Click(object sender, EventArgs e)
    {
        var register = new FrmLandedCostRegister
        {
            MdiParent = this
        };
        register.Show();
    }

    private void MnuProductWiseToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var costing = new FrmProductCosting
        {
            MdiParent = this
        };
        costing.Show();
    }

    private void BillWiseCostToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var costing = new FrmProductCosting
        {
            MdiParent = this
        };
        costing.Show();
    }

    private void MnuSystemCToolStripMenuItem_Click(object sender, EventArgs e)
    {
        //new FrmSystemConfiguration().ShowDialog();
        ////new FrmSystemConfiguration { MdiParent = this}.Show();
    }

    private void MnuBackUp_Click(object sender, EventArgs e)
    {
        var result = new FrmBackUpDataBase(@"BackUp");
        result.ShowDialog();
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

    private void ServerConnectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new FrmMultiServer(true);
        result.ShowDialog();
    }

    private void CompanyInfoToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new FrmAboutUs();
        result.ShowDialog();
    }

    private void MnuHelpFileUserManual_Click(object sender, EventArgs e)
    {
        var command = $@"{Application.StartupPath}\Help\MrSolutionUserManual.chm";
        if (File.Exists(command))
        {
            Help.ShowHelp(this, command);
        }
    }

    private void OpenTeamViewer_Click(object sender, EventArgs e)
    {
        var command = @"Help\TeamViewer.exe";
        if (File.Exists(command))
        {
            Process.Start(command);
        }
    }

    private void OpenAnyDesk_Click(object sender, EventArgs e)
    {
        var command = @"Help\AnyDesk.exe";
        if (File.Exists(command))
        {
            Process.Start(command);
        }
    }

    private void MnuMrSolutionViewer_Click(object sender, EventArgs e)
    {
        var command = @"Help\MrSolutionViewer.exe";
        if (File.Exists(command))
        {
            Process.Start(command);
        }
    }

    private void OnlineRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (!Debugger.IsAttached)
        {
            if (ObjGlobal.DomainLoginUser.Contains(ObjGlobal.LogInUser))
                new FrmLicenseGenerator().ShowDialog();
            else
                MessageBox.Show(@"YO DON'T HAVE RIGHTS FOR THIS MENU..!!", ObjGlobal.Caption);
        }
        else
        {
            var showDialog = new FrmLicenseGenerator();
            showDialog.ShowDialog(this);
        }
    }

    private void MnuButtonDashboard_Click(object sender, EventArgs e)
    {
    }

    private void MnuCostCenterExpenses_Click(object sender, EventArgs e)
    {
        var expenses = new FrmCostCenterExpenses(false, string.Empty)
        {
            MdiParent = this
        };
        expenses.Show();
    }

    private void MnuPartyOrder_Click(object sender, EventArgs e)
    {
        new FrmSalesOrderEntry(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuSampleCosting_Click(object sender, EventArgs e)
    {
        new FrmSampleCosting(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuProductionIssue_Click(object sender, EventArgs e)
    {
        new FrmRawMaterialIssue(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuProductReceived_Click(object sender, EventArgs e)
    {
        new FrmProductionReceived(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuCostCenterCenterIssue_Click(object sender, EventArgs e)
    {
        new FrmCostCenterIssue(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuCostCenterReceived_Click(object sender, EventArgs e)
    {
        new FrmCostCenterReceived(false, string.Empty) { MdiParent = this }.Show();
    }

    private void MnuProductionDashBoard_Click(object sender, EventArgs e)
    {
    }

    private void MnuDayClosingSystem_Click(object sender, EventArgs e)
    {
        var cash = new FrmCashClosing
        {
            MdiParent = this
        };
        cash.Show(this);
    }

    private void MnuReportTransaction_Click(object sender, EventArgs e)
    {
        var dialogResult = new XFrmRecalculate();
        dialogResult.ShowDialog();
    }

    private void MnuProductList_Click(object sender, EventArgs e)
    {
        var list = new FrmProductList(@"LIST")
        {
            MdiParent = this
        };
        list.Show();
    }

    private void MnuProductOpeningReport_Click(object sender, EventArgs e)
    {
        new FrmProductOpeningReport { MdiParent = this }.Show();
    }

    private void MnuProductScheme_Click(object sender, EventArgs e)
    {
        new FrmProductScheme().ShowDialog();
    }

    private void MnuGodownList_Click(object sender, EventArgs e)
    {
    }

    private void MnuColastorList_Click(object sender, EventArgs e)
    {
    }

    private void ProductListWithImageToolStripMenuItem_Click(object sender, EventArgs e)
    {
    }

    [Obsolete]
    private void MnFiscalYear_Click(object sender, EventArgs e)
    {
        var fiscalYear = new FrmFiscalYear();
        fiscalYear.ShowDialog(this);
        if (fiscalYear.LoginFiscalYearId > 0)
        {
            BindFooterDetails();
        }

    }

    private void MnuFiscalYearSetup_Click(object sender, EventArgs e)
    {
        var setup = new FrmFiscalYearSetup
        {
            MdiParent = this
        };
        setup.Show();
    }

    private void MnuSpecialProduct_Click(object sender, EventArgs e)
    {
        new FrmProductPreview { MdiParent = this }.Show();
    }

    [Obsolete]
    private void MnuExternalDeviceUtility_Click(object sender, EventArgs e)
    {
        //new FrmAttachDeAttach().ShowDialog();
        var result = new FrmExternalDeviceTools();
        result.ShowDialog();
    }

    private void MnuResDayClosing_Click(object sender, EventArgs e)
    {
        new FrmCashClosing { MdiParent = this }.Show();
    }

    private void ContactUsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        new FrmSoftwareInformation().ShowDialog();
    }

    private void MnuVersionUpdate_Click(object sender, EventArgs e)
    {
        // new FrmVersionUpdate().ShowDialog();
        AppUpdater.CheckUpdates(true);
    }

    private void MnuPosSalesInvoice_Click(object sender, EventArgs e)
    {
    }

    private void TsWebSite_Click(object sender, EventArgs e)
    {
        Process.Start(@"http://mrsolution.com.np/");
    }

    private void MnuRestroNew_Click(object sender, EventArgs e)
    {
        //new FrmRestroInvoiceNew().ShowDialog();
    }

    private void MnuPOSBilling_Click(object sender, EventArgs e)
    {
        var getMasterData = new ClsMasterSetup();
        var dtTerm = getMasterData.GetTagTermOnSalesValue();
        if (dtTerm.Rows.Count > 0)
            if (dtTerm.Rows[0]["SalesVat_Id"].GetInt() is 0 || dtTerm.Rows[0]["SalesDiscount_Id"].GetInt() is 0 ||
                dtTerm.Rows[0]["SalesSpecialDiscount_Id"].GetInt() is 0)
            {
                var msg = new StringBuilder();
                msg.AppendLine(@"Either Sales Vat or Sales Product Discount ");
                msg.AppendLine(@" or Sales Bill Discount not tag on System Configuration ");
                CustomMessageBox.Warning(msg.ToString());
                var result = new FrmSalesSetting();
                result.ShowDialog();
                return;
            }

        new FrmPSalesInvoice().ShowDialog();
    }

    private void MnuRestaurantDashBoard_Click(object sender, EventArgs e)
    {
        //new MDIRestro().ShowDialog();
    }

    private void MnuHospitalDashBoard_Click(object sender, EventArgs e)
    {
        var result = new MdiHospital();
        result.ShowDialog();
    }

    private void MnuShrinkDatabase_Click(object sender, EventArgs e)
    {
        try
        {
            SplashScreenManager.ShowForm(typeof(FrmWait));
            _recalculate.ShrinkDatabase(ObjGlobal.InitialCatalog);
            _recalculate.ShrinkDatabaseLog(ObjGlobal.InitialCatalog);
            CustomMessageBox.Information($@"{ObjGlobal.InitialCatalog} SHRINK DATABASE SUCCESSFULLY..!!");
            SplashScreenManager.CloseForm(false);
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
        }
    }

    private void MnuDecryptValue_Click(object sender, EventArgs e)
    {
        var result = new FrmLockScreen(false, @"DECRYPT");
        result.ShowDialog();
        if (result.DialogResult == DialogResult.Yes)
        {
            var api = new FrmViewEncryptValue();
            api.ShowDialog();
        }
    }
    [Obsolete]
    private void MnuDynamicReports_Click(object sender, EventArgs e)
    {
        var result = new FrmDynamicFinanceReports();
        result.Show();
    }

    private void MnuCashEntry_Click(object sender, EventArgs e)
    {
        if (!ObjGlobal.UserAuthorized)
            new FrmCashBankEntry(false, string.Empty, "CASH") { MdiParent = this }.Show();
        else
            MessageBox.Show(@"YOU ARE NOT A VALID USER FOR THIS FUNCTION..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    private void MnuBankEntry_Click(object sender, EventArgs e)
    {
        if (!ObjGlobal.UserAuthorized)
            new FrmCashBankEntry(false, string.Empty, "BANK") { MdiParent = this }.Show();
        else
            MessageBox.Show(@"YOU ARE NOT A VALID USER FOR THIS FUNCTION..!!", ObjGlobal.Caption, MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
    }

    private void MnuPDCEntry_Click(object sender, EventArgs e)
    {
        new FrmPDCVoucher(false, string.Empty).ShowDialog();
    }

    private void MnuDebitNotes_Click(object sender, EventArgs e)
    {
        var notes = new FrmVoucherNotesEntry(false, string.Empty)
        {
            MdiParent = this
        };
        notes.Show();
    }

    private void MnuCreditNotes_Click(object sender, EventArgs e)
    {
        //new FrmCreditNotes(false, string.Empty)
        //{
        //    MdiParent = this
        //}.Show();
    }

    private void MnuMoneyExchange_Click(object sender, EventArgs e)
    {
        var result = new FrmRemittance();
        result.ShowDialog();
    }

    private void MnuCurrencyPurchaseEntry_Click(object sender, EventArgs e)
    {
        var result = new FrmForeignCurrencyPurchase();
        result.ShowDialog();

    }

    private void MnuCurrencySalesEntry_Click(object sender, EventArgs e)
    {
        var result = new FrmForeignCurrencySales();
        result.ShowDialog();

    }

    private void MnuCurrencyExchangeEntry_Click(object sender, EventArgs e)
    {
        var result = new FrmCurrencyExchange();
        result.ShowDialog();
    }

    private void MnuVehicleDashBoard_Click(object sender, EventArgs e)
    {
        //new FrmVehicleDashBoard().ShowDialog();
    }

    private void MnuPOSDashBoard_Click(object sender, EventArgs e)
    {
        new MDIPOSDashboard().ShowDialog();
    }

    private void MnuFinishGoodReceive_Click(object sender, EventArgs e)
    {
        var received = new FrmFinishedGoodsReceived(false, string.Empty)
        {
            MdiParent = this
        };
        received.Show();
    }

    private void MnuSchoolTime_Click(object sender, EventArgs e)
    {
    }

    private void MnuPOSSales_Click(object sender, EventArgs e)
    {
        var result = new FrmPSalesInvoice();
        result.ShowDialog();
    }

    private void MnuAutoConsumption_Click(object sender, EventArgs e)
    {
        var consumption = new FrmAutoConsumption
        {
            MdiParent = this,
            WindowState = FormWindowState.Normal,
            Dock = DockStyle.Fill
        };
        consumption.Show();
    }

    private void MnuServiceSales_Click(object sender, EventArgs e)
    {
        var result = new FrmServiceSalesInvoice();
        result.ShowDialog();
    }

    private void MnuCustomizedInvoice_Click(object sender, EventArgs e)
    {
        new FrmPointOfSales().ShowDialog();
    }

    private void WebSitesToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new FrmWebSites();
        result.ShowDialog();
        //var result = new FrmVacancy(@"WEBSITES");
        //result.ShowDialog();
    }

    private void MnuOnlineDataSync_Click(object sender, EventArgs e)
    {
        new FrmDataSync().ShowDialog();
    }

    private void MnuOnlineSyncData_Click(object sender, EventArgs e)
    {
        var result = new FrmCloudDataSync();
        result.ShowDialog();
    }

    private void MnuBookManagement_Click(object sender, EventArgs e)
    {
        var result = new FrmStationaryDashboard();
        result.ShowDialog();
    }

    [Obsolete]
    private void MnuRegisterDynamicReports_Click(object sender, EventArgs e)
    {
        var result = new FrmDynamicRegisterReports();
        result.ShowDialog();
    }

    private void PartyAnalysisReports_Click(object sender, EventArgs e)
    {
    }

    private void MnuStockAgingReports_Click(object sender, EventArgs e)
    {
    }

    private void MnDataManage_Click(object sender, EventArgs e)
    {
    }

    private void ReconcileDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new FrmDataReconciliation();
        result.ShowDialog();
    }

    private void MnuSocial_Click(object sender, EventArgs e)
    {
        if (WebViewIsInstalled())
        {
            var result = new FrmSocialControl
            {
                MdiParent = this
            };
            result.Show();
        }
        else
        {
            ExecuteAsAdmin(@"MicrosoftEdgeWebview2Setup.exe");
        }
    }

    private void MnuLoomProduction_Click(object sender, EventArgs e)
    {
        var production = new MdiProduction();
        production.ShowDialog();
        production.BringToFront();
    }

    private void MnuMemberReports_Click(object sender, EventArgs e)
    {
        var reports = new FrmMemberShipReports
        {
            MdiParent = this
        };
        reports.Show();
    }

    private void MnuBillOfMaterialsReports_Click(object sender, EventArgs e)
    {
        var result = new FrmBillOfMaterialsReports();
        result.ShowDialog();
    }

    private void MnuVatRegisterIrdFormat_Click(object sender, EventArgs e)
    {
        var report = new FrmCrystalReportViewer(@"SalesVatRegister");
        report.ShowDialog();
    }

    private void MnuIrdSync_Click(object sender, EventArgs e)
    {
        var result = new FrmIrdSync();
        result.ShowDialog();
    }

    [Obsolete]
    private void MnuDynamicStockReports_Click(object sender, EventArgs e)
    {
        var result = new FrmDynamicInventoryReports();
        result.Show();
    }

    // REGISTER REPORT

    #region --------- REGISTER REPORT ----------

    private void MnuApiKeyList_Click(object sender, EventArgs e)
    {
        var result = new FrmLockScreen(false, @"APIKEY");
        result.ShowDialog();
        if (result.DialogResult == DialogResult.Yes)
        {
            var api = new FrmApiKeyList();
            api.ShowDialog();
        }
    }

    private void MnuOnlineConfig_Click(object sender, EventArgs e)
    {
        var result = new FrmLockScreen(false, @"API");
        result.ShowDialog();
        if (result.DialogResult == DialogResult.Yes)
        {
            var api = new FrmOnlineConfig().ShowDialog();
        }
    }

    #endregion --------- REGISTER REPORT ----------

    #endregion --------------- MENU CLICK ---------------


    // LIST OF MASTER REPORTS
    #region --------------- MENU LIST OF MASTER CLICK ---------------

    private void MnuListGeneralLedger_Click(object sender, EventArgs e)
    {
        var result = new FrmLedgerList();
        result.ShowDialog();
    }

    private void MnuTopNCustomerList_Click(object sender, EventArgs e)
    {
        var frm = new FrmTopNAnalysis("C")
        {
            MdiParent = this
        };
        frm.Show();
    }

    private void MnuTopNVendorList_Click(object sender, EventArgs e)
    {
        var frm = new FrmTopNAnalysis("V")
        {
            MdiParent = this
        };
        frm.Show();
    }

    private void MnuTopNProductList_Click(object sender, EventArgs e)
    {
        var frm = new FrmTopNAnalysis("P")
        {
            MdiParent = this
        };
        frm.Show();
    }

    private void TmrLicenses_Tick(object sender, EventArgs e)
    {
        if (ObjGlobal.IsLicenseExpire)
        {
            LicenseTimer.Enabled = false;
            if (ObjGlobal.LogInUser.GetUpper().Equals(@"AMSADMIN"))
            {
                var result = new FrmLicenseGenerator();
                result.ShowDialog();
                return;
            }
            else
            {
                if (Debugger.IsAttached)
                {
                    new FrmLicenseExpiredMessage().ShowDialog();
                    return;
                }
                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    new FrmLicenseExpiredMessage().ShowDialog();
                    return;
                }
                if (ObjGlobal.RemainingDays < -15 && ObjGlobal.RemainingDays != 0)
                {
                    var time = DateTime.Now.Hour;
                    if (time < 10 && time > 18)
                    {
                        new FrmLicenseExpiredMessage().ShowDialog();
                        return;
                    }
                }
                var result = new FrmRegistrationMail();
                result.ShowDialog();
            }
        }
        else
        {
            LicenseTimer.Stop();
            LicenseTimer.Enabled = false;
        }
    }

    private void MnuJobVacancyAnnounce_Click(object sender, EventArgs e)
    {
        var result = new FrmVacancy(@"MR_VACANCY");
        result.ShowDialog();
    }

    private void MnuTrainingCourse_Click(object sender, EventArgs e)
    {
        var result = new FrmVacancy(@"TRAINING");
        result.ShowDialog();
    }

    private void MnuVacancy_Click(object sender, EventArgs e)
    {
        var result = new FrmVacancy(@"VACANCY");
        result.ShowDialog();
    }

    private void MnuStockManagement_Click(object sender, EventArgs e)
    {
        var result = new FrmStockManagement();
        result.ShowDialog();
    }

    private void productOpeningImportToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var result = new FrmProductOpeningFromExcel();
        result.ShowDialog();
    }

    private void HMnuFinanceReport_Click(object sender, EventArgs e)
    {

    }

    private void MnuJoinLedger_Click(object sender, EventArgs e)
    {

    }

    private void MnuOutStandingReportsToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void MnuIndentToolStripMenuItem2_Click(object sender, EventArgs e)
    {

    }

    private void MnuPurchaseMaster_Click(object sender, EventArgs e)
    {

    }

    private void HMnuEntery_Click(object sender, EventArgs e)
    {

    }

    private void HMnuCompanyManage_Click(object sender, EventArgs e)
    {

    }

    private void MnuCompanyMaster_Click(object sender, EventArgs e)
    {

    }

    private void HMnuMaster_Click(object sender, EventArgs e)
    {

    }

    private void MnuLedgerImport_Click(object sender, EventArgs e)
    {

    }

    private void mergeLedgerToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void joinLedgerToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void MnuAgentMaster_Click(object sender, EventArgs e)
    {

    }

    private void MnuIrdRequiredDocument_Click(object sender, EventArgs e)
    {

    }

    private void toolStripSeparator105_Click(object sender, EventArgs e)
    {

    }

    private void MnuNightAuditConfiguration_Click(object sender, EventArgs e)
    {

    }

    private void MnuSpecialReports_Click(object sender, EventArgs e)
    {

    }

    private void MnuSpecialReportsProductWise_Click(object sender, EventArgs e)
    {

    }

    private void MnuStockMomentAnalysis_Click(object sender, EventArgs e)
    {

    }

    private void MnuPurchaseRegister_Click(object sender, EventArgs e)
    {

    }

    private void MnuPOS_Click(object sender, EventArgs e)
    {

    }

    private void MnuMergeLedger_Click(object sender, EventArgs e)
    {

    }

    private void MnutrialBalanceTBToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void MnuprofitLossPLToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void MnubalanceSheetBSToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void MnuMISReportsToolStripMenuItem_Click(object sender, EventArgs e)
    {

    }

    private void MnuPayroll_Click(object sender, EventArgs e)
    {

    }

    private void MnuProvision_Click(object sender, EventArgs e)
    {

    }

    private void MnuChartsofAccounts_Click(object sender, EventArgs e)
    {

    }

    private void MnuGodownTransferRegisterReports_Click(object sender, EventArgs e)
    {

    }

    private void MnuPhysicalStockRegisterReports_Click(object sender, EventArgs e)
    {

    }

    private void MnuStockAdjustmentRegisterReports_Click(object sender, EventArgs e)
    {

    }

    private void MnuProductionSystem_Click(object sender, EventArgs e)
    {

    }

    private void MnuAdvanceProduction_Click(object sender, EventArgs e)
    {

    }

    private void MnuGodownMaster_Click(object sender, EventArgs e)
    {

    }

    private void MnuProductionBillOfMaterialsRegisterReports_Click(object sender, EventArgs e)
    {

    }

    private void MnuFiscalYearClosed_Click(object sender, EventArgs e)
    {

    }

    private void MnuLastYearClosing_Click(object sender, EventArgs e)
    {

    }

    private void HmnuStockReport_Click(object sender, EventArgs e)
    {

    }

    private void MnuAreaMaster_Click(object sender, EventArgs e)
    {

    }

    #endregion --------------- Menu_ListOfMaster_Click ---------------


    // GRID CONTROL
    #region --------------- GRID CONTROL ---------------

    private void SearchGrid_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex == -1 || e.RowIndex == -1) return;
        var model = SearchGrid.Rows[e.RowIndex].DataBoundItem as GridMenuSearch;
        SearchPanel.Visible = false;
        TxtSearchGrid.Clear();
        model?.MenuItem.PerformClick();
    }

    private void TxtSearchGrid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var query = TxtSearchGrid.Text.Trim();
            SearchGrid.DataSource = query.IsValueExits()
                ? _menuSearches.Where(x => x.SubMenu.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1).ToList()
                : _menuSearches;
            ObjGlobal.DGridColorCombo(SearchGrid);
        }
        catch
        {
            // ignored
        }
    }

    private void TxtSearchGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode is Keys.Up or Keys.Down) SearchGrid.Focus();
    }

    private void SearchGrid_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar is (char)Keys.Enter)
        {
            if (SearchGrid.SelectedRows.Count == 0) return;
            SearchGrid_CellClick(SearchGrid, new DataGridViewCellEventArgs(0, SearchGrid.SelectedRows[0].Index));
        }
    }

    private void SearchGrid_EnterKeyPressed(object sender, EventArgs e)
    {
        SearchGrid_KeyPress(sender, new KeyPressEventArgs((char)Keys.Enter));
    }

    #endregion --------------- GRID CONTROL ---------------


    // RIGHT CLICK MENU
    #region --------------- RIGHT CLICK MENU ---------------
    private void RightClickOption_Opening(object sender, CancelEventArgs e)
    {
        if (ObjGlobal.SoftwareModule.Equals(@"POS"))
            RightClickOption.Items[3].Visible = false;
        else if (ObjGlobal.SoftwareModule.Equals(@"RESTRO"))
            RightClickOption.Items[3].Visible = false;
        else
            RightClickOption.Items[3].Visible = true;
    }

    private void OpenGeneralLedgerSetup_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.CreateGeneralLedger();
    }

    private void OpenProductSetup_Click(object sender, EventArgs e)
    {
        var result = GetMasterList.CreateProduct();
    }

    private void OpenSalesInvoice_Click(object sender, EventArgs e)
    {
        MnuSalesInvoice_Click(sender, e);
    }

    private void OpenPurchaseInvoice_Click(object sender, EventArgs e)
    {
        MnuPurchaseInvoice_Click(sender, e);
    }

    private void OpenCashBankVoucher_Click(object sender, EventArgs e)
    {
        MnuCashEntry_Click(sender, e);
    }

    private void OpenJournalVoucher_Click(object sender, EventArgs e)
    {
        MnuJournalVoucherEntry_Click(sender, e);
    }

    private void OpenGeneralLedger_Click(object sender, EventArgs e)
    {
        MnuLedgerReport_Click(sender, e);
    }

    private void OpenStockLedger_Click(object sender, EventArgs e)
    {
        MnuStockLedgerValue_Click(sender, e);
    }

    private void OpenVatRegister_Click(object sender, EventArgs e)
    {
        MnuVatRegister_Click(sender, e);
    }

    #endregion --------------- RIGHT CLICK MENU ---------------


    // SETTING 
    #region  --------------- SYSTEM SETTING ---------------

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

    [Obsolete]
    private void MnuSystemSetting_Click(object sender, EventArgs e)
    {
        var result = new FrmSystemSettings();
        result.ShowDialog(this);
        BindFooterDetails();
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

    private void MnuIncomeTaxSetting_Click(object sender, EventArgs e)
    {
        var dialog = new FrmIncomeTaxSetup();
        dialog.ShowDialog(this);
    }

    private void MnuSalarySetting_Click(object sender, EventArgs e)
    {

    }
    #endregion


    // HOTEL MANAGEMENT SYSTEM
    #region --------------- HOTEL MANAGEMENT SYSTEM ---------------
    private void MnuHotelDashboard_Click(object sender, EventArgs e)
    {

    }
    #endregion


    // METHOD FOR THIS FORM
    #region --------------- METHOD ---------------
    [Obsolete]
    private void BindFooterDetails()
    {
        try
        {
            HearderMenuList.Visible = true;
            SetSoftwareModule();
            ObjGlobal.GetFiscalYearDetails();
            SetFooterDetails();
            Text = @"MrSolution v2018 | ACCOUNT & INVENTORY MANAGEMENT SYSTEM";
            tsModule.Text = $@"MODULE :-{ObjGlobal.SoftwareModule.GetUpper()}";
            MenuAccessControl();
            SaveLoginAuditInDatabase();
            OpenTerminalInvoiceIfRequired();
            _ = DailyBackupAsync();
            GenerateHeaderMenuList();
            LicenseInformation();
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace);
        }
    }

    private void SetSoftwareModule()
    {
        if (ObjGlobal.SoftwareModule.IsBlankOrEmpty())
        {
            var frm = new FrmSoftwareModule();
            frm.ShowDialog();
            ObjGlobal.SoftwareModule = frm.SoftwareModule.ToUpper();
            tsModule.Text = $@"MODULE :-[{frm.SoftwareModule.ToUpper()}]";
            MnuLogOut.PerformClick();
        }
    }

    private void SetFooterDetails()
    {
        TsInitial.Text = ObjGlobal.InitialCatalog;
        TsLogInUser.Text = $@"USER_INFO :- {ObjGlobal.LogInUser.ToUpper()}";
        TsCompanyInfo.Text = $@"COMPANY :- {ObjGlobal.LogInCompany?.ToUpper()}";
        TsStartDate.Text = ObjGlobal.SysDateType switch
        {
            "M" or null => $@"FROM_DATE :-  {ObjGlobal.CfStartBsDate}",
            _ => $@"FROM_DATE :-  {ObjGlobal.CfStartAdDate.ToShortDateString()}"
        };
        TsEndDate.Text = ObjGlobal.SysDateType switch
        {
            "M" or null => $@"TO_DATE :- {ObjGlobal.CfEndBsDate}",
            _ => $@"TO_DATE :- {ObjGlobal.CfEndAdDate.ToShortDateString()}"
        };
        TsBranchInfo.Text = $@"BRANCH :- {ObjGlobal.SysBranchName}";
        TsFiscalYears.Text = $@"FISCAL_YEAR :- {(ObjGlobal.SysDateType is "M" ? ObjGlobal.SysBsFiscalYear : ObjGlobal.SysFiscalYear)}";
        TsWebSites.Text = @"www.mrsolution.com.np";
        TsCopyRights.Text = @$"COPYRIGHTS RESERVE : M&&R Solution © {DateTime.Now.Year} ";
    }

    private void OpenTerminalInvoiceIfRequired()
    {
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
    }

    private void LicenseInformation()
    {
        if (ObjGlobal.IsNewInstallation)
        {
            tsRegistration.Text = @" REGISTRATION INFO : YOUR LICENSE IS DEMO VERSION...!!";
            tsRegistration.ForeColor = Color.Blue;
        }
        else
        {
            tsRegistration.Text = ObjGlobal.IsLicenseExpire switch
            {
                true => @$"REGISTRATION INFO : YOUR LICENSE HAS BEEN EXPIRED [{DateTime.Now.GetDateString()}]",
                _ => @" REGISTRATION INFO : YOUR LICENSE HAS BEEN REGISTERED"
            };

            if (ObjGlobal.RemainingDays < 15)
            {
                tsRegistration.Text = $@"LICENSE WILL BE EXPIRED AFTER [{ObjGlobal.RemainingDays}] DAYS";
                tsRegistration.ForeColor = Color.IndianRed;
            }
            tsRegistration.ForeColor = ObjGlobal.IsLicenseExpire ? Color.Red : Color.DarkGreen;
        }

        LicenseTimer.Enabled = true;
    }

    private void MenuAccessControl()
    {
        MenuListVisibleFalse();
        UserFocusMenuVisible();

    }

    private void GenerateHeaderMenuList()
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

    [Obsolete]
    private async void SaveLoginAuditInDatabase()
    {
        try
        {
            LoginId = ClsMasterSetup.ReturnMaxIntValue(@"MASTER.AMS.LOGIN_LOG", @"OBJECT_ID").GetInt();
            _backup.LoginLog.OBJECT_ID = LoginId;
            _backup.LoginLog.LOGIN_USER = ObjGlobal.LogInUser;
            _backup.LoginLog.COMPANY = ObjGlobal.LogInCompany?.GetTrimReplace();
            _backup.LoginLog.LOGIN_DATABASE = ObjGlobal.InitialCatalog;
            _backup.LoginLog.DEVICE = ObjGlobal.GetServerName();
            _backup.LoginLog.DAVICE_MAC = ObjGlobal.GetMacAddress();
            _backup.LoginLog.DEVICE_IP = ObjGlobal.GetIpAddress();
            _backup.LoginLog.SYSTEM_ID = await ObjGlobal.GetSerialNo();
            _backup.LoginLog.LOG_STATUS = 1;
            await _backup.SaveLoginLog(@"SAVE");

        }
        catch (Exception e)
        {
            e.DialogResult();
        }
    }

    [Obsolete]
    private void UserTerminated()
    {
        DeviceName = ObjGlobal.GetServerName();
        DeviceIp = ObjGlobal.GetIpAddress();
        //_master.ResetLoginInfo("SAVE");
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
            e.ToNonQueryErrorResult(e.StackTrace);
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
        _backup.BackupLog.STATUS = @"Backup";
        var result = _backup.SaveBackupAndRestoreDatabaseLog(location);
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

        TsInitial.IsClear();
        TsLogInUser.IsClear();
        TsBranchInfo.IsClear();
        TsInitial.IsClear();
        TsLogInUser.IsClear();
        TsCompanyInfo.IsClear();
        TsStartDate.IsClear();
        TsEndDate.IsClear();
        TsBranchInfo.IsClear();
        TsFiscalYears.IsClear();
        TsWebSites.IsClear();
        TsCopyRights.IsClear();
        tsModule.IsClear();
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

        if (ObjGlobal.UserAuthorized)
        {
            TsProvisionTransactionSeparator.Visible = true;
            MnuProvisionTransaction.Enabled = MnuProvisionTransaction.Enabled = true;
        }

        if (ObjGlobal.SoftwareModule is "HOSPITAL")
        {
            TsHospitalMasterSeparator.Visible = true;
            MnuHospitalMaster.Enabled = MnuHospitalMaster.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "RESTRO")
        {
            TsRestaurantSeparator.Visible = true;
            MnuRestaurantMaster.Enabled = MnuRestaurantMaster.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "HOTEL")
        {

            TsHotelMasterSeparator.Visible = true;
            MnuHotelManagement.Visible = MnuHotelManagement.Enabled = true;
        }

        if (ObjGlobal.SoftwareModule is "PAYROL")
        {
            TsPayrolSetupSeparator.Visible = true;
            MnuPayrollManagement.Enabled = MnuPayrollManagement.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "POS")
        {
            TsPointOfSalesMasterSeparator.Visible = true;
            MnuPointOfSalesManagement.Enabled = MnuPointOfSalesManagement.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "VEHICLE")
        {
            TsVehicleMasterSeparator.Visible = true;
            MnuVehicleManagement.Enabled = MnuVehicleManagement.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "SCHOOLTIME" or "SCHOOL-TIME")
        {
            TsSchoolMasterSeparator.Visible = true;
            MnuSchoolTime.Enabled = MnuSchoolTime.Visible = true;
            MnuStationaryManagement.Enabled = MnuStationaryManagement.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "TRAVEL")
        {
            TsTravelMasterSeparator.Visible = true;
            MnuTravelMaster.Enabled = MnuTravelMaster.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "PHARMA")
        {
            TsPharmaMasterSeparator.Visible = true;
            MnuPharmaMaster.Enabled = MnuPharmaMaster.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "CINEMA")
        {
            TsCinemaMasterSeparator.Visible = true;
            MnuCinemaMaster.Enabled = MnuCinemaMaster.Visible = true;
        }

        if (ObjGlobal.SoftwareModule is "ERP")
        {
            TsAdvanceProductionMasterSeparator.Visible = true;
            MnuAdvanceProduction.Visible = MnuAdvanceProduction.Enabled = true;
            if (TsAdvanceProductionMasterSeparator.Visible)
            {
                TsPayrolSetupSeparator.Visible = true;
                MnuPayrollManagement.Enabled = MnuPayrollManagement.Visible = true;
            }

            MnuManufacturing.Enabled = MnuManufacturing.Visible = true;
        }

        if (ObjGlobal.IsOnlineMode)
        {
            MnuCompanySetup.Enabled = MnuCompanySetup.Visible = false;
            MnuCompanyRights.Enabled = MnuCompanyRights.Visible = false;

            MnuUserCreate.Enabled = MnuUserCreate.Visible = false;
            MnuUserControl.Enabled = MnuUserControl.Visible = false;

            MnuAddCrystalDesign.Enabled = MnuAddCrystalDesign.Visible = false;
            MnuSqlServerConnection.Enabled = MnuSqlServerConnection.Visible = false;

            MnuExternalDeviceUtility.Enabled = MnuExternalDeviceUtility.Visible = false;

            MnuImportData.Enabled = MnuImportData.Visible = false;
            MnuOnlineConfig.Enabled = MnuOnlineConfig.Visible = false;
            MnuOnlineDataSync.Enabled = MnuOnlineDataSync.Visible = false;

            MnuRestore.Enabled = MnuRestore.Visible = !ObjGlobal.IsIrdRegister;
        }

        MnuChangePassword.Enabled = !ObjGlobal.LogInUser.GetUpper().Equals(@"AMSADMIN");
        MnuChangePassword.Visible = MnuChangePassword.Enabled;

        MnuIrdRequiredDocument.Visible = MnuIrdRequiredDocument.Visible = ObjGlobal.LogInUser.Equals(@"ADMIN");

        var domain = ObjGlobal.DomainLoginUser.Contains(ObjGlobal.LogInUser);
        MnuDecryptValue.Enabled = MnuDecryptValue.Visible = domain;
        MnuOnlineRegistration.Visible = MnuOnlineRegistration.Enabled = domain;
    }

    private void MenuListVisibleFalse(bool isEnable = false)
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

        HearderMenuList.Items.OfType<ToolStripMenuItem>().Where(i => i.DropDownItems.Count > 0).ForEach(x =>
        {
            x.DropDownItems.OfType<ToolStripSeparator>().ForEach(di =>
            {
                di.Enabled = isEnable;
                di.Visible = isEnable;
            });
        });

        if (!isEnable)
        {
            return;
        }

        // COMPANY MANAGE
        TsDocumentReNumberingSeparator.Visible = false;
        MnuVoucherReNumbering.Visible = MnuVoucherReNumbering.Enabled = false;
        TsDashBoardSeparator.Visible = TsDashBoardSeparator.Enabled = false;
        MnuDashBoard.Visible = MnuDashBoard.Enabled = false;

        // MASTER SETUP
        TsLedgerImportSeparator.Visible = false;
        MnuLedgerImport.Visible = MnuLedgerImport.Enabled = false;
        TsLedgerMergeSeparator.Visible = false;
        MnuLedgerMergeSetup.Enabled = MnuLedgerJoinSetup.Enabled = false;
        MnuLedgerMergeSetup.Visible = MnuLedgerJoinSetup.Visible = false;

        TsColastarSeparator.Enabled = TsColastarSeparator.Visible = false;
        MnuColastar.Visible = MnuColastar.Enabled = false;

        MnuClass.Enabled = MnuClass.Visible = false;
        MnuSection.Enabled = MnuSection.Visible = false;
        TsClassSeparator.Enabled = TsClassSeparator.Visible = false;

        TsRoomSetupSeparator.Enabled = TsRoomSetupSeparator.Visible = false;
        MnuRoomNoSetup.Enabled = MnuRoomNoSetup.Visible = false;
        MnuRoomType.Enabled = MnuRoomType.Visible = false;
        MnuRoomPlanType.Enabled = MnuRoomPlanType.Visible = false;
        MnuGuestType.Enabled = MnuGuestType.Visible = false;

        TsPointOfSalesProductSetupSeparator.Enabled = TsPointOfSalesProductSetupSeparator.Visible = false;
        MnuCounterProduct.Enabled = MnuCounterProduct.Visible = false;
        MnuCounterProductMini.Enabled = MnuCounterProductMini.Visible = false;
        MnuSparePartsSetup.Enabled = MnuSparePartsSetup.Visible = false;

        TsRestaurantProductSeparator.Enabled = TsRestaurantProductSeparator.Visible = false;
        MnuMenuSetup.Enabled = MnuMenuSetup.Visible = false;
        MnuSubMenuItem.Enabled = MnuSubMenuItem.Visible = false;
        MnuRestoProduct.Enabled = MnuRestoProduct.Visible = false;

        TsMovieSetupSeparator.Enabled = TsMovieSetupSeparator.Visible = false;
        MnuMovies.Enabled = MnuMovies.Visible = false;
        MnuProductMapping.Enabled = MnuProductMapping.Visible = false;
        MnuProductLock.Enabled = MnuProductLock.Visible = false;
        MnuProductUpdate.Enabled = MnuProductUpdate.Visible = false;

        TsProductUpdateSeparator.Enabled = TsProductUpdateSeparator.Visible = false;
        MnuProductImport.Enabled = MnuProductImport.Visible = false;
        MnuProductAssamble.Enabled = MnuProductAssamble.Visible = false;
        MnuSparePartsImport.Enabled = MnuSparePartsImport.Visible = false;

        // VOUCHER ENTRY
        TsPurchaseInvoiceTypeSeparator.Visible = TsPurchaseInvoiceTypeSeparator.Enabled = false;
        MnuNormalPurchaseInvoiceEntry.Visible = MnuNormalPurchaseInvoiceEntry.Enabled = false;
        MnuPurchaseAvtInvoiceEntry.Visible = MnuPurchaseAvtInvoiceEntry.Enabled = false;

        TsNormalSalesSeparator.Visible = TsNormalSalesSeparator.Enabled = false;
        MnuSalesNormalInvoiceEntry.Visible = MnuSalesNormalInvoiceEntry.Enabled = false;
        MnuEstimateNormalInvoiceEntry.Visible = MnuEstimateNormalInvoiceEntry.Enabled = false;
        MnuServiceSalesEntry.Visible = MnuServiceSalesEntry.Enabled = false;

        TsPointOfSalesSeparator.Visible = TsPointOfSalesSeparator.Enabled = false;
        MnuSalesPosInvoiceEntry.Visible = MnuSalesPosInvoiceEntry.Enabled = false;

        TsAbtSalesSeparator.Visible = TsAbtSalesSeparator.Enabled = false;
        MnuSalesAvtInvoiceEntry.Visible = MnuSalesAvtInvoiceEntry.Enabled = false;
        MnuProvisionTransaction.Visible = MnuProvisionTransaction.Enabled = false;

        TsJournalVoucherSeparator.Visible = TsJournalVoucherSeparator.Enabled = false;
        MnuJournlVoucherMultiCurrency.Visible = MnuJournlVoucherMultiCurrency.Enabled = false;
        MnuSingleJournalVoucher.Visible = MnuSingleJournalVoucher.Enabled = false;

        TsMultiCashBankSeparator.Visible = TsMultiCashBankSeparator.Enabled = false;
        MnuCashBankMultiCurrency.Enabled = MnuCashBankMultiCurrency.Visible = false;

        TsPaymentReceiptSeparator.Visible = TsPaymentReceiptSeparator.Enabled = false;
        MnuPaymentVoucher.Visible = MnuPaymentVoucher.Enabled = false;
        MnuReceiptVoucher.Visible = MnuReceiptVoucher.Enabled = false;

        TsFinanceSalesSeparator.Visible = TsFinanceSalesSeparator.Enabled = false;
        MnuFinanceSalesInvoice.Visible = MnuFinanceSalesInvoice.Enabled = false;
        MnuFinanaceSalesReturn.Visible = MnuFinanaceSalesReturn.Enabled = false;

        TsFinancePurchaseSeparator.Visible = TsFinancePurchaseSeparator.Enabled = false;
        MnuFinancePurchaseInvoice.Visible = MnuFinancePurchaseInvoice.Enabled = false;
        MnuFinancePurchaseReturn.Visible = MnuFinancePurchaseReturn.Enabled = false;

        TsForeignExchangeSeparator.Visible = TsForeignExchangeSeparator.Enabled = false;
        MnuMoneyExchange.Visible = MnuMoneyExchange.Enabled = false;
        MnuLcGenerate.Visible = MnuLcGenerate.Enabled = false;

        TsAdvanceProductionMasterSeparator.Visible = TsAdvanceProductionMasterSeparator.Enabled = false;
        MnuAdvanceProduction.Visible = MnuAdvanceProduction.Enabled = false;

        TsHospitalMasterSeparator.Enabled = TsHospitalMasterSeparator.Visible = false;
        MnuHospitalMaster.Enabled = MnuHospitalMaster.Visible = false;

        TsRestaurantSeparator.Visible = TsRestaurantSeparator.Enabled = false;
        MnuRestaurantMaster.Visible = MnuRestaurantMaster.Enabled = false;

        TsHotelMasterSeparator.Visible = TsHotelMasterSeparator.Enabled = false;
        MnuHotelManagement.Visible = MnuHotelManagement.Enabled = false;

        TsPayrolSetupSeparator.Visible = TsPayrolSetupSeparator.Enabled = false;
        MnuPayrollManagement.Visible = MnuPayrollManagement.Enabled = false;

        TsPointOfSalesMasterSeparator.Visible = TsPointOfSalesMasterSeparator.Enabled = false;
        MnuPointOfSalesManagement.Visible = MnuPointOfSalesManagement.Enabled = false;

        TsVehicleMasterSeparator.Visible = TsVehicleMasterSeparator.Enabled = false;
        MnuVehicleManagement.Visible = MnuVehicleManagement.Enabled = false;

        TsSchoolMasterSeparator.Visible = TsSchoolMasterSeparator.Enabled = false;
        MnuSchoolTime.Visible = MnuSchoolTime.Enabled = false;
        MnuStationaryManagement.Visible = MnuStationaryManagement.Enabled = false;

        TsTravelMasterSeparator.Visible = TsTravelMasterSeparator.Enabled = false;
        MnuTravelMaster.Visible = MnuTravelMaster.Enabled = false;

        TsPharmaMasterSeparator.Visible = TsPharmaMasterSeparator.Enabled = false;
        MnuPharmaMaster.Visible = MnuPharmaMaster.Enabled = false;

        TsCinemaMasterSeparator.Visible = TsCinemaMasterSeparator.Enabled = false;
        MnuCinemaMaster.Visible = MnuCinemaMaster.Enabled = false;

        TsMoneyExchanger.Visible = TsMoneyExchanger.Enabled = false;
        MnuMoneyExchanger.Visible = MnuMoneyExchanger.Enabled = false;

        // REPORT
        TsJoinLedgerSeparator.Visible = TsJoinLedgerSeparator.Enabled = false;
        MnuJoinLedger.Visible = MnuJoinLedger.Enabled = false;
        MnuMergeLedger.Visible = MnuMergeLedger.Enabled = false;

        TsManufacturingSeparator.Visible = TsManufacturingSeparator.Enabled = false;
        MnuManufacturing.Enabled = MnuManufacturing.Visible = false;

        TsTrainingAndVacancySeparator.Enabled = TsTrainingAndVacancySeparator.Visible = false;
        MnuTraingAndVacancy.Enabled = MnuTraingAndVacancy.Visible = false;

        MnuIrdRequiredDocument.Visible = MnuIrdRequiredDocument.Enabled = false;
        MnuSocialMedia.Enabled = MnuSocialMedia.Visible = false;
        ProjectFocusMenuVisible();
    }

    private void SalesMenuVisibleTrue()
    {
        foreach (var x in HearderMenuList.Items.OfType<ToolStripMenuItem>())
        {
            if (x == HMnuCompanyManage)
            {
                HMnuCompanyManage.Visible = HMnuCompanyManage.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di != MnuCompanySelection)
                    {
                        continue;
                    }

                    MnuCompanySelection.Visible = MnuCompanySelection.Enabled = true;
                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }

                }
            }

            if (x == HMnuVoucherEntry)
            {
                HMnuVoucherEntry.Visible = HMnuVoucherEntry.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di == MnuSalesMaster)
                    {
                        MnuSalesMaster.Visible = MnuSalesMaster.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                        continue;
                    }

                    if (di != MnuPointOfSalesManagement)
                    {
                        continue;
                    }
                    MnuPointOfSalesManagement.Visible = MnuPointOfSalesManagement.Enabled = true;
                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }
                }
            }

            if (x == HMnuRegisterReport)
            {
                HMnuRegisterReport.Visible = HMnuRegisterReport.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di == MnuSalesRegister)
                    {
                        MnuSalesRegister.Visible = MnuSalesRegister.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                    }
                    if (di == MnuSalesRegister)
                    {
                        MnuSalesRegister.Visible = MnuSalesRegister.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                    }

                    if (di == MnuSalesAnaylsis)
                    {
                        MnuSalesAnaylsis.Visible = MnuSalesAnaylsis.Enabled = true;
                        if (di.DropDownItems.Count <= 0)
                        {
                            continue;
                        }
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }

                    if (di != MnuVatRegister)
                    {
                        continue;
                    }

                    MnuVatRegister.Visible = MnuVatRegister.Enabled = true;
                    if (di.DropDownItems.Count <= 0)
                    {
                        continue;
                    }
                    foreach (var child in di.DropDownItems.OfType<ToolStripItem>())
                    {
                        if (child == MnuSalesVatRegister || child == MnuSalesReturnVatRegister)
                        {
                            child.Visible = child.Enabled = true;
                        }
                    }
                }
            }

        }
    }

    private void SalesOrderMenuVisibleTrue()
    {
        foreach (var x in HearderMenuList.Items.OfType<ToolStripMenuItem>())
        {
            if (x == HMnuCompanyManage)
            {
                HMnuCompanyManage.Visible = HMnuCompanyManage.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di != MnuCompanySelection)
                    {
                        continue;
                    }

                    MnuCompanySelection.Visible = MnuCompanySelection.Enabled = true;
                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }

                }
            }

            if (x == HMnuVoucherEntry)
            {
                HMnuVoucherEntry.Visible = HMnuVoucherEntry.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di == MnuSalesMaster)
                    {
                        MnuSalesMaster.Visible = MnuSalesMaster.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                        continue;
                    }

                    if (di != MnuPointOfSalesManagement)
                    {
                        continue;
                    }
                    MnuPointOfSalesManagement.Visible = MnuPointOfSalesManagement.Enabled = true;
                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }
                }
            }

            if (x == HMnuRegisterReport)
            {
                HMnuRegisterReport.Visible = HMnuRegisterReport.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di == MnuSalesRegister)
                    {
                        MnuSalesRegister.Visible = MnuSalesRegister.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                    }
                    if (di == MnuSalesRegister)
                    {
                        MnuSalesRegister.Visible = MnuSalesRegister.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                    }

                    if (di == MnuSalesAnaylsis)
                    {
                        MnuSalesAnaylsis.Visible = MnuSalesAnaylsis.Enabled = true;
                        if (di.DropDownItems.Count <= 0)
                        {
                            continue;
                        }
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }

                    if (di != MnuVatRegister)
                    {
                        continue;
                    }

                    MnuVatRegister.Visible = MnuVatRegister.Enabled = true;
                    if (di.DropDownItems.Count <= 0)
                    {
                        continue;
                    }
                    foreach (var child in di.DropDownItems.OfType<ToolStripItem>())
                    {
                        if (child == MnuSalesVatRegister || child == MnuSalesReturnVatRegister)
                        {
                            child.Visible = child.Enabled = true;
                        }
                    }
                }
            }

        }
    }

    private void ReportMenuVisibleTrue()
    {
        foreach (var x in HearderMenuList.Items.OfType<ToolStripMenuItem>())
        {
            if (x == HMnuCompanyManage)
            {
                HMnuCompanyManage.Visible = HMnuCompanyManage.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di != MnuCompanySelection)
                    {
                        continue;
                    }

                    MnuCompanySelection.Visible = MnuCompanySelection.Enabled = true;
                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }

                }
            }

            if (x == HMnuVoucherEntry)
            {
                HMnuVoucherEntry.Visible = HMnuVoucherEntry.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di == MnuSalesMaster)
                    {
                        MnuSalesMaster.Visible = MnuSalesMaster.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                        continue;
                    }

                    if (di != MnuPointOfSalesManagement)
                    {
                        continue;
                    }
                    MnuPointOfSalesManagement.Visible = MnuPointOfSalesManagement.Enabled = true;
                    if (di.DropDownItems.Count > 0)
                    {
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }
                }
            }

            if (x == HMnuRegisterReport)
            {
                HMnuRegisterReport.Visible = HMnuRegisterReport.Enabled = true;
                foreach (var di in x.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    if (di == MnuSalesRegister)
                    {
                        MnuSalesRegister.Visible = MnuSalesRegister.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                    }
                    if (di == MnuSalesRegister)
                    {
                        MnuSalesRegister.Visible = MnuSalesRegister.Enabled = true;
                        if (di.DropDownItems.Count > 0)
                        {
                            di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                            {
                                child.Visible = child.Enabled = true;
                            });
                        }
                    }

                    if (di == MnuSalesAnaylsis)
                    {
                        MnuSalesAnaylsis.Visible = MnuSalesAnaylsis.Enabled = true;
                        if (di.DropDownItems.Count <= 0)
                        {
                            continue;
                        }
                        di.DropDownItems.OfType<ToolStripItem>().ForEach(child =>
                        {
                            child.Visible = child.Enabled = true;
                        });
                    }

                    if (di != MnuVatRegister)
                    {
                        continue;
                    }

                    MnuVatRegister.Visible = MnuVatRegister.Enabled = true;
                    if (di.DropDownItems.Count <= 0)
                    {
                        continue;
                    }
                    foreach (var child in di.DropDownItems.OfType<ToolStripItem>())
                    {
                        if (child == MnuSalesVatRegister || child == MnuSalesReturnVatRegister)
                        {
                            child.Visible = child.Enabled = true;
                        }
                    }
                }
            }

        }
    }

    private void UserFocusMenuVisible()
    {
        if (ObjGlobal.LogInUserCategory is "SALES" or "TERMINAL")
        {
            SalesMenuVisibleTrue();
        }
        else if (ObjGlobal.LogInUserCategory is "ORDER")
        {
            SalesOrderMenuVisibleTrue();
        }
        else if (ObjGlobal.LogInUserCategory is "REPORT")
        {
            ReportMenuVisibleTrue();
        }
        else
        {
            MenuListVisibleFalse(true);
        }

    }

    private void BranchLogin()
    {
        try
        {
            var dtBranch = _master.LoginBranchDataTable(ObjGlobal.LogInUser.ToUpper() is "ADMIN" || ObjGlobal.LogInUser.ToUpper() is "AMSADMIN");
            if (dtBranch?.Rows.Count > 1)
            {
                var branchListForm = new FrmBranchList();
                branchListForm.ShowDialog();
                if (branchListForm.DialogResult != DialogResult.OK)
                {
                    return;
                }
                var dt = _master.GetCompanyUnit(@"SAVE", 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return;
                }
                var frm1 = new FrmCompanyUnitList();
                frm1.ShowDialog();
                return;
            }
            else
            {
                if (dtBranch?.Rows.Count is 1)
                {
                    ObjGlobal.SysBranchId = dtBranch.Rows[0]["Branch_Id"].GetInt();
                    ObjGlobal.SysBranchName = dtBranch.Rows[0]["Branch_Name"].ToString();
                    return;
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

                    return;
                }

                const string companyUnit = "SELECT * FROM AMS.CompanyUnit ";
                var dtCompanyUnit = GetConnection.SelectDataTableQuery(companyUnit);
                if (dtCompanyUnit.Rows.Count > 0)
                {
                    var result = new FrmCompanyUnitList();
                    result.ShowDialog();
                    return;
                }
                else
                {
                    ObjGlobal.SysCompanyUnitId = 0;
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToNonQueryErrorResult(ex.StackTrace);
            return;
        }
    }

    [Obsolete]
    private DialogResult CompanyLogin()
    {
        const string query = @"Select * from Master.AMS.GlobalCompany ";
        var dtCompany = SqlExtensions.ExecuteDataSetOnMaster(query).Tables[0];
        if (dtCompany.Rows.Count == 0)
        {
            var setup = new FrmCompanySetup(true);
            setup.ShowDialog();
            if (setup.DialogResult != DialogResult.OK)
            {
                var login = new FrmLogin();
                login.ShowDialog();
                return DialogResult.Abort;
            }
            DialogResult = setup.DialogResult;
            return DialogResult;
        }

        var getList = new FrmCompanyList(false);
        getList.ShowDialog();
        var result = getList.DialogResult;
        if (result != DialogResult.OK)
        {
            return DialogResult.Abort;
        }
        BranchLogin();
        return DialogResult.OK;
    }

    private static bool WebViewIsInstalled()
    {
        const string regKey = @"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients";
        var edgeKey = Registry.LocalMachine.OpenSubKey(regKey);
        if (edgeKey == null) return false;
        var productKeys = edgeKey.GetSubKeyNames();
        return productKeys.Any();

        //return EnumerableExtensions.Any(productKeys);
    }

    private static void ExecuteAsAdmin(string fileName)
    {
        var proc = new Process
        {
            StartInfo =
            {
                FileName = fileName,
                UseShellExecute = true,
                Verb = "runas"
            }
        };
        proc.Start();
    }

    private int SaveNightAudit()
    {
        _salesEntry.AuditLog.AuditDate = DateTime.Now;
        _salesEntry.AuditLog.IsAudited = false;
        _salesEntry.AuditLog.AuditUser = "";
        _salesEntry.AuditLog.AuditedDate = DateTime.Now;
        _salesEntry.AuditLog.BranchId = ObjGlobal.SysBranchId;
        _salesEntry.AuditLog.CompanyUnitId = ObjGlobal.SysCompanyUnitId;
        return _salesEntry.SaveNightAuditLog(@"SAVE");
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
    #endregion --------------- Method ---------------


    // OBJECT FOR THIS FORM
    #region --------------- OBJECT ---------------
    public int Nodes; // = string.Empty;
    public int Users; // = string.Empty;
    private readonly IRecalculate _recalculate;
    public int LoginId;
    public string DeviceName = string.Empty;
    public string DeviceIp = string.Empty;
    public string DbSelect = string.Empty;
    private readonly IMasterSetup _master;
    private IBackupRestore _backup;
    private readonly ISalesEntry _salesEntry;
    private List<GridMenuSearch> _menuSearches = new();
    #endregion --------------- OBJECT ---------------


}