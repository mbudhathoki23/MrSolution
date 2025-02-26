using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.ProductionMaster
{
    partial class FrmAutoConsumption
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel2 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.GTxtCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VoucherNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FinishedGoods = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel2.Controls.Add(this.BtnRefresh);
            this.panel2.Controls.Add(this.ChkSelectAll);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.BtnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 480);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1234, 38);
            this.panel2.TabIndex = 1;
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnRefresh.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRefresh.Appearance.Options.UseFont = true;
            this.BtnRefresh.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnRefresh.Location = new System.Drawing.Point(890, 3);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(129, 31);
            this.BtnRefresh.TabIndex = 216;
            this.BtnRefresh.Text = "&REFRESH";
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.AutoSize = true;
            this.ChkSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ChkSelectAll.Location = new System.Drawing.Point(20, 13);
            this.ChkSelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.Size = new System.Drawing.Size(100, 23);
            this.ChkSelectAll.TabIndex = 215;
            this.ChkSelectAll.Text = "Select All";
            this.ChkSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            this.ChkSelectAll.CheckedChanged += new System.EventHandler(this.CkbSelectAll_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1122, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 31);
            this.btnCancel.TabIndex = 214;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(1021, 3);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(98, 31);
            this.BtnSave.TabIndex = 213;
            this.BtnSave.Text = "&POST";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel3.Controls.Add(this.DGrid);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1234, 480);
            this.panel3.TabIndex = 2;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtCheck,
            this.ProductId,
            this.VoucherDate,
            this.VoucherNo,
            this.FinishedGoods,
            this.Qty,
            this.Rate,
            this.Amount});
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(1234, 480);
            this.DGrid.TabIndex = 5;
            this.DGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellDoubleClick);
            // 
            // GTxtCheck
            // 
            this.GTxtCheck.DataPropertyName = "CheckBox";
            this.GTxtCheck.FalseValue = "";
            this.GTxtCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.GTxtCheck.HeaderText = "";
            this.GTxtCheck.IndeterminateValue = "";
            this.GTxtCheck.Name = "GTxtCheck";
            this.GTxtCheck.ReadOnly = true;
            this.GTxtCheck.ThreeState = true;
            this.GTxtCheck.TrueValue = "";
            this.GTxtCheck.Width = 65;
            // 
            // ProductId
            // 
            this.ProductId.DataPropertyName = "ProductId";
            this.ProductId.HeaderText = "ProductId";
            this.ProductId.Name = "ProductId";
            this.ProductId.ReadOnly = true;
            this.ProductId.Visible = false;
            // 
            // VoucherDate
            // 
            this.VoucherDate.DataPropertyName = "VoucherMiti";
            this.VoucherDate.HeaderText = "DATE";
            this.VoucherDate.Name = "VoucherDate";
            this.VoucherDate.ReadOnly = true;
            this.VoucherDate.Width = 120;
            // 
            // VoucherNo
            // 
            this.VoucherNo.DataPropertyName = "VoucherNo";
            this.VoucherNo.HeaderText = "VOUCHER NO";
            this.VoucherNo.Name = "VoucherNo";
            this.VoucherNo.ReadOnly = true;
            this.VoucherNo.Width = 150;
            // 
            // FinishedGoods
            // 
            this.FinishedGoods.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FinishedGoods.DataPropertyName = "ProductDesc";
            this.FinishedGoods.HeaderText = "FINISHED GOODS";
            this.FinishedGoods.Name = "FinishedGoods";
            this.FinishedGoods.ReadOnly = true;
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Qty.DefaultCellStyle = dataGridViewCellStyle1;
            this.Qty.HeaderText = "QTY";
            this.Qty.Name = "Qty";
            this.Qty.ReadOnly = true;
            this.Qty.Width = 120;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomRight;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle2;
            this.Rate.HeaderText = "RATE";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 120;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle3;
            this.Amount.HeaderText = "AMOUNT";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 150;
            // 
            // FrmAutoConsumption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 518);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAutoConsumption";
            this.ShowIcon = false;
            this.Text = "Auto Consumption";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmAutoConsumption_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.DataGridView DGrid;
        private MrPanel panel2;
        private MrPanel panel3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GTxtCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VoucherNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FinishedGoods;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private DevExpress.XtraEditors.SimpleButton BtnRefresh;
    }
}