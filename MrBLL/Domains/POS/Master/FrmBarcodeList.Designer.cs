using System.Windows.Forms;
using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Domains.POS.Master
{
    partial class FrmBarcodeList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBarcodeList));
            this.DGrid = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtUnitId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtMRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtTrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtWholesales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtRetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtDealer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtReseller = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSalesRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAltUnit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.BlockNavigationOnNextRowOnEnter = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.ColumnHeadersHeight = 25;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtSNo,
            this.GTxtBarcode,
            this.GTxtUnitId,
            this.GTxtUnit,
            this.GTxtMRP,
            this.GTxtTrade,
            this.GTxtWholesales,
            this.GTxtRetails,
            this.GTxtDealer,
            this.GTxtReseller,
            this.GTxtSalesRate,
            this.GTxtAltUnit});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.DoubleBufferEnabled = true;
            this.DGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.DGrid.Location = new System.Drawing.Point(3, 16);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.DGrid.RowHeadersWidth = 30;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(978, 200);
            this.DGrid.StandardTab = true;
            this.DGrid.TabIndex = 0;
            this.DGrid.EnterKeyPressed += new System.EventHandler<System.EventArgs>(this.DGrid_EnterKeyPressed);
            this.DGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellContentClick);
            this.DGrid.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellEnter);
            this.DGrid.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_RowEnter);
            this.DGrid.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.DGrid_UserDeletedRow);
            this.DGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGrid_KeyDown);
            // 
            // GTxtSNo
            // 
            this.GTxtSNo.HeaderText = "SNo";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 50;
            // 
            // GTxtBarcode
            // 
            this.GTxtBarcode.DataPropertyName = "GTxtBarcode";
            this.GTxtBarcode.HeaderText = "BARCODE";
            this.GTxtBarcode.Name = "GTxtBarcode";
            this.GTxtBarcode.ReadOnly = true;
            this.GTxtBarcode.Width = 120;
            // 
            // GTxtUnitId
            // 
            this.GTxtUnitId.HeaderText = "UOMID";
            this.GTxtUnitId.Name = "GTxtUnitId";
            this.GTxtUnitId.ReadOnly = true;
            this.GTxtUnitId.Visible = false;
            // 
            // GTxtUnit
            // 
            this.GTxtUnit.HeaderText = "UOM";
            this.GTxtUnit.Name = "GTxtUnit";
            this.GTxtUnit.ReadOnly = true;
            this.GTxtUnit.Width = 70;
            // 
            // GTxtMRP
            // 
            this.GTxtMRP.DataPropertyName = "GTxtMRP";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtMRP.DefaultCellStyle = dataGridViewCellStyle2;
            this.GTxtMRP.HeaderText = "MRP";
            this.GTxtMRP.Name = "GTxtMRP";
            this.GTxtMRP.ReadOnly = true;
            this.GTxtMRP.Width = 80;
            // 
            // GTxtTrade
            // 
            this.GTxtTrade.DataPropertyName = "GTxtTrade";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtTrade.DefaultCellStyle = dataGridViewCellStyle3;
            this.GTxtTrade.HeaderText = "TRADE PRICE";
            this.GTxtTrade.Name = "GTxtTrade";
            this.GTxtTrade.ReadOnly = true;
            this.GTxtTrade.Width = 110;
            // 
            // GTxtWholesales
            // 
            this.GTxtWholesales.DataPropertyName = "GTxtWholesales";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtWholesales.DefaultCellStyle = dataGridViewCellStyle4;
            this.GTxtWholesales.HeaderText = "WHOLESALE";
            this.GTxtWholesales.Name = "GTxtWholesales";
            this.GTxtWholesales.ReadOnly = true;
            this.GTxtWholesales.Width = 110;
            // 
            // GTxtRetails
            // 
            this.GTxtRetails.DataPropertyName = "GTxtRetails";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtRetails.DefaultCellStyle = dataGridViewCellStyle5;
            this.GTxtRetails.HeaderText = "RETAILS";
            this.GTxtRetails.Name = "GTxtRetails";
            this.GTxtRetails.ReadOnly = true;
            this.GTxtRetails.Width = 90;
            // 
            // GTxtDealer
            // 
            this.GTxtDealer.DataPropertyName = "GTxtDealer";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtDealer.DefaultCellStyle = dataGridViewCellStyle6;
            this.GTxtDealer.HeaderText = "DEALER";
            this.GTxtDealer.Name = "GTxtDealer";
            this.GTxtDealer.ReadOnly = true;
            this.GTxtDealer.Width = 90;
            // 
            // GTxtReseller
            // 
            this.GTxtReseller.DataPropertyName = "GTxtReseller";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtReseller.DefaultCellStyle = dataGridViewCellStyle7;
            this.GTxtReseller.HeaderText = "RE_SELLER";
            this.GTxtReseller.Name = "GTxtReseller";
            this.GTxtReseller.ReadOnly = true;
            // 
            // GTxtSalesRate
            // 
            this.GTxtSalesRate.DataPropertyName = "GTxtSalesRate";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtSalesRate.DefaultCellStyle = dataGridViewCellStyle8;
            this.GTxtSalesRate.HeaderText = "SALES RATE";
            this.GTxtSalesRate.Name = "GTxtSalesRate";
            this.GTxtSalesRate.ReadOnly = true;
            this.GTxtSalesRate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GTxtSalesRate.Width = 110;
            // 
            // GTxtAltUnit
            // 
            this.GTxtAltUnit.HeaderText = "IsAlltUnit";
            this.GTxtAltUnit.Name = "GTxtAltUnit";
            this.GTxtAltUnit.ReadOnly = true;
            this.GTxtAltUnit.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 219);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(984, 48);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnCancel.Location = new System.Drawing.Point(854, 11);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 34);
            this.BtnCancel.TabIndex = 3;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(749, 11);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(103, 34);
            this.BtnSave.TabIndex = 2;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.DGrid);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(984, 219);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // FrmBarcodeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 267);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBarcodeList";
            this.ShowIcon = false;
            this.Text = "BARCODE LIST";
            this.Load += new System.EventHandler(this.FrmBarcodeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private DevExpress.XtraEditors.SimpleButton BtnCancel;
		private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtUnitId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtMRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtTrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtWholesales;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtRetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDealer;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtReseller;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSalesRate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GTxtAltUnit;
        public EntryGridViewEx DGrid;
    }
}