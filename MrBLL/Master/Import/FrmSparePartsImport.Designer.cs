using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.Import
{
    partial class FrmSparePartsImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSparePartsImport));
            this.mrPanel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.mrProductDataGridView = new MrDAL.Control.ControlsEx.Control.MrDataGridView();
            this.mrGroup1 = new MrDAL.Control.ControlsEx.Control.MrGroup();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDownloadFormat = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnFileBrowser = new DevExpress.XtraEditors.SimpleButton();
            this.ChkUpdateProduct = new System.Windows.Forms.CheckBox();
            this.ShortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AltUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyConv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AltConv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BuyRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SalesRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaxRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRPRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BranchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mrProductDataGridView)).BeginInit();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
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
            this.mrPanel1.Size = new System.Drawing.Size(1464, 591);
            this.mrPanel1.TabIndex = 0;
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
            this.Group,
            this.SubGroup,
            this.Unit,
            this.AltUnit,
            this.QtyConv,
            this.AltConv,
            this.BuyRate,
            this.SalesRate,
            this.TaxRate,
            this.MinStock,
            this.MaxStock,
            this.MRPRate,
            this.BranchId,
            this.CompanyId});
            this.mrProductDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrProductDataGridView.DoubleBufferEnabled = true;
            this.mrProductDataGridView.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.mrProductDataGridView.Location = new System.Drawing.Point(0, 0);
            this.mrProductDataGridView.MultiSelect = false;
            this.mrProductDataGridView.Name = "mrProductDataGridView";
            this.mrProductDataGridView.ReadOnly = true;
            this.mrProductDataGridView.Size = new System.Drawing.Size(1464, 534);
            this.mrProductDataGridView.TabIndex = 0;
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
            this.mrGroup1.Location = new System.Drawing.Point(0, 534);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 5;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 5;
            this.mrGroup1.Size = new System.Drawing.Size(1464, 57);
            this.mrGroup1.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1297, 16);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 33);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // BtnDownloadFormat
            // 
            this.BtnDownloadFormat.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDownloadFormat.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDownloadFormat.Appearance.Options.UseFont = true;
            this.BtnDownloadFormat.Appearance.Options.UseForeColor = true;
            this.BtnDownloadFormat.Location = new System.Drawing.Point(662, 15);
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
            this.btnSave.Location = new System.Drawing.Point(1186, 16);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 33);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&IMPORT";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // BtnFileBrowser
            // 
            this.BtnFileBrowser.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnFileBrowser.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnFileBrowser.Appearance.Options.UseFont = true;
            this.BtnFileBrowser.Appearance.Options.UseForeColor = true;
            this.BtnFileBrowser.Location = new System.Drawing.Point(865, 15);
            this.BtnFileBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFileBrowser.Name = "BtnFileBrowser";
            this.BtnFileBrowser.Size = new System.Drawing.Size(158, 33);
            this.BtnFileBrowser.TabIndex = 1;
            this.BtnFileBrowser.Text = "FILE &BROWSE";
            this.BtnFileBrowser.Click += new System.EventHandler(this.BtnFileBrowser_Click);
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
            // ShortName
            // 
            this.ShortName.DataPropertyName = "ShortName";
            this.ShortName.FillWeight = 120F;
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
            // Group
            // 
            this.Group.DataPropertyName = "Group";
            this.Group.HeaderText = "Group";
            this.Group.Name = "Group";
            this.Group.ReadOnly = true;
            this.Group.Width = 79;
            // 
            // SubGroup
            // 
            this.SubGroup.DataPropertyName = "SubGroup";
            this.SubGroup.HeaderText = "Sub Group";
            this.SubGroup.Name = "SubGroup";
            this.SubGroup.ReadOnly = true;
            this.SubGroup.Width = 112;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 67;
            // 
            // AltUnit
            // 
            this.AltUnit.DataPropertyName = "AltUnit";
            this.AltUnit.HeaderText = "Alt Unit";
            this.AltUnit.Name = "AltUnit";
            this.AltUnit.ReadOnly = true;
            this.AltUnit.Width = 93;
            // 
            // QtyConv
            // 
            this.QtyConv.DataPropertyName = "QtyConv";
            this.QtyConv.HeaderText = "Qty Conv";
            this.QtyConv.Name = "QtyConv";
            this.QtyConv.ReadOnly = true;
            this.QtyConv.Width = 101;
            // 
            // AltConv
            // 
            this.AltConv.DataPropertyName = "AltConv";
            this.AltConv.HeaderText = "Alt Conv";
            this.AltConv.Name = "AltConv";
            this.AltConv.ReadOnly = true;
            this.AltConv.Width = 96;
            // 
            // BuyRate
            // 
            this.BuyRate.DataPropertyName = "BuyRate";
            this.BuyRate.HeaderText = "Buy Rate";
            this.BuyRate.Name = "BuyRate";
            this.BuyRate.ReadOnly = true;
            this.BuyRate.Width = 103;
            // 
            // SalesRate
            // 
            this.SalesRate.DataPropertyName = "SalesRate";
            this.SalesRate.HeaderText = "Sales Rate";
            this.SalesRate.Name = "SalesRate";
            this.SalesRate.ReadOnly = true;
            this.SalesRate.Width = 115;
            // 
            // TaxRate
            // 
            this.TaxRate.DataPropertyName = "TaxRate";
            this.TaxRate.HeaderText = "Tax Rate";
            this.TaxRate.Name = "TaxRate";
            this.TaxRate.ReadOnly = true;
            // 
            // MinStock
            // 
            this.MinStock.DataPropertyName = "MinStock";
            this.MinStock.HeaderText = "Min Stock";
            this.MinStock.Name = "MinStock";
            this.MinStock.ReadOnly = true;
            this.MinStock.Width = 108;
            // 
            // MaxStock
            // 
            this.MaxStock.DataPropertyName = "MaxStock";
            this.MaxStock.HeaderText = "Max Stock";
            this.MaxStock.Name = "MaxStock";
            this.MaxStock.ReadOnly = true;
            this.MaxStock.Width = 110;
            // 
            // MRPRate
            // 
            this.MRPRate.DataPropertyName = "MRPRate";
            this.MRPRate.HeaderText = "MRP Rate";
            this.MRPRate.Name = "MRPRate";
            this.MRPRate.ReadOnly = true;
            this.MRPRate.Width = 107;
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
            // FrmSparePartsImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1464, 591);
            this.Controls.Add(this.mrPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSparePartsImport";
            this.ShowIcon = false;
            this.Text = "IMPORT PRODUCT";
            this.Load += new System.EventHandler(this.FrmProductImport_Load);
            this.mrPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mrProductDataGridView)).EndInit();
            this.mrGroup1.ResumeLayout(false);
            this.mrGroup1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private MrDataGridView mrProductDataGridView;
        private MrGroup mrGroup1;
        private System.Windows.Forms.CheckBox ChkUpdateProduct;
        private DevExpress.XtraEditors.SimpleButton BtnFileBrowser;
        private DevExpress.XtraEditors.SimpleButton BtnDownloadFormat;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Group;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn AltUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyConv;
        private System.Windows.Forms.DataGridViewTextBoxColumn AltConv;
        private System.Windows.Forms.DataGridViewTextBoxColumn BuyRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SalesRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaxRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRPRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BranchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyId;
    }
}