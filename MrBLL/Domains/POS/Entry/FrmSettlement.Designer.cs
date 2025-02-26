
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Entry
{
    partial class FrmSettlement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSettlement));
            this.mrPanel1 = new MrPanel();
            this.LblBasicAmount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.LblTotalAmount = new System.Windows.Forms.Label();
            this.clsSeparator1 = new ClsSeparator();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.DGrid = new MrDataGridView();
            this.GTxtPaymentMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedgerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtLedger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtGiftVoucher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtGiftVoucherAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.LblBasicAmount);
            this.mrPanel1.Controls.Add(this.labelControl1);
            this.mrPanel1.Controls.Add(this.label2);
            this.mrPanel1.Controls.Add(this.LblTotalAmount);
            this.mrPanel1.Controls.Add(this.clsSeparator1);
            this.mrPanel1.Controls.Add(this.BtnCancel);
            this.mrPanel1.Controls.Add(this.BtnSave);
            this.mrPanel1.Controls.Add(this.DGrid);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(696, 301);
            this.mrPanel1.TabIndex = 0;
            // 
            // LblBasicAmount
            // 
            this.LblBasicAmount.Appearance.BackColor = System.Drawing.Color.White;
            this.LblBasicAmount.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.LblBasicAmount.Appearance.Options.UseBackColor = true;
            this.LblBasicAmount.Appearance.Options.UseFont = true;
            this.LblBasicAmount.Appearance.Options.UseTextOptions = true;
            this.LblBasicAmount.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LblBasicAmount.AppearanceDisabled.Options.UseTextOptions = true;
            this.LblBasicAmount.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.LblBasicAmount.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.LblBasicAmount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.LblBasicAmount.Location = new System.Drawing.Point(521, 3);
            this.LblBasicAmount.Name = "LblBasicAmount";
            this.LblBasicAmount.Size = new System.Drawing.Size(172, 29);
            this.LblBasicAmount.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(402, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(115, 19);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Invoice Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(397, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Total Amount :";
            // 
            // LblTotalAmount
            // 
            this.LblTotalAmount.BackColor = System.Drawing.Color.White;
            this.LblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTotalAmount.Location = new System.Drawing.Point(521, 224);
            this.LblTotalAmount.Name = "LblTotalAmount";
            this.LblTotalAmount.Size = new System.Drawing.Size(172, 29);
            this.LblTotalAmount.TabIndex = 3;
            this.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(13, 256);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(680, 2);
            this.clsSeparator1.TabIndex = 2;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(403, 259);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(99, 38);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(303, 259);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(98, 38);
            this.BtnSave.TabIndex = 1;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtPaymentMode,
            this.GTxtLedgerId,
            this.GTxtLedger,
            this.GTxtAmount,
            this.GTxtGiftVoucher,
            this.GTxtGiftVoucherAmount});
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 35);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(696, 186);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            // 
            // GTxtPaymentMode
            // 
            this.GTxtPaymentMode.HeaderText = "PAYMENT MODE";
            this.GTxtPaymentMode.Name = "GTxtPaymentMode";
            this.GTxtPaymentMode.ReadOnly = true;
            this.GTxtPaymentMode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GTxtPaymentMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GTxtPaymentMode.Width = 150;
            // 
            // GTxtLedgerId
            // 
            this.GTxtLedgerId.HeaderText = "LEDGERID";
            this.GTxtLedgerId.Name = "GTxtLedgerId";
            this.GTxtLedgerId.ReadOnly = true;
            this.GTxtLedgerId.Visible = false;
            // 
            // GTxtLedger
            // 
            this.GTxtLedger.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtLedger.HeaderText = "LEDGER";
            this.GTxtLedger.Name = "GTxtLedger";
            this.GTxtLedger.ReadOnly = true;
            // 
            // GTxtAmount
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtAmount.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxtAmount.HeaderText = "AMOUNT";
            this.GTxtAmount.Name = "GTxtAmount";
            this.GTxtAmount.ReadOnly = true;
            this.GTxtAmount.Width = 120;
            // 
            // GTxtGiftVoucher
            // 
            this.GTxtGiftVoucher.HeaderText = "GiftVoucher";
            this.GTxtGiftVoucher.Name = "GTxtGiftVoucher";
            this.GTxtGiftVoucher.ReadOnly = true;
            this.GTxtGiftVoucher.Visible = false;
            // 
            // GTxtGiftVoucherAmount
            // 
            this.GTxtGiftVoucherAmount.HeaderText = "GiftVoucherAmount";
            this.GTxtGiftVoucherAmount.Name = "GTxtGiftVoucherAmount";
            this.GTxtGiftVoucherAmount.ReadOnly = true;
            this.GTxtGiftVoucherAmount.Visible = false;
            // 
            // FrmSettlement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 301);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmSettlement";
            this.ShowIcon = false;
            this.Text = "Sales Settlement";
            this.Load += new System.EventHandler(this.FrmSettlement_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmSettlement_KeyPress);
            this.mrPanel1.ResumeLayout(false);
            this.mrPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private MrDataGridView DGrid;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblTotalAmount;
        private DevExpress.XtraEditors.LabelControl LblBasicAmount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtPaymentMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedgerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtLedger;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtGiftVoucher;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtGiftVoucherAmount;
    }
}