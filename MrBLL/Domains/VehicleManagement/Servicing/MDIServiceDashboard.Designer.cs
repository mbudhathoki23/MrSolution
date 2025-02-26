namespace MrBLL.Domains.VehicleManagement.Servicing
{
    partial class MDIServiceDashboard
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
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.dockPanel = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.accordionControl = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.mainAccordionGroup = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tbSalesInvoice1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tbSalesInvoice2 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tbSalesInvoice3 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tbSalesInvoice4 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.GrpAdditional = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.tbPurchaseChallan = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.dockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.tabbedView = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.documentManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.dockPanel.SuspendLayout();
            this.dockPanel_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 579);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Size = new System.Drawing.Size(1142, 20);
            // 
            // dockPanel
            // 
            this.dockPanel.Controls.Add(this.dockPanel_Container);
            this.dockPanel.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel.ID = new System.Guid("ca040f51-b307-46d3-8920-16938ed883d1");
            this.dockPanel.Location = new System.Drawing.Point(0, 0);
            this.dockPanel.Name = "dockPanel";
            this.dockPanel.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel.Size = new System.Drawing.Size(200, 579);
            this.dockPanel.Text = "Navigation";
            // 
            // dockPanel_Container
            // 
            this.dockPanel_Container.Controls.Add(this.accordionControl);
            this.dockPanel_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel_Container.Name = "dockPanel_Container";
            this.dockPanel_Container.Size = new System.Drawing.Size(193, 550);
            this.dockPanel_Container.TabIndex = 0;
            // 
            // accordionControl
            // 
            this.accordionControl.AllowItemSelection = true;
            this.accordionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accordionControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.mainAccordionGroup,
            this.GrpAdditional});
            this.accordionControl.Location = new System.Drawing.Point(0, 0);
            this.accordionControl.Name = "accordionControl";
            this.accordionControl.Size = new System.Drawing.Size(193, 550);
            this.accordionControl.TabIndex = 0;
            this.accordionControl.SelectedElementChanged += new DevExpress.XtraBars.Navigation.SelectedElementChangedEventHandler(this.accordionControl_SelectedElementChanged);
            // 
            // mainAccordionGroup
            // 
            this.mainAccordionGroup.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.tbSalesInvoice1,
            this.tbSalesInvoice2,
            this.tbSalesInvoice3,
            this.tbSalesInvoice4});
            this.mainAccordionGroup.Expanded = true;
            this.mainAccordionGroup.HeaderVisible = false;
            this.mainAccordionGroup.Name = "mainAccordionGroup";
            this.mainAccordionGroup.Text = "mainGroup";
            // 
            // tbSalesInvoice1
            // 
            this.tbSalesInvoice1.Name = "tbSalesInvoice1";
            this.tbSalesInvoice1.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.tbSalesInvoice1.Text = "SALES INVOICE";
            // 
            // tbSalesInvoice2
            // 
            this.tbSalesInvoice2.Name = "tbSalesInvoice2";
            this.tbSalesInvoice2.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.tbSalesInvoice2.Text = "INVOICE NEXT";
            // 
            // tbSalesInvoice3
            // 
            this.tbSalesInvoice3.Name = "tbSalesInvoice3";
            this.tbSalesInvoice3.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.tbSalesInvoice3.Text = "INVOICE THIRD";
            // 
            // tbSalesInvoice4
            // 
            this.tbSalesInvoice4.Name = "tbSalesInvoice4";
            this.tbSalesInvoice4.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.tbSalesInvoice4.Text = "INVOICE FORTH";
            // 
            // GrpAdditional
            // 
            this.GrpAdditional.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.tbPurchaseChallan});
            this.GrpAdditional.Expanded = true;
            this.GrpAdditional.Name = "GrpAdditional";
            this.GrpAdditional.Text = "Additional";
            // 
            // tbPurchaseChallan
            // 
            this.tbPurchaseChallan.Name = "tbPurchaseChallan";
            this.tbPurchaseChallan.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.tbPurchaseChallan.Text = "PURCHASE CHALLAN";
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
            // tabbedView
            // 
            this.tabbedView.DocumentClosed += new DevExpress.XtraBars.Docking2010.Views.DocumentEventHandler(this.tabbedView_DocumentClosed);
            // 
            // documentManager
            // 
            this.documentManager.ContainerControl = this;
            this.documentManager.RibbonAndBarsMergeStyle = DevExpress.XtraBars.Docking2010.Views.RibbonAndBarsMergeStyle.Always;
            this.documentManager.View = this.tabbedView;
            this.documentManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tabbedView});
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.accordionControlElement1.Text = "INVOICE NEXT";
            // 
            // MDIServiceDashboard
            // 
            this.Appearance.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 599);
            this.Controls.Add(this.dockPanel);
            this.Controls.Add(this.ribbonStatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "MDIServiceDashboard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.dockPanel.ResumeLayout(false);
            this.dockPanel_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabbedView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.documentManager)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
		private DevExpress.XtraBars.Docking.DockManager dockManager;
		private DevExpress.XtraBars.Docking.DockPanel dockPanel;
		private DevExpress.XtraBars.Docking.ControlContainer dockPanel_Container;
		private DevExpress.XtraBars.Navigation.AccordionControl accordionControl;
		private DevExpress.XtraBars.Navigation.AccordionControlElement tbSalesInvoice1;
		private DevExpress.XtraBars.Navigation.AccordionControlElement tbSalesInvoice2;
		private DevExpress.XtraBars.Navigation.AccordionControlElement mainAccordionGroup;
		private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tabbedView;
		private DevExpress.XtraBars.Docking2010.DocumentManager documentManager;
		private DevExpress.XtraBars.Navigation.AccordionControlElement tbSalesInvoice3;
		private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
		private DevExpress.XtraBars.Navigation.AccordionControlElement tbSalesInvoice4;
		private DevExpress.XtraBars.Navigation.AccordionControlElement GrpAdditional;
		private DevExpress.XtraBars.Navigation.AccordionControlElement tbPurchaseChallan;
	}
}