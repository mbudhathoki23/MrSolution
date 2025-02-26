using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.DataEntry.OpeningMaster
{
    partial class FrmLedgerImportFromExcel
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
            this.mrPanel1 = new MrPanel();
            this.RGrid = new DataGridViewEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.BtnDownloadSample = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.mrPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mrPanel1
            // 
            this.mrPanel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrPanel1.Controls.Add(this.RGrid);
            this.mrPanel1.Controls.Add(this.groupBox1);
            this.mrPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrPanel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrPanel1.Location = new System.Drawing.Point(0, 0);
            this.mrPanel1.Name = "mrPanel1";
            this.mrPanel1.Size = new System.Drawing.Size(1204, 591);
            this.mrPanel1.TabIndex = 0;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.RGrid.ColumnHeadersHeight = 30;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(0, 0);
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersWidth = 20;
            this.RGrid.Size = new System.Drawing.Size(1204, 534);
            this.RGrid.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnUpload);
            this.groupBox1.Controls.Add(this.BtnDownloadSample);
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 534);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1204, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // BtnUpload
            // 
            this.BtnUpload.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnUpload.Appearance.Options.UseFont = true;
            this.BtnUpload.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnUpload.Location = new System.Drawing.Point(228, 12);
            this.BtnUpload.Name = "BtnUpload";
            this.BtnUpload.Size = new System.Drawing.Size(140, 41);
            this.BtnUpload.TabIndex = 3;
            this.BtnUpload.Text = "&LOAD DATA";
            this.BtnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // BtnDownloadSample
            // 
            this.BtnDownloadSample.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnDownloadSample.Appearance.Options.UseFont = true;
            this.BtnDownloadSample.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnDownloadSample.Location = new System.Drawing.Point(12, 12);
            this.BtnDownloadSample.Name = "BtnDownloadSample";
            this.BtnDownloadSample.Size = new System.Drawing.Size(210, 41);
            this.BtnDownloadSample.TabIndex = 2;
            this.BtnDownloadSample.Text = "&DOWNLOAD SAMPLE";
            this.BtnDownloadSample.Click += new System.EventHandler(this.BtnDownloadSample_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(1069, 13);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(129, 44);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Add;
            this.BtnSave.Location = new System.Drawing.Point(944, 13);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(123, 44);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&IMPORT";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmLedgerImportFromExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 591);
            this.Controls.Add(this.mrPanel1);
            this.KeyPreview = true;
            this.Name = "FrmLedgerImportFromExcel";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LEDGER OPENING IMPORT FROM EXCEL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLedgerImportFromExcel_Load);
            this.mrPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MrPanel mrPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DataGridViewEx RGrid;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnDownloadSample;
        private DevExpress.XtraEditors.SimpleButton BtnUpload;
    }
}