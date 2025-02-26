namespace MrBLL.DataEntry.StockMaster
{
    partial class FrmStockManagement
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
            this.components = new System.ComponentModel.Container();
            this.UserControlContainer = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.MenuItemControl = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.StockAdjustmentMenu = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.PhysicalStockMenu = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.GodownTransferMenu = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // UserControlContainer
            // 
            this.UserControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserControlContainer.Location = new System.Drawing.Point(254, 31);
            this.UserControlContainer.Name = "UserControlContainer";
            this.UserControlContainer.Size = new System.Drawing.Size(896, 684);
            this.UserControlContainer.TabIndex = 0;
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.MenuItemControl});
            this.accordionControl1.Location = new System.Drawing.Point(0, 31);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(254, 684);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.False;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // MenuItemControl
            // 
            this.MenuItemControl.ControlFooterAlignment = DevExpress.XtraBars.Navigation.AccordionItemFooterAlignment.Far;
            this.MenuItemControl.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.StockAdjustmentMenu,
            this.PhysicalStockMenu,
            this.GodownTransferMenu});
            this.MenuItemControl.Expanded = true;
            this.MenuItemControl.Name = "MenuItemControl";
            this.MenuItemControl.Text = "Stock Management";
            this.MenuItemControl.Click += new System.EventHandler(this.MenuItemControl_Click);
            // 
            // StockAdjustmentMenu
            // 
            this.StockAdjustmentMenu.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StockAdjustmentMenu.Appearance.Default.Options.UseFont = true;
            this.StockAdjustmentMenu.Name = "StockAdjustmentMenu";
            this.StockAdjustmentMenu.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.StockAdjustmentMenu.Text = "Stock Adjustment";
            this.StockAdjustmentMenu.Click += new System.EventHandler(this.StockAdjustmentMenu_Click);
            // 
            // PhysicalStockMenu
            // 
            this.PhysicalStockMenu.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.PhysicalStockMenu.Appearance.Default.Options.UseFont = true;
            this.PhysicalStockMenu.Name = "PhysicalStockMenu";
            this.PhysicalStockMenu.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.PhysicalStockMenu.Text = "Physical Stock";
            this.PhysicalStockMenu.Click += new System.EventHandler(this.PhysicalStockMenu_Click);
            // 
            // GodownTransferMenu
            // 
            this.GodownTransferMenu.Appearance.Default.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.GodownTransferMenu.Appearance.Default.Options.UseFont = true;
            this.GodownTransferMenu.Name = "GodownTransferMenu";
            this.GodownTransferMenu.Style = DevExpress.XtraBars.Navigation.ElementStyle.Item;
            this.GodownTransferMenu.Text = "Godown Transfer";
            this.GodownTransferMenu.Click += new System.EventHandler(this.GodownTransferMenu_Click);
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(1150, 31);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.Form = this;
            // 
            // FrmStockManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 715);
            this.ControlContainer = this.UserControlContainer;
            this.Controls.Add(this.UserControlContainer);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.IconOptions.ShowIcon = false;
            this.KeyPreview = true;
            this.Name = "FrmStockManagement";
            this.NavigationControl = this.accordionControl1;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Stock Management Dash Board";
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer UserControlContainer;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement MenuItemControl;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement StockAdjustmentMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement PhysicalStockMenu;
        private DevExpress.XtraBars.Navigation.AccordionControlElement GodownTransferMenu;
    }
}