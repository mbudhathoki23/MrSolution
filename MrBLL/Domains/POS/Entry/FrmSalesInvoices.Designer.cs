
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Entry
{
    partial class FrmSalesInvoices
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
            this.pnlBottom = new MrPanel();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gcInvoices = new DevExpress.XtraGrid.GridControl();
            this.bsInvoices = new System.Windows.Forms.BindingSource(this.components);
            this.gvInvoices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInvoiceItems = new DevExpress.XtraGrid.GridControl();
            this.bsInvoiceItems = new System.Windows.Forms.BindingSource(this.components);
            this.gvInvoiceItems = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn13 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn14 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new MrPanel();
            this.lblInvoiceEnteredBy = new System.Windows.Forms.Label();
            this.lblInvoiceDateTime = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvoiceItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvoiceItems)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.BtnCancel);
            this.pnlBottom.Controls.Add(this.BtnAccept);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 526);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1140, 58);
            this.pnlBottom.TabIndex = 1;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCancel.Location = new System.Drawing.Point(1002, 10);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(131, 39);
            this.BtnCancel.TabIndex = 12;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnAccept
            // 
            this.BtnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAccept.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.BtnAccept.Appearance.Options.UseFont = true;
            this.BtnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtnAccept.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnAccept.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter;
            this.BtnAccept.Location = new System.Drawing.Point(889, 10);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(107, 39);
            this.BtnAccept.TabIndex = 11;
            this.BtnAccept.Text = "&ACCEPT";
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gcInvoices);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gcInvoiceItems);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1140, 526);
            this.splitContainer1.SplitterDistance = 696;
            this.splitContainer1.TabIndex = 4;
            // 
            // gcInvoices
            // 
            this.gcInvoices.DataSource = this.bsInvoices;
            this.gcInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInvoices.Location = new System.Drawing.Point(0, 0);
            this.gcInvoices.MainView = this.gvInvoices;
            this.gcInvoices.Name = "gcInvoices";
            this.gcInvoices.Size = new System.Drawing.Size(696, 526);
            this.gcInvoices.TabIndex = 2;
            this.gcInvoices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInvoices});
            this.gcInvoices.DoubleClick += new System.EventHandler(this.gcInvoices_DoubleClick);
            this.gcInvoices.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gcInvoices_KeyPress);
            // 
            // gvInvoices
            // 
            this.gvInvoices.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8});
            this.gvInvoices.GridControl = this.gcInvoices;
            this.gvInvoices.Name = "gvInvoices";
            this.gvInvoices.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvInvoices_FocusedRowChanged);
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "DATETIME";
            this.gridColumn1.FieldName = "Invoice_Time";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.FixedWidth = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 87;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "INVOICE NO.";
            this.gridColumn2.FieldName = "SB_Invoice";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.FixedWidth = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 96;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "MITI";
            this.gridColumn3.FieldName = "Invoice_Miti";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.FixedWidth = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 76;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "PARTY NAME";
            this.gridColumn4.FieldName = "Party_Name";
            this.gridColumn4.MinWidth = 150;
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 233;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "ADDRESS";
            this.gridColumn5.FieldName = "Address";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.FixedWidth = true;
            this.gridColumn5.Width = 129;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "PAN NO.";
            this.gridColumn6.FieldName = "Vat_No";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.OptionsColumn.FixedWidth = true;
            this.gridColumn6.Width = 83;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "ENTERED BY";
            this.gridColumn7.FieldName = "Enter_By";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.OptionsColumn.FixedWidth = true;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 5;
            this.gridColumn7.Width = 100;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "NET AMOUNT";
            this.gridColumn8.DisplayFormat.FormatString = "N2";
            this.gridColumn8.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn8.FieldName = "N_Amount";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.OptionsColumn.FixedWidth = true;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 4;
            this.gridColumn8.Width = 84;
            // 
            // gcInvoiceItems
            // 
            this.gcInvoiceItems.DataSource = this.bsInvoiceItems;
            this.gcInvoiceItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcInvoiceItems.Location = new System.Drawing.Point(0, 64);
            this.gcInvoiceItems.MainView = this.gvInvoiceItems;
            this.gcInvoiceItems.Name = "gcInvoiceItems";
            this.gcInvoiceItems.Size = new System.Drawing.Size(440, 462);
            this.gcInvoiceItems.TabIndex = 2;
            this.gcInvoiceItems.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInvoiceItems});
            // 
            // gvInvoiceItems
            // 
            this.gvInvoiceItems.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12,
            this.gridColumn13,
            this.gridColumn14});
            this.gvInvoiceItems.GridControl = this.gcInvoiceItems;
            this.gvInvoiceItems.Name = "gvInvoiceItems";
            this.gvInvoiceItems.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "PRODUCT";
            this.gridColumn9.FieldName = "ProductName";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 0;
            this.gridColumn9.Width = 160;
            // 
            // gridColumn10
            // 
            this.gridColumn10.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn10.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn10.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn10.Caption = "QTY";
            this.gridColumn10.DisplayFormat.FormatString = "N2";
            this.gridColumn10.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn10.FieldName = "Qty";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 1;
            this.gridColumn10.Width = 48;
            // 
            // gridColumn11
            // 
            this.gridColumn11.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn11.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn11.Caption = "UNIT";
            this.gridColumn11.FieldName = "UnitCode";
            this.gridColumn11.Name = "gridColumn11";
            this.gridColumn11.Visible = true;
            this.gridColumn11.VisibleIndex = 2;
            this.gridColumn11.Width = 42;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn12.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn12.Caption = "RATE";
            this.gridColumn12.DisplayFormat.FormatString = "N2";
            this.gridColumn12.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn12.FieldName = "Rate";
            this.gridColumn12.Name = "gridColumn12";
            this.gridColumn12.Visible = true;
            this.gridColumn12.VisibleIndex = 3;
            this.gridColumn12.Width = 56;
            // 
            // gridColumn13
            // 
            this.gridColumn13.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn13.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn13.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn13.Caption = "NET TOTAL";
            this.gridColumn13.DisplayFormat.FormatString = "N2";
            this.gridColumn13.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn13.FieldName = "N_Amount";
            this.gridColumn13.Name = "gridColumn13";
            this.gridColumn13.Visible = true;
            this.gridColumn13.VisibleIndex = 4;
            this.gridColumn13.Width = 65;
            // 
            // gridColumn14
            // 
            this.gridColumn14.AppearanceCell.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn14.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn14.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.gridColumn14.Caption = "BASIC TOTAL";
            this.gridColumn14.DisplayFormat.FormatString = "N2";
            this.gridColumn14.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn14.FieldName = "B_Amount";
            this.gridColumn14.Name = "gridColumn14";
            this.gridColumn14.Visible = true;
            this.gridColumn14.VisibleIndex = 5;
            this.gridColumn14.Width = 65;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblInvoiceEnteredBy);
            this.panel1.Controls.Add(this.lblInvoiceDateTime);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblInvoiceNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(440, 64);
            this.panel1.TabIndex = 1;
            // 
            // lblInvoiceEnteredBy
            // 
            this.lblInvoiceEnteredBy.AutoSize = true;
            this.lblInvoiceEnteredBy.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblInvoiceEnteredBy.Location = new System.Drawing.Point(108, 42);
            this.lblInvoiceEnteredBy.Name = "lblInvoiceEnteredBy";
            this.lblInvoiceEnteredBy.Size = new System.Drawing.Size(52, 19);
            this.lblInvoiceEnteredBy.TabIndex = 0;
            this.lblInvoiceEnteredBy.Text = "label1";
            // 
            // lblInvoiceDateTime
            // 
            this.lblInvoiceDateTime.AutoSize = true;
            this.lblInvoiceDateTime.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblInvoiceDateTime.Location = new System.Drawing.Point(108, 23);
            this.lblInvoiceDateTime.Name = "lblInvoiceDateTime";
            this.lblInvoiceDateTime.Size = new System.Drawing.Size(52, 19);
            this.lblInvoiceDateTime.TabIndex = 0;
            this.lblInvoiceDateTime.Text = "label1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label3.Location = new System.Drawing.Point(3, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "ENTERED BY";
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.AutoSize = true;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblInvoiceNo.Location = new System.Drawing.Point(108, 4);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(52, 19);
            this.lblInvoiceNo.TabIndex = 0;
            this.lblInvoiceNo.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "DATETIME";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "INVOICE #";
            // 
            // FrmSalesInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(1140, 584);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSalesInvoices";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoices";
            this.Load += new System.EventHandler(this.FrmSalesInvoices_Load);
            this.pnlBottom.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInvoiceItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvoiceItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        public DevExpress.XtraEditors.SimpleButton BtnCancel;
        public DevExpress.XtraEditors.SimpleButton BtnAccept;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblInvoiceEnteredBy;
        private System.Windows.Forms.Label lblInvoiceDateTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource bsInvoices;
        private System.Windows.Forms.BindingSource bsInvoiceItems;
        private DevExpress.XtraGrid.GridControl gcInvoices;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInvoices;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.GridControl gcInvoiceItems;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInvoiceItems;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn13;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn14;
    }
}