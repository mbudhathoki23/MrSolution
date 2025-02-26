using MrDAL.Control.ControlsEx.Control;

namespace MrClientManagement.Master.ExcelImport
{
    partial class FrmClientExcelImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmClientExcelImport));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.RGrid = new DataGridViewEx();
            this.GTxtCompany = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtPanNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtEmailAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtContactNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtPhoneNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mrGroup1 = new MrGroup();
            this.BtnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.BtnSample = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnImport = new DevExpress.XtraEditors.SimpleButton();
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).BeginInit();
            this.mrGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.RGrid);
            this.panelControl1.Controls.Add(this.mrGroup1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1177, 503);
            this.panelControl1.TabIndex = 0;
            // 
            // RGrid
            // 
            this.RGrid.AllowUserToAddRows = false;
            this.RGrid.AllowUserToDeleteRows = false;
            this.RGrid.AllowUserToResizeRows = false;
            this.RGrid.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.RGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.RGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtCompany,
            this.GTxtPanNo,
            this.GTxtAddress,
            this.GTxtEmailAddress,
            this.GTxtContactNo,
            this.GTxtPhoneNo,
            this.GTxtSource});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.RGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.RGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RGrid.DoubleBufferEnabled = true;
            this.RGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.RGrid.Location = new System.Drawing.Point(2, 2);
            this.RGrid.MultiSelect = false;
            this.RGrid.Name = "RGrid";
            this.RGrid.ReadOnly = true;
            this.RGrid.RowHeadersWidth = 20;
            this.RGrid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RGrid.Size = new System.Drawing.Size(1173, 447);
            this.RGrid.TabIndex = 1;
            // 
            // GTxtCompany
            // 
            this.GTxtCompany.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.GTxtCompany.DataPropertyName = "COMPANY";
            this.GTxtCompany.HeaderText = "COMPANY";
            this.GTxtCompany.MinimumWidth = 6;
            this.GTxtCompany.Name = "GTxtCompany";
            this.GTxtCompany.ReadOnly = true;
            // 
            // GTxtPanNo
            // 
            this.GTxtPanNo.DataPropertyName = "PAN_NO";
            this.GTxtPanNo.HeaderText = "PAN_NO";
            this.GTxtPanNo.MinimumWidth = 6;
            this.GTxtPanNo.Name = "GTxtPanNo";
            this.GTxtPanNo.ReadOnly = true;
            this.GTxtPanNo.Width = 125;
            // 
            // GTxtAddress
            // 
            this.GTxtAddress.DataPropertyName = "ADDRESS";
            this.GTxtAddress.HeaderText = "ADDRESS";
            this.GTxtAddress.MinimumWidth = 6;
            this.GTxtAddress.Name = "GTxtAddress";
            this.GTxtAddress.ReadOnly = true;
            this.GTxtAddress.Width = 160;
            // 
            // GTxtEmailAddress
            // 
            this.GTxtEmailAddress.DataPropertyName = "EMAIL";
            this.GTxtEmailAddress.HeaderText = "EMAIL";
            this.GTxtEmailAddress.MinimumWidth = 6;
            this.GTxtEmailAddress.Name = "GTxtEmailAddress";
            this.GTxtEmailAddress.ReadOnly = true;
            this.GTxtEmailAddress.Width = 160;
            // 
            // GTxtContactNo
            // 
            this.GTxtContactNo.DataPropertyName = "CONTACT_NO";
            this.GTxtContactNo.HeaderText = "CONTACT_NO";
            this.GTxtContactNo.MinimumWidth = 6;
            this.GTxtContactNo.Name = "GTxtContactNo";
            this.GTxtContactNo.ReadOnly = true;
            this.GTxtContactNo.Width = 175;
            // 
            // GTxtPhoneNo
            // 
            this.GTxtPhoneNo.DataPropertyName = "PHONE_NO";
            this.GTxtPhoneNo.HeaderText = "PHONE_NO";
            this.GTxtPhoneNo.MinimumWidth = 6;
            this.GTxtPhoneNo.Name = "GTxtPhoneNo";
            this.GTxtPhoneNo.ReadOnly = true;
            this.GTxtPhoneNo.Width = 150;
            // 
            // GTxtSource
            // 
            this.GTxtSource.DataPropertyName = "SOURCE";
            this.GTxtSource.HeaderText = "SOURCE";
            this.GTxtSource.MinimumWidth = 6;
            this.GTxtSource.Name = "GTxtSource";
            this.GTxtSource.ReadOnly = true;
            this.GTxtSource.Width = 165;
            // 
            // mrGroup1
            // 
            this.mrGroup1.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.mrGroup1.BackgroundGradientColor = System.Drawing.Color.White;
            this.mrGroup1.BackgroundGradientMode = MrGroup.GroupBoxGradientMode.None;
            this.mrGroup1.BorderColor = System.Drawing.Color.White;
            this.mrGroup1.BorderThickness = 1F;
            this.mrGroup1.Controls.Add(this.BtnUpload);
            this.mrGroup1.Controls.Add(this.BtnSample);
            this.mrGroup1.Controls.Add(this.BtnCancel);
            this.mrGroup1.Controls.Add(this.BtnImport);
            this.mrGroup1.Controls.Add(this.BtnUpdate);
            this.mrGroup1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.mrGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mrGroup1.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mrGroup1.GroupImage = null;
            this.mrGroup1.GroupTitle = "";
            this.mrGroup1.Location = new System.Drawing.Point(2, 449);
            this.mrGroup1.Name = "mrGroup1";
            this.mrGroup1.Padding = new System.Windows.Forms.Padding(20);
            this.mrGroup1.PaintGroupBox = false;
            this.mrGroup1.RoundCorners = 3;
            this.mrGroup1.ShadowColor = System.Drawing.Color.DarkGray;
            this.mrGroup1.ShadowControl = false;
            this.mrGroup1.ShadowThickness = 3;
            this.mrGroup1.Size = new System.Drawing.Size(1173, 52);
            this.mrGroup1.TabIndex = 0;
            // 
            // BtnUpload
            // 
            this.BtnUpload.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpload.Appearance.Options.UseFont = true;
            this.BtnUpload.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnUpload.ImageOptions.Image")));
            this.BtnUpload.Location = new System.Drawing.Point(246, 15);
            this.BtnUpload.Name = "BtnUpload";
            this.BtnUpload.Size = new System.Drawing.Size(123, 33);
            this.BtnUpload.TabIndex = 2;
            this.BtnUpload.Text = "UPLOAD";
            this.BtnUpload.Click += new System.EventHandler(this.BtnUpload_Click);
            // 
            // BtnSample
            // 
            this.BtnSample.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSample.Appearance.Options.UseFont = true;
            this.BtnSample.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnSample.ImageOptions.Image")));
            this.BtnSample.Location = new System.Drawing.Point(123, 15);
            this.BtnSample.Name = "BtnSample";
            this.BtnSample.Size = new System.Drawing.Size(123, 33);
            this.BtnSample.TabIndex = 1;
            this.BtnSample.Text = "SAMPLE";
            this.BtnSample.Click += new System.EventHandler(this.BtnSample_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.ImageOptions.Image")));
            this.BtnCancel.Location = new System.Drawing.Point(1046, 15);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(117, 33);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BtnImport.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Appearance.Options.UseFont = true;
            this.BtnImport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnImport.ImageOptions.Image")));
            this.BtnImport.Location = new System.Drawing.Point(929, 15);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(116, 33);
            this.BtnImport.TabIndex = 0;
            this.BtnImport.Text = "IMPORT";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdate.Appearance.Options.UseFont = true;
            this.BtnUpdate.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnUpdate.ImageOptions.Image")));
            this.BtnUpdate.Location = new System.Drawing.Point(6, 15);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(116, 33);
            this.BtnUpdate.TabIndex = 0;
            this.BtnUpdate.Text = "UPDATE";
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // FrmClientExcelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 503);
            this.Controls.Add(this.panelControl1);
            this.IconOptions.Image = global::MrClientManagement.Properties.Resources.Micon;
            this.Name = "FrmClientExcelImport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CLIENT INFORMATION IMPORT FROM EXCEL";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RGrid)).EndInit();
            this.mrGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private MrGroup mrGroup1;
        private DataGridViewEx RGrid;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private DevExpress.XtraEditors.SimpleButton BtnImport;
        private DevExpress.XtraEditors.SimpleButton BtnUpdate;
        private DevExpress.XtraEditors.SimpleButton BtnSample;
        private DevExpress.XtraEditors.SimpleButton BtnUpload;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCompany;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtPanNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtEmailAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtContactNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtPhoneNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSource;
    }
}