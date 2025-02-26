using MrDAL.Control.ControlsEx.Control;

namespace MrBLL.Utility.ImportUtility
{
	partial class FrmImportData
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
            this.StorePanel = new MrDAL.Control.ControlsEx.Control.MrPanel();
            this.BtnCompany = new System.Windows.Forms.Button();
            this.TxtCompany = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.clsSeparator3 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.clsSeparatorH1 = new MrDAL.Control.ControlsEx.Control.ClsSeparatorH();
            this.ChkTransaction = new System.Windows.Forms.CheckBox();
            this.clsSeparator2 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.ChkCopyMaster = new System.Windows.Forms.CheckBox();
            this.clsSeparator1 = new MrDAL.Control.ControlsEx.Control.ClsSeparator();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtDatabase = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.btnConnect = new DevExpress.XtraEditors.SimpleButton();
            this.TxtServerUser = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.TxtPassword = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.ChkListEntry = new System.Windows.Forms.CheckedListBox();
            this.ChkListMaster = new System.Windows.Forms.CheckedListBox();
            this.TxtServerInfo = new MrDAL.Control.ControlsEx.Control.MrTextBox();
            this.lbl_Login = new System.Windows.Forms.Label();
            this.lbl_DatabaseServer = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnUpdate = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.BtnImport = new DevExpress.XtraEditors.SimpleButton();
            this.ckbSelectAll = new System.Windows.Forms.CheckBox();
            this.StorePanel.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // StorePanel
            // 
            this.StorePanel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.StorePanel.Controls.Add(this.BtnCompany);
            this.StorePanel.Controls.Add(this.TxtCompany);
            this.StorePanel.Controls.Add(this.clsSeparator3);
            this.StorePanel.Controls.Add(this.clsSeparatorH1);
            this.StorePanel.Controls.Add(this.ChkTransaction);
            this.StorePanel.Controls.Add(this.clsSeparator2);
            this.StorePanel.Controls.Add(this.ChkCopyMaster);
            this.StorePanel.Controls.Add(this.clsSeparator1);
            this.StorePanel.Controls.Add(this.label1);
            this.StorePanel.Controls.Add(this.label2);
            this.StorePanel.Controls.Add(this.TxtDatabase);
            this.StorePanel.Controls.Add(this.btnConnect);
            this.StorePanel.Controls.Add(this.TxtServerUser);
            this.StorePanel.Controls.Add(this.TxtPassword);
            this.StorePanel.Controls.Add(this.lbl_Password);
            this.StorePanel.Controls.Add(this.ChkListEntry);
            this.StorePanel.Controls.Add(this.ChkListMaster);
            this.StorePanel.Controls.Add(this.TxtServerInfo);
            this.StorePanel.Controls.Add(this.lbl_Login);
            this.StorePanel.Controls.Add(this.lbl_DatabaseServer);
            this.StorePanel.Controls.Add(this.groupBox4);
            this.StorePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StorePanel.Font = new System.Drawing.Font("Bookman Old Style", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StorePanel.Location = new System.Drawing.Point(0, 0);
            this.StorePanel.Name = "StorePanel";
            this.StorePanel.Size = new System.Drawing.Size(526, 479);
            this.StorePanel.TabIndex = 0;
            // 
            // BtnCompany
            // 
            this.BtnCompany.Font = new System.Drawing.Font("Bookman Old Style", 11F);
            this.BtnCompany.ForeColor = System.Drawing.SystemColors.Window;
            this.BtnCompany.Image = global::MrBLL.Properties.Resources.search16;
            this.BtnCompany.Location = new System.Drawing.Point(497, 124);
            this.BtnCompany.Name = "BtnCompany";
            this.BtnCompany.Size = new System.Drawing.Size(24, 24);
            this.BtnCompany.TabIndex = 314;
            this.BtnCompany.TabStop = false;
            this.BtnCompany.UseVisualStyleBackColor = false;
            this.BtnCompany.Click += new System.EventHandler(this.BtnCompany_Click);
            // 
            // TxtCompany
            // 
            this.TxtCompany.BackColor = System.Drawing.SystemColors.Window;
            this.TxtCompany.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtCompany.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtCompany.Location = new System.Drawing.Point(177, 123);
            this.TxtCompany.Name = "TxtCompany";
            this.TxtCompany.ReadOnly = true;
            this.TxtCompany.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtCompany.Size = new System.Drawing.Size(317, 26);
            this.TxtCompany.TabIndex = 31;
            this.TxtCompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtCompany_KeyDown);
            // 
            // clsSeparator3
            // 
            this.clsSeparator3.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator3.Location = new System.Drawing.Point(7, 188);
            this.clsSeparator3.Name = "clsSeparator3";
            this.clsSeparator3.Size = new System.Drawing.Size(516, 2);
            this.clsSeparator3.TabIndex = 30;
            this.clsSeparator3.TabStop = false;
            // 
            // clsSeparatorH1
            // 
            this.clsSeparatorH1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.clsSeparatorH1.Location = new System.Drawing.Point(262, 200);
            this.clsSeparatorH1.Name = "clsSeparatorH1";
            this.clsSeparatorH1.Size = new System.Drawing.Size(3, 224);
            this.clsSeparatorH1.TabIndex = 30;
            this.clsSeparatorH1.TabStop = false;
            // 
            // ChkTransaction
            // 
            this.ChkTransaction.AutoSize = true;
            this.ChkTransaction.ForeColor = System.Drawing.Color.Black;
            this.ChkTransaction.Location = new System.Drawing.Point(280, 161);
            this.ChkTransaction.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkTransaction.Name = "ChkTransaction";
            this.ChkTransaction.Size = new System.Drawing.Size(109, 23);
            this.ChkTransaction.TabIndex = 1;
            this.ChkTransaction.Text = "Copy Entry";
            this.ChkTransaction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkTransaction.UseVisualStyleBackColor = true;
            this.ChkTransaction.CheckedChanged += new System.EventHandler(this.ChkTransaction_CheckedChanged);
            // 
            // clsSeparator2
            // 
            this.clsSeparator2.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator2.Location = new System.Drawing.Point(7, 153);
            this.clsSeparator2.Name = "clsSeparator2";
            this.clsSeparator2.Size = new System.Drawing.Size(516, 2);
            this.clsSeparator2.TabIndex = 29;
            this.clsSeparator2.TabStop = false;
            // 
            // ChkCopyMaster
            // 
            this.ChkCopyMaster.AutoSize = true;
            this.ChkCopyMaster.ForeColor = System.Drawing.Color.Black;
            this.ChkCopyMaster.Location = new System.Drawing.Point(13, 162);
            this.ChkCopyMaster.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkCopyMaster.Name = "ChkCopyMaster";
            this.ChkCopyMaster.Size = new System.Drawing.Size(120, 23);
            this.ChkCopyMaster.TabIndex = 0;
            this.ChkCopyMaster.Text = "Copy Master";
            this.ChkCopyMaster.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkCopyMaster.UseVisualStyleBackColor = true;
            this.ChkCopyMaster.CheckedChanged += new System.EventHandler(this.ChkCopyMaster_CheckedChanged);
            // 
            // clsSeparator1
            // 
            this.clsSeparator1.BackColor = System.Drawing.Color.AliceBlue;
            this.clsSeparator1.Location = new System.Drawing.Point(7, 93);
            this.clsSeparator1.Name = "clsSeparator1";
            this.clsSeparator1.Size = new System.Drawing.Size(516, 2);
            this.clsSeparator1.TabIndex = 28;
            this.clsSeparator1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label1.Location = new System.Drawing.Point(13, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "Database";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.label2.Location = new System.Drawing.Point(177, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "Company Desc";
            // 
            // TxtDatabase
            // 
            this.TxtDatabase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtDatabase.Enabled = false;
            this.TxtDatabase.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtDatabase.Location = new System.Drawing.Point(12, 122);
            this.TxtDatabase.Name = "TxtDatabase";
            this.TxtDatabase.ReadOnly = true;
            this.TxtDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtDatabase.Size = new System.Drawing.Size(159, 26);
            this.TxtDatabase.TabIndex = 4;
            this.TxtDatabase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDatabase_KeyDown);
            // 
            // btnConnect
            // 
            this.btnConnect.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btnConnect.Appearance.Options.UseBackColor = true;
            this.btnConnect.Appearance.Options.UseFont = true;
            this.btnConnect.Appearance.Options.UseForeColor = true;
            this.btnConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnConnect.Location = new System.Drawing.Point(373, 51);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(144, 41);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "&Test Connection";
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // TxtServerUser
            // 
            this.TxtServerUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtServerUser.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtServerUser.Location = new System.Drawing.Point(136, 36);
            this.TxtServerUser.Name = "TxtServerUser";
            this.TxtServerUser.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtServerUser.Size = new System.Drawing.Size(119, 26);
            this.TxtServerUser.TabIndex = 1;
            // 
            // TxtPassword
            // 
            this.TxtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPassword.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtPassword.Location = new System.Drawing.Point(136, 66);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.PasswordChar = '*';
            this.TxtPassword.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtPassword.Size = new System.Drawing.Size(229, 26);
            this.TxtPassword.TabIndex = 2;
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Password.Location = new System.Drawing.Point(9, 69);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(82, 20);
            this.lbl_Password.TabIndex = 21;
            this.lbl_Password.Text = "Password";
            // 
            // ChkListEntry
            // 
            this.ChkListEntry.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListEntry.FormattingEnabled = true;
            this.ChkListEntry.Items.AddRange(new object[] {
            "Opening"});
            this.ChkListEntry.Location = new System.Drawing.Point(280, 200);
            this.ChkListEntry.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListEntry.Name = "ChkListEntry";
            this.ChkListEntry.Size = new System.Drawing.Size(242, 224);
            this.ChkListEntry.TabIndex = 7;
            // 
            // ChkListMaster
            // 
            this.ChkListMaster.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkListMaster.FormattingEnabled = true;
            this.ChkListMaster.Items.AddRange(new object[] {
            "Account Group"});
            this.ChkListMaster.Location = new System.Drawing.Point(12, 200);
            this.ChkListMaster.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ChkListMaster.Name = "ChkListMaster";
            this.ChkListMaster.Size = new System.Drawing.Size(243, 224);
            this.ChkListMaster.TabIndex = 6;
            this.ChkListMaster.SelectedIndexChanged += new System.EventHandler(this.ChkListMaster_SelectedIndexChanged);
            // 
            // TxtServerInfo
            // 
            this.TxtServerInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtServerInfo.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.TxtServerInfo.Location = new System.Drawing.Point(136, 6);
            this.TxtServerInfo.Name = "TxtServerInfo";
            this.TxtServerInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TxtServerInfo.Size = new System.Drawing.Size(384, 26);
            this.TxtServerInfo.TabIndex = 0;
            this.TxtServerInfo.TextChanged += new System.EventHandler(this.TxtServerInfo_TextChanged);
            // 
            // lbl_Login
            // 
            this.lbl_Login.AutoSize = true;
            this.lbl_Login.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_Login.Location = new System.Drawing.Point(9, 39);
            this.lbl_Login.Name = "lbl_Login";
            this.lbl_Login.Size = new System.Drawing.Size(124, 20);
            this.lbl_Login.TabIndex = 19;
            this.lbl_Login.Text = "System Admin";
            // 
            // lbl_DatabaseServer
            // 
            this.lbl_DatabaseServer.AutoSize = true;
            this.lbl_DatabaseServer.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.lbl_DatabaseServer.Location = new System.Drawing.Point(9, 9);
            this.lbl_DatabaseServer.Name = "lbl_DatabaseServer";
            this.lbl_DatabaseServer.Size = new System.Drawing.Size(94, 20);
            this.lbl_DatabaseServer.TabIndex = 17;
            this.lbl_DatabaseServer.Text = "Server Info";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnUpdate);
            this.groupBox4.Controls.Add(this.btn_Cancel);
            this.groupBox4.Controls.Add(this.BtnImport);
            this.groupBox4.Controls.Add(this.ckbSelectAll);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Bookman Old Style", 12F);
            this.groupBox4.Location = new System.Drawing.Point(0, 423);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.groupBox4.Size = new System.Drawing.Size(526, 56);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // BtnUpdate
            // 
            this.BtnUpdate.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpdate.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BtnUpdate.Appearance.Options.UseBackColor = true;
            this.BtnUpdate.Appearance.Options.UseFont = true;
            this.BtnUpdate.Appearance.Options.UseForeColor = true;
            this.BtnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnUpdate.Location = new System.Drawing.Point(158, 15);
            this.BtnUpdate.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.BtnUpdate.Name = "BtnUpdate";
            this.BtnUpdate.Size = new System.Drawing.Size(117, 32);
            this.BtnUpdate.TabIndex = 0;
            this.BtnUpdate.Text = "&UPDATE";
            this.BtnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.btn_Cancel.Appearance.Options.UseFont = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Cancel.ImageOptions.Image = global::MrBLL.Properties.Resources.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(394, 15);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(115, 32);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnImport
            // 
            this.BtnImport.Appearance.Font = new System.Drawing.Font("Bookman Old Style", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImport.Appearance.ForeColor = System.Drawing.SystemColors.MenuText;
            this.BtnImport.Appearance.Options.UseBackColor = true;
            this.BtnImport.Appearance.Options.UseFont = true;
            this.BtnImport.Appearance.Options.UseForeColor = true;
            this.BtnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BtnImport.Location = new System.Drawing.Point(277, 15);
            this.BtnImport.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.BtnImport.Name = "BtnImport";
            this.BtnImport.Size = new System.Drawing.Size(117, 32);
            this.BtnImport.TabIndex = 1;
            this.BtnImport.Text = "&IMPORT";
            this.BtnImport.Click += new System.EventHandler(this.BtnImport_Click);
            // 
            // ckbSelectAll
            // 
            this.ckbSelectAll.AutoSize = true;
            this.ckbSelectAll.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.ForeColor = System.Drawing.Color.Black;
            this.ckbSelectAll.Location = new System.Drawing.Point(4, 19);
            this.ckbSelectAll.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ckbSelectAll.Name = "ckbSelectAll";
            this.ckbSelectAll.Size = new System.Drawing.Size(102, 24);
            this.ckbSelectAll.TabIndex = 0;
            this.ckbSelectAll.Text = "Select All";
            this.ckbSelectAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbSelectAll.UseVisualStyleBackColor = true;
            this.ckbSelectAll.CheckedChanged += new System.EventHandler(this.CkbSelectAll_CheckedChanged);
            // 
            // FrmImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 479);
            this.Controls.Add(this.StorePanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FrmImportData";
            this.ShowIcon = false;
            this.Text = "LOCAL SERVER DATA IMPORT";
            this.Load += new System.EventHandler(this.FrmImportData_Load);
            this.StorePanel.ResumeLayout(false);
            this.StorePanel.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion
		private DevExpress.XtraEditors.SimpleButton btnConnect;
		private System.Windows.Forms.Label lbl_DatabaseServer;
		private System.Windows.Forms.Label lbl_Password;
		private System.Windows.Forms.Label lbl_Login;
		private System.Windows.Forms.CheckedListBox ChkListEntry;
		private System.Windows.Forms.GroupBox groupBox4;
		private DevExpress.XtraEditors.SimpleButton btn_Cancel;
		private DevExpress.XtraEditors.SimpleButton BtnImport;
		private System.Windows.Forms.CheckBox ckbSelectAll;
		private System.Windows.Forms.CheckedListBox ChkListMaster;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox ChkTransaction;
		private System.Windows.Forms.CheckBox ChkCopyMaster;
		private ClsSeparator clsSeparator1;
		private ClsSeparator clsSeparator2;
		private ClsSeparatorH clsSeparatorH1;
		private ClsSeparator clsSeparator3;
		private DevExpress.XtraEditors.SimpleButton BtnUpdate;
		private System.Windows.Forms.Button BtnCompany;
        private MrPanel StorePanel;
        private MrTextBox TxtServerUser;
        private MrTextBox TxtServerInfo;
        private MrTextBox TxtPassword;
        private MrTextBox TxtDatabase;
        private MrTextBox TxtCompany;
    }
}