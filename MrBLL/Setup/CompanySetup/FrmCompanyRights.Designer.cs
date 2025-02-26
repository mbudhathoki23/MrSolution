using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Setup.CompanySetup
{
    partial class FrmCompanyRights
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SGrid = new MrDAL.Control.ControlsEx.Control.DataGridViewEx();
            this.GTxtCompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtSNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GTxtCompanyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.BtnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnUser = new System.Windows.Forms.Button();
            this.TxtUserInfo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_User = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.StorePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SGrid
            // 
            this.SGrid.AllowUserToAddRows = false;
            this.SGrid.AllowUserToDeleteRows = false;
            this.SGrid.AllowUserToResizeRows = false;
            this.SGrid.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.SGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GTxtCompanyId,
            this.GTxtSNo,
            this.GTxtFileName,
            this.GTxtCompanyName});
            this.SGrid.DoubleBufferEnabled = true;
            this.SGrid.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.SGrid.Location = new System.Drawing.Point(5, 36);
            this.SGrid.Margin = new System.Windows.Forms.Padding(2);
            this.SGrid.Name = "SGrid";
            this.SGrid.ReadOnly = true;
            this.SGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SGrid.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.SGrid.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.SGrid.RowTemplate.Height = 24;
            this.SGrid.Size = new System.Drawing.Size(591, 241);
            this.SGrid.TabIndex = 1;
            // 
            // GTxtCompanyId
            // 
            this.GTxtCompanyId.HeaderText = "CompanyId";
            this.GTxtCompanyId.Name = "GTxtCompanyId";
            this.GTxtCompanyId.ReadOnly = true;
            this.GTxtCompanyId.Visible = false;
            this.GTxtCompanyId.Width = 5;
            // 
            // GTxtSNo
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.GTxtSNo.DefaultCellStyle = dataGridViewCellStyle1;
            this.GTxtSNo.HeaderText = "SNo";
            this.GTxtSNo.Name = "GTxtSNo";
            this.GTxtSNo.ReadOnly = true;
            this.GTxtSNo.Width = 65;
            // 
            // GTxtFileName
            // 
            this.GTxtFileName.HeaderText = "File Name";
            this.GTxtFileName.Name = "GTxtFileName";
            this.GTxtFileName.ReadOnly = true;
            this.GTxtFileName.Width = 150;
            // 
            // GTxtCompanyName
            // 
            this.GTxtCompanyName.HeaderText = "Company Name";
            this.GTxtCompanyName.Name = "GTxtCompanyName";
            this.GTxtCompanyName.ReadOnly = true;
            this.GTxtCompanyName.Width = 500;
            // 
            // BtnSave
            // 
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.ImageOptions.Image = global::MrBLL.Properties.Resources.Save;
            this.BtnSave.Location = new System.Drawing.Point(389, 11);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(88, 33);
            this.BtnSave.TabIndex = 0;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancel.Appearance.Options.UseFont = true;
            this.BtnCancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.BtnCancel.Location = new System.Drawing.Point(479, 11);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(103, 33);
            this.BtnCancel.TabIndex = 1;
            this.BtnCancel.Text = "&CANCEL";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnCancel);
            this.groupBox1.Controls.Add(this.BtnSave);
            this.groupBox1.Location = new System.Drawing.Point(7, 269);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 46);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnUser);
            this.StorePanel.Controls.Add(this.TxtUserInfo);
            this.StorePanel.Controls.Add(this.SGrid);
            this.StorePanel.Controls.Add(this.lbl_User);
            this.StorePanel.Controls.Add(this.groupBox1);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(600, 322);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnUser
            // 
            this.BtnUser.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnUser.Location = new System.Drawing.Point(565, 6);
            this.BtnUser.Name = "BtnUser";
            this.BtnUser.Size = new System.Drawing.Size(30, 27);
            this.BtnUser.TabIndex = 3;
            this.BtnUser.UseVisualStyleBackColor = true;
            this.BtnUser.Click += new System.EventHandler(this.BtnUser_Click);
            // 
            // TxtUserInfo
            // 
            this.TxtUserInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtUserInfo.Location = new System.Drawing.Point(108, 7);
            this.TxtUserInfo.Name = "TxtUserInfo";
            this.TxtUserInfo.ReadOnly = true;
            this.TxtUserInfo.Size = new System.Drawing.Size(456, 25);
            this.TxtUserInfo.TabIndex = 0;
            this.TxtUserInfo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUserInfo_KeyDown);
            // 
            // lbl_User
            // 
            this.lbl_User.AutoSize = true;
            this.lbl_User.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.lbl_User.Location = new System.Drawing.Point(9, 9);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(93, 19);
            this.lbl_User.TabIndex = 9;
            this.lbl_User.Text = "User Name";
            // 
            // FrmCompanyRights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(600, 322);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmCompanyRights";
            this.ShowIcon = false;
            this.Text = "Company Rights";
            this.Load += new System.EventHandler(this.CompanyRights_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmCompanyRights_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.SGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DataGridViewEx SGrid;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private DevExpress.XtraEditors.SimpleButton BtnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private MrPanel StorePanel;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCompanyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtSNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtDataBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtCompanyName;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.Button BtnUser;
        private MrTextBox TxtUserInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn GTxtFileName;
    }
}