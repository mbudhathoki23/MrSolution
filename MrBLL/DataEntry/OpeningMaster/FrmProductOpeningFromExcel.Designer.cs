namespace MrBLL.DataEntry.OpeningMaster
{
    partial class FrmProductOpeningFromExcel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductOpeningFromExcel));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDownloadFormat = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.ChkUpdateProduct = new System.Windows.Forms.CheckBox();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.BtnFileBrowser = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrProductDataGridView = new MrDAL.Control.ControlsEx.Control.MrDataGridView();
            this.ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrGroup1.SuspendLayout();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mrProductDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(718, 20);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 33);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnDownloadFormat
            // 
            this.BtnDownloadFormat.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDownloadFormat.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDownloadFormat.Appearance.Options.UseFont = true;
            this.BtnDownloadFormat.Appearance.Options.UseForeColor = true;
            this.BtnDownloadFormat.Location = new System.Drawing.Point(154, 16);
            this.BtnDownloadFormat.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDownloadFormat.Name = "BtnDownloadFormat";
            this.BtnDownloadFormat.Size = new System.Drawing.Size(200, 33);
            this.BtnDownloadFormat.TabIndex = 2;
            this.BtnDownloadFormat.Text = "DOWNLOAD &FORMAT";
            this.BtnDownloadFormat.Click += new System.EventHandler(this.BtnDownloadFormat_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(607, 20);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 33);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&IMPORT";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ChkUpdateProduct
            // 
            this.ChkUpdateProduct.AutoSize = true;
            this.ChkUpdateProduct.Location = new System.Drawing.Point(20, 21);
            this.ChkUpdateProduct.Name = "ChkUpdateProduct";
            this.ChkUpdateProduct.Size = new System.Drawing.Size(95, 23);
            this.ChkUpdateProduct.TabIndex = 0;
            this.ChkUpdateProduct.Text = "UPDATE ";
            this.ChkUpdateProduct.UseVisualStyleBackColor = true;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrDAL.Control.ControlsEx.Control.MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.btnCancel);
            this.mrGroup1.Controls.Add(this.BtnDownloadFormat);
            this.mrGroup1.Controls.Add(this.btnSave);
            this.mrGroup1.Controls.Add(this.BtnFileBrowser);
            this.mrGroup1.Controls.Add(this.ChkUpdateProduct);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(0, 594);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 5;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 5;
            this.mrGroup1.Size = new System.Drawing.Size(843, 57);
            this.mrGroup1.TabIndex = 1;
            // 
            // BtnFileBrowser
            // 
            this.BtnFileBrowser.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnFileBrowser.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnFileBrowser.Appearance.Options.UseFont = true;
            this.BtnFileBrowser.Appearance.Options.UseForeColor = true;
            this.BtnFileBrowser.Location = new System.Drawing.Point(357, 16);
            this.BtnFileBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFileBrowser.Name = "BtnFileBrowser";
            this.BtnFileBrowser.Size = new System.Drawing.Size(158, 33);
            this.BtnFileBrowser.TabIndex = 1;
            this.BtnFileBrowser.Text = "FILE &BROWSE";
            this.BtnFileBrowser.Click += new System.EventHandler(this.BtnFileBrowser_Click);
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.mrProductDataGridView);
            this.mrPanel1.Controls.Add(this.mrGroup1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(843, 651);
            this.mrPanel1.TabIndex = 1;
            // 
            // mrProductDataGridView
            // 
            this.mrProductDataGridView.AllowUserToAddRows = false;
            this.mrProductDataGridView.AllowUserToDeleteRows = false;
            this.mrProductDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.mrProductDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.mrProductDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mrProductDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ShortName,
            this.Description,
            this.Quantity,
            this.Unit,
            this.Rate,
            this.Amount,
            this.BranchId,
            this.CompanyId});
            this.mrProductDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrProductDataGridView.DoubleBufferEnabled = true;
            this.mrProductDataGridView.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.mrProductDataGridView.Location = new System.Drawing.Point(0, 0);
            this.mrProductDataGridView.MultiSelect = false;
            this.mrProductDataGridView.Name = "mrProductDataGridView";
            this.mrProductDataGridView.ReadOnly = true;
            this.mrProductDataGridView.Size = new System.Drawing.Size(843, 594);
            this.mrProductDataGridView.TabIndex = 0;
            // 
            // ShortName
            // 
            this.ShortName.DataPropertyName = "ShortName";
            this.ShortName.FillWeight = 150F;
            this.ShortName.Frozen = true;
            this.ShortName.HeaderText = "Short Name";
            this.ShortName.Name = "ShortName";
            this.ShortName.ReadOnly = true;
            this.ShortName.Width = 123;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.FillWeight = 120F;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 120;
            // 
            // Quantity
            // 
            this.Quantity.DataPropertyName = "Quantity";
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 67;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            this.Rate.Width = 69;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            this.Amount.Width = 92;
            // 
            // BranchId
            // 
            this.BranchId.DataPropertyName = "BranchId";
            this.BranchId.HeaderText = "Branch Id";
            this.BranchId.Name = "BranchId";
            this.BranchId.ReadOnly = true;
            this.BranchId.Width = 108;
            // 
            // CompanyId
            // 
            this.CompanyId.DataPropertyName = "CompanyId";
            this.CompanyId.HeaderText = "Company Id";
            this.CompanyId.Name = "CompanyId";
            this.CompanyId.ReadOnly = true;
            this.CompanyId.Width = 121;
            // 
            // FrmProductOpeningFromExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 651);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProductOpeningFromExcel";
            this.ShowIcon = false;
            this.Text = "PRODUCT OPENING EXCEL IMPORT";
            this.Load += new System.EventHandler(this.ProductOpeningFromExcel_Load);
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.mrPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mrProductDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnDownloadFormat;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.CheckBox ChkUpdateProduct;
        private MrDAL.Control.ControlsEx.Control.MrGroup mrGroup1;
        private DevExpress.XtraEditors.SimpleButton BtnFileBrowser;
        private MrDAL.Control.ControlsEx.Control.MrPanel mrPanel1;
        private MrDAL.Control.ControlsEx.Control.MrDataGridView mrProductDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyId;
    }
}