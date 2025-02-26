
namespace MrBLL.Domains.Hospital
{
    partial class MdiHospital
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.MnuDoctorType = new DevExpress.XtraBars.BarButtonItem();
            this.MnuDoctorSetup = new DevExpress.XtraBars.BarButtonItem();
            this.MnuDepartment = new DevExpress.XtraBars.BarButtonItem();
            this.MnuWardMaster = new DevExpress.XtraBars.BarButtonItem();
            this.MnuBedType = new DevExpress.XtraBars.BarButtonItem();
            this.MnuBedNumber = new DevExpress.XtraBars.BarButtonItem();
            this.MnuBedMaster = new DevExpress.XtraBars.BarButtonItem();
            this.MnuCategory = new DevExpress.XtraBars.BarButtonItem();
            this.MnuSubCategory = new DevExpress.XtraBars.BarButtonItem();
            this.MnuItemUnit = new DevExpress.XtraBars.BarButtonItem();
            this.MnuItemMaster = new DevExpress.XtraBars.BarButtonItem();
            this.MnuPatientRegistration = new DevExpress.XtraBars.BarButtonItem();
            this.MnuOPDBilling = new DevExpress.XtraBars.BarButtonItem();
            this.MnuIPDBilling = new DevExpress.XtraBars.BarButtonItem();
            this.MnuCashReceipt = new DevExpress.XtraBars.BarButtonItem();
            this.MnuCashRefund = new DevExpress.XtraBars.BarButtonItem();
            this.MasterPage = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.MnuMasterList = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup5 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup7 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup6 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.DGridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.RGrid = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.MnuSalesRegister = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.DarkBlue;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.ribbon.SearchEditItem,
            this.MnuDoctorType,
            this.MnuDoctorSetup,
            this.MnuDepartment,
            this.MnuWardMaster,
            this.MnuBedType,
            this.MnuBedNumber,
            this.MnuBedMaster,
            this.MnuCategory,
            this.MnuSubCategory,
            this.MnuItemUnit,
            this.MnuItemMaster,
            this.MnuPatientRegistration,
            this.MnuOPDBilling,
            this.MnuIPDBilling,
            this.MnuCashReceipt,
            this.MnuCashRefund,
            this.MnuSalesRegister});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 18;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.MasterPage,
            this.ribbonPage1,
            this.ribbonPage2});
            this.ribbon.Size = new System.Drawing.Size(1123, 158);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // MnuDoctorType
            // 
            this.MnuDoctorType.Caption = "Doctor Type";
            this.MnuDoctorType.Id = 1;
            this.MnuDoctorType.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuDoctorType.Name = "MnuDoctorType";
            this.MnuDoctorType.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuDoctorType_ItemClick);
            // 
            // MnuDoctorSetup
            // 
            this.MnuDoctorSetup.Caption = "Doctor Setup";
            this.MnuDoctorSetup.Id = 2;
            this.MnuDoctorSetup.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuDoctorSetup.Name = "MnuDoctorSetup";
            this.MnuDoctorSetup.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuDoctorSetup_ItemClick);
            // 
            // MnuDepartment
            // 
            this.MnuDepartment.Caption = "Department";
            this.MnuDepartment.Id = 3;
            this.MnuDepartment.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuDepartment.Name = "MnuDepartment";
            this.MnuDepartment.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.MnuDepartment.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuDepartment_ItemClick);
            // 
            // MnuWardMaster
            // 
            this.MnuWardMaster.Caption = "Ward Master";
            this.MnuWardMaster.Id = 4;
            this.MnuWardMaster.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuWardMaster.Name = "MnuWardMaster";
            this.MnuWardMaster.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuWardMaster_ItemClick);
            // 
            // MnuBedType
            // 
            this.MnuBedType.Caption = "Bed Type";
            this.MnuBedType.Id = 5;
            this.MnuBedType.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuBedType.Name = "MnuBedType";
            this.MnuBedType.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuBedType_ItemClick);
            // 
            // MnuBedNumber
            // 
            this.MnuBedNumber.Caption = "Bed Number";
            this.MnuBedNumber.Id = 6;
            this.MnuBedNumber.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuBedNumber.Name = "MnuBedNumber";
            this.MnuBedNumber.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuBedNumber_ItemClick);
            // 
            // MnuBedMaster
            // 
            this.MnuBedMaster.Caption = "Bed Master";
            this.MnuBedMaster.Id = 7;
            this.MnuBedMaster.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuBedMaster.Name = "MnuBedMaster";
            this.MnuBedMaster.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuBedMaster_ItemClick);
            // 
            // MnuCategory
            // 
            this.MnuCategory.Caption = "Item Category";
            this.MnuCategory.Id = 8;
            this.MnuCategory.Name = "MnuCategory";
            this.MnuCategory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuCategory_ItemClick);
            // 
            // MnuSubCategory
            // 
            this.MnuSubCategory.Caption = "Item Sub Category";
            this.MnuSubCategory.Id = 9;
            this.MnuSubCategory.Name = "MnuSubCategory";
            this.MnuSubCategory.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuSubCategory_ItemClick);
            // 
            // MnuItemUnit
            // 
            this.MnuItemUnit.Caption = "Item Unit";
            this.MnuItemUnit.Id = 10;
            this.MnuItemUnit.Name = "MnuItemUnit";
            // 
            // MnuItemMaster
            // 
            this.MnuItemMaster.Caption = "Item Master";
            this.MnuItemMaster.Id = 11;
            this.MnuItemMaster.Name = "MnuItemMaster";
            this.MnuItemMaster.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuItemMaster_ItemClick);
            // 
            // MnuPatientRegistration
            // 
            this.MnuPatientRegistration.Caption = "Patient Registration";
            this.MnuPatientRegistration.Id = 12;
            this.MnuPatientRegistration.Name = "MnuPatientRegistration";
            this.MnuPatientRegistration.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuPatientRegistration_ItemClick);
            // 
            // MnuOPDBilling
            // 
            this.MnuOPDBilling.Caption = "OPD Billing";
            this.MnuOPDBilling.Id = 13;
            this.MnuOPDBilling.ImageOptions.Image = global::MrBLL.Properties.Resources.addparagraphtotableofcontents_32x32;
            this.MnuOPDBilling.Name = "MnuOPDBilling";
            this.MnuOPDBilling.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuOPDBilling_ItemClick);
            // 
            // MnuIPDBilling
            // 
            this.MnuIPDBilling.Caption = "IPD Billing";
            this.MnuIPDBilling.Id = 14;
            this.MnuIPDBilling.ImageOptions.Image = global::MrBLL.Properties.Resources.addparagraphtotableofcontents_32x32;
            this.MnuIPDBilling.Name = "MnuIPDBilling";
            this.MnuIPDBilling.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuIPDBilling_ItemClick);
            // 
            // MnuCashReceipt
            // 
            this.MnuCashReceipt.Caption = "Cash Receipt";
            this.MnuCashReceipt.Id = 15;
            this.MnuCashReceipt.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuCashReceipt.Name = "MnuCashReceipt";
            this.MnuCashReceipt.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuCashReceipt_ItemClick);
            // 
            // MnuCashRefund
            // 
            this.MnuCashRefund.Caption = "Refund Amount";
            this.MnuCashRefund.Id = 16;
            this.MnuCashRefund.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.MnuCashRefund.Name = "MnuCashRefund";
            this.MnuCashRefund.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuCashRefund_ItemClick);
            // 
            // MasterPage
            // 
            this.MasterPage.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.MnuMasterList,
            this.ribbonPageGroup1,
            this.ribbonPageGroup2,
            this.ribbonPageGroup3,
            this.ribbonPageGroup4});
            this.MasterPage.Name = "MasterPage";
            this.MasterPage.Text = "Master Page";
            // 
            // MnuMasterList
            // 
            this.MnuMasterList.ItemLinks.Add(this.MnuDoctorType);
            this.MnuMasterList.ItemLinks.Add(this.MnuDoctorSetup);
            this.MnuMasterList.Name = "MnuMasterList";
            this.MnuMasterList.Text = "Master Create";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.MnuDepartment);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Hospital Department";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.MnuWardMaster);
            this.ribbonPageGroup2.ItemLinks.Add(this.MnuBedType);
            this.ribbonPageGroup2.ItemLinks.Add(this.MnuBedNumber);
            this.ribbonPageGroup2.ItemLinks.Add(this.MnuBedMaster);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "Ward & Bed";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.MnuCategory);
            this.ribbonPageGroup3.ItemLinks.Add(this.MnuSubCategory);
            this.ribbonPageGroup3.ItemLinks.Add(this.MnuItemUnit);
            this.ribbonPageGroup3.ItemLinks.Add(this.MnuItemMaster);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Service Item";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.MnuPatientRegistration);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Patient Registration";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup5,
            this.ribbonPageGroup7});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Billing Page";
            // 
            // ribbonPageGroup5
            // 
            this.ribbonPageGroup5.ItemLinks.Add(this.MnuOPDBilling);
            this.ribbonPageGroup5.ItemLinks.Add(this.MnuIPDBilling);
            this.ribbonPageGroup5.Name = "ribbonPageGroup5";
            this.ribbonPageGroup5.Text = "OPD && IPD Billing";
            // 
            // ribbonPageGroup7
            // 
            this.ribbonPageGroup7.ItemLinks.Add(this.MnuCashReceipt);
            this.ribbonPageGroup7.ItemLinks.Add(this.MnuCashRefund);
            this.ribbonPageGroup7.Name = "ribbonPageGroup7";
            this.ribbonPageGroup7.Text = "Cash ClientInfo";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup6});
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "Reporting Page";
            // 
            // ribbonPageGroup6
            // 
            this.ribbonPageGroup6.ItemLinks.Add(this.MnuSalesRegister);
            this.ribbonPageGroup6.Name = "ribbonPageGroup6";
            this.ribbonPageGroup6.Text = "ribbonPageGroup6";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 557);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1123, 24);
            // 
            // DGridControl
            // 
            this.DGridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.RelationName = "Level1";
            this.DGridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.DGridControl.Location = new System.Drawing.Point(0, 158);
            this.DGridControl.MainView = this.gridView1;
            this.DGridControl.MenuManager = this.ribbon;
            this.DGridControl.Name = "DGridControl";
            this.DGridControl.Size = new System.Drawing.Size(1123, 399);
            this.DGridControl.TabIndex = 5;
            this.DGridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1,
            this.RGrid});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.DGridControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsCustomization.AllowRowSizing = true;
            this.gridView1.OptionsView.AllowCellMerge = true;
            this.gridView1.OptionsView.RowAutoHeight = true;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowChildrenInGroupPanel = true;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // RGrid
            // 
            this.RGrid.GridControl = this.DGridControl;
            this.RGrid.Name = "RGrid";
            // 
            // MnuSalesRegister
            // 
            this.MnuSalesRegister.Caption = "Sales Register";
            this.MnuSalesRegister.Id = 17;
            this.MnuSalesRegister.Name = "MnuSalesRegister";
            this.MnuSalesRegister.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.MnuSalesRegister_ItemClick);
            // 
            // MDIHospital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 581);
            this.Controls.Add(this.DGridControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "MdiHospital";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Hospital Management System";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage MasterPage;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup MnuMasterList;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.BarButtonItem MnuDoctorType;
        private DevExpress.XtraGrid.GridControl DGridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView RGrid;
        private DevExpress.XtraBars.BarButtonItem MnuDoctorSetup;
        private DevExpress.XtraBars.BarButtonItem MnuDepartment;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem MnuWardMaster;
        private DevExpress.XtraBars.BarButtonItem MnuBedType;
        private DevExpress.XtraBars.BarButtonItem MnuBedNumber;
        private DevExpress.XtraBars.BarButtonItem MnuBedMaster;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem MnuCategory;
        private DevExpress.XtraBars.BarButtonItem MnuSubCategory;
        private DevExpress.XtraBars.BarButtonItem MnuItemUnit;
        private DevExpress.XtraBars.BarButtonItem MnuItemMaster;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.BarButtonItem MnuPatientRegistration;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup5;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup6;
        private DevExpress.XtraBars.BarButtonItem MnuOPDBilling;
        private DevExpress.XtraBars.BarButtonItem MnuIPDBilling;
        private DevExpress.XtraBars.BarButtonItem MnuCashReceipt;
        private DevExpress.XtraBars.BarButtonItem MnuCashRefund;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup7;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarButtonItem MnuSalesRegister;
    }
}