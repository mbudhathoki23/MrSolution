using MrDAL.Control.ControlsEx.Control;

namespace MrSolution.About
{
    partial class FrmUserRight
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MrMenu = new System.Windows.Forms.MenuStrip();
            this.companyInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bsForms = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_UserType = new System.Windows.Forms.Label();
            this.CmbUserRole = new System.Windows.Forms.ComboBox();
            this.CmbUser = new System.Windows.Forms.ComboBox();
            this.lbl_User = new System.Windows.Forms.Label();
            this.BtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.mrGrid1 = new MrDAL.Control.ControlsEx.Control.EntryGridViewEx();
            this.FormId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmParticular = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNewButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colSaveButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmEditButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmUpdateButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmDeleteButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmViewButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmSearchButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmPrintButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmExportButton = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.roundPanel1 = new MrDAL.Control.ControlsEx.Control.RoundPanel();
            this.ChkAboutUs = new DevExpress.XtraEditors.CheckButton();
            this.ChkUtility = new DevExpress.XtraEditors.CheckButton();
            this.ChkStockReports = new DevExpress.XtraEditors.CheckButton();
            this.ChkRegisterReport = new DevExpress.XtraEditors.CheckButton();
            this.ChkFinanceReport = new DevExpress.XtraEditors.CheckButton();
            this.ChkDataEntry = new DevExpress.XtraEditors.CheckButton();
            this.ChkMaster = new DevExpress.XtraEditors.CheckButton();
            this.ChkCompanyInfo = new DevExpress.XtraEditors.CheckButton();
            this.MrMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsForms)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mrGrid1)).BeginInit();
            this.roundPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MrMenu
            // 
            this.MrMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.MrMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MrMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.companyInfoToolStripMenuItem});
            this.MrMenu.Location = new System.Drawing.Point(0, 0);
            this.MrMenu.Name = "MrMenu";
            this.MrMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MrMenu.ShowItemToolTips = true;
            this.MrMenu.Size = new System.Drawing.Size(1432, 24);
            this.MrMenu.Stretch = false;
            this.MrMenu.TabIndex = 1;
            this.MrMenu.TabStop = true;
            // 
            // companyInfoToolStripMenuItem
            // 
            this.companyInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.companyInfoToolStripMenuItem.Name = "companyInfoToolStripMenuItem";
            this.companyInfoToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.companyInfoToolStripMenuItem.Text = "CompanyInfo";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.testToolStripMenuItem.Text = "test";
            // 
            // tToolStripMenuItem
            // 
            this.tToolStripMenuItem.Name = "tToolStripMenuItem";
            this.tToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
            this.tToolStripMenuItem.Text = "t";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_UserType);
            this.panel1.Controls.Add(this.CmbUserRole);
            this.panel1.Controls.Add(this.CmbUser);
            this.panel1.Controls.Add(this.lbl_User);
            this.panel1.Controls.Add(this.BtnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(194, 658);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1238, 48);
            this.panel1.TabIndex = 5;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // lbl_UserType
            // 
            this.lbl_UserType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_UserType.AutoSize = true;
            this.lbl_UserType.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_UserType.Location = new System.Drawing.Point(608, 17);
            this.lbl_UserType.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_UserType.Name = "lbl_UserType";
            this.lbl_UserType.Size = new System.Drawing.Size(85, 20);
            this.lbl_UserType.TabIndex = 37;
            this.lbl_UserType.Text = "User Role";
            // 
            // CmbUserRole
            // 
            this.CmbUserRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbUserRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUserRole.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbUserRole.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbUserRole.FormattingEnabled = true;
            this.CmbUserRole.Location = new System.Drawing.Point(698, 14);
            this.CmbUserRole.Name = "CmbUserRole";
            this.CmbUserRole.Size = new System.Drawing.Size(152, 28);
            this.CmbUserRole.TabIndex = 19;
            this.CmbUserRole.SelectedIndexChanged += new System.EventHandler(this.CmbUserRole_SelectedIndexChanged);
            // 
            // CmbUser
            // 
            this.CmbUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CmbUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CmbUser.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbUser.FormattingEnabled = true;
            this.CmbUser.Location = new System.Drawing.Point(953, 13);
            this.CmbUser.Name = "CmbUser";
            this.CmbUser.Size = new System.Drawing.Size(201, 25);
            this.CmbUser.TabIndex = 18;
            this.CmbUser.SelectedIndexChanged += new System.EventHandler(this.CmbUser_SelectedIndexChanged);
            // 
            // lbl_User
            // 
            this.lbl_User.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_User.AutoSize = true;
            this.lbl_User.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_User.Location = new System.Drawing.Point(868, 16);
            this.lbl_User.Name = "lbl_User";
            this.lbl_User.Size = new System.Drawing.Size(82, 17);
            this.lbl_User.TabIndex = 17;
            this.lbl_User.Text = "User Name";
            // 
            // BtnSave
            // 
            this.BtnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSave.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.BtnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BtnSave.Appearance.Options.UseFont = true;
            this.BtnSave.Appearance.Options.UseForeColor = true;
            this.BtnSave.Location = new System.Drawing.Point(1162, 11);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(67, 28);
            this.BtnSave.TabIndex = 16;
            this.BtnSave.Text = "&SAVE";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // mrGrid1
            // 
            this.mrGrid1.AllowUserToAddRows = false;
            this.mrGrid1.AllowUserToDeleteRows = false;
            this.mrGrid1.AllowUserToResizeRows = false;
            this.mrGrid1.AutoGenerateColumns = false;
            this.mrGrid1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.mrGrid1.BlockNavigationOnNextRowOnEnter = true;
            this.mrGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mrGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FormId,
            this.clmParticular,
            this.colNewButton,
            this.colSaveButton,
            this.clmEditButton,
            this.clmUpdateButton,
            this.clmDeleteButton,
            this.clmViewButton,
            this.clmSearchButton,
            this.clmPrintButton,
            this.clmExportButton});
            this.mrGrid1.DataSource = this.bsForms;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Bookman Old Style", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mrGrid1.DefaultCellStyle = dataGridViewCellStyle3;
            this.mrGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mrGrid1.DoubleBufferEnabled = true;
            this.mrGrid1.GridColor = System.Drawing.Color.DarkSlateBlue;
            this.mrGrid1.Location = new System.Drawing.Point(194, 24);
            this.mrGrid1.MultiSelect = false;
            this.mrGrid1.Name = "mrGrid1";
            this.mrGrid1.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Bookman Old Style", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mrGrid1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.mrGrid1.RowHeadersVisible = false;
            this.mrGrid1.RowHeadersWidth = 51;
            this.mrGrid1.Size = new System.Drawing.Size(1238, 634);
            this.mrGrid1.TabIndex = 4;
            this.mrGrid1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mrGrid1_CellContentClick);
            this.mrGrid1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mrGrid1_ColumnHeaderMouseClick);
            this.mrGrid1.Click += new System.EventHandler(this.mrGrid1_Click);
            // 
            // FormId
            // 
            this.FormId.DataPropertyName = "FormId";
            this.FormId.HeaderText = "FormId";
            this.FormId.Name = "FormId";
            this.FormId.ReadOnly = true;
            this.FormId.Visible = false;
            // 
            // clmParticular
            // 
            this.clmParticular.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmParticular.DataPropertyName = "FormName";
            this.clmParticular.HeaderText = "Particular";
            this.clmParticular.MinimumWidth = 6;
            this.clmParticular.Name = "clmParticular";
            this.clmParticular.ReadOnly = true;
            // 
            // colNewButton
            // 
            this.colNewButton.DataPropertyName = "NewButtonCheck";
            this.colNewButton.HeaderText = "New";
            this.colNewButton.MinimumWidth = 6;
            this.colNewButton.Name = "colNewButton";
            this.colNewButton.ReadOnly = true;
            this.colNewButton.Width = 80;
            // 
            // colSaveButton
            // 
            this.colSaveButton.DataPropertyName = "SaveButtonCheck";
            this.colSaveButton.HeaderText = "Save";
            this.colSaveButton.MinimumWidth = 6;
            this.colSaveButton.Name = "colSaveButton";
            this.colSaveButton.ReadOnly = true;
            this.colSaveButton.Width = 80;
            // 
            // clmEditButton
            // 
            this.clmEditButton.DataPropertyName = "EditButtonCheck";
            this.clmEditButton.HeaderText = "Edit";
            this.clmEditButton.MinimumWidth = 6;
            this.clmEditButton.Name = "clmEditButton";
            this.clmEditButton.ReadOnly = true;
            this.clmEditButton.Width = 80;
            // 
            // clmUpdateButton
            // 
            this.clmUpdateButton.DataPropertyName = "UpdateButtonCheck";
            this.clmUpdateButton.HeaderText = "Update";
            this.clmUpdateButton.MinimumWidth = 6;
            this.clmUpdateButton.Name = "clmUpdateButton";
            this.clmUpdateButton.ReadOnly = true;
            this.clmUpdateButton.Width = 80;
            // 
            // clmDeleteButton
            // 
            this.clmDeleteButton.DataPropertyName = "DeleteButtonCheck";
            this.clmDeleteButton.HeaderText = "Delete";
            this.clmDeleteButton.MinimumWidth = 6;
            this.clmDeleteButton.Name = "clmDeleteButton";
            this.clmDeleteButton.ReadOnly = true;
            this.clmDeleteButton.Width = 80;
            // 
            // clmViewButton
            // 
            this.clmViewButton.DataPropertyName = "ViewButtonCheck";
            this.clmViewButton.HeaderText = "View";
            this.clmViewButton.MinimumWidth = 6;
            this.clmViewButton.Name = "clmViewButton";
            this.clmViewButton.ReadOnly = true;
            this.clmViewButton.Width = 80;
            // 
            // clmSearchButton
            // 
            this.clmSearchButton.DataPropertyName = "SearchButtonCheck";
            this.clmSearchButton.HeaderText = "Search";
            this.clmSearchButton.MinimumWidth = 6;
            this.clmSearchButton.Name = "clmSearchButton";
            this.clmSearchButton.ReadOnly = true;
            this.clmSearchButton.Width = 80;
            // 
            // clmPrintButton
            // 
            this.clmPrintButton.DataPropertyName = "PrintButtonCheck";
            this.clmPrintButton.HeaderText = "Print";
            this.clmPrintButton.MinimumWidth = 6;
            this.clmPrintButton.Name = "clmPrintButton";
            this.clmPrintButton.ReadOnly = true;
            this.clmPrintButton.Width = 80;
            // 
            // clmExportButton
            // 
            this.clmExportButton.DataPropertyName = "ExportButtonCheck";
            this.clmExportButton.HeaderText = "Export";
            this.clmExportButton.MinimumWidth = 6;
            this.clmExportButton.Name = "clmExportButton";
            this.clmExportButton.ReadOnly = true;
            this.clmExportButton.Width = 80;
            // 
            // roundPanel1
            // 
            this.roundPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundPanel1.Controls.Add(this.ChkAboutUs);
            this.roundPanel1.Controls.Add(this.ChkUtility);
            this.roundPanel1.Controls.Add(this.ChkStockReports);
            this.roundPanel1.Controls.Add(this.ChkRegisterReport);
            this.roundPanel1.Controls.Add(this.ChkFinanceReport);
            this.roundPanel1.Controls.Add(this.ChkDataEntry);
            this.roundPanel1.Controls.Add(this.ChkMaster);
            this.roundPanel1.Controls.Add(this.ChkCompanyInfo);
            this.roundPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.roundPanel1.Location = new System.Drawing.Point(0, 24);
            this.roundPanel1.Name = "roundPanel1";
            this.roundPanel1.Radious = 25;
            this.roundPanel1.Size = new System.Drawing.Size(194, 682);
            this.roundPanel1.TabIndex = 2;
            this.roundPanel1.TabStop = false;
            this.roundPanel1.Text = "SELECT MENU";
            this.roundPanel1.TitleBackColor = System.Drawing.Color.DarkSlateBlue;
            this.roundPanel1.TitleFont = new System.Drawing.Font("Bookman Old Style", 12F);
            this.roundPanel1.TitleForeColor = System.Drawing.Color.White;
            this.roundPanel1.TitleHatchStyle = System.Drawing.Drawing2D.HatchStyle.Percent60;
            // 
            // ChkAboutUs
            // 
            this.ChkAboutUs.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAboutUs.Appearance.Options.UseFont = true;
            this.ChkAboutUs.Location = new System.Drawing.Point(12, 400);
            this.ChkAboutUs.Name = "ChkAboutUs";
            this.ChkAboutUs.Size = new System.Drawing.Size(171, 45);
            this.ChkAboutUs.TabIndex = 7;
            this.ChkAboutUs.Text = "ABOUT US";
            this.ChkAboutUs.CheckedChanged += new System.EventHandler(this.ChkAboutUs_CheckedChanged);
            // 
            // ChkUtility
            // 
            this.ChkUtility.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkUtility.Appearance.Options.UseFont = true;
            this.ChkUtility.Location = new System.Drawing.Point(12, 347);
            this.ChkUtility.Name = "ChkUtility";
            this.ChkUtility.Size = new System.Drawing.Size(171, 45);
            this.ChkUtility.TabIndex = 6;
            this.ChkUtility.Text = "UTILITY";
            this.ChkUtility.CheckedChanged += new System.EventHandler(this.ChkUtility_CheckedChanged);
            // 
            // ChkStockReports
            // 
            this.ChkStockReports.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkStockReports.Appearance.Options.UseFont = true;
            this.ChkStockReports.Location = new System.Drawing.Point(12, 294);
            this.ChkStockReports.Name = "ChkStockReports";
            this.ChkStockReports.Size = new System.Drawing.Size(171, 45);
            this.ChkStockReports.TabIndex = 5;
            this.ChkStockReports.Text = "STOCK REPORTS";
            this.ChkStockReports.CheckedChanged += new System.EventHandler(this.ChkStockReports_CheckedChanged);
            // 
            // ChkRegisterReport
            // 
            this.ChkRegisterReport.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkRegisterReport.Appearance.Options.UseFont = true;
            this.ChkRegisterReport.Location = new System.Drawing.Point(12, 241);
            this.ChkRegisterReport.Name = "ChkRegisterReport";
            this.ChkRegisterReport.Size = new System.Drawing.Size(171, 45);
            this.ChkRegisterReport.TabIndex = 4;
            this.ChkRegisterReport.Text = "REGISTER REPORTS";
            this.ChkRegisterReport.CheckedChanged += new System.EventHandler(this.ChkRegisterReport_CheckedChanged);
            // 
            // ChkFinanceReport
            // 
            this.ChkFinanceReport.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFinanceReport.Appearance.Options.UseFont = true;
            this.ChkFinanceReport.Location = new System.Drawing.Point(12, 188);
            this.ChkFinanceReport.Name = "ChkFinanceReport";
            this.ChkFinanceReport.Size = new System.Drawing.Size(171, 45);
            this.ChkFinanceReport.TabIndex = 3;
            this.ChkFinanceReport.Text = "FINANCE REPORT";
            this.ChkFinanceReport.CheckedChanged += new System.EventHandler(this.ChkFinanceReport_CheckedChanged);
            // 
            // ChkDataEntry
            // 
            this.ChkDataEntry.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDataEntry.Appearance.Options.UseFont = true;
            this.ChkDataEntry.Location = new System.Drawing.Point(12, 135);
            this.ChkDataEntry.Name = "ChkDataEntry";
            this.ChkDataEntry.Size = new System.Drawing.Size(171, 45);
            this.ChkDataEntry.TabIndex = 2;
            this.ChkDataEntry.Text = "DATE ENTRY";
            this.ChkDataEntry.CheckedChanged += new System.EventHandler(this.ChkDataEntry_CheckedChanged);
            // 
            // ChkMaster
            // 
            this.ChkMaster.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkMaster.Appearance.Options.UseFont = true;
            this.ChkMaster.Location = new System.Drawing.Point(12, 82);
            this.ChkMaster.Name = "ChkMaster";
            this.ChkMaster.Size = new System.Drawing.Size(171, 45);
            this.ChkMaster.TabIndex = 1;
            this.ChkMaster.Text = "MASTER";
            this.ChkMaster.CheckedChanged += new System.EventHandler(this.ChkMaster_CheckedChanged);
            // 
            // ChkCompanyInfo
            // 
            this.ChkCompanyInfo.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkCompanyInfo.Appearance.Options.UseFont = true;
            this.ChkCompanyInfo.Location = new System.Drawing.Point(12, 29);
            this.ChkCompanyInfo.Name = "ChkCompanyInfo";
            this.ChkCompanyInfo.Size = new System.Drawing.Size(171, 45);
            this.ChkCompanyInfo.TabIndex = 0;
            this.ChkCompanyInfo.Text = "COMPANY INFO";
            this.ChkCompanyInfo.CheckedChanged += new System.EventHandler(this.ChkCompanyInfo_CheckedChanged);
            // 
            // FrmUserRight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 706);
            this.Controls.Add(this.mrGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.roundPanel1);
            this.Controls.Add(this.MrMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "FrmUserRight";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "USER MENU RIGHTS";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUserRight_FormClosing);
            this.Load += new System.EventHandler(this.FrmUserRight_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUserRight_KeyDown);
            this.MrMenu.ResumeLayout(false);
            this.MrMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsForms)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mrGrid1)).EndInit();
            this.roundPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip MrMenu;
        private RoundPanel roundPanel1;
        private EntryGridViewEx mrGrid1;
        private DevExpress.XtraEditors.CheckButton ChkCompanyInfo;
        private DevExpress.XtraEditors.CheckButton ChkAboutUs;
        private DevExpress.XtraEditors.CheckButton ChkUtility;
        private DevExpress.XtraEditors.CheckButton ChkStockReports;
        private DevExpress.XtraEditors.CheckButton ChkRegisterReport;
        private DevExpress.XtraEditors.CheckButton ChkFinanceReport;
        private DevExpress.XtraEditors.CheckButton ChkDataEntry;
        private DevExpress.XtraEditors.CheckButton ChkMaster;
        private System.Windows.Forms.ToolStripMenuItem companyInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton BtnSave;
        private System.Windows.Forms.ComboBox CmbUser;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.ComboBox CmbUserRole;
        private System.Windows.Forms.Label lbl_UserType;
        private System.Windows.Forms.BindingSource bsForms;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmParticular;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colNewButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colSaveButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmEditButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmUpdateButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmDeleteButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmViewButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmSearchButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmPrintButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmExportButton;
    }
}