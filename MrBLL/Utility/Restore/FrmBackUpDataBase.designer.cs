using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.Restore
{
    partial class FrmBackUpDataBase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBackUpDataBase));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.PanelHeader = new MrPanel();
            this.lnkhistory = new System.Windows.Forms.LinkLabel();
            this.ChkCompress = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.clsSeparator6 = new ClsSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.clsSeparator4 = new ClsSeparator();
            this.clsSeparator1 = new ClsSeparator();
            this.BtnBackupLocation = new DevExpress.XtraEditors.SimpleButton();
            this.BtnBackup = new DevExpress.XtraEditors.SimpleButton();
            this.TxtBackupPath = new MrTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv_Query = new System.Windows.Forms.DataGridView();
            this.panel8 = new MrPanel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Execute = new DevExpress.XtraEditors.SimpleButton();
            this.lbl_TableName = new System.Windows.Forms.Label();
            this.cb_TableName = new System.Windows.Forms.ComboBox();
            this.panel1 = new MrPanel();
            this.btn_CustomQuery = new DevExpress.XtraEditors.SimpleButton();
            this.txt_Query = new MrTextBox();
            this.fb_BackUpDataBase = new System.Windows.Forms.FolderBrowserDialog();
            this.of_ForRestoreData = new System.Windows.Forms.OpenFileDialog();
            this.saveFile_Dialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.PanelHeader.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Query)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(525, 227);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabPage1.Controls.Add(this.PanelHeader);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(517, 194);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Backup";
            // 
            // PanelHeader
            // 
            this.PanelHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelHeader.Controls.Add(this.lnkhistory);
            this.PanelHeader.Controls.Add(this.ChkCompress);
            this.PanelHeader.Controls.Add(this.progressBar);
            this.PanelHeader.Controls.Add(this.clsSeparator6);
            this.PanelHeader.Controls.Add(this.label1);
            this.PanelHeader.Controls.Add(this.clsSeparator4);
            this.PanelHeader.Controls.Add(this.clsSeparator1);
            this.PanelHeader.Controls.Add(this.BtnBackupLocation);
            this.PanelHeader.Controls.Add(this.BtnBackup);
            this.PanelHeader.Controls.Add(this.TxtBackupPath);
            this.PanelHeader.Controls.Add(this.label4);
            this.PanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelHeader.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PanelHeader.Location = new System.Drawing.Point(3, 3);
            this.PanelHeader.Name = "PanelHeader";
            this.PanelHeader.Size = new System.Drawing.Size(511, 188);
            this.PanelHeader.TabIndex = 40;
            // 
            // lnkhistory
            // 
            this.lnkhistory.AutoSize = true;
            this.lnkhistory.BackColor = System.Drawing.Color.Transparent;
            this.lnkhistory.Font = new System.Drawing.Font("Bookman Old Style", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkhistory.Location = new System.Drawing.Point(373, 6);
            this.lnkhistory.Name = "lnkhistory";
            this.lnkhistory.Size = new System.Drawing.Size(120, 16);
            this.lnkhistory.TabIndex = 22;
            this.lnkhistory.TabStop = true;
            this.lnkhistory.Text = "BACKUP HISTORY";
            this.lnkhistory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkHistory_LinkClicked);
            // 
            // ChkCompress
            // 
            this.ChkCompress.AutoSize = true;
            this.ChkCompress.Location = new System.Drawing.Point(173, 112);
            this.ChkCompress.Name = "ChkCompress";
            this.ChkCompress.Size = new System.Drawing.Size(163, 23);
            this.ChkCompress.TabIndex = 21;
            this.ChkCompress.Text = "Compress BackUp";
            this.ChkCompress.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(10, 153);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(483, 23);
            this.progressBar.TabIndex = 19;
            // 
            // clsSeparator6
            // 
            this.clsSeparator6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator6.Location = new System.Drawing.Point(3, 182);
            this.clsSeparator6.Name = "clsSeparator6";
            this.clsSeparator6.Size = new System.Drawing.Size(511, 2);
            this.clsSeparator6.TabIndex = 17;
            this.clsSeparator6.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 29);
            this.label1.TabIndex = 17;
            this.label1.Text = "Backup";
            // 
            // clsSeparator4
            // 
            this.clsSeparator4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator4.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator4.Location = new System.Drawing.Point(9, 144);
            this.clsSeparator4.Name = "clsSeparator4";
            this.clsSeparator4.Size = new System.Drawing.Size(500, 2);
            this.clsSeparator4.TabIndex = 16;
            this.clsSeparator4.TabStop = false;
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clsSeparator1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparator1.Location = new System.Drawing.Point(9, 45);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(500, 2);
            this.clsSeparator1.TabIndex = 15;
            this.clsSeparator1.TabStop = false;
            // 
            // BtnBackupLocation
            // 
            this.BtnBackupLocation.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackupLocation.Appearance.Options.UseFont = true;
            this.BtnBackupLocation.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
            this.BtnBackupLocation.ImageOptions.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnBackupLocation.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.BtnBackupLocation.Location = new System.Drawing.Point(465, 76);
            this.BtnBackupLocation.Name = "BtnBackupLocation";
            this.BtnBackupLocation.Size = new System.Drawing.Size(28, 25);
            this.BtnBackupLocation.TabIndex = 7;
            this.BtnBackupLocation.Click += new System.EventHandler(this.BtnBackupLocation_Click);
            // 
            // BtnBackup
            // 
            this.BtnBackup.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackup.Appearance.Options.UseFont = true;
            this.BtnBackup.Location = new System.Drawing.Point(7, 103);
            this.BtnBackup.Name = "BtnBackup";
            this.BtnBackup.Size = new System.Drawing.Size(133, 38);
            this.BtnBackup.TabIndex = 8;
            this.BtnBackup.Text = "&BACKUP";
            this.BtnBackup.Click += new System.EventHandler(this.BtnBackup_Click);
            // 
            // TxtBackupPath
            // 
            this.TxtBackupPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtBackupPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBackupPath.Location = new System.Drawing.Point(6, 77);
            this.TxtBackupPath.Name = "TxtBackupPath";
            this.TxtBackupPath.Size = new System.Drawing.Size(456, 23);
            this.TxtBackupPath.TabIndex = 1;
            this.TxtBackupPath.Enter += new System.EventHandler(this.TxtBackupPath_Enter);
            this.TxtBackupPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBackupPath_KeyDown);
            this.TxtBackupPath.Leave += new System.EventHandler(this.TxtBackupPath_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Destination";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabPage3.Controls.Add(this.dgv_Query);
            this.tabPage3.Controls.Add(this.panel8);
            this.tabPage3.Controls.Add(this.panel1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(517, 194);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "New Query";
            // 
            // dgv_Query
            // 
            this.dgv_Query.BackgroundColor = System.Drawing.SystemColors.InactiveCaption;
            this.dgv_Query.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dgv_Query.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Query.GridColor = System.Drawing.SystemColors.Control;
            this.dgv_Query.Location = new System.Drawing.Point(0, 101);
            this.dgv_Query.Name = "dgv_Query";
            this.dgv_Query.RowHeadersVisible = false;
            this.dgv_Query.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Query.Size = new System.Drawing.Size(517, 49);
            this.dgv_Query.StandardTab = true;
            this.dgv_Query.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel8.Controls.Add(this.simpleButton3);
            this.panel8.Controls.Add(this.btn_Execute);
            this.panel8.Controls.Add(this.lbl_TableName);
            this.panel8.Controls.Add(this.cb_TableName);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel8.Location = new System.Drawing.Point(0, 150);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(517, 44);
            this.panel8.TabIndex = 40;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton3.Appearance.Options.UseFont = true;
            this.simpleButton3.Location = new System.Drawing.Point(654, 3);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(90, 35);
            this.simpleButton3.TabIndex = 15;
            this.simpleButton3.Text = "&Export";
            this.simpleButton3.Click += new System.EventHandler(this.BtnExportExcel_Click);
            // 
            // btn_Execute
            // 
            this.btn_Execute.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Execute.Appearance.Options.UseFont = true;
            this.btn_Execute.Location = new System.Drawing.Point(542, 3);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(107, 36);
            this.btn_Execute.TabIndex = 14;
            this.btn_Execute.Text = "&Execute";
            this.btn_Execute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // lbl_TableName
            // 
            this.lbl_TableName.AutoSize = true;
            this.lbl_TableName.Location = new System.Drawing.Point(20, 11);
            this.lbl_TableName.Name = "lbl_TableName";
            this.lbl_TableName.Size = new System.Drawing.Size(97, 19);
            this.lbl_TableName.TabIndex = 5;
            this.lbl_TableName.Text = "Table Name";
            // 
            // cb_TableName
            // 
            this.cb_TableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TableName.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_TableName.FormattingEnabled = true;
            this.cb_TableName.Location = new System.Drawing.Point(134, 8);
            this.cb_TableName.Name = "cb_TableName";
            this.cb_TableName.Size = new System.Drawing.Size(389, 27);
            this.cb_TableName.TabIndex = 4;
            this.cb_TableName.Enter += new System.EventHandler(this.CmbTableName_Enter);
            this.cb_TableName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CmbTableName_KeyDown);
            this.cb_TableName.Leave += new System.EventHandler(this.CmbTableName_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.btn_CustomQuery);
            this.panel1.Controls.Add(this.txt_Query);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(517, 101);
            this.panel1.TabIndex = 41;
            // 
            // btn_CustomQuery
            // 
            this.btn_CustomQuery.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btn_CustomQuery.Appearance.Options.UseFont = true;
            this.btn_CustomQuery.Location = new System.Drawing.Point(3, 63);
            this.btn_CustomQuery.Name = "btn_CustomQuery";
            this.btn_CustomQuery.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
            this.btn_CustomQuery.Size = new System.Drawing.Size(128, 32);
            this.btn_CustomQuery.TabIndex = 16;
            this.btn_CustomQuery.Text = "Execute &Query";
            this.btn_CustomQuery.Click += new System.EventHandler(this.BtnCustomQuery_Click);
            // 
            // txt_Query
            // 
            this.txt_Query.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Query.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_Query.Location = new System.Drawing.Point(0, 0);
            this.txt_Query.Multiline = true;
            this.txt_Query.Name = "txt_Query";
            this.txt_Query.Size = new System.Drawing.Size(517, 59);
            this.txt_Query.TabIndex = 1;
            this.txt_Query.Enter += new System.EventHandler(this.TxtQuery_Enter);
            this.txt_Query.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtQuery_KeyDown);
            this.txt_Query.Leave += new System.EventHandler(this.TxtQuery_Leave);
            // 
            // of_ForRestoreData
            // 
            this.of_ForRestoreData.FileName = "openFileDialog1";
            // 
            // FrmBackUpDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(525, 227);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBackUpDataBase";
            this.ShowIcon = false;
            this.Text = "Database Backup Tool";
            this.Load += new System.EventHandler(this.FrmBackUpDataBase_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmBackUpDataBase_KeyPress);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.PanelHeader.ResumeLayout(false);
            this.PanelHeader.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Query)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.FolderBrowserDialog fb_BackUpDataBase;
        private System.Windows.Forms.OpenFileDialog of_ForRestoreData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFile_Dialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lbl_TableName;
        private System.Windows.Forms.ComboBox cb_TableName;
        private System.Windows.Forms.DataGridView dgv_Query;
        private DevExpress.XtraEditors.SimpleButton BtnBackupLocation;
        private DevExpress.XtraEditors.SimpleButton BtnBackup;
        private DevExpress.XtraEditors.SimpleButton btn_Execute;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton btn_CustomQuery;
        private ClsSeparator clsSeparator6;
        private System.Windows.Forms.Label label1;
        private ClsSeparator clsSeparator4;
        private ClsSeparator clsSeparator1;
        private System.Windows.Forms.ProgressBar progressBar;
		private System.Windows.Forms.CheckBox ChkCompress;
        private System.Windows.Forms.LinkLabel lnkhistory;
        private MrTextBox TxtBackupPath;
        private MrTextBox txt_Query;
        private MrPanel PanelHeader;
        private MrPanel panel8;
        private MrPanel panel1;
    }
}