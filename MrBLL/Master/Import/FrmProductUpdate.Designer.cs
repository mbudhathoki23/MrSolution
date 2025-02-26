using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Master.Import
{
    partial class FrmProductUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProductUpdate));
            this.PanelHeader = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.DGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsImport = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnDownloadFormat = new DevExpress.XtraEditors.SimpleButton();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.BtnFileBrowser = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDownload = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImport)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.DGrid);
            this.PanelHeader.Controls.Add(this.panel1);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(0, 0);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(1053, 576);
            this.PanelHeader.TabIndex = 0;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToOrderColumns = true;
            this.DGrid.AllowUserToResizeRows = false;
            this.DGrid.AutoGenerateColumns = false;
            this.DGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGrid.CausesValidation = false;
            this.DGrid.ColumnHeadersHeight = 29;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14});
            this.DGrid.DataSource = this.bsImport;
            this.DGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGrid.Location = new System.Drawing.Point(0, 0);
            this.DGrid.MultiSelect = false;
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DGrid.RowHeadersWidth = 20;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.ShowEditingIcon = false;
            this.DGrid.Size = new System.Drawing.Size(1053, 527);
            this.DGrid.TabIndex = 15;
            this.DGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "BarCode";
            this.Column1.HeaderText = "Bar Code";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.DataPropertyName = "Description";
            this.Column2.HeaderText = "Name";
            this.Column2.MinimumWidth = 150;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "BarCode1";
            this.Column3.HeaderText = "Bar Code1";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 125;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "BarCode2";
            this.Column4.HeaderText = "Bar Code2";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 125;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Type";
            this.Column5.HeaderText = "Product Type";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 125;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "Group";
            this.Column6.HeaderText = "Group";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "SubGroup";
            this.Column7.HeaderText = "Sub-Group";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 125;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "UOM";
            this.Column8.HeaderText = "Unit";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "BuyRate";
            this.Column9.HeaderText = "Buy Rate";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 90;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "SalesRate";
            this.Column10.HeaderText = "Sales Rate";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.Width = 125;
            // 
            // Column11
            // 
            this.Column11.DataPropertyName = "TaxRate";
            this.Column11.HeaderText = "Tax Percent";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Width = 125;
            // 
            // Column12
            // 
            this.Column12.DataPropertyName = "Margin";
            this.Column12.HeaderText = "Margin";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 80;
            // 
            // Column13
            // 
            this.Column13.DataPropertyName = "TradePrice";
            this.Column13.HeaderText = "Trade Price";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Width = 80;
            // 
            // Column14
            // 
            this.Column14.DataPropertyName = "Mrp";
            this.Column14.HeaderText = "Mrp";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Width = 80;
            // 
            // bsImport
            // 
            this.bsImport.AllowNew = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.BtnDownloadFormat);
            this.panel1.Controls.Add(this.clsSeparator2);
            this.panel1.Controls.Add(this.BtnFileBrowser);
            this.panel1.Controls.Add(this.BtnUpdate);
            this.panel1.Controls.Add(this.BtnDownload);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 527);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1053, 49);
            this.panel1.TabIndex = 16;
            // 
            // BtnDownloadFormat
            // 
            this.BtnDownloadFormat.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDownloadFormat.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDownloadFormat.Appearance.Options.UseFont = true;
            this.BtnDownloadFormat.Appearance.Options.UseForeColor = true;
            this.BtnDownloadFormat.Location = new System.Drawing.Point(163, 11);
            this.BtnDownloadFormat.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDownloadFormat.Name = "BtnDownloadFormat";
            this.BtnDownloadFormat.Size = new System.Drawing.Size(200, 33);
            this.BtnDownloadFormat.TabIndex = 1;
            this.BtnDownloadFormat.Text = "DOWNLOAD &FORMAT";
            this.BtnDownloadFormat.Click += new System.EventHandler(this.BtnDownloadFormat_Click);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(-2, 5);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(1038, 2);
            this.clsSeparator2.TabIndex = 15;
            this.clsSeparator2.TabStop = false;
            // 
            // BtnFileBrowser
            // 
            this.BtnFileBrowser.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnFileBrowser.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnFileBrowser.Appearance.Options.UseFont = true;
            this.BtnFileBrowser.Appearance.Options.UseForeColor = true;
            this.BtnFileBrowser.Location = new System.Drawing.Point(1, 11);
            this.BtnFileBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.BtnFileBrowser.Name = "BtnFileBrowser";
            this.BtnFileBrowser.Size = new System.Drawing.Size(158, 33);
            this.BtnFileBrowser.TabIndex = 0;
            this.BtnFileBrowser.Text = "FILE &BROWSE";
            this.BtnFileBrowser.Click += new System.EventHandler(this.BtnFileBrowser_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnUpdate.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnUpdate.Appearance.Options.UseFont = true;
            this.BtnUpdate.Appearance.Options.UseForeColor = true;
            this.BtnUpdate.Location = new System.Drawing.Point(697, 11);
            this.BtnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(114, 33);
            this.BtnUpdate.TabIndex = 4;
            this.BtnUpdate.Text = "&UPDATE";
            this.BtnUpdate.Visible = false;
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // BtnDownload
            // 
            this.BtnDownload.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDownload.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnDownload.Appearance.Options.UseFont = true;
            this.BtnDownload.Appearance.Options.UseForeColor = true;
            this.BtnDownload.Location = new System.Drawing.Point(367, 11);
            this.BtnDownload.Margin = new System.Windows.Forms.Padding(4);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.Size = new System.Drawing.Size(134, 33);
            this.BtnDownload.TabIndex = 2;
            this.BtnDownload.Text = "&DOWNLOAD";
            this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(926, 11);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(109, 33);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&CANCEL";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(815, 11);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(108, 33);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&SAVE";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmProductUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1053, 576);
            this.Controls.Add(this.PanelHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmProductUpdate";
            this.ShowIcon = false;
            this.Text = "Product Import & Update";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmProductUpdate_Load);
            this.PanelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsImport)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton BtnFileBrowser;
        private DevExpress.XtraEditors.SimpleButton BtnDownloadFormat;
        private DevExpress.XtraEditors.SimpleButton BtnDownload;
        private System.Windows.Forms.DataGridView DGrid;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton BtnUpdate;
        private ClsSeparator clsSeparator2;
        private System.Windows.Forms.BindingSource bsImport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private MrPanel PanelHeader;
        private MrPanel panel1;
    }
}