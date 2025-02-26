
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Entry
{
    partial class FrmTempInvoices
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.bsInvoices = new System.Windows.Forms.BindingSource(this.components);
            this.pnlBottom = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.lblInvoiceNo = new System.Windows.Forms.Label();
            this.lblInvoiceEnteredBy = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInvoiceDateTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bsInvoiceItems = new System.Windows.Forms.BindingSource(this.components);
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.DGridMaster = new MrDAL.Control.ControlsEx.Control.DataGridViewEx();
            this.DGridDetails = new MrDAL.Control.ControlsEx.Control.DataGridViewEx();
            this.GVoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVoucherMiti = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GEnterUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoices)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGridMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGridDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.pnlBottom.Controls.Add(this.DGridDetails);
            this.pnlBottom.Controls.Add(this.DGridMaster);
            this.pnlBottom.Controls.Add(this.panel1);
            this.pnlBottom.Controls.Add(this.BtnCancel);
            this.pnlBottom.Controls.Add(this.BtnAccept);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBottom.Location = new System.Drawing.Point(0, 0);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(977, 566);
            this.pnlBottom.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.lblInvoiceNo);
            this.panel1.Controls.Add(this.lblInvoiceEnteredBy);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblInvoiceDateTime);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 29);
            this.panel1.TabIndex = 2;
            // 
            // lblInvoiceNo
            // 
            this.lblInvoiceNo.BackColor = System.Drawing.Color.White;
            this.lblInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvoiceNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInvoiceNo.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblInvoiceNo.Location = new System.Drawing.Point(88, 4);
            this.lblInvoiceNo.Name = "lblInvoiceNo";
            this.lblInvoiceNo.Size = new System.Drawing.Size(202, 22);
            this.lblInvoiceNo.TabIndex = 0;
            // 
            // lblInvoiceEnteredBy
            // 
            this.lblInvoiceEnteredBy.BackColor = System.Drawing.Color.White;
            this.lblInvoiceEnteredBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvoiceEnteredBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInvoiceEnteredBy.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblInvoiceEnteredBy.Location = new System.Drawing.Point(606, 4);
            this.lblInvoiceEnteredBy.Name = "lblInvoiceEnteredBy";
            this.lblInvoiceEnteredBy.Size = new System.Drawing.Size(131, 22);
            this.lblInvoiceEnteredBy.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label2.Location = new System.Drawing.Point(292, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "DATETIME";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label3.Location = new System.Drawing.Point(507, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "ENTERED BY";
            // 
            // lblInvoiceDateTime
            // 
            this.lblInvoiceDateTime.BackColor = System.Drawing.Color.White;
            this.lblInvoiceDateTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInvoiceDateTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInvoiceDateTime.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.lblInvoiceDateTime.Location = new System.Drawing.Point(376, 4);
            this.lblInvoiceDateTime.Name = "lblInvoiceDateTime";
            this.lblInvoiceDateTime.Size = new System.Drawing.Size(131, 22);
            this.lblInvoiceDateTime.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 10F);
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "INVOICE #";
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCancel.Location = new System.Drawing.Point(415, 527);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(131, 34);
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
            this.BtnAccept.Location = new System.Drawing.Point(273, 527);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(136, 34);
            this.BtnAccept.TabIndex = 11;
            this.BtnAccept.Text = "&ACCEPT";
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // DGridMaster
            // 
            this.DGridMaster.AllowUserToAddRows = false;
            this.DGridMaster.AllowUserToDeleteRows = false;
            this.DGridMaster.AllowUserToResizeRows = false;
            this.DGridMaster.AutoGenerateColumns = false;
            this.DGridMaster.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGridMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGridMaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GVoucherNo,
            this.GVoucherMiti,
            this.GEnterUser});
            this.DGridMaster.DataSource = this.bsInvoices;
            this.DGridMaster.DoubleBufferEnabled = true;
            this.DGridMaster.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGridMaster.Location = new System.Drawing.Point(5, 38);
            this.DGridMaster.MultiSelect = false;
            this.DGridMaster.Name = "DGridMaster";
            this.DGridMaster.ReadOnly = true;
            this.DGridMaster.Size = new System.Drawing.Size(967, 226);
            this.DGridMaster.TabIndex = 13;
            this.DGridMaster.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGridMaster_CellContentClick);
            // 
            // DGridDetails
            // 
            this.DGridDetails.AllowUserToAddRows = false;
            this.DGridDetails.AllowUserToDeleteRows = false;
            this.DGridDetails.AllowUserToResizeRows = false;
            this.DGridDetails.AutoGenerateColumns = false;
            this.DGridDetails.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGridDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGridDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GBarcode,
            this.GProduct,
            this.GQty,
            this.GRate});
            this.DGridDetails.DataSource = this.bsInvoiceItems;
            this.DGridDetails.DoubleBufferEnabled = true;
            this.DGridDetails.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGridDetails.Location = new System.Drawing.Point(5, 270);
            this.DGridDetails.MultiSelect = false;
            this.DGridDetails.Name = "DGridDetails";
            this.DGridDetails.ReadOnly = true;
            this.DGridDetails.Size = new System.Drawing.Size(967, 251);
            this.DGridDetails.TabIndex = 14;
            // 
            // GVoucherNo
            // 
            this.GVoucherNo.DataPropertyName = "SB_Invoice";
            this.GVoucherNo.HeaderText = "VOUCHER NO";
            this.GVoucherNo.Name = "GVoucherNo";
            this.GVoucherNo.ReadOnly = true;
            this.GVoucherNo.Width = 150;
            // 
            // GVoucherMiti
            // 
            this.GVoucherMiti.DataPropertyName = "Invoice_Miti";
            this.GVoucherMiti.HeaderText = "VOUCHER MITI";
            this.GVoucherMiti.Name = "GVoucherMiti";
            this.GVoucherMiti.ReadOnly = true;
            this.GVoucherMiti.Width = 150;
            // 
            // GEnterUser
            // 
            this.GEnterUser.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GEnterUser.DataPropertyName = "Enter_By";
            this.GEnterUser.HeaderText = "USER";
            this.GEnterUser.Name = "GEnterUser";
            this.GEnterUser.ReadOnly = true;
            // 
            // GBarcode
            // 
            this.GBarcode.DataPropertyName = "Barcode";
            this.GBarcode.HeaderText = "BAR CODE";
            this.GBarcode.Name = "GBarcode";
            this.GBarcode.ReadOnly = true;
            this.GBarcode.Width = 150;
            // 
            // GProduct
            // 
            this.GProduct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GProduct.DataPropertyName = "ProductName";
            this.GProduct.HeaderText = "PRODUCT";
            this.GProduct.Name = "GProduct";
            this.GProduct.ReadOnly = true;
            // 
            // GQty
            // 
            this.GQty.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.GQty.HeaderText = "QTY";
            this.GQty.Name = "GQty";
            this.GQty.ReadOnly = true;
            this.GQty.Width = 150;
            // 
            // GRate
            // 
            this.GRate.DataPropertyName = "RATE";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GRate.DefaultCellStyle = dataGridViewCellStyle2;
            this.GRate.HeaderText = "RATE";
            this.GRate.Name = "GRate";
            this.GRate.ReadOnly = true;
            this.GRate.Width = 150;
            // 
            // FrmTempInvoices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(977, 566);
            this.Controls.Add(this.pnlBottom);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTempInvoices";
            this.Text = "HOLD INVOICE LIST";
            this.Load += new System.EventHandler(this.FrmTempInvoices_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoices)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGridMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGridDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton BtnCancel;
        public DevExpress.XtraEditors.SimpleButton BtnAccept;
        private System.Windows.Forms.BindingSource bsInvoices;
        private System.Windows.Forms.BindingSource bsInvoiceItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInvoiceEnteredBy;
        private System.Windows.Forms.Label lblInvoiceDateTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInvoiceNo;
        private System.Windows.Forms.Label label2;
        private DevExpress.Utils.Behaviors.BehaviorManager behaviorManager1;
        private MrPanel pnlBottom;
        private MrPanel panel1;
        private DataGridViewEx DGridMaster;
        private DataGridViewEx DGridDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GVoucherMiti;
        private System.Windows.Forms.DataGridViewTextBoxColumn GEnterUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn GBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn GQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRate;
    }
}