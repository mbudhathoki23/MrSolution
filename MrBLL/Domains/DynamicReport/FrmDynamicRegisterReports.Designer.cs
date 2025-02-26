using System;

namespace MrBLL.Domains.DynamicReport
{
    partial class FrmDynamicRegisterReports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        [Obsolete("Obsolete")]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.BtnSaveTemplate = new DevExpress.XtraBars.BarButtonItem();
            this.BtnSaveAsTemplate = new DevExpress.XtraBars.BarButtonItem();
            this.BtnRemoveTemplate = new DevExpress.XtraBars.BarButtonItem();
            this.skinRibbonGalleryBarItem1 = new DevExpress.XtraBars.SkinRibbonGalleryBarItem();
            this.BtnPrintPreview = new DevExpress.XtraBars.BarButtonItem();
            this.BtnPrintReport = new DevExpress.XtraBars.BarButtonItem();
            this.MnuExportToExcel = new DevExpress.XtraBars.BarButtonItem();
            this.MnuCheckFooter = new DevExpress.XtraBars.BarCheckItem();
            this.BtnFilterReports = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupNavigation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.ElementsControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.RegisterReport = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesMaster = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesInvoiceMaster = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesInvoiceDetails = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesInvoiceMasterDetails = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.UcMnuSalesInvoiceLedgerWise = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesInvoiceTableWise = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesInvoicePartialMaster = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.SalesSeprate = new DevExpress.XtraBars.Navigation.AccordionControlSeparator();
            this.uMnuSalesVatRegister = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesVatRegisterIncludeReturn = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuSalesReturnVatRegister = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.MnuSalesVatRegisterCustomerWise = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuPurchaseMaster = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuPurchaseInvoiceSummery = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuPurchaseInvoiceDetails = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuPurchaseInvoiceMasterDetails = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.accordionControlSeparator2 = new DevExpress.XtraBars.Navigation.AccordionControlSeparator();
            this.uMnuPurchaseVatRegister = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuPurchaseVatRegisterIncludeReturn = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuPurchaseReturnVatRegister = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.PartyLedger = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.AceCustomerLedger = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.AceVendorLedger = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.AceVendorCurrencyLedger = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.uMnuSalesInvoiceYearMaster = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanel.SuspendLayout();
            this.dockPanel_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ElementsControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.AutoHideEmptyItems = true;
            this.ribbonControl.AutoSaveLayoutToXml = true;
            this.ribbonControl.AutoSizeItems = true;
            this.ribbonControl.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.ribbonControl.SearchEditItem,
            this.BtnSaveTemplate,
            this.BtnSaveAsTemplate,
            this.BtnRemoveTemplate,
            this.skinRibbonGalleryBarItem1,
            this.BtnPrintPreview,
            this.BtnPrintReport,
            this.MnuExportToExcel,
            this.MnuCheckFooter,
            this.BtnFilterReports});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 61;
            this.ribbonControl.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage});
            this.ribbonControl.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl.ShowItemCaptionsInCaptionBar = true;
            this.ribbonControl.ShowItemCaptionsInPageHeader = true;
            this.ribbonControl.ShowItemCaptionsInQAT = true;
            this.ribbonControl.Size = new System.Drawing.Size(1267, 158);
            this.ribbonControl.StatusBar = this.ribbonStatusBar;
            this.ribbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // BtnSaveTemplate
            // 
            this.BtnSaveTemplate.Caption = "SAVE";
            this.BtnSaveTemplate.Id = 48;
            this.BtnSaveTemplate.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSaveTemplate.Name = "BtnSaveTemplate";
            this.BtnSaveTemplate.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.BtnSaveTemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSaveTemplate_ItemClick);
            // 
            // BtnSaveAsTemplate
            // 
            this.BtnSaveAsTemplate.Caption = "SAVE AS";
            this.BtnSaveAsTemplate.Id = 49;
            this.BtnSaveAsTemplate.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSaveAsTemplate.Name = "BtnSaveAsTemplate";
            this.BtnSaveAsTemplate.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.BtnSaveAsTemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSaveAsTemplate_ItemClick);
            // 
            // BtnRemoveTemplate
            // 
            this.BtnRemoveTemplate.Caption = "REMOVE";
            this.BtnRemoveTemplate.Id = 50;
            this.BtnRemoveTemplate.ImageOptions.Image = global::MrBLL.Properties.Resources.Delete;
            this.BtnRemoveTemplate.Name = "BtnRemoveTemplate";
            this.BtnRemoveTemplate.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.BtnRemoveTemplate.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnRemoveTemplate_ItemClick);
            // 
            // skinRibbonGalleryBarItem1
            // 
            this.skinRibbonGalleryBarItem1.Caption = "skinRibbonGalleryBarItem1";
            this.skinRibbonGalleryBarItem1.Id = 51;
            this.skinRibbonGalleryBarItem1.Name = "skinRibbonGalleryBarItem1";
            // 
            // BtnPrintPreview
            // 
            this.BtnPrintPreview.Caption = "PRINT PREVIEW";
            this.BtnPrintPreview.Id = 54;
            this.BtnPrintPreview.ImageOptions.Image = global::MrBLL.Properties.Resources.Print_16;
            this.BtnPrintPreview.Name = "BtnPrintPreview";
            this.BtnPrintPreview.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.BtnPrintPreview.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnPrintPreview_ItemClick);
            // 
            // BtnPrintReport
            // 
            this.BtnPrintReport.Caption = "PRINT REPORT";
            this.BtnPrintReport.Id = 55;
            this.BtnPrintReport.ImageOptions.Image = global::MrBLL.Properties.Resources.Printer24;
            this.BtnPrintReport.Name = "BtnPrintReport";
            this.BtnPrintReport.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // MnuExportToExcel
            // 
            this.MnuExportToExcel.Caption = "EXPORT TO EXCEL";
            this.MnuExportToExcel.Id = 56;
            this.MnuExportToExcel.ImageOptions.Image = global::MrBLL.Properties.Resources.Excel16;
            this.MnuExportToExcel.Name = "MnuExportToExcel";
            this.MnuExportToExcel.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.MnuExportToExcel.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuExportToExcel_ItemClick);
            // 
            // MnuCheckFooter
            // 
            this.MnuCheckFooter.Caption = "ENABLE FOOTER";
            this.MnuCheckFooter.Id = 59;
            this.MnuCheckFooter.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuCheckFooter.Name = "MnuCheckFooter";
            this.MnuCheckFooter.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.MnuCheckFooter.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuCheckFooter_CheckedChanged);
            // 
            // BtnFilterReports
            // 
            this.BtnFilterReports.Caption = "Filter Reports";
            this.BtnFilterReports.Id = 60;
            this.BtnFilterReports.ImageOptions.Image = global::MrBLL.Properties.Resources.find;
            this.BtnFilterReports.Name = "BtnFilterReports";
            this.BtnFilterReports.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.BtnFilterReports.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnFilterReports_ItemClick);
            // 
            // ribbonPage
            // 
            this.ribbonPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupNavigation,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3,
            this.ribbonPageGroup});
            this.ribbonPage.Name = "ribbonPage";
            this.ribbonPage.Text = "View";
            // 
            // ribbonPageGroupNavigation
            // 
            this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnSaveTemplate);
            this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnSaveAsTemplate);
            this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnRemoveTemplate);
            this.ribbonPageGroupNavigation.ItemLinks.Add(this.BtnFilterReports);
            this.ribbonPageGroupNavigation.Name = "ribbonPageGroupNavigation";
            this.ribbonPageGroupNavigation.Text = "Reports Type";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.BtnPrintPreview);
            this.ribbonPageGroup2.ItemLinks.Add(this.BtnPrintReport);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Print Tools";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.MnuExportToExcel);
            this.ribbonPageGroup3.ItemLinks.Add(this.MnuCheckFooter);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Report Export";
            // 
            // ribbonPageGroup
            // 
            this.ribbonPageGroup.AllowTextClipping = false;
            this.ribbonPageGroup.CaptionButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonPageGroup.ItemLinks.Add(this.skinRibbonGalleryBarItem1);
            this.ribbonPageGroup.Name = "ribbonPageGroup";
            this.ribbonPageGroup.Text = "Appearance";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 718);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1267, 24);
            // 
            // dockManager
            // 
            this.dockManager.Form = this;
            this.dockManager.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel});
            this.dockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.StatusBar",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane"});
            // 
            // dockPanel
            // 
            this.dockPanel.Controls.Add(this.dockPanel_Container);
            this.dockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dockPanel.ID = new System.Guid("6b5eb78e-eb2b-462d-8862-6c910cce070e");
            this.dockPanel.Location = new System.Drawing.Point(0, 158);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.OriginalSize = new System.Drawing.Size(357, 200);
            this.dockPanel.Size = new System.Drawing.Size(357, 560);
            this.dockPanel.Text = "Navigation";
            // 
            // dockPanel_Container
            // 
            this.dockPanel_Container.Controls.Add(this.ElementsControl);
            this.dockPanel_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel_Container.Name = "dockPanel_Container";
            this.dockPanel_Container.Size = new System.Drawing.Size(350, 531);
            this.dockPanel_Container.TabIndex = 0;
            // 
            // ElementsControl
            // 
            this.ElementsControl.AllowItemSelection = true;
            this.ElementsControl.Appearance.AccordionControl.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.AccordionControl.Options.UseFont = true;
            this.ElementsControl.Appearance.Group.Default.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Group.Default.Options.UseFont = true;
            this.ElementsControl.Appearance.Group.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Group.Disabled.Options.UseFont = true;
            this.ElementsControl.Appearance.Group.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Group.Hovered.Options.UseFont = true;
            this.ElementsControl.Appearance.Group.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Group.Normal.Options.UseFont = true;
            this.ElementsControl.Appearance.Group.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Group.Pressed.Options.UseFont = true;
            this.ElementsControl.Appearance.Hint.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Hint.Options.UseFont = true;
            this.ElementsControl.Appearance.Item.Default.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Item.Default.Options.UseFont = true;
            this.ElementsControl.Appearance.Item.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Item.Disabled.Options.UseFont = true;
            this.ElementsControl.Appearance.Item.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Item.Hovered.Options.UseFont = true;
            this.ElementsControl.Appearance.Item.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Item.Normal.Options.UseFont = true;
            this.ElementsControl.Appearance.Item.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.Item.Pressed.Options.UseFont = true;
            this.ElementsControl.Appearance.ItemWithContainer.Default.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.ItemWithContainer.Default.Options.UseFont = true;
            this.ElementsControl.Appearance.ItemWithContainer.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.ItemWithContainer.Disabled.Options.UseFont = true;
            this.ElementsControl.Appearance.ItemWithContainer.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.ItemWithContainer.Hovered.Options.UseFont = true;
            this.ElementsControl.Appearance.ItemWithContainer.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.ItemWithContainer.Normal.Options.UseFont = true;
            this.ElementsControl.Appearance.ItemWithContainer.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.ElementsControl.Appearance.ItemWithContainer.Pressed.Options.UseFont = true;
            this.ElementsControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ElementsControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.RegisterReport,
            this.PartyLedger});
            this.ElementsControl.Location = new System.Drawing.Point(0, 0);
            this.ElementsControl.Name = "ElementsControl";
            this.ElementsControl.RootDisplayMode = DevExpress.XtraBars.Navigation.AccordionControlRootDisplayMode.Footer;
            this.ElementsControl.Size = new System.Drawing.Size(350, 531);
            this.ElementsControl.TabIndex = 0;
            this.ElementsControl.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            this.ElementsControl.ElementClick += new DevExpress.XtraBars.Navigation.ElementClickEventHandler(this.AccordionControl_ElementClick);
            // 
            // RegisterReport
            // 
            this.RegisterReport.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.uMnuSalesMaster,
            this.uMnuPurchaseMaster});
            this.RegisterReport.Expanded = true;
            this.RegisterReport.HeaderVisible = false;
            this.RegisterReport.Name = "RegisterReport";
            this.RegisterReport.Text = "Register Report Templete";
            // 
            // uMnuSalesMaster
            // 
            this.uMnuSalesMaster.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesMaster.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesMaster.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.uMnuSalesInvoiceMaster,
            this.uMnuSalesInvoiceDetails,
            this.uMnuSalesInvoiceMasterDetails,
            this.UcMnuSalesInvoiceLedgerWise,
            this.uMnuSalesInvoiceTableWise,
            this.uMnuSalesInvoicePartialMaster,
            this.uMnuSalesInvoiceYearMaster,
            this.SalesSeprate,
            this.uMnuSalesVatRegister,
            this.uMnuSalesVatRegisterIncludeReturn,
            this.uMnuSalesReturnVatRegister,
            this.MnuSalesVatRegisterCustomerWise});
            this.uMnuSalesMaster.Expanded = true;
            this.uMnuSalesMaster.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.uMnuSalesMaster.Name = "uMnuSalesMaster";
            this.uMnuSalesMaster.Text = "Sales Master";
            // 
            // uMnuSalesInvoiceMaster
            // 
            this.uMnuSalesInvoiceMaster.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMaster.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesInvoiceMaster.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMaster.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesInvoiceMaster.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMaster.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesInvoiceMaster.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMaster.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesInvoiceMaster.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMaster.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesInvoiceMaster.ControlFooterAlignment = DevExpress.XtraBars.Navigation.AccordionItemFooterAlignment.Far;
            this.uMnuSalesInvoiceMaster.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl)});
            this.uMnuSalesInvoiceMaster.Name = "uMnuSalesInvoiceMaster";
            this.uMnuSalesInvoiceMaster.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesInvoiceMaster.Text = "Sales Invoice Summary";
            // 
            // uMnuSalesInvoiceDetails
            // 
            this.uMnuSalesInvoiceDetails.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceDetails.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesInvoiceDetails.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceDetails.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesInvoiceDetails.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceDetails.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesInvoiceDetails.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceDetails.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesInvoiceDetails.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceDetails.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesInvoiceDetails.Name = "uMnuSalesInvoiceDetails";
            this.uMnuSalesInvoiceDetails.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesInvoiceDetails.Text = "Sales Invoice Details";
            // 
            // uMnuSalesInvoiceMasterDetails
            // 
            this.uMnuSalesInvoiceMasterDetails.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMasterDetails.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesInvoiceMasterDetails.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMasterDetails.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesInvoiceMasterDetails.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMasterDetails.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesInvoiceMasterDetails.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMasterDetails.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesInvoiceMasterDetails.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceMasterDetails.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesInvoiceMasterDetails.Name = "uMnuSalesInvoiceMasterDetails";
            this.uMnuSalesInvoiceMasterDetails.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesInvoiceMasterDetails.Text = "Sales Invoice Master Details";
            // 
            // UcMnuSalesInvoiceLedgerWise
            // 
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Default.Options.UseFont = true;
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Disabled.Options.UseFont = true;
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Hovered.Options.UseFont = true;
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Normal.Options.UseFont = true;
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.UcMnuSalesInvoiceLedgerWise.Appearance.Pressed.Options.UseFont = true;
            this.UcMnuSalesInvoiceLedgerWise.Name = "UcMnuSalesInvoiceLedgerWise";
            this.UcMnuSalesInvoiceLedgerWise.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.UcMnuSalesInvoiceLedgerWise.Text = "Sales Invoice Ledger Wise";
            // 
            // uMnuSalesInvoiceTableWise
            // 
            this.uMnuSalesInvoiceTableWise.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceTableWise.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesInvoiceTableWise.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceTableWise.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesInvoiceTableWise.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceTableWise.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesInvoiceTableWise.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceTableWise.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesInvoiceTableWise.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoiceTableWise.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesInvoiceTableWise.Name = "uMnuSalesInvoiceTableWise";
            this.uMnuSalesInvoiceTableWise.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesInvoiceTableWise.Text = "Sales Register Table Wise";
            // 
            // uMnuSalesInvoicePartialMaster
            // 
            this.uMnuSalesInvoicePartialMaster.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoicePartialMaster.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesInvoicePartialMaster.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoicePartialMaster.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesInvoicePartialMaster.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoicePartialMaster.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesInvoicePartialMaster.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoicePartialMaster.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesInvoicePartialMaster.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesInvoicePartialMaster.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesInvoicePartialMaster.Name = "uMnuSalesInvoicePartialMaster";
            this.uMnuSalesInvoicePartialMaster.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesInvoicePartialMaster.Text = "Sales Invoice Partial Payment";
            // 
            // SalesSeprate
            // 
            this.SalesSeprate.Name = "SalesSeprate";
            // 
            // uMnuSalesVatRegister
            // 
            this.uMnuSalesVatRegister.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegister.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesVatRegister.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegister.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesVatRegister.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegister.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesVatRegister.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegister.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesVatRegister.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegister.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesVatRegister.Name = "uMnuSalesVatRegister";
            this.uMnuSalesVatRegister.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesVatRegister.Text = "Sales Vat Register";
            // 
            // uMnuSalesVatRegisterIncludeReturn
            // 
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesVatRegisterIncludeReturn.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesVatRegisterIncludeReturn.Name = "uMnuSalesVatRegisterIncludeReturn";
            this.uMnuSalesVatRegisterIncludeReturn.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesVatRegisterIncludeReturn.Text = "Sales Vat Register Include Return";
            // 
            // uMnuSalesReturnVatRegister
            // 
            this.uMnuSalesReturnVatRegister.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.uMnuSalesReturnVatRegister.Appearance.Default.Options.UseFont = true;
            this.uMnuSalesReturnVatRegister.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesReturnVatRegister.Appearance.Disabled.Options.UseFont = true;
            this.uMnuSalesReturnVatRegister.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.uMnuSalesReturnVatRegister.Appearance.Hovered.Options.UseFont = true;
            this.uMnuSalesReturnVatRegister.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesReturnVatRegister.Appearance.Normal.Options.UseFont = true;
            this.uMnuSalesReturnVatRegister.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuSalesReturnVatRegister.Appearance.Pressed.Options.UseFont = true;
            this.uMnuSalesReturnVatRegister.Name = "uMnuSalesReturnVatRegister";
            this.uMnuSalesReturnVatRegister.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesReturnVatRegister.Text = "Sales Return Vat Register";
            // 
            // MnuSalesVatRegisterCustomerWise
            // 
            this.MnuSalesVatRegisterCustomerWise.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuSalesVatRegisterCustomerWise.Appearance.Default.Options.UseFont = true;
            this.MnuSalesVatRegisterCustomerWise.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuSalesVatRegisterCustomerWise.Appearance.Disabled.Options.UseFont = true;
            this.MnuSalesVatRegisterCustomerWise.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuSalesVatRegisterCustomerWise.Appearance.Hovered.Options.UseFont = true;
            this.MnuSalesVatRegisterCustomerWise.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuSalesVatRegisterCustomerWise.Appearance.Normal.Options.UseFont = true;
            this.MnuSalesVatRegisterCustomerWise.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuSalesVatRegisterCustomerWise.Appearance.Pressed.Options.UseFont = true;
            this.MnuSalesVatRegisterCustomerWise.Name = "MnuSalesVatRegisterCustomerWise";
            this.MnuSalesVatRegisterCustomerWise.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.MnuSalesVatRegisterCustomerWise.Text = "Sales Vat Register Customer Wise";
            // 
            // uMnuPurchaseMaster
            // 
            this.uMnuPurchaseMaster.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseMaster.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseMaster.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseMaster.Appearance.Normal.Options.UseFont = true;
            this.uMnuPurchaseMaster.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.uMnuPurchaseInvoiceSummery,
            this.uMnuPurchaseInvoiceDetails,
            this.uMnuPurchaseInvoiceMasterDetails,
            this.accordionControlSeparator2,
            this.uMnuPurchaseVatRegister,
            this.uMnuPurchaseVatRegisterIncludeReturn,
            this.uMnuPurchaseReturnVatRegister});
            this.uMnuPurchaseMaster.Expanded = true;
            this.uMnuPurchaseMaster.Name = "uMnuPurchaseMaster";
            this.uMnuPurchaseMaster.Text = "Purchase Master";
            // 
            // uMnuPurchaseInvoiceSummery
            // 
            this.uMnuPurchaseInvoiceSummery.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseInvoiceSummery.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseInvoiceSummery.Name = "uMnuPurchaseInvoiceSummery";
            this.uMnuPurchaseInvoiceSummery.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuPurchaseInvoiceSummery.Text = "Purchase Invoice Summary";
            // 
            // uMnuPurchaseInvoiceDetails
            // 
            this.uMnuPurchaseInvoiceDetails.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseInvoiceDetails.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseInvoiceDetails.Name = "uMnuPurchaseInvoiceDetails";
            this.uMnuPurchaseInvoiceDetails.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuPurchaseInvoiceDetails.Text = "Purchase Invoice Details";
            // 
            // uMnuPurchaseInvoiceMasterDetails
            // 
            this.uMnuPurchaseInvoiceMasterDetails.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseInvoiceMasterDetails.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseInvoiceMasterDetails.Name = "uMnuPurchaseInvoiceMasterDetails";
            this.uMnuPurchaseInvoiceMasterDetails.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuPurchaseInvoiceMasterDetails.Text = "Purchase Invoice Master Details";
            // 
            // accordionControlSeparator2
            // 
            this.accordionControlSeparator2.Name = "accordionControlSeparator2";
            // 
            // uMnuPurchaseVatRegister
            // 
            this.uMnuPurchaseVatRegister.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseVatRegister.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseVatRegister.Name = "uMnuPurchaseVatRegister";
            this.uMnuPurchaseVatRegister.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuPurchaseVatRegister.Text = "Purchase Vat Register";
            // 
            // uMnuPurchaseVatRegisterIncludeReturn
            // 
            this.uMnuPurchaseVatRegisterIncludeReturn.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseVatRegisterIncludeReturn.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseVatRegisterIncludeReturn.Name = "uMnuPurchaseVatRegisterIncludeReturn";
            this.uMnuPurchaseVatRegisterIncludeReturn.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuPurchaseVatRegisterIncludeReturn.Text = "Purchase Vat Register Include Return";
            // 
            // uMnuPurchaseReturnVatRegister
            // 
            this.uMnuPurchaseReturnVatRegister.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuPurchaseReturnVatRegister.Appearance.Default.Options.UseFont = true;
            this.uMnuPurchaseReturnVatRegister.Name = "uMnuPurchaseReturnVatRegister";
            this.uMnuPurchaseReturnVatRegister.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuPurchaseReturnVatRegister.Text = "Purchase Return Vat Register";
            // 
            // PartyLedger
            // 
            this.PartyLedger.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.AceCustomerLedger,
            this.AceVendorLedger});
            this.PartyLedger.Expanded = true;
            this.PartyLedger.Name = "PartyLedger";
            this.PartyLedger.Text = "Party Ledger";
            // 
            // AceCustomerLedger
            // 
            this.AceCustomerLedger.Name = "AceCustomerLedger";
            this.AceCustomerLedger.Text = "Customer Ledger";
            this.AceCustomerLedger.Click += new System.EventHandler(this.AceCustomerLedger_Click);
            // 
            // AceVendorLedger
            // 
            this.AceVendorLedger.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.AceVendorCurrencyLedger});
            this.AceVendorLedger.Expanded = true;
            this.AceVendorLedger.Name = "AceVendorLedger";
            this.AceVendorLedger.Text = "Vender Ledger";
            // 
            // AceVendorCurrencyLedger
            // 
            this.AceVendorCurrencyLedger.Name = "AceVendorCurrencyLedger";
            this.AceVendorCurrencyLedger.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.AceVendorCurrencyLedger.Text = "Vendor Currency Ledger";
            // 
            // tabbedView
            // 
            this.tabbedView.DocumentClosed += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(this.TabbedView_DocumentClosed);
            // 
            // documentManager
            // 
            this.documentManager.ContainerControl = this;
            this.documentManager.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
            this.documentManager.View = this.tabbedView;
            this.documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView});
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Id = 1;
            this.btnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.btnSave.ImageOptions.LargeImage = global::MrBLL.Properties.Resources.Save;
            this.btnSave.Name = "btnSave";
            this.btnSave.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Save";
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.barButtonItem1.ImageOptions.LargeImage = global::MrBLL.Properties.Resources.Save;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // uMnuSalesInvoiceYearMaster
            // 
            this.uMnuSalesInvoiceYearMaster.Name = "uMnuSalesInvoiceYearMaster";
            this.uMnuSalesInvoiceYearMaster.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.uMnuSalesInvoiceYearMaster.Text = "Sales Invoice Year Wise";
            // 
            // FrmDynamicRegisterReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1267, 742);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "FrmDynamicRegisterReports";
            this.Ribbon = this.ribbonControl;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanel.ResumeLayout(false);
            this.dockPanel_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ElementsControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Docking.DockManager dockManager;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel_Container;
        private DevExpress.XtraBars.Navigation.AccordionControl ElementsControl;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupNavigation;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesMaster;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseMaster;
        private DevExpress.XtraBars.Navigation.AccordionControlElement RegisterReport;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesInvoiceMaster;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesInvoiceDetails;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesInvoiceMasterDetails;
        private DevExpress.XtraBars.Navigation.AccordionControlSeparator SalesSeprate;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseInvoiceSummery;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseInvoiceDetails;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseInvoiceMasterDetails;
        private DevExpress.XtraBars.BarButtonItem BtnSaveTemplate;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem BtnSaveAsTemplate;
        private DevExpress.XtraBars.BarButtonItem BtnRemoveTemplate;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement UcMnuSalesInvoiceLedgerWise;
        private DevExpress.XtraBars.BarButtonItem BtnPrintPreview;
        private DevExpress.XtraBars.BarButtonItem BtnPrintReport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem MnuExportToExcel;
        private DevExpress.XtraBars.BarCheckItem MnuCheckFooter;
        private DevExpress.XtraBars.BarButtonItem BtnFilterReports;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesInvoiceTableWise;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesVatRegister;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesReturnVatRegister;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesVatRegisterIncludeReturn;
        private DevExpress.XtraBars.Navigation.AccordionControlSeparator accordionControlSeparator2;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseVatRegister;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseReturnVatRegister;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuPurchaseVatRegisterIncludeReturn;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesInvoicePartialMaster;
        private DevExpress.XtraBars.Navigation.AccordionControlElement MnuSalesVatRegisterCustomerWise;
        private DevExpress.XtraBars.Navigation.AccordionControlElement PartyLedger;
        private DevExpress.XtraBars.Navigation.AccordionControlElement AceCustomerLedger;
        private DevExpress.XtraBars.Navigation.AccordionControlElement AceVendorLedger;
        private DevExpress.XtraBars.Navigation.AccordionControlElement AceVendorCurrencyLedger;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuSalesInvoiceYearMaster;
    }
}