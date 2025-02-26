using DatabaseModule.CloudSync;
using DatabaseModule.DataEntry.FinanceTransaction.CashBankVoucher;
using DatabaseModule.DataEntry.FinanceTransaction.JournalVoucher;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallan;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseChallanReturn;
using DatabaseModule.DataEntry.PurchaseMaster.PurchaseInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesChallan;
using DatabaseModule.DataEntry.SalesMaster.SalesInvoice;
using DatabaseModule.DataEntry.SalesMaster.SalesReturn;
using DatabaseModule.Master.FinanceSetup;
using DatabaseModule.Master.InventorySetup;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraSplashScreen;
using MrDAL.Core.Extensions;
using MrDAL.Core.Utils;
using MrDAL.DataEntry.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.FinanceTransaction.JournalVoucher;
using MrDAL.DataEntry.Interface;
using MrDAL.DataEntry.Interface.FinanceTransaction.CashBankVoucher;
using MrDAL.DataEntry.Interface.FinanceTransaction.JournalVoucher;
using MrDAL.DataEntry.Interface.OpeningMaster;
using MrDAL.DataEntry.Interface.PurchaseMaster;
using MrDAL.DataEntry.Interface.SalesChallan;
using MrDAL.DataEntry.Interface.SalesMaster;
using MrDAL.DataEntry.Interface.SalesReturn;
using MrDAL.DataEntry.OpeningMaster;
using MrDAL.DataEntry.PurchaseMaster;
using MrDAL.DataEntry.SalesMaster;
using MrDAL.DataEntry.TransactionClass;
using MrDAL.Domains.Shared.DataSync.Abstractions;
using MrDAL.Domains.Shared.DataSync.Common;
using MrDAL.Domains.Shared.DataSync.Factories;
using MrDAL.Domains.Shared.DataSync.Handlers;
using MrDAL.Global.Common;
using MrDAL.Global.Control;
using MrDAL.Global.Dialogs;
using MrDAL.Master;
using MrDAL.Master.FinanceSetup;
using MrDAL.Master.Interface;
using MrDAL.Master.Interface.FinanceSetup;
using MrDAL.Master.Interface.InventorySetup;
using MrDAL.Master.Interface.LedgerSetup;
using MrDAL.Master.Interface.ProductSetup;
using MrDAL.Master.InventorySetup;
using MrDAL.Master.LedgerSetup;
using MrDAL.Master.ProductSetup;
using MrDAL.Models.Common;
using MrDAL.Setup.CompanySetup;
using MrDAL.Setup.Interface;
using MrDAL.Utility.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBLL.Utility.DataSync;

public partial class FrmDataSync : XtraForm
{

    // DATA SYNC FORM
    #region ---------- DATA SYNC FORM ----------
    public FrmDataSync()
    {
        InitializeComponent();

        _master = new ClsMasterSetup();
        _frmSyncLogs = new FrmSyncLogs(false);

        _dataEntry = new ClsSalesEntry();
        _purchaseDataEntry = new ClsPurchaseEntry();
        _orderRepository = new PurchaseOrderRepository();
        _financeEntry = new ClsFinanceEntry();
        _companyUnitRepository = new CompanyUnitSetupRepository();
        _groupRepository = new AccountGroupRepository();
        _subGroupRepository = new AccountSubGroupRepository();
        _currencyRepository = new CurrencyRepository();
        _departmentRepository = new DepartmentRepository();
        _generalLedgerRepository = new GeneralLedgerRepository();
        _mainAreaRepository = new MainAreaRepository();
        _areaRepository = new AreaRepository();
        _mainAgentRepository = new MainAgentRepository();
        _juniorAgentRepository = new JuniorAgentRepository();
        _nrMasterRepository = new NarrationRemarksRepository();
        _subLedgerRepository = new SubLedgerRepository();
        _product = new ProductRepository();
        _productUnit = new ProductUnitRepository();
        _productSubGroup = new ProductSubGroupRepository();
        _productGroup = new ProductGroupRepository();
        _godownRepository = new GodownRepository();
        _costCenterRepository = new CostCenterRepository();
        _rackRepository = new RackRepository();
        _branchSetupRepository = new BranchSetupRepository();
        _ledgerOpeningRepository = new LedgerOpeningRepository();
        _giftVoucherRepository = new GiftVoucherRepository();
        _memberTypeRepository = new MemberTypeRepository();
        _membershipSetupRepository = new MembershipSetupRepository();
        _purchaseChallanRepository = new PurchaseChallanRepository();
        _narrationRemarksRepository = new NarrationRemarksRepository();
        _cashBankVoucherRepository = new CashBankVoucherRepository();
        _journalVoucherRepository = new JournalVoucherRepository();
        _salesReturnRepository = new SalesReturnRepository();
        _salesInvoiceRepository = new SalesInvoiceRepository();
        _salesChallanRepository = new SalesChallanRepository();
        _purchaseInvoiceRepository = new PurchaseInvoiceRepository();
        _purchaseReturnRepository = new PurchaseReturnInvoiceRepository();
        _purchaseOrderRepository = new PurchaseOrderRepository();
        _purchaseChallanReturnRepository = new PurchaseChallanReturnRepository();
        _productOpeningRepository = new ProductOpeningRepository();
    }
    private void FrmDataSync_FormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = _formBusy;
    }
    private void FrmDataSync_FormClosed(object sender, FormClosedEventArgs e)
    {
        _frmSyncLogs.AllowClosing = true;
        _frmSyncLogs.Close();
    }
    private async void FrmDataSync_Load(object sender, EventArgs e)
    {
        LoadSyncTypes();
        progressBar.ShowProgressInTaskBar = true;

        _configParams = DataSyncHelper.GetConfigParams(ObjGlobal.CompanyId, GetConnection.ConnectionString);
        if (!_configParams.Success || _configParams.Model.Item2 == null)
        {
            CustomMessageBox.ErrorMessage("Error fetching local-origin information. " + _configParams.ErrorMessage);
            _configParams.ShowErrorDialog();
            Close();
            return;
        }

        _localOriginId = _configParams.Model.Item1;
        _injectData = new DbSyncRepoInjectData
        {
            ExternalConnectionString = null,
            DateTime = DateTime.Now,
            LocalOriginId = _configParams.Model.Item1,
            LocalCompanyUnitId = ObjGlobal.SysCompanyUnitId,
            Username = ObjGlobal.LogInUser,
            LocalConnectionString = GetConnection.ConnectionString,
            LocalBranchId = ObjGlobal.SysBranchId,
            ApiConfig = DataSyncHelper.GetConfig(),
        };
        _frmSyncLogs.Show();
        _frmSyncLogs.BringToFront();
    }
    private async void BtnCheckUpdates_Click(object sender, EventArgs e)
    {
        if (_formBusy) return;

        _frmSyncLogs.BringToFront();
        _frmSyncLogs.ClearLog();
        progressBar.Position = 0;

        foreach (Control control in flpDataContainer.Controls) control.Dispose();
        flpDataContainer.Controls.Clear();

        var repoList = clbSyncItems.CheckedItems.Cast<ValueModel<SyncRepoType, string>>().ToList();
        _injectData.DateTime = DateTime.Now;

        // populate the new data in the grid
        var childMaxSize = new Size(splitContainerControl.Panel2.Width - 20,
            flpDataContainer.Height - 20);
        flpDataContainer.WrapContents = false;
        flpDataContainer.FlowDirection = FlowDirection.LeftToRight;

        var productRepo = DataSyncProviderFactory.GetRepository<ProductDataSync>(_injectData);
        var ledgerRepo = DataSyncProviderFactory.GetRepository<AccountLedgerDataSync>(_injectData);
        var salesRepo = DataSyncProviderFactory.GetRepository<SalesInvoiceDataSync>(_injectData);
        var salesReturnRepo = DataSyncProviderFactory.GetRepository<SalesReturnData>(_injectData);
        var purchRepo = DataSyncProviderFactory.GetRepository<PurchaseInvoiceDataSync>(_injectData);
        var purchReturnRepo = DataSyncProviderFactory.GetRepository<PurchaseReturnDataSync>(_injectData);

        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = false;
        SplashScreenManager.ShowDefaultWaitForm("Fetching data...");

        //if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Products) != null)
        //{
        //    _frmSyncLogs.AddLog("Fetching local origin products data...");
        //    var loResponse = await productRepo.GetLocalOriginDataAsync();
        //    if (!loResponse.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error.");
        //        SplashScreenManager.CloseForm();
        //        loResponse.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Done");

        //    _frmSyncLogs.AddLog("Fetching incoming products data...");
        //    var response = await productRepo.GetIncomingNewDataAsync(loResponse.Model);
        //    if (!response.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error.");
        //        SplashScreenManager.CloseForm();
        //        response.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Incoming products data fetch completed successfully.");
        //    progressBar.Increment(20);

        //    flpDataContainer.Controls.Add(
        //        new UcSyncDataViewer<Product>(response.Model.Products,
        //            "Products", childMaxSize.Width)
        //        {
        //            MaximumSize = childMaxSize,
        //            Size = childMaxSize
        //        });

        //    flpDataContainer.Controls.Add(
        //        new UcSyncDataViewer<ProductUnit>(response.Model.Units,
        //            "Units", childMaxSize.Width)
        //        {
        //            MaximumSize = childMaxSize,
        //            Size = childMaxSize
        //        });

        //    flpDataContainer.Controls.Add(
        //        new UcSyncDataViewer<ProductGroup>(response.Model.ProductGroups,
        //            "Product Groups", childMaxSize.Width)
        //        {
        //            MaximumSize = childMaxSize,
        //            Size = childMaxSize
        //        });

        //    flpDataContainer.Controls.Add(
        //        new UcSyncDataViewer<ProductSubGroup>(response.Model.SubGroups,
        //            "Product SubGroups", childMaxSize.Width)
        //        {
        //            MaximumSize = childMaxSize,
        //            Size = childMaxSize
        //        });
        //}

        //if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.AccountLedger) != null)
        //{
        //    _frmSyncLogs.AddLog("Fetching local origin account ledger data...");
        //    var loResponse = await ledgerRepo.GetLocalOriginDataAsync();
        //    if (!loResponse.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error.");
        //        SplashScreenManager.CloseForm();
        //        loResponse.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Done");

        //    _frmSyncLogs.AddLog("Fetching incoming new ledger data...");
        //    var response = await ledgerRepo.GetIncomingNewDataAsync(loResponse.Model);
        //    if (!response.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error");
        //        SplashScreenManager.CloseForm();
        //        response.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Incoming ledger data fetch completed successfully.");
        //    progressBar.Increment(20);

        //    flpDataContainer.Controls.Add(
        //        new UcSyncDataViewer<AccountGroup>(response.Model.AccountGroups,
        //            "Account Groups", childMaxSize.Width)
        //        {
        //            MaximumSize = childMaxSize,
        //            Size = childMaxSize
        //        });

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<GeneralLedger>(
        //        response.Model.GeneralLedgers,
        //        "General Ledgers", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });
        //}

        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.SalesInvoice) != null)
        {
            _frmSyncLogs.AddLog("Fetching local origin sales data...");
            var loResponse = await salesRepo.GetLocalOriginDataAsync();
            if (!loResponse.Success)
            {
                _frmSyncLogs.AddLog("Error.");
                SplashScreenManager.CloseForm();
                loResponse.ShowErrorDialog();
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                return;
            }

            _frmSyncLogs.AddLog("Done");

            _frmSyncLogs.AddLog("Fetching incoming sales data...");
            var salesResponse = await salesRepo.GetIncomingNewDataAsync(loResponse.Model);
            if (!salesResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                salesResponse.ShowErrorDialog();
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                return;
            }

            _frmSyncLogs.AddLog("Incoming sales data fetch completed successfully.");
            progressBar.Increment(20);

            flpDataContainer.Controls.Add(new UcSyncDataViewer<SB_Master>(
                salesResponse.Model.Masters,
                "Sales Master", childMaxSize.Width)
            {
                MaximumSize = childMaxSize,
                Size = childMaxSize
            });

            flpDataContainer.Controls.Add(new UcSyncDataViewer<SB_Details>(
                salesResponse.Model.Details,
                "Sales Details", childMaxSize.Width)
            {
                MaximumSize = childMaxSize,
                Size = childMaxSize
            });

            flpDataContainer.Controls.Add(new UcSyncDataViewer<SB_Term>(
                salesResponse.Model.Terms, "Sales Terms", childMaxSize.Width)
            {
                MaximumSize = childMaxSize,
                Size = childMaxSize
            });
        }

        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.SalesReturns) != null)
        {
            _frmSyncLogs.AddLog("Fetching local origin sales returns data...");
            var loResponse = await salesReturnRepo.GetLocalOriginDataAsync();
            if (!loResponse.Success)
            {
                _frmSyncLogs.AddLog("Error.");
                SplashScreenManager.CloseForm();
                loResponse.ShowErrorDialog();
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                return;
            }

            _frmSyncLogs.AddLog("Done");

            _frmSyncLogs.AddLog("Fetching incoming sales return data...");
            var response = await salesReturnRepo.GetIncomingNewDataAsync(loResponse.Model);
            if (!response.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                response.ShowErrorDialog();
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                return;
            }

            _frmSyncLogs.AddLog("Incoming sales returns data fetch completed successfully.");
            progressBar.Increment(20);

            flpDataContainer.Controls.Add(new UcSyncDataViewer<SR_Master>(response.Model.Masters,
                "Sales Return Master", childMaxSize.Width)
            {
                MaximumSize = childMaxSize,
                Size = childMaxSize
            });

            flpDataContainer.Controls.Add(new UcSyncDataViewer<SR_Details>(
                response.Model.Details, "Sales Return Details", childMaxSize.Width)
            {
                MaximumSize = childMaxSize,
                Size = childMaxSize
            });

            flpDataContainer.Controls.Add(new UcSyncDataViewer<SR_Term>(
                response.Model.Terms, "Sales Return Terms", childMaxSize.Width)
            {
                MaximumSize = childMaxSize,
                Size = childMaxSize
            });
        }

        //if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Purchase) != null)
        //{
        //    _frmSyncLogs.AddLog("Fetching local origin purchase data...");
        //    var loResponse = await purchRepo.GetLocalOriginDataAsync();
        //    if (!loResponse.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error.");
        //        SplashScreenManager.CloseForm();
        //        loResponse.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Done");

        //    _frmSyncLogs.AddLog("Fetching incoming purchase data...");
        //    var response = await purchRepo.GetIncomingNewDataAsync(loResponse.Model);
        //    if (!response.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error");
        //        SplashScreenManager.CloseForm();
        //        response.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Incoming purchase data fetch completed successfully.");
        //    progressBar.Increment(10);

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<PB_Master>(
        //        response.Model.Masters, "Purchase Master", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<PB_Details>(
        //        response.Model.Details, "Purchase Details", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<PB_Term>(
        //        response.Model.Terms, "Purchase Terms", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });
        //}

        //if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.PurchaseReturns) != null)
        //{
        //    _frmSyncLogs.AddLog("Fetching local origin purchase returns data...");
        //    var loResponse = await purchReturnRepo.GetLocalOriginDataAsync();
        //    if (!loResponse.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error.");
        //        SplashScreenManager.CloseForm();
        //        loResponse.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Done");

        //    _frmSyncLogs.AddLog("Fetching incoming purchase returns data...");
        //    var response = await purchReturnRepo.GetIncomingNewDataAsync(loResponse.Model);
        //    if (!response.Success)
        //    {
        //        _frmSyncLogs.AddLog("Error");
        //        SplashScreenManager.CloseForm();
        //        response.ShowErrorDialog();
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        return;
        //    }

        //    _frmSyncLogs.AddLog("Incoming purchase returns data fetch completed successfully.");
        //    progressBar.Increment(5);
        //    _frmSyncLogs.AddLog("Process completed.");

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<PR_Master>(
        //        response.Model.Masters, "Purchase Returns Master", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<PR_Details>(
        //        response.Model.Details,
        //        "Purchase Returns Details", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });

        //    flpDataContainer.Controls.Add(new UcSyncDataViewer<PR_Term>(
        //        response.Model.Terms, "Purchase Returns Terms", childMaxSize.Width)
        //    {
        //        MaximumSize = childMaxSize,
        //        Size = childMaxSize
        //    });
        //}

        SplashScreenManager.CloseForm();
        progressBar.Position = 100;
        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
    }
    private async void BtnSync_Click(object sender, EventArgs e)
    {
        if (_formBusy) return;

        if (clbSyncItems.CheckedItems.Count == 0) return;
        if (MessageBox.Show(@"Are you sure want to perform data sync. The operation can not be undone.", @"Confirmation", MessageBoxButtons.YesNo) != DialogResult.Yes)
        {
            return;
        }

        _frmSyncLogs.BringToFront();
        _frmSyncLogs.ClearLog();
        progressBar.Position = 0;
        _formBusy = true;
        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = false;

        _injectData.DateTime = DateTime.Now;
        var repoList = clbSyncItems.CheckedItems.Cast<ValueModel<SyncRepoType, string>>().ToList();
        SplashScreenManager.ShowDefaultWaitForm("Syncing local data...", "Please wait");

        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.AccountGroup) != null)
        {
            SyncAccountGroup();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Branch) != null)
        {
            SyncBranch();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.CompanyUnit) != null)
        {
            SyncCompanyUnit();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Currency) != null)
        {
            SyncCurrency();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.AccountSubGroup) != null)
        {
            SyncAccountSubGroup();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.MainAgent) != null)
        {
            SyncMainAgent();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Agent) != null)
        {
            SyncJuniorAgent();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.MainArea) != null)
        {
            SyncMainArea();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Area) != null)
        {
            SyncArea();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.GeneralLedger) != null)
        {
            SyncGeneralLedger();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.SubLedger) != null)
        {
            SyncSubLedger();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.GiftVoucherGenerate) != null)
        {
            SyncGiftVoucher();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.MemberType) != null)
        {
            SyncMemberType();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.MemberShipSetUp) != null)
        {
            SyncMemberShipSetup();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Department) != null)
        {
            SyncDepartment();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Terminal) != null)
        {
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}Counter/GetMemberCounterById",
                InsertUrl = @$"{_configParams.Model.Item2}Counter/InsertCounterList",
                UpdateUrl = @$"{_configParams.Model.Item2}Counter/UpdateCounter"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var counterRepo = DataSyncProviderFactory.GetRepository<Counter>(_injectData);

            // pull all new account data
            _frmSyncLogs.AddLog("Pulling new terminal data from server...");
            //var pullResponse = await accountGroupRepo.PullAllNewAsync();
            var pullResponse = await GetAndSaveUnSynchronizedCounters();
            if (!pullResponse)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return;
            }

            progressBar.Increment(5);
            _frmSyncLogs.AddLog("Success");
            // push all new account data
            var sqlCoQuery = @"SELECT *FROM AMS.Counter";
            var queryResponse = await QueryUtils.GetListAsync<Counter>(sqlCoQuery);
            var coList = queryResponse.List.ToList();
            if (coList.Count > 0)
            {
                _frmSyncLogs.AddLog("Pushing new local terminal data to server...");
                var pushResponse = await counterRepo.PushNewListAsync(coList);
                if (!pushResponse.Value)
                {
                    _frmSyncLogs.AddLog($"Error:{pushResponse.ErrorMessage}");
                    SplashScreenManager.CloseForm();
                    _formBusy = false;
                    btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                    pushResponse.ShowErrorDialog();
                    return;
                }

                progressBar.Increment(5);
                _frmSyncLogs.AddLog("Success");
            }
            //progressBar.Increment(5);
            //_frmSyncLogs.AddLog("Success");
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Floor) != null)
        {
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}Floor/GetFloorById",
                InsertUrl = @$"{_configParams.Model.Item2}Floor/InsertFloorList",
                UpdateUrl = @$"{_configParams.Model.Item2}Floor/UpdateFloor"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var floorRepo = DataSyncProviderFactory.GetRepository<FloorSetup>(_injectData);

            // pull all new account data
            _frmSyncLogs.AddLog("Pulling new floor data from server...");
            //var pullResponse = await accountGroupRepo.PullAllNewAsync();
            var pullResponse = await GetAndSaveUnSynchronizedFloors();
            if (!pullResponse)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return;
            }

            progressBar.Increment(5);
            _frmSyncLogs.AddLog("Success");
            // push all new account data
            var sqlFlQuery = @"SELECT *FROM AMS.Floor";
            var queryResponse = await QueryUtils.GetListAsync<FloorSetup>(sqlFlQuery);
            var flList = queryResponse.List.ToList();
            if (flList.Count > 0)
            {
                _frmSyncLogs.AddLog("Pushing new local floor data to server...");
                var pushResponse = await floorRepo.PushNewListAsync(flList);
                if (!pushResponse.Value)
                {
                    _frmSyncLogs.AddLog($"Error:{pushResponse.ErrorMessage}");
                    SplashScreenManager.CloseForm();
                    _formBusy = false;
                    btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                    pushResponse.ShowErrorDialog();
                    return;
                }

                progressBar.Increment(5);
                _frmSyncLogs.AddLog("Success");
            }

        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Table) != null)
        {
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}TableMaster/GetTableMastersByCallCount",
                InsertUrl = @$"{_configParams.Model.Item2}TableMaster/InsertTableMasterList",
                UpdateUrl = @$"{_configParams.Model.Item2}TableMaster/UpdateTableMaster"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var tableMasterRepo = DataSyncProviderFactory.GetRepository<TableMaster>(_injectData);

            // push all new table master data
            _frmSyncLogs.AddLog("Pushing new local table data to server...");
            var sqlTblQuery = @"SELECT *FROM AMS.TableMaster";
            var queryResponse = await QueryUtils.GetListAsync<TableMaster>(sqlTblQuery);
            var tblList = queryResponse.List.ToList();
            if (tblList.Count > 0)
            {
                var loopCount = 1;
                if (tblList.Count > 100)
                {
                    loopCount = tblList.Count / 100 + 1;
                }
                var newTblList = new List<TableMaster>();
                for (var i = 0; i < loopCount; i++)
                {
                    newTblList.Clear();
                    if (i == 0)
                    {
                        newTblList.AddRange(tblList.Take(100));
                    }
                    else
                    {
                        newTblList.AddRange(tblList.Skip(100 * i).Take(100));
                    }
                    var pushResponse = await tableMasterRepo.PushNewListAsync(newTblList);
                    if (!pushResponse.Value)
                    {
                        _frmSyncLogs.AddLog($"Error:{pushResponse.ErrorMessage}");
                        SplashScreenManager.CloseForm();
                        _formBusy = false;
                        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                        pushResponse.ShowErrorDialog();
                        return;
                    }
                }
            }
            progressBar.Increment(5);
            _frmSyncLogs.AddLog("Success");
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Godown) != null)
        {
            SyncGoDown();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.CostCenter) != null)
        {
            var apiConfig = new SyncApiConfig
            {
                BaseUrl = _configParams.Model.Item2,
                Apikey = _configParams.Model.Item3,
                Username = ObjGlobal.LogInUser,
                BranchId = ObjGlobal.SysBranchId,
                GetUrl = @$"{_configParams.Model.Item2}CostCenter/GetCostCenterById",
                InsertUrl = @$"{_configParams.Model.Item2}CostCenter/InsertCostCenterList",
                UpdateUrl = @$"{_configParams.Model.Item2}CostCenter/UpdateCostCenter"
            };

            DataSyncHelper.SetConfig(apiConfig);
            _injectData.ApiConfig = apiConfig;
            DataSyncManager.SetGlobalInjectData(_injectData);
            var costCenterRepo = DataSyncProviderFactory.GetRepository<CostCenter>(_injectData);
            // pull all new account data
            _frmSyncLogs.AddLog("Pulling new cost center data from server...");
            //var pullResponse = await accountGroupRepo.PullAllNewAsync();
            var pullResponse = await GetAndSaveUnSynchronizedCostCenters();
            if (!pullResponse)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return;
            }

            progressBar.Increment(5);
            _frmSyncLogs.AddLog("Success");
            // push all new account data
            var sqlQuery = @"SELECT *FROM AMS.CostCenter";
            var queryResponse = await QueryUtils.GetListAsync<CostCenter>(sqlQuery);
            var coList = queryResponse.List.ToList();
            if (coList.Count > 0)
            {
                _frmSyncLogs.AddLog("Pushing new local cost center data to server...");
                var pushResponse = await costCenterRepo.PushNewListAsync(coList);
                if (!pushResponse.Value)
                {
                    _frmSyncLogs.AddLog($"Error:{pushResponse.ErrorMessage}");
                    SplashScreenManager.CloseForm();
                    _formBusy = false;
                    btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                    pushResponse.ShowErrorDialog();
                    return;
                }

                progressBar.Increment(5);
                _frmSyncLogs.AddLog("Success");
            }
            //progressBar.Increment(5);
            //_frmSyncLogs.AddLog("Success");
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.ProductGroup) != null)
        {
            SyncProductGroup();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.ProductSubGroup) != null)
        {
            SyncProductSubGroup();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.ProductUnit) != null)
        {
            SyncProductUnit();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Product) != null)
        {
            SyncProduct();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Rack) != null)
        {
            SyncRack();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.NarrationAndRemarks) != null)
        {
            SyncNarrationRemarks();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.LedgerOpening) != null)
        {
            SyncLedgerOpening();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.ProductOpening) != null)
        {
            SyncProductOpening();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.PurchaseOrder) != null)
        {
            SyncPurchaseOrder();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.PurchaseChallan) != null)
        {
            SyncPurchaseChallan();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.PurchaseChallanReturn) != null)
        {
            SyncPurchaseChallanReturn();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.PurchaseInvoice) != null)
        {
            SyncPurchaseInvoice();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.PurchaseReturn) != null)
        {
            SyncPurchaseReturn();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.SalesChallan) != null)
        {
            SyncSalesChallan();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.SalesInvoice) != null)
        {
            SyncSalesInvoice();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.SalesReturns) != null)
        {
            SyncSalesReturn();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.JournalVoucher) != null)
        {
            SyncJournalVoucher();
        }
        if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.CashBankVoucher) != null)
        {
            SyncCashBankVoucher();
        }
    }
    private void BtnClose_Click(object sender, EventArgs e)
    {
        Close();
    }
    private void BtnImport_Click(object sender, EventArgs e)
    {
        //if (_formBusy) return;

        //_frmSyncLogs.BringToFront();
        //_frmSyncLogs.ClearLog();
        //progressBar.Position = 0;
        //_formBusy = true;
        //btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = false;

        //_injectData.DateTime = DateTime.Now;
        //var repoList = clbSyncItems.CheckedItems.Cast<ValueModel<SyncRepoType, string>>().ToList();

        //var productRepo = new ProductsImportRepository(_injectData);
        //var ledgerRepo = DataSyncProviderFactory.GetRepository<AccountLedgerDataSync>(_injectData);

        //SplashScreenManager.ShowDefaultWaitForm("Importing data from external source...", "Please wait");

        //if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.Products) != null)
        //{
        //    // pull all new products and associated records
        //    _frmSyncLogs.AddLog("Pulling new products data and updating the changes...");
        //    var pullResponse = await productRepo.PullAllNewAsync();
        //    if (!pullResponse.Value)
        //    {
        //        _frmSyncLogs.AddLog("Error");
        //        SplashScreenManager.CloseForm();
        //        _formBusy = false;
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        pullResponse.ShowErrorDialog();
        //        return;
        //    }
        //    progressBar.Increment(10);
        //    _frmSyncLogs.AddLog("Success");
        //}

        //if (repoList.FirstOrDefault(x => x.Item1 == SyncRepoType.AccountLedger) != null)
        //{
        //    // pull all new products and associated records
        //    _frmSyncLogs.AddLog("Pulling new products data and updating the changes...");
        //    var pullResponse = await productRepo.PullAllNewAsync();
        //    if (!pullResponse.Value)
        //    {
        //        _frmSyncLogs.AddLog("Error");
        //        SplashScreenManager.CloseForm();
        //        _formBusy = false;
        //        btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
        //        pullResponse.ShowErrorDialog();
        //        return;
        //    }
        //    progressBar.Increment(10);
        //    _frmSyncLogs.AddLog("Success");
        //}

        //btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
    }
    private void ChkMarkAll_CheckedChanged(object sender, EventArgs e)
    {
        clbSyncItems.ItemCheck -= ClbSyncItems_ItemCheck;
        clbSyncItems.ItemChecking -= ClbSyncItems_ItemChecking;
        if (chkMarkAll.Checked)
            clbSyncItems.CheckAll();
        else
            clbSyncItems.UnCheckAll();
        clbSyncItems.ItemCheck += ClbSyncItems_ItemCheck;
        clbSyncItems.ItemChecking += ClbSyncItems_ItemChecking;
    }
    private void ClbSyncItems_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
    {
        //CheckedListBoxControl checkedListBox = (CheckedListBoxControl)sender;
        //checkedListBox.ReadOnly = true;
        //CheckedListBoxItem item = (CheckedListBoxItem)checkedListBox.GetItem(e.Index);
        clbSyncItems.ItemCheck -= ClbSyncItems_ItemCheck;
        var data = (ValueModel<SyncRepoType, string>)clbSyncItems.SelectedItem;
        switch (data.Item1)
        {
            case SyncRepoType.AccountSubGroup:
                SetItemChecked([SyncRepoType.AccountGroup], e.State);
                break;

            case SyncRepoType.Agent:
                SetItemChecked([SyncRepoType.MainAgent], e.State);
                break;

            case SyncRepoType.Area:
                SetItemChecked([SyncRepoType.MainArea], e.State);
                break;

            case SyncRepoType.GeneralLedger:
                SetItemChecked(
                [
                    SyncRepoType.AccountGroup, SyncRepoType.AccountSubGroup, SyncRepoType.MainAgent,
                    SyncRepoType.Agent, SyncRepoType.MainArea, SyncRepoType.Area
                ], e.State);
                break;

            case SyncRepoType.SubLedger:
                SetItemChecked([
                        SyncRepoType.AccountGroup, SyncRepoType.AccountSubGroup, SyncRepoType.MainAgent,
                        SyncRepoType.Agent, SyncRepoType.MainArea, SyncRepoType.Area, SyncRepoType.GeneralLedger
                    ]
                    , e.State);
                break;

            case SyncRepoType.MemberShipSetUp:
                SetItemChecked([SyncRepoType.MemberType]
                    , e.State);
                break;

            case SyncRepoType.Table:
                SetItemChecked([SyncRepoType.Floor]
                    , e.State);
                break;

            case SyncRepoType.CostCenter:
                SetItemChecked([SyncRepoType.Godown]
                    , e.State);
                break;

            case SyncRepoType.ProductSubGroup:
                SetItemChecked([SyncRepoType.ProductGroup]
                    , e.State);
                break;

            case SyncRepoType.Product:
                SetItemChecked([SyncRepoType.ProductGroup, SyncRepoType.ProductSubGroup, SyncRepoType.ProductUnit]
                    , e.State);
                break;

            case SyncRepoType.PurchaseChallan:
                SetItemChecked([SyncRepoType.PurchaseOrder]
                    , e.State);
                break;

            case SyncRepoType.PurchaseChallanReturn:
                SetItemChecked([SyncRepoType.PurchaseOrder, SyncRepoType.PurchaseChallan]
                    , e.State);
                break;

            case SyncRepoType.PurchaseInvoice:
                SetItemChecked([SyncRepoType.PurchaseOrder, SyncRepoType.PurchaseChallan]
                    , e.State);
                break;

            case SyncRepoType.PurchaseReturn:
                SetItemChecked([
                        SyncRepoType.PurchaseOrder, SyncRepoType.PurchaseChallan,
                        SyncRepoType.PurchaseChallanReturn, SyncRepoType.PurchaseInvoice
                    ]
                    , e.State);
                break;

            case SyncRepoType.SalesInvoice:
                SetItemChecked([SyncRepoType.SalesChallan]
                    , e.State);
                break;
        }

        clbSyncItems.ItemCheck += ClbSyncItems_ItemCheck;
    }
    private void ClbSyncItems_ItemChecking(object sender, ItemCheckingEventArgs e)
    {
        var data = (ValueModel<SyncRepoType, string>)clbSyncItems.SelectedItem;
        switch (data.Item1)
        {
            case SyncRepoType.AccountGroup:
                e.Cancel = IsAllowToChangeState([SyncRepoType.AccountSubGroup, SyncRepoType.GeneralLedger]);
                break;

            case SyncRepoType.AccountSubGroup:
                e.Cancel = IsAllowToChangeState([SyncRepoType.GeneralLedger]);
                break;

            case SyncRepoType.MainAgent:
                e.Cancel = IsAllowToChangeState([SyncRepoType.Agent, SyncRepoType.GeneralLedger]);
                break;

            case SyncRepoType.Agent:
                e.Cancel = IsAllowToChangeState([SyncRepoType.GeneralLedger]);
                break;

            case SyncRepoType.MainArea:
                e.Cancel = IsAllowToChangeState([SyncRepoType.Area, SyncRepoType.GeneralLedger]);
                break;

            case SyncRepoType.Area:
                e.Cancel = IsAllowToChangeState([SyncRepoType.GeneralLedger]);
                break;

            case SyncRepoType.GeneralLedger:
                e.Cancel = IsAllowToChangeState([SyncRepoType.SubLedger]);
                break;

            case SyncRepoType.MemberType:
                e.Cancel = IsAllowToChangeState([SyncRepoType.MemberShipSetUp]);
                break;

            case SyncRepoType.Floor:
                e.Cancel = IsAllowToChangeState([SyncRepoType.Table]);
                break;

            case SyncRepoType.Godown:
                e.Cancel = IsAllowToChangeState([SyncRepoType.CostCenter]);
                break;

            case SyncRepoType.ProductGroup:
                e.Cancel = IsAllowToChangeState([SyncRepoType.ProductSubGroup, SyncRepoType.Product]);
                break;

            case SyncRepoType.ProductSubGroup:
                e.Cancel = IsAllowToChangeState([SyncRepoType.Product]);
                break;

            case SyncRepoType.ProductUnit:
                e.Cancel = IsAllowToChangeState([SyncRepoType.Product]);
                break;

            case SyncRepoType.PurchaseOrder:
                e.Cancel = IsAllowToChangeState([SyncRepoType.PurchaseChallan, SyncRepoType.PurchaseChallanReturn]);
                break;

            case SyncRepoType.PurchaseChallan:
                e.Cancel = IsAllowToChangeState([SyncRepoType.PurchaseInvoice, SyncRepoType.PurchaseChallanReturn]);
                break;

            case SyncRepoType.PurchaseInvoice:
                e.Cancel = IsAllowToChangeState([SyncRepoType.PurchaseReturn]);
                break;

            case SyncRepoType.SalesChallan:
                e.Cancel = IsAllowToChangeState([SyncRepoType.SalesInvoice]);
                break;
        }
    }
    #endregion

    // METHOD FOR THIS FORM

    #region ---------MASTER---------

    private async void SyncBranch()
    {
        var syncBranchDetails = await _branchSetupRepository.SyncBranchDetails();
        _frmSyncLogs.AddLog(syncBranchDetails ? "Success" : "Please try again!");
    }
    private async void SyncCompanyUnit()
    {
        var syncCompanyUnit = await _companyUnitRepository.SyncCompanyUnitDetailsAsync();
        _frmSyncLogs.AddLog(syncCompanyUnit ? "Success" : "Please try again!");

    }
    private async void SyncAccountGroup()
    {
        var syncAccountGroup = await _groupRepository.SyncAccountGroupDetailsAsync();
        _frmSyncLogs.AddLog(syncAccountGroup ? "Success" : "Please try again!");
    }
    private async void SyncAccountSubGroup()
    {
        var syncAccountSubGroup = await _subGroupRepository.SyncAccountSubGroupDetailsAsync();
        _frmSyncLogs.AddLog(syncAccountSubGroup ? "Success" : "Please try again!");

    }
    private async void SyncCurrency()
    {
        var syncCurrency = await _currencyRepository.SyncCurrencyDetailsAsync();
        _frmSyncLogs.AddLog(syncCurrency ? "Success" : "Please try again!");
    }
    private async void SyncDepartment()
    {
        var syncDepartment = await _departmentRepository.SyncDepartmentDetailsAsync();
        _frmSyncLogs.AddLog(syncDepartment ? "Success" : "Please try again!");
    }
    private async void SyncMainAgent()
    {
        var syncMainAgent = await _mainAgentRepository.SyncSeniorAgentDetailsAsync();
        _frmSyncLogs.AddLog(syncMainAgent ? "Success" : "Please try again!");

    }
    private async void SyncJuniorAgent()
    {
        var syncJuniorAgent = await _juniorAgentRepository.SyncJuniorAgentDetailsAsync();
        _frmSyncLogs.AddLog(syncJuniorAgent ? "Success" : "Please try again!");

    }
    private async void SyncMainArea()
    {
        var syncMainArea = await _mainAreaRepository.SyncMainAreaDetailsAsync();
        _frmSyncLogs.AddLog(syncMainArea ? "Success" : "Please try again!");

    }
    private async void SyncArea()
    {
        var syncArea = await _areaRepository.SyncAreaDetailsAsync();
        _frmSyncLogs.AddLog(syncArea ? "Success" : "Please try again!");

    }
    private async void SyncGeneralLedger()
    {
        var syncGeneralLedger = await _generalLedgerRepository.SyncGeneralLedgerDetailsAsync();
        _frmSyncLogs.AddLog(syncGeneralLedger ? "Success" : "Please try again!");

    }
    private async void SyncSubLedger()
    {
        var syncSubLedger = await _subLedgerRepository.SyncSubLedgerDetailsAsync();
        _frmSyncLogs.AddLog(syncSubLedger ? "Success" : "Please try again!");

    }
    private async void SyncProductGroup()
    {
        var syncProductGroup = await _productGroup.SyncProductGroupDetailsAsync();
        _frmSyncLogs.AddLog(syncProductGroup ? "Success" : "Please try again!");

    }
    private async void SyncProductSubGroup()
    {
        var syncProductSubGroup = await _productSubGroup.SyncProductSubGroupDetailsAsync();
        _frmSyncLogs.AddLog(syncProductSubGroup ? "Success" : "Please try again!");

    }
    private async void SyncProductUnit()
    {
        var syncProductUnit = await _productUnit.SyncProductUnitDetailsAsync();
        _frmSyncLogs.AddLog(syncProductUnit ? "Success" : "Please try again!");

    }
    private async void SyncProduct()
    {
        var syncProduct = await _product.SyncProductDetailsAsync();
        _frmSyncLogs.AddLog(syncProduct ? "Success" : "Please try again!");

    }
    private async void SyncRack()
    {
        var syncRack = await _rackRepository.SyncRackDetailsAsync();
        _frmSyncLogs.AddLog(syncRack ? "Success" : "Please try again!");

    }
    private async void SyncGiftVoucher()
    {
        var syncGiftVoucher = await _giftVoucherRepository.SyncGiftVoucherListAsync("");
        _frmSyncLogs.AddLog(syncGiftVoucher > 0 ? "Success" : "Please try again!");

    }
    private async void SyncMemberType()
    {
        var syncMemberType = await _memberTypeRepository.SyncMemberTypeAsync("");
        _frmSyncLogs.AddLog(syncMemberType > 0 ? "Success" : "Please try again!");

    }
    private async void SyncMemberShipSetup()
    {
        var syncMemberShip = await _membershipSetupRepository.SyncMembershipSetupAsync("");
        _frmSyncLogs.AddLog(syncMemberShip > 0 ? "Success" : "Please try again!");

    }
    private async void SyncGoDown()
    {
        var syncGodown = await _godownRepository.SyncGodownAsync("");
        _frmSyncLogs.AddLog(syncGodown > 0 ? "Success" : "Please try again!");

    }
    private async void SyncNarrationRemarks()
    {
        var syncNarrationRemarks = await _narrationRemarksRepository.SyncNarrationAsync("");
        _frmSyncLogs.AddLog(syncNarrationRemarks > 0 ? "Success" : "Please try again!");
    }
    private async void SyncCashBankVoucher()
    {
        var syncCashBankVoucher = await _cashBankVoucherRepository.SyncCashBankVoucherAsync("");
        _frmSyncLogs.AddLog(syncCashBankVoucher > 0 ? "Success" : "Please try again!");
    }
    private async void SyncJournalVoucher()
    {
        var syncJournalVoucher = await _journalVoucherRepository.SyncJournalVoucherAsync("");
        _frmSyncLogs.AddLog(syncJournalVoucher > 0 ? "Success" : "Please try again!");
    }

    #endregion

    #region ---------DATA ENTRY----------

    private async void SyncLedgerOpening()
    {
        var syncLedgerOpening = await _ledgerOpeningRepository.SyncLedgerOpeningDetailsAsync();
        _frmSyncLogs.AddLog(syncLedgerOpening ? "Success" : "Please try again!");

    }
    private async void SyncProductOpening()
    {
        var syncProductOpening = await _productOpeningRepository.SyncProductOpeningSetupAsync("");
        _frmSyncLogs.AddLog(syncProductOpening > 0 ? "Success" : "Please try again!");

    }
    private async void SyncPurchaseChallan()
    {
        var syncPurchaseChallan = await _purchaseChallanRepository.SyncPurchaseChallanDetailsAsync();
        _frmSyncLogs.AddLog(syncPurchaseChallan ? "Success" : "Please try again!");

    }
    private async void SyncSalesReturn()
    {
        var syncSalesReturn = await _salesReturnRepository.SyncSalesReturnAsync(false, "");
        _frmSyncLogs.AddLog(syncSalesReturn > 0 ? "Success" : "Please try again!");
    }
    private async void SyncSalesInvoice()
    {
        var syncSalesInvoice = await _salesInvoiceRepository.SyncSalesInvoiceAsync("");
        _frmSyncLogs.AddLog(syncSalesInvoice > 0 ? "Success" : "Please try again!");
    }
    private async void SyncSalesChallan()
    {
        var syncSalesChallan = await _salesChallanRepository.SyncSalesChallanAsync("");
        _frmSyncLogs.AddLog(syncSalesChallan > 0 ? "Success" : "Please try again!");
    }
    private async void SyncPurchaseChallanReturn()
    {
        var syncPurchaseChallanReturn = await _purchaseChallanReturnRepository.SyncPurchaseChallanReturnAsync("");
        _frmSyncLogs.AddLog(syncPurchaseChallanReturn > 0 ? "Success" : "Please try again!");
    }
    private async void SyncPurchaseInvoice()
    {
        var syncPurchaseInvoice = await _purchaseInvoiceRepository.SyncPurchaseInvoiceAsync("");
        _frmSyncLogs.AddLog(syncPurchaseInvoice > 0 ? "Success" : "Please try again!");
    }
    private async void SyncPurchaseReturn()
    {
        var syncPurchaseReturn = await _purchaseReturnRepository.SyncPurchaseReturnAsync("");
        _frmSyncLogs.AddLog(syncPurchaseReturn > 0 ? "Success" : "Please try again!");
    }
    private async void SyncPurchaseOrder()
    {
        var syncPurchaseOrder = await _purchaseOrderRepository.SyncPurchaseOrderAsync("");
        _frmSyncLogs.AddLog(syncPurchaseOrder > 0 ? "Success" : "Please try again!");
    }
    #endregion


    // PULL SALES CHALLAN INVOICE
    #region ---------- PULL SALES CHALLAN INVOICE ----------
    private async Task<bool> PullUnSyncSalesChallan(IDataSyncRepository<SC_Master> salesChallanNewRepo)
    {
        try
        {
            var pullResponse = await salesChallanNewRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    _dataEntry.SaveUnSyncSalesChallanFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncSalesChallan(salesChallanNewRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // PULL SALES INVOICE
    #region ---------- PULL SALES INVOICE ----------
    private async Task<bool> PullUnSyncSales(IDataSyncRepository<SB_Master> salesNewRepo)
    {
        try
        {
            var pullResponse = await salesNewRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    _dataEntry.SaveUnSyncSalesFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncSales(salesNewRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // PULL SALES RETURN INVOICE
    #region ---------- PULL SALES RETURN INVOICE
    private async Task<bool> PullUnSyncSalesReturn(IDataSyncRepository<SR_Master> salesReturnNewRepo)
    {
        try
        {
            var pullResponse = await salesReturnNewRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    _dataEntry.SaveUnSyncSalesReturnFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncSalesReturn(salesReturnNewRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion



    // PULL PURCHASE CHALLAN INVOICE
    #region ---------- PULL PURCHASE CHALLAN / CHALLAN RETURN INVOICE ----------
    private async Task<bool> PullUnSyncPurchaseChallan(IDataSyncRepository<PC_Master> purchaseChallanRepo)
    {
        try
        {
            var pullResponse = await purchaseChallanRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    //_purchaseDataEntry.SaveUnSyncPurchaseChallanFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncPurchaseChallan(purchaseChallanRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    private async Task<bool> PullUnSyncPurchaseChallanReturn(IDataSyncRepository<PCR_Master> purchaseChallanReturnRepo)
    {
        try
        {
            var pullResponse = await purchaseChallanReturnRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    //_purchaseDataEntry.SaveUnSyncPurchaseChallanReturnFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncPurchaseChallanReturn(purchaseChallanReturnRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // PULL PURCHASE INVOICE
    #region ---------- PULL PURCHASE INVOICE ----------
    private async Task<bool> PullUnSyncPurchaseInvoice(IDataSyncRepository<PB_Master> purchaseInvoiceRepo)
    {
        try
        {
            var pullResponse = await purchaseInvoiceRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    //_purchaseDataEntry.SaveUnSyncPurchaseInvoiceFromServerAsync(data, "SAVE");
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncPurchaseInvoice(purchaseInvoiceRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    // PULL JOURNAL VOUCHER
    #region ---------- PULL JOURNAL VOUCHER ----------
    private async Task<bool> PullUnSyncJournalVoucher(IDataSyncRepository<JV_Master> journalVoucherRepo)
    {
        try
        {
            var pullResponse = await journalVoucherRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    _financeEntry.SaveUnSyncJournalVoucherFromServerAsync(data);
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncJournalVoucher(journalVoucherRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // PULL CASH BANK VOUCHER
    #region ---------- PULL CASH & BANK VOUCHER ----------
    private async Task<bool> PullUnSyncCashBankVoucher(IDataSyncRepository<CB_Master> cashBankVoucherRepo)
    {
        try
        {
            var pullResponse = await cashBankVoucherRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var data in pullResponse.List)
                {
                    _financeEntry.SaveUnSyncCashBankVoucherFromServerAsync(data);
                }
            }

            if (pullResponse.IsReCall)
                await PullUnSyncCashBankVoucher(cashBankVoucherRepo);

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // PULL MEMBER TYPES
    #region ---------- PULL MEMBER TYPES ----------
    private async Task<bool> GetAndSaveUnSynchronizedMemberTypes()
    {
        try
        {
            //var memberTypeList = await _master.GetUnSynchronizedData("AMS.MemberType");
            //if (memberTypeList.List != null)
            //{
            //    foreach (var data in memberTypeList.List)
            //    {
            //        var memberTypeData = JsonConvert.DeserializeObject<MemberType>(data.JsonData);
            //        var actionTag = data.Action;
            //        // GroupId = _actionTag is "SAVE" ? GroupId.ReturnMaxIntId("AG", string.Empty) : GroupId;
            //        _master.ObjMemberType.MemberTypeId = memberTypeData.MemberTypeId;
            //        _master.ObjMemberType.MemberDesc = memberTypeData.MemberDesc;
            //        _master.ObjMemberType.MemberShortName = memberTypeData.MemberShortName;
            //        _master.ObjMemberType.Discount = memberTypeData.Discount;
            //        _master.ObjMemberType.BranchId = memberTypeData.BranchId;
            //        _master.ObjMemberType.CompanyUnitId = memberTypeData.CompanyUnitId;
            //        _master.ObjMemberType.EnterBy = memberTypeData.EnterBy;
            //        _master.ObjMemberType.EnterDate = memberTypeData.EnterDate;
            //        _master.ObjMemberType.ActiveStatus = memberTypeData.ActiveStatus;
            //        _master.ObjMemberType.SyncRowVersion = memberTypeData.SyncRowVersion;
            //        var result = _master.SaveMemberType(actionTag);
            //        if (result > 0)
            //        {
            //            //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
            //            //_master.ObjSyncLogDetail.SyncLogId = data.Id;
            //            //actionTag = "SAVE";
            //            //var response = await _master.SaveSyncLogDetails(actionTag);
            //        }
            //    }
            //}

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    private async Task<SyncRowsAffectedCount> PullMemberTypesByCallCount(IDataSyncRepository<MemberType> memberTypeRepo, int callCount)
    {
        var rows = new SyncRowsAffectedCount();
        int rowsInserted = 0;
        int rowsUpdated = 0;
        int rowsAffecated = 0;

        try
        {
            _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}MemberType/GetMemberTypesByCallCount?callCount=" + callCount;
            var pullResponse = await memberTypeRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                //return false;
                rows.Success = false;
                return rows;
            }
            else
            {
                //// comparing client database with server database data..
                //// 
                //// fetch all table data from client database..
                //var lstClientMemberTypes = new List<MemberType>();
                //var lstServerMemberTypes = new List<MemberType>();

                ////lstServerMemberTypes = pullResponse.Cast<MemberType>().ToList();

                //var clientAllDataQuery = $@"select * from AMS.MemberType";
                //var clientAllData = ExecuteCommand.ExecuteDataSetSql(clientAllDataQuery);
                //lstClientMemberTypes = clientAllData.ToList<MemberType>();

                //var diffList =new List<MemberType>();
                //diffList = lstClientMemberTypes.Except(lstServerMemberTypes).ToList();




                foreach (var memberTypeData in pullResponse.List)
                {
                    //var actionTag = "UPDATE";
                    //var query =
                    //    $@"select MemberShortName from AMS.MemberType where Lower(MemberShortName)='{memberTypeData.MemberShortName.ToLower()}'";
                    //var alreadyExistData = ExecuteCommand.ExecuteDataSetSql(query);
                    //if (alreadyExistData.Rows.Count == 0)
                    //{
                    //    query =
                    //       $@"select MemberTypeId from AMS.MemberType where MemberTypeId='{memberTypeData.MemberTypeId}'";
                    //    var alreadyExistData1 = ExecuteCommand.ExecuteDataSetSql(query);
                    //    if (alreadyExistData1.Rows.Count == 0)
                    //    {
                    //        actionTag = "SAVE";
                    //        rowsInserted++;
                    //    }
                    //}
                    //_master.ObjMemberType.MemberTypeId = memberTypeData.MemberTypeId;
                    //_master.ObjMemberType.NepaliDesc = memberTypeData.NepaliDesc;
                    //_master.ObjMemberType.MemberDesc = memberTypeData.MemberDesc;
                    //_master.ObjMemberType.MemberShortName = memberTypeData.MemberShortName;
                    //_master.ObjMemberType.Discount = memberTypeData.Discount;
                    //_master.ObjMemberType.BranchId = memberTypeData.BranchId;
                    //_master.ObjMemberType.CompanyUnitId = memberTypeData.CompanyUnitId;
                    //_master.ObjMemberType.EnterBy = memberTypeData.EnterBy;
                    //_master.ObjMemberType.EnterDate = memberTypeData.EnterDate;
                    //_master.ObjMemberType.ActiveStatus = memberTypeData.ActiveStatus;
                    //_master.ObjMemberType.SyncBaseId = memberTypeData.SyncBaseId;
                    //_master.ObjMemberType.SyncGlobalId = memberTypeData.SyncGlobalId;
                    //_master.ObjMemberType.SyncOriginId = memberTypeData.SyncOriginId;
                    //_master.ObjMemberType.SyncCreatedOn = memberTypeData.SyncCreatedOn;
                    //_master.ObjMemberType.SyncLastPatchedOn = memberTypeData.SyncLastPatchedOn;
                    //_master.ObjMemberType.SyncRowVersion = memberTypeData.SyncRowVersion;
                    //var result = _master.SaveMemberType(actionTag);


                    rowsAffecated++;
                    rowsUpdated++;
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMemberTypesByCallCount(memberTypeRepo, callCount);
            }


            rows.rowsAffecated = rowsAffecated;
            rows.rowsUpdated = rowsUpdated;
            rows.rowsInserted = rowsInserted;

            //return true;
            rows.Success = true;
            return rows;
        }
        catch (Exception e)
        {
            rows.Success = false;
            return rows;
        }
    }
    private async Task<SyncRowsAffectedCount> PullMemberTypesFromServerToClientDBByCallCount(IDataSyncRepository<MemberType> memberTypeRepo, int callCount)
    {
        var rows = new SyncRowsAffectedCount();
        int rowsInserted = 0;
        int rowsUpdated = 0;
        int rowsAffecated = 0;

        try
        {
            _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}MemberType/GetMemberTypesByCallCount?callCount=" + callCount;
            var pullResponse = await memberTypeRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                //return false;
                rows.Success = false;
                return rows;
            }
            else
            {
                var actionTag = "UPDATE";
                var query = $@"select * from AMS.MemberType";
                var alldata = SqlExtensions.ExecuteDataSetSql(query);
                foreach (var memberTypeData in pullResponse.List)
                {
                    //_master.ObjMemberType.MemberTypeId = memberTypeData.MemberTypeId;
                    //_master.ObjMemberType.NepaliDesc = memberTypeData.NepaliDesc;
                    //_master.ObjMemberType.MemberDesc = memberTypeData.MemberDesc;
                    //_master.ObjMemberType.MemberShortName = memberTypeData.MemberShortName;
                    //_master.ObjMemberType.Discount = memberTypeData.Discount;
                    //_master.ObjMemberType.BranchId = memberTypeData.BranchId;
                    //_master.ObjMemberType.CompanyUnitId = memberTypeData.CompanyUnitId;
                    //_master.ObjMemberType.EnterBy = memberTypeData.EnterBy;
                    //_master.ObjMemberType.EnterDate = memberTypeData.EnterDate;
                    //_master.ObjMemberType.ActiveStatus = memberTypeData.ActiveStatus;
                    //_master.ObjMemberType.SyncBaseId = memberTypeData.SyncBaseId;
                    //_master.ObjMemberType.SyncGlobalId = memberTypeData.SyncGlobalId;
                    //_master.ObjMemberType.SyncOriginId = memberTypeData.SyncOriginId;
                    //_master.ObjMemberType.SyncCreatedOn = memberTypeData.SyncCreatedOn;
                    //_master.ObjMemberType.SyncLastPatchedOn = memberTypeData.SyncLastPatchedOn;
                    //_master.ObjMemberType.SyncRowVersion = memberTypeData.SyncRowVersion;
                    ////var result = _master.SaveMemberType(actionTag);

                    //var alreadyExistData = alldata.Select("MemberTypeId= '" + memberTypeData.MemberTypeId + "'");
                    //if (alreadyExistData.Length > 0)
                    //{
                    //    //get SyncRowVersion from client database table
                    //    int ClientSyncRowVersionId = 1;
                    //    ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                    //    //update only server SyncRowVersion is greater than client database while data pulling from server
                    //    if (memberTypeData.SyncRowVersion > ClientSyncRowVersionId)
                    //    {
                    //        var result = _master.SaveMemberType(actionTag);
                    //        rowsUpdated++;
                    //    }
                    //}
                    //else
                    //{
                    //    actionTag = "SAVE";
                    //    var result = _master.SaveMemberType(actionTag);
                    //    rowsInserted++;
                    //}

                    rowsAffecated++;
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMemberTypesFromServerToClientDBByCallCount(memberTypeRepo, callCount);
            }


            rows.rowsAffecated = rowsAffecated;
            rows.rowsUpdated = rowsUpdated;
            rows.rowsInserted = rowsInserted;

            //return true;
            rows.Success = true;
            return rows;
        }
        catch (Exception e)
        {
            rows.Success = false;
            return rows;
        }
    }
    #endregion


    // PULL MEMBERSHIP
    #region ---------- PULL MEMBERSHIP ----------
    private async Task<bool> GetAndSaveUnSynchronizedMemberShips()
    {
        try
        {
            var memberShipSetUpList = await _master.GetUnSynchronizedData("AMS.MemberShipSetup");
            if (memberShipSetUpList.List != null)
            {
                foreach (var data in memberShipSetUpList.List)
                {
                    //var seniorAgentData = JsonConvert.DeserializeObject<MainAgent>(data.JsonData);
                    //var actionTag = data.Action;

                    //var result = _master.SaveMemberShip(actionTag);
                    //if (result > 0)
                    //{
                    //    //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                    //    //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                    //    //actionTag = "SAVE";
                    //    //var response = await _master.SaveSyncLogDetails(actionTag);
                    //}
                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    private async Task<bool> PullMemberShipsByCallCount(IDataSyncRepository<MemberShipSetup> memberShipRepo, int callCount)
    {
        try
        {
            _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}MemberShipSetup/GetMemberShipSetupsByCallCount?callCount=" + callCount;
            var pullResponse = await memberShipRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var memberShipData in pullResponse.List)
                {
                    //var actionTag = "UPDATE";
                    //var query =
                    //    $@"select MShipShortName from AMS.MemberShipSetup where Lower(MShipShortName)='{memberShipData.MShipShortName.ToLower()}'";
                    //var alreadyExistData = ExecuteCommand.ExecuteDataSetSql(query);
                    //if (alreadyExistData.Rows.Count == 0)
                    //{
                    //    query =
                    //       $@"select MShipId from AMS.MemberShipSetup where MShipId='{memberShipData.MShipId}'";
                    //    var alreadyExistData1 = ExecuteCommand.ExecuteDataSetSql(query);
                    //    if (alreadyExistData1.Rows.Count == 0)
                    //    {
                    //        actionTag = "SAVE";
                    //    }
                    //}
                    //_master.ObjMembershipSetup.MemberTypeId = memberShipData.MemberTypeId;
                    //_master.ObjMembershipSetup.MShipId = memberShipData.MShipId;
                    //_master.ObjMembershipSetup.MemberId = memberShipData.MemberId;
                    //_master.ObjMembershipSetup.NepaliDesc = memberShipData.NepaliDesc;
                    //_master.ObjMembershipSetup.MShipDesc = memberShipData.MShipDesc;
                    //_master.ObjMembershipSetup.MShipShortName = memberShipData.MShipShortName;
                    //_master.ObjMembershipSetup.PhoneNo = memberShipData.PhoneNo;
                    //_master.ObjMembershipSetup.PriceTag = memberShipData.PriceTag;
                    //_master.ObjMembershipSetup.LedgerId = memberShipData.LedgerId;
                    //_master.ObjMembershipSetup.EmailAdd = memberShipData.EmailAdd;
                    //_master.ObjMembershipSetup.MemberTypeId = memberShipData.MemberTypeId;
                    //_master.ObjMembershipSetup.BranchId = memberShipData.BranchId;
                    //_master.ObjMembershipSetup.CompanyUnitId = memberShipData.CompanyUnitId;
                    //_master.ObjMembershipSetup.MValidDate = memberShipData.MValidDate;
                    //_master.ObjMembershipSetup.MExpireDate = memberShipData.MExpireDate;
                    //_master.ObjMembershipSetup.EnterBy = memberShipData.EnterBy;
                    //_master.ObjMembershipSetup.EnterDate = memberShipData.EnterDate;
                    //_master.ObjMembershipSetup.ActiveStatus = memberShipData.ActiveStatus;
                    //_master.ObjMembershipSetup.SyncBaseId = memberShipData.SyncBaseId;
                    //_master.ObjMembershipSetup.SyncGlobalId = memberShipData.SyncGlobalId;
                    //_master.ObjMembershipSetup.SyncOriginId = memberShipData.SyncOriginId;
                    //_master.ObjMembershipSetup.SyncCreatedOn = memberShipData.SyncCreatedOn;
                    //_master.ObjMembershipSetup.SyncLastPatchedOn = memberShipData.SyncLastPatchedOn;
                    //_master.ObjMembershipSetup.SyncRowVersion = memberShipData.SyncRowVersion;
                    //var result = _master.SaveMemberShip(actionTag);
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMemberShipsByCallCount(memberShipRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    private async Task<bool> PullMemberShipsFromServerToClientDBByCallCount(IDataSyncRepository<MemberShipSetup> memberShipRepo, int callCount)
    {
        try
        {
            _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}MemberShipSetup/GetMemberShipSetupsByCallCount?callCount=" + callCount;
            var pullResponse = await memberShipRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                //var actionTag = "UPDATE";
                //var query = $@"select * from AMS.MemberShipSetup";
                //var alldata = ExecuteCommand.ExecuteDataSetSql(query);
                //foreach (var memberShipData in pullResponse.List)
                //{
                //    _master.ObjMembershipSetup.MemberTypeId = memberShipData.MemberTypeId;
                //    _master.ObjMembershipSetup.MShipId = memberShipData.MShipId;
                //    _master.ObjMembershipSetup.MemberId = memberShipData.MemberId;
                //    _master.ObjMembershipSetup.NepaliDesc = memberShipData.NepaliDesc;
                //    _master.ObjMembershipSetup.MShipDesc = memberShipData.MShipDesc;
                //    _master.ObjMembershipSetup.MShipShortName = memberShipData.MShipShortName;
                //    _master.ObjMembershipSetup.PhoneNo = memberShipData.PhoneNo;
                //    _master.ObjMembershipSetup.PriceTag = memberShipData.PriceTag;
                //    _master.ObjMembershipSetup.LedgerId = memberShipData.LedgerId;
                //    _master.ObjMembershipSetup.EmailAdd = memberShipData.EmailAdd;
                //    _master.ObjMembershipSetup.MemberTypeId = memberShipData.MemberTypeId;
                //    _master.ObjMembershipSetup.BranchId = memberShipData.BranchId;
                //    _master.ObjMembershipSetup.CompanyUnitId = memberShipData.CompanyUnitId;
                //    _master.ObjMembershipSetup.MValidDate = memberShipData.MValidDate;
                //    _master.ObjMembershipSetup.MExpireDate = memberShipData.MExpireDate;
                //    _master.ObjMembershipSetup.EnterBy = memberShipData.EnterBy;
                //    _master.ObjMembershipSetup.EnterDate = memberShipData.EnterDate;
                //    _master.ObjMembershipSetup.ActiveStatus = memberShipData.ActiveStatus;
                //    _master.ObjMembershipSetup.SyncBaseId = memberShipData.SyncBaseId;
                //    _master.ObjMembershipSetup.SyncGlobalId = memberShipData.SyncGlobalId;
                //    _master.ObjMembershipSetup.SyncOriginId = memberShipData.SyncOriginId;
                //    _master.ObjMembershipSetup.SyncCreatedOn = memberShipData.SyncCreatedOn;
                //    _master.ObjMembershipSetup.SyncLastPatchedOn = memberShipData.SyncLastPatchedOn;
                //    _master.ObjMembershipSetup.SyncRowVersion = memberShipData.SyncRowVersion;

                //    var alreadyExistData = alldata.Select("MShipId= '" + memberShipData.MShipId + "'");
                //    if (alreadyExistData.Length > 0)
                //    {
                //        //get SyncRowVersion from client database table
                //        int ClientSyncRowVersionId = 1;
                //        ClientSyncRowVersionId = Convert.ToInt32(alreadyExistData[0]["SyncRowVersion"]);

                //        //update only server SyncRowVersion is greater than client database while data pulling from server
                //        if (memberShipData.SyncRowVersion > ClientSyncRowVersionId)
                //        {
                //            var result = _master.SaveMemberShip(actionTag);
                //        }
                //    }
                //    else
                //    {
                //        actionTag = "SAVE";
                //        var result = _master.SaveMemberShip(actionTag);
                //    }
                //}
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullMemberShipsFromServerToClientDBByCallCount(memberShipRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion


    // PULL GIFT VOUCHER LIST
    #region ---------- PULL JUNIOR AGENT ----------
    private async Task<bool> GetAndSaveUnSynchronizedGiftVoucherLists()
    {
        try
        {
            var giftVoucherList = await _master.GetUnSynchronizedData("AMS.GiftVoucherList");
            if (giftVoucherList.List != null)
            {
                foreach (var data in giftVoucherList.List)
                {
                    //var giftVoucherListData = JsonConvert.DeserializeObject<GiftVoucherGenerate>(data.JsonData);
                    //var actionTag = data.Action;

                    //var result = _master.SaveGiftVoucherList(actionTag);
                    //if (result > 0)
                    //{
                    //    //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                    //    //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                    //    //actionTag = "SAVE";
                    //    //var response = await _master.SaveSyncLogDetails(actionTag);
                    //}
                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    private async Task<bool> PullGiftVoucherListsByCallCount(IDataSyncRepository<GiftVoucherList> giftVoucherListRepo, int callCount)
    {
        try
        {
            _injectData.ApiConfig.GetUrl = @$"{_configParams.Model.Item2}GiftVoucherList/GetGiftVoucherListsByCallCount?callCount=" + callCount;
            var pullResponse = await giftVoucherListRepo.GetUnSynchronizedDataAsync();
            if (!pullResponse.Success)
            {
                _frmSyncLogs.AddLog("Error");
                SplashScreenManager.CloseForm();
                _formBusy = false;
                btnCheckUpdates.Enabled = btnSync.Enabled = btnImport.Enabled = btnClose.Enabled = true;
                //pullResponse.ShowErrorDialog();
                return false;
            }
            else
            {
                foreach (var giftVoucherListData in pullResponse.List)
                {
                    //var actionTag = "UPDATE";
                    //var query =
                    //    $@"select VoucherId from AMS.GiftVoucherList where VoucherId='{giftVoucherListData.VoucherId}'";
                    //var alreadyExistData = ExecuteCommand.ExecuteDataSetSql(query);
                    //if (alreadyExistData.Rows.Count == 0)
                    //{
                    //    actionTag = "SAVE";
                    //}
                    //_master.GiftVoucherList.VoucherId = giftVoucherListData.VoucherId;
                    //_master.GiftVoucherList.CardNo = giftVoucherListData.CardNo;
                    //_master.GiftVoucherList.Description = giftVoucherListData.Description;
                    //_master.GiftVoucherList.VoucherType = giftVoucherListData.VoucherType;
                    //_master.GiftVoucherList.IssueAmount = giftVoucherListData.IssueAmount;
                    //_master.GiftVoucherList.IsUsed = giftVoucherListData.IsUsed;
                    //_master.GiftVoucherList.BalanceAmount = giftVoucherListData.BalanceAmount;
                    //_master.GiftVoucherList.BillAmount = giftVoucherListData.BillAmount;
                    //_master.GiftVoucherList.BranchId = giftVoucherListData.BranchId;
                    //_master.GiftVoucherList.CompanyUnitId = giftVoucherListData.CompanyUnitId;
                    //_master.GiftVoucherList.Status = giftVoucherListData.Status;
                    //_master.GiftVoucherList.EnterBy = giftVoucherListData.EnterBy;
                    //_master.GiftVoucherList.EnterDate = giftVoucherListData.EnterDate;
                    //_master.GiftVoucherList.SyncBaseId = giftVoucherListData.SyncBaseId;
                    //_master.GiftVoucherList.SyncGlobalId = giftVoucherListData.SyncGlobalId;
                    //_master.GiftVoucherList.SyncOriginId = giftVoucherListData.SyncOriginId;
                    //_master.GiftVoucherList.SyncCreatedOn = giftVoucherListData.SyncCreatedOn;
                    //_master.GiftVoucherList.SyncLastPatchedOn = giftVoucherListData.SyncLastPatchedOn;
                    //_master.GiftVoucherList.SyncRowVersion = giftVoucherListData.SyncRowVersion;
                    //var result = _master.SaveGiftVoucherList(actionTag);
                }
            }

            if (pullResponse.IsReCall)
            {
                callCount++;
                await PullGiftVoucherListsByCallCount(giftVoucherListRepo, callCount);
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    private async Task<bool> GetAndSaveUnSynchronizedCounters()
    {
        try
        {
            var counterList = await _master.GetUnSynchronizedData("AMS.Counter");
            if (counterList.List != null)
            {
                foreach (var data in counterList.List)
                {
                    //var counterData = JsonConvert.DeserializeObject<Counter>(data.JsonData);
                    //var actionTag = data.Action;
                    //_master.ObjCounter.CId = counterData.CId;
                    //_master.ObjCounter.CName = counterData.CName;
                    //_master.ObjCounter.CCode = counterData.CCode;
                    //_master.ObjCounter.Branch_ID = counterData.Branch_ID;
                    //_master.ObjCounter.Company_Id = counterData.Company_Id;
                    //_master.ObjCounter.Status = counterData.Status;
                    //_master.ObjCounter.EnterBy = counterData.EnterBy;
                    //_master.ObjCounter.EnterDate = counterData.EnterDate;
                    //_master.ObjCounter.Printer = counterData.Printer;
                    //_master.ObjCounter.SyncRowVersion = counterData.SyncRowVersion;
                    //var result = _master.SaveCounter(actionTag);
                    //if (result > 0)
                    //{
                    //    //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                    //    //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                    //    //actionTag = "SAVE";
                    //    //var response = await _master.SaveSyncLogDetails(actionTag);
                    //}
                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    private async Task<bool> GetAndSaveUnSynchronizedFloors()
    {
        try
        {
            var floorList = await _master.GetUnSynchronizedData("AMS.Floor");
            if (floorList.List != null)
            {
                foreach (var data in floorList.List)
                {
                    //var floorData = JsonConvert.DeserializeObject<FloorSetup>(data.JsonData);
                    //var actionTag = data.Action;
                    //_master.Floor.FloorId = floorData.FloorId;
                    //_master.Floor.Description = floorData.Description;
                    //_master.Floor.ShortName = floorData.ShortName;
                    //_master.Floor.Type = floorData.Type;
                    //_master.Floor.EnterBy = floorData.EnterBy;
                    //_master.Floor.EnterDate = floorData.EnterDate;
                    //_master.Floor.Branch_ID = floorData.Branch_ID;
                    //_master.Floor.Company_Id = floorData.Company_Id;
                    //_master.Floor.Status = floorData.Status;
                    //_master.Floor.SyncRowVersion = floorData.SyncRowVersion;

                    //var result = _master.SaveFloor(actionTag);
                    //if (result > 0)
                    //{
                    //    //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                    //    //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                    //    //actionTag = "SAVE";
                    //    //var response = await _master.SaveSyncLogDetails(actionTag);
                    //}
                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    private async Task<bool> GetAndSaveUnSynchronizedCostCenters()
    {
        try
        {
            var costCenterList = await _master.GetUnSynchronizedData("AMS.CostCenter");
            if (costCenterList.List != null)
            {
                foreach (var data in costCenterList.List)
                {
                    var costCenterData = JsonConvert.DeserializeObject<CostCenter>(data.JsonData);
                    var actionTag = data.Action;
                    if (costCenterData != null)
                    {
                        _costCenterRepository.ObjCostCenter.CCId = costCenterData.CCId;
                        _costCenterRepository.ObjCostCenter.CCName = costCenterData.CCName;
                        _costCenterRepository.ObjCostCenter.CCcode = costCenterData.CCcode;
                        _costCenterRepository.ObjCostCenter.Branch_ID = costCenterData.Branch_ID;
                        _costCenterRepository.ObjCostCenter.Company_Id = costCenterData.Company_Id;
                        _costCenterRepository.ObjCostCenter.Status = costCenterData.Status;
                        _costCenterRepository.ObjCostCenter.EnterBy = costCenterData.EnterBy;
                        _costCenterRepository.ObjCostCenter.EnterDate = costCenterData.EnterDate;
                        _costCenterRepository.ObjCostCenter.SyncRowVersion = costCenterData.SyncRowVersion;
                        _costCenterRepository.ObjCostCenter.GodownId = costCenterData.GodownId;
                    }

                    var result = _costCenterRepository.SaveCostCenter(actionTag);
                    if (result <= 0)
                    {
                        continue;
                    }

                    //_master.ObjSyncLogDetail.BranchId = ObjGlobal.SysBranchId;
                    //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                    //actionTag = "SAVE";
                    //var response = await _master.SaveSyncLogDetails(actionTag);

                }
            }

            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }

    // PULL NARRATION
    #region ---------- PULL NARRATION ----------
    private async Task<bool> GetAndSaveUnSynchronizedNarration()
    {
        try
        {
            var narrationList = await _master.GetUnSynchronizedData("AMS.NrMaster");
            if (narrationList.List != null)
            {
                foreach (var data in narrationList.List)
                {
                    var narrationData = JsonConvert.DeserializeObject<NR_Master>(data.JsonData);
                    var actionTag = data.Action;
                    _nrMasterRepository.Narration.NRID = narrationData.NRID;
                    _nrMasterRepository.Narration.NRDESC = narrationData.NRDESC;
                    _nrMasterRepository.Narration.NRTYPE = narrationData.NRTYPE;
                    _nrMasterRepository.Narration.IsActive = narrationData.IsActive;
                    _nrMasterRepository.Narration.EnterBy = narrationData.EnterBy;
                    _nrMasterRepository.Narration.EnterDate = narrationData.EnterDate;
                    _nrMasterRepository.Narration.BranchId = narrationData.BranchId;
                    _nrMasterRepository.Narration.CompanyUnitId = narrationData.CompanyUnitId;
                    _nrMasterRepository.Narration.SyncBaseId = narrationData.SyncBaseId;
                    _nrMasterRepository.Narration.SyncGlobalId = narrationData.SyncGlobalId;
                    _nrMasterRepository.Narration.SyncOriginId = narrationData.SyncOriginId;
                    _nrMasterRepository.Narration.SyncCreatedOn = narrationData.SyncCreatedOn;
                    _nrMasterRepository.Narration.SyncLastPatchedOn = narrationData.SyncLastPatchedOn;
                    _nrMasterRepository.Narration.SyncRowVersion = narrationData.SyncRowVersion;
                    var result = _nrMasterRepository.SaveNarration(actionTag);
                    if (result > 0)
                    {
                        //_master.ObjSyncLogDetail.BranchId = data.BranchId;
                        //_master.ObjSyncLogDetail.SyncLogId = data.Id;
                        //actionTag = "SAVE";
                        //var response = await _master.SaveSyncLogDetails(actionTag);
                    }
                }
            }
            return true;
        }
        catch (Exception e)
        {
            var msg = e.Message;
            e.ToNonQueryErrorResult(e.StackTrace);
            return false;
        }
    }
    #endregion


    // METHOD FOR THIS FORM
    private void LoadSyncTypes()
    {
        var syncTypes = Enum.GetValues(typeof(SyncRepoType));
        var list = (from SyncRepoType type in syncTypes select new ValueModel<SyncRepoType, string>(type, type.SplitCamelCase())).ToList();

        clbSyncItems.DataSource = list;
        clbSyncItems.DisplayMember = "Item2";
        clbSyncItems.ValueMember = "Item1";
        foreach (var d in list)
        {
            clbSyncItems.Items.Add(d);
        }
    }
    private async Task<SyncApiConfig> FetchUrlParamsAsync(string baseUrl)
    {
        var client = new HttpClient(new CompressHandler(), true);
        try
        {
            client.BaseAddress = new Uri(baseUrl);
            var json = await client.GetStringAsync("datasync/config");

            var jObject = JObject.Parse(json);
            var success = (bool)jObject["Success"];

            if (success)
            {
                var token = jObject.SelectToken("Model");
                if (token != null)
                {
                    var model = new SyncApiConfig
                    {
                        BaseUrl = baseUrl,
                        GetUrl = token.Value<string>("FetchRoute"),
                        InsertUrl = token.Value<string>("PushRoute"),
                        UpdateUrl = token.Value<string>("PatchRoute")
                    };

                    return model;
                }
            }

            MessageBox.Show(@"Unable to fetch the url configs.", @"Error");
        }
        catch (Exception e)
        {
            e.ToNonQueryErrorResult(e.StackTrace).ShowErrorDialog();
        }

        return null;
    }
    private void SetItemChecked(List<SyncRepoType> syncRepoTypes, CheckState state)
    {
        foreach (var syncRepoType in syncRepoTypes)
        {
            clbSyncItems.SetItemCheckState((int)syncRepoType, state);
        }
        clbSyncItems.Refresh();
    }
    private bool IsAllowToChangeState(List<SyncRepoType> syncRepoTypes)
    {
        var isChecked = false;
        foreach (var syncRep in syncRepoTypes)
        {
            switch (syncRep)
            {
                case SyncRepoType.AccountSubGroup:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.Agent:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.Area:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.GeneralLedger:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.SubLedger:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.MemberShipSetUp:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.Table:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.CostCenter:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.ProductSubGroup:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.Product:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.PurchaseChallan:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.PurchaseChallanReturn:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.PurchaseInvoice:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.PurchaseReturn:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;

                case SyncRepoType.SalesInvoice:
                    isChecked = clbSyncItems.GetItemChecked((int)syncRep);
                    break;
            }

            if (isChecked) break;
        }

        return isChecked;
    }
    private void labelControl1_Click(object sender, EventArgs e)
    {

    }


    // OBJECT FOR THIS FORM
    #region ---------- OBJECT FOR THIS FORM ----------
    private bool _formBusy;
    private string _localOriginId;
    private readonly IMasterSetup _master;
    private readonly IAccountSubGroupRepository _subGroupRepository;
    private readonly IAccountGroupRepository _groupRepository;
    private readonly IBranchSetupRepository _branchSetupRepository;
    private readonly ICompanyUnitSetupRepository _companyUnitRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IGeneralLedgerRepository _generalLedgerRepository;
    private readonly IJuniorAgentRepository _juniorAgentRepository;
    private readonly IMainAreaRepository _mainAreaRepository;
    private readonly INarrationRemarksRepository _nrMasterRepository;
    private readonly IMainAgentRepository _mainAgentRepository;
    private readonly IAreaRepository _areaRepository;
    private readonly ISubLedgerRepository _subLedgerRepository;
    private readonly IProductGroupRepository _productGroup;
    private readonly IGodownRepository _godownRepository;
    private readonly IRackRepository _rackRepository;
    private readonly IProductRepository _product;
    private readonly IProductSubGroupRepository _productSubGroup;
    private readonly IProductUnitRepository _productUnit;
    private readonly ICostCenterRepository _costCenterRepository;
    private readonly ISalesEntry _dataEntry;
    private readonly IPurchaseEntry _purchaseDataEntry;
    private readonly IPurchaseOrderRepository _orderRepository;
    private readonly IFinanceEntry _financeEntry;
    private readonly ILedgerOpeningRepository _ledgerOpeningRepository;
    private readonly IPurchaseChallanRepository _purchaseChallanRepository;
    private readonly IGiftVoucherRepository _giftVoucherRepository;
    private readonly IMemberTypeRepository _memberTypeRepository;
    private readonly IMembershipSetupRepository _membershipSetupRepository;
    private readonly INarrationRemarksRepository _narrationRemarksRepository;
    private readonly ICashBankVoucherRepository _cashBankVoucherRepository;
    private readonly IJournalVoucherRepository _journalVoucherRepository;
    private readonly ISalesReturn _salesReturnRepository;
    private readonly ISalesInvoiceRepository _salesInvoiceRepository;
    private readonly ISalesChallanRepository _salesChallanRepository;
    private readonly IPurchaseInvoice _purchaseInvoiceRepository;
    private readonly IPurchaseReturn _purchaseReturnRepository;
    private readonly IPurchaseOrderRepository _purchaseOrderRepository;
    private readonly IPurchaseChallanReturn _purchaseChallanReturnRepository;
    private readonly IProductOpeningRepository _productOpeningRepository;

    private InfoResult<ValueModel<string, string, Guid>> _configParams;
    private readonly FrmSyncLogs _frmSyncLogs;
    private DbSyncRepoInjectData _injectData;
    #endregion
}