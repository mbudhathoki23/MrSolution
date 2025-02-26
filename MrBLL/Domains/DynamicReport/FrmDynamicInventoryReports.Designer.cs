namespace MrBLL.Domains.DynamicReport
{
    partial class FrmDynamicInventoryReports
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
        [System.Obsolete]
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
            this.accordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.mainAccordionGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.uMnuStockLedger = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.MnuStockLedger = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.MnuStockLedgerWithValue = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.SalesSeprate = new DevExpress.XtraBars.Navigation.AccordionControlSeparator();
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.accordionControlElement2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            this.dockPanel.SuspendLayout();
            this.dockPanel_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).BeginInit();
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
            this.ribbonControl.Size = new System.Drawing.Size(1331, 158);
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
            this.ribbonStatusBar.Size = new System.Drawing.Size(1331, 24);
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
            this.dockPanel.OriginalSize = new System.Drawing.Size(371, 200);
            this.dockPanel.Size = new System.Drawing.Size(371, 560);
            this.dockPanel.Text = "Navigation";
            // 
            // dockPanel_Container
            // 
            this.dockPanel_Container.Controls.Add(this.accordionControl);
            this.dockPanel_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel_Container.Name = "dockPanel_Container";
            this.dockPanel_Container.Size = new System.Drawing.Size(364, 531);
            this.dockPanel_Container.TabIndex = 0;
            // 
            // accordionControl
            // 
            this.accordionControl.AllowItemSelection = true;
            this.accordionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.mainAccordionGroup});
            this.accordionControl.Location = new System.Drawing.Point(0, 0);
            this.accordionControl.Name = "accordionControl";
            this.accordionControl.Size = new System.Drawing.Size(364, 531);
            this.accordionControl.TabIndex = 0;
            this.accordionControl.ElementClick += new DevExpress.XtraBars.Navigation.ElementClickEventHandler(this.AccordionControl_ElementClick);
            // 
            // mainAccordionGroup
            // 
            this.mainAccordionGroup.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.uMnuStockLedger});
            this.mainAccordionGroup.Expanded = true;
            this.mainAccordionGroup.HeaderVisible = false;
            this.mainAccordionGroup.Name = "mainAccordionGroup";
            this.mainAccordionGroup.Text = "mainGroup";
            // 
            // uMnuStockLedger
            // 
            this.uMnuStockLedger.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuStockLedger.Appearance.Default.Options.UseFont = true;
            this.uMnuStockLedger.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuStockLedger.Appearance.Disabled.Options.UseFont = true;
            this.uMnuStockLedger.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuStockLedger.Appearance.Hovered.Options.UseFont = true;
            this.uMnuStockLedger.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuStockLedger.Appearance.Normal.Options.UseFont = true;
            this.uMnuStockLedger.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.uMnuStockLedger.Appearance.Pressed.Options.UseFont = true;
            this.uMnuStockLedger.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.MnuStockLedger,
            this.MnuStockLedgerWithValue,
            this.SalesSeprate});
            this.uMnuStockLedger.Expanded = true;
            this.uMnuStockLedger.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons)});
            this.uMnuStockLedger.Name = "uMnuStockLedger";
            this.uMnuStockLedger.Text = "Stock Ledger Reports";
            // 
            // MnuStockLedger
            // 
            this.MnuStockLedger.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedger.Appearance.Default.Options.UseFont = true;
            this.MnuStockLedger.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedger.Appearance.Disabled.Options.UseFont = true;
            this.MnuStockLedger.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedger.Appearance.Hovered.Options.UseFont = true;
            this.MnuStockLedger.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedger.Appearance.Normal.Options.UseFont = true;
            this.MnuStockLedger.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedger.Appearance.Pressed.Options.UseFont = true;
            this.MnuStockLedger.ControlFooterAlignment = DevExpress.XtraBars.Navigation.AccordionItemFooterAlignment.Far;
            this.MnuStockLedger.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl)});
            this.MnuStockLedger.Name = "MnuStockLedger";
            this.MnuStockLedger.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.MnuStockLedger.Text = "Stock Ledger";
            // 
            // MnuStockLedgerWithValue
            // 
            this.MnuStockLedgerWithValue.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedgerWithValue.Appearance.Default.Options.UseFont = true;
            this.MnuStockLedgerWithValue.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedgerWithValue.Appearance.Disabled.Options.UseFont = true;
            this.MnuStockLedgerWithValue.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedgerWithValue.Appearance.Hovered.Options.UseFont = true;
            this.MnuStockLedgerWithValue.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedgerWithValue.Appearance.Normal.Options.UseFont = true;
            this.MnuStockLedgerWithValue.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.MnuStockLedgerWithValue.Appearance.Pressed.Options.UseFont = true;
            this.MnuStockLedgerWithValue.Name = "MnuStockLedgerWithValue";
            this.MnuStockLedgerWithValue.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.MnuStockLedgerWithValue.Text = "Stock Ledger With Value";
            // 
            // SalesSeprate
            // 
            this.SalesSeprate.Name = "SalesSeprate";
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
            // accordionControlElement2
            // 
            this.accordionControlElement2.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.accordionControlElement2.Appearance.Default.Options.UseFont = true;
            this.accordionControlElement2.Appearance.Disabled.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.accordionControlElement2.Appearance.Disabled.Options.UseFont = true;
            this.accordionControlElement2.Appearance.Hovered.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.accordionControlElement2.Appearance.Hovered.Options.UseFont = true;
            this.accordionControlElement2.Appearance.Normal.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.accordionControlElement2.Appearance.Normal.Options.UseFont = true;
            this.accordionControlElement2.Appearance.Pressed.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.accordionControlElement2.Appearance.Pressed.Options.UseFont = true;
            this.accordionControlElement2.ControlFooterAlignment = DevExpress.XtraBars.Navigation.AccordionItemFooterAlignment.Far;
            this.accordionControlElement2.HeaderTemplate.AddRange(new DevExpress.XtraBars.Navigation.HeaderElementInfo[] {
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Image),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.Text),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.ContextButtons),
            new DevExpress.XtraBars.Navigation.HeaderElementInfo(DevExpress.XtraBars.Navigation.HeaderElementType.HeaderControl)});
            this.accordionControlElement2.Name = "accordionControlElement2";
            this.accordionControlElement2.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement2.Text = "Post Date Cheque";
            // 
            // FrmDynamicInventoryReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 742);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbonControl);
            this.Name = "FrmDynamicInventoryReports";
            this.Ribbon = this.ribbonControl;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            this.dockPanel.ResumeLayout(false);
            this.dockPanel_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).EndInit();
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
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupNavigation;
        private DevExpress.XtraBars.Navigation.AccordionControlElement uMnuStockLedger;
        private DevExpress.XtraBars.Navigation.AccordionControlElement mainAccordionGroup;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;
        private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
        private DevExpress.XtraBars.Navigation.AccordionControlElement MnuStockLedger;
        private DevExpress.XtraBars.Navigation.AccordionControlSeparator SalesSeprate;
        private DevExpress.XtraBars.BarButtonItem BtnSaveTemplate;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem BtnSaveAsTemplate;
        private DevExpress.XtraBars.BarButtonItem BtnRemoveTemplate;
        private DevExpress.XtraBars.SkinRibbonGalleryBarItem skinRibbonGalleryBarItem1;
        private DevExpress.XtraBars.BarButtonItem BtnPrintPreview;
        private DevExpress.XtraBars.BarButtonItem BtnPrintReport;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem MnuExportToExcel;
        private DevExpress.XtraBars.BarCheckItem MnuCheckFooter;
        private DevExpress.XtraBars.BarButtonItem BtnFilterReports;
        private DevExpress.XtraBars.Navigation.AccordionControlElement MnuStockLedgerWithValue;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement2;
    }
}