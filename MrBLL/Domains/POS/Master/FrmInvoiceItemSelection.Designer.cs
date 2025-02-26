
using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmInvoiceItemSelection
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlBottom = new MrPanel();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnAccept = new DevExpress.XtraEditors.SimpleButton();
            this.rdoAppend = new System.Windows.Forms.RadioButton();
            this.rdoModifyExisting = new System.Windows.Forms.RadioButton();
            this.bsInvoiceItems = new System.Windows.Forms.BindingSource(this.components);
            this.DGrid = new EntryGridViewEx();
            this.colSn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAltQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAltUom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiscount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNetAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new MrPanel();
            this.lblProductInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.BtnCancel);
            this.pnlBottom.Controls.Add(this.BtnAccept);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 491);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1021, 58);
            this.pnlBottom.TabIndex = 2;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.BtnCancel.Location = new System.Drawing.Point(883, 10);
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
            this.BtnAccept.Location = new System.Drawing.Point(770, 10);
            this.BtnAccept.Name = "BtnAccept";
            this.BtnAccept.Size = new System.Drawing.Size(107, 39);
            this.BtnAccept.TabIndex = 11;
            this.BtnAccept.Text = "&ACCEPT";
            this.BtnAccept.Click += new System.EventHandler(this.BtnAccept_Click);
            // 
            // rdoAppend
            // 
            this.rdoAppend.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoAppend.Checked = true;
            this.rdoAppend.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.rdoAppend.Location = new System.Drawing.Point(227, 3);
            this.rdoAppend.Name = "rdoAppend";
            this.rdoAppend.Size = new System.Drawing.Size(255, 46);
            this.rdoAppend.TabIndex = 4;
            this.rdoAppend.TabStop = true;
            this.rdoAppend.Text = "APPEND";
            this.rdoAppend.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoAppend.UseVisualStyleBackColor = true;
            this.rdoAppend.CheckedChanged += new System.EventHandler(this.OnRadioButton_CheckedChanged);
            this.rdoAppend.Paint += new System.Windows.Forms.PaintEventHandler(this.rdoModifyExisting_Paint);
            // 
            // rdoModifyExisting
            // 
            this.rdoModifyExisting.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.rdoModifyExisting.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.rdoModifyExisting.Location = new System.Drawing.Point(523, 3);
            this.rdoModifyExisting.Name = "rdoModifyExisting";
            this.rdoModifyExisting.Size = new System.Drawing.Size(255, 46);
            this.rdoModifyExisting.TabIndex = 4;
            this.rdoModifyExisting.Text = "MODIFY EXISTING";
            this.rdoModifyExisting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoModifyExisting.UseVisualStyleBackColor = true;
            this.rdoModifyExisting.CheckedChanged += new System.EventHandler(this.OnRadioButton_CheckedChanged);
            this.rdoModifyExisting.Paint += new System.Windows.Forms.PaintEventHandler(this.rdoModifyExisting_Paint);
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.AutoGenerateColumns = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.DGrid.ColumnHeadersHeight = 27;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSn,
            this.colShortName,
            this.colProductName,
            this.colAltQty,
            this.colAltUom,
            this.colQty,
            this.colUOM,
            this.colRate,
            this.colAmount,
            this.colDiscount,
            this.colNetAmount});
            this.DGrid.DataSource = this.bsInvoiceItems;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(0, 132);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersVisible = false;
            this.DGrid.RowHeadersWidth = 20;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.DGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(1021, 359);
            this.DGrid.TabIndex = 5;
            this.DGrid.Visible = false;
            this.DGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellDoubleClick);
            // 
            // colSn
            // 
            this.colSn.DataPropertyName = "SNo";
            this.colSn.HeaderText = "#";
            this.colSn.Name = "colSn";
            this.colSn.ReadOnly = true;
            this.colSn.Width = 40;
            // 
            // colShortName
            // 
            this.colShortName.DataPropertyName = "ProductShortName";
            this.colShortName.HeaderText = "SHORT NAME";
            this.colShortName.MinimumWidth = 120;
            this.colShortName.Name = "colShortName";
            this.colShortName.ReadOnly = true;
            this.colShortName.Width = 120;
            // 
            // colProductName
            // 
            this.colProductName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colProductName.DataPropertyName = "ProductName";
            this.colProductName.HeaderText = "PRODUCT";
            this.colProductName.MinimumWidth = 150;
            this.colProductName.Name = "colProductName";
            this.colProductName.ReadOnly = true;
            // 
            // colAltQty
            // 
            this.colAltQty.DataPropertyName = "AltQty";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.colAltQty.DefaultCellStyle = dataGridViewCellStyle10;
            this.colAltQty.HeaderText = "ALT QTY";
            this.colAltQty.Name = "colAltQty";
            this.colAltQty.ReadOnly = true;
            this.colAltQty.Width = 70;
            // 
            // colAltUom
            // 
            this.colAltUom.DataPropertyName = "AltUnitName";
            this.colAltUom.HeaderText = "ALT UOM";
            this.colAltUom.Name = "colAltUom";
            this.colAltUom.ReadOnly = true;
            this.colAltUom.Width = 70;
            // 
            // colQty
            // 
            this.colQty.DataPropertyName = "Qty";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            this.colQty.DefaultCellStyle = dataGridViewCellStyle11;
            this.colQty.HeaderText = "QTY";
            this.colQty.Name = "colQty";
            this.colQty.ReadOnly = true;
            this.colQty.Width = 70;
            // 
            // colUOM
            // 
            this.colUOM.DataPropertyName = "UnitName";
            this.colUOM.HeaderText = "UOM";
            this.colUOM.Name = "colUOM";
            this.colUOM.ReadOnly = true;
            this.colUOM.Width = 70;
            // 
            // colRate
            // 
            this.colRate.DataPropertyName = "Rate";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "0.00";
            this.colRate.DefaultCellStyle = dataGridViewCellStyle12;
            this.colRate.HeaderText = "RATE";
            this.colRate.Name = "colRate";
            this.colRate.ReadOnly = true;
            this.colRate.Width = 80;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "NAmount";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "0.00";
            this.colAmount.DefaultCellStyle = dataGridViewCellStyle13;
            this.colAmount.HeaderText = "TOTAL";
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            this.colAmount.Width = 80;
            // 
            // colDiscount
            // 
            this.colDiscount.DataPropertyName = "ItemDis";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "0.00";
            this.colDiscount.DefaultCellStyle = dataGridViewCellStyle14;
            this.colDiscount.HeaderText = "DISCOUNT";
            this.colDiscount.Name = "colDiscount";
            this.colDiscount.ReadOnly = true;
            this.colDiscount.Width = 80;
            // 
            // colNetAmount
            // 
            this.colNetAmount.DataPropertyName = "ActualAmountAfterDiscount";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle15.Format = "0.00";
            this.colNetAmount.DefaultCellStyle = dataGridViewCellStyle15;
            this.colNetAmount.HeaderText = "NET";
            this.colNetAmount.Name = "colNetAmount";
            this.colNetAmount.ReadOnly = true;
            this.colNetAmount.Width = 80;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblProductInfo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.rdoAppend);
            this.panel1.Controls.Add(this.rdoModifyExisting);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 132);
            this.panel1.TabIndex = 6;
            // 
            // lblProductInfo
            // 
            this.lblProductInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProductInfo.Font = new System.Drawing.Font("Bookman Old Style", 14F);
            this.lblProductInfo.Location = new System.Drawing.Point(12, 80);
            this.lblProductInfo.Name = "lblProductInfo";
            this.lblProductInfo.Size = new System.Drawing.Size(1002, 44);
            this.lblProductInfo.TabIndex = 6;
            this.lblProductInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(520, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Modifies the selected invoice item with given parameters";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(224, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Appends the new record to existing invoice items list";
            // 
            // FrmInvoiceItemSelection
            // 
            this.AcceptButton = this.BtnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(1021, 549);
            this.Controls.Add(this.DGrid);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlBottom);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmInvoiceItemSelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Item Selection";
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsInvoiceItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBottom;
        public DevExpress.XtraEditors.SimpleButton BtnCancel;
        public DevExpress.XtraEditors.SimpleButton BtnAccept;
        private System.Windows.Forms.RadioButton rdoAppend;
        private System.Windows.Forms.RadioButton rdoModifyExisting;
        private System.Windows.Forms.BindingSource bsInvoiceItems;
        private DataGridView DGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAltQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAltUom;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiscount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNetAmount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblProductInfo;
    }
}